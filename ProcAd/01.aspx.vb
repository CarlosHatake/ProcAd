Public Class _01
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    .litError.Text = ""
                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtBan.Text = 0

                        'Creación de Variables para la conexión y consulta de información a la base de datos
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        'Nombre del Solicitante
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "select nombre + ' ' + ap_paterno + ' ' + ap_materno " + _
                                                 "from cg_usuario " + _
                                                 "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                 "where id_usuario = @idUsuario "
                        SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        .lblSolicitante.Text = SCMValores.ExecuteScalar()
                        ConexionBD.Close()
                        'Lista de Empresas
                        Dim sdaEmpresa As New SqlDataAdapter
                        Dim dsEmpresa As New DataSet
                        sdaEmpresa.SelectCommand = New SqlCommand("select Empresa.id_empresa, Empresa.nombre " + _
                                                                  "from bd_empleado.dbo.cg_empresa Empresa " + _
                                                                  "  inner join bd_empleado.dbo.dt_empleado_prov dtCod on Empresa.id_empresa = dtCod.id_empresa " + _
                                                                  "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on dtCod.id_empleado = cgEmpl.id_empleado " + _
                                                                  "  inner join cg_usuario on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                                  "where Empresa.status = 'A' " + _
                                                                  "  and cg_usuario.id_usuario = @idUsuario " + _
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
                        .ddlEmpresa.SelectedIndex = -1
                        'Número de Empleado
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "select isnull((select dtNoProv.no_proveedor " + _
                                                 "			     from cg_usuario " + _
                                                 "			      inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                 "			      inner join bd_Empleado.dbo.dt_empleado_prov dtNoProv on cgEmpl.id_empleado = dtNoProv.id_empleado " + _
                                                 "	    		 where id_usuario = @idUsuario " + _
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
                        sdaAut.SelectCommand = New SqlCommand("select cgEmpl.id_empleado, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " + _
                                                              "from dt_autorizador " + _
                                                              "  left join cg_usuario on dt_autorizador.id_autorizador = cg_usuario.id_usuario " + _
                                                              "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                              "where dt_autorizador.id_usuario = @idUsuario " + _
                                                              "  and cg_usuario.status = 'A' " + _
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
                        'Limpiar Campos
                        .wdteFechaIni.Date = Now.Date
                        .wdteFechaFin.Date = Now.Date
                        .txtDestino.Text = ""
                        .txtAct.Text = ""
                        .wneNoPersonas.Value = 1
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
                        'Botones
                        .btnSumar.Enabled = True
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

#Region "Funciones"

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

    Public Sub sumar()
        With Me
            If .wdteFechaFin.Date < .wdteFechaIni.Date Then
                .litError.Text = "Periodo Inválido, favor de verificarlo"
            Else
                Dim ban As Integer = 0
                If .txtDestino.Text = "" Then
                    .litError.Text = "el Destino"
                    ban = 1
                End If
                If .txtAct.Text = "" Then
                    If ban = 0 Then
                        ban = 1
                    Else
                        .litError.Text = .litError.Text + ", "
                    End If
                    .litError.Text = .litError.Text + "la Actividad"
                End If
                If (.wneDiasO.Value > 0 Or .wceMontoO.Value > 0) And .txtOtros.Text = "" Then
                    If ban = 0 Then
                        ban = 1
                    Else
                        .litError.Text = .litError.Text + ", "
                    End If
                    .litError.Text = .litError.Text + "el concepto de Otros"
                End If
                If (.wneDiasH.Text = "" And .wceMontoH.Text <> "") Or (.wneDiasH.Text <> "" And .wceMontoH.Text = "") Or (.wneDiasA.Text = "" And .wceMontoA.Text <> "") Or (.wneDiasA.Text <> "" And .wceMontoA.Text = "") Or (.wneDiasC.Text = "" And .wceMontoC.Text <> "") Or (.wneDiasC.Text <> "" And .wceMontoC.Text = "") Or (.wneDiasO.Text = "" And .wceMontoO.Text <> "") Or (.wneDiasO.Text <> "" And .wceMontoO.Text = "") Then
                    If ban = 0 Then
                        ban = 1
                    Else
                        .litError.Text = .litError.Text + ", "
                    End If
                    .litError.Text = .litError.Text + "el número de días y/o montos"
                End If
                If ban = 0 Then
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
                    .btnEnviar.Enabled = True
                Else
                    .litError.Text = "Información Insuficiente; favor de indicar " + .litError.Text
                    .btnEnviar.Enabled = False
                End If
            End If
        End With
    End Sub

#End Region

#Region "Botones"

    Protected Sub ddlEmpresa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEmpresa.SelectedIndexChanged
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                'Número de Empleado
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select isnull((select dtNoProv.no_proveedor " + _
                                         "			     from cg_usuario " + _
                                         "			      inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                         "			      inner join bd_Empleado.dbo.dt_empleado_prov dtNoProv on cgEmpl.id_empleado = dtNoProv.id_empleado " + _
                                         "	    		 where id_usuario = @idUsuario " + _
                                         "				   and id_empresa = @idEmpresa), '') as no_proveedor "
                SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                SCMValores.Parameters.AddWithValue("@idEmpresa", .ddlEmpresa.SelectedValue)
                ConexionBD.Open()
                .lblNoProveedor.Text = SCMValores.ExecuteScalar()
                ConexionBD.Close()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnSumar_Click(sender As Object, e As EventArgs) Handles btnSumar.Click
        With Me
            .litError.Text = ""
            sumar()
        End With
    End Sub

    Protected Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click
        With Me
            Try
                .litError.Text = ""

                sumar()

                If .btnEnviar.Enabled = True Then
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    Dim contAnt As Integer
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "select case when isnull((select no_anticipos from cg_usuario_ant where id_usuario = @idUsuario), 0) = 0 " + _
                                             "         then (select count(*) " + _
                                             "               from ms_anticipo " + _
                                             "               where id_usr_solicita = @idUsuario " + _
                                             "               	 and status in ('P', 'A', 'TR', 'EE', 'TRCP', 'EECP', 'TRCA', 'EECA')) " + _
                                             "         else case when (select count(*) from ms_anticipo where id_usr_solicita = @idUsuario and empresa = @Empresa and status in ('P', 'A', 'TR', 'EE', 'TRCP', 'EECP', 'TRCA', 'EECA')) >= (select no_anticipos from cg_usuario_ant where id_usuario = @idUsuario) " + _
                                             "                then 1 " + _
                                             "                else 0 " + _
                                             "              end " + _
                                             "       end as no_anticipos "
                    SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                    SCMValores.Parameters.AddWithValue("@Empresa", .ddlEmpresa.SelectedItem.Text)
                    ConexionBD.Open()
                    contAnt = Val(SCMValores.ExecuteScalar)
                    ConexionBD.Close()
                    If contAnt >= 1 Then
                        .litError.Text = "Excedió el límite de Anticipos Pendientes, favor de finalizar los anteriores antes de registrar uno nuevo"
                    Else
                        While Val(._txtBan.Text) = 0
                            Dim fecha As DateTime
                            fecha = Date.Now

                            Dim sdaEmpleado As New SqlDataAdapter
                            Dim dsEmpleado As New DataSet
                            Dim query As String
                            query = "select cgEmpl.no_empleado as no_empleadoE " + _
                                    "     , cgUsrA.id_usuario as id_usr_aut " + _
                                    "     , cgAut.no_empleado as no_empleadoA " + _
                                    "     , cgAut.nombre + ' ' + cgAut.ap_paterno + ' ' + cgAut.ap_materno as Autorizador " + _
                                    "from bd_empleado.dbo.cg_empleado cgEmpl " + _
                                    "  left join cg_usuario cgUsrE on cgEmpl.id_empleado = cgUsrE.id_empleado " + _
                                    "  left join bd_Empleado.dbo.cg_empleado cgAut on cgAut.id_empleado = @idAut " + _
                                    "  left join cg_usuario cgUsrA on cgAut.id_empleado = cgUsrA.id_empleado " + _
                                    "where cgUsrE.id_usuario = @idEmpl "
                            sdaEmpleado.SelectCommand = New SqlCommand(query, ConexionBD)
                            sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idEmpl", Val(._txtIdUsuario.Text))
                            sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idAut", .ddlAutorizador.SelectedValue)
                            ConexionBD.Open()
                            sdaEmpleado.Fill(dsEmpleado)
                            ConexionBD.Close()

                            'Insertar Anticipo
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "insert into ms_anticipo ( id_usr_solicita,  fecha_solicita,  id_usr_autoriza,  empresa,  no_empleado,  no_proveedor,  empleado,  no_autorizador,  autorizador,  destino,  actividad,  periodo_comp,  periodo_ini,  periodo_fin,  tipo_pago,  no_personas,  dias_hospedaje,  monto_hospedaje,  dias_alimentos,  monto_alimentos,  dias_casetas,  monto_casetas,  dias_otros,  monto_otros,  otros_especifico, status) " + _
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
                            SCMValores.Parameters.AddWithValue("@actividad", .txtAct.Text.Trim)
                            SCMValores.Parameters.AddWithValue("@periodo_comp", "Del " + .wdteFechaIni.Text + " al " + .wdteFechaFin.Text)
                            SCMValores.Parameters.AddWithValue("@periodo_ini", .wdteFechaIni.Date)
                            SCMValores.Parameters.AddWithValue("@periodo_fin", .wdteFechaFin.Date)
                            SCMValores.Parameters.AddWithValue("@tipo_pago", .rblTipoPago.SelectedValue)
                            SCMValores.Parameters.AddWithValue("@no_personas", .wneNoPersonas.Value)
                            If .wceMontoH.Text = "" Then
                                SCMValores.Parameters.AddWithValue("@dias_hospedaje", DBNull.Value)
                                SCMValores.Parameters.AddWithValue("@monto_hospedaje", 0)
                            Else
                                SCMValores.Parameters.AddWithValue("@dias_hospedaje", .wneDiasH.Value)
                                SCMValores.Parameters.AddWithValue("@monto_hospedaje", .wceMontoH.Value)
                            End If
                            If .wceMontoA.Text = "" Then
                                SCMValores.Parameters.AddWithValue("@dias_alimentos", DBNull.Value)
                                SCMValores.Parameters.AddWithValue("@monto_alimentos", 0)
                            Else
                                SCMValores.Parameters.AddWithValue("@dias_alimentos", .wneDiasA.Value)
                                SCMValores.Parameters.AddWithValue("@monto_alimentos", .wceMontoA.Value)
                            End If
                            If .wceMontoC.Text = "" Then
                                SCMValores.Parameters.AddWithValue("@dias_casetas", DBNull.Value)
                                SCMValores.Parameters.AddWithValue("@monto_casetas", 0)
                            Else
                                SCMValores.Parameters.AddWithValue("@dias_casetas", .wneDiasC.Value)
                                SCMValores.Parameters.AddWithValue("@monto_casetas", .wceMontoC.Value)
                            End If
                            If .wceMontoO.Text = "" Then
                                SCMValores.Parameters.AddWithValue("@dias_otros", DBNull.Value)
                                SCMValores.Parameters.AddWithValue("@monto_otros", 0)
                                SCMValores.Parameters.AddWithValue("@otros_especifico", DBNull.Value)
                            Else
                                SCMValores.Parameters.AddWithValue("@dias_otros", .wneDiasO.Value)
                                SCMValores.Parameters.AddWithValue("@monto_otros", .wceMontoO.Value)
                                SCMValores.Parameters.AddWithValue("@otros_especifico", .txtOtros.Text)
                            End If
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()
                            sdaEmpleado.Dispose()
                            dsEmpleado.Dispose()

                            'Obtener ID de la Solicitud
                            SCMValores.CommandText = "select max(id_ms_anticipo) from ms_anticipo where id_usr_solicita = @id_usr_solicita and status not in ('Z') "
                            ConexionBD.Open()
                            .lblFolio.Text = SCMValores.ExecuteScalar
                            ConexionBD.Close()
                            If Val(.lblFolio.Text) > 0 Then
                                ._txtBan.Text = 1
                            End If

                            'Insertar Instancia del Anticipo
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "insert into ms_instancia ( id_ms_sol,  tipo,  id_actividad) " + _
                                                     "				    values (@id_ms_sol, @tipo, @id_actividad) "
                            SCMValores.Parameters.AddWithValue("@id_ms_sol", Val(.lblFolio.Text))
                            SCMValores.Parameters.AddWithValue("@tipo", "A")
                            SCMValores.Parameters.AddWithValue("@id_actividad", 2)
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()
                            'Obtener ID de la Instancia de Solicitud
                            SCMValores.CommandText = "select max(id_ms_instancia) from ms_instancia where id_ms_sol = @id_ms_sol and tipo = 'A' "
                            ConexionBD.Open()
                            ._txtIdMsInst.Text = SCMValores.ExecuteScalar
                            ConexionBD.Close()
                            'Insertar Históricos
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "insert into ms_historico ( id_ms_instancia,  id_actividad,  id_usr,  fecha) " + _
                                                     "				    values (@id_ms_instancia, @id_actividad, @id_usr, @fecha) "
                            SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                            SCMValores.Parameters.AddWithValue("@id_actividad", 2)
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
                            'Mensaje.From = New System.Net.Mail.MailAddress("soporte.unne.procad@gmail.com", "ProcAd", System.Text.Encoding.UTF8)
                            Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                            Mensaje.Subject = "ProcAd - Solicitud de Anticipo No. " + .lblFolio.Text + " por Autorizar en ProcAd"
                            Dim texto As String
                            texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                                    "Se solicitó la autorización del anticipo número <b>" + .lblFolio.Text + _
                                    "</b> por parte de <b>" + .lblSolicitante.Text + _
                                    "</b> por un importe de <b>" + .wceMontoT.Text + "</b> <br>" + _
                                    "<br>Favor de determinar si procede el Anticipo </span>"
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

                            'Inhabilitar Paneles
                            .pnlInicio.Enabled = False
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