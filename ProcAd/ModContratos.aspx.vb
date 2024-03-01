Public Class ModContratos
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then

                        ._txtIdContrato.Text = Session("idContrato")
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtBan.Text = 0

                        detalleContrato()

                        Dim anexos As Integer
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        SCMValores.Parameters.Clear()

                        SCMValores.CommandText = "SELECT COUNT (*) FROM ms_equipo_importes WHERE id_ms_contrato = @id_ms_contrato AND id_ms_anexo IS NOT NULL"
                        SCMValores.Parameters.AddWithValue("@añoActual", Val(._txtIdContrato.Text))
                        ConexionBD.Open()
                        anexos = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        'si el contrato tiene anexos
                        If anexos > 0 Then
                            llenarAnexos()
                        Else
                            llenarUnidadesContratos()
                        End If

                        ' llenarUnidadesAnexos()
                    Else
                        Server.Transfer("Login.aspx")
                    End If
                Catch ex As Exception

                End Try
            End With
        End If
    End Sub

#Region "Modificación de contrato"
    Public Sub detalleContrato()
        With Me
            Try
                .pnlInfoContrato.Visible = True

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim query As String
                Dim sdaContrato As New SqlDataAdapter
                Dim dsContrato As New DataSet

                query = " SELECT no_contrato, empresa, rfc_arrendadora, arrendadora, tipo_arrendamiento, plazo_meses, COALESCE(CONVERT(VARCHAR(30), fecha_inicio), '-') AS fecha_inicio, COALESCE(CONVERT(VARCHAR(30), fecha_fin), '-') AS fecha_fin, inversion FROM ms_contrato_arrenda WHERE id_ms_contrato = @id_ms_contrato "
                sdaContrato.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaContrato.SelectCommand.Parameters.AddWithValue("@id_ms_contrato", Val(._txtIdContrato.Text))
                ConexionBD.Open()
                sdaContrato.Fill(dsContrato)
                ConexionBD.Close()

                .lblContrato.Text = dsContrato.Tables(0).Rows(0).Item("no_contrato").ToString()
                .lblEmpresa.Text = dsContrato.Tables(0).Rows(0).Item("empresa").ToString()
                .lblArrendadora.Text = dsContrato.Tables(0).Rows(0).Item("arrendadora").ToString()
                .lblTipoArrendamiento.Text = dsContrato.Tables(0).Rows(0).Item("tipo_arrendamiento").ToString()
                .lblRFCArrendadora.Text = dsContrato.Tables(0).Rows(0).Item("rfc_arrendadora").ToString()
                .lblPlazo.Text = dsContrato.Tables(0).Rows(0).Item("plazo_meses").ToString()
                .lblFecInicio.Text = dsContrato.Tables(0).Rows(0).Item("fecha_inicio").ToString()
                .lblFechaFin.Text = dsContrato.Tables(0).Rows(0).Item("fecha_fin").ToString()
                .lblInversion.Text = dsContrato.Tables(0).Rows(0).Item("inversion").ToString()

            Catch ex As Exception

            End Try
        End With
    End Sub

    Public Sub llenarAnexos()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaAnexos As New SqlDataAdapter
                Dim dsAnexos As New DataSet
                .gvAnexos.DataSource = dsAnexos


                Dim query As String = ""

                query = " SELECT id_ms_anexo, empresa, anexo, tipo_arrendamiento, arrendadora, fecha_inicio, fecha_fin FROM ms_anexo_arrenda WHERE id_ms_contrato =@id_ms_contrato "

                sdaAnexos.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaAnexos.SelectCommand.Parameters.AddWithValue("@id_ms_contrato", Val(._txtIdContrato.Text))
                ConexionBD.Open()
                sdaAnexos.Fill(dsAnexos)
                .gvAnexos.DataBind()
                ConexionBD.Close()
                sdaAnexos.Dispose()
                dsAnexos.Dispose()
                .gvAnexos.SelectedIndex = -1
                .pnlgvAnexos.Update()
                If gvAnexos.Rows.Count() > 0 Then
                    .pnlgvAnexos.Visible = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub


    Public Sub llenarUnidadesAnexos()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaUnidades As New SqlDataAdapter
                Dim dsUnidades As New DataSet
                Dim importe As Double
                Dim importeTotal As Double
                .gvUnidades.DataSource = dsUnidades


                Dim query As String = ""

                query = " SELECT id_ms_equipo, importe.serie, equipo.id_ms_anexo," +
                " ISNULL(economico, '-') AS economico, " +
                " ISNULL(año, '-') AS año, " +
                " ISNULL(mes1, '-') AS mes1, " +
                " ISNULL(mes2, '-') AS mes2, " +
                " ISNULL(mes3, '-') AS mes3, " +
                " ISNULL(mes4, '-') AS mes4, " +
                " ISNULL(mes5, '-') AS mes5, " +
                " ISNULL(mes6, '-') AS mes6, " +
                " ISNULL(mes7, '-') AS mes7, " +
                " ISNULL(mes8, '-') AS mes8, " +
                " ISNULL(mes9, '-') AS mes9, " +
                " ISNULL(mes10, '-') AS mes10, " +
                " ISNULL(mes11, '-') AS mes11, " +
                " ISNULL(mes12, '-') AS mes12 " +
                " FROM ms_equipo_arrenda  equipo " +
                " LEFT JOIN ms_equipo_importes importe on importe.id_ms_anexo = equipo.id_ms_anexo " +
                " WHERE equipo.id_ms_anexo = @id_ms_anexo "

                sdaUnidades.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaUnidades.SelectCommand.Parameters.AddWithValue("@id_ms_anexo", Val(.gvAnexos.DataKeys(gvAnexos.SelectedIndex).Values("id_ms_anexo")))
                'sdaUnidades.SelectCommand.Parameters.AddWithValue("@id_ms_anexo", 9531)
                ConexionBD.Open()
                sdaUnidades.Fill(dsUnidades)
                .gvUnidades.DataBind()
                ConexionBD.Close()
                sdaUnidades.Dispose()
                dsUnidades.Dispose()
                .gvUnidades.SelectedIndex = -1
                If .gvUnidades.Rows.Count() > 0 Then
                    '.upUnidades.Update()
                    .pnlUnidades.Visible = True
                    .pnlgvUnidades.Visible = True
                End If

                For Each row As DataRow In dsUnidades.Tables(0).Rows

                    For Each column As DataColumn In dsUnidades.Tables(0).Columns  'Recorre las columnas 
                        Dim na As String = column.ColumnName.ToString()

                        If na <> "id_ms_equipo" And na <> "serie" And na <> "id_ms_anexo" And na <> "economico" And na <> "año" Then

                            importe = row.Item(column).ToString()
                            importeTotal += CDbl(importe)
                        End If

                    Next
                Next
                .lblTotal.Text = "$ " & importeTotal

            Catch ex As Exception

            End Try
        End With
    End Sub

    Public Sub llenarUnidadesContratos()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaUnidades As New SqlDataAdapter
                Dim dsUnidades As New DataSet
                .gvUnidades.DataSource = dsUnidades


                Dim query As String = ""

                query = " SELECT id_ms_equipo, importe.serie, equipo.id_ms_anexo," +
                " ISNULL(economico, '-') AS economico, " +
                " ISNULL(año, '-') AS año, " +
                " ISNULL(mes1, '-') AS mes1, " +
                " ISNULL(mes2, '-') AS mes2, " +
                " ISNULL(mes3, '-') AS mes3, " +
                " ISNULL(mes4, '-') AS mes4, " +
                " ISNULL(mes5, '-') AS mes5, " +
                " ISNULL(mes6, '-') AS mes6, " +
                " ISNULL(mes7, '-') AS mes7, " +
                " ISNULL(mes8, '-') AS mes8, " +
                " ISNULL(mes9, '-') AS mes9, " +
                " ISNULL(mes10, '-') AS mes10, " +
                " ISNULL(mes11, '-') AS mes11, " +
                " ISNULL(mes12, '-') AS mes12 " +
                " FROM ms_equipo_arrenda  equipo " +
                " LEFT JOIN ms_equipo_importes importe on importe.id_ms_contrato  = equipo.id_ms_contrato " +
                " WHERE importe.id_ms_contrato = @id_ms_contrato "

                sdaUnidades.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaUnidades.SelectCommand.Parameters.AddWithValue("@id_ms_contrato", Val(._txtIdContrato))
                ConexionBD.Open()
                sdaUnidades.Fill(dsUnidades)
                .gvUnidades.DataBind()
                ConexionBD.Close()
                sdaUnidades.Dispose()
                dsUnidades.Dispose()
                .gvUnidades.SelectedIndex = -1
                If .gvUnidades.Rows.Count() > 0 Then
                    '.upUnidades.Update()
                    .pnlUnidades.Visible = True
                    .pnlgvUnidades.Visible = True
                End If
            Catch ex As Exception

            End Try
        End With
    End Sub

    Public Sub llenarCC()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCC As New SqlDataAdapter
                Dim dsCC As New DataSet
                sdaCC.SelectCommand = New SqlCommand("select 0 as id_cc " +
                                                     "     , ' ' as nombre " +
                                                     "union " +
                                                     "select id_cc " +
                                                     "     , nombre " +
                                                     "from bd_Empleado.dbo.cg_cc " +
                                                     "where id_empresa = (SELECT id_empresa FROM bd_Empleado.dbo.cg_empresa WHERE nombre = @nombre AND status = 'A') " +
                                                     "  and status = 'A' " +
                                                     "order by nombre ", ConexionBD)
                sdaCC.SelectCommand.Parameters.AddWithValue("@nombre", .lblEmpresa.Text)
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


            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarDiv()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaDiv As New SqlDataAdapter
                Dim dsDiv As New DataSet
                sdaDiv.SelectCommand = New SqlCommand("select 0 as id_div " +
                                                      "     , ' ' as nombre " +
                                                      "union " +
                                                      "select id_div " +
                                                      "     , nombre " +
                                                      "from bd_Empleado.dbo.cg_div " +
                                                      "where id_empresa = (SELECT id_empresa FROM bd_Empleado.dbo.cg_empresa WHERE nombre = @nombre AND status = 'A') " +
                                                      "  and status = 'A' " +
                                                      "order by nombre ", ConexionBD)
                sdaDiv.SelectCommand.Parameters.AddWithValue("@nombre", lblEmpresa.Text)
                .ddlDivision.DataSource = dsDiv
                .ddlDivision.DataTextField = "nombre"
                .ddlDivision.DataValueField = "id_div"

                ConexionBD.Open()
                sdaDiv.Fill(dsDiv)
                .ddlDivision.DataBind()
                ConexionBD.Close()
                sdaDiv.Dispose()
                dsDiv.Dispose()
                .ddlDivision.SelectedIndex = -1

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub
#End Region

    Protected Sub gvAnexos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvAnexos.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim query As String
                Dim sdaContrato As New SqlDataAdapter
                Dim dsContrato As New DataSet

                query = " SELECT anexo, fecha_inicio, fecha_fin FROM ms_anexo_arrenda WHERE id_ms_anexo = @id_ms_anexo "
                sdaContrato.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaContrato.SelectCommand.Parameters.AddWithValue("@id_ms_anexo", Val(.gvAnexos.DataKeys(gvAnexos.SelectedIndex).Values("id_ms_anexo")))
                ConexionBD.Open()
                sdaContrato.Fill(dsContrato)
                ConexionBD.Close()

                .txtAnexo.Text = dsContrato.Tables(0).Rows(0).Item("anexo").ToString()
                .wdpFecInicioAnexo.Date = dsContrato.Tables(0).Rows(0).Item("fecha_inicio")
                .wdpFecFinAnexo.Date = dsContrato.Tables(0).Rows(0).Item("fecha_fin")

                .pnlModificarAnexo.Visible = True
                llenarUnidadesAnexos()

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub


#Region "Modificar anexo"
    Protected Sub btnGuardarAnexo_Click(sender As Object, e As EventArgs) Handles btnGuardarAnexo.Click
        With Me
            Try
                .litError.Text = ""

                If .txtAnexo.Text.Trim() = "" Then
                    .litError.Text = "Complete la información del contrato"

                ElseIf .wdpFecInicioAnexo.Text > .wdpFecFinAnexo.Text Then
                    litError.Text = "Fechas inválidas, rectifique"
                End If

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD

                SCMValores.Parameters.Clear()
                SCMValores.CommandText = " UPDATE ms_anexo_arrenda SET " +
                                             " anexo = @anexo, " +
                                             " fecha_inicio = @fecha_inicio, " +
                                             " fecha_fin = @fecha_fin " +
                                             " WHERE id_ms_anexo = @id_ms_anexo "

                SCMValores.Parameters.AddWithValue("@id_ms_anexo", Val(.gvAnexos.DataKeys(gvAnexos.SelectedIndex).Values("id_ms_anexo")))
                SCMValores.Parameters.AddWithValue("@anexo", .txtAnexo.Text)
                SCMValores.Parameters.AddWithValue("@fecha_inicio", wdpFecInicioAnexo.Text)
                SCMValores.Parameters.AddWithValue("@fecha_fin", .wdpFecFinAnexo.Text)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()
                llenarAnexos()
                .pnlModificarAnexo.Visible = False
                .gvAnexos.SelectedIndex = -1

            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub
#End Region

#Region "Modificar unidades"


    Protected Sub btnImportarUnidades_Click(sender As Object, e As EventArgs) Handles btnImportarUnidades.Click
        With Me
            Try
                litError.Text = ""
                '' Ruta local
                Dim rutaArchivo As String = "C:\ProcAd - ModUnidades\"
                ' Ruta en Atenea
                ' Dim rutaArchivo As String = "D:\ProcAd - ModUnidades\" 'Ruta en que se almacenará el archivo
                Dim sFileName As String = System.IO.Path.GetFileName(UpArchivo.PostedFile.FileName)
                Dim sFileExt As String = System.IO.Path.GetExtension(UpArchivo.PostedFile.FileName)
                If sFileExt = ".xlsx" Then
                    'Guarda archivo'
                    UpArchivo.PostedFile.SaveAs(Server.MapPath("ModUnidades\" + sFileName))

                    'Dim i As Integer
                    Dim dtUnidades As DataTable
                    Dim workbook As Workbook = New Workbook()
                    workbook.LoadFromFile(Server.MapPath("ModUnidades\" + sFileName))

                    Dim sheet As Worksheet = workbook.Worksheets(0)
                    dtUnidades = sheet.ExportDataTable(sheet.AllocatedRange, True, True)

                    If dtUnidades.Columns.Count >= 3 Then

                        Dim dtInsertar As New DataTable
                        dtInsertar.Columns.Add("importe")
                        dtInsertar.Columns.Add("mes")
                        dtInsertar.Columns.Add("año")
                        dtInsertar.Columns.Add("serie")

                        Dim iFin As Integer
                        Dim cFin As Integer
                        Dim año As String
                        Dim mes As String
                        Dim serie As String
                        Dim importe As String
                        Dim ban As Integer

                        'Se establece el número de registros a depurar
                        iFin = dtUnidades.Rows.Count - 1
                        cFin = dtUnidades.Columns.Count - 1

                        Dim name(dtUnidades.Columns.Count) As String
                        Dim c As Integer = 0


                        'For Each column As DataColumn In dtUnidades.Columns 'Recorre las columnas 
                        '    name(c) = column.ColumnName
                        '    c += 1


                        '    fec = column.ColumnName.Substring(0, 3)

                        '    If fec = "mes" Then 'Si la columna es un mes

                        '        While i <= iFin
                        '            año = column.ColumnName.Substring(5, 2) 'Obtener el año de la columna
                        '            If col > 2 And año <> añoAnterior Then 'Si el año es diferente al año anterior termina el ciclo 
                        '                Exit While
                        '            Else
                        '                Dim valorCelda As String = dtUnidades.Rows(i).Item(col)
                        '                dtInsertar.Rows.Add(New Object() {valorCelda, año})

                        '                fila = dtInsertar.Rows.Count - 1
                        '                añoAnterior = dtInsertar.Rows(fila)("año")
                        '                col += 1

                        '            End If
                        '        End While
                        '    ElseIf name Is "" Then
                        '        No hay más columnas a la derecha
                        '    End If

                        'Next


                        'While i <= iFin



                        ' Dim name As String = (dtUnidades.Columns(col).ColumnName.ToString.ToUpper)


                        For Each row As DataRow In dtUnidades.Rows

                            For Each column As DataColumn In dtUnidades.Columns 'Recorre las columnas 

                                'importe = (dtUnidades.Rows(i).Item(col).ToString)
                                'año = dtUnidades.Columns(col).ColumnName.Substring(4, 4)
                                'mes = dtUnidades.Columns(col).ColumnName.Substring(1, 2)
                                'serie = dtUnidades.Rows(i).Item(0)

                                Dim na As String = column.ColumnName.ToString()


                                If na <> "Serie" And na <> "Economico" Then

                                    importe = row.Item(column).ToString()
                                    serie = row.Item(0).ToString()
                                    año = (column.ColumnName.Substring(4, 4))
                                    mes = (column.ColumnName.Substring(1, 2))


                                    dtInsertar.Rows.Add(New Object() {importe, mes, año, serie})


                                    'Enviar datos a actualizar

                                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                                    SCMValores.Connection = ConexionBD
                                    SCMValores.Parameters.Clear()

                                    SCMValores.CommandText = "SELECT COUNT(*) FROM ms_equipo_importes " +
                                                             " WHERE serie = @serie " +
                                                             " AND año = @añoActual"
                                    SCMValores.Parameters.AddWithValue("@añoActual", Val(año))
                                    SCMValores.Parameters.AddWithValue("@serie", serie)
                                    ConexionBD.Open()
                                    ban = SCMValores.ExecuteScalar()
                                    ConexionBD.Close()

                                    If ban = 1 Then

                                        SCMValores.CommandText = " EXEC SP_U_ms_equipo_importe @mesActualizar, @importe, @año, @serieUnidad "

                                        SCMValores.Parameters.AddWithValue("@mesActualizar", Val(mes))

                                        If importe = "" Then
                                            SCMValores.Parameters.AddWithValue("@importe", DBNull.Value)
                                        Else
                                            SCMValores.Parameters.AddWithValue("@importe", Val(importe))
                                        End If
                                        SCMValores.Parameters.AddWithValue("@año", Val(año))
                                        SCMValores.Parameters.AddWithValue("@serieUnidad", serie)
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()
                                    Else

                                        SCMValores.CommandText = " EXEC SP_I_ms_equipo_importes @mesInsertar, @importe, @año, @serieUnidad, @id_ms_contrato, @id_ms_anexo, @id_usuario "

                                        SCMValores.Parameters.AddWithValue("@mesInsertar", Val(mes))

                                        If importe = "" Then
                                            SCMValores.Parameters.AddWithValue("@importe", DBNull.Value)
                                        Else
                                            SCMValores.Parameters.AddWithValue("@importe", Val(importe))
                                        End If
                                        SCMValores.Parameters.AddWithValue("@año", Val(año))
                                        SCMValores.Parameters.AddWithValue("@serieUnidad", serie)
                                        SCMValores.Parameters.AddWithValue("@id_ms_contrato", Val(._txtIdContrato.Text))

                                        If gvAnexos.SelectedIndex = -1 Then
                                            SCMValores.Parameters.AddWithValue("@id_ms_anexo", DBNull.Value)

                                        Else
                                            SCMValores.Parameters.AddWithValue("@id_ms_anexo", Val(.gvAnexos.DataKeys(gvAnexos.SelectedIndex).Values("id_ms_anexo")))
                                        End If
                                        'If .cbConsultaUnidades.SelectedIndex = 0 Then
                                        '    SCMValores.Parameters.AddWithValue("@id_ms_anexo", Val(.gvAnexos.DataKeys(gvAnexos.SelectedIndex).Values("id_ms_anexo")))
                                        'ElseIf .cbConsultaUnidades.SelectedIndex = 1 Then
                                        '    SCMValores.Parameters.AddWithValue("@id_ms_anexo", DBNull.Value)
                                        'End If

                                        SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()

                                    End If


                                End If



                            Next

                        Next



                        ' End While

                        'llenarUnidades()



                        '    Dim valorCelda As String = dtUnidades.Rows(i).Item(i)
                        '    'If dtUnidades.Columns.Contains("/") Then
                        '    '    Dim año As String = valorCelda.Substring(4, 6)
                        '    'End If
                        'Next

                        'For Each row As DataRow In dtUnidades.Rows
                        '    'For i = dtUnidades.Columns.Count - 1 To i >= 0
                        '    Dim valorCelda As String = row.ToString()
                        '    Dim año As Integer = valorCelda.Substring(4, 6)
                        '    Dim array1 As Object() = row.ItemArray
                        '    'Next
                        'Next


                    Else
                        .litError.Text = "El formato excel no es correcto"

                    End If

                Else
                    .litError.Text = "Formato de archivo no compatible"

                End If

            Catch ex As Exception
                .litError.Text = ex.Message
            End Try
        End With
    End Sub


    'Protected Sub cbConsultaUnidades_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    With Me
    '        Try

    '            If cbConsultaUnidades.SelectedIndex = 0 Then 'Consulta por anexos
    '                llenarUnidadesAnexos()
    '            ElseIf cbConsultaUnidades.SelectedIndex = 1 Then 'Consulta por contrato
    '                llenarUnidadesContratos()
    '            End If

    '        Catch ex As Exception
    '            .litError.Text = ex.ToString()
    '        End Try
    '    End With
    'End Sub



#End Region

#Region "Exportar"
    Protected Sub btnExportarUnidades_Click(sender As Object, e As EventArgs) Handles btnExportarUnidades.Click

        Try
            generarExcel()

            Response.ClearContent()
            Response.Buffer = True
            Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", "TestPage.xls"))
            Response.ContentEncoding = Encoding.UTF8
            Response.ContentType = "application/ms-excel"
            Dim sw As New StringWriter()
            Dim htw As New HtmlTextWriter(sw)
            gvUnidadesExportar.RenderControl(htw)
            Response.Write(sw.ToString())
            Response.[End]()

        Catch ex As Exception

        End Try

    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub

    Public Sub generarExcel()
        With Me
            Try

                Dim dtExcel As New DataTable
                dtExcel.Columns.Add("Serie")


                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim query As String
                Dim sdaUnidades As New SqlDataAdapter
                Dim dsUnidades As New DataSet
                Dim sdaAños As New SqlDataAdapter
                Dim dsAños As New DataSet

                Dim serie As String
                Dim año As String
                Dim mes1 As String
                Dim mes2 As String
                Dim mes3 As String
                Dim mes4 As String
                Dim mes5 As String
                Dim mes6 As String
                Dim mes7 As String
                Dim mes8 As String
                Dim mes9 As String
                Dim mes10 As String
                Dim mes11 As String
                Dim mes12 As String

                Dim ban As Integer


                query = " SELECT id_ms_equipo, importe.serie, equipo.id_ms_anexo," +
                " ISNULL(economico, '-') AS economico, " +
                " ISNULL(año, '-') AS año, " +
                " ISNULL(mes1, '-') AS mes1, " +
                " ISNULL(mes2, '-') AS mes2, " +
                " ISNULL(mes3, '-') AS mes3, " +
                " ISNULL(mes4, '-') AS mes4, " +
                " ISNULL(mes5, '-') AS mes5, " +
                " ISNULL(mes6, '-') AS mes6, " +
                " ISNULL(mes7, '-') AS mes7, " +
                " ISNULL(mes8, '-') AS mes8, " +
                " ISNULL(mes9, '-') AS mes9, " +
                " ISNULL(mes10, '-') AS mes10, " +
                " ISNULL(mes11, '-') AS mes11, " +
                " ISNULL(mes12, '-') AS mes12 " +
                " FROM ms_equipo_arrenda equipo " +
                " LEFT JOIN ms_equipo_importes importe on importe.id_ms_anexo  = equipo.id_ms_anexo " +
                " WHERE equipo.id_ms_anexo = 9531 "
                sdaUnidades.SelectCommand = New SqlCommand(query, ConexionBD)
                'sdaContrato.SelectCommand.Parameters.AddWithValue("@id_ms_anexo", Val(.gvAnexos.DataKeys(gvAnexos.SelectedIndex).Values("id_ms_anexo")))
                ConexionBD.Open()
                sdaUnidades.Fill(dsUnidades)
                ConexionBD.Close()

                query = "SELECT DISTINCT año FROM ms_equipo_importes WHERE id_ms_anexo = 9531 ORDER BY año "
                sdaAños.SelectCommand = New SqlCommand(query, ConexionBD)
                ConexionBD.Open()
                sdaAños.Fill(dsAños)
                ConexionBD.Close()


                For Each row As DataRow In dsAños.Tables(0).Rows() ' Se agregan las columnas dependiendo de los diferentes años que tienen las unidades ubicadas

                    año = row.Item("año")

                    dtExcel.Columns.Add("(01-" & año & ")")
                    dtExcel.Columns.Add("(02-" & año & ")")
                    dtExcel.Columns.Add("(03-" & año & ")")
                    dtExcel.Columns.Add("(04-" & año & ")")
                    dtExcel.Columns.Add("(05-" & año & ")")
                    dtExcel.Columns.Add("(06-" & año & ")")
                    dtExcel.Columns.Add("(07-" & año & ")")
                    dtExcel.Columns.Add("(08-" & año & ")")
                    dtExcel.Columns.Add("(09-" & año & ")")
                    dtExcel.Columns.Add("(10-" & año & ")")
                    dtExcel.Columns.Add("(11-" & año & ")")
                    dtExcel.Columns.Add("(12-" & año & ")")
                Next

                For Each row As DataRow In dsUnidades.Tables(0).Rows()



                    'Datos de la consulta
                    serie = row.Item("serie")
                    mes1 = row.Item("mes1")
                    mes2 = row.Item("mes2")
                    mes3 = row.Item("mes3")
                    mes4 = row.Item("mes4")
                    mes5 = row.Item("mes5")
                    mes6 = row.Item("mes6")
                    mes7 = row.Item("mes7")
                    mes8 = row.Item("mes8")
                    mes9 = row.Item("mes9")
                    mes10 = row.Item("mes10")
                    mes11 = row.Item("mes11")
                    mes12 = row.Item("mes12")
                    año = row.Item("año")


                    'Verificar que se inserte la serie de la unidad solo una vez
                    If dtExcel.Rows.Count > 0 Then
                        ban = 0
                        For Each serieExiste As DataRow In dtExcel.Rows
                            If serieExiste.Item(0).ToString() = serie Then
                                ban += 1
                            End If
                        Next
                        If ban = 0 Then
                            dtExcel.Rows.Add(serie)
                        End If
                    Else
                        dtExcel.Rows.Add(serie)
                    End If



                    Dim indiceColumna As Integer = ObtenerIndiceColumnaAño(dtExcel, año)
                    Dim indiceFila As Integer = BuscarFilaSerie(dtExcel, serie)


                    If indiceColumna <> -1 Then
                        InsertarDatosEnFilaEspecifica(dtExcel, indiceFila, indiceColumna, mes1, mes2, mes3, mes4, mes5, mes6, mes7, mes8, mes9, mes10, mes11, mes12)
                    End If


                Next



                .gvUnidadesExportar.DataSource = dtExcel
                .gvUnidadesExportar.DataBind()
                .gvUnidadesExportar.Visible = False


            Catch ex As Exception
                litError.Text = ex.ToString()
            End Try
        End With
    End Sub

    Function ObtenerIndiceColumnaAño(dataTable As DataTable, año As String) As Integer
        'Recorrer las columnas del dt
        For i As Integer = 0 To dataTable.Columns.Count - 1
            'Busca la columna que contiene el año
            If dataTable.Columns(i).ColumnName.Contains(año) Then
                'Devuelve el índice de la columna
                Return i
            End If
        Next

        'Si no encuentra el año
        Return -1
    End Function


    Function BuscarFilaSerie(tabla As DataTable, serie As Object) As Integer
        ' Buscar la fila que contiene la serie de la unidad 
        'For Each fila As DataRow In tabla.Rows
        '    For Each columna As DataColumn In tabla.Columns
        '        If fila(columna) = valorBuscado Then
        '            Dim numeroDeFila As Integer = tabla.Rows.IndexOf(fila)
        '            Return numeroDeFila
        '        End If
        '    Next
        'Next
        'Return Nothing

        For Each fila As DataRow In tabla.Rows
            If fila.Item(0).ToString = serie Then
                Dim numeroDeFila As Integer = tabla.Rows.IndexOf(fila)
                Return numeroDeFila
            End If
        Next
        Return Nothing

    End Function


    Sub InsertarDatosEnFilaEspecifica(ByRef tabla As DataTable, ByVal filaIndex As Integer, ByVal indicesColumnas As Integer, ParamArray valores() As Object)

        For i As Integer = 0 To valores.Length - 1
            Dim columnaIndex As Integer = valores.Length
            If columnaIndex >= 0 AndAlso columnaIndex < tabla.Columns.Count Then
                tabla.Rows(filaIndex)(indicesColumnas) = valores(i)
                indicesColumnas += 1
            Else
            End If
        Next

    End Sub

    Protected Sub gvUnidades_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvUnidades.SelectedIndexChanged
        With Me
            Try
                .pnlFacturas.Visible = true
            Catch ex As Exception

            End Try
        End With
    End Sub

#End Region

#Region "Facturas"

    Public Sub llenargvFacturasProveedor()

        With Me
            Try


                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaFacturas As New SqlDataAdapter
                Dim dsFacturas As New DataSet
                .gvFacturasProveedor.DataSource = dsFacturas

                sdaFacturas.SelectCommand = New SqlCommand("SP_C_dt_factura_Unidades @rfcProveedor, @nombreEmpresa, @idUsuario, @fecha, @tipo ", ConexionBD)

                sdaFacturas.SelectCommand.Parameters.AddWithValue("@rfcProv", .lblRFCArrendadora.Text)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@nombreEmpresa", .lblEmpresa.Text)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@fecha", Date.Now)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@tipo", "INGRESO")
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))

                ConexionBD.Open()
                sdaFacturas.Fill(dsFacturas)
                .gvFacturasProveedor.DataBind()
                ConexionBD.Close()
                sdaFacturas.Dispose()
                dsFacturas.Dispose()
                .gvFacturasProveedor.SelectedIndex = -1

                If .gvFacturasProveedor.Rows.Count() = 0 Then
                    .litError.Text = "No existe registros para el proveedor"
                    .ibtnAgregarFactura.ImageUrl = "images\Add_i2.png"
                    .ibtnAgregarFactura.Enabled = False
                Else
                    .ibtnAgregarFactura.Enabled = True
                End If


            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub



    Protected Sub gvFacturasProveedor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvFacturasProveedor.SelectedIndexChanged
        With Me
            Try
                .ibtnAgregarFactura.Enabled = True
                .ibtnAgregarFactura.ImageUrl = "images\Add.png"
            Catch ex As Exception

            End Try
        End With
    End Sub
    Protected Sub ibtnAltaProveedor_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAgregarFactura.Click
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD

                Dim fecha As DateTime
                fecha = Date.Now
                'Insertar facturas del proveedor
                Dim sdaSolicitud As New SqlDataAdapter
                Dim dsSolicitud As New DataSet
                Dim query As String

                query = "EXEC SP_I_dt_comprobacion_anticipo @id_ms_comp_anticipo, @id_dt_factura, @fecha_emision, @uuid, @serie, @folio, @lugar_exp, @forma_pago, @moneda, @subtotal, @importe, @id_usr_carga"
                sdaSolicitud.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@serie_unidad", (.gvUnidades.Rows(.gvUnidades.SelectedIndex).Cells(0).Text.ToString))
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_dt_factura", (.gvFacturasProveedor.DataKeys(gvFacturasProveedor.SelectedIndex).Values("id_dt_factura")))
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@fecha_emision", CDate(.gvFacturasProveedor.Rows(.gvFacturasProveedor.SelectedIndex).Cells(1).Text.ToString))
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@uuid", (.gvFacturasProveedor.Rows(.gvFacturasProveedor.SelectedIndex).Cells(2).Text.ToString))
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@serie", (.gvFacturasProveedor.Rows(.gvFacturasProveedor.SelectedIndex).Cells(3).Text.ToString))
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@folio", (.gvFacturasProveedor.Rows(.gvFacturasProveedor.SelectedIndex).Cells(4).Text.ToString))
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@lugar_exp", (.gvFacturasProveedor.Rows(.gvFacturasProveedor.SelectedIndex).Cells(5).Text.ToString))
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@forma_pago", (.gvFacturasProveedor.Rows(.gvFacturasProveedor.SelectedIndex).Cells(6).Text.ToString))
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@moneda", (.gvFacturasProveedor.Rows(.gvFacturasProveedor.SelectedIndex).Cells(7).Text.ToString))
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@subtotal", (.gvFacturasProveedor.Rows(.gvFacturasProveedor.SelectedIndex).Cells(8).Text.ToString))
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@importe", (.gvFacturasProveedor.Rows(.gvFacturasProveedor.SelectedIndex).Cells(9).Text.ToString))
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_usr_carga", Val(._txtIdUsuario.Text))

                ConexionBD.Open()
                sdaSolicitud.Fill(dsSolicitud)
                ConexionBD.Close()
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

#End Region

End Class
