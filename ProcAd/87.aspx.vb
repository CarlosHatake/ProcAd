Public Class _87
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 1

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
                        SCMValores.CommandText = "select nombre + ' ' + ap_paterno + ' ' + ap_materno " + _
                                                 "from cg_usuario " + _
                                                 "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                 "where id_usuario = @idUsuario "
                        SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        .lblSolicitante.Text = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        ' ''SCMValores.Parameters.Clear()
                        '' ''Tope 1
                        ' ''SCMValores.CommandText = "select valor from cg_parametros where parametro = 'ingFact_tope1'"
                        ' ''ConexionBD.Open()
                        ' ''._txtTope1.Text = SCMValores.ExecuteScalar()
                        ' ''ConexionBD.Close()
                        '' ''Tope 2
                        ' ''SCMValores.CommandText = "select valor from cg_parametros where parametro = 'ingFact_tope2'"
                        ' ''ConexionBD.Open()
                        ' ''._txtTope2.Text = SCMValores.ExecuteScalar()
                        ' ''ConexionBD.Close()

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
                        'División
                        llenarDiv()
                        'Bases
                        llenarBase()

                        .rblAdmonOper.SelectedIndex = 0

                        'Lista de Tipos de Servicios
                        Dim sdaTipoServ As New SqlDataAdapter
                        Dim dsTipoServ As New DataSet
                        sdaTipoServ.SelectCommand = New SqlCommand("select distinct(tipo_servicio) " + _
                                                                   "from cg_servicio " + _
                                                                   "where status = 'A' " + _
                                                                   "  and tipo_servicio = 'Servicios Negociados' " + _
                                                                   "order by tipo_servicio ", ConexionBD)
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
                        SCMValores.CommandText = "DELETE from dt_archivo_ns where id_ms_negoc_servicio = -1 and id_usuario = @id_usuario"
                        SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

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

    Public Sub llenarServicio()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaServicio As New SqlDataAdapter
                Dim dsServicio As New DataSet
                sdaServicio.SelectCommand = New SqlCommand("select id_servicio " + _
                                                           "     , servicio " + _
                                                           "from cg_servicio " + _
                                                           "where status = 'A' " + _
                                                           "  and tipo_servicio = @tipo_servicio " + _
                                                           "order by tipo_servicio ", ConexionBD)
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
                sdaCC.SelectCommand = New SqlCommand("select 0 as id_cc " + _
                                                     "     , ' ' as nombre " + _
                                                     "union " + _
                                                     "select id_cc " + _
                                                     "     , nombre " + _
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
                sdaDiv.SelectCommand = New SqlCommand("select 0 as id_div " + _
                                                      "     , ' ' as nombre " + _
                                                      "union " + _
                                                      "select id_div " + _
                                                      "     , nombre " + _
                                                      "from bd_Empleado.dbo.cg_div " + _
                                                      "where id_empresa = @idEmpresa " + _
                                                      "  and status = 'A' " + _
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

    Public Sub llenarBase()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaBase As New SqlDataAdapter
                Dim dsBase As New DataSet
                sdaBase.SelectCommand = New SqlCommand("select id_base " + _
                                                       "     , base " + _
                                                       "from cg_base " + _
                                                       "where status = 'A' " + _
                                                       "  and id_empresa = @id_empresa " + _
                                                       "order by base ", ConexionBD)
                sdaBase.SelectCommand.Parameters.AddWithValue("@id_empresa", .ddlEmpresa.SelectedValue)
                .ddlBase.DataSource = dsBase
                .ddlBase.DataTextField = "base"
                .ddlBase.DataValueField = "id_base"
                ConexionBD.Open()
                sdaBase.Fill(dsBase)
                .ddlBase.DataBind()
                ConexionBD.Close()
                sdaBase.Dispose()
                dsBase.Dispose()
                .upBase.Update()
            Catch ex As Exception

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
                SCMValores.CommandText = "select count(*) " + _
                                         "from cg_servicio " + _
                                         "  left join dt_servicio_conf on cg_servicio.id_servicio = dt_servicio_conf.id_servicio " + _
                                         "where cg_servicio.id_servicio = @id_servicio " + _
                                         "  and (admon_oper = @admon_oper or admon_oper = 'Ambos') " + _
                                         "  and (id_empresa = @id_empresa or id_empresa = 0) "
                SCMValores.Parameters.AddWithValue("@id_servicio", .ddlServicio.SelectedValue)
                SCMValores.Parameters.AddWithValue("@admon_oper", .rblAdmonOper.SelectedValue)
                SCMValores.Parameters.AddWithValue("@id_empresa", .ddlEmpresa.SelectedValue)
                ConexionBD.Open()
                contConfig = SCMValores.ExecuteScalar()
                ConexionBD.Close()

                'Validador 1
                actualizarValAut(-99, "V1", .lbl_Validador, .up_Validador, .ddlValidador, .upValidador)

                If contConfig = 0 Then
                    .litError.Text = "No existe Configuración del Servicio, favor de solicitarlo al Administrador de Catálogos"
                Else
                    If contConfig > 1 Then
                        .litError.Text = "Error en Configuración del Servicio, favor de validarlo con el Administrador de Catálogos"
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
                            sdaTipoServ.SelectCommand = New SqlCommand("select dt_servicio_conf.id_dt_servicio_conf " + _
                                                                       "     , cg_servicio.cotizaciones " + _
                                                                       "     , cg_servicio.cotizacion_unica " + _
                                                                       "     , cg_servicio.contrato " + _
                                                                       "     , cg_servicio.servicio_negociado " + _
                                                                       "     , cuenta_cont " + _
                                                                       "     , isnull(id_usr_valida, -99) as id_usr_valida1 " + _
                                                                       "     , isnull(id_usr_valida2, -99) as id_usr_valida2 " + _
                                                                       "     , finanzas " + _
                                                                       "     , valida_presup " + _
                                                                       "     , id_usr_autoriza1 " + _
                                                                       "     , id_usr_autoriza2 " + _
                                                                       "     , id_usr_autoriza3 " + _
                                                                       "from cg_servicio " + _
                                                                       "  left join dt_servicio_conf on cg_servicio.id_servicio = dt_servicio_conf.id_servicio " + _
                                                                       "where cg_servicio.id_servicio = @id_servicio " + _
                                                                       "  and (admon_oper = @admon_oper or admon_oper = 'Ambos') " + _
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
                            sdaTipoServ.Dispose()
                            dsTipoServ.Dispose()

                            'Validador 1
                            actualizarValAut(Val(._txtIdValidador.Text), "V1", .lbl_Validador, .up_Validador, .ddlValidador, .upValidador)

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
                            sdaValAut.SelectCommand = New SqlCommand("select isnull(id_usr_director, 0) as id_usuario " + _
                                                                     "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                     "from bd_Empleado.dbo.cg_empresa " + _
                                                                     "  left join cg_usuario on cg_empresa.id_usr_director = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                                                                     "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                                                                     "where id_empresa = @idEmpresa " + _
                                                                     "  and cg_empresa.status = 'A' ", ConexionBD)
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@idEmpresa", .ddlEmpresa.SelectedValue)
                        Case -6
                            ' Gerente de Empresa
                            sdaValAut.SelectCommand = New SqlCommand("select isnull(id_usr_gerente, 0) as id_usuario " + _
                                                                     "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                     "from bd_Empleado.dbo.cg_empresa " + _
                                                                     "  left join cg_usuario on cg_empresa.id_usr_gerente = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                                                                     "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                                                                     "where id_empresa = @idEmpresa " + _
                                                                     "  and cg_empresa.status = 'A' ", ConexionBD)
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@idEmpresa", .ddlEmpresa.SelectedValue)
                        Case -5
                            ' Director División / CC
                            If .ddlCC.SelectedValue = 0 Then
                                'División
                                sdaValAut.SelectCommand = New SqlCommand("select isnull(id_usr_director, 0) as id_usuario " + _
                                                                         "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                         "from bd_Empleado.dbo.cg_div " + _
                                                                         "  left join cg_usuario on cg_div.id_usr_director = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                                                                         "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                                                                         "where cg_div.id_div = @id_ccDiv " + _
                                                                         "  and cg_div.status = 'A' ", ConexionBD)
                                sdaValAut.SelectCommand.Parameters.AddWithValue("@id_ccDiv", .ddlDiv.SelectedValue)
                            Else
                                'Centro de Costo
                                sdaValAut.SelectCommand = New SqlCommand("select isnull(id_usr_director, 0) as id_usuario " + _
                                                                         "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                         "from bd_Empleado.dbo.cg_cc " + _
                                                                         "  left join cg_usuario on cg_cc.id_usr_director = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                                                                         "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                                                                         "where cg_cc.id_cc = @id_ccDiv " + _
                                                                         "  and cg_cc.status = 'A' ", ConexionBD)
                                sdaValAut.SelectCommand.Parameters.AddWithValue("@id_ccDiv", .ddlCC.SelectedValue)
                            End If
                        Case -4
                            ' Gerente División / CC
                            If .ddlCC.SelectedValue = 0 Then
                                'División
                                sdaValAut.SelectCommand = New SqlCommand("select isnull(id_usr_gerente, 0) as id_usuario " + _
                                                                         "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                         "from bd_Empleado.dbo.cg_div " + _
                                                                         "  left join cg_usuario on cg_div.id_usr_gerente = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                                                                         "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                                                                         "where cg_div.id_div = @id_ccDiv " + _
                                                                         "  and cg_div.status = 'A' ", ConexionBD)
                                sdaValAut.SelectCommand.Parameters.AddWithValue("@id_ccDiv", .ddlDiv.SelectedValue)
                            Else
                                'Centro de Costo
                                sdaValAut.SelectCommand = New SqlCommand("select isnull(id_usr_gerente, 0) as id_usuario " + _
                                                                         "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                         "from bd_Empleado.dbo.cg_cc " + _
                                                                         "  left join cg_usuario on cg_cc.id_usr_gerente = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                                                                         "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                                                                         "where cg_cc.id_cc = @id_ccDiv " + _
                                                                         "  and cg_cc.status = 'A' ", ConexionBD)
                                sdaValAut.SelectCommand.Parameters.AddWithValue("@id_ccDiv", .ddlCC.SelectedValue)
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
                sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " + _
                                                           "     , 'http://148.223.153.43/ProcAd - Adjuntos NegServ/' + cast(id_dt_archivo_ns as varchar(20)) + '-' + nombre as path " + _
                                                           "from dt_archivo_ns " + _
                                                           "where id_ms_negoc_servicio = -1 " + _
                                                           "  and tipo = 'A' " + _
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

#End Region

#Region "General"

    Protected Sub ddlTipoServicio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoServicio.SelectedIndexChanged
        llenarServicio()
    End Sub

    Protected Sub ddlEmpresa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEmpresa.SelectedIndexChanged
        rfcEmpresa()
        llenarCC()
        llenarDiv()
        llenarBase()
        configServ()
    End Sub

    Protected Sub ddlServicio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlServicio.SelectedIndexChanged
        configServ()
    End Sub

    Protected Sub rblAdmonOper_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblAdmonOper.SelectedIndexChanged
        configServ()
    End Sub

    Protected Sub ddlCC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCC.SelectedIndexChanged
        If Me.ddlCC.SelectedValue <> 0 Then
            Me.ddlDiv.SelectedIndex = -1
            Me.upDiv.Update()
            configServ()
        End If
    End Sub

    Protected Sub ddlDiv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDiv.SelectedIndexChanged
        If Me.ddlDiv.SelectedValue <> 0 Then
            Me.ddlCC.SelectedIndex = -1
            Me.upCC.Update()
            configServ()
        End If
    End Sub

    Protected Sub btnAgregarAdj_Click(sender As Object, e As EventArgs) Handles btnAgregarAdj.Click
        With Me
            Try
                .litError.Text = ""

                ' '' Ruta Local
                ''Dim sFileDir As String = "C:/ProcAd - Adjuntos NegServ/" 'Ruta en que se almacenará el archivo
                ' Ruta en Atenea
                Dim sFileDir As String = "D:\ProcAd - Adjuntos NegServ\" 'Ruta en que se almacenará el archivo
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
                            SCMValores.CommandText = "INSERT INTO dt_archivo_ns(id_ms_negoc_servicio, id_actividad, id_usuario, nombre, fecha, tipo) values(-1, 87, @id_usuario, @nombre, @fecha, @tipo)"
                            SCMValores.Parameters.AddWithValue("@id_usuario", Val(Me._txtIdUsuario.Text))
                            SCMValores.Parameters.AddWithValue("@nombre", sFileName)
                            SCMValores.Parameters.AddWithValue("@fecha", fecha)
                            SCMValores.Parameters.AddWithValue("@tipo", .ddlTipoArchivo.SelectedValue)
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()
                            'Obtener el Id del archivo
                            SCMValores.CommandText = "select max(id_dt_archivo_ns) from dt_archivo_ns where (id_ms_negoc_servicio = -1) and (fecha = @fecha)"
                            ConexionBD.Open()
                            idArchivo = SCMValores.ExecuteScalar
                            ConexionBD.Close()
                            'Se agrega el Id al nombre del archivo
                            sFileName = idArchivo.ToString + "-" + sFileName
                            'Almacenar el archivo en la ruta especificada
                            fuAdjunto.PostedFile.SaveAs(sFileDir + sFileName)
                            'lblMessage.Visible = True
                            'lblMessage.Text = "El archivo: " + sFileName + " se ha adjuntado exitosamente"

                            actualizarAdjuntos()
                        Else
                            'Rechazar el archivo
                            lblMessage.Visible = True
                            lblMessage.Text = "El archivo excede el límite de 10 MB"
                        End If
                    Catch exc As Exception    'En caso de error
                        'Eliminar el archivo en la base de datos
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "delete from dt_archivo_ns where id_archivo = @idArchivo"
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

#End Region

#Region "Guardar"

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        With Me
            Try
                .litError.Text = ""
                If Val(._txtBan.Text) = 1 Then
                    .litError.Text = "Ya se almacenó la factura previamente, favor de validarlo en el apartado de Consulta de Facturas"
                Else
                    Dim ban As Integer = 0
                    If .txtDescripcion.Text.Trim = "" Then
                        .litError.Text = "Favor de ingresar las especificaciones correspondientes"
                        ban = 1
                    End If
                    'If .gvAdjuntos.Rows.Count = 0 Then
                    '    If ban = 1 Then
                    '        .litError.Text = .litError.Text + "; "
                    '    Else
                    '        ban = 1
                    '    End If
                    '    .litError.Text = .litError.Text + "Debe existir al menos un Adjunto"
                    'End If
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
                            sdaEmpleado.SelectCommand = New SqlCommand("select cgEmpl.no_empleado as no_empleadoE " + _
                                                                       "     , cgVal.no_empleado as no_empleadoV " + _
                                                                       "from bd_empleado.dbo.cg_empleado cgEmpl " + _
                                                                       "  left join cg_usuario cgUsrE on cgEmpl.id_empleado = cgUsrE.id_empleado " + _
                                                                       "  left join cg_usuario cgUsrV on cgUsrV.id_usuario = @idVal " + _
                                                                       "  left join bd_Empleado.dbo.cg_empleado cgVal on cgVal.id_empleado = cgUsrV.id_empleado " + _
                                                                       "where cgUsrE.id_usuario = @idEmpl ", ConexionBD)
                            sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idEmpl", Val(._txtIdUsuario.Text))
                            sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idVal", .ddlValidador.SelectedValue)
                            ConexionBD.Open()
                            sdaEmpleado.Fill(dsEmpleado)
                            ConexionBD.Close()

                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "insert into ms_negoc_servicio ( id_usr_solicita,  empleado_solicita,  fecha_solicita,  status,  empresa,  centro_costo,  division,  base,  servicio_tipo,  servicio,  admon_oper,  id_dt_servicio_conf,  cValidador,  cCompras,  cFinanzas,  cPresupuesto,  cuenta_cont,  lugar_ejecucion,  fecha_ini,  fecha_fin,  fecha_ini_usr,  fecha_fin_usr,  descripcion,  id_usr_aut_negoc,  empleado_aut_negoc) " + _
                                                     " 			             values (@id_usr_solicita, @empleado_solicita, @fecha_solicita, @status, @empresa, @centro_costo, @division, @base, @servicio_tipo, @servicio, @admon_oper, @id_dt_servicio_conf, @cValidador, @cCompras, @cFinanzas, @cPresupuesto, @cuenta_cont, @lugar_ejecucion, @fecha_ini, @fecha_fin, @fecha_ini,     @fecha_fin,     @descripcion, @id_usr_aut_negoc, @empleado_aut_negoc)"
                            SCMValores.Parameters.AddWithValue("@id_usr_solicita", Val(._txtIdUsuario.Text))
                            SCMValores.Parameters.AddWithValue("@empleado_solicita", .lblSolicitante.Text)
                            SCMValores.Parameters.AddWithValue("@fecha_solicita", fecha)
                            SCMValores.Parameters.AddWithValue("@status", "P")
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
                            SCMValores.Parameters.AddWithValue("@base", .ddlBase.SelectedItem.Text)
                            SCMValores.Parameters.AddWithValue("@servicio_tipo", .ddlTipoServicio.SelectedItem.Text)
                            SCMValores.Parameters.AddWithValue("@servicio", .ddlServicio.SelectedItem.Text)
                            SCMValores.Parameters.AddWithValue("@admon_oper", .rblAdmonOper.SelectedValue)
                            SCMValores.Parameters.AddWithValue("@id_dt_servicio_conf", ._txtIdDtServ.Text)
                            SCMValores.Parameters.AddWithValue("@cValidador", "S")
                            SCMValores.Parameters.AddWithValue("@cCompras", ._txtCotizaciones.Text)
                            SCMValores.Parameters.AddWithValue("@cFinanzas", ._txtFinanzas.Text)
                            SCMValores.Parameters.AddWithValue("@cPresupuesto", ._txtValPresup.Text)
                            SCMValores.Parameters.AddWithValue("@cuenta_cont", ._txtCuentaCont.Text)
                            SCMValores.Parameters.AddWithValue("@lugar_ejecucion", .txtLugar.Text.Trim)
                            SCMValores.Parameters.AddWithValue("@fecha_ini", .wdpFechaIni.Date)
                            SCMValores.Parameters.AddWithValue("@fecha_fin", .wdpFechaFin.Date)
                            SCMValores.Parameters.AddWithValue("@descripcion", .txtDescripcion.Text.Trim)
                            If .ddlValidador.Visible = True Then
                                SCMValores.Parameters.AddWithValue("@id_usr_aut_negoc", .ddlValidador.SelectedValue)
                                SCMValores.Parameters.AddWithValue("@empleado_aut_negoc", .ddlValidador.SelectedItem.Text)
                            Else
                                SCMValores.Parameters.AddWithValue("@id_usr_aut_negoc", DBNull.Value)
                                SCMValores.Parameters.AddWithValue("@empleado_aut_negoc", DBNull.Value)
                            End If
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()
                            sdaEmpleado.Dispose()
                            dsEmpleado.Dispose()

                            'Obtener ID de la Solicitud
                            SCMValores.CommandText = "select max(id_ms_negoc_servicio) from ms_negoc_servicio where id_usr_solicita = @id_usr_solicita and fecha_solicita = @fecha_solicita and status not in ('Z') "
                            ConexionBD.Open()
                            .lblFolio.Text = SCMValores.ExecuteScalar
                            ConexionBD.Close()
                            If Val(.lblFolio.Text) > 0 Then
                                ._txtBan.Text = 1
                            End If

                            'Actualizar registro de Adjuntos / Evidencias no almacenados
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "update dt_archivo_ns set id_ms_negoc_servicio = @id_ms_negoc_servicio where id_ms_negoc_servicio = -1 and id_usuario = @id_usuario"
                            SCMValores.Parameters.AddWithValue("@id_ms_negoc_servicio", Val(.lblFolio.Text))
                            SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            'Insertar Instancia de Solicitud de Liberación
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "insert into ms_instancia ( id_ms_sol,  tipo,  id_actividad) " + _
                                                     "				    values (@id_ms_sol, @tipo, @id_actividad) "
                            SCMValores.Parameters.AddWithValue("@id_ms_sol", Val(.lblFolio.Text))
                            SCMValores.Parameters.AddWithValue("@tipo", "NS")
                            SCMValores.Parameters.AddWithValue("@id_actividad", 88)
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()
                            'Obtener ID de la Instancia de Solicitud 
                            SCMValores.CommandText = "select max(id_ms_instancia) from ms_instancia where id_ms_sol = @id_ms_sol and tipo = 'NS' "
                            ConexionBD.Open()
                            ._txtIdMsInst.Text = SCMValores.ExecuteScalar
                            ConexionBD.Close()
                            'Insertar Históricos
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "insert into ms_historico ( id_ms_instancia,  id_actividad,  id_usr,  fecha) " + _
                                                     "				    values (@id_ms_instancia, @id_actividad, @id_usr, @fecha) "
                            SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                            SCMValores.Parameters.AddWithValue("@id_actividad", 88)
                            SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                            SCMValores.Parameters.AddWithValue("@fecha", fecha)
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            ''Envío de Correo
                            'Dim Mensaje As New System.Net.Mail.MailMessage()
                            'Dim destinatario As String = ""
                            ''Obtener el Correo del Validador/Autorizador
                            'SCMValores.CommandText = "select cgEmpl.correo from cg_usuario left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado where cg_usuario.id_usuario = @idAut "
                            'SCMValores.Parameters.AddWithValue("@idAut", .ddlValidador.SelectedValue)
                            'ConexionBD.Open()
                            'destinatario = SCMValores.ExecuteScalar()
                            'ConexionBD.Close()

                            'Mensaje.[To].Add(destinatario)
                            'Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                            'Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                            'Dim texto As String
                            'Mensaje.Subject = "ProcAd - Solicitud de Negociación " + .lblFolio.Text + " por Validar"
                            'texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                            '        "Se ingresó la solicitud número <b>" + .lblFolio.Text + _
                            '        "</b> por parte de <b>" + .lblSolicitante.Text + _
                            '        "</b><br><br>Favor de Validar la Solicitud </span>"
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
                            '    .litError.Text = ex.ToString
                            'End Try

                            'Inhabilitar Paneles
                            .pnlInicio.Enabled = False
                        End While
                    End If
                    'End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class