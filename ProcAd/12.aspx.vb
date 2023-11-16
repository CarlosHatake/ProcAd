Public Class _12
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 1
                    'Session("idMsInst") = 14054

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtIdMsInst.Text = Session("idMsInst")
                        ._txtBan.Text = 0
                        ._txtBanImporte.Text = 0

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
                                "     , id_usr_autoriza " +
                                "     , cg_empleado.puesto_tabulador " +
                                "     , isnull(obs_autorizador, '') as obs_autorizador " +
                                "     , unidad_comp " +
                                "     , (select count(*) from dt_unidad where dt_unidad.id_ms_comp = ms_comp.id_ms_comp) as unidades " +
                                "     , isnull(ms_comp.id_cc, 0) as id_cc " +
                                "     , isnull(ms_comp.año_pgv, 0) as año_pgv " +
                                "     , isnull(ms_comp.mes_pgv, 0) as mes_pgv " +
                                "     , ms_comp.id_validador  as validador" +
                                "     , cg_usuario.omitir_PGV " +
                                "     , cg_usuario.alimentos_tab " +
                                "     , cg_usuario.taxi_tab " +
                                "     , right('0000000' + ltrim(rtrim(cg_empleado.no_empleado)),7) as no_empleado " +
                                "from ms_instancia " +
                                "  left join ms_comp on ms_instancia.id_ms_sol = ms_comp.id_ms_comp and ms_instancia.tipo = 'C' " +
                                "  left join cg_usuario on ms_comp.id_usr_solicita = cg_usuario.id_usuario " +
                                "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " +
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
                        ._txtIdAutoriza.Text = dsSol.Tables(0).Rows(0).Item("id_usr_autoriza").ToString()
                        ._txtPuestoTab.Text = dsSol.Tables(0).Rows(0).Item("puesto_tabulador").ToString()
                        ._txtIdCCPGV.Text = dsSol.Tables(0).Rows(0).Item("id_cc").ToString()
                        ._txtAñoPGV.Text = dsSol.Tables(0).Rows(0).Item("año_pgv").ToString()
                        ._txtMesPGV.Text = dsSol.Tables(0).Rows(0).Item("mes_pgv").ToString()
                        ._txtOmitValPGV.Text = dsSol.Tables(0).Rows(0).Item("mes_pgv").ToString()
                        ._txtAlimTab.Text = dsSol.Tables(0).Rows(0).Item("alimentos_tab").ToString()
                        ._txtTaxiTab.Text = dsSol.Tables(0).Rows(0).Item("taxi_tab").ToString()
                        ._txtValidador.Text = dsSol.Tables(0).Rows(0).Item("validador").ToString()
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
                        .txtObsAut.Text = dsSol.Tables(0).Rows(0).Item("obs_autorizador").ToString()
                        'If dsSol.Tables(0).Rows(0).Item("vale_ingreso").ToString() = "XX" Then
                        '    .lbl_ValeI.Visible = False
                        .hlValeI.Visible = False
                        'Else
                        '    .lbl_ValeI.Visible = True
                        '    .hlValeI.Visible = True
                        '    .hlValeI.Text = dsSol.Tables(0).Rows(0).Item("vale_ingreso").ToString()
                        '    '.hlValeI.NavigateUrl = "http://localhost/ProcAd - Adjuntos/" + .lblFolio.Text + "ValeI-" + dsSol.Tables(0).Rows(0).Item("vale_ingreso_adj").ToString()
                        '    .hlValeI.NavigateUrl = "http://148.223.153.43/ProcAd - Adjuntos/" + .lblFolio.Text + "ValeI-" + dsSol.Tables(0).Rows(0).Item("vale_ingreso_adj").ToString()
                        'End If
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

                        If Val(dsSol.Tables(0).Rows(0).Item("unidades").ToString()) > 0 Or dsSol.Tables(0).Rows(0).Item("unidad_comp").ToString() = "S" Then
                            .pnlUnidad.Visible = True
                            actualizarUnidades()
                        Else
                            .pnlUnidad.Visible = False
                        End If

                        'Llenar el grid de evidencias'
                        Dim sdaEvi As New SqlDataAdapter
                        Dim dsEvi As New DataSet
                        gvEvidencias.DataSource = dsEvi
                        sdaEvi.SelectCommand = New SqlCommand("SELECT id_dt_archivo_comp,  archivo as nombre, 'http://148.223.153.43/ProcAd - Adjuntos/' + archivo as ruta FROM dt_archivo_comp where id_ms_comp = @id_ms_comp and status = 'A' ", ConexionBD)
                        sdaEvi.SelectCommand.Parameters.AddWithValue("@id_ms_comp", Val(lblFolio.Text))
                        ConexionBD.Open()
                        sdaEvi.Fill(dsEvi)
                        gvEvidencias.DataBind()
                        gvEvidencias.Columns(0).Visible = False
                        ConexionBD.Close()

                        If gvEvidencias.Rows.Count <> 0 Then
                            lblEvidenciaN.Visible = False
                        Else
                            lblEvidenciaN.Visible = True
                        End If

                        'Fecha de alta Empleado NOM
                        Dim ConexionBDNom As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBDNom.ConnectionString = accessDB.conBD("NOM")
                        Dim SCMValoresNOM As SqlCommand = New System.Data.SqlClient.SqlCommand
                        Dim fechaAE As Date
                        SCMValoresNOM.Connection = ConexionBDNom
                        SCMValoresNOM.CommandText = ""
                        SCMValoresNOM.Parameters.Clear()
                        SCMValoresNOM.CommandText = "select dateadd(month, 1, isnull((select cast(fecini as date) " +
                                                    "                                 from nomtrab " +
                                                    "                                 where nomtrab.status = 'A' " +
                                                    "                                   and nomtrab.cvetra = @no_empleado), '1900-01-01')) as fecha_inicio "
                        SCMValoresNOM.Parameters.AddWithValue("@no_empleado", dsSol.Tables(0).Rows(0).Item("no_empleado").ToString())
                        ConexionBDNom.Open()
                        fechaAE = SCMValoresNOM.ExecuteScalar()
                        ConexionBDNom.Close()

                        If fechaAE > Now.Date() Then
                            ._txtTaxiTab.Text = "S"
                        End If

                        sdaSol.Dispose()
                        dsSol.Dispose()

                        'Tipo de Gasto y ID Centro de Costo
                        Dim sdaTipoId As New SqlDataAdapter
                        Dim dsTipoId As New DataSet
                        sdaTipoId.SelectCommand = New SqlCommand("select abreviatura " +
                                                                 "     , case " +
                                                                 "         when ms_comp.centro_costo is not NULL then " +
                                                                 "           (select id_cc " +
                                                                 "            from bd_Empleado.dbo.cg_cc " +
                                                                 "            where cg_cc.nombre = ms_comp.centro_costo " +
                                                                 "              and cg_cc.id_empresa = cg_empresa.id_empresa " +
                                                                 "              and cg_cc.status = 'A') " +
                                                                 "         else 0 " +
                                                                 "       end as idCC " +
                                                                 "from ms_comp " +
                                                                 "  left join bd_Empleado.dbo.cg_empresa on ms_comp.empresa = cg_empresa.nombre " +
                                                                 "  left join cg_tipoGasto on cg_empresa.id_empresa = cg_tipoGasto.id_empresa and ms_comp.tipo_gasto = cg_tipoGasto.nombre_gasto " +
                                                                 "where id_ms_comp = @id_ms_comp ", ConexionBD)
                        sdaTipoId.SelectCommand.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        sdaTipoId.Fill(dsTipoId)
                        ConexionBD.Close()
                        ._txtTipoGasto.Text = dsTipoId.Tables(0).Rows(0).Item("abreviatura").ToString()
                        ._txtIdCC.Text = dsTipoId.Tables(0).Rows(0).Item("idCC").ToString()

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
                        If .gvAnticipos.Rows.Count = 0 Then
                            .lblAnticiposN.Visible = True
                        Else
                            .lblAnticiposN.Visible = False
                        End If

                        'Vales de Ingreso
                        Dim sdaValeI As New SqlDataAdapter
                        Dim dsValeI As New DataSet
                        .gvValeI.DataSource = dsValeI
                        sdaValeI.SelectCommand = New SqlCommand("select vale_ingreso as Vale " +
                                                                "     , 'http://148.223.153.43/ProcAd - Adjuntos/' + cast(id_ms_comp as varchar(20)) + 'ValeI-' + vale_ingreso_adj as path " +
                                                                "from ms_comp " +
                                                                "where id_ms_comp = @id_ms_comp " +
                                                                "  and vale_ingreso is not NULL " +
                                                                "union " +
                                                                "select vale_ingreso as Vale " +
                                                                "     , 'http://148.223.153.43/ProcAd - Adjuntos/' + cast(id_dt_vale as varchar(20)) + 'ValeIC-' + vale_ingreso_adj as path " +
                                                                "from dt_vale " +
                                                                "where id_ms_comp = @id_ms_comp ", ConexionBD)
                        sdaValeI.SelectCommand.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        sdaValeI.Fill(dsValeI)
                        .gvValeI.DataBind()
                        ConexionBD.Close()
                        sdaValeI.Dispose()
                        dsValeI.Dispose()
                        If .gvValeI.Rows.Count = 0 Then
                            .lblValeIN.Visible = True
                        Else
                            .lblValeIN.Visible = False
                        End If

                        'Lista de Conceptos de Devolución
                        Dim sdaConceptoDev As New SqlDataAdapter
                        Dim dsConceptoDev As New DataSet
                        sdaConceptoDev.SelectCommand = New SqlCommand("select id_concepto, abreviatura + ' - ' + nombre_concepto as nombre_concepto " +
                                                                     "from cg_concepto " +
                                                                     "where status = 'A' " +
                                                                     "  and abreviatura = @abreviatura " +
                                                                     "  and (id_cc = 0 or id_cc = @idCC) " +
                                                                     "  and nombre_concepto like '%DEVOLUCION%' " +
                                                                     "order by nombre_concepto ", ConexionBD)
                        sdaConceptoDev.SelectCommand.Parameters.AddWithValue("@abreviatura", ._txtTipoGasto.Text)
                        sdaConceptoDev.SelectCommand.Parameters.AddWithValue("@idCC", ._txtIdCC.Text)
                        .ddlConceptoDev.DataSource = dsConceptoDev
                        .ddlConceptoDev.DataTextField = "nombre_concepto"
                        .ddlConceptoDev.DataValueField = "id_concepto"
                        ConexionBD.Open()
                        sdaConceptoDev.Fill(dsConceptoDev)
                        .ddlConceptoDev.DataBind()
                        ConexionBD.Close()
                        sdaConceptoDev.Dispose()
                        dsConceptoDev.Dispose()

                        actualizarConceptos()


                        'origen destino
                        Dim sdaLugar As New SqlDataAdapter
                        Dim dsLugar As New DataSet
                        sdaLugar.SelectCommand = New SqlCommand("select 0 as id_lugar " +
                                                                "     , '' as lugar " +
                                                                "union " +
                                                                "select id_lugar " +
                                                                "     , lugar " +
                                                                "from cg_lugar " +
                                                                "where status = 'A' " +
                                                                "order by lugar ", ConexionBD)
                        .ddlOrig1.DataSource = dsLugar
                        .ddlDest1.DataSource = dsLugar
                        .ddlOrig1.DataTextField = "lugar"
                        .ddlDest1.DataTextField = "lugar"
                        .ddlOrig1.DataValueField = "id_lugar"
                        .ddlDest1.DataValueField = "id_lugar"
                        ConexionBD.Open()
                        sdaLugar.Fill(dsLugar)
                        .ddlOrig1.DataBind()
                        .ddlDest1.DataBind()
                        ConexionBD.Close()
                        sdaLugar.Dispose()
                        dsLugar.Dispose()
                        'Panel
                        .pnlInicio.Visible = True
                        .pnlConcepto.Visible = False
                        .pnlValeIng.Visible = False
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

    Public Sub actualizarConceptos()
        With Me
            Try
                'Conceptos
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
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
                                                           "     , dt_comp.id_dt_comp " +
                                                           "     , isnull(dt_comp.no_valido, 'xx') as no_valido " +
                                                           "from dt_comp " +
                                                           "  left join dt_factura on dt_comp.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                                           "where id_ms_comp = @id_ms_comp " +
                                                           "order by id_dt_comp ", ConexionBD)
                sdaConcepto.SelectCommand.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                .gvConceptos.Columns(0).Visible = True
                .gvConceptos.Columns(1).Visible = True
                .gvConceptos.Columns(13).Visible = True
                .gvConceptos.Columns(17).Visible = True
                .gvConceptos.Columns(19).Visible = True
                ConexionBD.Open()
                sdaConcepto.Fill(dsConcepto)
                .gvConceptos.DataBind()
                ConexionBD.Close()
                .gvConceptos.Columns(0).Visible = False
                .gvConceptos.Columns(1).Visible = False
                .gvConceptos.Columns(17).Visible = False
                .gvConceptos.Columns(19).Visible = False
                sdaConcepto.Dispose()
                dsConcepto.Dispose()

                For i = 0 To .gvConceptos.Rows.Count - 1
                    '17
                    If .gvConceptos.Rows(i).Cells(17).Text = "xx" Then
                        .gvConceptos.Rows(i).Cells(18).Controls(0).Visible = False
                    Else
                        .gvConceptos.Rows(i).Cells(18).Controls(0).Visible = True
                    End If
                Next

                sumarConceptos()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub validarFactura()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaFactura As New SqlDataAdapter
                Dim dsFactura As New DataSet
                sdaFactura.SelectCommand = New SqlCommand("select isnull(razon_emisor,'') as razon_emisor " +
                                                          "     , subtotal - descuentos + tot_tras_ieps as subtotal " +
                                                          "     , tot_tras_iva " +
                                                          "     , importe " +
                                                          "from dt_factura " +
                                                          "where id_dt_factura = @idDtFactura ", ConexionBD)
                sdaFactura.SelectCommand.Parameters.AddWithValue("@idDtFactura", .gvConceptos.SelectedRow.Cells(19).Text)
                ConexionBD.Open()
                sdaFactura.Fill(dsFactura)
                ConexionBD.Close()
                'Datos de Conceptos
                Dim sdaTabulador As New SqlDataAdapter
                Dim dsTabulador As New DataSet
                sdaTabulador.SelectCommand = New SqlCommand("select no_conceptos " +
                                                            "     , round((isnull(cantidad1, 0) * isnull(cgTab1.imp_con_factura, 0) + isnull(cantidad2, 0) * isnull(cgTab2.imp_con_factura, 0)) / (1 + isnull(iva,0)), 2) as subtotal " +
                                                            "     , round(((isnull(cantidad1, 0) * isnull(cgTab1.imp_con_factura, 0) + isnull(cantidad2, 0) * isnull(cgTab2.imp_con_factura, 0)) / (1 + isnull(iva,0))) * isnull(iva,0), 2) as iva " +
                                                            "     , round((isnull(cantidad1, 0) * isnull(cgTab1.imp_con_factura, 0) + isnull(cantidad2, 0) * isnull(cgTab2.imp_con_factura, 0)) / (1 + isnull(iva,0)), 2) + round(((isnull(cantidad1, 0) * isnull(cgTab1.imp_con_factura, 0) + isnull(cantidad2, 0) * isnull(cgTab2.imp_con_factura, 0)) / (1 + isnull(iva,0))) * isnull(iva,0), 2) as importe " +
                                                            "     , isnull(cantidad1, 0) * isnull(cgTab1.imp_con_factura, 0) + isnull(cantidad2, 0) * isnull(cgTab2.imp_con_factura, 0) as importe_autorizado " +
                                                            "     , case when cg_usuario_alim.id_usuario is not null then 0 else alimentos end as alimentos " +
                                                            "from cg_concepto_comp " +
                                                            "  left join cg_tabulador as cgTab1 on cg_concepto_comp.cve_concepto1 = cgTab1.cve_concepto and cgTab1.cve_puesto = @idPuesto " +
                                                            "  left join cg_tabulador as cgTab2 on cg_concepto_comp.cve_concepto2 = cgTab2.cve_concepto and cgTab2.cve_puesto = @idPuesto " +
                                                            "  left join cg_usuario_alim on cg_usuario_alim.id_usuario = @idUsr " +
                                                            "where id_concepto_comp = @idConcepto ", ConexionBD)
                sdaTabulador.SelectCommand.Parameters.AddWithValue("@idConcepto", .ddlConcepto.SelectedValue)
                sdaTabulador.SelectCommand.Parameters.AddWithValue("@idPuesto", ._txtPuestoTab.Text)
                sdaTabulador.SelectCommand.Parameters.AddWithValue("@idUsr", ._txtIdUsuario.Text)
                ConexionBD.Open()
                sdaTabulador.Fill(dsTabulador)
                ConexionBD.Close()

                'Determinar si el concepto corresponde a Alimentos
                If dsTabulador.Tables(0).Rows(0).Item("alimentos").ToString() = 1 Then
                    .wneNoPers.Value = 1
                End If

                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                'Validar Número de Personas para caso de Hosedaje de Mecánicos
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select case when concepto = 'HOSPEDAJE' and @idPuesto = 'Mec' then cast(ROUND(@noPers/2.0,0) as int) else @noPers end as noPers " +
                                         "from cg_concepto_comp " +
                                         "where id_concepto_comp = @idConcepto "
                SCMValores.Parameters.AddWithValue("@noPers", .wneNoPers.Value)
                SCMValores.Parameters.AddWithValue("@idConcepto", .ddlConcepto.SelectedValue)
                SCMValores.Parameters.AddWithValue("@idPuesto", ._txtPuestoTab.Text)
                ConexionBD.Open()
                .wneNoPers.Value = SCMValores.ExecuteScalar
                ConexionBD.Close()

                If Val(dsTabulador.Tables(0).Rows(0).Item("no_conceptos").ToString()) = 0 Or Val(dsFactura.Tables(0).Rows(0).Item("importe").ToString()) <= (Val(dsTabulador.Tables(0).Rows(0).Item("importe_autorizado").ToString()) * .wneNoPers.Value * .wneNoDias.Value) Then
                    'No hay montos pre-autorizados para ese concepto / Por debajo del importe autorizado
                    .wpePorcentAut.Value = 1
                    .wceSubtotal.Value = Val(dsFactura.Tables(0).Rows(0).Item("subtotal").ToString())
                    .wceIVA.Value = Val(dsFactura.Tables(0).Rows(0).Item("tot_tras_iva").ToString())
                    .wceTotal.Value = Val(dsFactura.Tables(0).Rows(0).Item("importe").ToString())
                Else
                    'Sobrepasa el imprte autorizado
                    'Determinar el porcentaje autorizado con respecto a la Factura
                    Dim porAut As Decimal = 0
                    porAut = (Val(dsTabulador.Tables(0).Rows(0).Item("importe_autorizado").ToString()) * .wneNoPers.Value * .wneNoDias.Value) / Val(dsFactura.Tables(0).Rows(0).Item("importe").ToString())
                    wceSubtotal.Value = Val(dsFactura.Tables(0).Rows(0).Item("subtotal").ToString()) * porAut
                    wceIVA.Value = Val(dsFactura.Tables(0).Rows(0).Item("tot_tras_iva").ToString()) * porAut
                    wceTotal.Value = Val(dsFactura.Tables(0).Rows(0).Item("importe").ToString()) * porAut
                    wpePorcentAut.Value = porAut
                End If
                sdaFactura.Dispose()
                dsFactura.Dispose()
                sdaTabulador.Dispose()
                dsTabulador.Dispose()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub validarVale()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaVale As New SqlDataAdapter
                Dim dsVale As New DataSet
                sdaVale.SelectCommand = New SqlCommand("select monto_subtotal as importe " +
                                                       "from dt_comp " +
                                                       "where id_dt_comp = @id_dt_comp ", ConexionBD)
                sdaVale.SelectCommand.Parameters.AddWithValue("@id_dt_comp", .gvConceptos.SelectedRow.Cells(0).Text)
                ConexionBD.Open()
                sdaVale.Fill(dsVale)
                ConexionBD.Close()

                'Datos de Conceptos
                Dim sdaTabulador As New SqlDataAdapter
                Dim dsTabulador As New DataSet
                sdaTabulador.SelectCommand = New SqlCommand("select no_conceptos, cg_concepto.id_tipoComp " +
                                                            "     , isnull(cantidad1, 0) * isnull(cgTab1.imp_sin_factura, 0) + isnull(cantidad2, 0) * isnull(cgTab2.imp_sin_factura, 0) as importe_autorizado " +
                                                            "     , case when cg_usuario_alim.id_usuario is not null then 0 else alimentos end as alimentos " +
                                                            "from cg_concepto " +
                                                            "  left join cg_tabulador as cgTab1 on cg_concepto.cve_concepto1 = cgTab1.cve_concepto and cgTab1.cve_puesto = @idPuesto " +
                                                            "  left join cg_tabulador as cgTab2 on cg_concepto.cve_concepto2 = cgTab2.cve_concepto and cgTab2.cve_puesto = @idPuesto " +
                                                            "  left join cg_usuario_alim on cg_usuario_alim.id_usuario = @idUsr " +
                                                            "where id_concepto = @idConcepto ", ConexionBD)
                sdaTabulador.SelectCommand.Parameters.AddWithValue("@idConcepto", .ddlConcepto.SelectedValue)
                sdaTabulador.SelectCommand.Parameters.AddWithValue("@idPuesto", ._txtPuestoTab.Text)
                sdaTabulador.SelectCommand.Parameters.AddWithValue("@idUsr", ._txtIdUsuario.Text)
                ConexionBD.Open()
                sdaTabulador.Fill(dsTabulador)
                ConexionBD.Close()

                'Determinar si el concepto corresponde a Alimentos
                If dsTabulador.Tables(0).Rows(0).Item("alimentos").ToString() = 1 Then
                    .wneNoPers.Value = 1
                End If

                If Val(dsTabulador.Tables(0).Rows(0).Item("no_conceptos").ToString()) = 0 Or Val(dsVale.Tables(0).Rows(0).Item("importe").ToString()) <= (Val(dsTabulador.Tables(0).Rows(0).Item("importe_autorizado").ToString()) * .wneNoPers.Value * .wneNoDias.Value) Then
                    'No hay montos pre-autorizados para ese concepto / Por debajo del importe autorizado
                    .wceSubtotal.Value = Val(dsVale.Tables(0).Rows(0).Item("importe").ToString())
                    .wceTotal.Value = Val(dsVale.Tables(0).Rows(0).Item("importe").ToString())
                Else
                    'Sobrepasa el imprte autorizado
                    .wceSubtotal.Value = Val(dsTabulador.Tables(0).Rows(0).Item("importe_autorizado").ToString()) * .wneNoPers.Value * .wneNoDias.Value
                    .wceTotal.Value = Val(dsTabulador.Tables(0).Rows(0).Item("importe_autorizado").ToString()) * .wneNoPers.Value * .wneNoDias.Value
                End If

                ' ''If Val(dsTabulador.Tables(0).Rows(0).Item("id_tipoComp").ToString()) = 2 Then
                ' ''    ._txtAutDir.Text = 1
                ' ''End If

                sdaVale.Dispose()
                dsVale.Dispose()
                sdaTabulador.Dispose()
                dsTabulador.Dispose()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarGridE()
        Try

            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim sdaEvi As New SqlDataAdapter
            Dim dsEvi As New DataSet
            gvEvidencias.DataSource = dsEvi
            gvEvidencias.Columns(0).Visible = True
            Dim queryE As String
            queryE = "SELECT id_dt_archivo_comp,  archivo as nombre, 'http://148.223.153.43/ProcAd/Evidencias Comp/' + archivo as ruta FROM dt_archivo_comp where id_ms_comp = @id_ms_comp and status = 'A' "
            sdaEvi.SelectCommand = New SqlCommand(queryE, ConexionBD)
            sdaEvi.SelectCommand.Parameters.AddWithValue("@id_ms_comp", Val(lblFolio.Text))
            ConexionBD.Open()
            sdaEvi.Fill(dsEvi)
            gvEvidencias.DataBind()
            gvEvidencias.Columns(0).Visible = False

            ConexionBD.Close()
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Public Sub sumarConceptos()
        With Me
            Try
                .litError.Text = ""

                Dim banVale As Integer = 0
                If .cbDevolucion.Checked = True Then
                    If (Not ((Not fuValeIng.PostedFile Is Nothing) And (fuValeIng.PostedFile.ContentLength > 0))) Or .txtValeIng.Text.Trim = "" Or .wceImporteDev.Text = "" Then
                        .litError.Text = "Favor de ingresar la información del Vale de Ingreso"
                        banVale = 1
                    End If
                End If

                If banVale = 0 Then
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim sdaSol As New SqlDataAdapter
                    Dim dsSol As New DataSet
                    Dim query As String
                    query = "select (select isnull(sum(monto_hospedaje) + sum(monto_alimentos) + sum(monto_casetas) + sum(monto_otros) + isnull((select vale_ingreso_imp from ms_comp where id_ms_comp = @id_ms_comp), 0), 0) as monto_ant " +
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
                            "          left join cg_concepto_cat on cg_concepto.id_concepto = cg_concepto_cat.id_concepto_cat " +
                            "        where dt_comp.id_ms_comp = @id_ms_comp " +
                            "          and dt_comp.tipo = 'T' " +
                            "          and cg_concepto_cat.gastos_viaje = 'S') as comp_pgv "
                    sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                    sdaSol.SelectCommand.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                    ConexionBD.Open()
                    sdaSol.Fill(dsSol)
                    ConexionBD.Close()
                    .wceTotalA.Value = dsSol.Tables(0).Rows(0).Item("anticipo_imp").ToString()
                    .wceTotalC.Value = dsSol.Tables(0).Rows(0).Item("comp_imp").ToString()
                    .wceTotalPGV.Value = dsSol.Tables(0).Rows(0).Item("comp_pgv").ToString()

                    'Considerar la devolución en caso de existir
                    If .cbDevolucion.Checked = True Then
                        .wceTotalC.Value = .wceTotalC.Value + .wceImporteDev.Value
                    End If

                    .wceTotalS.Value = .wceTotalA.Value - .wceTotalC.Value

                    .lblTotalA.Text = .wceTotalA.Text
                    .lblTotalC.Text = .wceTotalC.Text
                    .lblTotalS.Text = .wceTotalS.Text
                End If
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
                sdaPGV.SelectCommand.Parameters.AddWithValue("@idCC", Val(._txtIdCCPGV.Text))
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
                SCMValores.Parameters.AddWithValue("@idCC", Val(._txtIdCCPGV.Text))
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

#Region "Tabla Conceptos"

    Protected Sub gvConceptos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvConceptos.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""

                'Cargar información del Concepto
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaConcepto As New SqlDataAdapter
                Dim dsConcepto As New DataSet
                sdaConcepto.SelectCommand = New SqlCommand("select dt_comp.tipo " +
                                                           "     , id_concepto " +
                                                           "     , no_personas " +
                                                           "     , no_dias " +
                                                           "     , monto_subtotal " +
                                                           "     , monto_iva " +
                                                           "     , monto_total " +
                                                           "     , obs " +
                                                           "     , isnull(CFDI, 'xx') as CFDI " +
                                                           "from dt_comp " +
                                                           "where id_dt_comp = @id_dt_comp ", ConexionBD)
                sdaConcepto.SelectCommand.Parameters.AddWithValue("@id_dt_comp", Val(.gvConceptos.SelectedRow.Cells(0).Text))
                ConexionBD.Open()
                sdaConcepto.Fill(dsConcepto)
                ConexionBD.Close()

                'Llenar Lista de Conceptos
                If dsConcepto.Tables(0).Rows(0).Item("tipo").ToString() = "F" Then
                    'Por Factura
                    'Lista de Conceptos
                    Dim sdaConceptoF As New SqlDataAdapter
                    Dim dsConceptoF As New DataSet
                    'sdaConceptoF.SelectCommand = New SqlCommand("select id_concepto_comp " + _
                    '                                            "     , case when iva is null then abreviatura + ' - ' + concepto + ' ' + 'EXENTO' else abreviatura + ' - ' + concepto + ' ' + cast(cast((iva * 100) as int) as varchar(2)) + '%' end as nombre_concepto " + _
                    '                                            "from cg_concepto_comp " + _
                    '                                            "where status = 'A' " + _
                    '                                            "  and abreviatura = @abreviatura " + _
                    '                                            "  and (id_cc = 0 or id_cc = @idCC) " + _
                    '                                            "  and iva in (select iva " + _
                    '                                            "              from cg_concepto_comp " + _
                    '                                            "              where id_concepto_comp = @idConcepto) " + _
                    '                                            "order by nombre_concepto ", ConexionBD)
                    sdaConceptoF.SelectCommand = New SqlCommand("select cgConcepto.id_concepto_comp " +
                                                                "     , case when cgConcepto.iva is null then cgConcepto.abreviatura + ' - ' + cgConcepto.concepto + ' ' + 'EXENTO' else cgConcepto.abreviatura + ' - ' + cgConcepto.concepto + ' ' + cast(cast((cgConcepto.iva * 100) as int) as varchar(2)) + '%' end as nombre_concepto " +
                                                                "from cg_concepto_comp cgConcepto " +
                                                                "where cgConcepto.status = 'A' " +
                                                                "  and cgConcepto.abreviatura = @abreviatura " +
                                                                "  and (cgConcepto.id_cc = 0 or cgConcepto.id_cc = @idCC) " +
                                                                "  and (case when (select cgConceptoT.iva from cg_concepto_comp cgConceptoT where cgConceptoT.id_concepto_comp = @idConcepto) is null " +
                                                                "              then " +
                                                                "                case when cgConcepto.iva is null then 0 else 1 end " +
                                                                "              else " +
                                                                "                case when cgConcepto.iva = (select cgConceptoT.iva from cg_concepto_comp cgConceptoT where cgConceptoT.id_concepto_comp = @idConcepto) then 0 else 1 end " +
                                                                "       end) = 0 " +
                                                                "order by nombre_concepto ", ConexionBD)
                    sdaConceptoF.SelectCommand.Parameters.AddWithValue("@abreviatura", ._txtTipoGasto.Text)
                    sdaConceptoF.SelectCommand.Parameters.AddWithValue("@idCC", ._txtIdCC.Text)
                    sdaConceptoF.SelectCommand.Parameters.AddWithValue("@idConcepto", Val(dsConcepto.Tables(0).Rows(0).Item("id_concepto").ToString()))
                    .ddlConcepto.DataSource = dsConceptoF
                    .ddlConcepto.DataTextField = "nombre_concepto"
                    .ddlConcepto.DataValueField = "id_concepto_comp"
                    ConexionBD.Open()
                    sdaConceptoF.Fill(dsConceptoF)
                    .ddlConcepto.DataBind()
                    ConexionBD.Close()
                    sdaConceptoF.Dispose()
                    dsConceptoF.Dispose()

                    .cbFactura.Checked = True
                    .cbTabulador.Checked = False

                    .lblObsE.Visible = False
                Else
                    'Por Tabulador
                    'Lista de Conceptos
                    Dim sdaConceptoT As New SqlDataAdapter
                    Dim dsConceptoT As New DataSet
                    Dim query As String
                    query = "select id_concepto, abreviatura + ' - ' + nombre_concepto as nombre_concepto " +
                            "from cg_concepto " +
                            "where status = 'A' " +
                            "  and abreviatura = @abreviatura " +
                            "  and (id_cc = 0 or id_cc = @idCC) "
                    If ._txtAlimTab.Text = "N" Then
                        query = query + "  and (cve_concepto1 not in ('Desayuno/Cena', 'Comida') or cve_concepto1 is null) " +
                                        "  and (cve_concepto2 not in ('Desayuno/Cena', 'Comida') or cve_concepto2 is null) "
                    End If
                    If ._txtTaxiTab.Text = "N" Then
                        query = query + "  and nombre_concepto not in ('TAXIS') "
                    End If
                    query = query + "order by nombre_concepto "

                    sdaConceptoT.SelectCommand = New SqlCommand(query, ConexionBD)
                    sdaConceptoT.SelectCommand.Parameters.AddWithValue("@abreviatura", ._txtTipoGasto.Text)
                    sdaConceptoT.SelectCommand.Parameters.AddWithValue("@idCC", ._txtIdCC.Text)
                    .ddlConcepto.DataSource = dsConceptoT
                    .ddlConcepto.DataTextField = "nombre_concepto"
                    .ddlConcepto.DataValueField = "id_concepto"
                    ConexionBD.Open()
                    sdaConceptoT.Fill(dsConceptoT)
                    .ddlConcepto.DataBind()
                    ConexionBD.Close()
                    sdaConceptoT.Dispose()
                    dsConceptoT.Dispose()

                    .cbFactura.Checked = False
                    .cbTabulador.Checked = True

                    If .txtObs.Text.Trim = "" Then
                        .lblObsE.Visible = True
                    Else
                        .lblObsE.Visible = False
                    End If

                    .wpePorcentAut.Value = 1
                End If

                .ddlConcepto.SelectedValue = Val(dsConcepto.Tables(0).Rows(0).Item("id_concepto").ToString())
                .wneNoPers.Value = Val(dsConcepto.Tables(0).Rows(0).Item("no_personas").ToString())
                .wneNoDias.Value = Val(dsConcepto.Tables(0).Rows(0).Item("no_dias").ToString())
                .wceSubtotal.Value = Val(dsConcepto.Tables(0).Rows(0).Item("monto_subtotal").ToString())
                .wceIVA.Value = Val(dsConcepto.Tables(0).Rows(0).Item("monto_iva").ToString())
                .wceTotal.Value = Val(dsConcepto.Tables(0).Rows(0).Item("monto_total").ToString())
                .txtObs.Text = dsConcepto.Tables(0).Rows(0).Item("obs").ToString()
                ._txtCFDI.Text = dsConcepto.Tables(0).Rows(0).Item("CFDI").ToString()

                sdaConcepto.Dispose()
                dsConcepto.Dispose()

                .pnlConcepto.Visible = True
                '.gvConceptos.Enabled = False
                .btnSumar.Enabled = False
                .btnGuardar.Enabled = False
                .btnCancelar.Enabled = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Sumar / Enviar a Aprobación"

    Protected Sub btnSumar_Click(sender As Object, e As EventArgs) Handles btnSumar.Click
        sumarConceptos()
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        With Me
            Try
                sumarConceptos()
                Dim valorR As Integer = 0
                If .litError.Text = "" Then
                    If .wceTotalS.Value > 0 Then
                        .litError.Text = "Favor de ingresar la línea correspondiente para la devolución"
                    Else
                        Dim ban As Integer = 0

                        If .lblCC.Visible = True And Val(._txtIdCCPGV.Text) > 0 And ._txtOmitValPGV.Text = "N" Then
                            If .wceTotalPGV.Value > .wceTotalA.Value Then
                                'Obtener el Presupuesto Disponible del Centro de Costo
                                Dim montoPresupDisp As Integer = 0
                                Dim mes As String
                                If Val(._txtMesPGV.Text) < 10 Then
                                    mes = "0" + ._txtMesPGV.Text
                                Else
                                    mes = ._txtMesPGV.Text
                                End If

                                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                                SCMValores.Connection = ConexionBD
                                SCMValores.CommandText = ""
                                SCMValores.Parameters.Clear()
                                '- - - Versión Mensual - - -
                                SCMValores.CommandText = "select mes_" + mes + "_p + mes_" + mes + "_a - mes_" + mes + "_ep - mes_" + mes + "_r " +
                                                         "from ms_presupuesto " +
                                                         "where id_cc = @idCC " +
                                                         "  and año = @año "
                                ''- - - Versión Anual - - -
                                'SCMValores.CommandText = "select mes_01_p + mes_01_a - mes_01_ep - mes_01_r + mes_02_p + mes_02_a - mes_02_ep - mes_02_r + mes_03_p + mes_03_a - mes_03_ep - mes_03_r + mes_04_p + mes_04_a - mes_04_ep - mes_04_r + mes_05_p + mes_05_a - mes_05_ep - mes_05_r + mes_06_p + mes_06_a - mes_06_ep - mes_06_r + mes_07_p + mes_07_a - mes_07_ep - mes_07_r + mes_08_p + mes_08_a - mes_08_ep - mes_08_r + mes_09_p + mes_09_a - mes_09_ep - mes_09_r + mes_10_p + mes_10_a - mes_10_ep - mes_10_r + mes_11_p + mes_11_a - mes_11_ep - mes_11_r + mes_12_p + mes_12_a - mes_12_ep - mes_12_r " + _
                                '                         "from ms_presupuesto " + _
                                '                         "where id_cc = @idCC " + _
                                '                         "  and año = @año "
                                SCMValores.Parameters.AddWithValue("@idCC", Val(._txtIdCCPGV.Text))
                                SCMValores.Parameters.AddWithValue("@año", Val(._txtAñoPGV.Text))
                                ConexionBD.Open()
                                montoPresupDisp = SCMValores.ExecuteScalar()
                                ConexionBD.Close()
                                If montoPresupDisp < (.wceTotalPGV.Value - .wceTotalA.Value) Then
                                    .litError.Text = "El monto a comprobar excede el Presupuesto Disponible, favor de validarlo con el responsable del Centro de Costo para que solicite la Ampliación del Presupuesto de Gastos de Viaje en caso de que aplique"
                                    ban = 1
                                Else
                                    ban = 0
                                End If
                            End If
                        End If

                        If ban = 0 Then
                            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                            SCMValores.Connection = ConexionBD
                            Dim fecha As DateTime = Date.Now
                            While Val(._txtBan.Text) = 0
                                'Actualizar datos de la Solicitud
                                SCMValores.CommandText = ""
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "Declare @valorR as int; Execute Sp_U_ms_comp_12 @importe_tot, @monto_pgv_ep, @monto_pgv_ex, @id_ms_comp, @id_actividad, @id_ms_instancia, @id_usr,  @valorR output; Select @valorR  "
                                'SCMValores.CommandText = "update ms_comp set fecha_autoriza = NULL, fecha_aut_dir = NULL, importe_tot = @importe_tot, monto_pgv_ep = @monto_pgv_ep, monto_pgv_ex = @monto_pgv_ex where id_ms_comp = @id_ms_comp "
                                SCMValores.Parameters.AddWithValue("@importe_tot", .wceTotalS.Value)
                                If .wceTotalA.Value > 0 Then
                                    If .wceTotalPGV.Value > .wceTotalA.Value Then
                                        SCMValores.Parameters.AddWithValue("@monto_pgv_ep", .wceTotalA.Value)
                                    Else
                                        SCMValores.Parameters.AddWithValue("@monto_pgv_ep", .wceTotalPGV.Value)
                                    End If
                                Else
                                    SCMValores.Parameters.AddWithValue("@monto_pgv_ep", DBNull.Value)
                                End If
                                If .wceTotalPGV.Value - .wceTotalA.Value > 0 Then
                                    SCMValores.Parameters.AddWithValue("@monto_pgv_ex", .wceTotalPGV.Value - .wceTotalA.Value)
                                Else
                                    SCMValores.Parameters.AddWithValue("@monto_pgv_ex", DBNull.Value)
                                End If
                                SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                                'Validador
                                If _txtValidador.Text IsNot "" Then
                                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                                    SCMValores.Parameters.AddWithValue("@id_actividad", 115)
                                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                                Else
                                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                                    SCMValores.Parameters.AddWithValue("@id_actividad", 9)
                                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                                End If
                                ConexionBD.Open()
                                valorR = SCMValores.ExecuteScalar()
                                ConexionBD.Close()

                                If valorR = 0 Then
                                    Server.Transfer("Menu.aspx")
                                End If

                                If .lblCC.Visible = True And Val(._txtIdCCPGV.Text) > 0 Then
                                    actualizarPresup()
                                End If

                                ._txtBan.Text = 1

                                'Insertar devolución en caso de que aplique
                                If .cbDevolucion.Checked = True Then
                                    'Vale de Ingreso
                                    ' '' Ruta Local
                                    ''Dim sFileDir As String = "C:/ProcAd - Adjuntos/" 'Ruta en que se almacenará el archivo
                                    ' Ruta en Atenea
                                    Dim sFileDir As String = "D:\ProcAd - Adjuntos\" 'Ruta en que se almacenará el archivo
                                    Dim lMaxFileSize As Long = 10485760 'Equivalente a 10 Megabytes
                                    Dim sFileValeIng As String = ""
                                    If .pnlValeIng.Visible = True Then
                                        sFileValeIng = System.IO.Path.GetFileName(fuValeIng.PostedFile.FileName)
                                    End If

                                    'Insertar registro de Vale
                                    SCMValores.CommandText = ""
                                    SCMValores.Parameters.Clear()
                                    SCMValores.CommandText = "insert into dt_vale ( id_ms_comp,  vale_ingreso,  vale_ingreso_imp,  vale_ingreso_adj) " +
                                                             "             values (@id_ms_comp, @vale_ingreso, @vale_ingreso_imp, @vale_ingreso_adj) "
                                    SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                                    SCMValores.Parameters.AddWithValue("@vale_ingreso", .txtValeIng.Text.Trim)
                                    SCMValores.Parameters.AddWithValue("@vale_ingreso_imp", .wceImporteDev.Value)
                                    SCMValores.Parameters.AddWithValue("@vale_ingreso_adj", sFileValeIng)
                                    ConexionBD.Open()
                                    SCMValores.ExecuteNonQuery()
                                    ConexionBD.Close()

                                    'Obtener ID dt dt_vale
                                    Dim id_dt_vale As Integer = 0
                                    SCMValores.CommandText = "select max(id_dt_vale) from dt_vale where id_ms_comp = @id_ms_comp "
                                    ConexionBD.Open()
                                    id_dt_vale = SCMValores.ExecuteScalar
                                    ConexionBD.Close()
                                    If id_dt_vale > 0 Then
                                        'Se agrega el Id al nombre del archivo / Almacenar el archivo en la ruta especificada
                                        sFileValeIng = id_dt_vale.ToString + "ValeIC-" + sFileValeIng
                                        fuValeIng.PostedFile.SaveAs(sFileDir + sFileValeIng)
                                    End If

                                    'Insertar Concepto Devolución
                                    SCMValores.Parameters.Clear()
                                    SCMValores.CommandText = "insert into dt_comp ( id_ms_comp,  fecha_realizo,  tipo,  no_factura,  id_concepto,  nombre_concepto,  iva,  no_personas,  no_dias,  monto_subtotal,  monto_iva,  monto_total,  rfc,  proveedor,  origen_destino,  vehiculo,  obs,  CFDI) " +
                                                             " 			   values (@id_ms_comp, @fecha_realizo, @tipo, @no_factura, @id_concepto, @nombre_concepto, @iva, @no_personas, @no_dias, @monto_subtotal, @monto_iva, @monto_total, @rfc, @proveedor, @origen_destino, @vehiculo, @obs, @CFDI)"
                                    SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                                    SCMValores.Parameters.AddWithValue("@fecha_realizo", Date.Now)
                                    SCMValores.Parameters.AddWithValue("@tipo", "T")
                                    SCMValores.Parameters.AddWithValue("@CFDI", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@no_factura", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@rfc", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@proveedor", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@iva", 0)
                                    SCMValores.Parameters.AddWithValue("@id_concepto", .ddlConceptoDev.SelectedValue)
                                    SCMValores.Parameters.AddWithValue("@nombre_concepto", .ddlConceptoDev.SelectedItem.Text)
                                    SCMValores.Parameters.AddWithValue("@no_personas", 1)
                                    SCMValores.Parameters.AddWithValue("@no_dias", 1)
                                    SCMValores.Parameters.AddWithValue("@monto_subtotal", .wceImporteDev.Value)
                                    SCMValores.Parameters.AddWithValue("@monto_iva", 0)
                                    SCMValores.Parameters.AddWithValue("@monto_total", .wceImporteDev.Value)
                                    SCMValores.Parameters.AddWithValue("@origen_destino", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@vehiculo", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@obs", "Devolución en Corrección de Comprobación")
                                    ConexionBD.Open()
                                    SCMValores.ExecuteNonQuery()
                                    ConexionBD.Close()

                                End If

                                'Validador
                                'If _txtValidador.Text IsNot "" Then
                                'Actualizar Instancia
                                'SCMValores.Parameters.Clear()
                                'SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                                ' SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                                'SCMValores.Parameters.AddWithValue("@id_actividad", 115)
                                'ConexionBD.Open()
                                'SCMValores.ExecuteNonQuery()
                                'ConexionBD.Close()

                                'Registrar en Histórico
                                'SCMValores.Parameters.Clear()
                                'SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                                '                         "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                                'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                                'SCMValores.Parameters.AddWithValue("@id_actividad", 115)
                                'SCMValores.Parameters.AddWithValue("@fecha", fecha)
                                'SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                                'ConexionBD.Open()
                                'SCMValores.ExecuteNonQuery()
                                'ConexionBD.Close()
                                'Else
                                'Actualizar Instancia
                                'SCMValores.Parameters.Clear()
                                'SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                                'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                                'SCMValores.Parameters.AddWithValue("@id_actividad", 9)
                                'ConexionBD.Open()
                                'SCMValores.ExecuteNonQuery()
                                'ConexionBD.Close()

                                ''Registrar en Histórico
                                'SCMValores.Parameters.Clear()
                                'SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                                '                         "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                                'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                                'SCMValores.Parameters.AddWithValue("@id_actividad", 9)
                                'SCMValores.Parameters.AddWithValue("@fecha", fecha)
                                'SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                                'ConexionBD.Open()
                                'SCMValores.ExecuteNonQuery()
                                'ConexionBD.Close()
                                'End If



                                .gvUnidad.Enabled = False
                                .gvConceptos.Enabled = False
                                .pnlValeIng.Enabled = False
                                .btnSumar.Enabled = False
                                .btnGuardar.Enabled = False
                                .btnCancelar.Enabled = False
                            End While
                        End If
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Cancelar"

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        With Me
            Try

                sumarConceptos()
                Dim valor As Integer = 0
                If .litError.Text = "" Then
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    Dim fecha As DateTime = Date.Now
                    While Val(._txtBan.Text) = 0
                        'Actualizar datos de la Solicitud
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "Declare @valorR as int; Execute Sp_D_ms_comp12 @id_ms_comp, @id_ms_instancia, @id_actividad, @id_usr, @valorR output; Select @valorR  "
                        'SCMValores.CommandText = "update ms_comp set fecha_cancela = @fecha_cancela, monto_pgv_ep = 0, monto_pgv_ex = 0, status = 'ZU' where id_ms_comp = @id_ms_comp "
                        'SCMValores.Parameters.AddWithValue("@fecha_cancela", fecha)
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


                        actualizarPresup()

                        ._txtBan.Text = 1

                        'Actualizar Anticipos
                        Dim SCMValoresAnt As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValoresAnt.Connection = ConexionBD
                        SCMValoresAnt.Parameters.Clear()
                        SCMValoresAnt.CommandText = "update ms_anticipo " +
                                                    "set status = case status when 'EECP' then 'EE' when 'TRCP' then 'TR' else status end " +
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
                                                       "where uuid in (select CFDI from dt_comp where id_dt_comp = @id_dt_comp) " +
                                                       "  and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') "
                        SCMValoresDtFact.Parameters.Add("@id_dt_comp", SqlDbType.VarChar)

                        For i = 0 To .gvConceptos.Rows.Count - 1
                            If .gvConceptos.Rows(i).Cells(1).Text = "F" Then
                                'Actualizar Status de Factura
                                SCMValoresDtFact.Parameters("@id_dt_comp").Value = Val(.gvConceptos.Rows(i).Cells(0).Text)
                                ConexionBD.Open()
                                SCMValoresDtFact.ExecuteNonQuery()
                                ConexionBD.Close()
                            End If
                        Next

                        'Actualizar Cargas de Combustible con Tarjeta
                        Dim SCMCargaComb As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMCargaComb.Connection = ConexionBD
                        SCMCargaComb.Parameters.Clear()
                        SCMCargaComb.CommandText = "update dt_carga_comb_tar " +
                                                   "  set status = 'Z', id_usr_cancel = @id_usr_cancel, fecha_cancel = @fecha_cancel " +
                                                   "where cast(obs as varchar(50)) = cast(@id_ms_comp as varchar(50)) "
                        SCMCargaComb.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                        SCMCargaComb.Parameters.AddWithValue("@id_usr_cancel", Val(._txtIdUsuario.Text))
                        SCMCargaComb.Parameters.AddWithValue("@fecha_cancel", fecha)
                        ConexionBD.Open()
                        SCMCargaComb.ExecuteNonQuery()
                        ConexionBD.Close()

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

                        .gvUnidad.Enabled = False
                        .gvConceptos.Enabled = False
                        .pnlValeIng.Enabled = False
                        .btnSumar.Enabled = False
                        .btnGuardar.Enabled = False
                        .btnCancelar.Enabled = False
                    End While
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Panel Concepto"

    Protected Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValoresDtFact As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValoresDtFact.Connection = ConexionBD
                SCMValoresDtFact.Parameters.Clear()
                If .gvConceptos.SelectedRow.Cells(1).Text = "F" Then
                    'Actualizar la Factura para que esté disponible
                    SCMValoresDtFact.CommandText = "update dt_factura " +
                                                   "set status = 'P' " +
                                                   "where uuid in (select CFDI from dt_comp where id_dt_comp = @id_dt_comp) " +
                                                   "  and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') "
                    SCMValoresDtFact.Parameters.AddWithValue("@id_dt_comp", Val(.gvConceptos.SelectedRow.Cells(0).Text))
                    ConexionBD.Open()
                    SCMValoresDtFact.ExecuteNonQuery()
                    ConexionBD.Close()
                End If
                'Eliminar la partida de la comprobación
                SCMValoresDtFact.Parameters.Clear()
                SCMValoresDtFact.CommandText = "delete from dt_comp_linea " +
                                               "where id_dt_comp = @id_dt_comp "
                SCMValoresDtFact.Parameters.AddWithValue("@id_dt_comp", Val(.gvConceptos.SelectedRow.Cells(0).Text))
                ConexionBD.Open()
                SCMValoresDtFact.ExecuteNonQuery()
                ConexionBD.Close()
                SCMValoresDtFact.CommandText = "delete from dt_comp " +
                                               "where id_dt_comp = @id_dt_comp "
                ConexionBD.Open()
                SCMValoresDtFact.ExecuteNonQuery()
                ConexionBD.Close()

                .gvConceptos.SelectedIndex = -1

                actualizarConceptos()

                .pnlConcepto.Visible = False
                .gvConceptos.Enabled = True
                .btnSumar.Enabled = True
                .btnGuardar.Enabled = True
                .btnCancelar.Enabled = True
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnValidar_Click(sender As Object, e As EventArgs) Handles btnValidar.Click
        If Me.cbFactura.Checked = True Then
            validarFactura()
        Else
            validarVale()
        End If
    End Sub

    Public Sub valOrigDest()
        With Me
            Dim banOD As Integer = 0

            If (.ddlOrig1.SelectedValue = 0 And .ddlDest1.SelectedValue > 0) Or (.ddlOrig1.SelectedValue > 0 And .ddlDest1.SelectedValue = 0) Then
                banOD = 1
            Else
                If .ddlOrig1.SelectedValue = 0 Then
                    .txtOriDes1.Text = ""
                Else
                    .txtOriDes1.Text = .ddlOrig1.SelectedItem.Text + " - " + .ddlDest1.SelectedItem.Text
                End If
            End If
        End With
    End Sub

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        With Me
            Try
                .litError.Text = ""
                If _txtVal_Origen_Destino.Text = "S" Then
                    If ddlOrig1.SelectedItem.Text = "" Then
                        litError.Text = "El concepto debe tener seleccionado el origen y el destino."
                        Return
                    Else
                        valOrigDest()
                    End If
                End If
                If .cbFactura.Checked = True Then
                    validarFactura()
                Else
                    validarVale()
                End If

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                'Validar si hay cambio de precio en dt_comp_linea
                If .cbFactura.Checked = True Then
                    Dim SCMValoresImp As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValoresImp.Connection = ConexionBD
                    SCMValoresImp.Parameters.Clear()
                    SCMValoresImp.CommandText = "select case when monto_subtotal = @monto_subtotal then 0 else 1 end as valor " +
                                                "from dt_comp " +
                                                "where id_dt_comp = @id_dt_comp "
                    SCMValoresImp.Parameters.AddWithValue("@monto_subtotal", .wceSubtotal.Value)
                    SCMValoresImp.Parameters.AddWithValue("@id_dt_comp", Val(.gvConceptos.SelectedRow.Cells(0).Text))
                    ConexionBD.Open()
                    ._txtBanImporte.Text = SCMValoresImp.ExecuteScalar()
                    ConexionBD.Close()
                End If

                'Actualizar datos dt_comp
                Dim SCMValoresDtComp As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValoresDtComp.Connection = ConexionBD
                SCMValoresDtComp.Parameters.Clear()
                SCMValoresDtComp.CommandText = "update dt_comp " +
                                               "set id_concepto = @id_concepto, nombre_concepto = @nombre_concepto, no_personas = @no_personas, no_dias = @no_dias, monto_subtotal = @monto_subtotal, monto_iva = @monto_iva, monto_total = @monto_total, obs = @obs, origen_destino = @origen_destino " +
                                               "where id_dt_comp = @id_dt_comp "
                SCMValoresDtComp.Parameters.AddWithValue("@id_concepto", .ddlConcepto.SelectedValue)
                SCMValoresDtComp.Parameters.AddWithValue("@nombre_concepto", .ddlConcepto.SelectedItem.Text)
                SCMValoresDtComp.Parameters.AddWithValue("@no_personas", .wneNoPers.Value)
                SCMValoresDtComp.Parameters.AddWithValue("@no_dias", .wneNoDias.Value)
                SCMValoresDtComp.Parameters.AddWithValue("@monto_subtotal", wceSubtotal.Value)
                SCMValoresDtComp.Parameters.AddWithValue("@monto_iva", .wceIVA.Value)
                SCMValoresDtComp.Parameters.AddWithValue("@monto_total", .wceTotal.Value)
                SCMValoresDtComp.Parameters.AddWithValue("@obs", .txtObs.Text.Trim)
                SCMValoresDtComp.Parameters.AddWithValue("@id_dt_comp", Val(.gvConceptos.SelectedRow.Cells(0).Text))
                If txtOriDes1.Text = "" Then
                    SCMValoresDtComp.Parameters.AddWithValue("@origen_destino", DBNull.Value)
                Else

                    SCMValoresDtComp.Parameters.AddWithValue("@origen_destino", .txtOriDes1.Text.Trim)
                End If

                ConexionBD.Open()
                SCMValoresDtComp.ExecuteNonQuery()
                ConexionBD.Close()

                If .cbFactura.Checked = True And ._txtBanImporte.Text = 1 Then
                    'Eliminar en dt_comp_linea
                    Dim SCMValoresDtFact As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValoresDtFact.Connection = ConexionBD
                    SCMValoresDtFact.Parameters.Clear()
                    SCMValoresDtFact.CommandText = "delete from dt_comp_linea " +
                                                   "where id_dt_comp = @id_dt_comp "
                    SCMValoresDtFact.Parameters.AddWithValue("@id_dt_comp", Val(.gvConceptos.SelectedRow.Cells(0).Text))
                    ConexionBD.Open()
                    SCMValoresDtFact.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Insertar en dt_comp_linea
                    Dim SCMValoresDtCompL As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValoresDtCompL.Connection = ConexionBD
                    SCMValoresDtCompL.Parameters.Clear()
                    SCMValoresDtCompL.CommandText = "insert into dt_comp_linea (id_dt_comp, importe, descuento, tasa_iva, iva, tasa_ieps, ieps) " +
                                                    "select @idDtComp as id_dt_comp " +
                                                    "     , importe * @porcentAut as importe " +
                                                    "	  , descuento * @porcentAut as descuento " +
                                                    "	  , case when impuesto_tras_1 = 2 and tasa_tras_1 = 0.16 or impuesto_tras_2 = 2 and tasa_tras_2 = 0.16 or impuesto_tras_3 = 2 and tasa_tras_3 = 0.16 or impuesto_tras_4 = 2 and tasa_tras_4 = 0.16 then 0.16  " +
                                                    "		     when impuesto_tras_1 = 2 and tasa_tras_1 = 0.08 or impuesto_tras_2 = 2 and tasa_tras_2 = 0.08 or impuesto_tras_3 = 2 and tasa_tras_3 = 0.08 or impuesto_tras_4 = 2 and tasa_tras_4 = 0.08 then 0.08 " +
                                                    "		     when impuesto_tras_1 = 2 and tasa_tras_1 = 0 or impuesto_tras_2 = 2 and tasa_tras_2 = 0 or impuesto_tras_3 = 2 and tasa_tras_3 = 0 or impuesto_tras_4 = 2 and tasa_tras_4 = 0 then 0 " +
                                                    "	    end as tasa_iva " +
                                                    "	  , isnull(tot_tras_iva, 0) * @porcentAut as iva " +
                                                    "	  , case when impuesto_tras_1 = 3 then tasa_tras_1 " +
                                                    "		     when impuesto_tras_2 = 3 then tasa_tras_2 " +
                                                    "		     when impuesto_tras_3 = 3 then tasa_tras_3 " +
                                                    "			 when impuesto_tras_4 = 3 then tasa_tras_4 " +
                                                    "	    end as tasa_ieps " +
                                                    "	  , isnull(tot_tras_ieps, 0) * @porcentAut as ieps " +
                                                    "from dt_factura_linea " +
                                                    "where uuid = @uuid " +
                                                    "   and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') "
                    SCMValoresDtCompL.Parameters.AddWithValue("@idDtComp", Val(.gvConceptos.SelectedRow.Cells(0).Text))
                    SCMValoresDtCompL.Parameters.AddWithValue("@porcentAut", .wpePorcentAut.Value)
                    SCMValoresDtCompL.Parameters.AddWithValue("@uuid", ._txtCFDI.Text)
                    ConexionBD.Open()
                    SCMValoresDtCompL.ExecuteNonQuery()
                    ConexionBD.Close()
                End If

                .gvConceptos.SelectedIndex = -1

                actualizarConceptos()

                .pnlConcepto.Visible = False
                .gvConceptos.Enabled = True
                .btnSumar.Enabled = True
                .btnGuardar.Enabled = True
                .btnCancelar.Enabled = True
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

    Protected Sub cbDevolucion_CheckedChanged(sender As Object, e As EventArgs) Handles cbDevolucion.CheckedChanged
        If Me.cbDevolucion.Checked = True Then
            Me.pnlValeIng.Visible = True
        Else
            Me.pnlValeIng.Visible = False
        End If
    End Sub

#Region "Unidades"

#Region "Funciones"

    Public Sub buscarNoEco()

        With Me
            Try
                .litError.Text = ""
                Dim ConexionBDNAV As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBDNAV.ConnectionString = accessDB.conBD("NAV")
                Dim sdaNoEco As New SqlDataAdapter
                Dim dsNoEco As New DataSet
                Dim query As String = "select [No_] as id " +
                                      "     , [Núm_ Economico] + ' - ' + [Empresa que administra] + ' - ' + [Num_ Placas] as no_eco " +
                                      "from [TRACSA$Standard Units] " +
                                      "where [Núm_ Economico] like '%' + @noEco + '%' " +
                                      "  and [Núm_ Economico] <> '' " +
                                      "  and [Status] <> 2 "
                ' TR
                query = query + "  and ( [Tipo Unidad] in (0, 4) "
                ' TQ
                query = query + "  or [Tipo Unidad] in (1) "
                ' DL
                query = query + "  or [Tipo Unidad] in (2) )"

                sdaNoEco.SelectCommand = New SqlCommand(query, ConexionBDNAV)
                sdaNoEco.SelectCommand.Parameters.AddWithValue("@noEco", "%" + .txtUnidad.Text.ToUpper + "%")
                .ddlUnidad.DataSource = dsNoEco
                .ddlUnidad.DataTextField = "no_eco"
                .ddlUnidad.DataValueField = "id"
                ConexionBDNAV.Open()
                sdaNoEco.Fill(dsNoEco)
                .ddlUnidad.DataBind()
                ConexionBDNAV.Close()
                sdaNoEco.Dispose()
                dsNoEco.Dispose()
                .ddlUnidad.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
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

    Protected Sub cmdBuscarU_Click(sender As Object, e As EventArgs) Handles cmdBuscarU.Click
        buscarNoEco()
    End Sub

    Protected Sub cmdAgregarU_Click(sender As Object, e As EventArgs) Handles cmdAgregarU.Click
        With Me
            Try
                'Verificar que exista una unidad en la lista
                If .ddlUnidad.Items.Count <> 0 Then
                    Dim ConexionBDNAV As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBDNAV.ConnectionString = accessDB.conBD("NAV")
                    Dim SCMValoresNAV As SqlCommand = New System.Data.SqlClient.SqlCommand
                    Dim empresa As String
                    Dim noEconomico As String
                    SCMValoresNAV.Connection = ConexionBDNAV
                    SCMValoresNAV.CommandText = ""
                    SCMValoresNAV.Parameters.Clear()
                    'Obtener Empresa que administra de la Unidad en NAV
                    SCMValoresNAV.CommandText = "select [Empresa que administra] " +
                                                "from [TRACSA$Standard Units] " +
                                                "where [No_] = @No_ "
                    SCMValoresNAV.Parameters.AddWithValue("@No_", .ddlUnidad.SelectedValue)
                    ConexionBDNAV.Open()
                    empresa = SCMValoresNAV.ExecuteScalar
                    ConexionBDNAV.Close()
                    'Obtener Número económico de la Unidad en NAV
                    SCMValoresNAV.CommandText = "select [Núm_ Economico] " +
                                                "from [TRACSA$Standard Units] " +
                                                "where [No_] = @No_ "
                    ConexionBDNAV.Open()
                    noEconomico = SCMValoresNAV.ExecuteScalar
                    ConexionBDNAV.Close()

                    'Obtener datos de la Unidad
                    Dim sdaUnidad As New SqlDataAdapter
                    Dim dsUnidad As New DataSet
                    sdaUnidad.SelectCommand = New SqlCommand("select StandardUnits.[Empresa que administra] as empresa " +
                                                             "     , StandardUnits.No_ as codigo " +
                                                             "     , StandardUnits.Description as descripcion " +
                                                             "     , StandardUnits.[Núm_ Economico] as no_economico " +
                                                             "     , case StandardUnits.Status " +
                                                             "         when 0 then 'Patio' " +
                                                             "         when 1 then 'Activo' " +
                                                             "         when 2 then 'Baja' " +
                                                             "       end as status " +
                                                             "     , case StandardUnits.[Tipo Unidad] " +
                                                             "         when 0 then 'Tractocamion' " +
                                                             "         when 1 then 'Remolque' " +
                                                             "         when 2 then 'Dolly' " +
                                                             "         when 3 then 'Maquinaria' " +
                                                             "         when 4 then 'Torton' " +
                                                             "       end as tipo " +
                                                             "     , StandardUnits.[Num_ Serie Chasis] as no_serie " +
                                                             "     , StandardUnits.AñoModelo as modelo " +
                                                             "     , StandardUnits.Marca as marca " +
                                                             "     , StandardUnits.[Num_ Placas] as placas " +
                                                             "     , StandardUnits.Division as div " +
                                                             "     , DIVValue.Name as division " +
                                                             "     , StandardUnits.[Shortcut Dimension 3 Code] as zn " +
                                                             "     , ZNValue.Name as zona " +
                                                             "from [" + empresa + "$Standard Units] as StandardUnits " +
                                                             "  left join [" + empresa + "$Dimension Value] DIVValue on StandardUnits.Division = DIVValue.Code and DIVValue.[Dimension Code] = 'DIV' " +
                                                             "  left join [" + empresa + "$Dimension Value] ZNValue on StandardUnits.[Shortcut Dimension 3 Code] = ZNValue.Code and ZNValue.[Dimension Code] = 'ZN' " +
                                                             "where StandardUnits.Status <> 2 " +
                                                             "  and [Núm_ Economico] = @numEconomico ", ConexionBDNAV)
                    sdaUnidad.SelectCommand.Parameters.AddWithValue("@numEconomico", noEconomico)
                    ConexionBDNAV.Open()
                    sdaUnidad.Fill(dsUnidad)
                    ConexionBDNAV.Close()

                    'Insertar datos de Unidad en dt_unidad
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    'Registrar la unidad en la base de datos
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into dt_unidad ( id_ms_comp,  id_usuario,  empresa,  codigo,  descripcion,  no_economico,  status,  tipo,  no_serie,  modelo,  placas,  div,  division,  zn,  zona) " +
                                             "               values (@id_ms_comp, @id_usuario, @empresa, @codigo, @descripcion, @no_economico, @status, @tipo, @no_serie, @modelo, @placas, @div, @division, @zn, @zona)"
                    SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                    SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                    SCMValores.Parameters.AddWithValue("@empresa", dsUnidad.Tables(0).Rows(0).Item("empresa").ToString())
                    SCMValores.Parameters.AddWithValue("@codigo", dsUnidad.Tables(0).Rows(0).Item("codigo").ToString())
                    SCMValores.Parameters.AddWithValue("@descripcion", dsUnidad.Tables(0).Rows(0).Item("descripcion").ToString())
                    SCMValores.Parameters.AddWithValue("@no_economico", dsUnidad.Tables(0).Rows(0).Item("no_economico").ToString())
                    SCMValores.Parameters.AddWithValue("@status", dsUnidad.Tables(0).Rows(0).Item("status").ToString())
                    SCMValores.Parameters.AddWithValue("@tipo", dsUnidad.Tables(0).Rows(0).Item("tipo").ToString())
                    SCMValores.Parameters.AddWithValue("@no_serie", dsUnidad.Tables(0).Rows(0).Item("no_serie").ToString())
                    SCMValores.Parameters.AddWithValue("@modelo", dsUnidad.Tables(0).Rows(0).Item("modelo").ToString())
                    SCMValores.Parameters.AddWithValue("@placas", dsUnidad.Tables(0).Rows(0).Item("placas").ToString())
                    SCMValores.Parameters.AddWithValue("@div", dsUnidad.Tables(0).Rows(0).Item("div").ToString())
                    SCMValores.Parameters.AddWithValue("@division", dsUnidad.Tables(0).Rows(0).Item("division").ToString())
                    SCMValores.Parameters.AddWithValue("@zn", dsUnidad.Tables(0).Rows(0).Item("zn").ToString())
                    SCMValores.Parameters.AddWithValue("@zona", dsUnidad.Tables(0).Rows(0).Item("zona").ToString())
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()
                    sdaUnidad.Dispose()
                    dsUnidad.Dispose()

                    actualizarUnidades()
                Else
                    .litError.Text = "Favor de seleccionar una Unidad"
                End If
            Catch exc As Exception
                .litError.Text = exc.ToString
            End Try
        End With
    End Sub

    Protected Sub gvUnidad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvUnidad.SelectedIndexChanged
        With Me
            Try
                'Eliminar la Unidad
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD

                'Registrar la unidad en la base de datos
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "delete from dt_unidad where id_dt_unidad = @id_dt_unidad"
                SCMValores.Parameters.AddWithValue("@id_dt_unidad", .gvUnidad.SelectedRow.Cells(0).Text)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                actualizarUnidades()
            Catch exc As Exception
                .litError.Text = exc.ToString
            End Try
        End With
    End Sub


    Protected Sub btnAgregarE_Click(sender As Object, e As EventArgs) Handles btnAgregarE.Click
        Try
            litError.Text = ""

            If System.IO.Path.GetFileName(fuEvidencia.PostedFile.FileName) = "" Then
                litError.Text = "No hay ningun archivo adjunto"
            Else


                'Agregar evidencia'
                Dim rutaArchivo As String = "Evidencias Comp\" 'Ruta en que se almacenará el archivo
                Dim sFileNameAr As String = System.IO.Path.GetFileName(fuEvidencia.PostedFile.FileName)
                'Guarda archivo'
                fuEvidencia.PostedFile.SaveAs(Server.MapPath("Evidencias Comp\" + lblFolio.Text + "-" + sFileNameAr))


                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "Insert into dt_archivo_comp (id_ms_comp, archivo, status) " +
                                          "                     values (@id_ms_comp, @archivo, 'A') "
                SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(lblFolio.Text))
                SCMValores.Parameters.AddWithValue("@archivo", lblFolio.Text + "-" + sFileNameAr)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                llenarGridE()
            End If

        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvEvidencias_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvEvidencias.SelectedIndexChanged
        Try
            litError.Text = ""
            gvEvidencias.Columns(0).Visible = True
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
            SCMValores.Connection = ConexionBD
            SCMValores.Parameters.Clear()
            SCMValores.CommandText = "Update dt_archivo_comp set status = 'B' " +
                                      "      where id_dt_archivo_comp = @id_dt_archivo_comp "
            SCMValores.Parameters.AddWithValue("@id_dt_archivo_comp", Val(gvEvidencias.Rows(gvEvidencias.SelectedIndex).Cells(0).Text))
            ConexionBD.Open()
            SCMValores.ExecuteNonQuery()
            ConexionBD.Close()
            gvEvidencias.Columns(0).Visible = False

            llenarGridE()
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlConcepto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConcepto.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                If Me.cbFactura.Checked = True Then
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto_comp where id_concepto_comp = @id_concepto_comp "
                    SCMValores.Parameters.AddWithValue("@id_concepto_comp", ddlConcepto.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()

                Else
                    SCMValores.CommandText = "select val_orig_destino  from cg_concepto where id_concepto = @id_concepto "
                    SCMValores.Parameters.AddWithValue("@id_concepto", ddlConcepto.SelectedValue)
                    ConexionBD.Open()
                    ._txtVal_Origen_Destino.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If

            Catch ex As Exception
                .litError.Text = ex.Message.ToString()
            End Try

        End With
    End Sub

#End Region

End Class