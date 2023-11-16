Public Class ConsSolRecDet
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
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        Dim sdaEmpleado As New SqlDataAdapter
                        Dim dsEmpleado As New DataSet
                        sdaEmpleado.SelectCommand = New SqlCommand("select perfil, nombre + ' ' + ap_paterno + ' ' + ap_materno as empleado " + _
                                                                   "from cg_usuario " + _
                                                                   "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
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
                        Dim query As String
                        'Solicitantes
                        Dim sdaSolicitante As New SqlDataAdapter
                        Dim dsSolicitante As New DataSet
                        query = "select distinct(id_usr_solicita) as id_usr_solicita, empleado as solicito " + _
                                "from ms_recursos "
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            query = query + "where empleado = @autorizador or autorizador = @autorizador "
                        End If
                        query = query + "order by solicito "
                        sdaSolicitante.SelectCommand = New SqlCommand(query, ConexionBD)
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            sdaSolicitante.SelectCommand.Parameters.AddWithValue("@autorizador", ._txtEmpleado.Text)
                        End If
                        .ddlSolicitante.DataSource = dsSolicitante
                        .ddlSolicitante.DataTextField = "solicito"
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
                        query = "select distinct(empresa) as empresa " + _
                                "from ms_recursos "
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            query = query + "where empleado = @autorizador or autorizador = @autorizador "
                        End If
                        query = query + "order by empresa "
                        sdaEmpresa.SelectCommand = New SqlCommand(query, ConexionBD)
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
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
                        query = "select distinct(autorizador) as autorizador " + _
                                "from ms_recursos "
                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Conta" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Then
                            query = query + "where empleado = @empleado "
                        End If
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            query = query + "where empleado = @autorizador or autorizador = @autorizador "
                        End If
                        query = query + "order by autorizador "
                        sdaAutorizador.SelectCommand = New SqlCommand(query, ConexionBD)
                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Conta" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Then
                            sdaAutorizador.SelectCommand.Parameters.AddWithValue("@empleado", ._txtEmpleado.Text)
                        End If
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
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

                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Conta" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Then
                            .pnlSolicitó.Visible = False
                            .upSolicitante.Update()
                        Else
                            .pnlSolicitó.Visible = True
                            .cbSolicitante.Checked = False
                            .pnlSolicitante.Visible = False
                            .upSolicitante.Update()
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
                            .upEmpresa.Update()
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
            .upFechaC.Update()
            .wdpFechaI.Date = Date.Now.ToShortDateString
            .wdpFechaF.Date = Date.Now.ToShortDateString
            .cbAutorizador.Checked = False
            .pnlAutorizador.Visible = False
            .upAutorizador.Update()
            .cbStatus.Checked = False
            .pnlStatus.Visible = False
            .upStatus.Update()
            .cbNoSolRec.Checked = False
            .pnlNoSolRec.Visible = False
            .upNoSolRec.Update()
            'Ocultar resto de paneles principales
            .pnlRegistros.Visible = False
        End With
    End Sub

    Public Sub vista(ByRef control, ByVal valor, ByRef up)
        With Me
            If valor Then
                control.Visible = True
            Else
                control.Visible = False
            End If
            up.Update()
        End With
    End Sub

#End Region

#Region "Filtros"

    Protected Sub cbSolicitante_CheckedChanged(sender As Object, e As EventArgs) Handles cbSolicitante.CheckedChanged
        vista(Me.pnlSolicitante, Me.cbSolicitante.Checked, Me.upSolicitante)
    End Sub

    Protected Sub cbEmpresa_CheckedChanged(sender As Object, e As EventArgs) Handles cbEmpresa.CheckedChanged
        vista(Me.pnlEmpresa, Me.cbEmpresa.Checked, Me.upEmpresa)
    End Sub

    Protected Sub cbFechaC_CheckedChanged(sender As Object, e As EventArgs) Handles cbFechaC.CheckedChanged
        vista(Me.pnlFechaC, Me.cbFechaC.Checked, Me.upFechaC)
    End Sub

    Protected Sub cbAutorizador_CheckedChanged(sender As Object, e As EventArgs) Handles cbAutorizador.CheckedChanged
        vista(Me.pnlAutorizador, Me.cbAutorizador.Checked, Me.upAutorizador)
    End Sub

    Protected Sub cbNoSolRec_CheckedChanged(sender As Object, e As EventArgs) Handles cbNoSolRec.CheckedChanged
        vista(Me.pnlNoSolRec, Me.cbNoSolRec.Checked, Me.upNoSolRec)
        If Me.cbNoSolRec.Checked = True Then
            Me.txtNoSolRec.Text = ""
        End If
    End Sub

    Protected Sub cbStatus_CheckedChanged(sender As Object, e As EventArgs) Handles cbStatus.CheckedChanged
        vista(Me.pnlStatus, Me.cbStatus.Checked, Me.upStatus)
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

                query = "select ms_recursos.id_ms_recursos as Folio " + _
                        "     , ms_recursos.Empresa " + _
                        "     , ms_recursos.empleado as Solicitó " + _
                        "     , ms_recursos.autorizador as Autorizó " + _
                        "     , ms_recursos.fecha_solicita as [Fecha de Solicitud] " + _
                        "     , ms_recursos.fecha_autoriza as [Fecha de Autorización] " + _
                        "     , ms_recursos.lugar_orig as Origen " + _
                        "     , ms_recursos.lugar_dest as Destino " + _
                        "     , ms_recursos.destino as Lugar " + _
                        "     , ms_recursos.periodo_ini as [Desde] " + _
                        "     , ms_recursos.periodo_fin as [Hasta] " + _
                        "     , ms_recursos.actividad as [Actividad] " + _
                        "     , ms_recursos.id_ms_anticipo as [No. Anticipo] " + _
                        "     , ms_anticipo.dias_hospedaje as [Noches Hotel] " + _
                        "     , ms_anticipo.monto_hospedaje as [Monto Hospedaje] " + _
                        "     , case when ms_recursos.id_ms_reserva is null then ms_recursos.id_dt_hist_util else ms_recursos.id_ms_reserva end as [No. Reserva Vehículo] " + _
                        "     , case when ms_recursos.id_ms_reserva is null then case when ms_recursos.id_dt_hist_util is null then null else dt_hist_util.no_eco end else ms_reserva.no_eco end as [No. Económico] " + _
                        "     , case when ms_recursos.id_ms_reserva is null then case when ms_recursos.id_dt_hist_util is null then null else 'Asignado' end else 'Comodín' end as [Tipo Vehículo] " + _
                        "     , ms_recursos.lugares_dispo as [Lugares Disponibles] " + _
                        "     , ms_recursos.lugares_reque as [Lugares Requeridos] " + _
                        "     , ms_recursos.id_ms_comb as [No. Comb] " + _
                        "     , ms_recursos.id_ms_avion as [Reserva de Avión] " + _
                        "     , case when ms_recursos.id_ms_avion is null then null else case when ms_avion.justificacion is null then 'No' else 'Sí' end end as [Fuera de Tiempo] " + _
                        "     , case when ms_recursos.id_ms_avion is null then null else case when ms_avion.justificacion is null then null else ms_avion.justificacion end end as Justificación " + _
                        "     , case cg_empleado.puesto_tabulador " + _
                        "         when 'AdmJef' then 'Administrativos y Jefaturas' " + _
                        "         when 'GerDir' then 'Gerentes y Directivos' " + _
                        "         when 'Cho' then 'Choferes' " + _
                        "         when 'Mec' then 'Mecánicos' " + _
                        "         when 'DirGral' then 'Director General' " + _
                        "         else '' " + _
                        "       end as Tabulador " + _
                        "     , case ms_recursos.status " + _
                        "         when 'P' then 'Pendiente de Autorización' " + _
                        "         when 'A' then 'Autorizado' " + _
                        "         when 'Z' then 'Solicitud No Autorizada' " + _
                        "       end as Estatus " + _
                        "from ms_recursos " + _
                        "  left join ms_anticipo on ms_recursos.id_ms_anticipo = ms_anticipo.id_ms_anticipo " + _
                        "  left join ms_reserva on ms_recursos.id_ms_reserva = ms_reserva.id_ms_reserva " + _
                        "  left join dt_hist_util on ms_recursos.id_dt_hist_util = dt_hist_util.id_dt_hist_util " + _
                        "  left join ms_avion on ms_recursos.id_ms_avion = ms_avion.id_ms_avion " + _
                        "  left join cg_usuario on ms_recursos.id_usr_solicita = cg_usuario.id_usuario " + _
                        "  left join bd_empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                        "where 1 = 1 "

                If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                    query = query + "and (ms_recursos.empleado = @autorizadorU or ms_recursos.autorizador = @autorizadorU) "
                End If

                If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Conta" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Or .cbSolicitante.Checked = True Then
                    query = query + "  and ms_recursos.id_usr_solicita = @id_usr_solicita "
                End If
                If .cbEmpresa.Checked = True Then
                    query = query + "  and ms_recursos.Empresa = @empresa "
                End If
                If .cbFechaC.Checked = True Then
                    query = query + "  and (ms_recursos.fecha_solicita between @fechaIni and @fechaFin) "
                End If
                If .cbAutorizador.Checked = True Then
                    query = query + "  and ms_recursos.autorizador = @autorizador "
                End If
                If .cbNoSolRec.Checked = True Then
                    query = query + "  and ms_recursos.id_ms_recursos = @id_ms_recursos "
                End If
                If .cbStatus.Checked = True Then
                    query = query + "  and ms_recursos.status in (" + .ddlStatus.SelectedValue + ") "
                End If
                If .cbHospVehAvion.Checked = True Then
                    query = query + "  and (ms_anticipo.dias_hospedaje is not null or ms_recursos.id_dt_hist_util is not null or ms_recursos.id_ms_reserva is not null or ms_recursos.id_ms_avion is not null) "
                End If
                query = query + "order by ms_recursos.id_ms_recursos "

                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)

                If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@autorizadorU", ._txtEmpleado.Text)
                End If

                If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Conta" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Then
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
                If .cbNoSolRec.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_ms_recursos", .txtNoSolRec.Text.Trim)
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