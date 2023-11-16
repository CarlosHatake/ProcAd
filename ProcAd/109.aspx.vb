Public Class _109
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 1
                    'Session("idDtCargaComb") = 1

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtIdDtCargaComb.Text = Session("idDtCargaComb")
                        ._txtBan.Text = 0

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim sdaSol As New SqlDataAdapter
                        Dim dsSol As New DataSet
                        Dim query As String
                        query = "select razon_social_afiliado " +
                                "     , dt_toka.no_comprobante " +
                                "     , cantidad_mercancia " +
                                "     , precio_ticket " +
                                "     , importe_con_ieps " +
                                "     , iva " +
                                "     , importe_transaccion " +
                                "     , isnull(cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno, '') as conductor " +
                                "     , isnull(cgEmpl.no_empleado, '') as id_conductorE " +
                                "     , dt_toka.identificador_vehiculo " +
                                "     , dt_toka.placa " +
                                "     , dt_toka.num_tarjeta " +
                                "     , ms_vehiculo.rendimiento " +
                                "     , dt_toka.tolerancia_unidad " +
                                "     , ms_vehiculo.marca " +
                                "     , ms_vehiculo.modelo " +
                                "     , case when dt_toka.fecha is null then '' else convert(varchar, dt_toka.fecha, 103) + replace(replace(replace(right(convert(varchar, dt_toka.fecha, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') end as fecha_texto " +
                                "     , isnull((select km_transaccion " +
                                "               from (select top 1 * " +
                                "                     from (select fecha, km_transaccion " +
                                "                           from dt_carga_comb " +
                                "                           where identificador_vehiculo = dt_toka.identificador_vehiculo " +
                                "                             and km_transaccion is not null " +
                                "                             and fecha < dt_toka.fecha " +
                                "                             and status not in ('Z') " +
                                "                           union " +
                                "                           select fecha, km_transaccion " +
                                "                           from dt_carga_comb_tar " +
                                "                           where identificador_vehiculo = dt_toka.identificador_vehiculo " +
                                "                             and km_transaccion is not null " +
                                "                             and fecha < dt_toka.fecha " +
                                "                             and status not in ('Z','ZA') " +
                                "                           union " +
                                "                           select fecha, km_transaccion " +
                                "                           from dt_carga_comb_toka " +
                                "                           where identificador_vehiculo = dt_toka.identificador_vehiculo " +
                                "                             and km_transaccion is not null " +
                                "                             and fecha < dt_toka.fecha " +
                                "                             and status not in ('Z') " +
                                "                             and id_dt_carga_comb_toka <> @id_dt_carga_comb_toka " +
                                "                           ) as ult_carga " +
                                "                     order by fecha desc) as ult_km), -1) as kms_ant " +
                                "     , isnull((select top 1 ms_comb.centro_costo " +
                                "               from dt_carga_comb_toka " +
                                "                 left join ms_comb on dt_carga_comb_toka.identificador_vehiculo = ms_comb.no_eco and (periodo_ini<= dt_carga_comb_toka.fecha and periodo_fin >= dt_carga_comb_toka.fecha) " +
                                "                 left join cg_usuario on ms_comb.id_usr_solicita = cg_usuario.id_usuario and cg_usuario.status = 'A' " +
                                "               where dt_carga_comb_toka.id_dt_carga_comb_toka = @id_dt_carga_comb_toka " +
                                "                 and cg_usuario.id_usuario = @id_usuario), '') as centro_costos " +
                                "from dt_carga_comb_toka dt_toka " +
                                "  left join bd_Empleado.dbo.ms_vehiculo on dt_toka.identificador_vehiculo = ms_vehiculo.no_eco and ms_vehiculo.status <> 'B' " +
                                "  left join cg_usuario on cg_usuario.id_usuario = @id_usuario " +
                                "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                "where id_dt_carga_comb_toka = @id_dt_carga_comb_toka "
                        sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@id_dt_carga_comb_toka", Val(._txtIdDtCargaComb.Text))
                        sdaSol.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()
                        .lblUnidad.Text = dsSol.Tables(0).Rows(0).Item("identificador_vehiculo").ToString()
                        .lblPlaca.Text = dsSol.Tables(0).Rows(0).Item("placa").ToString()
                        .lblNoTarjeta.Text = dsSol.Tables(0).Rows(0).Item("num_tarjeta").ToString()
                        ._txtRendimiento.Text = dsSol.Tables(0).Rows(0).Item("rendimiento").ToString()
                        ._txtTolerancia.Text = dsSol.Tables(0).Rows(0).Item("tolerancia_unidad").ToString()
                        .lblMarca.Text = dsSol.Tables(0).Rows(0).Item("marca").ToString()
                        .lblModelo.Text = dsSol.Tables(0).Rows(0).Item("modelo").ToString()
                        .lblFecha.Text = dsSol.Tables(0).Rows(0).Item("fecha_texto").ToString()
                        .lblEstacion.Text = dsSol.Tables(0).Rows(0).Item("razon_social_afiliado").ToString()
                        .lblNoTicket.Text = dsSol.Tables(0).Rows(0).Item("no_comprobante").ToString()
                        .wneLitros.Value = Val(dsSol.Tables(0).Rows(0).Item("cantidad_mercancia").ToString())
                        .wcePrecio.Value = Val(dsSol.Tables(0).Rows(0).Item("precio_ticket").ToString())
                        .wceSubtotal.Value = Val(dsSol.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                        .wceIVA.Value = Val(dsSol.Tables(0).Rows(0).Item("iva").ToString())
                        .wceTotal.Value = Val(dsSol.Tables(0).Rows(0).Item("importe_transaccion").ToString())
                        If Val(dsSol.Tables(0).Rows(0).Item("kms_ant").ToString()) = -1 Then
                            .wneOdometroAnt.Value = 0
                            .wneOdometroAnt.Enabled = True
                        Else
                            .wneOdometroAnt.Value = Val(dsSol.Tables(0).Rows(0).Item("kms_ant").ToString())
                            .wneOdometroAnt.Enabled = False
                        End If
                        ._txtIdConductor.Text = dsSol.Tables(0).Rows(0).Item("id_conductorE").ToString()
                        ._txtConductor.Text = dsSol.Tables(0).Rows(0).Item("conductor").ToString()
                        ._txtCentroCosto.Text = dsSol.Tables(0).Rows(0).Item("centro_costos").ToString()
                        sdaSol.Dispose()
                        dsSol.Dispose()

                        'Panel
                        .btnAceptar.Visible = True
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
                        If .wneOdometroAnt.Text.Trim = "" Or .wneOdometroAct.Text.Trim = "" Then
                            .litError.Text = "Favor de ingresar el Odómetro correspondiente"
                        Else
                            If .wneOdometroAnt.Value >= .wneOdometroAct.Value Then
                                .litError.Text = "Información de Odómetros incorrecta, favor de validarlo"
                            Else
                                If Not ((Not fuOdometroF.PostedFile Is Nothing) And (fuOdometroF.PostedFile.ContentLength > 0)) Then
                                    .litError.Text = "Favor de  ingresar la Fotografía del Odómetro"
                                Else
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
                                        'Cálculos de Rendimiento
                                        ._txtRendimientoReal.Text = (.wneOdometroAct.Value - .wneOdometroAnt.Value) / .wneLitros.Value
                                        ._txtDesvReal.Text = (Val(._txtRendimientoReal.Text) - Val(._txtRendimiento.Text)) / Val(._txtRendimiento.Text)
                                        If (Val(._txtDesvReal.Text) + Val(._txtTolerancia.Text)) < 0 Then
                                            ._txtBloqueoST.Text = "S"
                                        Else
                                            ._txtBloqueoST.Text = "N"
                                        End If

                                        'Actualizar datos de la Solicitud
                                        SCMValores.CommandText = ""
                                        SCMValores.Parameters.Clear()
                                        SCMValores.CommandText = "update dt_carga_comb_toka " +
                                                                 "  set status = 'R', obs_evid = @obs_evid, id_conductor = @id_conductor, conductor = @conductor, centro_costos = @centro_costos " +
                                                                 "    , km_ant_transaccion = @km_ant_transaccion, km_transaccion = @km_transaccion, recorrido = @recorrido, rendimiento = @rendimiento, rendimiento_real = @rendimiento_real " +
                                                                 "    , desviacion_real = @desviacion_real, bloqueo_st = @bloqueo_st, foto_evid = @foto_evid, odometro_evid = @odometro_evid, existe_dif = @existe_dif, diferencia = @diferencia " +
                                                                 "    , id_usr_evid = @id_usr_evid, fecha_evid = @fecha_evid " +
                                                                 "where id_dt_carga_comb_toka = @id_dt_carga_comb_toka "
                                        SCMValores.Parameters.AddWithValue("@obs_evid", .txtObs.Text.Trim)
                                        SCMValores.Parameters.AddWithValue("@id_conductor", ._txtIdConductor.Text)
                                        SCMValores.Parameters.AddWithValue("@conductor", ._txtConductor.Text)
                                        SCMValores.Parameters.AddWithValue("@centro_costos", ._txtCentroCosto.Text)
                                        SCMValores.Parameters.AddWithValue("@km_ant_transaccion", .wneOdometroAnt.Value)
                                        SCMValores.Parameters.AddWithValue("@km_transaccion", .wneOdometroAct.Value)
                                        SCMValores.Parameters.AddWithValue("@recorrido", (.wneOdometroAct.Value - .wneOdometroAnt.Value))
                                        SCMValores.Parameters.AddWithValue("@rendimiento", Val(._txtRendimiento.Text))
                                        SCMValores.Parameters.AddWithValue("@rendimiento_real", Val(._txtRendimientoReal.Text))
                                        SCMValores.Parameters.AddWithValue("@desviacion_real", Val(._txtDesvReal.Text))
                                        SCMValores.Parameters.AddWithValue("@bloqueo_st", ._txtBloqueoST.Text)
                                        SCMValores.Parameters.AddWithValue("@foto_evid", sFileNameFotoH)
                                        SCMValores.Parameters.AddWithValue("@odometro_evid", .wneOdometroAct.Value)
                                        SCMValores.Parameters.AddWithValue("@existe_dif", "N")
                                        SCMValores.Parameters.AddWithValue("@diferencia", 0)
                                        SCMValores.Parameters.AddWithValue("@id_usr_evid", Val(._txtIdUsuario.Text))
                                        SCMValores.Parameters.AddWithValue("@fecha_evid", fecha)
                                        SCMValores.Parameters.AddWithValue("@id_dt_carga_comb_toka", Val(._txtIdDtCargaComb.Text))
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()

                                        'Guardar Archivos Adjuntos
                                        If (Not fuOdometroF.PostedFile Is Nothing) And (fuOdometroF.PostedFile.ContentLength > 0) Then
                                            'Se agrega el Id al nombre del archivo / Almacenar el archivo en la ruta especificada
                                            sFileNameFotoH = ._txtIdDtCargaComb.Text + "FotoHT-" + sFileNameFotoH
                                            fuOdometroF.PostedFile.SaveAs(sFileDir + sFileNameFotoH)
                                        End If

                                        ._txtBan.Text = 1

                                        If ._txtBloqueoST.Text = "S" Then
                                            .litError.Text = "Se realiza Bloqueo por Rendimiento ya que el Rendimiento esperado es " + ._txtRendimiento.Text + " y el real fue " + ._txtRendimientoReal.Text + ", favor de contactar al departamento de Soporte Técnico para la aclaración"

                                            ''Envío de Notificación a Soporte Técnico
                                            'Dim Mensaje As New System.Net.Mail.MailMessage()
                                            'Dim destinatario As String = ""
                                            ''Obtener el Correo de Soporte Técnico
                                            'SCMValores.CommandText = "select valor from cg_parametros where parametro = 'mail_ST' "
                                            'ConexionBD.Open()
                                            'destinatario = SCMValores.ExecuteScalar()
                                            'ConexionBD.Close()

                                            'Mensaje.[To].Add(destinatario)
                                            'Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                                            ''Mensaje.From = New System.Net.Mail.MailAddress("soporte.unne.procad@gmail.com", "ProcAd", System.Text.Encoding.UTF8)
                                            'Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                                            'Mensaje.Subject = "ProcAd - Bloqueo por Rendiento, unidad " + .ddlUnidad.SelectedItem.Text
                                            'Dim texto As String
                                            'texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                                            '        "Se bloqueó al conductor <b>" + ._txtConductor.Text + _
                                            '        "</b> ya que el rendimiento esperado es " + ._txtRendimiento.Text + " y el real fue " + ._txtRendimientoReal.Text + ", teniendo ademas una tolerancia de "(Val(._txtTolerancia.Text) * 100).ToString + "%" + _
                                            '        "</b><br><br>Favor de determinar si procede el desbloqueo </span>"
                                            'Mensaje.Body = texto
                                            'Mensaje.IsBodyHtml = True
                                            'Mensaje.Priority = MailPriority.Normal

                                            'Dim Servidor As New SmtpClient()
                                            'Servidor.Host = "10.10.10.30"
                                            'Servidor.Port = 587
                                            'Servidor.EnableSsl = False
                                            'Servidor.UseDefaultCredentials = False
                                            'Servidor.Credentials = New System.Net.NetworkCredential("nprocad", "mc8HLB8lPe78")

                                            'Try
                                            '    Servidor.Send(Mensaje)
                                            'Catch ex As System.Net.Mail.SmtpException
                                            '    .litError.Text = ex.ToString
                                            'End Try

                                        End If

                                        .pnlInicio.Enabled = False
                                    End While
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

#End Region

End Class