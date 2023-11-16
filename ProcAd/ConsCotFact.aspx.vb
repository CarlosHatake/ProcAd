Public Class ConsCotFact
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
                        sdaEmpresa.SelectCommand = New SqlCommand("select distinct(empresa) as empresa " + _
                                                                  "from ms_factura msFact " + _
                                                                  "  inner join dt_cotizacion as dtCot1 on msFact.id_ms_factura = dtCot1.id_ms_factura and dtCot1.no_cotizacion = 1 " + _
                                                                  "order by empresa ", ConexionBD)
                        .ddlEmpresa.DataSource = dsEmpresa
                        .ddlEmpresa.DataTextField = "empresa"
                        .ddlEmpresa.DataValueField = "empresa"
                        ConexionBD.Open()
                        sdaEmpresa.Fill(dsEmpresa)
                        .ddlEmpresa.DataBind()
                        ConexionBD.Close()
                        sdaEmpresa.Dispose()
                        dsEmpresa.Dispose()
                        .ddlEmpresa.SelectedIndex = -1

                        'Solicitantes
                        Dim sdaSolicitante As New SqlDataAdapter
                        Dim dsSolicitante As New DataSet
                        sdaSolicitante.SelectCommand = New SqlCommand("select distinct(empleado) as empleado " + _
                                                                      "from ms_factura msFact " + _
                                                                      "  inner join dt_cotizacion as dtCot1 on msFact.id_ms_factura = dtCot1.id_ms_factura and dtCot1.no_cotizacion = 1 " + _
                                                                      "order by empleado ", ConexionBD)
                        .ddlSolicitante.DataSource = dsSolicitante
                        .ddlSolicitante.DataTextField = "empleado"
                        .ddlSolicitante.DataValueField = "empleado"
                        ConexionBD.Open()
                        sdaSolicitante.Fill(dsSolicitante)
                        .ddlSolicitante.DataBind()
                        ConexionBD.Close()
                        sdaSolicitante.Dispose()
                        dsSolicitante.Dispose()
                        .ddlSolicitante.SelectedIndex = -1

                        'Comprador
                        Dim sdaAutorizador As New SqlDataAdapter
                        Dim dsAutorizador As New DataSet
                        sdaAutorizador.SelectCommand = New SqlCommand("select distinct(cgEmplC.nombre + ' ' + cgEmplC.ap_paterno + ' ' + cgEmplC.ap_materno) as comprador " +
                                                                      "from ms_factura msFact " +
                                                                      "  inner join dt_cotizacion as dtCot1 on msFact.id_ms_factura = dtCot1.id_ms_factura and dtCot1.no_cotizacion = 1 " +
                                                                      "  left join cg_usuario cgUsrC on msFact.id_usr_cotiza = cgUsrC.id_usuario " +
                                                                      "  left join bd_Empleado.dbo.cg_empleado cgEmplC on cgUsrC.id_empleado = cgEmplC.id_empleado " +
                                                                      "order by comprador ", ConexionBD)
                        .ddlComprador.DataSource = dsAutorizador
                        .ddlComprador.DataTextField = "comprador"
                        .ddlComprador.DataValueField = "comprador"
                        ConexionBD.Open()
                        sdaAutorizador.Fill(dsAutorizador)
                        .ddlComprador.DataBind()
                        ConexionBD.Close()
                        sdaAutorizador.Dispose()
                        dsAutorizador.Dispose()
                        .ddlComprador.SelectedIndex = -1

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
            .cbSolicitante.Checked = False
            .pnlSolicitante.Visible = False
            'Fechas
            .cbFechaC.Checked = False
            .pnlFechaC.Visible = False
            .wdpFechaI.Date = Date.Now.ToShortDateString
            .wdpFechaF.Date = Date.Now.ToShortDateString
            .cbComprador.Checked = False
            .pnlComprador.Visible = False
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

    Protected Sub cbSolicitante_CheckedChanged(sender As Object, e As EventArgs) Handles cbSolicitante.CheckedChanged
        vista(Me.pnlSolicitante, Me.cbSolicitante.Checked)
    End Sub

    Protected Sub cbEmpresa_CheckedChanged(sender As Object, e As EventArgs) Handles cbEmpresa.CheckedChanged
        vista(Me.pnlEmpresa, Me.cbEmpresa.Checked)
    End Sub

    Protected Sub cbFechaC_CheckedChanged(sender As Object, e As EventArgs) Handles cbFechaC.CheckedChanged
        vista(Me.pnlFechaC, Me.cbFechaC.Checked)
    End Sub

    Protected Sub cbComprador_CheckedChanged(sender As Object, e As EventArgs) Handles cbComprador.CheckedChanged
        vista(Me.pnlComprador, Me.cbComprador.Checked)
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

                query = "select msFact.id_ms_factura as [No.] " + _
                        "     , empresa as Empresa " + _
                        "     , descrip as [Descripción] " + _
                        "     , empleado as Solicitante " + _
                        "     , cgEmplC.nombre + ' ' + cgEmplC.ap_paterno + ' ' + cgEmplC.ap_materno as Comprador " + _
                        "     , cgEmplAut.nombre + ' ' + cgEmplAut.ap_paterno + ' ' + cgEmplAut.ap_materno as [Autorizador Finanzas] " + _
                        "     , dtCot1.prov_nombre as [Proveedor 1] " + _
                        "     , dtCot1.precio as [Cotización 1] " + _
                        "     , cgDivisa1.divisa as [Divisa 1] " + _
                        "     , case when dtCot1.tipo_cambio = 1 then Null else dtCot1.tipo_cambio end as [Tipo Cambio 1] " + _
                        "     , dtCot2.prov_nombre as [Proveedor 2] " + _
                        "     , dtCot2.precio as [Cotización 2] " + _
                        "     , cgDivisa2.divisa as [Divisa 2] " + _
                        "     , case when dtCot2.tipo_cambio = 1 then Null else dtCot2.tipo_cambio end as [Tipo Cambio 2] " + _
                        "     , dtCot3.prov_nombre as [Proveedor 3] " + _
                        "     , dtCot3.precio as [Cotización 3] " + _
                        "     , cgDivisa3.divisa as [Divisa 3] " + _
                        "     , case when dtCot3.tipo_cambio = 1 then Null else dtCot3.tipo_cambio end as [Tipo Cambio 3] " + _
                        "     , case when cotizacion_selec > 0 then cotizacion_selec else Null end as [Proveedor Seleccionado] " + _
                        "     , msFact.fecha_solicita as [Fecha Solicitud] " + _
                        "     , msFact.fecha_cotiza as [Fecha Cotización] " + _
                        "     , msFact.fecha_finanzas as [Fecha Autoriza Finanzas] " + _
                        "     , case cg_actividad_inst.nombre_actividad " + _
                        "         when 'Autorizar Factura' then 'Pendiente de Autorización' " + _
                        "         when 'Corregir Factura' then 'Pendiente de Correción' " + _
                        "         when 'Validar Solicitud' then 'En Validación Técnica' " + _
                        "         when 'Solicitud Cancelada' then 'Rechazado' " + _
                        "         when 'Ingresar Cotizaciones' then 'En Cotización' " + _
                        "         when 'Autorizar Solicitud' then 'Pendiente de Aut. Finanzas' " + _
                        "         when 'Ingresar Factura' then 'Por Ingresar Factura' " + _
                        "         when 'Autorizar Factura (2da)' then 'Pendiente de Autorización' " + _
                        "         when 'Autorizar Factura (3ra)' then 'Pendiente de Autorización' " + _
                        "         when 'Asignar Cuenta' then 'Asignando Cuenta' " + _
                        "         when 'Ingreso de Factura Cancelado' then 'Cancelado' " + _
                        "         when 'Cuenta Asignada' then " + _
                        "           case when msFact.status = 'RN' then 'Registrado en NAV' " + _
                        "             else 'Pendiente de Registro en NAV' " + _
                        "           end " + _
                        "         when 'Complementar Datos de Contrato' then 'Pendiente de Complementación' " + _
                        "         when 'Autorizar Sol. de Contrato' then 'Pendiente de Autorización de Contrato' " + _
                        "         when 'Solicitud de Contrato Rechazada' then 'Solicitud de Contrato Rechazada' " + _
                        "         when 'Asignar Cuentas para Contrato' then 'Asignando Cuentas para Contrato' " + _
                        "         when 'Registrar Contrato en NAV' then 'Registrando Contrato' " + _
                        "         else " + _
                        "           case " + _
                        "             when msFact.status = 'RN' then 'Registrado en NAV' " + _
                        "             else '-' " + _
                        "           end " + _
                        "       end as Estatus " + _
                        "from ms_factura msFact " + _
                        "  inner join dt_cotizacion as dtCot1 on msFact.id_ms_factura = dtCot1.id_ms_factura and dtCot1.no_cotizacion = 1 " + _
                        "  left join bd_SiSAC.dbo.cg_divisa as cgDivisa1 on dtCot1.id_divisa = cgDivisa1.id_divisa " + _
                        "  left join dt_cotizacion as dtCot2 on msFact.id_ms_factura = dtCot2.id_ms_factura and dtCot2.no_cotizacion = 2 " + _
                        "  left join bd_SiSAC.dbo.cg_divisa as cgDivisa2 on dtCot2.id_divisa = cgDivisa2.id_divisa " + _
                        "  left join dt_cotizacion as dtCot3 on msFact.id_ms_factura = dtCot3.id_ms_factura and dtCot3.no_cotizacion = 3 " + _
                        "  left join bd_SiSAC.dbo.cg_divisa as cgDivisa3 on dtCot3.id_divisa = cgDivisa3.id_divisa " + _
                        "  left join ms_instancia on msFact.id_ms_factura = ms_instancia.id_ms_sol and ms_instancia.tipo = 'F' " + _
                        "  left join cg_actividad_inst on ms_instancia.id_actividad = cg_actividad_inst.id_actividad " + _
                        "  left join cg_usuario cgUsrC on msFact.id_usr_cotiza = cgUsrC.id_usuario " + _
                        "  left join bd_Empleado.dbo.cg_empleado cgEmplC on cgUsrC.id_empleado = cgEmplC.id_empleado " + _
                        "  left join cg_usuario cgUsrAut on msFact.id_usr_finanzas = cgUsrAut.id_usuario " + _
                        "  left join bd_Empleado.dbo.cg_empleado cgEmplAut on cgUsrAut.id_empleado = cgEmplAut.id_empleado " + _
                        "where 1 = 1 "

                If .cbEmpresa.Checked = True Then
                    query = query + "  and empresa = @empresa "
                End If
                If .cbSolicitante.Checked = True Then
                    query = query + "  and empleado = @empleado "
                End If
                If .cbFechaC.Checked = True Then
                    query = query + "  and msFact.fecha_solicita between @fi and @ft "
                End If
                If .cbComprador.Checked = True Then
                    query = query + "  and cgEmplC.nombre + ' ' + cgEmplC.ap_paterno + ' ' + cgEmplC.ap_materno = @comprador "
                End If
                query = query + "order by msFact.id_ms_factura "

                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)

                If .cbEmpresa.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedValue)
                End If
                If .cbSolicitante.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@empleado", .ddlSolicitante.SelectedValue)
                End If
                If .cbFechaC.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fi ", .wdpFechaI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@ft", .wdpFechaF.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
                End If
                If .cbComprador.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@comprador", .ddlComprador.SelectedValue)
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