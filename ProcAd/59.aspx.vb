Public Class _59
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 3
                    'Session("idMsInst") = 1

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtIdMsInst.Text = Session("idMsInst")
                        ._txtBan.Text = 0
                        Session("Error") = ""


                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim sdaSol As New SqlDataAdapter
                        Dim dsSol As New DataSet
                        Dim query As String
                        query = "select ms_recursos.id_ms_recursos " +
                                "     , ms_recursos.empleado " +
                                "     , format(cast(cg_empleado.no_empleado as int), '0000000', 'es-MX') as no_empleado " +
                                "     , isnull(ms_anticipo.no_proveedor, '') as no_proveedor " +
                                "     , cg_empleado.correo " +
                                "     , case cg_empleado.puesto_tabulador " +
                                "         when 'AdmJef' then 'Administrativos y Jefaturas' " +
                                "         when 'GerDir' then 'Gerentes y Directivos' " +
                                "         when 'Cho' then 'Choferes' " +
                                "         when 'Mec' then 'Mecánicos' " +
                                "         when 'DirGral' then 'Director General' " +
                                "       end as puesto_tab " +
                                "     , ms_recursos.autorizador " +
                                "     , ms_recursos.destino " +
                                "     , isnull(ms_recursos.lugar_orig, '') as lugar_orig " +
                                "     , isnull(ms_recursos.lugar_dest, '') as lugar_dest " +
                                "     , mov_locales " +
                                "     , ms_recursos.empresa " +
                                "     , ms_recursos.actividad as just " +
                                "     , convert(varchar, ms_recursos.periodo_ini, 103) as periodo_ini " +
                                "     , convert(varchar, ms_recursos.periodo_fin, 103) as periodo_fin " +
                                "     , ms_recursos.tipo_transporte " +
                                "     , ms_recursos.incluye_anticipo " +
                                "     , ms_anticipo.id_ms_anticipo " +
                                "     , isnull(dias_hospedaje, 0) as dias_hospedaje " +
                                "     , monto_hospedaje " +
                                "     , isnull(dias_alimentos, 0) as dias_alimentos " +
                                "     , monto_alimentos " +
                                "     , isnull(dias_casetas, 0) as dias_casetas " +
                                "     , monto_casetas " +
                                "     , isnull(dias_otros, 0) as dias_otros " +
                                "     , monto_otros " +
                                "     , isnull(otros_especifico, 'XX') as otros_especifico " +
                                "     , case tipo_pago when 'E' then 'Efectivo' when 'T' then 'Transferencia' else '-' end as tipo_pago " +
                                "     , monto_hospedaje + monto_alimentos + monto_casetas + monto_otros as importe " +
                                "     , ms_recursos.incluye_reserva " +
                                "     , ms_recursos.lugares_dispo " +
                                "     , ms_recursos.lugares_reque " +
                                "     , ms_recursos.id_ms_reserva " +
                                "     , ms_reserva.fecha_ini " +
                                "     , ms_reserva.fecha_fin " +
                                "     , ms_reserva.no_eco as no_eco_reserva " +
                                "     , ms_reserva.modelo as modelo_reserva " +
                                "     , ms_recursos.incluye_hist_util " +
                                "     , dt_hist_util.id_dt_hist_util " +
                                "     , dt_hist_util.periodo_ini as periodo_ini_util " +
                                "     , dt_hist_util.periodo_fin as periodo_fin_util " +
                                "     , dt_hist_util.no_eco as no_eco_util " +
                                "     , dt_hist_util.modelo as modelo_util " +
                                "     , isnull(format(dt_hist_util.km_actual, 'N0', 'es-MX'), '') as km_actual " +
                                "     , ms_recursos.incluye_comb " +
                                "     , ms_recursos.id_ms_comb " +
                                "     , ms_comb.no_eco as no_eco_comb " +
                                "     , ms_comb.no_tarjeta_edenred " +
                                "     , isnull(format(ms_comb.litros_comb, 'N0', 'es-MX'), '') as litros_comb " +
                                "     , isnull(format(ms_comb.importe_comb, 'C2', 'es-MX'), '') as importe_comb " +
                                "     , ms_recursos.incluye_avion " +
                                "     , ms_avion.id_ms_avion " +
                                "     , convert(varchar, ms_avion.fecha_nacimiento, 103) as fecha_nacimiento " +
                                "     , ms_avion.fecha_salida as fecha_salida " +
                                "     , ms_avion.fecha_regreso as fecha_regreso " +
                                "     , isnull(ms_avion.justificacion, '') as justificacion " +
                                "     , isnull(ms_recursos.centro_costo, '') as centro_costo " +
                                "     , ms_recursos.no_autorizador " +
                                "     , ms_recursos.id_usr_solicita " +
                                "from ms_instancia " +
                                "  left join ms_recursos on ms_instancia.id_ms_sol = ms_recursos.id_ms_recursos and ms_instancia.tipo = 'SR' " +
                                "  left join ms_anticipo on ms_recursos.id_ms_anticipo = ms_anticipo.id_ms_anticipo " +
                                "  left join ms_reserva on ms_recursos.id_ms_reserva = ms_reserva.id_ms_reserva " +
                                "  left join dt_hist_util on ms_recursos.id_dt_hist_util = dt_hist_util.id_dt_hist_util " +
                                "  left join ms_comb on ms_recursos.id_ms_comb = ms_comb.id_ms_comb " +
                                "  left join ms_avion on ms_recursos.id_ms_avion = ms_avion.id_ms_avion " +
                                "  left join cg_usuario on ms_recursos.id_usr_solicita = cg_usuario.id_usuario " +
                                "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " +
                                "where id_ms_instancia = @idMsInst "
                        sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@idMsInst", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()
                        .lblFolio.Text = dsSol.Tables(0).Rows(0).Item("id_ms_recursos").ToString()
                        .lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("empleado").ToString()
                        ._txtNoEmpleado.Text = dsSol.Tables(0).Rows(0).Item("no_empleado").ToString()
                        ._txtCorreo.Text = dsSol.Tables(0).Rows(0).Item("correo").ToString()
                        .lblNoProveedor.Text = dsSol.Tables(0).Rows(0).Item("no_proveedor").ToString()
                        If dsSol.Tables(0).Rows(0).Item("no_proveedor").ToString() = "" Then
                            .lbl_NoProveedor.Visible = False
                        Else
                            .lbl_NoProveedor.Visible = True
                        End If
                        _txtNo_Autorizador.Text = dsSol.Tables(0).Rows(0).Item("no_autorizador").ToString()
                        .lblAutorizador.Text = dsSol.Tables(0).Rows(0).Item("autorizador").ToString()
                        ._txtPuestoTab.Text = dsSol.Tables(0).Rows(0).Item("puesto_tab").ToString()
                        .lblOrig.Text = dsSol.Tables(0).Rows(0).Item("lugar_orig").ToString()
                        .lblDest.Text = dsSol.Tables(0).Rows(0).Item("lugar_dest").ToString()
                        .lblDestino.Text = dsSol.Tables(0).Rows(0).Item("destino").ToString()
                        If dsSol.Tables(0).Rows(0).Item("mov_locales").ToString = "S" Then
                            .cblMovLocales.Items(0).Selected = True
                        Else
                            .cblMovLocales.Items(0).Selected = False
                        End If
                        .lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                        .lblCC.Text = dsSol.Tables(0).Rows(0).Item("centro_costo").ToString()
                        .txtJust.Text = dsSol.Tables(0).Rows(0).Item("just").ToString()
                        .lblPeriodoIni.Text = dsSol.Tables(0).Rows(0).Item("periodo_ini").ToString()
                        .lblPeriodoFin.Text = dsSol.Tables(0).Rows(0).Item("periodo_fin").ToString()
                        .lblTipoTansp.Text = dsSol.Tables(0).Rows(0).Item("tipo_transporte").ToString()
                        'Anticipo
                        If dsSol.Tables(0).Rows(0).Item("incluye_anticipo").ToString() = "S" Then
                            .cblRecursos.Items(0).Selected = True
                            .pnlAnticipo.Visible = True

                            .lblFolioA.Text = dsSol.Tables(0).Rows(0).Item("id_ms_anticipo").ToString()
                            If Val(dsSol.Tables(0).Rows(0).Item("dias_hospedaje").ToString()) = 0 Then
                                .wneDiasH.Text = ""
                                .wceMontoH.Text = ""
                                .lbl_TipoHospedaje.Visible = False
                                .ddlTipoHospedaje.Visible = False
                            Else
                                .wneDiasH.Text = Val(dsSol.Tables(0).Rows(0).Item("dias_hospedaje").ToString())
                                .wceMontoH.Text = Val(dsSol.Tables(0).Rows(0).Item("monto_hospedaje").ToString())
                                .lbl_TipoHospedaje.Visible = True
                                .ddlTipoHospedaje.Visible = True

                                'Catálogos de Tipo de Hospedaje
                                Dim sdaTipoHosp As New SqlDataAdapter
                                Dim dsTipoHosp As New DataSet
                                sdaTipoHosp.SelectCommand = New SqlCommand("select id_tipo_hospedaje " +
                                                                           "     , tipo_hospedaje " +
                                                                           "from cg_tipo_hospedaje " +
                                                                           "where status = 'A' " +
                                                                           "order by tipo_hospedaje ", ConexionBD)
                                .ddlTipoHospedaje.DataSource = dsTipoHosp
                                .ddlTipoHospedaje.DataTextField = "tipo_hospedaje"
                                .ddlTipoHospedaje.DataValueField = "id_tipo_hospedaje"
                                ConexionBD.Open()
                                sdaTipoHosp.Fill(dsTipoHosp)
                                .ddlTipoHospedaje.DataBind()
                                ConexionBD.Close()
                                sdaTipoHosp.Dispose()
                                dsTipoHosp.Dispose()
                                .ddlTipoHospedaje.SelectedValue = 1
                            End If
                            If Val(dsSol.Tables(0).Rows(0).Item("dias_alimentos").ToString()) = 0 Then
                                .wneDiasA.Text = ""
                                .wceMontoA.Text = ""
                            Else
                                .wneDiasA.Text = Val(dsSol.Tables(0).Rows(0).Item("dias_alimentos").ToString())
                                .wceMontoA.Text = Val(dsSol.Tables(0).Rows(0).Item("monto_alimentos").ToString())
                            End If
                            If Val(dsSol.Tables(0).Rows(0).Item("dias_casetas").ToString()) = 0 Then
                                .wneDiasC.Text = ""
                                .wceMontoC.Text = ""
                            Else
                                .wneDiasC.Text = Val(dsSol.Tables(0).Rows(0).Item("dias_casetas").ToString())
                                .wceMontoC.Text = Val(dsSol.Tables(0).Rows(0).Item("monto_casetas").ToString())
                            End If
                            If Val(dsSol.Tables(0).Rows(0).Item("dias_otros").ToString()) = 0 Then
                                .wneDiasO.Text = ""
                                .wceMontoO.Text = ""
                                .lblOtros.Text = ""
                            Else
                                .wneDiasO.Text = Val(dsSol.Tables(0).Rows(0).Item("dias_otros").ToString())
                                .wceMontoO.Text = Val(dsSol.Tables(0).Rows(0).Item("monto_otros").ToString())
                                .lblOtros.Text = dsSol.Tables(0).Rows(0).Item("otros_especifico").ToString()
                            End If
                            .lblTipoPago.Text = dsSol.Tables(0).Rows(0).Item("tipo_pago").ToString()
                            .wceMontoT.Text = Val(dsSol.Tables(0).Rows(0).Item("importe").ToString())
                            .lblMontoTLetra.Text = "(" + montoLetra() + ")"
                        Else
                            .cblRecursos.Items(0).Selected = False
                            .pnlAnticipo.Visible = False
                        End If
                        .upAnticipo.Update()
                        'Reserva / Hist. Utilitarios
                        If dsSol.Tables(0).Rows(0).Item("incluye_reserva").ToString() = "S" Or dsSol.Tables(0).Rows(0).Item("incluye_hist_util").ToString() = "S" Then
                            .cblRecursos.Items(1).Selected = True
                            .pnlVehiculo.Visible = True

                            .lblLugaresDisp.Text = dsSol.Tables(0).Rows(0).Item("lugares_dispo").ToString()
                            .lblLugaresRequ.Text = dsSol.Tables(0).Rows(0).Item("lugares_reque").ToString()
                            If dsSol.Tables(0).Rows(0).Item("incluye_reserva").ToString() = "S" Then
                                'ms_reserva
                                .lbl_FolioV.Visible = True
                                .lblFolioV.Visible = True
                                .lblFolioV.Text = dsSol.Tables(0).Rows(0).Item("id_ms_reserva").ToString()
                                .lblVehiculo.Text = dsSol.Tables(0).Rows(0).Item("no_eco_reserva").ToString()
                                .lblModelo.Text = dsSol.Tables(0).Rows(0).Item("modelo_reserva").ToString()
                                .lblFechaIni.Text = dsSol.Tables(0).Rows(0).Item("fecha_ini").ToString()
                                .lblFechaFin.Text = dsSol.Tables(0).Rows(0).Item("fecha_fin").ToString()
                                .lblKmsActual.Text = "" 'queda vacío porque no aplica
                                .pnlKmActual.Visible = False
                            Else
                                'dt_hist_util
                                .lbl_FolioV.Visible = False 'No aplica
                                .lblFolioV.Visible = False
                                .lblFolioV.Text = dsSol.Tables(0).Rows(0).Item("id_dt_hist_util").ToString()
                                .lblVehiculo.Text = dsSol.Tables(0).Rows(0).Item("no_eco_util").ToString()
                                .lblModelo.Text = dsSol.Tables(0).Rows(0).Item("modelo_util").ToString()
                                .lblFechaIni.Text = dsSol.Tables(0).Rows(0).Item("periodo_ini_util").ToString()
                                .lblFechaFin.Text = dsSol.Tables(0).Rows(0).Item("periodo_fin_util").ToString()
                                .lblKmsActual.Text = dsSol.Tables(0).Rows(0).Item("km_actual").ToString()
                                .pnlKmActual.Visible = True
                            End If
                        Else
                            .cblRecursos.Items(1).Selected = False
                            .pnlVehiculo.Visible = False
                        End If
                        .upVehiculo.Update()
                        .upKmActual.Update()
                        'Combustible
                        If dsSol.Tables(0).Rows(0).Item("incluye_comb").ToString() = "S" Then
                            .cblRecursos.Items(2).Selected = True
                            .pnlCombustible.Visible = True
                            .lblFolioC.Text = dsSol.Tables(0).Rows(0).Item("id_ms_comb").ToString()
                            .lblVehiculoC.Text = dsSol.Tables(0).Rows(0).Item("no_eco_comb").ToString()
                            .lblTarjEdenred.Text = dsSol.Tables(0).Rows(0).Item("no_tarjeta_edenred").ToString()
                            .lblLitros.Text = dsSol.Tables(0).Rows(0).Item("litros_comb").ToString()
                            .lblImporte.Text = dsSol.Tables(0).Rows(0).Item("importe_comb").ToString()
                        Else
                            .cblRecursos.Items(2).Selected = False
                            .pnlCombustible.Visible = False
                        End If
                        .upCombustible.Update()
                        'Avión
                        If dsSol.Tables(0).Rows(0).Item("incluye_avion").ToString() = "S" Then
                            .cblRecursos.Items(3).Selected = True
                            .pnlAvion.Visible = True
                            .lblFolioAv.Text = dsSol.Tables(0).Rows(0).Item("id_ms_avion").ToString()
                            .lblFechaNac.Text = dsSol.Tables(0).Rows(0).Item("fecha_nacimiento").ToString()
                            .lblFechaSalida.Text = dsSol.Tables(0).Rows(0).Item("fecha_salida").ToString()
                            .lblFechaRegreso.Text = dsSol.Tables(0).Rows(0).Item("fecha_regreso").ToString()
                            .lblJustAv.Text = dsSol.Tables(0).Rows(0).Item("justificacion").ToString()
                            _txtIdUsuarioSolicita.Text = dsSol.Tables(0).Rows(0).Item("id_usr_solicita").ToString
                            pnlAmex.Visible = True
                            If .lblJustAv.Text = "" Then
                                .pnlJust.Visible = False
                            Else
                                .pnlJust.Visible = True
                            End If
                        Else
                            .cblRecursos.Items(3).Selected = False
                            .pnlAvion.Visible = False
                        End If
                        .upAvion.Update()

                        sdaSol.Dispose()
                        dsSol.Dispose()

                        'Panel
                        .pnlInicio.Visible = True
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

    Public Function montoLetra()
        Dim resto As Double = 0
        Dim temp As String = ""
        Dim op As Integer
        resto = Me.wceMontoT.Value

        If resto >= 100000.0 Then
            op = Val(CStr(resto).Substring(0, 1))
            resto = resto - CDbl(op * 100000.0)
            Select Case op
                Case 1
                    If resto < 1000.0 Then
                        temp = temp + " Cien"
                    Else
                        temp = temp + " Ciento"
                    End If
                Case 2
                    temp = temp + " Doscientos"
                Case 3
                    temp = temp + " Trescientos"
                Case 4
                    temp = temp + " Cuatrocientos"
                Case 5
                    temp = temp + " Quinientos"
                Case 6
                    temp = temp + " Seiscientos"
                Case 7
                    temp = temp + " Setecientos"
                Case 8
                    temp = temp + " Ochocientos"
                Case 9
                    temp = temp + " Novecientos"
            End Select
        End If

        If resto >= 10000.0 Then
            op = Val(CStr(resto).Substring(0, 1))
            resto = resto - CDbl(op * 10000.0)
            Select Case op
                Case 1
                    If resto < 1000 Then
                        temp = temp + " Diez"
                    Else
                        op = Val(CStr(resto).Substring(0, 1))
                        resto = resto - (op * 1000.0)
                        Select Case op
                            Case 1
                                temp = temp + " Once"
                            Case 2
                                temp = temp + " Doce"
                            Case 3
                                temp = temp + " Trece"
                            Case 4
                                temp = temp + " Catorce"
                            Case 5
                                temp = temp + " Quince"
                            Case 6
                                temp = temp + " Dieciséis"
                            Case 7
                                temp = temp + " Diecisiete"
                            Case 8
                                temp = temp + " Dieciocho"
                            Case 9
                                temp = temp + " Diecinueve"
                        End Select
                    End If
                Case 2
                    If resto < 1000.0 Then
                        temp = temp + " Veinte"
                    Else
                        op = Val(CStr(resto).Substring(0, 1))
                        resto = resto - CDbl(op * 1000.0)
                        Select Case op
                            Case 1
                                temp = temp + " Veintiuno"
                            Case 2
                                temp = temp + " Veintidós"
                            Case 3
                                temp = temp + " Veintitrés"
                            Case 4
                                temp = temp + " Veinticuatro"
                            Case 5
                                temp = temp + " Veinticinco"
                            Case 6
                                temp = temp + " Veintiséis"
                            Case 7
                                temp = temp + " Veintisiete"
                            Case 8
                                temp = temp + " Veintiocho"
                            Case 9
                                temp = temp + " Veintinueve"
                        End Select
                    End If
                Case 3
                    If resto < 1000.0 Then
                        temp = temp + " Treinta"
                    Else
                        temp = temp + " Treinta y"
                    End If
                Case 4
                    If resto < 1000.0 Then
                        temp = temp + " Cuarenta"
                    Else
                        temp = temp + " Cuarenta y"
                    End If
                Case 5
                    If resto < 1000.0 Then
                        temp = temp + " Cincuenta"
                    Else
                        temp = temp + " Cincuenta y"
                    End If
                Case 6
                    If resto < 1000.0 Then
                        temp = temp + " Sesenta"
                    Else
                        temp = temp + " Sesenta y"
                    End If
                Case 7
                    If resto < 1000.0 Then
                        temp = temp + " Setenta"
                    Else
                        temp = temp + " Setenta y"
                    End If
                Case 8
                    If resto < 1000.0 Then
                        temp = temp + " Ochenta"
                    Else
                        temp = temp + " Ochenta y"
                    End If
                Case 9
                    If resto < 1000.0 Then
                        temp = temp + " Noventa"
                    Else
                        temp = temp + " Noventa y"
                    End If
            End Select
        End If

        If resto >= 1000.0 Then
            op = Val(CStr(resto).Substring(0, 1))
            resto = resto - CDbl(op * 1000.0)
            Select Case op
                Case 1
                    temp = temp + " Un"
                Case 2
                    temp = temp + " Dos"
                Case 3
                    temp = temp + " Tres"
                Case 4
                    temp = temp + " Cuatro"
                Case 5
                    temp = temp + " Cinco"
                Case 6
                    temp = temp + " Seis"
                Case 7
                    temp = temp + " Siete"
                Case 8
                    temp = temp + " Ocho"
                Case 9
                    temp = temp + " Nueve"
            End Select
        End If
        If temp <> "" Then
            temp = temp + " Mil"
        End If

        If resto >= 100.0 Then
            op = Val(CStr(resto).Substring(0, 1))
            resto = resto - CDbl(op * 100.0)
            Select Case op
                Case 1
                    If resto < 1.0 Then
                        temp = temp + " Cien"
                    Else
                        temp = temp + " Ciento"
                    End If
                Case 2
                    temp = temp + " Doscientos"
                Case 3
                    temp = temp + " Trescientos"
                Case 4
                    temp = temp + " Cuatrocientos"
                Case 5
                    temp = temp + " Quinientos"
                Case 6
                    temp = temp + " Seiscientos"
                Case 7
                    temp = temp + " Setecientos"
                Case 8
                    temp = temp + " Ochocientos"
                Case 9
                    temp = temp + " Novecientos"
            End Select
        End If

        If resto >= 10.0 Then
            op = Val(CStr(resto).Substring(0, 1))
            resto = resto - CDbl(op * 10.0)
            Select Case op
                Case 1
                    If resto < 1 Then
                        temp = temp + " Diez"
                    Else
                        op = Val(CStr(resto).Substring(0, 1))
                        resto = resto - CDbl(op)
                        Select Case op
                            Case 1
                                temp = temp + " Once"
                            Case 2
                                temp = temp + " Doce"
                            Case 3
                                temp = temp + " Trece"
                            Case 4
                                temp = temp + " Catorce"
                            Case 5
                                temp = temp + " Quince"
                            Case 6
                                temp = temp + " Dieciséis"
                            Case 7
                                temp = temp + " Diecisiete"
                            Case 8
                                temp = temp + " Dieciocho"
                            Case 9
                                temp = temp + " Diecinueve"
                        End Select
                    End If
                Case 2
                    If resto < 1.0 Then
                        temp = temp + " Veinte"
                    Else
                        op = Val(CStr(resto).Substring(0, 1))
                        resto = resto - CDbl(op)
                        Select Case op
                            Case 1
                                temp = temp + " Veintiuno"
                            Case 2
                                temp = temp + " Veintidós"
                            Case 3
                                temp = temp + " Veintitrés"
                            Case 4
                                temp = temp + " Veinticuatro"
                            Case 5
                                temp = temp + " Veinticinco"
                            Case 6
                                temp = temp + " Veintiséis"
                            Case 7
                                temp = temp + " Veintisiete"
                            Case 8
                                temp = temp + " Veintiocho"
                            Case 9
                                temp = temp + " Veintinueve"
                        End Select
                    End If
                Case 3
                    If resto < 1.0 Then
                        temp = temp + " Treinta"
                    Else
                        temp = temp + " Treinta y"
                    End If
                Case 4
                    If resto < 1.0 Then
                        temp = temp + " Cuarenta"
                    Else
                        temp = temp + " Cuarenta y"
                    End If
                Case 5
                    If resto < 1.0 Then
                        temp = temp + " Cincuenta"
                    Else
                        temp = temp + " Cincuenta y"
                    End If
                Case 6
                    If resto < 1.0 Then
                        temp = temp + " Sesenta"
                    Else
                        temp = temp + " Sesenta y"
                    End If
                Case 7
                    If resto < 1.0 Then
                        temp = temp + " Setenta"
                    Else
                        temp = temp + " Setenta y"
                    End If
                Case 8
                    If resto < 1.0 Then
                        temp = temp + " Ochenta"
                    Else
                        temp = temp + " Ochenta y"
                    End If
                Case 9
                    If resto < 1.0 Then
                        temp = temp + " Noventa"
                    Else
                        temp = temp + " Noventa y"
                    End If
            End Select
        End If

        If resto >= 1.0 Then
            op = Val(CStr(resto).Substring(0, 1))
            resto = resto - CDbl(op)
            Select Case op
                Case 1
                    temp = temp + " Un"
                Case 2
                    temp = temp + " Dos"
                Case 3
                    temp = temp + " Tres"
                Case 4
                    temp = temp + " Cuatro"
                Case 5
                    temp = temp + " Cinco"
                Case 6
                    temp = temp + " Seis"
                Case 7
                    temp = temp + " Siete"
                Case 8
                    temp = temp + " Ocho"
                Case 9
                    temp = temp + " Nueve"
            End Select
        End If

        If temp <> "" Then
            If Val(Me.wceMontoT.Value) = 1 And op = 1 Then
                temp = temp + " peso"
            Else
                temp = temp + " pesos"
            End If
        End If

        If resto = 0.0 Then
            temp = temp + " 00"
        Else
            If CInt(resto * 100).ToString() < 10 Then
                temp = temp + " 0" + CInt(resto * 100).ToString()
            Else
                temp = temp + " " + CInt(resto * 100).ToString()
            End If
        End If
        temp = temp + "/100 M.N. "
        temp = temp.ToUpper()
        Return temp
    End Function

    Public Sub llenarAnticipo()
        Try
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")

            'Insertar Anticipo'
            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
            SCMValores.Connection = ConexionBD
            SCMValores.Parameters.Clear()
            SCMValores.CommandText = "insert into ms_anticipo ( id_usr_solicita,  fecha_solicita,  id_usr_autoriza,  empresa,  no_empleado,  no_proveedor,  empleado,  no_autorizador,  autorizador,  destino,  actividad,  periodo_comp,  periodo_ini,  periodo_fin,  tipo_pago,  no_personas,  dias_hospedaje,  monto_hospedaje,  dias_alimentos,  monto_alimentos,  dias_casetas,  monto_casetas,  dias_otros,  monto_otros,  otros_especifico, status, tipo, codigo_reservacion) " +
                                                         " 			       values (@id_usr_solicita, @fecha_solicita, @id_usr_autoriza, @empresa, @no_empleado, @no_proveedor, @empleado, @no_autorizador, @autorizador, @destino, @actividad, @periodo_comp, @periodo_ini, @periodo_fin, @tipo_pago, @no_personas, @dias_hospedaje, @monto_hospedaje, @dias_alimentos, @monto_alimentos, @dias_casetas, @monto_casetas, @dias_otros, @monto_otros, @otros_especifico,'TR', 'AAE', @codigo_reservacion)"
            SCMValores.Parameters.AddWithValue("@id_usr_solicita", Val(_txtIdUsuarioSolicita.Text))
            SCMValores.Parameters.AddWithValue("@fecha_solicita", Date.Now)
            SCMValores.Parameters.AddWithValue("@id_usr_autoriza", DBNull.Value)
            SCMValores.Parameters.AddWithValue("@empresa", lblEmpresa.Text)
            SCMValores.Parameters.AddWithValue("@no_empleado", _txtNoEmpleado.Text)
            SCMValores.Parameters.AddWithValue("@no_proveedor", lblNoProveedor.Text)
            SCMValores.Parameters.AddWithValue("@empleado", lblSolicitante.Text)
            SCMValores.Parameters.AddWithValue("@no_autorizador", _txtNo_Autorizador.Text)
            SCMValores.Parameters.AddWithValue("@autorizador", lblAutorizador.Text)
            SCMValores.Parameters.AddWithValue("@destino", lblDestino.Text)
            SCMValores.Parameters.AddWithValue("@actividad", txtJust.Text)
            SCMValores.Parameters.AddWithValue("@periodo_comp", "Del " + lblPeriodoIni.Text + " al " + lblPeriodoFin.Text)
            SCMValores.Parameters.AddWithValue("@periodo_ini", lblPeriodoIni.Text)
            SCMValores.Parameters.AddWithValue("@periodo_fin", lblPeriodoFin.Text)
            SCMValores.Parameters.AddWithValue("@tipo_pago", "T")
            SCMValores.Parameters.AddWithValue("@no_personas", 1)
            SCMValores.Parameters.AddWithValue("@dias_hospedaje", DBNull.Value)
            SCMValores.Parameters.AddWithValue("@monto_hospedaje", 0)
            SCMValores.Parameters.AddWithValue("@dias_alimentos", DBNull.Value)
            SCMValores.Parameters.AddWithValue("@monto_alimentos", 0)
            SCMValores.Parameters.AddWithValue("@dias_casetas", DBNull.Value)
            SCMValores.Parameters.AddWithValue("@monto_casetas", 0)
            SCMValores.Parameters.AddWithValue("@dias_otros", DBNull.Value)
            SCMValores.Parameters.AddWithValue("@monto_otros", Val(txtImporte.Text))
            SCMValores.Parameters.AddWithValue("@otros_especifico", "Boletos de avión American Express")
            SCMValores.Parameters.AddWithValue("@codigo_reservacion", txtCodigoReservacion.Text)
            ConexionBD.Open()
            SCMValores.ExecuteNonQuery()
            ConexionBD.Close()
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "Autorizar / Rechazar"

    Protected Sub btnAutoriza_Click(sender As Object, e As EventArgs) Handles btnAutoriza.Click
        With Me
            Try
                .litError.Text = ""
                If pnlAmex.Visible = True And txtCodigoReservacion.Text <> "" And Val(txtImporte.Text) <> 0 Then
                    If Session("Error") = "" Then
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        Dim fecha As DateTime = Date.Now
                        While Val(._txtBan.Text) = 0
                            'Actualizar datos de la Solicitud de Recursos
                            Dim valor As Integer = 0
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "DECLARE @valorR int;  Execute Sp_U_VoBoSolRecu  @id_usr_vobo, @fecha_vobo,  @comentario_vobo, @id_ms_recursos,  @id_actividad, @id_ms_instancia,  @montoPgvEp, @valorR OUTPUT; select @valorR"
                            SCMValores.Parameters.AddWithValue("@id_usr_vobo", Val(._txtIdUsuario.Text))
                            SCMValores.Parameters.AddWithValue("@fecha_vobo", fecha)
                            If .txtComentario.Text.Trim <> "" Then
                                SCMValores.Parameters.AddWithValue("@comentario_vobo", .txtComentario.Text.Trim)
                            Else
                                SCMValores.Parameters.AddWithValue("@comentario_vobo", DBNull.Value)
                            End If
                            SCMValores.Parameters.AddWithValue("@id_ms_recursos", Val(.lblFolio.Text))
                            SCMValores.Parameters.AddWithValue("@id_actividad", 39)
                            SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                            SCMValores.Parameters.AddWithValue("@montoPgvEp", 0)
                            ConexionBD.Open()
                            valor = SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            If valor = 0 Then
                                Server.Transfer("Menu.aspx")
                            End If
                            SCMValores.Parameters.Clear()
                            ''Actualizar datos de la Solicitud de Recursos
                            'SCMValores.CommandText = ""
                            'SCMValores.Parameters.Clear()
                            'SCMValores.CommandText = "update ms_recursos set id_usr_vobo = @id_usr_vobo, fecha_vobo = @fecha_vobo, comentario_vobo = @comentario_vobo, status = 'A' where id_ms_recursos = @id_ms_recursos "
                            'SCMValores.Parameters.AddWithValue("@id_usr_vobo", Val(._txtIdUsuario.Text))
                            'SCMValores.Parameters.AddWithValue("@fecha_vobo", fecha)
                            'If .txtComentario.Text.Trim <> "" Then
                            '    SCMValores.Parameters.AddWithValue("@comentario_vobo", .txtComentario.Text.Trim)
                            'Else
                            '    SCMValores.Parameters.AddWithValue("@comentario_vobo", DBNull.Value)
                            'End If
                            'SCMValores.Parameters.AddWithValue("@id_ms_recursos", Val(.lblFolio.Text))
                            'ConexionBD.Open()
                            'SCMValores.ExecuteNonQuery()
                            'ConexionBD.Close()

                            ''Actualizar Instancia
                            'SCMValores.Parameters.Clear()
                            'SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                            'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                            'SCMValores.Parameters.AddWithValue("@id_actividad", 39)
                            'ConexionBD.Open()
                            'SCMValores.ExecuteNonQuery()
                            'ConexionBD.Close()

                            ''Registrar en Histórico
                            'SCMValores.Parameters.Clear()
                            'SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                            '                         "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                            'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                            'SCMValores.Parameters.AddWithValue("@id_actividad", 39)
                            'SCMValores.Parameters.AddWithValue("@fecha", fecha)
                            'SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                            'ConexionBD.Open()
                            'SCMValores.ExecuteNonQuery()
                            'ConexionBD.Close()

                            ._txtBan.Text = 1

                            'Generar Instancias de acuerdo a los recursos solicitados
                            ' * * * Anticipo * * * 
                            If .cblRecursos.Items(0).Selected = True Then
                                'Actualizar datos del Anticipo
                                SCMValores.CommandText = ""
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "update ms_anticipo set tipo_hospedaje = @tipo_hospedaje, fecha_autoriza = @fecha_autoriza, status = 'A' where id_ms_anticipo = @id_ms_anticipo "
                                If .ddlTipoHospedaje.Visible = True Then
                                    SCMValores.Parameters.AddWithValue("@tipo_hospedaje", .ddlTipoHospedaje.SelectedItem.Text)
                                Else
                                    SCMValores.Parameters.AddWithValue("@tipo_hospedaje", DBNull.Value)
                                End If
                                SCMValores.Parameters.AddWithValue("@fecha_autoriza", fecha)
                                SCMValores.Parameters.AddWithValue("@id_ms_anticipo", Val(.lblFolioA.Text))
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()

                                'Crear Instancia del Anticipo
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "insert into ms_instancia ( id_ms_sol,  tipo,  id_actividad) " +
                                                         "				    values (@id_ms_sol, @tipo, @id_actividad) "
                                SCMValores.Parameters.AddWithValue("@id_ms_sol", Val(.lblFolioA.Text))
                                SCMValores.Parameters.AddWithValue("@tipo", "A")
                                If .lblTipoPago.Text = "Transferencia" Then
                                    SCMValores.Parameters.AddWithValue("@id_actividad", 3)
                                Else
                                    SCMValores.Parameters.AddWithValue("@id_actividad", 5)
                                End If
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()
                                'Obtener ID de la Instancia del Anticipo
                                SCMValores.CommandText = "select max(id_ms_instancia) from ms_instancia where id_ms_sol = @id_ms_sol and tipo = 'A' "
                                ConexionBD.Open()
                                ._txtIdMsInstA.Text = SCMValores.ExecuteScalar
                                ConexionBD.Close()
                                'Insertar Históricos
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "insert into ms_historico ( id_ms_instancia,  id_actividad,  id_usr,  fecha) " +
                                                         "				    values (@id_ms_instancia, @id_actividad, @id_usr, @fecha) "
                                SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInstA.Text))
                                If .lblTipoPago.Text = "Transferencia" Then
                                    SCMValores.Parameters.AddWithValue("@id_actividad", 3)
                                Else
                                    SCMValores.Parameters.AddWithValue("@id_actividad", 5)
                                End If
                                SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                                SCMValores.Parameters.AddWithValue("@fecha", fecha)
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()

                                If .lblTipoPago.Text = "Efectivo" Then
                                    'Envío de Correo
                                    Dim Mensaje As New System.Net.Mail.MailMessage()
                                    Dim destinatario As String = ""
                                    'Obtener el Correo del Solicitante
                                    SCMValores.CommandText = "select cgEmpl.correo " +
                                                             "from ms_anticipo " +
                                                             "  left join cg_usuario on ms_anticipo.id_usr_solicita = cg_usuario.id_usuario " +
                                                             "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                             "where id_ms_anticipo = @id_ms_anticipo "
                                    SCMValores.Parameters.AddWithValue("@id_ms_anticipo", Val(.lblFolioA.Text))
                                    ConexionBD.Open()
                                    destinatario = SCMValores.ExecuteScalar()
                                    ConexionBD.Close()

                                    Mensaje.[To].Add(destinatario)
                                    Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                                    Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                                    Mensaje.Subject = "ProcAd - Anticipo No. " + .lblFolioA.Text + " Autorizado"
                                    Dim texto As String
                                    texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                                            "El anticipo número <b>" + .lblFolioA.Text + "</b> fue autorizado, favor de pasar por el efectivo. <br></span>"
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
                                End If
                            End If

                            ' * * * Vehículo * * * 
                            If .cblRecursos.Items(1).Selected = True Then
                                If .lbl_FolioV.Visible = True Then
                                    'ms_reserva
                                    SCMValores.CommandText = ""
                                    SCMValores.Parameters.Clear()
                                    SCMValores.CommandText = "update ms_reserva " +
                                                             "  set status = 'A', fecha_autorizo = @fecha " +
                                                             "where id_ms_reserva = @idMsReserv "
                                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                                    SCMValores.Parameters.AddWithValue("@idMsReserv", Val(.lblFolioV.Text))
                                    ConexionBD.Open()
                                    SCMValores.ExecuteNonQuery()
                                    ConexionBD.Close()

                                    'Envío de Correo
                                    Dim Mensaje As New System.Net.Mail.MailMessage()
                                    Dim destinatario As String = ""
                                    'Obtener el Correo del Solicitante
                                    SCMValores.CommandText = "select cgEmpl.correo " +
                                                             "from ms_reserva " +
                                                             "  left join cg_usuario on ms_reserva.id_usr_solicito = cg_usuario.id_usuario " +
                                                             "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                             "where id_ms_reserva = @id_ms_reserva "
                                    SCMValores.Parameters.AddWithValue("@id_ms_reserva", Val(.lblFolioV.Text))
                                    ConexionBD.Open()
                                    destinatario = SCMValores.ExecuteScalar()
                                    ConexionBD.Close()

                                    Mensaje.[To].Add(destinatario)
                                    Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                                    Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                                    Mensaje.Subject = "ProcAd - Reservación No. " + .lblFolioV.Text + " Autorizada"
                                    Dim texto As String
                                    texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                                            "La reservación número <b>" + .lblFolioV.Text + "</b> fue autorizada. <br></span>"
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
                                Else
                                    'dt_hist_util
                                    SCMValores.CommandText = ""
                                    SCMValores.Parameters.Clear()
                                    SCMValores.CommandText = "update dt_hist_util " +
                                                             "  set status = 'A', fecha_autoriza = @fecha_autoriza " +
                                                             "where id_dt_hist_util = @id_dt_hist_util "
                                    SCMValores.Parameters.AddWithValue("@fecha_autoriza", fecha)
                                    SCMValores.Parameters.AddWithValue("@id_dt_hist_util", Val(.lblFolioV.Text))
                                    ConexionBD.Open()
                                    SCMValores.ExecuteNonQuery()
                                    ConexionBD.Close()
                                End If
                            End If

                            ' * * * Combustible * * * 
                            If .cblRecursos.Items(2).Selected = True Then
                                'Actualizar datos de la Solicitud de Combustible
                                SCMValores.CommandText = ""
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "update ms_comb set fecha_autoriza = @fecha_autoriza, status = 'A' where id_ms_comb = @id_ms_comb "
                                SCMValores.Parameters.AddWithValue("@fecha_autoriza", fecha)
                                SCMValores.Parameters.AddWithValue("@id_ms_comb", Val(.lblFolioC.Text))
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()

                                'Crear Instancia 
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "insert into ms_instancia ( id_ms_sol,  tipo,  id_actividad) " +
                                                         "				    values (@id_ms_sol, @tipo, @id_actividad) "
                                SCMValores.Parameters.AddWithValue("@id_ms_sol", Val(.lblFolioC.Text))
                                SCMValores.Parameters.AddWithValue("@tipo", "Comb")
                                SCMValores.Parameters.AddWithValue("@id_actividad", 40)
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()
                                'Obtener ID de la Instancia
                                SCMValores.CommandText = "select max(id_ms_instancia) from ms_instancia where id_ms_sol = @id_ms_sol and tipo = 'Comb' "
                                ConexionBD.Open()
                                ._txtIdMsInstC.Text = SCMValores.ExecuteScalar
                                ConexionBD.Close()
                                'Insertar Históricos
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "insert into ms_historico ( id_ms_instancia,  id_actividad,  id_usr,  fecha) " +
                                                         "				    values (@id_ms_instancia, @id_actividad, @id_usr, @fecha) "
                                SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInstC.Text))
                                SCMValores.Parameters.AddWithValue("@id_actividad", 40)
                                SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                                SCMValores.Parameters.AddWithValue("@fecha", fecha)
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()
                            End If

                            ' * * * Avión * * * 
                            If .cblRecursos.Items(3).Selected = True Then
                                'Actualizar datos de la Solicitud de Reserva de Avión
                                SCMValores.CommandText = ""
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "update ms_avion set fecha_autoriza = @fecha_autoriza, status = 'A' where id_ms_avion = @id_ms_avion "
                                SCMValores.Parameters.AddWithValue("@fecha_autoriza", fecha)
                                SCMValores.Parameters.AddWithValue("@id_ms_avion", Val(.lblFolioAv.Text))
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()

                                llenarAnticipo()

                            End If

                            .btnAutoriza.Enabled = False
                            .btnRechaza.Enabled = False

                            'Generación de Encuestas
                            Dim sdaEncuestas As New SqlDataAdapter
                            Dim dsEncuestas As New DataSet
                            sdaEncuestas.SelectCommand = New SqlCommand("select NEWID() as ID, isnull(correo,'XX') as correo " +
                                                                        "from ms_recursos " +
                                                                        "  inner join cg_usuario on cg_usuario.id_usuario = ms_recursos.id_usr_solicita " +
                                                                        "  inner join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " +
                                                                        "where id_ms_recursos = @Folio ", ConexionBD)
                            sdaEncuestas.SelectCommand.Parameters.AddWithValue("@Folio", Val(.lblFolio.Text))
                            ConexionBD.Open()
                            sdaEncuestas.Fill(dsEncuestas)
                            ConexionBD.Close()

                            'Insertar en Tabla
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "insert into ms_encuesta( id_ms_recursos,  id,  fecha_crea) " +
                                                     "                 values(@id_ms_recursos, @id, @fecha_crea)"
                            SCMValores.Parameters.AddWithValue("@id_ms_recursos", Val(.lblFolio.Text))
                            SCMValores.Parameters.AddWithValue("@id", dsEncuestas.Tables(0).Rows(0).Item("ID").ToString())
                            SCMValores.Parameters.AddWithValue("@fecha_crea", fecha)
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            'Envío de Correo
                            If dsEncuestas.Tables(0).Rows(0).Item("correo").ToString() <> "XX" Then
                                Dim Mensaje As New System.Net.Mail.MailMessage()

                                Mensaje.[To].Add(dsEncuestas.Tables(0).Rows(0).Item("correo").ToString())
                                Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                                Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                                Mensaje.Subject = "ProcAd - Encuesta de Satisfacción [Solicitud de Recursos " + .lblFolio.Text + "]"
                                Mensaje.Body = "<span style=""font-family:Verdana; font-size: 10pt;"">" +
                                               "Buen día,<br><br>" +
                                               "Solicitamos tu apoyo para complementar la siguiente encuesta sobre la Solicitud de Recursos <b>" + .lblFolio.Text + "</b>" +
                                               "; a continuación se presenta el link... <br><br>" +
                                               "http://148.223.153.43/ProcAd/Encuesta.aspx?idMs=" + .lblFolio.Text + "&id=" + dsEncuestas.Tables(0).Rows(0).Item("ID").ToString() +
                                               "<br><br></span>" +
                                               "<span style=""font-family: Verdana; font-size: 9pt; color: #FF0000; "">" +
                                               "<br><br> <b>Nota: Favor de <u>no</u> contestar a este correo</b>" +
                                               "</span>"
                                ' Servidor
                                ' --  "http://148.223.153.43/ProcAd/Encuesta.aspx?idMs=" + .lblFolio.Text + "&id=" + dsEncuestas.Tables(0).Rows(0).Item("ID").ToString()
                                ' Local
                                ' --  "http://localhost:49786/Encuesta.aspx?idMs=" + .lblFolio.Text + "&id=" + dsEncuestas.Tables(0).Rows(0).Item("ID").ToString()
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
                                Mensaje.Dispose()
                                Servidor.Dispose()
                            End If
                            sdaEncuestas.Dispose()
                            dsEncuestas.Dispose()

                            Session("id_actividadM") = 59
                            Session("TipoM") = "SR"
                            Server.Transfer("Menu.aspx")
                        End While
                    Else
                        Server.Transfer("Menu.aspx")
                    End If
                Else
                    litError.Text = "Debe meter un Codigo de reservación y un importe"
                End If
            Catch ex As Exception
                If ex.Message = "Subproceso anulado." Then
                    Session("Error") = "S"
                End If
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnRechaza_Click(sender As Object, e As EventArgs) Handles btnRechaza.Click
        With Me
            Try
                .litError.Text = ""
                If Session("Error") = "" Then
                    If .txtComentario.Text.Trim = "" Then
                        .litError.Text = "Favor de ingresar los comentarios correspondientes"
                    Else
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        Dim fecha As DateTime = Date.Now
                        While Val(._txtBan.Text) = 0
                            'Actualizar datos de la Solicitud de Recursos

                            Dim valor As Integer = 0
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "DECLARE @valorR int;  Execute Sp_U_VoBoSolRecu  @id_usr_vobo, @fecha_vobo,  @comentario_vobo, @id_ms_recursos,  @id_actividad, @id_ms_instancia,  @monto_pgv_ep, @valorR OUTPUT; select @valorR"
                            SCMValores.Parameters.AddWithValue("@id_usr_vobo", Val(._txtIdUsuario.Text))
                            SCMValores.Parameters.AddWithValue("@fecha_vobo", fecha)
                            SCMValores.Parameters.AddWithValue("@comentario_vobo", .txtComentario.Text.Trim)
                            SCMValores.Parameters.AddWithValue("@id_ms_recursos", Val(.lblFolio.Text))
                            SCMValores.Parameters.AddWithValue("@id_actividad", 60)
                            SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                            If .cblRecursos.Items(0).Selected = True Then
                                SCMValores.Parameters.AddWithValue("@monto_pgv_ep", 0)
                            Else
                                SCMValores.Parameters.AddWithValue("@monto_pgv_ep", DBNull.Value)
                            End If
                            ConexionBD.Open()
                            valor = SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            If valor = 0 Then
                                Server.Transfer("Menu.aspx")
                            End If
                            SCMValores.Parameters.Clear()

                            'SCMValores.CommandText = ""
                            'SCMValores.Parameters.Clear()
                            'SCMValores.CommandText = "update ms_recursos set id_usr_vobo = @id_usr_vobo, fecha_vobo = @fecha_vobo, comentario_vobo = @comentario_vobo, monto_pgv_ep = @monto_pgv_ep, status = 'Z' where id_ms_recursos = @id_ms_recursos "
                            'SCMValores.Parameters.AddWithValue("@id_usr_vobo", Val(._txtIdUsuario.Text))
                            'SCMValores.Parameters.AddWithValue("@fecha_vobo", fecha)
                            'SCMValores.Parameters.AddWithValue("@comentario_vobo", .txtComentario.Text.Trim)
                            'If .cblRecursos.Items(0).Selected = True Then
                            '    SCMValores.Parameters.AddWithValue("@monto_pgv_ep", 0)
                            'Else
                            '    SCMValores.Parameters.AddWithValue("@monto_pgv_ep", DBNull.Value)
                            'End If
                            'SCMValores.Parameters.AddWithValue("@id_ms_recursos", Val(.lblFolio.Text))
                            'ConexionBD.Open()
                            'SCMValores.ExecuteNonQuery()
                            'ConexionBD.Close()

                            ''Actualizar Instancia
                            'SCMValores.Parameters.Clear()
                            'SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                            'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                            'SCMValores.Parameters.AddWithValue("@id_actividad", 60)
                            'ConexionBD.Open()
                            'SCMValores.ExecuteNonQuery()
                            'ConexionBD.Close()

                            ''Registrar en Histórico
                            'SCMValores.Parameters.Clear()
                            'SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                            '                         "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                            'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                            'SCMValores.Parameters.AddWithValue("@id_actividad", 60)
                            'SCMValores.Parameters.AddWithValue("@fecha", fecha)
                            'SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                            'ConexionBD.Open()
                            'SCMValores.ExecuteNonQuery()
                            'ConexionBD.Close()

                            ._txtBan.Text = 1

                            'Anticipo
                            If .cblRecursos.Items(0).Selected = True Then
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "update ms_anticipo set fecha_autoriza = @fecha_autoriza, status = 'Z' where id_ms_anticipo = @id_ms_anticipo "
                                SCMValores.Parameters.AddWithValue("@fecha_autoriza", fecha)
                                SCMValores.Parameters.AddWithValue("@id_ms_anticipo", Val(.lblFolioA.Text))
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()
                            End If

                            'Vehículo
                            If .cblRecursos.Items(1).Selected = True Then
                                If .lbl_FolioV.Visible = True Then
                                    'ms_reserva
                                    SCMValores.Parameters.Clear()
                                    SCMValores.CommandText = "update ms_reserva set status = 'Z', fecha_autorizo = @fecha where id_ms_reserva = @idMsReserv "
                                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                                    SCMValores.Parameters.AddWithValue("@idMsReserv", Val(.lblFolioV.Text))
                                    ConexionBD.Open()
                                    SCMValores.ExecuteNonQuery()
                                    ConexionBD.Close()
                                Else
                                    'dt_hist_util
                                    SCMValores.Parameters.Clear()
                                    SCMValores.CommandText = "update dt_hist_util set status = 'Z', fecha_autoriza = @fecha_autoriza where id_dt_hist_util = @id_dt_hist_util "
                                    SCMValores.Parameters.AddWithValue("@fecha_autoriza", fecha)
                                    SCMValores.Parameters.AddWithValue("@id_dt_hist_util", Val(.lblFolioV.Text))
                                    ConexionBD.Open()
                                    SCMValores.ExecuteNonQuery()
                                    ConexionBD.Close()
                                End If
                            End If

                            'Combustible
                            If .cblRecursos.Items(2).Selected = True Then
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "update ms_comb set status = 'Z', fecha_autoriza = @fecha_autoriza where id_ms_comb = @id_ms_comb "
                                SCMValores.Parameters.AddWithValue("@fecha_autoriza", fecha)
                                SCMValores.Parameters.AddWithValue("@id_ms_comb", Val(.lblFolioC.Text))
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()
                            End If

                            'Avión
                            If .cblRecursos.Items(3).Selected = True Then
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "update ms_avion set status = 'Z', fecha_autoriza = @fecha_autoriza where id_ms_avion = @id_ms_avion "
                                SCMValores.Parameters.AddWithValue("@fecha_autoriza", fecha)
                                SCMValores.Parameters.AddWithValue("@id_ms_avion", Val(.lblFolioAv.Text))
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()
                            End If

                            'Envío de Correo
                            Dim Mensaje As New System.Net.Mail.MailMessage()
                            Dim destinatario As String = ""
                            'Obtener el Correo del Solicitante
                            SCMValores.CommandText = "select cgEmpl.correo " +
                                                     "from ms_recursos " +
                                                     "  left join cg_usuario on ms_recursos.id_usr_solicita = cg_usuario.id_usuario " +
                                                     "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                     "where id_ms_recursos = @id_ms_recursos "
                            SCMValores.Parameters.AddWithValue("@id_ms_recursos", Val(.lblFolio.Text))
                            ConexionBD.Open()
                            destinatario = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            Mensaje.[To].Add(destinatario)
                            Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                            Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                            Mensaje.Subject = "ProcAd - Solicitud de Recursos No. " + .lblFolio.Text + " Rechazada"
                            Dim texto As String
                            texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                                    "La solicitud número <b>" + .lblFolio.Text +
                                    "</b> no fue autorizada. <br></span>"
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

                            .btnAutoriza.Enabled = False
                            .btnRechaza.Enabled = False

                            Session("id_actividadM") = 59
                            Session("TipoM") = "SR"
                            Server.Transfer("Menu.aspx")
                        End While
                    End If
                Else
                    Server.Transfer("Menu.aspx")
                End If

            Catch ex As Exception
                If ex.Message = "Subproceso anulado." Then
                    Session("Error") = "S"
                End If
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class