Public Class BitacoraCargas
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
            .cbFechaC.Checked = False
            .pnlFechaC.Visible = False
            .wdpFechaI.Date = Date.Now.ToShortDateString
            .wdpFechaF.Date = Date.Now.ToShortDateString
            .cbUnidad.Checked = False
            .pnlUnidad.Visible = False
            .cbConductor.Checked = False
            .pnlConductor.Visible = False
            'Ocultar resto de paneles principales
            .pnlRegistros.Visible = False
        End With
    End Sub

    Public Sub vista(ByRef control, ByVal valor, ByRef up)
        With Me
            If valor Then
                control.Visible = True
            Else
                control.Visible = False
            End If
            up.Update()
        End With
    End Sub

#End Region

#Region "Filtros"

    Protected Sub cbFechaC_CheckedChanged(sender As Object, e As EventArgs) Handles cbFechaC.CheckedChanged
        vista(Me.pnlFechaC, Me.cbFechaC.Checked, Me.upFechaC)
    End Sub

    Protected Sub cbUnidad_CheckedChanged(sender As Object, e As EventArgs) Handles cbUnidad.CheckedChanged
        vista(Me.pnlUnidad, Me.cbUnidad.Checked, Me.upUnidad)
    End Sub

    Protected Sub cbConductor_CheckedChanged(sender As Object, e As EventArgs) Handles cbConductor.CheckedChanged
        vista(Me.pnlConductor, Me.cbConductor.Checked, Me.upConductor)
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
                Dim queryE As String
                Dim queryT As String
                Dim queryTK As String
                Dim queryG As String

                queryE = "select 'Edenred' as Tipo " +
                         "     , msVehi.no_eco as Unidad " +
                         "     , fecha as Fecha " +
                         "     , no_comprobante as Ticket " +
                         "     , razon_social_afiliado as Estación " +
                         "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as Conductor " +
                         "     , km_ant_transaccion as [Km Inicial] " +
                         "     , km_transaccion as [Km Final] " +
                         "     , recorrido as [Km Recorridos] " +
                         "     , cantidad_mercancia as [Litros Cargados] " +
                         "     , importe_transaccion as [Importe Total] " +
                         "     , dt_carga_comb.rendimiento as [Rendimiento Tabulado] " +
                         "     , rendimiento_real as [Rendimiento Real] " +
                         "     , tolerancia_unidad as [Tolerancia] " +
                         "     , desviacion_real as [Desviación Real] " +
                         "     , dt_carga_comb.foto_evid as archivo, 'http://148.223.153.43/ProcAd - Adjuntos GasC/' + cast(dt_carga_comb.id_dt_carga_comb as varchar(20)) + 'FotoHE-' + dt_carga_comb.foto_evid as path " +
                         "from dt_carga_comb " +
                         "  left join bd_Empleado.dbo.ms_vehiculo msVehi on dt_carga_comb.num_tarjeta = msVehi.no_tarjeta_edenred " +
                         "  left join bd_Empleado.dbo.cg_empleado cgEmpl on dt_carga_comb.id_conductor = cgEmpl.id_conductor_edenred and cgEmpl.status = 'A' " +
                         "where dt_carga_comb.status not in ('Z','ZA','ZD') "

                queryT = "select 'Tarjeta Bancaria' as Tipo " +
                         "     , identificador_vehiculo as Unidad " +
                         "     , fecha as Fecha " +
                         "     , no_comprobante as Ticket " +
                         "     , razon_social_afiliado as Estación " +
                         "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as Conductor " +
                         "     , km_ant_transaccion as [Km Inicial] " +
                         "     , km_transaccion as [Km Final] " +
                         "     , recorrido as [Km Recorridos] " +
                         "     , cantidad_mercancia as [Litros Cargados] " +
                         "     , importe_transaccion as [Importe Total] " +
                         "     , dt_carga_comb_tar.rendimiento as [Rendimiento Tabulado] " +
                         "     , rendimiento_real as [Rendimiento Real] " +
                         "     , tolerancia_unidad as [Tolerancia] " +
                         "     , desviacion_real as [Desviación Real] " +
                         "     , dt_carga_comb_tar.foto_evid as archivo, 'http://148.223.153.43/ProcAd - Adjuntos GasC/' + cast(dt_carga_comb_tar.id_dt_carga_comb_tar as varchar(20)) + 'FotoHTB-' + dt_carga_comb_tar.foto_evid as path " +
                         "from dt_carga_comb_tar " +
                         "  left join cg_usuario on dt_carga_comb_tar.id_usr_carga = cg_usuario.id_usuario " +
                         "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                         "where dt_carga_comb_tar.status not in ('Z','ZA','ZD') "

                queryTK = "select 'TOKA' as Tipo " +
                            "     , identificador_vehiculo as Unidad " +
                            "     , fecha as Fecha " +
                            "     , no_comprobante as Ticket " +
                            "     , razon_social_afiliado as Estación " +
                            "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as Conductor " +
                            "     , km_ant_transaccion as [Km Inicial] " +
                            "     , km_transaccion as [Km Final] " +
                            "     , recorrido as [Km Recorridos] " +
                            "     , cantidad_mercancia as [Litros Cargados] " +
                            "     , importe_transaccion as [Importe Total] " +
                            "     , dt_carga_comb_toka.rendimiento as [Rendimiento Tabulado] " +
                            "     , rendimiento_real as [Rendimiento Real] " +
                            "     , tolerancia_unidad as [Tolerancia] " +
                            "     , desviacion_real as [Desviación Real] " +
                            "     , dt_carga_comb_toka.foto_evid as archivo, 'http://148.223.153.43/ProcAd - Adjuntos GasC/' + cast(dt_carga_comb_toka.id_dt_carga_comb_toka as varchar(20)) + 'FotoHT-' + dt_carga_comb_toka.foto_evid as path " +
                            "from dt_carga_comb_toka " +
                            "  left join bd_Empleado.dbo.cg_empleado cgEmpl on dt_carga_comb_toka.id_conductor = cgEmpl.no_empleado " +
                            "where dt_carga_comb_toka.status not in ('Z','ZA','ZD') "

                If .cbFechaC.Checked = True Then
                    queryE = queryE + "  and (dt_carga_comb.fecha between @fechaIni and @fechaFin) "
                    queryT = queryT + "  and (dt_carga_comb_tar.fecha between @fechaIni and @fechaFin) "
                    queryTK = queryTK + "  and (dt_carga_comb_toka.fecha between @fechaIni and @fechaFin) "
                End If
                If .cbUnidad.Checked = True Then
                    queryE = queryE + "  and msVehi.no_eco like '%' + @unidad + '%' "
                    queryT = queryT + "  and identificador_vehiculo like '%' + @unidad + '%' "
                    queryTK = queryTK + "  and identificador_vehiculo like '%' + @unidad + '%' "
                End If
                If .cbConductor.Checked = True Then
                    queryE = queryE + "  and cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno like '%' + @conductor + '%' "
                    queryT = queryT + "  and cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno like '%' + @conductor + '%' "
                    queryTK = queryTK + "  and cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno like '%' + @conductor + '%' "
                End If
                queryG = queryE + "union " + queryT + "union " + queryTK + "order by Unidad, fecha desc "

                sdaConsulta.SelectCommand = New SqlCommand(queryG, ConexionBD)

                If .cbFechaC.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaIni", .wdpFechaI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaFin", .wdpFechaF.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
                End If
                If .cbUnidad.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@unidad", .txtUnidad.Text.Trim)
                End If
                If .cbConductor.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@conductor", .txtConductor.Text.Trim)
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