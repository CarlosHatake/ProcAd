Public Class ConsPresupGV
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 5

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        Dim sdaEmpleado As New SqlDataAdapter
                        Dim dsEmpleado As New DataSet
                        sdaEmpleado.SelectCommand = New SqlCommand("select perfil, nombre + ' ' + ap_paterno + ' ' + ap_materno as empleado " +
                                                                   "from cg_usuario " +
                                                                   "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                                   "where id_usuario = @idUsuario ", ConexionBD)
                        sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        sdaEmpleado.Fill(dsEmpleado)
                        ConexionBD.Close()
                        ._txtPerfil.Text = dsEmpleado.Tables(0).Rows(0).Item("perfil").ToString()
                        ._txtEmpleado.Text = dsEmpleado.Tables(0).Rows(0).Item("empleado").ToString()
                        sdaEmpleado.Dispose()
                        dsEmpleado.Dispose()

                        'Llenar listas
                        'Años
                        Dim sdaAño As New SqlDataAdapter
                        Dim dsAño As New DataSet
                        sdaAño.SelectCommand = New SqlCommand("select distinct(año) as año " +
                                                              "from ms_presupuesto " +
                                                              "  left join bd_Empleado.dbo.cg_cc on ms_presupuesto.id_cc = cg_cc.id_cc " +
                                                              "where cg_cc.id_usr_responsable = @idUsuario " +
                                                              "order by año ", ConexionBD)
                        sdaAño.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        .ddlAño.DataSource = dsAño
                        .ddlAño.DataTextField = "año"
                        .ddlAño.DataValueField = "año"
                        ConexionBD.Open()
                        sdaAño.Fill(dsAño)
                        .ddlAño.DataBind()
                        ConexionBD.Close()
                        sdaAño.Dispose()
                        dsAño.Dispose()
                        .ddlAño.SelectedIndex = -1

                        'Empresas
                        Dim sdaEmpresa As New SqlDataAdapter
                        Dim dsEmpresa As New DataSet
                        sdaEmpresa.SelectCommand = New SqlCommand("select distinct(empresa) as empresa ,ms_presupuesto.id_empresa " +
                                                                  "from ms_presupuesto " +
                                                                  "  left join bd_Empleado.dbo.cg_cc  on ms_presupuesto.id_cc = cg_cc.id_cc " +
                                                                  "where cg_cc.id_usr_responsable = @idUsuario " +
                                                                  "order by empresa ", ConexionBD)
                        sdaEmpresa.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        .ddlEmpresa.DataSource = dsEmpresa
                        .ddlEmpresa.DataTextField = "empresa"
                        .ddlEmpresa.DataValueField = "id_empresa"
                        ConexionBD.Open()
                        sdaEmpresa.Fill(dsEmpresa)
                        .ddlEmpresa.DataBind()
                        ConexionBD.Close()
                        sdaEmpresa.Dispose()
                        dsEmpresa.Dispose()
                        .ddlEmpresa.SelectedIndex = -1

                        'Centros de Costo
                        Dim sdaCC As New SqlDataAdapter
                        Dim dsCC As New DataSet
                        sdaCC.SelectCommand = New SqlCommand("select distinct(centro_costo) as centro_costo, ms_presupuesto.id_cc  " +
                                                             "from ms_presupuesto " +
                                                             "  left join bd_Empleado.dbo.cg_cc on ms_presupuesto.id_cc = cg_cc.id_cc " +
                                                             "where cg_cc.id_usr_responsable = @idUsuario " +
                                                             "order by centro_costo ", ConexionBD)
                        sdaCC.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        .ddlCC.DataSource = dsCC
                        .ddlCC.DataTextField = "centro_costo"
                        .ddlCC.DataValueField = "id_cc"
                        ConexionBD.Open()
                        sdaCC.Fill(dsCC)
                        .ddlCC.DataBind()
                        ConexionBD.Close()
                        sdaCC.Dispose()
                        dsCC.Dispose()
                        .ddlCC.SelectedIndex = -1

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
            .cbAño.Checked = True
            .pnlAño.Visible = True
            .cbEmpresa.Checked = False
            .pnlEmpresa.Visible = False
            .cbCC.Checked = False
            .pnlCC.Visible = False
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

    Protected Sub cbAño_CheckedChanged(sender As Object, e As EventArgs) Handles cbAño.CheckedChanged
        vista(Me.pnlAño, Me.cbAño.Checked)
    End Sub

    Protected Sub cbEmpresa_CheckedChanged(sender As Object, e As EventArgs) Handles cbEmpresa.CheckedChanged
        vista(Me.pnlEmpresa, Me.cbEmpresa.Checked)
    End Sub

    Protected Sub cbCC_CheckedChanged(sender As Object, e As EventArgs) Handles cbCC.CheckedChanged
        vista(Me.pnlCC, Me.cbCC.Checked)
    End Sub

#End Region

#Region "Buscar"

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        With Me
            Try
                .litError.Text = ""


                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaConsulta As New SqlDataAdapter
                Dim dsConsulta As New DataSet

                .gvRegistros.DataSource = dsConsulta

                sdaConsulta.SelectCommand = New SqlCommand("SP_C_ms_presupuesto @año, @empresa, @centro_costo", ConexionBD)
                sdaConsulta.SelectCommand.Parameters.AddWithValue("@año", .ddlAño.SelectedValue)

                If .cbEmpresa.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedValue)
                ElseIf cbEmpresa.Checked = False Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedValue)
                End If

                If .cbCC.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@centro_costo", .ddlCC.SelectedValue)
                ElseIf .cbCC.Checked = False Then

                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@centro_costo", .ddlCC.SelectedValue)
                End If

                ConexionBD.Open()
                sdaConsulta.Fill(dsConsulta)
                .gvRegistros.DataBind()
                ConexionBD.Close()
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


            'Llenar el GridView
            'Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            'ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            'Dim sdaConsulta As New SqlDataAdapter
            'Dim dsConsulta As New DataSet
            'Dim query As String

            'query = "select id_ms_presupuesto " + _
            '        "     , ms_presupuesto.empresa " + _
            '        "     , ms_presupuesto.centro_costo " + _
            '        "     , ms_presupuesto.codigo " + _
            '        "     , ms_presupuesto.año " + _
            '        "     , mes_01_p " + _
            '        "     , mes_01_a " + _
            '        "     , mes_01_ep " + _
            '        "     , mes_01_r " + _
            '        "     , mes_01_p + mes_01_a - mes_01_ep - mes_01_r as mes_01_disp " + _
            '        "     , mes_02_p " + _
            '        "     , mes_02_a " + _
            '        "     , mes_02_ep " + _
            '        "     , mes_02_r " + _
            '        "     , mes_02_p + mes_02_a - mes_02_ep - mes_02_r as mes_02_disp " + _
            '        "     , mes_03_p " + _
            '        "     , mes_03_a " + _
            '        "     , mes_03_ep " + _
            '        "     , mes_03_r " + _
            '        "     , mes_03_p + mes_03_a - mes_03_ep - mes_03_r as mes_03_disp " + _
            '        "     , mes_04_p " + _
            '        "     , mes_04_a " + _
            '        "     , mes_04_ep " + _
            '        "     , mes_04_r " + _
            '        "     , mes_04_p + mes_04_a - mes_04_ep - mes_04_r as mes_04_disp " + _
            '        "     , mes_05_p " + _
            '        "     , mes_05_a " + _
            '        "     , mes_05_ep " + _
            '        "     , mes_05_r " + _
            '        "     , mes_05_p + mes_05_a - mes_05_ep - mes_05_r as mes_05_disp " + _
            '        "     , mes_06_p " + _
            '        "     , mes_06_a " + _
            '        "     , mes_06_ep " + _
            '        "     , mes_06_r " + _
            '        "     , mes_06_p + mes_06_a - mes_06_ep - mes_06_r as mes_06_disp " + _
            '        "     , mes_07_p " + _
            '        "     , mes_07_a " + _
            '        "     , mes_07_ep " + _
            '        "     , mes_07_r " + _
            '        "     , mes_07_p + mes_07_a - mes_07_ep - mes_07_r as mes_07_disp " + _
            '        "     , mes_08_p " + _
            '        "     , mes_08_a " + _
            '        "     , mes_08_ep " + _
            '        "     , mes_08_r " + _
            '        "     , mes_08_p + mes_08_a - mes_08_ep - mes_08_r as mes_08_disp " + _
            '        "     , mes_09_p " + _
            '        "     , mes_09_a " + _
            '        "     , mes_09_ep " + _
            '        "     , mes_09_r " + _
            '        "     , mes_09_p + mes_09_a - mes_09_ep - mes_09_r as mes_09_disp " + _
            '        "     , mes_10_p " + _
            '        "     , mes_10_a " + _
            '        "     , mes_10_ep " + _
            '        "     , mes_10_r " + _
            '        "     , mes_10_p + mes_10_a - mes_10_ep - mes_10_r as mes_10_disp " + _
            '        "     , mes_11_p " + _
            '        "     , mes_11_a " + _
            '        "     , mes_11_ep " + _
            '        "     , mes_11_r " + _
            '        "     , mes_11_p + mes_11_a - mes_11_ep - mes_11_r as mes_11_disp " + _
            '        "     , mes_12_p " + _
            '        "     , mes_12_a " + _
            '        "     , mes_12_ep " + _
            '        "     , mes_12_r " + _
            '        "     , mes_12_p + mes_12_a - mes_12_ep - mes_12_r as mes_12_disp " + _
            '        "from ms_presupuesto " + _
            '        "  left join bd_Empleado.dbo.cg_cc on ms_presupuesto.id_cc = cg_cc.id_cc " + _
            '        "where cg_cc.id_usr_responsable = @idUsuario "

            'If .cbAño.Checked = True Then
            '    query = query + "  and año = @año "
            'End If
            'If .cbEmpresa.Checked = True Then
            '    query = query + "  and empresa = @empresa "
            'End If
            'If .cbCC.Checked = True Then
            '    query = query + "  and centro_costo = @centro_costo "
            'End If
            'query = query + "order by id_ms_presupuesto "

            'sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)
            'sdaConsulta.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
            'If .cbAño.Checked = True Then
            '    sdaConsulta.SelectCommand.Parameters.AddWithValue("@año", .ddlAño.SelectedValue)
            'End If
            'If .cbEmpresa.Checked = True Then
            '    sdaConsulta.SelectCommand.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedValue)
            'End If
            'If .cbCC.Checked = True Then
            '    sdaConsulta.SelectCommand.Parameters.AddWithValue("@centro_costo", .ddlCC.SelectedValue)
            'End If

            '.gvRegistros.DataSource = dsConsulta
            'ConexionBD.Open()
            'sdaConsulta.Fill(dsConsulta)
            'ConexionBD.Close()
            '.gvRegistros.DataBind()
            'sdaConsulta.Dispose()
            'dsConsulta.Dispose()
            '.gvRegistros.SelectedIndex = -1
            'If .gvRegistros.Rows.Count = 0 Then
            '    .pnlRegistros.Visible = False
            '    .litError.Text = "No existe Registros para esos valores"
            'Else
            '    .pnlRegistros.Visible = True
            'End If
            'Catch ex As Exception
            '    .litError.Text = ex.ToString
            'End Try
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