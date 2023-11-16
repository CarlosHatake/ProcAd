Public Class _61
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

                        'Eliminar registro de Unidades no almacenadas previamente
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
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

                        .txtEmpleadoBus.Text = ""
                        buscarEmpl()

                        'RFC de la empresa
                        rfcEmpresa()
                        'Ingresar Tipos de Gastos
                        tipoGasto()
                        'Ingresar Division y/o Centro de Costo
                        tipoDivCC()
                        'Actividades
                        Dim sdaActividad As New SqlDataAdapter
                        Dim dsActividad As New DataSet
                        sdaActividad.SelectCommand = New SqlCommand("select id_actividad, actividad " +
                                                                    "from cg_actividad " +
                                                                    "where status = 'A' " +
                                                                    "order by actividad ", ConexionBD)
                        .ddlTipoAct.DataSource = dsActividad
                        .ddlTipoAct.DataTextField = "actividad"
                        .ddlTipoAct.DataValueField = "id_actividad"
                        ConexionBD.Open()
                        sdaActividad.Fill(dsActividad)
                        .ddlTipoAct.DataBind()
                        ConexionBD.Close()
                        sdaActividad.Dispose()
                        dsActividad.Dispose()

                        'Datos del Autorizador
                        Dim sdaAutorizador As New SqlDataAdapter
                        Dim dsAutorizador As New DataSet
                        sdaAutorizador.SelectCommand = New SqlCommand("select cgEmpl.id_empleado " +
                                                                      "     , cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " +
                                                                      "from cg_usuario " +
                                                                      "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                                      "where id_usuario in (select valor " +
                                                                      "                     from cg_parametros " +
                                                                      "                     where parametro = 'id_aut_finanzas') ", ConexionBD)
                        ConexionBD.Open()
                        sdaAutorizador.Fill(dsAutorizador)
                        ConexionBD.Close()
                        .lblAutorizador.Text = dsAutorizador.Tables(0).Rows(0).Item("nombre_empleado").ToString()
                        ._txtIdAutorizador.Text = dsAutorizador.Tables(0).Rows(0).Item("id_empleado").ToString()
                        sdaAutorizador.Dispose()
                        dsAutorizador.Dispose()

                        'Limpiar Campos
                        .txtJust.Text = ""

                        ''Límites de Fechas Min/Max
                        '.wdteFechaIni.MinValue = Now.Date.AddDays(-20)
                        '.wdteFechaFin.MinValue = Now.Date.AddDays(-20)
                        '.wdpFecha1.MinValue = Now.Date.AddDays(-20)
                        '.wdpFecha2.MinValue = Now.Date.AddDays(-20)
                        '.wdpFecha3.MinValue = Now.Date.AddDays(-20)
                        '.wdpFecha4.MinValue = Now.Date.AddDays(-20)
                        '.wdpFecha5.MinValue = Now.Date.AddDays(-20)
                        '.wdpFecha6.MinValue = Now.Date.AddDays(-20)
                        '.wdpFecha7.MinValue = Now.Date.AddDays(-20)
                        '.wdpFecha8.MinValue = Now.Date.AddDays(-20)
                        '.wdpFecha9.MinValue = Now.Date.AddDays(-20)
                        '.wdpFecha10.MinValue = Now.Date.AddDays(-20)
                        '.wdpFecha11.MinValue = Now.Date.AddDays(-20)
                        '.wdpFecha12.MinValue = Now.Date.AddDays(-20)
                        '.wdpFecha13.MinValue = Now.Date.AddDays(-20)
                        '.wdpFecha14.MinValue = Now.Date.AddDays(-20)
                        '.wdpFecha15.MinValue = Now.Date.AddDays(-20)
                        '.wdpFecha16.MinValue = Now.Date.AddDays(-20)
                        '.wdpFecha17.MinValue = Now.Date.AddDays(-20)
                        '.wdpFecha18.MinValue = Now.Date.AddDays(-20)
                        '.wdpFecha19.MinValue = Now.Date.AddDays(-20)
                        '.wdpFecha20.MinValue = Now.Date.AddDays(-20)

                        .wdteFechaIni.MaxValue = Now.Date
                        .wdteFechaFin.MaxValue = Now.Date
                        .wdpFecha1.MaxValue = Now.Date
                        .wdpFecha2.MaxValue = Now.Date
                        .wdpFecha3.MaxValue = Now.Date
                        .wdpFecha4.MaxValue = Now.Date
                        .wdpFecha5.MaxValue = Now.Date
                        .wdpFecha6.MaxValue = Now.Date
                        .wdpFecha7.MaxValue = Now.Date
                        .wdpFecha8.MaxValue = Now.Date
                        .wdpFecha9.MaxValue = Now.Date
                        .wdpFecha10.MaxValue = Now.Date
                        .wdpFecha11.MaxValue = Now.Date
                        .wdpFecha12.MaxValue = Now.Date
                        .wdpFecha13.MaxValue = Now.Date
                        .wdpFecha14.MaxValue = Now.Date
                        .wdpFecha15.MaxValue = Now.Date
                        .wdpFecha16.MaxValue = Now.Date
                        .wdpFecha17.MaxValue = Now.Date
                        .wdpFecha18.MaxValue = Now.Date
                        .wdpFecha19.MaxValue = Now.Date
                        .wdpFecha20.MaxValue = Now.Date

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
                        .ddlOrig2.DataSource = dsLugar
                        .ddlOrig3.DataSource = dsLugar
                        .ddlOrig4.DataSource = dsLugar
                        .ddlOrig5.DataSource = dsLugar
                        .ddlOrig6.DataSource = dsLugar
                        .ddlOrig7.DataSource = dsLugar
                        .ddlOrig8.DataSource = dsLugar
                        .ddlOrig9.DataSource = dsLugar
                        .ddlOrig10.DataSource = dsLugar
                        .ddlOrig11.DataSource = dsLugar
                        .ddlOrig12.DataSource = dsLugar
                        .ddlOrig13.DataSource = dsLugar
                        .ddlOrig14.DataSource = dsLugar
                        .ddlOrig15.DataSource = dsLugar
                        .ddlOrig16.DataSource = dsLugar
                        .ddlOrig17.DataSource = dsLugar
                        .ddlOrig18.DataSource = dsLugar
                        .ddlOrig19.DataSource = dsLugar
                        .ddlOrig20.DataSource = dsLugar
                        .ddlDest1.DataSource = dsLugar
                        .ddlDest2.DataSource = dsLugar
                        .ddlDest3.DataSource = dsLugar
                        .ddlDest4.DataSource = dsLugar
                        .ddlDest5.DataSource = dsLugar
                        .ddlDest6.DataSource = dsLugar
                        .ddlDest7.DataSource = dsLugar
                        .ddlDest8.DataSource = dsLugar
                        .ddlDest9.DataSource = dsLugar
                        .ddlDest10.DataSource = dsLugar
                        .ddlDest11.DataSource = dsLugar
                        .ddlDest12.DataSource = dsLugar
                        .ddlDest13.DataSource = dsLugar
                        .ddlDest14.DataSource = dsLugar
                        .ddlDest15.DataSource = dsLugar
                        .ddlDest16.DataSource = dsLugar
                        .ddlDest17.DataSource = dsLugar
                        .ddlDest18.DataSource = dsLugar
                        .ddlDest19.DataSource = dsLugar
                        .ddlDest20.DataSource = dsLugar
                        .ddlOrig1.DataTextField = "lugar"
                        .ddlOrig2.DataTextField = "lugar"
                        .ddlOrig3.DataTextField = "lugar"
                        .ddlOrig4.DataTextField = "lugar"
                        .ddlOrig5.DataTextField = "lugar"
                        .ddlOrig6.DataTextField = "lugar"
                        .ddlOrig7.DataTextField = "lugar"
                        .ddlOrig8.DataTextField = "lugar"
                        .ddlOrig9.DataTextField = "lugar"
                        .ddlOrig10.DataTextField = "lugar"
                        .ddlOrig11.DataTextField = "lugar"
                        .ddlOrig12.DataTextField = "lugar"
                        .ddlOrig13.DataTextField = "lugar"
                        .ddlOrig14.DataTextField = "lugar"
                        .ddlOrig15.DataTextField = "lugar"
                        .ddlOrig16.DataTextField = "lugar"
                        .ddlOrig17.DataTextField = "lugar"
                        .ddlOrig18.DataTextField = "lugar"
                        .ddlOrig19.DataTextField = "lugar"
                        .ddlOrig20.DataTextField = "lugar"
                        .ddlDest1.DataTextField = "lugar"
                        .ddlDest2.DataTextField = "lugar"
                        .ddlDest3.DataTextField = "lugar"
                        .ddlDest4.DataTextField = "lugar"
                        .ddlDest5.DataTextField = "lugar"
                        .ddlDest6.DataTextField = "lugar"
                        .ddlDest7.DataTextField = "lugar"
                        .ddlDest8.DataTextField = "lugar"
                        .ddlDest9.DataTextField = "lugar"
                        .ddlDest10.DataTextField = "lugar"
                        .ddlDest11.DataTextField = "lugar"
                        .ddlDest12.DataTextField = "lugar"
                        .ddlDest13.DataTextField = "lugar"
                        .ddlDest14.DataTextField = "lugar"
                        .ddlDest15.DataTextField = "lugar"
                        .ddlDest16.DataTextField = "lugar"
                        .ddlDest17.DataTextField = "lugar"
                        .ddlDest18.DataTextField = "lugar"
                        .ddlDest19.DataTextField = "lugar"
                        .ddlDest20.DataTextField = "lugar"
                        .ddlOrig1.DataValueField = "id_lugar"
                        .ddlOrig2.DataValueField = "id_lugar"
                        .ddlOrig3.DataValueField = "id_lugar"
                        .ddlOrig4.DataValueField = "id_lugar"
                        .ddlOrig5.DataValueField = "id_lugar"
                        .ddlOrig6.DataValueField = "id_lugar"
                        .ddlOrig7.DataValueField = "id_lugar"
                        .ddlOrig8.DataValueField = "id_lugar"
                        .ddlOrig9.DataValueField = "id_lugar"
                        .ddlOrig10.DataValueField = "id_lugar"
                        .ddlOrig11.DataValueField = "id_lugar"
                        .ddlOrig12.DataValueField = "id_lugar"
                        .ddlOrig13.DataValueField = "id_lugar"
                        .ddlOrig14.DataValueField = "id_lugar"
                        .ddlOrig15.DataValueField = "id_lugar"
                        .ddlOrig16.DataValueField = "id_lugar"
                        .ddlOrig17.DataValueField = "id_lugar"
                        .ddlOrig18.DataValueField = "id_lugar"
                        .ddlOrig19.DataValueField = "id_lugar"
                        .ddlOrig20.DataValueField = "id_lugar"
                        .ddlDest1.DataValueField = "id_lugar"
                        .ddlDest2.DataValueField = "id_lugar"
                        .ddlDest3.DataValueField = "id_lugar"
                        .ddlDest4.DataValueField = "id_lugar"
                        .ddlDest5.DataValueField = "id_lugar"
                        .ddlDest6.DataValueField = "id_lugar"
                        .ddlDest7.DataValueField = "id_lugar"
                        .ddlDest8.DataValueField = "id_lugar"
                        .ddlDest9.DataValueField = "id_lugar"
                        .ddlDest10.DataValueField = "id_lugar"
                        .ddlDest11.DataValueField = "id_lugar"
                        .ddlDest12.DataValueField = "id_lugar"
                        .ddlDest13.DataValueField = "id_lugar"
                        .ddlDest14.DataValueField = "id_lugar"
                        .ddlDest15.DataValueField = "id_lugar"
                        .ddlDest16.DataValueField = "id_lugar"
                        .ddlDest17.DataValueField = "id_lugar"
                        .ddlDest18.DataValueField = "id_lugar"
                        .ddlDest19.DataValueField = "id_lugar"
                        .ddlDest20.DataValueField = "id_lugar"
                        ConexionBD.Open()
                        sdaLugar.Fill(dsLugar)
                        .ddlOrig1.DataBind()
                        .ddlOrig2.DataBind()
                        .ddlOrig3.DataBind()
                        .ddlOrig4.DataBind()
                        .ddlOrig5.DataBind()
                        .ddlOrig6.DataBind()
                        .ddlOrig7.DataBind()
                        .ddlOrig8.DataBind()
                        .ddlOrig9.DataBind()
                        .ddlOrig10.DataBind()
                        .ddlOrig11.DataBind()
                        .ddlOrig12.DataBind()
                        .ddlOrig13.DataBind()
                        .ddlOrig14.DataBind()
                        .ddlOrig15.DataBind()
                        .ddlOrig16.DataBind()
                        .ddlOrig17.DataBind()
                        .ddlOrig18.DataBind()
                        .ddlOrig19.DataBind()
                        .ddlOrig20.DataBind()
                        .ddlDest1.DataBind()
                        .ddlDest2.DataBind()
                        .ddlDest3.DataBind()
                        .ddlDest4.DataBind()
                        .ddlDest5.DataBind()
                        .ddlDest6.DataBind()
                        .ddlDest7.DataBind()
                        .ddlDest8.DataBind()
                        .ddlDest9.DataBind()
                        .ddlDest10.DataBind()
                        .ddlDest11.DataBind()
                        .ddlDest12.DataBind()
                        .ddlDest13.DataBind()
                        .ddlDest14.DataBind()
                        .ddlDest15.DataBind()
                        .ddlDest16.DataBind()
                        .ddlDest17.DataBind()
                        .ddlDest18.DataBind()
                        .ddlDest19.DataBind()
                        .ddlDest20.DataBind()
                        ConexionBD.Close()
                        sdaLugar.Dispose()
                        dsLugar.Dispose()

                        'Anticipos
                        actAnticipos()

                        'Paneles de Conceptos
                        mostrarOcultarCampos()

                        'Botones
                        .btnSumar.Enabled = True
                        .upValeIng.Visible = False
                        .btnGuardar.Enabled = False
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

#Region "Empleado"

    Public Sub buscarEmpl()
        With Me
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            'Datos del Solicitante
            Dim sdaEmpleado As New SqlDataAdapter
            Dim dsEmpleado As New DataSet
            sdaEmpleado.SelectCommand = New SqlCommand("select cg_usuario.id_usuario " +
                                                       "     , cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " +
                                                       "from cg_usuario " +
                                                       "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                       "where cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno like '%' + @empleado + '%' " +
                                                       "order by nombre_empleado ", ConexionBD)
            sdaEmpleado.SelectCommand.Parameters.AddWithValue("@empleado", .txtEmpleadoBus.Text.Trim)
            .ddlEmpleado.DataSource = dsEmpleado
            .ddlEmpleado.DataTextField = "nombre_empleado"
            .ddlEmpleado.DataValueField = "id_usuario"
            ConexionBD.Open()
            sdaEmpleado.Fill(dsEmpleado)
            .ddlEmpleado.DataBind()
            ConexionBD.Close()
            sdaEmpleado.Dispose()
            dsEmpleado.Dispose()
            .ddlEmpleado.SelectedIndex = -1
            .upEmpleado.Update()

            If .ddlEmpleado.Items.Count = 0 Then
                localizarEmpl(0)
            Else
                localizarEmpl(.ddlEmpleado.SelectedValue)
                'Actualizar División / Centro de Costo
                tipoDivCC()
                'Obtener la abreviatura del Gasto
                actAbreviatura()
                'Actualizar Conceptos
                actConceptos()
            End If
        End With
    End Sub

    Public Sub localizarEmpl(ByVal idUsuario)
        With Me
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            'Datos del Solicitante
            Dim sdaEmpleado As New SqlDataAdapter
            Dim dsEmpleado As New DataSet
            sdaEmpleado.SelectCommand = New SqlCommand("select cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " +
                                                       "     , puesto_tabulador " +
                                                       "     , cgCC.id_cc " +
                                                       "     , cgCC.id_empresa " +
                                                       "     , case ant_pendientes when 'S' then 1 else 0 end as antPend " +
                                                       "     , unidad_comp " +
                                                       "from cg_usuario " +
                                                       "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                       "  left join bd_Empleado.dbo.cg_cc cgCC on cgEmpl.id_cc = cgCC.id_cc " +
                                                       "where id_usuario = @idUsuario ", ConexionBD)
            sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idUsuario", idUsuario)
            ConexionBD.Open()
            sdaEmpleado.Fill(dsEmpleado)
            ConexionBD.Close()
            '.lblEmpleado.Text = dsEmpleado.Tables(0).Rows(0).Item("nombre_empleado").ToString()
            ._txtPuestoTab.Text = dsEmpleado.Tables(0).Rows(0).Item("puesto_tabulador").ToString()
            .upPuestoTab.Update()
            ._txtIdEmpresaEmpl.Text = dsEmpleado.Tables(0).Rows(0).Item("id_empresa").ToString()
            .upIdEmpresaEmpl.Update()
            ._txtIdCCEmpl.Text = dsEmpleado.Tables(0).Rows(0).Item("id_cc").ToString()
            .upIdCCEmpl.Update()
            ._txtAntPend.Text = dsEmpleado.Tables(0).Rows(0).Item("antPend").ToString()
            .upAntPend.Update()
            If dsEmpleado.Tables(0).Rows(0).Item("unidad_comp").ToString() = "S" Then
                .pnlUnidad.Visible = True
            Else
                .pnlUnidad.Visible = False
            End If
            .upUnidad.Update()
            sdaEmpleado.Dispose()
            dsEmpleado.Dispose()

            actAnticipos()
        End With
    End Sub

    Protected Sub ibtnEmpleadoBus_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnEmpleadoBus.Click
        buscarEmpl()
    End Sub

    Protected Sub ddlEmpleado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEmpleado.SelectedIndexChanged
        localizarEmpl(Me.ddlEmpleado.SelectedValue)
        'Actualizar División / Centro de Costo
        tipoDivCC()
        'Obtener la abreviatura del Gasto
        actAbreviatura()
        'Actualizar Conceptos
        actConceptos()
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
                sdaTipoGasto.SelectCommand = New SqlCommand("SELECT id_gasto, nombre_gasto FROM cg_tipoGasto WHERE id_empresa = @idEmpresa AND status = 'A' ORDER BY nombre_gasto", ConexionBD)
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
                        .ddlDiv.Visible = True
                        .lblDiv.Text = ""
                    Else
                        .ddlDiv.Visible = False
                        .lblDiv.Text = "(No Aplica)"
                    End If
                    .upDiv.Update()

                    If dsTemp.Tables(0).Rows(0).Item("centro_costo").ToString() = "S" Then
                        'Centro de Costo
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
                        .ddlCC.Visible = True
                        .lblCC.Text = ""

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
                    Else
                        .ddlCC.Visible = False
                        .lblCC.Text = "(No Aplica)"
                    End If
                    .upCC.Update()

                    actIdCC()

                    sdaTemp.Dispose()
                    dsTemp.Dispose()
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub actIdCC()
        With Me
            Try
                If .upCC.Visible = False Then
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

    Public Sub actAnticipos()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaAnticipos As New SqlDataAdapter
                Dim dsAnticipos As New DataSet
                .gvAnticipos.DataSource = dsAnticipos
                'Habilitar columnas para actualización
                .gvAnticipos.Columns(0).Visible = True
                .gvAnticipos.Columns(3).Visible = True
                'Grupo de Unidades a Facturar
                sdaAnticipos.SelectCommand = New SqlCommand("select id_ms_anticipo " +
                                                            "     , fecha_pago as fecha " +
                                                            "     , isnull(monto_hospedaje, 0) + isnull(monto_alimentos, 0) + isnull(monto_casetas, 0) + isnull(monto_otros, 0) as importe " +
                                                            "from ms_anticipo " +
                                                            "where id_usr_solicita = @idUsuario " +
                                                            "  and status in ('EE', 'TR') " +
                                                            "  and empresa = @Empresa ", ConexionBD)
                sdaAnticipos.SelectCommand.Parameters.AddWithValue("@idUsuario", .ddlEmpleado.SelectedValue)
                sdaAnticipos.SelectCommand.Parameters.AddWithValue("@Empresa", .ddlEmpresa.SelectedItem.Text)
                ConexionBD.Open()
                sdaAnticipos.Fill(dsAnticipos)
                .gvAnticipos.DataBind()
                ConexionBD.Close()
                sdaAnticipos.Dispose()
                dsAnticipos.Dispose()
                .gvAnticipos.SelectedIndex = -1
                'Inhabilitar columnas para vista
                .gvAnticipos.Columns(0).Visible = False
                .gvAnticipos.Columns(3).Visible = False
                If .gvAnticipos.Rows.Count = 0 Then
                    .litError.Text = "No existen Anticipos por Comprobar"
                Else
                    .litError.Text = ""
                    For Each row As GridViewRow In .gvAnticipos.Rows
                        If row.RowType = DataControlRowType.DataRow Then
                            Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                            chkRow.Checked = False
                        End If
                    Next
                End If
                upAnticipos.Update()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub mostrarOcultarCampos()
        With Me
            Try
                .litError.Text = ""
                'Ocultar Paneles, excepto el primero
                .pnlConcepto1.Visible = True
                If .cbFactura1.Checked = False And .cbTabulador1.Checked = False Then
                    .cbFactura1.Checked = True
                    tipoFT("F", .cbFactura1, .upFactura1, .cbTabulador1, .upTabulador1, .ddlConcepto1, .upConcepto1, .wneNoPers1.Value, .upNoPers1, .wneNoDias1.Value, .upNoDias1, .txtRFC1, .ibtnRFCBus1, .upRFC1, .ddlNoFactura1, .upNoFactura1, .hlProveedor1, .upProveedor1, .wceSubtotal1, .upSubtotal1, .wceIVA1, .upIVA1, .wceTotal1, .upTotal1, .wpePorcentAut1, .upPorcentAut1)
                End If
                If .cbTabulador1.Checked = True And .txtObs1.Text.Trim = "" Then
                    .lblObsE1.Visible = True
                Else
                    .lblObsE1.Visible = False
                End If
                .upObsE1.Update()
                .lblNoFacturaE1.Visible = False
                .pnlConcepto2.Visible = False
                .pnlConcepto3.Visible = False
                .pnlConcepto4.Visible = False
                .pnlConcepto5.Visible = False
                .pnlConcepto6.Visible = False
                .pnlConcepto7.Visible = False
                .pnlConcepto8.Visible = False
                .pnlConcepto9.Visible = False
                .pnlConcepto10.Visible = False
                .pnlConcepto11.Visible = False
                .pnlConcepto12.Visible = False
                .pnlConcepto13.Visible = False
                .pnlConcepto14.Visible = False
                .pnlConcepto15.Visible = False
                .pnlConcepto16.Visible = False
                .pnlConcepto17.Visible = False
                .pnlConcepto18.Visible = False
                .pnlConcepto19.Visible = False
                .pnlConcepto20.Visible = False
                'Mostrar paneles, con base en el número de conceptos por comprobar
                If .ddlNoConceptos.SelectedValue >= 2 Then
                    .pnlConcepto2.Visible = True
                    If .cbFactura2.Checked = False And .cbTabulador2.Checked = False Then
                        .cbFactura2.Checked = True
                        tipoFT("F", .cbFactura2, .upFactura2, .cbTabulador2, .upTabulador2, .ddlConcepto2, .upConcepto2, .wneNoPers2.Value, .upNoPers2, .wneNoDias2.Value, .upNoDias2, .txtRFC2, .ibtnRFCBus2, .upRFC2, .ddlNoFactura2, .upNoFactura2, .hlProveedor2, .upProveedor2, .wceSubtotal2, .upSubtotal2, .wceIVA2, .upIVA2, .wceTotal2, .upTotal2, .wpePorcentAut2, .upPorcentAut2)
                    End If
                    If .cbTabulador2.Checked = True And .txtObs2.Text.Trim = "" Then
                        .lblObsE2.Visible = True
                    Else
                        .lblObsE2.Visible = False
                    End If
                    .upObsE2.Update()
                End If
                If .ddlNoConceptos.SelectedValue >= 3 Then
                    .pnlConcepto3.Visible = True
                    If .cbFactura3.Checked = False And .cbTabulador3.Checked = False Then
                        .cbFactura3.Checked = True
                        tipoFT("F", .cbFactura3, .upFactura3, .cbTabulador3, .upTabulador3, .ddlConcepto3, .upConcepto3, .wneNoPers3.Value, .upNoPers3, .wneNoDias3.Value, .upNoDias3, .txtRFC3, .ibtnRFCBus3, .upRFC3, .ddlNoFactura3, .upNoFactura3, .hlProveedor3, .upProveedor3, .wceSubtotal3, .upSubtotal3, .wceIVA3, .upIVA3, .wceTotal3, .upTotal3, .wpePorcentAut3, .upPorcentAut3)
                    End If
                    If .cbTabulador3.Checked = True And .txtObs3.Text.Trim = "" Then
                        .lblObsE3.Visible = True
                    Else
                        .lblObsE3.Visible = False
                    End If
                    .upObsE3.Update()
                End If
                If .ddlNoConceptos.SelectedValue >= 4 Then
                    .pnlConcepto4.Visible = True
                    If .cbFactura4.Checked = False And .cbTabulador4.Checked = False Then
                        .cbFactura4.Checked = True
                        tipoFT("F", .cbFactura4, .upFactura4, .cbTabulador4, .upTabulador4, .ddlConcepto4, .upConcepto4, .wneNoPers4.Value, .upNoPers4, .wneNoDias4.Value, .upNoDias4, .txtRFC4, .ibtnRFCBus4, .upRFC4, .ddlNoFactura4, .upNoFactura4, .hlProveedor4, .upProveedor4, .wceSubtotal4, .upSubtotal4, .wceIVA4, .upIVA4, .wceTotal4, .upTotal4, .wpePorcentAut4, .upPorcentAut4)
                    End If
                    If .cbTabulador4.Checked = True And .txtObs4.Text.Trim = "" Then
                        .lblObsE4.Visible = True
                    Else
                        .lblObsE4.Visible = False
                    End If
                    .upObsE4.Update()
                End If
                If .ddlNoConceptos.SelectedValue >= 5 Then
                    .pnlConcepto5.Visible = True
                    If .cbFactura5.Checked = False And .cbTabulador5.Checked = False Then
                        .cbFactura5.Checked = True
                        tipoFT("F", .cbFactura5, .upFactura5, .cbTabulador5, .upTabulador5, .ddlConcepto5, .upConcepto5, .wneNoPers5.Value, .upNoPers5, .wneNoDias5.Value, .upNoDias5, .txtRFC5, .ibtnRFCBus5, .upRFC5, .ddlNoFactura5, .upNoFactura5, .hlProveedor5, .upProveedor5, .wceSubtotal5, .upSubtotal5, .wceIVA5, .upIVA5, .wceTotal5, .upTotal5, .wpePorcentAut5, .upPorcentAut5)
                    End If
                    If .cbTabulador5.Checked = True And .txtObs5.Text.Trim = "" Then
                        .lblObsE5.Visible = True
                    Else
                        .lblObsE5.Visible = False
                    End If
                    .upObsE5.Update()
                End If
                If .ddlNoConceptos.SelectedValue >= 6 Then
                    .pnlConcepto6.Visible = True
                    If .cbFactura6.Checked = False And .cbTabulador6.Checked = False Then
                        .cbFactura6.Checked = True
                        tipoFT("F", .cbFactura6, .upFactura6, .cbTabulador6, .upTabulador6, .ddlConcepto6, .upConcepto6, .wneNoPers6.Value, .upNoPers6, .wneNoDias6.Value, .upNoDias6, .txtRFC6, .ibtnRFCBus6, .upRFC6, .ddlNoFactura6, .upNoFactura6, .hlProveedor6, .upProveedor6, .wceSubtotal6, .upSubtotal6, .wceIVA6, .upIVA6, .wceTotal6, .upTotal6, .wpePorcentAut6, .upPorcentAut6)
                    End If
                    If .cbTabulador6.Checked = True And .txtObs6.Text.Trim = "" Then
                        .lblObsE6.Visible = True
                    Else
                        .lblObsE6.Visible = False
                    End If
                    .upObsE6.Update()
                End If
                If .ddlNoConceptos.SelectedValue >= 7 Then
                    .pnlConcepto7.Visible = True
                    If .cbFactura7.Checked = False And .cbTabulador7.Checked = False Then
                        .cbFactura7.Checked = True
                        tipoFT("F", .cbFactura7, .upFactura7, .cbTabulador7, .upTabulador7, .ddlConcepto7, .upConcepto7, .wneNoPers7.Value, .upNoPers7, .wneNoDias7.Value, .upNoDias7, .txtRFC7, .ibtnRFCBus7, .upRFC7, .ddlNoFactura7, .upNoFactura7, .hlProveedor7, .upProveedor7, .wceSubtotal7, .upSubtotal7, .wceIVA7, .upIVA7, .wceTotal7, .upTotal7, .wpePorcentAut7, .upPorcentAut7)
                    End If
                    If .cbTabulador7.Checked = True And .txtObs7.Text.Trim = "" Then
                        .lblObsE7.Visible = True
                    Else
                        .lblObsE7.Visible = False
                    End If
                    .upObsE7.Update()
                End If
                If .ddlNoConceptos.SelectedValue >= 8 Then
                    .pnlConcepto8.Visible = True
                    If .cbFactura8.Checked = False And .cbTabulador8.Checked = False Then
                        .cbFactura8.Checked = True
                        tipoFT("F", .cbFactura8, .upFactura8, .cbTabulador8, .upTabulador8, .ddlConcepto8, .upConcepto8, .wneNoPers8.Value, .upNoPers8, .wneNoDias8.Value, .upNoDias8, .txtRFC8, .ibtnRFCBus8, .upRFC8, .ddlNoFactura8, .upNoFactura8, .hlProveedor8, .upProveedor8, .wceSubtotal8, .upSubtotal8, .wceIVA8, .upIVA8, .wceTotal8, .upTotal8, .wpePorcentAut8, .upPorcentAut8)
                    End If
                    If .cbTabulador8.Checked = True And .txtObs8.Text.Trim = "" Then
                        .lblObsE8.Visible = True
                    Else
                        .lblObsE8.Visible = False
                    End If
                    .upObsE8.Update()
                End If
                If .ddlNoConceptos.SelectedValue >= 9 Then
                    .pnlConcepto9.Visible = True
                    If .cbFactura9.Checked = False And .cbTabulador9.Checked = False Then
                        .cbFactura9.Checked = True
                        tipoFT("F", .cbFactura9, .upFactura9, .cbTabulador9, .upTabulador9, .ddlConcepto9, .upConcepto9, .wneNoPers9.Value, .upNoPers9, .wneNoDias9.Value, .upNoDias9, .txtRFC9, .ibtnRFCBus9, .upRFC9, .ddlNoFactura9, .upNoFactura9, .hlProveedor9, .upProveedor9, .wceSubtotal9, .upSubtotal9, .wceIVA9, .upIVA9, .wceTotal9, .upTotal9, .wpePorcentAut9, .upPorcentAut9)
                    End If
                    If .cbTabulador9.Checked = True And .txtObs9.Text.Trim = "" Then
                        .lblObsE9.Visible = True
                    Else
                        .lblObsE9.Visible = False
                    End If
                    .upObsE9.Update()
                End If
                If .ddlNoConceptos.SelectedValue >= 10 Then
                    .pnlConcepto10.Visible = True
                    If .cbFactura10.Checked = False And .cbTabulador10.Checked = False Then
                        .cbFactura10.Checked = True
                        tipoFT("F", .cbFactura10, .upFactura10, .cbTabulador10, .upTabulador10, .ddlConcepto10, .upConcepto10, .wneNoPers10.Value, .upNoPers10, .wneNoDias10.Value, .upNoDias10, .txtRFC10, .ibtnRFCBus10, .upRFC10, .ddlNoFactura10, .upNoFactura10, .hlProveedor10, .upProveedor10, .wceSubtotal10, .upSubtotal10, .wceIVA10, .upIVA10, .wceTotal10, .upTotal10, .wpePorcentAut10, .upPorcentAut10)
                    End If
                    If .cbTabulador10.Checked = True And .txtObs10.Text.Trim = "" Then
                        .lblObsE10.Visible = True
                    Else
                        .lblObsE10.Visible = False
                    End If
                    .upObsE10.Update()
                End If
                If .ddlNoConceptos.SelectedValue >= 11 Then
                    .pnlConcepto11.Visible = True
                    If .cbFactura11.Checked = False And .cbTabulador11.Checked = False Then
                        .cbFactura11.Checked = True
                        tipoFT("F", .cbFactura11, .upFactura11, .cbTabulador11, .upTabulador11, .ddlConcepto11, .upConcepto11, .wneNoPers11.Value, .upNoPers11, .wneNoDias11.Value, .upNoDias11, .txtRFC11, .ibtnRFCBus11, .upRFC11, .ddlNoFactura11, .upNoFactura11, .hlProveedor11, .upProveedor11, .wceSubtotal11, .upSubtotal11, .wceIVA11, .upIVA11, .wceTotal11, .upTotal11, .wpePorcentAut11, .upPorcentAut11)
                    End If
                    If .cbTabulador11.Checked = True And .txtObs11.Text.Trim = "" Then
                        .lblObsE11.Visible = True
                    Else
                        .lblObsE11.Visible = False
                    End If
                    .upObsE11.Update()
                End If
                If .ddlNoConceptos.SelectedValue >= 12 Then
                    .pnlConcepto12.Visible = True
                    If .cbFactura12.Checked = False And .cbTabulador12.Checked = False Then
                        .cbFactura12.Checked = True
                        tipoFT("F", .cbFactura12, .upFactura12, .cbTabulador12, .upTabulador12, .ddlConcepto12, .upConcepto12, .wneNoPers12.Value, .upNoPers12, .wneNoDias12.Value, .upNoDias12, .txtRFC12, .ibtnRFCBus12, .upRFC12, .ddlNoFactura12, .upNoFactura12, .hlProveedor12, .upProveedor12, .wceSubtotal12, .upSubtotal12, .wceIVA12, .upIVA12, .wceTotal12, .upTotal12, .wpePorcentAut12, .upPorcentAut12)
                    End If
                    If .cbTabulador12.Checked = True And .txtObs12.Text.Trim = "" Then
                        .lblObsE12.Visible = True
                    Else
                        .lblObsE12.Visible = False
                    End If
                    .upObsE12.Update()
                End If
                If .ddlNoConceptos.SelectedValue >= 13 Then
                    .pnlConcepto13.Visible = True
                    If .cbFactura13.Checked = False And .cbTabulador13.Checked = False Then
                        .cbFactura13.Checked = True
                        tipoFT("F", .cbFactura13, .upFactura13, .cbTabulador13, .upTabulador13, .ddlConcepto13, .upConcepto13, .wneNoPers13.Value, .upNoPers13, .wneNoDias13.Value, .upNoDias13, .txtRFC13, .ibtnRFCBus13, .upRFC13, .ddlNoFactura13, .upNoFactura13, .hlProveedor13, .upProveedor13, .wceSubtotal13, .upSubtotal13, .wceIVA13, .upIVA13, .wceTotal13, .upTotal13, .wpePorcentAut13, .upPorcentAut13)
                    End If
                    If .cbTabulador13.Checked = True And .txtObs13.Text.Trim = "" Then
                        .lblObsE13.Visible = True
                    Else
                        .lblObsE13.Visible = False
                    End If
                    .upObsE13.Update()
                End If
                If .ddlNoConceptos.SelectedValue >= 14 Then
                    .pnlConcepto14.Visible = True
                    If .cbFactura14.Checked = False And .cbTabulador14.Checked = False Then
                        .cbFactura14.Checked = True
                        tipoFT("F", .cbFactura14, .upFactura14, .cbTabulador14, .upTabulador14, .ddlConcepto14, .upConcepto14, .wneNoPers14.Value, .upNoPers14, .wneNoDias14.Value, .upNoDias14, .txtRFC14, .ibtnRFCBus14, .upRFC14, .ddlNoFactura14, .upNoFactura14, .hlProveedor14, .upProveedor14, .wceSubtotal14, .upSubtotal14, .wceIVA14, .upIVA14, .wceTotal14, .upTotal14, .wpePorcentAut14, .upPorcentAut14)
                    End If
                    If .cbTabulador14.Checked = True And .txtObs14.Text.Trim = "" Then
                        .lblObsE14.Visible = True
                    Else
                        .lblObsE14.Visible = False
                    End If
                    .upObsE14.Update()
                End If
                If .ddlNoConceptos.SelectedValue >= 15 Then
                    .pnlConcepto15.Visible = True
                    If .cbFactura15.Checked = False And .cbTabulador15.Checked = False Then
                        .cbFactura15.Checked = True
                        tipoFT("F", .cbFactura15, .upFactura15, .cbTabulador15, .upTabulador15, .ddlConcepto15, .upConcepto15, .wneNoPers15.Value, .upNoPers15, .wneNoDias15.Value, .upNoDias15, .txtRFC15, .ibtnRFCBus15, .upRFC15, .ddlNoFactura15, .upNoFactura15, .hlProveedor15, .upProveedor15, .wceSubtotal15, .upSubtotal15, .wceIVA15, .upIVA15, .wceTotal15, .upTotal15, .wpePorcentAut15, .upPorcentAut15)
                    End If
                    If .cbTabulador15.Checked = True And .txtObs15.Text.Trim = "" Then
                        .lblObsE15.Visible = True
                    Else
                        .lblObsE15.Visible = False
                    End If
                    .upObsE15.Update()
                End If
                If .ddlNoConceptos.SelectedValue >= 16 Then
                    .pnlConcepto16.Visible = True
                    If .cbFactura16.Checked = False And .cbTabulador16.Checked = False Then
                        .cbFactura16.Checked = True
                        tipoFT("F", .cbFactura16, .upFactura16, .cbTabulador16, .upTabulador16, .ddlConcepto16, .upConcepto16, .wneNoPers16.Value, .upNoPers16, .wneNoDias16.Value, .upNoDias16, .txtRFC16, .ibtnRFCBus16, .upRFC16, .ddlNoFactura16, .upNoFactura16, .hlProveedor16, .upProveedor16, .wceSubtotal16, .upSubtotal16, .wceIVA16, .upIVA16, .wceTotal16, .upTotal16, .wpePorcentAut16, .upPorcentAut16)
                    End If
                    If .cbTabulador16.Checked = True And .txtObs16.Text.Trim = "" Then
                        .lblObsE16.Visible = True
                    Else
                        .lblObsE16.Visible = False
                    End If
                    .upObsE16.Update()
                End If
                If .ddlNoConceptos.SelectedValue >= 17 Then
                    .pnlConcepto17.Visible = True
                    If .cbFactura17.Checked = False And .cbTabulador17.Checked = False Then
                        .cbFactura17.Checked = True
                        tipoFT("F", .cbFactura17, .upFactura17, .cbTabulador17, .upTabulador17, .ddlConcepto17, .upConcepto17, .wneNoPers17.Value, .upNoPers17, .wneNoDias17.Value, .upNoDias17, .txtRFC17, .ibtnRFCBus17, .upRFC17, .ddlNoFactura17, .upNoFactura17, .hlProveedor17, .upProveedor17, .wceSubtotal17, .upSubtotal17, .wceIVA17, .upIVA17, .wceTotal17, .upTotal17, .wpePorcentAut17, .upPorcentAut17)
                    End If
                    If .cbTabulador17.Checked = True And .txtObs17.Text.Trim = "" Then
                        .lblObsE17.Visible = True
                    Else
                        .lblObsE17.Visible = False
                    End If
                    .upObsE17.Update()
                End If
                If .ddlNoConceptos.SelectedValue >= 18 Then
                    .pnlConcepto18.Visible = True
                    If .cbFactura18.Checked = False And .cbTabulador18.Checked = False Then
                        .cbFactura18.Checked = True
                        tipoFT("F", .cbFactura18, .upFactura18, .cbTabulador18, .upTabulador18, .ddlConcepto18, .upConcepto18, .wneNoPers18.Value, .upNoPers18, .wneNoDias18.Value, .upNoDias18, .txtRFC18, .ibtnRFCBus18, .upRFC18, .ddlNoFactura18, .upNoFactura18, .hlProveedor18, .upProveedor18, .wceSubtotal18, .upSubtotal18, .wceIVA18, .upIVA18, .wceTotal18, .upTotal18, .wpePorcentAut18, .upPorcentAut18)
                    End If
                    If .cbTabulador18.Checked = True And .txtObs18.Text.Trim = "" Then
                        .lblObsE18.Visible = True
                    Else
                        .lblObsE18.Visible = False
                    End If
                    .upObsE18.Update()
                End If
                If .ddlNoConceptos.SelectedValue >= 19 Then
                    .pnlConcepto19.Visible = True
                    If .cbFactura19.Checked = False And .cbTabulador19.Checked = False Then
                        .cbFactura19.Checked = True
                        tipoFT("F", .cbFactura19, .upFactura19, .cbTabulador19, .upTabulador19, .ddlConcepto19, .upConcepto19, .wneNoPers19.Value, .upNoPers19, .wneNoDias19.Value, .upNoDias19, .txtRFC19, .ibtnRFCBus19, .upRFC19, .ddlNoFactura19, .upNoFactura19, .hlProveedor19, .upProveedor19, .wceSubtotal19, .upSubtotal19, .wceIVA19, .upIVA19, .wceTotal19, .upTotal19, .wpePorcentAut19, .upPorcentAut19)
                    End If
                    If .cbTabulador19.Checked = True And .txtObs19.Text.Trim = "" Then
                        .lblObsE19.Visible = True
                    Else
                        .lblObsE19.Visible = False
                    End If
                    .upObsE19.Update()
                End If
                If .ddlNoConceptos.SelectedValue >= 20 Then
                    .pnlConcepto20.Visible = True
                    If .cbFactura20.Checked = False And .cbTabulador20.Checked = False Then
                        .cbFactura20.Checked = True
                        tipoFT("F", .cbFactura20, .upFactura20, .cbTabulador20, .upTabulador20, .ddlConcepto20, .upConcepto20, .wneNoPers20.Value, .upNoPers20, .wneNoDias20.Value, .upNoDias20, .txtRFC20, .ibtnRFCBus20, .upRFC20, .ddlNoFactura20, .upNoFactura20, .hlProveedor20, .upProveedor20, .wceSubtotal20, .upSubtotal20, .wceIVA20, .upIVA20, .wceTotal20, .upTotal20, .wpePorcentAut20, .upPorcentAut20)
                    End If
                    If .cbTabulador20.Checked = True And .txtObs20.Text.Trim = "" Then
                        .lblObsE20.Visible = True
                    Else
                        .lblObsE20.Visible = False
                    End If
                    .upObsE20.Update()
                End If
                valNoFacturaNoRep()
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
                    tipoFT(tipo, .cbFactura1, .upFactura1, .cbTabulador1, .upTabulador1, .ddlConcepto1, .upConcepto1, .wneNoPers1.Value, .upNoPers1, .wneNoDias1.Value, .upNoDias1, .txtRFC1, .ibtnRFCBus1, .upRFC1, .ddlNoFactura1, .upNoFactura1, .hlProveedor1, .upProveedor1, .wceSubtotal1, .upSubtotal1, .wceIVA1, .upIVA1, .wceTotal1, .upTotal1, .wpePorcentAut1, .upPorcentAut1)
                End If
                If .pnlConcepto2.Visible = True Then
                    If .cbFactura2.Checked = True Then
                        tipo = "F"
                    Else
                        tipo = "T"
                    End If
                    tipoFT(tipo, .cbFactura2, .upFactura2, .cbTabulador2, .upTabulador2, .ddlConcepto2, .upConcepto2, .wneNoPers2.Value, .upNoPers2, .wneNoDias2.Value, .upNoDias2, .txtRFC2, .ibtnRFCBus2, .upRFC2, .ddlNoFactura2, .upNoFactura2, .hlProveedor2, .upProveedor2, .wceSubtotal2, .upSubtotal2, .wceIVA2, .upIVA2, .wceTotal2, .upTotal2, .wpePorcentAut2, .upPorcentAut2)
                End If
                If .pnlConcepto3.Visible = True Then
                    If .cbFactura3.Checked = True Then
                        tipo = "F"
                    Else
                        tipo = "T"
                    End If
                    tipoFT(tipo, .cbFactura3, .upFactura3, .cbTabulador3, .upTabulador3, .ddlConcepto3, .upConcepto3, .wneNoPers3.Value, .upNoPers3, .wneNoDias3.Value, .upNoDias3, .txtRFC3, .ibtnRFCBus3, .upRFC3, .ddlNoFactura3, .upNoFactura3, .hlProveedor3, .upProveedor3, .wceSubtotal3, .upSubtotal3, .wceIVA3, .upIVA3, .wceTotal3, .upTotal3, .wpePorcentAut3, .upPorcentAut3)
                End If
                If .pnlConcepto4.Visible = True Then
                    If .cbFactura4.Checked = True Then
                        tipo = "F"
                    Else
                        tipo = "T"
                    End If
                    tipoFT(tipo, .cbFactura4, .upFactura4, .cbTabulador4, .upTabulador4, .ddlConcepto4, .upConcepto4, .wneNoPers4.Value, .upNoPers4, .wneNoDias4.Value, .upNoDias4, .txtRFC4, .ibtnRFCBus4, .upRFC4, .ddlNoFactura4, .upNoFactura4, .hlProveedor4, .upProveedor4, .wceSubtotal4, .upSubtotal4, .wceIVA4, .upIVA4, .wceTotal4, .upTotal4, .wpePorcentAut4, .upPorcentAut4)
                End If
                If .pnlConcepto5.Visible = True Then
                    If .cbFactura5.Checked = True Then
                        tipo = "F"
                    Else
                        tipo = "T"
                    End If
                    tipoFT(tipo, .cbFactura5, .upFactura5, .cbTabulador5, .upTabulador5, .ddlConcepto5, .upConcepto5, .wneNoPers5.Value, .upNoPers5, .wneNoDias5.Value, .upNoDias5, .txtRFC5, .ibtnRFCBus5, .upRFC5, .ddlNoFactura5, .upNoFactura5, .hlProveedor5, .upProveedor5, .wceSubtotal5, .upSubtotal5, .wceIVA5, .upIVA5, .wceTotal5, .upTotal5, .wpePorcentAut5, .upPorcentAut5)
                End If
                If .pnlConcepto6.Visible = True Then
                    If .cbFactura6.Checked = True Then
                        tipo = "F"
                    Else
                        tipo = "T"
                    End If
                    tipoFT(tipo, .cbFactura6, .upFactura6, .cbTabulador6, .upTabulador6, .ddlConcepto6, .upConcepto6, .wneNoPers6.Value, .upNoPers6, .wneNoDias6.Value, .upNoDias6, .txtRFC6, .ibtnRFCBus6, .upRFC6, .ddlNoFactura6, .upNoFactura6, .hlProveedor6, .upProveedor6, .wceSubtotal6, .upSubtotal6, .wceIVA6, .upIVA6, .wceTotal6, .upTotal6, .wpePorcentAut6, .upPorcentAut6)
                End If
                If .pnlConcepto7.Visible = True Then
                    If .cbFactura7.Checked = True Then
                        tipo = "F"
                    Else
                        tipo = "T"
                    End If
                    tipoFT(tipo, .cbFactura7, .upFactura7, .cbTabulador7, .upTabulador7, .ddlConcepto7, .upConcepto7, .wneNoPers7.Value, .upNoPers7, .wneNoDias7.Value, .upNoDias7, .txtRFC7, .ibtnRFCBus7, .upRFC7, .ddlNoFactura7, .upNoFactura7, .hlProveedor7, .upProveedor7, .wceSubtotal7, .upSubtotal7, .wceIVA7, .upIVA7, .wceTotal7, .upTotal7, .wpePorcentAut7, .upPorcentAut7)
                End If
                If .pnlConcepto8.Visible = True Then
                    If .cbFactura8.Checked = True Then
                        tipo = "F"
                    Else
                        tipo = "T"
                    End If
                    tipoFT(tipo, .cbFactura8, .upFactura8, .cbTabulador8, .upTabulador8, .ddlConcepto8, .upConcepto8, .wneNoPers8.Value, .upNoPers8, .wneNoDias8.Value, .upNoDias8, .txtRFC8, .ibtnRFCBus8, .upRFC8, .ddlNoFactura8, .upNoFactura8, .hlProveedor8, .upProveedor8, .wceSubtotal8, .upSubtotal8, .wceIVA8, .upIVA8, .wceTotal8, .upTotal8, .wpePorcentAut8, .upPorcentAut8)
                End If
                If .pnlConcepto9.Visible = True Then
                    If .cbFactura9.Checked = True Then
                        tipo = "F"
                    Else
                        tipo = "T"
                    End If
                    tipoFT(tipo, .cbFactura9, .upFactura9, .cbTabulador9, .upTabulador9, .ddlConcepto9, .upConcepto9, .wneNoPers9.Value, .upNoPers9, .wneNoDias9.Value, .upNoDias9, .txtRFC9, .ibtnRFCBus9, .upRFC9, .ddlNoFactura9, .upNoFactura9, .hlProveedor9, .upProveedor9, .wceSubtotal9, .upSubtotal9, .wceIVA9, .upIVA9, .wceTotal9, .upTotal9, .wpePorcentAut9, .upPorcentAut9)
                End If
                If .pnlConcepto10.Visible = True Then
                    If .cbFactura10.Checked = True Then
                        tipo = "F"
                    Else
                        tipo = "T"
                    End If
                    tipoFT(tipo, .cbFactura10, .upFactura10, .cbTabulador10, .upTabulador10, .ddlConcepto10, .upConcepto10, .wneNoPers10.Value, .upNoPers10, .wneNoDias10.Value, .upNoDias10, .txtRFC10, .ibtnRFCBus10, .upRFC10, .ddlNoFactura10, .upNoFactura10, .hlProveedor10, .upProveedor10, .wceSubtotal10, .upSubtotal10, .wceIVA10, .upIVA10, .wceTotal10, .upTotal10, .wpePorcentAut10, .upPorcentAut10)
                End If
                If .pnlConcepto11.Visible = True Then
                    If .cbFactura11.Checked = True Then
                        tipo = "F"
                    Else
                        tipo = "T"
                    End If
                    tipoFT(tipo, .cbFactura11, .upFactura11, .cbTabulador11, .upTabulador11, .ddlConcepto11, .upConcepto11, .wneNoPers11.Value, .upNoPers11, .wneNoDias11.Value, .upNoDias11, .txtRFC11, .ibtnRFCBus11, .upRFC11, .ddlNoFactura11, .upNoFactura11, .hlProveedor11, .upProveedor11, .wceSubtotal11, .upSubtotal11, .wceIVA11, .upIVA11, .wceTotal11, .upTotal11, .wpePorcentAut11, .upPorcentAut11)
                End If
                If .pnlConcepto12.Visible = True Then
                    If .cbFactura12.Checked = True Then
                        tipo = "F"
                    Else
                        tipo = "T"
                    End If
                    tipoFT(tipo, .cbFactura12, .upFactura12, .cbTabulador12, .upTabulador12, .ddlConcepto12, .upConcepto12, .wneNoPers12.Value, .upNoPers12, .wneNoDias12.Value, .upNoDias12, .txtRFC12, .ibtnRFCBus12, .upRFC12, .ddlNoFactura12, .upNoFactura12, .hlProveedor12, .upProveedor12, .wceSubtotal12, .upSubtotal12, .wceIVA12, .upIVA12, .wceTotal12, .upTotal12, .wpePorcentAut12, .upPorcentAut12)
                End If
                If .pnlConcepto13.Visible = True Then
                    If .cbFactura13.Checked = True Then
                        tipo = "F"
                    Else
                        tipo = "T"
                    End If
                    tipoFT(tipo, .cbFactura13, .upFactura13, .cbTabulador13, .upTabulador13, .ddlConcepto13, .upConcepto13, .wneNoPers13.Value, .upNoPers13, .wneNoDias13.Value, .upNoDias13, .txtRFC13, .ibtnRFCBus13, .upRFC13, .ddlNoFactura13, .upNoFactura13, .hlProveedor13, .upProveedor13, .wceSubtotal13, .upSubtotal13, .wceIVA13, .upIVA13, .wceTotal13, .upTotal13, .wpePorcentAut13, .upPorcentAut13)
                End If
                If .pnlConcepto14.Visible = True Then
                    If .cbFactura14.Checked = True Then
                        tipo = "F"
                    Else
                        tipo = "T"
                    End If
                    tipoFT(tipo, .cbFactura14, .upFactura14, .cbTabulador14, .upTabulador14, .ddlConcepto14, .upConcepto14, .wneNoPers14.Value, .upNoPers14, .wneNoDias14.Value, .upNoDias14, .txtRFC14, .ibtnRFCBus14, .upRFC14, .ddlNoFactura14, .upNoFactura14, .hlProveedor14, .upProveedor14, .wceSubtotal14, .upSubtotal14, .wceIVA14, .upIVA14, .wceTotal14, .upTotal14, .wpePorcentAut14, .upPorcentAut14)
                End If
                If .pnlConcepto15.Visible = True Then
                    If .cbFactura15.Checked = True Then
                        tipo = "F"
                    Else
                        tipo = "T"
                    End If
                    tipoFT(tipo, .cbFactura15, .upFactura15, .cbTabulador15, .upTabulador15, .ddlConcepto15, .upConcepto15, .wneNoPers15.Value, .upNoPers15, .wneNoDias15.Value, .upNoDias15, .txtRFC15, .ibtnRFCBus15, .upRFC15, .ddlNoFactura15, .upNoFactura15, .hlProveedor15, .upProveedor15, .wceSubtotal15, .upSubtotal15, .wceIVA15, .upIVA15, .wceTotal15, .upTotal15, .wpePorcentAut15, .upPorcentAut15)
                End If
                If .pnlConcepto16.Visible = True Then
                    If .cbFactura16.Checked = True Then
                        tipo = "F"
                    Else
                        tipo = "T"
                    End If
                    tipoFT(tipo, .cbFactura16, .upFactura16, .cbTabulador16, .upTabulador16, .ddlConcepto16, .upConcepto16, .wneNoPers16.Value, .upNoPers16, .wneNoDias16.Value, .upNoDias16, .txtRFC16, .ibtnRFCBus16, .upRFC16, .ddlNoFactura16, .upNoFactura16, .hlProveedor16, .upProveedor16, .wceSubtotal16, .upSubtotal16, .wceIVA16, .upIVA16, .wceTotal16, .upTotal16, .wpePorcentAut16, .upPorcentAut16)
                End If
                If .pnlConcepto17.Visible = True Then
                    If .cbFactura17.Checked = True Then
                        tipo = "F"
                    Else
                        tipo = "T"
                    End If
                    tipoFT(tipo, .cbFactura17, .upFactura17, .cbTabulador17, .upTabulador17, .ddlConcepto17, .upConcepto17, .wneNoPers17.Value, .upNoPers17, .wneNoDias17.Value, .upNoDias17, .txtRFC17, .ibtnRFCBus17, .upRFC17, .ddlNoFactura17, .upNoFactura17, .hlProveedor17, .upProveedor17, .wceSubtotal17, .upSubtotal17, .wceIVA17, .upIVA17, .wceTotal17, .upTotal17, .wpePorcentAut17, .upPorcentAut17)
                End If
                If .pnlConcepto18.Visible = True Then
                    If .cbFactura18.Checked = True Then
                        tipo = "F"
                    Else
                        tipo = "T"
                    End If
                    tipoFT(tipo, .cbFactura18, .upFactura18, .cbTabulador18, .upTabulador18, .ddlConcepto18, .upConcepto18, .wneNoPers18.Value, .upNoPers18, .wneNoDias18.Value, .upNoDias18, .txtRFC18, .ibtnRFCBus18, .upRFC18, .ddlNoFactura18, .upNoFactura18, .hlProveedor18, .upProveedor18, .wceSubtotal18, .upSubtotal18, .wceIVA18, .upIVA18, .wceTotal18, .upTotal18, .wpePorcentAut18, .upPorcentAut18)
                End If
                If .pnlConcepto19.Visible = True Then
                    If .cbFactura19.Checked = True Then
                        tipo = "F"
                    Else
                        tipo = "T"
                    End If
                    tipoFT(tipo, .cbFactura19, .upFactura19, .cbTabulador19, .upTabulador19, .ddlConcepto19, .upConcepto19, .wneNoPers19.Value, .upNoPers19, .wneNoDias19.Value, .upNoDias19, .txtRFC19, .ibtnRFCBus19, .upRFC19, .ddlNoFactura19, .upNoFactura19, .hlProveedor19, .upProveedor19, .wceSubtotal19, .upSubtotal19, .wceIVA19, .upIVA19, .wceTotal19, .upTotal19, .wpePorcentAut19, .upPorcentAut19)
                End If
                If .pnlConcepto20.Visible = True Then
                    If .cbFactura20.Checked = True Then
                        tipo = "F"
                    Else
                        tipo = "T"
                    End If
                    tipoFT(tipo, .cbFactura20, .upFactura20, .cbTabulador20, .upTabulador20, .ddlConcepto20, .upConcepto20, .wneNoPers20.Value, .upNoPers20, .wneNoDias20.Value, .upNoDias20, .txtRFC20, .ibtnRFCBus20, .upRFC20, .ddlNoFactura20, .upNoFactura20, .hlProveedor20, .upProveedor20, .wceSubtotal20, .upSubtotal20, .wceIVA20, .upIVA20, .wceTotal20, .upTotal20, .wpePorcentAut20, .upPorcentAut20)
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub tipoFT(ByRef tipo, ByRef cbFactura, ByRef upFactura, ByRef cbTabulador, ByRef upTabulador, ByRef ddlConcepto, ByRef upConcepto, ByRef noPersonas, ByRef upNoPersonas, ByRef noDias, ByRef upNoDias, ByRef txtRFC, ByRef ibtnRFCBus, ByRef upRFC, ByRef ddlNoFactura, ByRef upNoFactura, ByRef lblProveedor, ByRef upProveedor, ByRef wceSubtotal, ByRef upSubtotal, ByRef wceIVA, ByRef upIVA, ByRef wceTotal, ByRef upTotal, ByRef wpePocentAut, ByRef upPocentAut)
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
                                                                "     , case when iva is null then abreviatura + ' - ' + concepto + ' ' + 'EXENTO' else abreviatura + ' - ' + concepto + ' ' + cast(cast((iva * 100) as int) as varchar(2)) + '%' end as nombre_concepto " +
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
                    actNoFactura(ddlConcepto.SelectedValue, noPersonas, upNoPersonas, noDias, upNoDias, txtRFC, ddlNoFactura, upNoFactura, lblProveedor, upProveedor, wceSubtotal, upSubtotal, wceIVA, upIVA, wceTotal, upTotal, wpePocentAut, upPocentAut)
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

    Public Sub actConceptosT(ByRef ddlConcepto, ByRef upConcepto)
        With Me
            Try
                'Lista de Conceptos
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaConceptoT As New SqlDataAdapter
                Dim dsConceptoT As New DataSet
                sdaConceptoT.SelectCommand = New SqlCommand("select id_concepto, abreviatura + ' - ' + nombre_concepto as nombre_concepto " +
                                                            "from cg_concepto " +
                                                            "where status = 'A' " +
                                                            "  and abreviatura = @abreviatura " +
                                                            "  and (id_cc = 0 or id_cc = @idCC) " +
                                                            "order by nombre_concepto ", ConexionBD)
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

    Public Sub actNoFactura(ByVal idConcepto, ByRef noPersonas, ByRef upNoPers, ByRef noDias, ByRef upNoDias, ByRef txtRFC, ByRef ddlNoFactura, ByRef upNoFactura, ByRef hlProveedor, ByRef upProveedor, ByRef wceSubtotal, ByRef upSubtotal, ByRef wceIVA, ByRef upIVA, ByRef wceTotal, ByRef upTotal, ByRef wpePorcentAut, ByRef upPorcentAut)
        With Me
            Try
                'Validar que el RFC del Proveedor no esté en la lista negra
                Dim conteoLN As Integer
                Dim ConexionBDLN As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBDLN.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValoresLN As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValoresLN.Connection = ConexionBDLN
                SCMValoresLN.CommandText = ""
                SCMValoresLN.Parameters.Clear()
                SCMValoresLN.CommandText = "select count(*) " +
                                           "from cg_lista_negra " +
                                           "where rfc = @rfc " +
                                           "  and status = 'A' "
                SCMValoresLN.Parameters.AddWithValue("@rfc", txtRFC.Text)
                ConexionBDLN.Open()
                conteoLN = SCMValoresLN.ExecuteScalar()
                ConexionBDLN.Close()

                If conteoLN = 0 Then
                    'Lista de Números de Facturas
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim sdaNoFactura As New SqlDataAdapter
                    Dim dsNoFactura As New DataSet

                    ' Nueva versión Procedimiento Almacenado Carlos Hernández 19 Jul 22
                    sdaNoFactura.SelectCommand = New SqlCommand("exec SP_C_dt_Factura @RFC,@idEmpresa, @idConcepto, @idUsuario, @fechaIni, @fechaFin", ConexionBD)

                    ''Nueva Versión donde considera el % de IVA
                    'sdaNoFactura.SelectCommand = New SqlCommand("select dt_factura.id_dt_factura " +
                    '                                            "	  , upper(dt_factura.serie + dt_factura.folio + ' [' + substring(dt_factura.uuid, 1, 6) + ']') as no_factura " +
                    '                                            "from dt_factura " +
                    '                                            "  left join (select uuid " +
                    '                                            "				   , importe " +
                    '                                            "				   , descuento " +
                    '                                            "				   , case when impuesto_tras_1 = 2 and tasa_tras_1 = 0.16 or impuesto_tras_2 = 2 and tasa_tras_2 = 0.16 or impuesto_tras_3 = 2 and tasa_tras_3 = 0.16 or impuesto_tras_4 = 2 and tasa_tras_4 = 0.16 then 0.16  " +
                    '                                            "						  when impuesto_tras_1 = 2 and tasa_tras_1 = 0.08 or impuesto_tras_2 = 2 and tasa_tras_2 = 0.08 or impuesto_tras_3 = 2 and tasa_tras_3 = 0.08 or impuesto_tras_4 = 2 and tasa_tras_4 = 0.08 then 0.08 " +
                    '                                            "						  when impuesto_tras_1 = 2 and tasa_tras_1 = 0 or impuesto_tras_2 = 2 and tasa_tras_2 = 0 or impuesto_tras_3 = 2 and tasa_tras_3 = 0 or impuesto_tras_4 = 2 and tasa_tras_4 = 0 then 0 " +
                    '                                            "				     end as tasa " +
                    '                                            "			  from dt_factura_linea " +
                    '                                            "             where dt_factura_linea.movimiento in ('RECIBIDAS', 'RECIBIDA')) as dt_linea on dt_factura.uuid = dt_linea.uuid " +
                    '                                            "where dt_factura.estatus <> 'CANCELADO' " +
                    '                                            "  and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                    '                                            "  and dt_factura.status = 'P' " +
                    '                                            "  and dt_factura.rfc_emisor = @RFC " +
                    '                                            "  and dt_factura.rfc_receptor in (select rfc " +
                    '                                            "								  from bd_Empleado.dbo.cg_empresa " +
                    '                                            "								  where id_empresa = @idEmpresa) " +
                    '                                            "  and dt_factura.importe > 0 " +
                    '                                            "  and (case when rfc_emisor = 'SCT051121M62' then case when dt_factura.importe < 80000 then 0 else 1 end else case when dt_factura.importe < 20000 then 0 else 1 end end) = 0 " +
                    '                                            "  and (case when dt_factura.forma_pago = '01 - Efectivo' and dt_factura.importe > 2320 then 1 else 0 end) = 0 " +
                    '                                            "  and ((select count(*) " +
                    '                                            "        from cg_usuario " +
                    '                                            "        where id_usuario = @idUsuario " +
                    '                                            "          and factura_extemp_comp = 'S') > 0 " +
                    '                                            "       or " +
                    '                                            "       (fecha_emision between dateadd(day, -1, @fechaIni) and dateadd(day, 7, @fechaFin))) " +
                    '                                            "group by dt_factura.id_dt_factura, dt_factura.serie + dt_factura.folio, dt_factura.uuid " +
                    '                                            "having (case when (select iva from cg_concepto_comp where id_concepto_comp = @idConcepto) is not null then case when max(tasa) = (select iva from cg_concepto_comp where id_concepto_comp = @idConcepto) then 0 else 1 end else case when (count(dt_linea.uuid) > 0 and max(tasa) is null) then 0 else 1 end end) = 0 " +
                    '                                            "order by upper(dt_factura.serie + dt_factura.folio + ' [' + substring(dt_factura.uuid, 1, 6) + ']') ", ConexionBD)
                    ''                                           "having max(tasa) = (select iva from cg_concepto_comp where id_concepto_comp = @idConcepto) " + _

                    ' INICIO * Validación pendiente

                    '"  and ((select count(*) " + _
                    '"        from cg_usuario " + _
                    '"        where id_usuario = @idUsuario " + _
                    '"          and factura_extemp = 'S') > 0 " + _
                    '"       or " + _
                    '"	   (case when (month(@fecha) = 12 and day(@fecha) < 25) then 0 " + _
                    '"          else case when (day(@fecha) = 1 and (fecha_emision >= (dateadd(day, -5, convert(date, @fecha))))) then 0 " + _
                    '"                 else case when month(fecha_emision) = month(@fecha) then 0 " + _
                    '"                        else 1 " + _
                    '"                      end " + _
                    '"               end " + _
                    '"        end = 0 " + _
                    '"		and year(fecha_emision) = year(@fecha))) " + _
                    '"  and year(fecha_emision) >= (select valor from cg_parametros where parametro = 'año_emision') " + _

                    ' FIN * Validación pendiente 

                    hlProveedor.ForeColor = Color.Blue
                    hlProveedor.Font.Bold = False

                    sdaNoFactura.SelectCommand.Parameters.AddWithValue("@RFC", txtRFC.Text)
                    sdaNoFactura.SelectCommand.Parameters.AddWithValue("@idEmpresa", .ddlEmpresa.SelectedValue)
                    sdaNoFactura.SelectCommand.Parameters.AddWithValue("@idConcepto", idConcepto)
                    sdaNoFactura.SelectCommand.Parameters.AddWithValue("@idUsuario", .ddlEmpleado.SelectedValue)
                    sdaNoFactura.SelectCommand.Parameters.AddWithValue("@fecha", Date.Now)
                    sdaNoFactura.SelectCommand.Parameters.AddWithValue("@fechaIni", .wdteFechaIni.Date)
                    sdaNoFactura.SelectCommand.Parameters.AddWithValue("@fechaFin", .wdteFechaFin.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
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
                        actFactura(idConcepto, noPersonas, upNoPers, noDias, upNoDias, ddlNoFactura.SelectedValue, hlProveedor, upProveedor, wceSubtotal, upSubtotal, wceIVA, upIVA, wceTotal, upTotal, wpePorcentAut, upPorcentAut)
                    End If
                Else
                    'Limpiar Lista
                    ddlNoFactura.Items.Clear()
                    upNoFactura.Update()

                    'Limpiar campos                   
                    hlProveedor.ForeColor = Color.Red
                    hlProveedor.Font.Bold = True
                    hlProveedor.Text = "¡Proveedor Inválido!"
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
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub actFactura(ByVal idConcepto, ByRef noPersonas, ByRef upNoPers, ByRef noDias, ByRef upNoDias, ByVal idDtFactura, ByRef hlProveedor, ByRef upProveedor, ByRef wceSubtotal, ByRef upSubtotal, ByRef wceIVA, ByRef upIVA, ByRef wceTotal, ByRef upTotal, ByRef wpePorcentAut, ByRef upPorcentAut)
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
                                                          "     , 'http://148.223.153.43/Facturas/' + dt_factura.rfc_receptor + '/' + format(datepart(year, dt_factura.fecha_emision), '00') + '/' + upper(format(dt_factura.fecha_emision, 'MMMM', 'es-es')) + '/' + dt_factura.movimiento + '/' + dt_factura.tipo + '/' + dt_factura.uuid + '.pdf' as path " +
                                                          "from dt_factura " +
                                                          "where id_dt_factura = @idDtFactura ", ConexionBD)
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
                sdaTabulador.SelectCommand.Parameters.AddWithValue("@idPuesto", ._txtPuestoTab.Text)
                sdaTabulador.SelectCommand.Parameters.AddWithValue("@idUsr", .ddlEmpleado.SelectedValue)
                ConexionBD.Open()
                sdaTabulador.Fill(dsTabulador)
                ConexionBD.Close()

                'Determinar si el concepto corresponde a Alimentos
                If dsTabulador.Tables(0).Rows(0).Item("alimentos").ToString() = 1 Then
                    noPersonas = 1
                    upNoPers.Update()
                    noDias = 1
                    upNoDias.Update()
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
                SCMValores.Parameters.AddWithValue("@idPuesto", ._txtPuestoTab.Text)
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

                sdaFactura.Dispose()
                dsFactura.Dispose()
                sdaTabulador.Dispose()
                dsTabulador.Dispose()
                valNoFacturaNoRep()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub actTabulador(ByVal idConcepto, ByRef noPersonas, ByRef upNoPers, ByRef noDias, ByRef upNoDias, ByRef wceSubtotal, ByRef upSubtotal, ByRef wceTotal, ByRef upTotal)
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
                sdaTabulador.SelectCommand.Parameters.AddWithValue("@idUsr", .ddlEmpleado.SelectedValue)
                ConexionBD.Open()
                sdaTabulador.Fill(dsTabulador)
                ConexionBD.Close()

                'Determinar si el concepto corresponde a Alimentos
                If dsTabulador.Tables(0).Rows(0).Item("alimentos").ToString() = 1 Then
                    noPersonas = 1
                    upNoPers.Update()
                    noDias = 1
                    upNoDias.Update()
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

                sdaTabulador.Dispose()
                dsTabulador.Dispose()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub valNoFacturaNoRep()
        With Me
            Try
                Dim banVal As Integer = 0

                If .ddlNoConceptos.SelectedValue >= 2 Then
                    'Matriz donde se contendrán los id_dt_facturas
                    Dim matNoFact(20, 2) As Integer
                    'Asignación de Valores
                    If .ddlNoFactura1.Items.Count = 0 Then
                        matNoFact(1, 1) = 0
                    Else
                        matNoFact(1, 1) = .ddlNoFactura1.SelectedValue
                    End If
                    matNoFact(1, 2) = 0
                    If .ddlNoConceptos.SelectedValue >= 2 Then
                        If .ddlNoFactura2.Items.Count = 0 Then
                            matNoFact(2, 1) = 0
                        Else
                            matNoFact(2, 1) = .ddlNoFactura2.SelectedValue
                        End If
                        matNoFact(2, 2) = 0
                    End If
                    If .ddlNoConceptos.SelectedValue >= 3 Then
                        If .ddlNoFactura3.Items.Count = 0 Then
                            matNoFact(3, 1) = 0
                        Else
                            matNoFact(3, 1) = .ddlNoFactura3.SelectedValue
                        End If
                        matNoFact(3, 2) = 0
                    End If
                    If .ddlNoConceptos.SelectedValue >= 4 Then
                        If .ddlNoFactura4.Items.Count = 0 Then
                            matNoFact(4, 1) = 0
                        Else
                            matNoFact(4, 1) = .ddlNoFactura4.SelectedValue
                        End If
                        matNoFact(4, 2) = 0
                    End If
                    If .ddlNoConceptos.SelectedValue >= 5 Then
                        If .ddlNoFactura5.Items.Count = 0 Then
                            matNoFact(5, 1) = 0
                        Else
                            matNoFact(5, 1) = .ddlNoFactura5.SelectedValue
                        End If
                        matNoFact(5, 2) = 0
                    End If
                    If .ddlNoConceptos.SelectedValue >= 6 Then
                        If .ddlNoFactura6.Items.Count = 0 Then
                            matNoFact(6, 1) = 0
                        Else
                            matNoFact(6, 1) = .ddlNoFactura6.SelectedValue
                        End If
                        matNoFact(6, 2) = 0
                    End If
                    If .ddlNoConceptos.SelectedValue >= 7 Then
                        If .ddlNoFactura7.Items.Count = 0 Then
                            matNoFact(7, 1) = 0
                        Else
                            matNoFact(7, 1) = .ddlNoFactura7.SelectedValue
                        End If
                        matNoFact(7, 2) = 0
                    End If
                    If .ddlNoConceptos.SelectedValue >= 8 Then
                        If .ddlNoFactura8.Items.Count = 0 Then
                            matNoFact(8, 1) = 0
                        Else
                            matNoFact(8, 1) = .ddlNoFactura8.SelectedValue
                        End If
                        matNoFact(8, 2) = 0
                    End If
                    If .ddlNoConceptos.SelectedValue >= 9 Then
                        If .ddlNoFactura9.Items.Count = 0 Then
                            matNoFact(9, 1) = 0
                        Else
                            matNoFact(9, 1) = .ddlNoFactura9.SelectedValue
                        End If
                        matNoFact(9, 2) = 0
                    End If
                    If .ddlNoConceptos.SelectedValue >= 10 Then
                        If .ddlNoFactura10.Items.Count = 0 Then
                            matNoFact(10, 1) = 0
                        Else
                            matNoFact(10, 1) = .ddlNoFactura10.SelectedValue
                        End If
                        matNoFact(10, 2) = 0
                    End If
                    If .ddlNoConceptos.SelectedValue >= 11 Then
                        If .ddlNoFactura11.Items.Count = 0 Then
                            matNoFact(11, 1) = 0
                        Else
                            matNoFact(11, 1) = .ddlNoFactura11.SelectedValue
                        End If
                        matNoFact(11, 2) = 0
                    End If
                    If .ddlNoConceptos.SelectedValue >= 12 Then
                        If .ddlNoFactura12.Items.Count = 0 Then
                            matNoFact(12, 1) = 0
                        Else
                            matNoFact(12, 1) = .ddlNoFactura12.SelectedValue
                        End If
                        matNoFact(12, 2) = 0
                    End If
                    If .ddlNoConceptos.SelectedValue >= 13 Then
                        If .ddlNoFactura13.Items.Count = 0 Then
                            matNoFact(13, 1) = 0
                        Else
                            matNoFact(13, 1) = .ddlNoFactura13.SelectedValue
                        End If
                        matNoFact(13, 2) = 0
                    End If
                    If .ddlNoConceptos.SelectedValue >= 14 Then
                        If .ddlNoFactura14.Items.Count = 0 Then
                            matNoFact(14, 1) = 0
                        Else
                            matNoFact(14, 1) = .ddlNoFactura14.SelectedValue
                        End If
                        matNoFact(14, 2) = 0
                    End If
                    If .ddlNoConceptos.SelectedValue >= 15 Then
                        If .ddlNoFactura15.Items.Count = 0 Then
                            matNoFact(15, 1) = 0
                        Else
                            matNoFact(15, 1) = .ddlNoFactura15.SelectedValue
                        End If
                        matNoFact(15, 2) = 0
                    End If
                    If .ddlNoConceptos.SelectedValue >= 16 Then
                        If .ddlNoFactura16.Items.Count = 0 Then
                            matNoFact(16, 1) = 0
                        Else
                            matNoFact(16, 1) = .ddlNoFactura16.SelectedValue
                        End If
                        matNoFact(16, 2) = 0
                    End If
                    If .ddlNoConceptos.SelectedValue >= 17 Then
                        If .ddlNoFactura17.Items.Count = 0 Then
                            matNoFact(17, 1) = 0
                        Else
                            matNoFact(17, 1) = .ddlNoFactura17.SelectedValue
                        End If
                        matNoFact(17, 2) = 0
                    End If
                    If .ddlNoConceptos.SelectedValue >= 18 Then
                        If .ddlNoFactura18.Items.Count = 0 Then
                            matNoFact(18, 1) = 0
                        Else
                            matNoFact(18, 1) = .ddlNoFactura18.SelectedValue
                        End If
                        matNoFact(18, 2) = 0
                    End If
                    If .ddlNoConceptos.SelectedValue >= 19 Then
                        If .ddlNoFactura19.Items.Count = 0 Then
                            matNoFact(19, 1) = 0
                        Else
                            matNoFact(19, 1) = .ddlNoFactura19.SelectedValue
                        End If
                        matNoFact(19, 2) = 0
                    End If
                    If .ddlNoConceptos.SelectedValue >= 20 Then
                        If .ddlNoFactura20.Items.Count = 0 Then
                            matNoFact(20, 1) = 0
                        Else
                            matNoFact(20, 1) = .ddlNoFactura20.SelectedValue
                        End If
                        matNoFact(20, 2) = 0
                    End If

                    'Validación
                    Dim i, iDet As Integer
                    For i = 1 To .ddlNoConceptos.SelectedValue
                        If matNoFact(i, 1) <> 0 Then
                            For iDet = i + 1 To .ddlNoConceptos.SelectedValue
                                If matNoFact(i, 1) = matNoFact(iDet, 1) And matNoFact(iDet, 1) <> 0 Then
                                    matNoFact(iDet, 2) = 1
                                End If
                            Next
                        End If
                    Next

                    'Etiquetas de Error
                    If .ddlNoConceptos.SelectedValue >= 2 And matNoFact(2, 2) = 1 Then
                        .lblNoFacturaE2.Visible = True
                        .upNoFactura2.Update()
                    Else
                        .lblNoFacturaE2.Visible = False
                        .upNoFactura2.Update()
                    End If
                    If .ddlNoConceptos.SelectedValue >= 3 And matNoFact(3, 2) = 1 Then
                        .lblNoFacturaE3.Visible = True
                        .upNoFactura3.Update()
                    Else
                        .lblNoFacturaE3.Visible = False
                        .upNoFactura3.Update()
                    End If
                    If .ddlNoConceptos.SelectedValue >= 4 And matNoFact(4, 2) = 1 Then
                        .lblNoFacturaE4.Visible = True
                        .upNoFactura4.Update()
                    Else
                        .lblNoFacturaE4.Visible = False
                        .upNoFactura4.Update()
                    End If
                    If .ddlNoConceptos.SelectedValue >= 5 And matNoFact(5, 2) = 1 Then
                        .lblNoFacturaE5.Visible = True
                        .upNoFactura5.Update()
                    Else
                        .lblNoFacturaE5.Visible = False
                        .upNoFactura5.Update()
                    End If
                    If .ddlNoConceptos.SelectedValue >= 6 And matNoFact(6, 2) = 1 Then
                        .lblNoFacturaE6.Visible = True
                        .upNoFactura6.Update()
                    Else
                        .lblNoFacturaE6.Visible = False
                        .upNoFactura6.Update()
                    End If
                    If .ddlNoConceptos.SelectedValue >= 7 And matNoFact(7, 2) = 1 Then
                        .lblNoFacturaE7.Visible = True
                        .upNoFactura7.Update()
                    Else
                        .lblNoFacturaE7.Visible = False
                        .upNoFactura7.Update()
                    End If
                    If .ddlNoConceptos.SelectedValue >= 8 And matNoFact(8, 2) = 1 Then
                        .lblNoFacturaE8.Visible = True
                        .upNoFactura8.Update()
                    Else
                        .lblNoFacturaE8.Visible = False
                        .upNoFactura8.Update()
                    End If
                    If .ddlNoConceptos.SelectedValue >= 9 And matNoFact(9, 2) = 1 Then
                        .lblNoFacturaE9.Visible = True
                        .upNoFactura9.Update()
                    Else
                        .lblNoFacturaE9.Visible = False
                        .upNoFactura9.Update()
                    End If
                    If .ddlNoConceptos.SelectedValue >= 10 And matNoFact(10, 2) = 1 Then
                        .lblNoFacturaE10.Visible = True
                        .upNoFactura10.Update()
                    Else
                        .lblNoFacturaE10.Visible = False
                        .upNoFactura10.Update()
                    End If
                    If .ddlNoConceptos.SelectedValue >= 11 And matNoFact(11, 2) = 1 Then
                        .lblNoFacturaE11.Visible = True
                        .upNoFactura11.Update()
                    Else
                        .lblNoFacturaE11.Visible = False
                        .upNoFactura11.Update()
                    End If
                    If .ddlNoConceptos.SelectedValue >= 12 And matNoFact(12, 2) = 1 Then
                        .lblNoFacturaE12.Visible = True
                        .upNoFactura12.Update()
                    Else
                        .lblNoFacturaE12.Visible = False
                        .upNoFactura12.Update()
                    End If
                    If .ddlNoConceptos.SelectedValue >= 13 And matNoFact(13, 2) = 1 Then
                        .lblNoFacturaE13.Visible = True
                        .upNoFactura13.Update()
                    Else
                        .lblNoFacturaE13.Visible = False
                        .upNoFactura13.Update()
                    End If
                    If .ddlNoConceptos.SelectedValue >= 14 And matNoFact(14, 2) = 1 Then
                        .lblNoFacturaE14.Visible = True
                        .upNoFactura14.Update()
                    Else
                        .lblNoFacturaE14.Visible = False
                        .upNoFactura14.Update()
                    End If
                    If .ddlNoConceptos.SelectedValue >= 15 And matNoFact(15, 2) = 1 Then
                        .lblNoFacturaE15.Visible = True
                        .upNoFactura15.Update()
                    Else
                        .lblNoFacturaE15.Visible = False
                        .upNoFactura15.Update()
                    End If
                    If .ddlNoConceptos.SelectedValue >= 16 And matNoFact(16, 2) = 1 Then
                        .lblNoFacturaE16.Visible = True
                        .upNoFactura16.Update()
                    Else
                        .lblNoFacturaE16.Visible = False
                        .upNoFactura16.Update()
                    End If
                    If .ddlNoConceptos.SelectedValue >= 17 And matNoFact(17, 2) = 1 Then
                        .lblNoFacturaE17.Visible = True
                        .upNoFactura17.Update()
                    Else
                        .lblNoFacturaE17.Visible = False
                        .upNoFactura17.Update()
                    End If
                    If .ddlNoConceptos.SelectedValue >= 18 And matNoFact(18, 2) = 1 Then
                        .lblNoFacturaE18.Visible = True
                        .upNoFactura18.Update()
                    Else
                        .lblNoFacturaE18.Visible = False
                        .upNoFactura18.Update()
                    End If
                    If .ddlNoConceptos.SelectedValue >= 19 And matNoFact(19, 2) = 1 Then
                        .lblNoFacturaE19.Visible = True
                        .upNoFactura19.Update()
                    Else
                        .lblNoFacturaE19.Visible = False
                        .upNoFactura19.Update()
                    End If
                    If .ddlNoConceptos.SelectedValue >= 20 And matNoFact(20, 2) = 1 Then
                        .lblNoFacturaE20.Visible = True
                        .upNoFactura20.Update()
                    Else
                        .lblNoFacturaE20.Visible = False
                        .upNoFactura20.Update()
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Function valNoFacturas()
        With Me
            Dim banF As Integer = 0

            If .cbFactura1.Checked = True And .ddlNoFactura1.Items.Count = 0 Then
                banF = 1
            End If
            If .ddlNoConceptos.SelectedValue >= 2 Then
                If .cbFactura2.Checked = True And .ddlNoFactura2.Items.Count = 0 Then
                    banF = 1
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 3 Then
                If .cbFactura3.Checked = True And .ddlNoFactura3.Items.Count = 0 Then
                    banF = 1
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 4 Then
                If .cbFactura4.Checked = True And .ddlNoFactura4.Items.Count = 0 Then
                    banF = 1
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 5 Then
                If .cbFactura5.Checked = True And .ddlNoFactura5.Items.Count = 0 Then
                    banF = 1
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 6 Then
                If .cbFactura6.Checked = True And .ddlNoFactura6.Items.Count = 0 Then
                    banF = 1
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 7 Then
                If .cbFactura7.Checked = True And .ddlNoFactura7.Items.Count = 0 Then
                    banF = 1
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 8 Then
                If .cbFactura8.Checked = True And .ddlNoFactura8.Items.Count = 0 Then
                    banF = 1
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 9 Then
                If .cbFactura9.Checked = True And .ddlNoFactura9.Items.Count = 0 Then
                    banF = 1
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 10 Then
                If .cbFactura10.Checked = True And .ddlNoFactura10.Items.Count = 0 Then
                    banF = 1
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 11 Then
                If .cbFactura11.Checked = True And .ddlNoFactura11.Items.Count = 0 Then
                    banF = 1
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 12 Then
                If .cbFactura12.Checked = True And .ddlNoFactura12.Items.Count = 0 Then
                    banF = 1
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 13 Then
                If .cbFactura13.Checked = True And .ddlNoFactura13.Items.Count = 0 Then
                    banF = 1
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 14 Then
                If .cbFactura14.Checked = True And .ddlNoFactura14.Items.Count = 0 Then
                    banF = 1
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 15 Then
                If .cbFactura15.Checked = True And .ddlNoFactura15.Items.Count = 0 Then
                    banF = 1
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 16 Then
                If .cbFactura16.Checked = True And .ddlNoFactura16.Items.Count = 0 Then
                    banF = 1
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 17 Then
                If .cbFactura17.Checked = True And .ddlNoFactura17.Items.Count = 0 Then
                    banF = 1
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 18 Then
                If .cbFactura18.Checked = True And .ddlNoFactura18.Items.Count = 0 Then
                    banF = 1
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 19 Then
                If .cbFactura19.Checked = True And .ddlNoFactura19.Items.Count = 0 Then
                    banF = 1
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 20 Then
                If .cbFactura20.Checked = True And .ddlNoFactura20.Items.Count = 0 Then
                    banF = 1
                End If
            End If

            If banF = 0 Then
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
            If .ddlNoConceptos.SelectedValue >= 2 Then
                If (.ddlOrig2.SelectedValue = 0 And .ddlDest2.SelectedValue > 0) Or (.ddlOrig2.SelectedValue > 0 And .ddlDest2.SelectedValue = 0) Then
                    banOD = 1
                Else
                    If .ddlOrig2.SelectedValue = 0 Then
                        .txtOriDes2.Text = ""
                    Else
                        .txtOriDes2.Text = .ddlOrig2.SelectedItem.Text + " - " + .ddlDest2.SelectedItem.Text
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 3 Then
                If (.ddlOrig3.SelectedValue = 0 And .ddlDest3.SelectedValue > 0) Or (.ddlOrig3.SelectedValue > 0 And .ddlDest3.SelectedValue = 0) Then
                    banOD = 1
                Else
                    If .ddlOrig3.SelectedValue = 0 Then
                        .txtOriDes3.Text = ""
                    Else
                        .txtOriDes3.Text = .ddlOrig3.SelectedItem.Text + " - " + .ddlDest3.SelectedItem.Text
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 4 Then
                If (.ddlOrig4.SelectedValue = 0 And .ddlDest4.SelectedValue > 0) Or (.ddlOrig4.SelectedValue > 0 And .ddlDest4.SelectedValue = 0) Then
                    banOD = 1
                Else
                    If .ddlOrig4.SelectedValue = 0 Then
                        .txtOriDes4.Text = ""
                    Else
                        .txtOriDes4.Text = .ddlOrig4.SelectedItem.Text + " - " + .ddlDest4.SelectedItem.Text
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 5 Then
                If (.ddlOrig5.SelectedValue = 0 And .ddlDest5.SelectedValue > 0) Or (.ddlOrig5.SelectedValue > 0 And .ddlDest5.SelectedValue = 0) Then
                    banOD = 1
                Else
                    If .ddlOrig5.SelectedValue = 0 Then
                        .txtOriDes5.Text = ""
                    Else
                        .txtOriDes5.Text = .ddlOrig5.SelectedItem.Text + " - " + .ddlDest5.SelectedItem.Text
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 6 Then
                If (.ddlOrig6.SelectedValue = 0 And .ddlDest6.SelectedValue > 0) Or (.ddlOrig6.SelectedValue > 0 And .ddlDest6.SelectedValue = 0) Then
                    banOD = 1
                Else
                    If .ddlOrig6.SelectedValue = 0 Then
                        .txtOriDes6.Text = ""
                    Else
                        .txtOriDes6.Text = .ddlOrig6.SelectedItem.Text + " - " + .ddlDest6.SelectedItem.Text
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 7 Then
                If (.ddlOrig7.SelectedValue = 0 And .ddlDest7.SelectedValue > 0) Or (.ddlOrig7.SelectedValue > 0 And .ddlDest7.SelectedValue = 0) Then
                    banOD = 1
                Else
                    If .ddlOrig7.SelectedValue = 0 Then
                        .txtOriDes7.Text = ""
                    Else
                        .txtOriDes7.Text = .ddlOrig7.SelectedItem.Text + " - " + .ddlDest7.SelectedItem.Text
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 8 Then
                If (.ddlOrig8.SelectedValue = 0 And .ddlDest8.SelectedValue > 0) Or (.ddlOrig8.SelectedValue > 0 And .ddlDest8.SelectedValue = 0) Then
                    banOD = 1
                Else
                    If .ddlOrig8.SelectedValue = 0 Then
                        .txtOriDes8.Text = ""
                    Else
                        .txtOriDes8.Text = .ddlOrig8.SelectedItem.Text + " - " + .ddlDest8.SelectedItem.Text
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 9 Then
                If (.ddlOrig9.SelectedValue = 0 And .ddlDest9.SelectedValue > 0) Or (.ddlOrig9.SelectedValue > 0 And .ddlDest9.SelectedValue = 0) Then
                    banOD = 1
                Else
                    If .ddlOrig9.SelectedValue = 0 Then
                        .txtOriDes9.Text = ""
                    Else
                        .txtOriDes9.Text = .ddlOrig9.SelectedItem.Text + " - " + .ddlDest9.SelectedItem.Text
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 10 Then
                If (.ddlOrig10.SelectedValue = 0 And .ddlDest10.SelectedValue > 0) Or (.ddlOrig10.SelectedValue > 0 And .ddlDest10.SelectedValue = 0) Then
                    banOD = 1
                Else
                    If .ddlOrig10.SelectedValue = 0 Then
                        .txtOriDes10.Text = ""
                    Else
                        .txtOriDes10.Text = .ddlOrig10.SelectedItem.Text + " - " + .ddlDest10.SelectedItem.Text
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 11 Then
                If (.ddlOrig11.SelectedValue = 0 And .ddlDest11.SelectedValue > 0) Or (.ddlOrig11.SelectedValue > 0 And .ddlDest11.SelectedValue = 0) Then
                    banOD = 1
                Else
                    If .ddlOrig11.SelectedValue = 0 Then
                        .txtOriDes11.Text = ""
                    Else
                        .txtOriDes11.Text = .ddlOrig11.SelectedItem.Text + " - " + .ddlDest11.SelectedItem.Text
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 12 Then
                If (.ddlOrig12.SelectedValue = 0 And .ddlDest12.SelectedValue > 0) Or (.ddlOrig12.SelectedValue > 0 And .ddlDest12.SelectedValue = 0) Then
                    banOD = 1
                Else
                    If .ddlOrig12.SelectedValue = 0 Then
                        .txtOriDes12.Text = ""
                    Else
                        .txtOriDes12.Text = .ddlOrig12.SelectedItem.Text + " - " + .ddlDest12.SelectedItem.Text
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 13 Then
                If (.ddlOrig13.SelectedValue = 0 And .ddlDest13.SelectedValue > 0) Or (.ddlOrig13.SelectedValue > 0 And .ddlDest13.SelectedValue = 0) Then
                    banOD = 1
                Else
                    If .ddlOrig13.SelectedValue = 0 Then
                        .txtOriDes13.Text = ""
                    Else
                        .txtOriDes13.Text = .ddlOrig13.SelectedItem.Text + " - " + .ddlDest13.SelectedItem.Text
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 14 Then
                If (.ddlOrig14.SelectedValue = 0 And .ddlDest14.SelectedValue > 0) Or (.ddlOrig14.SelectedValue > 0 And .ddlDest14.SelectedValue = 0) Then
                    banOD = 1
                Else
                    If .ddlOrig14.SelectedValue = 0 Then
                        .txtOriDes14.Text = ""
                    Else
                        .txtOriDes14.Text = .ddlOrig14.SelectedItem.Text + " - " + .ddlDest14.SelectedItem.Text
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 15 Then
                If (.ddlOrig15.SelectedValue = 0 And .ddlDest15.SelectedValue > 0) Or (.ddlOrig15.SelectedValue > 0 And .ddlDest15.SelectedValue = 0) Then
                    banOD = 1
                Else
                    If .ddlOrig15.SelectedValue = 0 Then
                        .txtOriDes15.Text = ""
                    Else
                        .txtOriDes15.Text = .ddlOrig15.SelectedItem.Text + " - " + .ddlDest15.SelectedItem.Text
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 16 Then
                If (.ddlOrig16.SelectedValue = 0 And .ddlDest16.SelectedValue > 0) Or (.ddlOrig16.SelectedValue > 0 And .ddlDest16.SelectedValue = 0) Then
                    banOD = 1
                Else
                    If .ddlOrig16.SelectedValue = 0 Then
                        .txtOriDes16.Text = ""
                    Else
                        .txtOriDes16.Text = .ddlOrig16.SelectedItem.Text + " - " + .ddlDest16.SelectedItem.Text
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 17 Then
                If (.ddlOrig17.SelectedValue = 0 And .ddlDest17.SelectedValue > 0) Or (.ddlOrig17.SelectedValue > 0 And .ddlDest17.SelectedValue = 0) Then
                    banOD = 1
                Else
                    If .ddlOrig17.SelectedValue = 0 Then
                        .txtOriDes17.Text = ""
                    Else
                        .txtOriDes17.Text = .ddlOrig17.SelectedItem.Text + " - " + .ddlDest17.SelectedItem.Text
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 18 Then
                If (.ddlOrig18.SelectedValue = 0 And .ddlDest18.SelectedValue > 0) Or (.ddlOrig18.SelectedValue > 0 And .ddlDest18.SelectedValue = 0) Then
                    banOD = 1
                Else
                    If .ddlOrig18.SelectedValue = 0 Then
                        .txtOriDes18.Text = ""
                    Else
                        .txtOriDes18.Text = .ddlOrig18.SelectedItem.Text + " - " + .ddlDest18.SelectedItem.Text
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 19 Then
                If (.ddlOrig19.SelectedValue = 0 And .ddlDest19.SelectedValue > 0) Or (.ddlOrig19.SelectedValue > 0 And .ddlDest19.SelectedValue = 0) Then
                    banOD = 1
                Else
                    If .ddlOrig19.SelectedValue = 0 Then
                        .txtOriDes19.Text = ""
                    Else
                        .txtOriDes19.Text = .ddlOrig19.SelectedItem.Text + " - " + .ddlDest19.SelectedItem.Text
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 20 Then
                If (.ddlOrig20.SelectedValue = 0 And .ddlDest20.SelectedValue > 0) Or (.ddlOrig20.SelectedValue > 0 And .ddlDest20.SelectedValue = 0) Then
                    banOD = 1
                Else
                    If .ddlOrig20.SelectedValue = 0 Then
                        .txtOriDes20.Text = ""
                    Else
                        .txtOriDes20.Text = .ddlOrig20.SelectedItem.Text + " - " + .ddlDest20.SelectedItem.Text
                    End If
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

    Public Sub sumarConceptos()
        With Me
            Try
                .litError.Text = ""


                If _txtVal_Origen_Destino1.Text = "S" Then
                    If ddlOrig1.SelectedItem.Text = "" Or ddlDest1.Text = "" Then
                        .litError.Text = "El concepto N°1 exige seleccionar el origen y el destino."
                        Return
                    End If
                End If
                If _txtVal_Origen_Destino2.Text = "S" Then
                    If ddlOrig2.SelectedItem.Text = "" Or ddlDest2.Text = "" Then
                        .litError.Text = "El concepto N°2 exige seleccionar el origen y el destino."
                        Return
                    End If
                End If
                If _txtVal_Origen_Destino3.Text = "S" Then
                    If ddlOrig3.SelectedItem.Text = "" Or ddlDest3.Text = "" Then
                        .litError.Text = "El concepto N°3 exige seleccionar el origen y el destino."
                        Return
                    End If
                End If
                If _txtVal_Origen_Destino4.Text = "S" Then
                    If ddlOrig4.SelectedItem.Text = "" Or ddlDest4.Text = "" Then
                        .litError.Text = "El concepto N°4 exige seleccionar el origen y el destino."
                        Return
                    End If
                End If
                If _txtVal_Origen_Destino5.Text = "S" Then
                    If ddlOrig5.SelectedItem.Text = "" Or ddlDest5.Text = "" Then
                        .litError.Text = "El concepto N°5 exige seleccionar el origen y el destino."
                        Return
                    End If
                End If
                If _txtVal_Origen_Destino6.Text = "S" Then
                    If ddlOrig6.SelectedItem.Text = "" Or ddlDest6.Text = "" Then
                        .litError.Text = "El concepto N°6 exige seleccionar el origen y el destino."
                        Return
                    End If
                End If
                If _txtVal_Origen_Destino7.Text = "S" Then
                    If ddlOrig7.SelectedItem.Text = "" Or ddlDest7.Text = "" Then
                        .litError.Text = "El concepto N°7 exige seleccionar el origen y el destino."
                        Return
                    End If
                End If
                If _txtVal_Origen_Destino8.Text = "S" Then
                    If ddlOrig8.SelectedItem.Text = "" Or ddlDest8.Text = "" Then
                        .litError.Text = "El concepto N°8 exige seleccionar el origen y el destino."
                        Return
                    End If
                End If
                If _txtVal_Origen_Destino9.Text = "S" Then
                    If ddlOrig9.SelectedItem.Text = "" Or ddlDest9.Text = "" Then
                        .litError.Text = "El concepto N°9 exige seleccionar el origen y el destino."
                        Return
                    End If
                End If
                If _txtVal_Origen_Destino10.Text = "S" Then
                    If ddlOrig10.SelectedItem.Text = "" Or ddlDest10.Text = "" Then
                        .litError.Text = "El concepto N°10 exige seleccionar el origen y el destino."
                        Return
                    End If
                End If
                If _txtVal_Origen_Destino11.Text = "S" Then
                    If ddlOrig11.SelectedItem.Text = "" Or ddlDest11.Text = "" Then
                        .litError.Text = "El concepto N°11 exige seleccionar el origen y el destino."
                        Return
                    End If
                End If
                If _txtVal_Origen_Destino12.Text = "S" Then
                    If ddlOrig12.SelectedItem.Text = "" Or ddlDest12.Text = "" Then
                        .litError.Text = "El concepto N°12 exige seleccionar el origen y el destino."
                        Return
                    End If
                End If
                If _txtVal_Origen_Destino13.Text = "S" Then
                    If ddlOrig13.SelectedItem.Text = "" Or ddlDest13.Text = "" Then
                        .litError.Text = "El concepto N°13 exige seleccionar el origen y el destino."
                        Return
                    End If
                End If
                If _txtVal_Origen_Destino14.Text = "S" Then
                    If ddlOrig14.SelectedItem.Text = "" Or ddlDest14.Text = "" Then
                        .litError.Text = "El concepto N°14 exige seleccionar el origen y el destino."
                        Return
                    End If
                End If
                If _txtVal_Origen_Destino15.Text = "S" Then
                    If ddlOrig15.SelectedItem.Text = "" Or ddlDest15.Text = "" Then
                        .litError.Text = "El concepto N°15 exige seleccionar el origen y el destino."
                        Return
                    End If
                End If
                If _txtVal_Origen_Destino16.Text = "S" Then
                    If ddlOrig16.SelectedItem.Text = "" Or ddlDest16.Text = "" Then
                        .litError.Text = "El concepto N°16 exige seleccionar el origen y el destino."
                        Return
                    End If
                End If
                If _txtVal_Origen_Destino17.Text = "S" Then
                    If ddlOrig17.SelectedItem.Text = "" Or ddlDest17.Text = "" Then
                        .litError.Text = "El concepto N°17 exige seleccionar el origen y el destino."
                        Return
                    End If
                End If
                If _txtVal_Origen_Destino18.Text = "S" Then
                    If ddlOrig18.SelectedItem.Text = "" Or ddlDest18.Text = "" Then
                        .litError.Text = "El concepto N°18 exige seleccionar el origen y el destino."
                        Return
                    End If
                End If
                If _txtVal_Origen_Destino19.Text = "S" Then
                    If ddlOrig19.SelectedItem.Text = "" Or ddlDest19.Text = "" Then
                        .litError.Text = "El concepto N°19 exige seleccionar el origen y el destino."
                        Return
                    End If
                End If
                If _txtVal_Origen_Destino20.Text = "S" Then
                    If ddlOrig20.SelectedItem.Text = "" Or ddlDest20.Text = "" Then
                        .litError.Text = "El concepto N°20 exige seleccionar el origen y el destino."
                        Return
                    End If
                End If



                If .txtJust.Text.Trim = "" Then
                    .litError.Text = "Información Insuficiente, favor de ingresar las Justificación correspondiente"
                Else
                    Dim totalAnt As Integer = 0
                    .wceTotalA.Value = 0
                    ._txtDev.Text = 0
                    For Each row As GridViewRow In .gvAnticipos.Rows
                        If row.RowType = DataControlRowType.DataRow Then
                            Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                            If chkRow.Checked Then
                                totalAnt = totalAnt + 1
                                .wceTotalA.Value = .wceTotalA.Value + Val(row.Cells(3).Text)
                            End If
                        End If
                    Next

                    If .gvAnticipos.Rows.Count > 0 And totalAnt = 0 And Val(._txtAntPend.Text) = 0 Then
                        .litError.Text = "Favor de seleccionar el Anticipo a Comprobar"
                    Else
                        If valNoFacturas() Then
                            If valOrigDest() Then
                                Dim banC As Integer = 0
                                Dim total As Decimal
                                Dim tipo As String

                                'Concepto No. 1
                                If .cbFactura1.Checked = True Then
                                    tipo = "F"
                                    .lblObsE1.Visible = False
                                    actFactura(.ddlConcepto1.SelectedValue, .wneNoPers1.Value, .upNoPers1, .wneNoDias1.Value, .upNoDias1, .ddlNoFactura1.SelectedValue, .hlProveedor1, .upProveedor1, .wceSubtotal1, .upSubtotal1, .wceIVA1, .upIVA1, .wceTotal1, .upTotal1, .wpePorcentAut1, .upPorcentAut1)
                                Else
                                    tipo = "T"
                                    .lblNoFacturaE1.Visible = False
                                    If .cbTabulador1.Checked = True And .txtObs1.Text.Trim = "" Then
                                        .lblObsE1.Visible = True
                                        banC = 1
                                    Else
                                        .lblObsE1.Visible = False
                                    End If
                                    actTabulador(.ddlConcepto1.SelectedValue, .wneNoPers1.Value, .upNoPers1, .wneNoDias1.Value, .upNoDias1, .wceSubtotal1, .upSubtotal1, .wceTotal1, .upTotal1)
                                    valDev(.ddlConcepto1)
                                End If
                                total = .wceTotal1.Value
                                If .ddlNoConceptos.SelectedValue >= 2 Then
                                    'Concepto No. 2
                                    If .cbFactura2.Checked = True Then
                                        tipo = "F"
                                        .lblObsE2.Visible = False
                                        actFactura(.ddlConcepto2.SelectedValue, .wneNoPers2.Value, .upNoPers2, .wneNoDias2.Value, .upNoDias2, .ddlNoFactura2.SelectedValue, .hlProveedor2, .upProveedor2, .wceSubtotal2, .upSubtotal2, .wceIVA2, .upIVA2, .wceTotal2, .upTotal2, .wpePorcentAut2, .upPorcentAut2)
                                    Else
                                        tipo = "T"
                                        .lblNoFacturaE2.Visible = False
                                        If .cbTabulador2.Checked = True And .txtObs2.Text.Trim = "" Then
                                            .lblObsE2.Visible = True
                                            banC = 1
                                        Else
                                            .lblObsE2.Visible = False
                                        End If
                                        actTabulador(.ddlConcepto2.SelectedValue, .wneNoPers2.Value, .upNoPers2, .wneNoDias2.Value, .upNoDias2, .wceSubtotal2, .upSubtotal2, .wceTotal2, .upTotal2)
                                        valDev(.ddlConcepto2)
                                    End If
                                    total = total + .wceTotal2.Value
                                End If
                                If .ddlNoConceptos.SelectedValue >= 3 Then
                                    'Concepto No. 3
                                    If .cbFactura3.Checked = True Then
                                        tipo = "F"
                                        .lblObsE3.Visible = False
                                        actFactura(.ddlConcepto3.SelectedValue, .wneNoPers3.Value, .upNoPers3, .wneNoDias3.Value, .upNoDias3, .ddlNoFactura3.SelectedValue, .hlProveedor3, .upProveedor3, .wceSubtotal3, .upSubtotal3, .wceIVA3, .upIVA3, .wceTotal3, .upTotal3, .wpePorcentAut3, .upPorcentAut3)
                                    Else
                                        tipo = "T"
                                        .lblNoFacturaE3.Visible = False
                                        If .cbTabulador3.Checked = True And .txtObs3.Text.Trim = "" Then
                                            .lblObsE3.Visible = True
                                            banC = 1
                                        Else
                                            .lblObsE3.Visible = False
                                        End If
                                        actTabulador(.ddlConcepto3.SelectedValue, .wneNoPers3.Value, .upNoPers3, .wneNoDias3.Value, .upNoDias3, .wceSubtotal3, .upSubtotal3, .wceTotal3, .upTotal3)
                                        valDev(.ddlConcepto3)
                                    End If
                                    total = total + .wceTotal3.Value
                                End If
                                If .ddlNoConceptos.SelectedValue >= 4 Then
                                    'Concepto No. 4
                                    If .cbFactura4.Checked = True Then
                                        tipo = "F"
                                        .lblObsE4.Visible = False
                                        actFactura(.ddlConcepto4.SelectedValue, .wneNoPers4.Value, .upNoPers4, .wneNoDias4.Value, .upNoDias4, .ddlNoFactura4.SelectedValue, .hlProveedor4, .upProveedor4, .wceSubtotal4, .upSubtotal4, .wceIVA4, .upIVA4, .wceTotal4, .upTotal4, .wpePorcentAut4, .upPorcentAut4)
                                    Else
                                        tipo = "T"
                                        .lblNoFacturaE4.Visible = False
                                        If .cbTabulador4.Checked = True And .txtObs4.Text.Trim = "" Then
                                            .lblObsE4.Visible = True
                                            banC = 1
                                        Else
                                            .lblObsE4.Visible = False
                                        End If
                                        actTabulador(.ddlConcepto4.SelectedValue, .wneNoPers4.Value, .upNoPers4, .wneNoDias4.Value, .upNoDias4, .wceSubtotal4, .upSubtotal4, .wceTotal4, .upTotal4)
                                        valDev(.ddlConcepto4)
                                    End If
                                    total = total + .wceTotal4.Value
                                End If
                                If .ddlNoConceptos.SelectedValue >= 5 Then
                                    'Concepto No. 5
                                    If .cbFactura5.Checked = True Then
                                        tipo = "F"
                                        .lblObsE5.Visible = False
                                        actFactura(.ddlConcepto5.SelectedValue, .wneNoPers5.Value, .upNoPers5, .wneNoDias5.Value, .upNoDias5, .ddlNoFactura5.SelectedValue, .hlProveedor5, .upProveedor5, .wceSubtotal5, .upSubtotal5, .wceIVA5, .upIVA5, .wceTotal5, .upTotal5, .wpePorcentAut5, .upPorcentAut5)
                                    Else
                                        tipo = "T"
                                        .lblNoFacturaE5.Visible = False
                                        If .cbTabulador5.Checked = True And .txtObs5.Text.Trim = "" Then
                                            .lblObsE5.Visible = True
                                            banC = 1
                                        Else
                                            .lblObsE5.Visible = False
                                        End If
                                        actTabulador(.ddlConcepto5.SelectedValue, .wneNoPers5.Value, .upNoPers5, .wneNoDias5.Value, .upNoDias5, .wceSubtotal5, .upSubtotal5, .wceTotal5, .upTotal5)
                                        valDev(.ddlConcepto5)
                                    End If
                                    total = total + .wceTotal5.Value
                                End If
                                If .ddlNoConceptos.SelectedValue >= 6 Then
                                    'Concepto No. 6
                                    If .cbFactura6.Checked = True Then
                                        tipo = "F"
                                        .lblObsE6.Visible = False
                                        actFactura(.ddlConcepto6.SelectedValue, .wneNoPers6.Value, .upNoPers6, .wneNoDias6.Value, .upNoDias6, .ddlNoFactura6.SelectedValue, .hlProveedor6, .upProveedor6, .wceSubtotal6, .upSubtotal6, .wceIVA6, .upIVA6, .wceTotal6, .upTotal6, .wpePorcentAut6, .upPorcentAut6)
                                    Else
                                        tipo = "T"
                                        .lblNoFacturaE6.Visible = False
                                        If .cbTabulador6.Checked = True And .txtObs6.Text.Trim = "" Then
                                            .lblObsE6.Visible = True
                                            banC = 1
                                        Else
                                            .lblObsE6.Visible = False
                                        End If
                                        actTabulador(.ddlConcepto6.SelectedValue, .wneNoPers6.Value, .upNoPers6, .wneNoDias6.Value, .upNoDias6, .wceSubtotal6, .upSubtotal6, .wceTotal6, .upTotal6)
                                        valDev(.ddlConcepto6)
                                    End If
                                    total = total + .wceTotal6.Value
                                End If
                                If .ddlNoConceptos.SelectedValue >= 7 Then
                                    'Concepto No. 7
                                    If .cbFactura7.Checked = True Then
                                        tipo = "F"
                                        .lblObsE7.Visible = False
                                        actFactura(.ddlConcepto7.SelectedValue, .wneNoPers7.Value, .upNoPers7, .wneNoDias7.Value, .upNoDias7, .ddlNoFactura7.SelectedValue, .hlProveedor7, .upProveedor7, .wceSubtotal7, .upSubtotal7, .wceIVA7, .upIVA7, .wceTotal7, .upTotal7, .wpePorcentAut7, .upPorcentAut7)
                                    Else
                                        tipo = "T"
                                        .lblNoFacturaE7.Visible = False
                                        If .cbTabulador7.Checked = True And .txtObs7.Text.Trim = "" Then
                                            .lblObsE7.Visible = True
                                            banC = 1
                                        Else
                                            .lblObsE7.Visible = False
                                        End If
                                        actTabulador(.ddlConcepto7.SelectedValue, .wneNoPers7.Value, .upNoPers7, .wneNoDias7.Value, .upNoDias7, .wceSubtotal7, .upSubtotal7, .wceTotal7, .upTotal7)
                                        valDev(.ddlConcepto7)
                                    End If
                                    total = total + .wceTotal7.Value
                                End If
                                If .ddlNoConceptos.SelectedValue >= 8 Then
                                    'Concepto No. 8
                                    If .cbFactura8.Checked = True Then
                                        tipo = "F"
                                        .lblObsE8.Visible = False
                                        actFactura(.ddlConcepto8.SelectedValue, .wneNoPers8.Value, .upNoPers8, .wneNoDias8.Value, .upNoDias8, .ddlNoFactura8.SelectedValue, .hlProveedor8, .upProveedor8, .wceSubtotal8, .upSubtotal8, .wceIVA8, .upIVA8, .wceTotal8, .upTotal8, .wpePorcentAut8, .upPorcentAut8)
                                    Else
                                        tipo = "T"
                                        .lblNoFacturaE8.Visible = False
                                        If .cbTabulador8.Checked = True And .txtObs8.Text.Trim = "" Then
                                            .lblObsE8.Visible = True
                                            banC = 1
                                        Else
                                            .lblObsE8.Visible = False
                                        End If
                                        actTabulador(.ddlConcepto8.SelectedValue, .wneNoPers8.Value, .upNoPers8, .wneNoDias8.Value, .upNoDias8, .wceSubtotal8, .upSubtotal8, .wceTotal8, .upTotal8)
                                        valDev(.ddlConcepto8)
                                    End If
                                    total = total + .wceTotal8.Value
                                End If
                                If .ddlNoConceptos.SelectedValue >= 9 Then
                                    'Concepto No. 9
                                    If .cbFactura9.Checked = True Then
                                        tipo = "F"
                                        .lblObsE9.Visible = False
                                        actFactura(.ddlConcepto9.SelectedValue, .wneNoPers9.Value, .upNoPers9, .wneNoDias9.Value, .upNoDias9, .ddlNoFactura9.SelectedValue, .hlProveedor9, .upProveedor9, .wceSubtotal9, .upSubtotal9, .wceIVA9, .upIVA9, .wceTotal9, .upTotal9, .wpePorcentAut9, .upPorcentAut9)
                                    Else
                                        tipo = "T"
                                        .lblNoFacturaE9.Visible = False
                                        If .cbTabulador9.Checked = True And .txtObs9.Text.Trim = "" Then
                                            .lblObsE9.Visible = True
                                            banC = 1
                                        Else
                                            .lblObsE9.Visible = False
                                        End If
                                        actTabulador(.ddlConcepto9.SelectedValue, .wneNoPers9.Value, .upNoPers9, .wneNoDias9.Value, .upNoDias9, .wceSubtotal9, .upSubtotal9, .wceTotal9, .upTotal9)
                                        valDev(.ddlConcepto9)
                                    End If
                                    total = total + .wceTotal9.Value
                                End If
                                If .ddlNoConceptos.SelectedValue >= 10 Then
                                    'Concepto No. 10
                                    If .cbFactura10.Checked = True Then
                                        tipo = "F"
                                        .lblObsE10.Visible = False
                                        actFactura(.ddlConcepto10.SelectedValue, .wneNoPers10.Value, .upNoPers10, .wneNoDias10.Value, .upNoDias10, .ddlNoFactura10.SelectedValue, .hlProveedor10, .upProveedor10, .wceSubtotal10, .upSubtotal10, .wceIVA10, .upIVA10, .wceTotal10, .upTotal10, .wpePorcentAut10, .upPorcentAut10)
                                    Else
                                        tipo = "T"
                                        .lblNoFacturaE10.Visible = False
                                        If .cbTabulador10.Checked = True And .txtObs10.Text.Trim = "" Then
                                            .lblObsE10.Visible = True
                                            banC = 1
                                        Else
                                            .lblObsE10.Visible = False
                                        End If
                                        actTabulador(.ddlConcepto10.SelectedValue, .wneNoPers10.Value, .upNoPers10, .wneNoDias10.Value, .upNoDias10, .wceSubtotal10, .upSubtotal10, .wceTotal10, .upTotal10)
                                        valDev(.ddlConcepto10)
                                    End If
                                    total = total + .wceTotal10.Value
                                End If
                                If .ddlNoConceptos.SelectedValue >= 11 Then
                                    'Concepto No. 11
                                    If .cbFactura11.Checked = True Then
                                        tipo = "F"
                                        .lblObsE11.Visible = False
                                        actFactura(.ddlConcepto11.SelectedValue, .wneNoPers11.Value, .upNoPers11, .wneNoDias11.Value, .upNoDias11, .ddlNoFactura11.SelectedValue, .hlProveedor11, .upProveedor11, .wceSubtotal11, .upSubtotal11, .wceIVA11, .upIVA11, .wceTotal11, .upTotal11, .wpePorcentAut11, .upPorcentAut11)
                                    Else
                                        tipo = "T"
                                        .lblNoFacturaE11.Visible = False
                                        If .cbTabulador11.Checked = True And .txtObs11.Text.Trim = "" Then
                                            .lblObsE11.Visible = True
                                            banC = 1
                                        Else
                                            .lblObsE11.Visible = False
                                        End If
                                        actTabulador(.ddlConcepto11.SelectedValue, .wneNoPers11.Value, .upNoPers11, .wneNoDias11.Value, .upNoDias11, .wceSubtotal11, .upSubtotal11, .wceTotal11, .upTotal11)
                                        valDev(.ddlConcepto11)
                                    End If
                                    total = total + .wceTotal11.Value
                                End If
                                If .ddlNoConceptos.SelectedValue >= 12 Then
                                    'Concepto No. 12
                                    If .cbFactura12.Checked = True Then
                                        tipo = "F"
                                        .lblObsE12.Visible = False
                                        actFactura(.ddlConcepto12.SelectedValue, .wneNoPers12.Value, .upNoPers12, .wneNoDias12.Value, .upNoDias12, .ddlNoFactura12.SelectedValue, .hlProveedor12, .upProveedor12, .wceSubtotal12, .upSubtotal12, .wceIVA12, .upIVA12, .wceTotal12, .upTotal12, .wpePorcentAut12, .upPorcentAut12)
                                    Else
                                        tipo = "T"
                                        .lblNoFacturaE12.Visible = False
                                        If .cbTabulador12.Checked = True And .txtObs12.Text.Trim = "" Then
                                            .lblObsE12.Visible = True
                                            banC = 1
                                        Else
                                            .lblObsE12.Visible = False
                                        End If
                                        actTabulador(.ddlConcepto12.SelectedValue, .wneNoPers12.Value, .upNoPers12, .wneNoDias12.Value, .upNoDias12, .wceSubtotal12, .upSubtotal12, .wceTotal12, .upTotal12)
                                        valDev(.ddlConcepto12)
                                    End If
                                    total = total + .wceTotal12.Value
                                End If
                                If .ddlNoConceptos.SelectedValue >= 13 Then
                                    'Concepto No. 13
                                    If .cbFactura13.Checked = True Then
                                        tipo = "F"
                                        .lblObsE13.Visible = False
                                        actFactura(.ddlConcepto13.SelectedValue, .wneNoPers13.Value, .upNoPers13, .wneNoDias13.Value, .upNoDias13, .ddlNoFactura13.SelectedValue, .hlProveedor13, .upProveedor13, .wceSubtotal13, .upSubtotal13, .wceIVA13, .upIVA13, .wceTotal13, .upTotal13, .wpePorcentAut13, .upPorcentAut13)
                                    Else
                                        tipo = "T"
                                        .lblNoFacturaE13.Visible = False
                                        If .cbTabulador13.Checked = True And .txtObs13.Text.Trim = "" Then
                                            .lblObsE13.Visible = True
                                            banC = 1
                                        Else
                                            .lblObsE13.Visible = False
                                        End If
                                        actTabulador(.ddlConcepto13.SelectedValue, .wneNoPers13.Value, .upNoPers13, .wneNoDias13.Value, .upNoDias13, .wceSubtotal13, .upSubtotal13, .wceTotal13, .upTotal13)
                                        valDev(.ddlConcepto13)
                                    End If
                                    total = total + .wceTotal13.Value
                                End If
                                If .ddlNoConceptos.SelectedValue >= 14 Then
                                    'Concepto No. 14
                                    If .cbFactura14.Checked = True Then
                                        tipo = "F"
                                        .lblObsE14.Visible = False
                                        actFactura(.ddlConcepto14.SelectedValue, .wneNoPers14.Value, .upNoPers14, .wneNoDias14.Value, .upNoDias14, .ddlNoFactura14.SelectedValue, .hlProveedor14, .upProveedor14, .wceSubtotal14, .upSubtotal14, .wceIVA14, .upIVA14, .wceTotal14, .upTotal14, .wpePorcentAut14, .upPorcentAut14)
                                    Else
                                        tipo = "T"
                                        .lblNoFacturaE14.Visible = False
                                        If .cbTabulador14.Checked = True And .txtObs14.Text.Trim = "" Then
                                            .lblObsE14.Visible = True
                                            banC = 1
                                        Else
                                            .lblObsE14.Visible = False
                                        End If
                                        actTabulador(.ddlConcepto14.SelectedValue, .wneNoPers14.Value, .upNoPers14, .wneNoDias14.Value, .upNoDias14, .wceSubtotal14, .upSubtotal14, .wceTotal14, .upTotal14)
                                        valDev(.ddlConcepto14)
                                    End If
                                    total = total + .wceTotal14.Value
                                End If
                                If .ddlNoConceptos.SelectedValue >= 15 Then
                                    'Concepto No. 15
                                    If .cbFactura15.Checked = True Then
                                        tipo = "F"
                                        .lblObsE15.Visible = False
                                        actFactura(.ddlConcepto15.SelectedValue, .wneNoPers15.Value, .upNoPers15, .wneNoDias15.Value, .upNoDias15, .ddlNoFactura15.SelectedValue, .hlProveedor15, .upProveedor15, .wceSubtotal15, .upSubtotal15, .wceIVA15, .upIVA15, .wceTotal15, .upTotal15, .wpePorcentAut15, .upPorcentAut15)
                                    Else
                                        tipo = "T"
                                        .lblNoFacturaE15.Visible = False
                                        If .cbTabulador15.Checked = True And .txtObs15.Text.Trim = "" Then
                                            .lblObsE15.Visible = True
                                            banC = 1
                                        Else
                                            .lblObsE15.Visible = False
                                        End If
                                        actTabulador(.ddlConcepto15.SelectedValue, .wneNoPers15.Value, .upNoPers15, .wneNoDias15.Value, .upNoDias15, .wceSubtotal15, .upSubtotal15, .wceTotal15, .upTotal15)
                                        valDev(.ddlConcepto15)
                                    End If
                                    total = total + .wceTotal15.Value
                                End If
                                If .ddlNoConceptos.SelectedValue >= 16 Then
                                    'Concepto No. 16
                                    If .cbFactura16.Checked = True Then
                                        tipo = "F"
                                        .lblObsE16.Visible = False
                                        actFactura(.ddlConcepto16.SelectedValue, .wneNoPers16.Value, .upNoPers16, .wneNoDias16.Value, .upNoDias16, .ddlNoFactura16.SelectedValue, .hlProveedor16, .upProveedor16, .wceSubtotal16, .upSubtotal16, .wceIVA16, .upIVA16, .wceTotal16, .upTotal16, .wpePorcentAut16, .upPorcentAut16)
                                    Else
                                        tipo = "T"
                                        .lblNoFacturaE16.Visible = False
                                        If .cbTabulador16.Checked = True And .txtObs16.Text.Trim = "" Then
                                            .lblObsE16.Visible = True
                                            banC = 1
                                        Else
                                            .lblObsE16.Visible = False
                                        End If
                                        actTabulador(.ddlConcepto16.SelectedValue, .wneNoPers16.Value, .upNoPers16, .wneNoDias16.Value, .upNoDias16, .wceSubtotal16, .upSubtotal16, .wceTotal16, .upTotal16)
                                        valDev(.ddlConcepto16)
                                    End If
                                    total = total + .wceTotal16.Value
                                End If
                                If .ddlNoConceptos.SelectedValue >= 17 Then
                                    'Concepto No. 17
                                    If .cbFactura17.Checked = True Then
                                        tipo = "F"
                                        .lblObsE17.Visible = False
                                        actFactura(.ddlConcepto17.SelectedValue, .wneNoPers17.Value, .upNoPers17, .wneNoDias17.Value, .upNoDias17, .ddlNoFactura17.SelectedValue, .hlProveedor17, .upProveedor17, .wceSubtotal17, .upSubtotal17, .wceIVA17, .upIVA17, .wceTotal17, .upTotal17, .wpePorcentAut17, .upPorcentAut17)
                                    Else
                                        tipo = "T"
                                        .lblNoFacturaE17.Visible = False
                                        If .cbTabulador17.Checked = True And .txtObs17.Text.Trim = "" Then
                                            .lblObsE17.Visible = True
                                            banC = 1
                                        Else
                                            .lblObsE17.Visible = False
                                        End If
                                        actTabulador(.ddlConcepto17.SelectedValue, .wneNoPers17.Value, .upNoPers17, .wneNoDias17.Value, .upNoDias17, .wceSubtotal17, .upSubtotal17, .wceTotal17, .upTotal17)
                                        valDev(.ddlConcepto17)
                                    End If
                                    total = total + .wceTotal17.Value
                                End If
                                If .ddlNoConceptos.SelectedValue >= 18 Then
                                    'Concepto No. 18
                                    If .cbFactura18.Checked = True Then
                                        tipo = "F"
                                        .lblObsE18.Visible = False
                                        actFactura(.ddlConcepto18.SelectedValue, .wneNoPers18.Value, .upNoPers18, .wneNoDias18.Value, .upNoDias18, .ddlNoFactura18.SelectedValue, .hlProveedor18, .upProveedor18, .wceSubtotal18, .upSubtotal18, .wceIVA18, .upIVA18, .wceTotal18, .upTotal18, .wpePorcentAut18, .upPorcentAut18)
                                    Else
                                        tipo = "T"
                                        .lblNoFacturaE18.Visible = False
                                        If .cbTabulador18.Checked = True And .txtObs18.Text.Trim = "" Then
                                            .lblObsE18.Visible = True
                                            banC = 1
                                        Else
                                            .lblObsE18.Visible = False
                                        End If
                                        actTabulador(.ddlConcepto18.SelectedValue, .wneNoPers18.Value, .upNoPers18, .wneNoDias18.Value, .upNoDias18, .wceSubtotal18, .upSubtotal18, .wceTotal18, .upTotal18)
                                        valDev(.ddlConcepto18)
                                    End If
                                    total = total + .wceTotal18.Value
                                End If
                                If .ddlNoConceptos.SelectedValue >= 19 Then
                                    'Concepto No. 19
                                    If .cbFactura19.Checked = True Then
                                        tipo = "F"
                                        .lblObsE19.Visible = False
                                        actFactura(.ddlConcepto19.SelectedValue, .wneNoPers19.Value, .upNoPers19, .wneNoDias19.Value, .upNoDias19, .ddlNoFactura19.SelectedValue, .hlProveedor19, .upProveedor19, .wceSubtotal19, .upSubtotal19, .wceIVA19, .upIVA19, .wceTotal19, .upTotal19, .wpePorcentAut19, .upPorcentAut19)
                                    Else
                                        tipo = "T"
                                        .lblNoFacturaE19.Visible = False
                                        If .cbTabulador19.Checked = True And .txtObs19.Text.Trim = "" Then
                                            .lblObsE19.Visible = True
                                            banC = 1
                                        Else
                                            .lblObsE19.Visible = False
                                        End If
                                        actTabulador(.ddlConcepto19.SelectedValue, .wneNoPers19.Value, .upNoPers19, .wneNoDias19.Value, .upNoDias19, .wceSubtotal19, .upSubtotal19, .wceTotal19, .upTotal19)
                                        valDev(.ddlConcepto19)
                                    End If
                                    total = total + .wceTotal19.Value
                                End If
                                If .ddlNoConceptos.SelectedValue >= 20 Then
                                    'Concepto No. 20
                                    If .cbFactura20.Checked = True Then
                                        tipo = "F"
                                        .lblObsE20.Visible = False
                                        actFactura(.ddlConcepto20.SelectedValue, .wneNoPers20.Value, .upNoPers20, .wneNoDias20.Value, .upNoDias20, .ddlNoFactura20.SelectedValue, .hlProveedor20, .upProveedor20, .wceSubtotal20, .upSubtotal20, .wceIVA20, .upIVA20, .wceTotal20, .upTotal20, .wpePorcentAut20, .upPorcentAut20)
                                    Else
                                        tipo = "T"
                                        .lblNoFacturaE20.Visible = False
                                        If .cbTabulador20.Checked = True And .txtObs20.Text.Trim = "" Then
                                            .lblObsE20.Visible = True
                                            banC = 1
                                        Else
                                            .lblObsE20.Visible = False
                                        End If
                                        actTabulador(.ddlConcepto20.SelectedValue, .wneNoPers20.Value, .upNoPers20, .wneNoDias20.Value, .upNoDias20, .wceSubtotal20, .upSubtotal20, .wceTotal20, .upTotal20)
                                        valDev(.ddlConcepto20)
                                    End If
                                    total = total + .wceTotal20.Value
                                End If

                                'Asignar total a etiqueta
                                .wceTotalC.Value = total
                                .wceTotalS.Value = .wceTotalA.Value - .wceTotalC.Value
                                .lblTotalA.Text = .wceTotalA.Text
                                .lblTotalC.Text = .wceTotalC.Text
                                .lblTotalS.Text = .wceTotalS.Text

                                If Val(._txtDev.Text) > 0 Then
                                    .upValeIng.Visible = True
                                    .upValeIng.Update()
                                Else
                                    .upValeIng.Visible = False
                                    .upValeIng.Update()
                                End If

                                'Validación de información complenta
                                If banC = 1 Then
                                    .litError.Text = "Información incompleta, favor de ingresar las observaciones correspondientes"
                                    .btnGuardar.Enabled = False
                                Else
                                    .btnGuardar.Enabled = True
                                End If
                            Else
                                .litError.Text = "Información inválida, favor de verificar los Origenes y Destinos"
                            End If
                        Else
                            .litError.Text = "Información incompleta, favor de verificar las facturas seleccionadas"
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
                SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura1.SelectedValue
                ConexionBD.Open()
                valFacturaP = SCMValoresDtFact.ExecuteScalar()
                ConexionBD.Close()
                If valFacturaP <> 0 Then
                    ._txtBan.Text = 1
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 2 Then
                If (.cbFactura2.Checked = True And .lblNoFacturaE2.Visible = True) Or (.cbTabulador2.Checked = True And .lblObsE2.Visible = True) Then
                    banError = 1
                End If
                If .cbFactura2.Checked = True Then
                    SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura2.SelectedValue
                    ConexionBD.Open()
                    valFacturaP = SCMValoresDtFact.ExecuteScalar()
                    ConexionBD.Close()
                    If valFacturaP <> 0 Then
                        ._txtBan.Text = 1
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 3 Then
                If (.cbFactura3.Checked = True And .lblNoFacturaE3.Visible = True) Or (.cbTabulador3.Checked = True And .lblObsE3.Visible = True) Then
                    banError = 1
                End If
                If .cbFactura3.Checked = True Then
                    SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura3.SelectedValue
                    ConexionBD.Open()
                    valFacturaP = SCMValoresDtFact.ExecuteScalar()
                    ConexionBD.Close()
                    If valFacturaP <> 0 Then
                        ._txtBan.Text = 1
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 4 Then
                If (.cbFactura4.Checked = True And .lblNoFacturaE4.Visible = True) Or (.cbTabulador4.Checked = True And .lblObsE4.Visible = True) Then
                    banError = 1
                End If
                If .cbFactura4.Checked = True Then
                    SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura4.SelectedValue
                    ConexionBD.Open()
                    valFacturaP = SCMValoresDtFact.ExecuteScalar()
                    ConexionBD.Close()
                    If valFacturaP <> 0 Then
                        ._txtBan.Text = 1
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 5 Then
                If (.cbFactura5.Checked = True And .lblNoFacturaE5.Visible = True) Or (.cbTabulador5.Checked = True And .lblObsE5.Visible = True) Then
                    banError = 1
                End If
                If .cbFactura5.Checked = True Then
                    SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura5.SelectedValue
                    ConexionBD.Open()
                    valFacturaP = SCMValoresDtFact.ExecuteScalar()
                    ConexionBD.Close()
                    If valFacturaP <> 0 Then
                        ._txtBan.Text = 1
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 6 Then
                If (.cbFactura6.Checked = True And .lblNoFacturaE6.Visible = True) Or (.cbTabulador6.Checked = True And .lblObsE6.Visible = True) Then
                    banError = 1
                End If
                If .cbFactura6.Checked = True Then
                    SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura6.SelectedValue
                    ConexionBD.Open()
                    valFacturaP = SCMValoresDtFact.ExecuteScalar()
                    ConexionBD.Close()
                    If valFacturaP <> 0 Then
                        ._txtBan.Text = 1
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 7 Then
                If (.cbFactura7.Checked = True And .lblNoFacturaE7.Visible = True) Or (.cbTabulador7.Checked = True And .lblObsE7.Visible = True) Then
                    banError = 1
                End If
                If .cbFactura7.Checked = True Then
                    SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura7.SelectedValue
                    ConexionBD.Open()
                    valFacturaP = SCMValoresDtFact.ExecuteScalar()
                    ConexionBD.Close()
                    If valFacturaP <> 0 Then
                        ._txtBan.Text = 1
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 8 Then
                If (.cbFactura8.Checked = True And .lblNoFacturaE8.Visible = True) Or (.cbTabulador8.Checked = True And .lblObsE8.Visible = True) Then
                    banError = 1
                End If
                If .cbFactura8.Checked = True Then
                    SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura8.SelectedValue
                    ConexionBD.Open()
                    valFacturaP = SCMValoresDtFact.ExecuteScalar()
                    ConexionBD.Close()
                    If valFacturaP <> 0 Then
                        ._txtBan.Text = 1
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 9 Then
                If (.cbFactura9.Checked = True And .lblNoFacturaE9.Visible = True) Or (.cbTabulador9.Checked = True And .lblObsE9.Visible = True) Then
                    banError = 1
                End If
                If .cbFactura9.Checked = True Then
                    SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura9.SelectedValue
                    ConexionBD.Open()
                    valFacturaP = SCMValoresDtFact.ExecuteScalar()
                    ConexionBD.Close()
                    If valFacturaP <> 0 Then
                        ._txtBan.Text = 1
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 10 Then
                If (.cbFactura10.Checked = True And .lblNoFacturaE10.Visible = True) Or (.cbTabulador10.Checked = True And .lblObsE10.Visible = True) Then
                    banError = 1
                End If
                If .cbFactura10.Checked = True Then
                    SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura10.SelectedValue
                    ConexionBD.Open()
                    valFacturaP = SCMValoresDtFact.ExecuteScalar()
                    ConexionBD.Close()
                    If valFacturaP <> 0 Then
                        ._txtBan.Text = 1
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 11 Then
                If (.cbFactura11.Checked = True And .lblNoFacturaE11.Visible = True) Or (.cbTabulador11.Checked = True And .lblObsE11.Visible = True) Then
                    banError = 1
                End If
                If .cbFactura11.Checked = True Then
                    SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura11.SelectedValue
                    ConexionBD.Open()
                    valFacturaP = SCMValoresDtFact.ExecuteScalar()
                    ConexionBD.Close()
                    If valFacturaP <> 0 Then
                        ._txtBan.Text = 1
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 12 Then
                If (.cbFactura12.Checked = True And .lblNoFacturaE12.Visible = True) Or (.cbTabulador12.Checked = True And .lblObsE12.Visible = True) Then
                    banError = 1
                End If
                If .cbFactura12.Checked = True Then
                    SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura12.SelectedValue
                    ConexionBD.Open()
                    valFacturaP = SCMValoresDtFact.ExecuteScalar()
                    ConexionBD.Close()
                    If valFacturaP <> 0 Then
                        ._txtBan.Text = 1
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 13 Then
                If (.cbFactura13.Checked = True And .lblNoFacturaE13.Visible = True) Or (.cbTabulador13.Checked = True And .lblObsE13.Visible = True) Then
                    banError = 1
                End If
                If .cbFactura13.Checked = True Then
                    SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura13.SelectedValue
                    ConexionBD.Open()
                    valFacturaP = SCMValoresDtFact.ExecuteScalar()
                    ConexionBD.Close()
                    If valFacturaP <> 0 Then
                        ._txtBan.Text = 1
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 14 Then
                If (.cbFactura14.Checked = True And .lblNoFacturaE14.Visible = True) Or (.cbTabulador14.Checked = True And .lblObsE14.Visible = True) Then
                    banError = 1
                End If
                If .cbFactura14.Checked = True Then
                    SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura14.SelectedValue
                    ConexionBD.Open()
                    valFacturaP = SCMValoresDtFact.ExecuteScalar()
                    ConexionBD.Close()
                    If valFacturaP <> 0 Then
                        ._txtBan.Text = 1
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 15 Then
                If (.cbFactura15.Checked = True And .lblNoFacturaE15.Visible = True) Or (.cbTabulador15.Checked = True And .lblObsE15.Visible = True) Then
                    banError = 1
                End If
                If .cbFactura15.Checked = True Then
                    SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura15.SelectedValue
                    ConexionBD.Open()
                    valFacturaP = SCMValoresDtFact.ExecuteScalar()
                    ConexionBD.Close()
                    If valFacturaP <> 0 Then
                        ._txtBan.Text = 1
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 16 Then
                If (.cbFactura16.Checked = True And .lblNoFacturaE16.Visible = True) Or (.cbTabulador16.Checked = True And .lblObsE16.Visible = True) Then
                    banError = 1
                End If
                If .cbFactura16.Checked = True Then
                    SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura16.SelectedValue
                    ConexionBD.Open()
                    valFacturaP = SCMValoresDtFact.ExecuteScalar()
                    ConexionBD.Close()
                    If valFacturaP <> 0 Then
                        ._txtBan.Text = 1
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 17 Then
                If (.cbFactura17.Checked = True And .lblNoFacturaE17.Visible = True) Or (.cbTabulador17.Checked = True And .lblObsE17.Visible = True) Then
                    banError = 1
                End If
                If .cbFactura17.Checked = True Then
                    SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura17.SelectedValue
                    ConexionBD.Open()
                    valFacturaP = SCMValoresDtFact.ExecuteScalar()
                    ConexionBD.Close()
                    If valFacturaP <> 0 Then
                        ._txtBan.Text = 1
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 18 Then
                If (.cbFactura18.Checked = True And .lblNoFacturaE18.Visible = True) Or (.cbTabulador18.Checked = True And .lblObsE18.Visible = True) Then
                    banError = 1
                End If
                If .cbFactura18.Checked = True Then
                    SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura18.SelectedValue
                    ConexionBD.Open()
                    valFacturaP = SCMValoresDtFact.ExecuteScalar()
                    ConexionBD.Close()
                    If valFacturaP <> 0 Then
                        ._txtBan.Text = 1
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 19 Then
                If (.cbFactura19.Checked = True And .lblNoFacturaE19.Visible = True) Or (.cbTabulador19.Checked = True And .lblObsE19.Visible = True) Then
                    banError = 1
                End If
                If .cbFactura19.Checked = True Then
                    SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura19.SelectedValue
                    ConexionBD.Open()
                    valFacturaP = SCMValoresDtFact.ExecuteScalar()
                    ConexionBD.Close()
                    If valFacturaP <> 0 Then
                        ._txtBan.Text = 1
                    End If
                End If
            End If
            If .ddlNoConceptos.SelectedValue >= 20 Then
                If (.cbFactura20.Checked = True And .lblNoFacturaE20.Visible = True) Or (.cbTabulador20.Checked = True And .lblObsE20.Visible = True) Then
                    banError = 1
                End If
                If .cbFactura20.Checked = True Then
                    SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura20.SelectedValue
                    ConexionBD.Open()
                    valFacturaP = SCMValoresDtFact.ExecuteScalar()
                    ConexionBD.Close()
                    If valFacturaP <> 0 Then
                        ._txtBan.Text = 1
                    End If
                End If
            End If

            If banError = 1 Then
                Return True
            Else
                Return False
            End If
        End With
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
                .upDiv.Update()
                .ddlCC.Items.Clear()
                .upCC.Update()
                tipoDivCC()

                'Actualizar Conceptos
                actConceptos()

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
                .upDiv.Update()
                .ddlCC.Items.Clear()
                .upCC.Update()
                tipoDivCC()
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
    End Sub

    Protected Sub ddlNoConceptos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoConceptos.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                mostrarOcultarCampos()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
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
            tipoFT(tipo, .cbFactura1, .upFactura1, .cbTabulador1, .upTabulador1, .ddlConcepto1, .upConcepto1, .wneNoPers1.Value, .upNoPers1, .wneNoDias1.Value, .upNoDias1, .txtRFC1, .ibtnRFCBus1, .upRFC1, .ddlNoFactura1, .upNoFactura1, .hlProveedor1, .upProveedor1, .wceSubtotal1, .upSubtotal1, .wceIVA1, .upIVA1, .wceTotal1, .upTotal1, .wpePorcentAut1, .upPorcentAut1)
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
            tipoFT(tipo, .cbFactura1, .upFactura1, .cbTabulador1, .upTabulador1, .ddlConcepto1, .upConcepto1, .wneNoPers1.Value, .upNoPers1, .wneNoDias1.Value, .upNoDias1, .txtRFC1, .ibtnRFCBus1, .upRFC1, .ddlNoFactura1, .upNoFactura1, .hlProveedor1, .upProveedor1, .wceSubtotal1, .upSubtotal1, .wceIVA1, .upIVA1, .wceTotal1, .upTotal1, .wpePorcentAut1, .upPorcentAut1)
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
                    ._txtVal_Origen_Destino1.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    actNoFactura(Me.ddlConcepto1.SelectedValue, Me.wneNoPers1.Value, Me.upNoPers1, Me.wneNoDias1.Value, Me.upNoDias1, Me.txtRFC1, Me.ddlNoFactura1, Me.upNoFactura1, Me.hlProveedor1, Me.upProveedor1, Me.wceSubtotal1, Me.upSubtotal1, Me.wceIVA1, Me.upIVA1, Me.wceTotal1, Me.upTotal1, Me.wpePorcentAut1, Me.upPorcentAut1)
                Else
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto where id_concepto = @id_concepto "
                    SCMValores.Parameters.AddWithValue("@id_concepto", ddlConcepto1.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino1.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If
            Catch ex As Exception
                .litError.Text = ex.Message.ToString()
            End Try

            'If Me.cbFactura1.Checked = True Then

            ''    actFactura(Me.ddlConcepto1.SelectedValue, Me.wneNoPers1.Value, Me.ddlNoFactura1.SelectedValue, Me.hlProveedor1, Me.upProveedor1, Me.wceSubtotal1, Me.upSubtotal1, Me.wceIVA1, Me.upIVA1, Me.wceTotal1, Me.upTotal1)
            '    actNoFactura(Me.ddlConcepto1.SelectedValue, Me.wneNoPers1.Value, Me.upNoPers1, Me.wneNoDias1.Value, Me.upNoDias1, Me.txtRFC1, Me.ddlNoFactura1, Me.upNoFactura1, Me.hlProveedor1, Me.upProveedor1, Me.wceSubtotal1, Me.upSubtotal1, Me.wceIVA1, Me.upIVA1, Me.wceTotal1, Me.upTotal1, Me.wpePorcentAut1, Me.upPorcentAut1)
            'End If
        End With
    End Sub

    Protected Sub ibtnRFCBus1_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnRFCBus1.Click
        actNoFactura(Me.ddlConcepto1.SelectedValue, Me.wneNoPers1.Value, Me.upNoPers1, Me.wneNoDias1.Value, Me.upNoDias1, Me.txtRFC1, Me.ddlNoFactura1, Me.upNoFactura1, Me.hlProveedor1, Me.upProveedor1, Me.wceSubtotal1, Me.upSubtotal1, Me.wceIVA1, Me.upIVA1, Me.wceTotal1, Me.upTotal1, Me.wpePorcentAut1, Me.upPorcentAut1)
    End Sub

    Protected Sub ddlNoFactura1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoFactura1.SelectedIndexChanged
        actFactura(Me.ddlConcepto1.SelectedValue, Me.wneNoPers1.Value, Me.upNoPers1, Me.wneNoDias1.Value, Me.upNoDias1, Me.ddlNoFactura1.SelectedValue, Me.hlProveedor1, Me.upProveedor1, Me.wceSubtotal1, Me.upSubtotal1, Me.wceIVA1, Me.upIVA1, Me.wceTotal1, Me.upTotal1, Me.wpePorcentAut1, Me.upPorcentAut1)
    End Sub

#End Region

#Region "No. 2"

    Protected Sub cbFactura2_CheckedChanged(sender As Object, e As EventArgs) Handles cbFactura2.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbFactura2.Checked = True Then
                tipo = "F"
                .lblObsE2.Visible = False
            Else
                tipo = "T"
                .lblNoFacturaE2.Visible = False
                If .cbTabulador2.Checked = True And .txtObs2.Text.Trim = "" Then
                    .lblObsE2.Visible = True
                Else
                    .lblObsE2.Visible = False
                End If
            End If
            .upObsE2.Update()
            tipoFT(tipo, .cbFactura2, .upFactura2, .cbTabulador2, .upTabulador2, .ddlConcepto2, .upConcepto2, .wneNoPers2.Value, .upNoPers2, .wneNoDias2.Value, .upNoDias2, .txtRFC2, .ibtnRFCBus2, .upRFC2, .ddlNoFactura2, .upNoFactura2, .hlProveedor2, .upProveedor2, .wceSubtotal2, .upSubtotal2, .wceIVA2, .upIVA2, .wceTotal2, .upTotal2, .wpePorcentAut2, .upPorcentAut2)
        End With
    End Sub

    Protected Sub cbTabulador2_CheckedChanged(sender As Object, e As EventArgs) Handles cbTabulador2.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbTabulador2.Checked = True Then
                tipo = "T"
                .lblNoFacturaE2.Visible = False
                If .cbTabulador2.Checked = True And .txtObs2.Text.Trim = "" Then
                    .lblObsE2.Visible = True
                Else
                    .lblObsE2.Visible = False
                End If
            Else
                tipo = "F"
                .lblObsE2.Visible = False
            End If
            .upObsE2.Update()
            tipoFT(tipo, .cbFactura2, .upFactura2, .cbTabulador2, .upTabulador2, .ddlConcepto2, .upConcepto2, .wneNoPers2.Value, .upNoPers2, .wneNoDias2.Value, .upNoDias2, .txtRFC2, .ibtnRFCBus2, .upRFC2, .ddlNoFactura2, .upNoFactura2, .hlProveedor2, .upProveedor2, .wceSubtotal2, .upSubtotal2, .wceIVA2, .upIVA2, .wceTotal2, .upTotal2, .wpePorcentAut2, .upPorcentAut2)
        End With
    End Sub

    Protected Sub ddlConcepto2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConcepto2.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                If Me.cbFactura2.Checked = True Then
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto_comp where id_concepto_comp = @id_concepto_comp "
                    SCMValores.Parameters.AddWithValue("@id_concepto_comp", ddlConcepto2.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino2.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    actNoFactura(Me.ddlConcepto2.SelectedValue, Me.wneNoPers2.Value, Me.upNoPers2, Me.wneNoDias2.Value, Me.upNoDias2, Me.txtRFC2, Me.ddlNoFactura2, Me.upNoFactura2, Me.hlProveedor2, Me.upProveedor2, Me.wceSubtotal2, Me.upSubtotal2, Me.wceIVA2, Me.upIVA2, Me.wceTotal2, Me.upTotal2, Me.wpePorcentAut2, Me.upPorcentAut2)
                Else
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto where id_concepto = @id_concepto "
                    SCMValores.Parameters.AddWithValue("@id_concepto", ddlConcepto2.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino2.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If
            Catch ex As Exception
                .litError.Text = ex.Message.ToString()
            End Try
        End With
        'If Me.cbFactura2.Checked = True Then
        '    'actFactura(Me.ddlConcepto2.SelectedValue, Me.wneNoPers2.Value, Me.ddlNoFactura2.SelectedValue, Me.hlProveedor2, Me.upProveedor2, Me.wceSubtotal2, Me.upSubtotal2, Me.wceIVA2, Me.upIVA2, Me.wceTotal2, Me.upTotal2)
        '    actNoFactura(Me.ddlConcepto2.SelectedValue, Me.wneNoPers2.Value, Me.upNoPers2, Me.wneNoDias2.Value, Me.upNoDias2, Me.txtRFC2, Me.ddlNoFactura2, Me.upNoFactura2, Me.hlProveedor2, Me.upProveedor2, Me.wceSubtotal2, Me.upSubtotal2, Me.wceIVA2, Me.upIVA2, Me.wceTotal2, Me.upTotal2, Me.wpePorcentAut2, Me.upPorcentAut2)
        'End If
    End Sub

    Protected Sub ibtnRFCBus2_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnRFCBus2.Click
        actNoFactura(Me.ddlConcepto2.SelectedValue, Me.wneNoPers2.Value, Me.upNoPers2, Me.wneNoDias2.Value, Me.upNoDias2, Me.txtRFC2, Me.ddlNoFactura2, Me.upNoFactura2, Me.hlProveedor2, Me.upProveedor2, Me.wceSubtotal2, Me.upSubtotal2, Me.wceIVA2, Me.upIVA2, Me.wceTotal2, Me.upTotal2, Me.wpePorcentAut2, Me.upPorcentAut2)
    End Sub

    Protected Sub ddlNoFactura2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoFactura2.SelectedIndexChanged
        actFactura(Me.ddlConcepto2.SelectedValue, Me.wneNoPers2.Value, Me.upNoPers2, Me.wneNoDias2.Value, Me.upNoDias2, Me.ddlNoFactura2.SelectedValue, Me.hlProveedor2, Me.upProveedor2, Me.wceSubtotal2, Me.upSubtotal2, Me.wceIVA2, Me.upIVA2, Me.wceTotal2, Me.upTotal2, Me.wpePorcentAut2, Me.upPorcentAut2)
    End Sub

#End Region

#Region "No. 3"

    Protected Sub cbFactura3_CheckedChanged(sender As Object, e As EventArgs) Handles cbFactura3.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbFactura3.Checked = True Then
                tipo = "F"
                .lblObsE3.Visible = False
            Else
                tipo = "T"
                .lblNoFacturaE3.Visible = False
                If .cbTabulador3.Checked = True And .txtObs3.Text.Trim = "" Then
                    .lblObsE3.Visible = True
                Else
                    .lblObsE3.Visible = False
                End If
            End If
            .upObsE3.Update()
            tipoFT(tipo, .cbFactura3, .upFactura3, .cbTabulador3, .upTabulador3, .ddlConcepto3, .upConcepto3, .wneNoPers3.Value, .upNoPers3, .wneNoDias3.Value, .upNoDias3, .txtRFC3, .ibtnRFCBus3, .upRFC3, .ddlNoFactura3, .upNoFactura3, .hlProveedor3, .upProveedor3, .wceSubtotal3, .upSubtotal3, .wceIVA3, .upIVA3, .wceTotal3, .upTotal3, .wpePorcentAut3, .upPorcentAut3)
        End With
    End Sub

    Protected Sub cbTabulador3_CheckedChanged(sender As Object, e As EventArgs) Handles cbTabulador3.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbTabulador3.Checked = True Then
                tipo = "T"
                .lblNoFacturaE3.Visible = False
                If .cbTabulador3.Checked = True And .txtObs3.Text.Trim = "" Then
                    .lblObsE3.Visible = True
                Else
                    .lblObsE3.Visible = False
                End If
            Else
                tipo = "F"
                .lblObsE3.Visible = False
            End If
            .upObsE3.Update()
            tipoFT(tipo, .cbFactura3, .upFactura3, .cbTabulador3, .upTabulador3, .ddlConcepto3, .upConcepto3, .wneNoPers3.Value, .upNoPers3, .wneNoDias3.Value, .upNoDias3, .txtRFC3, .ibtnRFCBus3, .upRFC3, .ddlNoFactura3, .upNoFactura3, .hlProveedor3, .upProveedor3, .wceSubtotal3, .upSubtotal3, .wceIVA3, .upIVA3, .wceTotal3, .upTotal3, .wpePorcentAut3, .upPorcentAut3)
        End With
    End Sub

    Protected Sub ddlConcepto3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConcepto3.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                If Me.cbFactura3.Checked = True Then
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto_comp where id_concepto_comp = @id_concepto_comp "
                    SCMValores.Parameters.AddWithValue("@id_concepto_comp", ddlConcepto3.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino3.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    actNoFactura(Me.ddlConcepto3.SelectedValue, Me.wneNoPers3.Value, Me.upNoPers3, Me.wneNoDias3.Value, Me.upNoDias3, Me.txtRFC3, Me.ddlNoFactura3, Me.upNoFactura3, Me.hlProveedor3, Me.upProveedor3, Me.wceSubtotal3, Me.upSubtotal3, Me.wceIVA3, Me.upIVA3, Me.wceTotal3, Me.upTotal3, Me.wpePorcentAut3, Me.upPorcentAut3)
                Else
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto where id_concepto = @id_concepto "
                    SCMValores.Parameters.AddWithValue("@id_concepto", ddlConcepto3.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino3.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If
            Catch ex As Exception
                .litError.Text = ex.Message.ToString()
            End Try
        End With
        'If Me.cbFactura3.Checked = True Then
        '    'actFactura(Me.ddlConcepto3.SelectedValue, Me.wneNoPers3.Value, Me.ddlNoFactura3.SelectedValue, Me.hlProveedor3, Me.upProveedor3, Me.wceSubtotal3, Me.upSubtotal3, Me.wceIVA3, Me.upIVA3, Me.wceTotal3, Me.upTotal3)
        '    actNoFactura(Me.ddlConcepto3.SelectedValue, Me.wneNoPers3.Value, Me.upNoPers3, Me.wneNoDias3.Value, Me.upNoDias3, Me.txtRFC3, Me.ddlNoFactura3, Me.upNoFactura3, Me.hlProveedor3, Me.upProveedor3, Me.wceSubtotal3, Me.upSubtotal3, Me.wceIVA3, Me.upIVA3, Me.wceTotal3, Me.upTotal3, Me.wpePorcentAut3, Me.upPorcentAut3)
        'End If
    End Sub

    Protected Sub ibtnRFCBus3_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnRFCBus3.Click
        actNoFactura(Me.ddlConcepto3.SelectedValue, Me.wneNoPers3.Value, Me.upNoPers3, Me.wneNoDias3.Value, Me.upNoDias3, Me.txtRFC3, Me.ddlNoFactura3, Me.upNoFactura3, Me.hlProveedor3, Me.upProveedor3, Me.wceSubtotal3, Me.upSubtotal3, Me.wceIVA3, Me.upIVA3, Me.wceTotal3, Me.upTotal3, Me.wpePorcentAut3, Me.upPorcentAut3)
    End Sub

    Protected Sub ddlNoFactura3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoFactura3.SelectedIndexChanged
        actFactura(Me.ddlConcepto3.SelectedValue, Me.wneNoPers3.Value, Me.upNoPers3, Me.wneNoDias3.Value, Me.upNoDias3, Me.ddlNoFactura3.SelectedValue, Me.hlProveedor3, Me.upProveedor3, Me.wceSubtotal3, Me.upSubtotal3, Me.wceIVA3, Me.upIVA3, Me.wceTotal3, Me.upTotal3, Me.wpePorcentAut3, Me.upPorcentAut3)
    End Sub

#End Region

#Region "No. 4"

    Protected Sub cbFactura4_CheckedChanged(sender As Object, e As EventArgs) Handles cbFactura4.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbFactura4.Checked = True Then
                tipo = "F"
                .lblObsE4.Visible = False
            Else
                tipo = "T"
                .lblNoFacturaE4.Visible = False
                If .cbTabulador4.Checked = True And .txtObs4.Text.Trim = "" Then
                    .lblObsE4.Visible = True
                Else
                    .lblObsE4.Visible = False
                End If
            End If
            .upObsE4.Update()
            tipoFT(tipo, .cbFactura4, .upFactura4, .cbTabulador4, .upTabulador4, .ddlConcepto4, .upConcepto4, .wneNoPers4.Value, .upNoPers4, .wneNoDias4.Value, .upNoDias4, .txtRFC4, .ibtnRFCBus4, .upRFC4, .ddlNoFactura4, .upNoFactura4, .hlProveedor4, .upProveedor4, .wceSubtotal4, .upSubtotal4, .wceIVA4, .upIVA4, .wceTotal4, .upTotal4, .wpePorcentAut4, .upPorcentAut4)
        End With
    End Sub

    Protected Sub cbTabulador4_CheckedChanged(sender As Object, e As EventArgs) Handles cbTabulador4.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbTabulador4.Checked = True Then
                tipo = "T"
                .lblNoFacturaE4.Visible = False
                If .cbTabulador4.Checked = True And .txtObs4.Text.Trim = "" Then
                    .lblObsE4.Visible = True
                Else
                    .lblObsE4.Visible = False
                End If
            Else
                tipo = "F"
                .lblObsE4.Visible = False
            End If
            .upObsE4.Update()
            tipoFT(tipo, .cbFactura4, .upFactura4, .cbTabulador4, .upTabulador4, .ddlConcepto4, .upConcepto4, .wneNoPers4.Value, .upNoPers4, .wneNoDias4.Value, .upNoDias4, .txtRFC4, .ibtnRFCBus4, .upRFC4, .ddlNoFactura4, .upNoFactura4, .hlProveedor4, .upProveedor4, .wceSubtotal4, .upSubtotal4, .wceIVA4, .upIVA4, .wceTotal4, .upTotal4, .wpePorcentAut4, .upPorcentAut4)
        End With
    End Sub

    Protected Sub ddlConcepto4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConcepto4.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                If Me.cbFactura4.Checked = True Then
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto_comp where id_concepto_comp = @id_concepto_comp "
                    SCMValores.Parameters.AddWithValue("@id_concepto_comp", ddlConcepto4.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino4.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    actNoFactura(Me.ddlConcepto4.SelectedValue, Me.wneNoPers4.Value, Me.upNoPers4, Me.wneNoDias4.Value, Me.upNoDias4, Me.txtRFC4, Me.ddlNoFactura4, Me.upNoFactura4, Me.hlProveedor4, Me.upProveedor4, Me.wceSubtotal4, Me.upSubtotal4, Me.wceIVA4, Me.upIVA4, Me.wceTotal4, Me.upTotal4, Me.wpePorcentAut4, Me.upPorcentAut4)
                Else
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto where id_concepto = @id_concepto "
                    SCMValores.Parameters.AddWithValue("@id_concepto", ddlConcepto4.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino4.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If
            Catch ex As Exception
                .litError.Text = ex.Message.ToString()
            End Try
        End With

        'If Me.cbFactura4.Checked = True Then
        '    'actFactura(Me.ddlConcepto4.SelectedValue, Me.wneNoPers4.Value, Me.ddlNoFactura4.SelectedValue, Me.hlProveedor4, Me.upProveedor4, Me.wceSubtotal4, Me.upSubtotal4, Me.wceIVA4, Me.upIVA4, Me.wceTotal4, Me.upTotal4)
        '    actNoFactura(Me.ddlConcepto4.SelectedValue, Me.wneNoPers4.Value, Me.upNoPers4, Me.wneNoDias4.Value, Me.upNoDias4, Me.txtRFC4, Me.ddlNoFactura4, Me.upNoFactura4, Me.hlProveedor4, Me.upProveedor4, Me.wceSubtotal4, Me.upSubtotal4, Me.wceIVA4, Me.upIVA4, Me.wceTotal4, Me.upTotal4, Me.wpePorcentAut4, Me.upPorcentAut4)
        'End If
    End Sub

    Protected Sub ibtnRFCBus4_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnRFCBus4.Click
        actNoFactura(Me.ddlConcepto4.SelectedValue, Me.wneNoPers4.Value, Me.upNoPers4, Me.wneNoDias4.Value, Me.upNoDias4, Me.txtRFC4, Me.ddlNoFactura4, Me.upNoFactura4, Me.hlProveedor4, Me.upProveedor4, Me.wceSubtotal4, Me.upSubtotal4, Me.wceIVA4, Me.upIVA4, Me.wceTotal4, Me.upTotal4, Me.wpePorcentAut4, Me.upPorcentAut4)
    End Sub

    Protected Sub ddlNoFactura4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoFactura4.SelectedIndexChanged
        actFactura(Me.ddlConcepto4.SelectedValue, Me.wneNoPers4.Value, Me.upNoPers4, Me.wneNoDias4.Value, Me.upNoDias4, Me.ddlNoFactura4.SelectedValue, Me.hlProveedor4, Me.upProveedor4, Me.wceSubtotal4, Me.upSubtotal4, Me.wceIVA4, Me.upIVA4, Me.wceTotal4, Me.upTotal4, Me.wpePorcentAut4, Me.upPorcentAut4)
    End Sub

#End Region

#Region "No. 5"

    Protected Sub cbFactura5_CheckedChanged(sender As Object, e As EventArgs) Handles cbFactura5.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbFactura5.Checked = True Then
                tipo = "F"
                .lblObsE5.Visible = False
            Else
                tipo = "T"
                .lblNoFacturaE5.Visible = False
                If .cbTabulador5.Checked = True And .txtObs5.Text.Trim = "" Then
                    .lblObsE5.Visible = True
                Else
                    .lblObsE5.Visible = False
                End If
            End If
            .upObsE5.Update()
            tipoFT(tipo, .cbFactura5, .upFactura5, .cbTabulador5, .upTabulador5, .ddlConcepto5, .upConcepto5, .wneNoPers5.Value, .upNoPers5, .wneNoDias5.Value, .upNoDias5, .txtRFC5, .ibtnRFCBus5, .upRFC5, .ddlNoFactura5, .upNoFactura5, .hlProveedor5, .upProveedor5, .wceSubtotal5, .upSubtotal5, .wceIVA5, .upIVA5, .wceTotal5, .upTotal5, .wpePorcentAut5, .upPorcentAut5)
        End With
    End Sub

    Protected Sub cbTabulador5_CheckedChanged(sender As Object, e As EventArgs) Handles cbTabulador5.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbTabulador5.Checked = True Then
                tipo = "T"
                .lblNoFacturaE5.Visible = False
                If .cbTabulador5.Checked = True And .txtObs5.Text.Trim = "" Then
                    .lblObsE5.Visible = True
                Else
                    .lblObsE5.Visible = False
                End If
            Else
                tipo = "F"
                .lblObsE5.Visible = False
            End If
            .upObsE5.Update()
            tipoFT(tipo, .cbFactura5, .upFactura5, .cbTabulador5, .upTabulador5, .ddlConcepto5, .upConcepto5, .wneNoPers5.Value, .upNoPers5, .wneNoDias5.Value, .upNoDias5, .txtRFC5, .ibtnRFCBus5, .upRFC5, .ddlNoFactura5, .upNoFactura5, .hlProveedor5, .upProveedor5, .wceSubtotal5, .upSubtotal5, .wceIVA5, .upIVA5, .wceTotal5, .upTotal5, .wpePorcentAut5, .upPorcentAut5)
        End With
    End Sub

    Protected Sub ddlConcepto5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConcepto5.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                If Me.cbFactura5.Checked = True Then
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto_comp where id_concepto_comp = @id_concepto_comp "
                    SCMValores.Parameters.AddWithValue("@id_concepto_comp", ddlConcepto5.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino5.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    actNoFactura(Me.ddlConcepto5.SelectedValue, Me.wneNoPers5.Value, Me.upNoPers5, Me.wneNoDias5.Value, Me.upNoDias5, Me.txtRFC5, Me.ddlNoFactura5, Me.upNoFactura5, Me.hlProveedor5, Me.upProveedor5, Me.wceSubtotal5, Me.upSubtotal5, Me.wceIVA5, Me.upIVA5, Me.wceTotal5, Me.upTotal5, Me.wpePorcentAut5, Me.upPorcentAut5)
                Else
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto where id_concepto = @id_concepto "
                    SCMValores.Parameters.AddWithValue("@id_concepto", ddlConcepto5.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino5.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If
            Catch ex As Exception
                .litError.Text = ex.Message.ToString()
            End Try
        End With
        'If Me.cbFactura5.Checked = True Then
        '    'actFactura(Me.ddlConcepto5.SelectedValue, Me.wneNoPers5.Value, Me.ddlNoFactura5.SelectedValue, Me.hlProveedor5, Me.upProveedor5, Me.wceSubtotal5, Me.upSubtotal5, Me.wceIVA5, Me.upIVA5, Me.wceTotal5, Me.upTotal5)
        '    actNoFactura(Me.ddlConcepto5.SelectedValue, Me.wneNoPers5.Value, Me.upNoPers5, Me.wneNoDias5.Value, Me.upNoDias5, Me.txtRFC5, Me.ddlNoFactura5, Me.upNoFactura5, Me.hlProveedor5, Me.upProveedor5, Me.wceSubtotal5, Me.upSubtotal5, Me.wceIVA5, Me.upIVA5, Me.wceTotal5, Me.upTotal5, Me.wpePorcentAut5, Me.upPorcentAut5)
        'End If
    End Sub

    Protected Sub ibtnRFCBus5_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnRFCBus5.Click
        actNoFactura(Me.ddlConcepto5.SelectedValue, Me.wneNoPers5.Value, Me.upNoPers5, Me.wneNoDias5.Value, Me.txtRFC5, Me.upNoDias5, Me.ddlNoFactura5, Me.upNoFactura5, Me.hlProveedor5, Me.upProveedor5, Me.wceSubtotal5, Me.upSubtotal5, Me.wceIVA5, Me.upIVA5, Me.wceTotal5, Me.upTotal5, Me.wpePorcentAut5, Me.upPorcentAut5)
    End Sub

    Protected Sub ddlNoFactura5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoFactura5.SelectedIndexChanged
        actFactura(Me.ddlConcepto5.SelectedValue, Me.wneNoPers5.Value, Me.upNoPers5, Me.wneNoDias5.Value, Me.upNoDias5, Me.ddlNoFactura5.SelectedValue, Me.hlProveedor5, Me.upProveedor5, Me.wceSubtotal5, Me.upSubtotal5, Me.wceIVA5, Me.upIVA5, Me.wceTotal5, Me.upTotal5, Me.wpePorcentAut5, Me.upPorcentAut5)
    End Sub

#End Region

#Region "No. 6"

    Protected Sub cbFactura6_CheckedChanged(sender As Object, e As EventArgs) Handles cbFactura6.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbFactura6.Checked = True Then
                tipo = "F"
                .lblObsE6.Visible = False
            Else
                tipo = "T"
                .lblNoFacturaE6.Visible = False
                If .cbTabulador6.Checked = True And .txtObs6.Text.Trim = "" Then
                    .lblObsE6.Visible = True
                Else
                    .lblObsE6.Visible = False
                End If
            End If
            .upObsE6.Update()
            tipoFT(tipo, .cbFactura6, .upFactura6, .cbTabulador6, .upTabulador6, .ddlConcepto6, .upConcepto6, .wneNoPers6.Value, .upNoPers6, .wneNoDias6.Value, .upNoDias6, .txtRFC6, .ibtnRFCBus6, .upRFC6, .ddlNoFactura6, .upNoFactura6, .hlProveedor6, .upProveedor6, .wceSubtotal6, .upSubtotal6, .wceIVA6, .upIVA6, .wceTotal6, .upTotal6, .wpePorcentAut6, .upPorcentAut6)
        End With
    End Sub

    Protected Sub cbTabulador6_CheckedChanged(sender As Object, e As EventArgs) Handles cbTabulador6.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbTabulador6.Checked = True Then
                tipo = "T"
                .lblNoFacturaE6.Visible = False
                If .cbTabulador6.Checked = True And .txtObs6.Text.Trim = "" Then
                    .lblObsE6.Visible = True
                Else
                    .lblObsE6.Visible = False
                End If
            Else
                tipo = "F"
                .lblObsE6.Visible = False
            End If
            .upObsE6.Update()
            tipoFT(tipo, .cbFactura6, .upFactura6, .cbTabulador6, .upTabulador6, .ddlConcepto6, .upConcepto6, .wneNoPers6.Value, .upNoPers6, .wneNoDias6.Value, .upNoDias6, .txtRFC6, .ibtnRFCBus6, .upRFC6, .ddlNoFactura6, .upNoFactura6, .hlProveedor6, .upProveedor6, .wceSubtotal6, .upSubtotal6, .wceIVA6, .upIVA6, .wceTotal6, .upTotal6, .wpePorcentAut6, .upPorcentAut6)
        End With
    End Sub

    Protected Sub ddlConcepto6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConcepto6.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                If Me.cbFactura6.Checked = True Then
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto_comp where id_concepto_comp = @id_concepto_comp "
                    SCMValores.Parameters.AddWithValue("@id_concepto_comp", ddlConcepto6.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino6.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    actNoFactura(Me.ddlConcepto6.SelectedValue, Me.wneNoPers6.Value, Me.upNoPers6, Me.wneNoDias6.Value, Me.upNoDias6, Me.txtRFC6, Me.ddlNoFactura6, Me.upNoFactura6, Me.hlProveedor6, Me.upProveedor6, Me.wceSubtotal6, Me.upSubtotal6, Me.wceIVA6, Me.upIVA6, Me.wceTotal6, Me.upTotal6, Me.wpePorcentAut6, Me.upPorcentAut6)
                Else
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto where id_concepto = @id_concepto "
                    SCMValores.Parameters.AddWithValue("@id_concepto", ddlConcepto6.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino6.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If
            Catch ex As Exception
                .litError.Text = ex.Message.ToString()
            End Try
        End With
        'If Me.cbFactura6.Checked = True Then
        '    'actFactura(Me.ddlConcepto6.SelectedValue, Me.wneNoPers6.Value, Me.ddlNoFactura6.SelectedValue, Me.hlProveedor6, Me.upProveedor6, Me.wceSubtotal6, Me.upSubtotal6, Me.wceIVA6, Me.upIVA6, Me.wceTotal6, Me.upTotal6)
        '    actNoFactura(Me.ddlConcepto6.SelectedValue, Me.wneNoPers6.Value, Me.upNoPers6, Me.wneNoDias6.Value, Me.upNoDias6, Me.txtRFC6, Me.ddlNoFactura6, Me.upNoFactura6, Me.hlProveedor6, Me.upProveedor6, Me.wceSubtotal6, Me.upSubtotal6, Me.wceIVA6, Me.upIVA6, Me.wceTotal6, Me.upTotal6, Me.wpePorcentAut6, Me.upPorcentAut6)
        'End If
    End Sub

    Protected Sub ibtnRFCBus6_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnRFCBus6.Click
        actNoFactura(Me.ddlConcepto6.SelectedValue, Me.wneNoPers6.Value, Me.upNoPers6, Me.wneNoDias6.Value, Me.upNoDias6, Me.txtRFC6, Me.ddlNoFactura6, Me.upNoFactura6, Me.hlProveedor6, Me.upProveedor6, Me.wceSubtotal6, Me.upSubtotal6, Me.wceIVA6, Me.upIVA6, Me.wceTotal6, Me.upTotal6, Me.wpePorcentAut6, Me.upPorcentAut6)
    End Sub

    Protected Sub ddlNoFactura6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoFactura6.SelectedIndexChanged
        actFactura(Me.ddlConcepto6.SelectedValue, Me.wneNoPers6.Value, Me.upNoPers6, Me.wneNoDias6.Value, Me.upNoDias6, Me.ddlNoFactura6.SelectedValue, Me.hlProveedor6, Me.upProveedor6, Me.wceSubtotal6, Me.upSubtotal6, Me.wceIVA6, Me.upIVA6, Me.wceTotal6, Me.upTotal6, Me.wpePorcentAut6, Me.upPorcentAut6)
    End Sub

#End Region

#Region "No. 7"

    Protected Sub cbFactura7_CheckedChanged(sender As Object, e As EventArgs) Handles cbFactura7.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbFactura7.Checked = True Then
                tipo = "F"
                .lblObsE7.Visible = False
            Else
                tipo = "T"
                .lblNoFacturaE7.Visible = False
                If .cbTabulador7.Checked = True And .txtObs7.Text.Trim = "" Then
                    .lblObsE7.Visible = True
                Else
                    .lblObsE7.Visible = False
                End If
            End If
            .upObsE7.Update()
            tipoFT(tipo, .cbFactura7, .upFactura7, .cbTabulador7, .upTabulador7, .ddlConcepto7, .upConcepto7, .wneNoPers7.Value, .upNoPers7, .wneNoDias7.Value, .upNoDias7, .txtRFC7, .ibtnRFCBus7, .upRFC7, .ddlNoFactura7, .upNoFactura7, .hlProveedor7, .upProveedor7, .wceSubtotal7, .upSubtotal7, .wceIVA7, .upIVA7, .wceTotal7, .upTotal7, .wpePorcentAut7, .upPorcentAut7)
        End With
    End Sub

    Protected Sub cbTabulador7_CheckedChanged(sender As Object, e As EventArgs) Handles cbTabulador7.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbTabulador7.Checked = True Then
                tipo = "T"
                .lblNoFacturaE7.Visible = False
                If .cbTabulador7.Checked = True And .txtObs7.Text.Trim = "" Then
                    .lblObsE7.Visible = True
                Else
                    .lblObsE7.Visible = False
                End If
            Else
                tipo = "F"
                .lblObsE7.Visible = False
            End If
            .upObsE7.Update()
            tipoFT(tipo, .cbFactura7, .upFactura7, .cbTabulador7, .upTabulador7, .ddlConcepto7, .upConcepto7, .wneNoPers7.Value, .upNoPers7, .wneNoDias7.Value, .upNoDias7, .txtRFC7, .ibtnRFCBus7, .upRFC7, .ddlNoFactura7, .upNoFactura7, .hlProveedor7, .upProveedor7, .wceSubtotal7, .upSubtotal7, .wceIVA7, .upIVA7, .wceTotal7, .upTotal7, .wpePorcentAut7, .upPorcentAut7)
        End With
    End Sub

    Protected Sub ddlConcepto7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConcepto7.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                If Me.cbFactura7.Checked = True Then
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto_comp where id_concepto_comp = @id_concepto_comp "
                    SCMValores.Parameters.AddWithValue("@id_concepto_comp", ddlConcepto7.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino7.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    actNoFactura(Me.ddlConcepto7.SelectedValue, Me.wneNoPers7.Value, Me.upNoPers7, Me.wneNoDias7.Value, Me.upNoDias7, Me.txtRFC7, Me.ddlNoFactura7, Me.upNoFactura7, Me.hlProveedor7, Me.upProveedor7, Me.wceSubtotal7, Me.upSubtotal7, Me.wceIVA7, Me.upIVA7, Me.wceTotal7, Me.upTotal7, Me.wpePorcentAut7, Me.upPorcentAut7)
                Else
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto where id_concepto = @id_concepto "
                    SCMValores.Parameters.AddWithValue("@id_concepto", ddlConcepto7.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino7.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If
            Catch ex As Exception
                .litError.Text = ex.Message.ToString()
            End Try
        End With
        'If Me.cbFactura7.Checked = True Then
        '    'actFactura(Me.ddlConcepto7.SelectedValue, Me.wneNoPers7.Value, Me.ddlNoFactura7.SelectedValue, Me.hlProveedor7, Me.upProveedor7, Me.wceSubtotal7, Me.upSubtotal7, Me.wceIVA7, Me.upIVA7, Me.wceTotal7, Me.upTotal7)
        '    actNoFactura(Me.ddlConcepto7.SelectedValue, Me.wneNoPers7.Value, Me.upNoPers7, Me.wneNoDias7.Value, Me.upNoDias7, Me.txtRFC7, Me.ddlNoFactura7, Me.upNoFactura7, Me.hlProveedor7, Me.upProveedor7, Me.wceSubtotal7, Me.upSubtotal7, Me.wceIVA7, Me.upIVA7, Me.wceTotal7, Me.upTotal7, Me.wpePorcentAut7, Me.upPorcentAut7)
        'End If
    End Sub

    Protected Sub ibtnRFCBus7_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnRFCBus7.Click
        actNoFactura(Me.ddlConcepto7.SelectedValue, Me.wneNoPers7.Value, Me.upNoPers7, Me.wneNoDias7.Value, Me.upNoDias7, Me.txtRFC7, Me.ddlNoFactura7, Me.upNoFactura7, Me.hlProveedor7, Me.upProveedor7, Me.wceSubtotal7, Me.upSubtotal7, Me.wceIVA7, Me.upIVA7, Me.wceTotal7, Me.upTotal7, Me.wpePorcentAut7, Me.upPorcentAut7)
    End Sub

    Protected Sub ddlNoFactura7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoFactura7.SelectedIndexChanged
        actFactura(Me.ddlConcepto7.SelectedValue, Me.wneNoPers7.Value, Me.upNoPers7, Me.wneNoDias7.Value, Me.upNoDias7, Me.ddlNoFactura7.SelectedValue, Me.hlProveedor7, Me.upProveedor7, Me.wceSubtotal7, Me.upSubtotal7, Me.wceIVA7, Me.upIVA7, Me.wceTotal7, Me.upTotal7, Me.wpePorcentAut7, Me.upPorcentAut7)
    End Sub

#End Region

#Region "No. 8"

    Protected Sub cbFactura8_CheckedChanged(sender As Object, e As EventArgs) Handles cbFactura8.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbFactura8.Checked = True Then
                tipo = "F"
                .lblObsE8.Visible = False
            Else
                tipo = "T"
                .lblNoFacturaE8.Visible = False
                If .cbTabulador8.Checked = True And .txtObs8.Text.Trim = "" Then
                    .lblObsE8.Visible = True
                Else
                    .lblObsE8.Visible = False
                End If
            End If
            .upObsE8.Update()
            tipoFT(tipo, .cbFactura8, .upFactura8, .cbTabulador8, .upTabulador8, .ddlConcepto8, .upConcepto8, .wneNoPers8.Value, .upNoPers8, .wneNoDias8.Value, .upNoDias8, .txtRFC8, .ibtnRFCBus8, .upRFC8, .ddlNoFactura8, .upNoFactura8, .hlProveedor8, .upProveedor8, .wceSubtotal8, .upSubtotal8, .wceIVA8, .upIVA8, .wceTotal8, .upTotal8, .wpePorcentAut8, .upPorcentAut8)
        End With
    End Sub

    Protected Sub cbTabulador8_CheckedChanged(sender As Object, e As EventArgs) Handles cbTabulador8.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbTabulador8.Checked = True Then
                tipo = "T"
                .lblNoFacturaE8.Visible = False
                If .cbTabulador8.Checked = True And .txtObs8.Text.Trim = "" Then
                    .lblObsE8.Visible = True
                Else
                    .lblObsE8.Visible = False
                End If
            Else
                tipo = "F"
                .lblObsE8.Visible = False
            End If
            .upObsE8.Update()
            tipoFT(tipo, .cbFactura8, .upFactura8, .cbTabulador8, .upTabulador8, .ddlConcepto8, .upConcepto8, .wneNoPers8.Value, .upNoPers8, .wneNoDias8.Value, .upNoDias8, .txtRFC8, .ibtnRFCBus8, .upRFC8, .ddlNoFactura8, .upNoFactura8, .hlProveedor8, .upProveedor8, .wceSubtotal8, .upSubtotal8, .wceIVA8, .upIVA8, .wceTotal8, .upTotal8, .wpePorcentAut8, .upPorcentAut8)
        End With
    End Sub

    Protected Sub ddlConcepto8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConcepto8.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                If Me.cbFactura8.Checked = True Then
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto_comp where id_concepto_comp = @id_concepto_comp "
                    SCMValores.Parameters.AddWithValue("@id_concepto_comp", ddlConcepto8.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino8.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    actNoFactura(Me.ddlConcepto8.SelectedValue, Me.wneNoPers8.Value, Me.upNoPers8, Me.wneNoDias8.Value, Me.upNoDias8, Me.txtRFC8, Me.ddlNoFactura8, Me.upNoFactura8, Me.hlProveedor8, Me.upProveedor8, Me.wceSubtotal8, Me.upSubtotal8, Me.wceIVA8, Me.upIVA8, Me.wceTotal8, Me.upTotal8, Me.wpePorcentAut8, Me.upPorcentAut8)
                Else
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto where id_concepto = @id_concepto "
                    SCMValores.Parameters.AddWithValue("@id_concepto", ddlConcepto8.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino8.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If
            Catch ex As Exception
                .litError.Text = ex.Message.ToString()
            End Try
        End With
        'If Me.cbFactura8.Checked = True Then
        '    'actFactura(Me.ddlConcepto8.SelectedValue, Me.wneNoPers8.Value, Me.ddlNoFactura8.SelectedValue, Me.hlProveedor8, Me.upProveedor8, Me.wceSubtotal8, Me.upSubtotal8, Me.wceIVA8, Me.upIVA8, Me.wceTotal8, Me.upTotal8)
        '    actNoFactura(Me.ddlConcepto8.SelectedValue, Me.wneNoPers8.Value, Me.upNoPers8, Me.wneNoDias8.Value, Me.upNoDias8, Me.txtRFC8, Me.ddlNoFactura8, Me.upNoFactura8, Me.hlProveedor8, Me.upProveedor8, Me.wceSubtotal8, Me.upSubtotal8, Me.wceIVA8, Me.upIVA8, Me.wceTotal8, Me.upTotal8, Me.wpePorcentAut8, Me.upPorcentAut8)
        'End If
    End Sub

    Protected Sub ibtnRFCBus8_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnRFCBus8.Click
        actNoFactura(Me.ddlConcepto8.SelectedValue, Me.wneNoPers8.Value, Me.upNoPers8, Me.wneNoDias8.Value, Me.upNoDias8, Me.txtRFC8, Me.ddlNoFactura8, Me.upNoFactura8, Me.hlProveedor8, Me.upProveedor8, Me.wceSubtotal8, Me.upSubtotal8, Me.wceIVA8, Me.upIVA8, Me.wceTotal8, Me.upTotal8, Me.wpePorcentAut8, Me.upPorcentAut8)
    End Sub

    Protected Sub ddlNoFactura8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoFactura8.SelectedIndexChanged
        actFactura(Me.ddlConcepto8.SelectedValue, Me.wneNoPers8.Value, Me.upNoPers8, Me.wneNoDias8.Value, Me.upNoDias8, Me.ddlNoFactura8.SelectedValue, Me.hlProveedor8, Me.upProveedor8, Me.wceSubtotal8, Me.upSubtotal8, Me.wceIVA8, Me.upIVA8, Me.wceTotal8, Me.upTotal8, Me.wpePorcentAut8, Me.upPorcentAut8)
    End Sub

#End Region

#Region "No. 9"

    Protected Sub cbFactura9_CheckedChanged(sender As Object, e As EventArgs) Handles cbFactura9.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbFactura9.Checked = True Then
                tipo = "F"
                .lblObsE9.Visible = False
            Else
                tipo = "T"
                .lblNoFacturaE9.Visible = False
                If .cbTabulador9.Checked = True And .txtObs9.Text.Trim = "" Then
                    .lblObsE9.Visible = True
                Else
                    .lblObsE9.Visible = False
                End If
            End If
            .upObsE9.Update()
            tipoFT(tipo, .cbFactura9, .upFactura9, .cbTabulador9, .upTabulador9, .ddlConcepto9, .upConcepto9, .wneNoPers9.Value, .upNoPers9, .wneNoDias9.Value, .upNoDias9, .txtRFC9, .ibtnRFCBus9, .upRFC9, .ddlNoFactura9, .upNoFactura9, .hlProveedor9, .upProveedor9, .wceSubtotal9, .upSubtotal9, .wceIVA9, .upIVA9, .wceTotal9, .upTotal9, .wpePorcentAut9, .upPorcentAut9)
        End With
    End Sub

    Protected Sub cbTabulador9_CheckedChanged(sender As Object, e As EventArgs) Handles cbTabulador9.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbTabulador9.Checked = True Then
                tipo = "T"
                .lblNoFacturaE9.Visible = False
                If .cbTabulador9.Checked = True And .txtObs9.Text.Trim = "" Then
                    .lblObsE9.Visible = True
                Else
                    .lblObsE9.Visible = False
                End If
            Else
                tipo = "F"
                .lblObsE9.Visible = False
            End If
            .upObsE9.Update()
            tipoFT(tipo, .cbFactura9, .upFactura9, .cbTabulador9, .upTabulador9, .ddlConcepto9, .upConcepto9, .wneNoPers9.Value, .upNoPers9, .wneNoDias9.Value, .upNoDias9, .txtRFC9, .ibtnRFCBus9, .upRFC9, .ddlNoFactura9, .upNoFactura9, .hlProveedor9, .upProveedor9, .wceSubtotal9, .upSubtotal9, .wceIVA9, .upIVA9, .wceTotal9, .upTotal9, .wpePorcentAut9, .upPorcentAut9)
        End With
    End Sub

    Protected Sub ddlConcepto9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConcepto9.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                If Me.cbFactura9.Checked = True Then
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto_comp where id_concepto_comp = @id_concepto_comp "
                    SCMValores.Parameters.AddWithValue("@id_concepto_comp", ddlConcepto9.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino9.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    actNoFactura(Me.ddlConcepto9.SelectedValue, Me.wneNoPers9.Value, Me.upNoPers9, Me.wneNoDias9.Value, Me.upNoDias9, Me.txtRFC9, Me.ddlNoFactura9, Me.upNoFactura9, Me.hlProveedor9, Me.upProveedor9, Me.wceSubtotal9, Me.upSubtotal9, Me.wceIVA9, Me.upIVA9, Me.wceTotal9, Me.upTotal9, Me.wpePorcentAut9, Me.upPorcentAut9)
                Else
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto where id_concepto = @id_concepto "
                    SCMValores.Parameters.AddWithValue("@id_concepto", ddlConcepto9.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino9.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If
            Catch ex As Exception
                .litError.Text = ex.Message.ToString()
            End Try
        End With
        'If Me.cbFactura9.Checked = True Then
        '    'actFactura(Me.ddlConcepto9.SelectedValue, Me.wneNoPers9.Value, Me.ddlNoFactura9.SelectedValue, Me.hlProveedor9, Me.upProveedor9, Me.wceSubtotal9, Me.upSubtotal9, Me.wceIVA9, Me.upIVA9, Me.wceTotal9, Me.upTotal9)
        '    actNoFactura(Me.ddlConcepto9.SelectedValue, Me.wneNoPers9.Value, Me.upNoPers9, Me.wneNoDias9.Value, Me.upNoDias9, Me.txtRFC9, Me.ddlNoFactura9, Me.upNoFactura9, Me.hlProveedor9, Me.upProveedor9, Me.wceSubtotal9, Me.upSubtotal9, Me.wceIVA9, Me.upIVA9, Me.wceTotal9, Me.upTotal9, Me.wpePorcentAut9, Me.upPorcentAut9)
        'End If
    End Sub

    Protected Sub ibtnRFCBus9_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnRFCBus9.Click
        actNoFactura(Me.ddlConcepto9.SelectedValue, Me.wneNoPers9.Value, Me.upNoPers9, Me.wneNoDias9.Value, Me.upNoDias9, Me.txtRFC9, Me.ddlNoFactura9, Me.upNoFactura9, Me.hlProveedor9, Me.upProveedor9, Me.wceSubtotal9, Me.upSubtotal9, Me.wceIVA9, Me.upIVA9, Me.wceTotal9, Me.upTotal9, Me.wpePorcentAut9, Me.upPorcentAut9)
    End Sub

    Protected Sub ddlNoFactura9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoFactura9.SelectedIndexChanged
        actFactura(Me.ddlConcepto9.SelectedValue, Me.wneNoPers9.Value, Me.upNoPers9, Me.wneNoDias9.Value, Me.upNoDias9, Me.ddlNoFactura9.SelectedValue, Me.hlProveedor9, Me.upProveedor9, Me.wceSubtotal9, Me.upSubtotal9, Me.wceIVA9, Me.upIVA9, Me.wceTotal9, Me.upTotal9, Me.wpePorcentAut9, Me.upPorcentAut9)
    End Sub

#End Region

#Region "No. 10"

    Protected Sub cbFactura10_CheckedChanged(sender As Object, e As EventArgs) Handles cbFactura10.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbFactura10.Checked = True Then
                tipo = "F"
                .lblObsE10.Visible = False
            Else
                tipo = "T"
                .lblNoFacturaE10.Visible = False
                If .cbTabulador10.Checked = True And .txtObs10.Text.Trim = "" Then
                    .lblObsE10.Visible = True
                Else
                    .lblObsE10.Visible = False
                End If
            End If
            .upObsE10.Update()
            tipoFT(tipo, .cbFactura10, .upFactura10, .cbTabulador10, .upTabulador10, .ddlConcepto10, .upConcepto10, .wneNoPers10.Value, .upNoPers10, .wneNoDias10.Value, .upNoDias10, .txtRFC10, .ibtnRFCBus10, .upRFC10, .ddlNoFactura10, .upNoFactura10, .hlProveedor10, .upProveedor10, .wceSubtotal10, .upSubtotal10, .wceIVA10, .upIVA10, .wceTotal10, .upTotal10, .wpePorcentAut10, .upPorcentAut10)
        End With
    End Sub

    Protected Sub cbTabulador10_CheckedChanged(sender As Object, e As EventArgs) Handles cbTabulador10.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbTabulador10.Checked = True Then
                tipo = "T"
                .lblNoFacturaE10.Visible = False
                If .cbTabulador10.Checked = True And .txtObs10.Text.Trim = "" Then
                    .lblObsE10.Visible = True
                Else
                    .lblObsE10.Visible = False
                End If
            Else
                tipo = "F"
                .lblObsE10.Visible = False
            End If
            .upObsE10.Update()
            tipoFT(tipo, .cbFactura10, .upFactura10, .cbTabulador10, .upTabulador10, .ddlConcepto10, .upConcepto10, .wneNoPers10.Value, .upNoPers10, .wneNoDias10.Value, .upNoDias10, .txtRFC10, .ibtnRFCBus10, .upRFC10, .ddlNoFactura10, .upNoFactura10, .hlProveedor10, .upProveedor10, .wceSubtotal10, .upSubtotal10, .wceIVA10, .upIVA10, .wceTotal10, .upTotal10, .wpePorcentAut10, .upPorcentAut10)
        End With
    End Sub

    Protected Sub ddlConcepto10_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConcepto10.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                If Me.cbFactura10.Checked = True Then
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto_comp where id_concepto_comp = @id_concepto_comp "
                    SCMValores.Parameters.AddWithValue("@id_concepto_comp", ddlConcepto10.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino10.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    actNoFactura(Me.ddlConcepto10.SelectedValue, Me.wneNoPers10.Value, Me.upNoPers10, Me.wneNoDias10.Value, Me.upNoDias10, Me.txtRFC10, Me.ddlNoFactura10, Me.upNoFactura10, Me.hlProveedor10, Me.upProveedor10, Me.wceSubtotal10, Me.upSubtotal10, Me.wceIVA10, Me.upIVA10, Me.wceTotal10, Me.upTotal10, Me.wpePorcentAut10, Me.upPorcentAut10)
                Else
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto where id_concepto = @id_concepto "
                    SCMValores.Parameters.AddWithValue("@id_concepto", ddlConcepto10.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino10.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If
            Catch ex As Exception
                .litError.Text = ex.Message.ToString()
            End Try
        End With
        'If Me.cbFactura10.Checked = True Then
        '    'actFactura(Me.ddlConcepto10.SelectedValue, Me.wneNoPers10.Value, Me.ddlNoFactura10.SelectedValue, Me.hlProveedor10, Me.upProveedor10, Me.wceSubtotal10, Me.upSubtotal10, Me.wceIVA10, Me.upIVA10, Me.wceTotal10, Me.upTotal10)
        '    actNoFactura(Me.ddlConcepto10.SelectedValue, Me.wneNoPers10.Value, Me.upNoPers10, Me.wneNoDias10.Value, Me.upNoDias10, Me.txtRFC10, Me.ddlNoFactura10, Me.upNoFactura10, Me.hlProveedor10, Me.upProveedor10, Me.wceSubtotal10, Me.upSubtotal10, Me.wceIVA10, Me.upIVA10, Me.wceTotal10, Me.upTotal10, Me.wpePorcentAut10, Me.upPorcentAut10)
        'End If
    End Sub

    Protected Sub ibtnRFCBus10_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnRFCBus10.Click
        actNoFactura(Me.ddlConcepto10.SelectedValue, Me.wneNoPers10.Value, Me.upNoPers10, Me.wneNoDias10.Value, Me.upNoDias10, Me.txtRFC10, Me.ddlNoFactura10, Me.upNoFactura10, Me.hlProveedor10, Me.upProveedor10, Me.wceSubtotal10, Me.upSubtotal10, Me.wceIVA10, Me.upIVA10, Me.wceTotal10, Me.upTotal10, Me.wpePorcentAut10, Me.upPorcentAut10)
    End Sub

    Protected Sub ddlNoFactura10_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoFactura10.SelectedIndexChanged
        actFactura(Me.ddlConcepto10.SelectedValue, Me.wneNoPers10.Value, Me.upNoPers10, Me.wneNoDias10.Value, Me.upNoDias10, Me.ddlNoFactura10.SelectedValue, Me.hlProveedor10, Me.upProveedor10, Me.wceSubtotal10, Me.upSubtotal10, Me.wceIVA10, Me.upIVA10, Me.wceTotal10, Me.upTotal10, Me.wpePorcentAut10, Me.upPorcentAut10)
    End Sub

#End Region

#Region "No. 11"

    Protected Sub cbFactura11_CheckedChanged(sender As Object, e As EventArgs) Handles cbFactura11.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbFactura11.Checked = True Then
                tipo = "F"
                .lblObsE11.Visible = False
            Else
                tipo = "T"
                .lblNoFacturaE11.Visible = False
                If .cbTabulador11.Checked = True And .txtObs11.Text.Trim = "" Then
                    .lblObsE11.Visible = True
                Else
                    .lblObsE11.Visible = False
                End If
            End If
            .upObsE11.Update()
            tipoFT(tipo, .cbFactura11, .upFactura11, .cbTabulador11, .upTabulador11, .ddlConcepto11, .upConcepto11, .wneNoPers11.Value, .upNoPers11, .wneNoDias11.Value, .upNoDias11, .txtRFC11, .ibtnRFCBus11, .upRFC11, .ddlNoFactura11, .upNoFactura11, .hlProveedor11, .upProveedor11, .wceSubtotal11, .upSubtotal11, .wceIVA11, .upIVA11, .wceTotal11, .upTotal11, .wpePorcentAut11, .upPorcentAut11)
        End With
    End Sub

    Protected Sub cbTabulador11_CheckedChanged(sender As Object, e As EventArgs) Handles cbTabulador11.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbTabulador11.Checked = True Then
                tipo = "T"
                .lblNoFacturaE11.Visible = False
                If .cbTabulador11.Checked = True And .txtObs11.Text.Trim = "" Then
                    .lblObsE11.Visible = True
                Else
                    .lblObsE11.Visible = False
                End If
            Else
                tipo = "F"
                .lblObsE11.Visible = False
            End If
            .upObsE11.Update()
            tipoFT(tipo, .cbFactura11, .upFactura11, .cbTabulador11, .upTabulador11, .ddlConcepto11, .upConcepto11, .wneNoPers11.Value, .upNoPers11, .wneNoDias11.Value, .upNoDias11, .txtRFC11, .ibtnRFCBus11, .upRFC11, .ddlNoFactura11, .upNoFactura11, .hlProveedor11, .upProveedor11, .wceSubtotal11, .upSubtotal11, .wceIVA11, .upIVA11, .wceTotal11, .upTotal11, .wpePorcentAut11, .upPorcentAut11)
        End With
    End Sub

    Protected Sub ddlConcepto11_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConcepto11.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                If Me.cbFactura11.Checked = True Then
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto_comp where id_concepto_comp = @id_concepto_comp "
                    SCMValores.Parameters.AddWithValue("@id_concepto_comp", ddlConcepto11.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino11.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    actNoFactura(Me.ddlConcepto11.SelectedValue, Me.wneNoPers11.Value, Me.upNoPers11, Me.wneNoDias11.Value, Me.upNoDias11, Me.txtRFC11, Me.ddlNoFactura11, Me.upNoFactura11, Me.hlProveedor11, Me.upProveedor11, Me.wceSubtotal11, Me.upSubtotal11, Me.wceIVA11, Me.upIVA11, Me.wceTotal11, Me.upTotal11, Me.wpePorcentAut11, Me.upPorcentAut11)
                Else
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto where id_concepto = @id_concepto "
                    SCMValores.Parameters.AddWithValue("@id_concepto", ddlConcepto11.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino11.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If
            Catch ex As Exception
                .litError.Text = ex.Message.ToString()
            End Try
        End With
        'If Me.cbFactura11.Checked = True Then
        '    'actFactura(Me.ddlConcepto11.SelectedValue, Me.wneNoPers11.Value, Me.ddlNoFactura11.SelectedValue, Me.hlProveedor11, Me.upProveedor11, Me.wceSubtotal11, Me.upSubtotal11, Me.wceIVA11, Me.upIVA11, Me.wceTotal11, Me.upTotal11)
        '    actNoFactura(Me.ddlConcepto11.SelectedValue, Me.wneNoPers11.Value, Me.upNoPers11, Me.wneNoDias11.Value, Me.upNoDias11, Me.txtRFC11, Me.ddlNoFactura11, Me.upNoFactura11, Me.hlProveedor11, Me.upProveedor11, Me.wceSubtotal11, Me.upSubtotal11, Me.wceIVA11, Me.upIVA11, Me.wceTotal11, Me.upTotal11, Me.wpePorcentAut11, Me.upPorcentAut11)
        'End If
    End Sub

    Protected Sub ibtnRFCBus11_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnRFCBus11.Click
        actNoFactura(Me.ddlConcepto11.SelectedValue, Me.wneNoPers11.Value, Me.upNoPers11, Me.wneNoDias11.Value, Me.upNoDias11, Me.txtRFC11, Me.ddlNoFactura11, Me.upNoFactura11, Me.hlProveedor11, Me.upProveedor11, Me.wceSubtotal11, Me.upSubtotal11, Me.wceIVA11, Me.upIVA11, Me.wceTotal11, Me.upTotal11, Me.wpePorcentAut11, Me.upPorcentAut11)
    End Sub

    Protected Sub ddlNoFactura11_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoFactura11.SelectedIndexChanged
        actFactura(Me.ddlConcepto11.SelectedValue, Me.wneNoPers11.Value, Me.upNoPers11, Me.wneNoDias11.Value, Me.upNoDias11, Me.ddlNoFactura11.SelectedValue, Me.hlProveedor11, Me.upProveedor11, Me.wceSubtotal11, Me.upSubtotal11, Me.wceIVA11, Me.upIVA11, Me.wceTotal11, Me.upTotal11, Me.wpePorcentAut11, Me.upPorcentAut11)
    End Sub

#End Region

#Region "No. 12"

    Protected Sub cbFactura12_CheckedChanged(sender As Object, e As EventArgs) Handles cbFactura12.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbFactura12.Checked = True Then
                tipo = "F"
                .lblObsE12.Visible = False
            Else
                tipo = "T"
                .lblNoFacturaE12.Visible = False
                If .cbTabulador12.Checked = True And .txtObs12.Text.Trim = "" Then
                    .lblObsE12.Visible = True
                Else
                    .lblObsE12.Visible = False
                End If
            End If
            .upObsE12.Update()
            tipoFT(tipo, .cbFactura12, .upFactura12, .cbTabulador12, .upTabulador12, .ddlConcepto12, .upConcepto12, .wneNoPers12.Value, .upNoPers12, .wneNoDias12.Value, .upNoDias12, .txtRFC12, .ibtnRFCBus12, .upRFC12, .ddlNoFactura12, .upNoFactura12, .hlProveedor12, .upProveedor12, .wceSubtotal12, .upSubtotal12, .wceIVA12, .upIVA12, .wceTotal12, .upTotal12, .wpePorcentAut12, .upPorcentAut12)
        End With
    End Sub

    Protected Sub cbTabulador12_CheckedChanged(sender As Object, e As EventArgs) Handles cbTabulador12.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbTabulador12.Checked = True Then
                tipo = "T"
                .lblNoFacturaE12.Visible = False
                If .cbTabulador12.Checked = True And .txtObs12.Text.Trim = "" Then
                    .lblObsE12.Visible = True
                Else
                    .lblObsE12.Visible = False
                End If
            Else
                tipo = "F"
                .lblObsE12.Visible = False
            End If
            .upObsE12.Update()
            tipoFT(tipo, .cbFactura12, .upFactura12, .cbTabulador12, .upTabulador12, .ddlConcepto12, .upConcepto12, .wneNoPers12.Value, .upNoPers12, .wneNoDias12.Value, .upNoDias12, .txtRFC12, .ibtnRFCBus12, .upRFC12, .ddlNoFactura12, .upNoFactura12, .hlProveedor12, .upProveedor12, .wceSubtotal12, .upSubtotal12, .wceIVA12, .upIVA12, .wceTotal12, .upTotal12, .wpePorcentAut12, .upPorcentAut12)
        End With
    End Sub

    Protected Sub ddlConcepto12_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConcepto12.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                If Me.cbFactura12.Checked = True Then
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto_comp where id_concepto_comp = @id_concepto_comp "
                    SCMValores.Parameters.AddWithValue("@id_concepto_comp", ddlConcepto12.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino12.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    actNoFactura(Me.ddlConcepto12.SelectedValue, Me.wneNoPers12.Value, Me.upNoPers12, Me.wneNoDias12.Value, Me.upNoDias12, Me.txtRFC12, Me.ddlNoFactura12, Me.upNoFactura12, Me.hlProveedor12, Me.upProveedor12, Me.wceSubtotal12, Me.upSubtotal12, Me.wceIVA12, Me.upIVA12, Me.wceTotal12, Me.upTotal12, Me.wpePorcentAut12, Me.upPorcentAut12)
                Else
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto where id_concepto = @id_concepto "
                    SCMValores.Parameters.AddWithValue("@id_concepto", ddlConcepto12.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino12.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If
            Catch ex As Exception
                .litError.Text = ex.Message.ToString()
            End Try
        End With
        'If Me.cbFactura12.Checked = True Then
        '    'actFactura(Me.ddlConcepto12.SelectedValue, Me.wneNoPers12.Value, Me.ddlNoFactura12.SelectedValue, Me.hlProveedor12, Me.upProveedor12, Me.wceSubtotal12, Me.upSubtotal12, Me.wceIVA12, Me.upIVA12, Me.wceTotal12, Me.upTotal12)
        '    actNoFactura(Me.ddlConcepto12.SelectedValue, Me.wneNoPers12.Value, Me.upNoPers12, Me.wneNoDias12.Value, Me.upNoDias12, Me.txtRFC12, Me.ddlNoFactura12, Me.upNoFactura12, Me.hlProveedor12, Me.upProveedor12, Me.wceSubtotal12, Me.upSubtotal12, Me.wceIVA12, Me.upIVA12, Me.wceTotal12, Me.upTotal12, Me.wpePorcentAut12, Me.upPorcentAut12)
        'End If
    End Sub

    Protected Sub ibtnRFCBus12_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnRFCBus12.Click
        actNoFactura(Me.ddlConcepto12.SelectedValue, Me.wneNoPers12.Value, Me.upNoPers12, Me.wneNoDias12.Value, Me.upNoDias12, Me.txtRFC12, Me.ddlNoFactura12, Me.upNoFactura12, Me.hlProveedor12, Me.upProveedor12, Me.wceSubtotal12, Me.upSubtotal12, Me.wceIVA12, Me.upIVA12, Me.wceTotal12, Me.upTotal12, Me.wpePorcentAut12, Me.upPorcentAut12)
    End Sub

    Protected Sub ddlNoFactura12_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoFactura12.SelectedIndexChanged
        actFactura(Me.ddlConcepto12.SelectedValue, Me.wneNoPers12.Value, Me.upNoPers12, Me.wneNoDias12.Value, Me.upNoDias12, Me.ddlNoFactura12.SelectedValue, Me.hlProveedor12, Me.upProveedor12, Me.wceSubtotal12, Me.upSubtotal12, Me.wceIVA12, Me.upIVA12, Me.wceTotal12, Me.upTotal12, Me.wpePorcentAut12, Me.upPorcentAut12)
    End Sub

#End Region

#Region "No. 13"

    Protected Sub cbFactura13_CheckedChanged(sender As Object, e As EventArgs) Handles cbFactura13.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbFactura13.Checked = True Then
                tipo = "F"
                .lblObsE13.Visible = False
            Else
                tipo = "T"
                .lblNoFacturaE13.Visible = False
                If .cbTabulador13.Checked = True And .txtObs13.Text.Trim = "" Then
                    .lblObsE13.Visible = True
                Else
                    .lblObsE13.Visible = False
                End If
            End If
            .upObsE13.Update()
            tipoFT(tipo, .cbFactura13, .upFactura13, .cbTabulador13, .upTabulador13, .ddlConcepto13, .upConcepto13, .wneNoPers13.Value, .upNoPers13, .wneNoDias13.Value, .upNoDias13, .txtRFC13, .ibtnRFCBus13, .upRFC13, .ddlNoFactura13, .upNoFactura13, .hlProveedor13, .upProveedor13, .wceSubtotal13, .upSubtotal13, .wceIVA13, .upIVA13, .wceTotal13, .upTotal13, .wpePorcentAut13, .upPorcentAut13)
        End With
    End Sub

    Protected Sub cbTabulador13_CheckedChanged(sender As Object, e As EventArgs) Handles cbTabulador13.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbTabulador13.Checked = True Then
                tipo = "T"
                .lblNoFacturaE13.Visible = False
                If .cbTabulador13.Checked = True And .txtObs13.Text.Trim = "" Then
                    .lblObsE13.Visible = True
                Else
                    .lblObsE13.Visible = False
                End If
            Else
                tipo = "F"
                .lblObsE13.Visible = False
            End If
            .upObsE13.Update()
            tipoFT(tipo, .cbFactura13, .upFactura13, .cbTabulador13, .upTabulador13, .ddlConcepto13, .upConcepto13, .wneNoPers13.Value, .upNoPers13, .wneNoDias13.Value, .upNoDias13, .txtRFC13, .ibtnRFCBus13, .upRFC13, .ddlNoFactura13, .upNoFactura13, .hlProveedor13, .upProveedor13, .wceSubtotal13, .upSubtotal13, .wceIVA13, .upIVA13, .wceTotal13, .upTotal13, .wpePorcentAut13, .upPorcentAut13)
        End With
    End Sub

    Protected Sub ddlConcepto13_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConcepto13.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                If Me.cbFactura13.Checked = True Then
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto_comp where id_concepto_comp = @id_concepto_comp "
                    SCMValores.Parameters.AddWithValue("@id_concepto_comp", ddlConcepto13.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino13.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    actNoFactura(Me.ddlConcepto13.SelectedValue, Me.wneNoPers13.Value, Me.upNoPers13, Me.wneNoDias13.Value, Me.upNoDias13, Me.txtRFC13, Me.ddlNoFactura13, Me.upNoFactura13, Me.hlProveedor13, Me.upProveedor13, Me.wceSubtotal13, Me.upSubtotal13, Me.wceIVA13, Me.upIVA13, Me.wceTotal13, Me.upTotal13, Me.wpePorcentAut13, Me.upPorcentAut13)
                Else
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto where id_concepto = @id_concepto "
                    SCMValores.Parameters.AddWithValue("@id_concepto", ddlConcepto13.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino13.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If
            Catch ex As Exception
                .litError.Text = ex.Message.ToString()
            End Try
        End With
        'If Me.cbFactura13.Checked = True Then
        '    'actFactura(Me.ddlConcepto13.SelectedValue, Me.wneNoPers13.Value, Me.ddlNoFactura13.SelectedValue, Me.hlProveedor13, Me.upProveedor13, Me.wceSubtotal13, Me.upSubtotal13, Me.wceIVA13, Me.upIVA13, Me.wceTotal13, Me.upTotal13)
        '    actNoFactura(Me.ddlConcepto13.SelectedValue, Me.wneNoPers13.Value, Me.upNoPers13, Me.wneNoDias13.Value, Me.upNoDias13, Me.txtRFC13, Me.ddlNoFactura13, Me.upNoFactura13, Me.hlProveedor13, Me.upProveedor13, Me.wceSubtotal13, Me.upSubtotal13, Me.wceIVA13, Me.upIVA13, Me.wceTotal13, Me.upTotal13, Me.wpePorcentAut13, Me.upPorcentAut13)
        'End If
    End Sub

    Protected Sub ibtnRFCBus13_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnRFCBus13.Click
        actNoFactura(Me.ddlConcepto13.SelectedValue, Me.wneNoPers13.Value, Me.upNoPers13, Me.wneNoDias13.Value, Me.upNoDias13, Me.txtRFC13, Me.ddlNoFactura13, Me.upNoFactura13, Me.hlProveedor13, Me.upProveedor13, Me.wceSubtotal13, Me.upSubtotal13, Me.wceIVA13, Me.upIVA13, Me.wceTotal13, Me.upTotal13, Me.wpePorcentAut13, Me.upPorcentAut13)
    End Sub

    Protected Sub ddlNoFactura13_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoFactura13.SelectedIndexChanged
        actFactura(Me.ddlConcepto13.SelectedValue, Me.wneNoPers13.Value, Me.upNoPers13, Me.wneNoDias13.Value, Me.upNoDias13, Me.ddlNoFactura13.SelectedValue, Me.hlProveedor13, Me.upProveedor13, Me.wceSubtotal13, Me.upSubtotal13, Me.wceIVA13, Me.upIVA13, Me.wceTotal13, Me.upTotal13, Me.wpePorcentAut13, Me.upPorcentAut13)
    End Sub

#End Region

#Region "No. 14"

    Protected Sub cbFactura14_CheckedChanged(sender As Object, e As EventArgs) Handles cbFactura14.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbFactura14.Checked = True Then
                tipo = "F"
                .lblObsE14.Visible = False
            Else
                tipo = "T"
                .lblNoFacturaE14.Visible = False
                If .cbTabulador14.Checked = True And .txtObs14.Text.Trim = "" Then
                    .lblObsE14.Visible = True
                Else
                    .lblObsE14.Visible = False
                End If
            End If
            .upObsE14.Update()
            tipoFT(tipo, .cbFactura14, .upFactura14, .cbTabulador14, .upTabulador14, .ddlConcepto14, .upConcepto14, .wneNoPers14.Value, .upNoPers14, .wneNoDias14.Value, .upNoDias14, .txtRFC14, .ibtnRFCBus14, .upRFC14, .ddlNoFactura14, .upNoFactura14, .hlProveedor14, .upProveedor14, .wceSubtotal14, .upSubtotal14, .wceIVA14, .upIVA14, .wceTotal14, .upTotal14, .wpePorcentAut14, .upPorcentAut14)
        End With
    End Sub

    Protected Sub cbTabulador14_CheckedChanged(sender As Object, e As EventArgs) Handles cbTabulador14.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbTabulador14.Checked = True Then
                tipo = "T"
                .lblNoFacturaE14.Visible = False
                If .cbTabulador14.Checked = True And .txtObs14.Text.Trim = "" Then
                    .lblObsE14.Visible = True
                Else
                    .lblObsE14.Visible = False
                End If
            Else
                tipo = "F"
                .lblObsE14.Visible = False
            End If
            .upObsE14.Update()
            tipoFT(tipo, .cbFactura14, .upFactura14, .cbTabulador14, .upTabulador14, .ddlConcepto14, .upConcepto14, .wneNoPers14.Value, .upNoPers14, .wneNoDias14.Value, .upNoDias14, .txtRFC14, .ibtnRFCBus14, .upRFC14, .ddlNoFactura14, .upNoFactura14, .hlProveedor14, .upProveedor14, .wceSubtotal14, .upSubtotal14, .wceIVA14, .upIVA14, .wceTotal14, .upTotal14, .wpePorcentAut14, .upPorcentAut14)
        End With
    End Sub

    Protected Sub ddlConcepto14_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConcepto14.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                If Me.cbFactura14.Checked = True Then
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto_comp where id_concepto_comp = @id_concepto_comp "
                    SCMValores.Parameters.AddWithValue("@id_concepto_comp", ddlConcepto14.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino14.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    actNoFactura(Me.ddlConcepto14.SelectedValue, Me.wneNoPers14.Value, Me.upNoPers14, Me.wneNoDias14.Value, Me.upNoDias14, Me.txtRFC14, Me.ddlNoFactura14, Me.upNoFactura14, Me.hlProveedor14, Me.upProveedor14, Me.wceSubtotal14, Me.upSubtotal14, Me.wceIVA14, Me.upIVA14, Me.wceTotal14, Me.upTotal14, Me.wpePorcentAut14, Me.upPorcentAut14)
                Else
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto where id_concepto = @id_concepto "
                    SCMValores.Parameters.AddWithValue("@id_concepto", ddlConcepto14.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino14.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If
            Catch ex As Exception
                .litError.Text = ex.Message.ToString()
            End Try
        End With
        'If Me.cbFactura14.Checked = True Then
        '    'actFactura(Me.ddlConcepto14.SelectedValue, Me.wneNoPers14.Value, Me.ddlNoFactura14.SelectedValue, Me.hlProveedor14, Me.upProveedor14, Me.wceSubtotal14, Me.upSubtotal14, Me.wceIVA14, Me.upIVA14, Me.wceTotal14, Me.upTotal14)
        '    actNoFactura(Me.ddlConcepto14.SelectedValue, Me.wneNoPers14.Value, Me.upNoPers14, Me.wneNoDias14.Value, Me.upNoDias14, Me.txtRFC14, Me.ddlNoFactura14, Me.upNoFactura14, Me.hlProveedor14, Me.upProveedor14, Me.wceSubtotal14, Me.upSubtotal14, Me.wceIVA14, Me.upIVA14, Me.wceTotal14, Me.upTotal14, Me.wpePorcentAut14, Me.upPorcentAut14)
        'End If
    End Sub

    Protected Sub ibtnRFCBus14_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnRFCBus14.Click
        actNoFactura(Me.ddlConcepto14.SelectedValue, Me.wneNoPers14.Value, Me.upNoPers14, Me.wneNoDias14.Value, Me.upNoDias14, Me.txtRFC14, Me.ddlNoFactura14, Me.upNoFactura14, Me.hlProveedor14, Me.upProveedor14, Me.wceSubtotal14, Me.upSubtotal14, Me.wceIVA14, Me.upIVA14, Me.wceTotal14, Me.upTotal14, Me.wpePorcentAut14, Me.upPorcentAut14)
    End Sub

    Protected Sub ddlNoFactura14_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoFactura14.SelectedIndexChanged
        actFactura(Me.ddlConcepto14.SelectedValue, Me.wneNoPers14.Value, Me.upNoPers14, Me.wneNoDias14.Value, Me.upNoDias14, Me.ddlNoFactura14.SelectedValue, Me.hlProveedor14, Me.upProveedor14, Me.wceSubtotal14, Me.upSubtotal14, Me.wceIVA14, Me.upIVA14, Me.wceTotal14, Me.upTotal14, Me.wpePorcentAut14, Me.upPorcentAut14)
    End Sub

#End Region

#Region "No. 15"

    Protected Sub cbFactura15_CheckedChanged(sender As Object, e As EventArgs) Handles cbFactura15.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbFactura15.Checked = True Then
                tipo = "F"
                .lblObsE15.Visible = False
            Else
                tipo = "T"
                .lblNoFacturaE15.Visible = False
                If .cbTabulador15.Checked = True And .txtObs15.Text.Trim = "" Then
                    .lblObsE15.Visible = True
                Else
                    .lblObsE15.Visible = False
                End If
            End If
            .upObsE15.Update()
            tipoFT(tipo, .cbFactura15, .upFactura15, .cbTabulador15, .upTabulador15, .ddlConcepto15, .upConcepto15, .wneNoPers15.Value, .upNoPers15, .wneNoDias15.Value, .upNoDias15, .txtRFC15, .ibtnRFCBus15, .upRFC15, .ddlNoFactura15, .upNoFactura15, .hlProveedor15, .upProveedor15, .wceSubtotal15, .upSubtotal15, .wceIVA15, .upIVA15, .wceTotal15, .upTotal15, .wpePorcentAut15, .upPorcentAut15)
        End With
    End Sub

    Protected Sub cbTabulador15_CheckedChanged(sender As Object, e As EventArgs) Handles cbTabulador15.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbTabulador15.Checked = True Then
                tipo = "T"
                .lblNoFacturaE15.Visible = False
                If .cbTabulador15.Checked = True And .txtObs15.Text.Trim = "" Then
                    .lblObsE15.Visible = True
                Else
                    .lblObsE15.Visible = False
                End If
            Else
                tipo = "F"
                .lblObsE15.Visible = False
            End If
            .upObsE15.Update()
            tipoFT(tipo, .cbFactura15, .upFactura15, .cbTabulador15, .upTabulador15, .ddlConcepto15, .upConcepto15, .wneNoPers15.Value, .upNoPers15, .wneNoDias15.Value, .upNoDias15, .txtRFC15, .ibtnRFCBus15, .upRFC15, .ddlNoFactura15, .upNoFactura15, .hlProveedor15, .upProveedor15, .wceSubtotal15, .upSubtotal15, .wceIVA15, .upIVA15, .wceTotal15, .upTotal15, .wpePorcentAut15, .upPorcentAut15)
        End With
    End Sub

    Protected Sub ddlConcepto15_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConcepto15.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                If Me.cbFactura15.Checked = True Then
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto_comp where id_concepto_comp = @id_concepto_comp "
                    SCMValores.Parameters.AddWithValue("@id_concepto_comp", ddlConcepto15.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino15.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    actNoFactura(Me.ddlConcepto15.SelectedValue, Me.wneNoPers15.Value, Me.upNoPers15, Me.wneNoDias15.Value, Me.upNoDias15, Me.txtRFC15, Me.ddlNoFactura15, Me.upNoFactura15, Me.hlProveedor15, Me.upProveedor15, Me.wceSubtotal15, Me.upSubtotal15, Me.wceIVA15, Me.upIVA15, Me.wceTotal15, Me.upTotal15, Me.wpePorcentAut15, Me.upPorcentAut15)
                Else
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto where id_concepto = @id_concepto "
                    SCMValores.Parameters.AddWithValue("@id_concepto", ddlConcepto15.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino15.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If
            Catch ex As Exception
                .litError.Text = ex.Message.ToString()
            End Try
        End With
        'If Me.cbFactura15.Checked = True Then
        '    'actFactura(Me.ddlConcepto15.SelectedValue, Me.wneNoPers15.Value, Me.ddlNoFactura15.SelectedValue, Me.hlProveedor15, Me.upProveedor15, Me.wceSubtotal15, Me.upSubtotal15, Me.wceIVA15, Me.upIVA15, Me.wceTotal15, Me.upTotal15)
        '    actNoFactura(Me.ddlConcepto15.SelectedValue, Me.wneNoPers15.Value, Me.upNoPers15, Me.wneNoDias15.Value, Me.upNoDias15, Me.txtRFC15, Me.ddlNoFactura15, Me.upNoFactura15, Me.hlProveedor15, Me.upProveedor15, Me.wceSubtotal15, Me.upSubtotal15, Me.wceIVA15, Me.upIVA15, Me.wceTotal15, Me.upTotal15, Me.wpePorcentAut15, Me.upPorcentAut15)
        'End If
    End Sub

    Protected Sub ibtnRFCBus15_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnRFCBus15.Click
        actNoFactura(Me.ddlConcepto15.SelectedValue, Me.wneNoPers15.Value, Me.upNoPers15, Me.wneNoDias15.Value, Me.upNoDias15, Me.txtRFC15, Me.ddlNoFactura15, Me.upNoFactura15, Me.hlProveedor15, Me.upProveedor15, Me.wceSubtotal15, Me.upSubtotal15, Me.wceIVA15, Me.upIVA15, Me.wceTotal15, Me.upTotal15, Me.wpePorcentAut15, Me.upPorcentAut15)
    End Sub

    Protected Sub ddlNoFactura15_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoFactura15.SelectedIndexChanged
        actFactura(Me.ddlConcepto15.SelectedValue, Me.wneNoPers15.Value, Me.upNoPers15, Me.wneNoDias15.Value, Me.upNoDias15, Me.ddlNoFactura15.SelectedValue, Me.hlProveedor15, Me.upProveedor15, Me.wceSubtotal15, Me.upSubtotal15, Me.wceIVA15, Me.upIVA15, Me.wceTotal15, Me.upTotal15, Me.wpePorcentAut15, Me.upPorcentAut15)
    End Sub

#End Region

#Region "No. 16"

    Protected Sub cbFactura16_CheckedChanged(sender As Object, e As EventArgs) Handles cbFactura16.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbFactura16.Checked = True Then
                tipo = "F"
                .lblObsE16.Visible = False
            Else
                tipo = "T"
                .lblNoFacturaE16.Visible = False
                If .cbTabulador16.Checked = True And .txtObs16.Text.Trim = "" Then
                    .lblObsE16.Visible = True
                Else
                    .lblObsE16.Visible = False
                End If
            End If
            .upObsE16.Update()
            tipoFT(tipo, .cbFactura16, .upFactura16, .cbTabulador16, .upTabulador16, .ddlConcepto16, .upConcepto16, .wneNoPers16.Value, .upNoPers16, .wneNoDias16.Value, .upNoDias16, .txtRFC16, .ibtnRFCBus16, .upRFC16, .ddlNoFactura16, .upNoFactura16, .hlProveedor16, .upProveedor16, .wceSubtotal16, .upSubtotal16, .wceIVA16, .upIVA16, .wceTotal16, .upTotal16, .wpePorcentAut16, .upPorcentAut16)
        End With
    End Sub

    Protected Sub cbTabulador16_CheckedChanged(sender As Object, e As EventArgs) Handles cbTabulador16.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbTabulador16.Checked = True Then
                tipo = "T"
                .lblNoFacturaE16.Visible = False
                If .cbTabulador16.Checked = True And .txtObs16.Text.Trim = "" Then
                    .lblObsE16.Visible = True
                Else
                    .lblObsE16.Visible = False
                End If
            Else
                tipo = "F"
                .lblObsE16.Visible = False
            End If
            .upObsE16.Update()
            tipoFT(tipo, .cbFactura16, .upFactura16, .cbTabulador16, .upTabulador16, .ddlConcepto16, .upConcepto16, .wneNoPers16.Value, .upNoPers16, .wneNoDias16.Value, .upNoDias16, .txtRFC16, .ibtnRFCBus16, .upRFC16, .ddlNoFactura16, .upNoFactura16, .hlProveedor16, .upProveedor16, .wceSubtotal16, .upSubtotal16, .wceIVA16, .upIVA16, .wceTotal16, .upTotal16, .wpePorcentAut16, .upPorcentAut16)
        End With
    End Sub

    Protected Sub ddlConcepto16_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConcepto16.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                If Me.cbFactura16.Checked = True Then
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto_comp where id_concepto_comp = @id_concepto_comp "
                    SCMValores.Parameters.AddWithValue("@id_concepto_comp", ddlConcepto16.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino16.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    actNoFactura(Me.ddlConcepto16.SelectedValue, Me.wneNoPers16.Value, Me.upNoPers16, Me.wneNoDias16.Value, Me.upNoDias16, Me.txtRFC16, Me.ddlNoFactura16, Me.upNoFactura16, Me.hlProveedor16, Me.upProveedor16, Me.wceSubtotal16, Me.upSubtotal16, Me.wceIVA16, Me.upIVA16, Me.wceTotal16, Me.upTotal16, Me.wpePorcentAut16, Me.upPorcentAut16)
                Else
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto where id_concepto = @id_concepto "
                    SCMValores.Parameters.AddWithValue("@id_concepto", ddlConcepto16.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino16.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If
            Catch ex As Exception
                .litError.Text = ex.Message.ToString()
            End Try
        End With
        'If Me.cbFactura16.Checked = True Then
        '    'actFactura(Me.ddlConcepto16.SelectedValue, Me.wneNoPers16.Value, Me.ddlNoFactura16.SelectedValue, Me.hlProveedor16, Me.upProveedor16, Me.wceSubtotal16, Me.upSubtotal16, Me.wceIVA16, Me.upIVA16, Me.wceTotal16, Me.upTotal16)
        '    actNoFactura(Me.ddlConcepto16.SelectedValue, Me.wneNoPers16.Value, Me.upNoPers16, Me.wneNoDias16.Value, Me.upNoDias16, Me.txtRFC16, Me.ddlNoFactura16, Me.upNoFactura16, Me.hlProveedor16, Me.upProveedor16, Me.wceSubtotal16, Me.upSubtotal16, Me.wceIVA16, Me.upIVA16, Me.wceTotal16, Me.upTotal16, Me.wpePorcentAut16, Me.upPorcentAut16)
        'End If
    End Sub

    Protected Sub ibtnRFCBus16_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnRFCBus16.Click
        actNoFactura(Me.ddlConcepto16.SelectedValue, Me.wneNoPers16.Value, Me.upNoPers16, Me.wneNoDias16.Value, Me.upNoDias16, Me.txtRFC16, Me.ddlNoFactura16, Me.upNoFactura16, Me.hlProveedor16, Me.upProveedor16, Me.wceSubtotal16, Me.upSubtotal16, Me.wceIVA16, Me.upIVA16, Me.wceTotal16, Me.upTotal16, Me.wpePorcentAut16, Me.upPorcentAut16)
    End Sub

    Protected Sub ddlNoFactura16_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoFactura16.SelectedIndexChanged
        actFactura(Me.ddlConcepto16.SelectedValue, Me.wneNoPers16.Value, Me.upNoPers16, Me.wneNoDias16.Value, Me.upNoDias16, Me.ddlNoFactura16.SelectedValue, Me.hlProveedor16, Me.upProveedor16, Me.wceSubtotal16, Me.upSubtotal16, Me.wceIVA16, Me.upIVA16, Me.wceTotal16, Me.upTotal16, Me.wpePorcentAut16, Me.upPorcentAut16)
    End Sub

#End Region

#Region "No. 17"

    Protected Sub cbFactura17_CheckedChanged(sender As Object, e As EventArgs) Handles cbFactura17.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbFactura17.Checked = True Then
                tipo = "F"
                .lblObsE17.Visible = False
            Else
                tipo = "T"
                .lblNoFacturaE17.Visible = False
                If .cbTabulador17.Checked = True And .txtObs17.Text.Trim = "" Then
                    .lblObsE17.Visible = True
                Else
                    .lblObsE17.Visible = False
                End If
            End If
            .upObsE17.Update()
            tipoFT(tipo, .cbFactura17, .upFactura17, .cbTabulador17, .upTabulador17, .ddlConcepto17, .upConcepto17, .wneNoPers17.Value, .upNoPers17, .wneNoDias17.Value, .upNoDias17, .txtRFC17, .ibtnRFCBus17, .upRFC17, .ddlNoFactura17, .upNoFactura17, .hlProveedor17, .upProveedor17, .wceSubtotal17, .upSubtotal17, .wceIVA17, .upIVA17, .wceTotal17, .upTotal17, .wpePorcentAut17, .upPorcentAut17)
        End With
    End Sub

    Protected Sub cbTabulador17_CheckedChanged(sender As Object, e As EventArgs) Handles cbTabulador17.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbTabulador17.Checked = True Then
                tipo = "T"
                .lblNoFacturaE17.Visible = False
                If .cbTabulador17.Checked = True And .txtObs17.Text.Trim = "" Then
                    .lblObsE17.Visible = True
                Else
                    .lblObsE17.Visible = False
                End If
            Else
                tipo = "F"
                .lblObsE17.Visible = False
            End If
            .upObsE17.Update()
            tipoFT(tipo, .cbFactura17, .upFactura17, .cbTabulador17, .upTabulador17, .ddlConcepto17, .upConcepto17, .wneNoPers17.Value, .upNoPers17, .wneNoDias17.Value, .upNoDias17, .txtRFC17, .ibtnRFCBus17, .upRFC17, .ddlNoFactura17, .upNoFactura17, .hlProveedor17, .upProveedor17, .wceSubtotal17, .upSubtotal17, .wceIVA17, .upIVA17, .wceTotal17, .upTotal17, .wpePorcentAut17, .upPorcentAut17)
        End With
    End Sub

    Protected Sub ddlConcepto17_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConcepto17.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                If Me.cbFactura17.Checked = True Then
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto_comp where id_concepto_comp = @id_concepto_comp "
                    SCMValores.Parameters.AddWithValue("@id_concepto_comp", ddlConcepto17.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino17.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    actNoFactura(Me.ddlConcepto17.SelectedValue, Me.wneNoPers17.Value, Me.upNoPers17, Me.wneNoDias17.Value, Me.upNoDias17, Me.txtRFC17, Me.ddlNoFactura17, Me.upNoFactura17, Me.hlProveedor17, Me.upProveedor17, Me.wceSubtotal17, Me.upSubtotal17, Me.wceIVA17, Me.upIVA17, Me.wceTotal17, Me.upTotal17, Me.wpePorcentAut17, Me.upPorcentAut17)
                Else
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto where id_concepto = @id_concepto "
                    SCMValores.Parameters.AddWithValue("@id_concepto", ddlConcepto17.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino17.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If
            Catch ex As Exception
                .litError.Text = ex.Message.ToString()
            End Try
        End With
        'If Me.cbFactura17.Checked = True Then
        '    'actFactura(Me.ddlConcepto17.SelectedValue, Me.wneNoPers17.Value, Me.ddlNoFactura17.SelectedValue, Me.hlProveedor17, Me.upProveedor17, Me.wceSubtotal17, Me.upSubtotal17, Me.wceIVA17, Me.upIVA17, Me.wceTotal17, Me.upTotal17)
        '    actNoFactura(Me.ddlConcepto17.SelectedValue, Me.wneNoPers17.Value, Me.upNoPers17, Me.wneNoDias17.Value, Me.upNoDias17, Me.txtRFC17, Me.ddlNoFactura17, Me.upNoFactura17, Me.hlProveedor17, Me.upProveedor17, Me.wceSubtotal17, Me.upSubtotal17, Me.wceIVA17, Me.upIVA17, Me.wceTotal17, Me.upTotal17, Me.wpePorcentAut17, Me.upPorcentAut17)
        'End If
    End Sub

    Protected Sub ibtnRFCBus17_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnRFCBus17.Click
        actNoFactura(Me.ddlConcepto17.SelectedValue, Me.wneNoPers17.Value, Me.upNoPers17, Me.wneNoDias17.Value, Me.upNoDias17, Me.txtRFC17, Me.ddlNoFactura17, Me.upNoFactura17, Me.hlProveedor17, Me.upProveedor17, Me.wceSubtotal17, Me.upSubtotal17, Me.wceIVA17, Me.upIVA17, Me.wceTotal17, Me.upTotal17, Me.wpePorcentAut17, Me.upPorcentAut17)
    End Sub

    Protected Sub ddlNoFactura17_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoFactura17.SelectedIndexChanged
        actFactura(Me.ddlConcepto17.SelectedValue, Me.wneNoPers17.Value, Me.upNoPers17, Me.wneNoDias17.Value, Me.upNoDias17, Me.ddlNoFactura17.SelectedValue, Me.hlProveedor17, Me.upProveedor17, Me.wceSubtotal17, Me.upSubtotal17, Me.wceIVA17, Me.upIVA17, Me.wceTotal17, Me.upTotal17, Me.wpePorcentAut17, Me.upPorcentAut17)
    End Sub

#End Region

#Region "No. 18"

    Protected Sub cbFactura18_CheckedChanged(sender As Object, e As EventArgs) Handles cbFactura18.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbFactura18.Checked = True Then
                tipo = "F"
                .lblObsE18.Visible = False
            Else
                tipo = "T"
                .lblNoFacturaE18.Visible = False
                If .cbTabulador18.Checked = True And .txtObs18.Text.Trim = "" Then
                    .lblObsE18.Visible = True
                Else
                    .lblObsE18.Visible = False
                End If
            End If
            .upObsE18.Update()
            tipoFT(tipo, .cbFactura18, .upFactura18, .cbTabulador18, .upTabulador18, .ddlConcepto18, .upConcepto18, .wneNoPers18.Value, .upNoPers18, .wneNoDias18.Value, .upNoDias18, .txtRFC18, .ibtnRFCBus18, .upRFC18, .ddlNoFactura18, .upNoFactura18, .hlProveedor18, .upProveedor18, .wceSubtotal18, .upSubtotal18, .wceIVA18, .upIVA18, .wceTotal18, .upTotal18, .wpePorcentAut18, .upPorcentAut18)
        End With
    End Sub

    Protected Sub cbTabulador18_CheckedChanged(sender As Object, e As EventArgs) Handles cbTabulador18.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbTabulador18.Checked = True Then
                tipo = "T"
                .lblNoFacturaE18.Visible = False
                If .cbTabulador18.Checked = True And .txtObs18.Text.Trim = "" Then
                    .lblObsE18.Visible = True
                Else
                    .lblObsE18.Visible = False
                End If
            Else
                tipo = "F"
                .lblObsE18.Visible = False
            End If
            .upObsE18.Update()
            tipoFT(tipo, .cbFactura18, .upFactura18, .cbTabulador18, .upTabulador18, .ddlConcepto18, .upConcepto18, .wneNoPers18.Value, .upNoPers18, .upNoDias18, .wneNoDias18.Value, .txtRFC18, .ibtnRFCBus18, .upRFC18, .ddlNoFactura18, .upNoFactura18, .hlProveedor18, .upProveedor18, .wceSubtotal18, .upSubtotal18, .wceIVA18, .upIVA18, .wceTotal18, .upTotal18, .wpePorcentAut18, .upPorcentAut18)
        End With
    End Sub

    Protected Sub ddlConcepto18_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConcepto18.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                If Me.cbFactura18.Checked = True Then
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto_comp where id_concepto_comp = @id_concepto_comp "
                    SCMValores.Parameters.AddWithValue("@id_concepto_comp", ddlConcepto18.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino18.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    actNoFactura(Me.ddlConcepto18.SelectedValue, Me.wneNoPers18.Value, Me.upNoPers18, Me.wneNoDias18.Value, Me.upNoDias18, Me.txtRFC18, Me.ddlNoFactura18, Me.upNoFactura18, Me.hlProveedor18, Me.upProveedor18, Me.wceSubtotal18, Me.upSubtotal18, Me.wceIVA18, Me.upIVA18, Me.wceTotal18, Me.upTotal18, Me.wpePorcentAut18, Me.upPorcentAut18)
                Else
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto where id_concepto = @id_concepto "
                    SCMValores.Parameters.AddWithValue("@id_concepto", ddlConcepto18.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino18.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If
            Catch ex As Exception
                .litError.Text = ex.Message.ToString()
            End Try
        End With
        'If Me.cbFactura18.Checked = True Then
        '    'actFactura(Me.ddlConcepto18.SelectedValue, Me.wneNoPers18.Value, Me.ddlNoFactura18.SelectedValue, Me.hlProveedor18, Me.upProveedor18, Me.wceSubtotal18, Me.upSubtotal18, Me.wceIVA18, Me.upIVA18, Me.wceTotal18, Me.upTotal18)
        '    actNoFactura(Me.ddlConcepto18.SelectedValue, Me.wneNoPers18.Value, Me.upNoPers18, Me.wneNoDias18.Value, Me.upNoDias18, Me.txtRFC18, Me.ddlNoFactura18, Me.upNoFactura18, Me.hlProveedor18, Me.upProveedor18, Me.wceSubtotal18, Me.upSubtotal18, Me.wceIVA18, Me.upIVA18, Me.wceTotal18, Me.upTotal18, Me.wpePorcentAut18, Me.upPorcentAut18)
        'End If
    End Sub

    Protected Sub ibtnRFCBus18_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnRFCBus18.Click
        actNoFactura(Me.ddlConcepto18.SelectedValue, Me.wneNoPers18.Value, Me.upNoPers18, Me.wneNoDias18.Value, Me.upNoDias18, Me.txtRFC18, Me.ddlNoFactura18, Me.upNoFactura18, Me.hlProveedor18, Me.upProveedor18, Me.wceSubtotal18, Me.upSubtotal18, Me.wceIVA18, Me.upIVA18, Me.wceTotal18, Me.upTotal18, Me.wpePorcentAut18, Me.upPorcentAut18)
    End Sub

    Protected Sub ddlNoFactura18_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoFactura18.SelectedIndexChanged
        actFactura(Me.ddlConcepto18.SelectedValue, Me.wneNoPers18.Value, Me.upNoPers18, Me.wneNoDias18.Value, Me.upNoDias18, Me.ddlNoFactura18.SelectedValue, Me.hlProveedor18, Me.upProveedor18, Me.wceSubtotal18, Me.upSubtotal18, Me.wceIVA18, Me.upIVA18, Me.wceTotal18, Me.upTotal18, Me.wpePorcentAut18, Me.upPorcentAut18)
    End Sub

#End Region

#Region "No. 19"

    Protected Sub cbFactura19_CheckedChanged(sender As Object, e As EventArgs) Handles cbFactura19.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbFactura19.Checked = True Then
                tipo = "F"
                .lblObsE19.Visible = False
            Else
                tipo = "T"
                .lblNoFacturaE19.Visible = False
                If .cbTabulador19.Checked = True And .txtObs19.Text.Trim = "" Then
                    .lblObsE19.Visible = True
                Else
                    .lblObsE19.Visible = False
                End If
            End If
            .upObsE19.Update()
            tipoFT(tipo, .cbFactura19, .upFactura19, .cbTabulador19, .upTabulador19, .ddlConcepto19, .upConcepto19, .wneNoPers19.Value, .upNoPers19, .wneNoDias19.Value, .upNoDias19, .txtRFC19, .ibtnRFCBus19, .upRFC19, .ddlNoFactura19, .upNoFactura19, .hlProveedor19, .upProveedor19, .wceSubtotal19, .upSubtotal19, .wceIVA19, .upIVA19, .wceTotal19, .upTotal19, .wpePorcentAut19, .upPorcentAut19)
        End With
    End Sub

    Protected Sub cbTabulador19_CheckedChanged(sender As Object, e As EventArgs) Handles cbTabulador19.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbTabulador19.Checked = True Then
                tipo = "T"
                .lblNoFacturaE19.Visible = False
                If .cbTabulador19.Checked = True And .txtObs19.Text.Trim = "" Then
                    .lblObsE19.Visible = True
                Else
                    .lblObsE19.Visible = False
                End If
            Else
                tipo = "F"
                .lblObsE19.Visible = False
            End If
            .upObsE19.Update()
            tipoFT(tipo, .cbFactura19, .upFactura19, .cbTabulador19, .upTabulador19, .ddlConcepto19, .upConcepto19, .wneNoPers19.Value, .upNoPers19, .wneNoDias19.Value, Me.upNoDias19, .txtRFC19, .ibtnRFCBus19, .upRFC19, .ddlNoFactura19, .upNoFactura19, .hlProveedor19, .upProveedor19, .wceSubtotal19, .upSubtotal19, .wceIVA19, .upIVA19, .wceTotal19, .upTotal19, .wpePorcentAut19, .upPorcentAut19)
        End With
    End Sub

    Protected Sub ddlConcepto19_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConcepto19.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                If Me.cbFactura19.Checked = True Then
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto_comp where id_concepto_comp = @id_concepto_comp "
                    SCMValores.Parameters.AddWithValue("@id_concepto_comp", ddlConcepto19.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino19.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    actNoFactura(Me.ddlConcepto19.SelectedValue, Me.wneNoPers19.Value, Me.upNoPers19, Me.wneNoDias19.Value, Me.upNoDias19, Me.txtRFC19, Me.ddlNoFactura19, Me.upNoFactura19, Me.hlProveedor19, Me.upProveedor19, Me.wceSubtotal19, Me.upSubtotal19, Me.wceIVA19, Me.upIVA19, Me.wceTotal19, Me.upTotal19, Me.wpePorcentAut19, Me.upPorcentAut19)
                Else
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto where id_concepto = @id_concepto "
                    SCMValores.Parameters.AddWithValue("@id_concepto", ddlConcepto19.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino19.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If
            Catch ex As Exception
                .litError.Text = ex.Message.ToString()
            End Try
        End With
        'If Me.cbFactura19.Checked = True Then
        '    'actFactura(Me.ddlConcepto19.SelectedValue, Me.wneNoPers19.Value, Me.ddlNoFactura19.SelectedValue, Me.hlProveedor19, Me.upProveedor19, Me.wceSubtotal19, Me.upSubtotal19, Me.wceIVA19, Me.upIVA19, Me.wceTotal19, Me.upTotal19)
        '    actNoFactura(Me.ddlConcepto19.SelectedValue, Me.wneNoPers19.Value, Me.upNoPers19, Me.wneNoDias19.Value, Me.upNoDias19, Me.txtRFC19, Me.ddlNoFactura19, Me.upNoFactura19, Me.hlProveedor19, Me.upProveedor19, Me.wceSubtotal19, Me.upSubtotal19, Me.wceIVA19, Me.upIVA19, Me.wceTotal19, Me.upTotal19, Me.wpePorcentAut19, Me.upPorcentAut19)
        'End If
    End Sub

    Protected Sub ibtnRFCBus19_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnRFCBus19.Click
        actNoFactura(Me.ddlConcepto19.SelectedValue, Me.wneNoPers19.Value, Me.upNoPers19, Me.wneNoDias19.Value, Me.upNoDias19, Me.txtRFC19, Me.ddlNoFactura19, Me.upNoFactura19, Me.hlProveedor19, Me.upProveedor19, Me.wceSubtotal19, Me.upSubtotal19, Me.wceIVA19, Me.upIVA19, Me.wceTotal19, Me.upTotal19, Me.wpePorcentAut19, Me.upPorcentAut19)
    End Sub

    Protected Sub ddlNoFactura19_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoFactura19.SelectedIndexChanged
        actFactura(Me.ddlConcepto19.SelectedValue, Me.wneNoPers19.Value, Me.upNoPers19, Me.wneNoDias19.Value, Me.upNoDias19, Me.ddlNoFactura19.SelectedValue, Me.hlProveedor19, Me.upProveedor19, Me.wceSubtotal19, Me.upSubtotal19, Me.wceIVA19, Me.upIVA19, Me.wceTotal19, Me.upTotal19, Me.wpePorcentAut19, Me.upPorcentAut19)
    End Sub

#End Region

#Region "No. 20"

    Protected Sub cbFactura20_CheckedChanged(sender As Object, e As EventArgs) Handles cbFactura20.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbFactura20.Checked = True Then
                tipo = "F"
                .lblObsE20.Visible = False
            Else
                tipo = "T"
                .lblNoFacturaE20.Visible = False
                If .cbTabulador20.Checked = True And .txtObs20.Text.Trim = "" Then
                    .lblObsE20.Visible = True
                Else
                    .lblObsE20.Visible = False
                End If
            End If
            .upObsE20.Update()
            tipoFT(tipo, .cbFactura20, .upFactura20, .cbTabulador20, .upTabulador20, .ddlConcepto20, .upConcepto20, .wneNoPers20.Value, .upNoPers20, .wneNoDias20.Value, .upNoDias20, .txtRFC20, .ibtnRFCBus20, .upRFC20, .ddlNoFactura20, .upNoFactura20, .hlProveedor20, .upProveedor20, .wceSubtotal20, .upSubtotal20, .wceIVA20, .upIVA20, .wceTotal20, .upTotal20, .wpePorcentAut20, .upPorcentAut20)
        End With
    End Sub

    Protected Sub cbTabulador20_CheckedChanged(sender As Object, e As EventArgs) Handles cbTabulador20.CheckedChanged
        With Me
            Dim tipo As String = ""
            If .cbTabulador20.Checked = True Then
                tipo = "T"
                .lblNoFacturaE20.Visible = False
                If .cbTabulador20.Checked = True And .txtObs20.Text.Trim = "" Then
                    .lblObsE20.Visible = True
                Else
                    .lblObsE20.Visible = False
                End If
            Else
                tipo = "F"
                .lblObsE20.Visible = False
            End If
            .upObsE20.Update()
            tipoFT(tipo, .cbFactura20, .upFactura20, .cbTabulador20, .upTabulador20, .ddlConcepto20, .upConcepto20, .wneNoPers20.Value, .upNoPers20, .wneNoDias20.Value, .upNoDias20, .txtRFC20, .ibtnRFCBus20, .upRFC20, .ddlNoFactura20, .upNoFactura20, .hlProveedor20, .upProveedor20, .wceSubtotal20, .upSubtotal20, .wceIVA20, .upIVA20, .wceTotal20, .upTotal20, .wpePorcentAut20, .upPorcentAut20)
        End With
    End Sub

    Protected Sub ddlConcepto20_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConcepto20.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                If Me.cbFactura20.Checked = True Then
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto_comp where id_concepto_comp = @id_concepto_comp "
                    SCMValores.Parameters.AddWithValue("@id_concepto_comp", ddlConcepto20.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino20.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    actNoFactura(Me.ddlConcepto20.SelectedValue, Me.wneNoPers20.Value, Me.upNoPers20, Me.wneNoDias20.Value, Me.upNoDias20, Me.txtRFC20, Me.ddlNoFactura20, Me.upNoFactura20, Me.hlProveedor20, Me.upProveedor20, Me.wceSubtotal20, Me.upSubtotal20, Me.wceIVA20, Me.upIVA20, Me.wceTotal20, Me.upTotal20, Me.wpePorcentAut20, Me.upPorcentAut20)
                Else
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto where id_concepto = @id_concepto "
                    SCMValores.Parameters.AddWithValue("@id_concepto", ddlConcepto20.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino20.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If
            Catch ex As Exception
                .litError.Text = ex.Message.ToString()
            End Try
        End With
        'If Me.cbFactura20.Checked = True Then
        '    'actFactura(Me.ddlConcepto20.SelectedValue, Me.wneNoPers20.Value, Me.ddlNoFactura20.SelectedValue, Me.hlProveedor20, Me.upProveedor20, Me.wceSubtotal20, Me.upSubtotal20, Me.wceIVA20, Me.upIVA20, Me.wceTotal20, Me.upTotal20)
        '    actNoFactura(Me.ddlConcepto20.SelectedValue, Me.wneNoPers20.Value, Me.upNoPers20, Me.wneNoDias20.Value, Me.upNoDias20, Me.txtRFC20, Me.ddlNoFactura20, Me.upNoFactura20, Me.hlProveedor20, Me.upProveedor20, Me.wceSubtotal20, Me.upSubtotal20, Me.wceIVA20, Me.upIVA20, Me.wceTotal20, Me.upTotal20, Me.wpePorcentAut20, Me.upPorcentAut20)
        'End If
    End Sub

    Protected Sub ibtnRFCBus20_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnRFCBus20.Click
        actNoFactura(Me.ddlConcepto20.SelectedValue, Me.wneNoPers20.Value, Me.upNoPers20, Me.wneNoDias20.Value, Me.upNoDias20, Me.txtRFC20, Me.ddlNoFactura20, Me.upNoFactura20, Me.hlProveedor20, Me.upProveedor20, Me.wceSubtotal20, Me.upSubtotal20, Me.wceIVA20, Me.upIVA20, Me.wceTotal20, Me.upTotal20, Me.wpePorcentAut20, Me.upPorcentAut20)
    End Sub

    Protected Sub ddlNoFactura20_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoFactura20.SelectedIndexChanged
        actFactura(Me.ddlConcepto20.SelectedValue, Me.wneNoPers20.Value, Me.upNoPers20, Me.wneNoDias20.Value, Me.upNoDias20, Me.ddlNoFactura20.SelectedValue, Me.hlProveedor20, Me.upProveedor20, Me.wceSubtotal20, Me.upSubtotal20, Me.wceIVA20, Me.upIVA20, Me.wceTotal20, Me.upTotal20, Me.wpePorcentAut20, Me.upPorcentAut20)
    End Sub

#End Region

#End Region

#Region "Sumar / Guardar"

    Protected Sub btnSumar_Click(sender As Object, e As EventArgs) Handles btnSumar.Click
        sumarConceptos()
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        With Me
            Try
                sumarConceptos()

                If Val(._txtBan.Text) = 1 Then
                    .litError.Text = "Ya se almacenó la comprobación previamente, favor de validarlo en el apartado de Consulta de Comprobaciones"
                Else
                    If valError() Then
                        .litError.Text = "Favor de validar la información"
                    Else
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
                            If banVale = 0 Then
                                While Val(._txtBan.Text) = 0
                                    Dim fecha As DateTime
                                    fecha = Date.Now
                                    ._txtCargComb.Text = 0

                                    'Vale de Ingreso
                                    ' '' Ruta Local
                                    ''Dim sFileDir As String = "C:/ProcAd - Adjuntos/" 'Ruta en que se almacenará el archivo
                                    ' Ruta en Atenea
                                    Dim sFileDir As String = "D:\ProcAd - Adjuntos\" 'Ruta en que se almacenará el archivo
                                    Dim lMaxFileSize As Long = 10485760 'Equivalente a 10 Megabytes
                                    Dim sFileValeIng As String = ""
                                    If .upValeIng.Visible = True Then
                                        sFileValeIng = System.IO.Path.GetFileName(fuValeIng.PostedFile.FileName)
                                    End If

                                    Dim sFileEvidencia As String = ""
                                    sFileEvidencia = System.IO.Path.GetFileName(fuEvidencia.PostedFile.FileName)

                                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                                    Dim sdaEmpleado As New SqlDataAdapter
                                    Dim dsEmpleado As New DataSet
                                    Dim query As String
                                    query = "select cgEmpl.no_empleado as no_empleadoE " +
                                            "     , cgUsrA.id_usuario as id_usr_aut " +
                                            "     , cgAut.no_empleado as no_empleadoA " +
                                            "     , cgAut.nombre + ' ' + cgAut.ap_paterno + ' ' + cgAut.ap_materno as Autorizador " +
                                            "     , cgUsrD.id_usuario as id_usr_dir " +
                                            "     , cgDir.no_empleado as no_empleadoD " +
                                            "     , cgDir.nombre + ' ' + cgDir.ap_paterno + ' ' + cgDir.ap_materno as Director " +
                                            "from bd_empleado.dbo.cg_empleado cgEmpl " +
                                            "  left join cg_usuario cgUsrE on cgEmpl.id_empleado = cgUsrE.id_empleado " +
                                            "  left join bd_Empleado.dbo.cg_empleado cgAut on cgAut.id_empleado = @idAut " +
                                            "  left join cg_usuario cgUsrA on cgAut.id_empleado = cgUsrA.id_empleado " +
                                            "  left join bd_Empleado.dbo.cg_empleado cgDir on cgDir.id_empleado = @idDir " +
                                            "  left join cg_usuario cgUsrD on cgDir.id_empleado = cgUsrD.id_empleado " +
                                            "where cgUsrE.id_usuario = @idEmpl "
                                    sdaEmpleado.SelectCommand = New SqlCommand(query, ConexionBD)
                                    sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idEmpl", .ddlEmpleado.SelectedValue)
                                    sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idAut", Val(._txtIdAutorizador.Text))
                                    sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idDir", 0)
                                    ConexionBD.Open()
                                    sdaEmpleado.Fill(dsEmpleado)
                                    ConexionBD.Close()

                                    'Insertar Comprobación
                                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                                    SCMValores.Connection = ConexionBD
                                    SCMValores.Parameters.Clear()
                                    SCMValores.CommandText = "insert into ms_comp ( id_usr_solicita,  fecha_solicita,  id_usr_autoriza,  empresa,  periodo_comp,  periodo_ini,  periodo_fin,  tipo_gasto,  tipo_actividad,  centro_costo,  division,  no_empleado,  empleado,  no_autorizador,  autorizador,  justificacion,  importe_tot,  vale_ingreso,  vale_ingreso_imp,  vale_ingreso_adj,  evidencia_adj,  aut_dir,  id_usr_aut_dir,  no_director,  director, status) " +
                                                             " 			   values (@id_usr_solicita, @fecha_solicita, @id_usr_autoriza, @empresa, @periodo_comp, @periodo_ini, @periodo_fin, @tipo_gasto, @tipo_actividad, @centro_costo, @division, @no_empleado, @empleado, @no_autorizador, @autorizador, @justificacion, @importe_tot, @vale_ingreso, @vale_ingreso_imp, @vale_ingreso_adj, @evidencia_adj, @aut_dir, @id_usr_aut_dir, @no_director, @director, 'P') "
                                    SCMValores.Parameters.AddWithValue("@id_usr_solicita", Val(._txtIdUsuario.Text))
                                    SCMValores.Parameters.AddWithValue("@fecha_solicita", fecha)
                                    SCMValores.Parameters.AddWithValue("@id_usr_autoriza", Val(dsEmpleado.Tables(0).Rows(0).Item("id_usr_aut").ToString()))
                                    SCMValores.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedItem.Text)
                                    SCMValores.Parameters.AddWithValue("@periodo_comp", "Del " + .wdteFechaIni.Text + " al " + .wdteFechaFin.Text)
                                    SCMValores.Parameters.AddWithValue("@periodo_ini", .wdteFechaIni.Date)
                                    SCMValores.Parameters.AddWithValue("@periodo_fin", .wdteFechaFin.Date)
                                    SCMValores.Parameters.AddWithValue("@tipo_gasto", .ddlTipoGasto.SelectedItem.Text)
                                    SCMValores.Parameters.AddWithValue("@tipo_actividad", .ddlTipoAct.SelectedItem.Text)
                                    If .ddlCC.Visible = True Then
                                        SCMValores.Parameters.AddWithValue("@centro_costo", .ddlCC.SelectedItem.Text)
                                    Else
                                        SCMValores.Parameters.AddWithValue("@centro_costo", DBNull.Value)
                                    End If
                                    If .ddlDiv.Visible = True Then
                                        SCMValores.Parameters.AddWithValue("@division", .ddlDiv.SelectedItem.Text)
                                    Else
                                        SCMValores.Parameters.AddWithValue("@division", DBNull.Value)
                                    End If
                                    SCMValores.Parameters.AddWithValue("@no_empleado", dsEmpleado.Tables(0).Rows(0).Item("no_empleadoE").ToString())
                                    SCMValores.Parameters.AddWithValue("@empleado", .ddlEmpleado.SelectedItem.Text)
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
                                    SCMValores.Parameters.AddWithValue("@aut_dir", "N")
                                    SCMValores.Parameters.AddWithValue("@id_usr_aut_dir", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@no_director", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@director", DBNull.Value)

                                    ConexionBD.Open()
                                    SCMValores.ExecuteNonQuery()
                                    ConexionBD.Close()
                                    sdaEmpleado.Dispose()
                                    dsEmpleado.Dispose()

                                    'Obtener ID de la Comprobación
                                    SCMValores.CommandText = "select max(id_ms_comp) from ms_comp where id_usr_solicita = @id_usr_solicita and status not in ('Z') "
                                    ConexionBD.Open()
                                    .lblFolio.Text = SCMValores.ExecuteScalar
                                    ConexionBD.Close()
                                    If Val(.lblFolio.Text) > 0 Then
                                        ._txtBan.Text = 1
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
                                    SCMValores.CommandText = "insert into dt_comp ( id_ms_comp,  fecha_realizo,  tipo,  no_factura,  id_concepto,  nombre_concepto,  iva,  no_personas,  no_dias,  monto_subtotal,  monto_iva,  monto_total,  rfc,  proveedor,  origen_destino,  vehiculo,  obs,  id_lugar_orig,  lugar_orig,  id_lugar_dest,  lugar_dest,  CFDI) " +
                                                             " 			   values (@id_ms_comp, @fecha_realizo, @tipo, @no_factura, @id_concepto, @nombre_concepto, @iva, @no_personas, @no_dias, @monto_subtotal, @monto_iva, @monto_total, @rfc, @proveedor, @origen_destino, @vehiculo, @obs, @id_lugar_orig, @lugar_orig, @id_lugar_dest, @lugar_dest, @CFDI)"
                                    SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                                    SCMValores.Parameters.Add("@fecha_realizo", SqlDbType.Date)
                                    SCMValores.Parameters.Add("@tipo", SqlDbType.VarChar)
                                    SCMValores.Parameters.Add("@CFDI", SqlDbType.VarChar)
                                    SCMValores.Parameters.Add("@no_factura", SqlDbType.VarChar)
                                    SCMValores.Parameters.Add("@rfc", SqlDbType.VarChar)
                                    SCMValores.Parameters.Add("@proveedor", SqlDbType.VarChar)
                                    SCMValores.Parameters.Add("@iva", SqlDbType.Decimal)
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
                                    'Datos de la Factura
                                    Dim sdaFactura As New SqlDataAdapter
                                    Dim dsFactura As New DataSet
                                    sdaFactura.SelectCommand = New SqlCommand("select uuid as cfdi " +
                                                                              "     , serie + ' ' + folio as no_factura " +
                                                                              "     , rfc_emisor as rfc " +
                                                                              "     , razon_emisor as proveedor " +
                                                                              "from dt_factura " +
                                                                              "where id_dt_factura = @id_dt_factura ", ConexionBD)
                                    sdaFactura.SelectCommand.Parameters.Add("@id_dt_factura", SqlDbType.Int)
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
                                                                    "where CFDI = @uuid " +
                                                                    "  and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') "
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
                                                                                 "  and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA')", ConexionBD)
                                    sdaImpFactura.SelectCommand.Parameters.Add("@uuid", SqlDbType.VarChar)

                                    'Insertar Carga de Combustible por Comprobar
                                    Dim SCMCargaComb As SqlCommand = New System.Data.SqlClient.SqlCommand
                                    SCMCargaComb.Connection = ConexionBD
                                    SCMCargaComb.Parameters.Clear()
                                    SCMCargaComb.CommandText = "insert into dt_carga_comb_tar ( id_usr_carga,  fecha_carga,  empresa,  centro_costos,  importe_con_ieps,  importe_sin_imp,  ieps,  iva,  importe_transaccion,  porcent_iva,  cantidad_mercancia,  precio_ticket,  precio_sin_iva,  id_conductor,  conductor,  razon_social_afiliado,  rfc,  obs,  status) " +
                                                               " 			           values (@id_usr_carga, @fecha_carga, @empresa, @centro_costos, @importe_con_ieps, @importe_sin_imp, @ieps, @iva, @importe_transaccion, @porcent_iva, @cantidad_mercancia, @precio_ticket, @precio_sin_iva, @id_conductor, @conductor, @razon_social_afiliado, @rfc, @obs,  'P') "
                                    SCMCargaComb.Parameters.AddWithValue("@id_usr_carga", .ddlEmpleado.SelectedValue)
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
                                    SCMCargaComb.Parameters.AddWithValue("@conductor", .ddlEmpleado.SelectedItem.Text)
                                    SCMCargaComb.Parameters.Add("@razon_social_afiliado", SqlDbType.VarChar)
                                    SCMCargaComb.Parameters.Add("@rfc", SqlDbType.VarChar)
                                    SCMCargaComb.Parameters.AddWithValue("@obs", .lblFolio.Text)

                                    Dim idDtComp As Integer = 0
                                    'Insertar en dt_comp_linea
                                    Dim SCMValoresDtCompL As SqlCommand = New System.Data.SqlClient.SqlCommand
                                    SCMValoresDtCompL.Connection = ConexionBD
                                    SCMValoresDtCompL.Parameters.Clear()
                                    SCMValoresDtCompL.CommandText = "insert into dt_comp_linea (id_dt_comp, importe, descuento, tasa_iva, iva, tasa_ieps, ieps) " +
                                                                    "select @idDtComp as id_dt_comp " +
                                                                    "     , importe * @porcentAut as importe " +
                                                                    "	  , descuento * @porcentAut as descuento " +
                                                                    "	  , case when impuesto_tras_1 = 2 and tasa_tras_1 = 0.16 or impuesto_tras_2 = 2 and tasa_tras_2 = 0.16 or impuesto_tras_3 = 2 and tasa_tras_3 = 0.16 or impuesto_tras_4 = 2 and tasa_tras_4 = 0.16 then 0.16  " +
                                                                    "		     when impuesto_tras_1 = 2 and tasa_tras_1 = 0.08 or impuesto_tras_2 = 2 and tasa_tras_2 = 0.08 or impuesto_tras_3 = 2 and tasa_tras_3 = 0.08 or impuesto_tras_4 = 2 and tasa_tras_4 = 0.08 then 0.08 " +
                                                                    "		     when impuesto_tras_1 = 2 and tasa_tras_1 = 0 or impuesto_tras_2 = 2 and tasa_tras_2 = 0 or impuesto_tras_3 = 2 and tasa_tras_3 = 0 or impuesto_tras_4 = 2 and tasa_tras_4 = 0 then 0 " +
                                                                    "	    end as tasa_iva " +
                                                                    "	  , isnull(tot_tras_iva, 0) * @porcentAut as iva " +
                                                                    "	  , case when impuesto_tras_1 = 3 then tasa_tras_1 " +
                                                                    "		     when impuesto_tras_2 = 3 then tasa_tras_2 " +
                                                                    "		     when impuesto_tras_3 = 3 then tasa_tras_3 " +
                                                                    "			 when impuesto_tras_4 = 3 then tasa_tras_4 " +
                                                                    "	    end as tasa_ieps " +
                                                                    "	  , isnull(tot_tras_ieps, 0) * @porcentAut as ieps " +
                                                                    "from dt_factura_linea " +
                                                                    "where uuid = @uuid " +
                                                                    "  and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') "
                                    SCMValoresDtCompL.Parameters.Add("@idDtComp", SqlDbType.Int)
                                    SCMValoresDtCompL.Parameters.Add("@porcentAut", SqlDbType.Decimal)
                                    SCMValoresDtCompL.Parameters.Add("@uuid", SqlDbType.VarChar)

                                    'Concepto 1
                                    SCMValores.Parameters("@fecha_realizo").Value = .wdpFecha1.Date
                                    SCMValores.Parameters("@id_concepto").Value = .ddlConcepto1.SelectedValue
                                    SCMValores.Parameters("@nombre_concepto").Value = .ddlConcepto1.SelectedItem.Text
                                    SCMValores.Parameters("@no_personas").Value = .wneNoPers1.Value
                                    SCMValores.Parameters("@no_dias").Value = .wneNoDias1.Value
                                    SCMValores.Parameters("@monto_subtotal").Value = .wceSubtotal1.Value
                                    SCMValores.Parameters("@monto_iva").Value = .wceIVA1.Value
                                    SCMValores.Parameters("@monto_total").Value = .wceTotal1.Value
                                    If .txtOriDes1.Text.Trim = "" Then
                                        SCMValores.Parameters("@origen_destino").Value = DBNull.Value
                                        SCMValores.Parameters("@id_lugar_orig").Value = DBNull.Value
                                        SCMValores.Parameters("@lugar_orig").Value = DBNull.Value
                                        SCMValores.Parameters("@id_lugar_dest").Value = DBNull.Value
                                        SCMValores.Parameters("@lugar_dest").Value = DBNull.Value
                                    Else
                                        SCMValores.Parameters("@origen_destino").Value = .txtOriDes1.Text.Trim
                                        SCMValores.Parameters("@id_lugar_orig").Value = .ddlOrig1.SelectedValue
                                        SCMValores.Parameters("@lugar_orig").Value = .ddlOrig1.SelectedItem.Text
                                        SCMValores.Parameters("@id_lugar_dest").Value = .ddlDest1.SelectedValue
                                        SCMValores.Parameters("@lugar_dest").Value = .ddlDest1.SelectedItem.Text
                                    End If
                                    If .txtVehi1.Text.Trim = "" Then
                                        SCMValores.Parameters("@vehiculo").Value = DBNull.Value
                                    Else
                                        SCMValores.Parameters("@vehiculo").Value = .txtVehi1.Text.Trim
                                    End If
                                    If .txtObs1.Text.Trim = "" Then
                                        SCMValores.Parameters("@obs").Value = DBNull.Value
                                    Else
                                        SCMValores.Parameters("@obs").Value = .txtObs1.Text.Trim
                                    End If
                                    If .cbFactura1.Checked = True Then
                                        SCMValores.Parameters("@tipo").Value = "F"
                                        'Obtener datos de la Factura
                                        sdaFactura.SelectCommand.Parameters("@id_dt_factura").Value = .ddlNoFactura1.SelectedValue
                                        ConexionBD.Open()
                                        sdaFactura.Fill(dsFactura)
                                        ConexionBD.Close()
                                        'Complementar Parámetros
                                        SCMValores.Parameters("@CFDI").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                        SCMValores.Parameters("@no_factura").Value = dsFactura.Tables(0).Rows(0).Item("no_factura").ToString()
                                        SCMValores.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                        SCMValores.Parameters("@proveedor").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                        'Obtener el % de IVA del Concepto
                                        SCMValoresIVA.Parameters("@id_concepto").Value = .ddlConcepto1.SelectedValue
                                        ConexionBD.Open()
                                        SCMValores.Parameters("@iva").Value = SCMValoresIVA.ExecuteScalar
                                        ConexionBD.Close()
                                        'Actualizar Status de Factura
                                        SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura1.SelectedValue
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
                                    End If
                                    ConexionBD.Open()
                                    SCMValores.ExecuteNonQuery()
                                    ConexionBD.Close()
                                    If .cbFactura1.Checked = True Then
                                        'Obtener el id_dt_comp
                                        SCMValoresDtComp.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                        ConexionBD.Open()
                                        idDtComp = SCMValoresDtComp.ExecuteScalar()
                                        ConexionBD.Close()
                                        'Insertar Detalle de Factura
                                        SCMValoresDtCompL.Parameters("@idDtComp").Value = idDtComp
                                        SCMValoresDtCompL.Parameters("@porcentAut").Value = .wpePorcentAut1.Value
                                        SCMValoresDtCompL.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                        ConexionBD.Open()
                                        SCMValoresDtCompL.ExecuteNonQuery()
                                        ConexionBD.Close()

                                        'Validar si es un concepto de Combustible
                                        SCMContComb.Parameters("@id_concepto_comp").Value = .ddlConcepto1.SelectedValue
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
                                            SCMCargaComb.Parameters("@importe_transaccion").Value = .wceTotal1.Value
                                            SCMCargaComb.Parameters("@porcent_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("porcent_iva").ToString())
                                            SCMCargaComb.Parameters("@cantidad_mercancia").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("cantidad").ToString())
                                            SCMCargaComb.Parameters("@precio_ticket").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_ticket").ToString())
                                            SCMCargaComb.Parameters("@precio_sin_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_sin_iva").ToString())
                                            SCMCargaComb.Parameters("@razon_social_afiliado").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                            SCMCargaComb.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                            ConexionBD.Open()
                                            SCMCargaComb.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            ._txtCargComb.Text = Val(._txtCargComb.Text) + 1
                                        End If
                                    End If
                                    'Concepto 2
                                    If .ddlNoConceptos.SelectedValue >= 2 Then
                                        SCMValores.Parameters("@fecha_realizo").Value = .wdpFecha2.Date
                                        SCMValores.Parameters("@id_concepto").Value = .ddlConcepto2.SelectedValue
                                        SCMValores.Parameters("@nombre_concepto").Value = .ddlConcepto2.SelectedItem.Text
                                        SCMValores.Parameters("@no_personas").Value = .wneNoPers2.Value
                                        SCMValores.Parameters("@no_dias").Value = .wneNoDias2.Value
                                        SCMValores.Parameters("@monto_subtotal").Value = .wceSubtotal2.Value
                                        SCMValores.Parameters("@monto_iva").Value = .wceIVA2.Value
                                        SCMValores.Parameters("@monto_total").Value = .wceTotal2.Value
                                        If .txtOriDes2.Text.Trim = "" Then
                                            SCMValores.Parameters("@origen_destino").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_dest").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_dest").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@origen_destino").Value = .txtOriDes2.Text.Trim
                                            SCMValores.Parameters("@id_lugar_orig").Value = .ddlOrig2.SelectedValue
                                            SCMValores.Parameters("@lugar_orig").Value = .ddlOrig2.SelectedItem.Text
                                            SCMValores.Parameters("@id_lugar_dest").Value = .ddlDest2.SelectedValue
                                            SCMValores.Parameters("@lugar_dest").Value = .ddlDest2.SelectedItem.Text
                                        End If
                                        If .txtVehi2.Text.Trim = "" Then
                                            SCMValores.Parameters("@vehiculo").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@vehiculo").Value = .txtVehi2.Text.Trim
                                        End If
                                        If .txtObs2.Text.Trim = "" Then
                                            SCMValores.Parameters("@obs").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@obs").Value = .txtObs2.Text.Trim
                                        End If
                                        If .cbFactura2.Checked = True Then
                                            SCMValores.Parameters("@tipo").Value = "F"
                                            'Obtener datos de la Factura
                                            dsFactura.Clear()
                                            sdaFactura.SelectCommand.Parameters("@id_dt_factura").Value = .ddlNoFactura2.SelectedValue
                                            ConexionBD.Open()
                                            sdaFactura.Fill(dsFactura)
                                            ConexionBD.Close()
                                            'Complementar Parámetros
                                            SCMValores.Parameters("@CFDI").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            SCMValores.Parameters("@no_factura").Value = dsFactura.Tables(0).Rows(0).Item("no_factura").ToString()
                                            SCMValores.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                            SCMValores.Parameters("@proveedor").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                            'Obtener el % de IVA del Concepto
                                            SCMValoresIVA.Parameters("@id_concepto").Value = .ddlConcepto2.SelectedValue
                                            ConexionBD.Open()
                                            SCMValores.Parameters("@iva").Value = SCMValoresIVA.ExecuteScalar
                                            ConexionBD.Close()
                                            'Actualizar Status de Factura
                                            SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura2.SelectedValue
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
                                        End If
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()
                                        If .cbFactura2.Checked = True Then
                                            'Obtener el id_dt_comp
                                            SCMValoresDtComp.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            idDtComp = SCMValoresDtComp.ExecuteScalar()
                                            ConexionBD.Close()
                                            'Insertar Detalle de Factura
                                            SCMValoresDtCompL.Parameters("@idDtComp").Value = idDtComp
                                            SCMValoresDtCompL.Parameters("@porcentAut").Value = .wpePorcentAut2.Value
                                            SCMValoresDtCompL.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            SCMValoresDtCompL.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            'Validar si es un concepto de Combustible
                                            SCMContComb.Parameters("@id_concepto_comp").Value = .ddlConcepto2.SelectedValue
                                            ConexionBD.Open()
                                            contComb = SCMContComb.ExecuteScalar
                                            ConexionBD.Close()
                                            If contComb = 1 Then
                                                'Obtener importes de la Factura
                                                dsImpFactura.Clear()
                                                sdaImpFactura.SelectCommand.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                                ConexionBD.Open()
                                                sdaImpFactura.Fill(dsImpFactura)
                                                ConexionBD.Close()

                                                'Insertar Carga de Combustible por Comprobar
                                                SCMCargaComb.Parameters("@importe_con_ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                                                SCMCargaComb.Parameters("@importe_sin_imp").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("subtotal").ToString())
                                                SCMCargaComb.Parameters("@ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_ieps").ToString())
                                                SCMCargaComb.Parameters("@iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_iva").ToString())
                                                SCMCargaComb.Parameters("@importe_transaccion").Value = .wceTotal2.Value
                                                SCMCargaComb.Parameters("@porcent_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("porcent_iva").ToString())
                                                SCMCargaComb.Parameters("@cantidad_mercancia").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("cantidad").ToString())
                                                SCMCargaComb.Parameters("@precio_ticket").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_ticket").ToString())
                                                SCMCargaComb.Parameters("@precio_sin_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_sin_iva").ToString())
                                                SCMCargaComb.Parameters("@razon_social_afiliado").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                                SCMCargaComb.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                                ConexionBD.Open()
                                                SCMCargaComb.ExecuteNonQuery()
                                                ConexionBD.Close()

                                                ._txtCargComb.Text = Val(._txtCargComb.Text) + 1
                                            End If
                                        End If
                                    End If
                                    'Concepto 3
                                    If .ddlNoConceptos.SelectedValue >= 3 Then
                                        SCMValores.Parameters("@fecha_realizo").Value = .wdpFecha3.Date
                                        SCMValores.Parameters("@id_concepto").Value = .ddlConcepto3.SelectedValue
                                        SCMValores.Parameters("@nombre_concepto").Value = .ddlConcepto3.SelectedItem.Text
                                        SCMValores.Parameters("@no_personas").Value = .wneNoPers3.Value
                                        SCMValores.Parameters("@no_dias").Value = .wneNoDias3.Value
                                        SCMValores.Parameters("@monto_subtotal").Value = .wceSubtotal3.Value
                                        SCMValores.Parameters("@monto_iva").Value = .wceIVA3.Value
                                        SCMValores.Parameters("@monto_total").Value = .wceTotal3.Value
                                        If .txtOriDes3.Text.Trim = "" Then
                                            SCMValores.Parameters("@origen_destino").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_dest").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_dest").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@origen_destino").Value = .txtOriDes3.Text.Trim
                                            SCMValores.Parameters("@id_lugar_orig").Value = .ddlOrig3.SelectedValue
                                            SCMValores.Parameters("@lugar_orig").Value = .ddlOrig3.SelectedItem.Text
                                            SCMValores.Parameters("@id_lugar_dest").Value = .ddlDest3.SelectedValue
                                            SCMValores.Parameters("@lugar_dest").Value = .ddlDest3.SelectedItem.Text
                                        End If
                                        If .txtVehi3.Text.Trim = "" Then
                                            SCMValores.Parameters("@vehiculo").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@vehiculo").Value = .txtVehi3.Text.Trim
                                        End If
                                        If .txtObs3.Text.Trim = "" Then
                                            SCMValores.Parameters("@obs").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@obs").Value = .txtObs3.Text.Trim
                                        End If
                                        If .cbFactura3.Checked = True Then
                                            SCMValores.Parameters("@tipo").Value = "F"
                                            'Obtener datos de la Factura
                                            dsFactura.Clear()
                                            sdaFactura.SelectCommand.Parameters("@id_dt_factura").Value = .ddlNoFactura3.SelectedValue
                                            ConexionBD.Open()
                                            sdaFactura.Fill(dsFactura)
                                            ConexionBD.Close()
                                            'Complementar Parámetros
                                            SCMValores.Parameters("@CFDI").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            SCMValores.Parameters("@no_factura").Value = dsFactura.Tables(0).Rows(0).Item("no_factura").ToString()
                                            SCMValores.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                            SCMValores.Parameters("@proveedor").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                            'Obtener el % de IVA del Concepto
                                            SCMValoresIVA.Parameters("@id_concepto").Value = .ddlConcepto3.SelectedValue
                                            ConexionBD.Open()
                                            SCMValores.Parameters("@iva").Value = SCMValoresIVA.ExecuteScalar
                                            ConexionBD.Close()
                                            'Actualizar Status de Factura
                                            SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura3.SelectedValue
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
                                        End If
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()
                                        If .cbFactura3.Checked = True Then
                                            'Obtener el id_dt_comp
                                            SCMValoresDtComp.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            idDtComp = SCMValoresDtComp.ExecuteScalar()
                                            ConexionBD.Close()
                                            'Insertar Detalle de Factura
                                            SCMValoresDtCompL.Parameters("@idDtComp").Value = idDtComp
                                            SCMValoresDtCompL.Parameters("@porcentAut").Value = .wpePorcentAut3.Value
                                            SCMValoresDtCompL.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            SCMValoresDtCompL.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            'Validar si es un concepto de Combustible
                                            SCMContComb.Parameters("@id_concepto_comp").Value = .ddlConcepto3.SelectedValue
                                            ConexionBD.Open()
                                            contComb = SCMContComb.ExecuteScalar
                                            ConexionBD.Close()
                                            If contComb = 1 Then
                                                'Obtener importes de la Factura
                                                dsImpFactura.Clear()
                                                sdaImpFactura.SelectCommand.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                                ConexionBD.Open()
                                                sdaImpFactura.Fill(dsImpFactura)
                                                ConexionBD.Close()

                                                'Insertar Carga de Combustible por Comprobar
                                                SCMCargaComb.Parameters("@importe_con_ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                                                SCMCargaComb.Parameters("@importe_sin_imp").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("subtotal").ToString())
                                                SCMCargaComb.Parameters("@ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_ieps").ToString())
                                                SCMCargaComb.Parameters("@iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_iva").ToString())
                                                SCMCargaComb.Parameters("@importe_transaccion").Value = .wceTotal3.Value
                                                SCMCargaComb.Parameters("@porcent_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("porcent_iva").ToString())
                                                SCMCargaComb.Parameters("@cantidad_mercancia").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("cantidad").ToString())
                                                SCMCargaComb.Parameters("@precio_ticket").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_ticket").ToString())
                                                SCMCargaComb.Parameters("@precio_sin_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_sin_iva").ToString())
                                                SCMCargaComb.Parameters("@razon_social_afiliado").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                                SCMCargaComb.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                                ConexionBD.Open()
                                                SCMCargaComb.ExecuteNonQuery()
                                                ConexionBD.Close()

                                                ._txtCargComb.Text = Val(._txtCargComb.Text) + 1
                                            End If
                                        End If
                                    End If
                                    'Concepto 4
                                    If .ddlNoConceptos.SelectedValue >= 4 Then
                                        SCMValores.Parameters("@fecha_realizo").Value = .wdpFecha4.Date
                                        SCMValores.Parameters("@id_concepto").Value = .ddlConcepto4.SelectedValue
                                        SCMValores.Parameters("@nombre_concepto").Value = .ddlConcepto4.SelectedItem.Text
                                        SCMValores.Parameters("@no_personas").Value = .wneNoPers4.Value
                                        SCMValores.Parameters("@no_dias").Value = .wneNoDias4.Value
                                        SCMValores.Parameters("@monto_subtotal").Value = .wceSubtotal4.Value
                                        SCMValores.Parameters("@monto_iva").Value = .wceIVA4.Value
                                        SCMValores.Parameters("@monto_total").Value = .wceTotal4.Value
                                        If .txtOriDes4.Text.Trim = "" Then
                                            SCMValores.Parameters("@origen_destino").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_dest").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_dest").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@origen_destino").Value = .txtOriDes4.Text.Trim
                                            SCMValores.Parameters("@id_lugar_orig").Value = .ddlOrig4.SelectedValue
                                            SCMValores.Parameters("@lugar_orig").Value = .ddlOrig4.SelectedItem.Text
                                            SCMValores.Parameters("@id_lugar_dest").Value = .ddlDest4.SelectedValue
                                            SCMValores.Parameters("@lugar_dest").Value = .ddlDest4.SelectedItem.Text
                                        End If
                                        If .txtVehi4.Text.Trim = "" Then
                                            SCMValores.Parameters("@vehiculo").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@vehiculo").Value = .txtVehi4.Text.Trim
                                        End If
                                        If .txtObs4.Text.Trim = "" Then
                                            SCMValores.Parameters("@obs").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@obs").Value = .txtObs4.Text.Trim
                                        End If
                                        If .cbFactura4.Checked = True Then
                                            SCMValores.Parameters("@tipo").Value = "F"
                                            'Obtener datos de la Factura
                                            dsFactura.Clear()
                                            sdaFactura.SelectCommand.Parameters("@id_dt_factura").Value = .ddlNoFactura4.SelectedValue
                                            ConexionBD.Open()
                                            sdaFactura.Fill(dsFactura)
                                            ConexionBD.Close()
                                            'Complementar Parámetros
                                            SCMValores.Parameters("@CFDI").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            SCMValores.Parameters("@no_factura").Value = dsFactura.Tables(0).Rows(0).Item("no_factura").ToString()
                                            SCMValores.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                            SCMValores.Parameters("@proveedor").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                            'Obtener el % de IVA del Concepto
                                            SCMValoresIVA.Parameters("@id_concepto").Value = .ddlConcepto4.SelectedValue
                                            ConexionBD.Open()
                                            SCMValores.Parameters("@iva").Value = SCMValoresIVA.ExecuteScalar
                                            ConexionBD.Close()
                                            'Actualizar Status de Factura
                                            SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura4.SelectedValue
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
                                        End If
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()
                                        If .cbFactura4.Checked = True Then
                                            'Obtener el id_dt_comp
                                            SCMValoresDtComp.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            idDtComp = SCMValoresDtComp.ExecuteScalar()
                                            ConexionBD.Close()
                                            'Insertar Detalle de Factura
                                            SCMValoresDtCompL.Parameters("@idDtComp").Value = idDtComp
                                            SCMValoresDtCompL.Parameters("@porcentAut").Value = .wpePorcentAut4.Value
                                            SCMValoresDtCompL.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            SCMValoresDtCompL.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            'Validar si es un concepto de Combustible
                                            SCMContComb.Parameters("@id_concepto_comp").Value = .ddlConcepto4.SelectedValue
                                            ConexionBD.Open()
                                            contComb = SCMContComb.ExecuteScalar
                                            ConexionBD.Close()
                                            If contComb = 1 Then
                                                'Obtener importes de la Factura
                                                dsImpFactura.Clear()
                                                sdaImpFactura.SelectCommand.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                                ConexionBD.Open()
                                                sdaImpFactura.Fill(dsImpFactura)
                                                ConexionBD.Close()

                                                'Insertar Carga de Combustible por Comprobar
                                                SCMCargaComb.Parameters("@importe_con_ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                                                SCMCargaComb.Parameters("@importe_sin_imp").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("subtotal").ToString())
                                                SCMCargaComb.Parameters("@ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_ieps").ToString())
                                                SCMCargaComb.Parameters("@iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_iva").ToString())
                                                SCMCargaComb.Parameters("@importe_transaccion").Value = .wceTotal4.Value
                                                SCMCargaComb.Parameters("@porcent_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("porcent_iva").ToString())
                                                SCMCargaComb.Parameters("@cantidad_mercancia").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("cantidad").ToString())
                                                SCMCargaComb.Parameters("@precio_ticket").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_ticket").ToString())
                                                SCMCargaComb.Parameters("@precio_sin_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_sin_iva").ToString())
                                                SCMCargaComb.Parameters("@razon_social_afiliado").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                                SCMCargaComb.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                                ConexionBD.Open()
                                                SCMCargaComb.ExecuteNonQuery()
                                                ConexionBD.Close()

                                                ._txtCargComb.Text = Val(._txtCargComb.Text) + 1
                                            End If
                                        End If
                                    End If
                                    'Concepto 5
                                    If .ddlNoConceptos.SelectedValue >= 5 Then
                                        SCMValores.Parameters("@fecha_realizo").Value = .wdpFecha5.Date
                                        SCMValores.Parameters("@id_concepto").Value = .ddlConcepto5.SelectedValue
                                        SCMValores.Parameters("@nombre_concepto").Value = .ddlConcepto5.SelectedItem.Text
                                        SCMValores.Parameters("@no_personas").Value = .wneNoPers5.Value
                                        SCMValores.Parameters("@no_dias").Value = .wneNoDias5.Value
                                        SCMValores.Parameters("@monto_subtotal").Value = .wceSubtotal5.Value
                                        SCMValores.Parameters("@monto_iva").Value = .wceIVA5.Value
                                        SCMValores.Parameters("@monto_total").Value = .wceTotal5.Value
                                        If .txtOriDes5.Text.Trim = "" Then
                                            SCMValores.Parameters("@origen_destino").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_dest").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_dest").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@origen_destino").Value = .txtOriDes5.Text.Trim
                                            SCMValores.Parameters("@id_lugar_orig").Value = .ddlOrig5.SelectedValue
                                            SCMValores.Parameters("@lugar_orig").Value = .ddlOrig5.SelectedItem.Text
                                            SCMValores.Parameters("@id_lugar_dest").Value = .ddlDest5.SelectedValue
                                            SCMValores.Parameters("@lugar_dest").Value = .ddlDest5.SelectedItem.Text
                                        End If
                                        If .txtVehi5.Text.Trim = "" Then
                                            SCMValores.Parameters("@vehiculo").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@vehiculo").Value = .txtVehi5.Text.Trim
                                        End If
                                        If .txtObs5.Text.Trim = "" Then
                                            SCMValores.Parameters("@obs").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@obs").Value = .txtObs5.Text.Trim
                                        End If
                                        If .cbFactura5.Checked = True Then
                                            SCMValores.Parameters("@tipo").Value = "F"
                                            'Obtener datos de la Factura
                                            dsFactura.Clear()
                                            sdaFactura.SelectCommand.Parameters("@id_dt_factura").Value = .ddlNoFactura5.SelectedValue
                                            ConexionBD.Open()
                                            sdaFactura.Fill(dsFactura)
                                            ConexionBD.Close()
                                            'Complementar Parámetros
                                            SCMValores.Parameters("@CFDI").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            SCMValores.Parameters("@no_factura").Value = dsFactura.Tables(0).Rows(0).Item("no_factura").ToString()
                                            SCMValores.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                            SCMValores.Parameters("@proveedor").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                            'Obtener el % de IVA del Concepto
                                            SCMValoresIVA.Parameters("@id_concepto").Value = .ddlConcepto5.SelectedValue
                                            ConexionBD.Open()
                                            SCMValores.Parameters("@iva").Value = SCMValoresIVA.ExecuteScalar
                                            ConexionBD.Close()
                                            'Actualizar Status de Factura
                                            SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura5.SelectedValue
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
                                        End If
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()
                                        If .cbFactura5.Checked = True Then
                                            'Obtener el id_dt_comp
                                            SCMValoresDtComp.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            idDtComp = SCMValoresDtComp.ExecuteScalar()
                                            ConexionBD.Close()
                                            'Insertar Detalle de Factura
                                            SCMValoresDtCompL.Parameters("@idDtComp").Value = idDtComp
                                            SCMValoresDtCompL.Parameters("@porcentAut").Value = .wpePorcentAut5.Value
                                            SCMValoresDtCompL.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            SCMValoresDtCompL.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            'Validar si es un concepto de Combustible
                                            SCMContComb.Parameters("@id_concepto_comp").Value = .ddlConcepto5.SelectedValue
                                            ConexionBD.Open()
                                            contComb = SCMContComb.ExecuteScalar
                                            ConexionBD.Close()
                                            If contComb = 1 Then
                                                'Obtener importes de la Factura
                                                dsImpFactura.Clear()
                                                sdaImpFactura.SelectCommand.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                                ConexionBD.Open()
                                                sdaImpFactura.Fill(dsImpFactura)
                                                ConexionBD.Close()

                                                'Insertar Carga de Combustible por Comprobar
                                                SCMCargaComb.Parameters("@importe_con_ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                                                SCMCargaComb.Parameters("@importe_sin_imp").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("subtotal").ToString())
                                                SCMCargaComb.Parameters("@ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_ieps").ToString())
                                                SCMCargaComb.Parameters("@iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_iva").ToString())
                                                SCMCargaComb.Parameters("@importe_transaccion").Value = .wceTotal5.Value
                                                SCMCargaComb.Parameters("@porcent_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("porcent_iva").ToString())
                                                SCMCargaComb.Parameters("@cantidad_mercancia").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("cantidad").ToString())
                                                SCMCargaComb.Parameters("@precio_ticket").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_ticket").ToString())
                                                SCMCargaComb.Parameters("@precio_sin_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_sin_iva").ToString())
                                                SCMCargaComb.Parameters("@razon_social_afiliado").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                                SCMCargaComb.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                                ConexionBD.Open()
                                                SCMCargaComb.ExecuteNonQuery()
                                                ConexionBD.Close()

                                                ._txtCargComb.Text = Val(._txtCargComb.Text) + 1
                                            End If
                                        End If
                                    End If
                                    'Concepto 6
                                    If .ddlNoConceptos.SelectedValue >= 6 Then
                                        SCMValores.Parameters("@fecha_realizo").Value = .wdpFecha6.Date
                                        SCMValores.Parameters("@id_concepto").Value = .ddlConcepto6.SelectedValue
                                        SCMValores.Parameters("@nombre_concepto").Value = .ddlConcepto6.SelectedItem.Text
                                        SCMValores.Parameters("@no_personas").Value = .wneNoPers6.Value
                                        SCMValores.Parameters("@no_dias").Value = .wneNoDias6.Value
                                        SCMValores.Parameters("@monto_subtotal").Value = .wceSubtotal6.Value
                                        SCMValores.Parameters("@monto_iva").Value = .wceIVA6.Value
                                        SCMValores.Parameters("@monto_total").Value = .wceTotal6.Value
                                        If .txtOriDes6.Text.Trim = "" Then
                                            SCMValores.Parameters("@origen_destino").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_dest").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_dest").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@origen_destino").Value = .txtOriDes6.Text.Trim
                                            SCMValores.Parameters("@id_lugar_orig").Value = .ddlOrig6.SelectedValue
                                            SCMValores.Parameters("@lugar_orig").Value = .ddlOrig6.SelectedItem.Text
                                            SCMValores.Parameters("@id_lugar_dest").Value = .ddlDest6.SelectedValue
                                            SCMValores.Parameters("@lugar_dest").Value = .ddlDest6.SelectedItem.Text
                                        End If
                                        If .txtVehi6.Text.Trim = "" Then
                                            SCMValores.Parameters("@vehiculo").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@vehiculo").Value = .txtVehi6.Text.Trim
                                        End If
                                        If .txtObs6.Text.Trim = "" Then
                                            SCMValores.Parameters("@obs").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@obs").Value = .txtObs6.Text.Trim
                                        End If
                                        If .cbFactura6.Checked = True Then
                                            SCMValores.Parameters("@tipo").Value = "F"
                                            'Obtener datos de la Factura
                                            dsFactura.Clear()
                                            sdaFactura.SelectCommand.Parameters("@id_dt_factura").Value = .ddlNoFactura6.SelectedValue
                                            ConexionBD.Open()
                                            sdaFactura.Fill(dsFactura)
                                            ConexionBD.Close()
                                            'Complementar Parámetros
                                            SCMValores.Parameters("@CFDI").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            SCMValores.Parameters("@no_factura").Value = dsFactura.Tables(0).Rows(0).Item("no_factura").ToString()
                                            SCMValores.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                            SCMValores.Parameters("@proveedor").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                            'Obtener el % de IVA del Concepto
                                            SCMValoresIVA.Parameters("@id_concepto").Value = .ddlConcepto6.SelectedValue
                                            ConexionBD.Open()
                                            SCMValores.Parameters("@iva").Value = SCMValoresIVA.ExecuteScalar
                                            ConexionBD.Close()
                                            'Actualizar Status de Factura
                                            SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura6.SelectedValue
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
                                        End If
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()
                                        If .cbFactura6.Checked = True Then
                                            'Obtener el id_dt_comp
                                            SCMValoresDtComp.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            idDtComp = SCMValoresDtComp.ExecuteScalar()
                                            ConexionBD.Close()
                                            'Insertar Detalle de Factura
                                            SCMValoresDtCompL.Parameters("@idDtComp").Value = idDtComp
                                            SCMValoresDtCompL.Parameters("@porcentAut").Value = .wpePorcentAut6.Value
                                            SCMValoresDtCompL.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            SCMValoresDtCompL.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            'Validar si es un concepto de Combustible
                                            SCMContComb.Parameters("@id_concepto_comp").Value = .ddlConcepto6.SelectedValue
                                            ConexionBD.Open()
                                            contComb = SCMContComb.ExecuteScalar
                                            ConexionBD.Close()
                                            If contComb = 1 Then
                                                'Obtener importes de la Factura
                                                dsImpFactura.Clear()
                                                sdaImpFactura.SelectCommand.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                                ConexionBD.Open()
                                                sdaImpFactura.Fill(dsImpFactura)
                                                ConexionBD.Close()

                                                'Insertar Carga de Combustible por Comprobar
                                                SCMCargaComb.Parameters("@importe_con_ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                                                SCMCargaComb.Parameters("@importe_sin_imp").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("subtotal").ToString())
                                                SCMCargaComb.Parameters("@ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_ieps").ToString())
                                                SCMCargaComb.Parameters("@iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_iva").ToString())
                                                SCMCargaComb.Parameters("@importe_transaccion").Value = .wceTotal6.Value
                                                SCMCargaComb.Parameters("@porcent_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("porcent_iva").ToString())
                                                SCMCargaComb.Parameters("@cantidad_mercancia").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("cantidad").ToString())
                                                SCMCargaComb.Parameters("@precio_ticket").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_ticket").ToString())
                                                SCMCargaComb.Parameters("@precio_sin_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_sin_iva").ToString())
                                                SCMCargaComb.Parameters("@razon_social_afiliado").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                                SCMCargaComb.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                                ConexionBD.Open()
                                                SCMCargaComb.ExecuteNonQuery()
                                                ConexionBD.Close()

                                                ._txtCargComb.Text = Val(._txtCargComb.Text) + 1
                                            End If
                                        End If
                                    End If
                                    'Concepto 7
                                    If .ddlNoConceptos.SelectedValue >= 7 Then
                                        SCMValores.Parameters("@fecha_realizo").Value = .wdpFecha7.Date
                                        SCMValores.Parameters("@id_concepto").Value = .ddlConcepto7.SelectedValue
                                        SCMValores.Parameters("@nombre_concepto").Value = .ddlConcepto7.SelectedItem.Text
                                        SCMValores.Parameters("@no_personas").Value = .wneNoPers7.Value
                                        SCMValores.Parameters("@no_dias").Value = .wneNoDias7.Value
                                        SCMValores.Parameters("@monto_subtotal").Value = .wceSubtotal7.Value
                                        SCMValores.Parameters("@monto_iva").Value = .wceIVA7.Value
                                        SCMValores.Parameters("@monto_total").Value = .wceTotal7.Value
                                        If .txtOriDes7.Text.Trim = "" Then
                                            SCMValores.Parameters("@origen_destino").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_dest").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_dest").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@origen_destino").Value = .txtOriDes7.Text.Trim
                                            SCMValores.Parameters("@id_lugar_orig").Value = .ddlOrig7.SelectedValue
                                            SCMValores.Parameters("@lugar_orig").Value = .ddlOrig7.SelectedItem.Text
                                            SCMValores.Parameters("@id_lugar_dest").Value = .ddlDest7.SelectedValue
                                            SCMValores.Parameters("@lugar_dest").Value = .ddlDest7.SelectedItem.Text
                                        End If
                                        If .txtVehi7.Text.Trim = "" Then
                                            SCMValores.Parameters("@vehiculo").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@vehiculo").Value = .txtVehi7.Text.Trim
                                        End If
                                        If .txtObs7.Text.Trim = "" Then
                                            SCMValores.Parameters("@obs").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@obs").Value = .txtObs7.Text.Trim
                                        End If
                                        If .cbFactura7.Checked = True Then
                                            SCMValores.Parameters("@tipo").Value = "F"
                                            'Obtener datos de la Factura
                                            dsFactura.Clear()
                                            sdaFactura.SelectCommand.Parameters("@id_dt_factura").Value = .ddlNoFactura7.SelectedValue
                                            ConexionBD.Open()
                                            sdaFactura.Fill(dsFactura)
                                            ConexionBD.Close()
                                            'Complementar Parámetros
                                            SCMValores.Parameters("@CFDI").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            SCMValores.Parameters("@no_factura").Value = dsFactura.Tables(0).Rows(0).Item("no_factura").ToString()
                                            SCMValores.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                            SCMValores.Parameters("@proveedor").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                            'Obtener el % de IVA del Concepto
                                            SCMValoresIVA.Parameters("@id_concepto").Value = .ddlConcepto7.SelectedValue
                                            ConexionBD.Open()
                                            SCMValores.Parameters("@iva").Value = SCMValoresIVA.ExecuteScalar
                                            ConexionBD.Close()
                                            'Actualizar Status de Factura
                                            SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura7.SelectedValue
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
                                        End If
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()
                                        If .cbFactura7.Checked = True Then
                                            'Obtener el id_dt_comp
                                            SCMValoresDtComp.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            idDtComp = SCMValoresDtComp.ExecuteScalar()
                                            ConexionBD.Close()
                                            'Insertar Detalle de Factura
                                            SCMValoresDtCompL.Parameters("@idDtComp").Value = idDtComp
                                            SCMValoresDtCompL.Parameters("@porcentAut").Value = .wpePorcentAut7.Value
                                            SCMValoresDtCompL.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            SCMValoresDtCompL.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            'Validar si es un concepto de Combustible
                                            SCMContComb.Parameters("@id_concepto_comp").Value = .ddlConcepto7.SelectedValue
                                            ConexionBD.Open()
                                            contComb = SCMContComb.ExecuteScalar
                                            ConexionBD.Close()
                                            If contComb = 1 Then
                                                'Obtener importes de la Factura
                                                dsImpFactura.Clear()
                                                sdaImpFactura.SelectCommand.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                                ConexionBD.Open()
                                                sdaImpFactura.Fill(dsImpFactura)
                                                ConexionBD.Close()

                                                'Insertar Carga de Combustible por Comprobar
                                                SCMCargaComb.Parameters("@importe_con_ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                                                SCMCargaComb.Parameters("@importe_sin_imp").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("subtotal").ToString())
                                                SCMCargaComb.Parameters("@ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_ieps").ToString())
                                                SCMCargaComb.Parameters("@iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_iva").ToString())
                                                SCMCargaComb.Parameters("@importe_transaccion").Value = .wceTotal7.Value
                                                SCMCargaComb.Parameters("@porcent_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("porcent_iva").ToString())
                                                SCMCargaComb.Parameters("@cantidad_mercancia").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("cantidad").ToString())
                                                SCMCargaComb.Parameters("@precio_ticket").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_ticket").ToString())
                                                SCMCargaComb.Parameters("@precio_sin_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_sin_iva").ToString())
                                                SCMCargaComb.Parameters("@razon_social_afiliado").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                                SCMCargaComb.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                                ConexionBD.Open()
                                                SCMCargaComb.ExecuteNonQuery()
                                                ConexionBD.Close()

                                                ._txtCargComb.Text = Val(._txtCargComb.Text) + 1
                                            End If
                                        End If
                                    End If
                                    'Concepto 8
                                    If .ddlNoConceptos.SelectedValue >= 8 Then
                                        SCMValores.Parameters("@fecha_realizo").Value = .wdpFecha8.Date
                                        SCMValores.Parameters("@id_concepto").Value = .ddlConcepto8.SelectedValue
                                        SCMValores.Parameters("@nombre_concepto").Value = .ddlConcepto8.SelectedItem.Text
                                        SCMValores.Parameters("@no_personas").Value = .wneNoPers8.Value
                                        SCMValores.Parameters("@no_dias").Value = .wneNoDias8.Value
                                        SCMValores.Parameters("@monto_subtotal").Value = .wceSubtotal8.Value
                                        SCMValores.Parameters("@monto_iva").Value = .wceIVA8.Value
                                        SCMValores.Parameters("@monto_total").Value = .wceTotal8.Value
                                        If .txtOriDes8.Text.Trim = "" Then
                                            SCMValores.Parameters("@origen_destino").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_dest").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_dest").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@origen_destino").Value = .txtOriDes8.Text.Trim
                                            SCMValores.Parameters("@id_lugar_orig").Value = .ddlOrig8.SelectedValue
                                            SCMValores.Parameters("@lugar_orig").Value = .ddlOrig8.SelectedItem.Text
                                            SCMValores.Parameters("@id_lugar_dest").Value = .ddlDest8.SelectedValue
                                            SCMValores.Parameters("@lugar_dest").Value = .ddlDest8.SelectedItem.Text
                                        End If
                                        If .txtVehi8.Text.Trim = "" Then
                                            SCMValores.Parameters("@vehiculo").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@vehiculo").Value = .txtVehi8.Text.Trim
                                        End If
                                        If .txtObs8.Text.Trim = "" Then
                                            SCMValores.Parameters("@obs").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@obs").Value = .txtObs8.Text.Trim
                                        End If
                                        If .cbFactura8.Checked = True Then
                                            SCMValores.Parameters("@tipo").Value = "F"
                                            'Obtener datos de la Factura
                                            dsFactura.Clear()
                                            sdaFactura.SelectCommand.Parameters("@id_dt_factura").Value = .ddlNoFactura8.SelectedValue
                                            ConexionBD.Open()
                                            sdaFactura.Fill(dsFactura)
                                            ConexionBD.Close()
                                            'Complementar Parámetros
                                            SCMValores.Parameters("@CFDI").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            SCMValores.Parameters("@no_factura").Value = dsFactura.Tables(0).Rows(0).Item("no_factura").ToString()
                                            SCMValores.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                            SCMValores.Parameters("@proveedor").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                            'Obtener el % de IVA del Concepto
                                            SCMValoresIVA.Parameters("@id_concepto").Value = .ddlConcepto8.SelectedValue
                                            ConexionBD.Open()
                                            SCMValores.Parameters("@iva").Value = SCMValoresIVA.ExecuteScalar
                                            ConexionBD.Close()
                                            'Actualizar Status de Factura
                                            SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura8.SelectedValue
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
                                        End If
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()
                                        If .cbFactura8.Checked = True Then
                                            'Obtener el id_dt_comp
                                            SCMValoresDtComp.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            idDtComp = SCMValoresDtComp.ExecuteScalar()
                                            ConexionBD.Close()
                                            'Insertar Detalle de Factura
                                            SCMValoresDtCompL.Parameters("@idDtComp").Value = idDtComp
                                            SCMValoresDtCompL.Parameters("@porcentAut").Value = .wpePorcentAut8.Value
                                            SCMValoresDtCompL.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            SCMValoresDtCompL.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            'Validar si es un concepto de Combustible
                                            SCMContComb.Parameters("@id_concepto_comp").Value = .ddlConcepto8.SelectedValue
                                            ConexionBD.Open()
                                            contComb = SCMContComb.ExecuteScalar
                                            ConexionBD.Close()
                                            If contComb = 1 Then
                                                'Obtener importes de la Factura
                                                dsImpFactura.Clear()
                                                sdaImpFactura.SelectCommand.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                                ConexionBD.Open()
                                                sdaImpFactura.Fill(dsImpFactura)
                                                ConexionBD.Close()

                                                'Insertar Carga de Combustible por Comprobar
                                                SCMCargaComb.Parameters("@importe_con_ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                                                SCMCargaComb.Parameters("@importe_sin_imp").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("subtotal").ToString())
                                                SCMCargaComb.Parameters("@ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_ieps").ToString())
                                                SCMCargaComb.Parameters("@iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_iva").ToString())
                                                SCMCargaComb.Parameters("@importe_transaccion").Value = .wceTotal8.Value
                                                SCMCargaComb.Parameters("@porcent_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("porcent_iva").ToString())
                                                SCMCargaComb.Parameters("@cantidad_mercancia").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("cantidad").ToString())
                                                SCMCargaComb.Parameters("@precio_ticket").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_ticket").ToString())
                                                SCMCargaComb.Parameters("@precio_sin_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_sin_iva").ToString())
                                                SCMCargaComb.Parameters("@razon_social_afiliado").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                                SCMCargaComb.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                                ConexionBD.Open()
                                                SCMCargaComb.ExecuteNonQuery()
                                                ConexionBD.Close()

                                                ._txtCargComb.Text = Val(._txtCargComb.Text) + 1
                                            End If
                                        End If
                                    End If
                                    'Concepto 9
                                    If .ddlNoConceptos.SelectedValue >= 9 Then
                                        SCMValores.Parameters("@fecha_realizo").Value = .wdpFecha9.Date
                                        SCMValores.Parameters("@id_concepto").Value = .ddlConcepto9.SelectedValue
                                        SCMValores.Parameters("@nombre_concepto").Value = .ddlConcepto9.SelectedItem.Text
                                        SCMValores.Parameters("@no_personas").Value = .wneNoPers9.Value
                                        SCMValores.Parameters("@no_dias").Value = .wneNoDias9.Value
                                        SCMValores.Parameters("@monto_subtotal").Value = .wceSubtotal9.Value
                                        SCMValores.Parameters("@monto_iva").Value = .wceIVA9.Value
                                        SCMValores.Parameters("@monto_total").Value = .wceTotal9.Value
                                        If .txtOriDes9.Text.Trim = "" Then
                                            SCMValores.Parameters("@origen_destino").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_dest").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_dest").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@origen_destino").Value = .txtOriDes9.Text.Trim
                                            SCMValores.Parameters("@id_lugar_orig").Value = .ddlOrig9.SelectedValue
                                            SCMValores.Parameters("@lugar_orig").Value = .ddlOrig9.SelectedItem.Text
                                            SCMValores.Parameters("@id_lugar_dest").Value = .ddlDest9.SelectedValue
                                            SCMValores.Parameters("@lugar_dest").Value = .ddlDest9.SelectedItem.Text
                                        End If
                                        If .txtVehi9.Text.Trim = "" Then
                                            SCMValores.Parameters("@vehiculo").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@vehiculo").Value = .txtVehi9.Text.Trim
                                        End If
                                        If .txtObs9.Text.Trim = "" Then
                                            SCMValores.Parameters("@obs").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@obs").Value = .txtObs9.Text.Trim
                                        End If
                                        If .cbFactura9.Checked = True Then
                                            SCMValores.Parameters("@tipo").Value = "F"
                                            'Obtener datos de la Factura
                                            dsFactura.Clear()
                                            sdaFactura.SelectCommand.Parameters("@id_dt_factura").Value = .ddlNoFactura9.SelectedValue
                                            ConexionBD.Open()
                                            sdaFactura.Fill(dsFactura)
                                            ConexionBD.Close()
                                            'Complementar Parámetros
                                            SCMValores.Parameters("@CFDI").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            SCMValores.Parameters("@no_factura").Value = dsFactura.Tables(0).Rows(0).Item("no_factura").ToString()
                                            SCMValores.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                            SCMValores.Parameters("@proveedor").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                            'Obtener el % de IVA del Concepto
                                            SCMValoresIVA.Parameters("@id_concepto").Value = .ddlConcepto9.SelectedValue
                                            ConexionBD.Open()
                                            SCMValores.Parameters("@iva").Value = SCMValoresIVA.ExecuteScalar
                                            ConexionBD.Close()
                                            'Actualizar Status de Factura
                                            SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura9.SelectedValue
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
                                        End If
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()
                                        If .cbFactura9.Checked = True Then
                                            'Obtener el id_dt_comp
                                            SCMValoresDtComp.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            idDtComp = SCMValoresDtComp.ExecuteScalar()
                                            ConexionBD.Close()
                                            'Insertar Detalle de Factura
                                            SCMValoresDtCompL.Parameters("@idDtComp").Value = idDtComp
                                            SCMValoresDtCompL.Parameters("@porcentAut").Value = .wpePorcentAut9.Value
                                            SCMValoresDtCompL.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            SCMValoresDtCompL.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            'Validar si es un concepto de Combustible
                                            SCMContComb.Parameters("@id_concepto_comp").Value = .ddlConcepto9.SelectedValue
                                            ConexionBD.Open()
                                            contComb = SCMContComb.ExecuteScalar
                                            ConexionBD.Close()
                                            If contComb = 1 Then
                                                'Obtener importes de la Factura
                                                dsImpFactura.Clear()
                                                sdaImpFactura.SelectCommand.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                                ConexionBD.Open()
                                                sdaImpFactura.Fill(dsImpFactura)
                                                ConexionBD.Close()

                                                'Insertar Carga de Combustible por Comprobar
                                                SCMCargaComb.Parameters("@importe_con_ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                                                SCMCargaComb.Parameters("@importe_sin_imp").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("subtotal").ToString())
                                                SCMCargaComb.Parameters("@ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_ieps").ToString())
                                                SCMCargaComb.Parameters("@iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_iva").ToString())
                                                SCMCargaComb.Parameters("@importe_transaccion").Value = .wceTotal9.Value
                                                SCMCargaComb.Parameters("@porcent_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("porcent_iva").ToString())
                                                SCMCargaComb.Parameters("@cantidad_mercancia").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("cantidad").ToString())
                                                SCMCargaComb.Parameters("@precio_ticket").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_ticket").ToString())
                                                SCMCargaComb.Parameters("@precio_sin_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_sin_iva").ToString())
                                                SCMCargaComb.Parameters("@razon_social_afiliado").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                                SCMCargaComb.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                                ConexionBD.Open()
                                                SCMCargaComb.ExecuteNonQuery()
                                                ConexionBD.Close()

                                                ._txtCargComb.Text = Val(._txtCargComb.Text) + 1
                                            End If
                                        End If
                                    End If
                                    'Concepto 10
                                    If .ddlNoConceptos.SelectedValue >= 10 Then
                                        SCMValores.Parameters("@fecha_realizo").Value = .wdpFecha10.Date
                                        SCMValores.Parameters("@id_concepto").Value = .ddlConcepto10.SelectedValue
                                        SCMValores.Parameters("@nombre_concepto").Value = .ddlConcepto10.SelectedItem.Text
                                        SCMValores.Parameters("@no_personas").Value = .wneNoPers10.Value
                                        SCMValores.Parameters("@no_dias").Value = .wneNoDias10.Value
                                        SCMValores.Parameters("@monto_subtotal").Value = .wceSubtotal10.Value
                                        SCMValores.Parameters("@monto_iva").Value = .wceIVA10.Value
                                        SCMValores.Parameters("@monto_total").Value = .wceTotal10.Value
                                        If .txtOriDes10.Text.Trim = "" Then
                                            SCMValores.Parameters("@origen_destino").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_dest").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_dest").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@origen_destino").Value = .txtOriDes10.Text.Trim
                                            SCMValores.Parameters("@id_lugar_orig").Value = .ddlOrig10.SelectedValue
                                            SCMValores.Parameters("@lugar_orig").Value = .ddlOrig10.SelectedItem.Text
                                            SCMValores.Parameters("@id_lugar_dest").Value = .ddlDest10.SelectedValue
                                            SCMValores.Parameters("@lugar_dest").Value = .ddlDest10.SelectedItem.Text
                                        End If
                                        If .txtVehi10.Text.Trim = "" Then
                                            SCMValores.Parameters("@vehiculo").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@vehiculo").Value = .txtVehi10.Text.Trim
                                        End If
                                        If .txtObs10.Text.Trim = "" Then
                                            SCMValores.Parameters("@obs").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@obs").Value = .txtObs10.Text.Trim
                                        End If
                                        If .cbFactura10.Checked = True Then
                                            SCMValores.Parameters("@tipo").Value = "F"
                                            'Obtener datos de la Factura
                                            dsFactura.Clear()
                                            sdaFactura.SelectCommand.Parameters("@id_dt_factura").Value = .ddlNoFactura10.SelectedValue
                                            ConexionBD.Open()
                                            sdaFactura.Fill(dsFactura)
                                            ConexionBD.Close()
                                            'Complementar Parámetros
                                            SCMValores.Parameters("@CFDI").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            SCMValores.Parameters("@no_factura").Value = dsFactura.Tables(0).Rows(0).Item("no_factura").ToString()
                                            SCMValores.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                            SCMValores.Parameters("@proveedor").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                            'Obtener el % de IVA del Concepto
                                            SCMValoresIVA.Parameters("@id_concepto").Value = .ddlConcepto10.SelectedValue
                                            ConexionBD.Open()
                                            SCMValores.Parameters("@iva").Value = SCMValoresIVA.ExecuteScalar
                                            ConexionBD.Close()
                                            'Actualizar Status de Factura
                                            SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura10.SelectedValue
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
                                        End If
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()
                                        If .cbFactura10.Checked = True Then
                                            'Obtener el id_dt_comp
                                            SCMValoresDtComp.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            idDtComp = SCMValoresDtComp.ExecuteScalar()
                                            ConexionBD.Close()
                                            'Insertar Detalle de Factura
                                            SCMValoresDtCompL.Parameters("@idDtComp").Value = idDtComp
                                            SCMValoresDtCompL.Parameters("@porcentAut").Value = .wpePorcentAut10.Value
                                            SCMValoresDtCompL.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            SCMValoresDtCompL.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            'Validar si es un concepto de Combustible
                                            SCMContComb.Parameters("@id_concepto_comp").Value = .ddlConcepto10.SelectedValue
                                            ConexionBD.Open()
                                            contComb = SCMContComb.ExecuteScalar
                                            ConexionBD.Close()
                                            If contComb = 1 Then
                                                'Obtener importes de la Factura
                                                dsImpFactura.Clear()
                                                sdaImpFactura.SelectCommand.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                                ConexionBD.Open()
                                                sdaImpFactura.Fill(dsImpFactura)
                                                ConexionBD.Close()

                                                'Insertar Carga de Combustible por Comprobar
                                                SCMCargaComb.Parameters("@importe_con_ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                                                SCMCargaComb.Parameters("@importe_sin_imp").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("subtotal").ToString())
                                                SCMCargaComb.Parameters("@ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_ieps").ToString())
                                                SCMCargaComb.Parameters("@iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_iva").ToString())
                                                SCMCargaComb.Parameters("@importe_transaccion").Value = .wceTotal10.Value
                                                SCMCargaComb.Parameters("@porcent_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("porcent_iva").ToString())
                                                SCMCargaComb.Parameters("@cantidad_mercancia").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("cantidad").ToString())
                                                SCMCargaComb.Parameters("@precio_ticket").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_ticket").ToString())
                                                SCMCargaComb.Parameters("@precio_sin_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_sin_iva").ToString())
                                                SCMCargaComb.Parameters("@razon_social_afiliado").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                                SCMCargaComb.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                                ConexionBD.Open()
                                                SCMCargaComb.ExecuteNonQuery()
                                                ConexionBD.Close()

                                                ._txtCargComb.Text = Val(._txtCargComb.Text) + 1
                                            End If
                                        End If
                                    End If
                                    'Concepto 11
                                    If .ddlNoConceptos.SelectedValue >= 11 Then
                                        SCMValores.Parameters("@fecha_realizo").Value = .wdpFecha11.Date
                                        SCMValores.Parameters("@id_concepto").Value = .ddlConcepto11.SelectedValue
                                        SCMValores.Parameters("@nombre_concepto").Value = .ddlConcepto11.SelectedItem.Text
                                        SCMValores.Parameters("@no_personas").Value = .wneNoPers11.Value
                                        SCMValores.Parameters("@no_dias").Value = .wneNoDias11.Value
                                        SCMValores.Parameters("@monto_subtotal").Value = .wceSubtotal11.Value
                                        SCMValores.Parameters("@monto_iva").Value = .wceIVA11.Value
                                        SCMValores.Parameters("@monto_total").Value = .wceTotal11.Value
                                        If .txtOriDes11.Text.Trim = "" Then
                                            SCMValores.Parameters("@origen_destino").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_dest").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_dest").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@origen_destino").Value = .txtOriDes11.Text.Trim
                                            SCMValores.Parameters("@id_lugar_orig").Value = .ddlOrig11.SelectedValue
                                            SCMValores.Parameters("@lugar_orig").Value = .ddlOrig11.SelectedItem.Text
                                            SCMValores.Parameters("@id_lugar_dest").Value = .ddlDest11.SelectedValue
                                            SCMValores.Parameters("@lugar_dest").Value = .ddlDest11.SelectedItem.Text
                                        End If
                                        If .txtVehi11.Text.Trim = "" Then
                                            SCMValores.Parameters("@vehiculo").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@vehiculo").Value = .txtVehi11.Text.Trim
                                        End If
                                        If .txtObs11.Text.Trim = "" Then
                                            SCMValores.Parameters("@obs").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@obs").Value = .txtObs11.Text.Trim
                                        End If
                                        If .cbFactura11.Checked = True Then
                                            SCMValores.Parameters("@tipo").Value = "F"
                                            'Obtener datos de la Factura
                                            dsFactura.Clear()
                                            sdaFactura.SelectCommand.Parameters("@id_dt_factura").Value = .ddlNoFactura11.SelectedValue
                                            ConexionBD.Open()
                                            sdaFactura.Fill(dsFactura)
                                            ConexionBD.Close()
                                            'Complementar Parámetros
                                            SCMValores.Parameters("@CFDI").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            SCMValores.Parameters("@no_factura").Value = dsFactura.Tables(0).Rows(0).Item("no_factura").ToString()
                                            SCMValores.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                            SCMValores.Parameters("@proveedor").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                            'Obtener el % de IVA del Concepto
                                            SCMValoresIVA.Parameters("@id_concepto").Value = .ddlConcepto11.SelectedValue
                                            ConexionBD.Open()
                                            SCMValores.Parameters("@iva").Value = SCMValoresIVA.ExecuteScalar
                                            ConexionBD.Close()
                                            'Actualizar Status de Factura
                                            SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura11.SelectedValue
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
                                        End If
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()
                                        If .cbFactura11.Checked = True Then
                                            'Obtener el id_dt_comp
                                            SCMValoresDtComp.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            idDtComp = SCMValoresDtComp.ExecuteScalar()
                                            ConexionBD.Close()
                                            'Insertar Detalle de Factura
                                            SCMValoresDtCompL.Parameters("@idDtComp").Value = idDtComp
                                            SCMValoresDtCompL.Parameters("@porcentAut").Value = .wpePorcentAut11.Value
                                            SCMValoresDtCompL.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            SCMValoresDtCompL.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            'Validar si es un concepto de Combustible
                                            SCMContComb.Parameters("@id_concepto_comp").Value = .ddlConcepto11.SelectedValue
                                            ConexionBD.Open()
                                            contComb = SCMContComb.ExecuteScalar
                                            ConexionBD.Close()
                                            If contComb = 1 Then
                                                'Obtener importes de la Factura
                                                dsImpFactura.Clear()
                                                sdaImpFactura.SelectCommand.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                                ConexionBD.Open()
                                                sdaImpFactura.Fill(dsImpFactura)
                                                ConexionBD.Close()

                                                'Insertar Carga de Combustible por Comprobar
                                                SCMCargaComb.Parameters("@importe_con_ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                                                SCMCargaComb.Parameters("@importe_sin_imp").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("subtotal").ToString())
                                                SCMCargaComb.Parameters("@ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_ieps").ToString())
                                                SCMCargaComb.Parameters("@iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_iva").ToString())
                                                SCMCargaComb.Parameters("@importe_transaccion").Value = .wceTotal11.Value
                                                SCMCargaComb.Parameters("@porcent_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("porcent_iva").ToString())
                                                SCMCargaComb.Parameters("@cantidad_mercancia").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("cantidad").ToString())
                                                SCMCargaComb.Parameters("@precio_ticket").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_ticket").ToString())
                                                SCMCargaComb.Parameters("@precio_sin_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_sin_iva").ToString())
                                                SCMCargaComb.Parameters("@razon_social_afiliado").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                                SCMCargaComb.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                                ConexionBD.Open()
                                                SCMCargaComb.ExecuteNonQuery()
                                                ConexionBD.Close()

                                                ._txtCargComb.Text = Val(._txtCargComb.Text) + 1
                                            End If
                                        End If
                                    End If
                                    'Concepto 12
                                    If .ddlNoConceptos.SelectedValue >= 12 Then
                                        SCMValores.Parameters("@fecha_realizo").Value = .wdpFecha12.Date
                                        SCMValores.Parameters("@id_concepto").Value = .ddlConcepto12.SelectedValue
                                        SCMValores.Parameters("@nombre_concepto").Value = .ddlConcepto12.SelectedItem.Text
                                        SCMValores.Parameters("@no_personas").Value = .wneNoPers12.Value
                                        SCMValores.Parameters("@no_dias").Value = .wneNoDias12.Value
                                        SCMValores.Parameters("@monto_subtotal").Value = .wceSubtotal12.Value
                                        SCMValores.Parameters("@monto_iva").Value = .wceIVA12.Value
                                        SCMValores.Parameters("@monto_total").Value = .wceTotal12.Value
                                        If .txtOriDes12.Text.Trim = "" Then
                                            SCMValores.Parameters("@origen_destino").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_dest").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_dest").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@origen_destino").Value = .txtOriDes12.Text.Trim
                                            SCMValores.Parameters("@id_lugar_orig").Value = .ddlOrig12.SelectedValue
                                            SCMValores.Parameters("@lugar_orig").Value = .ddlOrig12.SelectedItem.Text
                                            SCMValores.Parameters("@id_lugar_dest").Value = .ddlDest12.SelectedValue
                                            SCMValores.Parameters("@lugar_dest").Value = .ddlDest12.SelectedItem.Text
                                        End If
                                        If .txtVehi12.Text.Trim = "" Then
                                            SCMValores.Parameters("@vehiculo").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@vehiculo").Value = .txtVehi12.Text.Trim
                                        End If
                                        If .txtObs12.Text.Trim = "" Then
                                            SCMValores.Parameters("@obs").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@obs").Value = .txtObs12.Text.Trim
                                        End If
                                        If .cbFactura12.Checked = True Then
                                            SCMValores.Parameters("@tipo").Value = "F"
                                            'Obtener datos de la Factura
                                            dsFactura.Clear()
                                            sdaFactura.SelectCommand.Parameters("@id_dt_factura").Value = .ddlNoFactura12.SelectedValue
                                            ConexionBD.Open()
                                            sdaFactura.Fill(dsFactura)
                                            ConexionBD.Close()
                                            'Complementar Parámetros
                                            SCMValores.Parameters("@CFDI").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            SCMValores.Parameters("@no_factura").Value = dsFactura.Tables(0).Rows(0).Item("no_factura").ToString()
                                            SCMValores.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                            SCMValores.Parameters("@proveedor").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                            'Obtener el % de IVA del Concepto
                                            SCMValoresIVA.Parameters("@id_concepto").Value = .ddlConcepto12.SelectedValue
                                            ConexionBD.Open()
                                            SCMValores.Parameters("@iva").Value = SCMValoresIVA.ExecuteScalar
                                            ConexionBD.Close()
                                            'Actualizar Status de Factura
                                            SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura12.SelectedValue
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
                                        End If
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()
                                        If .cbFactura12.Checked = True Then
                                            'Obtener el id_dt_comp
                                            SCMValoresDtComp.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            idDtComp = SCMValoresDtComp.ExecuteScalar()
                                            ConexionBD.Close()
                                            'Insertar Detalle de Factura
                                            SCMValoresDtCompL.Parameters("@idDtComp").Value = idDtComp
                                            SCMValoresDtCompL.Parameters("@porcentAut").Value = .wpePorcentAut12.Value
                                            SCMValoresDtCompL.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            SCMValoresDtCompL.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            'Validar si es un concepto de Combustible
                                            SCMContComb.Parameters("@id_concepto_comp").Value = .ddlConcepto12.SelectedValue
                                            ConexionBD.Open()
                                            contComb = SCMContComb.ExecuteScalar
                                            ConexionBD.Close()
                                            If contComb = 1 Then
                                                'Obtener importes de la Factura
                                                dsImpFactura.Clear()
                                                sdaImpFactura.SelectCommand.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                                ConexionBD.Open()
                                                sdaImpFactura.Fill(dsImpFactura)
                                                ConexionBD.Close()

                                                'Insertar Carga de Combustible por Comprobar
                                                SCMCargaComb.Parameters("@importe_con_ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                                                SCMCargaComb.Parameters("@importe_sin_imp").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("subtotal").ToString())
                                                SCMCargaComb.Parameters("@ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_ieps").ToString())
                                                SCMCargaComb.Parameters("@iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_iva").ToString())
                                                SCMCargaComb.Parameters("@importe_transaccion").Value = .wceTotal12.Value
                                                SCMCargaComb.Parameters("@porcent_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("porcent_iva").ToString())
                                                SCMCargaComb.Parameters("@cantidad_mercancia").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("cantidad").ToString())
                                                SCMCargaComb.Parameters("@precio_ticket").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_ticket").ToString())
                                                SCMCargaComb.Parameters("@precio_sin_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_sin_iva").ToString())
                                                SCMCargaComb.Parameters("@razon_social_afiliado").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                                SCMCargaComb.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                                ConexionBD.Open()
                                                SCMCargaComb.ExecuteNonQuery()
                                                ConexionBD.Close()

                                                ._txtCargComb.Text = Val(._txtCargComb.Text) + 1
                                            End If
                                        End If
                                    End If
                                    'Concepto 13
                                    If .ddlNoConceptos.SelectedValue >= 13 Then
                                        SCMValores.Parameters("@fecha_realizo").Value = .wdpFecha13.Date
                                        SCMValores.Parameters("@id_concepto").Value = .ddlConcepto13.SelectedValue
                                        SCMValores.Parameters("@nombre_concepto").Value = .ddlConcepto13.SelectedItem.Text
                                        SCMValores.Parameters("@no_personas").Value = .wneNoPers13.Value
                                        SCMValores.Parameters("@no_dias").Value = .wneNoDias13.Value
                                        SCMValores.Parameters("@monto_subtotal").Value = .wceSubtotal13.Value
                                        SCMValores.Parameters("@monto_iva").Value = .wceIVA13.Value
                                        SCMValores.Parameters("@monto_total").Value = .wceTotal13.Value
                                        If .txtOriDes13.Text.Trim = "" Then
                                            SCMValores.Parameters("@origen_destino").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_dest").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_dest").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@origen_destino").Value = .txtOriDes13.Text.Trim
                                            SCMValores.Parameters("@id_lugar_orig").Value = .ddlOrig13.SelectedValue
                                            SCMValores.Parameters("@lugar_orig").Value = .ddlOrig13.SelectedItem.Text
                                            SCMValores.Parameters("@id_lugar_dest").Value = .ddlDest13.SelectedValue
                                            SCMValores.Parameters("@lugar_dest").Value = .ddlDest13.SelectedItem.Text
                                        End If
                                        If .txtVehi13.Text.Trim = "" Then
                                            SCMValores.Parameters("@vehiculo").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@vehiculo").Value = .txtVehi13.Text.Trim
                                        End If
                                        If .txtObs13.Text.Trim = "" Then
                                            SCMValores.Parameters("@obs").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@obs").Value = .txtObs13.Text.Trim
                                        End If
                                        If .cbFactura13.Checked = True Then
                                            SCMValores.Parameters("@tipo").Value = "F"
                                            'Obtener datos de la Factura
                                            dsFactura.Clear()
                                            sdaFactura.SelectCommand.Parameters("@id_dt_factura").Value = .ddlNoFactura13.SelectedValue
                                            ConexionBD.Open()
                                            sdaFactura.Fill(dsFactura)
                                            ConexionBD.Close()
                                            'Complementar Parámetros
                                            SCMValores.Parameters("@CFDI").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            SCMValores.Parameters("@no_factura").Value = dsFactura.Tables(0).Rows(0).Item("no_factura").ToString()
                                            SCMValores.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                            SCMValores.Parameters("@proveedor").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                            'Obtener el % de IVA del Concepto
                                            SCMValoresIVA.Parameters("@id_concepto").Value = .ddlConcepto13.SelectedValue
                                            ConexionBD.Open()
                                            SCMValores.Parameters("@iva").Value = SCMValoresIVA.ExecuteScalar
                                            ConexionBD.Close()
                                            'Actualizar Status de Factura
                                            SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura13.SelectedValue
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
                                        End If
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()
                                        If .cbFactura13.Checked = True Then
                                            'Obtener el id_dt_comp
                                            SCMValoresDtComp.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            idDtComp = SCMValoresDtComp.ExecuteScalar()
                                            ConexionBD.Close()
                                            'Insertar Detalle de Factura
                                            SCMValoresDtCompL.Parameters("@idDtComp").Value = idDtComp
                                            SCMValoresDtCompL.Parameters("@porcentAut").Value = .wpePorcentAut13.Value
                                            SCMValoresDtCompL.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            SCMValoresDtCompL.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            'Validar si es un concepto de Combustible
                                            SCMContComb.Parameters("@id_concepto_comp").Value = .ddlConcepto13.SelectedValue
                                            ConexionBD.Open()
                                            contComb = SCMContComb.ExecuteScalar
                                            ConexionBD.Close()
                                            If contComb = 1 Then
                                                'Obtener importes de la Factura
                                                dsImpFactura.Clear()
                                                sdaImpFactura.SelectCommand.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                                ConexionBD.Open()
                                                sdaImpFactura.Fill(dsImpFactura)
                                                ConexionBD.Close()

                                                'Insertar Carga de Combustible por Comprobar
                                                SCMCargaComb.Parameters("@importe_con_ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                                                SCMCargaComb.Parameters("@importe_sin_imp").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("subtotal").ToString())
                                                SCMCargaComb.Parameters("@ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_ieps").ToString())
                                                SCMCargaComb.Parameters("@iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_iva").ToString())
                                                SCMCargaComb.Parameters("@importe_transaccion").Value = .wceTotal13.Value
                                                SCMCargaComb.Parameters("@porcent_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("porcent_iva").ToString())
                                                SCMCargaComb.Parameters("@cantidad_mercancia").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("cantidad").ToString())
                                                SCMCargaComb.Parameters("@precio_ticket").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_ticket").ToString())
                                                SCMCargaComb.Parameters("@precio_sin_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_sin_iva").ToString())
                                                SCMCargaComb.Parameters("@razon_social_afiliado").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                                SCMCargaComb.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                                ConexionBD.Open()
                                                SCMCargaComb.ExecuteNonQuery()
                                                ConexionBD.Close()

                                                ._txtCargComb.Text = Val(._txtCargComb.Text) + 1
                                            End If
                                        End If
                                    End If
                                    'Concepto 14
                                    If .ddlNoConceptos.SelectedValue >= 14 Then
                                        SCMValores.Parameters("@fecha_realizo").Value = .wdpFecha14.Date
                                        SCMValores.Parameters("@id_concepto").Value = .ddlConcepto14.SelectedValue
                                        SCMValores.Parameters("@nombre_concepto").Value = .ddlConcepto14.SelectedItem.Text
                                        SCMValores.Parameters("@no_personas").Value = .wneNoPers14.Value
                                        SCMValores.Parameters("@no_dias").Value = .wneNoDias14.Value
                                        SCMValores.Parameters("@monto_subtotal").Value = .wceSubtotal14.Value
                                        SCMValores.Parameters("@monto_iva").Value = .wceIVA14.Value
                                        SCMValores.Parameters("@monto_total").Value = .wceTotal14.Value
                                        If .txtOriDes14.Text.Trim = "" Then
                                            SCMValores.Parameters("@origen_destino").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_dest").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_dest").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@origen_destino").Value = .txtOriDes14.Text.Trim
                                            SCMValores.Parameters("@id_lugar_orig").Value = .ddlOrig14.SelectedValue
                                            SCMValores.Parameters("@lugar_orig").Value = .ddlOrig14.SelectedItem.Text
                                            SCMValores.Parameters("@id_lugar_dest").Value = .ddlDest14.SelectedValue
                                            SCMValores.Parameters("@lugar_dest").Value = .ddlDest14.SelectedItem.Text
                                        End If
                                        If .txtVehi14.Text.Trim = "" Then
                                            SCMValores.Parameters("@vehiculo").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@vehiculo").Value = .txtVehi14.Text.Trim
                                        End If
                                        If .txtObs14.Text.Trim = "" Then
                                            SCMValores.Parameters("@obs").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@obs").Value = .txtObs14.Text.Trim
                                        End If
                                        If .cbFactura14.Checked = True Then
                                            SCMValores.Parameters("@tipo").Value = "F"
                                            'Obtener datos de la Factura
                                            dsFactura.Clear()
                                            sdaFactura.SelectCommand.Parameters("@id_dt_factura").Value = .ddlNoFactura14.SelectedValue
                                            ConexionBD.Open()
                                            sdaFactura.Fill(dsFactura)
                                            ConexionBD.Close()
                                            'Complementar Parámetros
                                            SCMValores.Parameters("@CFDI").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            SCMValores.Parameters("@no_factura").Value = dsFactura.Tables(0).Rows(0).Item("no_factura").ToString()
                                            SCMValores.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                            SCMValores.Parameters("@proveedor").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                            'Obtener el % de IVA del Concepto
                                            SCMValoresIVA.Parameters("@id_concepto").Value = .ddlConcepto14.SelectedValue
                                            ConexionBD.Open()
                                            SCMValores.Parameters("@iva").Value = SCMValoresIVA.ExecuteScalar
                                            ConexionBD.Close()
                                            'Actualizar Status de Factura
                                            SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura14.SelectedValue
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
                                        End If
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()
                                        If .cbFactura14.Checked = True Then
                                            'Obtener el id_dt_comp
                                            SCMValoresDtComp.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            idDtComp = SCMValoresDtComp.ExecuteScalar()
                                            ConexionBD.Close()
                                            'Insertar Detalle de Factura
                                            SCMValoresDtCompL.Parameters("@idDtComp").Value = idDtComp
                                            SCMValoresDtCompL.Parameters("@porcentAut").Value = .wpePorcentAut14.Value
                                            SCMValoresDtCompL.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            SCMValoresDtCompL.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            'Validar si es un concepto de Combustible
                                            SCMContComb.Parameters("@id_concepto_comp").Value = .ddlConcepto14.SelectedValue
                                            ConexionBD.Open()
                                            contComb = SCMContComb.ExecuteScalar
                                            ConexionBD.Close()
                                            If contComb = 1 Then
                                                'Obtener importes de la Factura
                                                dsImpFactura.Clear()
                                                sdaImpFactura.SelectCommand.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                                ConexionBD.Open()
                                                sdaImpFactura.Fill(dsImpFactura)
                                                ConexionBD.Close()

                                                'Insertar Carga de Combustible por Comprobar
                                                SCMCargaComb.Parameters("@importe_con_ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                                                SCMCargaComb.Parameters("@importe_sin_imp").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("subtotal").ToString())
                                                SCMCargaComb.Parameters("@ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_ieps").ToString())
                                                SCMCargaComb.Parameters("@iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_iva").ToString())
                                                SCMCargaComb.Parameters("@importe_transaccion").Value = .wceTotal14.Value
                                                SCMCargaComb.Parameters("@porcent_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("porcent_iva").ToString())
                                                SCMCargaComb.Parameters("@cantidad_mercancia").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("cantidad").ToString())
                                                SCMCargaComb.Parameters("@precio_ticket").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_ticket").ToString())
                                                SCMCargaComb.Parameters("@precio_sin_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_sin_iva").ToString())
                                                SCMCargaComb.Parameters("@razon_social_afiliado").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                                SCMCargaComb.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                                ConexionBD.Open()
                                                SCMCargaComb.ExecuteNonQuery()
                                                ConexionBD.Close()

                                                ._txtCargComb.Text = Val(._txtCargComb.Text) + 1
                                            End If
                                        End If
                                    End If
                                    'Concepto 15
                                    If .ddlNoConceptos.SelectedValue >= 15 Then
                                        SCMValores.Parameters("@fecha_realizo").Value = .wdpFecha15.Date
                                        SCMValores.Parameters("@id_concepto").Value = .ddlConcepto15.SelectedValue
                                        SCMValores.Parameters("@nombre_concepto").Value = .ddlConcepto15.SelectedItem.Text
                                        SCMValores.Parameters("@no_personas").Value = .wneNoPers15.Value
                                        SCMValores.Parameters("@no_dias").Value = .wneNoDias15.Value
                                        SCMValores.Parameters("@monto_subtotal").Value = .wceSubtotal15.Value
                                        SCMValores.Parameters("@monto_iva").Value = .wceIVA15.Value
                                        SCMValores.Parameters("@monto_total").Value = .wceTotal15.Value
                                        If .txtOriDes15.Text.Trim = "" Then
                                            SCMValores.Parameters("@origen_destino").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_dest").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_dest").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@origen_destino").Value = .txtOriDes15.Text.Trim
                                            SCMValores.Parameters("@id_lugar_orig").Value = .ddlOrig15.SelectedValue
                                            SCMValores.Parameters("@lugar_orig").Value = .ddlOrig15.SelectedItem.Text
                                            SCMValores.Parameters("@id_lugar_dest").Value = .ddlDest15.SelectedValue
                                            SCMValores.Parameters("@lugar_dest").Value = .ddlDest15.SelectedItem.Text
                                        End If
                                        If .txtVehi15.Text.Trim = "" Then
                                            SCMValores.Parameters("@vehiculo").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@vehiculo").Value = .txtVehi15.Text.Trim
                                        End If
                                        If .txtObs15.Text.Trim = "" Then
                                            SCMValores.Parameters("@obs").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@obs").Value = .txtObs15.Text.Trim
                                        End If
                                        If .cbFactura15.Checked = True Then
                                            SCMValores.Parameters("@tipo").Value = "F"
                                            'Obtener datos de la Factura
                                            dsFactura.Clear()
                                            sdaFactura.SelectCommand.Parameters("@id_dt_factura").Value = .ddlNoFactura15.SelectedValue
                                            ConexionBD.Open()
                                            sdaFactura.Fill(dsFactura)
                                            ConexionBD.Close()
                                            'Complementar Parámetros
                                            SCMValores.Parameters("@CFDI").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            SCMValores.Parameters("@no_factura").Value = dsFactura.Tables(0).Rows(0).Item("no_factura").ToString()
                                            SCMValores.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                            SCMValores.Parameters("@proveedor").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                            'Obtener el % de IVA del Concepto
                                            SCMValoresIVA.Parameters("@id_concepto").Value = .ddlConcepto15.SelectedValue
                                            ConexionBD.Open()
                                            SCMValores.Parameters("@iva").Value = SCMValoresIVA.ExecuteScalar
                                            ConexionBD.Close()
                                            'Actualizar Status de Factura
                                            SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura15.SelectedValue
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
                                        End If
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()
                                        If .cbFactura15.Checked = True Then
                                            'Obtener el id_dt_comp
                                            SCMValoresDtComp.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            idDtComp = SCMValoresDtComp.ExecuteScalar()
                                            ConexionBD.Close()
                                            'Insertar Detalle de Factura
                                            SCMValoresDtCompL.Parameters("@idDtComp").Value = idDtComp
                                            SCMValoresDtCompL.Parameters("@porcentAut").Value = .wpePorcentAut15.Value
                                            SCMValoresDtCompL.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            SCMValoresDtCompL.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            'Validar si es un concepto de Combustible
                                            SCMContComb.Parameters("@id_concepto_comp").Value = .ddlConcepto15.SelectedValue
                                            ConexionBD.Open()
                                            contComb = SCMContComb.ExecuteScalar
                                            ConexionBD.Close()
                                            If contComb = 1 Then
                                                'Obtener importes de la Factura
                                                dsImpFactura.Clear()
                                                sdaImpFactura.SelectCommand.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                                ConexionBD.Open()
                                                sdaImpFactura.Fill(dsImpFactura)
                                                ConexionBD.Close()

                                                'Insertar Carga de Combustible por Comprobar
                                                SCMCargaComb.Parameters("@importe_con_ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                                                SCMCargaComb.Parameters("@importe_sin_imp").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("subtotal").ToString())
                                                SCMCargaComb.Parameters("@ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_ieps").ToString())
                                                SCMCargaComb.Parameters("@iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_iva").ToString())
                                                SCMCargaComb.Parameters("@importe_transaccion").Value = .wceTotal15.Value
                                                SCMCargaComb.Parameters("@porcent_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("porcent_iva").ToString())
                                                SCMCargaComb.Parameters("@cantidad_mercancia").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("cantidad").ToString())
                                                SCMCargaComb.Parameters("@precio_ticket").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_ticket").ToString())
                                                SCMCargaComb.Parameters("@precio_sin_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_sin_iva").ToString())
                                                SCMCargaComb.Parameters("@razon_social_afiliado").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                                SCMCargaComb.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                                ConexionBD.Open()
                                                SCMCargaComb.ExecuteNonQuery()
                                                ConexionBD.Close()

                                                ._txtCargComb.Text = Val(._txtCargComb.Text) + 1
                                            End If
                                        End If
                                    End If
                                    'Concepto 16
                                    If .ddlNoConceptos.SelectedValue >= 16 Then
                                        SCMValores.Parameters("@fecha_realizo").Value = .wdpFecha16.Date
                                        SCMValores.Parameters("@id_concepto").Value = .ddlConcepto16.SelectedValue
                                        SCMValores.Parameters("@nombre_concepto").Value = .ddlConcepto16.SelectedItem.Text
                                        SCMValores.Parameters("@no_personas").Value = .wneNoPers16.Value
                                        SCMValores.Parameters("@no_dias").Value = .wneNoDias16.Value
                                        SCMValores.Parameters("@monto_subtotal").Value = .wceSubtotal16.Value
                                        SCMValores.Parameters("@monto_iva").Value = .wceIVA16.Value
                                        SCMValores.Parameters("@monto_total").Value = .wceTotal16.Value
                                        If .txtOriDes16.Text.Trim = "" Then
                                            SCMValores.Parameters("@origen_destino").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_dest").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_dest").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@origen_destino").Value = .txtOriDes16.Text.Trim
                                            SCMValores.Parameters("@id_lugar_orig").Value = .ddlOrig16.SelectedValue
                                            SCMValores.Parameters("@lugar_orig").Value = .ddlOrig16.SelectedItem.Text
                                            SCMValores.Parameters("@id_lugar_dest").Value = .ddlDest16.SelectedValue
                                            SCMValores.Parameters("@lugar_dest").Value = .ddlDest16.SelectedItem.Text
                                        End If
                                        If .txtVehi16.Text.Trim = "" Then
                                            SCMValores.Parameters("@vehiculo").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@vehiculo").Value = .txtVehi16.Text.Trim
                                        End If
                                        If .txtObs16.Text.Trim = "" Then
                                            SCMValores.Parameters("@obs").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@obs").Value = .txtObs16.Text.Trim
                                        End If
                                        If .cbFactura16.Checked = True Then
                                            SCMValores.Parameters("@tipo").Value = "F"
                                            'Obtener datos de la Factura
                                            dsFactura.Clear()
                                            sdaFactura.SelectCommand.Parameters("@id_dt_factura").Value = .ddlNoFactura16.SelectedValue
                                            ConexionBD.Open()
                                            sdaFactura.Fill(dsFactura)
                                            ConexionBD.Close()
                                            'Complementar Parámetros
                                            SCMValores.Parameters("@CFDI").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            SCMValores.Parameters("@no_factura").Value = dsFactura.Tables(0).Rows(0).Item("no_factura").ToString()
                                            SCMValores.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                            SCMValores.Parameters("@proveedor").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                            'Obtener el % de IVA del Concepto
                                            SCMValoresIVA.Parameters("@id_concepto").Value = .ddlConcepto16.SelectedValue
                                            ConexionBD.Open()
                                            SCMValores.Parameters("@iva").Value = SCMValoresIVA.ExecuteScalar
                                            ConexionBD.Close()
                                            'Actualizar Status de Factura
                                            SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura16.SelectedValue
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
                                        End If
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()
                                        If .cbFactura16.Checked = True Then
                                            'Obtener el id_dt_comp
                                            SCMValoresDtComp.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            idDtComp = SCMValoresDtComp.ExecuteScalar()
                                            ConexionBD.Close()
                                            'Insertar Detalle de Factura
                                            SCMValoresDtCompL.Parameters("@idDtComp").Value = idDtComp
                                            SCMValoresDtCompL.Parameters("@porcentAut").Value = .wpePorcentAut16.Value
                                            SCMValoresDtCompL.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            SCMValoresDtCompL.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            'Validar si es un concepto de Combustible
                                            SCMContComb.Parameters("@id_concepto_comp").Value = .ddlConcepto16.SelectedValue
                                            ConexionBD.Open()
                                            contComb = SCMContComb.ExecuteScalar
                                            ConexionBD.Close()
                                            If contComb = 1 Then
                                                'Obtener importes de la Factura
                                                dsImpFactura.Clear()
                                                sdaImpFactura.SelectCommand.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                                ConexionBD.Open()
                                                sdaImpFactura.Fill(dsImpFactura)
                                                ConexionBD.Close()

                                                'Insertar Carga de Combustible por Comprobar
                                                SCMCargaComb.Parameters("@importe_con_ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                                                SCMCargaComb.Parameters("@importe_sin_imp").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("subtotal").ToString())
                                                SCMCargaComb.Parameters("@ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_ieps").ToString())
                                                SCMCargaComb.Parameters("@iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_iva").ToString())
                                                SCMCargaComb.Parameters("@importe_transaccion").Value = .wceTotal16.Value
                                                SCMCargaComb.Parameters("@porcent_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("porcent_iva").ToString())
                                                SCMCargaComb.Parameters("@cantidad_mercancia").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("cantidad").ToString())
                                                SCMCargaComb.Parameters("@precio_ticket").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_ticket").ToString())
                                                SCMCargaComb.Parameters("@precio_sin_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_sin_iva").ToString())
                                                SCMCargaComb.Parameters("@razon_social_afiliado").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                                SCMCargaComb.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                                ConexionBD.Open()
                                                SCMCargaComb.ExecuteNonQuery()
                                                ConexionBD.Close()

                                                ._txtCargComb.Text = Val(._txtCargComb.Text) + 1
                                            End If
                                        End If
                                    End If
                                    'Concepto 17
                                    If .ddlNoConceptos.SelectedValue >= 17 Then
                                        SCMValores.Parameters("@fecha_realizo").Value = .wdpFecha17.Date
                                        SCMValores.Parameters("@id_concepto").Value = .ddlConcepto17.SelectedValue
                                        SCMValores.Parameters("@nombre_concepto").Value = .ddlConcepto17.SelectedItem.Text
                                        SCMValores.Parameters("@no_personas").Value = .wneNoPers17.Value
                                        SCMValores.Parameters("@no_dias").Value = .wneNoDias17.Value
                                        SCMValores.Parameters("@monto_subtotal").Value = .wceSubtotal17.Value
                                        SCMValores.Parameters("@monto_iva").Value = .wceIVA17.Value
                                        SCMValores.Parameters("@monto_total").Value = .wceTotal17.Value
                                        If .txtOriDes17.Text.Trim = "" Then
                                            SCMValores.Parameters("@origen_destino").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_dest").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_dest").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@origen_destino").Value = .txtOriDes17.Text.Trim
                                            SCMValores.Parameters("@id_lugar_orig").Value = .ddlOrig17.SelectedValue
                                            SCMValores.Parameters("@lugar_orig").Value = .ddlOrig17.SelectedItem.Text
                                            SCMValores.Parameters("@id_lugar_dest").Value = .ddlDest17.SelectedValue
                                            SCMValores.Parameters("@lugar_dest").Value = .ddlDest17.SelectedItem.Text
                                        End If
                                        If .txtVehi17.Text.Trim = "" Then
                                            SCMValores.Parameters("@vehiculo").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@vehiculo").Value = .txtVehi17.Text.Trim
                                        End If
                                        If .txtObs17.Text.Trim = "" Then
                                            SCMValores.Parameters("@obs").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@obs").Value = .txtObs17.Text.Trim
                                        End If
                                        If .cbFactura17.Checked = True Then
                                            SCMValores.Parameters("@tipo").Value = "F"
                                            'Obtener datos de la Factura
                                            dsFactura.Clear()
                                            sdaFactura.SelectCommand.Parameters("@id_dt_factura").Value = .ddlNoFactura17.SelectedValue
                                            ConexionBD.Open()
                                            sdaFactura.Fill(dsFactura)
                                            ConexionBD.Close()
                                            'Complementar Parámetros
                                            SCMValores.Parameters("@CFDI").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            SCMValores.Parameters("@no_factura").Value = dsFactura.Tables(0).Rows(0).Item("no_factura").ToString()
                                            SCMValores.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                            SCMValores.Parameters("@proveedor").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                            'Obtener el % de IVA del Concepto
                                            SCMValoresIVA.Parameters("@id_concepto").Value = .ddlConcepto17.SelectedValue
                                            ConexionBD.Open()
                                            SCMValores.Parameters("@iva").Value = SCMValoresIVA.ExecuteScalar
                                            ConexionBD.Close()
                                            'Actualizar Status de Factura
                                            SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura17.SelectedValue
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
                                        End If
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()
                                        If .cbFactura17.Checked = True Then
                                            'Obtener el id_dt_comp
                                            SCMValoresDtComp.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            idDtComp = SCMValoresDtComp.ExecuteScalar()
                                            ConexionBD.Close()
                                            'Insertar Detalle de Factura
                                            SCMValoresDtCompL.Parameters("@idDtComp").Value = idDtComp
                                            SCMValoresDtCompL.Parameters("@porcentAut").Value = .wpePorcentAut17.Value
                                            SCMValoresDtCompL.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            SCMValoresDtCompL.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            'Validar si es un concepto de Combustible
                                            SCMContComb.Parameters("@id_concepto_comp").Value = .ddlConcepto17.SelectedValue
                                            ConexionBD.Open()
                                            contComb = SCMContComb.ExecuteScalar
                                            ConexionBD.Close()
                                            If contComb = 1 Then
                                                'Obtener importes de la Factura
                                                dsImpFactura.Clear()
                                                sdaImpFactura.SelectCommand.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                                ConexionBD.Open()
                                                sdaImpFactura.Fill(dsImpFactura)
                                                ConexionBD.Close()

                                                'Insertar Carga de Combustible por Comprobar
                                                SCMCargaComb.Parameters("@importe_con_ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                                                SCMCargaComb.Parameters("@importe_sin_imp").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("subtotal").ToString())
                                                SCMCargaComb.Parameters("@ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_ieps").ToString())
                                                SCMCargaComb.Parameters("@iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_iva").ToString())
                                                SCMCargaComb.Parameters("@importe_transaccion").Value = .wceTotal17.Value
                                                SCMCargaComb.Parameters("@porcent_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("porcent_iva").ToString())
                                                SCMCargaComb.Parameters("@cantidad_mercancia").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("cantidad").ToString())
                                                SCMCargaComb.Parameters("@precio_ticket").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_ticket").ToString())
                                                SCMCargaComb.Parameters("@precio_sin_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_sin_iva").ToString())
                                                SCMCargaComb.Parameters("@razon_social_afiliado").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                                SCMCargaComb.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                                ConexionBD.Open()
                                                SCMCargaComb.ExecuteNonQuery()
                                                ConexionBD.Close()

                                                ._txtCargComb.Text = Val(._txtCargComb.Text) + 1
                                            End If
                                        End If
                                    End If
                                    'Concepto 18
                                    If .ddlNoConceptos.SelectedValue >= 18 Then
                                        SCMValores.Parameters("@fecha_realizo").Value = .wdpFecha18.Date
                                        SCMValores.Parameters("@id_concepto").Value = .ddlConcepto18.SelectedValue
                                        SCMValores.Parameters("@nombre_concepto").Value = .ddlConcepto18.SelectedItem.Text
                                        SCMValores.Parameters("@no_personas").Value = .wneNoPers18.Value
                                        SCMValores.Parameters("@no_dias").Value = .wneNoDias18.Value
                                        SCMValores.Parameters("@monto_subtotal").Value = .wceSubtotal18.Value
                                        SCMValores.Parameters("@monto_iva").Value = .wceIVA18.Value
                                        SCMValores.Parameters("@monto_total").Value = .wceTotal18.Value
                                        If .txtOriDes18.Text.Trim = "" Then
                                            SCMValores.Parameters("@origen_destino").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_dest").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_dest").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@origen_destino").Value = .txtOriDes18.Text.Trim
                                            SCMValores.Parameters("@id_lugar_orig").Value = .ddlOrig18.SelectedValue
                                            SCMValores.Parameters("@lugar_orig").Value = .ddlOrig18.SelectedItem.Text
                                            SCMValores.Parameters("@id_lugar_dest").Value = .ddlDest18.SelectedValue
                                            SCMValores.Parameters("@lugar_dest").Value = .ddlDest18.SelectedItem.Text
                                        End If
                                        If .txtVehi18.Text.Trim = "" Then
                                            SCMValores.Parameters("@vehiculo").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@vehiculo").Value = .txtVehi18.Text.Trim
                                        End If
                                        If .txtObs18.Text.Trim = "" Then
                                            SCMValores.Parameters("@obs").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@obs").Value = .txtObs18.Text.Trim
                                        End If
                                        If .cbFactura18.Checked = True Then
                                            SCMValores.Parameters("@tipo").Value = "F"
                                            'Obtener datos de la Factura
                                            dsFactura.Clear()
                                            sdaFactura.SelectCommand.Parameters("@id_dt_factura").Value = .ddlNoFactura18.SelectedValue
                                            ConexionBD.Open()
                                            sdaFactura.Fill(dsFactura)
                                            ConexionBD.Close()
                                            'Complementar Parámetros
                                            SCMValores.Parameters("@CFDI").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            SCMValores.Parameters("@no_factura").Value = dsFactura.Tables(0).Rows(0).Item("no_factura").ToString()
                                            SCMValores.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                            SCMValores.Parameters("@proveedor").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                            'Obtener el % de IVA del Concepto
                                            SCMValoresIVA.Parameters("@id_concepto").Value = .ddlConcepto18.SelectedValue
                                            ConexionBD.Open()
                                            SCMValores.Parameters("@iva").Value = SCMValoresIVA.ExecuteScalar
                                            ConexionBD.Close()
                                            'Actualizar Status de Factura
                                            SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura18.SelectedValue
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
                                        End If
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()
                                        If .cbFactura18.Checked = True Then
                                            'Obtener el id_dt_comp
                                            SCMValoresDtComp.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            idDtComp = SCMValoresDtComp.ExecuteScalar()
                                            ConexionBD.Close()
                                            'Insertar Detalle de Factura
                                            SCMValoresDtCompL.Parameters("@idDtComp").Value = idDtComp
                                            SCMValoresDtCompL.Parameters("@porcentAut").Value = .wpePorcentAut18.Value
                                            SCMValoresDtCompL.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            SCMValoresDtCompL.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            'Validar si es un concepto de Combustible
                                            SCMContComb.Parameters("@id_concepto_comp").Value = .ddlConcepto18.SelectedValue
                                            ConexionBD.Open()
                                            contComb = SCMContComb.ExecuteScalar
                                            ConexionBD.Close()
                                            If contComb = 1 Then
                                                'Obtener importes de la Factura
                                                dsImpFactura.Clear()
                                                sdaImpFactura.SelectCommand.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                                ConexionBD.Open()
                                                sdaImpFactura.Fill(dsImpFactura)
                                                ConexionBD.Close()

                                                'Insertar Carga de Combustible por Comprobar
                                                SCMCargaComb.Parameters("@importe_con_ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                                                SCMCargaComb.Parameters("@importe_sin_imp").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("subtotal").ToString())
                                                SCMCargaComb.Parameters("@ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_ieps").ToString())
                                                SCMCargaComb.Parameters("@iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_iva").ToString())
                                                SCMCargaComb.Parameters("@importe_transaccion").Value = .wceTotal18.Value
                                                SCMCargaComb.Parameters("@porcent_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("porcent_iva").ToString())
                                                SCMCargaComb.Parameters("@cantidad_mercancia").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("cantidad").ToString())
                                                SCMCargaComb.Parameters("@precio_ticket").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_ticket").ToString())
                                                SCMCargaComb.Parameters("@precio_sin_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_sin_iva").ToString())
                                                SCMCargaComb.Parameters("@razon_social_afiliado").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                                SCMCargaComb.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                                ConexionBD.Open()
                                                SCMCargaComb.ExecuteNonQuery()
                                                ConexionBD.Close()

                                                ._txtCargComb.Text = Val(._txtCargComb.Text) + 1
                                            End If
                                        End If
                                    End If
                                    'Concepto 19
                                    If .ddlNoConceptos.SelectedValue >= 19 Then
                                        SCMValores.Parameters("@fecha_realizo").Value = .wdpFecha19.Date
                                        SCMValores.Parameters("@id_concepto").Value = .ddlConcepto19.SelectedValue
                                        SCMValores.Parameters("@nombre_concepto").Value = .ddlConcepto19.SelectedItem.Text
                                        SCMValores.Parameters("@no_personas").Value = .wneNoPers19.Value
                                        SCMValores.Parameters("@no_dias").Value = .wneNoDias19.Value
                                        SCMValores.Parameters("@monto_subtotal").Value = .wceSubtotal19.Value
                                        SCMValores.Parameters("@monto_iva").Value = .wceIVA19.Value
                                        SCMValores.Parameters("@monto_total").Value = .wceTotal19.Value
                                        If .txtOriDes19.Text.Trim = "" Then
                                            SCMValores.Parameters("@origen_destino").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_dest").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_dest").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@origen_destino").Value = .txtOriDes19.Text.Trim
                                            SCMValores.Parameters("@id_lugar_orig").Value = .ddlOrig19.SelectedValue
                                            SCMValores.Parameters("@lugar_orig").Value = .ddlOrig19.SelectedItem.Text
                                            SCMValores.Parameters("@id_lugar_dest").Value = .ddlDest19.SelectedValue
                                            SCMValores.Parameters("@lugar_dest").Value = .ddlDest19.SelectedItem.Text
                                        End If
                                        If .txtVehi19.Text.Trim = "" Then
                                            SCMValores.Parameters("@vehiculo").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@vehiculo").Value = .txtVehi19.Text.Trim
                                        End If
                                        If .txtObs19.Text.Trim = "" Then
                                            SCMValores.Parameters("@obs").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@obs").Value = .txtObs19.Text.Trim
                                        End If
                                        If .cbFactura19.Checked = True Then
                                            SCMValores.Parameters("@tipo").Value = "F"
                                            'Obtener datos de la Factura
                                            dsFactura.Clear()
                                            sdaFactura.SelectCommand.Parameters("@id_dt_factura").Value = .ddlNoFactura19.SelectedValue
                                            ConexionBD.Open()
                                            sdaFactura.Fill(dsFactura)
                                            ConexionBD.Close()
                                            'Complementar Parámetros
                                            SCMValores.Parameters("@CFDI").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            SCMValores.Parameters("@no_factura").Value = dsFactura.Tables(0).Rows(0).Item("no_factura").ToString()
                                            SCMValores.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                            SCMValores.Parameters("@proveedor").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                            'Obtener el % de IVA del Concepto
                                            SCMValoresIVA.Parameters("@id_concepto").Value = .ddlConcepto19.SelectedValue
                                            ConexionBD.Open()
                                            SCMValores.Parameters("@iva").Value = SCMValoresIVA.ExecuteScalar
                                            ConexionBD.Close()
                                            'Actualizar Status de Factura
                                            SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura19.SelectedValue
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
                                        End If
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()
                                        If .cbFactura19.Checked = True Then
                                            'Obtener el id_dt_comp
                                            SCMValoresDtComp.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            idDtComp = SCMValoresDtComp.ExecuteScalar()
                                            ConexionBD.Close()
                                            'Insertar Detalle de Factura
                                            SCMValoresDtCompL.Parameters("@idDtComp").Value = idDtComp
                                            SCMValoresDtCompL.Parameters("@porcentAut").Value = .wpePorcentAut19.Value
                                            SCMValoresDtCompL.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            SCMValoresDtCompL.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            'Validar si es un concepto de Combustible
                                            SCMContComb.Parameters("@id_concepto_comp").Value = .ddlConcepto19.SelectedValue
                                            ConexionBD.Open()
                                            contComb = SCMContComb.ExecuteScalar
                                            ConexionBD.Close()
                                            If contComb = 1 Then
                                                'Obtener importes de la Factura
                                                dsImpFactura.Clear()
                                                sdaImpFactura.SelectCommand.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                                ConexionBD.Open()
                                                sdaImpFactura.Fill(dsImpFactura)
                                                ConexionBD.Close()

                                                'Insertar Carga de Combustible por Comprobar
                                                SCMCargaComb.Parameters("@importe_con_ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                                                SCMCargaComb.Parameters("@importe_sin_imp").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("subtotal").ToString())
                                                SCMCargaComb.Parameters("@ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_ieps").ToString())
                                                SCMCargaComb.Parameters("@iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_iva").ToString())
                                                SCMCargaComb.Parameters("@importe_transaccion").Value = .wceTotal19.Value
                                                SCMCargaComb.Parameters("@porcent_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("porcent_iva").ToString())
                                                SCMCargaComb.Parameters("@cantidad_mercancia").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("cantidad").ToString())
                                                SCMCargaComb.Parameters("@precio_ticket").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_ticket").ToString())
                                                SCMCargaComb.Parameters("@precio_sin_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_sin_iva").ToString())
                                                SCMCargaComb.Parameters("@razon_social_afiliado").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                                SCMCargaComb.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                                ConexionBD.Open()
                                                SCMCargaComb.ExecuteNonQuery()
                                                ConexionBD.Close()

                                                ._txtCargComb.Text = Val(._txtCargComb.Text) + 1
                                            End If
                                        End If
                                    End If
                                    'Concepto 20
                                    If .ddlNoConceptos.SelectedValue >= 20 Then
                                        SCMValores.Parameters("@fecha_realizo").Value = .wdpFecha20.Date
                                        SCMValores.Parameters("@id_concepto").Value = .ddlConcepto20.SelectedValue
                                        SCMValores.Parameters("@nombre_concepto").Value = .ddlConcepto20.SelectedItem.Text
                                        SCMValores.Parameters("@no_personas").Value = .wneNoPers20.Value
                                        SCMValores.Parameters("@no_dias").Value = .wneNoDias20.Value
                                        SCMValores.Parameters("@monto_subtotal").Value = .wceSubtotal20.Value
                                        SCMValores.Parameters("@monto_iva").Value = .wceIVA20.Value
                                        SCMValores.Parameters("@monto_total").Value = .wceTotal20.Value
                                        If .txtOriDes20.Text.Trim = "" Then
                                            SCMValores.Parameters("@origen_destino").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_orig").Value = DBNull.Value
                                            SCMValores.Parameters("@id_lugar_dest").Value = DBNull.Value
                                            SCMValores.Parameters("@lugar_dest").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@origen_destino").Value = .txtOriDes20.Text.Trim
                                            SCMValores.Parameters("@id_lugar_orig").Value = .ddlOrig20.SelectedValue
                                            SCMValores.Parameters("@lugar_orig").Value = .ddlOrig20.SelectedItem.Text
                                            SCMValores.Parameters("@id_lugar_dest").Value = .ddlDest20.SelectedValue
                                            SCMValores.Parameters("@lugar_dest").Value = .ddlDest20.SelectedItem.Text
                                        End If
                                        If .txtVehi20.Text.Trim = "" Then
                                            SCMValores.Parameters("@vehiculo").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@vehiculo").Value = .txtVehi20.Text.Trim
                                        End If
                                        If .txtObs20.Text.Trim = "" Then
                                            SCMValores.Parameters("@obs").Value = DBNull.Value
                                        Else
                                            SCMValores.Parameters("@obs").Value = .txtObs20.Text.Trim
                                        End If
                                        If .cbFactura20.Checked = True Then
                                            SCMValores.Parameters("@tipo").Value = "F"
                                            'Obtener datos de la Factura
                                            dsFactura.Clear()
                                            sdaFactura.SelectCommand.Parameters("@id_dt_factura").Value = .ddlNoFactura20.SelectedValue
                                            ConexionBD.Open()
                                            sdaFactura.Fill(dsFactura)
                                            ConexionBD.Close()
                                            'Complementar Parámetros
                                            SCMValores.Parameters("@CFDI").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            SCMValores.Parameters("@no_factura").Value = dsFactura.Tables(0).Rows(0).Item("no_factura").ToString()
                                            SCMValores.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                            SCMValores.Parameters("@proveedor").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                            'Obtener el % de IVA del Concepto
                                            SCMValoresIVA.Parameters("@id_concepto").Value = .ddlConcepto20.SelectedValue
                                            ConexionBD.Open()
                                            SCMValores.Parameters("@iva").Value = SCMValoresIVA.ExecuteScalar
                                            ConexionBD.Close()
                                            'Actualizar Status de Factura
                                            SCMValoresDtFact.Parameters("@id_dt_factura").Value = .ddlNoFactura20.SelectedValue
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
                                        End If
                                        ConexionBD.Open()
                                        SCMValores.ExecuteNonQuery()
                                        ConexionBD.Close()
                                        If .cbFactura20.Checked = True Then
                                            'Obtener el id_dt_comp
                                            SCMValoresDtComp.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            idDtComp = SCMValoresDtComp.ExecuteScalar()
                                            ConexionBD.Close()
                                            'Insertar Detalle de Factura
                                            SCMValoresDtCompL.Parameters("@idDtComp").Value = idDtComp
                                            SCMValoresDtCompL.Parameters("@porcentAut").Value = .wpePorcentAut20.Value
                                            SCMValoresDtCompL.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                            ConexionBD.Open()
                                            SCMValoresDtCompL.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            'Validar si es un concepto de Combustible
                                            SCMContComb.Parameters("@id_concepto_comp").Value = .ddlConcepto20.SelectedValue
                                            ConexionBD.Open()
                                            contComb = SCMContComb.ExecuteScalar
                                            ConexionBD.Close()
                                            If contComb = 1 Then
                                                'Obtener importes de la Factura
                                                dsImpFactura.Clear()
                                                sdaImpFactura.SelectCommand.Parameters("@uuid").Value = dsFactura.Tables(0).Rows(0).Item("cfdi").ToString()
                                                ConexionBD.Open()
                                                sdaImpFactura.Fill(dsImpFactura)
                                                ConexionBD.Close()

                                                'Insertar Carga de Combustible por Comprobar
                                                SCMCargaComb.Parameters("@importe_con_ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                                                SCMCargaComb.Parameters("@importe_sin_imp").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("subtotal").ToString())
                                                SCMCargaComb.Parameters("@ieps").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_ieps").ToString())
                                                SCMCargaComb.Parameters("@iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("tot_tras_iva").ToString())
                                                SCMCargaComb.Parameters("@importe_transaccion").Value = .wceTotal20.Value
                                                SCMCargaComb.Parameters("@porcent_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("porcent_iva").ToString())
                                                SCMCargaComb.Parameters("@cantidad_mercancia").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("cantidad").ToString())
                                                SCMCargaComb.Parameters("@precio_ticket").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_ticket").ToString())
                                                SCMCargaComb.Parameters("@precio_sin_iva").Value = Val(dsImpFactura.Tables(0).Rows(0).Item("precio_sin_iva").ToString())
                                                SCMCargaComb.Parameters("@razon_social_afiliado").Value = dsFactura.Tables(0).Rows(0).Item("proveedor").ToString()
                                                SCMCargaComb.Parameters("@rfc").Value = dsFactura.Tables(0).Rows(0).Item("rfc").ToString()
                                                ConexionBD.Open()
                                                SCMCargaComb.ExecuteNonQuery()
                                                ConexionBD.Close()

                                                ._txtCargComb.Text = Val(._txtCargComb.Text) + 1
                                            End If
                                        End If
                                    End If
                                    sdaFactura.Dispose()
                                    dsFactura.Dispose()

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

                                    'Insertar Instancia de la Comprobación
                                    SCMValores.Parameters.Clear()
                                    SCMValores.CommandText = "insert into ms_instancia ( id_ms_sol,  tipo,  id_actividad) " +
                                                             "				    values (@id_ms_sol, @tipo, @id_actividad) "
                                    SCMValores.Parameters.AddWithValue("@id_ms_sol", Val(.lblFolio.Text))
                                    SCMValores.Parameters.AddWithValue("@tipo", "C")
                                    SCMValores.Parameters.AddWithValue("@id_actividad", 62)
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
                                    SCMValores.Parameters.AddWithValue("@id_actividad", 62)
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
                                    SCMValores.Parameters.AddWithValue("@idAut", Val(._txtIdAutorizador.Text))
                                    ConexionBD.Open()
                                    destinatario = SCMValores.ExecuteScalar()
                                    ConexionBD.Close()

                                    Mensaje.[To].Add(destinatario)
                                    Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                                    Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                                    Mensaje.Subject = "ProcAd - Comprobación No. " + .lblFolio.Text + " por Autorizar"
                                    Dim texto As String
                                    texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                                            "Se generó la comprobación número <b>" + .lblFolio.Text +
                                            "</b> por parte de <b>" + .ddlEmpleado.SelectedItem.Text +
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

                                    If Val(._txtCargComb.Text) > 0 Then
                                        .litError.Text = "Existen " + ._txtCargComb.Text + " Cargas de Combustible con Tarjeta, favor de complementarlas a la brevedad"
                                    End If

                                    'Inhabilitar Paneles
                                    .pnlInicio.Enabled = False
                                End While
                            End If
                        End If
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

End Class