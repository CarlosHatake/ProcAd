Public Class CargarArchivo
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
            .lbl_UuidOmitido.Visible = False
            .txtUuidOmitido.Visible = False
            .gvFacturas.DataBind()
        End With
    End Sub

#End Region

#Region "Aceptar / Nuevo"

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""

                '' ' Ruta Local
                'Dim sFileDir As String = "C:/ProcAd - Adjuntos FactSAT/" 'Ruta en que se almacenará el archivo
                ' Ruta en Atenea
                Dim sFileDir As String = "D:\ProcAd - Adjuntos FactSAT\" 'Ruta en que se almacenará el archivo
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
                        If sFileExt = ".xlsx" Then
                            Dim mensaje As String = ""
                            'Se agrega el la fecha al nombre del archivo
                            sFileName = Format(fecha.Year, "0000").ToString + Format(fecha.Month, "00").ToString + Format(fecha.Day, "00").ToString + Format(fecha.Hour, "00").ToString + Format(fecha.Minute, "00").ToString + Format(fecha.Second, "00").ToString + .ddlTipo.SelectedValue + "-" + sFileName
                            'Almacenar el archivo en la ruta especificada
                            fuArchivo.PostedFile.SaveAs(sFileDir + sFileName)

                            Dim dtRegistros As DataTable
                            Dim workbook As Workbook = New Workbook()
                            workbook.LoadFromFile(sFileDir + sFileName)
                            'Inicializar worksheet
                            Dim sheet As Worksheet = workbook.Worksheets(0)
                            dtRegistros = sheet.ExportDataTable()

                            'Determinarl el Tipo de Archivo (General / Detalle)
                            If (.ddlTipo.SelectedValue = "G" And .ddlVersion.SelectedValue.ToString = "3.3" And dtRegistros.Columns.Count <> 43) Or (.ddlTipo.SelectedValue = "G" And .ddlVersion.SelectedValue.ToString = "4.0" And dtRegistros.Columns.Count <> 50) Or (.ddlTipo.SelectedValue = "D" And dtRegistros.Columns.Count = 35) Then
                                .litError.Text = "El archivo no contiene el número de columnas requeridas, favor de validarlo"
                            Else
                                If .ddlVersion.SelectedValue.ToString <> dtRegistros.Rows(0).Item("Versión").ToString Then
                                    .litError.Text = "Versión Inválida, favor de validarlo"
                                Else
                                    'Depurar Tabla (Eliminar renglones Vacíos)
                                    Dim valor As String = ""
                                    Dim i As Integer = 0
                                    Dim iFin As Integer
                                    Dim numReg As Integer = 0
                                    Dim numRegV As Integer = 0
                                    'Se estable el número de registros a depurar
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
                                    Dim valStatus As Integer
                                    Dim cIns As Integer = 0
                                    Dim cAct As Integer = 0
                                    Dim cOmi As Integer = 0
                                    .txtUuidOmitido.Text = ""

                                    If .ddlTipo.SelectedValue = "G" Then
                                        'Tabla General [dt_factura]
                                        Select Case .ddlVersion.SelectedValue
                                            Case "3.3"
                                                ' * * *  Versión 3.3  * * *
                                                For i = 0 To dtRegistros.Rows.Count - 1
                                                    Dim banRegimenF As Integer = 0
                                                    'Validar el Régimen Fiscal [Régimen Simplificado de Confianza] y longitud del RFC del Emisor = 13 LNJ 18May22
                                                    If dtRegistros.Rows(i).Item(11).ToString = "626-Régimen Simplificado de Confianza" And (Len(dtRegistros.Rows(i).Item(9).ToString) = 13) Then
                                                        'Validar que el valor de [tot_ret_isr] sea mayor a cero
                                                        If Val(dtRegistros.Rows(i).Item(29).ToString) <= 0 Then
                                                            banRegimenF = 1
                                                        End If
                                                    End If

                                                    If banRegimenF = 0 Then
                                                        SCMValores.Parameters.Clear()
                                                        SCMValores.CommandText = "select case when (select count(*) from dt_factura where uuid = @uuid) = 0 then 0 else (select MAX(id_dt_factura) from dt_factura where uuid = @uuid) end as valor"
                                                        SCMValores.Parameters.AddWithValue("@uuid", dtRegistros.Rows(i).Item(1).ToString)
                                                        ConexionBD.Open()
                                                        valReg = SCMValores.ExecuteScalar
                                                        ConexionBD.Close()

                                                        If valReg > 0 Then
                                                            'Ya existe el registro, por lo que solo se actualiza
                                                            SCMValores.CommandText = "update dt_factura set id_usr_modif = @id_usr_modif, fecha_modif = @fecha_modif, no_ = @no_, uuid = @uuid, fecha_emision = @fecha_emision, fecha_timbrado = @fecha_timbrado, movimiento = @movimiento, version = @version, " +
                                                                                 "                      rfc_receptor = @rfc_receptor, razon_receptor = @razon_receptor, rfc_emisor = @rfc_emisor, razon_emisor = @razon_emisor, regimen_fiscal = @regimen_fiscal, pac_certificador = @pac_certificador, " +
                                                                                 "                      estatus = @estatus, tipo = @tipo, tipo_comprobante = @tipo_comprobante, serie = @serie, folio = @folio, lugar_exp = @lugar_exp, metodo_pago = @metodo_pago, forma_pago = @forma_pago, cond_pago = @cond_pago, " +
                                                                                 "                      tipo_cambio = @tipo_cambio, moneda = @moneda, uso_cfdi = @uso_cfdi, descripcion = @descripcion, tot_tras_iva = @tot_tras_iva, tot_tras_ieps = @tot_tras_ieps, tot_ret_iva = @tot_ret_iva, tot_ret_ieps = @tot_ret_ieps, " +
                                                                                 "                      tot_ret_isr = @tot_ret_isr, tot_imp_tras = @tot_imp_tras, tot_imp_ret = @tot_imp_ret, subtotal = @subtotal, descuentos = @descuentos, importe = @importe, fecha_cancel = @fecha_cancel " +
                                                                                 "where id_dt_factura = @id_dt_factura "
                                                            SCMValores.Parameters.AddWithValue("@id_dt_factura", valReg)
                                                            SCMValores.Parameters.AddWithValue("@id_usr_modif", Val(._txtIdUsuario.Text))
                                                            SCMValores.Parameters.AddWithValue("@fecha_modif", fecha)

                                                            'Validar si existe algún proceso que haya utilizado el folio, si no es así y el folio fue cancelado, se marca como Cancelado por el SAT
                                                            SCMValoresTemp.Parameters.Clear()
                                                            SCMValoresTemp.CommandText = "select (select count(*) from dt_comp left join ms_comp on ms_comp.id_ms_comp = dt_comp.id_ms_comp where CFDI = @uuid and status <> 'Z') + (select count(*) from ms_factura where CFDI = @uuid and status <> 'Z') as valStatus"
                                                            SCMValoresTemp.Parameters.AddWithValue("@uuid", dtRegistros.Rows(i).Item(1).ToString)
                                                            ConexionBD.Open()
                                                            valStatus = SCMValoresTemp.ExecuteScalar
                                                            ConexionBD.Close()
                                                            If valStatus = 0 And dtRegistros.Rows(i).Item(13).ToString = "CANCELADO" Then
                                                                SCMValores.Parameters.AddWithValue("@status", "ZSAT")
                                                            End If

                                                            cAct = cAct + 1
                                                        Else
                                                            'No existe el registro, por lo que se inserta
                                                            SCMValores.CommandText = "insert into dt_factura ( id_usr_carga,  fecha_carga, id_usr_modif,  fecha_modif,  no_,  uuid,  fecha_emision,  fecha_timbrado,  movimiento,  version,  rfc_receptor,  razon_receptor,  rfc_emisor,  razon_emisor,  regimen_fiscal,  pac_certificador,  estatus,  tipo,  tipo_comprobante,  serie,  folio,  lugar_exp,  metodo_pago,  forma_pago,  cond_pago,  tipo_cambio,  moneda,  uso_cfdi,  descripcion,  tot_tras_iva,  tot_tras_ieps,  tot_ret_iva,  tot_ret_ieps,  tot_ret_isr,  tot_imp_tras,  tot_imp_ret,  subtotal,  descuentos,  importe,  fecha_cancel,  status) " +
                                                                                 "                values (@id_usr_carga, @fecha_carga,@id_usr_modif, @fecha_modif, @no_, @uuid, @fecha_emision, @fecha_timbrado, @movimiento, @version, @rfc_receptor, @razon_receptor, @rfc_emisor, @razon_emisor, @regimen_fiscal, @pac_certificador, @estatus, @tipo, @tipo_comprobante, @serie, @folio, @lugar_exp, @metodo_pago, @forma_pago, @cond_pago, @tipo_cambio, @moneda, @uso_cfdi, @descripcion, @tot_tras_iva, @tot_tras_ieps, @tot_ret_iva, @tot_ret_ieps, @tot_ret_isr, @tot_imp_tras, @tot_imp_ret, @subtotal, @descuentos, @importe, @fecha_cancel, @status) "
                                                            SCMValores.Parameters.AddWithValue("@id_usr_carga", Val(._txtIdUsuario.Text))
                                                            SCMValores.Parameters.AddWithValue("@fecha_carga", fecha)
                                                            SCMValores.Parameters.AddWithValue("@id_usr_modif", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@fecha_modif", DBNull.Value)
                                                            If dtRegistros.Rows(i).Item(13).ToString = "VIGENTE" Or dtRegistros.Rows(i).Item(13).ToString = "SIN_ESTATUS" Then
                                                                SCMValores.Parameters.AddWithValue("@status", "P")
                                                            Else
                                                                SCMValores.Parameters.AddWithValue("@status", "ZSAT")
                                                            End If
                                                            cIns = cIns + 1
                                                        End If

                                                        SCMValores.Parameters.AddWithValue("@no_", Val(dtRegistros.Rows(i).Item(0).ToString))
                                                        SCMValores.Parameters.AddWithValue("@fecha_emision", CDate(dtRegistros.Rows(i).Item(2).ToString))
                                                        SCMValores.Parameters.AddWithValue("@fecha_timbrado", dtRegistros.Rows(i).Item(4).ToString)
                                                        SCMValores.Parameters.AddWithValue("@movimiento", dtRegistros.Rows(i).Item(5).ToString)
                                                        SCMValores.Parameters.AddWithValue("@version", dtRegistros.Rows(i).Item(6).ToString)
                                                        SCMValores.Parameters.AddWithValue("@rfc_receptor", dtRegistros.Rows(i).Item(7).ToString)
                                                        SCMValores.Parameters.AddWithValue("@razon_receptor", dtRegistros.Rows(i).Item(8).ToString)
                                                        SCMValores.Parameters.AddWithValue("@rfc_emisor", dtRegistros.Rows(i).Item(9).ToString)
                                                        SCMValores.Parameters.AddWithValue("@razon_emisor", dtRegistros.Rows(i).Item(10).ToString)
                                                        SCMValores.Parameters.AddWithValue("@regimen_fiscal", dtRegistros.Rows(i).Item(11).ToString)
                                                        SCMValores.Parameters.AddWithValue("@pac_certificador", dtRegistros.Rows(i).Item(12).ToString)
                                                        SCMValores.Parameters.AddWithValue("@estatus", dtRegistros.Rows(i).Item(13).ToString)
                                                        SCMValores.Parameters.AddWithValue("@tipo", dtRegistros.Rows(i).Item(14).ToString)
                                                        SCMValores.Parameters.AddWithValue("@tipo_comprobante", dtRegistros.Rows(i).Item(14).ToString)
                                                        SCMValores.Parameters.AddWithValue("@serie", dtRegistros.Rows(i).Item(15).ToString)
                                                        SCMValores.Parameters.AddWithValue("@folio", dtRegistros.Rows(i).Item(16).ToString)
                                                        SCMValores.Parameters.AddWithValue("@lugar_exp", dtRegistros.Rows(i).Item(17).ToString)
                                                        SCMValores.Parameters.AddWithValue("@metodo_pago", dtRegistros.Rows(i).Item(18).ToString)
                                                        SCMValores.Parameters.AddWithValue("@forma_pago", dtRegistros.Rows(i).Item(19).ToString)
                                                        SCMValores.Parameters.AddWithValue("@cond_pago", dtRegistros.Rows(i).Item(20).ToString)
                                                        If dtRegistros.Rows(i).Item(21).ToString = "" Then
                                                            SCMValores.Parameters.AddWithValue("@tipo_cambio", DBNull.Value)
                                                        Else
                                                            SCMValores.Parameters.AddWithValue("@tipo_cambio", Val(dtRegistros.Rows(i).Item(21).ToString))
                                                        End If
                                                        SCMValores.Parameters.AddWithValue("@moneda", dtRegistros.Rows(i).Item(22).ToString)
                                                        SCMValores.Parameters.AddWithValue("@uso_cfdi", dtRegistros.Rows(i).Item(23).ToString)
                                                        SCMValores.Parameters.AddWithValue("@descripcion", dtRegistros.Rows(i).Item(24).ToString)
                                                        SCMValores.Parameters.AddWithValue("@tot_tras_iva", Val(dtRegistros.Rows(i).Item(25).ToString))
                                                        SCMValores.Parameters.AddWithValue("@tot_tras_ieps", Val(dtRegistros.Rows(i).Item(26).ToString))
                                                        SCMValores.Parameters.AddWithValue("@tot_ret_iva", Val(dtRegistros.Rows(i).Item(27).ToString))
                                                        SCMValores.Parameters.AddWithValue("@tot_ret_ieps", Val(dtRegistros.Rows(i).Item(28).ToString))
                                                        SCMValores.Parameters.AddWithValue("@tot_ret_isr", Val(dtRegistros.Rows(i).Item(29).ToString))
                                                        SCMValores.Parameters.AddWithValue("@tot_imp_tras", Val(dtRegistros.Rows(i).Item(30).ToString))
                                                        SCMValores.Parameters.AddWithValue("@tot_imp_ret", Val(dtRegistros.Rows(i).Item(31).ToString))
                                                        SCMValores.Parameters.AddWithValue("@subtotal", Val(dtRegistros.Rows(i).Item(32).ToString))
                                                        SCMValores.Parameters.AddWithValue("@descuentos", Val(dtRegistros.Rows(i).Item(33).ToString))
                                                        SCMValores.Parameters.AddWithValue("@importe", dtRegistros.Rows(i).Item(34).ToString)
                                                        SCMValores.Parameters.AddWithValue("@fecha_cancel", dtRegistros.Rows(i).Item(40).ToString)

                                                        ConexionBD.Open()
                                                        SCMValores.ExecuteNonQuery()
                                                        ConexionBD.Close()
                                                    Else
                                                        'Se Omite el UUID
                                                        cOmi = cOmi + 1

                                                        'Se agrega el Folio Fiscal a la lista
                                                        .txtUuidOmitido.Text = .txtUuidOmitido.Text + dtRegistros.Rows(i).Item(1).ToString + Chr(13) & Chr(10)
                                                    End If
                                                Next
                                            Case "4.0"
                                                ' * * *  Versión 4.0  * * *
                                                For i = 0 To dtRegistros.Rows.Count - 1

                                                    '' codigo original antes de implementacion Dayra
                                                    'Dim banRegimenF As Integer = 0
                                                    ''Validar el Régimen Fiscal [Régimen Simplificado de Confianza] LNJ 18May22
                                                    'If dtRegistros.Rows(i).Item(13).ToString = "626-Régimen Simplificado de Confianza" And (Len(dtRegistros.Rows(i).Item(11).ToString) = 13) Then
                                                    '    'Validar que el valor de [tot_ret_isr] sea mayor a cero
                                                    '    If Val(dtRegistros.Rows(i).Item(32).ToString) <= 0 Then
                                                    '        banRegimenF = 1
                                                    '    End If
                                                    'End If
                                                    '' Implementacion de validacion CFDI  Dayra 17/04/2023

                                                    'Dim banRegimenF As Integer = 0
                                                    Dim banRegimenFR As Integer = 0
                                                    'Validar el Régimen Fiscal [Régimen Simplificado de Confianza] LNJ 18May22
                                                    If dtRegistros.Rows(i).Item(13).ToString = "626-Régimen Simplificado de Confianza" And (Len(dtRegistros.Rows(i).Item(11).ToString) = 13) Then
                                                        'Validar que el valor de [tot_ret_isr] sea mayor a cero
                                                        If Val(dtRegistros.Rows(i).Item(32).ToString) <= 0 Then
                                                            banRegimenFR = 1
                                                        End If
                                                    End If
                                                    If dtRegistros.Rows(i).Item(7) = "DCM9701204H0" Or dtRegistros.Rows(i).Item(7) = "CEP061206S12" Or dtRegistros.Rows(i).Item(7) = "DIB0806232T0" Or dtRegistros.Rows(i).Item(7) = "EXP080731UW6" Or dtRegistros.Rows(i).Item(7) = "OCP951208EV1" Or dtRegistros.Rows(i).Item(7) = "CPE011031CS9" Then
                                                        If dtRegistros.Rows(i).Item(9) <> "601-General de Ley Personas Morales" Then
                                                            banRegimenFR = 1
                                                        End If
                                                    End If

                                                    If dtRegistros.Rows(i).Item(7) = "TCA980629FC6" Or dtRegistros.Rows(i).Item(7) = "TRA750915RF6" Or dtRegistros.Rows(i).Item(7) = "TES070110LR3" Or dtRegistros.Rows(i).Item(7) = "TEC920831QJA" Or dtRegistros.Rows(i).Item(7) = "TIN0906116S1" Then
                                                        If dtRegistros.Rows(i).Item(9) <> "624-Coordinados" Then
                                                            banRegimenFR = 1
                                                        End If
                                                    End If

                                                    If banRegimenFR = 0 Then

                                                        '' fin cambios CFDI  Dayra 17/04/2023
                                                        SCMValores.Parameters.Clear()
                                                        SCMValores.CommandText = "select case when (select count(*) from dt_factura where uuid = @uuid) = 0 then 0 else (select MAX(id_dt_factura) from dt_factura where uuid = @uuid) end as valor"
                                                        SCMValores.Parameters.AddWithValue("@uuid", dtRegistros.Rows(i).Item(1).ToString)
                                                        ConexionBD.Open()
                                                        valReg = SCMValores.ExecuteScalar
                                                        ConexionBD.Close()

                                                        If valReg > 0 Then
                                                            'Ya existe el registro, por lo que solo se actualiza
                                                            SCMValores.CommandText = "update dt_factura set id_usr_modif = @id_usr_modif, fecha_modif = @fecha_modif, no_ = @no_, uuid = @uuid, fecha_emision = @fecha_emision, fecha_timbrado = @fecha_timbrado, movimiento = @movimiento, version = @version, " +
                                                                                 "                      rfc_receptor = @rfc_receptor, razon_receptor = @razon_receptor, rfc_emisor = @rfc_emisor, razon_emisor = @razon_emisor, regimen_fiscal = @regimen_fiscal, pac_certificador = @pac_certificador, " +
                                                                                 "                      estatus = @estatus, tipo = @tipo, tipo_comprobante = @tipo_comprobante, serie = @serie, folio = @folio, lugar_exp = @lugar_exp, metodo_pago = @metodo_pago, forma_pago = @forma_pago, cond_pago = @cond_pago, " +
                                                                                 "                      tipo_cambio = @tipo_cambio, moneda = @moneda, uso_cfdi = @uso_cfdi, descripcion = @descripcion, tot_tras_iva = @tot_tras_iva, tot_tras_ieps = @tot_tras_ieps, tot_ret_iva = @tot_ret_iva, tot_ret_ieps = @tot_ret_ieps, " +
                                                                                 "                      tot_ret_isr = @tot_ret_isr, tot_imp_tras = @tot_imp_tras, tot_imp_ret = @tot_imp_ret, subtotal = @subtotal, descuentos = @descuentos, importe = @importe, fecha_cancel = @fecha_cancel " +
                                                                                 "where id_dt_factura = @id_dt_factura "
                                                            SCMValores.Parameters.AddWithValue("@id_dt_factura", valReg)
                                                            SCMValores.Parameters.AddWithValue("@id_usr_modif", Val(._txtIdUsuario.Text))
                                                            SCMValores.Parameters.AddWithValue("@fecha_modif", fecha)

                                                            'Validar si existe algún proceso que haya utilizado el folio, si no es así y el folio fue cancelado, se marca como Cancelado por el SAT
                                                            SCMValoresTemp.Parameters.Clear()
                                                            SCMValoresTemp.CommandText = "select (select count(*) from dt_comp left join ms_comp on ms_comp.id_ms_comp = dt_comp.id_ms_comp where CFDI = @uuid and status <> 'Z') + (select count(*) from ms_factura where CFDI = @uuid and status <> 'Z') as valStatus"
                                                            SCMValoresTemp.Parameters.AddWithValue("@uuid", dtRegistros.Rows(i).Item(1).ToString)
                                                            ConexionBD.Open()
                                                            valStatus = SCMValoresTemp.ExecuteScalar
                                                            ConexionBD.Close()
                                                            If valStatus = 0 And dtRegistros.Rows(i).Item(16).ToString = "CANCELADO" Then
                                                                SCMValores.Parameters.AddWithValue("@status", "ZSAT")
                                                            End If

                                                            cAct = cAct + 1
                                                        Else
                                                            'No existe el registro, por lo que se inserta
                                                            SCMValores.CommandText = "insert into dt_factura ( id_usr_carga,  fecha_carga, id_usr_modif,  fecha_modif,  no_,  uuid,  fecha_emision,  fecha_timbrado,  movimiento,  version,  rfc_receptor,  razon_receptor,  rfc_emisor,  razon_emisor,  regimen_fiscal,  pac_certificador,  estatus,  tipo,  tipo_comprobante,  serie,  folio,  lugar_exp,  metodo_pago,  forma_pago,  cond_pago,  tipo_cambio,  moneda,  uso_cfdi,  descripcion,  tot_tras_iva,  tot_tras_ieps,  tot_ret_iva,  tot_ret_ieps,  tot_ret_isr,  tot_imp_tras,  tot_imp_ret,  subtotal,  descuentos,  importe,  fecha_cancel,  status) " +
                                                                                 "                values (@id_usr_carga, @fecha_carga,@id_usr_modif, @fecha_modif, @no_, @uuid, @fecha_emision, @fecha_timbrado, @movimiento, @version, @rfc_receptor, @razon_receptor, @rfc_emisor, @razon_emisor, @regimen_fiscal, @pac_certificador, @estatus, @tipo, @tipo_comprobante, @serie, @folio, @lugar_exp, @metodo_pago, @forma_pago, @cond_pago, @tipo_cambio, @moneda, @uso_cfdi, @descripcion, @tot_tras_iva, @tot_tras_ieps, @tot_ret_iva, @tot_ret_ieps, @tot_ret_isr, @tot_imp_tras, @tot_imp_ret, @subtotal, @descuentos, @importe, @fecha_cancel, @status) "
                                                            SCMValores.Parameters.AddWithValue("@id_usr_carga", Val(._txtIdUsuario.Text))
                                                            SCMValores.Parameters.AddWithValue("@fecha_carga", fecha)
                                                            SCMValores.Parameters.AddWithValue("@id_usr_modif", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@fecha_modif", DBNull.Value)
                                                            If dtRegistros.Rows(i).Item(16).ToString = "VIGENTE" Or dtRegistros.Rows(i).Item(16).ToString = "SIN_ESTATUS" Then
                                                                SCMValores.Parameters.AddWithValue("@status", "P")
                                                            Else
                                                                SCMValores.Parameters.AddWithValue("@status", "ZSAT")
                                                            End If
                                                            cIns = cIns + 1
                                                        End If

                                                        SCMValores.Parameters.AddWithValue("@no_", Val(dtRegistros.Rows(i).Item(0).ToString))
                                                        SCMValores.Parameters.AddWithValue("@fecha_emision", CDate(dtRegistros.Rows(i).Item(2).ToString))
                                                        SCMValores.Parameters.AddWithValue("@fecha_timbrado", dtRegistros.Rows(i).Item(4).ToString)
                                                        SCMValores.Parameters.AddWithValue("@movimiento", dtRegistros.Rows(i).Item(5).ToString)
                                                        SCMValores.Parameters.AddWithValue("@version", dtRegistros.Rows(i).Item(6).ToString)
                                                        SCMValores.Parameters.AddWithValue("@rfc_receptor", dtRegistros.Rows(i).Item(7).ToString)
                                                        SCMValores.Parameters.AddWithValue("@razon_receptor", dtRegistros.Rows(i).Item(8).ToString)
                                                        SCMValores.Parameters.AddWithValue("@rfc_emisor", dtRegistros.Rows(i).Item(11).ToString)
                                                        SCMValores.Parameters.AddWithValue("@razon_emisor", dtRegistros.Rows(i).Item(12).ToString)
                                                        SCMValores.Parameters.AddWithValue("@regimen_fiscal", dtRegistros.Rows(i).Item(13).ToString)
                                                        SCMValores.Parameters.AddWithValue("@pac_certificador", dtRegistros.Rows(i).Item(15).ToString)
                                                        SCMValores.Parameters.AddWithValue("@estatus", dtRegistros.Rows(i).Item(16).ToString)
                                                        SCMValores.Parameters.AddWithValue("@tipo", dtRegistros.Rows(i).Item(17).ToString)
                                                        SCMValores.Parameters.AddWithValue("@tipo_comprobante", dtRegistros.Rows(i).Item(17).ToString)
                                                        SCMValores.Parameters.AddWithValue("@serie", dtRegistros.Rows(i).Item(18).ToString)
                                                        SCMValores.Parameters.AddWithValue("@folio", dtRegistros.Rows(i).Item(19).ToString)
                                                        SCMValores.Parameters.AddWithValue("@lugar_exp", dtRegistros.Rows(i).Item(20).ToString)
                                                        SCMValores.Parameters.AddWithValue("@metodo_pago", dtRegistros.Rows(i).Item(21).ToString)
                                                        SCMValores.Parameters.AddWithValue("@forma_pago", dtRegistros.Rows(i).Item(22).ToString)
                                                        SCMValores.Parameters.AddWithValue("@cond_pago", dtRegistros.Rows(i).Item(23).ToString)
                                                        If dtRegistros.Rows(i).Item(24).ToString = "" Then
                                                            SCMValores.Parameters.AddWithValue("@tipo_cambio", DBNull.Value)
                                                        Else
                                                            SCMValores.Parameters.AddWithValue("@tipo_cambio", Val(dtRegistros.Rows(i).Item(24).ToString))
                                                        End If
                                                        SCMValores.Parameters.AddWithValue("@moneda", dtRegistros.Rows(i).Item(25).ToString)
                                                        SCMValores.Parameters.AddWithValue("@uso_cfdi", dtRegistros.Rows(i).Item(26).ToString)
                                                        SCMValores.Parameters.AddWithValue("@descripcion", dtRegistros.Rows(i).Item(27).ToString)
                                                        SCMValores.Parameters.AddWithValue("@tot_tras_iva", Val(dtRegistros.Rows(i).Item(28).ToString))
                                                        SCMValores.Parameters.AddWithValue("@tot_tras_ieps", Val(dtRegistros.Rows(i).Item(29).ToString))
                                                        SCMValores.Parameters.AddWithValue("@tot_ret_iva", Val(dtRegistros.Rows(i).Item(30).ToString))
                                                        SCMValores.Parameters.AddWithValue("@tot_ret_ieps", Val(dtRegistros.Rows(i).Item(31).ToString))
                                                        SCMValores.Parameters.AddWithValue("@tot_ret_isr", Val(dtRegistros.Rows(i).Item(32).ToString))
                                                        SCMValores.Parameters.AddWithValue("@tot_imp_tras", Val(dtRegistros.Rows(i).Item(33).ToString))
                                                        SCMValores.Parameters.AddWithValue("@tot_imp_ret", Val(dtRegistros.Rows(i).Item(34).ToString))
                                                        SCMValores.Parameters.AddWithValue("@subtotal", Val(dtRegistros.Rows(i).Item(35).ToString))
                                                        SCMValores.Parameters.AddWithValue("@descuentos", Val(dtRegistros.Rows(i).Item(36).ToString))
                                                        SCMValores.Parameters.AddWithValue("@importe", dtRegistros.Rows(i).Item(37).ToString)
                                                        SCMValores.Parameters.AddWithValue("@fecha_cancel", dtRegistros.Rows(i).Item(43).ToString)

                                                        ConexionBD.Open()
                                                        SCMValores.ExecuteNonQuery()
                                                        ConexionBD.Close()
                                                    Else
                                                        'Se Omite el UUID
                                                        cOmi = cOmi + 1

                                                        'Se agrega el Folio Fiscal a la lista
                                                        .txtUuidOmitido.Text = .txtUuidOmitido.Text + dtRegistros.Rows(i).Item(1).ToString + Chr(13) & Chr(10)
                                                    End If
                                                Next
                                            Case Else
                                                .litError.Text = "Versión Inválida"
                                        End Select
                                    Else
                                        'Tabla Detalle [dt_factura_linea]
                                        Select Case .ddlVersion.SelectedValue
                                            Case "3.3"
                                                ' * * *  Versión 3.3  * * *
                                                For i = 0 To dtRegistros.Rows.Count - 1
                                                    SCMValores.Parameters.Clear()
                                                    SCMValores.CommandText = "select count(*) as valor from dt_factura_linea where uuid = @uuid and fecha_carga <> @fecha_carga"
                                                    SCMValores.Parameters.AddWithValue("@fecha_carga", fecha)
                                                    SCMValores.Parameters.AddWithValue("@uuid", dtRegistros.Rows(i).Item(2).ToString)
                                                    ConexionBD.Open()
                                                    valReg = SCMValores.ExecuteScalar
                                                    ConexionBD.Close()

                                                    If valReg > 0 Then
                                                        'Ya existe el registro, por lo que solo se elimina
                                                        SCMValores.CommandText = "delete from dt_factura_linea where uuid = @uuid "
                                                        ConexionBD.Open()
                                                        SCMValores.ExecuteNonQuery()
                                                        ConexionBD.Close()
                                                        'Incrementar contador
                                                        cAct = cAct + 1
                                                    Else
                                                        'No existe el registro, por lo que solo se inserta
                                                        cIns = cIns + 1
                                                    End If

                                                    Dim queryInto As String
                                                    Dim queryVal As String

                                                    queryInto = "insert into dt_factura_linea ( id_usr_carga,  fecha_carga,  actualizado,  no_,  no_comp,  uuid,  version,  fecha_emision,  fecha_certificacion,  rfc_emisor,  razon_receptor,  rfc_receptor,  razon_emisor,  pac_certificador,  estatus,  tipo,  tipo_comprobante,  serie,  folio,  moneda,  forma_pago,  metodo_pago,  clave_producto,  clave_unidad,  unidad,  cantidad,  num_identificacion,  descripcion,  valor_unitario,  importe,  descuento"
                                                    queryVal = "                       values (@id_usr_carga, @fecha_carga, @actualizado, @no_, @no_comp, @uuid, @version, @fecha_emision, @fecha_certificacion, @rfc_emisor, @razon_receptor, @rfc_receptor, @razon_emisor, @pac_certificador, @estatus, @tipo, @tipo_comprobante, @serie, @folio, @moneda, @forma_pago, @metodo_pago, @clave_producto, @clave_unidad, @unidad, @cantidad, @num_identificacion, @descripcion, @valor_unitario, @importe, @descuento"

                                                    'Total Impuestos
                                                    If dtRegistros.Columns.Contains("Total Trasladado IVA") Then
                                                        queryInto = queryInto + ",  tot_tras_iva,  tot_tras_ieps"
                                                        queryVal = queryVal + ", @tot_tras_iva, @tot_tras_ieps"
                                                    End If
                                                    'Total Retenciones
                                                    If dtRegistros.Columns.Contains("Total Retenido IVA") Then
                                                        queryInto = queryInto + ",  tot_ret_iva,  tot_ret_ieps,  tot_ret_isr"
                                                        queryVal = queryVal + ", @tot_ret_iva, @tot_ret_ieps, @tot_ret_isr"
                                                    End If
                                                    'Impuestos 1
                                                    If dtRegistros.Columns.Contains("Trasladado - Base 1") Then
                                                        queryInto = queryInto + ",  base_tras_1,  impuesto_tras_1,  tipo_tras_1,  tasa_tras_1,  importe_tras_1"
                                                        queryVal = queryVal + ", @base_tras_1, @impuesto_tras_1, @tipo_tras_1, @tasa_tras_1, @importe_tras_1"
                                                    End If
                                                    'Impuestos 2
                                                    If dtRegistros.Columns.Contains("Trasladado - Base 2") Then
                                                        queryInto = queryInto + ",  base_tras_2,  impuesto_tras_2,  tipo_tras_2,  tasa_tras_2,  importe_tras_2"
                                                        queryVal = queryVal + ", @base_tras_2, @impuesto_tras_2, @tipo_tras_2, @tasa_tras_2, @importe_tras_2"
                                                    End If
                                                    'Impuestos 3
                                                    If dtRegistros.Columns.Contains("Trasladado - Base 3") Then
                                                        queryInto = queryInto + ",  base_tras_3,  impuesto_tras_3,  tipo_tras_3,  tasa_tras_3,  importe_tras_3"
                                                        queryVal = queryVal + ", @base_tras_3, @impuesto_tras_3, @tipo_tras_3, @tasa_tras_3, @importe_tras_3"
                                                    End If
                                                    'Impuestos 4
                                                    If dtRegistros.Columns.Contains("Trasladado - Base 4") Then
                                                        queryInto = queryInto + ",  base_tras_4,  impuesto_tras_4,  tipo_tras_4,  tasa_tras_4,  importe_tras_4"
                                                        queryVal = queryVal + ", @base_tras_4, @impuesto_tras_4, @tipo_tras_4, @tasa_tras_4, @importe_tras_4"
                                                    End If
                                                    'Retención 1
                                                    If dtRegistros.Columns.Contains("Retenido - Base 1") Then
                                                        queryInto = queryInto + ",  base_ret_1,  impuesto_ret_1,  tipo_ret_1,  tasa_ret_1,  importe_ret_1"
                                                        queryVal = queryVal + ", @base_ret_1, @impuesto_ret_1, @tipo_ret_1, @tasa_ret_1, @importe_ret_1"
                                                    End If
                                                    'Retención 2
                                                    If dtRegistros.Columns.Contains("Retenido - Base 2") Then
                                                        queryInto = queryInto + ",  base_ret_2,  impuesto_ret_2,  tipo_ret_2,  tasa_ret_2,  importe_ret_2"
                                                        queryVal = queryVal + ", @base_ret_2, @impuesto_ret_2, @tipo_ret_2, @tasa_ret_2, @importe_ret_2"
                                                    End If
                                                    'Retención 3
                                                    If dtRegistros.Columns.Contains("Retenido - Base 3") Then
                                                        queryInto = queryInto + ",  base_ret_3,  impuesto_ret_3,  tipo_ret_3,  tasa_ret_3,  importe_ret_3"
                                                        queryVal = queryVal + ", @base_ret_3, @impuesto_ret_3, @tipo_ret_3, @tasa_ret_3, @importe_ret_3"
                                                    End If
                                                    'Retención 4
                                                    If dtRegistros.Columns.Contains("Retenido - Base 4") Then
                                                        queryInto = queryInto + ",  base_ret_4,  impuesto_ret_4,  tipo_ret_4,  tasa_ret_4,  importe_ret_4"
                                                        queryVal = queryVal + ", @base_ret_4, @impuesto_ret_4, @tipo_ret_4, @tasa_ret_4, @importe_ret_4"
                                                    End If
                                                    queryInto = queryInto + ") "
                                                    queryVal = queryVal + ") "

                                                    SCMValores.CommandText = queryInto + queryVal

                                                    ''Versión Anterior
                                                    'Select Case dtRegistros.Columns().Count
                                                    '    Case 33 '28
                                                    '        'Sin Impuestos
                                                    '        SCMValores.CommandText = "insert into dt_factura_linea ( id_usr_carga,  fecha_carga,  actualizado,  no_,  no_comp,  uuid,  version,  fecha_emision,  fecha_certificacion,  rfc_emisor,  razon_receptor,  rfc_receptor,  razon_emisor,  pac_certificador,  estatus,  tipo,  tipo_comprobante,  serie,  folio,  moneda,  forma_pago,  metodo_pago,  clave_producto,  clave_unidad,  unidad,  cantidad,  num_identificacion,  descripcion,  valor_unitario,  importe,  descuento) " + _
                                                    '                                 "                      values (@id_usr_carga, @fecha_carga, @actualizado, @no_, @no_comp, @uuid, @version, @fecha_emision, @fecha_certificacion, @rfc_emisor, @razon_receptor, @rfc_receptor, @razon_emisor, @pac_certificador, @estatus, @tipo, @tipo_comprobante, @serie, @folio, @moneda, @forma_pago, @metodo_pago, @clave_producto, @clave_unidad, @unidad, @cantidad, @num_identificacion, @descripcion, @valor_unitario, @importe, @descuento) "
                                                    '    Case 43 '40 '35
                                                    '        'Con 1 tipo de Impuesto sin Retención
                                                    '        SCMValores.CommandText = "insert into dt_factura_linea ( id_usr_carga,  fecha_carga,  actualizado,  no_,  no_comp,  uuid,  version,  fecha_emision,  fecha_certificacion,  rfc_emisor,  razon_receptor,  rfc_receptor,  razon_emisor,  pac_certificador,  estatus,  tipo,  tipo_comprobante,  serie,  folio,  moneda,  forma_pago,  metodo_pago,  clave_producto,  clave_unidad,  unidad,  cantidad,  num_identificacion,  descripcion,  valor_unitario,  importe,  descuento,  tot_tras_iva,  tot_tras_ieps,  base_tras_1,  impuesto_tras_1,  tipo_tras_1,  tasa_tras_1,  importe_tras_1) " + _
                                                    '                                 "                      values (@id_usr_carga, @fecha_carga, @actualizado, @no_, @no_comp, @uuid, @version, @fecha_emision, @fecha_certificacion, @rfc_emisor, @razon_receptor, @rfc_receptor, @razon_emisor, @pac_certificador, @estatus, @tipo, @tipo_comprobante, @serie, @folio, @moneda, @forma_pago, @metodo_pago, @clave_producto, @clave_unidad, @unidad, @cantidad, @num_identificacion, @descripcion, @valor_unitario, @importe, @descuento, @tot_tras_iva, @tot_tras_ieps, @base_tras_1, @impuesto_tras_1, @tipo_tras_1, @tasa_tras_1, @importe_tras_1) "
                                                    '    Case 45 '40
                                                    '        'Con 2 tipos de Impuestos sin Retención
                                                    '        SCMValores.CommandText = "insert into dt_factura_linea ( id_usr_carga,  fecha_carga,  actualizado,  no_,  no_comp,  uuid,  version,  fecha_emision,  fecha_certificacion,  rfc_emisor,  razon_receptor,  rfc_receptor,  razon_emisor,  pac_certificador,  estatus,  tipo,  tipo_comprobante,  serie,  folio,  moneda,  forma_pago,  metodo_pago,  clave_producto,  clave_unidad,  unidad,  cantidad,  num_identificacion,  descripcion,  valor_unitario,  importe,  descuento,  tot_tras_iva,  tot_tras_ieps,  base_tras_1,  impuesto_tras_1,  tipo_tras_1,  tasa_tras_1,  importe_tras_1,  base_tras_2,  impuesto_tras_2,  tipo_tras_2,  tasa_tras_2,  importe_tras_2) " + _
                                                    '                                 "                      values (@id_usr_carga, @fecha_carga, @actualizado, @no_, @no_comp, @uuid, @version, @fecha_emision, @fecha_certificacion, @rfc_emisor, @razon_receptor, @rfc_receptor, @razon_emisor, @pac_certificador, @estatus, @tipo, @tipo_comprobante, @serie, @folio, @moneda, @forma_pago, @metodo_pago, @clave_producto, @clave_unidad, @unidad, @cantidad, @num_identificacion, @descripcion, @valor_unitario, @importe, @descuento, @tot_tras_iva, @tot_tras_ieps, @base_tras_1, @impuesto_tras_1, @tipo_tras_1, @tasa_tras_1, @importe_tras_1, @base_tras_2, @impuesto_tras_2, @tipo_tras_2, @tasa_tras_2, @importe_tras_2) "
                                                    '    Case 48 '43
                                                    '        'Con 1 tipo de Impuesto y 1 Retención
                                                    '        SCMValores.CommandText = "insert into dt_factura_linea ( id_usr_carga,  fecha_carga,  actualizado,  no_,  no_comp,  uuid,  version,  fecha_emision,  fecha_certificacion,  rfc_emisor,  razon_receptor,  rfc_receptor,  razon_emisor,  pac_certificador,  estatus,  tipo,  tipo_comprobante,  serie,  folio,  moneda,  forma_pago,  metodo_pago,  clave_producto,  clave_unidad,  unidad,  cantidad,  num_identificacion,  descripcion,  valor_unitario,  importe,  descuento,  tot_tras_iva,  tot_tras_ieps,  tot_ret_iva,  tot_ret_ieps,  tot_ret_isr,  base_tras_1,  impuesto_tras_1,  tipo_tras_1,  tasa_tras_1,  importe_tras_1,  base_ret_1,  impuesto_ret_1,  tipo_ret_1,  tasa_ret_1,  importe_ret_1) " + _
                                                    '                                 "                      values (@id_usr_carga, @fecha_carga, @actualizado, @no_, @no_comp, @uuid, @version, @fecha_emision, @fecha_certificacion, @rfc_emisor, @razon_receptor, @rfc_receptor, @razon_emisor, @pac_certificador, @estatus, @tipo, @tipo_comprobante, @serie, @folio, @moneda, @forma_pago, @metodo_pago, @clave_producto, @clave_unidad, @unidad, @cantidad, @num_identificacion, @descripcion, @valor_unitario, @importe, @descuento, @tot_tras_iva, @tot_tras_ieps, @tot_ret_iva, @tot_ret_ieps, @tot_ret_isr, @base_tras_1, @impuesto_tras_1, @tipo_tras_1, @tasa_tras_1, @importe_tras_1, @base_ret_1, @impuesto_ret_1, @tipo_ret_1, @tasa_ret_1, @importe_ret_1) "
                                                    '    Case 53 '48
                                                    '        'Con 2 tipos de Impuestos y 1 Retención
                                                    '        SCMValores.CommandText = "insert into dt_factura_linea ( id_usr_carga,  fecha_carga,  actualizado,  no_,  no_comp,  uuid,  version,  fecha_emision,  fecha_certificacion,  rfc_emisor,  razon_receptor,  rfc_receptor,  razon_emisor,  pac_certificador,  estatus,  tipo,  tipo_comprobante,  serie,  folio,  moneda,  forma_pago,  metodo_pago,  clave_producto,  clave_unidad,  unidad,  cantidad,  num_identificacion,  descripcion,  valor_unitario,  importe,  descuento,  tot_tras_iva,  tot_tras_ieps,  tot_ret_iva,  tot_ret_ieps,  tot_ret_isr,  base_tras_1,  impuesto_tras_1,  tipo_tras_1,  tasa_tras_1,  importe_tras_1,  base_ret_1,  impuesto_ret_1,  tipo_ret_1,  tasa_ret_1,  importe_ret_1,  base_tras_2,  impuesto_tras_2,  tipo_tras_2,  tasa_tras_2,  importe_tras_2) " + _
                                                    '                                 "                      values (@id_usr_carga, @fecha_carga, @actualizado, @no_, @no_comp, @uuid, @version, @fecha_emision, @fecha_certificacion, @rfc_emisor, @razon_receptor, @rfc_receptor, @razon_emisor, @pac_certificador, @estatus, @tipo, @tipo_comprobante, @serie, @folio, @moneda, @forma_pago, @metodo_pago, @clave_producto, @clave_unidad, @unidad, @cantidad, @num_identificacion, @descripcion, @valor_unitario, @importe, @descuento, @tot_tras_iva, @tot_tras_ieps, @tot_ret_iva, @tot_ret_ieps, @tot_ret_isr, @base_tras_1, @impuesto_tras_1, @tipo_tras_1, @tasa_tras_1, @importe_tras_1, @base_ret_1, @impuesto_ret_1, @tipo_ret_1, @tasa_ret_1, @importe_ret_1, @base_tras_2, @impuesto_tras_2, @tipo_tras_2, @tasa_tras_2, @importe_tras_2) "
                                                    '    Case 58 '53
                                                    '        'Con 2 tipos de Impuestos y 2 Retenciones
                                                    '        SCMValores.CommandText = "insert into dt_factura_linea ( id_usr_carga,  fecha_carga,  actualizado,  no_,  no_comp,  uuid,  version,  fecha_emision,  fecha_certificacion,  rfc_emisor,  razon_receptor,  rfc_receptor,  razon_emisor,  pac_certificador,  estatus,  tipo,  tipo_comprobante,  serie,  folio,  moneda,  forma_pago,  metodo_pago,  clave_producto,  clave_unidad,  unidad,  cantidad,  num_identificacion,  descripcion,  valor_unitario,  importe,  descuento,  tot_tras_iva,  tot_tras_ieps,  tot_ret_iva,  tot_ret_ieps,  tot_ret_isr,  base_tras_1,  impuesto_tras_1,  tipo_tras_1,  tasa_tras_1,  importe_tras_1,  base_ret_1,  impuesto_ret_1,  tipo_ret_1,  tasa_ret_1,  importe_ret_1,  base_tras_2,  impuesto_tras_2,  tipo_tras_2,  tasa_tras_2,  importe_tras_2,  base_ret_2,  impuesto_ret_2,  tipo_ret_2,  tasa_ret_2,  importe_ret_2) " + _
                                                    '                                 "                      values (@id_usr_carga, @fecha_carga, @actualizado, @no_, @no_comp, @uuid, @version, @fecha_emision, @fecha_certificacion, @rfc_emisor, @razon_receptor, @rfc_receptor, @razon_emisor, @pac_certificador, @estatus, @tipo, @tipo_comprobante, @serie, @folio, @moneda, @forma_pago, @metodo_pago, @clave_producto, @clave_unidad, @unidad, @cantidad, @num_identificacion, @descripcion, @valor_unitario, @importe, @descuento, @tot_tras_iva, @tot_tras_ieps, @tot_ret_iva, @tot_ret_ieps, @tot_ret_isr, @base_tras_1, @impuesto_tras_1, @tipo_tras_1, @tasa_tras_1, @importe_tras_1, @base_ret_1, @impuesto_ret_1, @tipo_ret_1, @tasa_ret_1, @importe_ret_1, @base_tras_2, @impuesto_tras_2, @tipo_tras_2, @tasa_tras_2, @importe_tras_2, @base_ret_2, @impuesto_ret_2, @tipo_ret_2, @tasa_ret_2, @importe_ret_2) "
                                                    '    Case 63 'XX
                                                    '        'Con 3 tipos de Impuestos y 2 Retenciones
                                                    '        SCMValores.CommandText = "insert into dt_factura_linea ( id_usr_carga,  fecha_carga,  actualizado,  no_,  no_comp,  uuid,  version,  fecha_emision,  fecha_certificacion,  rfc_emisor,  razon_receptor,  rfc_receptor,  razon_emisor,  pac_certificador,  estatus,  tipo,  tipo_comprobante,  serie,  folio,  moneda,  forma_pago,  metodo_pago,  clave_producto,  clave_unidad,  unidad,  cantidad,  num_identificacion,  descripcion,  valor_unitario,  importe,  descuento,  tot_tras_iva,  tot_tras_ieps,  tot_ret_iva,  tot_ret_ieps,  tot_ret_isr,  base_tras_1,  impuesto_tras_1,  tipo_tras_1,  tasa_tras_1,  importe_tras_1,  base_ret_1,  impuesto_ret_1,  tipo_ret_1,  tasa_ret_1,  importe_ret_1,  base_tras_2,  impuesto_tras_2,  tipo_tras_2,  tasa_tras_2,  importe_tras_2,  base_ret_2,  impuesto_ret_2,  tipo_ret_2,  tasa_ret_2,  importe_ret_2,  base_tras_3,  impuesto_tras_3,  tipo_tras_3,  tasa_tras_3,  importe_tras_3) " + _
                                                    '                                 "                      values (@id_usr_carga, @fecha_carga, @actualizado, @no_, @no_comp, @uuid, @version, @fecha_emision, @fecha_certificacion, @rfc_emisor, @razon_receptor, @rfc_receptor, @razon_emisor, @pac_certificador, @estatus, @tipo, @tipo_comprobante, @serie, @folio, @moneda, @forma_pago, @metodo_pago, @clave_producto, @clave_unidad, @unidad, @cantidad, @num_identificacion, @descripcion, @valor_unitario, @importe, @descuento, @tot_tras_iva, @tot_tras_ieps, @tot_ret_iva, @tot_ret_ieps, @tot_ret_isr, @base_tras_1, @impuesto_tras_1, @tipo_tras_1, @tasa_tras_1, @importe_tras_1, @base_ret_1, @impuesto_ret_1, @tipo_ret_1, @tasa_ret_1, @importe_ret_1, @base_tras_2, @impuesto_tras_2, @tipo_tras_2, @tasa_tras_2, @importe_tras_2, @base_ret_2, @impuesto_ret_2, @tipo_ret_2, @tasa_ret_2, @importe_ret_2, @base_tras_3, @impuesto_tras_3, @tipo_tras_3, @tasa_tras_3, @importe_tras_3) "
                                                    '    Case 68 '63
                                                    '        'Con 3 tipos de Impuestos y 3 Retenciones
                                                    '        SCMValores.CommandText = "insert into dt_factura_linea ( id_usr_carga,  fecha_carga,  actualizado,  no_,  no_comp,  uuid,  version,  fecha_emision,  fecha_certificacion,  rfc_emisor,  razon_receptor,  rfc_receptor,  razon_emisor,  pac_certificador,  estatus,  tipo,  tipo_comprobante,  serie,  folio,  moneda,  forma_pago,  metodo_pago,  clave_producto,  clave_unidad,  unidad,  cantidad,  num_identificacion,  descripcion,  valor_unitario,  importe,  descuento,  tot_tras_iva,  tot_tras_ieps,  tot_ret_iva,  tot_ret_ieps,  tot_ret_isr,  base_tras_1,  impuesto_tras_1,  tipo_tras_1,  tasa_tras_1,  importe_tras_1,  base_ret_1,  impuesto_ret_1,  tipo_ret_1,  tasa_ret_1,  importe_ret_1,  base_tras_2,  impuesto_tras_2,  tipo_tras_2,  tasa_tras_2,  importe_tras_2,  base_ret_2,  impuesto_ret_2,  tipo_ret_2,  tasa_ret_2,  importe_ret_2,  base_tras_3,  impuesto_tras_3,  tipo_tras_3,  tasa_tras_3,  importe_tras_3,  base_ret_3,  impuesto_ret_3,  tipo_ret_3,  tasa_ret_3,  importe_ret_3) " + _
                                                    '                                 "                      values (@id_usr_carga, @fecha_carga, @actualizado, @no_, @no_comp, @uuid, @version, @fecha_emision, @fecha_certificacion, @rfc_emisor, @razon_receptor, @rfc_receptor, @razon_emisor, @pac_certificador, @estatus, @tipo, @tipo_comprobante, @serie, @folio, @moneda, @forma_pago, @metodo_pago, @clave_producto, @clave_unidad, @unidad, @cantidad, @num_identificacion, @descripcion, @valor_unitario, @importe, @descuento, @tot_tras_iva, @tot_tras_ieps, @tot_ret_iva, @tot_ret_ieps, @tot_ret_isr, @base_tras_1, @impuesto_tras_1, @tipo_tras_1, @tasa_tras_1, @importe_tras_1, @base_ret_1, @impuesto_ret_1, @tipo_ret_1, @tasa_ret_1, @importe_ret_1, @base_tras_2, @impuesto_tras_2, @tipo_tras_2, @tasa_tras_2, @importe_tras_2, @base_ret_2, @impuesto_ret_2, @tipo_ret_2, @tasa_ret_2, @importe_ret_2, @base_tras_3, @impuesto_tras_3, @tipo_tras_3, @tasa_tras_3, @importe_tras_3, @base_ret_3, @impuesto_ret_3, @tipo_ret_3, @tasa_ret_3, @importe_ret_3) "
                                                    '    Case 78 '73
                                                    '        'Con 4 tipos de Impuestos y 4 Retenciones
                                                    '        SCMValores.CommandText = "insert into dt_factura_linea ( id_usr_carga,  fecha_carga,  actualizado,  no_,  no_comp,  uuid,  version,  fecha_emision,  fecha_certificacion,  rfc_emisor,  razon_receptor,  rfc_receptor,  razon_emisor,  pac_certificador,  estatus,  tipo,  tipo_comprobante,  serie,  folio,  moneda,  forma_pago,  metodo_pago,  clave_producto,  clave_unidad,  unidad,  cantidad,  num_identificacion,  descripcion,  valor_unitario,  importe,  descuento,  tot_tras_iva,  tot_tras_ieps,  tot_ret_iva,  tot_ret_ieps,  tot_ret_isr,  base_tras_1,  impuesto_tras_1,  tipo_tras_1,  tasa_tras_1,  importe_tras_1,  base_ret_1,  impuesto_ret_1,  tipo_ret_1,  tasa_ret_1,  importe_ret_1,  base_tras_2,  impuesto_tras_2,  tipo_tras_2,  tasa_tras_2,  importe_tras_2,  base_ret_2,  impuesto_ret_2,  tipo_ret_2,  tasa_ret_2,  importe_ret_2,  base_tras_3,  impuesto_tras_3,  tipo_tras_3,  tasa_tras_3,  importe_tras_3,  base_ret_3,  impuesto_ret_3,  tipo_ret_3,  tasa_ret_3,  importe_ret_3,  base_tras_4,  impuesto_tras_4,  tipo_tras_4,  tasa_tras_4,  importe_tras_4,  base_ret_4,  impuesto_ret_4,  tipo_ret_4,  tasa_ret_4,  importe_ret_4) " + _
                                                    '                                 "                      values (@id_usr_carga, @fecha_carga, @actualizado, @no_, @no_comp, @uuid, @version, @fecha_emision, @fecha_certificacion, @rfc_emisor, @razon_receptor, @rfc_receptor, @razon_emisor, @pac_certificador, @estatus, @tipo, @tipo_comprobante, @serie, @folio, @moneda, @forma_pago, @metodo_pago, @clave_producto, @clave_unidad, @unidad, @cantidad, @num_identificacion, @descripcion, @valor_unitario, @importe, @descuento, @tot_tras_iva, @tot_tras_ieps, @tot_ret_iva, @tot_ret_ieps, @tot_ret_isr, @base_tras_1, @impuesto_tras_1, @tipo_tras_1, @tasa_tras_1, @importe_tras_1, @base_ret_1, @impuesto_ret_1, @tipo_ret_1, @tasa_ret_1, @importe_ret_1, @base_tras_2, @impuesto_tras_2, @tipo_tras_2, @tasa_tras_2, @importe_tras_2, @base_ret_2, @impuesto_ret_2, @tipo_ret_2, @tasa_ret_2, @importe_ret_2, @base_tras_3, @impuesto_tras_3, @tipo_tras_3, @tasa_tras_3, @importe_tras_3, @base_ret_3, @impuesto_ret_3, @tipo_ret_3, @tasa_ret_3, @importe_ret_3, @base_tras_4, @impuesto_tras_4, @tipo_tras_4, @tasa_tras_4, @importe_tras_4, @base_ret_4, @impuesto_ret_4, @tipo_ret_4, @tasa_ret_4, @importe_ret_4) "
                                                    'End Select
                                                    SCMValores.Parameters.AddWithValue("@id_usr_carga", Val(._txtIdUsuario.Text))
                                                    If valReg > 0 Then
                                                        SCMValores.Parameters.AddWithValue("@actualizado", "S")
                                                    Else
                                                        SCMValores.Parameters.AddWithValue("@actualizado", "N")
                                                    End If
                                                    SCMValores.Parameters.AddWithValue("@no_", Val(dtRegistros.Rows(i).Item(0).ToString))
                                                    SCMValores.Parameters.AddWithValue("@no_comp", Val(dtRegistros.Rows(i).Item(1).ToString))
                                                    SCMValores.Parameters.AddWithValue("@version", Val(dtRegistros.Rows(i).Item(6).ToString))
                                                    SCMValores.Parameters.AddWithValue("@fecha_emision", CDate(dtRegistros.Rows(i).Item(3).ToString))
                                                    SCMValores.Parameters.AddWithValue("@fecha_certificacion", dtRegistros.Rows(i).Item(4).ToString)
                                                    SCMValores.Parameters.AddWithValue("@rfc_emisor", dtRegistros.Rows(i).Item(9).ToString)
                                                    SCMValores.Parameters.AddWithValue("@razon_receptor", dtRegistros.Rows(i).Item(8).ToString)
                                                    SCMValores.Parameters.AddWithValue("@rfc_receptor", dtRegistros.Rows(i).Item(7).ToString)
                                                    SCMValores.Parameters.AddWithValue("@razon_emisor", dtRegistros.Rows(i).Item(10).ToString)
                                                    SCMValores.Parameters.AddWithValue("@pac_certificador", dtRegistros.Rows(i).Item(12).ToString)
                                                    SCMValores.Parameters.AddWithValue("@estatus", dtRegistros.Rows(i).Item(13).ToString)
                                                    SCMValores.Parameters.AddWithValue("@tipo", dtRegistros.Rows(i).Item(14).ToString)
                                                    SCMValores.Parameters.AddWithValue("@tipo_comprobante", dtRegistros.Rows(i).Item(14).ToString)
                                                    SCMValores.Parameters.AddWithValue("@serie", dtRegistros.Rows(i).Item(15).ToString)
                                                    SCMValores.Parameters.AddWithValue("@folio", dtRegistros.Rows(i).Item(16).ToString)
                                                    SCMValores.Parameters.AddWithValue("@moneda", dtRegistros.Rows(i).Item(22).ToString)
                                                    SCMValores.Parameters.AddWithValue("@forma_pago", dtRegistros.Rows(i).Item(19).ToString)
                                                    SCMValores.Parameters.AddWithValue("@metodo_pago", dtRegistros.Rows(i).Item(18).ToString)
                                                    SCMValores.Parameters.AddWithValue("@clave_producto", dtRegistros.Rows(i).Item(24).ToString)
                                                    SCMValores.Parameters.AddWithValue("@clave_unidad", dtRegistros.Rows(i).Item(26).ToString)
                                                    SCMValores.Parameters.AddWithValue("@unidad", dtRegistros.Rows(i).Item(27).ToString)
                                                    SCMValores.Parameters.AddWithValue("@cantidad", Val(dtRegistros.Rows(i).Item(28).ToString))
                                                    SCMValores.Parameters.AddWithValue("@num_identificacion", dtRegistros.Rows(i).Item(29).ToString)
                                                    SCMValores.Parameters.AddWithValue("@descripcion", dtRegistros.Rows(i).Item(25).ToString)
                                                    SCMValores.Parameters.AddWithValue("@valor_unitario", Val(dtRegistros.Rows(i).Item(30).ToString))
                                                    SCMValores.Parameters.AddWithValue("@importe", Val(dtRegistros.Rows(i).Item(31).ToString))
                                                    SCMValores.Parameters.AddWithValue("@descuento", Val(dtRegistros.Rows(i).Item(32).ToString))

                                                    'Total Impuestos
                                                    If dtRegistros.Columns.Contains("Total Trasladado IVA") Then
                                                        SCMValores.Parameters.AddWithValue("@tot_tras_iva", Val(dtRegistros.Rows(i).Item("Total Trasladado IVA").ToString))
                                                        SCMValores.Parameters.AddWithValue("@tot_tras_ieps", Val(dtRegistros.Rows(i).Item("Total Trasladado IEPS").ToString))
                                                    End If
                                                    'Total Retenciones
                                                    If dtRegistros.Columns.Contains("Total Retenido IVA") Then
                                                        SCMValores.Parameters.AddWithValue("@tot_ret_iva", Val(dtRegistros.Rows(i).Item("Total Retenido IVA").ToString))
                                                        SCMValores.Parameters.AddWithValue("@tot_ret_ieps", Val(dtRegistros.Rows(i).Item("Total Retenido IEPS").ToString))
                                                        SCMValores.Parameters.AddWithValue("@tot_ret_isr", Val(dtRegistros.Rows(i).Item("Total Retenido ISR").ToString))
                                                    End If
                                                    'Impuestos 1
                                                    If dtRegistros.Columns.Contains("Trasladado - Base 1") Then
                                                        If dtRegistros.Rows(i).Item("Trasladado - Impuesto 1").ToString = "" Then
                                                            'Sin Impuesto Trasladado
                                                            SCMValores.Parameters.AddWithValue("@base_tras_1", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@impuesto_tras_1", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tipo_tras_1", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tasa_tras_1", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@importe_tras_1", DBNull.Value)
                                                        Else
                                                            'Con Impuesto Trasladado
                                                            SCMValores.Parameters.AddWithValue("@base_tras_1", Val(dtRegistros.Rows(i).Item("Trasladado - Base 1").ToString))
                                                            SCMValores.Parameters.AddWithValue("@impuesto_tras_1", dtRegistros.Rows(i).Item("Trasladado - Impuesto 1").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tipo_tras_1", dtRegistros.Rows(i).Item("Trasladado - Tipo o Factor 1").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tasa_tras_1", Val(dtRegistros.Rows(i).Item("Trasladado - Tasa o Cuota 1").ToString))
                                                            'Trasladado - Tasa o Cuota 1 Trasladado - Tasa o Couta 1
                                                            SCMValores.Parameters.AddWithValue("@importe_tras_1", Val(dtRegistros.Rows(i).Item("Trasladado - Importe 1").ToString))
                                                        End If
                                                    End If
                                                    'Impuestos 2
                                                    If dtRegistros.Columns.Contains("Trasladado - Base 2") Then
                                                        If dtRegistros.Rows(i).Item("Trasladado - Impuesto 2").ToString = "" Then
                                                            'Sin Impuesto Trasladado
                                                            SCMValores.Parameters.AddWithValue("@base_tras_2", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@impuesto_tras_2", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tipo_tras_2", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tasa_tras_2", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@importe_tras_2", DBNull.Value)
                                                        Else
                                                            'Con Impuesto Trasladado
                                                            SCMValores.Parameters.AddWithValue("@base_tras_2", Val(dtRegistros.Rows(i).Item("Trasladado - Base 2").ToString))
                                                            SCMValores.Parameters.AddWithValue("@impuesto_tras_2", dtRegistros.Rows(i).Item("Trasladado - Impuesto 2").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tipo_tras_2", dtRegistros.Rows(i).Item("Trasladado - Tipo o Factor 2").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tasa_tras_2", Val(dtRegistros.Rows(i).Item("Trasladado - Tasa o Cuota 2").ToString))
                                                            'Trasladado - Tasa o Cuota 2 Trasladado - Tasa o Couta 2
                                                            SCMValores.Parameters.AddWithValue("@importe_tras_2", Val(dtRegistros.Rows(i).Item("Trasladado - Importe 2").ToString))
                                                        End If
                                                    End If
                                                    'Impuestos 3
                                                    If dtRegistros.Columns.Contains("Trasladado - Base 3") Then
                                                        If dtRegistros.Rows(i).Item("Trasladado - Impuesto 3").ToString = "" Then
                                                            'Sin Impuesto Trasladado
                                                            SCMValores.Parameters.AddWithValue("@base_tras_3", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@impuesto_tras_3", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tipo_tras_3", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tasa_tras_3", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@importe_tras_3", DBNull.Value)
                                                        Else
                                                            'Con Impuesto Trasladado
                                                            SCMValores.Parameters.AddWithValue("@base_tras_3", Val(dtRegistros.Rows(i).Item("Trasladado - Base 3").ToString))
                                                            SCMValores.Parameters.AddWithValue("@impuesto_tras_3", dtRegistros.Rows(i).Item("Trasladado - Impuesto 3").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tipo_tras_3", dtRegistros.Rows(i).Item("Trasladado - Tipo o Factor 3").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tasa_tras_3", Val(dtRegistros.Rows(i).Item("Trasladado - Tasa o Cuota 3").ToString))
                                                            'Trasladado - Tasa o Cuota 3  Trasladado - Tasa o Couta 3
                                                            SCMValores.Parameters.AddWithValue("@importe_tras_3", Val(dtRegistros.Rows(i).Item("Trasladado - Importe 3").ToString))
                                                        End If
                                                    End If
                                                    'Impuestos 4
                                                    If dtRegistros.Columns.Contains("Trasladado - Base 4") Then
                                                        If dtRegistros.Rows(i).Item("Trasladado - Impuesto 4").ToString = "" Then
                                                            'Sin Impuesto Trasladado
                                                            SCMValores.Parameters.AddWithValue("@base_tras_4", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@impuesto_tras_4", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tipo_tras_4", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tasa_tras_4", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@importe_tras_4", DBNull.Value)
                                                        Else
                                                            'Con Impuesto Trasladado
                                                            SCMValores.Parameters.AddWithValue("@base_tras_4", Val(dtRegistros.Rows(i).Item("Trasladado - Base 4").ToString))
                                                            SCMValores.Parameters.AddWithValue("@impuesto_tras_4", dtRegistros.Rows(i).Item("Trasladado - Impuesto 4").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tipo_tras_4", dtRegistros.Rows(i).Item("Trasladado - Tipo o Factor 4").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tasa_tras_4", Val(dtRegistros.Rows(i).Item("Trasladado - Tasa o Couta 4").ToString))
                                                            SCMValores.Parameters.AddWithValue("@importe_tras_4", Val(dtRegistros.Rows(i).Item("Trasladado - Importe 4").ToString))
                                                        End If
                                                    End If
                                                    'Retención 1
                                                    If dtRegistros.Columns.Contains("Retenido - Base 1") Then
                                                        If dtRegistros.Rows(i).Item("Retenido - Impuesto 1").ToString = "" Then
                                                            'Sin Impuesto Retenido
                                                            SCMValores.Parameters.AddWithValue("@base_ret_1", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@impuesto_ret_1", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tipo_ret_1", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tasa_ret_1", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@importe_ret_1", DBNull.Value)
                                                        Else
                                                            'Con Impuesto Retenido
                                                            SCMValores.Parameters.AddWithValue("@base_ret_1", Val(dtRegistros.Rows(i).Item("Retenido - Base 1").ToString))
                                                            SCMValores.Parameters.AddWithValue("@impuesto_ret_1", dtRegistros.Rows(i).Item("Retenido - Impuesto 1").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tipo_ret_1", dtRegistros.Rows(i).Item("Retenido - Tipo o Factor 1").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tasa_ret_1", Val(dtRegistros.Rows(i).Item("Retenido - Tasa o Cuota 1").ToString))
                                                            'Retenido - Tasa o Cuota 1 Retenido - Tasa o Couta 1
                                                            SCMValores.Parameters.AddWithValue("@importe_ret_1", Val(dtRegistros.Rows(i).Item("Retenido - Importe 1").ToString))
                                                        End If
                                                    End If
                                                    'Retención 2
                                                    If dtRegistros.Columns.Contains("Retenido - Base 2") Then
                                                        If dtRegistros.Rows(i).Item("Retenido - Impuesto 2").ToString = "" Then
                                                            'Sin Impuesto Retenido
                                                            SCMValores.Parameters.AddWithValue("@base_ret_2", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@impuesto_ret_2", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tipo_ret_2", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tasa_ret_2", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@importe_ret_2", DBNull.Value)
                                                        Else
                                                            'Con Impuesto Retenido
                                                            SCMValores.Parameters.AddWithValue("@base_ret_2", Val(dtRegistros.Rows(i).Item("Retenido - Base 2").ToString))
                                                            SCMValores.Parameters.AddWithValue("@impuesto_ret_2", dtRegistros.Rows(i).Item("Retenido - Impuesto 2").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tipo_ret_2", dtRegistros.Rows(i).Item("Retenido - Tipo o Factor 2").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tasa_ret_2", Val(dtRegistros.Rows(i).Item("Retenido - Tasa o Cuota 2").ToString))
                                                            'Retenido - Tasa o Cuota 2 Retenido - Tasa o Couta 2
                                                            SCMValores.Parameters.AddWithValue("@importe_ret_2", Val(dtRegistros.Rows(i).Item("Retenido - Importe 2").ToString))
                                                        End If
                                                    End If
                                                    'Retención 3
                                                    If dtRegistros.Columns.Contains("Retenido - Base 3") Then
                                                        If dtRegistros.Rows(i).Item("Retenido - Impuesto 3").ToString = "" Then
                                                            'Sin Impuesto Retenido
                                                            SCMValores.Parameters.AddWithValue("@base_ret_3", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@impuesto_ret_3", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tipo_ret_3", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tasa_ret_3", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@importe_ret_3", DBNull.Value)
                                                        Else
                                                            'Con Impuesto Retenido
                                                            SCMValores.Parameters.AddWithValue("@base_ret_3", Val(dtRegistros.Rows(i).Item("Retenido - Base 3").ToString))
                                                            SCMValores.Parameters.AddWithValue("@impuesto_ret_3", dtRegistros.Rows(i).Item("Retenido - Impuesto 3").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tipo_ret_3", dtRegistros.Rows(i).Item("Retenido - Tipo o Factor 3").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tasa_ret_3", Val(dtRegistros.Rows(i).Item("Retenido - Tasa o Couta 3").ToString))
                                                            SCMValores.Parameters.AddWithValue("@importe_ret_3", Val(dtRegistros.Rows(i).Item("Retenido - Importe 3").ToString))
                                                        End If
                                                    End If
                                                    'Retención 4
                                                    If dtRegistros.Columns.Contains("Retenido - Base 4") Then
                                                        If dtRegistros.Rows(i).Item("Retenido - Impuesto 4").ToString = "" Then
                                                            'Sin Impuesto Retenido
                                                            SCMValores.Parameters.AddWithValue("@base_ret_4", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@impuesto_ret_4", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tipo_ret_4", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tasa_ret_4", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@importe_ret_4", DBNull.Value)
                                                        Else
                                                            'Con Impuesto Retenido
                                                            SCMValores.Parameters.AddWithValue("@base_ret_4", Val(dtRegistros.Rows(i).Item("Retenido - Base 4").ToString))
                                                            SCMValores.Parameters.AddWithValue("@impuesto_ret_4", dtRegistros.Rows(i).Item("Retenido - Impuesto 4").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tipo_ret_4", dtRegistros.Rows(i).Item("Retenido - Tipo o Factor 4").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tasa_ret_4", Val(dtRegistros.Rows(i).Item("Retenido - Tasa o Couta 4").ToString))
                                                            SCMValores.Parameters.AddWithValue("@importe_ret_4", Val(dtRegistros.Rows(i).Item("Retenido - Importe 4").ToString))
                                                        End If
                                                    End If

                                                    ''Versión Anterior
                                                    ''Asignación por Nombre de Columna
                                                    'If dtRegistros.Columns().Count > 33 Then '28
                                                    '    SCMValores.Parameters.AddWithValue("@tot_tras_iva", Val(dtRegistros.Rows(i).Item("Total Trasladado IVA").ToString))
                                                    '    SCMValores.Parameters.AddWithValue("@tot_tras_ieps", Val(dtRegistros.Rows(i).Item("Total Trasladado IEPS").ToString))
                                                    '    'Con 1 tipo de Impuesto
                                                    '    If dtRegistros.Rows(i).Item("Trasladado - Impuesto 1").ToString = "" Then
                                                    '        'Sin Impuesto Trasladado
                                                    '        SCMValores.Parameters.AddWithValue("@base_tras_1", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_tras_1", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_tras_1", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_tras_1", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@importe_tras_1", DBNull.Value)
                                                    '    Else
                                                    '        'Con Impuesto Trasladado
                                                    '        SCMValores.Parameters.AddWithValue("@base_tras_1", Val(dtRegistros.Rows(i).Item("Trasladado - Base 1").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_tras_1", dtRegistros.Rows(i).Item("Trasladado - Impuesto 1").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_tras_1", dtRegistros.Rows(i).Item("Trasladado - Tipo o Factor 1").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_tras_1", Val(dtRegistros.Rows(i).Item("Trasladado - Tasa o Couta 1").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@importe_tras_1", Val(dtRegistros.Rows(i).Item("Trasladado - Importe 1").ToString))
                                                    '    End If
                                                    'End If
                                                    ''Asignación de Impuestos
                                                    'If dtRegistros.Columns().Count >= 48 Then '43
                                                    '    SCMValores.Parameters.AddWithValue("@tot_ret_iva", Val(dtRegistros.Rows(i).Item("Total Retenido IVA").ToString))
                                                    '    SCMValores.Parameters.AddWithValue("@tot_ret_ieps", Val(dtRegistros.Rows(i).Item("Total Retenido IEPS").ToString))
                                                    '    SCMValores.Parameters.AddWithValue("@tot_ret_isr", Val(dtRegistros.Rows(i).Item("Total Retenido ISR").ToString))
                                                    '    'Con 1 tipo de Retención
                                                    '    If dtRegistros.Rows(i).Item("Retenido - Impuesto 1").ToString = "" Then
                                                    '        'Sin Impuesto Retenido
                                                    '        SCMValores.Parameters.AddWithValue("@base_ret_1", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_ret_1", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_ret_1", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_ret_1", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@importe_ret_1", DBNull.Value)
                                                    '    Else
                                                    '        'Con Impuesto Retenido
                                                    '        SCMValores.Parameters.AddWithValue("@base_ret_1", Val(dtRegistros.Rows(i).Item("Retenido - Base 1").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_ret_1", dtRegistros.Rows(i).Item("Retenido - Impuesto 1").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_ret_1", dtRegistros.Rows(i).Item("Retenido - Tipo o Factor 1").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_ret_1", Val(dtRegistros.Rows(i).Item("Retenido - Tasa o Couta 1").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@importe_ret_1", Val(dtRegistros.Rows(i).Item("Retenido - Importe 1").ToString))
                                                    '    End If
                                                    'End If
                                                    'If dtRegistros.Columns().Count = 45 Then '40
                                                    '    'Con 2 tipo de Impuesto
                                                    '    If dtRegistros.Rows(i).Item("Trasladado - Impuesto 2").ToString = "" Then
                                                    '        'Sin Impuesto Trasladado
                                                    '        SCMValores.Parameters.AddWithValue("@base_tras_2", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_tras_2", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_tras_2", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_tras_2", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@importe_tras_2", DBNull.Value)
                                                    '    Else
                                                    '        'Con Impuesto Trasladado
                                                    '        SCMValores.Parameters.AddWithValue("@base_tras_2", Val(dtRegistros.Rows(i).Item("Trasladado - Base 2").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_tras_2", dtRegistros.Rows(i).Item("Trasladado - Impuesto 2").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_tras_2", dtRegistros.Rows(i).Item("Trasladado - Tipo o Factor 2").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_tras_2", Val(dtRegistros.Rows(i).Item("Trasladado - Tasa o Couta 2").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@importe_tras_2", Val(dtRegistros.Rows(i).Item("Trasladado - Importe 2").ToString))
                                                    '    End If
                                                    'End If
                                                    'If dtRegistros.Columns().Count >= 53 Then '48
                                                    '    'Con 2 tipo de Impuesto
                                                    '    If dtRegistros.Rows(i).Item("Trasladado - Impuesto 2").ToString = "" Then
                                                    '        'Sin Impuesto Trasladado
                                                    '        SCMValores.Parameters.AddWithValue("@base_tras_2", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_tras_2", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_tras_2", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_tras_2", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@importe_tras_2", DBNull.Value)
                                                    '    Else
                                                    '        'Con Impuesto Trasladado
                                                    '        SCMValores.Parameters.AddWithValue("@base_tras_2", Val(dtRegistros.Rows(i).Item("Trasladado - Base 2").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_tras_2", dtRegistros.Rows(i).Item("Trasladado - Impuesto 2").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_tras_2", dtRegistros.Rows(i).Item("Trasladado - Tipo o Factor 2").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_tras_2", Val(dtRegistros.Rows(i).Item("Trasladado - Tasa o Couta 2").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@importe_tras_2", Val(dtRegistros.Rows(i).Item("Trasladado - Importe 2").ToString))
                                                    '    End If
                                                    '    If dtRegistros.Columns().Count >= 58 Then '53
                                                    '        If dtRegistros.Rows(i).Item("Retenido - Impuesto 2").ToString = "" Then
                                                    '            'Sin Impuesto Retenido
                                                    '            SCMValores.Parameters.AddWithValue("@base_ret_2", DBNull.Value)
                                                    '            SCMValores.Parameters.AddWithValue("@impuesto_ret_2", DBNull.Value)
                                                    '            SCMValores.Parameters.AddWithValue("@tipo_ret_2", DBNull.Value)
                                                    '            SCMValores.Parameters.AddWithValue("@tasa_ret_2", DBNull.Value)
                                                    '            SCMValores.Parameters.AddWithValue("@importe_ret_2", DBNull.Value)
                                                    '        Else
                                                    '            'Con Impuesto Retenido
                                                    '            SCMValores.Parameters.AddWithValue("@base_ret_2", Val(dtRegistros.Rows(i).Item("Retenido - Base 2").ToString))
                                                    '            SCMValores.Parameters.AddWithValue("@impuesto_ret_2", dtRegistros.Rows(i).Item("Retenido - Impuesto 2").ToString)
                                                    '            SCMValores.Parameters.AddWithValue("@tipo_ret_2", dtRegistros.Rows(i).Item("Retenido - Tipo o Factor 2").ToString)
                                                    '            SCMValores.Parameters.AddWithValue("@tasa_ret_2", Val(dtRegistros.Rows(i).Item("Retenido - Tasa o Couta 2").ToString))
                                                    '            SCMValores.Parameters.AddWithValue("@importe_ret_2", Val(dtRegistros.Rows(i).Item("Retenido - Importe 2").ToString))
                                                    '        End If
                                                    '    End If
                                                    'End If
                                                    'If dtRegistros.Columns().Count >= 63 Then 'XX
                                                    '    'Con 3 tipo de Impuesto
                                                    '    If dtRegistros.Rows(i).Item("Trasladado - Impuesto 3").ToString = "" Then
                                                    '        'Sin Impuesto Trasladado
                                                    '        SCMValores.Parameters.AddWithValue("@base_tras_3", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_tras_3", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_tras_3", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_tras_3", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@importe_tras_3", DBNull.Value)
                                                    '    Else
                                                    '        'Con Impuesto Trasladado
                                                    '        SCMValores.Parameters.AddWithValue("@base_tras_3", Val(dtRegistros.Rows(i).Item("Trasladado - Base 3").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_tras_3", dtRegistros.Rows(i).Item("Trasladado - Impuesto 3").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_tras_3", dtRegistros.Rows(i).Item("Trasladado - Tipo o Factor 3").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_tras_3", Val(dtRegistros.Rows(i).Item("Trasladado - Tasa o Couta 3").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@importe_tras_3", Val(dtRegistros.Rows(i).Item("Trasladado - Importe 3").ToString))
                                                    '    End If
                                                    '    If dtRegistros.Columns().Count >= 68 Then '63
                                                    '        If dtRegistros.Rows(i).Item("Retenido - Impuesto 3").ToString = "" Then
                                                    '            'Sin Impuesto Retenido
                                                    '            SCMValores.Parameters.AddWithValue("@base_ret_3", DBNull.Value)
                                                    '            SCMValores.Parameters.AddWithValue("@impuesto_ret_3", DBNull.Value)
                                                    '            SCMValores.Parameters.AddWithValue("@tipo_ret_3", DBNull.Value)
                                                    '            SCMValores.Parameters.AddWithValue("@tasa_ret_3", DBNull.Value)
                                                    '            SCMValores.Parameters.AddWithValue("@importe_ret_3", DBNull.Value)
                                                    '        Else
                                                    '            'Con Impuesto Retenido
                                                    '            SCMValores.Parameters.AddWithValue("@base_ret_3", Val(dtRegistros.Rows(i).Item("Retenido - Base 3").ToString))
                                                    '            SCMValores.Parameters.AddWithValue("@impuesto_ret_3", dtRegistros.Rows(i).Item("Retenido - Impuesto 3").ToString)
                                                    '            SCMValores.Parameters.AddWithValue("@tipo_ret_3", dtRegistros.Rows(i).Item("Retenido - Tipo o Factor 3").ToString)
                                                    '            SCMValores.Parameters.AddWithValue("@tasa_ret_3", Val(dtRegistros.Rows(i).Item("Retenido - Tasa o Couta 3").ToString))
                                                    '            SCMValores.Parameters.AddWithValue("@importe_ret_3", Val(dtRegistros.Rows(i).Item("Retenido - Importe 3").ToString))
                                                    '        End If
                                                    '    End If
                                                    'End If
                                                    'If dtRegistros.Columns().Count >= 78 Then '73
                                                    '    'Con 4 tipo de Impuesto
                                                    '    If dtRegistros.Rows(i).Item("Trasladado - Impuesto 4").ToString = "" Then
                                                    '        'Sin Impuesto Trasladado
                                                    '        SCMValores.Parameters.AddWithValue("@base_tras_4", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_tras_4", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_tras_4", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_tras_4", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@importe_tras_4", DBNull.Value)
                                                    '    Else
                                                    '        'Con Impuesto Trasladado
                                                    '        SCMValores.Parameters.AddWithValue("@base_tras_4", Val(dtRegistros.Rows(i).Item("Trasladado - Base 4").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_tras_4", dtRegistros.Rows(i).Item("Trasladado - Impuesto 4").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_tras_4", dtRegistros.Rows(i).Item("Trasladado - Tipo o Factor 4").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_tras_4", Val(dtRegistros.Rows(i).Item("Trasladado - Tasa o Couta 4").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@importe_tras_4", Val(dtRegistros.Rows(i).Item("Trasladado - Importe 4").ToString))
                                                    '    End If
                                                    '    If dtRegistros.Rows(i).Item("Retenido - Impuesto 4").ToString = "" Then
                                                    '        'Sin Impuesto Retenido
                                                    '        SCMValores.Parameters.AddWithValue("@base_ret_4", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_ret_4", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_ret_4", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_ret_4", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@importe_ret_4", DBNull.Value)
                                                    '    Else
                                                    '        'Con Impuesto Retenido
                                                    '        SCMValores.Parameters.AddWithValue("@base_ret_4", Val(dtRegistros.Rows(i).Item("Retenido - Base 4").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_ret_4", dtRegistros.Rows(i).Item("Retenido - Impuesto 4").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_ret_4", dtRegistros.Rows(i).Item("Retenido - Tipo o Factor 4").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_ret_4", Val(dtRegistros.Rows(i).Item("Retenido - Tasa o Couta 4").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@importe_ret_4", Val(dtRegistros.Rows(i).Item("Retenido - Importe 4").ToString))
                                                    '    End If
                                                    'End If

                                                    ConexionBD.Open()
                                                    SCMValores.ExecuteNonQuery()
                                                    ConexionBD.Close()
                                                Next
                                            Case "4.0"
                                                ' * * *  Versión 4.0  * * *
                                                For i = 0 To dtRegistros.Rows.Count - 1
                                                    SCMValores.Parameters.Clear()
                                                    SCMValores.CommandText = "select count(*) as valor from dt_factura_linea where uuid = @uuid and fecha_carga <> @fecha_carga"
                                                    SCMValores.Parameters.AddWithValue("@fecha_carga", fecha)
                                                    SCMValores.Parameters.AddWithValue("@uuid", dtRegistros.Rows(i).Item(2).ToString)
                                                    ConexionBD.Open()
                                                    valReg = SCMValores.ExecuteScalar
                                                    ConexionBD.Close()

                                                    If valReg > 0 Then
                                                        'Ya existe el registro, por lo que solo se elimina
                                                        SCMValores.CommandText = "delete from dt_factura_linea where uuid = @uuid "
                                                        ConexionBD.Open()
                                                        SCMValores.ExecuteNonQuery()
                                                        ConexionBD.Close()
                                                        'Incrementar contador
                                                        cAct = cAct + 1
                                                    Else
                                                        'No existe el registro, por lo que solo se inserta
                                                        cIns = cIns + 1
                                                    End If

                                                    Dim queryInto As String
                                                    Dim queryVal As String

                                                    queryInto = "insert into dt_factura_linea ( id_usr_carga,  fecha_carga,  actualizado,  no_,  no_comp,  uuid,  version,  fecha_emision,  fecha_certificacion,  rfc_emisor,  razon_receptor,  rfc_receptor,  razon_emisor,  pac_certificador,  estatus,  tipo,  tipo_comprobante,  serie,  folio,  moneda,  forma_pago,  metodo_pago,  clave_producto,  clave_unidad,  unidad,  cantidad,  num_identificacion,  descripcion,  valor_unitario, objeto_impuesto,  importe,  descuento"
                                                    queryVal = "                       values (@id_usr_carga, @fecha_carga, @actualizado, @no_, @no_comp, @uuid, @version, @fecha_emision, @fecha_certificacion, @rfc_emisor, @razon_receptor, @rfc_receptor, @razon_emisor, @pac_certificador, @estatus, @tipo, @tipo_comprobante, @serie, @folio, @moneda, @forma_pago, @metodo_pago, @clave_producto, @clave_unidad, @unidad, @cantidad, @num_identificacion, @descripcion, @valor_unitario, @objeto_impuesto ,@importe, @descuento"

                                                    'Total Impuestos
                                                    If dtRegistros.Columns.Contains("Total Trasladado IVA") Then
                                                        queryInto = queryInto + ",  tot_tras_iva,  tot_tras_ieps"
                                                        queryVal = queryVal + ", @tot_tras_iva, @tot_tras_ieps"
                                                    End If
                                                    'Total Retenciones
                                                    If dtRegistros.Columns.Contains("Total Retenido IVA") Then
                                                        queryInto = queryInto + ",  tot_ret_iva,  tot_ret_ieps,  tot_ret_isr"
                                                        queryVal = queryVal + ", @tot_ret_iva, @tot_ret_ieps, @tot_ret_isr"
                                                    End If
                                                    'Impuestos 1
                                                    If dtRegistros.Columns.Contains("Trasladado - Base 1") Then
                                                        queryInto = queryInto + ",  base_tras_1,  impuesto_tras_1,  tipo_tras_1,  tasa_tras_1,  importe_tras_1"
                                                        queryVal = queryVal + ", @base_tras_1, @impuesto_tras_1, @tipo_tras_1, @tasa_tras_1, @importe_tras_1"
                                                    End If
                                                    'Impuestos 2
                                                    If dtRegistros.Columns.Contains("Trasladado - Base 2") Then
                                                        queryInto = queryInto + ",  base_tras_2,  impuesto_tras_2,  tipo_tras_2,  tasa_tras_2,  importe_tras_2"
                                                        queryVal = queryVal + ", @base_tras_2, @impuesto_tras_2, @tipo_tras_2, @tasa_tras_2, @importe_tras_2"
                                                    End If
                                                    'Impuestos 3
                                                    If dtRegistros.Columns.Contains("Trasladado - Base 3") Then
                                                        queryInto = queryInto + ",  base_tras_3,  impuesto_tras_3,  tipo_tras_3,  tasa_tras_3,  importe_tras_3"
                                                        queryVal = queryVal + ", @base_tras_3, @impuesto_tras_3, @tipo_tras_3, @tasa_tras_3, @importe_tras_3"
                                                    End If
                                                    'Impuestos 4
                                                    If dtRegistros.Columns.Contains("Trasladado - Base 4") Then
                                                        queryInto = queryInto + ",  base_tras_4,  impuesto_tras_4,  tipo_tras_4,  tasa_tras_4,  importe_tras_4"
                                                        queryVal = queryVal + ", @base_tras_4, @impuesto_tras_4, @tipo_tras_4, @tasa_tras_4, @importe_tras_4"
                                                    End If
                                                    'Retención 1
                                                    If dtRegistros.Columns.Contains("Retenido - Base 1") Then
                                                        queryInto = queryInto + ",  base_ret_1,  impuesto_ret_1,  tipo_ret_1,  tasa_ret_1,  importe_ret_1"
                                                        queryVal = queryVal + ", @base_ret_1, @impuesto_ret_1, @tipo_ret_1, @tasa_ret_1, @importe_ret_1"
                                                    End If
                                                    'Retención 2
                                                    If dtRegistros.Columns.Contains("Retenido - Base 2") Then
                                                        queryInto = queryInto + ",  base_ret_2,  impuesto_ret_2,  tipo_ret_2,  tasa_ret_2,  importe_ret_2"
                                                        queryVal = queryVal + ", @base_ret_2, @impuesto_ret_2, @tipo_ret_2, @tasa_ret_2, @importe_ret_2"
                                                    End If
                                                    'Retención 3
                                                    If dtRegistros.Columns.Contains("Retenido - Base 3") Then
                                                        queryInto = queryInto + ",  base_ret_3,  impuesto_ret_3,  tipo_ret_3,  tasa_ret_3,  importe_ret_3"
                                                        queryVal = queryVal + ", @base_ret_3, @impuesto_ret_3, @tipo_ret_3, @tasa_ret_3, @importe_ret_3"
                                                    End If
                                                    'Retención 4
                                                    If dtRegistros.Columns.Contains("Retenido - Base 4") Then
                                                        queryInto = queryInto + ",  base_ret_4,  impuesto_ret_4,  tipo_ret_4,  tasa_ret_4,  importe_ret_4"
                                                        queryVal = queryVal + ", @base_ret_4, @impuesto_ret_4, @tipo_ret_4, @tasa_ret_4, @importe_ret_4"
                                                    End If
                                                    queryInto = queryInto + ") "
                                                    queryVal = queryVal + ") "

                                                    SCMValores.CommandText = queryInto + queryVal

                                                    ''Versión Anterior
                                                    'Select Case dtRegistros.Columns().Count
                                                    '    Case 33 '28
                                                    '        'Sin Impuestos
                                                    '        SCMValores.CommandText = "insert into dt_factura_linea ( id_usr_carga,  fecha_carga,  actualizado,  no_,  no_comp,  uuid,  version,  fecha_emision,  fecha_certificacion,  rfc_emisor,  razon_receptor,  rfc_receptor,  razon_emisor,  pac_certificador,  estatus,  tipo,  tipo_comprobante,  serie,  folio,  moneda,  forma_pago,  metodo_pago,  clave_producto,  clave_unidad,  unidad,  cantidad,  num_identificacion,  descripcion,  valor_unitario,  importe,  descuento) " + _
                                                    '                                 "                      values (@id_usr_carga, @fecha_carga, @actualizado, @no_, @no_comp, @uuid, @version, @fecha_emision, @fecha_certificacion, @rfc_emisor, @razon_receptor, @rfc_receptor, @razon_emisor, @pac_certificador, @estatus, @tipo, @tipo_comprobante, @serie, @folio, @moneda, @forma_pago, @metodo_pago, @clave_producto, @clave_unidad, @unidad, @cantidad, @num_identificacion, @descripcion, @valor_unitario, @importe, @descuento) "
                                                    '    Case 43 '40 '35
                                                    '        'Con 1 tipo de Impuesto sin Retención
                                                    '        SCMValores.CommandText = "insert into dt_factura_linea ( id_usr_carga,  fecha_carga,  actualizado,  no_,  no_comp,  uuid,  version,  fecha_emision,  fecha_certificacion,  rfc_emisor,  razon_receptor,  rfc_receptor,  razon_emisor,  pac_certificador,  estatus,  tipo,  tipo_comprobante,  serie,  folio,  moneda,  forma_pago,  metodo_pago,  clave_producto,  clave_unidad,  unidad,  cantidad,  num_identificacion,  descripcion,  valor_unitario,  importe,  descuento,  tot_tras_iva,  tot_tras_ieps,  base_tras_1,  impuesto_tras_1,  tipo_tras_1,  tasa_tras_1,  importe_tras_1) " + _
                                                    '                                 "                      values (@id_usr_carga, @fecha_carga, @actualizado, @no_, @no_comp, @uuid, @version, @fecha_emision, @fecha_certificacion, @rfc_emisor, @razon_receptor, @rfc_receptor, @razon_emisor, @pac_certificador, @estatus, @tipo, @tipo_comprobante, @serie, @folio, @moneda, @forma_pago, @metodo_pago, @clave_producto, @clave_unidad, @unidad, @cantidad, @num_identificacion, @descripcion, @valor_unitario, @importe, @descuento, @tot_tras_iva, @tot_tras_ieps, @base_tras_1, @impuesto_tras_1, @tipo_tras_1, @tasa_tras_1, @importe_tras_1) "
                                                    '    Case 45 '40
                                                    '        'Con 2 tipos de Impuestos sin Retención
                                                    '        SCMValores.CommandText = "insert into dt_factura_linea ( id_usr_carga,  fecha_carga,  actualizado,  no_,  no_comp,  uuid,  version,  fecha_emision,  fecha_certificacion,  rfc_emisor,  razon_receptor,  rfc_receptor,  razon_emisor,  pac_certificador,  estatus,  tipo,  tipo_comprobante,  serie,  folio,  moneda,  forma_pago,  metodo_pago,  clave_producto,  clave_unidad,  unidad,  cantidad,  num_identificacion,  descripcion,  valor_unitario,  importe,  descuento,  tot_tras_iva,  tot_tras_ieps,  base_tras_1,  impuesto_tras_1,  tipo_tras_1,  tasa_tras_1,  importe_tras_1,  base_tras_2,  impuesto_tras_2,  tipo_tras_2,  tasa_tras_2,  importe_tras_2) " + _
                                                    '                                 "                      values (@id_usr_carga, @fecha_carga, @actualizado, @no_, @no_comp, @uuid, @version, @fecha_emision, @fecha_certificacion, @rfc_emisor, @razon_receptor, @rfc_receptor, @razon_emisor, @pac_certificador, @estatus, @tipo, @tipo_comprobante, @serie, @folio, @moneda, @forma_pago, @metodo_pago, @clave_producto, @clave_unidad, @unidad, @cantidad, @num_identificacion, @descripcion, @valor_unitario, @importe, @descuento, @tot_tras_iva, @tot_tras_ieps, @base_tras_1, @impuesto_tras_1, @tipo_tras_1, @tasa_tras_1, @importe_tras_1, @base_tras_2, @impuesto_tras_2, @tipo_tras_2, @tasa_tras_2, @importe_tras_2) "
                                                    '    Case 48 '43
                                                    '        'Con 1 tipo de Impuesto y 1 Retención
                                                    '        SCMValores.CommandText = "insert into dt_factura_linea ( id_usr_carga,  fecha_carga,  actualizado,  no_,  no_comp,  uuid,  version,  fecha_emision,  fecha_certificacion,  rfc_emisor,  razon_receptor,  rfc_receptor,  razon_emisor,  pac_certificador,  estatus,  tipo,  tipo_comprobante,  serie,  folio,  moneda,  forma_pago,  metodo_pago,  clave_producto,  clave_unidad,  unidad,  cantidad,  num_identificacion,  descripcion,  valor_unitario,  importe,  descuento,  tot_tras_iva,  tot_tras_ieps,  tot_ret_iva,  tot_ret_ieps,  tot_ret_isr,  base_tras_1,  impuesto_tras_1,  tipo_tras_1,  tasa_tras_1,  importe_tras_1,  base_ret_1,  impuesto_ret_1,  tipo_ret_1,  tasa_ret_1,  importe_ret_1) " + _
                                                    '                                 "                      values (@id_usr_carga, @fecha_carga, @actualizado, @no_, @no_comp, @uuid, @version, @fecha_emision, @fecha_certificacion, @rfc_emisor, @razon_receptor, @rfc_receptor, @razon_emisor, @pac_certificador, @estatus, @tipo, @tipo_comprobante, @serie, @folio, @moneda, @forma_pago, @metodo_pago, @clave_producto, @clave_unidad, @unidad, @cantidad, @num_identificacion, @descripcion, @valor_unitario, @importe, @descuento, @tot_tras_iva, @tot_tras_ieps, @tot_ret_iva, @tot_ret_ieps, @tot_ret_isr, @base_tras_1, @impuesto_tras_1, @tipo_tras_1, @tasa_tras_1, @importe_tras_1, @base_ret_1, @impuesto_ret_1, @tipo_ret_1, @tasa_ret_1, @importe_ret_1) "
                                                    '    Case 53 '48
                                                    '        'Con 2 tipos de Impuestos y 1 Retención
                                                    '        SCMValores.CommandText = "insert into dt_factura_linea ( id_usr_carga,  fecha_carga,  actualizado,  no_,  no_comp,  uuid,  version,  fecha_emision,  fecha_certificacion,  rfc_emisor,  razon_receptor,  rfc_receptor,  razon_emisor,  pac_certificador,  estatus,  tipo,  tipo_comprobante,  serie,  folio,  moneda,  forma_pago,  metodo_pago,  clave_producto,  clave_unidad,  unidad,  cantidad,  num_identificacion,  descripcion,  valor_unitario,  importe,  descuento,  tot_tras_iva,  tot_tras_ieps,  tot_ret_iva,  tot_ret_ieps,  tot_ret_isr,  base_tras_1,  impuesto_tras_1,  tipo_tras_1,  tasa_tras_1,  importe_tras_1,  base_ret_1,  impuesto_ret_1,  tipo_ret_1,  tasa_ret_1,  importe_ret_1,  base_tras_2,  impuesto_tras_2,  tipo_tras_2,  tasa_tras_2,  importe_tras_2) " + _
                                                    '                                 "                      values (@id_usr_carga, @fecha_carga, @actualizado, @no_, @no_comp, @uuid, @version, @fecha_emision, @fecha_certificacion, @rfc_emisor, @razon_receptor, @rfc_receptor, @razon_emisor, @pac_certificador, @estatus, @tipo, @tipo_comprobante, @serie, @folio, @moneda, @forma_pago, @metodo_pago, @clave_producto, @clave_unidad, @unidad, @cantidad, @num_identificacion, @descripcion, @valor_unitario, @importe, @descuento, @tot_tras_iva, @tot_tras_ieps, @tot_ret_iva, @tot_ret_ieps, @tot_ret_isr, @base_tras_1, @impuesto_tras_1, @tipo_tras_1, @tasa_tras_1, @importe_tras_1, @base_ret_1, @impuesto_ret_1, @tipo_ret_1, @tasa_ret_1, @importe_ret_1, @base_tras_2, @impuesto_tras_2, @tipo_tras_2, @tasa_tras_2, @importe_tras_2) "
                                                    '    Case 58 '53
                                                    '        'Con 2 tipos de Impuestos
                                                    '        SCMValores.CommandText = "insert into dt_factura_linea ( id_usr_carga,  fecha_carga,  actualizado,  no_,  no_comp,  uuid,  version,  fecha_emision,  fecha_certificacion,  rfc_emisor,  razon_receptor,  rfc_receptor,  razon_emisor,  pac_certificador,  estatus,  tipo,  tipo_comprobante,  serie,  folio,  moneda,  forma_pago,  metodo_pago,  clave_producto,  clave_unidad,  unidad,  cantidad,  num_identificacion,  descripcion,  valor_unitario,  importe,  descuento,  tot_tras_iva,  tot_tras_ieps,  tot_ret_iva,  tot_ret_ieps,  tot_ret_isr,  base_tras_1,  impuesto_tras_1,  tipo_tras_1,  tasa_tras_1,  importe_tras_1,  base_ret_1,  impuesto_ret_1,  tipo_ret_1,  tasa_ret_1,  importe_ret_1,  base_tras_2,  impuesto_tras_2,  tipo_tras_2,  tasa_tras_2,  importe_tras_2,  base_ret_2,  impuesto_ret_2,  tipo_ret_2,  tasa_ret_2,  importe_ret_2) " + _
                                                    '                                 "                      values (@id_usr_carga, @fecha_carga, @actualizado, @no_, @no_comp, @uuid, @version, @fecha_emision, @fecha_certificacion, @rfc_emisor, @razon_receptor, @rfc_receptor, @razon_emisor, @pac_certificador, @estatus, @tipo, @tipo_comprobante, @serie, @folio, @moneda, @forma_pago, @metodo_pago, @clave_producto, @clave_unidad, @unidad, @cantidad, @num_identificacion, @descripcion, @valor_unitario, @importe, @descuento, @tot_tras_iva, @tot_tras_ieps, @tot_ret_iva, @tot_ret_ieps, @tot_ret_isr, @base_tras_1, @impuesto_tras_1, @tipo_tras_1, @tasa_tras_1, @importe_tras_1, @base_ret_1, @impuesto_ret_1, @tipo_ret_1, @tasa_ret_1, @importe_ret_1, @base_tras_2, @impuesto_tras_2, @tipo_tras_2, @tasa_tras_2, @importe_tras_2, @base_ret_2, @impuesto_ret_2, @tipo_ret_2, @tasa_ret_2, @importe_ret_2) "
                                                    '    Case 68 '63
                                                    '        'Con 3 tipos de Impuestos
                                                    '        SCMValores.CommandText = "insert into dt_factura_linea ( id_usr_carga,  fecha_carga,  actualizado,  no_,  no_comp,  uuid,  version,  fecha_emision,  fecha_certificacion,  rfc_emisor,  razon_receptor,  rfc_receptor,  razon_emisor,  pac_certificador,  estatus,  tipo,  tipo_comprobante,  serie,  folio,  moneda,  forma_pago,  metodo_pago,  clave_producto,  clave_unidad,  unidad,  cantidad,  num_identificacion,  descripcion,  valor_unitario,  importe,  descuento,  tot_tras_iva,  tot_tras_ieps,  tot_ret_iva,  tot_ret_ieps,  tot_ret_isr,  base_tras_1,  impuesto_tras_1,  tipo_tras_1,  tasa_tras_1,  importe_tras_1,  base_ret_1,  impuesto_ret_1,  tipo_ret_1,  tasa_ret_1,  importe_ret_1,  base_tras_2,  impuesto_tras_2,  tipo_tras_2,  tasa_tras_2,  importe_tras_2,  base_ret_2,  impuesto_ret_2,  tipo_ret_2,  tasa_ret_2,  importe_ret_2,  base_tras_3,  impuesto_tras_3,  tipo_tras_3,  tasa_tras_3,  importe_tras_3,  base_ret_3,  impuesto_ret_3,  tipo_ret_3,  tasa_ret_3,  importe_ret_3) " + _
                                                    '                                 "                      values (@id_usr_carga, @fecha_carga, @actualizado, @no_, @no_comp, @uuid, @version, @fecha_emision, @fecha_certificacion, @rfc_emisor, @razon_receptor, @rfc_receptor, @razon_emisor, @pac_certificador, @estatus, @tipo, @tipo_comprobante, @serie, @folio, @moneda, @forma_pago, @metodo_pago, @clave_producto, @clave_unidad, @unidad, @cantidad, @num_identificacion, @descripcion, @valor_unitario, @importe, @descuento, @tot_tras_iva, @tot_tras_ieps, @tot_ret_iva, @tot_ret_ieps, @tot_ret_isr, @base_tras_1, @impuesto_tras_1, @tipo_tras_1, @tasa_tras_1, @importe_tras_1, @base_ret_1, @impuesto_ret_1, @tipo_ret_1, @tasa_ret_1, @importe_ret_1, @base_tras_2, @impuesto_tras_2, @tipo_tras_2, @tasa_tras_2, @importe_tras_2, @base_ret_2, @impuesto_ret_2, @tipo_ret_2, @tasa_ret_2, @importe_ret_2, @base_tras_3, @impuesto_tras_3, @tipo_tras_3, @tasa_tras_3, @importe_tras_3, @base_ret_3, @impuesto_ret_3, @tipo_ret_3, @tasa_ret_3, @importe_ret_3) "
                                                    '    Case 78 '73
                                                    '        'Con 4 tipos de Impuestos
                                                    '        SCMValores.CommandText = "insert into dt_factura_linea ( id_usr_carga,  fecha_carga,  actualizado,  no_,  no_comp,  uuid,  version,  fecha_emision,  fecha_certificacion,  rfc_emisor,  razon_receptor,  rfc_receptor,  razon_emisor,  pac_certificador,  estatus,  tipo,  tipo_comprobante,  serie,  folio,  moneda,  forma_pago,  metodo_pago,  clave_producto,  clave_unidad,  unidad,  cantidad,  num_identificacion,  descripcion,  valor_unitario,  importe,  descuento,  tot_tras_iva,  tot_tras_ieps,  tot_ret_iva,  tot_ret_ieps,  tot_ret_isr,  base_tras_1,  impuesto_tras_1,  tipo_tras_1,  tasa_tras_1,  importe_tras_1,  base_ret_1,  impuesto_ret_1,  tipo_ret_1,  tasa_ret_1,  importe_ret_1,  base_tras_2,  impuesto_tras_2,  tipo_tras_2,  tasa_tras_2,  importe_tras_2,  base_ret_2,  impuesto_ret_2,  tipo_ret_2,  tasa_ret_2,  importe_ret_2,  base_tras_3,  impuesto_tras_3,  tipo_tras_3,  tasa_tras_3,  importe_tras_3,  base_ret_3,  impuesto_ret_3,  tipo_ret_3,  tasa_ret_3,  importe_ret_3,  base_tras_4,  impuesto_tras_4,  tipo_tras_4,  tasa_tras_4,  importe_tras_4,  base_ret_4,  impuesto_ret_4,  tipo_ret_4,  tasa_ret_4,  importe_ret_4) " + _
                                                    '                                 "                      values (@id_usr_carga, @fecha_carga, @actualizado, @no_, @no_comp, @uuid, @version, @fecha_emision, @fecha_certificacion, @rfc_emisor, @razon_receptor, @rfc_receptor, @razon_emisor, @pac_certificador, @estatus, @tipo, @tipo_comprobante, @serie, @folio, @moneda, @forma_pago, @metodo_pago, @clave_producto, @clave_unidad, @unidad, @cantidad, @num_identificacion, @descripcion, @valor_unitario, @importe, @descuento, @tot_tras_iva, @tot_tras_ieps, @tot_ret_iva, @tot_ret_ieps, @tot_ret_isr, @base_tras_1, @impuesto_tras_1, @tipo_tras_1, @tasa_tras_1, @importe_tras_1, @base_ret_1, @impuesto_ret_1, @tipo_ret_1, @tasa_ret_1, @importe_ret_1, @base_tras_2, @impuesto_tras_2, @tipo_tras_2, @tasa_tras_2, @importe_tras_2, @base_ret_2, @impuesto_ret_2, @tipo_ret_2, @tasa_ret_2, @importe_ret_2, @base_tras_3, @impuesto_tras_3, @tipo_tras_3, @tasa_tras_3, @importe_tras_3, @base_ret_3, @impuesto_ret_3, @tipo_ret_3, @tasa_ret_3, @importe_ret_3, @base_tras_4, @impuesto_tras_4, @tipo_tras_4, @tasa_tras_4, @importe_tras_4, @base_ret_4, @impuesto_ret_4, @tipo_ret_4, @tasa_ret_4, @importe_ret_4) "
                                                    'End Select

                                                    SCMValores.Parameters.AddWithValue("@id_usr_carga", Val(._txtIdUsuario.Text))
                                                    If valReg > 0 Then
                                                        SCMValores.Parameters.AddWithValue("@actualizado", "S")
                                                    Else
                                                        SCMValores.Parameters.AddWithValue("@actualizado", "N")
                                                    End If
                                                    SCMValores.Parameters.AddWithValue("@no_", Val(dtRegistros.Rows(i).Item(0).ToString))
                                                    SCMValores.Parameters.AddWithValue("@no_comp", Val(dtRegistros.Rows(i).Item(1).ToString))
                                                    SCMValores.Parameters.AddWithValue("@version", Val(dtRegistros.Rows(i).Item(6).ToString))
                                                    SCMValores.Parameters.AddWithValue("@fecha_emision", CDate(dtRegistros.Rows(i).Item(3).ToString))
                                                    SCMValores.Parameters.AddWithValue("@fecha_certificacion", dtRegistros.Rows(i).Item(4).ToString)
                                                    SCMValores.Parameters.AddWithValue("@rfc_emisor", dtRegistros.Rows(i).Item(9).ToString)
                                                    SCMValores.Parameters.AddWithValue("@razon_receptor", dtRegistros.Rows(i).Item(8).ToString)
                                                    SCMValores.Parameters.AddWithValue("@rfc_receptor", dtRegistros.Rows(i).Item(7).ToString)
                                                    SCMValores.Parameters.AddWithValue("@razon_emisor", dtRegistros.Rows(i).Item(10).ToString)
                                                    SCMValores.Parameters.AddWithValue("@pac_certificador", dtRegistros.Rows(i).Item(12).ToString)
                                                    SCMValores.Parameters.AddWithValue("@estatus", dtRegistros.Rows(i).Item(13).ToString)
                                                    SCMValores.Parameters.AddWithValue("@tipo", dtRegistros.Rows(i).Item(14).ToString)
                                                    SCMValores.Parameters.AddWithValue("@tipo_comprobante", dtRegistros.Rows(i).Item(14).ToString)
                                                    SCMValores.Parameters.AddWithValue("@serie", dtRegistros.Rows(i).Item(15).ToString)
                                                    SCMValores.Parameters.AddWithValue("@folio", dtRegistros.Rows(i).Item(16).ToString)
                                                    SCMValores.Parameters.AddWithValue("@moneda", dtRegistros.Rows(i).Item(22).ToString)
                                                    SCMValores.Parameters.AddWithValue("@forma_pago", dtRegistros.Rows(i).Item(19).ToString)
                                                    SCMValores.Parameters.AddWithValue("@metodo_pago", dtRegistros.Rows(i).Item(18).ToString)
                                                    SCMValores.Parameters.AddWithValue("@clave_producto", dtRegistros.Rows(i).Item(24).ToString)
                                                    SCMValores.Parameters.AddWithValue("@clave_unidad", dtRegistros.Rows(i).Item(26).ToString)
                                                    SCMValores.Parameters.AddWithValue("@unidad", dtRegistros.Rows(i).Item(27).ToString)
                                                    SCMValores.Parameters.AddWithValue("@cantidad", Val(dtRegistros.Rows(i).Item(28).ToString))
                                                    SCMValores.Parameters.AddWithValue("@num_identificacion", dtRegistros.Rows(i).Item(29).ToString)
                                                    SCMValores.Parameters.AddWithValue("@descripcion", dtRegistros.Rows(i).Item(25).ToString)
                                                    SCMValores.Parameters.AddWithValue("@valor_unitario", Val(dtRegistros.Rows(i).Item(30).ToString))
                                                    SCMValores.Parameters.AddWithValue("@importe", Val(dtRegistros.Rows(i).Item(32).ToString))
                                                    SCMValores.Parameters.AddWithValue("@descuento", Val(dtRegistros.Rows(i).Item(33).ToString))
                                                    SCMValores.Parameters.AddWithValue("@objeto_impuesto", Val(dtRegistros.Rows(i).Item(31).ToString))




                                                    'Total Impuestos
                                                    If dtRegistros.Columns.Contains("Total Trasladado IVA") Then
                                                        SCMValores.Parameters.AddWithValue("@tot_tras_iva", Val(dtRegistros.Rows(i).Item("Total Trasladado IVA").ToString))
                                                        SCMValores.Parameters.AddWithValue("@tot_tras_ieps", Val(dtRegistros.Rows(i).Item("Total Trasladado IEPS").ToString))
                                                    End If
                                                    'Total Retenciones
                                                    If dtRegistros.Columns.Contains("Total Retenido IVA") Then
                                                        SCMValores.Parameters.AddWithValue("@tot_ret_iva", Val(dtRegistros.Rows(i).Item("Total Retenido IVA").ToString))
                                                        SCMValores.Parameters.AddWithValue("@tot_ret_ieps", Val(dtRegistros.Rows(i).Item("Total Retenido IEPS").ToString))
                                                        SCMValores.Parameters.AddWithValue("@tot_ret_isr", Val(dtRegistros.Rows(i).Item("Total Retenido ISR").ToString))
                                                    End If
                                                    'Impuestos 1
                                                    If dtRegistros.Columns.Contains("Trasladado - Base 1") Then
                                                        If dtRegistros.Rows(i).Item("Trasladado - Impuesto 1").ToString = "" Then
                                                            'Sin Impuesto Trasladado
                                                            SCMValores.Parameters.AddWithValue("@base_tras_1", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@impuesto_tras_1", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tipo_tras_1", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tasa_tras_1", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@importe_tras_1", DBNull.Value)
                                                        Else
                                                            'Con Impuesto Trasladado
                                                            SCMValores.Parameters.AddWithValue("@base_tras_1", Val(dtRegistros.Rows(i).Item("Trasladado - Base 1").ToString))
                                                            SCMValores.Parameters.AddWithValue("@impuesto_tras_1", dtRegistros.Rows(i).Item("Trasladado - Impuesto 1").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tipo_tras_1", dtRegistros.Rows(i).Item("Trasladado - Tipo o Factor 1").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tasa_tras_1", Val(dtRegistros.Rows(i).Item("Trasladado - Tasa o Cuota 1").ToString))
                                                            'Trasladado - Tasa o Cuota 1    Trasladado - Tasa o Couta 1'  
                                                            SCMValores.Parameters.AddWithValue("@importe_tras_1", Val(dtRegistros.Rows(i).Item("Trasladado - Importe 1").ToString))
                                                        End If
                                                    End If
                                                    'Impuestos 2
                                                    If dtRegistros.Columns.Contains("Trasladado - Base 2") Then
                                                        If dtRegistros.Rows(i).Item("Trasladado - Impuesto 2").ToString = "" Then
                                                            'Sin Impuesto Trasladado
                                                            SCMValores.Parameters.AddWithValue("@base_tras_2", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@impuesto_tras_2", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tipo_tras_2", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tasa_tras_2", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@importe_tras_2", DBNull.Value)
                                                        Else
                                                            'Con Impuesto Trasladado
                                                            SCMValores.Parameters.AddWithValue("@base_tras_2", Val(dtRegistros.Rows(i).Item("Trasladado - Base 2").ToString))
                                                            SCMValores.Parameters.AddWithValue("@impuesto_tras_2", dtRegistros.Rows(i).Item("Trasladado - Impuesto 2").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tipo_tras_2", dtRegistros.Rows(i).Item("Trasladado - Tipo o Factor 2").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tasa_tras_2", Val(dtRegistros.Rows(i).Item("Trasladado - Tasa o Cuota 2").ToString))
                                                            SCMValores.Parameters.AddWithValue("@importe_tras_2", Val(dtRegistros.Rows(i).Item("Trasladado - Importe 2").ToString))
                                                        End If
                                                    End If
                                                    'Impuestos 3
                                                    If dtRegistros.Columns.Contains("Trasladado - Base 3") Then
                                                        If dtRegistros.Rows(i).Item("Trasladado - Impuesto 3").ToString = "" Then
                                                            'Sin Impuesto Trasladado
                                                            SCMValores.Parameters.AddWithValue("@base_tras_3", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@impuesto_tras_3", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tipo_tras_3", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tasa_tras_3", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@importe_tras_3", DBNull.Value)
                                                        Else
                                                            'Con Impuesto Trasladado
                                                            SCMValores.Parameters.AddWithValue("@base_tras_3", Val(dtRegistros.Rows(i).Item("Trasladado - Base 3").ToString))
                                                            SCMValores.Parameters.AddWithValue("@impuesto_tras_3", dtRegistros.Rows(i).Item("Trasladado - Impuesto 3").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tipo_tras_3", dtRegistros.Rows(i).Item("Trasladado - Tipo o Factor 3").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tasa_tras_3", Val(dtRegistros.Rows(i).Item("Trasladado - Tasa o Cuota 3").ToString))
                                                            SCMValores.Parameters.AddWithValue("@importe_tras_3", Val(dtRegistros.Rows(i).Item("Trasladado - Importe 3").ToString))
                                                        End If
                                                    End If
                                                    'Impuestos 4
                                                    If dtRegistros.Columns.Contains("Trasladado - Base 4") Then
                                                        If dtRegistros.Rows(i).Item("Trasladado - Impuesto 4").ToString = "" Then
                                                            'Sin Impuesto Trasladado
                                                            SCMValores.Parameters.AddWithValue("@base_tras_4", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@impuesto_tras_4", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tipo_tras_4", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tasa_tras_4", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@importe_tras_4", DBNull.Value)
                                                        Else
                                                            'Con Impuesto Trasladado
                                                            SCMValores.Parameters.AddWithValue("@base_tras_4", Val(dtRegistros.Rows(i).Item("Trasladado - Base 4").ToString))
                                                            SCMValores.Parameters.AddWithValue("@impuesto_tras_4", dtRegistros.Rows(i).Item("Trasladado - Impuesto 4").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tipo_tras_4", dtRegistros.Rows(i).Item("Trasladado - Tipo o Factor 4").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tasa_tras_4", Val(dtRegistros.Rows(i).Item("Trasladado - Tasa o Cuota 4").ToString))
                                                            SCMValores.Parameters.AddWithValue("@importe_tras_4", Val(dtRegistros.Rows(i).Item("Trasladado - Importe 4").ToString))
                                                        End If
                                                    End If
                                                    'Retención 1
                                                    If dtRegistros.Columns.Contains("Retenido - Base 1") Then
                                                        If dtRegistros.Rows(i).Item("Retenido - Impuesto 1").ToString = "" Then
                                                            'Sin Impuesto Retenido
                                                            SCMValores.Parameters.AddWithValue("@base_ret_1", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@impuesto_ret_1", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tipo_ret_1", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tasa_ret_1", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@importe_ret_1", DBNull.Value)
                                                        Else
                                                            'Con Impuesto Retenido
                                                            SCMValores.Parameters.AddWithValue("@base_ret_1", Val(dtRegistros.Rows(i).Item("Retenido - Base 1").ToString))
                                                            SCMValores.Parameters.AddWithValue("@impuesto_ret_1", dtRegistros.Rows(i).Item("Retenido - Impuesto 1").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tipo_ret_1", dtRegistros.Rows(i).Item("Retenido - Tipo o Factor 1").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tasa_ret_1", Val(dtRegistros.Rows(i).Item("Retenido - Tasa o Cuota 1").ToString))
                                                            SCMValores.Parameters.AddWithValue("@importe_ret_1", Val(dtRegistros.Rows(i).Item("Retenido - Importe 1").ToString))
                                                        End If
                                                    End If
                                                    'Retención 2
                                                    If dtRegistros.Columns.Contains("Retenido - Base 2") Then
                                                        If dtRegistros.Rows(i).Item("Retenido - Impuesto 2").ToString = "" Then
                                                            'Sin Impuesto Retenido
                                                            SCMValores.Parameters.AddWithValue("@base_ret_2", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@impuesto_ret_2", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tipo_ret_2", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tasa_ret_2", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@importe_ret_2", DBNull.Value)
                                                        Else
                                                            'Con Impuesto Retenido
                                                            SCMValores.Parameters.AddWithValue("@base_ret_2", Val(dtRegistros.Rows(i).Item("Retenido - Base 2").ToString))
                                                            SCMValores.Parameters.AddWithValue("@impuesto_ret_2", dtRegistros.Rows(i).Item("Retenido - Impuesto 2").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tipo_ret_2", dtRegistros.Rows(i).Item("Retenido - Tipo o Factor 2").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tasa_ret_2", Val(dtRegistros.Rows(i).Item("Retenido - Tasa o Cuota 2").ToString))
                                                            'Retenido - Tasa o Cuota 2

                                                            SCMValores.Parameters.AddWithValue("@importe_ret_2", Val(dtRegistros.Rows(i).Item("Retenido - Importe 2").ToString))
                                                        End If
                                                    End If
                                                    'Retención 3
                                                    If dtRegistros.Columns.Contains("Retenido - Base 3") Then
                                                        If dtRegistros.Rows(i).Item("Retenido - Impuesto 3").ToString = "" Then
                                                            'Sin Impuesto Retenido
                                                            SCMValores.Parameters.AddWithValue("@base_ret_3", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@impuesto_ret_3", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tipo_ret_3", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tasa_ret_3", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@importe_ret_3", DBNull.Value)
                                                        Else
                                                            'Con Impuesto Retenido
                                                            SCMValores.Parameters.AddWithValue("@base_ret_3", Val(dtRegistros.Rows(i).Item("Retenido - Base 3").ToString))
                                                            SCMValores.Parameters.AddWithValue("@impuesto_ret_3", dtRegistros.Rows(i).Item("Retenido - Impuesto 3").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tipo_ret_3", dtRegistros.Rows(i).Item("Retenido - Tipo o Factor 3").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tasa_ret_3", Val(dtRegistros.Rows(i).Item("Retenido - Tasa o Cuota 3").ToString))
                                                            SCMValores.Parameters.AddWithValue("@importe_ret_3", Val(dtRegistros.Rows(i).Item("Retenido - Importe 3").ToString))
                                                        End If
                                                    End If
                                                    'Retención 4
                                                    If dtRegistros.Columns.Contains("Retenido - Base 4") Then
                                                        If dtRegistros.Rows(i).Item("Retenido - Impuesto 4").ToString = "" Then
                                                            'Sin Impuesto Retenido
                                                            SCMValores.Parameters.AddWithValue("@base_ret_4", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@impuesto_ret_4", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tipo_ret_4", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@tasa_ret_4", DBNull.Value)
                                                            SCMValores.Parameters.AddWithValue("@importe_ret_4", DBNull.Value)
                                                        Else
                                                            'Con Impuesto Retenido
                                                            SCMValores.Parameters.AddWithValue("@base_ret_4", Val(dtRegistros.Rows(i).Item("Retenido - Base 4").ToString))
                                                            SCMValores.Parameters.AddWithValue("@impuesto_ret_4", dtRegistros.Rows(i).Item("Retenido - Impuesto 4").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tipo_ret_4", dtRegistros.Rows(i).Item("Retenido - Tipo o Factor 4").ToString)
                                                            SCMValores.Parameters.AddWithValue("@tasa_ret_4", Val(dtRegistros.Rows(i).Item("Retenido - Tasa o Cuota 4").ToString))
                                                            SCMValores.Parameters.AddWithValue("@importe_ret_4", Val(dtRegistros.Rows(i).Item("Retenido - Importe 4").ToString))
                                                        End If
                                                    End If

                                                    ''Versión Anterior
                                                    ''Asignación por Nombre de Columna
                                                    'If dtRegistros.Columns().Count > 33 Then '28
                                                    '    SCMValores.Parameters.AddWithValue("@tot_tras_iva", Val(dtRegistros.Rows(i).Item("Total Trasladado IVA").ToString))
                                                    '    SCMValores.Parameters.AddWithValue("@tot_tras_ieps", Val(dtRegistros.Rows(i).Item("Total Trasladado IEPS").ToString))
                                                    '    'Con 1 tipo de Impuesto
                                                    '    If dtRegistros.Rows(i).Item("Trasladado - Impuesto 1").ToString = "" Then
                                                    '        'Sin Impuesto Trasladado
                                                    '        SCMValores.Parameters.AddWithValue("@base_tras_1", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_tras_1", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_tras_1", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_tras_1", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@importe_tras_1", DBNull.Value)
                                                    '    Else
                                                    '        'Con Impuesto Trasladado
                                                    '        SCMValores.Parameters.AddWithValue("@base_tras_1", Val(dtRegistros.Rows(i).Item("Trasladado - Base 1").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_tras_1", dtRegistros.Rows(i).Item("Trasladado - Impuesto 1").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_tras_1", dtRegistros.Rows(i).Item("Trasladado - Tipo o Factor 1").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_tras_1", Val(dtRegistros.Rows(i).Item("Trasladado - Tasa o Couta 1").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@importe_tras_1", Val(dtRegistros.Rows(i).Item("Trasladado - Importe 1").ToString))
                                                    '    End If
                                                    'End If
                                                    ''Asignación de Impuestos
                                                    'If dtRegistros.Columns().Count >= 48 Then '43
                                                    '    SCMValores.Parameters.AddWithValue("@tot_ret_iva", Val(dtRegistros.Rows(i).Item("Total Retenido IVA").ToString))
                                                    '    SCMValores.Parameters.AddWithValue("@tot_ret_ieps", Val(dtRegistros.Rows(i).Item("Total Retenido IEPS").ToString))
                                                    '    SCMValores.Parameters.AddWithValue("@tot_ret_isr", Val(dtRegistros.Rows(i).Item("Total Retenido ISR").ToString))
                                                    '    'Con 1 tipo de Retención
                                                    '    If dtRegistros.Rows(i).Item("Retenido - Impuesto 1").ToString = "" Then
                                                    '        'Sin Impuesto Retenido
                                                    '        SCMValores.Parameters.AddWithValue("@base_ret_1", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_ret_1", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_ret_1", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_ret_1", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@importe_ret_1", DBNull.Value)
                                                    '    Else
                                                    '        'Con Impuesto Retenido
                                                    '        SCMValores.Parameters.AddWithValue("@base_ret_1", Val(dtRegistros.Rows(i).Item("Retenido - Base 1").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_ret_1", dtRegistros.Rows(i).Item("Retenido - Impuesto 1").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_ret_1", dtRegistros.Rows(i).Item("Retenido - Tipo o Factor 1").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_ret_1", Val(dtRegistros.Rows(i).Item("Retenido - Tasa o Couta 1").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@importe_ret_1", Val(dtRegistros.Rows(i).Item("Retenido - Importe 1").ToString))
                                                    '    End If
                                                    'End If
                                                    'If dtRegistros.Columns().Count = 45 Then '40
                                                    '    'Con 2 tipo de Impuesto
                                                    '    If dtRegistros.Rows(i).Item("Trasladado - Impuesto 2").ToString = "" Then
                                                    '        'Sin Impuesto Trasladado
                                                    '        SCMValores.Parameters.AddWithValue("@base_tras_2", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_tras_2", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_tras_2", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_tras_2", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@importe_tras_2", DBNull.Value)
                                                    '    Else
                                                    '        'Con Impuesto Trasladado
                                                    '        SCMValores.Parameters.AddWithValue("@base_tras_2", Val(dtRegistros.Rows(i).Item("Trasladado - Base 2").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_tras_2", dtRegistros.Rows(i).Item("Trasladado - Impuesto 2").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_tras_2", dtRegistros.Rows(i).Item("Trasladado - Tipo o Factor 2").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_tras_2", Val(dtRegistros.Rows(i).Item("Trasladado - Tasa o Couta 2").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@importe_tras_2", Val(dtRegistros.Rows(i).Item("Trasladado - Importe 2").ToString))
                                                    '    End If
                                                    'End If
                                                    'If dtRegistros.Columns().Count >= 53 Then '48
                                                    '    'Con 2 tipo de Impuesto
                                                    '    If dtRegistros.Rows(i).Item("Trasladado - Impuesto 2").ToString = "" Then
                                                    '        'Sin Impuesto Trasladado
                                                    '        SCMValores.Parameters.AddWithValue("@base_tras_2", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_tras_2", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_tras_2", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_tras_2", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@importe_tras_2", DBNull.Value)
                                                    '    Else
                                                    '        'Con Impuesto Trasladado
                                                    '        SCMValores.Parameters.AddWithValue("@base_tras_2", Val(dtRegistros.Rows(i).Item("Trasladado - Base 2").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_tras_2", dtRegistros.Rows(i).Item("Trasladado - Impuesto 2").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_tras_2", dtRegistros.Rows(i).Item("Trasladado - Tipo o Factor 2").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_tras_2", Val(dtRegistros.Rows(i).Item("Trasladado - Tasa o Couta 2").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@importe_tras_2", Val(dtRegistros.Rows(i).Item("Trasladado - Importe 2").ToString))
                                                    '    End If
                                                    '    If dtRegistros.Columns().Count >= 58 Then '53
                                                    '        If dtRegistros.Rows(i).Item("Retenido - Impuesto 2").ToString = "" Then
                                                    '            'Sin Impuesto Retenido
                                                    '            SCMValores.Parameters.AddWithValue("@base_ret_2", DBNull.Value)
                                                    '            SCMValores.Parameters.AddWithValue("@impuesto_ret_2", DBNull.Value)
                                                    '            SCMValores.Parameters.AddWithValue("@tipo_ret_2", DBNull.Value)
                                                    '            SCMValores.Parameters.AddWithValue("@tasa_ret_2", DBNull.Value)
                                                    '            SCMValores.Parameters.AddWithValue("@importe_ret_2", DBNull.Value)
                                                    '        Else
                                                    '            'Con Impuesto Retenido
                                                    '            SCMValores.Parameters.AddWithValue("@base_ret_2", Val(dtRegistros.Rows(i).Item("Retenido - Base 2").ToString))
                                                    '            SCMValores.Parameters.AddWithValue("@impuesto_ret_2", dtRegistros.Rows(i).Item("Retenido - Impuesto 2").ToString)
                                                    '            SCMValores.Parameters.AddWithValue("@tipo_ret_2", dtRegistros.Rows(i).Item("Retenido - Tipo o Factor 2").ToString)
                                                    '            SCMValores.Parameters.AddWithValue("@tasa_ret_2", Val(dtRegistros.Rows(i).Item("Retenido - Tasa o Couta 2").ToString))
                                                    '            SCMValores.Parameters.AddWithValue("@importe_ret_2", Val(dtRegistros.Rows(i).Item("Retenido - Importe 2").ToString))
                                                    '        End If
                                                    '    End If
                                                    'End If
                                                    'If dtRegistros.Columns().Count >= 68 Then '63
                                                    '    'Con 3 tipo de Impuesto
                                                    '    If dtRegistros.Rows(i).Item("Trasladado - Impuesto 3").ToString = "" Then
                                                    '        'Sin Impuesto Trasladado
                                                    '        SCMValores.Parameters.AddWithValue("@base_tras_3", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_tras_3", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_tras_3", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_tras_3", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@importe_tras_3", DBNull.Value)
                                                    '    Else
                                                    '        'Con Impuesto Trasladado
                                                    '        SCMValores.Parameters.AddWithValue("@base_tras_3", Val(dtRegistros.Rows(i).Item("Trasladado - Base 3").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_tras_3", dtRegistros.Rows(i).Item("Trasladado - Impuesto 3").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_tras_3", dtRegistros.Rows(i).Item("Trasladado - Tipo o Factor 3").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_tras_3", Val(dtRegistros.Rows(i).Item("Trasladado - Tasa o Couta 3").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@importe_tras_3", Val(dtRegistros.Rows(i).Item("Trasladado - Importe 3").ToString))
                                                    '    End If
                                                    '    If dtRegistros.Rows(i).Item("Retenido - Impuesto 3").ToString = "" Then
                                                    '        'Sin Impuesto Retenido
                                                    '        SCMValores.Parameters.AddWithValue("@base_ret_3", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_ret_3", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_ret_3", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_ret_3", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@importe_ret_3", DBNull.Value)
                                                    '    Else
                                                    '        'Con Impuesto Retenido
                                                    '        SCMValores.Parameters.AddWithValue("@base_ret_3", Val(dtRegistros.Rows(i).Item("Retenido - Base 3").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_ret_3", dtRegistros.Rows(i).Item("Retenido - Impuesto 3").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_ret_3", dtRegistros.Rows(i).Item("Retenido - Tipo o Factor 3").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_ret_3", Val(dtRegistros.Rows(i).Item("Retenido - Tasa o Couta 3").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@importe_ret_3", Val(dtRegistros.Rows(i).Item("Retenido - Importe 3").ToString))
                                                    '    End If
                                                    'End If
                                                    'If dtRegistros.Columns().Count >= 78 Then '73
                                                    '    'Con 4 tipo de Impuesto
                                                    '    If dtRegistros.Rows(i).Item("Trasladado - Impuesto 4").ToString = "" Then
                                                    '        'Sin Impuesto Trasladado
                                                    '        SCMValores.Parameters.AddWithValue("@base_tras_4", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_tras_4", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_tras_4", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_tras_4", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@importe_tras_4", DBNull.Value)
                                                    '    Else
                                                    '        'Con Impuesto Trasladado
                                                    '        SCMValores.Parameters.AddWithValue("@base_tras_4", Val(dtRegistros.Rows(i).Item("Trasladado - Base 4").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_tras_4", dtRegistros.Rows(i).Item("Trasladado - Impuesto 4").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_tras_4", dtRegistros.Rows(i).Item("Trasladado - Tipo o Factor 4").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_tras_4", Val(dtRegistros.Rows(i).Item("Trasladado - Tasa o Couta 4").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@importe_tras_4", Val(dtRegistros.Rows(i).Item("Trasladado - Importe 4").ToString))
                                                    '    End If
                                                    '    If dtRegistros.Rows(i).Item("Retenido - Impuesto 4").ToString = "" Then
                                                    '        'Sin Impuesto Retenido
                                                    '        SCMValores.Parameters.AddWithValue("@base_ret_4", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_ret_4", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_ret_4", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_ret_4", DBNull.Value)
                                                    '        SCMValores.Parameters.AddWithValue("@importe_ret_4", DBNull.Value)
                                                    '    Else
                                                    '        'Con Impuesto Retenido
                                                    '        SCMValores.Parameters.AddWithValue("@base_ret_4", Val(dtRegistros.Rows(i).Item("Retenido - Base 4").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@impuesto_ret_4", dtRegistros.Rows(i).Item("Retenido - Impuesto 4").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tipo_ret_4", dtRegistros.Rows(i).Item("Retenido - Tipo o Factor 4").ToString)
                                                    '        SCMValores.Parameters.AddWithValue("@tasa_ret_4", Val(dtRegistros.Rows(i).Item("Retenido - Tasa o Couta 4").ToString))
                                                    '        SCMValores.Parameters.AddWithValue("@importe_ret_4", Val(dtRegistros.Rows(i).Item("Retenido - Importe 4").ToString))
                                                    '    End If
                                                    'End If

                                                    ConexionBD.Open()
                                                    SCMValores.ExecuteNonQuery()
                                                    ConexionBD.Close()
                                                Next
                                            Case Else
                                                .litError.Text = "Versión Inválida"
                                        End Select
                                    End If

                                    mensaje = mensaje + Chr(13) & Chr(10) + Chr(13) & Chr(10) + "Insertados: " + cIns.ToString + Chr(13) & Chr(10) + "Actualizados: " + cAct.ToString + Chr(13) & Chr(10) + "Omitidos: " + cOmi.ToString

                                    .txtResultado.Visible = True
                                    .txtResultado.Text = mensaje
                                    .lbl_UuidOmitido.Visible = True
                                    .txtUuidOmitido.Visible = True

                                    .btnAceptar.Enabled = False
                                    .btnNuevo.Enabled = True
                                End If
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