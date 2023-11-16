Public Class ConsEval
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 55  ' 1 - Liliana | 5 - Octavio | 45 - Lic Marco | 64 - Gerardo | 3 - Jessica Alfaro | 55 -  Javier Sanchez

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        Dim sdaEmpleado As New SqlDataAdapter
                        Dim dsEmpleado As New DataSet
                        sdaEmpleado.SelectCommand = New SqlCommand("select perfil, nombre + ' ' + ap_paterno + ' ' + ap_materno as empleado, format(cast(cgEmpl.no_empleado as int), '0000000', 'es-MX') as no_empleado " + _
                                                                   "from cg_usuario " + _
                                                                   "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                                   "where id_usuario = @idUsuario ", ConexionBD)
                        sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        sdaEmpleado.Fill(dsEmpleado)
                        ConexionBD.Close()
                        ._txtPerfil.Text = dsEmpleado.Tables(0).Rows(0).Item("perfil").ToString()
                        ._txtEmpleado.Text = dsEmpleado.Tables(0).Rows(0).Item("empleado").ToString()
                        ._txtNoEmpleado.Text = dsEmpleado.Tables(0).Rows(0).Item("no_empleado").ToString()
                        sdaEmpleado.Dispose()
                        dsEmpleado.Dispose()

                        Dim contPnl As Integer = 0
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "select count(*) " + _
                                                 "from ms_evaluacion " + _
                                                 "  left join cg_usuario cgUsrEval on ms_evaluacion.id_usr_registro = cgUsrEval.id_usuario " + _
                                                 "  left join cg_usuario cgUsrVal on ms_evaluacion.id_usr_valida = cgUsrVal.id_usuario " + _
                                                 "  left join cg_usuario cgUsrDir on ms_evaluacion.id_usr_director = cgUsrDir.id_usuario " + _
                                                 "where cgUsrEval.id_usuario = @idUsuario " + _
                                                 "   or cgUsrVal.id_usuario = @idUsuario " + _
                                                 "   or cgUsrDir.id_usuario = @idUsuario "
                        SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        contPnl = SCMValores.ExecuteScalar()
                        ConexionBD.Close()
                        If contPnl = 0 And Not (._txtPerfil.Text = "DesOrg" Or ._txtPerfil.Text = "JefInfo") Then
                            .pnlSolicitó.Visible = False
                        Else
                            .pnlSolicitó.Visible = True
                            .cbEmpresa.Checked = False
                            .pnlEmpresa.Visible = False
                            .cbDireccion.Checked = False
                            .pnlDireccion.Visible = False
                            .cbEmpleado.Checked = False
                            .pnlEmpleado.Visible = False
                        End If

                        'Llenar listas
                        Dim query As String
                        'Empresas
                        Dim sdaEmpresa As New SqlDataAdapter
                        Dim dsEmpresa As New DataSet
                        query = "select distinct(empresa) as empresa " + _
                                "from ms_evaluacion "
                        If ._txtPerfil.Text <> "DesOrg" And ._txtPerfil.Text <> "JefInfo" Then
                            query = query + "  left join cg_usuario cgUsrEval on ms_evaluacion.id_usr_registro = cgUsrEval.id_usuario " + _
                                            "  left join cg_usuario cgUsrVal on ms_evaluacion.id_usr_valida = cgUsrVal.id_usuario " + _
                                            "  left join cg_usuario cgUsrDir on ms_evaluacion.id_usr_director = cgUsrDir.id_usuario " + _
                                            "where cgUsrEval.id_usuario = @idUsuario " + _
                                            "   or cgUsrVal.id_usuario = @idUsuario " + _
                                            "   or cgUsrDir.id_usuario = @idUsuario "
                        End If
                        query = query + "order by empresa "
                        sdaEmpresa.SelectCommand = New SqlCommand(query, ConexionBD)
                        If ._txtPerfil.Text <> "DesOrg" And ._txtPerfil.Text <> "JefInfo" Then
                            sdaEmpresa.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        End If
                        .ddlEmpresa.DataSource = dsEmpresa
                        .ddlEmpresa.DataTextField = "empresa"
                        .ddlEmpresa.DataValueField = "empresa"
                        ConexionBD.Open()
                        sdaEmpresa.Fill(dsEmpresa)
                        .ddlEmpresa.DataBind()
                        ConexionBD.Close()
                        sdaEmpresa.Dispose()
                        dsEmpresa.Dispose()
                        .ddlEmpresa.SelectedIndex = -1

                        'Direcciones
                        Dim sdaDireccion As New SqlDataAdapter
                        Dim dsDireccion As New DataSet
                        query = "select distinct(direccion) as direccion " + _
                                "from ms_evaluacion "
                        If ._txtPerfil.Text <> "DesOrg" And ._txtPerfil.Text <> "JefInfo" Then
                            query = query + "  left join cg_usuario cgUsrEval on ms_evaluacion.id_usr_registro = cgUsrEval.id_usuario " + _
                                            "  left join cg_usuario cgUsrVal on ms_evaluacion.id_usr_valida = cgUsrVal.id_usuario " + _
                                            "  left join cg_usuario cgUsrDir on ms_evaluacion.id_usr_director = cgUsrDir.id_usuario " + _
                                            "where cgUsrEval.id_usuario = @idUsuario " + _
                                            "   or cgUsrVal.id_usuario = @idUsuario " + _
                                            "   or cgUsrDir.id_usuario = @idUsuario "
                        End If
                        query = query + "order by direccion "
                        sdaDireccion.SelectCommand = New SqlCommand(query, ConexionBD)
                        If ._txtPerfil.Text <> "DesOrg" And ._txtPerfil.Text <> "JefInfo" Then
                            sdaDireccion.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        End If
                        .ddlDireccion.DataSource = dsDireccion
                        .ddlDireccion.DataTextField = "direccion"
                        .ddlDireccion.DataValueField = "direccion"
                        ConexionBD.Open()
                        sdaDireccion.Fill(dsDireccion)
                        .ddlDireccion.DataBind()
                        ConexionBD.Close()
                        sdaDireccion.Dispose()
                        dsDireccion.Dispose()
                        .ddlDireccion.SelectedIndex = -1

                        'Empleados Evaluados
                        Dim sdaEmpleadoE As New SqlDataAdapter
                        Dim dsEmpleadoE As New DataSet
                        query = "select distinct(no_empleado) " + _
                                "     , ms_evaluacion.nombre + ' ' + ms_evaluacion.ap_paterno + ' ' + ms_evaluacion.ap_materno as nombre_empleado " + _
                                "     , '[' + no_empleado + '] ' + ms_evaluacion.nombre + ' ' + ms_evaluacion.ap_paterno + ' ' + ms_evaluacion.ap_materno as empleado " + _
                                "from ms_evaluacion "
                        If ._txtPerfil.Text <> "DesOrg" And ._txtPerfil.Text <> "JefInfo" Then
                            query = query + "  left join cg_usuario cgUsrEval on ms_evaluacion.id_usr_registro = cgUsrEval.id_usuario " + _
                                            "  left join cg_usuario cgUsrVal on ms_evaluacion.id_usr_valida = cgUsrVal.id_usuario " + _
                                            "  left join cg_usuario cgUsrDir on ms_evaluacion.id_usr_director = cgUsrDir.id_usuario " + _
                                            "where cgUsrEval.id_usuario = @idUsuario " + _
                                            "   or cgUsrVal.id_usuario = @idUsuario " + _
                                            "   or cgUsrDir.id_usuario = @idUsuario "
                        End If
                        query = query + "order by nombre_empleado "
                        sdaEmpleadoE.SelectCommand = New SqlCommand(query, ConexionBD)
                        If ._txtPerfil.Text <> "DesOrg" And ._txtPerfil.Text <> "JefInfo" Then
                            sdaEmpleadoE.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        End If
                        .ddlEmpleado.DataSource = dsEmpleadoE
                        .ddlEmpleado.DataTextField = "empleado"
                        .ddlEmpleado.DataValueField = "no_empleado"
                        ConexionBD.Open()
                        sdaEmpleadoE.Fill(dsEmpleadoE)
                        .ddlEmpleado.DataBind()
                        ConexionBD.Close()
                        sdaEmpleadoE.Dispose()
                        dsEmpleadoE.Dispose()
                        .ddlEmpleado.SelectedIndex = -1

                        'Mes Evaluado
                        Dim sdaMesEval As New SqlDataAdapter
                        Dim dsMesEval As New DataSet
                        sdaMesEval.SelectCommand = New SqlCommand("select distinct(format(mes_eval, '00') + '-' + cast(año_eval as varchar(4))) as mes_eval " + _
                                                                  "     , case mes_eval " + _
                                                                  "         when 1 then 'Enero' " + _
                                                                  "         when 2 then 'Febrero' " + _
                                                                  "         when 3 then 'Marzo' " + _
                                                                  "         when 4 then 'Abril' " + _
                                                                  "         when 5 then 'Mayo' " + _
                                                                  "         when 6 then 'Junio' " + _
                                                                  "         when 7 then 'Julio' " + _
                                                                  "         when 8 then 'Agosto' " + _
                                                                  "         when 9 then 'Septiembre' " + _
                                                                  "         when 10 then 'Octubre' " + _
                                                                  "         when 11 then 'Noviembre' " + _
                                                                  "         when 12 then 'Diciembre' " + _
                                                                  "         else '-' " + _
                                                                  "       end  " + _
                                                                  "       + ' ' + cast(año_eval as varchar(4)) as mes_evaluacion " + _
                                                                  "from ms_evaluacion " + _
                                                                  "order by mes_eval ", ConexionBD)
                        .ddlMesEval.DataSource = dsMesEval
                        .ddlMesEval.DataTextField = "mes_evaluacion"
                        .ddlMesEval.DataValueField = "mes_eval"
                        ConexionBD.Open()
                        sdaMesEval.Fill(dsMesEval)
                        .ddlMesEval.DataBind()
                        ConexionBD.Close()
                        sdaMesEval.Dispose()
                        dsMesEval.Dispose()
                        .ddlMesEval.SelectedIndex = -1

                        'Evaluadores
                        Dim sdaEvaluador As New SqlDataAdapter
                        Dim dsEvaluador As New DataSet
                        query = "select distinct(cgUsrEval.id_usuario) " + _
                                "     , cgEmplEval.nombre + ' ' + cgEmplEval.ap_paterno + ' ' + cgEmplEval.ap_materno as Evaluador " + _
                                "from ms_evaluacion " + _
                                "  left join cg_usuario cgUsrEval on ms_evaluacion.id_usr_registro = cgUsrEval.id_usuario " + _
                                "  left join bd_Empleado.dbo.cg_empleado cgEmplEval on cgUsrEval.id_empleado = cgEmplEval.id_empleado " + _
                                "  left join cg_usuario cgUsrVal on ms_evaluacion.id_usr_valida = cgUsrVal.id_usuario " + _
                                "  left join cg_usuario cgUsrDir on ms_evaluacion.id_usr_director = cgUsrDir.id_usuario "
                        If ._txtPerfil.Text <> "DesOrg" And ._txtPerfil.Text <> "JefInfo" Then
                            query = query + "where cgUsrEval.id_usuario = @idUsuario " + _
                                            "   or cgUsrVal.id_usuario = @idUsuario " + _
                                            "   or cgUsrDir.id_usuario = @idUsuario " + _
                                            "   or ms_evaluacion.no_empleado = @noEmpleado "
                        End If
                        query = query + "order by Evaluador "
                        sdaEvaluador.SelectCommand = New SqlCommand(query, ConexionBD)
                        If ._txtPerfil.Text <> "DesOrg" And ._txtPerfil.Text <> "JefInfo" Then
                            sdaEvaluador.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                            sdaEvaluador.SelectCommand.Parameters.AddWithValue("@noEmpleado", Val(._txtNoEmpleado.Text))
                        End If
                        .ddlEvaluador.DataSource = dsEvaluador
                        .ddlEvaluador.DataTextField = "Evaluador"
                        .ddlEvaluador.DataValueField = "id_usuario"
                        ConexionBD.Open()
                        sdaEvaluador.Fill(dsEvaluador)
                        .ddlEvaluador.DataBind()
                        ConexionBD.Close()
                        sdaEvaluador.Dispose()
                        dsEvaluador.Dispose()
                        .ddlEvaluador.SelectedIndex = -1

                        'Estatus
                        Dim sdaEstatus As New SqlDataAdapter
                        Dim dsEstatus As New DataSet
                        sdaEstatus.SelectCommand = New SqlCommand("select distinct(status) as status " + _
                                                                  "     , case status " + _
                                                                  "         when 'P' then 'En Validación' " + _
                                                                  "         when 'PC' then 'Evaluación por Corregir' " + _
                                                                  "         when 'V' then 'Evaluación Validada' " + _
                                                                  "         when 'PA' then 'Pendiente de Autorizar por Director' " + _
                                                                  "         when 'PCA' then 'Evaluaciones de Área por Corregir' " + _
                                                                  "         when 'PVA' then 'Evaluaciones de Área por Validar' " + _
                                                                  "         when 'A' then 'Evaluaciones de Área Autorizadas' " + _
                                                                  "         when 'PCE' then 'Evaluaciones de Área por Concentrar' " + _
                                                                  "         when 'PPE' then 'Evaluaciones de Área por Procesar' " + _
                                                                  "         when 'EP' then 'Evaluación Procesada' " + _
                                                                  "         else '-' " + _
                                                                  "       end as estatus " + _
                                                                  "     , case status " + _
                                                                  "         when 'P' then 0 " + _
                                                                  "         when 'PC' then 1 " + _
                                                                  "         when 'V' then 2 " + _
                                                                  "         when 'PA' then 3 " + _
                                                                  "         when 'PCA' then 4 " + _
                                                                  "         when 'PVA' then 5 " + _
                                                                  "         when 'A' then 6 " + _
                                                                  "         when 'PCE' then 7 " + _
                                                                  "         when 'PPE' then 8 " + _
                                                                  "         when 'EP' then 9 " + _
                                                                  "         else 10 " + _
                                                                  "       end as ind " + _
                                                                  "from ms_evaluacion " + _
                                                                  "order by ind ", ConexionBD)
                        .ddlStatus.DataSource = dsEstatus
                        .ddlStatus.DataTextField = "estatus"
                        .ddlStatus.DataValueField = "status"
                        ConexionBD.Open()
                        sdaEstatus.Fill(dsEstatus)
                        .ddlStatus.DataBind()
                        ConexionBD.Close()
                        sdaEstatus.Dispose()
                        dsEstatus.Dispose()
                        .ddlStatus.SelectedIndex = -1

                        'Limpiar Pantalla
                        limpiar()
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

    Public Sub limpiar()
        With Me
            .pnlFiltros.Visible = True
            'Filtros
            'Fechas
            .cbFechaR.Checked = False
            .pnlFechaR.Visible = False
            .wdpFechaI.Date = Date.Now.ToShortDateString
            .wdpFechaF.Date = Date.Now.ToShortDateString
            .cbEvaluador.Checked = False
            .pnlEvaluador.Visible = False
            .cbMesEval.Checked = False
            .pnlMesEval.Visible = False
            .cbNoEval.Checked = False
            .pnlNoEval.Visible = False
            .cbStatus.Checked = False
            .pnlStatus.Visible = False
            'Ocultar resto de paneles principales
            .pnlRegistros.Visible = False
        End With
    End Sub

    Public Sub vista(ByRef control, ByVal valor)
        With Me
            If valor Then
                control.Visible = True
            Else
                control.Visible = False
            End If
        End With
    End Sub

#End Region

#Region "Filtros"

    Protected Sub cbEmpresa_CheckedChanged(sender As Object, e As EventArgs) Handles cbEmpresa.CheckedChanged
        vista(Me.pnlEmpresa, Me.cbEmpresa.Checked)
    End Sub

    Protected Sub cbDireccion_CheckedChanged(sender As Object, e As EventArgs) Handles cbDireccion.CheckedChanged
        vista(Me.pnlDireccion, Me.cbDireccion.Checked)
    End Sub

    Protected Sub cbEmpleado_CheckedChanged(sender As Object, e As EventArgs) Handles cbEmpleado.CheckedChanged
        vista(Me.pnlEmpleado, Me.cbEmpleado.Checked)
    End Sub

    Protected Sub cbFechaR_CheckedChanged(sender As Object, e As EventArgs) Handles cbFechaR.CheckedChanged
        vista(Me.pnlFechaR, Me.cbFechaR.Checked)
    End Sub

    Protected Sub cbMesEval_CheckedChanged(sender As Object, e As EventArgs) Handles cbMesEval.CheckedChanged
        vista(Me.pnlMesEval, Me.cbMesEval.Checked)
    End Sub

    Protected Sub cbEvaluador_CheckedChanged(sender As Object, e As EventArgs) Handles cbEvaluador.CheckedChanged
        vista(Me.pnlEvaluador, Me.cbEvaluador.Checked)
    End Sub

    Protected Sub cbStatus_CheckedChanged(sender As Object, e As EventArgs) Handles cbStatus.CheckedChanged
        vista(Me.pnlStatus, Me.cbStatus.Checked)
    End Sub

    Protected Sub cbNoEval_CheckedChanged(sender As Object, e As EventArgs) Handles cbNoEval.CheckedChanged
        vista(Me.pnlNoEval, Me.cbNoEval.Checked)
        If Me.cbNoEval.Checked = True Then
            Me.txtNoEval.Text = ""
        End If
    End Sub

#End Region

#Region "Buscar"

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        With Me
            Try
                .litError.Text = ""

                'Llenar el GridView
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaConsulta As New SqlDataAdapter
                Dim dsConsulta As New DataSet
                Dim query As String

                query = "select id_ms_evaluacion " + _
                        "     , ms_evaluacion.no_empleado " + _
                        "     , ms_evaluacion.nombre + ' ' + ms_evaluacion.ap_paterno + ' ' + ms_evaluacion.ap_materno as colaborador " + _
                        "     , ms_evaluacion.lider " + _
                        "     , ms_evaluacion.empresa " + _
                        "     , ms_evaluacion.direccion " + _
                        "     , ms_evaluacion.area " + _
                        "     , ms_evaluacion.unidad_neg " + _
                        "     , ms_evaluacion.puesto " + _
                        "     , ms_evaluacion.puesto_lider " + _
                        "     , case ms_evaluacion.mes_eval " + _
                        "         when 1 then 'Enero' " + _
                        "         when 2 then 'Febrero' " + _
                        "         when 3 then 'Marzo' " + _
                        "         when 4 then 'Abril' " + _
                        "         when 5 then 'Mayo' " + _
                        "         when 6 then 'Junio' " + _
                        "         when 7 then 'Julio' " + _
                        "         when 8 then 'Agosto' " + _
                        "         when 9 then 'Septiembre' " + _
                        "         when 10 then 'Octubre' " + _
                        "         when 11 then 'Noviembre' " + _
                        "         when 12 then 'Diciembre' " + _
                        "         else '-' " + _
                        "       end + ' ' + cast(ms_evaluacion.año_eval as varchar(4)) as mes_eval " + _
                        "     , porcent_cumpl " + _
                        "     , case cobra_bono_asist when 'S' then 'Sí' when 'N' then 'No' else '-' end as cobra_bono_asist " + _
                        "     , case cobra_bono_cumpl_UN when 'S' then 'Sí' when 'N' then 'No' else '-' end as cobra_bono_cumpl_UN " + _
                        "     , porcent_bono_cumpl_UN " + _
                        "     , case ms_evaluacion.status " + _
                        "         when 'P' then 'En Validación' " + _
                        "         when 'PC' then 'Evaluación por Corregir' " + _
                        "         when 'V' then 'Evaluación Validada' " + _
                        "         when 'PA' then 'Pendiente de Autorizar por Director' " + _
                        "         when 'PCA' then 'Evaluaciones de Área por Corregir' " + _
                        "         when 'PVA' then 'Evaluaciones de Área por Validar' " + _
                        "         when 'A' then 'Evaluaciones de Área Autorizadas' " + _
                        "         when 'PCE' then 'Evaluaciones de Área por Concentrar' " + _
                        "         when 'PPE' then 'Evaluaciones de Área por Procesar' " + _
                        "         when 'EP' then 'Evaluación Procesada' " + _
                        "         else '-' " + _
                        "       end as estatus " + _
                        "from ms_evaluacion "

                If ._txtPerfil.Text <> "DesOrg" And ._txtPerfil.Text <> "JefInfo" Then
                    query = query + "  left join cg_usuario cgUsrEval on ms_evaluacion.id_usr_registro = cgUsrEval.id_usuario " + _
                                    "  left join bd_Empleado.dbo.cg_empleado cgEmplEval on cgUsrEval.id_empleado = cgEmplEval.id_empleado " + _
                                    "  left join cg_usuario cgUsrVal on ms_evaluacion.id_usr_valida = cgUsrVal.id_usuario " + _
                                    "  left join cg_usuario cgUsrDir on ms_evaluacion.id_usr_director = cgUsrDir.id_usuario " + _
                                    "where (cgUsrEval.id_usuario = @idUsuario " + _
                                    "    or cgUsrVal.id_usuario = @idUsuario " + _
                                    "    or cgUsrDir.id_usuario = @idUsuario " + _
                                    "    or ms_evaluacion.no_empleado = @noEmpleado) "
                Else
                    query = query + "where 1 = 1 "
                End If

                If .cbEmpresa.Checked = True Then
                    query = query + "  and ms_evaluacion.empresa =  @empresa "
                End If
                If .cbDireccion.Checked = True Then
                    query = query + "  and ms_evaluacion.direccion = @direccion "
                End If
                If .cbEmpleado.Checked = True Then
                    query = query + "  and ms_evaluacion.no_empleado = @noEmpleado "
                End If
                If .cbFechaR.Checked = True Then
                    query = query + "  and ms_evaluacion.fecha_registro between @fechaIni and @fechaFin "
                End If
                If .cbMesEval.Checked = True Then
                    query = query + "  and format(mes_eval, '00') + '-' + cast(año_eval as varchar(4)) = @mesEval "
                End If
                If .cbEvaluador.Checked = True Then
                    query = query + "  and ms_evaluacion.id_usr_registro = @idUsrEvaluador "
                End If
                If .cbStatus.Checked = True Then
                    query = query + "  and ms_evaluacion.status = @estatus "
                End If
                If .cbNoEval.Checked = True Then
                    query = query + "  and ms_evaluacion.id_ms_evaluacion = @idMsEvaluacion "
                End If

                query = query + "order by ms_evaluacion.año_eval, ms_evaluacion.mes_eval, ms_evaluacion.empresa, ms_evaluacion.direccion, ms_evaluacion.centro_costo, colaborador "

                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)

                If ._txtPerfil.Text <> "DesOrg" And ._txtPerfil.Text <> "JefInfo" Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@noEmpleado", Val(._txtNoEmpleado.Text))
                End If

                If .cbEmpresa.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedValue)
                End If
                If .cbDireccion.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@direccion", .ddlDireccion.SelectedValue)
                End If
                If .cbEmpleado.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@noEmpleado", .ddlEmpleado.SelectedValue)
                End If
                If .cbFechaR.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaIni", .wdpFechaI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaFin", .wdpFechaF.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
                End If
                If .cbMesEval.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@mesEval", .ddlMesEval.SelectedValue)
                End If
                If .cbEvaluador.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@idUsrEvaluador", .ddlEvaluador.SelectedValue)
                End If
                If .cbStatus.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@estatus", .ddlStatus.SelectedValue)
                End If
                If .cbNoEval.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@idMsEvaluacion", .txtNoEval.Text.Trim)
                End If

                .gvRegistros.DataSource = dsConsulta
                .gvRegistrosT.DataSource = dsConsulta
                ConexionBD.Open()
                sdaConsulta.Fill(dsConsulta)
                ConexionBD.Close()
                .gvRegistros.DataBind()
                .gvRegistrosT.DataBind()
                sdaConsulta.Dispose()
                dsConsulta.Dispose()
                .gvRegistros.SelectedIndex = -1
                If .gvRegistros.Rows.Count = 0 Then
                    .pnlRegistros.Visible = False
                    .litError.Text = "No existe Registros para esos valores"
                Else
                    .pnlRegistros.Visible = True
                    .pnlImp.Visible = False
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Tabla"

    Protected Sub gvRegistros_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRegistros.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim contDtEval As Integer
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select count(*) " + _
                                         "from dt_evaluacion " + _
                                         "where id_ms_evaluacion = @id_ms_evaluacion "
                SCMValores.Parameters.AddWithValue("@id_ms_evaluacion", Val(.gvRegistros.SelectedRow.Cells(1).Text))
                ConexionBD.Open()
                contDtEval = SCMValores.ExecuteScalar()
                ConexionBD.Close()

                If contDtEval > 0 Then '.gvRegistros.SelectedRow.Cells(11).Text.Trim <> "&nbsp;" Then
                    Session("idMsEval") = .gvRegistros.SelectedRow.Cells(1).Text
                    .pnlImp.Visible = True
                Else
                    .pnlImp.Visible = False
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Exportar"

    Protected Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        Dim tw As New System.IO.StringWriter
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        With Me
            Try
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment;filename=Registros.xls")
                Response.ContentType = "application/vnd.ms-excel"
                .gvRegistrosT.Visible = True
                .gvRegistrosT.RenderControl(hw)
                .gvRegistrosT.Visible = False
                Response.Write(tw.ToString())
                Response.End()
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub

#End Region

End Class