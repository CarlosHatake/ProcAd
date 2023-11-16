Public Class PorcentBono
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
            .wpeMin.Enabled = valor
            .wpeMax.Enabled = valor
            .wpeBono.Enabled = valor
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
                .gvPorcentBono.Columns(0).Visible = True
                .gvPorcentBono.DataSource = dsCatalogo
                'Catálogo de Usuarios
                Dim query As String = ""
                query = "select id_dt_porcent_bono " +
                        "     , porcent_min " +
                        "     , porcent_max " +
                        "     , porcent_bono " +
                        "from dt_porcent_bono " +
                        "where status = 'A' " +
                        "order by porcent_min, porcent_max "
                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvPorcentBono.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvPorcentBono.Columns(0).Visible = False
                .gvPorcentBono.SelectedIndex = -1
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
                sdaCatalogo.SelectCommand = New SqlCommand("select porcent_min " +
                                                           "     , porcent_max " +
                                                           "     , porcent_bono " +
                                                           "from dt_porcent_bono " +
                                                           "where id_dt_porcent_bono = @id_dt_porcent_bono ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_dt_porcent_bono", idRegistro)
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                ConexionBD.Close()
                .wpeMin.Value = Val(dsCatalogo.Tables(0).Rows(0).Item("porcent_min").ToString())
                .wpeMax.Value = Val(dsCatalogo.Tables(0).Rows(0).Item("porcent_max").ToString())
                .wpeBono.Value = Val(dsCatalogo.Tables(0).Rows(0).Item("porcent_bono").ToString())
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
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
                        SCMTemp.CommandText = "select count(*) " +
                                              "from dt_porcent_bono " +
                                              "where dt_porcent_bono.status not in ('B') " +
                                              "and ((porcent_min <= @min and porcent_max > @min) " +
                                              "  or (porcent_min < @max and porcent_max >= @max) " +
                                              "  or (porcent_min <= @min and porcent_max >= @max) " +
                                              "  or (porcent_min > @min and porcent_max < @max)) "
                    Case Else
                        SCMTemp.CommandText = "select count(*) " +
                                              "from dt_porcent_bono " +
                                              "where dt_porcent_bono.status not in ('B') " +
                                              "and id_dt_porcent_bono <> @id_dt_porcent_bono " +
                                              "and ((porcent_min <= @min and porcent_max > @min) " +
                                              "  or (porcent_min < @max and porcent_max >= @max) " +
                                              "  or (porcent_min <= @min and porcent_max >= @max) " +
                                              "  or (porcent_min > @min and porcent_max < @max)) "
                        SCMTemp.Parameters.AddWithValue("@id_dt_porcent_bono", .gvPorcentBono.SelectedRow.Cells(0).Text)
                End Select
                SCMTemp.Parameters.AddWithValue("@min", .wpeMin.Value)
                SCMTemp.Parameters.AddWithValue("@max", .wpeMax.Value)
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

    Protected Sub gvUsuario_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvPorcentBono.SelectedIndexChanged
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
                .wpeMin.Text = ""
                .wpeMax.Text = ""
                .wpeBono.Text = ""
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
                localizar(.gvPorcentBono.SelectedRow.Cells(0).Text)
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
                localizar(.gvPorcentBono.SelectedRow.Cells(0).Text)
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
                If .wpeMin.Text.Trim = "" Or .wpeMax.Text.Trim = "" Or .wpeBono.Text.Trim = "" Then
                    .litError.Text = "Información Insuficiente, favor de verificar"
                Else
                    If .wpeMax.Value < .wpeMin.Value Then
                        .litError.Text = "Rango incorrecto, favor de validarlo"
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
                                    SCMValores.CommandText = "INSERT INTO dt_porcent_bono (porcent_min, porcent_max, porcent_bono, status) values(@porcent_min, @porcent_max, @porcent_bono, 'A')"
                                Else
                                    .litError.Text = "Rango Inválido, favor de verificar"
                                    ban = 1
                                End If
                            Case "B"
                                SCMValores.CommandText = "UPDATE dt_porcent_bono SET status = 'B' WHERE id_dt_porcent_bono = @id_dt_porcent_bono"
                                SCMValores.Parameters.AddWithValue("@id_dt_porcent_bono", .gvPorcentBono.SelectedRow.Cells(0).Text)
                            Case Else
                                If validar() Then
                                    SCMValores.CommandText = "UPDATE dt_porcent_bono SET porcent_min = @porcent_min, porcent_max = @porcent_max, porcent_bono = @porcent_bono WHERE id_dt_porcent_bono = @id_dt_porcent_bono"
                                    SCMValores.Parameters.AddWithValue("@id_dt_porcent_bono", .gvPorcentBono.SelectedRow.Cells(0).Text)
                                Else
                                    .litError.Text = "Rango Inválido, favor de verificar"
                                    ban = 1
                                End If
                        End Select
                        If ban = 0 Then
                            SCMValores.Parameters.AddWithValue("@porcent_min", .wpeMin.Value)
                            SCMValores.Parameters.AddWithValue("@porcent_max", .wpeMax.Value)
                            SCMValores.Parameters.AddWithValue("@porcent_bono", .wpeBono.Value)
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            limpiarPantalla()
                        End If
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