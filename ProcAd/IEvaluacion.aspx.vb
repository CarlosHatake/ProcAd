Public Class IEvaluacion
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio e Impresión"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack() Then
            With Me
                Try
                    'Session("idMsEval") = 13
                    ._txtFolio.Text = Session("idMsEval")
                    If Val(._txtFolio.Text) > 0 Then
                        .litError.Text = ""

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
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
                                                                     "from ms_evaluacion " + _
                                                                     "where ms_evaluacion.id_ms_evaluacion = @idMsEval ", ConexionBD)
                        sdaEvaluacion.SelectCommand.Parameters.AddWithValue("@idMsEval", Val(._txtFolio.Text))
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

                        imprimir()
                    Else
                        .litError.Text = "Folio Inválido, favor de Verificar"
                    End If
                Catch ex As Exception
                    .litError.Text = ex.ToString
                End Try
            End With
        End If
    End Sub

    Private Sub imprimir()
        ' * * * Anterior * * *
        'Dim scriptString As String = "<script LANGUAJE='JavaScript'> window.print();</script>"
        'Page.RegisterStartupScript("clientScript", scriptString)
        ' * * * Anterior * * *

        ' Define el Nombre y Tipo del client scripts en la página
        Dim csNombre As [String] = "ImpPagina"
        Dim csTipo As Type = Me.[GetType]()
        'Asigna al ClientScriptManager la referencia del la clase de la Página 
        Dim cs As ClientScriptManager = Page.ClientScript
        'Revisa si el inicio del script ya está registrado 
        If Not cs.IsStartupScriptRegistered(csTipo, csNombre) Then
            Dim cstext1 As New StringBuilder()
            cstext1.Append("<script type=text/javascript> window.print();</script>")
            cs.RegisterStartupScript(csTipo, csNombre, cstext1.ToString())
        End If
    End Sub

#End Region

End Class