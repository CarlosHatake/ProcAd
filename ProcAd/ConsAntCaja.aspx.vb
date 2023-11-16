Public Class ConsAntCaja
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
                                "from ms_comp "
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
                                "from ms_comp "
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

                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Then
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
            .cbStatus.Checked = True
            .pnlStatus.Visible = True
            .ddlStatus.SelectedIndex = 3
            .cbTipoAnt.Checked = True
            .pnlTipoAnt.Visible = True
            .cbNoAnticipo.Checked = False
            .pnlNoAnticipo.Visible = False
            'Comprobaciones
            .cbPagoEfec.Checked = False
            .pnlPagoEfec.Visible = False
            .cbFechaCC.Checked = False
            .pnlFechaCC.Visible = False
            .cbStatusC.Checked = True
            .pnlStatusC.Visible = True
            .ddlStatusC.SelectedIndex = 3
            .cbNoComp.Checked = False
            .pnlNoComp.Visible = False
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
    'Anticipos
    Protected Sub cbSolicitante_CheckedChanged(sender As Object, e As EventArgs) Handles cbSolicitante.CheckedChanged
        vista(Me.pnlSolicitante, Me.cbSolicitante.Checked)
    End Sub

    Protected Sub cbEmpresa_CheckedChanged(sender As Object, e As EventArgs) Handles cbEmpresa.CheckedChanged
        vista(Me.pnlEmpresa, Me.cbEmpresa.Checked)
    End Sub

    Protected Sub cbFechaC_CheckedChanged(sender As Object, e As EventArgs) Handles cbFechaC.CheckedChanged
        vista(Me.pnlFechaC, Me.cbFechaC.Checked)
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

    'Comprobaciones
    Protected Sub cbPagoEfec_CheckedChanged(sender As Object, e As EventArgs) Handles cbPagoEfec.CheckedChanged
        vista(Me.pnlPagoEfec, Me.cbPagoEfec.Checked)
    End Sub

    Protected Sub cbFechaCC_CheckedChanged(sender As Object, e As EventArgs) Handles cbFechaCC.CheckedChanged
        vista(Me.pnlFechaCC, Me.cbFechaCC.Checked)
    End Sub

    Protected Sub cbStatusC_CheckedChanged(sender As Object, e As EventArgs) Handles cbStatusC.CheckedChanged
        vista(Me.pnlStatusC, Me.cbStatusC.Checked)
    End Sub

    Protected Sub cbNoComp_CheckedChanged(sender As Object, e As EventArgs) Handles cbNoComp.CheckedChanged
        vista(Me.pnlNoComp, Me.cbNoComp.Checked)
        If Me.cbNoComp.Checked = True Then
            Me.txtNoComp.Text = ""
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
                Dim queryA As String
                Dim queryC As String
                Dim queryG As String

                queryA = "select ms_anticipo.id_ms_anticipo as [No. Anticipo] " +
                        "     , ms_anticipo.empleado as [Solicitante] " +
                        "     , ms_anticipo.Empresa " +
                        "	  , case ms_anticipo.tipo_pago when 'E' then 'Efectivo' when 'T' then 'Transferencia' else '-' end as [Tipo Anticipo] " +
                        "     , ms_anticipo.monto_hospedaje + ms_anticipo.monto_alimentos + ms_anticipo.monto_casetas + ms_anticipo.monto_otros as [Importe del Anticipo] " +
                        "     , ms_anticipo.fecha_pago as [Fecha de Entrega del Efectivo] " +
                        "     , case ms_anticipo.status " +
                        "         when 'P' then 'Pendiente de Autorización' " +
                        "         when 'A' then 'Autorizado' " +
                        "         when 'Z' then 'Rechazado' " +
                        "         when 'ZC' then 'Cancelado' " +
                        "         when 'TR' then 'Transferencia Realizada' " +
                        "         when 'TRCP' then 'Transferencia Realizada' " +
                        "         when 'TRCA' then 'Transferencia Realizada' " +
                        "         when 'TRCR' then 'Transferencia Realizada' " +
                        "         when 'EE' then 'Efectivo Entregado' " +
                        "         when 'EECP' then 'Efectivo Entregado' " +
                        "         when 'EECA' then 'Efectivo Entregado' " +
                        "         when 'EECR' then 'Efectivo Entregado' " +
                        "       end as [Estatus Anticipo] " +
                        "     , msComp.id_ms_comp as [No. Comprobación] " +
                        "     , msComp.fecha_valida as [Fecha de Validación de la comprobación] " +
                        "     , case msComp.status " +
                        "         when 'P' then 'En Proceso' " +
                        "         when 'A' then 'Autorizada' " +
                        "         when 'R' then 'Registrada' " +
                        "		 else null " +
                        "	    end as [Estatus Comprobación] " +
                        "     , (select sum(monto_hospedaje) + sum(monto_alimentos) + sum(monto_casetas) + sum(monto_otros) as monto_ant " +
                        "        from dt_anticipo " +
                        "            left join ms_anticipo on dt_anticipo.id_ms_anticipo = ms_anticipo.id_ms_anticipo " +
                        "        where dt_anticipo.id_ms_comp = msComp.id_ms_comp) as [Total Anticipo] " +
                        "     , (select sum(monto_total) as monto_comp " +
                        "        from dt_comp " +
                        "        where dt_comp.id_ms_comp = msComp.id_ms_comp) as [Total Comprobado] " +
                        "     , (select isnull(sum(monto_hospedaje) + sum(monto_alimentos) + sum(monto_casetas) + sum(monto_otros), 0) as monto_ant " +
                        "        from dt_anticipo " +
                        "            left join ms_anticipo on dt_anticipo.id_ms_anticipo = ms_anticipo.id_ms_anticipo " +
                        "        where dt_anticipo.id_ms_comp = msComp.id_ms_comp) - " +
                        "		(select sum(monto_total) as monto_comp " +
                        "        from dt_comp " +
                        "        where dt_comp.id_ms_comp = msComp.id_ms_comp) as [Saldo Comprobación] " +
                        "     , case when len(evidencia_adj) > 30 then substring(evidencia_adj, 0, 30) + '...' else evidencia_adj end as [Evidencia] " +
                        "     , 'http://148.223.153.43/ProcAd - Adjuntos/' + cast(id_ms_comp as varchar) + 'Evid-' + evidencia_adj as evidencia_ruta " +
                        "     , case when len(vale_ingreso) > 30 then substring(vale_ingreso, 0, 30) + '...' else vale_ingreso end as [Vale Ingreso] " +
                        "     , 'http://148.223.153.43/ProcAd - Adjuntos/' + cast(id_ms_comp as varchar) + 'ValeI-' + vale_ingreso_adj as vale_ruta " +
                        "	  , case pago_efectivo when 'S' then 'Sí' when 'N' then 'No' else null end as [Pago Efectivo] " +
                        "	  , fecha_efectivo as [Fecha Efectivo] " +
                        "from ms_anticipo " +
                        "  left join (select ms_comp.id_ms_comp " +
                        "                  , dt_anticipo.id_ms_anticipo " +
                        "                  , ms_comp.status " +
                        "                  , ms_comp.fecha_valida " +
                        "                  , ms_comp.evidencia_adj " +
                        "                  , ms_comp.vale_ingreso " +
                        "                  , ms_comp.vale_ingreso_adj " +
                        "                  , ms_comp.pago_efectivo " +
                        "                  , ms_comp.fecha_efectivo " +
                        "             from ms_comp " +
                        "               left join dt_anticipo on dt_anticipo.id_ms_comp = ms_comp.id_ms_comp " +
                        "             where ms_comp.status in ('P','A','R')) as msComp on ms_anticipo.id_ms_anticipo = msComp.id_ms_anticipo " +
                        "where 1 = 1 "

                queryC = "select null as [No. Anticipo] " +
                         "     , ms_comp.empleado as [Solicitante] " +
                         "     , ms_comp.Empresa " +
                         "     , null as [Tipo Anticipo] " +
                         "     , null as [Importe del Anticipo] " +
                         "     , null as [Fecha de Entrega del Efectivo] " +
                         "     , null as [Estatus Anticipo] " +
                         "     , ms_comp.id_ms_comp as [No. Comprobación] " +
                         "     , ms_comp.fecha_valida as [Fecha de Validación de la comprobación] " +
                         "     , case ms_comp.status " +
                         "         when 'P' then 'En Proceso' " +
                         "         when 'A' then 'Autorizada' " +
                         "         when 'R' then 'Registrada' " +
                         "		 else null " +
                         "	     end as [Estatus Comprobación] " +
                         "     , (select sum(monto_hospedaje) + sum(monto_alimentos) + sum(monto_casetas) + sum(monto_otros) as monto_ant " +
                         "        from dt_anticipo " +
                         "            left join ms_anticipo on dt_anticipo.id_ms_anticipo = ms_anticipo.id_ms_anticipo " +
                         "        where dt_anticipo.id_ms_comp = ms_comp.id_ms_comp) as [Total Anticipo] " +
                         "     , (select sum(monto_total) as monto_comp " +
                         "        from dt_comp " +
                         "        where dt_comp.id_ms_comp = ms_comp.id_ms_comp) as [Total Comprobado] " +
                         "     , (select isnull(sum(monto_hospedaje) + sum(monto_alimentos) + sum(monto_casetas) + sum(monto_otros), 0) as monto_ant " +
                         "        from dt_anticipo " +
                         "            left join ms_anticipo on dt_anticipo.id_ms_anticipo = ms_anticipo.id_ms_anticipo " +
                         "        where dt_anticipo.id_ms_comp = ms_comp.id_ms_comp) - " +
                         "		 (select sum(monto_total) as monto_comp " +
                         "        from dt_comp " +
                         "        where dt_comp.id_ms_comp = ms_comp.id_ms_comp) as [Saldo Comprobación] " +
                         "     , case when len(evidencia_adj) > 30 then substring(evidencia_adj, 0, 30) + '...' else evidencia_adj end as [Evidencia] " +
                         "     , 'http://148.223.153.43/ProcAd - Adjuntos/' + cast(id_ms_comp as varchar) + 'Evid-' + evidencia_adj as evidencia_ruta " +
                         "     , case when len(vale_ingreso) > 30 then substring(vale_ingreso, 0, 30) + '...' else vale_ingreso end as [Vale Ingreso] " +
                         "     , 'http://148.223.153.43/ProcAd - Adjuntos/' + cast(id_ms_comp as varchar) + 'ValeI-' + vale_ingreso_adj as vale_ruta " +
                         "	   , case pago_efectivo when 'S' then 'Sí' when 'N' then 'No' else null end as [Pago Efectivo] " +
                         "	   , fecha_efectivo as [Fecha Efectivo] " +
                         "from ms_comp " +
                         "where id_ms_comp not in (select distinct(id_ms_comp) " +
                         "                         from dt_anticipo) "

                If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                    queryA = queryA + "and (empleado = @autorizadorU or autorizador = @autorizadorU) "
                    'queryC = queryC + "and (empleado = @autorizadorU or autorizador = @autorizadorU) "
                End If

                If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or .cbSolicitante.Checked = True Then
                    queryA = queryA + "  and id_usr_solicita = @id_usr_solicita "
                    'queryC = queryC + "  and id_usr_solicita = @id_usr_solicita "
                End If
                If .cbEmpresa.Checked = True Then
                    queryA = queryA + "  and empresa = @empresa "
                    queryC = queryC + "  and empresa = @empresa "
                End If
                If .cbFechaC.Checked = True Then
                    queryA = queryA + "  and (ms_anticipo.fecha_solicita between @fechaIniA and @fechaFinA) "
                End If
                If .cbNoAnticipo.Checked = True Then
                    queryA = queryA + "  and ms_anticipo.id_ms_anticipo = @id_ms_anticipo "
                End If
                If .cbStatus.Checked = True Then
                    queryA = queryA + "  and ms_anticipo.status in (" + .ddlStatus.SelectedValue + ") "
                End If
                If .cbTipoAnt.Checked = True Then
                    queryA = queryA + "  and ms_anticipo.tipo_pago = @tipo_pago "
                End If

                'Comprobaciones
                If .cbFechaCC.Checked = True Then
                    queryC = queryC + "  and (ms_comp.fecha_solicita between @fechaIniC and @fechaFinC) "
                End If
                If .cbPagoEfec.Checked = True Then
                    queryC = queryC + "  and ms_comp.pago_efectivo = @pago_efectivo "
                End If
                If .cbNoComp.Checked = True Then
                    queryC = queryC + "  and ms_comp.id_ms_comp = @id_ms_comp "
                End If
                If .cbStatus.Checked = True Then
                    queryC = queryC + "  and ms_comp.status in (" + .ddlStatusC.SelectedValue + ") "
                End If


                queryG = queryA + "union all " + queryC + "order by [Fecha de Validación de la comprobación] "

                sdaConsulta.SelectCommand = New SqlCommand(queryG, ConexionBD)

                If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@autorizadorU", ._txtEmpleado.Text)
                End If

                If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_usr_solicita", Val(._txtIdUsuario.Text))
                End If
                If .cbSolicitante.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_usr_solicita", .ddlSolicitante.SelectedValue)
                End If
                If .cbEmpresa.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedValue)
                End If
                If .cbFechaC.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaIniA", .wdpFechaI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaFinA", .wdpFechaF.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
                End If
                If .cbNoAnticipo.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_ms_anticipo", .txtNoAnticipo.Text.Trim)
                End If
                If .cbTipoAnt.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@tipo_pago", .ddlTipoAnt.SelectedValue)
                End If
                'Comprobaciones
                If .cbFechaCC.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaIniC", .wdpFechaCI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaFinC", .wdpFechaCF.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
                End If
                If .cbPagoEfec.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@pago_efectivo", .ddlPagoEfec.SelectedValue)
                End If
                If .cbNoComp.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_ms_comp", .txtNoComp.Text.Trim)
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