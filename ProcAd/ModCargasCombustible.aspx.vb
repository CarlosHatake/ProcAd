Public Class ModCargasCombustible
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        limpiarPantalla()
                        listaEmpleados()
                        listaUnidades()
                    Else
                        Server.Transfer("Menu.aspx")
                    End If
                Catch ex As Exception
                    .litError.Text = ex.ToString
                End Try
            End With
        End If
    End Sub
#End Region

#Region "Funciones"
    Public Sub listaEmpleados()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaEmpleado As New SqlDataAdapter
                Dim dsEmpleado As New DataSet
                sdaEmpleado.SelectCommand = New SqlCommand(" SELECT us.id_usuario, nombre + ' ' + ap_paterno + ' ' + ap_materno AS nombreCompleto FROM bd_Empleado.dbo.cg_empleado empl" +
                                                         " LEFT JOIN cg_usuario us ON us.id_empleado = empl.id_empleado WHERE us.status = 'A' AND empl.nombre + ' ' + empl.ap_paterno + ' ' + empl.ap_materno LIKE '%' + @nombreEmpl + '%' ORDER BY nombreCompleto ASC   ", ConexionBD)

                sdaEmpleado.SelectCommand.Parameters.AddWithValue("@nombreEmpl", .txtEmpleado.Text)
                .ddlEmpleado.DataSource = dsEmpleado
                .ddlEmpleado.DataTextField = "nombreCompleto"
                .ddlEmpleado.DataValueField = "id_usuario"
                ConexionBD.Open()
                sdaEmpleado.Fill(dsEmpleado)
                .ddlEmpleado.DataBind()
                ConexionBD.Close()
                sdaEmpleado.Dispose()
                dsEmpleado.Dispose()
                .ddlEmpleado.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With

    End Sub

    Public Sub listaUnidades()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaUnidad As New SqlDataAdapter
                Dim dsUnidad As New DataSet
                sdaUnidad.SelectCommand = New SqlCommand("SELECT DISTINCT no_eco FROM ms_comb WHERE no_eco LIKE '%' + @unidad + '%'ORDER BY no_eco ASC", ConexionBD)

                sdaUnidad.SelectCommand.Parameters.AddWithValue("@unidad", .txtUnidad.Text)
                .ddlUnidad.DataSource = dsUnidad
                .ddlUnidad.DataTextField = "no_eco"
                .ddlUnidad.DataValueField = "no_eco"
                ConexionBD.Open()
                sdaUnidad.Fill(dsUnidad)
                .ddlUnidad.DataBind()
                ConexionBD.Close()
                sdaUnidad.Dispose()
                dsUnidad.Dispose()
                .ddlUnidad.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub



    Public Sub llenarGvMsCargaCombustible()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim query As String
                Dim sdaConsulta As New SqlDataAdapter
                Dim dsConsulta As New DataSet

                query = " SELECT id_ms_comb, no_eco, empleado, periodo_ini, periodo_fin, status FROM ms_comb WHERE status NOT IN ('Z', 'ZC') "

                If .cbEmpleado.Checked = True Then
                    query = query + "  and id_usr_solicita = @id_usr_solicita "

                End If

                If .cbUnidad.Checked = True Then
                    query = query + "  and no_eco = @no_eco "

                End If

                If .cbPeriodo.Checked = True Then
                    query = query + "  and periodo_ini >= @f1 and periodo_fin < @f2"

                End If


                sdaConsulta.SelectCommand = New SqlCommand(query + " order by fecha_solicita ASC ", ConexionBD)


                If cbEmpleado.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_usr_solicita", .ddlEmpleado.SelectedItem.Value)

                End If

                If .cbUnidad.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@no_eco", .ddlUnidad.SelectedItem.Value)
                End If

                If .cbPeriodo.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@f1", .wdpFechaI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@f2", .wdpFechaF.Date)
                End If


                .gvMsCargasCombustible.DataSource = dsConsulta
                ConexionBD.Open()
                sdaConsulta.Fill(dsConsulta)
                ConexionBD.Close()
                .gvMsCargasCombustible.DataBind()
                sdaConsulta.Dispose()
                dsConsulta.Dispose()

                If gvMsCargasCombustible.Rows.Count() = 0 Then
                    litError.Text = "No existen registros para esos valores, rectifique"
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

    Public Sub llenarGvDtCargaCombustible()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim query As String
                Dim sdaConsulta As New SqlDataAdapter
                Dim dsConsulta As New DataSet

                query = " SELECT id_dt_carga_comb_toka, identificador_vehiculo, razon_social_afiliado, cantidad_mercancia, importe_transaccion, km_ant_transaccion, km_transaccion, recorrido, fecha, status " +
                        " FROM dt_carga_comb_toka " +
                        " WHERE identificador_vehiculo = @identificador_vehiculo AND fecha >= @fecha ORDER BY dt_carga_comb_toka.fecha"
                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)

                sdaConsulta.SelectCommand.Parameters.AddWithValue("@identificador_vehiculo", .ddlUnidad.SelectedItem.Text)
                sdaConsulta.SelectCommand.Parameters.AddWithValue("@fecha", .wdpFecha.Date)


                .gvDtCargaCombustible.DataSource = dsConsulta
                ConexionBD.Open()
                sdaConsulta.Fill(dsConsulta)
                ConexionBD.Close()
                .gvDtCargaCombustible.DataBind()
                sdaConsulta.Dispose()
                dsConsulta.Dispose()

                If gvDtCargaCombustible.Rows.Count() = 0 Then
                    litError.Text = "No existen registros para esos valores, rectifique"
                End If
            Catch ex As Exception
                litError.Text = ex.ToString()
            End Try
        End With
    End Sub

    Protected Sub gvMsCargasCombustible_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvMsCargasCombustible.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaSol As New SqlDataAdapter
                Dim dsSol As New DataSet
                Dim query As String
                query = " SELECT no_eco, periodo_fin, periodo_ini, status " +
                        " FROM ms_comb " +
                        " WHERE id_ms_comb = @id_ms_comb"
                sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaSol.SelectCommand.Parameters.AddWithValue("@id_ms_comb", .gvMsCargasCombustible.DataKeys(gvMsCargasCombustible.SelectedIndex).Values("id_ms_comb"))
                sdaSol.Fill(dsSol)
                ConexionBD.Close()
                .lbl_Unidad.Text = dsSol.Tables(0).Rows(0).Item("no_eco").ToString()
                .lbl_PeriodoInic.Text = (dsSol.Tables(0).Rows(0).Item("periodo_ini").ToShortDateString())
                .lbl_PeriodoFin.Text = (dsSol.Tables(0).Rows(0).Item("periodo_fin").ToShortDateString())
                .lbl_Status.Text = dsSol.Tables(0).Rows(0).Item("status").ToString()
                sdaSol.Dispose()
                dsSol.Dispose()

                .pnlAmpliarPeriodo.Visible = True
            Catch ex As Exception
                .litError.Text = ex.ToString

            End Try
        End With
    End Sub

    Protected Sub gvDtCargasCombustible_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDtCargaCombustible.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaSol As New SqlDataAdapter
                Dim dsSol As New DataSet
                Dim query As String
                query = " SELECT identificador_vehiculo, fecha, status " +
                        " FROM dt_carga_comb_toka " +
                        " WHERE id_dt_carga_comb_toka = @id_dt_carga_comb_toka"
                sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaSol.SelectCommand.Parameters.AddWithValue("@id_dt_carga_comb_toka", .gvDtCargaCombustible.DataKeys(gvDtCargaCombustible.SelectedIndex).Values("id_dt_carga_comb_toka"))
                sdaSol.Fill(dsSol)
                ConexionBD.Close()
                .lbl_UnidadDt.Text = dsSol.Tables(0).Rows(0).Item("identificador_vehiculo").ToString()
                .lbl_FechaActual.Text = dsSol.Tables(0).Rows(0).Item("fecha").ToString()
                .lbl_StatusDt.Text = dsSol.Tables(0).Rows(0).Item("status").ToString()
                sdaSol.Dispose()
                dsSol.Dispose()

                .pnlDescomplementar.Visible = True
            Catch ex As Exception
                .litError.Text = ex.ToString

            End Try
        End With
    End Sub


    Public Sub limpiarPantalla()
        With Me
            Try
                .cbEmpleado.Checked = False
                .pnlEmpleadoB.Visible = False
                .pnlEmpleadoD.Visible = False
                '.txtEmpleado.Visible = False
                '.ibtnBuscarEmpleado.Visible = False
                '.ddlEmpleado.Visible = False
                .ddlEmpleado.SelectedIndex = -1
                .txtEmpleado.Text = ""

                .cbUnidad.Checked = False
                .pnlUnidadB.Visible = False
                .pnlUnidadD.Visible = False
                '.txtUnidad.Visible = False
                '.ibtnBuscarUnidad.Visible = False
                'ddlUnidad.Visible = False
                .ddlUnidad.SelectedIndex = -1
                .txtUnidad.Text = ""

                .cbPeriodo.Checked = False
                .pnlPeriodo.Visible = False

                .cbFecha.Checked = False
                .pnlFecha.Visible = False

                .pnlAmpliarPeriodo.Visible = False
                .pnlDescomplementar.Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

    Public Sub vista(ByRef control, ByVal valor)
        With Me
            If valor Then
                control.Visible = True
            Else
                control.Visible = False
            End If
        End With
    End Sub

    Protected Sub cbEmpresa_CheckedChanged(sender As Object, e As EventArgs) Handles cbEmpleado.CheckedChanged
        vista(Me.pnlEmpleadoB, Me.cbEmpleado.Checked)
        vista(Me.pnlEmpleadoD, Me.cbEmpleado.Checked)
        Me.txtEmpleado.Text = ""
        listaEmpleados()
    End Sub

    Protected Sub cbUnidad_CheckedChanged(sender As Object, e As EventArgs) Handles cbUnidad.CheckedChanged
        vista(Me.pnlUnidadB, Me.cbUnidad.Checked)
        vista(Me.pnlUnidadD, Me.cbUnidad.Checked)
        Me.txtUnidad.Text = ""
        listaUnidades()
    End Sub

    Protected Sub cbPeriodo_CheckedChanged(sender As Object, e As EventArgs) Handles cbPeriodo.CheckedChanged
        vista(Me.pnlPeriodo, Me.cbPeriodo.Checked)
    End Sub

    Protected Sub cbFecha_CheckedChanged(sender As Object, e As EventArgs) Handles cbFecha.CheckedChanged
        vista(Me.pnlFecha, Me.cbFecha.Checked)
    End Sub
#End Region

#Region "Botones"
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        With Me
            Try
                .litError.Text = ""
                .pnlAmpliarPeriodo.Visible = False
                .pnlDescomplementar.Visible = False
                .gvMsCargasCombustible.SelectedIndex = -1
                .gvDtCargaCombustible.SelectedIndex = -1

                If cbEmpleado.Checked = False And cbPeriodo.Checked = False And cbUnidad.Checked = False Then
                    .litError.Text = "Seleccione un filtro para continuar"
                Else
                    llenarGvMsCargaCombustible()
                    If cbUnidad.Checked = True And cbFecha.Checked Then
                        llenarGvDtCargaCombustible()
                    End If

                End If

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnBuscarEmpleado_Click(sender As Object, e As EventArgs) Handles ibtnBuscarEmpleado.Click
        With Me
            Try
                listaEmpleados()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnBuscarUnidad_Click(sender As Object, e As EventArgs) Handles ibtnBuscarUnidad.Click
        With Me
            Try
                listaUnidades()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnAmpliarPeriodo_Click(sender As Object, e As EventArgs) Handles btnAmpliarPeriodo.Click
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = ""

                SCMValores.CommandText = " UPDATE ms_comb " +
                                        " SET periodo_fin = @periodo_fin," +
                                        " periodo_ini = @periodo_ini " +
                                        " WHERE id_ms_comb = @id_ms_comb  "
                SCMValores.Parameters.AddWithValue("@periodo_ini", .wdpNuevoPeriodoIni.Text)
                SCMValores.Parameters.AddWithValue("@periodo_fin", .wdpNuevoPeriodoFin.Text)
                SCMValores.Parameters.AddWithValue("@id_ms_comb", .gvMsCargasCombustible.DataKeys(gvMsCargasCombustible.SelectedIndex).Values("id_ms_comb"))
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                llenarGvMsCargaCombustible()
                .pnlAmpliarPeriodo.Visible = False
                .gvMsCargasCombustible.SelectedIndex = -1

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With

    End Sub

    Protected Sub btnCancelarAmpliarPeriodo_Click(sender As Object, e As EventArgs) Handles btnCancelarAmpliarPeriodo.Click
        With Me
            .pnlAmpliarPeriodo.Visible = False
            .gvMsCargasCombustible.SelectedIndex = -1
        End With
    End Sub

    Protected Sub btnCambiarFecha_Click(sender As Object, e As EventArgs) Handles btnCambiarFecha.Click
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                Dim fecha As DateTime

                fecha = Convert.ToDateTime(lbl_FechaActual.Text).ToShortDateString() & Convert.ToDateTime(.wdpFechaDt.Text)

                SCMValores.Connection = ConexionBD
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = ""

                SCMValores.CommandText = " UPDATE dt_carga_comb_toka " +
                                        " SET fecha = @fecha" +
                                        " WHERE id_dt_carga_comb_toka = @id_dt_carga_comb_toka"
                SCMValores.Parameters.AddWithValue("@fecha", fecha)
                SCMValores.Parameters.AddWithValue("@id_dt_carga_comb_toka", .gvDtCargaCombustible.DataKeys(gvDtCargaCombustible.SelectedIndex).Values("id_dt_carga_comb_toka"))
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                llenarGvDtCargaCombustible()
                .pnlDescomplementar.Visible = False
                .gvDtCargaCombustible.SelectedIndex = -1

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With

    End Sub

    Protected Sub btnDescomplementar_Click(sender As Object, e As EventArgs) Handles btnDescomplementar.Click
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                Dim val As Integer = 0
                SCMValores.Connection = ConexionBD
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = ""

                'Validar que no hayan cargas posteriores

                SCMValores.CommandText = "SELECT COUNT(*) FROM dt_carga_comb_toka WHERE fecha >= @fecha AND status = 'R'"
                SCMValores.Parameters.AddWithValue("@fecha", Convert.ToDateTime(lbl_FechaActual.Text))
                ConexionBD.Open()
                val = SCMValores.ExecuteScalar()
                ConexionBD.Close()

                If val = 0 Then

                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = ""
                    SCMValores.CommandText = " UPDATE dt_carga_comb_toka " +
                                         " SET status = 'P', obs_evid = null, id_conductor = null, conductor = null, centro_costos = null " +
                                         " , km_ant_transaccion = null, km_transaccion = null, recorrido = null, rendimiento = null, rendimiento_real = null " +
                                         " , desviacion_real = null, bloqueo_st = 'N', foto_evid = null, odometro_evid = null, existe_dif = null, diferencia = null " +
                                         " , id_usr_evid = null, fecha_evid = null " +
                                         " WHERE id_dt_carga_comb_toka = @id_dt_carga_comb_toka"

                    SCMValores.Parameters.AddWithValue("@id_dt_carga_comb_toka", .gvDtCargaCombustible.DataKeys(gvDtCargaCombustible.SelectedIndex).Values("id_dt_carga_comb_toka"))
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    llenarGvDtCargaCombustible()
                    .pnlDescomplementar.Visible = False
                    .gvDtCargaCombustible.SelectedIndex = -1
                Else
                    .litError.Text = "Existen cargas posteriores pendientes de descomplementar"
                End If



            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With

    End Sub

    Protected Sub btnCancelarDtCarga_Click(sender As Object, e As EventArgs) Handles btnCancelarDtCarga.Click
        With Me
            .pnlDescomplementar.Visible = False
            .gvDtCargaCombustible.SelectedIndex = -1
        End With
    End Sub

#End Region
End Class