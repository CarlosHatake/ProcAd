Public Class _18
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    ._txtIdUsuario.Text = Session("id_usuario")
                    'Creación de Variables para la conexión y consulta de información a la base de datos
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD

                    'Nombre del Solicitante
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "select nombre + ' ' + ap_paterno + ' ' + ap_materno " + _
                                             "from cg_usuario " + _
                                             "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                             "where id_usuario = @idUsuario "
                    SCMValores.Parameters.AddWithValue("@idUsuario", Val(Session("id_usuario")))
                    ConexionBD.Open()
                    .lblSolicitante.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
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
                    .ddlAutorizador.DataTextField = "autorizador"
                    .ddlAutorizador.DataValueField = "id_usuario"
                    ConexionBD.Open()
                    sdaAut.Fill(dsAut)
                    .ddlAutorizador.DataBind()
                    ConexionBD.Close()
                    sdaAut.Dispose()
                    dsAut.Dispose()
                    .ddlAutorizador.SelectedIndex = -1

                    limpiarPantalla()
                Catch ex As Exception
                    .litError.Text = ex.ToString
                End Try
            End With
        End If
    End Sub

#End Region

#Region "Funciones"

    Public Sub limpiarPantalla()
        With Me
            .pnlSolicitud.Enabled = True
            .lblFolio.Text = ""
            .ddlAutorizador.SelectedIndex = -1
            .ddlPrioridad.SelectedIndex = 1
            .wdpFechaIni.Date = Date.Now
            .wdpFechaFin.Date = Date.Now.AddHours(1)
            llenarVehiculos()
            .txtDestino.Text = ""
            .txtJust.Text = ""
            .btnGuardar.Enabled = True
            .btnNueva.Enabled = False
        End With
    End Sub

    Public Sub llenarVehiculos()
        With Me
            Try
                If .wdpFechaIni.Date > .wdpFechaFin.Date Then
                    .ddlVehiculo.Items.Clear()
                Else
                    .litError.Text = ""

                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim sdaVehi As New SqlDataAdapter
                    Dim dsVehi As New DataSet
                    .ddlVehiculo.DataSource = dsVehi
                    sdaVehi.SelectCommand = New SqlCommand("select id_ms_vehiculo, modelo + ' [' + no_eco + ' ' + placas + ']' as vehiculo " + _
                                                           "from bd_Empleado.dbo.ms_vehiculo " + _
                                                           "where status = 'A' " + _
                                                           "  and uso_utilitario = 'COMODIN' " + _
                                                           "  and poliza_seguro_vig > GETDATE() " + _
                                                           "  and tarjeta_cir_vig > GETDATE() " + _
                                                           "  and id_ms_vehiculo not in (select id_ms_vehiculo " + _
                                                           "							 from ms_reserva " + _
                                                           "							 where ms_reserva.status not in ('Z', 'ZM', 'R') " + _
                                                           "							   and ((fecha_ini <= @fechaIni and fecha_fin > @fechaIni) " + _
                                                           "							     or (fecha_ini < @fechaFin and fecha_fin >= @fechaFin) " + _
                                                           "							     or (fecha_ini <= @fechaIni and fecha_fin >= @fechaFin) " + _
                                                           "							     or (fecha_ini > @fechaIni and fecha_fin < @fechaFin))) ", ConexionBD)
                    sdaVehi.SelectCommand.Parameters.AddWithValue("@fechaIni", .wdpFechaIni.Date)
                    sdaVehi.SelectCommand.Parameters.AddWithValue("@fechaFin", .wdpFechaFin.Date)
                    .ddlVehiculo.DataTextField = "vehiculo"
                    .ddlVehiculo.DataValueField = "id_ms_vehiculo"
                    ConexionBD.Open()
                    sdaVehi.Fill(dsVehi)
                    .ddlVehiculo.DataBind()
                    ConexionBD.Close()
                    sdaVehi.Dispose()
                    dsVehi.Dispose()
                    .ddlVehiculo.SelectedIndex = -1
                    If .ddlVehiculo.Items.Count = 0 Then
                        .litError.Text = "No hay vehículos disponibles, favor de validarlo"
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Guardar / Nuevo"

    Protected Sub wdpFechaIni_ValueChanged(sender As Object, e As TextEditorValueChangedEventArgs) Handles wdpFechaIni.ValueChanged
        llenarVehiculos()
    End Sub

    Protected Sub wdpFechaFin_ValueChanged(sender As Object, e As TextEditorValueChangedEventArgs) Handles wdpFechaFin.ValueChanged
        llenarVehiculos()
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        With Me
            Try
                .litError.Text = ""
                If .ddlVehiculo.Items.Count = 0 Then
                    .litError.Text = "No hay vehículos disponibles, favor de validarlo"
                Else
                    If .wdpFechaIni.Date > .wdpFechaFin.Date Then
                        .litError.Text = "Información Inválida, favor de validar el periodo de la reservación"
                    Else
                        If .txtDestino.Text.Trim = "" Or .txtJust.Text.Trim = "" Then
                            .litError.Text = "Información Insuficiente, favor de validar los campos de texto"
                        Else
                            Dim cont As Integer = 0
                            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                            SCMValores.Connection = ConexionBD
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()

                            'Validar Disponibilidad
                            SCMValores.CommandText = "select count (*) " + _
                                                     "from bd_Empleado.dbo.ms_vehiculo " + _
                                                     "where status = 'A' " + _
                                                     "  and uso_utilitario = 'COMODIN' " + _
                                                     "  and poliza_seguro_vig > GETDATE() " + _
                                                     "  and tarjeta_cir_vig > GETDATE() " + _
                                                     "  and id_ms_vehiculo = @idMsVehiculo " + _
                                                     "  and id_ms_vehiculo not in (select id_ms_vehiculo " + _
                                                     "							 from ms_reserva " + _
                                                     "							 where ms_reserva.status not in ('Z', 'ZM', 'R') " + _
                                                     "							   and ((fecha_ini <= @fechaIni and fecha_fin > @fechaIni) " + _
                                                     "							     or (fecha_ini < @fechaFin and fecha_fin >= @fechaFin) " + _
                                                     "							     or (fecha_ini <= @fechaIni and fecha_fin >= @fechaFin) " + _
                                                     "							     or (fecha_ini > @fechaIni and fecha_fin < @fechaFin))) "
                            SCMValores.Parameters.AddWithValue("@fechaIni", .wdpFechaIni.Date)
                            SCMValores.Parameters.AddWithValue("@fechaFin", .wdpFechaFin.Date)
                            SCMValores.Parameters.AddWithValue("@idMsVehiculo", .ddlVehiculo.SelectedValue)
                            ConexionBD.Open()
                            cont = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If cont > 0 Then
                                'Almacenar los datos
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "insert into ms_reserva(prioridad, empresa, centro_costo, no_empleado, empleado_nom, empleado_appat, empleado_apmat, no_autorizador, autorizador_nom, autorizador_appat, autorizador_apmat, fecha_ini, fecha_fin, no_eco, marca, modelo, año, placas, IAVE, poliza_seguro_vig, tarjeta_cir_vig, destino, just, id_usr_solicito, fecha_solicito, id_ms_vehiculo, id_usr_autorizo, status) " + _
                                                         "select @prioridad as prioridad, cgEmpr.nombre as empresa, cgCC.nombre as centro_costo " + _
                                                         "	 , cgEmpl.no_empleado, cgEmpl.nombre as empleado_nom, cgEmpl.ap_paterno as empleado_appat, cgEmpl.ap_materno as empleado_apmat " + _
                                                         "	 , cgAut.no_empleado as no_autorizador, cgAut.nombre as autorizador_nom, cgAut.ap_paterno as autorizador_appat, cgAut.ap_materno as autorizador_apmat " + _
                                                         "	 , @fechaIni as fecha_ini, @fechaFin as fecha_fin " + _
                                                         "	 , msVeh.no_eco, msVeh.marca, msVeh.modelo, msVeh.año, msVeh.placas, msVeh.IAVE, msVeh.poliza_seguro_vig, msVeh.tarjeta_cir_vig " + _
                                                         "	 , @destino as destino, @just as just, @idUsrSol as id_usr_solicito, GETDATE() as fecha_solicito, @idMsVehiculo as id_ms_vehiculo, @idUsrAut as id_usr_autorizo, 'P' as status " + _
                                                         "from cg_usuario as cgUsrE " + _
                                                         "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cgUsrE.id_empleado = cgEmpl.id_empleado " + _
                                                         "  inner join bd_Empleado.dbo.cg_cc cgCC on cgEmpl.id_cc = cgCC.id_cc " + _
                                                         "  inner join bd_Empleado.dbo.cg_empresa cgEmpr on cgCC.id_empresa = cgEmpr.id_empresa " + _
                                                         "  inner join cg_usuario cgUsrA on cgUsrA.id_usuario = @idUsrAut " + _
                                                         "  inner join bd_Empleado.dbo.cg_empleado cgAut on cgUsrA.id_empleado = cgAut.id_empleado " + _
                                                         "  inner join bd_Empleado.dbo.ms_vehiculo msVeh on msVeh.id_ms_vehiculo = @idMsVehiculo " + _
                                                         "where cgUsrE.id_usuario = @idUsrSol "
                                SCMValores.Parameters.AddWithValue("@idUsrSol", Val(._txtIdUsuario.Text))
                                SCMValores.Parameters.AddWithValue("@idUsrAut", .ddlAutorizador.SelectedValue)
                                SCMValores.Parameters.AddWithValue("@prioridad", .ddlPrioridad.SelectedValue)
                                SCMValores.Parameters.AddWithValue("@fechaIni", .wdpFechaIni.Date)
                                SCMValores.Parameters.AddWithValue("@fechaFin", .wdpFechaFin.Date)
                                SCMValores.Parameters.AddWithValue("@idMsVehiculo", .ddlVehiculo.SelectedValue)
                                SCMValores.Parameters.AddWithValue("@destino", .txtDestino.Text.Trim)
                                SCMValores.Parameters.AddWithValue("@just", .txtJust.Text.Trim)
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()

                                'Obtener Folio
                                SCMValores.CommandText = "select max(id_ms_reserva) " + _
                                                         "from ms_reserva " + _
                                                         "where id_usr_solicito = @idUsrSol " + _
                                                         "  and fecha_ini = @fechaIni " + _
                                                         "  and id_ms_vehiculo = @idMsVehiculo "
                                ConexionBD.Open()
                                .lblFolio.Text = SCMValores.ExecuteScalar()
                                ConexionBD.Close()

                                'Envío de Correo
                                Dim Mensaje As New System.Net.Mail.MailMessage()
                                Dim destinatario As String = ""
                                'Obtener el Correos del Autorizador
                                SCMValores.CommandText = "select cgEmpl.correo from bd_empleado.dbo.cg_empleado cgEmpl left join cg_usuario on cgEmpl.id_empleado = cg_usuario.id_empleado where cg_usuario.id_usuario = @idAut "
                                SCMValores.Parameters.AddWithValue("@idAut", .ddlAutorizador.SelectedValue)
                                ConexionBD.Open()
                                destinatario = SCMValores.ExecuteScalar()
                                ConexionBD.Close()

                                Mensaje.[To].Add(destinatario)
                                Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                                'Mensaje.From = New System.Net.Mail.MailAddress("soporte.unne.procad@gmail.com", "ProcAd", System.Text.Encoding.UTF8)
                                Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                                Mensaje.Subject = "ProcAd - Solicitud de Reservación No. " + .lblFolio.Text + " por Autorizar"
                                Dim texto As String
                                texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                                        "Se solicitó la reservación número <b>" + .lblFolio.Text + _
                                        "</b> por parte de <b>" + .lblSolicitante.Text + _
                                        "</b><br>Favor de determinar si procede </span>"
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

                                .pnlSolicitud.Enabled = False
                                .btnGuardar.Enabled = False
                                .btnNueva.Enabled = True
                            Else
                                .litError.Text = "Ya fue reservado el vehículo, favor de validar la disponibilidad"
                                llenarVehiculos()
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnNueva_Click(sender As Object, e As EventArgs) Handles btnNueva.Click
        limpiarPantalla()
    End Sub

#End Region

End Class