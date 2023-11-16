Public Class _47
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
                        Dim query As String
                        query = "select ms_factura.id_ms_factura " + _
                                "     , empleado " + _
                                "     , empresa " + _
                                "     , isnull(division, centro_costo) as centro_costo " + _
                                "     , case when division is null then 'CC' else 'DIV' end as dimension " + _
                                "     , isnull(tipo_servicio, servicio) as tipo_servicio " + _
                                "     , validador " + _
                                "     , especificaciones " + _
                                "     , comentario_valida " + _
                                "     , comentario_cotiza " + _
                                "     , cotizacion_selec " + _
                                "     , isnull(proveedor_selec, 0) as proveedor_selec " + _
                                "     , isnull(cg_proveedor.nombre, '') as proveedor " + _
                                "     , isnull(prov1.nombre, cot1.prov_nombre) as proveedor1 " + _
                                "     , isnull(FORMAT(cot1.precio, 'C2', 'es-MX'), '') as importe1 " + _
                                "     , isnull(div1.descripcion, '') as divisa1 " + _
                                "     , isnull(convert(varchar, cot1.fecha_ini, 103), '') as fecha_ini1 " + _
                                "     , isnull(convert(varchar, cot1.fecha_fin, 103), '') as fecha_fin1 " + _
                                "     , isnull(condPago1.cond_pago, '') as condPago1 " + _
                                "     , isnull(cot1.nombre_archivo, '') as archivo1 " + _
                                "     , isnull('http://148.223.153.43/ProcAd - Adjuntos IngFact Cot/' + cast(cot1.id_dt_cotizacion as varchar(20)) + '-' + cot1.nombre_archivo, '') as path1 " + _
                                "     , isnull(prov2.nombre, cot2.prov_nombre) as proveedor2 " + _
                                "     , isnull(FORMAT(cot2.precio, 'C2', 'es-MX'), '') as importe2 " + _
                                "     , isnull(div2.descripcion, '') as divisa2 " + _
                                "     , isnull(convert(varchar, cot2.fecha_ini, 103), '') as fecha_ini2 " + _
                                "     , isnull(convert(varchar, cot2.fecha_fin, 103), '') as fecha_fin2 " + _
                                "     , isnull(condPago2.cond_pago, '') as condPago2 " + _
                                "     , isnull(cot2.nombre_archivo, '') as archivo2 " + _
                                "     , isnull('http://148.223.153.43/ProcAd - Adjuntos IngFact Cot/' + cast(cot2.id_dt_cotizacion as varchar(20)) + '-' + cot2.nombre_archivo, '') as path2 " + _
                                "     , isnull(prov3.nombre, cot3.prov_nombre) as proveedor3 " + _
                                "     , isnull(FORMAT(cot3.precio, 'C2', 'es-MX'), '') as importe3 " + _
                                "     , isnull(div3.descripcion, '') as divisa3 " + _
                                "     , isnull(convert(varchar, cot3.fecha_ini, 103), '') as fecha_ini3 " + _
                                "     , isnull(convert(varchar, cot3.fecha_fin, 103), '') as fecha_fin3 " + _
                                "     , isnull(condPago3.cond_pago, '') as condPago3 " + _
                                "     , isnull(cot3.nombre_archivo, '') as archivo3 " + _
                                "     , isnull('http://148.223.153.43/ProcAd - Adjuntos IngFact Cot/' + cast(cot3.id_dt_cotizacion as varchar(20)) + '-' + cot3.nombre_archivo, '') as path3 " + _
                                "     , case contrato_NAV_alta when 'S' then 'Sí' else 'No' end as contrato_NAV_alta " + _
                                "     , cPresupuesto " + _
                                "from ms_factura " + _
                                "  left join ms_instancia on ms_factura.id_ms_factura = ms_instancia.id_ms_sol and ms_instancia.tipo = 'F' " + _
                                "  left join bd_SiSAC.dbo.cg_proveedor on ms_factura.proveedor_selec = cg_proveedor.id_proveedor " + _
                                "  left join dt_cotizacion cot1 on cot1.id_dt_cotizacion in (select max(cotT.id_dt_cotizacion) from dt_cotizacion cotT where cotT.id_ms_factura = ms_factura.id_ms_factura and cotT.no_cotizacion = 1) " + _
                                "  left join bd_SiSAC.dbo.cg_proveedor prov1 on cot1.id_proveedor = prov1.id_proveedor " + _
                                "  left join bd_SiSAC.dbo.cg_divisa div1 on cot1.id_divisa = div1.id_divisa " + _
                                "  left join bd_SiSAC.dbo.cg_cond_pago condPago1 on cot1.id_cond_pago = condPago1.id_cond_pago " + _
                                "  left join dt_cotizacion cot2 on cot2.id_dt_cotizacion in (select max(cotT.id_dt_cotizacion) from dt_cotizacion cotT where cotT.id_ms_factura = ms_factura.id_ms_factura and cotT.no_cotizacion = 2) " + _
                                "  left join bd_SiSAC.dbo.cg_proveedor prov2 on cot2.id_proveedor = prov2.id_proveedor " + _
                                "  left join bd_SiSAC.dbo.cg_divisa div2 on cot2.id_divisa = div2.id_divisa " + _
                                "  left join bd_SiSAC.dbo.cg_cond_pago condPago2 on cot2.id_cond_pago = condPago2.id_cond_pago " + _
                                "  left join dt_cotizacion cot3 on cot3.id_dt_cotizacion in (select max(cotT.id_dt_cotizacion) from dt_cotizacion cotT where cotT.id_ms_factura = ms_factura.id_ms_factura and cotT.no_cotizacion = 3) " + _
                                "  left join bd_SiSAC.dbo.cg_proveedor prov3 on cot3.id_proveedor = prov3.id_proveedor " + _
                                "  left join bd_SiSAC.dbo.cg_divisa div3 on cot3.id_divisa = div3.id_divisa " + _
                                "  left join bd_SiSAC.dbo.cg_cond_pago condPago3 on cot3.id_cond_pago = condPago3.id_cond_pago " + _
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
                            .lbl_CC.Text = "Centro de Costo:"
                        Else
                            .lbl_CC.Text = "División:"
                        End If
                        .lblCC.Text = dsSol.Tables(0).Rows(0).Item("centro_costo").ToString()
                        .lblTipoServicio.Text = dsSol.Tables(0).Rows(0).Item("tipo_servicio").ToString()
                        .lblValidador.Text = dsSol.Tables(0).Rows(0).Item("validador").ToString()
                        .txtEspecificaciones.Text = dsSol.Tables(0).Rows(0).Item("especificaciones").ToString()
                        ._txtValPresup.Text = dsSol.Tables(0).Rows(0).Item("cPresupuesto").ToString()
                        .txtComentarioVal.Text = dsSol.Tables(0).Rows(0).Item("comentario_valida").ToString()
                        .lblContratoNAV.Text = dsSol.Tables(0).Rows(0).Item("contrato_NAV_alta").ToString()
                        If Val(dsSol.Tables(0).Rows(0).Item("cotizacion_selec").ToString()) = 0 Then
                            .lbl_Proveedor.Visible = True
                            .lblProveedor.Visible = True
                            .lbl_Cotizacion.Visible = False
                            .pnlCotizaciones.Visible = False
                            .lbl_ProvSel.Visible = False
                            .rblProvSel.Visible = False
                            .lbl_ComentarioComp.Visible = False
                            .txtComentarioComp.Visible = False
                        Else
                            .lbl_Proveedor.Visible = False
                            .lblProveedor.Visible = False
                            .lbl_Cotizacion.Visible = True
                            .pnlCotizaciones.Visible = True
                            .lbl_ProvSel.Visible = True
                            .rblProvSel.Visible = True
                            .rblProvSel.SelectedValue = dsSol.Tables(0).Rows(0).Item("cotizacion_selec").ToString()
                            .lbl_ComentarioComp.Visible = True
                            .txtComentarioComp.Visible = True
                            .txtComentarioComp.Text = dsSol.Tables(0).Rows(0).Item("comentario_cotiza").ToString()
                        End If
                        .lblProveedor.Text = dsSol.Tables(0).Rows(0).Item("proveedor").ToString()
                        'Cotización 1
                        .lblProveedorCot1.Text = dsSol.Tables(0).Rows(0).Item("proveedor1").ToString()
                        .lblImporte1.Text = dsSol.Tables(0).Rows(0).Item("importe1").ToString()
                        .lblDivisa1.Text = dsSol.Tables(0).Rows(0).Item("divisa1").ToString()
                        .lblFechaIni1.Text = dsSol.Tables(0).Rows(0).Item("fecha_ini1").ToString()
                        .lblFechaFin1.Text = dsSol.Tables(0).Rows(0).Item("fecha_fin1").ToString()
                        .lblCondP1.Text = dsSol.Tables(0).Rows(0).Item("condPago1").ToString()
                        .hlPDF1.Text = dsSol.Tables(0).Rows(0).Item("archivo1").ToString()
                        .hlPDF1.NavigateUrl = dsSol.Tables(0).Rows(0).Item("path1").ToString()
                        'Cotización 2
                        .lblProveedorCot2.Text = dsSol.Tables(0).Rows(0).Item("proveedor2").ToString()
                        .lblImporte2.Text = dsSol.Tables(0).Rows(0).Item("importe2").ToString()
                        .lblDivisa2.Text = dsSol.Tables(0).Rows(0).Item("divisa2").ToString()
                        .lblFechaIni2.Text = dsSol.Tables(0).Rows(0).Item("fecha_ini2").ToString()
                        .lblFechaFin2.Text = dsSol.Tables(0).Rows(0).Item("fecha_fin2").ToString()
                        .lblCondP2.Text = dsSol.Tables(0).Rows(0).Item("condPago2").ToString()
                        .hlPDF2.Text = dsSol.Tables(0).Rows(0).Item("archivo2").ToString()
                        .hlPDF2.NavigateUrl = dsSol.Tables(0).Rows(0).Item("path2").ToString()
                        'Cotización 3
                        .lblProveedorCot3.Text = dsSol.Tables(0).Rows(0).Item("proveedor3").ToString()
                        .lblImporte3.Text = dsSol.Tables(0).Rows(0).Item("importe3").ToString()
                        .lblDivisa3.Text = dsSol.Tables(0).Rows(0).Item("divisa3").ToString()
                        .lblFechaIni3.Text = dsSol.Tables(0).Rows(0).Item("fecha_ini3").ToString()
                        .lblFechaFin3.Text = dsSol.Tables(0).Rows(0).Item("fecha_fin3").ToString()
                        .lblCondP3.Text = dsSol.Tables(0).Rows(0).Item("condPago3").ToString()
                        .hlPDF3.Text = dsSol.Tables(0).Rows(0).Item("archivo3").ToString()
                        .hlPDF3.NavigateUrl = dsSol.Tables(0).Rows(0).Item("path3").ToString()
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
                    SCMValores.CommandText = "update ms_factura set id_usr_finanzas = @id_usr_finanzas, fecha_finanzas = @fecha_finanzas, comentario_finanzas = @comentario_finanzas, status = 'VP' where id_ms_factura = @id_ms_factura "
                    SCMValores.Parameters.AddWithValue("@id_usr_finanzas", Val(._txtIdUsuario.Text))
                    SCMValores.Parameters.AddWithValue("@fecha_finanzas", fecha)
                    If .txtComentario.Text.Trim <> "" Then
                        SCMValores.Parameters.AddWithValue("@comentario_finanzas", .txtComentario.Text.Trim)
                    Else
                        SCMValores.Parameters.AddWithValue("@comentario_finanzas", DBNull.Value)
                    End If
                    SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    ._txtBan.Text = 1

                    Dim idActividad As Integer
                    If ._txtValPresup.Text = "S" Then
                        idActividad = 84
                    Else
                        If .lblContratoNAV.Text = "Sí" Then
                            idActividad = 54
                        Else
                            idActividad = 49
                        End If
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

                    If ._txtValPresup.Text = "S" Then
                        'Envío de Correo al Validador de Presupuesto
                        Dim Mensaje As New System.Net.Mail.MailMessage()
                        Dim destinatario As String = ""
                        'Obtener el Correos del Validador de Presupuesto
                        SCMValores.CommandText = "select valor from cg_parametros where parametro = 'val_presupuesto' "
                        ConexionBD.Open()
                        destinatario = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        Mensaje.[To].Add(destinatario)
                        Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                        Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                        Mensaje.Subject = "ProcAd - Solicitud de Factura No. " + .lblFolio.Text + " por Validar Presupuesto"
                        Dim texto As String
                        texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                                "Se autorizó la solicitud número <b>" + .lblFolio.Text + _
                                "</b> de <b>" + .lblSolicitante.Text + _
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
                    Else
                        If .lblContratoNAV.Text = "Sí" Then
                            'Envío de Correo al Solicitante para Complementación de Contrato
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
                            Mensaje.Subject = "ProcAd - Solicitud de Factura No. " + .lblFolio.Text + " Autorizada"
                            Dim texto As String
                            texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                                    "Se autorizó la solicitud número <b>" + .lblFolio.Text + _
                                    "</b><br><br>Favor de Complementar los Datos para el Contrato </span>"
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
                        Else
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
                            Mensaje.Subject = "ProcAd - Solicitud de Factura No. " + .lblFolio.Text + " Autorizada"
                            Dim texto As String
                            texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                                    "Se autorizó la solicitud número <b>" + .lblFolio.Text + _
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
                        End If
                    End If

                    .pnlInicio.Enabled = False

                    Session("id_actividadM") = 47
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
                        SCMValores.CommandText = "update ms_factura set id_usr_finanzas = @id_usr_finanzas, fecha_finanzas = @fecha_finanzas, comentario_finanzas = @comentario_finanzas, status = 'ZF' where id_ms_factura = @id_ms_factura "
                        SCMValores.Parameters.AddWithValue("@id_usr_finanzas", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@fecha_finanzas", fecha)
                        SCMValores.Parameters.AddWithValue("@comentario_finanzas", .txtComentario.Text.Trim)
                        SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        ._txtBan.Text = 1

                        'Actualizar Instancia
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 48)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Registrar en Histórico
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                                 "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 48)
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
                        Mensaje.Subject = "ProcAd - Solicitud de Factura No. " + .lblFolio.Text + " No Autorizada"
                        Dim texto As String
                        texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                                "La solicitud número <b>" + .lblFolio.Text + "</b> no fue autorizada. <br>" + _
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

                        Session("id_actividadM") = 47
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