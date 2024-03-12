Public Class ConAntProvedor
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
                        query = "select distinct(id_usr_solicita) as id_usr_solicita, empleado_solicita as solicito " +
                                "from ms_anticipo_proveedor "
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            query = query + "where empleado_solicita = @autorizador or autorizador = @autorizador "
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
                                "from ms_anticipo_proveedor "
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            query = query + "where empleado_solicita = @autorizador or autorizador = @autorizador "
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
                                "from ms_anticipo_proveedor "
                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "Conta" Or ._txtPerfil.Text = "ContaF" Then
                            query = query + "where empleado_solicita = @empleado "
                        End If
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            query = query + "where empleado_solicita = @autorizador or autorizador = @autorizador "
                        End If
                        query = query + "order by autorizador "
                        sdaAutorizador.SelectCommand = New SqlCommand(query, ConexionBD)
                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "Conta" Or ._txtPerfil.Text = "ContaF" Then
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

                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "Conta" Or ._txtPerfil.Text = "ContaF" Then
                            .pnlFiltros.Visible = False
                        Else
                            .pnlFiltros.Visible = True
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
            .wdpFechaI.Date = Date.Now.ToLongDateString
            .wdpFechaF.Date = Date.Now.ToLongDateString
            .cbAutorizador.Checked = False
            .pnlAutorizador.Visible = False
            .cbStatus.Checked = False
            .pnlStatus.Visible = False
            .cbTipoAnt.Checked = False
            .pnlTipoAnt.Visible = False
            .cbNoAnticipo.Checked = False
            .pnlNoAnticipo.Visible = False
            'ocultar el resto de paneles principales
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

#End Region

#Region "Filtros"

    Protected Sub cbEmpresa_CheckedChanged(sender As Object, e As EventArgs) Handles cbEmpresa.CheckedChanged
        vista(Me.pnlEmpresa, Me.cbEmpresa.Checked)
    End Sub
    Protected Sub cbSolicitante_CheckedChanged(sender As Object, e As EventArgs) Handles cbSolicitante.CheckedChanged
        vista(Me.pnlSolicitante, Me.cbSolicitante.Checked)
    End Sub
    Protected Sub cbFechaC_CheckedChanged(Sender As Object, e As EventArgs) Handles cbFechaC.CheckedChanged
        vista(Me.pnlFechaC, Me.cbFechaC.Checked)
    End Sub
    Protected Sub cbAutorizador_CheckedChanged(sender As Object, e As EventArgs) Handles cbAutorizador.CheckedChanged
        vista(Me.pnlAutorizador, Me.cbAutorizador.Checked)
    End Sub
    Protected Sub cbStatus_CheckedChanged(sender As Object, e As EventArgs) Handles cbStatus.CheckedChanged
        vista(Me.pnlStatus, Me.cbStatus.Checked)
    End Sub
    Protected Sub cbTipoAnt_ChekedChanged(Sender As Object, e As EventArgs) Handles cbTipoAnt.CheckedChanged
        vista(Me.pnlTipoAnt, Me.cbTipoAnt.Checked)
    End Sub
    Protected Sub cbNoAnticipo_CheckedChanged(sender As Object, e As EventArgs) Handles cbNoAnticipo.CheckedChanged
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
                'Llenar el GridView
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaConsulta As New SqlDataAdapter
                Dim dsConsulta As New DataSet
                gvRegistros.DataSource = dsConsulta
                Dim query As String

                query = "select ms_anticipo_proveedor.id_ms_anticipo_proveedor " +
                        "     , ms_anticipo_proveedor.id_usr_solicita " +
                        "     , ms_anticipo_proveedor.empresa " +
                        "     , ms_anticipo_proveedor.id_proveedor " +
                        "     , ms_anticipo_proveedor.proveedor " +
                        "     , ms_anticipo_proveedor.justificacion " +
                        "     , ms_anticipo_proveedor.empleado_solicita  " +
                        "     , ms_anticipo_proveedor.autorizador " +
                        "     , ms_anticipo_proveedor.importe_requerido " +
                        "     , ms_anticipo_proveedor.fecha_solicita " +
                        "     , ms_anticipo_proveedor.fecha_reg_autorizador " +
                        "     , ms_anticipo_proveedor.fecha_registro_pago_cxp " +
                        "     , ms_anticipo_proveedor.fecha_prog_pago_tesoreria " +
                        ", case " +
                        " WHEN ms_anticipo_proveedor.estatus ='P' and insta.id_actividad =135 THEN 'Pendiente de autorización'" +
                        " WHEN ms_anticipo_proveedor.estatus ='A' and insta.id_actividad =136 THEN 'Pendiente de Vbo' " +
                        " WHEN ms_anticipo_proveedor.estatus ='Z' and insta.id_actividad =137 THEN 'Rechazado por Autorizador' " +
                        " WHEN ms_anticipo_proveedor.estatus ='A' and insta.id_actividad =138 THEN 'Pendiente Regis. CXP' " +
                        " WHEN ms_anticipo_proveedor.estatus ='Z' and insta.id_actividad =139 THEN 'Rechazado Regis. CXP' " +
                        " WHEN ms_anticipo_proveedor.estatus ='TR' and insta.id_actividad =140 THEN 'Tranferencia Realizada' " +
                        " WHEN ms_anticipo_proveedor.estatus ='TR' and insta.id_actividad =141 THEN 'Tranferencia Realizada' " +
                        " End As estado_anticipo  " +
                        "     , case " +
                        "         WHEN ms_comprobacion_anticipo.estatus = 'P' AND inst.id_actividad = 142 THEN 'Pendiente de autorización por autorizador'  " +
                        "         WHEN ms_comprobacion_anticipo.estatus = 'A' AND inst.id_actividad = 143 AND fecha_autoriza IS NOT NULL THEN 'Autorizado por el autorizador' " +
                        "         WHEN ms_comprobacion_anticipo.estatus = 'ZA' AND inst.id_actividad = 144 THEN 'Rechazado por el autorizador'  " +
                        "         WHEN ms_comprobacion_anticipo.estatus = 'A' AND inst.id_actividad = 143 AND fecha_autoriza_2 IS NOT NULL THEN 'Autorizado por el director' " +
                        "         WHEN ms_comprobacion_anticipo.estatus = 'ZD' AND inst.id_actividad = 144 THEN 'Rechazado por el director' " +
                        "         WHEN ms_comprobacion_anticipo.estatus = 'A' AND inst.id_actividad = 143 AND fecha_autoriza_3 IS NOT NULL THEN 'Autorizado por el C' " +
                        "         WHEN ms_comprobacion_anticipo.estatus = 'ZC' AND inst.id_actividad = 144 THEN 'Rechazado por el C' " +
                        "         WHEN ms_comprobacion_anticipo.estatus = 'AF' AND inst.id_actividad = 149 THEN 'Validado en codificacion contable'  " +
                        "         WHEN ms_comprobacion_anticipo.estatus = 'RN' AND inst.id_actividad = 150 THEN 'Comprobado' " +
                        "         WHEN ms_comprobacion_anticipo.estatus = 'Z' AND inst.id_actividad = 151 THEN 'Rechazado por cuentas por pagar' " +
                        "         ELSE '-'  " +
                        "       end as estatus " +
                        "     , case ms_anticipo_proveedor.tipo_anticipo " +
                        "       when '1' then 'Anticipo' " +
                        "       when '2' then 'Pago Anticipado' " +
                        "       when '3' then 'pago anticipado agente aduanal' " +
                        "       Else '-' end tipo_anticipo " +
                        "from ms_anticipo_proveedor " +
                        "  left join ms_comprobacion_anticipo on ms_anticipo_proveedor.id_ms_anticipo_proveedor = ms_comprobacion_anticipo.id_ms_anticipo_proveedor " +
                        "  LEFT JOIN ms_instancia insta ON ms_anticipo_proveedor.id_ms_anticipo_proveedor  = insta.id_ms_sol  AND insta.tipo = 'AP'  " +
                        "  LEFT JOIN ms_instancia inst ON ms_comprobacion_anticipo.id_ms_comprobacion_anticipo = inst.id_ms_sol AND inst.tipo = 'CAP' " +
                        "where 1 = 1 "

                If .cbSolicitante.Checked = True Then
                    query = query + "and ms_anticipo_proveedor.id_usr_solicita = @empleadosolicita "
                End If
                If .cbEmpresa.Checked = True Then
                    query = query + "and ms_anticipo_proveedor.empresa = @empresa "
                End If
                If .cbFechaC.Checked = True Then
                    query = query + "and (ms_anticipo_proveedor.fecha_solicita between @fecha and @fechaF) "
                End If
                If .cbStatus.Checked = True Then
                    query = query + "and ms_comprobacion_anticipo.estatus in(" + .ddlStatus.SelectedValue + ") "
                End If
                If .cbAutorizador.Checked = True Then
                    query = query + "and ms_anticipo_proveedor.autorizador = @autoriza "
                End If
                If .cbTipoAnt.Checked = True Then
                    query = query + "and ms_anticipo_proveedor.tipo_anticipo in(" + .ddlTipoAnt.SelectedValue + ") "
                End If
                If .cbNoAnticipo.Checked = True Then
                    query = query + "  and ms_anticipo_proveedor.id_ms_anticipo_proveedor = @NoAnticipo "
                End If

                query = query + "order by ms_anticipo_proveedor.id_ms_anticipo_proveedor "


                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)

                If .cbSolicitante.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@empleadosolicita", ddlSolicitante.SelectedValue)
                End If
                If .cbEmpresa.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@empresa", ddlEmpresa.SelectedValue)
                End If
                If .cbFechaC.Checked Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fecha", .wdpFechaI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaF", .wdpFechaF.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
                End If
                If .cbStatus.Checked Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@estado", ddlStatus.SelectedValue)
                End If
                If .cbAutorizador.Checked Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@autoriza", ddlAutorizador.SelectedValue)
                End If


                If .cbTipoAnt.Checked Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@tipo", ddlTipoAnt.SelectedValue)
                End If
                If .cbNoAnticipo.Checked Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@NoAnticipo", txtNoAnticipo.Text)
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
                Dim sdaSol As New SqlDataAdapter
                Dim dsSol As New DataSet
                Dim query As String

                query = "select ms_anticipo_proveedor.id_ms_anticipo_proveedor " +
                        "     , ms_anticipo_proveedor.id_usr_solicita " +
                        "     , ms_anticipo_proveedor.empresa " +
                        "     , ms_anticipo_proveedor.id_proveedor " +
                        "     , ms_anticipo_proveedor.proveedor " +
                        "     , ms_anticipo_proveedor.justificacion " +
                        "     , ms_anticipo_proveedor.empleado_solicita  " +
                        "     , ms_anticipo_proveedor.autorizador " +
                        "     , ms_anticipo_proveedor.importe_requerido " +
                        "     , ms_anticipo_proveedor.fecha_solicita " +
                        "     , ms_anticipo_proveedor.fecha_reg_autorizador " +
                        "     , ms_anticipo_proveedor.fecha_registro_pago_cxp " +
                        "     , ms_anticipo_proveedor.fecha_prog_pago_tesoreria " +
                        "     , case " +
                        "         WHEN ms_comprobacion_anticipo.estatus = 'P' AND inst.id_actividad = 142 THEN 'Pendiente de autorización por autorizador'  " +
                        "         WHEN ms_comprobacion_anticipo.estatus = 'A' AND inst.id_actividad = 143 AND fecha_autoriza IS NOT NULL THEN 'Autorizado por el autorizador' " +
                        "         WHEN ms_comprobacion_anticipo.estatus = 'ZA' AND inst.id_actividad = 144 THEN 'Rechazado por el autorizador'  " +
                        "         WHEN ms_comprobacion_anticipo.estatus = 'A' AND inst.id_actividad = 143 AND fecha_autoriza_2 IS NOT NULL THEN 'Autorizado por el director' " +
                        "         WHEN ms_comprobacion_anticipo.estatus = 'ZD' AND inst.id_actividad = 144 THEN 'Rechazado por el director' " +
                        "         WHEN ms_comprobacion_anticipo.estatus = 'A' AND inst.id_actividad = 143 AND fecha_autoriza_3 IS NOT NULL THEN 'Autorizado por el C' " +
                        "         WHEN ms_comprobacion_anticipo.estatus = 'ZC' AND inst.id_actividad = 144 THEN 'Rechazado por el C' " +
                        "         WHEN ms_comprobacion_anticipo.estatus = 'AF' AND inst.id_actividad = 149 THEN 'Validado en codificacion contable'  " +
                        "         WHEN ms_comprobacion_anticipo.estatus = 'RN' AND inst.id_actividad = 150 THEN 'Comprobado' " +
                        "         WHEN ms_comprobacion_anticipo.estatus = 'Z' AND inst.id_actividad = 151 THEN 'Rechazado por cuentas por pagar' " +
                        "         ELSE '-'  " +
                        "       end as estatus " +
                        "     , case ms_anticipo_proveedor.tipo_anticipo " +
                        "       when '1' then 'Anticipo' " +
                        "       when '2' then 'Pago Anticipado' " +
                        "       when '3' then 'pago anticipado agente aduanal' " +
                        "       Else '-' end tipo_anticipo " +
                        "from ms_anticipo_proveedor " +
                        "  left join ms_comprobacion_anticipo on ms_anticipo_proveedor.id_ms_anticipo_proveedor = ms_comprobacion_anticipo.id_ms_anticipo_proveedor " +
                        "  LEFT JOIN ms_instancia inst ON ms_comprobacion_anticipo.id_ms_comprobacion_anticipo = inst.id_ms_sol AND tipo = 'CAP' " +
                        " where 1 = 1  and ms_anticipo_proveedor.id_ms_anticipo_proveedor = @id_ms_anticipo_proveedor"

                sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaSol.SelectCommand.Parameters.AddWithValue("@id_ms_anticipo_proveedor", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaSol.Fill(dsSol)
                ConexionBD.Close()

                .lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("empleado_solicita").ToString()
                .lblNoProveedor.Text = dsSol.Tables(0).Rows(0).Item("id_proveedor").ToString()
                .lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                .lblAutorizador.Text = dsSol.Tables(0).Rows(0).Item("autorizador").ToString()
                .lblFechaS.Text = dsSol.Tables(0).Rows(0).Item("fecha_solicita").ToString()
                .lblProveedor.Text = dsSol.Tables(0).Rows(0).Item("proveedor").ToString()
                .txtAct.Text = dsSol.Tables(0).Rows(0).Item("justificacion").ToString()


                'Adjuntos
                'Dim sdaArchivos As New SqlDataAdapter
                'Dim dsArchivos As New DataSet
                '.gvAdjuntos.DataSource = dsArchivos
                ''Ruta Servidor Prueba 172.16.18.239'
                ''Ruta Servidor Bueno 148.223.153.43'
                'sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " +
                '                                           "     , 'http://148.223.153.43/ProcAd - Adjuntos AntProv/'  + cast(id_dt_archivo_adj_anticipo as varchar(20)) + '-' + nombre as path " +
                '                                           "from dt_archivo_adj_anticipo " +
                '                                           "where id_ms_anticipo_proveedor = @idMsNegSer ", ConexionBD)
                Dim sdaArchivos As New SqlDataAdapter
                Dim dsArchivos As New DataSet
                .gvAdjuntos.DataSource = dsArchivos
                'Ruta Servidor Prueba 172.16.18.239'
                'Ruta Servidor Bueno 148.223.153.43'
                sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " +
                                                           "     , 'http://148.223.153.43/ProcAd/Adjuntos AntProv/' + nombre as path " +
                                                           "from dt_archivo_adj_anticipo " +
                                                           "where id_ms_anticipo_proveedor = @idMsNegSer ", ConexionBD)
                sdaArchivos.SelectCommand.Parameters.AddWithValue("@idMsNegSer", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaArchivos.Fill(dsArchivos)
                .gvAdjuntos.DataBind()
                ConexionBD.Close()
                sdaArchivos.Dispose()
                dsArchivos.Dispose()
                .gvAdjuntos.SelectedIndex = -1
                If .gvAdjuntos.Rows.Count = 0 Then
                    .lbl_Adjunto.Visible = False
                Else
                    .lbl_Adjunto.Visible = True
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