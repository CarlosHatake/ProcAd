Public Class CatServicio
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        'Lista de Empresas
                        Dim sdaEmpresa As New SqlDataAdapter
                        Dim dsEmpresa As New DataSet
                        .ddlEmpresa.DataSource = dsEmpresa
                        sdaEmpresa.SelectCommand = New SqlCommand("select 0 as id_empresa " +
                                                                  "     , ' ' as empresa " +
                                                                  "union " +
                                                                  "select id_empresa " +
                                                                  "     , nombre as empresa " +
                                                                  "from bd_Empleado.dbo.cg_empresa " +
                                                                  "where status = 'A' " +
                                                                  "order by empresa ", ConexionBD)
                        .ddlEmpresa.DataTextField = "empresa"
                        .ddlEmpresa.DataValueField = "id_empresa"
                        ConexionBD.Open()
                        sdaEmpresa.Fill(dsEmpresa)
                        .ddlEmpresa.DataBind()
                        ConexionBD.Close()
                        sdaEmpresa.Dispose()
                        dsEmpresa.Dispose()
                        .ddlEmpresa.SelectedIndex = -1

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

    Protected Sub ibtnBuscar_Click(sender As Object, e As EventArgs) Handles ibtnBuscar.Click
        llenarGrid()
    End Sub

#End Region

#Region "Funciones"

    Public Sub limpiarPantalla()
        With Me
            llenarGrid()
            .pnlGrid.Enabled = True
            .ibtnAlta.Enabled = True
            .ibtnBaja.Enabled = False
            .ibtnBaja.ImageUrl = "images\Trash_i2.png"
            .ibtnModif.Enabled = False
            .ibtnModif.ImageUrl = "images\Edit_i2.png"
            .pnlGrid.Visible = True
            .pnlDatos.Visible = False
        End With
    End Sub

    Public Sub bloqueoPantalla()
        With Me
            .pnlGrid.Visible = False
            .pnlDatos.Visible = True
            .pnlConfig.Visible = False
        End With
    End Sub

    Public Sub llenarGrid()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .gvServicio.Columns(0).Visible = True
                .gvServicio.DataSource = dsCatalogo
                'Catálogo de Servicios
                Dim query As String = ""
                query = "select id_servicio " +
                        "     , servicio " +
                        "     , tipo_servicio as tipo " +
                        "     , case when (select count(*) " +
                        "                  from dt_servicio_conf " +
                        "                  where dt_servicio_conf.id_servicio = cg_servicio.id_servicio) = 0 then null " +
                        "            else case when (select count(*) " +
                        "                            from dt_servicio_conf " +
                        "                            where dt_servicio_conf.id_servicio = cg_servicio.id_servicio) = 1 then (select admon_oper " +
                        "                                                                                                    from dt_servicio_conf " +
                        "                                                                                                    where dt_servicio_conf.id_servicio = cg_servicio.id_servicio) " +
                        "                      else 'Múltiple' " +
                        "                 end " +
                        "       end as admon_oper " +
                        "     , case when (select count(*) " +
                        "                  from dt_servicio_conf " +
                        "                  where dt_servicio_conf.id_servicio = cg_servicio.id_servicio) = 0 then null " +
                        "            else case when (select count(*) " +
                        "                            from dt_servicio_conf " +
                        "                            where dt_servicio_conf.id_servicio = cg_servicio.id_servicio) = 1 then (select cg_empresa.nombre " +
                        "                                                                                                    from dt_servicio_conf " +
                        "                                                                                                      left join bd_Empleado.dbo.cg_empresa on dt_servicio_conf.id_empresa = cg_empresa.id_empresa " +
                        "                                                                                                    where dt_servicio_conf.id_servicio = cg_servicio.id_servicio) " +
                        "                      else 'Múltiple' " +
                        "                 end " +
                        "       end as empresa " +
                        "     , case when (select count(*) " +
                        "                  from dt_servicio_conf " +
                        "                  where dt_servicio_conf.id_servicio = cg_servicio.id_servicio) = 0 then null " +
                        "            else case when (select count(*) " +
                        "                            from dt_servicio_conf " +
                        "                            where dt_servicio_conf.id_servicio = cg_servicio.id_servicio) = 1 then (select finanzas " +
                        "                                                                                                    from dt_servicio_conf " +
                        "                                                                                                    where dt_servicio_conf.id_servicio = cg_servicio.id_servicio) " +
                        "                      else 'Múltiple' " +
                        "                 end " +
                        "       end as finanzas " +
                        "     , case when (select count(*) " +
                        "                  from dt_servicio_conf " +
                        "                  where dt_servicio_conf.id_servicio = cg_servicio.id_servicio) = 0 then null " +
                        "            else case when (select count(*) " +
                        "                            from dt_servicio_conf " +
                        "                            where dt_servicio_conf.id_servicio = cg_servicio.id_servicio) = 1 then (select valida_presup " +
                        "                                                                                                    from dt_servicio_conf " +
                        "                                                                                                    where dt_servicio_conf.id_servicio = cg_servicio.id_servicio) " +
                        "                      else 'Múltiple' " +
                        "                 end " +
                        "       end as valida_presup " +
                        "from cg_servicio " +
                        "where (" + .ddlCampo.SelectedValue.ToString + " like '%' + @valor +'%') " +
                        "  and cg_servicio.status = 'A' " +
                        "order by servicio "
                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@valor", .txtValor.Text.Trim)
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvServicio.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvServicio.Columns(0).Visible = False
                .gvServicio.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarListaUsrAut(ByVal nombre, ByRef lista)
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                lista.DataSource = dsCatalogo
                'Catálogo de Empleados
                sdaCatalogo.SelectCommand = New SqlCommand("select id_usuario " +
                                                           "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as empleado " +
                                                           "from cg_usuario " +
                                                           "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                           "where cg_usuario.status = 'A' " +
                                                           "  and cgEmpl.status = 'A' " +
                                                           "  and cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno like '%' + @nombreEmpl + '%' " +
                                                           "order by empleado ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@nombreEmpl", nombre)
                lista.DataTextField = "empleado"
                lista.DataValueField = "id_usuario"
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                lista.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                lista.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#Region "Tabla Configuraciones"

    Public Sub actDesCheckCot()
        With Me
            Try
                .litError.Text = ""

                If .ddlTipo.SelectedValue = "Servicios Únicos" Or .ddlTipo.SelectedValue = "Servicios Específicos" Then
                    .cbReqCotizaciones.Checked = False
                    .cbReqCotizaciones.Enabled = False
                Else
                    .cbReqCotizaciones.Checked = True
                    .cbReqCotizaciones.Enabled = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub localizar(ByVal idRegistro)
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                sdaCatalogo.SelectCommand = New SqlCommand("select servicio " +
                                                           "     , tipo_servicio " +
                                                           "     , cotizaciones " +
                                                           "from cg_servicio " +
                                                           "where id_servicio = @idServicio ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@idServicio", idRegistro)
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                ConexionBD.Close()
                .txtServicio.Text = dsCatalogo.Tables(0).Rows(0).Item("servicio").ToString()
                .ddlTipo.SelectedValue = dsCatalogo.Tables(0).Rows(0).Item("tipo_servicio").ToString()
                If dsCatalogo.Tables(0).Rows(0).Item("cotizaciones").ToString() = "S" Then
                    .cbReqCotizaciones.Checked = True
                Else
                    .cbReqCotizaciones.Checked = False
                End If

                .ddlTipo.Enabled = False
                If dsCatalogo.Tables(0).Rows(0).Item("tipo_servicio").ToString() = "Servicios Únicos" Or dsCatalogo.Tables(0).Rows(0).Item("tipo_servicio").ToString() = "Servicios Específicos" Then
                    .cbReqCotizaciones.Enabled = False
                Else
                    .cbReqCotizaciones.Enabled = True
                End If
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()

                limpiarPantallaConfig()

                bloqueoPantalla()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub limpiarPantallaConfig()
        With Me
            'Actualizar Lista de Configuraciones
            llenarConfiguraciones(Val(._txtIdServicio.Text))
            'General
            .pnlDetalle.Visible = True
            .pnlGeneral.Enabled = True
            .pnlTablaConfig.Visible = True
            .pnlTablaConfig.Enabled = True
            .ibtnAltaConf.Enabled = True
            .ibtnBajaConf.Enabled = False
            .ibtnBajaConf.ImageUrl = "images\Trash_i2.png"
            .ibtnModifConf.Enabled = False
            .ibtnModifConf.ImageUrl = "images\Edit_i2.png"

            llenarAdjuntos(Val(._txtIdServicio.Text))
            .txtAdjunto.Text = ""
            .ibtnAltaAdj.Enabled = True
            .ibtnBajaAdj.Enabled = False
            .ibtnBajaAdj.ImageUrl = "images\Trash_i2.png"
            .ibtnModifAdj.Enabled = False
            .ibtnModifAdj.ImageUrl = "images\Edit_i2.png"

            .pnlConfig.Visible = False
            .btnAceptar.Visible = True
            .btnCancelar.Visible = True
        End With
    End Sub

    Public Sub limpiarAdjuntos()
        With Me
            llenarAdjuntos(Val(._txtIdServicio.Text))
            .txtAdjunto.Text = ""
            .ibtnAltaAdj.Enabled = True
            .ibtnBajaAdj.Enabled = False
            .ibtnBajaAdj.ImageUrl = "images\Trash_i2.png"
            .ibtnModifAdj.Enabled = False
            .ibtnModifAdj.ImageUrl = "images\Edit_i2.png"
        End With
    End Sub

    Public Sub llenarConfiguraciones(ByVal idServicio)
        With Me
            Try
                'Llenar Grid
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaAutorizadores As New SqlDataAdapter
                Dim dsAutorizadores As New DataSet
                .gvConfiguracion.DataSource = dsAutorizadores
                .gvConfiguracion.Columns(0).Visible = True
                sdaAutorizadores.SelectCommand = New SqlCommand("select id_dt_servicio_conf " +
                                                                "     , admon_oper " +
                                                                "     , isnull(cg_empresa.nombre, 'Para Todas') as empresa " +
                                                                "     , cuenta_cont " +
                                                                "     , case id_usr_valida " +
                                                                "         when   0 then 'Lista pre-definida' " +
                                                                "         when  -1 then 'x Organigrama' " +
                                                                "         when  -2 then 'Gerente Organigrama' " +
                                                                "         when  -3 then 'Director Organigrama' " +
                                                                "         when  -4 then 'Gerente División / CC' " +
                                                                "         when  -5 then 'Director División / CC' " +
                                                                "         when  -6 then 'Gerente Empresa' " +
                                                                "         when  -7 then 'Director Empresa' " +
                                                                "         when -99 then 'No aplica' " +
                                                                "         else cgEmplVal.nombre + ' ' + cgEmplVal.ap_paterno " +
                                                                "       end as validador " +
                                                                "     , finanzas " +
                                                                "     , valida_presup " +
                                                                "     , case id_usr_autoriza1 " +
                                                                "         when   0 then 'Lista pre-definida' " +
                                                                "         when  -1 then 'x Organigrama' " +
                                                                "         when  -2 then 'Gerente Organigrama' " +
                                                                "         when  -3 then 'Director Organigrama' " +
                                                                "         when  -4 then 'Gerente División / CC' " +
                                                                "         when  -5 then 'Director División / CC' " +
                                                                "         when  -6 then 'Gerente Empresa' " +
                                                                "         when  -7 then 'Director Empresa' " +
                                                                "         when -99 then 'No aplica' " +
                                                                "         else cgEmplAut1.nombre + ' ' + cgEmplAut1.ap_paterno " +
                                                                "       end as autorizador1 " +
                                                                "     , case id_usr_autoriza2 " +
                                                                "         when   0 then 'Lista pre-definida' " +
                                                                "         when  -1 then 'x Organigrama' " +
                                                                "         when  -2 then 'Gerente Organigrama' " +
                                                                "         when  -3 then 'Director Organigrama' " +
                                                                "         when  -4 then 'Gerente División / CC' " +
                                                                "         when  -5 then 'Director División / CC' " +
                                                                "         when  -6 then 'Gerente Empresa' " +
                                                                "         when  -7 then 'Director Empresa' " +
                                                                "         when -99 then 'No aplica' " +
                                                                "         else cgEmplAut2.nombre + ' ' + cgEmplAut2.ap_paterno " +
                                                                "       end as autorizador2 " +
                                                                "     , case id_usr_autoriza3 " +
                                                                "         when   0 then 'Lista pre-definida' " +
                                                                "         when  -1 then 'x Organigrama' " +
                                                                "         when  -2 then 'Gerente Organigrama' " +
                                                                "         when  -3 then 'Director Organigrama' " +
                                                                "         when  -4 then 'Gerente División / CC' " +
                                                                "         when  -5 then 'Director División / CC' " +
                                                                "         when  -6 then 'Gerente Empresa' " +
                                                                "         when  -7 then 'Director Empresa' " +
                                                                "         when -99 then 'No aplica' " +
                                                                "         else cgEmplAut3.nombre + ' ' + cgEmplAut3.ap_paterno " +
                                                                "       end as autorizador3 " +
                                                                "from dt_servicio_conf " +
                                                                "  left join bd_Empleado.dbo.cg_empresa on dt_servicio_conf.id_empresa = cg_empresa.id_empresa " +
                                                                "  left join cg_usuario cgUsrVal on dt_servicio_conf.id_usr_valida = cgUsrVal.id_usuario " +
                                                                "  left join bd_Empleado.dbo.cg_empleado cgEmplVal on cgUsrVal.id_empleado = cgEmplVal.id_empleado " +
                                                                "  left join cg_usuario cgUsrAut1 on dt_servicio_conf.id_usr_autoriza1 = cgUsrAut1.id_usuario " +
                                                                "  left join bd_Empleado.dbo.cg_empleado cgEmplAut1 on cgUsrAut1.id_empleado = cgEmplAut1.id_empleado " +
                                                                "  left join cg_usuario cgUsrAut2 on dt_servicio_conf.id_usr_autoriza2 = cgUsrAut2.id_usuario " +
                                                                "  left join bd_Empleado.dbo.cg_empleado cgEmplAut2 on cgUsrAut2.id_empleado = cgEmplAut2.id_empleado " +
                                                                "  left join cg_usuario cgUsrAut3 on dt_servicio_conf.id_usr_autoriza3 = cgUsrAut3.id_usuario " +
                                                                "  left join bd_Empleado.dbo.cg_empleado cgEmplAut3 on cgUsrAut3.id_empleado = cgEmplAut3.id_empleado " +
                                                                "where id_servicio = @idServicio ", ConexionBD)
                sdaAutorizadores.SelectCommand.Parameters.AddWithValue("@idServicio", idServicio)
                ConexionBD.Open()
                sdaAutorizadores.Fill(dsAutorizadores)
                .gvConfiguracion.DataBind()
                ConexionBD.Close()
                sdaAutorizadores.Dispose()
                dsAutorizadores.Dispose()
                .gvConfiguracion.Columns(0).Visible = False
                .gvConfiguracion.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarAdjuntos(ByVal idServicio)
        With Me
            Try
                'Llenar Grid
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaAdjuntos As New SqlDataAdapter
                Dim dsAdjuntos As New DataSet
                .gvAdjunto.DataSource = dsAdjuntos
                .gvAdjunto.Columns(0).Visible = True
                sdaAdjuntos.SelectCommand = New SqlCommand("select id_dt_servicio_adj " +
                                                           "     , adjunto " +
                                                           "from dt_servicio_adj " +
                                                           "where id_servicio = @idServicio ", ConexionBD)
                sdaAdjuntos.SelectCommand.Parameters.AddWithValue("@idServicio", idServicio)
                ConexionBD.Open()
                sdaAdjuntos.Fill(dsAdjuntos)
                .gvAdjunto.DataBind()
                ConexionBD.Close()
                sdaAdjuntos.Dispose()
                dsAdjuntos.Dispose()
                .gvAdjunto.Columns(0).Visible = False
                .gvAdjunto.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Validadores / Autorizadores"

    Public Sub llenarAutorizadores(ByVal tipo, ByRef tabla)
        With Me
            Try
                'Llenar Grid
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaAutorizadores As New SqlDataAdapter
                Dim dsAutorizadores As New DataSet
                tabla.DataSource = dsAutorizadores
                tabla.Columns(0).Visible = True
                sdaAutorizadores.SelectCommand = New SqlCommand("select dt_servicio_aut.id_usuario " +
                                                                "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as usuario " +
                                                                "from dt_servicio_aut " +
                                                                "  left join cg_usuario on dt_servicio_aut.id_usuario = cg_usuario.id_usuario " +
                                                                "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                                "where dt_servicio_aut.id_dt_servicio_conf = @id_dt_servicio_conf " +
                                                                "  and dt_servicio_aut.tipo = @tipo " +
                                                                "order by usuario ", ConexionBD)
                sdaAutorizadores.SelectCommand.Parameters.AddWithValue("@id_dt_servicio_conf", Val(._txtIdConfig.Text))
                sdaAutorizadores.SelectCommand.Parameters.AddWithValue("@tipo", tipo)
                ConexionBD.Open()
                sdaAutorizadores.Fill(dsAutorizadores)
                tabla.DataBind()
                ConexionBD.Close()
                sdaAutorizadores.Dispose()
                dsAutorizadores.Dispose()
                tabla.Columns(0).Visible = False
                tabla.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Function validarAut(ByVal idUsuario, ByVal tipo)
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMTemp As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMTemp.Connection = ConexionBD
                Dim conteo As Integer = 0
                SCMTemp.CommandText = ""
                SCMTemp.Parameters.Clear()
                SCMTemp.CommandText = "select count(*) " +
                                      "from dt_servicio_aut " +
                                      "where id_dt_servicio_conf = @id_dt_servicio_conf " +
                                      "  and id_usuario = @idUsuario " +
                                      "  and tipo = @tipo "
                SCMTemp.Parameters.AddWithValue("@id_dt_servicio_conf", Val(._txtIdConfig.Text))
                SCMTemp.Parameters.AddWithValue("@idUsuario", idUsuario)
                SCMTemp.Parameters.AddWithValue("@tipo", tipo)
                ConexionBD.Open()
                conteo = SCMTemp.ExecuteScalar
                ConexionBD.Close()
                If conteo > 0 Then
                    validarAut = False
                Else
                    validarAut = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
                validarAut = False
            End Try
        End With
    End Function

    Public Sub aceptarUsrAut(ByRef lista, ByVal tipo, ByRef tabla, ByVal tipoMov)
        With Me
            Try
                .litError.Text = ""
                Dim ban As Integer = 0
                If lista.Items.Count = 0 Then
                    .litError.Text = "Información Insuficiente, favor de elegir un autorizador"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    Select Case tipoMov 'Tipo de Ajuste (Agregar o Eliminar)
                        Case "A"
                            If validarAut(lista.SelectedValue, tipo) Then
                                SCMValores.CommandText = "insert into dt_servicio_aut (id_dt_servicio_conf, id_usuario, tipo) values (@id_dt_servicio_conf, @id_usuario, @tipo)"
                                SCMValores.Parameters.AddWithValue("@id_dt_servicio_conf", Val(._txtIdConfig.Text))
                                SCMValores.Parameters.AddWithValue("@id_usuario", lista.SelectedValue)
                                SCMValores.Parameters.AddWithValue("@tipo", tipo)
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()
                                llenarAutorizadores(tipo, tabla)
                            Else
                                .litError.Text = "Valor Inválido, ya se asignó ese Usuario "
                                ban = 1
                            End If
                        Case "B"
                            bajaUsrAut(tipo, tabla)
                    End Select
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub bajaUsrAut(ByVal tipo, ByRef tabla)
        With Me
            Try
                .litError.Text = ""
                If tabla.SelectedIndex = -1 Then
                    .litError.Text = "Información Insuficiente, favor de elegir un autorizador"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "delete from dt_servicio_aut where id_dt_servicio_conf = @id_dt_servicio_conf and id_usuario = @id_usuario and tipo = @tipo "
                    SCMValores.Parameters.AddWithValue("@id_dt_servicio_conf", Val(._txtIdConfig.Text))
                    SCMValores.Parameters.AddWithValue("@id_usuario", Val(tabla.SelectedRow.Cells(0).Text))
                    SCMValores.Parameters.AddWithValue("@tipo", tipo)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()
                    llenarAutorizadores(tipo, tabla)
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

    Function validar()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMTemp As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMTemp.Connection = ConexionBD
                Dim conteo As Integer = 0
                SCMTemp.CommandText = ""
                SCMTemp.Parameters.Clear()
                Select Case ._txtTipoMov.Text 'Tipo de Ajuste (alta, baja o modificación)
                    Case "A"
                        SCMTemp.CommandText = "SELECT COUNT(*) FROM cg_servicio WHERE servicio = @servicio AND status = 'A'"
                    Case Else
                        SCMTemp.CommandText = "SELECT COUNT(*) FROM cg_servicio WHERE servicio = @servicio AND id_servicio <> @id_servicio AND status = 'A'"
                        SCMTemp.Parameters.AddWithValue("@id_servicio", Val(._txtIdServicio.Text))
                End Select
                SCMTemp.Parameters.AddWithValue("@servicio", .txtServicio.Text)
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

#End Region

#Region "Tabla de Servicios"

    Protected Sub gvServicio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvServicio.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                .ibtnBaja.Enabled = True
                .ibtnBaja.ImageUrl = "images\Trash.png"
                .ibtnModif.Enabled = True
                .ibtnModif.ImageUrl = "images\Edit.png"

                ._txtIdServicio.Text = .gvServicio.SelectedRow.Cells(0).Text
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Botones Catálogo"

    Protected Sub ibtnAlta_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAlta.Click
        With Me
            Try
                ._txtTipoMov.Text = "A"
                .lblTipoMov.Text = "Alta"
                bloqueoPantalla()
                .txtServicio.Text = ""
                .ddlTipo.SelectedIndex = -1
                actDesCheckCot()
                'Habilitar / Deshabilitar campos
                .pnlGeneral.Enabled = True
                .ddlTipo.Enabled = True
                .btnAceptarA.Visible = True
                .pnlTablaConfig.Visible = False

                .btnAceptar.Visible = False
                .btnCancelar.Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnBaja_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBaja.Click
        With Me
            Try
                ._txtTipoMov.Text = "B"
                .lblTipoMov.Text = "Baja"
                localizar(.gvServicio.SelectedRow.Cells(0).Text)
                'Deshabilitar campos
                .pnlGeneral.Enabled = False
                .btnAceptarA.Visible = False
                .pnlTablaConfig.Visible = True
                .pnlTablaConfig.Enabled = False

                .btnAceptar.Visible = True
                .btnCancelar.Visible = True
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnModif_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnModif.Click
        With Me
            Try
                ._txtTipoMov.Text = "M"
                .lblTipoMov.Text = "Modificación"
                localizar(.gvServicio.SelectedRow.Cells(0).Text)
                'Habilitar / Deshabilitar campos
                .pnlGeneral.Enabled = True
                .ddlTipo.Enabled = False
                .btnAceptarA.Visible = False
                .pnlTablaConfig.Visible = True
                .pnlTablaConfig.Enabled = True

                .btnAceptar.Visible = True
                .btnCancelar.Visible = True
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Datos Generales del Servicio"

    Protected Sub ddlTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipo.SelectedIndexChanged
        actDesCheckCot()
    End Sub

    Protected Sub btnAceptarA_Click(sender As Object, e As EventArgs) Handles btnAceptarA.Click
        With Me
            Try
                .litError.Text = ""
                Dim ban As Integer = 0
                If .txtServicio.Text.Trim = "" Then
                    .litError.Text = "Información Insuficiente, favor de indicar el nombre del servicio"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    If validar() Then
                        SCMValores.CommandText = "INSERT INTO cg_servicio ( servicio,  tipo_servicio,  cotizacion_unica,  cotizaciones,  contrato,  servicio_negociado) " +
                                                 "                 values (@servicio, @tipo_servicio, @cotizacion_unica, @cotizaciones, @contrato, @servicio_negociado)"
                    Else
                        .litError.Text = "Valor Inválido, ya existe ese Servicio"
                        ban = 1
                    End If
                    If ban = 0 Then
                        SCMValores.Parameters.AddWithValue("@servicio", .txtServicio.Text)
                        SCMValores.Parameters.AddWithValue("@tipo_servicio", .ddlTipo.SelectedValue)
                        Select Case .ddlTipo.SelectedValue
                            Case "Servicios Específicos"
                                SCMValores.Parameters.AddWithValue("@cotizacion_unica", "S")
                                SCMValores.Parameters.AddWithValue("@contrato", "N")
                                SCMValores.Parameters.AddWithValue("@servicio_negociado", "N")
                            Case "Servicios Únicos"
                                SCMValores.Parameters.AddWithValue("@cotizacion_unica", "N")
                                SCMValores.Parameters.AddWithValue("@contrato", "N")
                                SCMValores.Parameters.AddWithValue("@servicio_negociado", "N")
                            Case "Contrato"
                                SCMValores.Parameters.AddWithValue("@cotizacion_unica", "N")
                                SCMValores.Parameters.AddWithValue("@contrato", "S")
                                SCMValores.Parameters.AddWithValue("@servicio_negociado", "N")
                            Case "Servicios Negociados"
                                SCMValores.Parameters.AddWithValue("@cotizacion_unica", "N")
                                SCMValores.Parameters.AddWithValue("@contrato", "N")
                                SCMValores.Parameters.AddWithValue("@servicio_negociado", "S")
                        End Select
                        If .cbReqCotizaciones.Checked = True Then
                            SCMValores.Parameters.AddWithValue("@cotizaciones", "S")
                        Else
                            SCMValores.Parameters.AddWithValue("@cotizaciones", "N")
                        End If
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Obtener el ID Servicio
                        SCMValores.CommandText = ""
                        SCMValores.CommandText = "select isnull(max(id_servicio), 0) from cg_servicio WHERE servicio = @servicio and tipo_servicio = @tipo_servicio and status = 'A' "
                        ConexionBD.Open()
                        ._txtIdServicio.Text = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        .ddlTipo.Enabled = False
                        .btnAceptarA.Visible = False
                        .pnlTablaConfig.Visible = True
                        .pnlTablaConfig.Enabled = True

                        'Actualizar Lista de Configuraciones
                        llenarConfiguraciones(Val(._txtIdServicio.Text))
                        llenarAdjuntos(Val(._txtIdServicio.Text))
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""
                Dim ban As Integer = 0
                If .txtServicio.Text.Trim = "" Then
                    .litError.Text = "Información Insuficiente, favor de indicar el nombre del servicio"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    Select Case ._txtTipoMov.Text 'Tipo de Ajuste (alta, baja o modificación)
                        Case "B"
                            SCMValores.CommandText = "UPDATE cg_servicio SET status = 'B' WHERE id_servicio = @id_servicio"
                        Case Else
                            Dim SCMTemp As SqlCommand = New System.Data.SqlClient.SqlCommand
                            SCMTemp.Connection = ConexionBD
                            Dim conteo As Integer = 0
                            SCMTemp.CommandText = ""
                            SCMTemp.Parameters.Clear()
                            SCMTemp.CommandText = "SELECT COUNT(*) FROM cg_servicio WHERE servicio = @servicio AND id_servicio <> @id_servicio AND status = 'A'"
                            SCMTemp.Parameters.AddWithValue("@id_servicio", Val(._txtIdServicio.Text))
                            SCMTemp.Parameters.AddWithValue("@servicio", .txtServicio.Text)
                            ConexionBD.Open()
                            conteo = SCMTemp.ExecuteScalar
                            ConexionBD.Close()
                            If conteo > 0 Then
                                .litError.Text = "Valor Inválido, ya existe ese Servicio"
                                ban = 1
                            Else
                                SCMValores.CommandText = "UPDATE cg_servicio SET servicio = @servicio, tipo_servicio = @tipo_servicio, cotizacion_unica = @cotizacion_unica, cotizaciones = @cotizaciones, contrato = @contrato, servicio_negociado = @servicio_negociado WHERE id_servicio = @id_servicio"
                            End If
                    End Select
                    If ban = 0 Then
                        'If ._txtTipoMov.Text = "B" Or ._txtTipoMov.Text = "M" Then
                        SCMValores.Parameters.AddWithValue("@id_servicio", Val(._txtIdServicio.Text))
                        SCMValores.Parameters.AddWithValue("@servicio", .txtServicio.Text)
                        SCMValores.Parameters.AddWithValue("@tipo_servicio", .ddlTipo.SelectedValue)
                        Select Case .ddlTipo.SelectedValue
                            Case "Servicios Específicos"
                                SCMValores.Parameters.AddWithValue("@cotizacion_unica", "S")
                                SCMValores.Parameters.AddWithValue("@contrato", "N")
                                SCMValores.Parameters.AddWithValue("@servicio_negociado", "N")
                            Case "Servicios Únicos"
                                SCMValores.Parameters.AddWithValue("@cotizacion_unica", "N")
                                SCMValores.Parameters.AddWithValue("@contrato", "N")
                                SCMValores.Parameters.AddWithValue("@servicio_negociado", "N")
                            Case "Contrato"
                                SCMValores.Parameters.AddWithValue("@cotizacion_unica", "N")
                                SCMValores.Parameters.AddWithValue("@contrato", "S")
                                SCMValores.Parameters.AddWithValue("@servicio_negociado", "N")
                            Case "Servicios Negociados"
                                SCMValores.Parameters.AddWithValue("@cotizacion_unica", "N")
                                SCMValores.Parameters.AddWithValue("@contrato", "N")
                                SCMValores.Parameters.AddWithValue("@servicio_negociado", "S")
                        End Select
                        If .cbReqCotizaciones.Checked = True Then
                            SCMValores.Parameters.AddWithValue("@cotizaciones", "S")
                        Else
                            SCMValores.Parameters.AddWithValue("@cotizaciones", "N")
                        End If
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()
                        'End If

                        limpiarPantalla()
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        limpiarPantalla()
    End Sub

#End Region

#Region "Tabla de Configuración"

    Public Sub habilitarCampos(ByVal valor)
        With Me
            .rblAdmonOper.Enabled = valor
            .ddlEmpresa.Enabled = valor
            .cbFinanzas.Enabled = valor
            .cbValidaPrep.Enabled = valor
            'Validador
            .ddlValidador1T.Enabled = valor
            .txtUsuarioVal1B.Enabled = valor
            .cmdBuscarUsrVal1.Enabled = valor
            .ddlUsuarioVal1.Enabled = valor
            .ibtnAltaVal1.Enabled = valor
            .ibtnBajaVal1.Enabled = valor
            If valor = True Then
                .ibtnAltaVal1.ImageUrl = "images\Add.png"
                .ibtnBajaVal1.ImageUrl = "images\Trash.png"
            Else
                .ibtnAltaVal1.ImageUrl = "images\Add_i2.png"
                .ibtnBajaVal1.ImageUrl = "images\Trash_i2.png"
            End If
            .gvValidador1.Enabled = valor

            'Validador 2
            .ddlValidador2T.Enabled = valor
            .txtUsuarioVal2B.Enabled = valor
            .cmdBuscarUsrVal2.Enabled = valor
            .ddlUsuarioVal2.Enabled = valor
            .ibtnAltaVal2.Enabled = valor
            .ibtnBajaVal2.Enabled = valor
            If valor = True Then
                .ibtnAltaVal2.ImageUrl = "images\Add.png"
                .ibtnBajaVal2.ImageUrl = "images\Trash.png"
            Else
                .ibtnAltaVal2.ImageUrl = "images\Add_i2.png"
                .ibtnBajaVal2.ImageUrl = "images\Trash_i2.png"
            End If
            .gvValidador2.Enabled = valor

            'Autorizador 1
            .ddlAutorizador1T.Enabled = valor
            .txtUsuarioAut1B.Enabled = valor
            .cmdBuscarUsrAut1.Enabled = valor
            .ddlUsuarioAut1.Enabled = valor
            .ibtnAltaAut1.Enabled = valor
            .ibtnBajaAut1.Enabled = valor
            If valor = True Then
                .ibtnAltaAut1.ImageUrl = "images\Add.png"
                .ibtnBajaAut1.ImageUrl = "images\Trash.png"
            Else
                .ibtnAltaAut1.ImageUrl = "images\Add_i2.png"
                .ibtnBajaAut1.ImageUrl = "images\Trash_i2.png"
            End If
            .gvAutorizador1.Enabled = valor

            'Autorizador 2
            .ddlAutorizador2T.Enabled = valor
            .txtUsuarioAut2B.Enabled = valor
            .cmdBuscarUsrAut2.Enabled = valor
            .ddlUsuarioAut2.Enabled = valor
            .ibtnAltaAut2.Enabled = valor
            .ibtnBajaAut2.Enabled = valor
            If valor = True Then
                .ibtnAltaAut2.ImageUrl = "images\Add.png"
                .ibtnBajaAut2.ImageUrl = "images\Trash.png"
            Else
                .ibtnAltaAut2.ImageUrl = "images\Add_i2.png"
                .ibtnBajaAut2.ImageUrl = "images\Trash_i2.png"
            End If
            .gvAutorizador2.Enabled = valor

            'Autorizador 3
            .ddlAutorizador3T.Enabled = valor
            .txtUsuarioAut3B.Enabled = valor
            .cmdBuscarUsrAut3.Enabled = valor
            .ddlUsuarioAut3.Enabled = valor
            .ibtnAltaAut3.Enabled = valor
            .ibtnBajaAut3.Enabled = valor
            If valor = True Then
                .ibtnAltaAut3.ImageUrl = "images\Add.png"
                .ibtnBajaAut3.ImageUrl = "images\Trash.png"
            Else
                .ibtnAltaAut3.ImageUrl = "images\Add_i2.png"
                .ibtnBajaAut3.ImageUrl = "images\Trash_i2.png"
            End If
            .gvAutorizador3.Enabled = valor
        End With
    End Sub

    Public Sub camposUsrValAut(ByVal idTipo, ByRef texto, ByRef boton, ByRef lista, ByRef ibtnAlta, ByRef ibtnBaja, ByRef grid)
        With Me
            Select Case idTipo
                Case 0
                    texto.Visible = True
                    boton.Visible = True
                    lista.Visible = True
                    llenarListaUsrAut(texto.Text, lista)
                    ibtnAlta.Visible = True
                    ibtnBaja.Visible = True
                    grid.Visible = True
                Case 1
                    texto.Visible = True
                    boton.Visible = True
                    lista.Visible = True
                    llenarListaUsrAut(texto.Text, lista)
                    ibtnAlta.Visible = False
                    ibtnBaja.Visible = False
                    grid.Visible = False
                Case Else
                    texto.Visible = False
                    boton.Visible = False
                    lista.Visible = False
                    ibtnAlta.Visible = False
                    ibtnBaja.Visible = False
                    grid.Visible = False
            End Select
        End With
    End Sub

    Protected Sub gvConfiguracion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvConfiguracion.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                .ibtnBajaConf.Enabled = True
                .ibtnBajaConf.ImageUrl = "images\Trash.png"
                .ibtnModifConf.Enabled = True
                .ibtnModifConf.ImageUrl = "images\Edit.png"

                ._txtIdConfig.Text = .gvConfiguracion.SelectedRow.Cells(0).Text
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnAltaConf_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAltaConf.Click
        With Me
            ._txtTipoMovC.Text = "A"
            'General
            .pnlGeneral.Enabled = False
            .pnlTablaConfig.Visible = False
            .pnlConfig.Visible = True
            .btnAceptarC.Visible = True
            .pnlConfigVA.Visible = False

            .rblAdmonOper.SelectedIndex = -1
            .ddlEmpresa.SelectedIndex = -1
            .txtCuentaCont.Text = ""

            habilitarCampos(True)

            .btnAceptarConf.Visible = False
            .btnCancelarConf.Visible = False

            .btnAceptar.Visible = False
            .btnCancelar.Visible = False
        End With
    End Sub

    Protected Sub ibtnBajaConf_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBajaConf.Click
        With Me
            ._txtTipoMovC.Text = "B"
            'General
            .pnlGeneral.Enabled = False
            .pnlTablaConfig.Visible = False
            .pnlConfig.Visible = True
            .btnAceptarC.Visible = False
            .pnlConfigVA.Visible = True

            localizarConfig()
            habilitarCampos(False)

            .btnAceptarConf.Visible = True
            .btnCancelarConf.Visible = True

            .btnAceptar.Visible = False
            .btnCancelar.Visible = False
        End With
    End Sub

    Protected Sub ibtnModifConf_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnModifConf.Click
        With Me
            ._txtTipoMovC.Text = "M"
            'General
            .pnlGeneral.Enabled = False
            .pnlTablaConfig.Visible = False
            .pnlConfig.Visible = True
            .btnAceptarC.Visible = False
            .pnlConfigVA.Visible = True

            localizarConfig()
            habilitarCampos(True)

            .btnAceptarConf.Visible = True
            .btnCancelarConf.Visible = True

            .btnAceptar.Visible = False
            .btnCancelar.Visible = False
        End With
    End Sub

    Protected Sub gvAdjunto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvAdjunto.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                .ibtnBajaAdj.Enabled = True
                .ibtnBajaAdj.ImageUrl = "images\Trash.png"
                .ibtnModifAdj.Enabled = True
                .ibtnModifAdj.ImageUrl = "images\Edit.png"

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select adjunto " +
                                         "from dt_servicio_adj " +
                                         "where id_dt_servicio_adj = @id_dt_servicio_adj "
                SCMValores.Parameters.AddWithValue("@id_dt_servicio_adj", .gvAdjunto.SelectedRow.Cells(0).Text)
                ConexionBD.Open()
                .txtAdjunto.Text = SCMValores.ExecuteScalar
                ConexionBD.Close()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnAltaAdj_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAltaAdj.Click
        With Me
            Try
                .litError.Text = ""

                If .txtAdjunto.Text.Trim = "" Then
                    .litError.Text = "Favor de ingresar el nombre del adjunto"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into dt_servicio_adj ( id_servicio,  adjunto) " +
                                             "                     values (@id_servicio, @adjunto)"
                    SCMValores.Parameters.AddWithValue("@id_servicio", Val(._txtIdServicio.Text))
                    SCMValores.Parameters.AddWithValue("@adjunto", .txtAdjunto.Text.Trim)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    limpiarAdjuntos()
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnBajaAdj_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBajaAdj.Click
        With Me
            Try
                .litError.Text = ""

                If .gvAdjunto.SelectedIndex = -1 Then
                    .litError.Text = "Favor de seleccionar el adjunto a eliminar"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "delete from dt_servicio_adj where id_dt_servicio_adj = @id_dt_servicio_adj "
                    SCMValores.Parameters.AddWithValue("@id_dt_servicio_adj", .gvAdjunto.SelectedRow.Cells(0).Text)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    limpiarAdjuntos()
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnModifAdj_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnModifAdj.Click
        With Me
            Try
                .litError.Text = ""

                If .gvAdjunto.SelectedIndex = -1 Then
                    .litError.Text = "Favor de seleccionar el adjunto a modificar"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update dt_servicio_adj set adjunto = @adjunto where id_dt_servicio_adj = @id_dt_servicio_adj "
                    SCMValores.Parameters.AddWithValue("@adjunto", .txtAdjunto.Text.Trim)
                    SCMValores.Parameters.AddWithValue("@id_dt_servicio_adj", .gvAdjunto.SelectedRow.Cells(0).Text)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    limpiarAdjuntos()
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnAceptarConf_Click(sender As Object, e As EventArgs) Handles btnAceptarConf.Click
        With Me
            Try
                .litError.Text = ""
                Dim ban As Integer = 0
                If .txtCuentaCont.Text.Trim = "" Then
                    .litError.Text = "Información Insuficiente, favor de indicar la cuenta contable"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    Select Case ._txtTipoMovC.Text 'Tipo de Ajuste (alta, baja o modificación)
                        Case "B"
                            SCMValores.CommandText = "delete from dt_servicio_conf where id_dt_servicio_conf = @id_dt_servicio_conf"
                        Case Else
                            Dim SCMTemp As SqlCommand = New System.Data.SqlClient.SqlCommand
                            SCMTemp.Connection = ConexionBD
                            Dim conteo As Integer = 0
                            SCMTemp.CommandText = ""
                            SCMTemp.Parameters.Clear()
                            SCMTemp.CommandText = "SELECT COUNT(*) FROM dt_servicio_conf WHERE id_servicio = @id_servicio AND admon_oper = @admon_oper AND id_empresa = @id_empresa AND id_dt_servicio_conf <> @id_dt_servicio_conf "
                            SCMTemp.Parameters.AddWithValue("@id_dt_servicio_conf", Val(._txtIdConfig.Text))
                            SCMTemp.Parameters.AddWithValue("@id_servicio", Val(._txtIdServicio.Text))
                            SCMTemp.Parameters.AddWithValue("@admon_oper", .rblAdmonOper.SelectedValue)
                            SCMTemp.Parameters.AddWithValue("@id_empresa", .ddlEmpresa.SelectedValue)
                            ConexionBD.Open()
                            conteo = SCMTemp.ExecuteScalar
                            ConexionBD.Close()
                            If conteo > 0 Then
                                .litError.Text = "Valor Inválido, ya existe esa Configuración"
                                ban = 1
                            Else
                                SCMValores.CommandText = "update dt_servicio_conf set admon_oper = @admon_oper, id_empresa = @id_empresa, cuenta_cont = @cuenta_cont, id_usr_valida = @id_usr_valida, finanzas = @finanzas, valida_presup = @valida_presup, id_usr_valida2 = @id_usr_valida2, id_usr_autoriza1 = @id_usr_autoriza1, id_usr_autoriza2 = @id_usr_autoriza2, id_usr_autoriza3 = @id_usr_autoriza3 where id_dt_servicio_conf = @id_dt_servicio_conf "
                            End If
                    End Select
                    If ban = 0 Then
                        SCMValores.Parameters.AddWithValue("@id_dt_servicio_conf", Val(._txtIdConfig.Text))
                        SCMValores.Parameters.AddWithValue("@admon_oper", .rblAdmonOper.SelectedValue)
                        SCMValores.Parameters.AddWithValue("@id_empresa", .ddlEmpresa.SelectedValue)
                        SCMValores.Parameters.AddWithValue("@cuenta_cont", .txtCuentaCont.Text.Trim)
                        If .lbl_Validador1.Visible = True Then
                            If .ddlValidador1T.SelectedValue = 1 Then
                                SCMValores.Parameters.AddWithValue("@id_usr_valida", .ddlUsuarioVal1.SelectedValue)
                            Else
                                SCMValores.Parameters.AddWithValue("@id_usr_valida", .ddlValidador1T.SelectedValue)
                            End If
                        Else
                            SCMValores.Parameters.AddWithValue("@id_usr_valida", DBNull.Value)
                        End If
                        If .cbFinanzas.Visible = True Then
                            If .cbFinanzas.Checked = True Then
                                SCMValores.Parameters.AddWithValue("@finanzas", "S")
                            Else
                                SCMValores.Parameters.AddWithValue("@finanzas", "N")
                            End If
                        Else
                            SCMValores.Parameters.AddWithValue("@finanzas", "N")
                        End If
                        If .cbValidaPrep.Visible = True Then
                            If .cbValidaPrep.Checked = True Then
                                SCMValores.Parameters.AddWithValue("@valida_presup", "S")
                            Else
                                SCMValores.Parameters.AddWithValue("@valida_presup", "N")
                            End If
                        Else
                            SCMValores.Parameters.AddWithValue("@valida_presup", "N")
                        End If
                        If .lbl_Validador2.Visible = True Then
                            If .ddlValidador2T.SelectedValue = 1 Then
                                SCMValores.Parameters.AddWithValue("@id_usr_valida2", .ddlUsuarioVal2.SelectedValue)
                            Else
                                SCMValores.Parameters.AddWithValue("@id_usr_valida2", .ddlValidador2T.SelectedValue)
                            End If
                        Else
                            SCMValores.Parameters.AddWithValue("@id_usr_valida2", DBNull.Value)
                        End If
                        If .ddlAutorizador1T.SelectedValue = 1 Then
                            SCMValores.Parameters.AddWithValue("@id_usr_autoriza1", .ddlUsuarioAut1.SelectedValue)
                        Else
                            SCMValores.Parameters.AddWithValue("@id_usr_autoriza1", .ddlAutorizador1T.SelectedValue)
                        End If
                        If .ddlAutorizador2T.SelectedValue = 1 Then
                            SCMValores.Parameters.AddWithValue("@id_usr_autoriza2", .ddlUsuarioAut2.SelectedValue)
                        Else
                            SCMValores.Parameters.AddWithValue("@id_usr_autoriza2", .ddlAutorizador2T.SelectedValue)
                        End If
                        If .ddlAutorizador3T.SelectedValue = 1 Then
                            SCMValores.Parameters.AddWithValue("@id_usr_autoriza3", .ddlUsuarioAut3.SelectedValue)
                        Else
                            SCMValores.Parameters.AddWithValue("@id_usr_autoriza3", .ddlAutorizador3T.SelectedValue)
                        End If

                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        limpiarPantallaConfig()
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnCancelarConf_Click(sender As Object, e As EventArgs) Handles btnCancelarConf.Click
        limpiarPantallaConfig()
    End Sub

#End Region

#Region "Validadores / Autorizadores"

    Public Sub camposXtipo()
        With Me
            Select Case .ddlTipo.SelectedValue
                Case "Servicios Específicos"  'Cotización Única
                    .cbFinanzas.Visible = False
                    .cbFinanzas.Checked = False
                    .cbValidaPrep.Visible = False
                    .cbValidaPrep.Checked = False

                    .lbl_Validador1.Visible = False
                    .ddlValidador1T.Visible = False
                    .ddlValidador1T.SelectedIndex = -1
                    camposUsrValAut(-99, .txtUsuarioVal1B, .cmdBuscarUsrVal1, .ddlUsuarioVal1, .ibtnAltaVal1, .ibtnBajaVal1, .gvValidador1)
                    If .ddlUsuarioVal1.Visible = True Then
                        .txtUsuarioVal1B.Text = ""
                        llenarListaUsrAut(.txtUsuarioVal1B.Text, .ddlUsuarioVal1)
                    End If
                    .lbl_Validador2.Visible = False
                    .ddlValidador2T.Visible = False
                    .ddlValidador2T.SelectedIndex = -1
                    camposUsrValAut(-99, .txtUsuarioVal2B, .cmdBuscarUsrVal2, .ddlUsuarioVal2, .ibtnAltaVal2, .ibtnBajaVal2, .gvValidador2)
                    If .ddlUsuarioVal2.Visible = True Then
                        .txtUsuarioVal2B.Text = ""
                        llenarListaUsrAut(.txtUsuarioVal2B.Text, .ddlUsuarioVal2)
                    End If
                Case Else
                    .cbFinanzas.Visible = True
                    .cbFinanzas.Checked = True
                    .cbValidaPrep.Visible = True
                    .cbValidaPrep.Checked = True

                    .lbl_Validador1.Visible = True
                    .ddlValidador1T.Visible = True
                    .ddlValidador1T.SelectedIndex = -1
                    .txtUsuarioVal1B.Text = ""
                    camposUsrValAut(.ddlValidador1T.SelectedValue, .txtUsuarioVal1B, .cmdBuscarUsrVal1, .ddlUsuarioVal1, .ibtnAltaVal1, .ibtnBajaVal1, .gvValidador1)
                    If .ddlTipo.SelectedValue = "Servicios Negociados" Then
                        .lbl_Validador2.Visible = True
                        .ddlValidador2T.Visible = True
                        .ddlValidador2T.SelectedIndex = -1
                        .txtUsuarioVal2B.Text = ""
                        camposUsrValAut(.ddlValidador2T.SelectedValue, .txtUsuarioVal2B, .cmdBuscarUsrVal2, .ddlUsuarioVal2, .ibtnAltaVal2, .ibtnBajaVal2, .gvValidador2)
                    Else
                        .lbl_Validador2.Visible = False
                        .ddlValidador2T.Visible = False
                        .ddlValidador2T.SelectedIndex = -1
                        camposUsrValAut(-99, .txtUsuarioVal2B, .cmdBuscarUsrVal2, .ddlUsuarioVal2, .ibtnAltaVal2, .ibtnBajaVal2, .gvValidador2)
                    End If
            End Select

            .ddlAutorizador1T.SelectedIndex = -1
            camposUsrValAut(.ddlAutorizador1T.SelectedValue, .txtUsuarioAut1B, .cmdBuscarUsrAut1, .ddlUsuarioAut1, .ibtnAltaAut1, .ibtnBajaAut1, .gvAutorizador1)
            If .ddlUsuarioAut1.Visible = True Then
                .txtUsuarioAut1B.Text = ""
                llenarListaUsrAut(.txtUsuarioAut1B.Text, .ddlUsuarioAut1)
            End If
            .ddlAutorizador2T.SelectedIndex = -1
            camposUsrValAut(.ddlAutorizador2T.SelectedValue, .txtUsuarioAut2B, .cmdBuscarUsrAut2, .ddlUsuarioAut2, .ibtnAltaAut2, .ibtnBajaAut2, .gvAutorizador2)
            If .ddlUsuarioAut2.Visible = True Then
                .txtUsuarioAut2B.Text = ""
                llenarListaUsrAut(.txtUsuarioAut2B.Text, .ddlUsuarioAut2)
            End If
            .ddlAutorizador3T.SelectedIndex = -1
            camposUsrValAut(.ddlAutorizador3T.SelectedValue, .txtUsuarioAut3B, .cmdBuscarUsrAut3, .ddlUsuarioAut3, .ibtnAltaAut3, .ibtnBajaAut3, .gvAutorizador3)
            If .ddlUsuarioAut3.Visible = True Then
                .txtUsuarioAut3B.Text = ""
                llenarListaUsrAut(.txtUsuarioAut3B.Text, .ddlUsuarioAut3)
            End If
        End With
    End Sub

    Public Sub cambioTipoValAut(ByVal tipoVA, ByVal tipo)
        With Me
            Try
                .litError.Text = ""

                If tipo <> 0 Then
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "delete from dt_servicio_aut where id_dt_servicio_conf = @id_dt_servicio_conf and tipo = @tipo"
                    SCMValores.Parameters.AddWithValue("@id_dt_servicio_conf", Val(._txtIdConfig.Text))
                    SCMValores.Parameters.AddWithValue("@tipo", tipoVA)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarTablaAut(ByVal idConfig, ByVal tipo, ByRef tabla)
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaAut As New SqlDataAdapter
                Dim dsAut As New DataSet
                tabla.Columns(0).Visible = True
                tabla.DataSource = dsAut
                'Validadores / Autorizadores
                sdaAut.SelectCommand = New SqlCommand("select cg_usuario.id_usuario " +
                                                      "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as usuario " +
                                                      "from dt_servicio_aut " +
                                                      "  left join cg_usuario on dt_servicio_aut.id_usuario = cg_usuario.id_usuario " +
                                                      "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " +
                                                      "where id_dt_servicio_conf = @id_dt_servicio_conf " +
                                                      "  and tipo = @tipo ", ConexionBD)
                sdaAut.SelectCommand.Parameters.AddWithValue("@id_dt_servicio_conf", idConfig)
                sdaAut.SelectCommand.Parameters.AddWithValue("@tipo", tipo)
                ConexionBD.Open()
                sdaAut.Fill(dsAut)
                tabla.DataBind()
                ConexionBD.Close()
                sdaAut.Dispose()
                dsAut.Dispose()
                tabla.Columns(0).Visible = False
                tabla.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub localizarConfig()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaConfig As New SqlDataAdapter
                Dim dsConfig As New DataSet
                sdaConfig.SelectCommand = New SqlCommand("select admon_oper " +
                                                         "     , id_empresa " +
                                                         "     , cuenta_cont " +
                                                         "     , id_usr_valida " +
                                                         "     , finanzas " +
                                                         "     , valida_presup " +
                                                         "     , id_usr_valida2 " +
                                                         "     , id_usr_autoriza1 " +
                                                         "     , id_usr_autoriza2 " +
                                                         "     , id_usr_autoriza3 " +
                                                         "from dt_servicio_conf " +
                                                         "where id_dt_servicio_conf = @idConfig ", ConexionBD)
                sdaConfig.SelectCommand.Parameters.AddWithValue("@idConfig", Val(._txtIdConfig.Text))
                ConexionBD.Open()
                sdaConfig.Fill(dsConfig)
                ConexionBD.Close()
                .rblAdmonOper.SelectedValue = dsConfig.Tables(0).Rows(0).Item("admon_oper").ToString()
                .ddlEmpresa.SelectedValue = dsConfig.Tables(0).Rows(0).Item("id_empresa").ToString()
                .txtCuentaCont.Text = dsConfig.Tables(0).Rows(0).Item("cuenta_cont").ToString()

                camposXtipo()

                If .lbl_Validador1.Visible = True Then
                    .txtUsuarioVal1B.Text = ""
                    If Val(dsConfig.Tables(0).Rows(0).Item("id_usr_valida").ToString()) = -99 Then
                        .ddlValidador1T.Visible = False
                        camposUsrValAut(-99, .txtUsuarioVal1B, .cmdBuscarUsrVal1, .ddlUsuarioVal1, .ibtnAltaVal1, .ibtnBajaVal1, .gvValidador1)
                    Else
                        .ddlValidador1T.Visible = True
                        If Val(dsConfig.Tables(0).Rows(0).Item("id_usr_valida").ToString()) > 0 Then
                            .ddlValidador1T.SelectedValue = 1
                        Else
                            .ddlValidador1T.SelectedValue = Val(dsConfig.Tables(0).Rows(0).Item("id_usr_valida").ToString())
                        End If
                        camposUsrValAut(.ddlValidador1T.SelectedValue, .txtUsuarioVal1B, .cmdBuscarUsrVal1, .ddlUsuarioVal1, .ibtnAltaVal1, .ibtnBajaVal1, .gvValidador1)
                        If .ddlUsuarioVal1.Visible = True Then
                            llenarListaUsrAut(.txtUsuarioVal1B.Text, .ddlUsuarioVal1)
                            If .ddlValidador1T.SelectedValue = 1 Then
                                .ddlUsuarioVal1.SelectedValue = Val(dsConfig.Tables(0).Rows(0).Item("id_usr_valida").ToString())
                            End If
                        End If
                        If .gvValidador1.Visible = True Then
                            llenarTablaAut(Val(._txtIdConfig.Text), "V1", .gvValidador1)
                        End If
                    End If
                End If

                If .cbFinanzas.Visible = True Then
                    If dsConfig.Tables(0).Rows(0).Item("finanzas").ToString() = "S" Then
                        .cbFinanzas.Checked = True
                    Else
                        .cbFinanzas.Checked = False
                    End If
                End If
                If .cbValidaPrep.Visible = True Then
                    If dsConfig.Tables(0).Rows(0).Item("valida_presup").ToString() = "S" Then
                        .cbValidaPrep.Checked = True
                    Else
                        .cbValidaPrep.Checked = False
                    End If
                End If

                If .lbl_Validador2.Visible = True Then
                    .txtUsuarioVal2B.Text = ""
                    If Val(dsConfig.Tables(0).Rows(0).Item("id_usr_valida2").ToString()) = -99 Then
                        .ddlValidador2T.Visible = False
                        camposUsrValAut(-99, .txtUsuarioVal2B, .cmdBuscarUsrVal2, .ddlUsuarioVal2, .ibtnAltaVal2, .ibtnBajaVal2, .gvValidador2)
                    Else
                        .ddlValidador2T.Visible = True
                        If Val(dsConfig.Tables(0).Rows(0).Item("id_usr_valida2").ToString()) > 0 Then
                            .ddlValidador2T.SelectedValue = 1
                        Else
                            .ddlValidador2T.SelectedValue = Val(dsConfig.Tables(0).Rows(0).Item("id_usr_valida2").ToString())
                        End If
                        camposUsrValAut(.ddlValidador2T.SelectedValue, .txtUsuarioVal2B, .cmdBuscarUsrVal2, .ddlUsuarioVal2, .ibtnAltaVal2, .ibtnBajaVal2, .gvValidador2)
                        If .ddlUsuarioVal2.Visible = True Then
                            llenarListaUsrAut(.txtUsuarioVal2B.Text, .ddlUsuarioVal2)
                            If .ddlValidador2T.SelectedValue = 1 Then
                                .ddlUsuarioVal2.SelectedValue = Val(dsConfig.Tables(0).Rows(0).Item("id_usr_valida2").ToString())
                            End If
                        End If
                        If .gvValidador2.Visible = True Then
                            llenarTablaAut(Val(._txtIdConfig.Text), "V2", .gvValidador2)
                        End If
                    End If
                End If

                .txtUsuarioAut1B.Text = ""
                If Val(dsConfig.Tables(0).Rows(0).Item("id_usr_autoriza1").ToString()) = -99 Then
                    .ddlAutorizador1T.Visible = False
                    camposUsrValAut(-99, .txtUsuarioAut1B, .cmdBuscarUsrAut1, .ddlUsuarioAut1, .ibtnAltaAut1, .ibtnBajaAut1, .gvAutorizador1)
                Else
                    .ddlAutorizador1T.Visible = True
                    If Val(dsConfig.Tables(0).Rows(0).Item("id_usr_autoriza1").ToString()) > 0 Then
                        .ddlAutorizador1T.SelectedValue = 1
                    Else
                        .ddlAutorizador1T.SelectedValue = Val(dsConfig.Tables(0).Rows(0).Item("id_usr_autoriza1").ToString())
                    End If
                    camposUsrValAut(.ddlAutorizador1T.SelectedValue, .txtUsuarioAut1B, .cmdBuscarUsrAut1, .ddlUsuarioAut1, .ibtnAltaAut1, .ibtnBajaAut1, .gvAutorizador1)
                    If .ddlUsuarioAut1.Visible = True Then
                        llenarListaUsrAut(.txtUsuarioAut1B.Text, .ddlUsuarioAut1)
                        If .ddlAutorizador1T.SelectedValue = 1 Then
                            .ddlUsuarioAut1.SelectedValue = Val(dsConfig.Tables(0).Rows(0).Item("id_usr_autoriza1").ToString())
                        End If
                    End If
                    If .gvAutorizador1.Visible = True Then
                        llenarTablaAut(Val(._txtIdConfig.Text), "A1", .gvAutorizador1)
                    End If
                End If

                .txtUsuarioAut2B.Text = ""
                If Val(dsConfig.Tables(0).Rows(0).Item("id_usr_autoriza2").ToString()) = -99 Then
                    .ddlAutorizador2T.Visible = False
                    camposUsrValAut(-99, .txtUsuarioAut2B, .cmdBuscarUsrAut2, .ddlUsuarioAut2, .ibtnAltaAut2, .ibtnBajaAut2, .gvAutorizador2)
                Else
                    .ddlAutorizador2T.Visible = True
                    If Val(dsConfig.Tables(0).Rows(0).Item("id_usr_autoriza2").ToString()) > 0 Then
                        .ddlAutorizador2T.SelectedValue = 1
                    Else
                        .ddlAutorizador2T.SelectedValue = Val(dsConfig.Tables(0).Rows(0).Item("id_usr_autoriza2").ToString())
                    End If
                    camposUsrValAut(.ddlAutorizador2T.SelectedValue, .txtUsuarioAut2B, .cmdBuscarUsrAut2, .ddlUsuarioAut2, .ibtnAltaAut2, .ibtnBajaAut2, .gvAutorizador2)
                    If .ddlUsuarioAut2.Visible = True Then
                        llenarListaUsrAut(.txtUsuarioAut2B.Text, .ddlUsuarioAut2)
                        If .ddlAutorizador2T.SelectedValue = 1 Then
                            .ddlUsuarioAut2.SelectedValue = Val(dsConfig.Tables(0).Rows(0).Item("id_usr_autoriza2").ToString())
                        End If
                    End If
                    If .gvAutorizador2.Visible = True Then
                        llenarTablaAut(Val(._txtIdConfig.Text), "A2", .gvAutorizador2)
                    End If
                End If

                .txtUsuarioAut3B.Text = ""
                If Val(dsConfig.Tables(0).Rows(0).Item("id_usr_autoriza3").ToString()) = -99 Then
                    .ddlAutorizador3T.Visible = False
                    camposUsrValAut(-99, .txtUsuarioAut3B, .cmdBuscarUsrAut3, .ddlUsuarioAut3, .ibtnAltaAut3, .ibtnBajaAut3, .gvAutorizador3)
                Else
                    .ddlAutorizador3T.Visible = True
                    If Val(dsConfig.Tables(0).Rows(0).Item("id_usr_autoriza3").ToString()) > 0 Then
                        .ddlAutorizador3T.SelectedValue = 1
                    Else
                        .ddlAutorizador3T.SelectedValue = Val(dsConfig.Tables(0).Rows(0).Item("id_usr_autoriza3").ToString())
                    End If
                    camposUsrValAut(.ddlAutorizador3T.SelectedValue, .txtUsuarioAut3B, .cmdBuscarUsrAut3, .ddlUsuarioAut3, .ibtnAltaAut3, .ibtnBajaAut3, .gvAutorizador3)
                    If .ddlUsuarioAut3.Visible = True Then
                        llenarListaUsrAut(.txtUsuarioAut3B.Text, .ddlUsuarioAut3)
                        If .ddlAutorizador3T.SelectedValue = 1 Then
                            .ddlUsuarioAut3.SelectedValue = Val(dsConfig.Tables(0).Rows(0).Item("id_usr_autoriza3").ToString())
                        End If
                    End If
                    If .gvAutorizador3.Visible = True Then
                        llenarTablaAut(Val(._txtIdConfig.Text), "A3", .gvAutorizador3)
                    End If
                End If

                sdaConfig.Dispose()
                dsConfig.Dispose()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Function validarConfig()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMTemp As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMTemp.Connection = ConexionBD
                Dim conteo As Integer = 0
                SCMTemp.CommandText = ""
                SCMTemp.Parameters.Clear()
                Select Case ._txtTipoMovC.Text 'Tipo de Ajuste (alta, baja o modificación)
                    Case "A"
                        SCMTemp.CommandText = "SELECT COUNT(*) FROM dt_servicio_conf WHERE id_servicio = @id_servicio AND admon_oper = @admon_oper AND id_empresa = @id_empresa "
                    Case Else
                        SCMTemp.CommandText = "SELECT COUNT(*) FROM dt_servicio_conf WHERE id_servicio = @id_servicio AND admon_oper = @admon_oper AND id_empresa = @id_empresa AND id_dt_servicio_conf <> @id_dt_servicio_conf "
                        SCMTemp.Parameters.AddWithValue("@id_dt_servicio_conf", Val(._txtIdConfig.Text))
                End Select
                SCMTemp.Parameters.AddWithValue("@id_servicio", Val(._txtIdServicio.Text))
                SCMTemp.Parameters.AddWithValue("@admon_oper", .rblAdmonOper.SelectedValue)
                SCMTemp.Parameters.AddWithValue("@id_empresa", .ddlEmpresa.SelectedValue)
                ConexionBD.Open()
                conteo = SCMTemp.ExecuteScalar
                ConexionBD.Close()
                If conteo > 0 Then
                    validarConfig = False
                Else
                    validarConfig = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
                validarConfig = False
            End Try
        End With
    End Function

    Protected Sub btnAceptarC_Click(sender As Object, e As EventArgs) Handles btnAceptarC.Click
        With Me
            Try
                .litError.Text = ""
                Dim ban As Integer = 0
                If .rblAdmonOper.SelectedIndex = -1 Then
                    .litError.Text = "Información Insuficiente, favor de indicar si la configuración es para Administración, Operacióno aplica para Ambos"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    If validarConfig() Then
                        SCMValores.CommandText = "INSERT INTO dt_servicio_conf ( id_servicio,  admon_oper,  id_empresa,  cuenta_cont,  id_usr_valida,  finanzas,  valida_presup,  id_usr_autoriza1,  id_usr_autoriza2,  id_usr_autoriza3) " +
                                                 "                      values (@id_servicio, @admon_oper, @id_empresa, @cuenta_cont,              0,       'S',            'S',                -1,                -3,                -3)"
                    Else
                        .litError.Text = "Valor Inválido, ya existe esa configuración"
                        ban = 1
                    End If
                    If ban = 0 Then
                        SCMValores.Parameters.AddWithValue("@id_servicio", Val(._txtIdServicio.Text))
                        SCMValores.Parameters.AddWithValue("@admon_oper", .rblAdmonOper.SelectedValue)
                        SCMValores.Parameters.AddWithValue("@id_empresa", .ddlEmpresa.SelectedValue)
                        SCMValores.Parameters.AddWithValue("@cuenta_cont", .txtCuentaCont.Text.Trim)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Obtener el ID Configuración (id_dt_servicio_conf)
                        SCMValores.CommandText = ""
                        SCMValores.CommandText = "select isnull(max(id_dt_servicio_conf), 0) from dt_servicio_conf WHERE id_servicio = @id_servicio and admon_oper = @admon_oper and id_empresa = @id_empresa "
                        ConexionBD.Open()
                        ._txtIdConfig.Text = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        .btnAceptarC.Visible = False
                        .pnlConfigVA.Visible = True

                        camposXtipo()

                        .btnAceptarConf.Visible = True
                        .btnCancelarConf.Visible = True
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ddlValidador1T_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlValidador1T.SelectedIndexChanged
        With Me
            cambioTipoValAut("V1", .ddlValidador1T.SelectedValue)
            camposUsrValAut(.ddlValidador1T.SelectedValue, .txtUsuarioVal1B, .cmdBuscarUsrVal1, .ddlUsuarioVal1, .ibtnAltaVal1, .ibtnBajaVal1, .gvValidador1)
            If .ddlUsuarioVal1.Visible = True Then
                llenarListaUsrAut(.txtUsuarioVal1B.Text, .ddlUsuarioVal1)
            End If
        End With
    End Sub

    Protected Sub cmdBuscarUsrVal1_Click(sender As Object, e As EventArgs) Handles cmdBuscarUsrVal1.Click
        llenarListaUsrAut(Me.txtUsuarioVal1B.Text, Me.ddlUsuarioVal1)
    End Sub

    Protected Sub ibtnAltaVal1_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAltaVal1.Click
        aceptarUsrAut(Me.ddlUsuarioVal1, "V1", Me.gvValidador1, "A")
    End Sub

    Protected Sub ibtnBajaVal1_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBajaVal1.Click
        If Me.gvValidador1.SelectedIndex = -1 Then
            Me.litError.Text = "Favor de seleccionar el Validador a Eliminar"
        Else
            aceptarUsrAut(Me.ddlUsuarioVal1, "V1", Me.gvValidador1, "B")
        End If
    End Sub

    Protected Sub ddlValidador2T_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlValidador2T.SelectedIndexChanged
        With Me
            cambioTipoValAut("V2", .ddlValidador2T.SelectedValue)
            camposUsrValAut(.ddlValidador2T.SelectedValue, .txtUsuarioVal2B, .cmdBuscarUsrVal2, .ddlUsuarioVal2, .ibtnAltaVal2, .ibtnBajaVal2, .gvValidador2)
            If .ddlUsuarioVal2.Visible = True Then
                llenarListaUsrAut(.txtUsuarioVal2B.Text, .ddlUsuarioVal2)
            End If
        End With
    End Sub

    Protected Sub cmdBuscarUsrVal2_Click(sender As Object, e As EventArgs) Handles cmdBuscarUsrVal2.Click
        llenarListaUsrAut(Me.txtUsuarioVal2B.Text, Me.ddlUsuarioVal2)
    End Sub

    Protected Sub ibtnAltaVal2_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAltaVal2.Click
        aceptarUsrAut(Me.ddlUsuarioVal2, "V2", Me.gvValidador2, "A")
    End Sub

    Protected Sub ibtnBajaVal2_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBajaVal2.Click
        If Me.gvValidador2.SelectedIndex = -1 Then
            Me.litError.Text = "Favor de seleccionar el Validador a Eliminar"
        Else
            aceptarUsrAut(Me.ddlUsuarioVal2, "V2", Me.gvValidador2, "B")
        End If
    End Sub

    Protected Sub ddlAutorizador1T_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAutorizador1T.SelectedIndexChanged
        With Me
            cambioTipoValAut("A1", .ddlAutorizador1T.SelectedValue)
            camposUsrValAut(.ddlAutorizador1T.SelectedValue, .txtUsuarioAut1B, .cmdBuscarUsrAut1, .ddlUsuarioAut1, .ibtnAltaAut1, .ibtnBajaAut1, .gvAutorizador1)
            If .ddlUsuarioAut1.Visible = True Then
                llenarListaUsrAut(.txtUsuarioAut1B.Text, .ddlUsuarioAut1)
            End If
        End With
    End Sub

    Protected Sub cmdBuscarUsrAut1_Click(sender As Object, e As EventArgs) Handles cmdBuscarUsrAut1.Click
        llenarListaUsrAut(Me.txtUsuarioAut1B.Text, Me.ddlUsuarioAut1)
    End Sub

    Protected Sub ibtnAltaAut1_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAltaAut1.Click
        aceptarUsrAut(Me.ddlUsuarioAut1, "A1", Me.gvAutorizador1, "A")
    End Sub

    Protected Sub ibtnBajaAut1_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBajaAut1.Click
        If Me.gvAutorizador1.SelectedIndex = -1 Then
            Me.litError.Text = "Favor de seleccionar el Autorizador a Eliminar"
        Else
            aceptarUsrAut(Me.ddlUsuarioAut1, "A1", Me.gvAutorizador1, "B")
        End If
    End Sub

    Protected Sub ddlAutorizador2T_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAutorizador2T.SelectedIndexChanged
        With Me
            cambioTipoValAut("A2", .ddlAutorizador2T.SelectedValue)
            camposUsrValAut(.ddlAutorizador2T.SelectedValue, .txtUsuarioAut2B, .cmdBuscarUsrAut2, .ddlUsuarioAut2, .ibtnAltaAut2, .ibtnBajaAut2, .gvAutorizador2)
            If .ddlUsuarioAut2.Visible = True Then
                llenarListaUsrAut(.txtUsuarioAut2B.Text, .ddlUsuarioAut2)
            End If
        End With
    End Sub

    Protected Sub cmdBuscarUsrAut2_Click(sender As Object, e As EventArgs) Handles cmdBuscarUsrAut2.Click
        llenarListaUsrAut(Me.txtUsuarioAut2B.Text, Me.ddlUsuarioAut2)
    End Sub

    Protected Sub ibtnAltaAut2_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAltaAut2.Click
        aceptarUsrAut(Me.ddlUsuarioAut2, "A2", Me.gvAutorizador2, "A")
    End Sub

    Protected Sub ibtnBajaAut2_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBajaAut2.Click
        If Me.gvAutorizador2.SelectedIndex = -1 Then
            Me.litError.Text = "Favor de seleccionar el Autorizador a Eliminar"
        Else
            aceptarUsrAut(Me.ddlUsuarioAut2, "A2", Me.gvAutorizador2, "B")
        End If
    End Sub

    Protected Sub ddlAutorizador3T_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAutorizador3T.SelectedIndexChanged
        With Me
            cambioTipoValAut("A3", .ddlAutorizador3T.SelectedValue)
            camposUsrValAut(.ddlAutorizador3T.SelectedValue, .txtUsuarioAut3B, .cmdBuscarUsrAut3, .ddlUsuarioAut3, .ibtnAltaAut3, .ibtnBajaAut3, .gvAutorizador3)
            If .ddlUsuarioAut3.Visible = True Then
                llenarListaUsrAut(.txtUsuarioAut3B.Text, .ddlUsuarioAut3)
            End If
        End With
    End Sub

    Protected Sub cmdBuscarUsrAut3_Click(sender As Object, e As EventArgs) Handles cmdBuscarUsrAut3.Click
        llenarListaUsrAut(Me.txtUsuarioAut3B.Text, Me.ddlUsuarioAut3)
    End Sub

    Protected Sub ibtnAltaAut3_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAltaAut3.Click
        aceptarUsrAut(Me.ddlUsuarioAut3, "A3", Me.gvAutorizador3, "A")
    End Sub

    Protected Sub ibtnBajaAut3_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBajaAut3.Click
        If Me.gvAutorizador3.SelectedIndex = -1 Then
            Me.litError.Text = "Favor de seleccionar el Autorizador a Eliminar"
        Else
            aceptarUsrAut(Me.ddlUsuarioAut3, "A3", Me.gvAutorizador3, "B")
        End If
    End Sub

#End Region

End Class