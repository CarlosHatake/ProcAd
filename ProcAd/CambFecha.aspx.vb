Public Class CambFecha
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
                    sdaMsReserv.SelectCommand = New SqlCommand("select empleado_nom + ' ' + empleado_appat + ' ' + empleado_apmat as solicito " +
                                                               "     , autorizador_nom + ' ' + autorizador_appat + ' ' + autorizador_apmat as autorizador " +
                                                               "	 , prioridad " +
                                                               "	 , fecha_ini, fecha_fin " +
                                                               "	 , no_eco + ' [' + placas + ']' as vehiculo " +
                                                               "	 , destino, id_ms_vehiculo " +
                                                               "from ms_reserva " +
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
                    ._txtIdMsVeh.Text = dsMsReserv.Tables(0).Rows(0).Item("id_ms_vehiculo").ToString()
                    .lblDestino.Text = dsMsReserv.Tables(0).Rows(0).Item("destino").ToString()
                    sdaMsReserv.Dispose()
                    dsMsReserv.Dispose()
                Catch ex As Exception
                    .litError.Text = ex.ToString
                End Try
            End With
        End If
    End Sub

#End Region

#Region "Funciones"

    Public Function validarDisp()
        With Me
            Try
                If .wdteFechaIni.Date > .wdteFechaFin.Date Then
                    .litError.Text = "Periodo inválido, favor de validarlo"
                    validarDisp = False
                Else
                    .litError.Text = ""

                    Dim cont As Integer = 0
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()

                    'Validar Disponibilidad
                    SCMValores.CommandText = "select count (*) " +
                                             "from bd_Empleado.dbo.ms_vehiculo " +
                                             "where status = 'A' " +
                                             "  and uso_utilitario = 'COMODIN' " +
                                             "  and poliza_seguro_vig > GETDATE() " +
                                             "  and tarjeta_cir_vig > GETDATE() " +
                                             "  and id_ms_vehiculo = @idMsVehiculo " +
                                             "  and id_ms_vehiculo not in (select id_ms_vehiculo " +
                                             "							 from ms_reserva " +
                                             "							 where ms_reserva.status not in ('Z', 'ZM') " +
                                             "							   and ms_reserva.id_ms_reserva <> @IdMsReserva " +
                                             "							   and ((fecha_ini <= @fechaIni and fecha_fin > @fechaIni) " +
                                             "							     or (fecha_ini < @fechaFin and fecha_fin >= @fechaFin) " +
                                             "							     or (fecha_ini <= @fechaIni and fecha_fin >= @fechaFin) " +
                                             "							     or (fecha_ini > @fechaIni and fecha_fin < @fechaFin))) "
                    SCMValores.Parameters.AddWithValue("@fechaIni", .wdteFechaIni.Date)
                    SCMValores.Parameters.AddWithValue("@fechaFin", .wdteFechaFin.Date)
                    SCMValores.Parameters.AddWithValue("@idMsVehiculo", Val(._txtIdMsVeh.Text))
                    SCMValores.Parameters.AddWithValue("@IdMsReserva", Val(._txtIdMsReserv.Text))
                    ConexionBD.Open()
                    cont = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    If cont > 0 Then
                        validarDisp = True
                    Else
                        .litError.Text = "Existen otras reservaciones autorizadas, favor de validarlo"
                        validarDisp = False
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
                validarDisp = False
            End Try
        End With
    End Function

#End Region

#Region "Guardar"

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        With Me
            Try
                .litError.Text = ""
                If validarDisp() Then
                    Dim cont As Integer = 0
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()

                    'Insertar Histórico
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into ms_historico_cFec (id_ms_reserva, fecha_fin_orig, fecha_fin_nuev, id_usr_cambio, fecha_cambio) " +
                                             "select @idMsReserva, msRes.fecha_fin, @fechaFinNueva as fecha_fin_nueva, @idUsuario as id_usr_cambio, @Fecha as fecha_cambio " +
                                             "from ms_reserva msRes " +
                                             "where id_ms_reserva = @idMsReserva "
                    SCMValores.Parameters.AddWithValue("@idMsReserva", Val(._txtIdMsReserv.Text))
                    SCMValores.Parameters.AddWithValue("@fechaFinNueva", .wdteFechaFin.Date)
                    SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                    SCMValores.Parameters.AddWithValue("@Fecha", Date.Now)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Actualizar Reservación
                    SCMValores.CommandText = "update ms_reserva set fecha_fin = @fechaFinNueva " +
                                             "where id_ms_reserva = @idMsReserva "
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    .pnlSolicitud.Enabled = False
                    .btnGuardar.Enabled = False
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

    Protected Sub wdteFechaFin_ValueChanged(sender As Object, e As TextEditorValueChangedEventArgs) Handles wdteFechaFin.ValueChanged
        validarDisp()
    End Sub
End Class