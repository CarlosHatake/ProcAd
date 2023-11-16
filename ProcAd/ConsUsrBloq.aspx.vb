Public Class ConsUsrBloq
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
                        sdaEmpleado.Dispose()
                        dsEmpleado.Dispose()

                        'Limpiar Pantalla
                        limpiar()
                        'Actualizar Tabla de Usuarios Bloqueados
                        llenarGrid()
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
            .cbConductor.Checked = False
            .pnlConductor.Visible = False
            .cbUnidad.Checked = False
            .pnlUnidad.Visible = False
            'Ocultar resto de paneles principales
            .pnlRegistros.Visible = False
            .pnlCancelar.Visible = False
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

    Public Sub llenarGrid()
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

                queryE = "select 'Edenred' as Tipo " + _
                         "     , id_dt_carga_comb " + _
                         "     , dt_carga_comb.fecha " + _
                         "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as nombre_conductor " + _
                         "     , case dt_carga_comb.bloqueo_st when 'S' then 'Sí' else null end as bloqueoST " + _
                         "     , dt_carga_comb.identificador_vehiculo as unidad " + _
                         "     , dt_carga_comb.placa " + _
                         "     , dt_carga_comb.num_tarjeta " + _
                         "     , dt_carga_comb.no_comprobante " + _
                         "     , dt_carga_comb.razon_social_afiliado " + _
                         "     , dt_carga_comb.cantidad_mercancia " + _
                         "     , dt_carga_comb.importe_transaccion " + _
                         "from dt_carga_comb " + _
                         "  left join bd_Empleado.dbo.cg_empleado cgEmpl on dt_carga_comb.id_conductor = cgEmpl.id_conductor_edenred and cgEmpl.status = 'A' " + _
                         "  left join cg_usuario on cgEmpl.id_empleado = cg_usuario.id_empleado and cg_usuario.status = 'A' "

                queryT = "select 'Tarjeta Bancaria' as Tipo " + _
                         "     , id_dt_carga_comb_tar " + _
                         "     , dt_carga_comb_tar.fecha " + _
                         "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as nombre_conductor " + _
                         "     , case dt_carga_comb_tar.bloqueo_st when 'S' then 'Sí' else null end as bloqueoST " + _
                         "     , dt_carga_comb_tar.identificador_vehiculo as unidad " + _
                         "     , dt_carga_comb_tar.placa " + _
                         "     , dt_carga_comb_tar.num_tarjeta " + _
                         "     , dt_carga_comb_tar.no_comprobante " + _
                         "     , dt_carga_comb_tar.razon_social_afiliado " + _
                         "     , dt_carga_comb_tar.cantidad_mercancia " + _
                         "     , dt_carga_comb_tar.importe_transaccion " + _
                         "from dt_carga_comb_tar " + _
                         "  left join cg_usuario on dt_carga_comb_tar.id_usr_carga = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                         "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado "

                queryTK = "select 'TOKA' as Tipo " + _
                          "     , id_dt_carga_comb_toka " + _
                          "     , dt_carga_comb_toka.fecha " + _
                          "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as nombre_conductor " + _
                          "     , case dt_carga_comb_toka.bloqueo_st when 'S' then 'Sí' else null end as bloqueoST " + _
                          "     , dt_carga_comb_toka.identificador_vehiculo as unidad " + _
                          "     , dt_carga_comb_toka.placa " + _
                          "     , dt_carga_comb_toka.num_tarjeta " + _
                          "     , dt_carga_comb_toka.no_comprobante " + _
                          "     , dt_carga_comb_toka.razon_social_afiliado " + _
                          "     , dt_carga_comb_toka.cantidad_mercancia " + _
                          "     , dt_carga_comb_toka.importe_transaccion " + _
                          "from dt_carga_comb_toka " + _
                          "  left join bd_Empleado.dbo.cg_empleado cgEmpl on dt_carga_comb_toka.id_conductor = cgEmpl.no_empleado "

                If ._txtPerfil.Text = "SopTec" Or ._txtPerfil.Text = "GerSopTec" Then
                    'Perfil de Soporte Técnico, solo ve lo que le corresponde
                    queryE = queryE + "where (dt_carga_comb.bloqueo_st = 'S' and dt_carga_comb.just_st is null) "
                    queryT = queryT + "where (dt_carga_comb_tar.bloqueo_st = 'S' and dt_carga_comb_tar.just_st is null)"
                    queryTK = queryTK + "where (dt_carga_comb_toka.bloqueo_st = 'S' and dt_carga_comb_toka.just_st is null)"
                Else
                    'Perfil de Comprobaciones, visualiza todos los bloqueos
                    queryE = queryE + "where ((dt_carga_comb.status = 'P' and getdate() >= fecha ) or (dt_carga_comb.bloqueo_st = 'S' and dt_carga_comb.just_st is null)) "
                    queryT = queryT + "where ((dt_carga_comb_tar.status = 'P' and getdate() >= fecha_carga ) or (dt_carga_comb_tar.bloqueo_st = 'S' and dt_carga_comb_tar.just_st is null))"
                    queryTK = queryTK + "where ((dt_carga_comb_toka.status = 'P' and getdate() >= fecha_carga ) or (dt_carga_comb_toka.bloqueo_st = 'S' and dt_carga_comb_toka.just_st is null))"
                End If

                If .cbFechaC.Checked = True Then
                    queryE = queryE + "  and (dt_carga_comb.fecha between @fechaIni and @fechaFin) "
                    queryT = queryT + "  and (dt_carga_comb_tar.fecha between @fechaIni and @fechaFin) "
                    queryTK = queryTK + "  and (dt_carga_comb_toka.fecha between @fechaIni and @fechaFin) "
                End If
                If .cbConductor.Checked = True Then
                    queryE = queryE + "  and cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno like '%' + @conductor + '%' "
                    queryT = queryT + "  and cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno like '%' + @conductor + '%' "
                    queryTK = queryTK + "  and cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno like '%' + @conductor + '%' "
                End If
                If .cbUnidad.Checked = True Then
                    queryE = queryE + "  and dt_carga_comb.identificador_vehiculo like '%' + @unidad + '%' "
                    queryT = queryT + "  and dt_carga_comb_tar.identificador_vehiculo like '%' + @unidad + '%' "
                    queryTK = queryTK + "  and dt_carga_comb_toka.identificador_vehiculo like '%' + @unidad + '%' "
                End If
                queryG = queryE + "union " + queryT + "union " + queryTK + "order by nombre_conductor, dt_carga_comb.fecha "

                sdaConsulta.SelectCommand = New SqlCommand(queryG, ConexionBD)

                If .cbFechaC.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaIni", .wdpFechaI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaFin", .wdpFechaF.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
                End If
                If .cbConductor.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@conductor", .txtConductor.Text.Trim)
                End If
                If .cbUnidad.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@unidad", .txtUnidad.Text.Trim)
                End If

                .gvRegistros.DataSource = dsConsulta
                'Habilitar columnas para actualización
                .gvRegistros.Columns(0).Visible = True
                'If ._txtPerfil.Text <> "SopTec" And ._txtPerfil.Text <> "GerSopTec" Then
                '    .gvRegistros.Columns(1).Visible = True
                'End If

                ConexionBD.Open()
                sdaConsulta.Fill(dsConsulta)
                ConexionBD.Close()
                .gvRegistros.DataBind()
                sdaConsulta.Dispose()
                dsConsulta.Dispose()
                .gvRegistros.SelectedIndex = -1

                'Inhabilitar columnas para vista
                .gvRegistros.Columns(0).Visible = False
                'If ._txtPerfil.Text <> "SopTec" And ._txtPerfil.Text <> "GerSopTec" Then
                '    .gvRegistros.Columns(1).Visible = False
                'End If

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

#Region "Filtros"

    Protected Sub cbFechaC_CheckedChanged(sender As Object, e As EventArgs) Handles cbFechaC.CheckedChanged
        vista(Me.pnlFechaC, Me.cbFechaC.Checked)
    End Sub

    Protected Sub cbConductor_CheckedChanged(sender As Object, e As EventArgs) Handles cbConductor.CheckedChanged
        vista(Me.pnlConductor, Me.cbConductor.Checked)
    End Sub

    Protected Sub cbUnidad_CheckedChanged(sender As Object, e As EventArgs) Handles cbUnidad.CheckedChanged
        vista(Me.pnlUnidad, Me.cbUnidad.Checked)
    End Sub

#End Region

#Region "Buscar"

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        llenarGrid()
    End Sub

#End Region

#Region "Tabla - Solicitudes"

    Protected Sub gvRegistros_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRegistros.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                .pnlFiltros.Visible = False
                .pnlRegistros.Visible = False
                .pnlCancelar.Visible = False
                .pnlDetalle.Visible = False

                If ._txtPerfil.Text <> "SopTec" And ._txtPerfil.Text <> "GerSopTec" Then
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    'Datos de la Carga de Combustible
                    Dim sdaSol As New SqlDataAdapter
                    Dim dsSol As New DataSet
                    Dim query As String
                    query = "select isnull(fecha, '') as fecha " + _
                            "     , isnull(identificador_vehiculo, '') as unidad " + _
                            "     , isnull(placa, '') as placa " + _
                            "     , isnull(num_tarjeta, '') as num_tarjeta " + _
                            "     , isnull(no_comprobante, '') as no_comprobante " + _
                            "     , razon_social_afiliado " + _
                            "     , isnull(cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno, '') as nombre_conductor " + _
                            "     , cantidad_mercancia " + _
                            "     , precio_ticket " + _
                            "     , importe_con_ieps " + _
                            "     , iva " + _
                            "     , importe_transaccion " + _
                            "     , isnull(km_ant_transaccion, -9999) as km_ant_transaccion " + _
                            "     , isnull(km_transaccion, -9999) as km_transaccion " + _
                            "     , isnull(recorrido, -999) as recorrido " + _
                            "     , isnull(odometro_evid, 0) as odometro_evid " + _
                            "     , isnull(foto_evid, 'XX') as foto_evid "
                    If .gvRegistros.SelectedRow.Cells(12).Text = "Edenred" Then
                        query = query + "from dt_carga_comb " + _
                                        "  left join bd_Empleado.dbo.ms_vehiculo msVehi on dt_carga_comb.num_tarjeta = msVehi.no_tarjeta_edenred " + _
                                        "  left join bd_Empleado.dbo.cg_empleado cgEmpl on dt_carga_comb.id_conductor = cgEmpl.id_conductor_edenred and cgEmpl.status = 'A' " + _
                                        "where id_dt_carga_comb = @id_dt_carga_comb "
                    Else
                        If .gvRegistros.SelectedRow.Cells(12).Text = "TOKA" Then
                            query = query + "from dt_carga_comb_toka " + _
                                            "  left join bd_Empleado.dbo.cg_empleado cgEmpl on dt_carga_comb_toka.id_conductor = cgEmpl.no_empleado " + _
                                            "where id_dt_carga_comb_toka = @id_dt_carga_comb "
                        Else
                            query = query + "from dt_carga_comb_tar " + _
                                            "  left join cg_usuario on dt_carga_comb_tar.id_usr_carga = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                                            "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                            "where id_dt_carga_comb_tar = @id_dt_carga_comb "
                        End If
                    End If

                    sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                    sdaSol.SelectCommand.Parameters.AddWithValue("@id_dt_carga_comb", Val(.gvRegistros.SelectedRow.Cells(0).Text))
                    ConexionBD.Open()
                    sdaSol.Fill(dsSol)
                    ConexionBD.Close()

                    If CDate(dsSol.Tables(0).Rows(0).Item("fecha").ToString()) = CDate("01-01-1900") Then
                        .lblFechaC.Text = ""
                    Else
                        .lblFechaC.Text = dsSol.Tables(0).Rows(0).Item("fecha").ToString()
                    End If
                    .lblUnidadC.Text = dsSol.Tables(0).Rows(0).Item("unidad").ToString()
                    .lblPlacaC.Text = dsSol.Tables(0).Rows(0).Item("placa").ToString()
                    .lblNoTarjetaC.Text = dsSol.Tables(0).Rows(0).Item("num_tarjeta").ToString()
                    .lblEstacionC.Text = dsSol.Tables(0).Rows(0).Item("razon_social_afiliado").ToString()
                    .lblConductorC.Text = dsSol.Tables(0).Rows(0).Item("nombre_conductor").ToString()
                    .lblNoTicketC.Text = dsSol.Tables(0).Rows(0).Item("no_comprobante").ToString()
                    .wneLitrosC.Value = Val(dsSol.Tables(0).Rows(0).Item("cantidad_mercancia").ToString())
                    .wcePrecioC.Value = Val(dsSol.Tables(0).Rows(0).Item("precio_ticket").ToString())
                    .wceSubtotalC.Value = Val(dsSol.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                    .wceIVAC.Value = Val(dsSol.Tables(0).Rows(0).Item("iva").ToString())
                    .wceTotalC.Value = Val(dsSol.Tables(0).Rows(0).Item("importe_transaccion").ToString())
                    sdaSol.Dispose()
                    dsSol.Dispose()

                    .pnlCancelar.Visible = True
                Else
                    Dim texto As String
                    texto = .gvRegistros.SelectedRow.Cells(4).Text
                    If .gvRegistros.SelectedRow.Cells(4).Text.Trim <> "&nbsp;" Then
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        'Datos de la Carga de Combustible
                        Dim sdaSol As New SqlDataAdapter
                        Dim dsSol As New DataSet
                        Dim query As String
                        query = "select isnull(fecha, '') as fecha " + _
                                "     , isnull(identificador_vehiculo, '') as unidad " + _
                                "     , isnull(placa, '') as placa " + _
                                "     , isnull(num_tarjeta, '') as num_tarjeta " + _
                                "     , isnull(no_comprobante, '') as no_comprobante " + _
                                "     , razon_social_afiliado " + _
                                "     , isnull(cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno, '') as nombre_conductor " + _
                                "     , cantidad_mercancia " + _
                                "     , precio_ticket " + _
                                "     , importe_con_ieps " + _
                                "     , iva " + _
                                "     , importe_transaccion " + _
                                "     , isnull(km_ant_transaccion, -9999) as km_ant_transaccion " + _
                                "     , isnull(km_transaccion, -9999) as km_transaccion " + _
                                "     , isnull(recorrido, -999) as recorrido " + _
                                "     , isnull(odometro_evid, 0) as odometro_evid " + _
                                "     , isnull(foto_evid, 'XX') as foto_evid "
                        If .gvRegistros.SelectedRow.Cells(12).Text = "Edenred" Then
                            query = query + "from dt_carga_comb " + _
                                            "  left join bd_Empleado.dbo.ms_vehiculo msVehi on dt_carga_comb.num_tarjeta = msVehi.no_tarjeta_edenred " + _
                                            "  left join bd_Empleado.dbo.cg_empleado cgEmpl on dt_carga_comb.id_conductor = cgEmpl.id_conductor_edenred and cgEmpl.status = 'A' " + _
                                            "where id_dt_carga_comb = @id_dt_carga_comb "
                        Else
                            If .gvRegistros.SelectedRow.Cells(12).Text = "TOKA" Then
                                query = query + "from dt_carga_comb_toka " + _
                                                "  left join bd_Empleado.dbo.cg_empleado cgEmpl on dt_carga_comb_toka.id_conductor = cgEmpl.no_empleado " + _
                                                "where id_dt_carga_comb_toka = @id_dt_carga_comb "
                            Else
                                query = query + "from dt_carga_comb_tar " + _
                                                "  left join cg_usuario on dt_carga_comb_tar.id_usr_carga = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                                                "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                "where id_dt_carga_comb_tar = @id_dt_carga_comb "
                            End If
                        End If

                        sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@id_dt_carga_comb", Val(.gvRegistros.SelectedRow.Cells(0).Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()

                        If CDate(dsSol.Tables(0).Rows(0).Item("fecha").ToString()) = CDate("01-01-1900") Then
                            .lblFecha.Text = ""
                        Else
                            .lblFecha.Text = dsSol.Tables(0).Rows(0).Item("fecha").ToString()
                        End If
                        .lblUnidad.Text = dsSol.Tables(0).Rows(0).Item("unidad").ToString()
                        .lblPlaca.Text = dsSol.Tables(0).Rows(0).Item("placa").ToString()
                        .lblNoTarjeta.Text = dsSol.Tables(0).Rows(0).Item("num_tarjeta").ToString()
                        .lblEstacion.Text = dsSol.Tables(0).Rows(0).Item("razon_social_afiliado").ToString()
                        .lblConductor.Text = dsSol.Tables(0).Rows(0).Item("nombre_conductor").ToString()
                        .lblNoTicket.Text = dsSol.Tables(0).Rows(0).Item("no_comprobante").ToString()
                        .wneLitros.Value = Val(dsSol.Tables(0).Rows(0).Item("cantidad_mercancia").ToString())
                        .wcePrecio.Value = Val(dsSol.Tables(0).Rows(0).Item("precio_ticket").ToString())
                        .wceSubtotal.Value = Val(dsSol.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                        .wceIVA.Value = Val(dsSol.Tables(0).Rows(0).Item("iva").ToString())
                        .wceTotal.Value = Val(dsSol.Tables(0).Rows(0).Item("importe_transaccion").ToString())
                        .wneOdometroAnt.Value = Val(dsSol.Tables(0).Rows(0).Item("km_ant_transaccion").ToString())
                        .wneOdometroAct.Value = Val(dsSol.Tables(0).Rows(0).Item("km_transaccion").ToString())
                        .wneKmsRecorridos.Value = Val(dsSol.Tables(0).Rows(0).Item("recorrido").ToString())
                        .wneOdometroU.Value = Val(dsSol.Tables(0).Rows(0).Item("odometro_evid").ToString())
                        If dsSol.Tables(0).Rows(0).Item("foto_evid").ToString() = "XX" Then
                            .lbl_OdometroF.Visible = False
                            .hlOdometroF.Visible = False
                        Else
                            .lbl_OdometroF.Visible = True
                            .hlOdometroF.Visible = True
                            .hlOdometroF.Text = dsSol.Tables(0).Rows(0).Item("foto_evid").ToString()
                            Dim tipoA As String
                            If .gvRegistros.SelectedRow.Cells(12).Text = "Edenred" Then
                                tipoA = "E"
                            Else
                                tipoA = "T"
                            End If
                            '.hlOdometroF.NavigateUrl = "http://localhost/ProcAd - Adjuntos GasC/" + .gvRegistros.SelectedRow.Cells(0).Text + "FotoH" + tipoA + "-" + dsSol.Tables(0).Rows(0).Item("foto_evid").ToString()
                            .hlOdometroF.NavigateUrl = "http://148.223.153.43/ProcAd - Adjuntos GasC/" + .gvRegistros.SelectedRow.Cells(0).Text + "FotoH" + tipoA + "-" + dsSol.Tables(0).Rows(0).Item("foto_evid").ToString()
                        End If
                        sdaSol.Dispose()
                        dsSol.Dispose()

                        .rblCargoCond.SelectedIndex = -1

                        'Cargar Registros de la Bitácora de Cargas
                        Dim sdaBitacora As New SqlDataAdapter
                        Dim dsBitacora As New DataSet
                        sdaBitacora.SelectCommand = New SqlCommand("select top 5 * " + _
                                                                   "from (select fecha as Fecha " + _
                                                                   "           , no_comprobante as Ticket " + _
                                                                   "           , razon_social_afiliado as Estación " + _
                                                                   "           , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as Conductor " + _
                                                                   "           , km_ant_transaccion as [Km Inicial] " + _
                                                                   "           , km_transaccion as [Km Final] " + _
                                                                   "           , recorrido as [Km Recorridos] " + _
                                                                   "           , cantidad_mercancia as [Litros Cargados] " + _
                                                                   "           , importe_transaccion as [Importe Total] " + _
                                                                   "           , dt_carga_comb.rendimiento as [Rendimiento Tabulado] " + _
                                                                   "           , rendimiento_real as [Rendimiento Real] " + _
                                                                   "           , tolerancia_unidad as [Tolerancia] " + _
                                                                   "           , desviacion_real as [Desviación Real] " + _
                                                                   "      from dt_carga_comb " + _
                                                                   "        left join bd_Empleado.dbo.cg_empleado cgEmpl on dt_carga_comb.id_conductor = cgEmpl.id_conductor_edenred and cgEmpl.status = 'A' " + _
                                                                   "      where num_tarjeta = @num_tarjeta " + _
                                                                   "      union " + _
                                                                   "      select fecha as Fecha " + _
                                                                   "           , no_comprobante as Ticket " + _
                                                                   "           , razon_social_afiliado as Estación " + _
                                                                   "           , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as Conductor " + _
                                                                   "           , km_ant_transaccion as [Km Inicial] " + _
                                                                   "           , km_transaccion as [Km Final] " + _
                                                                   "           , recorrido as [Km Recorridos] " + _
                                                                   "           , cantidad_mercancia as [Litros Cargados] " + _
                                                                   "           , importe_transaccion as [Importe Total] " + _
                                                                   "           , dt_carga_comb_tar.rendimiento as [Rendimiento Tabulado] " + _
                                                                   "           , rendimiento_real as [Rendimiento Real] " + _
                                                                   "           , tolerancia_unidad as [Tolerancia] " + _
                                                                   "           , desviacion_real as [Desviación Real] " + _
                                                                   "      from dt_carga_comb_tar " + _
                                                                   "        left join cg_usuario on dt_carga_comb_tar.id_usr_carga = cg_usuario.id_usuario " + _
                                                                   "        left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                                   "      where num_tarjeta = @num_tarjeta " + _
                                                                   "      union " + _
                                                                   "      select fecha as Fecha " + _
                                                                   "           , no_comprobante as Ticket " + _
                                                                   "           , razon_social_afiliado as Estación " + _
                                                                   "           , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as Conductor " + _
                                                                   "           , km_ant_transaccion as [Km Inicial] " + _
                                                                   "           , km_transaccion as [Km Final] " + _
                                                                   "           , recorrido as [Km Recorridos] " + _
                                                                   "           , cantidad_mercancia as [Litros Cargados] " + _
                                                                   "           , importe_transaccion as [Importe Total] " + _
                                                                   "           , dt_carga_comb_toka.rendimiento as [Rendimiento Tabulado] " + _
                                                                   "           , rendimiento_real as [Rendimiento Real] " + _
                                                                   "           , tolerancia_unidad as [Tolerancia] " + _
                                                                   "           , desviacion_real as [Desviación Real] " + _
                                                                   "      from dt_carga_comb_toka " + _
                                                                   "          left join bd_Empleado.dbo.cg_empleado cgEmpl on dt_carga_comb_toka.id_conductor = cgEmpl.no_empleado " + _
                                                                   "      where num_tarjeta = @num_tarjeta " + _
                                                                   "      ) as bitacora " + _
                                                                   "order by fecha desc ", ConexionBD)
                        sdaBitacora.SelectCommand.Parameters.AddWithValue("@num_tarjeta", .lblNoTarjeta.Text.Trim)
                        .gvBitacora.DataSource = dsBitacora
                        ConexionBD.Open()
                        sdaBitacora.Fill(dsBitacora)
                        ConexionBD.Close()
                        .gvBitacora.DataBind()
                        sdaBitacora.Dispose()
                        dsBitacora.Dispose()
                        .gvBitacora.SelectedIndex = -1

                        .pnlDetalle.Visible = True
                    End If
                End If
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
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                If .gvRegistros.SelectedRow.Cells(12).Text = "Edenred" Then
                    SCMValores.CommandText = "update dt_carga_comb SET status = 'Z', id_usr_cancela = @id_usr_cancela, fecha_cancela = @fecha_cancela " + _
                                             "where id_dt_carga_comb = @id_dt_carga_comb "
                Else
                    If .gvRegistros.SelectedRow.Cells(12).Text = "TOKA" Then
                        SCMValores.CommandText = "update dt_carga_comb_toka SET status = 'Z', id_usr_cancela = @id_usr_cancela, fecha_cancela = @fecha_cancela " + _
                                                 "where id_dt_carga_comb_toka = @id_dt_carga_comb "
                    Else
                        SCMValores.CommandText = "update dt_carga_comb_tar SET status = 'Z', id_usr_cancela = @id_usr_cancela, fecha_cancela = @fecha_cancela " + _
                                                 "where id_dt_carga_comb_tar = @id_dt_carga_comb "
                    End If
                End If
                SCMValores.Parameters.AddWithValue("@id_usr_cancela", Val(._txtIdUsuario.Text))
                SCMValores.Parameters.AddWithValue("@fecha_cancela", Date.Now)
                SCMValores.Parameters.AddWithValue("@id_dt_carga_comb", Val(.gvRegistros.SelectedRow.Cells(0).Text))

                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                limpiar()
                llenarGrid()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnDesbloquear_Click(sender As Object, e As EventArgs) Handles btnDesbloquear.Click
        With Me
            Try
                .litError.Text = ""

                If .txtObs.Text.Trim = "" Then
                    .litError.Text = "Información Insuficiente, favor de ingresar la justificación para el Desbloqueo del Conductor"
                Else
                    If .rblCargoCond.SelectedIndex = -1 Then
                        .litError.Text = "Información Insuficiente, favor de indicar si se realizó un cargo al conductor"
                    Else
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        If .gvRegistros.SelectedRow.Cells(12).Text = "Edenred" Then
                            SCMValores.CommandText = "update dt_carga_comb SET cargo_conductor = @cargo_conductor, just_st = @just_st, id_usr_st = @id_usr_st, fecha_st = @fecha_st " + _
                                                     "where id_dt_carga_comb = @id_dt_carga_comb "
                        Else
                            If .gvRegistros.SelectedRow.Cells(12).Text = "TOKA" Then
                                SCMValores.CommandText = "update dt_carga_comb_toka SET cargo_conductor = @cargo_conductor, just_st = @just_st, id_usr_st = @id_usr_st, fecha_st = @fecha_st " + _
                                                         "where id_dt_carga_comb_toka = @id_dt_carga_comb "
                            Else
                                SCMValores.CommandText = "update dt_carga_comb_tar SET cargo_conductor = @cargo_conductor, just_st = @just_st, id_usr_st = @id_usr_st, fecha_st = @fecha_st " + _
                                                         "where id_dt_carga_comb_tar = @id_dt_carga_comb "
                            End If
                        End If
                        SCMValores.Parameters.AddWithValue("@cargo_conductor", .rblCargoCond.SelectedValue)
                        SCMValores.Parameters.AddWithValue("@just_st", .txtObs.Text.Trim)
                        SCMValores.Parameters.AddWithValue("@id_usr_st", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@fecha_st", Date.Now)
                        SCMValores.Parameters.AddWithValue("@id_dt_carga_comb", Val(.gvRegistros.SelectedRow.Cells(0).Text))

                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        limpiar()
                        llenarGrid()
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Nueva Búsqueda"

    Protected Sub btnNueBusProdC_Click(sender As Object, e As EventArgs) Handles btnNueBusProdC.Click
        Me.pnlFiltros.Visible = True
        Me.pnlRegistros.Visible = True
        Me.pnlCancelar.Visible = False
        Me.pnlDetalle.Visible = False
        Me.gvRegistros.SelectedIndex = -1
    End Sub

    Protected Sub btnNueBusProd_Click(sender As Object, e As EventArgs) Handles btnNueBusProd.Click
        Me.pnlFiltros.Visible = True
        Me.pnlRegistros.Visible = True
        Me.pnlCancelar.Visible = False
        Me.pnlDetalle.Visible = False
        Me.gvRegistros.SelectedIndex = -1
    End Sub

#End Region

End Class