Public Class _82
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 5
                    'Session("idMsInst") = 41111

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtIdMsInst.Text = Session("idMsInst")
                        ._txtBan.Text = 0

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        'Datos de las Evaluaciones del Área
                        Dim sdaEvaluacion As New SqlDataAdapter
                        Dim dsEvaluacion As New DataSet
                        sdaEvaluacion.SelectCommand = New SqlCommand("select id_ms_evaluacionA " + _
                                                                     "     , ms_evaluacionA.area " + _
                                                                     "     , ms_evaluacionA.id_dt_area " + _
                                                                     "     , ms_evaluacionA.id_usr_evalua " + _
                                                                     "     , ms_evaluacionA.lider " + _
                                                                     "     , ms_evaluacionA.puesto_lider " + _
                                                                     "     , case ms_evaluacionA.mes_eval " + _
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
                                                                     "       end + ' ' + cast(ms_evaluacionA.año_eval as varchar(4)) as mes_eval " + _
                                                                     "     , id_direccion " + _
                                                                     "from ms_instancia " + _
                                                                     "  left join ms_evaluacionA on ms_instancia.id_ms_sol = ms_evaluaciona.id_ms_evaluacionA and ms_instancia.tipo = 'EvalA' " + _
                                                                     "  left join dt_area on ms_evaluacionA.id_dt_area = dt_area.id_dt_area " + _
                                                                     "where ms_instancia.id_ms_instancia = @idMsInst ", ConexionBD)
                        sdaEvaluacion.SelectCommand.Parameters.AddWithValue("@idMsInst", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaEvaluacion.Fill(dsEvaluacion)
                        ConexionBD.Close()
                        ._txtIdMsEvaluacionA.Text = dsEvaluacion.Tables(0).Rows(0).Item("id_ms_evaluacionA").ToString()
                        ._txtIdDtArea.Text = dsEvaluacion.Tables(0).Rows(0).Item("id_dt_area").ToString()
                        ._txtIdUsuarioE.Text = dsEvaluacion.Tables(0).Rows(0).Item("id_usr_evalua").ToString()
                        ._txtIdDireccion.Text = dsEvaluacion.Tables(0).Rows(0).Item("id_direccion").ToString()
                        .lblArea.Text = dsEvaluacion.Tables(0).Rows(0).Item("area").ToString()
                        .lblLider.Text = dsEvaluacion.Tables(0).Rows(0).Item("lider").ToString()
                        .lblPuestoLider.Text = dsEvaluacion.Tables(0).Rows(0).Item("puesto_lider").ToString()
                        .lblMesEval.Text = dsEvaluacion.Tables(0).Rows(0).Item("mes_eval").ToString()
                        '.txtComentarioL.Text = dsEvaluacion.Tables(0).Rows(0).Item("com_registro").ToString()
                        '.txtComentarioD.Text = dsEvaluacion.Tables(0).Rows(0).Item("com_director").ToString()
                        sdaEvaluacion.Dispose()
                        dsEvaluacion.Dispose()

                        llenarEvaluaciones()

                        'Evidencias
                        Dim sdaEvidencias As New SqlDataAdapter
                        Dim dsEvidencias As New DataSet
                        .gvEvidencias.DataSource = dsEvidencias
                        sdaEvidencias.SelectCommand = New SqlCommand("select evidencia " + _
                                                                     "     , 'http://148.223.153.43/ProcAd - Evidencias/' + cast(id_dt_evaluacion_evid as varchar(20)) + 'EvalE-' + evidencia as path " + _
                                                                     "from dt_evaluacion_evid " + _
                                                                     "where id_ms_evaluacionA = @id_ms_evaluacionA ", ConexionBD)
                        sdaEvidencias.SelectCommand.Parameters.AddWithValue("@id_ms_evaluacionA", Val(._txtIdMsEvaluacionA.Text))
                        ConexionBD.Open()
                        sdaEvidencias.Fill(dsEvidencias)
                        .gvEvidencias.DataBind()
                        ConexionBD.Close()
                        sdaEvidencias.Dispose()
                        dsEvidencias.Dispose()

                        If .gvEvidencias.Rows.Count > 0 Then
                            .lbl_Evidencias.Visible = True
                            .gvEvidencias.Visible = True
                        Else
                            .lbl_Evidencias.Visible = False
                            .gvEvidencias.Visible = False
                        End If
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

    Public Sub llenarEvaluaciones()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                'Evaluaciones Autorizadas
                Dim sdaEvaluaciones As New SqlDataAdapter
                Dim dsEvaluaciones As New DataSet
                .gvEvaluaciones.Columns(0).Visible = True
                .gvEvaluaciones.DataSource = dsEvaluaciones
                sdaEvaluaciones.SelectCommand = New SqlCommand("select id_ms_evaluacion " + _
                                                               "     , no_empleado " + _
                                                               "     , nombre + ' ' + ap_paterno + ' ' + ap_materno as colaborador " + _
                                                               "     , empresa " + _
                                                               "     , centro_costo " + _
                                                               "     , puesto " + _
                                                               "     , base " + _
                                                               "     , porcent_cumpl " + _
                                                               "     , case cobra_bono_asist when 'S' then 'Sí' when 'N' then 'No' else '-' end as cobra_bono_asist " + _
                                                               "     , case cobra_bono_cumpl_UN when 'S' then 'Sí' when 'N' then 'No' else '-' end as cobra_bono_cumpl_UN " + _
                                                               "     , case invalida when 'S' then 'Sí' when 'N' then 'No' else '-' end as invalida " + _
                                                               "from ms_evaluacionA " + _
                                                               "  left join ms_evaluacion on ms_evaluacionA.id_ms_evaluacionA = ms_evaluacion.id_ms_evaluacionA " + _
                                                               "where ms_evaluacionA.id_ms_evaluacionA = @id_ms_evaluacionA " + _
                                                               "order by colaborador ", ConexionBD)
                sdaEvaluaciones.SelectCommand.Parameters.AddWithValue("@id_ms_evaluacionA", Val(._txtIdMsEvaluacionA.Text))
                ConexionBD.Open()
                sdaEvaluaciones.Fill(dsEvaluaciones)
                .gvEvaluaciones.DataBind()
                ConexionBD.Close()
                sdaEvaluaciones.Dispose()
                dsEvaluaciones.Dispose()
                .gvEvaluaciones.Columns(0).Visible = False
                .gvEvaluaciones.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Autorizar / Rechazar"

    Protected Sub btnAutorizar_Click(sender As Object, e As EventArgs) Handles btnAutorizar.Click
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim fecha As DateTime = Date.Now
                While Val(._txtBan.Text) = 0
                    'Actualizar Estatus de las Evaluaciones Individuales
                    SCMValores.CommandText = "update ms_evaluacion  " + _
                                             "  set fecha_director = @fecha_director, status = 'PA' " + _
                                             "where id_ms_evaluacionA = @id_ms_evaluacionA "
                    SCMValores.Parameters.AddWithValue("@fecha_director", fecha)
                    SCMValores.Parameters.AddWithValue("@id_ms_evaluacionA", Val(._txtIdMsEvaluacionA.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Actualizar datos de la Evaluación por Área
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_evaluacionA set fecha_director = @fecha_director, status = 'PA' where id_ms_evaluacionA = @id_ms_evaluacionA "
                    SCMValores.Parameters.AddWithValue("@fecha_director", fecha)
                    SCMValores.Parameters.AddWithValue("@id_ms_evaluacionA", Val(._txtIdMsEvaluacionA.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Actualizar Instancia
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 73)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Registrar en Histórico
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                             "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 73)
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    ._txtBan.Text = 1

                    .btnAutorizar.Enabled = False

                    Session("id_actividadM") = 82
                    Session("TipoM") = "Eval"
                    Server.Transfer("Menu.aspx")
                End While
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
                While Val(._txtBan.Text) = 0
                    'Actualizar Estatus de las Evaluaciones Individuales
                    SCMValores.CommandText = "update ms_evaluacion  " + _
                                             "  set status = 'PCA' " + _
                                             "where id_ms_evaluacionA = @id_ms_evaluacionA "
                    SCMValores.Parameters.AddWithValue("@fecha_director", fecha)
                    SCMValores.Parameters.AddWithValue("@id_ms_evaluacionA", Val(._txtIdMsEvaluacionA.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Actualizar datos de la Evaluación por Área
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_evaluacionA set status = 'PCA' where id_ms_evaluacionA = @id_ms_evaluacionA "
                    SCMValores.Parameters.AddWithValue("@fecha_director", fecha)
                    SCMValores.Parameters.AddWithValue("@id_ms_evaluacionA", Val(._txtIdMsEvaluacionA.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Actualizar Instancia
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 81)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Registrar en Histórico
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                             "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 81)
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    ._txtBan.Text = 1

                    .btnAutorizar.Enabled = False

                    Session("id_actividadM") = 82
                    Session("TipoM") = "Eval"
                    Server.Transfer("Menu.aspx")
                End While
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class