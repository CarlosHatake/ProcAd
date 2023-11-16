Public Class ContAntAud
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
                                "from ms_anticipo "
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
                                "from ms_anticipo "
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
                                "from ms_anticipo "
                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "Conta" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Then
                            query = query + "where empleado = @empleado "
                        End If
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            query = query + "where empleado = @autorizador or autorizador = @autorizador "
                        End If
                        query = query + "order by autorizador "
                        sdaAutorizador.SelectCommand = New SqlCommand(query, ConexionBD)
                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "Conta" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Then
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

                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "Conta" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Then
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
            .cbTipoAnt.Checked = False
            .pnlTipoAnt.Visible = False
            .cbNoAnticipo.Checked = False
            .pnlNoAnticipo.Visible = False
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

    Protected Sub cbTipoAnt_CheckedChanged(sender As Object, e As EventArgs) Handles cbTipoAnt.CheckedChanged
        vista(Me.pnlTipoAnt, Me.cbTipoAnt.Checked)
    End Sub

    Protected Sub cbNoSol_CheckedChanged(sender As Object, e As EventArgs) Handles cbNoAnticipo.CheckedChanged
        vista(Me.pnlNoAnticipo, Me.cbNoAnticipo.Checked)
        If Me.cbNoAnticipo.Checked = True Then
            Me.txtNoAnticipo.Text = ""
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

                query = "select ms_anticipo.id_ms_anticipo as no_anticipo " + _
                        "     , Empresa " + _
                        "     , empleado " + _
                        "     , autorizador " + _
                        "     , ms_anticipo.fecha_solicita " + _
                        "     , ms_anticipo.fecha_autoriza " + _
                        "     , fecha_pago " + _
                        "     , periodo_ini " + _
                        "     , periodo_fin " + _
                        "     , case ms_anticipo.tipo_pago when 'E' then 'Efectivo' when 'T' then 'Transferencia' else '-' end as tipoAnt " + _
                        "     , monto_hospedaje + monto_alimentos + monto_casetas + monto_otros as importe " + _
                        "     , case ms_anticipo.status " + _
                        "         when 'P' then 'Pendiente de Autorización' " + _
                        "         when 'A' then 'Autorizado' " + _
                        "         when 'Z' then 'Rechazado' " + _
                        "         when 'ZC' then 'Cancelado' " + _
                        "         when 'TR' then 'Transferencia Realizada' " + _
                        "         when 'TRCP' then 'Transferencia Realizada' " + _
                        "         when 'TRCA' then 'Transferencia Realizada' " + _
                        "         when 'TRCR' then 'Transferencia Realizada' " + _
                        "         when 'EE' then 'Efectivo Entregado' " + _
                        "         when 'EECP' then 'Efectivo Entregado' " + _
                        "         when 'EECA' then 'Efectivo Entregado' " + _
                        "         when 'EECR' then 'Efectivo Entregado' " + _
                        "       end as estatus " + _
                        "     , comp.id_ms_comp " + _
                        "     , comp.fecha_solicita as fecha_solicitaC " + _
                        "     , comp.fecha_autoriza as fecha_autorizaC " + _
                        "     , comp.fecha_aut_dir as fecha_aut_dirC " + _
                        "     , comp.fecha_valida as fecha_validaC " + _
                        "     , comp.status " + _
                        "     , case comp.status " + _
                        "         when 'P' then 'En Proceso' " + _
                        "         when 'A' then 'Autorizada' " + _
                        "         when 'R' then 'Registrada' " + _
                        "         else null " + _
                        "        end as comprobado " + _
                        "from ms_anticipo " + _
                        "  left join (select id_ms_anticipo " + _
                        "                  , ms_comp.id_ms_comp " + _
                        "                  , ms_comp.fecha_solicita " + _
                        "                  , ms_comp.fecha_autoriza " + _
                        "                  , ms_comp.fecha_aut_dir " + _
                        "                  , ms_comp.fecha_valida " + _
                        "                  , ms_comp.status " + _
                        "             from dt_anticipo " + _
                        "               left join ms_comp on dt_anticipo.id_ms_comp = ms_comp.id_ms_comp " + _
                        "             where ms_comp.status in ('P','A','R')) comp on ms_anticipo.id_ms_anticipo = comp.id_ms_anticipo " + _
                        "where 1 =1  "

                '"     , case (select isnull((select top 1 ms_comp.status " + _
                '"             from ms_comp " + _
                '"               left join dt_anticipo on dt_anticipo.id_ms_comp = ms_comp.id_ms_comp " + _
                '"             where ms_comp.status in ('P','A','R') " + _
                '"               and id_ms_anticipo = ms_anticipo.id_ms_anticipo), '-') as estatus) " + _
                '"         when 'P' then 'En Proceso' " + _
                '"         when 'A' then 'Autorizada' " + _
                '"         when 'R' then 'Registrada' " + _
                '"		 else null " + _
                '"	   end as comprobado " + _
                '"from ms_anticipo " + _


                If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                    query = query + "and (empleado = @autorizadorU or autorizador = @autorizadorU) "
                End If

                If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "Conta" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Or .cbSolicitante.Checked = True Then
                    query = query + "  and id_usr_solicita = @id_usr_solicita "
                End If

                If .cbEmpresa.Checked = True Then
                    query = query + "  and empresa = @empresa "
                End If
                If .cbFechaC.Checked = True Then
                    query = query + "  and (ms_anticipo.fecha_solicita between @fechaIni and @fechaFin) "
                End If
                If .cbAutorizador.Checked = True Then
                    query = query + "  and autorizador = @autorizador "
                End If
                If .cbNoAnticipo.Checked = True Then
                    query = query + "  and ms_anticipo.id_ms_anticipo = @id_ms_anticipo "
                End If
                If .cbStatus.Checked = True Then
                    query = query + "  and ms_anticipo.status in (" + .ddlStatus.SelectedValue + ") "
                End If
                If .cbTipoAnt.Checked = True Then
                    query = query + "  and ms_anticipo.tipo_pago = @tipo_pago "
                End If
                query = query + "order by ms_anticipo.id_ms_anticipo "

                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)

                If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@autorizadorU", ._txtEmpleado.Text)
                End If

                If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "Conta" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Then
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
                If .cbNoAnticipo.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_ms_anticipo", .txtNoAnticipo.Text.Trim)
                End If
                If .cbTipoAnt.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@tipo_pago", .ddlTipoAnt.SelectedValue)
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