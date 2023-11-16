Public Class ConsNegoc
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
                        query = "select distinct(id_usr_solicita) as id_usr_solicita " + _
                                "     , empleado_solicita as solicito " + _
                                "from ms_negoc_servicio "
                        'If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                        '    query = query + "where (id_usr_solicita = @idAutorizador or id_usr_cotiza = @idAutorizador or id_usr_aut_cotiza = @idAutorizador or id_usr_aut_negoc = @idAutorizador) "
                        'End If
                        query = query + "order by solicito "
                        sdaSolicitante.SelectCommand = New SqlCommand(query, ConexionBD)
                        'If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                        '    sdaSolicitante.SelectCommand.Parameters.AddWithValue("@idAutorizador", Val(._txtIdUsuario.Text))
                        'End If
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
                                "from ms_negoc_servicio "
                        'If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                        '    query = query + "where (id_usr_solicita = @idAutorizador or id_usr_cotiza = @idAutorizador or id_usr_aut_cotiza = @idAutorizador or id_usr_aut_negoc = @idAutorizador) "
                        'End If
                        query = query + "order by empresa "
                        sdaEmpresa.SelectCommand = New SqlCommand(query, ConexionBD)
                        'If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                        '    sdaEmpresa.SelectCommand.Parameters.AddWithValue("@autorizador", Val(._txtIdUsuario.Text))
                        'End If
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
                        query = "select distinct(id_usr_aut_negoc) as id_usr_aut_negoc " + _
                                "     , empleado_aut_negoc " + _
                                "from ms_negoc_servicio "
                        'If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Then
                        '    query = query + "where id_usr_solicita = @id_usr_solicita "
                        'End If
                        'If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Or ._txtPerfil.Text = "ContaF" Then
                        '    query = "select distinct(id_usr_aut_negoc) as id_usr_aut_negoc " + _
                        '            "     , empleado_aut_negoc " + _
                        '            "from ms_negoc_servicio " + _
                        '            "where id_usr_aut_negoc = @id_usr_aut_negoc "
                        'End If
                        query = query + "order by empleado_aut_negoc "
                        sdaAutorizador.SelectCommand = New SqlCommand(query, ConexionBD)
                        'If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Then
                        '    sdaAutorizador.SelectCommand.Parameters.AddWithValue("@id_usr_solicita", Val(._txtIdUsuario.Text))
                        'End If
                        'If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Or ._txtPerfil.Text = "ContaF" Then
                        '    sdaAutorizador.SelectCommand.Parameters.AddWithValue("@id_usr_aut_negoc", Val(._txtIdUsuario.Text))
                        'End If
                        .ddlAutorizador.DataSource = dsAutorizador
                        .ddlAutorizador.DataTextField = "empleado_aut_negoc"
                        .ddlAutorizador.DataValueField = "id_usr_aut_negoc"
                        ConexionBD.Open()
                        sdaAutorizador.Fill(dsAutorizador)
                        .ddlAutorizador.DataBind()
                        ConexionBD.Close()
                        sdaAutorizador.Dispose()
                        dsAutorizador.Dispose()
                        .ddlAutorizador.SelectedIndex = -1

                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "ContaF" Then
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
            .cbNoNegoc.Checked = False
            .pnlNoNegoc.Visible = False
            .cbStatus.Checked = False
            .pnlStatus.Visible = False
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

    Protected Sub cbNoFact_CheckedChanged(sender As Object, e As EventArgs) Handles cbNoNegoc.CheckedChanged
        vista(Me.pnlNoNegoc, Me.cbNoNegoc.Checked)
        If Me.cbNoNegoc.Checked = True Then
            Me.txtNoNegoc.Text = ""
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

                query = "select id_ms_negoc_servicio " + _
                        "     , servicio " + _
                        "     , empleado_solicita " + _
                        "     , empleado_cotiza " + _
                        "     , empleado_aut_cotiza " + _
                        "     , empleado_aut_negoc " + _
                        "     , empresa " + _
                        "     , isnull(division, centro_costo) as centro_costo " + _
                        "     , base " + _
                        "     , fecha_ini " + _
                        "     , fecha_fin " + _
                        "     , cg_proveedor.nombre as proveedor " + _
                        "     , fecha_solicita " + _
                        "     , fecha_cotiza " + _
                        "     , fecha_aut_cotiza " + _
                        "     , fecha_aut_negoc " + _
                        "     , case status " + _
                        "         when 'P' then 'En Cotización' " + _
                        "         when 'CotI' then 'Pendiente Autorización de Cotizaciones' " + _
                        "         when 'CotA' then 'Pendiente Autorización de Negociación' " + _
                        "         when 'NeA' then 'Negociación Autorizada' " + _
                        "         when 'NeZ' then 'Negociación Rechazada' " + _
                        "       end as estatus " + _
                        "from ms_negoc_servicio " + _
                        "  left join bd_SiSAC.dbo.cg_proveedor on ms_negoc_servicio.proveedor_selec = cg_proveedor.id_proveedor " + _
                        "where 1 = 1 "

                'If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Or ._txtPerfil.Text = "ContaF" Then
                '    query = query + "  and (id_usr_solicita = @idAutorizadorU or id_usr_cotiza = @idAutorizadorU or id_usr_aut_cotiza = @idAutorizadorU or id_usr_aut_negoc = @idAutorizadorU) "
                'End If

                'If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "GerSopTec" Or ._txtPerfil.Text = "SopTec" Or ._txtPerfil.Text = "Vig" Or _txtPerfil.Text = "JefCompras" Or ._txtPerfil.Text = "Compras" Or .cbSolicitante.Checked = True Then
                If .cbSolicitante.Checked = True Then
                    query = query + "  and id_usr_solicita = @id_usr_solicita "
                End If
                If .cbEmpresa.Checked = True Then
                    query = query + "  and empresa = @empresa "
                End If
                If .cbFechaC.Checked = True Then
                    query = query + "  and (fecha_solicita between @fechaIni and @fechaFin) "
                End If
                If .cbAutorizador.Checked = True Then
                    query = query + "  and (id_usr_aut_negoc = @id_usr_aut_negoc) "
                End If
                If .cbProveedor.Checked = True Then
                    query = query + "  and cg_proveedor.nombre like '%' + @proveedor + '%' "
                End If
                If .cbNoNegoc.Checked = True Then
                    query = query + "  and id_ms_negoc_servicio = @id_ms_negoc_servicio "
                End If
                If .cbStatus.Checked = True Then
                    query = query + "  and ms_negoc_servicio.status in (" + .ddlStatus.SelectedValue + ") "
                End If
                query = query + "order by ms_negoc_servicio.id_ms_negoc_servicio "

                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)

                'If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Or ._txtPerfil.Text = "ContaF" Then
                '    sdaConsulta.SelectCommand.Parameters.AddWithValue("@idAutorizadorU", Val(._txtIdUsuario.Text))
                'End If

                'If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "GerSopTec" Or ._txtPerfil.Text = "SopTec" Or ._txtPerfil.Text = "Vig" Or _txtPerfil.Text = "JefCompras" Or ._txtPerfil.Text = "Compras" Then
                '    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_usr_solicita", Val(._txtIdUsuario.Text))
                'End If
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
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_usr_aut_negoc", .ddlAutorizador.SelectedValue)
                End If
                If .cbProveedor.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@proveedor", .txtProveedor.Text.Trim)
                End If
                If .cbNoNegoc.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_ms_negoc_servicio", .txtNoNegoc.Text.Trim)
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
                query = "select empleado_solicita " + _
                        "     , empresa " + _
                        "     , isnull(division, centro_costo) as centro_costo " + _
                        "     , case when division is null then 'CC' else 'DIV' end as dimension " + _
                        "     , servicio " + _
                        "     , base " + _
                        "     , convert(varchar, ms_negoc_servicio.fecha_ini, 103) + ' al ' + convert(varchar, ms_negoc_servicio.fecha_fin, 103) as vigencia " + _
                        "     , lugar_ejecucion " + _
                        "     , ms_negoc_servicio.descripcion " + _
                        "     , isnull(comentario_aut_cotiza, '') as comentario_aut_cotiza " + _
                        "     , isnull(comentario_aut_negoc, '') as comentario_aut_negoc " + _
                        "     , isnull(cotizacion_selec, 0) as cotizacion_selec " + _
                        "     , isnull(proveedor_selec, 0) as proveedor_selec " + _
                        "     , isnull(cg_proveedor.nombre, '') as proveedor " + _
                        "     , isnull(prov1.nombre, cot1.prov_nombre) as proveedor1 " + _
                        "     , isnull(cast(cot1.cantidad as varchar(10)), '') as cantidad1 " + _
                        "     , isnull(FORMAT(cot1.precio, 'C2', 'es-MX'), '') as importe1 " + _
                        "     , isnull(div1.descripcion, '') as divisa1 " + _
                        "     , isnull(convert(varchar, cot1.fecha_ini, 103), '') as fecha_ini1 " + _
                        "     , isnull(convert(varchar, cot1.fecha_fin, 103), '') as fecha_fin1 " + _
                        "     , isnull(condPago1.cond_pago, '') as condPago1 " + _
                        "     , isnull(cot1.nombre_archivo, '') as archivo1 " + _
                        "     , isnull('http://148.223.153.43/ProcAd - Adjuntos NegServ Cot/' + cast(cot1.id_dt_cotizacion_ns as varchar(20)) + '-' + cot1.nombre_archivo, '') as path1 " + _
                        "     , isnull(prov2.nombre, cot2.prov_nombre) as proveedor2 " + _
                        "     , isnull(cast(cot2.cantidad as varchar(10)), '') as cantidad2 " + _
                        "     , isnull(FORMAT(cot2.precio, 'C2', 'es-MX'), '') as importe2 " + _
                        "     , isnull(div2.descripcion, '') as divisa2 " + _
                        "     , isnull(convert(varchar, cot2.fecha_ini, 103), '') as fecha_ini2 " + _
                        "     , isnull(convert(varchar, cot2.fecha_fin, 103), '') as fecha_fin2 " + _
                        "     , isnull(condPago2.cond_pago, '') as condPago2 " + _
                        "     , isnull(cot2.nombre_archivo, '') as archivo2 " + _
                        "     , isnull('http://148.223.153.43/ProcAd - Adjuntos NegServ Cot/' + cast(cot2.id_dt_cotizacion_ns as varchar(20)) + '-' + cot2.nombre_archivo, '') as path2 " + _
                        "     , isnull(prov3.nombre, cot3.prov_nombre) as proveedor3 " + _
                        "     , isnull(cast(cot3.cantidad as varchar(10)), '') as cantidad3 " + _
                        "     , isnull(FORMAT(cot3.precio, 'C2', 'es-MX'), '') as importe3 " + _
                        "     , isnull(div3.descripcion, '') as divisa3 " + _
                        "     , isnull(convert(varchar, cot3.fecha_ini, 103), '') as fecha_ini3 " + _
                        "     , isnull(convert(varchar, cot3.fecha_fin, 103), '') as fecha_fin3 " + _
                        "     , isnull(condPago3.cond_pago, '') as condPago3 " + _
                        "     , isnull(cot3.nombre_archivo, '') as archivo3 " + _
                        "     , isnull('http://148.223.153.43/ProcAd - Adjuntos NegServ Cot/' + cast(cot3.id_dt_cotizacion_ns as varchar(20)) + '-' + cot3.nombre_archivo, '') as path3 " + _
                        "from ms_negoc_servicio " + _
                        "  left join bd_SiSAC.dbo.cg_proveedor on ms_negoc_servicio.proveedor_selec = cg_proveedor.id_proveedor " + _
                        "  left join dt_cotizacion_ns cot1 on cot1.id_dt_cotizacion_ns in (select max(cotT.id_dt_cotizacion_ns) from dt_cotizacion_ns cotT where cotT.id_ms_negoc_servicio = ms_negoc_servicio.id_ms_negoc_servicio and cotT.no_cotizacion = 1) " + _
                        "  left join bd_SiSAC.dbo.cg_proveedor prov1 on cot1.id_proveedor = prov1.id_proveedor " + _
                        "  left join bd_SiSAC.dbo.cg_divisa div1 on cot1.id_divisa = div1.id_divisa " + _
                        "  left join bd_SiSAC.dbo.cg_cond_pago condPago1 on cot1.id_cond_pago = condPago1.id_cond_pago " + _
                        "  left join dt_cotizacion_ns cot2 on cot2.id_dt_cotizacion_ns in (select max(cotT.id_dt_cotizacion_ns) from dt_cotizacion_ns cotT where cotT.id_ms_negoc_servicio = ms_negoc_servicio.id_ms_negoc_servicio and cotT.no_cotizacion = 2) " + _
                        "  left join bd_SiSAC.dbo.cg_proveedor prov2 on cot2.id_proveedor = prov2.id_proveedor " + _
                        "  left join bd_SiSAC.dbo.cg_divisa div2 on cot2.id_divisa = div2.id_divisa " + _
                        "  left join bd_SiSAC.dbo.cg_cond_pago condPago2 on cot2.id_cond_pago = condPago2.id_cond_pago " + _
                        "  left join dt_cotizacion_ns cot3 on cot3.id_dt_cotizacion_ns in (select max(cotT.id_dt_cotizacion_ns) from dt_cotizacion_ns cotT where cotT.id_ms_negoc_servicio = ms_negoc_servicio.id_ms_negoc_servicio and cotT.no_cotizacion = 3) " + _
                        "  left join bd_SiSAC.dbo.cg_proveedor prov3 on cot3.id_proveedor = prov3.id_proveedor " + _
                        "  left join bd_SiSAC.dbo.cg_divisa div3 on cot3.id_divisa = div3.id_divisa " + _
                        "  left join bd_SiSAC.dbo.cg_cond_pago condPago3 on cot3.id_cond_pago = condPago3.id_cond_pago " + _
                        "where ms_negoc_servicio.id_ms_negoc_servicio = @id_ms_negoc_servicio "
                sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaSol.SelectCommand.Parameters.AddWithValue("@id_ms_negoc_servicio", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaSol.Fill(dsSol)
                ConexionBD.Close()
                .lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("empleado_solicita").ToString()
                .lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                If dsSol.Tables(0).Rows(0).Item("dimension").ToString() = "CC" Then
                    .lbl_CC.Text = "Centro de Costo:"
                Else
                    .lbl_CC.Text = "División:"
                End If
                .lblCC.Text = dsSol.Tables(0).Rows(0).Item("centro_costo").ToString()
                .lblTipoServicio.Text = dsSol.Tables(0).Rows(0).Item("servicio").ToString()
                .lblBase.Text = dsSol.Tables(0).Rows(0).Item("base").ToString()
                .lblVigencia.Text = dsSol.Tables(0).Rows(0).Item("vigencia").ToString()
                .lblLugar.Text = dsSol.Tables(0).Rows(0).Item("lugar_ejecucion").ToString()
                .txtDescripcion.Text = dsSol.Tables(0).Rows(0).Item("descripcion").ToString()
                .lbl_Cotizacion.Visible = True
                .pnlCotizaciones.Visible = True
                .lbl_ProvSel.Visible = True
                .rblProvSel.Visible = True
                If Val(dsSol.Tables(0).Rows(0).Item("cotizacion_selec").ToString()) = 0 Then
                    .rblProvSel.SelectedIndex = -1
                Else
                    .rblProvSel.SelectedValue = dsSol.Tables(0).Rows(0).Item("cotizacion_selec").ToString()
                End If
                .lbl_ComentarioComp.Visible = True
                .txtComentarioComp.Visible = True
                .txtComentarioComp.Text = dsSol.Tables(0).Rows(0).Item("comentario_aut_cotiza").ToString()
                .txtComentario.Text = dsSol.Tables(0).Rows(0).Item("comentario_aut_negoc").ToString()

                'Cotización 1
                .lblProveedorCot1.Text = dsSol.Tables(0).Rows(0).Item("proveedor1").ToString()
                .lblCantidad1.Text = dsSol.Tables(0).Rows(0).Item("cantidad1").ToString()
                .lblImporte1.Text = dsSol.Tables(0).Rows(0).Item("importe1").ToString()
                .lblDivisa1.Text = dsSol.Tables(0).Rows(0).Item("divisa1").ToString()
                .lblFechaIni1.Text = dsSol.Tables(0).Rows(0).Item("fecha_ini1").ToString()
                .lblFechaFin1.Text = dsSol.Tables(0).Rows(0).Item("fecha_fin1").ToString()
                .lblCondP1.Text = dsSol.Tables(0).Rows(0).Item("condPago1").ToString()
                .hlPDF1.Text = dsSol.Tables(0).Rows(0).Item("archivo1").ToString()
                .hlPDF1.NavigateUrl = dsSol.Tables(0).Rows(0).Item("path1").ToString()
                'Cotización 2
                .lblProveedorCot2.Text = dsSol.Tables(0).Rows(0).Item("proveedor2").ToString()
                .lblCantidad2.Text = dsSol.Tables(0).Rows(0).Item("cantidad2").ToString()
                .lblImporte2.Text = dsSol.Tables(0).Rows(0).Item("importe2").ToString()
                .lblDivisa2.Text = dsSol.Tables(0).Rows(0).Item("divisa2").ToString()
                .lblFechaIni2.Text = dsSol.Tables(0).Rows(0).Item("fecha_ini2").ToString()
                .lblFechaFin2.Text = dsSol.Tables(0).Rows(0).Item("fecha_fin2").ToString()
                .lblCondP2.Text = dsSol.Tables(0).Rows(0).Item("condPago2").ToString()
                .hlPDF2.Text = dsSol.Tables(0).Rows(0).Item("archivo2").ToString()
                .hlPDF2.NavigateUrl = dsSol.Tables(0).Rows(0).Item("path2").ToString()
                'Cotización 3
                .lblProveedorCot3.Text = dsSol.Tables(0).Rows(0).Item("proveedor3").ToString()
                .lblCantidad3.Text = dsSol.Tables(0).Rows(0).Item("cantidad3").ToString()
                .lblImporte3.Text = dsSol.Tables(0).Rows(0).Item("importe3").ToString()
                .lblDivisa3.Text = dsSol.Tables(0).Rows(0).Item("divisa3").ToString()
                .lblFechaIni3.Text = dsSol.Tables(0).Rows(0).Item("fecha_ini3").ToString()
                .lblFechaFin3.Text = dsSol.Tables(0).Rows(0).Item("fecha_fin3").ToString()
                .lblCondP3.Text = dsSol.Tables(0).Rows(0).Item("condPago3").ToString()
                .hlPDF3.Text = dsSol.Tables(0).Rows(0).Item("archivo3").ToString()
                .hlPDF3.NavigateUrl = dsSol.Tables(0).Rows(0).Item("path3").ToString()
                sdaSol.Dispose()
                dsSol.Dispose()

                'Adjuntos
                Dim sdaArchivos As New SqlDataAdapter
                Dim dsArchivos As New DataSet
                .gvAdjuntos.DataSource = dsArchivos
                sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " + _
                                                           "     , 'http://148.223.153.43/ProcAd - Adjuntos NegServ/' + cast(id_dt_archivo_ns as varchar(20)) + '-' + nombre as path " + _
                                                           "from dt_archivo_ns " + _
                                                           "where id_ms_negoc_servicio = @idMsNegSer " + _
                                                           "  and tipo = 'A' ", ConexionBD)
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

    Protected Sub btnNuevaBus_Click(sender As Object, e As EventArgs) Handles btnNuevaBus.Click
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