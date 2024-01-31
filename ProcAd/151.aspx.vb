Public Class _151
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                _txtIdUsuario.Text = Session("id_usuario")
                _txtIdMsInst.Text = Session("idMsInst")
                _txtBan.Text = 0

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaSol As New SqlDataAdapter
                Dim dsSol As New DataSet
                sdaSol.SelectCommand = New SqlCommand("select " +
                                                              " empleado_solicita, MCA.empresa, id_ms_comprobacion_anticipo as folio, " +
                                                              "  MCA.autorizador, MCA.tipo_escenario, " +
                                                              "  MCA.id_usr_autorizador2, MCA.id_usr_autorizador3, " +
                                                              "  isnull(cgEmpl2.nombre + ' ' + cgEmpl2.ap_paterno + ' ' + cgEmpl2.ap_materno, 'XX') as autorizador2, " +
                                                              "  isnull(cgEmpl3.nombre + ' ' + cgEmpl3.ap_paterno + ' ' + cgEmpl3.ap_materno, 'XX') as autorizador3, " +
                                                              "  proveedor, division, centro_costos " +
                                                              "  from ms_comprobacion_anticipo MCA " +
                                                              "  inner join ms_instancia MSA on MCA.id_ms_comprobacion_anticipo = MSA.id_ms_sol and Tipo = 'CAP' " +
                                                              "  left join cg_usuario cgAut2 on MCA.id_usr_autorizador2 = cgAut2.id_usuario " +
                                                              "  left join bd_Empleado.dbo.cg_empleado cgEmpl2 on cgAut2.id_empleado = cgEmpl2.id_empleado " +
                                                              "  left join cg_usuario cgAut3 on MCA.id_usr_autorizador3 = cgAut3.id_usuario " +
                                                              "  left join bd_Empleado.dbo.cg_empleado cgEmpl3 on cgAut3.id_empleado = cgEmpl3.id_empleado " +
                                                              "  where MSA.id_ms_instancia = @id_ms_instancia ", ConexionBD)
                sdaSol.SelectCommand.Parameters.AddWithValue("@id_ms_instancia", Val(_txtIdMsInst.Text))
                ConexionBD.Open()
                sdaSol.Fill(dsSol)
                ConexionBD.Close()
                lblFolio.Text = dsSol.Tables(0).Rows(0).Item("folio").ToString()
                lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("empleado_solicita").ToString()
                lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                lblCC.Text = dsSol.Tables(0).Rows(0).Item("centro_costos").ToString()
                lblDivision.Text = dsSol.Tables(0).Rows(0).Item("division").ToString()
                lblProveedor.Text = dsSol.Tables(0).Rows(0).Item("proveedor").ToString()
                lblAutorizador.Text = dsSol.Tables(0).Rows(0).Item("autorizador").ToString()
                _txtTipoEscenario.Text = dsSol.Tables(0).Rows(0).Item("tipo_escenario").ToString()
                If dsSol.Tables(0).Rows(0).Item("autorizador2").ToString() = "XX" Then
                    lbl_Autorizador2.Visible = False
                    lblAutorizador2.Visible = False
                Else
                    lbl_Autorizador2.Visible = True
                    lblAutorizador2.Visible = True
                    lblAutorizador2.Text = dsSol.Tables(0).Rows(0).Item("autorizador2").ToString()
                End If
                If dsSol.Tables(0).Rows(0).Item("autorizador3").ToString() = "XX" Then
                    lbl_Autorizador3.Visible = False
                    lblAutorizador3.Visible = False
                Else
                    lbl_Autorizador3.Visible = True
                    lblAutorizador3.Visible = True
                    lblAutorizador3.Text = dsSol.Tables(0).Rows(0).Item("autorizador3").ToString()
                End If
                sdaSol.Dispose()
                dsSol.Dispose()



                'Facturas
                Dim sdaFacturas As New SqlDataAdapter
                Dim dsFacturas As New DataSet
                gvFacturas.DataSource = dsFacturas
                'Habilitar columnas para actualización
                gvFacturas.Columns(0).Visible = True
                If _txtTipoEscenario.Text = "Anticipo sin pedido de compra" Then
                    gvFacturas.Columns(1).Visible = False
                Else
                    gvFacturas.Columns(1).Visible = True
                End If
                sdaFacturas.SelectCommand = New SqlCommand("select id_dt_comprobacion_anticipo " +
                                                           "     , fecha_emision " +
                                                           "     , uuid " +
                                                           "     , lugar_exp " +
                                                           "     , forma_pago " +
                                                           "     , moneda " +
                                                           "     , subtotal " +
                                                           "     , importe " +
                                                           "from dt_comprobacion_anticipo " +
                                                           "where id_ms_comp_anticipo = @id_ms_comp_anticipo", ConexionBD)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@id_ms_comp_anticipo", Val(lblFolio.Text))
                ConexionBD.Open()
                sdaFacturas.Fill(dsFacturas)
                gvFacturas.DataBind()
                ConexionBD.Close()
                sdaFacturas.Dispose()
                dsFacturas.Dispose()
                gvFacturas.SelectedIndex = -1
                'Inhabilitar columnas para vista
                gvFacturas.Columns(0).Visible = False


                'Panel
                pnlInicio.Enabled = True
                If _txtTipoEscenario.Text = "5" Then
                    llenarGridRegistrosDT()
                Else
                    llenarRegistros()
                End If
            Else
                Server.Transfer("Login.aspx")
            End If

        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

#End Region

#Region "Funciones"

    Public Sub llenarGridRegistrosDT()
        Try
            litError.Text = ""
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim sdaCatalogo As New SqlDataAdapter
            Dim dsCatalogo As New DataSet
            ' gvRegistros.Columns(0).Visible = True
            gvRegistros.DataSource = dsCatalogo
            Dim query As String = ""

            query = " select * from dt_comprobacion_anticipo_cuenta where id_ms_comprobacion_anticipo = @id_ms_comprobacion_anticipo and id_dt_comprobacion_anticipo = @id_dt_comprobacion_anticipo "

            sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
            sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_ms_comprobacion_anticipo", Val(lblFolio.Text))
            If _txtIdDTComprobacionAnticipo.Text = "" Then
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_dt_comprobacion_anticipo", Val(gvFacturas.Rows(0).Cells(0).Text))
            Else
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_dt_comprobacion_anticipo", Val(_txtIdDTComprobacionAnticipo.Text))

            End If
            ConexionBD.Open()
            sdaCatalogo.Fill(dsCatalogo)
            gvRegistros.DataBind()
            ConexionBD.Close()
            sdaCatalogo.Dispose()
            dsCatalogo.Dispose()
            'gvRegistros.Columns(0).Visible = False

        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Public Sub llenarRegistros()
        Try
            Dim ban As Integer = 0
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim sdaCatalogo As New SqlDataAdapter
            Dim dsCatalogo As New DataSet
            gvRegistros.DataSource = dsCatalogo
            Dim query As String = ""

            'SOLO VAN LAS 3'
            query = " select * from dt_comprobacion_anticipo_cuenta where id_ms_comprobacion_anticipo = @id_ms_comprobacion_anticipo"
            sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
            sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_ms_comprobacion_anticipo", Val(lblFolio.Text))
            ConexionBD.Open()
            sdaCatalogo.Fill(dsCatalogo)
            gvRegistros.DataBind()
            ConexionBD.Close()
            sdaCatalogo.Dispose()
            dsCatalogo.Dispose()

        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvFacturas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvFacturas.SelectedIndexChanged
        Try
            _txtIdDTComprobacionAnticipo.Text = gvFacturas.SelectedRow().Cells(0).Text
            llenarGridRegistrosDT()
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

#End Region

#Region "Botones"
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Try

            litError.Text = ""
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
            SCMValores.Connection = ConexionBD
            'Actualizar datos de la Solicitud
            SCMValores.CommandText = ""
            SCMValores.Parameters.Clear()
            SCMValores.CommandText = "update ms_comprobacion_anticipo set  fecha_valida_cxp = @fecha_valida_cxp,  estatus = 'RN' where id_ms_comprobacion_anticipo = @id_ms_comprobacion_anticipo "
            SCMValores.Parameters.AddWithValue("@fecha_valida_cxp", Date.Now)
            SCMValores.Parameters.AddWithValue("@id_ms_comprobacion_anticipo", Val(lblFolio.Text))
            ConexionBD.Open()
            SCMValores.ExecuteNonQuery()
            ConexionBD.Close()

            'Actividad de fin de flujo'
            Dim idActividad As Integer
            idActividad = 152

            'Actualizar Instancia
            SCMValores.Parameters.Clear()
            SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
            SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(_txtIdMsInst.Text))
            SCMValores.Parameters.AddWithValue("@id_actividad", idActividad)
            ConexionBD.Open()
            SCMValores.ExecuteNonQuery()
            ConexionBD.Close()

            'Registrar en Histórico
            SCMValores.Parameters.Clear()
            SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                                          "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
            SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(_txtIdMsInst.Text))
            SCMValores.Parameters.AddWithValue("@id_actividad", idActividad)
            SCMValores.Parameters.AddWithValue("@fecha", Date.Now)
            SCMValores.Parameters.AddWithValue("@id_usr", Val(_txtIdUsuario.Text))
            ConexionBD.Open()
            SCMValores.ExecuteNonQuery()
            ConexionBD.Close()

            btnAceptar.Enabled = False
            btnRechaza.Visible = False
            txtComentario.Enabled = False

        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnRechaza_Click(sender As Object, e As EventArgs) Handles btnRechaza.Click
        Try
            If txtComentario.Text = "" Then
                litError.Text = "Favor de escribir un comentario"
            Else
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                'Actualizar datos de la Solicitud
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "update ms_comprobacion_anticipo set estatus = 'Z' where id_ms_comprobacion_anticipo = @id_ms_comprobacion_anticipo "
                SCMValores.Parameters.AddWithValue("@id_ms_comprobacion_anticipo", Val(lblFolio.Text))
                'SCMValores.Parameters.AddWithValue("@comentario_codc", txtComentario.Text.Trim)
                'SCMValores.Parameters.AddWithValue("@id_usr_codCont", Val(_txtIdUsuario.Text))
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                Dim idActividad As Integer
                idActividad = 151
                'Actividad para cancelar'

                'Actualizar Instancia
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(_txtIdMsInst.Text))
                SCMValores.Parameters.AddWithValue("@id_actividad", idActividad)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                'Registrar en Histórico
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                                         "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(_txtIdMsInst.Text))
                SCMValores.Parameters.AddWithValue("@id_actividad", idActividad)
                SCMValores.Parameters.AddWithValue("@fecha", Date.Now)
                SCMValores.Parameters.AddWithValue("@id_usr", Val(_txtIdUsuario.Text))
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                'Envío de Correo
                Dim Mensaje As New System.Net.Mail.MailMessage()
                Dim destinatario As String = ""
                'Obtener el Correo del Solicitante
                SCMValores.CommandText = "select cgEmpl.correo " +
                                                 "from ms_comprobacion_anticipo " +
                                                 "  left join cg_usuario on ms_comprobacion_anticipo.id_usr_solicita = cg_usuario.id_usuario " +
                                                 "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado  " +
                                                 "where id_ms_comprobacion_anticipo  = @id_ms_comprobacion_anticipo "
                SCMValores.Parameters.AddWithValue("@id_ms_comprobacion_anticipo", Val(lblFolio.Text))
                ConexionBD.Open()
                destinatario = SCMValores.ExecuteScalar()
                ConexionBD.Close()

                Mensaje.[To].Add(destinatario)
                Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                Mensaje.Subject = "ProcAd - Solicitud de Comprobacion Anticipo: " + lblFolio.Text + " Rechazada"
                Dim texto As String
                texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                                "La solicitud número <b>" + lblFolio.Text + "</b> fue rechazada.  <br>" +
                                "Comentarios: <b>" + txtComentario.Text.Trim + "</b><br></span>"
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

            End If
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub


#End Region

End Class