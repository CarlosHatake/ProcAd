Public Class _149
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                _txtIdUsuario.Text = Session("id_usuario")
                _txtIdMsInst.Text = Session("idMsInst")
                _txtBan.Text = 0

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaSol As New SqlDataAdapter
                Dim dsSol As New DataSet
                sdaSol.SelectCommand = New SqlCommand("select " +
                                                              " empleado_solicita, MCA.empresa, id_ms_comprobacion_anticipo as folio, " +
                                                              "  MCA.autorizador, MCA.tipo_escenario, " +
                                                              "  MCA.id_usr_autorizador2, MCA.id_usr_autorizador3, " +
                                                              "  isnull(cgEmpl2.nombre + ' ' + cgEmpl2.ap_paterno + ' ' + cgEmpl2.ap_materno, 'XX') as autorizador2, " +
                                                              "  isnull(cgEmpl3.nombre + ' ' + cgEmpl3.ap_paterno + ' ' + cgEmpl3.ap_materno, 'XX') as autorizador3, " +
                                                              "  proveedor, division, centro_costos " +
                                                              "  from ms_comprobacion_anticipo MCA " +
                                                              "  inner join ms_instancia MSA on MCA.id_ms_comprobacion_anticipo = MSA.id_ms_sol and Tipo = 'CAP' " +
                                                              "  left join cg_usuario cgAut2 on MCA.id_usr_autorizador2 = cgAut2.id_usuario " +
                                                              "  left join bd_Empleado.dbo.cg_empleado cgEmpl2 on cgAut2.id_empleado = cgEmpl2.id_empleado " +
                                                              "  left join cg_usuario cgAut3 on MCA.id_usr_autorizador3 = cgAut3.id_usuario " +
                                                              "  left join bd_Empleado.dbo.cg_empleado cgEmpl3 on cgAut3.id_empleado = cgEmpl3.id_empleado " +
                                                              "  where MSA.id_ms_instancia = @id_ms_instancia ", ConexionBD)
                sdaSol.SelectCommand.Parameters.AddWithValue("@id_ms_instancia", Val(_txtIdMsInst.Text))
                ConexionBD.Open()
                sdaSol.Fill(dsSol)
                ConexionBD.Close()
                lblFolio.Text = dsSol.Tables(0).Rows(0).Item("folio").ToString()
                lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("empleado_solicita").ToString()
                lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                lblCC.Text = dsSol.Tables(0).Rows(0).Item("centro_costos").ToString()
                lblDivision.Text = dsSol.Tables(0).Rows(0).Item("division").ToString()
                lblProveedor.Text = dsSol.Tables(0).Rows(0).Item("proveedor").ToString()
                lblAutorizador.Text = dsSol.Tables(0).Rows(0).Item("autorizador").ToString()
                _txtTipoEscenario.Text = dsSol.Tables(0).Rows(0).Item("tipo_escenario").ToString()
                If dsSol.Tables(0).Rows(0).Item("autorizador2").ToString() = "XX" Then
                    lbl_Autorizador2.Visible = False
                    lblAutorizador2.Visible = False
                Else
                    lbl_Autorizador2.Visible = True
                    lblAutorizador2.Visible = True
                    lblAutorizador2.Text = dsSol.Tables(0).Rows(0).Item("autorizador2").ToString()
                End If
                If dsSol.Tables(0).Rows(0).Item("autorizador3").ToString() = "XX" Then
                    lbl_Autorizador3.Visible = False
                    lblAutorizador3.Visible = False
                Else
                    lbl_Autorizador3.Visible = True
                    lblAutorizador3.Visible = True
                    lblAutorizador3.Text = dsSol.Tables(0).Rows(0).Item("autorizador3").ToString()
                End If
                sdaSol.Dispose()
                dsSol.Dispose()



                'Facturas
                Dim sdaFacturas As New SqlDataAdapter
                Dim dsFacturas As New DataSet
                gvFacturas.DataSource = dsFacturas
                'Habilitar columnas para actualización
                gvFacturas.Columns(0).Visible = True
                If _txtTipoEscenario.Text = "5" Then
                    gvFacturas.Columns(1).Visible = True
                    ibtnAlta.Enabled = False
                    ibtnAlta.ImageUrl = "images\Add_i2.png"
                Else
                    gvFacturas.Columns(1).Visible = False
                End If
                sdaFacturas.SelectCommand = New SqlCommand("select id_dt_comprobacion_anticipo " +
                                                           "     , fecha_emision " +
                                                           "     , uuid " +
                                                           "     , lugar_exp " +
                                                           "     , forma_pago " +
                                                           "     , moneda " +
                                                           "     , subtotal " +
                                                           "     , importe " +
                                                           "from dt_comprobacion_anticipo " +
                                                           "where id_ms_comp_anticipo = @id_ms_comp_anticipo", ConexionBD)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@id_ms_comp_anticipo", Val(lblFolio.Text))
                ConexionBD.Open()
                sdaFacturas.Fill(dsFacturas)
                gvFacturas.DataBind()
                ConexionBD.Close()
                sdaFacturas.Dispose()
                dsFacturas.Dispose()
                gvFacturas.SelectedIndex = -1
                'Inhabilitar columnas para vista
                gvFacturas.Columns(0).Visible = False

                'Evidencias
                Dim sdaEvidencias As New SqlDataAdapter
                Dim dsEvidencias As New DataSet
                gvEvidencias.DataSource = dsEvidencias
                'Evidencias
                sdaEvidencias.SelectCommand = New SqlCommand("select nombre as archivo " +
                                                           "     , 'http://148.223.153.43/ProcAd - Adjuntos IngFact/' + cast(id_dt_archivo_adj_comp_anticipo as varchar(20)) + '-' + nombre as path " +
                                                           "from dt_archivo_adj_comp_anticipo " +
                                                           "where id_ms_comp_anticipo_proveedor = @id_ms_comp_anticipo_proveedor", ConexionBD)
                sdaEvidencias.SelectCommand.Parameters.AddWithValue("@id_ms_comp_anticipo_proveedor", Val(lblFolio.Text))
                ConexionBD.Open()
                sdaEvidencias.Fill(dsEvidencias)
                gvEvidencias.DataBind()
                ConexionBD.Close()
                sdaEvidencias.Dispose()
                dsEvidencias.Dispose()
                gvEvidencias.SelectedIndex = -1


                'Panel
                pnlInicio.Enabled = True
                ocultarPaneles()

                If _txtTipoEscenario.Text = "5" Then
                    llenarGridRegistrosDT()
                Else
                    llenarGridRegistros()
                End If

                If Val(lblPorcentaje.Text) >= 100 Then
                    btnAceptar.Enabled = True
                Else
                    btnAceptar.Enabled = False
                End If
            Else
                Server.Transfer("Login.aspx")
            End If
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "Funciones"

    Public Sub ocultarPaneles()
        Try
            pnlDatos.Visible = False
            ibtnBaja.ImageUrl = "images\Trash_i2.png"
            ibtnBaja.Enabled = False
            ibtnModif.ImageUrl = "images\Edit_i2.png"
            ibtnModif.Enabled = False
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Public Sub llenarGridRegistrosDT()
        Try
            litError.Text = ""
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim sdaCatalogo As New SqlDataAdapter
            Dim dsCatalogo As New DataSet
            ' gvRegistros.Columns(0).Visible = True
            gvRegistros.DataSource = dsCatalogo
            Dim query As String = ""

            query = " select * from dt_comprobacion_anticipo_cuenta where id_ms_comprobacion_anticipo = @id_ms_comprobacion_anticipo and id_dt_comprobacion_anticipo = @id_dt_comprobacion_anticipo "

            sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
            sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_ms_comprobacion_anticipo", Val(lblFolio.Text))
            If _txtIdDTComprobacionAnticipo.Text = "" Then
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_dt_comprobacion_anticipo", Val(gvFacturas.Rows(0).Cells(0).Text))
            Else
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_dt_comprobacion_anticipo", Val(_txtIdDTComprobacionAnticipo.Text))

            End If
            ConexionBD.Open()
            sdaCatalogo.Fill(dsCatalogo)
            gvRegistros.DataBind()
            ConexionBD.Close()
            sdaCatalogo.Dispose()
            dsCatalogo.Dispose()
            'gvRegistros.Columns(0).Visible = False
            gvRegistros.SelectedIndex = -1
            Dim sum As Double = 0

            If gvRegistros.Rows.Count > 0 Then
                For index As Integer = 0 To gvRegistros.Rows.Count - 1
                    sum = sum + Val(gvRegistros.Rows(index).Cells(1).Text)
                Next
            End If
            lblPorcentaje.Text = sum

            If sum > 99 Then
                btnAceptar.Enabled = True
                ibtnAlta.Visible = False
            Else
                btnAceptar.Enabled = False
                ibtnAlta.Visible = True
            End If
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub
    Public Sub llenarGridRegistros()
        Try
            litError.Text = ""
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim sdaCatalogo As New SqlDataAdapter
            Dim dsCatalogo As New DataSet
            ' gvRegistros.Columns(0).Visible = True
            gvRegistros.DataSource = dsCatalogo
            Dim query As String = ""

            query = " select * from dt_comprobacion_anticipo_cuenta where id_ms_comprobacion_anticipo = @id_ms_comprobacion_anticipo"

            sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
            sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_ms_comprobacion_anticipo", Val(lblFolio.Text))
            ConexionBD.Open()
            sdaCatalogo.Fill(dsCatalogo)
            gvRegistros.DataBind()
            ConexionBD.Close()
            sdaCatalogo.Dispose()
            dsCatalogo.Dispose()
            'gvRegistros.Columns(0).Visible = False
            gvRegistros.SelectedIndex = -1
            Dim sum As Double = 0

            If gvRegistros.Rows.Count > 0 Then
                For index As Integer = 0 To gvRegistros.Rows.Count - 1
                    sum = sum + Val(gvRegistros.Rows(index).Cells(1).Text)
                Next
            End If
            lblPorcentaje.Text = sum

            If sum > 99 Then
                btnAceptar.Enabled = True
                ibtnAlta.Visible = False
            Else
                btnAceptar.Enabled = False
                ibtnAlta.Visible = True
            End If
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub
    Public Sub localizar(id_anticipo_cuenta As Integer)
        Try
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim sdaCatalogo As New SqlDataAdapter
            Dim dsCatalogo As New DataSet
            sdaCatalogo.SelectCommand = New SqlCommand("select cuenta_contable, porcentaje, centro_costo, division, zona  from dt_comprobacion_anticipo_cuenta where id_dt_comprobacion_anticipo_cuenta = @id_dt_comprobacion_anticipo_cuenta", ConexionBD)
            sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_dt_comprobacion_anticipo_cuenta", id_anticipo_cuenta)
            ConexionBD.Open()
            sdaCatalogo.Fill(dsCatalogo)
            ConexionBD.Close()
            txtCuentaContable.Text = dsCatalogo.Tables(0).Rows(0).Item("cuenta_contable").ToString
            txtDiv.Text = dsCatalogo.Tables(0).Rows(0).Item("division").ToString
            txtPorcentaje.Text = dsCatalogo.Tables(0).Rows(0).Item("porcentaje").ToString
            txtZona.Text = dsCatalogo.Tables(0).Rows(0).Item("zona").ToString
            txtCentroCosto.Text = dsCatalogo.Tables(0).Rows(0).Item("centro_costo").ToString
            sdaCatalogo.Dispose()
            dsCatalogo.Dispose()
            bloqueoPantalla()

        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub
    Public Sub bloqueoPantalla()
        pnlGrid.Visible = False
        pnlDatos.Visible = True
    End Sub
    Public Sub habilitarCampos(Valor As Boolean)
        Try
            txtCuentaContable.Enabled = Valor
            txtDiv.Enabled = Valor
            txtPorcentaje.Enabled = Valor
            txtZona.Enabled = Valor
            txtCentroCosto.Enabled = Valor
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub
    Public Sub limpiarCampos()
        Try
            txtCuentaContable.Text = ""
            txtDiv.Text = ""
            txtPorcentaje.Text = ""
            txtZona.Text = ""
            txtCentroCosto.Text = ""
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Public Function validar()
        Try
            litError.Text = ""
            Dim conteo As Integer = 0
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim SCMTemp As SqlCommand = New System.Data.SqlClient.SqlCommand
            SCMTemp.Connection = ConexionBD
            Dim query As String
            SCMTemp.CommandText = ""
            SCMTemp.Parameters.Clear()
            Select Case _txtTipoMov.Text 'Tipo de Ajuste (alta o modificación)
                Case "A"
                    If _txtTipoEscenario.Text = "5" Then
                        query = " SELECT COUNT(*) FROM dt_comprobacion_anticipo_cuenta " +
                         " WHERE cuenta_contable = @cuenta_contable " +
                         " AND id_ms_comprobacion_anticipo = @id_ms_comprobacion_anticipo " +
                         " AND id_dt_comprobacion_anticipo = @id_dt_comprobacion_anticipo " +
                         " AND centro_costo = @centro_costo " +
                         " AND division = @division " +
                         " AND zona = @zona"
                    Else
                        query = " SELECT COUNT(*) FROM dt_comprobacion_anticipo_cuenta " +
                         " WHERE cuenta_contable = @cuenta_contable " +
                         " AND id_ms_comprobacion_anticipo = @id_ms_comprobacion_anticipo " +
                         " AND centro_costo = @centro_costo " +
                         " AND division = @division " +
                         " AND zona = @zona"
                    End If


                    SCMTemp.CommandText = query
                Case Else

                    If _txtTipoEscenario.Text = "5" Then
                        query = " SELECT COUNT(*) FROM dt_comprobacion_anticipo_cuenta " +
                            " WHERE cuenta_contable = @cuenta_contable " +
                            " AND id_ms_comprobacion_anticipo = @id_ms_comprobacion_anticipo " +
                            " AND id_dt_comprobacion_anticipo = @id_dt_comprobacion_anticipo " +
                            " AND centro_costo = @centro_costo " +
                            " AND division = @division " +
                            " AND zona = @zona" +
                            " AND id_dt_comprobacion_anticipo_cuenta <> @id_dt_comprobacion_anticipo_cuenta "
                    Else
                        query = " SELECT COUNT(*) FROM dt_comprobacion_anticipo_cuenta " +
                            " WHERE cuenta_contable = @cuenta_contable " +
                            " AND id_ms_comprobacion_anticipo = @id_ms_comprobacion_anticipo " +
                            " AND centro_costo = @centro_costo " +
                            " AND division = @division " +
                            " AND zona = @zona" +
                            " AND id_dt_comprobacion_anticipo_cuenta <> @id_dt_comprobacion_anticipo_cuenta "
                    End If


                    SCMTemp.CommandText = query
                    'SCMTemp.Parameters.AddWithValue("@id_dt_movimientos_int", Val(gvRegistros.SelectedRow.Cells(0).Text))
                    SCMTemp.Parameters.AddWithValue("@id_dt_comprobacion_anticipo_cuenta", Val(gvRegistros.DataKeys(gvRegistros.SelectedIndex).Values("id_dt_comprobacion_anticipo_cuenta")))

            End Select
            SCMTemp.Parameters.AddWithValue("@cuenta_contable", txtCuentaContable.Text)
            SCMTemp.Parameters.AddWithValue("@id_dt_comprobacion_anticipo", Val(_txtTipoEscenario.Text))
            SCMTemp.Parameters.AddWithValue("@id_ms_comprobacion_anticipo", Val(lblFolio.Text))
            SCMTemp.Parameters.AddWithValue("@centro_costo", txtCentroCosto.Text)
            SCMTemp.Parameters.AddWithValue("@division", txtDiv.Text)
            SCMTemp.Parameters.AddWithValue("@zona", txtZona.Text)
            ConexionBD.Open()
            conteo = SCMTemp.ExecuteScalar
            ConexionBD.Close()
            If conteo > 0 Then
                validar = False
            Else
                validar = True
            End If
        Catch ex As Exception
            litError.Text = ex.Message
            validar = False
        End Try
    End Function
#End Region

#Region "Botones Grid"
    Protected Sub ibtnAlta_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAlta.Click
        Try

            pnlDatos.Visible = True
            _txtTipoMov.Text = "A"
            lblTipoMov.Text = "Alta"
            habilitarCampos(True)
            limpiarCampos()
            bloqueoPantalla()
            pnlBotones.Visible = False

        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ibtnBaja_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBaja.Click
        Try
            _txtTipoMov.Text = "B"
            lblTipoMov.Text = "Baja"
            localizar(gvRegistros.SelectedRow.Cells(0).Text)
            habilitarCampos(False)
            pnlBotones.Visible = False

        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ibtnModif_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnModif.Click
        Try
            _txtTipoMov.Text = "M"
            lblTipoMov.Text = "Modificación"
            localizar(gvRegistros.SelectedRow.Cells(0).Text)
            habilitarCampos(True)
            pnlBotones.Visible = False

        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub


#End Region

#Region "Botones Aceptar"
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Try
            If Val(lblPorcentaje.Text) = 100 Then
                litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                'Actualizar datos de la Solicitud
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                '' "update ms_movimientos_internos set comentario_codc = @comentario_codc, id_usr_codCont = @id_usr_codCont, fecha_codCont = @fecha_codCont,  estatus = 'A' where id_ms_movimientos_internos = @id_ms_movimientos_internos "
                SCMValores.CommandText = "update ms_comprobacion_anticipo set  fecha_valida_cc = @fecha_valida_cc where id_ms_comprobacion_anticipo = @id_ms_comprobacion_anticipo "
                SCMValores.Parameters.AddWithValue("@fecha_valida_cc", Date.Now)
                SCMValores.Parameters.AddWithValue("@id_ms_comprobacion_anticipo", Val(lblFolio.Text))
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                'Actividad de fin de flujo'
                Dim idActividad As Integer
                idActividad = 150

                'Actualizar Instancia
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(_txtIdMsInst.Text))
                SCMValores.Parameters.AddWithValue("@id_actividad", idActividad)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                'Registrar en Histórico
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                                         "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(_txtIdMsInst.Text))
                SCMValores.Parameters.AddWithValue("@id_actividad", idActividad)
                SCMValores.Parameters.AddWithValue("@fecha", Date.Now)
                SCMValores.Parameters.AddWithValue("@id_usr", Val(_txtIdUsuario.Text))
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                btnAceptar.Enabled = False
                btnRechazar.Visible = False
                txtComentario.Enabled = False
            Else
                litError.Text = "Favor de asignar el 100% de porcentaje"
            End If
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

#End Region

#Region "Botones"
    Protected Sub btnAceptarC_Click(sender As Object, e As EventArgs) Handles btnAceptarC.Click
        Try

            Dim ban As Integer = 0
            If _txtTipoMov.Text = "A" Then
                lblPorcentaje.Text = Val(lblPorcentaje.Text) + Val(txtPorcentaje.Text)
            End If
            If Val(lblPorcentaje.Text) > 100 Then
                litError.Text = "Ya sobrepaso el limite de porcentaje"
            ElseIf txtCentroCosto.Text <> "" And txtDiv.Text <> "" Then
                litError.Text = "No puede tener un centro de costo y una división"
            ElseIf txtDiv.Text <> "" And txtZona.Text = "" Then
                litError.Text = "Favor de poner una zona"
            ElseIf Val(txtPorcentaje.Text) > 100 Or Val(txtPorcentaje.Text) <= 0 Then
                litError.Text = "Porcentaje no valido"
            ElseIf txtCuentaContable.Text = "" Then
                litError.Text = "Favor de introducir una cuenta contable"
            Else
                'Flujo normal'
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                Select Case _txtTipoMov.Text
                    Case "A"
                        If validar() Then
                            SCMValores.CommandText = "insert into dt_comprobacion_anticipo_cuenta(id_ms_comprobacion_anticipo, id_dt_comprobacion_anticipo,  cuenta_contable, porcentaje, centro_costo, division, zona, id_usr_carga, fecha) " +
                                              "values (@id_ms_comprobacion_anticipo, @id_dt_comprobacion_anticipo, @cuenta_contable, @porcentaje, @centro_costo, @division, @zona, @id_usr_carga, @fecha) "
                        Else
                            litError.Text = "Valor invalido, ya existe esta cuenta contable con un porcentaje"
                            ban = 1
                        End If
                    Case "B"
                        SCMValores.CommandText = "delete from dt_comprobacion_anticipo_cuenta where id_dt_comprobacion_anticipo_cuenta = @id_dt_comprobacion_anticipo_cuenta"
                        SCMValores.Parameters.AddWithValue("id_dt_comprobacion_anticipo_cuenta", Val(Me.gvRegistros.DataKeys(Me.gvRegistros.SelectedIndex).Values("id_dt_comprobacion_anticipo_cuenta")))
                        lblPorcentaje.Text = Val(lblPorcentaje.Text) - Val(gvRegistros.SelectedRow.Cells(1).Text)
                    Case Else
                        If validar() Then
                            SCMValores.CommandText = "update dt_comprobacion_anticipo_cuenta set cuenta_contable = @cuenta_contable, porcentaje = @porcentaje, centro_costo = @centro_costo, division = @division, zona = @zona, id_usr_carga = @id_usr_carga, fecha = @fecha where id_dt_comprobacion_anticipo_cuenta = @id_dt_comprobacion_anticipo_cuenta"
                            SCMValores.Parameters.AddWithValue("id_dt_comprobacion_anticipo_cuenta", Val(Me.gvRegistros.DataKeys(Me.gvRegistros.SelectedIndex).Values("id_dt_comprobacion_anticipo_cuenta")))

                        Else
                            litError.Text = "Valor invalido, ya existe esta cuenta contable con un porcentaje"
                            ban = 1
                        End If
                End Select


                If _txtTipoEscenario.Text = "5" Then
                    SCMValores.Parameters.AddWithValue("@id_dt_comprobacion_anticipo", Val(_txtIdDTComprobacionAnticipo.Text))
                Else
                    SCMValores.Parameters.AddWithValue("@id_dt_comprobacion_anticipo", DBNull.Value)
                End If

                If ban = 0 Then
                    SCMValores.Parameters.AddWithValue("@id_ms_comprobacion_anticipo", Val(lblFolio.Text))
                    SCMValores.Parameters.AddWithValue("@cuenta_contable", txtCuentaContable.Text)
                    SCMValores.Parameters.AddWithValue("@porcentaje", txtPorcentaje.Text)
                    SCMValores.Parameters.AddWithValue("@centro_costo", txtCentroCosto.Text)
                    SCMValores.Parameters.AddWithValue("@division", txtDiv.Text)
                    SCMValores.Parameters.AddWithValue("@zona", txtZona.Text)
                    SCMValores.Parameters.AddWithValue("@id_usr_carga", Val(_txtIdUsuario.Text))
                    SCMValores.Parameters.AddWithValue("@fecha", Date.Now)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()
                    limpiarCampos()
                    pnlDatos.Visible = False
                    pnlGrid.Visible = True
                    pnlBotones.Visible = True
                    If _txtTipoEscenario.Text = "5" Then
                        llenarGridRegistrosDT()
                    Else
                        llenarGridRegistros()
                    End If

                End If


            End If
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            litError.Text = ""
            limpiarCampos()
            pnlDatos.Visible = False
            pnlGrid.Visible = True
            pnlBotones.Visible = True
            llenarGridRegistros()
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "Grid"
    Protected Sub gvRegistros_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRegistros.SelectedIndexChanged
        Try
            litError.Text = ""
            ibtnBaja.Enabled = True
            ibtnBaja.ImageUrl = "images\Trash.png"
            ibtnModif.Enabled = True
            ibtnModif.ImageUrl = "images\Edit.png"

        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvFacturas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvFacturas.SelectedIndexChanged
        Try
            ibtnAlta.Enabled = True
            ibtnAlta.ImageUrl = "images\Add.png"
            gvFacturas.Columns(0).Visible = True
            _txtIdDTComprobacionAnticipo.Text = gvFacturas.SelectedRow().Cells(0).Text
            gvFacturas.Columns(0).Visible = False
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

#End Region

End Class
