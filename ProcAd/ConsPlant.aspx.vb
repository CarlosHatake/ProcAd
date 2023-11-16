Public Class ConsPlant
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
                        Dim query As String
                        'Solicitantes
                        Dim sdaSolicitante As New SqlDataAdapter
                        Dim dsSolicitante As New DataSet
                        query = "select distinct(id_usr_solicita) as id_usr_solicita, empleado as solicito " +
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
                        query = "select distinct(empresa) as empresa " +
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
                        'Autorizadores
                        Dim sdaAutorizador As New SqlDataAdapter
                        Dim dsAutorizador As New DataSet
                        query = "select distinct(autorizador) as autorizador " +
                                "from ms_comp "
                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "Liq" Then
                            query = query + "where empleado = @empleado "
                        End If
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            query = query + "where empleado = @autorizador or autorizador = @autorizador "
                        End If
                        query = query + "order by autorizador "
                        sdaAutorizador.SelectCommand = New SqlCommand(query, ConexionBD)
                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "Liq" Then
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

                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "Liq" Then
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
            .cbNoComp.Checked = False
            .pnlNoComp.Visible = False
            'Ocultar resto de paneles principales
            .pnlRegistros.Visible = False
            .pnlDetalle.Visible = False
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

    Public Sub mostrarTabla()
        If Me.rblTabla.SelectedValue = "F" Then
            Me.gvConceptos.Visible = True
            Me.gvPlantilla.Visible = False
        Else
            Me.gvConceptos.Visible = False
            Me.gvPlantilla.Visible = True
        End If
    End Sub

    Public Sub pintarTabla(ByRef gridView)
        With Me
            Try
                Dim i As Integer
                Dim ban As Integer
                For i = 0 To (gridView.Rows.Count() - 1)
                    ban = 0
                    If gridView.Rows(i).Cells(6).Text.Trim = "&nbsp;" Then
                        ban = 0
                    Else
                        If Val(gridView.Rows(i).Cells(6).Text.Trim) < 0 Then
                            ban = 1
                        End If
                    End If
                    Select Case ban
                        Case 0
                            gridView.Rows(i).Cells(6).ForeColor = Color.Black
                            gridView.Rows(i).Cells(6).Font.Bold = False
                        Case 1
                            gridView.Rows(i).Cells(6).ForeColor = Color.Red
                            gridView.Rows(i).Cells(6).Font.Bold = True
                    End Select
                    If gridView.Rows(i).Cells(7).Text.Trim <> "&nbsp;" Then
                        If Val(gridView.Rows(i).Cells(7).Text) = 0 Then
                            gridView.Rows(i).Cells(7).ForeColor = Color.Red
                            gridView.Rows(i).Cells(7).Font.Bold = False
                        Else
                            gridView.Rows(i).Cells(7).ForeColor = Color.Green
                            gridView.Rows(i).Cells(7).Font.Bold = False
                        End If
                    End If

                Next
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub


#End Region

#Region "Filtros"

    Protected Sub cbSolicitante_CheckedChanged(sender As Object, e As EventArgs) Handles cbSolicitante.CheckedChanged
        vista(Me.pnlSolicitante, Me.cbSolicitante.Checked)
    End Sub

    Protected Sub cbEmpresa_CheckedChanged(sender As Object, e As EventArgs) Handles cbEmpresa.CheckedChanged
        vista(Me.pnlEmpresa, Me.cbEmpresa.Checked)
    End Sub

    Protected Sub cbFechaC_CheckedChanged(sender As Object, e As EventArgs) Handles cbFechaC.CheckedChanged
        vista(Me.pnlFechaC, Me.cbFechaC.Checked)
    End Sub

    Protected Sub cbAutorizador_CheckedChanged(sender As Object, e As EventArgs) Handles cbAutorizador.CheckedChanged
        vista(Me.pnlAutorizador, Me.cbAutorizador.Checked)
    End Sub

    Protected Sub cbNoSol_CheckedChanged(sender As Object, e As EventArgs) Handles cbNoComp.CheckedChanged
        vista(Me.pnlNoComp, Me.cbNoComp.Checked)
        If Me.cbNoComp.Checked = True Then
            Me.txtNoComp.Text = ""
        End If
    End Sub

    Protected Sub cbStatus_CheckedChanged(sender As Object, e As EventArgs) Handles cbStatus.CheckedChanged
        vista(Me.pnlStatus, Me.cbStatus.Checked)
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

                query = "select id_ms_comp as no_comp " +
                        "     , empleado " +
                        "     , autorizador " +
                        "     , fecha_solicita " +
                        "     , fecha_autoriza " +
                        "     , fecha_valida " +
                        "     , periodo_ini " +
                        "     , periodo_fin " +
                        "     , (select sum(monto_hospedaje) + sum(monto_alimentos) + sum(monto_casetas) + sum(monto_otros) as monto_ant " +
                        "        from dt_anticipo " +
                        "          left join ms_anticipo on dt_anticipo.id_ms_anticipo = ms_anticipo.id_ms_anticipo " +
                        "        where dt_anticipo.id_ms_comp = ms_comp.id_ms_comp) as importe_ant " +
                        "     , (select sum(monto_total) as monto_comp " +
                        "        from dt_comp " +
                        "        where dt_comp.id_ms_comp = ms_comp.id_ms_comp) as importe_comp " +
                        "     , importe_tot " +
                        "     , case status " +
                        "         when 'P' then 'Pendiente de Autorización' " +
                        "         when 'A' then 'Autorizado' " +
                        "         when 'ZA' then 'Comprobación No Autorizada' " +
                        "         when 'ZD' then 'Comprobación No Autorizada por Director' " +
                        "         when 'ZC' then 'Comprobación Cancelada' " +
                        "         when 'ZP' then 'Comprobación Cancelada' " +
                        "         when 'ZU' then 'Comprobación Cancelada por el Usuario' " +
                        "         when 'R' then 'Comprobación Registrada' " +
                        "       end as estatus " +
                        "from ms_comp " +
                        "where 1 =1  "

                If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                    query = query + "and (empleado = @autorizadorU or autorizador = @autorizadorU) "
                End If

                If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "Liq" Or .cbSolicitante.Checked = True Then
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
                If .cbNoComp.Checked = True Then
                    query = query + "  and id_ms_comp = @id_ms_comp "
                End If
                If .cbStatus.Checked = True Then
                    query = query + "  and status in (" + .ddlStatus.SelectedValue + ") "
                End If
                query = query + "order by id_ms_comp "

                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)

                If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@autorizadorU", ._txtEmpleado.Text)
                End If

                If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "Liq" Then
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

#Region "Tabla - Solicitudes"

    Protected Sub gvRegistros_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRegistros.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                .pnlFiltros.Visible = False
                .pnlRegistros.Visible = False
                .pnlDetalle.Visible = False

                .lblFolio.Text = .gvRegistros.SelectedRow.Cells(1).Text

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                'Datos de la Comprobación
                Dim sdaSol As New SqlDataAdapter
                Dim dsSol As New DataSet
                Dim query As String
                query = "select empresa " +
                        "     , periodo_comp " +
                        "     , empleado " +
                        "     , tipo_gasto " +
                        "     , tipo_actividad " +
                        "     , autorizador " +
                        "     , isnull(centro_costo, 'XX') as centro_costo " +
                        "     , isnull(division, 'XX') as division " +
                        "     , justificacion " +
                        "     , isnull(vale_ingreso, 'XX') as vale_ingreso " +
                        "     , isnull(evidencia_adj, 'XX') as evidencia_adj " +
                        "     , vale_ingreso_adj " +
                        "from ms_comp " +
                        "where id_ms_comp = @id_ms_comp "
                sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaSol.SelectCommand.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaSol.Fill(dsSol)
                ConexionBD.Close()
                .lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                .lblPeriodo.Text = dsSol.Tables(0).Rows(0).Item("periodo_comp").ToString()
                .lblEmpleado.Text = dsSol.Tables(0).Rows(0).Item("empleado").ToString()
                .lblTipoGasto.Text = dsSol.Tables(0).Rows(0).Item("tipo_gasto").ToString()
                .lblTipoAct.Text = dsSol.Tables(0).Rows(0).Item("tipo_actividad").ToString()
                .lblAutorizador.Text = dsSol.Tables(0).Rows(0).Item("autorizador").ToString()
                If dsSol.Tables(0).Rows(0).Item("centro_costo").ToString() = "XX" Then
                    .lbl_CC.Visible = False
                    .lblCC.Visible = False
                    .lblCC.Text = ""
                Else
                    .lbl_CC.Visible = True
                    .lblCC.Visible = True
                    .lblCC.Text = dsSol.Tables(0).Rows(0).Item("centro_costo").ToString()
                End If
                If dsSol.Tables(0).Rows(0).Item("division").ToString() = "XX" Then
                    .lbl_Div.Visible = False
                    .lblDiv.Visible = False
                    .lblDiv.Text = ""
                Else
                    .lbl_Div.Visible = True
                    .lblDiv.Visible = True
                    .lblDiv.Text = dsSol.Tables(0).Rows(0).Item("division").ToString()
                End If
                .txtJust.Text = dsSol.Tables(0).Rows(0).Item("justificacion").ToString()
                If dsSol.Tables(0).Rows(0).Item("vale_ingreso").ToString() = "XX" Then
                    .lbl_ValeI.Visible = False
                    .hlValeI.Visible = False
                Else
                    .lbl_ValeI.Visible = True
                    .hlValeI.Visible = True
                    .hlValeI.Text = dsSol.Tables(0).Rows(0).Item("vale_ingreso").ToString()
                    '.hlValeI.NavigateUrl = "http://localhost/ProcAd - Adjuntos/" + .lblFolio.Text + "ValeI-" + dsSol.Tables(0).Rows(0).Item("vale_ingreso_adj").ToString()
                    .hlValeI.NavigateUrl = "http://148.223.153.43/ProcAd - Adjuntos/" + .lblFolio.Text + "ValeI-" + dsSol.Tables(0).Rows(0).Item("vale_ingreso_adj").ToString()
                End If
                If dsSol.Tables(0).Rows(0).Item("evidencia_adj").ToString() = "XX" Then
                    .lblEvidenciaN.Visible = True
                    .hlEvidencia.Visible = False
                Else
                    .lblEvidenciaN.Visible = False
                    .hlEvidencia.Visible = True
                    .hlEvidencia.Text = dsSol.Tables(0).Rows(0).Item("evidencia_adj").ToString()
                    '.hlEvidencia.NavigateUrl = "http://localhost/ProcAd - Adjuntos/" + .lblFolio.Text + "Evid-" + dsSol.Tables(0).Rows(0).Item("evidencia_adj").ToString()
                    .hlEvidencia.NavigateUrl = "http://148.223.153.43/ProcAd - Adjuntos/" + .lblFolio.Text + "Evid-" + dsSol.Tables(0).Rows(0).Item("evidencia_adj").ToString()
                End If
                sdaSol.Dispose()
                dsSol.Dispose()

                'Anticipos
                Dim sdaAnticipo As New SqlDataAdapter
                Dim dsAnticipo As New DataSet
                .gvAnticipos.DataSource = dsAnticipo
                sdaAnticipo.SelectCommand = New SqlCommand("select ms_anticipo.id_ms_anticipo " +
                                                           "     , periodo_ini " +
                                                           "     , periodo_fin " +
                                                           "     , monto_hospedaje + monto_alimentos + monto_hospedaje + monto_otros as importe " +
                                                           "from dt_anticipo " +
                                                           "	left join ms_anticipo on dt_anticipo.id_ms_anticipo = ms_anticipo.id_ms_anticipo " +
                                                           "where dt_anticipo.id_ms_comp = @id_ms_comp ", ConexionBD)
                sdaAnticipo.SelectCommand.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaAnticipo.Fill(dsAnticipo)
                .gvAnticipos.DataBind()
                ConexionBD.Close()
                sdaAnticipo.Dispose()
                dsAnticipo.Dispose()

                'Conceptos
                Dim sdaConcepto As New SqlDataAdapter
                Dim dsConcepto As New DataSet
                .gvConceptos.DataSource = dsConcepto
                sdaConcepto.SelectCommand = New SqlCommand("select fecha_realizo " +
                                                           "     , case dt_comp.tipo when 'F' then 'F' else null end as Factura " +
                                                           "     , case dt_comp.tipo when 'F' then 'http://148.223.153.43/Facturas/' + dt_factura.rfc_receptor + '/' + format(datepart(year, dt_factura.fecha_emision), '00') + '/' + upper(format(dt_factura.fecha_emision, 'MMMM', 'es-es')) + '/' + dt_factura.movimiento + '/' + dt_factura.tipo + '/' + dt_factura.uuid + '.pdf' else null end as path " +
                                                           "     , case dt_comp.tipo when 'T' then 'V' else null end as Tabulador " +
                                                           "     , nombre_concepto " +
                                                           "     , no_personas " +
                                                           "     , no_dias " +
                                                           "     , monto_subtotal " +
                                                           "     , monto_iva " +
                                                           "     , monto_total " +
                                                           "     , rfc " +
                                                           "     , proveedor " +
                                                           "     , no_factura " +
                                                           "     , origen_destino " +
                                                           "     , vehiculo " +
                                                           "     , obs " +
                                                           "     , dt_factura.id_dt_factura " +
                                                           "from dt_comp " +
                                                           "  left join dt_factura on dt_comp.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                                           "where id_ms_comp = @id_ms_comp " +
                                                           "order by id_dt_comp ", ConexionBD)
                sdaConcepto.SelectCommand.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                .gvConceptos.Columns(0).Visible = True
                .gvConceptos.Columns(1).Visible = True
                ConexionBD.Open()
                sdaConcepto.Fill(dsConcepto)
                .gvConceptos.DataBind()
                ConexionBD.Close()
                .gvConceptos.Columns(0).Visible = False
                .gvConceptos.Columns(1).Visible = False
                sdaConcepto.Dispose()
                dsConcepto.Dispose()

                'Plantilla
                Dim sdaPlantilla As New SqlDataAdapter
                Dim dsPlantilla As New DataSet
                .gvPlantilla.DataSource = dsPlantilla
                sdaPlantilla.SelectCommand = New SqlCommand("select 'Cuenta' as Tipo " +
                                                            "     , case tipo when 'F' " +
                                                            "	     then (select cuenta from cg_concepto_comp where cg_concepto_comp.id_concepto_comp = dt_comp.id_concepto) " +
                                                            "		 else (select cuenta from cg_concepto where cg_concepto.id_concepto = dt_comp.id_concepto) " +
                                                            "	   end as [Num. Cuenta] " +
                                                            "	 , case " +
                                                            "	     when centro_costo is not null then (select max(codigo) " +
                                                            "											 from bd_Empleado.dbo.cg_cc as cgCC " +
                                                            "											   left join bd_Empleado.dbo.cg_empresa cgEmpresa on cgCC.id_empresa = cgEmpresa.id_empresa " +
                                                            "											 where cgEmpresa.nombre = ms_comp.empresa " +
                                                            "											   and cgCC.nombre = ms_comp.centro_costo) " +
                                                            "		 else NULL " +
                                                            "	   end as CC " +
                                                            "	 , case " +
                                                            "	     when division is not null then (select max(codigo) " +
                                                            "										 from bd_Empleado.dbo.cg_div as cgDIV " +
                                                            "										   left join bd_Empleado.dbo.cg_empresa cgEmpresa on cgDIV.id_empresa = cgEmpresa.id_empresa " +
                                                            "										 where cgEmpresa.nombre = ms_comp.empresa " +
                                                            "										   and cgDIV.nombre = ms_comp.division) " +
                                                            "		 else NULL " +
                                                            "	   end as DIV " +
                                                            "	 , case " +
                                                            "	     when division is not null then (select max(zn) " +
                                                            "										 from bd_Empleado.dbo.cg_div as cgDIV " +
                                                            "										   left join bd_Empleado.dbo.cg_empresa cgEmpresa on cgDIV.id_empresa = cgEmpresa.id_empresa " +
                                                            "										 where cgEmpresa.nombre = ms_comp.empresa " +
                                                            "										   and cgDIV.nombre = ms_comp.division) " +
                                                            "		 else NULL " +
                                                            "	   end as ZN " +
                                                            "	 , case tipo " +
                                                            "	     when 'F' then sum(dt_comp_linea.importe) " +
                                                            "		 else dt_comp.monto_subtotal " +
                                                            "	   end as Subtotal " +
                                                            "	 , case tipo " +
                                                            "	     when 'F' then -1 * sum(dt_comp_linea.descuento) " +
                                                            "		 else NULL " +
                                                            "	   end as Descuento " +
                                                            "     , case tipo " +
                                                            "	     when 'F' then sum(dt_comp_linea.importe) + (-1 * sum(dt_comp_linea.descuento)) " +
                                                            "		 else dt_comp.monto_subtotal " +
                                                            "	   end as Diferencia " +
                                                            "	 , dt_comp_linea.tasa_iva as [Tasa IVA] " +
                                                            "	 , case tipo " +
                                                            "	     when 'F' then sum(dt_comp_linea.iva) " +
                                                            "		 else NULL " +
                                                            "	   end as [IVA Importe] " +
                                                            "	 , case tipo " +
                                                            "	     when 'F' then no_factura + ' ' + proveedor " +
                                                            "		 else 'Vale Azul' " +
                                                            "	   end as Descripcion " +
                                                            "from ms_comp " +
                                                            "  left join dt_comp on ms_comp.id_ms_comp = dt_comp.id_ms_comp " +
                                                            "  left join dt_comp_linea on dt_comp.id_dt_comp = dt_comp_linea.id_dt_comp " +
                                                            "where ms_comp.id_ms_comp = @idMsComp " +
                                                            "group by dt_comp.id_dt_comp, ms_comp.empresa, ms_comp.centro_costo, ms_comp.division, tasa_iva, tipo, no_factura + ' ' + proveedor, dt_comp.id_concepto, dt_comp.nombre_concepto, dt_comp.monto_subtotal " +
                                                            "union all " +
                                                            "select 'Cuenta' as Tipo " +
                                                            "     , case when (select abreviatura  " +
                                                            "                  from ms_comp " +
                                                            "                    inner join cg_tipoGasto on ms_comp.tipo_gasto = cg_tipoGasto.nombre_gasto " +
                                                            "                    inner join bd_Empleado.dbo.cg_empresa cgEmpr on ms_comp.empresa = cgEmpr.nombre and cg_tipoGasto.id_empresa = cgEmpr.id_empresa " +
                                                            "                  where id_ms_comp = @idMsComp) = 'GOI' " +
                                                            "              then '6100-0173' " +
                                                            "			else '6101-0173' " +
                                                            "       end as [Num. Cuenta] " +
                                                            "	 , case " +
                                                            "	     when centro_costo is not null then (select max(codigo) " +
                                                            "											 from bd_Empleado.dbo.cg_cc as cgCC " +
                                                            "											   left join bd_Empleado.dbo.cg_empresa cgEmpresa on cgCC.id_empresa = cgEmpresa.id_empresa " +
                                                            "											 where cgEmpresa.nombre = ms_comp.empresa " +
                                                            "											   and cgCC.nombre = ms_comp.centro_costo) " +
                                                            "		 else NULL " +
                                                            "	   end as CC " +
                                                            "	 , case " +
                                                            "	     when division is not null then (select max(codigo) " +
                                                            "										 from bd_Empleado.dbo.cg_div as cgDIV " +
                                                            "										   left join bd_Empleado.dbo.cg_empresa cgEmpresa on cgDIV.id_empresa = cgEmpresa.id_empresa " +
                                                            "										 where cgEmpresa.nombre = ms_comp.empresa " +
                                                            "										   and cgDIV.nombre = ms_comp.division) " +
                                                            "		 else NULL " +
                                                            "	   end as DIV " +
                                                            "	 , case " +
                                                            "	     when division is not null then (select max(zn) " +
                                                            "										 from bd_Empleado.dbo.cg_div as cgDIV " +
                                                            "										   left join bd_Empleado.dbo.cg_empresa cgEmpresa on cgDIV.id_empresa = cgEmpresa.id_empresa " +
                                                            "										 where cgEmpresa.nombre = ms_comp.empresa " +
                                                            "										   and cgDIV.nombre = ms_comp.division) " +
                                                            "		 else NULL " +
                                                            "	   end as ZN " +
                                                            "	 , sum(dt_comp_linea.ieps) as Subtotal " +
                                                            "	 , NULL as Descuento " +
                                                            "	 , sum(dt_comp_linea.ieps) as Diferencia " +
                                                            "	 , NULL as [Tasa IVA] " +
                                                            "	 , NULL as [IVA Importe] " +
                                                            "	 , 'IEPS' as Descripcion " +
                                                            "from ms_comp " +
                                                            "  left join dt_comp on ms_comp.id_ms_comp = dt_comp.id_ms_comp " +
                                                            "  left join dt_comp_linea on dt_comp.id_dt_comp = dt_comp_linea.id_dt_comp " +
                                                            "where ms_comp.id_ms_comp = @idMsComp and tipo = 'F' and dt_comp_linea.ieps > 0 " +
                                                            "group by dt_comp.id_ms_comp, ms_comp.empresa, ms_comp.centro_costo, ms_comp.division " +
                                                            "union all " +
                                                            "select 'Cuenta' as Tipo " +
                                                            "     , case when (select abreviatura  " +
                                                            "                  from ms_comp " +
                                                            "                    inner join cg_tipoGasto on ms_comp.tipo_gasto = cg_tipoGasto.nombre_gasto " +
                                                            "                    inner join bd_Empleado.dbo.cg_empresa cgEmpr on ms_comp.empresa = cgEmpr.nombre and cg_tipoGasto.id_empresa = cgEmpr.id_empresa " +
                                                            "                  where id_ms_comp = @idMsComp) = 'GOI' " +
                                                            "              then '6100-0168' " +
                                                            "			else '6101-0168' " +
                                                            "       end as [Num. Cuenta] " +
                                                            "	 , case " +
                                                            "	     when centro_costo is not null then (select max(codigo) " +
                                                            "											 from bd_Empleado.dbo.cg_cc as cgCC " +
                                                            "											   left join bd_Empleado.dbo.cg_empresa cgEmpresa on cgCC.id_empresa = cgEmpresa.id_empresa " +
                                                            "											 where cgEmpresa.nombre = ms_comp.empresa " +
                                                            "											   and cgCC.nombre = ms_comp.centro_costo) " +
                                                            "		 else NULL " +
                                                            "	   end as CC " +
                                                            "	 , case " +
                                                            "	     when division is not null then (select max(codigo) " +
                                                            "										 from bd_Empleado.dbo.cg_div as cgDIV " +
                                                            "										   left join bd_Empleado.dbo.cg_empresa cgEmpresa on cgDIV.id_empresa = cgEmpresa.id_empresa " +
                                                            "										 where cgEmpresa.nombre = ms_comp.empresa " +
                                                            "										   and cgDIV.nombre = ms_comp.division) " +
                                                            "		 else NULL " +
                                                            "	   end as DIV " +
                                                            "	 , case " +
                                                            "	     when division is not null then (select max(zn) " +
                                                            "										 from bd_Empleado.dbo.cg_div as cgDIV " +
                                                            "										   left join bd_Empleado.dbo.cg_empresa cgEmpresa on cgDIV.id_empresa = cgEmpresa.id_empresa " +
                                                            "										 where cgEmpresa.nombre = ms_comp.empresa " +
                                                            "										   and cgDIV.nombre = ms_comp.division) " +
                                                            "		 else NULL " +
                                                            "	   end as ZN " +
                                                            "	 , sum(monto_total - (monto_subtotal + monto_iva)) as Subtotal " +
                                                            "	 , NULL as Descuento " +
                                                            "	 , sum(monto_total - (monto_subtotal + monto_iva)) as Diferencia " +
                                                            "	 , NULL as [Tasa IVA] " +
                                                            "	 , NULL as [IVA Importe] " +
                                                            "	 , 'Impuestos Locales' as Descripcion " +
                                                            "from ms_comp " +
                                                            "  left join dt_comp on ms_comp.id_ms_comp = dt_comp.id_ms_comp " +
                                                            "where ms_comp.id_ms_comp = @idMsComp and tipo = 'F' " +
                                                            "  and monto_total > monto_subtotal + monto_iva " +
                                                            "group by dt_comp.id_ms_comp, ms_comp.empresa, ms_comp.centro_costo, ms_comp.division, monto_total ", ConexionBD)
                sdaPlantilla.SelectCommand.Parameters.AddWithValue("@idMsComp", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaPlantilla.Fill(dsPlantilla)
                .gvPlantilla.DataBind()
                ConexionBD.Close()
                sdaPlantilla.Dispose()
                dsPlantilla.Dispose()

                pintarTabla(.gvPlantilla)

                'Totales
                Dim sdaTot As New SqlDataAdapter
                Dim dsTot As New DataSet
                query = "select (select isnull(sum(monto_hospedaje) + sum(monto_alimentos) + sum(monto_casetas) + sum(monto_otros), 0) as monto_ant " +
                        "        from dt_anticipo " +
                        "            left join ms_anticipo on dt_anticipo.id_ms_anticipo = ms_anticipo.id_ms_anticipo " +
                        "        where dt_anticipo.id_ms_comp = @id_ms_comp) as anticipo_imp " +
                        "     , (select sum(monto_total) as monto_comp " +
                        "        from dt_comp " +
                        "        where dt_comp.id_ms_comp = @id_ms_comp) as comp_imp "
                sdaTot.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaTot.SelectCommand.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaTot.Fill(dsTot)
                ConexionBD.Close()
                .wceTotalA.Value = dsTot.Tables(0).Rows(0).Item("anticipo_imp").ToString()
                .wceTotalC.Value = dsTot.Tables(0).Rows(0).Item("comp_imp").ToString()
                .wceTotalS.Value = .wceTotalA.Value - .wceTotalC.Value
                sdaTot.Dispose()
                dsTot.Dispose()

                .lblTotalA.Text = .wceTotalA.Text
                .lblTotalC.Text = .wceTotalC.Text
                .lblTotalS.Text = .wceTotalS.Text

                .pnlDetalle.Visible = True

                .rblTabla.SelectedIndex = 1
                mostrarTabla()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub rblTabla_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblTabla.SelectedIndexChanged
        mostrarTabla()
    End Sub

#End Region

#Region "Nueva Búsqueda"

    Protected Sub btnNueBusProd_Click(sender As Object, e As EventArgs) Handles btnNueBusProd.Click
        Me.pnlFiltros.Visible = True
        Me.pnlRegistros.Visible = True
        Me.pnlDetalle.Visible = False
        Me.gvRegistros.SelectedIndex = -1
    End Sub

#End Region

End Class