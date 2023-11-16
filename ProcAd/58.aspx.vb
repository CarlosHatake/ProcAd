Public Class _58
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
                                "     , ms_contrato.rfc " + _
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
                        ._txtRFC.Text = dsSol.Tables(0).Rows(0).Item("rfc").ToString()
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
                        llenarCuentas()

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

#End Region

#Region "Registrar"

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""

                If .txtNoContratoNAV.Text.Trim = "" Or .txtGarantias.Text.Trim = "" Or .txtGarantiaCFP.Text.Trim = "" Then
                    .litError.Text = "Información Insuficiente, favor de validarlo"
                Else
                    'Validar Contrato VS Base de Datos NAV
                    Dim ConexionBDNAV As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBDNAV.ConnectionString = accessDB.conBD("NAV")
                    Dim SCMValoresNAV As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValoresNAV.Connection = ConexionBDNAV
                    Dim contContrato As Integer
                    SCMValoresNAV.CommandText = ""
                    SCMValoresNAV.Parameters.Clear()
                    SCMValoresNAV.CommandText = "select count(*) " + _
                                                "from [" + .lblEmpresa.Text + "$Contrato Compra] cgContrato " + _
                                                "  left join [" + .lblEmpresa.Text + "$Vendor] cgProveedor on cgContrato.Proveedor = cgProveedor.No_ " + _
                                                "where cgContrato.[Fecha Fin] > GETDATE() " + _
                                                "  and cgContrato.Aprobado = 1 " + _
                                                "  and cgContrato.Contrato = @contrato " + _
                                                "  and cgProveedor.[VAT Registration No_] = @rfc "
                    SCMValoresNAV.Parameters.AddWithValue("@rfc", ._txtRFC.Text)
                    SCMValoresNAV.Parameters.AddWithValue("@contrato", .txtNoContratoNAV.Text)
                    ConexionBDNAV.Open()
                    contContrato = SCMValoresNAV.ExecuteScalar
                    ConexionBDNAV.Close()

                    If contContrato = 0 Then
                        .litError.Text = "Número de Contrato Inválido, favor de verificarlo"
                    Else
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        Dim fecha As DateTime = Date.Now
                        While Val(._txtBan.Text) = 0
                            'Actualizar datos del Contrato
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "update ms_contrato set id_usr_registra = @id_usr_registra, fecha_registra = @fecha_registra, no_contrato_NAV = @no_contrato_NAV, garantias_serv = @garantias_serv, gar_cond_form_pago = @gar_cond_form_pago where id_ms_contrato = @id_ms_contrato "
                            SCMValores.Parameters.AddWithValue("@id_usr_registra", Val(._txtIdUsuario.Text))
                            SCMValores.Parameters.AddWithValue("@fecha_registra", fecha)
                            SCMValores.Parameters.AddWithValue("@no_contrato_NAV", .txtNoContratoNAV.Text)
                            SCMValores.Parameters.AddWithValue("@garantias_serv", .txtGarantias.Text)
                            SCMValores.Parameters.AddWithValue("@gar_cond_form_pago", .txtGarantiaCFP.Text)
                            SCMValores.Parameters.AddWithValue("@id_ms_contrato", Val(._txtMsContrato.Text))
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            ._txtBan.Text = 1

                            'Actualizar Instancia
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                            SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                            SCMValores.Parameters.AddWithValue("@id_actividad", 49)
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            'Registrar en Histórico
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                                     "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                            SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                            SCMValores.Parameters.AddWithValue("@id_actividad", 49)
                            SCMValores.Parameters.AddWithValue("@fecha", fecha)
                            SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            'Envío de Correo
                            Dim Mensaje As New System.Net.Mail.MailMessage()
                            Dim destinatario As String = ""
                            'Obtener el Correo del Solicitante
                            SCMValores.CommandText = "select cgEmpl.correo " + _
                                                     "from ms_factura " + _
                                                     "  left join cg_usuario on ms_factura.id_usr_solicita = cg_usuario.id_usuario " + _
                                                     "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                     "where id_ms_factura = @id_ms_factura "
                            SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                            ConexionBD.Open()
                            destinatario = SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            Mensaje.[To].Add(destinatario)
                            Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                            Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                            Mensaje.Subject = "ProcAd - Solicitud de Factura No. " + .lblFolio.Text + " Autorizada"
                            Dim texto As String
                            texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                                    "Se autorizó la solicitud número <b>" + .lblFolio.Text + _
                                    "</b><br><br>Favor de Ingresar la Factura Correspondiente </span>"
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
                            .btnAceptar.Visible = False
                        End While
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class