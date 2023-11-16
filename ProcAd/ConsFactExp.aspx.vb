Public Class ConsFactExp
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
            .cbProveedor.Checked = False
            .pnlProveedor.Visible = False
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

    Protected Sub cbFechaC_CheckedChanged(sender As Object, e As EventArgs) Handles cbFechaC.CheckedChanged
        vista(Me.pnlFechaC, Me.cbFechaC.Checked)
    End Sub

    Protected Sub cbProveedor_CheckedChanged(sender As Object, e As EventArgs) Handles cbProveedor.CheckedChanged
        vista(Me.pnlProveedor, Me.cbProveedor.Checked)
        If Me.cbProveedor.Checked = True Then
            Me.txtProveedor.Text = ""
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

                query = "select ms_factura.id_ms_factura as Folio " + _
                        "     , isnull(ms_factura.tipo_servicio, ms_factura.servicio) as Servicio " + _
                        "     , ms_factura.empleado as Solicita " + _
                        "     , ms_factura.autorizador as Autorizador " + _
                        "     , cgEmplA2.nombre + ' ' + cgEmplA2.ap_paterno + ' ' + cgEmplA2.ap_materno as [2do Autorizador] " + _
                        "     , cgEmplA3.nombre + ' ' + cgEmplA3.ap_paterno + ' ' + cgEmplA3.ap_materno as [3er Autorizador] " + _
                        "     , dt_factura.razon_emisor as [Proveedor] " + _
                        "     , dt_factura.serie + dt_factura.folio as [No. Factura] " + _
                        "     , CFDI " + _
                        "     , importe_tot as [Importe Total] " + _
                        "     , case when (select top 1 fecha " + _
                        "                  from ms_historico " + _
                        "                  where ms_historico.id_ms_instancia = ms_instancia.id_ms_instancia " + _
                        "                    and ms_historico.id_actividad = 50 " + _
                        "                  order by fecha) is null and cfdi is not null " + _
                        "         then ms_factura.fecha_solicita " + _
                        "		 else (select top 1 fecha " + _
                        "               from ms_historico " + _
                        "               where ms_historico.id_ms_instancia = ms_instancia.id_ms_instancia " + _
                        "                 and ms_historico.id_actividad = 50 " + _
                        "               order by fecha) " + _
                        "       end as [Fecha Ing. Factura] " + _
                        "     , case when ms_factura.fecha_autoriza is null then null else convert(varchar, ms_factura.fecha_autoriza, 103) + replace(replace(replace(right(convert(varchar, ms_factura.fecha_autoriza, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') end as [Fecha Autorización] " + _
                        "     , case when cfdi is null then null else case when id_usr_autoriza2 is null then 'No Aplica' else case when fecha_autoriza2 is null then null else convert(varchar, fecha_autoriza2, 103) + replace(replace(replace(right(convert(varchar, fecha_autoriza2, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') end end end as [Fecha 2da Autorización] " + _
                        "     , case when cfdi is null then null else case when id_usr_autoriza3 is null then 'No Aplica' else case when fecha_autoriza3 is null then null else convert(varchar, fecha_autoriza3, 103) + replace(replace(replace(right(convert(varchar, fecha_autoriza3, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') end end end as [Fecha 3ra Autorización] " + _
                        "     , ms_factura.fecha_asigna as [Fecha Asig. Cuenta] " + _
                        "     , ms_factura.fecha_regNAV as [Fecha Reg. NAV] " + _
                        "     , dt_factura.fecha_emision " + _
                        "     , cast(dateadd(dd, 30, dt_factura.fecha_emision) as date) as fecha_vence " + _
                        "     , case cg_actividad_inst.nombre_actividad " + _
                        "         when 'Autorizar Factura' then 'Pendiente de Autorización' " + _
                        "         when 'Corregir Factura' then 'Corregir Factura' " + _
                        "         when 'Validar Solicitud' then 'En Validación Técnica' " + _
                        "         when 'Solicitud Cancelada' then 'Rechazado' " + _
                        "         when 'Ingresar Cotizaciones' then 'En Cotización' " + _
                        "         when 'Corregir Solicitud' then 'Corregir Solicitud' " + _
                        "         when 'Autorizar Solicitud' then 'Pendiente de Aut. Finanzas' " + _
                        "         when 'Validar Presupuesto' then 'Validando Presupuesto' " + _
                        "         when 'Solicitar Ampliación de Presupuesto' then 'Solicitando Ampliación de Presupuesto' " + _
                        "         when 'Solicitud Cancelada por el Usuario' then 'Cancelado' " + _
                        "         when 'Ingresar Factura' then 'Por Ingresar Factura' " + _
                        "         when 'Autorizar Factura (2da)' then 'Pendiente de Autorización' " + _
                        "         when 'Autorizar Factura (3ra)' then 'Pendiente de Autorización' " + _
                        "         when 'Asignar Cuenta' then 'Asignando Cuenta' " + _
                        "         when 'Ingreso de Factura Cancelado' then 'Cancelado' " + _
                        "         when 'Cuenta Asignada' then " + _
                        "           case when ms_factura.status = 'RN' then 'Registrado en NAV' " + _
                        "             else 'Pendiente de Registro en NAV' " + _
                        "           end " + _
                        "         when 'Complementar Datos de Contrato' then 'Pendiente de Complementación' " + _
                        "         when 'Autorizar Sol. de Contrato' then 'Pendiente de Autorización de Contrato' " + _
                        "         when 'Solicitud de Contrato Rechazada' then 'Solicitud de Contrato Rechazada' " + _
                        "         when 'Asignar Cuentas para Contrato' then 'Asignando Cuentas para Contrato' " + _
                        "         when 'Registrar Contrato en NAV' then 'Registrando Contrato' " + _
                        "         when 'Validar Aplicabilidad' then 'En Validación Técnica' " + _
                        "         when 'Solicitud Rechazada' then 'Rechazado' " + _
                        "         when 'Autorizar Servicio Negociado' then 'Pendiente de Aut. Finanzas' " + _
                        "         when 'Solicitud de Servicio Negociado Cancelada' then 'Cancelado' " + _
                        "         when 'Validar Presupuesto para Servicio Negociado' then 'Validando Presupuesto' " + _
                        "         when 'Solicitar Ampliación de Presupuesto para Servicio Negociado' then 'Solicitando Ampliación de Presupuesto' " + _
                        "         when 'Solicitud Cancelada por el Usuario' then 'Cancelado' " + _
                        "         when 'Ingresar Factura de Servicio Negociado' then 'Por Ingresar Factura' " + _
                        "         when 'Validar Soportes' then 'Validando Soportes' " + _
                        "         when 'Factura de Servicio Negociado Rechazada' then 'Rechazado' " + _
                        "         when 'Corregir Factura de Servicio Negociado' then 'Corregir Factura' " + _
                        "         else " + _
                        "           case " + _
                        "             when ms_factura.status = 'RN' then 'Registrado en NAV' " + _
                        "             else '-' " + _
                        "           end " + _
                        "       end as [Estado] " + _
                        "from ms_factura " + _
                        "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " + _
                        "  left join ms_instancia on ms_factura.id_ms_factura = ms_instancia.id_ms_sol and ms_instancia.tipo = 'F' " + _
                        "  left join cg_actividad_inst on ms_instancia.id_actividad = cg_actividad_inst.id_actividad " + _
                        "  left join cg_usuario cgUsrVal2 on ms_factura.id_usr_valida2 = cgUsrVal2.id_usuario " + _
                        "  left join bd_Empleado.dbo.cg_empleado cgEmplVal2 on cgUsrVal2.id_empleado = cgEmplVal2.id_empleado " + _
                        "  left join cg_usuario cgUsrA2 on ms_factura.id_usr_autoriza2 = cgUsrA2.id_usuario " + _
                        "  left join bd_empleado.dbo.cg_empleado cgEmplA2 on cgUsrA2.id_empleado = cgEmplA2.id_empleado " + _
                        "  left join cg_usuario cgUsrA3 on ms_factura.id_usr_autoriza3 = cgUsrA3.id_usuario " + _
                        "  left join bd_empleado.dbo.cg_empleado cgEmplA3 on cgUsrA3.id_empleado = cgEmplA3.id_empleado " + _
                        "where ms_factura.empresa = 'EXPROMAT' " + _
                        "  and dt_factura.rfc_emisor in (select split.a.value('.', 'NVARCHAR(MAX)') data " + _
                        "                                from (select cast('<X>' + replace((select valor from cg_parametros where parametro = 'rfc_prov_expromat'), ',', '</X><X>') + '</X>' as xml) as string) as A " + _
                        "                                cross apply string.nodes('/X') as split(a)) "

                If .cbFechaC.Checked = True Then
                    query = query + "  and (ms_factura.fecha_solicita between @fechaIni and @fechaFin) "
                End If
                If .cbProveedor.Checked = True Then
                    query = query + "  and dt_factura.razon_emisor like '%' + @proveedor + '%' "
                End If
                query = query + "order by ms_factura.id_ms_factura "

                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)

                If .cbFechaC.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaIni", .wdpFechaI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaFin", .wdpFechaF.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
                End If
                If .cbProveedor.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@proveedor", .txtProveedor.Text.Trim)
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

#Region "Exportar"

    Protected Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        Dim tw As New System.IO.StringWriter
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        With Me
            Try
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment;filename=Registros.xls")
                Response.ContentType = "application/vnd.ms-excel"
                .gvRegistros.RenderControl(hw)
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