Public Class _62
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
                        ConexionBD.Open()
                        sdaConcepto.Fill(dsConcepto)
                        .gvConceptos.DataBind()
                        ConexionBD.Close()
                        .gvConceptos.Columns(0).Visible = False
                        .gvConceptos.Columns(1).Visible = False
                        .gvConceptos.Columns(2).Visible = False
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

#Region "Autorizar / Rechazar"

    Protected Sub btnAutoriza_Click(sender As Object, e As EventArgs) Handles btnAutoriza.Click
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
                    Dim SCMValoresNoVal As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValoresNoVal.Connection = ConexionBD
                    'Autorizador
                    'Actualizar datos de la Solicitud
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "Declare @valorR as int;Execute  Sp_U_ms_comp62 @obs_autorizador, @id_ms_comp ,  @id_ms_instancia , @id_actividad , @id_usr,  @valorR output; Select @valorR  "
                    'SCMValores.CommandText = "update ms_comp set fecha_autoriza = @fecha_autoriza, obs_autorizador = @obs_autorizador, id_usr_valida = @id_usr_valida, fecha_valida = @fecha_autoriza, pago_efectivo = 'N', status = 'R' where id_ms_comp = @id_ms_comp "
                    'SCMValores.Parameters.AddWithValue("@id_usr_valida", Val(._txtIdUsuario.Text))
                    'SCMValores.Parameters.AddWithValue("@fecha_autoriza", fecha)
                    If .txtObsAut.Text.Trim = "" Then
                        SCMValores.Parameters.AddWithValue("@obs_autorizador", DBNull.Value)
                    Else
                        SCMValores.Parameters.AddWithValue("@obs_autorizador", .txtObsAut.Text.Trim)
                    End If
                    SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 63)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    valor = SCMValores.ExecuteScalar()
                    ConexionBD.Close()

                    If valor = 0 Then
                        Server.Transfer("Menu.aspx")
                    End If
                    SCMValores.Parameters.Clear()

                    ._txtBan.Text = 1

                    'Actualizar Anticipos
                    Dim SCMValoresAnt As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValoresAnt.Connection = ConexionBD
                    SCMValoresAnt.Parameters.Clear()
                    SCMValoresAnt.CommandText = "update ms_anticipo " +
                                                "set status = case status when 'EECP' then 'EECR' when 'TRCP' then 'TRCR' else status end " +
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
                                                   "set status = 'CR' " +
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

                    ''Actualizar Instancia
                    'SCMValores.Parameters.Clear()
                    'SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                    'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    'SCMValores.Parameters.AddWithValue("@id_actividad", 63)
                    'ConexionBD.Open()
                    'SCMValores.ExecuteNonQuery()
                    'ConexionBD.Close()

                    ''Registrar en Histórico
                    'SCMValores.Parameters.Clear()
                    'SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                    '                         "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    'SCMValores.Parameters.AddWithValue("@id_actividad", 63)
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

                    .btnAutoriza.Enabled = False
                    .btnRechaza.Enabled = False
                    .gvConceptos.Enabled = False

                    Session("id_actividadM") = 62
                    Session("TipoM") = "C"
                    Server.Transfer("Menu.aspx")
                End While
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
                Dim valor As Integer = 0
                Dim fecha As DateTime = Date.Now
                While Val(._txtBan.Text) = 0
                    'Actualizar datos de la Comprobación
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "Declare @valorR as int;EXECUTE  Sp_D_ms_comp62 @obs_autorizador, @id_ms_comp ,  @id_ms_instancia , @id_actividad , @id_usr,  @valorR output; Select @valorR  "
                    'SCMValores.CommandText = "update ms_comp set fecha_autoriza = @fecha_autoriza, obs_autorizador = @obs_autorizador, status = 'ZA' where id_ms_comp = @id_ms_comp "
                    'SCMValores.Parameters.AddWithValue("@fecha_autoriza", fecha)
                    If .txtObsAut.Text.Trim = "" Then
                        SCMValores.Parameters.AddWithValue("@obs_autorizador", DBNull.Value)
                    Else
                        SCMValores.Parameters.AddWithValue("@obs_autorizador", .txtObsAut.Text.Trim)
                    End If
                    SCMValores.Parameters.AddWithValue("@id_ms_comp", Val(.lblFolio.Text))
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 64)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    valor = SCMValores.ExecuteScalar()
                    ConexionBD.Close()

                    If valor = 0 Then
                        Server.Transfer("Menu.aspx")
                    End If
                    SCMValores.Parameters.Clear()
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

                    'Actualizar Cargas de Combustible con Tarjeta
                    Dim SCMCargaComb As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMCargaComb.Connection = ConexionBD
                    SCMCargaComb.Parameters.Clear()
                    SCMCargaComb.CommandText = "update dt_carga_comb_tar " +
                                               "  set status = 'ZA', id_usr_cancel = @id_usr_cancel, fecha_cancel = @fecha_cancel " +
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
                    'SCMValores.Parameters.AddWithValue("@id_actividad", 64)
                    'ConexionBD.Open()
                    'SCMValores.ExecuteNonQuery()
                    'ConexionBD.Close()

                    ''Registrar en Histórico
                    'SCMValores.Parameters.Clear()
                    'SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                    '                         "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    'SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    'SCMValores.Parameters.AddWithValue("@id_actividad", 64)
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
                    Mensaje.Subject = "ProcAd - Comprobación No. " + .lblFolio.Text + " Rechazada"
                    Dim texto As String
                    texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                            "La comprobación número <b>" + .lblFolio.Text + "</b> fue rechazada. <br></span>"
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
                    .btnRechaza.Enabled = False
                    .gvConceptos.Enabled = False

                    Session("id_actividadM") = 62
                    Session("TipoM") = "C"
                    Server.Transfer("Menu.aspx")
                End While
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class