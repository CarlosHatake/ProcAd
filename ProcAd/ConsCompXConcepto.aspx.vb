Public Class ConsCompXConcepto
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
            .cbConcepto.Checked = False
            .pnlConcepto.Visible = False
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

    Protected Sub cbFechaC_CheckedChanged(sender As Object, e As EventArgs) Handles cbFechaR.CheckedChanged
        vista(Me.pnlFechaC, Me.cbFechaR.Checked)
    End Sub

    Protected Sub cbConcepto_CheckedChanged(sender As Object, e As EventArgs) Handles cbConcepto.CheckedChanged
        vista(Me.pnlConcepto, Me.cbConcepto.Checked)
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
                queryComp = "select no_empleado as [Id Empleado] " +
                            "     , empleado as Empleado " +
                            "     , case when (centro_costo is null) then division else centro_costo end as [CC / División] " +
                            "     , 'CON COMPROBANTE' as Tipo " +
                            "     , cg_concepto_comp.Concepto " +
                            "     , sum(monto_subtotal) as Subtotal " +
                            "     , sum(monto_total) as Total " +
                            "     , datename(month, fecha_realizo) as Mes " +
                            "     , datepart(year, fecha_realizo) as Año " +
                            "     , datepart(month, fecha_realizo) as [Mes Numero] " +
                            "     , ms_comp.id_ms_comp as Folio " +
                            " from ms_comp " +
                            "  left join dt_comp on ms_comp.id_ms_comp = dt_comp.id_ms_comp " +
                            "  left join cg_concepto_comp on dt_comp.id_concepto = cg_concepto_comp.id_concepto_comp " +
                            "where dt_comp.tipo = 'F' " +
                            "  and ms_comp.status in ('R') "

                'Consulta de Conceptos sin Factura
                queryValeA = "select no_empleado as [Id Empleado] " +
                             "     , empleado as Empleado " +
                             "     , case when (centro_costo is null) then division else centro_costo end as [CC / División] " +
                             "     , 'VALE AZUL' as Tipo " +
                             "     , cg_concepto.nombre_concepto AS Concepto " +
                             "     , sum(monto_subtotal) as Subtotal " +
                             "     , sum(monto_total) as Total " +
                             "     , datename(month, fecha_realizo) as Mes " +
                             "     , datepart(year, fecha_realizo) as Año " +
                             "     , datepart(month, fecha_realizo) as [Mes Numero] " +
                             "     ,  ms_comp.id_ms_comp as Folio " +
                             " from ms_comp " +
                             "  left join dt_comp on ms_comp.id_ms_comp = dt_comp.id_ms_comp " +
                             "  left join cg_concepto on dt_comp.id_concepto = cg_concepto.id_concepto " +
                             "where dt_comp.tipo = 'T' " +
                             "  and ms_comp.status in ('R') "

                If .cbFechaR.Checked = True Then
                    queryComp = queryComp + "  and (fecha_realizo BETWEEN @FI AND @FF) "
                    queryValeA = queryValeA + "  and (fecha_realizo BETWEEN @FI AND @FF) "
                End If
                If .cbConcepto.Checked = True Then
                    queryComp = queryComp + "  and cg_concepto_comp.concepto like '%' + @concepto + '%' "
                    queryValeA = queryValeA + "  and cg_concepto.nombre_concepto like '%' + @concepto + '%' "
                End If

                queryComp = queryComp + "group by ms_comp.id_ms_comp,no_empleado, empleado, centro_costo, division, cg_concepto_comp.Concepto, datepart(year, fecha_realizo), datepart(month, fecha_realizo), datename(month, fecha_realizo) "
                queryValeA = queryValeA + "group by  ms_comp.id_ms_comp, no_empleado, empleado, centro_costo, division, cg_concepto.nombre_concepto, datepart(year, fecha_realizo), datepart(month, fecha_realizo), datename(month, fecha_realizo) "

                Dim query As String
                query = queryComp +
                        "union all " +
                        queryValeA +
                        "order by empleado, Tipo, datepart(year, fecha_realizo), datepart(month, fecha_realizo), [CC / División] "

                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)

                If .cbFechaR.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@FI", .wdpFechaI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@FF", .wdpFechaF.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
                End If
                If .cbConcepto.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@concepto", .txtConcepto.Text.Trim)
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