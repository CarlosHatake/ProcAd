Public Class _52
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 3
                    'Session("idMsInst") = 29622 '29623

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtIdMsInst.Text = Session("idMsInst")
                        ._txtBan.Text = 0

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim sdaSol As New SqlDataAdapter
                        Dim dsSol As New DataSet
                        sdaSol.SelectCommand = New SqlCommand("select ms_factura.id_ms_factura " +
                                                              "     , empleado " +
                                                              "     , empresa " +
                                                              "     , isnull(ms_factura.division, ms_factura.centro_costo) as centro_costo " +
                                                              "     , case when ms_factura.division is null then 'CC' else 'DIV' end as dimension " +
                                                              "     , isnull(ms_factura.tipo_servicio, ms_factura.servicio) as tipo_servicio " +
                                                              "     , cg_tipo_servicio.id_usr_autoriza " +
                                                              "     , especificaciones " +
                                                              "     , isnull(cg_proveedor.nombre, '') as proveedor " +
                                                              "     , dt_factura.regimen_fiscal " +
                                                              "     , ms_factura.autorizador " +
                                                              "     , isnull(cgEmpl2.nombre + ' ' + cgEmpl2.ap_paterno + ' ' + cgEmpl2.ap_materno, 'XX') as autorizador2 " +
                                                              "     , isnull(cgEmpl3.nombre + ' ' + cgEmpl3.ap_paterno + ' ' + cgEmpl3.ap_materno, 'XX') as autorizador3 " +
                                                              "     , isnull(ms_factura.id_usr_autoriza3, 0) as id_usr_autoriza3 " +
                                                              "     , case when servicio_tipo is null or servicio_tipo <> 'Servicios Negociados' then 'N' else 'S' end servNeg " +
                                                              "from ms_factura " +
                                                              "  left join ms_instancia on ms_factura.id_ms_factura = ms_instancia.id_ms_sol and ms_instancia.tipo = 'F' " +
                                                              "  left join bd_SiSAC.dbo.cg_proveedor on ms_factura.proveedor_selec = cg_proveedor.id_proveedor " +
                                                              "  left join cg_tipo_servicio on ms_factura.id_tipo_servicio = cg_tipo_servicio.id_tipo_servicio " +
                                                              "  left join cg_usuario cgAut2 on ms_factura.id_usr_autoriza2 = cgAut2.id_usuario " +
                                                              "  left join bd_Empleado.dbo.cg_empleado cgEmpl2 on cgAut2.id_empleado = cgEmpl2.id_empleado " +
                                                              "  left join cg_usuario cgAut3 on ms_factura.id_usr_autoriza3 = cgAut3.id_usuario " +
                                                              "  left join bd_Empleado.dbo.cg_empleado cgEmpl3 on cgAut3.id_empleado = cgEmpl3.id_empleado " +
                                                              "  left join dt_factura on dt_factura.uuid = ms_factura.CFDI and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                                              "where id_ms_instancia = @idMsInst ", ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@idMsInst", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()
                        .lblFolio.Text = dsSol.Tables(0).Rows(0).Item("id_ms_factura").ToString()
                        .lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("empleado").ToString()
                        .lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                        If dsSol.Tables(0).Rows(0).Item("dimension").ToString() = "CC" Then
                            .lbl_CC.Text = "Centro de Costo:"
                        Else
                            .lbl_CC.Text = "División:"
                        End If
                        .lblCC.Text = dsSol.Tables(0).Rows(0).Item("centro_costo").ToString()
                        .lblTipoServicio.Text = dsSol.Tables(0).Rows(0).Item("tipo_servicio").ToString()
                        .txtEspecificaciones.Text = dsSol.Tables(0).Rows(0).Item("especificaciones").ToString()
                        .lblProveedor.Text = dsSol.Tables(0).Rows(0).Item("proveedor").ToString()
                        .lblRegimenF.Text = dsSol.Tables(0).Rows(0).Item("regimen_fiscal").ToString()
                        .lblAutorizador.Text = dsSol.Tables(0).Rows(0).Item("autorizador").ToString()
                        ._txtIdAutorizador3.Text = dsSol.Tables(0).Rows(0).Item("id_usr_autoriza3").ToString()
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
                        ._txtServNeg.Text = dsSol.Tables(0).Rows(0).Item("servNeg").ToString()
                        sdaSol.Dispose()
                        dsSol.Dispose()

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

                        'Facturas
                        Dim sdaFacturas As New SqlDataAdapter
                        Dim dsFacturas As New DataSet
                        .gvFacturas.DataSource = dsFacturas
                        'Habilitar columnas para actualización
                        .gvFacturas.Columns(0).Visible = True
                        .gvFacturas.Columns(10).Visible = True
                        sdaFacturas.SelectCommand = New SqlCommand("select id_dt_factura " + _
                                                                   "     , fecha_emision " + _
                                                                   "     , uuid " + _
                                                                   "     , serie " + _
                                                                   "     , folio " + _
                                                                   "     , lugar_exp " + _
                                                                   "     , forma_pago " + _
                                                                   "     , moneda " + _
                                                                   "     , subtotal " + _
                                                                   "     , importe " + _
                                                                   "from dt_factura " + _
                                                                   "  left join ms_factura on dt_factura.uuid = ms_factura.CFDI and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " + _
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

                        'Evidencias
                        Dim sdaEvidencias As New SqlDataAdapter
                        Dim dsEvidencias As New DataSet
                        .gvEvidencias.DataSource = dsEvidencias
                        'Evidencias
                        sdaEvidencias.SelectCommand = New SqlCommand("select nombre as archivo " + _
                                                                   "     , 'http://148.223.153.43/ProcAd - Adjuntos IngFact/' + cast(id_dt_archivo as varchar(20)) + '-' + nombre as path " + _
                                                                   "from dt_archivo " + _
                                                                   "where id_ms_factura = @idMsFactura " + _
                                                                   "  and tipo = 'E' ", ConexionBD)
                        sdaEvidencias.SelectCommand.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        sdaEvidencias.Fill(dsEvidencias)
                        .gvEvidencias.DataBind()
                        ConexionBD.Close()
                        sdaEvidencias.Dispose()
                        dsEvidencias.Dispose()
                        .gvEvidencias.SelectedIndex = -1

                        'Activos Fijos
                        Dim sdaActivos As New SqlDataAdapter
                        Dim dsActivos As New DataSet
                        .gvAF.DataSource = dsActivos
                        sdaActivos.SelectCommand = New SqlCommand("select codigo as no_economico " + _
                                                                  "     , descripcion " + _
                                                                  "from dt_af " + _
                                                                  "where id_ms_factura = @idMsFactura ", ConexionBD)
                        sdaActivos.SelectCommand.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        sdaActivos.Fill(dsActivos)
                        .gvAF.DataBind()
                        ConexionBD.Close()
                        sdaActivos.Dispose()
                        dsActivos.Dispose()

                        If .gvAF.Rows.Count > 0 Then
                            .lbl_AF.Visible = True
                        Else
                            .lbl_AF.Visible = False
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
                    'Determinar si existe fecha_autoriza 3
                    Dim fechaAut3 As Date
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "select isnull(fecha_autoriza3, '1901-01-01') as fecha_autoriza3 " + _
                                             "from ms_factura " + _
                                             "where id_ms_factura = @id_ms_factura "
                    SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                    ConexionBD.Open()
                    fechaAut3 = SCMValores.ExecuteScalar()
                    ConexionBD.Close()

                    'Actualizar datos de la Solicitud
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    If fechaAut3 = CDate("1901-01-01") Then
                        SCMValores.CommandText = "update ms_factura set fecha_autoriza2 = @fecha_autoriza2, fecha_autoriza3 = @fecha_autoriza3, comentario_autoriza2 = @comentario_autoriza2, ultimos_comentarios = @comentario_autoriza2, status = 'A' where id_ms_factura = @id_ms_factura "
                        If Val(._txtIdAutorizador3.Text) > 0 And ._txtIdAutorizador3.Text = ._txtIdUsuario.Text Then
                            SCMValores.Parameters.AddWithValue("@fecha_autoriza3", fecha)
                        Else
                            SCMValores.Parameters.AddWithValue("@fecha_autoriza3", DBNull.Value)
                        End If
                    Else
                        SCMValores.CommandText = "update ms_factura set fecha_autoriza2 = @fecha_autoriza2, comentario_autoriza2 = @comentario_autoriza2, ultimos_comentarios = @comentario_autoriza2, status = 'A' where id_ms_factura = @id_ms_factura "
                    End If
                    SCMValores.Parameters.AddWithValue("@fecha_autoriza2", fecha)
                    If .txtComentario.Text.Trim <> "" Then
                        SCMValores.Parameters.AddWithValue("@comentario_autoriza2", .txtComentario.Text.Trim)
                    Else
                        SCMValores.Parameters.AddWithValue("@comentario_autoriza2", DBNull.Value)
                    End If
                    SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    ._txtBan.Text = 1

                    Dim idActividad As Integer
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "select case " + _
                                             "         when (fecha_autoriza is not null and (fecha_autoriza2 is not null or id_usr_autoriza2 is null) and (fecha_autoriza3 is not null or id_usr_autoriza3 is null)) then 15 " + _
                                             "         when (id_usr_autoriza is not null and fecha_autoriza is null) then 50 " + _
                                             "         when (id_usr_autoriza2 is not null and fecha_autoriza2 is null) then 52 " + _
                                             "         when (id_usr_autoriza3 is not null and fecha_autoriza3 is null) then 53 " + _
                                             "       end as idAct " + _
                                             "from ms_factura " + _
                                             "where id_ms_factura = @id_ms_factura "
                    SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                    ConexionBD.Open()
                    idActividad = SCMValores.ExecuteScalar()
                    ConexionBD.Close()

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
                    SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                             "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", idActividad)
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    If idActividad = 53 Then
                        'Envío de Correo
                        Dim Mensaje As New System.Net.Mail.MailMessage()
                        Dim destinatario As String = ""
                        SCMValores.CommandText = "select cgEmpl.correo " + _
                                                 "from cg_usuario " + _
                                                 "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                 "where cg_usuario.id_usuario = @idAut "
                        SCMValores.Parameters.AddWithValue("@idAut", Val(._txtIdAutorizador3.Text))
                        'Obtener el Correos del Tercer Autorizador
                        ConexionBD.Open()
                        destinatario = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        Mensaje.[To].Add(destinatario)
                        Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                        Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                        Mensaje.Subject = "ProcAd - Solicitud de Factura No. " + .lblFolio.Text + " por Autorizar"
                        Dim texto As String
                        texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                                "Se ingresó la solicitud número <b>" + .lblFolio.Text + _
                                "</b> por parte de <b>" + .lblSolicitante.Text + _
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

                    Session("id_actividadM") = 50
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
                        'Actualizar datos de la Solicitud
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_factura set fecha_autoriza = NULL, fecha_autoriza2 = NULL, fecha_autoriza3 = NULL, comentario_autoriza2 = @comentario_autoriza2, ultimos_comentarios = @comentario_autoriza2, status = 'Corr' where id_ms_factura = @id_ms_factura "
                        'SCMValores.Parameters.AddWithValue("@fecha_autoriza2", fecha)
                        SCMValores.Parameters.AddWithValue("@comentario_autoriza2", .txtComentario.Text.Trim)
                        SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        ._txtBan.Text = 1

                        Dim idActividad As Integer
                        If ._txtServNeg.Text = "N" Then
                            idActividad = 51
                        Else
                            idActividad = 104
                        End If

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
                        SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
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
                        Dim texto As String
                        If ._txtServNeg.Text = "N" Then
                            Mensaje.Subject = "ProcAd - Solicitud de Factura No. " + .lblFolio.Text + " Rechazada"
                            texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                                    "La solicitud número <b>" + .lblFolio.Text + "</b> fue rechazada, favor ingresar a la actividad de Corregir Factura. <br></span>"
                        Else
                            Mensaje.Subject = "ProcAd - Solicitud de Servicio Negociado No. " + .lblFolio.Text + " Rechazada"
                            texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                                    "La solicitud número <b>" + .lblFolio.Text + "</b> fue rechazada, favor ingresar a la actividad de Corregir Factura de SN. <br></span>"
                        End If
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

                        Session("id_actividadM") = 50
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