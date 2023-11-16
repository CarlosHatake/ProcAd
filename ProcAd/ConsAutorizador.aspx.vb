Public Class ConsAutorizador
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

                        'Llenar listas
                        'Empresas
                        Dim sdaEmpresa As New SqlDataAdapter
                        Dim dsEmpresa As New DataSet
                        sdaEmpresa.SelectCommand = New SqlCommand("select distinct(cgEmplEmpr.nombre) as Empresa " + _
                                                                  "from cg_usuario cgEmplUsr " + _
                                                                  "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cgEmplUsr.id_empleado = cgEmpl.id_empleado " + _
                                                                  "  left join bd_Empleado.dbo.cg_cc cgEmplCC on cgEmpl.id_cc = cgEmplCC.id_cc " + _
                                                                  "  left join bd_Empleado.dbo.cg_empresa cgEmplEmpr on cgEmplCC.id_empresa = cgEmplEmpr.id_empresa " + _
                                                                  "where cgEmplUsr.status = 'A' " + _
                                                                  "order by Empresa ", ConexionBD)
                        .ddlEmpresa.DataSource = dsEmpresa
                        .ddlEmpresa.DataTextField = "Empresa"
                        .ddlEmpresa.DataValueField = "Empresa"
                        ConexionBD.Open()
                        sdaEmpresa.Fill(dsEmpresa)
                        .ddlEmpresa.DataBind()
                        ConexionBD.Close()
                        sdaEmpresa.Dispose()
                        dsEmpresa.Dispose()
                        .ddlEmpresa.SelectedIndex = -1
                        'Centro de Costo
                        Dim sdaCC As New SqlDataAdapter
                        Dim dsCC As New DataSet
                        sdaCC.SelectCommand = New SqlCommand("select distinct(cgEmplCC.nombre) as [Centro de Costo] " + _
                                                             "from cg_usuario cgEmplUsr " + _
                                                             "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cgEmplUsr.id_empleado = cgEmpl.id_empleado " + _
                                                             "  left join bd_Empleado.dbo.cg_cc cgEmplCC on cgEmpl.id_cc = cgEmplCC.id_cc " + _
                                                             "where cgEmplUsr.status = 'A' " + _
                                                             "order by [Centro de Costo] ", ConexionBD)
                        .ddlCC.DataSource = dsCC
                        .ddlCC.DataTextField = "Centro de Costo"
                        .ddlCC.DataValueField = "Centro de Costo"
                        ConexionBD.Open()
                        sdaCC.Fill(dsCC)
                        .ddlCC.DataBind()
                        ConexionBD.Close()
                        sdaCC.Dispose()
                        dsCC.Dispose()
                        .ddlCC.SelectedIndex = -1
                        'Empleado
                        Dim sdaEmpleado As New SqlDataAdapter
                        Dim dsEmpleado As New DataSet
                        sdaEmpleado.SelectCommand = New SqlCommand("select distinct(cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno) as Empleado " + _
                                                                   "from cg_usuario cgEmplUsr " + _
                                                                   "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cgEmplUsr.id_empleado = cgEmpl.id_empleado " + _
                                                                   "where cgEmplUsr.status = 'A' " + _
                                                                   "order by Empleado ", ConexionBD)
                        .ddlEmpleado.DataSource = dsEmpleado
                        .ddlEmpleado.DataTextField = "Empleado"
                        .ddlEmpleado.DataValueField = "Empleado"
                        ConexionBD.Open()
                        sdaEmpleado.Fill(dsEmpleado)
                        .ddlEmpleado.DataBind()
                        ConexionBD.Close()
                        sdaEmpleado.Dispose()
                        dsEmpleado.Dispose()
                        .ddlEmpleado.SelectedIndex = -1
                        'Autorizadores
                        Dim sdaAutorizador As New SqlDataAdapter
                        Dim dsAutorizador As New DataSet
                        sdaAutorizador.SelectCommand = New SqlCommand("select distinct(cgAutEmpl.nombre + ' ' + cgAutEmpl.ap_paterno + ' ' + cgAutEmpl.ap_materno) as Autorizador " + _
                                                                      "from cg_usuario cgEmplUsr " + _
                                                                      "  left join dt_autorizador dtAut on cgEmplUsr.id_usuario = dtAut.id_usuario " + _
                                                                      "  left join cg_usuario cgAutUsr on dtAut.id_autorizador = cgAutUsr.id_usuario " + _
                                                                      "  left join bd_Empleado.dbo.cg_empleado cgAutEmpl on cgAutUsr.id_empleado = cgAutEmpl.id_empleado " + _
                                                                      "where cgEmplUsr.status = 'A' " + _
                                                                      "  and cgAutUsr.status = 'A' " + _
                                                                      "  and cgAutEmpl.nombre + ' ' + cgAutEmpl.ap_paterno + ' ' + cgAutEmpl.ap_materno is not null " + _
                                                                      "order by Autorizador ", ConexionBD)
                        .ddlAutorizador.DataSource = dsAutorizador
                        .ddlAutorizador.DataTextField = "Autorizador"
                        .ddlAutorizador.DataValueField = "Autorizador"
                        ConexionBD.Open()
                        sdaAutorizador.Fill(dsAutorizador)
                        .ddlAutorizador.DataBind()
                        ConexionBD.Close()
                        sdaAutorizador.Dispose()
                        dsAutorizador.Dispose()
                        .ddlAutorizador.SelectedIndex = -1

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
            .cbEmpresa.Checked = False
            .pnlEmpresa.Visible = False
            .cbCC.Checked = False
            .pnlCC.Visible = False
            .cbEmpleado.Checked = False
            .pnlEmpleado.Visible = False
            .cbAutorizador.Checked = False
            .pnlAutorizador.Visible = False
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

    Protected Sub cbCC_CheckedChanged(sender As Object, e As EventArgs) Handles cbCC.CheckedChanged
        vista(Me.pnlCC, Me.cbCC.Checked)
    End Sub

    Protected Sub cbEmpleado_CheckedChanged(sender As Object, e As EventArgs) Handles cbEmpleado.CheckedChanged
        vista(Me.pnlEmpleado, Me.cbEmpleado.Checked)
    End Sub

    Protected Sub cbAutorizador_CheckedChanged(sender As Object, e As EventArgs) Handles cbAutorizador.CheckedChanged
        vista(Me.pnlAutorizador, Me.cbAutorizador.Checked)
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

                query = "select cgEmplUsr.id_usuario " + _
                        "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as Empleado " + _
                        "     , cgEmplUsr.nick as Usuario " + _
                        "     , case cgEmplUsr.perfil " + _
                        "         when 'Adm' then 'Administrador' " + _
                        "         when 'AltUsr' then 'Admin. Usuarios' " + _
                        "         when 'Usr' then 'Usuario' " + _
                        "         when 'Aut' then 'Autorizador' " + _
                        "         when 'DirAdFi' then 'Director de Administración y Finanzas' " + _
                        "         when 'GerConta' then 'Gerente de Contabilidad' " + _
                        "         when 'AdmCat' then 'Admin. Catálogos' " + _
                        "         when 'AdmCatEst' then 'Admin. Catálogos Estadía' " + _
                        "         when 'Vig' then 'Vigilante' " + _
                        "         when 'CoPame' then 'Comprobaciones PAME' " + _
                        "         when 'CoDCM' then 'Comprobaciones DICOMEX' " + _
                        "         when 'Comp' then 'Comprobaciones' " + _
                        "         when 'Aud' then 'Auditor' " + _
                        "         when 'Caja' then 'Caja' " + _
                        "         when 'CxP' then 'Cuentas por Pagar' " + _
                        "         when 'Conta' then 'Contador' " + _
                        "         when 'ContaF' then 'Contador Funcionarios' " + _
                        "         when 'GerSopTec' then 'Gerente de Soporte Técnico' " + _
                        "         when 'SopTec' then 'Soporte Técnico' " + _
                        "         when 'GerTesor' then 'Gerente de Tesorería' " + _
                        "         when 'ValPresup' then 'Validador de Presupuesto' " + _
                        "         when 'AdmonDCM' then 'Administración Dicomex' " + _
                        "         when 'Compras' then 'Compras' " + _
                        "         when 'JefCompras' then 'Jefe de Compras' " + _
                        "         when 'AdmViajes' then 'Administrador de Viajes' " + _
                        "         when 'SegViajes' then 'Seguimiento a Viajes' " + _
                        "         when 'DesOrg' then 'Desarrollo Organizacional' " + _
                        "         when 'Liq' then 'Liquidador' " + _
                        "         when 'JefInfo' then 'Jefe de Información' " + _
                        "         else '-' " + _
                        "       end as Perfil " + _
                        "     , cgEmplEmpr.nombre as Empresa " + _
                        "     , cgEmplCC.nombre as [Centro de Costo] " + _
                        "     , cgEmplPue.nombre as Puesto " + _
                        "     , cgAutEmpl.nombre + ' ' + cgAutEmpl.ap_paterno + ' ' + cgAutEmpl.ap_materno as Autorizador " + _
                        "     , cgAutUsr.nick as [Usuario Autorizador] " + _
                        "     , case when dtAut.aut_dir is null then null else case dtAut.aut_dir when 'S' then 'Sí' when 'N' then 'No' else '-' end end as Director " + _
                        "     , case when cgAutUsr.perfil is null then null else case cgAutUsr.perfil " + _
                        "         when 'Adm' then 'Administrador' " + _
                        "         when 'AltUsr' then 'Admin. Usuarios' " + _
                        "         when 'Usr' then 'Usuario' " + _
                        "         when 'Aut' then 'Autorizador' " + _
                        "         when 'DirAdFi' then 'Director de Administración y Finanzas' " + _
                        "         when 'GerConta' then 'Gerente de Contabilidad' " + _
                        "         when 'AdmCat' then 'Admin. Catálogos' " + _
                        "         when 'AdmCatEst' then 'Admin. Catálogos Estadía' " + _
                        "         when 'Vig' then 'Vigilante' " + _
                        "         when 'CoPame' then 'Comprobaciones PAME' " + _
                        "         when 'CoDCM' then 'Comprobaciones DICOMEX' " + _
                        "         when 'Comp' then 'Comprobaciones' " + _
                        "         when 'Aud' then 'Auditor' " + _
                        "         when 'Caja' then 'Caja' " + _
                        "         when 'CxP' then 'Cuentas por Pagar' " + _
                        "         when 'Conta' then 'Contador' " + _
                        "         when 'ContaF' then 'Contador Funcionarios' " + _
                        "         when 'GerSopTec' then 'Gerente de Soporte Técnico' " + _
                        "         when 'SopTec' then 'Soporte Técnico' " + _
                        "         when 'GerTesor' then 'Gerente de Tesorería' " + _
                        "         when 'ValPresup' then 'Validador de Presupuesto' " + _
                        "         when 'AdmonDCM' then 'Administración Dicomex' " + _
                        "         when 'Compras' then 'Compras' " + _
                        "         when 'JefCompras' then 'Jefe de Compras' " + _
                        "         when 'AdmViajes' then 'Administrador de Viajes' " + _
                        "         when 'SegViajes' then 'Seguimiento a Viajes' " + _
                        "         when 'DesOrg' then 'Desarrollo Organizacional' " + _
                        "         when 'Liq' then 'Liquidador' " + _
                        "         when 'JefInfo' then 'Jefe de Información' " + _
                        "         else '-' " + _
                        "       end end as [Perfil Autorizador] " + _
                        "     , cgAutEmpr.nombre as [Empresa Autorizador] " + _
                        "     , cgAutPue.nombre as [Puesto Autorizador] " + _
                        "from cg_usuario cgEmplUsr " + _
                        "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cgEmplUsr.id_empleado = cgEmpl.id_empleado " + _
                        "  left join bd_Empleado.dbo.cg_puesto cgEmplPue on cgEmpl.id_puesto = cgEmplPue.id_puesto " + _
                        "  left join bd_Empleado.dbo.cg_cc cgEmplCC on cgEmpl.id_cc = cgEmplCC.id_cc " + _
                        "  left join bd_Empleado.dbo.cg_empresa cgEmplEmpr on cgEmplCC.id_empresa = cgEmplEmpr.id_empresa " + _
                        "  left join dt_autorizador dtAut on cgEmplUsr.id_usuario = dtAut.id_usuario " + _
                        "  left join cg_usuario cgAutUsr on dtAut.id_autorizador = cgAutUsr.id_usuario " + _
                        "  left join bd_Empleado.dbo.cg_empleado cgAutEmpl on cgAutUsr.id_empleado = cgAutEmpl.id_empleado " + _
                        "  left join bd_Empleado.dbo.cg_puesto cgAutPue on cgAutEmpl.id_puesto = cgAutPue.id_puesto " + _
                        "  left join bd_Empleado.dbo.cg_cc cgAutCC on cgAutEmpl.id_cc = cgAutCC.id_cc " + _
                        "  left join bd_Empleado.dbo.cg_empresa cgAutEmpr on cgAutCC.id_empresa = cgAutEmpr.id_empresa " + _
                        "where cgEmplUsr.status = 'A' " + _
                        "  and cgAutUsr.status = 'A' "

                If .cbEmpresa.Checked = True Then
                    query = query + "  and cgEmplEmpr.nombre = @empresa "
                End If
                If .cbCC.Checked = True Then
                    query = query + "  and cgEmplCC.nombre = @centroCosto "
                End If
                If .cbEmpleado.Checked = True Then
                    query = query + "  and cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno = @empleado "
                End If
                If .cbAutorizador.Checked = True Then
                    query = query + "  and cgAutEmpl.nombre + ' ' + cgAutEmpl.ap_paterno + ' ' + cgAutEmpl.ap_materno = @autorizador "
                End If

                sdaConsulta.SelectCommand = New SqlCommand(query + _
                                                           "order by Empleado, Director ", ConexionBD)

                If .cbEmpresa.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedValue)
                End If
                If .cbCC.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@centroCosto", .ddlCC.SelectedValue)
                End If
                If .cbEmpleado.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@empleado", .ddlEmpleado.SelectedValue)
                End If
                If .cbAutorizador.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@autorizador", .ddlAutorizador.SelectedValue)
                End If

                .gvRegistros.DataSource = dsConsulta
                ConexionBD.Open()
                sdaConsulta.Fill(dsConsulta)
                ConexionBD.Close()
                .gvRegistros.DataBind()
                sdaConsulta.Dispose()
                dsConsulta.Dispose()
                .gvRegistros.SelectedIndex = -1
                If .gvRegistros.Rows.Count = 0 Then
                    .pnlRegistros.Visible = False
                    .litError.Text = "No existe Registros para esos valores"
                Else
                    .pnlRegistros.Visible = True
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
                .gvRegistros.Visible = True
                .gvRegistros.RenderControl(hw)
                .gvRegistros.Visible = False
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