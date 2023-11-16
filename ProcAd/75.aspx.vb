Public Class _75
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 5
                    'Session("idMsInst") = 41116

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtIdMsInst.Text = Session("idMsInst")
                        ._txtBan.Text = 0

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        'Datos de la Evaluación
                        Dim sdaEvaluacion As New SqlDataAdapter
                        Dim dsEvaluacion As New DataSet
                        sdaEvaluacion.SelectCommand = New SqlCommand("select id_ms_evaluacionA " + _
                                                                     "     , ms_evaluacionA.area " + _
                                                                     "     , ms_evaluacionA.id_dt_area " + _
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
                                                                     "     , isnull(com_registro, '') as com_registro " + _
                                                                     "     , isnull(com_valida, '') as com_valida " + _
                                                                     "from ms_instancia " + _
                                                                     "  left join ms_evaluacionA on ms_instancia.id_ms_sol = ms_evaluaciona.id_ms_evaluacionA and ms_instancia.tipo = 'EvalA' " + _
                                                                     "where ms_instancia.id_ms_instancia = @idMsInst ", ConexionBD)
                        sdaEvaluacion.SelectCommand.Parameters.AddWithValue("@idMsInst", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaEvaluacion.Fill(dsEvaluacion)
                        ConexionBD.Close()
                        ._txtIdMsEvaluacionA.Text = dsEvaluacion.Tables(0).Rows(0).Item("id_ms_evaluacionA").ToString()
                        ._txtIdDtArea.Text = dsEvaluacion.Tables(0).Rows(0).Item("id_dt_area").ToString()
                        .lblArea.Text = dsEvaluacion.Tables(0).Rows(0).Item("area").ToString()
                        .lblLider.Text = dsEvaluacion.Tables(0).Rows(0).Item("lider").ToString()
                        .lblPuestoLider.Text = dsEvaluacion.Tables(0).Rows(0).Item("puesto_lider").ToString()
                        .lblMesEval.Text = dsEvaluacion.Tables(0).Rows(0).Item("mes_eval").ToString()
                        .txtComentarioL.Text = dsEvaluacion.Tables(0).Rows(0).Item("com_registro").ToString()
                        .txtComentarioV.Text = dsEvaluacion.Tables(0).Rows(0).Item("com_valida").ToString()
                        sdaEvaluacion.Dispose()
                        dsEvaluacion.Dispose()

                        'Evaluaciones Autorizadas
                        Dim sdaEvaluaciones As New SqlDataAdapter
                        Dim dsEvaluaciones As New DataSet
                        .gvEvaluaciones.DataSource = dsEvaluaciones
                        sdaEvaluaciones.SelectCommand = New SqlCommand("select no_empleado " + _
                                                                       "     , nombre + ' ' + ap_paterno + ' ' + ap_materno as colaborador " + _
                                                                       "     , empresa " + _
                                                                       "     , centro_costo " + _
                                                                       "     , puesto " + _
                                                                       "     , base " + _
                                                                       "     , porcent_cumpl " + _
                                                                       "     , case cobra_bono_asist when 'S' then 'Sí' when 'N' then 'No' else '-' end as cobra_bono_asist " + _
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
                .gvEvidencia.DataSource = dsEvidencias
                sdaEvidencias.SelectCommand = New SqlCommand("select evidencia " + _
                                                             "     , 'http://148.223.153.43/ProcAd - Evidencias/' + cast(id_dt_evaluacion_evid as varchar(20)) + 'EvalE-' + evidencia as path " + _
                                                             "from dt_evaluacion_evid " + _
                                                             "where id_ms_evaluacionA = @id_ms_evaluacionA ", ConexionBD)
                sdaEvidencias.SelectCommand.Parameters.AddWithValue("@id_ms_evaluacionA", Val(._txtIdMsEvaluacionA.Text))
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
                    SCMValores.CommandText = "insert into dt_evaluacion_evid(id_ms_evaluacionA, evidencia, id_usuario, fecha) values(@id_ms_evaluacionA, @evidencia, @id_usuario, @fecha)"
                    SCMValores.Parameters.AddWithValue("@id_ms_evaluacionA", Val(Me._txtIdMsEvaluacionA.Text))
                    SCMValores.Parameters.AddWithValue("@evidencia", sFileName)
                    SCMValores.Parameters.AddWithValue("@id_usuario", Val(Me._txtIdUsuario.Text))
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()
                    'Obtener el Id del archivo
                    SCMValores.CommandText = "select max(id_dt_evaluacion_evid) from dt_evaluacion_evid where (id_ms_evaluacionA = @id_ms_evaluacionA) and (fecha = @fecha)"
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
                    'Actualizar Estatus de las Evaluaciones
                    SCMValores.CommandText = "update ms_evaluacion  " + _
                                             "  set status = 'PVA' " + _
                                             "where id_dt_area = @id_dt_area " + _
                                             "  and status = 'PCA' " + _
                                             "  and mes_eval = month((select cast(valor as datetime) " + _
                                             "                        from cg_parametros " + _
                                             "                        where parametro = 'mes_eval')) " + _
                                             "  and año_eval = year((select cast(valor as datetime) " + _
                                             "                       from cg_parametros " + _
                                             "                       where parametro = 'mes_eval')) "
                    SCMValores.Parameters.AddWithValue("@id_dt_area", Val(._txtIdDtArea.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Actualizar datos de la Evaluación
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_evaluacionA set com_registro = @com_registro, status = 'PVA' where id_ms_evaluacionA = @id_ms_evaluacionA "
                    If .txtComentarioL.Text.Trim = "" Then
                        SCMValores.Parameters.AddWithValue("@com_registro", DBNull.Value)
                    Else
                        SCMValores.Parameters.AddWithValue("@com_registro", .txtComentarioL.Text.Trim)
                    End If
                    SCMValores.Parameters.AddWithValue("@id_ms_evaluacionA", Val(._txtIdMsEvaluacionA.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Actualizar Instancia
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_instancia set id_actividad = @id_actividad where id_ms_instancia = @id_ms_instancia"
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 72)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Registrar en Histórico
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into ms_historico( id_ms_instancia, id_actividad, fecha, id_usr) " + _
                                             "                 values (@id_ms_instancia,@id_actividad,@fecha,@id_usr) "
                    SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                    SCMValores.Parameters.AddWithValue("@id_actividad", 72)
                    SCMValores.Parameters.AddWithValue("@fecha", fecha)
                    SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    ._txtBan.Text = 1

                    .pnlInicio.Enabled = False

                    Session("id_actividadM") = 75
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