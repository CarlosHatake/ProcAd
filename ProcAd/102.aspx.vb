Public Class _102
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
                        query = "select id_ms_factura " +
                                "     , empleado " +
                                "     , empresa " +
                                "     , isnull(division, centro_costo) as centro_costo " +
                                "     , case when division is null then 'CC' else 'DIV' end as dimension " +
                                "     , servicio " +
                                "     , isnull(base, '') as base " +
                                "     , isnull(lugar_ejecucion, '') as lugar_ejecucion " +
                                "     , isnull(cg_proveedor.nombre, '') as proveedor " +
                                "     , especificaciones " +
                                "     , isnull(comentario_valida2, '') as comentario_valida2 " +
                                "     , ms_factura.id_usr_autoriza " +
                                "     , cgEmplAut1.nombre + ' ' + cgEmplAut1.ap_paterno + ' ' + cgEmplAut1.ap_materno as autorizador1 " +
                                "     , isnull(cgEmplAut2.nombre + ' ' + cgEmplAut2.ap_paterno + ' ' + cgEmplAut2.ap_materno, 'XX') as autorizador2 " +
                                "     , isnull(cgEmplAut3.nombre + ' ' + cgEmplAut3.ap_paterno + ' ' + cgEmplAut3.ap_materno, 'XX') as autorizador3 " +
                                "from ms_factura " +
                                "  left join ms_instancia on ms_factura.id_ms_factura = ms_instancia.id_ms_sol and ms_instancia.tipo = 'F' " +
                                "  left join bd_SiSAC.dbo.cg_proveedor on ms_factura.proveedor_selec = cg_proveedor.id_proveedor " +
                                "  left join cg_usuario cgUsrAut1 on ms_factura.id_usr_autoriza = cgUsrAut1.id_usuario " +
                                "  left join bd_Empleado.dbo.cg_empleado cgEmplAut1 on cgUsrAut1.id_empleado = cgEmplAut1.id_empleado " +
                                "  left join cg_usuario cgUsrAut2 on ms_factura.id_usr_autoriza2 = cgUsrAut2.id_usuario " +
                                "  left join bd_Empleado.dbo.cg_empleado cgEmplAut2 on cgUsrAut2.id_empleado = cgEmplAut2.id_empleado " +
                                "  left join cg_usuario cgUsrAut3 on ms_factura.id_usr_autoriza3 = cgUsrAut3.id_usuario " +
                                "  left join bd_Empleado.dbo.cg_empleado cgEmplAut3 on cgUsrAut3.id_empleado = cgEmplAut3.id_empleado " +
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
                        .txtComentario.Text = dsSol.Tables(0).Rows(0).Item("comentario_valida2").ToString()
                        .lblAutorizador1.Text = dsSol.Tables(0).Rows(0).Item("autorizador1").ToString()
                        ._txtIdAutorizador1.Text = dsSol.Tables(0).Rows(0).Item("id_usr_autoriza").ToString()
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

                        'Dt_factura
                        Dim sdaCatalogo As New SqlDataAdapter
                        Dim dsCatalogo As New DataSet
                        .gvDtFacturaSN.DataSource = dsCatalogo
                        'Habilitar columna para actualización
                        .gvDtFacturaSN.Columns(0).Visible = True
                        'Catálogo de Cantidades Agregadas
                        sdaCatalogo.SelectCommand = New SqlCommand("select id_dt_factura_sn " +
                                                                   "     , cantidad " +
                                                                   "     , empresa " +
                                                                   "     , no_economico " +
                                                                   "     , tipo " +
                                                                   "     , modelo " +
                                                                   "     , placas " +
                                                                   "     , div " +
                                                                   "     , division " +
                                                                   "     , zn " +
                                                                   "from dt_factura_sn " +
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
                        sdaAdjReq.SelectCommand = New SqlCommand("select adjunto " +
                                                                 "from dt_factura_adj " +
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
                        sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " +
                                                                   "     , 'http://148.223.153.43/ProcAd - Adjuntos IngFact/' + cast(id_dt_archivo as varchar(20)) + '-' + nombre as path " +
                                                                   "from dt_archivo " +
                                                                   "where id_ms_factura = @idMsFactura " +
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

                        'Evidencias
                        Dim sdaEvidencias As New SqlDataAdapter
                        Dim dsEvidencias As New DataSet
                        .gvEvidencias.DataSource = dsEvidencias
                        'Evidencias
                        sdaEvidencias.SelectCommand = New SqlCommand("select nombre as archivo " +
                                                                     "     , 'http://148.223.153.43/ProcAd - Adjuntos IngFact/' + cast(id_dt_archivo as varchar(20)) + '-' + nombre as path " +
                                                                     "from dt_archivo " +
                                                                     "where id_ms_factura = @idMsFactura " +
                                                                     "  and tipo = 'E' ", ConexionBD)
                        sdaEvidencias.SelectCommand.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        sdaEvidencias.Fill(dsEvidencias)
                        .gvEvidencias.DataBind()
                        ConexionBD.Close()
                        sdaEvidencias.Dispose()
                        dsEvidencias.Dispose()
                        .gvEvidencias.SelectedIndex = -1

                        'Facturas
                        Dim sdaFacturas As New SqlDataAdapter
                        Dim dsFacturas As New DataSet
                        .gvFacturas.DataSource = dsFacturas
                        'Habilitar columnas para actualización
                        .gvFacturas.Columns(0).Visible = True
                        .gvFacturas.Columns(10).Visible = True
                        sdaFacturas.SelectCommand = New SqlCommand("select id_dt_factura " +
                                                                   "     , fecha_emision " +
                                                                   "     , uuid " +
                                                                   "     , 'http://148.223.153.43/Facturas/' + dt_factura.rfc_receptor + '/' + format(datepart(year, dt_factura.fecha_emision), '00') + '/' + upper(format(dt_factura.fecha_emision, 'MMMM', 'es-es')) + '/' + dt_factura.movimiento + '/' + dt_factura.tipo + '/' + dt_factura.uuid + '.pdf' as path " +
                                                                   "     , 'PDF' as pdf " +
                                                                   "     , serie " +
                                                                   "     , folio " +
                                                                   "     , lugar_exp " +
                                                                   "     , forma_pago " +
                                                                   "     , moneda " +
                                                                   "     , subtotal " +
                                                                   "     , importe " +
                                                                   "from dt_factura " +
                                                                   "  left join ms_factura on dt_factura.uuid = ms_factura.CFDI and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                                                   "where ms_factura.id_ms_factura = @idMsFactura ", ConexionBD)
                        sdaFacturas.SelectCommand.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        sdaFacturas.Fill(dsFacturas)
                        .gvFacturas.DataBind()
                        ConexionBD.Close()
                        sdaFacturas.Dispose()
                        dsFacturas.Dispose()
                        .gvFacturas.SelectedIndex = -1
                        'Inhabilitar columnas para vista
                        .gvFacturas.Columns(0).Visible = False
                        .gvFacturas.Columns(10).Visible = False

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
                        SCMValores.CommandText = "update ms_factura set fecha_valida2 = @fecha_valida2, comentario_valida2 = @comentario_valida2, status = 'AV2' where id_ms_factura = @id_ms_factura "
                        SCMValores.Parameters.AddWithValue("@fecha_valida2", fecha)
                        SCMValores.Parameters.AddWithValue("@comentario_valida2", .txtComentario.Text.Trim)
                        SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        ._txtBan.Text = 1

                        'Actualizar Instancia
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 50)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Registrar en Histórico
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                                                 "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 50)
                        SCMValores.Parameters.AddWithValue("@fecha", fecha)
                        SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Envío de Correo
                        Dim Mensaje As New System.Net.Mail.MailMessage()
                        Dim destinatario As String = ""
                        'Obtener el Correos del Primer Autorizador
                        SCMValores.CommandText = "select cgEmpl.correo from cg_usuario left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado where cg_usuario.id_usuario = @idAut "
                        SCMValores.Parameters.AddWithValue("@idAut", Val(._txtIdAutorizador1.Text))
                        ConexionBD.Open()
                        destinatario = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        Mensaje.[To].Add(destinatario)
                        Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                        'Mensaje.From = New System.Net.Mail.MailAddress("soporte.unne.procad@gmail.com", "ProcAd", System.Text.Encoding.UTF8)
                        Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                        Mensaje.Subject = "ProcAd - Solicitud de Servicio Negociado No. " + .lblFolio.Text + " por Autorizar"
                        Dim texto As String
                        texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                                "Se ingresó la factura de la solicitud de Servicio Negociado número <b>" + .lblFolio.Text +
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

                        .pnlInicio.Enabled = False

                        Session("id_actividadM") = 102
                        Session("TipoM") = "SN"
                        Server.Transfer("Menu.aspx")
                    End While
                End If
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
                        SCMValores.CommandText = "update ms_factura set fecha_valida2 = @fecha_valida2, comentario_valida2 = @comentario_valida2, ultimos_comentarios = @comentario_valida2, status = 'Corr' where id_ms_factura = @id_ms_factura "
                        SCMValores.Parameters.AddWithValue("@fecha_valida2", fecha)
                        SCMValores.Parameters.AddWithValue("@comentario_valida2", .txtComentario.Text.Trim)
                        SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        ._txtBan.Text = 1

                        'Actualizar Instancia
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 104)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Registrar en Histórico
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                                                 "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 104)
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
                        'Mensaje.From = New System.Net.Mail.MailAddress("soporte.unne.procad@gmail.com", "ProcAd", System.Text.Encoding.UTF8)
                        Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                        Mensaje.Subject = "ProcAd - Solicitud de Servicio Negociado No. " + .lblFolio.Text + " Rechazado"
                        Dim texto As String
                        texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                                "La solicitud número <b>" + .lblFolio.Text + "</b> fue rechazada, favor ingresar a la actividad de Corregir Factura de SN. <br></span>"
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

                        Session("id_actividadM") = 102
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