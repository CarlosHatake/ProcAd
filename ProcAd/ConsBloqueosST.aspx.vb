Public Class ConsBloqueosST
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
                        Dim query As String
                        'Solicitantes
                        Dim sdaSolicitante As New SqlDataAdapter
                        Dim dsSolicitante As New DataSet
                        query = "select distinct(cgEmpl.id_empleado) as id_empleado " + _
                                "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as empleado " + _
                                "from dt_carga_comb " + _
                                "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on dt_carga_comb.id_conductor = cgEmpl.id_conductor_edenred and cgEmpl.status = 'A' " + _
                                "  inner join cg_usuario on cgEmpl.id_empleado = cg_usuario.id_empleado and cg_usuario.status = 'A' " + _
                                "where dt_carga_comb.bloqueo_st = 'S' " + _
                                "union " + _
                                "select distinct(cgEmpl.id_empleado) as id_empleado " + _
                                "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as empleado " + _
                                "from dt_carga_comb_tar " + _
                                "  inner join cg_usuario on dt_carga_comb_tar.id_usr_carga = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                                "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                "where dt_carga_comb_tar.bloqueo_st = 'S' " + _
                                "union " + _
                                "select distinct(cgEmpl.id_empleado) as id_empleado " + _
                                "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as empleado " + _
                                "from dt_carga_comb_toka " + _
                                "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on dt_carga_comb_toka.id_conductor = cgEmpl.no_empleado " + _
                                "where dt_carga_comb_toka.bloqueo_st = 'S' " + _
                                "order by empleado "
                        sdaSolicitante.SelectCommand = New SqlCommand(query, ConexionBD)
                        .ddlSolicitante.DataSource = dsSolicitante
                        .ddlSolicitante.DataTextField = "empleado"
                        .ddlSolicitante.DataValueField = "id_empleado"
                        ConexionBD.Open()
                        sdaSolicitante.Fill(dsSolicitante)
                        .ddlSolicitante.DataBind()
                        ConexionBD.Close()
                        sdaSolicitante.Dispose()
                        dsSolicitante.Dispose()
                        .ddlSolicitante.SelectedIndex = -1

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
            .cbSolicitante.Checked = False
            .pnlSolicitante.Visible = False
            'Fechas
            .cbFechaC.Checked = False
            .pnlFechaC.Visible = False
            .wdpFechaI.Date = Date.Now.ToShortDateString
            .wdpFechaF.Date = Date.Now.ToShortDateString
            .cbNoEco.Checked = False
            .pnlNoEco.Visible = False
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

    Protected Sub cbFechaC_CheckedChanged(sender As Object, e As EventArgs) Handles cbFechaC.CheckedChanged
        vista(Me.pnlFechaC, Me.cbFechaC.Checked)
    End Sub

    Protected Sub cbNoEco_CheckedChanged(sender As Object, e As EventArgs) Handles cbNoEco.CheckedChanged
        vista(Me.pnlNoEco, Me.cbNoEco.Checked)
        If Me.cbNoEco.Checked = True Then
            Me.txtNoEco.Text = ""
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
                Dim queryE As String
                Dim queryTB As String
                Dim queryTK As String

                queryE = "select 'Edenred' as Tipo " + _
                         "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as Conductor " + _
                         "     , dt_carga_comb.identificador_vehiculo as Unidad " + _
                         "     , dt_carga_comb.placa as Placa " + _
                         "     , dt_carga_comb.num_tarjeta as Tarjeta " + _
                         "     , dt_carga_comb.razon_social_afiliado as [Estación de Carga] " + _
                         "     , fecha as Fecha " + _
                         "     , dt_carga_comb.no_comprobante as [No. Ticket] " + _
                         "     , dt_carga_comb.cantidad_mercancia as Litros " + _
                         "     , dt_carga_comb.importe_transaccion as Importe " + _
                         "     , isnull(km_ant_transaccion, -9999) as [Odómetro Anterior] " + _
                         "     , isnull(km_transaccion, -9999) as [Odómetro Actual] " + _
                         "     , isnull(recorrido, -999) as [Kms Recorridos] " + _
                         "     , tolerancia_unidad as [Tolerancia Unidad] " + _
                         "     , desviacion_real as [Desviación Real] " + _
                         "     , fecha_st as [Fecha ST] " + _
                         "     , cargo_conductor as [Se generó Cargo] " + _
                         "     , cast(dt_carga_comb.just_st as varchar(500)) as [Observaciones ST] " + _
                         "     , isnull(recorrido, -999) / dt_carga_comb.cantidad_mercancia as [Rend. Km x Litro] " + _
                         "from dt_carga_comb " + _
                         "  left join bd_Empleado.dbo.cg_empleado cgEmpl on dt_carga_comb.id_conductor = cgEmpl.id_conductor_edenred and cgEmpl.status = 'A' " + _
                         "  left join cg_usuario on cgEmpl.id_empleado = cg_usuario.id_empleado and cg_usuario.status = 'A' " + _
                         "where dt_carga_comb.bloqueo_st = 'S' "

                queryTB = "select 'Tarjeta Bancaria' as Tipo " + _
                          "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as Conductor " + _
                          "     , dt_carga_comb_tar.identificador_vehiculo as Unidad " + _
                          "     , dt_carga_comb_tar.placa as Placa " + _
                          "     , dt_carga_comb_tar.num_tarjeta as Tarjeta " + _
                          "     , dt_carga_comb_tar.razon_social_afiliado as [Estación de Carga] " + _
                          "     , fecha as Fecha " + _
                          "     , dt_carga_comb_tar.no_comprobante as [No. Ticket] " + _
                          "     , dt_carga_comb_tar.cantidad_mercancia as Litros " + _
                          "     , dt_carga_comb_tar.importe_transaccion as Importe " + _
                          "     , isnull(km_ant_transaccion, -9999) as [Odómetro Anterior] " + _
                          "     , isnull(km_transaccion, -9999) as [Odómetro Actual] " + _
                          "     , isnull(recorrido, -999) as [Kms Recorridos] " + _
                          "     , tolerancia_unidad as [Tolerancia Unidad] " + _
                          "     , desviacion_real as [Desviación Real] " + _
                          "     , fecha_st as [Fecha ST] " + _
                          "     , cargo_conductor as [Se generó Cargo] " + _
                          "     , cast(dt_carga_comb_tar.just_st as varchar(500)) as [Observaciones ST] " + _
                          "     , isnull(recorrido, -999) / dt_carga_comb_tar.cantidad_mercancia as [Rend. Km x Litro] " + _
                          "from dt_carga_comb_tar " + _
                          "  left join cg_usuario on dt_carga_comb_tar.id_usr_carga = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                          "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                          "where dt_carga_comb_tar.bloqueo_st = 'S' "

                queryTK = "select 'TOKA' as Tipo " + _
                          "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as Conductor " + _
                          "     , dt_carga_comb_toka.identificador_vehiculo as Unidad " + _
                          "     , dt_carga_comb_toka.placa as Placa " + _
                          "     , dt_carga_comb_toka.num_tarjeta as Tarjeta " + _
                          "     , dt_carga_comb_toka.razon_social_afiliado as [Estación de Carga] " + _
                          "     , fecha as Fecha " + _
                          "     , dt_carga_comb_toka.no_comprobante as [No. Ticket] " + _
                          "     , dt_carga_comb_toka.cantidad_mercancia as Litros " + _
                          "     , dt_carga_comb_toka.importe_transaccion as Importe " + _
                          "     , isnull(km_ant_transaccion, -9999) as [Odómetro Anterior] " + _
                          "     , isnull(km_transaccion, -9999) as [Odómetro Actual] " + _
                          "     , isnull(recorrido, -999) as [Kms Recorridos] " + _
                          "     , tolerancia_unidad as [Tolerancia Unidad] " + _
                          "     , desviacion_real as [Desviación Real] " + _
                          "     , fecha_st as [Fecha ST] " + _
                          "     , cargo_conductor as [Se generó Cargo] " + _
                          "     , cast(dt_carga_comb_toka.just_st as varchar(500)) as [Observaciones ST] " + _
                          "     , isnull(recorrido, -999) / dt_carga_comb_toka.cantidad_mercancia as [Rend. Km x Litro] " + _
                          "from dt_carga_comb_toka " + _
                          "  left join bd_Empleado.dbo.cg_empleado cgEmpl on dt_carga_comb_toka.id_conductor = cgEmpl.no_empleado " + _
                          "where dt_carga_comb_toka.bloqueo_st = 'S' "

                If .cbSolicitante.Checked = True Then
                    queryE = queryE + "  and cgEmpl.id_empleado = @id_empleado "
                    queryTB = queryTB + "  and cgEmpl.id_empleado = @id_empleado "
                    queryTK = queryTK + "  and cgEmpl.id_empleado = @id_empleado "
                End If
                If .cbFechaC.Checked = True Then
                    queryE = queryE + "  and (fecha between @FechaIni and @FechaFin) "
                    queryTB = queryTB + "  and (fecha between @FechaIni and @FechaFin) "
                    queryTK = queryTK + "  and (fecha between @FechaIni and @FechaFin) "
                End If
                If .cbNoEco.Checked = True Then
                    queryE = queryE + "  and dt_carga_comb.identificador_vehiculo like '%' + @no_eco + '%' "
                    queryTB = queryTB + "  and dt_carga_comb_tar.identificador_vehiculo like '%' + @no_eco + '%' "
                    queryTK = queryTK + "  and dt_carga_comb_toka.identificador_vehiculo like '%' + @no_eco + '%' "
                End If

                sdaConsulta.SelectCommand = New SqlCommand(queryE + _
                                                           "union " + _
                                                           queryTB + _
                                                           "union " + _
                                                           queryTK + _
                                                           "order by Conductor, Fecha ", ConexionBD)

                If .cbSolicitante.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_empleado", .ddlSolicitante.SelectedValue)
                End If
                If .cbFechaC.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaIni", .wdpFechaI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaFin", .wdpFechaF.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
                End If
                If .cbNoEco.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@no_eco", .txtNoEco.Text.Trim)
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