Public Class ConsFactI
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
            .pnlFechaE.Visible = False
            .wdpFechaEI.Date = Date.Now.ToShortDateString
            .wdpFechaEF.Date = Date.Now.ToShortDateString
            .cbProveedor.Checked = False
            .pnlProveedor.Visible = False
            .cbAutorizador.Checked = False
            .pnlAutorizador.Visible = False
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

    Protected Sub cbFechaE_CheckedChanged(sender As Object, e As EventArgs) Handles cbFechaE.CheckedChanged
        vista(Me.pnlFechaE, Me.cbFechaE.Checked)
    End Sub

    Protected Sub cbProveedor_CheckedChanged(sender As Object, e As EventArgs) Handles cbProveedor.CheckedChanged
        vista(Me.pnlProveedor, Me.cbProveedor.Checked)
        If Me.cbProveedor.Checked = True Then
            Me.txtProveedor.Text = ""
        End If
    End Sub

    Protected Sub cbAutorizador_CheckedChanged(sender As Object, e As EventArgs) Handles cbAutorizador.CheckedChanged
        vista(Me.pnlAutorizador, Me.cbAutorizador.Checked)
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
                        "where ms_factura.status = 'R' "

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
                If .cbFechaE.Checked = True Then
                    query = query + "  and (dt_factura.fecha_emision between @fechaEIni and @fechaEFin) "
                End If
                If .cbProveedor.Checked = True Then
                    query = query + "  and dt_factura.razon_emisor like '%' + @proveedor + '%' "
                End If
                If .cbAutorizador.Checked = True Then
                    query = query + "  and autorizador = @autorizador "
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
                If .cbFechaE.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaEIni", .wdpFechaEI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaEFin", .wdpFechaEF.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
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
                query = "select dt_factura.razon_emisor " +
                        "     , isnull(dt_factura.regimen_fiscal, '') as regimen_fiscal " +
                        "	  , ms_factura.descrip " +
                        "	  , ms_factura.fecha_asigna " +
                        "	  , dt_factura.fecha_emision " +
                        "     , dt_factura.serie + dt_factura.folio as folio " +
                        "	  , CFDI " +
                        "     , dt_factura.subtotal " +
                        "	  , importe_tot as [Importe Total] " +
                        "     , 'http://148.223.153.43/Facturas/' + dt_factura.rfc_receptor + '/' + format(datepart(year, dt_factura.fecha_emision), '00') + '/' + upper(format(dt_factura.fecha_emision, 'MMMM', 'es-es')) + '/' + dt_factura.movimiento + '/' + dt_factura.tipo + '/' + dt_factura.uuid + '.pdf' as path " +
                        "     , case when (importe_tot - (subtotal - descuentos - tot_imp_ret + tot_imp_tras)) < = 0 then -1 else (importe_tot - (subtotal - descuentos - tot_imp_ret + tot_imp_tras)) * (porcent/100) end as [Impuestos Locales] " +
                        "     , isnull(tabla_comp, 'XX') as tabla_comp " +
                        "     , isnull(adjunto_opcional, 'XX') as adjunto_opcional " +
                        "     , case when contrato_NAV_reg = 'S' or contrato_NAV_alta = 'S' then 'S' else 'N' end as contrato_NAV " +
                        "     , case when contrato_NAV_alta = 'S' then isnull((select no_contrato_NAV from ms_contrato where ms_contrato.id_ms_factura = ms_factura.id_ms_factura), 'XX') else isnull(no_contrato_NAV, 'XX') end as no_contrato_NAV " +
                        "     , dt_factura.tipo " +
                        "     , ms_factura.especificaciones " +
                        "from ms_factura " +
                        "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                        "  left join dt_partida on ms_factura.id_ms_factura = dt_partida.id_ms_factura " +
                        "where ms_factura.id_ms_factura = @id_ms_factura "
                sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaSol.SelectCommand.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaSol.Fill(dsSol)
                ConexionBD.Close()
                .lblRazonS.Text = dsSol.Tables(0).Rows(0).Item("razon_emisor").ToString()
                .lblRegimenF.Text = dsSol.Tables(0).Rows(0).Item("regimen_fiscal").ToString()
                .lblDescrip.Text = dsSol.Tables(0).Rows(0).Item("descrip").ToString()
                .txtEspecificaciones.Text = dsSol.Tables(0).Rows(0).Item("especificaciones").ToString()
                .lblFechaAsig.Text = dsSol.Tables(0).Rows(0).Item("fecha_asigna").ToString()
                .lblFechaEmi.Text = dsSol.Tables(0).Rows(0).Item("fecha_emision").ToString()
                .lblFolioE.Text = dsSol.Tables(0).Rows(0).Item("folio").ToString()
                .lblCFDI.Text = dsSol.Tables(0).Rows(0).Item("CFDI").ToString()
                .lblTipo.Text = dsSol.Tables(0).Rows(0).Item("tipo").ToString()
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
                If dsSol.Tables(0).Rows(0).Item("contrato_NAV").ToString() = "S" Then
                    .lbl_Contrato.Visible = True
                    .lblContrato.Visible = True
                    .lblContrato.Text = dsSol.Tables(0).Rows(0).Item("no_contrato_NAV").ToString()
                Else
                    .lbl_Contrato.Visible = False
                    .lblContrato.Visible = False
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
                                                          "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid " + _
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
                .txtComentario.Enabled = True
                .btnRegNAV.Visible = True
                .btnRechaza.Visible = True
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

#Region "Registrar / Rechazar"

    Protected Sub btnRegNAV_Click(sender As Object, e As EventArgs) Handles btnRegNAV.Click
        With Me
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
            SCMValores.Connection = ConexionBD
            Dim fecha As DateTime = Date.Now
            'Actualizar datos de la Solicitud
            SCMValores.CommandText = ""
            SCMValores.Parameters.Clear()
            SCMValores.CommandText = "update ms_factura set id_usr_regNav = @id_usr_regNav, fecha_regNav = @fecha_regNav, status = 'RN', comentario_cxp = @comentario, ultimos_comentarios = @comentario where id_ms_factura = @id_ms_factura "
            SCMValores.Parameters.AddWithValue("@id_usr_regNav", Val(._txtIdUsuario.Text))
            SCMValores.Parameters.AddWithValue("@fecha_regNav", fecha)
            If .txtComentario.Text.Trim = "" Then
                SCMValores.Parameters.AddWithValue("@comentario", DBNull.Value)
            Else
                SCMValores.Parameters.AddWithValue("@comentario", .txtComentario.Text.Trim)
            End If
            SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
            ConexionBD.Open()
            SCMValores.ExecuteNonQuery()
            ConexionBD.Close()

            'Actualizar dt_factura
            SCMValores.Parameters.Clear()
            SCMValores.CommandText = "update dt_factura " + _
                                     "  set status = 'RN' " + _
                                     "where uuid = @uuid " + _
                                     "  and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') "
            SCMValores.Parameters.AddWithValue("@uuid", .lblCFDI.Text)
            ConexionBD.Open()
            SCMValores.ExecuteNonQuery()
            ConexionBD.Close()

            .txtComentario.Enabled = False
            .btnRegNAV.Visible = False
            .btnRechaza.Visible = False
        End With
    End Sub

    Protected Sub btnRechaza_Click(sender As Object, e As EventArgs) Handles btnRechaza.Click
        With Me
            Try
                .litError.Text = ""

                If .txtComentario.Text.Trim = "" Then
                    .litError.Text = "Favor de ingresar los comentarios correspondientes"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    Dim fecha As DateTime = Date.Now
                    'Actualizar datos de la Solicitud
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_factura set fecha_autoriza = NULL, fecha_autoriza2 = NULL, fecha_autoriza3 = NULL, status = 'P', comentario_cxp = @comentario, ultimos_comentarios = @comentario where id_ms_factura = @id_ms_factura "
                    SCMValores.Parameters.AddWithValue("@comentario", .txtComentario.Text.Trim)
                    SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Actualizar dt_factura
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update dt_factura " + _
                                             "  set status = 'As' " + _
                                             "where uuid = @uuid " + _
                                             "  and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') "
                    SCMValores.Parameters.AddWithValue("@uuid", .lblCFDI.Text)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    Dim idMsInst As Integer
                    Dim idAutAnt As Integer
                    Dim idActividad As Integer
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "select max(id_ms_instancia) as instancia " + _
                                             "from ms_instancia " + _
                                             "where tipo = 'F' " + _
                                             "  and id_ms_sol = @id_ms_factura "
                    SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                    ConexionBD.Open()
                    idMsInst = SCMValores.ExecuteScalar
                    ConexionBD.Close()

                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "select count(*) " + _
                                             "from ms_instancia " + _
                                             "  left join ms_historico on ms_instancia.id_ms_instancia = ms_historico.id_ms_instancia " + _
                                             "where tipo = 'F' " + _
                                             "  and id_ms_sol = @id_ms_factura " + _
                                             "  and ms_historico.id_actividad = 14 "
                    SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                    ConexionBD.Open()
                    idAutAnt = SCMValores.ExecuteScalar
                    ConexionBD.Close()

                    If idAutAnt > 0 Then
                        'Proceso anterior
                        idActividad = 17
                    Else
                        'Nuevo Proceso
                        idActividad = 51
                    End If

                    'Actualizar Instancia
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", idMsInst)
                    SCMValores.Parameters.AddWithValue("@id_actividad", idActividad)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Registrar en Histórico
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                             "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", idMsInst)
                    SCMValores.Parameters.AddWithValue("@id_actividad", idActividad)
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Envío de Correo
                    Dim Mensaje As New System.Net.Mail.MailMessage()
                    Dim destinatario As String = ""
                    'Obtener el Correo del Solicitante
                    SCMValores.CommandText = "select cgEmpl.correo " + _
                                             "from ms_factura " + _
                                             "  left join cg_usuario on ms_factura.id_usr_solicita = cg_usuario.id_usuario " + _
                                             "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                             "where id_ms_factura = @id_ms_factura "
                    SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                    ConexionBD.Open()
                    destinatario = SCMValores.ExecuteScalar()
                    ConexionBD.Close()

                    Mensaje.[To].Add(destinatario)
                    Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                    Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                    Mensaje.Subject = "ProcAd - Solicitud de Factura No. " + .lblFolio.Text + " Rechazada"
                    Dim texto As String
                    texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                            "La solicitud número <b>" + .lblFolio.Text + "</b> fue rechazada, se encuentra en la actividad <b>Corregir Factura</b><br></span>"
                    Mensaje.Body = texto
                    Mensaje.IsBodyHtml = True
                    Mensaje.Priority = MailPriority.Normal

                    Dim Servidor As New SmtpClient()
                    Servidor.Host = "10.10.10.30"
                    Servidor.Port = 587
                    Servidor.EnableSsl = False
                    Servidor.UseDefaultCredentials = False
                    Servidor.Credentials = New System.Net.NetworkCredential("nprocad", "mc8HLB8lPe78")

                    Try
                        Servidor.Send(Mensaje)
                    Catch ex As System.Net.Mail.SmtpException
                        .litError.Text = ex.ToString
                    End Try

                    .txtComentario.Enabled = False
                    .btnRegNAV.Visible = False
                    .btnRechaza.Visible = False
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class