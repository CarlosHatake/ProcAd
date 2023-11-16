Public Class _98
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 3
                    'Session("idMsInst") = 29623

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
                                "     , isnull(division, centro_costo) as centro_costo " + _
                                "     , case when division is null then 'CC' else 'DIV' end as dimension " + _
                                "     , servicio " + _
                                "     , isnull(base, '') as base " + _
                                "     , isnull(lugar_ejecucion, '') as lugar_ejecucion " + _
                                "     , isnull(cg_proveedor.nombre, '') as proveedor " + _
                                "     , especificaciones " + _
                                "     , cFinanzas " + _
                                "     , cPresupuesto " + _
                                "     , isnull(validador, '') as validador " + _
                                "     , isnull(comentario_valida, '') as comentario_valida " + _
                                "     , isnull(comentario_finanzas, '') as comentario_finanzas " + _
                                "     , isnull(comentario_val_presupuesto, '') as comentario_val_presupuesto " + _
                                "from ms_factura " + _
                                "  left join ms_instancia on ms_factura.id_ms_factura = ms_instancia.id_ms_sol and ms_instancia.tipo = 'F' " + _
                                "  left join bd_SiSAC.dbo.cg_proveedor on ms_factura.proveedor_selec = cg_proveedor.id_proveedor " + _
                                "where id_ms_instancia = @idMsInst "
                        sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@idMsInst", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()
                        .lblFolio.Text = dsSol.Tables(0).Rows(0).Item("id_ms_factura").ToString()
                        .lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("empleado").ToString()
                        .lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                        If dsSol.Tables(0).Rows(0).Item("dimension").ToString() = "CC" Then
                            .lbl_Dimension.Text = "Centro de Costo:"
                        Else
                            .lbl_Dimension.Text = "División:"
                        End If
                        .lblDimension.Text = dsSol.Tables(0).Rows(0).Item("centro_costo").ToString()
                        .lblServicio.Text = dsSol.Tables(0).Rows(0).Item("servicio").ToString()
                        .lblProveedor.Text = dsSol.Tables(0).Rows(0).Item("proveedor").ToString()
                        .lblBase.Text = dsSol.Tables(0).Rows(0).Item("base").ToString()
                        .lblLugar.Text = dsSol.Tables(0).Rows(0).Item("lugar_ejecucion").ToString()
                        .txtDescripcion.Text = dsSol.Tables(0).Rows(0).Item("especificaciones").ToString()
                        .lblValidador.Text = dsSol.Tables(0).Rows(0).Item("validador").ToString()
                        .txtComentarioVal.Text = dsSol.Tables(0).Rows(0).Item("comentario_valida").ToString()
                        .txtComentarioAF.Text = dsSol.Tables(0).Rows(0).Item("comentario_finanzas").ToString()
                        .txtComentario.Text = dsSol.Tables(0).Rows(0).Item("comentario_val_presupuesto").ToString()
                        sdaSol.Dispose()
                        dsSol.Dispose()

                        'Dt_factura
                        Dim sdaCatalogo As New SqlDataAdapter
                        Dim dsCatalogo As New DataSet
                        .gvDtFacturaSN.DataSource = dsCatalogo
                        'Habilitar columna para actualización
                        .gvDtFacturaSN.Columns(0).Visible = True
                        'Catálogo de Cantidades Agregadas
                        sdaCatalogo.SelectCommand = New SqlCommand("select id_dt_factura_sn " + _
                                                                   "     , cantidad " + _
                                                                   "     , empresa " + _
                                                                   "     , no_economico " + _
                                                                   "     , tipo " + _
                                                                   "     , modelo " + _
                                                                   "     , placas " + _
                                                                   "     , div " + _
                                                                   "     , division " + _
                                                                   "     , zn " + _
                                                                   "from dt_factura_sn " + _
                                                                   "where id_ms_factura = @id_ms_factura ", ConexionBD)
                        sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        sdaCatalogo.Fill(dsCatalogo)
                        .gvDtFacturaSN.DataBind()
                        ConexionBD.Close()
                        sdaCatalogo.Dispose()
                        dsCatalogo.Dispose()
                        .gvDtFacturaSN.SelectedIndex = -1
                        'Inhabilitar columna para vista
                        .gvDtFacturaSN.Columns(0).Visible = False


                        'Adjuntos Requeridos
                        Dim sdaAdjReq As New SqlDataAdapter
                        Dim dsAdjReq As New DataSet
                        .gvAdjuntosReq.DataSource = dsAdjReq
                        sdaAdjReq.SelectCommand = New SqlCommand("select adjunto " + _
                                                                 "from dt_factura_adj " + _
                                                                 "where id_ms_factura = @id_ms_factura ", ConexionBD)
                        sdaAdjReq.SelectCommand.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        sdaAdjReq.Fill(dsAdjReq)
                        .gvAdjuntosReq.DataBind()
                        ConexionBD.Close()
                        sdaAdjReq.Dispose()
                        dsAdjReq.Dispose()
                        .gvAdjuntosReq.SelectedIndex = -1
                        If .gvAdjuntosReq.Rows.Count > 0 Then
                            .lbl_AdjuntoReq.Visible = True
                            .gvAdjuntosReq.Visible = True
                        Else
                            .lbl_AdjuntoReq.Visible = False
                            .gvAdjuntosReq.Visible = False
                        End If

                        'Adjuntos
                        Dim sdaArchivos As New SqlDataAdapter
                        Dim dsArchivos As New DataSet
                        .gvAdjuntos.DataSource = dsArchivos
                        sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " + _
                                                                   "     , 'http://148.223.153.43/ProcAd - Adjuntos IngFact/' + cast(id_dt_archivo as varchar(20)) + '-' + nombre as path " + _
                                                                   "from dt_archivo " + _
                                                                   "where id_ms_factura = @idMsFactura " + _
                                                                   "  and tipo = 'A' ", ConexionBD)
                        sdaArchivos.SelectCommand.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        sdaArchivos.Fill(dsArchivos)
                        .gvAdjuntos.DataBind()
                        ConexionBD.Close()
                        sdaArchivos.Dispose()
                        dsArchivos.Dispose()
                        .gvAdjuntos.SelectedIndex = -1
                        If .gvAdjuntos.Rows.Count > 0 Then
                            .lbl_Adjunto.Visible = True
                            .gvAdjuntos.Visible = True
                        Else
                            .lbl_Adjunto.Visible = False
                            .gvAdjuntos.Visible = False
                        End If

                        'Soportes
                        Dim sdaSoportes As New SqlDataAdapter
                        Dim dsSoportes As New DataSet
                        .gvSoportes.DataSource = dsSoportes
                        sdaSoportes.SelectCommand = New SqlCommand("select nombre as archivo " + _
                                                                   "     , 'http://148.223.153.43/ProcAd - Adjuntos IngFact/' + cast(id_dt_archivo as varchar(20)) + '-' + nombre as path " + _
                                                                   "from dt_archivo " + _
                                                                   "where id_ms_factura = @idMsFactura " + _
                                                                   "  and tipo = 'S' ", ConexionBD)
                        sdaSoportes.SelectCommand.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        sdaSoportes.Fill(dsSoportes)
                        .gvSoportes.DataBind()
                        ConexionBD.Close()
                        sdaSoportes.Dispose()
                        dsSoportes.Dispose()
                        .gvSoportes.SelectedIndex = -1
                        If .gvSoportes.Rows.Count = 0 Then
                            .lbl_Soportes.Visible = False
                            .gvSoportes.Visible = False
                        Else
                            .lbl_Soportes.Visible = True
                            .gvSoportes.Visible = True
                        End If

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
                    'Actualizar datos de la Solicitud
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_factura set id_usr_val_presupuesto = @id_usr_val_presupuesto, fecha_val_presupuesto = @fecha_val_presupuesto, comentario_val_presupuesto = @comentario_val_presupuesto, status = 'AF' where id_ms_factura = @id_ms_factura "
                    SCMValores.Parameters.AddWithValue("@id_usr_val_presupuesto", Val(._txtIdUsuario.Text))
                    SCMValores.Parameters.AddWithValue("@fecha_val_presupuesto", fecha)
                    If .txtComentario.Text.Trim <> "" Then
                        SCMValores.Parameters.AddWithValue("@comentario_val_presupuesto", .txtComentario.Text.Trim)
                    Else
                        SCMValores.Parameters.AddWithValue("@comentario_val_presupuesto", DBNull.Value)
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
                    SCMValores.Parameters.AddWithValue("@id_actividad", 101)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Registrar en Histórico
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                             "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 101)
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Envío de Correo al Solicitante para Ingreso de Factura
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
                    Mensaje.Subject = "ProcAd - Solicitud de Servicio Negociado No. " + .lblFolio.Text + " Autorizada"
                    Dim texto As String
                    texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                            "Se autorizó la solicitud de Servicio Negociado número <b>" + .lblFolio.Text + _
                            "</b><br><br>Favor de Ingresar la Factura Correspondiente </span>"
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

                    Session("id_actividadM") = 98
                    Session("TipoM") = "SN"
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
                        SCMValores.CommandText = "update ms_factura set id_usr_val_presupuesto = @id_usr_val_presupuesto, fecha_val_presupuesto = @fecha_val_presupuesto, comentario_val_presupuesto = @comentario_val_presupuesto, status = 'SAP' where id_ms_factura = @id_ms_factura "
                        SCMValores.Parameters.AddWithValue("@id_usr_val_presupuesto", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@fecha_val_presupuesto", fecha)
                        SCMValores.Parameters.AddWithValue("@comentario_val_presupuesto", .txtComentario.Text.Trim)
                        SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        ._txtBan.Text = 1

                        'Actualizar Instancia
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 99)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Registrar en Histórico
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                                 "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 99)
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
                        Mensaje.Subject = "ProcAd - Solicitud de Servicio Negociado No. " + .lblFolio.Text + " No Autorizada"
                        Dim texto As String
                        texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                                "La solicitud número <b>" + .lblFolio.Text + "</b> no fue autorizada. <br>" + _
                                "Comentarios: <b>" + .txtComentario.Text.Trim + "</b><br>" + _
                                "Favor ingresar a la actividad <b>Solicitar Ampl. de Presup. para SN</b> </span>"
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

                        Session("id_actividadM") = 98
                        Session("TipoM") = "SN"
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