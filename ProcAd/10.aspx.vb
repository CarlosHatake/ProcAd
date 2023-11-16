Public Class _10
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
                        'Datos de la Comprobación
                        Dim sdaSol As New SqlDataAdapter
                        Dim dsSol As New DataSet
                        Dim query As String
                        query = "select id_ms_comp " +
                                "     , empresa " +
                                "     , periodo_comp " +
                                "     , empleado " +
                                "     , tipo_gasto " +
                                "     , tipo_actividad " +
                                "     , ms_comp.autorizador " +
                                "     , isnull(centro_costo, 'XX') as centro_costo " +
                                "     , isnull(division, 'XX') as division " +
                                "     , justificacion " +
                                "     , isnull(vale_ingreso, 'XX') as vale_ingreso " +
                                "     , vale_ingreso_adj " +
                                "     , isnull(evidencia_adj, 'XX') as evidencia_adj " +
                                "     , aut_dir " +
                                "     , director " +
                                "     , (select count(*) from dt_unidad where dt_unidad.id_ms_comp = ms_comp.id_ms_comp) as unidades " +
                                "     , isnull(ms_comp.id_cc, 0) as id_cc " +
                                "     , isnull(ms_comp.año_pgv, 0) as año_pgv " +
                                "     , isnull(ms_comp.mes_pgv, 0) as mes_pgv " +
                                "     , isnull(ms_comp.id_cc, -1) as id_cc_comp " +
                                "     , cg_empleado.id_cc as id_cc_empl " +
                                "     , cg_cc.nombre as cc_empl " +
                                "from ms_instancia " +
                                "  left join ms_comp on ms_instancia.id_ms_sol = ms_comp.id_ms_comp and ms_instancia.tipo = 'C' " +
                                "  left join bd_Empleado.dbo.cg_empleado on ms_comp.no_empleado = cg_empleado.no_empleado " +
                                "  left join bd_Empleado.dbo.cg_cc on cg_empleado.id_cc = cg_cc.id_cc " +
                                "where id_ms_instancia = @idMsInst "
                        sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@idMsInst", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()
                        .lblFolio.Text = dsSol.Tables(0).Rows(0).Item("id_ms_comp").ToString()
                        .lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                        .lblPeriodo.Text = dsSol.Tables(0).Rows(0).Item("periodo_comp").ToString()
                        .lblEmpleado.Text = dsSol.Tables(0).Rows(0).Item("empleado").ToString()
                        .lblTipoGasto.Text = dsSol.Tables(0).Rows(0).Item("tipo_gasto").ToString()
                        .lblTipoAct.Text = dsSol.Tables(0).Rows(0).Item("tipo_actividad").ToString()
                        .lblAutorizador.Text = dsSol.Tables(0).Rows(0).Item("autorizador").ToString()
                        ._txtIdCC.Text = dsSol.Tables(0).Rows(0).Item("id_cc").ToString()
                        ._txtAñoPGV.Text = dsSol.Tables(0).Rows(0).Item("año_pgv").ToString()
                        ._txtMesPGV.Text = dsSol.Tables(0).Rows(0).Item("mes_pgv").ToString()
                        If dsSol.Tables(0).Rows(0).Item("aut_dir").ToString() = "S" Then
                            .lbl_Director.Visible = True
                            .lblDirector.Visible = True
                            .lblDirector.Text = dsSol.Tables(0).Rows(0).Item("director").ToString()
                        Else
                            .lbl_Director.Visible = False
                            .lblDirector.Visible = False
                        End If
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
                        'If dsSol.Tables(0).Rows(0).Item("evidencia_adj").ToString() = "XX" Then
                        '    .lblEvidenciaN.Visible = True
                        '    .hlEvidencia.Visible = False
                        'Else
                        '    .lblEvidenciaN.Visible = False
                        '    .hlEvidencia.Visible = True
                        '    .hlEvidencia.Text = dsSol.Tables(0).Rows(0).Item("evidencia_adj").ToString()
                        '    '.hlEvidencia.NavigateUrl = "http://localhost/ProcAd - Adjuntos/" + .lblFolio.Text + "Evid-" + dsSol.Tables(0).Rows(0).Item("evidencia_adj").ToString()
                        '    .hlEvidencia.NavigateUrl = "http://148.223.153.43/ProcAd - Adjuntos/" + .lblFolio.Text + "Evid-" + dsSol.Tables(0).Rows(0).Item("evidencia_adj").ToString()
                        'End If

                        If dsSol.Tables(0).Rows(0).Item("evidencia_adj").ToString() = "XX" Then

                            .lblEvidenciaN.Visible = True
                            .hlEvidencia.Visible = False
                            'Evidencia'
                            Dim sdaEvi As New SqlDataAdapter
                            Dim dsEvi As New DataSet
                            gvEvidencias.DataSource = dsEvi

                            Dim queryE As String
                            queryE = "SELECT id_dt_archivo_comp,  archivo as evidencia_adj, 'http://148.223.153.43/ProcAd/Evidencias Comp/' + archivo as ruta FROM dt_archivo_comp where id_ms_comp = @id_ms_comp and status = 'A' "
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

                        'Validar si el Centro de Costo existe en la Comprobación, y en ese caso validar si es el mismo del usuario
                        If Val(dsSol.Tables(0).Rows(0).Item("id_cc_comp").ToString()) > 0 And Val(dsSol.Tables(0).Rows(0).Item("id_cc_comp").ToString()) <> Val(dsSol.Tables(0).Rows(0).Item("id_cc_empl").ToString()) Then
                            .litError.Text = "El Centro de Costo de la comprobación es diferente al Centro del Costo del Empleado [" + dsSol.Tables(0).Rows(0).Item("cc_empl").ToString() + "]"
                        End If
                        sdaSol.Dispose()
                        dsSol.Dispose()

                        'Anticipos
                        Dim sdaAnticipo As New SqlDataAdapter
                        Dim dsAnticipo As New DataSet
                        .gvAnticipos.DataSource = dsAnticipo
                        sdaAnticipo.SelectCommand = New SqlCommand("select ms_anticipo.id_ms_anticipo " +
                                                                   "     , case ms_anticipo.tipo_pago when 'T' then 'Transferencia' when 'E' then 'Efectivo' else '-' end as tipo " +
                                                                   "     , periodo_ini , periodo_fin " +
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
                        If .gvAnticipos.Rows.Count = 0 Then
                            .lbl_Anticipos.Visible = False
                        Else
                            .lbl_Anticipos.Visible = True
                        End If

                        'Conceptos
                        Dim sdaConcepto As New SqlDataAdapter
                        Dim dsConcepto As New DataSet
                        .gvConceptos.DataSource = dsConcepto
                        sdaConcepto.SelectCommand = New SqlCommand("select fecha_realizo " +
                                                                   "     , case dt_comp.tipo when 'F' then 'F' else null end as Factura " +
                                                                   "     , case dt_comp.tipo when 'F' then dt_factura.lugar_exp else null end as lugar_exp " +
                                                                   "     , case dt_comp.tipo when 'F' then 'http://148.223.153.43/Facturas/' + dt_factura.rfc_receptor + '/' + format(datepart(year, dt_factura.fecha_emision), '00') + '/' + upper(format(dt_factura.fecha_emision, 'MMMM', 'es-es')) + '/' + dt_factura.movimiento + '/' + dt_factura.tipo + '/' + dt_factura.uuid + '.pdf' else null end as path " +
                                                                   "     , case dt_comp.tipo when 'T' then 'V' else null end as Tabulador " +
                                                                   "     , nombre_concepto " +
                                                                   "     , no_personas " +
                                                                   "     , no_dias " +
                                                                   "     , monto_subtotal " +
                                                                   "     , monto_iva " +
                                                                   "     , ISNULL(isr_ret,0) as isr_ret " +
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
                                                                   "order by fecha_realizo ", ConexionBD)
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



                        'Totales
                        Dim sdaTot As New SqlDataAdapter
                        Dim dsTot As New DataSet
                        query = "select (select isnull(sum(monto_hospedaje) + sum(monto_alimentos) + sum(monto_casetas) + sum(monto_otros), 0) as monto_ant " +
                            "        from dt_anticipo " +
                            "            left join ms_anticipo on dt_anticipo.id_ms_anticipo = ms_anticipo.id_ms_anticipo " +
                            "        where dt_anticipo.id_ms_comp = @id_ms_comp) as anticipo_imp " +
                            "     , (select sum(monto_total) as monto_comp " +
                            "        from dt_comp " +
                            "        where dt_comp.id_ms_comp = @id_ms_comp) as comp_imp " +
                            "     , (select isnull(sum(monto_total), 0) " +
                            "        from dt_comp " +
                            "          left join cg_concepto_comp on dt_comp.id_concepto = cg_concepto_comp.id_concepto_comp " +
                            "          left join cg_concepto_cat on cg_concepto_comp.id_concepto_cat = cg_concepto_cat.id_concepto_cat " +
                            "        where dt_comp.id_ms_comp = @id_ms_comp " +
                            "          and dt_comp.tipo = 'F' " +
                            "          and cg_concepto_cat.gastos_viaje = 'S') " +
                            "        + " +
                            "       (select isnull(sum(monto_total), 0) " +
                            "        from dt_comp " +
                            "          left join cg_concepto on dt_comp.id_concepto = cg_concepto.id_concepto " +
                            "          left join cg_concepto_cat on cg_concepto.id_concepto_cat = cg_concepto_cat.id_concepto_cat " +
                            "        where dt_comp.id_ms_comp = @id_ms_comp " +
                            "          and dt_comp.tipo = 'T' " +
                            "          and cg_concepto_cat.gastos_viaje = 'S') as comp_pgv "
                        sdaTot.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaTot.SelectCommand.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        sdaTot.Fill(dsTot)
                        ConexionBD.Close()
                        .wceTotalA.Value = Val(dsTot.Tables(0).Rows(0).Item("anticipo_imp").ToString())
                        .wceTotalC.Value = Val(dsTot.Tables(0).Rows(0).Item("comp_imp").ToString())
                        .wceTotalPGV.Value = Val(dsTot.Tables(0).Rows(0).Item("comp_pgv").ToString())
                        .wceTotalS.Value = .wceTotalA.Value - .wceTotalC.Value
                        sdaTot.Dispose()
                        dsTot.Dispose()

                        .lblTotalA.Text = .wceTotalA.Text
                        .lblTotalC.Text = .wceTotalC.Text
                        .lblTotalS.Text = .wceTotalS.Text

                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        Dim banEfectivo As Integer = 0
                        'Validar Conceptos para Efectivo 
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "select (select count(*) " +
                                             "        from dt_comp " +
                                             "          left join cg_concepto on dt_comp.id_concepto = cg_concepto.id_concepto " +
                                             "        where dt_comp.id_ms_comp = ms_comp.id_ms_comp " +
                                             "          and ((dt_comp.tipo = 'T' and cg_concepto.nombre_concepto in ('COMPLEMENTO SUELDO','GRATIFICACION', 'GRATIFICACIONES (EXISTENTES EN CATALOGO)', 'ALIMENTOS (FRESNITOS)', 'ESTADIA')) " +
                                             "		      or (select pago_efectivo from cg_usuario where cg_usuario.id_usuario = id_usr_solicita) = 'S') " +
                                             "	     ) as pagoEfectivo " +
                                             "from ms_comp " +
                                             "where id_ms_comp = @id_ms_comp "
                        SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        banEfectivo = SCMValores.ExecuteScalar
                        ConexionBD.Close()

                        .cbPagoEfect.Checked = False
                        If (.wceTotalS.Value < 0 And .wceTotalS.Value > -50) Or (banEfectivo > 0 And .wceTotalS.Value < 0) Then
                            .cbPagoEfect.Enabled = True
                        Else
                            .cbPagoEfect.Enabled = False
                        End If

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

    Public Sub actualizarPresup()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD

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
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
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
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Registrar / Rechazar"

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        With Me
            Try
                .litError.Text = ""

                Dim valor As Integer = 0
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim fecha As DateTime = Date.Now
                While Val(._txtBan.Text) = 0
                    'Actualizar datos de la Solicitud
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "Declare @valorR int; Execute Sp_U_ms_comp10  @id_usr_valida, @obs_comp, @pago_efectivo, @monto_pgv_r,  @id_ms_comp, @id_ms_instancia, @id_actividad,  @valorR output; Select @valorR "
                    'SCMValores.CommandText = "update ms_comp set id_usr_valida = @id_usr_valida, fecha_valida = @fecha_valida, pago_efectivo = @pago_efectivo, monto_pgv_ep = 0, monto_pgv_ex = 0, monto_pgv_r = @monto_pgv_r, status = 'R', obs_comp=@obs_comp where id_ms_comp = @id_ms_comp "
                    SCMValores.Parameters.AddWithValue("@id_usr_valida", Val(._txtIdUsuario.Text))
                    SCMValores.Parameters.AddWithValue("@obs_comp", txtObs.Text)

                    SCMValores.Parameters.AddWithValue("@fecha_valida", fecha)
                    If .cbPagoEfect.Checked = True Then
                        'Pasa a la actividad de Caja para Entrega de Efectivo
                        SCMValores.Parameters.AddWithValue("@pago_efectivo", "S")
                    Else
                        'Finaliza el proceso
                        SCMValores.Parameters.AddWithValue("@pago_efectivo", "N")
                    End If
                    SCMValores.Parameters.AddWithValue("monto_pgv_r", .wceTotalPGV.Value)
                    SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    Dim idAct As Integer
                    If .cbPagoEfect.Checked = True Then
                        'Pasa a la actividad de Caja para Entrega de Efectivo
                        idAct = 31
                    Else
                        'Finaliza el proceso
                        idAct = 11
                    End If
                    SCMValores.Parameters.AddWithValue("@id_actividad", idAct)
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    valor = SCMValores.ExecuteScalar()
                    ConexionBD.Close()

                    If valor = 0 Then
                        Server.Transfer("Menu.aspx")
                    End If
                    SCMValores.Parameters.Clear()

                    ._txtBan.Text = 1

                    'Actualizar Anticipos
                    Dim SCMValoresAnt As SqlCommand = New System.Data.SqlClient.SqlCommand
                    Dim SCMValoresMsR As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValoresAnt.Connection = ConexionBD
                    SCMValoresMsR.Connection = ConexionBD
                    SCMValoresAnt.Parameters.Clear()
                    SCMValoresMsR.Parameters.Clear()
                    SCMValoresAnt.CommandText = "update ms_anticipo " +
                                                "set status = case status when 'EECA' then 'EECR' when 'TRCA' then 'TRCR' else status end " +
                                                "where id_ms_anticipo = @id_ms_anticipo "
                    SCMValoresAnt.Parameters.Add("@id_ms_anticipo", SqlDbType.Int)
                    SCMValoresMsR.CommandText = "update ms_recursos " +
                                                "  set monto_pgv_r = monto_pgv_ep, monto_pgv_ep = 0 " +
                                                "where id_ms_anticipo = @id_ms_anticipo "
                    SCMValoresMsR.Parameters.Add("@id_ms_anticipo", SqlDbType.Int)
                    For i = 0 To .gvAnticipos.Rows.Count - 1
                        SCMValoresAnt.Parameters("@id_ms_anticipo").Value = Val(.gvAnticipos.Rows(i).Cells(0).Text)
                        ConexionBD.Open()
                        SCMValoresAnt.ExecuteNonQuery()
                        ConexionBD.Close()
                        SCMValoresMsR.Parameters("@id_ms_anticipo").Value = Val(.gvAnticipos.Rows(i).Cells(0).Text)
                        ConexionBD.Open()
                        SCMValoresMsR.ExecuteNonQuery()
                        ConexionBD.Close()
                    Next

                    If Val(._txtIdCC.Text) > 0 Then
                        actualizarPresup()
                    End If

                    'Actualización de dt_factura
                    Dim SCMValoresDtFact As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValoresDtFact.Connection = ConexionBD
                    SCMValoresDtFact.Parameters.Clear()
                    SCMValoresDtFact.CommandText = "update dt_factura " +
                                                   "set status = 'CR' " +
                                                   "where id_dt_factura = @id_dt_factura "
                    SCMValoresDtFact.Parameters.Add("@id_dt_factura", SqlDbType.VarChar)

                    For i = 0 To .gvConceptos.Rows.Count - 1
                        If .gvConceptos.Rows(i).Cells(1).Text = "F" Then
                            'Actualizar Status de Factura
                            SCMValoresDtFact.Parameters("@id_dt_factura").Value = .gvConceptos.Rows(i).Cells(0).Text
                            ConexionBD.Open()
                            SCMValoresDtFact.ExecuteNonQuery()
                            ConexionBD.Close()
                        End If
                    Next


                    ''Actualizar Instancia
                    'SCMValores.Parameters.Clear()
                    'SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                    'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    'SCMValores.Parameters.AddWithValue("@id_actividad", idAct)
                    'ConexionBD.Open()
                    'SCMValores.ExecuteNonQuery()
                    'ConexionBD.Close()

                    ''Registrar en Histórico
                    'SCMValores.Parameters.Clear()
                    'SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                    '                         "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    'SCMValores.Parameters.AddWithValue("@id_actividad", idAct)
                    'SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    'SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    'ConexionBD.Open()
                    'SCMValores.ExecuteNonQuery()
                    'ConexionBD.Close()

                    'Envío de Correo
                    Dim Mensaje As New System.Net.Mail.MailMessage()
                    Dim destinatario As String = ""
                    'Obtener el Correo del Solicitante
                    SCMValores.CommandText = "select cgEmpl.correo " +
                                             "from ms_comp " +
                                             "  left join cg_usuario on ms_comp.id_usr_solicita = cg_usuario.id_usuario " +
                                             "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                             "where id_ms_comp = @id_ms_comp "
                    SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                    ConexionBD.Open()
                    destinatario = SCMValores.ExecuteScalar()
                    ConexionBD.Close()

                    Mensaje.[To].Add(destinatario)
                    Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                    'Mensaje.From = New System.Net.Mail.MailAddress("soporte.unne.procad@gmail.com", "ProcAd", System.Text.Encoding.UTF8)
                    Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                    Mensaje.Subject = "ProcAd - Comprobación No. " + .lblFolio.Text + " Validada"
                    Dim texto As String
                    If txtObs.Text = "" Then
                        texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                             "La comprobación número <b>" + .lblFolio.Text + "</b> fue validada. <br>"
                    Else
                        texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                             "La comprobación número <b>" + .lblFolio.Text + "</b> fue validada.<br> <p>Comentarios: " + txtObs.Text + " </p> <br>"
                    End If


                    If .cbPagoEfect.Checked = True Then
                        'Pasa a la actividad de Caja para Entrega de Efectivo
                        texto = texto + "Favor de pasar a Caja por el pago en efectivo"
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

                    .cbPagoEfect.Enabled = False
                    .btnGuardar.Enabled = False
                    .btnRegresar.Enabled = False
                    .btnRechaza.Enabled = False
                End While



            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        With Me
            Try
                .litError.Text = ""

                If txtObs.Text = "" Then
                    litError.Text = "Se debe poner una observación del por que se esta rechazando"
                Else

                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    Dim fecha As DateTime = Date.Now
                    Dim valor As Integer = 0
                    While Val(._txtBan.Text) = 0

                        ._txtBan.Text = 1

                        'Actualizar datos de la Comprobación
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "Declare @valorR int; Execute Sp_U_ms_comp_ms_instancia10  @id_ms_comp, @obs_comp,  @id_ms_instancia, @id_actividad, @id_usr,  @valorR output; Select @valorR "
                        'SCMValores.CommandText = "update ms_comp set fecha_autoriza = NULL, fecha_aut_dir = NULL, status = 'P', obs_comp=@obs_comp where id_ms_comp = @id_ms_comp "
                        SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                        SCMValores.Parameters.AddWithValue("@obs_comp", Val(.lblFolio.Text))
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 12)
                        SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        valor = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        If valor = 0 Then
                            Server.Transfer("Menu.aspx")
                        End If

                        SCMValores.Parameters.Clear()

                        'Actualizar Anticipos
                        Dim SCMValoresAnt As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValoresAnt.Connection = ConexionBD
                        SCMValoresAnt.Parameters.Clear()
                        SCMValoresAnt.CommandText = "update ms_anticipo " +
                                                    "set status = case status when 'EECA' then 'EECP' when 'TRCA' then 'TRCP' else status end " +
                                                    "where id_ms_anticipo = @id_ms_anticipo "
                        SCMValoresAnt.Parameters.Add("@id_ms_anticipo", SqlDbType.Int)
                        For i = 0 To .gvAnticipos.Rows.Count - 1
                            SCMValoresAnt.Parameters("@id_ms_anticipo").Value = Val(.gvAnticipos.Rows(i).Cells(0).Text)
                            ConexionBD.Open()
                            SCMValoresAnt.ExecuteNonQuery()
                            ConexionBD.Close()
                        Next


                        'Actualización de dt_factura
                        Dim SCMValoresDtFact As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValoresDtFact.Connection = ConexionBD
                        SCMValoresDtFact.Parameters.Clear()
                        SCMValoresDtFact.CommandText = "update dt_factura " +
                                                       "set status = 'CP' " +
                                                       "where id_dt_factura = @id_dt_factura "
                        SCMValoresDtFact.Parameters.Add("@id_dt_factura", SqlDbType.VarChar)

                        For i = 0 To .gvConceptos.Rows.Count - 1
                            If .gvConceptos.Rows(i).Cells(1).Text = "F" Then
                                'Actualizar Status de Factura
                                SCMValoresDtFact.Parameters("@id_dt_factura").Value = Val(.gvConceptos.Rows(i).Cells(0).Text)
                                ConexionBD.Open()
                                SCMValoresDtFact.ExecuteNonQuery()
                                ConexionBD.Close()
                            End If
                        Next

                        'Actualizar Instancia
                        'SCMValores.Parameters.Clear()
                        'SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                        'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        'SCMValores.Parameters.AddWithValue("@id_actividad", 12)
                        'ConexionBD.Open()
                        'SCMValores.ExecuteNonQuery()
                        'ConexionBD.Close()

                        'Registrar en Histórico
                        'SCMValores.Parameters.Clear()
                        'SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                        '                         "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 12)
                        'SCMValores.Parameters.AddWithValue("@fecha", fecha)
                        SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        'ConexionBD.Open()
                        'SCMValores.ExecuteNonQuery()
                        'ConexionBD.Close()

                        'Envío de Correo
                        Dim Mensaje As New System.Net.Mail.MailMessage()
                        Dim destinatario As String = ""
                        'Obtener el Correo del Solicitante
                        SCMValores.CommandText = "select cgEmpl.correo " +
                                                 "from ms_comp " +
                                                 "  left join cg_usuario on ms_comp.id_usr_solicita = cg_usuario.id_usuario " +
                                                 "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                 "where id_ms_comp = @id_ms_comp "
                        SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        destinatario = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        Mensaje.[To].Add(destinatario)
                        Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                        'Mensaje.From = New System.Net.Mail.MailAddress("soporte.unne.procad@gmail.com", "ProcAd", System.Text.Encoding.UTF8)
                        Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                        Mensaje.Subject = "ProcAd - Comprobación No. " + .lblFolio.Text + " por Corregir"
                        Dim texto As String
                        texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                                "La comprobación número <b>" + .lblFolio.Text + "</b> fue rechazada por Comprobaciones, por la siguiente observacion " + txtObs.Text + ", se encuentra en la actividad de Corregir Comprobación. <br></span>"
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

                        .cbPagoEfect.Enabled = False
                        .btnGuardar.Enabled = False
                        .btnRegresar.Enabled = False
                        .btnRechaza.Enabled = False
                    End While
                End If


            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnRechaza_Click(sender As Object, e As EventArgs) Handles btnRechaza.Click
        With Me
            Try
                .litError.Text = ""

                If txtObs.Text = "" Then
                    litError.Text = "Se debe poner una observación del por que se esta rechazando"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    Dim fecha As DateTime = Date.Now
                    Dim valor As Integer = 0
                    While Val(._txtBan.Text) = 0
                        'Actualizar datos de la Comprobación
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "Declare @valorR int; Execute Sp_D_ms_comp10  @id_ms_comp, @id_ms_instancia, @id_actividad, @id_usr,  @valorR output; Select @valorR "
                        'SCMValores.CommandText = "update ms_comp set id_usr_valida = @id_usr_valida, fecha_valida = @fecha_valida, monto_pgv_ep = 0, monto_pgv_ex = 0, status = 'ZC' where id_ms_comp = @id_ms_comp "
                        'SCMValores.Parameters.AddWithValue("@id_usr_valida", Val(._txtIdUsuario.Text))
                        'SCMValores.Parameters.AddWithValue("@fecha_valida", fecha)
                        SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 30)
                        SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        valor = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        If valor = 0 Then
                            Server.Transfer("Menu.aspx")
                        End If


                        If Val(._txtIdCC.Text) > 0 Then
                            actualizarPresup()
                        End If

                        ._txtBan.Text = 1

                        'Actualizar Anticipos
                        Dim SCMValoresAnt As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValoresAnt.Connection = ConexionBD
                        SCMValoresAnt.Parameters.Clear()
                        SCMValoresAnt.CommandText = "update ms_anticipo " +
                                                    "set status = case status when 'EECA' then 'EE' when 'TRCA' then 'TR' else status end " +
                                                    "where id_ms_anticipo = @id_ms_anticipo "
                        SCMValoresAnt.Parameters.Add("@id_ms_anticipo", SqlDbType.Int)
                        For i = 0 To .gvAnticipos.Rows.Count - 1
                            SCMValoresAnt.Parameters("@id_ms_anticipo").Value = Val(.gvAnticipos.Rows(i).Cells(0).Text)
                            ConexionBD.Open()
                            SCMValoresAnt.ExecuteNonQuery()
                            ConexionBD.Close()
                        Next

                        'Actualización de dt_factura
                        Dim SCMValoresDtFact As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValoresDtFact.Connection = ConexionBD
                        SCMValoresDtFact.Parameters.Clear()
                        SCMValoresDtFact.CommandText = "update dt_factura " +
                                                       "set status = 'P' " +
                                                       "where id_dt_factura = @id_dt_factura "
                        SCMValoresDtFact.Parameters.Add("@id_dt_factura", SqlDbType.VarChar)

                        For i = 0 To .gvConceptos.Rows.Count - 1
                            If .gvConceptos.Rows(i).Cells(1).Text = "F" Then
                                'Actualizar Status de Factura
                                SCMValoresDtFact.Parameters("@id_dt_factura").Value = Val(.gvConceptos.Rows(i).Cells(0).Text)
                                ConexionBD.Open()
                                SCMValoresDtFact.ExecuteNonQuery()
                                ConexionBD.Close()
                            End If
                        Next

                        ''Actualizar Instancia
                        'SCMValores.Parameters.Clear()
                        'SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                        'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        'SCMValores.Parameters.AddWithValue("@id_actividad", 30)
                        'ConexionBD.Open()
                        'SCMValores.ExecuteNonQuery()
                        'ConexionBD.Close()

                        ''Registrar en Histórico
                        'SCMValores.Parameters.Clear()
                        'SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                        '                         "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                        'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        'SCMValores.Parameters.AddWithValue("@id_actividad", 30)
                        'SCMValores.Parameters.AddWithValue("@fecha", fecha)
                        'SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        'ConexionBD.Open()
                        'SCMValores.ExecuteNonQuery()
                        'ConexionBD.Close()

                        'Envío de Correo
                        SCMValores.Parameters.Clear()
                        Dim Mensaje As New System.Net.Mail.MailMessage()
                        Dim destinatario As String = ""
                        'Obtener el Correo del Solicitante
                        SCMValores.CommandText = "select cgEmpl.correo " +
                                                 "from ms_comp " +
                                                 "  left join cg_usuario on ms_comp.id_usr_solicita = cg_usuario.id_usuario " +
                                                 "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                 "where id_ms_comp = @id_ms_comp "
                        SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        destinatario = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        Mensaje.[To].Add(destinatario)
                        Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                        'Mensaje.From = New System.Net.Mail.MailAddress("soporte.unne.procad@gmail.com", "ProcAd", System.Text.Encoding.UTF8)
                        Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                        Mensaje.Subject = "ProcAd - Comprobación No. " + .lblFolio.Text + " Cancelada"
                        Dim texto As String
                        texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                                "La comprobación número <b>" + .lblFolio.Text + "</b> fue cancelada. <br> por los siguientes comentarios: " + txtObs.Text + "</span>"
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

                        .cbPagoEfect.Enabled = False
                        .btnGuardar.Enabled = False
                        .btnRegresar.Enabled = False
                        .btnRechaza.Enabled = False
                    End While
                End If


            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class