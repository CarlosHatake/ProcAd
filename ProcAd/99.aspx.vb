Public Class _99
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

#Region "Soportes"

    Protected Sub btnAgregarSoporte_Click(sender As Object, e As EventArgs) Handles btnAgregarSoporte.Click
        With Me
            Try
                .litError.Text = ""

                ' '' Ruta Local
                ''Dim sFileDir As String = "C:/ProcAd - Adjuntos IngFact/" 'Ruta en que se almacenará el archivo
                ' Ruta en Atenea
                Dim sFileDir As String = "D:\ProcAd - Adjuntos IngFact\" 'Ruta en que se almacenará el archivo
                Dim lMaxFileSize As Long = 10485760 'Equivalente a 10 Megabytes

                'Verificar que el archivo ha sido seleccionado y es un archivo válido
                If (Not fuSoporte.PostedFile Is Nothing) And (fuSoporte.PostedFile.ContentLength > 0) Then
                    'Determinar el nombre original del archivo
                    Dim sFileName As String = System.IO.Path.GetFileName(fuSoporte.PostedFile.FileName)
                    Dim idArchivo As Integer 'Index correspondiente al archivo
                    Dim fecha As DateTime = Date.Now
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    Try
                        If fuSoporte.PostedFile.ContentLength <= lMaxFileSize Then
                            'Registrar el archivo en la base de datos
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "INSERT INTO dt_archivo(id_ms_factura, id_actividad, id_usuario, nombre, fecha, tipo) values(@id_ms_factura, 85, @id_usuario, @nombre, @fecha, @tipo)"
                            SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                            SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                            SCMValores.Parameters.AddWithValue("@nombre", sFileName)
                            SCMValores.Parameters.AddWithValue("@fecha", fecha)
                            SCMValores.Parameters.AddWithValue("@tipo", "S")
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()
                            'Obtener el Id del archivo
                            SCMValores.CommandText = "select max(id_dt_archivo) from dt_archivo where (id_ms_factura = @id_ms_factura) and (fecha = @fecha)"
                            ConexionBD.Open()
                            idArchivo = SCMValores.ExecuteScalar
                            ConexionBD.Close()
                            'Se agrega el Id al nombre del archivo
                            sFileName = idArchivo.ToString + "-" + sFileName
                            'Almacenar el archivo en la ruta especificada
                            fuSoporte.PostedFile.SaveAs(sFileDir + sFileName)
                            'lblMessage.Visible = True
                            'lblMessage.Text = "El archivo: " + sFileName + " se ha adjuntado exitosamente"

                            actualizarSoportes()
                        Else
                            'Rechazar el archivo
                            lblMessage.Visible = True
                            lblMessage.Text = "El archivo excede el límite de 10 MB"
                        End If
                    Catch exc As Exception    'En caso de error
                        'Eliminar el archivo en la base de datos
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "delete from dt_archivo where id_dt_archivo = @idArchivo"
                        SCMValores.Parameters.AddWithValue("@idArchivo", idArchivo)
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        lblMessage.Visible = True
                        lblMessage.Text = lblMessage.Text + ". Un Error ha ocurrido. Favor de intentarlo nuevamente"
                        DeleteFile(sFileDir + sFileName)
                    End Try
                Else
                    lblMessage.Visible = True
                    lblMessage.Text = "Favor de seleccionar un Archivo"
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With

    End Sub

    Private Sub DeleteFile(ByVal strFileName As String)
        If strFileName.Trim().Length > 0 Then
            Dim fi As New FileInfo(strFileName)
            If (fi.Exists) Then    'Si existe el archivo, eliminarlo
                fi.Delete()
            End If
        End If
    End Sub

    Public Sub actualizarSoportes()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaArchivos As New SqlDataAdapter
                Dim dsArchivos As New DataSet
                .gvSoportes.DataSource = dsArchivos
                'Evidencias
                sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " + _
                                                           "     , 'http://148.223.153.43/ProcAd - Adjuntos IngFact/' + cast(id_dt_archivo as varchar(20)) + '-' + nombre as path " + _
                                                           "from dt_archivo " + _
                                                           "where id_ms_factura = @idMsFactura " + _
                                                           "  and tipo = 'S' ", ConexionBD)
                sdaArchivos.SelectCommand.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaArchivos.Fill(dsArchivos)
                .gvSoportes.DataBind()
                ConexionBD.Close()
                sdaArchivos.Dispose()
                dsArchivos.Dispose()
                .gvSoportes.SelectedIndex = -1
                .upSoportes.Update()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
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
                    SCMValores.CommandText = "update ms_factura set status = 'VP' where id_ms_factura = @id_ms_factura "
                    SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    ._txtBan.Text = 1

                    'Actualizar Instancia
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 98)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Registrar en Histórico
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                             "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 98)
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Envío de Correo
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
                    Mensaje.Subject = "ProcAd - Solicitud de Servicio Negociado No. " + .lblFolio.Text + " por Validar Presupuesto"
                    Dim texto As String
                    texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                            "Se amplió el presupuesto de la solicitud de Servicio Negociado número <b>" + .lblFolio.Text + _
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

                    .pnlInicio.Enabled = False

                    Session("id_actividadM") = 99
                    Session("TipoM") = "SN"
                    Server.Transfer("Menu.aspx")
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
                    SCMValores.CommandText = "update ms_factura set status = 'ZU' where id_ms_factura = @id_ms_factura "
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
                    SCMValores.Parameters.AddWithValue("@id_actividad", 97)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Registrar en Histórico
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                             "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 97)
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    .pnlInicio.Enabled = False

                    Session("id_actividadM") = 99
                    Session("TipoM") = "SN"
                    Server.Transfer("Menu.aspx")
                End While
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class