Public Class ConsReunion
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 1
                    'Session("id_usuario") = 21

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD

                        'Llenar lista de Grupos
                        Dim sdaGrupo As New SqlDataAdapter
                        Dim dsGrupo As New DataSet
                        sdaGrupo.SelectCommand = New SqlCommand("select distinct(cg_grupo.id_grupo) " + _
                                                                "     , grupo " + _
                                                                "from cg_grupo " + _
                                                                "  left join dt_grupo on cg_grupo.id_grupo = dt_grupo.id_grupo and dt_grupo.status = 'A' " + _
                                                                "where cg_grupo.status = 'A' " + _
                                                                "  and (id_usr_secretario = @idUsuario " + _
                                                                "    or id_usr_lider = @idUsuario) ", ConexionBD)
                        sdaGrupo.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        .ddlGrupo.DataSource = dsGrupo
                        .ddlGrupo.DataTextField = "grupo"
                        .ddlGrupo.DataValueField = "id_grupo"
                        ConexionBD.Open()
                        sdaGrupo.Fill(dsGrupo)
                        .ddlGrupo.DataBind()
                        ConexionBD.Close()
                        sdaGrupo.Dispose()
                        dsGrupo.Dispose()
                        .ddlGrupo.SelectedIndex = -1

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
            'Ocultar todos los paneles principales
            .pnlFiltros.Visible = True
            .pnlTickets.Visible = False
            .pnlDetalle.Visible = False
            'Filtros
            .cbFecha.Checked = False
            .pnlFecha.Visible = False
            .wdpFechaI.Date = Now.Date
            .wdpFechaF.Date = Now.Date
            .cbTema.Checked = False
            .pnlTema.Visible = False
            .cbGrupo.Checked = False
            .pnlGrupo.Visible = False
            .cbNoReunion.Checked = False
            .pnlNoReunion.Visible = False
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

    Private Sub cbFecha_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFecha.CheckedChanged
        vista(Me.pnlFecha, Me.cbFecha.Checked)
    End Sub

    Private Sub cbTema_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTema.CheckedChanged
        vista(Me.pnlTema, Me.cbTema.Checked)
        If Me.cbTema.Checked = True Then
            Me.txtTema.Text = ""
        End If
    End Sub

    Private Sub cbGrupo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbGrupo.CheckedChanged
        vista(Me.pnlGrupo, Me.cbGrupo.Checked)
    End Sub

    Protected Sub cbNoActividad_CheckedChanged(sender As Object, e As EventArgs) Handles cbNoReunion.CheckedChanged
        vista(Me.pnlNoReunion, Me.cbNoReunion.Checked)
        If Me.cbNoReunion.Checked = True Then
            Me.txtNoReunion.Text = ""
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

                query = "select id_ms_reunion " + _
                        "     , ms_reunion.grupo " + _
                        "     , tema " + _
                        "     , cgEmplLid.nombre + ' ' + cgEmplLid.ap_paterno + ' ' + cgEmplLid.ap_materno as lider " + _
                        "     , cgEmplSec.nombre + ' ' + cgEmplSec.ap_paterno + ' ' + cgEmplSec.ap_materno as secretario " + _
                        "     , fecha_reunion " + _
                        "     , fecha_inicio " + _
                        "     , fecha_cierre " + _
                        "     , calif_gral " + _
                        "     , case ms_reunion.status " + _
                        "         when 'P' then 'Pendiente' " + _
                        "         when 'I' then 'Iniciada' " + _
                        "         when 'F' then 'Finalizada' " + _
                        "         when 'Z' then 'Cancelada' " + _
                        "         when 'ZU' then 'Cancelada por Usuario' " + _
                        "         else '-' " + _
                        "       end as estatus " + _
                        "from ms_reunion " + _
                        "  left join cg_grupo on ms_reunion.id_grupo = cg_grupo.id_grupo " + _
                        "  left join cg_usuario cgUsrSec on ms_reunion.id_usr_secretario = cgUsrSec.id_usuario " + _
                        "  left join bd_Empleado.dbo.cg_empleado cgEmplSec on cgUsrSec.id_empleado = cgEmplSec.id_empleado " + _
                        "  left join cg_usuario cgUsrLid on cg_grupo.id_usr_lider = cgUsrLid.id_usuario " + _
                        "  left join bd_Empleado.dbo.cg_empleado cgEmplLid on cgUsrLid.id_empleado = cgEmplLid.id_empleado " + _
                        "where id_ms_reunion in (select distinct(id_ms_reunion) " + _
                        "                        from ms_reunion " + _
                        "                          left join cg_grupo on ms_reunion.id_grupo = cg_grupo.id_grupo " + _
                        "                        where cg_grupo.status = 'A' " + _
                        "                          and (ms_reunion.id_usr_secretario = @idUsuario " + _
                        "                            or id_usr_lider = @idUsuario)) "

                If .cbFecha.Checked = True Then
                    query = query + "  and (ms_reunion.fecha_reunion between @FI and @FT) "
                End If
                If .cbTema.Checked = True Then
                    query = query + "  and ms_reunion.tema = @tema "
                End If
                If .cbGrupo.Checked = True Then
                    query = query + "  and ms_reunion.id_grupo = @idGrupo "
                End If
                If .cbNoReunion.Checked = True Then
                    query = query + "  and ms_reunion.id_ms_reunion = @idMsReunion "
                End If

                query = query + "order by ms_reunion.id_ms_reunion "

                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaConsulta.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))

                If .cbFecha.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@FI", .wdpFechaI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@FT", .wdpFechaF.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
                End If
                If .cbTema.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@tema", .txtTema.Text.Trim)
                End If
                If .cbGrupo.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@idGrupo", .ddlGrupo.SelectedValue)
                End If
                If .cbNoReunion.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@idMsReunion", Val(.txtNoReunion.Text.Trim))
                End If

                .gvReuniones.DataSource = dsConsulta
                .gvReunionesT.DataSource = dsConsulta
                ConexionBD.Open()
                sdaConsulta.Fill(dsConsulta)
                ConexionBD.Close()
                .gvReuniones.DataBind()
                .gvReunionesT.DataBind()
                sdaConsulta.Dispose()
                dsConsulta.Dispose()
                .gvReuniones.SelectedIndex = -1
                If .gvReuniones.Rows.Count = 0 Then
                    .pnlTickets.Visible = False
                    .litError.Text = "No existe Registros para esos valores"
                Else
                    .pnlTickets.Visible = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Tabla"

    Protected Sub gvReuniones_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvReuniones.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                .lblNoReunion.Text = .gvReuniones.SelectedRow.Cells(1).Text
                .pnlTickets.Visible = False
                .pnlDetalle.Visible = True
                'Llenar la información correspondiente a la actividad
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaTicket As New SqlDataAdapter
                Dim dsTicket As New DataSet
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD

                sdaTicket.SelectCommand = New SqlCommand("select tema " + _
                                                         "     , grupo " + _
                                                         "     , fecha_reunion " + _
                                                         "     , isnull(calif_gral, -1) as calif " + _
                                                         "from ms_reunion " + _
                                                         "where id_ms_reunion = @idMsReunion ", ConexionBD)
                sdaTicket.SelectCommand.Parameters.AddWithValue("@idMsReunion", Val(.lblNoReunion.Text))
                ConexionBD.Open()
                sdaTicket.Fill(dsTicket)
                ConexionBD.Close()
                .lblTema.Text = dsTicket.Tables(0).Rows(0).Item("tema").ToString()
                .lblGrupo.Text = dsTicket.Tables(0).Rows(0).Item("grupo").ToString()
                .lblFecha.Text = dsTicket.Tables(0).Rows(0).Item("fecha_reunion").ToString()
                If Val(dsTicket.Tables(0).Rows(0).Item("calif").ToString()) = -1 Then
                    .lblCalif.Text = ""
                Else
                    .lblCalif.Text = dsTicket.Tables(0).Rows(0).Item("calif").ToString()
                End If
                sdaTicket.Dispose()
                dsTicket.Dispose()

                'Asistencia
                Dim sdaParticipanteAs As New SqlDataAdapter
                Dim dsParticipanteAs As New DataSet
                .gvAsistencia.Columns(0).Visible = True
                .gvAsistencia.DataSource = dsParticipanteAs
                sdaParticipanteAs.SelectCommand = New SqlCommand("select id_dt_reunion " + _
                                                                "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as integrante " + _
                                                                "     , replace(replace(replace(right(convert(varchar, llegada_fecha, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') as llegada_hora " + _
                                                                "from dt_reunion " + _
                                                                "  left join cg_usuario on dt_reunion.id_usr_part = cg_usuario.id_usuario " + _
                                                                "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                                "where id_ms_reunion = @id_ms_reunion ", ConexionBD)
                sdaParticipanteAs.SelectCommand.Parameters.AddWithValue("@id_ms_reunion", Val(.lblNoReunion.Text))
                ConexionBD.Open()
                sdaParticipanteAs.Fill(dsParticipanteAs)
                .gvAsistencia.DataBind()
                ConexionBD.Close()
                sdaParticipanteAs.Dispose()
                dsParticipanteAs.Dispose()
                .gvAsistencia.Columns(0).Visible = False
                .gvAsistencia.SelectedIndex = -1

                'Participación
                Dim sdaParticipantePa As New SqlDataAdapter
                Dim dsParticipantePa As New DataSet
                .gvParticipacion.Columns(0).Visible = True
                .gvParticipacion.DataSource = dsParticipantePa
                sdaParticipantePa.SelectCommand = New SqlCommand("select id_dt_reunion " + _
                                                                "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as integrante " + _
                                                                "     , particip_pos " + _
                                                                "from dt_reunion " + _
                                                                "  left join cg_usuario on dt_reunion.id_usr_part = cg_usuario.id_usuario " + _
                                                                "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                                "where id_ms_reunion = @id_ms_reunion ", ConexionBD)
                sdaParticipantePa.SelectCommand.Parameters.AddWithValue("@id_ms_reunion", Val(.lblNoReunion.Text))
                ConexionBD.Open()
                sdaParticipantePa.Fill(dsParticipantePa)
                .gvParticipacion.DataBind()
                ConexionBD.Close()
                sdaParticipantePa.Dispose()
                dsParticipantePa.Dispose()
                .gvParticipacion.Columns(0).Visible = False
                .gvParticipacion.SelectedIndex = -1

                'Actividades
                Dim sdaParticipanteAc As New SqlDataAdapter
                Dim dsParticipanteAc As New DataSet
                .gvActividad.Columns(0).Visible = True
                .gvActividad.DataSource = dsParticipanteAc
                sdaParticipanteAc.SelectCommand = New SqlCommand("select id_dt_reunion " + _
                                                                "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as integrante " + _
                                                                "     , activ_tot as total " + _
                                                                "     , activ_et as et " + _
                                                                "     , activ_ft as ft " + _
                                                                "     , activ_calif as calif " + _
                                                                "from dt_reunion " + _
                                                                "  left join cg_usuario on dt_reunion.id_usr_part = cg_usuario.id_usuario " + _
                                                                "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                                "where id_ms_reunion = @id_ms_reunion ", ConexionBD)
                sdaParticipanteAc.SelectCommand.Parameters.AddWithValue("@id_ms_reunion", Val(.lblNoReunion.Text))
                ConexionBD.Open()
                sdaParticipanteAc.Fill(dsParticipanteAc)
                .gvActividad.DataBind()
                ConexionBD.Close()
                sdaParticipanteAc.Dispose()
                dsParticipanteAc.Dispose()
                .gvActividad.Columns(0).Visible = False
                .gvActividad.SelectedIndex = -1

                'Calificación General
                Dim sdaParticipanteCG As New SqlDataAdapter
                Dim dsParticipanteCG As New DataSet
                .gvCalificacion.Columns(0).Visible = True
                .gvCalificacion.DataSource = dsParticipanteCG
                sdaParticipanteCG.SelectCommand = New SqlCommand("select id_dt_reunion " + _
                                                                "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as integrante " + _
                                                                "     , reunion_calif " + _
                                                                "from dt_reunion " + _
                                                                "  left join cg_usuario on dt_reunion.id_usr_part = cg_usuario.id_usuario " + _
                                                                "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                                "where id_ms_reunion = @id_ms_reunion ", ConexionBD)
                sdaParticipanteCG.SelectCommand.Parameters.AddWithValue("@id_ms_reunion", Val(.lblNoReunion.Text))
                ConexionBD.Open()
                sdaParticipanteCG.Fill(dsParticipanteCG)
                .gvCalificacion.DataBind()
                ConexionBD.Close()
                sdaParticipanteCG.Dispose()
                dsParticipanteCG.Dispose()
                .gvCalificacion.Columns(0).Visible = False
                .gvCalificacion.SelectedIndex = -1

                .pnlFiltros.Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Detalle"

    Protected Sub btnNuevoBus_Click(sender As Object, e As EventArgs) Handles btnNuevoBus.Click
        With Me
            'Ocultar todos los paneles principales
            .pnlFiltros.Visible = True
            .pnlTickets.Visible = False
            .pnlDetalle.Visible = False
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
                Response.AddHeader("Content-Disposition", "attachment;filename=Tickets.xls")
                Response.ContentType = "application/vnd.ms-excel"
                .gvReunionesT.Visible = True
                .gvReunionesT.RenderControl(hw)
                .gvReunionesT.Visible = False
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