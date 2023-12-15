Public Class _125
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        _txtIdUsuario.Text = Session("id_usuario")
                        _txtIdMsInst.Text = Session("idMsInst")
                        _txtBan.Text = 0


                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim sdaSol As New SqlDataAdapter
                        Dim dsSol As New DataSet
                        Dim query As String
                        query = " select " +
                                "(select nombre + ' ' + ap_paterno + ' ' + ap_materno from bd_Empleado.dbo.cg_empleado where id_empleado = CG.id_empleado) as solicita, " +
                                "Case MSI.tipoM When 'CPP' then 'Cuentas Por Pagar' else 'Cuentas Por Cobrar' end as TipoMovimiento, " +
                                "MSI.empresa, " +
                                "MSI.admon_oper, " +
                                "(select nombre + ' ' + ap_paterno + ' ' + ap_materno from bd_Empleado.dbo.cg_empleado where id_empleado = CGU.id_empleado) as autoriza, " +
                                "MSI.centro_costo, " +
                                "MSI.division, " +
                                "MSI.especificaciones, " +
                                "MSI.id_ms_movimientos_internos, " +
                                "MSI.comentario_autoriza " +
                                "From ms_movimientos_internos MSI " +
                                "Left Join ms_instancia MI on MI.id_ms_sol = MSI.id_ms_movimientos_internos " +
                                "Left Join cg_usuario CG on CG.id_usuario = MSI.id_usr_solicita " +
                                "Left Join cg_usuario CGU on CGU.id_usuario = MSI.id_usr_autoriza " +
                                "where id_ms_instancia = @idMsInst "
                        sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@idMsInst", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()
                        lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("solicita").ToString()
                        lblTipo.Text = dsSol.Tables(0).Rows(0).Item("TipoMovimiento").ToString()
                        lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                        lblAutorizador.Text = dsSol.Tables(0).Rows(0).Item("autoriza").ToString()
                        lblCentroCosto.Text = dsSol.Tables(0).Rows(0).Item("centro_costo").ToString()
                        lblDivision.Text = dsSol.Tables(0).Rows(0).Item("division").ToString()
                        txtEspecificaciones.Text = dsSol.Tables(0).Rows(0).Item("especificaciones").ToString()
                        txtEspecificaciones.Enabled = False

                        If dsSol.Tables(0).Rows(0).Item("admon_oper").ToString = "Admon" Then
                            rbAdministrativo.Checked = True
                            rbAdministrativo.Enabled = False
                            rbOperativo.Enabled = False
                        Else
                            rbOperativo.Checked = True
                            rbOperativo.Enabled = False
                            rbAdministrativo.Enabled = False
                        End If
                        lblFolio.Text = dsSol.Tables(0).Rows(0).Item("id_ms_movimientos_internos").ToString
                        'Llenar Grid de Evidencias'
                        Dim sdaArchivos As New SqlDataAdapter
                        Dim dsArchivos As New DataSet
                        gvAdjuntos.DataSource = dsArchivos
                        'Ruta Servidor Prueba 172.16.18.239'
                        'Ruta Servidor Bueno 148.223.153.43'
                        sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " +
                                                                   "     , 'http://148.223.153.43/ProcAd/Evidencias MovLib/' + cast(id_ms_movimientos_internos as varchar(20)) + '-' + nombre as path " +
                                                                   "from dt_archivo_movInt " +
                                                                   "where id_ms_movimientos_internos = @idMsMovInt ", ConexionBD)
                        sdaArchivos.SelectCommand.Parameters.AddWithValue("@idMsMovInt", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        sdaArchivos.Fill(dsArchivos)
                        .gvAdjuntos.DataBind()
                        ConexionBD.Close()
                        sdaArchivos.Dispose()
                        dsArchivos.Dispose()
                        .gvAdjuntos.SelectedIndex = -1

                        .pnlDatos.Visible = False
                        .ibtnBaja.Enabled = False
                        .ibtnModif.Enabled = False
                        llenarGridRegistros()
                    Else
                        Server.Transfer("Login.aspx")
                    End If
                Catch ex As Exception
                    litError.Text = ex.Message
                End Try
            End With
        End If
    End Sub

#Region "Funciones"
    Public Sub Localizar(idDtMovInt As Integer)
        Try
            litError.Text = ""
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim sdaCodigo As New SqlDataAdapter
            Dim dsCodigo As New DataSet
            sdaCodigo.SelectCommand = New SqlCommand("Select id_dt_movimientos_int, cuenta_contable, porcentaje, centro_costo, division, zona " +
                                                     "from dt_movimientos_int " +
                                                     "where id_dt_movimientos_int = @id_dt_movimientos_int", ConexionBD)
            sdaCodigo.SelectCommand.Parameters.AddWithValue("@id_dt_movimientos_int", idDtMovInt)
            ConexionBD.Open()
            sdaCodigo.Fill(dsCodigo)
            ConexionBD.Close()
            txtCuentaContable.Text = dsCodigo.Tables(0).Rows(0).Item("cuenta_contable").ToString
            txtPorcentaje.Text = dsCodigo.Tables(0).Rows(0).Item("porcentaje").ToString
            txtCentroCosto.Text = dsCodigo.Tables(0).Rows(0).Item("centro_costo").ToString
            txtDiv.Text = dsCodigo.Tables(0).Rows(0).Item("division").ToString
            txtZona.Text = dsCodigo.Tables(0).Rows(0).Item("zona").ToString

            Me.pnlDatos.Visible = True 

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
            query = " select * from dt_movimientos_int where id_ms_movimientos_internos = @id_ms_movimientos_internos "
            sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
            sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_ms_movimientos_internos", Val(lblFolio.Text))
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
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub
    Public Function validar()
        With Me
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
                Select Case _txtTipoMov.Text 'Tipo de Ajuste (alta, baja o modificación)
                    Case "A"
                        query = " SELECT COUNT(*) FROM dt_movimientos_int " +
                                " WHERE cuenta_contable = @cuenta_contable " +
                                " AND id_ms_movimientos_internos = @id_ms_movimientos_internos " +
                                " AND centro_costo = @centro_costo " +
                                " AND division = @division " +
                                " AND zona = @zona"

                        'query = "select count(*) " +
                        '        "from dt_movimientos_int " +
                        '        "where cuenta_contable = @cuenta_contable " +
                        '        "  and id_ms_movimientos_internos = @id_ms_movimientos_internos and centro_costo = @centro_costo"
                        SCMTemp.CommandText = query
                    Case Else

                        query = " SELECT COUNT(*) FROM dt_movimientos_int " +
                                " WHERE cuenta_contable = @cuenta_contable " +
                                " AND id_ms_movimientos_internos = @id_ms_movimientos_internos " +
                                " AND centro_costo = @centro_costo " +
                                " AND division = @division " +
                                " AND zona = @zona" +
                                " AND id_dt_movimientos_int <> @id_dt_movimientos_int "

                        'query = "select count(*) " +
                        '        "from dt_movimientos_int " +
                        '        "where cuenta_contable = @cuenta_contable " +
                        '        "  and id_ms_movimientos_internos  = @id_ms_movimientos_internos  " +
                        '        "  and id_dt_movimientos_int <> @id_dt_movimientos_int "
                        SCMTemp.CommandText = query
                        'SCMTemp.Parameters.AddWithValue("@id_dt_movimientos_int", Val(gvRegistros.SelectedRow.Cells(0).Text))
                        SCMTemp.Parameters.AddWithValue("@id_dt_movimientos_int", Val(.gvRegistros.DataKeys(gvRegistros.SelectedIndex).Values("id_dt_movimientos_int")))

                End Select
                SCMTemp.Parameters.AddWithValue("@cuenta_contable", txtCuentaContable.Text)
                SCMTemp.Parameters.AddWithValue("@id_ms_movimientos_internos", Val(lblFolio.Text))
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
        End With
    End Function

    Public Sub limpiarPantalla()
        txtCuentaContable.Text = ""
        txtPorcentaje.Text = ""
        txtCentroCosto.Text = ""
        txtDiv.Text = ""
        txtZona.Text = ""
    End Sub

#End Region

#Region "Botones "
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
                SCMValores.CommandText = "update ms_movimientos_internos set comentario_codc = @comentario_codc, id_usr_codCont = @id_usr_codCont, fecha_codCont = @fecha_codCont,  estatus = 'A' where id_ms_movimientos_internos = @id_ms_movimientos_internos "
                SCMValores.Parameters.AddWithValue("@fecha_codCont", Date.Now)
                SCMValores.Parameters.AddWithValue("@comentario_codc", txtComentario.Text.Trim)
                SCMValores.Parameters.AddWithValue("@id_usr_codCont", Val(_txtIdUsuario.Text))
                SCMValores.Parameters.AddWithValue("@id_ms_movimientos_internos", Val(lblFolio.Text))
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                'Actividad de fin de flujo'
                Dim idActividad As Integer
                idActividad = 127

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

    Protected Sub btnRechazar_Click(sender As Object, e As EventArgs) Handles btnRechazar.Click
        Try
            If txtComentario.Text = "" Then
                litError.Text = "Favor de escribir un comentario"
            Else
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                'Actualizar datos de la Solicitud
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "update ms_movimientos_internos set estatus = 'Z', comentario_codc = @comentario_codc, id_usr_codCont = @id_usr_codCont where id_ms_movimientos_internos = @id_ms_movimientos_internos "
                SCMValores.Parameters.AddWithValue("@id_ms_movimientos_internos", Val(lblFolio.Text))
                SCMValores.Parameters.AddWithValue("@comentario_codc", txtComentario.Text.Trim)
                SCMValores.Parameters.AddWithValue("@id_usr_codCont", Val(_txtIdUsuario.Text))
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                Dim idActividad As Integer
                idActividad = 126
                'Actividad para cancelar'

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

                'Envío de Correo
                Dim Mensaje As New System.Net.Mail.MailMessage()
                Dim destinatario As String = ""
                'Obtener el Correo del Solicitante
                SCMValores.CommandText = "select cgEmpl.correo " +
                                                 "from ms_movimientos_internos " +
                                                 "  left join cg_usuario on ms_movimientos_internos.id_usr_solicita = cg_usuario.id_usuario " +
                                                 "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado  " +
                                                 "where id_ms_movimientos_internos = @id_ms_movInt "
                SCMValores.Parameters.AddWithValue("@id_ms_movInt", Val(lblFolio.Text))
                ConexionBD.Open()
                destinatario = SCMValores.ExecuteScalar()
                ConexionBD.Close()

                Mensaje.[To].Add(destinatario)
                Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                Mensaje.Subject = "ProcAd - Solicitud de Movimientos Internos " + lblFolio.Text + " Rechazada"
                Dim texto As String
                texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                                "La solicitud número <b>" + lblFolio.Text + "</b> fue rechazada.  <br>" +
                                "Comentarios: <b>" + txtComentario.Text.Trim + "</b><br></span>"
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
                    litError.Text = ex.ToString
                End Try

            End If
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ibtnAlta_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAlta.Click
        Try
            _txtTipoMov.Text = "A"
            txtCuentaContable.Text = ""
            txtPorcentaje.Text = ""
            txtCentroCosto.Text = ""
            txtDiv.Text = ""
            txtZona.Text = ""
            pnlGrid.Visible = False
            pnlBotones.Visible = False
            pnlDatos.Visible = True
            txtCuentaContable.Enabled = True
            txtPorcentaje.Enabled = True
            txtCentroCosto.Enabled = True
            txtDiv.Enabled = True
            txtZona.Enabled = True
            lblTipoMov.Text = "Alta"
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ibtnBaja_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBaja.Click
        Try
            _txtTipoMov.Text = "B"
            'Localizar(Val(gvRegistros.SelectedRow.Cells(0).Text))
            Localizar(Me.gvRegistros.DataKeys(Me.gvRegistros.SelectedIndex).Values("id_dt_movimientos_int"))
            txtCuentaContable.Enabled = False
            txtPorcentaje.Enabled = False
            txtCentroCosto.Enabled = False
            txtDiv.Enabled = False
            txtZona.Enabled = False
            pnlGrid.Visible = False
            pnlDatos.Visible = True
            pnlBotones.Visible = False
            lblTipoMov.Text = "Eliminar"
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ibtnModif_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnModif.Click
        Try
            _txtTipoMov.Text = "M"
            'Localizar(Val(gvRegistros.SelectedRow.Cells(0).Text))
            Localizar(Me.gvRegistros.DataKeys(Me.gvRegistros.SelectedIndex).Values("id_dt_movimientos_int"))
            pnlGrid.Visible = False
            pnlBotones.Visible = False
            lblTipoMov.Text = "Modificación"
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvRegistros_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRegistros.SelectedIndexChanged
        With Me
            .litError.Text = ""
            .ibtnBaja.Enabled = True
            .ibtnBaja.ImageUrl = "images\Trash.png"
            .ibtnModif.Enabled = True
            .ibtnModif.ImageUrl = "images\Edit.png"
        End With
    End Sub

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
                            SCMValores.CommandText = "insert into dt_movimientos_int(id_ms_movimientos_internos, cuenta_contable, porcentaje, centro_costo, division, zona, id_usr_carga, fecha) " +
                                              "values (@id_ms_movimientos_internos, @cuenta_contable, @porcentaje, @centro_costo, @division, @zona, @id_usr_carga, @fecha) "
                        Else
                            litError.Text = "Valor invalido, ya existe esta cuenta contable con un porcentaje"
                            ban = 1
                        End If
                    Case "B"
                        SCMValores.CommandText = "delete from dt_movimientos_int where id_dt_movimientos_int = @id_dt_movimientos_int"
                        'SCMValores.Parameters.AddWithValue("id_dt_movimientos_int", Val(gvRegistros.SelectedRow.Cells(0).Text))
                        SCMValores.Parameters.AddWithValue("id_dt_movimientos_int", Val(Me.gvRegistros.DataKeys(Me.gvRegistros.SelectedIndex).Values("id_dt_movimientos_int")))
                        'lblPorcentaje.Text = Val(lblPorcentaje.Text) - Val(gvRegistros.SelectedRow.Cells(2).Text)
                        lblPorcentaje.Text = Val(lblPorcentaje.Text) - Val(gvRegistros.SelectedRow.Cells(1).Text)
                    Case Else
                        If validar() Then
                            SCMValores.CommandText = "update dt_movimientos_int set cuenta_contable = @cuenta_contable, porcentaje = @porcentaje, centro_costo = @centro_costo, division = @division, zona = @zona, id_usr_carga = @id_usr_carga, fecha = @fecha where id_dt_movimientos_int = @id_dt_movimientos_int"
                            'SCMValores.Parameters.AddWithValue("id_dt_movimientos_int", Val(gvRegistros.SelectedRow.Cells(0).Text))
                            SCMValores.Parameters.AddWithValue("id_dt_movimientos_int", Val(Me.gvRegistros.DataKeys(Me.gvRegistros.SelectedIndex).Values("id_dt_movimientos_int")))

                        Else
                            litError.Text = "Valor invalido, ya existe esta cuenta contable con un porcentaje"
                            ban = 1
                        End If
                End Select

                If ban = 0 Then
                    SCMValores.Parameters.AddWithValue("@id_ms_movimientos_internos", Val(lblFolio.Text))
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
                    limpiarPantalla()
                    pnlDatos.Visible = False
                    pnlGrid.Visible = True
                    pnlBotones.Visible = True

                    llenarGridRegistros()
                End If

            End If
        Catch ex As Exception
            litError.Text = ex.Message

        End Try
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            litError.Text = ""
            limpiarPantalla()
            pnlDatos.Visible = False
            pnlGrid.Visible = True
            pnlBotones.Visible = True
            llenarGridRegistros()
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub
#End Region

End Class