Public Class CargaPresupGV
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 1

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
            .litError.Text = ""
            .txtAño.Text = Now.Year.ToString
            .fuArchivo.Enabled = True
            .btnAceptar.Enabled = True
            .btnNuevo.Enabled = False
            .txtResultado.Visible = False
            .gvPresupuesto.DataBind()
        End With
    End Sub

#End Region

#Region "Aceptar / Nuevo"

    Protected Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaConsulta As New SqlDataAdapter
                Dim dsConsulta As New DataSet
                sdaConsulta.SelectCommand = New SqlCommand("select cg_cc.id_cc as ID " + _
                                                           "     , cg_empresa.nombre as Empresa " + _
                                                           "     , cg_cc.codigo as Código " + _
                                                           "     , cg_cc.nombre as Nombre " + _
                                                           "     , null as Enero " + _
                                                           "     , null as Febrero " + _
                                                           "     , null as Marzo " + _
                                                           "     , null as Abril " + _
                                                           "     , null as Mayo " + _
                                                           "     , null as Junio " + _
                                                           "     , null as Julio " + _
                                                           "     , null as Agosto " + _
                                                           "     , null as Septiembre " + _
                                                           "     , null as Octubre " + _
                                                           "     , null as Noviembre " + _
                                                           "     , null as Diciembre " + _
                                                           "from bd_Empleado.dbo.cg_cc " + _
                                                           "  left join bd_Empleado.dbo.cg_empresa on cg_cc.id_empresa = cg_empresa.id_empresa " + _
                                                           "where cg_cc.status = 'A' " + _
                                                           "order by Empresa, Nombre ", ConexionBD)
                .gvPlantilla.DataSource = dsConsulta
                ConexionBD.Open()
                sdaConsulta.Fill(dsConsulta)
                ConexionBD.Close()
                .gvPlantilla.DataBind()
                sdaConsulta.Dispose()
                dsConsulta.Dispose()

                Dim tw As New System.IO.StringWriter
                Dim hw As New System.Web.UI.HtmlTextWriter(tw)
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment;filename=Plantilla.xls")
                Response.ContentType = "application/vnd.ms-excel"
                .gvPlantilla.Visible = True
                .gvPlantilla.RenderControl(hw)
                .gvPlantilla.Visible = False
                Response.Write(tw.ToString())
                Response.End()
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""

                ' '' Ruta Local
                ''Dim sFileDir As String = "C:/ProcAd - Adjuntos PresupC/" 'Ruta en que se almacenará el archivo
                ' Ruta en Atenea
                Dim sFileDir As String = "D:\ProcAd - Adjuntos PresupC\" 'Ruta en que se almacenará el archivo
                Dim lMaxFileSize As Long = 10485760 'Equivalente a 10 Megabytes

                'Verificar que el archivo ha sido seleccionado y es un archivo válido
                If (Not fuArchivo.PostedFile Is Nothing) And (fuArchivo.PostedFile.ContentLength > 0) Then
                    'Determinar el nombre original del archivo
                    Dim sFileName As String = System.IO.Path.GetFileName(fuArchivo.PostedFile.FileName)
                    Dim sFileExt As String = System.IO.Path.GetExtension(fuArchivo.PostedFile.FileName)
                    Dim fecha As DateTime = Date.Now
                    'Validar el tamaño máximo permitido
                    If fuArchivo.PostedFile.ContentLength <= lMaxFileSize Then
                        'Validar que se trate de un archivo Excel
                        If sFileExt = ".xls" Or sFileExt = ".xlsx" Then
                            Dim mensaje As String = ""
                            'Se agrega el la fecha al nombre del archivo
                            sFileName = Format(fecha.Year, "0000").ToString + Format(fecha.Month, "00").ToString + Format(fecha.Day, "00").ToString + Format(fecha.Hour, "00").ToString + Format(fecha.Minute, "00").ToString + Format(fecha.Second, "00").ToString + "-" + sFileName
                            'Almacenar el archivo en la ruta especificada
                            fuArchivo.PostedFile.SaveAs(sFileDir + sFileName)

                            Dim dtRegistros As DataTable
                            Dim workbook As Workbook = New Workbook()
                            workbook.LoadFromFile(sFileDir + sFileName)
                            'Inicializar worksheet
                            Dim sheet As Worksheet = workbook.Worksheets(0)

                            Dim noLineaC As Integer = 0
                            Dim aux As Integer = 1
                            Dim temp As String = ""
                            'Obtener el número de línea donde se encuantra la cabecera
                            Do While noLineaC = 0
                                If sheet.Range(aux, 1).Text = "ID" Then
                                    noLineaC = aux
                                Else
                                    aux = aux + 1
                                End If
                            Loop
                            'Eliminar Registros previoas a la Cabecera
                            For ind = (noLineaC - 1) To 1 Step -1
                                sheet.DeleteRow(ind)
                            Next
                            'Exportar la tabla al Datatable
                            dtRegistros = sheet.ExportDataTable()

                            'Validar el número de columnas del archivo
                            If dtRegistros.Columns.Count <> 16 Then
                                .litError.Text = "El archivo no contiene el número de columnas requeridas, favor de validarlo"
                            Else
                                'Depurar Tabla (Eliminar renglones Vacíos)
                                Dim valor As String = ""
                                Dim i As Integer = 0
                                Dim iFin As Integer
                                Dim numReg As Integer = 0
                                Dim numRegV As Integer = 0
                                'Se establece el número de registros a depurar
                                iFin = dtRegistros.Rows.Count - 1
                                While i <= iFin
                                    If dtRegistros.Rows(i).Item(0).ToString = "" Then
                                        dtRegistros.Rows(i).Delete()
                                        numRegV = numRegV + 1
                                        iFin = iFin - 1
                                    Else
                                        numReg = numReg + 1
                                        i = i + 1
                                    End If
                                End While

                                mensaje = "Registros a Procesar: " + numReg.ToString + Chr(13) & Chr(10) + "Registros Vacios: " + numRegV.ToString

                                gvPresupuesto.DataSource = dtRegistros
                                gvPresupuesto.DataBind()

                                'Almacenar Registros en BD
                                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                                SCMValores.Connection = ConexionBD
                                Dim SCMValoresTemp As SqlCommand = New System.Data.SqlClient.SqlCommand
                                SCMValoresTemp.Connection = ConexionBD
                                'Determinar si ya existe el registro
                                Dim valReg As Integer
                                Dim cIns As Integer = 0
                                Dim cOmit As Integer = 0

                                'Tabla Detalle [dt_carga_comb]
                                For i = 0 To dtRegistros.Rows.Count - 1
                                    SCMValores.Parameters.Clear()
                                    SCMValores.CommandText = "select case when (select count(*) from ms_presupuesto where id_cc = @id_cc and año = @año) = 0 then 0 else (select MAX(id_ms_presupuesto) from ms_presupuesto where id_cc = @id_cc and año = @año) end as valor "
                                    SCMValores.Parameters.AddWithValue("@id_cc", dtRegistros.Rows(i).Item("ID").ToString)
                                    SCMValores.Parameters.AddWithValue("@año", Val(.txtAño.Text))
                                    ConexionBD.Open()
                                    valReg = SCMValores.ExecuteScalar
                                    ConexionBD.Close()

                                    If valReg = 0 Then
                                        'No existe el registro, por lo que se inserta
                                        Dim sdaCC As New SqlDataAdapter
                                        Dim dsCC As New DataSet
                                        sdaCC.SelectCommand = New SqlCommand("select cg_empresa.id_empresa " + _
                                                                             "     , cg_empresa.nombre as empresa " + _
                                                                             "     , cg_cc.codigo " + _
                                                                             "     , cg_cc.nombre as centro_costo " + _
                                                                             "from bd_Empleado.dbo.cg_cc " + _
                                                                             "  left join bd_Empleado.dbo.cg_empresa on cg_cc.id_empresa = cg_empresa.id_empresa " + _
                                                                             "where cg_cc.id_cc = @id_cc ", ConexionBD)
                                        sdaCC.SelectCommand.Parameters.AddWithValue("@id_cc", Val(dtRegistros.Rows(i).Item("ID").ToString))
                                        ConexionBD.Open()
                                        sdaCC.Fill(dsCC)
                                        ConexionBD.Close()

                                        SCMValores.Parameters.Clear()
                                        SCMValores.CommandText = "insert into ms_presupuesto ( id_usr_carga,  fecha_carga,  id_empresa,  empresa,  id_cc,  centro_costo,  codigo,  año,  mes_01_p,  mes_02_p,  mes_03_p,  mes_04_p,  mes_05_p,  mes_06_p,  mes_07_p,  mes_08_p,  mes_09_p,  mes_10_p,  mes_11_p,  mes_12_p) " + _
                                                                 "                    values (@id_usr_carga, @fecha_carga, @id_empresa, @empresa, @id_cc, @centro_costo, @codigo, @año, @mes_01_p, @mes_02_p, @mes_03_p, @mes_04_p, @mes_05_p, @mes_06_p, @mes_07_p, @mes_08_p, @mes_09_p, @mes_10_p, @mes_11_p, @mes_12_p) "

                                        SCMValores.Parameters.AddWithValue("@id_usr_carga", Val(._txtIdUsuario.Text))
                                        SCMValores.Parameters.AddWithValue("@fecha_carga", fecha)
                                        SCMValores.Parameters.AddWithValue("@id_empresa", dsCC.Tables(0).Rows(0).Item("id_empresa").ToString())
                                        SCMValores.Parameters.AddWithValue("@empresa", dsCC.Tables(0).Rows(0).Item("empresa").ToString())
                                        SCMValores.Parameters.AddWithValue("@id_cc", Val(dtRegistros.Rows(i).Item("ID").ToString))
                                        SCMValores.Parameters.AddWithValue("@centro_costo", dsCC.Tables(0).Rows(0).Item("centro_costo").ToString())
                                        SCMValores.Parameters.AddWithValue("@codigo", dsCC.Tables(0).Rows(0).Item("codigo").ToString())
                                        SCMValores.Parameters.AddWithValue("@año", Val(.txtAño.Text))
                                        If dtRegistros.Rows(i).Item("Enero").ToString = "" Then
                                            SCMValores.Parameters.AddWithValue("@mes_01_p", 0)
                                        Else
                                            SCMValores.Parameters.AddWithValue("@mes_01_p", Val(dtRegistros.Rows(i).Item("Enero").ToString))
                                        End If
                                        If dtRegistros.Rows(i).Item("Febrero").ToString = "" Then
                                            SCMValores.Parameters.AddWithValue("@mes_02_p", 0)
                                        Else
                                            SCMValores.Parameters.AddWithValue("@mes_02_p", Val(dtRegistros.Rows(i).Item("Febrero").ToString))
                                        End If
                                        If dtRegistros.Rows(i).Item("Marzo").ToString = "" Then
                                            SCMValores.Parameters.AddWithValue("@mes_03_p", 0)
                                        Else
                                            SCMValores.Parameters.AddWithValue("@mes_03_p", Val(dtRegistros.Rows(i).Item("Marzo").ToString))
                                        End If
                                        If dtRegistros.Rows(i).Item("Abril").ToString = "" Then
                                            SCMValores.Parameters.AddWithValue("@mes_04_p", 0)
                                        Else
                                            SCMValores.Parameters.AddWithValue("@mes_04_p", Val(dtRegistros.Rows(i).Item("Abril").ToString))
                                        End If
                                        If dtRegistros.Rows(i).Item("Mayo").ToString = "" Then
                                            SCMValores.Parameters.AddWithValue("@mes_05_p", 0)
                                        Else
                                            SCMValores.Parameters.AddWithValue("@mes_05_p", Val(dtRegistros.Rows(i).Item("Mayo").ToString))
                                        End If
                                        If dtRegistros.Rows(i).Item("Junio").ToString = "" Then
                                            SCMValores.Parameters.AddWithValue("@mes_06_p", 0)
                                        Else
                                            SCMValores.Parameters.AddWithValue("@mes_06_p", Val(dtRegistros.Rows(i).Item("Junio").ToString))
                                        End If
                                        If dtRegistros.Rows(i).Item("Julio").ToString = "" Then
                                            SCMValores.Parameters.AddWithValue("@mes_07_p", 0)
                                        Else
                                            SCMValores.Parameters.AddWithValue("@mes_07_p", Val(dtRegistros.Rows(i).Item("Julio").ToString))
                                        End If
                                        If dtRegistros.Rows(i).Item("Agosto").ToString = "" Then
                                            SCMValores.Parameters.AddWithValue("@mes_08_p", 0)
                                        Else
                                            SCMValores.Parameters.AddWithValue("@mes_08_p", Val(dtRegistros.Rows(i).Item("Agosto").ToString))
                                        End If
                                        If dtRegistros.Rows(i).Item("Septiembre").ToString = "" Then
                                            SCMValores.Parameters.AddWithValue("@mes_09_p", 0)
                                        Else
                                            SCMValores.Parameters.AddWithValue("@mes_09_p", Val(dtRegistros.Rows(i).Item("Septiembre").ToString))
                                        End If
                                        If dtRegistros.Rows(i).Item("Octubre").ToString = "" Then
                                            SCMValores.Parameters.AddWithValue("@mes_10_p", 0)
                                        Else
                                            SCMValores.Parameters.AddWithValue("@mes_10_p", Val(dtRegistros.Rows(i).Item("Octubre").ToString))
                                        End If
                                        If dtRegistros.Rows(i).Item("Noviembre").ToString = "" Then
                                            SCMValores.Parameters.AddWithValue("@mes_11_p", 0)
                                        Else
                                            SCMValores.Parameters.AddWithValue("@mes_11_p", Val(dtRegistros.Rows(i).Item("Noviembre").ToString))
                                        End If
                                        If dtRegistros.Rows(i).Item("Diciembre").ToString = "" Then
                                            SCMValores.Parameters.AddWithValue("@mes_12_p", 0)
                                        Else
                                            SCMValores.Parameters.AddWithValue("@mes_12_p", Val(dtRegistros.Rows(i).Item("Diciembre").ToString))
                                        End If

                                        sdaCC.Dispose()
                                        dsCC.Dispose()

                                        cIns = cIns + 1
                                    Else
                                        'Ya existe el registro, por lo que se omite
                                        cOmit = cOmit + 1
                                    End If

                                    ConexionBD.Open()
                                    SCMValores.ExecuteNonQuery()
                                    ConexionBD.Close()
                                Next

                                mensaje = mensaje + Chr(13) & Chr(10) + Chr(13) & Chr(10) + "Insertados: " + cIns.ToString + Chr(13) & Chr(10) + "Omitidos: " + cOmit.ToString

                                .txtResultado.Visible = True
                                .txtResultado.Text = mensaje

                                .btnAceptar.Enabled = False
                                .btnNuevo.Enabled = True
                            End If
                        Else
                            'Rechazar el archivo por extensión
                            .lblMessage.Visible = True
                            .lblMessage.Text = "El archivo no tiene el formato correcto"
                        End If
                    Else
                        'Rechazar el archivo por tamaño
                        .lblMessage.Visible = True
                        .lblMessage.Text = "El archivo excede el límite de 10 MB"
                    End If
                Else
                    .lblMessage.Visible = True
                    .lblMessage.Text = "Favor de seleccionar un Archivo"
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        With Me
            limpiarPantalla()
            .btnAceptar.Enabled = True
        End With
    End Sub

#End Region

End Class