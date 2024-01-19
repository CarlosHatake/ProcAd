Public Class _143
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtIdMsInst.Text = Session("idMsInst")
                        ._txtBan.Text = 0

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim sdaSol As New SqlDataAdapter
                        Dim dsSol As New DataSet
                        sdaSol.SelectCommand = New SqlCommand("select " +
                                                              " empleado_solicita, MCA.empresa, id_ms_comprobacion_anticipo as folio, " +
                                                              "  MCA.autorizador, MCA.tipo_escenario, " +
                                                              "  MCA.id_usr_autorizador2, MCA.id_usr_autorizador3, " +
                                                              "  isnull(cgEmpl2.nombre + ' ' + cgEmpl2.ap_paterno + ' ' + cgEmpl2.ap_materno, 'XX') as autorizador2, " +
                                                              "  isnull(cgEmpl3.nombre + ' ' + cgEmpl3.ap_paterno + ' ' + cgEmpl3.ap_materno, 'XX') as autorizador3, " +
                                                              "  proveedor, division, centro_costos " +
                                                              "  from ms_comprobacion_anticipo MCA " +
                                                              "  inner join ms_instancia MSA on MCA.id_ms_comprobacion_anticipo = MSA.id_ms_sol and Tipo = 'CAP' " +
                                                              "  left join cg_usuario cgAut2 on MCA.id_usr_autorizador2 = cgAut2.id_usuario " +
                                                              "  left join bd_Empleado.dbo.cg_empleado cgEmpl2 on cgAut2.id_empleado = cgEmpl2.id_empleado " +
                                                              "  left join cg_usuario cgAut3 on MCA.id_usr_autorizador3 = cgAut3.id_usuario " +
                                                              "  left join bd_Empleado.dbo.cg_empleado cgEmpl3 on cgAut3.id_empleado = cgEmpl3.id_empleado " +
                                                              "  where MSA.id_ms_instancia = @id_ms_instancia ", ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()
                        .lblFolio.Text = dsSol.Tables(0).Rows(0).Item("folio").ToString()
                        .lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("empleado_solicita").ToString()
                        .lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                        .lblCC.Text = dsSol.Tables(0).Rows(0).Item("centro_costos").ToString()
                        .lblDivision.Text = dsSol.Tables(0).Rows(0).Item("division").ToString()
                        .lblProveedor.Text = dsSol.Tables(0).Rows(0).Item("proveedor").ToString()
                        .lblAutorizador.Text = dsSol.Tables(0).Rows(0).Item("autorizador").ToString()
                        ._txtIdAutorizador2.Text = dsSol.Tables(0).Rows(0).Item("id_usr_autorizador2").ToString()
                        ._txtIdAutorizador3.Text = dsSol.Tables(0).Rows(0).Item("id_usr_autorizador3").ToString()
                        If dsSol.Tables(0).Rows(0).Item("autorizador2").ToString() = "XX" Then
                            .lbl_Autorizador2.Visible = False
                            .lblAutorizador2.Visible = False
                        Else
                            .lbl_Autorizador2.Visible = True
                            .lblAutorizador2.Visible = True
                            .lblAutorizador2.Text = dsSol.Tables(0).Rows(0).Item("autorizador2").ToString()
                        End If
                        If dsSol.Tables(0).Rows(0).Item("autorizador3").ToString() = "XX" Then
                            .lbl_Autorizador3.Visible = False
                            .lblAutorizador3.Visible = False
                        Else
                            .lbl_Autorizador3.Visible = True
                            .lblAutorizador3.Visible = True
                            .lblAutorizador3.Text = dsSol.Tables(0).Rows(0).Item("autorizador3").ToString()
                        End If
                        sdaSol.Dispose()
                        dsSol.Dispose()



                        'Facturas
                        Dim sdaFacturas As New SqlDataAdapter
                        Dim dsFacturas As New DataSet
                        .gvFacturas.DataSource = dsFacturas
                        'Habilitar columnas para actualización
                        .gvFacturas.Columns(0).Visible = True
                        sdaFacturas.SelectCommand = New SqlCommand("select id_dt_comprobacion_anticipo " +
                                                           "     , fecha_emision " +
                                                           "     , uuid " +
                                                           "     , lugar_exp " +
                                                           "     , forma_pago " +
                                                           "     , moneda " +
                                                           "     , subtotal " +
                                                           "     , importe " +
                                                           "from dt_comprobacion_anticipo " +
                                                           "where id_ms_comp_anticipo = @id_ms_comp_anticipo", ConexionBD)
                        sdaFacturas.SelectCommand.Parameters.AddWithValue("@id_ms_comp_anticipo", Val(lblFolio.Text))
                        ConexionBD.Open()
                        sdaFacturas.Fill(dsFacturas)
                        .gvFacturas.DataBind()
                        ConexionBD.Close()
                        sdaFacturas.Dispose()
                        dsFacturas.Dispose()
                        .gvFacturas.SelectedIndex = -1
                        'Inhabilitar columnas para vista
                        .gvFacturas.Columns(0).Visible = False

                        'Evidencias
                        Dim sdaEvidencias As New SqlDataAdapter
                        Dim dsEvidencias As New DataSet
                        .gvEvidencias.DataSource = dsEvidencias
                        'Evidencias
                        sdaEvidencias.SelectCommand = New SqlCommand("select nombre as archivo " +
                                                                   "     , 'http://148.223.153.43/ProcAd - Adjuntos IngFact/' + cast(id_dt_archivo_adj_comp_anticipo as varchar(20)) + '-' + nombre as path " +
                                                                   "from dt_archivo_adj_comp_anticipo " +
                                                                   "where id_ms_comp_anticipo_proveedor = @id_ms_comp_anticipo_proveedor", ConexionBD)
                        sdaEvidencias.SelectCommand.Parameters.AddWithValue("@id_ms_comp_anticipo_proveedor", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        sdaEvidencias.Fill(dsEvidencias)
                        .gvEvidencias.DataBind()
                        ConexionBD.Close()
                        sdaEvidencias.Dispose()
                        dsEvidencias.Dispose()
                        .gvEvidencias.SelectedIndex = -1


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

                    'Conocer el estatus para la solicitud para envio de correo'
                    Dim estatusCancelacion As String
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "select case " +
                                             "         when (id_usr_autoriza is not null and fecha_autoriza is null) then 'ZA' " +
                                             "         when (id_usr_autorizador2 is not null and fecha_autoriza_2 is null) then 'ZD'  " +
                                             "         when (id_usr_autorizador3 is not null and fecha_autoriza_3 is null) then 'ZC' " +
                                             "         else 'AF' " +
                                             "       end as idAct " +
                                             "from ms_comprobacion_anticipo " +
                                             "where id_ms_comprobacion_anticipo = @id_ms_comprobacion_anticipo "
                    SCMValores.Parameters.AddWithValue("@id_ms_comprobacion_anticipo", Val(.lblFolio.Text))
                    ConexionBD.Open()
                    estatusCancelacion = SCMValores.ExecuteScalar()
                    ConexionBD.Close()

                    'Actualizar datos de la Solicitud
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_comprobacion_anticipo set fecha_autoriza = @fecha_autoriza, fecha_autoriza_2 = @fecha_autoriza2, fecha_autoriza_3 = @fecha_autoriza3,  estatus = @status where id_ms_comprobacion_anticipo = @id_ms_comprobacion_anticipo "

                    If estatusCancelacion = "ZA" Then
                        SCMValores.Parameters.AddWithValue("@fecha_autoriza", fecha)
                    Else
                        SCMValores.Parameters.AddWithValue("@fecha_autoriza", DBNull.Value)

                    End If
                    If estatusCancelacion = "ZD" Then
                        SCMValores.Parameters.AddWithValue("@fecha_autoriza2", fecha)
                    Else
                        SCMValores.Parameters.AddWithValue("@fecha_autoriza2", DBNull.Value)

                    End If
                    If estatusCancelacion = "ZC" Then
                        SCMValores.Parameters.AddWithValue("@fecha_autoriza3", fecha)
                    Else
                        SCMValores.Parameters.AddWithValue("@fecha_autoriza3", DBNull.Value)
                    End If

                    If estatusCancelacion = "AF" Then
                        SCMValores.Parameters.AddWithValue("@status", "AF")
                    Else
                        SCMValores.Parameters.AddWithValue("@status", "A")
                    End If

                    SCMValores.Parameters.AddWithValue("@id_ms_comprobacion_anticipo", Val(.lblFolio.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    ._txtBan.Text = 1

                    Dim idActividad As Integer
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "select case " +
                                             "         when (id_usr_autoriza is not null and fecha_autoriza is null) then 143 " +
                                             "         when (id_usr_autorizador2 is not null and fecha_autoriza_2 is null) then 143  " +
                                             "         when (id_usr_autorizador3 is not null and fecha_autoriza_3 is null) then 143 " +
                                             "         else 149 " +
                                             "       end as idAct " +
                                             "from ms_comprobacion_anticipo " +
                                             "where id_ms_comprobacion_anticipo = @id_ms_comprobacion_anticipo "
                    SCMValores.Parameters.AddWithValue("@id_ms_comprobacion_anticipo", Val(.lblFolio.Text))
                    ConexionBD.Open()
                    idActividad = SCMValores.ExecuteScalar()
                    ConexionBD.Close()

                    If idActividad = 149 Then
                        'Actualizar Instancia
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", idActividad)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()
                    End If

                    'Registrar en Histórico
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                                             "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", idActividad)
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()


                    If estatusCancelacion <> "AF" Or estatusCancelacion <> "ZA" Then
                        'Envío de Correo
                        Dim Mensaje As New System.Net.Mail.MailMessage()
                        Dim destinatario As String = ""
                        SCMValores.CommandText = "select cgEmpl.correo " +
                                                 "from cg_usuario " +
                                                 "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                 "where cg_usuario.id_usuario = @idAut "
                        Select Case estatusCancelacion
                            Case "ZD"
                                'Segundo Autorizador
                                SCMValores.Parameters.AddWithValue("@idAut", Val(._txtIdAutorizador2.Text))
                            Case "ZC"
                                'Tercer Autorizador
                                SCMValores.Parameters.AddWithValue("@idAut", Val(._txtIdAutorizador3.Text))
                        End Select
                        'Obtener el Correos del Autorizador
                        ConexionBD.Open()
                        destinatario = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        Mensaje.[To].Add(destinatario)
                        Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                        Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                        Mensaje.Subject = "ProcAd - Solicitud de Comprobación Anticipo No. " + .lblFolio.Text + " por Autorizar"
                        Dim texto As String
                        texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                                "Se ingresó la solicitud número <b>" + .lblFolio.Text +
                                "</b> por parte de <b>" + .lblSolicitante.Text +
                                "</b><br><br>Favor de determinar si procede </span>"
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
                    End If

                    .pnlInicio.Enabled = False

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
                        'Conocer el estatus para la solicitud a cancelar'
                        Dim estatusCancelacion As String
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "select case " +
                                             "         when (id_usr_autoriza is not null and fecha_autoriza is null) then 'ZA' " +
                                             "         when (id_usr_autorizador2 is not null and fecha_autoriza_2 is null) then 'ZD'  " +
                                             "         when (id_usr_autorizador3 is not null and fecha_autoriza_3 is null) then 'ZC' " +
                                             "       end as idAct " +
                                             "from ms_comprobacion_anticipo " +
                                             "where id_ms_comprobacion_anticipo = @id_ms_comprobacion_anticipo "
                        SCMValores.Parameters.AddWithValue("@id_ms_comprobacion_anticipo", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        estatusCancelacion = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        'Actualizar datos de la Solicitud
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_factura set fecha_autoriza = @fecha_autoriza, fecha_autoriza2 = @fecha_autoriza_2, fecha_autoriza_3 = @fecha_autoriza_3, estatus = @status where id_ms_comprobacion_anticipo = @id_ms_comprobacion_anticipo "
                        'SCMValores.Parameters.AddWithValue("@fecha_autoriza", fecha)
                        'SCMValores.Parameters.AddWithValue("@comentario_autoriza", .txtComentario.Text.Trim)
                        SCMValores.Parameters.AddWithValue("@id_ms_comprobacion_anticipo", Val(.lblFolio.Text))
                        SCMValores.Parameters.AddWithValue("@status", estatusCancelacion)
                        If estatusCancelacion = "ZA" Then
                            SCMValores.Parameters.AddWithValue("@fecha_autoriza", Date.Now)
                        ElseIf estatusCancelacion = "ZD" Then
                            SCMValores.Parameters.AddWithValue("@fecha_autoriza_2", Date.Now)
                        ElseIf estatusCancelacion = "ZC" Then
                            SCMValores.Parameters.AddWithValue("@fecha_autoriza_3", Date.Now)
                        End If
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        ._txtBan.Text = 1

                        Dim idActividad As Integer = 144

                        'Actualizar Instancia
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", idActividad)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Registrar en Histórico
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                                                 "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", idActividad)
                        SCMValores.Parameters.AddWithValue("@fecha", fecha)
                        SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Envío de Correo
                        Dim Mensaje As New System.Net.Mail.MailMessage()
                        Dim destinatario As String = ""
                        'Obtener el Correo del Solicitante
                        SCMValores.CommandText = "select cgEmpl.correo " +
                                                 "from ms_factura " +
                                                 "  left join cg_usuario on ms_factura.id_usr_solicita = cg_usuario.id_usuario " +
                                                 "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                 "where id_ms_factura = @id_ms_factura "
                        SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        destinatario = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        Mensaje.[To].Add(destinatario)
                        Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                        Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                        Dim texto As String
                        Mensaje.Subject = "ProcAd - Solicitud de Comprobacion Anticipo. " + .lblFolio.Text + " Rechazada"
                        texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                                    "La solicitud número <b>" + .lblFolio.Text + "</b> fue rechazada <br></span>"
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

                        .pnlInicio.Enabled = False

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