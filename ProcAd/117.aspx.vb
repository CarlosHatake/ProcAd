Public Class _117
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess
    Dim clsCorreo As New Correo
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("idMsInst") = 77651
                    'Session("id_usuario") = 64

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtIdMsInst.Text = Session("idMsInst")
                        ._txtBan.Text = 0
                        .pnlUpdateAmpliacion.Visible = False
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim sdaSol As New SqlDataAdapter
                        Dim dsSol As New DataSet
                        Dim query As String
                        query = " select id_ms_ampliacion_p " +
                                     " , id_ms_presupuesto " +
                                     " , solicita  " +
                                     " , empresa" +
                                     " , centro_costo" +
                                     " , just_motivo " +
                                     " , just_beneficio" +
                                     " , just_impacto" +
                                     " , msapml.autorizador" +
                                     " , correo" +
                                     " , id_usr_autoriza" +
                                  " from ms_ampliacion_p msapml" +
                                  " left join ms_instancia on msapml.id_ms_ampliacion_p = ms_instancia.id_ms_sol and ms_instancia.tipo = 'SAP' " +
                                  " left join cg_usuario on msapml.id_usr_solicita = cg_usuario.id_usuario" +
                                  " left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado" +
                                  " where id_ms_instancia = @idMsInst"
                        sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@idMsInst", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()
                        .lblFolio.Text = dsSol.Tables(0).Rows(0).Item("id_ms_ampliacion_p").ToString()
                        ._txtIdMsPresup.Text = dsSol.Tables(0).Rows(0).Item("id_ms_presupuesto").ToString()
                        .lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("solicita").ToString()
                        ._txtCorreoSolic.Text = dsSol.Tables(0).Rows(0).Item("correo").ToString()
                        .lblAutorizador.Text = dsSol.Tables(0).Rows(0).Item("autorizador").ToString()
                        .lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                        .lblCC.Text = dsSol.Tables(0).Rows(0).Item("centro_costo").ToString()
                        .txtMotivosCambio.Text = dsSol.Tables(0).Rows(0).Item("just_motivo").ToString()
                        .txtJustBeneficio.Text = dsSol.Tables(0).Rows(0).Item("just_beneficio").ToString()
                        .txtJustImpacto.Text = dsSol.Tables(0).Rows(0).Item("just_impacto").ToString()
                        ._txtIdUsuarioAut.Text = dsSol.Tables(0).Rows(0).Item("id_usr_autoriza").ToString()

                        sdaSol.Dispose()
                        dsSol.Dispose()

                        llenarGvDtAmpliacion()

                        'Correo del autorizador
                        Dim sdaInfo As New SqlDataAdapter
                        Dim dsInfo As New DataSet
                        sdaInfo.SelectCommand = New SqlCommand("SELECT empl.correo " +
                                                               "FROM bd_Empleado.dbo.cg_empleado empl " +
                                                               "left join cg_usuario usr On usr.id_empleado = empl.id_empleado " +
                                                               "where id_usuario = @id_usuario", ConexionBD)
                        sdaInfo.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuarioAut.Text))
                        ConexionBD.Open()
                        sdaInfo.Fill(dsInfo)
                        ConexionBD.Close()
                        ._txtCorreoAut.Text = dsInfo.Tables(0).Rows(0).Item("correo").ToString()



                        'Actualizar Adjuntos
                        Dim sdaArchivos As New SqlDataAdapter
                        Dim dsArchivos As New DataSet
                        .gvAdjuntos.DataSource = dsArchivos
                        'Adjuntos
                        sdaArchivos.SelectCommand = New SqlCommand("Select nombre As archivo " +
                                                                   "     , 'http://148.223.153.43/ProcAd - Adjuntos APGV/' + cast(id_dt_archivo_pgv as varchar(20)) + '-' + nombre as path " +
                                                                   "from dt_archivo_pgv " +
                                                                   "where id_ms_ampliacion_p = @id_ms_ampliacion_p ", ConexionBD)


                        sdaArchivos.SelectCommand.Parameters.AddWithValue("@id_ms_ampliacion_p", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        sdaArchivos.Fill(dsArchivos)
                        .gvAdjuntos.DataBind()
                        ConexionBD.Close()
                        sdaArchivos.Dispose()
                        dsArchivos.Dispose()
                        .gvAdjuntos.SelectedIndex = -1
                        .upAdjuntos.Update()
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


#Region "Validar / Rechazar solicitud"

    Protected Sub btnAutorizar_Click(sender As Object, e As EventArgs) Handles btnAutorizar.Click
        With Me
            Try

                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim fecha As DateTime = Date.Now
                Dim selecAut As Integer = 0, solic As Integer = 0, nv As Integer = 0


                For Each row As GridViewRow In .gvDtAmpliacion.Rows
                    solic = solic + 1
                    If (TryCast(row.FindControl("cbxSeleccionar"), CheckBox)).Checked Then
                        selecAut = selecAut + 1
                    End If
                Next

                If selecAut = 0 Then
                    .litError.Text = "Seleccione un registro para continuar"
                Else
                    Dim valor As Integer
                    valor = 0
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "DECLARE @valorR int;  Execute SP_U_ms_ampliacion_p  @id_dt_ampliacion_p , @monto_nuevo_val ,  @impacto_pres_monto_val , @impacto_pres_porcent_val ,  @id_ms_instancia ,  @id_actividad ,@id_usr  ,@fecha ,@comentario_autoriza ,@id_ms_ampliacion_p , @valorR OUTPUT; select @valorR"
                    SCMValores.Parameters.AddWithValue("@id_dt_ampliacion_p", DBNull.Value)

                    SCMValores.Parameters.AddWithValue("@monto_nuevo_val", DBNull.Value)
                    SCMValores.Parameters.AddWithValue("@impacto_pres_porcent_val", DBNull.Value)
                    SCMValores.Parameters.AddWithValue("@impacto_pres_monto_val", DBNull.Value)
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 106)
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    SCMValores.Parameters.AddWithValue("@comentario_autoriza", DBNull.Value)
                    SCMValores.Parameters.AddWithValue("@id_ms_ampliacion_p", .lblFolio.Text)

                    ConexionBD.Open()
                    valor = SCMValores.ExecuteScalar()
                    ConexionBD.Close()

                    If valor = 0 Then
                        Server.Transfer("Menu.aspx")
                    End If

                    'Actualizacion del validador de presupuesto 
                    For Each row As GridViewRow In .gvDtAmpliacion.Rows
                        If (TryCast(row.FindControl("cbxSeleccionar"), CheckBox)).Checked Then
                            valor = 0
                            'Montos actuales de la solicitud
                            Dim sdaSol As New SqlDataAdapter
                            Dim dsSol As New DataSet
                            Dim query As String

                            query = " select monto_actual, monto_solicita_val " +
                                    " from dt_ampliacion_p  " +
                                    " where id_dt_ampliacion_p = @id_dt_ampliacion_p"

                            sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                            sdaSol.SelectCommand.Parameters.AddWithValue("@id_dt_ampliacion_p", row.Cells(0).Text)
                            ConexionBD.Open()
                            sdaSol.Fill(dsSol)
                            ConexionBD.Close()
                            .wbeMontoActual.Value = dsSol.Tables(0).Rows(0).Item("monto_actual").ToString()
                            .wceMontoSolVal.Value = dsSol.Tables(0).Rows(0).Item("monto_solicita_val").ToString()
                            sdaSol.Dispose()
                            dsSol.Dispose()
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "DECLARE @valorR int;  Execute SP_U_ms_ampliacion_p  @id_dt_ampliacion_p , @monto_nuevo_val ,  @impacto_pres_monto_val , @impacto_pres_porcent_val ,  @id_ms_instancia ,  @id_actividad ,@id_usr  ,@fecha ,@comentario_autoriza ,@id_ms_ampliacion_p , @valorR OUTPUT; select @valorR"
                            SCMValores.Parameters.AddWithValue("@id_dt_ampliacion_p", row.Cells(0).Text)
                            If wbeMontoActual.Value > 0 Then

                                SCMValores.Parameters.AddWithValue("@monto_nuevo_val", .wbeMontoActual.Value + .wceMontoSolVal.Value)
                                SCMValores.Parameters.AddWithValue("@impacto_pres_porcent_val", .wceMontoSolVal.Value / wbeMontoActual.Value)

                                'Si el presupuesto actual es $0.00
                            Else
                                SCMValores.Parameters.AddWithValue("@monto_nuevo_val", .wceMontoSolVal.Value)
                                SCMValores.Parameters.AddWithValue("@impacto_pres_porcent_val", 0)
                            End If

                            SCMValores.Parameters.AddWithValue("@impacto_pres_monto_val", .wceMontoSolVal.Value)
                            SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                            SCMValores.Parameters.AddWithValue("@id_actividad", 106)
                            SCMValores.Parameters.AddWithValue("@fecha", fecha)
                            SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))



                            SCMValores.Parameters.AddWithValue("@comentario_autoriza", DBNull.Value)
                            SCMValores.Parameters.AddWithValue("@id_ms_ampliacion_p", DBNull.Value)

                            ConexionBD.Open()
                            valor = SCMValores.ExecuteScalar()
                            ConexionBD.Close()


                            'SCMValores.CommandText = ""
                            'SCMValores.Parameters.Clear()
                            'SCMValores.CommandText = " update dt_ampliacion_p " +
                            '                             " set " +
                            '                             " monto_nuevo_val = @monto_nuevo_val, " +
                            '                             " impacto_pres_monto_val = @impacto_pres_monto_val, " +
                            '                             " impacto_pres_porcent_val = @impacto_pres_porcent_val " +
                            '                             " where id_dt_ampliacion_p = @id_dt_ampliacion_p"
                        End If

                    Next
                    ' Procedimiento 
                    If valor = 0 Then
                        Server.Transfer("Menu.aspx")
                    Else


                        Dim cabeceraM As String, cuerpoM As String, destinatario As String
                        destinatario = ._txtCorreoAut.Text
                        cabeceraM = "ProcAd - Solicitud de Ampliación No. " + .lblFolio.Text + " por Autorizar"
                        cuerpoM = "Buen día:
                                 <br>
                                 <br> Por medio de la presente le informamos que se generó la solicitud número <b>" + .lblFolio.Text +
                                   "</b> por parte de <b>" + .lblSolicitante.Text + "</b>
                                <br>
                                <br>Favor de validar si procede la Solicitud de Ampliación de Presupuesto"
                        clsCorreo.enviarCorreo(cabeceraM, cuerpoM, destinatario)


                        'Envío de correo al solicitante 

                        cabeceraM = ""
                        cuerpoM = ""
                        destinatario = ""

                        nv = solic - selecAut
                        destinatario = _txtCorreoSolic.Text
                        cabeceraM = "ProcAd - Solicitud de Ampliación No. " + .lblFolio.Text + " Validada"
                        cuerpoM = "Buen día:
                             <br>
                             <br> Por medio de la presente le informamos que de la solicitud número <b>" + .lblFolio.Text + "</b> han sido: 
                             <br>
                               <b> Aprobada:</b> " + selecAut.ToString() + " 
                             <br>
                                <b> Rechazada: </b> " + nv.ToString() + "
                             <br>
                             Por el validador de presupuesto
                            <br>
                            <br>Puede consultar mayor información en ProcAd http://148.223.153.43/ProcAd "
                        clsCorreo.enviarCorreo(cabeceraM, cuerpoM, destinatario)

                        .pnlInicio.Enabled = False

                    End If
                    .btnActPres.Enabled = False
                    .btnRechazar.Enabled = False
                    Server.Transfer("Menu.aspx")
                    ''Actualizar Instancia
                    'SCMValores.Parameters.Clear()
                    'SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                    'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    'SCMValores.Parameters.AddWithValue("@id_actividad", 106)
                    'ConexionBD.Open()
                    'SCMValores.ExecuteNonQuery()
                    'ConexionBD.Close()

                    ''Registrar en Histórico
                    'SCMValores.Parameters.Clear()
                    'SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                    '                             " values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    'SCMValores.Parameters.AddWithValue("@id_actividad", 106)
                    'SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    'SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    'ConexionBD.Open()
                    'SCMValores.ExecuteNonQuery()
                    'ConexionBD.Close()
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With

    End Sub
    Protected Sub btnRechazar_Click(sender As Object, e As EventArgs) Handles btnRechazar.Click
        With Me
            Try

                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim fecha As DateTime = Date.Now
                Dim Selec As Integer = 0, Solic As Integer = 0

                For Each row As GridViewRow In .gvDtAmpliacion.Rows
                    Solic = Solic + 1
                    If (TryCast(row.FindControl("cbxSeleccionar"), CheckBox)).Checked Then
                        Selec = Selec + 1
                    End If
                Next

                If Selec = 0 Then
                    .litError.Text = "Seleccione un registro para continuar"

                ElseIf Selec = Solic Then

                    'Actualizar Instancia
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 118)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Registrar en Histórico
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                                                         "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 118)
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Envío de Correo al solicitante de la ampliacion

                    Dim cabeceraM As String, cuerpoM As String, destinatario As String
                    destinatario = ._txtCorreoSolic.Text
                    cabeceraM = "ProcAd - Solicitud de Ampliación No. " + .lblFolio.Text + " Rechazada"
                    cuerpoM = " Buen día:
                                 <br>
                                 <br> Por medio de la presente le informamos que la solicitud de <b> ampliación de presupuesto para gastos de viaje </b> 
                                 <br>
                                 con número de folio: <b>" + .lblFolio.Text + "</b> del sistema  <b>ProcAd</b> http://148.223.153.43/ProcAd ha sido <b>rechazada.</b>
                                 <br> "
                    clsCorreo.enviarCorreo(cabeceraM, cuerpoM, destinatario)

                    Session("id_actividadM") = 117
                    Session("TipoM") = "SAP"
                    .pnlInicio.Enabled = False
                    'Server.Transfer("Menu.aspx")
                    'End While
                    'Error.Text = "Solicitud rechazada"
                End If
                'llenarGvDtAmpliacion()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Botones"

    Protected Sub btnActPres_Click(sender As Object, e As EventArgs) Handles btnActPres.Click
        With Me
            Try
                .litError.Text = ""

                'Modificación del monto de ampliación solicitado 
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD

                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = " update dt_ampliacion_p " +
                                         " set monto_solicita_val = @monto_solicita_val" +
                                         " where id_dt_ampliacion_p = @id_dt_ampliacion_p "
                SCMValores.Parameters.AddWithValue("@id_dt_ampliacion_p", .gvDtAmpliacion.SelectedRow.Cells(0).Text)
                SCMValores.Parameters.AddWithValue("@monto_solicita_val", .wceMontoSolVal.Value)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                SCMValores.Parameters.Clear()
                SCMValores.CommandText = " update dt_ampliacion_p " +
                                         " set monto_nuevo = (monto_actual + monto_solicita_val)" +
                                         " where id_dt_ampliacion_p = @id_dt_ampliacion_p "
                SCMValores.Parameters.AddWithValue("@id_dt_ampliacion_p", .gvDtAmpliacion.SelectedRow.Cells(0).Text)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                SCMValores.Parameters.Clear()
                SCMValores.CommandText = " update dt_ampliacion_p " +
                                         " set impacto_pres_monto = monto_solicita_val," +
                                         " impacto_pres_porcent = monto_solicita_val / monto_actual" +
                                         " where id_dt_ampliacion_p = @id_dt_ampliacion_p "
                SCMValores.Parameters.AddWithValue("@id_dt_ampliacion_p", .gvDtAmpliacion.SelectedRow.Cells(0).Text)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                llenarGvDtAmpliacion()
                .gvDtAmpliacion.SelectedIndex = -1
                .pnlUpdateAmpliacion.Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString

            End Try
        End With
    End Sub

    Protected Sub btnCancelarActPres_Click(sender As Object, e As EventArgs) Handles btnCancelarActPres.Click
        With Me
            Try
                .pnlUpdateAmpliacion.Visible = False
                .gvDtAmpliacion.SelectedIndex = -1
                .litError.Text = ""

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Acciones"
    Public Sub llenarGvDtAmpliacion()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaSolicitud As New SqlDataAdapter
                Dim dsSolicitud As New DataSet
                Dim monto_valido As Double

                .gvDtAmpliacion.DataSource = dsSolicitud
                'Habilitar columna para actualización
                .gvDtAmpliacion.Columns(0).Visible = True
                .gvDtAmpliacion.Columns(1).Visible = True
                'Catálogo de Unidades agregados
                sdaSolicitud.SelectCommand = New SqlCommand(" select id_dt_ampliacion_p " +
                                                           "     , año " +
                                                           "     , mes " +
                                                           "     , monto_actual " +
                                                           "     , monto_solicita " +
                                                           "     , monto_nuevo " +
                                                           "     , impacto_pres_monto " +
                                                           "     , impacto_pres_porcent " +
                                                           "     , monto_solicita_val " +
                                                           " from dt_ampliacion_p " +
                                                           " where id_ms_ampliacion_p = @id_ms_ampliacion_p ", ConexionBD)
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_ms_ampliacion_p", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaSolicitud.Fill(dsSolicitud)
                .gvDtAmpliacion.DataBind()
                ConexionBD.Close()

                sdaSolicitud.Dispose()
                dsSolicitud.Dispose()
                Dim cont As Integer
                cont = .gvDtAmpliacion.Rows.Count()

                For Each row As GridViewRow In .gvDtAmpliacion.Rows

                    monto_valido = Math.Round(Convert.ToDouble(row.Cells(9).Text.Replace("$", "")), 4)
                    If cont > 0 And monto_valido > 0 Then
                        Dim cbxSeleccionar As CheckBox = TryCast(row.FindControl("cbxSeleccionar"), CheckBox)
                        cbxSeleccionar.Checked = True
                    End If
                Next
                .gvDtAmpliacion.SelectedIndex = -1
                'Inhabilitar columna para vista
                .gvDtAmpliacion.Columns(0).Visible = False
                .gvDtAmpliacion.Columns(1).Visible = False



            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try

        End With
    End Sub

    Protected Sub gvDtAmpliacion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDtAmpliacion.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaSol As New SqlDataAdapter
                Dim dsSol As New DataSet
                Dim query As String
                query = " select monto_solicita, monto_solicita_val" +
                         " from dt_ampliacion_p " +
                         " where id_dt_ampliacion_p = @id_dt_ampliacion_p "
                sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaSol.SelectCommand.Parameters.AddWithValue("@id_dt_ampliacion_p", .gvDtAmpliacion.SelectedRow.Cells(0).Text)
                sdaSol.Fill(dsSol)
                ConexionBD.Close()
                .lblMontoSol.Text = "$" + dsSol.Tables(0).Rows(0).Item("monto_solicita").ToString()
                .wceMontoSolVal.Text = dsSol.Tables(0).Rows(0).Item("monto_solicita_val").ToString()
                sdaSol.Dispose()
                dsSol.Dispose()

                .pnlUpdateAmpliacion.Visible = True
            Catch ex As Exception
                .litError.Text = ex.ToString

            End Try
        End With
    End Sub

    Protected Sub cbxSeleccionar_CheckedChanged(sender As Object, e As EventArgs)
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD

                For Each row As GridViewRow In .gvDtAmpliacion.Rows
                    If TryCast(row.FindControl("cbxSeleccionar"), CheckBox).Checked Then

                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = " update dt_ampliacion_p " +
                                         " set monto_solicita_val = monto_solicita" +
                                         " where id_dt_ampliacion_p = @id_dt_ampliacion_p "
                        SCMValores.Parameters.AddWithValue("@id_dt_ampliacion_p", row.Cells(0).Text)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()
                        'Exit For
                    Else
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = " update dt_ampliacion_p " +
                                         " set monto_solicita_val = 0" +
                                         " where id_dt_ampliacion_p = @id_dt_ampliacion_p "
                        SCMValores.Parameters.AddWithValue("@id_dt_ampliacion_p", row.Cells(0).Text)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()
                    End If
                Next

                llenarGvDtAmpliacion()
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

    'Protected Sub cbxHeaderS_CheckedChanged(sender As Object, e As EventArgs)
    '    With Me
    '        Try
    '            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
    '            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
    '            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
    '            SCMValores.Connection = ConexionBD
    '            For Each row As GridViewRow In .gvDtAmpliacion.Rows
    '                If TryCast(gvDtAmpliacion.HeaderRow.FindControl("cbxHeaderS"), CheckBox).Checked Then
    '                    Dim cbxSeleccionar As CheckBox = TryCast(row.FindControl("cbxSeleccionar"), CheckBox)
    '                    cbxSeleccionar.Checked = True
    '                    .btnAutorizar.Enabled = True
    '                    .btnRechazar.Enabled = True

    '                    SCMValores.CommandText = ""
    '                    SCMValores.Parameters.Clear()
    '                    SCMValores.CommandText = " update dt_ampliacion_p " +
    '                                     " set monto_solicita_val = monto_solicita" +
    '                                     " where id_dt_ampliacion_p = @id_dt_ampliacion_p "
    '                    SCMValores.Parameters.AddWithValue("@id_dt_ampliacion_p", row.Cells(0).Text)
    '                    ConexionBD.Open()
    '                    SCMValores.ExecuteNonQuery()
    '                    ConexionBD.Close()
    '                Else
    '                    Dim cbxSeleccionar As CheckBox = TryCast(row.FindControl("cbxSeleccionar"), CheckBox)
    '                    cbxSeleccionar.Checked = False
    '                    .btnAutorizar.Enabled = False
    '                    .btnRechazar.Enabled = False

    '                    SCMValores.CommandText = ""
    '                    SCMValores.Parameters.Clear()
    '                    SCMValores.CommandText = " update dt_ampliacion_p " +
    '                                     " set monto_solicita_val = 0" +
    '                                     " where id_dt_ampliacion_p = @id_dt_ampliacion_p "
    '                    SCMValores.Parameters.AddWithValue("@id_dt_ampliacion_p", row.Cells(0).Text)
    '                    ConexionBD.Open()
    '                    SCMValores.ExecuteNonQuery()
    '                    ConexionBD.Close()
    '                End If
    '            Next
    '            'llenarGvDtAmpliacion()
    '        Catch ex As Exception
    '            .litError.Text = ex.ToString()
    '        End Try
    '    End With
    'End Sub


#End Region

End Class