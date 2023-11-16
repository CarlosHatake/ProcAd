Public Class _22
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
                                                               "	 , destino " +
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

#Region "Guardar"

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim valor As Integer = 0

                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "DECLARE @valorR int;  Execute Sp_U_EntEfectivo   @id_usr_recibe,  @fecha ,  @idMsReserv ,  @valorR OUTPUT; select @valorR"
                'SCMValores.CommandText = "update ms_reserva " + _
                '                         "  set status = 'R', id_usr_recibe = @id_usr_recibe, fecha_recibe = @fecha " + _
                '                         "where id_ms_reserva = @idMsReserv "
                SCMValores.Parameters.AddWithValue("@idMsReserv", Val(Session("idMsReserv")))
                SCMValores.Parameters.AddWithValue("@id_usr_recibe", Val(._txtIdUsuario.Text))
                SCMValores.Parameters.AddWithValue("@fecha", Date.Now)
                ConexionBD.Open()
                valor = SCMValores.ExecuteScalar()
                ConexionBD.Close()
                If valor = 0 Then
                    Server.Transfer("Menu.aspx")
                End If
                SCMValores.Parameters.Clear()
                .pnlSolicitud.Enabled = False
                .btnGuardar.Enabled = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class