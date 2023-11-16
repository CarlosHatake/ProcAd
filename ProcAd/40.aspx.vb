Public Class _40
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 3
                    'Session("idMsInst") = 1

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtIdMsInst.Text = Session("idMsInst")
                        ._txtBan.Text = 0
                        Session("Error") = ""

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim sdaSol As New SqlDataAdapter
                        Dim dsSol As New DataSet
                        Dim query As String
                        query = "select ms_comb.id_ms_comb " +
                                "     , ms_comb.empleado " +
                                "     , ms_comb.autorizador " +
                                "     , ms_comb.destino " +
                                "     , ms_comb.empresa " +
                                "     , ms_comb.actividad as just " +
                                "     , convert(varchar, ms_comb.periodo_ini, 103) as periodo_ini " +
                                "     , convert(varchar, ms_comb.periodo_fin, 103) as periodo_fin " +
                                "     , ms_comb.no_eco as no_eco_comb " +
                                "     , ms_comb.no_tarjeta_edenred " +
                                "     , isnull(format(ms_comb.litros_comb, 'N0', 'es-MX'), '') as litros_comb " +
                                "     , isnull(format(ms_comb.importe_comb, 'C2', 'es-MX'), '') as importe_comb " +
                                "from ms_instancia " +
                                "  left join ms_comb on ms_instancia.id_ms_sol = ms_comb.id_ms_comb and ms_instancia.tipo = 'Comb' " +
                                "where id_ms_instancia = @idMsInst "
                        sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@idMsInst", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()
                        .lblFolio.Text = dsSol.Tables(0).Rows(0).Item("id_ms_comb").ToString()
                        .lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("empleado").ToString()
                        .lblAutorizador.Text = dsSol.Tables(0).Rows(0).Item("autorizador").ToString()
                        .lblDestino.Text = dsSol.Tables(0).Rows(0).Item("destino").ToString()
                        .lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                        .txtJust.Text = dsSol.Tables(0).Rows(0).Item("just").ToString()
                        .lblPeriodoIni.Text = dsSol.Tables(0).Rows(0).Item("periodo_ini").ToString()
                        .lblPeriodoFin.Text = dsSol.Tables(0).Rows(0).Item("periodo_fin").ToString()
                        .lblVehiculoC.Text = dsSol.Tables(0).Rows(0).Item("no_eco_comb").ToString()
                        .lblTarjEdenred.Text = dsSol.Tables(0).Rows(0).Item("no_tarjeta_edenred").ToString()
                        .lblLitros.Text = dsSol.Tables(0).Rows(0).Item("litros_comb").ToString()
                        .lblImporte.Text = dsSol.Tables(0).Rows(0).Item("importe_comb").ToString()

                        sdaSol.Dispose()
                        dsSol.Dispose()

                        'Panel
                        .pnlInicio.Visible = True
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

    Protected Sub btnDispersar_Click(sender As Object, e As EventArgs) Handles btnDispersar.Click
        With Me
            Try
                .litError.Text = ""
                If Session("Error") = "" Then
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    Dim fecha As DateTime = Date.Now
                    While Val(._txtBan.Text) = 0
                        'Actualizar datos de la Solicitud de Combustible

                        Dim valor As Integer = 0
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "DECLARE @valorR int;  Execute Sp_U_DispGas @id_usr_dispersion, @fecha_dispersion,   @obs_dispersion , @id_ms_comb , @id_actividad , @id_ms_instancia ,   @valorR OUTPUT; select @valorR"
                        SCMValores.Parameters.AddWithValue("@id_usr_dispersion", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@fecha_dispersion", fecha)
                        If .txtObs.Text.Trim = "" Then
                            SCMValores.Parameters.AddWithValue("@obs_dispersion", DBNull.Value)
                        Else
                            SCMValores.Parameters.AddWithValue("@obs_dispersion", .txtObs.Text.Trim)
                        End If
                        SCMValores.Parameters.AddWithValue("@id_ms_comb", Val(.lblFolio.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 41)
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        valor = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        If valor = 0 Then
                            Server.Transfer("Menu.aspx")
                        End If
                        SCMValores.Parameters.Clear()

                        'SCMValores.CommandText = ""
                        'SCMValores.Parameters.Clear()
                        'SCMValores.CommandText = "update ms_comb set id_usr_dispersion = @id_usr_dispersion, fecha_dispersion = @fecha_dispersion, obs_dispersion = @obs_dispersion, status = 'D' where id_ms_comb = @id_ms_comb "
                        'SCMValores.Parameters.AddWithValue("@id_usr_dispersion", Val(._txtIdUsuario.Text))
                        'SCMValores.Parameters.AddWithValue("@fecha_dispersion", fecha)
                        'If .txtObs.Text.Trim = "" Then
                        '    SCMValores.Parameters.AddWithValue("@obs_dispersion", DBNull.Value)
                        'Else
                        '    SCMValores.Parameters.AddWithValue("@obs_dispersion", .txtObs.Text.Trim)
                        'End If
                        'SCMValores.Parameters.AddWithValue("@id_ms_comb", Val(.lblFolio.Text))
                        'ConexionBD.Open()
                        'SCMValores.ExecuteNonQuery()
                        'ConexionBD.Close()

                        ''Actualizar Instancia
                        'SCMValores.Parameters.Clear()
                        'SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                        'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        'SCMValores.Parameters.AddWithValue("@id_actividad", 41)
                        'ConexionBD.Open()
                        'SCMValores.ExecuteNonQuery()
                        'ConexionBD.Close()

                        ''Registrar en Histórico
                        'SCMValores.Parameters.Clear()
                        'SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                        '                         "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                        'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        'SCMValores.Parameters.AddWithValue("@id_actividad", 41)
                        'SCMValores.Parameters.AddWithValue("@fecha", fecha)
                        'SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        'ConexionBD.Open()
                        'SCMValores.ExecuteNonQuery()
                        'ConexionBD.Close()

                        ._txtBan.Text = 1

                        'Envío de Correo
                        Dim Mensaje As New System.Net.Mail.MailMessage()
                        Dim destinatario As String = ""
                        'Obtener el Correo del Solicitante
                        SCMValores.CommandText = "select cgEmpl.correo " +
                                                 "from ms_comb " +
                                                 "  left join cg_usuario on ms_comb.id_usr_solicita = cg_usuario.id_usuario " +
                                                 "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                 "where id_ms_comb = @id_ms_comb "
                        SCMValores.Parameters.AddWithValue("@id_ms_comb", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        destinatario = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        Mensaje.[To].Add(destinatario)
                        Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                        Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                        Mensaje.Subject = "ProcAd - Carga de Combustible No. " + .lblFolio.Text + " Aplicada"
                        Dim texto As String
                        texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                                "La Carga de Combustible número <b>" + .lblFolio.Text +
                                "</b> fue aplicada <br></span>"
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

                        .txtObs.Enabled = False
                        .btnDispersar.Enabled = False
                        .btnCancelar.Enabled = False
                    End While
                Else
                    Server.Transfer("Menu.aspx")
                End If
            Catch ex As Exception
                If ex.Message = "Subproceso anulado." Then
                    Session("Error") = "S"
                End If
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        With Me
            Try
                .litError.Text = ""
                If Session("Error") = "" Then
                    If .txtObs.Text.Trim = "" Then
                        .litError.Text = "Favor de Ingresar las observaciones correspondientes"
                    Else
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        Dim fecha As DateTime = Date.Now
                        While Val(._txtBan.Text) = 0
                            'Actualizar datos de la Solicitud de Combustible

                            Dim valor As Integer = 0
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "DECLARE @valorR int;  Execute Sp_U_DispGas @id_usr_dispersion, @fecha_dispersion,   @obs_dispersion, @id_ms_comb , @id_actividad , @id_ms_instancia , @valorR OUTPUT; select @valorR"
                            SCMValores.Parameters.AddWithValue("@id_usr_dispersion", Val(._txtIdUsuario.Text))
                            SCMValores.Parameters.AddWithValue("@fecha_dispersion", fecha)
                            SCMValores.Parameters.AddWithValue("@obs_dispersion", .txtObs.Text.Trim)
                            SCMValores.Parameters.AddWithValue("@id_ms_comb", Val(.lblFolio.Text))
                            SCMValores.Parameters.AddWithValue("@id_actividad", 42)
                            SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                            ConexionBD.Open()
                            valor = SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            If valor = 0 Then
                                Server.Transfer("Menu.aspx")
                            End If
                            SCMValores.Parameters.Clear()

                            'SCMValores.CommandText = ""
                            'SCMValores.Parameters.Clear()
                            'SCMValores.CommandText = "update ms_comb set id_usr_dispersion = @id_usr_dispersion, fecha_dispersion = @fecha_dispersion, obs_dispersion = @obs_dispersion, status = 'ZC' where id_ms_comb = @id_ms_comb "
                            'SCMValores.Parameters.AddWithValue("@id_usr_dispersion", Val(._txtIdUsuario.Text))
                            'SCMValores.Parameters.AddWithValue("@fecha_dispersion", fecha)
                            'SCMValores.Parameters.AddWithValue("@obs_dispersion", .txtObs.Text.Trim)
                            'SCMValores.Parameters.AddWithValue("@id_ms_comb", Val(.lblFolio.Text))
                            'ConexionBD.Open()
                            'SCMValores.ExecuteNonQuery()
                            'ConexionBD.Close()

                            ''Actualizar Instancia
                            'SCMValores.Parameters.Clear()
                            'SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                            'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                            'SCMValores.Parameters.AddWithValue("@id_actividad", 42)
                            'ConexionBD.Open()
                            'SCMValores.ExecuteNonQuery()
                            'ConexionBD.Close()

                            ''Registrar en Histórico
                            'SCMValores.Parameters.Clear()
                            'SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                            '                         "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                            'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                            'SCMValores.Parameters.AddWithValue("@id_actividad", 42)
                            'SCMValores.Parameters.AddWithValue("@fecha", fecha)
                            'SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                            'ConexionBD.Open()
                            'SCMValores.ExecuteNonQuery()
                            'ConexionBD.Close()

                            ._txtBan.Text = 1

                            'Envío de Correo
                            Dim Mensaje As New System.Net.Mail.MailMessage()
                            Dim destinatario As String = ""
                            'Obtener el Correo del Solicitante
                            SCMValores.CommandText = "select cgEmpl.correo " +
                                                     "from ms_comb " +
                                                     "  left join cg_usuario on ms_comb.id_usr_solicita = cg_usuario.id_usuario " +
                                                     "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                     "where id_ms_comb = @id_ms_comb "
                            SCMValores.Parameters.AddWithValue("@id_ms_comb", Val(.lblFolio.Text))
                            ConexionBD.Open()
                            destinatario = SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            Mensaje.[To].Add(destinatario)
                            Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                            Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                            Mensaje.Subject = "ProcAd - Carga de Combustible No. " + .lblFolio.Text + " Rechazada"
                            Dim texto As String
                            texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                                    "La Carga de Combustible número <b>" + .lblFolio.Text +
                                    "</b> fue cancelada con las siguientes observaciones:" +
                                    "<br> " + .txtObs.Text.Trim + "<br></span>"
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

                            .txtObs.Enabled = False
                            .btnDispersar.Enabled = False
                            .btnCancelar.Enabled = False
                        End While
                    End If
                Else
                    Server.Transfer("Menu.aspx")
                End If
            Catch ex As Exception
                If ex.Message = "Subproceso anulado." Then
                    Session("Error") = "S"
                End If
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class