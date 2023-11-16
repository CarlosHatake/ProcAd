Public Class _43
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 1359

                    ' Lic Marco 45 
                    ' Luis 1359  
                    ' Ing Ruben 14

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
                        SCMValores.CommandText = "select nombre + ' ' + ap_paterno + ' ' + ap_materno " +
                                                 "from cg_usuario " +
                                                 "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                 "where id_usuario = @idUsuario "
                        SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        .lblSolicitante.Text = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

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

                        'Lista de Empresas
                        Dim sdaEmpresa As New SqlDataAdapter
                        Dim dsEmpresa As New DataSet
                        sdaEmpresa.SelectCommand = New SqlCommand("select id_empresa, nombre " +
                                                                  "from bd_empleado.dbo.cg_empresa Empresa " +
                                                                  "where status = 'A' " +
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
                        'División
                        llenarDiv()

                        .rblAdmonOper.SelectedIndex = 0

                        'Lista de Tipos de Servicios
                        Dim sdaTipoServ As New SqlDataAdapter
                        Dim dsTipoServ As New DataSet
                        sdaTipoServ.SelectCommand = New SqlCommand("select distinct(tipo_servicio) " +
                                                                   "from cg_servicio " +
                                                                   "where status = 'A' " +
                                                                   "  and tipo_servicio <> 'Servicios Negociados' " +
                                                                   "order by tipo_servicio  ASC", ConexionBD)
                        .ddlTipoServicio.DataSource = dsTipoServ
                        .ddlTipoServicio.DataTextField = "tipo_servicio"
                        .ddlTipoServicio.DataValueField = "tipo_servicio"
                        ConexionBD.Open()
                        sdaTipoServ.Fill(dsTipoServ)
                        .ddlTipoServicio.DataBind()
                        ConexionBD.Close()
                        sdaTipoServ.Dispose()
                        dsTipoServ.Dispose()
                        llenarServicio()

                        'Eliminar registro de Adjuntos / Evidencias no almacenados previamente
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "DELETE from dt_archivo where id_ms_factura = -1 and id_usuario = @id_usuario"
                        SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Eliminar registro de Activos Fijos no almacenados previamente
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "DELETE from dt_af where id_ms_factura = -1 and id_usuario = @id_usuario"
                        SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Botones
                        .pnlFiltroAF.Visible = False
                        .upFiltroAF.Update()
                        .gvAF.Visible = False
                        .upAF.Update()

                        ''.btnGuardar.Enabled = True
                        ._txtBanConfig.Text = 0

                        'Ocultar panel dividir de factura 
                        .pnlDividirFactura.Visible = False
                        .pnlgvFacturaDividida.Visible = False

                        'Campos de división de factura
                        habilitarCampos(True)
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
                SCMValores.CommandText = "select rfc " +
                                         "from bd_Empleado.dbo.cg_empresa " +
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

    Public Sub llenarServicio()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaServicio As New SqlDataAdapter
                Dim dsServicio As New DataSet
                sdaServicio.SelectCommand = New SqlCommand("select id_servicio " +
                                                           "     , servicio " +
                                                           "from cg_servicio " +
                                                           "where status = 'A' " +
                                                           "  and tipo_servicio = @tipo_servicio " +
                                                           "order by servicio ASC ", ConexionBD)
                sdaServicio.SelectCommand.Parameters.AddWithValue("@tipo_servicio", .ddlTipoServicio.SelectedValue)
                .ddlServicio.DataSource = dsServicio
                .ddlServicio.DataTextField = "servicio"
                .ddlServicio.DataValueField = "id_servicio"
                ConexionBD.Open()
                sdaServicio.Fill(dsServicio)
                .ddlServicio.DataBind()
                ConexionBD.Close()
                sdaServicio.Dispose()
                dsServicio.Dispose()
                .ddlServicio.SelectedIndex = -1
                .upServicio.Update()

                configServ()
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
                sdaCC.SelectCommand = New SqlCommand("select 0 as id_cc " +
                                                     "     , ' ' as nombre " +
                                                     "union " +
                                                     "select id_cc " +
                                                     "     , nombre " +
                                                     "from bd_Empleado.dbo.cg_cc " +
                                                     "where id_empresa = @idEmpresa " +
                                                     "  and status = 'A' " +
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
                .upCC.Update()
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
                                                      "where id_empresa = @idEmpresa " +
                                                      "  and status = 'A' " +
                                                      "order by nombre ", ConexionBD)
                sdaDiv.SelectCommand.Parameters.AddWithValue("@idEmpresa", .ddlEmpresa.SelectedValue)
                .ddlDiv.DataSource = dsDiv
                .ddlDiv.DataTextField = "nombre"
                .ddlDiv.DataValueField = "id_div"
                ConexionBD.Open()
                sdaDiv.Fill(dsDiv)
                .ddlDiv.DataBind()
                ConexionBD.Close()
                sdaDiv.Dispose()
                dsDiv.Dispose()
                .ddlDiv.SelectedIndex = -1
                .upDiv.Update()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarDivD()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaDiv As New SqlDataAdapter
                Dim dsDiv As New DataSet
                sdaDiv.SelectCommand = New SqlCommand(" select codigo, nombre " +
                                                      " from bd_Empleado.dbo.cg_div " +
                                                      " where id_empresa = @idEmpresa " +
                                                      " and status = 'A' order by nombre  ", ConexionBD)

                sdaDiv.SelectCommand.Parameters.AddWithValue("@idEmpresa", .ddlEmpresa.SelectedValue)
                .ddlDivD.DataSource = dsDiv
                .ddlDivD.DataTextField = "nombre"
                .ddlDivD.DataValueField = "codigo"
                ConexionBD.Open()
                sdaDiv.Fill(dsDiv)
                .ddlDivD.DataBind()
                ConexionBD.Close()
                sdaDiv.Dispose()
                dsDiv.Dispose()

                .ddlDivD.Items.Insert(0, "")
                .txtDiv.Text = .ddlDivD.SelectedValue
                .upDivD.Update()
                .upDivDDl.Update()



            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub configServ()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim contConfig As Integer
                'Validar que exista la configuración
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select count(*) " +
                                         "from cg_servicio " +
                                         "  left join dt_servicio_conf on cg_servicio.id_servicio = dt_servicio_conf.id_servicio " +
                                         "where cg_servicio.id_servicio = @id_servicio " +
                                         "  and (admon_oper = @admon_oper or admon_oper = 'Ambos') " +
                                         "  and (id_empresa = @id_empresa or id_empresa = 0) "
                SCMValores.Parameters.AddWithValue("@id_servicio", .ddlServicio.SelectedValue)
                SCMValores.Parameters.AddWithValue("@admon_oper", .rblAdmonOper.SelectedValue)
                SCMValores.Parameters.AddWithValue("@id_empresa", .ddlEmpresa.SelectedValue)
                ConexionBD.Open()
                contConfig = SCMValores.ExecuteScalar()
                ConexionBD.Close()

                'Adjuntos Requeridos
                Dim sdaAdjReq As New SqlDataAdapter
                Dim dsAdjReq As New DataSet
                .gvAdjuntosReq.DataSource = dsAdjReq
                sdaAdjReq.SelectCommand = New SqlCommand("select adjunto " +
                                                         "from dt_servicio_adj " +
                                                         "where id_servicio = @id_servicio ", ConexionBD)
                sdaAdjReq.SelectCommand.Parameters.AddWithValue("@id_servicio", .ddlServicio.SelectedValue)
                ConexionBD.Open()
                sdaAdjReq.Fill(dsAdjReq)
                .gvAdjuntosReq.DataBind()
                ConexionBD.Close()
                sdaAdjReq.Dispose()
                dsAdjReq.Dispose()
                .gvAdjuntosReq.SelectedIndex = -1
                .upAdjuntosReq.Update()

                If .gvAdjuntosReq.Rows.Count = 0 Then
                    .lbl_AdjuntoReq.Visible = False
                    .up_AdjuntosReq.Update()
                    .gvAdjuntosReq.Visible = False
                Else
                    .lbl_AdjuntoReq.Visible = True
                    .up_AdjuntosReq.Update()
                    .gvAdjuntosReq.Visible = True
                End If
                .upAdjuntosReq.Update()

                'No permite manejo de Contratos
                .cbAltaContrato.Visible = False
                .upCbAltaContrato.Update()
                .cbContratoNav.Visible = False
                .upCbContratoNav.Update()
                .pnlFiltroContrato.Visible = False
                .upFiltroContrato.Update()
                'Validador 1
                actualizarValAut(-99, "V1", .lbl_Validador, .up_Validador, .ddlValidador, .upValidador)
                'Se ocultan lista de proveedores y panel de facturas
                .lbl_Proveedor.Visible = False
                .up_Proveedor.Update()
                .pnlProveedor.Visible = False
                .upProveedor.Update()
                .pnlFactura.Visible = False
                .upFactura.Update()


                If contConfig = 0 Then
                    .litError.Text = "No existe Configuración del Servicio, favor de solicitarlo al Administrador de Catálogos"
                    ''.btnGuardar.Enabled = False
                    ._txtBanConfig.Text = 1
                Else
                    If contConfig > 1 Then
                        .litError.Text = "Error en Configuración del Servicio, favor de validarlo con el Administrador de Catálogos"
                        ''.btnGuardar.Enabled = False
                        ._txtBanConfig.Text = 1
                    Else
                        Dim ban As Integer = 0

                        If .ddlCC.Items.Count = 0 And .ddlDiv.Items.Count = 0 Then
                            .litError.Text = "No existen Centros de Costo ni Divisiones, favor de validarlo con la Administradora de Catálogos"
                            ban = 1
                        Else
                            If .ddlCC.Items.Count > 0 And .ddlDiv.Items.Count > 0 Then
                                'CC y Div
                                If .ddlCC.SelectedValue = 0 And .ddlDiv.SelectedValue = 0 Then
                                    .litError.Text = "Favor de seleccionar el Centro de Costo o División correspondiente"
                                    ban = 1
                                End If
                            Else
                                If .ddlCC.Items.Count > 0 Then
                                    'Solo CC
                                    If .ddlCC.SelectedValue = 0 Then
                                        .litError.Text = "Favor de seleccionar el Centro de Costo correspondiente"
                                        ban = 1
                                    End If
                                Else
                                    'Solo Div
                                    If .ddlDiv.SelectedValue = 0 Then
                                        .litError.Text = "Favor de seleccionar la División correspondiente"
                                        ban = 1
                                    End If
                                End If
                            End If
                        End If

                        If ban = 0 Then
                            'Configuración del Servicio
                            Dim sdaTipoServ As New SqlDataAdapter
                            Dim dsTipoServ As New DataSet
                            sdaTipoServ.SelectCommand = New SqlCommand("select dt_servicio_conf.id_dt_servicio_conf " +
                                                                       "     , cg_servicio.cotizaciones " +
                                                                       "     , cg_servicio.cotizacion_unica " +
                                                                       "     , cg_servicio.contrato " +
                                                                       "     , cg_servicio.servicio_negociado " +
                                                                       "     , cuenta_cont " +
                                                                       "     , isnull(id_usr_valida, -99) as id_usr_valida1 " +
                                                                       "     , isnull(id_usr_valida2, -99) as id_usr_valida2 " +
                                                                       "     , finanzas " +
                                                                       "     , valida_presup " +
                                                                       "     , id_usr_autoriza1 " +
                                                                       "     , id_usr_autoriza2 " +
                                                                       "     , id_usr_autoriza3 " +
                                                                       "from cg_servicio " +
                                                                       "  left join dt_servicio_conf on cg_servicio.id_servicio = dt_servicio_conf.id_servicio " +
                                                                       "where cg_servicio.id_servicio = @id_servicio " +
                                                                       "  and (admon_oper = @admon_oper or admon_oper = 'Ambos') " +
                                                                       "  and (id_empresa = @id_empresa or id_empresa = 0) ", ConexionBD)
                            sdaTipoServ.SelectCommand.Parameters.AddWithValue("@id_servicio", .ddlServicio.SelectedValue)
                            sdaTipoServ.SelectCommand.Parameters.AddWithValue("@admon_oper", .rblAdmonOper.SelectedValue)
                            sdaTipoServ.SelectCommand.Parameters.AddWithValue("@id_empresa", .ddlEmpresa.SelectedValue)
                            ConexionBD.Open()
                            sdaTipoServ.Fill(dsTipoServ)
                            ConexionBD.Close()
                            ._txtIdDtServ.Text = dsTipoServ.Tables(0).Rows(0).Item("id_dt_servicio_conf").ToString()
                            ._txtCotizaciones.Text = dsTipoServ.Tables(0).Rows(0).Item("cotizaciones").ToString()
                            ._txtCotUnica.Text = dsTipoServ.Tables(0).Rows(0).Item("cotizacion_unica").ToString()
                            ._txtContrato.Text = dsTipoServ.Tables(0).Rows(0).Item("contrato").ToString()
                            ._txtServNeg.Text = dsTipoServ.Tables(0).Rows(0).Item("servicio_negociado").ToString()
                            ._txtCuentaCont.Text = dsTipoServ.Tables(0).Rows(0).Item("cuenta_cont").ToString()
                            ._txtIdValidador.Text = dsTipoServ.Tables(0).Rows(0).Item("id_usr_valida1").ToString()
                            ._txtIdValidador2.Text = dsTipoServ.Tables(0).Rows(0).Item("id_usr_valida2").ToString()
                            ._txtFinanzas.Text = dsTipoServ.Tables(0).Rows(0).Item("finanzas").ToString()
                            ._txtValPresup.Text = dsTipoServ.Tables(0).Rows(0).Item("valida_presup").ToString()
                            ._txtIdAutorizador1.Text = dsTipoServ.Tables(0).Rows(0).Item("id_usr_autoriza1").ToString()
                            ._txtIdAutorizador2.Text = dsTipoServ.Tables(0).Rows(0).Item("id_usr_autoriza2").ToString()
                            ._txtIdAutorizador3.Text = dsTipoServ.Tables(0).Rows(0).Item("id_usr_autoriza3").ToString()
                            sdaTipoServ.Dispose()
                            dsTipoServ.Dispose()

                            If ._txtContrato.Text = "S" Then
                                'Habilitar manejo de Contratos
                                .cbAltaContrato.Checked = False
                                .cbAltaContrato.Visible = True
                                .upCbAltaContrato.Update()
                                .cbContratoNav.Checked = False
                                .cbContratoNav.Visible = True
                                .upCbContratoNav.Update()
                            Else
                                'No permite manejo de Contratos
                                .cbAltaContrato.Visible = False
                                .upCbAltaContrato.Update()
                                .cbContratoNav.Visible = False
                                .upCbContratoNav.Update()
                            End If
                            .pnlFiltroContrato.Visible = False
                            .upFiltroContrato.Update()

                            'Validador 1
                            actualizarValAut(Val(._txtIdValidador.Text), "V1", .lbl_Validador, .up_Validador, .ddlValidador, .upValidador)

                            actualizarCamposFact()
                            ''.btnGuardar.Enabled = True
                            ._txtBanConfig.Text = 0
                        Else
                            ''.btnGuardar.Enabled = False
                            ._txtBanConfig.Text = 1
                        End If
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

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
                            sdaValAut.SelectCommand = New SqlCommand("select isnull(id_usr_director, 0) as id_usuario " +
                                                                     "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " +
                                                                     "from bd_Empleado.dbo.cg_empresa " +
                                                                     "  left join cg_usuario on cg_empresa.id_usr_director = cg_usuario.id_usuario and cg_usuario.status = 'A' " +
                                                                     "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " +
                                                                     "where id_empresa = @idEmpresa " +
                                                                     "  and cg_empresa.status = 'A' ", ConexionBD)
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@idEmpresa", .ddlEmpresa.SelectedValue)
                        Case -6
                            ' Gerente de Empresa
                            sdaValAut.SelectCommand = New SqlCommand("select isnull(id_usr_gerente, 0) as id_usuario " +
                                                                     "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " +
                                                                     "from bd_Empleado.dbo.cg_empresa " +
                                                                     "  left join cg_usuario on cg_empresa.id_usr_gerente = cg_usuario.id_usuario and cg_usuario.status = 'A' " +
                                                                     "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " +
                                                                     "where id_empresa = @idEmpresa " +
                                                                     "  and cg_empresa.status = 'A' ", ConexionBD)
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@idEmpresa", .ddlEmpresa.SelectedValue)
                        Case -5
                            ' Director División / CC
                            If .ddlCC.SelectedValue = 0 Then
                                'División
                                sdaValAut.SelectCommand = New SqlCommand("select isnull(id_usr_director, 0) as id_usuario " +
                                                                         "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " +
                                                                         "from bd_Empleado.dbo.cg_div " +
                                                                         "  left join cg_usuario on cg_div.id_usr_director = cg_usuario.id_usuario and cg_usuario.status = 'A' " +
                                                                         "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " +
                                                                         "where cg_div.id_div = @id_ccDiv " +
                                                                         "  and cg_div.status = 'A' ", ConexionBD)
                                sdaValAut.SelectCommand.Parameters.AddWithValue("@id_ccDiv", .ddlDiv.SelectedValue)
                            Else
                                'Centro de Costo
                                sdaValAut.SelectCommand = New SqlCommand("select isnull(id_usr_director, 0) as id_usuario " +
                                                                         "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " +
                                                                         "from bd_Empleado.dbo.cg_cc " +
                                                                         "  left join cg_usuario on cg_cc.id_usr_director = cg_usuario.id_usuario and cg_usuario.status = 'A' " +
                                                                         "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " +
                                                                         "where cg_cc.id_cc = @id_ccDiv " +
                                                                         "  and cg_cc.status = 'A' ", ConexionBD)
                                sdaValAut.SelectCommand.Parameters.AddWithValue("@id_ccDiv", .ddlCC.SelectedValue)
                            End If
                        Case -4
                            ' Gerente División / CC
                            If .ddlCC.SelectedValue = 0 Then
                                'División
                                sdaValAut.SelectCommand = New SqlCommand("select isnull(id_usr_gerente, 0) as id_usuario " +
                                                                         "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " +
                                                                         "from bd_Empleado.dbo.cg_div " +
                                                                         "  left join cg_usuario on cg_div.id_usr_gerente = cg_usuario.id_usuario and cg_usuario.status = 'A' " +
                                                                         "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " +
                                                                         "where cg_div.id_div = @id_ccDiv " +
                                                                         "  and cg_div.status = 'A' ", ConexionBD)
                                sdaValAut.SelectCommand.Parameters.AddWithValue("@id_ccDiv", .ddlDiv.SelectedValue)
                            Else
                                'Centro de Costo
                                sdaValAut.SelectCommand = New SqlCommand("select isnull(id_usr_gerente, 0) as id_usuario " +
                                                                         "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " +
                                                                         "from bd_Empleado.dbo.cg_cc " +
                                                                         "  left join cg_usuario on cg_cc.id_usr_gerente = cg_usuario.id_usuario and cg_usuario.status = 'A' " +
                                                                         "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " +
                                                                         "where cg_cc.id_cc = @id_ccDiv " +
                                                                         "  and cg_cc.status = 'A' ", ConexionBD)
                                sdaValAut.SelectCommand.Parameters.AddWithValue("@id_ccDiv", .ddlCC.SelectedValue)
                            End If
                        Case -3
                            ' Director Organigrama
                            sdaValAut.SelectCommand = New SqlCommand("select cgUsrA.id_usuario as id_usuario " +
                                                                     "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " +
                                                                     "from cg_usuario cgUsr " +
                                                                     "  left join dt_autorizador on cgUsr.id_usuario = dt_autorizador.id_usuario " +
                                                                     "  left join cg_usuario cgUsrA on dt_autorizador.id_autorizador = cgUsrA.id_usuario and cgUsrA.status = 'A' " +
                                                                     "  left join bd_Empleado.dbo.cg_empleado on cgUsrA.id_empleado = cg_empleado.id_empleado " +
                                                                     "where cgUsr.id_usuario = @idUsuario " +
                                                                     "  and dt_autorizador.aut_dir = 'S' ", ConexionBD)
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        Case -2
                            ' Gerente Organigrama
                            sdaValAut.SelectCommand = New SqlCommand("select cgUsrA.id_usuario as id_usuario " +
                                                                     "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " +
                                                                     "from cg_usuario cgUsr " +
                                                                     "  left join dt_autorizador on cgUsr.id_usuario = dt_autorizador.id_usuario " +
                                                                     "  left join cg_usuario cgUsrA on dt_autorizador.id_autorizador = cgUsrA.id_usuario and cgUsrA.status = 'A' " +
                                                                     "  left join bd_Empleado.dbo.cg_empleado on cgUsrA.id_empleado = cg_empleado.id_empleado " +
                                                                     "where cgUsr.id_usuario = @idUsuario " +
                                                                     "  and dt_autorizador.aut_dir = 'N' ", ConexionBD)
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        Case -1
                            ' x Organigrama
                            sdaValAut.SelectCommand = New SqlCommand("select cgUsrA.id_usuario as id_usuario " +
                                                                     "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " +
                                                                     "from cg_usuario cgUsr " +
                                                                     "  left join dt_autorizador on cgUsr.id_usuario = dt_autorizador.id_usuario " +
                                                                     "  left join cg_usuario cgUsrA on dt_autorizador.id_autorizador = cgUsrA.id_usuario and cgUsrA.status = 'A' " +
                                                                     "  left join bd_Empleado.dbo.cg_empleado on cgUsrA.id_empleado = cg_empleado.id_empleado " +
                                                                     "where cgUsr.id_usuario = @idUsuario ", ConexionBD)
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        Case 0
                            ' Lista Pre-definida
                            sdaValAut.SelectCommand = New SqlCommand("select cg_usuario.id_usuario as id_usuario " +
                                                                     "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " +
                                                                     "from dt_servicio_aut " +
                                                                     "  left join cg_usuario on dt_servicio_aut.id_usuario = cg_usuario.id_usuario and cg_usuario.status = 'A' " +
                                                                     "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " +
                                                                     "where id_dt_servicio_conf = @idDtServicio " +
                                                                     "  and tipo = @tipo ", ConexionBD)
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@idDtServicio", Val(._txtIdDtServ.Text))
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@tipo", tipo)
                        Case Else
                            ' Usuario Específico
                            sdaValAut.SelectCommand = New SqlCommand("select cgUsrA.id_usuario as id_usuario " +
                                                                     "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " +
                                                                     "from cg_usuario cgUsrA " +
                                                                     "  left join bd_Empleado.dbo.cg_empleado on cgUsrA.id_empleado = cg_empleado.id_empleado " +
                                                                     "where cgUsrA.id_usuario = @idValAut " +
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

    Public Sub actualizarCamposFact()
        With Me
            If ._txtCotizaciones.Text = "N" Or ._txtCotUnica.Text = "S" Or (.pnlFiltroContrato.Visible = True And .ddlContrato.Items.Count() > 0) Then
                'Se activa la pestaña de Proveedores
                .lbl_Proveedor.Visible = True
                .up_Proveedor.Update()

                .pnlProveedor.Visible = True
                .upProveedor.Update()
                'Limpiar Campos
                .txtProveedor.Text = ""
                llenarProv()
                .upProveedores.Update()

                'Determinar si se presenta el panel de facturas
                If ._txtCotUnica.Text = "S" Or (.pnlFiltroContrato.Visible = True And .ddlContrato.Items.Count() > 0) Then
                    'Lista de Autorizadores
                    'Autorizador 1
                    actualizarValAut(Val(._txtIdAutorizador1.Text), "A1", .lbl_Autorizador, .up_Autorizador, .ddlAutorizador, .upAutorizador)
                    'Autorizador 2
                    actualizarValAut(Val(._txtIdAutorizador2.Text), "A2", .lbl_Autorizador2, .up_Autorizador2, .ddlAutorizador2, .upAutorizador2)
                    'Autorizador 3
                    actualizarValAut(Val(._txtIdAutorizador3.Text), "A3", .lbl_Autorizador3, .up_Autorizador3, .ddlAutorizador3, .upAutorizador3)

                    .pnlFactura.Visible = True
                    llenarFacturas()
                Else
                    .pnlFactura.Visible = False
                End If
                .upFactura.Update()
            Else
                'Se ocultan lista de proveedores y panel de facturas
                .lbl_Proveedor.Visible = False
                .up_Proveedor.Update()
                .pnlProveedor.Visible = False
                .upProveedor.Update()
                .pnlFactura.Visible = False
                .upFactura.Update()
            End If
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
                query = "select distinct(rfc) as rfc_emisor, id_proveedor, rfc + ' / ' + nombre as proveedor " +
                        "from bd_SiSAC.dbo.cg_proveedor " +
                        "where (rfc <> '' and rfc is not null) " +
                        "  and (rfc like '%' + @valor+ '%' or nombre like '%' + @valor+ '%') "
                If (.pnlFiltroContrato.Visible = True And .ddlContrato.Items.Count() > 0) Then
                    query = query + "  and rfc = @rfcContrato "
                End If
                sdaProv.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaProv.SelectCommand.Parameters.AddWithValue("@valor", .txtProveedor.Text.Trim)
                If (.pnlFiltroContrato.Visible = True And .ddlContrato.Items.Count() > 0) Then
                    sdaProv.SelectCommand.Parameters.AddWithValue("@rfcContrato", ._txtRFCContrato.Text.Trim)
                End If
                .ddlProveedor.DataSource = dsProv
                .ddlProveedor.DataTextField = "proveedor"
                .ddlProveedor.DataValueField = "id_proveedor"
                ConexionBD.Open()
                sdaProv.Fill(dsProv)
                .ddlProveedor.DataBind()
                ConexionBD.Close()
                sdaProv.Dispose()
                dsProv.Dispose()
                .ddlProveedor.SelectedIndex = -1
                rfcProv()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub rfcProv()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select rfc " +
                                         "from  bd_SiSAC.dbo.cg_proveedor " +
                                         "where id_proveedor = @idProveedor "
                SCMValores.Parameters.AddWithValue("@idProveedor", .ddlProveedor.SelectedValue)
                ConexionBD.Open()
                ._txtRFCProv.Text = SCMValores.ExecuteScalar()
                ConexionBD.Close()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub rfcProvContrato()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("NAV")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select cgProveedor.[VAT Registration No_] " +
                                         "from [" + .ddlEmpresa.SelectedItem.Text + "$Contrato Compra] cgContrato " +
                                         "  left join [" + .ddlEmpresa.SelectedItem.Text + "$Vendor] cgProveedor on cgContrato.Proveedor = cgProveedor.[No_] " +
                                         "where cgContrato.Contrato = @contrato " +
                                         "  and cgContrato.Aprobado = 1 "
                SCMValores.Parameters.AddWithValue("@contrato", .ddlContrato.SelectedValue)
                ConexionBD.Open()
                ._txtRFCContrato.Text = SCMValores.ExecuteScalar()
                ConexionBD.Close()
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
                    '        "where estatus = 'VIGENTE' " + _
                    '        "  and movimiento in ('RECIBIDAS', 'RECIBIDA') " + _
                    '        "  and rfc_emisor = @rfcProv " + _
                    '        "  and rfc_receptor = @rfcEmpr " + _
                    '        "  and status = 'P' " + _
                    '        "  and (importe > 0 or rfc_emisor = 'ASE930924SS7') " + _
                    '        "  and ((select count(*) " + _
                    '        "        from cg_usuario " + _
                    '        "        where id_usuario = @idUsuario " + _
                    '        "          and factura_extemp = 'S') > 0 " + _
                    '        "       or " + _
                    '        "	   (case when (month(@fecha) = 12 and day(@fecha) < 25) then 0 " + _
                    '        "          else case when (day(@fecha) = 1 and (fecha_emision >= (dateadd(day, -5, convert(date, @fecha))))) then 0 " + _
                    '        "                 else case when month(fecha_emision) = month(@fecha) then 0 " + _
                    '        "                        else 1 " + _
                    '        "                      end " + _
                    '        "               end " + _
                    '        "        end = 0 " + _
                    '        "		and year(fecha_emision) = year(@fecha)) " + _
                    '        "       or " + _
                    '        "	   (case when (month(@fecha) = 1 and day(@fecha) < 4 and month(fecha_emision) = 12) then 0 " + _
                    '        "          else 1 " + _
                    '        "        end = 0) " + _
                    '        "        ) " + _
                    '        "  and year(fecha_emision) >= (select valor from cg_parametros where parametro = 'año_emision') " + _
                    '        "order by fecha_emision "

                    'Nueva versión Procedimiento Almacenado Carlos Hernández 19 Jul 22
                    sdaFacturas.SelectCommand = New SqlCommand("SP_C_dt_facturaLlenado @rfcProv, @rfcEmpr, @cfdi, @idUsuario, @fecha ", ConexionBD)

                    sdaFacturas.SelectCommand.Parameters.AddWithValue("@rfcProv", ._txtRFCProv.Text)
                    sdaFacturas.SelectCommand.Parameters.AddWithValue("@rfcEmpr", ._txtRFCEmpr.Text)
                    sdaFacturas.SelectCommand.Parameters.AddWithValue("@cfdi", DBNull.Value)
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

                    'Ocultar campos de Autorizadores 2 y 3
                    .lbl_Autorizador2.Visible = False
                    .up_Autorizador2.Update()
                    .ddlAutorizador2.Visible = False
                    .upAutorizador2.Update()
                    .lbl_Autorizador3.Visible = False
                    .up_Autorizador3.Update()
                    .ddlAutorizador3.Visible = False
                    .upAutorizador3.Update()

                Else
                    .gvFacturas.DataBind()
                End If
                If .gvFacturas.Rows.Count > 0 Then
                    .gvFacturas.Visible = True
                Else
                    .gvFacturas.Visible = False
                    .litError.Text = "No existen registros de ese Proveedor, favor de validarlo."
                End If
                If .ddlTipoServicio.SelectedIndex = 0 Then
                    .gvFacturas.Columns(2).Visible = False
                End If
                .upFacturas.Update()
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
                sdaAF.SelectCommand = New SqlCommand("select id_dt_equipo, codigo + ' [' + descripcion + ']' as equipo " +
                                                     "from bd_SiRAc.dbo.dt_equipo " +
                                                     "  left join bd_SiRAc.dbo.cg_equipo on dt_equipo.id_equipo = cg_equipo.id_equipo " +
                                                     "where dt_equipo.status = 'A' " +
                                                     "  and codigo not in (select distinct(codigo) from dt_af) " +
                                                     "  and (codigo like '%' + @valor + '%' " +
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

    Public Sub actualizarAdjuntos()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaArchivos As New SqlDataAdapter
                Dim dsArchivos As New DataSet
                .gvAdjuntos.DataSource = dsArchivos
                'Adjuntos
                sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " +
                                                           "     , 'http://148.223.153.43/ProcAd - Adjuntos IngFact/' + cast(id_dt_archivo as varchar(20)) + '-' + nombre as path " +
                                                           "from dt_archivo " +
                                                           "where id_ms_factura = -1 " +
                                                           "  and tipo = 'A' " +
                                                           "  and id_usuario = @idUsuario ", ConexionBD)
                sdaArchivos.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                sdaArchivos.Fill(dsArchivos)
                .gvAdjuntos.DataBind()
                ConexionBD.Close()
                sdaArchivos.Dispose()
                dsArchivos.Dispose()
                .gvAdjuntos.SelectedIndex = -1
                .upAdjuntos.Update()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
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
                sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " +
                                                           "     , 'http://148.223.153.43/ProcAd - Adjuntos IngFact/' + cast(id_dt_archivo as varchar(20)) + '-' + nombre as path " +
                                                           "from dt_archivo " +
                                                           "where id_ms_factura = -1 " +
                                                           "  and tipo = 'E' " +
                                                           "  and id_usuario = @idUsuario ", ConexionBD)
                sdaArchivos.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
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
                sdaActivos.SelectCommand = New SqlCommand("select codigo as no_economico " +
                                                          "     , descripcion " +
                                                          "from dt_af " +
                                                          "where id_ms_factura = -1 " +
                                                          "  and id_usuario = @idUsuario ", ConexionBD)
                sdaActivos.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
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

    Function validarProveedor()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                Dim val As Integer
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = " SELECT COUNT(*) FROM bd_SiSAC.dbo.cg_proveedor cg " +
                                         " LEFT JOIN bd_SiSAC.dbo.dt_proveedor dt ON dt.id_proveedor = cg.id_proveedor " +
                                         " WHERE dt.id_empresa = @idEmpresa AND cg.id_proveedor = @idProveedor "
                SCMValores.Parameters.AddWithValue("@idEmpresa", .ddlEmpresa.SelectedValue)
                SCMValores.Parameters.AddWithValue("@idProveedor", .ddlProveedor.SelectedValue)
                ConexionBD.Open()
                val = SCMValores.ExecuteScalar()

                If val = 0 Then
                    validarProveedor = False
                Else
                    validarProveedor = True
                End If

                ConexionBD.Close()
            Catch ex As Exception
                .litError.Text = ex.ToString()
                validarProveedor = False
            End Try
        End With
    End Function


    Protected Sub gvFacturas_Sorting(sender As Object, e As GridViewSortEventArgs)
        If e.SortExpression = SortExpression Then
            IsAscendingSort = Not IsAscendingSort
        Else
            SortExpression = e.SortExpression
        End If
        BindGrid()
    End Sub


#End Region

#Region "General"

    Protected Sub ddlTipoServicio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoServicio.SelectedIndexChanged
        llenarServicio()
        Me.pnlCBDividirFact.Visible = False
        Me.pnlgvFacturaDividida.Visible = False
    End Sub

    Protected Sub ddlEmpresa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEmpresa.SelectedIndexChanged
        Me.pnlCBDividirFact.Visible = False
        Me.pnlgvFacturaDividida.Visible = False
        rfcEmpresa()
        llenarCC()
        llenarDiv()
        'actualizaProvFact()
        configServ()
    End Sub

    Protected Sub ddlServicio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlServicio.SelectedIndexChanged
        Me.pnlCBDividirFact.Visible = False
        Me.pnlgvFacturaDividida.Visible = False
        configServ()
    End Sub

    Protected Sub rblAdmonOper_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblAdmonOper.SelectedIndexChanged
        configServ()
    End Sub

    Protected Sub ddlCC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCC.SelectedIndexChanged
        Me.pnlCBDividirFact.Visible = False
        Me.pnlgvFacturaDividida.Visible = False
        If Me.ddlCC.SelectedValue <> 0 Then
            Me.ddlDiv.SelectedIndex = -1
            Me.upDiv.Update()
            configServ()
        End If

    End Sub

    Protected Sub ddlDiv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDiv.SelectedIndexChanged
        Me.pnlCBDividirFact.Visible = False
        Me.pnlgvFacturaDividida.Visible = False
        If Me.ddlDiv.SelectedValue <> 0 Then
            Me.ddlCC.SelectedIndex = -1
            Me.upCC.Update()
            configServ()
        End If
        Me.pnlCBDividirFact.Visible = False
        Me.pnlgvFacturaDividida.Visible = False
    End Sub

    Protected Sub ddlDivD_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDivD.SelectedIndexChanged
        With Me
            .txtDiv.Text = ddlDivD.SelectedValue
        End With
    End Sub


    Protected Sub ibtnBuscarProv_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBuscarProv.Click
        With Me
            llenarProv()
            .upProveedores.Update()

            'Determinar si se presenta el panel de facturas
            If ._txtCotUnica.Text = "S" Then
                .pnlFactura.Visible = True
                llenarFacturas()
                .upFactura.Update()
                If .ddlTipoServicio.SelectedIndex = 0 Then
                    .gvFacturas.Columns(2).Visible = False
                End If
            Else
                .pnlFactura.Visible = False
                .upFactura.Update()
            End If

        End With
    End Sub

    Protected Sub ddlProveedor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProveedor.SelectedIndexChanged
        rfcProv()

        'Determinar si se presenta el panel de facturas
        If Me._txtCotUnica.Text = "S" Then
            Me.pnlFactura.Visible = True
            llenarFacturas()
            Me.upFactura.Update()
            If Me.ddlTipoServicio.SelectedIndex = 0 Then
                Me.gvFacturas.Columns(2).Visible = False
            End If
        Else
            Me.pnlFactura.Visible = False
            Me.upFactura.Update()
        End If
        Me.pnlCBDividirFact.Visible = False
        Me.pnlgvFacturaDividida.Visible = False
    End Sub

    Protected Sub btnAgregarAdj_Click(sender As Object, e As EventArgs) Handles btnAgregarAdj.Click
        With Me
            Try
                .litError.Text = ""
                .lblMessage.Text = ""

                If .pnlFactura.Visible = False And .ddlTipoArchivo.SelectedValue = "E" Then
                    .litError.Text = "No se pueden agregar Evidencias a menos que se seleccione una factura"
                Else
                    ' '' Ruta Local
                    'Dim sFileDir As String = "C:/ProcAd - Adjuntos IngFact/" 'Ruta en que se almacenará el archivo
                    ' Ruta en Atenea
                    Dim sFileDir As String = "D:\ProcAd - Adjuntos IngFact\" 'Ruta en que se almacenará el archivo
                    Dim lMaxFileSize As Long = 10485760 'Equivalente a 10 Megabytes

                    'Verificar que el archivo ha sido seleccionado y es un archivo válido
                    If (Not fuAdjunto.PostedFile Is Nothing) And (fuAdjunto.PostedFile.ContentLength > 0) Then
                        'Determinar el nombre original del archivo
                        Dim sFileName As String = System.IO.Path.GetFileName(fuAdjunto.PostedFile.FileName)
                        Dim idArchivo As Integer 'Index correspondiente al archivo
                        Dim fecha As DateTime = Date.Now
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        Try
                            If fuAdjunto.PostedFile.ContentLength <= lMaxFileSize Then
                                'Registrar el archivo en la base de datos
                                SCMValores.CommandText = ""
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "INSERT INTO dt_archivo(id_ms_factura, id_actividad, id_usuario, nombre, fecha, tipo) values(-1, 43, @id_usuario, @nombre, @fecha, @tipo)"
                                SCMValores.Parameters.AddWithValue("@id_usuario", Val(Me._txtIdUsuario.Text))
                                SCMValores.Parameters.AddWithValue("@nombre", sFileName)
                                SCMValores.Parameters.AddWithValue("@fecha", fecha)
                                SCMValores.Parameters.AddWithValue("@tipo", .ddlTipoArchivo.SelectedValue)
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()
                                'Obtener el Id del archivo
                                SCMValores.CommandText = "select max(id_dt_archivo) from dt_archivo where (id_ms_factura = -1) and (fecha = @fecha)"
                                ConexionBD.Open()
                                idArchivo = SCMValores.ExecuteScalar
                                ConexionBD.Close()
                                'Se agrega el Id al nombre del archivo
                                sFileName = idArchivo.ToString + "-" + sFileName
                                'Almacenar el archivo en la ruta especificada
                                fuAdjunto.PostedFile.SaveAs(sFileDir + sFileName)
                                'lblMessage.Visible = True
                                'lblMessage.Text = "El archivo: " + sFileName + " se ha adjuntado exitosamente"

                                If .ddlTipoArchivo.SelectedValue = "A" Then
                                    actualizarAdjuntos()
                                Else
                                    actualizarEvidencias()
                                End If
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
                            ConexionBD.Open()
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
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Factura"

    Protected Sub BindGrid()

        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaFacturas As New SqlDataAdapter
                Dim dsFacturas As New DataSet
                Dim dt As New DataTable()
                .gvFacturas.DataSource = dsFacturas
                'Habilitar columnas para actualización

                sdaFacturas.SelectCommand = New SqlCommand("SP_C_dt_facturaLlenado @rfcProv, @rfcEmpr, @cfdi, @idUsuario, @fecha ", ConexionBD)

                sdaFacturas.SelectCommand.Parameters.AddWithValue("@rfcProv", ._txtRFCProv.Text)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@rfcEmpr", ._txtRFCEmpr.Text)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@cfdi", DBNull.Value)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@fecha", Date.Now)
                ConexionBD.Open()
                sdaFacturas.Fill(dt)
                Dim dv As DataView = dt.DefaultView
                dv.Sort = SortExpression + " " + If(IsAscendingSort, "ASC", "DESC")
                gvFacturas.DataSource = dv
                gvFacturas.DataBind()
                ConexionBD.Close()
                sdaFacturas.Dispose()
                dsFacturas.Dispose()
                .gvFacturas.SelectedIndex = -1

                If .ddlTipoServicio.SelectedIndex = 0 Then
                    .gvFacturas.Columns(2).Visible = False
                End If
            Catch ex As Exception

            End Try
        End With

    End Sub


    Protected Property SortExpression As String
        Get
            Dim value As Object = ViewState("SortExpression")
            Return If(Not IsNothing(value), CStr(value), "fecha_emision")
        End Get
        Set(value As String)
            ViewState("SortExpression") = value
        End Set
    End Property

    Protected Property IsAscendingSort As Boolean
        Get
            Dim value As Object = ViewState("IsAscendingSort")
            Return If(Not IsNothing(value), CBool(value), False)
        End Get
        Set(value As Boolean)
            ViewState("IsAscendingSort") = value
        End Set
    End Property

    Protected Sub gvFacturas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvFacturas.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""

                'Tope 1
                If Val(.gvFacturas.DataKeys(gvFacturas.SelectedIndex).Values("importe")) >= Val(._txtTope1.Text) Then
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
                If Val(.gvFacturas.DataKeys(gvFacturas.SelectedIndex).Values("importe")) >= Val(._txtTope2.Text) Then
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

                .pnlCBDividirFact.Visible = True

                If .pnlDividirFactura.Visible Then
                    .cbDividirFact.Checked = False
                    .pnlDividirFactura.Visible = False
                    .pnlgvFacturaDividida.Visible = False
                    limpiarCampos()
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
                    sdaAF.SelectCommand = New SqlCommand("select codigo, descripcion " +
                                                         "from bd_SiRAc.dbo.dt_equipo " +
                                                         "  left join bd_SiRAc.dbo.cg_equipo on dt_equipo.id_equipo = cg_equipo.id_equipo " +
                                                         "where id_dt_equipo = @idDtActivo ", ConexionBD)
                    sdaAF.SelectCommand.Parameters.AddWithValue("@idDtActivo", .ddlAF.SelectedValue)
                    ConexionBD.Open()
                    sdaAF.Fill(dsAF)
                    ConexionBD.Close()

                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into dt_af ( id_ms_factura,  id_usuario,  id_dt_equipo,  codigo,  descripcion) " +
                                             " 		     values (@id_ms_factura, @id_usuario, @id_dt_equipo, @codigo, @descripcion)"
                    SCMValores.Parameters.AddWithValue("@id_ms_factura", -1)
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
                If Val(._txtBanConfig.Text) = 1 Then
                    .litError.Text = "Error en Configuración del Servicio, favor de validarlo con el Administrador de Catálogos"
                Else
                    If Val(._txtBan.Text) = 1 Then
                        .litError.Text = "Ya se almacenó la factura previamente, favor de validarlo en el apartado de Consulta de Facturas"
                    Else
                        Dim ban As Integer = 0
                        If .txtEspecificaciones.Text.Trim = "" Then
                            .litError.Text = "Favor de ingresar las especificaciones correspondientes"
                            ban = 1
                        End If
                        If .gvAdjuntos.Rows.Count = 0 Then
                            If ban = 1 Then
                                .litError.Text = .litError.Text + "; "
                            Else
                                ban = 1
                            End If
                            .litError.Text = .litError.Text + "Debe existir al menos un Adjunto"
                        End If
                        If .ddlCC.Items.Count = 0 And .ddlDiv.Items.Count = 0 Then
                            If ban = 1 Then
                                .litError.Text = .litError.Text + "; "
                            Else
                                ban = 1
                            End If
                            .litError.Text = .litError.Text + "No existen Centros de Costo ni Divisiones, favor de validarlo con la Administradora de Catálogos"
                        Else
                            If .ddlCC.Items.Count > 0 And .ddlDiv.Items.Count > 0 Then
                                'CC y Div
                                If .ddlCC.SelectedValue = 0 And .ddlDiv.SelectedValue = 0 Then
                                    If ban = 1 Then
                                        .litError.Text = .litError.Text + "; "
                                    Else
                                        ban = 1
                                    End If
                                    .litError.Text = .litError.Text + "Favor de seleccionar el Centro de Costo o División correspondiente"
                                End If
                            Else
                                If .ddlCC.Items.Count > 0 Then
                                    'Solo CC
                                    If .ddlCC.SelectedValue = 0 Then
                                        If ban = 1 Then
                                            .litError.Text = .litError.Text + "; "
                                        Else
                                            ban = 1
                                        End If
                                        .litError.Text = .litError.Text + "Favor de seleccionar el Centro de Costo correspondiente"
                                    End If
                                Else
                                    'Solo Div
                                    If .ddlDiv.SelectedValue = 0 Then
                                        If ban = 1 Then
                                            .litError.Text = .litError.Text + "; "
                                        Else
                                            ban = 1
                                        End If
                                        .litError.Text = .litError.Text + "Favor de seleccionar la División correspondiente"
                                    End If
                                End If
                            End If
                        End If
                        If validarProveedor() = False Then
                            .litError.Text = .litError.Text + ";" + "Comuniquese con el administrador de catálogos para que se replique al proveedor en la empresa seleccionada"
                            ban = 1
                        End If
                        If .pnlFactura.Visible = True Then
                            If .gvFacturas.Rows.Count = 0 Or .gvFacturas.SelectedIndex = -1 Then
                                If ban = 1 Then
                                    .litError.Text = .litError.Text + "; "
                                Else
                                    ban = 1
                                End If
                                .litError.Text = .litError.Text + "Debe seleccionarse una Factura"
                            End If


                            'If gvFacturaDividida.Rows.Count > 0 Then
                            '    ' Validación coincidencia de montos de la factura dividida
                            '    Dim importeDiv As Double, importeTotal As Double, importeDivTotal As Double
                            '    importeTotal = Math.Round(Convert.ToDouble(.gvFacturas.SelectedRow.Cells(9).Text.Replace("$", "")), 2)

                            '    For Each row As GridViewRow In gvFacturaDividida.Rows
                            '        importeDiv += (Math.Round(Val(row.Cells(2).Text), 3))

                            '    Next

                            '    importeDivTotal = Math.Round(importeDiv, 3)

                            '    If importeDivTotal > importeTotal Or importeDivTotal < importeTotal Then
                            '        ban = 1
                            '        .litError.Text = "No coinciden los montos de la factura dividida, rectifique"
                            '    End If
                            'End If
                            'If gvFacturaDividida.Rows.Count > 0 Then
                            '    ' Validación coincidencia de montos de la factura dividida
                            '    Dim importeDiv As Double, importeTotal As Double
                            '    importeTotal = Convert.ToDouble(.gvFacturas.SelectedRow.Cells(9).Text.Replace("$", ""))
                            '    For Each row As GridViewRow In gvFacturaDividida.Rows
                            '        importeDiv += Val(row.Cells(2).Text)
                            '    Next

                            '    If importeDiv > importeTotal Or importeDiv < importeTotal Then
                            '        ban = 1
                            '        .litError.Text = "No coinciden los montos de la factura dividida, rectifique"
                            '    End If
                            'End If

                            If .ddlAutorizador.Items.Count = 0 Then
                                If ban = 1 Then
                                    .litError.Text = .litError.Text + "; "
                                Else
                                    ban = 1
                                End If
                                .litError.Text = .litError.Text + "Favor de seleccionar al Primer Autorizador"
                            Else
                                If .ddlAutorizador.SelectedValue = 0 Then
                                    If ban = 1 Then
                                        .litError.Text = .litError.Text + "; "
                                    Else
                                        ban = 1
                                    End If
                                    .litError.Text = .litError.Text + "Favor de seleccionar al Primer Autorizador"
                                End If
                            End If
                            If .ddlAutorizador2.Visible = True And .ddlAutorizador2.SelectedValue = 0 Then
                                If ban = 1 Then
                                    .litError.Text = .litError.Text + "; "
                                Else
                                    ban = 1
                                End If
                                .litError.Text = .litError.Text + "Favor de seleccionar al Segundo Autorizador"
                            End If
                            If .gvEvidencias.Rows.Count = 0 Then
                                If ban = 1 Then
                                    .litError.Text = .litError.Text + "; "
                                Else
                                    ban = 1
                                End If
                                .litError.Text = .litError.Text + "Debe existir al menos una Evidencia"
                            End If
                            If .cbAF.Checked = True And .gvAF.Rows.Count = 0 Then
                                If ban = 1 Then
                                    .litError.Text = .litError.Text + "; "
                                Else
                                    ban = 1
                                End If
                                .litError.Text = .litError.Text + "Se debe agregar al menos un Activo Fijo"
                            End If
                        End If

                        If ban = 0 Then
                            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                            SCMValores.Connection = ConexionBD

                            While Val(._txtBan.Text) = 0
                                Dim fecha As DateTime
                                fecha = Date.Now
                                'Insertar Solicitud
                                Dim sdaEmpleado As New SqlDataAdapter
                                Dim dsEmpleado As New DataSet
                                Dim query As String
                                If .pnlFactura.Visible = True Then
                                    'Con Factura
                                    query = "select cgEmpl.no_empleado as no_empleadoE " +
                                            "     , cgVal.no_empleado as no_empleadoV " +
                                            "     , cgAut.no_empleado as no_empleadoA " +
                                            "from bd_empleado.dbo.cg_empleado cgEmpl " +
                                            "  left join cg_usuario cgUsrE on cgEmpl.id_empleado = cgUsrE.id_empleado " +
                                            "  left join cg_usuario cgUsrV on cgUsrV.id_usuario = @idVal " +
                                            "  left join bd_Empleado.dbo.cg_empleado cgVal on cgVal.id_empleado = cgUsrV.id_empleado " +
                                            "  left join cg_usuario cgUsrA on cgUsrA.id_usuario = @idAut " +
                                            "  left join bd_Empleado.dbo.cg_empleado cgAut on cgAut.id_empleado = cgUsrA.id_empleado " +
                                            "where cgUsrE.id_usuario = @idEmpl "
                                Else
                                    'Sin Factura
                                    query = "select cgEmpl.no_empleado as no_empleadoE " +
                                            "     , cgVal.no_empleado as no_empleadoV " +
                                            "from bd_empleado.dbo.cg_empleado cgEmpl " +
                                            "  left join cg_usuario cgUsrE on cgEmpl.id_empleado = cgUsrE.id_empleado " +
                                            "  left join cg_usuario cgUsrV on cgUsrV.id_usuario = @idVal " +
                                            "  left join bd_Empleado.dbo.cg_empleado cgVal on cgVal.id_empleado = cgUsrV.id_empleado " +
                                            "where cgUsrE.id_usuario = @idEmpl "
                                End If
                                sdaEmpleado.SelectCommand = New SqlCommand(query, ConexionBD)
                                sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idEmpl", Val(._txtIdUsuario.Text))
                                sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idVal", .ddlValidador.SelectedValue)
                                If .pnlFactura.Visible = True Then
                                    sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idAut", .ddlAutorizador.SelectedValue)
                                End If
                                ConexionBD.Open()
                                sdaEmpleado.Fill(dsEmpleado)
                                ConexionBD.Close()

                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "insert into ms_factura ( id_usr_solicita,  fecha_solicita,  id_usr_autoriza,  id_usr_autoriza2,  id_usr_autoriza3,  empresa,  centro_costo,  division,  no_empleado,  empleado,  no_autorizador,  autorizador,  RFC,  CFDI,  importe_tot,  id_tipo_servicio,  tipo_servicio,  contrato_NAV_alta,  contrato_NAV_reg,  no_contrato_NAV,  no_validador,  validador,  especificaciones,  proveedor_selec,  cotizacion_selec,  id_usr_valida,  id_dt_servicio_conf,  servicio_tipo,  servicio,  admon_oper,  cValidador,  cCompras,  cFinanzas,  cPresupuesto,  cuenta_cont,  status) " +
                                                         " 			      values (@id_usr_solicita, @fecha_solicita, @id_usr_autoriza, @id_usr_autoriza2, @id_usr_autoriza3, @empresa, @centro_costo, @division, @no_empleado, @empleado, @no_autorizador, @autorizador, @RFC, @CFDI, @importe_tot, @id_tipo_servicio, @tipo_servicio, @contrato_NAV_alta, @contrato_NAV_reg, @no_contrato_NAV, @no_validador, @validador, @especificaciones, @proveedor_selec, @cotizacion_selec, @id_usr_valida, @id_dt_servicio_conf, @servicio_tipo, @servicio, @admon_oper, @cValidador, @cCompras, @cFinanzas, @cPresupuesto, @cuenta_cont, @status)"
                                SCMValores.Parameters.AddWithValue("@id_usr_solicita", Val(._txtIdUsuario.Text))
                                SCMValores.Parameters.AddWithValue("@fecha_solicita", fecha)
                                SCMValores.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedItem.Text)
                                If .ddlCC.Items.Count > 0 Then
                                    'CC
                                    If .ddlCC.SelectedValue = 0 Then
                                        SCMValores.Parameters.AddWithValue("@centro_costo", "")
                                    Else
                                        SCMValores.Parameters.AddWithValue("@centro_costo", .ddlCC.SelectedItem.Text)
                                    End If
                                Else
                                    SCMValores.Parameters.AddWithValue("@centro_costo", "")
                                End If
                                If .ddlDiv.Items.Count > 0 Then
                                    'Div
                                    If .ddlDiv.SelectedValue = 0 Then
                                        SCMValores.Parameters.AddWithValue("@division", DBNull.Value)
                                    Else
                                        SCMValores.Parameters.AddWithValue("@division", .ddlDiv.SelectedItem.Text)
                                    End If
                                Else
                                    SCMValores.Parameters.AddWithValue("@division", DBNull.Value)
                                End If

                                SCMValores.Parameters.AddWithValue("@no_empleado", dsEmpleado.Tables(0).Rows(0).Item("no_empleadoE").ToString())
                                SCMValores.Parameters.AddWithValue("@empleado", .lblSolicitante.Text)
                                SCMValores.Parameters.AddWithValue("@id_tipo_servicio", DBNull.Value)
                                SCMValores.Parameters.AddWithValue("@tipo_servicio", DBNull.Value)
                                If .ddlValidador.Visible = True Then
                                    SCMValores.Parameters.AddWithValue("@id_usr_valida", .ddlValidador.SelectedValue)
                                    SCMValores.Parameters.AddWithValue("@no_validador", dsEmpleado.Tables(0).Rows(0).Item("no_empleadoV").ToString())
                                    SCMValores.Parameters.AddWithValue("@validador", .ddlValidador.SelectedItem.Text)
                                Else
                                    SCMValores.Parameters.AddWithValue("@id_usr_valida", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@no_validador", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@validador", DBNull.Value)
                                End If
                                SCMValores.Parameters.AddWithValue("@especificaciones", .txtEspecificaciones.Text.Trim)

                                SCMValores.Parameters.AddWithValue("@id_dt_servicio_conf", ._txtIdDtServ.Text)
                                SCMValores.Parameters.AddWithValue("@servicio_tipo", .ddlTipoServicio.SelectedItem.Text)
                                SCMValores.Parameters.AddWithValue("@servicio", .ddlServicio.SelectedItem.Text)
                                SCMValores.Parameters.AddWithValue("@admon_oper", .rblAdmonOper.SelectedValue)
                                If ._txtCotUnica.Text = "S" Or .cbContratoNav.Checked = True Then
                                    SCMValores.Parameters.AddWithValue("@cValidador", "N")
                                Else
                                    SCMValores.Parameters.AddWithValue("@cValidador", "S")
                                End If
                                SCMValores.Parameters.AddWithValue("@cCompras", ._txtCotizaciones.Text)
                                SCMValores.Parameters.AddWithValue("@cFinanzas", ._txtFinanzas.Text)
                                SCMValores.Parameters.AddWithValue("@cPresupuesto", ._txtValPresup.Text)
                                SCMValores.Parameters.AddWithValue("@cuenta_cont", ._txtCuentaCont.Text)

                                'Contrato NAV
                                If .cbAltaContrato.Visible = True And .cbAltaContrato.Checked = True Then
                                    SCMValores.Parameters.AddWithValue("@contrato_NAV_alta", "S")
                                Else
                                    SCMValores.Parameters.AddWithValue("@contrato_NAV_alta", "N")
                                End If
                                If .cbContratoNav.Visible = True And .cbContratoNav.Checked = True Then
                                    SCMValores.Parameters.AddWithValue("@contrato_NAV_reg", "S")
                                    SCMValores.Parameters.AddWithValue("@no_contrato_NAV", .ddlContrato.SelectedValue)
                                Else
                                    SCMValores.Parameters.AddWithValue("@contrato_NAV_reg", "N")
                                    SCMValores.Parameters.AddWithValue("@no_contrato_NAV", DBNull.Value)
                                End If

                                If .pnlProveedor.Visible = True Then
                                    SCMValores.Parameters.AddWithValue("@RFC", ._txtRFCProv.Text)
                                    SCMValores.Parameters.AddWithValue("@proveedor_selec", .ddlProveedor.SelectedValue)
                                    SCMValores.Parameters.AddWithValue("@cotizacion_selec", 0)
                                Else
                                    SCMValores.Parameters.AddWithValue("@RFC", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@proveedor_selec", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@cotizacion_selec", DBNull.Value)
                                End If
                                If .pnlFactura.Visible = True Then
                                    'Con Factura
                                    SCMValores.Parameters.AddWithValue("@status", "PA")
                                    SCMValores.Parameters.AddWithValue("@id_usr_autoriza", .ddlAutorizador.SelectedValue)
                                    SCMValores.Parameters.AddWithValue("@no_autorizador", dsEmpleado.Tables(0).Rows(0).Item("no_empleadoA").ToString())
                                    SCMValores.Parameters.AddWithValue("@autorizador", .ddlAutorizador.SelectedItem.Text)
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
                                    SCMValores.Parameters.AddWithValue("@CFDI", .gvFacturas.SelectedRow.Cells(4).Text)
                                    SCMValores.Parameters.AddWithValue("@importe_tot", Val(.gvFacturas.DataKeys(gvFacturas.SelectedIndex).Values("importe")))
                                Else
                                    'Sin Factura
                                    SCMValores.Parameters.AddWithValue("@status", "P")
                                    SCMValores.Parameters.AddWithValue("@id_usr_autoriza", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@no_autorizador", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@autorizador", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@id_usr_autoriza2", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@id_usr_autoriza3", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@CFDI", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@importe_tot", DBNull.Value)
                                End If
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()
                                sdaEmpleado.Dispose()
                                dsEmpleado.Dispose()

                                'Obtener ID de la Solicitud
                                SCMValores.CommandText = "select max(id_ms_factura) from ms_factura where no_empleado = @no_empleado and status not in ('Z') "
                                ConexionBD.Open()
                                .lblFolio.Text = SCMValores.ExecuteScalar
                                ConexionBD.Close()
                                If Val(.lblFolio.Text) > 0 Then
                                    ._txtBan.Text = 1
                                End If

                                'Insertar el id_ms_factura a la tabla dt_factura_div

                                If .gvFacturaDividida.Rows.Count > 0 Then
                                    SCMValores.CommandText = ""
                                    SCMValores.Parameters.Clear()
                                    SCMValores.CommandText = "update dt_factura_div set id_ms_factura = @id_ms_factura where id_dt_factura = @id_dt_factura"
                                    SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                                    SCMValores.Parameters.AddWithValue("@id_dt_factura", .gvFacturas.DataKeys(gvFacturas.SelectedIndex).Values("id_dt_factura"))
                                    ConexionBD.Open()
                                    SCMValores.ExecuteNonQuery()
                                    ConexionBD.Close()
                                End If
                                'SCMValores.CommandText = ""
                                'SCMValores.Parameters.Clear()
                                'SCMValores.CommandText = "update dt_factura_div set id_ms_factura = @id_ms_factura where id_dt_factura = @id_dt_factura"
                                'SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                                'SCMValores.Parameters.AddWithValue("@id_dt_factura", .gvFacturas.SelectedRow.Cells(0).Text)
                                'ConexionBD.Open()
                                'SCMValores.ExecuteNonQuery()
                                'ConexionBD.Close()

                                'Actualizar registro de Adjuntos / Evidencias no almacenados
                                SCMValores.CommandText = ""
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "update dt_archivo set id_ms_factura = @idMsFactura where id_ms_factura = -1 and id_usuario = @id_usuario"
                                SCMValores.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))
                                SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()

                                If .gvAdjuntosReq.Rows.Count > 0 Then
                                    'Insertar Adjuntos Requeridos 
                                    SCMValores.CommandText = ""
                                    SCMValores.Parameters.Clear()
                                    SCMValores.CommandText = "insert into dt_factura_adj (id_ms_factura, adjunto) " +
                                                             "select @id_ms_factura, adjunto " +
                                                             "from dt_servicio_adj " +
                                                             "where id_servicio = @id_servicio "
                                    SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                                    SCMValores.Parameters.AddWithValue("@id_servicio", .ddlServicio.SelectedValue)
                                    ConexionBD.Open()
                                    SCMValores.ExecuteNonQuery()
                                    ConexionBD.Close()
                                End If

                                'Actualizar registro de Activos Fijos no almacenados
                                SCMValores.CommandText = ""
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "update dt_af set id_ms_factura = @idMsFactura where id_ms_factura = -1 and id_usuario = @id_usuario"
                                SCMValores.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))
                                SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()

                                Dim idActividad As Integer
                                If .pnlFactura.Visible = True Then
                                    idActividad = 50
                                    'Actualizar dt_factura
                                    SCMValores.Parameters.Clear()
                                    SCMValores.CommandText = "update dt_factura " +
                                                             "  set status = 'As' " +
                                                             "where uuid = @uuid " +
                                                             "  and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') "
                                    SCMValores.Parameters.AddWithValue("@uuid", .gvFacturas.SelectedRow.Cells(4).Text)
                                    ConexionBD.Open()
                                    SCMValores.ExecuteNonQuery()
                                    ConexionBD.Close()
                                Else
                                    idActividad = 44
                                End If

                                'Insertar Instancia de Solicitud de Liberación
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "insert into ms_instancia ( id_ms_sol,  tipo,  id_actividad) " +
                                                         "				    values (@id_ms_sol, @tipo, @id_actividad) "
                                SCMValores.Parameters.AddWithValue("@id_ms_sol", Val(.lblFolio.Text))
                                SCMValores.Parameters.AddWithValue("@tipo", "F")
                                SCMValores.Parameters.AddWithValue("@id_actividad", idActividad)
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
                                SCMValores.CommandText = "insert into ms_historico ( id_ms_instancia,  id_actividad,  id_usr,  fecha) " +
                                                         "				    values (@id_ms_instancia, @id_actividad, @id_usr, @fecha) "
                                SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                                SCMValores.Parameters.AddWithValue("@id_actividad", idActividad)
                                SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                                SCMValores.Parameters.AddWithValue("@fecha", fecha)
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()

                                'Envío de Correo
                                Dim Mensaje As New System.Net.Mail.MailMessage()
                                Dim destinatario As String = ""
                                SCMValores.CommandText = "select cgEmpl.correo from cg_usuario left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado where cg_usuario.id_usuario = @idAut "
                                If .pnlFactura.Visible = True Then
                                    'Cotización Única
                                    SCMValores.Parameters.AddWithValue("@idAut", .ddlAutorizador.SelectedValue)
                                Else
                                    'Validar Solicitud
                                    SCMValores.Parameters.AddWithValue("@idAut", .ddlValidador.SelectedValue)
                                End If
                                'Obtener el Correo del Validador/Autorizador
                                ConexionBD.Open()
                                destinatario = SCMValores.ExecuteScalar()
                                ConexionBD.Close()

                                Mensaje.[To].Add(destinatario)
                                Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                                Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                                Dim texto As String
                                If .pnlFactura.Visible = True Then
                                    'Cotización Única
                                    Mensaje.Subject = "ProcAd - Solicitud de Factura No. " + .lblFolio.Text + " por Autorizar"
                                    texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                                            "Se ingresó la solicitud número <b>" + .lblFolio.Text +
                                            "</b> por parte de <b>" + .lblSolicitante.Text +
                                            "</b><br><br>Favor de determinar si procede </span>"
                                Else
                                    'Validar Solicitud
                                    Mensaje.Subject = "ProcAd - Solicitud de Factura No. " + .lblFolio.Text + " por Validar"
                                    texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                                            "Se ingresó la solicitud número <b>" + .lblFolio.Text +
                                            "</b> por parte de <b>" + .lblSolicitante.Text +
                                            "</b><br><br>Favor de Validar la Solicitud </span>"
                                End If
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
                        'End If
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Contratos NAV"

    Protected Sub cbAltaContrato_CheckedChanged(sender As Object, e As EventArgs) Handles cbAltaContrato.CheckedChanged
        With Me
            If .cbAltaContrato.Checked = True Then
                .cbContratoNav.Checked = False
                .upCbContratoNav.Update()
                .pnlFiltroContrato.Visible = False
                .upFiltroContrato.Update()
            End If

            actualizarCamposFact()
        End With
    End Sub

    Protected Sub cbContratoNav_CheckedChanged(sender As Object, e As EventArgs) Handles cbContratoNav.CheckedChanged
        With Me
            If .cbContratoNav.Checked = True Then
                .cbAltaContrato.Checked = False
                .upCbAltaContrato.Update()
                .pnlFiltroContrato.Visible = True
                .upFiltroContrato.Update()
            Else
                .pnlFiltroContrato.Visible = False
                .upFiltroContrato.Update()
            End If

            actualizarCamposFact()
        End With
    End Sub

    Protected Sub ibtnBuscarContrato_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBuscarContrato.Click
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("NAV")
                Dim sdaContrato As New SqlDataAdapter
                Dim dsContrato As New DataSet
                sdaContrato.SelectCommand = New SqlCommand("select Contrato, Contrato + ' [' + Proveedor + ' / ' +Descripcion + ']' as texto " +
                                                           "from [" + .ddlEmpresa.SelectedItem.Text + "$Contrato Compra] " +
                                                           "where [Fecha Fin] > GETDATE() " +
                                                           "  and Aprobado = 1 " +
                                                           "  and (Contrato like '%' + @valor + '%' " +
                                                           "    or Descripcion like '%' + @valor + '%' " +
                                                           "    or Proveedor like '%' + @valor + '%') " +
                                                           "order by Contrato ", ConexionBD)
                sdaContrato.SelectCommand.Parameters.AddWithValue("@valor", .txtContratoNAV.Text.Trim.ToUpper)
                .ddlContrato.DataSource = dsContrato
                .ddlContrato.DataTextField = "texto"
                .ddlContrato.DataValueField = "Contrato"
                ConexionBD.Open()
                sdaContrato.Fill(dsContrato)
                .ddlContrato.DataBind()
                ConexionBD.Close()
                sdaContrato.Dispose()
                dsContrato.Dispose()
                .ddlContrato.SelectedIndex = -1
                .upDdlContrato.Update()

                rfcProvContrato()
                actualizarCamposFact()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ddlContrato_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlContrato.SelectedIndexChanged
        rfcProvContrato()
        actualizarCamposFact()
    End Sub

#End Region

#Region "Dividir Factura"

    Public Sub limpiarPantalla()
        With Me
            llenargvFacturaDividida()
            porcentajeAsigando()
            ._txtTipoMov.Text = "A"
            .lblTipoMov.Text = "Alta"
            .ibtnEliminar.Enabled = False
            .ibtnEliminar.ImageUrl = "images\Trash_i2.png"
            .ibtnModif.Enabled = False
            .ibtnModif.ImageUrl = "images\Edit_i2.png"
            .pnlInicio.Enabled = True
            .rblTipoAsig.SelectedValue = "P"
            .wnePorcent.Value = 1
            .txtCuentaC1.Text = ""
            .txtCuentaC2.Text = ""
            .wnePorcent.Text = ""
            .wceImporte.Text = ""
            .cbCC.Checked = False
            .txtCC.Text = ""
            .cbDiv.Checked = False
            .txtDiv.Text = ""
            .cbZona.Checked = False
            .txtZona.Text = ""
            .ddlDivD.Items.Clear()
            '.pnlPartida.Visible = False
            '.btnAceptar.Visible = True
            '.btnRechaza.Visible = True
            habilitarCampos(True)

        End With
    End Sub

    Public Sub limpiarCampos()
        With Me
            Try

                .rblTipoAsig.SelectedValue = "P"
                .wnePorcent.Text = 1
                .txtCuentaC1.Text = ""
                .txtCuentaC2.Text = ""
                .wnePorcent.Text = ""
                .wceImporte.Text = ""
                .cbCC.Checked = False
                .txtCC.Text = ""
                .cbDiv.Checked = False
                .txtDiv.Text = ""
                .cbZona.Checked = False
                .txtDiv.Text = ""
                ._txtTipoMov.Text = "A"
                .lblTipoMov.Text = "Alta"
                .ddlDivD.Items.Clear()

            Catch ex As Exception

            End Try
        End With
    End Sub



    Protected Sub ibtnEliminarDivision_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnEliminar.Click
        With Me
            Try
                .lblTipoMov.Text = "Eliminar"
                ._txtTipoMov.Text = "B"
                ' localizarFactDiv(.gvFacturaDividida.SelectedRow.Cells(0).Text)
                localizarFactDiv()
                habilitarCampos(False)
                '.rblTipoAsig.Enabled = False

            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

    Protected Sub ibtnModificarDivision_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnModif.Click
        With Me
            Try
                .lblTipoMov.Text = "Modificar"
                ._txtTipoMov.Text = "M"
                'localizarFactDiv(.gvFacturaDividida.SelectedRow.Cells(0).Text)
                localizarFactDiv()
                habilitarCampos(True)
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

    Protected Sub ibtnAgregarDivision_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAlta.Click
        With Me
            Try
                .lblTipoMov.Text = "Alta"
                ._txtTipoMov.Text = "A"
                .ibtnEliminar.Enabled = False
                .ibtnEliminar.ImageUrl = "images\Trash_i2.png"
                .ibtnModif.Enabled = False
                .ibtnModif.ImageUrl = "images\Edit_i2.png"
                limpiarCampos()
                '.rblTipoAsig.SelectedValue = "P"
                '.wnePorcent.Value = 1
                habilitarCampos(True)
                .gvFacturaDividida.SelectedIndex = -1
            Catch ex As Exception

            End Try
        End With
    End Sub

    Protected Sub cbDividirFactura_CheckedChanged(sender As Object, e As EventArgs) Handles cbDividirFact.CheckedChanged
        With Me
            If .cbDividirFact.Checked = True Then
                .pnlDividirFactura.Visible = True
                .lblTipoMov.Text = "Alta"
                ._txtTipoMov.Text = "A"
                .ibtnEliminar.Enabled = False
                .ibtnEliminar.ImageUrl = "images\Trash_i2.png"
                .ibtnModif.Enabled = False
                .ibtnModif.ImageUrl = "images\Edit_i2.png"
                .lblPorcentAsig.Visible = True
                .lbl_PorcentAsig.Visible = True
                habilitarCampos(True)
                llenargvFacturaDividida()
                porcentajeAsigando()
                limpiarCampos()

                If .gvFacturaDividida.Rows.Count > 0 Then
                    .ibtnAlta.Visible = True
                End If

            ElseIf cbDividirFact.Checked = False Then
                .pnlgvFacturaDividida.Visible = False
                .pnlDividirFactura.Visible = False
                .lblPorcent.Visible = False
                .lbl_PorcentAsig.Visible = False
                limpiarCampos()


            End If
        End With
    End Sub

    Protected Sub cbDiv_CheckedChanged(sender As Object, e As EventArgs) Handles cbDiv.CheckedChanged
        With Me
            If cbDiv.Checked = True Then
                llenarDivD()
                .txtDiv.Text = ddlDivD.SelectedValue
            ElseIf cbDiv.Checked = False Then
                .ddlDivD.Items.Clear()
                .txtDiv.Text = ""
            End If

        End With
    End Sub


    Protected Sub rblTipoAsig_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblTipoAsig.SelectedIndexChanged
        Me.wnePorcent.Text = ""
        Me.lblPorcent.Text = ""
        Me.wceImporte.Text = ""
        If Me.rblTipoAsig.SelectedValue = "P" Then
            'Por Porcentaje
            Me.wnePorcent.Enabled = True
            Me.wnePorcent.Value = 1
            Me.wceImporte.Enabled = False
        Else
            'Por Importe
            Me.wnePorcent.Enabled = False
            Me.wceImporte.Enabled = True
            Me.wceImporte.Value = 1
        End If
    End Sub

    Protected Sub btnAceptarDiv_Click(sender As Object, e As EventArgs) Handles btnAceptarDiv.Click
        With Me
            Try
                .litError.Text = ""
                Dim ban As Integer = 0
                If .txtCuentaC1.Text.Trim = "" Or .txtCuentaC2.Text.Trim = "" Or (.rblTipoAsig.SelectedValue = "P" And .wnePorcent.Text = "") Or (.rblTipoAsig.SelectedValue = "I" And .wceImporte.Text = "") Or (.cbCC.Checked = True And .txtCC.Text.Trim = "") Or (.cbDiv.Checked = True And .txtDiv.Text.Trim = "") Or (.cbZona.Checked = True And .txtZona.Text.Trim = "") Then
                    .litError.Text = "Información Insuficiente, favor de verificar"
                Else
                    If (.cbDiv.Checked = True And .cbZona.Checked = False) Or (.cbDiv.Checked = False And .cbZona.Checked = True) Or (.cbCC.Checked = True And .cbZona.Checked = True) Or (.cbCC.Checked = True And .cbDiv.Checked = True) Then
                        .litError.Text = "Combinación de dimensiones inválida, favor de verificar"
                    Else
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD

                        'Determinar Porcentaje
                        If .rblTipoAsig.SelectedValue = "P" Then
                            .lblPorcent.Text = .wnePorcent.Value
                        Else
                            'Calcular Porcentaje
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "select (@impPart / subtotal) * 100 as porcentaje " +
                                                     "from dt_factura " +
                                                     "where UUID = @uuid " +
                                                     "and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA')"
                            SCMValores.Parameters.AddWithValue("@uuid", .gvFacturas.SelectedRow.Cells(4).Text)
                            SCMValores.Parameters.Add("@impPart", SqlDbType.Float)
                            SCMValores.Parameters("@impPart").Value = .wceImporte.Value
                            ConexionBD.Open()
                            .lblPorcent.Text = SCMValores.ExecuteScalar
                            ConexionBD.Close()
                        End If

                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        Select Case ._txtTipoMov.Text 'Tipo de Ajuste (Agregar, Eliminar o Modificar)
                            Case "A"
                                If validar() Then
                                    SCMValores.CommandText = "SP_I_dt_factura_div"
                                    SCMValores.CommandType = CommandType.StoredProcedure
                                    SCMValores.Parameters.AddWithValue("@id_dt_factura", .gvFacturas.DataKeys(gvFacturas.SelectedIndex).Values("id_dt_factura"))
                                    SCMValores.Parameters.AddWithValue("@id_ms_factura", DBNull.Value)

                                Else
                                    .litError.Text = "Valor Inválido, ya existe esa combinación"
                                    ban = 1
                                End If
                            Case "B"
                                SCMValores.CommandText = "SP_D_dt_factura_div"
                                SCMValores.CommandType = CommandType.StoredProcedure
                                SCMValores.Parameters.AddWithValue("@id_dt_factura_div", .gvFacturaDividida.DataKeys(gvFacturaDividida.SelectedIndex).Values("id_dt_factura_div"))
                            Case Else
                                If validar() Then
                                    SCMValores.CommandText = "SP_U_dt_factura_div"
                                    SCMValores.CommandType = CommandType.StoredProcedure
                                    SCMValores.Parameters.AddWithValue("@id_dt_factura_div", .gvFacturaDividida.DataKeys(gvFacturaDividida.SelectedIndex).Values("id_dt_factura_div"))
                                    SCMValores.Parameters.AddWithValue("@id_ms_factura", DBNull.Value)

                                    'SCMValores.CommandText = "update dt_factura_div SET cuenta_c1 = @cuenta_c1, cuenta_c2 = @cuenta_c2, porcent = @porcent, centro_costo = @centro_costo, division = @division, zona = @zona WHERE id_dt_factura_div = @id_dt_factura_div"
                                    'SCMValores.Parameters.AddWithValue("@id_dt_factura_div", .gvFacturaDividida.SelectedRow.Cells(0).Text)
                                Else
                                    .litError.Text = "Valor Inválido, ya existe esa combinación"
                                    ban = 1
                                End If
                        End Select
                        If ban = 0 And ._txtTipoMov.Text = "A" Or ban = 0 And ._txtTipoMov.Text = "M" Then
                            'SCMValores.Parameters.AddWithValue("@id_ms_factura", idMsFact)
                            SCMValores.Parameters.AddWithValue("@cuenta_c1", .txtCuentaC1.Text.Trim)
                            SCMValores.Parameters.AddWithValue("@cuenta_c2", .txtCuentaC2.Text.Trim)
                            SCMValores.Parameters.Add("@porcent", SqlDbType.Decimal)
                            SCMValores.Parameters("@porcent").Value = CDec(.lblPorcent.Text)
                            If .cbCC.Checked = True Then
                                SCMValores.Parameters.AddWithValue("@centro_costo", .txtCC.Text.Trim)
                            Else
                                SCMValores.Parameters.AddWithValue("@centro_costo", DBNull.Value)
                            End If
                            If .cbDiv.Checked = True Then
                                SCMValores.Parameters.AddWithValue("@division", .txtDiv.Text.Trim)
                            Else
                                SCMValores.Parameters.AddWithValue("@division", DBNull.Value)
                            End If
                            If .cbZona.Checked = True Then
                                SCMValores.Parameters.AddWithValue("@zona", .txtZona.Text.Trim)
                            Else
                                SCMValores.Parameters.AddWithValue("@zona", DBNull.Value)
                            End If

                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()
                            'limpiarCampos()
                            limpiarPantalla()

                        ElseIf ban = 0 Then

                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()
                            'limpiarCampos()
                            limpiarPantalla()
                        End If
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenargvFacturaDividida()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaFacturasDiv As New SqlDataAdapter
                Dim dsFacturasDiv As New DataSet
                .gvFacturaDividida.DataSource = dsFacturasDiv

                sdaFacturasDiv.SelectCommand = New SqlCommand("SP_C_dt_factura_divLlenado @id_ms_factura, @id_dt_factura", ConexionBD)

                sdaFacturasDiv.SelectCommand.Parameters.AddWithValue("@id_ms_factura", DBNull.Value)
                sdaFacturasDiv.SelectCommand.Parameters.AddWithValue("@id_dt_factura", .gvFacturas.DataKeys(gvFacturas.SelectedIndex).Values("id_dt_factura"))


                ConexionBD.Open()
                sdaFacturasDiv.Fill(dsFacturasDiv)
                .gvFacturaDividida.DataBind()
                ConexionBD.Close()
                sdaFacturasDiv.Dispose()
                dsFacturasDiv.Dispose()
                .gvFacturaDividida.SelectedIndex = -1

                If gvFacturaDividida.Rows.Count > 0 Then
                    .pnlgvFacturaDividida.Visible = True
                Else
                    .pnlgvFacturaDividida.Visible = False
                End If

            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

    Protected Sub gvFacturaDividida_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvFacturaDividida.SelectedIndexChanged
        With Me
            Try
                .ibtnEliminar.Enabled = True
                .ibtnEliminar.ImageUrl = "images\Trash.png"
                .ibtnModif.Enabled = True
                .ibtnModif.ImageUrl = "images\Edit.png"
                limpiarCampos()

            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

    Public Sub habilitarCampos(ByVal valor As Boolean)
        With Me

            If valor = True And ._txtTipoMov.Text = "M" Then
                If .rblTipoAsig.SelectedValue = "P" Then
                    .wnePorcent.Enabled = True
                    .wceImporte.Enabled = False
                    .rblTipoAsig.Enabled = True
                Else
                    'Por Importe
                    .wnePorcent.Enabled = False
                    .wceImporte.Enabled = True
                    .rblTipoAsig.Enabled = True
                End If

            ElseIf valor = False And ._txtTipoMov.Text = "B" Then
                .rblTipoAsig.Enabled = False
                .wnePorcent.Enabled = False
                .wnePorcent.Enabled = False
            Else
                .rblTipoAsig.Enabled = valor
                .wnePorcent.Enabled = valor
                .wceImporte.Enabled = valor
            End If

            .txtCuentaC1.Enabled = valor
            .txtCuentaC2.Enabled = valor
            .cbCC.Enabled = valor
            .txtCC.Enabled = valor
            .cbDiv.Enabled = valor
            .txtDiv.Enabled = valor
            .cbZona.Enabled = valor
            .txtZona.Enabled = valor
            .wnePorcent.Value = 1

        End With
    End Sub


    Function validar()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMTemp As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMTemp.Connection = ConexionBD
                Dim conteo As Integer = 0
                Dim query As String
                SCMTemp.CommandText = ""
                SCMTemp.Parameters.Clear()
                Select Case ._txtTipoMov.Text 'Tipo de Ajuste (alta, baja o modificación)
                    Case "A"
                        query = " select count(*) " +
                                " from dt_factura_div " +
                                " where id_dt_factura = @id_dt_factura " +
                                " and cuenta_c1 = @cuenta_c1 " +
                                " and cuenta_c2 = @cuenta_c2 "
                        If .cbCC.Checked = True Then
                            query = query + "  and centro_costo = @centro_costo "
                        Else
                            query = query + "  and centro_costo is null "
                        End If
                        If .cbDiv.Checked = True Then
                            query = query + "  and division = @division "
                        Else
                            query = query + "  and division is null "
                        End If
                        If .cbZona.Checked = True Then
                            query = query + "  and zona = @zona "
                        Else
                            query = query + "  and zona is null "
                        End If
                        SCMTemp.CommandText = query
                    Case Else
                        query = " select count(*) " +
                                " from dt_factura_div " +
                                " where id_dt_factura = @id_dt_factura " +
                                " and id_dt_factura_div <> @id_dt_factura_div " +
                                " and cuenta_c1 = @cuenta_c1 " +
                                " and cuenta_c2 = @cuenta_c2 "
                        If .cbCC.Checked = True Then
                            query = query + "  and centro_costo = @centro_costo "
                        Else
                            query = query + "  and centro_costo is null "
                        End If
                        If .cbDiv.Checked = True Then
                            query = query + "  and division = @division "
                        Else
                            query = query + "  and division is null "
                        End If
                        If .cbZona.Checked = True Then
                            query = query + "  and zona = @zona "
                        Else
                            query = query + "  and zona is null "
                        End If
                        SCMTemp.CommandText = query
                        SCMTemp.Parameters.AddWithValue("@id_dt_factura_div", .gvFacturaDividida.DataKeys(gvFacturaDividida.SelectedIndex).Values("id_dt_factura_div"))
                End Select
                'SCMTemp.Parameters.AddWithValue("@id_dt_factura", .gvFacturaDividida.DataKeys(0).Values("id_dt_factura"))
                SCMTemp.Parameters.AddWithValue("@id_dt_factura", .gvFacturas.DataKeys(gvFacturas.SelectedIndex).Values("id_dt_factura"))
                SCMTemp.Parameters.AddWithValue("@cuenta_c1", .txtCuentaC1.Text.Trim)
                SCMTemp.Parameters.AddWithValue("@cuenta_c2", .txtCuentaC2.Text.Trim)
                If .cbCC.Checked = True Then
                    SCMTemp.Parameters.AddWithValue("@centro_costo", .txtCC.Text.Trim)
                End If
                If .cbDiv.Checked = True Then
                    SCMTemp.Parameters.AddWithValue("@division", .txtDiv.Text.Trim)
                End If
                If .cbZona.Checked = True Then
                    SCMTemp.Parameters.AddWithValue("@zona", .txtZona.Text.Trim)
                End If
                ConexionBD.Open()
                conteo = SCMTemp.ExecuteScalar
                ConexionBD.Close()
                If conteo > 0 Then
                    validar = False
                Else
                    validar = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
                validar = False
            End Try
        End With
    End Function


    Public Sub localizarFactDiv()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaPartidas As New SqlDataAdapter
                Dim dsPartidas As New DataSet
                .gvFacturaDividida.DataSource = dsPartidas
                sdaPartidas.SelectCommand = New SqlCommand("select id_dt_factura_div " +
                                                           "     , cuenta_c1, cuenta_c2 " +
                                                           "     , porcent " +
                                                           "     , isnull(centro_costo, 'XX') as centro_costo " +
                                                           "     , isnull(division, 'XX') as division " +
                                                           "     , isnull(zona, 'XX') as zona " +
                                                           "from dt_factura_div " +
                                                           "where id_dt_factura_div = @id_dt_factura_div ", ConexionBD)
                sdaPartidas.SelectCommand.Parameters.AddWithValue("@id_dt_factura_div", .gvFacturaDividida.DataKeys(gvFacturaDividida.SelectedIndex).Values("id_dt_factura_div"))
                ConexionBD.Open()
                sdaPartidas.Fill(dsPartidas)
                ConexionBD.Close()
                'Llenar Campos
                .txtCuentaC1.Text = dsPartidas.Tables(0).Rows(0).Item("cuenta_c1").ToString()
                .txtCuentaC2.Text = dsPartidas.Tables(0).Rows(0).Item("cuenta_c2").ToString()

                'Determinar número de decimales
                Dim noDecimales As Integer = 0
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = " Select len(format(((Select porcent As porcentAsig from dt_factura_div dtfv " +
                                                             " where id_dt_factura_div = @id_dt_factura_divv " +
                                                             " and id_dt_factura = @id_dt_factura ))  " +
                                                     " - floor(((select porcent as porcentAsig  " +
                                                               " from dt_factura_div " +
                                                              " where id_dt_factura_div = @id_dt_factura_divv " +
                                                             " and id_dt_factura = @id_dt_factura))), '#.###################')) - 1 "
                SCMValores.Parameters.AddWithValue("@id_dt_factura", .gvFacturas.DataKeys(gvFacturas.SelectedIndex).Values("id_dt_factura"))
                SCMValores.Parameters.AddWithValue("@id_dt_factura_divv", .gvFacturaDividida.DataKeys(gvFacturaDividida.SelectedIndex).Values("id_dt_factura_div"))
                ConexionBD.Open()
                noDecimales = SCMValores.ExecuteScalar()
                ConexionBD.Close()
                If noDecimales < 3 Then
                    'Por Porcentaje
                    .rblTipoAsig.SelectedValue = "P"
                    .wnePorcent.Value = Val(dsPartidas.Tables(0).Rows(0).Item("porcent").ToString())
                Else
                    'Por Importe
                    .rblTipoAsig.SelectedValue = "I"
                    SCMValores.CommandText = " select cast(cast(subtotal as float) * (cast(porcent as float)/100) as money) " +
                                             " from dt_factura_div" +
                                             " left join dt_factura on dt_factura_div.id_dt_factura = dt_factura.id_dt_factura" +
                                             " left join ms_factura on dt_factura.uuid = ms_factura.CFDI " +
                                             " where dt_factura_div.id_dt_factura_div = @id_dt_factura_div "

                    SCMValores.Parameters.AddWithValue("@id_dt_factura_div", .gvFacturaDividida.DataKeys(gvFacturaDividida.SelectedIndex).Values("id_dt_factura_div"))
                    ConexionBD.Open()
                    .wceImporte.Value = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If
                If dsPartidas.Tables(0).Rows(0).Item("centro_costo").ToString() = "XX" Then
                    .cbCC.Checked = False
                    .txtCC.Text = ""
                    .txtCC.Enabled = False
                Else
                    .cbCC.Checked = True
                    .txtCC.Text = dsPartidas.Tables(0).Rows(0).Item("centro_costo").ToString()
                    .txtCC.Enabled = True
                End If
                If dsPartidas.Tables(0).Rows(0).Item("division").ToString() = "XX" Then
                    .cbDiv.Checked = False
                    .txtDiv.Text = ""
                    .txtDiv.Enabled = False
                Else
                    .cbDiv.Checked = True
                    llenarDivD()
                    .txtDiv.Text = dsPartidas.Tables(0).Rows(0).Item("division").ToString()
                    .ddlDivD.SelectedIndex = ddlDivD.Items.IndexOf(ddlDivD.Items.FindByValue(.txtDiv.Text))
                    .txtDiv.Enabled = True

                End If
                If dsPartidas.Tables(0).Rows(0).Item("zona").ToString() = "XX" Then
                    .cbZona.Checked = False
                    .txtZona.Text = ""
                    .txtZona.Enabled = False
                Else
                    .cbZona.Checked = True
                    .txtZona.Text = dsPartidas.Tables(0).Rows(0).Item("zona").ToString()
                    .txtZona.Enabled = True
                End If
                sdaPartidas.Dispose()
                dsPartidas.Dispose()


            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub porcentajeAsigando()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "Select format(isnull(sum(porcent), 0), '##0.00#################', 'en-US') as porcentAsig " +
                                         "from dt_factura_div " +
                                         "where id_dt_factura = @id_dt_factura "
                SCMValores.Parameters.AddWithValue("@id_dt_factura", .gvFacturas.DataKeys(gvFacturas.SelectedIndex).Values("id_dt_factura"))
                ConexionBD.Open()
                .lblPorcentAsig.Text = SCMValores.ExecuteScalar
                ConexionBD.Close()
            Catch ex As Exception

            End Try
        End With
    End Sub


#End Region

End Class
