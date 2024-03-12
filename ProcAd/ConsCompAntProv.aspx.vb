Public Class ConsCompAntProv
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

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
                        sdaEmpleado.SelectCommand = New SqlCommand("select perfil, nombre + ' ' + ap_paterno + ' ' + ap_materno as empleado " +
                                                                   "from cg_usuario " +
                                                                   "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                                   "where id_usuario = @idUsuario ", ConexionBD)
                        sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        sdaEmpleado.Fill(dsEmpleado)
                        ConexionBD.Close()
                        ._txtPerfil.Text = dsEmpleado.Tables(0).Rows(0).Item("perfil").ToString()
                        ._txtEmpleado.Text = dsEmpleado.Tables(0).Rows(0).Item("empleado").ToString()
                        sdaEmpleado.Dispose()
                        dsEmpleado.Dispose()

                        Dim query As String

                        'Llenar lista de solicitantes
                        Dim sdaSolicitante As New SqlDataAdapter
                        Dim dsSolicitante As New DataSet
                        query = ""
                        query = " SELECT DISTINCT id_usr_solicita, empleado_solicita AS empleado" +
                                " FROM ms_comprobacion_anticipo " +
                                " ORDER BY empleado_solicita "
                        sdaSolicitante.SelectCommand = New SqlCommand(query, ConexionBD)
                        .ddlSolicitante.DataSource = dsSolicitante
                        .ddlSolicitante.DataTextField = "empleado"
                        .ddlSolicitante.DataValueField = "id_usr_solicita"
                        ConexionBD.Open()
                        sdaSolicitante.Fill(dsSolicitante)
                        .ddlSolicitante.DataBind()
                        ConexionBD.Close()
                        sdaSolicitante.Dispose()
                        dsSolicitante.Dispose()
                        .ddlSolicitante.SelectedIndex = -1

                        'Llenar lista de empresas 
                        Dim sdaEmpresa As New SqlDataAdapter
                        Dim dsEmpresa As New DataSet
                        query = ""
                        query = " SELECT DISTINCT(empresa) AS empresa " +
                                " FROM ms_comprobacion_anticipo " +
                                " ORDER BY empresa "
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

                        'Llenar lista de autorizadores
                        Dim sdaAutorizador As New SqlDataAdapter
                        Dim dsAutorizador As New DataSet
                        query = ""
                        query = " SELECT DISTINCT id_usr_autoriza, autorizador " +
                                " FROM ms_comprobacion_anticipo " +
                                " ORDER BY autorizador "
                        sdaAutorizador.SelectCommand = New SqlCommand(query, ConexionBD)

                        .ddlAutorizador.DataSource = dsAutorizador
                        .ddlAutorizador.DataTextField = "autorizador"
                        .ddlAutorizador.DataValueField = "id_usr_autoriza"
                        ConexionBD.Open()
                        sdaAutorizador.Fill(dsAutorizador)
                        .ddlAutorizador.DataBind()
                        ConexionBD.Close()
                        sdaAutorizador.Dispose()
                        dsAutorizador.Dispose()
                        .ddlAutorizador.SelectedIndex = -1

                        'Llenar lista de autorizador2
                        Dim sdaDirector As New SqlDataAdapter
                        Dim dsDirector As New DataSet
                        query = ""
                        query = " SELECT DISTINCT id_usr_autorizador2, autorizador2 " +
                                " FROM ms_comprobacion_anticipo " +
                                " ORDER BY autorizador2 "
                        sdaDirector.SelectCommand = New SqlCommand(query, ConexionBD)

                        .ddlDirector.DataSource = dsDirector
                        .ddlDirector.DataTextField = "autorizador2"
                        .ddlDirector.DataValueField = "id_usr_autorizador2"
                        ConexionBD.Open()
                        sdaDirector.Fill(dsDirector)
                        .ddlDirector.DataBind()
                        ConexionBD.Close()
                        sdaDirector.Dispose()
                        dsDirector.Dispose()
                        .ddlDirector.SelectedIndex = -1

                        'Llenar lista de autorizador3
                        Dim sdaAutorizador3 As New SqlDataAdapter
                        Dim dsautorizador3 As New DataSet
                        query = ""
                        query = " SELECT DISTINCT id_usr_autorizador3, autorizador3 " +
                                " FROM ms_comprobacion_anticipo " +
                                " ORDER BY autorizador3 "
                        sdaAutorizador3.SelectCommand = New SqlCommand(query, ConexionBD)

                        .ddlAutorizador3.DataSource = dsautorizador3
                        .ddlAutorizador3.DataTextField = "autorizador3"
                        .ddlAutorizador3.DataValueField = "id_usr_autorizador3"
                        ConexionBD.Open()
                        sdaAutorizador3.Fill(dsautorizador3)
                        .ddlAutorizador3.DataBind()
                        ConexionBD.Close()
                        sdaAutorizador3.Dispose()
                        dsautorizador3.Dispose()
                        .ddlAutorizador3.SelectedIndex = -1

                        limpiarPantalla()
                    Else
                        Server.Transfer("Login.aspx")
                    End If

                Catch ex As Exception

                End Try
            End With
        End If

    End Sub

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

    Protected Sub cbAutorizador_CheckedChanged(sender As Object, e As EventArgs) Handles cbAutorizador.CheckedChanged
        vista(Me.pnlAutorizador, Me.cbAutorizador.Checked)
    End Sub

    Protected Sub cbDirector_CheckedChanged(sender As Object, e As EventArgs) Handles cbDirector.CheckedChanged
        vista(Me.pnlDirector, Me.cbDirector.Checked)
    End Sub
    Protected Sub cbAutorizador3_CheckedChanged(sender As Object, e As EventArgs) Handles cbAutorizador3.CheckedChanged
        vista(Me.pnlAutorizador3, Me.cbAutorizador3.Checked)
    End Sub
    Protected Sub cbEstatus_CheckedChanged(sender As Object, e As EventArgs) Handles cbEstatus.CheckedChanged
        vista(Me.pnlEstatus, Me.cbEstatus.Checked)
    End Sub
    Protected Sub cbNoSol_CheckedChanged(sender As Object, e As EventArgs) Handles cbNoComp.CheckedChanged
        vista(Me.pnlNoComp, Me.cbNoComp.Checked)
        If Me.cbNoComp.Checked = True Then
            Me.txtNoComp.Text = ""
        End If
    End Sub

#End Region

#Region "Funciones"

    Public Sub limpiarPantalla()

        With Me
            Try
                '.pnlFiltros.Visible = True
                'Filtros
                .cbFechaC.Checked = False
                .pnlFechaC.Visible = False
                .wdpFechaI.Date = Date.Now.ToShortDateString
                .wdpFechaF.Date = Date.Now.ToShortDateString
                .cbSolicitante.Checked = False
                .pnlSolicitante.Visible = False
                .cbAutorizador.Checked = False
                .pnlAutorizador.Visible = False
                .cbEmpresa.Checked = False
                .pnlEmpresa.Visible = False
                .cbDirector.Checked = False
                .pnlDirector.Visible = False
                .cbAutorizador3.Checked = False
                .pnlAutorizador3.Visible = False
                .cbEstatus.Checked = False
                .pnlEstatus.Visible = False
                .cbNoComp.Checked = False
                .pnlNoComp.Visible = False
                'Ocultar resto de paneles principales
                .pnlRegistros.Visible = False
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

                query = "SELECT" +
                        " id_ms_comprobacion_anticipo, " +
                        " CASE " +
                        " WHEN tipo_escenario = 1 THEN 'Anticipo sin pedido de compra' " +
                        " WHEN tipo_escenario = 3 THEN 'Pago anticipado sin pedido de compra' " +
                        " WHEN tipo_escenario = 4 THEN 'Pago anticipado con pedido de compra' " +
                        " WHEN tipo_escenario = 5 THEN 'Pago anticipado agente aduanal' " +
                        " ELSE '-'" +
                        " END AS tipo_escenario, " +
                        " empleado_solicita, " +
                        " fecha_solicita, " +
                        " importe_anticipo, " +
                        " importe_comprobado, " +
                        " ISNULL(importe_devolucion, '-') AS importe_devolucion, " +
                        " empresa,  " +
                        "ISNULL(division, '-') AS division,  " +
                        "ISNULL(centro_costos, '-') AS centro_costos,  " +
                        "ISNULL(autorizador, '-') AS autorizador,  " +
                        "COALESCE(CONVERT(VARCHAR(30), fecha_autoriza), '-') AS fecha_autoriza,  " +
                        "ISNULL(autorizador2, '-') AS autorizador2,  " +
                        "COALESCE(CONVERT(VARCHAR(30), fecha_autoriza_2), '-') AS fecha_autoriza_2,  " +
                        "ISNULL(autorizador3, '-') AS autorizador3,  " +
                        "COALESCE(CONVERT(VARCHAR(30), fecha_autoriza_3), '-') AS fecha_autoriza_3,  " +
                        "COALESCE(CONVERT(VARCHAR(30), fecha_valida_cc), '-') AS fecha_valida_cc,  " +
                        "COALESCE(CONVERT(VARCHAR(30), fecha_valida_cxp), '-') AS fecha_valida_cxp, " +
                        "CASE  " +
                        "WHEN estatus = 'P' AND inst.id_actividad = 142 THEN 'Pendiente de autorización por autorizador'  " +
                        "WHEN estatus = 'A' AND inst.id_actividad = 143 AND fecha_autoriza IS NOT NULL THEN 'Autorizado por el autorizador' " +
                        "WHEN estatus = 'ZA' AND inst.id_actividad = 144 THEN 'Rechazado por el autorizador' " +
                        "WHEN estatus = 'A' AND inst.id_actividad = 143 AND fecha_autoriza_2 IS NOT NULL THEN 'Autorizado por el director' " +
                        "WHEN estatus = 'ZD' AND inst.id_actividad = 144 THEN 'Rechazado por el director'  " +
                        "WHEN estatus = 'A' AND inst.id_actividad = 143 AND fecha_autoriza_3 IS NOT NULL THEN 'Autorizado por el C' " +
                        "WHEN estatus = 'ZC' AND inst.id_actividad = 144 THEN 'Rechazado por el C' " +
                        "WHEN estatus = 'AF' AND inst.id_actividad = 149 THEN 'Validado en codificacion contable' " +
                        "WHEN estatus = 'RN' AND inst.id_actividad = 150 THEN 'Comprobado'  " +
                        "WHEN estatus = 'Z' AND inst.id_actividad = 151 THEN 'Rechazado por cuentas por pagar' " +
                        "ELSE '-' " +
                        "END AS estado " +
                        "FROM ms_comprobacion_anticipo  " +
                        "LEFT JOIN ms_instancia inst ON ms_comprobacion_anticipo.id_ms_comprobacion_anticipo = inst.id_ms_sol AND tipo = 'CAP' " +
                        "WHERE 1 = 1 "

                If cbSolicitante.Checked = True Then
                    query = query + " and id_usr_solicita = @id_usr_solicita"
                End If
                If .cbEmpresa.Checked = True Then
                    query = query + "  and empresa = @empresa "
                End If
                If .cbFechaC.Checked = True Then
                    query = query + "  and (fecha_solicita between @fechaIni and @fechaFin) "
                End If
                If .cbAutorizador.Checked = True Then
                    query = query + "  and id_usr_autoriza = @id_usr_autoriza "
                End If
                If .cbDirector.Checked = True Then
                    query = query + "  and id_usr_autorizador2  = @id_usr_autorizador2"
                End If
                If .cbNoComp.Checked = True Then
                    query = query + "  and id_ms_comprobacion_anticipo = @id_ms_comprobacion_anticipo "
                End If
                If .cbEstatus.Checked = True Then
                    query = query + "  and estatus = @estatus "
                End If
                query = query + "ORDER BY id_ms_comprobacion_anticipo "

                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)

                If .cbSolicitante.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_usr_solicita", .ddlSolicitante.SelectedItem.Value)
                End If
                If .cbEmpresa.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedItem.Value)
                End If
                If .cbFechaC.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaIni", .wdpFechaI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaFin", .wdpFechaF.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
                End If
                If .cbAutorizador.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_usr_autoriza", .ddlAutorizador.SelectedItem.Value)
                End If
                If .cbDirector.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_usr_autorizador2", .ddlDirector.SelectedItem.Value)
                End If
                If .cbAutorizador3.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_usr_autorizador3", .ddlAutorizador3.SelectedItem.Value)
                End If
                If .cbNoComp.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_ms_comprobacion_anticipo", .txtNoComp.Text.Trim)
                End If
                If .cbEstatus.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@estatus", .ddlEstatus.SelectedItem.Value)
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

#Region "Exportar"

    Protected Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        Dim tw As New System.IO.StringWriter
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        With Me
            Try
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment;filename=Consulta comprobacion de anticipos.xls")
                Response.ContentType = "application/vnd.ms-excel"
                .gvRegistros.Visible = True
                .gvRegistros.RenderControl(hw)
                .gvRegistros.Visible = False
                Response.Write(tw.ToString())
                Response.End()
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub

#End Region

End Class