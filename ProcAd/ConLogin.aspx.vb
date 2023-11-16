Public Class ConLogin
    Inherits System.Web.UI.Page
    Public nombre As String
    Public idUsuario As Integer
    Dim accessDB As New clsAccess

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub Seleccionar(idUsuario, nom)
        With Me

            'variables pasa mover el usuario y contraseña obtenidos en la consulta
            Dim nick As String
            Dim pass As String
            Dim pin As String

            'Se establece la conexion a base de datos, 
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim sdaUsuario As New SqlDataAdapter
            Dim dsDatos As New DataSet

            Dim passOrg As String = accessDB.RandomString()
            Dim passSHA1 As String = accessDB.EncryptSHA1(passOrg)
            Dim passEncr As String = accessDB.Encrypt(passSHA1)

            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
            SCMValores.Connection = ConexionBD
            SCMValores.CommandText = ""
            SCMValores.Parameters.Clear()
            SCMValores.CommandText = "update cg_usuario where pass = @pass and status = 'A'"
            SCMValores.Parameters.AddWithValue("@idUsuario", idUsuario)
            SCMValores.Parameters.AddWithValue("@pass", passEncr)
            ConexionBD.Open()
            SCMValores.ExecuteNonQuery()
            ConexionBD.Close()

            'Realiza consulta y se obtiene el usuario y pin para implemetacion en correo
            sdaUsuario.SelectCommand = New SqlCommand("select nick, pin from cg_usuario where id_usuario = @idUsuario and status = 'A'", ConexionBD)
            sdaUsuario.SelectCommand.Parameters.AddWithValue("@idUsuario", idUsuario)
            ConexionBD.Open()
            sdaUsuario.Fill(dsDatos)
            ConexionBD.Close()
            nick = dsDatos.Tables(0).Rows(0).Item("nick").ToString()
            pass = passOrg
            pin = dsDatos.Tables(0).Rows(0).Item("pin").ToString()
            sdaUsuario.Dispose()
            dsDatos.Dispose()

            'Variables para  enviar correo
            Dim correo As New System.Net.Mail.MailMessage
            Dim smtp As New SmtpClient()

            'Datos de correo que envia 
            With smtp
                smtp.Host = "10.10.10.30"
                smtp.Port = 587
                smtp.EnableSsl = False
                smtp.UseDefaultCredentials = False
                smtp.Credentials = New System.Net.NetworkCredential("nprocad", "mc8HLB8lPe78")
            End With
            'Informacion que llevara el correo
            With correo
                .To.Add(txtCorreo.Text.Trim)
                .Bcc.Add("notificaciones.procad@unne.com.mx")
                .From = New MailAddress("notificaciones.procad@unne.com.mx")
                .Subject = "ProcAd - Recuperación de Usuario y Contraseña"

                'Cuerpo de correo
                Dim texto As String
                texto = "<span style=""font-family:Verdana;font-size: 10pt;"">
                             Buen día: 
                         <br>
                         <br> Por medio de la presente te hacemos llegar tu usuario y contraseña para el uso del ProcAd (Sistema de Procesos Administrativos). 
                              Es muy importante que utilices correctamente esta información considerando los siguientes aspectos: 
                         <br>
                         <ul style= ""list-style-type: square;"">
                         <li> El acceso al ProcAd es a través de la dirección http://148.223.153.43/ProcAd </li>
                         <li> La utilización del usuario y contraseña son de uso <b>PERSONAL</b>, es decir, tú eres el responsable de cualquier movimiento 
                                que se haga en el sistema con ese usuario, por lo tanto no debes prestarlo a nadie. </li>
                         </ul>
                         </span>
                         <table style=""font-family:Calibri;font-size: 11pt; border: 1px solid black; border-collapse: collapse; border-spacing: 5px; width: 310px"">
                         <tr><th style=""border: 1px solid black; border-collapse: collapse; border-spacing: 10px; text-align: center; width:125 px; color: white; background-color: black""> Usuario ProcAd </th>
                             <th style=""border: 1px solid black; border-collapse: collapse; border-spacing: 10px; text-align: center; width:125 px; color: white; background-color: black""> Contraseña </th>
                             <th style=""border: 1px solid black; border-collapse: collapse; border-spacing: 10px; text-align: center; width:45 px; color: white; background-color: black""> PIN </th>
                         </tr>
                         <tr><th style=""border: 1px solid black; border-collapse: collapse; border-spacing: 10px; text-align: center; width:125 px; color: black; background-color: #a9a9a9""> " + nick + " </th>
                             <th style=""border: 1px solid black; border-collapse: collapse; border-spacing: 10px; text-align: center; width:125 px;"">" + pass + "</th>
                             <th style=""border: 1px solid black; border-collapse: collapse; border-spacing: 10px; text-align: center; width:45 px;"">" + pin + "</th>
                         </tr>
                         </table>
                         <span style=""font-family:Verdana;font-size: 10pt;"">                 
                         <br>Saludos.
                         <br></span>
                         <span style=""font-family:Verdana;font-size: 11pt;"">     
                         <br><img src=""data:image/png;base64,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"" >
                         <br><font color='#1F497D'> <b>Departamento de T.I. </b>
                         <br>Carretera Tula-Refinería Km. 3
                         <br>El Llano 1ra Sección, Tula de Allende, Hgo.
                         <br>C.P. 42820
                         <br>Tel. +52 (773) 7329204 </font>
                         <br>http://www.unne.com.mx
                         </span>" +
                         "<span style=""font-family:Tahoma;font-size: 8pt;color:#163C86;"">" +
                         "<br><br>" &
                         "Antes de imprimir, piense en su responsabilidad y compromiso con el </span>" + "<span style=""font-family:Tahoma;font-size: 8pt;color:#9FFF77;"">" + "MEDIO AMBIENTE </span>" &
                         "<span style=""font-family:Tahoma;font-size: 8pt;color:#163C86;"">" +
                         "<br><br>" &
                         "La información contenida en este correo, es de carácter privado y confidencial y solo se dirige exclusivamente a los " +
                         "destinatarios. Se encuentra estrictamente prohibido reproducir, alterar, archivar o comunicar a terceros el presente mensaje " +
                         "y ficheros anexos, todo ello bajo pena de incurrir en responsabilidades legales."

                .Body = texto
                .IsBodyHtml = True
                .Priority = System.Net.Mail.MailPriority.Normal
                .Priority = MailPriority.Normal

            End With

            Try
                'Envio de Correo
                smtp.Send(correo)
                Dim msg As String
                msg = "Datos enviados de manera correcta"
                Me.Response.Output.Write("<script>javascript:alert('" & msg & "')</script>")
                'Redireccionamiendo a Login Una vez enviado el correo
                Server.Transfer("Login.aspx")
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnBuscar_Click(sender As Object, e As EventArgs) Handles ibtnCorreo.Click
        With Me

            Try
                'Validacion que txt no venga vacio
                If .txtCorreo.Text.Trim = "" Then
                    .litError.Text = "Información Insuficiente, favor de verificar"
                Else
                    'Establece conexion y genera consulta
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim cnsEmpl As New SqlDataAdapter
                    Dim cgEmp As New DataSet
                    gvEmp.DataSource = cgEmp
                    Dim query As String
                    query = "select e.nombre " +
                            "     , e.ap_paterno " +
                            "     , e.ap_materno " +
                            "     , c.nick " +
                            "     , c.id_usuario " +
                            "from cg_usuario as c " +
                            "  inner join  bd_Empleado.dbo.cg_empleado as e  on c.id_empleado=e.id_empleado " +
                            "where e.correo=@correo and e.status='A' and c.status='A'"

                    cnsEmpl.SelectCommand = New SqlCommand(query, ConexionBD)
                    cnsEmpl.SelectCommand.Parameters.AddWithValue("@correo", .txtCorreo.Text.Trim)

                    ConexionBD.Open()
                    cnsEmpl.Fill(cgEmp)
                    .gvEmp.DataBind()
                    ConexionBD.Close()
                    cnsEmpl.Dispose()
                    cgEmp.Dispose()
                    .gvEmp.Columns(0).Visible = False
                    .gvEmp.SelectedIndex = -1
                    .litError.Text = ""
                    'Bucle If que valida que la consula trajera los datos, asi mismo si es un solo dato traido en la consulta
                    If .gvEmp.Rows.Count = 0 Then
                        .txtCorreo.Text = ""
                        .gvEmp.DataSource = Nothing
                        .gvEmp.Columns(0).Visible = True
                        .ibtnEnviar.Visible = False
                        .lblNomb.Visible = False
                        .litError.Text = "<b>Verificar que el correo sea correcto y que cuente con usuario en sistema ProcAd</b>"
                    ElseIf gvEmp.Rows.Count = 1 Then
                        .litError.Text = ""
                        'Varibles por si la consulta  valida que solo trae un dato y generar el envio de el correo
                        Dim idUsuario As String
                        Dim Nom As String
                        Dim Ap As String
                        Dim Am As String
                        idUsuario = cgEmp.Tables(0).Rows(0).Item("id_usuario").ToString()
                        Nom = cgEmp.Tables(0).Rows(0).Item("nombre").ToString()
                        Ap = cgEmp.Tables(0).Rows(0).Item("ap_paterno").ToString()
                        Am = cgEmp.Tables(0).Rows(0).Item("ap_materno").ToString()
                        'LLamado de Metodo para envio de Correo
                        Seleccionar(idUsuario, Nom + " " + Ap + " " + Am)
                    End If
                End If
            Catch ex As Exception
                litError.Text = ex.Message
            End Try
        End With
    End Sub

    Protected Sub gvEmp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvEmp.SelectedIndexChanged
        With Me
            Try
                'Llenado de label para mostrar la informacion de persona seleccionada
                .litError.Text = ""
                .ibtnEnviar.Visible = True
                nombre = .gvEmp.SelectedRow.Cells(2).Text + " " + .gvEmp.SelectedRow.Cells(3).Text + " " + .gvEmp.SelectedRow.Cells(4).Text
                lblNomb.Text = nombre
                lbl_Nomb.Visible = True
                lblNomb.Visible = True

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnEnviar_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnEnviar.Click
        With Me
            Try
                'llenado de metodo para realizar el envio de correo
                nombre = .gvEmp.SelectedRow.Cells(2).Text + " " + .gvEmp.SelectedRow.Cells(3).Text + " " + .gvEmp.SelectedRow.Cells(4).Text
                idUsuario = Convert.ToInt32(gvEmp.SelectedRow.Cells(0).Text)
                Seleccionar(idUsuario, nombre)

            Catch ex As Exception
                litError.Text = ex.Message
            End Try
        End With
    End Sub

    Protected Sub btnMenu_Click(sender As Object, e As EventArgs) Handles btnMenu.Click
        With Me
            'Regrecion al Login
            Server.Transfer("Login.aspx")
        End With
    End Sub

End Class