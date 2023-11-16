Public Class ConsCompXConceptoAud
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
            '.cbFechaC.Checked = False
            .pnlFechaC.Visible = True
            .wdpFechaI.Date = Date.Now.ToShortDateString
            .wdpFechaF.Date = Date.Now.ToShortDateString
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

    Protected Sub cbFechaC_CheckedChanged(sender As Object, e As EventArgs) Handles cbFechaV.CheckedChanged
        vista(Me.pnlFechaC, Me.cbFechaV.Checked)
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
                Dim queryComp As String
                Dim queryValeA As String

                'Consulta de Conceptos con Factura
                queryComp = "select no_empleado as [Id Empleado] " + _
                            "     , ms_comp.Empleado " + _
                            "     , ms_comp.Empresa " + _
                            "     , case when (centro_costo is null) then division else centro_costo end as [CC / División] " + _
                            "     , ms_comp.Autorizador " + _
                            "     , ms_comp.fecha_solicita as [Fecha de Solicitud] " + _
                            "     , ms_comp.fecha_autoriza as [Fecha de Autorización] " + _
                            "     , ms_comp.fecha_valida as [Fecha de Validación] " + _
                            "     , ms_comp.id_ms_comp as [No. Comp] " + _
                            "     , ms_comp.tipo_gasto as [Tipo Gasto] " + _
                            "     , ms_comp.tipo_actividad as [Tipo Actividad] " + _
                            "     , ms_comp.periodo_comp as [Periodo Comp.] " + _
                            "     , (select sum(monto_hospedaje) + sum(monto_alimentos) + sum(monto_casetas) + sum(monto_otros) as monto_ant " + _
                            "        from dt_anticipo " + _
                            "          left join ms_anticipo on dt_anticipo.id_ms_anticipo = ms_anticipo.id_ms_anticipo " + _
                            "        where dt_anticipo.id_ms_comp = ms_comp.id_ms_comp) as Anticipos " + _
                            "     , (select sum(dtCompT.monto_total) as monto_comp " + _
                            "        from dt_comp dtCompT " + _
                            "        where dtCompT.id_ms_comp = ms_comp.id_ms_comp) as Comprobado " + _
                            "     , ms_comp.importe_tot as Saldo " + _
                            "     , 'CON COMPROBANTE' as Tipo " + _
                            "     , cg_concepto_comp.Concepto " + _
                            "     , cg_concepto_comp.cuenta as [Cuenta Contable] " + _
                            "     , dt_comp.no_personas as [No. Personas] " + _
                            "     , dt_comp.no_dias as [No. Días] " + _
                            "     , cg_concepto_comp.IVA " + _
                            "     , monto_subtotal as Subtotal " + _
                            "     , monto_total as Total " + _
                            "     , dt_comp.Proveedor " + _
                            "     , dt_comp.origen_destino as [Origen - Destino] " + _
                            "     , dt_comp.obs as [Observaciones] " + _
                            "     , fecha_realizo as [Fecha Realizó] " + _
                            "     , datename(day, fecha_realizo) as Día " + _
                            "     , datename(month, fecha_realizo) as Mes " + _
                            "     , datepart(year, fecha_realizo) as Año " + _
                            "     , datepart(month, fecha_realizo) as [Mes Numero] " + _
                            "from ms_comp " + _
                            "  left join dt_comp on ms_comp.id_ms_comp = dt_comp.id_ms_comp " + _
                            "  left join cg_concepto_comp on dt_comp.id_concepto = cg_concepto_comp.id_concepto_comp " + _
                            "where dt_comp.tipo = 'F' " + _
                            "  and ms_comp.status in ('R') "

                'Consulta de Conceptos sin Factura
                queryValeA = "select no_empleado as [Id Empleado] " + _
                             "     , ms_comp.Empleado " + _
                             "     , ms_comp.Empresa " + _
                             "     , case when (centro_costo is null) then division else centro_costo end as [CC / División] " + _
                             "     , ms_comp.Autorizador " + _
                             "     , ms_comp.fecha_solicita as [Fecha de Solicitud] " + _
                             "     , ms_comp.fecha_autoriza as [Fecha de Autorización] " + _
                             "     , ms_comp.fecha_valida as [Fecha de Validación] " + _
                             "     , ms_comp.id_ms_comp as [No. Comp] " + _
                             "     , ms_comp.tipo_gasto as [Tipo Gasto] " + _
                             "     , ms_comp.tipo_actividad as [Tipo Actividad] " + _
                             "     , ms_comp.periodo_comp as [Periodo Comp.] " + _
                             "     , (select sum(monto_hospedaje) + sum(monto_alimentos) + sum(monto_casetas) + sum(monto_otros) as monto_ant " + _
                             "        from dt_anticipo " + _
                             "          left join ms_anticipo on dt_anticipo.id_ms_anticipo = ms_anticipo.id_ms_anticipo " + _
                             "        where dt_anticipo.id_ms_comp = ms_comp.id_ms_comp) as Anticipos " + _
                             "     , (select sum(dtCompT.monto_total) as monto_comp " + _
                             "        from dt_comp dtCompT " + _
                             "        where dtCompT.id_ms_comp = ms_comp.id_ms_comp) as Comprobado " + _
                             "     , ms_comp.importe_tot as Saldo " + _
                             "     , 'VALE AZUL' as Tipo " + _
                             "     , cg_concepto.nombre_concepto AS Concepto " + _
                             "     , cg_concepto.cuenta as [Cuenta Contable] " + _
                             "     , dt_comp.no_personas as [No. Personas] " + _
                             "     , dt_comp.no_dias as [No. Días] " + _
                             "     , NULL as IVA " + _
                             "     , monto_subtotal as Subtotal " + _
                             "     , monto_total as Total " + _
                             "     , dt_comp.Proveedor " + _
                             "     , dt_comp.origen_destino as [Origen - Destino] " + _
                             "     , dt_comp.obs as [Observaciones] " + _
                             "     , fecha_realizo as [Fecha Realizó] " + _
                             "     , datename(day, fecha_realizo) as Día " + _
                             "     , datename(month, fecha_realizo) as Mes " + _
                             "     , datepart(year, fecha_realizo) as Año " + _
                             "     , datepart(month, fecha_realizo) as [Mes Numero] " + _
                             "from ms_comp " + _
                             "  left join dt_comp on ms_comp.id_ms_comp = dt_comp.id_ms_comp " + _
                             "  left join cg_concepto on dt_comp.id_concepto = cg_concepto.id_concepto " + _
                             "where dt_comp.tipo = 'T' " + _
                             "  and ms_comp.status in ('R') "

                If .cbFechaV.Checked = True Then
                    queryComp = queryComp + "  and (ms_comp.fecha_valida BETWEEN @FI AND @FF) "
                    queryValeA = queryValeA + "  and (ms_comp.fecha_valida BETWEEN @FI AND @FF) "
                End If

                Dim query As String
                query = queryComp + _
                        "union all " + _
                        queryValeA + _
                        "order by empleado, Tipo, datepart(year, fecha_realizo), datepart(month, fecha_realizo), Empresa, [CC / División] "

                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)

                If .cbFechaV.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@FI", .wdpFechaI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@FF", .wdpFechaF.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
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