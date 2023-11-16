Public Class CargasSinID
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 1

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        Dim sdaEmpleado As New SqlDataAdapter
                        Dim dsEmpleado As New DataSet
                        sdaEmpleado.SelectCommand = New SqlCommand("select perfil, nombre + ' ' + ap_paterno + ' ' + ap_materno as empleado " + _
                                                                   "from cg_usuario " + _
                                                                   "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                                   "where id_usuario = @idUsuario ", ConexionBD)
                        sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        sdaEmpleado.Fill(dsEmpleado)
                        ConexionBD.Close()
                        ._txtPerfil.Text = dsEmpleado.Tables(0).Rows(0).Item("perfil").ToString()
                        sdaEmpleado.Dispose()
                        dsEmpleado.Dispose()

                        'Limpiar Pantalla
                        limpiar()
                        'Actualizar Tabla de Usuarios Bloqueados
                        llenarGrid()
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

    Public Sub limpiar()
        With Me
            .pnlFiltros.Visible = True
            'Filtros
            'Fechas
            .cbFechaC.Checked = False
            .pnlFechaC.Visible = False
            .wdpFechaI.Date = Date.Now.ToShortDateString
            .wdpFechaF.Date = Date.Now.ToShortDateString
            .cbConductor.Checked = False
            .pnlConductor.Visible = False
            .cbUnidad.Checked = False
            .pnlUnidad.Visible = False
            'Ocultar resto de paneles principales
            .pnlRegistros.Visible = False
            .pnlDetalle.Visible = False
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

    Public Sub llenarGrid()
        With Me
            Try
                .litError.Text = ""

                'Llenar el GridView
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaConsulta As New SqlDataAdapter
                Dim dsConsulta As New DataSet
                Dim queryE As String

                queryE = "select id_dt_carga_comb " + _
                         "     , region as [Región] " + _
                         "     , centro_costos as [Centro de Costos] " + _
                         "     , identificador_vehiculo as [Identificador Vehículo] " + _
                         "     , vehiculo as [Vehículo] " + _
                         "     , fecha_transaccion as [Fecha Transacción] " + _
                         "     , transaccion as [Transacción] " + _
                         "     , cantidad_mercancia " + _
                         "     , importe_transaccion [Importe Transacción] " + _
                         "     , id_conductor as [ID Conductor Edenred] " + _
                         "     , razon_social_afiliado " + _
                         "     , Conductor " + _
                         "from dt_carga_comb " + _
                         "  left join bd_Empleado.dbo.cg_empleado cgEmpl on dt_carga_comb.id_conductor = cgEmpl.id_conductor_edenred " + _
                         "where dt_carga_comb.status = 'P' " + _
                         "  and cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno is null "

                If .cbFechaC.Checked = True Then
                    queryE = queryE + "  and (dt_carga_comb.fecha between @fechaIni and @fechaFin) "
                End If
                If .cbConductor.Checked = True Then
                    queryE = queryE + "  and dt_carga_comb.conductor like '%' + @conductor + '%' "
                End If
                If .cbUnidad.Checked = True Then
                    queryE = queryE + "  and dt_carga_comb.identificador_vehiculo like '%' + @unidad + '%' "
                End If
                queryE = queryE + "order by Conductor, dt_carga_comb.fecha "

                sdaConsulta.SelectCommand = New SqlCommand(queryE, ConexionBD)

                If .cbFechaC.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaIni", .wdpFechaI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaFin", .wdpFechaF.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
                End If
                If .cbConductor.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@conductor", .txtConductor.Text.Trim)
                End If
                If .cbUnidad.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@unidad", .txtUnidad.Text.Trim)
                End If

                .gvRegistros.DataSource = dsConsulta
                'Habilitar columnas para actualización
                .gvRegistros.Columns(0).Visible = True

                ConexionBD.Open()
                sdaConsulta.Fill(dsConsulta)
                ConexionBD.Close()
                .gvRegistros.DataBind()
                sdaConsulta.Dispose()
                dsConsulta.Dispose()
                .gvRegistros.SelectedIndex = -1

                'Inhabilitar columnas para vista
                .gvRegistros.Columns(0).Visible = False

                If .gvRegistros.Rows.Count = 0 Then
                    .pnlRegistros.Visible = False
                    .litError.Text = "No existe Registros para esos valores"
                Else
                    .pnlRegistros.Visible = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Filtros"

    Protected Sub cbFechaC_CheckedChanged(sender As Object, e As EventArgs) Handles cbFechaC.CheckedChanged
        vista(Me.pnlFechaC, Me.cbFechaC.Checked)
    End Sub

    Protected Sub cbConductor_CheckedChanged(sender As Object, e As EventArgs) Handles cbConductor.CheckedChanged
        vista(Me.pnlConductor, Me.cbConductor.Checked)
    End Sub

    Protected Sub cbUnidad_CheckedChanged(sender As Object, e As EventArgs) Handles cbUnidad.CheckedChanged
        vista(Me.pnlUnidad, Me.cbUnidad.Checked)
    End Sub

#End Region

#Region "Buscar"

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        llenarGrid()
    End Sub

#End Region

#Region "Tabla - Solicitudes"

    Protected Sub gvRegistros_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRegistros.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                .pnlFiltros.Visible = False
                .pnlRegistros.Visible = False
                .pnlDetalle.Visible = False

                Dim texto As String
                texto = .gvRegistros.SelectedRow.Cells(4).Text
                If .gvRegistros.SelectedRow.Cells(4).Text.Trim <> "&nbsp;" Then
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    'Datos de la Carga de Combustible
                    Dim sdaSol As New SqlDataAdapter
                    Dim dsSol As New DataSet
                    sdaSol.SelectCommand = New SqlCommand("select fecha_transaccion " + _
                                                          "     , identificador_vehiculo " + _
                                                          "     , vehiculo " + _
                                                          "     , region " + _
                                                          "     , centro_costos " + _
                                                          "     , transaccion " + _
                                                          "     , razon_social_afiliado " + _
                                                          "     , id_conductor " + _
                                                          "     , conductor " + _
                                                          "     , cantidad_mercancia " + _
                                                          "     , precio_ticket " + _
                                                          "     , importe_con_ieps " + _
                                                          "     , iva " + _
                                                          "     , importe_transaccion " + _
                                                          "from dt_carga_comb " + _
                                                          "where dt_carga_comb.id_dt_carga_comb = @id_dt_carga_comb ", ConexionBD)
                    sdaSol.SelectCommand.Parameters.AddWithValue("@id_dt_carga_comb", Val(.gvRegistros.SelectedRow.Cells(0).Text))
                    ConexionBD.Open()
                    sdaSol.Fill(dsSol)
                    ConexionBD.Close()

                    .lblFecha.Text = dsSol.Tables(0).Rows(0).Item("fecha_transaccion").ToString()
                    .lblUnidad.Text = dsSol.Tables(0).Rows(0).Item("identificador_vehiculo").ToString()
                    .lblRegion.Text = dsSol.Tables(0).Rows(0).Item("region").ToString()
                    .lblTransaccion.Text = dsSol.Tables(0).Rows(0).Item("transaccion").ToString()
                    .lblVehiculo.Text = dsSol.Tables(0).Rows(0).Item("vehiculo").ToString()
                    .lblCC.Text = dsSol.Tables(0).Rows(0).Item("centro_costos").ToString()
                    .lblEstacion.Text = dsSol.Tables(0).Rows(0).Item("razon_social_afiliado").ToString()
                    .txtIdConductor.Text = dsSol.Tables(0).Rows(0).Item("id_conductor").ToString()
                    .txtConductorE.Text = dsSol.Tables(0).Rows(0).Item("conductor").ToString()
                    .wneLitros.Value = Val(dsSol.Tables(0).Rows(0).Item("cantidad_mercancia").ToString())
                    .wcePrecio.Value = Val(dsSol.Tables(0).Rows(0).Item("precio_ticket").ToString())
                    .wceSubtotal.Value = Val(dsSol.Tables(0).Rows(0).Item("importe_con_ieps").ToString())
                    .wceIVA.Value = Val(dsSol.Tables(0).Rows(0).Item("iva").ToString())
                    .wceTotal.Value = Val(dsSol.Tables(0).Rows(0).Item("importe_transaccion").ToString())
                    sdaSol.Dispose()
                    dsSol.Dispose()

                    .pnlDetalle.Visible = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        With Me
            Try
                .litError.Text = ""

                If .txtIdConductor.Text.Trim = "" Then
                    .litError.Text = "Información Insuficiente, favor de ingresar el ID del Conductor"
                Else
                    If .txtConductorE.Text.Trim = "" Then
                        .litError.Text = "Información Insuficiente, favor de ingresar el Nombre del Conductor"
                    Else
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update dt_carga_comb SET id_conductor = @id_conductor, conductor = @conductor, id_usr_modifica = @id_usr_modifica, fecha_modifica = @fecha_modifica " + _
                                                 "where id_dt_carga_comb = @id_dt_carga_comb "
                        SCMValores.Parameters.AddWithValue("@id_conductor", .txtIdConductor.Text.Trim)
                        SCMValores.Parameters.AddWithValue("@conductor", .txtConductorE.Text.Trim)
                        SCMValores.Parameters.AddWithValue("@id_usr_modifica", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@fecha_modifica", Date.Now)
                        SCMValores.Parameters.AddWithValue("@id_dt_carga_comb", Val(.gvRegistros.SelectedRow.Cells(0).Text))

                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        limpiar()
                        llenarGrid()
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "update dt_carga_comb SET status = 'Z', id_usr_cancela = @id_usr_cancela, fecha_cancela = @fecha_cancela " + _
                            "where id_dt_carga_comb = @id_dt_carga_comb "
                SCMValores.Parameters.AddWithValue("@id_usr_cancela", Val(._txtIdUsuario.Text))
                SCMValores.Parameters.AddWithValue("@fecha_cancela", Date.Now)
                SCMValores.Parameters.AddWithValue("@id_dt_carga_comb", Val(.gvRegistros.SelectedRow.Cells(0).Text))

                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                limpiar()
                llenarGrid()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Nueva Búsqueda"

    Protected Sub btnNueBusProd_Click(sender As Object, e As EventArgs) Handles btnNueBusProd.Click
        Me.pnlFiltros.Visible = True
        Me.pnlRegistros.Visible = True
        Me.pnlDetalle.Visible = False
        Me.gvRegistros.SelectedIndex = -1
    End Sub

#End Region

End Class