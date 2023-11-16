Public Class ConsCompSV
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
                                "from ms_comp " +
                                "order by solicito "
                        sdaSolicitante.SelectCommand = New SqlCommand(query, ConexionBD)
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
                                "from ms_comp " +
                                "order by empresa "
                        sdaEmpresa.SelectCommand = New SqlCommand(query, ConexionBD)
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
                                "from ms_comp " +
                                "order by autorizador "
                        sdaAutorizador.SelectCommand = New SqlCommand(query, ConexionBD)
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
            .cbValeA.Checked = False
            'Fechas
            .cbFechaC.Checked = False
            .pnlFechaC.Visible = False
            .wdpFechaI.Date = Date.Now.ToShortDateString
            .wdpFechaF.Date = Date.Now.ToShortDateString
            .cbAutorizador.Checked = False
            .pnlAutorizador.Visible = False
            .cbNoComp.Checked = False
            .pnlNoComp.Visible = False
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

    Protected Sub cbAutorizador_CheckedChanged(sender As Object, e As EventArgs) Handles cbAutorizador.CheckedChanged
        vista(Me.pnlAutorizador, Me.cbAutorizador.Checked)
    End Sub

    Protected Sub cbNoSol_CheckedChanged(sender As Object, e As EventArgs) Handles cbNoComp.CheckedChanged
        vista(Me.pnlNoComp, Me.cbNoComp.Checked)
        If Me.cbNoComp.Checked = True Then
            Me.txtNoComp.Text = ""
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
                Dim queryF As String
                Dim queryT As String

                queryF = "select ms_comp.no_empleado as [Id Empleado] " +
                         "     , ms_comp.Empleado " +
                         "     , ms_comp.Empresa " +
                         "     , case when (centro_costo is null) then division else centro_costo end as [CC / División] " +
                         "     , ms_comp.Autorizador " +
                         "     , ms_comp.fecha_solicita as [Fecha de Solicitud] " +
                         "     , ms_comp.fecha_autoriza as [Fecha de Autorización] " +
                         "     , ms_comp.fecha_valida as [Fecha de Validación] " +
                         "     , ms_comp.id_ms_comp as [No. Comp] " +
                         "     , ms_comp.tipo_gasto as [Tipo Gasto] " +
                         "     , ms_comp.tipo_actividad as [Tipo Actividad] " +
                         "     , ms_comp.periodo_comp as [Periodo Comp.] " +
                         "     , (select sum(monto_hospedaje) + sum(monto_alimentos) + sum(monto_casetas) + sum(monto_otros) as monto_ant " +
                         "        from dt_anticipo " +
                         "          left join ms_anticipo on dt_anticipo.id_ms_anticipo = ms_anticipo.id_ms_anticipo " +
                         "        where dt_anticipo.id_ms_comp = ms_comp.id_ms_comp) as Anticipos " +
                         "     , (select sum(dtCompT.monto_total) as monto_comp " +
                         "        from dt_comp dtCompT " +
                         "        where dtCompT.id_ms_comp = ms_comp.id_ms_comp) as Comprobado " +
                         "     , ms_comp.importe_tot as Saldo " +
                         "     , 'CON COMPROBANTE' as Tipo " +
                         "     , cg_concepto_cat.categoria as [Categoría Concepto] " +
                         "     , cg_concepto_comp.Concepto " +
                         "     , cg_concepto_comp.cuenta as [Cuenta Contable] " +
                         "     , dt_comp.no_personas as [No. Personas] " +
                         "     , dt_comp.no_dias as [No. Días] " +
                         "     , cg_concepto_comp.IVA " +
                         "     , monto_subtotal as Subtotal " +
                         "     , monto_total as Total " +
                         "     , dt_factura.lugar_exp  as [C.P.] " +
                         "     , dt_comp.Proveedor " +
                         "     , dt_comp.lugar_orig as Origen " +
                         "     , dt_comp.lugar_dest as Destino " +
                         "     , dt_comp.origen_destino as [Lugar] " +
                         "     , dt_comp.obs as [Observaciones] " +
                         "     , fecha_realizo as [Fecha Realizó] " +
                         "from ms_comp " +
                         "  left join dt_comp on ms_comp.id_ms_comp = dt_comp.id_ms_comp " +
                         "  left join cg_concepto_comp on dt_comp.id_concepto = cg_concepto_comp.id_concepto_comp " +
                         "  left join cg_concepto_cat on cg_concepto_comp.id_concepto_cat = cg_concepto_cat.id_concepto_cat " +
                         "  left join dt_factura on dt_factura.uuid = dt_comp.CFDI  " +
                         " where dt_comp.tipo = 'F' " +
                         "  and ms_comp.status in ('R') "

                queryT = "select no_empleado as [Id Empleado] " +
                         "     , ms_comp.Empleado " +
                         "     , ms_comp.Empresa " +
                         "     , case when (centro_costo is null) then division else centro_costo end as [CC / División] " +
                         "     , ms_comp.Autorizador " +
                         "     , ms_comp.fecha_solicita as [Fecha de Solicitud] " +
                         "     , ms_comp.fecha_autoriza as [Fecha de Autorización] " +
                         "     , ms_comp.fecha_valida as [Fecha de Validación] " +
                         "     , ms_comp.id_ms_comp as [No. Comp] " +
                         "     , ms_comp.tipo_gasto as [Tipo Gasto] " +
                         "     , ms_comp.tipo_actividad as [Tipo Actividad] " +
                         "     , ms_comp.periodo_comp as [Periodo Comp.] " +
                         "     , (select sum(monto_hospedaje) + sum(monto_alimentos) + sum(monto_casetas) + sum(monto_otros) as monto_ant " +
                         "        from dt_anticipo " +
                         "          left join ms_anticipo on dt_anticipo.id_ms_anticipo = ms_anticipo.id_ms_anticipo " +
                         "        where dt_anticipo.id_ms_comp = ms_comp.id_ms_comp) as Anticipos " +
                         "     , (select sum(dtCompT.monto_total) as monto_comp " +
                         "        from dt_comp dtCompT " +
                         "        where dtCompT.id_ms_comp = ms_comp.id_ms_comp) as Comprobado " +
                         "     , ms_comp.importe_tot as Saldo " +
                         "     , 'VALE AZUL' as Tipo " +
                         "     , cg_concepto_cat.categoria as [Categoría Concepto] " +
                         "     , cg_concepto.nombre_concepto AS Concepto " +
                         "     , cg_concepto.cuenta as [Cuenta Contable] " +
                         "     , dt_comp.no_personas as [No. Personas] " +
                         "     , dt_comp.no_dias as [No. Días] " +
                         "     , NULL as IVA " +
                         "     , monto_subtotal as Subtotal " +
                         "     , monto_total as Total " +
                         "     , 'NA'  as [C.P.]  " +
                         "     , dt_comp.Proveedor " +
                         "      , dt_comp.lugar_orig as Origen " +
                         "      , dt_comp.lugar_dest as Destino " +
                         "      , dt_comp.origen_destino as [Lugar] " +
                         "      , dt_comp.obs as [Observaciones] " +
                         "      , fecha_realizo as [Fecha Realizó] " +
                         "from ms_comp " +
                         "  left join dt_comp on ms_comp.id_ms_comp = dt_comp.id_ms_comp " +
                         "  left join cg_concepto on dt_comp.id_concepto = cg_concepto.id_concepto " +
                         "  left join cg_concepto_cat on cg_concepto.id_concepto_cat = cg_concepto_cat.id_concepto_cat " +
                         "where dt_comp.tipo = 'T' " +
                         "  and ms_comp.status in ('R') "

                If .cbEmpresa.Checked = True Then
                    queryF = queryF + "  and ms_comp.Empresa = @empresa "
                    queryT = queryT + "  and ms_comp.Empresa = @empresa "
                End If
                If .cbFechaC.Checked = True Then
                    queryF = queryF + "  and (ms_comp.fecha_solicita between @fechaIni and @fechaFin) "
                    queryT = queryT + "  and (ms_comp.fecha_solicita between @fechaIni and @fechaFin) "
                End If
                If .cbAutorizador.Checked = True Then
                    queryF = queryF + "  and ms_comp.Autorizador = @autorizador "
                    queryT = queryT + "  and ms_comp.Autorizador = @autorizador "
                End If
                If .cbNoComp.Checked = True Then
                    queryF = queryF + "  and ms_comp.id_ms_comp = @id_ms_comp "
                    queryT = queryT + "  and ms_comp.id_ms_comp = @id_ms_comp "
                End If
                If .cbValeA.Checked = True Then
                    queryF = queryF + "  and dt_comp.tipo = 'T' "
                    queryT = queryT + "  and dt_comp.tipo = 'T' "
                End If




                sdaConsulta.SelectCommand = New SqlCommand(queryF +
                                                           "union all " +
                                                            queryT +
                                                           "order by empleado, Tipo, fecha_realizo, Empresa, [CC / División] ", ConexionBD)

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
                If .cbNoComp.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_ms_comp", .txtNoComp.Text.Trim)
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
                .gvRegistros.Visible = True
                .gvRegistros.RenderControl(hw)
                .gvRegistros.Visible = False
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