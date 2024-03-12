Public Class _37
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 1

                    .litError.Text = ""
                    .litMsgE.Text = ""
                    .upMsgE.Update()

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtBan.Text = 0

                        'Creación de Variables para la conexión y consulta de información a la base de datos
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        'Datos del Solicitante
                        Dim sdaEmpleado As New SqlDataAdapter
                        Dim dsEmpleado As New DataSet
                        sdaEmpleado.SelectCommand = New SqlCommand("select cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " +
                                                                   "     , puesto_tabulador " +
                                                                   "     , cgCC.id_cc " +
                                                                   "     , cgCC.id_empresa " +
                                                                   "     , cg_usuario.omitir_PGV " +
                                                                   "     , hospedaje_libre " +
                                                                   "from cg_usuario " +
                                                                   "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                                   "  left join bd_Empleado.dbo.cg_cc cgCC on cgEmpl.id_cc = cgCC.id_cc " +
                                                                   "where id_usuario = @idUsuario ", ConexionBD)
                        sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        sdaEmpleado.Fill(dsEmpleado)
                        ConexionBD.Close()
                        .lblSolicitante.Text = dsEmpleado.Tables(0).Rows(0).Item("nombre_empleado").ToString()
                        ._txtComodin.Text = dsEmpleado.Tables(0).Rows(0).Item("puesto_tabulador").ToString()
                        ._txtIdEmpresaEmpl.Text = dsEmpleado.Tables(0).Rows(0).Item("id_empresa").ToString()
                        ._txtIdCCEmpl.Text = dsEmpleado.Tables(0).Rows(0).Item("id_cc").ToString()
                        ._txtOmitValPGV.Text = dsEmpleado.Tables(0).Rows(0).Item("omitir_PGV").ToString()
                        If dsEmpleado.Tables(0).Rows(0).Item("hospedaje_libre").ToString() = "N" Then
                            .wneDiasH.Enabled = False
                        Else
                            .wneDiasH.Enabled = True
                        End If
                        sdaEmpleado.Dispose()
                        dsEmpleado.Dispose()

                        'Validar si está autorizado para solicitar anticipos en Efectivo
                        Dim banEf As Integer = 0
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "select count(*) " +
                                                 "where @idUsuario in (select split.a.value('.', 'NVARCHAR(MAX)') data " +
                                                 "                     from (select cast('<X>' + replace((select valor from cg_parametros where parametro = 'anticipo_efectivo'), ',', '</X><X>') + '</X>' as xml) as string) as A " +
                                                 "                       cross apply string.nodes('/X') as split(a)) "
                        SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        banEf = SCMValores.ExecuteScalar()
                        ConexionBD.Close()
                        If banEf > 0 Then
                            .rblTipoPago.Enabled = True
                        Else
                            .rblTipoPago.Enabled = False
                        End If

                        'Lista de Empresas
                        Dim sdaEmpresa As New SqlDataAdapter
                        Dim dsEmpresa As New DataSet
                        sdaEmpresa.SelectCommand = New SqlCommand("select Empresa.id_empresa, Empresa.nombre " +
                                                                  "from bd_empleado.dbo.cg_empresa Empresa " +
                                                                  "  inner join bd_empleado.dbo.dt_empleado_prov dtCod on Empresa.id_empresa = dtCod.id_empresa " +
                                                                  "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on dtCod.id_empleado = cgEmpl.id_empleado " +
                                                                  "  inner join cg_usuario on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                                  "where Empresa.status = 'A' " +
                                                                  "  and cg_usuario.id_usuario = @idUsuario " +
                                                                  "order by Empresa.nombre ", ConexionBD)
                        sdaEmpresa.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        .ddlEmpresa.DataSource = dsEmpresa
                        .ddlEmpresa.DataTextField = "nombre"
                        .ddlEmpresa.DataValueField = "id_empresa"
                        ConexionBD.Open()
                        sdaEmpresa.Fill(dsEmpresa)
                        .ddlEmpresa.DataBind()
                        ConexionBD.Close()
                        sdaEmpresa.Dispose()
                        dsEmpresa.Dispose()

                        'Validar si la Empresa está en la lista
                        Dim ban As Integer = 0
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "select count(*) " +
                                                 "from bd_empleado.dbo.cg_empresa Empresa " +
                                                 "  inner join bd_empleado.dbo.dt_empleado_prov dtCod on Empresa.id_empresa = dtCod.id_empresa " +
                                                 "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on dtCod.id_empleado = cgEmpl.id_empleado " +
                                                 "  inner join cg_usuario on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                 "where Empresa.status = 'A' " +
                                                 "  and cg_usuario.id_usuario = @idUsuario " +
                                                 "  and Empresa.id_empresa = @idEmpresa "
                        SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@idEmpresa", Val(._txtIdEmpresaEmpl.Text))
                        ConexionBD.Open()
                        ban = SCMValores.ExecuteScalar()
                        ConexionBD.Close()
                        If ban = 0 Then
                            .ddlEmpresa.SelectedIndex = -1
                        Else
                            .ddlEmpresa.SelectedValue = Val(._txtIdEmpresaEmpl.Text)
                        End If

                        'Actualizar Centros de Costo
                        actCC()

                        'Número de Empleado
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "select isnull((select dtNoProv.no_proveedor " +
                                                 "			     from cg_usuario " +
                                                 "			      inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado and cgEmpl.status = 'A' " +
                                                 "			      inner join bd_Empleado.dbo.dt_empleado_prov dtNoProv on cgEmpl.id_empleado = dtNoProv.id_empleado and dtNoProv.status = 'A' " +
                                                 "	    		 where id_usuario = @idUsuario " +
                                                 "				   and id_empresa = @idEmpresa), '') as no_proveedor "
                        SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@idEmpresa", .ddlEmpresa.SelectedValue)
                        ConexionBD.Open()
                        .lblNoProveedor.Text = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        'Lista de Autorizadores
                        Dim sdaAut As New SqlDataAdapter
                        Dim dsAut As New DataSet
                        .ddlAutorizador.DataSource = dsAut
                        sdaAut.SelectCommand = New SqlCommand("select cgEmpl.id_empleado, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " +
                                                              "from dt_autorizador " +
                                                              "  left join cg_usuario on dt_autorizador.id_autorizador = cg_usuario.id_usuario " +
                                                              "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado and cgEmpl.status = 'A' " +
                                                              "where dt_autorizador.id_usuario = @idUsuario " +
                                                              "  and cg_usuario.status = 'A' " +
                                                              "order by aut_dir, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno ", ConexionBD)
                        sdaAut.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        .ddlAutorizador.DataTextField = "nombre_empleado"
                        .ddlAutorizador.DataValueField = "id_empleado"
                        ConexionBD.Open()
                        sdaAut.Fill(dsAut)
                        .ddlAutorizador.DataBind()
                        ConexionBD.Close()
                        sdaAut.Dispose()
                        dsAut.Dispose()
                        .ddlAutorizador.SelectedIndex = -1

                        'Lista de Directores
                        Dim sdaDir As New SqlDataAdapter
                        Dim dsDir As New DataSet
                        .ddlDirector.DataSource = dsDir
                        sdaDir.SelectCommand = New SqlCommand("select cgEmpl.id_empleado, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " +
                                                              "from dt_autorizador " +
                                                              "  left join cg_usuario on dt_autorizador.id_autorizador = cg_usuario.id_usuario " +
                                                              "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado and cgEmpl.status = 'A' " +
                                                              "where dt_autorizador.id_usuario = @idUsuario " +
                                                              "  and dt_autorizador.aut_dir = 'S'  " +
                                                              "  and cg_usuario.status = 'A' " +
                                                              "order by aut_dir, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno ", ConexionBD)
                        sdaDir.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        .ddlDirector.DataTextField = "nombre_empleado"
                        .ddlDirector.DataValueField = "id_empleado"
                        ConexionBD.Open()
                        sdaDir.Fill(dsDir)
                        .ddlDirector.DataBind()
                        ConexionBD.Close()
                        sdaDir.Dispose()
                        dsDir.Dispose()
                        .ddlDirector.SelectedIndex = -1

                        .ddlDirector.Visible = False
                        .lbl_Director.Visible = False


                        'Limite de anticipo
                        Dim sdaLimAnt As New SqlDataAdapter
                        Dim dsDLimAnt As New DataSet
                        sdaLimAnt.SelectCommand = New SqlCommand(" SELECT valor  FROM cg_parametros WHERE parametro = 'Limt_anticipo'  ", ConexionBD)
                        ConexionBD.Open()
                        sdaLimAnt.Fill(dsDLimAnt)
                        ConexionBD.Close()
                        ._txtLim_anticipo.Text = dsDLimAnt.Tables(0).Rows(0).Item("valor").ToString()
                        sdaLimAnt.Dispose()
                        dsDLimAnt.Dispose()

                        'Lista de Vehículos para Combustible
                        Dim sdaVehiC As New SqlDataAdapter
                        Dim dsVehiC As New DataSet
                        .ddlVehiculoC.DataSource = dsVehiC
                        sdaVehiC.SelectCommand = New SqlCommand("select id_ms_vehiculo, no_eco + ' [' + modelo + ']' as vehiculo " +
                                                                "from bd_Empleado.dbo.ms_vehiculo " +
                                                                "where status = 'A' " +
                                                                "  and poliza_seguro_vig > GETDATE() " +
                                                                "  and tarjeta_cir_vig > GETDATE() " +
                                                                "  and (uso_utilitario = 'COMODIN' " +
                                                                "    or ms_vehiculo.id_empleado_asig in (select id_empleado " +
                                                                "                                        from cg_usuario " +
                                                                "                                        where cg_usuario.id_usuario = @id_usuario)) ", ConexionBD)
                        sdaVehiC.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                        .ddlVehiculoC.DataTextField = "vehiculo"
                        .ddlVehiculoC.DataValueField = "id_ms_vehiculo"
                        ConexionBD.Open()
                        sdaVehiC.Fill(dsVehiC)
                        .ddlVehiculoC.DataBind()
                        ConexionBD.Close()
                        sdaVehiC.Dispose()
                        dsVehiC.Dispose()
                        .ddlVehiculoC.SelectedIndex = -1
                        actDatosVehiC()

                        'Lista de Origenes / Destinos
                        Dim sdaLugar As New SqlDataAdapter
                        Dim dsLugar As New DataSet
                        .ddlOrig.DataSource = dsLugar
                        .ddlDest.DataSource = dsLugar
                        sdaLugar.SelectCommand = New SqlCommand("select id_lugar " +
                                                                "     , lugar " +
                                                                "from cg_lugar " +
                                                                "where status = 'A' " +
                                                                "order by lugar ", ConexionBD)
                        .ddlOrig.DataTextField = "lugar"
                        .ddlDest.DataTextField = "lugar"
                        .ddlOrig.DataValueField = "id_lugar"
                        .ddlDest.DataValueField = "id_lugar"
                        ConexionBD.Open()
                        sdaLugar.Fill(dsLugar)
                        .ddlOrig.DataBind()
                        .ddlDest.DataBind()
                        ConexionBD.Close()
                        sdaLugar.Dispose()
                        dsLugar.Dispose()
                        .ddlOrig.SelectedIndex = -1
                        .ddlDest.SelectedIndex = -1

                        'Limpiar Campos de Cabecera
                        .txtDestino.Text = ""
                        .txtJust.Text = ""
                        .wdpPeriodoIni.Date = Now.Date
                        .wdpPeriodoFin.Date = Now.Date
                        .cblRecursos.Items(0).Selected = False
                        .cblRecursos.Items(1).Selected = False
                        .cblRecursos.Items(2).Selected = False
                        .ddlTipoTransp.SelectedIndex = -1

                        'Ocultar paneles de recursos
                        .pnlAnticipo.Visible = False
                        .upAnticipo.Update()
                        .pnlVehiculo.Visible = False
                        .upVehiculo.Update()
                        .pnlCombustible.Visible = False
                        .upCombustible.Update()
                        .pnlAvion.Visible = False
                        .upAvion.Update()

                        'Limpiar campos de Anticipos
                        .wneDiasH.Text = ""
                        .wceMontoH.Text = ""
                        .wneDiasA.Text = ""
                        .wceMontoA.Text = ""
                        .wneDiasC.Text = ""
                        .wceMontoC.Text = ""
                        .wneDiasO.Text = ""
                        .wceMontoO.Text = ""
                        .txtOtros.Text = ""
                        .wceMontoT.Text = ""
                        .lblMontoTLetra.Text = ""
                        .rblTipoPago.SelectedIndex = 1

                        'Limpiar campos de Reserva de Vehículo
                        .wdpFechaIni.Date = Date.Now
                        .wdpFechaFin.Date = Date.Now.AddHours(1)
                        'llenarVehiculos()

                        'Limpiar campos de Combustible
                        .wneLitros.Text = ""
                        .wceImporte.Text = ""

                        'Botones
                        .btnValidar.Enabled = True
                        .btnEnviar.Enabled = False
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

#Region "Cabecera"

    Protected Sub ddlEmpresa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEmpresa.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                'Número de Empleado
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select isnull((select dtNoProv.no_proveedor " +
                                         "			     from cg_usuario " +
                                         "			      inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                         "			      inner join bd_Empleado.dbo.dt_empleado_prov dtNoProv on cgEmpl.id_empleado = dtNoProv.id_empleado " +
                                         "	    		 where id_usuario = @idUsuario " +
                                         "				   and id_empresa = @idEmpresa), '') as no_proveedor "
                SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                SCMValores.Parameters.AddWithValue("@idEmpresa", .ddlEmpresa.SelectedValue)
                ConexionBD.Open()
                .lblNoProveedor.Text = SCMValores.ExecuteScalar()
                ConexionBD.Close()
                .upNoProveedor.Update()

                'Actualizar Centros de Costo
                actCC()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub cblRecursos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cblRecursos.SelectedIndexChanged
        With Me
            Dim msgE As String = ""
            'Anticipos
            If .cblRecursos.Items(0).Selected = True Then
                If valNoProv() Then
                    'Activar Panel
                    .pnlAnticipo.Visible = True
                Else
                    .cblRecursos.Items(0).Selected = False
                    msgE = "Sin Código de Proveedor NAV, favor de validarlo con el Administrador del SiCEm y Nómina"
                End If
            Else
                .pnlAnticipo.Visible = False
            End If
            .upAnticipo.Update()

            'Reserva de Vehículo
            If .cblRecursos.Items(1).Selected = True Then
                If valLic() Then
                    If valCurso() Then
                        'Activar Panel
                        .pnlVehiculo.Visible = True
                        llenarVehiculos()
                    Else
                        .cblRecursos.Items(1).Selected = False
                        msgE = "Curso de Manejo a la Defensiva no Aprobado, favor de validarlo con el Administrador del SiCEm"
                    End If
                Else
                    .cblRecursos.Items(1).Selected = False
                    msgE = "Licencia Expirada, favor de validarlo con el Administrador del SiCEm"
                End If
            Else
                .pnlVehiculo.Visible = False
            End If
            .upVehiculo.Update()

            'Combustible
            If .cblRecursos.Items(2).Selected = True Then
                If valLic() Then
                    If valCurso() Then
                        'Activar Panel
                        .pnlCombustible.Visible = True
                        If .cblRecursos.Items(1).Selected = True Then
                            'Existe Reserva de Vehículo, por lo que se muestra la etiqueta y no la lista
                            .ddlVehiculoC.Visible = False
                            .lblVehiculoC.Visible = True
                        Else
                            'No existe Reserva de Vehículo, por lo que se muestra la lista y se oculta la etiqueta
                            .ddlVehiculoC.Visible = True
                            .lblVehiculoC.Visible = False
                        End If
                    Else
                        .cblRecursos.Items(2).Selected = False
                        msgE = "Curso de Manejo a la Defensiva no Aprobado, favor de validarlo con el Administrador del SiCEm"
                    End If
                Else
                    .cblRecursos.Items(2).Selected = False
                    msgE = "Licencia Expirada, favor de validarlo con el Administrador del SiCEm"
                End If
            Else
                .pnlCombustible.Visible = False
            End If
            .upCombustible.Update()

            'Avión
            If .cblRecursos.Items(3).Selected = True Then
                'Activar Panel
                .pnlAvion.Visible = True
                .pnlJust.Visible = False
            Else
                .pnlAvion.Visible = False
            End If
            .upAvion.Update()

            .litMsgE.Text = msgE
            .upMsgE.Update()
        End With
    End Sub

#End Region

#Region "Funciones"

#Region "Centro de Costo"

    Public Sub actCC()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")

                'Centro de Costo
                Dim sdaCentroCosto As New SqlDataAdapter
                Dim dsCentroCosto As New DataSet
                sdaCentroCosto.SelectCommand = New SqlCommand("select id_cc, nombre " +
                                                              "from bd_Empleado.dbo.cg_cc " +
                                                              "where id_empresa = @idEmpresa " +
                                                              "  and status = 'A' " +
                                                              "order by nombre ", ConexionBD)
                sdaCentroCosto.SelectCommand.Parameters.AddWithValue("@idEmpresa", .ddlEmpresa.SelectedValue)
                .ddlCC.DataSource = dsCentroCosto
                .ddlCC.DataTextField = "nombre"
                .ddlCC.DataValueField = "id_cc"
                ConexionBD.Open()
                sdaCentroCosto.Fill(dsCentroCosto)
                .ddlCC.DataBind()
                ConexionBD.Close()
                sdaCentroCosto.Dispose()
                dsCentroCosto.Dispose()
                .ddlCC.Visible = True

                'Validar si el CC del Empleado está en la lista
                Dim ban As Integer = 0
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select count(*) as valor " +
                                         "from bd_Empleado.dbo.cg_cc " +
                                         "where id_empresa = @idEmpresa " +
                                         "  and status = 'A' " +
                                         "  and id_cc = @idCCEmpl "
                SCMValores.Parameters.AddWithValue("@idEmpresa", .ddlEmpresa.SelectedValue)
                SCMValores.Parameters.AddWithValue("@idCCEmpl", Val(._txtIdCCEmpl.Text))
                ConexionBD.Open()
                ban = SCMValores.ExecuteScalar()
                ConexionBD.Close()
                If ban > 0 Then
                    .ddlCC.SelectedValue = Val(._txtIdCCEmpl.Text)
                Else
                    .ddlCC.SelectedIndex = -1
                End If
                .upCC.Update()

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Anticipo"

    Public Function montoLetra()
        Dim resto As Double = 0
        Dim temp As String = ""
        Dim op As Integer
        resto = Me.wceMontoT.Value

        If resto >= 100000.0 Then
            op = Val(CStr(resto).Substring(0, 1))
            resto = resto - CDbl(op * 100000.0)
            Select Case op
                Case 1
                    If resto < 1000.0 Then
                        temp = temp + " Cien"
                    Else
                        temp = temp + " Ciento"
                    End If
                Case 2
                    temp = temp + " Doscientos"
                Case 3
                    temp = temp + " Trescientos"
                Case 4
                    temp = temp + " Cuatrocientos"
                Case 5
                    temp = temp + " Quinientos"
                Case 6
                    temp = temp + " Seiscientos"
                Case 7
                    temp = temp + " Setecientos"
                Case 8
                    temp = temp + " Ochocientos"
                Case 9
                    temp = temp + " Novecientos"
            End Select
        End If

        If resto >= 10000.0 Then
            op = Val(CStr(resto).Substring(0, 1))
            resto = resto - CDbl(op * 10000.0)
            Select Case op
                Case 1
                    If resto < 1000 Then
                        temp = temp + " Diez"
                    Else
                        op = Val(CStr(resto).Substring(0, 1))
                        resto = resto - (op * 1000.0)
                        Select Case op
                            Case 1
                                temp = temp + " Once"
                            Case 2
                                temp = temp + " Doce"
                            Case 3
                                temp = temp + " Trece"
                            Case 4
                                temp = temp + " Catorce"
                            Case 5
                                temp = temp + " Quince"
                            Case 6
                                temp = temp + " Dieciséis"
                            Case 7
                                temp = temp + " Diecisiete"
                            Case 8
                                temp = temp + " Dieciocho"
                            Case 9
                                temp = temp + " Diecinueve"
                        End Select
                    End If
                Case 2
                    If resto < 1000.0 Then
                        temp = temp + " Veinte"
                    Else
                        op = Val(CStr(resto).Substring(0, 1))
                        resto = resto - CDbl(op * 1000.0)
                        Select Case op
                            Case 1
                                temp = temp + " Veintiuno"
                            Case 2
                                temp = temp + " Veintidós"
                            Case 3
                                temp = temp + " Veintitrés"
                            Case 4
                                temp = temp + " Veinticuatro"
                            Case 5
                                temp = temp + " Veinticinco"
                            Case 6
                                temp = temp + " Veintiséis"
                            Case 7
                                temp = temp + " Veintisiete"
                            Case 8
                                temp = temp + " Veintiocho"
                            Case 9
                                temp = temp + " Veintinueve"
                        End Select
                    End If
                Case 3
                    If resto < 1000.0 Then
                        temp = temp + " Treinta"
                    Else
                        temp = temp + " Treinta y"
                    End If
                Case 4
                    If resto < 1000.0 Then
                        temp = temp + " Cuarenta"
                    Else
                        temp = temp + " Cuarenta y"
                    End If
                Case 5
                    If resto < 1000.0 Then
                        temp = temp + " Cincuenta"
                    Else
                        temp = temp + " Cincuenta y"
                    End If
                Case 6
                    If resto < 1000.0 Then
                        temp = temp + " Sesenta"
                    Else
                        temp = temp + " Sesenta y"
                    End If
                Case 7
                    If resto < 1000.0 Then
                        temp = temp + " Setenta"
                    Else
                        temp = temp + " Setenta y"
                    End If
                Case 8
                    If resto < 1000.0 Then
                        temp = temp + " Ochenta"
                    Else
                        temp = temp + " Ochenta y"
                    End If
                Case 9
                    If resto < 1000.0 Then
                        temp = temp + " Noventa"
                    Else
                        temp = temp + " Noventa y"
                    End If
            End Select
        End If

        If resto >= 1000.0 Then
            op = Val(CStr(resto).Substring(0, 1))
            resto = resto - CDbl(op * 1000.0)
            Select Case op
                Case 1
                    temp = temp + " Un"
                Case 2
                    temp = temp + " Dos"
                Case 3
                    temp = temp + " Tres"
                Case 4
                    temp = temp + " Cuatro"
                Case 5
                    temp = temp + " Cinco"
                Case 6
                    temp = temp + " Seis"
                Case 7
                    temp = temp + " Siete"
                Case 8
                    temp = temp + " Ocho"
                Case 9
                    temp = temp + " Nueve"
            End Select
        End If
        If temp <> "" Then
            temp = temp + " Mil"
        End If

        If resto >= 100.0 Then
            op = Val(CStr(resto).Substring(0, 1))
            resto = resto - CDbl(op * 100.0)
            Select Case op
                Case 1
                    If resto < 1.0 Then
                        temp = temp + " Cien"
                    Else
                        temp = temp + " Ciento"
                    End If
                Case 2
                    temp = temp + " Doscientos"
                Case 3
                    temp = temp + " Trescientos"
                Case 4
                    temp = temp + " Cuatrocientos"
                Case 5
                    temp = temp + " Quinientos"
                Case 6
                    temp = temp + " Seiscientos"
                Case 7
                    temp = temp + " Setecientos"
                Case 8
                    temp = temp + " Ochocientos"
                Case 9
                    temp = temp + " Novecientos"
            End Select
        End If

        If resto >= 10.0 Then
            op = Val(CStr(resto).Substring(0, 1))
            resto = resto - CDbl(op * 10.0)
            Select Case op
                Case 1
                    If resto < 1 Then
                        temp = temp + " Diez"
                    Else
                        op = Val(CStr(resto).Substring(0, 1))
                        resto = resto - CDbl(op)
                        Select Case op
                            Case 1
                                temp = temp + " Once"
                            Case 2
                                temp = temp + " Doce"
                            Case 3
                                temp = temp + " Trece"
                            Case 4
                                temp = temp + " Catorce"
                            Case 5
                                temp = temp + " Quince"
                            Case 6
                                temp = temp + " Dieciséis"
                            Case 7
                                temp = temp + " Diecisiete"
                            Case 8
                                temp = temp + " Dieciocho"
                            Case 9
                                temp = temp + " Diecinueve"
                        End Select
                    End If
                Case 2
                    If resto < 1.0 Then
                        temp = temp + " Veinte"
                    Else
                        op = Val(CStr(resto).Substring(0, 1))
                        resto = resto - CDbl(op)
                        Select Case op
                            Case 1
                                temp = temp + " Veintiuno"
                            Case 2
                                temp = temp + " Veintidós"
                            Case 3
                                temp = temp + " Veintitrés"
                            Case 4
                                temp = temp + " Veinticuatro"
                            Case 5
                                temp = temp + " Veinticinco"
                            Case 6
                                temp = temp + " Veintiséis"
                            Case 7
                                temp = temp + " Veintisiete"
                            Case 8
                                temp = temp + " Veintiocho"
                            Case 9
                                temp = temp + " Veintinueve"
                        End Select
                    End If
                Case 3
                    If resto < 1.0 Then
                        temp = temp + " Treinta"
                    Else
                        temp = temp + " Treinta y"
                    End If
                Case 4
                    If resto < 1.0 Then
                        temp = temp + " Cuarenta"
                    Else
                        temp = temp + " Cuarenta y"
                    End If
                Case 5
                    If resto < 1.0 Then
                        temp = temp + " Cincuenta"
                    Else
                        temp = temp + " Cincuenta y"
                    End If
                Case 6
                    If resto < 1.0 Then
                        temp = temp + " Sesenta"
                    Else
                        temp = temp + " Sesenta y"
                    End If
                Case 7
                    If resto < 1.0 Then
                        temp = temp + " Setenta"
                    Else
                        temp = temp + " Setenta y"
                    End If
                Case 8
                    If resto < 1.0 Then
                        temp = temp + " Ochenta"
                    Else
                        temp = temp + " Ochenta y"
                    End If
                Case 9
                    If resto < 1.0 Then
                        temp = temp + " Noventa"
                    Else
                        temp = temp + " Noventa y"
                    End If
            End Select
        End If

        If resto >= 1.0 Then
            op = Val(CStr(resto).Substring(0, 1))
            resto = resto - CDbl(op)
            Select Case op
                Case 1
                    temp = temp + " Un"
                Case 2
                    temp = temp + " Dos"
                Case 3
                    temp = temp + " Tres"
                Case 4
                    temp = temp + " Cuatro"
                Case 5
                    temp = temp + " Cinco"
                Case 6
                    temp = temp + " Seis"
                Case 7
                    temp = temp + " Siete"
                Case 8
                    temp = temp + " Ocho"
                Case 9
                    temp = temp + " Nueve"
            End Select
        End If

        If temp <> "" Then
            If Me.wceMontoT.Value = 1 And op = 1 Then
                temp = temp + " peso"
            Else
                temp = temp + " pesos"
            End If
        End If

        If resto = 0.0 Then
            temp = temp + " 00"
        Else
            If CInt(resto * 100).ToString() < 10 Then
                temp = temp + " 0" + CInt(resto * 100).ToString()
            Else
                temp = temp + " " + CInt(resto * 100).ToString()
            End If
        End If
        temp = temp + "/100 M.N. "
        temp = temp.ToUpper()
        Return temp
    End Function

    Function valAnticipo()
        With Me
            Dim ban As Integer = 0
            Dim nochesE As Integer = 0
            If (.wneDiasA.Text = "" And .wceMontoA.Text <> "") Or (.wneDiasA.Text <> "" And .wceMontoA.Text = "") Or (.wneDiasC.Text = "" And .wceMontoC.Text <> "") Or (.wneDiasC.Text <> "" And .wceMontoC.Text = "") Or (.wneDiasO.Text = "" And .wceMontoO.Text <> "") Or (.wneDiasO.Text <> "" And .wceMontoO.Text = "") Or (.wceMontoH.Text = "" And .wceMontoA.Text = "" And .wceMontoC.Text = "" And .wceMontoO.Text = "") Then
                .litError.Text = "el número de días y/o montos"
                ban = 1
            End If
            If (.wneDiasO.Value > 0 Or .wceMontoO.Value > 0) And .txtOtros.Text = "" Then
                If ban = 0 Then
                    ban = 1
                Else
                    .litError.Text = .litError.Text + ", "
                End If
                .litError.Text = .litError.Text + "el concepto de Otros"

            End If
            If wneDiasH.Value = 0 And wceMontoH.Value > 0 Then
                ban = 1
                litError.Text = "El numero de dias no puede ser cero y tener un monto"
                nochesE = 1
            End If
            If ban = 0 Then
                Dim banH As Integer = 0
                If .wceMontoH.Text <> "" Then
                    Dim diasPeriodo As Integer
                    diasPeriodo = DateDiff(DateInterval.Day, .wdpPeriodoIni.Date, .wdpPeriodoFin.Date)
                    'Validar Noches Hospedaje
                    If .wneDiasH.Value > diasPeriodo Then
                        .litError.Text = "No. noches hospedaje inválido, favor de verificarlo"
                        banH = 1
                    End If
                End If

                If banH = 0 Then
                    Dim montoTotal As Double = 0
                    If .wceMontoH.Text <> "" Then
                        montoTotal = .wceMontoH.Value
                    End If
                    If .wceMontoA.Text <> "" Then
                        montoTotal = montoTotal + .wceMontoA.Value
                    End If
                    If .wceMontoC.Text <> "" Then
                        montoTotal = montoTotal + .wceMontoC.Value
                    End If
                    If .wceMontoO.Text <> "" Then
                        montoTotal = montoTotal + .wceMontoO.Value
                    End If
                    .wceMontoT.Value = montoTotal
                    .lblMontoTLetra.Text = "(" + montoLetra() + ")"
                    'If .wceMontoT.Value > 2000 Then
                    '.rblTipoPago.Enabled = False
                    '.rblTipoPago.SelectedIndex = 1
                    'Else
                    '    .rblTipoPago.Enabled = True
                    'End If
                    valAnticipo = True
                Else
                    .wceMontoT.Text = ""
                    .lblMontoTLetra.Text = ""
                    valAnticipo = False
                End If
            Else
                If nochesE > 0 Then
                    .litError.Text = .litError.Text
                Else
                    .litError.Text = "Información Insuficiente; favor de indicar " + .litError.Text

                End If
                .wceMontoT.Text = ""
                .lblMontoTLetra.Text = ""
                valAnticipo = False
            End If
        End With
    End Function

    Public Function valPresupuesto()
        With Me

            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim sdaEmpleado As New SqlDataAdapter
            Dim dsEmpleado As New DataSet
            Dim montoAcumDisp As Double = 0

            sdaEmpleado.SelectCommand = New SqlCommand("SP_C_ms_presupuesto_acum", ConexionBD)
            sdaEmpleado.SelectCommand.CommandType = CommandType.StoredProcedure
            sdaEmpleado.SelectCommand.Parameters.AddWithValue("@año", wdpPeriodoFin.Date.Year())
            sdaEmpleado.SelectCommand.Parameters.AddWithValue("@centro_costo", ddlCC.SelectedValue())
            sdaEmpleado.SelectCommand.Parameters.AddWithValue("@mes", wdpPeriodoFin.Date.Month())

            ConexionBD.Open()
            sdaEmpleado.Fill(dsEmpleado)
            ConexionBD.Close()

            If dsEmpleado.Tables(0).Rows.Count() > 0 Then
                montoAcumDisp = dsEmpleado.Tables(0).Rows(0).Item("acumulado").ToString()
            Else
                litError.Text = "NO EXISTE REGISTRO DE PRESUPUESTO DE GASTOS DE VIAJE EN EL CENTRO DE COSTOS O DIVISION SELECCIONADA, FAVOR DE VALIDAR CON EL AREA"
                Exit Function
            End If
            sdaEmpleado.Dispose()
            dsEmpleado.Dispose()

            If montoAcumDisp < (.wceMontoH.Value + .wceMontoA.Value + .wceMontoC.Value) Then
                .litError.Text = "El monto solicitado excede el Presupuesto Disponible, favor de validarlo con el responsable del Centro de Costo para que solicite la Ampliación del Presupuesto de Gastos de Viaje en caso de que aplique"
                valPresupuesto = False
            Else
                valPresupuesto = True
            End If

            'Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            'ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            ''Obtener el Presupuesto Disponible del Centro de Costo
            'Dim montoPresupDisp As Integer = 0
            'Dim mes As String
            'If .wdpPeriodoFin.Date.Month() < 10 Then
            '    mes = "0" + .wdpPeriodoFin.Date.Month().ToString
            'Else
            '    mes = .wdpPeriodoFin.Date.Month().ToString
            'End If

            'Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
            'SCMValores.Connection = ConexionBD
            'SCMValores.CommandText = ""
            'SCMValores.Parameters.Clear()
            ''- - - Versión Mensual - - -
            'SCMValores.CommandText = "select mes_" + mes + "_p + mes_" + mes + "_a - mes_" + mes + "_ep - mes_" + mes + "_r " +
            '                         "from ms_presupuesto " +
            '                         "where id_cc = @idCC " +
            '                         "  and año = @año "
            ''
            ''SCMValores.CommandText = "select mes_01_p + mes_01_a - mes_01_ep - mes_01_r + mes_02_p + mes_02_a - mes_02_ep - mes_02_r + mes_03_p + mes_03_a - mes_03_ep - mes_03_r + mes_04_p + mes_04_a - mes_04_ep - mes_04_r + mes_05_p + mes_05_a - mes_05_ep - mes_05_r + mes_06_p + mes_06_a - mes_06_ep - mes_06_r + mes_07_p + mes_07_a - mes_07_ep - mes_07_r + mes_08_p + mes_08_a - mes_08_ep - mes_08_r + mes_09_p + mes_09_a - mes_09_ep - mes_09_r + mes_10_p + mes_10_a - mes_10_ep - mes_10_r + mes_11_p + mes_11_a - mes_11_ep - mes_11_r + mes_12_p + mes_12_a - mes_12_ep - mes_12_r " + _
            ''                         "from ms_presupuesto " + _
            ''                         "where id_cc = @idCC " + _
            ''                         "  and año = @año "
            'SCMValores.Parameters.AddWithValue("@idCC", .ddlCC.SelectedValue)
            'SCMValores.Parameters.AddWithValue("@año", .wdpPeriodoFin.Date.Year())
            'ConexionBD.Open()
            'montoPresupDisp = SCMValores.ExecuteScalar()
            'ConexionBD.Close()
            'If montoPresupDisp < (.wceMontoH.Value + .wceMontoA.Value + .wceMontoC.Value) Then
            '    .litError.Text = "El monto solicitado excede el Presupuesto Disponible, favor de validarlo con el responsable del Centro de Costo para que solicite la Ampliación del Presupuesto de Gastos de Viaje en caso de que aplique"
            '    valPresupuesto = False
            'Else
            '    valPresupuesto = True
            'End If
        End With
    End Function

#End Region

#Region "Vehículo"

    Public Sub llenarVehiculos()
        With Me
            Try
                If .wdpFechaIni.Date > .wdpFechaFin.Date Then
                    .ddlVehiculo.Items.Clear()
                Else
                    .litError.Text = ""

                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim sdaVehi As New SqlDataAdapter
                    Dim dsVehi As New DataSet
                    .ddlVehiculo.DataSource = dsVehi
                    sdaVehi.SelectCommand = New SqlCommand("select id_ms_vehiculo, no_eco + ' [' + placas + ']' as vehiculo " +
                                                           "from bd_Empleado.dbo.ms_vehiculo " +
                                                           "where status = 'A' " +
                                                           "  and uso_utilitario = 'COMODIN' " +
                                                           "  and poliza_seguro_vig > GETDATE() " +
                                                           "  and tarjeta_cir_vig > GETDATE() " +
                                                           "  and id_ms_vehiculo not in (select id_ms_vehiculo " +
                                                           "							 from ms_reserva " +
                                                           "							 where ms_reserva.status not in ('Z', 'ZM', 'R') " +
                                                           "							   and ((fecha_ini <= @fechaIni and fecha_fin > @fechaIni) " +
                                                           "							     or (fecha_ini < @fechaFin and fecha_fin >= @fechaFin) " +
                                                           "							     or (fecha_ini <= @fechaIni and fecha_fin >= @fechaFin) " +
                                                           "							     or (fecha_ini > @fechaIni and fecha_fin < @fechaFin))) " +
                                                           "union " +
                                                           "select id_ms_vehiculo, no_eco + ' [' + placas + ']' as vehiculo " +
                                                           "from bd_Empleado.dbo.ms_vehiculo " +
                                                           "  left join cg_usuario on ms_vehiculo.id_empleado_asig = cg_usuario.id_empleado " +
                                                           "where ms_vehiculo.status = 'A' " +
                                                           "  and uso_utilitario in ('ASIGNADO', 'PRESTACIÓN') " +
                                                           "  and poliza_seguro_vig > GETDATE() " +
                                                           "  and tarjeta_cir_vig > GETDATE() " +
                                                           "  and cg_usuario.id_usuario = @id_usuario ", ConexionBD)
                    sdaVehi.SelectCommand.Parameters.AddWithValue("@fechaIni", .wdpFechaIni.Date)
                    sdaVehi.SelectCommand.Parameters.AddWithValue("@fechaFin", .wdpFechaFin.Date)
                    sdaVehi.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                    .ddlVehiculo.DataTextField = "vehiculo"
                    .ddlVehiculo.DataValueField = "id_ms_vehiculo"
                    ConexionBD.Open()
                    sdaVehi.Fill(dsVehi)
                    .ddlVehiculo.DataBind()
                    ConexionBD.Close()
                    sdaVehi.Dispose()
                    dsVehi.Dispose()
                    .ddlVehiculo.SelectedIndex = -1
                    If .ddlVehiculo.Items.Count = 0 Then
                        .litError.Text = "No hay vehículos disponibles, favor de validarlo"
                    Else
                        actDatosVehi()
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub actDatosVehi()
        With Me
            Try
                'Actualizar campos de la unidad
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaDatosVehi As New SqlDataAdapter
                Dim dsDatosVehi As New DataSet
                .ddlVehiculo.DataSource = dsDatosVehi
                sdaDatosVehi.SelectCommand = New SqlCommand("select no_eco " +
                                                            "     , no_tarjeta_edenred " +
                                                            "     , modelo " +
                                                            "     , uso_utilitario " +
                                                            "from bd_Empleado.dbo.ms_vehiculo " +
                                                            "where id_ms_vehiculo = @id_ms_vehiculo ", ConexionBD)
                sdaDatosVehi.SelectCommand.Parameters.AddWithValue("@id_ms_vehiculo", .ddlVehiculo.SelectedValue)
                ConexionBD.Open()
                sdaDatosVehi.Fill(dsDatosVehi)
                ConexionBD.Close()
                .lblModelo.Text = dsDatosVehi.Tables(0).Rows(0).Item("modelo").ToString()
                .lblVehiculoC.Text = dsDatosVehi.Tables(0).Rows(0).Item("no_eco").ToString()
                .lblTarjEdenred.Text = dsDatosVehi.Tables(0).Rows(0).Item("no_tarjeta_edenred").ToString()
                ._txtComodin.Text = dsDatosVehi.Tables(0).Rows(0).Item("uso_utilitario").ToString()
                If dsDatosVehi.Tables(0).Rows(0).Item("uso_utilitario").ToString() = "ASIGNADO" Or dsDatosVehi.Tables(0).Rows(0).Item("uso_utilitario").ToString() = "PRESTACIÓN" Then
                    .pnlKmActual.Visible = True
                Else
                    .pnlKmActual.Visible = False
                End If
                .upKmActual.Update()
                sdaDatosVehi.Dispose()
                dsDatosVehi.Dispose()
                .upVehiculo.Update()
                .upCombustible.Update()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Function valVehiculo()
        With Me
            Dim ban As Integer = 0
            If .wneLugaresDisp.Text = "" Or .wneLugaresRequ.Text = "" Then
                .litError.Text = .litError.Text + "los Lugares Disponibles y/o Requeridos"
                ban = 1
            End If
            If .pnlKmActual.Visible = True And .wneKmsActual.Text = "" Then
                If ban = 0 Then
                    ban = 1
                Else
                    .litError.Text = .litError.Text + ", "
                End If
                .litError.Text = .litError.Text + "el Kilometraje Actual"
            End If
            If ban = 0 Then
                If .wneLugaresDisp.Value < .wneLugaresRequ.Value Then
                    .litError.Text = "El número de Lugares Disponibles no puede ser menor al número de Lugares Requeridos"
                    valVehiculo = False
                Else
                    valVehiculo = True
                End If
            Else
                .litError.Text = "Información Insuficiente; favor de indicar " + .litError.Text
                valVehiculo = False
            End If
        End With
    End Function

#End Region

#Region "Combustible"

    Public Sub actDatosVehiC()
        With Me
            Try
                'Actualizar campos de la unidad
                If .ddlVehiculoC.Items.Count > 0 Then
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim sdaDatosVehi As New SqlDataAdapter
                    Dim dsDatosVehi As New DataSet
                    .ddlVehiculo.DataSource = dsDatosVehi
                    sdaDatosVehi.SelectCommand = New SqlCommand("select no_tarjeta_edenred " +
                                                                "from bd_Empleado.dbo.ms_vehiculo " +
                                                                "where id_ms_vehiculo = @id_ms_vehiculo ", ConexionBD)
                    sdaDatosVehi.SelectCommand.Parameters.AddWithValue("@id_ms_vehiculo", .ddlVehiculoC.SelectedValue)
                    ConexionBD.Open()
                    sdaDatosVehi.Fill(dsDatosVehi)
                    ConexionBD.Close()
                    .lblTarjEdenred.Text = dsDatosVehi.Tables(0).Rows(0).Item("no_tarjeta_edenred").ToString()
                    sdaDatosVehi.Dispose()
                    dsDatosVehi.Dispose()
                    .upCombustible.Update()
                Else
                    .lblTarjEdenred.Text = ""
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Function valCombustible()
        With Me
            If .wneLitros.Text = "" And .wceImporte.Text = "" Then
                .litError.Text = "Favor de indicar el Número de Litros o Importe requerido"
                valCombustible = False
            Else
                If .wneLitros.Text <> "" And .wceImporte.Text <> "" Then
                    .litError.Text = "Solo se puede indicar Litros o Importe, no ambos, favor de validarlo"
                    valCombustible = False
                Else
                    valCombustible = True
                End If
            End If
        End With
    End Function

#End Region

#Region "Validación"

    Public Function valNoProv()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim conteo As Integer = 0
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select count(*) " +
                                         "from cg_usuario " +
                                         "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                         "  inner join bd_Empleado.dbo.dt_empleado_prov dtNoProv on cgEmpl.id_empleado = dtNoProv.id_empleado " +
                                         "where id_usuario = @idUsuario " +
                                         "  and cg_usuario.status = 'A' " +
                                         "  and cgEmpl.status = 'A' " +
                                         "  and dtNoProv.status = 'A' "
                SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                conteo = SCMValores.ExecuteScalar()
                ConexionBD.Close()
                If conteo > 0 Then
                    valNoProv = True
                Else
                    valNoProv = False
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
                valNoProv = False
            End Try
        End With
    End Function

    Public Function valLic()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim conteo As Integer = 0
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select count(*) " +
                                         "from cg_usuario " +
                                         "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                         "where id_usuario = @idUsuario " +
                                         "  and cg_usuario.status = 'A' " +
                                         "  and cgEmpl.vig_licencia > getdate() "
                SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                conteo = SCMValores.ExecuteScalar()
                ConexionBD.Close()
                If conteo > 0 Then
                    valLic = True
                Else
                    valLic = False
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
                valLic = False
            End Try
        End With
    End Function

    Public Function valCurso()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim conteo As Integer = 0
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select count(*) " +
                                        "from cg_usuario " +
                                        "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                        "where id_usuario = @idUsuario " +
                                        "  and cg_usuario.status = 'A' " +
                                        "  and (cgEmpl.curso_prog = 'N' or cgEmpl.fecha_prog > cast(getdate() as date) or cgEmpl.curso_aprob = 'S') "
                SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                conteo = SCMValores.ExecuteScalar()
                ConexionBD.Close()
                If conteo > 0 Then
                    valCurso = True
                Else
                    valCurso = False
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
                valCurso = False
            End Try
        End With
    End Function

    Public Sub valSolicitud()
        With Me
            .litError.Text = ""
            .litMsgE.Text = ""
            .upMsgE.Update()

            If .wdpPeriodoFin.Date < .wdpPeriodoIni.Date Then
                .litError.Text = "Periodo de Comisión Inválido, favor de verificarlo"
            Else
                If .ddlOrig.SelectedValue = .ddlDest.SelectedValue And .cblMovLocales.Items(0).Selected = False Then
                    .litError.Text = "Origen - Destino Inválidos, favor de verificarlo"
                Else
                    Dim ban As Integer = 0
                    If .txtDestino.Text = "" Then
                        .litError.Text = "el Destino"
                        ban = 1
                    End If
                    If .txtJust.Text = "" Then
                        If ban = 0 Then
                            ban = 1
                        Else
                            .litError.Text = .litError.Text + ", "
                        End If
                        .litError.Text = .litError.Text + "la Justificación"
                    End If
                    If ban = 0 Then
                        If .cblRecursos.Items(0).Selected = False And .cblRecursos.Items(1).Selected = False And .cblRecursos.Items(2).Selected = False And .cblRecursos.Items(3).Selected = False Then
                            .litError.Text = "Información Insuficiente; favor de seleccionar al menos un recurso a solicitar"
                        Else
                            Dim valRec As Integer = 0
                            'Si se activó la casilla de Anticipo, validar la información
                            If .cblRecursos.Items(0).Selected = True Then
                                If valAnticipo() = False Then
                                    valRec = 1
                                Else
                                    If ._txtOmitValPGV.Text = "N" And (.wceMontoH.Value + .wceMontoA.Value + .wceMontoC.Value) > 0 Then
                                        If valPresupuesto() = False Then
                                            valRec = 1
                                        End If
                                    End If
                                End If
                            End If
                            'Si no hay error previo validar la casilla de Vehículo
                            If valRec = 0 Then
                                If .cblRecursos.Items(1).Selected = True Then
                                    If valVehiculo() = False Then
                                        valRec = 1
                                    End If
                                End If
                            End If
                            'Si no hay error previo validar la casilla de Combustible
                            If valRec = 0 Then
                                If .cblRecursos.Items(2).Selected = True Then
                                    If valCombustible() = False Then
                                        valRec = 1
                                    End If
                                End If
                            End If
                            'Si no hay error previo validar la casilla de Avión
                            If valRec = 0 Then
                                If .cblRecursos.Items(3).Selected = True Then
                                    If valAvion() = False Then
                                        valRec = 1
                                    End If
                                End If
                            End If

                            'Si no hay error previo, habilitar el botón de "Enviar a Aprobación"
                            If valRec = 0 Then
                                .btnEnviar.Enabled = True
                            Else
                                .btnEnviar.Enabled = False
                            End If
                        End If
                    Else
                        .litError.Text = "Información Insuficiente; favor de indicar " + .litError.Text
                        .btnEnviar.Enabled = False
                    End If
                End If
            End If

            If .wceMontoT.Value >= ._txtLim_anticipo.Text Then
                .ddlDirector.Visible = True
                .lbl_Director.Visible = True
            Else
                .ddlDirector.Visible = False
                .lbl_Director.Visible = False

            End If
        End With
    End Sub

#End Region

#End Region

#Region "Anticipo"


#Region "NOCHES-VALIDAR"

    Protected Sub wdpPeriodoFin_ValueChanged(sender As Object, e As TextEditorValueChangedEventArgs) Handles wdpPeriodoFin.ValueChanged
        With Me
            llenarNoches()
        End With
    End Sub


    Protected Sub wdpPeriodoIni_ValueChanged(sender As Object, e As TextEditorValueChangedEventArgs) Handles wdpPeriodoIni.ValueChanged
        With Me
            llenarNoches()
        End With
    End Sub
#End Region


#Region "Validación noches"
    Public Sub llenarNoches()
        With Me
            Try
                .wneDiasH.Text = ""
                If .wdpPeriodoIni.Date > .wdpPeriodoFin.Date Or .wdpPeriodoIni.Date < Date.Now.Date Or .wdpPeriodoFin.Date < Date.Now.Date Then
                    .litError.Text = "Seleccionar fecha valida"
                    .wdpPeriodoIni.Date = Date.Now
                    .wdpPeriodoFin.Date = Date.Now
                    .wceMontoH.Text = ""
                    wneDiasH.MaxValue = 0
                Else
                    .litError.Text = ""
                    Dim noche As Integer
                    noche = DateDiff(DateInterval.Day, .wdpPeriodoIni.Date, .wdpPeriodoFin.Date)
                    If noche > 0 Then
                        .wneDiasH.Text = noche.ToString()
                        wneDiasH.Enabled = True
                        wneDiasH.MaxValue = noche.ToString()
                    Else
                        .wneDiasH.Text = ""
                        .wceMontoH.Text = ""
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub
#End Region

#End Region

#Region "Reserva de Vehículo"

    Protected Sub wdpFechaIni_ValueChanged(sender As Object, e As TextEditorValueChangedEventArgs) Handles wdpFechaIni.ValueChanged
        llenarVehiculos()
    End Sub

    Protected Sub wdpFechaFin_ValueChanged(sender As Object, e As TextEditorValueChangedEventArgs) Handles wdpFechaFin.ValueChanged
        llenarVehiculos()
    End Sub

    Protected Sub ddlVehiculo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVehiculo.SelectedIndexChanged
        actDatosVehi()
    End Sub

#End Region

#Region "Combustible"

    Protected Sub ddlVehiculoC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVehiculoC.SelectedIndexChanged
        actDatosVehiC()
    End Sub

#End Region

#Region "Avion"

    Public Function calFechaComp()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("SiTTi")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD

                Dim diasMin As Integer = 5
                Dim fechaTer As DateTime
                'Obtener la fecha compromiso
                Dim resultado As Integer = 0
                Dim cmdF As New SqlCommand
                cmdF.Connection = ConexionBD
                cmdF.CommandText = "select count(*) from cg_cal_tra where fecha = @fecha "
                cmdF.Parameters.Add("@fecha", SqlDbType.DateTime)

                fechaTer = Now 'CDate("2020-06-09 10:00:00") 
                Select Case fechaTer.DayOfWeek
                    Case 1, 2, 3, 4 ', 5
                        If fechaTer.Hour = 14 Then
                            fechaTer = fechaTer.AddHours(1)
                        End If
                        If fechaTer.Hour < 8 Or fechaTer.Hour > 18 Or (fechaTer.Hour = 18 And fechaTer.Minute > 0) Then
                            If fechaTer.Hour > 18 Or (fechaTer.Hour = 18 And fechaTer.Minute > 0) Then
                                fechaTer = fechaTer.AddDays(1)
                            End If
                            fechaTer = fechaTer.AddHours(-(fechaTer.Hour) + 8)
                            fechaTer = fechaTer.AddMinutes(-(fechaTer.Minute))
                            fechaTer = fechaTer.AddSeconds(-(fechaTer.Second))
                            fechaTer = fechaTer.AddMilliseconds(-(fechaTer.Millisecond))
                        End If
                    Case 5
                        If fechaTer.Hour = 14 Then
                            fechaTer = fechaTer.AddHours(1)
                        End If
                        If fechaTer.Hour < 8 Or (fechaTer.Hour > 18 Or (fechaTer.Hour = 18 And fechaTer.Minute > 0)) Then
                            If fechaTer.Hour > 18 Or (fechaTer.Hour = 18 And fechaTer.Minute > 0) Then
                                fechaTer = fechaTer.AddDays(1)
                                'Determinar si la fecha es festiva o laboral
                                cmdF.Parameters("@fecha").Value = CDate(fechaTer.Year.ToString + "-" + fechaTer.Month.ToString + "-" + fechaTer.Day.ToString)
                                cmdF.Connection.Open()
                                resultado = cmdF.ExecuteScalar()
                                cmdF.Connection.Close()
                                If resultado >= 1 Then
                                    fechaTer = fechaTer.AddDays(2)
                                End If
                            End If
                            fechaTer = fechaTer.AddHours(-(fechaTer.Hour) + 8)
                            fechaTer = fechaTer.AddMinutes(-(fechaTer.Minute))
                            fechaTer = fechaTer.AddSeconds(-(fechaTer.Second))
                            fechaTer = fechaTer.AddMilliseconds(-(fechaTer.Millisecond))
                        End If
                    Case 6
                        If fechaTer.Hour < 8 Then
                            fechaTer = fechaTer.AddHours(-(fechaTer.Hour) + 8)
                            fechaTer = fechaTer.AddMinutes(-(fechaTer.Minute))
                            fechaTer = fechaTer.AddSeconds(-(fechaTer.Second))
                            fechaTer = fechaTer.AddMilliseconds(-(fechaTer.Millisecond))
                        End If
                        'Determinar si la fecha es festiva o laboral
                        cmdF.Parameters("@fecha").Value = CDate(fechaTer.Year.ToString + "-" + fechaTer.Month.ToString + "-" + fechaTer.Day.ToString)
                        cmdF.Connection.Open()
                        resultado = cmdF.ExecuteScalar()
                        cmdF.Connection.Close()
                        If resultado >= 1 Or (fechaTer.Hour > 13 Or (fechaTer.Hour = 13 And fechaTer.Minute > 0)) Then
                            fechaTer = fechaTer.AddHours(-(fechaTer.Hour) + 8)
                            fechaTer = fechaTer.AddMinutes(-(fechaTer.Minute))
                            fechaTer = fechaTer.AddSeconds(-(fechaTer.Second))
                            fechaTer = fechaTer.AddMilliseconds(-(fechaTer.Millisecond))
                            fechaTer = fechaTer.AddDays(2)
                        End If
                    Case 0
                        fechaTer = fechaTer.AddHours(-(fechaTer.Hour) + 8)
                        fechaTer = fechaTer.AddMinutes(-(fechaTer.Minute))
                        fechaTer = fechaTer.AddSeconds(-(fechaTer.Second))
                        fechaTer = fechaTer.AddMilliseconds(-(fechaTer.Millisecond))
                        fechaTer = fechaTer.AddDays(1)
                End Select

                'Validar que la fecha inicial sea hábil
                Dim j As Integer = 0
                While j < 1
                    Select Case fechaTer.DayOfWeek
                        Case 1, 2, 3, 4, 5
                            'Determinar si la fecha es festiva o laboral
                            cmdF.Parameters("@fecha").Value = CDate(fechaTer.Year.ToString + "-" + fechaTer.Month.ToString + "-" + fechaTer.Day.ToString)
                            cmdF.Connection.Open()
                            resultado = cmdF.ExecuteScalar()
                            cmdF.Connection.Close()
                            If resultado < 1 Then
                                j = 1
                                If fechaTer.DayOfWeek = 6 Then
                                    If j < Val(diasMin) Then
                                        fechaTer = fechaTer.AddDays(1)
                                        If fechaTer.Hour < 8 Or fechaTer.Hour > 13 Or (fechaTer.Hour = 13 And fechaTer.Minute > 0) Then
                                            fechaTer = fechaTer.AddHours(19)
                                        Else
                                            fechaTer = fechaTer.AddHours(5)
                                        End If
                                    Else
                                        If fechaTer.Hour < 8 Or fechaTer.Hour > 13 Or (fechaTer.Hour = 13 And fechaTer.Minute > 0) Then
                                            fechaTer = fechaTer.AddHours(19)
                                        End If
                                    End If
                                    'Determinar si la fecha es festiva o laboral
                                    cmdF.Parameters("@fecha").Value = CDate(fechaTer.Year.ToString + "-" + fechaTer.Month.ToString + "-" + fechaTer.Day.ToString)
                                    cmdF.Connection.Open()
                                    resultado = cmdF.ExecuteScalar()
                                    cmdF.Connection.Close()
                                    If resultado >= 1 Then
                                        j = 0
                                    End If
                                End If
                            End If
                        Case 0 'en caso de que sea Domingo, se agrega un día
                            fechaTer = fechaTer.AddDays(1)
                            j = 1
                            'Determinar si la fecha es festiva o laboral
                            cmdF.Parameters("@fecha").Value = CDate(fechaTer.Year.ToString + "-" + fechaTer.Month.ToString + "-" + fechaTer.Day.ToString)
                            cmdF.Connection.Open()
                            resultado = cmdF.ExecuteScalar()
                            cmdF.Connection.Close()
                            If resultado >= 1 Then
                                j = 0
                            End If
                    End Select
                    If j = 0 Then
                        fechaTer = fechaTer.AddDays(1)
                    End If
                End While

                Dim i As Integer = 0
                'Días
                i = 0
                While i < Val(diasMin)
                    fechaTer = fechaTer.AddDays(1)
                    Select Case fechaTer.DayOfWeek
                        Case 1, 2, 3, 4, 5
                            'Determinar si la fecha es festiva o laboral
                            cmdF.Parameters("@fecha").Value = CDate(fechaTer.Year.ToString + "-" + fechaTer.Month.ToString + "-" + fechaTer.Day.ToString)
                            cmdF.Connection.Open()
                            resultado = cmdF.ExecuteScalar()
                            cmdF.Connection.Close()
                            If resultado < 1 Then
                                i = i + 1
                                If fechaTer.DayOfWeek = 6 Then
                                    If i < Val(diasMin) Then
                                        fechaTer = fechaTer.AddDays(1)
                                        If fechaTer.Hour < 8 Or fechaTer.Hour > 13 Or (fechaTer.Hour = 13 And fechaTer.Minute > 0) Then
                                            fechaTer = fechaTer.AddHours(19)
                                        Else
                                            fechaTer = fechaTer.AddHours(5)
                                        End If
                                    Else
                                        If fechaTer.Hour < 8 Or fechaTer.Hour > 13 Or (fechaTer.Hour = 13 And fechaTer.Minute > 0) Then
                                            fechaTer = fechaTer.AddHours(19)
                                        End If
                                    End If
                                    'Determinar si la fecha es festiva o laboral
                                    cmdF.Parameters("@fecha").Value = CDate(fechaTer.Year.ToString + "-" + fechaTer.Month.ToString + "-" + fechaTer.Day.ToString)
                                    cmdF.Connection.Open()
                                    resultado = cmdF.ExecuteScalar()
                                    cmdF.Connection.Close()
                                    If resultado >= 1 Then
                                        i = i - 1
                                    End If
                                End If
                            End If
                        Case 0 'en caso de que sea Domingo, se agrega un día
                            fechaTer = fechaTer.AddDays(1)
                            i = i + 1
                            'Determinar si la fecha es festiva o laboral
                            cmdF.Parameters("@fecha").Value = CDate(fechaTer.Year.ToString + "-" + fechaTer.Month.ToString + "-" + fechaTer.Day.ToString)
                            cmdF.Connection.Open()
                            resultado = cmdF.ExecuteScalar()
                            cmdF.Connection.Close()
                            If resultado >= 1 Then
                                i = i - 1
                            End If
                    End Select
                End While
                cmdF.Dispose()

                calFechaComp = fechaTer.AddSeconds(-1)
            Catch ex As Exception
                .litError.Text = ex.ToString
                calFechaComp = CDate("01/01/1900")
            End Try
        End With
    End Function

    Function valAvion()
        With Me
            If (.wdpFechaSalida.Date < calFechaComp()) Then 'Now.Date.AddDays(5)
                .pnlJust.Visible = True
            Else
                .pnlJust.Visible = False
            End If
            .upAvion.Update()

            Dim ban As Integer = 0
            If .wdpFechaSalida.Date > .wdpFechaRegreso.Date Then
                .litError.Text = "Periodo para el vuelo inválido, favor de verificarlo"
                ban = 1
            Else
                If .pnlJust.Visible = True And .txtJustAv.Text.Trim = "" Then
                    .litError.Text = "La Fecha de Salida no cumple con el plazo mínimo, favor de ingresar la justificación correspondiente"
                    ban = 1
                End If
            End If

            If ban = 0 Then
                valAvion = True
            Else
                valAvion = False
            End If
        End With
    End Function

#End Region

#Region "Validar / Guardar"

    Protected Sub btnValidar_Click(sender As Object, e As EventArgs) Handles btnValidar.Click
        valSolicitud()
    End Sub

    Protected Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click
        With Me
            Try
                valSolicitud()

                If .btnEnviar.Enabled = True Then
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    Dim banError As Integer = 0
                    Dim cont As Integer

                    'Si se activó la casilla de Anticipo, validar la información
                    If .cblRecursos.Items(0).Selected = True Then
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        'SCMValores.CommandText = "select case when isnull((select no_anticipos from cg_usuario_ant where id_usuario = @idUsuario), 0) = 0 " +
                        '                         "         then (select count(*) " +
                        '                         "               from ms_anticipo " +
                        '                         "               where id_usr_solicita = @idUsuario " +
                        '                         "               	 and status in ('P', 'A', 'TR', 'EE', 'TRCP', 'EECP', 'TRCA', 'EECA') " +
                        '                         "                   and tipo = 'A') " +
                        '                         "         else case when (select count(*) from ms_anticipo where id_usr_solicita = @idUsuario and empresa = @Empresa and status in ('P', 'A', 'TR', 'EE', 'TRCP', 'EECP', 'TRCA', 'EECA') and tipo = 'A') >= (select no_anticipos from cg_usuario_ant where id_usuario = @idUsuario) " +
                        '                         "                then 1 " +
                        '                         "                else 0 " +
                        '                         "              end " +
                        '                         "       end as no_anticipos "

                        SCMValores.CommandText = "select case when isnull((select no_anticipos from cg_usuario_ant where id_usuario = @idUsuario and tipo is null ), 0) = 0 " +
                         "         then (select count(*) " +
                         "               from ms_anticipo " +
                         "               where id_usr_solicita = @idUsuario " +
                         "               	 and status in ('P', 'A', 'TR', 'EE', 'TRCP', 'EECP', 'TRCA', 'EECA') " +
                         "                   and tipo = 'A') " +
                         "         else case when (select count(*) from ms_anticipo where id_usr_solicita = @idUsuario and empresa = @Empresa and status in ('P', 'A', 'TR', 'EE', 'TRCP', 'EECP', 'TRCA', 'EECA') and tipo = 'A') >= (select no_anticipos from cg_usuario_ant where id_usuario = @idUsuario and tipo is null ) " +
                         "                then 1 " +
                         "                else 0 " +
                         "              end " +
                         "       end as no_anticipos "

                        SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@Empresa", .ddlEmpresa.SelectedItem.Text)
                        ConexionBD.Open()
                        cont = Val(SCMValores.ExecuteScalar)
                        ConexionBD.Close()
                        If cont >= 1 Then
                            .litError.Text = "Excedió el límite de Anticipos Pendientes, favor de finalizar los anteriores antes de registrar uno nuevo"
                            banError = 1
                        End If
                    End If

                    If .ddlCC.Items.Count = 0 Then
                        .litError.Text = "No se ha indicado el Centro de Costo, favor de verificarlo con el Administrador de Catálogos"
                        banError = 1
                    End If

                    'Si no hay error previo validar la casilla de Vehículo
                    If banError = 0 Then
                        If .cblRecursos.Items(1).Selected = True Then
                            'Validar Disponibilidad
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "select count (*) " +
                                                     "from (select id_ms_vehiculo " +
                                                     "      from bd_Empleado.dbo.ms_vehiculo " +
                                                     "      where status = 'A' " +
                                                     "        and uso_utilitario = 'COMODIN' " +
                                                     "        and poliza_seguro_vig > GETDATE() " +
                                                     "        and tarjeta_cir_vig > GETDATE() " +
                                                     "        and id_ms_vehiculo = @idMsVehiculo " +
                                                     "        and id_ms_vehiculo not in (select id_ms_vehiculo " +
                                                     "							         from ms_reserva " +
                                                     "							         where ms_reserva.status not in ('Z', 'ZM', 'R') " +
                                                     "							           and ((fecha_ini <= @fechaIni and fecha_fin > @fechaIni) " +
                                                     "							             or (fecha_ini < @fechaFin and fecha_fin >= @fechaFin) " +
                                                     "							             or (fecha_ini <= @fechaIni and fecha_fin >= @fechaFin) " +
                                                     "							             or (fecha_ini > @fechaIni and fecha_fin < @fechaFin))) " +
                                                     "      union " +
                                                     "      select id_ms_vehiculo " +
                                                     "      from bd_Empleado.dbo.ms_vehiculo " +
                                                     "        left join cg_usuario on ms_vehiculo.id_empleado_asig = cg_usuario.id_empleado " +
                                                     "      where ms_vehiculo.status = 'A' " +
                                                     "        and uso_utilitario in ('ASIGNADO', 'PRESTACIÓN') " +
                                                     "        and poliza_seguro_vig > GETDATE() " +
                                                     "        and tarjeta_cir_vig > GETDATE() " +
                                                     "        and id_ms_vehiculo = @idMsVehiculo " +
                                                     "      ) as dt_disponibilidad "
                            SCMValores.Parameters.AddWithValue("@fechaIni", .wdpFechaIni.Date)
                            SCMValores.Parameters.AddWithValue("@fechaFin", .wdpFechaFin.Date)
                            SCMValores.Parameters.AddWithValue("@idMsVehiculo", .ddlVehiculo.SelectedValue)
                            ConexionBD.Open()
                            cont = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If Not cont > 0 Then
                                .litError.Text = "Ya fue reservado el vehículo, favor de validar la disponibilidad"
                                llenarVehiculos()
                                banError = 1
                            End If
                        End If
                    End If

                    'Si no hay error, almacenar la Solicitud de Recursos
                    If banError = 0 Then
                        Dim fecha As DateTime
                        fecha = Date.Now

                        While Val(._txtBan.Text) = 0
                            Dim sdaEmpleado As New SqlDataAdapter
                            Dim dsEmpleado As New DataSet
                            Dim query As String
                            query = "select cgEmpl.no_empleado as no_empleadoE " +
                                    "     , cgEmpresaE.nombre as empresaE " +
                                    "     , cgCCE.nombre as ccE " +
                                    "     , cgUsrA.id_usuario as id_usr_aut " +
                                    "     , cgAut.no_empleado as no_empleadoA " +
                                    "     , cgAut.nombre + ' ' + cgAut.ap_paterno + ' ' + cgAut.ap_materno as Autorizador " +
                                    "     , cgUsrD.id_usuario  as id_usr_dir  " +
                                    "     , cgDir.no_empleado as no_empleadoD " +
                                    "     , cgDir.nombre + ' ' + cgDir.ap_paterno + ' ' + cgDir.ap_materno as Director " +
                                    "from bd_empleado.dbo.cg_empleado cgEmpl " +
                                    "  left join bd_Empleado.dbo.cg_cc as cgCCE on cgEmpl.id_cc = cgCCE.id_cc " +
                                    "  left join bd_Empleado.dbo.cg_empresa as cgEmpresaE on cgCCE.id_empresa = cgEmpresaE.id_empresa " +
                                    "  left join cg_usuario cgUsrE on cgEmpl.id_empleado = cgUsrE.id_empleado " +
                                    "  left join bd_Empleado.dbo.cg_empleado cgAut on cgAut.id_empleado = @idAut " +
                                    "  left join cg_usuario cgUsrA on cgAut.id_empleado = cgUsrA.id_empleado " +
                                    "  left join bd_Empleado.dbo.cg_empleado cgDir on cgDir.id_empleado = @idDir " +
                                    "  left join cg_usuario cgUsrD on cgDir.id_empleado = cgUsrD.id_empleado " +
                                    "where cgUsrE.id_usuario = @idEmpl "
                            sdaEmpleado.SelectCommand = New SqlCommand(query, ConexionBD)
                            sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idEmpl", Val(._txtIdUsuario.Text))
                            sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idAut", .ddlAutorizador.SelectedValue)
                            sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idDir", .ddlDirector.SelectedValue)
                            ConexionBD.Open()
                            sdaEmpleado.Fill(dsEmpleado)
                            ConexionBD.Close()

                            Dim SCMValoresSolR As SqlCommand = New System.Data.SqlClient.SqlCommand
                            SCMValoresSolR.Connection = ConexionBD
                            'Insertar Cabecera de la Solicitud [ms_recursos]
                            SCMValoresSolR.Parameters.Clear()
                            SCMValoresSolR.CommandText = "insert into ms_recursos ( id_usr_solicita,  fecha_solicita,  id_usr_autoriza,  empresa,  no_empleado,  empleado,  no_autorizador,  autorizador,  destino,  actividad,  periodo_ini,  periodo_fin,  tipo_transporte,  lugares_dispo,  lugares_reque,  incluye_anticipo,  id_ms_anticipo,  incluye_reserva,  id_ms_reserva,  incluye_comb,  id_ms_comb,  incluye_hist_util,  id_dt_hist_util,  incluye_avion,  id_ms_avion,  id_lugar_orig,  lugar_orig,  id_lugar_dest,  lugar_dest,  mov_locales,  admon_viajes_vobo,  id_cc,  centro_costo,  año_pgv,  mes_pgv,  monto_pgv_ep,  status, id_usr_director, no_director, director) " +
                                                         " 			       values (@id_usr_solicita, @fecha_solicita, @id_usr_autoriza, @empresa, @no_empleado, @empleado, @no_autorizador, @autorizador, @destino, @actividad, @periodo_ini, @periodo_fin, @tipo_transporte, @lugares_dispo, @lugares_reque, @incluye_anticipo, @id_ms_anticipo, @incluye_reserva, @id_ms_reserva, @incluye_comb, @id_ms_comb, @incluye_hist_util, @id_dt_hist_util, @incluye_avion, @id_ms_avion, @id_lugar_orig, @lugar_orig, @id_lugar_dest, @lugar_dest, @mov_locales, @admon_viajes_vobo, @id_cc, @centro_costo, @año_pgv, @mes_pgv, @monto_pgv_ep,  'P', @id_usr_director, @no_director , @director )"
                            SCMValoresSolR.Parameters.AddWithValue("@id_usr_solicita", Val(._txtIdUsuario.Text))
                            SCMValoresSolR.Parameters.AddWithValue("@fecha_solicita", fecha)
                            SCMValoresSolR.Parameters.AddWithValue("@id_usr_autoriza", dsEmpleado.Tables(0).Rows(0).Item("id_usr_aut").ToString())
                            SCMValoresSolR.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedItem.Text)
                            SCMValoresSolR.Parameters.AddWithValue("@id_cc", .ddlCC.SelectedValue)
                            SCMValoresSolR.Parameters.AddWithValue("@centro_costo", .ddlCC.SelectedItem.Text)
                            SCMValoresSolR.Parameters.AddWithValue("@no_empleado", dsEmpleado.Tables(0).Rows(0).Item("no_empleadoE").ToString())
                            SCMValoresSolR.Parameters.AddWithValue("@empleado", .lblSolicitante.Text)
                            SCMValoresSolR.Parameters.AddWithValue("@no_autorizador", dsEmpleado.Tables(0).Rows(0).Item("no_empleadoA").ToString())
                            SCMValoresSolR.Parameters.AddWithValue("@autorizador", dsEmpleado.Tables(0).Rows(0).Item("Autorizador").ToString())
                            SCMValoresSolR.Parameters.AddWithValue("@destino", .txtDestino.Text.Trim)
                            SCMValoresSolR.Parameters.AddWithValue("@actividad", .txtJust.Text.Trim)
                            SCMValoresSolR.Parameters.AddWithValue("@periodo_ini", .wdpPeriodoIni.Date)
                            SCMValoresSolR.Parameters.AddWithValue("@periodo_fin", .wdpPeriodoFin.Date)
                            SCMValoresSolR.Parameters.AddWithValue("@tipo_transporte", .ddlTipoTransp.SelectedValue)
                            SCMValoresSolR.Parameters.AddWithValue("@id_lugar_orig", .ddlOrig.SelectedValue)
                            SCMValoresSolR.Parameters.AddWithValue("@lugar_orig", .ddlOrig.SelectedItem.Text)
                            SCMValoresSolR.Parameters.AddWithValue("@id_lugar_dest", .ddlDest.SelectedValue)
                            SCMValoresSolR.Parameters.AddWithValue("@lugar_dest", .ddlDest.SelectedItem.Text)
                            If .cblMovLocales.Items(0).Selected = True Then
                                SCMValoresSolR.Parameters.AddWithValue("@mov_locales", "S")
                            Else
                                SCMValoresSolR.Parameters.AddWithValue("@mov_locales", "N")
                            End If
                            If (.cblRecursos.Items(0).Selected = True And .wceMontoH.Text <> "") Or .cblRecursos.Items(1).Selected = True Or .cblRecursos.Items(3).Selected = True Then
                                SCMValoresSolR.Parameters.AddWithValue("@admon_viajes_vobo", "S")
                            Else
                                SCMValoresSolR.Parameters.AddWithValue("@admon_viajes_vobo", "N")
                            End If

                            If wceMontoT.Value >= Val(_txtLim_anticipo.Text) Then
                                SCMValoresSolR.Parameters.AddWithValue("@id_usr_director", dsEmpleado.Tables(0).Rows(0).Item("id_usr_dir").ToString())
                                SCMValoresSolR.Parameters.AddWithValue("@no_director", dsEmpleado.Tables(0).Rows(0).Item("no_empleadoD").ToString())
                                SCMValoresSolR.Parameters.AddWithValue("@director", dsEmpleado.Tables(0).Rows(0).Item("Director").ToString())
                            Else
                                SCMValoresSolR.Parameters.AddWithValue("@id_usr_director", DBNull.Value)
                                SCMValoresSolR.Parameters.AddWithValue("@no_director", DBNull.Value)
                                SCMValoresSolR.Parameters.AddWithValue("@director", DBNull.Value)
                            End If

                            SCMValoresSolR.Parameters.Add("@lugares_dispo", SqlDbType.Int)
                            SCMValoresSolR.Parameters.Add("@lugares_reque", SqlDbType.Int)
                            SCMValoresSolR.Parameters.Add("@incluye_anticipo", SqlDbType.VarChar)
                            SCMValoresSolR.Parameters.Add("@id_ms_anticipo", SqlDbType.Int)
                            SCMValoresSolR.Parameters.Add("@año_pgv", SqlDbType.Int)
                            SCMValoresSolR.Parameters.Add("@mes_pgv", SqlDbType.Int)
                            SCMValoresSolR.Parameters.Add("@monto_pgv_ep", SqlDbType.Money)
                            SCMValoresSolR.Parameters.Add("@incluye_reserva", SqlDbType.VarChar)
                            SCMValoresSolR.Parameters.Add("@id_ms_reserva", SqlDbType.Int)
                            SCMValoresSolR.Parameters.Add("@incluye_comb", SqlDbType.VarChar)
                            SCMValoresSolR.Parameters.Add("@id_ms_comb", SqlDbType.Int)
                            SCMValoresSolR.Parameters.Add("@incluye_hist_util", SqlDbType.VarChar)
                            SCMValoresSolR.Parameters.Add("@id_dt_hist_util", SqlDbType.Int)
                            SCMValoresSolR.Parameters.Add("@incluye_avion", SqlDbType.VarChar)
                            SCMValoresSolR.Parameters.Add("@id_ms_avion", SqlDbType.Int)

                            'Anticipo
                            If .cblRecursos.Items(0).Selected = True Then
                                Dim totAnt As Decimal = 0
                                'Insertar Anticipo [ms_anticipo]
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "insert into ms_anticipo ( id_usr_solicita,  fecha_solicita,  id_usr_autoriza,  empresa,  no_empleado,  no_proveedor,  empleado,  no_autorizador,  autorizador,  destino,  actividad,  periodo_comp,  periodo_ini,  periodo_fin,  tipo_pago,  no_personas,  dias_hospedaje,  monto_hospedaje,  dias_alimentos,  monto_alimentos,  dias_casetas,  monto_casetas,  dias_otros,  monto_otros,  otros_especifico, status) " +
                                                         " 			       values (@id_usr_solicita, @fecha_solicita, @id_usr_autoriza, @empresa, @no_empleado, @no_proveedor, @empleado, @no_autorizador, @autorizador, @destino, @actividad, @periodo_comp, @periodo_ini, @periodo_fin, @tipo_pago, @no_personas, @dias_hospedaje, @monto_hospedaje, @dias_alimentos, @monto_alimentos, @dias_casetas, @monto_casetas, @dias_otros, @monto_otros, @otros_especifico,    'P')"
                                SCMValores.Parameters.AddWithValue("@id_usr_solicita", Val(._txtIdUsuario.Text))
                                SCMValores.Parameters.AddWithValue("@fecha_solicita", fecha)
                                SCMValores.Parameters.AddWithValue("@id_usr_autoriza", dsEmpleado.Tables(0).Rows(0).Item("id_usr_aut").ToString())
                                SCMValores.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedItem.Text)
                                SCMValores.Parameters.AddWithValue("@no_empleado", dsEmpleado.Tables(0).Rows(0).Item("no_empleadoE").ToString())
                                SCMValores.Parameters.AddWithValue("@no_proveedor", .lblNoProveedor.Text)
                                SCMValores.Parameters.AddWithValue("@empleado", .lblSolicitante.Text)
                                SCMValores.Parameters.AddWithValue("@no_autorizador", dsEmpleado.Tables(0).Rows(0).Item("no_empleadoA").ToString())
                                SCMValores.Parameters.AddWithValue("@autorizador", dsEmpleado.Tables(0).Rows(0).Item("Autorizador").ToString())
                                SCMValores.Parameters.AddWithValue("@destino", .txtDestino.Text.Trim)
                                SCMValores.Parameters.AddWithValue("@actividad", .txtJust.Text.Trim)
                                SCMValores.Parameters.AddWithValue("@periodo_comp", "Del " + .wdpPeriodoIni.Text + " al " + .wdpPeriodoFin.Text)
                                SCMValores.Parameters.AddWithValue("@periodo_ini", .wdpPeriodoIni.Date)
                                SCMValores.Parameters.AddWithValue("@periodo_fin", .wdpPeriodoFin.Date)
                                SCMValores.Parameters.AddWithValue("@tipo_pago", .rblTipoPago.SelectedValue)
                                SCMValores.Parameters.AddWithValue("@no_personas", 1)
                                If .wceMontoH.Text = "" Then
                                    SCMValores.Parameters.AddWithValue("@dias_hospedaje", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@monto_hospedaje", 0)
                                Else
                                    SCMValores.Parameters.AddWithValue("@dias_hospedaje", .wneDiasH.Value)
                                    SCMValores.Parameters.AddWithValue("@monto_hospedaje", .wceMontoH.Value)
                                    totAnt = totAnt + .wceMontoH.Value
                                End If
                                If .wceMontoA.Text = "" Then
                                    SCMValores.Parameters.AddWithValue("@dias_alimentos", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@monto_alimentos", 0)
                                Else
                                    SCMValores.Parameters.AddWithValue("@dias_alimentos", .wneDiasA.Value)
                                    SCMValores.Parameters.AddWithValue("@monto_alimentos", .wceMontoA.Value)
                                    totAnt = totAnt + .wceMontoA.Value
                                End If
                                If .wceMontoC.Text = "" Then
                                    SCMValores.Parameters.AddWithValue("@dias_casetas", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@monto_casetas", 0)
                                Else
                                    SCMValores.Parameters.AddWithValue("@dias_casetas", .wneDiasC.Value)
                                    SCMValores.Parameters.AddWithValue("@monto_casetas", .wceMontoC.Value)
                                    totAnt = totAnt + .wceMontoC.Value
                                End If
                                If .wceMontoO.Text = "" Then
                                    SCMValores.Parameters.AddWithValue("@dias_otros", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@monto_otros", 0)
                                    SCMValores.Parameters.AddWithValue("@otros_especifico", DBNull.Value)
                                Else
                                    SCMValores.Parameters.AddWithValue("@dias_otros", .wneDiasO.Value)
                                    SCMValores.Parameters.AddWithValue("@monto_otros", .wceMontoO.Value)
                                    SCMValores.Parameters.AddWithValue("@otros_especifico", .txtOtros.Text)
                                    totAnt = totAnt + .wceMontoO.Value
                                End If
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()
                                sdaEmpleado.Dispose()
                                dsEmpleado.Dispose()

                                SCMValoresSolR.Parameters("@incluye_anticipo").Value = "S"
                                'Obtener ID de la Solicitud
                                SCMValores.CommandText = "select max(id_ms_anticipo) from ms_anticipo where id_usr_solicita = @id_usr_solicita and status not in ('Z') "
                                ConexionBD.Open()
                                .lblFolioA.Text = SCMValores.ExecuteScalar()
                                ConexionBD.Close()
                                SCMValoresSolR.Parameters("@id_ms_anticipo").Value = Val(.lblFolioA.Text)
                                SCMValoresSolR.Parameters("@año_pgv").Value = .wdpPeriodoFin.Date.Year
                                SCMValoresSolR.Parameters("@mes_pgv").Value = .wdpPeriodoFin.Date.Month()
                                If .wceMontoO.Text <> "" Then
                                    SCMValoresSolR.Parameters("@monto_pgv_ep").Value = totAnt - .wceMontoO.Value
                                Else
                                    SCMValoresSolR.Parameters("@monto_pgv_ep").Value = totAnt
                                End If

                                'Inhabilitar Panel
                                .pnlAnticipo.Enabled = False
                                .upAnticipo.Update()
                            Else
                                SCMValoresSolR.Parameters("@incluye_anticipo").Value = "N"
                                SCMValoresSolR.Parameters("@id_ms_anticipo").Value = DBNull.Value
                                SCMValoresSolR.Parameters("@año_pgv").Value = DBNull.Value
                                SCMValoresSolR.Parameters("@mes_pgv").Value = DBNull.Value
                                SCMValoresSolR.Parameters("@monto_pgv_ep").Value = DBNull.Value
                            End If

                            'Reserva de Vehículo
                            If .cblRecursos.Items(1).Selected = True Then
                                SCMValoresSolR.Parameters("@lugares_dispo").Value = .wneLugaresDisp.Value
                                SCMValoresSolR.Parameters("@lugares_reque").Value = .wneLugaresRequ.Value
                                If ._txtComodin.Text = "COMODIN" Then
                                    'Almacenar Reserva de Vehículo [ms_reserva]
                                    SCMValores.Parameters.Clear()
                                    SCMValores.CommandText = "insert into ms_reserva(prioridad, empresa, centro_costo, no_empleado, empleado_nom, empleado_appat, empleado_apmat, no_autorizador, autorizador_nom, autorizador_appat, autorizador_apmat, fecha_ini, fecha_fin, no_eco, marca, modelo, año, placas, IAVE, poliza_seguro_vig, tarjeta_cir_vig, destino, just, id_usr_solicito, fecha_solicito, id_ms_vehiculo, id_usr_autorizo, status) " +
                                                             "select @prioridad as prioridad, cgEmpr.nombre as empresa, cgCC.nombre as centro_costo " +
                                                             "	 , cgEmpl.no_empleado, cgEmpl.nombre as empleado_nom, cgEmpl.ap_paterno as empleado_appat, cgEmpl.ap_materno as empleado_apmat " +
                                                             "	 , cgAut.no_empleado as no_autorizador, cgAut.nombre as autorizador_nom, cgAut.ap_paterno as autorizador_appat, cgAut.ap_materno as autorizador_apmat " +
                                                             "	 , @fechaIni as fecha_ini, @fechaFin as fecha_fin " +
                                                             "	 , msVeh.no_eco, msVeh.marca, msVeh.modelo, msVeh.año, msVeh.placas, msVeh.IAVE, msVeh.poliza_seguro_vig, msVeh.tarjeta_cir_vig " +
                                                             "	 , @destino as destino, @just as just, @idUsrSol as id_usr_solicito, @fecha_solicita as fecha_solicito, @idMsVehiculo as id_ms_vehiculo, cgUsrA.id_usuario as id_usr_autorizo, 'P' as status " +
                                                             "from cg_usuario as cgUsrE " +
                                                             "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cgUsrE.id_empleado = cgEmpl.id_empleado " +
                                                             "  inner join bd_Empleado.dbo.cg_cc cgCC on cgEmpl.id_cc = cgCC.id_cc " +
                                                             "  inner join bd_Empleado.dbo.cg_empresa cgEmpr on cgCC.id_empresa = cgEmpr.id_empresa " +
                                                             "  inner join bd_Empleado.dbo.cg_empleado cgAut on cgAut.id_empleado = @idEmplAut " +
                                                             "  inner join cg_usuario cgUsrA on cgUsrA.id_empleado = cgAut.id_empleado " +
                                                             "  inner join bd_Empleado.dbo.ms_vehiculo msVeh on msVeh.id_ms_vehiculo = @idMsVehiculo " +
                                                             "where cgUsrE.id_usuario = @idUsrSol "
                                    SCMValores.Parameters.AddWithValue("@idUsrSol", Val(._txtIdUsuario.Text))
                                    SCMValores.Parameters.AddWithValue("@idEmplAut", .ddlAutorizador.SelectedValue)
                                    SCMValores.Parameters.AddWithValue("@prioridad", "Media")
                                    SCMValores.Parameters.AddWithValue("@fecha_solicita", fecha)
                                    SCMValores.Parameters.AddWithValue("@fechaIni", .wdpFechaIni.Date)
                                    SCMValores.Parameters.AddWithValue("@fechaFin", .wdpFechaFin.Date)
                                    SCMValores.Parameters.AddWithValue("@idMsVehiculo", .ddlVehiculo.SelectedValue)
                                    SCMValores.Parameters.AddWithValue("@destino", .txtDestino.Text.Trim)
                                    SCMValores.Parameters.AddWithValue("@just", .txtJust.Text.Trim)
                                    ConexionBD.Open()
                                    SCMValores.ExecuteNonQuery()
                                    ConexionBD.Close()

                                    SCMValoresSolR.Parameters("@incluye_reserva").Value = "S"
                                    'Obtener Folio
                                    SCMValores.CommandText = "select max(id_ms_reserva) " +
                                                             "from ms_reserva " +
                                                             "where id_usr_solicito = @idUsrSol " +
                                                             "  and fecha_ini = @fechaIni " +
                                                             "  and id_ms_vehiculo = @idMsVehiculo "
                                    ConexionBD.Open()
                                    .lblFolioV.Text = SCMValores.ExecuteScalar()
                                    ConexionBD.Close()
                                    SCMValoresSolR.Parameters("@id_ms_reserva").Value = Val(.lblFolioV.Text)
                                    SCMValoresSolR.Parameters("@incluye_hist_util").Value = "N"
                                    SCMValoresSolR.Parameters("@id_dt_hist_util").Value = DBNull.Value
                                Else
                                    'Almacenar Histórico de Utilitario [dt_hist_util]
                                    SCMValores.Parameters.Clear()
                                    SCMValores.CommandText = "insert into dt_hist_util( id_usr_solicita, fecha_solicita, id_usr_autoriza, empresa, centro_costo, no_empleado, empleado, no_autorizador, autorizador, periodo_ini, periodo_fin, destino, actividad, id_ms_vehiculo, no_eco, marca, modelo, año, placas, IAVE, no_tarjeta_edenred, km_actual, status) " +
                                                             "select @idUsrSol as id_usr_solicita " +
                                                             "     , @fecha_solicita as fecha_solicita " +
                                                             "     , cgUsrA.id_usuario as id_usr_autoriza " +
                                                             "     , cgEmpr.nombre as empresa " +
                                                             "     , cgCC.nombre as centro_costo " +
                                                             "     , cgEmpl.no_empleado " +
                                                             "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as empleado " +
                                                             "     , cgAut.no_empleado as no_autorizador " +
                                                             "     , cgAut.nombre + ' ' + cgAut.ap_paterno + ' ' + cgAut.ap_materno as autorizador " +
                                                             "     , @fechaIni as periodo_ini " +
                                                             "     , @fechaFin as periodo_fin " +
                                                             "     , @destino as destino " +
                                                             "     , @just as actividad " +
                                                             "     , @idMsVehiculo as id_ms_vehiculo " +
                                                             "     , msVeh.no_eco, msVeh.marca, msVeh.modelo, msVeh.año, msVeh.placas, msVeh.IAVE " +
                                                             "     , msVeh.no_tarjeta_edenred " +
                                                             "     , @kmsActual as km_actual " +
                                                             "     , 'P' as status " +
                                                             "from cg_usuario as cgUsrE " +
                                                             "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cgUsrE.id_empleado = cgEmpl.id_empleado " +
                                                             "  inner join bd_Empleado.dbo.cg_cc cgCC on cgEmpl.id_cc = cgCC.id_cc " +
                                                             "  inner join bd_Empleado.dbo.cg_empresa cgEmpr on cgCC.id_empresa = cgEmpr.id_empresa " +
                                                             "  inner join bd_Empleado.dbo.cg_empleado cgAut on cgAut.id_empleado = @idEmplAut " +
                                                             "  inner join cg_usuario cgUsrA on cgUsrA.id_empleado = cgAut.id_empleado " +
                                                             "  inner join bd_Empleado.dbo.ms_vehiculo msVeh on msVeh.id_ms_vehiculo = @idMsVehiculo " +
                                                             "where cgUsrE.id_usuario = @idUsrSol "
                                    SCMValores.Parameters.AddWithValue("@idUsrSol", Val(._txtIdUsuario.Text))
                                    SCMValores.Parameters.AddWithValue("@idEmplAut", .ddlAutorizador.SelectedValue)
                                    SCMValores.Parameters.AddWithValue("@fecha_solicita", fecha)
                                    SCMValores.Parameters.AddWithValue("@fechaIni", .wdpFechaIni.Date)
                                    SCMValores.Parameters.AddWithValue("@fechaFin", .wdpFechaFin.Date)
                                    SCMValores.Parameters.AddWithValue("@idMsVehiculo", .ddlVehiculo.SelectedValue)
                                    SCMValores.Parameters.AddWithValue("@destino", .txtDestino.Text.Trim)
                                    SCMValores.Parameters.AddWithValue("@just", .txtJust.Text.Trim)
                                    SCMValores.Parameters.AddWithValue("@kmsActual", .wneKmsActual.Value)
                                    ConexionBD.Open()
                                    SCMValores.ExecuteNonQuery()
                                    ConexionBD.Close()

                                    SCMValoresSolR.Parameters("@incluye_reserva").Value = "N"
                                    SCMValoresSolR.Parameters("@id_ms_reserva").Value = DBNull.Value
                                    SCMValoresSolR.Parameters("@incluye_hist_util").Value = "S"
                                    'Obtener Folio
                                    SCMValores.CommandText = "select max(id_dt_hist_util) " +
                                                             "from dt_hist_util " +
                                                             "where id_usr_solicita = @idUsrSol " +
                                                             "  and periodo_ini = @fechaIni " +
                                                             "  and id_ms_vehiculo = @idMsVehiculo "
                                    ConexionBD.Open()
                                    SCMValoresSolR.Parameters("@id_dt_hist_util").Value = SCMValores.ExecuteScalar()
                                    ConexionBD.Close()
                                    .lbl_FolioV.Visible = False
                                End If

                                'Inhabilitar Panel
                                .pnlVehiculo.Enabled = False
                                .upVehiculo.Update()
                            Else
                                SCMValoresSolR.Parameters("@lugares_dispo").Value = DBNull.Value
                                SCMValoresSolR.Parameters("@lugares_reque").Value = DBNull.Value
                                SCMValoresSolR.Parameters("@incluye_reserva").Value = "N"
                                SCMValoresSolR.Parameters("@id_ms_reserva").Value = DBNull.Value
                                SCMValoresSolR.Parameters("@incluye_hist_util").Value = "N"
                                SCMValoresSolR.Parameters("@id_dt_hist_util").Value = DBNull.Value
                            End If

                            'Combustible
                            'If .cblRecursos.Items(2).Selected = True Then
                            '    'Almacenar Combustible [ms_comb]
                            '    SCMValores.Parameters.Clear()
                            '    SCMValores.CommandText = "insert into ms_comb( id_usr_solicita, fecha_solicita, id_usr_autoriza, empresa, centro_costo, no_empleado, empleado, no_autorizador, autorizador, periodo_ini, periodo_fin, destino, actividad, id_ms_vehiculo, no_eco, marca, modelo, año, placas, IAVE, no_tarjeta_edenred, litros_comb, importe_comb, status) " +
                            '                             "select @idUsrSol as id_usr_solicita " +
                            '                             "     , @fecha_solicita as fecha_solicita " +
                            '                             "     , cgUsrA.id_usuario as id_usr_autoriza " +
                            '                             "     , cgEmpr.nombre as empresa " +
                            '                             "     , cgCC.nombre as centro_costo " +
                            '                             "     , cgEmpl.no_empleado " +
                            '                             "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as empleado " +
                            '                             "     , cgAut.no_empleado as no_autorizador " +
                            '                             "     , cgAut.nombre + ' ' + cgAut.ap_paterno + ' ' + cgAut.ap_materno as autorizador " +
                            '                             "     , @fechaIni as periodo_ini " +
                            '                             "     , @fechaFin as periodo_fin " +
                            '                             "     , @destino as destino " +
                            '                             "     , @just as actividad " +
                            '                             "     , @idMsVehiculo as id_ms_vehiculo " +
                            '                             "     , msVeh.no_eco, msVeh.marca, msVeh.modelo, msVeh.año, msVeh.placas, msVeh.IAVE " +
                            '                             "     , msVeh.no_tarjeta_edenred " +
                            '                             "     , @litros_comb as litros_comb " +
                            '                             "     , @importe_comb as importe_comb " +
                            '                             "     , 'P' as status " +
                            '                             "from cg_usuario as cgUsrE " +
                            '                             "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cgUsrE.id_empleado = cgEmpl.id_empleado " +
                            '                             "  inner join bd_Empleado.dbo.cg_cc cgCC on cgEmpl.id_cc = cgCC.id_cc " +
                            '                             "  inner join bd_Empleado.dbo.cg_empresa cgEmpr on cgCC.id_empresa = cgEmpr.id_empresa " +
                            '                             "  inner join bd_Empleado.dbo.cg_empleado cgAut on cgAut.id_empleado = @idEmplAut " +
                            '                             "  inner join cg_usuario cgUsrA on cgUsrA.id_empleado = cgAut.id_empleado " +
                            '                             "  inner join bd_Empleado.dbo.ms_vehiculo msVeh on msVeh.id_ms_vehiculo = @idMsVehiculo " +
                            '                             "where cgUsrE.id_usuario = @idUsrSol "
                            '    SCMValores.Parameters.AddWithValue("@idUsrSol", Val(._txtIdUsuario.Text))
                            '    SCMValores.Parameters.AddWithValue("@idEmplAut", .ddlAutorizador.SelectedValue)
                            '    SCMValores.Parameters.AddWithValue("@fecha_solicita", fecha)
                            '    If .cblRecursos.Items(1).Selected = True Then
                            '        'Existe Reserva de Vehículo, por lo que se muestra la etiqueta y no la lista
                            '        SCMValores.Parameters.AddWithValue("@idMsVehiculo", .ddlVehiculo.SelectedValue)
                            '    Else
                            '        'No existe Reserva de Vehículo, por lo que se muestra la lista y se oculta la etiqueta
                            '        SCMValores.Parameters.AddWithValue("@idMsVehiculo", .ddlVehiculoC.SelectedValue)
                            '    End If
                            '    SCMValores.Parameters.AddWithValue("@destino", .txtDestino.Text.Trim)
                            '    SCMValores.Parameters.AddWithValue("@just", .txtJust.Text.Trim)
                            '    SCMValores.Parameters.AddWithValue("@fechaIni", .wdpFechaIni.Date)
                            '    SCMValores.Parameters.AddWithValue("@fechaFin", .wdpFechaFin.Date)
                            '    If .wneLitros.Text = "" Then
                            '        'Se solicitó por Importe
                            '        SCMValores.Parameters.AddWithValue("@litros_comb", DBNull.Value)
                            '        SCMValores.Parameters.AddWithValue("@importe_comb", .wceImporte.Value)
                            '    Else
                            '        'Se solicitó por Litros
                            '        SCMValores.Parameters.AddWithValue("@litros_comb", .wneLitros.Value)
                            '        SCMValores.Parameters.AddWithValue("@importe_comb", DBNull.Value)
                            '    End If

                            '    ConexionBD.Open()
                            '    SCMValores.ExecuteNonQuery()
                            '    ConexionBD.Close()

                            '    SCMValoresSolR.Parameters("@incluye_comb").Value = "S"
                            '    'Obtener Folio
                            '    SCMValores.CommandText = "select max(id_ms_comb) " + _
                            '                             "from ms_comb " + _
                            '                             "where id_usr_solicita = @idUsrSol " + _
                            '                             "  and fecha_solicita = @fecha_solicita "
                            '    ConexionBD.Open()
                            '    .lblFolioC.Text = SCMValores.ExecuteScalar()
                            '    ConexionBD.Close()
                            '    SCMValoresSolR.Parameters("@id_ms_comb").Value = Val(.lblFolioC.Text)

                            '    'Inhabilitar Panel
                            '    .pnlCombustible.Enabled = False
                            '    .upCombustible.Update()
                            'Else
                            '    SCMValoresSolR.Parameters("@incluye_comb").Value = "N"
                            '    SCMValoresSolR.Parameters("@id_ms_comb").Value = DBNull.Value
                            'End If
                            If .cblRecursos.Items(2).Selected = True Then
                                'Almacenar Combustible [ms_comb]
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "insert into ms_comb( id_usr_solicita, fecha_solicita, id_usr_autoriza, empresa, centro_costo, no_empleado, empleado, no_autorizador, autorizador, periodo_ini, periodo_fin, destino, actividad, id_ms_vehiculo, no_eco, marca, modelo, año, placas, IAVE, no_tarjeta_edenred, litros_comb, importe_comb, status) " +
                                                         "select @idUsrSol as id_usr_solicita " +
                                                         "     , @fecha_solicita as fecha_solicita " +
                                                         "     , cgUsrA.id_usuario as id_usr_autoriza " +
                                                         "     , cgEmpr.nombre as empresa " +
                                                         "     , cgCC.nombre as centro_costo " +
                                                         "     , cgEmpl.no_empleado " +
                                                         "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as empleado " +
                                                         "     , cgAut.no_empleado as no_autorizador " +
                                                         "     , cgAut.nombre + ' ' + cgAut.ap_paterno + ' ' + cgAut.ap_materno as autorizador " +
                                                         "     , @fechaIni as periodo_ini, @fechaFin as periodo_fin " +
                                                         "     , @destino as destino " +
                                                         "     , @just as actividad " +
                                                         "     , @idMsVehiculo as id_ms_vehiculo " +
                                                         "     , msVeh.no_eco, msVeh.marca, msVeh.modelo, msVeh.año, msVeh.placas, msVeh.IAVE " +
                                                         "     , msVeh.no_tarjeta_edenred " +
                                                         "     , @litros_comb as litros_comb " +
                                                         "     , @importe_comb as importe_comb " +
                                                         "     , 'P' as status " +
                                                         "from cg_usuario as cgUsrE " +
                                                         "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cgUsrE.id_empleado = cgEmpl.id_empleado " +
                                                         "  inner join bd_Empleado.dbo.cg_cc cgCC on cgEmpl.id_cc = cgCC.id_cc " +
                                                         "  inner join bd_Empleado.dbo.cg_empresa cgEmpr on cgCC.id_empresa = cgEmpr.id_empresa " +
                                                         "  inner join bd_Empleado.dbo.cg_empleado cgAut on cgAut.id_empleado = @idEmplAut " +
                                                         "  inner join cg_usuario cgUsrA on cgUsrA.id_empleado = cgAut.id_empleado " +
                                                         "  inner join bd_Empleado.dbo.ms_vehiculo msVeh on msVeh.id_ms_vehiculo = @idMsVehiculo " +
                                                         "where cgUsrE.id_usuario = @idUsrSol "
                                SCMValores.Parameters.AddWithValue("@idUsrSol", Val(._txtIdUsuario.Text))
                                SCMValores.Parameters.AddWithValue("@idEmplAut", .ddlAutorizador.SelectedValue)
                                SCMValores.Parameters.AddWithValue("@fecha_solicita", fecha)
                                SCMValores.Parameters.AddWithValue("@fechaIni", .wdpPeriodoIni.Date)
                                SCMValores.Parameters.AddWithValue("@fechaFin", .wdpPeriodoFin.Date)
                                If .cblRecursos.Items(1).Selected = True Then
                                    'Existe Reserva de Vehículo, por lo que se muestra la etiqueta y no la lista
                                    SCMValores.Parameters.AddWithValue("@idMsVehiculo", .ddlVehiculo.SelectedValue)
                                Else
                                    'No existe Reserva de Vehículo, por lo que se muestra la lista y se oculta la etiqueta
                                    SCMValores.Parameters.AddWithValue("@idMsVehiculo", .ddlVehiculoC.SelectedValue)
                                End If
                                SCMValores.Parameters.AddWithValue("@destino", .txtDestino.Text.Trim)
                                SCMValores.Parameters.AddWithValue("@just", .txtJust.Text.Trim)
                                If .wneLitros.Text = "" Then
                                    'Se solicitó por Importe
                                    SCMValores.Parameters.AddWithValue("@litros_comb", DBNull.Value)
                                    SCMValores.Parameters.AddWithValue("@importe_comb", .wceImporte.Value)
                                Else
                                    'Se solicitó por Litros
                                    SCMValores.Parameters.AddWithValue("@litros_comb", .wneLitros.Value)
                                    SCMValores.Parameters.AddWithValue("@importe_comb", DBNull.Value)
                                End If
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()

                                SCMValoresSolR.Parameters("@incluye_comb").Value = "S"
                                'Obtener Folio
                                SCMValores.CommandText = "select max(id_ms_comb) " +
                                                         "from ms_comb " +
                                                         "where id_usr_solicita = @idUsrSol " +
                                                         "  and fecha_solicita = @fecha_solicita "
                                ConexionBD.Open()
                                .lblFolioC.Text = SCMValores.ExecuteScalar()
                                ConexionBD.Close()
                                SCMValoresSolR.Parameters("@id_ms_comb").Value = Val(.lblFolioC.Text)

                                'Inhabilitar Panel
                                .pnlCombustible.Enabled = False
                                .upCombustible.Update()
                            Else
                                SCMValoresSolR.Parameters("@incluye_comb").Value = "N"
                                SCMValoresSolR.Parameters("@id_ms_comb").Value = DBNull.Value
                            End If
                            'Avión
                            If .cblRecursos.Items(3).Selected = True Then
                                'Almacenar Avión [ms_avion]
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "insert into ms_avion( fecha_nacimiento,  fecha_salida,  fecha_regreso,  justificacion,  status) " +
                                                         "              values(@fecha_nacimiento, @fecha_salida, @fecha_regreso, @justificacion,  'P' ) "
                                SCMValores.Parameters.AddWithValue("@fecha_nacimiento", .wdpFechaNac.Date)
                                SCMValores.Parameters.AddWithValue("@fecha_salida", .wdpFechaSalida.Date)
                                SCMValores.Parameters.AddWithValue("@fecha_regreso", .wdpFechaRegreso.Date)
                                If .pnlJust.Visible = True Then
                                    SCMValores.Parameters.AddWithValue("@justificacion", .txtJustAv.Text.Trim)
                                Else
                                    SCMValores.Parameters.AddWithValue("@justificacion", DBNull.Value)
                                End If
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()

                                SCMValoresSolR.Parameters("@incluye_avion").Value = "S"
                                'Obtener Folio
                                SCMValores.CommandText = "select max(id_ms_avion) " +
                                                         "from ms_avion " +
                                                         "where fecha_nacimiento = @fecha_nacimiento " +
                                                         "  and fecha_salida = @fecha_salida "
                                ConexionBD.Open()
                                .lblFolioAv.Text = SCMValores.ExecuteScalar()
                                ConexionBD.Close()
                                SCMValoresSolR.Parameters("@id_ms_avion").Value = Val(.lblFolioAv.Text)

                                'Inhabilitar Panel
                                .pnlAvion.Enabled = False
                                .upAvion.Update()
                            Else
                                SCMValoresSolR.Parameters("@incluye_avion").Value = "N"
                                SCMValoresSolR.Parameters("@id_ms_avion").Value = DBNull.Value
                            End If

                            ConexionBD.Open()
                            SCMValoresSolR.ExecuteNonQuery()
                            ConexionBD.Close()

                            'Obtener Folio
                            SCMValoresSolR.CommandText = "select max(id_ms_recursos) " +
                                                         "from ms_recursos " +
                                                         "where id_usr_solicita = @id_usr_solicita " +
                                                         "  and fecha_solicita = @fecha_solicita "
                            ConexionBD.Open()
                            .lblFolio.Text = SCMValoresSolR.ExecuteScalar()
                            ConexionBD.Close()

                            'Actualizar montos de presupuesto de Centro de Costo en caso de que corresponda
                            If .cblRecursos.Items(0).Selected = True Then
                                Dim sdaPGV As New SqlDataAdapter
                                Dim dsPGV As New DataSet
                                sdaPGV.SelectCommand = New SqlCommand("select (select isnull(sum(monto_pgv_ep), 0) as msAntEP " +
                                                                      "        from ms_recursos " +
                                                                      "        where id_cc = @idCC " +
                                                                      "          and año_pgv = @año " +
                                                                      "          and mes_pgv = @mes) " +
                                                                      "      + " +
                                                                      "       (select isnull(sum(monto_pgv_ex), 0) as msCompEx " +
                                                                      "        from ms_comp " +
                                                                      "        where id_cc = @idCC " +
                                                                      "          and año_pgv = @año " +
                                                                      "          and mes_pgv = @mes) as pgvEP " +
                                                                      "     , (select isnull(sum(monto_pgv_r), 0) as msCompR " +
                                                                      "        from ms_comp " +
                                                                      "        where id_cc = @idCC " +
                                                                      "          and año_pgv = @año " +
                                                                      "          and mes_pgv = @mes) as pgvR ", ConexionBD)
                                sdaPGV.SelectCommand.Parameters.AddWithValue("@idCC", .ddlCC.SelectedValue)
                                sdaPGV.SelectCommand.Parameters.AddWithValue("@año", .wdpPeriodoFin.Date.Year)
                                sdaPGV.SelectCommand.Parameters.AddWithValue("@mes", .wdpPeriodoFin.Date.Month())
                                ConexionBD.Open()
                                sdaPGV.Fill(dsPGV)
                                ConexionBD.Close()

                                Dim mes As String
                                If .wdpPeriodoFin.Date.Month() < 10 Then
                                    mes = "0" + .wdpPeriodoFin.Date.Month().ToString
                                Else
                                    mes = .wdpPeriodoFin.Date.Month().ToString
                                End If
                                SCMValores.CommandText = "update ms_presupuesto " +
                                                         "  set mes_" + mes + "_ep = @pgvEP, mes_" + mes + "_r = @pgvR " +
                                                         "where id_cc = @idCC " +
                                                         "  and año = @año "
                                SCMValores.Parameters.AddWithValue("@pgvEP", Val(dsPGV.Tables(0).Rows(0).Item("pgvEP").ToString()))
                                SCMValores.Parameters.AddWithValue("@pgvR", Val(dsPGV.Tables(0).Rows(0).Item("pgvR").ToString()))
                                SCMValores.Parameters.AddWithValue("@idCC", .ddlCC.SelectedValue)
                                SCMValores.Parameters.AddWithValue("@año", .wdpPeriodoFin.Date.Year)
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()
                            End If

                            ._txtBan.Text = 1

                            'Insertar Instancia de la Solicitud de Recursos
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "insert into ms_instancia ( id_ms_sol,  tipo,  id_actividad) " +
                                                     "				    values (@id_ms_sol, @tipo, @id_actividad) "
                            SCMValores.Parameters.AddWithValue("@id_ms_sol", Val(.lblFolio.Text))
                            SCMValores.Parameters.AddWithValue("@tipo", "SR")
                            SCMValores.Parameters.AddWithValue("@id_actividad", 38)
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()
                            'Obtener ID de la Instancia de Solicitud
                            SCMValores.CommandText = "select max(id_ms_instancia) from ms_instancia where id_ms_sol = @id_ms_sol and tipo = 'SR' "
                            ConexionBD.Open()
                            ._txtIdMsInst.Text = SCMValores.ExecuteScalar
                            ConexionBD.Close()
                            'Insertar Históricos
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "insert into ms_historico ( id_ms_instancia,  id_actividad,  id_usr,  fecha) " +
                                                     "				    values (@id_ms_instancia, @id_actividad, @id_usr, @fecha) "
                            SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                            SCMValores.Parameters.AddWithValue("@id_actividad", 38)
                            SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                            SCMValores.Parameters.AddWithValue("@fecha", fecha)
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            'Envío de Correo
                            Dim Mensaje As New System.Net.Mail.MailMessage()
                            Dim destinatario As String = ""
                            'Obtener el Correos del Autorizador
                            SCMValores.CommandText = "select cgEmpl.correo from bd_empleado.dbo.cg_empleado cgEmpl where cgEmpl.id_empleado = @idAut "
                            SCMValores.Parameters.AddWithValue("@idAut", .ddlAutorizador.SelectedValue)
                            ConexionBD.Open()
                            destinatario = SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            Mensaje.[To].Add(destinatario)
                            Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                            Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                            Mensaje.Subject = "ProcAd - Solicitud de Anticipo No. " + .lblFolio.Text + " por Autorizar"
                            Dim texto As String
                            texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                                    "Se solicitó la autorización de recursos número <b>" + .lblFolio.Text +
                                    "</b> por parte de <b>" + .lblSolicitante.Text + "</b> <br>" +
                                    "<br>Favor de determinar si procede la petición </span>"
                            Mensaje.Body = texto
                            Mensaje.IsBodyHtml = True
                            Mensaje.Priority = MailPriority.Normal

                            Dim Servidor As New SmtpClient()
                            Servidor.Host = "10.10.10.30"
                            Servidor.Port = 587
                            Servidor.EnableSsl = False
                            Servidor.UseDefaultCredentials = False
                            Servidor.Credentials = New System.Net.NetworkCredential("nprocad", "mc8HLB8lPe78")

                            Try
                                Servidor.Send(Mensaje)
                            Catch ex As System.Net.Mail.SmtpException
                                .litError.Text = ex.ToString
                            End Try

                            .pnlInicio.Enabled = False
                            .btnValidar.Enabled = False
                            .btnEnviar.Enabled = False
                        End While
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class