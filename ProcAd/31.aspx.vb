Public Class _31
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

                        'Conceptos
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

                        .btnGuardar.Enabled = False
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

#Region "Registrar"

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        With Me
            Try
                .litError.Text = ""

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
                    SCMValores.CommandText = "Declare @valorR int; Execute  Sp_U_ms_comp31 @tipo_recibe , @id_empl_recibe , @id_ms_comp ,  @id_ms_instancia , @id_actividad , @id_usr,  @valorR output; Select @valorR  "
                    'SCMValores.CommandText = "update ms_comp set id_usr_efectivo = @id_usr_efectivo, fecha_efectivo = @fecha_efectivo, id_empl_recibe = @id_empl_recibe, tipo_recibe = @tipo_recibe where id_ms_comp = @id_ms_comp "
                    'SCMValores.Parameters.AddWithValue("@id_usr_efectivo", Val(._txtIdUsuario.Text))
                    'SCMValores.Parameters.AddWithValue("@fecha_efectivo", fecha)
                    SCMValores.Parameters.AddWithValue("@tipo_recibe", ._txtTipoRecibe.Text)
                    SCMValores.Parameters.AddWithValue("@id_empl_recibe", Val(._txtIdEmplRecibe.Text))
                    SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 32)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    valor = SCMValores.ExecuteScalar()
                    ConexionBD.Close()

                    If valor = 0 Then
                        Server.Transfer("Menu.aspx")
                    End If


                    ._txtBan.Text = 1

                    'Actualizar Instancia
                    'SCMValores.Parameters.Clear()
                    'SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                    'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    'SCMValores.Parameters.AddWithValue("@id_actividad", 32)
                    'ConexionBD.Open()
                    'SCMValores.ExecuteNonQuery()
                    'ConexionBD.Close()

                    'Registrar en Histórico
                    'SCMValores.Parameters.Clear()
                    'SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                    '                         "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    'SCMValores.Parameters.AddWithValue("@id_actividad", 32)
                    'SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    'SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    'ConexionBD.Open()
                    'SCMValores.ExecuteNonQuery()
                    'ConexionBD.Close()

                    .pnlInicio.Enabled = False
                End While
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

    Protected Sub txtPin_TextChanged(sender As Object, e As EventArgs) Handles txtPin.TextChanged
        With Me
            Dim contEstadia As Integer = 0
            Dim ban As Integer = 0
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
            SCMValores.Connection = ConexionBD
            'Determinar si el Efectivo es de Estadía
            SCMValores.CommandText = ""
            SCMValores.Parameters.Clear()
            SCMValores.CommandText = "select (select count(*) " +
                                      "        from dt_comp " +
                                      "          left join cg_concepto on dt_comp.id_concepto = cg_concepto.id_concepto " +
                                      "        where dt_comp.id_ms_comp = ms_comp.id_ms_comp " +
                                      "          and dt_comp.tipo = 'T' " +
                                      "          and cg_concepto.nombre_concepto = 'ESTADIA') as contEstadia " +
                                      "from ms_comp " +
                                      "where id_ms_comp = @id_ms_comp "
            SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
            ConexionBD.Open()
            contEstadia = SCMValores.ExecuteScalar()
            ConexionBD.Close()

            If contEstadia > 0 Then
                'Estadía
                SCMValores.CommandText = "select count(*) " +
                                         "from ms_comp " +
                                         "  left join bd_Empleado.dbo.cg_empresa cgEmpr on ms_comp.empresa = cgEmpr.nombre " +
                                         "  left join bd_Empleado.dbo.cg_cc cgCC on cgEmpr.id_empresa = cgCC.id_empresa and cgCC.nombre = ms_comp.centro_costo " +
                                         "  left join bd_Empleado.dbo.cg_estadia cgEst on cgEst.id_cc = cgCC.id_cc " +
                                         "where id_ms_comp = @id_ms_comp " +
                                         "  and @fecha between fecha_ini and fecha_fin " +
                                         "  and pin = @pin "
                SCMValores.Parameters.AddWithValue("@fecha", Date.Now.ToShortDateString)
            Else
                'Administrativo / Operario
                SCMValores.CommandText = "select count(*) " +
                                         "from ms_comp " +
                                         "  left join cg_usuario on ms_comp.id_usr_solicita = cg_usuario.id_usuario " +
                                         "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                         "where id_ms_comp = @id_ms_comp " +
                                         "  and isnull(pin, '0000') = @pin "
            End If
            SCMValores.Parameters.AddWithValue("@pin", .txtPin.Text)
            ConexionBD.Open()
            ban = SCMValores.ExecuteScalar()
            ConexionBD.Close()
            If ban > 0 Then
                .btnGuardar.Enabled = True
                If contEstadia > 0 Then
                    'Estadía
                    ._txtTipoRecibe.Text = "E"
                    SCMValores.CommandText = "select id_estadia " +
                                             "from ms_comp " +
                                             "  left join bd_Empleado.dbo.cg_empresa cgEmpr on ms_comp.empresa = cgEmpr.nombre " +
                                             "  left join bd_Empleado.dbo.cg_cc cgCC on cgEmpr.id_empresa = cgCC.id_empresa and cgCC.nombre = ms_comp.centro_costo " +
                                             "  left join bd_Empleado.dbo.cg_estadia cgEst on cgEst.id_cc = cgCC.id_cc " +
                                             "where id_ms_comp = @id_ms_comp " +
                                             "  and @fecha between fecha_ini and fecha_fin " +
                                             "  and pin = @pin "
                Else
                    'Administrativo / Operario
                    ._txtTipoRecibe.Text = "A"
                    SCMValores.CommandText = "select cgEmpl.id_empleado " +
                                             "from ms_comp " +
                                             "  left join cg_usuario on ms_comp.id_usr_solicita = cg_usuario.id_usuario " +
                                             "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                             "where id_ms_comp = @id_ms_comp " +
                                             "  and isnull(pin, '0000') = @pin "
                End If
                ConexionBD.Open()
                ._txtIdEmplRecibe.Text = SCMValores.ExecuteScalar()
                ConexionBD.Close()
            Else
                .btnGuardar.Enabled = False
            End If
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
                Dim valor As Integer = 0
                While Val(._txtBan.Text) = 0
                    'Actualizar datos de la Comprobación
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "Declare @valorR as int;  Sp_D_ms_comp31 @id_ms_comp ,  @id_ms_instancia , @id_actividad , @id_usr,  @valorR output; Select @valorR  "
                    'SCMValores.CommandText = "update ms_comp set id_usr_efectivo = @id_usr_efectivo, fecha_efectivo = @fecha_efectivo, status = 'ZP' where id_ms_comp = @id_ms_comp "
                    'SCMValores.Parameters.AddWithValue("@id_usr_efectivo", Val(._txtIdUsuario.Text))
                    'SCMValores.Parameters.AddWithValue("@fecha_efectivo", fecha)
                    SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 30)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    valor = SCMValores.ExecuteScalar()
                    ConexionBD.Close()

                    If valor = 0 Then
                        Server.Transfer("Menu.aspx")
                    End If


                    ._txtBan.Text = 1

                    'Actualizar Anticipos
                    Dim SCMValoresAnt As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValoresAnt.Connection = ConexionBD
                    SCMValoresAnt.Parameters.Clear()
                    SCMValoresAnt.CommandText = "update ms_anticipo " +
                                                "set status = case status when 'EECR' then 'EE' when 'TRCR' then 'TR' else status end " +
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
                    'SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                    '                         "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    'SCMValores.Parameters.AddWithValue("@id_actividad", 30)
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
                    Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                    Mensaje.Subject = "ProcAd - Comprobación No. " + .lblFolio.Text + " Cancelada"
                    Dim texto As String
                    texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                            "La comprobación número <b>" + .lblFolio.Text + "</b> fue cancelada. <br></span>"
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

                    .btnGuardar.Enabled = False
                    .btnRechaza.Enabled = False
                End While
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub
End Class