Public Class ConsActividad
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 1
                    'Session("id_usuario") = 21

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD

                        'Llenar lista de Asignado a
                        Dim sdaAsigA As New SqlDataAdapter
                        Dim dsAsigA As New DataSet
                        sdaAsigA.SelectCommand = New SqlCommand("select distinct(id_usr_responsable) " + _
                                                                "     , responsable " + _
                                                                "from ms_actividad " + _
                                                                "  left join cg_grupo on ms_actividad.id_grupo = cg_grupo.id_grupo " + _
                                                                "  left join dt_grupo on cg_grupo.id_grupo = dt_grupo.id_grupo and dt_grupo.status = 'A' " + _
                                                                "where cg_grupo.status = 'A' " + _
                                                                "  and (id_usr_secretario = @idUsuario " + _
                                                                "    or id_usr_lider = @idUsuario) " + _
                                                                "union " + _
                                                                "select distinct(id_usr_responsable) " + _
                                                                "     , responsable " + _
                                                                "from ms_actividad " + _
                                                                "where id_usr_responsable = @idUsuario ", ConexionBD)
                        sdaAsigA.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        .ddlAsignado.DataSource = dsAsigA
                        .ddlAsignado.DataTextField = "responsable"
                        .ddlAsignado.DataValueField = "id_usr_responsable"
                        ConexionBD.Open()
                        sdaAsigA.Fill(dsAsigA)
                        .ddlAsignado.DataBind()
                        ConexionBD.Close()
                        sdaAsigA.Dispose()
                        dsAsigA.Dispose()
                        .ddlAsignado.SelectedIndex = -1
                        'Llenar lista de Grupos
                        Dim sdaGrupo As New SqlDataAdapter
                        Dim dsGrupo As New DataSet
                        sdaGrupo.SelectCommand = New SqlCommand("select distinct(cg_grupo.id_grupo) " + _
                                                                "     , grupo " + _
                                                                "from cg_grupo " + _
                                                                "  left join dt_grupo on cg_grupo.id_grupo = dt_grupo.id_grupo and dt_grupo.status = 'A' " + _
                                                                "where cg_grupo.status = 'A' " + _
                                                                "  and (id_usr_secretario = @idUsuario " + _
                                                                "    or id_usr_lider = @idUsuario " + _
                                                                "    or id_usr_part = @idUsuario) ", ConexionBD)
                        sdaGrupo.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        .ddlGrupo.DataSource = dsGrupo
                        .ddlGrupo.DataTextField = "grupo"
                        .ddlGrupo.DataValueField = "id_grupo"
                        ConexionBD.Open()
                        sdaGrupo.Fill(dsGrupo)
                        .ddlGrupo.DataBind()
                        ConexionBD.Close()
                        sdaGrupo.Dispose()
                        dsGrupo.Dispose()
                        .ddlGrupo.SelectedIndex = -1

                        limpiar()
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

    Public Sub limpiar()
        With Me
            'Ocultar todos los paneles principales
            .pnlFiltros.Visible = True
            .pnlTickets.Visible = False
            .pnlDetalle.Visible = False
            'Filtros
            .cbFecha.Checked = False
            .pnlFecha.Visible = False
            .wdpFechaI.Date = Now.Date
            .wdpFechaF.Date = Now.Date
            .cbTema.Checked = False
            .pnlTema.Visible = False
            .cbAsignado.Checked = False
            .pnlAsignado.Visible = False
            .cbGrupo.Checked = False
            .pnlGrupo.Visible = False
            .cbStatus.Checked = False
            .pnlStatus.Visible = False
            .cbNoActividad.Checked = False
            .pnlNoActividad.Visible = False
        End With
    End Sub

    Public Sub vista(ByRef control, ByVal valor)
        With Me
            If valor Then
                control.Visible = True
            Else
                control.Visible = False
            End If
        End With
    End Sub

    Function ValidarFecha(ByVal dia, ByVal mes, ByVal año)
        With Me
            Try
                Dim banFecha As Integer
                banFecha = 0
                Select Case mes
                    Case 4, 6, 9, 11
                        If dia > 30 Then
                            banFecha = 1
                        End If
                    Case 2
                        If Val(año) Mod 4 = 0 Then
                            If dia > 29 Then
                                banFecha = 1
                            End If
                        Else
                            If dia > 28 Then
                                banFecha = 1
                            End If
                        End If
                End Select
                If banFecha = 0 Then
                    ValidarFecha = True
                Else
                    ValidarFecha = False
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
                ValidarFecha = False
            End Try
        End With
    End Function

    Public Sub actualizaCom()
        With Me
            Try
                .litError.Text = ""

                'Llenar lista Comentarios
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaHistorico As New SqlDataAdapter
                Dim dsHistorico As New DataSet
                sdaHistorico.SelectCommand = New SqlCommand("select fecha_com as Fecha " + _
                                                            "     , nombre + ' ' + ap_paterno as Empleado " + _
                                                            "     , REPLACE(comentario, CHAR(13) + CHAR(10), '<br/>') as comentario " + _
                                                            "from ms_historicoA " + _
                                                            "  inner join cg_usuario on ms_historicoA.id_usr_com = cg_usuario.id_usuario " + _
                                                            "  inner join bd_Empleado.dbo.cg_empleado Emp on cg_usuario.id_empleado = Emp.id_empleado " + _
                                                            "where id_ms_actividad = @idMsAct ", ConexionBD)
                sdaHistorico.SelectCommand.Parameters.AddWithValue("@idMsAct", Val(._txtIdMsAct.Text))
                .gvHistorico.DataSource = dsHistorico
                ConexionBD.Open()
                sdaHistorico.Fill(dsHistorico)
                .gvHistorico.DataBind()
                ConexionBD.Close()
                sdaHistorico.Dispose()
                dsHistorico.Dispose()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Function EliminarAcentos(ByVal texto)
        Dim i, s1, s2
        s1 = "ÁÀÉÈÍÏÓÒÚÜáàèéíïóòúü"
        s2 = "AAEEIIOOUUaaeeiioouu"
        If Len(texto) <> 0 Then
            For i = 1 To Len(s1)
                texto = Replace(texto, Mid(s1, i, 1), Mid(s2, i, 1))
            Next
        End If
        EliminarAcentos = texto
    End Function

    Public Sub pintarTabla(ByRef gridView)
        With Me
            Try
                Dim i As Integer
                Dim ban As Integer
                For i = 0 To (gridView.Rows.Count() - 1)
                    ban = 0
                    If gridView.Rows(i).Cells(10).Text <> "Cerrado" And gridView.Rows(i).Cells(10).Text <> "Cancelado" Then
                        If gridView.Rows(i).Cells(8).Text.Trim = "&nbsp;" Then
                            ban = 0
                        Else
                            If Date.Now > CDate(gridView.Rows(i).Cells(8).Text) Then
                                ban = 1
                            Else
                                ban = -1
                            End If
                        End If
                    Else
                        If gridView.Rows(i).Cells(9).Text.Trim = "&nbsp;" Then
                            ban = 2
                        Else
                            If CDate(gridView.Rows(i).Cells(9).Text) > CDate(gridView.Rows(i).Cells(8).Text) Then
                                ban = 2
                            Else
                                ban = 3
                            End If
                        End If
                    End If
                    Select Case ban
                        Case -1
                            gridView.Rows(i).Cells(8).ForeColor = Color.Green
                            gridView.Rows(i).Cells(8).Font.Bold = True
                        Case 0
                            gridView.Rows(i).Cells(8).ForeColor = Color.Black
                            gridView.Rows(i).Cells(8).Font.Bold = False
                        Case 1
                            gridView.Rows(i).Cells(8).ForeColor = Color.Red
                            gridView.Rows(i).Cells(8).Font.Bold = True
                        Case 2
                            gridView.Rows(i).Cells(8).ForeColor = Color.Salmon
                            gridView.Rows(i).Cells(8).Font.Bold = True
                        Case Else
                            gridView.Rows(i).Cells(8).ForeColor = Color.Black
                            gridView.Rows(i).Cells(8).Font.Bold = False
                    End Select
                Next
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub pintarTablaTemp(ByRef gridView)
        With Me
            Try
                Dim i As Integer
                Dim ban As Integer
                For i = 0 To (gridView.Rows.Count() - 1)
                    ban = 0
                    If gridView.Rows(i).Cells(7).Text <> "Cerrado" And gridView.Rows(i).Cells(7).Text <> "Cancelado" Then
                        If gridView.Rows(i).Cells(7).Text.Trim = "&nbsp;" Then
                            ban = 0
                        Else
                            If Date.Now > CDate(gridView.Rows(i).Cells(7).Text) Then
                                ban = 1
                            Else
                                ban = -1
                            End If
                        End If
                    Else
                        If gridView.Rows(i).Cells(8).Text.Trim = "&nbsp;" Then
                            ban = 2
                        Else
                            If CDate(gridView.Rows(i).Cells(8).Text) > CDate(gridView.Rows(i).Cells(7).Text) Then
                                ban = 2
                            Else
                                ban = 3
                            End If
                        End If
                    End If
                    Select Case ban
                        Case -1
                            gridView.Rows(i).Cells(7).ForeColor = Color.Green
                            gridView.Rows(i).Cells(7).Font.Bold = True
                        Case 0
                            gridView.Rows(i).Cells(7).ForeColor = Color.Black
                            gridView.Rows(i).Cells(7).Font.Bold = False
                        Case 1
                            gridView.Rows(i).Cells(7).ForeColor = Color.Red
                            gridView.Rows(i).Cells(7).Font.Bold = True
                        Case 2
                            gridView.Rows(i).Cells(7).ForeColor = Color.Salmon
                            gridView.Rows(i).Cells(7).Font.Bold = True
                        Case Else
                            gridView.Rows(i).Cells(7).ForeColor = Color.Black
                            gridView.Rows(i).Cells(7).Font.Bold = False
                    End Select
                Next
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Private Sub actualizarGrid()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .gvAdjuntos.DataSource = dsCatalogo
                'Habilitar columnas para actualización
                .gvAdjuntos.Columns(0).Visible = True
                .gvAdjuntos.Columns(1).Visible = True
                sdaCatalogo.SelectCommand = New SqlCommand("select id_ms_archivoA " + _
                                                           "     , id_ms_actividad " + _
                                                           "     , nombre as Archivo " + _
                                                           "     , 'http://148.223.153.43/ProcAd - Adjuntos EPR/' + CAST(id_ms_archivoA as varchar(10)) + '-' + nombre as Path " + _
                                                           "     , nombre as Ruta, fecha as Fecha " + _
                                                           "from ms_archivoA " + _
                                                           "where id_ms_actividad = @idMsAct ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@idMsAct", Val(._txtIdMsAct.Text))
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvAdjuntos.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvAdjuntos.SelectedIndex = -1
                'Inhabilitar columnas para vista
                .gvAdjuntos.Columns(0).Visible = False
                .gvAdjuntos.Columns(1).Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Sub enviarMailComentario(ByVal destinatario)
        With Me
            'Versión 2.0
            Dim Mensaje As New System.Net.Mail.MailMessage()

            Mensaje.[To].Add(destinatario)
            Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
            Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
            Mensaje.Subject = "ProcAd - Actividad " + .lblNoActividad.Text + ": " + .lblSolicito.Text
            Mensaje.Body = "<span style=""font-family:Verdana; font-size: 10pt;"">" + _
                           "Se actualizó la actividad con el siguiente comentario..." + _
                           "<br><br><b>" + .txtComentario.Text + "</b>" + _
                           "<br><br>En caso de ser necesario, favor de ingresar al ProcAd [http://148.223.153.43/ProcAd] para dar seguimiento y/o adjuntar información solicitada." + _
                           "</span>" + _
                           "<span style=""font-family: Verdana; font-size: 9pt; color: #FF0000; "">" + _
                           "<br><br> <b>Nota: Favor de <u>no</u> contestar a este correo</b>" + _
                           "</span>"
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

        End With
    End Sub

    Sub enviarMailComentarioS(ByVal destinatario)
        With Me
            'Versión 2.0
            Dim Mensaje As New System.Net.Mail.MailMessage()

            Mensaje.[To].Add(destinatario)
            Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
            Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
            Mensaje.Subject = "ProcAd - Actividad " + .lblNoActividad.Text + ": " + .lblSolicito.Text
            Mensaje.Body = "<span style=""font-family:Verdana; font-size: 10pt;"">" + _
                           "Se <b>concluyó</b> la actividad y se agregó el siguiente comentario..." + _
                           "<br><br><b>" + .txtComentario.Text + "</b>" + _
                           "<br><br>En caso de ser necesario, favor de ingresar al ProcAd [http://148.223.153.43/ProcAd] para dar seguimiento y/o adjuntar información solicitada." + _
                           "</span>" + _
                           "<span style=""font-family: Verdana; font-size: 9pt; color: #FF0000; "">" + _
                           "<br><br> <b>Nota: Favor de <u>no</u> contestar a este correo</b>" + _
                           "</span>"
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

        End With
    End Sub

    Sub enviarMailCancelado(ByVal destinatario)
        With Me
            Dim Mensaje As New System.Net.Mail.MailMessage()

            Mensaje.[To].Add(destinatario)
            Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
            Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
            Mensaje.Subject = "ProcAd - Actividad " + .lblNoActividad.Text + ": " + .lblSolicito.Text
            Mensaje.Body = "<span style=""font-family:Verdana; font-size: 10pt;"">" + _
                           "La Actividad número " + .lblNoActividad.Text + ": " + .lblSolicito.Text + ", fue <b>cancelada</b> con el siguiente comentario..." + _
                           "<br><br><b>" + .txtComentario.Text + "</b>" + _
                           "<br><br>Cualquier duda, te invitamos a revisar la actividad en el portal del ProcAd http://148.223.153.43/ProcAd" + _
                           "</span>" + _
                           "<span style=""font-family: Verdana; font-size: 9pt; color: #FF0000; "">" + _
                           "<br><br> <b>Nota: Favor de <u>no</u> contestar a este correo</b>" + _
                           "</span>"
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
        End With
    End Sub

    Sub enviarMailCierre(ByVal destinatario)
        With Me
            Dim Mensaje As New System.Net.Mail.MailMessage()

            Mensaje.[To].Add(destinatario)
            Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
            Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
            Mensaje.Subject = "ProcAd - Actividad " + .lblNoActividad.Text + ": " + .lblSolicito.Text
            Mensaje.Body = "<span style=""font-family:Verdana; font-size: 10pt;"">" + _
                           "La Actividad número " + .lblNoActividad.Text + ": " + .lblSolicito.Text + ", fue <b>cerrada</b>." + _
                           "<br><br>Cualquier duda, te invitamos a revisar la actividad en el portal del ProcAd http://148.223.153.43/ProcAd" + _
                           "</span>" + _
                           "<span style=""font-family: Verdana; font-size: 9pt; color: #FF0000; "">" + _
                           "<br><br> <b>Nota: Favor de <u>no</u> contestar a este correo</b>" + _
                           "</span>"
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
        End With
    End Sub

    Function DifFechas(ByVal fi, ByVal ff)
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("SiTTi")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD

                'Obtener la fecha compromiso
                Dim fechaIni As DateTime
                Dim fechaFin As DateTime
                Dim fechaDif As DateTime = "01/01/1900"
                Dim resultado As Integer = 0
                Dim cmdF As New SqlCommand
                Dim cmdDif As New SqlCommand
                cmdF.Connection = ConexionBD
                cmdDif.Connection = ConexionBD
                cmdF.CommandText = "select count(*) from cg_cal_tra where fecha = @fecha "
                cmdDif.CommandText = "select @ff - @fi"
                cmdF.Parameters.Add("@fecha", SqlDbType.DateTime)
                cmdDif.Parameters.Add("@ff", SqlDbType.DateTime)
                cmdDif.Parameters.Add("@fi", SqlDbType.DateTime)
                cmdDif.Parameters.Add("@fDif", SqlDbType.DateTime)

                fechaIni = fi
                fechaFin = ff

                Dim f As Integer = 1
                'Ajustar Fecha Inicio para Hora Hábil
                While f = 1
                    Select Case fechaIni.DayOfWeek
                        Case 1, 2, 3, 4
                            If fechaIni.Hour = 14 Then
                                fechaIni = fechaIni.AddHours(-(fechaIni.Hour) + 15)
                                fechaIni = fechaIni.AddMinutes(-(fechaIni.Minute))
                                fechaIni = fechaIni.AddSeconds(-(fechaIni.Second))
                                fechaIni = fechaIni.AddMilliseconds(-(fechaIni.Millisecond))
                            End If
                            If fechaIni.Hour < 8 Or fechaIni.Hour > 18 Or (fechaIni.Hour = 18 And fechaIni.Minute > 0) Then
                                If fechaIni.Hour > 18 Or (fechaIni.Hour = 18 And fechaIni.Minute > 0) Then
                                    fechaIni = fechaIni.AddDays(1)
                                End If
                                fechaIni = fechaIni.AddHours(-(fechaIni.Hour) + 8)
                                fechaIni = fechaIni.AddMinutes(-(fechaIni.Minute))
                                fechaIni = fechaIni.AddSeconds(-(fechaIni.Second))
                                fechaIni = fechaIni.AddMilliseconds(-(fechaIni.Millisecond))
                            End If
                        Case 5
                            If fechaIni.Hour = 14 Then
                                fechaIni = fechaIni.AddHours(-(fechaIni.Hour) + 15)
                                fechaIni = fechaIni.AddMinutes(-(fechaIni.Minute))
                                fechaIni = fechaIni.AddSeconds(-(fechaIni.Second))
                                fechaIni = fechaIni.AddMilliseconds(-(fechaIni.Millisecond))
                            End If
                            If fechaIni.Hour < 8 Or (fechaIni.Hour > 18 Or (fechaIni.Hour = 18 And fechaIni.Minute > 0)) Then
                                If fechaIni.Hour > 18 Or (fechaIni.Hour = 18 And fechaIni.Minute > 0) Then
                                    fechaIni = fechaIni.AddDays(1)
                                    'Determinar si la fecha es festiva o laboral
                                    cmdF.Parameters("@fecha").Value = CDate(fechaIni.Year.ToString + "-" + fechaIni.Month.ToString + "-" + fechaIni.Day.ToString)
                                    cmdF.Connection.Open()
                                    resultado = cmdF.ExecuteScalar()
                                    cmdF.Connection.Close()
                                    If resultado >= 1 Then
                                        fechaIni = fechaIni.AddDays(2)
                                    End If
                                End If
                                fechaIni = fechaIni.AddHours(-(fechaIni.Hour) + 8)
                                fechaIni = fechaIni.AddMinutes(-(fechaIni.Minute))
                                fechaIni = fechaIni.AddSeconds(-(fechaIni.Second))
                                fechaIni = fechaIni.AddMilliseconds(-(fechaIni.Millisecond))
                            End If
                        Case 0, 6
                            fechaIni = fechaIni.AddHours(-(fechaIni.Hour) + 8)
                            fechaIni = fechaIni.AddMinutes(-(fechaIni.Minute))
                            fechaIni = fechaIni.AddSeconds(-(fechaIni.Second))
                            fechaIni = fechaIni.AddMilliseconds(-(fechaIni.Millisecond))
                            If fechaIni.DayOfWeek = 6 Then
                                fechaIni = fechaIni.AddDays(2)
                            Else
                                fechaIni = fechaIni.AddDays(1)
                            End If
                    End Select
                    'Determinar si la fecha es festiva o laboral
                    cmdF.Parameters("@fecha").Value = CDate(fechaIni.Year.ToString + "-" + fechaIni.Month.ToString + "-" + fechaIni.Day.ToString)
                    cmdF.Connection.Open()
                    resultado = cmdF.ExecuteScalar()
                    cmdF.Connection.Close()
                    If resultado >= 1 Then
                        fechaIni = fechaIni.AddDays(1)
                        f = 1
                    Else
                        f = 0
                    End If
                End While
                'Ajustar Fecha Final para Hora Hábil
                f = 1
                While f = 1
                    Select Case fechaFin.DayOfWeek
                        Case 1, 2, 3, 4
                            If fechaFin.Hour = 14 Then
                                fechaFin = fechaFin.AddHours(-(fechaFin.Hour) + 15)
                                fechaFin = fechaFin.AddMinutes(-(fechaFin.Minute))
                                fechaFin = fechaFin.AddSeconds(-(fechaFin.Second))
                                fechaFin = fechaFin.AddMilliseconds(-(fechaFin.Millisecond))
                            End If
                            If fechaFin.Hour < 8 Or fechaFin.Hour > 18 Or (fechaFin.Hour = 18 And fechaFin.Minute > 0) Then
                                If fechaFin.Hour > 18 Or (fechaFin.Hour = 18 And fechaFin.Minute > 0) Then
                                    fechaFin = fechaFin.AddDays(1)
                                End If
                                fechaFin = fechaFin.AddHours(-(fechaFin.Hour) + 8)
                                fechaFin = fechaFin.AddMinutes(-(fechaFin.Minute))
                                fechaFin = fechaFin.AddSeconds(-(fechaFin.Second))
                                fechaFin = fechaFin.AddMilliseconds(-(fechaFin.Millisecond))
                            End If
                        Case 5
                            If fechaFin.Hour = 14 Then
                                fechaFin = fechaFin.AddHours(-(fechaFin.Hour) + 15)
                                fechaFin = fechaFin.AddMinutes(-(fechaFin.Minute))
                                fechaFin = fechaFin.AddSeconds(-(fechaFin.Second))
                                fechaFin = fechaFin.AddMilliseconds(-(fechaFin.Millisecond))
                            End If
                            If fechaFin.Hour < 8 Or (fechaFin.Hour > 18 Or (fechaFin.Hour = 18 And fechaFin.Minute > 0)) Then
                                If fechaFin.Hour > 18 Or (fechaFin.Hour = 18 And fechaFin.Minute > 0) Then
                                    fechaFin = fechaFin.AddDays(1)
                                    'Determinar si la fecha es festiva o laboral
                                    cmdF.Parameters("@fecha").Value = CDate(fechaFin.Year.ToString + "-" + fechaFin.Month.ToString + "-" + fechaFin.Day.ToString)
                                    cmdF.Connection.Open()
                                    resultado = cmdF.ExecuteScalar()
                                    cmdF.Connection.Close()
                                    If resultado >= 1 Then
                                        fechaFin = fechaFin.AddDays(2)
                                    End If
                                End If
                                fechaFin = fechaFin.AddHours(-(fechaFin.Hour) + 8)
                                fechaFin = fechaFin.AddMinutes(-(fechaFin.Minute))
                                fechaFin = fechaFin.AddSeconds(-(fechaFin.Second))
                                fechaFin = fechaFin.AddMilliseconds(-(fechaFin.Millisecond))
                            End If
                        Case 0, 6
                            fechaFin = fechaFin.AddHours(-(fechaFin.Hour) + 8)
                            fechaFin = fechaFin.AddMinutes(-(fechaFin.Minute))
                            fechaFin = fechaFin.AddSeconds(-(fechaFin.Second))
                            fechaFin = fechaFin.AddMilliseconds(-(fechaFin.Millisecond))
                            If fechaFin.DayOfWeek = 6 Then
                                fechaFin = fechaFin.AddDays(2)
                            Else
                                fechaFin = fechaFin.AddDays(1)
                            End If
                    End Select
                    'Determinar si la fecha es festiva o laboral
                    cmdF.Parameters("@fecha").Value = CDate(fechaFin.Year.ToString + "-" + fechaFin.Month.ToString + "-" + fechaFin.Day.ToString)
                    cmdF.Connection.Open()
                    resultado = cmdF.ExecuteScalar()
                    cmdF.Connection.Close()
                    If resultado >= 1 Then
                        fechaFin = fechaFin.AddDays(1)
                        f = 1
                    Else
                        f = 0
                    End If
                End While

                'Obtener horas hábiles
                If fechaIni <> fechaFin Then
                    If fechaIni.Date = fechaFin.Date Then
                        'Mismo día
                        If fechaFin.Hour < 14 Then
                            cmdDif.CommandText = "select @ff - @fi"
                            cmdDif.Parameters("@fi").Value = fechaIni
                            cmdDif.Parameters("@ff").Value = fechaFin
                            cmdDif.Parameters("@fDif").Value = DBNull.Value
                            cmdDif.Connection.Open()
                            fechaDif = cmdDif.ExecuteScalar()
                            cmdDif.Connection.Close()
                        Else
                            If fechaIni.Hour < 14 Then
                                cmdDif.CommandText = "select DATEADD(HOUR, -1, @ff - @fi)"
                                cmdDif.Parameters("@fi").Value = fechaIni
                                cmdDif.Parameters("@ff").Value = fechaFin
                                cmdDif.Parameters("@fDif").Value = DBNull.Value
                                cmdDif.Connection.Open()
                                fechaDif = cmdDif.ExecuteScalar()
                                cmdDif.Connection.Close()
                            Else
                                cmdDif.CommandText = "select @ff - @fi"
                                cmdDif.Parameters("@fi").Value = fechaIni
                                cmdDif.Parameters("@ff").Value = fechaFin
                                cmdDif.Parameters("@fDif").Value = DBNull.Value
                                cmdDif.Connection.Open()
                                fechaDif = cmdDif.ExecuteScalar()
                                cmdDif.Connection.Close()
                            End If
                        End If
                    Else
                        'Más de un día
                        If fechaIni.Hour < 14 Then
                            cmdDif.CommandText = "select DATEADD(HOUR, -1, @ff - @fi)"
                        Else
                            cmdDif.CommandText = "select @ff - @fi"
                        End If
                        cmdDif.Parameters("@fi").Value = fechaIni
                        '6 pm de ese día
                        fechaIni = fechaIni.AddHours(-(fechaIni.Hour) + 18)
                        fechaIni = fechaIni.AddMinutes(-(fechaIni.Minute))
                        fechaIni = fechaIni.AddSeconds(-(fechaIni.Second))
                        fechaIni = fechaIni.AddMilliseconds(-(fechaIni.Millisecond))
                        cmdDif.Parameters("@ff").Value = fechaIni
                        cmdDif.Parameters("@fDif").Value = DBNull.Value
                        cmdDif.Connection.Open()
                        fechaDif = cmdDif.ExecuteScalar()
                        cmdDif.Connection.Close()
                        fechaIni = fechaIni.AddHours(-(fechaIni.Hour) + 8)
                        'Agregar un día
                        fechaIni = fechaIni.AddDays(1)
                        f = 1
                        While f = 1
                            If fechaIni.DayOfWeek = 6 Then
                                fechaIni = fechaIni.AddDays(2)
                            Else
                                If fechaIni.DayOfWeek = 0 Then
                                    fechaIni = fechaIni.AddDays(1)
                                End If
                            End If
                            'Determinar si la fecha es festiva o laboral
                            cmdF.Parameters("@fecha").Value = CDate(fechaIni.Year.ToString + "-" + fechaIni.Month.ToString + "-" + fechaIni.Day.ToString)
                            cmdF.Connection.Open()
                            resultado = cmdF.ExecuteScalar()
                            cmdF.Connection.Close()
                            If resultado >= 1 Then
                                fechaIni = fechaIni.AddDays(1)
                                f = 1
                            Else
                                f = 0
                            End If
                        End While

                        While fechaIni <= fechaFin
                            'Validar si es el mismo día
                            If fechaIni.Date = fechaFin.Date Then
                                If fechaFin.Hour < 14 Then
                                    cmdDif.CommandText = "select @fDif + (@ff - @fi)"
                                    cmdDif.Parameters("@fi").Value = fechaIni
                                    cmdDif.Parameters("@ff").Value = fechaFin
                                    cmdDif.Parameters("@fDif").Value = fechaDif
                                    cmdDif.Connection.Open()
                                    fechaDif = cmdDif.ExecuteScalar()
                                    cmdDif.Connection.Close()
                                Else
                                    If fechaIni.Hour < 14 Then
                                        cmdDif.CommandText = "select @fDif + (DATEADD(HOUR, -1, @ff - @fi))"
                                        cmdDif.Parameters("@fi").Value = fechaIni
                                        cmdDif.Parameters("@ff").Value = fechaFin
                                        cmdDif.Parameters("@fDif").Value = fechaDif
                                        cmdDif.Connection.Open()
                                        fechaDif = cmdDif.ExecuteScalar()
                                        cmdDif.Connection.Close()
                                    Else
                                        cmdDif.CommandText = "select @fDif + (@ff - @fi)"
                                        cmdDif.Parameters("@fi").Value = fechaIni
                                        cmdDif.Parameters("@ff").Value = fechaFin
                                        cmdDif.Parameters("@fDif").Value = fechaDif
                                        cmdDif.Connection.Open()
                                        fechaDif = cmdDif.ExecuteScalar()
                                        cmdDif.Connection.Close()
                                    End If
                                End If
                            Else
                                fechaDif = fechaDif.AddHours(9)
                            End If
                            'Agregar un día
                            fechaIni = fechaIni.AddDays(1)
                            f = 1
                            While f = 1
                                If fechaIni.DayOfWeek = 6 Then
                                    fechaIni = fechaIni.AddDays(2)
                                Else
                                    If fechaIni.DayOfWeek = 0 Then
                                        fechaIni = fechaIni.AddDays(1)
                                    End If
                                End If
                                'Determinar si la fecha es festiva o laboral
                                cmdF.Parameters("@fecha").Value = CDate(fechaIni.Year.ToString + "-" + fechaIni.Month.ToString + "-" + fechaIni.Day.ToString)
                                cmdF.Connection.Open()
                                resultado = cmdF.ExecuteScalar()
                                cmdF.Connection.Close()
                                If resultado >= 1 Then
                                    fechaIni = fechaIni.AddDays(1)
                                    f = 1
                                Else
                                    f = 0
                                End If
                            End While
                        End While
                    End If
                End If

                DifFechas = fechaDif
            Catch ex As Exception
                .litError.Text = ex.ToString
                DifFechas = DBNull.Value
            End Try
        End With
    End Function

    Public Sub cambioStatus(ByVal status)
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                Dim fechaHoy As DateTime = Date.Now
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                If .txtComentario.Text.Trim <> "" Then
                    'Guardar el Comentario
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "INSERT INTO ms_historicoA(id_ms_actividad, id_usr_com, fecha_com, comentario) values (@idMsAct, @id_usr_com, @fecha_com, @comentario)"
                    SCMValores.Parameters.AddWithValue("@idMsAct", Val(._txtIdMsAct.Text))
                    SCMValores.Parameters.AddWithValue("@id_usr_com", Val(._txtIdUsuario.Text))
                    SCMValores.Parameters.AddWithValue("@fecha_com", fechaHoy)
                    SCMValores.Parameters.AddWithValue("@comentario", .txtComentario.Text)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()
                    'Obtener el ID del comentario
                    SCMValores.CommandText = "select max(id_ms_historicoA) from ms_historicoA where id_ms_actividad = @idMsAct and id_usr_com = @id_usr_com and fecha_com = @fecha_com"
                    ConexionBD.Open()
                    ._txtIdMsHist.Text = SCMValores.ExecuteScalar
                    ConexionBD.Close()
                End If

                'Cambio de Estatus
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "UPDATE ms_actividad SET status = @status where id_ms_actividad = @idMsAct"
                SCMValores.Parameters.AddWithValue("@idMsAct", Val(._txtIdMsAct.Text))
                SCMValores.Parameters.AddWithValue("@status", status)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()
                'Guardar Histórico de Cambio de Estatus
                SCMValores.CommandText = "INSERT INTO ms_hStatusA(id_ms_actividad,  id_ms_historicoA,  id_usuario,  fecha,  status,  tiempo_sol,  tiempo_resp,  tiempo_prov,  tiempo_sol_q,  tiempo_resp_q,  tiempo_prov_q) " + _
                                         "                 VALUES(       @idMsAct,  @id_ms_historico, @id_usuario, @fecha, @status, @tiempo_usr,   @tiempo_ti, @tiempo_prov, @tiempo_usr_q,   @tiempo_ti_q, @tiempo_prov_q)"
                If .txtComentario.Text.Trim <> "" Then
                    SCMValores.Parameters.AddWithValue("@id_ms_historico", Val(._txtIdMsHist.Text))
                Else
                    SCMValores.Parameters.AddWithValue("@id_ms_historico", DBNull.Value)
                End If
                SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                SCMValores.Parameters.AddWithValue("@fecha", fechaHoy)
                'Datos del último cambio de estatus
                Dim idMsHStatus As Integer = 0
                Dim SCMValoresT As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValoresT.Connection = ConexionBD
                SCMValoresT.CommandText = "select isnull(max(id_ms_hStatusA),0) from ms_hStatusA where id_ms_actividad = @idMsAct"
                SCMValoresT.Parameters.Clear()
                SCMValoresT.Parameters.AddWithValue("@idMsAct", Val(._txtIdMsAct.Text))
                ConexionBD.Open()
                idMsHStatus = SCMValoresT.ExecuteScalar
                ConexionBD.Close()
                Dim sdaStatusA As New SqlDataAdapter
                Dim dsStatusA As New DataSet
                sdaStatusA.SelectCommand = New SqlCommand("select status, fecha from ms_hStatusA where id_ms_hStatusA = @idMsHStatus ", ConexionBD)
                sdaStatusA.SelectCommand.Parameters.AddWithValue("@idMsHStatus", idMsHStatus)
                ConexionBD.Open()
                sdaStatusA.Fill(dsStatusA)
                ConexionBD.Close()
                If idMsHStatus <> 0 Then
                    Dim perfil As String = ""
                    Select Case dsStatusA.Tables(0).Rows(0).Item("status").ToString()
                        Case "P"
                            'En Proceso
                            perfil = "TI"
                        Case "PU"
                            'Pend. Usuario
                            perfil = "Usr"
                        Case "PP"
                            'Pend. Proveedor
                            perfil = "Prov"
                    End Select

                    Select Case perfil
                        Case "TI"
                            'Complementar Parámetros
                            SCMValores.Parameters.AddWithValue("@tiempo_ti", DifFechas(Convert.ToDateTime(dsStatusA.Tables(0).Rows(0).Item("fecha").ToString()), fechaHoy))
                            If fechaHoy > Convert.ToDateTime(.lblFechaComp.Text) Then
                                If Convert.ToDateTime(dsStatusA.Tables(0).Rows(0).Item("fecha").ToString()) > Convert.ToDateTime(.lblFechaComp.Text) Then
                                    SCMValores.Parameters.AddWithValue("@tiempo_ti_q", DifFechas(Convert.ToDateTime(dsStatusA.Tables(0).Rows(0).Item("fecha").ToString()), fechaHoy))
                                Else
                                    SCMValores.Parameters.AddWithValue("@tiempo_ti_q", DifFechas(Convert.ToDateTime(.lblFechaComp.Text), fechaHoy))
                                End If
                            Else
                                SCMValores.Parameters.AddWithValue("@tiempo_ti_q", DBNull.Value)
                            End If
                            'Vaciar Parámetros restantes
                            SCMValores.Parameters.AddWithValue("@tiempo_usr", DBNull.Value)
                            SCMValores.Parameters.AddWithValue("@tiempo_usr_q", DBNull.Value)
                            SCMValores.Parameters.AddWithValue("@tiempo_prov", DBNull.Value)
                            SCMValores.Parameters.AddWithValue("@tiempo_prov_q", DBNull.Value)
                        Case "Usr"
                            'Complementar Parámetros
                            SCMValores.Parameters.AddWithValue("@tiempo_usr", DifFechas(Convert.ToDateTime(dsStatusA.Tables(0).Rows(0).Item("fecha").ToString()), fechaHoy))
                            If fechaHoy > Convert.ToDateTime(.lblFechaComp.Text) Then
                                If Convert.ToDateTime(dsStatusA.Tables(0).Rows(0).Item("fecha").ToString()) > Convert.ToDateTime(.lblFechaComp.Text) Then
                                    SCMValores.Parameters.AddWithValue("@tiempo_usr_q", DifFechas(Convert.ToDateTime(dsStatusA.Tables(0).Rows(0).Item("fecha").ToString()), fechaHoy))
                                Else
                                    SCMValores.Parameters.AddWithValue("@tiempo_usr_q", DifFechas(Convert.ToDateTime(.lblFechaComp.Text), fechaHoy))
                                End If
                            Else
                                SCMValores.Parameters.AddWithValue("@tiempo_usr_q", DBNull.Value)
                            End If
                            'Vaciar Parámetros restantes
                            SCMValores.Parameters.AddWithValue("@tiempo_ti", DBNull.Value)
                            SCMValores.Parameters.AddWithValue("@tiempo_ti_q", DBNull.Value)
                            SCMValores.Parameters.AddWithValue("@tiempo_prov", DBNull.Value)
                            SCMValores.Parameters.AddWithValue("@tiempo_prov_q", DBNull.Value)
                        Case "Prov"
                            'Complementar Parámetros
                            SCMValores.Parameters.AddWithValue("@tiempo_prov", DifFechas(Convert.ToDateTime(dsStatusA.Tables(0).Rows(0).Item("fecha").ToString()), fechaHoy))
                            If fechaHoy > Convert.ToDateTime(.lblFechaComp.Text) Then
                                If Convert.ToDateTime(dsStatusA.Tables(0).Rows(0).Item("fecha").ToString()) > Convert.ToDateTime(.lblFechaComp.Text) Then
                                    SCMValores.Parameters.AddWithValue("@tiempo_prov_q", DifFechas(Convert.ToDateTime(dsStatusA.Tables(0).Rows(0).Item("fecha").ToString()), fechaHoy))
                                Else
                                    SCMValores.Parameters.AddWithValue("@tiempo_prov_q", DifFechas(Convert.ToDateTime(.lblFechaComp.Text), fechaHoy))
                                End If
                            Else
                                SCMValores.Parameters.AddWithValue("@tiempo_prov_q", DBNull.Value)
                            End If
                            'Vaciar Parámetros restantes
                            SCMValores.Parameters.AddWithValue("@tiempo_usr", DBNull.Value)
                            SCMValores.Parameters.AddWithValue("@tiempo_usr_q", DBNull.Value)
                            SCMValores.Parameters.AddWithValue("@tiempo_ti", DBNull.Value)
                            SCMValores.Parameters.AddWithValue("@tiempo_ti_q", DBNull.Value)
                    End Select
                Else
                    SCMValores.Parameters.AddWithValue("@tiempo_usr", DBNull.Value)
                    SCMValores.Parameters.AddWithValue("@tiempo_usr_q", DBNull.Value)
                    SCMValores.Parameters.AddWithValue("@tiempo_prov", DBNull.Value)
                    SCMValores.Parameters.AddWithValue("@tiempo_prov_q", DBNull.Value)

                    SCMValores.Parameters.AddWithValue("@tiempo_ti", DifFechas(Convert.ToDateTime(.lblFecha.Text), Convert.ToDateTime(fechaHoy)))
                    If fechaHoy > Convert.ToDateTime(.lblFechaComp.Text) Then
                        SCMValores.Parameters.AddWithValue("@tiempo_ti_q", DifFechas(Convert.ToDateTime(.lblFechaComp.Text), fechaHoy))
                    Else
                        SCMValores.Parameters.AddWithValue("@tiempo_ti_q", DBNull.Value)
                    End If
                End If
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                'Indicar Fecha de Término en caso de que el estatus sea Cerrado y Generar Encuesta
                If status = "C" Then
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "UPDATE ms_actividad SET fecha_cierre = @fecha where id_ms_actividad = @idMsAct"
                    SCMValores.Parameters.AddWithValue("@idMsAct", Val(._txtIdMsAct.Text))
                    SCMValores.Parameters.AddWithValue("@fecha", fechaHoy)
                    .lblFechaTer.Text = fechaHoy
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()
                    If Convert.ToDateTime(.lblFechaTer.Text) > Convert.ToDateTime(.lblFechaComp.Text) Then
                        .lblFechaTer.ForeColor = Color.DarkRed
                    Else
                        .lblFechaTer.ForeColor = Color.DarkGreen
                    End If

                End If
                sdaStatusA.Dispose()
                dsStatusA.Dispose()

                actualizaCom()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Filtros"

    Private Sub cbFecha_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFecha.CheckedChanged
        vista(Me.pnlFecha, Me.cbFecha.Checked)
    End Sub

    Private Sub cbTema_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTema.CheckedChanged
        vista(Me.pnlTema, Me.cbTema.Checked)
        If Me.cbTema.Checked = True Then
            Me.txtTema.Text = ""
        End If
    End Sub

    Protected Sub cbAsignado_CheckedChanged(sender As Object, e As EventArgs) Handles cbAsignado.CheckedChanged
        vista(Me.pnlAsignado, Me.cbAsignado.Checked)
    End Sub

    Private Sub cbGrupo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbGrupo.CheckedChanged
        vista(Me.pnlGrupo, Me.cbGrupo.Checked)
    End Sub

    Protected Sub cbStatus_CheckedChanged(sender As Object, e As EventArgs) Handles cbStatus.CheckedChanged
        vista(Me.pnlStatus, Me.cbStatus.Checked)
    End Sub

    Protected Sub cbNoActividad_CheckedChanged(sender As Object, e As EventArgs) Handles cbNoActividad.CheckedChanged
        vista(Me.pnlNoActividad, Me.cbNoActividad.Checked)
        If Me.cbNoActividad.Checked = True Then
            Me.txtNoActividad.Text = ""
        End If
    End Sub

#End Region

#Region "Buscar"

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        With Me

            Try
                .litError.Text = ""
                'Llenar el GridView
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaConsulta As New SqlDataAdapter
                Dim dsConsulta As New DataSet
                Dim query As String

                Dim valAsig As Integer
                Dim textoAsig As String
                valAsig = Val(.ddlAsignado.SelectedValue)
                textoAsig = .ddlAsignado.SelectedValue.ToString

                query = "select ms_actividad.id_ms_actividad " + _
                        "     , ms_actividad.grupo " + _
                        "     , ms_reunion.tema " + _
                        "     , ms_actividad.descripcion " + _
                        "     , ms_actividad.solicito " + _
                        "     , ms_actividad.responsable " + _
                        "     , ms_actividad.fecha_registro " + _
                        "     , ms_actividad.fecha_compromiso " + _
                        "     , ms_actividad.fecha_cierre " + _
                        "     , case ms_actividad.status " + _
                        "         when 'P' then 'En Proceso' " + _
                        "         when 'PU' then 'Pend. Usuario' " + _
                        "         when 'PP' then 'Pend. Proveedor' " + _
                        "         when 'C' then 'Cerrado' " + _
                        "         when 'Z' then 'Cancelado' " + _
                        "         when 'Ca' then 'Cancelado' " + _
                        "         else '-' " + _
                        "       end as estatus " + _
                        "from ms_actividad " + _
                        "  left join ms_reunion on ms_actividad.id_ms_reunion = ms_reunion.id_ms_reunion " + _
                        "where id_ms_actividad in (select distinct(id_ms_actividad) " + _
                        "                          from ms_actividad " + _
                        "                            left join cg_grupo on ms_actividad.id_grupo = cg_grupo.id_grupo " + _
                        "                            left join dt_grupo on cg_grupo.id_grupo = dt_grupo.id_grupo and dt_grupo.status = 'A' " + _
                        "                          where cg_grupo.status = 'A' " + _
                        "                            and (id_usr_secretario = @idUsuario " + _
                        "                              or id_usr_lider = @idUsuario) " + _
                        "                          union " + _
                        "                          select distinct(id_ms_actividad) " + _
                        "                          from ms_actividad " + _
                        "                          where id_usr_responsable = @idUsuario) "

                If .cbFecha.Checked = True Then
                    query = query + " and (ms_actividad.fecha_registro between @FI and @FT) "
                End If
                If .cbTema.Checked = True Then
                    query = query + " and ms_reunion.tema = @tema "
                End If
                If .cbAsignado.Checked = True Then
                    query = query + " and ms_actividad.id_usr_responsable = @idUsrResp "
                End If
                If .cbGrupo.Checked = True Then
                    query = query + " and ms_actividad.id_grupo = @idGrupo "
                End If
                If .cbStatus.Checked = True Then
                    Dim banC As Integer = 0
                    Dim Status As String = ""
                    For i = 0 To 4
                        If .cblStatus.Items(i).Selected = True Then
                            If banC = 0 Then
                                Status = Status + "'" + .cblStatus.Items(i).Value + "'"
                            Else
                                Status = Status + ", '" + .cblStatus.Items(i).Value + "'"
                            End If
                            banC = 1
                        End If
                    Next
                    query = query + " and ms_actividad.status in ( " + Status + " ) "
                End If
                If .cbNoActividad.Checked = True Then
                    query = query + " and ms_actividad.id_ms_actividad = @idMsAct"
                End If

                query = query + " order by ms_actividad.id_ms_actividad "

                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaConsulta.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))

                If .cbFecha.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@FI", .wdpFechaI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@FT", .wdpFechaF.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
                End If
                If .cbTema.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@tema", .txtTema.Text.Trim)
                End If
                If .cbAsignado.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@idUsrResp", .ddlAsignado.SelectedValue)
                End If
                If .cbGrupo.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@idGrupo", .ddlGrupo.SelectedValue)
                End If
                If .cbNoActividad.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@idMsAct", Val(.txtNoActividad.Text.Trim))
                End If

                .gvActividades.DataSource = dsConsulta
                .gvActividadesT.DataSource = dsConsulta
                ConexionBD.Open()
                sdaConsulta.Fill(dsConsulta)
                ConexionBD.Close()
                .gvActividades.DataBind()
                .gvActividadesT.DataBind()
                sdaConsulta.Dispose()
                dsConsulta.Dispose()
                .gvActividades.SelectedIndex = -1
                If .gvActividades.Rows.Count = 0 Then
                    .pnlTickets.Visible = False
                    .litError.Text = "No existe Registros para esos valores"
                Else
                    .pnlTickets.Visible = True

                    pintarTabla(.gvActividades)
                    pintarTablaTemp(.gvActividadesT)
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Tabla"

    Protected Sub gvActividades_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvActividades.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                ._txtIdMsAct.Text = .gvActividades.SelectedRow.Cells(1).Text
                .pnlTickets.Visible = False
                .pnlDetalle.Visible = True
                .lblFechaTer.Text = ""
                .pnlFechaTer.Visible = False
                'Llenar la información correspondiente a la actividad
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaTicket As New SqlDataAdapter
                Dim dsTicket As New DataSet
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD

                sdaTicket.SelectCommand = New SqlCommand("select ms_actividad.id_ms_actividad " + _
                                                         "     , ms_actividad.fecha_registro " + _
                                                         "     , ms_actividad.fecha_compromiso " + _
                                                         "     , ms_actividad.fecha_cierre " + _
                                                         "     , ms_actividad.grupo " + _
                                                         "     , ms_actividad.solicito " + _
                                                         "     , ms_actividad.responsable " + _
                                                         "     , ms_actividad.descripcion " + _
                                                         "     , ms_actividad.status " + _
                                                         "     , ms_actividad.id_usr_registro " + _
                                                         "     , isnull(cg_grupo.id_usr_secretario, 0) as id_usr_secretario " + _
                                                         "from ms_actividad " + _
                                                         "  left join cg_grupo on ms_actividad.id_grupo = cg_grupo.id_grupo " + _
                                                         "where id_ms_actividad = @idMsAct ", ConexionBD)
                sdaTicket.SelectCommand.Parameters.AddWithValue("@idMsAct", Val(._txtIdMsAct.Text))
                ConexionBD.Open()
                sdaTicket.Fill(dsTicket)
                ConexionBD.Close()
                .lblNoActividad.Text = dsTicket.Tables(0).Rows(0).Item("id_ms_actividad").ToString()
                .lblFecha.Text = dsTicket.Tables(0).Rows(0).Item("fecha_registro").ToString()
                .lblFechaComp.Text = dsTicket.Tables(0).Rows(0).Item("fecha_compromiso").ToString()
                Select Case dsTicket.Tables(0).Rows(0).Item("status").ToString()
                    Case "P"
                        .lblEstatus.Text = "En Proceso".ToUpper()
                    Case "PU"
                        .lblEstatus.Text = "Pend. Usuario".ToUpper()
                    Case "PP"
                        .lblEstatus.Text = "Pend. Proveedor".ToUpper()
                    Case "C"
                        .lblEstatus.Text = "Cerrado".ToUpper()
                        .lblFechaTer.Text = dsTicket.Tables(0).Rows(0).Item("fecha_cierre").ToString()
                        .pnlFechaTer.Visible = True
                        If Convert.ToDateTime(dsTicket.Tables(0).Rows(0).Item("fecha_cierre").ToString()) > Convert.ToDateTime(dsTicket.Tables(0).Rows(0).Item("fecha_compromiso").ToString()) Then
                            .lblFechaTer.ForeColor = Color.DarkRed
                        Else
                            .lblFechaTer.ForeColor = Color.DarkGreen
                        End If
                    Case "Ca"
                        .lblEstatus.Text = "Cancelado".ToUpper()
                End Select
                .lblGrupo.Text = dsTicket.Tables(0).Rows(0).Item("grupo").ToString()
                .lblResponsable.Text = dsTicket.Tables(0).Rows(0).Item("responsable").ToString()
                .lblSolicito.Text = dsTicket.Tables(0).Rows(0).Item("solicito").ToString()
                .txtDescripcion.Text = dsTicket.Tables(0).Rows(0).Item("descripcion").ToString()
                .lblIdUsrReg.Text = dsTicket.Tables(0).Rows(0).Item("id_usr_registro").ToString()
                .lblIdUsrSecre.Text = dsTicket.Tables(0).Rows(0).Item("id_usr_secretario").ToString()
                sdaTicket.Dispose()
                dsTicket.Dispose()
                actualizaCom()

                If Val(.lblIdUsrReg.Text) = Val(._txtIdUsuario.Text) Or Val(.lblIdUsrSecre.Text) = Val(._txtIdUsuario.Text) Then
                    'Secretario que registró la actividad
                    Select Case dsTicket.Tables(0).Rows(0).Item("status").ToString()
                        Case "P", "PU", "PP"
                            .pnlAccion.Visible = True
                            .ddlAccion.Items.Clear()
                            .ddlAccion.Items.Add("Agregar Comentario")
                            .ddlAccion.Items.Add("Solicitar Inf. al Usuario")
                            .ddlAccion.Items.Add("Reportar al Proveedor")
                            .ddlAccion.Items.Add("En Proceso")
                            .ddlAccion.Items.Add("Cerrar Actividad")
                            .ddlAccion.Items.Add("Cancelar Actividad")
                            .txtComentario.Text = ""
                            'Adjuntos
                            .lbl_Adjuntos.Visible = True
                            .FUAdjuntos.Visible = True
                            .cmdAdjuntar.Visible = True
                        Case "C", "Ca"
                            .pnlAccion.Visible = False
                            'Adjuntos
                            .lbl_Adjuntos.Visible = False
                            .FUAdjuntos.Visible = False
                            .cmdAdjuntar.Visible = False
                    End Select
                Else
                    .pnlAccion.Visible = False
                    'Adjuntos
                    .lbl_Adjuntos.Visible = False
                    .FUAdjuntos.Visible = False
                    .cmdAdjuntar.Visible = False
                End If
                .pnlFiltros.Visible = False

                'Actualizar Adjuntos
                actualizarGrid()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Detalle"

    Protected Sub btnNuevoBus_Click(sender As Object, e As EventArgs) Handles btnNuevoBus.Click
        With Me
            'Ocultar todos los paneles principales
            .pnlFiltros.Visible = True
            .pnlTickets.Visible = False
            .pnlDetalle.Visible = False
        End With
    End Sub

    Protected Sub cmdAdjuntar_Click(sender As Object, e As EventArgs) Handles cmdAdjuntar.Click
        ''Dim sFileDir As String = "C:/ProcAd - Adjuntos EPR/" 'Ruta en que se almacenará el archivo
        Dim sFileDir As String = "D:\ProcAd - Adjuntos EPR\" 'Ruta en que se almacenará el archivo
        Dim lMaxFileSize As Long = 10485760 'Equivalente a 10 Megabytes

        'Verificar que el archivo ha sido seleccionado y es un archivo válido
        If (Not FUAdjuntos.PostedFile Is Nothing) And (FUAdjuntos.PostedFile.ContentLength > 0) Then
            'Determinar el nombre original del archivo
            Dim sFileName As String = System.IO.Path.GetFileName(FUAdjuntos.PostedFile.FileName)
            Dim idArchivo As Integer 'Index correspondiente al archivo
            Dim fecha As DateTime = Date.Now
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
            SCMValores.Connection = ConexionBD
            Try
                If FUAdjuntos.PostedFile.ContentLength <= lMaxFileSize Then

                    'Registrar el archivo en la base de datos
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "INSERT INTO ms_archivoA(id_ms_actividad, id_usuario, nombre, fecha) values(@id_ms_actividad, @id_usuario, @nombre, @fecha)"
                    SCMValores.Parameters.AddWithValue("@id_ms_actividad", Val(Me._txtIdMsAct.Text))
                    SCMValores.Parameters.AddWithValue("@id_usuario", Val(Me._txtIdUsuario.Text))
                    SCMValores.Parameters.AddWithValue("@nombre", sFileName)
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()
                    'Obtener el Id del archivo
                    SCMValores.CommandText = "select max(id_ms_archivoA) from ms_archivoA where (id_ms_actividad = @id_ms_actividad) and (fecha = @fecha)"
                    ConexionBD.Open()
                    idArchivo = SCMValores.ExecuteScalar
                    ConexionBD.Close()
                    'Se agrega el Id al nombre del archivo
                    sFileName = idArchivo.ToString + "-" + sFileName
                    'Almacenar el archivo en la ruta especificada
                    FUAdjuntos.PostedFile.SaveAs(sFileDir + sFileName)
                    'lblMessage.Visible = True
                    'lblMessage.Text = "El archivo: " + sFileName + " se ha adjuntado exitosamente"
                    actualizarGrid()
                Else
                    'Rechazar el archivo
                    lblMessage.Visible = True
                    lblMessage.Text = "El archivo excede el límite de 10 MB"
                End If
            Catch exc As Exception    'En caso de error
                'Eliminar el archivo en la base de datos
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "delete from ms_archivo where id_archivo = @idArchivo"
                SCMValores.Parameters.AddWithValue("@idArchivo", idArchivo)
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                lblMessage.Visible = True
                lblMessage.Text = "Un Error ha ocurrido. Favor de intentarlo nuevamente"
                ''''DeleteFile(sFileDir + sFileName)
            End Try
        Else
            lblMessage.Visible = True
            lblMessage.Text = "Favor de seleccionar un Archivo"
        End If
    End Sub

    Protected Sub btnActualiza_Click(sender As Object, e As EventArgs) Handles btnActualiza.Click
        With Me
            Try
                .litError.Text = ""
                If .txtDescripcion.Text.Length > 990 Then
                    .litError.Text = "Favor de reducir la descripción, el máximo de caracteres permitido es 950"
                Else
                    Select Case .ddlAccion.SelectedValue
                        Case "Agregar Comentario"
                            If .txtComentario.Text.Trim = "" Then
                                .litError.Text = "Favor de ingresar el Comentario"
                            Else
                                'Usuario
                                cambioStatus("P")
                                'Actualizar Leyenda de Estatus
                                .lblEstatus.Text = "En Proceso".ToUpper()

                                ''Enviar Correo al Usuario que atendió
                                'enviarMailComentario(._txtDestinatario.Text)
                                ''Enviar Correo al Usuario que reportó
                                'enviarMailComentario(._txtDestinatarioE.Text)

                                .txtComentario.Text = ""
                            End If
                        Case "Cancelar Actividad"
                            If .txtComentario.Text.Trim = "" Then
                                .litError.Text = "Favor de ingresar el motivo por el que se requiere Cancelar el Ticket"
                            Else
                                cambioStatus("Ca")
                                'Actualizar Leyenda de Estatus 
                                .lblEstatus.Text = "Cancelado".ToUpper()
                                .pnlAccion.Visible = False
                                'Adjuntos
                                .lbl_Adjuntos.Visible = False
                                .FUAdjuntos.Visible = False
                                .cmdAdjuntar.Visible = False

                                ''Enviar Correo al Usuario que atendió
                                'enviarMailCancelado(._txtDestinatario.Text)
                                ''Enviar Correo al Usuario que reportó
                                'enviarMailCancelado(._txtDestinatarioE.Text)
                            End If
                        Case "Cerrar Actividad"
                            cambioStatus("C")
                            'Actualizar Leyenda de Estatus
                            .lblEstatus.Text = "Cerrado".ToUpper()
                            .pnlFechaTer.Visible = True
                            .pnlAccion.Visible = False
                            'Adjuntos
                            .lbl_Adjuntos.Visible = False
                            .FUAdjuntos.Visible = False
                            .cmdAdjuntar.Visible = False

                            ''Enviar Correo al Usuario que atendió
                            'enviarMailCierre(._txtDestinatario.Text)
                            ''Enviar Correo al Usuario que reportó
                            'enviarMailCierre(._txtDestinatarioE.Text)
                        Case "Solicitar Inf. al Usuario"
                            If .txtComentario.Text.Trim = "" Then
                                .litError.Text = "Favor de ingresar el Comentario"
                            Else
                                cambioStatus("PU")
                                'Actualizar Leyenda de Estatus 
                                .lblEstatus.Text = "Pend. Usuario".ToUpper()

                                ''Enviar Correo al Usuario que atendió
                                'enviarMailComentario(._txtDestinatario.Text)
                                ''Enviar Correo al Usuario que reportó
                                'enviarMailComentario(._txtDestinatarioE.Text)

                                .txtComentario.Text = ""
                            End If
                        Case "Reportar al Proveedor"
                            If .txtComentario.Text.Trim = "" Then
                                .litError.Text = "Favor de ingresar el Comentario"
                            Else
                                cambioStatus("PP")
                                'Actualizar Leyenda de Estatus 
                                .lblEstatus.Text = "Pend. Proveedor".ToUpper()

                                ''Enviar Correo al Usuario que atendió
                                'enviarMailComentario(._txtDestinatario.Text)
                                ''Enviar Correo al Usuario que reportó
                                'enviarMailComentario(._txtDestinatarioE.Text)

                                .txtComentario.Text = ""
                            End If
                        Case "En Proceso"
                            If .txtComentario.Text.Trim = "" Then
                                .litError.Text = "Favor de ingresar el Comentario"
                            Else
                                cambioStatus("P")
                                'Actualizar Leyenda de Estatus 
                                .lblEstatus.Text = "En Proceso".ToUpper()
                                
                                ''Enviar Correo al Usuario que atendió
                                'enviarMailComentario(._txtDestinatario.Text)
                                ''Enviar Correo al Usuario que reportó
                                'enviarMailComentario(._txtDestinatarioE.Text)

                                .txtComentario.Text = ""
                            End If
                    End Select
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Exportar"

    Protected Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        Dim tw As New System.IO.StringWriter
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        With Me
            Try
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment;filename=Tickets.xls")
                Response.ContentType = "application/vnd.ms-excel"
                .gvActividadesT.Visible = True
                .gvActividadesT.RenderControl(hw)
                .gvActividadesT.Visible = False
                Response.Write(tw.ToString())
                Response.End()
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub

#End Region

End Class