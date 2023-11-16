Public Class _57
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 5
                    'Session("idMsInst") = 33461

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtIdMsInst.Text = Session("idMsInst")
                        ._txtBan.Text = 0

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim sdaSol As New SqlDataAdapter
                        Dim dsSol As New DataSet
                        Dim query As String
                        query = "select ms_contrato.id_ms_factura " + _
                                "     , ms_contrato.id_ms_contrato " + _
                                "     , ms_contrato.empleado " + _
                                "     , ms_contrato.empresa " + _
                                "     , ms_factura.fecha_solicita " + _
                                "     , ms_contrato.tipo_servicio " + _
                                "     , ms_factura.validador " + _
                                "     , ms_contrato.proveedor " + _
                                "     , ms_contrato.base " + _
                                "     , convert(varchar, ms_contrato.fecha_servicio_ini, 103) as fecha_ini " + _
                                "     , convert(varchar, ms_contrato.fecha_servicio_fin, 103) as fecha_ter " + _
                                "     , ms_contrato.periodicidad " + _
                                "     , format(ms_contrato.monto_periodo, 'C', 'es-MX') as monto_periodo " + _
                                "     , format(ms_contrato.monto_contrato, 'C', 'es-MX') as monto_contrato " + _
                                "     , ms_contrato.autorizador " + _
                                "     , ms_contrato.lugar_ejecucion " + _
                                "     , ms_contrato.descrip_servicio " + _
                                "from ms_factura " + _
                                "  left join ms_instancia on ms_factura.id_ms_factura = ms_instancia.id_ms_sol and ms_instancia.tipo = 'F' " + _
                                "  left join ms_contrato on ms_factura.id_ms_factura = ms_contrato.id_ms_factura " + _
                                "where id_ms_instancia = @idMsInst "
                        sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@idMsInst", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()
                        .lblFolio.Text = dsSol.Tables(0).Rows(0).Item("id_ms_factura").ToString()
                        ._txtMsContrato.Text = dsSol.Tables(0).Rows(0).Item("id_ms_contrato").ToString()
                        .lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("empleado").ToString()
                        .lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                        .lblFechaSol.Text = dsSol.Tables(0).Rows(0).Item("fecha_solicita").ToString()
                        .lblTipoServicio.Text = dsSol.Tables(0).Rows(0).Item("tipo_servicio").ToString()
                        .lblValidador.Text = dsSol.Tables(0).Rows(0).Item("validador").ToString()
                        .lblProveedor.Text = dsSol.Tables(0).Rows(0).Item("proveedor").ToString()
                        .lblBase.Text = dsSol.Tables(0).Rows(0).Item("base").ToString()
                        .lblFechaIni.Text = dsSol.Tables(0).Rows(0).Item("fecha_ini").ToString()
                        .lblFechaTer.Text = dsSol.Tables(0).Rows(0).Item("fecha_ter").ToString()
                        .lblPeriodicidad.Text = dsSol.Tables(0).Rows(0).Item("periodicidad").ToString()
                        .lblMontoXPeriodo.Text = dsSol.Tables(0).Rows(0).Item("monto_periodo").ToString()
                        .lblMontoContrato.Text = dsSol.Tables(0).Rows(0).Item("monto_contrato").ToString()
                        .lblAutorizador.Text = dsSol.Tables(0).Rows(0).Item("autorizador").ToString()
                        .lblLugar.Text = dsSol.Tables(0).Rows(0).Item("lugar_ejecucion").ToString()
                        .txtEspecificaciones.Text = dsSol.Tables(0).Rows(0).Item("descrip_servicio").ToString()
                        sdaSol.Dispose()
                        dsSol.Dispose()

                        'Cuentas Contables
                        limpiarPantalla()

                        'Panel
                        .pnlInicio.Enabled = True
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

    Public Sub llenarCuentas()
        With Me
            Try
                'Llenar Grid
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCuentas As New SqlDataAdapter
                Dim dsCuentas As New DataSet
                .gvCuentas.DataSource = dsCuentas
                .gvCuentas.Columns(0).Visible = True
                sdaCuentas.SelectCommand = New SqlCommand("select id_dt_cuenta " + _
                                                           "     , cuenta_c1 +  '-' + cuenta_c2 as cuenta_c " + _
                                                           "     , porcent " + _
                                                           "     , dt_cuenta.centro_costo " + _
                                                           "     , division " + _
                                                           "     , zona " + _
                                                           "from dt_cuenta " + _
                                                           "  left join ms_contrato on dt_cuenta.id_ms_contrato = ms_contrato.id_ms_contrato " + _
                                                           "where dt_cuenta.id_ms_contrato = @idMsContrato ", ConexionBD)
                sdaCuentas.SelectCommand.Parameters.AddWithValue("@idMsContrato", Val(._txtMsContrato.Text))
                ConexionBD.Open()
                sdaCuentas.Fill(dsCuentas)
                .gvCuentas.DataBind()
                ConexionBD.Close()
                sdaCuentas.Dispose()
                dsCuentas.Dispose()
                .gvCuentas.Columns(0).Visible = False
                .gvCuentas.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub limpiarPantalla()
        With Me
            llenarCuentas()
            .ibtnAlta.Enabled = True
            .ibtnBaja.Enabled = False
            .ibtnBaja.ImageUrl = "images\Trash_i2.png"
            .ibtnModif.Enabled = False
            .ibtnModif.ImageUrl = "images\Edit_i2.png"
            .pnlInicio.Enabled = True
            .pnlCuenta.Visible = False
            .btnAceptar.Visible = True
            'Calcular Porcentaje Asignado
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
            SCMValores.Connection = ConexionBD
            SCMValores.CommandText = ""
            SCMValores.Parameters.Clear()
            SCMValores.CommandText = "select format(isnull(sum(porcent), 0), '##0.00#################', 'en-US') as porcentAsig " + _
                                     "from dt_cuenta " + _
                                     "where id_ms_contrato = @id_ms_contrato "
            SCMValores.Parameters.AddWithValue("@id_ms_contrato", ._txtMsContrato.Text)
            ConexionBD.Open()
            .lblPorcentAsig.Text = SCMValores.ExecuteScalar
            ConexionBD.Close()
        End With
    End Sub

    Public Sub habilitarCampos(ByVal valor As Boolean)
        With Me
            .txtCuentaC1.Enabled = valor
            .txtCuentaC2.Enabled = valor
            .wnePorcent.Enabled = valor
            .cbCC.Enabled = valor
            .txtCC.Enabled = valor
            .cbDiv.Enabled = valor
            .txtDiv.Enabled = valor
            .cbZona.Enabled = valor
            .txtZona.Enabled = valor
        End With
    End Sub


    Public Sub localizar(ByVal idRegistro)
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCuentas As New SqlDataAdapter
                Dim dsCuentas As New DataSet
                .gvCuentas.DataSource = dsCuentas
                sdaCuentas.SelectCommand = New SqlCommand("select id_dt_cuenta " + _
                                                           "     , cuenta_c1, cuenta_c2 " + _
                                                           "     , porcent " + _
                                                           "     , isnull(centro_costo, 'XX') as centro_costo " + _
                                                           "     , isnull(division, 'XX') as division " + _
                                                           "     , isnull(zona, 'XX') as zona " + _
                                                           "from dt_cuenta " + _
                                                           "where id_dt_cuenta = @idDtCuenta ", ConexionBD)
                sdaCuentas.SelectCommand.Parameters.AddWithValue("@idDtCuenta", idRegistro)
                ConexionBD.Open()
                sdaCuentas.Fill(dsCuentas)
                ConexionBD.Close()
                'Llenar Campos
                .txtCuentaC1.Text = dsCuentas.Tables(0).Rows(0).Item("cuenta_c1").ToString()
                .txtCuentaC2.Text = dsCuentas.Tables(0).Rows(0).Item("cuenta_c2").ToString()
                .wnePorcent.Value = Val(dsCuentas.Tables(0).Rows(0).Item("porcent").ToString())
                If dsCuentas.Tables(0).Rows(0).Item("centro_costo").ToString() = "XX" Then
                    .cbCC.Checked = False
                    .txtCC.Text = ""
                    .txtCC.Enabled = False
                Else
                    .cbCC.Checked = True
                    .txtCC.Text = dsCuentas.Tables(0).Rows(0).Item("centro_costo").ToString()
                    .txtCC.Enabled = True
                End If
                If dsCuentas.Tables(0).Rows(0).Item("division").ToString() = "XX" Then
                    .cbDiv.Checked = False
                    .txtDiv.Text = ""
                    .txtDiv.Enabled = False
                Else
                    .cbDiv.Checked = True
                    .txtDiv.Text = dsCuentas.Tables(0).Rows(0).Item("division").ToString()
                    .txtDiv.Enabled = True
                End If
                If dsCuentas.Tables(0).Rows(0).Item("zona").ToString() = "XX" Then
                    .cbZona.Checked = False
                    .txtZona.Text = ""
                    .txtZona.Enabled = False
                Else
                    .cbZona.Checked = True
                    .txtZona.Text = dsCuentas.Tables(0).Rows(0).Item("zona").ToString()
                    .txtZona.Enabled = True
                End If
                sdaCuentas.Dispose()
                dsCuentas.Dispose()

                .pnlInicio.Enabled = True
                .pnlCuenta.Visible = True
                .btnAceptar.Visible = False
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
                        query = "select count(*) " + _
                                "from dt_cuenta " + _
                                "where id_ms_contrato = @id_ms_contrato " + _
                                "  and cuenta_c1 = @cuenta_c1 " + _
                                "  and cuenta_c2 = @cuenta_c2 "
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
                        query = "select count(*) " + _
                                "from dt_cuenta " + _
                                "where id_ms_contrato = @id_ms_contrato " + _
                                "  and id_dt_cuenta <> @id_dt_cuenta " + _
                                "  and cuenta_c1 = @cuenta_c1 " + _
                                "  and cuenta_c2 = @cuenta_c2 "
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
                        SCMTemp.Parameters.AddWithValue("@id_dt_cuenta", Val(.gvCuentas.SelectedRow.Cells(0).Text))
                End Select
                SCMTemp.Parameters.AddWithValue("@id_ms_contrato", Val(._txtMsContrato.Text))
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

#Region "Botones Cuentas"

    Protected Sub ibtnAlta_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAlta.Click
        With Me
            Try
                ._txtTipoMov.Text = "A"
                .lblTipoMov.Text = "Alta"

                .txtCuentaC1.Text = ""
                .txtCuentaC2.Text = ""

                .wnePorcent.Value = 1
                .lblPorcent.Text = ""

                .cbCC.Checked = False
                .txtCC.Text = ""
                .cbDiv.Checked = False
                .txtDiv.Text = ""
                .cbZona.Checked = False
                .txtZona.Text = ""

                habilitarCampos(True)
                .pnlInicio.Enabled = True
                .pnlCuenta.Visible = True
                .btnAceptar.Visible = False
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
                localizar(.gvCuentas.SelectedRow.Cells(0).Text)
                habilitarCampos(False)
                .pnlInicio.Enabled = True
                .pnlCuenta.Visible = True
                .btnAceptar.Visible = False
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
                localizar(.gvCuentas.SelectedRow.Cells(0).Text)
                habilitarCampos(True)
                .pnlInicio.Enabled = True
                .pnlCuenta.Visible = True
                .btnAceptar.Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Cuentas"

    Protected Sub gvCuentas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvCuentas.SelectedIndexChanged
        With Me
            .litError.Text = ""
            .ibtnBaja.Enabled = True
            .ibtnBaja.ImageUrl = "images\Trash.png"
            .ibtnModif.Enabled = True
            .ibtnModif.ImageUrl = "images\Edit.png"
        End With
    End Sub

    Protected Sub btnAceptarP_Click(sender As Object, e As EventArgs) Handles btnAceptarP.Click
        With Me
            Try
                .litError.Text = ""
                Dim ban As Integer = 0
                If .txtCuentaC1.Text.Trim = "" Or .txtCuentaC2.Text.Trim = "" Or .wnePorcent.Text = "" Or (.cbCC.Checked = True And .txtCC.Text.Trim = "") Or (.cbDiv.Checked = True And .txtDiv.Text.Trim = "") Or (.cbZona.Checked = True And .txtZona.Text.Trim = "") Then
                    .litError.Text = "Información Insuficiente, favor de verificar"
                Else
                    If (.cbDiv.Checked = True And .cbZona.Checked = False) Or (.cbDiv.Checked = False And .cbZona.Checked = True) Or (.cbCC.Checked = True And .cbZona.Checked = True) Or (.cbCC.Checked = True And .cbDiv.Checked = True) Then
                        .litError.Text = "Combinación de dimensiones inválida, favor de verificar"
                    Else
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD

                        .lblPorcent.Text = .wnePorcent.Value

                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        Select Case ._txtTipoMov.Text 'Tipo de Ajuste (Agregar, Eliminar o Modificar)
                            Case "A"
                                If validar() Then
                                    SCMValores.CommandText = "insert into dt_cuenta (id_ms_contrato, cuenta_c1, cuenta_c2, porcent, centro_costo, division, zona) values(@id_ms_contrato, @cuenta_c1, @cuenta_c2, @porcent, @centro_costo, @division, @zona)"
                                Else
                                    .litError.Text = "Valor Inválido, ya existe esa combinación"
                                    ban = 1
                                End If
                            Case "B"
                                SCMValores.CommandText = "delete from dt_cuenta WHERE id_dt_cuenta = @id_dt_cuenta"
                                SCMValores.Parameters.AddWithValue("@id_dt_cuenta", .gvCuentas.SelectedRow.Cells(0).Text)
                            Case Else
                                If validar() Then
                                    SCMValores.CommandText = "update dt_cuenta SET cuenta_c1 = @cuenta_c1, cuenta_c2 = @cuenta_c2, porcent = @porcent, centro_costo = @centro_costo, division = @division, zona = @zona WHERE id_dt_cuenta = @id_dt_cuenta"
                                    SCMValores.Parameters.AddWithValue("@id_dt_cuenta", .gvCuentas.SelectedRow.Cells(0).Text)
                                Else
                                    .litError.Text = "Valor Inválido, ya existe esa combinación"
                                    ban = 1
                                End If
                        End Select
                        If ban = 0 Then
                            SCMValores.Parameters.AddWithValue("@id_ms_contrato", Val(._txtMsContrato.Text))
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
                        End If
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Registrar"

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim valTotal As Integer
                SCMValores.Parameters.Clear()
                'SCMValores.CommandText = "select sum(porcent) as total " + _
                '                         "from dt_cuenta " + _
                '                         "where id_ms_contrato = @id_ms_contrato "
                SCMValores.CommandText = "select case when (cast((select sum(porcent) " + _
                                         "                        from dt_cuenta " + _
                                         "                        where id_ms_contrato = @id_ms_contrato) as decimal) = 100) then 1 else 0 end  as valTotal "
                SCMValores.Parameters.AddWithValue("@id_ms_contrato", Val(._txtMsContrato.Text))
                ConexionBD.Open()
                valTotal = SCMValores.ExecuteScalar
                ConexionBD.Close()

                If valTotal <> 1 Then
                    .litError.Text = "Porcentaje inválido, favor de verificarlo"
                Else
                    Dim fecha As DateTime = Date.Now
                    While Val(._txtBan.Text) = 0
                        'Actualizar datos del Contrato
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_contrato set id_usr_asigna = @id_usr_asigna, fecha_asigna = @fecha_asigna, comentario_asigna = @comentario_asigna where id_ms_contrato = @id_ms_contrato "
                        SCMValores.Parameters.AddWithValue("@id_usr_asigna", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@fecha_asigna", fecha)
                        If .txtComentario.Text.Trim = "" Then
                            SCMValores.Parameters.AddWithValue("@comentario_asigna", DBNull.Value)
                        Else
                            SCMValores.Parameters.AddWithValue("@comentario_asigna", .txtComentario.Text.Trim)
                        End If
                        SCMValores.Parameters.AddWithValue("@id_ms_contrato", Val(._txtMsContrato.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Actualizar datos de la Solicitud
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_factura set ultimos_comentarios = @comentario where id_ms_factura = @id_ms_factura "
                        If .txtComentario.Text.Trim = "" Then
                            SCMValores.Parameters.AddWithValue("@comentario", DBNull.Value)
                        Else
                            SCMValores.Parameters.AddWithValue("@comentario", .txtComentario.Text.Trim)
                        End If
                        SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        ._txtBan.Text = 1

                        'Actualizar Instancia
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 58)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Registrar en Histórico
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                                 "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 58)
                        SCMValores.Parameters.AddWithValue("@fecha", fecha)
                        SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        .pnlInicio.Enabled = False
                        .pnlComentario.Enabled = False
                        .btnAceptar.Visible = False
                    End While
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class