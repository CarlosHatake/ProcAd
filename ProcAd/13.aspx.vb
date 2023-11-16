Public Class _13
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 1

                    .litError.Text = ""
                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtBan.Text = 0

                        'Creación de Variables para la conexión y consulta de información a la base de datos
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        'Nombre del Solicitante
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "select nombre + ' ' + ap_paterno + ' ' + ap_materno " + _
                                                 "from cg_usuario " + _
                                                 "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                 "where id_usuario = @idUsuario "
                        SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        .lblSolicitante.Text = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        'Lista de Empresas
                        Dim sdaEmpresa As New SqlDataAdapter
                        Dim dsEmpresa As New DataSet
                        sdaEmpresa.SelectCommand = New SqlCommand("select id_empresa, nombre " + _
                                                                  "from bd_empleado.dbo.cg_empresa Empresa " + _
                                                                  "where status = 'A' " + _
                                                                  "order by nombre", ConexionBD)
                        .ddlEmpresa.DataSource = dsEmpresa
                        .ddlEmpresa.DataTextField = "nombre"
                        .ddlEmpresa.DataValueField = "id_empresa"
                        ConexionBD.Open()
                        sdaEmpresa.Fill(dsEmpresa)
                        .ddlEmpresa.DataBind()
                        ConexionBD.Close()
                        sdaEmpresa.Dispose()
                        dsEmpresa.Dispose()
                        .ddlEmpresa.SelectedValue = 9 'Se elige TRACSA por Default
                        rfcEmpresa()
                        'Centros de Costo
                        llenarCC()
                        'Lista de Autorizadores
                        Dim sdaAut As New SqlDataAdapter
                        Dim dsAut As New DataSet
                        .ddlAutorizador.DataSource = dsAut
                        sdaAut.SelectCommand = New SqlCommand("select cgEmpl.id_empleado, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " + _
                                                              "from dt_autorizador " + _
                                                              "  left join cg_usuario on dt_autorizador.id_autorizador = cg_usuario.id_usuario " + _
                                                              "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                              "where dt_autorizador.id_usuario = @idUsuario " + _
                                                              "  and cg_usuario.status = 'A' " + _
                                                              "order by aut_dir, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno ", ConexionBD)
                        sdaAut.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        .ddlAutorizador.DataTextField = "nombre_empleado"
                        .ddlAutorizador.DataValueField = "id_empleado"
                        ConexionBD.Open()
                        sdaAut.Fill(dsAut)
                        .ddlAutorizador.DataBind()
                        ConexionBD.Close()
                        sdaAut.Dispose()
                        dsAut.Dispose()
                        .ddlAutorizador.SelectedIndex = -1
                        'Limpiar Campos
                        .wdtFecha.Text = ""
                        .txtProveedor.Text = ""
                        llenarProv()
                        llenarFacturas()

                        .wdtFecha.MinValue = CDate(Date.Now.Year.ToString + "-01-01")

                        'Botones
                        .btnGuardar.Enabled = True
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

    Public Sub rfcEmpresa()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select rfc " + _
                                         "from bd_Empleado.dbo.cg_empresa " + _
                                         "where id_empresa = @idEmpresa "
                SCMValores.Parameters.AddWithValue("@idEmpresa", .ddlEmpresa.SelectedValue)
                ConexionBD.Open()
                ._txtRFCEmpr.Text = SCMValores.ExecuteScalar()
                ConexionBD.Close()
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
                Dim sdaCC As New SqlDataAdapter
                Dim dsCC As New DataSet
                sdaCC.SelectCommand = New SqlCommand("select id_cc, nombre " + _
                                                     "from bd_Empleado.dbo.cg_cc " + _
                                                     "where id_empresa = @idEmpresa " + _
                                                     "  and status = 'A' " + _
                                                     "order by nombre ", ConexionBD)
                sdaCC.SelectCommand.Parameters.AddWithValue("@idEmpresa", .ddlEmpresa.SelectedValue)
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

    Public Sub llenarProv()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaProv As New SqlDataAdapter
                Dim dsProv As New DataSet
                Dim query As String
                query = "select distinct(rfc_emisor), rfc_emisor + ' / ' + razon_emisor as proveedor " + _
                        "from dt_factura " + _
                        "where estatus = 'VIGENTE' " + _
                        "  and movimiento in ('RECIBIDAS', 'RECIBIDA') " + _
                        "  and (rfc_emisor like '%' + @valor+ '%' or razon_emisor like '%' + @valor+ '%') " + _
                        "  and rfc_receptor = @rfcEmpr " + _
                        "  and status = 'P' " + _
                        "  and (importe > 0 or rfc_emisor = 'ASE930924SS7') " + _
                        "  and ((select count(*) " + _
                        "        from cg_usuario " + _
                        "        where id_usuario = @idUsuario " + _
                        "          and factura_extemp = 'S') > 0 " + _
                        "       or " + _
                        "	   (case when (month(@fecha) = 12 and day(@fecha) < 25) then 0 " + _
                        "          else case when (day(@fecha) = 1 and (fecha_emision >= (dateadd(day, -5, convert(date, @fecha))))) then 0 " + _
                        "                 else case when month(fecha_emision) = month(@fecha) then 0 " + _
                        "                        else 1 " + _
                        "                      end " + _
                        "               end " + _
                        "        end = 0 " + _
                        "		and year(fecha_emision) = year(@fecha)) " + _
                        "       or " + _
                        "	   (case when (month(@fecha) = 1 and day(@fecha) < 4 and month(fecha_emision) = 12) then 0 " + _
                        "          else 1 " + _
                        "        end = 0) " + _
                        "        ) " + _
                        "  and year(fecha_emision) >= (select valor from cg_parametros where parametro = 'año_emision') "
                If .wdtFecha.Text <> "" Then
                    query = query + "  and fecha_emision between @fechaE and dateadd(HOUR, 24, @fechaE)"
                End If
                sdaProv.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaProv.SelectCommand.Parameters.AddWithValue("@valor", .txtProveedor.Text.Trim)
                sdaProv.SelectCommand.Parameters.AddWithValue("@rfcEmpr", ._txtRFCEmpr.Text)
                If .wdtFecha.Text <> "" Then
                    sdaProv.SelectCommand.Parameters.AddWithValue("@fechaE", .wdtFecha.Date)
                End If
                sdaProv.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                sdaProv.SelectCommand.Parameters.AddWithValue("@fecha", Date.Now)
                .ddlProveedor.DataSource = dsProv
                .ddlProveedor.DataTextField = "proveedor"
                .ddlProveedor.DataValueField = "rfc_emisor"
                ConexionBD.Open()
                sdaProv.Fill(dsProv)
                .ddlProveedor.DataBind()
                ConexionBD.Close()
                sdaProv.Dispose()
                dsProv.Dispose()
                .ddlProveedor.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarFacturas()
        With Me
            Try
                .litError.Text = ""
                If .ddlProveedor.Items.Count > 0 Then
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim sdaFacturas As New SqlDataAdapter
                    Dim dsFacturas As New DataSet
                    .gvFacturas.DataSource = dsFacturas
                    'Habilitar columnas para actualización
                    .gvFacturas.Columns(0).Visible = True
                    .gvFacturas.Columns(11).Visible = True
                    Dim query As String
                    query = "select id_dt_factura " + _
                            "     , fecha_emision " + _
                            "     , uuid " + _
                            "     , serie " + _
                            "     , folio " + _
                            "     , lugar_exp " + _
                            "     , forma_pago " + _
                            "     , moneda " + _
                            "     , subtotal " + _
                            "     , importe " + _
                            "from dt_factura " + _
                            "where estatus = 'VIGENTE' " + _
                            "  and movimiento in ('RECIBIDAS', 'RECIBIDA') " + _
                            "  and rfc_emisor = @rfcProv " + _
                            "  and rfc_receptor = @rfcEmpr " + _
                            "  and status = 'P' " + _
                            "  and (importe > 0 or rfc_emisor = 'ASE930924SS7') " + _
                            "  and ((select count(*) " + _
                            "        from cg_usuario " + _
                            "        where id_usuario = @idUsuario " + _
                            "          and factura_extemp = 'S') > 0 " + _
                            "       or " + _
                            "	   (case when (month(@fecha) = 12 and day(@fecha) < 25) then 0 " + _
                            "          else case when (day(@fecha) = 1 and (fecha_emision >= (dateadd(day, -5, convert(date, @fecha))))) then 0 " + _
                            "                 else case when month(fecha_emision) = month(@fecha) then 0 " + _
                            "                        else 1 " + _
                            "                      end " + _
                            "               end " + _
                            "        end = 0 " + _
                            "		and year(fecha_emision) = year(@fecha)) " + _
                            "       or " + _
                            "	   (case when (month(@fecha) = 1 and day(@fecha) < 4 and month(fecha_emision) = 12) then 0 " + _
                            "          else 1 " + _
                            "        end = 0) " + _
                            "        ) " + _
                            "  and year(fecha_emision) >= (select valor from cg_parametros where parametro = 'año_emision') "
                    If .wdtFecha.Text <> "" Then
                        query = query + "  and fecha_emision between @fechaE and dateadd(HOUR, 24, @fechaE) "
                    End If
                    sdaFacturas.SelectCommand = New SqlCommand(query + "order by fecha_emision ", ConexionBD)
                    sdaFacturas.SelectCommand.Parameters.AddWithValue("@rfcProv", .ddlProveedor.SelectedValue)
                    sdaFacturas.SelectCommand.Parameters.AddWithValue("@rfcEmpr", ._txtRFCEmpr.Text)
                    If .wdtFecha.Text <> "" Then
                        sdaFacturas.SelectCommand.Parameters.AddWithValue("@fechaE", .wdtFecha.Date)
                    End If
                    sdaFacturas.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                    sdaFacturas.SelectCommand.Parameters.AddWithValue("@fecha", Date.Now)
                    ConexionBD.Open()
                    sdaFacturas.Fill(dsFacturas)
                    .gvFacturas.DataBind()
                    ConexionBD.Close()
                    sdaFacturas.Dispose()
                    dsFacturas.Dispose()
                    .gvFacturas.SelectedIndex = -1
                    'Inhabilitar columnas para vista
                    .gvFacturas.Columns(0).Visible = False
                    .gvFacturas.Columns(11).Visible = False
                Else
                    .gvFacturas.DataBind()
                End If
                If .gvFacturas.Rows.Count > 0 Then
                    .gvFacturas.Visible = True
                Else
                    .gvFacturas.Visible = False
                    .litError.Text = "No existen registros de ese Proveedor, favor de validarlo."
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Botones / Listas de Búsqueda"

    Protected Sub ddlEmpresa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEmpresa.SelectedIndexChanged
        rfcEmpresa()
        llenarCC()
        llenarProv()
        llenarFacturas()
    End Sub

    Protected Sub ibtnBuscarProv_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBuscarProv.Click
        llenarProv()
        llenarFacturas()
    End Sub

    Protected Sub ddlProveedor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProveedor.SelectedIndexChanged
        llenarFacturas()
    End Sub

#End Region

#Region "Guardar"

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        With Me
            Try
                .litError.Text = ""
                If Val(._txtBan.Text) = 1 Then
                    .litError.Text = "Ya se almacenó la factura previamente, favor de validarlo en el apartado de Consulta de Facturas"
                Else
                    If .gvFacturas.Rows.Count = 0 Or .gvFacturas.SelectedIndex = -1 Then
                        .litError.Text = "Favor de Seleccionar una Factura"
                    Else
                        If Not ((Not fuTablaComp.PostedFile Is Nothing) And (fuTablaComp.PostedFile.ContentLength > 0)) Then
                            .litError.Text = "Favor de Ingresar la Tabla Comparativa o Contrato"
                        Else
                            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                            SCMValores.Connection = ConexionBD
                            'Tabla Comparativa
                            ' '' Ruta Local
                            ''Dim sFileDir As String = "C:/ProcAd - Adjuntos/" 'Ruta en que se almacenará el archivo
                            ' Ruta en Atenea
                            Dim sFileDir As String = "D:\ProcAd - Adjuntos\" 'Ruta en que se almacenará el archivo
                            Dim lMaxFileSize As Long = 10485760 'Equivalente a 10 Megabytes
                            Dim sFileNameTabCon As String = System.IO.Path.GetFileName(fuTablaComp.PostedFile.FileName)
                            Dim sFileNameAdjOpc As String = ""
                            If (Not fuAdjOpc.PostedFile Is Nothing) And (fuAdjOpc.PostedFile.ContentLength > 0) Then
                                sFileNameAdjOpc = System.IO.Path.GetFileName(fuAdjOpc.PostedFile.FileName)
                            End If

                            While Val(._txtBan.Text) = 0
                                Dim fecha As DateTime
                                fecha = Date.Now
                                'Insertar Solicitud
                                Dim sdaEmpleado As New SqlDataAdapter
                                Dim dsEmpleado As New DataSet
                                Dim query As String
                                query = "select cgEmpl.no_empleado as no_empleadoE " + _
                                        "     , cgUsrA.id_usuario as id_usr_aut " + _
                                        "     , cgAut.no_empleado as no_empleadoA " + _
                                        "     , cgAut.nombre + ' ' + cgAut.ap_paterno + ' ' + cgAut.ap_materno as Autorizador " + _
                                        "from bd_empleado.dbo.cg_empleado cgEmpl " + _
                                        "  left join cg_usuario cgUsrE on cgEmpl.id_empleado = cgUsrE.id_empleado " + _
                                        "  left join bd_Empleado.dbo.cg_empleado cgAut on cgAut.id_empleado = @idAut " + _
                                        "  left join cg_usuario cgUsrA on cgAut.id_empleado = cgUsrA.id_empleado " + _
                                        "where cgUsrE.id_usuario = @idEmpl "
                                sdaEmpleado.SelectCommand = New SqlCommand(query, ConexionBD)
                                sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idEmpl", Val(._txtIdUsuario.Text))
                                sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idAut", .ddlAutorizador.SelectedValue)
                                ConexionBD.Open()
                                sdaEmpleado.Fill(dsEmpleado)
                                ConexionBD.Close()

                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "insert into ms_factura ( id_usr_solicita,  fecha_solicita,  id_usr_autoriza,  empresa,  centro_costo,  no_empleado,  empleado,  no_autorizador,  autorizador,  RFC,  CFDI,  importe_tot,  tabla_comp,  adjunto_opcional,  status) " + _
                                                         " 			      values (@id_usr_solicita, @fecha_solicita, @id_usr_autoriza, @empresa, @centro_costo, @no_empleado, @empleado, @no_autorizador, @autorizador, @RFC, @CFDI, @importe_tot, @tabla_comp, @adjunto_opcional,     'P')"
                                SCMValores.Parameters.AddWithValue("@id_usr_solicita", Val(._txtIdUsuario.Text))
                                SCMValores.Parameters.AddWithValue("@fecha_solicita", fecha)
                                SCMValores.Parameters.AddWithValue("@id_usr_autoriza", dsEmpleado.Tables(0).Rows(0).Item("id_usr_aut").ToString())
                                SCMValores.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedItem.Text)
                                SCMValores.Parameters.AddWithValue("@centro_costo", .ddlCC.SelectedItem.Text)
                                SCMValores.Parameters.AddWithValue("@no_empleado", dsEmpleado.Tables(0).Rows(0).Item("no_empleadoE").ToString())
                                SCMValores.Parameters.AddWithValue("@empleado", .lblSolicitante.Text)
                                SCMValores.Parameters.AddWithValue("@no_autorizador", dsEmpleado.Tables(0).Rows(0).Item("no_empleadoA").ToString())
                                SCMValores.Parameters.AddWithValue("@autorizador", dsEmpleado.Tables(0).Rows(0).Item("Autorizador").ToString())
                                SCMValores.Parameters.AddWithValue("@RFC", .ddlProveedor.SelectedValue)
                                SCMValores.Parameters.AddWithValue("@CFDI", .gvFacturas.SelectedRow.Cells(3).Text)
                                SCMValores.Parameters.AddWithValue("@importe_tot", Val(.gvFacturas.SelectedRow.Cells(11).Text))
                                SCMValores.Parameters.AddWithValue("@tabla_comp", sFileNameTabCon)
                                If (Not fuAdjOpc.PostedFile Is Nothing) And (fuAdjOpc.PostedFile.ContentLength > 0) Then
                                    SCMValores.Parameters.AddWithValue("@adjunto_opcional", sFileNameAdjOpc)
                                Else
                                    SCMValores.Parameters.AddWithValue("@adjunto_opcional", DBNull.Value)
                                End If
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()
                                'Obtener ID de la Solicitud
                                SCMValores.CommandText = "select max(id_ms_factura) from ms_factura where no_empleado = @no_empleado and status not in ('Z') "
                                ConexionBD.Open()
                                .lblFolio.Text = SCMValores.ExecuteScalar
                                ConexionBD.Close()
                                If Val(.lblFolio.Text) > 0 Then
                                    ._txtBan.Text = 1
                                End If

                                'Actualizar dt_factura
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "update dt_factura " + _
                                                         "  set status = 'As' " + _
                                                         "where uuid = @uuid " + _
                                                         "  and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') "
                                SCMValores.Parameters.AddWithValue("@uuid", .gvFacturas.SelectedRow.Cells(3).Text)
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()

                                'Guardar Archivos Adjuntos
                                If (Not fuTablaComp.PostedFile Is Nothing) And (fuTablaComp.PostedFile.ContentLength > 0) Then
                                    'Se agrega el Id al nombre del archivo / Almacenar el archivo en la ruta especificada
                                    sFileNameTabCon = .lblFolio.Text + "TabCon-" + sFileNameTabCon
                                    fuTablaComp.PostedFile.SaveAs(sFileDir + sFileNameTabCon)
                                End If

                                If (Not fuAdjOpc.PostedFile Is Nothing) And (fuAdjOpc.PostedFile.ContentLength > 0) Then
                                    'Se agrega el Id al nombre del archivo / Almacenar el archivo en la ruta especificada
                                    sFileNameAdjOpc = .lblFolio.Text + "AdjOpc-" + sFileNameAdjOpc
                                    fuAdjOpc.PostedFile.SaveAs(sFileDir + sFileNameAdjOpc)
                                End If

                                'Insertar Instancia de Solicitud de Liberación
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "insert into ms_instancia ( id_ms_sol,  tipo,  id_actividad) " + _
                                                         "				    values (@id_ms_sol, @tipo, @id_actividad) "
                                SCMValores.Parameters.AddWithValue("@id_ms_sol", Val(.lblFolio.Text))
                                SCMValores.Parameters.AddWithValue("@tipo", "F")
                                SCMValores.Parameters.AddWithValue("@id_actividad", 14)
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()
                                'Obtener ID de la Instancia de Solicitud 
                                SCMValores.CommandText = "select max(id_ms_instancia) from ms_instancia where id_ms_sol = @id_ms_sol and tipo = 'F' "
                                ConexionBD.Open()
                                ._txtIdMsInst.Text = SCMValores.ExecuteScalar
                                ConexionBD.Close()
                                'Insertar Históricos
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "insert into ms_historico ( id_ms_instancia,  id_actividad,  id_usr,  fecha) " + _
                                                         "				    values (@id_ms_instancia, @id_actividad, @id_usr, @fecha) "
                                SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                                SCMValores.Parameters.AddWithValue("@id_actividad", 14)
                                SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                                SCMValores.Parameters.AddWithValue("@fecha", fecha)
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()

                                'Envío de Correo
                                Dim Mensaje As New System.Net.Mail.MailMessage()
                                Dim destinatario As String = ""
                                'Obtener el Correos del Autorizador
                                SCMValores.CommandText = "select cgEmpl.correo from bd_empleado.dbo.cg_empleado cgEmpl where cgEmpl.id_empleado = @idAut "
                                SCMValores.Parameters.AddWithValue("@idAut", .ddlAutorizador.SelectedValue)
                                ConexionBD.Open()
                                destinatario = SCMValores.ExecuteScalar()
                                ConexionBD.Close()

                                Mensaje.[To].Add(destinatario)
                                Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                                'Mensaje.From = New System.Net.Mail.MailAddress("soporte.unne.procad@gmail.com", "ProcAd", System.Text.Encoding.UTF8)
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

                                'Inhabilitar Paneles
                                .pnlInicio.Enabled = False
                            End While
                        End If
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class