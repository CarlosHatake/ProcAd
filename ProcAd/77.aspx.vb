Public Class _77
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 297
                    'Session("idMsInst") = 43427

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtIdMsInst.Text = Session("idMsInst")
                        ._txtBan.Text = 0

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
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
                                                                       "     , porcent_bono_cumpl_UN " + _
                                                                       "from ms_evaluacion " + _
                                                                       "where mes_eval = @mes " + _
                                                                       "  and año_eval = @año " + _
                                                                       "  and status in ('PPE') " + _
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

#Region "Procesar"

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
                        'Actualizar ID Usr y Fecha de Nómina 2ra Quincena de las Evaluaciones Individuales
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_evaluacion  " + _
                                                 "  set id_usr_nomina_2da = @id_usr_nomina_2da, fecha_nomina_2da = @fecha_nomina_2da, status = 'EP' " + _
                                                 "where id_ms_evaluacion = @id_ms_evaluacion "
                        SCMValores.Parameters.AddWithValue("@id_usr_nomina_2da", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@fecha_nomina_2da", fecha)
                        SCMValores.Parameters.AddWithValue("@id_ms_evaluacion", Val(.gvEvaluaciones.Rows(i).Cells(0).Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()
                    Next

                    .pnlInicio.Enabled = False

                    Server.Transfer("Menu.aspx")
                End While
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

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