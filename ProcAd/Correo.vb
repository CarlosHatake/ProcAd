Public Class Correo
    Public Sub enviarCorreo(cabeceraM As String, cuerpoM As String, destinatario As String)
        Try
            Dim ban As Boolean = True
            Do While ban = True
                Dim Mensaje As New System.Net.Mail.MailMessage()
                Mensaje.[To].Add(destinatario)
                Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                Mensaje.Subject = cabeceraM + " "
                Mensaje.Body = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                             cuerpoM + " <br></span>"
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
                    ban = False
                Catch ex As Exception
                    ban = True
                End Try
            Loop

        Catch ex As Exception

        End Try
    End Sub
End Class
