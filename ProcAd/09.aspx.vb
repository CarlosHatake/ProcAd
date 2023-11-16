Public Class _09
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
                                "     , autorizador " +
                                "     , isnull(centro_costo, 'XX') as centro_costo " +
                                "     , isnull(division, 'XX') as division " +
                                "     , justificacion " +
                                "     , isnull(vale_ingreso, 'XX') as vale_ingreso " +
                                "     , vale_ingreso_adj " +
                                "     , isnull(evidencia_adj, 'XX') as evidencia_adj " +
                                "     , aut_dir " +
                                "     , director " +
                                "     , id_usr_autoriza " +
                                "     , isnull(obs_autorizador, '') as obs_autorizador " +
                                "     , (select count(*) from dt_unidad where dt_unidad.id_ms_comp = ms_comp.id_ms_comp) as unidades " +
                                "     , isnull(ms_comp.id_cc, 0) as id_cc " +
                                "     , isnull(ms_comp.año_pgv, 0) as año_pgv " +
                                "     , isnull(ms_comp.mes_pgv, 0) as mes_pgv " +
                                "from ms_instancia " +
                                "  left join ms_comp on ms_instancia.id_ms_sol = ms_comp.id_ms_comp and ms_instancia.tipo = 'C' " +
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
                        ._txtIdUsrAut.Text = dsSol.Tables(0).Rows(0).Item("id_usr_autoriza").ToString()
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
                                                                   "order by fecha_realizo ", ConexionBD)
                        sdaConcepto.SelectCommand.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                        .gvConceptos.Columns(0).Visible = True
                        .gvConceptos.Columns(1).Visible = True
                        .gvConceptos.Columns(2).Visible = True
                        '.gvConceptos.Columns(3).Visible = True
                        ConexionBD.Open()
                        sdaConcepto.Fill(dsConcepto)
                        .gvConceptos.DataBind()
                        ConexionBD.Close()
                        .gvConceptos.Columns(0).Visible = False
                        .gvConceptos.Columns(1).Visible = False
                        .gvConceptos.Columns(2).Visible = False
                        '.gvConceptos.Columns(3).Visible = False

                        pintarTabla(.gvConceptos)

                        Dim i As Integer = 0
                        For Each row As GridViewRow In .gvConceptos.Rows
                            If row.RowType = DataControlRowType.DataRow Then
                                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                                If dsConcepto.Tables(0).Rows(i).Item("no_valido").ToString() = "x" Then
                                    chkRow.Checked = True
                                Else
                                    chkRow.Checked = False
                                End If
                            End If
                            i = i + 1
                        Next

                        sdaConcepto.Dispose()
                        dsConcepto.Dispose()

                        'Evidencia'
                        Dim sdaEvi As New SqlDataAdapter
                        Dim dsEvi As New DataSet
                        gvEvidencias.DataSource = dsEvi
                        sdaEvi.SelectCommand = New SqlCommand("SELECT id_dt_archivo_comp,  archivo as nombre, 'http://148.223.153.43/ProcAd/Evidencias Comp/' + archivo as ruta FROM dt_archivo_comp where id_ms_comp = @id_ms_comp and status = 'A' ", ConexionBD)
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

#End Region


    Public Sub pintarTabla(ByRef gridView)
        With Me
            Try
                Dim i As Integer
                Dim ban As Integer
                For i = 0 To (gridView.Rows.Count() - 1)
                    ban = 0
                    If gridView.Rows(i).Cells(5).Text = "V" Then
                        ban = 1
                    End If
                    Select Case ban

                        Case 1
                            gridView.Rows(i).Cells(6).ForeColor = Color.Red
                            ' gridView.Rows(i).Cells(6).Font.Bold = True
                            gridView.Rows(i).Cells(12).ForeColor = Color.Red
                            ' gridView.Rows(i).Cells(12).Font.Bold = True
                    End Select
                    'If gridView.Rows(i).Cells(16).Text.Trim <> "&nbsp;" Then
                    '    If Val(gridView.Rows(i).Cells(16).Text) > Val(gridView.Rows(i).Cells(15).Text) Then
                    '        gridView.Rows(i).Cells(16).ForeColor = Color.Red
                    '        gridView.Rows(i).Cells(16).Font.Bold = True
                    '    Else
                    '        gridView.Rows(i).Cells(16).ForeColor = Color.Black
                    '        gridView.Rows(i).Cells(16).Font.Bold = False
                    '    End If
                    'End If

                Next
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#Region "Autorizar / Rechazar"

    Protected Sub btnAutoriza_Click(sender As Object, e As EventArgs) Handles btnAutoriza.Click
        With Me
            Try
                .litError.Text = ""

                Dim noValido As Integer = 0
                For Each row As GridViewRow In .gvConceptos.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                        If chkRow.Checked Then
                            noValido = noValido + 1
                        End If
                    End If
                Next

                If noValido <> 0 Then
                    .litError.Text = "Hay líneas marcadas como No Válidas, favor de validarlo"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    Dim fecha As DateTime = Date.Now
                    Dim actividad As Integer = 0
                    Dim valorR As Integer = 0
                    ' While Val(._txtBan.Text) = 0

                    'PROCEDIMIENTO ALMACENADO'
                    SCMValores.CommandText = "Declare @valorR int; Execute Sp_U_ms_comp_dt_comp @id_ms_comp, @obs_autorizador, @autoriza, @id_actividad, @id_ms_instancia, @id_usr, @valorR output; Select @valorR "
                    SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))

                    If Val(._txtIdUsrAut.Text) = Val(._txtIdUsuario.Text) Then
                        'Autorizador
                        If .txtObsAut.Text.Trim = "" Then
                            SCMValores.Parameters.AddWithValue("@obs_autorizador", DBNull.Value)
                        Else
                            SCMValores.Parameters.AddWithValue("@obs_autorizador", .txtObsAut.Text.Trim)
                        End If
                        SCMValores.Parameters.AddWithValue("@autoriza", "Aut")
                    Else
                        'Director
                        If .txtObsAut.Text.Trim = "" Then
                            SCMValores.Parameters.AddWithValue("@obs_autorizador", DBNull.Value)
                        Else
                            SCMValores.Parameters.AddWithValue("@obs_autorizador", .txtObsAut.Text.Trim)
                        End If
                        SCMValores.Parameters.AddWithValue("@autoriza", "Dir")
                        '._txtBan.Text = 1
                    End If

                    If Val(._txtIdUsrAut.Text) = Val(._txtIdUsuario.Text) Then
                        If .lblDirector.Visible = False Or .lblAutorizador.Text = .lblDirector.Text Then
                            SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                            SCMValores.Parameters.AddWithValue("@id_actividad", 10)
                            SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                            actividad = 10
                        Else
                            SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                            SCMValores.Parameters.AddWithValue("@id_actividad", 9)
                            SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        End If
                    Else
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 10)
                        SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        actividad = 10
                    End If
                    ConexionBD.Open()
                    valorR = SCMValores.ExecuteScalar()
                    ConexionBD.Close()

                    If valorR = 0 Then
                        Server.Transfer("Menu.aspx")
                    End If



                    ' NUEVO
                    'Actualizar Líneas de Comprobación
                    'Dim SCMValoresNoVal As SqlCommand = New System.Data.SqlClient.SqlCommand
                    'SCMValoresNoVal.Connection = ConexionBD
                    'SCMValoresNoVal.Parameters.Clear()
                    'SCMValoresNoVal.CommandText = "update dt_comp " +
                    '                              "set no_valido = NULL " +
                    '                              "where id_ms_comp = @id_ms_comp "
                    'SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                    'ConexionBD.Open()
                    'SCMValoresNoVal.ExecuteNonQuery()
                    'ConexionBD.Close()

                    If Val(._txtIdUsrAut.Text) = Val(._txtIdUsuario.Text) Then
                        'Autorizador
                        'Actualizar datos de la Solicitud
                        'SCMValores.CommandText = ""
                        'SCMValores.Parameters.Clear()
                        'SCMValores.CommandText = "update ms_comp set fecha_autoriza = @fecha_autoriza, obs_autorizador = @obs_autorizador, status = 'A' where id_ms_comp = @id_ms_comp "
                        'SCMValores.Parameters.AddWithValue("@fecha_autoriza", fecha)
                        'If .txtObsAut.Text.Trim = "" Then
                        'SCMValores.Parameters.AddWithValue("@obs_autorizador", DBNull.Value)
                        'Else
                        'SCMValores.Parameters.AddWithValue("@obs_autorizador", .txtObsAut.Text.Trim)
                        'End If
                        'SCMValores.Parameters.AddWithValue("@autoriza", "Aut")
                        'SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                        'ConexionBD.Open()
                        'SCMValores.ExecuteNonQuery()
                        'ConexionBD.Close()

                        '._txtBan.Text = 1

                        'Actualizar Anticipos
                        Dim SCMValoresAnt As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValoresAnt.Connection = ConexionBD
                        SCMValoresAnt.Parameters.Clear()
                        SCMValoresAnt.CommandText = "update ms_anticipo " +
                                                "set status = case status when 'EECP' then 'EECA' when 'TRCP' then 'TRCA' else status end " +
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
                                                   "set status = 'CA' " +
                                                   "where id_dt_factura = @id_dt_factura "
                        SCMValoresDtFact.Parameters.Add("@id_dt_factura", SqlDbType.VarChar)

                        For i = 0 To .gvConceptos.Rows.Count - 1
                            If .gvConceptos.Rows(i).Cells(2).Text = "F" Then
                                'Actualizar Status de Factura
                                SCMValoresDtFact.Parameters("@id_dt_factura").Value = Val(.gvConceptos.Rows(i).Cells(0).Text)
                                ConexionBD.Open()
                                SCMValoresDtFact.ExecuteNonQuery()
                                ConexionBD.Close()
                            End If
                        Next
                        'Else
                        'Director
                        'Actualizar datos de la Solicitud
                        'SCMValores.CommandText = ""
                        'SCMValores.Parameters.Clear()
                        'SCMValores.CommandText = "update ms_comp set fecha_aut_dir = @fecha_aut_dir, obs_autorizador = @obs_autorizador where id_ms_comp = @id_ms_comp "
                        ''SCMValores.Parameters.AddWithValue("@fecha_aut_dir", fecha)
                        'If .txtObsAut.Text.Trim = "" Then
                        '    SCMValores.Parameters.AddWithValue("@obs_autorizador", DBNull.Value)
                        'Else
                        '    SCMValores.Parameters.AddWithValue("@obs_autorizador", .txtObsAut.Text.Trim)
                        'End If
                        'SCMValores.Parameters.AddWithValue("@autoriza", "Dir")
                        'SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                        'ConexionBD.Open()
                        'SCMValores.ExecuteNonQuery()
                        'ConexionBD.Close()

                        '._txtBan.Text = 1
                    End If

                    'If Val(._txtIdUsrAut.Text) = Val(._txtIdUsuario.Text) Then
                    'If .lblDirector.Visible = False Or .lblAutorizador.Text = .lblDirector.Text Then
                    'Actualizar Instancia
                    'SCMValores.Parameters.Clear()
                    'SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                    'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    'SCMValores.Parameters.AddWithValue("@id_actividad", 10)
                    'ConexionBD.Open()
                    'SCMValores.ExecuteNonQuery()
                    'ConexionBD.Close()

                    ''Registrar en Histórico
                    'SCMValores.Parameters.Clear()
                    'SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                    '                         "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    'SCMValores.Parameters.AddWithValue("@id_actividad", 10)
                    'SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    'SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    'actividad = 10
                    'ConexionBD.Open()
                    'SCMValores.ExecuteNonQuery()
                    'ConexionBD.Close()

                    'Envío de Correo
                    'Dim Mensaje As New System.Net.Mail.MailMessage()
                    'Dim destinatario As String = ""
                    ''Obtener el Correo del Solicitante
                    'SCMValores.CommandText = "select cgEmpl.correo " +
                    '                         "from ms_comp " +
                    '                         "  left join cg_usuario on ms_comp.id_usr_solicita = cg_usuario.id_usuario " +
                    '                         "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                    '                         "where id_ms_comp = @id_ms_comp "
                    'SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                    'ConexionBD.Open()
                    'destinatario = SCMValores.ExecuteScalar()
                    'ConexionBD.Close()

                    'Mensaje.[To].Add(destinatario)
                    'Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                    ''Mensaje.From = New System.Net.Mail.MailAddress("soporte.unne.procad@gmail.com", "ProcAd", System.Text.Encoding.UTF8)
                    'Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                    'Mensaje.Subject = "ProcAd - Comprobación No. " + .lblFolio.Text + " Autorizada"
                    'Dim texto As String
                    'texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                    '        "La comprobación número <b>" + .lblFolio.Text + "</b> fue autorizada. <br></span>"
                    'Mensaje.Body = texto
                    'Mensaje.IsBodyHtml = True
                    'Mensaje.Priority = MailPriority.Normal

                    'Dim Servidor As New SmtpClient()
                    'Servidor.Host = "10.10.10.30"
                    'Servidor.Port = 587
                    'Servidor.EnableSsl = False
                    'Servidor.UseDefaultCredentials = False
                    'Servidor.Credentials = New System.Net.NetworkCredential("nprocad", "mc8HLB8lPe78")

                    'Try
                    '    Servidor.Send(Mensaje)
                    'Catch ex As System.Net.Mail.SmtpException
                    '    .litError.Text = ex.ToString
                    'End Try
                    'Else
                    'Actualizar Instancia
                    'SCMValores.Parameters.Clear()
                    'SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                    'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    'SCMValores.Parameters.AddWithValue("@id_actividad", 9)
                    ''ConexionBD.Open()
                    ''SCMValores.ExecuteNonQuery()
                    ''ConexionBD.Close()

                    '''Registrar en Histórico
                    ''SCMValores.Parameters.Clear()
                    ''SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                    ''                         "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    ''SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    ''SCMValores.Parameters.AddWithValue("@id_actividad", 9)
                    ''SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    'SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    'ConexionBD.Open()
                    'SCMValores.ExecuteNonQuery()
                    'ConexionBD.Close()
                    'End If
                    'Else
                    'Actualizar Instancia
                    'SCMValores.Parameters.Clear()
                    'SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                    '    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    '    SCMValores.Parameters.AddWithValue("@id_actividad", 10)
                    '    'ConexionBD.Open()
                    '    'SCMValores.ExecuteNonQuery()
                    '    'ConexionBD.Close()

                    '    ''Registrar en Histórico
                    '    'SCMValores.Parameters.Clear()
                    '    'SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                    '    '                         "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    '    'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    '    'SCMValores.Parameters.AddWithValue("@id_actividad", 10)
                    '    'SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    '    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    '    'ConexionBD.Open()
                    '    'SCMValores.ExecuteNonQuery()
                    '    'ConexionBD.Close()
                    '    actividad = 10
                    '    'Envío de Correo
                    '    'Dim Mensaje As New System.Net.Mail.MailMessage()
                    '    'Dim destinatario As String = ""
                    '    ''Obtener el Correo del Solicitante
                    '    'SCMValores.CommandText = "select cgEmpl.correo " +
                    '    '                         "from ms_comp " +
                    '    '                         "  left join cg_usuario on ms_comp.id_usr_solicita = cg_usuario.id_usuario " +
                    '    '                         "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                    '    '                         "where id_ms_comp = @id_ms_comp "
                    '    'SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                    '    'ConexionBD.Open()
                    '    'destinatario = SCMValores.ExecuteScalar()
                    '    'ConexionBD.Close()

                    '    'Mensaje.[To].Add(destinatario)
                    '    'Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                    '    ''Mensaje.From = New System.Net.Mail.MailAddress("soporte.unne.procad@gmail.com", "ProcAd", System.Text.Encoding.UTF8)
                    '    'Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                    '    'Mensaje.Subject = "ProcAd - Comprobación No. " + .lblFolio.Text + " Autorizada"
                    '    'Dim texto As String
                    '    'texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                    '    '        "La comprobación número <b>" + .lblFolio.Text + "</b> fue autorizada. <br></span>"
                    '    'Mensaje.Body = texto
                    '    'Mensaje.IsBodyHtml = True
                    '    'Mensaje.Priority = MailPriority.Normal

                    '    'Dim Servidor As New SmtpClient()
                    '    'Servidor.Host = "10.10.10.30"
                    '    'Servidor.Port = 587
                    '    'Servidor.EnableSsl = False
                    '    'Servidor.UseDefaultCredentials = False
                    '    'Servidor.Credentials = New System.Net.NetworkCredential("nprocad", "mc8HLB8lPe78")

                    '    'Try
                    '    '    Servidor.Send(Mensaje)
                    '    'Catch ex As System.Net.Mail.SmtpException
                    '    '    .litError.Text = ex.ToString
                    '    'End Try
                    'End If
                    'ConexionBD.Open()
                    'valorR = SCMValores.ExecuteScalar()
                    'ConexionBD.Close()

                    'If valorR = 0 Then
                    '    Server.Transfer("Menu.aspx")
                    'End If


                    If actividad = 10 Then
                        Dim Mensaje As New System.Net.Mail.MailMessage()
                        Dim destinatario As String = ""
                        'Obtener el Correo del Solicitante
                        SCMValores.CommandText = "select cgEmpl.correo " +
                                                 "from ms_comp " +
                                                 "  left join cg_usuario on ms_comp.id_usr_solicita = cg_usuario.id_usuario " +
                                                 "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                 "where id_ms_comp = @id_ms_comp "
                        'SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        destinatario = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        Mensaje.[To].Add(destinatario)
                        Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                        'Mensaje.From = New System.Net.Mail.MailAddress("soporte.unne.procad@gmail.com", "ProcAd", System.Text.Encoding.UTF8)
                        Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                        Mensaje.Subject = "ProcAd - Comprobación No. " + .lblFolio.Text + " Autorizada"
                        Dim texto As String
                        texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                                "La comprobación número <b>" + .lblFolio.Text + "</b> fue autorizada. <br></span>"
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


                    .btnAutoriza.Enabled = False
                    .btnRegresar.Enabled = False
                    .btnRechaza.Enabled = False
                    .gvConceptos.Enabled = False

                    Session("id_actividadM") = 9
                    Session("TipoM") = "C"
                    Server.Transfer("Menu.aspx")
                    'End While
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        With Me
            Try
                .litError.Text = ""

                Dim noValido As Integer = 0
                For Each row As GridViewRow In .gvConceptos.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                        If chkRow.Checked Then
                            noValido = noValido + 1
                        End If
                    End If
                Next

                If noValido = 0 Then
                    .litError.Text = "No hay líneas marcadas como No Válidas, favor de validarlo y seleccionar al menos una"
                Else
                    If .txtObsAut.Text.Trim = "" Then
                        .litError.Text = "Favor de indicar las observaciones correspondientes"
                    Else
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        Dim fecha As DateTime = Date.Now
                        Dim valorR As Integer = 0
                        'While Val(._txtBan.Text) = 0

                        '._txtBan.Text = 1

                        'Regreso por segundo Autorizador
                        If Val(._txtIdUsrAut.Text) <> Val(._txtIdUsuario.Text) Then
                            'Actualizar datos de la Comprobación
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "Declare @valorR int; Execute Sp_U_ms_comp_08  @obs_autorizador, @id_ms_comp, @corrige , @valorR output; Select @valorR"
                            '  SCMValores.CommandText = "update ms_comp set fecha_autoriza = NULL, obs_autorizador = @obs_autorizador, status = 'P' where id_ms_comp = @id_ms_comp "
                            SCMValores.Parameters.AddWithValue("@obs_autorizador", .txtObsAut.Text.Trim)
                            SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                            SCMValores.Parameters.AddWithValue("@corrige", "AutS")
                            ConexionBD.Open()
                            valorR = SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            If valorR = 0 Then
                                Server.Transfer("Menu.aspx")
                            End If

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
                                If .gvConceptos.Rows(i).Cells(2).Text = "F" Then
                                    'Actualizar Status de Factura
                                    SCMValoresDtFact.Parameters("@id_dt_factura").Value = Val(.gvConceptos.Rows(i).Cells(0).Text)
                                    ConexionBD.Open()
                                    SCMValoresDtFact.ExecuteNonQuery()
                                    ConexionBD.Close()
                                End If
                            Next
                        Else
                            valorR = 0
                            'Almacenar las observaciones
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "Declare @valorR int; Execute Sp_U_ms_comp_08  @obs_autorizador, @id_ms_comp, @corrige , @valorR output; Select @valorR"
                            'SCMValores.CommandText = "update ms_comp set obs_autorizador = @obs_autorizador where id_ms_comp = @id_ms_comp "
                            SCMValores.Parameters.AddWithValue("@obs_autorizador", .txtObsAut.Text.Trim)
                            SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                            SCMValores.Parameters.AddWithValue("@corrige", "Aut")
                            ConexionBD.Open()
                            valorR = SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            If valorR = 0 Then
                                Server.Transfer("Menu.aspx")
                            End If
                        End If

                        'Actualizar Líneas de Comprobación
                        Dim SCMValoresNoVal As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValoresNoVal.Connection = ConexionBD
                        SCMValoresNoVal.Parameters.Clear()
                        SCMValoresNoVal.CommandText = "update dt_comp " +
                                                      "set no_valido = @valor " +
                                                      "where id_dt_comp = @id_dt_comp "
                        SCMValoresNoVal.Parameters.Add("@id_dt_comp", SqlDbType.Int)
                        SCMValoresNoVal.Parameters.Add("@valor", SqlDbType.VarChar)
                        For Each row As GridViewRow In .gvConceptos.Rows
                            If row.RowType = DataControlRowType.DataRow Then
                                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                                If chkRow.Checked Then
                                    SCMValoresNoVal.Parameters("@valor").Value = "x"
                                Else
                                    SCMValoresNoVal.Parameters("@valor").Value = DBNull.Value
                                End If
                                SCMValoresNoVal.Parameters("@id_dt_comp").Value = Val(row.Cells(1).Text)
                                ConexionBD.Open()
                                SCMValoresNoVal.ExecuteNonQuery()
                                ConexionBD.Close()
                            End If
                        Next

                        'Actualizar Instancia
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 12)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Registrar en Histórico
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                                                 "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 12)
                        SCMValores.Parameters.AddWithValue("@fecha", fecha)
                        SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

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
                                "La comprobación número <b>" + .lblFolio.Text + "</b> fue rechazada, se encuentra en la actividad de Corregir Comprobación. <br>" +
                                "Observaciones: <b>" + .txtObsAut.Text + "</b>  <br></span>"
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
                        .btnRegresar.Enabled = False
                        .btnRechaza.Enabled = False
                        .gvConceptos.Enabled = False

                        Session("id_actividadM") = 9
                        Session("TipoM") = "C"
                        Server.Transfer("Menu.aspx")
                        'End While
                    End If
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

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim fecha As DateTime = Date.Now
                Dim valorR As Integer = 0
                'While Val(._txtBan.Text) = 0
                'Actualizar datos de la Comprobación
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "Declare @valorR int; Execute Sp_D_ms_comp  @obs_autorizador, @id_ms_comp, @rechaza , @valorR output; Select @valorR"
                If Val(._txtIdUsrAut.Text) = Val(._txtIdUsuario.Text) Then
                    'Autorizador
                    'SCMValores.CommandText = "update ms_comp set fecha_autoriza = @fecha_autoriza, obs_autorizador = @obs_autorizador, monto_pgv_ep = 0, monto_pgv_ex = 0, status = 'ZA' where id_ms_comp = @id_ms_comp "
                    SCMValores.Parameters.AddWithValue("@rechaza", "Aut")
                Else
                    'Director
                    'SCMValores.CommandText = "update ms_comp set fecha_aut_dir = @fecha_autoriza, obs_autorizador = @obs_autorizador, monto_pgv_ep = 0, monto_pgv_ex = 0, status = 'ZD' where id_ms_comp = @id_ms_comp "
                    SCMValores.Parameters.AddWithValue("@rechaza", "Dir")
                End If
                'SCMValores.Parameters.AddWithValue("@fecha_autoriza", fecha)
                If .txtObsAut.Text.Trim = "" Then
                    SCMValores.Parameters.AddWithValue("@obs_autorizador", DBNull.Value)
                Else
                    SCMValores.Parameters.AddWithValue("@obs_autorizador", .txtObsAut.Text.Trim)
                End If
                SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                ConexionBD.Open()
                valorR = SCMValores.ExecuteScalar()
                ConexionBD.Close()

                If valorR = 0 Then
                    Server.Transfer("Menu.aspx")


                Else

                    If Val(._txtIdCC.Text) > 0 Then
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

                    '._txtBan.Text = 1

                    'Actualizar Anticipos
                    Dim SCMValoresAnt As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValoresAnt.Connection = ConexionBD
                    SCMValoresAnt.Parameters.Clear()
                    If Val(._txtIdUsrAut.Text) = Val(._txtIdUsuario.Text) Then
                        'Autorizador
                        SCMValoresAnt.CommandText = "update ms_anticipo " +
                                                    "set status = case status when 'EECP' then 'EE' when 'TRCP' then 'TR' else status end " +
                                                    "where id_ms_anticipo = @id_ms_anticipo "
                    Else
                        'Director
                        SCMValoresAnt.CommandText = "update ms_anticipo " +
                                                    "set status = case status when 'EECA' then 'EE' when 'TRCA' then 'TR' else status end " +
                                                    "where id_ms_anticipo = @id_ms_anticipo "
                    End If
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
                        If .gvConceptos.Rows(i).Cells(2).Text = "F" Then
                            'Actualizar Status de Factura
                            SCMValoresDtFact.Parameters("@id_dt_factura").Value = Val(.gvConceptos.Rows(i).Cells(0).Text)
                            ConexionBD.Open()
                            SCMValoresDtFact.ExecuteNonQuery()
                            ConexionBD.Close()
                        End If
                    Next

                    'Actualizar Líneas de Comprobación
                    Dim SCMValoresNoVal As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValoresNoVal.Connection = ConexionBD
                    SCMValoresNoVal.Parameters.Clear()
                    SCMValoresNoVal.CommandText = "update dt_comp " +
                                                  "set no_valido = @valor " +
                                                  "where id_dt_comp = @id_dt_comp "
                    SCMValoresNoVal.Parameters.Add("@id_dt_comp", SqlDbType.Int)
                    SCMValoresNoVal.Parameters.Add("@valor", SqlDbType.VarChar)
                    For Each row As GridViewRow In .gvConceptos.Rows
                        If row.RowType = DataControlRowType.DataRow Then
                            Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                            If chkRow.Checked Then
                                SCMValoresNoVal.Parameters("@valor").Value = "x"
                            Else
                                SCMValoresNoVal.Parameters("@valor").Value = DBNull.Value
                            End If
                            SCMValoresNoVal.Parameters("@id_dt_comp").Value = Val(row.Cells(1).Text)
                            ConexionBD.Open()
                            SCMValoresNoVal.ExecuteNonQuery()
                            ConexionBD.Close()
                        End If
                    Next

                    'Actualizar Cargas de Combustible con Tarjeta
                    Dim SCMCargaComb As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMCargaComb.Connection = ConexionBD
                    SCMCargaComb.Parameters.Clear()
                    If Val(._txtIdUsrAut.Text) = Val(._txtIdUsuario.Text) Then
                        'Autorizador
                        SCMCargaComb.CommandText = "update dt_carga_comb_tar " +
                                                   "  set status = 'ZA', id_usr_cancel = @id_usr_cancel, fecha_cancel = @fecha_cancel " +
                                                   "where cast(obs as varchar(50)) = cast(@id_ms_comp as varchar(50)) "
                    Else
                        'Director
                        SCMCargaComb.CommandText = "update dt_carga_comb_tar " +
                                                   "  set status = 'ZD', id_usr_cancel = @id_usr_cancel, fecha_cancel = @fecha_cancel " +
                                                   "where cast(obs as varchar(50)) = cast(@id_ms_comp as varchar(50)) "
                    End If
                    SCMCargaComb.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                    SCMCargaComb.Parameters.AddWithValue("@id_usr_cancel", Val(._txtIdUsuario.Text))
                    SCMCargaComb.Parameters.AddWithValue("@fecha_cancel", fecha)
                    ConexionBD.Open()
                    SCMCargaComb.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Actualizar Instancia
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 29)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Registrar en Histórico
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                                             "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 29)
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

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
                    Mensaje.Subject = "ProcAd - Comprobación No. " + .lblFolio.Text + " Rechazada"
                    Dim texto As String
                    texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                            "La comprobación número <b>" + .lblFolio.Text + "</b> fue rechazada. <br>" +
                            "Observaciones: <b>" + .txtObsAut.Text + "</b>  <br></span>"
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
                    .btnRegresar.Enabled = False
                    .btnRechaza.Enabled = False
                    .gvConceptos.Enabled = False

                    Session("id_actividadM") = 9
                    Session("TipoM") = "C"
                    Server.Transfer("Menu.aspx")
                End If


                'End While
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class