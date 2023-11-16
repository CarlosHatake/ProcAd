Public Class CatVehiculoST
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
                    Else
                        Server.Transfer("Default.aspx")
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
            'Actualizar Lista de Vehículos
            llenarGrid()
            .pnlInicio.Enabled = True
            .gvVehiculo.Enabled = True
            .ibtnModif.Enabled = False
            .ibtnModif.ImageUrl = "images\Edit_i2.png"
            .pnlInicio.Visible = True
            .pnlDatos.Visible = False
        End With
    End Sub

    Public Sub bloqueoPantalla()
        With Me
            .pnlInicio.Visible = False
            .pnlDatos.Visible = True
        End With
    End Sub

    Public Sub llenarGrid()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("SiCEm")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .gvVehiculo.Columns(0).Visible = True
                .gvVehiculo.DataSource = dsCatalogo
                'Catálogo de Empleados
                sdaCatalogo.SelectCommand = New SqlCommand("select id_ms_vehiculo " +
                                                           "     , no_eco " +
                                                           "     , tipo_unidad " +
                                                           "     , marca " +
                                                           "     , modelo " +
                                                           "     , año " +
                                                           "     , placas " +
                                                           "     , cg_empleado.nombre + ' ' + ap_paterno + ' ' + ap_materno as asignado_a " +
                                                           "     , porcent_tolerancia as tolerancia " +
                                                           "     , rendimiento " +
                                                           "from ms_vehiculo " +
                                                           "  left join cg_empleado on ms_vehiculo.id_empleado_asig = cg_empleado.id_empleado " +
                                                           "where ms_vehiculo.status <> 'B' " +
                                                           "  and (" + .ddlCampo.SelectedValue.ToString + " like '%' + @valor +'%') " +
                                                           "order by no_eco, placas ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@valor", .txtValor.Text.Trim)
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvVehiculo.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvVehiculo.Columns(0).Visible = False
                .gvVehiculo.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub localizar(ByVal idVehiculo)
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("SiCEm")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                sdaCatalogo.SelectCommand = New SqlCommand("select no_eco " +
                                                           "     , isnull(marca, '') as marca " +
                                                           "     , modelo " +
                                                           "     , isnull(tipo_unidad, '') as tipo_unidad " +
                                                           "     , serie " +
                                                           "     , motor " +
                                                           "     , año " +
                                                           "     , ubicacion " +
                                                           "     , isnull(porcent_tolerancia, 0) as porcent_tolerancia " +
                                                           "     , isnull(rendimiento, 0) as rendimiento " +
                                                           "     , placas " +
                                                           "     , convert(varchar, isnull(mantto_fecha_ult, '1900-01-01'), 103) as mantto_fecha_ult " +
                                                           "     , convert(varchar, isnull(mantto_fecha_prox, '1900-01-01'), 103) as mantto_fecha_prox " +
                                                           "     , isnull(mantto_fecha_prox_km, 0) as mantto_fecha_prox_km " +
                                                           "     , isnull(km_actual, 0) as km_actual " +
                                                           "     , obs " +
                                                           "     , cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as asignado_a " +
                                                           "     , cgCC.nombre as centro_costo " +
                                                           "     , case ms_vehiculo.status " +
                                                           "         when 'A' then 'Activo' " +
                                                           "         when 'S' then 'Siniestrado' " +
                                                           "         when 'M' then 'Mantenimiento' " +
                                                           "         when 'PV' then 'Proceso de Venta' " +
                                                           "         when 'RB' then 'Robo' " +
                                                           "         when 'B' then 'Baja' " +
                                                           "       end as statusU " +
                                                           "     , (select top 1 dt_carga_comb.rendimiento " +
                                                           "        from bd_ProcAd.dbo.dt_carga_comb " +
                                                           "        where num_tarjeta = ms_vehiculo.no_tarjeta_edenred " +
                                                           "        order by fecha desc) as ult_rend_tabulado " +
                                                           "from ms_vehiculo " +
                                                           "  left join cg_empleado cgEmpl on ms_vehiculo.id_empleado_asig = cgEmpl.id_empleado " +
                                                           "  left join cg_cc cgCC on cgEmpl.id_cc = cgCC.id_cc " +
                                                           "where id_ms_vehiculo = @id_ms_vehiculo ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_ms_vehiculo", idVehiculo)
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                ConexionBD.Close()

                .lblStatusU.Text = dsCatalogo.Tables(0).Rows(0).Item("statusU").ToString()
                .lblNoEconomico.Text = dsCatalogo.Tables(0).Rows(0).Item("no_eco").ToString()
                .lblMarca.Text = dsCatalogo.Tables(0).Rows(0).Item("marca").ToString()
                .lblModelo.Text = dsCatalogo.Tables(0).Rows(0).Item("modelo").ToString()
                .lblTipoUnidad.Text = dsCatalogo.Tables(0).Rows(0).Item("tipo_unidad").ToString()
                .lblSerie.Text = dsCatalogo.Tables(0).Rows(0).Item("serie").ToString()
                .lblMotor.Text = dsCatalogo.Tables(0).Rows(0).Item("motor").ToString()
                .lblAño.Text = dsCatalogo.Tables(0).Rows(0).Item("año").ToString()
                .lblUbicacion.Text = dsCatalogo.Tables(0).Rows(0).Item("ubicacion").ToString()
                .lblUltRendTab.Text = dsCatalogo.Tables(0).Rows(0).Item("ult_rend_tabulado").ToString()
                .lblPlacas.Text = dsCatalogo.Tables(0).Rows(0).Item("placas").ToString()
                .lblManttoUlt.Text = dsCatalogo.Tables(0).Rows(0).Item("mantto_fecha_ult").ToString()
                .lblManttoProx.Text = dsCatalogo.Tables(0).Rows(0).Item("mantto_fecha_prox").ToString()
                .lblKmsAct.Text = dsCatalogo.Tables(0).Rows(0).Item("km_actual").ToString()
                .txtObs.Text = dsCatalogo.Tables(0).Rows(0).Item("obs").ToString()
                .wneRendimiento.Value = Val(dsCatalogo.Tables(0).Rows(0).Item("rendimiento").ToString())
                .wpeTolerancia.Value = Val(dsCatalogo.Tables(0).Rows(0).Item("porcent_tolerancia").ToString())
                .lblAsignadoA.Text = dsCatalogo.Tables(0).Rows(0).Item("asignado_a").ToString()
                .lblCC.Text = dsCatalogo.Tables(0).Rows(0).Item("centro_costo").ToString()

                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                bloqueoPantalla()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Selección"

    Protected Sub ibtnBuscar_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBuscar.Click
        llenarGrid()
    End Sub

    Private Sub gvVehiculo_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvVehiculo.PageIndexChanging
        gvVehiculo.PageIndex = e.NewPageIndex
        llenarGrid()
    End Sub

    Protected Sub gvVehiculo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvVehiculo.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                .ibtnModif.Enabled = True
                .ibtnModif.ImageUrl = "images\Edit.png"
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Botones - Inicio"

    Protected Sub ibtnModif_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnModif.Click
        With Me
            Try
                ._txtTipoMov.Text = "M"
                .lblTipoMov.Text = "Modificación"
                bloqueoPantalla()
                localizar(Val(.gvVehiculo.SelectedRow.Cells(0).Text))
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Botones - Datos"

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""
                Dim ban As Integer = 0
                If .wpeTolerancia.Text.Trim = "" Then
                    .litError.Text = "Información Insuficiente, favor de ingresar el porcentaje de Tolerancia"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("SiCEm")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "UPDATE ms_vehiculo SET rendimiento = @rendimiento, porcent_tolerancia = @porcent_tolerancia " + _
                                             "WHERE id_ms_vehiculo = @id_ms_vehiculo"
                    SCMValores.Parameters.AddWithValue("@rendimiento", .wneRendimiento.Value)
                    SCMValores.Parameters.AddWithValue("@porcent_tolerancia", .wpeTolerancia.Value)
                    SCMValores.Parameters.AddWithValue("@id_ms_vehiculo", Val(.gvVehiculo.SelectedRow.Cells(0).Text))

                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()
                    limpiarPantalla()
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        limpiarPantalla()
    End Sub

#End Region

End Class