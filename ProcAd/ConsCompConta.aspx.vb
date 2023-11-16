Public Class ConsCompConta
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
                        query = "select distinct(empresa) as empresa " + _
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
                        query = "select distinct(autorizador) as autorizador " + _
                                "from ms_comp "
                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Then
                            query = query + "where empleado = @empleado "
                        End If
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            query = query + "where empleado = @autorizador or autorizador = @autorizador or id_usr_aut_dir = @idUsrDir "
                        End If
                        query = query + "order by autorizador "
                        sdaAutorizador.SelectCommand = New SqlCommand(query, ConexionBD)
                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Then
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

                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Then
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
            '.cbFechaC.Checked = False
            .pnlFechaC.Visible = True
            .wdpFechaI.Date = Date.Now.ToShortDateString
            .wdpFechaF.Date = Date.Now.ToShortDateString
            .cbAutorizador.Checked = False
            .pnlAutorizador.Visible = False
            .cbStatus.Checked = True
            .pnlStatus.Visible = True
            .ddlStatus.SelectedIndex = 3
            .cbNoComp.Checked = False
            .pnlNoComp.Visible = False
            .cbEmpresa.Checked = True
            .pnlEmpresa.Visible = True
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
                Dim queryGral As String
                Dim queryIEPS As String
                Dim queryImpLoc As String
                'Consulta General
                queryGral = "select ms_comp.id_ms_comp as [No. Comp.] " +
                            "     , ms_comp.empresa as Empresa " +
                            "     , ms_comp.tipo_gasto as [Tipo Gasto] " +
                            "     , ms_comp.periodo_comp as [Periodo Comprobación] " +
                            "     , ms_comp.periodo_ini as Desde " +
                            "     , ms_comp.periodo_fin as Hasta " +
                            "     , ms_comp.empleado as Empleado " +
                            "     , ms_comp.tipo_actividad as [Tipo Actividad] " +
                            "     , ms_comp.autorizador as Autorizador " +
                            "     , ms_comp.director as Director " +
                            "     , CAST(ms_comp.justificacion AS varchar(max)) as Justificación " +
                            "     , (select sum(monto_hospedaje) + sum(monto_alimentos) + sum(monto_casetas) + sum(monto_otros) as monto_ant " +
                            "        from dt_anticipo " +
                            "          left join ms_anticipo on dt_anticipo.id_ms_anticipo = ms_anticipo.id_ms_anticipo " +
                            "        where dt_anticipo.id_ms_comp = ms_comp.id_ms_comp) as importe_ant " +
                            "     , (select sum(monto_total) as monto_comp " +
                            "        from dt_comp " +
                            "        where dt_comp.id_ms_comp = ms_comp.id_ms_comp) as importe_comp " +
                            "     , importe_tot " +
                            "     , CONVERT(varchar, dt_comp.fecha_realizo, 103) as [Fecha de realización] " +
                            "     , case tipo when 'F' " +
                            "	     then (select cuenta from cg_concepto_comp where cg_concepto_comp.id_concepto_comp = dt_comp.id_concepto) " +
                            "		 else (select cuenta from cg_concepto where cg_concepto.id_concepto = dt_comp.id_concepto) " +
                            "	   end as [Num. Cuenta] " +
                            "     , case " +
                            "	     when centro_costo is not null then (select max(codigo) " +
                            "											 from bd_Empleado.dbo.cg_cc as cgCC " +
                            "											   left join bd_Empleado.dbo.cg_empresa cgEmpresa on cgCC.id_empresa = cgEmpresa.id_empresa " +
                            "											 where cgEmpresa.nombre = ms_comp.empresa " +
                            "											   and cgCC.nombre = ms_comp.centro_costo) " +
                            "		 else NULL " +
                            "	   end as CC " +
                            "     , case " +
                            "	     when division is not null then (select max(codigo) " +
                            "										 from bd_Empleado.dbo.cg_div as cgDIV " +
                            "										   left join bd_Empleado.dbo.cg_empresa cgEmpresa on cgDIV.id_empresa = cgEmpresa.id_empresa " +
                            "										 where cgEmpresa.nombre = ms_comp.empresa " +
                            "										   and cgDIV.nombre = ms_comp.division) " +
                            "		 else NULL " +
                            "	   end as DIV " +
                            "     , case " +
                            "	     when division is not null then (select max(zn) " +
                            "										 from bd_Empleado.dbo.cg_div as cgDIV " +
                            "										   left join bd_Empleado.dbo.cg_empresa cgEmpresa on cgDIV.id_empresa = cgEmpresa.id_empresa " +
                            "										 where cgEmpresa.nombre = ms_comp.empresa " +
                            "										   and cgDIV.nombre = ms_comp.division) " +
                            "		 else NULL " +
                            "	   end as ZN " +
                            "     , case tipo " +
                            "	     when 'F' then sum(dt_comp_linea.importe) " +
                            "		 else dt_comp.monto_subtotal " +
                            "	   end as Subtotal " +
                            "     , case tipo " +
                            "	     when 'F' then -1 * sum(dt_comp_linea.descuento) " +
                            "		 else NULL " +
                            "	   end as Descuento " +
                            "     , case tipo " +
                            "	     when 'F' then sum(dt_comp_linea.importe) + (-1 * sum(dt_comp_linea.descuento)) " +
                            "		 else dt_comp.monto_subtotal " +
                            "	   end as Diferencia " +
                            "     , dt_comp_linea.tasa_iva as [Tasa IVA] " +
                            "     , case tipo " +
                            "	     when 'F' then sum(dt_comp_linea.iva) " +
                            "		 else NULL " +
                            "	   end as [IVA Importe] " +
                            "     , NULL as [ISR Retenido] " +
                            "     , case tipo " +
                            "	     when 'F' then no_factura + ' ' + proveedor " +
                            "		 else 'Vale Azul' " +
                            "	   end as Descripcion " +
                            "     , case ms_comp.status " +
                            "         when 'P' then 'Pendiente de Autorización' " +
                            "         when 'A' then 'Autorizado' " +
                            "         when 'ZA' then 'Comprobación No Autorizada' " +
                            "         when 'ZD' then 'Comprobación No Autorizada por Director' " +
                            "         when 'ZC' then 'Comprobación Cancelada' " +
                            "         when 'ZP' then 'Comprobación Cancelada' " +
                            "         when 'ZU' then 'Comprobación Cancelada por el Usuario' " +
                            "         when 'R' then 'Comprobación Registrada' " +
                            "       end as Estatus " +
                            "     , ms_comp.fecha_solicita " +
                            "from ms_comp " +
                            "  left join dt_comp on ms_comp.id_ms_comp = dt_comp.id_ms_comp " +
                            "  left join dt_comp_linea on dt_comp.id_dt_comp = dt_comp_linea.id_dt_comp " +
                            "where (ms_comp.fecha_solicita between @fechaIni and @fechaFin) "
                'Consulta de IEPS
                queryIEPS = "select ms_comp.id_ms_comp as [No. Comp.] " +
                            "     , ms_comp.empresa as Empresa " +
                            "     , ms_comp.tipo_gasto as [Tipo Gasto] " +
                            "     , ms_comp.periodo_comp as [Periodo Comprobación] " +
                            "     , ms_comp.periodo_ini as Desde " +
                            "     , ms_comp.periodo_fin as Hasta " +
                            "     , ms_comp.empleado as Empleado " +
                            "     , ms_comp.tipo_actividad as [Tipo Actividad] " +
                            "     , ms_comp.autorizador as Autorizador " +
                            "     , ms_comp.director as Director " +
                            "     , CAST(ms_comp.justificacion AS varchar(max)) as Justificación " +
                            "     , (select sum(monto_hospedaje) + sum(monto_alimentos) + sum(monto_casetas) + sum(monto_otros) as monto_ant " +
                            "        from dt_anticipo " +
                            "          left join ms_anticipo on dt_anticipo.id_ms_anticipo = ms_anticipo.id_ms_anticipo " +
                            "        where dt_anticipo.id_ms_comp = ms_comp.id_ms_comp) as importe_ant " +
                            "     , (select sum(monto_total) as monto_comp " +
                            "        from dt_comp " +
                            "        where dt_comp.id_ms_comp = ms_comp.id_ms_comp) as importe_comp " +
                            "     , importe_tot " +
                            "     , ms_comp.periodo_comp as [Fecha de realización] " +
                            "     , case when (select abreviatura  " +
                            "                  from ms_comp msCompT " +
                            "                    inner join cg_tipoGasto on msCompT.tipo_gasto = cg_tipoGasto.nombre_gasto " +
                            "                    inner join bd_Empleado.dbo.cg_empresa cgEmpr on ms_comp.empresa = cgEmpr.nombre and cg_tipoGasto.id_empresa = cgEmpr.id_empresa " +
                            "                  where msCompT.id_ms_comp = ms_comp.id_ms_comp) = 'GOI' " +
                            "              then '6100-0173' " +
                            "			else '6101-0173' " +
                            "       end as [Num. Cuenta] " +
                            "     , case " +
                            "	     when centro_costo is not null then (select max(codigo) " +
                            "											 from bd_Empleado.dbo.cg_cc as cgCC " +
                            "											   left join bd_Empleado.dbo.cg_empresa cgEmpresa on cgCC.id_empresa = cgEmpresa.id_empresa " +
                            "											 where cgEmpresa.nombre = ms_comp.empresa " +
                            "											   and cgCC.nombre = ms_comp.centro_costo) " +
                            "		 else NULL " +
                            "	   end as CC " +
                            "     , case " +
                            "	     when division is not null then (select max(codigo) " +
                            "										 from bd_Empleado.dbo.cg_div as cgDIV " +
                            "										   left join bd_Empleado.dbo.cg_empresa cgEmpresa on cgDIV.id_empresa = cgEmpresa.id_empresa " +
                            "										 where cgEmpresa.nombre = ms_comp.empresa " +
                            "										   and cgDIV.nombre = ms_comp.division) " +
                            "		 else NULL " +
                            "	   end as DIV " +
                            "     , case " +
                            "	     when division is not null then (select max(zn) " +
                            "										 from bd_Empleado.dbo.cg_div as cgDIV " +
                            "										   left join bd_Empleado.dbo.cg_empresa cgEmpresa on cgDIV.id_empresa = cgEmpresa.id_empresa " +
                            "										 where cgEmpresa.nombre = ms_comp.empresa " +
                            "										   and cgDIV.nombre = ms_comp.division) " +
                            "		 else NULL " +
                            "	   end as ZN " +
                            "     , sum(dt_comp_linea.ieps) as Subtotal " +
                            "     , NULL as Descuento " +
                            "     , sum(dt_comp_linea.ieps) as Diferencia " +
                            "     , NULL as [Tasa IVA] " +
                            "     , NULL as [IVA Importe] " +
                            "     , NULL as [ISR Retenido] " +
                            "     , 'IEPS' as Descripcion " +
                            "     , case ms_comp.status " +
                            "         when 'P' then 'Pendiente de Autorización' " +
                            "         when 'A' then 'Autorizado' " +
                            "         when 'ZA' then 'Comprobación No Autorizada' " +
                            "         when 'ZD' then 'Comprobación No Autorizada por Director' " +
                            "         when 'ZC' then 'Comprobación Cancelada' " +
                            "         when 'ZP' then 'Comprobación Cancelada' " +
                            "         when 'ZU' then 'Comprobación Cancelada por el Usuario' " +
                            "         when 'R' then 'Comprobación Registrada' " +
                            "       end as Estatus " +
                            "     , ms_comp.fecha_solicita " +
                            "from ms_comp " +
                            "  left join dt_comp on ms_comp.id_ms_comp = dt_comp.id_ms_comp " +
                            "  left join dt_comp_linea on dt_comp.id_dt_comp = dt_comp_linea.id_dt_comp " +
                            "where (ms_comp.fecha_solicita between @fechaIni and @fechaFin) and tipo = 'F' and dt_comp_linea.ieps > 0 "
                'Consulta de Impuestos Locales
                queryImpLoc = "select ms_comp.id_ms_comp as [No. Comp.] " +
                              "     , ms_comp.empresa as Empresa " +
                              "     , ms_comp.tipo_gasto as [Tipo Gasto] " +
                              "     , ms_comp.periodo_comp as [Periodo Comprobación] " +
                              "     , ms_comp.periodo_ini as Desde " +
                              "     , ms_comp.periodo_fin as Hasta " +
                              "     , ms_comp.empleado as Empleado " +
                              "     , ms_comp.tipo_actividad as [Tipo Actividad] " +
                              "     , ms_comp.autorizador as Autorizador " +
                              "     , ms_comp.director as Director " +
                              "     , CAST(ms_comp.justificacion AS varchar(max)) as Justificación " +
                              "     , (select sum(monto_hospedaje) + sum(monto_alimentos) + sum(monto_casetas) + sum(monto_otros) as monto_ant " +
                              "        from dt_anticipo " +
                              "          left join ms_anticipo on dt_anticipo.id_ms_anticipo = ms_anticipo.id_ms_anticipo " +
                              "        where dt_anticipo.id_ms_comp = ms_comp.id_ms_comp) as importe_ant " +
                              "     , (select sum(monto_total) as monto_comp " +
                              "        from dt_comp " +
                              "        where dt_comp.id_ms_comp = ms_comp.id_ms_comp) as importe_comp " +
                              "     , importe_tot " +
                              "     , ms_comp.periodo_comp as [Fecha de realización] " +
                              "     , case when (select abreviatura " +
                              "                  from ms_comp msCompT " +
                              "                    inner join cg_tipoGasto on msCompT.tipo_gasto = cg_tipoGasto.nombre_gasto " +
                              "                    inner join bd_Empleado.dbo.cg_empresa cgEmpr on ms_comp.empresa = cgEmpr.nombre and cg_tipoGasto.id_empresa = cgEmpr.id_empresa " +
                              "                  where msCompT.id_ms_comp = ms_comp.id_ms_comp) = 'GOI' " +
                              "              then '6100-0168' " +
                              "			else '6101-0168' " +
                              "       end as [Num. Cuenta] " +
                              "     , case " +
                              "	        when centro_costo is not null then (select max(codigo) " +
                              "											    from bd_Empleado.dbo.cg_cc as cgCC " +
                              "											      left join bd_Empleado.dbo.cg_empresa cgEmpresa on cgCC.id_empresa = cgEmpresa.id_empresa " +
                              "											    where cgEmpresa.nombre = ms_comp.empresa " +
                              "											      and cgCC.nombre = ms_comp.centro_costo) " +
                              "		    else NULL " +
                              "	      end as CC " +
                              "     , case " +
                              "	        when division is not null then (select max(codigo) " +
                              "										    from bd_Empleado.dbo.cg_div as cgDIV " +
                              "										      left join bd_Empleado.dbo.cg_empresa cgEmpresa on cgDIV.id_empresa = cgEmpresa.id_empresa " +
                              "										    where cgEmpresa.nombre = ms_comp.empresa " +
                              "										      and cgDIV.nombre = ms_comp.division) " +
                              "		    else NULL " +
                              "	      end as DIV " +
                              "     , case " +
                              "	        when division is not null then (select max(zn) " +
                              "										    from bd_Empleado.dbo.cg_div as cgDIV " +
                              "										      left join bd_Empleado.dbo.cg_empresa cgEmpresa on cgDIV.id_empresa = cgEmpresa.id_empresa " +
                              "										    where cgEmpresa.nombre = ms_comp.empresa " +
                              "										      and cgDIV.nombre = ms_comp.division) " +
                              "		    else NULL " +
                              "	      end as ZN " +
                              "     , sum(monto_total - (monto_subtotal + monto_iva)) as Subtotal " +
                              "     , NULL as Descuento " +
                              "     , sum(monto_total - (monto_subtotal + monto_iva)) as Diferencia " +
                              "     , NULL as [Tasa IVA] " +
                              "     , NULL as [IVA Importe] " +
                              "     , NULL as [ISR Retenido] " +
                              "     , 'Impuestos Locales' as Descripcion " +
                              "     , case ms_comp.status " +
                              "         when 'P' then 'Pendiente de Autorización' " +
                              "         when 'A' then 'Autorizado' " +
                              "         when 'ZA' then 'Comprobación No Autorizada' " +
                              "         when 'ZD' then 'Comprobación No Autorizada por Director' " +
                              "         when 'ZC' then 'Comprobación Cancelada' " +
                              "         when 'ZP' then 'Comprobación Cancelada' " +
                              "         when 'ZU' then 'Comprobación Cancelada por el Usuario' " +
                              "         when 'R' then 'Comprobación Registrada' " +
                              "       end as Estatus " +
                              "     , ms_comp.fecha_solicita " +
                              "from ms_comp " +
                              "  left join dt_comp on ms_comp.id_ms_comp = dt_comp.id_ms_comp " +
                              "where (ms_comp.fecha_solicita between @fechaIni and @fechaFin) and tipo = 'F' " +
                              "  and monto_total > monto_subtotal + monto_iva "

                If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                    queryGral = queryGral + "and (ms_comp.empleado = @autorizadorU or ms_comp.autorizador = @autorizadorU or id_usr_aut_dir = @idUsrDir) "
                    queryIEPS = queryIEPS + "and (ms_comp.empleado = @autorizadorU or ms_comp.autorizador = @autorizadorU or id_usr_aut_dir = @idUsrDir) "
                    queryImpLoc = queryImpLoc + "and (ms_comp.empleado = @autorizadorU or ms_comp.autorizador = @autorizadorU or id_usr_aut_dir = @idUsrDir) "
                End If

                If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Or .cbSolicitante.Checked = True Then
                    queryGral = queryGral + "  and id_usr_solicita = @id_usr_solicita "
                    queryIEPS = queryIEPS + "  and id_usr_solicita = @id_usr_solicita "
                    queryImpLoc = queryImpLoc + "  and id_usr_solicita = @id_usr_solicita "
                End If
                If .cbEmpresa.Checked = True Then
                    queryGral = queryGral + "  and ms_comp.empresa = @empresa "
                    queryIEPS = queryIEPS + "  and ms_comp.empresa = @empresa "
                    queryImpLoc = queryImpLoc + "  and ms_comp.empresa = @empresa "
                End If
                'If .cbFechaC.Checked = True Then
                '    queryGral = queryGral + "  and (fecha_solicita between @fechaIni and @fechaFin) "
                '    queryIEPS = queryIEPS + "  and (fecha_solicita between @fechaIni and @fechaFin) "
                '    queryImpLoc = queryImpLoc + "  and (fecha_solicita between @fechaIni and @fechaFin) "
                'End If
                If .cbAutorizador.Checked = True Then
                    queryGral = queryGral + "  and ms_comp.autorizador = @autorizador "
                    queryIEPS = queryIEPS + "  and ms_comp.autorizador = @autorizador "
                    queryImpLoc = queryImpLoc + "  and ms_comp.autorizador = @autorizador "
                End If
                If .cbNoComp.Checked = True Then
                    queryGral = queryGral + "  and ms_comp.id_ms_comp = @id_ms_comp "
                    queryIEPS = queryIEPS + "  and ms_comp.id_ms_comp = @id_ms_comp "
                    queryImpLoc = queryImpLoc + "  and ms_comp.id_ms_comp = @id_ms_comp "
                End If
                If .cbStatus.Checked = True Then
                    queryGral = queryGral + "  and ms_comp.status in (" + .ddlStatus.SelectedValue + ") "
                    queryIEPS = queryIEPS + "  and ms_comp.status in (" + .ddlStatus.SelectedValue + ") "
                    queryImpLoc = queryImpLoc + "  and ms_comp.status in (" + .ddlStatus.SelectedValue + ") "
                End If

                queryGral = queryGral + "group by dt_comp.id_dt_comp, dt_comp.fecha_realizo, ms_comp.id_ms_comp, ms_comp.empresa, ms_comp.importe_tot " + _
                                        "       , ms_comp.tipo_gasto, ms_comp.periodo_comp, ms_comp.periodo_ini, ms_comp.periodo_fin, ms_comp.empleado, ms_comp.tipo_actividad, ms_comp.autorizador, ms_comp.director, CAST(ms_comp.justificacion AS varchar(max)) " + _
                                        "       , ms_comp.centro_costo, ms_comp.division, tasa_iva, tipo, no_factura + ' ' + proveedor, dt_comp.id_concepto, dt_comp.nombre_concepto, dt_comp.monto_subtotal, ms_comp.status, ms_comp.fecha_solicita "

                queryIEPS = queryIEPS + "group by ms_comp.id_ms_comp, ms_comp.empresa, ms_comp.importe_tot " + _
                                        "       , ms_comp.tipo_gasto, ms_comp.periodo_comp, ms_comp.periodo_ini, ms_comp.periodo_fin, ms_comp.empleado, ms_comp.tipo_actividad, ms_comp.autorizador, ms_comp.director, CAST(ms_comp.justificacion AS varchar(max)) " + _
                                        "	    , ms_comp.centro_costo, ms_comp.division, ms_comp.status, ms_comp.fecha_solicita "

                queryImpLoc = queryImpLoc + "group by ms_comp.id_ms_comp, ms_comp.empresa, ms_comp.importe_tot " + _
                                            "       , ms_comp.tipo_gasto, ms_comp.periodo_comp, ms_comp.periodo_ini, ms_comp.periodo_fin, ms_comp.empleado, ms_comp.tipo_actividad, ms_comp.autorizador, ms_comp.director, CAST(ms_comp.justificacion AS varchar(max)) " + _
                                            "	    , ms_comp.centro_costo, ms_comp.division, ms_comp.status, ms_comp.fecha_solicita "

                Dim query As String
                query = queryGral + _
                        "union all " + _
                        queryIEPS + _
                        "union all " + _
                        queryImpLoc + _
                        "order by ms_comp.id_ms_comp "

                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)

                If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@autorizadorU", ._txtEmpleado.Text)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@idUsrDir", Val(._txtIdUsuario.Text))
                End If

                If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_usr_solicita", Val(._txtIdUsuario.Text))
                End If
                If .cbSolicitante.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_usr_solicita", .ddlSolicitante.SelectedValue)
                End If
                If .cbEmpresa.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedValue)
                End If
                'If .cbFechaC.Checked = True Then
                sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaIni", .wdpFechaI.Date)
                sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaFin", .wdpFechaF.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
                'End If
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