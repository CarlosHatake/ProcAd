Public Class _54
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 3
                    'Session("idMsInst") = 29623

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtIdMsInst.Text = Session("idMsInst")
                        ._txtBan.Text = 0

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim sdaSol As New SqlDataAdapter
                        Dim dsSol As New DataSet
                        Dim query As String
                        query = "select id_ms_factura " + _
                                "     , empleado " + _
                                "     , empresa " + _
                                "     , fecha_solicita " + _
                                "     , isnull(ms_factura.tipo_servicio, ms_factura.servicio) as tipo_servicio " + _
                                "     , validador " + _
                                "     , isnull(cg_proveedor.rfc, '') as rfc " + _
                                "     , isnull(cg_proveedor.nombre, '') as proveedor " + _
                                "     , especificaciones " + _
                                "from ms_factura " + _
                                "  left join ms_instancia on ms_factura.id_ms_factura = ms_instancia.id_ms_sol and ms_instancia.tipo = 'F' " + _
                                "  left join bd_SiSAC.dbo.cg_proveedor on ms_factura.proveedor_selec = cg_proveedor.id_proveedor " + _
                                "where id_ms_instancia = @idMsInst "
                        sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@idMsInst", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()
                        .lblFolio.Text = dsSol.Tables(0).Rows(0).Item("id_ms_factura").ToString()
                        .lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("empleado").ToString()
                        .lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                        .lblFechaSol.Text = dsSol.Tables(0).Rows(0).Item("fecha_solicita").ToString()
                        .lblTipoServicio.Text = dsSol.Tables(0).Rows(0).Item("tipo_servicio").ToString()
                        .lblValidador.Text = dsSol.Tables(0).Rows(0).Item("validador").ToString()
                        If dsSol.Tables(0).Rows(0).Item("proveedor").ToString() = "" Then
                            'No se ha establecido Proveedor
                            .lbl_Proveedor.Visible = False
                            .lblProveedor.Visible = False
                        Else
                            'Se seleccionó Proveedor
                            .lbl_Proveedor.Visible = True
                            .lblProveedor.Visible = True
                            ._txtRFC.Text = dsSol.Tables(0).Rows(0).Item("rfc").ToString()
                            .lblProveedor.Text = dsSol.Tables(0).Rows(0).Item("proveedor").ToString()
                        End If
                        .txtEspecificaciones.Text = dsSol.Tables(0).Rows(0).Item("especificaciones").ToString()
                        sdaSol.Dispose()
                        dsSol.Dispose()

                        'Bases
                        Dim sdaBase As New SqlDataAdapter
                        Dim dsBase As New DataSet
                        sdaBase.SelectCommand = New SqlCommand("select id_base " + _
                                                               "     , base " + _
                                                               "from cg_base " + _
                                                               "  left join bd_Empleado.dbo.cg_empresa on cg_base.id_empresa = cg_empresa.id_empresa " + _
                                                               "where cg_empresa.status = 'A' " + _
                                                               "  and cg_base.status = 'A' " + _
                                                               "  and cg_empresa.nombre = @empresa " + _
                                                               "order by base ", ConexionBD)
                        sdaBase.SelectCommand.Parameters.AddWithValue("@empresa", .lblEmpresa.Text)
                        .ddlBase.DataSource = dsBase
                        .ddlBase.DataTextField = "base"
                        .ddlBase.DataValueField = "id_base"
                        ConexionBD.Open()
                        sdaBase.Fill(dsBase)
                        .ddlBase.DataBind()
                        ConexionBD.Close()
                        sdaBase.Dispose()
                        dsBase.Dispose()
                        .ddlBase.SelectedIndex = -1


                        'Autorizadores
                        Dim sdaAut As New SqlDataAdapter
                        Dim dsAut As New DataSet
                        .ddlAutorizador.DataSource = dsAut
                        sdaAut.SelectCommand = New SqlCommand("select cg_usuario.id_usuario, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " + _
                                                              "from dt_autorizador " + _
                                                              "  left join cg_usuario on dt_autorizador.id_autorizador = cg_usuario.id_usuario " + _
                                                              "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                              "where dt_autorizador.id_usuario = @idUsuario " + _
                                                              "  and cg_usuario.status = 'A' " + _
                                                              "order by aut_dir, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno ", ConexionBD)
                        sdaAut.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        .ddlAutorizador.DataTextField = "nombre_empleado"
                        .ddlAutorizador.DataValueField = "id_usuario"
                        ConexionBD.Open()
                        sdaAut.Fill(dsAut)
                        .ddlAutorizador.DataBind()
                        ConexionBD.Close()
                        sdaAut.Dispose()
                        dsAut.Dispose()
                        .ddlAutorizador.SelectedIndex = -1

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

#Region "Enviar a Aprobación"

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""

                If .wdpFechaIni.Text = "" Or .wdpFechaFin.Text = "" Then
                    .litError.Text = "Favor de complementar el Periodo del Contrato"
                Else
                    If .wceMontoXPeriodo.Text = "" Or .wceMontoContrato.Text = "" Then
                        .litError.Text = "Favor de complementar los Montos"
                    Else
                        If .txtLugar.Text.Trim = "" Then
                            .litError.Text = "Favor de complementar el Lugar de Ejecución"
                        Else
                            If .wdpFechaFin.Date <= .wdpFechaIni.Date Then
                                .litError.Text = "Periodo inválido, favor de validarlo"
                            Else
                                If .wceMontoContrato.Value < .wceMontoXPeriodo.Value Then
                                    .litError.Text = "Montos inválidos, favor de validarlos"
                                Else
                                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                                    SCMValores.Connection = ConexionBD

                                    'Validar que no exista registro previo
                                    Dim contMsContrato As Integer
                                    SCMValores.CommandText = ""
                                    SCMValores.Parameters.Clear()
                                    SCMValores.CommandText = "select count(*) " + _
                                                             "from ms_contrato " + _
                                                             "where id_ms_factura = @id_ms_factura "
                                    SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                                    ConexionBD.Open()
                                    contMsContrato = SCMValores.ExecuteScalar()
                                    ConexionBD.Close()

                                    If contMsContrato > 0 Then
                                        .litError.Text = "Existe un registro previo, favor de validarlo"
                                    Else
                                        Dim fecha As DateTime = Date.Now
                                        While Val(._txtBan.Text) = 0
                                            'Insertar Solicitud de Contrato
                                            SCMValores.CommandText = ""
                                            SCMValores.Parameters.Clear()
                                            SCMValores.CommandText = "insert into ms_contrato ( id_ms_factura,  id_usr_complementa,  fecha_complementa,  id_usr_autoriza,  fecha_autoriza,  empresa,  base,  empleado,  autorizador,  tipo_servicio,  periodicidad,  rfc,  proveedor,  monto_periodo,  monto_contrato,  lugar_ejecucion,  descrip_servicio,  fecha_servicio_ini,  fecha_servicio_fin) " +
                                                                     " 			       values (@id_ms_factura, @id_usr_complementa, @fecha_complementa, @id_usr_autoriza, @fecha_autoriza, @empresa, @base, @empleado, @autorizador, @tipo_servicio, @periodicidad, @rfc, @proveedor, @monto_periodo, @monto_contrato, @lugar_ejecucion, @descrip_servicio, @fecha_servicio_ini, @fecha_servicio_fin)"
                                            SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                                            SCMValores.Parameters.AddWithValue("@id_usr_complementa", Val(._txtIdUsuario.Text))
                                            SCMValores.Parameters.AddWithValue("@fecha_complementa", fecha)
                                            SCMValores.Parameters.AddWithValue("@id_usr_autoriza", .ddlAutorizador.SelectedValue)
                                            ' * * * Se omite la autorización 06Nov22 * * * Inicio
                                            SCMValores.Parameters.AddWithValue("@fecha_autoriza", fecha)
                                            ' * * * Se omite la autorización 06Nov22 * * * Fin
                                            SCMValores.Parameters.AddWithValue("@empresa", .lblEmpresa.Text)
                                            SCMValores.Parameters.AddWithValue("@base", .ddlBase.SelectedItem.Text)
                                            SCMValores.Parameters.AddWithValue("@empleado", .lblSolicitante.Text)
                                            SCMValores.Parameters.AddWithValue("@autorizador", .ddlAutorizador.SelectedItem.Text)
                                            SCMValores.Parameters.AddWithValue("@tipo_servicio", .lblTipoServicio.Text)
                                            SCMValores.Parameters.AddWithValue("@periodicidad", .ddlPeriodicidad.SelectedValue)
                                            SCMValores.Parameters.AddWithValue("@rfc", ._txtRFC.Text)
                                            SCMValores.Parameters.AddWithValue("@proveedor", .lblProveedor.Text)
                                            SCMValores.Parameters.AddWithValue("@monto_periodo", .wceMontoXPeriodo.Value)
                                            SCMValores.Parameters.AddWithValue("@monto_contrato", .wceMontoContrato.Value)
                                            SCMValores.Parameters.AddWithValue("@lugar_ejecucion", .txtLugar.Text.Trim)
                                            SCMValores.Parameters.AddWithValue("@descrip_servicio", .txtEspecificaciones.Text)
                                            SCMValores.Parameters.AddWithValue("@fecha_servicio_ini", .wdpFechaIni.Date)
                                            SCMValores.Parameters.AddWithValue("@fecha_servicio_fin", .wdpFechaFin.Date)
                                            ConexionBD.Open()
                                            SCMValores.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            ._txtBan.Text = 1

                                            'Actualizar Instancia
                                            SCMValores.Parameters.Clear()
                                            SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                                            SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                                            SCMValores.Parameters.AddWithValue("@id_actividad", 57) '55)
                                            ConexionBD.Open()
                                            SCMValores.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            'Registrar en Histórico
                                            SCMValores.Parameters.Clear()
                                            SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                                                     "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                                            SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                                            SCMValores.Parameters.AddWithValue("@id_actividad", 57) '55)
                                            SCMValores.Parameters.AddWithValue("@fecha", fecha)
                                            SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                                            ConexionBD.Open()
                                            SCMValores.ExecuteNonQuery()
                                            ConexionBD.Close()

                                            ' * * * Se omite la autorización 06Nov22 * * *
                                            ''Envío de Correo
                                            'Dim Mensaje As New System.Net.Mail.MailMessage()
                                            'Dim destinatario As String = ""
                                            'SCMValores.CommandText = "select cgEmpl.correo " + _
                                            '                         "from cg_usuario " + _
                                            '                         "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                            '                         "where cg_usuario.id_usuario = @idAut "
                                            'SCMValores.Parameters.AddWithValue("@idAut", .ddlAutorizador.SelectedValue)
                                            ''Obtener el Correo del Autorizador
                                            'ConexionBD.Open()
                                            'destinatario = SCMValores.ExecuteScalar()
                                            'ConexionBD.Close()

                                            'Mensaje.[To].Add(destinatario)
                                            'Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                                            'Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                                            'Dim texto As String
                                            'Mensaje.Subject = "ProcAd - Solicitud de Contrato " + .lblFolio.Text + " por Autorizar"
                                            'texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                                            '        "Se ingresó la solicitud de contrato correspondiente a la factura número <b>" + .lblFolio.Text + _
                                            '        "</b> por parte de <b>" + .lblSolicitante.Text + _
                                            '        "</b><br><br>Favor de determinar si procede </span>"
                                            'Mensaje.Body = texto
                                            'Mensaje.IsBodyHtml = True
                                            'Mensaje.Priority = MailPriority.Normal

                                            'Dim Servidor As New SmtpClient()
                                            'Servidor.Host = "10.10.10.30"
                                            'Servidor.Port = 587
                                            'Servidor.EnableSsl = False
                                            'Servidor.UseDefaultCredentials = False
                                            'Servidor.Credentials = New System.Net.NetworkCredential("nprocad", "mc8HLB8lPe78")

                                            'Try
                                            '    Servidor.Send(Mensaje)
                                            'Catch ex As System.Net.Mail.SmtpException
                                            '    .litError.Text = ex.ToString
                                            'End Try

                                            .pnlInicio.Enabled = False

                                            Session("id_actividadM") = 54
                                            Session("TipoM") = "F"
                                            Server.Transfer("Menu.aspx")
                                        End While
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class