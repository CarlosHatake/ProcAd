Public Class AltaActividad
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

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")

                        'Catálogo de Grupos
                        Dim sdaCatalogo As New SqlDataAdapter
                        Dim dsCatalogo As New DataSet
                        .ddlGrupo.DataSource = dsCatalogo
                        sdaCatalogo.SelectCommand = New SqlCommand("select cg_grupo.id_grupo " +
                                                                   "     , grupo " +
                                                                   "from cg_grupo " +
                                                                   "  left join dt_grupo on cg_grupo.id_grupo = dt_grupo.id_grupo " +
                                                                   "where cg_grupo.status = 'A' " +
                                                                   "  and dt_grupo.status = 'A' " +
                                                                   "  and id_usr_part = @idUsuario " +
                                                                   "order by grupo ", ConexionBD)
                        sdaCatalogo.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        .ddlGrupo.DataTextField = "grupo"
                        .ddlGrupo.DataValueField = "id_grupo"
                        ConexionBD.Open()
                        sdaCatalogo.Fill(dsCatalogo)
                        .ddlGrupo.DataBind()
                        ConexionBD.Close()
                        sdaCatalogo.Dispose()
                        dsCatalogo.Dispose()
                        .ddlGrupo.SelectedIndex = -1

                        .wdpFechaComp.Date = Date.Now.AddDays(1)
                        llenarEmpleados(.ddlSolicito, .txtSolicito.Text, 0)
                        llenarEmpleados(.ddlResponsable, .txtResponsable.Text, 1)

                        'Eliminar registro de Adjuntos no almacenados previamente
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "DELETE from ms_archivoA where id_ms_actividad = -1 and id_usuario = @id_usuario"
                        SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Panel
                        .pnlInicio.Visible = True
                        .pnlActividad.Enabled = True
                        .btnGuardar.Enabled = True
                        .btnNuevo.Enabled = False
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
                    query = query + "  and id_usuario = @id_usuario "
                End If

                query = query + "order by empleado "

                'Catálogo de Empleados
                lista.DataSource = dsCatalogo
                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@nombreEmpl", txtNombe)
                If responsable = 1 Then
                    sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                End If
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

#Region "Actividad"

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

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
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
                    SCMValores.Parameters.AddWithValue("@id_ms_reunion", 0)
                    SCMValores.Parameters.AddWithValue("@id_grupo", .ddlGrupo.SelectedValue)
                    SCMValores.Parameters.AddWithValue("@grupo", .ddlGrupo.SelectedItem.Text)
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

                    SCMValores.CommandText = "select max(id_ms_actividad) from ms_actividad where (id_ms_reunion=@id_ms_reunion) and (id_usr_responsable=@id_usr_responsable)"
                    ConexionBD.Open()
                    .lblFolio.Text = SCMValores.ExecuteScalar
                    ConexionBD.Close()


                    'Actualizar los registros de los archivos adjuntos
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_archivoA set id_ms_actividad = @id_ms_actividad where id_ms_actividad = -1 and id_usuario = @id_usuario"
                    SCMValores.Parameters.AddWithValue("@id_ms_actividad", Val(.lblFolio.Text))
                    SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    .pnlActividad.Enabled = False
                    .btnGuardar.Enabled = False
                    .btnNuevo.Enabled = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        With Me
            Try
                .lblFolio.Text = ""
                .txtDescrip.Text = ""
                .wdpFechaComp.Date = Date.Now.AddDays(1)
                .txtSolicito.Text = ""
                llenarEmpleados(.ddlSolicito, .txtSolicito.Text, 0)
                .txtResponsable.Text = ""
                llenarEmpleados(.ddlResponsable, .txtResponsable.Text, 1)

                actualizarGrid()

                .pnlActividad.Enabled = True
                .btnGuardar.Enabled = True
                .btnNuevo.Enabled = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class