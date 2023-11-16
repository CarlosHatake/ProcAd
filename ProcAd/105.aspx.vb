Public Class _105
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess
    Dim clsCorreo As New Correo

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try

                    .litError.Text = ""
                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtBan.Text = 0

                        'Creación de Variables para la conexión y consulta de información a la base de datos
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        'Nombre del Solicitante
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "select nombre + ' ' + ap_paterno + ' ' + ap_materno " +
                                                 "from cg_usuario " +
                                                 "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                 "where id_usuario = @idUsuario "
                        SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        .lblSolicitante.Text = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        'Lista de Empresas
                        Dim sdaEmpresa As New SqlDataAdapter
                        Dim dsEmpresa As New DataSet
                        sdaEmpresa.SelectCommand = New SqlCommand("select distinct(cg_empresa.id_empresa) as id_empresa " +
                                                                  "     , cg_empresa.nombre " +
                                                                  "from bd_Empleado.dbo.cg_cc " +
                                                                  "  left join bd_Empleado.dbo.cg_empresa on cg_cc.id_empresa = cg_empresa.id_empresa " +
                                                                  "where cg_cc.status = 'A' " +
                                                                  "  and id_usr_responsable = @idUsr " +
                                                                  "order by nombre ", ConexionBD)
                        sdaEmpresa.SelectCommand.Parameters.AddWithValue("@idUsr", Val(._txtIdUsuario.Text))
                        .ddlEmpresa.DataSource = dsEmpresa
                        .ddlEmpresa.DataTextField = "nombre"
                        .ddlEmpresa.DataValueField = "id_empresa"
                        ConexionBD.Open()
                        sdaEmpresa.Fill(dsEmpresa)
                        .ddlEmpresa.DataBind()
                        ConexionBD.Close()
                        sdaEmpresa.Dispose()
                        dsEmpresa.Dispose()
                        .upEmpresa.Update()
                        '.ddlEmpresa.SelectedValue = 9 'Se elige TRACSA por Default
                        'Centros de Costo
                        llenarCC()

                        'Lista de Años
                        llenarAño()

                        'Eliminar registro de Montos no almacenadas previamente
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "DELETE from dt_ampliacion_p where id_ms_ampliacion_p = -1 and id_usuario = @id_usuario"
                        SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()
                        actualizarDtAmpliacion()

                        'Eliminar registro de Adjuntos no almacenados previamente
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "DELETE from dt_archivo_pgv where id_ms_ampliacion_p = -1 and id_usuario = @id_usuario"
                        SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()
                        actualizarAdjuntos()

                        'Justificación
                        .txtJustMotivo.Text = ""
                        .txtJustBeneficio.Text = ""
                        .txtJustImpacto.Text = ""
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

    Public Sub llenarCC()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCC As New SqlDataAdapter
                Dim dsCC As New DataSet
                sdaCC.SelectCommand = New SqlCommand("select id_cc " +
                                                     "     , nombre " +
                                                     "from bd_Empleado.dbo.cg_cc " +
                                                     "where id_empresa = @idEmpresa " +
                                                     "  and status = 'A' " +
                                                     "  and id_usr_responsable = @idUsr " +
                                                     "order by nombre ", ConexionBD)
                sdaCC.SelectCommand.Parameters.AddWithValue("@idEmpresa", .ddlEmpresa.SelectedValue)
                sdaCC.SelectCommand.Parameters.AddWithValue("@idUsr", Val(._txtIdUsuario.Text))
                .ddlCC.DataSource = dsCC
                .ddlCC.DataTextField = "nombre"
                .ddlCC.DataValueField = "id_cc"
                ConexionBD.Open()
                sdaCC.Fill(dsCC)
                .ddlCC.DataBind()
                ConexionBD.Close()
                sdaCC.Dispose()
                dsCC.Dispose()
                .ddlCC.SelectedIndex = -1
                .upCC.Update()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarAño()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaAño As New SqlDataAdapter
                Dim dsAño As New DataSet
                sdaAño.SelectCommand = New SqlCommand("select año " +
                                                      "from ms_presupuesto " +
                                                      "where id_cc = @idCC " +
                                                      "  and año >= year(getdate()) " +
                                                      "order by año ", ConexionBD)
                sdaAño.SelectCommand.Parameters.AddWithValue("@idCC", .ddlCC.SelectedValue)
                .ddlAño.DataSource = dsAño
                .ddlAño.DataTextField = "año"
                .ddlAño.DataValueField = "año"
                ConexionBD.Open()
                sdaAño.Fill(dsAño)
                .ddlAño.DataBind()
                ConexionBD.Close()
                sdaAño.Dispose()
                dsAño.Dispose()
                .upAño.Update()
                'Meses
                If .ddlAño.Items.Count > 0 Then
                    llenarMeses()

                    'Actualizar información de Presupuesto
                    infoPresup()

                    .btnGuardar.Enabled = True
                Else
                    .ddlMes.Items.Clear()
                    .upMes.Update()
                    .wceMontoAct.Text = ""
                    .upMontoAct.Update()

                    .btnGuardar.Enabled = False
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarMeses()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaMes As New SqlDataAdapter
                Dim dsMes As New DataSet
                sdaMes.SelectCommand = New SqlCommand("select mes " +
                                                      "from (select case when cast(cast(@año as varchar(4)) + '-01-31' as date) >= cast(getdate() as date) then 1 end as mes " +
                                                      "      union " +
                                                      "      select case when dateadd(day, -1, cast(cast(@año as varchar(4)) + '-03-01' as date)) >= cast(getdate() as date) then 2 end as mes " +
                                                      "      union " +
                                                      "      select case when cast(cast(@año as varchar(4)) + '-03-31' as date) >= cast(getdate() as date) then 3 end as mes " +
                                                      "      union " +
                                                      "      select case when cast(cast(@año as varchar(4)) + '-04-30' as date) >= cast(getdate() as date) then 4 end as mes " +
                                                      "      union " +
                                                      "      select case when cast(cast(@año as varchar(4)) + '-05-31' as date) >= cast(getdate() as date) then 5 end as mes " +
                                                      "      union " +
                                                      "      select case when cast(cast(@año as varchar(4)) + '-06-30' as date) >= cast(getdate() as date) then 6 end as mes " +
                                                      "      union " +
                                                      "      select case when cast(cast(@año as varchar(4)) + '-07-31' as date) >= cast(getdate() as date) then 7 end as mes " +
                                                      "      union " +
                                                      "      select case when cast(cast(@año as varchar(4)) + '-08-31' as date) >= cast(getdate() as date) then 8 end as mes " +
                                                      "      union " +
                                                      "      select case when cast(cast(@año as varchar(4)) + '-09-30' as date) >= cast(getdate() as date) then 9 end as mes " +
                                                      "      union " +
                                                      "      select case when cast(cast(@año as varchar(4)) + '-10-31' as date) >= cast(getdate() as date) then 10 end as mes " +
                                                      "      union " +
                                                      "      select case when cast(cast(@año as varchar(4)) + '-11-30' as date) >= cast(getdate() as date) then 11 end as mes " +
                                                      "      union " +
                                                      "      select case when cast(cast(@año as varchar(4)) + '-12-31' as date) >= cast(getdate() as date) then 12 end as mes) as meses " +
                                                      "where mes is not null ", ConexionBD)
                sdaMes.SelectCommand.Parameters.AddWithValue("@año", .ddlAño.SelectedValue)
                .ddlMes.DataSource = dsMes
                .ddlMes.DataTextField = "mes"
                .ddlMes.DataValueField = "mes"
                ConexionBD.Open()
                sdaMes.Fill(dsMes)
                .ddlMes.DataBind()
                ConexionBD.Close()
                sdaMes.Dispose()
                dsMes.Dispose()
                .ddlMes.SelectedIndex = -1
                .upMes.Update()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub infoPresup()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaPresup As New SqlDataAdapter
                Dim dsPresup As New DataSet
                Dim mes As String
                If .ddlMes.SelectedValue < 10 Then
                    mes = "0" + .ddlMes.SelectedValue.ToString
                Else
                    mes = .ddlMes.SelectedValue.ToString
                End If

                sdaPresup.SelectCommand = New SqlCommand("select mes_" + mes + "_p + mes_" + mes + "_a as monto_actual " +
                                                         "from ms_presupuesto " +
                                                         "where id_cc = @id_cc " +
                                                         "  and año >= @año ", ConexionBD)
                sdaPresup.SelectCommand.Parameters.AddWithValue("@id_cc", Val(.ddlCC.SelectedValue))
                sdaPresup.SelectCommand.Parameters.AddWithValue("@año", .ddlAño.SelectedValue)
                ConexionBD.Open()
                sdaPresup.Fill(dsPresup)
                ConexionBD.Close()

                .wceMontoAct.Value = dsPresup.Tables(0).Rows(0).Item("monto_actual").ToString()
                .upMontoAct.Update()

                sdaPresup.Dispose()
                dsPresup.Dispose()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub actualizarDtAmpliacion()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .gvDtAmpliacion.DataSource = dsCatalogo
                'Habilitar columna para actualización
                .gvDtAmpliacion.Columns(0).Visible = True
                'Catálogo de Unidades agregados
                sdaCatalogo.SelectCommand = New SqlCommand("select id_dt_ampliacion_p " +
                                                           "     , año " +
                                                           "     , mes " +
                                                           "     , monto_actual " +
                                                           "     , monto_solicita " +
                                                           "     , monto_nuevo " +
                                                           "     , impacto_pres_monto " +
                                                           "     , impacto_pres_porcent " +
                                                           "from dt_ampliacion_p " +
                                                           "where id_ms_ampliacion_p = -1 " +
                                                           "  and id_usuario = @idUsuario ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvDtAmpliacion.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvDtAmpliacion.SelectedIndex = -1
                'Inhabilitar columna para vista
                .gvDtAmpliacion.Columns(0).Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub actualizarAdjuntos()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaArchivos As New SqlDataAdapter
                Dim dsArchivos As New DataSet
                .gvAdjuntos.DataSource = dsArchivos
                'Adjuntos
                sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " +
                                                           "     , 'http://148.223.153.43/ProcAd - Adjuntos APGV/' + cast(id_dt_archivo_pgv as varchar(20)) + '-' + nombre as path " +
                                                           "from dt_archivo_pgv " +
                                                           "where id_ms_ampliacion_p = -1 " +
                                                           "  and id_usuario = @idUsuario ", ConexionBD)
                sdaArchivos.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                sdaArchivos.Fill(dsArchivos)
                .gvAdjuntos.DataBind()
                ConexionBD.Close()
                sdaArchivos.Dispose()
                dsArchivos.Dispose()
                .gvAdjuntos.SelectedIndex = -1
                .upAdjuntos.Update()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Private Sub DeleteFile(ByVal strFileName As String)
        If strFileName.Trim().Length > 0 Then
            Dim fi As New FileInfo(strFileName)
            If (fi.Exists) Then    'Si existe el archivo, eliminarlo
                fi.Delete()
            End If
        End If
    End Sub

#End Region

#Region "General"

    Protected Sub ddlEmpresa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEmpresa.SelectedIndexChanged
        'Lista de Centros de Costo
        llenarCC()
    End Sub

    Protected Sub ddlCC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCC.SelectedIndexChanged
        'Lista de Años
        llenarAño()
    End Sub

    Protected Sub ddlAño_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAño.SelectedIndexChanged
        llenarMeses()
        'Actualizar información de Presupuesto
        infoPresup()
    End Sub

    Protected Sub ddlMes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMes.SelectedIndexChanged
        'Actualizar información de Presupuesto
        infoPresup()
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        With Me
            Try
                .litError.Text = ""
                If .wceMontoSol.Text = "" Then
                    .litError.Text = "Favor de indicar la cantidad"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    Dim contReg As Integer
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "select count(*) " +
                                             "from dt_ampliacion_p " +
                                             "where id_ms_ampliacion_p = -1 " +
                                             "  and id_usuario = @id_usuario " +
                                             "  and año = @año " +
                                             "  and mes = @mes "
                    SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                    SCMValores.Parameters.AddWithValue("@año", .ddlAño.SelectedValue)
                    SCMValores.Parameters.AddWithValue("@mes", .ddlMes.SelectedValue)
                    ConexionBD.Open()
                    contReg = SCMValores.ExecuteScalar()
                    ConexionBD.Close()

                    If contReg <> 0 Then
                        .litError.Text = "Mes Inválido, ya se incluyó un registro con ese mes, favor de validarlo"
                    Else
                        'Insertar datos de monto en dt_ampliacion_p
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into dt_ampliacion_p (id_ms_ampliacion_p,  id_usuario,  año,  mes,  monto_actual,  monto_solicita,  monto_nuevo,  impacto_pres_monto,  impacto_pres_porcent) " +
                                                 "                     values (                -1, @id_usuario, @año, @mes, @monto_actual, @monto_solicita, @monto_nuevo, @impacto_pres_monto, @impacto_pres_porcent)"
                        SCMValores.Parameters.AddWithValue("@id_usuario", Val(Me._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@año", .ddlAño.SelectedValue)
                        SCMValores.Parameters.AddWithValue("@mes", .ddlMes.SelectedValue)
                        SCMValores.Parameters.AddWithValue("@monto_actual", .wceMontoAct.Value)
                        SCMValores.Parameters.AddWithValue("@monto_solicita", .wceMontoSol.Value)
                        SCMValores.Parameters.AddWithValue("@impacto_pres_monto", .wceMontoSol.Value)
                        '' SCMValores.Parameters.AddWithValue("@monto_solicita_val", .wceMontoSol.Value)
                        If .wceMontoAct.Value > 0 Then
                            SCMValores.Parameters.AddWithValue("@monto_nuevo", .wceMontoAct.Value + .wceMontoSol.Value)
                            SCMValores.Parameters.AddWithValue("@impacto_pres_porcent", .wceMontoSol.Value / .wceMontoAct.Value)
                        Else
                            SCMValores.Parameters.AddWithValue("@monto_nuevo", .wceMontoSol.Value)
                            SCMValores.Parameters.AddWithValue("@impacto_pres_porcent", DBNull.Value)
                        End If

                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        .wceMontoSol.Text = ""
                        infoPresup()

                        actualizarDtAmpliacion()
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub gvDtAmpliacion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDtAmpliacion.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                'Eliminar la Unidad
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD

                'Registrar la unidad en la base de datos
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "delete from dt_ampliacion_p where id_dt_ampliacion_p = @id_dt_ampliacion_p"
                SCMValores.Parameters.AddWithValue("@id_dt_ampliacion_p", .gvDtAmpliacion.SelectedRow.Cells(0).Text)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                actualizarDtAmpliacion()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnAgregarAdj_Click(sender As Object, e As EventArgs) Handles btnAgregarAdj.Click
        With Me
            Try
                .litError.Text = ""

                ' '' Ruta Local
                ''Dim sFileDir As String = "C:/ProcAd - Adjuntos APGV/" 'Ruta en que se almacenará el archivo
                ' Ruta en Atenea
                Dim sFileDir As String = "D:\ProcAd - Adjuntos APGV\" 'Ruta en que se almacenará el archivo
                Dim lMaxFileSize As Long = 10485760 'Equivalente a 10 Megabytes

                'Verificar que el archivo ha sido seleccionado y es un archivo válido
                If (Not fuAdjunto.PostedFile Is Nothing) And (fuAdjunto.PostedFile.ContentLength > 0) Then
                    'Determinar el nombre original del archivo
                    Dim sFileName As String = System.IO.Path.GetFileName(fuAdjunto.PostedFile.FileName)
                    Dim idArchivo As Integer 'Index correspondiente al archivo
                    Dim fecha As DateTime = Date.Now
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    Try
                        If fuAdjunto.PostedFile.ContentLength <= lMaxFileSize Then
                            'Registrar el archivo en la base de datos
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "INSERT INTO dt_archivo_pgv(id_ms_ampliacion_p, id_usuario, nombre, fecha) values(-1, @id_usuario, @nombre, @fecha)"
                            SCMValores.Parameters.AddWithValue("@id_usuario", Val(Me._txtIdUsuario.Text))
                            SCMValores.Parameters.AddWithValue("@nombre", sFileName)
                            SCMValores.Parameters.AddWithValue("@fecha", fecha)
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()
                            'Obtener el Id del archivo
                            SCMValores.CommandText = "select max(id_dt_archivo_pgv) from dt_archivo_pgv where (id_ms_ampliacion_p = -1) and (fecha = @fecha)"
                            ConexionBD.Open()
                            idArchivo = SCMValores.ExecuteScalar
                            ConexionBD.Close()
                            'Se agrega el Id al nombre del archivo
                            sFileName = idArchivo.ToString + "-" + sFileName
                            'Almacenar el archivo en la ruta especificada
                            fuAdjunto.PostedFile.SaveAs(sFileDir + sFileName)
                            'lblMessage.Visible = True
                            'lblMessage.Text = "El archivo: " + sFileName + " se ha adjuntado exitosamente"

                            actualizarAdjuntos()
                        Else
                            'Rechazar el archivo
                            lblMessage.Visible = True
                            lblMessage.Text = "El archivo excede el límite de 10 MB"
                        End If
                    Catch exc As Exception    'En caso de error
                        'Eliminar el archivo en la base de datos
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "delete from dt_archivo_pgv where id_dt_archivoPGV = @idArchivo"
                        SCMValores.Parameters.AddWithValue("@idArchivo", idArchivo)
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        lblMessage.Visible = True
                        lblMessage.Text = lblMessage.Text + ". Un Error ha ocurrido. Favor de intentarlo nuevamente"
                        DeleteFile(sFileDir + sFileName)
                    End Try
                Else
                    lblMessage.Visible = True
                    lblMessage.Text = "Favor de seleccionar un Archivo"
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Solicitar"

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        With Me
            Try
                .litError.Text = ""

                If .txtJustMotivo.Text.Trim = "" Or .txtJustBeneficio.Text.Trim = "" Or .txtJustImpacto.Text.Trim = "" Then
                    .litError.Text = "Información Insuficiente, favor de complementar los campos de texto"
                Else
                    If .gvDtAmpliacion.Rows.Count = 0 Then
                        .litError.Text = "Favor de indicar al menos un monto para la ampliación"
                    Else
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim sdaInfo As New SqlDataAdapter
                        Dim dsInfo As New DataSet
                        sdaInfo.SelectCommand = New SqlCommand("select id_ms_presupuesto " +
                                                               "     , id_usr_director " +
                                                               "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as director " +
                                                               "     , empresa " +
                                                               "     , centro_costo " +
                                                               "     , correo " +
                                                               "from ms_presupuesto " +
                                                               "  left join bd_Empleado.dbo.cg_cc on ms_presupuesto.id_cc = cg_cc.id_cc " +
                                                               "  left join cg_usuario on cg_cc.id_usr_director = cg_usuario.id_usuario " +
                                                               "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " +
                                                               "where ms_presupuesto.id_cc = @id_cc " +
                                                               "  and año >= @año ", ConexionBD)
                        sdaInfo.SelectCommand.Parameters.AddWithValue("@id_cc", Val(.ddlCC.SelectedValue))
                        sdaInfo.SelectCommand.Parameters.AddWithValue("@año", .ddlAño.SelectedValue)
                        ConexionBD.Open()
                        sdaInfo.Fill(dsInfo)
                        ConexionBD.Close()

                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        Dim fecha As DateTime = Date.Now
                        'Insertar Información de la Solicitud
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_ampliacion_p ( id_ms_presupuesto,  id_usr_solicita,  fecha_solicita,  solicita,  id_usr_autoriza,  autorizador,  empresa,  centro_costo,  just_motivo,  just_beneficio,  just_impacto,  status) " +
                                                 "                     values (@id_ms_presupuesto, @id_usr_solicita, @fecha_solicita, @solicita, @id_usr_autoriza, @autorizador, @empresa, @centro_costo, @just_motivo, @just_beneficio, @just_impacto,     'VP') "
                        SCMValores.Parameters.AddWithValue("@id_ms_presupuesto", Val(dsInfo.Tables(0).Rows(0).Item("id_ms_presupuesto").ToString()))
                        SCMValores.Parameters.AddWithValue("@id_usr_solicita", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@fecha_solicita", fecha)
                        SCMValores.Parameters.AddWithValue("@solicita", .lblSolicitante.Text)
                        SCMValores.Parameters.AddWithValue("@id_usr_autoriza", Val(dsInfo.Tables(0).Rows(0).Item("id_usr_director").ToString()))
                        SCMValores.Parameters.AddWithValue("@autorizador", dsInfo.Tables(0).Rows(0).Item("director").ToString())
                        SCMValores.Parameters.AddWithValue("@empresa", dsInfo.Tables(0).Rows(0).Item("empresa").ToString())
                        SCMValores.Parameters.AddWithValue("@centro_costo", dsInfo.Tables(0).Rows(0).Item("centro_costo").ToString())
                        SCMValores.Parameters.AddWithValue("@just_motivo", .txtJustMotivo.Text.Trim)
                        SCMValores.Parameters.AddWithValue("@just_beneficio", .txtJustBeneficio.Text.Trim)
                        SCMValores.Parameters.AddWithValue("@just_impacto", .txtJustImpacto.Text.Trim)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        ._txtCorreoAut.Text = dsInfo.Tables(0).Rows(0).Item("correo").ToString()

                        sdaInfo.Dispose()
                        dsInfo.Dispose()

                        'Obtener ID de la Solicitud
                        SCMValores.CommandText = "select max(id_ms_ampliacion_p) " +
                                                 "from ms_ampliacion_p " +
                                                 "where id_ms_presupuesto = @id_ms_presupuesto " +
                                                 "  and fecha_solicita = @fecha_solicita "
                        ConexionBD.Open()
                        .lblFolio.Text = SCMValores.ExecuteScalar
                        ConexionBD.Close()

                        'Actualizar Detalles
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update dt_ampliacion_p set id_ms_ampliacion_p = @id_ms_ampliacion_p where id_ms_ampliacion_p = -1 and id_usuario = @id_usuario"
                        SCMValores.Parameters.AddWithValue("@id_ms_ampliacion_p", Val(.lblFolio.Text))
                        SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Actualizar registro de Adjuntos / Evidencias no almacenados
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update dt_archivo_pgv set id_ms_ampliacion_p = @id_ms_ampliacion_p where id_ms_ampliacion_p = -1 and id_usuario = @id_usuario"
                        SCMValores.Parameters.AddWithValue("@id_ms_ampliacion_p", Val(.lblFolio.Text))
                        SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Insertar Instancia de Solicitud de Ampliación
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_instancia ( id_ms_sol,  tipo,  id_actividad) " +
                                                 "				    values (@id_ms_sol, @tipo, @id_actividad) "
                        SCMValores.Parameters.AddWithValue("@id_ms_sol", Val(.lblFolio.Text))
                        SCMValores.Parameters.AddWithValue("@tipo", "SAP")
                        SCMValores.Parameters.AddWithValue("@id_actividad", 117)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Obtener ID de la Instancia de Solicitud 
                        SCMValores.CommandText = "select max(id_ms_instancia) from ms_instancia where id_ms_sol = @id_ms_sol and tipo = 'SAP' "
                        ConexionBD.Open()
                        ._txtIdMsInst.Text = SCMValores.ExecuteScalar
                        ConexionBD.Close()

                        'Insertar Históricos
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_historico ( id_ms_instancia,  id_actividad,  id_usr,  fecha) " +
                                                 "				    values (@id_ms_instancia, @id_actividad, @id_usr, @fecha) "
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 117)
                        SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@fecha", fecha)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        ' Consulta el correo del validador de presupuesto

                        Dim destinatario As String = ""
                        SCMValores.CommandText = ""
                        SCMValores.CommandText = " select empl.correo from cg_usuario us " +
                                                 " left join bd_Empleado.dbo.cg_empleado empl " +
                                                 " on us.id_empleado = empl.id_empleado " +
                                                 " where us.perfil = 'ValPresup' and us.status = 'A' "
                        ConexionBD.Open()
                        destinatario = SCMValores.ExecuteScalar
                        ConexionBD.Close()

                        'Sumar montos solicitados
                        Dim total As Double = 0
                        For Each row As GridViewRow In .gvDtAmpliacion.Rows
                            total = total + Convert.ToDouble(row.Cells(4).Text.Replace("$", ""))
                        Next
                        .wceMontoSol.Text = ""
                        .wceMontoSol.Text = total.ToString()

                        'Envío de correo al validador 
                        Dim cabeceraM As String, cuerpoM As String

                        cabeceraM = "ProcAd - Solicitud de Ampliación de Presupuesto No. " + .lblFolio.Text + " por Validar"
                        cuerpoM = "Se generó la solicitud número <b>" + .lblFolio.Text + "</b> 
                                   por parte de <b>" + .lblSolicitante.Text + "</b> 
                                   por <b>" + wceMontoSol.Text + "</b>
                                   <br>
                                   <br>           
                                   Favor de validar si procede la Solicitud de Ampliación de Presupuesto"
                        clsCorreo.enviarCorreo(cabeceraM, cuerpoM, destinatario)
                        .pnlInicio.Enabled = False
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class