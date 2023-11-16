Public Class CatConceptoCF
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim sdaCatalogo As New SqlDataAdapter
                        Dim dsCatalogo As New DataSet
                        .ddlEmpresa.DataSource = dsCatalogo
                        'Catálogo de Empresas
                        sdaCatalogo.SelectCommand = New SqlCommand("select 0 as id_empresa " +
                                                                   "     , '' as empresa " +
                                                                   "union " +
                                                                   "select distinct(cg_empresa.id_empresa) as id_empresa " +
                                                                   "     , nombre as empresa " +
                                                                   "from bd_Empleado.dbo.cg_empresa " +
                                                                   "where cg_empresa.status = 'A' " +
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
                        llenarCC()

                        Dim sdaCategoria As New SqlDataAdapter
                        Dim dsCategoria As New DataSet
                        .ddlCategoria.DataSource = dsCategoria
                        'Catálogo de Categorías
                        sdaCategoria.SelectCommand = New SqlCommand("select 0 as id_concepto_cat " +
                                                                    "     , '' as categoria " +
                                                                    "union " +
                                                                    "select id_concepto_cat " +
                                                                    "     , categoria " +
                                                                    "from cg_concepto_cat " +
                                                                    "where status = 'A' " +
                                                                    "order by categoria ", ConexionBD)
                        .ddlCategoria.DataTextField = "categoria"
                        .ddlCategoria.DataValueField = "id_concepto_cat"
                        ConexionBD.Open()
                        sdaCategoria.Fill(dsCategoria)
                        .ddlCategoria.DataBind()
                        ConexionBD.Close()
                        sdaCategoria.Dispose()
                        dsCategoria.Dispose()
                        .ddlCategoria.SelectedIndex = -1

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
            .gvConcepto.Enabled = True
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
            .ddlCategoria.Enabled = valor
            .txtAbrev.Enabled = valor
            .txtConcepto.Enabled = valor
            .txtCuentaC.Enabled = valor
            .ddlEmpresa.Enabled = valor
            .ddlCC.Enabled = valor
            .ddlNoConceptos.Enabled = valor
            .wneIVA.Enabled = valor
            .cbCombustible.Enabled = valor
            .cbValAlimentos.Enabled = valor
            .cbReqAutDir.Enabled = valor
            .ddlCant1.Enabled = valor
            .ddlClave1.Enabled = valor
            .ddlCant2.Enabled = valor
            .ddlClave2.Enabled = valor
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
                .gvConcepto.Columns(0).Visible = True
                .gvConcepto.DataSource = dsCatalogo
                'Catálogo de Conceptos Sin Factura
                sdaCatalogo.SelectCommand = New SqlCommand("select id_concepto_comp " +
                                                           "     , concepto " +
                                                           "     , abreviatura " +
                                                           "     , iva " +
                                                           "     , cuenta " +
                                                           "     , categoria " +
                                                           "     , alimentos " +
                                                           "     , no_conceptos " +
                                                           "     , cg_cc.nombre as centro_costo " +
                                                           "from cg_concepto_comp " +
                                                           "  left join cg_concepto_cat on cg_concepto_comp.id_concepto_cat = cg_concepto_cat.id_concepto_cat " +
                                                           "  left join bd_Empleado.dbo.cg_cc on cg_concepto_comp.id_cc = cg_cc.id_cc " +
                                                           "where cg_concepto_comp.status = 'A' " +
                                                           "  and (" + .ddlCampo.SelectedValue.ToString + " like '%' + @valor +'%') " +
                                                           "order by concepto, abreviatura ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@valor", .txtValor.Text.Trim)
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvConcepto.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvConcepto.Columns(0).Visible = False
                .gvConcepto.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub localizar(ByVal idConcepto)
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                sdaCatalogo.SelectCommand = New SqlCommand("select id_concepto_cat " +
                                                           "     , abreviatura " +
                                                           "     , concepto " +
                                                           "     , cuenta " +
                                                           "     , isnull(id_empresa, 0) as id_empresa " +
                                                           "     , cg_concepto_comp.id_cc " +
                                                           "     , no_conceptos " +
                                                           "     , cantidad1, cve_concepto1, cantidad2, cve_concepto2 " +
                                                           "     , isnull(iva, -1) as iva " +
                                                           "     , combustible " +
                                                           "     , alimentos " +
                                                           "     , reqFirmaD " +
                                                           "     , val_orig_destino " +
                                                           "from cg_concepto_comp " +
                                                           "  left join bd_Empleado.dbo.cg_cc on cg_concepto_comp.id_cc = cg_cc.id_cc " +
                                                           "where id_concepto_comp = @id_concepto_comp ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_concepto_comp", idConcepto)
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                ConexionBD.Close()
                .ddlCategoria.SelectedValue = Val(dsCatalogo.Tables(0).Rows(0).Item("id_concepto_cat").ToString())
                .txtAbrev.Text = dsCatalogo.Tables(0).Rows(0).Item("abreviatura").ToString()
                .txtConcepto.Text = dsCatalogo.Tables(0).Rows(0).Item("concepto").ToString()
                .txtCuentaC.Text = dsCatalogo.Tables(0).Rows(0).Item("cuenta").ToString()
                .ddlEmpresa.SelectedValue = Val(dsCatalogo.Tables(0).Rows(0).Item("id_empresa").ToString())
                llenarCC()
                .ddlCC.SelectedValue = Val(dsCatalogo.Tables(0).Rows(0).Item("id_cc").ToString())
                If Val(dsCatalogo.Tables(0).Rows(0).Item("iva").ToString()) = -1 Then
                    .wneIVA.Text = ""
                Else
                    .wneIVA.Value = Val(dsCatalogo.Tables(0).Rows(0).Item("iva").ToString())
                End If
                If dsCatalogo.Tables(0).Rows(0).Item("combustible").ToString() = "1" Then
                    .cbCombustible.Checked = True
                Else
                    .cbCombustible.Checked = False
                End If
                If dsCatalogo.Tables(0).Rows(0).Item("alimentos").ToString() = "1" Then
                    .cbValAlimentos.Checked = True
                Else
                    .cbValAlimentos.Checked = False
                End If
                If dsCatalogo.Tables(0).Rows(0).Item("reqFirmaD").ToString() = "S" Then
                    .cbReqAutDir.Checked = True
                Else
                    .cbReqAutDir.Checked = False
                End If
                .ddlNoConceptos.SelectedValue = Val(dsCatalogo.Tables(0).Rows(0).Item("no_conceptos").ToString())
                actConceptos()
                If Val(dsCatalogo.Tables(0).Rows(0).Item("no_conceptos").ToString()) >= 1 Then
                    .ddlCant1.SelectedValue = Val(dsCatalogo.Tables(0).Rows(0).Item("cantidad1").ToString())
                    .ddlClave1.SelectedValue = dsCatalogo.Tables(0).Rows(0).Item("cve_concepto1").ToString()
                End If
                If Val(dsCatalogo.Tables(0).Rows(0).Item("no_conceptos").ToString()) = 2 Then
                    .ddlCant2.SelectedValue = Val(dsCatalogo.Tables(0).Rows(0).Item("cantidad2").ToString())
                    .ddlClave2.SelectedValue = dsCatalogo.Tables(0).Rows(0).Item("cve_concepto2").ToString()
                End If

                If dsCatalogo.Tables(0).Rows(0).Item("val_orig_destino").ToString() = "S" Then
                    .cbVali_Orineg_Destino.Checked = True
                Else
                    .cbVali_Orineg_Destino.Checked = False
                End If
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                bloqueoPantalla()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarCC()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .ddlCC.DataSource = dsCatalogo
                'Catálogo de Centros de Costo
                sdaCatalogo.SelectCommand = New SqlCommand("select 0 as id_cc " +
                                                           "     , '' as centro_costo " +
                                                           "union " +
                                                           "select distinct(cg_cc.id_cc) as id_cc " +
                                                           "     , nombre as centro_costo " +
                                                           "from bd_Empleado.dbo.cg_cc " +
                                                           "where cg_cc.status = 'A' " +
                                                           "  and cg_cc.id_empresa = @id_empresa " +
                                                           "order by centro_costo ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_empresa", .ddlEmpresa.SelectedValue)
                .ddlCC.DataTextField = "centro_costo"
                .ddlCC.DataValueField = "id_cc"
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .ddlCC.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .ddlCC.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub actConceptos()
        With Me
            Select Case .ddlNoConceptos.SelectedValue
                Case 0
                    .lbl_Cant1.Visible = False
                    .ddlCant1.Visible = False
                    .lbl_Clave1.Visible = False
                    .ddlClave1.Visible = False
                    .lbl_Cant2.Visible = False
                    .ddlCant2.Visible = False
                    .lbl_Clave2.Visible = False
                    .ddlClave2.Visible = False
                Case 1
                    .lbl_Cant1.Visible = True
                    .ddlCant1.Visible = True
                    .lbl_Clave1.Visible = True
                    .ddlClave1.Visible = True
                    .lbl_Cant2.Visible = False
                    .ddlCant2.Visible = False
                    .lbl_Clave2.Visible = False
                    .ddlClave2.Visible = False
                Case 2
                    .lbl_Cant1.Visible = True
                    .ddlCant1.Visible = True
                    .lbl_Clave1.Visible = True
                    .ddlClave1.Visible = True
                    .lbl_Cant2.Visible = True
                    .ddlCant2.Visible = True
                    .lbl_Clave2.Visible = True
                    .ddlClave2.Visible = True
            End Select
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
                        SCMTemp.CommandText = "SELECT COUNT(*) FROM cg_concepto_comp WHERE concepto = @concepto AND abreviatura = @abreviatura AND iva = @iva AND id_cc = @id_cc "  '' AND status = 'A'
                    Case Else
                        SCMTemp.CommandText = "SELECT COUNT(*) FROM cg_concepto_comp WHERE concepto = @concepto AND abreviatura = @abreviatura AND iva = @iva AND id_cc = @id_cc AND id_concepto_comp <> @id_concepto_comp"  '' AND status = 'A'
                        SCMTemp.Parameters.AddWithValue("@id_concepto_comp", .gvConcepto.SelectedRow.Cells(0).Text)
                End Select
                SCMTemp.Parameters.AddWithValue("@concepto", .txtConcepto.Text)
                SCMTemp.Parameters.AddWithValue("@abreviatura", .txtAbrev.Text)
                SCMTemp.Parameters.AddWithValue("@id_cc", .ddlCC.SelectedValue)
                If .wneIVA.Text.Trim = "" Then
                    SCMTemp.Parameters.AddWithValue("@iva", DBNull.Value)
                Else
                    SCMTemp.Parameters.AddWithValue("@iva", .wneIVA.Value)
                End If
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

    Protected Sub gvCategoria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvConcepto.SelectedIndexChanged
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
                .ddlCategoria.SelectedIndex = -1
                .txtAbrev.Text = ""
                .txtConcepto.Text = ""
                .txtCuentaC.Text = ""
                .wneIVA.Text = ""
                .ddlEmpresa.SelectedIndex = -1
                llenarCC()
                .ddlCC.SelectedIndex = -1
                .ddlNoConceptos.SelectedValue = 0
                actConceptos()
                .cbCombustible.Checked = False
                .cbReqAutDir.Checked = False
                .cbValAlimentos.Checked = False
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
                localizar(.gvConcepto.SelectedRow.Cells(0).Text)
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
                localizar(.gvConcepto.SelectedRow.Cells(0).Text)
                habilitarCampos(True)
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Botones - Datos"

    Protected Sub ddlEmpresa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEmpresa.SelectedIndexChanged
        llenarCC()
    End Sub

    Protected Sub ddlNoConceptos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoConceptos.SelectedIndexChanged
        actConceptos()
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""
                Dim ban As Integer = 0
                If .txtConcepto.Text.Trim = "" Then
                    .litError.Text = "Información Insuficiente, favor de verificar"
                Else
                    .txtConcepto.Text = EliminarAcentos(.txtConcepto.Text)
                    .txtConcepto.Text = .txtConcepto.Text.ToUpper

                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    Select Case ._txtTipoMov.Text 'Tipo de Ajuste (alta, baja o modificación)
                        Case "A"
                            If validar() Then
                                SCMValores.CommandText = "INSERT INTO cg_concepto_comp( id_concepto_cat,  abreviatura,  concepto,  cuenta,  id_cc,  alimentos,  iva,  combustible,  reqFirmaD,  no_conceptos,  cve_concepto1,  cantidad1,  cve_concepto2,  cantidad2,  val_orig_destino) " +
                                                         "                      values(@id_concepto_cat, @abreviatura, @concepto, @cuenta, @id_cc, @alimentos, @iva, @combustible, @reqFirmaD, @no_conceptos, @cve_concepto1, @cantidad1, @cve_concepto2, @cantidad2, @val_orig_destino)"
                            Else
                                .litError.Text = "Valor Inválido, ya existe ese Concepto"
                                ban = 1
                            End If
                        Case "B"
                            SCMValores.CommandText = "UPDATE cg_concepto_comp SET status = 'B' WHERE id_concepto_comp = @id_concepto_comp"
                            SCMValores.Parameters.AddWithValue("@id_concepto_comp", .gvConcepto.SelectedRow.Cells(0).Text)
                        Case Else
                            If validar() Then
                                SCMValores.CommandText = "UPDATE cg_concepto_comp SET id_concepto_cat = @id_concepto_cat, abreviatura = @abreviatura, concepto = @concepto, cuenta = @cuenta, id_cc = @id_cc, alimentos = @alimentos, iva = @iva, combustible = @combustible, reqFirmaD = @reqFirmaD, no_conceptos = @no_conceptos, cve_concepto1 = @cve_concepto1, cantidad1 = @cantidad1, cve_concepto2 = @cve_concepto2, cantidad2 = @cantidad2, val_orig_destino = @val_orig_destino  WHERE id_concepto_comp = @id_concepto_comp"
                                SCMValores.Parameters.AddWithValue("@id_concepto_comp", .gvConcepto.SelectedRow.Cells(0).Text)
                            Else
                                .litError.Text = "Valor Inválido, ya existe ese Concepto"
                                ban = 1
                            End If
                    End Select
                    If ban = 0 Then
                        SCMValores.Parameters.AddWithValue("@id_concepto_cat", .ddlCategoria.SelectedValue)
                        SCMValores.Parameters.AddWithValue("@abreviatura", .txtAbrev.Text)
                        SCMValores.Parameters.AddWithValue("@concepto", .txtConcepto.Text)
                        SCMValores.Parameters.AddWithValue("@cuenta", .txtCuentaC.Text)
                        SCMValores.Parameters.AddWithValue("@id_cc", .ddlCC.SelectedValue)
                        If .cbValAlimentos.Checked = True Then
                            SCMValores.Parameters.AddWithValue("@alimentos", 1)
                        Else
                            SCMValores.Parameters.AddWithValue("@alimentos", 0)
                        End If
                        If .wneIVA.Text.Trim = "" Then
                            SCMValores.Parameters.AddWithValue("@iva", DBNull.Value)
                        Else
                            SCMValores.Parameters.AddWithValue("@iva", .wneIVA.Value)
                        End If
                        If .cbCombustible.Checked = True Then
                            SCMValores.Parameters.AddWithValue("@combustible", 1)
                        Else
                            SCMValores.Parameters.AddWithValue("@combustible", 0)
                        End If
                        If .cbReqAutDir.Checked = True Then
                            SCMValores.Parameters.AddWithValue("@reqFirmaD", "S")
                        Else
                            SCMValores.Parameters.AddWithValue("@reqFirmaD", "N")
                        End If
                        SCMValores.Parameters.AddWithValue("@no_conceptos", .ddlNoConceptos.SelectedValue)
                        If .ddlCant1.Visible = True Then
                            SCMValores.Parameters.AddWithValue("@cve_concepto1", .ddlClave1.SelectedValue)
                            SCMValores.Parameters.AddWithValue("@cantidad1", .ddlCant1.SelectedValue)
                        Else
                            SCMValores.Parameters.AddWithValue("@cve_concepto1", DBNull.Value)
                            SCMValores.Parameters.AddWithValue("@cantidad1", DBNull.Value)
                        End If
                        If .ddlCant2.Visible = True Then
                            SCMValores.Parameters.AddWithValue("@cve_concepto2", .ddlClave2.SelectedValue)
                            SCMValores.Parameters.AddWithValue("@cantidad2", .ddlCant2.SelectedValue)
                        Else
                            SCMValores.Parameters.AddWithValue("@cve_concepto2", DBNull.Value)
                            SCMValores.Parameters.AddWithValue("@cantidad2", DBNull.Value)
                        End If
                        If .cbVali_Orineg_Destino.Checked = True Then
                            SCMValores.Parameters.AddWithValue("@val_orig_destino", "S")
                        Else
                            SCMValores.Parameters.AddWithValue("@val_orig_destino", "N")
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