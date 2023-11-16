Public Class CatArea
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
                        sdaCatalogo.SelectCommand = New SqlCommand("select distinct(cg_empresa.id_empresa) " + _
                                                                   "     , nombre as empresa " + _
                                                                   "from bd_Empleado.dbo.cg_empresa " + _
                                                                   "  inner join cg_direccion on cg_empresa.id_empresa = cg_direccion.id_empresa " + _
                                                                   "where cg_empresa.status = 'A' " + _
                                                                   "  and cg_direccion.status = 'A' " + _
                                                                   "order by empresa ", ConexionBD)
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
            .ddlDirecccion.Enabled = valor
            .txtArea.Enabled = valor
            .txtAutorizador.Enabled = valor
            .ibtnBuscarAut.Enabled = valor
            .ddlAutorizador.Enabled = valor
        End With
    End Sub

    Public Sub bloqueoPantalla()
        With Me
            .pnlGrid.Visible = False
            .pnlDatos.Visible = True
        End With
    End Sub

    Public Sub llenarDirecciones()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .ddlDirecccion.DataSource = dsCatalogo
                'Catálogo de Autorizadores
                sdaCatalogo.SelectCommand = New SqlCommand("select distinct(cg_direccion.id_direccion) " + _
                                                           "     , cg_direccion.direccion " + _
                                                           "from bd_Empleado.dbo.cg_empresa " + _
                                                           "  inner join cg_direccion on cg_empresa.id_empresa = cg_direccion.id_empresa " + _
                                                           "where cg_empresa.status = 'A' " + _
                                                           "  and cg_direccion.status = 'A' " + _
                                                           "  and cg_direccion.id_empresa = @id_empresa " + _
                                                           "order by direccion ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_empresa", .ddlEmpresa.SelectedValue)
                .ddlDirecccion.DataTextField = "direccion"
                .ddlDirecccion.DataValueField = "id_direccion"
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .ddlDirecccion.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .ddlDirecccion.SelectedIndex = -1

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarAutorizadores()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBDNom As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBDNom.ConnectionString = accessDB.conBD("NOM")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .ddlAutorizador.DataSource = dsCatalogo
                'Catálogo de Autorizadores
                sdaCatalogo.SelectCommand = New SqlCommand("select rtrim(ltrim(isnull(nomtrab.cvetra,''))) as no_empleado " + _
                                                           "     , rtrim(ltrim(isnull(nomtrab.nombre,''))) + ' ' + rtrim(ltrim(isnull(nomtrab.apepat,''))) + ' ' + rtrim(ltrim(isnull(nomtrab.apemat,''))) as empleado " + _
                                                           "from nomtrab " + _
                                                           "where nomtrab.status = 'A' " + _
                                                           "  and (nomtrab.cvetra like '%' + @valor + '%' " + _
                                                           "    or rtrim(ltrim(isnull(nomtrab.nombre,''))) + ' ' + rtrim(ltrim(isnull(nomtrab.apepat,''))) + ' ' + rtrim(ltrim(isnull(nomtrab.apemat,''))) like '%' + @valor + '%') " + _
                                                           "order by empleado ", ConexionBDNom)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@valor", .txtAutorizador.Text)
                .ddlAutorizador.DataTextField = "empleado"
                .ddlAutorizador.DataValueField = "no_empleado"
                ConexionBDNom.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .ddlAutorizador.DataBind()
                ConexionBDNom.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .ddlAutorizador.SelectedIndex = -1

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
                SCMValores.Parameters.AddWithValue("@no_empleado", .ddlAutorizador.SelectedValue)
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
                    sdaCatalogo.SelectCommand.Parameters.AddWithValue("@no_empleado", .ddlAutorizador.SelectedValue)
                    ConexionBD.Open()
                    sdaCatalogo.Fill(dsCatalogo)
                    ConexionBD.Close()
                    .lblAutorizadorUsr.Text = dsCatalogo.Tables(0).Rows(0).Item("nick").ToString()
                    .lblAutorizadorID.Text = dsCatalogo.Tables(0).Rows(0).Item("id_usuario").ToString()
                    sdaCatalogo.Dispose()
                    dsCatalogo.Dispose()
                Else
                    .lblAutorizadorUsr.Text = ""
                    .lblAutorizadorID.Text = ""
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
                .gvArea.Columns(0).Visible = True
                .gvArea.DataSource = dsCatalogo
                'Catálogo de Áreas
                Dim query As String = ""
                query = "select id_dt_area " + _
                        "     , cg_empresa.nombre as empresa " + _
                        "     , direccion " + _
                        "     , area " + _
                        "     , nombre_aut as autorizador " + _
                        "from dt_area " + _
                        "  left join cg_direccion on dt_area.id_direccion = cg_direccion.id_direccion " + _
                        "  left join bd_Empleado.dbo.cg_empresa on cg_direccion.id_empresa = cg_empresa.id_empresa " + _
                        "where cg_direccion.status = 'A' " + _
                        "  and dt_area.status = 'A' " + _
                        "  and (" + .ddlCampo.SelectedValue.ToString + " like '%' + @valor +'%') " + _
                        "order by empresa, direccion, area "
                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@valor", .txtValor.Text.Trim)
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvArea.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvArea.Columns(0).Visible = False
                .gvArea.SelectedIndex = -1
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
                sdaCatalogo.SelectCommand = New SqlCommand("select dt_area.id_direccion " + _
                                                           "     , id_empresa " + _
                                                           "     , area " + _
                                                           "     , no_empleado_aut " + _
                                                           "from dt_area " + _
                                                           "  left join cg_direccion on dt_area.id_direccion = cg_direccion.id_direccion " + _
                                                           "where id_dt_area = @id_dt_area ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_dt_area", idRegistro)
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                ConexionBD.Close()
                .ddlEmpresa.SelectedValue = Val(dsCatalogo.Tables(0).Rows(0).Item("id_empresa").ToString())
                llenarDirecciones()
                .ddlDirecccion.SelectedValue = Val(dsCatalogo.Tables(0).Rows(0).Item("id_direccion").ToString())
                .txtArea.Text = dsCatalogo.Tables(0).Rows(0).Item("area").ToString()
                .txtAutorizador.Text = ""
                llenarAutorizadores()
                .ddlAutorizador.SelectedValue = dsCatalogo.Tables(0).Rows(0).Item("no_empleado_aut").ToString()
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
                        SCMTemp.CommandText = "SELECT COUNT(*) FROM dt_area WHERE id_direccion = @id_direccion and area = @area AND status = 'A'"
                    Case Else
                        SCMTemp.CommandText = "SELECT COUNT(*) FROM dt_area WHERE id_direccion = @id_direccion and area = @area AND id_dt_area <> @id_dt_area AND status = 'A'"
                        SCMTemp.Parameters.AddWithValue("@id_dt_area", .gvArea.SelectedRow.Cells(0).Text)
                End Select
                SCMTemp.Parameters.AddWithValue("@id_direccion", .ddlDirecccion.SelectedValue)
                SCMTemp.Parameters.AddWithValue("@area", .txtArea.Text)
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

    Protected Sub gvArea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvArea.SelectedIndexChanged
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
                llenarDirecciones()
                .txtArea.Text = ""
                .txtAutorizador.Text = ""
                llenarAutorizadores()
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
                localizar(.gvArea.SelectedRow.Cells(0).Text)
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
                localizar(.gvArea.SelectedRow.Cells(0).Text)
                habilitarCampos(True)
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Botones Datos"

    Protected Sub ddlEmpresa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEmpresa.SelectedIndexChanged
        llenarDirecciones()
    End Sub

    Protected Sub ibtnBuscarAut_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBuscarAut.Click
        llenarAutorizadores()
    End Sub

    Protected Sub ddlAutorizador_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAutorizador.SelectedIndexChanged
        usuarioProcAd()
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""
                Dim ban As Integer = 0
                If .txtArea.Text.Trim = "" Or .ddlDirecccion.Items.Count = 0 Or .ddlAutorizador.Items.Count = 0 Then
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
                                SCMValores.CommandText = "INSERT INTO dt_area (id_direccion, area, no_empleado_aut, nombre_aut, id_usr_aut) values(@id_direccion, @area, @no_empleado_aut, @nombre_aut, @id_usr_aut)"
                            Else
                                .litError.Text = "Valor Inválido, ya existe ese registro"
                                ban = 1
                            End If
                        Case "B"
                            SCMValores.CommandText = "UPDATE dt_area SET status = 'B' WHERE id_dt_area = @id_dt_area"
                            SCMValores.Parameters.AddWithValue("@id_dt_area", .gvArea.SelectedRow.Cells(0).Text)
                        Case Else
                            If validar() Then
                                SCMValores.CommandText = "UPDATE dt_area SET id_direccion = @id_direccion, area = @area, no_empleado_aut = @no_empleado_aut, nombre_aut = @nombre_aut, id_usr_aut = @id_usr_aut WHERE id_dt_area = @id_dt_area"
                                SCMValores.Parameters.AddWithValue("@id_dt_area", .gvArea.SelectedRow.Cells(0).Text)
                            Else
                                .litError.Text = "Valor Inválido, ya existe ese registro"
                                ban = 1
                            End If
                    End Select
                    If ban = 0 Then
                        SCMValores.Parameters.AddWithValue("@id_direccion", .ddlDirecccion.SelectedValue)
                        SCMValores.Parameters.AddWithValue("@area", .txtArea.Text.Trim)
                        SCMValores.Parameters.AddWithValue("@no_empleado_aut", .ddlAutorizador.SelectedValue)
                        SCMValores.Parameters.AddWithValue("@nombre_aut", .ddlAutorizador.SelectedItem.Text)
                        If .lblAutorizadorUsr.Text = "" Then
                            SCMValores.Parameters.AddWithValue("@id_usr_aut", DBNull.Value)
                        Else
                            SCMValores.Parameters.AddWithValue("@id_usr_aut", Val(.lblAutorizadorID.Text))
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