Public Class ConsAmpl
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

                        'Autorizador
                        SCMValores.CommandText = "select count(*) " +
                                                 "from ms_ampliacion_p " +
                                                 "where id_usr_autoriza = @idUsuario "
                        SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        ._txtUsrAut.Text = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        If Val(._txtUsrAut.Text) > 1 Or ._txtPerfil.Text = "ValPresup" Or ._txtPerfil.Text = "admin" Then
                            ._txtPnlAut.Text = "S"
                        Else
                            ._txtPnlAut.Text = "N"
                        End If

                        'Llenar listas
                        Dim query As String
                        'Solicitantes
                        Dim sdaSolicitante As New SqlDataAdapter
                        Dim dsSolicitante As New DataSet
                        query = "select distinct(id_usr_solicita) as id_usr_solicita, solicita " +
                                "from ms_ampliacion_p "
                        If Val(._txtUsrAut.Text) > 1 Then
                            query = query + "where solicita = @autorizador or autorizador = @autorizador "
                        End If
                        query = query + "order by solicita "
                        sdaSolicitante.SelectCommand = New SqlCommand(query, ConexionBD)
                        If Val(._txtUsrAut.Text) > 1 Then
                            sdaSolicitante.SelectCommand.Parameters.AddWithValue("@autorizador", ._txtEmpleado.Text)
                        End If
                        .ddlSolicitante.DataSource = dsSolicitante
                        .ddlSolicitante.DataTextField = "solicita"
                        .ddlSolicitante.DataValueField = "id_usr_solicita"
                        ConexionBD.Open()
                        sdaSolicitante.Fill(dsSolicitante)
                        .ddlSolicitante.DataBind()
                        ConexionBD.Close()
                        sdaSolicitante.Dispose()
                        dsSolicitante.Dispose()
                        .ddlSolicitante.SelectedIndex = -1
                        'Empresas
                        Dim sdaEmpresa As New SqlDataAdapter
                        Dim dsEmpresa As New DataSet
                        query = "select distinct(empresa) as empresa " +
                                "from ms_ampliacion_p "
                        If Val(._txtUsrAut.Text) > 1 Then
                            query = query + "where solicita = @autorizador or autorizador = @autorizador "
                        End If
                        query = query + "order by empresa "
                        sdaEmpresa.SelectCommand = New SqlCommand(query, ConexionBD)
                        If Val(._txtUsrAut.Text) > 1 Then
                            sdaEmpresa.SelectCommand.Parameters.AddWithValue("@autorizador", ._txtEmpleado.Text)
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
                        'Autorizadores
                        Dim sdaAutorizador As New SqlDataAdapter
                        Dim dsAutorizador As New DataSet
                        query = "select distinct(autorizador) as autorizador " +
                                "from ms_ampliacion_p "
                        If ._txtPnlAut.Text = "N" Then
                            query = query + "where solicita = @empleado "
                        End If
                        If Val(._txtUsrAut.Text) > 1 Then
                            query = query + "where solicita = @autorizador or autorizador = @autorizador "
                        End If
                        query = query + "order by autorizador "
                        sdaAutorizador.SelectCommand = New SqlCommand(query, ConexionBD)
                        If ._txtPnlAut.Text = "N" Then
                            sdaAutorizador.SelectCommand.Parameters.AddWithValue("@empleado", ._txtEmpleado.Text)
                        End If
                        If Val(._txtUsrAut.Text) > 1 Then
                            sdaAutorizador.SelectCommand.Parameters.AddWithValue("@autorizador", ._txtEmpleado.Text)
                        End If
                        .ddlAutorizador.DataSource = dsAutorizador
                        .ddlAutorizador.DataTextField = "autorizador"
                        .ddlAutorizador.DataValueField = "autorizador"
                        ConexionBD.Open()
                        sdaAutorizador.Fill(dsAutorizador)
                        .ddlAutorizador.DataBind()
                        ConexionBD.Close()
                        sdaAutorizador.Dispose()
                        dsAutorizador.Dispose()
                        .ddlAutorizador.SelectedIndex = -1

                        If ._txtPnlAut.Text = "N" Then
                            .pnlSolicitó.Visible = False
                        Else
                            .pnlSolicitó.Visible = True
                            .cbSolicitante.Checked = False
                            .pnlSolicitante.Visible = False
                            .cbEmpresa.Checked = False
                            .pnlEmpresa.Visible = False
                            If ._txtPerfil.Text = "CoPame" Then
                                .cbEmpresa.Checked = True
                                .cbEmpresa.Enabled = False
                                .pnlEmpresa.Visible = True
                                .ddlEmpresa.SelectedValue = "PAME"
                                .pnlEmpresa.Enabled = False
                            End If
                            If ._txtPerfil.Text = "CoDCM" Then
                                .cbEmpresa.Checked = True
                                .cbEmpresa.Enabled = False
                                .pnlEmpresa.Visible = True
                                .ddlEmpresa.SelectedValue = "DICOMEX"
                                .pnlEmpresa.Enabled = False
                            End If
                        End If

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
            .cbFechaC.Checked = False
            .pnlFechaC.Visible = False
            .wdpFechaI.Date = Date.Now.ToShortDateString
            .wdpFechaF.Date = Date.Now.ToShortDateString
            .cbAutorizador.Checked = False
            .pnlAutorizador.Visible = False
            .cbStatus.Checked = False
            .pnlStatus.Visible = False
            .cbFolio.Checked = False
            .pnlFolio.Visible = False
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

    Protected Sub cbSolicitante_CheckedChanged(sender As Object, e As EventArgs) Handles cbSolicitante.CheckedChanged
        vista(Me.pnlSolicitante, Me.cbSolicitante.Checked)
    End Sub

    Protected Sub cbFechaC_CheckedChanged(sender As Object, e As EventArgs) Handles cbFechaC.CheckedChanged
        vista(Me.pnlFechaC, Me.cbFechaC.Checked)
    End Sub

    Protected Sub cbAutorizador_CheckedChanged(sender As Object, e As EventArgs) Handles cbAutorizador.CheckedChanged
        vista(Me.pnlAutorizador, Me.cbAutorizador.Checked)
    End Sub

    Protected Sub cbStatus_CheckedChanged(sender As Object, e As EventArgs) Handles cbStatus.CheckedChanged
        vista(Me.pnlStatus, Me.cbStatus.Checked)
    End Sub

    Protected Sub cbFolio_CheckedChanged(sender As Object, e As EventArgs) Handles cbFolio.CheckedChanged
        vista(Me.pnlFolio, Me.cbFolio.Checked)
        If Me.cbFolio.Checked = True Then
            Me.txtFolio.Text = ""
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

                'query = "select ms_ampliacion_p.id_ms_ampliacion_p " +
                '        "     , solicita " +
                '        "     , autorizador " +
                '        "     , empresa " +
                '        "     , centro_costo " +
                '        "     , año " +
                '        "     , mes " +
                '        "     , monto_actual " +
                '        "     , monto_solicita " +
                '        "     , monto_nuevo " +
                '        "     , case when impacto_pres_porcent is null then 'Sin monto previo para comparar' else format(impacto_pres_porcent, 'p', 'es-MX') end as impacto_pres_porcent " +
                '        "     , fecha_solicita " +
                '        "     , fecha_autoriza , " +
                '        "       case  " +
                '        "       when status = 'P' and inst.id_actividad = 116 then 'Pendiente de validación del presupuesto' " +
                '        "       when status = 'P' and inst.id_actividad = 106 then 'Pendiente de Autorización por el director' " +
                '        "       when status = 'A' and inst.id_actividad = 107 then 'Autorizado' " +
                '        "       when status = 'Z' and inst.id_actividad = 117 then 'Rechazado por validador de presupuesto' " +
                '        "       when status = 'Z' and inst.id_actividad = 108 then 'Rechazado por el director' " +
                '        "       when status = 'P' then 'Pendiente de Autorización' " +
                '        "       when status = 'A' then 'Autorizado' " +
                '        "       when status = 'Z' then 'Rechazado' " +
                '        "       else '-' " +
                '        "       end as estado " +
                '        "from ms_ampliacion_p " +
                '        "  left join dt_ampliacion_p on ms_ampliacion_p.id_ms_ampliacion_p = dt_ampliacion_p.id_ms_ampliacion_p " +
                '        "  left join ms_instancia inst on ms_ampliacion_p.id_ms_ampliacion_p = inst.id_ms_sol " +
                '        "where 1 = 1 "
                query = "select ms_ampliacion_p.id_ms_ampliacion_p " +
                        "     , solicita " +
                        "     , autorizador " +
                        "     , empresa " +
                        "     , centro_costo " +
                        "     , año " +
                        "     , mes " +
                        "     , monto_actual " +
                        "     , monto_solicita " +
                        "     , monto_solicita_val " +
                        "     , monto_nuevo " +
                        "     , case when impacto_pres_porcent is null then 'Sin monto previo para comparar' else format(impacto_pres_porcent, 'p', 'es-MX') end as impacto_pres_porcent " +
                        "     , fecha_solicita " +
                        "     , fecha_autoriza " +
                        "     , msHis.fecha as fecha_valida " +
                        "     , case  " +
                        "       when status = 'P' then 'Pendiente de Autorización' " +
                        "       when status = 'A' then 'Autorizado' " +
                        "       when status = 'VP' then 'Pendiente de Validación Presupuesto' " +
                        "       when status = 'Z' then 'Rechazado' " +
                        "       else '-' " +
                        "       end as estado" +
                        " from ms_ampliacion_p " +
                        "  left join dt_ampliacion_p on ms_ampliacion_p.id_ms_ampliacion_p = dt_ampliacion_p.id_ms_ampliacion_p " +
                        " left join ms_instancia inst on ms_ampliacion_p.id_ms_ampliacion_p = inst.id_ms_sol  and inst.tipo='SAP'" +
                        " left join ms_historico msHis on inst.id_ms_instancia = msHis.id_ms_instancia  and (msHis.id_actividad = 106 or msHis.id_actividad = 118) " +
                        " where 1 = 1 "

                If Val(._txtUsrAut.Text) > 1 Then
                    query = query + "and (solicita = @autorizadorU or autorizador = @autorizadorU) "
                End If

                If ._txtPnlAut.Text = "N" Or .cbSolicitante.Checked = True Then
                    query = query + "  and id_usr_solicita = @id_usr_solicita "
                End If

                If .cbEmpresa.Checked = True Then
                    query = query + "  and empresa = @empresa "
                End If
                If .cbFechaC.Checked = True Then
                    query = query + "  and (fecha_solicita between @fechaIni and @fechaFin) "
                End If
                If .cbAutorizador.Checked = True Then
                    query = query + "  and autorizador = @autorizador "
                End If
                If .cbFolio.Checked = True Then
                    query = query + "  and ms_ampliacion_p.id_ms_ampliacion_p = @id_ms_ampliacion_p"
                End If
                If .cbStatus.Checked = True Then
                    query = query + "  and status in (" + .ddlStatus.SelectedValue + ") "
                End If
                query = query + " order by mes "

                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)

                If Val(._txtUsrAut.Text) > 1 Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@autorizadorU", ._txtEmpleado.Text)
                End If

                If ._txtPnlAut.Text = "N" Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_usr_solicita", Val(._txtIdUsuario.Text))
                End If
                If .cbSolicitante.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_usr_solicita", .ddlSolicitante.SelectedValue)
                End If
                If .cbEmpresa.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedValue)
                End If
                If .cbFechaC.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaIni", .wdpFechaI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaFin", .wdpFechaF.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
                End If
                If .cbAutorizador.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@autorizador", .ddlAutorizador.SelectedValue)
                End If
                If .cbFolio.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_ms_ampliacion_p", .txtFolio.Text.Trim)
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