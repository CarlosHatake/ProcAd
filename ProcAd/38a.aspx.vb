Public Class _38a
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
                                "     , isnull(ms_recursos.director, 'N') as director" +
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
                                "     , isnull(ms_recursos.id_cc, 0) as id_cc " +
                                "     , isnull(ms_recursos.año_pgv, 0) as año_pgv " +
                                "     , isnull(ms_recursos.mes_pgv, 0) as mes_pgv " +
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
                            Else
                                .wneDiasH.Text = Val(dsSol.Tables(0).Rows(0).Item("dias_hospedaje").ToString())
                                .wceMontoH.Text = Val(dsSol.Tables(0).Rows(0).Item("monto_hospedaje").ToString())
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

                        ._txtIdCC.Text = dsSol.Tables(0).Rows(0).Item("id_cc").ToString()
                        ._txtAñoPGV.Text = dsSol.Tables(0).Rows(0).Item("año_pgv").ToString()
                        ._txtMesPGV.Text = dsSol.Tables(0).Rows(0).Item("mes_pgv").ToString()

                        If dsSol.Tables(0).Rows(0).Item("director").ToString() = "N" Then
                            .lbl_Director.Visible = False
                            .lblDirector.Visible = False
                        Else
                            .lblDirector.Visible = True
                            .lbl_Director.Visible = True
                            .lblDirector.Text = dsSol.Tables(0).Rows(0).Item("director").ToString()

                        End If

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

#End Region

#Region "Autorizar / Rechazar"

    Protected Sub btnAutoriza_Click(sender As Object, e As EventArgs) Handles btnAutoriza.Click
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim fecha As DateTime = Date.Now
                .btnAutoriza.Enabled = False
                .btnRechaza.Enabled = False

                While Val(._txtBan.Text) = 0
                    'Validar si requiere Vo.Bo. del Administrador de Viajes
                    'Hospedaje / Reserva de Vehículo / Avión
                    ' * * * Versión Anterior -  Se omitirá para Vehículos [cblRecursos Item(1)] y Combustible * * * 06Nov22
                    'If (.cblRecursos.Items(0).Selected = True And .wceMontoH.Text <> "") Or .cblRecursos.Items(1).Selected = True Or .cblRecursos.Items(3).Selected = True Then
                    If (.cblRecursos.Items(0).Selected = True And .wceMontoH.Text <> "") Or .cblRecursos.Items(3).Selected = True Then
                        'Requiere Vo.Bo.
                        'Actualizar datos de la Solicitud de Recursos
                        Dim valor As Integer = 0
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "DECLARE @valorR int;  Execute Sp_U_SolRecDir  @id_ms_recursos, @id_actividad, @id_ms_instancia,  @id_usr,  @montoPgvEp, @valorR OUTPUT; select @valorR"
                        SCMValores.Parameters.AddWithValue("@id_ms_recursos", Val(lblFolio.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 59)
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(_txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@montoPgvEp", 0)
                        ConexionBD.Open()
                        valor = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        If valor = 0 Then
                            Server.Transfer("Menu.aspx")
                        End If
                        SCMValores.Parameters.Clear()

                        'SCMValores.CommandText = ""
                        'SCMValores.Parameters.Clear()
                        'SCMValores.CommandText = "update ms_recursos set fecha_autor_direc = @fecha_autoriza where id_ms_recursos = @id_ms_recursos "
                        'SCMValores.Parameters.AddWithValue("@fecha_autoriza", fecha)
                        'SCMValores.Parameters.AddWithValue("@id_ms_recursos", Val(.lblFolio.Text))
                        'ConexionBD.Open()
                        'SCMValores.ExecuteNonQuery()
                        'ConexionBD.Close()

                        ''Actualizar Instancia
                        'SCMValores.Parameters.Clear()
                        'SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia "
                        'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        'SCMValores.Parameters.AddWithValue("@id_actividad", 59)
                        'ConexionBD.Open()
                        'SCMValores.ExecuteNonQuery()
                        'ConexionBD.Close()

                        ''Registrar en Histórico
                        'SCMValores.Parameters.Clear()
                        'SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                        '                             "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                        'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        'SCMValores.Parameters.AddWithValue("@id_actividad", 59)
                        'SCMValores.Parameters.AddWithValue("@fecha", fecha)
                        'SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        'ConexionBD.Open()
                        'SCMValores.ExecuteNonQuery()
                        'ConexionBD.Close()

                        ._txtBan.Text = 1

                        'Datos del Empleado
                        Dim ConexionBDNom As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBDNom.ConnectionString = accessDB.conBD("NOM")

                        Dim sdaEmpleado As New SqlDataAdapter
                        Dim dsEmpleado As New DataSet
                        sdaEmpleado.SelectCommand = New SqlCommand("select rtrim(ltrim(isnull(nompues.despue,''))) as puesto " +
                                                                       "     , isnull(cc.desubi,'') as centro_costo " +
                                                                       "     , isnull(nompais.despai,'') as base " +
                                                                       "from nomtrab " +
                                                                       "  left join nompues on nomtrab.cvecia = nompues.cvecia and nomtrab.cvepue = nompues.cvepue " +
                                                                       "  left join nomposi on nomtrab.cvecia = nomposi.cvecia and nomtrab.cvepos = nomposi.cvepos " +
                                                                       "  left join nompais on nomposi.cvecia = nompais.cvecia and nomposi.cvepai = nompais.cvepai " +
                                                                       "  left join nomubic cc on nomposi.cvecia = cc.cvecia  and nomposi.cvepai = cc.cvepai and nomposi.cveciu = cc.cveciu and nomposi.cveubi = cc.cveubi " +
                                                                       "where nomtrab.status = 'A' " +
                                                                       "  and nomtrab.cvetra = @no_empleado ", ConexionBDNom)
                        sdaEmpleado.SelectCommand.Parameters.AddWithValue("@no_empleado", ._txtNoEmpleado.Text)
                        ConexionBDNom.Open()
                        sdaEmpleado.Fill(dsEmpleado)
                        ConexionBDNom.Close()

                        'Envío de Correo
                        Dim Mensaje As New System.Net.Mail.MailMessage()
                        Dim destinatario As String = ""
                        'Obtener el Correo del Coord. de Viajes
                        SCMValores.CommandText = "select valor " +
                                                     "from cg_parametros " +
                                                     "where parametro = 'mail_coord_viajes' "
                        ConexionBD.Open()
                        destinatario = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        Mensaje.[To].Add(destinatario)
                        Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                        Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                        Mensaje.Subject = "ProcAd - Solicitud de Recursos No. " + .lblFolio.Text + " Autorizada"
                        Dim texto As String
                        texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                                    "Se autorizó la solicitud a <b>" + .lblSolicitante.Text + "</b> <br>" +
                                    "Correo: <b>" + ._txtCorreo.Text + "</b> <br>" +
                                    "Puesto: <b>" + dsEmpleado.Tables(0).Rows(0).Item("puesto").ToString() + "</b> <br>" +
                                    "Centro de Costo: <b>" + dsEmpleado.Tables(0).Rows(0).Item("centro_costo").ToString() + "</b> <br>" +
                                    "Base: <b>" + dsEmpleado.Tables(0).Rows(0).Item("base").ToString() + "</b> <br>" +
                                    "Origen-Destino: <b>" + .lblOrig.Text + "</b> - <b>" + .lblDest.Text + "</b> <br>" +
                                    "Lugar: <b>" + .lblDestino.Text + "</b> <br>" +
                                    "Del <b>" + .lblPeriodoIni.Text + "</b> al <b>" + .lblPeriodoFin.Text + "</b><br><br>"
                        'Anticipo para Hospedaje
                        If .cblRecursos.Items(0).Selected = True And .wceMontoH.Text <> "" Then
                            texto = texto + "Anticipo para Hospedaje para <b>" + .wneDiasH.Value.ToString + "</b> noches por: <b>" + .wceMontoH.Text + "</b>, cabe mencionar que el puesto en el tabulador del empleado es <b>" + ._txtPuestoTab.Text + "</b><br><br>"
                        End If
                        'Vehículo
                        If .cblRecursos.Items(1).Selected = True Then
                            texto = texto + "Reserva del vehículo "
                            If .pnlKmActual.Visible = True Then
                                'Auto Asignado
                                texto = texto + "asignado "
                            Else
                                'Comodín
                                texto = texto + "comodín "
                            End If
                            texto = texto + "<b>" + .lblVehiculo.Text + "</b>, " +
                                                "del <b>" + .lblFechaIni.Text + "</b> al <b>" + .lblFechaFin.Text + "</b><br>" +
                                                "Lugares Requeridos: <b>" + .lblLugaresRequ.Text + "</b><br><br>"
                        End If
                        'Avión
                        If .cblRecursos.Items(3).Selected = True Then
                            texto = texto + "Reserva de Avión, saliendo el <b>" + .lblFechaSalida.Text + "</b> y regresando el <b>" + .lblFechaRegreso.Text + "</b><br>" +
                                                "Fecha Nacimiento: <b>" + .lblFechaNac.Text + "</b><br>"
                            If .pnlJust.Visible = True Then
                                texto = texto + "Justificación: <b>" + .lblJustAv.Text + "</b><br>"
                            End If
                            texto = texto + "<br>"
                        End If
                        texto = texto + "</span>"
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

                        sdaEmpleado.Dispose()
                        dsEmpleado.Dispose()
                    Else
                        'Se procesan las solicitudes
                        'Actualizar datos de la Solicitud de Recursos
                        Dim valor As Integer = 0
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "DECLARE @valorR int;  Execute Sp_U_SolRecDir  @id_ms_recursos,  @id_actividad, @id_ms_instancia,  @id_usr,  @montoPgvEp, @valorR OUTPUT; select @valorR"
                        SCMValores.Parameters.AddWithValue("@id_ms_recursos", Val(.lblFolio.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 39)
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@montoPgvEp", 0)
                        ConexionBD.Open()
                        valor = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        If valor = 0 Then
                            Server.Transfer("Menu.aspx")
                        End If
                        SCMValores.Parameters.Clear()

                        'SCMValores.CommandText = ""
                        '    SCMValores.Parameters.Clear()
                        'SCMValores.CommandText = "update ms_recursos set fecha_autor_direc = @fecha_autoriza, status = 'A' where id_ms_recursos = @id_ms_recursos "
                        'SCMValores.Parameters.AddWithValue("@fecha_autoriza", fecha)
                        'SCMValores.Parameters.AddWithValue("@id_ms_recursos", Val(.lblFolio.Text))
                        'ConexionBD.Open()
                        'SCMValores.ExecuteNonQuery()
                        'ConexionBD.Close()

                        ''Actualizar Instancia
                        'SCMValores.Parameters.Clear()
                        'SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia and tipo ='SR'"
                        'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        'SCMValores.Parameters.AddWithValue("@id_actividad", 39)
                        'ConexionBD.Open()
                        'SCMValores.ExecuteNonQuery()
                        'ConexionBD.Close()

                        ''Registrar en Histórico
                        'SCMValores.Parameters.Clear()
                        'SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                        '                             "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
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
                            SCMValores.CommandText = "update ms_anticipo set fecha_autoriza = @fecha_autoriza, status = 'A' where id_ms_anticipo = @id_ms_anticipo "
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
                        End If
                    End If

                    .btnAutoriza.Enabled = False
                    .btnRechaza.Enabled = False

                    Session("id_actividadM") = 38
                    Session("TipoM") = "SR"
                    Server.Transfer("Menu.aspx")
                End While



            Catch ex As Exception
                .btnAutoriza.Enabled = True
                .btnRechaza.Enabled = True
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnRechaza_Click(sender As Object, e As EventArgs) Handles btnRechaza.Click
        With Me
            Try
                .litError.Text = ""

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
                    SCMValores.CommandText = "DECLARE @valorR int;  Execute Sp_U_SolRecDir  @id_ms_recursos, @id_actividad, @id_ms_instancia,  @id_usr,  @monto_pgv_ep, @valorR OUTPUT; select @valorR"
                    If .cblRecursos.Items(0).Selected = True Then
                        SCMValores.Parameters.AddWithValue("@monto_pgv_ep", 0)
                    Else
                        SCMValores.Parameters.AddWithValue("@monto_pgv_ep", DBNull.Value)
                    End If
                    SCMValores.Parameters.AddWithValue("@id_ms_recursos", Val(.lblFolio.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 43)
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    valor = SCMValores.ExecuteScalar()
                    ConexionBD.Close()

                    If valor = 0 Then
                        Server.Transfer("Menu.aspx")
                    End If
                    SCMValores.Parameters.Clear()

                    'SCMValores.CommandText = ""
                    'SCMValores.Parameters.Clear()
                    'SCMValores.CommandText = "update ms_recursos set fecha_autor_direc = @fecha_autoriza, monto_pgv_ep = @monto_pgv_ep, status = 'Z' where id_ms_recursos = @id_ms_recursos "
                    'SCMValores.Parameters.AddWithValue("@fecha_autoriza", fecha)
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
                    'SCMValores.Parameters.AddWithValue("@id_actividad", 43)
                    'ConexionBD.Open()
                    'SCMValores.ExecuteNonQuery()
                    'ConexionBD.Close()

                    ''Registrar en Histórico
                    'SCMValores.Parameters.Clear()
                    'SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                    '                         "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    'SCMValores.Parameters.AddWithValue("@id_actividad", 43)
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

                        'Actualizar montos de presupuesto de Centro de Costo en caso de que corresponda
                        Dim sdaPGV As New SqlDataAdapter
                        Dim dsPGV As New DataSet
                        sdaPGV.SelectCommand = New SqlCommand("select (select isnull(sum(monto_pgv_ep), 0) as msAntEP " +
                                                              "        from ms_recursos " +
                                                              "        where id_cc = @idCC " +
                                                              "          and año_pgv = @año " +
                                                              "          and mes_pgv = @mes) " +
                                                              "      + " +
                                                              "       (select isnull(sum(monto_pgv_ex), 0) as msCompEx " +
                                                              "        from ms_comp " +
                                                              "        where id_cc = @idCC " +
                                                              "          and año_pgv = @año " +
                                                              "          and mes_pgv = @mes) as pgvEP " +
                                                              "     , (select isnull(sum(monto_pgv_r), 0) as msCompR " +
                                                              "        from ms_comp " +
                                                              "        where id_cc = @idCC " +
                                                              "          and año_pgv = @año " +
                                                              "          and mes_pgv = @mes) as pgvR ", ConexionBD)
                        sdaPGV.SelectCommand.Parameters.AddWithValue("@idCC", Val(._txtIdCC.Text))
                        sdaPGV.SelectCommand.Parameters.AddWithValue("@año", Val(._txtAñoPGV.Text))
                        sdaPGV.SelectCommand.Parameters.AddWithValue("@mes", Val(._txtMesPGV.Text))
                        ConexionBD.Open()
                        sdaPGV.Fill(dsPGV)
                        ConexionBD.Close()

                        Dim mes As String
                        If Val(._txtMesPGV.Text) < 10 Then
                            mes = "0" + ._txtMesPGV.Text
                        Else
                            mes = ._txtMesPGV.Text
                        End If
                        SCMValores.CommandText = "update ms_presupuesto " +
                                                 "  set mes_" + mes + "_ep = @pgvEP, mes_" + mes + "_r = @pgvR " +
                                                 "where id_cc = @idCC " +
                                                 "  and año = @año "
                        SCMValores.Parameters.AddWithValue("@pgvEP", Val(dsPGV.Tables(0).Rows(0).Item("pgvEP").ToString()))
                        SCMValores.Parameters.AddWithValue("@pgvR", Val(dsPGV.Tables(0).Rows(0).Item("pgvR").ToString()))
                        SCMValores.Parameters.AddWithValue("@idCC", Val(._txtIdCC.Text))
                        SCMValores.Parameters.AddWithValue("@año", Val(._txtAñoPGV.Text))
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

                    Session("id_actividadM") = 38
                    Session("TipoM") = "SR"
                    Server.Transfer("Menu.aspx")
                End While
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class