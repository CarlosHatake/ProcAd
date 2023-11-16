Public Class ConsDisp
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        'Llenar lista de Vehículos
                        Dim query As String
                        Dim sdaVehiculo As New SqlDataAdapter
                        Dim dsVehiculo As New DataSet
                        query = "select id_ms_vehiculo, modelo + ' [' + no_eco + ' ' + placas + ']' as vehiculo " + _
                                "from bd_Empleado.dbo.ms_vehiculo " + _
                                "where status = 'A' " + _
                                "  and uso_utilitario = 'COMODIN' " + _
                                "  and poliza_seguro_vig > GETDATE() " + _
                                "  and tarjeta_cir_vig > GETDATE() "
                        sdaVehiculo.SelectCommand = New SqlCommand(query, ConexionBD)
                        .ddlNoEco.DataSource = dsVehiculo
                        .ddlNoEco.DataTextField = "vehiculo"
                        .ddlNoEco.DataValueField = "id_ms_vehiculo"
                        ConexionBD.Open()
                        sdaVehiculo.Fill(dsVehiculo)
                        .ddlNoEco.DataBind()
                        ConexionBD.Close()
                        sdaVehiculo.Dispose()
                        dsVehiculo.Dispose()
                        .ddlNoEco.SelectedIndex = -1

                        'Limpiar Pantalla
                        limpiar()
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
            '.cbFechaC.Checked = False
            '.pnlFechaC.Visible = False
            .wdpFechaI.Date = Date.Now
            .wdpFechaF.Date = CDate(Date.Now.ToShortDateString).AddHours(23).AddMinutes(59).AddSeconds(59)
            vista(Me.pnlFechaC, Me.cbFechaC.Checked)
            .cbNoEco.Checked = False
            .pnlNoEco.Visible = False
            'Ocultar resto de paneles principales
            .pnlRegistros.Visible = False
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

#End Region

#Region "Filtros"

    Protected Sub cbFechaC_CheckedChanged(sender As Object, e As EventArgs) Handles cbFechaC.CheckedChanged
        vista(Me.pnlFechaC, Me.cbFechaC.Checked)
    End Sub

    Protected Sub cbNoEco_CheckedChanged(sender As Object, e As EventArgs) Handles cbNoEco.CheckedChanged
        vista(Me.pnlNoEco, Me.cbNoEco.Checked)
    End Sub

#End Region

#Region "Buscar"

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        With Me
            Try
                .litError.Text = ""

                'Llenar el GridView
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaVehi As New SqlDataAdapter
                Dim dsVehi As New DataSet
                Dim queryD As String
                Dim sdaConsulta As New SqlDataAdapter
                Dim dsConsulta As New DataSet
                Dim queryR As String

                queryD = "select id_ms_vehiculo " + _
                         "     , no_eco " + _
                         "     , ubicacion " + _
                         "     , marca " + _
                         "     , modelo " + _
                         "     , año " + _
                         "     , placas " + _
                         "     , IAVE " + _
                         "from bd_Empleado.dbo.ms_vehiculo " + _
                         "where status = 'A' " + _
                         "  and uso_utilitario = 'COMODIN' " + _
                         "  and poliza_seguro_vig > GETDATE() " + _
                         "  and tarjeta_cir_vig > GETDATE() "

                queryR = "select empleado_nom + ' ' + empleado_appat + ' ' + empleado_apmat as solicito " + _
                         "	 , fecha_ini " + _
                         "	 , fecha_fin " + _
                         "	 , prioridad " + _
                         "	 , no_eco " + _
                         "	 , placas " + _
                         "	 , destino " + _
                         "	 , case status " + _
                         "	     when 'P' then 'Pendiente' " + _
                         "		 when 'A' then 'Autorizado' " + _
                         "		 when 'T' then 'Entregado al Usuario' " + _
                         "		 when 'R' then 'Recibido por Vigilancia' " + _
                         "		 when 'Z' then 'Rechazado' " + _
                         "		 when 'ZM' then 'Cancelado' " + _
                         "		 else '-' " + _
                         "	   end as estado " + _
                         "from ms_reserva " + _
                         "where status not in ('Z', 'ZM', 'R') "

                If .cbFechaC.Checked = True Then
                    queryD = queryD + "  and id_ms_vehiculo not in (select id_ms_vehiculo " + _
                                      "							    from ms_reserva " + _
                                      "							    where ms_reserva.status not in ('Z', 'ZM', 'R') " + _
                                      "							      and ((fecha_ini <= @fechaIni and fecha_fin > @fechaIni) " + _
                                      "							       or (fecha_ini < @fechaFin and fecha_fin >= @fechaFin) " + _
                                      "							       or (fecha_ini <= @fechaIni and fecha_fin >= @fechaFin) " + _
                                      "							       or (fecha_ini > @fechaIni and fecha_fin < @fechaFin))) "

                    queryR = queryR + "  and((fecha_ini <= @fechaIni and fecha_fin > @fechaIni) " + _
                                    "   or (fecha_ini < @fechaFin and fecha_fin >= @fechaFin) " + _
                                    "   or (fecha_ini <= @fechaIni and fecha_fin >= @fechaFin) " + _
                                    "   or (fecha_ini > @fechaIni and fecha_fin < @fechaFin)) "
                End If

                If .cbNoEco.Checked = True Then
                    queryD = queryD + "  and id_ms_vehiculo = @idMsVehiculo "
                    queryR = queryR + "  and id_ms_vehiculo = @idMsVehiculo "
                End If

                sdaConsulta.SelectCommand = New SqlCommand(queryR, ConexionBD)
                sdaVehi.SelectCommand = New SqlCommand(queryD, ConexionBD)

                If .cbFechaC.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaIni", .wdpFechaI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaFin", .wdpFechaF.Date)
                    sdaVehi.SelectCommand.Parameters.AddWithValue("@fechaIni", .wdpFechaI.Date)
                    sdaVehi.SelectCommand.Parameters.AddWithValue("@fechaFin", .wdpFechaF.Date)
                End If

                If .cbNoEco.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@idMsVehiculo", .ddlNoEco.SelectedValue)
                    sdaVehi.SelectCommand.Parameters.AddWithValue("@idMsVehiculo", .ddlNoEco.SelectedValue)
                End If

                .gvReservas.DataSource = dsConsulta
                ConexionBD.Open()
                sdaConsulta.Fill(dsConsulta)
                ConexionBD.Close()
                .gvReservas.DataBind()
                sdaConsulta.Dispose()
                dsConsulta.Dispose()
                If .gvReservas.Rows.Count = 0 Then
                    .gvReservas.Visible = False
                    .lbl_NoReservas.Visible = True
                Else
                    .gvReservas.Visible = True
                    .lbl_NoReservas.Visible = False
                End If

                .gvVehiculos.DataSource = dsVehi
                ConexionBD.Open()
                sdaVehi.Fill(dsVehi)
                ConexionBD.Close()
                .gvVehiculos.DataBind()
                sdaVehi.Dispose()
                dsVehi.Dispose()
                If .gvVehiculos.Rows.Count = 0 Then
                    .gvVehiculos.Visible = False
                    .lbl_NoVehiculos.Visible = True
                Else
                    .gvVehiculos.Visible = True
                    .lbl_NoVehiculos.Visible = False
                End If

                .pnlRegistros.Visible = True
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class