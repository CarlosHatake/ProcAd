Public Class SegReunion
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 5
                    'Session("idMsInst") = 14

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtIdMsInst.Text = Session("idMsInst")

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim sdaSol As New SqlDataAdapter
                        Dim dsSol As New DataSet
                        Dim query As String
                        query = "select tema " +
                                "     , id_grupo " +
                                "     , grupo " +
                                "     , convert(varchar, fecha_reunion, 103) + replace(replace(replace(right(convert(varchar, fecha_reunion, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') as fecha_reunion " +
                                "     , status " +
                                "from ms_reunion " +
                                "where id_ms_reunion = @id_ms_reunion "
                        sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@id_ms_reunion", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()
                        .lblFolio.Text = ._txtIdMsInst.Text
                        .lblTema.Text = dsSol.Tables(0).Rows(0).Item("tema").ToString()
                        ._txtIdGrupo.Text = dsSol.Tables(0).Rows(0).Item("id_grupo").ToString()
                        .lblGrupo.Text = dsSol.Tables(0).Rows(0).Item("grupo").ToString()
                        .lblFecha.Text = dsSol.Tables(0).Rows(0).Item("fecha_reunion").ToString()
                        If dsSol.Tables(0).Rows(0).Item("status").ToString() = "P" Then
                            .btnIniciar.Visible = True
                            .gvAsistencia.Visible = False
                            .btnFinalizar.Enabled = False
                        Else
                            .btnIniciar.Visible = False
                            .gvAsistencia.Visible = True
                            .btnFinalizar.Enabled = True
                        End If
                        sdaSol.Dispose()
                        dsSol.Dispose()

                        If .gvAsistencia.Visible = True Then
                            actualizarAsistencia()
                        End If
                        actualizarParticipacion()
                        actualizarActividad()

                        llenarEmpleados(.ddlSolicito, .txtSolicito.Text, 0)
                        llenarEmpleados(.ddlResponsable, .txtResponsable.Text, 1)

                        'Panel
                        .pnlInicio.Visible = True
                        .pnlParticipaPN.Visible = False
                        .pnlActividad.Visible = False
                    Else
                        Server.Transfer("Login.aspx")
                    End If
                Catch ex As Exception
                    .litError.Text = ex.ToString
                End Try
            End With
        End If
    End Sub

    Protected Sub btnIniciar_Click(sender As Object, e As EventArgs) Handles btnIniciar.Click
        With Me
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
            SCMValores.Connection = ConexionBD
            SCMValores.CommandText = ""
            SCMValores.Parameters.Clear()
            SCMValores.CommandText = "update ms_reunion " +
                                     "  set status = 'I', fecha_inicio = case when fecha_reunion > @fecha then fecha_reunion else @fecha end " +
                                     "where id_ms_reunion = @id_ms_reunion "
            SCMValores.Parameters.AddWithValue("@fecha", Date.Now)
            SCMValores.Parameters.AddWithValue("@id_ms_reunion", Val(._txtIdMsInst.Text))
            ConexionBD.Open()
            SCMValores.ExecuteNonQuery()
            ConexionBD.Close()

            .btnIniciar.Visible = False
            .gvAsistencia.Visible = True
            .btnFinalizar.Enabled = True
            actualizarAsistencia()
        End With
    End Sub

#End Region

#Region "Funciones"

    Public Sub actualizarAsistencia()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaParticipantes As New SqlDataAdapter
                Dim dsParticipantes As New DataSet
                .gvAsistencia.Columns(0).Visible = True
                .gvAsistencia.Columns(1).Visible = True
                .gvAsistencia.Columns(4).Visible = True
                .gvAsistencia.DataSource = dsParticipantes
                'Catálogo de Participantes
                sdaParticipantes.SelectCommand = New SqlCommand("select id_dt_reunion " +
                                                                "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as integrante " +
                                                                "     , replace(replace(replace(right(convert(varchar, llegada_fecha, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') as llegada_hora " +
                                                                "     , case when llegada_fecha is null then 'No' else 'Sí' end as listo " +
                                                                "from dt_reunion " +
                                                                "  left join cg_usuario on dt_reunion.id_usr_part = cg_usuario.id_usuario " +
                                                                "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                                "where id_ms_reunion = @id_ms_reunion ", ConexionBD)
                sdaParticipantes.SelectCommand.Parameters.AddWithValue("@id_ms_reunion", Val(._txtIdMsInst.Text))
                ConexionBD.Open()
                sdaParticipantes.Fill(dsParticipantes)
                .gvAsistencia.DataBind()
                ConexionBD.Close()
                sdaParticipantes.Dispose()
                dsParticipantes.Dispose()
                .gvAsistencia.SelectedIndex = -1
                .gvAsistencia.Columns(0).Visible = False
                '.gvAsistencia.Columns(1).Visible = False

                For i = 0 To .gvAsistencia.Rows.Count - 1
                    If .gvAsistencia.Rows(i).Cells(4).Text = "No" Then
                        .gvAsistencia.Rows(i).Cells(1).Controls(0).Visible = True
                    Else
                        .gvAsistencia.Rows(i).Cells(1).Controls(0).Visible = False
                    End If
                Next
                .gvAsistencia.Columns(4).Visible = False

                ' .gvAsistencia.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub actualizarParticipacion()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaParticipantes As New SqlDataAdapter
                Dim dsParticipantes As New DataSet
                .gvParticipacion.Columns(0).Visible = True
                .gvParticipacion.DataSource = dsParticipantes
                'Catálogo de Participantes
                sdaParticipantes.SelectCommand = New SqlCommand("select id_dt_reunion " +
                                                                "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as integrante " +
                                                                "     , particip_pos " +
                                                                "     , particip_neut " +
                                                                "from dt_reunion " +
                                                                "  left join cg_usuario on dt_reunion.id_usr_part = cg_usuario.id_usuario " +
                                                                "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                                "where id_ms_reunion = @id_ms_reunion ", ConexionBD)
                sdaParticipantes.SelectCommand.Parameters.AddWithValue("@id_ms_reunion", Val(._txtIdMsInst.Text))
                ConexionBD.Open()
                sdaParticipantes.Fill(dsParticipantes)
                .gvParticipacion.DataBind()
                ConexionBD.Close()
                sdaParticipantes.Dispose()
                dsParticipantes.Dispose()
                .gvParticipacion.Columns(0).Visible = False
                .gvParticipacion.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub registrarParticipacion(ByVal tipoParticipacion)
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "update dt_reunion " +
                                         "  set " + tipoParticipacion + " = (select isnull(" + tipoParticipacion + ", 0) as participacion " +
                                         "                       from dt_reunion " +
                                         "                       where id_dt_reunion = @id_dt_reunion) + 1 " +
                                         "where id_dt_reunion = @id_dt_reunion "
                SCMValores.Parameters.AddWithValue("@id_dt_reunion", .gvParticipacion.SelectedRow.Cells(0).Text)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                .pnlParticipaPN.Visible = False

                actualizarParticipacion()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub actualizarActividad()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaParticipantes As New SqlDataAdapter
                Dim dsParticipantes As New DataSet
                .gvActividad.Columns(0).Visible = True
                .gvActividad.DataSource = dsParticipantes
                'Catálogo de Participantes
                sdaParticipantes.SelectCommand = New SqlCommand("select id_dt_reunion " +
                                                                "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as integrante " +
                                                                "     , count(id_ms_actividad) as total " +
                                                                "     , sum(case when ms_actividad.status in ('P', 'PP', 'PU') then case when fecha_compromiso > getdate() then 1 else 0 end else case when (fecha_compromiso > ms_actividad.fecha_cierre and (ms_actividad.id_ms_reunion_cont is null or ms_actividad.id_ms_reunion_cont = @id_ms_reunion)) then 1 else 0 end end) as et " +
                                                                "     , sum(case when ms_actividad.status in ('P', 'PP', 'PU') then case when fecha_compromiso <= getdate() then 1 else 0 end else case when (fecha_compromiso <= ms_actividad.fecha_cierre and (ms_actividad.id_ms_reunion_cont is null or ms_actividad.id_ms_reunion_cont = @id_ms_reunion)) then 1 else 0 end end) as ft " +
                                                                "     , case when count(id_ms_actividad) = 0 then null else (cast(sum(case when ms_actividad.status in ('P', 'PP', 'PU') then case when fecha_compromiso > getdate() then 1 else 0 end else case when fecha_compromiso > ms_actividad.fecha_cierre then 1 else 0 end end) as decimal) / count(id_ms_actividad)) * 10 end as Calif " +
                                                                "from dt_reunion " +
                                                                "  left join cg_usuario on dt_reunion.id_usr_part = cg_usuario.id_usuario " +
                                                                "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                                "  left join ms_actividad on ms_actividad.id_grupo = @id_grupo and dt_reunion.id_usr_part = ms_actividad.id_usr_responsable and (ms_actividad.status in ('P', 'PP', 'PU') or (ms_actividad.status = 'C' and (id_ms_reunion_cont is null or ms_actividad.id_ms_reunion_cont = @id_ms_reunion))) " +
                                                                "where dt_reunion.id_ms_reunion = @id_ms_reunion " +
                                                                "  and ms_actividad.status <> 'Ca' " +
                                                                "group by id_dt_reunion, cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno ", ConexionBD)
                sdaParticipantes.SelectCommand.Parameters.AddWithValue("@id_ms_reunion", Val(._txtIdMsInst.Text))
                sdaParticipantes.SelectCommand.Parameters.AddWithValue("@id_grupo", Val(._txtIdGrupo.Text))
                ConexionBD.Open()
                sdaParticipantes.Fill(dsParticipantes)
                .gvActividad.DataBind()
                ConexionBD.Close()
                sdaParticipantes.Dispose()
                dsParticipantes.Dispose()
                .gvActividad.Columns(0).Visible = False
                .gvActividad.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarEmpleados(ByRef lista, ByVal txtNombe, ByVal responsable)
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                Dim query As String

                query = "select id_usuario " +
                        "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as empleado " +
                        "from cg_usuario " +
                        "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                        "where cg_usuario.status = 'A' " +
                        "  and cgEmpl.status = 'A' " +
                        "  and cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno like '%' + @nombreEmpl + '%' "

                'Se filtran solo los participantes que pertenecen a la reunión para la lista de responsables
                If responsable = 1 Then
                    query = query + "  and id_usuario in (select id_usr_part " +
                                    "                     from dt_reunion " +
                                    "                     where id_ms_reunion = @id_ms_reunion) "
                End If

                query = query + "order by empleado "



                'Catálogo de Empleados
                lista.DataSource = dsCatalogo
                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@nombreEmpl", txtNombe)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_ms_reunion", Val(._txtIdMsInst.Text))
                lista.DataTextField = "empleado"
                lista.DataValueField = "id_usuario"
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                lista.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                lista.SelectedIndex = -1
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
                sdaCatalogo.SelectCommand = New SqlCommand("select id_ms_archivoA " +
                                                           "     , id_ms_actividad " +
                                                           "     , nombre as Archivo " +
                                                           "     , 'http://148.223.153.43/ProcAd - Adjuntos EPR/' + CAST(id_ms_archivoA as varchar(10)) + '-' + nombre as Path " +
                                                           "     , nombre as Ruta, fecha as Fecha " +
                                                           "from ms_archivoA " +
                                                           "where id_ms_actividad = -1 and id_usuario = @idUsuario ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
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

#End Region

#Region "Tablas"

    Protected Sub gvAsistencia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvAsistencia.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "update dt_reunion " +
                                         "  set llegada_fecha = getdate() " +
                                         "    , llegada_calif = (select isnull((select top 1 calificacion " +
                                         "                                      from dt_calif_llegada " +
                                         "                                      where datediff(minute, fecha_inicio, getdate()) >= minutos_min " +
                                         "                                        and datediff(minute, fecha_inicio, getdate()) <= minutos_max), 0) as llegada_calfi " +
                                         "                       from dt_reunion " +
                                         "                         left join ms_reunion on dt_reunion.id_ms_reunion = ms_reunion.id_ms_reunion " +
                                         "                       where id_dt_reunion = @id_dt_reunion) " +
                                         "where id_dt_reunion = @id_dt_reunion "
                SCMValores.Parameters.AddWithValue("@id_dt_reunion", .gvAsistencia.SelectedRow.Cells(0).Text)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                actualizarAsistencia()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub gvParticipacion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvParticipacion.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                Dim idUsuario As Integer
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select id_usr_part " +
                                         "from dt_reunion " +
                                         "where id_dt_reunion = @id_dt_reunion "
                SCMValores.Parameters.AddWithValue("@id_dt_reunion", .gvParticipacion.SelectedRow.Cells(0).Text)
                ConexionBD.Open()
                idUsuario = SCMValores.ExecuteScalar
                ConexionBD.Close()

                If idUsuario = Val(._txtIdUsuario.Text) Then
                    .pnlParticipaPN.Visible = False
                    .gvParticipacion.SelectedIndex = -1
                Else
                    .pnlParticipaPN.Visible = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Participación"

    Protected Sub btnPositivaP_Click(sender As Object, e As EventArgs) Handles btnPositivaP.Click
        registrarParticipacion("particip_pos")
    End Sub

    'Protected Sub btnNeutralP_Click(sender As Object, e As EventArgs) Handles btnNeutralP.Click
    '    registrarParticipacion("particip_neut")
    'End Sub

    Protected Sub btnCancelarP_Click(sender As Object, e As EventArgs) Handles btnCancelarP.Click
        Me.pnlParticipaPN.Visible = False
        Me.gvParticipacion.SelectedIndex = -1
    End Sub

#End Region

#Region "Actividad"

    Protected Sub ibtnAlta_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAlta.Click
        With Me
            Try
                .gvActividad.Visible = False
                .ibtnAlta.Visible = False
                .pnlActividad.Visible = True
                .btnFinalizar.Visible = False

                .txtDescrip.Text = ""
                .wdpFechaComp.Date = Date.Now.AddDays(1)
                .txtSolicito.Text = ""
                llenarEmpleados(.ddlSolicito, .txtSolicito.Text, 0)
                .txtResponsable.Text = ""
                llenarEmpleados(.ddlResponsable, .txtResponsable.Text, 1)

                actualizarGrid()

                'Eliminar registro de Adjuntos no almacenados previamente
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "DELETE from ms_archivoA where id_ms_actividad = -1 and id_usuario = @id_usuario"
                SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
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
                    SCMValores.Parameters.AddWithValue("@id_ms_actividad", -1)
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
                SCMValores.CommandText = "delete from ms_archivoA where id_ms_archivoA = @idArchivo"
                SCMValores.Parameters.AddWithValue("@idArchivo", idArchivo)
                ConexionBD.Open()
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

    Protected Sub cmdBuscarSol_Click(sender As Object, e As EventArgs) Handles cmdBuscarSol.Click
        llenarEmpleados(Me.ddlSolicito, Me.txtSolicito.Text, 0)
    End Sub

    Protected Sub cmdBuscarResp_Click(sender As Object, e As EventArgs) Handles cmdBuscarResp.Click
        llenarEmpleados(Me.ddlResponsable, Me.txtResponsable.Text, 1)
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""

                If .txtDescrip.Text.Trim = "" Or .ddlSolicito.Items.Count = 0 Or .ddlResponsable.Items.Count = 0 Then
                    .litError.Text = "Información Insuficiente, favor de Validar"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into ms_actividad ( id_ms_reunion,  id_grupo,  grupo,  descripcion,  id_usr_solicito,  solicito,  id_usr_responsable,  responsable,  id_usr_registro,  fecha_registro,  fecha_compromiso) " +
                                             "                  values (@id_ms_reunion, @id_grupo, @grupo, @descripcion, @id_usr_solicito, @solicito, @id_usr_responsable, @responsable, @id_usr_registro,       getdate(), @fecha_compromiso) "
                    SCMValores.Parameters.AddWithValue("@id_ms_reunion", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_grupo", Val(._txtIdGrupo.Text))
                    SCMValores.Parameters.AddWithValue("@grupo", .lblGrupo.Text)
                    SCMValores.Parameters.AddWithValue("@descripcion", .txtDescrip.Text.Trim)
                    SCMValores.Parameters.AddWithValue("@id_usr_solicito", .ddlSolicito.SelectedValue)
                    SCMValores.Parameters.AddWithValue("@solicito", .ddlSolicito.SelectedItem.Text)
                    SCMValores.Parameters.AddWithValue("@id_usr_responsable", .ddlResponsable.SelectedValue)
                    SCMValores.Parameters.AddWithValue("@responsable", .ddlResponsable.SelectedItem.Text)
                    SCMValores.Parameters.AddWithValue("@id_usr_registro", Val(._txtIdUsuario.Text))
                    SCMValores.Parameters.AddWithValue("@fecha_compromiso", .wdpFechaComp.Date)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    Dim idMsActividad As Integer
                    SCMValores.CommandText = "select max(id_ms_actividad) from ms_actividad where (id_ms_reunion=@id_ms_reunion) and (id_usr_responsable=@id_usr_responsable)"
                    ConexionBD.Open()
                    idMsActividad = SCMValores.ExecuteScalar
                    ConexionBD.Close()


                    'Actualizar los registros de los archivos adjuntos
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_archivoA set id_ms_actividad = @id_ms_actividad where id_ms_actividad = -1 and id_usuario = @id_usuario"
                    SCMValores.Parameters.AddWithValue("@id_ms_actividad", idMsActividad)
                    SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Enviar Mail 
                    Dim Mensaje As New System.Net.Mail.MailMessage()
                    Dim destinatario As String = ""
                    'Obtener el Correo del Responsable
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "select cgEmpl.correo " +
                                             "from cg_usuario " +
                                             "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                             "where id_usuario = @id_usuario "
                    SCMValores.Parameters.AddWithValue("@id_usuario", .ddlResponsable.SelectedValue)
                    ConexionBD.Open()
                    destinatario = SCMValores.ExecuteScalar()
                    ConexionBD.Close()

                    Mensaje.[To].Add(destinatario)
                    Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                    Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                    Mensaje.Subject = "ProcAd - Nueva Actividad"
                    Mensaje.Body = "<span style=""font-family:Verdana; font-size: 10pt;"">" +
                                   "En la reunión <b>" + ._txtIdMsInst.Text + "</b> del Grupo <b>" + .lblGrupo.Text + "</b> se te asignó la actividad... " +
                                   "<br><br><b>" + .txtDescrip.Text.Trim + "</b>" +
                                   "<br><br>Con Fecha Compromiso: <b>" + .wdpFechaComp.Date.ToString + "</b>" +
                                   "<br><br>Cualquier duda, te invitamos a revisar la actividad en el portal del ProcAd http://148.223.153.43/ProcAd" +
                                   "</span>" +
                                   "<span style=""font-family: Verdana; font-size: 9pt; color: #FF0000; "">" +
                                   "<br><br> <b>Nota: Favor de <u>no</u> contestar a este correo</b>" +
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

                    actualizarActividad()

                    .gvActividad.Visible = True
                    .ibtnAlta.Visible = True
                    .pnlActividad.Visible = False
                    .btnFinalizar.Visible = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.gvActividad.Visible = True
        Me.ibtnAlta.Visible = True
        Me.pnlActividad.Visible = False
        Me.btnFinalizar.Visible = True
    End Sub

#End Region

#Region "Finalizar"

    Protected Sub btnFinalizar_Click(sender As Object, e As EventArgs) Handles btnFinalizar.Click
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD

                'Actualizar Datos de Actividades
                actualizarActividad()

                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "update dt_reunion " +
                                         "  set activ_tot = @activ_tot, activ_et = @activ_et, activ_ft = @activ_ft, activ_calif = @activ_calif " +
                                         "where id_dt_reunion = @id_dt_reunion "
                SCMValores.Parameters.Add("@activ_tot", SqlDbType.Int)
                SCMValores.Parameters.Add("@activ_et", SqlDbType.Int)
                SCMValores.Parameters.Add("@activ_ft", SqlDbType.Int)
                SCMValores.Parameters.Add("@activ_calif", SqlDbType.Decimal)
                SCMValores.Parameters.Add("@id_dt_reunion", SqlDbType.Int)

                For i = 0 To (.gvActividad.Rows.Count() - 1)
                    If Val(.gvActividad.Rows(i).Cells(2).Text.Trim) = 0 Then
                        SCMValores.Parameters("@activ_tot").Value = 0
                        SCMValores.Parameters("@activ_et").Value = 0
                        SCMValores.Parameters("@activ_ft").Value = 0
                        SCMValores.Parameters("@activ_calif").Value = DBNull.Value
                    Else
                        SCMValores.Parameters("@activ_tot").Value = Val(.gvActividad.Rows(i).Cells(2).Text.Trim)
                        SCMValores.Parameters("@activ_et").Value = Val(.gvActividad.Rows(i).Cells(3).Text.Trim)
                        SCMValores.Parameters("@activ_ft").Value = Val(.gvActividad.Rows(i).Cells(4).Text.Trim)
                        SCMValores.Parameters("@activ_calif").Value = Val(.gvActividad.Rows(i).Cells(5).Text.Trim)
                    End If
                    SCMValores.Parameters("@id_dt_reunion").Value = Val(.gvActividad.Rows(i).Cells(0).Text.Trim)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()
                Next

                'Actualizar las actividades Finalizadas, contabilizadas en la reunión
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "update ms_actividad " +
                                         "  set id_ms_reunion_cont = @id_ms_reunion " +
                                         "where id_ms_actividad in (select distinct(id_ms_actividad) " +
                                         "                          from dt_reunion " +
                                         "                            left join ms_reunion on dt_reunion.id_ms_reunion = ms_reunion.id_ms_reunion " +
                                         "                            left join ms_actividad on ms_reunion.id_grupo = ms_actividad.id_grupo and dt_reunion.id_usr_part = ms_actividad.id_usr_responsable and (ms_actividad.status = 'P' or (ms_actividad.status = 'C' and (id_ms_reunion_cont is null or ms_actividad.id_ms_reunion_cont = @id_ms_reunion))) " +
                                         "                          where dt_reunion.id_ms_reunion = @id_ms_reunion " +
                                         "                            and ms_actividad.status <> 'P' " +
                                         "                            and ms_actividad.id_ms_reunion_cont is null) "
                SCMValores.Parameters.AddWithValue("@id_ms_reunion", Val(._txtIdMsInst.Text))
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                'Actualizar Estatus de la Reunión
                SCMValores.CommandText = "update ms_reunion " +
                                         "  set status = 'F', fecha_cierre = getdate() " +
                                         "where id_ms_reunion = @id_ms_reunion "
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                'Insertar registros para Evaluación Cruzada
                SCMValores.CommandText = "insert into ms_contribucion (id_dt_reunion, id_usr_evaluador, fecha_registro) " +
                                         "select dt_reunion.id_dt_reunion " +
                                         "     , dtReunionT.id_usr_part as id_usr_evaluador " +
                                         "     , getdate() as fecha_registro " +
                                         "from dt_reunion " +
                                         "  left join dt_reunion dtReunionT on dt_reunion.id_ms_reunion = dtReunionT.id_ms_reunion and dt_reunion.id_usr_part <> dtReunionT.id_usr_part and dtReunionT.llegada_fecha is not null " +
                                         "where dt_reunion.id_ms_reunion = @id_ms_reunion " +
                                         "  and dt_reunion.llegada_fecha is not null "
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                'Actualizar Estatus de registros de Ausentes
                SCMValores.CommandText = "update dt_reunion " +
                                         "  set status = 'Au' " +
                                         "where id_ms_reunion = @id_ms_reunion " +
                                         "  and llegada_fecha is null "
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                .gvAsistencia.Enabled = False
                .gvParticipacion.Enabled = False
                .ibtnAlta.Visible = False
                .btnFinalizar.Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class