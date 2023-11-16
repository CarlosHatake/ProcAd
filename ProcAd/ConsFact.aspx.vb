Public Class ConsFact
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
                        sdaEmpleado.SelectCommand = New SqlCommand("select perfil, nombre + ' ' + ap_paterno + ' ' + ap_materno as empleado " +
                                                                   "from cg_usuario " +
                                                                   "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
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
                        Dim query As String
                        'Solicitantes
                        Dim sdaSolicitante As New SqlDataAdapter
                        Dim dsSolicitante As New DataSet
                        query = "select distinct(id_usr_solicita) as id_usr_solicita, empleado as solicito " +
                                "from ms_factura "
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            query = query + "  left join cg_usuario cgUsrA2 on ms_factura.id_usr_autoriza2 = cgUsrA2.id_usuario " +
                                            "  left join bd_empleado.dbo.cg_empleado cgEmplA2 on cgUsrA2.id_empleado = cgEmplA2.id_empleado " +
                                            "  left join cg_usuario cgUsrA3 on ms_factura.id_usr_autoriza3 = cgUsrA3.id_usuario " +
                                            "  left join bd_empleado.dbo.cg_empleado cgEmplA3 on cgUsrA3.id_empleado = cgEmplA3.id_empleado " +
                                            "where (empleado = @autorizador or ms_factura.autorizador = @autorizador or cgEmplA2.nombre + ' ' + cgEmplA2.ap_paterno + ' ' + cgEmplA2.ap_materno = @autorizador or cgEmplA3.nombre + ' ' + cgEmplA3.ap_paterno + ' ' + cgEmplA3.ap_materno = @autorizador) "
                        End If
                        query = query + "order by solicito "
                        sdaSolicitante.SelectCommand = New SqlCommand(query, ConexionBD)
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            sdaSolicitante.SelectCommand.Parameters.AddWithValue("@autorizador", ._txtEmpleado.Text)
                        End If
                        .ddlSolicitante.DataSource = dsSolicitante
                        .ddlSolicitante.DataTextField = "solicito"
                        .ddlSolicitante.DataValueField = "id_usr_solicita"
                        ConexionBD.Open()
                        sdaSolicitante.Fill(dsSolicitante)
                        .ddlSolicitante.DataBind()
                        ConexionBD.Close()
                        sdaSolicitante.Dispose()
                        dsSolicitante.Dispose()
                        .ddlSolicitante.SelectedIndex = -1
                        'Empresas
                        Dim sdaEmpresa As New SqlDataAdapter
                        Dim dsEmpresa As New DataSet
                        query = "select distinct(empresa) as empresa " +
                                "from ms_factura "
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            query = query + "  left join cg_usuario cgUsrA2 on ms_factura.id_usr_autoriza2 = cgUsrA2.id_usuario " +
                                            "  left join bd_empleado.dbo.cg_empleado cgEmplA2 on cgUsrA2.id_empleado = cgEmplA2.id_empleado " +
                                            "  left join cg_usuario cgUsrA3 on ms_factura.id_usr_autoriza3 = cgUsrA3.id_usuario " +
                                            "  left join bd_empleado.dbo.cg_empleado cgEmplA3 on cgUsrA3.id_empleado = cgEmplA3.id_empleado " +
                                            "where (empleado = @autorizador or ms_factura.autorizador = @autorizador or cgEmplA2.nombre + ' ' + cgEmplA2.ap_paterno + ' ' + cgEmplA2.ap_materno = @autorizador or cgEmplA3.nombre + ' ' + cgEmplA3.ap_paterno + ' ' + cgEmplA3.ap_materno = @autorizador) "
                        End If
                        query = query + "order by empresa "
                        sdaEmpresa.SelectCommand = New SqlCommand(query, ConexionBD)
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            sdaEmpresa.SelectCommand.Parameters.AddWithValue("@autorizador", ._txtEmpleado.Text)
                        End If
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
                        'Autorizadores
                        Dim sdaAutorizador As New SqlDataAdapter
                        Dim dsAutorizador As New DataSet
                        query = "select distinct(autorizador) as autorizador " +
                                "from ms_factura "
                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Then
                            query = query + "where empleado = @empleado "
                        End If
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Or ._txtPerfil.Text = "ContaF" Then
                            query = "select distinct(ms_factura.autorizador) as autorizador " +
                                    "from ms_factura " +
                                    "  left join cg_usuario cgUsrA2 on ms_factura.id_usr_autoriza2 = cgUsrA2.id_usuario " +
                                    "  left join bd_empleado.dbo.cg_empleado cgEmplA2 on cgUsrA2.id_empleado = cgEmplA2.id_empleado " +
                                    "  left join cg_usuario cgUsrA3 on ms_factura.id_usr_autoriza3 = cgUsrA3.id_usuario " +
                                    "  left join bd_empleado.dbo.cg_empleado cgEmplA3 on cgUsrA3.id_empleado = cgEmplA3.id_empleado " +
                                    "where (empleado = @autorizador or ms_factura.autorizador = @autorizador or cgEmplA2.nombre + ' ' + cgEmplA2.ap_paterno + ' ' + cgEmplA2.ap_materno = @autorizador or cgEmplA3.nombre + ' ' + cgEmplA3.ap_paterno + ' ' + cgEmplA3.ap_materno = @autorizador) " +
                                    "union " +
                                    "select distinct(cgEmplA2.nombre + ' ' + cgEmplA2.ap_paterno + ' ' + cgEmplA2.ap_materno) as autorizador " +
                                    "from ms_factura " +
                                    "  left join cg_usuario cgUsrA2 on ms_factura.id_usr_autoriza2 = cgUsrA2.id_usuario " +
                                    "  left join bd_empleado.dbo.cg_empleado cgEmplA2 on cgUsrA2.id_empleado = cgEmplA2.id_empleado " +
                                    "  left join cg_usuario cgUsrA3 on ms_factura.id_usr_autoriza3 = cgUsrA3.id_usuario " +
                                    "  left join bd_empleado.dbo.cg_empleado cgEmplA3 on cgUsrA3.id_empleado = cgEmplA3.id_empleado " +
                                    "where (empleado = @autorizador or ms_factura.autorizador = @autorizador or cgEmplA2.nombre + ' ' + cgEmplA2.ap_paterno + ' ' + cgEmplA2.ap_materno = @autorizador or cgEmplA3.nombre + ' ' + cgEmplA3.ap_paterno + ' ' + cgEmplA3.ap_materno = @autorizador) " +
                                    "  and id_usr_autoriza2 is not null " +
                                    "union " +
                                    "select distinct(cgEmplA3.nombre + ' ' + cgEmplA3.ap_paterno + ' ' + cgEmplA3.ap_materno) as autorizador " +
                                    "from ms_factura " +
                                    "  left join cg_usuario cgUsrA2 on ms_factura.id_usr_autoriza2 = cgUsrA2.id_usuario " +
                                    "  left join bd_empleado.dbo.cg_empleado cgEmplA2 on cgUsrA2.id_empleado = cgEmplA2.id_empleado " +
                                    "  left join cg_usuario cgUsrA3 on ms_factura.id_usr_autoriza3 = cgUsrA3.id_usuario " +
                                    "  left join bd_empleado.dbo.cg_empleado cgEmplA3 on cgUsrA3.id_empleado = cgEmplA3.id_empleado " +
                                    "where (empleado = @autorizador or ms_factura.autorizador = @autorizador or cgEmplA2.nombre + ' ' + cgEmplA2.ap_paterno + ' ' + cgEmplA2.ap_materno = @autorizador or cgEmplA3.nombre + ' ' + cgEmplA3.ap_paterno + ' ' + cgEmplA3.ap_materno = @autorizador) " +
                                    "  and id_usr_autoriza3 is not null "
                        End If
                        query = query + "order by autorizador "
                        sdaAutorizador.SelectCommand = New SqlCommand(query, ConexionBD)
                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Then
                            sdaAutorizador.SelectCommand.Parameters.AddWithValue("@empleado", ._txtEmpleado.Text)
                        End If
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Or ._txtPerfil.Text = "ContaF" Then
                            sdaAutorizador.SelectCommand.Parameters.AddWithValue("@autorizador", ._txtEmpleado.Text)
                        End If
                        .ddlAutorizador.DataSource = dsAutorizador
                        .ddlAutorizador.DataTextField = "autorizador"
                        .ddlAutorizador.DataValueField = "autorizador"
                        ConexionBD.Open()
                        sdaAutorizador.Fill(dsAutorizador)
                        .ddlAutorizador.DataBind()
                        ConexionBD.Close()
                        sdaAutorizador.Dispose()
                        dsAutorizador.Dispose()
                        .ddlAutorizador.SelectedIndex = -1

                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "ContaF" Then
                            .pnlSolicitó.Visible = False
                        Else
                            .pnlSolicitó.Visible = True
                            .cbSolicitante.Checked = False
                            .pnlSolicitante.Visible = False
                            .cbEmpresa.Checked = False
                            .pnlEmpresa.Visible = False
                        End If

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
            'Fechas
            .cbFechaC.Checked = False
            .pnlFechaC.Visible = False
            .wdpFechaI.Date = Date.Now.ToShortDateString
            .wdpFechaF.Date = Date.Now.ToShortDateString
            .cbAutorizador.Checked = False
            .pnlAutorizador.Visible = False
            .cbProveedor.Checked = False
            .pnlProveedor.Visible = False
            .cbNoFact.Checked = False
            .pnlNoFact.Visible = False
            .cbStatus.Checked = False
            .pnlStatus.Visible = False
            .cbContrato.Checked = False
            .pnlContrato.Visible = False
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

    Protected Sub cbSolicitante_CheckedChanged(sender As Object, e As EventArgs) Handles cbSolicitante.CheckedChanged
        vista(Me.pnlSolicitante, Me.cbSolicitante.Checked)
    End Sub

    Protected Sub cbEmpresa_CheckedChanged(sender As Object, e As EventArgs) Handles cbEmpresa.CheckedChanged
        vista(Me.pnlEmpresa, Me.cbEmpresa.Checked)
    End Sub

    Protected Sub cbFechaC_CheckedChanged(sender As Object, e As EventArgs) Handles cbFechaC.CheckedChanged
        vista(Me.pnlFechaC, Me.cbFechaC.Checked)
    End Sub

    Protected Sub cbAutorizador_CheckedChanged(sender As Object, e As EventArgs) Handles cbAutorizador.CheckedChanged
        vista(Me.pnlAutorizador, Me.cbAutorizador.Checked)
    End Sub

    Protected Sub cbProveedor_CheckedChanged(sender As Object, e As EventArgs) Handles cbProveedor.CheckedChanged
        vista(Me.pnlProveedor, Me.cbProveedor.Checked)
        If Me.cbProveedor.Checked = True Then
            Me.txtProveedor.Text = ""
        End If
    End Sub

    Protected Sub cbNoFact_CheckedChanged(sender As Object, e As EventArgs) Handles cbNoFact.CheckedChanged
        vista(Me.pnlNoFact, Me.cbNoFact.Checked)
        If Me.cbNoFact.Checked = True Then
            Me.txtNoFact.Text = ""
        End If
    End Sub

    Protected Sub cbStatus_CheckedChanged(sender As Object, e As EventArgs) Handles cbStatus.CheckedChanged
        vista(Me.pnlStatus, Me.cbStatus.Checked)
    End Sub
    Protected Sub cbContrato_CheckedChanged(sender As Object, e As EventArgs) Handles cbContrato.CheckedChanged
        vista(Me.pnlContrato, Me.cbContrato.Checked)
        If Me.cbContrato.Checked = True Then
            Me.txtContrato.Text = ""
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

                ''Versión 1
                'query = "select ms_factura.id_ms_factura as no_factura " + _
                '        "     , ms_factura.tipo_servicio " + _
                '        "     , ms_factura.empleado " + _
                '        "     , ms_factura.autorizador " + _
                '        "     , cgEmplA2.nombre + ' ' + cgEmplA2.ap_paterno + ' ' + cgEmplA2.ap_materno as autorizador2 " + _
                '        "     , cgEmplA3.nombre + ' ' + cgEmplA3.ap_paterno + ' ' + cgEmplA3.ap_materno as autorizador3 " + _
                '        "     , ms_factura.empresa " + _
                '        "     , ms_factura.centro_costo " + _
                '        "     , dt_factura.razon_emisor " + _
                '        "     , dt_factura.serie + dt_factura.folio as noFactura " + _
                '        "     , importe_tot " + _
                '        "     , convert(varchar, ms_factura.fecha_solicita, 103) + replace(replace(replace(right(convert(varchar, ms_factura.fecha_solicita, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') as fecha_solicita " + _
                '        "     , case when ms_factura.fecha_autoriza is null then null else convert(varchar, ms_factura.fecha_autoriza, 103) + replace(replace(replace(right(convert(varchar, ms_factura.fecha_autoriza, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') end as fecha_autoriza " + _
                '        "     , case when cfdi is null then null else case when id_usr_autoriza2 is null then 'No Aplica' else case when fecha_autoriza2 is null then null else convert(varchar, fecha_autoriza2, 103) + replace(replace(replace(right(convert(varchar, fecha_autoriza2, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') end end end as fecha_autoriza2 " + _
                '        "     , case when cfdi is null then null else case when id_usr_autoriza3 is null then 'No Aplica' else case when fecha_autoriza3 is null then null else convert(varchar, fecha_autoriza3, 103) + replace(replace(replace(right(convert(varchar, fecha_autoriza3, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') end end end as fecha_autoriza3 " + _
                '        "     , ms_factura.fecha_asigna " + _
                '        "     , ms_factura.fecha_regNAV " + _
                '        "     , case when ms_factura.contrato_NAV_alta = 'S' then 'Alta' else case when ms_factura.contrato_NAV_reg = 'S' then 'Registrado' else 'No Aplica' end end as contrato " + _
                '        "     , case when ms_factura.contrato_NAV_alta = 'S' then ms_contrato.no_contrato_NAV else case when ms_factura.contrato_NAV_reg = 'S' then ms_factura.no_contrato_NAV else null end end as no_contrato " + _
                '        "     , case cg_actividad_inst.nombre_actividad " + _
                '        "         when 'Autorizar Factura' then 'Pendiente de Autorización' " + _
                '        "         when 'Corregir Factura' then 'Pendiente de Correción' " + _
                '        "         when 'Validar Solicitud' then 'En Validación Técnica' " + _
                '        "         when 'Solicitud Cancelada' then 'Rechazado' " + _
                '        "         when 'Ingresar Cotizaciones' then 'En Cotización' " + _
                '        "         when 'Autorizar Solicitud' then 'Pendiente de Aut. Finanzas' " + _
                '        "         when 'Validar Presupuesto' then 'Validando Presupuesto' " + _
                '        "         when 'Solicitar Ampliación de Presupuesto' then 'Solicitando Ampliación de Presupuesto' " + _
                '        "         when 'Solicitud Cancelada por el Usuario' then 'Cancelado' " + _
                '        "         when 'Ingresar Factura' then 'Por Ingresar Factura' " + _
                '        "         when 'Autorizar Factura (2da)' then 'Pendiente de Autorización' " + _
                '        "         when 'Autorizar Factura (3ra)' then 'Pendiente de Autorización' " + _
                '        "         when 'Asignar Cuenta' then 'Asignando Cuenta' " + _
                '        "         when 'Ingreso de Factura Cancelado' then 'Cancelado' " + _
                '        "         when 'Cuenta Asignada' then " + _
                '        "           case when ms_factura.status = 'RN' then 'Registrado en NAV' " + _
                '        "             else 'Pendiente de Registro en NAV' " + _
                '        "           end " + _
                '        "         when 'Complementar Datos de Contrato' then 'Pendiente de Complementación' " + _
                '        "         when 'Autorizar Sol. de Contrato' then 'Pendiente de Autorización de Contrato' " + _
                '        "         when 'Solicitud de Contrato Rechazada' then 'Solicitud de Contrato Rechazada' " + _
                '        "         when 'Asignar Cuentas para Contrato' then 'Asignando Cuentas para Contrato' " + _
                '        "         when 'Registrar Contrato en NAV' then 'Registrando Contrato' " + _
                '        "         else " + _
                '        "           case " + _
                '        "             when ms_factura.status = 'RN' then 'Registrado en NAV' " + _
                '        "             else '-' " + _
                '        "           end " + _
                '        "       end as estatus " + _
                '        "from ms_factura " + _
                '        "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid " + _
                '        "  left join ms_instancia on ms_factura.id_ms_factura = ms_instancia.id_ms_sol and ms_instancia.tipo = 'F' " + _
                '        "  left join cg_actividad_inst on ms_instancia.id_actividad = cg_actividad_inst.id_actividad " + _
                '        "  left join cg_usuario cgUsrA2 on ms_factura.id_usr_autoriza2 = cgUsrA2.id_usuario " + _
                '        "  left join bd_empleado.dbo.cg_empleado cgEmplA2 on cgUsrA2.id_empleado = cgEmplA2.id_empleado " + _
                '        "  left join cg_usuario cgUsrA3 on ms_factura.id_usr_autoriza3 = cgUsrA3.id_usuario " + _
                '        "  left join bd_empleado.dbo.cg_empleado cgEmplA3 on cgUsrA3.id_empleado = cgEmplA3.id_empleado " + _
                '        "  left join ms_contrato on ms_factura.id_ms_factura = ms_contrato.id_ms_factura " + _
                '        "where 1 = 1 "

                'Versión 2
                'query = "select ms_factura.id_ms_factura as no_factura " + _
                '        "     , isnull(ms_factura.tipo_servicio, ms_factura.servicio) as tipo_servicio " + _
                '        "     , ms_factura.empleado " + _
                '        "     , ms_factura.validador " + _
                '        "     , cgEmplVal2.nombre + ' ' + cgEmplVal2.ap_paterno + ' ' + cgEmplVal2.ap_materno as validador2 " + _
                '        "     , ms_factura.autorizador " + _
                '        "     , cgEmplA2.nombre + ' ' + cgEmplA2.ap_paterno + ' ' + cgEmplA2.ap_materno as autorizador2 " + _
                '        "     , cgEmplA3.nombre + ' ' + cgEmplA3.ap_paterno + ' ' + cgEmplA3.ap_materno as autorizador3 " + _
                '        "     , ms_factura.empresa " + _
                '        "     , isnull(ms_factura.division, ms_factura.centro_costo) as centro_costo " + _
                '        "     , isnull(dt_factura.razon_emisor, isnull((select nombre " + _
                '        "                                               from bd_SiSAC.dbo.cg_proveedor " + _
                '        "                                               where id_proveedor = (select proveedor_selec " + _
                '        "                                                                     from ms_factura msFact " + _
                '        "                                                                     where msFact.id_ms_factura = ms_factura.id_ms_factura)), '')) as razon_emisor " + _
                '        "     , dt_factura.serie + dt_factura.folio as noFactura " + _
                '        "     , CFDI " + _
                '        "     , importe_tot " + _
                '        "     , ms_factura.fecha_solicita " + _
                '        "     , case when ms_factura.cValidador is null then convert(varchar, ms_factura.fecha_valida, 103) + replace(replace(replace(right(convert(varchar, ms_factura.fecha_valida, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') else case when ms_factura.cValidador = 'N' then 'No Aplica' else convert(varchar, ms_factura.fecha_valida, 103) + replace(replace(replace(right(convert(varchar, ms_factura.fecha_valida, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') end end as fecha_valida " + _
                '        "     , case when ms_factura.cCompras is null then convert(varchar, ms_factura.fecha_cotiza, 103) + replace(replace(replace(right(convert(varchar, ms_factura.fecha_cotiza, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') else case when ms_factura.cCompras = 'N' then 'No Aplica' else convert(varchar, ms_factura.fecha_cotiza, 103) + replace(replace(replace(right(convert(varchar, ms_factura.fecha_cotiza, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') end end as fecha_cotiza " + _
                '        "     , case when ms_factura.cFinanzas is null then convert(varchar, ms_factura.fecha_finanzas, 103) + replace(replace(replace(right(convert(varchar, ms_factura.fecha_finanzas, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') else case when ms_factura.cFinanzas = 'N' then 'No Aplica' else convert(varchar, ms_factura.fecha_finanzas, 103) + replace(replace(replace(right(convert(varchar, ms_factura.fecha_finanzas, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') end end as fecha_finanzas " + _
                '        "     , case when ms_factura.cPresupuesto is null then convert(varchar, ms_factura.fecha_val_presupuesto, 103) + replace(replace(replace(right(convert(varchar, ms_factura.fecha_val_presupuesto, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') else case when ms_factura.cPresupuesto = 'N' then 'No Aplica' else convert(varchar, ms_factura.fecha_val_presupuesto, 103) + replace(replace(replace(right(convert(varchar, ms_factura.fecha_val_presupuesto, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') end end as fecha_val_presupuesto " + _
                '        "     , case when ms_factura.servicio_tipo is null then null else case when ms_factura.servicio_tipo = 'Servicios Negociados' and ms_factura.cValidadorSop = 'S' then convert(varchar, ms_factura.fecha_valida2, 103) + replace(replace(replace(right(convert(varchar, ms_factura.fecha_valida2, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') else 'No Aplica' end end as fecha_valida2 " + _
                '        "     , case when (select top 1 fecha " + _
                '        "                  from ms_historico " + _
                '        "                  where ms_historico.id_ms_instancia = ms_instancia.id_ms_instancia " + _
                '        "                    and ms_historico.id_actividad = 50 " + _
                '        "                  order by fecha) is null and cfdi is not null  " + _
                '        "         then ms_factura.fecha_solicita " + _
                '        "		 else (select top 1 fecha " + _
                '        "               from ms_historico " + _
                '        "               where ms_historico.id_ms_instancia = ms_instancia.id_ms_instancia " + _
                '        "                 and ms_historico.id_actividad = 50 " + _
                '        "               order by fecha) " + _
                '        "       end as fecha_ingreso_factura " + _
                '        "     , case when ms_factura.fecha_autoriza is null then null else convert(varchar, ms_factura.fecha_autoriza, 103) + replace(replace(replace(right(convert(varchar, ms_factura.fecha_autoriza, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') end as fecha_autoriza " + _
                '        "     , case when cfdi is null then null else case when id_usr_autoriza2 is null then 'No Aplica' else case when fecha_autoriza2 is null then null else convert(varchar, fecha_autoriza2, 103) + replace(replace(replace(right(convert(varchar, fecha_autoriza2, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') end end end as fecha_autoriza2 " + _
                '        "     , case when cfdi is null then null else case when id_usr_autoriza3 is null then 'No Aplica' else case when fecha_autoriza3 is null then null else convert(varchar, fecha_autoriza3, 103) + replace(replace(replace(right(convert(varchar, fecha_autoriza3, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') end end end as fecha_autoriza3 " + _
                '        "     , ms_factura.fecha_asigna " + _
                '        "     , ms_factura.fecha_regNAV " + _
                '        "     , case when ms_factura.contrato_NAV_alta = 'S' then 'Alta' else case when ms_factura.contrato_NAV_reg = 'S' then 'Registrado' else 'No Aplica' end end as contrato " + _
                '        "     , case when ms_factura.contrato_NAV_alta = 'S' then ms_contrato.no_contrato_NAV else case when ms_factura.contrato_NAV_reg = 'S' then ms_factura.no_contrato_NAV else null end end as no_contrato " + _
                '        "     , case cg_actividad_inst.nombre_actividad " + _
                '        "         when 'Autorizar Factura' then 'Pendiente de Autorización' " + _
                '        "         when 'Corregir Factura' then 'Corregir Factura' " + _
                '        "         when 'Validar Solicitud' then 'En Validación Técnica' " + _
                '        "         when 'Solicitud Cancelada' then 'Rechazado' " + _
                '        "         when 'Ingresar Cotizaciones' then 'En Cotización' " + _
                '        "         when 'Corregir Solicitud' then 'Corregir Solicitud' " + _
                '        "         when 'Autorizar Solicitud' then 'Pendiente de Aut. Finanzas' " + _
                '        "         when 'Validar Presupuesto' then 'Validando Presupuesto' " + _
                '        "         when 'Solicitar Ampliación de Presupuesto' then 'Solicitando Ampliación de Presupuesto' " + _
                '        "         when 'Solicitud Cancelada por el Usuario' then 'Cancelado' " + _
                '        "         when 'Ingresar Factura' then 'Por Ingresar Factura' " + _
                '        "         when 'Autorizar Factura (2da)' then 'Pendiente de Autorización' " + _
                '        "         when 'Autorizar Factura (3ra)' then 'Pendiente de Autorización' " + _
                '        "         when 'Asignar Cuenta' then 'Asignando Cuenta' " + _
                '        "         when 'Ingreso de Factura Cancelado' then 'Cancelado' " + _
                '        "         when 'Cuenta Asignada' then " + _
                '        "           case when ms_factura.status = 'RN' then 'Registrado en NAV' " + _
                '        "             else 'Pendiente de Registro en NAV' " + _
                '        "           end " + _
                '        "         when 'Complementar Datos de Contrato' then 'Pendiente de Complementación' " + _
                '        "         when 'Autorizar Sol. de Contrato' then 'Pendiente de Autorización de Contrato' " + _
                '        "         when 'Solicitud de Contrato Rechazada' then 'Solicitud de Contrato Rechazada' " + _
                '        "         when 'Asignar Cuentas para Contrato' then 'Asignando Cuentas para Contrato' " + _
                '        "         when 'Registrar Contrato en NAV' then 'Registrando Contrato' " + _
                '        "         when 'Validar Aplicabilidad' then 'En Validación Técnica' " + _
                '        "         when 'Solicitud Rechazada' then 'Rechazado' " + _
                '        "         when 'Autorizar Servicio Negociado' then 'Pendiente de Aut. Finanzas' " + _
                '        "         when 'Solicitud de Servicio Negociado Cancelada' then 'Cancelado' " + _
                '        "         when 'Validar Presupuesto para Servicio Negociado' then 'Validando Presupuesto' " + _
                '        "         when 'Solicitar Ampliación de Presupuesto para Servicio Negociado' then 'Solicitando Ampliación de Presupuesto' " + _
                '        "         when 'Solicitud Cancelada por el Usuario' then 'Cancelado' " + _
                '        "         when 'Ingresar Factura de Servicio Negociado' then 'Por Ingresar Factura' " + _
                '        "         when 'Validar Soportes' then 'Validando Soportes' " + _
                '        "         when 'Factura de Servicio Negociado Rechazada' then 'Rechazado' " + _
                '        "         when 'Corregir Factura de Servicio Negociado' then 'Corregir Factura' " + _
                '        "         else " + _
                '        "           case " + _
                '        "             when ms_factura.status = 'RN' then 'Registrado en NAV' " + _
                '        "             else '-' " + _
                '        "           end " + _
                '        "       end as estatus " + _
                '        "from ms_factura " + _
                '        "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " + _
                '        "  left join ms_instancia on ms_factura.id_ms_factura = ms_instancia.id_ms_sol and ms_instancia.tipo = 'F' " + _
                '        "  left join cg_actividad_inst on ms_instancia.id_actividad = cg_actividad_inst.id_actividad " + _
                '        "  left join cg_usuario cgUsrVal2 on ms_factura.id_usr_valida2 = cgUsrVal2.id_usuario " + _
                '        "  left join bd_Empleado.dbo.cg_empleado cgEmplVal2 on cgUsrVal2.id_empleado = cgEmplVal2.id_empleado " + _
                '        "  left join cg_usuario cgUsrA2 on ms_factura.id_usr_autoriza2 = cgUsrA2.id_usuario " + _
                '        "  left join bd_empleado.dbo.cg_empleado cgEmplA2 on cgUsrA2.id_empleado = cgEmplA2.id_empleado " + _
                '        "  left join cg_usuario cgUsrA3 on ms_factura.id_usr_autoriza3 = cgUsrA3.id_usuario " + _
                '        "  left join bd_empleado.dbo.cg_empleado cgEmplA3 on cgUsrA3.id_empleado = cgEmplA3.id_empleado " + _
                '        "  left join ms_contrato on ms_factura.id_ms_factura = ms_contrato.id_ms_factura " + _
                '        "where 1 = 1 "

                query = "select ms_factura.id_ms_factura as no_factura " +
                        "     , isnull(ms_factura.tipo_servicio, ms_factura.servicio) as tipo_servicio " +
                        "     , ms_factura.empleado " +
                        "     , ms_factura.validador " +
                        "     , cgEmplVal2.nombre + ' ' + cgEmplVal2.ap_paterno + ' ' + cgEmplVal2.ap_materno as validador2 " +
                        "     , ms_factura.autorizador " +
                        "     , cgEmplA2.nombre + ' ' + cgEmplA2.ap_paterno + ' ' + cgEmplA2.ap_materno as autorizador2 " +
                        "     , cgEmplA3.nombre + ' ' + cgEmplA3.ap_paterno + ' ' + cgEmplA3.ap_materno as autorizador3 " +
                        "     , ms_factura.empresa " +
                        "     , isnull(ms_factura.division, ms_factura.centro_costo) as centro_costo " +
                        "     , isnull(dt_factura.razon_emisor, isnull((select nombre " +
                        "                                               from bd_SiSAC.dbo.cg_proveedor " +
                        "                                               where id_proveedor = (select proveedor_selec " +
                        "                                                                     from ms_factura msFact " +
                        "                                                                     where msFact.id_ms_factura = ms_factura.id_ms_factura)), '')) as razon_emisor " +
                        "     , dt_factura.serie + dt_factura.folio as noFactura " +
                        "     , CFDI " +
                        "     , importe_tot " +
                        "     , ms_factura.fecha_solicita " +
                        "     , case when ms_factura.cValidador is null then convert(varchar, ms_factura.fecha_valida, 103) + replace(replace(replace(right(convert(varchar, ms_factura.fecha_valida, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') else case when ms_factura.cValidador = 'N' then 'No Aplica' else convert(varchar, ms_factura.fecha_valida, 103) + replace(replace(replace(right(convert(varchar, ms_factura.fecha_valida, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') end end as fecha_valida " +
                        "     , case when ms_factura.cCompras is null then convert(varchar, ms_factura.fecha_cotiza, 103) + replace(replace(replace(right(convert(varchar, ms_factura.fecha_cotiza, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') else case when ms_factura.cCompras = 'N' then 'No Aplica' else convert(varchar, ms_factura.fecha_cotiza, 103) + replace(replace(replace(right(convert(varchar, ms_factura.fecha_cotiza, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') end end as fecha_cotiza " +
                        "     , case when ms_factura.cFinanzas is null then convert(varchar, ms_factura.fecha_finanzas, 103) + replace(replace(replace(right(convert(varchar, ms_factura.fecha_finanzas, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') else case when ms_factura.cFinanzas = 'N' then 'No Aplica' else convert(varchar, ms_factura.fecha_finanzas, 103) + replace(replace(replace(right(convert(varchar, ms_factura.fecha_finanzas, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') end end as fecha_finanzas " +
                        "     , case when ms_factura.cPresupuesto is null then convert(varchar, ms_factura.fecha_val_presupuesto, 103) + replace(replace(replace(right(convert(varchar, ms_factura.fecha_val_presupuesto, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') else case when ms_factura.cPresupuesto = 'N' then 'No Aplica' else convert(varchar, ms_factura.fecha_val_presupuesto, 103) + replace(replace(replace(right(convert(varchar, ms_factura.fecha_val_presupuesto, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') end end as fecha_val_presupuesto " +
                        "     , case when ms_factura.servicio_tipo is null then null else case when ms_factura.servicio_tipo = 'Servicios Negociados' and ms_factura.cValidadorSop = 'S' then convert(varchar, ms_factura.fecha_valida2, 103) + replace(replace(replace(right(convert(varchar, ms_factura.fecha_valida2, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') else 'No Aplica' end end as fecha_valida2 " +
                        "     , case when (select top 1 fecha " +
                        "                  from ms_historico " +
                        "                  where ms_historico.id_ms_instancia = ms_instancia.id_ms_instancia " +
                        "                    and ms_historico.id_actividad = 50 " +
                        "                  order by fecha) is null and cfdi is not null  " +
                        "         then ms_factura.fecha_solicita " +
                        "		 else (select top 1 fecha " +
                        "               from ms_historico " +
                        "               where ms_historico.id_ms_instancia = ms_instancia.id_ms_instancia " +
                        "                 and ms_historico.id_actividad = 50 " +
                        "               order by fecha) " +
                        "       end as fecha_ingreso_factura " +
                        "     , case when ms_factura.fecha_autoriza is null then null else convert(varchar, ms_factura.fecha_autoriza, 103) + replace(replace(replace(right(convert(varchar, ms_factura.fecha_autoriza, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') end as fecha_autoriza " +
                        "     , case when cfdi is null then null else case when id_usr_autoriza2 is null then 'No Aplica' else case when fecha_autoriza2 is null then null else convert(varchar, fecha_autoriza2, 103) + replace(replace(replace(right(convert(varchar, fecha_autoriza2, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') end end end as fecha_autoriza2 " +
                        "     , case when cfdi is null then null else case when id_usr_autoriza3 is null then 'No Aplica' else case when fecha_autoriza3 is null then null else convert(varchar, fecha_autoriza3, 103) + replace(replace(replace(right(convert(varchar, fecha_autoriza3, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') end end end as fecha_autoriza3 " +
                        "     , ms_factura.fecha_asigna " +
                        "     , ms_factura.fecha_regNAV " +
                        "     , case when ms_factura.contrato_NAV_alta = 'S' then 'Alta' else case when ms_factura.contrato_NAV_reg = 'S' then 'Registrado' else 'No Aplica' end end as contrato " +
                        "     , case when ms_factura.contrato_NAV_alta = 'S' then ms_contrato.no_contrato_NAV else case when ms_factura.contrato_NAV_reg = 'S' then ms_factura.no_contrato_NAV else null end end as no_contrato " +
                        "     , case when ms_factura.contrato_NAV_alta = 'S' then 'http://148.223.153.43/ProcAd - Adjuntos IngFact/'+ cast(dt_archivo.id_dt_archivo as varchar(20)) + '-' + dt_archivo.nombre else case when ms_factura.contrato_NAV_reg = 'S'  then 'http://148.223.153.43/ProcAd - Adjuntos IngFact/'+ cast(dt_archivo.id_dt_archivo as varchar(20)) + '-' + dt_archivo.nombre else null end end as Path " +
                        "     , case cg_actividad_inst.nombre_actividad " +
                        "         when 'Autorizar Factura' then 'Pendiente de Autorización' " +
                        "         when 'Corregir Factura' then 'Corregir Factura' " +
                        "         when 'Validar Solicitud' then 'En Validación Técnica' " +
                        "         when 'Solicitud Cancelada' then 'Rechazado' " +
                        "         when 'Ingresar Cotizaciones' then 'En Cotización' " +
                        "         when 'Corregir Solicitud' then 'Corregir Solicitud' " +
                        "         when 'Autorizar Solicitud' then 'Pendiente de Aut. Finanzas' " +
                        "         when 'Validar Presupuesto' then 'Validando Presupuesto' " +
                        "         when 'Solicitar Ampliación de Presupuesto' then 'Solicitando Ampliación de Presupuesto' " +
                        "         when 'Solicitud Cancelada por el Usuario' then 'Cancelado' " +
                        "         when 'Ingresar Factura' then 'Por Ingresar Factura' " +
                        "         when 'Autorizar Factura (2da)' then 'Pendiente de Autorización' " +
                        "         when 'Autorizar Factura (3ra)' then 'Pendiente de Autorización' " +
                        "         when 'Asignar Cuenta' then 'Asignando Cuenta' " +
                        "         when 'Ingreso de Factura Cancelado' then 'Cancelado' " +
                        "         when 'Cuenta Asignada' then " +
                        "           case when ms_factura.status = 'RN' then 'Registrado en NAV' " +
                        "             else 'Pendiente de Registro en NAV' " +
                        "           end " +
                        "         when 'Complementar Datos de Contrato' then 'Pendiente de Complementación' " +
                        "         when 'Autorizar Sol. de Contrato' then 'Pendiente de Autorización de Contrato' " +
                        "         when 'Solicitud de Contrato Rechazada' then 'Solicitud de Contrato Rechazada' " +
                        "         when 'Asignar Cuentas para Contrato' then 'Asignando Cuentas para Contrato' " +
                        "         when 'Registrar Contrato en NAV' then 'Registrando Contrato' " +
                        "         when 'Validar Aplicabilidad' then 'En Validación Técnica' " +
                        "         when 'Solicitud Rechazada' then 'Rechazado' " +
                        "         when 'Autorizar Servicio Negociado' then 'Pendiente de Aut. Finanzas' " +
                        "         when 'Solicitud de Servicio Negociado Cancelada' then 'Cancelado' " +
                        "         when 'Validar Presupuesto para Servicio Negociado' then 'Validando Presupuesto' " +
                        "         when 'Solicitar Ampliación de Presupuesto para Servicio Negociado' then 'Solicitando Ampliación de Presupuesto' " +
                        "         when 'Solicitud Cancelada por el Usuario' then 'Cancelado' " +
                        "         when 'Ingresar Factura de Servicio Negociado' then 'Por Ingresar Factura' " +
                        "         when 'Validar Soportes' then 'Validando Soportes' " +
                        "         when 'Factura de Servicio Negociado Rechazada' then 'Rechazado' " +
                        "         when 'Corregir Factura de Servicio Negociado' then 'Corregir Factura' " +
                        "         else " +
                        "           case " +
                        "             when ms_factura.status = 'RN' then 'Registrado en NAV' " +
                        "             else '-' " +
                        "           end " +
                        "       end as estatus " +
                        "from ms_factura " +
                        "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                        "  left join ms_instancia on ms_factura.id_ms_factura = ms_instancia.id_ms_sol and ms_instancia.tipo = 'F' " +
                        "  left join cg_actividad_inst on ms_instancia.id_actividad = cg_actividad_inst.id_actividad " +
                        "  left join cg_usuario cgUsrVal2 on ms_factura.id_usr_valida2 = cgUsrVal2.id_usuario " +
                        "  left join bd_Empleado.dbo.cg_empleado cgEmplVal2 on cgUsrVal2.id_empleado = cgEmplVal2.id_empleado " +
                        "  left join cg_usuario cgUsrA2 on ms_factura.id_usr_autoriza2 = cgUsrA2.id_usuario " +
                        "  left join bd_empleado.dbo.cg_empleado cgEmplA2 on cgUsrA2.id_empleado = cgEmplA2.id_empleado " +
                        "  left join cg_usuario cgUsrA3 on ms_factura.id_usr_autoriza3 = cgUsrA3.id_usuario " +
                        "  left join bd_empleado.dbo.cg_empleado cgEmplA3 on cgUsrA3.id_empleado = cgEmplA3.id_empleado " +
                        "  left join ms_contrato on ms_factura.id_ms_factura = ms_contrato.id_ms_factura " +
                        "  left join dt_archivo on dt_archivo.id_dt_archivo  = (select top 1 id_dt_archivo from dt_archivo where id_ms_factura =ms_factura.id_ms_factura and dt_archivo.tipo = 'A' order by fecha asc)  " +
                        "where 1 = 1 "

                If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "GerSopTec" Or _txtPerfil.Text = "JefCompras" Then
                    query = query + "  and (ms_factura.empleado = @autorizadorU or ms_factura.validador = @autorizadorU or cgEmplVal2.nombre + ' ' + cgEmplVal2.ap_paterno + ' ' + cgEmplVal2.ap_materno = @autorizadorU or ms_factura.autorizador = @autorizadorU or cgEmplA2.nombre + ' ' + cgEmplA2.ap_paterno + ' ' + cgEmplA2.ap_materno = @autorizadorU or cgEmplA3.nombre + ' ' + cgEmplA3.ap_paterno + ' ' + cgEmplA3.ap_materno = @autorizadorU) "
                End If

                If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "SopTec" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "Compras" Then
                    query = query + "  and ms_factura.id_usr_solicita = @id_usr_solicitaDef "
                End If
                If .cbSolicitante.Checked = True Then
                    query = query + "  and ms_factura.id_usr_solicita = @id_usr_solicita "
                End If
                If .cbEmpresa.Checked = True Then
                    query = query + "  and ms_factura.empresa = @empresa "
                End If
                If .cbFechaC.Checked = True Then
                    query = query + "  and (ms_factura.fecha_solicita between @fechaIni and @fechaFin) "
                End If
                If .cbAutorizador.Checked = True Then
                    query = query + "  and (ms_factura.autorizador = @autorizador or cgEmplA2.nombre + ' ' + cgEmplA2.ap_paterno + ' ' + cgEmplA2.ap_materno = @autorizador or cgEmplA3.nombre + ' ' + cgEmplA3.ap_paterno + ' ' + cgEmplA3.ap_materno = @autorizador) "
                End If
                If .cbProveedor.Checked = True Then
                    query = query + "  and dt_factura.razon_emisor like '%' + @proveedor + '%' "
                End If
                If .cbNoFact.Checked = True Then
                    query = query + "  and ms_factura.id_ms_factura = @id_ms_factura "
                End If
                If .cbStatus.Checked = True Then
                    query = query + "  and ms_factura.status in (" + .ddlStatus.SelectedValue + ") "
                End If
                If .cbContrato.Checked = True Then
                    query = query + "  and ms_contrato.no_contrato_NAV = @no_contrato "
                End If
                query = query + "order by ms_factura.id_ms_factura "

                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)

                If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "GerSopTec" Or _txtPerfil.Text = "JefCompras" Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@autorizadorU", ._txtEmpleado.Text)
                End If

                If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "SopTec" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "Compras" Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_usr_solicitaDef", Val(._txtIdUsuario.Text))
                End If
                If .cbSolicitante.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_usr_solicita", .ddlSolicitante.SelectedValue)
                End If
                If .cbEmpresa.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedValue)
                End If
                If .cbFechaC.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaIni", .wdpFechaI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaFin", .wdpFechaF.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
                End If
                If .cbAutorizador.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@autorizador", .ddlAutorizador.SelectedValue)
                End If
                If .cbProveedor.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@proveedor", .txtProveedor.Text.Trim)
                End If
                If .cbNoFact.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_ms_factura", .txtNoFact.Text.Trim)
                End If
                If .cbContrato.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@no_contrato", .txtContrato.Text.Trim)
                End If

                .gvRegistrosT.Visible = True
                .gvRegistros.DataSource = dsConsulta
                .gvRegistrosT.DataSource = dsConsulta
                ConexionBD.Open()
                sdaConsulta.Fill(dsConsulta)
                ConexionBD.Close()
                .gvRegistros.DataBind()
                .gvRegistrosT.DataBind()
                sdaConsulta.Dispose()
                dsConsulta.Dispose()
                .gvRegistrosT.Visible = False
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

                .lblFolio.Text = .gvRegistros.SelectedRow.Cells(1).Text

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaSol As New SqlDataAdapter
                Dim dsSol As New DataSet
                Dim query As String
                query = "select id_ms_factura " +
                        "     , empleado " +
                        "     , empresa " +
                        "     , especificaciones " +
                        "     , isnull(ms_factura.autorizador, '') as autorizador " +
                        "     , isnull(rfc_emisor, isnull((select rfc " +
                        "                                  from bd_SiSAC.dbo.cg_proveedor " +
                        "                                  where id_proveedor = (select proveedor_selec " +
                        "                                                        from ms_factura msFact " +
                        "                                                        where msFact.id_ms_factura = ms_factura.id_ms_factura)), '')) as rfc_emisor " +
                        "     , isnull(razon_emisor, isnull((select nombre " +
                        "                                    from bd_SiSAC.dbo.cg_proveedor " +
                        "                                    where id_proveedor = (select proveedor_selec " +
                        "                                                          from ms_factura msFact " +
                        "                                                          where msFact.id_ms_factura = ms_factura.id_ms_factura)), '')) as razon_emisor " +
                        "     , isnull(dt_factura.regimen_fiscal, '') as regimen_fiscal " +
                        "     , isnull(ms_factura.division, ms_factura.centro_costo) as centro_costo " +
                        "     , case when ms_factura.division is null then 'CC' else 'DIV' end as dimension " +
                        "     , isnull(isnull(ms_factura.tipo_servicio, ms_factura.servicio), '') as tipo_servicio " +
                        "     , isnull(ms_factura.validador, '') as validador " +
                        "     , isnull(cgEmplVal2.nombre + ' ' + cgEmplVal2.ap_paterno + ' ' + cgEmplVal2.ap_materno, '') as validador2 " +
                        "     , isnull(cgEmplA2.nombre + ' ' + cgEmplA2.ap_paterno + ' ' + cgEmplA2.ap_materno, '') as autorizador2 " +
                        "     , isnull(cgEmplA3.nombre + ' ' + cgEmplA3.ap_paterno + ' ' + cgEmplA3.ap_materno, '') as autorizador3 " +
                        "     , case when ms_factura.status in ('R', 'RN', 'Z', 'ZF', 'ZU', 'ZV') then 0 else 1 end as banStatus " +
                        "from ms_factura " +
                        "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                        "  left join cg_tipo_servicio on ms_factura.id_tipo_servicio = cg_tipo_servicio.id_tipo_servicio " +
                        "  left join cg_usuario cgUsrVal2 on ms_factura.id_usr_valida2 = cgUsrVal2.id_usuario " +
                        "  left join bd_Empleado.dbo.cg_empleado cgEmplVal2 on cgUsrVal2.id_empleado = cgEmplVal2.id_empleado " +
                        "  left join cg_usuario cgUsrA2 on ms_factura.id_usr_autoriza2 = cgUsrA2.id_usuario " +
                        "  left join bd_empleado.dbo.cg_empleado cgEmplA2 on cgUsrA2.id_empleado = cgEmplA2.id_empleado " +
                        "  left join cg_usuario cgUsrA3 on ms_factura.id_usr_autoriza3 = cgUsrA3.id_usuario " +
                        "  left join bd_empleado.dbo.cg_empleado cgEmplA3 on cgUsrA3.id_empleado = cgEmplA3.id_empleado " +
                        "where id_ms_factura = @id_ms_factura "
                sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaSol.SelectCommand.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaSol.Fill(dsSol)
                ConexionBD.Close()
                .lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("empleado").ToString()
                .lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                .lblRFC.Text = dsSol.Tables(0).Rows(0).Item("rfc_emisor").ToString()
                .lblValidador1.Text = dsSol.Tables(0).Rows(0).Item("validador").ToString()
                .lblValidador2.Text = dsSol.Tables(0).Rows(0).Item("validador2").ToString()
                .lblAutorizador.Text = dsSol.Tables(0).Rows(0).Item("autorizador").ToString()
                If dsSol.Tables(0).Rows(0).Item("dimension").ToString() = "CC" Then
                    .lbl_CC.Text = "Centro de Costo:"
                Else
                    .lbl_CC.Text = "División:"
                End If
                .lblCC.Text = dsSol.Tables(0).Rows(0).Item("centro_costo").ToString()
                .lblProveedor.Text = dsSol.Tables(0).Rows(0).Item("razon_emisor").ToString()
                .lblRegimenF.Text = dsSol.Tables(0).Rows(0).Item("regimen_fiscal").ToString()
                .lblServicio.Text = dsSol.Tables(0).Rows(0).Item("tipo_servicio").ToString()
                .lblAutorizador2.Text = dsSol.Tables(0).Rows(0).Item("autorizador2").ToString()
                .lblAutorizador3.Text = dsSol.Tables(0).Rows(0).Item("autorizador3").ToString()
                .txtEspecificacion.Text = dsSol.Tables(0).Rows(0).Item("especificaciones").ToString()
                If Val(dsSol.Tables(0).Rows(0).Item("banStatus").ToString()) = 1 And .lblSolicitante.Text = ._txtEmpleado.Text Then
                    .btnCancelar.Visible = True
                Else
                    .btnCancelar.Visible = False
                End If
                sdaSol.Dispose()
                dsSol.Dispose()

                If .lblServicio.Text = "No Aplica" Then
                    .lblServicio.Font.Italic = True
                Else
                    .lblServicio.Font.Italic = False
                End If
                If .lblServicio.Text = "" Then
                    .lbl_Servicio.Visible = False
                    .lblServicio.Visible = False
                Else
                    .lbl_Servicio.Visible = True
                    .lblServicio.Visible = True
                End If
                If .lblValidador1.Text = "" Then
                    .lbl_Validador1.Visible = False
                    .lblValidador1.Visible = False
                Else
                    .lbl_Validador1.Visible = True
                    .lblValidador1.Visible = True
                End If
                If .lblValidador2.Text = "" Then
                    .lbl_Validador2.Visible = False
                    .lblValidador2.Visible = False
                Else
                    .lbl_Validador2.Visible = True
                    .lblValidador2.Visible = True
                End If
                If .lblRFC.Text = "" Then
                    .lbl_RFC.Visible = False
                    .lblRFC.Visible = False
                Else
                    .lbl_RFC.Visible = True
                    .lblRFC.Visible = True
                End If
                If .lblProveedor.Text = "" Then
                    .lbl_Proveedor.Visible = False
                    .lblProveedor.Visible = False
                Else
                    .lbl_Proveedor.Visible = True
                    .lblProveedor.Visible = True
                End If
                If .lblAutorizador.Text = "" Then
                    .lbl_Autorizador.Visible = False
                    .lblAutorizador.Visible = False
                Else
                    .lbl_Autorizador.Visible = True
                    .lblAutorizador.Visible = True
                End If
                If .lblAutorizador2.Text = "" Then
                    .lbl_Autorizador2.Visible = False
                    .lblAutorizador2.Visible = False
                Else
                    .lbl_Autorizador2.Visible = True
                    .lblAutorizador2.Visible = True
                End If
                If .lblAutorizador3.Text = "" Then
                    .lbl_Autorizador3.Visible = False
                    .lblAutorizador3.Visible = False
                Else
                    .lbl_Autorizador3.Visible = True
                    .lblAutorizador3.Visible = True
                End If

                'Llenar Grid
                Dim sdaFactura As New SqlDataAdapter
                Dim dsFactura As New DataSet
                .gvFactura.DataSource = dsFactura
                sdaFactura.SelectCommand = New SqlCommand("select fecha_emision " +
                                                          "     , uuid " +
                                                          "     , 'http://148.223.153.43/Facturas/' + dt_factura.rfc_receptor + '/' + format(datepart(year, dt_factura.fecha_emision), '00') + '/' + upper(format(dt_factura.fecha_emision, 'MMMM', 'es-es')) + '/' + dt_factura.movimiento + '/' + dt_factura.tipo + '/' + dt_factura.uuid + '.pdf' as path " +
                                                          "     , 'PDF' as pdf " +
                                                          "     , serie " +
                                                          "     , folio " +
                                                          "     , lugar_exp " +
                                                          "     , forma_pago " +
                                                          "     , moneda " +
                                                          "     , subtotal " +
                                                          "     , importe " +
                                                          "from ms_factura " +
                                                          "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                                          "where id_ms_factura = @idMsFactura ", ConexionBD)
                sdaFactura.SelectCommand.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaFactura.Fill(dsFactura)
                .gvFactura.DataBind()
                ConexionBD.Close()
                sdaFactura.Dispose()
                dsFactura.Dispose()
                .gvFactura.SelectedIndex = -1
                If .gvFactura.Rows(0).Cells(0).Text.Trim <> "&nbsp;" Then
                    .gvFactura.Visible = True
                Else
                    .gvFactura.Visible = False
                End If

                'Adjuntos
                Dim sdaArchivos As New SqlDataAdapter
                Dim dsArchivos As New DataSet
                .gvAdjuntos.DataSource = dsArchivos
                sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " +
                                                           "     , 'http://148.223.153.43/ProcAd - Adjuntos IngFact/' + cast(id_dt_archivo as varchar(20)) + '-' + nombre as path " +
                                                           "from dt_archivo " +
                                                           "where id_ms_factura = @idMsFactura " +
                                                           "  and tipo = 'A' " +
                                                           "union " +
                                                           "select tabla_comp as archivo " +
                                                           "     , 'http://148.223.153.43/ProcAd - Adjuntos/' + cast(id_ms_factura as varchar(20)) + 'TabCon-' + tabla_comp as path " +
                                                           "from ms_factura " +
                                                           "where id_ms_factura = @idMsFactura " +
                                                           "  and tabla_comp is not null " +
                                                           "union " +
                                                           "select adjunto_opcional as archivo " +
                                                           "     , 'http://148.223.153.43/ProcAd - Adjuntos/' + cast(id_ms_factura as varchar(20)) + 'AdjOpc-' + adjunto_opcional as path " +
                                                           "from ms_factura " +
                                                           "where id_ms_factura = @idMsFactura " +
                                                           "  and adjunto_opcional is not null ", ConexionBD)
                sdaArchivos.SelectCommand.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaArchivos.Fill(dsArchivos)
                .gvAdjuntos.DataBind()
                ConexionBD.Close()
                sdaArchivos.Dispose()
                dsArchivos.Dispose()
                .gvAdjuntos.SelectedIndex = -1
                If .gvAdjuntos.Rows.Count > 0 Then
                    .lbl_Adjunto.Visible = True
                Else
                    .lbl_Adjunto.Visible = False
                End If

                'Evidencias
                Dim sdaEvidencias As New SqlDataAdapter
                Dim dsEvidencias As New DataSet
                .gvEvidencias.DataSource = dsEvidencias
                'Evidencias
                sdaEvidencias.SelectCommand = New SqlCommand("select nombre as archivo " +
                                                           "     , 'http://148.223.153.43/ProcAd - Adjuntos IngFact/' + cast(id_dt_archivo as varchar(20)) + '-' + nombre as path " +
                                                           "from dt_archivo " +
                                                           "where id_ms_factura = @idMsFactura " +
                                                           "  and tipo = 'E' ", ConexionBD)
                sdaEvidencias.SelectCommand.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaEvidencias.Fill(dsEvidencias)
                .gvEvidencias.DataBind()
                ConexionBD.Close()
                sdaEvidencias.Dispose()
                dsEvidencias.Dispose()
                .gvEvidencias.SelectedIndex = -1
                If .gvEvidencias.Rows.Count > 0 Then
                    .lbl_Evidencia.Visible = True
                Else
                    .lbl_Evidencia.Visible = False
                End If

                'Activos Fijos
                Dim sdaActivos As New SqlDataAdapter
                Dim dsActivos As New DataSet
                .gvAF.DataSource = dsActivos
                sdaActivos.SelectCommand = New SqlCommand("select codigo as no_economico " +
                                                          "     , descripcion " +
                                                          "from dt_af " +
                                                          "where id_ms_factura = @idMsFactura ", ConexionBD)
                sdaActivos.SelectCommand.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaActivos.Fill(dsActivos)
                .gvAF.DataBind()
                ConexionBD.Close()
                sdaActivos.Dispose()
                dsActivos.Dispose()
                If .gvAF.Rows.Count > 0 Then
                    .lbl_AF.Visible = True
                Else
                    .lbl_AF.Visible = False
                End If

                'Detalle Servicios Negociados
                Dim sdaDetSN As New SqlDataAdapter
                Dim dsDetSN As New DataSet
                .gvDtFacturaSN.DataSource = dsDetSN
                'Habilitar columna para actualización
                .gvDtFacturaSN.Columns(0).Visible = True
                'Cantidades Agregadas
                sdaDetSN.SelectCommand = New SqlCommand("select id_dt_factura_sn " +
                                                           "     , cantidad " +
                                                           "     , empresa " +
                                                           "     , no_economico " +
                                                           "     , tipo " +
                                                           "     , modelo " +
                                                           "     , placas " +
                                                           "     , div " +
                                                           "     , division " +
                                                           "     , zn " +
                                                           "from dt_factura_sn " +
                                                           "where id_ms_factura = @id_ms_factura ", ConexionBD)
                sdaDetSN.SelectCommand.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaDetSN.Fill(dsDetSN)
                .gvDtFacturaSN.DataBind()
                ConexionBD.Close()
                sdaDetSN.Dispose()
                dsDetSN.Dispose()
                .gvDtFacturaSN.SelectedIndex = -1
                'Inhabilitar columna para vista
                .gvDtFacturaSN.Columns(0).Visible = False
                If .gvDtFacturaSN.Rows.Count > 0 Then
                    .gvDtFacturaSN.Visible = True
                Else
                    .gvDtFacturaSN.Visible = False
                End If

                .pnlDetalle.Visible = True
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim idMsInst As Integer = 0
                'Obtener el ID de la instancia
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select top 1 id_ms_instancia " +
                                         "from ms_instancia " +
                                         "  left join cg_actividad_inst on ms_instancia.id_actividad = cg_actividad_inst.id_actividad " +
                                         "where id_ms_sol = @idMsFactura " +
                                         "  and tipo = 'F' "
                SCMValores.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))
                ConexionBD.Open()
                idMsInst = SCMValores.ExecuteScalar()
                ConexionBD.Close()

                'Cambiar Estatus de la Factura
                SCMValores.CommandText = "update ms_factura " +
                                         "  set status = 'ZU' " +
                                         "where id_ms_factura = @idMsFactura "
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                'Obtener el UUID, en caso de que ya se haya agregado la factura
                Dim uuid As String
                SCMValores.CommandText = "select isnull(CFDI, 'XX') as CFDI " +
                                         "from ms_factura " +
                                         "where id_ms_factura = @idMsFactura "
                ConexionBD.Open()
                uuid = SCMValores.ExecuteScalar()
                ConexionBD.Close()
                If uuid <> "XX" Then
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update dt_factura " +
                                             "set status = 'P' " +
                                             "where uuid = @cfdi " +
                                             "  and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') "
                    SCMValores.Parameters.AddWithValue("@cfdi", uuid)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()
                End If

                'Cambiar Estatus de la Instancia
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "update ms_instancia set id_actividad = 86 where id_ms_instancia = @idMsInst"
                SCMValores.Parameters.AddWithValue("@idMsInst", idMsInst)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                'Insertar en Histórico
                SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad,     fecha, id_usr) " +
                                         "                 values (       @idMsInst,           86, getdate(), @idUsr) "
                SCMValores.Parameters.AddWithValue("@idUsr", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                .btnCancelar.Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Nueva Búsqueda"

    Protected Sub btnNuevaBus_Click(sender As Object, e As EventArgs) Handles btnNuevaBus.Click
        Me.pnlFiltros.Visible = True
        Me.pnlRegistros.Visible = True
        Me.pnlDetalle.Visible = False
        Me.gvRegistros.SelectedIndex = -1
    End Sub

#End Region

#Region "Exportar"

    Protected Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        Dim tw As New System.IO.StringWriter
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        With Me
            Try
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment;filename=Registros.xls")
                Response.ContentType = "application/vnd.ms-excel"
                .gvRegistrosT.Visible = True
                .gvRegistrosT.RenderControl(hw)
                .gvRegistrosT.Visible = False
                Response.Write(tw.ToString())
                Response.End()
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub

#End Region

End Class