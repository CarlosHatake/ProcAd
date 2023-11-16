Public Class CatDireccion
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 1

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim sdaCatalogo As New SqlDataAdapter
                        Dim dsCatalogo As New DataSet
                        .ddlEmpresa.DataSource = dsCatalogo
                        'Catálogo de Empresas
                        sdaCatalogo.SelectCommand = New SqlCommand("select id_empresa " + _
                                                                   "     , nombre as empresa " + _
                                                                   "from bd_Empleado.dbo.cg_empresa " + _
                                                                   "where status = 'A' " + _
                                                                   "order by empresa ", ConexionBD)
                        sdaCatalogo.SelectCommand.Parameters.AddWithValue("@nombreEmplDir", .txtDirector.Text)
                        .ddlEmpresa.DataTextField = "empresa"
                        .ddlEmpresa.DataValueField = "id_empresa"
                        ConexionBD.Open()
                        sdaCatalogo.Fill(dsCatalogo)
                        .ddlEmpresa.DataBind()
                        ConexionBD.Close()
                        sdaCatalogo.Dispose()
                        dsCatalogo.Dispose()
                        .ddlEmpresa.SelectedIndex = -1

                        limpiarPantalla()
                    Else
                        Server.Transfer("Login.aspx")
                    End If
                Catch ex As Exception
                    .litError.Text = ex.ToString
                End Try
            End With
        End If
    End Sub

    Protected Sub ibtnBuscar_Click(sender As Object, e As EventArgs) Handles ibtnBuscar.Click
        llenarGrid()
    End Sub

#End Region

#Region "Funciones"

    Public Sub limpiarPantalla()
        With Me
            llenarGrid()
            .pnlGrid.Enabled = True
            .ibtnAlta.Enabled = True
            .ibtnBaja.Enabled = False
            .ibtnBaja.ImageUrl = "images\Trash_i2.png"
            .ibtnModif.Enabled = False
            .ibtnModif.ImageUrl = "images\Edit_i2.png"
            .pnlGrid.Visible = True
            .pnlDatos.Visible = False
        End With
    End Sub

    Public Sub habilitarCampos(ByVal valor As Boolean)
        With Me
            .ddlEmpresa.Enabled = valor
            .txtDireccion.Enabled = valor
            .txtDirector.Enabled = valor
            .ibtnBuscarDir.Enabled = valor
            .ddlDirector.Enabled = valor
        End With
    End Sub

    Public Sub bloqueoPantalla()
        With Me
            .pnlGrid.Visible = False
            .pnlDatos.Visible = True
        End With
    End Sub

    Public Sub llenarDirectores()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBDNom As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBDNom.ConnectionString = accessDB.conBD("NOM")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .ddlDirector.DataSource = dsCatalogo
                'Catálogo de Autorizadores
                sdaCatalogo.SelectCommand = New SqlCommand("select rtrim(ltrim(isnull(nomtrab.cvetra,''))) as no_empleado " + _
                                                            "     , rtrim(ltrim(isnull(nomtrab.nombre,''))) + ' ' + rtrim(ltrim(isnull(nomtrab.apepat,''))) + ' ' + rtrim(ltrim(isnull(nomtrab.apemat,''))) as empleado " + _
                                                            "from nomtrab " + _
                                                            "where nomtrab.status = 'A' " + _
                                                            "  and (nomtrab.cvetra like '%' + @valor + '%' " + _
                                                            "    or rtrim(ltrim(isnull(nomtrab.nombre,''))) + ' ' + rtrim(ltrim(isnull(nomtrab.apepat,''))) + ' ' + rtrim(ltrim(isnull(nomtrab.apemat,''))) like '%' + @valor + '%') " + _
                                                            "order by empleado ", ConexionBDNom)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@valor", .txtDirector.Text)
                .ddlDirector.DataTextField = "empleado"
                .ddlDirector.DataValueField = "no_empleado"
                ConexionBDNom.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .ddlDirector.DataBind()
                ConexionBDNom.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .ddlDirector.SelectedIndex = -1

                usuarioProcAd()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub usuarioProcAd()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim conteo As Integer = 0
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select count(*) " + _
                                         "from cg_usuario " + _
                                         "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                                         "where case len(no_empleado) when 4 then '000' + no_empleado when 5 then '00' + no_empleado else no_empleado end = @no_empleado "
                SCMValores.Parameters.AddWithValue("@no_empleado", .ddlDirector.SelectedValue)
                ConexionBD.Open()
                conteo = SCMValores.ExecuteScalar
                ConexionBD.Close()
                If conteo > 0 Then
                    Dim sdaCatalogo As New SqlDataAdapter
                    Dim dsCatalogo As New DataSet
                    sdaCatalogo.SelectCommand = New SqlCommand("select id_usuario, nick " + _
                                                               "from cg_usuario " + _
                                                               "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                                                               "where case len(no_empleado) when 4 then '000' + no_empleado when 5 then '00' + no_empleado else no_empleado end = @no_empleado ", ConexionBD)
                    sdaCatalogo.SelectCommand.Parameters.AddWithValue("@no_empleado", .ddlDirector.SelectedValue)
                    ConexionBD.Open()
                    sdaCatalogo.Fill(dsCatalogo)
                    ConexionBD.Close()
                    .lblDirectorUsr.Text = dsCatalogo.Tables(0).Rows(0).Item("nick").ToString()
                    .lblDirectorID.Text = dsCatalogo.Tables(0).Rows(0).Item("id_usuario").ToString()
                    sdaCatalogo.Dispose()
                    dsCatalogo.Dispose()
                Else
                    .lblDirectorUsr.Text = ""
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarGrid()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .gvDireccion.Columns(0).Visible = True
                .gvDireccion.DataSource = dsCatalogo
                'Catálogo de Direcciones
                Dim query As String = ""
                query = "select id_direccion " + _
                        "     , cg_empresa.nombre as empresa " + _
                        "     , direccion " + _
                        "     , nombre_dir as director " + _
                        "from cg_direccion " + _
                        "  left join bd_Empleado.dbo.cg_empresa on cg_direccion.id_empresa = cg_empresa.id_empresa " + _
                        "where cg_direccion.status = 'A' " + _
                        "  and (" + .ddlCampo.SelectedValue.ToString + " like '%' + @valor +'%') " + _
                        "order by direccion "
                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@valor", .txtValor.Text.Trim)
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvDireccion.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvDireccion.Columns(0).Visible = False
                .gvDireccion.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub localizar(ByVal idRegistro)
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                sdaCatalogo.SelectCommand = New SqlCommand("select id_empresa, direccion, no_empleado_dir " + _
                                                           "from cg_direccion " + _
                                                           "where id_direccion = @id_direccion ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_direccion", idRegistro)
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                ConexionBD.Close()
                .ddlEmpresa.SelectedValue = Val(dsCatalogo.Tables(0).Rows(0).Item("id_empresa").ToString())
                .txtDireccion.Text = dsCatalogo.Tables(0).Rows(0).Item("direccion").ToString()
                .txtDirector.Text = ""
                llenarDirectores()
                .ddlDirector.SelectedValue = dsCatalogo.Tables(0).Rows(0).Item("no_empleado_dir").ToString()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                usuarioProcAd()

                bloqueoPantalla()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Function validar()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMTemp As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMTemp.Connection = ConexionBD
                Dim conteo As Integer = 0
                SCMTemp.CommandText = ""
                SCMTemp.Parameters.Clear()
                Select Case ._txtTipoMov.Text 'Tipo de Ajuste (alta, baja o modificación)
                    Case "A"
                        SCMTemp.CommandText = "SELECT COUNT(*) FROM cg_direccion WHERE id_empresa = @id_empresa and direccion = @direccion AND status = 'A'"
                    Case Else
                        SCMTemp.CommandText = "SELECT COUNT(*) FROM cg_direccion WHERE id_empresa = @id_empresa and direccion = @direccion AND id_direccion <> @id_direccion AND status = 'A'"
                        SCMTemp.Parameters.AddWithValue("@id_direccion", .gvDireccion.SelectedRow.Cells(0).Text)
                End Select
                SCMTemp.Parameters.AddWithValue("@id_empresa", .ddlEmpresa.SelectedValue)
                SCMTemp.Parameters.AddWithValue("@direccion", .txtDireccion.Text)
                ConexionBD.Open()
                conteo = SCMTemp.ExecuteScalar
                ConexionBD.Close()
                If conteo > 0 Then
                    validar = False
                Else
                    validar = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
                validar = False
            End Try
        End With
    End Function

#End Region

#Region "Selección"

    Protected Sub gvDireccion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDireccion.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                .ibtnBaja.Enabled = True
                .ibtnBaja.ImageUrl = "images\Trash.png"
                .ibtnModif.Enabled = True
                .ibtnModif.ImageUrl = "images\Edit.png"
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Botones Inicio"

    Protected Sub ibtnAlta_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAlta.Click
        With Me
            Try
                ._txtTipoMov.Text = "A"
                .lblTipoMov.Text = "Alta"
                bloqueoPantalla()
                .ddlEmpresa.SelectedValue = 9
                .txtDireccion.Text = ""
                .txtDirector.Text = ""
                llenarDirectores()
                habilitarCampos(True)
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnBaja_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBaja.Click
        With Me
            Try
                ._txtTipoMov.Text = "B"
                .lblTipoMov.Text = "Baja"
                localizar(.gvDireccion.SelectedRow.Cells(0).Text)
                habilitarCampos(False)
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnModif_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnModif.Click
        With Me
            Try
                ._txtTipoMov.Text = "M"
                .lblTipoMov.Text = "Modificación"
                localizar(.gvDireccion.SelectedRow.Cells(0).Text)
                habilitarCampos(True)
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Botones Datos"

    Protected Sub ibtnBuscarDir_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBuscarDir.Click
        llenarDirectores()
    End Sub

    Protected Sub ddlDirector_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDirector.SelectedIndexChanged
        usuarioProcAd()
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""
                Dim ban As Integer = 0
                If .txtDireccion.Text.Trim = "" Or .ddlDirector.Items.Count = 0 Then
                    .litError.Text = "Información Insuficiente, favor de verificar"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    Select Case ._txtTipoMov.Text 'Tipo de Ajuste (alta, baja o modificación)
                        Case "A"
                            If validar() Then
                                SCMValores.CommandText = "INSERT INTO cg_direccion (id_empresa, direccion, no_empleado_dir, nombre_dir, id_usr_dir) values(@id_empresa, @direccion, @no_empleado_dir, @nombre_dir, @id_usr_dir)"
                            Else
                                .litError.Text = "Valor Inválido, ya existe ese registro"
                                ban = 1
                            End If
                        Case "B"
                            SCMValores.CommandText = "UPDATE cg_direccion SET status = 'B' WHERE id_direccion = @id_direccion"
                            SCMValores.Parameters.AddWithValue("@id_direccion", .gvDireccion.SelectedRow.Cells(0).Text)

                            'Dar de Baja las Áreas correspondientes
                            Dim SCMTemp As SqlCommand = New System.Data.SqlClient.SqlCommand
                            SCMTemp.Connection = ConexionBD
                            Dim conteo As Integer = 0
                            SCMTemp.CommandText = ""
                            SCMTemp.Parameters.Clear()
                            SCMTemp.CommandText = "UPDATE dt_area set status = 'B' WHERE id_direccion = @id_direccion AND status = 'A'"
                            SCMTemp.Parameters.AddWithValue("@id_direccion", .gvDireccion.SelectedRow.Cells(0).Text)
                            ConexionBD.Open()
                            SCMTemp.ExecuteNonQuery()
                            ConexionBD.Close()
                        Case Else
                            If validar() Then
                                SCMValores.CommandText = "UPDATE cg_direccion SET id_empresa = @id_empresa, direccion = @direccion, no_empleado_dir = @no_empleado_dir, nombre_dir = @nombre_dir, id_usr_dir = @id_usr_dir WHERE id_direccion = @id_direccion"
                                SCMValores.Parameters.AddWithValue("@id_direccion", .gvDireccion.SelectedRow.Cells(0).Text)
                            Else
                                .litError.Text = "Valor Inválido, ya existe ese registro"
                                ban = 1
                            End If
                    End Select
                    If ban = 0 Then
                        SCMValores.Parameters.AddWithValue("@id_empresa", .ddlEmpresa.SelectedValue)
                        SCMValores.Parameters.AddWithValue("@direccion", .txtDireccion.Text.Trim)
                        SCMValores.Parameters.AddWithValue("@no_empleado_dir", .ddlDirector.SelectedValue)
                        SCMValores.Parameters.AddWithValue("@nombre_dir", .ddlDirector.SelectedItem.Text)
                        If .lblDirectorUsr.Text = "" Then
                            SCMValores.Parameters.AddWithValue("@id_usr_dir", DBNull.Value)
                        Else
                            SCMValores.Parameters.AddWithValue("@id_usr_dir", Val(.lblDirectorID.Text))
                        End If
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        limpiarPantalla()
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        limpiarPantalla()
    End Sub

#End Region

End Class