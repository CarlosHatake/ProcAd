Public Class ConsComp
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
                            query = query + "where empleado = @autorizador or autorizador = @autorizador or id_usr_aut_dir = @idUsrDir "
                        End If
                        query = query + "order by solicito "
                        sdaSolicitante.SelectCommand = New SqlCommand(query, ConexionBD)
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            sdaSolicitante.SelectCommand.Parameters.AddWithValue("@autorizador", ._txtEmpleado.Text)
                            sdaSolicitante.SelectCommand.Parameters.AddWithValue("@idUsrDir", Val(._txtIdUsuario.Text))
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
                            query = query + "where empleado = @autorizador or autorizador = @autorizador or id_usr_aut_dir = @idUsrDir "
                        End If
                        query = query + "order by empresa "
                        sdaEmpresa.SelectCommand = New SqlCommand(query, ConexionBD)
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            sdaEmpresa.SelectCommand.Parameters.AddWithValue("@autorizador", ._txtEmpleado.Text)
                            sdaEmpresa.SelectCommand.Parameters.AddWithValue("@idUsrDir", Val(._txtIdUsuario.Text))
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
                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Conta" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Then
                            query = query + "where empleado = @empleado "
                        End If
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            query = query + "where empleado = @autorizador or autorizador = @autorizador or id_usr_aut_dir = @idUsrDir "
                        End If
                        query = query + "order by autorizador "
                        sdaAutorizador.SelectCommand = New SqlCommand(query, ConexionBD)
                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Conta" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Then
                            sdaAutorizador.SelectCommand.Parameters.AddWithValue("@empleado", ._txtEmpleado.Text)
                        End If
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            sdaAutorizador.SelectCommand.Parameters.AddWithValue("@autorizador", ._txtEmpleado.Text)
                            sdaAutorizador.SelectCommand.Parameters.AddWithValue("@idUsrDir", Val(._txtIdUsuario.Text))
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
                        'If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Conta" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Then
                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Conta" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Then
                            .pnlSolicitó.Visible = False
                        Else
                            If ._txtIdUsuario.Text = 14113 Then
                                .pnlSolicitó.Visible = True
                            End If

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


                        .lbl_ObsAut.Visible = False
                        .txtObsAut.Visible = False

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
            .cbPagoEfec.Checked = False
            .pnlPagoEfec.Visible = False
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

    Public Sub actualizarUnidades()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .gvUnidad.DataSource = dsCatalogo
                'Habilitar columna para actualización
                .gvUnidad.Columns(0).Visible = True
                'Catálogo de Unidades agregados
                sdaCatalogo.SelectCommand = New SqlCommand("select id_dt_unidad " +
                                                           "     , empresa " +
                                                           "     , no_economico " +
                                                           "     , status " +
                                                           "     , tipo " +
                                                           "     , no_serie " +
                                                           "     , modelo " +
                                                           "     , placas " +
                                                           "     , div " +
                                                           "     , division " +
                                                           "     , zn " +
                                                           "     , zona " +
                                                           "from dt_unidad " +
                                                           "where id_ms_comp = @id_ms_comp ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvUnidad.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvUnidad.SelectedIndex = -1
                'Inhabilitar columna para vista
                .gvUnidad.Columns(0).Visible = False
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

    Protected Sub cbPagoEfec_CheckedChanged(sender As Object, e As EventArgs) Handles cbPagoEfec.CheckedChanged
        vista(Me.pnlPagoEfec, Me.cbPagoEfec.Checked)
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
                        "     , Empresa " +
                        "     , empleado " +
                        "     , autorizador " +
                        "     , director " +
                        "     , fecha_solicita " +
                        "     , fecha_autoriza " +
                        "     , case when autorizador = director then fecha_autoriza else fecha_aut_dir end as fecha_aut_dir " +
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
                        "     , case ms_comp.status " +
                        "         when 'P' then 'Pendiente de Autorización' " +
                        "         when 'A' then 'Autorizado' " +
                        "         when 'ZA' then 'Comprobación No Autorizada' " +
                        "         when 'ZD' then 'Comprobación No Autorizada por Director' " +
                        "         when 'ZC' then 'Comprobación Cancelada' " +
                        "         when 'ZP' then 'Comprobación Cancelada' " +
                        "         when 'ZU' then 'Comprobación Cancelada por el Usuario' " +
                        "         when 'R' then 'Comprobación Registrada' " +
                        "       end as estatus " +
                        "     , cg_actividad_inst.nombre_actividad " +
                        "     , case pago_efectivo when 'S' then 'Sí' when 'N' then 'No' else null end as [Pago Efectivo] " +
                        "     , fecha_efectivo as [Fecha Efectivo] " +
                        "from ms_comp " +
                        "  left join ms_instancia on ms_comp.id_ms_comp = ms_instancia.id_ms_sol and ms_instancia.tipo = 'C' " +
                        "  left join cg_actividad_inst on ms_instancia.id_actividad = cg_actividad_inst.id_actividad " +
                        "where 1 =1  "

                If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                    query = query + "and (empleado = @autorizadorU or autorizador = @autorizadorU or id_usr_aut_dir = @idUsrDir) "
                End If

                If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Conta" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Or .cbSolicitante.Checked = True Then
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
                If .cbPagoEfec.Checked = True Then
                    query = query + "  and ms_comp.pago_efectivo = @pago_efectivo "
                End If
                If .cbNoComp.Checked = True Then
                    query = query + "  and id_ms_comp = @id_ms_comp "
                End If
                If .cbStatus.Checked = True Then
                    'query = query + "  and status in (" + .ddlStatus.SelectedValue + ") "
                    query = query + "  and ms_comp.status in (" + .ddlStatus.SelectedValue + ") "
                End If
                query = query + "order by id_ms_comp "

                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)

                If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@autorizadorU", ._txtEmpleado.Text)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@idUsrDir", Val(._txtIdUsuario.Text))
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
                If .cbPagoEfec.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@pago_efectivo", .ddlPagoEfec.SelectedValue)
                End If
                If .cbNoComp.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_ms_comp", .txtNoComp.Text.Trim)
                End If

                .gvRegistrosT.Visible = True
                .gvRegistros.DataSource = dsConsulta
                .gvRegistrosT.DataSource = dsConsulta
                ConexionBD.Open()
                sdaConsulta.Fill(dsConsulta)
                ConexionBD.Close()
                .gvRegistros.DataBind()
                .gvRegistrosT.DataBind()
                sdaConsulta.Dispose()
                dsConsulta.Dispose()
                .gvRegistrosT.Visible = False
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
                        "     , obs_autorizador " +
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
                        "     , aut_dir " +
                        "     , director " +
                        "     , (select count(*) from dt_unidad where dt_unidad.id_ms_comp = ms_comp.id_ms_comp) as unidades " +
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
                If dsSol.Tables(0).Rows(0).Item("aut_dir").ToString() = "S" Then
                    .lbl_Director.Visible = True
                    .lblDirector.Visible = True
                    .lblDirector.Text = dsSol.Tables(0).Rows(0).Item("director").ToString()
                Else
                    .lbl_Director.Visible = False
                    .lblDirector.Visible = False
                End If
                .txtJust.Text = dsSol.Tables(0).Rows(0).Item("justificacion").ToString()
                If dsSol.Tables(0).Rows(0).Item("obs_autorizador").ToString() IsNot "" Then
                    .lbl_ObsAut.Visible = True
                    .txtObsAut.Visible = True
                    .txtObsAut.Text = dsSol.Tables(0).Rows(0).Item("obs_autorizador").ToString()

                End If

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
                If Val(dsSol.Tables(0).Rows(0).Item("unidades").ToString()) > 0 Then
                    .pnlUnidad.Visible = True
                    actualizarUnidades()
                Else
                    .pnlUnidad.Visible = False
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
                                                           "     , monto_hospedaje + monto_alimentos + monto_casetas + monto_otros as importe " +
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

                ' Habilitamos los estilos visuales
                'For Each Fila As GridView In .gvConceptos.Rows.Cast(Of DataGridColumn)

                '    If Fila.Cells("nombredecolumna").Value.ToString() Then

                '    End If
                'Next


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

                'Evidencia'
                Dim sdaEvi As New SqlDataAdapter
                Dim dsEvi As New DataSet
                gvEvidencias.DataSource = dsEvi

                Dim queryE As String
                queryE = "SELECT id_dt_archivo_comp,  archivo as nombre, 'http://148.223.153.43/ProcAd/Evidencias Comp/' + archivo as ruta FROM dt_archivo_comp where id_ms_comp = @id_ms_comp and status = 'A' "
                sdaEvi.SelectCommand = New SqlCommand(queryE, ConexionBD)
                sdaEvi.SelectCommand.Parameters.AddWithValue("@id_ms_comp", Val(lblFolio.Text))
                ConexionBD.Open()
                sdaEvi.Fill(dsEvi)
                gvEvidencias.DataBind()
                gvEvidencias.Columns(0).Visible = False
                ConexionBD.Close()

                If hlEvidencia.Visible = False And gvEvidencias.Rows.Count = 0 Then
                    lblEvidenciaN.Visible = True
                Else
                    lblEvidenciaN.Visible = False
                End If

                .pnlDetalle.Visible = True
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
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