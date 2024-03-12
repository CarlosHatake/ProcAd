Public Class _134
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess
#Region "Inicio"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtIdMsInst.Text = Session("idMsInst")
                        ._txtBan.Text = 0

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim query As String
                        Dim sdaEmpresa As New SqlDataAdapter
                        Dim dsEmpresa As New DataSet


                        query = " SELECT id_empresa, nombre " +
                                " FROM bd_Empleado.dbo.cg_empresa " +
                                " WHERE status = 'A' " +
                                " ORDER BY nombre "
                        sdaEmpresa.SelectCommand = New SqlCommand(query, ConexionBD)
                        .ddlEmpresa.DataSource = dsEmpresa
                        .ddlEmpresa.DataTextField = "nombre"
                        .ddlEmpresa.DataValueField = "id_empresa"
                        ConexionBD.Open()
                        sdaEmpresa.Fill(dsEmpresa)
                        .ddlEmpresa.DataBind()
                        ConexionBD.Close()
                        sdaEmpresa.Dispose()
                        dsEmpresa.Dispose()
                        .ddlEmpresa.SelectedIndex = -1

                        Dim sdaDivisa As New SqlDataAdapter
                        Dim dsDivisa As New DataSet
                        query = " SELECT id_divisa, divisa AS divisa " +
                                " FROM bd_SiSAC.dbo.cg_divisa " +
                                " WHERE status = 'A' ORDER BY divisa "
                        sdaDivisa.SelectCommand = New SqlCommand(query, ConexionBD)
                        .ddlDivisa.DataSource = dsDivisa
                        .ddlDivisa.DataTextField = "divisa"
                        .ddlDivisa.DataValueField = "id_divisa"
                        ConexionBD.Open()
                        sdaDivisa.Fill(dsDivisa)
                        .ddlDivisa.DataBind()
                        ConexionBD.Close()
                        sdaDivisa.Dispose()
                        dsDivisa.Dispose()
                        .ddlDivisa.SelectedIndex = -1

                        'Llena la lista de proveedores
                        llenarProv()

                        Dim sdaAutorizador As New SqlDataAdapter
                        Dim dsAutorizador As New DataSet
                        query = ""
                        query = " SELECT cgEmpl.id_empleado, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno AS autorizador " +
                                " FROM dt_autorizador " +
                                " LEFT JOIN cg_usuario usr ON dt_autorizador.id_autorizador = usr.id_usuario " +
                                " LEFT JOIN bd_Empleado.dbo.cg_empleado cgEmpl ON usr.id_empleado = cgEmpl.id_empleado " +
                                " WHERE dt_autorizador .id_usuario = @id_usuario " +
                                " AND usr.status = 'A' " +
                                " ORDER BY aut_dir, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno "
                        sdaAutorizador.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaAutorizador.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))

                        .ddlAutorizador.DataSource = dsAutorizador
                        .ddlAutorizador.DataTextField = "autorizador"
                        .ddlAutorizador.DataValueField = "id_empleado"
                        ConexionBD.Open()
                        sdaAutorizador.Fill(dsAutorizador)
                        .ddlAutorizador.DataBind()
                        ConexionBD.Close()
                        sdaAutorizador.Dispose()
                        dsAutorizador.Dispose()
                        .ddlAutorizador.SelectedIndex = -1


                    Else
                        Server.Transfer("Login.aspx")
                    End If

                Catch ex As Exception
                    .litError.Text = ex.ToString()

                End Try
            End With
        End If

    End Sub
#End Region

#Region "Funciones"
    Public Sub llenarProv()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaProv As New SqlDataAdapter
                Dim dsProv As New DataSet
                Dim query As String
                query = " SELECT DISTINCT(rfc) as rfc_emisor, id_proveedor, rfc + ' / ' + nombre AS proveedor " +
                        " FROM bd_SiSAC.dbo.cg_proveedor " +
                        " WHERE (rfc <> '' AND rfc IS NOT NULL) " +
                        " AND (rfc LIKE '%' + @valor+ '%' OR nombre LIKE '%' + @valor+ '%') "

                sdaProv.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaProv.SelectCommand.Parameters.AddWithValue("@valor", .txtProveedor.Text.Trim)
                .ddlProveedor.DataSource = dsProv
                .ddlProveedor.DataTextField = "proveedor"
                .ddlProveedor.DataValueField = "id_proveedor"
                ConexionBD.Open()
                sdaProv.Fill(dsProv)
                .ddlProveedor.DataBind()
                ConexionBD.Close()
                sdaProv.Dispose()
                dsProv.Dispose()
                '.ddlProveedor.SelectedIndex = -1

                If ddlProveedor.Items.Count > 0 Then
                    Dim sdaSolicitud As New SqlDataAdapter
                    Dim dsSolicitud As New DataSet
                    query = ""

                    query = "SELECT DISTINCT(rfc) as rfc_emisor FROM bd_SiSAC.dbo.cg_proveedor WHERE (rfc <> '' AND rfc IS NOT NULL) and id_proveedor = @id_proveedor"
                    sdaSolicitud.SelectCommand = New SqlCommand(query, ConexionBD)
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_proveedor", ddlProveedor.SelectedItem.Value)
                    ConexionBD.Open()
                    sdaSolicitud.Fill(dsSolicitud)
                    ConexionBD.Close()
                    ._txtRFCProveedor.Text = dsSolicitud.Tables(0).Rows(0).Item("rfc_emisor").ToString()

                    'llenarPedidosCompra(._txtRFCProveedor.Text)
                End If



            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarPedidosCompra(ByVal rfc)

        With Me
            Try
                .litError.Text = ""
                Dim ConexionBDNAV As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBDNAV.ConnectionString = accessDB.conBD("NAV")
                Dim sdaPC As New SqlDataAdapter
                Dim dsPC As New DataSet
                Dim empresa As String = .ddlEmpresa.SelectedItem.Text

                Dim query As String = " SELECT  Pc.No_ as id, Pc.No_ +' / '+  Cast(Convert(decimal(20,2) , SUM(PcL.[Amount Including VAT])) as varchar) as [Compra] " +
                                      " FROM [" + empresa + "$Purchase Header] AS Pc" +
                                      " LEFT JOIN [" + empresa + "$Vendor] As Pv On  Pv.No_ =  Pc.[Pay-to Vendor No_]" +
                                      " INNER JOIN [" + empresa + "$Purchase Line] As PcL On PcL.[Document No_] = Pc.No_ " +
                                      " WHERE DATEPART(YEAR,Pc.[Order Date]) = '2024' " +
                                      " AND [Last Receiving No_]='' " +
                                      " AND SUBSTRING(Pc.No_,1,2) ='PC' " +
                                      " AND Pv.[RFC No_] = @RFCProveedor " +
                                      " group by  Pc.No_ " +
                                      " ORDER BY Pc.No_ DESC "

                sdaPC.SelectCommand = New SqlCommand(query, ConexionBDNAV)
                sdaPC.SelectCommand.Parameters.AddWithValue("@RFCProveedor", rfc)
                .ddlPedidosCompra.DataSource = dsPC
                .ddlPedidosCompra.DataTextField = "Compra"
                .ddlPedidosCompra.DataValueField = "id"
                ConexionBDNAV.Open()
                sdaPC.Fill(dsPC)
                .ddlPedidosCompra.DataBind()
                ConexionBDNAV.Close()
                sdaPC.Dispose()
                dsPC.Dispose()
                .ddlPedidosCompra.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub


    Private Sub DeleteFile(ByVal strFileName As String)
        If strFileName.Trim().Length > 0 Then
            Dim fi As New FileInfo(strFileName)
            If (fi.Exists) Then    'Si existe el archivo, eliminarlo
                fi.Delete()
            End If
        End If
    End Sub

    'Public Sub actualizarAdjuntos()
    '    With Me
    '        Try
    '            .litError.Text = ""
    '            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
    '            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
    '            Dim sdaArchivos As New SqlDataAdapter
    '            Dim dsArchivos As New DataSet
    '            .gvEvidencias.DataSource = dsArchivos
    '            'Adjuntos
    '            sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " +
    '                                                       "     , 'http://148.223.153.43/ProcAd - Adjuntos AntProv/' + cast(id_dt_archivo_adj_anticipo as varchar(20)) + '-' + nombre as path " +
    '                                                       "from dt_archivo_adj_anticipo " +
    '                                                       "where id_ms_anticipo_proveedor = -1 " +
    '                                                        "  and id_usuario = @idUsuario ", ConexionBD)
    '            sdaArchivos.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
    '            ConexionBD.Open()
    '            sdaArchivos.Fill(dsArchivos)
    '            .gvEvidencias.DataBind()
    '            ConexionBD.Close()
    '            sdaArchivos.Dispose()
    '            dsArchivos.Dispose()
    '            .gvEvidencias.SelectedIndex = -1
    '            .upAdjuntos.Update()
    '        Catch ex As Exception
    '            .litError.Text = ex.ToString
    '        End Try
    '    End With
    'End Sub

#End Region

#Region "Botones"

    Protected Sub ibtnBuscarProv_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBuscarProv.Click
        With Me
            llenarProv()
            .upProveedor.Update()
            llenarPedidosCompra(.txtProveedor.Text)

        End With
    End Sub




    'Protected Sub btnAgregarAdj_Click(sender As Object, e As EventArgs) Handles btnAgregarAdj.Click
    '    With Me
    '        Try
    '            .litError.Text = ""
    '            .lblMessage.Text = ""


    '            ' '' Ruta Local
    '            'Dim sFileDir As String = "C:/ProcAd - Adjuntos AntProv/" 'Ruta en que se almacenará el archivo
    '            ' Ruta en Atenea
    '            Dim sFileDir As String = "D:\ProcAd - Adjuntos IngFact\" 'Ruta en que se almacenará el archivo
    '            Dim lMaxFileSize As Long = 10485760 'Equivalente a 10 Megabytes

    '            'Verificar que el archivo ha sido seleccionado y es un archivo válido
    '            If (Not fuAdjunto.PostedFile Is Nothing) And (fuAdjunto.PostedFile.ContentLength > 0) Then
    '                'Determinar el nombre original del archivo
    '                Dim sFileName As String = System.IO.Path.GetFileName(fuAdjunto.PostedFile.FileName)
    '                Dim idArchivo As Integer 'Index correspondiente al archivo
    '                Dim fecha As DateTime = Date.Now
    '                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
    '                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
    '                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
    '                SCMValores.Connection = ConexionBD
    '                Try
    '                    If fuAdjunto.PostedFile.ContentLength <= lMaxFileSize Then
    '                        'Registrar el archivo en la base de datos
    '                        SCMValores.CommandText = ""
    '                        SCMValores.Parameters.Clear()
    '                        SCMValores.CommandText = "INSERT INTO dt_archivo_adj_anticipo (id_ms_anticipo_proveedor, id_usuario, nombre, fecha ) values(-1, @id_usuario, @nombre, @fecha)"
    '                        SCMValores.Parameters.AddWithValue("@id_usuario", Val(Me._txtIdUsuario.Text))
    '                        SCMValores.Parameters.AddWithValue("@nombre", sFileName)
    '                        SCMValores.Parameters.AddWithValue("@fecha", fecha)
    '                        ConexionBD.Open()
    '                        SCMValores.ExecuteNonQuery()
    '                        ConexionBD.Close()
    '                        'Obtener el Id del archivo
    '                        SCMValores.CommandText = "select max(id_dt_archivo_adj_anticipo) from dt_archivo_adj_anticipo where (id_ms_anticipo_proveedor = -1) and (fecha = @fecha)"
    '                        ConexionBD.Open()
    '                        idArchivo = SCMValores.ExecuteScalar
    '                        ConexionBD.Close()
    '                        'Se agrega el Id al nombre del archivo
    '                        sFileName = idArchivo.ToString + "-" + sFileName
    '                        'Almacenar el archivo en la ruta especificada
    '                        fuAdjunto.PostedFile.SaveAs(sFileDir + sFileName)
    '                        'lblMessage.Visible = True
    '                        'lblMessage.Text = "El archivo: " + sFileName + " se ha adjuntado exitosamente"

    '                        actualizarAdjuntos()

    '                    Else
    '                        'Rechazar el archivo
    '                        lblMessage.Visible = True
    '                        lblMessage.Text = "El archivo excede el límite de 10 MB"
    '                    End If
    '                Catch exc As Exception    'En caso de error
    '                    'Eliminar el archivo en la base de datos
    '                    SCMValores.CommandText = ""
    '                    SCMValores.Parameters.Clear()
    '                    SCMValores.CommandText = "delete from dt_archivo_adj_anticipo where id_dt_archivo_adj_anticipo = @idArchivo"
    '                    SCMValores.Parameters.AddWithValue("@idArchivo", idArchivo)
    '                    ConexionBD.Open()
    '                    SCMValores.ExecuteNonQuery()
    '                    ConexionBD.Close()

    '                    lblMessage.Visible = True
    '                    lblMessage.Text = lblMessage.Text + ". Un Error ha ocurrido. Favor de intentarlo nuevamente"
    '                    DeleteFile(sFileDir + sFileName)
    '                End Try
    '            Else
    '                lblMessage.Visible = True
    '                lblMessage.Text = "Favor de seleccionar un Archivo"
    '            End If
    '        Catch ex As Exception
    '            .litError.Text = ex.ToString
    '        End Try
    '    End With
    'End Sub
    Protected Sub btnAEvidencia_Click(sender As Object, e As EventArgs) Handles btnAEvidencia.Click

        Try
            litError.Text = ""

            If System.IO.Path.GetFileName(fuEvidencia.PostedFile.FileName) = "" Then
                litError.Text = "No hay ningun archivo adjunto"
            Else
                gvEvidencias.Columns(2).Visible = True
                gvEvidencias.Columns(3).Visible = True

                'Agregar evidencia'
                Dim rutaArchivo As String = "Adjuntos AntProv\" 'Ruta en que se almacenará el archivo
                Dim sFileNameAr As String = System.IO.Path.GetFileName(fuEvidencia.PostedFile.FileName)
                'Guarda archivo'
                fuEvidencia.PostedFile.SaveAs(Server.MapPath("Adjuntos AntProv\" + _txtIdUsuario.Text.ToString + "-" + sFileNameAr))

                If gvEvidencias.Rows.Count <> 0 Then
                    Dim tabla As DataTable = New DataTable
                    tabla.Columns.Add("nombre", GetType(String))
                    tabla.Columns.Add("ruta", GetType(String))
                    tabla.Columns.Add("nombre_archivo", GetType(String))
                    tabla.Columns.Add("ruta_archivo", GetType(String))

                    For index As Integer = 0 To gvEvidencias.Rows.Count - 1
                        Dim Row1 As DataRow = tabla.NewRow
                        Row1("ruta") = gvEvidencias.Rows(index).Cells(3).Text
                        Row1("nombre") = gvEvidencias.Rows(index).Cells(2).Text
                        Row1("nombre_archivo") = gvEvidencias.Rows(index).Cells(2).Text
                        Row1("ruta_archivo") = gvEvidencias.Rows(index).Cells(3).Text
                        tabla.Rows.Add(Row1)
                    Next

                    Dim Row As DataRow = tabla.NewRow
                    Row("nombre") = sFileNameAr
                    Row("ruta") = "http://148.223.153.43/ProcAd/Adjuntos AntProv/"
                    Row("nombre_archivo") = sFileNameAr
                    Row("ruta_archivo") = "http://148.223.153.43/ProcAd/Adjuntos AntProv/"
                    tabla.Rows.Add(Row)
                    gvEvidencias.DataSource = tabla
                    gvEvidencias.DataBind()


                Else
                    Dim tabla As DataTable = New DataTable
                    tabla.Columns.Add("nombre", GetType(String))
                    tabla.Columns.Add("ruta", GetType(String))
                    tabla.Columns.Add("nombre_archivo", GetType(String))
                    tabla.Columns.Add("ruta_archivo", GetType(String))

                    Dim Row1 As DataRow = tabla.NewRow
                    Row1("nombre") = sFileNameAr
                    Row1("ruta") = "http://148.223.153.43/ProcAd/Adjuntos AntProv/"
                    Row1("nombre_archivo") = sFileNameAr
                    Row1("ruta_archivo") = "http://148.223.153.43/ProcAd/Adjuntos AntProv/"
                    tabla.Rows.Add(Row1)

                    gvEvidencias.DataSource = tabla
                    gvEvidencias.DataBind()
                End If

                gvEvidencias.Columns(2).Visible = False
                gvEvidencias.Columns(3).Visible = False
            End If

        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvEvidencias_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvEvidencias.SelectedIndexChanged
        Try
            litError.Text = ""
            gvEvidencias.Columns(2).Visible = True
            gvEvidencias.Columns(3).Visible = True

            Dim tabla As DataTable = New DataTable
            tabla.Columns.Add("nombre", GetType(String))
            tabla.Columns.Add("ruta", GetType(String))
            tabla.Columns.Add("nombre_archivo", GetType(String))
            tabla.Columns.Add("ruta_archivo", GetType(String))

            For index As Integer = 0 To gvEvidencias.Rows.Count - 1

                If gvEvidencias.SelectedIndex = index Then
                    My.Computer.FileSystem.DeleteFile(Server.MapPath("Adjuntos AntProv\" + _txtIdUsuario.Text.ToString() + "-" + gvEvidencias.Rows(index).Cells(2).Text.ToString))
                Else
                    Dim Row1 As DataRow = tabla.NewRow
                    Row1("ruta") = gvEvidencias.Rows(index).Cells(3).Text
                    Row1("nombre") = gvEvidencias.Rows(index).Cells(2).Text
                    Row1("nombre_archivo") = gvEvidencias.Rows(index).Cells(2).Text
                    Row1("ruta_archivo") = gvEvidencias.Rows(index).Cells(3).Text
                    tabla.Rows.Add(Row1)
                End If
            Next
            gvEvidencias.DataSource = tabla
            gvEvidencias.DataBind()
            gvEvidencias.Columns(2).Visible = False
            gvEvidencias.Columns(3).Visible = False
            gvEvidencias.SelectedIndex = -1
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        With Me
            Try
                .litError.Text = ""
                Dim ban As Integer

                If .txtJustificacion.Text.Trim = "" Then
                    .litError.Text = "Ingrese la justificación correspondiente"
                    ban = 1
                End If

                If .wceImporte.Text.Trim = "" Then
                    If ban = 1 Then
                        .litError.Text = .litError.Text + "; "
                    Else
                        ban = 1
                    End If
                    .litError.Text = .litError.Text + "Ingrese el importe requerido"
                End If

                If .cbTipoAnticipo.SelectedIndex = -1 Then
                    If ban = 1 Then
                        .litError.Text = .litError.Text + "; "
                    Else
                        ban = 1
                    End If
                    .litError.Text = .litError.Text + "Seleccione el tipo de anticipo"
                End If

                'Escenario anticipo sin pedido de compra
                If .cbTipoAnticipo.SelectedIndex = 0 And .ddlPedidosCompra.SelectedIndex = 0 Then

                    If .gvEvidencias.Rows.Count = 0 Then
                        If ban = 1 Then
                            .litError.Text = .litError.Text + "; "
                        Else
                            ban = 1
                        End If
                        .litError.Text = .litError.Text + "Debe existir al menos un Adjunto"
                    End If

                End If

                'Escenario pago anticipado sin pedido de compra
                If .cbTipoAnticipo.SelectedIndex = 1 And .ddlPedidosCompra.SelectedIndex = 0 Then
                    If .gvEvidencias.Rows.Count = 0 Then
                        If ban = 1 Then
                            .litError.Text = .litError.Text + "; "
                        Else
                            ban = 1
                        End If
                        .litError.Text = .litError.Text + "Debe existir al menos un Adjunto"
                    End If
                End If

                'Escenario pago anticipado con pedido de compra
                If .cbTipoAnticipo.SelectedIndex = 1 And .ddlPedidosCompra.SelectedIndex > 0 Then
                    If .gvEvidencias.Rows.Count = 0 Then
                        If ban = 1 Then
                            .litError.Text = .litError.Text + "; "
                        Else
                            ban = 1
                        End If
                        .litError.Text = .litError.Text + "Debe existir al menos un Adjunto"
                    End If
                End If

                'Escenario pago anticipado agente aduanal
                If .cbTipoAnticipo.SelectedIndex = 2 Then
                    If .gvEvidencias.Rows.Count = 0 Then
                        If ban = 1 Then
                            .litError.Text = .litError.Text + "; "
                        Else
                            ban = 1
                        End If
                        .litError.Text = .litError.Text + "Debe existir al menos un Adjunto"
                    End If
                End If

                If ban = 0 Then
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD

                    Dim fecha As DateTime
                    fecha = Date.Now
                    'Insertar Solicitud
                    Dim sdaSolicitud As New SqlDataAdapter
                    Dim dsSolicitud As New DataSet
                    Dim query As String

                    query = "EXEC SP_I_ms_anticipo_proveedor @id_usr_solicita, @fecha_solicita, @id_empresa, @empresa, @id_empleado_autoriza, @autorizador, @id_proveedor, @proveedor, @tipo_anticipo, @importe_requerido, @divisa, @justificacion"
                    sdaSolicitud.SelectCommand = New SqlCommand(query, ConexionBD)
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_usr_solicita", Val(._txtIdUsuario.Text))
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@fecha_solicita", fecha)
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_empresa", .ddlEmpresa.SelectedItem.Value)
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedItem.Text)
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_empleado_autoriza", .ddlAutorizador.SelectedItem.Value)
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@autorizador", .ddlAutorizador.SelectedItem.Text)
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_proveedor", .ddlProveedor.SelectedItem.Value)
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@proveedor", .ddlProveedor.SelectedItem.Text)
                    If .cbTipoAnticipo.SelectedIndex = 0 Then
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@tipo_anticipo", "1")
                    End If

                    If .cbTipoAnticipo.SelectedIndex = 1 Then
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@tipo_anticipo", "2")
                    End If

                    If .cbTipoAnticipo.SelectedIndex = 2 Then
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@tipo_anticipo", "3")
                    End If

                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@importe_requerido", wceImporte.Value)
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@divisa", .ddlDivisa.SelectedItem.Text)
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@justificacion", .txtJustificacion.Text)
                    ConexionBD.Open()
                    sdaSolicitud.Fill(dsSolicitud)
                    ConexionBD.Close()

                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "select max(id_ms_anticipo_proveedor) from ms_anticipo_proveedor where id_usr_solicita = @id_usr_solicita and estatus not in ('Z') "
                    SCMValores.Parameters.AddWithValue("@id_usr_solicita", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    .lblFolio.Text = SCMValores.ExecuteScalar
                    ConexionBD.Close()
                    If Val(.lblFolio.Text) > 0 Then
                        ._txtBan.Text = 1
                    End If

                    If ._txtBan.Text = 1 Then

                        'Insertar el id_ms_anticipo_proveedor a la tabla dt_archivo_adj_anticipo

                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update dt_archivo_adj_anticipo set id_ms_anticipo_proveedor = @id_ms_anticipo_proveedor where id_ms_anticipo_proveedor = -1 and id_usuario = @id_usuario"
                        SCMValores.Parameters.AddWithValue("@id_ms_anticipo_proveedor", Val(.lblFolio.Text))
                        SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Insertar el id_ms_anticipo_proveedor a la tabla dt_pedidos_compra
                        If gvPedidosCompras.Visible = True Then
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "update dt_pedidos_compra set id_ms_anticipo_proveedor = @id_ms_anticipo_proveedor where id_ms_anticipo_proveedor = -1 and id_usuario = @id_usuario"
                            SCMValores.Parameters.AddWithValue("@id_ms_anticipo_proveedor", Val(.lblFolio.Text))
                            SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()
                        End If
                        'Insertar Instancia de Solicitud de Anticipo
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_instancia ( id_ms_sol,  tipo,  id_actividad) " +
                                                 "				    values (@id_ms_sol, @tipo, @id_actividad) "
                        SCMValores.Parameters.AddWithValue("@id_ms_sol", Val(.lblFolio.Text))
                        SCMValores.Parameters.AddWithValue("@tipo", "AP")

                        SCMValores.Parameters.AddWithValue("@id_actividad", 135)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()
                        'Obtener ID de la Instancia de Solicitud 
                        SCMValores.CommandText = "select max(id_ms_instancia) from ms_instancia where id_ms_sol = @id_ms_sol and tipo = 'AP' "
                        ConexionBD.Open()
                        ._txtIdMsInst.Text = SCMValores.ExecuteScalar
                        ConexionBD.Close()


                        'Insertar Históricos
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_historico ( id_ms_instancia,  id_actividad,  id_usr,  fecha) " +
                                                 "				    values (@id_ms_instancia, @id_actividad, @id_usr, @fecha) "
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 135)
                        SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@fecha", fecha)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        .pnlInicio.Enabled = False

                        'Solicitante 
                        Dim solicitante As String
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "SELECT empleado_solicita FROM ms_anticipo_proveedor WHERE id_ms_anticipo_proveedor = @id_ms_anticipo_proveedor "
                        SCMValores.Parameters.AddWithValue("@id_ms_anticipo_proveedor", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        solicitante = SCMValores.ExecuteScalar
                        ConexionBD.Close()


                        gvEvidencias.Columns(2).Visible = True
                        gvEvidencias.Columns(3).Visible = True

                        'Insertar Evidencias'
                        For index As Integer = 0 To gvEvidencias.Rows.Count - 1


                            My.Computer.FileSystem.RenameFile(Server.MapPath("Adjuntos AntProv\" + _txtIdUsuario.Text.ToString() + "-" + gvEvidencias.Rows(index).Cells(2).Text.ToString.Trim()), lblFolio.Text + "-" + gvEvidencias.Rows(index).Cells(2).Text)


                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "Insert into dt_archivo_adj_anticipo (id_ms_anticipo_proveedor, id_usuario, nombre) " +
                                                    "                     values (@id_ms_anticipo_proveedor,@id_usuario,@nombre) "
                            SCMValores.Parameters.AddWithValue("@id_ms_anticipo_proveedor", Val(lblFolio.Text))
                            SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                            SCMValores.Parameters.AddWithValue("@nombre", lblFolio.Text + "-" + gvEvidencias.Rows(index).Cells(2).Text)
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()


                        Next

                        gvEvidencias.Columns(2).Visible = False
                        gvEvidencias.Columns(3).Visible = False
                        gvEvidencias.Columns(4).Visible = False
                        ''Envío de Correo
                        'Dim Mensaje As New System.Net.Mail.MailMessage()
                        'Dim destinatario As String = ""
                        ''Obtener el Correo del Solicitante
                        'SCMValores.CommandText = "select cgEmpl.correo " +
                        '                                 " from ms_anticipo_proveedor " +
                        '                                 "  left join cg_usuario on ms_anticipo_proveedor.id_usr_autoriza = cg_usuario.id_usuario  " +
                        '                                 "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado  " +
                        '                                 "  where id_ms_anticipo_proveedor = @id_ms_anticipo_proveedor "
                        'SCMValores.Parameters.AddWithValue("@id_ms_anticipo_proveedor", Val(.lblFolio.Text))
                        'ConexionBD.Open()
                        'destinatario = SCMValores.ExecuteScalar()
                        'ConexionBD.Close()

                        'Mensaje.[To].Add(destinatario)
                        'Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                        'Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                        'Mensaje.Subject = "ProcAd - Solicitud de Anticipo de proveedor No. " + .lblFolio.Text + " por Autorizar"
                        'Dim texto As String
                        'texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                        '               "Buen día:
                        '         <br>
                        '         <br> Por medio de la presente le informamos que se generó la solicitud número <b>" + .lblFolio.Text +
                        '           "</b> por parte de <b>" + solicitante + "</b>
                        '        <br>
                        '        <br>Favor de validar si procede la Solicitud de Anticipo de proveedor"
                        'Mensaje.Body = texto
                        'Mensaje.IsBodyHtml = True
                        'Mensaje.Priority = MailPriority.Normal

                        'Dim Servidor As New SmtpClient()
                        'Servidor.Host = "10.10.10.30"
                        'Servidor.Port = 587
                        'Servidor.EnableSsl = False
                        'Servidor.UseDefaultCredentials = False
                        'Servidor.Credentials = New System.Net.NetworkCredential("nprocad", "mc8HLB8lPe78")

                        'Try
                        '    Servidor.Send(Mensaje)
                        'Catch ex As System.Net.Mail.SmtpException
                        '    litError.Text = ex.ToString
                        'End Try

                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString()

            End Try
        End With
    End Sub

    Protected Sub cbTipoAnticipo_SelectedIndexChanged(sender As Object, e As EventArgs)
        With Me
            Try
                If cbTipoAnticipo.SelectedIndex = 0 Then
                    .pnlAdjuntos.Visible = True
                    .lbl_PedidosCompra.Visible = False
                    .ddlPedidosCompra.Visible = False
                    .upPedidosCompras.Visible = False
                    .ibtnAltaPedidoC.Visible = False
                    .ibtnBajaPedidoC.Visible = False
                End If
                If cbTipoAnticipo.SelectedIndex = 1 Then
                    .pnlAdjuntos.Visible = True
                    .lbl_PedidosCompra.Visible = True
                    .ddlPedidosCompra.Visible = True
                    .ddlPedidosCompra.SelectedIndex = -1
                    llenarGvPedidosCompra()
                    .upPedidosCompras.Visible = True
                    .ibtnAltaPedidoC.Visible = True
                    .ibtnAltaPedidoC.ImageUrl = "images\Add_i2.png"
                    .ibtnAltaPedidoC.Enabled = False
                    .ibtnBajaPedidoC.Visible = True
                    .ibtnBajaPedidoC.ImageUrl = "images\Trash_i2.png"
                    .ibtnBajaPedidoC.Enabled = False
                End If
                If cbTipoAnticipo.SelectedIndex = 2 Then
                    .pnlAdjuntos.Visible = True
                    .lbl_PedidosCompra.Visible = False
                    .ddlPedidosCompra.Visible = False
                    .upPedidosCompras.Visible = False
                    .ibtnAltaPedidoC.Visible = False
                    .ibtnBajaPedidoC.Visible = False
                End If
            Catch ex As Exception

            End Try
        End With

    End Sub

    Protected Sub ddlProveedor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProveedor.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaProv As New SqlDataAdapter
                Dim dsProv As New DataSet
                Dim query As String

                query = ""

                query = "SELECT DISTINCT(rfc) as rfc_emisor FROM bd_SiSAC.dbo.cg_proveedor WHERE (rfc <> '' AND rfc IS NOT NULL) and id_proveedor = @id_proveedor"
                sdaProv.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaProv.SelectCommand.Parameters.AddWithValue("@id_proveedor", ddlProveedor.SelectedItem.Value)
                ConexionBD.Open()
                sdaProv.Fill(dsProv)
                ConexionBD.Close()
                ._txtRFCProveedor.Text = dsProv.Tables(0).Rows(0).Item("rfc_emisor").ToString()

                llenarPedidosCompra(._txtRFCProveedor.Text)
            Catch ex As Exception

            End Try
        End With
    End Sub

    Protected Sub ibtnAltaPedidoC_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAltaPedidoC.Click
        With Me
            Try



                Dim ConexionBDNAV As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBDNAV.ConnectionString = accessDB.conBD("NAV")
                Dim sdaPC As New SqlDataAdapter
                Dim dsPC As New DataSet
                Dim empresa As String = .ddlEmpresa.SelectedItem.Text
                Dim query As String
                Dim pc As String
                Dim tot As Decimal

                query = " "

                query = "  SELECT Pc.No_ as pc ,  Convert(decimal(20,2) , SUM(PcL.[Amount Including VAT])) as Total  " +
                                      " FROM [" + empresa + "$Purchase Header] AS Pc" +
                                      " LEFT JOIN [" + empresa + "$Vendor] As Pv On  Pv.No_ =  Pc.[Pay-to Vendor No_]" +
                                      " INNER JOIN [" + empresa + "$Purchase Line] As PcL On PcL.[Document No_] = Pc.No_ " +
                                      " WHERE DATEPART(YEAR,Pc.[Order Date]) >= '2024' " +
                                      " and Pc.No_ = @pc " +
                                      " group by  Pc.No_ " +
                                      " ORDER BY Pc.No_ DESC "

                sdaPC.SelectCommand = New SqlCommand(query, ConexionBDNAV)
                sdaPC.SelectCommand.Parameters.AddWithValue("@pc", .ddlPedidosCompra.SelectedItem.Value)


                ConexionBDNAV.Open()
                sdaPC.Fill(dsPC)
                ConexionBDNAV.Close()


                pc = dsPC.Tables(0).Rows(0).Item("pc").ToString()
                tot = dsPC.Tables(0).Rows(0).Item("Total").ToString()


                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim fecha As DateTime
                fecha = Date.Now
                'Guardar el pedido de compra
                Dim sdaSolicitud As New SqlDataAdapter
                Dim dsSolicitud As New DataSet
                query = " "
                query = " INSERT INTO dt_pedidos_compra ( " +
                        " id_ms_anticipo_proveedor, " +
                        " pedido_compra, " +
                        " total, " +
                        " id_usuario, " +
                        " fecha ) " +
                        " VALUES " +
                        " ( " +
                        " @id_ms_anticipo_proveedor, " +
                        " @pedido_compra, " +
                        " @total, " +
                        " @id_usuario, " +
                        " @fecha " +
                        " ) "
                sdaSolicitud.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_ms_anticipo_proveedor", "-1")
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@pedido_compra", pc)
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@total", tot)
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@fecha", fecha)

                ConexionBD.Open()
                sdaSolicitud.Fill(dsSolicitud)
                ConexionBD.Close()

                llenarGvPedidosCompra()


            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnBajaPedidoC_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBajaPedidoC.Click
        With Me
            Try

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD

                Dim fecha As DateTime
                fecha = Date.Now
                'Eliminar el pedido de compra
                Dim sdaSolicitud As New SqlDataAdapter
                Dim dsSolicitud As New DataSet
                Dim query As String

                query = " DELETE FROM dt_pedidos_compra" +
                        " WHERE id_dt_pedidos_compra = @id_dt_pedidos_compra "
                sdaSolicitud.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_dt_pedidos_compra", (.gvPedidosCompras.DataKeys(gvPedidosCompras.SelectedIndex).Values("id_dt_pedidos_compra")))

                ConexionBD.Open()
                sdaSolicitud.Fill(dsSolicitud)
                ConexionBD.Close()

                llenarGvPedidosCompra()


            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarGvPedidosCompra()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaPedidosC As New SqlDataAdapter
                Dim dsPedididosC As New DataSet
                .gvPedidosCompras.DataSource = dsPedididosC
                sdaPedidosC.SelectCommand = New SqlCommand(" SELECT id_dt_pedidos_compra, total, pedido_compra " +
                                                           " FROM dt_pedidos_compra " +
                                                           " WHERE id_ms_anticipo_proveedor = -1 and id_usuario = @id_usuario ", ConexionBD)
                sdaPedidosC.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                sdaPedidosC.Fill(dsPedididosC)
                .gvPedidosCompras.DataBind()
                ConexionBD.Close()
                sdaPedidosC.Dispose()
                dsPedididosC.Dispose()
                .gvPedidosCompras.SelectedIndex = -1
                .upPedidosCompras.Update()
                .wceImporte.Value = 0


                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = " SELECT SUM(total) FROM dt_pedidos_compra  WHERE id_ms_anticipo_proveedor = -1 and id_usuario = @id_usuario "
                SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                .wceImporte.Value = SCMValores.ExecuteScalar
                ConexionBD.Close()

                .upImpo.Update()

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub


    Protected Sub gvPedidosCompras_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvPedidosCompras.SelectedIndexChanged
        With Me
            Try

                .ibtnBajaPedidoC.Enabled = True
                .ibtnBajaPedidoC.ImageUrl = "images\Trash.png"
            Catch ex As Exception

            End Try
        End With
    End Sub

    Protected Sub ddlPedidosCompra_SelectedIndexChanged(sender As Object, e As EventArgs)
        With Me
            Try
                If .ddlPedidosCompra.SelectedIndex = -1 Then
                    .ibtnAltaPedidoC.Enabled = False
                    .ibtnAltaPedidoC.ImageUrl = "images\Add_i2.png"
                Else
                    .ibtnAltaPedidoC.Enabled = True
                    .ibtnAltaPedidoC.ImageUrl = "images\Add.png"

                End If
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

#End Region


End Class