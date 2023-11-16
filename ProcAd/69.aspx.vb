Public Class _69
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 5
                    'Session("idMsInst") = 41094

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtIdMsInst.Text = Session("idMsInst")
                        ._txtBan.Text = 0

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        'Datos de la Evaluación
                        Dim sdaEvaluacion As New SqlDataAdapter
                        Dim dsEvaluacion As New DataSet
                        sdaEvaluacion.SelectCommand = New SqlCommand("select id_ms_evaluacion " + _
                                                                     "     , nombre + ' ' + ap_paterno + ' ' + ap_materno as empleado " + _
                                                                     "     , puesto " + _
                                                                     "     , area " + _
                                                                     "     , lider " + _
                                                                     "     , puesto_lider " + _
                                                                     "     , case mes_eval " + _
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
                                                                     "       end + ' ' + cast(año_eval as varchar(4)) as mes_eval " + _
                                                                     "     , format((select sum(ponderacion) " + _
                                                                     "        from dt_evaluacion " + _
                                                                     "        where dt_evaluacion.id_ms_evaluacion = ms_evaluacion.id_ms_evaluacion) * 100, '##0.##', 'es_MX') + ' %' as ponteracionT " + _
                                                                     "     , format(isnull(porcent_cumpl, 0) * 100, '##0.##', 'es_MX') + ' %' as cumpl_pondT " + _
                                                                     "     , case " + _
                                                                     "         when isnull(porcent_cumpl, 0) * 100 >= 100 then 'Sobresaliente' " + _
                                                                     "         else case " + _
                                                                     "                when isnull(porcent_cumpl, 0) * 100 >= 90 and isnull(porcent_cumpl, 0) * 100 < 100 then 'Satisfactorio' " + _
                                                                     "                else case " + _
                                                                     "                       when isnull(porcent_cumpl, 0) * 100 >= 66 and isnull(porcent_cumpl, 0) * 100 < 90 then 'Necesita Mejorar' " + _
                                                                     "                       else case " + _
                                                                     "                              when isnull(porcent_cumpl, 0) * 100 < 66 then 'No Satisfactorio' " + _
                                                                     "                              else '-' " + _
                                                                     "                            end " + _
                                                                     "                     end " + _
                                                                     "              end " + _
                                                                     "       end as califInd " + _
                                                                     "     , isnull(com_registro, '') as com_registro " + _
                                                                     "     , isnull(com_valida, '') as com_valida " + _
                                                                     "     , isnull(id_usr_registro, '') as id_usr_registro " + _
                                                                     "     , ms_evaluacion.id_dt_area " + _
                                                                     "from ms_instancia " + _
                                                                     "  left join ms_evaluacion on ms_instancia.id_ms_sol = ms_evaluacion.id_ms_evaluacion and ms_instancia.tipo = 'Eval' " + _
                                                                     "where ms_instancia.id_ms_instancia = @idMsInst ", ConexionBD)
                        sdaEvaluacion.SelectCommand.Parameters.AddWithValue("@idMsInst", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaEvaluacion.Fill(dsEvaluacion)
                        ConexionBD.Close()
                        .lblFolio.Text = dsEvaluacion.Tables(0).Rows(0).Item("id_ms_evaluacion").ToString()
                        .lblColaborador.Text = dsEvaluacion.Tables(0).Rows(0).Item("empleado").ToString()
                        .lblPuestoEval.Text = dsEvaluacion.Tables(0).Rows(0).Item("puesto").ToString()
                        .lblArea.Text = dsEvaluacion.Tables(0).Rows(0).Item("area").ToString()
                        .lblLider.Text = dsEvaluacion.Tables(0).Rows(0).Item("lider").ToString()
                        .lblPuestoLider.Text = dsEvaluacion.Tables(0).Rows(0).Item("puesto_lider").ToString()
                        .lblMesEval.Text = dsEvaluacion.Tables(0).Rows(0).Item("mes_eval").ToString()
                        .lblPondTotal.Text = dsEvaluacion.Tables(0).Rows(0).Item("ponteracionT").ToString()
                        .lblPorcentCumpl.Text = dsEvaluacion.Tables(0).Rows(0).Item("cumpl_pondT").ToString()
                        .lblCalifCumpl.Text = dsEvaluacion.Tables(0).Rows(0).Item("califInd").ToString()
                        .txtComentarioC.Text = dsEvaluacion.Tables(0).Rows(0).Item("com_registro").ToString()
                        .txtComentarioL.Text = dsEvaluacion.Tables(0).Rows(0).Item("com_valida").ToString()
                        ._txtIdUsuarioE.Text = dsEvaluacion.Tables(0).Rows(0).Item("id_usr_registro").ToString()
                        ._txtIdDtArea.Text = dsEvaluacion.Tables(0).Rows(0).Item("id_dt_area").ToString()
                        sdaEvaluacion.Dispose()
                        dsEvaluacion.Dispose()

                        'Indicadores Registrados
                        Dim sdaIndicadores As New SqlDataAdapter
                        Dim dsIndicadores As New DataSet
                        .gvIndicadores.DataSource = dsIndicadores
                        sdaIndicadores.SelectCommand = New SqlCommand("select indicador " + _
                                                                      "     , (select case mes_eval " + _
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
                                                                      "              + ' ' + cast(año_eval as varchar(4)) as mes_eval " + _
                                                                      "         from ms_evaluacion " + _
                                                                      "         where ms_evaluacion.id_ms_evaluacion = dt_evaluacion.id_ms_evaluacion) as mes_eval " + _
                                                                      "     , tipo_indicador " + _
                                                                      "     , ponderacion " + _
                                                                      "     , meta " + _
                                                                      "     , real " + _
                                                                      "     , cumpl_pond " + _
                                                                      "     , fuente " + _
                                                                      "from dt_evaluacion " + _
                                                                      "where id_ms_evaluacion = @id_ms_evaluacion " + _
                                                                      "order by indicador ", ConexionBD)
                        sdaIndicadores.SelectCommand.Parameters.AddWithValue("@id_ms_evaluacion", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        sdaIndicadores.Fill(dsIndicadores)
                        .gvIndicadores.DataBind()
                        ConexionBD.Close()
                        sdaIndicadores.Dispose()
                        dsIndicadores.Dispose()

                        llenarEvidencias()
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

    Public Sub llenarEvidencias()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                'Evidencias
                Dim sdaEvidencias As New SqlDataAdapter
                Dim dsEvidencias As New DataSet
                .gvEvidencias.DataSource = dsEvidencias
                sdaEvidencias.SelectCommand = New SqlCommand("select evidencia " + _
                                                             "     , 'http://148.223.153.43/ProcAd - Evidencias/' + cast(id_dt_evaluacion_evid as varchar(20)) + 'EvalE-' + evidencia as path " + _
                                                             "from dt_evaluacion_evid " + _
                                                             "where id_ms_evaluacion in (select msEvalA.id_ms_evaluacion " + _
                                                             "                           from ms_evaluacion msEval " + _
                                                             "                             left join dt_empleado dtEmplA on msEval.id_dt_area = dtEmplA.id_dt_area " + _
                                                             "                             left join ms_evaluacion msEvalA on dtEmplA.no_empleado = msEvalA.no_empleado " + _
                                                             "                                                            and msEvalA.mes_eval = month((select cast(valor as datetime) " + _
                                                             "                                                                                          from cg_parametros " + _
                                                             "                                                                                          where parametro = 'mes_eval')) " + _
                                                             "                                                            and msEvalA.año_eval = year((select cast(valor as datetime) " + _
                                                             "                                                                                         from cg_parametros " + _
                                                             "                                                                                         where parametro = 'mes_eval')) " + _
                                                             "                           where msEval.id_ms_evaluacion = @id_ms_evaluacion " + _
                                                             "                             and dtEmplA.status = 'A' " + _
                                                             "                             and msEvalA.id_ms_evaluacion is not null) ", ConexionBD)
                sdaEvidencias.SelectCommand.Parameters.AddWithValue("@id_ms_evaluacion", Val(.lblFolio.Text))
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
                    'Actualizar datos de la Evaluación
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_evaluacion set fecha_valida = @fecha_valida, com_valida = @com_valida, status = 'V' where id_ms_evaluacion = @id_ms_evaluacion "
                    SCMValores.Parameters.AddWithValue("@fecha_valida", fecha)
                    If .txtComentarioL.Text.Trim = "" Then
                        SCMValores.Parameters.AddWithValue("@com_valida", DBNull.Value)
                    Else
                        SCMValores.Parameters.AddWithValue("@com_valida", .txtComentarioL.Text.Trim)
                    End If
                    SCMValores.Parameters.AddWithValue("@id_ms_evaluacion", Val(.lblFolio.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Actualizar Instancia
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 70)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Registrar en Histórico
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                             "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 70)
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    ._txtBan.Text = 1

                    'Determinar si ya se autorizaron todas las evaluaciones del área
                    Dim evalPend As Integer
                    SCMValores.Parameters.Clear()
                    'SCMValores.CommandText = "select count(*) as eval_pend " + _
                    '                         "from dt_empleado " + _
                    '                         "  left join ms_evaluacion on dt_empleado.id_usr_empl = ms_evaluacion.id_usr_registro " + _
                    '                         "where dt_empleado.status = 'A' " + _
                    '                         "  and (select cast(valor as datetime) " + _
                    '                         "       from cg_parametros " + _
                    '                         "       where parametro = 'mes_eval') >= dateadd(mm, 2, cast(cast(datepart(year, fecha_ingreso) as varchar(4)) + '/' + cast(datepart(month, fecha_ingreso) as varchar(2)) + '/01' as date)) " + _
                    '                         "  and dt_empleado.id_dt_area = @id_dt_area " + _
                    '                         "  and (id_ms_evaluacion is null " + _
                    '                         "   or (ms_evaluacion.status in ('P', 'PC') " + _
                    '                         "   and mes_eval = month((select cast(valor as datetime) " + _
                    '                         "                         from cg_parametros " + _
                    '                         "                         where parametro = 'mes_eval')) " + _
                    '                         "   and año_eval = year((select cast(valor as datetime) " + _
                    '                         "                        from cg_parametros " + _
                    '                         "                        where parametro = 'mes_eval')))) "
                    SCMValores.CommandText = "select count(*) as eval_pend " + _
                                             "from dt_empleado " + _
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
                                             "  and dt_empleado.id_dt_area = @id_dt_area " + _
                                             "  and dt_empleado.id_usr_evalua = @id_usuario " + _
                                             "  and (id_ms_evaluacion is null or (ms_evaluacion.status in ('P', 'PC'))) "
                    SCMValores.Parameters.AddWithValue("@id_dt_area", Val(._txtIdDtArea.Text))
                    SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuarioE.Text))
                    ConexionBD.Open()
                    evalPend = SCMValores.ExecuteScalar
                    ConexionBD.Close()

                    'En caso de que no existan evaluaciones pendientes por generar y/o autorizar
                    If evalPend = 0 Then
                        'Validar si existen empleados de 2 meses o menos para insertar su registros 
                        Dim contEmpl As Integer
                        SCMValores.Parameters.Clear()
                        'SCMValores.CommandText = "select count(*) " + _
                        '                         "from dt_empleado " + _
                        '                         "  left join ms_evaluacion on dt_empleado.id_usr_empl = ms_evaluacion.id_usr_registro " + _
                        '                         "where dt_empleado.status = 'A' " + _
                        '                         "  and (select cast(valor as datetime) " + _
                        '                         "       from cg_parametros " + _
                        '                         "       where parametro = 'mes_eval') < dateadd(mm, 2, cast(cast(datepart(year, fecha_ingreso) as varchar(4)) + '/' + cast(datepart(month, fecha_ingreso) as varchar(2)) + '/01' as date)) " + _
                        '                         "  and dt_empleado.id_dt_area = @id_dt_area " + _
                        '                         "  and dt_empleado.id_usr_evalua = @id_usuario " + _
                        '                         "  and (id_ms_evaluacion is null " + _
                        '                         "   or (ms_evaluacion.status in ('P', 'PC') " + _
                        '                         "   and mes_eval = month((select cast(valor as datetime) " + _
                        '                         "                         from cg_parametros " + _
                        '                         "                         where parametro = 'mes_eval')) " + _
                        '                         "   and año_eval = year((select cast(valor as datetime) " + _
                        '                         "                        from cg_parametros " + _
                        '                         "                        where parametro = 'mes_eval')))) "
                        SCMValores.CommandText = "select count(*) " + _
                                                 "from dt_empleado " + _
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
                                                 "       where parametro = 'mes_eval') < dateadd(mm, 2, cast(cast(datepart(year, fecha_ingreso) as varchar(4)) + '/' + cast(datepart(month, fecha_ingreso) as varchar(2)) + '/01' as date)) " + _
                                                 "  and dt_empleado.id_dt_area = @id_dt_area " + _
                                                 "  and dt_empleado.id_usr_evalua = @id_usuario " + _
                                                 "  and id_ms_evaluacion is null "
                        SCMValores.Parameters.AddWithValue("@id_dt_area", Val(._txtIdDtArea.Text))
                        SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuarioE.Text))
                        ConexionBD.Open()
                        contEmpl = SCMValores.ExecuteScalar
                        ConexionBD.Close()
                        If contEmpl > 0 Then
                            'Insertar registros
                            Dim sdaEmpleadoProID As New SqlDataAdapter
                            Dim dsEmpleadoProID As New DataSet
                            sdaEmpleadoProID.SelectCommand = New SqlCommand("select id_dt_empleado " + _
                                                                            "from dt_empleado " + _
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
                                                                            "       where parametro = 'mes_eval') < dateadd(mm, 2, cast(cast(datepart(year, fecha_ingreso) as varchar(4)) + '/' + cast(datepart(month, fecha_ingreso) as varchar(2)) + '/01' as date)) " + _
                                                                            "  and dt_empleado.id_dt_area = @id_dt_area " + _
                                                                            "  and dt_empleado.id_usr_evalua = @id_usuario " + _
                                                                            "  and id_ms_evaluacion is null ", ConexionBD)
                            sdaEmpleadoProID.SelectCommand.Parameters.AddWithValue("@id_dt_area", Val(._txtIdDtArea.Text))
                            sdaEmpleadoProID.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuarioE.Text))
                            ConexionBD.Open()
                            sdaEmpleadoProID.Fill(dsEmpleadoProID)
                            ConexionBD.Close()

                            For i = 0 To dsEmpleadoProID.Tables(0).Rows().Count - 1
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
                                                                              "     , dt_empleado.id_usr_evalua " + _
                                                                              "from dt_empleado " + _
                                                                              "  left join cg_unidad_neg on dt_empleado.id_unidad_neg = cg_unidad_neg.id_unidad_neg " + _
                                                                              "  left join dt_area on dt_empleado.id_dt_area = dt_area.id_dt_area " + _
                                                                              "  left join cg_direccion on dt_area.id_direccion = cg_direccion.id_direccion " + _
                                                                              "  left join bd_Empleado.dbo.cg_empresa on cg_direccion.id_empresa = cg_empresa.id_empresa " + _
                                                                              "  left join cg_usuario on cg_usuario.id_usuario = case when dt_empleado.nombre = nombre_aut then id_usr_dir else id_usr_aut end " + _
                                                                              "where id_dt_empleado = @id_dt_empleado ", ConexionBD)
                                sdaEmpleadoProNE.SelectCommand.Parameters.AddWithValue("@id_dt_empleado", Val(dsEmpleadoProID.Tables(0).Rows(i).Item("id_dt_empleado").ToString()))
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
                                SCMValores.CommandText = "insert into ms_evaluacion(  no_empleado,  nombre,  ap_paterno,  ap_materno,  empresa,  centro_costo,  puesto,  base,  unidad_neg,  direccion,  id_dt_area,  area,  lider,  puesto_lider,  año_eval,  mes_eval,  porcent_cumpl,  cobra_bono_asist, cobra_bono_cumpl_UN,  id_usr_registro,  fecha_registro,  com_registro,  id_usr_valida,  fecha_valida,  com_valida,  id_usr_director,  status) " + _
                                                         "                   values( @no_empleado, @nombre, @ap_paterno, @ap_materno, @empresa, @centro_costo, @puesto, @base, @unidad_neg, @direccion, @id_dt_area, @area, @lider, @puesto_lider, @año_eval, @mes_eval, @porcent_cumpl, @cobra_bono_asist,   @cobra_bono_asist, @id_usr_registro, @fecha_registro, @com_registro, @id_usr_valida, @fecha_valida, @com_valida, @id_usr_director, @status) "
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
                                SCMValores.Parameters.AddWithValue("@porcent_cumpl", DBNull.Value)
                                SCMValores.Parameters.AddWithValue("@cobra_bono_asist", "S")
                                SCMValores.Parameters.AddWithValue("@id_usr_registro", Val(dsEmpleadoProNE.Tables(0).Rows(0).Item("id_usr_evalua").ToString())) 'Val(dsEmpleadoProID.Tables(0).Rows(i).Item("id_usr_empl").ToString()))
                                SCMValores.Parameters.AddWithValue("@fecha_registro", fecha)
                                SCMValores.Parameters.AddWithValue("@com_registro", DBNull.Value)
                                SCMValores.Parameters.AddWithValue("@com_valida", DBNull.Value)
                                If Val(dsEmpleadoProNE.Tables(0).Rows(0).Item("id_usrVal").ToString()) = 0 Then
                                    SCMValores.Parameters.AddWithValue("@id_usr_valida", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@fecha_valida", DBNull.Value)
                                Else
                                    SCMValores.Parameters.AddWithValue("@id_usr_valida", Val(dsEmpleadoProNE.Tables(0).Rows(0).Item("id_usrVal").ToString()))
                                    SCMValores.Parameters.AddWithValue("@fecha_valida", fecha)
                                End If
                                SCMValores.Parameters.AddWithValue("@id_usr_director", Val(dsEmpleadoProNE.Tables(0).Rows(0).Item("id_usr_dir").ToString()))
                                SCMValores.Parameters.AddWithValue("@status", "V")
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()

                                sdaEmpleadoNom.Dispose()
                                dsEmpleadoNom.Dispose()
                                sdaEmpleadoProNE.Dispose()
                                dsEmpleadoProNE.Dispose()
                            Next

                        End If

                        'Actualizar Estatus de las Evaluaciones
                        SCMValores.CommandText = "update ms_evaluacion  " + _
                                                 "  set status = 'PA' " + _
                                                 "where id_dt_area = @id_dt_area " + _
                                                 "  and id_usr_registro = @id_usr_evalua " + _
                                                 "  and status = 'V' " + _
                                                 "  and mes_eval = month((select cast(valor as datetime) " + _
                                                 "                        from cg_parametros " + _
                                                 "                        where parametro = 'mes_eval')) " + _
                                                 "  and año_eval = year((select cast(valor as datetime) " + _
                                                 "                       from cg_parametros " + _
                                                 "                       where parametro = 'mes_eval')) "
                        SCMValores.Parameters.AddWithValue("@id_usr_evalua", Val(._txtIdUsuarioE.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Obtener id de última evaluación registrada
                        Dim idMsEval As Integer
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "select isnull(max(id_ms_evaluacion), 0) as id_ms_evaluacion " + _
                                                 "from ms_evaluacion " + _
                                                 "where id_dt_area = @id_dt_area " + _
                                                 "  and id_usr_registro = @id_usr_evalua " + _
                                                 "  and mes_eval = month((select cast(valor as datetime) " + _
                                                 "                        from cg_parametros " + _
                                                 "                        where parametro = 'mes_eval')) " + _
                                                 "  and año_eval = year((select cast(valor as datetime) " + _
                                                 "                       from cg_parametros " + _
                                                 "                       where parametro = 'mes_eval')) "
                        SCMValores.Parameters.AddWithValue("@id_dt_area", Val(._txtIdDtArea.Text))
                        SCMValores.Parameters.AddWithValue("@id_usr_evalua", Val(._txtIdUsuarioE.Text))
                        ConexionBD.Open()
                        idMsEval = SCMValores.ExecuteScalar
                        ConexionBD.Close()

                        'Datos Empleado ProcAd
                        Dim sdaEmpleadoPro As New SqlDataAdapter
                        Dim dsEmpleadoPro As New DataSet
                        sdaEmpleadoPro.SelectCommand = New SqlCommand("select ms_evaluacion.unidad_neg " + _
                                                                      "     , ms_evaluacion.direccion " + _
                                                                      "     , ms_evaluacion.area " + _
                                                                      "     , dt_area.nombre_aut as lider " + _
                                                                      "     , case when ms_evaluacion.no_empleado = dt_area.no_empleado_aut then ms_evaluacion.puesto else ms_evaluacion.puesto_lider end as puesto_lider " + _
                                                                      "     , ms_evaluacion.año_eval " + _
                                                                      "     , ms_evaluacion.mes_eval " + _
                                                                      "     , ms_evaluacion.id_usr_director " + _
                                                                      "from ms_evaluacion " + _
                                                                      "  left join dt_area on ms_evaluacion.id_dt_area = dt_area.id_dt_area " + _
                                                                      "where id_ms_evaluacion = @id_ms_evaluacion ", ConexionBD)
                        sdaEmpleadoPro.SelectCommand.Parameters.AddWithValue("@id_ms_evaluacion", idMsEval)
                        ConexionBD.Open()
                        sdaEmpleadoPro.Fill(dsEmpleadoPro)
                        ConexionBD.Close()

                        'Insertar en tabla ms_evaluacionA
                        Dim idMsEvalA As Integer
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_evaluacionA ( unidad_neg,  direccion,  area,  id_dt_area,  lider,  puesto_lider,  año_eval,  mes_eval,  id_usr_registro,  fecha_registro,  com_registro,  id_usr_director,  status,  id_usr_evalua) " + _
                                                 "                    values (@unidad_neg, @direccion, @area, @id_dt_area, @lider, @puesto_lider, @año_eval, @mes_eval, @id_usr_registro, @fecha_registro, @com_registro, @id_usr_director, @status, @id_usr_evalua) "
                        SCMValores.Parameters.AddWithValue("@unidad_neg", dsEmpleadoPro.Tables(0).Rows(0).Item("unidad_neg").ToString())
                        SCMValores.Parameters.AddWithValue("@direccion", dsEmpleadoPro.Tables(0).Rows(0).Item("direccion").ToString())
                        SCMValores.Parameters.AddWithValue("@area", dsEmpleadoPro.Tables(0).Rows(0).Item("area").ToString())
                        SCMValores.Parameters.AddWithValue("@id_dt_area", Val(._txtIdDtArea.Text))
                        SCMValores.Parameters.AddWithValue("@lider", dsEmpleadoPro.Tables(0).Rows(0).Item("lider").ToString())
                        SCMValores.Parameters.AddWithValue("@puesto_lider", dsEmpleadoPro.Tables(0).Rows(0).Item("puesto_lider").ToString())
                        SCMValores.Parameters.AddWithValue("@año_eval", Val(dsEmpleadoPro.Tables(0).Rows(0).Item("año_eval").ToString()))
                        SCMValores.Parameters.AddWithValue("@mes_eval", Val(dsEmpleadoPro.Tables(0).Rows(0).Item("mes_eval").ToString()))
                        SCMValores.Parameters.AddWithValue("@id_usr_registro", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@id_usr_evalua", Val(._txtIdUsuarioE.Text))
                        SCMValores.Parameters.AddWithValue("@fecha_registro", fecha)
                        SCMValores.Parameters.AddWithValue("@com_registro", DBNull.Value)
                        SCMValores.Parameters.AddWithValue("@id_usr_director", Val(dsEmpleadoPro.Tables(0).Rows(0).Item("id_usr_director").ToString()))
                        SCMValores.Parameters.AddWithValue("@status", "PA")
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()
                        'Obtener id_ms_evaluacionA
                        SCMValores.CommandText = "select max(id_ms_evaluacionA) " + _
                                                 "from ms_evaluacionA " + _
                                                 "where id_dt_area = @id_dt_area" + _
                                                 "  and id_usr_evalua = @id_usr_evalua " + _
                                                 "  and año_eval = @año_eval " + _
                                                 "  and mes_eval = @mes_eval "
                        ConexionBD.Open()
                        idMsEvalA = SCMValores.ExecuteScalar
                        ConexionBD.Close()

                        'Insertar Instancia de Validación de Evaluaciones del Área
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_instancia ( id_ms_sol,  tipo,  id_actividad) " + _
                                                 "				    values (@id_ms_sol, @tipo, @id_actividad) "
                        SCMValores.Parameters.AddWithValue("@id_ms_sol", idMsEvalA)
                        SCMValores.Parameters.AddWithValue("@tipo", "EvalA")
                        SCMValores.Parameters.AddWithValue("@id_actividad", 73)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()
                        'Obtener ID de la Instancia de Solicitud
                        SCMValores.CommandText = "select max(id_ms_instancia) from ms_instancia where id_ms_sol = @id_ms_sol and tipo = 'EvalA' "
                        ConexionBD.Open()
                        ._txtIdMsInst.Text = SCMValores.ExecuteScalar
                        ConexionBD.Close()
                        'Insertar Históricos
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_historico ( id_ms_instancia,  id_actividad,  id_usr,  fecha) " + _
                                                 "				    values (@id_ms_instancia, @id_actividad, @id_usr, @fecha) "
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 73)
                        SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@fecha", fecha)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        ' ''Envío de Correo
                        ''Dim Mensaje As New System.Net.Mail.MailMessage()
                        ''Dim destinatario As String = ""
                        ' ''Obtener el Correo del Colaborador
                        ''SCMValores.CommandText = "select valor " + _
                        ''                         "from cg_parametros " + _
                        ''                         "where parametro = 'mail_valida_eval' "
                        ''SCMValores.Parameters.AddWithValue("@id_ms_recursos", Val(.lblFolio.Text))
                        ''ConexionBD.Open()
                        ''destinatario = SCMValores.ExecuteScalar()
                        ''ConexionBD.Close()

                        ''Mensaje.[To].Add(destinatario)
                        ''Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                        ''Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                        ''Mensaje.Subject = "ProcAd - Evaluaciones del Área de " + .lblArea.Text + " por Validar"
                        ''Dim texto As String
                        ''texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                        ''        "Se concluyó con la autorización de las evaluaciones del área de <b>" + .lblArea.Text + _
                        ''        "</b>, favor de proceder con su validación. <br></span>"
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

                        'Actualizar Estatus de las Evaluaciones
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_evaluacion  " + _
                                                 "  set id_ms_evaluacionA = @id_ms_evaluacionA " + _
                                                 "where id_dt_area = @id_dt_area " + _
                                                 "  and id_usr_registro = @id_usr_registro " + _
                                                 "  and status = 'PA' " + _
                                                 "  and mes_eval = month((select cast(valor as datetime) " + _
                                                 "                        from cg_parametros " + _
                                                 "                        where parametro = 'mes_eval')) " + _
                                                 "  and año_eval = year((select cast(valor as datetime) " + _
                                                 "                       from cg_parametros " + _
                                                 "                       where parametro = 'mes_eval')) "
                        SCMValores.Parameters.AddWithValue("@id_dt_area", Val(._txtIdDtArea.Text))
                        SCMValores.Parameters.AddWithValue("@id_usr_registro", Val(._txtIdUsuarioE.Text))
                        SCMValores.Parameters.AddWithValue("@id_ms_evaluacionA", idMsEvalA)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Actualizar id_ms_evaluacionA de las Evidencias
                        SCMValores.CommandText = "update dt_evaluacion_evid " + _
                                                 "  set id_ms_evaluacionA = @id_ms_evaluacionA " + _
                                                 "where id_ms_evaluacion in (select id_ms_evaluacion " + _
                                                 "                           from ms_evaluacion " + _
                                                 "                           where ms_evaluacion.id_ms_evaluacionA = @id_ms_evaluacionA) "
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                    End If

                    .btnAutorizar.Enabled = False
                    .btnRechazar.Enabled = False

                    Session("id_actividadM") = 69
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

                If .txtComentarioL.Text.Trim = "" Then
                    .litError.Text = "Favor de ingresar los comentarios correspondientes"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    Dim fecha As DateTime = Date.Now
                    While Val(._txtBan.Text) = 0
                        'Actualizar datos de la Evaluación
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_evaluacion set com_valida = @com_valida, status = 'PC' where id_ms_evaluacion = @id_ms_evaluacion "
                        SCMValores.Parameters.AddWithValue("@com_valida", .txtComentarioL.Text.Trim)
                        SCMValores.Parameters.AddWithValue("@id_ms_evaluacion", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Actualizar Instancia
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 71)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Registrar en Histórico
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                                 "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                        SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        SCMValores.Parameters.AddWithValue("@id_actividad", 71)
                        SCMValores.Parameters.AddWithValue("@fecha", fecha)
                        SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        ._txtBan.Text = 1

                        ' ''Envío de Correo
                        ''Dim Mensaje As New System.Net.Mail.MailMessage()
                        ''Dim destinatario As String = ""
                        ' ''Obtener el Correo del Colaborador
                        ''SCMValores.CommandText = "select correo " + _
                        ''                         "from ms_evaluacion " + _
                        ''                         "  left join cg_usuario on ms_evaluacion.id_usr_registro = cg_usuario.id_usuario " + _
                        ''                         "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                        ''                         "where id_ms_evaluacion = @id_ms_evaluacion "
                        ''SCMValores.Parameters.AddWithValue("@id_ms_evaluacion", Val(.lblFolio.Text))
                        ''ConexionBD.Open()
                        ''destinatario = SCMValores.ExecuteScalar()
                        ''ConexionBD.Close()

                        ''Mensaje.[To].Add(destinatario)
                        ''Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                        ''Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                        ''Mensaje.Subject = "ProcAd - Evaluación No. " + .lblFolio.Text + " Rechazada"
                        ''Dim texto As String
                        ''texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                        ''        "La evaluación número <b>" + .lblFolio.Text + _
                        ''        "</b> no fue autorizada. <br>" + _
                        ''        "Favor de ingresar a la actividad <b>Corregir Evaluación</b> </span>"
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

                        .btnAutorizar.Enabled = False
                        .btnRechazar.Enabled = False

                        Session("id_actividadM") = 69
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