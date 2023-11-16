Public Class Encuesta
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    .lblID.Text = Request.QueryString("id")
                    .lblFolio.Text = Request.QueryString("idMs")

                    .pnlSolRec.Visible = False
                    .pnlR1a.Visible = False
                    .pnlR1.Visible = False
                    .pnlR2.Visible = False
                    .pnlR3.Visible = False
                    .pnlR4.Visible = False
                    .pnlR5.Visible = False

                    'Verificar que la encuesta exista y que no haya sido complementada previamente
                    'Creación de Variables para la conexión y consulta de infromación a la base de datos
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD

                    Dim conteo As Integer = 0
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "select count(*) from ms_recursos where id_ms_recursos = @id_ms_recursos"
                    SCMValores.Parameters.AddWithValue("@id_ms_recursos", Val(.lblFolio.Text))
                    ConexionBD.Open()
                    conteo = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    .litError.Text = ""
                    If conteo > 0 Then
                        SCMValores.CommandText = "select count(*) from ms_encuesta where id_ms_recursos = @id_ms_recursos and id = @id"
                        SCMValores.Parameters.AddWithValue("@id", .lblID.Text)
                        ConexionBD.Open()
                        conteo = SCMValores.ExecuteScalar()
                        ConexionBD.Close()
                        If conteo > 0 Then
                            SCMValores.CommandText = "select count(*) from ms_encuesta where id_ms_recursos = @id_ms_recursos and id = @id and status = 'P'"
                            ConexionBD.Open()
                            conteo = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If conteo > 0 Then
                                'Presentar Encuesta
                                .pnlSolRec.Visible = True
                                .pnlR1.Visible = True

                                Dim sdaSol As New SqlDataAdapter
                                Dim dsSol As New DataSet
                                Dim query As String
                                query = "select ms_recursos.empleado " + _
                                        "     , format(cast(cg_empleado.no_empleado as int), '0000000', 'es-MX') as no_empleado " + _
                                        "     , isnull(ms_anticipo.no_proveedor, '') as no_proveedor " + _
                                        "     , cg_empleado.correo " + _
                                        "     , case cg_empleado.puesto_tabulador " + _
                                        "         when 'AdmJef' then 'Administrativos y Jefaturas' " + _
                                        "         when 'GerDir' then 'Gerentes y Directivos' " + _
                                        "         when 'Cho' then 'Choferes' " + _
                                        "         when 'Mec' then 'Mecánicos' " + _
                                        "         when 'DirGral' then 'Director General' " + _
                                        "       end as puesto_tab " + _
                                        "     , ms_recursos.autorizador " + _
                                        "     , ms_recursos.destino " + _
                                        "     , isnull(ms_recursos.lugar_orig, '') as lugar_orig " + _
                                        "     , isnull(ms_recursos.lugar_dest, '') as lugar_dest " + _
                                        "     , mov_locales " + _
                                        "     , ms_recursos.empresa " + _
                                        "     , ms_recursos.actividad as just " + _
                                        "     , convert(varchar, ms_recursos.periodo_ini, 103) as periodo_ini " + _
                                        "     , convert(varchar, ms_recursos.periodo_fin, 103) as periodo_fin " + _
                                        "     , ms_recursos.tipo_transporte " + _
                                        "     , ms_recursos.incluye_anticipo " + _
                                        "     , ms_anticipo.id_ms_anticipo " + _
                                        "     , isnull(dias_hospedaje, 0) as dias_hospedaje " + _
                                        "     , monto_hospedaje " + _
                                        "     , isnull(dias_alimentos, 0) as dias_alimentos " + _
                                        "     , monto_alimentos " + _
                                        "     , isnull(dias_casetas, 0) as dias_casetas " + _
                                        "     , monto_casetas " + _
                                        "     , isnull(dias_otros, 0) as dias_otros " + _
                                        "     , monto_otros " + _
                                        "     , isnull(otros_especifico, 'XX') as otros_especifico " + _
                                        "     , case tipo_pago when 'E' then 'Efectivo' when 'T' then 'Transferencia' else '-' end as tipo_pago " + _
                                        "     , monto_hospedaje + monto_alimentos + monto_casetas + monto_otros as importe " + _
                                        "     , ms_recursos.incluye_reserva " + _
                                        "     , ms_recursos.lugares_dispo " + _
                                        "     , ms_recursos.lugares_reque " + _
                                        "     , ms_recursos.id_ms_reserva " + _
                                        "     , ms_reserva.fecha_ini " + _
                                        "     , ms_reserva.fecha_fin " + _
                                        "     , ms_reserva.no_eco as no_eco_reserva " + _
                                        "     , ms_reserva.modelo as modelo_reserva " + _
                                        "     , ms_recursos.incluye_hist_util " + _
                                        "     , dt_hist_util.id_dt_hist_util " + _
                                        "     , dt_hist_util.periodo_ini as periodo_ini_util " + _
                                        "     , dt_hist_util.periodo_fin as periodo_fin_util " + _
                                        "     , dt_hist_util.no_eco as no_eco_util " + _
                                        "     , dt_hist_util.modelo as modelo_util " + _
                                        "     , isnull(format(dt_hist_util.km_actual, 'N0', 'es-MX'), '') as km_actual " + _
                                        "     , ms_recursos.incluye_comb " + _
                                        "     , ms_recursos.id_ms_comb " + _
                                        "     , ms_comb.no_eco as no_eco_comb " + _
                                        "     , ms_comb.no_tarjeta_edenred " + _
                                        "     , isnull(format(ms_comb.litros_comb, 'N0', 'es-MX'), '') as litros_comb " + _
                                        "     , isnull(format(ms_comb.importe_comb, 'C2', 'es-MX'), '') as importe_comb " + _
                                        "     , ms_recursos.incluye_avion " + _
                                        "     , ms_avion.id_ms_avion " + _
                                        "     , convert(varchar, ms_avion.fecha_nacimiento, 103) as fecha_nacimiento " + _
                                        "     , ms_avion.fecha_salida as fecha_salida " + _
                                        "     , ms_avion.fecha_regreso as fecha_regreso " + _
                                        "     , isnull(ms_avion.justificacion, '') as justificacion " + _
                                        "from ms_recursos " + _
                                        "  left join ms_anticipo on ms_recursos.id_ms_anticipo = ms_anticipo.id_ms_anticipo " + _
                                        "  left join ms_reserva on ms_recursos.id_ms_reserva = ms_reserva.id_ms_reserva " + _
                                        "  left join dt_hist_util on ms_recursos.id_dt_hist_util = dt_hist_util.id_dt_hist_util " + _
                                        "  left join ms_comb on ms_recursos.id_ms_comb = ms_comb.id_ms_comb " + _
                                        "  left join ms_avion on ms_recursos.id_ms_avion = ms_avion.id_ms_avion " + _
                                        "  left join cg_usuario on ms_recursos.id_usr_solicita = cg_usuario.id_usuario " + _
                                        "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                                        "where ms_recursos.id_ms_recursos = @id_ms_recursos "
                                sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                                sdaSol.SelectCommand.Parameters.AddWithValue("@id_ms_recursos", Val(.lblFolio.Text))
                                ConexionBD.Open()
                                sdaSol.Fill(dsSol)
                                ConexionBD.Close()
                                .lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("empleado").ToString()
                                .lblNoProveedor.Text = dsSol.Tables(0).Rows(0).Item("no_proveedor").ToString()
                                If dsSol.Tables(0).Rows(0).Item("no_proveedor").ToString() = "" Then
                                    .lbl_NoProveedor.Visible = False
                                Else
                                    .lbl_NoProveedor.Visible = True
                                End If
                                .lblAutorizador.Text = dsSol.Tables(0).Rows(0).Item("autorizador").ToString()
                                .lblOrig.Text = dsSol.Tables(0).Rows(0).Item("lugar_orig").ToString()
                                .lblDest.Text = dsSol.Tables(0).Rows(0).Item("lugar_dest").ToString()
                                .lblDestino.Text = dsSol.Tables(0).Rows(0).Item("destino").ToString()
                                If dsSol.Tables(0).Rows(0).Item("mov_locales").ToString = "S" Then
                                    .cblMovLocales.Items(0).Selected = True
                                Else
                                    .cblMovLocales.Items(0).Selected = False
                                End If
                                .lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                                .txtJust.Text = dsSol.Tables(0).Rows(0).Item("just").ToString()
                                .lblPeriodoIni.Text = dsSol.Tables(0).Rows(0).Item("periodo_ini").ToString()
                                .lblPeriodoFin.Text = dsSol.Tables(0).Rows(0).Item("periodo_fin").ToString()
                                .lblTipoTansp.Text = dsSol.Tables(0).Rows(0).Item("tipo_transporte").ToString()
                                'Anticipo
                                If dsSol.Tables(0).Rows(0).Item("incluye_anticipo").ToString() = "S" Then
                                    .cblRecursos.Items(0).Selected = True
                                Else
                                    .cblRecursos.Items(0).Selected = False
                                End If
                                'Reserva / Hist. Utilitarios
                                If dsSol.Tables(0).Rows(0).Item("incluye_reserva").ToString() = "S" Or dsSol.Tables(0).Rows(0).Item("incluye_hist_util").ToString() = "S" Then
                                    .cblRecursos.Items(1).Selected = True
                                Else
                                    .cblRecursos.Items(1).Selected = False
                                End If
                                'Combustible
                                If dsSol.Tables(0).Rows(0).Item("incluye_comb").ToString() = "S" Then
                                    .cblRecursos.Items(2).Selected = True
                                Else
                                    .cblRecursos.Items(2).Selected = False
                                End If
                                'Avión
                                If dsSol.Tables(0).Rows(0).Item("incluye_avion").ToString() = "S" Then
                                    .cblRecursos.Items(3).Selected = True
                                Else
                                    .cblRecursos.Items(3).Selected = False
                                End If

                                sdaSol.Dispose()
                                dsSol.Dispose()
                            Else
                                SCMValores.CommandText = "select count(*) from ms_encuesta where id_ms_recursos = @id_ms_recursos and id = @id and status = 'Z'"
                                ConexionBD.Open()
                                conteo = SCMValores.ExecuteScalar()
                                ConexionBD.Close()
                                If conteo > 0 Then
                                    .litError.Text = "Se excedió el tiempo máximo para complementar la encuesta, te recordamos que solo se cuentan con diez días naturales posteriores a la fecha de la llegada del correo"
                                Else
                                    .litError.Text = "Ya fue complementada esa encuesta, gracias"
                                End If
                            End If
                        Else
                            .litError.Text = "No se ha generado una encuesta para esa solicitud, favor de validarlo con el Área de Tecnologías de Información"
                        End If
                    Else
                        .litError.Text = "No existe la Solicitud al que se hace referencia, favor de validarlo con el Área de Tecnologías de Información"
                    End If
                Catch ex As Exception
                    .litError.Text = ex.ToString
                End Try
            End With
        End If
    End Sub

#End Region

#Region "P1"

    Protected Sub rblR1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblR1.SelectedIndexChanged
        Me.pnlR1a.Visible = True
        If Me.rblR1.SelectedValue = "E" Or Me.rblR1.SelectedValue = "MB" Then
            Me.lbl_P1a.Text = "Específicamente, ¿qué hizo que el servicio fuera " + Me.rblR1.SelectedItem.Text + "?"
        Else
            Me.lbl_P1a.Text = "Específicamente, ¿qué se podría cambiar para mejorar el servicio?"
        End If
    End Sub

    Protected Sub btnSig1_Click(sender As Object, e As EventArgs) Handles btnSig1.Click
        With Me
            Try
                .litError.Text = ""
                If .txtR1a.Text.Trim = "" Then
                    .litError.Text = "Favor de complementar el campo de texto"
                Else
                    'Almacenar respuestas
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD

                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_encuesta set resp_1 = @resp_1, resp_1a = @resp_1a where id_ms_recursos = @id_ms_recursos and id = @id and status = 'P'"
                    SCMValores.Parameters.AddWithValue("@resp_1", .rblR1.SelectedValue)
                    SCMValores.Parameters.AddWithValue("@resp_1a", .txtR1a.Text.Trim)
                    SCMValores.Parameters.AddWithValue("@id_ms_recursos", Val(.lblFolio.Text))
                    SCMValores.Parameters.AddWithValue("@id", .lblID.Text)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    .pnlR1.Visible = False
                    .pnlR2.Visible = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "P2"

    Protected Sub btnSig2_Click(sender As Object, e As EventArgs) Handles btnSig2.Click
        With Me
            Try
                .litError.Text = ""
                If .rblR2a.SelectedIndex = -1 Or .rblR2b.SelectedIndex = -1 Or .rblR2c.SelectedIndex = -1 Then
                    .litError.Text = "Favor de seleccionar la respuesta para cada afirmación"
                Else
                    'Almacenar respuestas
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD

                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_encuesta set resp_2a = @resp_2a, resp_2b = @resp_2b, resp_2c = @resp_2c where id_ms_recursos = @id_ms_recursos and id = @id and status = 'P'"
                    SCMValores.Parameters.AddWithValue("@resp_2a", .rblR2a.SelectedValue)
                    SCMValores.Parameters.AddWithValue("@resp_2b", .rblR2b.SelectedValue)
                    SCMValores.Parameters.AddWithValue("@resp_2c", .rblR2c.SelectedValue)
                    SCMValores.Parameters.AddWithValue("@id_ms_recursos", Val(.lblFolio.Text))
                    SCMValores.Parameters.AddWithValue("@id", .lblID.Text)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    .pnlR2.Visible = False
                    .pnlR3.Visible = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "P3"

    Protected Sub btnSig3_Click(sender As Object, e As EventArgs) Handles btnSig3.Click
        With Me
            Try
                .litError.Text = ""
                If .rblR3.SelectedIndex = -1 Then
                    .litError.Text = "Favor de seleccionar la respuesta"
                Else
                    'Almacenar respuesta
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD

                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_encuesta set resp_3 = @resp_3 where id_ms_recursos = @id_ms_recursos and id = @id and status = 'P'"
                    SCMValores.Parameters.AddWithValue("@resp_3", .rblR3.SelectedValue)
                    SCMValores.Parameters.AddWithValue("@id_ms_recursos", Val(.lblFolio.Text))
                    SCMValores.Parameters.AddWithValue("@id", .lblID.Text)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    .pnlR3.Visible = False
                    .pnlR4.Visible = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "P4"

    Protected Sub btnSig4_Click(sender As Object, e As EventArgs) Handles btnSig4.Click
        With Me
            Try
                .litError.Text = ""
                If .rblR3.SelectedIndex = -1 Then
                    .litError.Text = "Favor de seleccionar la respuesta"
                Else
                    'Almacenar respuesta
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD

                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_encuesta set resp_4 = @resp_4, status = 'C', fecha_reg = getdate() where id_ms_recursos = @id_ms_recursos and id = @id and status = 'P'"
                    SCMValores.Parameters.AddWithValue("@resp_4", .txtR4.Text.Trim)
                    SCMValores.Parameters.AddWithValue("@id_ms_recursos", Val(.lblFolio.Text))
                    SCMValores.Parameters.AddWithValue("@id", .lblID.Text)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    .pnlR4.Visible = False
                    .pnlR5.Visible = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class