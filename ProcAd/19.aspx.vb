Public Class _19
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
                                                               "	 , destino, just " + _
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
                    .txtJust.Text = dsMsReserv.Tables(0).Rows(0).Item("just").ToString()
                    sdaMsReserv.Dispose()
                    dsMsReserv.Dispose()
                Catch ex As Exception
                    .litError.Text = ex.ToString
                End Try
            End With
        End If
    End Sub

#End Region

#Region "Autorizar / Rechazar"

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "update ms_reserva " + _
                                         "  set status = 'A', fecha_autorizo = @fecha " + _
                                         "where id_ms_reserva = @idMsReserv "
                SCMValores.Parameters.AddWithValue("@idMsReserv", Val(Session("idMsReserv")))
                SCMValores.Parameters.AddWithValue("@fecha", Date.Now)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                'Envío de Correo
                Dim Mensaje As New System.Net.Mail.MailMessage()
                Dim destinatario As String = ""
                'Obtener el Correo del Solicitante
                SCMValores.CommandText = "select cgEmpl.correo " + _
                                         "from ms_reserva " + _
                                         "  left join cg_usuario on ms_reserva.id_usr_solicito = cg_usuario.id_usuario " + _
                                         "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                         "where id_ms_reserva = @id_ms_reserva "
                SCMValores.Parameters.AddWithValue("@id_ms_reserva", Val(.lblFolio.Text))
                ConexionBD.Open()
                destinatario = SCMValores.ExecuteScalar()
                ConexionBD.Close()

                Mensaje.[To].Add(destinatario)
                Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                Mensaje.Subject = "ProcAd - Reservación No. " + .lblFolio.Text + " Autorizada"
                Dim texto As String
                texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                        "La reservación número <b>" + .lblFolio.Text + "</b> fue autorizada. <br></span>"
                Mensaje.Body = texto
                Mensaje.IsBodyHtml = True
                Mensaje.Priority = MailPriority.Normal

                Dim Servidor As New SmtpClient()
                Servidor.Host = "10.10.10.30"
                Servidor.Port = 587
                Servidor.EnableSsl = False
                Servidor.UseDefaultCredentials = False
                Servidor.Credentials = New System.Net.NetworkCredential("nprocad", "mc8HLB8lPe78")

                Try
                    Servidor.Send(Mensaje)
                Catch ex As System.Net.Mail.SmtpException
                    .litError.Text = ex.ToString
                End Try

                .pnlSolicitud.Enabled = False
                .btnAceptar.Enabled = False
                .btnRechazar.Enabled = False

                Session("id_actividadM") = 19
                Session("TipoM") = "V"
                Server.Transfer("Menu.aspx")
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnRechazar_Click(sender As Object, e As EventArgs) Handles btnRechazar.Click
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "update ms_reserva " + _
                                         "  set status = 'Z', fecha_autorizo = @fecha " + _
                                         "where id_ms_reserva = @idMsReserv "
                SCMValores.Parameters.AddWithValue("@idMsReserv", Val(Session("idMsReserv")))
                SCMValores.Parameters.AddWithValue("@fecha", Date.Now)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                'Envío de Correo
                Dim Mensaje As New System.Net.Mail.MailMessage()
                Dim destinatario As String = ""
                'Obtener el Correo del Solicitante
                SCMValores.CommandText = "select cgEmpl.correo " + _
                                         "from ms_reserva " + _
                                         "  left join cg_usuario on ms_reserva.id_usr_solicito = cg_usuario.id_usuario " + _
                                         "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                         "where id_ms_reserva = @id_ms_reserva "
                SCMValores.Parameters.AddWithValue("@id_ms_reserva", Val(.lblFolio.Text))
                ConexionBD.Open()
                destinatario = SCMValores.ExecuteScalar()
                ConexionBD.Close()

                Mensaje.[To].Add(destinatario)
                Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                Mensaje.Subject = "ProcAd - Reservación No. " + .lblFolio.Text + " Rechazada"
                Dim texto As String
                texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                        "La reservación número <b>" + .lblFolio.Text + "</b> fue rechazada. <br></span>"
                Mensaje.Body = texto
                Mensaje.IsBodyHtml = True
                Mensaje.Priority = MailPriority.Normal

                Dim Servidor As New SmtpClient()
                Servidor.Host = "10.10.10.30"
                Servidor.Port = 587
                Servidor.EnableSsl = False
                Servidor.UseDefaultCredentials = False
                Servidor.Credentials = New System.Net.NetworkCredential("nprocad", "mc8HLB8lPe78")

                Try
                    Servidor.Send(Mensaje)
                Catch ex As System.Net.Mail.SmtpException
                    .litError.Text = ex.ToString
                End Try

                .pnlSolicitud.Enabled = False
                .btnAceptar.Enabled = False
                .btnRechazar.Enabled = False

                Session("id_actividadM") = 19
                Session("TipoM") = "V"
                Server.Transfer("Menu.aspx")
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class