Public Class _124
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        _txtIdUsuario.Text = Session("id_usuario")
                        _txtIdMsInst.Text = Session("idMsInst")
                        _txtBan.Text = 0

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim sdaSol As New SqlDataAdapter
                        Dim dsSol As New DataSet
                        Dim query As String
                        query = " select " +
                                "(select nombre + ' ' + ap_paterno + ' ' + ap_materno from bd_Empleado.dbo.cg_empleado where id_empleado = CG.id_empleado) as solicita, " +
                                "Case MSI.tipoM When 'CPP' then 'Cuentas Por Pagar' else 'Cuentas Por Cobrar' end as TipoMovimiento, " +
                                "MSI.empresa, " +
                                "MSI.admon_oper, " +
                                "(select nombre + ' ' + ap_paterno + ' ' + ap_materno from bd_Empleado.dbo.cg_empleado where id_empleado = CGU.id_empleado) as autoriza, " +
                                "MSI.centro_costo, " +
                                "MSI.division, " +
                                "MSI.especificaciones, " +
                                "MSI.id_ms_movimientos_internos " +
                                "From ms_movimientos_internos MSI " +
                                "Left Join ms_instancia MI on MI.id_ms_sol = MSI.id_ms_movimientos_internos " +
                                "Left Join cg_usuario CG on CG.id_usuario = MSI.id_usr_solicita " +
                                "Left Join cg_usuario CGU on CGU.id_usuario = MSI.id_usr_autoriza " +
                                "where id_ms_instancia = @idMsInst "
                        sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@idMsInst", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()
                        lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("solicita").ToString()
                        lblTipo.Text = dsSol.Tables(0).Rows(0).Item("TipoMovimiento").ToString()
                        lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                        lblAutorizador.Text = dsSol.Tables(0).Rows(0).Item("autoriza").ToString()
                        lblCentroCosto.Text = dsSol.Tables(0).Rows(0).Item("centro_costo").ToString()
                        lblDivision.Text = dsSol.Tables(0).Rows(0).Item("division").ToString()
                        txtEspecificaciones.Text = dsSol.Tables(0).Rows(0).Item("especificaciones").ToString()
                        txtEspecificaciones.Enabled = False
                        If dsSol.Tables(0).Rows(0).Item("admon_oper").ToString = "Admon" Then
                            rbAdministrativo.Checked = True
                            rbAdministrativo.Enabled = False
                            rbOperativo.Enabled = False
                        Else
                            rbOperativo.Checked = True
                            rbOperativo.Enabled = False
                            rbAdministrativo.Enabled = False
                        End If
                        lblFolio.Text = dsSol.Tables(0).Rows(0).Item("id_ms_movimientos_internos").ToString
                        'Llenar Grid de Evidencias'
                        Dim sdaArchivos As New SqlDataAdapter
                        Dim dsArchivos As New DataSet
                        gvAdjuntos.DataSource = dsArchivos
                        'Ruta Servidor Prueba 172.16.18.239'
                        'Ruta Servidor Bueno 148.223.153.43'
                        sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " +
                                                                   "     , 'http://148.223.153.43/ProcAd/Evidencias MovLib/' + cast(id_ms_movimientos_internos as varchar(20)) + '-' + nombre as path " +
                                                                   "from dt_archivo_movInt " +
                                                                   "where id_ms_movimientos_internos = @idMsMovInt ", ConexionBD)
                        sdaArchivos.SelectCommand.Parameters.AddWithValue("@idMsMovInt", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        sdaArchivos.Fill(dsArchivos)
                        .gvAdjuntos.DataBind()
                        ConexionBD.Close()
                        sdaArchivos.Dispose()
                        dsArchivos.Dispose()
                        .gvAdjuntos.SelectedIndex = -1

                    Else
                        Server.Transfer("Login.aspx")
                    End If
                Catch ex As Exception
                    litError.Text = ex.Message
                End Try
            End With
        End If
    End Sub


    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Try
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
            SCMValores.Connection = ConexionBD
            'Actualizar datos de la Solicitud
            SCMValores.CommandText = ""
            SCMValores.Parameters.Clear()
            SCMValores.CommandText = "update ms_movimientos_internos set fecha_autoriza = @fecha_autoriza, comentario_autoriza = @comentario_autoriza, estatus = 'P' where id_ms_movimientos_internos = @id_ms_movimientos_internos "
            SCMValores.Parameters.AddWithValue("@fecha_autoriza", Date.Now)
            SCMValores.Parameters.AddWithValue("@comentario_autoriza", txtComentario.Text.Trim)
            SCMValores.Parameters.AddWithValue("@id_ms_movimientos_internos", Val(lblFolio.Text))
            ConexionBD.Open()
            SCMValores.ExecuteNonQuery()
            ConexionBD.Close()

            Dim idActividad As Integer
            idActividad = 125

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
            btnRechazar.Visible = False
            txtComentario.Enabled = False

        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnRechazar_Click(sender As Object, e As EventArgs) Handles btnRechazar.Click
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
                SCMValores.CommandText = "update ms_movimientos_internos set fecha_autoriza = @fecha_autoriza, comentario_autoriza = @comentario_autoriza, estatus = 'Z' where id_ms_movimientos_internos = @id_ms_movimientos_internos "
                SCMValores.Parameters.AddWithValue("@fecha_autoriza", Date.Now)
                SCMValores.Parameters.AddWithValue("@comentario_autoriza", txtComentario.Text.Trim)
                SCMValores.Parameters.AddWithValue("@id_ms_movimientos_internos", Val(lblFolio.Text))
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                Dim idActividad As Integer
                idActividad = 126
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
                                                 "from ms_movimientos_internos " +
                                                 "  left join cg_usuario on ms_movimientos_internos.id_usr_solicita = cg_usuario.id_usuario " +
                                                 "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado  " +
                                                 "where id_ms_movimientos_internos = @id_ms_movInt "
                SCMValores.Parameters.AddWithValue("@id_ms_movInt", Val(lblFolio.Text))
                ConexionBD.Open()
                destinatario = SCMValores.ExecuteScalar()
                ConexionBD.Close()

                Mensaje.[To].Add(destinatario)
                Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                Mensaje.Subject = "ProcAd - Solicitud de Movimientos Internos " + lblFolio.Text + " Rechazada"
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
End Class