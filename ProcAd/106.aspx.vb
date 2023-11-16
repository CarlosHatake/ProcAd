Public Class _106
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess
    Dim clsCorreo As New Correo
#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("idMsInst") = 77651
                    'Session("id_usuario") = 64

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtIdMsInst.Text = Session("idMsInst")
                        ._txtBan.Text = 0

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim sdaSol As New SqlDataAdapter
                        Dim dsSol As New DataSet
                        Dim query As String
                        query = "select id_ms_ampliacion_p " +
                                "     , id_ms_presupuesto " +
                                "     , solicita " +
                                "     , empresa " +
                                "     , centro_costo " +
                                "     , just_motivo " +
                                "     , just_beneficio " +
                                "     , just_impacto " +
                                "     , correo " +
                                "from ms_ampliacion_p " +
                                "  left join ms_instancia on ms_ampliacion_p.id_ms_ampliacion_p = ms_instancia.id_ms_sol and ms_instancia.tipo = 'SAP' " +
                                "  left join cg_usuario on ms_ampliacion_p.id_usr_solicita = cg_usuario.id_usuario " +
                                "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " +
                                "where id_ms_instancia = @idMsInst "
                        sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@idMsInst", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()
                        .lblFolio.Text = dsSol.Tables(0).Rows(0).Item("id_ms_ampliacion_p").ToString()
                        ._txtIdMsPresup.Text = dsSol.Tables(0).Rows(0).Item("id_ms_presupuesto").ToString()
                        .lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("solicita").ToString()
                        ._txtCorreo.Text = dsSol.Tables(0).Rows(0).Item("correo").ToString()
                        .lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                        .lblCC.Text = dsSol.Tables(0).Rows(0).Item("centro_costo").ToString()
                        .txtJustMotivo.Text = dsSol.Tables(0).Rows(0).Item("just_motivo").ToString()
                        .txtJustBeneficio.Text = dsSol.Tables(0).Rows(0).Item("just_beneficio").ToString()
                        .txtJustImpacto.Text = dsSol.Tables(0).Rows(0).Item("just_impacto").ToString()
                        sdaSol.Dispose()
                        dsSol.Dispose()

                        'Actualizar Detalle
                        Dim sdaCatalogo As New SqlDataAdapter
                        Dim dsCatalogo As New DataSet
                        .gvDtAmpliacion.DataSource = dsCatalogo
                        'Habilitar columna para actualización
                        .gvDtAmpliacion.Columns(0).Visible = True
                        .gvDtAmpliacion.Columns(1).Visible = True
                        'Catálogo de Unidades agregados
                        'sdaCatalogo.SelectCommand = New SqlCommand("select id_dt_ampliacion_p " + _
                        '                                           "     , año " + _
                        '                                           "     , mes " + _
                        '                                           "     , monto_actual " + _
                        '                                           "     , monto_solicita " + _
                        '                                           "     , monto_nuevo " + _
                        '                                           "     , impacto_pres_monto " + _
                        '                                           "     , impacto_pres_porcent " + _
                        '                                           "from dt_ampliacion_p " + _
                        '                                           "where id_ms_ampliacion_p = @id_ms_ampliacion_p ", ConexionBD)
                        'sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_ms_ampliacion_p", Val(.lblFolio.Text))

                        sdaCatalogo.SelectCommand = New SqlCommand(" select id_dt_ampliacion_p , " +
                                                                   " año, " +
                                                                   " mes, " +
                                                                   " monto_solicita, " +
                                                                   " monto_actual, " +
                                                                   " monto_solicita_val, " +
                                                                   " monto_nuevo_val, " +
                                                                   " impacto_pres_monto_val," +
                                                                   " impacto_pres_porcent_val " +
                                                                   " from dt_ampliacion_p " +
                                                                   " where id_ms_ampliacion_p = @id_ms_ampliacion_p " +
                                                                   " and monto_nuevo_val > 0 " +
                                                                   " and impacto_pres_monto_val > 0 ", ConexionBD)
                        sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_ms_ampliacion_p", Val(.lblFolio.Text))

                        ConexionBD.Open()
                        sdaCatalogo.Fill(dsCatalogo)
                        .gvDtAmpliacion.DataBind()
                        ConexionBD.Close()
                        sdaCatalogo.Dispose()
                        dsCatalogo.Dispose()
                        .gvDtAmpliacion.SelectedIndex = -1
                        'Inhabilitar columna para vista
                        .gvDtAmpliacion.Columns(0).Visible = False
                        .gvDtAmpliacion.Columns(1).Visible = False

                        'Actualizar Adjuntos
                        Dim sdaArchivos As New SqlDataAdapter
                        Dim dsArchivos As New DataSet
                        .gvAdjuntos.DataSource = dsArchivos
                        'Adjuntos
                        sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " +
                                                                   "     , 'http://148.223.153.43/ProcAd - Adjuntos APGV/' + cast(id_dt_archivo_pgv as varchar(20)) + '-' + nombre as path " +
                                                                   "from dt_archivo_pgv " +
                                                                   "where id_ms_ampliacion_p = @id_ms_ampliacion_p ", ConexionBD)
                        sdaArchivos.SelectCommand.Parameters.AddWithValue("@id_ms_ampliacion_p", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        sdaArchivos.Fill(dsArchivos)
                        .gvAdjuntos.DataBind()
                        ConexionBD.Close()
                        sdaArchivos.Dispose()
                        dsArchivos.Dispose()
                        .gvAdjuntos.SelectedIndex = -1
                        .upAdjuntos.Update()

                        'Panel
                        .pnlInicio.Enabled = True
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

#Region "Autorizar / Rechazar"

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim fecha As DateTime = Date.Now
                While Val(._txtBan.Text) = 0


                    Dim valor As Integer
                    valor = 0
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "DECLARE @valorR int;  Execute SP_U_ms_ampliacion_p  @id_dt_ampliacion_p , @monto_nuevo_val ,  @impacto_pres_monto_val , @impacto_pres_porcent_val ,  @id_ms_instancia ,  @id_actividad ,@id_usr  ,@fecha , @comentario_autoriza, @id_ms_ampliacion_p , @valorR OUTPUT; select @valorR"
                    SCMValores.Parameters.AddWithValue("@id_dt_ampliacion_p", DBNull.Value)
                    SCMValores.Parameters.AddWithValue("@monto_nuevo_val", DBNull.Value)
                    SCMValores.Parameters.AddWithValue("@impacto_pres_monto_val", DBNull.Value)
                    SCMValores.Parameters.AddWithValue("@impacto_pres_porcent_val", DBNull.Value)
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 107)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)


                    If .txtComentario.Text.Trim = "" Then
                        SCMValores.Parameters.AddWithValue("@comentario_autoriza", DBNull.Value)
                    Else
                        SCMValores.Parameters.AddWithValue("@comentario_autoriza", .txtComentario.Text.Trim)
                    End If
                    SCMValores.Parameters.AddWithValue("@id_ms_ampliacion_p", Val(.lblFolio.Text))

                    ConexionBD.Open()
                    valor = SCMValores.ExecuteScalar()
                    ConexionBD.Close()

                    If valor = 0 Then
                        Server.Transfer("Menu.aspx")
                    End If
                    SCMValores.Parameters.Clear()

                    ''Actualizar datos de la Solicitud
                    'SCMValores.CommandText = ""
                    'SCMValores.Parameters.Clear()
                    'SCMValores.CommandText = "update ms_ampliacion_p set fecha_autoriza = @fecha_autoriza, comentario_autoriza = @comentario_autoriza, status = 'A' where id_ms_ampliacion_p = @id_ms_ampliacion_p "
                    'SCMValores.Parameters.AddWithValue("@fecha_autoriza", fecha)
                    'If .txtComentario.Text.Trim = "" Then
                    '    SCMValores.Parameters.AddWithValue("@comentario_autoriza", DBNull.Value)
                    'Else
                    '    SCMValores.Parameters.AddWithValue("@comentario_autoriza", .txtComentario.Text.Trim)
                    'End If
                    'SCMValores.Parameters.AddWithValue("@id_ms_ampliacion_p", Val(.lblFolio.Text))
                    'ConexionBD.Open()
                    'SCMValores.ExecuteNonQuery()
                    'ConexionBD.Close()

                    ._txtBan.Text = 1

                    For i = 0 To .gvDtAmpliacion.Rows.Count - 1
                        'Actualizar la tabla de Presupuesto
                        Dim mes As String
                        If Val(.gvDtAmpliacion.Rows(i).Cells(3).Text) < 10 Then
                            mes = "0" + .gvDtAmpliacion.Rows(i).Cells(3).Text
                        Else
                            mes = .gvDtAmpliacion.Rows(i).Cells(3).Text
                        End If
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_presupuesto " +
                                                 "  set mes_" + mes + "_a = mes_" + mes + "_a + @importeSol " +
                                                 "where id_ms_presupuesto = @id_ms_presupuesto "
                        SCMValores.Parameters.AddWithValue("@importeSol", Val(.gvDtAmpliacion.Rows(i).Cells(1).Text))
                        SCMValores.Parameters.AddWithValue("@id_ms_presupuesto", Val(._txtIdMsPresup.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()
                        SCMValores.CommandText = ""

                    Next

                    ''Actualizar Instancia
                    'SCMValores.CommandText = ""
                    'SCMValores.Parameters.Clear()
                    'SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                    'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    'SCMValores.Parameters.AddWithValue("@id_actividad", 107)
                    'ConexionBD.Open()
                    'SCMValores.ExecuteNonQuery()
                    'ConexionBD.Close()

                    ''Registrar en Histórico
                    'SCMValores.CommandText = ""
                    'SCMValores.Parameters.Clear()
                    'SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                    '                         "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    'SCMValores.Parameters.AddWithValue("@id_actividad", 107)
                    'SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    'SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    'ConexionBD.Open()
                    'SCMValores.ExecuteNonQuery()
                    'ConexionBD.Close()

                    'Envio de correo 
                    Dim cabeceraM As String, cuerpoM As String, destinatario As String
                    destinatario = ._txtCorreo.Text

                    cabeceraM = "ProcAd - Solicitud de Ampliación No. " + .lblFolio.Text + " Autorizada"
                    cuerpoM = " Buen día:
                             <br>
                             <br> Por medio de la presente le informamos que la solicitud de <b> ampliación de presupuesto para gastos de viaje </b> 
                             <br>
                             con número de folio: <b>" + .lblFolio.Text + "</b> del sistema  <b>ProcAd</b> http://148.223.153.43/ProcAd ha sido <b>autorizada.</b>
                             <br> "
                    clsCorreo.enviarCorreo(cabeceraM, cuerpoM, destinatario)


                    ''Envío de Correo
                    'Dim Mensaje As New System.Net.Mail.MailMessage()
                    'Dim destinatario As String = ""
                    'destinatario = ._txtCorreo.Text

                    'Mensaje.[To].Add(destinatario)
                    'Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                    ''Mensaje.From = New System.Net.Mail.MailAddress("soporte.unne.procad@gmail.com", "ProcAd", System.Text.Encoding.UTF8)
                    'Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                    'Mensaje.Subject = "ProcAd - Solicitud de Ampliación No. " + .lblFolio.Text + " Autorizada"
                    'Dim texto As String
                    'texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                    '        "La solicitud número <b>" + .lblFolio.Text + "</b> fue autorizada.<br></span>"
                    'Mensaje.Body = texto
                    'Mensaje.IsBodyHtml = True
                    'Mensaje.Priority = MailPriority.Normal

                    'Dim Servidor As New SmtpClient()
                    'Servidor.Host = "10.10.10.30"
                    'Servidor.Port = 587
                    'Servidor.EnableSsl = False
                    'Servidor.UseDefaultCredentials = False
                    'Servidor.Credentials = New System.Net.NetworkCredential("nprocad", "mc8HLB8lPe78")

                    'Try
                    '    Servidor.Send(Mensaje)
                    'Catch ex As System.Net.Mail.SmtpException
                    '    .litError.Text = ex.ToString
                    'End Try

                    .pnlInicio.Enabled = False

                    Session("id_actividadM") = 106
                    Session("TipoM") = "SAP"
                    Server.Transfer("Menu.aspx")
                End While
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnRechazar_Click(sender As Object, e As EventArgs) Handles btnRechazar.Click
        With Me
            Try
                .litError.Text = ""

                If .txtComentario.Text.Trim = "" Then
                    .litError.Text = "Favor de ingresar los comentarios correspondientes"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    Dim fecha As DateTime = Date.Now
                    While Val(._txtBan.Text) = 0
                        'Actualizar datos de la Solicitud
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_ampliacion_p set fecha_autoriza = @fecha_autoriza, comentario_autoriza = @comentario_autoriza, status = 'Z' where id_ms_ampliacion_p = @id_ms_ampliacion_p "
                        SCMValores.Parameters.AddWithValue("@fecha_autoriza", fecha)
                        SCMValores.Parameters.AddWithValue("@comentario_autoriza", .txtComentario.Text.Trim)
                        SCMValores.Parameters.AddWithValue("@id_ms_ampliacion_p", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        ._txtBan.Text = 1

                        'Actualizar Instancia
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 108)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Registrar en Histórico
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                                                 "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 108)
                        SCMValores.Parameters.AddWithValue("@fecha", fecha)
                        SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()


                        Dim cabeceraM As String, cuerpoM As String, destinatario As String
                        destinatario = ._txtCorreo.Text

                        cabeceraM = "ProcAd - Solicitud de Ampliación No. " + .lblFolio.Text + " Rechazada"
                        cuerpoM = " Buen día:
                                 <br>
                                 <br> Por medio de la presente le informamos que la solicitud de <b> ampliación de presupuesto para gastos de viaje </b> 
                                 <br>
                                 con número de folio: <b>" + .lblFolio.Text + "</b> del sistema  <b>ProcAd</b> http://148.223.153.43/ProcAd ha sido <b>rechazada.</b>
                                 <br>
                                 <br>
                                  Comentarios: <b>" + txtComentario.Text + "</b>"
                        clsCorreo.enviarCorreo(cabeceraM, cuerpoM, destinatario)

                        ''Envío de Correo
                        'Dim Mensaje As New System.Net.Mail.MailMessage()
                        'Dim destinatario As String = ""
                        'destinatario = ._txtCorreo.Text

                        'Mensaje.[To].Add(destinatario)
                        'Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                        ''Mensaje.From = New System.Net.Mail.MailAddress("soporte.unne.procad@gmail.com", "ProcAd", System.Text.Encoding.UTF8)
                        'Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                        'Mensaje.Subject = "ProcAd - Solicitud de Ampliación No. " + .lblFolio.Text + " Rechazada"
                        'Dim texto As String
                        'texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                        '        "La solicitud número <b>" + .lblFolio.Text + "</b> fue rechazada.  <br>" + _
                        '        "Comentarios: <b>" + .txtComentario.Text.Trim + "</b><br></span>"
                        'Mensaje.Body = texto
                        'Mensaje.IsBodyHtml = True
                        'Mensaje.Priority = MailPriority.Normal

                        'Dim Servidor As New SmtpClient()
                        'Servidor.Host = "10.10.10.30"
                        'Servidor.Port = 587
                        'Servidor.EnableSsl = False
                        'Servidor.UseDefaultCredentials = False
                        'Servidor.Credentials = New System.Net.NetworkCredential("nprocad", "mc8HLB8lPe78")

                        'Try
                        '    Servidor.Send(Mensaje)
                        'Catch ex As System.Net.Mail.SmtpException
                        '    .litError.Text = ex.ToString
                        'End Try

                        .pnlInicio.Enabled = False

                        Session("id_actividadM") = 106
                        Session("TipoM") = "SAP"
                        Server.Transfer("Menu.aspx")
                    End While
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class