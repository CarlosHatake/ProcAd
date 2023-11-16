Public Class _14
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

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim sdaSol As New SqlDataAdapter
                        Dim dsSol As New DataSet
                        Dim query As String
                        query = "select id_ms_factura " + _
                                "     , empleado " + _
                                "     , empresa " + _
                                "     , autorizador " + _
                                "     , centro_costo " + _
                                "     , rfc_emisor " + _
                                "     , razon_emisor " + _
                                "     , isnull(tabla_comp, 'XX') as tabla_comp " + _
                                "     , isnull(adjunto_opcional, 'XX') as adjunto_opcional " + _
                                "from ms_factura " + _
                                "  left join ms_instancia on ms_factura.id_ms_factura = ms_instancia.id_ms_sol and ms_instancia.tipo = 'F' " + _
                                "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " + _
                                "where id_ms_instancia = @idMsInst "
                        sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@idMsInst", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()
                        .lblFolio.Text = dsSol.Tables(0).Rows(0).Item("id_ms_factura").ToString()
                        .lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("empleado").ToString()
                        .lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                        .lblRFC.Text = dsSol.Tables(0).Rows(0).Item("rfc_emisor").ToString()
                        .lblAutorizador.Text = dsSol.Tables(0).Rows(0).Item("autorizador").ToString()
                        .lblCC.Text = dsSol.Tables(0).Rows(0).Item("centro_costo").ToString()
                        .lblProveedor.Text = dsSol.Tables(0).Rows(0).Item("razon_emisor").ToString()
                        'Tabla Comparativa
                        If dsSol.Tables(0).Rows(0).Item("tabla_comp").ToString() = "XX" Then
                            .hlTablaComp.Visible = False
                            .lblTablaComp.Visible = True
                        Else
                            .hlTablaComp.Visible = True
                            .lblTablaComp.Visible = False
                            .hlTablaComp.Text = dsSol.Tables(0).Rows(0).Item("tabla_comp").ToString()
                            '.hlTablaComp.NavigateUrl = "http://localhost/ProcAd - Adjuntos/" + .lblFolio.Text + "TabCon-" + dsSol.Tables(0).Rows(0).Item("tabla_comp").ToString()
                            .hlTablaComp.NavigateUrl = "http://148.223.153.43/ProcAd - Adjuntos/" + .lblFolio.Text + "TabCon-" + dsSol.Tables(0).Rows(0).Item("tabla_comp").ToString()
                        End If
                        'Adjunto Opcional
                        If dsSol.Tables(0).Rows(0).Item("adjunto_opcional").ToString() = "XX" Then
                            .hlAdjOpc.Visible = False
                            .lblAdjOpc.Visible = True
                        Else
                            .hlAdjOpc.Visible = True
                            .lblAdjOpc.Visible = False
                            .hlAdjOpc.Text = dsSol.Tables(0).Rows(0).Item("adjunto_opcional").ToString()
                            '.hlAdjOpc.NavigateUrl = "http://localhost/ProcAd - Adjuntos/" + .lblFolio.Text + "AdjOpc-" + dsSol.Tables(0).Rows(0).Item("adjunto_opcional").ToString()
                            .hlAdjOpc.NavigateUrl = "http://148.223.153.43/ProcAd - Adjuntos/" + .lblFolio.Text + "AdjOpc-" + dsSol.Tables(0).Rows(0).Item("adjunto_opcional").ToString()
                        End If
                        sdaSol.Dispose()
                        dsSol.Dispose()

                        'Llenar Grid
                        Dim sdaFactura As New SqlDataAdapter
                        Dim dsFactura As New DataSet
                        .gvFactura.DataSource = dsFactura
                        sdaFactura.SelectCommand = New SqlCommand("select fecha_emision " + _
                                                                  "     , uuid " + _
                                                                  "     , 'http://148.223.153.43/Facturas/' + dt_factura.rfc_receptor + '/' + format(datepart(year, dt_factura.fecha_emision), '00') + '/' + upper(format(dt_factura.fecha_emision, 'MMMM', 'es-es')) + '/' + dt_factura.movimiento + '/' + dt_factura.tipo + '/' + dt_factura.uuid + '.pdf' as path " + _
                                                                  "     , 'PDF' as pdf " + _
                                                                  "     , serie " + _
                                                                  "     , folio " + _
                                                                  "     , lugar_exp " + _
                                                                  "     , forma_pago " + _
                                                                  "     , moneda " + _
                                                                  "     , subtotal " + _
                                                                  "     , importe " + _
                                                                  "from ms_factura " + _
                                                                  "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " + _
                                                                  "where id_ms_factura = @idMsFactura ", ConexionBD)
                        sdaFactura.SelectCommand.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        sdaFactura.Fill(dsFactura)
                        .gvFactura.DataBind()
                        ConexionBD.Close()
                        sdaFactura.Dispose()
                        dsFactura.Dispose()
                        .gvFactura.SelectedIndex = -1

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

    Protected Sub btnAutoriza_Click(sender As Object, e As EventArgs) Handles btnAutoriza.Click
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim fecha As DateTime = Date.Now
                While Val(._txtBan.Text) = 0
                    'Actualizar datos de la Solicitud
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_factura set fecha_autoriza = @fecha_autoriza, status = 'A' where id_ms_factura = @id_ms_factura "
                    SCMValores.Parameters.AddWithValue("@fecha_autoriza", fecha)
                    SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Actualizar dt_factura
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update dt_factura " + _
                                             "  set status = 'A' " + _
                                             "where uuid = @uuid " + _
                                             "  and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') "
                    SCMValores.Parameters.AddWithValue("@uuid", .gvFactura.Rows(0).Cells(1).Text)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    ._txtBan.Text = 1

                    'Actualizar Instancia
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 15)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Registrar en Histórico
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                             "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 15)
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    .pnlInicio.Enabled = False

                    Session("id_actividadM") = 14
                    Session("TipoM") = "F"
                    Server.Transfer("Menu.aspx")
                End While
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnRechaza_Click(sender As Object, e As EventArgs) Handles btnRechaza.Click
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim fecha As DateTime = Date.Now
                While Val(._txtBan.Text) = 0
                    'Actualizar datos de la Solicitud
                    'Se omite la actualización de la fecha_autoriza, para evitar confusión hacia el usuario pues no se autorizó
                    'SCMValores.CommandText = ""
                    'SCMValores.Parameters.Clear()
                    'SCMValores.CommandText = "update ms_factura set fecha_autoriza = @fecha_autoriza where id_ms_factura = @id_ms_factura "
                    'SCMValores.Parameters.AddWithValue("@fecha_autoriza", fecha)
                    'SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                    'ConexionBD.Open()
                    'SCMValores.ExecuteNonQuery()
                    'ConexionBD.Close()

                    ._txtBan.Text = 1

                    'Actualizar Instancia
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 17)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Registrar en Histórico
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                             "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 17)
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
                    'Mensaje.From = New System.Net.Mail.MailAddress("soporte.unne.procad@gmail.com", "ProcAd", System.Text.Encoding.UTF8)
                    Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                    Mensaje.Subject = "ProcAd - Solicitud de Factura No. " + .lblFolio.Text + " Rechazada"
                    Dim texto As String
                    texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                            "La solicitud número <b>" + .lblFolio.Text + "</b> fue rechazada. <br></span>"
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

                    Session("id_actividadM") = 14
                    Session("TipoM") = "F"
                    Server.Transfer("Menu.aspx")
                End While
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class