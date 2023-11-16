Public Class CatGrupo
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
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        'Actualizar Integrantes pendientes
                        SCMValores.CommandText = "update dt_grupo set status = 'B' where id_grupo = 0 and status = 'A' and id_usr_registro = @id_usr_registro "
                        SCMValores.Parameters.AddWithValue("@id_usr_registro", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

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
            .txtGrupo.Enabled = valor
            .gvIntegrantes.Enabled = valor
            .ibtnAltaInt.Enabled = valor
            .ibtnBajaInt.Enabled = valor
            .ibtnModifInt.Enabled = valor
        End With
    End Sub

    Public Sub bloqueoPantalla()
        With Me
            .pnlGrid.Visible = False
            .pnlDatos.Visible = True
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
                .gvGrupo.Columns(0).Visible = True
                .gvGrupo.DataSource = dsCatalogo
                'Catálogo de Grupos
                Dim query As String = ""
                query = "select id_grupo " + _
                        "     , grupo " + _
                        "from cg_grupo " + _
                        "where (" + .ddlCampo.SelectedValue.ToString + " like '%' + @valor +'%') " + _
                        "  and cg_grupo.status = 'A' " + _
                        "  and id_usr_lider = @id_usr_lider " + _
                        "order by grupo "
                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@valor", .txtValor.Text.Trim)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_usr_lider", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvGrupo.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvGrupo.Columns(0).Visible = False
                .gvGrupo.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarEmpleados()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .ddlUsuarioInt.DataSource = dsCatalogo
                'Catálogo de Integrantes
                sdaCatalogo.SelectCommand = New SqlCommand("select id_usuario " + _
                                                           "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as empleado " + _
                                                           "from cg_usuario " + _
                                                           "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                           "where cg_usuario.status = 'A' " + _
                                                           "  and cgEmpl.status = 'A' " + _
                                                           "  and cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno like '%' + @nombreEmplInt + '%' " +
                                                           "order by empleado ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@nombreEmplInt", .txtUsuarioIntB.Text)
                .ddlUsuarioInt.DataTextField = "empleado"
                .ddlUsuarioInt.DataValueField = "id_usuario"
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .ddlUsuarioInt.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .ddlUsuarioInt.SelectedIndex = -1
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
                sdaCatalogo.SelectCommand = New SqlCommand("select grupo " + _
                                                           "     , id_usr_secretario " + _
                                                           "     , min_part " + _
                                                           "from cg_grupo " + _
                                                           "where id_grupo = @id_grupo ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_grupo", idRegistro)
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                ConexionBD.Close()
                .txtGrupo.Text = dsCatalogo.Tables(0).Rows(0).Item("grupo").ToString()
                ._txtIdUsrSecre.Text = dsCatalogo.Tables(0).Rows(0).Item("id_usr_secretario").ToString()
                .wneMinPart.Value = Val(dsCatalogo.Tables(0).Rows(0).Item("min_part").ToString())
                limpiarPantallaInt()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                bloqueoPantalla()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarIntegrantes()
        With Me
            Try
                'Llenar Grid
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaIntegrantes As New SqlDataAdapter
                Dim dsIntegrantes As New DataSet
                .gvIntegrantes.DataSource = dsIntegrantes
                .gvIntegrantes.Columns(0).Visible = True
                sdaIntegrantes.SelectCommand = New SqlCommand("select id_dt_grupo " + _
                                                              "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as integrante " + _
                                                              "     , case when id_usr_part = @id_usr_secretario then 'Sí' else null end as secretario " + _
                                                              "from dt_grupo " + _
                                                              "  left join cg_usuario on dt_grupo.id_usr_part = cg_usuario.id_usuario " + _
                                                              "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                              "where dt_grupo.id_grupo = @id_grupo " + _
                                                              "  and id_usr_registro = @id_usr_registro " + _
                                                              "  and dt_grupo.status = 'A' " + _
                                                              "order by integrante ", ConexionBD)
                If ._txtTipoMov.Text = "A" Then
                    sdaIntegrantes.SelectCommand.Parameters.AddWithValue("@id_grupo", 0)
                Else
                    sdaIntegrantes.SelectCommand.Parameters.AddWithValue("@id_grupo", .gvGrupo.SelectedRow.Cells(0).Text)
                End If
                sdaIntegrantes.SelectCommand.Parameters.AddWithValue("id_usr_registro", Val(._txtIdUsuario.Text))
                sdaIntegrantes.SelectCommand.Parameters.AddWithValue("@id_usr_secretario", Val(._txtIdUsrSecre.Text))
                ConexionBD.Open()
                sdaIntegrantes.Fill(dsIntegrantes)
                .gvIntegrantes.DataBind()
                ConexionBD.Close()
                sdaIntegrantes.Dispose()
                dsIntegrantes.Dispose()
                .gvIntegrantes.Columns(0).Visible = False
                .gvIntegrantes.SelectedIndex = -1

                Dim contSec As Integer
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select count(*) " + _
                                         "from dt_grupo " + _
                                         "where id_grupo = @id_grupo " + _
                                         "  and id_usr_registro = @id_usr_registro " + _
                                         "  and dt_grupo.status = 'A' " + _
                                         "  and id_usr_part = @id_usr_secretario "
                If ._txtTipoMov.Text = "A" Then
                    SCMValores.Parameters.AddWithValue("@id_grupo", 0)
                Else
                    SCMValores.Parameters.AddWithValue("@id_grupo", .gvGrupo.SelectedRow.Cells(0).Text)
                End If
                SCMValores.Parameters.AddWithValue("id_usr_registro", Val(._txtIdUsuario.Text))
                SCMValores.Parameters.AddWithValue("@id_usr_secretario", Val(._txtIdUsrSecre.Text))
                ConexionBD.Open()
                contSec = SCMValores.ExecuteScalar
                ConexionBD.Close()

                If contSec = 0 Then
                    ._txtIdUsrSecre.Text = 0
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#Region "Participantes"

    Public Sub ocultarBotones()
        With Me
            .pnlAutorizador.Visible = True
            .btnAceptar.Visible = False
            .btnCancelar.Visible = False
        End With
    End Sub

    Public Sub habilitarCamposAut(ByVal valor)
        With Me
            .ddlUsuarioInt.Enabled = valor
            .txtUsuarioIntB.Enabled = valor
        End With
    End Sub

    Public Sub localizarInt(ByVal idDtIntegrante)
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCodigo As New SqlDataAdapter
                Dim dsCodigo As New DataSet
                sdaCodigo.SelectCommand = New SqlCommand("select id_usr_part, cg_usuario.status " + _
                                                         "from dt_grupo " + _
                                                         "  left join cg_usuario on dt_grupo.id_usr_part = cg_usuario.id_usuario " + _
                                                         "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                         "where id_dt_grupo = @id_dt_grupo ", ConexionBD)
                sdaCodigo.SelectCommand.Parameters.AddWithValue("@id_dt_grupo", idDtIntegrante)
                ConexionBD.Open()
                sdaCodigo.Fill(dsCodigo)
                ConexionBD.Close()
                'Llenar Campos
                If dsCodigo.Tables(0).Rows(0).Item("status").ToString() = "A" Then
                    'Integrante Activo
                    .txtUsuarioIntB.Text = ""
                    .ddlUsuarioInt.SelectedValue = Val(dsCodigo.Tables(0).Rows(0).Item("id_usr_part").ToString())
                    If Val(._txtIdUsrSecre.Text) = .ddlUsuarioInt.SelectedValue Then
                        .cbSecretario.Checked = True
                    Else
                        .cbSecretario.Checked = False
                    End If
                Else
                    'Integrante dado de Baja - Eliminarlo y actualizar tabla
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update dt_grupo set status = 'B', fecha_baja = getdate() where id_dt_grupo = @id_dt_grupo "
                    SCMValores.Parameters.AddWithValue("@id_dt_grupo", idDtIntegrante)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()
                    limpiarPantallaInt()
                End If
                sdaCodigo.Dispose()
                dsCodigo.Dispose()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub limpiarPantallaInt()
        With Me
            'Actualizar Lista de Autorizadores
            llenarIntegrantes()
            .pnlDetalle.Enabled = True
            .ibtnAltaInt.Enabled = True
            .ibtnBajaInt.Enabled = False
            .ibtnBajaInt.ImageUrl = "images\Trash_i2.png"
            .ibtnModifInt.Enabled = False
            .ibtnModifInt.ImageUrl = "images\Edit_i2.png"
            .pnlAutorizador.Visible = False
            .btnAceptar.Visible = True
            .btnCancelar.Visible = True
        End With
    End Sub

    Public Function validarInt()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMTemp As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMTemp.Connection = ConexionBD
                Dim conteo As Integer = 0
                Dim query As String
                SCMTemp.CommandText = ""
                SCMTemp.Parameters.Clear()
                Select Case ._txtTipoMovA.Text 'Tipo de Ajuste (alta, baja o modificación)
                    Case "A"
                        query = "select count(*) " + _
                                "from dt_grupo " + _
                                "where id_grupo = @id_grupo " + _
                                "  and id_usr_part = @id_usr_part " + _
                                "  and id_usr_registro = @id_usr_registro " + _
                                "  and status = 'A' "
                        SCMTemp.CommandText = query
                    Case Else
                        query = "select count(*) " + _
                                "from dt_grupo " + _
                                "where id_grupo = @id_grupo " + _
                                "  and id_usr_part = @id_usr_part " + _
                                "  and id_dt_grupo <> @id_dt_grupo " + _
                                "  and id_usr_registro = @id_usr_registro " + _
                                "  and status = 'A' "
                        SCMTemp.CommandText = query
                        SCMTemp.Parameters.AddWithValue("@id_dt_grupo", Val(.gvIntegrantes.SelectedRow.Cells(0).Text))
                End Select
                If ._txtTipoMov.Text = "A" Then
                    SCMTemp.Parameters.AddWithValue("@id_grupo", 0)
                Else
                    SCMTemp.Parameters.AddWithValue("@id_grupo", .gvGrupo.SelectedRow.Cells(0).Text)
                End If
                SCMTemp.Parameters.AddWithValue("@id_usr_part", .ddlUsuarioInt.SelectedValue)
                SCMTemp.Parameters.AddWithValue("@id_usr_registro", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                conteo = SCMTemp.ExecuteScalar
                ConexionBD.Close()
                If conteo > 0 Then
                    validarInt = False
                Else
                    validarInt = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
                validarInt = False
            End Try
        End With
    End Function

    Protected Sub ibtnAltaInt_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAltaInt.Click
        With Me
            Try
                ._txtTipoMovA.Text = "A"
                .ddlUsuarioInt.SelectedIndex = -1
                .txtUsuarioIntB.Text = ""
                llenarEmpleados()
                .cbSecretario.Checked = False
                habilitarCamposAut(True)
                .pnlDetalle.Enabled = False
                ocultarBotones()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnBajaInt_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBajaInt.Click
        With Me
            Try
                If .gvIntegrantes.SelectedIndex > -1 Then
                    ._txtTipoMovA.Text = "B"
                    .txtUsuarioIntB.Text = ""
                    llenarEmpleados()
                    localizarInt(.gvIntegrantes.SelectedRow.Cells(0).Text)
                    habilitarCamposAut(False)
                    .pnlDetalle.Enabled = False
                    ocultarBotones()
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnModifInt_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnModifInt.Click
        With Me
            Try
                If .gvIntegrantes.SelectedIndex > -1 Then
                    ._txtTipoMovA.Text = "M"
                    .txtUsuarioIntB.Text = ""
                    llenarEmpleados()
                    localizarInt(.gvIntegrantes.SelectedRow.Cells(0).Text)
                    habilitarCamposAut(True)
                    .pnlDetalle.Enabled = False
                    ocultarBotones()
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub gvIntegrantes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvIntegrantes.SelectedIndexChanged
        With Me
            .litError.Text = ""
            .ibtnBajaInt.Enabled = True
            .ibtnBajaInt.ImageUrl = "images\Trash.png"
            .ibtnModifInt.Enabled = True
            .ibtnModifInt.ImageUrl = "images\Edit.png"
        End With
    End Sub

    Protected Sub cmdBuscarUsrInt_Click(sender As Object, e As EventArgs) Handles cmdBuscarUsrInt.Click
        llenarEmpleados()
    End Sub

    Protected Sub btnAceptarInt_Click(sender As Object, e As EventArgs) Handles btnAceptarInt.Click
        With Me
            Try
                .litError.Text = ""
                Dim ban As Integer = 0
                If .ddlUsuarioInt.Items.Count = 0 Then
                    .litError.Text = "Información Insuficiente, favor de elegir un autorizador"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    Select Case ._txtTipoMovA.Text 'Tipo de Ajuste (Agregar, Eliminar o Modificar)
                        Case "A"
                            If validarInt() Then
                                SCMValores.CommandText = "insert into dt_grupo (id_grupo,  id_usr_part,  id_usr_registro, fecha_registro) values (@id_grupo, @id_usr_part, @id_usr_registro, getdate())"
                                If ._txtTipoMov.Text = "A" Then
                                    SCMValores.Parameters.AddWithValue("@id_grupo", 0)
                                Else
                                    SCMValores.Parameters.AddWithValue("@id_grupo", .gvGrupo.SelectedRow.Cells(0).Text)
                                End If
                                SCMValores.Parameters.AddWithValue("@id_usr_part", .ddlUsuarioInt.SelectedValue)
                                SCMValores.Parameters.AddWithValue("@id_usr_registro", Val(._txtIdUsuario.Text))
                            Else
                                .litError.Text = "Valor Inválido, ya se asignó ese Integrante "
                                ban = 1
                            End If
                        Case "B"
                            SCMValores.CommandText = "update dt_grupo set status = 'B', fecha_baja = getdate() where id_dt_grupo = @id_dt_grupo "
                            SCMValores.Parameters.AddWithValue("@id_dt_grupo", .gvIntegrantes.SelectedRow.Cells(0).Text)
                        Case Else
                            If validarInt() Then
                                SCMValores.CommandText = "update dt_grupo SET id_usr_part = @id_usr_part, fecha_registro = getdate() WHERE id_dt_grupo = @id_dt_grupo"
                                SCMValores.Parameters.AddWithValue("@id_usr_part", .ddlUsuarioInt.SelectedValue)
                                SCMValores.Parameters.AddWithValue("@id_dt_grupo", .gvIntegrantes.SelectedRow.Cells(0).Text)
                            Else
                                .litError.Text = "Valor Inválido, ya se asignó ese Integrante "
                                ban = 1
                            End If
                    End Select
                    If ban = 0 Then
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        If .cbSecretario.Checked = True Then
                            If ._txtTipoMov.Text = "M" And ._txtIdUsrSecre.Text <> .ddlUsuarioInt.SelectedValue Then
                                Dim SCMValoresS As SqlCommand = New System.Data.SqlClient.SqlCommand
                                SCMValoresS.Connection = ConexionBD
                                SCMValoresS.CommandText = ""
                                SCMValoresS.Parameters.Clear()
                                SCMValoresS.CommandText = "update cg_grupo set id_usr_secretario = @id_usr_secretario where id_grupo = @id_grupo "
                                SCMValoresS.Parameters.AddWithValue("@id_grupo", .gvGrupo.SelectedRow.Cells(0).Text)
                                SCMValoresS.Parameters.AddWithValue("@id_usr_secretario", .ddlUsuarioInt.SelectedValue)
                                ConexionBD.Open()
                                SCMValoresS.ExecuteNonQuery()
                                ConexionBD.Close()
                            End If
                            ._txtIdUsrSecre.Text = .ddlUsuarioInt.SelectedValue
                        End If

                        limpiarPantallaInt()
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnCancelarInt_Click(sender As Object, e As EventArgs) Handles btnCancelarInt.Click
        limpiarPantallaInt()
    End Sub

#End Region

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
                        SCMTemp.CommandText = "SELECT COUNT(*) FROM cg_grupo WHERE grupo = @grupo AND id_usr_lider = @id_usr_lider AND status = 'A'"
                    Case Else
                        SCMTemp.CommandText = "SELECT COUNT(*) FROM cg_grupo WHERE grupo = @grupo AND id_usr_lider = @id_usr_lider AND id_grupo <> @id_grupo AND status = 'A'"
                        SCMTemp.Parameters.AddWithValue("@id_grupo", .gvGrupo.SelectedRow.Cells(0).Text)
                End Select
                SCMTemp.Parameters.AddWithValue("@grupo", .txtGrupo.Text)
                SCMTemp.Parameters.AddWithValue("@id_usr_lider", Val(._txtIdUsuario.Text))
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

    Protected Sub gvGrupo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvGrupo.SelectedIndexChanged
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
                .txtGrupo.Text = ""
                .wneMinPart.Value = 0
                ._txtIdUsrSecre.Text = 0
                llenarIntegrantes()

                '.ibtnAltaInt.Visible = False
                '.ibtnBajaInt.Visible = False
                '.ibtnModifInt.Visible = False
                .pnlAutorizador.Visible = False
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
                localizar(.gvGrupo.SelectedRow.Cells(0).Text)
                .ibtnAltaInt.Visible = True
                .ibtnBajaInt.Visible = True
                .ibtnModifInt.Visible = True
                .pnlAutorizador.Visible = False
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
                localizar(.gvGrupo.SelectedRow.Cells(0).Text)
                .ibtnAltaInt.Visible = True
                .ibtnBajaInt.Visible = True
                .ibtnModifInt.Visible = True
                .pnlAutorizador.Visible = False
                habilitarCampos(True)
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Botones Datos"

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""
                Dim ban As Integer = 0
                If .txtGrupo.Text.Trim = "" Or .gvIntegrantes.Rows.Count = 0 Or ._txtIdUsrSecre.Text = 0 Then
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
                                SCMValores.CommandText = "INSERT INTO cg_grupo (grupo, id_usr_lider, id_usr_secretario, fecha_registro, min_part) values(@grupo, @id_usr_lider, @id_usr_secretario, getdate(), @min_part) "
                            Else
                                .litError.Text = "Valor Inválido, ya existe ese Grupo"
                                ban = 1
                            End If
                        Case "B"
                            SCMValores.CommandText = "UPDATE cg_grupo SET status = 'B', fecha_baja = getdate() WHERE id_grupo = @id_grupo"
                            SCMValores.Parameters.AddWithValue("@id_grupo", .gvGrupo.SelectedRow.Cells(0).Text)
                        Case Else
                            If validar() Then
                                SCMValores.CommandText = "UPDATE cg_grupo SET grupo = @grupo, id_usr_secretario = @id_usr_secretario, fecha_ult_modif = getdate(), min_part = @min_part WHERE id_grupo = @id_grupo"
                                SCMValores.Parameters.AddWithValue("@id_grupo", .gvGrupo.SelectedRow.Cells(0).Text)
                            Else
                                .litError.Text = "Valor Inválido, ya existe ese Grupo"
                                ban = 1
                            End If
                    End Select
                    If ban = 0 Then
                        SCMValores.Parameters.AddWithValue("@grupo", .txtGrupo.Text)
                        SCMValores.Parameters.AddWithValue("@min_part", .wneMinPart.Value)
                        SCMValores.Parameters.AddWithValue("@id_usr_lider", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@id_usr_secretario", Val(._txtIdUsrSecre.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        If ._txtTipoMov.Text = "A" Then
                            'Obtener el id_grupo
                            Dim idGrupo As Integer
                            SCMValores.CommandText = "select isnull(max(id_grupo), 0) from cg_grupo WHERE id_usr_lider = @id_usr_lider and status = 'A' "
                            ConexionBD.Open()
                            idGrupo = SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            'Actualizar la lista de Integrantes
                            SCMValores.CommandText = "update dt_grupo set id_grupo = @id_grupo where id_grupo = 0 and status = 'A' and id_usr_registro = @id_usr_registro "
                            SCMValores.Parameters.AddWithValue("@id_grupo", idGrupo)
                            SCMValores.Parameters.AddWithValue("@id_usr_registro", Val(._txtIdUsuario.Text))
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()
                        End If

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