Public Class ConsVehiculo
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 55  ' 1 - Liliana | 5 - Octavio | 45 - Lic Marco | 64 - Gerardo | 3 - Jessica Alfaro | 55 -  Javier Sanchez

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
                        ._txtEmpleado.Text = dsEmpleado.Tables(0).Rows(0).Item("empleado").ToString()
                        sdaEmpleado.Dispose()
                        dsEmpleado.Dispose()

                        'Llenar listas
                        'Catálogo de Uso Utilitario
                        Dim sdaUsoUtil As New SqlDataAdapter
                        Dim dsUsoUtil As New DataSet
                        .ddlUsoUtil.DataSource = dsUsoUtil
                        sdaUsoUtil.SelectCommand = New SqlCommand("select id_uso_utilitario, uso_utilitario " + _
                                                                  "from bd_Empleado.dbo.cg_uso_utilitario " + _
                                                                  "where status = 'A' " + _
                                                                  "order by uso_utilitario ", ConexionBD)
                        .ddlUsoUtil.DataTextField = "uso_utilitario"
                        .ddlUsoUtil.DataValueField = "id_uso_utilitario"
                        ConexionBD.Open()
                        sdaUsoUtil.Fill(dsUsoUtil)
                        .ddlUsoUtil.DataBind()
                        ConexionBD.Close()
                        sdaUsoUtil.Dispose()
                        dsUsoUtil.Dispose()
                        .ddlUsoUtil.SelectedIndex = -1

                        'Asignado a...
                        Dim sdaAsignadoA As New SqlDataAdapter
                        Dim dsAsignadoA As New DataSet
                        sdaAsignadoA.SelectCommand = New SqlCommand("select distinct(ms_vehiculo.id_empleado_asig) " + _
                                                                    "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno  as [Asignado a] " + _
                                                                    "from bd_Empleado.dbo.ms_vehiculo " + _
                                                                    "  left join bd_Empleado.dbo.cg_empleado on ms_vehiculo.id_empleado_asig = cg_empleado.id_empleado " + _
                                                                    "where ms_vehiculo.id_empleado_asig is not null " + _
                                                                    "order by [Asignado a] ", ConexionBD)
                        .ddlAsignadoA.DataSource = dsAsignadoA
                        .ddlAsignadoA.DataTextField = "Asignado a"
                        .ddlAsignadoA.DataValueField = "id_empleado_asig"
                        ConexionBD.Open()
                        sdaAsignadoA.Fill(dsAsignadoA)
                        .ddlAsignadoA.DataBind()
                        ConexionBD.Close()
                        sdaAsignadoA.Dispose()
                        dsAsignadoA.Dispose()
                        .ddlAsignadoA.SelectedIndex = -1

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
            .cbUsoUtil.Checked = False
            .pnlUsoUtil.Visible = False
            .cbAsignadoA.Checked = False
            .pnlAsignadoA.Visible = False
            .cbStatus.Checked = False
            .pnlStatus.Visible = False
            .cbNoEco.Checked = False
            .pnlNoEco.Visible = False
            .cbPlacas.Checked = False
            .pnlPlacas.Visible = False
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

    Protected Sub cbUsoUtil_CheckedChanged(sender As Object, e As EventArgs) Handles cbUsoUtil.CheckedChanged
        vista(Me.pnlUsoUtil, Me.cbUsoUtil.Checked)
    End Sub

    Protected Sub cbAsignadoA_CheckedChanged(sender As Object, e As EventArgs) Handles cbAsignadoA.CheckedChanged
        vista(Me.pnlAsignadoA, Me.cbAsignadoA.Checked)
    End Sub

    Protected Sub cbStatus_CheckedChanged(sender As Object, e As EventArgs) Handles cbStatus.CheckedChanged
        vista(Me.pnlStatus, Me.cbStatus.Checked)
    End Sub

    Protected Sub cbNoEco_CheckedChanged(sender As Object, e As EventArgs) Handles cbNoEco.CheckedChanged
        vista(Me.pnlNoEco, Me.cbNoEco.Checked)
        If Me.cbNoEco.Checked = True Then
            Me.txtNoEco.Text = ""
        End If
    End Sub

    Protected Sub cbPlacas_CheckedChanged(sender As Object, e As EventArgs) Handles cbPlacas.CheckedChanged
        vista(Me.pnlPlacas, Me.cbPlacas.Checked)
        If Me.cbPlacas.Checked = True Then
            Me.txtPlacas.Text = ""
        End If
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

                query = "select no_eco as [No. Económico] " + _
                        "     , ms_vehiculo.Marca " + _
                        "     , Modelo " + _
                        "     , ms_vehiculo.tipo_unidad as [Tipo Unidad] " + _
                        "     , ms_vehiculo.uso_utilitario as [Uso Utilitario] " + _
                        "     , Serie " + _
                        "     , Motor " + _
                        "     , Año " + _
                        "     , ms_vehiculo.ubicacion as [Ubicación] " + _
                        "     , ms_vehiculo.estado as Estado " + _
                        "     , Placas " + _
                        "     , ms_vehiculo.periodo_verif [Periodo Verificación] " + _
                        "     , verificacion_vig as [Vig. Verificación] " + _
                        "     , tarjeta_cir_vig as [Vig. Tarjeta Circulación] " + _
                        "     , ms_vehiculo.tipo_iave as [Tipo IAVE] " + _
                        "     , IAVE " + _
                        "     , mantto_fecha_ult as [Fecha Ult. Mantenimiento] " + _
                        "     , mantto_fecha_prox as [Fecha Prox. Mantenimiento] " + _
                        "     , mantto_fecha_prox_km as [Fecha Prox. Mantenimiento KM] " + _
                        "     , poliza_seguro as [Póliza Seguro] " + _
                        "     , poliza_inciso as [Póliza Inciso] " + _
                        "     , poliza_seguro_ini as [Inicio Póliza de Seguro] " + _
                        "     , poliza_seguro_vig as [Vig. Póliza de Seguro] " + _
                        "     , km_ultimo as [Último KM] " + _
                        "     , km_actual as [KM Actual] " + _
                        "     , no_tarjeta_edenred as [No. Tarjeta Edenred] " + _
                        "     , case when arrendadora is null then 'No' else 'Sí' end as Arrendado " + _
                        "     , arrendadora as Arrendadora " + _
                        "     , arrend_no_contrato as [No. de Contrato] " + _
                        "     , arrend_anexo as [Anexo] " + _
                        "     , arrend_tipo as [Tipo de Arrendamiento] " + _
                        "     , arrend_meses as [Meses de Arrendamiento] " + _
                        "     , arrend_fecha_ini as [Inicio de Arrendamiento] " + _
                        "     , arrend_fecha_fin as [Fin de Arrendamiento] " + _
                        "     , obs as Observaciones " + _
                        "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno  as [Asignado a] " + _
                        "     , case ms_vehiculo.status " + _
                        "         when 'A' then 'Activo' " + _
                        "         when 'M' then 'En Mantenimiento' " + _
                        "         when 'S' then 'Siniestrado' " + _
                        "         when 'PV' then 'Proceso de Venta' " + _
                        "         when 'RB' then 'Robo' " + _
                        "         when 'B' then 'Baja' " + _
                        "	   end as Estatus " + _
                        "from bd_Empleado.dbo.ms_vehiculo " + _
                        "  left join bd_Empleado.dbo.cg_empleado on ms_vehiculo.id_empleado_asig = cg_empleado.id_empleado " + _
                        "where ms_vehiculo.status in ('A', 'S', 'M', 'PV', 'RB') "

                If .cbUsoUtil.Checked = True Then
                    query = query + "  and ms_vehiculo.uso_utilitario = @usoUtilitario "
                End If
                If .cbAsignadoA.Checked = True Then
                    query = query + "  and ms_vehiculo.id_empleado_asig = @idEmplAsig "
                End If
                If .cbStatus.Checked = True Then
                    query = query + "  and ms_vehiculo.status = @estatus "
                End If
                If .cbNoEco.Checked = True Then
                    query = query + "  and ms_vehiculo.no_eco like '%' + @noEco + '%' "
                End If
                If .cbPlacas.Checked = True Then
                    query = query + "  and ms_vehiculo.placas like '%' + @placas + '%' "
                End If

                query = query + "order by no_eco "

                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)

                If .cbUsoUtil.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@usoUtilitario", .ddlUsoUtil.SelectedItem.Text)
                End If
                If .cbAsignadoA.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@idEmplAsig", .ddlAsignadoA.SelectedValue)
                End If
                If .cbStatus.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@estatus", .ddlStatus.SelectedValue)
                End If
                If .cbNoEco.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@noEco", .txtNoEco.Text.Trim)
                End If
                If .cbPlacas.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@placas", .txtPlacas.Text.Trim)
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
                Response.AddHeader("Content-Disposition", "attachment;filename=Registros.xls")
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