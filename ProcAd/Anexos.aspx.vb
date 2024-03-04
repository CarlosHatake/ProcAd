Public Class Anexos
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                    _txtIdUsuario.Text = Session("id_usuario")

                    limpiarPantalla()
                    lblFolio.Text = "2"
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

    Public Sub limpiarPantalla()
        Try
            pnlCargarArchivo.Visible = False
            pnlCargaManual.Visible = False
            cbTipoCarga.Items(0).Selected = False
            cbTipoCarga.Items(1).Selected = False
            btnGuardarArchivo.Enabled = False
            'btnGenerarPlantilla.Visible = False
            hlPlantilla.Visible = False
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Public Sub ocultarCampos()
        Try
            lbl_cargaArchivo.Visible = False
            fuArchivo.Visible = False
            btnValidar.Visible = False
            btnGenerarPlantilla.Visible = False
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Public Sub valores(Valor)
        Try
            lbl_Registrados.Visible = Valor
            lblRegistrados.Visible = Valor
            lbl_Omitidos.Visible = Valor
            lblOmitidos.Visible = Valor
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub
    Function validarNombreColumna(nombreMes As String, numeroMes As Integer)
        Dim bandera As Boolean = False

        nombreMes = nombreMes.Replace("(", "").Replace(")", "").Trim()
        Dim nuevoM As String() = nombreMes.Split("-"c, " "c)


        If nuevoM(3) = "Enero" And numeroMes = 0 Then
            bandera = True
        End If
        If nuevoM(3) = "Febrero" And numeroMes = 1 Then
            bandera = True
        End If
        If nuevoM(3) = "Marzo" And numeroMes = 2 Then
            bandera = True
        End If
        If nuevoM(3) = "Abril" And numeroMes = 3 Then
            bandera = True
        End If
        If nuevoM(3) = "Mayo" And numeroMes = 4 Then
            bandera = True
        End If
        If nuevoM(3) = "Junio" And numeroMes = 5 Then
            bandera = True
        End If
        If nuevoM(3) = "Julio" And numeroMes = 6 Then
            bandera = True
        End If
        If nuevoM(3) = "Agosto" And numeroMes = 7 Then
            bandera = True
        End If
        If nuevoM(3) = "Septiembre" And numeroMes = 8 Then
            bandera = True
        End If
        If nuevoM(3) = "Octubre" And numeroMes = 9 Then
            bandera = True
        End If
        If nuevoM(3) = "Noviembre" And numeroMes = 10 Then
            bandera = True
        End If
        If nuevoM(3) = "Diciembre" And numeroMes = 11 Then
            bandera = True
        End If

        Return bandera


    End Function
    Function nombreColumna(nombreMes As String, año As Integer)
        Dim mensaje As String = ""

        Select Case nombreMes
            Case "mes1"
                mensaje = "( " + CStr(año) + " - Enero )"
            Case "mes2"
                mensaje = "( " + CStr(año) + " - Febrero )"
            Case "mes3"
                mensaje = "( " + CStr(año) + " - Marzo )"
            Case "mes4"
                mensaje = "( " + CStr(año) + " - Abril )"
            Case "mes5"
                mensaje = "( " + CStr(año) + " - Mayo )"
            Case "mes6"
                mensaje = "( " + CStr(año) + " - Junio )"
            Case "mes7"
                mensaje = "( " + CStr(año) + " - Julio )"
            Case "mes8"
                mensaje = "( " + CStr(año) + " - Agosto )"
            Case "mes9"
                mensaje = "( " + CStr(año) + " - Septiembre )"
            Case "mes10"
                mensaje = "( " + CStr(año) + " - Octubre )"
            Case "mes11"
                mensaje = "( " + CStr(año) + " - Noviembre )"
            Case "mes12"
                mensaje = "( " + CStr(año) + " - Diciembre )"
        End Select

        Return mensaje
    End Function
#End Region

#Region "Controles"
    Protected Sub cbTipoCarga_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTipoCarga.SelectedIndexChanged
        Try
            If cbTipoCarga.Items(0).Selected = True Then
                pnlCargarArchivo.Visible = True
                valores(False)
            Else
                pnlCargaManual.Visible = False
            End If
            cbTipoCarga.Visible = False
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

#End Region

#Region "Botones"
    Protected Sub btnValidar_Click(sender As Object, e As EventArgs) Handles btnValidar.Click
        Try
            ' Ruta local
            ' Dim rutaArchivo As String = "C:\SiSAC - CargaMasiva\"
            '' Ruta en Atenea
            Dim rutaArchivo As String = "Anexos\" 'Ruta en que se almacenará el archivo
            Dim sFileExtAr As String = System.IO.Path.GetExtension(fuArchivo.PostedFile.FileName)
            Dim sFileNameAr As String = System.IO.Path.GetFileName(fuArchivo.PostedFile.FileName)
            If sFileExtAr = ".xlsx" Then
                'Guarda archivo'
                fuArchivo.PostedFile.SaveAs(Server.MapPath("Anexos\" + sFileNameAr))
                _txtNombreArchivo.Text = sFileNameAr
                Dim dtRegistros As DataTable
                Dim workbook As Workbook = New Workbook()
                workbook.LoadFromFile(Server.MapPath("Anexos\" + sFileNameAr))

                Dim sheet As Worksheet = workbook.Worksheets(0)
                dtRegistros = sheet.ExportDataTable(sheet.AllocatedRange, True, True)

                Dim i As Integer = 0
                Dim iFin As Integer = dtRegistros.Rows.Count - 1
                Dim Omitidos As Integer = 0
                While i <= iFin
                    If dtRegistros.Rows(i).Item(0).ToString = "" Or dtRegistros.Rows(i).Item(9).ToString = "" Or dtRegistros.Rows(i).Item(10).ToString = "" Then
                        dtRegistros.Rows(i).Delete()
                        iFin = iFin - 1
                        Omitidos = Omitidos + 1
                    Else
                        i = i + 1
                    End If
                End While

                gvRegistros.DataSource = dtRegistros
                gvRegistros.DataBind()

                If gvRegistros.Rows.Count > 0 Then
                    ocultarCampos()
                    valores(True)
                    btnGuardarArchivo.Enabled = True
                    lblRegistrados.Text = gvRegistros.Rows.Count()
                    lblOmitidos.Text = Omitidos
                Else
                    litError.Text = ""
                End If
            Else
                litError.Text = "Los formatos de los archivos no corresponden"
            End If
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnGuardarArchivo_Click(sender As Object, e As EventArgs) Handles btnGuardarArchivo.Click
        Try
            Dim dtRegistros As DataTable
            Dim workbook As Workbook = New Workbook()
            workbook.LoadFromFile(Server.MapPath("Anexos\" + _txtNombreArchivo.Text))

            Dim sheet As Worksheet = workbook.Worksheets(0)
            dtRegistros = sheet.ExportDataTable(sheet.AllocatedRange, True, True)

            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
            SCMValores.Connection = ConexionBD
            SCMValores.CommandText = ""
            SCMValores.Parameters.Clear()

            Dim id_carga As Integer
            SCMValores.CommandText = "select  top 1 ISNULL(id_carga, 0) from ms_contrato_arrenda order by id_carga desc "
            ConexionBD.Open()
            id_carga = SCMValores.ExecuteScalar()
            ConexionBD.Close()

            Dim i As Integer = 0
            Dim iFin As Integer = dtRegistros.Rows.Count - 1
            Dim consulta As Integer = 0
            Dim query As String = ""
            While i <= iFin
                Try
                    If dtRegistros.Rows(i).Item(0).ToString = "" Or dtRegistros.Rows(i).Item(9).ToString() = "" Or dtRegistros.Rows(i).Item(10).ToString() = "" Then
                        ' dtRegistros.Rows(i).Delete()
                        i = i + 1
                    Else
                        SCMValores.Parameters.Clear()
                        query = "Insert into ms_contrato_arrenda (no_contrato, empresa, arrendadora, fecha_inicio, fecha_fin, plazo_meses, tipo_arrendamiento, rfc_arrendadora, id_usuario_carga, fecha_usuario_carga, id_carga, inversion) " +
                                    " values (@no_contrato, @empresa, @arrendadora, @fecha_inicio, @fecha_fin, @plazo_meses, @tipo_arrendamiento, @rfc_arrendadora, @id_usuario_carga, GETDATE(), @id_carga, @inversion)"
                        SCMValores.Parameters.AddWithValue("@no_contrato", dtRegistros.Rows(i).Item(1).ToString)
                        SCMValores.Parameters.AddWithValue("@empresa", dtRegistros.Rows(i).Item(0).ToString)
                        SCMValores.Parameters.AddWithValue("@arrendadora", dtRegistros.Rows(i).Item(3).ToString)
                        SCMValores.Parameters.AddWithValue("@fecha_inicio", dtRegistros.Rows(i).Item(9).ToString)
                        SCMValores.Parameters.AddWithValue("@fecha_fin", dtRegistros.Rows(i).Item(10).ToString)
                        SCMValores.Parameters.AddWithValue("@plazo_meses", dtRegistros.Rows(i).Item(8).ToString)
                        SCMValores.Parameters.AddWithValue("@tipo_arrendamiento", dtRegistros.Rows(i).Item(5).ToString)
                        SCMValores.Parameters.AddWithValue("@rfc_arrendadora", DBNull.Value)
                        SCMValores.Parameters.AddWithValue("@id_usuario_carga", _txtIdUsuario.Text)
                        SCMValores.Parameters.AddWithValue("@id_carga", id_carga + 1)
                        SCMValores.Parameters.AddWithValue("@inversion", Val(dtRegistros.Rows(i).Item(11).ToString()))


                        SCMValores.CommandText = "Select count(*) from ms_contrato_arrenda where no_contrato = @no_contrato "
                        ConexionBD.Open()
                        consulta = SCMValores.ExecuteScalar()
                        ConexionBD.Close()
                        If consulta = 0 Then
                            ConexionBD.Open()
                            SCMValores.CommandText = query
                            SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            Dim id_ms_contrato As Integer
                            SCMValores.CommandText = "select top 1 id_ms_contrato FROM ms_contrato_arrenda order by id_ms_contrato desc "
                            ConexionBD.Open()
                            id_ms_contrato = SCMValores.ExecuteScalar()
                            ConexionBD.Close()


                            Dim id_ms_anexo As Integer = 0
                            SCMValores.Parameters.AddWithValue("@id_ms_contrato", id_ms_contrato)
                            If dtRegistros.Rows(i).Item(2).ToString <> "" Then
                                SCMValores.CommandText = "select count(*) from ms_anexo_arrenda where id_ms_contrato = @id_ms_contrato and anexo = @anexo "
                                SCMValores.Parameters.AddWithValue("@anexo", dtRegistros.Rows(i).Item(2).ToString)
                                ConexionBD.Open()
                                consulta = SCMValores.ExecuteScalar()
                                ConexionBD.Close()

                                If consulta = 0 Then
                                    SCMValores.CommandText = "Insert into ms_anexo_arrenda (empresa, id_ms_contrato, arrendadora, anexo, fecha_inicio, fecha_fin, tipo_arrendamiento, id_carga, id_usuario ) " +
                                " values (@empresa, @id_ms_contrato, @arrendadora, @anexo, @fecha_inicio, @fecha_fin, @tipo_arrendamiento, @id_carga, @id_usuario_carga ) "
                                    ConexionBD.Open()
                                    SCMValores.ExecuteScalar()
                                    ConexionBD.Close()

                                End If


                                SCMValores.CommandText = "Select top 1 id_ms_anexo from ms_anexo_arrenda  where id_ms_contrato = @id_ms_contrato order by id_ms_anexo desc "
                                ConexionBD.Open()
                                id_ms_anexo = SCMValores.ExecuteScalar()
                                ConexionBD.Close()

                                SCMValores.CommandText = "Select count(*) from ms_equipo_arrenda where id_ms_anexo = @id_ms_anexo and serie = @serie"
                                SCMValores.Parameters.AddWithValue("@id_ms_anexo", id_ms_anexo)
                                SCMValores.Parameters.AddWithValue("@serie", dtRegistros.Rows(i).Item(4).ToString())
                                ConexionBD.Open()
                                consulta = SCMValores.ExecuteScalar()
                                ConexionBD.Close()
                            Else
                                SCMValores.CommandText = "Select count(*) from ms_equipo_arrenda where id_ms_contrato = @id_ms_contrato and serie = @serie"
                                SCMValores.Parameters.AddWithValue("@id_ms_anexo", DBNull.Value)
                                SCMValores.Parameters.AddWithValue("@serie", dtRegistros.Rows(i).Item(4).ToString())
                                ConexionBD.Open()
                                consulta = SCMValores.ExecuteScalar()
                                ConexionBD.Close()
                            End If



                            If consulta = 0 Then
                                SCMValores.CommandText = " Insert into ms_equipo_arrenda (serie, provision, tipo_arrendamiento, economico, nombre_division, tipo_unidad, nombre_zona, fecha_actual, fecha_inicio, fecha_fin, id_ms_anexo, id_ms_contrato, estatus, centro_costo, id_carga, id_usuario ) " +
                                " values (@serie, @provision, @tipo_arrendamiento, @economico, @nombre_division, @tipo_unidad, @nombre_zona, GETDATE() , @fecha_inicio, @fecha_fin, @id_ms_anexo , @id_ms_contrato,  @estatus , @centro_costo, @id_carga, @id_usuario_carga) "
                                'SCMValores.Parameters.AddWithValue("@serie", dtRegistros.Rows(i).Item(4).ToString)
                                SCMValores.Parameters.AddWithValue("@provision", DBNull.Value)
                                SCMValores.Parameters.AddWithValue("@economico", DBNull.Value)
                                SCMValores.Parameters.AddWithValue("@nombre_division", DBNull.Value)
                                SCMValores.Parameters.AddWithValue("@tipo_unidad", DBNull.Value)
                                SCMValores.Parameters.AddWithValue("@nombre_zona", DBNull.Value)
                                SCMValores.Parameters.AddWithValue("@estatus", DBNull.Value)
                                SCMValores.Parameters.AddWithValue("@centro_costo", DBNull.Value)
                                ConexionBD.Open()
                                SCMValores.ExecuteScalar()
                                ConexionBD.Close()

                                Dim id_ms_equipo As Integer = 0
                                If id_ms_anexo <> 0 Then
                                    SCMValores.CommandText = "select top 1 id_ms_equipo from ms_equipo_arrenda where id_ms_anexo = @id_ms_anexo order by id_ms_equipo desc "
                                    ConexionBD.Open()
                                    id_ms_equipo = SCMValores.ExecuteScalar
                                    ConexionBD.Close()
                                Else
                                    SCMValores.CommandText = "select top 1 id_ms_equipo from ms_equipo_arrenda where id_ms_contrato = @id_ms_contrato order by id_ms_equipo desc "
                                    ConexionBD.Open()
                                    id_ms_equipo = SCMValores.ExecuteScalar
                                    ConexionBD.Close()
                                End If


                                Dim casillaMes As Integer = 14
                                Dim año As Integer = 0
                                Dim añoA As Integer = 0
                                Dim contador As Integer = 0
                                Dim suma As Double = 0
                                Dim mes1 = 0
                                Dim mes2 = 0
                                Dim mes3 = 0
                                Dim mes4 = 0
                                Dim mes5 = 0
                                Dim mes6 = 0
                                Dim mes7 = 0
                                Dim mes8 = 0
                                Dim mes9 = 0
                                Dim mes10 = 0
                                Dim mes11 = 0
                                Dim mes12 = 0
                                While casillaMes <= dtRegistros.Columns.Count()
                                    If dtRegistros.Columns(casillaMes).ColumnName().ToString.Substring(0, 6) <> "Column" Then
                                        año = Val(dtRegistros.Columns(casillaMes).ColumnName().ToString().Substring(6, 5))
                                        suma = mes1 + mes2 + mes3 + mes4 + mes5 + mes6 + mes7 + mes8 + mes9 + mes10 + mes11 + mes12

                                            If (año <> añoA) And (contador <> 0) And (suma <> 0) Then
                                                SCMValores.Parameters.Clear()
                                                SCMValores.CommandText = "Insert into ms_equipo_importes (id_ms_equipo_arrenda, id_ms_anexo, id_ms_contrato, año, mes1, mes2, mes3, mes4, mes5, mes6, mes7, mes8, mes9, mes10, mes11, mes12, estatus_arrenda, fecha_termino, id_carga, id_usuario) " +
                                                    " values (@id_ms_equipo_arrenda, @id_ms_anexo, @id_ms_contrato, @año, @mes1, @mes2, @mes3, @mes4, @mes5, @mes6, @mes7, @mes8, @mes9, @mes10, @mes11, @mes12,  @estatus_arrenda, @fecha_termino, @id_carga, @id_usuario) "
                                                SCMValores.Parameters.AddWithValue("@id_ms_equipo_arrenda", id_ms_equipo)
                                                If id_ms_anexo = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@id_ms_anexo", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@id_ms_anexo", id_ms_anexo)
                                                End If
                                                SCMValores.Parameters.AddWithValue("@id_ms_contrato", id_ms_contrato)
                                                SCMValores.Parameters.AddWithValue("@año", añoA)
                                                If mes1 = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@mes1", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@mes1", mes1)
                                                End If
                                                If mes2 = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@mes2", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@mes2", mes2)
                                                End If
                                                If mes3 = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@mes3", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@mes3", mes3)
                                                End If
                                                If mes4 = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@mes4", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@mes4", mes4)
                                                End If
                                                If mes5 = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@mes5", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@mes5", mes5)
                                                End If
                                                If mes6 = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@mes6", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@mes6", mes6)
                                                End If
                                                If mes7 = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@mes7", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@mes7", mes7)
                                                End If
                                                If mes8 = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@mes8", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@mes8", mes8)
                                                End If
                                                If mes9 = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@mes9", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@mes9", mes9)
                                                End If
                                                If mes10 = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@mes10", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@mes10", mes10)
                                                End If
                                                If mes11 = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@mes11", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@mes11", mes11)
                                                End If
                                                If mes12 = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@mes12", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@mes12", mes12)
                                                End If
                                                ''SCMValores.Parameters.AddWithValue("@mes1", dtRegistros.Columns(casillaMes).ColumnName().ToString().Substring(3, 2))
                                                ''SCMValores.Parameters.AddWithValue("monto", Val(dtRegistros.Rows(i).Item(casillaMes).ToString()))
                                                SCMValores.Parameters.AddWithValue("@estatus_arrenda", DBNull.Value)
                                                SCMValores.Parameters.AddWithValue("@fecha_termino", DBNull.Value)
                                                SCMValores.Parameters.AddWithValue("@id_carga", id_carga + 1)
                                                SCMValores.Parameters.AddWithValue("@id_usuario", Val(_txtIdUsuario.Text))
                                                ConexionBD.Open()
                                                SCMValores.ExecuteScalar()
                                                ConexionBD.Close()
                                                mes1 = 0
                                                mes2 = 0
                                                mes3 = 0
                                                mes4 = 0
                                                mes5 = 0
                                                mes6 = 0
                                                mes7 = 0
                                                mes8 = 0
                                                mes9 = 0
                                                mes10 = 0
                                                mes11 = 0
                                                mes12 = 0
                                                contador = 0
                                                casillaMes = casillaMes - 1
                                            Else
                                                Select Case Val(dtRegistros.Columns(casillaMes).ColumnName().ToString().Substring(3, 2))
                                                    Case 1
                                                        mes1 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                    Case 2
                                                        mes2 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                    Case 3
                                                        mes3 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                    Case 4
                                                        mes4 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                    Case 5
                                                        mes5 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                    Case 6
                                                        mes6 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                    Case 7
                                                        mes7 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                    Case 8
                                                        mes8 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                    Case 9
                                                        mes9 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                    Case 10
                                                        mes10 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                    Case 11
                                                        mes11 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                    Case 12
                                                        mes12 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                End Select
                                            End If
                                            casillaMes = casillaMes + 1
                                            contador = contador + 1
                                            añoA = año

                                    Else
                                        casillaMes = casillaMes + dtRegistros.Columns().Count + 1

                                    End If

                                End While

                            End If


                        Else
                            'Ya esta el contrato'
                            'Validar si ya existe el anexo'
                            Dim id_ms_contrato As Integer
                            SCMValores.CommandText = "select top 1 id_ms_contrato FROM ms_contrato_arrenda where no_contrato = @no_contrato "
                            ConexionBD.Open()
                            id_ms_contrato = SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            'VALIDAR SI EXISTE ANEXO
                            SCMValores.Parameters.AddWithValue("@id_ms_contrato", id_ms_contrato)
                            If dtRegistros.Rows(i).Item(2).ToString <> "" Then
                                SCMValores.CommandText = "select count(*) from ms_anexo_arrenda where id_ms_contrato = @id_ms_contrato and anexo = @anexo "
                                SCMValores.Parameters.AddWithValue("@anexo", dtRegistros.Rows(i).Item(2).ToString)
                                ConexionBD.Open()
                                consulta = SCMValores.ExecuteScalar()
                                ConexionBD.Close()
                            Else
                                consulta = 1
                                SCMValores.Parameters.AddWithValue("@anexo", DBNull.Value)
                            End If


                            If consulta = 0 Then
                                'ES UN NUEVO ANEXO'
                                SCMValores.CommandText = "Insert into ms_anexo_arrenda (empresa, id_ms_contrato, arrendadora, anexo, fecha_inicio, fecha_fin, tipo_arrendamiento, id_carga, id_usuario ) " +
                             " values (@empresa, @id_ms_contrato, @arrendadora, @anexo, @fecha_inicio, @fecha_fin, @tipo_arrendamiento, @id_carga, @id_usuario_carga ) "
                                ConexionBD.Open()
                                SCMValores.ExecuteScalar()
                                ConexionBD.Close()

                                Dim id_ms_anexo As Integer
                                SCMValores.CommandText = "Select top 1 id_ms_anexo from ms_anexo_arrenda  where id_ms_contrato = @id_ms_contrato order by id_ms_anexo desc "
                                ConexionBD.Open()
                                id_ms_anexo = SCMValores.ExecuteScalar()
                                ConexionBD.Close()

                                'Es nuevo el equipo arrenda'
                                SCMValores.CommandText = "Insert into ms_equipo_arrenda (serie, provision, tipo_arrendamiento, economico, nombre_division, tipo_unidad, nombre_zona, fecha_actual, fecha_inicio, fecha_fin, id_ms_anexo, id_ms_contrato, estatus, centro_costo, id_carga, id_usuario) " +
                            " values (@serie, @provision, @tipo_arrendamiento, @economico, @nombre_division, @tipo_unidad, @nombre_zona, GETDATE() , @fecha_inicio, @fecha_fin, @id_ms_anexo, @id_ms_contrato, @estatus , @centro_costo, @id_carga, @id_usuario_carga)"
                                SCMValores.Parameters.AddWithValue("@id_ms_anexo", id_ms_anexo)
                                SCMValores.Parameters.AddWithValue("@serie", dtRegistros.Rows(i).Item(4).ToString)
                                SCMValores.Parameters.AddWithValue("@provision", DBNull.Value)
                                SCMValores.Parameters.AddWithValue("@economico", DBNull.Value)
                                SCMValores.Parameters.AddWithValue("@nombre_division", DBNull.Value)
                                SCMValores.Parameters.AddWithValue("@tipo_unidad", DBNull.Value)
                                SCMValores.Parameters.AddWithValue("@nombre_zona", DBNull.Value)
                                SCMValores.Parameters.AddWithValue("@estatus", DBNull.Value)
                                SCMValores.Parameters.AddWithValue("@centro_costo", DBNull.Value)
                                ConexionBD.Open()
                                SCMValores.ExecuteScalar()
                                ConexionBD.Close()

                                Dim id_ms_equipo As Integer = 0
                                SCMValores.CommandText = "select top 1 id_ms_equipo from ms_equipo_arrenda where id_ms_anexo = @id_ms_anexo order by id_ms_equipo desc "
                                ConexionBD.Open()
                                id_ms_equipo = SCMValores.ExecuteScalar
                                ConexionBD.Close()


                                Dim casillaMes As Integer = 14
                                Dim año As Integer = 0
                                Dim añoA As Integer = 0
                                Dim contador As Integer = 0
                                Dim suma As Double = 0
                                Dim mes1 = 0
                                Dim mes2 = 0
                                Dim mes3 = 0
                                Dim mes4 = 0
                                Dim mes5 = 0
                                Dim mes6 = 0
                                Dim mes7 = 0
                                Dim mes8 = 0
                                Dim mes9 = 0
                                Dim mes10 = 0
                                Dim mes11 = 0
                                Dim mes12 = 0
                                While casillaMes <= dtRegistros.Columns.Count()
                                    If dtRegistros.Columns(casillaMes).ColumnName().ToString.Substring(0, 6) <> "Column" Then
                                        año = Val(dtRegistros.Columns(casillaMes).ColumnName().ToString().Substring(6, 5))
                                        suma = mes1 + mes2 + mes3 + mes4 + mes5 + mes6 + mes7 + mes8 + mes9 + mes10 + mes11 + mes12

                                        If (año <> añoA) And (contador <> 0) And (suma <> 0) Then
                                            SCMValores.Parameters.Clear()
                                            SCMValores.CommandText = "Insert into ms_equipo_importes (id_ms_equipo_arrenda, id_ms_anexo, id_ms_contrato, año, mes1, mes2, mes3, mes4, mes5, mes6, mes7, mes8, mes9, mes10, mes11, mes12, estatus_arrenda, fecha_termino, id_carga, id_usuario) " +
                                                    " values (@id_ms_equipo_arrenda, @id_ms_anexo, @id_ms_contrato, @año, @mes1, @mes2, @mes3, @mes4, @mes5, @mes6, @mes7, @mes8, @mes9, @mes10, @mes11, @mes12,  @estatus_arrenda, @fecha_termino, @id_carga, @id_usuario) "
                                            SCMValores.Parameters.AddWithValue("@id_ms_equipo_arrenda", id_ms_equipo)
                                            If id_ms_anexo = 0 Then
                                                SCMValores.Parameters.AddWithValue("@id_ms_anexo", DBNull.Value)
                                            Else
                                                SCMValores.Parameters.AddWithValue("@id_ms_anexo", id_ms_anexo)
                                            End If
                                            SCMValores.Parameters.AddWithValue("@id_ms_contrato", id_ms_contrato)
                                            SCMValores.Parameters.AddWithValue("@año", añoA)
                                            If mes1 = 0 Then
                                                SCMValores.Parameters.AddWithValue("@mes1", DBNull.Value)
                                            Else
                                                SCMValores.Parameters.AddWithValue("@mes1", mes1)
                                            End If
                                            If mes2 = 0 Then
                                                SCMValores.Parameters.AddWithValue("@mes2", DBNull.Value)
                                            Else
                                                SCMValores.Parameters.AddWithValue("@mes2", mes2)
                                            End If
                                            If mes3 = 0 Then
                                                SCMValores.Parameters.AddWithValue("@mes3", DBNull.Value)
                                            Else
                                                SCMValores.Parameters.AddWithValue("@mes3", mes3)
                                            End If
                                            If mes4 = 0 Then
                                                SCMValores.Parameters.AddWithValue("@mes4", DBNull.Value)
                                            Else
                                                SCMValores.Parameters.AddWithValue("@mes4", mes4)
                                            End If
                                            If mes5 = 0 Then
                                                SCMValores.Parameters.AddWithValue("@mes5", DBNull.Value)
                                            Else
                                                SCMValores.Parameters.AddWithValue("@mes5", mes5)
                                            End If
                                            If mes6 = 0 Then
                                                SCMValores.Parameters.AddWithValue("@mes6", DBNull.Value)
                                            Else
                                                SCMValores.Parameters.AddWithValue("@mes6", mes6)
                                            End If
                                            If mes7 = 0 Then
                                                SCMValores.Parameters.AddWithValue("@mes7", DBNull.Value)
                                            Else
                                                SCMValores.Parameters.AddWithValue("@mes7", mes7)
                                            End If
                                            If mes8 = 0 Then
                                                SCMValores.Parameters.AddWithValue("@mes8", DBNull.Value)
                                            Else
                                                SCMValores.Parameters.AddWithValue("@mes8", mes8)
                                            End If
                                            If mes9 = 0 Then
                                                SCMValores.Parameters.AddWithValue("@mes9", DBNull.Value)
                                            Else
                                                SCMValores.Parameters.AddWithValue("@mes9", mes9)
                                            End If
                                            If mes10 = 0 Then
                                                SCMValores.Parameters.AddWithValue("@mes10", DBNull.Value)
                                            Else
                                                SCMValores.Parameters.AddWithValue("@mes10", mes10)
                                            End If
                                            If mes11 = 0 Then
                                                SCMValores.Parameters.AddWithValue("@mes11", DBNull.Value)
                                            Else
                                                SCMValores.Parameters.AddWithValue("@mes11", mes11)
                                            End If
                                            If mes12 = 0 Then
                                                SCMValores.Parameters.AddWithValue("@mes12", DBNull.Value)
                                            Else
                                                SCMValores.Parameters.AddWithValue("@mes12", mes12)
                                            End If
                                            ''SCMValores.Parameters.AddWithValue("@mes1", dtRegistros.Columns(casillaMes).ColumnName().ToString().Substring(3, 2))
                                            ''SCMValores.Parameters.AddWithValue("monto", Val(dtRegistros.Rows(i).Item(casillaMes).ToString()))
                                            SCMValores.Parameters.AddWithValue("@estatus_arrenda", DBNull.Value)
                                            SCMValores.Parameters.AddWithValue("@fecha_termino", DBNull.Value)
                                            SCMValores.Parameters.AddWithValue("@id_carga", id_carga + 1)
                                            SCMValores.Parameters.AddWithValue("@id_usuario", Val(_txtIdUsuario.Text))
                                            ConexionBD.Open()
                                            SCMValores.ExecuteScalar()
                                            ConexionBD.Close()
                                            mes1 = 0
                                            mes2 = 0
                                            mes3 = 0
                                            mes4 = 0
                                            mes5 = 0
                                            mes6 = 0
                                            mes7 = 0
                                            mes8 = 0
                                            mes9 = 0
                                            mes10 = 0
                                            mes11 = 0
                                            mes12 = 0
                                            contador = 0
                                            casillaMes = casillaMes - 1
                                        Else
                                            Select Case Val(dtRegistros.Columns(casillaMes).ColumnName().ToString().Substring(3, 2))
                                                Case 1
                                                    mes1 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                Case 2
                                                    mes2 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                Case 3
                                                    mes3 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                Case 4
                                                    mes4 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                Case 5
                                                    mes5 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                Case 6
                                                    mes6 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                Case 7
                                                    mes7 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                Case 8
                                                    mes8 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                Case 9
                                                    mes9 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                Case 10
                                                    mes10 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                Case 11
                                                    mes11 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                Case 12
                                                    mes12 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                            End Select
                                        End If
                                        casillaMes = casillaMes + 1
                                        contador = contador + 1
                                        añoA = año

                                    Else
                                        casillaMes = casillaMes + dtRegistros.Columns().Count + 1

                                    End If

                                End While

                            Else
                                'Ya existe este anexo'
                                'Validar si un equipo con la misma serie en el anexo'
                                Dim id_ms_anexo As Integer = 0
                                If dtRegistros.Rows(i).Item(2).ToString <> "" Then
                                    SCMValores.CommandText = "Select id_ms_anexo from ms_anexo_arrenda  where id_ms_contrato = @id_ms_contrato and anexo = @anexo"
                                    ConexionBD.Open()
                                    id_ms_anexo = SCMValores.ExecuteScalar()
                                    ConexionBD.Close()

                                    SCMValores.CommandText = "Select count(*) from ms_equipo_arrenda where id_ms_anexo = @id_ms_anexo and serie = @serie"
                                    SCMValores.Parameters.AddWithValue("@id_ms_anexo", id_ms_anexo)
                                    SCMValores.Parameters.AddWithValue("@serie", dtRegistros.Rows(i).Item(4).ToString())
                                    ConexionBD.Open()
                                    consulta = SCMValores.ExecuteScalar()
                                    ConexionBD.Close()
                                Else
                                    SCMValores.CommandText = "Select count(*) from ms_equipo_arrenda where id_ms_anexo is null and serie = @serie"
                                    SCMValores.Parameters.AddWithValue("@id_ms_anexo", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@serie", dtRegistros.Rows(i).Item(4).ToString())
                                    ConexionBD.Open()
                                    consulta = SCMValores.ExecuteScalar()
                                    ConexionBD.Close()
                                End If

                                If consulta = 0 Then
                                    'Aqui iria lo de id_usr'


                                    'Es nuevo el equipo arrenda'
                                    SCMValores.CommandText = "Insert into ms_equipo_arrenda (serie, provision, tipo_arrendamiento, economico, nombre_division, tipo_unidad, nombre_zona, fecha_actual, fecha_inicio, fecha_fin, id_ms_anexo, id_ms_contrato, estatus, centro_costo, id_carga, id_usuario) " +
                                " values (@serie, @provision, @tipo_arrendamiento, @economico, @nombre_division, @tipo_unidad, @nombre_zona, GETDATE() , @fecha_inicio, @fecha_fin, @id_ms_anexo, @id_ms_contrato, @estatus , @centro_costo, @id_carga, @id_usuario_carga)"
                                    'SCMValores.Parameters.AddWithValue("@serie", dtRegistros.Rows(i).Item(4).ToString)
                                    SCMValores.Parameters.AddWithValue("@provision", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@economico", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@nombre_division", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@tipo_unidad", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@nombre_zona", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@estatus", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@centro_costo", DBNull.Value)
                                    ConexionBD.Open()
                                    SCMValores.ExecuteScalar()
                                    ConexionBD.Close()

                                    Dim id_ms_equipo As Integer = 0
                                    If id_ms_anexo <> 0 Then
                                        SCMValores.CommandText = "select top 1 id_ms_equipo from ms_equipo_arrenda where id_ms_anexo = @id_ms_anexo order by id_ms_equipo desc "
                                        ConexionBD.Open()
                                        id_ms_equipo = SCMValores.ExecuteScalar
                                        ConexionBD.Close()
                                    Else
                                        SCMValores.CommandText = "select top 1 id_ms_equipo from ms_equipo_arrenda where id_ms_contrato = @id_ms_contrato order by id_ms_equipo desc "
                                        ConexionBD.Open()
                                        id_ms_equipo = SCMValores.ExecuteScalar
                                        ConexionBD.Close()
                                    End If


                                    Dim casillaMes As Integer = 14
                                    Dim año As Integer = 0
                                    Dim añoA As Integer = 0
                                    Dim contador As Integer = 0
                                    Dim suma As Double = 0
                                    Dim mes1 = 0
                                    Dim mes2 = 0
                                    Dim mes3 = 0
                                    Dim mes4 = 0
                                    Dim mes5 = 0
                                    Dim mes6 = 0
                                    Dim mes7 = 0
                                    Dim mes8 = 0
                                    Dim mes9 = 0
                                    Dim mes10 = 0
                                    Dim mes11 = 0
                                    Dim mes12 = 0
                                    While casillaMes <= dtRegistros.Columns.Count()
                                        If dtRegistros.Columns(casillaMes).ColumnName().ToString.Substring(0, 6) <> "Column" Then
                                            año = Val(dtRegistros.Columns(casillaMes).ColumnName().ToString().Substring(6, 5))
                                            suma = mes1 + mes2 + mes3 + mes4 + mes5 + mes6 + mes7 + mes8 + mes9 + mes10 + mes11 + mes12

                                            If (año <> añoA) And (contador <> 0) And (suma <> 0) Then
                                                SCMValores.Parameters.Clear()
                                                SCMValores.CommandText = "Insert into ms_equipo_importes (id_ms_equipo_arrenda, id_ms_anexo, id_ms_contrato, año, mes1, mes2, mes3, mes4, mes5, mes6, mes7, mes8, mes9, mes10, mes11, mes12, estatus_arrenda, fecha_termino, id_carga, id_usuario) " +
                                                    " values (@id_ms_equipo_arrenda, @id_ms_anexo, @id_ms_contrato, @año, @mes1, @mes2, @mes3, @mes4, @mes5, @mes6, @mes7, @mes8, @mes9, @mes10, @mes11, @mes12,  @estatus_arrenda, @fecha_termino, @id_carga, @id_usuario) "
                                                SCMValores.Parameters.AddWithValue("@id_ms_equipo_arrenda", id_ms_equipo)
                                                If id_ms_anexo = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@id_ms_anexo", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@id_ms_anexo", id_ms_anexo)
                                                End If
                                                SCMValores.Parameters.AddWithValue("@id_ms_contrato", id_ms_contrato)
                                                SCMValores.Parameters.AddWithValue("@año", añoA)
                                                If mes1 = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@mes1", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@mes1", mes1)
                                                End If
                                                If mes2 = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@mes2", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@mes2", mes2)
                                                End If
                                                If mes3 = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@mes3", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@mes3", mes3)
                                                End If
                                                If mes4 = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@mes4", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@mes4", mes4)
                                                End If
                                                If mes5 = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@mes5", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@mes5", mes5)
                                                End If
                                                If mes6 = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@mes6", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@mes6", mes6)
                                                End If
                                                If mes7 = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@mes7", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@mes7", mes7)
                                                End If
                                                If mes8 = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@mes8", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@mes8", mes8)
                                                End If
                                                If mes9 = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@mes9", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@mes9", mes9)
                                                End If
                                                If mes10 = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@mes10", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@mes10", mes10)
                                                End If
                                                If mes11 = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@mes11", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@mes11", mes11)
                                                End If
                                                If mes12 = 0 Then
                                                    SCMValores.Parameters.AddWithValue("@mes12", DBNull.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@mes12", mes12)
                                                End If
                                                ''SCMValores.Parameters.AddWithValue("@mes1", dtRegistros.Columns(casillaMes).ColumnName().ToString().Substring(3, 2))
                                                ''SCMValores.Parameters.AddWithValue("monto", Val(dtRegistros.Rows(i).Item(casillaMes).ToString()))
                                                SCMValores.Parameters.AddWithValue("@estatus_arrenda", DBNull.Value)
                                                SCMValores.Parameters.AddWithValue("@fecha_termino", DBNull.Value)
                                                SCMValores.Parameters.AddWithValue("@id_carga", id_carga + 1)
                                                SCMValores.Parameters.AddWithValue("@id_usuario", Val(_txtIdUsuario.Text))
                                                ConexionBD.Open()
                                                SCMValores.ExecuteScalar()
                                                ConexionBD.Close()
                                                mes1 = 0
                                                mes2 = 0
                                                mes3 = 0
                                                mes4 = 0
                                                mes5 = 0
                                                mes6 = 0
                                                mes7 = 0
                                                mes8 = 0
                                                mes9 = 0
                                                mes10 = 0
                                                mes11 = 0
                                                mes12 = 0
                                                contador = 0
                                                casillaMes = casillaMes - 1
                                            Else
                                                Select Case Val(dtRegistros.Columns(casillaMes).ColumnName().ToString().Substring(3, 2))
                                                    Case 1
                                                        mes1 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                    Case 2
                                                        mes2 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                    Case 3
                                                        mes3 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                    Case 4
                                                        mes4 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                    Case 5
                                                        mes5 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                    Case 6
                                                        mes6 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                    Case 7
                                                        mes7 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                    Case 8
                                                        mes8 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                    Case 9
                                                        mes9 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                    Case 10
                                                        mes10 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                    Case 11
                                                        mes11 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                    Case 12
                                                        mes12 = Val(dtRegistros.Rows(i).Item(casillaMes).ToString())
                                                End Select
                                            End If
                                            casillaMes = casillaMes + 1
                                            contador = contador + 1
                                            añoA = año

                                        Else
                                            casillaMes = casillaMes + dtRegistros.Columns().Count + 1

                                        End If

                                    End While

                                Else
                                    'Ya existe / Ya no hay más proceso'
                                    SCMValores.Parameters.Clear()
                                End If
                            End If
                        End If

                        i = i + 1
                    End If

                Catch ex As Exception
                    litError.Text = ex.Message
                End Try
            End While

            lblFolio.Text = id_carga + 1
            btnGuardarArchivo.Visible = False
            btnGenerarPlantilla.Visible = True
            hlPlantilla.Visible = True

        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnGenerarPlantilla_Click(sender As Object, e As EventArgs) Handles btnGenerarPlantilla.Click
        Try
            '  btnGenerarPlantilla.Visible = False

            'Dim dtRegistros As DataTable
            Dim workbook As Workbook = New Workbook()
            workbook.LoadFromFile(Server.MapPath("Anexos\Plantillas\Plantilla.xlsx"))
            Dim sheet As Worksheet = workbook.Worksheets(0)
            sheet.InsertColumn(9, 90)
            sheet.Range("A50").Text = " "
            workbook.SaveToFile(Server.MapPath("Anexos\Plantillas\Plantilla" + CStr(lblFolio.Text) + ".xlsx"))
            workbook.LoadFromFile(Server.MapPath("Anexos\Plantillas\Plantilla" + CStr(lblFolio.Text) + ".xlsx"))


            'Dim sheet As Worksheet = workbook.Worksheets(0)
            'dtRegistros = sheet.ExportDataTable(sheet.AllocatedRange, True, True)

            'Consultas'
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")

            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
            SCMValores.Connection = ConexionBD
            SCMValores.CommandText = ""
            SCMValores.Parameters.Clear()
            Dim añoMaximo As Integer
            Dim añoMinimo As Integer
            SCMValores.CommandText = "Select MAX(año) from ms_equipo_importes where id_carga = @id_carga "
            SCMValores.Parameters.AddWithValue("@id_carga", Val(lblFolio.Text))
            ConexionBD.Open()
            añoMaximo = SCMValores.ExecuteScalar()
            ConexionBD.Close()

            SCMValores.CommandText = "Select MIN(año) from ms_equipo_importes where id_carga = @id_carga "
            ConexionBD.Open()
            añoMinimo = SCMValores.ExecuteScalar()
            ConexionBD.Close()

            'Llenar cabeceras del excel por los meses que se registraron '
            Dim sdaConsultaMes As New SqlDataAdapter
            Dim dsConsultaMes As New DataTable
            Dim query As String = ""


            Dim inicio As Integer = 10
            While añoMinimo <= añoMaximo
                query = "Select " +
                    " ISNULL( MAX(mes1), 0) as mes1, ISNULL( MAX(mes2), 0) as mes2, ISNULL( MAX(mes3), 0) as mes3, ISNULL( MAX(mes4), 0) as mes4, " +
                    " ISNULL( MAX(mes5), 0) as mes5, ISNULL( MAX(mes6), 0) as mes6, ISNULL( MAX(mes7), 0) as mes7, ISNULL( MAX(mes8), 0) as mes8, " +
                    " ISNULL( MAX(mes9), 0) as mes9, ISNULL( MAX(mes10), 0) as mes10, ISNULL( MAX(mes11), 0) as mes11, ISNULL( MAX(mes12), 0) as mes12 " +
                    " from ms_equipo_importes  where año = @año and id_carga = @id_carga  "
                sdaConsultaMes.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaConsultaMes.SelectCommand.Parameters.AddWithValue("@año", añoMinimo)
                sdaConsultaMes.SelectCommand.Parameters.AddWithValue("@id_carga", Val(lblFolio.Text))
                ConexionBD.Open()
                sdaConsultaMes.Fill(dsConsultaMes)
                ConexionBD.Close()

                For index As Integer = 0 To 11

                    If Val(dsConsultaMes.Rows(dsConsultaMes.Rows.Count() - 1).ItemArray(index)) <> 0 Then
                        workbook.Worksheets(0).Cells(inicio).Value = nombreColumna(dsConsultaMes.Columns(index).ColumnName, añoMinimo)
                        workbook.Worksheets(0).Cells(inicio).ColumnWidth = 18
                        ' sheet.Columns(inicio).Text = nombreColumna(dsConsultaMes.Columns(index).ColumnName, añoMinimo)
                        inicio = inicio + 1
                    End If
                Next
                'FIN DE LA INSTRUCCION'
                sdaConsultaMes.Dispose()
                dsConsultaMes.Dispose()
                añoMinimo = añoMinimo + 1
            End While


            workbook.SaveToFile(Server.MapPath("Anexos\Plantillas\Plantilla" + CStr(lblFolio.Text) + ".xlsx"))

            'Llenado de los datos'
            workbook.LoadFromFile(Server.MapPath("Anexos\Plantillas\Plantilla" + CStr(lblFolio.Text) + ".xlsx"))
            Dim sheetCeldas As Worksheet = workbook.Worksheets(0)
            Dim inicioCelda As Integer = 50
            Dim sdaConsulta As New SqlDataAdapter
            Dim dsConsulta As New DataTable
            Dim id_ms_contrato As Integer = 0
            query = " select id_ms_contrato, no_contrato, empresa, arrendadora, tipo_arrendamiento, fecha_inicio, fecha_fin, isnull (inversion, 0) from ms_contrato_arrenda where id_carga = @id_carga "
            sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)
            sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_carga", Val(lblFolio.Text))
            ConexionBD.Open()
            sdaConsulta.Fill(dsConsulta)
            ConexionBD.Close()

            Dim inicioCasilla As Integer = 1
            For index As Integer = 0 To dsConsulta.Rows.Count() - 1

                id_ms_contrato = Val(dsConsulta.Rows(index).Item(0).ToString())
                'Contrato'
                sheetCeldas.Columns(0).Cells(inicioCasilla).Value = dsConsulta.Rows(index).Item(1).ToString()
                '   workbook.Worksheets(0).Columns(0).Cells(inicioCasilla).Value = dsConsulta.Rows(index).Item(1).ToString()
                'Empresa'
                sheetCeldas.Columns(1).Cells(inicioCasilla).Value = dsConsulta.Rows(index).Item(2).ToString()
                '  workbook.Worksheets(0).Columns(1).Cells(inicioCasilla).Value = dsConsulta.Rows(index).Item(2).ToString()
                'Arrendedora'
                sheetCeldas.Columns(2).Cells(inicioCasilla).Value = dsConsulta.Rows(index).Item(3).ToString()
                'workbook.Worksheets(0).Columns(2).Cells(inicioCasilla).Value = dsConsulta.Rows(index).Item(3).ToString()
                'Tipo de arrendamiento'
                sheetCeldas.Columns(3).Cells(inicioCasilla).Value = dsConsulta.Rows(index).Item(4).ToString()
                'workbook.Worksheets(0).Columns(3).Cells(inicioCasilla).Value = dsConsulta.Rows(index).Item(4).ToString()
                'Inicio'
                sheetCeldas.Columns(5).Cells(inicioCasilla).Value = dsConsulta.Rows(index).Item(5).ToString()
                'workbook.Worksheets(0).Columns(5).Cells(inicioCasilla).Value = dsConsulta.Rows(index).Item(5).ToString()
                'Fin'
                sheetCeldas.Columns(6).Cells(inicioCasilla).Value = dsConsulta.Rows(index).Item(6).ToString()
                'workbook.Worksheets(0).Columns(6).Cells(inicioCasilla).Value = dsConsulta.Rows(index).Item(6).ToString()
                'Inversion'
                sheetCeldas.Columns(7).Cells(inicioCasilla).Value = dsConsulta.Rows(index).Item(7).ToString()
                'workbook.Worksheets(0).Columns(7).Cells(inicioCasilla).Value = dsConsulta.Rows(index).Item(7).ToString()



                'VALIDAR LOS ANEXOS QUE ESTEN LIGADOS A ESTE CONTRATO CON ESTA CARGA'
                Dim sdaAnexos As New SqlDataAdapter
                Dim dsAnexos As New DataSet
                Dim id_ms_anexo As Integer = 0
                query = "SELECT id_ms_anexo, anexo from ms_anexo_arrenda where id_ms_contrato = @id_ms_contrato"
                sdaAnexos.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaAnexos.SelectCommand.Parameters.AddWithValue("@id_ms_contrato", id_ms_contrato)
                ConexionBD.Open()
                sdaAnexos.Fill(dsAnexos)
                ConexionBD.Close()

                For indexA As Integer = 0 To dsAnexos.Tables(0).Rows().Count() - 1

                    If indexA <> 0 Then
                        inicioCasilla = inicioCasilla + 1
                    End If

                    If inicioCasilla > inicioCelda - 30 Then
                        inicioCelda = inicioCelda + 50
                        sheetCeldas.Range("A" + inicioCelda.ToString()).Text = " "
                        'workbook.SaveToFile(Server.MapPath("Anexos\Plantillas\Plantilla" + CStr(lblFolio.Text) + ".xlsx"))
                    End If

                    ' inicioCasilla = inicioCasilla + 1
                    id_ms_anexo = Val(dsAnexos.Tables(0).Rows(indexA).Item("id_ms_anexo"))
                    'Anexo'
                    sheetCeldas.Columns(4).Cells(inicioCasilla).Value = dsAnexos.Tables(0).Rows(indexA).Item("anexo")
                    'workbook.Worksheets(0).Columns(4).Cells(inicioCasilla).Value = dsAnexos.Tables(0).Rows(indexA).Item("anexo")

                    'CREAR CONSULTA PARA LAS UNIDADES'
                    Dim sdaUnidades As New SqlDataAdapter
                    Dim dsUnidades As New DataSet
                    Dim id_ms_equipo As Integer = 0
                    query = "SELECT id_ms_equipo, serie  FROM ms_equipo_arrenda where id_ms_anexo = @id_ms_anexo "
                    sdaUnidades.SelectCommand = New SqlCommand(query, ConexionBD)
                    sdaUnidades.SelectCommand.Parameters.AddWithValue("@id_ms_anexo", id_ms_anexo)
                    ConexionBD.Open()
                    sdaUnidades.Fill(dsUnidades)
                    ConexionBD.Close()

                    For indexU As Integer = 0 To dsUnidades.Tables(0).Rows().Count - 1

                        If indexU <> 0 Then
                            inicioCasilla = inicioCasilla + 1
                        End If

                        If inicioCasilla > inicioCelda - 30 Then
                            inicioCelda = inicioCelda + 50
                            sheetCeldas.Range("A" + inicioCelda.ToString()).Text = " "
                            'workbook.SaveToFile(Server.MapPath("Anexos\Plantillas\Plantilla" + CStr(lblFolio.Text) + ".xlsx"))
                        End If

                        id_ms_equipo = Val(dsUnidades.Tables(0).Rows(indexU).Item("id_ms_equipo"))
                        'Serie'
                        sheetCeldas.Columns(8).Cells(inicioCasilla).Value = dsUnidades.Tables(0).Rows(indexU).Item("serie").ToString()
                        ' workbook.Worksheets(0).Columns(8).Cells(inicioCasilla).Value = dsUnidades.Tables(0).Rows(indexU).Item("serie").ToString()

                        'CREAR CONSULTA PARA LOS IMPORTES DE ESTA UNIDAD'
                        Dim sdaImportes As New SqlDataAdapter
                        Dim dsImportes As New DataTable

                        Dim inicioMes As Integer = 10
                        ' añoMinimo = añoMinimoMes
                        'While añoMinimo <= añoMaximo
                        '  Dim neuvo As String = query.Remove(0, 6)
                        query = "Select " +
                                    " ISNULL(mes1, 0) as mes1, ISNULL( mes2, 0) as mes2, ISNULL( mes3, 0) as mes3, ISNULL( mes4, 0) as mes4, " +
                                    " ISNULL( mes5, 0) as mes5, ISNULL( mes6, 0) as mes6, ISNULL( mes7, 0) as mes7, ISNULL( mes8, 0) as mes8, " +
                                    " ISNULL( mes9, 0) as mes9, ISNULL( mes10, 0) as mes10, ISNULL( mes11, 0) as mes11, ISNULL( mes12, 0) as mes12, año " +
                                    " from ms_equipo_importes  where id_ms_equipo_arrenda = @id_ms_equipo "
                        sdaImportes.SelectCommand = New SqlCommand(query, ConexionBD)
                            sdaImportes.SelectCommand.Parameters.AddWithValue("@id_ms_equipo", id_ms_equipo)
                            ConexionBD.Open()
                            sdaImportes.Fill(dsImportes)
                            ConexionBD.Close()

                            Dim indexI As Integer = 0

                        For indexUI As Integer = 0 To dsImportes.Rows.Count() - 1
                            Dim añoActual = Val(dsImportes.Rows(indexUI).Item(12))
                            While indexI <= 11

                                If Val(dsImportes.Rows(indexUI).ItemArray(indexI)) <> 0 Then
                                    If Val(workbook.Worksheets(0).Cells(inicioMes).Value.ToString().Substring(1, 5)) = añoActual And validarNombreColumna(workbook.Worksheets(0).Cells(inicioMes).Value.ToString(), indexI) Then

                                        'sheetCeldas.Columns(inicioMes).Cells(inicioCasilla).Value = dsImportes.Rows(indexUI).Item(indexI)
                                        workbook.Worksheets(0).Columns(inicioMes).Cells(inicioCasilla).Value = dsImportes.Rows(dsImportes.Rows.Count() - 1).Item(indexI)
                                        inicioMes = inicioMes + 1

                                        indexI = indexI + 1

                                    Else
                                        If Val(workbook.Worksheets(0).Cells(inicioMes).Value.ToString().Substring(1, 5)) > añoActual Then
                                            indexI = 12
                                        Else
                                            inicioMes = inicioMes + 1
                                        End If
                                    End If
                                Else
                                    indexI = indexI + 1
                                End If

                            End While
                        Next


                        workbook.SaveToFile(Server.MapPath("Anexos\Plantillas\Plantilla" + lblFolio.Text + ".xlsx"))


                        'FIN DE LA INSTRUCCION'
                        sdaImportes.Dispose()
                            dsImportes.Dispose()
                            'añoMinimo = añoMinimo + 1
                            ' End While
                        Next

                        '  sdaUnidades.Dispose()
                        ' dsUnidades.Dispose()
                    Next
                inicioCasilla = inicioCasilla + 1

                If inicioCasilla > inicioCelda - 30 Then
                    inicioCelda = inicioCelda + 50
                    sheetCeldas.Range("A" + inicioCelda.ToString()).Text = " "
                    'workbook.SaveToFile(Server.MapPath("Anexos\Plantillas\Plantilla" + CStr(lblFolio.Text) + ".xlsx"))
                End If
            Next
            workbook.SaveToFile(Server.MapPath("Anexos\Plantillas\Plantilla" + CStr(lblFolio.Text) + ".xlsx"))
            btnGenerarPlantilla.Visible = False
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub
#End Region
End Class