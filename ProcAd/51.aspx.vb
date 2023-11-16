Public Class _51
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 3
                    'Session("idMsInst") = 29622 '29623

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtIdMsInst.Text = Session("idMsInst")
                        ._txtBan.Text = 0

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim sdaSol As New SqlDataAdapter
                        Dim dsSol As New DataSet
                        sdaSol.SelectCommand = New SqlCommand("select ms_factura.id_ms_factura " + _
                                                              "     , empleado " + _
                                                              "     , empresa " + _
                                                              "     , isnull(division, centro_costo) as centro_costo " + _
                                                              "     , case when division is null then 'CC' else 'DIV' end as dimension " + _
                                                              "     , isnull(ms_factura.tipo_servicio, servicio) as tipo_servicio " + _
                                                              "     , isnull(ms_factura.id_tipo_servicio, 0) as id_tipo_servicio " + _
                                                              "     , isnull(ms_factura.id_dt_servicio_conf, 0) as id_dt_servicio_conf " + _
                                                              "     , cg_tipo_servicio.id_usr_autoriza " + _
                                                              "     , isnull(ms_factura.id_usr_autoriza2, 0) as id_usr_autoriza2 " + _
                                                              "     , isnull(ms_factura.id_usr_autoriza3, 0) as id_usr_autoriza3 " + _
                                                              "     , especificaciones " + _
                                                              "     , isnull(proveedor_selec, 0) as proveedor_selec " + _
                                                              "     , isnull(cg_proveedor.nombre, '') as proveedor " + _
                                                              "     , cg_empresa.rfc " + _
                                                              "     , CFDI " + _
                                                              "     , isnull(ultimos_comentarios, '') as ultimos_comentarios " + _
                                                              "from ms_factura " + _
                                                              "  left join ms_instancia on ms_factura.id_ms_factura = ms_instancia.id_ms_sol and ms_instancia.tipo = 'F' " + _
                                                              "  left join bd_SiSAC.dbo.cg_proveedor on ms_factura.proveedor_selec = cg_proveedor.id_proveedor " + _
                                                              "  left join cg_tipo_servicio on ms_factura.id_tipo_servicio = cg_tipo_servicio.id_tipo_servicio " + _
                                                              "  left join bd_Empleado.dbo.cg_empresa on ms_factura.empresa = cg_empresa.nombre " + _
                                                              "where id_ms_instancia = @idMsInst ", ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@idMsInst", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()
                        .lblFolio.Text = dsSol.Tables(0).Rows(0).Item("id_ms_factura").ToString()
                        .lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("empleado").ToString()
                        .lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                        ._txtRFCEmpr.Text = dsSol.Tables(0).Rows(0).Item("rfc").ToString()
                        If dsSol.Tables(0).Rows(0).Item("dimension").ToString() = "CC" Then
                            .lbl_CC.Text = "Centro de Costo:"
                        Else
                            .lbl_CC.Text = "División:"
                        End If
                        .lblCC.Text = dsSol.Tables(0).Rows(0).Item("centro_costo").ToString()
                        ._txtIdTipoServicio.Text = dsSol.Tables(0).Rows(0).Item("id_tipo_servicio").ToString()
                        ._txtIdDtServ.Text = dsSol.Tables(0).Rows(0).Item("id_dt_servicio_conf").ToString()
                        .lblTipoServicio.Text = dsSol.Tables(0).Rows(0).Item("tipo_servicio").ToString()
                        ._txtIdAutorizador.Text = dsSol.Tables(0).Rows(0).Item("id_usr_autoriza").ToString()
                        .txtEspecificaciones.Text = dsSol.Tables(0).Rows(0).Item("especificaciones").ToString()
                        ._txtIdProveedor.Text = dsSol.Tables(0).Rows(0).Item("proveedor_selec").ToString()
                        .lblProveedor.Text = dsSol.Tables(0).Rows(0).Item("proveedor").ToString()
                        ._txtCFDI.Text = dsSol.Tables(0).Rows(0).Item("CFDI").ToString()
                        .txtUltComent.Text = dsSol.Tables(0).Rows(0).Item("ultimos_comentarios").ToString()
                        ._txtIdAutorizador2.Text = dsSol.Tables(0).Rows(0).Item("id_usr_autoriza2").ToString()
                        ._txtIdAutorizador3.Text = dsSol.Tables(0).Rows(0).Item("id_usr_autoriza3").ToString()
                        sdaSol.Dispose()
                        dsSol.Dispose()

                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        SCMValores.Parameters.Clear()
                        'Tope 1
                        SCMValores.CommandText = "select valor from cg_parametros where parametro = 'ingFact_tope1'"
                        ConexionBD.Open()
                        ._txtTope1.Text = SCMValores.ExecuteScalar()
                        ConexionBD.Close()
                        'Tope 2
                        SCMValores.CommandText = "select valor from cg_parametros where parametro = 'ingFact_tope2'"
                        ConexionBD.Open()
                        ._txtTope2.Text = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        If Val(._txtIdTipoServicio.Text) > 0 Then
                            'Autorizadores
                            Dim sdaAut As New SqlDataAdapter
                            Dim dsAut As New DataSet
                            .ddlAutorizador.DataSource = dsAut
                            Select Case Val(._txtIdAutorizador.Text)
                                Case -1
                                    sdaAut.SelectCommand = New SqlCommand("select cg_usuario.id_usuario, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " + _
                                                                          "from dt_autorizador " + _
                                                                          "  left join cg_usuario on dt_autorizador.id_autorizador = cg_usuario.id_usuario " + _
                                                                          "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                                          "where dt_autorizador.id_usuario = @idUsuario " + _
                                                                          "  and cg_usuario.status = 'A' " + _
                                                                          "order by aut_dir, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno ", ConexionBD)
                                    sdaAut.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                                Case 0
                                    sdaAut.SelectCommand = New SqlCommand("select cg_usuario.id_usuario, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " + _
                                                                          "from dt_autoriza_servicio " + _
                                                                          "  left join cg_usuario on dt_autoriza_servicio.id_usuario = cg_usuario.id_usuario " + _
                                                                          "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                                          "where id_tipo_servicio = @idTipoServicio " + _
                                                                          "  and valida_autoriza = 'A' " + _
                                                                          "order by nombre_empleado ", ConexionBD)
                                    sdaAut.SelectCommand.Parameters.AddWithValue("@idTipoServicio", Val(._txtIdTipoServicio.Text))
                                Case Else
                                    sdaAut.SelectCommand = New SqlCommand("select cg_usuario.id_usuario, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " + _
                                                                          "from cg_usuario " + _
                                                                          "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                                          "where cg_usuario.id_usuario = @idUsuario ", ConexionBD)
                                    sdaAut.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdAutorizador.Text))
                            End Select
                            .ddlAutorizador.DataTextField = "nombre_empleado"
                            .ddlAutorizador.DataValueField = "id_usuario"
                            ConexionBD.Open()
                            sdaAut.Fill(dsAut)
                            .ddlAutorizador.DataBind()
                            ConexionBD.Close()
                            sdaAut.Dispose()
                            dsAut.Dispose()
                            .ddlAutorizador.SelectedIndex = -1

                            '2do Autorizador
                            Dim sdaAut2 As New SqlDataAdapter
                            Dim dsAut2 As New DataSet
                            .ddlAutorizador2.DataSource = dsAut2
                            sdaAut2.SelectCommand = New SqlCommand("select 0 as id_usuario " + _
                                                                   "     , ' ' as nombre_empleado " + _
                                                                   "union " + _
                                                                   "select cg_usuario.id_usuario, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " + _
                                                                   "from dt_autoriza_servicio " + _
                                                                   "  left join cg_usuario on dt_autoriza_servicio.id_usuario = cg_usuario.id_usuario " + _
                                                                   "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                                   "where id_tipo_servicio = 0 " + _
                                                                   "  and valida_autoriza = 'T' " + _
                                                                   "order by nombre_empleado ", ConexionBD)
                            .ddlAutorizador2.DataTextField = "nombre_empleado"
                            .ddlAutorizador2.DataValueField = "id_usuario"
                            ConexionBD.Open()
                            sdaAut2.Fill(dsAut2)
                            .ddlAutorizador2.DataBind()
                            ConexionBD.Close()
                            sdaAut2.Dispose()
                            dsAut2.Dispose()
                            .ddlAutorizador2.SelectedIndex = -1
                            .upAutorizador2.Update()

                            '3er Autorizador
                            Dim sdaAut3 As New SqlDataAdapter
                            Dim dsAut3 As New DataSet
                            .ddlAutorizador3.DataSource = dsAut3
                            sdaAut3.SelectCommand = New SqlCommand("select cg_tipo_servicio.id_usr_autoriza_tercer " + _
                                                                   "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as nom_usr_autoriza_tercer " + _
                                                                   "from cg_tipo_servicio " + _
                                                                   "  left join cg_usuario on cg_tipo_servicio.id_usr_autoriza_tercer = cg_usuario.id_usuario " + _
                                                                   "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                                                                   "where id_tipo_servicio = @id_tipo_servicio ", ConexionBD)
                            sdaAut3.SelectCommand.Parameters.AddWithValue("@id_tipo_servicio", Val(._txtIdTipoServicio.Text))
                            .ddlAutorizador3.DataTextField = "nom_usr_autoriza_tercer"
                            .ddlAutorizador3.DataValueField = "id_usr_autoriza_tercer"
                            ConexionBD.Open()
                            sdaAut3.Fill(dsAut3)
                            .ddlAutorizador3.DataBind()
                            ConexionBD.Close()
                            sdaAut3.Dispose()
                            dsAut3.Dispose()
                            .ddlAutorizador3.SelectedIndex = -1
                            .upAutorizador3.Update()
                        Else
                            Dim sdaMsFact As New SqlDataAdapter
                            Dim dsMsFact As New DataSet
                            sdaMsFact.SelectCommand = New SqlCommand("select cg_empresa.id_empresa " + _
                                                                     "     , isnull(cg_cc.id_cc, 0) as id_cc " + _
                                                                     "     , isnull(cg_div.id_div, 0) as id_div " + _
                                                                     "     , dt_servicio_conf.id_usr_autoriza1 " + _
                                                                     "     , dt_servicio_conf.id_usr_autoriza2 " + _
                                                                     "     , dt_servicio_conf.id_usr_autoriza3 " + _
                                                                     "from ms_factura " + _
                                                                     "  left join dt_servicio_conf on ms_factura.id_dt_servicio_conf = dt_servicio_conf.id_dt_servicio_conf " + _
                                                                     "  left join bd_Empleado.dbo.cg_empresa on ms_factura.empresa = cg_empresa.nombre " + _
                                                                     "  left join bd_Empleado.dbo.cg_cc on cg_empresa.id_empresa = cg_cc.id_empresa and ms_factura.centro_costo = cg_cc.nombre " + _
                                                                     "  left join bd_Empleado.dbo.cg_div on cg_empresa.id_empresa = cg_div.id_empresa and ms_factura.division = cg_div.nombre " + _
                                                                     "where id_ms_factura = @id_ms_factura ", ConexionBD)
                            sdaMsFact.SelectCommand.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                            ConexionBD.Open()
                            sdaMsFact.Fill(dsMsFact)
                            ConexionBD.Close()
                            ._txtIdEmpresa.Text = dsMsFact.Tables(0).Rows(0).Item("id_empresa").ToString()
                            ._txtCC.Text = dsMsFact.Tables(0).Rows(0).Item("id_cc").ToString()
                            ._txtDiv.Text = dsMsFact.Tables(0).Rows(0).Item("id_div").ToString()
                            ._txtIdAutorizador1.Text = dsMsFact.Tables(0).Rows(0).Item("id_usr_autoriza1").ToString()
                            ._txtIdAutorizador2.Text = dsMsFact.Tables(0).Rows(0).Item("id_usr_autoriza2").ToString()
                            ._txtIdAutorizador3.Text = dsMsFact.Tables(0).Rows(0).Item("id_usr_autoriza3").ToString()
                            sdaMsFact.Dispose()
                            dsMsFact.Dispose()

                            'Autorizador 1
                            actualizarValAut(Val(._txtIdAutorizador1.Text), "A1", .lbl_Autorizador, .up_Autorizador, .ddlAutorizador, .upAutorizador)
                            'Autorizador 2
                            actualizarValAut(Val(._txtIdAutorizador2.Text), "A2", .lbl_Autorizador2, .up_Autorizador2, .ddlAutorizador2, .upAutorizador2)
                            'Autorizador 3
                            actualizarValAut(Val(._txtIdAutorizador3.Text), "A3", .lbl_Autorizador3, .up_Autorizador3, .ddlAutorizador3, .upAutorizador3)
                        End If

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

                        If Val(._txtIdProveedor.Text) > 0 Then
                            'RFC del Proveedor
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "select rfc " + _
                                                     "from  bd_SiSAC.dbo.cg_proveedor " + _
                                                     "where id_proveedor = @idProveedor "
                            SCMValores.Parameters.AddWithValue("@idProveedor", Val(._txtIdProveedor.Text))
                            ConexionBD.Open()
                            ._txtRFCProv.Text = SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            'Facturas
                            Dim sdaFacturas As New SqlDataAdapter
                            Dim dsFacturas As New DataSet
                            .gvFacturas.DataSource = dsFacturas
                            'Habilitar columnas para actualización
                            .gvFacturas.Columns(0).Visible = True
                            .gvFacturas.Columns(11).Visible = True
                            'Dim query As String
                            'query = "select id_dt_factura " + _
                            '        "     , fecha_emision " + _
                            '        "     , uuid " + _
                            '        "     , serie " + _
                            '        "     , folio " + _
                            '        "     , lugar_exp " + _
                            '        "     , forma_pago " + _
                            '        "     , moneda " + _
                            '        "     , subtotal " + _
                            '        "     , importe " + _
                            '        "from dt_factura " + _
                            '        "where (estatus = 'VIGENTE' " + _
                            '        "   and movimiento in ('RECIBIDAS', 'RECIBIDA') " + _
                            '        "   and rfc_emisor = @rfcProv " + _
                            '        "   and rfc_receptor = @rfcEmpr " + _
                            '        "   and status = 'P' " + _
                            '        "   and (importe > 0 or rfc_emisor = 'ASE930924SS7') " + _
                            '        "   and ((select count(*) " + _
                            '        "         from cg_usuario " + _
                            '        "         where id_usuario = @idUsuario " + _
                            '        "           and factura_extemp = 'S') > 0 " + _
                            '        "        or " + _
                            '        " 	     (case when (month(@fecha) = 12 and day(@fecha) < 25) then 0 " + _
                            '        "          else case when (day(@fecha) = 1 and (fecha_emision >= (dateadd(day, -5, convert(date, @fecha))))) then 0 " + _
                            '        "                 else case when month(fecha_emision) = month(@fecha) then 0 " + _
                            '        "                        else 1 " + _
                            '        "                      end " + _
                            '        "              end " + _
                            '        "        end = 0 " + _
                            '        "		and year(fecha_emision) = year(@fecha)) " + _
                            '        "		or " + _
                            '        "	    (case when (month(@fecha) = 1 and day(@fecha) < 4 and month(fecha_emision) = 12) then 0 " + _
                            '        "           else 1 " + _
                            '        "         end = 0) " + _
                            '        "        )) " + _
                            '        "  and year(fecha_emision) >= (select valor from cg_parametros where parametro = 'año_emision') " + _
                            '        "   or (dt_factura.uuid = @cfdi and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA')) "
                            'query + "order by fecha_emision "

                            'Nueva versión Procedimiento Almacenado Carlos Hernández 19 Jul 22
                            sdaFacturas.SelectCommand = New SqlCommand("SP_C_dt_facturaLlenado @rfcProv, @rfcEmpr, @cfdi, @idUsuario, @fecha ", ConexionBD)

                            sdaFacturas.SelectCommand.Parameters.AddWithValue("@rfcProv", ._txtRFCProv.Text)
                            sdaFacturas.SelectCommand.Parameters.AddWithValue("@rfcEmpr", ._txtRFCEmpr.Text)
                            sdaFacturas.SelectCommand.Parameters.AddWithValue("@cfdi", ._txtCFDI.Text)
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

                            ''Ocultar campos de Autorizadores 2 y 3
                            '.lbl_Autorizador2.Visible = False
                            '.up_Autorizador2.Update()
                            '.ddlAutorizador2.Visible = False
                            '.upAutorizador2.Update()
                            '.lbl_Autorizador3.Visible = False
                            '.up_Autorizador3.Update()
                            '.lblAutorizador3.Visible = False
                            '.upAutorizador3.Update()
                        Else
                            .gvFacturas.DataBind()
                        End If
                        If .gvFacturas.Rows.Count > 0 Then
                            .gvFacturas.Visible = True
                            'Seleccionar la factura elegida previamente
                            For i = 0 To .gvFacturas.Rows.Count() - 1
                                If .gvFacturas.Rows(i).Cells(3).Text = ._txtCFDI.Text Then
                                    .gvFacturas.SelectedIndex = i

                                    'Tope 1
                                    If Val(.gvFacturas.SelectedRow.Cells(11).Text) >= Val(._txtTope1.Text) Then
                                        .lbl_Autorizador2.Visible = True
                                        .up_Autorizador2.Update()
                                        .ddlAutorizador2.Visible = True
                                        .upAutorizador2.Update()
                                    Else
                                        .lbl_Autorizador2.Visible = False
                                        .up_Autorizador2.Update()
                                        .ddlAutorizador2.Visible = False
                                        .upAutorizador2.Update()
                                    End If
                                    'Tope 2
                                    If Val(.gvFacturas.SelectedRow.Cells(11).Text) >= Val(._txtTope2.Text) Then
                                        .lbl_Autorizador3.Visible = True
                                        .up_Autorizador3.Update()
                                        .ddlAutorizador3.Visible = True
                                        .upAutorizador3.Update()
                                    Else
                                        .lbl_Autorizador3.Visible = False
                                        .up_Autorizador3.Update()
                                        .ddlAutorizador3.Visible = False
                                        .upAutorizador3.Update()
                                    End If
                                End If
                            Next
                        Else
                            .gvFacturas.Visible = False
                            .litError.Text = "No existen registros de ese Proveedor, favor de validarlo."
                        End If

                        'Panel
                        .pnlInicio.Enabled = True

                        'Activos Fijos
                        .pnlFiltroAF.Visible = False
                        .upFiltroAF.Update()
                        actualizarEvidencias()
                        actualizarAF()
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

    Public Sub actualizarValAut(ByVal idUsr, ByVal tipo, ByRef etiqueta, ByRef upEtiqueta, ByRef lista, ByRef upLista)
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                If idUsr = -99 Then
                    'No aplica
                    etiqueta.Visible = False
                    upEtiqueta.Update()
                    lista.Visible = False
                    lista.Items.Clear()
                    upLista.Update()
                Else
                    'Sí aplica
                    Dim sdaValAut As New SqlDataAdapter
                    Dim dsValAut As New DataSet
                    etiqueta.Visible = True
                    upEtiqueta.Update()
                    lista.Visible = True
                    lista.DataSource = dsValAut
                    Select Case idUsr
                        Case -7
                            ' Director de Empresa
                            sdaValAut.SelectCommand = New SqlCommand("select isnull(id_usr_director, 0) as id_usuario " + _
                                                                     "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                     "from bd_Empleado.dbo.cg_empresa " + _
                                                                     "  left join cg_usuario on cg_empresa.id_usr_director = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                                                                     "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                                                                     "where id_empresa = @idEmpresa " + _
                                                                     "  and cg_empresa.status = 'A' ", ConexionBD)
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@idEmpresa", Val(._txtIdEmpresa.Text))
                        Case -6
                            ' Gerente de Empresa
                            sdaValAut.SelectCommand = New SqlCommand("select isnull(id_usr_gerente, 0) as id_usuario " + _
                                                                     "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                     "from bd_Empleado.dbo.cg_empresa " + _
                                                                     "  left join cg_usuario on cg_empresa.id_usr_gerente = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                                                                     "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                                                                     "where id_empresa = @idEmpresa " + _
                                                                     "  and cg_empresa.status = 'A' ", ConexionBD)
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@idEmpresa", Val(._txtIdEmpresa.Text))
                        Case -5
                            ' Director División / CC
                            If Val(._txtCC.Text) = 0 Then
                                'División
                                sdaValAut.SelectCommand = New SqlCommand("select isnull(id_usr_director, 0) as id_usuario " + _
                                                                         "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                         "from bd_Empleado.dbo.cg_div " + _
                                                                         "  left join cg_usuario on cg_div.id_usr_director = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                                                                         "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                                                                         "where cg_div.id_div = @id_ccDiv " + _
                                                                         "  and cg_div.status = 'A' ", ConexionBD)
                                sdaValAut.SelectCommand.Parameters.AddWithValue("@id_ccDiv", Val(._txtDiv.Text))
                            Else
                                'Centro de Costo
                                sdaValAut.SelectCommand = New SqlCommand("select isnull(id_usr_director, 0) as id_usuario " + _
                                                                         "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                         "from bd_Empleado.dbo.cg_cc " + _
                                                                         "  left join cg_usuario on cg_cc.id_usr_director = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                                                                         "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                                                                         "where cg_cc.id_cc = @id_ccDiv " + _
                                                                         "  and cg_cc.status = 'A' ", ConexionBD)
                                sdaValAut.SelectCommand.Parameters.AddWithValue("@id_ccDiv", Val(._txtCC.Text))
                            End If
                        Case -4
                            ' Gerente División / CC
                            If Val(._txtCC.Text) = 0 Then
                                'División
                                sdaValAut.SelectCommand = New SqlCommand("select isnull(id_usr_gerente, 0) as id_usuario " + _
                                                                         "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                         "from bd_Empleado.dbo.cg_div " + _
                                                                         "  left join cg_usuario on cg_div.id_usr_gerente = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                                                                         "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                                                                         "where cg_div.id_div = @id_ccDiv " + _
                                                                         "  and cg_div.status = 'A' ", ConexionBD)
                                sdaValAut.SelectCommand.Parameters.AddWithValue("@id_ccDiv", Val(._txtDiv.Text))
                            Else
                                'Centro de Costo
                                sdaValAut.SelectCommand = New SqlCommand("select isnull(id_usr_gerente, 0) as id_usuario " + _
                                                                         "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                         "from bd_Empleado.dbo.cg_cc " + _
                                                                         "  left join cg_usuario on cg_cc.id_usr_gerente = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                                                                         "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                                                                         "where cg_cc.id_cc = @id_ccDiv " + _
                                                                         "  and cg_cc.status = 'A' ", ConexionBD)
                                sdaValAut.SelectCommand.Parameters.AddWithValue("@id_ccDiv", Val(._txtCC.Text))
                            End If
                        Case -3
                            ' Director Organigrama
                            sdaValAut.SelectCommand = New SqlCommand("select cgUsrA.id_usuario as id_usuario " + _
                                                                     "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                     "from cg_usuario cgUsr " + _
                                                                     "  left join dt_autorizador on cgUsr.id_usuario = dt_autorizador.id_usuario " + _
                                                                     "  left join cg_usuario cgUsrA on dt_autorizador.id_autorizador = cgUsrA.id_usuario and cgUsrA.status = 'A' " + _
                                                                     "  left join bd_Empleado.dbo.cg_empleado on cgUsrA.id_empleado = cg_empleado.id_empleado " + _
                                                                     "where cgUsr.id_usuario = @idUsuario " + _
                                                                     "  and dt_autorizador.aut_dir = 'S' ", ConexionBD)
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        Case -2
                            ' Gerente Organigrama
                            sdaValAut.SelectCommand = New SqlCommand("select cgUsrA.id_usuario as id_usuario " + _
                                                                     "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                     "from cg_usuario cgUsr " + _
                                                                     "  left join dt_autorizador on cgUsr.id_usuario = dt_autorizador.id_usuario " + _
                                                                     "  left join cg_usuario cgUsrA on dt_autorizador.id_autorizador = cgUsrA.id_usuario and cgUsrA.status = 'A' " + _
                                                                     "  left join bd_Empleado.dbo.cg_empleado on cgUsrA.id_empleado = cg_empleado.id_empleado " + _
                                                                     "where cgUsr.id_usuario = @idUsuario " + _
                                                                     "  and dt_autorizador.aut_dir = 'N' ", ConexionBD)
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        Case -1
                            ' x Organigrama
                            sdaValAut.SelectCommand = New SqlCommand("select cgUsrA.id_usuario as id_usuario " + _
                                                                     "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                     "from cg_usuario cgUsr " + _
                                                                     "  left join dt_autorizador on cgUsr.id_usuario = dt_autorizador.id_usuario " + _
                                                                     "  left join cg_usuario cgUsrA on dt_autorizador.id_autorizador = cgUsrA.id_usuario and cgUsrA.status = 'A' " + _
                                                                     "  left join bd_Empleado.dbo.cg_empleado on cgUsrA.id_empleado = cg_empleado.id_empleado " + _
                                                                     "where cgUsr.id_usuario = @idUsuario ", ConexionBD)
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        Case 0
                            ' Lista Pre-definida
                            sdaValAut.SelectCommand = New SqlCommand("select cg_usuario.id_usuario as id_usuario " + _
                                                                     "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                     "from dt_servicio_aut " + _
                                                                     "  left join cg_usuario on dt_servicio_aut.id_usuario = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                                                                     "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                                                                     "where id_dt_servicio_conf = @idDtServicio " + _
                                                                     "  and tipo = @tipo ", ConexionBD)
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@idDtServicio", Val(._txtIdDtServ.Text))
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@tipo", tipo)
                        Case Else
                            ' Usuario Específico
                            sdaValAut.SelectCommand = New SqlCommand("select cgUsrA.id_usuario as id_usuario " + _
                                                                     "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                     "from cg_usuario cgUsrA " + _
                                                                     "  left join bd_Empleado.dbo.cg_empleado on cgUsrA.id_empleado = cg_empleado.id_empleado " + _
                                                                     "where cgUsrA.id_usuario = @idValAut " + _
                                                                     "  and cgUsrA.status = 'A' ", ConexionBD)
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@idValAut", idUsr)
                    End Select
                    lista.DataTextField = "empleado"
                    lista.DataValueField = "id_usuario"
                    ConexionBD.Open()
                    sdaValAut.Fill(dsValAut)
                    lista.DataBind()
                    ConexionBD.Close()
                    sdaValAut.Dispose()
                    dsValAut.Dispose()
                    lista.SelectedIndex = -1
                    upLista.Update()
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub buscarAF()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaAF As New SqlDataAdapter
                Dim dsAF As New DataSet
                sdaAF.SelectCommand = New SqlCommand("select id_dt_equipo, codigo + ' [' + descripcion + ']' as equipo " + _
                                                     "from bd_SiRAc.dbo.dt_equipo " + _
                                                     "  left join bd_SiRAc.dbo.cg_equipo on dt_equipo.id_equipo = cg_equipo.id_equipo " + _
                                                     "where dt_equipo.status = 'A' " + _
                                                     "  and codigo not in (select distinct(codigo) from dt_af) " + _
                                                     "  and (codigo like '%' + @valor + '%' " + _
                                                     "    or descripcion like '%' + @valor + '%') ", ConexionBD)
                sdaAF.SelectCommand.Parameters.AddWithValue("@valor", .txtAF.Text.Trim)
                .ddlAF.DataSource = dsAF
                .ddlAF.DataTextField = "equipo"
                .ddlAF.DataValueField = "id_dt_equipo"
                ConexionBD.Open()
                sdaAF.Fill(dsAF)
                .ddlAF.DataBind()
                ConexionBD.Close()
                sdaAF.Dispose()
                dsAF.Dispose()
                .ddlAF.SelectedIndex = -1
                .upDdlAF.Update()
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

    Public Sub actualizarEvidencias()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaArchivos As New SqlDataAdapter
                Dim dsArchivos As New DataSet
                .gvEvidencias.DataSource = dsArchivos
                'Evidencias
                sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " + _
                                                           "     , 'http://148.223.153.43/ProcAd - Adjuntos IngFact/' + cast(id_dt_archivo as varchar(20)) + '-' + nombre as path " + _
                                                           "from dt_archivo " + _
                                                           "where id_ms_factura = @idMsFactura " + _
                                                           "  and tipo = 'E' ", ConexionBD)
                sdaArchivos.SelectCommand.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaArchivos.Fill(dsArchivos)
                .gvEvidencias.DataBind()
                ConexionBD.Close()
                sdaArchivos.Dispose()
                dsArchivos.Dispose()
                .gvEvidencias.SelectedIndex = -1
                .upEvidencias.Update()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub actualizarAF()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaActivos As New SqlDataAdapter
                Dim dsActivos As New DataSet
                .gvAF.DataSource = dsActivos
                'Activos Fijos
                sdaActivos.SelectCommand = New SqlCommand("select codigo as no_economico " + _
                                                          "     , descripcion " + _
                                                          "from dt_af " + _
                                                          "where id_ms_factura = @idMsFactura ", ConexionBD)
                sdaActivos.SelectCommand.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaActivos.Fill(dsActivos)
                .gvAF.DataBind()
                ConexionBD.Close()
                sdaActivos.Dispose()
                dsActivos.Dispose()
                .upAF.Update()

                If .gvAF.Rows.Count > 0 Then
                    .cbAF.Checked = True
                    .pnlFiltroAF.Visible = True
                    .upFiltroAF.Update()
                    .gvAF.Visible = True
                    .upAF.Update()
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Evidencias / Adjuntos"

    Protected Sub gvFacturas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvFacturas.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""

                'Tope 1
                If Val(.gvFacturas.SelectedRow.Cells(11).Text) >= Val(._txtTope1.Text) Then
                    .lbl_Autorizador2.Visible = True
                    .up_Autorizador2.Update()
                    .ddlAutorizador2.Visible = True
                    .upAutorizador2.Update()
                Else
                    .lbl_Autorizador2.Visible = False
                    .up_Autorizador2.Update()
                    .ddlAutorizador2.Visible = False
                    .upAutorizador2.Update()
                End If
                'Tope 2
                If Val(.gvFacturas.SelectedRow.Cells(11).Text) >= Val(._txtTope2.Text) Then
                    .lbl_Autorizador3.Visible = True
                    .up_Autorizador3.Update()
                    .ddlAutorizador3.Visible = True
                    .upAutorizador3.Update()
                Else
                    .lbl_Autorizador3.Visible = False
                    .up_Autorizador3.Update()
                    .ddlAutorizador3.Visible = False
                    .upAutorizador3.Update()
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnAgregarEvid_Click(sender As Object, e As EventArgs) Handles btnAgregarEvid.Click
        With Me
            Try
                .litError.Text = ""

                ' '' Ruta Local
                ''Dim sFileDir As String = "C:/ProcAd - Adjuntos IngFact/" 'Ruta en que se almacenará el archivo
                ' Ruta en Atenea
                Dim sFileDir As String = "D:\ProcAd - Adjuntos IngFact\" 'Ruta en que se almacenará el archivo
                Dim lMaxFileSize As Long = 10485760 'Equivalente a 10 Megabytes

                'Verificar que el archivo ha sido seleccionado y es un archivo válido
                If (Not fuEvidencia.PostedFile Is Nothing) And (fuEvidencia.PostedFile.ContentLength > 0) Then
                    'Determinar el nombre original del archivo
                    Dim sFileName As String = System.IO.Path.GetFileName(fuEvidencia.PostedFile.FileName)
                    Dim idArchivo As Integer 'Index correspondiente al archivo
                    Dim fecha As DateTime = Date.Now
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    Try
                        If fuEvidencia.PostedFile.ContentLength <= lMaxFileSize Then
                            'Registrar el archivo en la base de datos
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "INSERT INTO dt_archivo(id_ms_factura, id_actividad, id_usuario, nombre, fecha, tipo) values(@id_ms_factura, 51, @id_usuario, @nombre, @fecha, @tipo)"
                            SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                            SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                            SCMValores.Parameters.AddWithValue("@nombre", sFileName)
                            SCMValores.Parameters.AddWithValue("@fecha", fecha)
                            SCMValores.Parameters.AddWithValue("@tipo", "E")
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()
                            'Obtener el Id del archivo
                            SCMValores.CommandText = "select max(id_dt_archivo) from dt_archivo where (id_ms_factura = @id_ms_factura) and (fecha = @fecha)"
                            ConexionBD.Open()
                            idArchivo = SCMValores.ExecuteScalar
                            ConexionBD.Close()
                            'Se agrega el Id al nombre del archivo
                            sFileName = idArchivo.ToString + "-" + sFileName
                            'Almacenar el archivo en la ruta especificada
                            fuEvidencia.PostedFile.SaveAs(sFileDir + sFileName)
                            'lblMessage.Visible = True
                            'lblMessage.Text = "El archivo: " + sFileName + " se ha adjuntado exitosamente"

                            actualizarEvidencias()
                        Else
                            'Rechazar el archivo
                            lblMessage.Visible = True
                            lblMessage.Text = "El archivo excede el límite de 10 MB"
                        End If
                    Catch exc As Exception    'En caso de error
                        'Eliminar el archivo en la base de datos
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "delete from dt_archivo where id_dt_archivo = @idArchivo"
                        SCMValores.Parameters.AddWithValue("@idArchivo", idArchivo)
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        lblMessage.Visible = True
                        lblMessage.Text = lblMessage.Text + ". Un Error ha ocurrido. Favor de intentarlo nuevamente"
                        DeleteFile(sFileDir + sFileName)
                    End Try
                Else
                    lblMessage.Visible = True
                    lblMessage.Text = "Favor de seleccionar un Archivo"
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub cbAF_CheckedChanged(sender As Object, e As EventArgs) Handles cbAF.CheckedChanged
        With Me
            If .cbAF.Checked = True Then
                .pnlFiltroAF.Visible = True
                .upFiltroAF.Update()
                .gvAF.Visible = True
                .upAF.Update()
            Else
                .pnlFiltroAF.Visible = False
                .upFiltroAF.Update()
                .gvAF.Visible = False
                .upAF.Update()
            End If
        End With
    End Sub

    Protected Sub ibtnBuscarAF_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBuscarAF.Click
        buscarAF()
    End Sub

    Protected Sub btnAgregarAF_Click(sender As Object, e As EventArgs) Handles btnAgregarAF.Click
        With Me
            Try
                .litError.Text = ""

                If .ddlAF.Items.Count > 0 Then
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD

                    Dim sdaAF As New SqlDataAdapter
                    Dim dsAF As New DataSet
                    sdaAF.SelectCommand = New SqlCommand("select codigo, descripcion " + _
                                                         "from bd_SiRAc.dbo.dt_equipo " + _
                                                         "  left join bd_SiRAc.dbo.cg_equipo on dt_equipo.id_equipo = cg_equipo.id_equipo " + _
                                                         "where id_dt_equipo = @idDtActivo ", ConexionBD)
                    sdaAF.SelectCommand.Parameters.AddWithValue("@idDtActivo", .ddlAF.SelectedValue)
                    ConexionBD.Open()
                    sdaAF.Fill(dsAF)
                    ConexionBD.Close()

                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into dt_af ( id_ms_factura,  id_usuario,  id_dt_equipo,  codigo,  descripcion) " + _
                                             " 		     values (@id_ms_factura, @id_usuario, @id_dt_equipo, @codigo, @descripcion)"
                    SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                    SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                    SCMValores.Parameters.AddWithValue("@id_dt_equipo", .ddlAF.SelectedValue)
                    SCMValores.Parameters.AddWithValue("@codigo", dsAF.Tables(0).Rows(0).Item("codigo").ToString())
                    SCMValores.Parameters.AddWithValue("@descripcion", dsAF.Tables(0).Rows(0).Item("descripcion").ToString())
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    actualizarAF()

                    buscarAF()
                Else
                    .litError.Text = "Favor de seleccionar un activo válido"
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Guardar"

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        With Me
            Try
                .litError.Text = ""

                If .gvFacturas.Rows.Count = 0 Or .gvFacturas.SelectedIndex = -1 Then
                    .litError.Text = "Favor de seleccionar la factura"
                Else
                    If .ddlAutorizador2.Visible = True And .ddlAutorizador2.SelectedValue = 0 Then
                        .litError.Text = "Favor de seleccionar al Segundo Autorizador"
                    Else
                        actualizarEvidencias()
                        actualizarAF()

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        Dim banFecha As Integer
                        Dim fecha As DateTime = Date.Now

                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "select case when (select factura_emi_prev " + _
                                                 "                  from cg_usuario " + _
                                                 "                  where id_usuario = @id_usr) = 'S' " + _
                                                 "         then 0 " + _
                                                 "         else case when (select fecha_solicita " + _
                                                 "                         from ms_factura " + _
                                                 "                         where id_ms_factura = @id_ms_factura) > (select fecha_emision " + _
                                                 "                                                                  from dt_factura " + _
                                                 "                                                                  where uuid = @CFDI " + _
                                                 "                                                                    and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA')) " + _
                                                 "                then 1 " + _
                                                 "                else 0 " + _
                                                 "              end " + _
                                                 "       end as banFecha "
                        SCMValores.Parameters.AddWithValue("@CFDI", .gvFacturas.SelectedRow.Cells(3).Text)
                        SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                        SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        banFecha = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        If banFecha = 1 Then
                            .litError.Text = "La fecha de emisión de la factura es previa a la fecha de solicitud, por lo que no procede; favor de validarlo"
                        Else
                            Dim sdaEmpleado As New SqlDataAdapter
                            Dim dsEmpleado As New DataSet
                            sdaEmpleado.SelectCommand = New SqlCommand("select cgUsrA.id_usuario as id_usr_aut " + _
                                                                       "    , cgAut.no_empleado as no_empleadoA " + _
                                                                       "    , cgAut.nombre + ' ' + cgAut.ap_paterno + ' ' + cgAut.ap_materno as Autorizador " + _
                                                                       "from bd_Empleado.dbo.cg_empleado cgAut " + _
                                                                       "  left join cg_usuario cgUsrA on cgAut.id_empleado = cgUsrA.id_empleado " + _
                                                                       "where cgUsrA.id_usuario = @idAut ", ConexionBD)
                            sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idAut", .ddlAutorizador.SelectedValue)
                            ConexionBD.Open()
                            sdaEmpleado.Fill(dsEmpleado)
                            ConexionBD.Close()

                            'Actualizar datos de la Solicitud
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "update ms_factura " + _
                                                     "  set id_usr_autoriza = @id_usr_autoriza, no_autorizador = @no_autorizador, autorizador = @autorizador, id_usr_autoriza2 = @id_usr_autoriza2, id_usr_autoriza3 = @id_usr_autoriza3, CFDI = @CFDI, importe_tot = @importe_tot, status = 'PA' " + _
                                                     "where id_ms_factura = @id_ms_factura "
                            SCMValores.Parameters.AddWithValue("@id_usr_autoriza", dsEmpleado.Tables(0).Rows(0).Item("id_usr_aut").ToString())
                            SCMValores.Parameters.AddWithValue("@no_autorizador", dsEmpleado.Tables(0).Rows(0).Item("no_empleadoA").ToString())
                            SCMValores.Parameters.AddWithValue("@autorizador", dsEmpleado.Tables(0).Rows(0).Item("Autorizador").ToString())
                            If .ddlAutorizador2.Visible = True Then
                                SCMValores.Parameters.AddWithValue("@id_usr_autoriza2", .ddlAutorizador2.SelectedValue)
                            Else
                                SCMValores.Parameters.AddWithValue("@id_usr_autoriza2", DBNull.Value)
                            End If
                            If .ddlAutorizador3.Visible = True Then
                                SCMValores.Parameters.AddWithValue("@id_usr_autoriza3", .ddlAutorizador3.SelectedValue)
                            Else
                                SCMValores.Parameters.AddWithValue("@id_usr_autoriza3", DBNull.Value)
                            End If
                            SCMValores.Parameters.AddWithValue("@CFDI", .gvFacturas.SelectedRow.Cells(3).Text)
                            SCMValores.Parameters.AddWithValue("@importe_tot", Val(.gvFacturas.SelectedRow.Cells(11).Text))
                            SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            If ._txtCFDI.Text <> .gvFacturas.SelectedRow.Cells(3).Text Then
                                'Actualizar dt_factura - Liberar Factura Anterior
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "update dt_factura " + _
                                                         "  set status = 'P' " + _
                                                         "where uuid = @uuid " + _
                                                         "  and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') "
                                SCMValores.Parameters.AddWithValue("@uuid", ._txtCFDI.Text)
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()

                                'Actualizar dt_factura - Reservar Nueva Factura
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "update dt_factura " + _
                                                         "  set status = 'As' " + _
                                                         "where uuid = @uuid " + _
                                                         "  and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') "
                                SCMValores.Parameters.AddWithValue("@uuid", .gvFacturas.SelectedRow.Cells(3).Text)
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()
                            End If


                            'Actualizar Instancia
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                            SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                            SCMValores.Parameters.AddWithValue("@id_actividad", 50)
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            'Registrar en Histórico
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                                     "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                            SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                            SCMValores.Parameters.AddWithValue("@id_actividad", 50)
                            SCMValores.Parameters.AddWithValue("@fecha", fecha)
                            SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            'Envío de Correo
                            Dim Mensaje As New System.Net.Mail.MailMessage()
                            Dim destinatario As String = ""
                            'Obtener el Correos del Autorizador
                            SCMValores.CommandText = "select cgEmpl.correo from cg_usuario left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado where cg_usuario.id_usuario = @idAut "
                            SCMValores.Parameters.AddWithValue("@idAut", .ddlAutorizador.SelectedValue)
                            ConexionBD.Open()
                            destinatario = SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            Mensaje.[To].Add(destinatario)
                            Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                            Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                            Mensaje.Subject = "ProcAd - Solicitud de Factura No. " + .lblFolio.Text + " por Autorizar"
                            Dim texto As String
                            texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                                    "Se ingresaron las cotizaciones de la solicitud número <b>" + .lblFolio.Text + _
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

                            .pnlInicio.Enabled = False
                        End If
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim fecha As DateTime = Date.Now
                While Val(._txtBan.Text) = 0
                    'Actualizar datos de la Solicitud
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_factura set status = 'ZU' where id_ms_factura = @id_ms_factura "
                    SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Actualizar dt_factura
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update dt_factura " + _
                                             "  set status = 'P' " + _
                                             "where uuid = @uuid " + _
                                             "  and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') "
                    SCMValores.Parameters.AddWithValue("@uuid", ._txtCFDI.Text)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    ._txtBan.Text = 1

                    'Actualizar Instancia
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 28)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Registrar en Histórico
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                             "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 28)
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    .pnlInicio.Enabled = False
                End While
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class