Public Class AltReunion
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
                        'Actualizar Participantes pendientes
                        SCMValores.CommandText = "update dt_reunion set status = 'B' where id_ms_reunion = 0 and status = 'P' and id_usr_registro = @id_usr_registro "
                        SCMValores.Parameters.AddWithValue("@id_usr_registro", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        CancelarReunionPrevia()

                        'Catálogo de Grupos
                        Dim sdaCatalogo As New SqlDataAdapter
                        Dim dsCatalogo As New DataSet
                        .ddlGrupo.DataSource = dsCatalogo
                        sdaCatalogo.SelectCommand = New SqlCommand("select id_grupo " + _
                                                                   "     , grupo " + _
                                                                   "from cg_grupo " + _
                                                                   "where status = 'A' " + _
                                                                   "  and id_usr_secretario = @id_usr_secretario " + _
                                                                   "order by grupo ", ConexionBD)
                        sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_usr_secretario", Val(._txtIdUsuario.Text))
                        .ddlGrupo.DataTextField = "grupo"
                        .ddlGrupo.DataValueField = "id_grupo"
                        ConexionBD.Open()
                        sdaCatalogo.Fill(dsCatalogo)
                        .ddlGrupo.DataBind()
                        ConexionBD.Close()
                        sdaCatalogo.Dispose()
                        dsCatalogo.Dispose()
                        .ddlGrupo.SelectedIndex = -1

                        .wdpFecha.MinValue = Date.Now

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
            .txtTema.Enabled = valor
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

    Public Sub CancelarReunionPrevia()
        With Me
            Try
                'Cancelar reuniónes pasadas de acuerdo al la fecha_reunión
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "update dt_reunion set status = 'Z' where id_ms_reunion in (select id_ms_reunion from ms_reunion where status = 'P' and dateadd(hour, 1, fecha_reunion) < getdate()) "
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()
                SCMValores.CommandText = "update ms_reunion set status = 'Z' where status = 'P' and dateadd(hour, 1, fecha_reunion) < getdate() "
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarGrid()
        With Me
            Try
                .litError.Text = ""

                CancelarReunionPrevia()

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .gvReunion.Columns(0).Visible = True
                .gvReunion.DataSource = dsCatalogo
                'Catálogo de Reuniones Vigentes
                Dim query As String = ""
                query = "select id_ms_reunion " + _
                        "     , grupo " + _
                        "     , tema " + _
                        "     , fecha_reunion " + _
                        "from ms_reunion " + _
                        "where (" + .ddlCampo.SelectedValue.ToString + " like '%' + @valor +'%') " + _
                        "  and ms_reunion.status = 'P' " + _
                        "  and id_usr_secretario = @id_usr_secretario " + _
                        "order by grupo, tema "
                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@valor", .txtValor.Text.Trim)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_usr_secretario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvReunion.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvReunion.Columns(0).Visible = False
                .gvReunion.SelectedIndex = -1
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
                sdaCatalogo.SelectCommand = New SqlCommand("select tema " + _
                                                           "     , id_grupo " + _
                                                           "from ms_reunion " + _
                                                           "where id_ms_reunion = @id_ms_reunion ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_ms_reunion", idRegistro)
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                ConexionBD.Close()
                .txtTema.Text = dsCatalogo.Tables(0).Rows(0).Item("tema").ToString()
                .ddlGrupo.SelectedValue = dsCatalogo.Tables(0).Rows(0).Item("id_grupo").ToString()
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
                sdaIntegrantes.SelectCommand = New SqlCommand("select id_dt_reunion " + _
                                                              "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as integrante " + _
                                                              "     , case when id_usr_part = @id_usr_secretario then 'Sí' else null end as secretario " + _
                                                              "from dt_reunion " + _
                                                              "  left join cg_usuario on dt_reunion.id_usr_part = cg_usuario.id_usuario " + _
                                                              "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                              "where dt_reunion.id_ms_reunion = @id_ms_reunion " + _
                                                              "  and id_usr_registro = @id_usr_registro " + _
                                                              "  and dt_reunion.status = 'P' " + _
                                                              "order by integrante ", ConexionBD)
                If ._txtTipoMov.Text = "A" Then
                    sdaIntegrantes.SelectCommand.Parameters.AddWithValue("@id_ms_reunion", 0)
                Else
                    sdaIntegrantes.SelectCommand.Parameters.AddWithValue("@id_ms_reunion", .gvReunion.SelectedRow.Cells(0).Text)
                End If
                sdaIntegrantes.SelectCommand.Parameters.AddWithValue("id_usr_registro", Val(._txtIdUsuario.Text))
                sdaIntegrantes.SelectCommand.Parameters.AddWithValue("@id_usr_secretario", Val(._txtIdUsuario.Text))
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
                                         "from dt_reunion " + _
                                         "where id_ms_reunion = @id_ms_reunion " + _
                                         "  and id_usr_registro = @id_usr_registro " + _
                                         "  and dt_reunion.status = 'P' " + _
                                         "  and id_usr_part = @id_usr_secretario "
                If ._txtTipoMov.Text = "A" Then
                    SCMValores.Parameters.AddWithValue("@id_ms_reunion", 0)
                Else
                    SCMValores.Parameters.AddWithValue("@id_ms_reunion", .gvReunion.SelectedRow.Cells(0).Text)
                End If
                SCMValores.Parameters.AddWithValue("id_usr_registro", Val(._txtIdUsuario.Text))
                SCMValores.Parameters.AddWithValue("@id_usr_secretario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                contSec = SCMValores.ExecuteScalar
                ConexionBD.Close()
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
                                                         "from dt_reunion " + _
                                                         "  left join cg_usuario on dt_reunion.id_usr_part = cg_usuario.id_usuario " + _
                                                         "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                         "where id_dt_reunion = @id_dt_reunion ", ConexionBD)
                sdaCodigo.SelectCommand.Parameters.AddWithValue("@id_dt_reunion", idDtIntegrante)
                ConexionBD.Open()
                sdaCodigo.Fill(dsCodigo)
                ConexionBD.Close()
                'Llenar Campos
                If dsCodigo.Tables(0).Rows(0).Item("status").ToString() = "A" Then
                    'Integrante Activo
                    .txtUsuarioIntB.Text = ""
                    .ddlUsuarioInt.SelectedValue = Val(dsCodigo.Tables(0).Rows(0).Item("id_usr_part").ToString())
                Else
                    'Integrante dado de Baja - Eliminarlo y actualizar tabla
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update dt_reunion set status = 'B', fecha_baja = getdate() where id_dt_reunion = @id_dt_reunion "
                    SCMValores.Parameters.AddWithValue("@id_dt_reunion", idDtIntegrante)
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
                                "from dt_reunion " + _
                                "where id_ms_reunion = @id_ms_reunion " + _
                                "  and id_usr_part = @id_usr_part " + _
                                "  and id_usr_registro = @id_usr_registro " + _
                                "  and status <> 'B' "
                        SCMTemp.CommandText = query
                    Case Else
                        query = "select count(*) " + _
                                "from dt_reunion " + _
                                "where id_ms_reunion = @id_ms_reunion " + _
                                "  and id_usr_part = @id_usr_part " + _
                                "  and id_dt_reunion <> @id_dt_reunion " + _
                                "  and id_usr_registro = @id_usr_registro " + _
                                "  and status <> 'B' "
                        SCMTemp.CommandText = query
                        SCMTemp.Parameters.AddWithValue("@id_dt_reunion", Val(.gvIntegrantes.SelectedRow.Cells(0).Text))
                End Select
                If ._txtTipoMov.Text = "A" Then
                    SCMTemp.Parameters.AddWithValue("@id_ms_reunion", 0)
                Else
                    SCMTemp.Parameters.AddWithValue("@id_ms_reunion", .gvReunion.SelectedRow.Cells(0).Text)
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
                                SCMValores.CommandText = "insert into dt_reunion (id_ms_reunion,  id_usr_part,  id_usr_registro, fecha_registro) values (@id_ms_reunion, @id_usr_part, @id_usr_registro, getdate())"
                                If ._txtTipoMov.Text = "A" Then
                                    SCMValores.Parameters.AddWithValue("@id_ms_reunion", 0)
                                Else
                                    SCMValores.Parameters.AddWithValue("@id_ms_reunion", .gvReunion.SelectedRow.Cells(0).Text)
                                End If
                                SCMValores.Parameters.AddWithValue("@id_usr_part", .ddlUsuarioInt.SelectedValue)
                                SCMValores.Parameters.AddWithValue("@id_usr_registro", Val(._txtIdUsuario.Text))
                            Else
                                .litError.Text = "Valor Inválido, ya se asignó ese Participante "
                                ban = 1
                            End If
                        Case "B"
                            SCMValores.CommandText = "update dt_reunion set status = 'B', fecha_baja = getdate() where id_dt_reunion = @id_dt_reunion "
                            SCMValores.Parameters.AddWithValue("@id_dt_reunion", .gvIntegrantes.SelectedRow.Cells(0).Text)
                        Case Else
                            If validarInt() Then
                                SCMValores.CommandText = "update dt_reunion SET id_usr_part = @id_usr_part, fecha_registro = getdate() WHERE id_dt_reunion = @id_dt_reunion"
                                SCMValores.Parameters.AddWithValue("@id_usr_part", .ddlUsuarioInt.SelectedValue)
                                SCMValores.Parameters.AddWithValue("@id_dt_reunion", .gvIntegrantes.SelectedRow.Cells(0).Text)
                            Else
                                .litError.Text = "Valor Inválido, ya se asignó ese Participante "
                                ban = 1
                            End If
                    End Select
                    If ban = 0 Then
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

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
                        SCMTemp.CommandText = "SELECT COUNT(*) FROM ms_reunion WHERE id_grupo = @id_grupo AND fecha_reunion = @fecha_reunion AND status = 'P'"
                    Case Else
                        SCMTemp.CommandText = "SELECT COUNT(*) FROM ms_reunion WHERE id_grupo = @id_grupo AND fecha_reunion = @fecha_reunion AND id_ms_reunion <> @id_ms_reunion AND status = 'P'"
                        SCMTemp.Parameters.AddWithValue("@id_ms_reunion", .gvReunion.SelectedRow.Cells(0).Text)
                End Select
                SCMTemp.Parameters.AddWithValue("@id_grupo", .ddlGrupo.SelectedValue)
                SCMTemp.Parameters.AddWithValue("@fecha_reunion", .wdpFecha.Date)
                ConexionBD.Open()
                conteo = SCMTemp.ExecuteScalar
                ConexionBD.Close()
                If conteo > 0 Then
                    validar = 1
                Else
                    Dim contSec As Integer
                    Select Case ._txtTipoMov.Text 'Tipo de Ajuste (alta, baja o modificación)
                        Case "A"
                            SCMTemp.CommandText = "SELECT COUNT(*) FROM dt_reunion WHERE id_usr_part = @id_usr_registro and id_ms_reunion = 0 AND id_usr_registro = @id_usr_registro AND status = 'P' "
                        Case Else
                            SCMTemp.CommandText = "SELECT COUNT(*) FROM dt_reunion WHERE id_usr_part = @id_usr_registro and id_ms_reunion = @id_ms_reunion AND id_usr_registro = @id_usr_registro AND status = 'P' and id_ms_reunion = @id_ms_reunion "
                            SCMTemp.Parameters.AddWithValue("@id_ms_reunion", .gvReunion.SelectedRow.Cells(0).Text)
                    End Select
                    SCMTemp.Parameters.AddWithValue("@id_usr_registro", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    contSec = SCMTemp.ExecuteScalar
                    ConexionBD.Close()
                    If contSec > 0 Then
                        validar = 0
                    Else
                        validar = 2
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
                validar = 1
            End Try
        End With
    End Function

#End Region

#Region "Selección"

    Protected Sub btnAceptarG_Click(sender As Object, e As EventArgs) Handles btnAceptarG.Click
        With Me
            Try
                .ddlGrupo.Enabled = False
                .btnAceptarG.Visible = False

                .ibtnAltaInt.Visible = True
                .ibtnBajaInt.Visible = True
                .ibtnModifInt.Visible = True

                'Insertar Participantes de acuerdo al Grupo Elegido
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "insert into dt_reunion (id_ms_reunion,  id_usr_part,  id_usr_registro, fecha_registro) " + _
                                         "select @id_ms_reunion " + _
                                         "     , id_usr_part " + _
                                         "     , @id_usr_registro " + _
                                         "     , getdate() " + _
                                         "from dt_grupo " + _
                                         "where id_grupo = @id_grupo " + _
                                         "  and status = 'A' "
                If ._txtTipoMov.Text = "A" Then
                    SCMValores.Parameters.AddWithValue("@id_ms_reunion", 0)
                Else
                    SCMValores.Parameters.AddWithValue("@id_ms_reunion", .gvReunion.SelectedRow.Cells(0).Text)
                End If
                SCMValores.Parameters.AddWithValue("@id_grupo", .ddlGrupo.SelectedValue)
                SCMValores.Parameters.AddWithValue("@id_usr_registro", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                limpiarPantallaInt()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub gvGrupo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvReunion.SelectedIndexChanged
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
                .txtTema.Text = ""

                .ddlGrupo.Enabled = True
                .btnAceptarG.Visible = True

                .ibtnAltaInt.Visible = False
                .ibtnBajaInt.Visible = False
                .ibtnModifInt.Visible = False

                llenarIntegrantes()

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
                localizar(.gvReunion.SelectedRow.Cells(0).Text)
                .ddlGrupo.Enabled = False
                .btnAceptarG.Visible = False
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
                localizar(.gvReunion.SelectedRow.Cells(0).Text)
                .ddlGrupo.Enabled = False
                .btnAceptarG.Visible = False
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
                If .txtTema.Text.Trim = "" Or .gvIntegrantes.Rows.Count = 0 Then
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
                            Dim val As Integer
                            val = validar()
                            If val = 0 Then
                                SCMValores.CommandText = "INSERT INTO ms_reunion (id_grupo, grupo, tema, id_usr_secretario, fecha_reunion, fecha_registro) values(@id_grupo, @grupo, @tema, @id_usr_secretario, @fecha_reunion, getdate()) "
                            Else
                                If val = 1 Then
                                    .litError.Text = "Información Inválida, ya existe ese Grupo"
                                Else
                                    .litError.Text = "Información Inválida, no se puede generar una reunión sin el Secretario"
                                End If
                                ban = 1
                            End If
                        Case "B"
                            SCMValores.CommandText = "UPDATE ms_reunion SET status = 'ZU', fecha_baja = getdate() WHERE id_ms_reunion = @id_ms_reunion"
                            SCMValores.Parameters.AddWithValue("@id_usuario", .gvReunion.SelectedRow.Cells(0).Text)
                        Case Else
                            Dim val As Integer
                            val = validar()
                            If val = 0 Then
                                SCMValores.CommandText = "UPDATE ms_reunion SET id_grupo = @id_grupo, grupo = @grupo, tema = @tema, id_usr_secretario = @id_usr_secretario, fecha_reunion = @fecha_reunion, fecha_ult_modif = getdate() WHERE id_ms_reunion = @id_ms_reunion"
                                SCMValores.Parameters.AddWithValue("@id_ms_reunion", .gvReunion.SelectedRow.Cells(0).Text)
                            Else
                                If val = 1 Then
                                    .litError.Text = "Información Inválida, ya existe ese Grupo"
                                Else
                                    .litError.Text = "Información Inválida, no se puede generar una reunión sin el Secretario"
                                End If
                                ban = 1
                            End If
                    End Select
                    If ban = 0 Then
                        SCMValores.Parameters.AddWithValue("@id_grupo", .ddlGrupo.SelectedValue)
                        SCMValores.Parameters.AddWithValue("@grupo", .ddlGrupo.SelectedItem.Text)
                        SCMValores.Parameters.AddWithValue("@tema", .txtTema.Text)
                        SCMValores.Parameters.AddWithValue("@id_usr_secretario", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@fecha_reunion", .wdpFecha.Date)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        If ._txtTipoMov.Text = "A" Then
                            'Obtener el id_ms_reunion
                            Dim idMsReunion As Integer
                            SCMValores.CommandText = "select isnull(max(id_ms_reunion), 0) from ms_reunion WHERE id_usr_secretario = @id_usr_secretario and status = 'P' "
                            ConexionBD.Open()
                            idMsReunion = SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            'Actualizar la lista de Integrantes
                            SCMValores.CommandText = "update dt_reunion set id_ms_reunion = @id_ms_reunion where id_ms_reunion = 0 and status = 'P' and id_usr_registro = @id_usr_registro "
                            SCMValores.Parameters.AddWithValue("@id_ms_reunion", idMsReunion)
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