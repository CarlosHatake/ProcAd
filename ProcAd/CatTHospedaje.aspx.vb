Public Class CatTHospedaje
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        limpiarPantalla()
                    Else
                        Server.Transfer("Default.aspx")
                    End If
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
            llenarGrid()
            .gvTipoHospedaje.Enabled = True
            .ibtnAlta.Enabled = True
            .ibtnBaja.Enabled = False
            .ibtnBaja.ImageUrl = "images\Trash_i2.png"
            .ibtnModif.Enabled = False
            .ibtnModif.ImageUrl = "images\Edit_i2.png"
            .pnlInicio.Visible = True
            .pnlDatos.Visible = False
        End With
    End Sub

    Public Sub habilitarCampos(ByVal valor As Boolean)
        With Me
            .txtNombre.Enabled = valor
        End With
    End Sub

    Public Sub bloqueoPantalla()
        With Me
            .pnlInicio.Visible = False
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
                .gvTipoHospedaje.Columns(0).Visible = True
                .gvTipoHospedaje.DataSource = dsCatalogo
                'Catálogo de Categorías
                sdaCatalogo.SelectCommand = New SqlCommand("select id_tipo_hospedaje " +
                                                           "     , tipo_hospedaje " +
                                                           "from cg_tipo_hospedaje " +
                                                           "where status = 'A' " +
                                                           "  and (" + .ddlCampo.SelectedValue.ToString + " like '%' + @valor +'%') " +
                                                           "order by tipo_hospedaje", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@valor", .txtValor.Text.Trim)
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvTipoHospedaje.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvTipoHospedaje.Columns(0).Visible = False
                .gvTipoHospedaje.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub localizar(ByVal idTipoHospedaje)
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                sdaCatalogo.SelectCommand = New SqlCommand("select tipo_hospedaje " +
                                                           "from cg_tipo_hospedaje " +
                                                           "where id_tipo_hospedaje = @id_tipo_hospedaje ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_tipo_hospedaje", idTipoHospedaje)
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                ConexionBD.Close()
                .txtNombre.Text = dsCatalogo.Tables(0).Rows(0).Item("tipo_hospedaje").ToString()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                bloqueoPantalla()
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
                        SCMTemp.CommandText = "SELECT COUNT(*) FROM cg_tipo_hospedaje WHERE tipo_hospedaje = @tipo_hospedaje"  '' AND status = 'A'
                    Case Else
                        SCMTemp.CommandText = "SELECT COUNT(*) FROM cg_tipo_hospedaje WHERE tipo_hospedaje = @tipo_hospedaje AND id_tipo_hospedaje <> @id_tipo_hospedaje"  '' AND status = 'A'
                        SCMTemp.Parameters.AddWithValue("@id_tipo_hospedaje", .gvTipoHospedaje.SelectedRow.Cells(0).Text)
                End Select
                SCMTemp.Parameters.AddWithValue("@tipo_hospedaje", .txtNombre.Text)
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

    Protected Sub gvTipoHospedaje_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvTipoHospedaje.SelectedIndexChanged
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

#Region "Botones - Inicio"

    Protected Sub ibtnBuscar_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBuscar.Click
        llenarGrid()
    End Sub

    Protected Sub ibtnAlta_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAlta.Click
        With Me
            Try
                ._txtTipoMov.Text = "A"
                .lblTipoMov.Text = "Alta"
                bloqueoPantalla()
                .txtNombre.Text = ""
                habilitarCampos(True)
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnBaja_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBaja.Click
        With Me
            Try
                If Val(.gvTipoHospedaje.SelectedRow.Cells(0).Text) = 1 Then
                    .litError.Text = "No se puede eliminar ese elemento, favor de validarlo"
                Else
                    ._txtTipoMov.Text = "B"
                    .lblTipoMov.Text = "Baja"
                    localizar(.gvTipoHospedaje.SelectedRow.Cells(0).Text)
                    habilitarCampos(False)
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnModif_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnModif.Click
        With Me
            Try
                If Val(.gvTipoHospedaje.SelectedRow.Cells(0).Text) = 1 Then
                    .litError.Text = "No se puede modificar ese elemento, favor de validarlo"
                Else
                    ._txtTipoMov.Text = "M"
                    .lblTipoMov.Text = "Modificación"
                    localizar(.gvTipoHospedaje.SelectedRow.Cells(0).Text)
                    habilitarCampos(True)
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Botones - Datos"

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""
                Dim ban As Integer = 0
                If .txtNombre.Text.Trim = "" Then
                    .litError.Text = "Información Insuficiente, favor de verificar"
                Else
                    .txtNombre.Text = EliminarAcentos(.txtNombre.Text)
                    .txtNombre.Text = .txtNombre.Text.ToUpper

                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    Select Case ._txtTipoMov.Text 'Tipo de Ajuste (alta, baja o modificación)
                        Case "A"
                            If validar() Then
                                SCMValores.CommandText = "INSERT INTO cg_tipo_hospedaje(tipo_hospedaje) values(@tipo_hospedaje)"
                            Else
                                .litError.Text = "Valor Inválido, ya existe ese Tipo de Hospedaje"
                                ban = 1
                            End If
                        Case "B"
                            SCMValores.CommandText = "UPDATE cg_tipo_hospedaje SET status = 'B' WHERE id_tipo_hospedaje = @id_tipo_hospedaje"
                            SCMValores.Parameters.AddWithValue("@id_tipo_hospedaje", .gvTipoHospedaje.SelectedRow.Cells(0).Text)
                        Case Else
                            If validar() Then
                                SCMValores.CommandText = "UPDATE cg_tipo_hospedaje SET tipo_hospedaje = @tipo_hospedaje WHERE id_tipo_hospedaje = @id_tipo_hospedaje"
                                SCMValores.Parameters.AddWithValue("@id_tipo_hospedaje", .gvTipoHospedaje.SelectedRow.Cells(0).Text)
                            Else
                                .litError.Text = "Valor Inválido, ya existe ese Tipo de Hospedaje"
                                ban = 1
                            End If
                    End Select
                    If ban = 0 Then
                        SCMValores.Parameters.AddWithValue("@tipo_hospedaje", .txtNombre.Text)
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