Public Class _35
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
                        query = "select razon_social_afiliado " + _
                                "     , cantidad_mercancia " + _
                                "     , precio_ticket " + _
                                "     , importe_con_ieps " + _
                                "     , iva " + _
                                "     , importe_transaccion " + _
                                "     , conductor " + _
                                "     , isnull(cgEmpl.id_conductor_edenred, '') as id_conductorE " + _
                                "from dt_carga_comb_tar " + _
                                "  left join cg_usuario on dt_carga_comb_tar.id_usr_carga = cg_usuario.id_usuario " + _
                                "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                "where id_dt_carga_comb_tar = @id_dt_carga_comb_tar "
                        sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@id_dt_carga_comb_tar", Val(._txtIdDtCargaComb.Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()
                        .lblEstacion.Text = dsSol.Tables(0).Rows(0).Item("razon_social_afiliado").ToString()
                        .wneLitros.Value = Val(dsSol.Tables(0).Rows(0).Item("cantidad_mercancia").ToString())
                        .wcePrecio.Value = Val(dsSol.Tables(0).Rows(0).Item("precio_ticket").ToString())
                        .wceSubtotal.Value = Val(dsSol.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                        .wceIVA.Value = Val(dsSol.Tables(0).Rows(0).Item("iva").ToString())
                        .wceTotal.Value = Val(dsSol.Tables(0).Rows(0).Item("importe_transaccion").ToString())
                        ._txtIdConductor.Text = dsSol.Tables(0).Rows(0).Item("id_conductorE").ToString()
                        ._txtConductor.Text = dsSol.Tables(0).Rows(0).Item("conductor").ToString()
                        sdaSol.Dispose()
                        dsSol.Dispose()

                        'Lista de Unidades
                        Dim sdaUnidad As New SqlDataAdapter
                        Dim dsUnidad As New DataSet
                        sdaUnidad.SelectCommand = New SqlCommand("select id_ms_vehiculo " + _
                                                                 "     , no_eco " + _
                                                                 "from bd_Empleado.dbo.ms_vehiculo " + _
                                                                 "where status = 'A' " + _
                                                                 "  and (uso_utilitario = 'COMODIN' " + _
                                                                 "    or id_empleado_asig in (select id_empleado " + _
                                                                 "                            from cg_usuario " + _
                                                                 "                            where id_usuario = @id_usuario)) " + _
                                                                 "order by no_eco ", ConexionBD)
                        sdaUnidad.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                        .ddlUnidad.DataSource = dsUnidad
                        .ddlUnidad.DataTextField = "no_eco"
                        .ddlUnidad.DataValueField = "id_ms_vehiculo"
                        ConexionBD.Open()
                        sdaUnidad.Fill(dsUnidad)
                        .ddlUnidad.DataBind()
                        ConexionBD.Close()
                        sdaUnidad.Dispose()
                        dsUnidad.Dispose()

                        'Actualizar Datos de la unidad
                        actUnidad()

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

    Protected Sub ddlUnidad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUnidad.SelectedIndexChanged
        actUnidad()
    End Sub

#End Region

#Region "Funciones"

    Public Sub actUnidad()
        With Me
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim sdaInfUnidad As New SqlDataAdapter
            Dim dsInfUnidad As New DataSet
            sdaInfUnidad.SelectCommand = New SqlCommand("select placas " + _
                                                        "     , no_tarjeta_edenred " + _
                                                        "     , marca " + _
                                                        "     , modelo " + _
                                                        "     , serie " + _
                                                        "     , isnull(porcent_tolerancia, .1) as tolerancia " + _
                                                        "	  , isnull((select top 1 dt_carga_comb.rendimiento " + _
                                                        "               from dt_carga_comb " + _
                                                        "               where num_tarjeta = ms_vehiculo.no_tarjeta_edenred " + _
                                                        "                 and fecha <= @fecha " + _
                                                        "               order by fecha desc), 9) as rendimiento " + _
                                                        "from bd_Empleado.dbo.ms_vehiculo " + _
                                                        "where id_ms_vehiculo = @id_ms_vehiculo ", ConexionBD)
            sdaInfUnidad.SelectCommand.Parameters.AddWithValue("@id_ms_vehiculo", .ddlUnidad.SelectedValue)
            sdaInfUnidad.SelectCommand.Parameters.AddWithValue("@fecha", .wdpFecha.Date)
            ConexionBD.Open()
            sdaInfUnidad.Fill(dsInfUnidad)
            ConexionBD.Close()
            .lblPlaca.Text = dsInfUnidad.Tables(0).Rows(0).Item("placas").ToString()
            .lblNoTarjeta.Text = dsInfUnidad.Tables(0).Rows(0).Item("no_tarjeta_edenred").ToString()
            .lblMarca.Text = dsInfUnidad.Tables(0).Rows(0).Item("marca").ToString()
            .lblModelo.Text = dsInfUnidad.Tables(0).Rows(0).Item("modelo").ToString()
            ._txtNoSerie.Text = dsInfUnidad.Tables(0).Rows(0).Item("serie").ToString()
            ._txtTolerancia.Text = dsInfUnidad.Tables(0).Rows(0).Item("tolerancia").ToString()
            ._txtRendimiento.Text = dsInfUnidad.Tables(0).Rows(0).Item("rendimiento").ToString()
            sdaInfUnidad.Dispose()
            dsInfUnidad.Dispose()
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
                    If .txtTicket.Text.Trim = "" Then
                        .litError.Text = "Información incompleta, favor de indicar el número de Ticket"
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
                                            SCMValores.CommandText = "update dt_carga_comb_tar set status = 'R', obs_evid = @obs_evid, id_conductor = @id_conductor, identificador_vehiculo = @identificador_vehiculo, placa = @placa, num_tarjeta = @num_tarjeta, numero_serie = @numero_serie, tolerancia_unidad = @tolerancia_unidad, no_comprobante = @no_comprobante, " + _
                                                                     "                             id_mercancia = @id_mercancia, mercancia = @mercancia, km_ant_transaccion = @km_ant_transaccion, km_transaccion = @km_transaccion, recorrido = @recorrido, rendimiento = @rendimiento, rendimiento_real = @rendimiento_real, " + _
                                                                     "                             desviacion_real = @desviacion_real, bloqueo_st = @bloqueo_st, fecha = @fecha, foto_evid = @foto_evid, odometro_evid = @odometro_evid, existe_dif = @existe_dif, diferencia = @diferencia, id_usr_evid = @id_usr_evid, fecha_evid = @fecha_evid " + _
                                                                     "where id_dt_carga_comb_tar = @id_dt_carga_comb_tar "
                                            SCMValores.Parameters.AddWithValue("@obs_evid", .txtObs.Text.Trim)
                                            SCMValores.Parameters.AddWithValue("@id_conductor", ._txtIdConductor.Text)
                                            SCMValores.Parameters.AddWithValue("@identificador_vehiculo", .ddlUnidad.SelectedItem.Text)
                                            SCMValores.Parameters.AddWithValue("@placa", .lblPlaca.Text)
                                            SCMValores.Parameters.AddWithValue("@num_tarjeta", .lblNoTarjeta.Text)
                                            SCMValores.Parameters.AddWithValue("@numero_serie", ._txtNoSerie.Text)
                                            SCMValores.Parameters.AddWithValue("@tolerancia_unidad", ._txtTolerancia.Text)
                                            SCMValores.Parameters.AddWithValue("@no_comprobante", .txtTicket.Text)
                                            SCMValores.Parameters.AddWithValue("@id_mercancia", 1)
                                            SCMValores.Parameters.AddWithValue("@mercancia", "Gasolina")
                                            SCMValores.Parameters.AddWithValue("@km_ant_transaccion", .wneOdometroAnt.Value)
                                            SCMValores.Parameters.AddWithValue("@km_transaccion", .wneOdometroAct.Value)
                                            SCMValores.Parameters.AddWithValue("@recorrido", (.wneOdometroAct.Value - .wneOdometroAnt.Value))
                                            SCMValores.Parameters.AddWithValue("@rendimiento", Val(._txtRendimiento.Text))
                                            SCMValores.Parameters.AddWithValue("@rendimiento_real", Val(._txtRendimientoReal.Text))
                                            SCMValores.Parameters.AddWithValue("@desviacion_real", Val(._txtDesvReal.Text))
                                            SCMValores.Parameters.AddWithValue("@bloqueo_st", ._txtBloqueoST.Text)
                                            SCMValores.Parameters.AddWithValue("@fecha", .wdpFecha.Date)
                                            SCMValores.Parameters.AddWithValue("@foto_evid", sFileNameFotoH)
                                            SCMValores.Parameters.AddWithValue("@odometro_evid", .wneOdometroAct.Value)
                                            SCMValores.Parameters.AddWithValue("@existe_dif", "N")
                                            SCMValores.Parameters.AddWithValue("@diferencia", 0)
                                            SCMValores.Parameters.AddWithValue("@id_usr_evid", Val(._txtIdUsuario.Text))
                                            SCMValores.Parameters.AddWithValue("@fecha_evid", fecha)
                                            SCMValores.Parameters.AddWithValue("@id_dt_carga_comb_tar", Val(._txtIdDtCargaComb.Text))
                                            ConexionBD.Open()
                                            SCMValores.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            'Guardar Archivos Adjuntos
                                            If (Not fuOdometroF.PostedFile Is Nothing) And (fuOdometroF.PostedFile.ContentLength > 0) Then
                                                'Se agrega el Id al nombre del archivo / Almacenar el archivo en la ruta especificada
                                                sFileNameFotoH = ._txtIdDtCargaComb.Text + "FotoHTB-" + sFileNameFotoH
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
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class