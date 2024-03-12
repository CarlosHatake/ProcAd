Public Class _135
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
                        Dim query As String
                        Dim sdaSolicitud As New SqlDataAdapter
                        Dim dsSolicitud As New DataSet


                        query = " SELECT id_ms_anticipo_proveedor, empleado_solicita,importe_requerido, empresa, justificacion, prov.rfc as RFCProveedor, prov.nombre as Proveedor FROM ms_anticipo_proveedor " +
                        " LEFT JOIN ms_instancia ON ms_anticipo_proveedor.id_ms_anticipo_proveedor = ms_instancia.id_ms_sol " +
                        " LEFT JOIN bd_SiSAC.dbo.cg_proveedor prov ON ms_anticipo_proveedor.id_proveedor = prov.id_proveedor " +
                        " WHERE id_ms_instancia = @id_ms_instancia"
                        sdaSolicitud.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaSolicitud.Fill(dsSolicitud)
                        ConexionBD.Close()
                        .lblFolio.Text = dsSolicitud.Tables(0).Rows(0).Item("id_ms_anticipo_proveedor").ToString()
                        .lblSolicitante.Text = dsSolicitud.Tables(0).Rows(0).Item("empleado_solicita").ToString()
                        .lblImporte.Text = "$" + dsSolicitud.Tables(0).Rows(0).Item("importe_requerido").ToString()
                        .lblEmpresa.Text = dsSolicitud.Tables(0).Rows(0).Item("empresa").ToString()
                        .txtJustificacion.Text = dsSolicitud.Tables(0).Rows(0).Item("justificacion").ToString()
                        .lblProveedor.Text = dsSolicitud.Tables(0).Rows(0).Item("Proveedor").ToString()
                        .lblNumProveedor.Text = dsSolicitud.Tables(0).Rows(0).Item("RFCProveedor").ToString()

                        'Consulta de Adjuntos
                        Dim sdaArchivos As New SqlDataAdapter
                        Dim dsArchivos As New DataSet
                        .gvAdjuntos.DataSource = dsArchivos
                        'Adjuntos
                        sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " +
                                                                   "     , 'http://148.223.153.43/ProcAd/Adjuntos AntProv/' + nombre as path " +
                                                                   "from dt_archivo_adj_anticipo " +
                                                                   "where id_ms_anticipo_proveedor = @id_ms_anticipo_proveedor ", ConexionBD)
                        sdaArchivos.SelectCommand.Parameters.AddWithValue("@id_ms_anticipo_proveedor", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        sdaArchivos.Fill(dsArchivos)
                        .gvAdjuntos.DataBind()
                        ConexionBD.Close()
                        sdaArchivos.Dispose()
                        dsArchivos.Dispose()
                        .gvAdjuntos.SelectedIndex = -1
                        .upAdjuntos.Update()
                        llenarGvPedidosCompra()
                        If gvPedidosCompras.Rows.Count = 0 Then
                            .lbl_Pedido.Visible = False
                            .gvPedidosCompras.Visible = False
                        End If
                    Else
                        Server.Transfer("Login.aspx")
                    End If

                Catch ex As Exception
                    .litError.Text = ex.ToString()
                End Try

            End With
        End If
    End Sub

    Public Sub llenarGvPedidosCompra()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaPedidosC As New SqlDataAdapter
                Dim dsPedididosC As New DataSet
                .gvPedidosCompras.DataSource = dsPedididosC
                sdaPedidosC.SelectCommand = New SqlCommand(" SELECT id_dt_pedidos_compra, pedido_compra, total " +
                                                           " FROM dt_pedidos_compra " +
                                                           " WHERE id_ms_anticipo_proveedor = @id_ms_anticipo_proveedor ", ConexionBD)
                sdaPedidosC.SelectCommand.Parameters.AddWithValue("@id_ms_anticipo_proveedor", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaPedidosC.Fill(dsPedididosC)
                .gvPedidosCompras.DataBind()
                ConexionBD.Close()
                sdaPedidosC.Dispose()
                dsPedididosC.Dispose()
                .gvPedidosCompras.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub
#End Region


#Region "Botones"
    Protected Sub btnAutorizar_Click(sender As Object, e As EventArgs) Handles btnAutorizar.Click
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD

                Dim fecha As DateTime = Date.Now
                Dim valor As Integer = 0

                'Autorizar la solicitud
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = " DECLARE @valorR int; EXEC SP_U_ms_anticipo_proveedor_autorizador @id_ms_anticipo_proveedor, @fecha_reg_autorizador, @comentario_autorizador, @id_ms_instancia, @id_actividad, @fecha, @id_usr, @valorR OUTPUT; select @valorR"
                SCMValores.Parameters.AddWithValue("@id_ms_anticipo_proveedor", .lblFolio.Text)
                SCMValores.Parameters.AddWithValue("@fecha_reg_autorizador", fecha)
                If .txtObservaciones.Text = "" Then
                    SCMValores.Parameters.AddWithValue("@comentario_autorizador", DBNull.Value)
                Else
                    SCMValores.Parameters.AddWithValue("@comentario_autorizador", .txtObservaciones.Text)
                End If
                SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                SCMValores.Parameters.AddWithValue("@id_actividad", 136)
                SCMValores.Parameters.AddWithValue("@fecha", fecha)
                SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                valor = SCMValores.ExecuteScalar()
                ConexionBD.Close()

                If valor = 1 Then
                    .pnlInicio.Enabled = False
                Else
                    Server.Transfer("Menu.aspx")
                End If
                SCMValores.Parameters.Clear()
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With

    End Sub


    Protected Sub btnRechazar_Click(sender As Object, e As EventArgs) Handles btnRechazar.Click
        With Me
            Try
                If .txtObservaciones.Text = "" Then
                    .litError.Text = "Escriba las observaciones correspondientes"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD

                    Dim fecha As DateTime = Date.Now
                    Dim valor As Integer = 0

                    'Rechazar la solicitud
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = " DECLARE @valorR int; EXEC SP_U_ms_anticipo_proveedor_autorizador @id_ms_anticipo_proveedor, @fecha_reg_autorizador, @comentario_autorizador, @id_ms_instancia, @id_actividad, @fecha, @id_usr, @valorR OUTPUT; select @valorR"
                    SCMValores.Parameters.AddWithValue("@id_ms_anticipo_proveedor", .lblFolio.Text)
                    SCMValores.Parameters.AddWithValue("@fecha_reg_autorizador", fecha)
                    SCMValores.Parameters.AddWithValue("@comentario_autorizador", .txtObservaciones.Text)
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 137)
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    valor = SCMValores.ExecuteScalar()
                    ConexionBD.Close()

                    If valor = 1 Then
                        .pnlInicio.Enabled = False

                        'Envío de Correo
                        Dim Mensaje As New System.Net.Mail.MailMessage()
                        Dim destinatario As String = ""
                        'Obtener el Correo del Solicitante
                        SCMValores.CommandText = "select cgEmpl.correo " +
                                                         "from ms_anticipo_proveedor " +
                                                         "  left join cg_usuario on ms_anticipo_proveedor.id_usr_solicita = cg_usuario.id_usuario " +
                                                         "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado  " +
                                                         "where id_ms_anticipo_proveedor  = @id_ms_anticipo_proveedor "
                        SCMValores.Parameters.AddWithValue("@id_ms_anticipo_proveedor", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        destinatario = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        Mensaje.[To].Add(destinatario)
                        Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                        Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                        Mensaje.Subject = "ProcAd - Solicitud de Comprobacion Anticipo: " + .lblFolio.Text + " Rechazada"
                        Dim texto As String
                        texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                                        "La solicitud número <b>" + lblFolio.Text + "</b> fue rechazada.  <br>" +
                                        "Comentarios: <b>" + txtObservaciones.Text.Trim + "</b><br></span>"
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
                            litError.Text = ex.ToString
                        End Try

                    Else
                        Server.Transfer("Menu.aspx")
                    End If
                    SCMValores.Parameters.Clear()
                End If

            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With

    End Sub
#End Region
End Class