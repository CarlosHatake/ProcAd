Public Class _03
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
                        Session("Error") = ""

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim sdaSol As New SqlDataAdapter
                        Dim dsSol As New DataSet
                        Dim query As String
                        query = "select ms_anticipo.id_ms_anticipo " +
                                "     , ms_anticipo.empleado " +
                                "     , ms_anticipo.no_proveedor " +
                                "     , ms_anticipo.empresa " +
                                "     , ms_anticipo.autorizador " +
                                "     , ms_anticipo.periodo_comp " +
                                "     , ms_anticipo.destino " +
                                "     , ms_anticipo.actividad " +
                                "     , ms_anticipo.no_personas " +
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
                                "     , ms_recursos.id_cc " +
                                "     , ms_recursos.periodo_fin " +
                                "from ms_instancia " +
                                "  left join ms_anticipo on ms_instancia.id_ms_sol = ms_anticipo.id_ms_anticipo and ms_instancia.tipo = 'A' " +
                                "  left join ms_recursos on ms_recursos.id_ms_anticipo = ms_anticipo.id_ms_anticipo " +
                                "where id_ms_instancia = @idMsInst "
                        sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@idMsInst", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()
                        .lblFolio.Text = dsSol.Tables(0).Rows(0).Item("id_ms_anticipo").ToString()
                        .lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("empleado").ToString()
                        .lblNoProveedor.Text = dsSol.Tables(0).Rows(0).Item("no_proveedor").ToString()
                        .lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                        .lblAutorizador.Text = dsSol.Tables(0).Rows(0).Item("autorizador").ToString()
                        .lblPeriodo.Text = dsSol.Tables(0).Rows(0).Item("periodo_comp").ToString()
                        .lblDestino.Text = dsSol.Tables(0).Rows(0).Item("destino").ToString()
                        .txtAct.Text = dsSol.Tables(0).Rows(0).Item("actividad").ToString()
                        .wneNoPersonas.Value = Val(dsSol.Tables(0).Rows(0).Item("no_personas").ToString())
                        If Val(dsSol.Tables(0).Rows(0).Item("dias_hospedaje").ToString()) = 0 Then
                            .wneDiasH.Text = ""
                            .wceMontoH.Text = ""
                        Else
                            .wneDiasH.Value = Val(dsSol.Tables(0).Rows(0).Item("dias_hospedaje").ToString())
                            .wceMontoH.Value = Val(dsSol.Tables(0).Rows(0).Item("monto_hospedaje").ToString())
                        End If
                        If Val(dsSol.Tables(0).Rows(0).Item("dias_alimentos").ToString()) = 0 Then
                            .wneDiasA.Text = ""
                            .wceMontoA.Text = ""
                        Else
                            .wneDiasA.Value = Val(dsSol.Tables(0).Rows(0).Item("dias_alimentos").ToString())
                            .wceMontoA.Value = Val(dsSol.Tables(0).Rows(0).Item("monto_alimentos").ToString())
                        End If
                        If Val(dsSol.Tables(0).Rows(0).Item("dias_casetas").ToString()) = 0 Then
                            .wneDiasC.Text = ""
                            .wceMontoC.Text = ""
                        Else
                            .wneDiasC.Value = Val(dsSol.Tables(0).Rows(0).Item("dias_casetas").ToString())
                            .wceMontoC.Value = Val(dsSol.Tables(0).Rows(0).Item("monto_casetas").ToString())
                        End If
                        If Val(dsSol.Tables(0).Rows(0).Item("dias_otros").ToString()) = 0 Then
                            .wneDiasO.Text = ""
                            .wceMontoO.Text = ""
                            .lblOtros.Text = ""
                        Else
                            .wneDiasO.Value = Val(dsSol.Tables(0).Rows(0).Item("dias_otros").ToString())
                            .wceMontoO.Value = Val(dsSol.Tables(0).Rows(0).Item("monto_otros").ToString())
                            .lblOtros.Text = dsSol.Tables(0).Rows(0).Item("otros_especifico").ToString()
                        End If
                        .lblTipoPago.Text = dsSol.Tables(0).Rows(0).Item("tipo_pago").ToString()
                        .wceMontoT.Value = Val(dsSol.Tables(0).Rows(0).Item("importe").ToString())
                        .lblMontoTLetra.Text = "(" + montoLetra() + ")"
                        ._txtIdCC.Text = dsSol.Tables(0).Rows(0).Item("id_cc").ToString()
                        ._txtPeriodoFin.Text = dsSol.Tables(0).Rows(0).Item("periodo_fin").ToString()
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
            If Me.wceMontoT.Value = 1 And op = 1 Then
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

#Region "Registrar / Rechazar"

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""

                If Session("Error") = "" Then
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    Dim fecha As DateTime = Date.Now
                    While Val(._txtBan.Text) = 0
                        'Actualizar datos de la Solicitud
                        Dim valor As Integer = 0
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "DECLARE @valorR int;  Execute Sp_U_GenTrans  @id_usr_pago , @fecha_pago , @id_ms_anticipo , @id_ms_instancia , @id_actividad ,  @valorR OUTPUT; select @valorR"
                        SCMValores.Parameters.AddWithValue("@id_usr_pago", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@fecha_pago", fecha)
                        SCMValores.Parameters.AddWithValue("@id_ms_anticipo", Val(.lblFolio.Text))
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 4)
                        ConexionBD.Open()
                        valor = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        If valor = 0 Then

                            Server.Transfer("Menu.aspx")
                        End If
                        SCMValores.Parameters.Clear()

                        'SCMValores.CommandText = ""
                        'SCMValores.Parameters.Clear()
                        'SCMValores.CommandText = "update ms_anticipo set id_usr_pago = @id_usr_pago, fecha_pago = @fecha_pago, status = 'TR' where id_ms_anticipo = @id_ms_anticipo "
                        'SCMValores.Parameters.AddWithValue("@id_usr_pago", Val(._txtIdUsuario.Text))
                        'SCMValores.Parameters.AddWithValue("@fecha_pago", fecha)
                        'SCMValores.Parameters.AddWithValue("@id_ms_anticipo", Val(.lblFolio.Text))
                        'ConexionBD.Open()
                        'SCMValores.ExecuteNonQuery()
                        'ConexionBD.Close()

                        ._txtBan.Text = 1

                        ''Actualizar Instancia
                        'SCMValores.Parameters.Clear()
                        'SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                        'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        'SCMValores.Parameters.AddWithValue("@id_actividad", 4)
                        'ConexionBD.Open()
                        'SCMValores.ExecuteNonQuery()
                        'ConexionBD.Close()

                        ''Registrar en Histórico
                        'SCMValores.Parameters.Clear()
                        'SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                        '                         "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                        'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        'SCMValores.Parameters.AddWithValue("@id_actividad", 4)
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
                                                 "from ms_anticipo " +
                                                 "  left join cg_usuario on ms_anticipo.id_usr_solicita = cg_usuario.id_usuario " +
                                                 "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                 "where id_ms_anticipo = @id_ms_anticipo "
                        SCMValores.Parameters.AddWithValue("@id_ms_anticipo", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        destinatario = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        Mensaje.[To].Add(destinatario)
                        Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                        'Mensaje.From = New System.Net.Mail.MailAddress("soporte.unne.procad@gmail.com", "ProcAd", System.Text.Encoding.UTF8)
                        Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                        Mensaje.Subject = "ProcAd - Anticipo No. " + .lblFolio.Text + " Procesado"
                        Dim texto As String
                        texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                                "La transferencia del anticipo número <b>" + .lblFolio.Text + "</b> ya fue procesada. <br></span>"
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

                        .btnAceptar.Enabled = False
                        .btnRechaza.Enabled = False
                    End While
                Else
                    Server.Transfer("Menu.aspx")
                End If

            Catch ex As Exception
                If ex.Message = "Subproceso anulado." Then
                    Session("Error") = "S"
                End If
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnRechaza_Click(sender As Object, e As EventArgs) Handles btnRechaza.Click
        With Me
            Try
                .litError.Text = ""
                If Session("Error") = "" Then
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    Dim fecha As DateTime = Date.Now
                    Dim valor As Integer = 0
                    While Val(._txtBan.Text) = 0
                        'Actualizar datos de la Solicitud

                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "DECLARE @valorR int;  Execute Sp_U_GenTrans  @id_usr_pago , @fecha_pago , @id_ms_anticipo , @id_ms_instancia , @id_actividad ,  @valorR OUTPUT; select @valorR as 'valorR'"
                        SCMValores.Parameters.AddWithValue("@id_usr_pago", Val(_txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@fecha_pago", Date.Now)
                        SCMValores.Parameters.AddWithValue("@id_ms_anticipo", Val(lblFolio.Text))
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(_txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 7)
                        ConexionBD.Open()
                        valor = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        If Val(valor) = 0 Then

                            Server.Transfer("Menu.aspx")
                        End If
                        SCMValores.Parameters.Clear()

                        'SCMValores.CommandText = ""
                        'SCMValores.Parameters.Clear()
                        'SCMValores.CommandText = "update ms_anticipo set id_usr_pago = @id_usr_pago, fecha_pago = @fecha_pago, status = 'ZC' where id_ms_anticipo = @id_ms_anticipo "
                        'SCMValores.Parameters.AddWithValue("@id_usr_pago", Val(._txtIdUsuario.Text))
                        'SCMValores.Parameters.AddWithValue("@fecha_pago", fecha)
                        'SCMValores.Parameters.AddWithValue("@id_ms_anticipo", Val(.lblFolio.Text))
                        'ConexionBD.Open()
                        'SCMValores.ExecuteNonQuery()
                        'ConexionBD.Close()

                        ''Actualizar datos de la Solicitud de Recursos
                        'SCMValores.Parameters.Clear()
                        'SCMValores.CommandText = "update ms_recursos set monto_pgv_ep = 0 where id_ms_anticipo = @id_ms_anticipo "
                        'SCMValores.Parameters.AddWithValue("@id_ms_anticipo", Val(.lblFolio.Text))
                        'ConexionBD.Open()
                        'SCMValores.ExecuteNonQuery()
                        'ConexionBD.Close()

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
                        sdaPGV.SelectCommand.Parameters.AddWithValue("@año", CDate(._txtPeriodoFin.Text).Date.Year)
                        sdaPGV.SelectCommand.Parameters.AddWithValue("@mes", CDate(._txtPeriodoFin.Text).Date.Month())
                        ConexionBD.Open()
                        sdaPGV.Fill(dsPGV)
                        ConexionBD.Close()

                        Dim mes As String
                        If CDate(._txtPeriodoFin.Text).Date.Month() < 10 Then
                            mes = "0" + CDate(._txtPeriodoFin.Text).Date.Month().ToString
                        Else
                            mes = CDate(._txtPeriodoFin.Text).Date.Month().ToString
                        End If
                        SCMValores.CommandText = "update ms_presupuesto " +
                                                 "  set mes_" + mes + "_ep = @pgvEP, mes_" + mes + "_r = @pgvR " +
                                                 "where id_cc = @idCC " +
                                                 "  and año = @año "
                        SCMValores.Parameters.AddWithValue("@pgvEP", Val(dsPGV.Tables(0).Rows(0).Item("pgvEP").ToString()))
                        SCMValores.Parameters.AddWithValue("@pgvR", Val(dsPGV.Tables(0).Rows(0).Item("pgvR").ToString()))
                        SCMValores.Parameters.AddWithValue("@idCC", Val(._txtIdCC.Text))
                        SCMValores.Parameters.AddWithValue("@año", CDate(._txtPeriodoFin.Text).Date.Year)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        ._txtBan.Text = 1

                        'Actualizar Instancia
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 7)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Registrar en Histórico
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " +
                                                 "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 7)
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
                                                 "from ms_anticipo " +
                                                 "  left join cg_usuario on ms_anticipo.id_usr_solicita = cg_usuario.id_usuario " +
                                                 "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                 "where id_ms_anticipo = @id_ms_anticipo "
                        SCMValores.Parameters.AddWithValue("@id_ms_anticipo", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        destinatario = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        Mensaje.[To].Add(destinatario)
                        Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                        'Mensaje.From = New System.Net.Mail.MailAddress("soporte.unne.procad@gmail.com", "ProcAd", System.Text.Encoding.UTF8)
                        Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                        Mensaje.Subject = "ProcAd - Anticipo No. " + .lblFolio.Text + " Cancelado"
                        Dim texto As String
                        texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                                "El anticipo número <b>" + .lblFolio.Text +
                                "</b> fue cancelado. <br></span>"
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

                        .btnAceptar.Enabled = False
                        .btnRechaza.Enabled = False
                    End While
                Else
                    Server.Transfer("Menu.aspx")
                End If

            Catch ex As Exception
                If ex.Message = "Subproceso anulado." Then
                    Session("Error") = "S"
                End If
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class