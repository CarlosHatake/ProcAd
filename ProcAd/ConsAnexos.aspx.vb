Imports Infragistics.Web.UI.ListControls

Public Class ConsAnexos
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                    _txtIdUsuario.Text = Session("id_usuario")
                    limpiarControles()
                    pnlCarga.Visible = False
                    btnExportar.Visible = False
                Else
                    Server.Transfer("Login.aspx")
                End If
            Catch ex As Exception
                litError.Text = ex.Message
            End Try
        End If
    End Sub
#End Region

#Region "Funciones"
    Public Sub limpiarControles()
        Try
            cbCarga.Checked = False
            cbContratos.Checked = False
            cbAnexos.Checked = False
            cbUnidades.Checked = False
            cbImporteUnidades.Checked = False
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Public Sub llenarGridCarga()
        Try
            llenarGridContratos()
            llenarGridAnexos(0)
            llenarGridUnidades(0)
            llenarGridImportesUnidades(0)
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Public Sub llenarGridContratos()
        Try
            If cbAnexos.Checked = True Or cbUnidades.Checked = True Then
                gvContratos.Columns(1).Visible = True
            Else
                gvContratos.Columns(1).Visible = False
            End If

            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim sdaConsulta As New SqlDataAdapter
            Dim dsConsulta As New DataSet
            Dim query As String
            query = " SELECT id_ms_contrato, empresa, no_contrato, arrendadora, tipo_arrendamiento, plazo_meses , fecha_inicio, fecha_fin, inversion FROM ms_contrato_arrenda "
            If cbCarga.Checked = True And txtNoCarga.Text <> "" Then
                query = query + " where id_carga = @id_carga"
            End If
            query = query + " order by no_contrato asc"
            sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)
            sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_carga", Val(txtNoCarga.Text))
            gvContratos.DataSource = dsConsulta
            ConexionBD.Open()
            sdaConsulta.Fill(dsConsulta)
            ConexionBD.Close()
            gvContratos.DataBind()
            sdaConsulta.Dispose()
            dsConsulta.Dispose()
            gvContratos.Columns(0).Visible = False
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Public Sub llenarGridAnexos(idContrato As Integer)
        Try
            If cbUnidades.Checked = True Then
                gvAnexos.Columns(1).Visible = True
            Else
                gvAnexos.Columns(1).Visible = False
            End If

            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim sdaConsulta As New SqlDataAdapter
            Dim dsConsulta As New DataSet
            Dim query As String
            query = " SELECT id_ms_anexo, no_contrato, anexo, MAA.arrendadora, MAA.tipo_arrendamiento FROM  ms_anexo_arrenda MAA " +
                " INNER JOIN ms_contrato_arrenda MCA on MAA.id_ms_contrato = MCA.id_ms_contrato WHERE 1 = 1 "
            If idContrato <> 0 Then
                query = query + " and MAA.id_ms_contrato = @id_ms_contrato "
            End If
            If cbCarga.Checked = True And txtNoCarga.Text <> "" Then
                query = query + " and MCA.id_carga = @id_carga "
            End If
            sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)
            sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_carga", Val(txtNoCarga.Text))
            sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_ms_contrato", idContrato)
            gvAnexos.DataSource = dsConsulta
            ConexionBD.Open()
            sdaConsulta.Fill(dsConsulta)
            ConexionBD.Close()
            gvAnexos.DataBind()
            sdaConsulta.Dispose()
            dsConsulta.Dispose()
            gvAnexos.Columns(0).Visible = False
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Public Sub llenarGridUnidades(idAnexo As Integer)
        Try
            If cbImporteUnidades.Checked = True Then
                gvUnidades.Columns(1).Visible = True
            Else
                gvUnidades.Columns(1).Visible = False
            End If

            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim sdaConsulta As New SqlDataAdapter
            Dim dsConsulta As New DataSet
            Dim query As String
            query = " Select id_ms_equipo, MCA.no_contrato, MAA.anexo, serie, MAA.tipo_arrendamiento   from ms_equipo_arrenda MEA " +
                " inner join ms_anexo_arrenda MAA on MEA.id_ms_anexo = MAA.id_ms_anexo " +
                " inner join ms_contrato_arrenda MCA on MCA.id_ms_contrato = MAA.id_ms_contrato " +
                " where 1 = 1"
            If idAnexo <> 0 Then
                query = query + " and MEA.id_ms_anexo = @id_ms_anexo "
            End If
            If cbCarga.Checked = True And txtNoCarga.Text <> "" Then
                query = query + " and MCA.id_carga = @id_carga "
            End If
            sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)
            sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_carga", Val(txtNoCarga.Text))
            sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_ms_anexo", idAnexo)
            gvUnidades.DataSource = dsConsulta
            ConexionBD.Open()
            sdaConsulta.Fill(dsConsulta)
            ConexionBD.Close()
            gvUnidades.DataBind()
            sdaConsulta.Dispose()
            dsConsulta.Dispose()
            gvUnidades.Columns(0).Visible = False

        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Public Sub llenarGridImportesUnidades(idUnidades)
        Try
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim sdaConsulta As New SqlDataAdapter
            Dim dsConsulta As New DataSet
            Dim query As String
            query = " Select MEA.serie, año, isnull(mes1, 0) as mes1, isnull(mes2, 0) as mes2, isnull(mes3, 0) as mes3, isnull(mes4, 0) as mes4, isnull(mes5, 0) as mes5, isnull(mes6, 0) as mes6, isnull(mes7, 0) as mes7, isnull(mes8, 0) as mes8, isnull(mes9, 0) as mes9, isnull(mes10, 0) as mes10, isnull(mes11, 0) as mes11, isnull(mes12, 0) as mes12 from ms_equipo_importes MEI " +
                " inner join ms_equipo_arrenda MEA on MEI.id_ms_equipo_arrenda  = MEA.id_ms_equipo " +
                " where 1 = 1 "
            If idUnidades <> 0 Then
                query = query + " and MEI.id_ms_equipo_arrenda = @id_equipo"
            End If
            If cbCarga.Checked = True And txtNoCarga.Text <> "" Then
                query = query + " and MEA.id_carga = @id_carga "
            End If
            sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)
            sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_carga", Val(txtNoCarga.Text))
            sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_equipo", idUnidades)
            gvImportes.DataSource = dsConsulta
            ConexionBD.Open()
            sdaConsulta.Fill(dsConsulta)
            ConexionBD.Close()
            gvImportes.DataBind()
            sdaConsulta.Dispose()
            dsConsulta.Dispose()
            gvImportes.Columns(0).Visible = False
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "Botones"
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            If cbCarga.Checked = True And txtNoCarga.Text <> "" Then
                llenarGridCarga()
            Else
                If cbContratos.Checked = True Then
                    llenarGridContratos()
                End If
                If cbAnexos.Checked = True Then
                    gvContratos.Columns(0).Visible = True
                    If cbContratos.Checked = True Then
                        llenarGridAnexos(Val(gvContratos.Rows(0).Cells(0).Text))
                    Else
                        llenarGridAnexos(0)
                    End If
                    gvContratos.Columns(0).Visible = False
                End If
                If cbUnidades.Checked = True Then
                    gvAnexos.Columns(0).Visible = True

                    If cbAnexos.Checked = True Then
                        If gvAnexos.Rows.Count() <> 0 Then
                            llenarGridUnidades(Val(gvAnexos.Rows(0).Cells(0)))
                        End If
                    Else
                        llenarGridUnidades(0)
                    End If
                End If
                If cbImporteUnidades.Checked = True Then
                    gvUnidades.Columns(0).Visible = True
                    If cbUnidades.Checked = True Then
                        If gvUnidades.Rows().Count() <> 0 Then
                            llenarGridImportesUnidades(gvUnidades.Rows(0).Cells(0))
                        End If
                    Else
                        llenarGridImportesUnidades(0)
                    End If
                End If
            End If

            If gvContratos.Rows.Count() <> 0 Or gvAnexos.Rows.Count() <> 0 Or gvUnidades.Rows.Count() <> 0 Then
                btnExportar.Visible = True
            End If
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        Try
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim sdaConsulta As New SqlDataAdapter
            Dim dsConsulta As New DataSet
            Dim query As String = ""
            If cbCarga.Checked = True Then
                query = " Select MCA.no_contrato, MCA.empresa, MCA.arrendadora, MCA.tipo_arrendamiento, MAA.anexo, MCA.fecha_inicio, MCA.fecha_fin, MEA.serie, MEI.año, MEI.mes1 " +
                    " , MEI.mes2, MEI.mes3, MEI.mes4, MEI.mes5, mes6, mes7, mes8, mes9, mes10, mes11, mes12 " +
                    " from ms_contrato_arrenda MCA inner join ms_anexo_arrenda MAA on MCA.id_ms_contrato = MAA.id_ms_contrato " +
                    " inner join ms_equipo_arrenda MEA on MEA.id_ms_anexo = MAA.id_ms_anexo " +
                    " inner join ms_equipo_importes MEI on MEI.id_ms_equipo_arrenda = MEA.id_ms_equipo  where MCA.id_carga = @id_carga "
            Else
                If cbContratos.Checked = True Then
                    query = " Select MCA.no_contrato, MCA.empresa, MCA.arrendadora, MCA.tipo_arrendamiento, MCA.fecha_inicio, MCA.fecha_fin from ms_contrato_arrenda MCA "
                End If
                If cbAnexos.Checked = True Then

                    query = "Select MCA.no_contrato, MCA.empresa, MCA.arrendadora, MCA.tipo_arrendamiento, MAA.anexo, MCA.fecha_inicio, MCA.fecha_fin from ms_contrato_arrenda MCA " +
                            " inner join ms_anexo_arrenda MAA on MCA.id_ms_contrato = MAA.id_ms_contrato "

                End If
                If cbUnidades.Checked = True Then
                    query = " Select MCA.no_contrato, MCA.empresa, MCA.arrendadora, MCA.tipo_arrendamiento, MAA.anexo, MCA.fecha_inicio, MCA.fecha_fin, MEA.serie from ms_contrato_arrenda MCA inner join ms_anexo_arrenda MAA on MCA.id_ms_contrato = MAA.id_ms_contrato inner join ms_equipo_arrenda MEA on MEA.id_ms_anexo = MAA.id_ms_anexo "
                End If
                If cbImporteUnidades.Checked = True Then
                    query = " Select MCA.no_contrato, MCA.empresa, MCA.arrendadora, MCA.tipo_arrendamiento, MAA.anexo, MCA.fecha_inicio, MCA.fecha_fin, MEA.serie, MEI.año, MEI.mes1 " +
                   " , MEI.mes2, MEI.mes3, MEI.mes4, MEI.mes5, mes6, mes7, mes8, mes9, mes10, mes11, mes12 " +
                   " from ms_contrato_arrenda MCA inner join ms_anexo_arrenda MAA on MCA.id_ms_contrato = MAA.id_ms_contrato " +
                   " inner join ms_equipo_arrenda MEA on MEA.id_ms_anexo = MAA.id_ms_anexo " +
                   " inner join ms_equipo_importes MEI on MEI.id_ms_equipo_arrenda = MEA.id_ms_equipo "
                End If
            End If
            sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)
            sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_carga", Val(txtNoCarga.Text))
            gvExportar.DataSource = dsConsulta
            ConexionBD.Open()
            sdaConsulta.Fill(dsConsulta)
            ConexionBD.Close()
            gvExportar.DataBind()
            sdaConsulta.Dispose()
            dsConsulta.Dispose()


            Dim tw As New System.IO.StringWriter
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)

            Try
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment;filename=Registros.xls")
                Response.ContentType = "application/vnd.ms-excel"
                gvExportar.Visible = True
                gvExportar.RenderControl(hw)
                gvExportar.Visible = False
                Response.Write(tw.ToString())
                Response.End()
            Catch ex As Exception
                litError.Text = ex.ToString()
            End Try
            gvExportar.Visible = False
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub
#End Region

#Region "Grid"
    Protected Sub gvContratos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvContratos.SelectedIndexChanged
        Try
            llenarGridAnexos(Val(gvContratos.SelectedRow.Cells(0).Text))
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvAnexos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvAnexos.SelectedIndexChanged
        Try
            llenarGridUnidades(Val(gvAnexos.SelectedRow.Cells(0).Text))
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvUnidades_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvUnidades.SelectedIndexChanged
        Try
            llenarGridImportesUnidades(Val(gvUnidades.SelectedRow.Cells(0).Text))
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

#End Region

#Region "Controles"
    Protected Sub cbCarga_CheckedChanged(sender As Object, e As EventArgs) Handles cbCarga.CheckedChanged
        Try
            If cbCarga.Checked = True Then
                pnlCarga.Visible = True
                limpiarControles()
                cbCarga.Checked = True
            Else
                pnlCarga.Visible = False
            End If
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvContratos_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvContratos.PageIndexChanging
        Try
            gvContratos.PageIndex = e.NewPageIndex
            llenarGridContratos()
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

#End Region

End Class