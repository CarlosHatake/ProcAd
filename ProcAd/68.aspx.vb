Public Class _68
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 14

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")

                        llenarColaboradores()
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

    Public Sub llenarColaboradores()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaColaborador As New SqlDataAdapter
                Dim dsColaborador As New DataSet
                .ddlColaborador.DataSource = dsColaborador
                'Catálogo de Colaboradores
                sdaColaborador.SelectCommand = New SqlCommand("select id_dt_empleado " + _
                                                              "     , dt_empleado.nombre as empleado " + _
                                                              "from dt_empleado " + _
                                                              "  left join ms_evaluacion on dt_empleado.no_empleado = ms_evaluacion.no_empleado " + _
                                                              "                         and mes_eval = month((select cast(valor as datetime) " + _
                                                              "                                               from cg_parametros " + _
                                                              "                                               where parametro = 'mes_eval')) " + _
                                                              "                         and año_eval = year((select cast(valor as datetime) " + _
                                                              "                                              from cg_parametros " + _
                                                              "                                              where parametro = 'mes_eval')) " + _
                                                              "where id_usr_evalua = @idUsuario " + _
                                                              "  and (select count(*) from dt_empl_ind where dt_empl_ind.id_dt_empleado = dt_empleado.id_dt_empleado and status = 'A') > 0 " + _
                                                              "  and dt_empleado.status = 'A' " + _
                                                              "  and (select cast(valor as datetime) " + _
                                                              "       from cg_parametros " + _
                                                              "       where parametro = 'mes_eval') >= dateadd(mm, 2, cast(cast(datepart(year, fecha_ingreso) as varchar(4)) + '/' + cast(datepart(month, fecha_ingreso) as varchar(2)) + '/01' as date)) " + _
                                                              "  and id_ms_evaluacion is null " + _
                                                              "order by empleado ", ConexionBD)
                sdaColaborador.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                .ddlColaborador.DataTextField = "empleado"
                .ddlColaborador.DataValueField = "id_dt_empleado"
                ConexionBD.Open()
                sdaColaborador.Fill(dsColaborador)
                .ddlColaborador.DataBind()
                ConexionBD.Close()
                sdaColaborador.Dispose()
                dsColaborador.Dispose()
                .ddlColaborador.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub limpiarPantalla()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim ConexionBDNom As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBDNom.ConnectionString = accessDB.conBD("NOM")

                'Borrar Registros Previos no Almacenados
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "delete from dt_evaluacion " + _
                                         "where id_ms_evaluacion = 0 " + _
                                         "  and id_usr_reg = @id_usr_reg "
                SCMValores.Parameters.AddWithValue("@id_usr_reg", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                SCMValores.CommandText = "delete from dt_evaluacion_evid " + _
                                         "where id_ms_evaluacion = 0 " + _
                                         "  and id_usuario = @id_usr_reg "
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                .lblFolio.Text = ""

                'Datos de Empleado ProcAd
                Dim sdaEmpleado As New SqlDataAdapter
                Dim dsEmpleado As New DataSet
                sdaEmpleado.SelectCommand = New SqlCommand("select dt_empleado.nombre as empleado " + _
                                                           "     , dt_empleado.no_empleado " + _
                                                           "     , area " + _
                                                           "     , case when dt_empleado.nombre = nombre_aut then nombre_dir else nombre_aut end as lider " + _
                                                           "     , case when dt_empleado.nombre = nombre_aut then no_empleado_dir else no_empleado_aut end as no_empleado_lid " + _
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
                                                           "     , (select format(isnull(sum(ponderacion), 0) * 100, '##0.##', 'es_MX') + ' %' " + _
                                                           "        from dt_empl_ind " + _
                                                           "          left join dt_empleado on dt_empl_ind.id_dt_empleado = dt_empleado.id_dt_empleado " + _
                                                           "        where dt_empl_ind.status = 'A' " + _
                                                           "          and dt_empl_ind.id_dt_empleado = @id_dt_empleado) as pondT " + _
                                                           "from dt_empleado " + _
                                                           "  left join dt_area on dt_empleado.id_dt_area = dt_area.id_dt_area " + _
                                                           "  left join cg_direccion on dt_area.id_direccion = cg_direccion.id_direccion " + _
                                                           "where dt_empleado.id_dt_empleado = @id_dt_empleado ", ConexionBD)
                sdaEmpleado.SelectCommand.Parameters.AddWithValue("@id_dt_empleado", .ddlColaborador.SelectedValue)
                ConexionBD.Open()
                sdaEmpleado.Fill(dsEmpleado)
                ConexionBD.Close()
                'Datos de Empleado NOM
                Dim sdaEmpleadoNom As New SqlDataAdapter
                Dim dsEmpleadoNom As New DataSet
                sdaEmpleadoNom.SelectCommand = New SqlCommand("select rtrim(ltrim(isnull(nompues.despue,''))) as puesto " + _
                                                              "     , rtrim(ltrim(isnull(nompuesL.despue,''))) as puestoL " + _
                                                              "from nomtrab " + _
                                                              "  left join nompues on nomtrab.cvecia = nompues.cvecia and nomtrab.cvepue = nompues.cvepue " + _
                                                              "  left join nomtrab nomtrabL on nomtrabL.cvetra = @no_empleado_lid and nomtrabL.status = 'A' " + _
                                                              "  left join nompues nompuesL on nomtrabL.cvecia = nompuesL.cvecia and nomtrabL.cvepue = nompuesL.cvepue " + _
                                                              "where nomtrab.status = 'A' " + _
                                                              "  and nomtrab.cvetra = @no_empleado ", ConexionBDNom)
                sdaEmpleadoNom.SelectCommand.Parameters.AddWithValue("@no_empleado", dsEmpleado.Tables(0).Rows(0).Item("no_empleado").ToString())
                sdaEmpleadoNom.SelectCommand.Parameters.AddWithValue("@no_empleado_lid", dsEmpleado.Tables(0).Rows(0).Item("no_empleado_lid").ToString())
                ConexionBD.Open()
                sdaEmpleadoNom.Fill(dsEmpleadoNom)
                ConexionBD.Close()
                '                        .lblColaborador.Text = dsEmpleado.Tables(0).Rows(0).Item("empleado").ToString()
                .lblPuestoEval.Text = dsEmpleadoNom.Tables(0).Rows(0).Item("puesto").ToString()
                .lblArea.Text = dsEmpleado.Tables(0).Rows(0).Item("area").ToString()
                .lblLider.Text = dsEmpleado.Tables(0).Rows(0).Item("lider").ToString()
                .lblPuestoLider.Text = dsEmpleadoNom.Tables(0).Rows(0).Item("puestoL").ToString()
                .lblMesEval.Text = dsEmpleado.Tables(0).Rows(0).Item("mes_eval").ToString()
                .lblPondTotal.Text = dsEmpleado.Tables(0).Rows(0).Item("pondT").ToString()
                .lblMesEvalI.Text = dsEmpleado.Tables(0).Rows(0).Item("mes_eval").ToString()
                sdaEmpleadoNom.Dispose()
                dsEmpleadoNom.Dispose()
                sdaEmpleado.Dispose()
                dsEmpleado.Dispose()

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
                                                              "where id_ms_evaluacion = 0 " + _
                                                              "  and id_usr_reg = @id_usuario " + _
                                                              "union " + _
                                                              "select 0 as id_dt_evaluacion " + _
                                                              "     , id_dt_empl_ind " + _
                                                              "     , indicador " + _
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
                                                              "     , null as real " + _
                                                              "     , null as cumpl_pond " + _
                                                              "     , fuente " + _
                                                              "from dt_empl_ind " + _
                                                              "  left join dt_empleado on dt_empl_ind.id_dt_empleado = dt_empleado.id_dt_empleado " + _
                                                              "where dt_empl_ind.status = 'A' " + _
                                                              "  and dt_empl_ind.id_dt_empleado = @id_dt_empleado " + _
                                                              "  and indicador not in (select dt_evaluacion.indicador " + _
                                                              "                        from dt_evaluacion " + _
                                                              "                        where dt_evaluacion.id_usr_reg = @id_usuario " + _
                                                              "                          and dt_evaluacion.id_ms_evaluacion = 0) " + _
                                                              "order by indicador ", ConexionBD)
                sdaIndicadores.SelectCommand.Parameters.AddWithValue("@id_dt_empleado", .ddlColaborador.SelectedValue)
                sdaIndicadores.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
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
                                                         "     , (select count(*) " + _
                                                         "        from dt_empl_ind " + _
                                                         "          left join dt_empleado on dt_empl_ind.id_dt_empleado = dt_empleado.id_dt_empleado " + _
                                                         "        where dt_empl_ind.status = 'A' " + _
                                                         "          and dt_empl_ind.id_dt_empleado = @id_dt_empleado " + _
                                                         "          and indicador not in (select dt_evaluacion.indicador " + _
                                                         "                                from dt_evaluacion " + _
                                                         "                                where dt_evaluacion.id_usr_reg = @id_usuario " + _
                                                         "                                  and dt_evaluacion.id_ms_evaluacion = 0)) as indPend " + _
                                                         "from dt_evaluacion " + _
                                                         "where id_ms_evaluacion = 0 " + _
                                                         "    and id_usr_reg = @id_usuario ", ConexionBD)
                sdaTotInd.SelectCommand.Parameters.AddWithValue("@id_dt_empleado", .ddlColaborador.SelectedValue)
                sdaTotInd.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                sdaTotInd.Fill(dsTotInd)
                ConexionBD.Close()
                .lblPorcentCumpl.Text = dsTotInd.Tables(0).Rows(0).Item("cumpl_pondT").ToString()
                .lblCalifCumpl.Text = dsTotInd.Tables(0).Rows(0).Item("califInd").ToString()
                If Val(dsTotInd.Tables(0).Rows(0).Item("indPend").ToString()) = 0 Then
                    .btnAceptar.Visible = True
                    .btnNuevo.Visible = True
                    .btnNuevo.Enabled = False
                Else
                    .btnAceptar.Visible = False
                    .btnNuevo.Visible = False
                End If
                sdaTotInd.Dispose()
                dsTotInd.Dispose()

                'Limpiar Comentarios
                .txtComentarioC.Text = ""
                .txtComentarioL.Text = ""

                .pnlIndicadores.Visible = True
                .pnlIndicador.Visible = False
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
                                                             "where (id_ms_evaluacion = 0 and id_usuario = @id_usuario) " + _
                                                             "   or (id_ms_evaluacion in (select id_ms_evaluacion " + _
                                                             "                            from dt_empleado dtEmpl " + _
                                                             "                              left join dt_empleado dtEmplA on dtEmpl.id_dt_area = dtEmplA.id_dt_area " + _
                                                             "                              left join ms_evaluacion on dtEmplA.no_empleado = ms_evaluacion.no_empleado " + _
                                                             "                                                        and mes_eval = month((select cast(valor as datetime) " + _
                                                             "                                                                              from cg_parametros " + _
                                                             "                                                                              where parametro = 'mes_eval')) " + _
                                                             "                                                        and año_eval = year((select cast(valor as datetime) " + _
                                                             "                                                                             from cg_parametros " + _
                                                             "                                                                             where parametro = 'mes_eval')) " + _
                                                             "                            where dtEmpl.id_dt_empleado = @id_dt_empleado " + _
                                                             "                              and dtEmplA.status = 'A' " + _
                                                             "                              and id_ms_evaluacion is not null)) ", ConexionBD)
                sdaEvidencias.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(Me._txtIdUsuario.Text))
                sdaEvidencias.SelectCommand.Parameters.AddWithValue("@id_dt_empleado", .ddlColaborador.SelectedValue)
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
                    SCMValores.Parameters.AddWithValue("@id_ms_evaluacion", 0)
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

    Protected Sub ddlColaborador_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlColaborador.SelectedIndexChanged
        limpiarPantalla()
    End Sub

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

#Region "Guardar Solicitud"

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""

                'Datos Empleado ProcAd
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaEmpleadoPro As New SqlDataAdapter
                Dim dsEmpleadoPro As New DataSet
                sdaEmpleadoPro.SelectCommand = New SqlCommand("select no_empleado " + _
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
                sdaEmpleadoPro.SelectCommand.Parameters.AddWithValue("@id_dt_empleado", .ddlColaborador.SelectedValue)
                ConexionBD.Open()
                sdaEmpleadoPro.Fill(dsEmpleadoPro)
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
                sdaEmpleadoNom.SelectCommand.Parameters.AddWithValue("@no_empleado", dsEmpleadoPro.Tables(0).Rows(0).Item("no_empleado").ToString())
                sdaEmpleadoNom.SelectCommand.Parameters.AddWithValue("@no_empleado_lid", dsEmpleadoPro.Tables(0).Rows(0).Item("no_empleadoAut").ToString())
                ConexionBD.Open()
                sdaEmpleadoNom.Fill(dsEmpleadoNom)
                ConexionBD.Close()

                'Totales
                Dim sdaTotInd As New SqlDataAdapter
                Dim dsTotInd As New DataSet
                sdaTotInd.SelectCommand = New SqlCommand("select isnull(sum(cumpl_pond), 0) as cumpl_pondT " + _
                                                         "     , case " + _
                                                         "         when isnull(sum(cumpl_pond), 0) >= .66 then 'S' " + _
                                                         "         else 'N' " + _
                                                         "       end as cobra_bono_asist " + _
                                                         "from dt_evaluacion " + _
                                                         "where id_ms_evaluacion = 0 " + _
                                                         "    and id_usr_reg = @id_usuario ", ConexionBD)
                sdaTotInd.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                sdaTotInd.Fill(dsTotInd)
                ConexionBD.Close()

                Dim fecha As DateTime
                fecha = Date.Now
                'Insertar en ms_evaluación
                Dim idUsrValida As Integer
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "insert into ms_evaluacion(  no_empleado,  nombre,  ap_paterno,  ap_materno,  empresa,  centro_costo,  puesto,  base,  unidad_neg,  direccion,  id_dt_area,  area,  lider,  puesto_lider,  año_eval,  mes_eval,  porcent_cumpl,  cobra_bono_asist, cobra_bono_cumpl_UN,   id_usr_registro,  fecha_registro,  com_registro,  id_usr_valida,  com_valida,  id_usr_director,  status) " + _
                                         "                   values( @no_empleado, @nombre, @ap_paterno, @ap_materno, @empresa, @centro_costo, @puesto, @base, @unidad_neg, @direccion, @id_dt_area, @area, @lider, @puesto_lider, @año_eval, @mes_eval, @porcent_cumpl, @cobra_bono_asist,   @cobra_bono_asist,  @id_usr_registro, @fecha_registro, @com_registro, @id_usr_valida, @com_valida, @id_usr_director, @status) "
                SCMValores.Parameters.AddWithValue("@no_empleado", dsEmpleadoPro.Tables(0).Rows(0).Item("no_empleado").ToString())
                SCMValores.Parameters.AddWithValue("@nombre", dsEmpleadoNom.Tables(0).Rows(0).Item("nombre").ToString())
                SCMValores.Parameters.AddWithValue("@ap_paterno", dsEmpleadoNom.Tables(0).Rows(0).Item("ap_paterno").ToString())
                SCMValores.Parameters.AddWithValue("@ap_materno", dsEmpleadoNom.Tables(0).Rows(0).Item("ap_materno").ToString())
                SCMValores.Parameters.AddWithValue("@empresa", dsEmpleadoPro.Tables(0).Rows(0).Item("empresa").ToString())
                SCMValores.Parameters.AddWithValue("@centro_costo", dsEmpleadoNom.Tables(0).Rows(0).Item("centro_costo").ToString())
                SCMValores.Parameters.AddWithValue("@puesto", dsEmpleadoNom.Tables(0).Rows(0).Item("puesto").ToString())
                SCMValores.Parameters.AddWithValue("@base", dsEmpleadoNom.Tables(0).Rows(0).Item("base").ToString())
                SCMValores.Parameters.AddWithValue("@unidad_neg", dsEmpleadoPro.Tables(0).Rows(0).Item("unidad_neg").ToString())
                SCMValores.Parameters.AddWithValue("@direccion", dsEmpleadoPro.Tables(0).Rows(0).Item("direccion").ToString())
                SCMValores.Parameters.AddWithValue("@id_dt_area", dsEmpleadoPro.Tables(0).Rows(0).Item("id_dt_area").ToString())
                SCMValores.Parameters.AddWithValue("@area", dsEmpleadoPro.Tables(0).Rows(0).Item("area").ToString())
                SCMValores.Parameters.AddWithValue("@lider", dsEmpleadoNom.Tables(0).Rows(0).Item("lider").ToString())
                SCMValores.Parameters.AddWithValue("@puesto_lider", dsEmpleadoNom.Tables(0).Rows(0).Item("puestoL").ToString())
                SCMValores.Parameters.AddWithValue("@año_eval", Val(dsEmpleadoPro.Tables(0).Rows(0).Item("año_eval").ToString()))
                SCMValores.Parameters.AddWithValue("@mes_eval", Val(dsEmpleadoPro.Tables(0).Rows(0).Item("mes_eval").ToString()))
                SCMValores.Parameters.AddWithValue("@porcent_cumpl", Val(dsTotInd.Tables(0).Rows(0).Item("cumpl_pondT").ToString()))
                SCMValores.Parameters.AddWithValue("@cobra_bono_asist", dsTotInd.Tables(0).Rows(0).Item("cobra_bono_asist").ToString())
                SCMValores.Parameters.AddWithValue("@id_usr_registro", Val(._txtIdUsuario.Text))
                SCMValores.Parameters.AddWithValue("@fecha_registro", fecha)
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
                idUsrValida = Val(dsEmpleadoPro.Tables(0).Rows(0).Item("id_usrVal").ToString())
                If idUsrValida = 0 Then
                    SCMValores.Parameters.AddWithValue("@id_usr_valida", DBNull.Value)
                    SCMValores.Parameters.AddWithValue("@status", "V")
                Else
                    SCMValores.Parameters.AddWithValue("@id_usr_valida", idUsrValida)
                    SCMValores.Parameters.AddWithValue("@status", "P")
                End If
                SCMValores.Parameters.AddWithValue("@id_usr_director", Val(dsEmpleadoPro.Tables(0).Rows(0).Item("id_usr_dir").ToString()))

                ._txtIdDtArea.Text = dsEmpleadoPro.Tables(0).Rows(0).Item("id_dt_area").ToString()

                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                sdaTotInd.Dispose()
                dsTotInd.Dispose()
                sdaEmpleadoNom.Dispose()
                dsEmpleadoNom.Dispose()

                'Obtener id_ms_evaluacion
                SCMValores.CommandText = "select max(id_ms_evaluacion) " + _
                                         "from ms_evaluacion " + _
                                         "where no_empleado = @no_empleado " + _
                                         "  and año_eval = @año_eval " + _
                                         "  and mes_eval = @mes_eval "
                ConexionBD.Open()
                .lblFolio.Text = SCMValores.ExecuteScalar()
                ConexionBD.Close()

                'Actualizar dt_evaluacion
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "update dt_evaluacion " + _
                                         " set id_ms_evaluacion = @id_ms_evaluacion " + _
                                         "where id_ms_evaluacion = 0 " + _
                                         "  and id_usr_reg = @id_usuario "
                SCMValores.Parameters.AddWithValue("@id_ms_evaluacion", Val(.lblFolio.Text))
                SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                'Actualizar Evidencias
                SCMValores.CommandText = "update dt_evaluacion_evid " + _
                                         " set id_ms_evaluacion = @id_ms_evaluacion " + _
                                         "where id_ms_evaluacion = 0 " + _
                                         "  and id_usuario = @id_usuario "
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                Dim idActividad As Integer
                If idUsrValida = 0 Then
                    idActividad = 70
                Else
                    idActividad = 69
                End If

                'Insertar Instancia de la Evaluación
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "insert into ms_instancia ( id_ms_sol,  tipo,  id_actividad) " + _
                                         "				    values (@id_ms_sol, @tipo, @id_actividad) "
                SCMValores.Parameters.AddWithValue("@id_ms_sol", Val(.lblFolio.Text))
                SCMValores.Parameters.AddWithValue("@tipo", "Eval")
                SCMValores.Parameters.AddWithValue("@id_actividad", idActividad)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()
                'Obtener ID de la Instancia de Solicitud
                SCMValores.CommandText = "select max(id_ms_instancia) from ms_instancia where id_ms_sol = @id_ms_sol and tipo = 'Eval' "
                ConexionBD.Open()
                ._txtIdMsInst.Text = SCMValores.ExecuteScalar
                ConexionBD.Close()
                'Insertar Históricos
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "insert into ms_historico ( id_ms_instancia,  id_actividad,  id_usr,  fecha) " + _
                                         "				    values (@id_ms_instancia, @id_actividad, @id_usr, @fecha) "
                SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                SCMValores.Parameters.AddWithValue("@id_actividad", idActividad)
                SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                SCMValores.Parameters.AddWithValue("@fecha", fecha)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                ' ''Envío de Correo
                ''Dim Mensaje As New System.Net.Mail.MailMessage()
                ''Dim destinatario As String = ""
                ' ''Obtener el Correos del Autorizador
                ''SCMValores.CommandText = "select cgEmpl.correo from bd_empleado.dbo.cg_empleado cgEmpl where cgEmpl.id_empleado = @idAut "
                ''SCMValores.Parameters.AddWithValue("@idAut", Val(dsEmpleadoPro.Tables(0).Rows(0).Item("id_empleado_aut").ToString()))
                ''ConexionBD.Open()
                ''destinatario = SCMValores.ExecuteScalar()
                ''ConexionBD.Close()

                ''Mensaje.[To].Add(destinatario)
                ''Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                ''Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                ''Mensaje.Subject = "ProcAd - Registro de Evaluación No. " + .lblFolio.Text + " por Autorizar"
                ''Dim texto As String
                ''texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                ''        "Se registró la evaluación número <b>" + .lblFolio.Text + _
                ''        "</b> por parte de <b>" + .lblColaborador.Text + "</b> <br>" + _
                ''        "<br>Favor de determinar si se autoriza </span>"
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

                sdaEmpleadoPro.Dispose()
                dsEmpleadoPro.Dispose()

                Dim evalPend As Integer
                SCMValores.Parameters.Clear()
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
                SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                evalPend = SCMValores.ExecuteScalar
                ConexionBD.Close()

                'En caso de que no existan evaluaciones pendientes por generar y/o autorizar
                If evalPend = 0 Then
                    fecha = Date.Now

                    'Validar si existen empleados de 2 meses o menos para insertar su registros 
                    Dim contEmpl As Integer
                    SCMValores.Parameters.Clear()
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
                                             "  and id_ms_evaluacion is null " '"or (ms_evaluacion.status in ('P', 'PC'))) "
                    SCMValores.Parameters.AddWithValue("@id_dt_area", Val(._txtIdDtArea.Text))
                    SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
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
                        sdaEmpleadoProID.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
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
                            Dim sdaEmpleadoNomA As New SqlDataAdapter
                            Dim dsEmpleadoNomA As New DataSet
                            sdaEmpleadoNomA.SelectCommand = New SqlCommand("select rtrim(ltrim(isnull(nomtrab.nombre,''))) as nombre " + _
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
                            sdaEmpleadoNomA.SelectCommand.Parameters.AddWithValue("@no_empleado", dsEmpleadoProNE.Tables(0).Rows(0).Item("no_empleado").ToString())
                            sdaEmpleadoNomA.SelectCommand.Parameters.AddWithValue("@no_empleado_lid", dsEmpleadoProNE.Tables(0).Rows(0).Item("no_empleadoAut").ToString())
                            ConexionBD.Open()
                            sdaEmpleadoNomA.Fill(dsEmpleadoNomA)
                            ConexionBD.Close()

                            'Insertar en ms_evaluación
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "insert into ms_evaluacion(  no_empleado,  nombre,  ap_paterno,  ap_materno,  empresa,  centro_costo,  puesto,  base,  unidad_neg,  direccion,  id_dt_area,  area,  lider,  puesto_lider,  año_eval,  mes_eval,  porcent_cumpl,  cobra_bono_asist, cobra_bono_cumpl_UN,  id_usr_registro,  fecha_registro,  com_registro,  id_usr_valida,  com_valida,  id_usr_director,  status) " + _
                                                     "                   values( @no_empleado, @nombre, @ap_paterno, @ap_materno, @empresa, @centro_costo, @puesto, @base, @unidad_neg, @direccion, @id_dt_area, @area, @lider, @puesto_lider, @año_eval, @mes_eval, @porcent_cumpl, @cobra_bono_asist,   @cobra_bono_asist, @id_usr_registro, @fecha_registro, @com_registro, @id_usr_valida, @com_valida, @id_usr_director, @status) "
                            SCMValores.Parameters.AddWithValue("@no_empleado", dsEmpleadoProNE.Tables(0).Rows(0).Item("no_empleado").ToString())
                            SCMValores.Parameters.AddWithValue("@nombre", dsEmpleadoNomA.Tables(0).Rows(0).Item("nombre").ToString())
                            SCMValores.Parameters.AddWithValue("@ap_paterno", dsEmpleadoNomA.Tables(0).Rows(0).Item("ap_paterno").ToString())
                            SCMValores.Parameters.AddWithValue("@ap_materno", dsEmpleadoNomA.Tables(0).Rows(0).Item("ap_materno").ToString())
                            SCMValores.Parameters.AddWithValue("@empresa", dsEmpleadoProNE.Tables(0).Rows(0).Item("empresa").ToString())
                            SCMValores.Parameters.AddWithValue("@centro_costo", dsEmpleadoNomA.Tables(0).Rows(0).Item("centro_costo").ToString())
                            SCMValores.Parameters.AddWithValue("@puesto", dsEmpleadoNomA.Tables(0).Rows(0).Item("puesto").ToString())
                            SCMValores.Parameters.AddWithValue("@base", dsEmpleadoNomA.Tables(0).Rows(0).Item("base").ToString())
                            SCMValores.Parameters.AddWithValue("@unidad_neg", dsEmpleadoProNE.Tables(0).Rows(0).Item("unidad_neg").ToString())
                            SCMValores.Parameters.AddWithValue("@direccion", dsEmpleadoProNE.Tables(0).Rows(0).Item("direccion").ToString())
                            SCMValores.Parameters.AddWithValue("@id_dt_area", dsEmpleadoProNE.Tables(0).Rows(0).Item("id_dt_area").ToString())
                            SCMValores.Parameters.AddWithValue("@area", dsEmpleadoProNE.Tables(0).Rows(0).Item("area").ToString())
                            SCMValores.Parameters.AddWithValue("@lider", dsEmpleadoNomA.Tables(0).Rows(0).Item("lider").ToString())
                            SCMValores.Parameters.AddWithValue("@puesto_lider", dsEmpleadoNomA.Tables(0).Rows(0).Item("puestoL").ToString())
                            SCMValores.Parameters.AddWithValue("@año_eval", Val(dsEmpleadoProNE.Tables(0).Rows(0).Item("año_eval").ToString()))
                            SCMValores.Parameters.AddWithValue("@mes_eval", Val(dsEmpleadoProNE.Tables(0).Rows(0).Item("mes_eval").ToString()))
                            SCMValores.Parameters.AddWithValue("@porcent_cumpl", DBNull.Value)
                            SCMValores.Parameters.AddWithValue("@cobra_bono_asist", "S")
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
                            SCMValores.Parameters.AddWithValue("@status", "V")
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            sdaEmpleadoNomA.Dispose()
                            dsEmpleadoNomA.Dispose()
                            sdaEmpleadoProNE.Dispose()
                            dsEmpleadoProNE.Dispose()
                        Next

                    End If

                    'Actualizar Estatus de las Evaluaciones
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update ms_evaluacion  " + _
                                             "  set status = 'PA' " + _
                                             "where id_dt_area = @id_dt_area " + _
                                             "  and id_usr_registro = @id_usr_registro " + _
                                             "  and status = 'V' " + _
                                             "  and mes_eval = month((select cast(valor as datetime) " + _
                                             "                        from cg_parametros " + _
                                             "                        where parametro = 'mes_eval')) " + _
                                             "  and año_eval = year((select cast(valor as datetime) " + _
                                             "                       from cg_parametros " + _
                                             "                       where parametro = 'mes_eval')) "
                    SCMValores.Parameters.AddWithValue("@id_dt_area", Val(._txtIdDtArea.Text))
                    SCMValores.Parameters.AddWithValue("@id_usr_registro", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Obtener id de última evaluación registrada
                    Dim idMsEval As Integer
                    SCMValores.CommandText = "select isnull(max(id_ms_evaluacion), 0) as id_ms_evaluacion " + _
                                             "from ms_evaluacion " + _
                                             "where id_dt_area = @id_dt_area " + _
                                             "  and id_usr_registro = @id_usr_registro " + _
                                             "  and mes_eval = month((select cast(valor as datetime) " + _
                                             "                        from cg_parametros " + _
                                             "                        where parametro = 'mes_eval')) " + _
                                             "  and año_eval = year((select cast(valor as datetime) " + _
                                             "                       from cg_parametros " + _
                                             "                       where parametro = 'mes_eval')) "
                    ConexionBD.Open()
                    idMsEval = SCMValores.ExecuteScalar
                    ConexionBD.Close()

                    'Datos Empleado ProcAd
                    Dim sdaEmpleadoProA As New SqlDataAdapter
                    Dim dsEmpleadoProA As New DataSet
                    sdaEmpleadoProA.SelectCommand = New SqlCommand("select ms_evaluacion.unidad_neg " + _
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
                    sdaEmpleadoProA.SelectCommand.Parameters.AddWithValue("@id_ms_evaluacion", idMsEval)
                    ConexionBD.Open()
                    sdaEmpleadoProA.Fill(dsEmpleadoProA)
                    ConexionBD.Close()

                    'Insertar en tabla ms_evaluacionA
                    Dim idMsEvalA As Integer
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "insert into ms_evaluacionA ( unidad_neg,  direccion,  area,  id_dt_area,  lider,  puesto_lider,  año_eval,  mes_eval,  id_usr_registro,  fecha_registro,  com_registro,  id_usr_director,  status,  id_usr_evalua) " + _
                                             "                    values (@unidad_neg, @direccion, @area, @id_dt_area, @lider, @puesto_lider, @año_eval, @mes_eval, @id_usr_registro, @fecha_registro, @com_registro, @id_usr_director, @status, @id_usr_evalua) "
                    SCMValores.Parameters.AddWithValue("@unidad_neg", dsEmpleadoProA.Tables(0).Rows(0).Item("unidad_neg").ToString())
                    SCMValores.Parameters.AddWithValue("@direccion", dsEmpleadoProA.Tables(0).Rows(0).Item("direccion").ToString())
                    SCMValores.Parameters.AddWithValue("@area", dsEmpleadoProA.Tables(0).Rows(0).Item("area").ToString())
                    SCMValores.Parameters.AddWithValue("@id_dt_area", Val(._txtIdDtArea.Text))
                    SCMValores.Parameters.AddWithValue("@lider", dsEmpleadoProA.Tables(0).Rows(0).Item("lider").ToString())
                    SCMValores.Parameters.AddWithValue("@puesto_lider", dsEmpleadoProA.Tables(0).Rows(0).Item("puesto_lider").ToString())
                    SCMValores.Parameters.AddWithValue("@año_eval", Val(dsEmpleadoProA.Tables(0).Rows(0).Item("año_eval").ToString()))
                    SCMValores.Parameters.AddWithValue("@mes_eval", Val(dsEmpleadoProA.Tables(0).Rows(0).Item("mes_eval").ToString()))
                    SCMValores.Parameters.AddWithValue("@id_usr_registro", Val(._txtIdUsuario.Text))
                    SCMValores.Parameters.AddWithValue("@id_usr_evalua", Val(._txtIdUsuario.Text))
                    SCMValores.Parameters.AddWithValue("@fecha_registro", fecha)
                    SCMValores.Parameters.AddWithValue("@com_registro", DBNull.Value)
                    SCMValores.Parameters.AddWithValue("@id_usr_director", Val(dsEmpleadoProA.Tables(0).Rows(0).Item("id_usr_director").ToString()))
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

                    'Insertar Instancia de Validación de Evaluaciones del ÁrEa
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

                    'Insertar id_ms_evaluacionA en las evaluaciones
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
                    SCMValores.Parameters.AddWithValue("@id_usr_registro", Val(._txtIdUsuario.Text))
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

                .pnlInicio.Enabled = False
                .btnAceptar.Enabled = False
                .btnNuevo.Enabled = True
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD

                Dim evalReg As Integer
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select count(*) as empInd " + _
                                              "from dt_empleado " + _
                                              "  left join ms_evaluacion on dt_empleado.no_empleado = ms_evaluacion.no_empleado " + _
                                              "                         and mes_eval = month((select cast(valor as datetime) " + _
                                              "                                               from cg_parametros " + _
                                              "                                               where parametro = 'mes_eval')) " + _
                                              "                         and año_eval = year((select cast(valor as datetime) " + _
                                              "                                              from cg_parametros " + _
                                              "                                              where parametro = 'mes_eval')) " + _
                                              "where id_usr_evalua = @idUsuario " + _
                                              "  and (select count(*) from dt_empl_ind where dt_empl_ind.id_dt_empleado = dt_empleado.id_dt_empleado and status = 'A') > 0 " + _
                                              "  and dt_empleado.status = 'A' " + _
                                              "  and (select cast(valor as datetime) " + _
                                              "       from cg_parametros " + _
                                              "       where parametro = 'mes_eval') >= dateadd(mm, 2, cast(cast(datepart(year, fecha_ingreso) as varchar(4)) + '/' + cast(datepart(month, fecha_ingreso) as varchar(2)) + '/01' as date)) " + _
                                              "  and id_ms_evaluacion is null "
                SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                evalReg = SCMValores.ExecuteScalar()
                ConexionBD.Close()
                If evalReg > 0 Then
                    .pnlInicio.Enabled = True
                    .btnAceptar.Enabled = True
                    .btnNuevo.Enabled = False
                    llenarColaboradores()
                    limpiarPantalla()
                    llenarEvidencias()
                Else
                    Server.Transfer("Menu.aspx")
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class