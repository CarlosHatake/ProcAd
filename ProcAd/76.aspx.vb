Public Class _76
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 55

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtBan.Text = 0

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        'Datos de la Evaluación
                        Dim sdaEvaluacion As New SqlDataAdapter
                        Dim dsEvaluacion As New DataSet
                        sdaEvaluacion.SelectCommand = New SqlCommand("select month((select cast(valor as datetime) " + _
                                                                     "              from cg_parametros " + _
                                                                     "              where parametro = 'mes_eval')) as mes " + _
                                                                     "     , year((select cast(valor as datetime) " + _
                                                                     "             from cg_parametros " + _
                                                                     "             where parametro = 'mes_eval')) as año " + _
                                                                     "     , case month((select cast(valor as datetime) " + _
                                                                     "                   from cg_parametros " + _
                                                                     "                   where parametro = 'mes_eval')) " + _
                                                                     "         when 1 then 'Enero' " + _
                                                                     "         when 2 then 'Febrero' " + _
                                                                     "         when 3 then 'Marzo' " + _
                                                                     "         when 4 then 'Abril' " + _
                                                                     "         when 5 then 'Mayo' " + _
                                                                     "         when 6 then 'Junio' " + _
                                                                     "         when 7 then 'Julio' " + _
                                                                     "         when 8 then 'Agosto' " + _
                                                                     "         when 9 then 'Septiembre' " + _
                                                                     "         when 10 then 'Octubre' " + _
                                                                     "         when 11 then 'Noviembre' " + _
                                                                     "         when 12 then 'Diciembre' " + _
                                                                     "         else '-' " + _
                                                                     "       end + ' ' + cast(year((select cast(valor as datetime) " + _
                                                                     "                              from cg_parametros " + _
                                                                     "                              where parametro = 'mes_eval')) as varchar(4)) as mes_eval ", ConexionBD)
                        ConexionBD.Open()
                        sdaEvaluacion.Fill(dsEvaluacion)
                        ConexionBD.Close()
                        ._txtMes.Text = dsEvaluacion.Tables(0).Rows(0).Item("mes").ToString()
                        ._txtAño.Text = dsEvaluacion.Tables(0).Rows(0).Item("año").ToString()
                        .lblMesEval.Text = dsEvaluacion.Tables(0).Rows(0).Item("mes_eval").ToString()
                        sdaEvaluacion.Dispose()
                        dsEvaluacion.Dispose()

                        'Evaluaciones Individuales
                        Dim sdaEvaluaciones As New SqlDataAdapter
                        Dim dsEvaluaciones As New DataSet
                        .gvEvaluaciones.DataSource = dsEvaluaciones
                        'Habilitar columnas para actualización
                        .gvEvaluaciones.Columns(0).Visible = True
                        .gvEvaluaciones.Columns(1).Visible = True
                        sdaEvaluaciones.SelectCommand = New SqlCommand("select id_ms_evaluacion " + _
                                                                       "     , no_empleado " + _
                                                                       "     , nombre + ' ' + ap_paterno + ' ' + ap_materno as colaborador " + _
                                                                       "     , ms_evaluacion.lider " + _
                                                                       "     , ms_evaluacion.empresa " + _
                                                                       "     , ms_evaluacion.area " + _
                                                                       "     , ms_evaluacion.unidad_neg " + _
                                                                       "     , puesto " + _
                                                                       "     , ms_evaluacion.puesto_lider " + _
                                                                       "     , case ms_evaluacion.mes_eval " + _
                                                                       "         when 1 then 'Enero' " + _
                                                                       "         when 2 then 'Febrero' " + _
                                                                       "         when 3 then 'Marzo' " + _
                                                                       "         when 4 then 'Abril' " + _
                                                                       "         when 5 then 'Mayo' " + _
                                                                       "         when 6 then 'Junio' " + _
                                                                       "         when 7 then 'Julio' " + _
                                                                       "         when 8 then 'Agosto' " + _
                                                                       "         when 9 then 'Septiembre' " + _
                                                                       "         when 10 then 'Octubre' " + _
                                                                       "         when 11 then 'Noviembre' " + _
                                                                       "         when 12 then 'Diciembre' " + _
                                                                       "         else '-' " + _
                                                                       "       end + ' ' + cast(ms_evaluacion.año_eval as varchar(4)) as mes_eval " + _
                                                                       "     , porcent_cumpl " + _
                                                                       "     , case cobra_bono_cumpl_UN when 'S' then 'Sí' when 'N' then 'No' else '-' end as cobra_bono_cumpl_UN " + _
                                                                       "     , porcent_bono_cumpl_UN = (select case when msEvalTmp.cobra_bono_cumpl_UN = 'S' then " + _
                                                                       "                                              case when msEvalTmp.porcent_cumpl is null then .1 " + _
                                                                       "                                                   else case when cg_unidad_neg.calculado = 'N' then ms_cumpl_UN.porcent_bono " + _
                                                                       "                                                             else (select sum(porcent * porcent_bono) " + _
                                                                       "                                                                   from dt_unidad_neg " + _
                                                                       "                                                                     left join ms_cumpl_UN msCumplPorcent on dt_unidad_neg.id_unidad_neg_p = msCumplPorcent.id_unidad_neg and msCumplPorcent.año_eval = msEvalTmp.año_eval and msCumplPorcent.mes_eval = msEvalTmp.mes_eval " + _
                                                                       "                                                                   where dt_unidad_neg.id_unidad_neg = cg_unidad_neg.id_unidad_neg) " + _
                                                                       "                                                        end " + _
                                                                       "                                              end " + _
                                                                       "                                            else null " + _
                                                                       "                                       end as bono " + _
                                                                       "                                from ms_evaluacion msEvalTmp " + _
                                                                       "                                  left join cg_unidad_neg on msEvalTmp.unidad_neg = cg_unidad_neg.unidad_neg and cg_unidad_neg.status = 'A' " + _
                                                                       "                                  left join ms_cumpl_UN on msEvalTmp.unidad_neg = ms_cumpl_UN.unidad_neg and msEvalTmp.año_eval = ms_cumpl_UN.año_eval and msEvalTmp.mes_eval = ms_cumpl_UN.mes_eval " + _
                                                                       "                                where msEvalTmp.id_ms_evaluacion = ms_evaluacion.id_ms_evaluacion) " + _
                                                                       "from ms_evaluacion " + _
                                                                       "where mes_eval = @mes " + _
                                                                       "  and año_eval = @año " + _
                                                                       "  and status in ('PCE') " + _
                                                                       "order by ms_evaluacion.empresa, ms_evaluacion.direccion, centro_costo, colaborador ", ConexionBD)
                        sdaEvaluaciones.SelectCommand.Parameters.AddWithValue("@mes", Val(._txtMes.Text))
                        sdaEvaluaciones.SelectCommand.Parameters.AddWithValue("@año", Val(._txtAño.Text))
                        ConexionBD.Open()
                        sdaEvaluaciones.Fill(dsEvaluaciones)
                        .gvEvaluaciones.DataBind()
                        ConexionBD.Close()
                        sdaEvaluaciones.Dispose()
                        dsEvaluaciones.Dispose()
                        'Inhabilitar columnas para vista
                        .gvEvaluaciones.Columns(0).Visible = False
                        .gvEvaluaciones.Columns(1).Visible = False
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

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""

                While Val(._txtBan.Text) = 0
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    Dim fecha As DateTime = Date.Now

                    ._txtBan.Text = 1

                    'Obtener los id_ms_evaluacion correspondientes 
                    For i = 0 To .gvEvaluaciones.Rows.Count - 1
                        'Actualizar % de Bono de Cumplimiento UN, ID Usr y Fecha de las Evaluaciones Individuales
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_evaluacion  " + _
                                                 "  set porcent_bono_cumpl_UN = @porcent_bono_cumpl_UN, id_usr_concentra = @id_usr_concentra, fecha_concentra = @fecha_concentra, status = 'PPE' " + _
                                                 "where id_ms_evaluacion = @id_ms_evaluacion "
                        SCMValores.Parameters.AddWithValue("@id_usr_concentra", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@fecha_concentra", fecha)
                        If .gvEvaluaciones.Rows(i).Cells(11).Text = "No" Then
                            SCMValores.Parameters.AddWithValue("@porcent_bono_cumpl_UN", DBNull.Value)
                        Else
                            SCMValores.Parameters.AddWithValue("@porcent_bono_cumpl_UN", Val(.gvEvaluaciones.Rows(i).Cells(1).Text))
                        End If
                        SCMValores.Parameters.AddWithValue("@id_ms_evaluacion", Val(.gvEvaluaciones.Rows(i).Cells(0).Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()
                    Next

                    ' ''Envío de Correo
                    ''Dim Mensaje As New System.Net.Mail.MailMessage()
                    ''Dim destinatario As String = ""
                    ' ''Obtener el Correo del Colaborador
                    ''SCMValores.CommandText = "select valor " + _
                    ''                         "from cg_parametros " + _
                    ''                         "where parametro = 'mail_nomina_eval' "
                    ''SCMValores.Parameters.AddWithValue("@id_ms_recursos", Val(.lblFolio.Text))
                    ''ConexionBD.Open()
                    ''destinatario = SCMValores.ExecuteScalar()
                    ''ConexionBD.Close()

                    ''Mensaje.[To].Add(destinatario)
                    ''Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                    ''Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                    ''Mensaje.Subject = "ProcAd - Concentrado de Evaluaciones por Aplicar"
                    ''Dim texto As String
                    ''texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                    ''        "Se realizó el envío de evaluaciones correspondientes al mes <b>" + .lblMesEval.Text + _
                    ''        "</b>, favor de proceder con su aplicación. <br></span>"
                    ''Mensaje.Body = texto
                    ''Mensaje.IsBodyHtml = True
                    ''Mensaje.Priority = MailPriority.Normal

                    ''Dim Servidor As New SmtpClient()
                    ''Servidor.Host = "10.10.10.30"
                    ''Servidor.Port = 587
                    ''Servidor.EnableSsl = False
                    ''Servidor.UseDefaultCredentials = False
                    ''Servidor.Credentials = New System.Net.NetworkCredential("nprocad", "mc8HLB8lPe78")
                    ''Try
                    ''    Servidor.Send(Mensaje)
                    ''Catch ex As System.Net.Mail.SmtpException
                    ''    .litError.Text = ex.ToString
                    ''End Try

                    .pnlInicio.Enabled = False

                    Server.Transfer("Menu.aspx")
                End While
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#Region "Exportar"

    Protected Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        Dim tw As New System.IO.StringWriter
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        With Me
            Try
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment;filename=Evaluaciones.xls")
                Response.ContentType = "application/vnd.ms-excel"
                .gvEvaluaciones.Visible = True
                .gvEvaluaciones.RenderControl(hw)
                .gvEvaluaciones.Visible = False
                Response.Write(tw.ToString())
                Response.End()
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub

#End Region

End Class