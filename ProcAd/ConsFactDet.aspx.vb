Public Class ConsFactDet
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
            .cbCFDI.Checked = True
            .pnlCFDI.Visible = True
            .cbFolio.Checked = False
            .pnlFolio.Visible = False
            .cbEmpresa.Checked = False
            .pnlEmpresa.Visible = False
            'Fechas
            .cbFechaC.Checked = False
            .pnlFechaC.Visible = False
            .wdpFechaI.Date = Date.Now.ToShortDateString
            .wdpFechaF.Date = Date.Now.ToShortDateString
            'Ocultar resto de paneles principales
            .pnlRegistros.Visible = False
            .pnlDetalle.Visible = False
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

    Protected Sub cbCFDI_CheckedChanged(sender As Object, e As EventArgs) Handles cbCFDI.CheckedChanged
        vista(Me.pnlCFDI, Me.cbCFDI.Checked)
        If Me.cbCFDI.Checked = True Then
            Me.txtCFDI.Text = ""
        End If
    End Sub

    Protected Sub cbNoSol_CheckedChanged(sender As Object, e As EventArgs) Handles cbFolio.CheckedChanged
        vista(Me.pnlFolio, Me.cbFolio.Checked)
        If Me.cbFolio.Checked = True Then
            Me.txtFolio.Text = ""
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
                        "     , 'PDF' as PDF " + _
                        "     , 'http://148.223.153.43/Facturas/' + dt_factura.rfc_receptor + '/' + format(datepart(year, dt_factura.fecha_emision), '00') + '/' + upper(format(dt_factura.fecha_emision, 'MMMM', 'es-es')) + '/' + dt_factura.movimiento + '/' + dt_factura.tipo + '/' + dt_factura.uuid + '.pdf' as path " + _
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
                        "where 1 = 1 "

                If .cbEmpresa.Checked = True Then
                    query = query + "  and rfc_receptor = @empresa "
                End If
                If .cbFechaC.Checked = True Then
                    query = query + "  and (fecha_emision between @fechaIni and @fechaFin) "
                End If
                If .cbCFDI.Checked = True Then
                    query = query + "  and uuid like '%' + @cfdi + '%' "
                End If
                If .cbFolio.Checked = True Then
                    query = query + "  and serie + ' ' + folio like '%' + @folio + '%' "
                End If
                query = query + "order by id_dt_factura "

                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)

                If .cbEmpresa.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedValue)
                End If
                If .cbFechaC.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaIni", .wdpFechaI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaFin", .wdpFechaF.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
                End If
                If .cbCFDI.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@cfdi", .txtCFDI.Text)
                End If
                If .cbFolio.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@folio", .txtFolio.Text.Trim)
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

#Region "Tabla - Solicitudes"

    Protected Sub gvRegistros_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRegistros.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                .pnlFiltros.Visible = False
                .pnlRegistros.Visible = False
                .pnlDetalle.Visible = False

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")

                'Cabecera de Factura
                Dim sdaFacturaC As New SqlDataAdapter
                Dim dsFacturaC As New DataSet
                .gvFacturaC.DataSource = dsFacturaC
                sdaFacturaC.SelectCommand = New SqlCommand("select id_dt_factura as ID " + _
                                                           "	 , fecha_carga " + _
                                                           "	 , fecha_emision " + _
                                                           "	 , uuid " + _
                                                           "     , 'PDF' as PDF " + _
                                                           "     , 'http://148.223.153.43/Facturas/' + dt_factura.rfc_receptor + '/' + format(datepart(year, dt_factura.fecha_emision), '00') + '/' + upper(format(dt_factura.fecha_emision, 'MMMM', 'es-es')) + '/' + dt_factura.movimiento + '/' + dt_factura.tipo + '/' + dt_factura.uuid + '.pdf' as path " + _
                                                           "     , serie + ' ' + folio as folio_factura " + _
                                                           "	 , razon_emisor " + _
                                                           "	 , razon_receptor " + _
                                                           "	 , rfc_receptor " + _
                                                           "	 , forma_pago " + _
                                                           "	 , importe " + _
                                                           "	 , moneda " + _
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
                                                           "from dt_factura  " + _
                                                           "where uuid = @uuid " + _
                                                           "  and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') ", ConexionBD)
                sdaFacturaC.SelectCommand.Parameters.AddWithValue("@uuid", .gvRegistros.SelectedRow.Cells(4).Text)
                ConexionBD.Open()
                sdaFacturaC.Fill(dsFacturaC)
                .gvFacturaC.DataBind()
                ConexionBD.Close()
                sdaFacturaC.Dispose()
                dsFacturaC.Dispose()

                'Líneas de la Factura
                Dim sdaFacturaL As New SqlDataAdapter
                Dim dsFacturaL As New DataSet
                .gvFacturaL.DataSource = dsFacturaL
                sdaFacturaL.SelectCommand = New SqlCommand("select no_ " + _
                                                           "     , no_comp " + _
                                                           "     , fecha_carga " + _
                                                           "     , cantidad " + _
                                                           "     , descripcion " + _
                                                           "     , valor_unitario " + _
                                                           "     , importe " + _
                                                           "     , descuento " + _
                                                           "     , tot_tras_iva " + _
                                                           "     , tot_tras_ieps " + _
                                                           "     , tot_ret_iva " + _
                                                           "     , tot_ret_ieps " + _
                                                           "     , tot_ret_isr " + _
                                                           "     , tipo_tras_1 " + _
                                                           "     , tasa_tras_1 " + _
                                                           "     , tipo_ret_1 " + _
                                                           "     , tasa_ret_1 " + _
                                                           "     , tipo_tras_2 " + _
                                                           "     , tasa_tras_2 " + _
                                                           "     , tipo_ret_2 " + _
                                                           "     , tasa_ret_2 " + _
                                                           "from dt_factura_linea " + _
                                                           "where uuid = @uuid " + _
                                                           "  and dt_factura_linea.movimiento in ('RECIBIDAS', 'RECIBIDA') ", ConexionBD)
                sdaFacturaL.SelectCommand.Parameters.AddWithValue("@uuid", .gvRegistros.SelectedRow.Cells(4).Text)
                ConexionBD.Open()
                sdaFacturaL.Fill(dsFacturaL)
                .gvFacturaL.DataBind()
                ConexionBD.Close()
                sdaFacturaL.Dispose()
                dsFacturaL.Dispose()

                'Comprobaciones
                Dim sdaComp As New SqlDataAdapter
                Dim dsComp As New DataSet
                .gvComp.DataSource = dsComp
                sdaComp.SelectCommand = New SqlCommand("select ms_comp.id_ms_comp " + _
                                                       "     , empresa " + _
                                                       "     , empleado " + _
                                                       "     , autorizador " + _
                                                       "     , director " + _
                                                       "     , periodo_comp " + _
                                                       "     , case status " + _
                                                       "         when 'P' then 'Pendiente de Autorización' " + _
                                                       "         when 'A' then 'Autorizado' " + _
                                                       "         when 'ZA' then 'Comprobación No Autorizada' " + _
                                                       "         when 'ZD' then 'Comprobación No Autorizada por Director' " + _
                                                       "         when 'ZC' then 'Comprobación Cancelada' " + _
                                                       "         when 'ZP' then 'Comprobación Cancelada' " + _
                                                       "         when 'ZU' then 'Comprobación Cancelada por el Usuario' " + _
                                                       "         when 'R' then 'Comprobación Registrada' " + _
                                                       "       end as estatus " + _
                                                       "from ms_comp " + _
                                                       "  left join dt_comp on ms_comp.id_ms_comp = dt_comp.id_ms_comp " + _
                                                       "where dt_comp.tipo = 'F' " + _
                                                       "  and dt_comp.cfdi = @uuid ", ConexionBD)
                sdaComp.SelectCommand.Parameters.AddWithValue("@uuid", .gvRegistros.SelectedRow.Cells(4).Text)
                ConexionBD.Open()
                sdaComp.Fill(dsComp)
                .gvComp.DataBind()
                ConexionBD.Close()
                sdaComp.Dispose()

                'Ingreso de Facturas
                Dim sdaFact As New SqlDataAdapter
                Dim dsFact As New DataSet
                .gvFact.DataSource = dsFact
                sdaFact.SelectCommand = New SqlCommand("select id_ms_factura as no_factura " + _
                                                       "     , empresa " + _
                                                       "     , centro_costo " + _
                                                       "     , empleado " + _
                                                       "     , autorizador " + _
                                                       "     , fecha_solicita " + _
                                                       "     , case ms_factura.status " + _
                                                       "         when 'P' then case when cg_actividad_inst.nombre_actividad = 'Autorizar Factura' then 'Pendiente de Autorización' else 'Pendiente de Correción' end " + _
                                                       "         when 'A' then 'Autorizado' " + _
                                                       "         when 'Z' then 'Rechazado' " + _
                                                       "         when 'R' then 'Cuenta Asignada' " + _
                                                       "         when 'RN' then 'Registrado en NAV' " + _
                                                       "       end as estatus " + _
                                                       "from ms_factura " + _
                                                       "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " + _
                                                       "  left join ms_instancia on ms_factura.id_ms_factura = ms_instancia.id_ms_sol and ms_instancia.tipo = 'F' " + _
                                                       "  left join cg_actividad_inst on ms_instancia.id_actividad = cg_actividad_inst.id_actividad " + _
                                                       "where cfdi = @uuid ", ConexionBD)
                sdaFact.SelectCommand.Parameters.AddWithValue("@uuid", .gvRegistros.SelectedRow.Cells(4).Text)
                ConexionBD.Open()
                sdaFact.Fill(dsFact)
                .gvFact.DataBind()
                ConexionBD.Close()
                sdaFact.Dispose()

                .pnlDetalle.Visible = True
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Nueva Búsqueda"

    Protected Sub btnNueBusProd_Click(sender As Object, e As EventArgs) Handles btnNueBusProd.Click
        Me.pnlFiltros.Visible = True
        Me.pnlRegistros.Visible = True
        Me.pnlDetalle.Visible = False
        Me.gvRegistros.SelectedIndex = -1
    End Sub

#End Region

End Class