Public Class CambVeh
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    ._txtIdUsuario.Text = Session("id_usuario")
                    ._txtIdMsReserv.Text = Session("idMsReserv")
                    'Creación de Variables para la conexión y consulta de información a la base de datos
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim sdaMsReserv As New SqlDataAdapter
                    Dim dsMsReserv As New DataSet
                    sdaMsReserv.SelectCommand = New SqlCommand("select empleado_nom + ' ' + empleado_appat + ' ' + empleado_apmat as solicito " + _
                                                               "     , autorizador_nom + ' ' + autorizador_appat + ' ' + autorizador_apmat as autorizador " + _
                                                               "	 , prioridad " + _
                                                               "	 , fecha_ini, fecha_fin " + _
                                                               "	 , no_eco + ' [' + placas + ']' as vehiculo " + _
                                                               "	 , destino " + _
                                                               "from ms_reserva " + _
                                                               "where id_ms_reserva = @idMsReserv ", ConexionBD)
                    sdaMsReserv.SelectCommand.Parameters.AddWithValue("@idMsReserv", Val(._txtIdMsReserv.Text))
                    ConexionBD.Open()
                    sdaMsReserv.Fill(dsMsReserv)
                    ConexionBD.Close()
                    .lblFolio.Text = ._txtIdMsReserv.Text
                    .lblSolicitante.Text = dsMsReserv.Tables(0).Rows(0).Item("solicito").ToString()
                    .lblAutorizador.Text = dsMsReserv.Tables(0).Rows(0).Item("autorizador").ToString()
                    .lblPrioridad.Text = dsMsReserv.Tables(0).Rows(0).Item("prioridad").ToString()
                    .wdteFechaIni.Date = CDate(dsMsReserv.Tables(0).Rows(0).Item("fecha_ini").ToString())
                    .wdteFechaFin.Date = CDate(dsMsReserv.Tables(0).Rows(0).Item("fecha_fin").ToString())
                    .lblVehiculo.Text = dsMsReserv.Tables(0).Rows(0).Item("vehiculo").ToString()
                    .lblDestino.Text = dsMsReserv.Tables(0).Rows(0).Item("destino").ToString()
                    sdaMsReserv.Dispose()
                    dsMsReserv.Dispose()
                    llenarVehiculos()
                Catch ex As Exception
                    .litError.Text = ex.ToString
                End Try
            End With
        End If
    End Sub

#End Region

#Region "Funciones"

    Public Sub llenarVehiculos()
        With Me
            Try
                If .wdteFechaIni.Date > .wdteFechaFin.Date Then
                    .ddlVehiculo.Items.Clear()
                Else
                    .litError.Text = ""

                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim sdaVehi As New SqlDataAdapter
                    Dim dsVehi As New DataSet
                    .ddlVehiculo.DataSource = dsVehi
                    sdaVehi.SelectCommand = New SqlCommand("select id_ms_vehiculo, no_eco + ' [' + placas + ']' vehiculo " + _
                                                           "from bd_Empleado.dbo.ms_vehiculo " + _
                                                           "where status = 'A' " + _
                                                           "  and uso_utilitario = 'COMODIN' " + _
                                                           "  and poliza_seguro_vig > GETDATE() " + _
                                                           "  and tarjeta_cir_vig > GETDATE() " + _
                                                           "  and id_ms_vehiculo not in (select id_ms_vehiculo " + _
                                                           "							 from ms_reserva " + _
                                                           "							 where ms_reserva.status <> 'Z' " + _
                                                           "							   and ((fecha_ini <= @fechaIni and fecha_fin > @fechaIni) " + _
                                                           "							     or (fecha_ini < @fechaFin and fecha_fin >= @fechaFin) " + _
                                                           "							     or (fecha_ini <= @fechaIni and fecha_fin >= @fechaFin) " + _
                                                           "							     or (fecha_ini > @fechaIni and fecha_fin < @fechaFin))) ", ConexionBD)
                    sdaVehi.SelectCommand.Parameters.AddWithValue("@fechaIni", .wdteFechaIni.Date)
                    sdaVehi.SelectCommand.Parameters.AddWithValue("@fechaFin", .wdteFechaFin.Date)
                    .ddlVehiculo.DataTextField = "vehiculo"
                    .ddlVehiculo.DataValueField = "id_ms_vehiculo"
                    ConexionBD.Open()
                    sdaVehi.Fill(dsVehi)
                    .ddlVehiculo.DataBind()
                    ConexionBD.Close()
                    sdaVehi.Dispose()
                    dsVehi.Dispose()
                    .ddlVehiculo.SelectedIndex = -1
                    If .ddlVehiculo.Items.Count = 0 Then
                        .litError.Text = "No hay vehículos disponibles, favor de validarlo"
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Guardar"

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        With Me
            Try
                .litError.Text = ""
                If .ddlVehiculo.Items.Count = 0 Then
                    .litError.Text = "No hay vehículos disponibles, favor de validarlo"
                Else
                    Dim cont As Integer = 0
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()

                    'Validar Disponibilidad
                    SCMValores.CommandText = "select count (*) " + _
                                             "from bd_Empleado.dbo.ms_vehiculo " + _
                                             "where status = 'A' " + _
                                             "  and uso_utilitario = 'COMODIN' " + _
                                             "  and poliza_seguro_vig > GETDATE() " + _
                                             "  and tarjeta_cir_vig > GETDATE() " + _
                                             "  and id_ms_vehiculo = @idMsVehiculo " + _
                                             "  and id_ms_vehiculo not in (select id_ms_vehiculo " + _
                                             "							 from ms_reserva " + _
                                             "							 where ms_reserva.status <> 'Z' " + _
                                             "							   and ((fecha_ini <= @fechaIni and fecha_fin > @fechaIni) " + _
                                             "							     or (fecha_ini < @fechaFin and fecha_fin >= @fechaFin) " + _
                                             "							     or (fecha_ini <= @fechaIni and fecha_fin >= @fechaFin) " + _
                                             "							     or (fecha_ini > @fechaIni and fecha_fin < @fechaFin))) "
                    SCMValores.Parameters.AddWithValue("@fechaIni", .wdteFechaIni.Date)
                    SCMValores.Parameters.AddWithValue("@fechaFin", .wdteFechaFin.Date)
                    SCMValores.Parameters.AddWithValue("@idMsVehiculo", .ddlVehiculo.SelectedValue)
                    ConexionBD.Open()
                    cont = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    If cont > 0 Then
                        'Insertar Histórico
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_historico_cVeh(id_ms_reserva, id_ms_vehiculo_orig, no_eco_orig, marca_orig, modelo_orig, año_orig, placas_orig, IAVE_orig, poliza_seguro_vig_orig, tarjeta_cir_vig_orig " + _
                                                 "                            , id_ms_vehiculo_nuev, no_eco_nuev, marca_nuev, modelo_nuev, año_nuev, placas_nuev, IAVE_nuev, poliza_seguro_vig_nuev, tarjeta_cir_vig_nuev " + _
                                                 "							  , id_usr_cambio, fecha_cambio) " + _
                                                 "select @idMsReserva, msRes.id_ms_vehiculo, msRes.no_eco, msRes.marca, msRes.modelo, msRes.año, msRes.placas, msRes.IAVE, msRes.poliza_seguro_vig, msRes.tarjeta_cir_vig " + _
                                                 "     , msVeh.id_ms_vehiculo, msVeh.no_eco, msVeh.marca, msVeh.modelo, msVeh.año, msVeh.placas, msVeh.IAVE, msVeh.poliza_seguro_vig, msVeh.tarjeta_cir_vig " + _
                                                 "	   , @idUsuario as id_usr_cambio, @Fecha as fecha_cambio " + _
                                                 "from ms_reserva msRes " + _
                                                 "  left join bd_Empleado.dbo.ms_vehiculo msVeh on msVeh.id_ms_vehiculo = @idMsVehiculo " + _
                                                 "where id_ms_reserva = @idMsReserva "
                        SCMValores.Parameters.AddWithValue("@idMsReserva", Val(._txtIdMsReserv.Text))
                        SCMValores.Parameters.AddWithValue("@idMsVehiculo", .ddlVehiculo.SelectedValue)
                        SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@Fecha", Date.Now)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Actualizar Reservación
                        SCMValores.CommandText = "update ms_reserva " + _
                                                 "set ms_reserva.id_ms_vehiculo = msVeh.id_ms_vehiculo, ms_reserva.no_eco = msVeh.no_eco, ms_reserva.marca = msVeh.marca, ms_reserva.modelo = msVeh.modelo, ms_reserva.año = msVeh.año " + _
                                                 "  , ms_reserva.placas = msVeh.placas, ms_reserva.IAVE = msVeh.IAVE, ms_reserva.poliza_seguro_vig = msVeh.poliza_seguro_vig, ms_reserva.tarjeta_cir_vig = msVeh.tarjeta_cir_vig " + _
                                                 "from ms_reserva msRes " + _
                                                 "  left join bd_Empleado.dbo.ms_vehiculo msVeh on msVeh.id_ms_vehiculo = @idMsVehiculo " + _
                                                 "where id_ms_reserva = @idMsReserva "
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        .pnlSolicitud.Enabled = False
                        .btnGuardar.Enabled = False
                    Else
                        .litError.Text = "Ya fue reservado el vehículo, favor de validar la disponibilidad"
                        llenarVehiculos()
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class