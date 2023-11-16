Public Class _55
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 5
                    'Session("idMsInst") = 33461

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtIdMsInst.Text = Session("idMsInst")
                        ._txtBan.Text = 0

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim sdaSol As New SqlDataAdapter
                        Dim dsSol As New DataSet
                        Dim query As String
                        query = "select ms_contrato.id_ms_factura " + _
                                "     , ms_contrato.empleado " + _
                                "     , ms_contrato.empresa " + _
                                "     , ms_factura.fecha_solicita " + _
                                "     , ms_contrato.tipo_servicio " + _
                                "     , ms_factura.validador " + _
                                "     , ms_contrato.proveedor " + _
                                "     , ms_contrato.base " + _
                                "     , convert(varchar, ms_contrato.fecha_servicio_ini, 103) as fecha_ini " + _
                                "     , convert(varchar, ms_contrato.fecha_servicio_fin, 103) as fecha_ter " + _
                                "     , ms_contrato.periodicidad " + _
                                "     , format(ms_contrato.monto_periodo, 'C', 'es-MX') as monto_periodo " + _
                                "     , format(ms_contrato.monto_contrato, 'C', 'es-MX') as monto_contrato " + _
                                "     , ms_contrato.autorizador " + _
                                "     , ms_contrato.lugar_ejecucion " + _
                                "     , ms_contrato.descrip_servicio " + _
                                "from ms_factura " + _
                                "  left join ms_instancia on ms_factura.id_ms_factura = ms_instancia.id_ms_sol and ms_instancia.tipo = 'F' " + _
                                "  left join ms_contrato on ms_factura.id_ms_factura = ms_contrato.id_ms_factura " + _
                                "where id_ms_instancia = @idMsInst "
                        sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@idMsInst", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()
                        .lblFolio.Text = dsSol.Tables(0).Rows(0).Item("id_ms_factura").ToString()
                        .lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("empleado").ToString()
                        .lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                        .lblFechaSol.Text = dsSol.Tables(0).Rows(0).Item("fecha_solicita").ToString()
                        .lblTipoServicio.Text = dsSol.Tables(0).Rows(0).Item("tipo_servicio").ToString()
                        .lblValidador.Text = dsSol.Tables(0).Rows(0).Item("validador").ToString()
                        .lblProveedor.Text = dsSol.Tables(0).Rows(0).Item("proveedor").ToString()
                        .lblBase.Text = dsSol.Tables(0).Rows(0).Item("base").ToString()
                        .lblFechaIni.Text = dsSol.Tables(0).Rows(0).Item("fecha_ini").ToString()
                        .lblFechaTer.Text = dsSol.Tables(0).Rows(0).Item("fecha_ter").ToString()
                        .lblPeriodicidad.Text = dsSol.Tables(0).Rows(0).Item("periodicidad").ToString()
                        .lblMontoXPeriodo.Text = dsSol.Tables(0).Rows(0).Item("monto_periodo").ToString()
                        .lblMontoContrato.Text = dsSol.Tables(0).Rows(0).Item("monto_contrato").ToString()
                        .lblAutorizador.Text = dsSol.Tables(0).Rows(0).Item("autorizador").ToString()
                        .lblLugar.Text = dsSol.Tables(0).Rows(0).Item("lugar_ejecucion").ToString()
                        .txtEspecificaciones.Text = dsSol.Tables(0).Rows(0).Item("descrip_servicio").ToString()
                        sdaSol.Dispose()
                        dsSol.Dispose()

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
                    'Actualizar datos del Contrato
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_contrato set fecha_autoriza = @fecha_autoriza, comentario_autoriza = @comentario_autoriza where id_ms_factura = @id_ms_factura "
                    SCMValores.Parameters.AddWithValue("@fecha_autoriza", fecha)
                    If .txtComentario.Text.Trim <> "" Then
                        SCMValores.Parameters.AddWithValue("@comentario_autoriza", .txtComentario.Text.Trim)
                    Else
                        SCMValores.Parameters.AddWithValue("@comentario_autoriza", DBNull.Value)
                    End If
                    SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    ._txtBan.Text = 1

                    'Actualizar Instancia
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 57)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Registrar en Histórico
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                             "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 57)
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    .pnlInicio.Enabled = False

                    Session("id_actividadM") = 55
                    Session("TipoM") = "F"
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
                        'Actualizar datos del Contrato
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_contrato set fecha_autoriza = @fecha_autoriza, comentario_autoriza = @comentario_autoriza where id_ms_factura = @id_ms_factura "
                        SCMValores.Parameters.AddWithValue("@fecha_autoriza", fecha)
                        SCMValores.Parameters.AddWithValue("@comentario_autoriza", .txtComentario.Text.Trim)
                        SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()
                        'Actualizar datos de la Solicitud
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_factura set status = 'Z', ultimos_comentarios = @comentario_autoriza where id_ms_factura = @id_ms_factura "
                        SCMValores.Parameters.AddWithValue("@comentario_autoriza", .txtComentario.Text.Trim)
                        SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        ._txtBan.Text = 1

                        'Actualizar Instancia
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 56)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Registrar en Histórico
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                                 "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 56)
                        SCMValores.Parameters.AddWithValue("@fecha", fecha)
                        SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Envío de Correo
                        Dim Mensaje As New System.Net.Mail.MailMessage()
                        Dim destinatario As String = ""
                        'Obtener el Correo del Solicitante
                        SCMValores.CommandText = "select cgEmpl.correo " + _
                                                 "from ms_factura " + _
                                                 "  left join cg_usuario on ms_factura.id_usr_solicita = cg_usuario.id_usuario " + _
                                                 "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                 "where id_ms_factura = @id_ms_factura "
                        SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        destinatario = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        Mensaje.[To].Add(destinatario)
                        Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                        Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                        Mensaje.Subject = "ProcAd - Solicitud de Contrato " + .lblFolio.Text + " Rechazada"
                        Dim texto As String
                        texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                                "La solicitud de contrato correspondiente a la factura número <b>" + .lblFolio.Text + "</b> fue rechazada. <br>" + _
                                "Comentarios: <b>" + .txtComentario.Text.Trim + "</b><br></span>"
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

                        Session("id_actividadM") = 55
                        Session("TipoM") = "F"
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