Public Class ConsFactIng
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
                        'Esto para saber si es cuentas por cobrar o por pagar'
                        ._txtPerfil.Text = dsEmpleado.Tables(0).Rows(0).Item("perfil").ToString()
                        ._txtEmpleado.Text = dsEmpleado.Tables(0).Rows(0).Item("empleado").ToString()
                        sdaEmpleado.Dispose()
                        dsEmpleado.Dispose()
                        'Llenar listas
                        Dim query As String
                        'Solicitantes
                        Dim sdaSolicitante As New SqlDataAdapter
                        Dim dsSolicitante As New DataSet
                        query = "select distinct nombre + ' ' + ap_paterno + ' ' + ap_materno as solicita, id_usr_solicita from ms_movimientos_internos " +
                            " inner join cg_usuario on cg_usuario.id_usuario = ms_movimientos_internos.id_usr_solicita " +
                            " left join bd_Empleado.dbo.cg_empleado BDE on BDE.id_empleado = cg_usuario.id_empleado"
                        sdaSolicitante.SelectCommand = New SqlCommand(query, ConexionBD)
                        .ddlSolicitante.DataSource = dsSolicitante
                        .ddlSolicitante.DataTextField = "solicita"
                        .ddlSolicitante.DataValueField = "id_usr_solicita"
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
                        query = "select distinct empresa from ms_movimientos_internos"
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

                        'Autorizadores
                        Dim sdaAutorizador As New SqlDataAdapter
                        Dim dsAutorizador As New DataSet
                        query = "select distinct nombre + ' ' + ap_paterno + ' ' + ap_materno as autorizador, id_usr_autoriza from ms_movimientos_internos " +
                            " inner join cg_usuario on cg_usuario.id_usuario = ms_movimientos_internos.id_usr_autoriza " +
                            " left join bd_Empleado.dbo.cg_empleado BDE on BDE.id_empleado = cg_usuario.id_empleado"
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

                        limpiar()
                    Else
                        Server.Transfer("Login.aspx")
                    End If
                Catch ex As Exception
                    litError.Text = ex.Message
                End Try
            End With
        End If
    End Sub


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
            .cbAutorizador.Checked = False
            .pnlAutorizador.Visible = False
            .cbNoFact.Checked = False
            .cbEmpresa.Checked = False
            .cbSolicitante.Checked = False
            pnlEmpresa.Visible = False
            pnlSolicitante.Visible = False
            .pnlNoFact.Visible = False
            pnlDatos.Visible = False


        End With
    End Sub

    Public Sub localizar()
        With Me
            Try
                Dim idMsInt As Integer = Val(gvRegistros.SelectedRow.Cells(1).Text)
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaSol As New SqlDataAdapter
                Dim dsSol As New DataSet
                Dim query As String
                query = " select " +
                              "(select nombre + ' ' + ap_paterno + ' ' + ap_materno from bd_Empleado.dbo.cg_empleado where id_empleado = CG.id_empleado) as solicita, " +
                              "  Case MSI.tipoM When 'CPP' then 'Cuentas Por Pagar'  " +
                              "  when 'CPC'then 'Cuentas Por Cobrar' " +
                              "  when 'CON' then 'Contabilidad' " +
                              "  end as TipoMovimiento,  " +
                              "MSI.empresa, " +
                              "MSI.admon_oper, " +
                              "(select nombre + ' ' + ap_paterno + ' ' + ap_materno from bd_Empleado.dbo.cg_empleado where id_empleado = CGU.id_empleado) as autoriza, " +
                              "MSI.centro_costo, " +
                              "MSI.division, " +
                              "MSI.especificaciones, " +
                              "MSI.id_ms_movimientos_internos, " +
                              "MSI.comentario_autoriza, " +
                              "MSI.comentario_codc " +
                              "From ms_movimientos_internos MSI " +
                              "Left Join ms_instancia MI on MI.id_ms_sol = MSI.id_ms_movimientos_internos " +
                              "Left Join cg_usuario CG on CG.id_usuario = MSI.id_usr_solicita " +
                              "Left Join cg_usuario CGU on CGU.id_usuario = MSI.id_usr_autoriza " +
                              "where id_ms_movimientos_internos = @id_ms_movimientos_internos and MI.tipo='MI'  "
                sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaSol.SelectCommand.Parameters.AddWithValue("@id_ms_movimientos_internos", Val(idMsInt))
                ConexionBD.Open()
                sdaSol.Fill(dsSol)
                ConexionBD.Close()
                lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("solicita").ToString()
                lblTipo.Text = dsSol.Tables(0).Rows(0).Item("TipoMovimiento").ToString()
                lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                lblAutorizador.Text = dsSol.Tables(0).Rows(0).Item("autoriza").ToString()
                lblCentroCosto.Text = dsSol.Tables(0).Rows(0).Item("centro_costo").ToString()
                lblDivision.Text = dsSol.Tables(0).Rows(0).Item("division").ToString()
                txtEspecificaciones.Text = dsSol.Tables(0).Rows(0).Item("especificaciones").ToString()
                txtEspecificaciones.Enabled = False
                txtCodCont.Text = dsSol.Tables(0).Rows(0).Item("comentario_codc").ToString()
                txtCodCont.Enabled = False
                txtVal.Text = dsSol.Tables(0).Rows(0).Item("comentario_autoriza").ToString()
                txtVal.Enabled = False

                If dsSol.Tables(0).Rows(0).Item("admon_oper").ToString = "Admon" Then
                    rbAdministrativo.Checked = True
                    rbAdministrativo.Enabled = False
                    rbOperativo.Enabled = False
                Else
                    rbOperativo.Checked = True
                    rbOperativo.Enabled = False
                    rbAdministrativo.Enabled = False
                End If

                'Llenar Grid de Evidencias'
                Dim sdaArchivos As New SqlDataAdapter
                Dim dsArchivos As New DataSet
                gvAdjuntos.DataSource = dsArchivos
                'Ruta Servidor Prueba 172.16.18.239'
                'Ruta Servidor Bueno 148.223.153.43'
                sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " +
                                                           "     , 'http://148.223.153.43/ProcAd/Evidencias MovLib/' + cast(id_ms_movimientos_internos as varchar(20)) + '-' + nombre as path " +
                                                           "from dt_archivo_movInt " +
                                                           "where id_ms_movimientos_internos = @idMsMovInt ", ConexionBD)
                sdaArchivos.SelectCommand.Parameters.AddWithValue("@idMsMovInt", Val(idMsInt))
                ConexionBD.Open()
                sdaArchivos.Fill(dsArchivos)
                .gvAdjuntos.DataBind()
                ConexionBD.Close()
                sdaArchivos.Dispose()
                dsArchivos.Dispose()
                .gvAdjuntos.SelectedIndex = -1

                'Grid de datos'
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                gvDatos.Columns(0).Visible = True
                gvDatos.DataSource = dsCatalogo
                query = " select * from dt_movimientos_int where id_ms_movimientos_internos = @id_ms_movimientos_internos "
                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_ms_movimientos_internos", Val(idMsInt))
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                gvDatos.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                gvDatos.Columns(0).Visible = False
                gvDatos.SelectedIndex = -1
            Catch ex As Exception
                litError.Text = ex.Message
            End Try
        End With
    End Sub
#End Region
#Region "Botones"
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        With Me
            Try
                .litError.Text = ""

                'Llenar el GridView
                'Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                'ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                'Dim sdaConsulta As New SqlDataAdapter
                'Dim dsConsulta As New DataSet
                'Dim query As String

                'query = "select " +
                '    " id_ms_movimientos_internos, " +
                '    " especificaciones, " +
                '    " fecha_solicita, " +
                '    " nombre + ' ' + ap_paterno + ' ' + ap_materno as solicita, " +
                '    " Case ms_movimientos_internos.admon_oper when 'Admon' then 'Administrativo' else 'Operativo' end as admon_oper, " +
                '    " empresa " +
                '    " from ms_movimientos_internos " +
                '    " inner join cg_usuario on cg_usuario.id_usuario = ms_movimientos_internos.id_usr_solicita " +
                '    " left join bd_Empleado.dbo.cg_empleado BDE on BDE.id_empleado = cg_usuario.id_empleado " +
                '    " where estatus = 'A' "

                'If .cbSolicitante.Checked = True Then
                '    query = query + "  and ms_movimientos_internos.id_usr_solicita = @id_usr_solicita "
                'End If
                'If .cbEmpresa.Checked = True Then
                '    query = query + "  and ms_movimientos_internos.empresa = @empresa "
                'End If
                'If .cbFechaC.Checked = True Then
                '    query = query + "  and (ms_movimientos_internos.fecha_solicita between @fechaIni and @fechaFin) "
                'End If
                'If .cbAutorizador.Checked = True Then
                '    query = query + "  and (ms_movimientos_internos.id_usr_autoriza = @id_usr_autoriza ) "
                'End If
                'If .cbNoFact.Checked = True Then
                '    query = query + "  and ms_movimientos_internos.id_ms_movimientos_internos = @id_ms_movimientos_internos "
                'End If
                'query = query + "order by ms_movimientos_internos.id_ms_movimientos_internos "
                'sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)
                'If .cbSolicitante.Checked = True Then
                '    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_usr_solicita", Val(ddlSolicitante.SelectedItem))
                'End If
                'If .cbEmpresa.Checked = True Then
                '    sdaConsulta.SelectCommand.Parameters.AddWithValue("@empresa", Val(ddlEmpresa.SelectedItem))
                'End If
                'If .cbFechaC.Checked = True Then
                '    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fecha_solicita", wdpFechaI.Date)
                '    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fecha_fin", wdpFechaI.Date)

                'End If
                'If .cbAutorizador.Checked = True Then
                '    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_usr_autoriza", Val(ddlAutorizador.SelectedItem))
                'End If
                'If .cbNoFact.Checked = True Then
                '    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_ms_movimientos_internos", Val(txtNoFact.Text))
                'End If
                '.gvRegistros.DataSource = dsConsulta
                'ConexionBD.Open()
                'sdaConsulta.Fill(dsConsulta)
                'ConexionBD.Close()
                '.gvRegistros.DataBind()
                'sdaConsulta.Dispose()
                'dsConsulta.Dispose()
                '.gvRegistros.SelectedIndex = -1
                'If .gvRegistros.Rows.Count = 0 Then
                '    .pnlRegistros.Visible = False
                '    .litError.Text = "No existe Registros para esos valores"
                'Else
                '    .pnlRegistros.Visible = True
                'End If
                'Llenar el GridView
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaConsulta As New SqlDataAdapter
                Dim dsConsulta As New DataSet
                Dim query As String

                'query = "select " +
                '    " id_ms_movimientos_internos, " +
                '    " especificaciones, " +
                '    " fecha_solicita, " +
                '    " nombre + ' ' + ap_paterno + ' ' + ap_materno as solicita, " +
                '    " Case ms_movimientos_internos.admon_oper when 'Admon' then 'Administrativo' else 'Operativo' end as admon_oper, " +
                '    " empresa , " +
                '    " Case estatus When 'P' then 'Pendiente de autorización' When 'A' Then 'Autorizado'  " +
                '    " end as estado " +
                '    " from ms_movimientos_internos " +
                '    " inner join cg_usuario on cg_usuario.id_usuario = ms_movimientos_internos.id_usr_solicita " +
                '    " left join bd_Empleado.dbo.cg_empleado BDE on BDE.id_empleado = cg_usuario.id_empleado " +
                '    " where 1 = 1 "
                query = "SELECT  id_ms_movimientos_internos, " +
                        " especificaciones, " +
                        " BDE.nombre + ' ' + BDE.ap_paterno + ' ' + BDE.ap_materno as solicita, " +
                        " BDEAUT.nombre + ' ' + BDEAUT.ap_paterno + ' ' + BDEAUT.ap_materno as autoriza, " +
                        " empresa, " +
                        " ISNULL(centro_costo, '-') AS centro_costo, " +
                        " ISNULL(division, '-') AS division, " +
                        " COALESCE(CONVERT(VARCHAR(30), fecha_solicita), '-') AS fecha_solicita, " +
                        " COALESCE(CONVERT(VARCHAR(30), fecha_autoriza), '-') AS fecha_autoriza, " +
                        " COALESCE(CONVERT(VARCHAR(30), fecha_codCont), '-') AS fecha_codCont, " +
                        " CASE ms_movimientos_internos.admon_oper WHEN 'Admon' THEN 'Administrativo' ELSE 'Operativo' END AS admon_oper, " +
                        " CASE " +
                        " WHEN estatus = 'P' AND inst.id_actividad = 124 THEN 'Pendiente de autorización' " +
                        " WHEN estatus = 'P' AND inst.id_actividad = 125 THEN 'Codificacion contable' " +
                        " WHEN estatus = 'Z' AND inst.id_actividad = 126 THEN 'Rechazado' " +
                        " WHEN estatus = 'A' AND inst.id_actividad = 127 THEN 'Registrado' " +
                        " ELSE '-' " +
                        " END AS estado " +
                        " FROM ms_movimientos_internos " +
                        " INNER JOIN cg_usuario ON cg_usuario.id_usuario = ms_movimientos_internos.id_usr_solicita " +
                        " INNER JOIN cg_usuario usrAut ON usrAut.id_usuario = ms_movimientos_internos.id_usr_autoriza " +
                        " LEFT JOIN bd_Empleado.dbo.cg_empleado BDE ON BDE.id_empleado = cg_usuario.id_empleado " +
                        " LEFT JOIN bd_Empleado.dbo.cg_empleado BDEAUT ON BDEAUT.id_empleado = usrAut.id_empleado " +
                        " LEFT JOIN ms_instancia inst ON ms_movimientos_internos.id_ms_movimientos_internos = inst.id_ms_sol AND inst.tipo = 'MI'" +
                        " WHERE 1 = 1"

                If .cbSolicitante.Checked = True Then
                    query = query + "  and ms_movimientos_internos.id_usr_solicita = @id_usr_solicita "
                End If
                If .cbEmpresa.Checked = True Then
                    query = query + "  and ms_movimientos_internos.empresa = @empresa "
                End If
                If .cbFechaC.Checked = True Then
                    query = query + "  and (ms_movimientos_internos.fecha_solicita between @fechaIni and @fechaFin) "
                End If
                If .cbAutorizador.Checked = True Then
                    query = query + "  and (ms_movimientos_internos.id_usr_autoriza = @id_usr_autoriza ) "
                End If
                If .cbNoFact.Checked = True Then
                    query = query + "  and ms_movimientos_internos.id_ms_movimientos_internos = @id_ms_movimientos_internos "
                End If
                query = query + "order by ms_movimientos_internos.id_ms_movimientos_internos "
                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)

                If .cbSolicitante.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_usr_solicita", Val(ddlSolicitante.SelectedValue))
                End If
                If .cbEmpresa.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedItem.Text)
                End If
                If .cbFechaC.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaIni", wdpFechaI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaFin", wdpFechaF.Date)

                End If
                If .cbAutorizador.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_usr_autoriza", Val(ddlAutorizador.SelectedValue))
                End If
                If .cbNoFact.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_ms_movimientos_internos", Val(txtNoFact.Text))
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
                litError.Text = ex.Message
            End Try
        End With
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        limpiar()
    End Sub

    Protected Sub gvRegistros_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRegistros.SelectedIndexChanged
        Try
            pnlFiltros.Visible = False
            pnlDatos.Visible = True
            gvRegistros.Visible = False
            localizar()
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "Filtros"

#End Region

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



    Protected Sub cbNoFact_CheckedChanged(sender As Object, e As EventArgs) Handles cbNoFact.CheckedChanged
        vista(Me.pnlNoFact, Me.cbNoFact.Checked)
        If Me.cbNoFact.Checked = True Then
            Me.txtNoFact.Text = ""
        End If
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
End Class