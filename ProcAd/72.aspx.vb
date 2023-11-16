Public Class _72
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 5
                    'Session("idMsInst") = 41111

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtIdMsInst.Text = Session("idMsInst")
                        ._txtBan.Text = 0

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        'Datos de la Evaluación
                        Dim sdaEvaluacion As New SqlDataAdapter
                        Dim dsEvaluacion As New DataSet
                        sdaEvaluacion.SelectCommand = New SqlCommand("select id_ms_evaluacionA " + _
                                                                     "     , ms_evaluacionA.area " + _
                                                                     "     , ms_evaluacionA.id_dt_area " + _
                                                                     "     , ms_evaluacionA.lider " + _
                                                                     "     , ms_evaluacionA.puesto_lider " + _
                                                                     "     , case ms_evaluacionA.mes_eval " + _
                                                                     "         when 1 then 'Enero' " + _
                                                                     "         when 2 then 'Febrero' " + _
                                                                     "         when 3 then 'Marzo' " + _
                                                                     "         when 4 then 'Abril' " + _
                                                                     "         when 5 then 'Mayo' " + _
                                                                     "         when 6 then 'Junio' " + _
                                                                     "         when 7 then 'Julio' " + _
                                                                     "         when 8 then 'Agosto' " + _
                                                                     "         when 9 then 'Septiembre' " + _
                                                                     "         when 10 then 'Octubre' " + _
                                                                     "         when 11 then 'Noviembre' " + _
                                                                     "         when 12 then 'Diciembre' " + _
                                                                     "         else '-' " + _
                                                                     "       end + ' ' + cast(ms_evaluacionA.año_eval as varchar(4)) as mes_eval " + _
                                                                     "     , isnull(com_registro, '') as com_registro " + _
                                                                     "     , isnull(com_valida, '') as com_valida " + _
                                                                     "from ms_instancia " + _
                                                                     "  left join ms_evaluacionA on ms_instancia.id_ms_sol = ms_evaluaciona.id_ms_evaluacionA and ms_instancia.tipo = 'EvalA' " + _
                                                                     "where ms_instancia.id_ms_instancia = @idMsInst ", ConexionBD)
                        sdaEvaluacion.SelectCommand.Parameters.AddWithValue("@idMsInst", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaEvaluacion.Fill(dsEvaluacion)
                        ConexionBD.Close()
                        ._txtIdMsEvaluacionA.Text = dsEvaluacion.Tables(0).Rows(0).Item("id_ms_evaluacionA").ToString()
                        ._txtIdDtArea.Text = dsEvaluacion.Tables(0).Rows(0).Item("id_dt_area").ToString()
                        .lblArea.Text = dsEvaluacion.Tables(0).Rows(0).Item("area").ToString()
                        .lblLider.Text = dsEvaluacion.Tables(0).Rows(0).Item("lider").ToString()
                        .lblPuestoLider.Text = dsEvaluacion.Tables(0).Rows(0).Item("puesto_lider").ToString()
                        .lblMesEval.Text = dsEvaluacion.Tables(0).Rows(0).Item("mes_eval").ToString()
                        .txtComentarioL.Text = dsEvaluacion.Tables(0).Rows(0).Item("com_registro").ToString()
                        .txtComentarioV.Text = dsEvaluacion.Tables(0).Rows(0).Item("com_valida").ToString()
                        sdaEvaluacion.Dispose()
                        dsEvaluacion.Dispose()

                        'Evaluaciones Autorizadas
                        Dim sdaEvaluaciones As New SqlDataAdapter
                        Dim dsEvaluaciones As New DataSet
                        .gvEvaluaciones.DataSource = dsEvaluaciones
                        sdaEvaluaciones.SelectCommand = New SqlCommand("select no_empleado " + _
                                                                       "     , nombre + ' ' + ap_paterno + ' ' + ap_materno as colaborador " + _
                                                                       "     , empresa " + _
                                                                       "     , centro_costo " + _
                                                                       "     , puesto " + _
                                                                       "     , base " + _
                                                                       "     , porcent_cumpl " + _
                                                                       "     , case cobra_bono_asist when 'S' then 'Sí' when 'N' then 'No' else '-' end as cobra_bono_asist " + _
                                                                       "from ms_evaluacionA " + _
                                                                       "  left join ms_evaluacion on ms_evaluacionA.id_ms_evaluacionA = ms_evaluacion.id_ms_evaluacionA " + _
                                                                       "where ms_evaluacionA.id_ms_evaluacionA = @id_ms_evaluacionA " + _
                                                                       "order by colaborador ", ConexionBD)
                        sdaEvaluaciones.SelectCommand.Parameters.AddWithValue("@id_ms_evaluacionA", Val(._txtIdMsEvaluacionA.Text))
                        ConexionBD.Open()
                        sdaEvaluaciones.Fill(dsEvaluaciones)
                        .gvEvaluaciones.DataBind()
                        ConexionBD.Close()
                        sdaEvaluaciones.Dispose()
                        dsEvaluaciones.Dispose()

                        'Evidencias
                        Dim sdaEvidencias As New SqlDataAdapter
                        Dim dsEvidencias As New DataSet
                        .gvEvidencia.DataSource = dsEvidencias
                        sdaEvidencias.SelectCommand = New SqlCommand("select evidencia " + _
                                                                     "     , 'http://148.223.153.43/ProcAd - Evidencias/' + cast(id_dt_evaluacion_evid as varchar(20)) + 'EvalE-' + evidencia as path " + _
                                                                     "from dt_evaluacion_evid " + _
                                                                     "where id_ms_evaluacionA = @id_ms_evaluacionA ", ConexionBD)
                        sdaEvidencias.SelectCommand.Parameters.AddWithValue("@id_ms_evaluacionA", Val(._txtIdMsEvaluacionA.Text))
                        ConexionBD.Open()
                        sdaEvidencias.Fill(dsEvidencias)
                        .gvEvidencia.DataBind()
                        ConexionBD.Close()
                        sdaEvidencias.Dispose()
                        dsEvidencias.Dispose()
                        If .gvEvidencia.Rows.Count > 0 Then
                            .lbl_Evidencias.Visible = True
                            .gvEvidencia.Visible = True
                        Else
                            .lbl_Evidencias.Visible = False
                            .gvEvidencia.Visible = False
                        End If
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

    Protected Sub btnAutorizar_Click(sender As Object, e As EventArgs) Handles btnAutorizar.Click
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim fecha As DateTime = Date.Now
                While Val(._txtBan.Text) = 0
                    'Actualizar Estatus de las Evaluaciones
                    SCMValores.CommandText = "update ms_evaluacion  " + _
                                             "  set id_usr_valida = @id_usr_valida, fecha_valida = @fecha_valida, status = 'PAD' " + _
                                             "where id_dt_area = @id_dt_area " + _
                                             "  and status = 'PVA' " + _
                                             "  and mes_eval = month((select cast(valor as datetime) " + _
                                             "                        from cg_parametros " + _
                                             "                        where parametro = 'mes_eval')) " + _
                                             "  and año_eval = year((select cast(valor as datetime) " + _
                                             "                       from cg_parametros " + _
                                             "                       where parametro = 'mes_eval')) "
                    SCMValores.Parameters.AddWithValue("@fecha_valida", fecha)
                    SCMValores.Parameters.AddWithValue("@id_usr_valida", Val(._txtIdUsuario.Text))
                    SCMValores.Parameters.AddWithValue("@id_dt_area", Val(._txtIdDtArea.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Actualizar datos de la Evaluación de Área
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_evaluacionA set id_usr_valida = @id_usr_valida, fecha_valida = @fecha_valida, com_valida = @com_valida, status = 'PAD' where id_ms_evaluacionA = @id_ms_evaluacionA "
                    If .txtComentarioV.Text.Trim = "" Then
                        SCMValores.Parameters.AddWithValue("@com_valida", DBNull.Value)
                    Else
                        SCMValores.Parameters.AddWithValue("@com_valida", .txtComentarioV.Text.Trim)
                    End If
                    SCMValores.Parameters.AddWithValue("@fecha_valida", fecha)
                    SCMValores.Parameters.AddWithValue("@id_usr_valida", Val(._txtIdUsuario.Text))
                    SCMValores.Parameters.AddWithValue("@id_ms_evaluacionA", Val(._txtIdMsEvaluacionA.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Actualizar Instancia
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 73)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Registrar en Histórico
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                             "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 73)
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    ._txtBan.Text = 1

                    ' ''Envío de Correo
                    ''Dim Mensaje As New System.Net.Mail.MailMessage()
                    ''Dim destinatario As String = ""
                    ' ''Obtener el Correo del Director
                    ''SCMValores.CommandText = "select correo " + _
                    ''                         "from ms_evaluacionA " + _
                    ''                         "  left join cg_usuario on ms_evaluacionA.id_usr_director = cg_usuario.id_usuario " + _
                    ''                         "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                    ''                         "where id_ms_evaluacionA = @id_ms_evaluacionA "
                    ''SCMValores.Parameters.AddWithValue("@id_ms_evaluacionA", Val(._txtIdMsEvaluacionA.Text))
                    ''ConexionBD.Open()
                    ''destinatario = SCMValores.ExecuteScalar()
                    ''ConexionBD.Close()

                    ''Mensaje.[To].Add(destinatario)
                    ''Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                    ''Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                    ''Mensaje.Subject = "ProcAd - Evaluaciones del Área Validadas"
                    ''Dim texto As String
                    ''texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                    ''        "Las evaluaciones del área " + .lblArea.Text + " fueron validadas. <br>" + _
                    ''        "Favor de ingresar a la actividad <b>Autorizar Evaluaciones</b> </span>"
                    ''Mensaje.Body = texto
                    ''Mensaje.IsBodyHtml = True
                    ''Mensaje.Priority = MailPriority.Normal

                    ''Dim Servidor As New SmtpClient()
                    ''Servidor.Host = "10.10.10.30"
                    ''Servidor.Port = 587
                    ''Servidor.EnableSsl = False
                    ''Servidor.UseDefaultCredentials = False
                    ''Servidor.Credentials = New System.Net.NetworkCredential("nprocad", "mc8HLB8lPe78")
                    ''Try
                    ''    Servidor.Send(Mensaje)
                    ''Catch ex As System.Net.Mail.SmtpException
                    ''    .litError.Text = ex.ToString
                    ''End Try

                    .btnAutorizar.Enabled = False
                    .btnRechazar.Enabled = False

                    Session("id_actividadM") = 72
                    Session("TipoM") = "Eval"
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

                If .txtComentarioV.Text.Trim = "" Then
                    .litError.Text = "Favor de ingresar los comentarios correspondientes"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    Dim fecha As DateTime = Date.Now
                    While Val(._txtBan.Text) = 0
                        'Actualizar Estatus de las Evaluaciones
                        SCMValores.CommandText = "update ms_evaluacion  " + _
                                                 "  set status = 'PCA' " + _
                                                 "where id_dt_area = @id_dt_area " + _
                                                 "  and status = 'PVA' " + _
                                                 "  and mes_eval = month((select cast(valor as datetime) " + _
                                                 "                        from cg_parametros " + _
                                                 "                        where parametro = 'mes_eval')) " + _
                                                 "  and año_eval = year((select cast(valor as datetime) " + _
                                                 "                       from cg_parametros " + _
                                                 "                       where parametro = 'mes_eval')) "
                        SCMValores.Parameters.AddWithValue("@id_dt_area", Val(._txtIdDtArea.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Actualizar datos de la Evaluación de Área
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_evaluacionA set com_valida = @com_valida, status = 'PCA' where id_ms_evaluacionA = @id_ms_evaluacionA "
                        SCMValores.Parameters.AddWithValue("@com_valida", .txtComentarioV.Text.Trim)
                        SCMValores.Parameters.AddWithValue("@id_ms_evaluacionA", Val(._txtIdMsEvaluacionA.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Actualizar Instancia
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 75)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Registrar en Histórico
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                                 "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 75)
                        SCMValores.Parameters.AddWithValue("@fecha", fecha)
                        SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        ._txtBan.Text = 1

                        ' ''Envío de Correo
                        ''Dim Mensaje As New System.Net.Mail.MailMessage()
                        ''Dim destinatario As String = ""
                        ' ''Obtener el Correo del Autorizador
                        ''SCMValores.CommandText = "select correo " + _
                        ''                         "from dt_area " + _
                        ''                         "  left join cg_usuario on dt_area.id_usr_aut = cg_usuario.id_usuario " + _
                        ''                         "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                        ''                         "where id_dt_area = @id_dt_area "
                        ''SCMValores.Parameters.AddWithValue("@id_dt_area", Val(._txtIdDtArea.Text))
                        ''ConexionBD.Open()
                        ''destinatario = SCMValores.ExecuteScalar()
                        ''ConexionBD.Close()

                        ''Mensaje.[To].Add(destinatario)
                        ''Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                        ''Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                        ''Mensaje.Subject = "ProcAd - Evaluaciones del Área Rechazadas"
                        ''Dim texto As String
                        ''texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                        ''        "Las evaluaciones del área no fueron validadas. <br>" + _
                        ''        "Favor de ingresar a la actividad <b>Corregir Evaluación del Área</b> </span>"
                        ''Mensaje.Body = texto
                        ''Mensaje.IsBodyHtml = True
                        ''Mensaje.Priority = MailPriority.Normal

                        ''Dim Servidor As New SmtpClient()
                        ''Servidor.Host = "10.10.10.30"
                        ''Servidor.Port = 587
                        ''Servidor.EnableSsl = False
                        ''Servidor.UseDefaultCredentials = False
                        ''Servidor.Credentials = New System.Net.NetworkCredential("nprocad", "mc8HLB8lPe78")
                        ''Try
                        ''    Servidor.Send(Mensaje)
                        ''Catch ex As System.Net.Mail.SmtpException
                        ''    .litError.Text = ex.ToString
                        ''End Try

                        .btnAutorizar.Enabled = False
                        .btnRechazar.Enabled = False

                        Session("id_actividadM") = 72
                        Session("TipoM") = "Eval"
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