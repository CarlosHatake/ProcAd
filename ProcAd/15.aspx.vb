Public Class _15
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
                        query = "select id_ms_factura " +
                                "     , empleado " +
                                "     , empresa " +
                                "     , autorizador " +
                                "     , isnull(division, centro_costo) as centro_costo " +
                                "     , case when division is null then 'CC' else 'DIV' end as dimension " +
                                "     , rfc_emisor " +
                                "     , razon_emisor " +
                                "     , dt_factura.regimen_fiscal " +
                                "     , isnull(tabla_comp, 'XX') as tabla_comp " +
                                "     , isnull(adjunto_opcional, 'XX') as adjunto_opcional " +
                                "     , ms_factura.especificaciones " +
                                "from ms_factura " +
                                "  left join ms_instancia on ms_factura.id_ms_factura = ms_instancia.id_ms_sol and ms_instancia.tipo = 'F' " +
                                "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                "where id_ms_instancia = @idMsInst "
                        sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@idMsInst", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()
                        .lblFolio.Text = dsSol.Tables(0).Rows(0).Item("id_ms_factura").ToString()
                        .lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("empleado").ToString()
                        .lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                        .lblRFC.Text = dsSol.Tables(0).Rows(0).Item("rfc_emisor").ToString()
                        .lblRegimenF.Text = dsSol.Tables(0).Rows(0).Item("regimen_fiscal").ToString()
                        .lblAutorizador.Text = dsSol.Tables(0).Rows(0).Item("autorizador").ToString()
                        If dsSol.Tables(0).Rows(0).Item("dimension").ToString() = "CC" Then
                            .lbl_CC.Text = "Centro de Costo:"
                        Else
                            .lbl_CC.Text = "División:"
                        End If
                        .lblCC.Text = dsSol.Tables(0).Rows(0).Item("centro_costo").ToString()
                        .lblProveedor.Text = dsSol.Tables(0).Rows(0).Item("razon_emisor").ToString()
                        .txtEspecificaciones.Text = dsSol.Tables(0).Rows(0).Item("especificaciones").ToString()
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
                        sdaFactura.SelectCommand = New SqlCommand("select fecha_emision " +
                                                                  "     , uuid " +
                                                                  "     , 'http://148.223.153.43/Facturas/' + dt_factura.rfc_receptor + '/' + format(datepart(year, dt_factura.fecha_emision), '00') + '/' + upper(format(dt_factura.fecha_emision, 'MMMM', 'es-es')) + '/' + dt_factura.movimiento + '/' + dt_factura.tipo + '/' + dt_factura.uuid + '.pdf' as path " +
                                                                  "     , 'PDF' as pdf " +
                                                                  "     , serie " +
                                                                  "     , folio " +
                                                                  "     , lugar_exp " +
                                                                  "     , forma_pago " +
                                                                  "     , moneda " +
                                                                  "     , subtotal " +
                                                                  "     , importe " +
                                                                  "from ms_factura " +
                                                                  "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                                                  "where id_ms_factura = @idMsFactura ", ConexionBD)
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
                        sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " +
                                                                   "     , 'http://148.223.153.43/ProcAd - Adjuntos IngFact/' + cast(id_dt_archivo as varchar(20)) + '-' + nombre as path " +
                                                                   "from dt_archivo " +
                                                                   "where id_ms_factura = @idMsFactura " +
                                                                   "  and tipo = 'A' " +
                                                                   "union " +
                                                                   "select tabla_comp as archivo " +
                                                                   "     , 'http://148.223.153.43/ProcAd - Adjuntos/' + cast(id_ms_factura as varchar(20)) + 'TabCon-' + tabla_comp as path " +
                                                                   "from ms_factura " +
                                                                   "where id_ms_factura = @idMsFactura " +
                                                                   "  and tabla_comp is not null " +
                                                                   "union " +
                                                                   "select adjunto_opcional as archivo " +
                                                                   "     , 'http://148.223.153.43/ProcAd - Adjuntos/' + cast(id_ms_factura as varchar(20)) + 'AdjOpc-' + adjunto_opcional as path " +
                                                                   "from ms_factura " +
                                                                   "where id_ms_factura = @idMsFactura " +
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
                        sdaEvidencias.SelectCommand = New SqlCommand("select nombre as archivo " +
                                                                     "     , 'http://148.223.153.43/ProcAd - Adjuntos IngFact/' + cast(id_dt_archivo as varchar(20)) + '-' + nombre as path " +
                                                                     "from dt_archivo " +
                                                                     "where id_ms_factura = @idMsFactura " +
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
                        sdaActivos.SelectCommand = New SqlCommand("select codigo as no_economico " +
                                                                  "     , descripcion " +
                                                                  "from dt_af " +
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

                        'Partidas
                        limpiarPantalla()

                        'Panel
                        .pnlInicio.Enabled = True


                        .txtDiv.Enabled = False


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

    Public Sub llenarPartidas()
        With Me
            Try
                'Llenar Grid
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaPartidas As New SqlDataAdapter
                Dim dsPartidas As New DataSet
                .gvPartidas.DataSource = dsPartidas
                .gvPartidas.Columns(0).Visible = True
                sdaPartidas.SelectCommand = New SqlCommand("SP_C_dt_factura_divLlenado @id_ms_factura, @id_dt_factura", ConexionBD)

                'sdaPartidas.SelectCommand = New SqlCommand("select id_dt_partida " + _
                '                                           "     , cuenta_c1 +  '-' + cuenta_c2 as cuenta_c " + _
                '                                           "     , porcent " + _
                '                                           "     , dt_factura.subtotal * (porcent/100) as [Importe Partida] " + _
                '                                           "     , dt_partida.centro_costo " + _
                '                                           "     , dt_partida.division " + _
                '                                           "     , zona " + _
                '                                           "from dt_partida " + _
                '                                           "  left join ms_factura on dt_partida.id_ms_factura = ms_factura.id_ms_factura " + _
                '                                           "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " + _
                '                                           "where dt_partida.id_ms_factura = @idMsFactura ", ConexionBD)
                'sdaPartidas.SelectCommand.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))

                sdaPartidas.SelectCommand.Parameters.AddWithValue("@id_ms_factura", .lblFolio.Text)
                sdaPartidas.SelectCommand.Parameters.AddWithValue("@id_dt_factura", DBNull.Value)

                ConexionBD.Open()
                sdaPartidas.Fill(dsPartidas)
                .gvPartidas.DataBind()
                ConexionBD.Close()
                sdaPartidas.Dispose()
                dsPartidas.Dispose()
                .gvPartidas.Columns(0).Visible = False
                .gvPartidas.Columns(1).Visible = False
                .gvPartidas.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub limpiarPantalla()
        With Me
            llenarPartidas()
            .ibtnAlta.Enabled = True
            .ibtnBaja.Enabled = False
            .ibtnBaja.ImageUrl = "images\Trash_i2.png"
            .ibtnModif.Enabled = False
            .ibtnModif.ImageUrl = "images\Edit_i2.png"
            .pnlInicio.Enabled = True
            .pnlPartida.Visible = False
            .btnAceptar.Visible = True
            .btnRechaza.Visible = True
            .ddlDivD.Items.Clear()

            'Calcular Porcentaje Asignado
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
            SCMValores.Connection = ConexionBD
            SCMValores.CommandText = ""
            SCMValores.Parameters.Clear()
            SCMValores.CommandText = "select format(isnull(sum(porcent), 0), '##0.00#################', 'en-US') as porcentAsig " +
                                     "from dt_factura_div " +
                                     "where id_ms_factura = @id_ms_factura "
            SCMValores.Parameters.AddWithValue("@id_ms_factura", .lblFolio.Text)
            ConexionBD.Open()
            .lblPorcentAsig.Text = SCMValores.ExecuteScalar
            ConexionBD.Close()

        End With
    End Sub

    Public Sub habilitarCampos(ByVal valor As Boolean)
        With Me
            .txtCuentaC1.Enabled = valor
            .txtCuentaC2.Enabled = valor
            If valor = True Then
                If .rblTipoAsig.SelectedValue = "P" Then
                    .wnePorcent.Enabled = True
                    .wceImporte.Enabled = False
                Else
                    'Por Importe
                    .wnePorcent.Enabled = False
                    .wceImporte.Enabled = True
                End If
            Else
                .wnePorcent.Enabled = valor
                .wceImporte.Enabled = valor
            End If
            .cbCC.Enabled = valor
            .txtCC.Enabled = valor
            .cbDiv.Enabled = valor
            .txtDiv.Enabled = valor
            .cbZona.Enabled = valor
            .txtZona.Enabled = valor
        End With
    End Sub

    Public Sub llenarDivD()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaDiv As New SqlDataAdapter
                Dim dsDiv As New DataSet
                sdaDiv.SelectCommand = New SqlCommand(" select codigo, nombre " +
                                                      " from bd_Empleado.dbo.cg_div " +
                                                      " where id_empresa = (select id_empresa from bd_Empleado.dbo.cg_empresa where nombre = @empresa) " +
                                                      " and status = 'A' order by nombre  ", ConexionBD)

                sdaDiv.SelectCommand.Parameters.AddWithValue("@empresa", .lblEmpresa.Text)
                .ddlDivD.DataSource = dsDiv
                .ddlDivD.DataTextField = "nombre"
                .ddlDivD.DataValueField = "codigo"
                ConexionBD.Open()
                sdaDiv.Fill(dsDiv)
                .ddlDivD.DataBind()
                ConexionBD.Close()
                sdaDiv.Dispose()
                dsDiv.Dispose()

                .ddlDivD.Items.Insert(0, "")
                .txtDiv.Text = .ddlDivD.SelectedValue
                .upDivD.Update()
                .upDivDDl.Update()



            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub cbDiv_CheckedChanged(sender As Object, e As EventArgs) Handles cbDiv.CheckedChanged
        With Me
            If .cbDiv.Checked = True Then
                llenarDivD()
                'If .ddlDivD.SelectedIndex = 0 Then
                'txtDiv.Text = ""
                'Else
                .txtDiv.Text = ddlDivD.SelectedValue
                .txtDiv.Enabled = True

                'End If
            ElseIf .cbDiv.Checked = False Then
                .ddlDivD.Items.Clear()
                .txtDiv.Text = ""
                .txtDiv.Enabled = False

            End If
        End With
    End Sub

    Protected Sub ddlDivD_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDivD.SelectedIndexChanged
        With Me
            'If .ddlDivD.SelectedIndex = 0 Then
            'txtDiv.Text = ""
            'Else
            .txtDiv.Text = ddlDivD.SelectedValue

            'End If
            .upDivD.Update()
        End With
    End Sub

    Public Sub localizar(ByVal idRegistro)
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaPartidas As New SqlDataAdapter
                Dim dsPartidas As New DataSet
                .gvPartidas.DataSource = dsPartidas
                sdaPartidas.SelectCommand = New SqlCommand("select id_dt_factura_div " +
                                                           "     , cuenta_c1, cuenta_c2 " +
                                                           "     , porcent " +
                                                           "     , isnull(centro_costo, 'XX') as centro_costo " +
                                                           "     , isnull(division, 'XX') as division " +
                                                           "     , isnull(zona, 'XX') as zona " +
                                                           "from dt_factura_div " +
                                                           "where id_dt_factura_div = @idDtFac ", ConexionBD)
                sdaPartidas.SelectCommand.Parameters.AddWithValue("@idDtFac", idRegistro)
                'sdaPartidas.SelectCommand = New SqlCommand("select id_dt_partida " + _
                '                                           "     , cuenta_c1, cuenta_c2 " + _
                '                                           "     , porcent " + _
                '                                           "     , isnull(centro_costo, 'XX') as centro_costo " + _
                '                                           "     , isnull(division, 'XX') as division " + _
                '                                           "     , isnull(zona, 'XX') as zona " + _
                '                                           "from dt_partida " + _
                '                                           "where id_dt_partida = @idDtPartida ", ConexionBD)
                'sdaPartidas.SelectCommand.Parameters.AddWithValue("@idDtPartida", idRegistro)
                ConexionBD.Open()
                sdaPartidas.Fill(dsPartidas)
                ConexionBD.Close()
                'Llenar Campos
                .txtCuentaC1.Text = dsPartidas.Tables(0).Rows(0).Item("cuenta_c1").ToString()
                .txtCuentaC2.Text = dsPartidas.Tables(0).Rows(0).Item("cuenta_c2").ToString()
                'Determinar número de decimales
                Dim noDecimales As Integer = 0
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                'SCMValores.CommandText = "select len(format(((select porcent as porcentAsig " + _
                '                         "                    from dt_partida " + _
                '                         "                    where id_ms_factura = @id_ms_factura " + _
                '                         "                      and id_dt_partida = @idDtPartida)) " + _
                '                         "            - floor(((select porcent as porcentAsig " + _
                '                         "                      from dt_partida " + _
                '                         "                      where id_ms_factura = @id_ms_factura " + _
                '                         "                        and id_dt_partida = @idDtPartida))), '#.###################')) - 1 "
                'SCMValores.Parameters.AddWithValue("@id_ms_factura", .lblFolio.Text)
                'SCMValores.Parameters.AddWithValue("@idDtPartida", idRegistro)
                SCMValores.CommandText = "select len(format(((select porcent as porcentAsig " +
                                         "                    from dt_factura_div " +
                                         "                    where id_ms_factura = @id_ms_factura " +
                                         "                      and id_dt_factura_div = @idDtFac)) " +
                                         "            - floor(((select porcent as porcentAsig " +
                                         "                      from dt_factura_div " +
                                         "                      where id_ms_factura = @id_ms_factura " +
                                         "                        and id_dt_factura_div = @idDtFac))), '#.###################')) - 1 "
                SCMValores.Parameters.AddWithValue("@id_ms_factura", .lblFolio.Text)
                SCMValores.Parameters.AddWithValue("@idDtFac", idRegistro)
                ConexionBD.Open()
                noDecimales = SCMValores.ExecuteScalar()
                ConexionBD.Close()
                If noDecimales < 3 Then
                    'Por Porcentaje
                    .rblTipoAsig.SelectedValue = "P"
                    .wnePorcent.Value = Val(dsPartidas.Tables(0).Rows(0).Item("porcent").ToString())
                Else
                    'Por Importe
                    .rblTipoAsig.SelectedValue = "I"
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = " select cast(cast(subtotal as float) * (cast(porcent as float)/100) as money)" +
                                             " from dt_factura_div" +
                                             " left Join ms_factura on dt_factura_div.id_ms_factura = ms_factura.id_ms_factura " +
                                             " left Join dt_factura on ms_factura.CFDI = dt_factura.uuid And dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA')" +
                                             " where id_dt_factura_div = @id_dt_factura_divv"
                    SCMValores.Parameters.AddWithValue("@id_dt_factura_divv", idRegistro)

                    'SCMValores.CommandText = "select cast(cast(subtotal as float) * (cast(porcent as float)/100) as money) " + _
                    '                         "from dt_partida " + _
                    '                         "  left join ms_factura on dt_partida.id_ms_factura = ms_factura.id_ms_factura " + _
                    '                         "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " + _
                    '                         "where id_dt_partida = @idDtPartida "
                    ConexionBD.Open()
                    .wceImporte.Value = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If
                If dsPartidas.Tables(0).Rows(0).Item("centro_costo").ToString() = "XX" Then
                    .cbCC.Checked = False
                    .txtCC.Text = ""
                    .txtCC.Enabled = False
                Else
                    .cbCC.Checked = True
                    .txtCC.Text = dsPartidas.Tables(0).Rows(0).Item("centro_costo").ToString()
                    .txtCC.Enabled = True
                End If
                If dsPartidas.Tables(0).Rows(0).Item("division").ToString() = "XX" Then
                    .cbDiv.Checked = False
                    .txtDiv.Text = ""
                    .txtDiv.Enabled = False
                Else
                    llenarDivD()
                    .txtDiv.Text = dsPartidas.Tables(0).Rows(0).Item("division").ToString()
                    .ddlDivD.SelectedIndex = ddlDivD.Items.IndexOf(ddlDivD.Items.FindByValue(.txtDiv.Text)) 'llenar el txt con el value del DDL 
                    .txtDiv.Enabled = True
                    .cbDiv.Checked = True
                End If
                If dsPartidas.Tables(0).Rows(0).Item("zona").ToString() = "XX" Then
                    .cbZona.Checked = False
                    .txtZona.Text = ""
                    .txtZona.Enabled = False
                Else
                    .cbZona.Checked = True
                    .txtZona.Text = dsPartidas.Tables(0).Rows(0).Item("zona").ToString()
                    .txtZona.Enabled = True
                End If
                sdaPartidas.Dispose()
                dsPartidas.Dispose()

                .pnlInicio.Enabled = True
                .pnlPartida.Visible = True
                .btnAceptar.Visible = False
                .btnRechaza.Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Function validar()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMTemp As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMTemp.Connection = ConexionBD
                Dim conteo As Integer = 0
                Dim query As String
                SCMTemp.CommandText = ""
                SCMTemp.Parameters.Clear()
                Select Case ._txtTipoMov.Text 'Tipo de Ajuste (alta, baja o modificación)
                    Case "A"
                        query = " select count(*) " +
                                " from dt_factura_div " +
                                " where id_ms_factura = @id_ms_factura " +
                                " and cuenta_c1 = @cuenta_c1 " +
                                " and cuenta_c2 = @cuenta_c2 "
                        If .cbCC.Checked = True Then
                            query = query + "  and centro_costo = @centro_costo "
                        Else
                            query = query + "  and centro_costo is null "
                        End If
                        If .cbDiv.Checked = True Then
                            query = query + "  and division = @division "
                        Else
                            query = query + "  and division is null "
                        End If
                        If .cbZona.Checked = True Then
                            query = query + "  and zona = @zona "
                        Else
                            query = query + "  and zona is null "
                        End If
                        SCMTemp.CommandText = query
                    Case Else
                        query = " select count(*) " +
                                " from dt_factura_div " +
                                " where id_ms_factura = @id_ms_factura " +
                                " and id_dt_factura_div <> @id_dt_factura_div " +
                                " and cuenta_c1 = @cuenta_c1 " +
                                " and cuenta_c2 = @cuenta_c2 "

                        If .cbCC.Checked = True Then
                            query = query + "  and centro_costo = @centro_costo "
                        Else
                            query = query + "  and centro_costo is null "
                        End If
                        If .cbDiv.Checked = True Then
                            query = query + "  and division = @division "
                        Else
                            query = query + "  and division is null "
                        End If
                        If .cbZona.Checked = True Then
                            query = query + "  and zona = @zona "
                        Else
                            query = query + "  and zona is null "
                        End If
                        SCMTemp.CommandText = query
                        SCMTemp.Parameters.AddWithValue("@id_dt_factura_div", Val(.gvPartidas.SelectedRow.Cells(0).Text))
                End Select
                SCMTemp.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                SCMTemp.Parameters.AddWithValue("@cuenta_c1", .txtCuentaC1.Text.Trim)
                SCMTemp.Parameters.AddWithValue("@cuenta_c2", .txtCuentaC2.Text.Trim)
                If .cbCC.Checked = True Then
                    SCMTemp.Parameters.AddWithValue("@centro_costo", .txtCC.Text.Trim)
                End If
                If .cbDiv.Checked = True Then
                    SCMTemp.Parameters.AddWithValue("@division", .txtDiv.Text.Trim)
                End If
                If .cbZona.Checked = True Then
                    SCMTemp.Parameters.AddWithValue("@zona", .txtZona.Text.Trim)
                End If
                ConexionBD.Open()
                conteo = SCMTemp.ExecuteScalar
                ConexionBD.Close()
                If conteo > 0 Then
                    validar = False
                Else
                    validar = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
                validar = False
            End Try
        End With
    End Function

#End Region

#Region "Botones Partidas"

    Protected Sub ibtnAlta_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAlta.Click
        With Me
            Try
                ._txtTipoMov.Text = "A"
                .lblTipoMov.Text = "Alta"

                .txtCuentaC1.Text = ""
                .txtCuentaC2.Text = ""

                .rblTipoAsig.SelectedValue = "P"
                .wnePorcent.Value = 1
                .lblPorcent.Text = ""
                .wceImporte.Text = ""

                .cbCC.Checked = False
                .txtCC.Text = ""
                .cbDiv.Checked = False

                .cbZona.Checked = False
                .txtZona.Text = ""

                .upDivD.Update()

                habilitarCampos(True)
                .pnlInicio.Enabled = True
                .pnlPartida.Visible = True
                .btnAceptar.Visible = False
                .btnRechaza.Visible = False
                .txtDiv.Enabled = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnBaja_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBaja.Click
        With Me
            Try
                ._txtTipoMov.Text = "B"
                .lblTipoMov.Text = "Baja"
                localizar(.gvPartidas.SelectedRow.Cells(0).Text)
                habilitarCampos(False)
                .pnlInicio.Enabled = True
                .pnlPartida.Visible = True
                .btnAceptar.Visible = False
                .btnRechaza.Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnModif_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnModif.Click
        With Me
            Try
                ._txtTipoMov.Text = "M"
                .lblTipoMov.Text = "Modificación"
                localizar(.gvPartidas.SelectedRow.Cells(0).Text)
                habilitarCampos(True)
                .pnlInicio.Enabled = True
                .pnlPartida.Visible = True
                .btnAceptar.Visible = False
                .btnRechaza.Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Partidas"

    Protected Sub gvPartidas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvPartidas.SelectedIndexChanged
        With Me
            .litError.Text = ""
            .ibtnBaja.Enabled = True
            .ibtnBaja.ImageUrl = "images\Trash.png"
            .ibtnModif.Enabled = True
            .ibtnModif.ImageUrl = "images\Edit.png"
        End With
    End Sub

    Protected Sub rblTipoAsig_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblTipoAsig.SelectedIndexChanged
        Me.wnePorcent.Text = ""
        Me.lblPorcent.Text = ""
        Me.wceImporte.Text = ""
        If Me.rblTipoAsig.SelectedValue = "P" Then
            'Por Porcentaje
            Me.wnePorcent.Enabled = True
            Me.wnePorcent.Value = 1
            Me.wceImporte.Enabled = False
        Else
            'Por Importe
            Me.wnePorcent.Enabled = False
            Me.wceImporte.Enabled = True
            Me.wceImporte.Value = 1
        End If
    End Sub

    Function ValidacionIU()

        With Me
            Dim ban As Integer = 0
            If .txtCuentaC1.Text.Trim = "" Or .txtCuentaC2.Text.Trim = "" Or (.rblTipoAsig.SelectedValue = "P" And .wnePorcent.Text = "") Or (.rblTipoAsig.SelectedValue = "I" And .wceImporte.Text = "") Or (.cbCC.Checked = True And .txtCC.Text.Trim = "") Or (.cbDiv.Checked = True And .txtDiv.Text.Trim = "") Or (.cbZona.Checked = True And .txtZona.Text.Trim = "") Then
                .litError.Text = "Información Insuficiente, favor de verificar"
                ban = 1
            Else
                If (.cbDiv.Checked = True And .cbZona.Checked = False) Or (.cbDiv.Checked = False And .cbZona.Checked = True) Or (.cbCC.Checked = True And .cbZona.Checked = True) Or (.cbCC.Checked = True And .cbDiv.Checked = True) Then
                    .litError.Text = "Combinación de dimensiones inválida, favor de verificar"
                    ban = 1

                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD

                    'Determinar Porcentaje
                    If .rblTipoAsig.SelectedValue = "P" Then
                        .lblPorcent.Text = .wnePorcent.Value
                    Else
                        'Calcular Porcentaje
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "select (@impPart / subtotal) * 100 as porcentaje " +
                                             "from dt_factura " +
                                             "where UUID = @uuid " +
                                             "  and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA')"
                        SCMValores.Parameters.AddWithValue("@uuid", .gvFactura.Rows(0).Cells(1).Text)
                        SCMValores.Parameters.Add("@impPart", SqlDbType.Float)
                        SCMValores.Parameters("@impPart").Value = .wceImporte.Value
                        ConexionBD.Open()
                        .lblPorcent.Text = SCMValores.ExecuteScalar
                        ConexionBD.Close()
                    End If

                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()

                    If ban > 0 Then
                        ValidacionIU = False
                    Else
                        ValidacionIU = True
                    End If
                End If
            End If

        End With
    End Function

    Protected Sub btnAceptarP_Click(sender As Object, e As EventArgs) Handles btnAceptarP.Click
        With Me
            Try
                .litError.Text = ""
                Dim ban As Integer = 0
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Select Case ._txtTipoMov.Text 'Tipo de Ajuste (Agregar, Eliminar o Modificar)
                    Case "A"
                        If ValidacionIU() Then
                            If validar() Then
                                SCMValores.CommandText = "SP_I_dt_factura_div"
                                SCMValores.CommandType = CommandType.StoredProcedure
                                SCMValores.Parameters.AddWithValue("@id_dt_factura", DBNull.Value)
                                'SCMValores.CommandText = "insert into dt_partida (id_ms_factura, cuenta_c1, cuenta_c2, porcent, centro_costo, division, zona) values(@id_ms_factura, @cuenta_c1, @cuenta_c2, @porcent, @centro_costo, @division, @zona)"
                            Else
                                .litError.Text = "Valor Inválido, ya existe esa combinación"
                                ban = 1
                            End If
                        Else
                            ban = 1
                        End If

                    Case "B"
                        SCMValores.CommandText = "SP_D_dt_factura_div"
                        SCMValores.CommandType = CommandType.StoredProcedure
                        SCMValores.Parameters.AddWithValue("@id_dt_factura_div", .gvPartidas.SelectedRow.Cells(0).Text)
                        'SCMValores.CommandText = "delete from dt_partida WHERE id_dt_partida = @id_dt_partida"
                        'SCMValores.Parameters.AddWithValue("@id_dt_partida", .gvPartidas.SelectedRow.Cells(0).Text)
                    Case Else

                        If ValidacionIU() Then
                            If validar() Then
                                SCMValores.CommandText = "SP_U_dt_factura_div"
                                SCMValores.CommandType = CommandType.StoredProcedure
                                SCMValores.Parameters.AddWithValue("@id_dt_factura_div", .gvPartidas.SelectedRow.Cells(0).Text)
                                'SCMValores.CommandText = "update dt_partida SET cuenta_c1 = @cuenta_c1, cuenta_c2 = @cuenta_c2, porcent = @porcent, centro_costo = @centro_costo, division = @division, zona = @zona WHERE id_dt_partida = @id_dt_partida"
                                'SCMValores.Parameters.AddWithValue("@id_dt_partida", .gvPartidas.SelectedRow.Cells(0).Text)
                            Else
                                .litError.Text = "Valor Inválido, ya existe esa combinación"
                                ban = 1
                            End If
                        Else
                            ban = 1
                        End If

                End Select

                If ban = 0 And ._txtTipoMov.Text = "A" Or ban = 0 And ._txtTipoMov.Text = "M" Then
                    SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                    SCMValores.Parameters.AddWithValue("@cuenta_c1", .txtCuentaC1.Text.Trim)
                    SCMValores.Parameters.AddWithValue("@cuenta_c2", .txtCuentaC2.Text.Trim)
                    SCMValores.Parameters.Add("@porcent", SqlDbType.Decimal)
                    SCMValores.Parameters("@porcent").Value = CDec(.lblPorcent.Text)
                    If .cbCC.Checked = True Then
                        SCMValores.Parameters.AddWithValue("@centro_costo", .txtCC.Text.Trim)
                    Else
                        SCMValores.Parameters.AddWithValue("@centro_costo", DBNull.Value)
                    End If
                    If .cbDiv.Checked = True Then
                        SCMValores.Parameters.AddWithValue("@division", .txtDiv.Text.Trim)
                    Else
                        SCMValores.Parameters.AddWithValue("@division", DBNull.Value)
                    End If
                    If .cbZona.Checked = True Then
                        SCMValores.Parameters.AddWithValue("@zona", .txtZona.Text.Trim)
                    Else
                        SCMValores.Parameters.AddWithValue("@zona", DBNull.Value)
                    End If
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()
                    limpiarPantalla()

                ElseIf ban = 0 Then

                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    limpiarPantalla()

                End If

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Aceptar / Rechazar"

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim valTotal As Integer
                Dim importeDiv As Double = 0

                'Si los montos coinciden se realiza el proceso 

                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                'SCMValores.CommandText = "select sum(porcent) as total " + _
                '                         "from dt_partida " + _
                '                         "where id_ms_factura = @id_ms_factura "
                SCMValores.CommandText = "select case when (cast((select sum(porcent) " +
                                         "                        from dt_factura_div  " +
                                         "                        where id_ms_factura = @id_ms_factura) as decimal) = 100) then 1 else 0 end  as valTotal "
                SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                ConexionBD.Open()
                valTotal = SCMValores.ExecuteScalar
                ConexionBD.Close()

                If valTotal <> 1 Then
                    .litError.Text = "Porcentaje inválido, favor de verificarlo"
                Else
                    If .txtDescripcion.Text.Trim = "" Then
                        .litError.Text = "Favor de Ingresar la Descripción"
                    Else
                        Dim fecha As DateTime = Date.Now
                        While Val(._txtBan.Text) = 0

                            'Actualizar datos de la Solicitud
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "update ms_factura set id_usr_asigna = @id_usr_asigna, fecha_asigna = @fecha_asigna, descrip = @descrip, status = 'R', comentario_asig_cuenta = @comentario, ultimos_comentarios = @comentario where id_ms_factura = @id_ms_factura "
                            SCMValores.Parameters.AddWithValue("@id_usr_asigna", Val(._txtIdUsuario.Text))
                            SCMValores.Parameters.AddWithValue("@fecha_asigna", fecha)
                            SCMValores.Parameters.AddWithValue("@descrip", .txtDescripcion.Text.Trim)
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
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "update dt_factura " +
                                                     "  set status = 'R' " +
                                                     "where uuid = @uuid " +
                                                     "  and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') "
                            SCMValores.Parameters.AddWithValue("@uuid", .gvFactura.Rows(0).Cells(1).Text)
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            ._txtBan.Text = 1

                            ''Actualizar Instancia
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                            SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                            SCMValores.Parameters.AddWithValue("@id_actividad", 16)
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            'Registrar en Histórico
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                                                     "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                            SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                            SCMValores.Parameters.AddWithValue("@id_actividad", 16)
                            SCMValores.Parameters.AddWithValue("@fecha", fecha)
                            SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            ''Insertar en dt_partidas

                            For Each r As GridViewRow In gvPartidas.Rows

                                SCMValores.CommandText = ""
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "SP_I_dt_partida"
                                SCMValores.CommandType = CommandType.StoredProcedure
                                SCMValores.Parameters.AddWithValue("@id_ms_factura", .lblFolio.Text)
                                SCMValores.Parameters.AddWithValue("@cuenta_c1", r.Cells(3).Text.Substring(0, 4))
                                SCMValores.Parameters.AddWithValue("@cuenta_c2", r.Cells(3).Text.Substring(5, 4))
                                SCMValores.Parameters.AddWithValue("@porcent", Convert.ToDouble(r.Cells(4).Text.Replace("%", "")))
                                If String.IsNullOrEmpty(r.Cells(6).Text.Trim().Replace("&nbsp;", String.Empty)) Then
                                    SCMValores.Parameters.AddWithValue("@centro_costo", DBNull.Value)
                                Else
                                    SCMValores.Parameters.AddWithValue("@centro_costo", r.Cells(6).Text)
                                End If

                                If String.IsNullOrEmpty(r.Cells(7).Text.Trim().Replace("&nbsp;", String.Empty)) Then
                                    SCMValores.Parameters.AddWithValue("@division", DBNull.Value)
                                Else
                                    SCMValores.Parameters.AddWithValue("@division", r.Cells(7).Text)
                                End If

                                If String.IsNullOrEmpty(r.Cells(8).Text.Trim().Replace("&nbsp;", String.Empty)) Then
                                    SCMValores.Parameters.AddWithValue("@zona", DBNull.Value)
                                Else
                                    SCMValores.Parameters.AddWithValue("@zona", r.Cells(8).Text)
                                End If

                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()

                            Next

                            'Envío de Correo
                            Dim Mensaje As New System.Net.Mail.MailMessage()
                            Dim destinatario As String = ""
                            'Obtener el Correo del Solicitante
                            SCMValores.CommandText = ""
                            SCMValores.CommandType = CommandType.Text
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "select cgEmpl.correo " +
                                                     "from ms_factura " +
                                                     "  left join cg_usuario on ms_factura.id_usr_solicita = cg_usuario.id_usuario " +
                                                     "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                     "where id_ms_factura = @id_ms_factura "
                            SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                            ConexionBD.Open()
                            destinatario = SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            Mensaje.[To].Add(destinatario)
                            Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                            'Mensaje.From = New System.Net.Mail.MailAddress("soporte.unne.procad@gmail.com", "ProcAd", System.Text.Encoding.UTF8)
                            Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                            Mensaje.Subject = "ProcAd - Solicitud de Factura No. " + .lblFolio.Text + " Procesada"
                            Dim texto As String
                            texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                                    "Se asignó la cuenta para la solicitud número <b>" + .lblFolio.Text + "</b>.</span>"
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

                            .pnlInicio.Enabled = False
                            .pnlPartida.Enabled = False
                            .ibtnAlta.Enabled = False
                            .ibtnModif.Enabled = False
                            .ibtnBaja.Enabled = False
                            .gvPartidas.Enabled = False
                            .pnlComentario.Enabled = False
                            .btnAceptar.Visible = False
                            .btnRechaza.Visible = False
                        End While
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

                If .txtComentario.Text.Trim = "" Then
                    .litError.Text = "Favor de ingresar los comentarios correspondientes"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    Dim fecha As DateTime = Date.Now
                    While Val(._txtBan.Text) = 0
                        'Actualizar datos de la Solicitud
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_factura set fecha_autoriza = NULL, fecha_autoriza2 = NULL, fecha_autoriza3 = NULL, status = 'P', comentario_asig_cuenta = @comentario, ultimos_comentarios = @comentario where id_ms_factura = @id_ms_factura "
                        SCMValores.Parameters.AddWithValue("@comentario", .txtComentario.Text.Trim)
                        SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Actualizar dt_factura
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update dt_factura " +
                                                 "  set status = 'As' " +
                                                 "where uuid = @uuid " +
                                                 "  and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') "
                        SCMValores.Parameters.AddWithValue("@uuid", .gvFactura.Rows(0).Cells(1).Text)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        ._txtBan.Text = 1

                        Dim idAutAnt As Integer
                        Dim idActividad As Integer
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "select count(*) " +
                                                 "from ms_instancia " +
                                                 "  left join ms_historico on ms_instancia.id_ms_instancia = ms_historico.id_ms_instancia " +
                                                 "where tipo = 'F' " +
                                                 "  and id_ms_sol = @id_ms_factura " +
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
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", idActividad)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Registrar en Histórico
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                                                 "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
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
                        SCMValores.CommandText = "select cgEmpl.correo " +
                                                 "from ms_factura " +
                                                 "  left join cg_usuario on ms_factura.id_usr_solicita = cg_usuario.id_usuario " +
                                                 "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                 "where id_ms_factura = @id_ms_factura "
                        SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        destinatario = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        Mensaje.[To].Add(destinatario)
                        Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                        'Mensaje.From = New System.Net.Mail.MailAddress("soporte.unne.procad@gmail.com", "ProcAd", System.Text.Encoding.UTF8)
                        Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                        Mensaje.Subject = "ProcAd - Solicitud de Factura No. " + .lblFolio.Text + " Rechazada"
                        Dim texto As String
                        texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
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

                        .pnlInicio.Enabled = False
                        .pnlPartida.Enabled = False
                        .ibtnAlta.Enabled = False
                        .ibtnModif.Enabled = False
                        .ibtnBaja.Enabled = False
                        .gvPartidas.Enabled = False
                        .pnlComentario.Enabled = False
                        .btnAceptar.Visible = False
                        .btnRechaza.Visible = False
                    End While
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class