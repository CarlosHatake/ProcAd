Public Class _46
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 3
                    'Session("idMsInst") = 29622

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtIdMsInst.Text = Session("idMsInst")
                        ._txtBan.Text = 0

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim sdaSol As New SqlDataAdapter
                        Dim dsSol As New DataSet
                        Dim query As String
                        query = "select id_ms_factura " + _
                                "     , empleado " + _
                                "     , empresa " + _
                                "     , isnull(division, centro_costo) as centro_costo " + _
                                "     , case when division is null then 'CC' else 'DIV' end as dimension " + _
                                "     , isnull(tipo_servicio, servicio) as tipo_servicio " + _
                                "     , validador " + _
                                "     , especificaciones " + _
                                "     , comentario_valida " + _
                                "     , case contrato_NAV_alta when 'S' then 'Sí' else 'No' end as contrato_NAV_alta " + _
                                "     , cFinanzas " + _
                                "     , cPresupuesto " + _
                                "from ms_factura " + _
                                "  left join ms_instancia on ms_factura.id_ms_factura = ms_instancia.id_ms_sol and ms_instancia.tipo = 'F' " + _
                                "  left join bd_SiSAC.dbo.cg_proveedor on ms_factura.proveedor_selec = cg_proveedor.id_proveedor " + _
                                "where id_ms_instancia = @idMsInst "
                        sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@idMsInst", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()
                        .lblFolio.Text = dsSol.Tables(0).Rows(0).Item("id_ms_factura").ToString()
                        .lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("empleado").ToString()
                        .lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                        If dsSol.Tables(0).Rows(0).Item("dimension").ToString() = "CC" Then
                            .lbl_CC.Text = "Centro de Costo:"
                        Else
                            .lbl_CC.Text = "División:"
                        End If
                        .lblCC.Text = dsSol.Tables(0).Rows(0).Item("centro_costo").ToString()
                        .lblTipoServicio.Text = dsSol.Tables(0).Rows(0).Item("tipo_servicio").ToString()
                        .lblValidador.Text = dsSol.Tables(0).Rows(0).Item("validador").ToString()
                        .txtEspecificaciones.Text = dsSol.Tables(0).Rows(0).Item("especificaciones").ToString()
                        .txtComentarioVal.Text = dsSol.Tables(0).Rows(0).Item("comentario_valida").ToString()
                        .lblContratoNAV.Text = dsSol.Tables(0).Rows(0).Item("contrato_NAV_alta").ToString()
                        ._txtFinanzas.Text = dsSol.Tables(0).Rows(0).Item("cFinanzas").ToString()
                        ._txtValPresup.Text = dsSol.Tables(0).Rows(0).Item("cPresupuesto").ToString()
                        sdaSol.Dispose()
                        dsSol.Dispose()

                        'Adjuntos
                        Dim sdaArchivos As New SqlDataAdapter
                        Dim dsArchivos As New DataSet
                        .gvAdjuntos.DataSource = dsArchivos
                        sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " + _
                                                                   "     , 'http://148.223.153.43/ProcAd - Adjuntos IngFact/' + cast(id_dt_archivo as varchar(20)) + '-' + nombre as path " + _
                                                                   "from dt_archivo " + _
                                                                   "where id_ms_factura = @idMsFactura " + _
                                                                   "  and tipo = 'A' ", ConexionBD)
                        sdaArchivos.SelectCommand.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        sdaArchivos.Fill(dsArchivos)
                        .gvAdjuntos.DataBind()
                        ConexionBD.Close()
                        sdaArchivos.Dispose()
                        dsArchivos.Dispose()
                        .gvAdjuntos.SelectedIndex = -1

                        'Llenar listas de Divisa
                        Dim sdaDivisa As New SqlDataAdapter
                        Dim dsDivisa As New DataSet
                        .ddlDivisa1.DataSource = dsDivisa
                        .ddlDivisa2.DataSource = dsDivisa
                        .ddlDivisa3.DataSource = dsDivisa
                        sdaDivisa.SelectCommand = New SqlCommand("select id_divisa, divisa as divisa from bd_SiSAC.dbo.cg_divisa where status = 'A' order by divisa", ConexionBD)
                        .ddlDivisa1.DataTextField = "divisa"
                        .ddlDivisa1.DataValueField = "id_divisa"
                        .ddlDivisa2.DataTextField = "divisa"
                        .ddlDivisa2.DataValueField = "id_divisa"
                        .ddlDivisa3.DataTextField = "divisa"
                        .ddlDivisa3.DataValueField = "id_divisa"
                        ConexionBD.Open()
                        sdaDivisa.Fill(dsDivisa)
                        .ddlDivisa1.DataBind()
                        .ddlDivisa2.DataBind()
                        .ddlDivisa3.DataBind()
                        ConexionBD.Close()
                        sdaDivisa.Dispose()
                        dsDivisa.Dispose()
                        .ddlDivisa1.SelectedIndex = 1
                        .ddlDivisa2.SelectedIndex = 1
                        .ddlDivisa3.SelectedIndex = 1
                        'Llenar listas de Condiciones de Pago
                        Dim sdaCPago As New SqlDataAdapter
                        Dim dsCPago As New DataSet
                        .ddlCondP1.DataSource = dsCPago
                        .ddlCondP2.DataSource = dsCPago
                        .ddlCondP3.DataSource = dsCPago
                        sdaCPago.SelectCommand = New SqlCommand("select id_cond_pago, cond_pago from bd_SiSAC.dbo.cg_cond_pago where status = 'A' order by cond_pago", ConexionBD)
                        .ddlCondP1.DataTextField = "cond_pago"
                        .ddlCondP1.DataValueField = "id_cond_pago"
                        .ddlCondP2.DataTextField = "cond_pago"
                        .ddlCondP2.DataValueField = "id_cond_pago"
                        .ddlCondP3.DataTextField = "cond_pago"
                        .ddlCondP3.DataValueField = "id_cond_pago"
                        ConexionBD.Open()
                        sdaCPago.Fill(dsCPago)
                        .ddlCondP1.DataBind()
                        .ddlCondP2.DataBind()
                        .ddlCondP3.DataBind()
                        ConexionBD.Close()
                        sdaCPago.Dispose()
                        dsCPago.Dispose()
                        'Llenar listas de Proveedores
                        llenarProv(.txtProveedor1, .ddlProveedor1, .upProveedor1)
                        llenarProv(.txtProveedor2, .ddlProveedor2, .upProveedor2)
                        llenarProv(.txtProveedor3, .ddlProveedor3, .upProveedor3)

                        'Panel
                        .pnlInicio.Enabled = True
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

    Public Sub llenarProv(ByRef txtProveedor, ByRef ddlProveedor, ByRef upProveedor)
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaProv As New SqlDataAdapter
                Dim dsProv As New DataSet
                Dim query As String
                query = "select distinct(rfc) as rfc_emisor, id_proveedor, rfc + ' / ' + nombre as proveedor " + _
                        "from bd_SiSAC.dbo.cg_proveedor " + _
                        "where (rfc <> '' and rfc is not null) " + _
                        "  and (rfc like '%' + @valor+ '%' or nombre like '%' + @valor+ '%') "
                sdaProv.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaProv.SelectCommand.Parameters.AddWithValue("@valor", txtProveedor.Text.Trim)
                ddlProveedor.DataSource = dsProv
                ddlProveedor.DataTextField = "proveedor"
                ddlProveedor.DataValueField = "id_proveedor"
                ConexionBD.Open()
                sdaProv.Fill(dsProv)
                ddlProveedor.DataBind()
                ConexionBD.Close()
                sdaProv.Dispose()
                dsProv.Dispose()
                ddlProveedor.SelectedIndex = -1
                upProveedor.Update()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Function obtenerTipoCambio(ByRef divisa)
        Dim ConexionBDNAV As SqlConnection = New System.Data.SqlClient.SqlConnection
        ConexionBDNAV.ConnectionString = accessDB.conBD("NAV")
        Dim SCMDivisa As SqlCommand = New System.Data.SqlClient.SqlCommand
        SCMDivisa.Connection = ConexionBDNAV
        Dim conteo As Integer = 0
        'Obtener tipo de cambio NAV
        SCMDivisa.CommandText = ""
        SCMDivisa.Parameters.Clear()
        SCMDivisa.CommandText = "select ISNULL((select[Relational Exch_ Rate Amount] " + _
                                "               from UnneProd.dbo.[TRACSA$Currency Exchange Rate] cgTipoCambio " + _
                                "               where [Starting Date] = CAST(GETDATE() as date) " + _
                                "                 and [Currency Code] = @divisa), ISNULL((select top 1 [Relational Exch_ Rate Amount] " + _
                                "                                                         from UnneProd.dbo.[TRACSA$Currency Exchange Rate] cgTipoCambio " + _
                                "                                                         where [Currency Code] = @divisa " + _
                                "                                                         order by [Starting Date] desc), 1)) as tipo_cambio "
        SCMDivisa.Parameters.AddWithValue("@divisa", divisa)
        ConexionBDNAV.Open()
        obtenerTipoCambio = SCMDivisa.ExecuteScalar
        ConexionBDNAV.Close()
    End Function

    Private Sub DeleteFile(ByVal strFileName As String)
        If strFileName.Trim().Length > 0 Then
            Dim fi As New FileInfo(strFileName)
            If (fi.Exists) Then    'Si existe el archivo, eliminarlo
                fi.Delete()
            End If
        End If
    End Sub

#End Region

#Region "Proveedores"

    Protected Sub ibtnBuscarP1_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBuscarP1.Click
        llenarProv(Me.txtProveedor1, Me.ddlProveedor1, Me.upProveedor1)
    End Sub

    Protected Sub ibtnBuscarP2_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBuscarP2.Click
        llenarProv(Me.txtProveedor2, Me.ddlProveedor2, Me.upProveedor2)
    End Sub

    Protected Sub ibtnBuscarP3_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBuscarP3.Click
        llenarProv(Me.txtProveedor3, Me.ddlProveedor3, Me.upProveedor3)
    End Sub

#End Region

#Region "Guardar"

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        With Me
            Try
                .litError.Text = ""

                Dim ban As Integer = 0
                If .txtComentario.Text.Trim = "" Then
                    .litError.Text = "Información Insuficiente, favor de: ingresar los comentarios correspondientes"
                    ban = 1
                End If
                If .rblProvSel.SelectedIndex = -1 Then
                    If ban = 1 Then
                        .litError.Text = .litError.Text + "; "
                    Else
                        .litError.Text = "Información Insuficiente, favor de: "
                        ban = 1
                    End If
                    .litError.Text = .litError.Text + "seleccionar el Proveedor Elegido"
                Else
                    Dim banPS As Integer = 0
                    Select Case .rblProvSel.SelectedValue
                        Case 1
                            If .ddlProveedor1.Items.Count = 0 Then
                                banPS = 1
                            End If
                        Case 2
                            If .ddlProveedor2.Items.Count = 0 Then
                                banPS = 1
                            End If
                        Case 3
                            If .ddlProveedor3.Items.Count = 0 Then
                                banPS = 1
                            End If
                    End Select
                    If banPS = 1 Then
                        If ban = 1 Then
                            .litError.Text = .litError.Text + "; "
                        Else
                            .litError.Text = "Información Insuficiente, favor de: "
                            ban = 1
                        End If
                        .litError.Text = .litError.Text + "seleccionar un Proveedor dado de alta en el SiSAC"
                    End If
                End If
                If .ddlProveedor1.Items.Count = 0 And .ddlProveedor2.Items.Count = 0 And .ddlProveedor3.Items.Count = 0 Then
                    If ban = 1 Then
                        .litError.Text = .litError.Text + "; "
                    Else
                        .litError.Text = "Información Insuficiente, favor de: "
                        ban = 1
                    End If
                    .litError.Text = .litError.Text + "indicar los proveedores de las cotizaciones"
                End If
                If .wceImporte1.Text = "" Or .wceImporte2.Text = "" Or .wceImporte3.Text = "" Then
                    If ban = 1 Then
                        .litError.Text = .litError.Text + "; "
                    Else
                        .litError.Text = "Información Insuficiente, favor de: "
                        ban = 1
                    End If
                    .litError.Text = .litError.Text + "ingresar los importes de las cotizaciones"
                End If
                Dim banP As Integer = 0
                If .ddlProveedor1.Items.Count > 0 And .ddlProveedor2.Items.Count > 0 Then
                    If .ddlProveedor1.SelectedValue = .ddlProveedor2.SelectedValue Then
                        banP = 1
                    End If
                End If
                If .ddlProveedor2.Items.Count > 0 And .ddlProveedor3.Items.Count > 0 Then
                    If .ddlProveedor2.SelectedValue = .ddlProveedor3.SelectedValue Then
                        banP = 1
                    End If
                End If
                If .ddlProveedor3.Items.Count > 0 And .ddlProveedor1.Items.Count > 0 Then
                    If .ddlProveedor3.SelectedValue = .ddlProveedor1.SelectedValue Then
                        banP = 1
                    End If
                End If
                If banP = 1 Then
                    If ban = 1 Then
                        .litError.Text = .litError.Text + "; "
                    Else
                        ban = 1
                    End If
                    .litError.Text = .litError.Text + "Información Inválida, no se pueden repetir los proveedores"
                End If


                If ban = 0 Then
                    'Verificar que el archivo ha sido seleccionado y es un archivo válido
                    If (Not fuPDF1.PostedFile Is Nothing) And (fuPDF1.PostedFile.ContentLength > 0) And (Not fuPDF2.PostedFile Is Nothing) And (fuPDF2.PostedFile.ContentLength > 0) And (Not fuPDF3.PostedFile Is Nothing) And (fuPDF3.PostedFile.ContentLength > 0) Then
                        ' '' Ruta Local
                        ''Dim sFileDir As String = "C:/ProcAd - Adjuntos IngFact Cot/" 'Ruta en que se almacenará el archivo
                        ' Ruta en Atenea
                        Dim sFileDir As String = "D:\ProcAd - Adjuntos IngFact Cot\" 'Ruta en que se almacenará el archivo
                        Dim lMaxFileSize As Long = 10485760 'Equivalente a 10 Megabytes

                        'Determinar el nombre original del archivo
                        Dim sFileName1 As String = System.IO.Path.GetFileName(fuPDF1.PostedFile.FileName)
                        Dim sFileName2 As String = System.IO.Path.GetFileName(fuPDF2.PostedFile.FileName)
                        Dim sFileName3 As String = System.IO.Path.GetFileName(fuPDF3.PostedFile.FileName)
                        Dim idArchivo As Integer 'Index correspondiente al archivo
                        Dim fecha As DateTime = Date.Now
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        Dim SCMTemp As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMTemp.Connection = ConexionBD
                        Try
                            Dim tipoCambio1 As Decimal = obtenerTipoCambio(ddlDivisa1.SelectedItem.Text)
                            Dim tipoCambio2 As Decimal = obtenerTipoCambio(ddlDivisa2.SelectedItem.Text)
                            Dim tipoCambio3 As Decimal = obtenerTipoCambio(ddlDivisa3.SelectedItem.Text)
                            'Registrar las cotizaciones en la Base de Datos
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "insert into dt_cotizacion( id_ms_factura,  no_cotizacion,  id_proveedor,  prov_codigo,  prov_nombre,  precio,  id_divisa,  tipo_cambio,  fecha_ini,  fecha_fin,  id_cond_pago,  nombre_archivo) " + _
                                                     "                   values(@id_ms_factura, @no_cotizacion, @id_proveedor, @prov_codigo, @prov_nombre, @precio, @id_divisa, @tipo_cambio, @fecha_ini, @fecha_fin, @id_cond_pago, @nombre_archivo)"
                            SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                            SCMValores.Parameters.Add("@no_cotizacion", SqlDbType.Int)
                            SCMValores.Parameters.Add("@id_proveedor", SqlDbType.Int)
                            SCMValores.Parameters.Add("@prov_codigo", SqlDbType.VarChar)
                            SCMValores.Parameters.Add("@prov_nombre", SqlDbType.VarChar)
                            SCMValores.Parameters.Add("@precio", SqlDbType.Money)
                            SCMValores.Parameters.Add("@id_divisa", SqlDbType.Int)
                            SCMValores.Parameters.Add("@tipo_cambio", SqlDbType.Money)
                            SCMValores.Parameters.Add("@fecha_ini", SqlDbType.DateTime)
                            SCMValores.Parameters.Add("@fecha_fin", SqlDbType.DateTime)
                            SCMValores.Parameters.Add("@id_cond_pago", SqlDbType.Int)
                            SCMValores.Parameters.Add("@nombre_archivo", SqlDbType.VarChar)
                            'Cotización 1
                            If fuPDF1.PostedFile.ContentLength <= lMaxFileSize Then
                                SCMValores.Parameters("@no_cotizacion").Value = 1
                                If .ddlProveedor1.Items.Count = 0 Then
                                    SCMValores.Parameters("@id_proveedor").Value = -1
                                    SCMValores.Parameters("@prov_codigo").Value = ""
                                    SCMValores.Parameters("@prov_nombre").Value = .txtProveedor1.Text.Trim.ToUpper
                                Else
                                    SCMValores.Parameters("@id_proveedor").Value = .ddlProveedor1.SelectedValue
                                    SCMTemp.CommandText = ""
                                    SCMTemp.Parameters.Clear()
                                    SCMTemp.CommandText = "select codigo_unico " + _
                                                          "from bd_SiSAC.dbo.cg_proveedor " + _
                                                          "where id_proveedor = @id_proveedor "
                                    SCMTemp.Parameters.AddWithValue("@id_proveedor", .ddlProveedor1.SelectedValue)
                                    ConexionBD.Open()
                                    SCMValores.Parameters("@prov_codigo").Value = SCMTemp.ExecuteScalar
                                    ConexionBD.Close()
                                    SCMTemp.CommandText = "select nombre " + _
                                                          "from bd_SiSAC.dbo.cg_proveedor " + _
                                                          "where id_proveedor = @id_proveedor "
                                    ConexionBD.Open()
                                    SCMValores.Parameters("@prov_nombre").Value = SCMTemp.ExecuteScalar
                                    ConexionBD.Close()
                                End If
                                SCMValores.Parameters("@precio").Value = .wceImporte1.Value
                                SCMValores.Parameters("@id_divisa").Value = .ddlDivisa1.SelectedValue
                                SCMValores.Parameters("@tipo_cambio").Value = tipoCambio1
                                If .wdpFechaIni1.Text = "" Then
                                    SCMValores.Parameters("@fecha_ini").Value = DBNull.Value
                                Else
                                    SCMValores.Parameters("@fecha_ini").Value = .wdpFechaIni1.Date
                                End If
                                If .wdpFechaFin1.Text = "" Then
                                    SCMValores.Parameters("@fecha_fin").Value = DBNull.Value
                                Else
                                    SCMValores.Parameters("@fecha_fin").Value = .wdpFechaFin1.Date
                                End If
                                SCMValores.Parameters("@id_cond_pago").Value = .ddlCondP1.SelectedValue
                                SCMValores.Parameters("@nombre_archivo").Value = sFileName1
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()
                                'Obtener el Id del archivo
                                SCMValores.CommandText = "select max(id_dt_cotizacion) from dt_cotizacion where (id_ms_factura = @id_ms_factura) and (no_cotizacion = 1)"
                                ConexionBD.Open()
                                idArchivo = SCMValores.ExecuteScalar
                                ConexionBD.Close()
                                'Se agrega el Id al nombre del archivo
                                sFileName1 = idArchivo.ToString + "-" + sFileName1
                                'Almacenar el archivo en la ruta especificada
                                fuPDF1.PostedFile.SaveAs(sFileDir + sFileName1)
                            Else
                                'Rechazar el archivo
                                .litError.Text = "El archivo excede el límite de 10 MB"
                            End If

                            'Cotización 2
                            If fuPDF2.PostedFile.ContentLength <= lMaxFileSize Then
                                SCMValores.CommandText = "insert into dt_cotizacion( id_ms_factura,  no_cotizacion,  id_proveedor,  prov_codigo,  prov_nombre,  precio,  id_divisa,  tipo_cambio,  fecha_ini,  fecha_fin,  id_cond_pago,  nombre_archivo) " + _
                                                         "                   values(@id_ms_factura, @no_cotizacion, @id_proveedor, @prov_codigo, @prov_nombre, @precio, @id_divisa, @tipo_cambio, @fecha_ini, @fecha_fin, @id_cond_pago, @nombre_archivo)"
                                SCMValores.Parameters("@no_cotizacion").Value = 2
                                If .ddlProveedor2.Items.Count = 0 Then
                                    SCMValores.Parameters("@id_proveedor").Value = -1
                                    SCMValores.Parameters("@prov_codigo").Value = ""
                                    SCMValores.Parameters("@prov_nombre").Value = .txtProveedor2.Text.Trim.ToUpper
                                Else
                                    SCMValores.Parameters("@id_proveedor").Value = .ddlProveedor2.SelectedValue
                                    SCMTemp.CommandText = ""
                                    SCMTemp.Parameters.Clear()
                                    SCMTemp.CommandText = "select codigo_unico " + _
                                                          "from bd_SiSAC.dbo.cg_proveedor " + _
                                                          "where id_proveedor = @id_proveedor "
                                    SCMTemp.Parameters.AddWithValue("@id_proveedor", .ddlProveedor2.SelectedValue)
                                    ConexionBD.Open()
                                    SCMValores.Parameters("@prov_codigo").Value = SCMTemp.ExecuteScalar
                                    ConexionBD.Close()
                                    SCMTemp.CommandText = "select nombre " + _
                                                          "from bd_SiSAC.dbo.cg_proveedor " + _
                                                          "where id_proveedor = @id_proveedor "
                                    ConexionBD.Open()
                                    SCMValores.Parameters("@prov_nombre").Value = SCMTemp.ExecuteScalar
                                    ConexionBD.Close()
                                End If
                                SCMValores.Parameters("@precio").Value = .wceImporte2.Value
                                SCMValores.Parameters("@id_divisa").Value = .ddlDivisa2.SelectedValue
                                SCMValores.Parameters("@tipo_cambio").Value = tipoCambio2
                                If .wdpFechaIni2.Text = "" Then
                                    SCMValores.Parameters("@fecha_ini").Value = DBNull.Value
                                Else
                                    SCMValores.Parameters("@fecha_ini").Value = .wdpFechaIni2.Date
                                End If
                                If .wdpFechaFin2.Text = "" Then
                                    SCMValores.Parameters("@fecha_fin").Value = DBNull.Value
                                Else
                                    SCMValores.Parameters("@fecha_fin").Value = .wdpFechaFin2.Date
                                End If
                                SCMValores.Parameters("@id_cond_pago").Value = .ddlCondP2.SelectedValue
                                SCMValores.Parameters("@nombre_archivo").Value = sFileName2
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()
                                'Obtener el Id del archivo
                                SCMValores.CommandText = "select max(id_dt_cotizacion) from dt_cotizacion where (id_ms_factura = @id_ms_factura) and (no_cotizacion = 2)"
                                ConexionBD.Open()
                                idArchivo = SCMValores.ExecuteScalar
                                ConexionBD.Close()
                                'Se agrega el Id al nombre del archivo
                                sFileName2 = idArchivo.ToString + "-" + sFileName2
                                'Almacenar el archivo en la ruta especificada
                                fuPDF2.PostedFile.SaveAs(sFileDir + sFileName2)
                            Else
                                'Rechazar el archivo
                                .litError.Text = "El archivo excede el límite de 10 MB"
                            End If

                            'Cotización 3
                            If fuPDF3.PostedFile.ContentLength <= lMaxFileSize Then
                                SCMValores.CommandText = "insert into dt_cotizacion( id_ms_factura,  no_cotizacion,  id_proveedor,  prov_codigo,  prov_nombre,  precio,  id_divisa,  tipo_cambio,  fecha_ini,  fecha_fin,  id_cond_pago,  nombre_archivo) " + _
                                                         "                   values(@id_ms_factura, @no_cotizacion, @id_proveedor, @prov_codigo, @prov_nombre, @precio, @id_divisa, @tipo_cambio, @fecha_ini, @fecha_fin, @id_cond_pago, @nombre_archivo)"
                                SCMValores.Parameters("@no_cotizacion").Value = 3
                                If .ddlProveedor3.Items.Count = 0 Then
                                    SCMValores.Parameters("@id_proveedor").Value = -1
                                    SCMValores.Parameters("@prov_codigo").Value = ""
                                    SCMValores.Parameters("@prov_nombre").Value = .txtProveedor3.Text.Trim.ToUpper
                                Else
                                    SCMValores.Parameters("@id_proveedor").Value = .ddlProveedor3.SelectedValue
                                    SCMTemp.CommandText = ""
                                    SCMTemp.Parameters.Clear()
                                    SCMTemp.CommandText = "select codigo_unico " + _
                                                          "from bd_SiSAC.dbo.cg_proveedor " + _
                                                          "where id_proveedor = @id_proveedor "
                                    SCMTemp.Parameters.AddWithValue("@id_proveedor", .ddlProveedor3.SelectedValue)
                                    ConexionBD.Open()
                                    SCMValores.Parameters("@prov_codigo").Value = SCMTemp.ExecuteScalar
                                    ConexionBD.Close()
                                    SCMTemp.CommandText = "select nombre " + _
                                                          "from bd_SiSAC.dbo.cg_proveedor " + _
                                                          "where id_proveedor = @id_proveedor "
                                    ConexionBD.Open()
                                    SCMValores.Parameters("@prov_nombre").Value = SCMTemp.ExecuteScalar
                                    ConexionBD.Close()
                                End If
                                SCMValores.Parameters("@precio").Value = .wceImporte3.Value
                                SCMValores.Parameters("@id_divisa").Value = .ddlDivisa3.SelectedValue
                                SCMValores.Parameters("@tipo_cambio").Value = tipoCambio3
                                If .wdpFechaIni3.Text = "" Then
                                    SCMValores.Parameters("@fecha_ini").Value = DBNull.Value
                                Else
                                    SCMValores.Parameters("@fecha_ini").Value = .wdpFechaIni3.Date
                                End If
                                If .wdpFechaFin3.Text = "" Then
                                    SCMValores.Parameters("@fecha_fin").Value = DBNull.Value
                                Else
                                    SCMValores.Parameters("@fecha_fin").Value = .wdpFechaFin3.Date
                                End If
                                SCMValores.Parameters("@id_cond_pago").Value = .ddlCondP3.SelectedValue
                                SCMValores.Parameters("@nombre_archivo").Value = sFileName3
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()
                                'Obtener el Id del archivo
                                SCMValores.CommandText = "select max(id_dt_cotizacion) from dt_cotizacion where (id_ms_factura = @id_ms_factura) and (no_cotizacion = 3)"
                                ConexionBD.Open()
                                idArchivo = SCMValores.ExecuteScalar
                                ConexionBD.Close()
                                'Se agrega el Id al nombre del archivo
                                sFileName3 = idArchivo.ToString + "-" + sFileName3
                                'Almacenar el archivo en la ruta especificada
                                fuPDF3.PostedFile.SaveAs(sFileDir + sFileName3)
                            Else
                                'Rechazar el archivo
                                .litError.Text = "El archivo excede el límite de 10 MB"
                            End If

                            'Actualizar datos de la Solicitud
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "update ms_factura set id_usr_cotiza = @id_usr_cotiza, fecha_cotiza = @fecha_cotiza, comentario_cotiza = @comentario_cotiza, status = 'CotI', proveedor_selec = @proveedor_selec, cotizacion_selec = @cotizacion_selec where id_ms_factura = @id_ms_factura "
                            SCMValores.Parameters.AddWithValue("@id_usr_cotiza", Val(._txtIdUsuario.Text))
                            SCMValores.Parameters.AddWithValue("@fecha_cotiza", fecha)
                            SCMValores.Parameters.AddWithValue("@comentario_cotiza", .txtComentario.Text.Trim)
                            SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                            SCMValores.Parameters.AddWithValue("@cotizacion_selec", .rblProvSel.SelectedValue)
                            Select Case .rblProvSel.SelectedValue
                                Case 1
                                    SCMValores.Parameters.AddWithValue("@proveedor_selec", .ddlProveedor1.SelectedValue)
                                Case 2
                                    SCMValores.Parameters.AddWithValue("@proveedor_selec", .ddlProveedor2.SelectedValue)
                                Case 3
                                    SCMValores.Parameters.AddWithValue("@proveedor_selec", .ddlProveedor3.SelectedValue)
                            End Select
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            Dim idActividad As Integer
                            If ._txtFinanzas.Text = "S" Then
                                idActividad = 47
                            Else
                                If ._txtValPresup.Text = "S" Then
                                    idActividad = 84
                                Else
                                    If .lblContratoNAV.Text = "Sí" Then
                                        idActividad = 54
                                    Else
                                        idActividad = 49
                                    End If
                                End If
                            End If

                            'Actualizar Instancia
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                            SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                            SCMValores.Parameters.AddWithValue("@id_actividad", idActividad)
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            'Registrar en Histórico
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                                     "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                            SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                            SCMValores.Parameters.AddWithValue("@id_actividad", idActividad)
                            SCMValores.Parameters.AddWithValue("@fecha", fecha)
                            SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            If ._txtFinanzas.Text = "S" Then
                                'Envío de Correo al Autorizador de Finanzas
                                Dim Mensaje As New System.Net.Mail.MailMessage()
                                Dim destinatario As String = ""
                                'Obtener Correo
                                If .lblEmpresa.Text = "COPE" Or .lblEmpresa.Text = "DICOMEX" Or .lblEmpresa.Text = "DIBIESE" Then
                                    SCMValores.CommandText = "select valor from cg_parametros where parametro = 'aut_finanzasDCM' "
                                Else
                                    SCMValores.CommandText = "select valor from cg_parametros where parametro = 'aut_finanzas' "
                                End If
                                ConexionBD.Open()
                                destinatario = SCMValores.ExecuteScalar()
                                ConexionBD.Close()

                                Mensaje.[To].Add(destinatario)
                                Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                                Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                                Mensaje.Subject = "ProcAd - Solicitud de Factura No. " + .lblFolio.Text + " por Autorizar"
                                Dim texto As String
                                texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                                        "Se ingresó la solicitud número <b>" + .lblFolio.Text + _
                                        "</b> por parte de <b>" + .lblSolicitante.Text + _
                                        "</b><br><br>Favor de determinar si procede </span>"
                                Mensaje.Body = texto
                                Mensaje.IsBodyHtml = True
                                Mensaje.Priority = MailPriority.Normal

                                Dim Servidor As New SmtpClient()
                                Servidor.Host = "10.10.10.30"
                                Servidor.Port = 587
                                Servidor.EnableSsl = False
                                Servidor.UseDefaultCredentials = False
                                Servidor.Credentials = New System.Net.NetworkCredential("nprocad", "mc8HLB8lPe78")

                                Try
                                    Servidor.Send(Mensaje)
                                Catch ex As System.Net.Mail.SmtpException
                                    .litError.Text = ex.ToString
                                End Try
                            Else
                                If ._txtValPresup.Text = "S" Then
                                    'Envío de Correo al Validador de Presupuesto
                                    Dim Mensaje As New System.Net.Mail.MailMessage()
                                    Dim destinatario As String = ""
                                    'Obtener el Correos del Validador de Presupuesto
                                    SCMValores.CommandText = "select valor from cg_parametros where parametro = 'val_presupuesto' "
                                    ConexionBD.Open()
                                    destinatario = SCMValores.ExecuteScalar()
                                    ConexionBD.Close()

                                    Mensaje.[To].Add(destinatario)
                                    Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                                    Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                                    Mensaje.Subject = "ProcAd - Solicitud de Factura No. " + .lblFolio.Text + " por Validar Presupuesto"
                                    Dim texto As String
                                    texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                                            "Se autorizó la solicitud número <b>" + .lblFolio.Text + _
                                            "</b> de <b>" + .lblSolicitante.Text + _
                                            "</b><br><br>Favor de determinar si procede </span>"
                                    Mensaje.Body = texto
                                    Mensaje.IsBodyHtml = True
                                    Mensaje.Priority = MailPriority.Normal

                                    Dim Servidor As New SmtpClient()
                                    Servidor.Host = "10.10.10.30"
                                    Servidor.Port = 587
                                    Servidor.EnableSsl = False
                                    Servidor.UseDefaultCredentials = False
                                    Servidor.Credentials = New System.Net.NetworkCredential("nprocad", "mc8HLB8lPe78")

                                    Try
                                        Servidor.Send(Mensaje)
                                    Catch ex As System.Net.Mail.SmtpException
                                        .litError.Text = ex.ToString
                                    End Try
                                Else
                                    If .lblContratoNAV.Text = "Sí" Then
                                        'Envío de Correo al Solicitante para Complementación de Contrato
                                        Dim Mensaje As New System.Net.Mail.MailMessage()
                                        Dim destinatario As String = ""
                                        'Obtener el Correo del Solicitante
                                        SCMValores.CommandText = "select cgEmpl.correo " + _
                                                                 "from ms_factura " + _
                                                                 "  left join cg_usuario on ms_factura.id_usr_solicita = cg_usuario.id_usuario " + _
                                                                 "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                                 "where id_ms_factura = @id_ms_factura "
                                        SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                                        ConexionBD.Open()
                                        destinatario = SCMValores.ExecuteScalar()
                                        ConexionBD.Close()

                                        Mensaje.[To].Add(destinatario)
                                        Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                                        Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                                        Mensaje.Subject = "ProcAd - Solicitud de Factura No. " + .lblFolio.Text + " Autorizada"
                                        Dim texto As String
                                        texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                                                "Se autorizó la solicitud número <b>" + .lblFolio.Text + _
                                                "</b><br><br>Favor de Complementar los Datos para el Contrato </span>"
                                        Mensaje.Body = texto
                                        Mensaje.IsBodyHtml = True
                                        Mensaje.Priority = MailPriority.Normal

                                        Dim Servidor As New SmtpClient()
                                        Servidor.Host = "10.10.10.30"
                                        Servidor.Port = 587
                                        Servidor.EnableSsl = False
                                        Servidor.UseDefaultCredentials = False
                                        Servidor.Credentials = New System.Net.NetworkCredential("nprocad", "mc8HLB8lPe78")

                                        Try
                                            Servidor.Send(Mensaje)
                                        Catch ex As System.Net.Mail.SmtpException
                                            .litError.Text = ex.ToString
                                        End Try
                                    Else
                                        'Envío de Correo al Solicitante para Ingreso de Factura
                                        Dim Mensaje As New System.Net.Mail.MailMessage()
                                        Dim destinatario As String = ""
                                        'Obtener el Correo del Solicitante
                                        SCMValores.CommandText = "select cgEmpl.correo " + _
                                                                 "from ms_factura " + _
                                                                 "  left join cg_usuario on ms_factura.id_usr_solicita = cg_usuario.id_usuario " + _
                                                                 "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                                 "where id_ms_factura = @id_ms_factura "
                                        SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                                        ConexionBD.Open()
                                        destinatario = SCMValores.ExecuteScalar()
                                        ConexionBD.Close()

                                        Mensaje.[To].Add(destinatario)
                                        Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                                        Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                                        Mensaje.Subject = "ProcAd - Solicitud de Factura No. " + .lblFolio.Text + " Autorizada"
                                        Dim texto As String
                                        texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                                                "Se autorizó la solicitud número <b>" + .lblFolio.Text + _
                                                "</b><br><br>Favor de Ingresar la Factura Correspondiente </span>"
                                        Mensaje.Body = texto
                                        Mensaje.IsBodyHtml = True
                                        Mensaje.Priority = MailPriority.Normal

                                        Dim Servidor As New SmtpClient()
                                        Servidor.Host = "10.10.10.30"
                                        Servidor.Port = 587
                                        Servidor.EnableSsl = False
                                        Servidor.UseDefaultCredentials = False
                                        Servidor.Credentials = New System.Net.NetworkCredential("nprocad", "mc8HLB8lPe78")

                                        Try
                                            Servidor.Send(Mensaje)
                                        Catch ex As System.Net.Mail.SmtpException
                                            .litError.Text = ex.ToString
                                        End Try
                                    End If
                                End If
                            End If

                            .pnlInicio.Enabled = False

                        Catch exc As Exception    'En caso de error
                            'Eliminar el archivo en la base de datos
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "delete from dt_cotizacion where id_ms_factura = @id_ms_factura"
                            SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            .litError.Text = "Un Error ha ocurrido. Favor de intentarlo nuevamente"
                            DeleteFile(sFileDir + sFileName1)
                            DeleteFile(sFileDir + sFileName2)
                            DeleteFile(sFileDir + sFileName3)
                        End Try
                    Else
                        .litError.Text = "Favor de seleccionar los Archivos de las cotizaciones"
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnRechazar_Click(sender As Object, e As EventArgs) Handles btnRechazar.Click
        With Me
            Try
                .litError.Text = ""

                If .txtComentario.Text.Trim = "" Then
                    .litError.Text = "Favor de ingresar los comentarios correspondientes"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    Dim fecha As DateTime = Date.Now
                    While Val(._txtBan.Text) = 0
                        'Actualizar datos de la Solicitud
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_factura set id_usr_cotiza = @id_usr_cotiza, fecha_cotiza = @fecha_cotiza, comentario_cotiza = @comentario_cotiza, status = 'CorrSol' where id_ms_factura = @id_ms_factura "
                        SCMValores.Parameters.AddWithValue("@id_usr_cotiza", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@fecha_cotiza", fecha)
                        SCMValores.Parameters.AddWithValue("@comentario_cotiza", .txtComentario.Text.Trim)
                        SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        ._txtBan.Text = 1

                        'Actualizar Instancia
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 83)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Registrar en Histórico
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                                 "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 83)
                        SCMValores.Parameters.AddWithValue("@fecha", fecha)
                        SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Envío de Correo
                        Dim Mensaje As New System.Net.Mail.MailMessage()
                        Dim destinatario As String = ""
                        'Obtener el Correo del Solicitante
                        SCMValores.CommandText = "select cgEmpl.correo " + _
                                                 "from ms_factura " + _
                                                 "  left join cg_usuario on ms_factura.id_usr_solicita = cg_usuario.id_usuario " + _
                                                 "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                 "where id_ms_factura = @id_ms_factura "
                        SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        destinatario = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        Mensaje.[To].Add(destinatario)
                        Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                        Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                        Mensaje.Subject = "ProcAd - Solicitud de Factura No. " + .lblFolio.Text + " No Autorizada"
                        Dim texto As String
                        texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                                "La solicitud número <b>" + .lblFolio.Text + "</b> fue rechazada por Compras. <br>" + _
                                "Comentarios: <b>" + .txtComentario.Text.Trim + "</b><br>" + _
                                "Favor ingresar a la actividad <b>Corregir Solicitud</b> </span>"
                        Mensaje.Body = texto
                        Mensaje.IsBodyHtml = True
                        Mensaje.Priority = MailPriority.Normal

                        Dim Servidor As New SmtpClient()
                        Servidor.Host = "10.10.10.30"
                        Servidor.Port = 587
                        Servidor.EnableSsl = False
                        Servidor.UseDefaultCredentials = False
                        Servidor.Credentials = New System.Net.NetworkCredential("nprocad", "mc8HLB8lPe78")

                        Try
                            Servidor.Send(Mensaje)
                        Catch ex As System.Net.Mail.SmtpException
                            .litError.Text = ex.ToString
                        End Try

                        .pnlInicio.Enabled = False
                    End While
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class