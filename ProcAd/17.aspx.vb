Public Class _17
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 1

                    .litError.Text = ""
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
                                "     , isnull(cgEmpr.id_empresa, 0) as id_empresa " + _
                                "     , cgEmpr.id_empresa " + _
                                "     , empresa " + _
                                "     , cg_usuario.id_empleado " + _
                                "     , autorizador " + _
                                "     , isnull(cgCC.id_cc, 0) as id_cc " + _
                                "     , centro_costo " + _
                                "     , rfc_emisor " + _
                                "     , razon_emisor " + _
                                "     , isnull(tabla_comp, 'XX') as tabla_comp " + _
                                "     , isnull(adjunto_opcional, 'XX') as adjunto_opcional " + _
                                "     , isnull(ultimos_comentarios, '') as ultimos_comentarios " + _
                                "from ms_factura " + _
                                "  left join ms_instancia on ms_factura.id_ms_factura = ms_instancia.id_ms_sol and ms_instancia.tipo = 'F' " + _
                                "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " + _
                                "  left join bd_Empleado.dbo.cg_empresa cgEmpr on ms_factura.empresa = cgEmpr.nombre and cgEmpr.status = 'A' " + _
                                "  left join bd_Empleado.dbo.cg_cc cgCC on cgEmpr.id_empresa = cgCC.id_empresa and ms_factura.centro_costo = cgCC.nombre and cgCC.status = 'A' " + _
                                "  left join cg_usuario on ms_factura.id_usr_autoriza = cg_usuario.id_usuario " + _
                                "where id_ms_instancia = @idMsInst "
                        sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@idMsInst", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()
                        .lblFolio.Text = dsSol.Tables(0).Rows(0).Item("id_ms_factura").ToString()
                        .lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("empleado").ToString()
                        ._txtIdEmpresa.Text = dsSol.Tables(0).Rows(0).Item("id_empresa").ToString()
                        .lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                        .lblRFC.Text = dsSol.Tables(0).Rows(0).Item("rfc_emisor").ToString()
                        ._txtIdAut.Text = dsSol.Tables(0).Rows(0).Item("id_empleado").ToString()
                        ._txtIdCC.Text = dsSol.Tables(0).Rows(0).Item("id_cc").ToString()
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
                        .txtUltComent.Text = dsSol.Tables(0).Rows(0).Item("ultimos_comentarios").ToString()
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

                        'Centros de Costo
                        Dim sdaCC As New SqlDataAdapter
                        Dim dsCC As New DataSet
                        sdaCC.SelectCommand = New SqlCommand("select id_cc, nombre " + _
                                                             "from bd_Empleado.dbo.cg_cc " + _
                                                             "where id_empresa = @idEmpresa " + _
                                                             "  and status = 'A' " + _
                                                             "order by nombre ", ConexionBD)
                        sdaCC.SelectCommand.Parameters.AddWithValue("@idEmpresa", Val(._txtIdEmpresa.Text))
                        .ddlCC.DataSource = dsCC
                        .ddlCC.DataTextField = "nombre"
                        .ddlCC.DataValueField = "id_cc"
                        ConexionBD.Open()
                        sdaCC.Fill(dsCC)
                        .ddlCC.DataBind()
                        ConexionBD.Close()
                        sdaCC.Dispose()
                        dsCC.Dispose()
                        .ddlCC.SelectedValue = Val(._txtIdCC.Text)

                        'Lista de Autorizadores
                        Dim sdaAut As New SqlDataAdapter
                        Dim dsAut As New DataSet
                        .ddlAutorizador.DataSource = dsAut
                        sdaAut.SelectCommand = New SqlCommand("select cgEmpl.id_empleado, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " + _
                                                              "from dt_autorizador " + _
                                                              "  left join cg_usuario on dt_autorizador.id_autorizador = cg_usuario.id_usuario " + _
                                                              "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                              "where dt_autorizador.id_usuario = @idUsuario " + _
                                                              "  and cg_usuario.status = 'A' " + _
                                                              "order by aut_dir, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno ", ConexionBD)
                        sdaAut.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        .ddlAutorizador.DataTextField = "nombre_empleado"
                        .ddlAutorizador.DataValueField = "id_empleado"
                        ConexionBD.Open()
                        sdaAut.Fill(dsAut)
                        .ddlAutorizador.DataBind()
                        ConexionBD.Close()
                        sdaAut.Dispose()
                        dsAut.Dispose()
                        .ddlAutorizador.SelectedValue = Val(._txtIdAut.Text)

                        'Botones
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

#Region "Botones"

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
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
                    Dim sdaEmpleado As New SqlDataAdapter
                    Dim dsEmpleado As New DataSet
                    Dim query As String
                    query = "select cgEmpl.no_empleado as no_empleadoE " + _
                            "     , cgUsrA.id_usuario as id_usr_aut " + _
                            "     , cgAut.no_empleado as no_empleadoA " + _
                            "     , cgAut.nombre + ' ' + cgAut.ap_paterno + ' ' + cgAut.ap_materno as Autorizador " + _
                            "from bd_empleado.dbo.cg_empleado cgEmpl " + _
                            "  left join cg_usuario cgUsrE on cgEmpl.id_empleado = cgUsrE.id_empleado " + _
                            "  left join bd_Empleado.dbo.cg_empleado cgAut on cgAut.id_empleado = @idAut " + _
                            "  left join cg_usuario cgUsrA on cgAut.id_empleado = cgUsrA.id_empleado " + _
                            "where cgUsrE.id_usuario = @idEmpl "
                    sdaEmpleado.SelectCommand = New SqlCommand(query, ConexionBD)
                    sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idEmpl", Val(._txtIdUsuario.Text))
                    sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idAut", .ddlAutorizador.SelectedValue)
                    ConexionBD.Open()
                    sdaEmpleado.Fill(dsEmpleado)
                    ConexionBD.Close()

                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    If Not ((Not fuTablaComp.PostedFile Is Nothing) And (fuTablaComp.PostedFile.ContentLength > 0)) Then
                        'No se Actualiza la Tabla Comparativa
                        SCMValores.CommandText = "update ms_factura set id_usr_autoriza = @id_usr_autoriza, centro_costo = @centro_costo, no_autorizador = @no_autorizador, autorizador = @autorizador where id_ms_factura = @id_ms_factura "
                    Else
                        'Se Actualiza la Tabla Comparativa
                        'Tabla Comparativa
                        ' '' Ruta Local
                        ''Dim sFileDir As String = "C:/ProcAd - Adjuntos/" 'Ruta en que se almacenará el archivo
                        ' Ruta en Atenea
                        Dim sFileDir As String = "D:\ProcAd - Adjuntos\" 'Ruta en que se almacenará el archivo
                        Dim lMaxFileSize As Long = 10485760 'Equivalente a 10 Megabytes
                        Dim sFileNameTabCon As String = System.IO.Path.GetFileName(fuTablaComp.PostedFile.FileName)

                        SCMValores.CommandText = "update ms_factura set id_usr_autoriza = @id_usr_autoriza, centro_costo = @centro_costo, no_autorizador = @no_autorizador, autorizador = @autorizador, tabla_comp = @tabla_comp where id_ms_factura = @id_ms_factura "
                        SCMValores.Parameters.AddWithValue("@tabla_comp", sFileNameTabCon)

                        'Se agrega el Id al nombre del archivo / Almacenar el archivo en la ruta especificada
                        sFileNameTabCon = .lblFolio.Text + "TabCon-" + sFileNameTabCon
                        fuTablaComp.PostedFile.SaveAs(sFileDir + sFileNameTabCon)
                    End If
                    SCMValores.Parameters.AddWithValue("@id_usr_autoriza", dsEmpleado.Tables(0).Rows(0).Item("id_usr_aut").ToString())
                    SCMValores.Parameters.AddWithValue("@centro_costo", .ddlCC.SelectedItem.Text)
                    SCMValores.Parameters.AddWithValue("@no_autorizador", dsEmpleado.Tables(0).Rows(0).Item("no_empleadoA").ToString())
                    SCMValores.Parameters.AddWithValue("@autorizador", dsEmpleado.Tables(0).Rows(0).Item("Autorizador").ToString())
                    SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    If (Not fuAdjOpc.PostedFile Is Nothing) And (fuAdjOpc.PostedFile.ContentLength > 0) Then
                        'Actualizar Adjunto Opcional
                        ' '' Ruta Local
                        ''Dim sFileDir As String = "C:/ProcAd - Adjuntos/" 'Ruta en que se almacenará el archivo
                        ' Ruta en Atenea
                        Dim sFileDir As String = "D:\ProcAd - Adjuntos\" 'Ruta en que se almacenará el archivo
                        Dim lMaxFileSize As Long = 10485760 'Equivalente a 10 Megabytes
                        Dim sFileNameAdjOpc As String = System.IO.Path.GetFileName(fuAdjOpc.PostedFile.FileName)

                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_factura set adjunto_opcional = @adjunto_opcional where id_ms_factura = @id_ms_factura "
                        SCMValores.Parameters.AddWithValue("@adjunto_opcional", sFileNameAdjOpc)
                        SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Se agrega el Id al nombre del archivo / Almacenar el archivo en la ruta especificada
                        sFileNameAdjOpc = .lblFolio.Text + "AdjOpc-" + sFileNameAdjOpc
                        fuTablaComp.PostedFile.SaveAs(sFileDir + sFileNameAdjOpc)
                    End If

                    ._txtBan.Text = 1

                    'Actualizar Instancia
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 14)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Registrar en Histórico
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                             "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 14)
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Envío de Correo
                    Dim Mensaje As New System.Net.Mail.MailMessage()
                    Dim destinatario As String = ""
                    'Obtener el Correos del Autorizador
                    SCMValores.CommandText = "select cgEmpl.correo from bd_empleado.dbo.cg_empleado cgEmpl where cgEmpl.id_empleado = @idAut "
                    SCMValores.Parameters.AddWithValue("@idAut", .ddlAutorizador.SelectedValue)
                    ConexionBD.Open()
                    destinatario = SCMValores.ExecuteScalar()
                    ConexionBD.Close()

                    Mensaje.[To].Add(destinatario)
                    Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                    'Mensaje.From = New System.Net.Mail.MailAddress("soporte.unne.procad@gmail.com", "ProcAd", System.Text.Encoding.UTF8)
                    Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                    Mensaje.Subject = "ProcAd - Solicitud de Factura No. " + .lblFolio.Text + " por Autorizar"
                    Dim texto As String
                    texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                            "Se actualizó la solicitud número <b>" + .lblFolio.Text + _
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

                    .pnlInicio.Enabled = False
                End While
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
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
                    SCMValores.CommandText = "update ms_factura set status = 'Z' where id_ms_factura = @id_ms_factura "
                    SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Actualizar dt_factura
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update dt_factura " + _
                                             "  set status = 'P' " + _
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
                    SCMValores.Parameters.AddWithValue("@id_actividad", 28)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Registrar en Histórico
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                             "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 28)
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    .pnlInicio.Enabled = False
                End While
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class