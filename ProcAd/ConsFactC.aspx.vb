Public Class ConsFactC
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
                                "from ms_factura "
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
                                "from ms_factura "
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
                                "from ms_factura "
                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Conta" Then
                            query = query + "where empleado = @empleado "
                        End If
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            query = query + "where empleado = @autorizador or autorizador = @autorizador "
                        End If
                        query = query + "order by autorizador "
                        sdaAutorizador.SelectCommand = New SqlCommand(query, ConexionBD)
                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Conta" Then
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

                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Conta" Then
                            .pnlSolicitó.Visible = False
                        Else
                            .pnlSolicitó.Visible = True
                            .cbSolicitante.Checked = False
                            .pnlSolicitante.Visible = False
                            .cbEmpresa.Checked = False
                            .pnlEmpresa.Visible = False
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
            .cbProveedor.Checked = False
            .pnlProveedor.Visible = False
            .cbNoFact.Checked = False
            .pnlNoFact.Visible = False
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

    Protected Sub cbProveedor_CheckedChanged(sender As Object, e As EventArgs) Handles cbProveedor.CheckedChanged
        vista(Me.pnlProveedor, Me.cbProveedor.Checked)
        If Me.cbProveedor.Checked = True Then
            Me.txtProveedor.Text = ""
        End If
    End Sub

    Protected Sub cbNoFact_CheckedChanged(sender As Object, e As EventArgs) Handles cbNoFact.CheckedChanged
        vista(Me.pnlNoFact, Me.cbNoFact.Checked)
        If Me.cbNoFact.Checked = True Then
            Me.txtNoFact.Text = ""
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

                query = "select ms_factura.id_ms_factura as [No. Solicitud] " + _
                        "     , dt_factura.razon_emisor as [Razón Social] " + _
                        "	  , ms_factura.descrip as [Descripción] " + _
                        "	  , ms_factura.fecha_solicita as [Fecha Solicitud usuario] " + _
                        "	  , ms_factura.fecha_asigna as [Fecha Asignación Cuenta] " + _
                        "	  , dt_factura.fecha_emision as [Fecha Emisión] " + _
                        "     , dt_factura.serie + dt_factura.folio as [Folio Externo] " + _
                        "	  , CFDI " + _
                        "     , dt_factura.subtotal " + _
                        "     , case when (importe_tot - (subtotal - descuentos - tot_imp_ret + tot_imp_tras)) < = 0 then null else importe_tot - (subtotal - descuentos + tot_imp_tras - tot_imp_ret) end as [Impuestos Locales] " + _
                        "	  , importe_tot as [Importe Total] " + _
                        "from ms_factura " + _
                        "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " + _
                        "where ms_factura.status in ('R', 'RN') "

                If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                    query = query + "and (empleado = @autorizadorU or autorizador = @autorizadorU) "
                End If

                If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Conta" Or .cbSolicitante.Checked = True Then
                    query = query + "  and id_usr_solicita = @id_usr_solicita "
                End If
                If .cbEmpresa.Checked = True Then
                    query = query + "  and ms_factura.empresa = @empresa "
                End If
                If .cbFechaC.Checked = True Then
                    query = query + "  and (fecha_solicita between @fechaIni and @fechaFin) "
                End If
                If .cbAutorizador.Checked = True Then
                    query = query + "  and autorizador = @autorizador "
                End If
                If .cbProveedor.Checked = True Then
                    query = query + "  and dt_factura.razon_emisor like '%' + @proveedor + '%' "
                End If
                If .cbNoFact.Checked = True Then
                    query = query + "  and ms_factura.id_ms_factura = @id_ms_factura "
                End If
                query = query + "order by ms_factura.id_ms_factura "

                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)

                If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@autorizadorU", ._txtEmpleado.Text)
                End If

                If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Conta" Then
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
                If .cbProveedor.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@proveedor", .txtProveedor.Text.Trim)
                End If
                If .cbNoFact.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_ms_factura", .txtNoFact.Text.Trim)
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
                Dim sdaSol As New SqlDataAdapter
                Dim dsSol As New DataSet
                Dim query As String
                query = "select dt_factura.razon_emisor " + _
                        "	  , ms_factura.descrip " + _
                        "	  , ms_factura.fecha_asigna " + _
                        "	  , dt_factura.fecha_emision " + _
                        "     , dt_factura.serie + dt_factura.folio as folio " + _
                        "	  , CFDI " + _
                        "     , dt_factura.subtotal " + _
                        "	  , importe_tot as [Importe Total] " + _
                        "     , 'http://148.223.153.43/Facturas/' + dt_factura.rfc_receptor + '/' + format(datepart(year, dt_factura.fecha_emision), '00') + '/' + upper(format(dt_factura.fecha_emision, 'MMMM', 'es-es')) + '/' + dt_factura.movimiento + '/' + dt_factura.tipo + '/' + dt_factura.uuid + '.pdf' as path " + _
                        "     , case when (importe_tot - (subtotal - descuentos - tot_imp_ret + tot_imp_tras)) < = 0 then -1 else (importe_tot - (subtotal - descuentos - tot_imp_ret + tot_imp_tras)) * (porcent/100) end as [Impuestos Locales] " + _
                        "     , isnull(tabla_comp, 'XX') as tabla_comp " + _
                        "     , isnull(adjunto_opcional, 'XX') as adjunto_opcional " + _
                        "from ms_factura " + _
                        "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " + _
                        "  left join dt_partida on ms_factura.id_ms_factura = dt_partida.id_ms_factura " + _
                        "where ms_factura.id_ms_factura = @id_ms_factura "
                sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaSol.SelectCommand.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaSol.Fill(dsSol)
                ConexionBD.Close()
                .lblRazonS.Text = dsSol.Tables(0).Rows(0).Item("razon_emisor").ToString()
                .lblDescrip.Text = dsSol.Tables(0).Rows(0).Item("descrip").ToString()
                .lblFechaAsig.Text = dsSol.Tables(0).Rows(0).Item("fecha_asigna").ToString()
                .lblFechaEmi.Text = dsSol.Tables(0).Rows(0).Item("fecha_emision").ToString()
                .lblFolioE.Text = dsSol.Tables(0).Rows(0).Item("folio").ToString()
                .lblCFDI.Text = dsSol.Tables(0).Rows(0).Item("CFDI").ToString()
                .lblSubtotal.Text = .gvRegistros.SelectedRow.Cells(9).Text
                .lblImpLoc.Text = .gvRegistros.SelectedRow.Cells(10).Text
                .lblTotal.Text = .gvRegistros.SelectedRow.Cells(11).Text
                .hlPDF.NavigateUrl = dsSol.Tables(0).Rows(0).Item("path").ToString()
                If Val(dsSol.Tables(0).Rows(0).Item("Impuestos Locales").ToString()) < 0 Then
                    .lbl_ImpLoc.Visible = False
                    .lblImpLoc.Visible = False
                Else
                    .lbl_ImpLoc.Visible = True
                    .lblImpLoc.Visible = True
                End If
                ''Tabla Comparativa
                'If dsSol.Tables(0).Rows(0).Item("tabla_comp").ToString() = "XX" Then
                '    .hlTablaComp.Visible = False
                '    .lblTablaComp.Visible = True
                'Else
                '    .hlTablaComp.Visible = True
                '    .lblTablaComp.Visible = False
                '    .hlTablaComp.Text = dsSol.Tables(0).Rows(0).Item("tabla_comp").ToString()
                '    '.hlTablaComp.NavigateUrl = "http://localhost/ProcAd - Adjuntos/" + .lblFolio.Text + "TabCon-" + dsSol.Tables(0).Rows(0).Item("tabla_comp").ToString()
                '    .hlTablaComp.NavigateUrl = "http://148.223.153.43/ProcAd - Adjuntos/" + .lblFolio.Text + "TabCon-" + dsSol.Tables(0).Rows(0).Item("tabla_comp").ToString()
                'End If
                ''Adjunto Opcional
                'If dsSol.Tables(0).Rows(0).Item("adjunto_opcional").ToString() = "XX" Then
                '    .hlAdjOpc.Visible = False
                '    .lblAdjOpc.Visible = True
                'Else
                '    .hlAdjOpc.Visible = True
                '    .lblAdjOpc.Visible = False
                '    .hlAdjOpc.Text = dsSol.Tables(0).Rows(0).Item("adjunto_opcional").ToString()
                '    '.hlAdjOpc.NavigateUrl = "http://localhost/ProcAd - Adjuntos/" + .lblFolio.Text + "AdjOpc-" + dsSol.Tables(0).Rows(0).Item("adjunto_opcional").ToString()
                '    .hlAdjOpc.NavigateUrl = "http://148.223.153.43/ProcAd - Adjuntos/" + .lblFolio.Text + "AdjOpc-" + dsSol.Tables(0).Rows(0).Item("adjunto_opcional").ToString()
                'End If
                sdaSol.Dispose()
                dsSol.Dispose()

                'Llenar Grid
                Dim sdaFactura As New SqlDataAdapter
                Dim dsFactura As New DataSet
                .gvFactura.DataSource = dsFactura
                sdaFactura.SelectCommand = New SqlCommand("select cuenta_c1 + '-' +cuenta_c2 as [Cuenta Contable] " + _
                                                          "	    , dt_partida.centro_costo as [Centro de Costo] " + _
                                                          "	    , dt_partida.division as División " + _
                                                          "	    , dt_partida.zona as Zona " + _
                                                          "	    , porcent as [Porcentaje Partida] " + _
                                                          "	    , dt_factura.subtotal * (porcent/100) as [Importe Partida] " + _
                                                          "     , case when (importe_tot - (subtotal - descuentos - tot_imp_ret + tot_imp_tras)) < = 0 then null else (importe_tot - (subtotal - descuentos - tot_imp_ret + tot_imp_tras)) * (porcent/100) end as [Impuestos Locales] " + _
                                                          "	    , dt_factura.moneda as Divisa " + _
                                                          "from ms_factura " + _
                                                          "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " + _
                                                          "  left join dt_partida on ms_factura.id_ms_factura = dt_partida.id_ms_factura " + _
                                                          "where ms_factura.id_ms_factura = @idMsFactura ", ConexionBD)
                sdaFactura.SelectCommand.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaFactura.Fill(dsFactura)
                .gvFactura.DataBind()
                ConexionBD.Close()
                sdaFactura.Dispose()
                dsFactura.Dispose()
                .gvFactura.SelectedIndex = -1

                'Adjuntos
                Dim sdaArchivos As New SqlDataAdapter
                Dim dsArchivos As New DataSet
                .gvAdjuntos.DataSource = dsArchivos
                sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " + _
                                                           "     , 'http://148.223.153.43/ProcAd - Adjuntos IngFact/' + cast(id_dt_archivo as varchar(20)) + '-' + nombre as path " + _
                                                           "from dt_archivo " + _
                                                           "where id_ms_factura = @idMsFactura " + _
                                                           "  and tipo = 'A' " + _
                                                           "union " + _
                                                           "select tabla_comp as archivo " + _
                                                           "     , 'http://148.223.153.43/ProcAd - Adjuntos/' + cast(id_ms_factura as varchar(20)) + 'TabCon-' + tabla_comp as path " + _
                                                           "from ms_factura " + _
                                                           "where id_ms_factura = @idMsFactura " + _
                                                           "  and tabla_comp is not null " + _
                                                           "union " + _
                                                           "select adjunto_opcional as archivo " + _
                                                           "     , 'http://148.223.153.43/ProcAd - Adjuntos/' + cast(id_ms_factura as varchar(20)) + 'AdjOpc-' + adjunto_opcional as path " + _
                                                           "from ms_factura " + _
                                                           "where id_ms_factura = @idMsFactura " + _
                                                           "  and adjunto_opcional is not null ", ConexionBD)
                sdaArchivos.SelectCommand.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaArchivos.Fill(dsArchivos)
                .gvAdjuntos.DataBind()
                ConexionBD.Close()
                sdaArchivos.Dispose()
                dsArchivos.Dispose()
                .gvAdjuntos.SelectedIndex = -1
                If .gvAdjuntos.Rows.Count > 0 Then
                    .lbl_Adjunto.Visible = True
                Else
                    .lbl_Adjunto.Visible = False
                End If

                'Evidencias
                Dim sdaEvidencias As New SqlDataAdapter
                Dim dsEvidencias As New DataSet
                .gvEvidencias.DataSource = dsEvidencias
                'Evidencias
                sdaEvidencias.SelectCommand = New SqlCommand("select nombre as archivo " + _
                                                           "     , 'http://148.223.153.43/ProcAd - Adjuntos IngFact/' + cast(id_dt_archivo as varchar(20)) + '-' + nombre as path " + _
                                                           "from dt_archivo " + _
                                                           "where id_ms_factura = @idMsFactura " + _
                                                           "  and tipo = 'E' ", ConexionBD)
                sdaEvidencias.SelectCommand.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaEvidencias.Fill(dsEvidencias)
                .gvEvidencias.DataBind()
                ConexionBD.Close()
                sdaEvidencias.Dispose()
                dsEvidencias.Dispose()
                .gvEvidencias.SelectedIndex = -1
                If .gvEvidencias.Rows.Count > 0 Then
                    .lbl_Evidencia.Visible = True
                Else
                    .lbl_Evidencia.Visible = False
                End If

                'Activos Fijos
                Dim sdaActivos As New SqlDataAdapter
                Dim dsActivos As New DataSet
                .gvAF.DataSource = dsActivos
                sdaActivos.SelectCommand = New SqlCommand("select codigo as no_economico " + _
                                                          "     , descripcion " + _
                                                          "from dt_af " + _
                                                          "where id_ms_factura = @idMsFactura ", ConexionBD)
                sdaActivos.SelectCommand.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaActivos.Fill(dsActivos)
                .gvAF.DataBind()
                ConexionBD.Close()
                sdaActivos.Dispose()
                dsActivos.Dispose()
                If .gvAF.Rows.Count > 0 Then
                    .lbl_AF.Visible = True
                Else
                    .lbl_AF.Visible = False
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
        Me.pnlRegistros.Visible = False
        Me.pnlDetalle.Visible = False
        Me.gvRegistros.SelectedIndex = -1
    End Sub

#End Region

End Class