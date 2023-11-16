Public Class CatEmplInd
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 1

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")

                        'Eliminar Indicadores previos no almacenados
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "delete from dt_empl_ind " + _
                                                 "where id_dt_empleado = 0 and id_usr_reg = @id_usr_reg "
                        SCMValores.Parameters.AddWithValue("@id_usr_reg", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Catálogo de Empresas
                        Dim sdaEmpresa As New SqlDataAdapter
                        Dim dsEmpresa As New DataSet
                        .ddlEmpresa.DataSource = dsEmpresa
                        sdaEmpresa.SelectCommand = New SqlCommand("select distinct(cg_empresa.id_empresa) " + _
                                                                   "     , nombre as empresa " + _
                                                                   "from bd_Empleado.dbo.cg_empresa " + _
                                                                   "  inner join cg_direccion on cg_empresa.id_empresa = cg_direccion.id_empresa " + _
                                                                   "where cg_empresa.status = 'A' " + _
                                                                   "  and cg_direccion.status = 'A' " + _
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
                        llenarDirecciones()

                        'Catálogo de Unidades de Negocio
                        Dim sdaUnidadNeg As New SqlDataAdapter
                        Dim dsUnidadNeg As New DataSet
                        .ddlUnidadN.DataSource = dsUnidadNeg
                        sdaUnidadNeg.SelectCommand = New SqlCommand("select id_unidad_neg " + _
                                                                    "     , unidad_neg " + _
                                                                    "from cg_unidad_neg " + _
                                                                    "where status = 'A' " + _
                                                                    "order by unidad_neg ", ConexionBD)
                        .ddlUnidadN.DataTextField = "unidad_neg"
                        .ddlUnidadN.DataValueField = "id_unidad_neg"
                        ConexionBD.Open()
                        sdaUnidadNeg.Fill(dsUnidadNeg)
                        .ddlUnidadN.DataBind()
                        ConexionBD.Close()
                        sdaUnidadNeg.Dispose()
                        dsUnidadNeg.Dispose()
                        .ddlUnidadN.SelectedIndex = -1

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

    Public Sub habilitarCampos(ByVal valor As Boolean)
        With Me
            .ddlEmpresa.Enabled = valor
            .ddlUnidadN.Enabled = valor
            .ddlDirecccion.Enabled = valor
            .ddlArea.Enabled = valor
            .txtEmpleado.Enabled = valor
            .ibtnBuscarEmpl.Enabled = valor
            .ddlEmpleado.Enabled = valor
            .ibtnAltaInd.Enabled = valor
            .ibtnBajaInd.Enabled = valor
            .ibtnModifInd.Enabled = valor
            .gvIndicadores.Enabled = valor
        End With
    End Sub

    Public Sub bloqueoPantalla()
        With Me
            .pnlGrid.Visible = False
            .pnlDatos.Visible = True
        End With
    End Sub

    Public Sub llenarDirecciones()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .ddlDirecccion.DataSource = dsCatalogo
                'Catálogo de Autorizadores
                sdaCatalogo.SelectCommand = New SqlCommand("select distinct(cg_direccion.id_direccion) " + _
                                                           "     , cg_direccion.direccion " + _
                                                           "from bd_Empleado.dbo.cg_empresa " + _
                                                           "  inner join cg_direccion on cg_empresa.id_empresa = cg_direccion.id_empresa " + _
                                                           "where cg_empresa.status = 'A' " + _
                                                           "  and cg_direccion.status = 'A' " + _
                                                           "  and cg_direccion.id_empresa = @id_empresa " + _
                                                           "order by direccion ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_empresa", .ddlEmpresa.SelectedValue)
                .ddlDirecccion.DataTextField = "direccion"
                .ddlDirecccion.DataValueField = "id_direccion"
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .ddlDirecccion.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .ddlDirecccion.SelectedIndex = -1

                llenarAreas()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarAreas()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .ddlArea.DataSource = dsCatalogo
                'Catálogo de Autorizadores
                sdaCatalogo.SelectCommand = New SqlCommand("select distinct(dt_area.id_dt_area) " + _
                                                           "     , dt_area.area " + _
                                                           "from cg_direccion " + _
                                                           "  left join dt_area on cg_direccion.id_direccion = dt_area.id_direccion " + _
                                                           "where cg_direccion.status = 'A' " + _
                                                           "  and dt_area.status = 'A' " + _
                                                           "  and cg_direccion.id_direccion = @id_direccion " + _
                                                           "order by area ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_direccion", .ddlDirecccion.SelectedValue)
                .ddlArea.DataTextField = "area"
                .ddlArea.DataValueField = "id_dt_area"
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .ddlArea.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .ddlArea.SelectedIndex = -1

                'empleadoLider()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    'Public Sub empleadoLider()
    '    With Me
    '        Try
    '            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
    '            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
    '            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
    '            SCMValores.Connection = ConexionBD
    '            SCMValores.CommandText = ""
    '            SCMValores.Parameters.Clear()
    '            SCMValores.CommandText = "select case when isnull(nombre_aut, '') = @empleado then isnull(nombre_dir, '') else isnull(nombre_aut, '') end as lider " + _
    '                                     "from dt_area " + _
    '                                     "  left join cg_direccion on dt_area.id_direccion = cg_direccion.id_direccion " + _
    '                                     "where id_dt_area = @id_dt_area "
    '            SCMValores.Parameters.AddWithValue("@empleado", .ddlEmpleado.SelectedItem.Text)
    '            SCMValores.Parameters.AddWithValue("@id_dt_area", .ddlArea.SelectedValue)
    '            ConexionBD.Open()
    '            .lblLider.Text = SCMValores.ExecuteScalar
    '            ConexionBD.Close()
    '        Catch ex As Exception
    '            .litError.Text = ex.ToString
    '        End Try
    '    End With
    'End Sub

    Public Sub llenarEmpleados()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBDNom As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBDNom.ConnectionString = accessDB.conBD("NOM")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .ddlEmpleado.DataSource = dsCatalogo
                'Catálogo de Autorizadores
                sdaCatalogo.SelectCommand = New SqlCommand("select rtrim(ltrim(isnull(nomtrab.cvetra,''))) as no_empleado " + _
                                                           "     , rtrim(ltrim(isnull(nomtrab.nombre,''))) + ' ' + rtrim(ltrim(isnull(nomtrab.apepat,''))) + ' ' + rtrim(ltrim(isnull(nomtrab.apemat,''))) as empleado " + _
                                                           "from nomtrab " + _
                                                           "where nomtrab.status = 'A' " + _
                                                           "  and (nomtrab.cvetra like '%' + @valor + '%' " + _
                                                           "    or rtrim(ltrim(isnull(nomtrab.nombre,''))) + ' ' + rtrim(ltrim(isnull(nomtrab.apepat,''))) + ' ' + rtrim(ltrim(isnull(nomtrab.apemat,''))) like '%' + @valor + '%') " + _
                                                           "order by empleado ", ConexionBDNom)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@valor", .txtEmpleado.Text)
                .ddlEmpleado.DataTextField = "empleado"
                .ddlEmpleado.DataValueField = "no_empleado"
                ConexionBDNom.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .ddlEmpleado.DataBind()
                ConexionBDNom.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .ddlEmpleado.SelectedIndex = -1

                fechaIngreso()
                usuarioProcAd()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarEvaluadores()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .ddlEvalua.DataSource = dsCatalogo
                'Catálogo de Autorizadores
                sdaCatalogo.SelectCommand = New SqlCommand("select id_usuario " + _
                                                           "     , nombre + ' ' + ap_paterno + ' ' + ap_materno as usuario " + _
                                                           "from cg_usuario " + _
                                                           "  left join bd_Empleado.dbo.cg_empleado as cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                           "where cg_usuario.status = 'A' " + _
                                                           "  and nombre + ' ' + ap_paterno + ' ' + ap_materno like '%' + @valor + '%' " + _
                                                           "order by usuario ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@valor", .txtEvalua.Text)
                .ddlEvalua.DataTextField = "usuario"
                .ddlEvalua.DataValueField = "id_usuario"
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .ddlEvalua.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .ddlEvalua.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarValidadores()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .ddlValida.DataSource = dsCatalogo
                'Catálogo de Autorizadores
                sdaCatalogo.SelectCommand = New SqlCommand("select 0 as id_usuario " + _
                                                           "     , ' ' as usuario " + _
                                                           "union " + _
                                                           "select id_usuario " + _
                                                           "     , nombre + ' ' + ap_paterno + ' ' + ap_materno as usuario " + _
                                                           "from cg_usuario " + _
                                                           "  left join bd_Empleado.dbo.cg_empleado as cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                           "where cg_usuario.status = 'A' " + _
                                                           "  and nombre + ' ' + ap_paterno + ' ' + ap_materno like '%' + @valor + '%' " + _
                                                           "order by usuario ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@valor", .txtValida.Text)
                .ddlValida.DataTextField = "usuario"
                .ddlValida.DataValueField = "id_usuario"
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .ddlValida.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .ddlValida.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub
    Public Sub fechaIngreso()
        With Me
            Try
                .litError.Text = ""
                If .ddlEmpleado.Items.Count > 0 Then
                    Dim ConexionBDNom As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBDNom.ConnectionString = accessDB.conBD("NOM")
                    Dim sdaCatalogo As New SqlDataAdapter
                    Dim dsCatalogo As New DataSet
                    sdaCatalogo.SelectCommand = New SqlCommand("select fecini " + _
                                                               "     , convert(varchar, fecini, 103) as fecha_ini " + _
                                                               "from nomtrab " + _
                                                               "where nomtrab.status = 'A' " + _
                                                               "  and nomtrab.cvetra = @no_empleado ", ConexionBDNom)
                    sdaCatalogo.SelectCommand.Parameters.AddWithValue("@no_empleado", .ddlEmpleado.SelectedValue)
                    ConexionBDNom.Open()
                    sdaCatalogo.Fill(dsCatalogo)
                    ConexionBDNom.Close()
                    .lblFechaIng.Text = dsCatalogo.Tables(0).Rows(0).Item("fecha_ini").ToString()
                    .lblFechaIngD.Text = dsCatalogo.Tables(0).Rows(0).Item("fecini").ToString()
                    sdaCatalogo.Dispose()
                    dsCatalogo.Dispose()
                Else
                    .lblFechaIng.Text = ""
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub usuarioProcAd()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim conteo As Integer = 0
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select count(*) " + _
                                         "from cg_usuario " + _
                                         "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado and cg_usuario.status = 'A' and cg_empleado.status = 'A' " + _
                                         "where case len(no_empleado) when 4 then '000' + no_empleado when 5 then '00' + no_empleado else no_empleado end = @no_empleado "
                SCMValores.Parameters.AddWithValue("@no_empleado", .ddlEmpleado.SelectedValue)
                ConexionBD.Open()
                conteo = SCMValores.ExecuteScalar
                ConexionBD.Close()
                If conteo > 0 Then
                    Dim sdaCatalogo As New SqlDataAdapter
                    Dim dsCatalogo As New DataSet
                    sdaCatalogo.SelectCommand = New SqlCommand("select id_usuario, nick " + _
                                                               "from cg_usuario " + _
                                                               "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado and cg_usuario.status = 'A' and cg_empleado.status = 'A' " + _
                                                               "where case len(no_empleado) when 4 then '000' + no_empleado when 5 then '00' + no_empleado else no_empleado end = @no_empleado ", ConexionBD)
                    sdaCatalogo.SelectCommand.Parameters.AddWithValue("@no_empleado", .ddlEmpleado.SelectedValue)
                    ConexionBD.Open()
                    sdaCatalogo.Fill(dsCatalogo)
                    ConexionBD.Close()
                    .lblEmpleadoUsr.Text = dsCatalogo.Tables(0).Rows(0).Item("nick").ToString()
                    .lblEmpleadoID.Text = dsCatalogo.Tables(0).Rows(0).Item("id_usuario").ToString()
                    sdaCatalogo.Dispose()
                    dsCatalogo.Dispose()
                Else
                    .lblEmpleadoUsr.Text = ""
                    .lblEmpleadoID.Text = ""
                End If

                'empleadoLider()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
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
                .gvEmpleado.Columns(0).Visible = True
                .gvEmpleado.DataSource = dsCatalogo
                'Catálogo de Empleados
                Dim query As String = ""
                query = "select id_dt_empleado " + _
                        "     , cg_empresa.nombre as empresa " + _
                        "     , unidad_neg " + _
                        "     , direccion " + _
                        "     , area " + _
                        "     , dt_empleado.no_empleado " + _
                        "     , dt_empleado.nombre as empleado " + _
                        "     , cgEmplEval.nombre + ' ' + cgEmplEval.ap_paterno + ' ' + cgEmplEval.ap_materno as evalua " + _
                        "     , cgEmplVal.nombre + ' ' + cgEmplVal.ap_paterno + ' ' + cgEmplVal.ap_materno as valida " + _
                        "     , (select count(*) from dt_empl_ind where dt_empl_ind.id_dt_empleado = dt_empleado.id_dt_empleado and status = 'A') as no_indicadores " + _
                        "from dt_empleado " + _
                        "  left join dt_area on dt_empleado.id_dt_area = dt_area.id_dt_area " + _
                        "  left join cg_direccion on dt_area.id_direccion = cg_direccion.id_direccion " + _
                        "  left join bd_Empleado.dbo.cg_empresa on cg_direccion.id_empresa = cg_empresa.id_empresa " + _
                        "  left join cg_unidad_neg on dt_empleado.id_unidad_neg = cg_unidad_neg.id_unidad_neg " + _
                        "  left join cg_usuario cgUsrEval on dt_empleado.id_usr_evalua = cgUsrEval.id_usuario " + _
                        "  left join bd_Empleado.dbo.cg_empleado cgEmplEval on cgUsrEval.id_empleado = cgEmplEval.id_empleado " + _
                        "  left join cg_usuario cgUsrVal on dt_empleado.id_usr_valida = cgUsrVal.id_usuario " + _
                        "  left join bd_Empleado.dbo.cg_empleado cgEmplVal on cgUsrVal.id_empleado = cgEmplVal.id_empleado " + _
                        "where dt_empleado.status = 'A' " + _
                        "  and (" + .ddlCampo.SelectedValue.ToString + " like '%' + @valor +'%') " + _
                        "order by empresa, direccion, area, empleado "
                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@valor", .txtValor.Text.Trim)
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvEmpleado.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvEmpleado.Columns(0).Visible = False
                .gvEmpleado.SelectedIndex = -1
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
                sdaCatalogo.SelectCommand = New SqlCommand("select id_empresa " + _
                                                           "     , id_unidad_neg " + _
                                                           "     , cg_direccion.id_direccion " + _
                                                           "     , dt_area.id_dt_area " + _
                                                           "     , isnull(nombre_aut, '') as lider " + _
                                                           "     , isnull(no_empleado, '') as no_empleado " + _
                                                           "     , isnull(id_usr_evalua, 0) as id_usr_evalua " + _
                                                           "     , isnull(id_usr_valida, 0) as id_usr_valida " + _
                                                           "     , director " + _
                                                           "     , primer_nivel_dir " + _
                                                           "from dt_empleado " + _
                                                           "  left join dt_area on dt_empleado.id_dt_area = dt_area.id_dt_area " + _
                                                           "  left join cg_direccion on dt_area.id_direccion = cg_direccion.id_direccion " + _
                                                           "where id_dt_empleado = @id_dt_empleado ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_dt_empleado", idRegistro)
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                ConexionBD.Close()
                .ddlUnidadN.SelectedValue = Val(dsCatalogo.Tables(0).Rows(0).Item("id_unidad_neg").ToString())
                .ddlEmpresa.SelectedValue = Val(dsCatalogo.Tables(0).Rows(0).Item("id_empresa").ToString())
                llenarDirecciones()
                .ddlDirecccion.SelectedValue = Val(dsCatalogo.Tables(0).Rows(0).Item("id_direccion").ToString())
                llenarAreas()
                .ddlArea.SelectedValue = Val(dsCatalogo.Tables(0).Rows(0).Item("id_dt_area").ToString())
                '.lblLider.Text = dsCatalogo.Tables(0).Rows(0).Item("lider").ToString()
                .txtEmpleado.Text = ""
                llenarEmpleados()
                .ddlEmpleado.SelectedValue = dsCatalogo.Tables(0).Rows(0).Item("no_empleado").ToString()
                .txtEvalua.Text = ""
                llenarEvaluadores()
                If Val(dsCatalogo.Tables(0).Rows(0).Item("id_usr_evalua").ToString()) > 0 Then
                    .ddlEvalua.SelectedValue = Val(dsCatalogo.Tables(0).Rows(0).Item("id_usr_evalua").ToString())
                End If
                .txtValida.Text = ""
                llenarValidadores()
                If Val(dsCatalogo.Tables(0).Rows(0).Item("id_usr_valida").ToString()) > 0 Then
                    .ddlEvalua.SelectedValue = Val(dsCatalogo.Tables(0).Rows(0).Item("id_usr_valida").ToString())
                End If
                If dsCatalogo.Tables(0).Rows(0).Item("director").ToString() = "S" Then
                    .cbDirector.Checked = True
                Else
                    .cbDirector.Checked = False
                End If
                If dsCatalogo.Tables(0).Rows(0).Item("primer_nivel_dir").ToString() = "S" Then
                    .cb1erND.Checked = True
                Else
                    .cb1erND.Checked = False
                End If
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                fechaIngreso()
                usuarioProcAd()
                llenarIndicadores(idRegistro)

                bloqueoPantalla()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
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
                SCMTemp.CommandText = ""
                SCMTemp.Parameters.Clear()
                Select Case ._txtTipoMov.Text 'Tipo de Ajuste (alta, baja o modificación)
                    Case "A"
                        SCMTemp.CommandText = "SELECT COUNT(*) FROM dt_empleado WHERE no_empleado = @no_empleado AND status = 'A'"
                    Case Else
                        SCMTemp.CommandText = "SELECT COUNT(*) FROM dt_empleado WHERE no_empleado = @no_empleado AND id_dt_empleado <> @id_dt_empleado AND status = 'A'"
                        SCMTemp.Parameters.AddWithValue("@id_dt_empleado", .gvEmpleado.SelectedRow.Cells(0).Text)
                End Select
                SCMTemp.Parameters.AddWithValue("@no_empleado", .ddlEmpleado.SelectedValue)
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

#Region "Indicadores"

    Public Sub ocultarBotones()
        With Me
            .pnlIndicador.Visible = True
            .btnAceptar.Visible = False
            .btnCancelar.Visible = False
        End With
    End Sub

    Public Sub habilitarCamposInd(ByVal valor)
        With Me
            .txtIndicador.Enabled = valor
            .ddlTipoInd.Enabled = valor
            .wpeMeta.Enabled = valor
            .txtFuente.Enabled = valor
            .txtFormula.Enabled = valor
        End With
    End Sub

    Public Sub llenarIndicadores(ByVal idDtEmpleado)
        With Me
            Try
                'Llenar Grid
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaIndicadores As New SqlDataAdapter
                Dim dsIndicadores As New DataSet
                Dim query As String

                query = "select id_dt_empl_ind " + _
                        "     , indicador " + _
                        "     , tipo_indicador " + _
                        "     , ponderacion " + _
                        "     , meta " + _
                        "     , fuente " + _
                        "     , formula " + _
                        "from dt_empl_ind " + _
                        "where id_dt_empleado = @id_dt_empleado " + _
                        "  and status = 'A' "
                If idDtEmpleado = 0 Then
                    query = query + "  and id_usr_reg = @id_usr_reg"
                End If

                .gvIndicadores.DataSource = dsIndicadores
                .gvIndicadores.Columns(0).Visible = True
                sdaIndicadores.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaIndicadores.SelectCommand.Parameters.AddWithValue("@id_dt_empleado", idDtEmpleado)
                If idDtEmpleado = 0 Then
                    sdaIndicadores.SelectCommand.Parameters.AddWithValue("@id_usr_reg", Val(._txtIdUsuario.Text))
                End If
                ConexionBD.Open()
                sdaIndicadores.Fill(dsIndicadores)
                .gvIndicadores.DataBind()
                ConexionBD.Close()
                sdaIndicadores.Dispose()
                dsIndicadores.Dispose()
                .gvIndicadores.Columns(0).Visible = False
                .gvIndicadores.SelectedIndex = -1

                .lblPondT.Text = sumarPond().ToString + " %"
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub localizarInd(ByVal idDtEmplInd)
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaIndicador As New SqlDataAdapter
                Dim dsIndicador As New DataSet
                sdaIndicador.SelectCommand = New SqlCommand("select indicador " + _
                                                            "     , tipo_indicador " + _
                                                            "     , ponderacion " + _
                                                            "     , meta " + _
                                                            "     , fuente " + _
                                                            "     , isnull(formula, '') as formula " + _
                                                            "from dt_empl_ind " + _
                                                            "where id_dt_empl_ind = @id_dt_empl_ind ", ConexionBD)
                sdaIndicador.SelectCommand.Parameters.AddWithValue("@id_dt_empl_ind", idDtEmplInd)
                ConexionBD.Open()
                sdaIndicador.Fill(dsIndicador)
                ConexionBD.Close()
                .txtIndicador.Text = dsIndicador.Tables(0).Rows(0).Item("indicador").ToString()
                .ddlTipoInd.SelectedValue = dsIndicador.Tables(0).Rows(0).Item("tipo_indicador").ToString()
                .wpePond.Value = Val(dsIndicador.Tables(0).Rows(0).Item("ponderacion").ToString())
                .wpeMeta.Value = Val(dsIndicador.Tables(0).Rows(0).Item("meta").ToString())
                .txtFuente.Text = dsIndicador.Tables(0).Rows(0).Item("fuente").ToString()
                .txtFormula.Text = dsIndicador.Tables(0).Rows(0).Item("formula").ToString()
                sdaIndicador.Dispose()
                dsIndicador.Dispose()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub limpiarPantallaInd()
        With Me
            'Actualizar Lista de Autorizadores
            If ._txtTipoMov.Text = "A" Then
                llenarIndicadores(0)
            Else
                llenarIndicadores(.gvEmpleado.SelectedRow.Cells(0).Text)
            End If
            .pnlDetalle.Enabled = True
            .ibtnAltaInd.Enabled = True
            .ibtnBajaInd.Enabled = False
            .ibtnBajaInd.ImageUrl = "images\Trash_i2.png"
            .ibtnModifInd.Enabled = False
            .ibtnModifInd.ImageUrl = "images\Edit_i2.png"
            .pnlIndicador.Visible = False
            .btnAceptar.Visible = True
            .btnCancelar.Visible = True
        End With
    End Sub

    Public Function validarInd(ByVal idDtEmpleado)
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
                Select Case ._txtTipoMovI.Text 'Tipo de Ajuste (alta, baja o modificación)
                    Case "A"
                        query = "select count(*) " + _
                                "from dt_empl_ind " + _
                                "where id_dt_empleado = @id_dt_empleado " + _
                                "  and indicador = @indicador " + _
                                "  and status = 'A' "
                        SCMTemp.CommandText = query
                    Case Else
                        query = "select count(*) " + _
                                "from dt_empl_ind " + _
                                "where id_dt_empleado = @id_dt_empleado " + _
                                "  and indicador = @indicador " + _
                                "  and id_dt_empl_ind <> @id_dt_empl_ind " + _
                                "  and status = 'A' "
                        SCMTemp.CommandText = query
                        SCMTemp.Parameters.AddWithValue("@id_dt_empl_ind", Val(.gvIndicadores.SelectedRow.Cells(0).Text))
                End Select
                SCMTemp.Parameters.AddWithValue("@id_dt_empleado", idDtEmpleado)
                SCMTemp.Parameters.AddWithValue("@indicador", .txtIndicador.Text.Trim)
                ConexionBD.Open()
                conteo = SCMTemp.ExecuteScalar
                ConexionBD.Close()
                If conteo > 0 Then
                    validarInd = False
                Else
                    validarInd = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
                validarInd = False
            End Try
        End With
    End Function

    Protected Sub ibtnAltaInd_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAltaInd.Click
        With Me
            Try
                ._txtTipoMovI.Text = "A"
                .txtIndicador.Text = ""
                .ddlTipoInd.SelectedIndex = -1
                .wpePond.Text = ""
                .wpeMeta.Text = ""
                .txtFuente.Text = ""
                .txtFormula.Text = ""

                habilitarCamposInd(True)
                .pnlDetalle.Enabled = False
                ocultarBotones()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnBajaInd_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBajaInd.Click
        With Me
            Try
                If .gvIndicadores.SelectedIndex > -1 Then
                    ._txtTipoMovI.Text = "B"
                    localizarInd(.gvIndicadores.SelectedRow.Cells(0).Text)
                    habilitarCamposInd(False)
                    .pnlDetalle.Enabled = False
                    ocultarBotones()
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnModifInd_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnModifInd.Click
        With Me
            Try
                If .gvIndicadores.SelectedIndex > -1 Then
                    ._txtTipoMovI.Text = "M"
                    localizarInd(.gvIndicadores.SelectedRow.Cells(0).Text)
                    habilitarCamposInd(True)
                    .pnlDetalle.Enabled = False
                    ocultarBotones()
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub gvIndicadores_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvIndicadores.SelectedIndexChanged
        With Me
            .litError.Text = ""
            .ibtnBajaInd.Enabled = True
            .ibtnBajaInd.ImageUrl = "images\Trash.png"
            .ibtnModifInd.Enabled = True
            .ibtnModifInd.ImageUrl = "images\Edit.png"
        End With
    End Sub

    Protected Sub btnAceptarInd_Click(sender As Object, e As EventArgs) Handles btnAceptarInd.Click
        With Me
            Try
                .litError.Text = ""
                Dim ban As Integer = 0

                If .txtIndicador.Text = "" Or .wpeMeta.Text = "" Or .txtFuente.Text = "" Then
                    .litError.Text = "Información Insuficiente, favor de validar"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    Select Case ._txtTipoMovI.Text 'Tipo de Ajuste (Agregar, Eliminar o Modificar)
                        Case "A"
                            If ._txtTipoMov.Text = "A" Then
                                If validarInd(0) Then
                                    'Alta de Empleado, no existe id_dt_empleado
                                    SCMValores.CommandText = "insert into dt_empl_ind ( indicador,  tipo_indicador,  ponderacion,  meta,  fuente,  formula,  id_dt_empleado,  id_usr_reg) " + _
                                                             "                 values (@indicador, @tipo_indicador, @ponderacion, @meta, @fuente, @formula, @id_dt_empleado, @id_usr_reg)"
                                    SCMValores.Parameters.AddWithValue("@id_dt_empleado", 0)
                                    SCMValores.Parameters.AddWithValue("@id_usr_reg", Val(._txtIdUsuario.Text))
                                Else
                                    .litError.Text = "Valor Inválido, ya existe ese Indicador"
                                    ban = 1
                                End If
                            Else
                                If validarInd(.gvEmpleado.SelectedRow.Cells(0).Text) Then
                                    'Empleado existente
                                    SCMValores.CommandText = "insert into dt_empl_ind ( indicador,  tipo_indicador,  ponderacion,  meta,  fuente,  formula,  id_dt_empleado,  id_usr_reg) " + _
                                                             "                 values (@indicador, @tipo_indicador, @ponderacion, @meta, @fuente, @formula, @id_dt_empleado, @id_usr_reg)"
                                    SCMValores.Parameters.AddWithValue("@id_dt_empleado", .gvEmpleado.SelectedRow.Cells(0).Text)
                                    SCMValores.Parameters.AddWithValue("@id_usr_reg", Val(._txtIdUsuario.Text))
                                Else
                                    .litError.Text = "Valor Inválido, ya existe ese Indicador"
                                    ban = 1
                                End If
                            End If
                        Case "B"
                            SCMValores.CommandText = "delete from dt_empl_ind where id_dt_empl_ind = @id_dt_empl_ind "
                            SCMValores.Parameters.AddWithValue("@id_dt_empl_ind", .gvIndicadores.SelectedRow.Cells(0).Text)
                        Case Else
                            If ._txtTipoMov.Text = "A" Then
                                'Alta de Empleado, no existe id_dt_empleado
                                If validarInd(0) Then
                                    SCMValores.CommandText = "update dt_empl_ind SET indicador = @indicador, tipo_indicador = @tipo_indicador, ponderacion = @ponderacion, meta = @meta, fuente = @fuente, formula = @formula WHERE id_dt_empl_ind = @id_dt_empl_ind"
                                    SCMValores.Parameters.AddWithValue("@id_dt_empl_ind", .gvIndicadores.SelectedRow.Cells(0).Text)
                                Else
                                    .litError.Text = "Valor Inválido, ya existe ese Indicador "
                                    ban = 1
                                End If
                            Else
                                'Empleado existente
                                If validarInd(.gvEmpleado.SelectedRow.Cells(0).Text) Then
                                    SCMValores.CommandText = "update dt_empl_ind SET indicador = @indicador, tipo_indicador = @tipo_indicador, ponderacion = @ponderacion, meta = @meta, fuente = @fuente, formula = @formula WHERE id_dt_empl_ind = @id_dt_empl_ind"
                                    SCMValores.Parameters.AddWithValue("@id_dt_empl_ind", .gvIndicadores.SelectedRow.Cells(0).Text)
                                Else
                                    .litError.Text = "Valor Inválido, ya existe ese Indicador "
                                    ban = 1
                                End If
                            End If
                    End Select
                    If ban = 0 Then
                        If ._txtTipoMovI.Text <> "B" Then
                            SCMValores.Parameters.AddWithValue("@indicador", .txtIndicador.Text.Trim)
                            SCMValores.Parameters.AddWithValue("@tipo_indicador", .ddlTipoInd.SelectedValue)
                            SCMValores.Parameters.AddWithValue("@ponderacion", .wpePond.Value)
                            SCMValores.Parameters.AddWithValue("@meta", .wpeMeta.Value)
                            SCMValores.Parameters.AddWithValue("@fuente", .txtFuente.Text.Trim)
                            SCMValores.Parameters.AddWithValue("@formula", .txtFormula.Text.Trim)
                        End If
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        limpiarPantallaInd()
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnCancelarInd_Click(sender As Object, e As EventArgs) Handles btnCancelarInd.Click
        limpiarPantallaInd()
    End Sub

#End Region

#End Region

#Region "Selección"

    Protected Sub gvEmpleado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvEmpleado.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                .ibtnBaja.Enabled = True
                .ibtnBaja.ImageUrl = "images\Trash.png"
                .ibtnModif.Enabled = True
                .ibtnModif.ImageUrl = "images\Edit.png"
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Botones Inicio"

    Protected Sub ibtnAlta_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAlta.Click
        With Me
            Try
                ._txtTipoMov.Text = "A"
                .lblTipoMov.Text = "Alta"
                'Datos del Empleado
                bloqueoPantalla()
                .ddlUnidadN.SelectedIndex = -1
                .ddlEmpresa.SelectedValue = 9
                llenarDirecciones()
                .txtEmpleado.Text = ""
                llenarEmpleados()
                .txtEvalua.Text = ""
                llenarEvaluadores()
                .txtValida.Text = ""
                llenarValidadores()
                'Botones para Indicadores
                .ibtnAltaInd.Visible = True
                .ibtnBajaInd.Visible = True
                .ibtnModifInd.Visible = True
                .pnlIndicador.Visible = False
                'Indicadores
                llenarIndicadores(0)

                habilitarCampos(True)
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
                localizar(.gvEmpleado.SelectedRow.Cells(0).Text)
                .ibtnAltaInd.Visible = True
                .ibtnBajaInd.Visible = True
                .ibtnModifInd.Visible = True
                .pnlIndicador.Visible = False
                habilitarCampos(False)
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
                localizar(.gvEmpleado.SelectedRow.Cells(0).Text)
                .ibtnAltaInd.Visible = True
                .ibtnBajaInd.Visible = True
                .ibtnModifInd.Visible = True
                .pnlIndicador.Visible = False
                habilitarCampos(True)
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub


#End Region

#Region "Botones Datos"

    Protected Sub ddlEmpresa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEmpresa.SelectedIndexChanged
        llenarDirecciones()
    End Sub

    Protected Sub ddlDirecccion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDirecccion.SelectedIndexChanged
        llenarAreas()
    End Sub

    'Protected Sub ddlArea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlArea.SelectedIndexChanged
    '    empleadoLider()
    'End Sub

    Protected Sub ibtnBuscarEval_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBuscarEval.Click
        llenarEvaluadores()
    End Sub

    Protected Sub ibtnBuscarVal_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBuscarVal.Click
        llenarValidadores()
    End Sub

    Protected Sub ibtnBuscarEmpl_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBuscarEmpl.Click
        llenarEmpleados()
    End Sub

    Protected Sub ddlEmpleado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEmpleado.SelectedIndexChanged
        fechaIngreso()
        usuarioProcAd()
    End Sub

    Function sumarPond()
        With Me
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
            SCMValores.Connection = ConexionBD
            Dim banInd As Integer = 0
            If ._txtTipoMov.Text <> "B" Then
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                If ._txtTipoMov.Text = "A" Then
                    'Alta
                    SCMValores.CommandText = "select format(isnull(sum(ponderacion), 0) * 100, '##0.##') as pondT " + _
                                             "from dt_empl_ind " + _
                                             "where id_dt_empleado = 0 " + _
                                             "  and id_usr_reg = @id_usr_reg " + _
                                             "  and status = 'A' "
                    SCMValores.Parameters.AddWithValue("@id_usr_reg", Val(._txtIdUsuario.Text))
                Else
                    'Modificación
                    SCMValores.CommandText = "select format(isnull(sum(ponderacion), 0) * 100, '##0.##') as pondT " + _
                                             "from dt_empl_ind " + _
                                             "where id_dt_empleado = @id_dt_empleado " + _
                                             "  and status = 'A' "
                    SCMValores.Parameters.AddWithValue("@id_dt_empleado", .gvEmpleado.SelectedRow.Cells(0).Text)
                End If
                ConexionBD.Open()
                sumarPond = SCMValores.ExecuteScalar()
                ConexionBD.Close()
            Else
                sumarPond = 0
            End If
        End With
    End Function

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""
                If .cb1erND.Checked = True And .cbDirector.Checked = True Then
                    .litError.Text = "Un Empleado no puede ser Director y 1er Nivel de Dirección"
                Else
                    If .ddlUnidadN.Items.Count = 0 Or .ddlArea.Items.Count = 0 Or .ddlEmpleado.Items.Count = 0 Then
                        .litError.Text = "Información Insuficiente, favor de verificar"
                    Else
                        If .gvIndicadores.Rows.Count > 0 And .cbDirector.Checked = True Then
                            .litError.Text = "Los Directores no pueden tener indicadores, favor de eliminarlos"
                        Else
                            'Validar la suma de ponderaciones
                            Dim banInd As Integer = 0
                            If ._txtTipoMov.Text <> "B" And .cbDirector.Checked = False Then
                                Dim pondeT As Decimal = sumarPond()
                                If pondeT < 99.99 Or pondeT > 100 Then
                                    banInd = 1
                                End If
                            End If

                            If banInd = 0 Then
                                Dim ban As Integer = 0
                                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                                SCMValores.Connection = ConexionBD
                                SCMValores.CommandText = ""
                                SCMValores.Parameters.Clear()
                                Select Case ._txtTipoMov.Text 'Tipo de Ajuste (alta, baja o modificación)
                                    Case "A"
                                        If validar() Then
                                            SCMValores.CommandText = "INSERT INTO dt_empleado (id_unidad_neg, id_dt_area, no_empleado, fecha_ingreso, nombre, id_usr_empl, id_usr_evalua, id_usr_valida, director, primer_nivel_dir) values(@id_unidad_neg, @id_dt_area, @no_empleado, @fecha_ingreso, @nombre, @id_usr_empl, @id_usr_evalua, @id_usr_valida, @director, @primer_nivel_dir)"
                                        Else
                                            .litError.Text = "Valor Inválido, ya existe ese Empleado"
                                            ban = 1
                                        End If
                                    Case "B"
                                        SCMValores.CommandText = "UPDATE dt_empleado SET status = 'B' WHERE id_dt_empleado = @id_dt_empleado"
                                        SCMValores.Parameters.AddWithValue("@id_dt_empleado", .gvEmpleado.SelectedRow.Cells(0).Text)
                                    Case Else
                                        If validar() Then
                                            SCMValores.CommandText = "UPDATE dt_empleado SET id_unidad_neg = @id_unidad_neg, id_dt_area = @id_dt_area, no_empleado = @no_empleado, fecha_ingreso = @fecha_ingreso, nombre = @nombre, id_usr_empl = @id_usr_empl, id_usr_evalua = @id_usr_evalua, id_usr_valida = @id_usr_valida, director = @director, primer_nivel_dir = @primer_nivel_dir WHERE id_dt_empleado = @id_dt_empleado"
                                            SCMValores.Parameters.AddWithValue("@id_dt_empleado", .gvEmpleado.SelectedRow.Cells(0).Text)
                                        Else
                                            .litError.Text = "Valor Inválido, ya existe ese Empleado"
                                            ban = 1
                                        End If
                                End Select
                                If ban = 0 Then
                                    SCMValores.Parameters.AddWithValue("@id_unidad_neg", .ddlUnidadN.SelectedValue)
                                    SCMValores.Parameters.AddWithValue("@id_dt_area", .ddlArea.SelectedValue)
                                    SCMValores.Parameters.AddWithValue("@no_empleado", .ddlEmpleado.SelectedValue)
                                    SCMValores.Parameters.AddWithValue("@fecha_ingreso", CDate(.lblFechaIng.Text))
                                    SCMValores.Parameters.AddWithValue("@nombre", .ddlEmpleado.SelectedItem.Text)
                                    If .lblEmpleadoUsr.Text = "" Then
                                        SCMValores.Parameters.AddWithValue("@id_usr_empl", DBNull.Value)
                                    Else
                                        SCMValores.Parameters.AddWithValue("@id_usr_empl", Val(.lblEmpleadoID.Text))
                                    End If
                                    SCMValores.Parameters.AddWithValue("@id_usr_evalua", .ddlEvalua.SelectedValue)
                                    If .ddlValida.SelectedValue = 0 Then
                                        SCMValores.Parameters.AddWithValue("@id_usr_valida", DBNull.Value)
                                    Else
                                        SCMValores.Parameters.AddWithValue("@id_usr_valida", .ddlValida.SelectedValue)
                                    End If
                                    If .cbDirector.Checked = True Then
                                        SCMValores.Parameters.AddWithValue("@director", "S")
                                    Else
                                        SCMValores.Parameters.AddWithValue("@director", "N")
                                    End If
                                    If .cb1erND.Checked = True Then
                                        SCMValores.Parameters.AddWithValue("@primer_nivel_dir", "S")
                                    Else
                                        SCMValores.Parameters.AddWithValue("@primer_nivel_dir", "N")
                                    End If
                                    ConexionBD.Open()
                                    SCMValores.ExecuteNonQuery()
                                    ConexionBD.Close()

                                    'Obtener el id_dt_empleado
                                    Dim idDtEmpleado As Integer
                                    SCMValores.CommandText = ""
                                    SCMValores.Parameters.Clear()
                                    SCMValores.CommandText = "select isnull(max(id_dt_empleado), 0) from dt_empleado WHERE no_empleado = @no_empleado and status = 'A' "
                                    SCMValores.Parameters.AddWithValue("@no_empleado", .ddlEmpleado.SelectedValue)
                                    ConexionBD.Open()
                                    idDtEmpleado = SCMValores.ExecuteScalar()
                                    ConexionBD.Close()

                                    'Actualizar Indicadores
                                    SCMValores.CommandText = ""
                                    SCMValores.Parameters.Clear()
                                    SCMValores.CommandText = "update dt_empl_ind set id_dt_empleado = @id_dt_empleado where id_usr_reg = @id_usr_reg and id_dt_empleado = 0 "
                                    SCMValores.Parameters.AddWithValue("@id_dt_empleado", idDtEmpleado)
                                    SCMValores.Parameters.AddWithValue("@id_usr_reg", Val(._txtIdUsuario.Text))
                                    ConexionBD.Open()
                                    SCMValores.ExecuteNonQuery()
                                    ConexionBD.Close()

                                    limpiarPantalla()
                                End If
                            Else
                                .litError.Text = "Favor de validar las ponderaciones"
                            End If
                        End If
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

End Class