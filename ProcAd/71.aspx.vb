Public Class _71
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
                        sdaEvaluacion.Dispose()
                        dsEvaluacion.Dispose()

                        limpiarPantalla()

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

    Public Sub limpiarPantalla()
        With Me
            Try
                .litError.Text = ""

                actualizarIndicadores()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub actualizarIndicadores()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaIndicadores As New SqlDataAdapter
                Dim dsIndicadores As New DataSet
                .gvIndicadores.Columns(0).Visible = True
                .gvIndicadores.Columns(1).Visible = True
                .gvIndicadores.DataSource = dsIndicadores
                'Indicadores Registrados y Pendientes
                sdaIndicadores.SelectCommand = New SqlCommand("select id_dt_evaluacion " + _
                                                              "     , null as id_dt_empl_ind " + _
                                                              "     , indicador " + _
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
                .gvIndicadores.Columns(0).Visible = False
                .gvIndicadores.Columns(1).Visible = False
                .gvIndicadores.SelectedIndex = -1

                'Totales
                Dim sdaTotInd As New SqlDataAdapter
                Dim dsTotInd As New DataSet
                sdaTotInd.SelectCommand = New SqlCommand("select format(isnull(sum(cumpl_pond), 0) * 100, '##0.##', 'es_MX') + ' %' as cumpl_pondT " + _
                                                         "     , case " + _
                                                         "         when isnull(sum(cumpl_pond), 0) * 100 >= 100 then 'Sobresaliente' " + _
                                                         "         else case " + _
                                                         "                when isnull(sum(cumpl_pond), 0) * 100 >= 90 and isnull(sum(cumpl_pond), 0) * 100 < 100 then 'Satisfactorio' " + _
                                                         "                else case " + _
                                                         "                       when isnull(sum(cumpl_pond), 0) * 100 >= 66 and isnull(sum(cumpl_pond), 0) * 100 < 90 then 'Necesita Mejorar' " + _
                                                         "                       else case " + _
                                                         "                              when isnull(sum(cumpl_pond), 0) * 100 < 66 then 'No Satisfactorio' " + _
                                                         "                              else '-' " + _
                                                         "                            end " + _
                                                         "                     end " + _
                                                         "              end " + _
                                                         "       end as califInd " + _
                                                         "from dt_evaluacion " + _
                                                         "where id_ms_evaluacion = @id_ms_evaluacion ", ConexionBD)
                sdaTotInd.SelectCommand.Parameters.AddWithValue("@id_ms_evaluacion", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaTotInd.Fill(dsTotInd)
                ConexionBD.Close()
                .lblPorcentCumpl.Text = dsTotInd.Tables(0).Rows(0).Item("cumpl_pondT").ToString()
                .lblCalifCumpl.Text = dsTotInd.Tables(0).Rows(0).Item("califInd").ToString()
                sdaTotInd.Dispose()
                dsTotInd.Dispose()

                .pnlIndicadores.Visible = True
                .pnlIndicador.Visible = False
                .btnAceptar.Visible = True
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarEvidencias()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                'Evidencias
                Dim sdaEvidencias As New SqlDataAdapter
                Dim dsEvidencias As New DataSet
                .gvEvidencia.DataSource = dsEvidencias
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
                .gvEvidencia.DataBind()
                ConexionBD.Close()
                sdaEvidencias.Dispose()
                dsEvidencias.Dispose()

                If .gvEvidencia.Rows.Count > 0 Then
                    .lbl_Evidencias.Visible = True
                    .gvEvidencia.Visible = True
                Else
                    .lbl_Evidencias.Visible = False
                    .gvEvidencia.Visible = False
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Agregar Evidencia"

    Protected Sub cmdAgregarE_Click(sender As Object, e As EventArgs) Handles cmdAgregarE.Click
        Me.litError.Text = ""
        ' '' Ruta Local
        ''Dim sFileDir As String = "C:/ProcAd - Evidencias/" 'Ruta en que se almacenará el archivo
        ' Ruta en Atenea
        Dim sFileDir As String = "D:\ProcAd - Evidencias\" 'Ruta en que se almacenará el archivo
        Dim lMaxFileSize As Long = 10485760 'Equivalente a 10 Megabytes

        'Verificar que el archivo ha sido seleccionado y es un archivo válido
        If (Not fuEvidencias.PostedFile Is Nothing) And (fuEvidencias.PostedFile.ContentLength > 0) Then
            'Determinar el nombre original del archivo
            Dim sFileName As String = System.IO.Path.GetFileName(fuEvidencias.PostedFile.FileName)
            Dim idArchivo As Integer 'Index correspondiente al archivo
            Dim fecha As DateTime = Date.Now
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
            SCMValores.Connection = ConexionBD
            Try
                If fuEvidencias.PostedFile.ContentLength <= lMaxFileSize Then
                    'Registrar el archivo en la base de datos
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into dt_evaluacion_evid(id_ms_evaluacion, evidencia, id_usuario, fecha) values(@id_ms_evaluacion, @evidencia, @id_usuario, @fecha)"
                    SCMValores.Parameters.AddWithValue("@id_ms_evaluacion", Val(Me.lblFolio.Text))
                    SCMValores.Parameters.AddWithValue("@evidencia", sFileName)
                    SCMValores.Parameters.AddWithValue("@id_usuario", Val(Me._txtIdUsuario.Text))
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()
                    'Obtener el Id del archivo
                    SCMValores.CommandText = "select max(id_dt_evaluacion_evid) from dt_evaluacion_evid where (id_usuario = @id_usuario) and (fecha = @fecha)"
                    ConexionBD.Open()
                    idArchivo = SCMValores.ExecuteScalar
                    ConexionBD.Close()
                    'Se agrega el Id al nombre del archivo
                    sFileName = idArchivo.ToString + "EvalE-" + sFileName
                    'Almacenar el archivo en la ruta especificada
                    fuEvidencias.PostedFile.SaveAs(sFileDir + sFileName)
                    'lblMessage.Visible = True
                    'lblMessage.Text = "El archivo: " + sFileName + " se ha adjuntado exitosamente"
                    llenarEvidencias()
                Else
                    'Rechazar el archivo
                    Me.litError.Text = "El archivo excede el límite de 10 MB"
                End If
            Catch exc As Exception    'En caso de error
                'Eliminar el archivo en la base de datos
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "delete from dt_evaluacion_evid where id_dt_evaluacion_evid = @idArchivo"
                SCMValores.Parameters.AddWithValue("@idArchivo", idArchivo)
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                Me.litError.Text = "Un Error ha ocurrido. Favor de intentarlo nuevamente"
            End Try
        Else
            Me.litError.Text = "Favor de seleccionar un Archivo"
        End If
    End Sub

#End Region

#Region "Tabla"

    Protected Sub gvIndicadores_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvIndicadores.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaIndicador As New SqlDataAdapter
                Dim dsIndicador As New DataSet
                If Val(.gvIndicadores.SelectedRow.Cells(0).Text) = 0 Then
                    'No se ha registrado el valor real
                    sdaIndicador.SelectCommand = New SqlCommand("select indicador " + _
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
                                                                "     , format(isnull(ponderacion, 0) * 100, '##0.##', 'es_MX') + ' %' as ponderacion " + _
                                                                "     , format(isnull(meta, 0) * 100, '##0.##', 'es_MX') + ' %' as meta " + _
                                                                "     , -1 as real " + _
                                                                "     , fuente " + _
                                                                "from dt_empl_ind " + _
                                                                "where id_dt_empl_ind = @id_dt_empl_ind " + _
                                                                "  and status = 'A' ", ConexionBD)
                    sdaIndicador.SelectCommand.Parameters.AddWithValue("@id_dt_empl_ind", Val(.gvIndicadores.SelectedRow.Cells(1).Text))
                Else
                    'Se registró el valor y se actualizará
                    sdaIndicador.SelectCommand = New SqlCommand("select indicador " + _
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
                                                                "     , format(isnull(ponderacion, 0) * 100, '##0.##', 'es_MX') + ' %' as ponderacion " + _
                                                                "     , format(isnull(meta, 0) * 100, '##0.##', 'es_MX') + ' %' as meta " + _
                                                                "     , real " + _
                                                                "     , fuente " + _
                                                                "from dt_evaluacion " + _
                                                                "where id_dt_evaluacion = @id_dt_evaluacion ", ConexionBD)
                    sdaIndicador.SelectCommand.Parameters.AddWithValue("@id_dt_evaluacion", Val(.gvIndicadores.SelectedRow.Cells(0).Text))
                End If
                ConexionBD.Open()
                sdaIndicador.Fill(dsIndicador)
                ConexionBD.Close()
                .lblIndicador.Text = dsIndicador.Tables(0).Rows(0).Item("indicador").ToString()
                .lblMesEvalI.Text = dsIndicador.Tables(0).Rows(0).Item("mes_eval").ToString()
                .lblTipoIndI.Text = dsIndicador.Tables(0).Rows(0).Item("tipo_indicador").ToString()
                .lblPondIndI.Text = dsIndicador.Tables(0).Rows(0).Item("ponderacion").ToString()
                .lblMetaI.Text = dsIndicador.Tables(0).Rows(0).Item("meta").ToString()
                If Val(dsIndicador.Tables(0).Rows(0).Item("real").ToString()) = -1 Then
                    'Vacío 
                    .wpeReal.Text = ""
                Else
                    'Con valor
                    .wpeReal.Value = Val(dsIndicador.Tables(0).Rows(0).Item("real").ToString())
                End If
                .lblFuenteI.Text = dsIndicador.Tables(0).Rows(0).Item("fuente").ToString()
                sdaIndicador.Dispose()
                dsIndicador.Dispose()

                .pnlIndicadores.Visible = False
                .pnlIndicador.Visible = True
                .btnAceptar.Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Aceptar Detalle Indicador"

    Protected Sub btnAceptarI_Click(sender As Object, e As EventArgs) Handles btnAceptarI.Click
        With Me
            Try
                .litError.Text = ""

                If .wpeReal.Text = "" Then
                    .litError.Text = "Favor de indicar el porcentaje Real alcanzado"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim sdaIndicador As New SqlDataAdapter
                    Dim dsIndicador As New DataSet
                    If Val(.gvIndicadores.SelectedRow.Cells(0).Text) = 0 Then
                        'No se ha registrado el valor real
                        sdaIndicador.SelectCommand = New SqlCommand("select indicador " + _
                                                                    "     , tipo_indicador " + _
                                                                    "     , ponderacion " + _
                                                                    "     , meta " + _
                                                                    "     , @real as real " + _
                                                                    "     , case tipo_indicador " + _
                                                                    "         when 'Ascendente' then case when @real < meta then 0 " + _
                                                                    "                                     else case when ((@real - meta) / meta) * ponderacion + ponderacion <= ponderacion then ((@real - meta) / meta) * ponderacion + ponderacion " + _
                                                                    "                                            else ponderacion " + _
                                                                    "                                          end " + _
                                                                    "                                end " + _
                                                                    "         when 'Descendente' then case when @real > meta then 0 " + _
                                                                    "                                      else case when ((meta - @real) / meta) * ponderacion + ponderacion <= ponderacion then ((meta - @real) / meta) * ponderacion + ponderacion " + _
                                                                    "                                             else ponderacion " + _
                                                                    "                                           end " + _
                                                                    "                                 end " + _
                                                                    "         else 0 " + _
                                                                    "       end as cumplimiento " + _
                                                                    "     , fuente " + _
                                                                    "     , formula " + _
                                                                    "from dt_empl_ind " + _
                                                                    "where id_dt_empl_ind = @id_dt_empl_ind " + _
                                                                    "  and status = 'A' ", ConexionBD)
                        sdaIndicador.SelectCommand.Parameters.AddWithValue("@id_dt_empl_ind", Val(.gvIndicadores.SelectedRow.Cells(1).Text))
                    Else
                        'Se registro el valor y se actualizará
                        sdaIndicador.SelectCommand = New SqlCommand("select indicador " + _
                                                                    "     , tipo_indicador " + _
                                                                    "     , ponderacion " + _
                                                                    "     , meta " + _
                                                                    "     , @real as real " + _
                                                                    "     , case tipo_indicador " + _
                                                                    "         when 'Ascendente' then case when @real < meta then 0 " + _
                                                                    "                                     else case when ((@real - meta) / meta) * ponderacion + ponderacion <= ponderacion then ((@real - meta) / meta) * ponderacion + ponderacion " + _
                                                                    "                                            else ponderacion " + _
                                                                    "                                          end " + _
                                                                    "                                end " + _
                                                                    "         when 'Descendente' then case when @real > meta then 0 " + _
                                                                    "                                      else case when ((meta - @real) / meta) * ponderacion + ponderacion <= ponderacion then ((meta - @real) / meta) * ponderacion + ponderacion " + _
                                                                    "                                             else ponderacion " + _
                                                                    "                                           end " + _
                                                                    "                                 end " + _
                                                                    "         else 0 " + _
                                                                    "       end as cumplimiento " + _
                                                                    "     , fuente " + _
                                                                    "     , formula " + _
                                                                    "from dt_evaluacion " + _
                                                                    "where id_dt_evaluacion = @id_dt_evaluacion ", ConexionBD)
                        sdaIndicador.SelectCommand.Parameters.AddWithValue("@id_dt_evaluacion", Val(.gvIndicadores.SelectedRow.Cells(0).Text))
                    End If
                    sdaIndicador.SelectCommand.Parameters.AddWithValue("@real", .wpeReal.Value)
                    ConexionBD.Open()
                    sdaIndicador.Fill(dsIndicador)
                    ConexionBD.Close()

                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    If Val(.gvIndicadores.SelectedRow.Cells(0).Text) = 0 Then
                        'Insertar
                        SCMValores.CommandText = "insert into dt_evaluacion( id_ms_evaluacion,  indicador,  tipo_indicador,  ponderacion,  meta,  real,  cumpl_pond,  fuente,  formula,  id_usr_reg) " + _
                                                 "                   values(                0, @indicador, @tipo_indicador, @ponderacion, @meta, @real, @cumpl_pond, @fuente, @formula, @id_usr_reg) "
                    Else
                        'Actualizar
                        SCMValores.CommandText = "update dt_evaluacion " + _
                                                 "  set indicador = @indicador, tipo_indicador = @tipo_indicador, ponderacion = @ponderacion, meta = @meta " + _
                                                 "    , real = @real, cumpl_pond = @cumpl_pond, fuente = @fuente, formula = @formula, id_usr_reg = @id_usr_reg " + _
                                                 "where id_dt_evaluacion = @id_dt_evaluacion "
                        SCMValores.Parameters.AddWithValue("@id_dt_evaluacion", Val(.gvIndicadores.SelectedRow.Cells(0).Text))
                    End If
                    SCMValores.Parameters.AddWithValue("@indicador", dsIndicador.Tables(0).Rows(0).Item("indicador").ToString())
                    SCMValores.Parameters.AddWithValue("@tipo_indicador", dsIndicador.Tables(0).Rows(0).Item("tipo_indicador").ToString())
                    SCMValores.Parameters.AddWithValue("@ponderacion", Val(dsIndicador.Tables(0).Rows(0).Item("ponderacion").ToString()))
                    SCMValores.Parameters.AddWithValue("@meta", Val(dsIndicador.Tables(0).Rows(0).Item("meta").ToString()))
                    SCMValores.Parameters.AddWithValue("@real", Val(dsIndicador.Tables(0).Rows(0).Item("real").ToString()))
                    SCMValores.Parameters.AddWithValue("@cumpl_pond", Val(dsIndicador.Tables(0).Rows(0).Item("cumplimiento").ToString()))
                    SCMValores.Parameters.AddWithValue("@fuente", dsIndicador.Tables(0).Rows(0).Item("fuente").ToString())
                    SCMValores.Parameters.AddWithValue("@formula", dsIndicador.Tables(0).Rows(0).Item("formula").ToString())
                    SCMValores.Parameters.AddWithValue("@id_usr_reg", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    sdaIndicador.Dispose()
                    dsIndicador.Dispose()

                    actualizarIndicadores()
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Re enviar a Aprobación"

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim fecha As DateTime = Date.Now
                While Val(._txtBan.Text) = 0
                    'Totales
                    Dim sdaTotInd As New SqlDataAdapter
                    Dim dsTotInd As New DataSet
                    sdaTotInd.SelectCommand = New SqlCommand("select isnull(sum(cumpl_pond), 0) as cumpl_pondT " + _
                                                             "     , case " + _
                                                             "         when isnull(sum(cumpl_pond), 0) >= .66 then 'S' " + _
                                                             "         else 'N' " + _
                                                             "       end as cobra_bono_asist " + _
                                                             "from dt_evaluacion " + _
                                                             "where id_ms_evaluacion = @id_ms_evaluacion ", ConexionBD)
                    sdaTotInd.SelectCommand.Parameters.AddWithValue("@id_ms_evaluacion", Val(.lblFolio.Text))
                    ConexionBD.Open()
                    sdaTotInd.Fill(dsTotInd)
                    ConexionBD.Close()

                    'Actualizar datos de la Evaluación
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_evaluacion set com_registro = @com_registro, com_valida = @com_valida, porcent_cumpl = @porcent_cumpl, cobra_bono_asist = @cobra_bono_asist, cobra_bono_cumpl_UN = @cobra_bono_asist, status = 'P' where id_ms_evaluacion = @id_ms_evaluacion "
                    If .txtComentarioC.Text.Trim = "" Then
                        SCMValores.Parameters.AddWithValue("@com_registro", DBNull.Value)
                    Else
                        SCMValores.Parameters.AddWithValue("@com_registro", .txtComentarioC.Text.Trim)
                    End If
                    If .txtComentarioL.Text.Trim = "" Then
                        SCMValores.Parameters.AddWithValue("@com_valida", DBNull.Value)
                    Else
                        SCMValores.Parameters.AddWithValue("@com_valida", .txtComentarioL.Text.Trim)
                    End If
                    SCMValores.Parameters.AddWithValue("@porcent_cumpl", Val(dsTotInd.Tables(0).Rows(0).Item("cumpl_pondT").ToString()))
                    SCMValores.Parameters.AddWithValue("@cobra_bono_asist", dsTotInd.Tables(0).Rows(0).Item("cobra_bono_asist").ToString())
                    SCMValores.Parameters.AddWithValue("@id_ms_evaluacion", Val(.lblFolio.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    sdaTotInd.Dispose()
                    dsTotInd.Dispose()

                    'Actualizar Instancia
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 69)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Registrar en Histórico
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                             "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 69)
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    ._txtBan.Text = 1

                    .pnlInicio.Enabled = False

                    Session("id_actividadM") = 71
                    Session("TipoM") = "Eval"
                End While
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class