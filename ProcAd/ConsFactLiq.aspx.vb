Public Class ConsFactLiq
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 1

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        Dim sdaEmpleado As New SqlDataAdapter
                        Dim dsEmpleado As New DataSet
                        sdaEmpleado.SelectCommand = New SqlCommand("select perfil, nombre + ' ' + ap_paterno + ' ' + ap_materno as empleado " + _
                                                                   "from cg_usuario " + _
                                                                   "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                                   "where id_usuario = @idUsuario ", ConexionBD)
                        sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        sdaEmpleado.Fill(dsEmpleado)
                        ConexionBD.Close()
                        ._txtPerfil.Text = dsEmpleado.Tables(0).Rows(0).Item("perfil").ToString()
                        ._txtEmpleado.Text = dsEmpleado.Tables(0).Rows(0).Item("empleado").ToString()
                        sdaEmpleado.Dispose()
                        dsEmpleado.Dispose()

                        'Llenar listas
                        'Empresas
                        Dim sdaEmpresa As New SqlDataAdapter
                        Dim dsEmpresa As New DataSet
                        sdaEmpresa.SelectCommand = New SqlCommand("select nombre, rfc " + _
                                                                  "from bd_Empleado.dbo.cg_empresa " + _
                                                                  "where status = 'A' " + _
                                                                  "  and rfc is not null " + _
                                                                  "order by nombre ", ConexionBD)
                        .ddlEmpresa.DataSource = dsEmpresa
                        .ddlEmpresa.DataTextField = "nombre"
                        .ddlEmpresa.DataValueField = "rfc"
                        ConexionBD.Open()
                        sdaEmpresa.Fill(dsEmpresa)
                        .ddlEmpresa.DataBind()
                        ConexionBD.Close()
                        sdaEmpresa.Dispose()
                        dsEmpresa.Dispose()
                        .ddlEmpresa.SelectedIndex = 11

                        'Limpiar Pantalla
                        limpiar()
                    Else
                        Server.Transfer("Login.aspx")
                    End If
                Catch ex As Exception
                    .litError.Text = ex.ToString
                End Try
            End With
        End If
    End Sub

#End Region

#Region "Funciones"

    Public Sub limpiar()
        With Me
            .pnlFiltros.Visible = True
            'Filtros
            .cbEmpresa.Checked = False
            .pnlEmpresa.Visible = False

            'Fechas
            .cbFechaC.Checked = False
            .pnlFechaC.Visible = False
            .wdpFechaI.Date = Date.Now.ToShortDateString
            .wdpFechaF.Date = Date.Now.ToShortDateString
            .cbProveedor.Checked = False

            .pnlProveedor.Visible = False
            .cbRFCProv.Checked = False
            .pnlRFCProv.Visible = False
            .cbFolio.Checked = False
            .pnlFolio.Visible = False
            .cbCFDI.Checked = False
            .pnlCFDI.Visible = False
            'Ocultar resto de paneles principales
            .pnlRegistros.Visible = False
        End With
    End Sub

    Public Sub vista(ByRef control, ByVal valor)
        With Me
            If valor Then
                control.Visible = True
            Else
                control.Visible = False
            End If
        End With
    End Sub

#End Region

#Region "Filtros"

    Protected Sub cbEmpresa_CheckedChanged(sender As Object, e As EventArgs) Handles cbEmpresa.CheckedChanged
        vista(Me.pnlEmpresa, Me.cbEmpresa.Checked)
    End Sub

    Protected Sub cbFechaC_CheckedChanged(sender As Object, e As EventArgs) Handles cbFechaC.CheckedChanged
        vista(Me.pnlFechaC, Me.cbFechaC.Checked)
    End Sub

    Protected Sub cbProveedor_CheckedChanged(sender As Object, e As EventArgs) Handles cbProveedor.CheckedChanged
        vista(Me.pnlProveedor, Me.cbProveedor.Checked)
        If Me.cbProveedor.Checked = True Then
            Me.txtProveedor.Text = ""
        End If
    End Sub

    Protected Sub cbRFCProv_CheckedChanged(sender As Object, e As EventArgs) Handles cbRFCProv.CheckedChanged
        vista(Me.pnlRFCProv, Me.cbRFCProv.Checked)
        If Me.cbRFCProv.Checked = True Then
            Me.txtRFCProv.Text = ""
        End If
    End Sub

    Protected Sub cbNoSol_CheckedChanged(sender As Object, e As EventArgs) Handles cbFolio.CheckedChanged
        vista(Me.pnlFolio, Me.cbFolio.Checked)
        If Me.cbFolio.Checked = True Then
            Me.txtFolio.Text = ""
        End If
    End Sub

    Protected Sub cbCFDI_CheckedChanged(sender As Object, e As EventArgs) Handles cbCFDI.CheckedChanged
        vista(Me.pnlCFDI, Me.cbCFDI.Checked)
        If Me.cbCFDI.Checked = True Then
            Me.txtCFDI.Text = ""
        End If
    End Sub

#End Region

#Region "Buscar"

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        With Me
            Try
                .litError.Text = ""

                'Llenar el GridView
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaConsulta As New SqlDataAdapter
                Dim dsConsulta As New DataSet
                Dim query As String

                query = "select id_dt_factura as ID " + _
                        "	  , fecha_carga " + _
                        "	  , fecha_emision " + _
                        "	  , uuid " + _
                        "     , 'http://148.223.153.43/Facturas/' + dt_factura.rfc_receptor + '/' + format(datepart(year, dt_factura.fecha_emision), '00') + '/' + upper(format(dt_factura.fecha_emision, 'MMMM', 'es-es')) + '/' + dt_factura.movimiento + '/' + dt_factura.tipo + '/' + dt_factura.uuid + '.pdf' as path " + _
                        "     , 'PDF' as pdf " + _
                        "	  , serie + ' ' + folio as folio_factura " + _
                        "	  , razon_emisor " + _
                        "	  , razon_receptor " + _
                        "	  , forma_pago " + _
                        "	  , importe " + _
                        "	  , moneda " + _
                        "     , case status " + _
                        "	     when 'P' then 'Disponible' " + _
                        "	     when 'As' then 'Asignada' " + _
                        "	     when 'CP' then 'Comprobación en Proceso' " + _
                        "	     when 'CA' then 'Comprobación Autorizada' " + _
                        "	     when 'CR' then 'Comprobación Registrada' " + _
                        "	     when 'A' then 'Factura Autorizada' " + _
                        "	     when 'R' then 'Cuenta Asignada' " + _
                        "	     when 'RN' then 'Factura Registrada en NAV' " + _
                        "	     when 'ZSAT' then 'Cancelada por el SAT' " + _
                        "		 else '-' " + _
                        "       end as status " + _
                        "from dt_factura " + _
                        "where dt_factura.estatus <> 'CANCELADO' " + _
                        "  and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " + _
                        "  and dt_factura.importe > 0 " + _
                        "  and (case when rfc_emisor = 'SCT051121M62' then case when dt_factura.importe < 80000 then 0 else 1 end else case when dt_factura.importe < 20000 then 0 else 1 end end) = 0 "

                If .cbEmpresa.Checked = True Then
                    query = query + "  and rfc_receptor = @empresa "
                End If
                If .cbFechaC.Checked = True Then
                    query = query + "  and (fecha_emision between @fechaIni and @fechaFin) "
                End If
                If .cbProveedor.Checked = True Then
                    query = query + "  and razon_emisor like '%' + @razon_emisor + '%' "
                End If
                If .cbRFCProv.Checked = True Then
                    query = query + "  and rfc_emisor like '%' + @rfc_emisor + '%' "
                End If
                If .cbFolio.Checked = True Then
                    query = query + "  and serie + ' ' + folio like '%' + @folio + '%' "
                End If
                If .cbCFDI.Checked = True Then
                    query = query + "  and uuid like '%' + @cfdi + '%' "
                End If
                query = query + "order by fecha_emision "

                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)

                If .cbEmpresa.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedValue)
                End If
                If .cbFechaC.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaIni", .wdpFechaI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaFin", .wdpFechaF.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
                End If
                If .cbProveedor.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@razon_emisor", .txtProveedor.Text.Trim.ToUpper)
                End If
                If .cbRFCProv.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@rfc_emisor", .txtRFCProv.Text.Trim.ToUpper)
                End If
                If .cbFolio.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@folio", .txtFolio.Text.Trim.ToUpper)
                End If
                If .cbCFDI.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@cfdi", .txtCFDI.Text.Trim.ToUpper)
                End If

                .gvRegistros.DataSource = dsConsulta
                ConexionBD.Open()
                sdaConsulta.Fill(dsConsulta)
                ConexionBD.Close()
                .gvRegistros.DataBind()
                sdaConsulta.Dispose()
                dsConsulta.Dispose()
                .gvRegistros.SelectedIndex = -1
                If .gvRegistros.Rows.Count = 0 Then
                    .pnlRegistros.Visible = False
                    .litError.Text = "No existe Registros para esos valores"
                Else
                    .pnlRegistros.Visible = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class