Public Class _08
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
                        pnlAnticiposDecision.Visible = False
                        'Creación de Variables para la conexión y consulta de información a la base de datos
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        Dim cont As Integer
                        Dim sin_comprobacion As String

                        'Validacion de anticipos
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "SELECT anticipo_obl, edit_compro_datos  FROM cg_usuario WHERE id_usuario =@id_usuario"
                        SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        sin_comprobacion = SCMValores.ExecuteScalar
                        ConexionBD.Close()
                        If sin_comprobacion = "N" Then
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "select count(*) from ms_anticipo" +
                                                     "               where id_usr_solicita = @idUsuario " +
                                                     "               and status in ('P', 'A', 'TR', 'EE', 'TRCP', 'EECP', 'TRCA', 'EECA') " +
                                                     "               and tipo = 'A'"
                            SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                            ConexionBD.Open()
                            cont = Val(SCMValores.ExecuteScalar)
                            ConexionBD.Close()
                            If cont = 0 Then
                                Server.Transfer("Login.aspx")
                            End If
                        End If
                        'Datos del Solicitante
                        Dim sdaEmpleado As New SqlDataAdapter
                        Dim dsEmpleado As New DataSet
                        sdaEmpleado.SelectCommand = New SqlCommand("select cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " +
                                                                   "     , puesto_tabulador " +
                                                                   "     , cgCC.id_cc " +
                                                                   "     , cgCC.id_empresa " +
                                                                   "     , case ant_pendientes when 'S' then 1 else 0 end as antPend " +
                                                                   "     , unidad_comp " +
                                                                   "     , cg_usuario.omitir_PGV " +
                                                                   "     , cg_usuario.alimentos_tab " +
                                                                   "     , cg_usuario.taxi_tab " +
                                                                   "     , right('0000000' + ltrim(rtrim(no_empleado)),7) as no_empleado " +
                                                                   "     , factura_extemp_comp " +
                                                                   "     , edit_compro_datos " +
                                                                   "     , cg_usuario.american_express " +
                                                                   "from cg_usuario " +
                                                                   "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                                   "  left join bd_Empleado.dbo.cg_cc cgCC on cgEmpl.id_cc = cgCC.id_cc " +
                                                                   "where id_usuario = @idUsuario ", ConexionBD)
                        sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        sdaEmpleado.Fill(dsEmpleado)
                        ConexionBD.Close()
                        .lblEmpleado.Text = dsEmpleado.Tables(0).Rows(0).Item("nombre_empleado").ToString()
                        ._txtPuestoTab.Text = dsEmpleado.Tables(0).Rows(0).Item("puesto_tabulador").ToString()
                        ._txtIdEmpresaEmpl.Text = dsEmpleado.Tables(0).Rows(0).Item("id_empresa").ToString()
                        ._txtIdCCEmpl.Text = dsEmpleado.Tables(0).Rows(0).Item("id_cc").ToString()
                        ._txtAntPend.Text = dsEmpleado.Tables(0).Rows(0).Item("antPend").ToString()
                        ._txtOmitValPGV.Text = dsEmpleado.Tables(0).Rows(0).Item("omitir_PGV").ToString()
                        ._txtAlimTab.Text = dsEmpleado.Tables(0).Rows(0).Item("alimentos_tab").ToString()
                        ._txtTaxiTab.Text = dsEmpleado.Tables(0).Rows(0).Item("taxi_tab").ToString()
                        ._txtPeriodoLibre.Text = dsEmpleado.Tables(0).Rows(0).Item("factura_extemp_comp").ToString()
                        ._txtEditComprobacion.Text = dsEmpleado.Tables(0).Rows(0).Item("edit_compro_datos").ToString()
                        ' _txtAmericanExpress.Text = dsEmpleado.Tables(0).Rows(0).Item("american_express").ToString()
                        If dsEmpleado.Tables(0).Rows(0).Item("unidad_comp").ToString() = "S" Then
                            .pnlUnidad.Visible = True
                        Else
                            .pnlUnidad.Visible = False
                        End If

                        'If _txtAmericanExpress.Text = "S" Then
                        '    pnlAEE.Visible = True
                        '    pnlAEEC.Visible = True
                        'Else
                        '    pnlAEE.Visible = False
                        '    pnlAEEC.Visible = False
                        'End If

                        'Fecha de alta Empleado NOM
                        Dim ConexionBDNom As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBDNom.ConnectionString = accessDB.conBD("NOM")
                        Dim SCMValoresNOM As SqlCommand = New System.Data.SqlClient.SqlCommand
                        Dim fechaAE As Date
                        SCMValoresNOM.Connection = ConexionBDNom
                        SCMValoresNOM.CommandText = ""
                        SCMValoresNOM.Parameters.Clear()
                        SCMValoresNOM.CommandText = "select dateadd(month, 1, isnull((select top 1 cast(fecini as date) " +
                                                    "                                 from nomtrab " +
                                                    "                                 where nomtrab.status = 'A' " +
                                                    "                                   and nomtrab.cvetra = @no_empleado), '1900-01-01')) as fecha_inicio "
                        SCMValoresNOM.Parameters.AddWithValue("@no_empleado", dsEmpleado.Tables(0).Rows(0).Item("no_empleado").ToString())
                        ConexionBDNom.Open()
                        fechaAE = SCMValoresNOM.ExecuteScalar()
                        ConexionBDNom.Close()

                        If fechaAE > Now.Date() Then
                            ._txtTaxiTab.Text = "S"
                        End If

                        sdaEmpleado.Dispose()
                        dsEmpleado.Dispose()


                        'Eliminar registro de Unidades no almacenadas previamente
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "DELETE from dt_unidad where id_ms_comp = -1 and id_usuario = @id_usuario"
                        SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
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
                        .ddlEmpresa.SelectedValue = Val(._txtIdEmpresaEmpl.Text) '9  Se elige TRACSA por Default
                        'RFC de la empresa
                        rfcEmpresa()
                        'Ingresar Tipos de Gastos
                        tipoGasto()
                        'Ingresar Division y/o Centro de Costo
                        tipoDivCC()

                        'Se borrara

                        ''Actividades
                        'Dim sdaActividad As New SqlDataAdapter
                        'Dim dsActividad As New DataSet
                        'sdaActividad.SelectCommand = New SqlCommand("select id_actividad, actividad " +
                        '                                            "from cg_actividad " +
                        '                                            "where status = 'A' " +
                        '                                            "order by actividad ", ConexionBD)
                        '.ddlTipoAct.DataSource = dsActividad
                        '.ddlTipoAct.DataTextField = "actividad"
                        '.ddlTipoAct.DataValueField = "id_actividad"
                        'ConexionBD.Open()
                        'sdaActividad.Fill(dsActividad)
                        '.ddlTipoAct.DataBind()
                        'ConexionBD.Close()
                        'sdaActividad.Dispose()
                        'dsActividad.Dispose()

                        'actAutorizadores()

                        'Hasta aqui'

                        'Verificar lo de las fechas'
                        Dim fecha_termino As Integer
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "select count(*) " +
                                         "from cg_usuario " +
                                         "where id_usuario = @idUsuario and fecha_termino = 'S' "
                        SCMValores.Parameters.AddWithValue("@idUsuario", Val(_txtIdUsuario.Text))
                        ConexionBD.Open()
                        fecha_termino = SCMValores.ExecuteScalar()
                        ConexionBD.Close()



                        'Limpiar Campos
                        .wdteFechaIni.Date = Now.Date
                        .wdteFechaFin.Date = Now.Date
                        .txtJust.Text = ""
                        'Límites de Fechas Min/Max

                        If fecha_termino <> 0 Then
                            .wdteFechaFin.MinValue = Now.Date.AddDays(-20)


                        Else
                            .wdteFechaFin.MinValue = Now.Date.AddDays(-7)
                            .wdteFechaIni.MinValue = Now.Date.AddDays(-20)
                            .wdpFecha1.MinValue = Now.Date.AddDays(-20)

                        End If
                        .wdteFechaIni.MaxValue = Now.Date
                        .wdteFechaFin.MaxValue = Now.Date
                        .wdpFecha1.MaxValue = Now.Date
                        'Origenes y Destinos
                        Dim sdaLugar As New SqlDataAdapter
                        Dim dsLugar As New DataSet
                        sdaLugar.SelectCommand = New SqlCommand("select 0 as id_lugar " +
                                                                "     , '' as lugar " +
                                                                "union " +
                                                                "select id_lugar " +
                                                                "     , lugar " +
                                                                "from cg_lugar " +
                                                                "where status = 'A' " +
                                                                "order by lugar ", ConexionBD)
                        .ddlOrig1.DataSource = dsLugar
                        .ddlDest1.DataSource = dsLugar
                        .ddlOrig1.DataTextField = "lugar"
                        .ddlDest1.DataTextField = "lugar"
                        .ddlOrig1.DataValueField = "id_lugar"
                        .ddlDest1.DataValueField = "id_lugar"
                        ConexionBD.Open()
                        sdaLugar.Fill(dsLugar)
                        .ddlOrig1.DataBind()
                        .ddlDest1.DataBind()
                        ConexionBD.Close()
                        sdaLugar.Dispose()
                        dsLugar.Dispose()

                        'Anticipos
                        actAnticipos()

                        'Panel de fechas
                        upDateV1.Visible = False

                        'Paneles de Conceptos
                        mostrarOcultarCampos()

                        _txtAutDir.Text = 0
                        .lbl_Director.Visible = False
                        .ddlDirector.Visible = False

                        'Botones
                        .upValeIng.Visible = False
                        .btnGuardar.Enabled = False

                        'btnAgregar.Visible = False
                        'pnlGrid.Visible = False
                        pnlSuma.Visible = False
                        pnlFinalizar.Visible = False
                        pnlEliminar.Visible = False

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
                SCMValores.CommandText = "select isnull(rfc, '') as rfc " +
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

    Public Sub tipoGasto()
        With Me
            Try
                .litError.Text = ""
                'Tipo de Gasto
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaTipoGasto As New SqlDataAdapter
                Dim dsTipoGasto As New DataSet
                sdaTipoGasto.SelectCommand = New SqlCommand("SELECT id_gasto, nombre_gasto FROM cg_tipoGasto WHERE status = 'A' and id_empresa=@idEmpresa ORDER BY nombre_gasto", ConexionBD)
                sdaTipoGasto.SelectCommand.Parameters.AddWithValue("@idEmpresa", .ddlEmpresa.SelectedValue)
                .ddlTipoGasto.DataSource = dsTipoGasto
                .ddlTipoGasto.DataTextField = "nombre_gasto"
                .ddlTipoGasto.DataValueField = "id_gasto"
                ConexionBD.Open()
                sdaTipoGasto.Fill(dsTipoGasto)
                .ddlTipoGasto.DataBind()
                ConexionBD.Close()
                sdaTipoGasto.Dispose()
                dsTipoGasto.Dispose()
                actAbreviatura()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub actAbreviatura()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "SELECT isnull(abreviatura, '') as valor " +
                                         "FROM cg_tipoGasto " +
                                         "WHERE id_gasto = @idGasto "
                SCMValores.Parameters.AddWithValue("@idGasto", .ddlTipoGasto.SelectedValue)
                ConexionBD.Open()
                ._txtTipoGasto.Text = SCMValores.ExecuteScalar()
                ConexionBD.Close()
                .upAbreviatura.Update()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub tipoDivCC()
        With Me
            Try
                .litError.Text = ""
                If .ddlTipoGasto.Items.Count > 0 Then
                    'Determinar caracterpisticas taller, division y centroCosto
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim sdaTemp As New SqlDataAdapter
                    Dim dsTemp As New DataSet
                    sdaTemp.SelectCommand = New SqlCommand("select taller, centro_costo, division from cg_tipoGasto where id_gasto = @idGasto", ConexionBD)
                    sdaTemp.SelectCommand.Parameters.AddWithValue("@idGasto", .ddlTipoGasto.SelectedValue)
                    ConexionBD.Open()
                    sdaTemp.Fill(dsTemp)
                    ConexionBD.Close()

                    If dsTemp.Tables(0).Rows(0).Item("division").ToString() = "S" Then
                        'Divisiones

                        .ddlDiv.Visible = True
                        .lbl_Div.Visible = True
                        .lbl_CC.Visible = False
                        .ddlCC.Visible = False


                        Dim sdaDivision As New SqlDataAdapter
                        Dim dsDivision As New DataSet
                        sdaDivision.SelectCommand = New SqlCommand("select id_div, nombre " +
                                                                   "from bd_Empleado.dbo.cg_div " +
                                                                   "where id_empresa = @idEmpresa and taller = @taller and status = 'A' " +
                                                                   "order by nombre ", ConexionBD)
                        sdaDivision.SelectCommand.Parameters.AddWithValue("@idEmpresa", .ddlEmpresa.SelectedValue)
                        sdaDivision.SelectCommand.Parameters.AddWithValue("@taller", dsTemp.Tables(0).Rows(0).Item("taller").ToString())
                        .ddlDiv.DataSource = dsDivision
                        .ddlDiv.DataTextField = "nombre"
                        .ddlDiv.DataValueField = "id_div"
                        ConexionBD.Open()
                        sdaDivision.Fill(dsDivision)
                        .ddlDiv.DataBind()
                        ConexionBD.Close()
                        sdaDivision.Dispose()
                        dsDivision.Dispose()


                        'Else
                        '    .ddlDiv.Visible = False
                        '    .lbl_Div.Visible = False
                    End If

                    If dsTemp.Tables(0).Rows(0).Item("centro_costo").ToString() = "S" Then
                        'Centro de Costo

                        .ddlCC.Visible = True
                        .lbl_CC.Visible = True
                        .ddlDiv.Visible = False
                        .lbl_Div.Visible = False


                        Dim sdaCentroCosto As New SqlDataAdapter
                        Dim dsCentroCosto As New DataSet
                        sdaCentroCosto.SelectCommand = New SqlCommand("select id_cc, nombre " +
                                                                      "from bd_Empleado.dbo.cg_cc " +
                                                                      "where id_empresa = @idEmpresa " +
                                                                      "  and status = 'A' " +
                                                                      "order by nombre ", ConexionBD)
                        sdaCentroCosto.SelectCommand.Parameters.AddWithValue("@idEmpresa", .ddlEmpresa.SelectedValue)
                        .ddlCC.DataSource = dsCentroCosto
                        .ddlCC.DataTextField = "nombre"
                        .ddlCC.DataValueField = "id_cc"
                        ConexionBD.Open()
                        sdaCentroCosto.Fill(dsCentroCosto)
                        .ddlCC.DataBind()
                        ConexionBD.Close()
                        sdaCentroCosto.Dispose()
                        dsCentroCosto.Dispose()

                        'Validar si el CC del Empleado está en la lista
                        Dim ban As Integer = 0
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "select count(*) as valor " +
                                                 "from bd_Empleado.dbo.cg_cc " +
                                                 "where id_empresa = @idEmpresa " +
                                                 "  and status = 'A' " +
                                                 "  and id_cc = @idCCEmpl "
                        SCMValores.Parameters.AddWithValue("@idEmpresa", .ddlEmpresa.SelectedValue)
                        SCMValores.Parameters.AddWithValue("@idCCEmpl", Val(._txtIdCCEmpl.Text))
                        ConexionBD.Open()
                        ban = SCMValores.ExecuteScalar()
                        ConexionBD.Close()
                        If ban > 0 Then
                            .ddlCC.SelectedValue = Val(._txtIdCCEmpl.Text)
                        Else
                            .ddlCC.SelectedIndex = -1
                        End If
                        'Else
                        '    .ddlCC.Visible = False
                        '    .lbl_CC.Visible = False
                    End If


                    .upCCDiv.Update()
                    .up_lblCCDiv.Update()

                    actIdCC()

                    actAutorizadores()

                    sdaTemp.Dispose()
                    dsTemp.Dispose()

                    btnCancelar.Visible = False
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub actIdCC()
        With Me
            Try
                If .upCCDiv.Visible = False And .up_lblCCDiv.Visible = True Then
                    ._txtIdCC.Text = 0
                    .upIdCC.Update()
                Else
                    ._txtIdCC.Text = .ddlCC.SelectedValue
                    .upIdCC.Update()
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub actAutorizadores()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")

                'Lista de Validadores
                Dim sdaVal As New SqlDataAdapter
                Dim dsVal As New DataSet
                .ddlValidador.DataSource = dsVal
                sdaVal.SelectCommand = New SqlCommand("select cgEmpl.id_empleado, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " +
                                                    "from dt_autorizador " +
                                                    "  left join cg_usuario on dt_autorizador.id_autorizador = cg_usuario.id_usuario " +
                                                    "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                    "where dt_autorizador.id_usuario = @idUsuario " +
                                                    "  and cg_usuario.status = 'A' " +
                                                    "  and dt_autorizador.validador = 'S' " +
                                                    "order by cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno ", ConexionBD)
                sdaVal.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                .ddlValidador.DataTextField = "nombre_empleado"
                .ddlValidador.DataValueField = "id_empleado"
                ConexionBD.Open()
                sdaVal.Fill(dsVal)
                .ddlValidador.DataBind()
                ConexionBD.Close()
                sdaVal.Dispose()
                dsVal.Dispose()
                .ddlValidador.SelectedIndex = -1
                If .ddlValidador.Items.Count = 0 Then
                    .ddlValidador.Visible = False
                    .lbl_Validador.Visible = False
                Else
                    .ddlValidador.Visible = True
                    .lbl_Validador.Visible = True

                End If
                upValidador.Update()

                'Lista de Autorizadores
                Dim sdaAut As New SqlDataAdapter
                Dim dsAut As New DataSet
                .ddlAutorizador.DataSource = dsAut

                'Autorizadores por Usuario
                sdaAut.SelectCommand = New SqlCommand("select cgEmpl.id_empleado, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " +
                                                      "from dt_autorizador " +
                                                      "  left join cg_usuario on dt_autorizador.id_autorizador = cg_usuario.id_usuario " +
                                                      "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                      "where dt_autorizador.id_usuario = @idUsuario " +
                                                      "  and cg_usuario.status = 'A' and validador = 'N' " +
                                                      "order by aut_dir, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno ", ConexionBD)

                '' Autorización por CC / DIV
                'If .ddlCC.Visible = True Then
                '    'Autorizadores del CC
                '    sdaAut.SelectCommand = New SqlCommand("select 1 as no_, cgEmpl.id_empleado, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " + _
                '                                          "from bd_empleado.dbo.cg_empleado cgEmpl " + _
                '                                          "  left join bd_empleado.dbo.cg_cc cgCC on cgCC.id_empl_ger = cgEmpl.id_empleado " + _
                '                                          "  left join cg_usuario on cgEmpl.id_empleado = cg_usuario.id_empleado " + _
                '                                          "where cgEmpl.status = 'A' " + _
                '                                          "  and cg_usuario.status = 'A' " + _
                '                                          "  and cgCC.id_cc = @idCC " + _
                '                                          "  and autorizador = 'S' " + _
                '                                          "  and cg_usuario.id_usuario not in (@idUsuario) " + _
                '                                          "union " + _
                '                                          "select 2 as no_, cgEmpl.id_empleado, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " + _
                '                                          "from bd_empleado.dbo.cg_empleado cgEmpl " + _
                '                                          "  left join bd_empleado.dbo.cg_cc cgCC on cgCC.id_empl_dir = cgEmpl.id_empleado " + _
                '                                          "  left join cg_usuario on cgEmpl.id_empleado = cg_usuario.id_empleado " + _
                '                                          "where cgEmpl.status = 'A' " + _
                '                                          "  and cg_usuario.status = 'A' " + _
                '                                          "  and cgCC.id_cc = @idCC " + _
                '                                          "  and autorizador = 'S' " + _
                '                                          "  and cg_usuario.id_usuario not in (@idUsuario) ", ConexionBD)
                '    sdaAut.SelectCommand.Parameters.AddWithValue("@idCC", .ddlCC.SelectedValue)
                'Else
                '    'Autorizadores de la Div
                '    sdaAut.SelectCommand = New SqlCommand("select 1 as no_, cgEmpl.id_empleado, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " + _
                '                                          "from bd_empleado.dbo.cg_empleado cgEmpl " + _
                '                                          "  left join bd_empleado.dbo.cg_div cgDiv on cgDiv.id_empl_ger = cgEmpl.id_empleado " + _
                '                                          "  left join cg_usuario on cgEmpl.id_empleado = cg_usuario.id_empleado " + _
                '                                          "where cgEmpl.status = 'A' " + _
                '                                          "  and cg_usuario.status = 'A' " + _
                '                                          "  and cgDiv.id_div = @idDiv " + _
                '                                          "  and autorizador = 'S' " + _
                '                                          "  and cg_usuario.id_usuario not in (@idUsuario) " + _
                '                                          "union " + _
                '                                          "select 2 as no_, cgEmpl.id_empleado, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " + _
                '                                          "from bd_empleado.dbo.cg_empleado cgEmpl " + _
                '                                          "  left join bd_empleado.dbo.cg_div cgDiv on cgDiv.id_empl_dir = cgEmpl.id_empleado " + _
                '                                          "  left join cg_usuario on cgEmpl.id_empleado = cg_usuario.id_empleado " + _
                '                                          "where cgEmpl.status = 'A' " + _
                '                                          "  and cg_usuario.status = 'A' " + _
                '                                          "  and cgDiv.id_div = @idDiv " + _
                '                                          "  and autorizador = 'S' " + _
                '                                          "  and cg_usuario.id_usuario not in (@idUsuario) " + _
                '                                          "order by nombre_empleado ", ConexionBD)
                '    sdaAut.SelectCommand.Parameters.AddWithValue("@idDiv", .ddlDiv.SelectedValue)
                'End If

                'sdaAut.SelectCommand = New SqlCommand("select cgEmpl.id_empleado, nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " + _
                '                                      "from bd_empleado.dbo.cg_empleado cgEmpl " + _
                '                                      "  inner join cg_usuario on cgEmpl.id_empleado = cg_usuario.id_empleado " + _
                '                                      "where cgEmpl.status = 'A' and cg_usuario.status = 'A' AND autorizador = 'S' and cg_usuario.id_usuario not in (@idUsuario) " + _
                '                                      "order by nombre_empleado ", ConexionBD)

                sdaAut.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                .ddlAutorizador.DataTextField = "nombre_empleado"
                .ddlAutorizador.DataValueField = "id_empleado"
                ConexionBD.Open()
                sdaAut.Fill(dsAut)
                .ddlAutorizador.DataBind()
                ConexionBD.Close()
                sdaAut.Dispose()
                dsAut.Dispose()

                upAutorizador.Update()

                'Lista de Directores
                Dim sdaDir As New SqlDataAdapter
                Dim dsDir As New DataSet
                .ddlDirector.DataSource = dsDir

                'Autorizadores por Usuario
                sdaDir.SelectCommand = New SqlCommand("select cgEmpl.id_empleado, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " +
                                                      "from dt_autorizador " +
                                                      "  left join cg_usuario on dt_autorizador.id_autorizador = cg_usuario.id_usuario " +
                                                      "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                      "where dt_autorizador.id_usuario = @idUsuario " +
                                                      "  and cg_usuario.status = 'A' " +
                                                      "  and dt_autorizador.aut_dir = 'S' " +
                                                      "order by cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno ", ConexionBD)

                '' Autorización por CC / DIV
                'If .ddlCC.Visible = True Then
                '    'Autorizadores del CC
                '    sdaDir.SelectCommand = New SqlCommand("select cgEmpl.id_empleado, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " + _
                '                                          "from bd_empleado.dbo.cg_empleado cgEmpl " + _
                '                                          "  left join bd_empleado.dbo.cg_cc cgCC on cgCC.id_empl_dir = cgEmpl.id_empleado " + _
                '                                          "  left join cg_usuario on cgEmpl.id_empleado = cg_usuario.id_empleado " + _
                '                                          "where cgEmpl.status = 'A' " + _
                '                                          "  and cg_usuario.status = 'A' " + _
                '                                          "  and cgCC.id_cc = @idCC " + _
                '                                          "  and autorizador = 'S' " + _
                '                                          "  and cg_usuario.id_usuario not in (@idUsuario) ", ConexionBD)
                '    sdaDir.SelectCommand.Parameters.AddWithValue("@idCC", .ddlCC.SelectedValue)
                'Else
                '    'Autorizadores de la Div
                '    sdaDir.SelectCommand = New SqlCommand("select cgEmpl.id_empleado, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " + _
                '                                          "from bd_empleado.dbo.cg_empleado cgEmpl " + _
                '                                          "  left join bd_empleado.dbo.cg_div cgDiv on cgDiv.id_empl_dir = cgEmpl.id_empleado " + _
                '                                          "  left join cg_usuario on cgEmpl.id_empleado = cg_usuario.id_empleado " + _
                '                                          "where cgEmpl.status = 'A' " + _
                '                                          "  and cg_usuario.status = 'A' " + _
                '                                          "  and cgDiv.id_div = @idDiv " + _
                '                                          "  and autorizador = 'S' " + _
                '                                          "  and cg_usuario.id_usuario not in (@idUsuario) " + _
                '                                          "order by nombre_empleado ", ConexionBD)
                '    sdaDir.SelectCommand.Parameters.AddWithValue("@idDiv", .ddlDiv.SelectedValue)
                'End If

                'sdaDir.SelectCommand = New SqlCommand("select cgEmpl.id_empleado, nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " + _
                '                                      "from bd_empleado.dbo.cg_empleado cgEmpl " + _
                '                                      "  inner join cg_usuario on cgEmpl.id_empleado = cg_usuario.id_empleado " + _
                '                                      "where cgEmpl.status = 'A' and cg_usuario.status = 'A' AND autorizador = 'S' and cgEmpl.id_empleado in (8, 28, 5119) and cg_usuario.id_usuario not in (@idUsuario) " + _
                '                                      "order by nombre_empleado ", ConexionBD)

                sdaDir.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                .ddlDirector.DataTextField = "nombre_empleado"
                .ddlDirector.DataValueField = "id_empleado"
                ConexionBD.Open()
                sdaDir.Fill(dsDir)
                .ddlDirector.DataBind()
                ConexionBD.Close()
                sdaDir.Dispose()
                dsDir.Dispose()
                .ddlDirector.SelectedIndex = -1
                upDirector.Update()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    'Public Sub actValidador()
    '    With Me
    '        Try
    '            .litError.Text = ""
    '            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
    '            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
    '            'Lista de Validadores
    '            Dim sdaVal As New SqlDataAdapter
    '            Dim dsVal As New DataSet
    '            .ddlValidador.DataSource = dsVal
    '            sdaVal.SelectCommand = New SqlCommand("select cgEmpl.id_empleado, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " +
    '                                                "from dt_autorizador " +
    '                                                "  left join cg_usuario on dt_autorizador.id_autorizador = cg_usuario.id_usuario " +
    '                                                "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
    '                                                "where dt_autorizador.id_usuario = @idUsuario " +
    '                                                "  and cg_usuario.status = 'A' " +
    '                                                "  and dt_autorizador.validador = 'S' " +
    '                                                "order by cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno ", ConexionBD)
    '            sdaVal.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
    '            .ddlValidador.DataTextField = "nombre_empleado"
    '            .ddlValidador.DataValueField = "id_empleado"
    '            ConexionBD.Open()
    '            sdaVal.Fill(dsVal)
    '            .ddlValidador.DataBind()
    '            ConexionBD.Close()
    '            sdaVal.Dispose()
    '            dsVal.Dispose()
    '            .ddlValidador.SelectedIndex = -1
    '            upValidador.Update()
    '        Catch ex As Exception
    '            .litError.Text = ex.Message.ToString()
    '        End Try
    '    End With
    'End Sub

    Public Sub actAnticipos()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaAnticipos As New SqlDataAdapter
                Dim dsAnticipos As New DataSet
                .gvAnticipos.DataSource = dsAnticipos
                'gvAnticiposAmex.DataSource = dsAnticipos
                'Inhabilitar columnas para vista
                .gvAnticipos.Columns(0).Visible = True
                .gvAnticipos.Columns(3).Visible = True
                .gvAnticipos.Columns(4).Visible = True
                Dim FechInc As Date
                Dim FechFin As Date
                'Grupo de Unidades a Facturar
                sdaAnticipos.SelectCommand = New SqlCommand("select id_ms_anticipo " +
                                                            "     , fecha_pago as fecha " +
                                                            "     , periodo_ini " +
                                                            "     , periodo_fin " +
                                                            "     , isnull(monto_hospedaje, 0) + isnull(monto_alimentos, 0) + isnull(monto_casetas, 0) + isnull(monto_otros, 0) as importe " +
                                                            "     , isnull(monto_hospedaje, 0) + isnull(monto_alimentos, 0) + isnull(monto_casetas, 0) as importeAPGV " +
                                                            "     , case tipo when 'A' then 'N' else 'S' end  as tipo " +
                                                            "from ms_anticipo " +
                                                            "where id_usr_solicita = @idUsuario " +
                                                            "  and status in ('EE', 'TR') " +
                                                            "  and empresa = @Empresa and tipo = 'A' ", ConexionBD)
                sdaAnticipos.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(_txtIdUsuario.Text))
                sdaAnticipos.SelectCommand.Parameters.AddWithValue("@Empresa", ddlEmpresa.SelectedItem.Text)
                ConexionBD.Open()
                sdaAnticipos.Fill(dsAnticipos)
                .gvAnticipos.DataBind()
                ConexionBD.Close()

                'Inhabilitar columnas para vista
                .gvAnticipos.Columns(0).Visible = False
                .gvAnticipos.Columns(3).Visible = False
                .gvAnticipos.Columns(4).Visible = False

                'AMEX'
                Dim sdaAnticiposAMEX As New SqlDataAdapter
                Dim dsAnticiposAMEX As New DataSet
                .gvAnticiposAmex.DataSource = dsAnticiposAMEX
                'gvAnticiposAmex.DataSource = dsAnticipos
                'Inhabilitar columnas para vista
                .gvAnticiposAmex.Columns(0).Visible = True
                .gvAnticiposAmex.Columns(3).Visible = True
                .gvAnticiposAmex.Columns(4).Visible = True
                'Grupo de Unidades a Facturar
                sdaAnticiposAMEX.SelectCommand = New SqlCommand("select id_ms_anticipo " +
                                                            "     , fecha_solicita as fecha " +
                                                            "     , periodo_ini " +
                                                            "     , periodo_fin " +
                                                            "     , isnull(monto_hospedaje, 0) + isnull(monto_alimentos, 0) + isnull(monto_casetas, 0) + isnull(monto_otros, 0) as importe " +
                                                            "     , isnull(monto_hospedaje, 0) + isnull(monto_alimentos, 0) + isnull(monto_casetas, 0) as importeAPGV " +
                                                            "     , case tipo when 'A' then 'N' else 'S' end  as tipo " +
                                                            "from ms_anticipo " +
                                                            "where id_usr_solicita = @idUsuario " +
                                                            "  and status in ('EE', 'TR') " +
                                                            "  and empresa = @Empresa and tipo = 'AAE' ", ConexionBD)
                sdaAnticiposAMEX.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(_txtIdUsuario.Text))
                sdaAnticiposAMEX.SelectCommand.Parameters.AddWithValue("@Empresa", ddlEmpresa.SelectedItem.Text)
                ConexionBD.Open()
                sdaAnticiposAMEX.Fill(dsAnticiposAMEX)
                .gvAnticiposAmex.DataBind()
                ConexionBD.Close()

                'Inhabilitar columnas para vista
                .gvAnticiposAmex.Columns(0).Visible = False
                .gvAnticiposAmex.Columns(3).Visible = False
                .gvAnticiposAmex.Columns(4).Visible = False

                If gvAnticiposAmex.Rows.Count = 0 Then
                    gvAnticiposAmex.Visible = False
                End If

                rbdOpcionAnticipo.Items(0).Text = rbdOpcionAnticipo.Items(0).Text + " (" + gvAnticipos.Rows.Count().ToString + ")"
                rbdOpcionAnticipo.Items(1).Text = rbdOpcionAnticipo.Items(1).Text + " (" + gvAnticiposAmex.Rows.Count().ToString + ")"

                If gvAnticiposAmex.Rows.Count <> 0 And gvAnticipos.Rows.Count <> 0 And gvAnticipos.Visible = True And gvAnticiposAmex.Visible = True Then
                    pnlAnticiposDecision.Visible = True
                    gvAnticipos.Visible = False
                    gvAnticiposAmex.Visible = False
                    btnAceptar.Enabled = False
                Else
                    If gvAnticipos.Rows.Count > 0 And gvAnticipos.Visible = True Then
                        _txtAnticipo.Text = "Anticipo"
                        FechInc = dsAnticipos.Tables(0).Rows(0).Item("periodo_ini").ToString()
                        FechFin = dsAnticipos.Tables(0).Rows(0).Item("periodo_fin").ToString()
                        .litError.Text = ""
                        '.btnSumar.Enabled = True

                        For Each row As GridViewRow In .gvAnticipos.Rows
                            If row.RowType = DataControlRowType.DataRow Then
                                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                                chkRow.Checked = False
                            End If
                        Next

                        .wdteFechaIni.Date = dsAnticipos.Tables(0).Rows(0).Item("periodo_ini").ToString()
                        .wdteFechaFin.Date = dsAnticipos.Tables(0).Rows(0).Item("periodo_fin").ToString()

                        If ._txtEditComprobacion.Text = "N" Then
                            .wdteFechaIni.Visible = False
                            .wdteFechaFin.Visible = False
                            .lblFechInc.Text = FechInc.ToShortDateString()
                            .lblFechFin.Text = FechFin.ToShortDateString()

                        Else
                            .lblFechInc.Visible = False
                            .lblFechFin.Visible = False

                        End If

                    ElseIf gvAnticiposAmex.Visible = True And gvAnticiposAmex.Rows.Count() > 0 Then
                        'ANTICIPOS AMEX'
                        _txtAnticipo.Text = "Anticipo AMEX"
                        FechInc = dsAnticiposAMEX.Tables(0).Rows(0).Item("periodo_ini").ToString()
                        FechFin = dsAnticiposAMEX.Tables(0).Rows(0).Item("periodo_fin").ToString()
                        .litError.Text = ""
                        '.btnSumar.Enabled = True

                        For Each row As GridViewRow In .gvAnticiposAmex.Rows
                            If row.RowType = DataControlRowType.DataRow Then
                                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                                chkRow.Checked = False
                            End If
                        Next

                        .wdteFechaIni.Date = dsAnticiposAMEX.Tables(0).Rows(0).Item("periodo_ini").ToString()
                        .wdteFechaFin.Date = dsAnticiposAMEX.Tables(0).Rows(0).Item("periodo_fin").ToString()

                        If ._txtEditComprobacion.Text = "N" Then
                            .wdteFechaIni.Visible = False
                            .wdteFechaFin.Visible = False
                            .lblFechInc.Text = FechInc.ToShortDateString()
                            .lblFechFin.Text = FechFin.ToShortDateString()

                        Else
                            .lblFechInc.Visible = False
                            .lblFechFin.Visible = False

                        End If
                    End If
                End If

                .upEmpresa.Update()
                .upAnticipos.Update()
                'UpdatePanel1.Update()
                UpdatePanel3.Update()
                sdaAnticipos.Dispose()
                dsAnticipos.Dispose()
                sdaAnticiposAMEX.Dispose()
                dsAnticiposAMEX.Dispose()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub



    'Public Sub SelectCheckBo()
    '    With Me
    '        ._txtIdAnticipoSeleccionado.Text = ""
    '        For Each row As GridViewRow In gvAnticipos.Rows
    '            If row.RowType = DataControlRowType.DataRow Then
    '                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
    '                If chkRow.Checked Then
    '                    ._txtIdAnticipoSeleccionado.Text = .gvAnticipos.DataKeys(gvAnticipos.SelectedIndex).Values("importe")
    '                    'row.Cells(0).Text
    '                End If
    '            End If
    '        Next
    '    End With
    'End Sub
    Public Sub mostrarOcultarCampos()
        With Me
            Try
                .litError.Text = ""
                'Ocultar Paneles, excepto el primero
                .pnlConcepto1.Visible = True
                If .cbFactura1.Checked = False And .cbTabulador1.Checked = False Then
                    .cbFactura1.Checked = True
                    tipoFT("F", .cbFactura1, .upFactura1, .cbTabulador1, .upTabulador1, .ddlConcepto1, .upConcepto1, .wneNoPers1.Value, .upNoPers1, .wneNoDias1.Value, .txtRFC1, .ibtnRFCBus1, .upRFC1, .ddlNoFactura1, .upNoFactura1, .hlProveedor1, .upProveedor1, .wceSubtotal1, .upSubtotal1, .wceIVA1, .upIVA1, .wceTotal1, .upTotal1, .wpePorcentAut1, .upPorcentAut1)
                End If
                If .cbTabulador1.Checked = True And .txtObs1.Text.Trim = "" Then
                    .lblObsE1.Visible = True
                Else
                    .lblObsE1.Visible = False
                End If
                .upObsE1.Update()
                .lblNoFacturaE1.Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub actConceptos()
        With Me
            Try
                Dim tipo As String
                If .pnlConcepto1.Visible = True Then
                    If .cbFactura1.Checked = True Then
                        tipo = "F"
                    Else
                        tipo = "T"
                    End If
                    tipoFT(tipo, .cbFactura1, .upFactura1, .cbTabulador1, .upTabulador1, .ddlConcepto1, .upConcepto1, .wneNoPers1.Value, .upNoPers1, .wneNoDias1.Value, .txtRFC1, .ibtnRFCBus1, .upRFC1, .ddlNoFactura1, .upNoFactura1, .hlProveedor1, .upProveedor1, .wceSubtotal1, .upSubtotal1, .wceIVA1, .upIVA1, .wceTotal1, .upTotal1, .wpePorcentAut1, .upPorcentAut1)
                End If

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub tipoFT(ByRef tipo, ByRef cbFactura, ByRef upFactura, ByRef cbTabulador, ByRef upTabulador, ByRef ddlConcepto, ByRef upConcepto, ByRef noPersonas, ByRef upNoPersonas, ByVal noDias, ByRef txtRFC, ByRef ibtnRFCBus, ByRef upRFC, ByRef ddlNoFactura, ByRef upNoFactura, ByRef lblProveedor, ByRef upProveedor, ByRef wceSubtotal, ByRef upSubtotal, ByRef wceIVA, ByRef upIVA, ByRef wceTotal, ByRef upTotal, ByRef wpePocentAut, ByRef upPocentAut)
        With Me
            Try
                'Creación de Variables para la conexión y consulta de información a la base de datos
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                If tipo = "F" Then
                    'Por Factura
                    'Lista de Conceptos
                    Dim sdaConceptoF As New SqlDataAdapter
                    Dim dsConceptoF As New DataSet
                    sdaConceptoF.SelectCommand = New SqlCommand("select id_concepto_comp " +
                                                                "   , case when iva is null then abreviatura + ' - ' + concepto + ' ' + 'EXENTO' else abreviatura + ' - ' + concepto + ' ' + cast(cast((iva * 100) as int) as varchar(2)) + '%' end as nombre_concepto " +
                                                                "from cg_concepto_comp " +
                                                                "where status = 'A' " +
                                                                "  and abreviatura = @abreviatura " +
                                                                "  and (id_cc = 0 or id_cc = @idCC) " +
                                                                "order by nombre_concepto ", ConexionBD)
                    sdaConceptoF.SelectCommand.Parameters.AddWithValue("@abreviatura", ._txtTipoGasto.Text)
                    sdaConceptoF.SelectCommand.Parameters.AddWithValue("@idCC", ._txtIdCC.Text)
                    ddlConcepto.DataSource = dsConceptoF
                    ddlConcepto.DataTextField = "nombre_concepto"
                    ddlConcepto.DataValueField = "id_concepto_comp"
                    ConexionBD.Open()
                    sdaConceptoF.Fill(dsConceptoF)
                    ddlConcepto.DataBind()
                    ConexionBD.Close()
                    sdaConceptoF.Dispose()
                    dsConceptoF.Dispose()
                    upConcepto.Update()
                    cbFactura.Checked = True
                    upFactura.Update()
                    cbTabulador.Checked = False
                    upTabulador.Update()
                    txtRFC.Text = ""
                    txtRFC.Enabled = True
                    upRFC.Update()
                    'Llenar Lista de Facturas
                    actNoFactura(ddlConcepto.SelectedValue, noPersonas, upNoPersonas, noDias, txtRFC, ddlNoFactura, upNoFactura, lblProveedor, upProveedor, wceSubtotal, upSubtotal, wceIVA, upIVA, wceTotal, upTotal, wpePocentAut, upPocentAut)
                    ddlNoFactura.Enabled = True
                    ibtnRFCBus.Enabled = True
                Else
                    'Por Tabulador
                    actConceptosT(ddlConcepto, upConcepto)
                    cbFactura.Checked = False
                    upFactura.Update()
                    cbTabulador.Checked = True
                    upTabulador.Update()
                    txtRFC.Text = ""
                    txtRFC.Enabled = False
                    upRFC.Update()
                    'Limpiar Lista de Facturas
                    ddlNoFactura.Items.Clear()
                    ddlNoFactura.Enabled = False
                    ibtnRFCBus.Enabled = False
                    upNoFactura.Update()
                    'Limpiar Campos de Importes para Captura
                    lblProveedor.Text = ""
                    upProveedor.Update()
                    wceSubtotal.Value = 0
                    wceSubtotal.Enabled = True
                    upSubtotal.Update()
                    wceIVA.Value = 0
                    wceIVA.Enabled = False
                    upIVA.Update()
                    wceTotal.Value = 0
                    wceTotal.Enabled = False
                    upTotal.Update()
                    wpePocentAut.Value = 1
                    upPocentAut.Update()
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub conNuevo()
        Try
            Dim totalAnt As Integer = 0
            Dim columnas As Integer = 0
            'VALIDACION DE AMEX'
            If rbdOpcionAnticipo.Text = "Anticipo AMEX" Or _txtAnticipo.Text = "Anticipo AMEX" Then
                For Each row As GridViewRow In gvAnticiposAmex.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                        If chkRow.Checked Then
                            totalAnt = totalAnt + 1
                        End If
                    End If
                Next
                columnas = gvAnticiposAmex.Rows.Count()
            Else
                For Each row As GridViewRow In gvAnticipos.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                        If chkRow.Checked Then
                            totalAnt = totalAnt + 1
                        End If
                    End If
                Next
                columnas = gvAnticipos.Rows.Count()
            End If


            If columnas > 0 And totalAnt = 0 And Val(_txtAntPend.Text) = 0 Then
                litError.Text = "Favor de seleccionar el Anticipo a Comprobar"
            Else
                If gvProducto.Rows.Count <> 0 Then
                    btnCancelar.Visible = True
                End If

                'cbFactura1.Checked = True
                'cbTabulador1.Checked = False

                ''Limpiar panel'
                'ddlConcepto1.SelectedIndex = -1
                'wneNoPers1.Value = 1
                'wneNoDias1.Value = 1
                'wceSubtotal1.Value = 0
                'wceSubtotal1.Enabled = False
                'wceIVA1.Enabled = False
                'wceIVA1.Value = 0
                'wceTotal1.Value = 0
                'wceTotal1.Enabled = False
                'txtRFC1.Text = ""
                'hlProveedor1.Text = ""
                'ddlNoFactura1.Items.Clear()
                ddlOrig1.SelectedIndex = -1
                ddlDest1.SelectedIndex = -1
                txtVehi1.Text = ""
                txtObs1.Text = ""

                Dim tipo As String = ""
                If cbFactura1.Checked = True Then
                    tipo = "F"
                    lblObsE1.Visible = False
                Else
                    tipo = "T"
                    lblNoFacturaE1.Visible = False
                    If cbTabulador1.Checked = True And txtObs1.Text.Trim = "" Then
                        lblObsE1.Visible = True
                    Else
                        lblObsE1.Visible = False
                    End If
                End If
                upObsE1.Update()
                tipoFT(tipo, cbFactura1, upFactura1, cbTabulador1, upTabulador1, ddlConcepto1, upConcepto1, wneNoPers1.Value, upNoPers1, wneNoDias1.Value, txtRFC1, ibtnRFCBus1, upRFC1, ddlNoFactura1, upNoFactura1, hlProveedor1, upProveedor1, wceSubtotal1, upSubtotal1, wceIVA1, upIVA1, wceTotal1, upTotal1, wpePorcentAut1, upPorcentAut1)

                ' wdteFechaIni.Enabled = False
                ' wdteFechaFin.Enabled = False
                'Ocultar'
                pnlConcepto1.Visible = True
                'pnlGrid.Visible = False
                pnlAceptar.Visible = True
                pnlSuma.Visible = True
                'btnAgregar.Visible = False
                pnlFinalizar.Visible = True
                pnlGvEvidencias.Visible = True
                gvProducto.Visible = True
                pnlGrid.Visible = True
            End If
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Public Sub actConceptosT(ByRef ddlConcepto, ByRef upConcepto)
        With Me
            Try
                'Lista de Conceptos
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaConceptoT As New SqlDataAdapter
                Dim dsConceptoT As New DataSet
                Dim query As String
                query = "select id_concepto, abreviatura + ' - ' + nombre_concepto as nombre_concepto " +
                        "from cg_concepto " +
                        "where status = 'A' " +
                        "  and abreviatura = @abreviatura " +
                        "  and (id_cc = 0 or id_cc = @idCC) "
                If ._txtAlimTab.Text = "N" Then
                    query = query + "  and (cve_concepto1 not in ('Desayuno/Cena', 'Comida') or cve_concepto1 is null) " +
                                    "  and (cve_concepto2 not in ('Desayuno/Cena', 'Comida') or cve_concepto2 is null) "
                End If
                If ._txtTaxiTab.Text = "N" Then
                    query = query + "  and nombre_concepto not in ('TAXIS') "
                End If
                query = query + "order by nombre_concepto "

                sdaConceptoT.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaConceptoT.SelectCommand.Parameters.AddWithValue("@abreviatura", ._txtTipoGasto.Text)
                sdaConceptoT.SelectCommand.Parameters.AddWithValue("@idCC", ._txtIdCC.Text)
                'Se unen los conceptos de todos los tipos de comprobación
                ''sdaConceptoT.SelectCommand.Parameters.AddWithValue("@idTipoComp", .ddlTipoComp.SelectedValue)
                ddlConcepto.DataSource = dsConceptoT
                ddlConcepto.DataTextField = "nombre_concepto"
                ddlConcepto.DataValueField = "id_concepto"
                ConexionBD.Open()
                sdaConceptoT.Fill(dsConceptoT)
                ddlConcepto.DataBind()
                ConexionBD.Close()
                sdaConceptoT.Dispose()
                dsConceptoT.Dispose()
                upConcepto.Update()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub actNoFactura(ByVal idConcepto, ByRef noPersonas, ByRef upNoPers, ByVal noDias, ByRef txtRFC, ByRef ddlNoFactura, ByRef upNoFactura, ByRef hlProveedor, ByRef upProveedor, ByRef wceSubtotal, ByRef upSubtotal, ByRef wceIVA, ByRef upIVA, ByRef wceTotal, ByRef upTotal, ByRef wpePorcentAut, ByRef upPorcentAut)
        With Me
            Try
                'Lista de Números de Facturas
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaNoFactura As New SqlDataAdapter
                Dim dsNoFactura As New DataSet

                ' Nueva versión Procedimiento Almacenado Carlos Hernández 19 Jul 22
                sdaNoFactura.SelectCommand = New SqlCommand("exec SP_C_dt_Factura @RFC,@idEmpresa, @idConcepto, @idUsuario, @fechaIni, @fechaFin", ConexionBD)

                sdaNoFactura.SelectCommand.Parameters.AddWithValue("@RFC", txtRFC.Text)
                sdaNoFactura.SelectCommand.Parameters.AddWithValue("@idEmpresa", ddlEmpresa.SelectedValue)
                sdaNoFactura.SelectCommand.Parameters.AddWithValue("@idConcepto", idConcepto)
                sdaNoFactura.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                sdaNoFactura.SelectCommand.Parameters.AddWithValue("@fecha", Date.Now)
                sdaNoFactura.SelectCommand.Parameters.AddWithValue("@fechaIni", .wdteFechaIni.Date)
                sdaNoFactura.SelectCommand.Parameters.AddWithValue("@fechaFin", .wdteFechaFin.Date.AddDays(4).AddHours(23).AddMinutes(59).AddSeconds(59))

                ddlNoFactura.DataSource = dsNoFactura
                ddlNoFactura.DataTextField = "no_factura"
                ddlNoFactura.DataValueField = "id_dt_factura"
                ConexionBD.Open()
                sdaNoFactura.Fill(dsNoFactura)
                ddlNoFactura.DataBind()
                ConexionBD.Close()
                sdaNoFactura.Dispose()
                dsNoFactura.Dispose()
                upNoFactura.Update()
                If ddlNoFactura.Items.Count = 0 Then
                    hlProveedor.Text = ""
                    hlProveedor.NavigateUrl = ""
                    upProveedor.Update()
                    wceSubtotal.Text = ""
                    wceSubtotal.Enabled = False
                    upSubtotal.Update()
                    wceIVA.Text = ""
                    wceIVA.Enabled = False
                    upIVA.Update()
                    wceTotal.Text = ""
                    wceTotal.Enabled = False
                    upTotal.Update()
                    wpePorcentAut.Value = 1
                    upPorcentAut.Update()
                Else
                    actFactura(idConcepto, noPersonas, upNoPers, noDias, ddlNoFactura.SelectedValue, hlProveedor, upProveedor, wceSubtotal, upSubtotal, wceIVA, upIVA, wceTotal, upTotal, wpePorcentAut, upPorcentAut)
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub actFactura(ByVal idConcepto, ByRef noPersonas, ByRef upNoPers, ByVal noDias, ByVal idDtFactura, ByRef hlProveedor, ByRef upProveedor, ByRef wceSubtotal, ByRef upSubtotal, ByRef wceIVA, ByRef upIVA, ByRef wceTotal, ByRef upTotal, ByRef wpePorcentAut, ByRef upPorcentAut)
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaFactura As New SqlDataAdapter
                Dim dsFactura As New DataSet
                sdaFactura.SelectCommand = New SqlCommand("select isnull(razon_emisor,'') as razon_emisor " +
                                                          "     , subtotal - descuentos + tot_tras_ieps as subtotal " +
                                                          "     , tot_tras_iva " +
                                                          "     , importe " +
                                                          "     , tot_ret_isr " +
                                                          "     , 'http://148.223.153.43/Facturas/' + dt_factura.rfc_receptor + '/' + format(datepart(year, dt_factura.fecha_emision), '00') + '/' + upper(format(dt_factura.fecha_emision, 'MMMM', 'es-es')) + '/' + dt_factura.movimiento + '/' + dt_factura.tipo + '/' + dt_factura.uuid + '.pdf' as path " +
                                                          "from dt_factura " +
                                                          "where id_dt_factura = @idDtFactura ", ConexionBD)
                'Versión Anterior   * * * vLili * * *
                '"     , 'http://148.223.153.43/Facturas/' + dt_factura.rfc_receptor + '/' + format(datepart(year, dt_factura.fecha_emision), '00') + '/' + upper(format(dt_factura.fecha_emision, 'MMMM', 'es-es')) + '/' + dt_factura.movimiento + '/' + dt_factura.tipo + '/' + dt_factura.uuid + '.pdf' as path " + _
                'Versión Actual  * * * vKarim * * *
                '"     , 'http://148.223.153.37:8084/facturas/' + uuid as path " + _
                sdaFactura.SelectCommand.Parameters.AddWithValue("@idDtFactura", idDtFactura)
                ConexionBD.Open()
                sdaFactura.Fill(dsFactura)
                ConexionBD.Close()

                'Datos de Conceptos
                Dim sdaTabulador As New SqlDataAdapter
                Dim dsTabulador As New DataSet
                sdaTabulador.SelectCommand = New SqlCommand("select no_conceptos " +
                                                            "     , round((isnull(cantidad1, 0) * isnull(cgTab1.imp_con_factura, 0) + isnull(cantidad2, 0) * isnull(cgTab2.imp_con_factura, 0)) / (1 + isnull(iva,0)), 2) as subtotal " +
                                                            "     , round(((isnull(cantidad1, 0) * isnull(cgTab1.imp_con_factura, 0) + isnull(cantidad2, 0) * isnull(cgTab2.imp_con_factura, 0)) / (1 + isnull(iva,0))) * isnull(iva,0), 2) as iva " +
                                                            "     , round((isnull(cantidad1, 0) * isnull(cgTab1.imp_con_factura, 0) + isnull(cantidad2, 0) * isnull(cgTab2.imp_con_factura, 0)) / (1 + isnull(iva,0)), 2) + round(((isnull(cantidad1, 0) * isnull(cgTab1.imp_con_factura, 0) + isnull(cantidad2, 0) * isnull(cgTab2.imp_con_factura, 0)) / (1 + isnull(iva,0))) * isnull(iva,0), 2) as importe " +
                                                            "     , isnull(cantidad1, 0) * isnull(cgTab1.imp_con_factura, 0) + isnull(cantidad2, 0) * isnull(cgTab2.imp_con_factura, 0) as importe_autorizado " +
                                                            "     , case when cg_usuario_alim.id_usuario is not null then 0 else alimentos end as alimentos " +
                                                            "     , reqFirmaD " +
                                                            "from cg_concepto_comp " +
                                                            "  left join cg_tabulador as cgTab1 on cg_concepto_comp.cve_concepto1 = cgTab1.cve_concepto and cgTab1.cve_puesto = @idPuesto " +
                                                            "  left join cg_tabulador as cgTab2 on cg_concepto_comp.cve_concepto2 = cgTab2.cve_concepto and cgTab2.cve_puesto = @idPuesto " +
                                                            "  left join cg_usuario_alim on cg_usuario_alim.id_usuario = @idUsr " +
                                                            "where id_concepto_comp = @idConcepto ", ConexionBD)
                sdaTabulador.SelectCommand.Parameters.AddWithValue("@idConcepto", idConcepto)
                sdaTabulador.SelectCommand.Parameters.AddWithValue("@idPuesto", _txtPuestoTab.Text)
                sdaTabulador.SelectCommand.Parameters.AddWithValue("@idUsr", _txtIdUsuario.Text)
                ConexionBD.Open()
                sdaTabulador.Fill(dsTabulador)
                ConexionBD.Close()

                'Determinar si el concepto corresponde a Alimentos
                If dsTabulador.Tables(0).Rows(0).Item("alimentos").ToString() = 1 Then
                    noPersonas = 1
                    upNoPers.Update()
                End If

                hlProveedor.Text = dsFactura.Tables(0).Rows(0).Item("razon_emisor").ToString()
                'hlProveedor.NavigateUrl = "http://localhost/ProcAd - Adjuntos/" + .lblFolio.Text + "TabCon-" + dsSol.Tables(0).Rows(0).Item("tabla_comp").ToString()
                hlProveedor.NavigateUrl = dsFactura.Tables(0).Rows(0).Item("path").ToString()
                upProveedor.Update()

                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                'Validar Número de Personas para caso de Hosedaje de Mecánicos
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select case when concepto = 'HOSPEDAJE' and @idPuesto = 'Mec' then cast(ROUND(@noPers/2.0,0) as int) else @noPers end as noPers " +
                                         "from cg_concepto_comp " +
                                         "where id_concepto_comp = @idConcepto "
                SCMValores.Parameters.AddWithValue("@noPers", noPersonas)
                SCMValores.Parameters.AddWithValue("@idConcepto", idConcepto)
                SCMValores.Parameters.AddWithValue("@idPuesto", _txtPuestoTab.Text)
                ConexionBD.Open()
                noPersonas = SCMValores.ExecuteScalar
                ConexionBD.Close()

                If Val(dsTabulador.Tables(0).Rows(0).Item("no_conceptos").ToString()) = 0 Or Val(dsFactura.Tables(0).Rows(0).Item("importe").ToString()) <= (Val(dsTabulador.Tables(0).Rows(0).Item("importe_autorizado").ToString()) * noPersonas * noDias) Then
                    'No hay montos pre-autorizados para ese concepto / Por debajo del importe autorizado
                    wpePorcentAut.Value = 1
                    upPorcentAut.Update()
                    wceSubtotal.Value = Val(dsFactura.Tables(0).Rows(0).Item("subtotal").ToString())
                    wceSubtotal.Enabled = False
                    upSubtotal.Update()
                    wceIVA.Value = Val(dsFactura.Tables(0).Rows(0).Item("tot_tras_iva").ToString())
                    wceIVA.Enabled = False
                    upIVA.Update()
                    wceTotal.Value = Val(dsFactura.Tables(0).Rows(0).Item("importe").ToString())
                    wceTotal.Enabled = False
                    upTotal.Update()
                Else
                    'Sobrepasa el imprte autorizado
                    'Determinar el porcentaje autorizado con respecto a la Factura
                    Dim porAut As Decimal = 0
                    porAut = (Val(dsTabulador.Tables(0).Rows(0).Item("importe_autorizado").ToString()) * noPersonas * noDias) / Val(dsFactura.Tables(0).Rows(0).Item("importe").ToString())
                    'wceSubtotal.Value = Val(dsTabulador.Tables(0).Rows(0).Item("subtotal").ToString()) * noPersonas
                    wceSubtotal.Value = Val(dsFactura.Tables(0).Rows(0).Item("subtotal").ToString()) * porAut
                    wceSubtotal.Enabled = False
                    upSubtotal.Update()
                    'wceIVA.Value = Val(dsTabulador.Tables(0).Rows(0).Item("iva").ToString()) * noPersonas
                    wceIVA.Value = Val(dsFactura.Tables(0).Rows(0).Item("tot_tras_iva").ToString()) * porAut
                    wceIVA.Enabled = False
                    upIVA.Update()
                    'wceTotal.Value = Val(dsTabulador.Tables(0).Rows(0).Item("importe").ToString()) * noPersonas
                    wceTotal.Value = Val(dsFactura.Tables(0).Rows(0).Item("importe").ToString()) * porAut
                    wceTotal.Enabled = False
                    upTotal.Update()
                    wpePorcentAut.Value = porAut
                    upPorcentAut.Update()
                End If
                _txtIsrRet.Text = "0"
                _txtIsrRet.Text = CDbl(dsFactura.Tables(0).Rows(0).Item("tot_ret_isr").ToString())
                If dsTabulador.Tables(0).Rows(0).Item("reqFirmaD").ToString() = "S" Then
                    ._txtAutDir.Text = 1
                End If

                sdaFactura.Dispose()
                dsFactura.Dispose()
                sdaTabulador.Dispose()
                dsTabulador.Dispose()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub actTabulador(ByVal idConcepto, ByRef noPersonas, ByRef upNoPers, ByVal noDias, ByRef wceSubtotal, ByRef upSubtotal, ByRef wceTotal, ByRef upTotal)
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                'Datos de Conceptos
                Dim sdaTabulador As New SqlDataAdapter
                Dim dsTabulador As New DataSet
                sdaTabulador.SelectCommand = New SqlCommand("select no_conceptos, cg_concepto.id_tipoComp " +
                                                            "     , isnull(cantidad1, 0) * isnull(cgTab1.imp_sin_factura, 0) + isnull(cantidad2, 0) * isnull(cgTab2.imp_sin_factura, 0) as importe_autorizado " +
                                                            "     , case when cg_usuario_alim.id_usuario is not null then 0 else alimentos end as alimentos " +
                                                            "from cg_concepto " +
                                                            "  left join cg_tabulador as cgTab1 on cg_concepto.cve_concepto1 = cgTab1.cve_concepto and cgTab1.cve_puesto = @idPuesto " +
                                                            "  left join cg_tabulador as cgTab2 on cg_concepto.cve_concepto2 = cgTab2.cve_concepto and cgTab2.cve_puesto = @idPuesto " +
                                                            "  left join cg_usuario_alim on cg_usuario_alim.id_usuario = @idUsr " +
                                                            "where id_concepto = @idConcepto ", ConexionBD)
                sdaTabulador.SelectCommand.Parameters.AddWithValue("@idConcepto", idConcepto)
                sdaTabulador.SelectCommand.Parameters.AddWithValue("@idPuesto", ._txtPuestoTab.Text)
                sdaTabulador.SelectCommand.Parameters.AddWithValue("@idUsr", ._txtIdUsuario.Text)
                ConexionBD.Open()
                sdaTabulador.Fill(dsTabulador)
                ConexionBD.Close()

                'Determinar si el concepto corresponde a Alimentos
                If dsTabulador.Tables(0).Rows(0).Item("alimentos").ToString() = 1 Then
                    noPersonas = 1
                    upNoPers.Update()
                End If

                If Val(dsTabulador.Tables(0).Rows(0).Item("no_conceptos").ToString()) = 0 Or wceSubtotal.Value <= (Val(dsTabulador.Tables(0).Rows(0).Item("importe_autorizado").ToString()) * noPersonas * noDias) Then
                    'No hay montos pre-autorizados para ese concepto / Por debajo del importe autorizado
                    wceSubtotal.Value = wceSubtotal.Value
                    upSubtotal.Update()
                    wceTotal.Value = wceSubtotal.Value
                    upTotal.Update()
                Else
                    'Sobrepasa el imprte autorizado
                    wceSubtotal.Value = Val(dsTabulador.Tables(0).Rows(0).Item("importe_autorizado").ToString()) * noPersonas * noDias
                    upSubtotal.Update()
                    wceTotal.Value = Val(dsTabulador.Tables(0).Rows(0).Item("importe_autorizado").ToString()) * noPersonas * noDias
                    upTotal.Update()
                End If

                If Val(dsTabulador.Tables(0).Rows(0).Item("id_tipoComp").ToString()) = 2 Then
                    ._txtAutDir.Text = 1
                End If

                sdaTabulador.Dispose()
                dsTabulador.Dispose()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Function valNoFacturas()
        With Me
            Dim banF As Integer = 0

            If cbFactura1.Checked = True And ddlNoFactura1.Items.Count = 0 Then
                banF = 1
            End If

            If banF = 0 Then
                Return True
            Else
                Return False
            End If
        End With
    End Function

    Public Function valDate()
        With Me
            Dim banError As Integer = 0

            If pnlConcepto1.Visible = True Then
                If wdpFecha1.Value < wdteFechaIni.Value Or wdpFecha1.Value > wdteFechaFin.Value Then
                    upDateV1.Visible = True
                Else
                    upDateV1.Visible = False
                    banError = 1
                End If
            Else
            End If

            If banError = 1 Then
                Return True
            Else
                Return False
            End If
        End With
    End Function

    Public Function valOrigDest()
        With Me
            Dim banOD As Integer = 0

            If (.ddlOrig1.SelectedValue = 0 And .ddlDest1.SelectedValue > 0) Or (.ddlOrig1.SelectedValue > 0 And .ddlDest1.SelectedValue = 0) Then
                banOD = 1
            Else
                If .ddlOrig1.SelectedValue = 0 Then
                    .txtOriDes1.Text = ""
                Else
                    .txtOriDes1.Text = .ddlOrig1.SelectedItem.Text + " - " + .ddlDest1.SelectedItem.Text
                End If
            End If

            If banOD = 0 Then
                Return True
            Else
                Return False
            End If
        End With
    End Function

    Public Sub valDev(ByRef ddlConcepto)
        With Me
            If ._txtDev.Text <> 1 And (ddlConcepto.SelectedItem.Text = "GA - DEVOLUCION DE EFECTIVO" Or ddlConcepto.SelectedItem.Text = "GA - DEVOLUCION POR TRANSFERENCIA" Or ddlConcepto.SelectedItem.Text = "GOI - DEVOLUCION DE EFECTIVO" Or ddlConcepto.SelectedItem.Text = "GOI - DEVOLUCION POR TRANSFERENCIA") Then
                ._txtDev.Text = 1
                .upDev.Update()
            End If
        End With
    End Sub

    Public Function conceptoPGV(tipo, idConcepto)
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                If tipo = "F" Then
                    SCMValores.CommandText = "select count(*) " +
                                             "from cg_concepto_comp " +
                                             "  left join cg_concepto_cat on cg_concepto_comp.id_concepto_cat = cg_concepto_cat.id_concepto_cat " +
                                             "where id_concepto_comp = @idConcepto " +
                                             "  and gastos_viaje = 'S' "
                Else
                    SCMValores.CommandText = "select count(*) " +
                                             "from cg_concepto " +
                                             "  left join cg_concepto_cat on cg_concepto.id_concepto_cat = cg_concepto_cat.id_concepto_cat " +
                                             "where id_concepto = @idConcepto " +
                                             "  and gastos_viaje = 'S' "
                End If
                SCMValores.Parameters.AddWithValue("@idConcepto", idConcepto)
                ConexionBD.Open()
                conceptoPGV = SCMValores.ExecuteScalar
                ConexionBD.Close()
            Catch ex As Exception
                .litError.Text = ex.ToString
                conceptoPGV = 0
            End Try
        End With
    End Function

    Public Sub sumarConceptos()
        With Me
            Try
                .litError.Text = ""
                If rbdOpcionAnticipo.Text = "Anticipo AMEX" Or _txtAnticipo.Text = "Anticipo AMEX" And cbTabulador1.Checked = True Then
                    litError.Text = "Solo puede comprobar facturas con el anticipo AMEX"
                Else
                    If .txtJust.Text.Trim = "" Then
                        .litError.Text = "Información Insuficiente, favor de ingresar las Justificación correspondiente"
                    Else
                        If valDate() = False Then

                        Else
                            Dim totalAnt As Integer = 0
                            Dim columnas As Integer = 0
                            wceTotalA.Value = 0
                            wceTotalAPGV.Value = 0
                            _txtDev.Text = 0

                            If rbdOpcionAnticipo.Text = "Anticipo AMEX" Or _txtAnticipo.Text = "Anticipo AMEX" Then
                                For Each row As GridViewRow In gvAnticiposAmex.Rows
                                    If row.RowType = DataControlRowType.DataRow Then
                                        Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                                        If chkRow.Checked Then
                                            totalAnt = totalAnt + 1
                                            wceTotalA.Value = wceTotalA.Value + Val(row.Cells(3).Text)
                                            wceTotalAPGV.Value = wceTotalAPGV.Value + Val(row.Cells(4).Text)
                                        End If
                                    End If
                                Next
                                columnas = gvAnticiposAmex.Rows.Count()
                            Else
                                For Each row As GridViewRow In gvAnticipos.Rows
                                    If row.RowType = DataControlRowType.DataRow Then
                                        Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                                        If chkRow.Checked Then
                                            totalAnt = totalAnt + 1
                                            wceTotalA.Value = wceTotalA.Value + Val(row.Cells(3).Text)
                                            wceTotalAPGV.Value = wceTotalAPGV.Value + Val(row.Cells(4).Text)
                                        End If
                                    End If
                                Next
                                columnas = gvAnticipos.Rows.Count()

                            End If



                            If columnas > 0 And totalAnt = 0 And Val(_txtAntPend.Text) = 0 Then
                                .litError.Text = "Favor de seleccionar el Anticipo a Comprobar"
                            Else
                                If valNoFacturas() Then
                                    If valOrigDest() Then
                                        Dim banC As Integer = 0
                                        Dim total As Decimal
                                        Dim totalPGV As Decimal
                                        Dim tipo As String

                                        'For index As Integer = 0 To gvProducto.Rows.Count - 1
                                        '    Dim tipoConcepto As String = gvProducto.Rows(index).Cells(2).Text
                                        '    If tipoConcepto = "Factura" Then
                                        '        tipo = "F"
                                        '        ' ESTA LINEA ERA DE LOS COMENTARIOS NO OBLIGATORIOS
                                        '        actFactura(.ddlConcepto1.SelectedValue, .wneNoPers1.Value, .upNoPers1, .wneNoDias1.Value, .ddlNoFactura1.SelectedValue, .hlProveedor1, .upProveedor1, .wceSubtotal1, .upSubtotal1, .wceIVA1, .upIVA1, .wceTotal1, .upTotal1, .wpePorcentAut1, .upPorcentAut1)

                                        '    Else
                                        '        tipo = "T"
                                        '    End If



                                        'Next

                                        'Concepto 
                                        If .cbFactura1.Checked = True Then
                                            tipo = "F"
                                            .lblObsE1.Visible = False
                                            actFactura(.ddlConcepto1.SelectedValue, .wneNoPers1.Value, .upNoPers1, .wneNoDias1.Value, .ddlNoFactura1.SelectedValue, .hlProveedor1, .upProveedor1, .wceSubtotal1, .upSubtotal1, .wceIVA1, .upIVA1, .wceTotal1, .upTotal1, .wpePorcentAut1, .upPorcentAut1)
                                        Else
                                            tipo = "T"
                                            .lblNoFacturaE1.Visible = False
                                            If .cbTabulador1.Checked = True And .txtObs1.Text.Trim = "" Then
                                                .lblObsE1.Visible = True
                                                banC = 1
                                            Else
                                                .lblObsE1.Visible = False
                                            End If
                                            actTabulador(.ddlConcepto1.SelectedValue, .wneNoPers1.Value, .upNoPers1, .wneNoDias1.Value, .wceSubtotal1, .upSubtotal1, .wceTotal1, .upTotal1)
                                            valDev(.ddlConcepto1)
                                        End If
                                        If conceptoPGV(tipo, .ddlConcepto1.SelectedValue) = 1 Then
                                            totalPGV = totalPGV + .wceTotal1.Value
                                        End If
                                        total = total + .wceTotal1.Value

                                        'Asignar total a etiqueta
                                        wceTotalPGV.Value = wceTotalPGV.Value + totalPGV
                                        wceTotalC.Value = wceTotalC.Value + total
                                        wceTotalS.Value = wceTotalA.Value - wceTotalC.Value
                                        lblTotalA.Text = wceTotalA.Text
                                        lblTotalC.Text = wceTotalC.Text
                                        lblTotalS.Text = wceTotalS.Text

                                        If Val(._txtDev.Text) > 0 Then
                                            upValeIng.Visible = True
                                            upValeIng.Update()
                                        Else
                                            upValeIng.Visible = False
                                            upValeIng.Update()
                                        End If

                                        If Val(._txtAutDir.Text) > 0 Then
                                            lbl_Director.Visible = True
                                            ddlDirector.Visible = True
                                        Else
                                            'Validar Límite de Importe para Autorización de Director
                                            Dim limAutDir As Decimal
                                            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                                            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                                            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                                            SCMValores.Connection = ConexionBD
                                            SCMValores.CommandText = ""
                                            SCMValores.Parameters.Clear()
                                            SCMValores.CommandText = "select format(isnull(limite_aut_dir, -1), '########0.##'), 'es_MX' " +
                                                                     "from cg_usuario " +
                                                                     "where id_usuario = @idUsuario "
                                            SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                                            ConexionBD.Open()
                                            limAutDir = SCMValores.ExecuteScalar
                                            ConexionBD.Close()

                                            If (((wceTotalS.Value * -1) >= limAutDir) Or (wceTotalC.Value >= limAutDir)) And limAutDir <> -1 Then
                                                lbl_Director.Visible = True
                                                ddlDirector.Visible = True
                                            Else
                                                lbl_Director.Visible = False
                                                ddlDirector.Visible = False
                                            End If
                                        End If

                                        If _txtBotonCancelar.Text = "S" Then
                                            _txtBotonCancelar.Text = "N"
                                        Else
                                            'Validación de información complenta
                                            If banC = 1 Then
                                                litError.Text = "Información incompleta, favor de ingresar las observaciones correspondientes"
                                                btnGuardar.Enabled = False
                                            Else
                                                If ddlCC.Visible = True And _txtOmitValPGV.Text = "N" Then
                                                    Dim banPGV As Integer = 0
                                                    If wceTotalPGV.Value > wceTotalAPGV.Value Then



                                                        ''Obtener el Presupuesto Disponible del Centro de Costo
                                                        'Dim montoPresupDisp As Integer = 0
                                                        'Dim mes As String
                                                        'If .wdteFechaFin.Date.Month() < 10 Then
                                                        '    mes = "0" + .wdteFechaFin.Date.Month().ToString
                                                        'Else
                                                        '    mes = .wdteFechaFin.Date.Month().ToString
                                                        'End If

                                                        'Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                                                        'ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                                                        'Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                                                        'SCMValores.Connection = ConexionBD
                                                        'SCMValores.CommandText = ""
                                                        'SCMValores.Parameters.Clear()
                                                        ''- - - Versión Mensual - - -
                                                        'SCMValores.CommandText = "select mes_" + mes + "_p + mes_" + mes + "_a - mes_" + mes + "_ep - mes_" + mes + "_r " +
                                                        '                         "from ms_presupuesto " +
                                                        '                         "where id_cc = @idCC " +
                                                        '                         "  and año = @año "
                                                        ''- - - Versión Anual - - -
                                                        ''SCMValores.CommandText = "select mes_01_p + mes_01_a - mes_01_ep - mes_01_r + mes_02_p + mes_02_a - mes_02_ep - mes_02_r + mes_03_p + mes_03_a - mes_03_ep - mes_03_r + mes_04_p + mes_04_a - mes_04_ep - mes_04_r + mes_05_p + mes_05_a - mes_05_ep - mes_05_r + mes_06_p + mes_06_a - mes_06_ep - mes_06_r + mes_07_p + mes_07_a - mes_07_ep - mes_07_r + mes_08_p + mes_08_a - mes_08_ep - mes_08_r + mes_09_p + mes_09_a - mes_09_ep - mes_09_r + mes_10_p + mes_10_a - mes_10_ep - mes_10_r + mes_11_p + mes_11_a - mes_11_ep - mes_11_r + mes_12_p + mes_12_a - mes_12_ep - mes_12_r " +
                                                        ''                         "from ms_presupuesto " +
                                                        ''                         "where id_cc = @idCC " +
                                                        ''                         "  and año = @año "
                                                        'SCMValores.Parameters.AddWithValue("@idCC", ddlCC.SelectedValue)
                                                        'SCMValores.Parameters.AddWithValue("@año", wdteFechaFin.Date.Year())
                                                        'ConexionBD.Open()
                                                        'montoPresupDisp = SCMValores.ExecuteScalar()
                                                        'ConexionBD.Close()

                                                        ' implementacion Dayra

                                                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                                                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                                                        Dim sdaEmpleado As New SqlDataAdapter
                                                        Dim dsEmpleado As New DataSet
                                                        Dim montoAcumDisp As Double = 0

                                                        sdaEmpleado.SelectCommand = New SqlCommand("SP_C_ms_presupuesto_acum", ConexionBD)
                                                        sdaEmpleado.SelectCommand.CommandType = CommandType.StoredProcedure
                                                        sdaEmpleado.SelectCommand.Parameters.AddWithValue("@año", wdteFechaFin.Date.Year())
                                                        sdaEmpleado.SelectCommand.Parameters.AddWithValue("@centro_costo", ddlCC.SelectedValue())
                                                        sdaEmpleado.SelectCommand.Parameters.AddWithValue("@mes", wdteFechaFin.Date.Month())

                                                        ConexionBD.Open()
                                                        sdaEmpleado.Fill(dsEmpleado)
                                                        ConexionBD.Close()

                                                        If dsEmpleado.Tables(0).Rows.Count() > 0 Then
                                                            montoAcumDisp = dsEmpleado.Tables(0).Rows(0).Item("acumulado").ToString()
                                                        Else
                                                            litError.Text = "NO EXISTE REGISTRO DE PRESUPUESTO DE GASTOS DE VIAJE EN EL CENTRO DE COSTOS O DIVISION SELECCIONADA, FAVOR DE VALIDAR CON EL AREA"
                                                            Exit Sub
                                                        End If

                                                        sdaEmpleado.Dispose()
                                                        dsEmpleado.Dispose()

                                                        If montoAcumDisp < (wceTotalPGV.Value - wceTotalAPGV.Value) Then
                                                            .litError.Text = "El monto a comprobar excede el Presupuesto Disponible, favor de validarlo con el responsable del Centro de Costo para que solicite la Ampliación del Presupuesto de Gastos de Viaje en caso de que aplique"
                                                            banPGV = 1
                                                        Else
                                                            banPGV = 0
                                                        End If

                                                        'If montoPresupDisp < (wceTotalPGV.Value - wceTotalAPGV.Value) Then
                                                        '    .litError.Text = "El monto a comprobar excede el Presupuesto Disponible, favor de validarlo con el responsable del Centro de Costo para que solicite la Ampliación del Presupuesto de Gastos de Viaje en caso de que aplique"
                                                        '    banPGV = 1
                                                        'Else
                                                        '    banPGV = 0
                                                        'End If
                                                    End If
                                                    If banPGV = 0 Then
                                                        btnGuardar.Enabled = True
                                                    Else
                                                        .btnGuardar.Enabled = False
                                                    End If
                                                Else
                                                    .btnGuardar.Enabled = True
                                                End If
                                            End If
                                        End If


                                    Else
                                        .litError.Text = "Información inválida, favor de verificar los Origenes y Destinos"
                                    End If
                                Else
                                    .litError.Text = "Información incompleta, favor de verificar las facturas seleccionadas"
                                End If
                            End If
                        End If


                    End If
                End If

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Function valError()
        With Me
            Dim banError As Integer = 0

            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim SCMValoresDtFact As SqlCommand = New System.Data.SqlClient.SqlCommand
            SCMValoresDtFact.Connection = ConexionBD
            SCMValoresDtFact.Parameters.Clear()
            SCMValoresDtFact.CommandText = "select count(id_dt_factura) " +
                                           "from dt_factura " +
                                           "where id_dt_factura = @id_dt_factura " +
                                           "  and status <> 'P' "
            SCMValoresDtFact.Parameters.Add("@id_dt_factura", SqlDbType.Int)
            Dim valFacturaP As Integer

            If (.cbFactura1.Checked = True And .lblNoFacturaE1.Visible = True) Or (.cbTabulador1.Checked = True And .lblObsE1.Visible = True) Then
                banError = 1
            End If
            If .cbFactura1.Checked = True Then
                SCMValoresDtFact.Parameters("@id_dt_factura").Value = ddlNoFactura1.SelectedValue
                ConexionBD.Open()
                valFacturaP = SCMValoresDtFact.ExecuteScalar()
                ConexionBD.Close()
                If valFacturaP <> 0 Then
                    ._txtBan.Text = 1
                End If
            End If

            If banError = 1 Then
                Return True
            Else
                Return False
            End If
        End With
    End Function

    Public Function validarAMEX()
        Try
            Dim bandera As Boolean = True
            If _txtAnticipo.Text = "Anticipo AMEX" And wceTotalC.Value >= wceTotalA.Value Then
                bandera = True

            Else
                If _txtAnticipo.Text = "Anticipo" Then
                    bandera = True
                Else
                    bandera = False
                End If
            End If
            Return bandera
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Function
#End Region

#Region "Botones / Listas de Encabezado"

    Protected Sub ddlEmpresa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEmpresa.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                .ddlTipoGasto.Items.Clear()
                tipoGasto()
                .upTipoGasto.Update()
                .ddlDiv.Items.Clear()
                .ddlCC.Items.Clear()
                tipoDivCC()
                'Actualizar Autorizadores
                actAutorizadores()
                'Anticipos
                actAnticipos()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ddlTipoGasto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoGasto.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                .ddlDiv.Items.Clear()
                .ddlCC.Items.Clear()
                tipoDivCC()
                'Actualizar Autorizadores
                actAutorizadores()
                'Obtener la abreviatura del Gasto
                actAbreviatura()
                'Actualizar Conceptos
                actConceptos()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ddlCC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCC.SelectedIndexChanged
        Me.litError.Text = ""
        actIdCC()
        'Actualizar Conceptos
        actConceptos()
        'Actualizar Autorizadores
        actAutorizadores()
    End Sub

    Protected Sub ddlDiv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDiv.SelectedIndexChanged
        'Actualizar Autorizadores
        actAutorizadores()
    End Sub


#End Region

#Region "Botones de Conceptos"

#Region "No. 1"

    Protected Sub cbFactura1_CheckedChanged(sender As Object, e As EventArgs) Handles cbFactura1.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbFactura1.Checked = True Then
                tipo = "F"
                .lblObsE1.Visible = False
            Else
                tipo = "T"
                .lblNoFacturaE1.Visible = False
                If .cbTabulador1.Checked = True And .txtObs1.Text.Trim = "" Then
                    .lblObsE1.Visible = True
                Else
                    .lblObsE1.Visible = False
                End If
            End If
            .upObsE1.Update()
            tipoFT(tipo, .cbFactura1, .upFactura1, .cbTabulador1, .upTabulador1, .ddlConcepto1, .upConcepto1, .wneNoPers1.Value, .upNoPers1, .wneNoDias1.Value, .txtRFC1, .ibtnRFCBus1, .upRFC1, .ddlNoFactura1, .upNoFactura1, .hlProveedor1, .upProveedor1, .wceSubtotal1, .upSubtotal1, .wceIVA1, .upIVA1, .wceTotal1, .upTotal1, .wpePorcentAut1, .upPorcentAut1)
        End With
    End Sub

    Protected Sub cbTabulador1_CheckedChanged(sender As Object, e As EventArgs) Handles cbTabulador1.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbTabulador1.Checked = True Then
                tipo = "T"
                .lblNoFacturaE1.Visible = False
                If .cbTabulador1.Checked = True And .txtObs1.Text.Trim = "" Then
                    .lblObsE1.Visible = True
                Else
                    .lblObsE1.Visible = False
                End If
            Else
                tipo = "F"
                .lblObsE1.Visible = False
            End If
            .upObsE1.Update()
            tipoFT(tipo, .cbFactura1, .upFactura1, .cbTabulador1, .upTabulador1, .ddlConcepto1, .upConcepto1, .wneNoPers1.Value, .upNoPers1, .wneNoDias1.Value, .txtRFC1, .ibtnRFCBus1, .upRFC1, .ddlNoFactura1, .upNoFactura1, .hlProveedor1, .upProveedor1, .wceSubtotal1, .upSubtotal1, .wceIVA1, .upIVA1, .wceTotal1, .upTotal1, .wpePorcentAut1, .upPorcentAut1)
        End With
    End Sub

    Protected Sub ddlConcepto1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConcepto1.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                If Me.cbFactura1.Checked = True Then
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto_comp where id_concepto_comp = @id_concepto_comp "
                    SCMValores.Parameters.AddWithValue("@id_concepto_comp", ddlConcepto1.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    'actFactura(Me.ddlConcepto1.SelectedValue, Me.wneNoPers1.Value, Me.ddlNoFactura1.SelectedValue, Me.hlProveedor1, Me.upProveedor1, Me.wceSubtotal1, Me.upSubtotal1, Me.wceIVA1, Me.upIVA1, Me.wceTotal1, Me.upTotal1)
                    actNoFactura(Me.ddlConcepto1.SelectedValue, Me.wneNoPers1.Value, Me.upNoPers1, Me.wneNoDias1.Value, Me.txtRFC1, Me.ddlNoFactura1, Me.upNoFactura1, Me.hlProveedor1, Me.upProveedor1, Me.wceSubtotal1, Me.upSubtotal1, Me.wceIVA1, Me.upIVA1, Me.wceTotal1, Me.upTotal1, Me.wpePorcentAut1, Me.upPorcentAut1)
                Else
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto where id_concepto = @id_concepto "
                    SCMValores.Parameters.AddWithValue("@id_concepto", ddlConcepto1.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If

            Catch ex As Exception
                .litError.Text = ex.Message.ToString()
            End Try

        End With
    End Sub

    Protected Sub ibtnRFCBus1_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnRFCBus1.Click
        actNoFactura(Me.ddlConcepto1.SelectedValue, Me.wneNoPers1.Value, Me.upNoPers1, Me.wneNoDias1.Value, Me.txtRFC1, Me.ddlNoFactura1, Me.upNoFactura1, Me.hlProveedor1, Me.upProveedor1, Me.wceSubtotal1, Me.upSubtotal1, Me.wceIVA1, Me.upIVA1, Me.wceTotal1, Me.upTotal1, Me.wpePorcentAut1, Me.upPorcentAut1)
    End Sub

    Protected Sub ddlNoFactura1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoFactura1.SelectedIndexChanged
        actFactura(Me.ddlConcepto1.SelectedValue, Me.wneNoPers1.Value, Me.upNoPers1, Me.wneNoDias1.Value, Me.ddlNoFactura1.SelectedValue, Me.hlProveedor1, Me.upProveedor1, Me.wceSubtotal1, Me.upSubtotal1, Me.wceIVA1, Me.upIVA1, Me.wceTotal1, Me.upTotal1, Me.wpePorcentAut1, Me.upPorcentAut1)
    End Sub

#End Region


#End Region

#Region "Sumar / Guardar"

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        With Me
            Try
                'sumarConceptos()

                If Val(._txtBan.Text) = 1 Then
                    .litError.Text = "Ya se almacenó la comprobación previamente, favor de validarlo en el apartado de Consulta de Comprobaciones"
                Else

                    If validarAMEX() Then
                        If .wceTotalS.Value > 0 Then
                            .litError.Text = "Favor de ingresar la línea correspondiente para la devolución"
                        Else
                            Dim banVale As Integer = 0
                            If .upValeIng.Visible = True Then
                                If (Not ((Not fuValeIng.PostedFile Is Nothing) And (fuValeIng.PostedFile.ContentLength > 0))) Or .txtValeIng.Text.Trim = "" Then
                                    .litError.Text = "Favor de ingresar la información del Vale de Ingreso"
                                    banVale = 1
                                End If
                            End If
                            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                            SCMValores.Connection = ConexionBD
                            Dim cont As Integer
                            Dim contA As Integer = 0
                            Dim sin_comprobacion As String

                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "SELECT anticipo_obl, edit_compro_datos  FROM cg_usuario WHERE id_usuario =@id_usuario"
                            SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                            ConexionBD.Open()
                            sin_comprobacion = SCMValores.ExecuteScalar
                            ConexionBD.Close()
                            If sin_comprobacion = "N" Then
                                SCMValores.CommandText = ""
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "select count(*) from ms_anticipo" +
                                                     "               where id_usr_solicita = @idUsuario " +
                                                     "               and status in ('P', 'A', 'TR', 'EE', 'TRCP', 'EECP', 'TRCA', 'EECA') " +
                                                     "               and tipo = 'A'"
                                SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                                ConexionBD.Open()
                                cont = Val(SCMValores.ExecuteScalar)
                                ConexionBD.Close()
                                If cont = 0 Then
                                    SCMValores.CommandText = ""
                                    SCMValores.Parameters.Clear()
                                    Server.Transfer("Login.aspx")
                                End If
                            End If

                            'AMEX'
                            If rbdOpcionAnticipo.Text = "Anticipo AMEX" Or _txtAnticipo.Text = "Anticipo AMEX" Then
                                For Each row As GridViewRow In .gvAnticiposAmex.Rows
                                    If row.RowType = DataControlRowType.DataRow Then
                                        Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                                        If chkRow.Checked Then
                                            SCMValores.CommandText = ""
                                            SCMValores.Parameters.Clear()
                                            SCMValores.CommandText = " select(select COUNT(*) from dt_anticipo ant" +
                                                         "               left join ms_comp as com on com.id_ms_comp = ant.id_ms_comp " +
                                                         "               where id_ms_anticipo = @id_ms_anticipo and com.status in ('A','P')) as comp " +
                                                         "               from ms_anticipo where id_ms_anticipo =@id_ms_anticipo "
                                            SCMValores.Parameters.AddWithValue("@id_ms_anticipo", Val(row.Cells(0).Text))
                                            ConexionBD.Open()
                                            contA = Val(SCMValores.ExecuteScalar)
                                            ConexionBD.Close()
                                        End If
                                    End If
                                Next
                            Else
                                For Each row As GridViewRow In .gvAnticipos.Rows
                                    If row.RowType = DataControlRowType.DataRow Then
                                        Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                                        If chkRow.Checked Then
                                            SCMValores.CommandText = ""
                                            SCMValores.Parameters.Clear()
                                            SCMValores.CommandText = " select(select COUNT(*) from dt_anticipo ant" +
                                                         "               left join ms_comp as com on com.id_ms_comp = ant.id_ms_comp " +
                                                         "               where id_ms_anticipo = @id_ms_anticipo and com.status in ('A','P')) as comp " +
                                                         "               from ms_anticipo where id_ms_anticipo =@id_ms_anticipo "
                                            SCMValores.Parameters.AddWithValue("@id_ms_anticipo", Val(row.Cells(0).Text))
                                            ConexionBD.Open()
                                            contA = Val(SCMValores.ExecuteScalar)
                                            ConexionBD.Close()
                                        End If
                                    End If
                                Next
                            End If


                            If contA = 1 Then
                                .litError.Text = "Favor de Validar, un Anticipo seleccionado se encuentra asociado a otra comprobación"
                                banVale = 1
                                SCMValores.CommandText = ""
                                SCMValores.Parameters.Clear()
                            End If


                            If banVale = 0 Then
                                'Validar que las fechas sean correctas' 
                                Dim bandera As Boolean = False
                                For index As Integer = 0 To gvProducto.Rows.Count - 1
                                    Dim fechaConcepto As Date = CDate(gvProducto.Rows(index).Cells(2).Text)

                                    If fechaConcepto > wdteFechaFin.Date Or fechaConcepto < wdteFechaIni.Date Then
                                        bandera = True
                                    End If
                                Next

                                If bandera = True Then
                                    litError.Text = "Alguna fecha de realizacion esta incorrecta"
                                Else

                                    While Val(._txtBan.Text) = 0
                                        Dim fecha As DateTime
                                        fecha = Date.Now
                                        ._txtCargComb.Text = 0

                                        'Vale de Ingreso
                                        ' '' Ruta Local
                                        'Dim sFileDir As String = "C:/ProcAd - Adjuntos/" 'Ruta en que se almacenará el archivo
                                        ' Ruta en Atenea
                                        Dim sFileDir As String = "D:\ProcAd - Adjuntos\" 'Ruta en que se almacenará el archivo
                                        Dim lMaxFileSize As Long = 10485760 'Equivalente a 10 Megabytes
                                        Dim sFileValeIng As String = ""
                                        If .upValeIng.Visible = True Then
                                            sFileValeIng = System.IO.Path.GetFileName(fuValeIng.PostedFile.FileName)
                                        End If

                                        Dim sFileEvidencia As String = ""
                                        sFileEvidencia = System.IO.Path.GetFileName(fuEvidencia.PostedFile.FileName)
                                        Dim sdaEmpleado As New SqlDataAdapter
                                        Dim dsEmpleado As New DataSet
                                        Dim query As String
                                        query = "select cgEmpl.no_empleado as no_empleadoE " +
                                                    "     , cgUsrA.id_usuario as id_usr_aut " +
                                                    "     , cgUsrV.id_usuario as id_usr_val " +
                                                    "     , cgVal.nombre + ' ' + cgVal.ap_paterno + ' ' + cgVal.ap_materno as Validador " +
                                                    "     , cgAut.no_empleado as no_empleadoA " +
                                                    "     , cgAut.nombre + ' ' + cgAut.ap_paterno + ' ' + cgAut.ap_materno as Autorizador " +
                                                    "     , cgUsrD.id_usuario as id_usr_dir " +
                                                    "     , cgDir.no_empleado as no_empleadoD " +
                                                    "     , cgDir.nombre + ' ' + cgDir.ap_paterno + ' ' + cgDir.ap_materno as Director " +
                                                    "from bd_empleado.dbo.cg_empleado cgEmpl " +
                                                    "  left join cg_usuario cgUsrE on cgEmpl.id_empleado = cgUsrE.id_empleado " +
                                                    "  left join bd_Empleado.dbo.cg_empleado cgVal on cgVal.id_empleado = @idVal " +
                                                    "  left join bd_Empleado.dbo.cg_empleado cgAut on cgAut.id_empleado = @idAut " +
                                                    "  left join cg_usuario cgUsrA on cgAut.id_empleado = cgUsrA.id_empleado " +
                                                    "  left join cg_usuario cgUsrV on cgVal.id_empleado = cgUsrV.id_empleado " +
                                                    "  left join bd_Empleado.dbo.cg_empleado cgDir on cgDir.id_empleado = @idDir " +
                                                    "  left join cg_usuario cgUsrD on cgDir.id_empleado = cgUsrD.id_empleado " +
                                                    "where cgUsrE.id_usuario = @idUsr "
                                        sdaEmpleado.SelectCommand = New SqlCommand(query, ConexionBD)
                                        sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idUsr", Val(._txtIdUsuario.Text))
                                        If .ddlValidador.Visible = True Then
                                            sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idVal", .ddlValidador.SelectedValue)
                                        Else
                                            sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idVal", 0)
                                        End If
                                        sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idAut", .ddlAutorizador.SelectedValue)
                                        If .ddlDirector.Visible = True Then
                                            sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idDir", .ddlDirector.SelectedValue)
                                        Else
                                            sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idDir", 0)
                                        End If
                                        ConexionBD.Open()
                                        sdaEmpleado.Fill(dsEmpleado)
                                        ConexionBD.Close()

                                        'Insertar Comprobación

                                        SCMValores.CommandText = ""
                                        SCMValores.Parameters.Clear()
                                        SCMValores.CommandText = "insert into ms_comp ( id_usr_solicita,  fecha_solicita,  id_usr_autoriza,  empresa,  periodo_comp,  periodo_ini,  periodo_fin,  tipo_gasto,  tipo_actividad,  centro_costo,  division,  no_empleado,  empleado,  no_autorizador,  autorizador,  justificacion,  importe_tot,  vale_ingreso,  vale_ingreso_imp,  vale_ingreso_adj,  evidencia_adj,  aut_dir,  id_usr_aut_dir,  no_director,  director,  id_cc,  año_pgv,  mes_pgv,  monto_pgv_ep,  monto_pgv_ex, status , aut_val , id_validador, nombre_validador, AMEX) " +
                                                                     " 			   values (@id_usr_solicita, @fecha_solicita, @id_usr_autoriza, @empresa, @periodo_comp, @periodo_ini, @periodo_fin, @tipo_gasto, @tipo_actividad, @centro_costo, @division, @no_empleado, @empleado, @no_autorizador, @autorizador, @justificacion, @importe_tot, @vale_ingreso, @vale_ingreso_imp, @vale_ingreso_adj, @evidencia_adj, @aut_dir, @id_usr_aut_dir, @no_director, @director, @id_cc, @año_pgv, @mes_pgv, @monto_pgv_ep, @monto_pgv_ex, 'P', @aut_val , @id_validador , @nombre_validador, @AMEX) "
                                        SCMValores.Parameters.AddWithValue("@id_usr_solicita", Val(._txtIdUsuario.Text))
                                        SCMValores.Parameters.AddWithValue("@fecha_solicita", fecha)
                                        SCMValores.Parameters.AddWithValue("@id_usr_autoriza", Val(dsEmpleado.Tables(0).Rows(0).Item("id_usr_aut").ToString()))
                                        SCMValores.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedItem.Text)
                                        SCMValores.Parameters.AddWithValue("@periodo_comp", "Del " + .wdteFechaIni.Text + " al " + .wdteFechaFin.Text)
                                        SCMValores.Parameters.AddWithValue("@periodo_ini", .wdteFechaIni.Date)
                                        SCMValores.Parameters.AddWithValue("@periodo_fin", .wdteFechaFin.Date)
                                        SCMValores.Parameters.AddWithValue("@tipo_gasto", .ddlTipoGasto.SelectedItem.Text)
                                        SCMValores.Parameters.AddWithValue("@tipo_actividad", "")
                                        If .ddlCC.Visible = True Then
                                            SCMValores.Parameters.AddWithValue("@id_cc", .ddlCC.SelectedValue)
                                            SCMValores.Parameters.AddWithValue("@centro_costo", .ddlCC.SelectedItem.Text)
                                            SCMValores.Parameters.AddWithValue("@año_pgv", .wdteFechaFin.Date.Year())
                                            SCMValores.Parameters.AddWithValue("@mes_pgv", .wdteFechaFin.Date.Month())
                                            If .wceTotalA.Value > 0 Then
                                                If .wceTotalPGV.Value > .wceTotalAPGV.Value Then
                                                    SCMValores.Parameters.AddWithValue("@monto_pgv_ep", .wceTotalAPGV.Value)
                                                Else
                                                    SCMValores.Parameters.AddWithValue("@monto_pgv_ep", .wceTotalPGV.Value)
                                                End If
                                            Else
                                                SCMValores.Parameters.AddWithValue("@monto_pgv_ep", DBNull.Value)
                                            End If
                                            If .wceTotalPGV.Value - .wceTotalAPGV.Value > 0 Then
                                                SCMValores.Parameters.AddWithValue("@monto_pgv_ex", .wceTotalPGV.Value - .wceTotalAPGV.Value)
                                            Else
                                                SCMValores.Parameters.AddWithValue("@monto_pgv_ex", DBNull.Value)
                                            End If
                                        Else
                                            SCMValores.Parameters.AddWithValue("@id_cc", DBNull.Value)
                                            SCMValores.Parameters.AddWithValue("@centro_costo", DBNull.Value)
                                            SCMValores.Parameters.AddWithValue("@año_pgv", DBNull.Value)
                                            SCMValores.Parameters.AddWithValue("@mes_pgv", DBNull.Value)
                                            SCMValores.Parameters.AddWithValue("@monto_pgv_ep", DBNull.Value)
                                            SCMValores.Parameters.AddWithValue("@monto_pgv_ex", DBNull.Value)
                                        End If
                                        If .ddlDiv.Visible = True Then
                                            SCMValores.Parameters.AddWithValue("@division", .ddlDiv.SelectedItem.Text)
                                        Else
                                            SCMValores.Parameters.AddWithValue("@division", DBNull.Value)
                                        End If
                                        SCMValores.Parameters.AddWithValue("@no_empleado", dsEmpleado.Tables(0).Rows(0).Item("no_empleadoE").ToString())
                                        SCMValores.Parameters.AddWithValue("@empleado", .lblEmpleado.Text)
                                        SCMValores.Parameters.AddWithValue("@no_autorizador", dsEmpleado.Tables(0).Rows(0).Item("no_empleadoA").ToString())
                                        SCMValores.Parameters.AddWithValue("@autorizador", dsEmpleado.Tables(0).Rows(0).Item("Autorizador").ToString())
                                        SCMValores.Parameters.AddWithValue("@justificacion", .txtJust.Text.Trim)
                                        SCMValores.Parameters.AddWithValue("@importe_tot", .wceTotalS.Value)
                                        If .upValeIng.Visible = True Then
                                            'Incluye Vale de Ingreso
                                            SCMValores.Parameters.AddWithValue("@vale_ingreso", .txtValeIng.Text.Trim)
                                            SCMValores.Parameters.AddWithValue("@vale_ingreso_adj", sFileValeIng)
                                            SCMValores.Parameters.AddWithValue("@vale_ingreso_imp", .wceTotalS.Value)
                                        Else
                                            'Sin Vale de Ingreso
                                            SCMValores.Parameters.AddWithValue("@vale_ingreso", DBNull.Value)
                                            SCMValores.Parameters.AddWithValue("@vale_ingreso_adj", DBNull.Value)
                                            SCMValores.Parameters.AddWithValue("@vale_ingreso_imp", DBNull.Value)
                                        End If
                                        If (Not fuEvidencia.PostedFile Is Nothing) And (fuEvidencia.PostedFile.ContentLength > 0) Then
                                            SCMValores.Parameters.AddWithValue("@evidencia_adj", sFileEvidencia)
                                        Else
                                            SCMValores.Parameters.AddWithValue("@evidencia_adj", DBNull.Value)
                                        End If
                                        If .ddlDirector.Visible = True Then
                                            SCMValores.Parameters.AddWithValue("@aut_dir", "S")
                                            SCMValores.Parameters.AddWithValue("@id_usr_aut_dir", Val(dsEmpleado.Tables(0).Rows(0).Item("id_usr_dir").ToString()))
                                            SCMValores.Parameters.AddWithValue("@no_director", dsEmpleado.Tables(0).Rows(0).Item("no_empleadoD").ToString())
                                            SCMValores.Parameters.AddWithValue("@director", dsEmpleado.Tables(0).Rows(0).Item("Director").ToString())
                                        Else
                                            SCMValores.Parameters.AddWithValue("@aut_dir", "N")
                                            SCMValores.Parameters.AddWithValue("@id_usr_aut_dir", DBNull.Value)
                                            SCMValores.Parameters.AddWithValue("@no_director", DBNull.Value)
                                            SCMValores.Parameters.AddWithValue("@director", DBNull.Value)
                                        End If
                                        If .ddlValidador.Visible = True Then
                                            SCMValores.Parameters.AddWithValue("@aut_val", "S")
                                            SCMValores.Parameters.AddWithValue("@id_validador", dsEmpleado.Tables(0).Rows(0).Item("id_usr_val").ToString())
                                            SCMValores.Parameters.AddWithValue("@nombre_validador", dsEmpleado.Tables(0).Rows(0).Item("Validador").ToString())
                                        Else
                                            SCMValores.Parameters.AddWithValue("@aut_val", "N")
                                            SCMValores.Parameters.AddWithValue("@id_validador", DBNull.Value)
                                            SCMValores.Parameters.AddWithValue("@nombre_validador", DBNull.Value)
                                        End If
                                        If _txtAnticipo.Text = "Anticipo AMEX" Or rbdOpcionAnticipo.Text = "Anticipo AMEX" Then
                                            SCMValores.Parameters.AddWithValue("@AMEX", "S")
                                        Else
                                            SCMValores.Parameters.AddWithValue("@AMEX", DBNull.Value)
                                        End If
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()
                                        sdaEmpleado.Dispose()
                                        dsEmpleado.Dispose()

                                        'Obtener ID de la Comprobación
                                        SCMValores.CommandText = "select max(id_ms_comp) from ms_comp where id_usr_solicita = @id_usr_solicita and status not in ('Z') "
                                        ConexionBD.Open()
                                        lblFolio.Text = SCMValores.ExecuteScalar
                                        ConexionBD.Close()
                                        If Val(.lblFolio.Text) > 0 Then
                                            ._txtBan.Text = 1
                                        End If

                                        'Actualizar montos de presupuesto de Centro de Costo en caso de que aplique
                                        If .ddlCC.Visible = True Then
                                            Dim sdaPGV As New SqlDataAdapter
                                            Dim dsPGV As New DataSet
                                            sdaPGV.SelectCommand = New SqlCommand("select (select isnull(sum(monto_pgv_ep), 0) as msAntEP " +
                                                                                      "        from ms_recursos " +
                                                                                      "        where id_cc = @idCC " +
                                                                                      "          and año_pgv = @año " +
                                                                                      "          and mes_pgv = @mes) " +
                                                                                      "      + " +
                                                                                      "       (select isnull(sum(monto_pgv_ex), 0) as msCompEx " +
                                                                                      "        from ms_comp " +
                                                                                      "        where id_cc = @idCC " +
                                                                                      "          and año_pgv = @año " +
                                                                                      "          and mes_pgv = @mes) as pgvEP " +
                                                                                      "     , (select isnull(sum(monto_pgv_r), 0) as msCompR " +
                                                                                      "        from ms_comp " +
                                                                                      "        where id_cc = @idCC " +
                                                                                      "          and año_pgv = @año " +
                                                                                      "          and mes_pgv = @mes) as pgvR ", ConexionBD)
                                            sdaPGV.SelectCommand.Parameters.AddWithValue("@idCC", .ddlCC.SelectedValue)
                                            sdaPGV.SelectCommand.Parameters.AddWithValue("@año", .wdteFechaFin.Date.Year())
                                            sdaPGV.SelectCommand.Parameters.AddWithValue("@mes", .wdteFechaFin.Date.Month())
                                            ConexionBD.Open()
                                            sdaPGV.Fill(dsPGV)
                                            ConexionBD.Close()

                                            Dim mes As String
                                            If .wdteFechaFin.Date.Month() < 10 Then
                                                mes = "0" + .wdteFechaFin.Date.Month().ToString
                                            Else
                                                mes = .wdteFechaFin.Date.Month().ToString
                                            End If
                                            SCMValores.CommandText = "update ms_presupuesto " +
                                                                         "  set mes_" + mes + "_ep = @pgvEP, mes_" + mes + "_r = @pgvR " +
                                                                         "where id_cc = @idCC " +
                                                                         "  and año = @año "
                                            SCMValores.Parameters.AddWithValue("@pgvEP", dsPGV.Tables(0).Rows(0).Item("pgvEP").ToString())
                                            SCMValores.Parameters.AddWithValue("@pgvR", dsPGV.Tables(0).Rows(0).Item("pgvR").ToString())
                                            SCMValores.Parameters.AddWithValue("@idCC", .ddlCC.SelectedValue)
                                            SCMValores.Parameters.AddWithValue("@año", .wdteFechaFin.Date.Year)
                                            ConexionBD.Open()
                                            SCMValores.ExecuteNonQuery()
                                            ConexionBD.Close()
                                        End If

                                        'Guardar Vale de Ingreso en caso de Existir
                                        If .upValeIng.Visible = True Then
                                            'Se agrega el Id al nombre del archivo / Almacenar el archivo en la ruta especificada
                                            sFileValeIng = .lblFolio.Text + "ValeI-" + sFileValeIng
                                            fuValeIng.PostedFile.SaveAs(sFileDir + sFileValeIng)
                                        End If
                                        If (Not fuEvidencia.PostedFile Is Nothing) And (fuEvidencia.PostedFile.ContentLength > 0) Then
                                            'Se agrega el Id al nombre del archivo / Almacenar el archivo en la ruta especificada
                                            sFileEvidencia = .lblFolio.Text + "Evid-" + sFileEvidencia
                                            fuEvidencia.PostedFile.SaveAs(sFileDir + sFileEvidencia)
                                        End If



                                        'Insertar Conceptos
                                        SCMValores.Parameters.Clear()
                                        SCMValores.CommandText = "insert into dt_comp ( id_ms_comp,  fecha_realizo,  tipo,  no_factura,  id_concepto,  nombre_concepto,  iva,  no_personas,  no_dias,  monto_subtotal,  monto_iva,  monto_total,  rfc,  proveedor,  origen_destino,  vehiculo,  obs,  id_lugar_orig,  lugar_orig,  id_lugar_dest,  lugar_dest,  CFDI, isr_ret) " +
                                                                     " 			   values (@id_ms_comp, @fecha_realizo, @tipo, @no_factura, @id_concepto, @nombre_concepto, @iva, @no_personas, @no_dias, @monto_subtotal, @monto_iva, @monto_total, @rfc, @proveedor, @origen_destino, @vehiculo, @obs, @id_lugar_orig, @lugar_orig, @id_lugar_dest, @lugar_dest, @CFDI, @isr_ret)"
                                        SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                                        SCMValores.Parameters.Add("@fecha_realizo", SqlDbType.Date)
                                        SCMValores.Parameters.Add("@tipo", SqlDbType.VarChar)
                                        SCMValores.Parameters.Add("@CFDI", SqlDbType.VarChar)
                                        SCMValores.Parameters.Add("@no_factura", SqlDbType.VarChar)
                                        SCMValores.Parameters.Add("@rfc", SqlDbType.VarChar)
                                        SCMValores.Parameters.Add("@proveedor", SqlDbType.VarChar)
                                        SCMValores.Parameters.Add("@iva", SqlDbType.Decimal)
                                        SCMValores.Parameters.Add("@isr_ret", SqlDbType.Decimal)
                                        SCMValores.Parameters.Add("@id_concepto", SqlDbType.Int)
                                        SCMValores.Parameters.Add("@nombre_concepto", SqlDbType.VarChar)
                                        SCMValores.Parameters.Add("@no_personas", SqlDbType.Int)
                                        SCMValores.Parameters.Add("@no_dias", SqlDbType.Int)
                                        SCMValores.Parameters.Add("@monto_subtotal", SqlDbType.Money)
                                        SCMValores.Parameters.Add("@monto_iva", SqlDbType.Money)
                                        SCMValores.Parameters.Add("@monto_total", SqlDbType.Money)
                                        SCMValores.Parameters.Add("@origen_destino", SqlDbType.VarChar)
                                        SCMValores.Parameters.Add("@vehiculo", SqlDbType.VarChar)
                                        SCMValores.Parameters.Add("@obs", SqlDbType.VarChar)
                                        SCMValores.Parameters.Add("@id_lugar_orig", SqlDbType.Int)
                                        SCMValores.Parameters.Add("@lugar_orig", SqlDbType.VarChar)
                                        SCMValores.Parameters.Add("@id_lugar_dest", SqlDbType.Int)
                                        SCMValores.Parameters.Add("@lugar_dest", SqlDbType.VarChar)

                                        'Porcentaje de IVA
                                        Dim SCMValoresIVA As SqlCommand = New System.Data.SqlClient.SqlCommand
                                        SCMValoresIVA.Connection = ConexionBD
                                        SCMValoresIVA.Parameters.Clear()
                                        SCMValoresIVA.CommandText = "select iva " +
                                                                        "from cg_concepto_comp " +
                                                                        "where id_concepto_comp = @id_concepto "
                                        SCMValoresIVA.Parameters.Add("@id_concepto", SqlDbType.Int)
                                        'Actualización de dt_factura
                                        Dim SCMValoresDtFact As SqlCommand = New System.Data.SqlClient.SqlCommand
                                        SCMValoresDtFact.Connection = ConexionBD
                                        SCMValoresDtFact.Parameters.Clear()
                                        SCMValoresDtFact.CommandText = "update dt_factura " +
                                                                           "set status = 'CP' " +
                                                                           "where id_dt_factura = @id_dt_factura "
                                        SCMValoresDtFact.Parameters.Add("@id_dt_factura", SqlDbType.Int)
                                        'Obtener el id_dt_comp
                                        Dim SCMValoresDtComp As SqlCommand = New System.Data.SqlClient.SqlCommand
                                        SCMValoresDtComp.Connection = ConexionBD
                                        SCMValoresDtComp.Parameters.Clear()
                                        SCMValoresDtComp.CommandText = "select max(id_dt_comp) as id_dt_comp " +
                                                                            "from dt_comp " +
                                                                            "where CFDI = @uuid "
                                        SCMValoresDtComp.Parameters.Add("@uuid", SqlDbType.VarChar)

                                        'Determinar si es un concepto de combustible
                                        Dim contComb As Integer = 0
                                        Dim SCMContComb As SqlCommand = New System.Data.SqlClient.SqlCommand
                                        SCMContComb.Connection = ConexionBD
                                        SCMContComb.Parameters.Clear()
                                        SCMContComb.CommandText = "select count(*) " +
                                                                      "from cg_concepto_comp " +
                                                                      "where id_concepto_comp = @id_concepto_comp " +
                                                                      "  and combustible = 1 "
                                        SCMContComb.Parameters.Add("@id_concepto_comp", SqlDbType.Int)

                                        'Importes Factura
                                        Dim sdaImpFactura As New SqlDataAdapter
                                        Dim dsImpFactura As New DataSet
                                        sdaImpFactura.SelectCommand = New SqlCommand("select top 1 " +
                                                                                         "       dt_factura_linea.cantidad " +
                                                                                         "     , cast((dt_factura.importe / dt_factura_linea.cantidad) as decimal(18,6)) as precio_ticket " +
                                                                                         "     , cast(((dt_factura.subtotal + dt_factura.tot_tras_ieps) / dt_factura_linea.cantidad) as decimal(18,6)) as precio_sin_iva " +
                                                                                         "     , dt_factura.subtotal " +
                                                                                         "     , dt_factura.subtotal + dt_factura.tot_tras_ieps as importe_con_ieps " +
                                                                                         "     , dt_factura.tot_tras_iva " +
                                                                                         "     , dt_factura.tot_tras_ieps " +
                                                                                         "     , dt_factura.importe " +
                                                                                         "     , case when impuesto_tras_1 = 2 and tasa_tras_1 = 0.16 or impuesto_tras_2 = 2 and tasa_tras_2 = 0.16 or impuesto_tras_3 = 2 and tasa_tras_3 = 0.16 or impuesto_tras_4 = 2 and tasa_tras_4 = 0.16 then 0.16 " +
                                                                                         "            when impuesto_tras_1 = 2 and tasa_tras_1 = 0.08 or impuesto_tras_2 = 2 and tasa_tras_2 = 0.08 or impuesto_tras_3 = 2 and tasa_tras_3 = 0.08 or impuesto_tras_4 = 2 and tasa_tras_4 = 0.08 then 0.08 " +
                                                                                         "            when impuesto_tras_1 = 2 and tasa_tras_1 = 0 or impuesto_tras_2 = 2 and tasa_tras_2 = 0 or impuesto_tras_3 = 2 and tasa_tras_3 = 0 or impuesto_tras_4 = 2 and tasa_tras_4 = 0 then 0 " +
                                                                                         "       end * 100 as porcent_iva " +
                                                                                         "from dt_factura " +
                                                                                         "  left join dt_factura_linea on dt_factura.uuid = dt_factura_linea.uuid and dt_factura_linea.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                                                                         "where dt_factura.uuid = @uuid " +
                                                                                         "  and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') ", ConexionBD)
                                        sdaImpFactura.SelectCommand.Parameters.Add("@uuid", SqlDbType.VarChar)

                                        'Insertar Carga de Combustible por Comprobar
                                        Dim SCMCargaComb As SqlCommand = New System.Data.SqlClient.SqlCommand
                                        SCMCargaComb.Connection = ConexionBD
                                        SCMCargaComb.Parameters.Clear()
                                        SCMCargaComb.CommandText = "insert into dt_carga_comb_tar ( id_usr_carga,  fecha_carga,  empresa,  centro_costos,  importe_con_ieps,  importe_sin_imp,  ieps,  iva,  importe_transaccion,  porcent_iva,  cantidad_mercancia,  precio_ticket,  precio_sin_iva,  id_conductor,  conductor,  razon_social_afiliado,  rfc,  obs,  status) " +
                                                                       " 			           values (@id_usr_carga, @fecha_carga, @empresa, @centro_costos, @importe_con_ieps, @importe_sin_imp, @ieps, @iva, @importe_transaccion, @porcent_iva, @cantidad_mercancia, @precio_ticket, @precio_sin_iva, @id_conductor, @conductor, @razon_social_afiliado, @rfc, @obs,  'P') "
                                        SCMCargaComb.Parameters.AddWithValue("@id_usr_carga", Val(._txtIdUsuario.Text))
                                        SCMCargaComb.Parameters.AddWithValue("@fecha_carga", fecha)
                                        SCMCargaComb.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedItem.Text)
                                        If .ddlCC.Visible = True Then
                                            SCMCargaComb.Parameters.AddWithValue("@centro_costos", .ddlCC.SelectedItem.Text)
                                        Else
                                            If .ddlDiv.Visible = True Then
                                                SCMCargaComb.Parameters.AddWithValue("@centro_costos", .ddlDiv.SelectedItem.Text)
                                            Else
                                                SCMCargaComb.Parameters.AddWithValue("@centro_costos", "")
                                            End If
                                        End If
                                        SCMCargaComb.Parameters.Add("@importe_con_ieps", SqlDbType.Money)
                                        SCMCargaComb.Parameters.Add("@importe_sin_imp", SqlDbType.Money)
                                        SCMCargaComb.Parameters.Add("@ieps", SqlDbType.Money)
                                        SCMCargaComb.Parameters.Add("@iva", SqlDbType.Money)
                                        SCMCargaComb.Parameters.Add("@importe_transaccion", SqlDbType.Money)
                                        SCMCargaComb.Parameters.Add("@porcent_iva", SqlDbType.Float)
                                        SCMCargaComb.Parameters.Add("@cantidad_mercancia", SqlDbType.Float)
                                        SCMCargaComb.Parameters.Add("@precio_ticket", SqlDbType.Money)
                                        SCMCargaComb.Parameters.Add("@precio_sin_iva", SqlDbType.Float)
                                        SCMCargaComb.Parameters.AddWithValue("@id_conductor", "")
                                        SCMCargaComb.Parameters.AddWithValue("@conductor", .lblEmpleado.Text)
                                        SCMCargaComb.Parameters.Add("@razon_social_afiliado", SqlDbType.VarChar)
                                        SCMCargaComb.Parameters.Add("@rfc", SqlDbType.VarChar)
                                        SCMCargaComb.Parameters.AddWithValue("@obs", .lblFolio.Text)

                                        Dim idDtComp As Integer = 0
                                        'Insertar en dt_comp_linea
                                        Dim SCMValoresDtCompL As SqlCommand = New System.Data.SqlClient.SqlCommand
                                        SCMValoresDtCompL.Connection = ConexionBD
                                        SCMValoresDtCompL.Parameters.Clear()
                                        SCMValoresDtCompL.CommandText = "insert into dt_comp_linea (id_dt_comp, importe, descuento, tasa_iva, iva, tasa_ieps, ieps, tasa_ret, isr) " +
                                                                            "select @idDtComp as id_dt_comp " +
                                                                            "     , importe * @porcentAuto as importe " +
                                                                            "	  , descuento * @porcentAuto as descuento " +
                                                                            "	  , case when impuesto_tras_1 = 2 and tasa_tras_1 = 0.16 or impuesto_tras_2 = 2 and tasa_tras_2 = 0.16 or impuesto_tras_3 = 2 and tasa_tras_3 = 0.16 or impuesto_tras_4 = 2 and tasa_tras_4 = 0.16 then 0.16  " +
                                                                            "		     when impuesto_tras_1 = 2 and tasa_tras_1 = 0.08 or impuesto_tras_2 = 2 and tasa_tras_2 = 0.08 or impuesto_tras_3 = 2 and tasa_tras_3 = 0.08 or impuesto_tras_4 = 2 and tasa_tras_4 = 0.08 then 0.08 " +
                                                                            "		     when impuesto_tras_1 = 2 and tasa_tras_1 = 0 or impuesto_tras_2 = 2 and tasa_tras_2 = 0 or impuesto_tras_3 = 2 and tasa_tras_3 = 0 or impuesto_tras_4 = 2 and tasa_tras_4 = 0 then 0 " +
                                                                            "	    end as tasa_iva " +
                                                                            "	  , isnull(tot_tras_iva, 0) * @porcentAuto as iva " +
                                                                            "	  , case when impuesto_tras_1 = 3 then tasa_tras_1 " +
                                                                            "		     when impuesto_tras_2 = 3 then tasa_tras_2 " +
                                                                            "		     when impuesto_tras_3 = 3 then tasa_tras_3 " +
                                                                            "			 when impuesto_tras_4 = 3 then tasa_tras_4 " +
                                                                            "	    end as tasa_ieps " +
                                                                            "	  , isnull(tot_tras_ieps, 0) * @porcentAuto as ieps " +
                                                                            "	  , isnull(tasa_ret_1, 0) * @porcentAuto as tasa_ret " +
                                                                            "	  , isnull(importe_ret_1, 0) * @porcentAuto as isr " +
                                                                            "from dt_factura_linea " +
                                                                            "where uuid = @uuid " +
                                                                            "  and movimiento in ('RECIBIDAS', 'RECIBIDA') "
                                        SCMValoresDtCompL.Parameters.Add("@idDtComp", SqlDbType.Int)
                                        SCMValoresDtCompL.Parameters.Add("@porcentAuto", SqlDbType.Decimal)
                                        SCMValoresDtCompL.Parameters.Add("@uuid", SqlDbType.VarChar)

                                        'checar
                                        gvProducto.Columns(1).Visible = True
                                        'gvProducto.Columns(18).Visible = True
                                        'gvProducto.Columns(19).Visible = True
                                        'gvProducto.Columns(20).Visible = True
                                        'gvProducto.Columns(21).Visible = True
                                        'gvProducto.Columns(22).Visible = True
                                        'gvProducto.Columns(23).Visible = True
                                        'gvProducto.Columns(24).Visible = True

                                        gvProducto.Columns(19).Visible = True
                                        gvProducto.Columns(20).Visible = True
                                        gvProducto.Columns(21).Visible = True
                                        gvProducto.Columns(22).Visible = True
                                        gvProducto.Columns(23).Visible = True
                                        gvProducto.Columns(24).Visible = True
                                        gvProducto.Columns(25).Visible = True
                                        gvProducto.Columns(26).Visible = True
                                        gvProducto.Columns(27).Visible = True

                                        For index As Integer = 0 To gvProducto.Rows.Count - 1

                                            'Datos de la Factura
                                            Dim sdaFactura As New SqlDataAdapter
                                            Dim dsFactura As New DataSet
                                            sdaFactura.SelectCommand = New SqlCommand("select uuid as cfdi " +
                                                                                      "     , serie + ' ' + folio as no_factura " +
                                                                                      "     , rfc_emisor as rfc " +
                                                                                      "     , razon_emisor as proveedor " +
                                                                                      "from dt_factura " +
                                                                                      "where id_dt_factura = @id_dt_factura " +
                                                                                      "  and movimiento in ('RECIBIDAS', 'RECIBIDA')", ConexionBD)
                                            sdaFactura.SelectCommand.Parameters.Add("@id_dt_factura", SqlDbType.Int)

                                            'PARA INSERTAR LOS CONCEPTOS'
                                            SCMValores.Parameters("@fecha_realizo").Value = gvProducto.Rows(index).Cells(2).Text
                                            SCMValores.Parameters("@id_concepto").Value = Val(gvProducto.Rows(index).Cells(1).Text)
                                            SCMValores.Parameters("@nombre_concepto").Value = gvProducto.Rows(index).Cells(4).Text
                                            SCMValores.Parameters("@no_personas").Value = Val(gvProducto.Rows(index).Cells(5).Text)
                                            SCMValores.Parameters("@no_dias").Value = Val(gvProducto.Rows(index).Cells(6).Text)
                                            SCMValores.Parameters("@monto_subtotal").Value = CDbl(gvProducto.Rows(index).Cells(7).Text.ToString)
                                            SCMValores.Parameters("@monto_iva").Value = CDbl(gvProducto.Rows(index).Cells(8).Text.ToString)
                                            SCMValores.Parameters("@monto_total").Value = CDbl(gvProducto.Rows(index).Cells(9).Text.ToString)
                                            If gvProducto.Rows(index).Cells(19).Text.ToString = "&nbsp;" Then

                                                SCMValores.Parameters("@origen_destino").Value = DBNull.Value
                                                SCMValores.Parameters("@id_lugar_orig").Value = DBNull.Value
                                                SCMValores.Parameters("@lugar_orig").Value = DBNull.Value
                                                SCMValores.Parameters("@id_lugar_dest").Value = DBNull.Value
                                                SCMValores.Parameters("@lugar_dest").Value = DBNull.Value
                                            Else
                                                SCMValores.Parameters("@origen_destino").Value = gvProducto.Rows(index).Cells(19).Text.ToString
                                                SCMValores.Parameters("@id_lugar_orig").Value = Val(gvProducto.Rows(index).Cells(20).Text.ToString)
                                                SCMValores.Parameters("@lugar_orig").Value = gvProducto.Rows(index).Cells(15).Text.ToString
                                                SCMValores.Parameters("@id_lugar_dest").Value = Val(gvProducto.Rows(index).Cells(21).Text.ToString)
                                                SCMValores.Parameters("@lugar_dest").Value = gvProducto.Rows(index).Cells(16).Text.ToString
                                            End If

                                            If gvProducto.Rows(index).Cells(17).Text.ToString = "&nbsp;" Then
                                                SCMValores.Parameters("@vehiculo").Value = DBNull.Value
                                            Else
                                                SCMValores.Parameters("@vehiculo").Value = gvProducto.Rows(index).Cells(17).Text.ToString
                                            End If
                                            If gvProducto.Rows(index).Cells(18).Text.ToString = "&nbsp;" Then
                                                SCMValores.Parameters("@obs").Value = DBNull.Value
                                            Else
                                                'SCMValores.Parameters("@obs").Value = gvProducto.Rows(index).Cells(18).Text.ToString
                                                SCMValores.Parameters("@obs").Value = HttpUtility.HtmlDecode(Me.gvProducto.Rows(index).Cells(18).Text)
                                            End If

                                            If gvProducto.Rows(index).Cells(3).Text.ToString = "Factura" Then
                                                SCMValores.Parameters("@tipo").Value = "F"
                                                'Obtener datos de la Factura
                                                'Dim cadena As String = Replace(gvProducto.Rows(index).Cells(24).Text.ToString, "[", "")
                                                'Dim valor As String = Replace(cadena, "]", "")

                                                sdaFactura.SelectCommand.Parameters("@id_dt_factura").Value = Val(gvProducto.Rows(index).Cells(25).Text.ToString)
                                                ConexionBD.Open()
                                                sdaFactura.Fill(dsFactura)
                                                ConexionBD.Close()
                                                'Complementar Parámetros
                                                SCMValores.Parameters("@CFDI").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                                SCMValores.Parameters("@no_factura").Value = dsFactura.Tables(0).Rows(0).Item("no_factura").ToString()
                                                SCMValores.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                                SCMValores.Parameters("@proveedor").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                                'Obtener el % de IVA del Concepto
                                                SCMValoresIVA.Parameters("@id_concepto").Value = Val(gvProducto.Rows(index).Cells(1).Text.ToString)
                                                ConexionBD.Open()
                                                SCMValores.Parameters("@iva").Value = SCMValoresIVA.ExecuteScalar

                                                If Val(gvProducto.Rows(index).Cells(22).Text.ToString) <> 0 Then
                                                    SCMValores.Parameters("@isr_ret").Value = CDbl(gvProducto.Rows(index).Cells(22).Text.ToString)

                                                Else
                                                    SCMValores.Parameters("@isr_ret").Value = DBNull.Value

                                                End If
                                                ConexionBD.Close()
                                                'Actualizar Status de Factura
                                                SCMValoresDtFact.Parameters("@id_dt_factura").Value = Val(gvProducto.Rows(index).Cells(25).Text.ToString)
                                                ConexionBD.Open()
                                                SCMValoresDtFact.ExecuteNonQuery()
                                                ConexionBD.Close()
                                            Else
                                                SCMValores.Parameters("@tipo").Value = "T"
                                                SCMValores.Parameters("@CFDI").Value = DBNull.Value
                                                SCMValores.Parameters("@no_factura").Value = DBNull.Value
                                                SCMValores.Parameters("@rfc").Value = DBNull.Value
                                                SCMValores.Parameters("@proveedor").Value = DBNull.Value
                                                SCMValores.Parameters("@iva").Value = 0
                                                SCMValores.Parameters("@isr_ret").Value = DBNull.Value
                                            End If
                                            ConexionBD.Open()
                                            SCMValores.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            '' Segunda parte


                                            If gvProducto.Rows(index).Cells(3).Text.ToString = "Factura" Then
                                                'Obtener el id_dt_comp
                                                SCMValoresDtComp.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                                ConexionBD.Open()
                                                idDtComp = SCMValoresDtComp.ExecuteScalar()
                                                ConexionBD.Close()
                                                'Insertar Detalle de Factura
                                                SCMValoresDtCompL.Parameters("@idDtComp").Value = idDtComp
                                                SCMValoresDtCompL.Parameters("@porcentAuto").Value = CDec(Replace(gvProducto.Rows(index).Cells(27).Text.ToString, "%", "Auto"))
                                                SCMValoresDtCompL.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                                ConexionBD.Open()
                                                SCMValoresDtCompL.ExecuteNonQuery()
                                                ConexionBD.Close()

                                                'Validar si es un concepto de Combustible
                                                SCMContComb.Parameters("@id_concepto_comp").Value = Val(gvProducto.Rows(index).Cells(1).Text.ToString)
                                                ConexionBD.Open()
                                                contComb = SCMContComb.ExecuteScalar
                                                ConexionBD.Close()
                                                If contComb = 1 Then
                                                    'Obtener importes de la Factura
                                                    sdaImpFactura.SelectCommand.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                                    ConexionBD.Open()
                                                    sdaImpFactura.Fill(dsImpFactura)
                                                    ConexionBD.Close()

                                                    'Insertar Carga de Combustible por Comprobar
                                                    SCMCargaComb.Parameters("@importe_con_ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                                                    SCMCargaComb.Parameters("@importe_sin_imp").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("subtotal").ToString())
                                                    SCMCargaComb.Parameters("@ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_ieps").ToString())
                                                    SCMCargaComb.Parameters("@iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_iva").ToString())
                                                    'SCMCargaComb.Parameters("@importe_transaccion").Value = CDec(gvProducto.Rows(index).Cells(9).ToString())
                                                    Dim mon As Double
                                                    mon = gvProducto.Rows(index).Cells(9).Text
                                                    SCMCargaComb.Parameters("@importe_transaccion").Value = mon
                                                    SCMCargaComb.Parameters("@porcent_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("porcent_iva").ToString())
                                                    SCMCargaComb.Parameters("@cantidad_mercancia").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("cantidad").ToString())
                                                    SCMCargaComb.Parameters("@precio_ticket").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_ticket").ToString())
                                                    SCMCargaComb.Parameters("@precio_sin_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_sin_iva").ToString())
                                                    SCMCargaComb.Parameters("@razon_social_afiliado").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                                    SCMCargaComb.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                                    ConexionBD.Open()
                                                    SCMCargaComb.ExecuteNonQuery()
                                                    ConexionBD.Close()

                                                    _txtCargComb.Text = Val(_txtCargComb.Text) + 1
                                                End If
                                            End If

                                            sdaFactura.Dispose()
                                            dsFactura.Dispose()

                                        Next

                                        gvProducto.Columns(1).Visible = False
                                        'gvProducto.Columns(18).Visible = False
                                        'gvProducto.Columns(19).Visible = False
                                        'gvProducto.Columns(20).Visible = False
                                        'gvProducto.Columns(21).Visible = False
                                        'gvProducto.Columns(22).Visible = False
                                        'gvProducto.Columns(23).Visible = False
                                        'gvProducto.Columns(24).Visible = False

                                        gvProducto.Columns(19).Visible = False
                                        gvProducto.Columns(20).Visible = False
                                        gvProducto.Columns(21).Visible = False
                                        gvProducto.Columns(22).Visible = False
                                        gvProducto.Columns(23).Visible = False
                                        gvProducto.Columns(24).Visible = False
                                        gvProducto.Columns(25).Visible = False
                                        gvProducto.Columns(26).Visible = False
                                        gvProducto.Columns(27).Visible = False


                                        'Actualizar Anticipos
                                        Dim SCMValoresAnt As SqlCommand = New System.Data.SqlClient.SqlCommand
                                        SCMValoresAnt.Connection = ConexionBD
                                        SCMValoresAnt.Parameters.Clear()
                                        SCMValoresAnt.CommandText = "update ms_anticipo " +
                                                                        "set status = case status when 'EE' then 'EECP' when 'TR' then 'TRCP' else status end " +
                                                                        "where id_ms_anticipo = @id_ms_anticipo "
                                        SCMValoresAnt.Parameters.Add("@id_ms_anticipo", SqlDbType.Int)
                                        'Insertar en tabla Comprobación / Anticipos
                                        Dim SCMValoresCompAnt As SqlCommand = New System.Data.SqlClient.SqlCommand
                                        SCMValoresCompAnt.Connection = ConexionBD
                                        SCMValoresCompAnt.Parameters.Clear()
                                        SCMValoresCompAnt.CommandText = "insert into dt_anticipo ( id_ms_comp,  id_ms_anticipo) " +
                                                                            "                 values (@id_ms_comp, @id_ms_anticipo) "
                                        SCMValoresCompAnt.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                                        SCMValoresCompAnt.Parameters.Add("@id_ms_anticipo", SqlDbType.Int)

                                        If rbdOpcionAnticipo.Text = "Anticipo AMEX" Or _txtAnticipo.Text = "Anticipo AMEX" Then
                                            For Each row As GridViewRow In gvAnticiposAmex.Rows
                                                If row.RowType = DataControlRowType.DataRow Then
                                                    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                                                    If chkRow.Checked Then
                                                        SCMValoresAnt.Parameters("@id_ms_anticipo").Value = Val(row.Cells(0).Text)
                                                        ConexionBD.Open()
                                                        SCMValoresAnt.ExecuteNonQuery()
                                                        ConexionBD.Close()
                                                        SCMValoresCompAnt.Parameters("@id_ms_anticipo").Value = Val(row.Cells(0).Text)
                                                        ConexionBD.Open()
                                                        SCMValoresCompAnt.ExecuteNonQuery()
                                                        ConexionBD.Close()
                                                    End If
                                                End If
                                            Next
                                        Else
                                            For Each row As GridViewRow In .gvAnticipos.Rows
                                                If row.RowType = DataControlRowType.DataRow Then
                                                    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                                                    If chkRow.Checked Then
                                                        SCMValoresAnt.Parameters("@id_ms_anticipo").Value = Val(row.Cells(0).Text)
                                                        ConexionBD.Open()
                                                        SCMValoresAnt.ExecuteNonQuery()
                                                        ConexionBD.Close()
                                                        SCMValoresCompAnt.Parameters("@id_ms_anticipo").Value = Val(row.Cells(0).Text)
                                                        ConexionBD.Open()
                                                        SCMValoresCompAnt.ExecuteNonQuery()
                                                        ConexionBD.Close()
                                                    End If
                                                End If
                                            Next
                                        End If


                                        'Actualizar Unidades en caso de que el panel sea visible y se agregara al menos una unidad
                                        If .pnlUnidad.Visible = True And .gvUnidad.Rows.Count > 0 Then
                                            SCMValores.CommandText = ""
                                            SCMValores.Parameters.Clear()
                                            SCMValores.CommandText = "update dt_unidad set id_ms_comp = @id_ms_comp where id_ms_comp = -1 and id_usuario = @id_usuario"
                                            SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                                            SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                                            ConexionBD.Open()
                                            SCMValores.ExecuteNonQuery()
                                            ConexionBD.Close()
                                        End If

                                        'Insertar Instancia de la Comprobación dependencia si hay o no validador

                                        SCMValores.Parameters.Clear()
                                        SCMValores.CommandText = "insert into ms_instancia ( id_ms_sol,  tipo,  id_actividad) " +
                                                                     "				    values (@id_ms_sol, @tipo, @id_actividad) "
                                        SCMValores.Parameters.AddWithValue("@id_ms_sol", Val(.lblFolio.Text))
                                        SCMValores.Parameters.AddWithValue("@tipo", "C")

                                        If .ddlValidador.Visible = True Then
                                            SCMValores.Parameters.AddWithValue("@id_actividad", 115)
                                        Else
                                            SCMValores.Parameters.AddWithValue("@id_actividad", 9)
                                        End If
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()
                                        'Obtener ID de la Instancia de Solicitud
                                        SCMValores.CommandText = "select max(id_ms_instancia) from ms_instancia where id_ms_sol = @id_ms_sol and tipo = 'C' "
                                        ConexionBD.Open()
                                        ._txtIdMsInst.Text = SCMValores.ExecuteScalar
                                        ConexionBD.Close()
                                        'Insertar Históricos
                                        SCMValores.Parameters.Clear()
                                        SCMValores.CommandText = "insert into ms_historico ( id_ms_instancia,  id_actividad,  id_usr,  fecha) " +
                                                                     "				    values (@id_ms_instancia, @id_actividad, @id_usr, @fecha) "
                                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))

                                        If .ddlValidador.Visible = True Then
                                            SCMValores.Parameters.AddWithValue("@id_actividad", 115)
                                        Else
                                            SCMValores.Parameters.AddWithValue("@id_actividad", 9)
                                        End If
                                        SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                                        SCMValores.Parameters.AddWithValue("@fecha", fecha)
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()

                                        gvEvidencias.Columns(2).Visible = True
                                        gvEvidencias.Columns(3).Visible = True

                                        'Insertar Evidencias'
                                        For index As Integer = 0 To gvEvidencias.Rows.Count - 1


                                            My.Computer.FileSystem.RenameFile(Server.MapPath("Evidencias Comp\" + _txtIdUsuario.Text.ToString() + "-" + gvEvidencias.Rows(index).Cells(2).Text.ToString.Trim()), lblFolio.Text + "-" + gvEvidencias.Rows(index).Cells(2).Text)

                                            SCMValores.Parameters.Clear()
                                            SCMValores.CommandText = "Insert into dt_archivo_comp (id_ms_comp, archivo, status) " +
                                                    "                     values (@id_ms_comp, @archivo, 'A') "
                                            SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(lblFolio.Text))
                                            SCMValores.Parameters.AddWithValue("@archivo", lblFolio.Text + "-" + gvEvidencias.Rows(index).Cells(2).Text)
                                            ConexionBD.Open()
                                            SCMValores.ExecuteNonQuery()
                                            ConexionBD.Close()


                                        Next

                                        gvEvidencias.Columns(2).Visible = False
                                        gvEvidencias.Columns(3).Visible = False
                                        gvEvidencias.Columns(4).Visible = False

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
                                        Mensaje.Bcc.Add("notificaciones@unne.com.mx")
                                        'Mensaje.From = New System.Net.Mail.MailAddress("soporte.unne.procad@gmail.com", "ProcAd", System.Text.Encoding.UTF8)
                                        Mensaje.From = New MailAddress("notificaciones@unne.com.mx")
                                        Mensaje.Subject = "ProcAd - Comprobación No. " + .lblFolio.Text + " por Autorizar"
                                        Dim texto As String
                                        texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                                                    "Se generó la comprobación número <b>" + .lblFolio.Text +
                                                    "</b> por parte de <b>" + .lblEmpleado.Text +
                                                    "</b><br><br>Favor de determinar si procede </span>"
                                        Mensaje.Body = texto
                                        Mensaje.IsBodyHtml = True
                                        Mensaje.Priority = MailPriority.Normal

                                        Dim Servidor As New SmtpClient()
                                        Servidor.Host = "10.10.10.30"
                                        Servidor.Port = 587
                                        Servidor.EnableSsl = False
                                        Servidor.UseDefaultCredentials = False
                                        Servidor.Credentials = New System.Net.NetworkCredential("notificaciones", "Oqi2LFAZea07")

                                        Try
                                            Servidor.Send(Mensaje)
                                        Catch ex As System.Net.Mail.SmtpException
                                            .litError.Text = ex.ToString
                                        End Try

                                        If Val(._txtCargComb.Text) > 0 Then
                                            .litError.Text = "Existen " + ._txtCargComb.Text + " Cargas de Combustible con Tarjeta, favor de complementarlas a la brevedad"
                                        End If

                                        'Inhabilitar Paneles
                                        .pnlInicio.Enabled = False
                                    End While
                                End If

                            End If
                        End If
                    Else
                        litError.Text = "El monto comprobado no puede ser menor al monto del anticipo"
                    End If

                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Unidades"


#Region "Funciones"

    Public Sub buscarNoEco()

        With Me
            Try
                .litError.Text = ""
                Dim ConexionBDNAV As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBDNAV.ConnectionString = accessDB.conBD("NAV")
                Dim sdaNoEco As New SqlDataAdapter
                Dim dsNoEco As New DataSet
                Dim query As String = "select [No_] as id " +
                                      "     , [Núm_ Economico] + ' - ' + [Empresa que administra] + ' - ' + [Num_ Placas] as no_eco " +
                                      "from [TRACSA$Standard Units] " +
                                      "where [Núm_ Economico] like '%' + @noEco + '%' " +
                                      "  and [Núm_ Economico] <> '' " +
                                      "  and [Status] <> 2 "
                ' TR
                query = query + "  and ( [Tipo Unidad] in (0, 4) "
                ' TQ
                query = query + "  or [Tipo Unidad] in (1) "
                ' DL
                query = query + "  or [Tipo Unidad] in (2) )"

                sdaNoEco.SelectCommand = New SqlCommand(query, ConexionBDNAV)
                sdaNoEco.SelectCommand.Parameters.AddWithValue("@noEco", "%" + .txtUnidad.Text.ToUpper + "%")
                .ddlUnidad.DataSource = dsNoEco
                .ddlUnidad.DataTextField = "no_eco"
                .ddlUnidad.DataValueField = "id"
                ConexionBDNAV.Open()
                sdaNoEco.Fill(dsNoEco)
                .ddlUnidad.DataBind()
                ConexionBDNAV.Close()
                sdaNoEco.Dispose()
                dsNoEco.Dispose()
                .ddlUnidad.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub actualizarUnidades()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .gvUnidad.DataSource = dsCatalogo
                'Habilitar columna para actualización
                .gvUnidad.Columns(0).Visible = True
                'Catálogo de Unidades agregados
                sdaCatalogo.SelectCommand = New SqlCommand("select id_dt_unidad " +
                                                           "     , empresa " +
                                                           "     , no_economico " +
                                                           "     , tipo " +
                                                           "     , modelo " +
                                                           "     , placas " +
                                                           "     , div " +
                                                           "     , division " +
                                                           "     , zn " +
                                                           "from dt_unidad " +
                                                           "where id_ms_comp = -1 " +
                                                           "  and id_usuario = @idUsuario ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvUnidad.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvUnidad.SelectedIndex = -1
                'Inhabilitar columna para vista
                .gvUnidad.Columns(0).Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

    Protected Sub cmdBuscarU_Click(sender As Object, e As EventArgs) Handles cmdBuscarU.Click
        buscarNoEco()
    End Sub

    Protected Sub cmdAgregarU_Click(sender As Object, e As EventArgs) Handles cmdAgregarU.Click
        With Me
            Try
                'Verificar que exista una unidad en la lista
                If .ddlUnidad.Items.Count <> 0 Then
                    Dim ConexionBDNAV As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBDNAV.ConnectionString = accessDB.conBD("NAV")
                    Dim SCMValoresNAV As SqlCommand = New System.Data.SqlClient.SqlCommand
                    Dim empresa As String
                    Dim noEconomico As String
                    SCMValoresNAV.Connection = ConexionBDNAV
                    SCMValoresNAV.CommandText = ""
                    SCMValoresNAV.Parameters.Clear()
                    'Obtener Empresa que administra de la Unidad en NAV
                    SCMValoresNAV.CommandText = "select [Empresa que administra] " +
                                                "from [TRACSA$Standard Units] " +
                                                "where [No_] = @No_ "
                    SCMValoresNAV.Parameters.AddWithValue("@No_", .ddlUnidad.SelectedValue)
                    ConexionBDNAV.Open()
                    empresa = SCMValoresNAV.ExecuteScalar
                    ConexionBDNAV.Close()
                    'Obtener Número económico de la Unidad en NAV
                    SCMValoresNAV.CommandText = "select [Núm_ Economico] " +
                                                "from [TRACSA$Standard Units] " +
                                                "where [No_] = @No_ "
                    ConexionBDNAV.Open()
                    noEconomico = SCMValoresNAV.ExecuteScalar
                    ConexionBDNAV.Close()

                    'Obtener datos de la Unidad
                    Dim sdaUnidad As New SqlDataAdapter
                    Dim dsUnidad As New DataSet
                    sdaUnidad.SelectCommand = New SqlCommand("select StandardUnits.[Empresa que administra] as empresa " +
                                                             "     , StandardUnits.No_ as codigo " +
                                                             "     , StandardUnits.Description as descripcion " +
                                                             "     , StandardUnits.[Núm_ Economico] as no_economico " +
                                                             "     , case StandardUnits.Status " +
                                                             "         when 0 then 'Patio' " +
                                                             "         when 1 then 'Activo' " +
                                                             "         when 2 then 'Baja' " +
                                                             "       end as status " +
                                                             "     , case StandardUnits.[Tipo Unidad] " +
                                                             "         when 0 then 'Tractocamion' " +
                                                             "         when 1 then 'Remolque' " +
                                                             "         when 2 then 'Dolly' " +
                                                             "         when 3 then 'Maquinaria' " +
                                                             "         when 4 then 'Torton' " +
                                                             "       end as tipo " +
                                                             "     , StandardUnits.[Num_ Serie Chasis] as no_serie " +
                                                             "     , StandardUnits.AñoModelo as modelo " +
                                                             "     , StandardUnits.Marca as marca " +
                                                             "     , StandardUnits.[Num_ Placas] as placas " +
                                                             "     , StandardUnits.Division as div " +
                                                             "     , DIVValue.Name as division " +
                                                             "     , StandardUnits.[Shortcut Dimension 3 Code] as zn " +
                                                             "     , ZNValue.Name as zona " +
                                                             "from [" + empresa + "$Standard Units] as StandardUnits " +
                                                             "  left join [" + empresa + "$Dimension Value] DIVValue on StandardUnits.Division = DIVValue.Code and DIVValue.[Dimension Code] = 'DIV' " +
                                                             "  left join [" + empresa + "$Dimension Value] ZNValue on StandardUnits.[Shortcut Dimension 3 Code] = ZNValue.Code and ZNValue.[Dimension Code] = 'ZN' " +
                                                             "where StandardUnits.Status <> 2 " +
                                                             "  and [Núm_ Economico] = @numEconomico ", ConexionBDNAV)
                    sdaUnidad.SelectCommand.Parameters.AddWithValue("@numEconomico", noEconomico)
                    ConexionBDNAV.Open()
                    sdaUnidad.Fill(dsUnidad)
                    ConexionBDNAV.Close()

                    'Insertar datos de Unidad en dt_unidad
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    'Registrar la unidad en la base de datos
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into dt_unidad (id_ms_comp,  id_usuario,  empresa,  codigo,  descripcion,  no_economico,  status,  tipo,  no_serie,  modelo,  placas,  div,  division,  zn,  zona) " +
                                             "               values (        -1, @id_usuario, @empresa, @codigo, @descripcion, @no_economico, @status, @tipo, @no_serie, @modelo, @placas, @div, @division, @zn, @zona)"
                    SCMValores.Parameters.AddWithValue("@id_usuario", Val(Me._txtIdUsuario.Text))
                    SCMValores.Parameters.AddWithValue("@empresa", dsUnidad.Tables(0).Rows(0).Item("empresa").ToString())
                    SCMValores.Parameters.AddWithValue("@codigo", dsUnidad.Tables(0).Rows(0).Item("codigo").ToString())
                    SCMValores.Parameters.AddWithValue("@descripcion", dsUnidad.Tables(0).Rows(0).Item("descripcion").ToString())
                    SCMValores.Parameters.AddWithValue("@no_economico", dsUnidad.Tables(0).Rows(0).Item("no_economico").ToString())
                    SCMValores.Parameters.AddWithValue("@status", dsUnidad.Tables(0).Rows(0).Item("status").ToString())
                    SCMValores.Parameters.AddWithValue("@tipo", dsUnidad.Tables(0).Rows(0).Item("tipo").ToString())
                    SCMValores.Parameters.AddWithValue("@no_serie", dsUnidad.Tables(0).Rows(0).Item("no_serie").ToString())
                    SCMValores.Parameters.AddWithValue("@modelo", dsUnidad.Tables(0).Rows(0).Item("modelo").ToString())
                    SCMValores.Parameters.AddWithValue("@placas", dsUnidad.Tables(0).Rows(0).Item("placas").ToString())
                    SCMValores.Parameters.AddWithValue("@div", dsUnidad.Tables(0).Rows(0).Item("div").ToString())
                    SCMValores.Parameters.AddWithValue("@division", dsUnidad.Tables(0).Rows(0).Item("division").ToString())
                    SCMValores.Parameters.AddWithValue("@zn", dsUnidad.Tables(0).Rows(0).Item("zn").ToString())
                    SCMValores.Parameters.AddWithValue("@zona", dsUnidad.Tables(0).Rows(0).Item("zona").ToString())
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()
                    sdaUnidad.Dispose()
                    dsUnidad.Dispose()

                    actualizarUnidades()
                Else
                    .litError.Text = "Favor de seleccionar una Unidad"
                End If
            Catch exc As Exception
                .litError.Text = exc.ToString
            End Try
        End With
    End Sub

    Protected Sub gvUnidad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvUnidad.SelectedIndexChanged
        With Me
            Try
                'Eliminar la Unidad
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD

                'Registrar la unidad en la base de datos
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "delete from dt_unidad where id_dt_unidad = @id_dt_unidad"
                SCMValores.Parameters.AddWithValue("@id_dt_unidad", .gvUnidad.SelectedRow.Cells(0).Text)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                actualizarUnidades()
            Catch exc As Exception
                .litError.Text = exc.ToString
            End Try
        End With
    End Sub

#End Region

#Region " Aceptar"

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Try
            Dim totalAnt As Integer = 0
            Dim columnas As Integer = 0
            If rbdOpcionAnticipo.Text = "Anticipo AMEX" Or _txtAnticipo.Text = "Anticipo AMEX" Then
                For Each row As GridViewRow In gvAnticiposAmex.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                        If chkRow.Checked Then
                            totalAnt = totalAnt + 1
                        End If
                    End If
                Next
                columnas = gvAnticiposAmex.Rows.Count
            Else
                For Each row As GridViewRow In gvAnticipos.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                        If chkRow.Checked Then
                            totalAnt = totalAnt + 1
                        End If
                    End If
                Next
                columnas = gvAnticipos.Rows.Count
            End If


            If columnas > 0 And totalAnt = 0 And Val(_txtAntPend.Text) = 0 Then
                litError.Text = "Favor de seleccionar el Anticipo a Comprobar"
            Else
                gvProducto.Columns(1).Visible = True
                'gvProducto.Columns(18).Visible = True
                'gvProducto.Columns(19).Visible = True
                'gvProducto.Columns(20).Visible = True
                'gvProducto.Columns(21).Visible = True
                'gvProducto.Columns(22).Visible = True
                'gvProducto.Columns(23).Visible = True
                'gvProducto.Columns(24).Visible = True

                'gvProducto.Columns(18).Visible = True
                gvProducto.Columns(19).Visible = True
                gvProducto.Columns(20).Visible = True
                gvProducto.Columns(21).Visible = True
                gvProducto.Columns(22).Visible = True
                gvProducto.Columns(23).Visible = True
                gvProducto.Columns(24).Visible = True
                gvProducto.Columns(25).Visible = True
                gvProducto.Columns(26).Visible = True
                gvProducto.Columns(27).Visible = True
                If _txtVal_Origen_Destino.Text = "S" Then
                    If ddlOrig1.SelectedItem.Text = "" Then
                        litError.Text = "El concepto debe tener seleccionado el origen y el destino."
                        Return
                    End If
                End If
                If rbdOpcionAnticipo.Text = "Anticipo AMEX" Or _txtAnticipo.Text = "Anticipo AMEX" And cbTabulador1.Checked = True Then
                    litError.Text = "Solo puede comprobar facturas con el anticipo AMEX"
                Else
                    If wdteFechaIni.Date > wdteFechaFin.Date Or wdpFecha1.Date > wdteFechaFin.Date Or wdpFecha1.Date < wdteFechaIni.Date Then
                        litError.Text = "La fechas no son correctas"
                        If wdpFecha1.Date > wdteFechaFin.Date Or wdpFecha1.Date < wdteFechaIni.Date Then
                            upDateV1.Visible = True
                            lblDateV1.Visible = True
                        End If
                    Else
                        If txtJust.Text.Trim = "" Then
                            litError.Text = "Información Insuficiente, favor de ingresar las Justificación correspondiente"
                        Else

                            If cbTabulador1.Checked = True And txtObs1.Text = "" Then
                                litError.Text = "Hace falta las observaciones"
                            Else

                                If cbFactura1.Checked = True And hlProveedor1.Text = "" Then
                                    litError.Text = "Hace falta información"
                                Else

                                    Dim banderaP As Boolean = True
                                    For index As Integer = 0 To gvProducto.Rows.Count - 1
                                        If gvProducto.Rows(index).Cells(25).Text = ddlNoFactura1.Text Then
                                            banderaP = False
                                        End If
                                    Next

                                    If banderaP = False Then
                                        litError.Text = "Ya esta agregada esta factura"
                                    Else

                                        litError.Text = ""
                                        sumarConceptos()

                                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                                        Dim sdaFact As New SqlDataAdapter
                                        Dim dsFac As New DataSet
                                        Dim ruta As String = ""
                                        Dim cp As String = ""
                                        If cbFactura1.Checked = True Then
                                            sdaFact.SelectCommand = New SqlCommand("select 'http://148.223.153.43/Facturas/' + dt_factura.rfc_receptor + '/' + format(datepart(year, dt_factura.fecha_emision), '00') + '/' + upper(format(dt_factura.fecha_emision, 'MMMM', 'es-es')) + '/' + dt_factura.movimiento + '/' + dt_factura.tipo + '/' + dt_factura.uuid + '.pdf' as ruta  " +
                                                                           "  , dt_factura.lugar_exp " +
                                                                           " from dt_factura where id_dt_factura = @id_dt_factura ", ConexionBD)
                                            sdaFact.SelectCommand.Parameters.AddWithValue("@id_dt_factura", Val(ddlNoFactura1.SelectedValue))
                                            ConexionBD.Open()
                                            sdaFact.Fill(dsFac)
                                            ConexionBD.Close()

                                            ruta = dsFac.Tables(0).Rows(0).Item("ruta").ToString()
                                            cp = dsFac.Tables(0).Rows(0).Item("lugar_exp").ToString()
                                        End If

                                        'If cbFactura1.Checked = True Then
                                        '    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                                        '    SCMValores.Connection = ConexionBD
                                        '    SCMValores.Parameters.Clear()
                                        '    SCMValores.CommandText = " select 'http://148.223.153.43/Facturas/' + dt_factura.rfc_receptor + '/' + format(datepart(year, dt_factura.fecha_emision), '00') + '/' + upper(format(dt_factura.fecha_emision, 'MMMM', 'es-es')) + '/' + dt_factura.movimiento + '/' + dt_factura.tipo + '/' + dt_factura.uuid + '.pdf' as ruta  " +
                                        '                                 "  , dt_factura.lugar_exp " +
                                        '                                 " from dt_factura where id_dt_factura = @id_dt_factura "
                                        '    SCMValores.Parameters.AddWithValue("@id_dt_factura", Val(ddlNoFactura1.SelectedValue))
                                        '    ConexionBD.Open()
                                        '    ruta = SCMValores.ExecuteScalar
                                        '    ConexionBD.Close()
                                        'End If

                                        upDateV1.Visible = False
                                        lblDateV1.Visible = False

                                        Dim valor As String = ""

                                        If gvProducto.Rows.Count <> 0 Then
                                            'Ya tiene Comprobaciones'
                                            Dim tabla As DataTable = New DataTable
                                            tabla.Columns.Add("id_concepto", GetType(String))
                                            tabla.Columns.Add("Fecha de realizacion", GetType(String))
                                            tabla.Columns.Add("Tipo", GetType(String))
                                            tabla.Columns.Add("Concepto", GetType(String))
                                            tabla.Columns.Add("No.Pers", GetType(String))
                                            tabla.Columns.Add("No.Dias", GetType(String))
                                            tabla.Columns.Add("Subtotal", GetType(String))
                                            tabla.Columns.Add("IVA", GetType(String))
                                            tabla.Columns.Add("Total", GetType(String))
                                            tabla.Columns.Add("%Aut", GetType(String))
                                            tabla.Columns.Add("RFC", GetType(String))
                                            tabla.Columns.Add("lugar_exp", GetType(String))
                                            tabla.Columns.Add("Proveedor", GetType(String))
                                            tabla.Columns.Add("No.Factura", GetType(String))
                                            tabla.Columns.Add("Origen", GetType(String))
                                            tabla.Columns.Add("Destino", GetType(String))
                                            tabla.Columns.Add("Vehiculo", GetType(String))
                                            tabla.Columns.Add("Observaciones", GetType(String))
                                            tabla.Columns.Add("txtOriDes", GetType(String))
                                            tabla.Columns.Add("id_lugar_orig", GetType(String))
                                            tabla.Columns.Add("id_lugar_dest", GetType(String))
                                            tabla.Columns.Add("isr_ret", GetType(String))
                                            'Nuevos campos'
                                            tabla.Columns.Add("ruta", GetType(String))
                                            tabla.Columns.Add("nombre_fact", GetType(String))
                                            tabla.Columns.Add("ruta_fact", GetType(String))
                                            tabla.Columns.Add("%Auto", GetType(String))

                                            'Los que estan en el gv'
                                            For index As Integer = 0 To gvProducto.Rows.Count - 1
                                                Dim Row1 As DataRow = tabla.NewRow
                                                Row1("id_concepto") = gvProducto.Rows(index).Cells(1).Text
                                                Row1("Fecha de realizacion") = gvProducto.Rows(index).Cells(2).Text
                                                Row1("Tipo") = gvProducto.Rows(index).Cells(3).Text
                                                Row1("Concepto") = gvProducto.Rows(index).Cells(4).Text
                                                Row1("No.Pers") = gvProducto.Rows(index).Cells(5).Text
                                                Row1("No.Dias") = gvProducto.Rows(index).Cells(6).Text
                                                Row1("Subtotal") = gvProducto.Rows(index).Cells(7).Text
                                                Row1("IVA") = gvProducto.Rows(index).Cells(8).Text
                                                Row1("Total") = gvProducto.Rows(index).Cells(9).Text
                                                Row1("%Aut") = gvProducto.Rows(index).Cells(10).Text
                                                If gvProducto.Rows(index).Cells(11).Text = "&nbsp;" Then
                                                    Row1("RFC") = ""
                                                Else
                                                    Row1("RFC") = gvProducto.Rows(index).Cells(11).Text
                                                End If
                                                If gvProducto.Rows(index).Cells(12).Text = "&nbsp;" Then
                                                    Row1("lugar_exp") = ""
                                                Else
                                                    Row1("lugar_exp") = gvProducto.Rows(index).Cells(12).Text
                                                End If
                                                If gvProducto.Rows(index).Cells(13).Text = "&nbsp;" Then
                                                    Row1("Proveedor") = ""
                                                Else
                                                    Row1("Proveedor") = gvProducto.Rows(index).Cells(13).Text
                                                End If
                                                If gvProducto.Rows(index).Cells(24).Text.ToString = "&nbsp;" Then
                                                    Row1("No.Factura") = ""
                                                Else
                                                    Row1("No.Factura") = gvProducto.Rows(index).Cells(24).Text
                                                End If
                                                If gvProducto.Rows(index).Cells(15).Text = "&nbsp;" Then
                                                    Row1("Origen") = ""
                                                Else
                                                    Row1("Origen") = gvProducto.Rows(index).Cells(15).Text
                                                End If
                                                If gvProducto.Rows(index).Cells(16).Text = "&nbsp;" Then
                                                    Row1("Destino") = ""
                                                Else
                                                    Row1("Destino") = gvProducto.Rows(index).Cells(16).Text
                                                End If
                                                If gvProducto.Rows(index).Cells(17).Text = "&nbsp;" Then
                                                    Row1("Vehiculo") = ""
                                                Else
                                                    Row1("Vehiculo") = gvProducto.Rows(index).Cells(17).Text
                                                End If
                                                If gvProducto.Rows(index).Cells(18).Text = "&nbsp;" Then
                                                    Row1("Observaciones") = ""
                                                Else
                                                    valor = HttpUtility.HtmlDecode(Me.gvProducto.Rows(index).Cells(18).Text)
                                                    Row1("Observaciones") = valor
                                                End If
                                                If gvProducto.Rows(index).Cells(19).Text = "&nbsp;" Then
                                                    Row1("txtOriDes") = ""
                                                Else
                                                    Row1("txtOriDes") = gvProducto.Rows(index).Cells(19).Text
                                                End If
                                                If gvProducto.Rows(index).Cells(20).Text = "&nbsp;" Then
                                                    Row1("id_lugar_orig") = ""
                                                Else
                                                    Row1("id_lugar_orig") = gvProducto.Rows(index).Cells(20).Text
                                                End If
                                                If gvProducto.Rows(index).Cells(21).Text = "&nbsp;" Then
                                                    Row1("id_lugar_dest") = ""
                                                Else
                                                    Row1("id_lugar_dest") = gvProducto.Rows(index).Cells(21).Text
                                                End If
                                                Row1("isr_ret") = gvProducto.Rows(index).Cells(22).Text
                                                Row1("ruta") = gvProducto.Rows(index).Cells(23).Text
                                                Row1("nombre_fact") = gvProducto.Rows(index).Cells(25).Text
                                                Row1("ruta_fact") = gvProducto.Rows(index).Cells(26).Text
                                                Row1("%Auto") = gvProducto.Rows(index).Cells(27).Text

                                                tabla.Rows.Add(Row1)
                                            Next

                                            'El nuevo'
                                            Dim Row As DataRow = tabla.NewRow
                                            Row("id_concepto") = ddlConcepto1.SelectedValue
                                            Row("Fecha de realizacion") = wdpFecha1.Text
                                            If cbFactura1.Checked = True Then
                                                Row("Tipo") = "Factura"
                                                Row("No.Factura") = ddlNoFactura1.SelectedItem.Text
                                                Row("isr_ret") = CDbl(_txtIsrRet.Text)
                                                Row("ruta") = ruta
                                                Row("nombre_fact") = ddlNoFactura1.SelectedValue
                                                Row("ruta_fact") = ruta
                                                Row("RFC") = txtRFC1.Text
                                            Else
                                                Row("Tipo") = "Tabulador"
                                                Row("No.Factura") = ""
                                                Row("isr_ret") = 0
                                                Row("ruta") = ""
                                                Row("nombre_fact") = ""
                                                Row("ruta_fact") = ""
                                                Row("RFC") = ""
                                            End If
                                            Row("Concepto") = ddlConcepto1.SelectedItem.Text
                                            Row("No.Pers") = wneNoPers1.Text
                                            Row("No.Dias") = wneNoDias1.Text
                                            Row("Subtotal") = wceSubtotal1.Text
                                            Row("IVA") = wceIVA1.Text
                                            Row("Total") = wceTotal1.Text
                                            Row("%Aut") = wpePorcentAut1.Text

                                            Row("lugar_exp") = cp
                                            Row("Proveedor") = hlProveedor1.Text
                                            Row("Origen") = ddlOrig1.SelectedItem
                                            Row("Destino") = ddlDest1.SelectedItem
                                            Row("Vehiculo") = txtVehi1.Text
                                            valor = HttpUtility.HtmlDecode(Me.txtObs1.Text)
                                            Row("Observaciones") = valor
                                            Row("txtOriDes") = txtOriDes1.Text
                                            Row("id_lugar_orig") = ddlOrig1.SelectedValue
                                            Row("id_lugar_dest") = ddlDest1.SelectedValue
                                            Row("%Auto") = wpePorcentAut1.Value

                                            tabla.Rows.Add(Row)

                                            gvProducto.DataSource = tabla
                                            gvProducto.DataBind()

                                        Else
                                            'Hay que crear'
                                            Dim tabla As DataTable = New DataTable
                                            tabla.Columns.Add("id_concepto", GetType(String))
                                            tabla.Columns.Add("Fecha de realizacion", GetType(String))
                                            tabla.Columns.Add("Tipo", GetType(String))
                                            tabla.Columns.Add("Concepto", GetType(String))
                                            tabla.Columns.Add("No.Pers", GetType(String))
                                            tabla.Columns.Add("No.Dias", GetType(String))
                                            tabla.Columns.Add("Subtotal", GetType(String))
                                            tabla.Columns.Add("IVA", GetType(String))
                                            tabla.Columns.Add("Total", GetType(String))
                                            tabla.Columns.Add("%Aut", GetType(String))
                                            tabla.Columns.Add("RFC", GetType(String))
                                            tabla.Columns.Add("lugar_exp", GetType(String))
                                            tabla.Columns.Add("Proveedor", GetType(String))
                                            tabla.Columns.Add("No.Factura", GetType(String))
                                            tabla.Columns.Add("Origen", GetType(String))
                                            tabla.Columns.Add("Destino", GetType(String))
                                            tabla.Columns.Add("Vehiculo", GetType(String))
                                            tabla.Columns.Add("Observaciones", GetType(String))
                                            tabla.Columns.Add("txtOriDes", GetType(String))
                                            tabla.Columns.Add("id_lugar_orig", GetType(String))
                                            tabla.Columns.Add("id_lugar_dest", GetType(String))
                                            tabla.Columns.Add("isr_ret", GetType(String))
                                            'Nuevos
                                            tabla.Columns.Add("ruta", GetType(String))
                                            tabla.Columns.Add("nombre_fact", GetType(String))
                                            tabla.Columns.Add("ruta_fact", GetType(String))
                                            tabla.Columns.Add("%Auto", GetType(String))

                                            Dim Row As DataRow = tabla.NewRow
                                            Row("id_concepto") = ddlConcepto1.SelectedValue
                                            Row("Fecha de realizacion") = wdpFecha1.Text
                                            If cbFactura1.Checked = True Then
                                                Row("Tipo") = "Factura"
                                                Row("isr_ret") = _txtIsrRet.Text
                                                Row("ruta") = ruta
                                                Row("nombre_fact") = ddlNoFactura1.SelectedItem.Value
                                                Row("ruta_fact") = ruta
                                                Row("No.Factura") = ddlNoFactura1.SelectedItem.Text
                                                Row("RFC") = txtRFC1.Text
                                            Else
                                                Row("Tipo") = "Tabulador"
                                                Row("isr_ret") = 0
                                                Row("ruta") = ""
                                                Row("nombre_fact") = ""
                                                Row("ruta_fact") = ""
                                                Row("No.Factura") = ""
                                                Row("RFC") = ""
                                            End If
                                            Row("concepto") = ddlConcepto1.SelectedItem.Text
                                            Row("No.Pers") = wneNoPers1.Text
                                            Row("No.Dias") = wneNoDias1.Text
                                            Row("Subtotal") = wceSubtotal1.Text
                                            Row("IVA") = wceIVA1.Text
                                            Row("Total") = wceTotal1.Text
                                            Row("%Aut") = wpePorcentAut1.Text

                                            Row("lugar_exp") = cp
                                            Row("Proveedor") = hlProveedor1.Text
                                            Row("Origen") = ddlOrig1.SelectedItem
                                            Row("Destino") = ddlDest1.SelectedItem
                                            Row("Vehiculo") = txtVehi1.Text

                                            valor = HttpUtility.HtmlDecode(Me.txtObs1.Text)
                                            Row("Observaciones") = valor
                                            Row("txtOriDes") = txtOriDes1.Text
                                            Row("id_lugar_orig") = ddlOrig1.SelectedValue
                                            Row("id_lugar_dest") = ddlDest1.SelectedValue
                                            Row("%Auto") = wpePorcentAut1.Value
                                            tabla.Rows.Add(Row)
                                            gvProducto.DataSource = tabla
                                            gvProducto.DataBind()
                                        End If

                                        pnlGrid.Visible = True
                                        'pnlConceptos.Visible = False
                                        pnlAceptar.Visible = False
                                        pnlSuma.Visible = True
                                        lblNoConceptos.Text = gvProducto.Rows.Count
                                        'btnAgregar.Visible = True
                                        pnlConcepto1.Visible = False

                                        If gvProducto.Rows.Count <> 0 Then
                                            pnlFinalizar.Visible = True
                                        Else
                                            pnlFinalizar.Visible = False
                                        End If
                                        pnlGvEvidencias.Visible = True
                                        pnlSuma.Visible = True

                                        ddlEmpresa.Enabled = False
                                        ddlTipoGasto.Enabled = False

                                        conNuevo()
                                    End If
                                End If
                            End If

                        End If
                    End If
                End If


                gvProducto.Visible = True
                gvProducto.Columns(1).Visible = False
                'gvProducto.Columns(18).Visible = False
                gvProducto.Columns(19).Visible = False
                gvProducto.Columns(20).Visible = False
                gvProducto.Columns(21).Visible = False
                gvProducto.Columns(22).Visible = False
                gvProducto.Columns(23).Visible = False
                gvProducto.Columns(24).Visible = False
                gvProducto.Columns(25).Visible = False
                gvProducto.Columns(26).Visible = False
                gvProducto.Columns(27).Visible = False
            End If
        Catch ex As Exception
            litError.Text = ex.Message
            gvProducto.Visible = True
            gvProducto.Columns(1).Visible = False
            'gvProducto.Columns(18).Visible = False
            gvProducto.Columns(19).Visible = False
            gvProducto.Columns(20).Visible = False
            gvProducto.Columns(21).Visible = False
            gvProducto.Columns(22).Visible = False
            gvProducto.Columns(23).Visible = False
            gvProducto.Columns(24).Visible = False
            gvProducto.Columns(25).Visible = False
            gvProducto.Columns(26).Visible = False
            gvProducto.Columns(27).Visible = False
        End Try
    End Sub


    Protected Sub gvProducto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvProducto.SelectedIndexChanged
        Try
            If gvProducto.Rows.Count > 1 Then
                'Mostrar el boton'
                pnlEliminar.Visible = True
            Else
                'Ocultar'
                pnlEliminar.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Try

            gvProducto.Columns(1).Visible = True
            'gvProducto.Columns(18).Visible = True
            'gvProducto.Columns(19).Visible = True
            'gvProducto.Columns(20).Visible = True
            'gvProducto.Columns(21).Visible = True
            'gvProducto.Columns(22).Visible = True
            'gvProducto.Columns(23).Visible = True
            'gvProducto.Columns(24).Visible = True
            'gvProducto.Columns(18).Visible = True
            gvProducto.Columns(19).Visible = True
            gvProducto.Columns(20).Visible = True
            gvProducto.Columns(21).Visible = True
            gvProducto.Columns(22).Visible = True
            gvProducto.Columns(23).Visible = True
            gvProducto.Columns(24).Visible = True
            gvProducto.Columns(25).Visible = True
            gvProducto.Columns(26).Visible = True
            gvProducto.Columns(27).Visible = True


            wceTotalPGV.Value = wceTotalPGV.Value - CDbl(gvProducto.Rows(gvProducto.SelectedIndex).Cells(9).Text.ToString)
            wceTotalA.Value = 0
            wceTotalAPGV.Value = 0
            wceTotalC.Value = wceTotalC.Value - CDbl(gvProducto.Rows(gvProducto.SelectedIndex).Cells(9).Text.ToString)
            wceTotalS.Value = wceTotalS.Value + CDbl(gvProducto.Rows(gvProducto.SelectedIndex).Cells(9).Text.ToString)
            lblTotalS.Text = wceTotalS.Value
            lblTotalC.Text = wceTotalC.Value
            Dim valor As String

            If gvProducto.Rows.Count <> 0 Then
                'Ya tiene Comprobaciones'
                Dim tabla As DataTable = New DataTable
                tabla.Columns.Add("id_concepto", GetType(String))
                tabla.Columns.Add("Fecha de realizacion", GetType(String))
                tabla.Columns.Add("Tipo", GetType(String))
                tabla.Columns.Add("Concepto", GetType(String))
                tabla.Columns.Add("No.Pers", GetType(String))
                tabla.Columns.Add("No.Dias", GetType(String))
                tabla.Columns.Add("Subtotal", GetType(String))
                tabla.Columns.Add("IVA", GetType(String))
                tabla.Columns.Add("Total", GetType(String))
                tabla.Columns.Add("%Aut", GetType(String))
                tabla.Columns.Add("RFC", GetType(String))
                tabla.Columns.Add("lugar_exp", GetType(String))
                tabla.Columns.Add("Proveedor", GetType(String))
                tabla.Columns.Add("No.Factura", GetType(String))
                tabla.Columns.Add("Origen", GetType(String))
                tabla.Columns.Add("Destino", GetType(String))
                tabla.Columns.Add("Vehiculo", GetType(String))
                tabla.Columns.Add("Observaciones", GetType(String))
                tabla.Columns.Add("txtOriDes", GetType(String))
                tabla.Columns.Add("id_lugar_orig", GetType(String))
                tabla.Columns.Add("id_lugar_dest", GetType(String))
                tabla.Columns.Add("isr_ret", GetType(String))
                tabla.Columns.Add("ruta", GetType(String))
                tabla.Columns.Add("nombre_fact", GetType(String))
                tabla.Columns.Add("ruta_fact", GetType(String))
                tabla.Columns.Add("%Auto", GetType(String))
                'Los que estan en el gv'
                For index As Integer = 0 To gvProducto.Rows.Count - 1

                    If index <> gvProducto.SelectedIndex Then
                        Dim Row1 As DataRow = tabla.NewRow
                        Row1("id_concepto") = gvProducto.Rows(index).Cells(1).Text
                        Row1("Fecha de realizacion") = gvProducto.Rows(index).Cells(2).Text
                        Row1("Tipo") = gvProducto.Rows(index).Cells(3).Text
                        Row1("Concepto") = gvProducto.Rows(index).Cells(4).Text
                        Row1("No.Pers") = gvProducto.Rows(index).Cells(5).Text
                        Row1("No.Dias") = gvProducto.Rows(index).Cells(6).Text
                        Row1("Subtotal") = gvProducto.Rows(index).Cells(7).Text
                        Row1("IVA") = gvProducto.Rows(index).Cells(8).Text
                        Row1("Total") = gvProducto.Rows(index).Cells(9).Text
                        Row1("%Aut") = gvProducto.Rows(index).Cells(10).Text
                        If gvProducto.Rows(index).Cells(11).Text = "&nbsp;" Then
                            Row1("RFC") = ""
                        Else
                            Row1("RFC") = gvProducto.Rows(index).Cells(11).Text
                        End If
                        If gvProducto.Rows(index).Cells(12).Text = "&nbsp;" Then
                            Row1("lugar_exp") = ""
                        Else
                            Row1("lugar_exp") = gvProducto.Rows(index).Cells(12).Text
                        End If
                        If gvProducto.Rows(index).Cells(13).Text = "&nbsp;" Then
                            Row1("Proveedor") = ""
                        Else
                            Row1("Proveedor") = gvProducto.Rows(index).Cells(13).Text
                        End If
                        If gvProducto.Rows(index).Cells(24).Text = "&nbsp;" Then
                            Row1("No.Factura") = ""
                        Else
                            Row1("No.Factura") = gvProducto.Rows(index).Cells(24).Text
                        End If
                        If gvProducto.Rows(index).Cells(15).Text = "&nbsp;" Then
                            Row1("Origen") = ""
                        Else
                            Row1("Origen") = gvProducto.Rows(index).Cells(15).Text
                        End If
                        If gvProducto.Rows(index).Cells(16).Text = "&nbsp;" Then
                            Row1("Destino") = ""
                        Else
                            Row1("Destino") = gvProducto.Rows(index).Cells(16).Text
                        End If
                        If gvProducto.Rows(index).Cells(17).Text = "&nbsp;" Then
                            Row1("Vehiculo") = ""
                        Else
                            Row1("Vehiculo") = gvProducto.Rows(index).Cells(17).Text
                        End If
                        If gvProducto.Rows(index).Cells(18).Text = "&nbsp;" Then
                            Row1("Observaciones") = ""
                        Else
                            valor = HttpUtility.HtmlDecode(Me.gvProducto.Rows(index).Cells(18).Text)
                            Row1("Observaciones") = valor
                        End If
                        If gvProducto.Rows(index).Cells(19).Text = "&nbsp;" Then
                            Row1("txtOriDes") = ""
                        Else
                            Row1("txtOriDes") = gvProducto.Rows(index).Cells(19).Text
                        End If
                        If gvProducto.Rows(index).Cells(20).Text = "&nbsp;" Then
                            Row1("id_lugar_orig") = ""
                        Else
                            Row1("id_lugar_orig") = gvProducto.Rows(index).Cells(20).Text
                        End If
                        If gvProducto.Rows(index).Cells(21).Text = "&nbsp;" Then
                            Row1("id_lugar_dest") = ""
                        Else
                            Row1("id_lugar_dest") = gvProducto.Rows(index).Cells(21).Text
                        End If
                        Row1("isr_ret") = gvProducto.Rows(index).Cells(22).Text
                        Row1("ruta") = gvProducto.Rows(index).Cells(23).Text
                        Row1("nombre_fact") = gvProducto.Rows(index).Cells(25).Text
                        Row1("ruta_fact") = gvProducto.Rows(index).Cells(26).Text
                        Row1("%Auto") = gvProducto.Rows(index).Cells(27).Text

                        tabla.Rows.Add(Row1)
                    Else

                    End If
                Next
                gvProducto.DataSource = tabla
                gvProducto.DataBind()
            Else
            End If
            gvProducto.Visible = True
            pnlGrid.Visible = True
            'pnlConceptos.Visible = False
            pnlConcepto1.Visible = True
            pnlAceptar.Visible = True
            pnlSuma.Visible = True
            lblNoConceptos.Text = gvProducto.Rows.Count
            'btnAgregar.Visible = True
            gvProducto.Columns(1).Visible = False
            'gvProducto.Columns(18).Visible = False
            'gvProducto.Columns(19).Visible = False
            'gvProducto.Columns(20).Visible = False
            'gvProducto.Columns(21).Visible = False
            'gvProducto.Columns(22).Visible = False
            'gvProducto.Columns(23).Visible = False
            'gvProducto.Columns(24).Visible = False
            'gvProducto.Columns(18).Visible = False
            gvProducto.Columns(19).Visible = False
            gvProducto.Columns(20).Visible = False
            gvProducto.Columns(21).Visible = False
            gvProducto.Columns(22).Visible = False
            gvProducto.Columns(23).Visible = False
            gvProducto.Columns(24).Visible = False
            gvProducto.Columns(25).Visible = False
            gvProducto.Columns(26).Visible = False
            gvProducto.Columns(27).Visible = False
            pnlEliminar.Visible = False
            gvProducto.SelectedIndex = -1

        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAEvidencia_Click(sender As Object, e As EventArgs) Handles btnAEvidencia.Click

        Try
            litError.Text = ""

            If System.IO.Path.GetFileName(fuEvidencia.PostedFile.FileName) = "" Then
                litError.Text = "No hay ningun archivo adjunto"
            Else
                gvEvidencias.Columns(2).Visible = True
                gvEvidencias.Columns(3).Visible = True

                'Agregar evidencia'
                Dim rutaArchivo As String = "Evidencias Comp\" 'Ruta en que se almacenará el archivo
                Dim sFileNameAr As String = System.IO.Path.GetFileName(fuEvidencia.PostedFile.FileName)
                'Guarda archivo'
                fuEvidencia.PostedFile.SaveAs(Server.MapPath("Evidencias Comp\" + _txtIdUsuario.Text.ToString + "-" + sFileNameAr))

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
                    Row("ruta") = "http://148.223.153.43/ProcAd/Evidencias Comp/"
                    Row("nombre_archivo") = sFileNameAr
                    Row("ruta_archivo") = "http://148.223.153.43/ProcAd/Evidencias Comp/"
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
                    Row1("ruta") = "http://148.223.153.43/ProcAd/Evidencias Comp/"
                    Row1("nombre_archivo") = sFileNameAr
                    Row1("ruta_archivo") = "http://148.223.153.43/ProcAd/Evidencias Comp/"
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
                    My.Computer.FileSystem.DeleteFile(Server.MapPath("Evidencias Comp\" + _txtIdUsuario.Text.ToString() + "-" + gvEvidencias.Rows(index).Cells(2).Text.ToString))
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

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            litError.Text = ""

            Dim totalAnt As Integer = 0
            Dim columnas As Integer
            If rbdOpcionAnticipo.Text = "Anticipo AMEX" Or _txtAnticipo.Text = "Anticipo AMEX" Then
                For Each row As GridViewRow In gvAnticiposAmex.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                        If chkRow.Checked Then
                            totalAnt = totalAnt + 1
                        End If
                    End If
                Next
                columnas = gvAnticiposAmex.Rows.Count()
            Else
                For Each row As GridViewRow In gvAnticipos.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                        If chkRow.Checked Then
                            totalAnt = totalAnt + 1
                        End If
                    End If
                Next
                columnas = gvAnticipos.Rows.Count()
            End If


            If columnas > 0 And totalAnt = 0 And Val(_txtAntPend.Text) = 0 Then
                litError.Text = "Favor de seleccionar el Anticipo a Comprobar"
            Else
                wceSubtotal1.Value = 0
                wceTotal1.Value = 0
                _txtBotonCancelar.Text = "S"
                sumarConceptos()
                'pnlConceptos.Visible = False
                pnlConcepto1.Visible = True
                pnlAceptar.Visible = True
                pnlGrid.Visible = True
                'btnAgregar.Visible = True
                pnlSuma.Visible = True
            End If
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub rbdOpcionAnticipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbdOpcionAnticipo.SelectedIndexChanged
        Try
            If rbdOpcionAnticipo.Items(0).Selected = True Then
                gvAnticipos.Visible = True
                _txtAnticipo.Text = "Anticipo"
                gvAnticiposAmex.Visible = False
            Else
                gvAnticiposAmex.Visible = True
                _txtAnticipo.Text = "Anticipo AMEX"
                gvAnticipos.Visible = False
            End If
            btnAceptar.Enabled = True
            pnlAnticiposDecision.Visible = False
            actAnticipos()
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

#End Region

End Class