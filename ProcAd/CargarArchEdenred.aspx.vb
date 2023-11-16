Public Class CargarArchEdenred
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
                        If sFileExt = ".xls" Then
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
                                If sheet.Range(aux, 1).Text = "Id Grupo de Región" Then
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
                            If dtRegistros.Columns.Count <> 47 Then
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

                                'Tabla Detalle [dt_carga_comb]
                                For i = 0 To dtRegistros.Rows.Count - 1
                                    SCMValores.Parameters.Clear()
                                    SCMValores.CommandText = "select case when (select count(*) from dt_carga_comb where transaccion = @noTrans and num_tarjeta = @noTarjeta) = 0 then 0 else (select MAX(id_dt_carga_comb) from dt_carga_comb where transaccion = @noTrans and num_tarjeta = @noTarjeta) end as valor "
                                    SCMValores.Parameters.AddWithValue("@noTrans", dtRegistros.Rows(i).Item("Transacción").ToString)
                                    SCMValores.Parameters.AddWithValue("@noTarjeta", dtRegistros.Rows(i).Item("Núm Tarjeta").ToString)
                                    ConexionBD.Open()
                                    valReg = SCMValores.ExecuteScalar
                                    ConexionBD.Close()


                                    If valReg = 0 Then
                                        'No existe el registro, por lo que se inserta
                                        'SCMValores.CommandText = "insert into dt_carga_comb ( id_usr_carga,  fecha_carga,  codigo_grupo_region,  grupo_region,  codigo_region,  region,  codigo_cc,  cc,  identificador,  nombre_tarjeta,  estacion_servicio,  no_comprobante,  num_tarjeta,  tipo_tarjeta,  consumo_en,  placa,  marca,  cantidad,  importe_total,  importe_neto,  iva,  fecha,  hora,  codigo_conductor,  nombre_conductor,  km,  km_ant,  recorrido,  rendimiento,  obs,  status) " + _
                                        '                         "                   values (@id_usr_carga, @fecha_carga, @codigo_grupo_region, @grupo_region, @codigo_region, @region, @codigo_cc, @cc, @identificador, @nombre_tarjeta, @estacion_servicio, @no_comprobante, @num_tarjeta, @tipo_tarjeta, @consumo_en, @placa, @marca, @cantidad, @importe_total, @importe_neto, @iva, @fecha, @hora, @codigo_conductor, @nombre_conductor, @km, @km_ant, @recorrido, @rendimiento, @obs, @status) "
                                        SCMValores.CommandText = "insert into dt_carga_comb ( id_usr_carga,  fecha_carga,  id_grupo_region,  grupo_region,  id_region,  region,  id_centro_costos,  centro_costos,  identificador_vehiculo,  vehiculo,  placa,  num_tarjeta,  numero_serie,  fecha_transaccion,  transaccion,  hora_transaccion,  tipo_tarjeta,  consumo_en,  no_comprobante,  id_mercancia,  mercancia,  km_ant_transaccion,  km_transaccion,  recorrido,  importe_con_ieps,  importe_sin_imp,  ieps,  iva,  importe_transaccion,  porcent_iva,  cantidad_mercancia,  precio_ticket,  precio_sin_iva,  rendimiento,  rendimiento_real,  saldo_ant_transaccion,  id_conductor,  conductor,  no_estacion_pemex,  razon_social_afiliado,  rfc,  numero_control,  folio,  obs,  respuesta,  fecha,  status) " + _
                                                                 "                   values (@id_usr_carga, @fecha_carga, @id_grupo_region, @grupo_region, @id_region, @region, @id_centro_costos, @centro_costos, @identificador_vehiculo, @vehiculo, @placa, @num_tarjeta, @numero_serie, @fecha_transaccion, @transaccion, @hora_transaccion, @tipo_tarjeta, @consumo_en, @no_comprobante, @id_mercancia, @mercancia, @km_ant_transaccion, @km_transaccion, @recorrido, @importe_con_ieps, @importe_sin_imp, @ieps, @iva, @importe_transaccion, @porcent_iva, @cantidad_mercancia, @precio_ticket, @precio_sin_iva, @rendimiento, @rendimiento_real, @saldo_ant_transaccion, @id_conductor, @conductor, @no_estacion_pemex, @razon_social_afiliado, @rfc, @numero_control, @folio, @obs, @respuesta, @fecha, @status) "

                                        SCMValores.Parameters.AddWithValue("@id_usr_carga", Val(._txtIdUsuario.Text))
                                        SCMValores.Parameters.AddWithValue("@fecha_carga", fecha)
                                        'SCMValores.Parameters.AddWithValue("@id_usr_modif", DBNull.Value)
                                        'SCMValores.Parameters.AddWithValue("@fecha_modif", DBNull.Value)

                                        'SCMValores.Parameters.AddWithValue("@codigo_grupo_region", dtRegistros.Rows(i).Item("Código Grupo Región").ToString)
                                        'SCMValores.Parameters.AddWithValue("@grupo_region", dtRegistros.Rows(i).Item("Grupo Región").ToString)
                                        'SCMValores.Parameters.AddWithValue("@codigo_region", dtRegistros.Rows(i).Item("Código Región").ToString)
                                        'SCMValores.Parameters.AddWithValue("@region", dtRegistros.Rows(i).Item("Región").ToString)
                                        'SCMValores.Parameters.AddWithValue("@codigo_cc", dtRegistros.Rows(i).Item("Código Centro Costos").ToString)
                                        'SCMValores.Parameters.AddWithValue("@cc", dtRegistros.Rows(i).Item("Centro Costos").ToString)
                                        'SCMValores.Parameters.AddWithValue("@identificador", dtRegistros.Rows(i).Item("Identificador").ToString)
                                        'SCMValores.Parameters.AddWithValue("@nombre_tarjeta", dtRegistros.Rows(i).Item("Nombre Tarjeta").ToString)
                                        'SCMValores.Parameters.AddWithValue("@estacion_servicio", dtRegistros.Rows(i).Item("Estación de Servicio").ToString)
                                        'SCMValores.Parameters.AddWithValue("@no_comprobante", dtRegistros.Rows(i).Item("NO Comprobante").ToString)
                                        'SCMValores.Parameters.AddWithValue("@num_tarjeta", dtRegistros.Rows(i).Item("Num Tarjeta").ToString)
                                        'SCMValores.Parameters.AddWithValue("@tipo_tarjeta", dtRegistros.Rows(i).Item("Tipo de Tarjeta").ToString)
                                        'SCMValores.Parameters.AddWithValue("@consumo_en", dtRegistros.Rows(i).Item("Consumo En").ToString)
                                        'SCMValores.Parameters.AddWithValue("@placa", dtRegistros.Rows(i).Item("Placa").ToString)
                                        'SCMValores.Parameters.AddWithValue("@marca", dtRegistros.Rows(i).Item("Marca").ToString)
                                        'SCMValores.Parameters.AddWithValue("@cantidad", dtRegistros.Rows(i).Item("Cantidad").ToString)
                                        'SCMValores.Parameters.AddWithValue("@importe_total", dtRegistros.Rows(i).Item("Importe Total").ToString)
                                        'SCMValores.Parameters.AddWithValue("@importe_neto", dtRegistros.Rows(i).Item("Importe Neto").ToString)
                                        'SCMValores.Parameters.AddWithValue("@iva", dtRegistros.Rows(i).Item("IVA").ToString)
                                        'SCMValores.Parameters.AddWithValue("@fecha", CDate(dtRegistros.Rows(i).Item("Fecha").ToString))
                                        'SCMValores.Parameters.AddWithValue("@hora", CDate(dtRegistros.Rows(i).Item("Hora").ToString))
                                        'SCMValores.Parameters.AddWithValue("@codigo_conductor", dtRegistros.Rows(i).Item("Código Conductor").ToString)
                                        'SCMValores.Parameters.AddWithValue("@nombre_conductor", dtRegistros.Rows(i).Item("Nombre Conductor").ToString)
                                        'SCMValores.Parameters.AddWithValue("@km", dtRegistros.Rows(i).Item("Km").ToString)
                                        'SCMValores.Parameters.AddWithValue("@km_ant", dtRegistros.Rows(i).Item("Km Ant").ToString)
                                        'SCMValores.Parameters.AddWithValue("@recorrido", dtRegistros.Rows(i).Item("Recorrido").ToString)
                                        'SCMValores.Parameters.AddWithValue("@rendimiento", dtRegistros.Rows(i).Item("Rendimiento").ToString)
                                        'SCMValores.Parameters.AddWithValue("@obs", dtRegistros.Rows(i).Item("Observaciones").ToString)

                                        SCMValores.Parameters.AddWithValue("@id_grupo_region", dtRegistros.Rows(i).Item("Id Grupo de Región").ToString)
                                        SCMValores.Parameters.AddWithValue("@grupo_region", dtRegistros.Rows(i).Item("Grupo de Región ").ToString)
                                        SCMValores.Parameters.AddWithValue("@id_region", dtRegistros.Rows(i).Item("Id Región").ToString)
                                        SCMValores.Parameters.AddWithValue("@region", dtRegistros.Rows(i).Item("Región").ToString)
                                        SCMValores.Parameters.AddWithValue("@id_centro_costos", dtRegistros.Rows(i).Item("Id Centro de Costos").ToString)
                                        SCMValores.Parameters.AddWithValue("@centro_costos", dtRegistros.Rows(i).Item("Centro de Costos").ToString)
                                        SCMValores.Parameters.AddWithValue("@identificador_vehiculo", dtRegistros.Rows(i).Item("Identificador Vehículo").ToString)
                                        SCMValores.Parameters.AddWithValue("@vehiculo", dtRegistros.Rows(i).Item("Vehículo").ToString)
                                        SCMValores.Parameters.AddWithValue("@placa", dtRegistros.Rows(i).Item("Placa").ToString)
                                        SCMValores.Parameters.AddWithValue("@num_tarjeta", dtRegistros.Rows(i).Item("Núm Tarjeta").ToString)
                                        SCMValores.Parameters.AddWithValue("@numero_serie", dtRegistros.Rows(i).Item("Número de Serie").ToString)
                                        SCMValores.Parameters.AddWithValue("@fecha_transaccion", dtRegistros.Rows(i).Item("Fecha Transacción").ToString)
                                        SCMValores.Parameters.AddWithValue("@transaccion", dtRegistros.Rows(i).Item("Transacción").ToString)
                                        SCMValores.Parameters.AddWithValue("@hora_transaccion", dtRegistros.Rows(i).Item("Hora Transacción").ToString)
                                        SCMValores.Parameters.AddWithValue("@tipo_tarjeta", dtRegistros.Rows(i).Item("Tipo de Tarjeta").ToString)
                                        SCMValores.Parameters.AddWithValue("@consumo_en", dtRegistros.Rows(i).Item("Consumo En").ToString)
                                        SCMValores.Parameters.AddWithValue("@no_comprobante", dtRegistros.Rows(i).Item("No Comprobante").ToString)
                                        SCMValores.Parameters.AddWithValue("@id_mercancia", dtRegistros.Rows(i).Item("Id Mercancía").ToString)
                                        SCMValores.Parameters.AddWithValue("@mercancia", dtRegistros.Rows(i).Item("Mercancía").ToString)
                                        SCMValores.Parameters.AddWithValue("@km_ant_transaccion", dtRegistros.Rows(i).Item("Km Ant Transacción").ToString)
                                        SCMValores.Parameters.AddWithValue("@km_transaccion", dtRegistros.Rows(i).Item("Km Transacción").ToString)
                                        SCMValores.Parameters.AddWithValue("@recorrido", dtRegistros.Rows(i).Item("Recorrido").ToString)
                                        SCMValores.Parameters.AddWithValue("@importe_con_ieps", dtRegistros.Rows(i).Item("Importe con IEPS").ToString)
                                        SCMValores.Parameters.AddWithValue("@importe_sin_imp", dtRegistros.Rows(i).Item("Importe sin Impuestos").ToString)
                                        SCMValores.Parameters.AddWithValue("@ieps", dtRegistros.Rows(i).Item("IEPS").ToString)
                                        SCMValores.Parameters.AddWithValue("@iva", dtRegistros.Rows(i).Item("IVA").ToString)
                                        SCMValores.Parameters.AddWithValue("@importe_transaccion", dtRegistros.Rows(i).Item("Importe Transacción").ToString)
                                        SCMValores.Parameters.AddWithValue("@porcent_iva", dtRegistros.Rows(i).Item("% IVA").ToString)
                                        SCMValores.Parameters.AddWithValue("@cantidad_mercancia", dtRegistros.Rows(i).Item("Cantidad Mercancía").ToString)
                                        SCMValores.Parameters.AddWithValue("@precio_ticket", dtRegistros.Rows(i).Item("Precio Ticket").ToString)
                                        SCMValores.Parameters.AddWithValue("@precio_sin_iva", dtRegistros.Rows(i).Item("Precio sin IVA").ToString)
                                        SCMValores.Parameters.AddWithValue("@rendimiento", dtRegistros.Rows(i).Item("Rendimiento").ToString)
                                        SCMValores.Parameters.AddWithValue("@rendimiento_real", dtRegistros.Rows(i).Item("Rendimiento Real").ToString)
                                        SCMValores.Parameters.AddWithValue("@saldo_ant_transaccion", dtRegistros.Rows(i).Item("Saldo Ant Transacción").ToString)
                                        SCMValores.Parameters.AddWithValue("@id_conductor", dtRegistros.Rows(i).Item("Id Conductor").ToString)
                                        SCMValores.Parameters.AddWithValue("@conductor", dtRegistros.Rows(i).Item("Conductor").ToString)
                                        SCMValores.Parameters.AddWithValue("@no_estacion_pemex", dtRegistros.Rows(i).Item("No Estación Pemex").ToString)
                                        SCMValores.Parameters.AddWithValue("@razon_social_afiliado", dtRegistros.Rows(i).Item("Razón Social Afiliado").ToString)
                                        SCMValores.Parameters.AddWithValue("@rfc", dtRegistros.Rows(i).Item("RFC").ToString)
                                        SCMValores.Parameters.AddWithValue("@numero_control", dtRegistros.Rows(i).Item("Número de Control").ToString)
                                        SCMValores.Parameters.AddWithValue("@folio", dtRegistros.Rows(i).Item("Folio").ToString)
                                        SCMValores.Parameters.AddWithValue("@obs", dtRegistros.Rows(i).Item("Observaciones").ToString)
                                        SCMValores.Parameters.AddWithValue("@respuesta", dtRegistros.Rows(i).Item("Respuesta").ToString)
                                        SCMValores.Parameters.AddWithValue("@fecha", CDate(dtRegistros.Rows(i).Item("Fecha Transacción").ToString + " " + dtRegistros.Rows(i).Item("Hora Transacción").ToString))

                                        SCMValores.Parameters.AddWithValue("@status", "P")

                                        cIns = cIns + 1
                                    Else
                                        'Ya existe el registro, por lo que se omite
                                        SCMValores.CommandText = "update dt_carga_comb set id_usr_modif = @id_usr_modif, fecha_modif = @fecha_modif " + _
                                                                 "where id_dt_carga_comb = @id_dt_carga_comb "
                                        SCMValores.Parameters.AddWithValue("@id_dt_carga_comb", valReg)
                                        SCMValores.Parameters.AddWithValue("@id_usr_modif", Val(._txtIdUsuario.Text))
                                        SCMValores.Parameters.AddWithValue("@fecha_modif", fecha)

                                        cAct = cAct + 1
                                    End If

                                    ConexionBD.Open()
                                    SCMValores.ExecuteNonQuery()
                                    ConexionBD.Close()
                                Next

                                mensaje = mensaje + Chr(13) & Chr(10) + Chr(13) & Chr(10) + "Insertados: " + cIns.ToString + Chr(13) & Chr(10) + "Omitidos: " + cAct.ToString

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