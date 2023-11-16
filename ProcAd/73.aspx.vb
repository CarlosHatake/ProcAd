Public Class _73
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

                        .pnlIndicador.Visible = False
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

#Region "Tabla"

    Protected Sub gvEvaluaciones_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvEvaluaciones.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                'Datos de la Evaluación
                Dim sdaEvaluacion As New SqlDataAdapter
                Dim dsEvaluacion As New DataSet
                sdaEvaluacion.SelectCommand = New SqlCommand("select nombre + ' ' + ap_paterno + ' ' + ap_materno as colaborador " + _
                                                             "     , puesto " + _
                                                             "     , base " + _
                                                             "     , cobra_bono_asist " + _
                                                             "     , cobra_bono_cumpl_UN " + _
                                                             "     , case " + _
                                                             "         when (isnull(porcent_cumpl, 0) >= .66) " + _
                                                             "           or (select case when cast('01-' + cast(ms_evaluacion.mes_eval as varchar(2)) + '-' +  + cast(ms_evaluacion.año_eval as varchar(4)) as date) < dateadd(mm, 2, cast(cast(datepart(year, fecha_ingreso) as varchar(4)) + '/' + cast(datepart(month, fecha_ingreso) as varchar(2)) + '/01' as date)) then 1 else 0 end as banMeses " + _
                                                             "               from dt_empleado " + _
                                                             "               where dt_empleado.no_empleado = ms_evaluacion.no_empleado) = 1 " + _
                                                             "           then 'S' " + _
                                                             "         else 'N' " + _
                                                             "       end as cobra_bono_calculado " + _
                                                             "     , isnull(com_director, '') as obs_director " + _
                                                             "     , isnull(invalida, 'N') as invalida " + _
                                                             "from ms_evaluacion " + _
                                                             "where id_ms_evaluacion = @id_ms_evaluacion ", ConexionBD)
                sdaEvaluacion.SelectCommand.Parameters.AddWithValue("@id_ms_evaluacion", Val(.gvEvaluaciones.SelectedRow.Cells(0).Text))
                ConexionBD.Open()
                sdaEvaluacion.Fill(dsEvaluacion)
                ConexionBD.Close()
                .lblColaborador.Text = dsEvaluacion.Tables(0).Rows(0).Item("colaborador").ToString()
                .lblPuesto.Text = dsEvaluacion.Tables(0).Rows(0).Item("puesto").ToString()
                .lblBase.Text = dsEvaluacion.Tables(0).Rows(0).Item("base").ToString()
                .lblBono.Text = dsEvaluacion.Tables(0).Rows(0).Item("cobra_bono_calculado").ToString()
                .rblBonoAsist.SelectedValue = dsEvaluacion.Tables(0).Rows(0).Item("cobra_bono_asist").ToString()
                .rblBonoCumplUN.SelectedValue = dsEvaluacion.Tables(0).Rows(0).Item("cobra_bono_asist").ToString()
                .txtObs.Text = dsEvaluacion.Tables(0).Rows(0).Item("obs_director").ToString()
                If dsEvaluacion.Tables(0).Rows(0).Item("invalida").ToString() = "S" Then
                    .cbEvalInvalida.Checked = True
                Else
                    .cbEvalInvalida.Checked = False
                End If
                sdaEvaluacion.Dispose()
                dsEvaluacion.Dispose()

                'Indicadores
                Dim sdaIndicadores As New SqlDataAdapter
                Dim dsIndicadores As New DataSet
                .gvIndicadores.DataSource = dsIndicadores
                sdaIndicadores.SelectCommand = New SqlCommand("select indicador " + _
                                                              "     , (select case month(cast(valor as date)) " + _
                                                              "                 when 1 then 'Enero' " + _
                                                              "                 when 2 then 'Febrero' " + _
                                                              "                 when 3 then 'Marzo' " + _
                                                              "                 when 4 then 'Abril' " + _
                                                              "                 when 5 then 'Mayo' " + _
                                                              "                 when 6 then 'Junio' " + _
                                                              "                 when 7 then 'Julio' " + _
                                                              "                 when 8 then 'Agosto' " + _
                                                              "                 when 9 then 'Septiembre' " + _
                                                              "                 when 10 then 'Octubre' " + _
                                                              "                 when 11 then 'Noviembre' " + _
                                                              "                 when 12 then 'Diciembre' " + _
                                                              "                 else '-' " + _
                                                              "                end  " + _
                                                              "              + ' ' + cast(year(cast(valor as date)) as varchar(4)) as mes_eval " + _
                                                              "         from cg_parametros " + _
                                                              "         where parametro = 'mes_eval') as mes_eval " + _
                                                              "     , tipo_indicador " + _
                                                              "     , ponderacion " + _
                                                              "     , meta " + _
                                                              "     , real " + _
                                                              "     , cumpl_pond " + _
                                                              "     , fuente " + _
                                                              "from dt_evaluacion " + _
                                                              "where id_ms_evaluacion = @id_ms_evaluacion " + _
                                                              "order by indicador ", ConexionBD)
                sdaIndicadores.SelectCommand.Parameters.AddWithValue("@id_ms_evaluacion", Val(.gvEvaluaciones.SelectedRow.Cells(0).Text))
                ConexionBD.Open()
                sdaIndicadores.Fill(dsIndicadores)
                .gvIndicadores.DataBind()
                ConexionBD.Close()
                sdaIndicadores.Dispose()
                dsIndicadores.Dispose()

                .pnlIndicadores.Visible = False
                .pnlIndicador.Visible = True
                .btnAutorizar.Visible = False
                .btnRechazar.Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnAceptarI_Click(sender As Object, e As EventArgs) Handles btnAceptarI.Click
        With Me
            Try
                .litError.Text = ""

                If (.lblBono.Text <> .rblBonoAsist.SelectedValue And .txtObs.Text.Trim = "") Or (.lblBono.Text <> .rblBonoCumplUN.SelectedValue And .txtObs.Text.Trim = "") Or (.cbEvalInvalida.Checked = True And .txtObs.Text.Trim = "") Then
                    .litError.Text = "Favor de ingresar las observaciones correspondientes"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    'Actualizar Evaluación
                    SCMValores.CommandText = "update ms_evaluacion  " + _
                                             "  set cobra_bono_asist = @cobra_bono_asist, cobra_bono_cumpl_UN = @cobra_bono_cumpl_UN, com_director = @com_director, invalida = @invalida " + _
                                             "where id_ms_evaluacion = @id_ms_evaluacion "
                    SCMValores.Parameters.AddWithValue("@cobra_bono_asist", .rblBonoAsist.SelectedValue)
                    SCMValores.Parameters.AddWithValue("@cobra_bono_cumpl_UN", .rblBonoCumplUN.SelectedValue)
                    If .txtObs.Text.Trim = "" Then
                        SCMValores.Parameters.AddWithValue("@com_director", DBNull.Value)
                    Else
                        SCMValores.Parameters.AddWithValue("@com_director", .txtObs.Text.Trim)
                    End If
                    If .cbEvalInvalida.Checked = True Then
                        SCMValores.Parameters.AddWithValue("@invalida", "S")
                    Else
                        SCMValores.Parameters.AddWithValue("@invalida", "N")
                    End If
                    SCMValores.Parameters.AddWithValue("@id_ms_evaluacion", Val(.gvEvaluaciones.SelectedRow.Cells(0).Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    llenarEvaluaciones()

                    .pnlIndicadores.Visible = True
                    .pnlIndicador.Visible = False
                    .btnAutorizar.Visible = True
                    .btnRechazar.Visible = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Autorizar"

    Protected Sub btnAutorizar_Click(sender As Object, e As EventArgs) Handles btnAutorizar.Click
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim contInv As Integer

                SCMValores.CommandText = "select count(*) " + _
                                         "from ms_evaluacion " + _
                                         "where id_dt_area = @id_dt_area " + _
                                         "  and id_usr_registro = @id_usr_registro " + _
                                         "  and invalida = 'S' " + _
                                         "  and status = 'PA' " + _
                                         "  and mes_eval = month((select cast(valor as datetime) " + _
                                         "                        from cg_parametros " + _
                                         "                        where parametro = 'mes_eval')) " + _
                                         "  and año_eval = year((select cast(valor as datetime) " + _
                                         "                       from cg_parametros " + _
                                         "                       where parametro = 'mes_eval')) "
                SCMValores.Parameters.AddWithValue("@id_dt_area", Val(._txtIdDtArea.Text))
                SCMValores.Parameters.AddWithValue("@id_usr_registro", Val(._txtIdUsuarioE.Text))
                ConexionBD.Open()
                contInv = SCMValores.ExecuteScalar
                ConexionBD.Close()

                If contInv > 0 Then
                    .litError.Text = "Existe al menos una evaluación marcada como inválida, favor de verificarlo"
                Else
                    Dim fecha As DateTime = Date.Now
                    While Val(._txtBan.Text) = 0
                        'Actualizar Estatus de las Evaluaciones Individuales
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_evaluacion  " + _
                                                 "  set fecha_director = @fecha_director, status = 'PCE' " + _
                                                 "where id_dt_area = @id_dt_area " + _
                                                 "  and id_usr_registro = @id_usr_registro " + _
                                                 "  and status = 'PA' " + _
                                                 "  and mes_eval = month((select cast(valor as datetime) " + _
                                                 "                        from cg_parametros " + _
                                                 "                        where parametro = 'mes_eval')) " + _
                                                 "  and año_eval = year((select cast(valor as datetime) " + _
                                                 "                       from cg_parametros " + _
                                                 "                       where parametro = 'mes_eval')) "
                        SCMValores.Parameters.AddWithValue("@fecha_director", fecha)
                        SCMValores.Parameters.AddWithValue("@id_dt_area", Val(._txtIdDtArea.Text))
                        SCMValores.Parameters.AddWithValue("@id_usr_registro", Val(._txtIdUsuarioE.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Actualizar datos de la Evaluación por Área
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_evaluacionA set fecha_director = @fecha_director, status = 'PCE' where id_ms_evaluacionA = @id_ms_evaluacionA "
                        SCMValores.Parameters.AddWithValue("@fecha_director", fecha)
                        SCMValores.Parameters.AddWithValue("@id_ms_evaluacionA", Val(._txtIdMsEvaluacionA.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Actualizar Instancia
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 74)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Registrar en Histórico
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                                 "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 74)
                        SCMValores.Parameters.AddWithValue("@fecha", fecha)
                        SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        ._txtBan.Text = 1

                        'Determinar si ya se autorizaron todas las evaluaciones de las áreas de la Dirección
                        Dim evalPend As Integer
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "select id_dt_empleado " + _
                                                 "     , dt_empleado.nombre as empleado " + _
                                                 "from dt_empleado " + _
                                                 "  left join dt_area on dt_empleado.id_dt_area = dt_area.id_dt_area " + _
                                                 "  left join ms_evaluacion on dt_empleado.no_empleado = ms_evaluacion.no_empleado " + _
                                                 "                         and mes_eval = month((select cast(valor as datetime) " + _
                                                 "                                               from cg_parametros " + _
                                                 "                                               where parametro = 'mes_eval')) " + _
                                                 "                         and año_eval = year((select cast(valor as datetime) " + _
                                                 "                                              from cg_parametros " + _
                                                 "                                              where parametro = 'mes_eval')) " + _
                                                 "where dt_empleado.status = 'A' " + _
                                                 "  and (select count(*) from dt_empl_ind where dt_empl_ind.id_dt_empleado = dt_empleado.id_dt_empleado and status = 'A') > 0 " + _
                                                 "  and (select cast(valor as datetime) " + _
                                                 "       from cg_parametros " + _
                                                 "       where parametro = 'mes_eval') >= dateadd(mm, 2, cast(cast(datepart(year, fecha_ingreso) as varchar(4)) + '/' + cast(datepart(month, fecha_ingreso) as varchar(2)) + '/01' as date)) " + _
                                                 "  and dt_area.id_direccion = @id_direccion " + _
                                                 "  and (id_ms_evaluacion is null or ms_evaluacion.status in ('P', 'PC', 'V', 'PA')) "
                        SCMValores.Parameters.AddWithValue("@id_direccion", Val(._txtIdDireccion.Text))
                        ConexionBD.Open()
                        evalPend = SCMValores.ExecuteScalar
                        ConexionBD.Close()

                        'En caso de que no existan evaluaciones pendientes por generar y/o autorizar, Calcular la Evaluación del Director
                        If evalPend = 0 Then
                            Dim contDir As Integer
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "select count(*) " + _
                                                     "from dt_empleado " + _
                                                     "  left join dt_area on dt_empleado.id_dt_area = dt_area.id_dt_area " + _
                                                     "  left join cg_direccion on dt_area.id_direccion = cg_direccion.id_direccion and cg_direccion.no_empleado_dir = dt_empleado.no_empleado " + _
                                                     "  left join ms_evaluacion on dt_empleado.no_empleado = ms_evaluacion.no_empleado " + _
                                                     "                         and mes_eval = month((select cast(valor as datetime) " + _
                                                     "                                               from cg_parametros " + _
                                                     "                                               where parametro = 'mes_eval')) " + _
                                                     "                         and año_eval = year((select cast(valor as datetime) " + _
                                                     "                                              from cg_parametros " + _
                                                     "                                              where parametro = 'mes_eval')) " + _
                                                     "where dt_empleado.status = 'A' " + _
                                                     "  and cg_direccion.id_direccion = @id_direccion " + _
                                                     "  and id_ms_evaluacion is null "
                            SCMValores.Parameters.AddWithValue("@id_direccion", Val(._txtIdDireccion.Text))
                            ConexionBD.Open()
                            contDir = SCMValores.ExecuteScalar
                            ConexionBD.Close()
                            If contDir > 0 Then
                                'Calcular Evaluación del Director con base al personal de su 1er nivel
                                Dim porcentDir As Decimal
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "select avg(ms_evaluacion.porcent_cumpl) " + _
                                                         "from dt_empleado " + _
                                                         "  left join dt_area on dt_empleado.id_dt_area = dt_area.id_dt_area " + _
                                                         "  left join cg_direccion on dt_area.id_direccion = cg_direccion.id_direccion " + _
                                                         "  left join ms_evaluacion on dt_empleado.no_empleado = ms_evaluacion.no_empleado " + _
                                                         "                         and mes_eval = month((select cast(valor as datetime) " + _
                                                         "                                               from cg_parametros " + _
                                                         "                                               where parametro = 'mes_eval')) " + _
                                                         "                         and año_eval = year((select cast(valor as datetime) " + _
                                                         "                                              from cg_parametros " + _
                                                         "                                              where parametro = 'mes_eval')) " + _
                                                         "where dt_empleado.status = 'A' " + _
                                                         "  and dt_empleado.primer_nivel_dir = 'S' " + _
                                                         "  and cg_direccion.id_direccion = @id_direccion " + _
                                                         "  and ms_evaluacion.id_ms_evaluacion is not null "
                                SCMValores.Parameters.AddWithValue("@id_direccion", Val(._txtIdDireccion.Text))
                                ConexionBD.Open()
                                porcentDir = SCMValores.ExecuteScalar
                                ConexionBD.Close()

                                Dim sdaEmpleadoProID As New SqlDataAdapter
                                Dim dsEmpleadoProID As New DataSet
                                sdaEmpleadoProID.SelectCommand = New SqlCommand("select id_dt_empleado " + _
                                                                                "from dt_empleado " + _
                                                                                "  left join dt_area on dt_empleado.id_dt_area = dt_area.id_dt_area " + _
                                                                                "  left join cg_direccion on dt_area.id_direccion = cg_direccion.id_direccion and cg_direccion.no_empleado_dir = dt_empleado.no_empleado " + _
                                                                                "  left join ms_evaluacion on dt_empleado.no_empleado = ms_evaluacion.no_empleado " + _
                                                                                "                         and mes_eval = month((select cast(valor as datetime) " + _
                                                                                "                                               from cg_parametros " + _
                                                                                "                                               where parametro = 'mes_eval')) " + _
                                                                                "                         and año_eval = year((select cast(valor as datetime) " + _
                                                                                "                                              from cg_parametros " + _
                                                                                "                                              where parametro = 'mes_eval')) " + _
                                                                                "where dt_empleado.status = 'A' " + _
                                                                                "  and cg_direccion.id_direccion = @id_direccion " + _
                                                                                "  and id_ms_evaluacion is null ", ConexionBD)
                                sdaEmpleadoProID.SelectCommand.Parameters.AddWithValue("@id_direccion", Val(._txtIdDireccion.Text))
                                ConexionBD.Open()
                                sdaEmpleadoProID.Fill(dsEmpleadoProID)
                                ConexionBD.Close()

                                'Datos Empleado ProcAd
                                Dim sdaEmpleadoProNE As New SqlDataAdapter
                                Dim dsEmpleadoProNE As New DataSet
                                sdaEmpleadoProNE.SelectCommand = New SqlCommand("select no_empleado " + _
                                                                                "     , unidad_neg " + _
                                                                                "     , cg_empresa.nombre as empresa " + _
                                                                                "     , direccion " + _
                                                                                "     , area " + _
                                                                                "     , (select month(cast(valor as date)) " + _
                                                                                "         from cg_parametros " + _
                                                                                "         where parametro = 'mes_eval') as mes_eval " + _
                                                                                "     , (select year(cast(valor as date)) " + _
                                                                                "         from cg_parametros " + _
                                                                                "         where parametro = 'mes_eval') as año_eval " + _
                                                                                "     , case when dt_empleado.nombre = nombre_aut then no_empleado_dir else no_empleado_aut end as no_empleadoAut " + _
                                                                                "     , case when dt_empleado.id_usr_valida is null then 0 else dt_empleado.id_usr_valida end as id_usrVal " + _
                                                                                "     , id_usr_dir " + _
                                                                                "     , id_empleado as id_empleado_aut " + _
                                                                                "     , dt_empleado.id_dt_area " + _
                                                                                "from dt_empleado " + _
                                                                                "  left join cg_unidad_neg on dt_empleado.id_unidad_neg = cg_unidad_neg.id_unidad_neg " + _
                                                                                "  left join dt_area on dt_empleado.id_dt_area = dt_area.id_dt_area " + _
                                                                                "  left join cg_direccion on dt_area.id_direccion = cg_direccion.id_direccion " + _
                                                                                "  left join bd_Empleado.dbo.cg_empresa on cg_direccion.id_empresa = cg_empresa.id_empresa " + _
                                                                                "  left join cg_usuario on cg_usuario.id_usuario = case when dt_empleado.nombre = nombre_aut then id_usr_dir else id_usr_aut end " + _
                                                                                "where id_dt_empleado = @id_dt_empleado ", ConexionBD)
                                sdaEmpleadoProNE.SelectCommand.Parameters.AddWithValue("@id_dt_empleado", Val(dsEmpleadoProID.Tables(0).Rows(0).Item("id_dt_empleado").ToString()))
                                ConexionBD.Open()
                                sdaEmpleadoProNE.Fill(dsEmpleadoProNE)
                                ConexionBD.Close()

                                'Datos Empleado NOM
                                Dim ConexionBDNom As SqlConnection = New System.Data.SqlClient.SqlConnection
                                ConexionBDNom.ConnectionString = accessDB.conBD("NOM")
                                Dim sdaEmpleadoNom As New SqlDataAdapter
                                Dim dsEmpleadoNom As New DataSet
                                sdaEmpleadoNom.SelectCommand = New SqlCommand("select rtrim(ltrim(isnull(nomtrab.nombre,''))) as nombre " + _
                                                                              "     , rtrim(ltrim(isnull(nomtrab.apepat,''))) as ap_paterno " + _
                                                                              "     , rtrim(ltrim(isnull(nomtrab.apemat,''))) as ap_materno " + _
                                                                              "     , isnull(cc.desubi,'') as centro_costo " + _
                                                                              "     , rtrim(ltrim(isnull(nompues.despue,''))) as puesto " + _
                                                                              "     , isnull(nompais.despai,'') as base " + _
                                                                              "     , rtrim(ltrim(isnull(nomtrabL.nombre,''))) + ' ' + rtrim(ltrim(isnull(nomtrabL.apepat,''))) + ' ' + rtrim(ltrim(isnull(nomtrabL.apemat,''))) as lider " + _
                                                                              "     , rtrim(ltrim(isnull(nompuesL.despue,''))) as puestoL " + _
                                                                              "from nomtrab " + _
                                                                              "  left join nompues on nomtrab.cvecia = nompues.cvecia and nomtrab.cvepue = nompues.cvepue " + _
                                                                              "  left join nomposi on nomtrab.cvecia = nomposi.cvecia and nomtrab.cvepos = nomposi.cvepos " + _
                                                                              "  left join nompais on nomposi.cvecia = nompais.cvecia and nomposi.cvepai = nompais.cvepai " + _
                                                                              "  left join nomubic cc on nomposi.cvecia = cc.cvecia  and nomposi.cvepai = cc.cvepai and nomposi.cveciu = cc.cveciu and nomposi.cveubi = cc.cveubi " + _
                                                                              "  left join nomtrab nomtrabL on nomtrabL.cvetra = @no_empleado_lid and nomtrabL.status = 'A' " + _
                                                                              "  left join nompues nompuesL on nomtrabL.cvecia = nompuesL.cvecia and nomtrabL.cvepue = nompuesL.cvepue " + _
                                                                              "where nomtrab.status = 'A' " + _
                                                                              "  and nomtrab.cvetra = @no_empleado ", ConexionBDNom)
                                sdaEmpleadoNom.SelectCommand.Parameters.AddWithValue("@no_empleado", dsEmpleadoProNE.Tables(0).Rows(0).Item("no_empleado").ToString())
                                sdaEmpleadoNom.SelectCommand.Parameters.AddWithValue("@no_empleado_lid", dsEmpleadoProNE.Tables(0).Rows(0).Item("no_empleadoAut").ToString())
                                ConexionBD.Open()
                                sdaEmpleadoNom.Fill(dsEmpleadoNom)
                                ConexionBD.Close()

                                'Insertar en ms_evaluación
                                SCMValores.CommandText = ""
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "insert into ms_evaluacion(  no_empleado,  nombre,  ap_paterno,  ap_materno,  empresa,  centro_costo,  puesto,  base,  unidad_neg,  direccion,  id_dt_area,  area,  lider,  puesto_lider,  año_eval,  mes_eval,  porcent_cumpl,  cobra_bono_asist, cobra_bono_cumpl_UN,  id_usr_registro,  fecha_registro,  com_registro,  id_usr_valida,  com_valida,  id_usr_director,  fecha_director,  status) " + _
                                                         "                   values( @no_empleado, @nombre, @ap_paterno, @ap_materno, @empresa, @centro_costo, @puesto, @base, @unidad_neg, @direccion, @id_dt_area, @area, @lider, @puesto_lider, @año_eval, @mes_eval, @porcent_cumpl, @cobra_bono_asist,   @cobra_bono_asist, @id_usr_registro, @fecha_registro, @com_registro, @id_usr_valida, @com_valida, @id_usr_director, @fecha_director, @status) "
                                SCMValores.Parameters.AddWithValue("@no_empleado", dsEmpleadoProNE.Tables(0).Rows(0).Item("no_empleado").ToString())
                                SCMValores.Parameters.AddWithValue("@nombre", dsEmpleadoNom.Tables(0).Rows(0).Item("nombre").ToString())
                                SCMValores.Parameters.AddWithValue("@ap_paterno", dsEmpleadoNom.Tables(0).Rows(0).Item("ap_paterno").ToString())
                                SCMValores.Parameters.AddWithValue("@ap_materno", dsEmpleadoNom.Tables(0).Rows(0).Item("ap_materno").ToString())
                                SCMValores.Parameters.AddWithValue("@empresa", dsEmpleadoProNE.Tables(0).Rows(0).Item("empresa").ToString())
                                SCMValores.Parameters.AddWithValue("@centro_costo", dsEmpleadoNom.Tables(0).Rows(0).Item("centro_costo").ToString())
                                SCMValores.Parameters.AddWithValue("@puesto", dsEmpleadoNom.Tables(0).Rows(0).Item("puesto").ToString())
                                SCMValores.Parameters.AddWithValue("@base", dsEmpleadoNom.Tables(0).Rows(0).Item("base").ToString())
                                SCMValores.Parameters.AddWithValue("@unidad_neg", dsEmpleadoProNE.Tables(0).Rows(0).Item("unidad_neg").ToString())
                                SCMValores.Parameters.AddWithValue("@direccion", dsEmpleadoProNE.Tables(0).Rows(0).Item("direccion").ToString())
                                SCMValores.Parameters.AddWithValue("@id_dt_area", dsEmpleadoProNE.Tables(0).Rows(0).Item("id_dt_area").ToString())
                                SCMValores.Parameters.AddWithValue("@area", dsEmpleadoProNE.Tables(0).Rows(0).Item("area").ToString())
                                SCMValores.Parameters.AddWithValue("@lider", dsEmpleadoNom.Tables(0).Rows(0).Item("lider").ToString())
                                SCMValores.Parameters.AddWithValue("@puesto_lider", dsEmpleadoNom.Tables(0).Rows(0).Item("puestoL").ToString())
                                SCMValores.Parameters.AddWithValue("@año_eval", Val(dsEmpleadoProNE.Tables(0).Rows(0).Item("año_eval").ToString()))
                                SCMValores.Parameters.AddWithValue("@mes_eval", Val(dsEmpleadoProNE.Tables(0).Rows(0).Item("mes_eval").ToString()))
                                SCMValores.Parameters.AddWithValue("@porcent_cumpl", porcentDir)
                                If porcentDir >= 0.66 Then
                                    SCMValores.Parameters.AddWithValue("@cobra_bono_asist", "S")
                                Else
                                    SCMValores.Parameters.AddWithValue("@cobra_bono_asist", "N")
                                End If
                                SCMValores.Parameters.AddWithValue("@id_usr_registro", Val(._txtIdUsuario.Text)) 'Val(dsEmpleadoProID.Tables(0).Rows(i).Item("id_usr_empl").ToString()))
                                SCMValores.Parameters.AddWithValue("@fecha_registro", fecha)
                                SCMValores.Parameters.AddWithValue("@com_registro", DBNull.Value)
                                SCMValores.Parameters.AddWithValue("@com_valida", DBNull.Value)
                                If Val(dsEmpleadoProNE.Tables(0).Rows(0).Item("id_usrVal").ToString()) = 0 Then
                                    SCMValores.Parameters.AddWithValue("@id_usr_valida", DBNull.Value)
                                Else
                                    SCMValores.Parameters.AddWithValue("@id_usr_valida", Val(dsEmpleadoProNE.Tables(0).Rows(0).Item("id_usrVal").ToString()))
                                End If
                                SCMValores.Parameters.AddWithValue("@id_usr_director", Val(dsEmpleadoProNE.Tables(0).Rows(0).Item("id_usr_dir").ToString()))
                                SCMValores.Parameters.AddWithValue("@fecha_director", fecha)
                                SCMValores.Parameters.AddWithValue("@status", "PCE")
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()

                                sdaEmpleadoNom.Dispose()
                                dsEmpleadoNom.Dispose()
                                sdaEmpleadoProNE.Dispose()
                                dsEmpleadoProNE.Dispose()

                            End If

                        End If

                        .btnAutorizar.Enabled = False

                        Session("id_actividadM") = 73
                        Session("TipoM") = "Eval"
                        Server.Transfer("Menu.aspx")
                    End While
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
                Dim contInv As Integer

                SCMValores.CommandText = "select count(*) " + _
                                         "from ms_evaluacion " + _
                                         "where id_dt_area = @id_dt_area " + _
                                         "  and id_usr_registro = @id_usr_registro " + _
                                         "  and invalida = 'S' " + _
                                         "  and status = 'PA' " + _
                                         "  and mes_eval = month((select cast(valor as datetime) " + _
                                         "                        from cg_parametros " + _
                                         "                        where parametro = 'mes_eval')) " + _
                                         "  and año_eval = year((select cast(valor as datetime) " + _
                                         "                       from cg_parametros " + _
                                         "                       where parametro = 'mes_eval')) "
                SCMValores.Parameters.AddWithValue("@id_dt_area", Val(._txtIdDtArea.Text))
                SCMValores.Parameters.AddWithValue("@id_usr_registro", Val(._txtIdUsuarioE.Text))
                ConexionBD.Open()
                contInv = SCMValores.ExecuteScalar
                ConexionBD.Close()

                If contInv = 0 Then
                    .litError.Text = "No hay evaluaciones marcadas como inválidas, favor de verificarlo"
                Else
                    Dim fecha As DateTime = Date.Now
                    While Val(._txtBan.Text) = 0
                        'Actualizar Estatus de las Evaluaciones Individuales
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_evaluacion  " + _
                                                 "  set status = 'PCA' " + _
                                                 "where id_dt_area = @id_dt_area " + _
                                                 "  and id_usr_registro = @id_usr_registro " + _
                                                 "  and status = 'PA' " + _
                                                 "  and mes_eval = month((select cast(valor as datetime) " + _
                                                 "                        from cg_parametros " + _
                                                 "                        where parametro = 'mes_eval')) " + _
                                                 "  and año_eval = year((select cast(valor as datetime) " + _
                                                 "                       from cg_parametros " + _
                                                 "                       where parametro = 'mes_eval')) "
                        SCMValores.Parameters.AddWithValue("@fecha_director", fecha)
                        SCMValores.Parameters.AddWithValue("@id_dt_area", Val(._txtIdDtArea.Text))
                        SCMValores.Parameters.AddWithValue("@id_usr_registro", Val(._txtIdUsuarioE.Text))
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

                        Session("id_actividadM") = 73
                        Session("TipoM") = "Eval"
                        Server.Transfer("Menu.aspx")
                    End While
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class