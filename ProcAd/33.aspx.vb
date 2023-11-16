Public Class _33
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
                        ._txtIdDtCargaComb.Text = Session("idDtCargaComb")
                        ._txtBan.Text = 0

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim sdaSol As New SqlDataAdapter
                        Dim dsSol As New DataSet
                        Dim query As String
                        query = "select fecha " + _
                                "     , identificador_vehiculo as unidad " + _
                                "     , placa " + _
                                "     , msVehi.marca " + _
                                "     , msVehi.modelo " + _
                                "     , num_tarjeta " + _
                                "     , no_comprobante " + _
                                "     , razon_social_afiliado " + _
                                "     , cantidad_mercancia " + _
                                "     , precio_ticket " + _
                                "     , importe_con_ieps " + _
                                "     , iva " + _
                                "     , importe_transaccion " + _
                                "     , km_transaccion " + _
                                "     , dt_carga_comb.rendimiento " + _
                                "     , rendimiento_real " + _
                                "     , ((rendimiento_real - dt_carga_comb.rendimiento) / dt_carga_comb.rendimiento) as desv_real " + _
                                "     , isnull(msVehi.porcent_tolerancia, .1) as tolerancia " + _
                                "     , case when ((rendimiento_real - dt_carga_comb.rendimiento) / dt_carga_comb.rendimiento) + isnull(msVehi.porcent_tolerancia, .1) < 0 then 'S' else 'N' end bloqueoST " + _
                                "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as nombre_conductor " + _
                                "from dt_carga_comb " + _
                                "  left join bd_Empleado.dbo.ms_vehiculo msVehi on dt_carga_comb.num_tarjeta = msVehi.no_tarjeta_edenred " + _
                                "  left join bd_Empleado.dbo.cg_empleado cgEmpl on dt_carga_comb.id_conductor = cgEmpl.id_conductor_edenred and cgEmpl.status = 'A' " + _
                                "where id_dt_carga_comb = @id_dt_carga_comb "
                        sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@id_dt_carga_comb", Val(._txtIdDtCargaComb.Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()
                        .lblUnidad.Text = dsSol.Tables(0).Rows(0).Item("unidad").ToString()
                        .lblPlaca.Text = dsSol.Tables(0).Rows(0).Item("placa").ToString()
                        .lblNoTarjeta.Text = dsSol.Tables(0).Rows(0).Item("num_tarjeta").ToString()
                        .lblMarca.Text = dsSol.Tables(0).Rows(0).Item("marca").ToString()
                        .lblModelo.Text = dsSol.Tables(0).Rows(0).Item("modelo").ToString()
                        .lblFecha.Text = dsSol.Tables(0).Rows(0).Item("fecha").ToString()
                        .lblEstacion.Text = dsSol.Tables(0).Rows(0).Item("razon_social_afiliado").ToString()
                        .lblNoTicket.Text = dsSol.Tables(0).Rows(0).Item("no_comprobante").ToString()
                        .wneLitros.Value = Val(dsSol.Tables(0).Rows(0).Item("cantidad_mercancia").ToString())
                        .wcePrecio.Value = Val(dsSol.Tables(0).Rows(0).Item("precio_ticket").ToString())
                        .wceSubtotal.Value = Val(dsSol.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                        .wceIVA.Value = Val(dsSol.Tables(0).Rows(0).Item("iva").ToString())
                        .wceTotal.Value = Val(dsSol.Tables(0).Rows(0).Item("importe_transaccion").ToString())
                        .wneOdometroE.Value = Val(dsSol.Tables(0).Rows(0).Item("km_transaccion").ToString())
                        ._txtRendimiento.Text = dsSol.Tables(0).Rows(0).Item("rendimiento").ToString()
                        ._txtRendimientoReal.Text = dsSol.Tables(0).Rows(0).Item("rendimiento_real").ToString()
                        ._txtDesvReal.Text = dsSol.Tables(0).Rows(0).Item("desv_real").ToString()
                        ._txtTolerancia.Text = dsSol.Tables(0).Rows(0).Item("tolerancia").ToString()
                        ._txtBloqueoST.Text = dsSol.Tables(0).Rows(0).Item("bloqueoST").ToString()
                        ._txtConductor.Text = dsSol.Tables(0).Rows(0).Item("nombre_conductor").ToString()
                        sdaSol.Dispose()
                        dsSol.Dispose()

                        'Panel
                        .btnAceptar.Visible = True
                        .btnConfirmar.Visible = False
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

#Region "Funciones"

    Public Sub actCargaComb()
        With Me
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
            SCMValores.Connection = ConexionBD

            'Foto del Odómetro
            ' '' Ruta Local
            ''Dim sFileDir As String = "C:/ProcAd - Adjuntos GasC/" 'Ruta en que se almacenará el archivo
            ' Ruta en Atenea
            Dim sFileDir As String = "D:\ProcAd - Adjuntos GasC\" 'Ruta en que se almacenará el archivo
            Dim lMaxFileSize As Long = 10485760 'Equivalente a 10 Megabytes
            Dim sFileNameFotoH As String = System.IO.Path.GetFileName(fuOdometroF.PostedFile.FileName)

            Dim fecha As DateTime = Date.Now
            While Val(._txtBan.Text) = 0
                'Actualizar datos de la Solicitud
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "update dt_carga_comb set status = 'R', obs_evid = @obs_evid, bloqueo_st = @bloqueo_st, desviacion_real = @desviacion_real, tolerancia_unidad = @tolerancia_unidad, foto_evid = @foto_evid, odometro_evid = @odometro_evid, existe_dif = @existe_dif, diferencia = @diferencia, id_usr_evid = @id_usr_evid, fecha_evid = @fecha_evid where id_dt_carga_comb = @id_dt_carga_comb "
                SCMValores.Parameters.AddWithValue("@obs_evid", .txtObs.Text.Trim)
                SCMValores.Parameters.AddWithValue("@bloqueo_st", ._txtBloqueoST.Text)
                SCMValores.Parameters.AddWithValue("@desviacion_real", Val(._txtDesvReal.Text))
                SCMValores.Parameters.AddWithValue("@tolerancia_unidad", Val(._txtTolerancia.Text))
                SCMValores.Parameters.AddWithValue("@foto_evid", sFileNameFotoH)
                SCMValores.Parameters.AddWithValue("@odometro_evid", .wneOdometroU.Value)
                If (.wneOdometroE.Value - .wneOdometroU.Value > 5) Or (.wneOdometroE.Value - .wneOdometroU.Value < -5) Then
                    'Hay diferencia
                    SCMValores.Parameters.AddWithValue("@existe_dif", "S")
                Else
                    'No hay diferencia
                    SCMValores.Parameters.AddWithValue("@existe_dif", "N")
                End If
                SCMValores.Parameters.AddWithValue("@diferencia", .wneOdometroE.Value - .wneOdometroU.Value)
                SCMValores.Parameters.AddWithValue("@id_usr_evid", Val(._txtIdUsuario.Text))
                SCMValores.Parameters.AddWithValue("@fecha_evid", fecha)
                SCMValores.Parameters.AddWithValue("@id_dt_carga_comb", Val(._txtIdDtCargaComb.Text))
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                'Guardar Archivos Adjuntos
                If (Not fuOdometroF.PostedFile Is Nothing) And (fuOdometroF.PostedFile.ContentLength > 0) Then
                    'Se agrega el Id al nombre del archivo / Almacenar el archivo en la ruta especificada
                    sFileNameFotoH = ._txtIdDtCargaComb.Text + "FotoHE-" + sFileNameFotoH
                    fuOdometroF.PostedFile.SaveAs(sFileDir + sFileNameFotoH)
                End If

                ._txtBan.Text = 1

                If ._txtBloqueoST.Text = "S" Then
                    .litError.Text = "Se realiza Bloqueo por Rendimiento ya que el Rendimiento esperado es " + ._txtRendimiento.Text + " y el real fue " + ._txtRendimientoReal.Text + ", favor de contactar al departamento de Soporte Técnico para la aclaración"

                    'Envío de Notificación a Soporte Técnico
                    Dim Mensaje As New System.Net.Mail.MailMessage()
                    Dim destinatario As String = ""
                    'Obtener el Correo de Soporte Técnico
                    SCMValores.CommandText = "select valor from cg_parametros where parametro = 'mail_ST' "
                    ConexionBD.Open()
                    destinatario = SCMValores.ExecuteScalar()
                    ConexionBD.Close()

                    Mensaje.[To].Add(destinatario)
                    Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                    Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                    Mensaje.Subject = "ProcAd - Bloqueo por Rendiento, unidad " + .lblUnidad.Text
                    Dim texto As String
                    texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                            "Se bloqueó al conductor <b>" + ._txtConductor.Text + _
                            "</b> ya que el rendimiento esperado es " + ._txtRendimiento.Text + " y el real fue " + ._txtRendimientoReal.Text + ", teniendo ademas una tolerancia de "(Val(._txtTolerancia.Text) * 100).ToString + "%" + _
                            "</b><br><br>Favor de determinar si procede el desbloqueo </span>"
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
            End While
        End With
    End Sub

#End Region

#Region "Registrar"

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""

                If Val(._txtBan.Text) = 1 Then
                    .litError.Text = "Ya se almacenó el registro previamente, favor de validarlo en el apartado de Consulta de Cargas de Combustible"
                Else
                    If .txtObs.Text.Trim = "" Then
                        .litError.Text = "Información incompleta, favor de indicar los Movimientos Realizados"
                    Else
                        If .wneOdometroU.Text.Trim = "" Then
                            .litError.Text = "Favor de ingresar el Odómetro"
                        Else
                            If Not ((Not fuOdometroF.PostedFile Is Nothing) And (fuOdometroF.PostedFile.ContentLength > 0)) Then
                                .litError.Text = "Favor de  ingresar la Fotografía del Odómetro"
                            Else
                                If (.wneOdometroE.Value - .wneOdometroU.Value > 5) Or (.wneOdometroE.Value - .wneOdometroU.Value < -5) Then
                                    .litError.Text = "Existe diferencia entre el Odómetro registrado por Edenred y el ingresado manualmente; favor de validar y en caso de que proceda, dar clic en el botón <b>Confirmar</b>"
                                    .btnAceptar.Visible = False
                                    .btnConfirmar.Visible = True
                                Else
                                    actCargaComb()
                                End If
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click
        With Me
            Try
                .litError.Text = ""

                If Val(._txtBan.Text) = 1 Then
                    .litError.Text = "Ya se almacenó el registro previamente, favor de validarlo en el apartado de Consulta de Cargas de Combustible"
                Else
                    If .txtObs.Text.Trim = "" Then
                        .litError.Text = "Información incompleta, favor de indicar los Movimientos Realizados"
                    Else
                        If .wneOdometroU.Text.Trim = "" Then
                            .litError.Text = "Favor de ingresar el Odómetro"
                        Else
                            If Not ((Not fuOdometroF.PostedFile Is Nothing) And (fuOdometroF.PostedFile.ContentLength > 0)) Then
                                .litError.Text = "Favor de  ingresar la Fotografía del Odómetro"
                            Else
                                actCargaComb()
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class