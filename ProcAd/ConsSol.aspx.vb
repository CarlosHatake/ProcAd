Public Class ConsSol
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
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        Dim sdaEmpleado As New SqlDataAdapter
                        Dim dsEmpleado As New DataSet
                        sdaEmpleado.SelectCommand = New SqlCommand("select perfil from cg_usuario where id_usuario = @idUsuario", ConexionBD)
                        sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        sdaEmpleado.Fill(dsEmpleado)
                        ConexionBD.Close()
                        ._txtPerfil.Text = dsEmpleado.Tables(0).Rows(0).Item("perfil").ToString()
                        sdaEmpleado.Dispose()
                        dsEmpleado.Dispose()

                        'Llenar listas
                        Dim query As String
                        'Solicitantes
                        Dim sdaSolicitante As New SqlDataAdapter
                        Dim dsSolicitante As New DataSet
                        query = "select distinct(id_usr_solicito) as id_usr_solicito, empleado_nom + ' ' + empleado_appat + ' ' + empleado_apmat as solicito " + _
                                "from ms_reserva " + _
                                "order by solicito "
                        sdaSolicitante.SelectCommand = New SqlCommand(query, ConexionBD)
                        .ddlSolicitante.DataSource = dsSolicitante
                        .ddlSolicitante.DataTextField = "solicito"
                        .ddlSolicitante.DataValueField = "id_usr_solicito"
                        ConexionBD.Open()
                        sdaSolicitante.Fill(dsSolicitante)
                        .ddlSolicitante.DataBind()
                        ConexionBD.Close()
                        sdaSolicitante.Dispose()
                        dsSolicitante.Dispose()
                        .ddlSolicitante.SelectedIndex = -1
                        'Empresas
                        Dim sdaEmpresa As New SqlDataAdapter
                        Dim dsEmpresa As New DataSet
                        query = "select distinct(empresa) as empresa " + _
                                "from ms_reserva " + _
                                "order by empresa "
                        sdaEmpresa.SelectCommand = New SqlCommand(query, ConexionBD)
                        .ddlEmpresa.DataSource = dsEmpresa
                        .ddlEmpresa.DataTextField = "empresa"
                        .ddlEmpresa.DataValueField = "empresa"
                        ConexionBD.Open()
                        sdaEmpresa.Fill(dsEmpresa)
                        .ddlEmpresa.DataBind()
                        ConexionBD.Close()
                        sdaEmpresa.Dispose()
                        dsEmpresa.Dispose()
                        .ddlEmpresa.SelectedIndex = -1
                        'Vehículos
                        Dim sdaVehiculo As New SqlDataAdapter
                        Dim dsVehiculo As New DataSet
                        query = "select distinct(id_ms_vehiculo) as id_ms_vehiculo, no_eco + ' [' + placas + ']' as vehiculo " + _
                                "from ms_reserva " + _
                                "order by vehiculo "
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

                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Then
                            .pnlSolicitó.Visible = False
                        Else
                            .pnlSolicitó.Visible = True
                            .cbSolicitante.Checked = False
                            .pnlSolicitante.Visible = False
                            .cbEmpresa.Checked = False
                            .pnlEmpresa.Visible = False
                        End If

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
            .cbFechaC.Checked = False
            .pnlFechaC.Visible = False
            .wdpFechaI.Date = Date.Now.ToShortDateString
            .wdpFechaF.Date = Date.Now.ToShortDateString
            .cbNoEco.Checked = False
            .pnlNoEco.Visible = False
            .cbStatus.Checked = False
            .pnlStatus.Visible = False
            .cbNoSol.Checked = False
            .pnlNoSol.Visible = False
            'Ocultar resto de paneles principales
            .pnlRegistros.Visible = False
            .pnlDetalle.Visible = False
            .pnlImp.Visible = False
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

    Protected Sub cbSolicitante_CheckedChanged(sender As Object, e As EventArgs) Handles cbSolicitante.CheckedChanged
        vista(Me.pnlSolicitante, Me.cbSolicitante.Checked)
    End Sub

    Protected Sub cbEmpresa_CheckedChanged(sender As Object, e As EventArgs) Handles cbEmpresa.CheckedChanged
        vista(Me.pnlEmpresa, Me.cbEmpresa.Checked)
    End Sub

    Protected Sub cbFechaC_CheckedChanged(sender As Object, e As EventArgs) Handles cbFechaC.CheckedChanged
        vista(Me.pnlFechaC, Me.cbFechaC.Checked)
    End Sub

    Protected Sub cbNoEco_CheckedChanged(sender As Object, e As EventArgs) Handles cbNoEco.CheckedChanged
        vista(Me.pnlNoEco, Me.cbNoEco.Checked)
    End Sub

    Protected Sub cbNoSol_CheckedChanged(sender As Object, e As EventArgs) Handles cbNoSol.CheckedChanged
        vista(Me.pnlNoSol, Me.cbNoSol.Checked)
        If Me.cbNoSol.Checked = True Then
            Me.txtNoSol.Text = ""
        End If
    End Sub

    Protected Sub cbStatus_CheckedChanged(sender As Object, e As EventArgs) Handles cbStatus.CheckedChanged
        vista(Me.pnlStatus, Me.cbStatus.Checked)
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
                Dim sdaConsulta As New SqlDataAdapter
                Dim dsConsulta As New DataSet
                Dim query As String

                query = "select id_ms_reserva " +
                        "     , empleado_nom + ' ' + empleado_appat + ' ' + empleado_apmat as solicito " +
                        "     , autorizador_nom + ' ' + autorizador_appat + ' ' + autorizador_apmat as autorizador " +
                        "	  , prioridad " +
                        "	  , fecha_ini, fecha_fin " +
                        "	  , no_eco, placas " +
                        "	  , destino " +
                        "	  , case status " +
                        "	      when 'P' then 'Pendiente' " +
                        "		  when 'A' then 'Autorizado' " +
                        "		  when 'T' then 'Entregado al Usuario' " +
                        "		  when 'R' then 'Recibido por Vigilancia' " +
                        "		  when 'Z' then 'Rechazado' " +
                        "		  when 'ZM' then 'Cancelado' " +
                        "		  else '-' " +
                        "	    end as estado " +
                        "from ms_reserva " +
                        "where 1 =1  "

                If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or .cbSolicitante.Checked = True Then
                    query = query + "  and id_usr_solicito = @idSolicito "
                End If

                If .cbEmpresa.Checked = True Then
                    query = query + "  and empresa = @empresa "
                End If

                If .cbFechaC.Checked = True Then
                    query = query + "  and (fecha_solicito between @fechaIni and @fechaFin) "
                End If

                If .cbNoEco.Checked = True Then
                    query = query + "  and id_ms_vehiculo = @idMsVehiculo"
                End If

                If .cbNoSol.Checked = True Then
                    query = query + "  and id_ms_reserva = @idMsReserv"
                End If

                If .cbStatus.Checked = True Then
                    query = query + "  and status in (" + .ddlStatus.SelectedValue + ")"
                End If

                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)

                If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@idSolicito", Val(._txtIdUsuario.Text))
                End If
                If .cbSolicitante.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@idSolicito", .ddlSolicitante.SelectedValue)
                End If

                If .cbEmpresa.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedValue)
                End If

                If .cbFechaC.Checked = True Then
                    'Dim fechaB As Date = .wdpFechaF.Date.AddHours(23).AddMinutes(59).AddSeconds(59)

                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaIni", .wdpFechaI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaFin", .wdpFechaF.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
                    Dim cs = sdaConsulta.SelectCommand.ToString()


                End If

                If .cbNoEco.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@idMsVehiculo", .ddlNoEco.SelectedValue)
                End If

                If .cbNoSol.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@idMsReserv", .txtNoSol.Text.Trim)
                End If

                .gvRegistros.DataSource = dsConsulta
                ConexionBD.Open()
                sdaConsulta.Fill(dsConsulta)
                ConexionBD.Close()
                .gvRegistros.DataBind()
                sdaConsulta.Dispose()
                dsConsulta.Dispose()
                .gvRegistros.SelectedIndex = -1
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

#Region "Tabla - Solicitudes"

    Protected Sub gvRegistros_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRegistros.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                .pnlFiltros.Visible = False
                .pnlRegistros.Visible = False
                .pnlDetalle.Visible = False
                .pnlImp.Visible = False

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                .lblFolio.Text = .gvRegistros.SelectedRow.Cells(1).Text
                'Datos de la Reservación
                Dim sdaMsReserv As New SqlDataAdapter
                Dim dsMsReserv As New DataSet
                sdaMsReserv.SelectCommand = New SqlCommand("select empleado_nom + ' ' + empleado_appat + ' ' + empleado_apmat as solicito " + _
                                                           "     , autorizador_nom + ' ' + autorizador_appat + ' ' + autorizador_apmat as autorizador " + _
                                                           "	 , prioridad " + _
                                                           "	 , fecha_ini, fecha_fin " + _
                                                           "	 , no_eco + ' [' + placas + ']' as vehiculo " + _
                                                           "	 , destino, just, id_usr_solicito, status " + _
                                                           "from ms_reserva " + _
                                                           "where id_ms_reserva = @idMsReserv ", ConexionBD)
                sdaMsReserv.SelectCommand.Parameters.AddWithValue("@idMsReserv", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaMsReserv.Fill(dsMsReserv)
                ConexionBD.Close()
                ._txtIdSolicito.Text = dsMsReserv.Tables(0).Rows(0).Item("id_usr_solicito").ToString()
                .lblSolicitante.Text = dsMsReserv.Tables(0).Rows(0).Item("solicito").ToString()
                .lblAutorizador.Text = dsMsReserv.Tables(0).Rows(0).Item("autorizador").ToString()
                .lblPrioridad.Text = dsMsReserv.Tables(0).Rows(0).Item("prioridad").ToString()
                .wdteFechaIni.Date = CDate(dsMsReserv.Tables(0).Rows(0).Item("fecha_ini").ToString())
                .wdteFechaFin.Date = CDate(dsMsReserv.Tables(0).Rows(0).Item("fecha_fin").ToString())
                .lblVehiculo.Text = dsMsReserv.Tables(0).Rows(0).Item("vehiculo").ToString()
                .lblDestino.Text = dsMsReserv.Tables(0).Rows(0).Item("destino").ToString()
                .txtJust.Text = dsMsReserv.Tables(0).Rows(0).Item("just").ToString()

                If Val(._txtIdUsuario.Text) = Val(._txtIdSolicito.Text) And .wdteFechaFin.Date > Date.Now Then
                    .btnCancelar.Enabled = True
                Else
                    .btnCancelar.Enabled = False
                End If
                If dsMsReserv.Tables(0).Rows(0).Item("status").ToString() = "A" Then
                    .pnlImp.Visible = True
                    Session("idMsReserv") = .lblFolio.Text
                Else
                    .pnlImp.Visible = False
                End If

                sdaMsReserv.Dispose()
                dsMsReserv.Dispose()

                .pnlDetalle.Visible = True
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

#Region "Cancelar Reservación"

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "update ms_reserva " + _
                                         "  set status = 'ZM', id_usr_cancela = @id_usr_cancela, fecha_cancela = @fecha " + _
                                         "where id_ms_reserva = @idMsReserv "
                SCMValores.Parameters.AddWithValue("@idMsReserv", Val(Session("idMsReserv")))
                SCMValores.Parameters.AddWithValue("@id_usr_cancela", Val(._txtIdUsuario.Text))
                SCMValores.Parameters.AddWithValue("@fecha", Date.Now)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()
                .pnlImp.Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class