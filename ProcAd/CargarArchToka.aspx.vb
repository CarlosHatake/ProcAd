Public Class CargarArchToka
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
            .fuArchivo.Enabled = True
            .btnAceptar.Enabled = True
            .btnNuevo.Enabled = False
            .txtResultado.Visible = False
            .gvFacturas.DataBind()
        End With
    End Sub

#End Region

#Region "Aceptar / Nuevo"

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""

                ' '' Ruta Local
                ''Dim sFileDir As String = "C:/ProcAd - Adjuntos Gas/" 'Ruta en que se almacenará el archivo
                ' Ruta en Atenea
                Dim sFileDir As String = "D:\ProcAd - Adjuntos Gas\" 'Ruta en que se almacenará el archivo
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
                                If sheet.Range(aux, 1).Text = "Tarjeta" Then
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

                            Dim tempc As Integer
                            tempc = dtRegistros.Columns.Count

                            'Validar el número de columnas del archivo
                            If dtRegistros.Columns.Count <> 29 Then
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

                                gvFacturas.DataSource = dtRegistros
                                gvFacturas.DataBind()

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
                                Dim cAct As Integer = 0
                                Dim cOmit As Integer = 0

                                'Tabla Detalle [dt_carga_comb_toka]
                                For i = 0 To dtRegistros.Rows.Count - 1
                                    If dtRegistros.Rows(i).Item("Cargo O Abono").ToString = "CARGO" Then

                                        SCMValores.Parameters.Clear()
                                        SCMValores.CommandText = "select case when (select count(*) from dt_carga_comb_toka where no_comprobante = @no_comprobante and rfc = @rfc) = 0 then 0 else (select MAX(id_dt_carga_comb_toka) from dt_carga_comb_toka where no_comprobante = @no_comprobante and rfc = @rfc) end as valor "
                                        SCMValores.Parameters.AddWithValue("@no_comprobante", dtRegistros.Rows(i).Item("Autorizacion").ToString)
                                        SCMValores.Parameters.AddWithValue("@rfc", dtRegistros.Rows(i).Item("Rfc").ToString)
                                        ConexionBD.Open()
                                        valReg = SCMValores.ExecuteScalar
                                        ConexionBD.Close()

                                        If valReg = 0 Then
                                            Dim consumo As Decimal
                                            consumo = dtRegistros.Rows(i).Item("Consumo").ToString
                                            Dim ieps As Decimal
                                            ieps = dtRegistros.Rows(i).Item("Ieps").ToString

                                            'No existe el registro, por lo que se inserta
                                            SCMValores.CommandText = "insert into dt_carga_comb_toka ( id_usr_carga,  fecha_carga,  empresa,  identificador_vehiculo,  placa,  num_tarjeta,  numero_serie,  no_comprobante,  id_mercancia,  mercancia,  importe_con_ieps,  importe_sin_imp,  ieps,  iva,  importe_transaccion,  cantidad_mercancia,  precio_ticket,  precio_sin_iva,  id_gasolinera,  razon_social_afiliado,  rfc,  obs,  fecha,  status,  tolerancia_unidad) " + _
                                                                     "                        values (@id_usr_carga, @fecha_carga, @empresa, @identificador_vehiculo, @placa, @num_tarjeta, @numero_serie, @no_comprobante, @id_mercancia, @mercancia, @importe_con_ieps, @importe_sin_imp, @ieps, @iva, @importe_transaccion, @cantidad_mercancia, @precio_ticket, @precio_sin_iva, @id_gasolinera, @razon_social_afiliado, @rfc, @obs, @fecha, @status, @tolerancia_unidad) "

                                            SCMValores.Parameters.AddWithValue("@id_usr_carga", Val(._txtIdUsuario.Text))
                                            SCMValores.Parameters.AddWithValue("@fecha_carga", fecha)
                                            SCMValores.Parameters.AddWithValue("@empresa", dtRegistros.Rows(i).Item("Nombre Empleado").ToString)
                                            SCMValores.Parameters.AddWithValue("@identificador_vehiculo", dtRegistros.Rows(i).Item("Nomina").ToString)
                                            SCMValores.Parameters.AddWithValue("@num_tarjeta", dtRegistros.Rows(i).Item("Tarjeta").ToString)
                                            'SCMValores.Parameters.AddWithValue("@no_comprobante", dtRegistros.Rows(i).Item("Autorizacion").ToString)
                                            SCMValores.Parameters.AddWithValue("@id_mercancia", "1")
                                            SCMValores.Parameters.AddWithValue("@mercancia", dtRegistros.Rows(i).Item("Producto").ToString)
                                            SCMValores.Parameters.AddWithValue("@importe_con_ieps", consumo + ieps)
                                            SCMValores.Parameters.AddWithValue("@importe_sin_imp", consumo)
                                            SCMValores.Parameters.AddWithValue("@ieps", ieps)
                                            SCMValores.Parameters.AddWithValue("@iva", dtRegistros.Rows(i).Item("Iva").ToString)
                                            SCMValores.Parameters.AddWithValue("@importe_transaccion", dtRegistros.Rows(i).Item("Importe").ToString)
                                            SCMValores.Parameters.AddWithValue("@cantidad_mercancia", dtRegistros.Rows(i).Item("Litros").ToString)
                                            SCMValores.Parameters.AddWithValue("@precio_ticket", dtRegistros.Rows(i).Item("Precio Gas").ToString)
                                            SCMValores.Parameters.AddWithValue("@precio_sin_iva", (consumo + ieps) / Val(dtRegistros.Rows(i).Item("Litros").ToString))
                                            SCMValores.Parameters.AddWithValue("@id_gasolinera", dtRegistros.Rows(i).Item("Id Gasolinera").ToString)
                                            SCMValores.Parameters.AddWithValue("@razon_social_afiliado", dtRegistros.Rows(i).Item("Nombre Gasolinera").ToString)
                                            'SCMValores.Parameters.AddWithValue("@rfc", dtRegistros.Rows(i).Item("Rfc").ToString)
                                            SCMValores.Parameters.AddWithValue("@obs", "")
                                            SCMValores.Parameters.AddWithValue("@fecha", CDate(dtRegistros.Rows(i).Item("Fecha Movimiento").ToString))
                                            SCMValores.Parameters.AddWithValue("@status", "P")

                                            'SiCEm
                                            Dim sdaUnidad As New SqlDataAdapter
                                            Dim dsUnidad As New DataSet
                                            sdaUnidad.SelectCommand = New SqlCommand("select isnull((select placas from bd_Empleado.dbo.ms_vehiculo where no_eco = @noEco and status = 'A'), '') as placas " + _
                                                                                     "     , isnull((select serie from bd_Empleado.dbo.ms_vehiculo where no_eco = @noEco and status = 'A'), '') as serie " + _
                                                                                     "     , isnull((select porcent_tolerancia from bd_Empleado.dbo.ms_vehiculo where no_eco = @noEco and status = 'A'), 0) as porcent_tolerancia ", ConexionBD)
                                            sdaUnidad.SelectCommand.Parameters.AddWithValue("@noEco", dtRegistros.Rows(i).Item("Nomina").ToString)
                                            ConexionBD.Open()
                                            sdaUnidad.Fill(dsUnidad)
                                            ConexionBD.Close()
                                            SCMValores.Parameters.AddWithValue("@placa", dsUnidad.Tables(0).Rows(0).Item("placas").ToString())
                                            SCMValores.Parameters.AddWithValue("@numero_serie", dsUnidad.Tables(0).Rows(0).Item("serie").ToString())
                                            SCMValores.Parameters.AddWithValue("@tolerancia_unidad", dsUnidad.Tables(0).Rows(0).Item("porcent_tolerancia").ToString())
                                            sdaUnidad.Dispose()
                                            dsUnidad.Dispose()

                                            cIns = cIns + 1
                                        Else
                                            'Ya existe el registro, por lo que se omite
                                            SCMValores.CommandText = "update dt_carga_comb_toka set id_usr_modifica = @id_usr_modifica, fecha_modifica = @fecha_modifica " + _
                                                                     "where id_dt_carga_comb_toka = @id_dt_carga_comb_toka "
                                            SCMValores.Parameters.AddWithValue("@id_dt_carga_comb_toka", valReg)
                                            SCMValores.Parameters.AddWithValue("@id_usr_modifica", Val(._txtIdUsuario.Text))
                                            SCMValores.Parameters.AddWithValue("@fecha_modifica", fecha)

                                            cAct = cAct + 1
                                        End If

                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()
                                    Else
                                        cOmit = cOmit + 1
                                    End If
                                Next

                                mensaje = mensaje + Chr(13) & Chr(10) + Chr(13) & Chr(10) + "Insertados: " + cIns.ToString + Chr(13) & Chr(10) + "Omitidos: " + cAct.ToString + Chr(13) & Chr(10) + "Otros: " + cOmit.ToString

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