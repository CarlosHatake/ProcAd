Public Class ConsSolRec
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

                        'Llenar listas
                        Dim query As String
                        'Solicitantes
                        Dim sdaSolicitante As New SqlDataAdapter
                        Dim dsSolicitante As New DataSet
                        query = "select distinct(id_usr_solicita) as id_usr_solicita, empleado as solicito " +
                                "from ms_recursos "
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            query = query + "where empleado = @autorizador or autorizador = @autorizador "
                        End If
                        query = query + "order by solicito "
                        sdaSolicitante.SelectCommand = New SqlCommand(query, ConexionBD)
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            sdaSolicitante.SelectCommand.Parameters.AddWithValue("@autorizador", ._txtEmpleado.Text)
                        End If
                        .ddlSolicitante.DataSource = dsSolicitante
                        .ddlSolicitante.DataTextField = "solicito"
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
                        query = "select distinct(empresa) as empresa " +
                                "from ms_recursos "
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            query = query + "where empleado = @autorizador or autorizador = @autorizador "
                        End If
                        query = query + "order by empresa "
                        sdaEmpresa.SelectCommand = New SqlCommand(query, ConexionBD)
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            sdaEmpresa.SelectCommand.Parameters.AddWithValue("@autorizador", ._txtEmpleado.Text)
                        End If
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
                        query = "select distinct(autorizador) as autorizador " +
                                "from ms_recursos "
                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Conta" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Then
                            query = query + "where empleado = @empleado "
                        End If
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            query = query + "where empleado = @autorizador or autorizador = @autorizador "
                        End If
                        query = query + "order by autorizador "
                        sdaAutorizador.SelectCommand = New SqlCommand(query, ConexionBD)
                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Conta" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Then
                            sdaAutorizador.SelectCommand.Parameters.AddWithValue("@empleado", ._txtEmpleado.Text)
                        End If
                        If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                            sdaAutorizador.SelectCommand.Parameters.AddWithValue("@autorizador", ._txtEmpleado.Text)
                        End If
                        .ddlAutorizador.DataSource = dsAutorizador
                        .ddlAutorizador.DataTextField = "autorizador"
                        .ddlAutorizador.DataValueField = "autorizador"
                        ConexionBD.Open()
                        sdaAutorizador.Fill(dsAutorizador)
                        .ddlAutorizador.DataBind()
                        ConexionBD.Close()
                        sdaAutorizador.Dispose()
                        dsAutorizador.Dispose()
                        .ddlAutorizador.SelectedIndex = -1

                        If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Conta" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Then
                            .pnlSolicitó.Visible = False
                            .upSolicitante.Update()
                        Else
                            .pnlSolicitó.Visible = True
                            .cbSolicitante.Checked = False
                            .pnlSolicitante.Visible = False
                            .upSolicitante.Update()
                            .cbEmpresa.Checked = False
                            .pnlEmpresa.Visible = False
                            If ._txtPerfil.Text = "CoPame" Then
                                .cbEmpresa.Checked = True
                                .cbEmpresa.Enabled = False
                                .pnlEmpresa.Visible = True
                                .ddlEmpresa.SelectedValue = "PAME"
                                .pnlEmpresa.Enabled = False
                            End If
                            If ._txtPerfil.Text = "CoDCM" Then
                                .cbEmpresa.Checked = True
                                .cbEmpresa.Enabled = False
                                .pnlEmpresa.Visible = True
                                .ddlEmpresa.SelectedValue = "DICOMEX"
                                .pnlEmpresa.Enabled = False
                            End If
                            .upEmpresa.Update()
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
            .upFechaC.Update()
            .wdpFechaI.Date = Date.Now.ToShortDateString
            .wdpFechaF.Date = Date.Now.ToShortDateString
            .cbAutorizador.Checked = False
            .pnlAutorizador.Visible = False
            .upAutorizador.Update()
            .cbStatus.Checked = False
            .pnlStatus.Visible = False
            .upStatus.Update()
            .cbNoSolRec.Checked = False
            .pnlNoSolRec.Visible = False
            .upNoSolRec.Update()
            'Ocultar resto de paneles principales
            .pnlRegistros.Visible = False
            .pnlDetalle.Visible = False
        End With
    End Sub

    Public Sub vista(ByRef control, ByVal valor, ByRef up)
        With Me
            If valor Then
                control.Visible = True
            Else
                control.Visible = False
            End If
            up.Update()
        End With
    End Sub

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
            If Val(Me.wceMontoT.Value) = 1 And op = 1 Then
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

#End Region

#Region "Filtros"

    Protected Sub cbSolicitante_CheckedChanged(sender As Object, e As EventArgs) Handles cbSolicitante.CheckedChanged
        vista(Me.pnlSolicitante, Me.cbSolicitante.Checked, Me.upSolicitante)
    End Sub

    Protected Sub cbEmpresa_CheckedChanged(sender As Object, e As EventArgs) Handles cbEmpresa.CheckedChanged
        vista(Me.pnlEmpresa, Me.cbEmpresa.Checked, Me.upEmpresa)
    End Sub

    Protected Sub cbFechaC_CheckedChanged(sender As Object, e As EventArgs) Handles cbFechaC.CheckedChanged
        vista(Me.pnlFechaC, Me.cbFechaC.Checked, Me.upFechaC)
    End Sub

    Protected Sub cbAutorizador_CheckedChanged(sender As Object, e As EventArgs) Handles cbAutorizador.CheckedChanged
        vista(Me.pnlAutorizador, Me.cbAutorizador.Checked, Me.upAutorizador)
    End Sub

    Protected Sub cbNoSolRec_CheckedChanged(sender As Object, e As EventArgs) Handles cbNoSolRec.CheckedChanged
        vista(Me.pnlNoSolRec, Me.cbNoSolRec.Checked, Me.upNoSolRec)
        If Me.cbNoSolRec.Checked = True Then
            Me.txtNoSolRec.Text = ""
        End If
    End Sub

    Protected Sub cbStatus_CheckedChanged(sender As Object, e As EventArgs) Handles cbStatus.CheckedChanged
        vista(Me.pnlStatus, Me.cbStatus.Checked, Me.upStatus)
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

                'query = "select id_ms_recursos " +
                '        "     , Empresa " +
                '        "     , empleado " +
                '        "     , autorizador " +
                '        "     , convert(varchar, fecha_solicita, 103) + replace(replace(replace(right(convert(varchar, fecha_solicita, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') as fecha_solicita " +
                '        "     , convert(varchar, fecha_autoriza, 103) + replace(replace(replace(right(convert(varchar, fecha_autoriza, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') as fecha_autoriza " +
                '        "     , case admon_viajes_vobo when 'N' then 'No Aplica' else case when fecha_vobo is null then 'NA' else convert(varchar, fecha_vobo, 103) + replace(replace(replace(right(convert(varchar, fecha_vobo, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') end end as fecha_vobo " +
                '        "     , periodo_ini " +
                '        "     , periodo_fin " +
                '        "     , id_ms_anticipo as no_anticipo " +
                '        "     , id_ms_reserva as no_reserva " +
                '        "     , id_ms_comb as no_combustible " +
                '        "     , case status " +
                '        "         when 'P' then 'Pendiente de Autorización' " +
                '        "         when 'A' then 'Autorizado' " +
                '        "         when 'Z' then 'Solicitud No Autorizada' " +
                '        "       end as estatus " +
                '        "from ms_recursos " +
                '        "where 1 =1 "
                query = " select * from VTA_C_ms_recursos where 1=1 "

                If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                    query = query + " and (empleado = @autorizadorU or autorizador = @autorizadorU) "
                End If

                If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Conta" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Or .cbSolicitante.Checked = True Then
                    query = query + "  and id_usr_solicita = @id_usr_solicita "
                End If
                If .cbEmpresa.Checked = True Then
                    query = query + "  and empresa = @empresa "
                End If
                If .cbFechaC.Checked = True Then
                    query = query + "  and (FechSolc between @fechaIni and @fechaFin) "
                End If
                If .cbAutorizador.Checked = True Then
                    query = query + "  and autorizador = @autorizador "
                End If
                If .cbNoSolRec.Checked = True Then
                    query = query + "  and id_ms_recursos = @id_ms_recursos "
                End If
                If .cbStatus.Checked = True Then
                    query = query + "  and status in (" + .ddlStatus.SelectedValue + ") "
                End If
                query = query + "order by id_ms_recursos "

                sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)

                If ._txtPerfil.Text = "Aut" Or ._txtPerfil.Text = "SegViajes" Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@autorizadorU", ._txtEmpleado.Text)
                End If

                If ._txtPerfil.Text = "Usr" Or ._txtPerfil.Text = "UsrSL" Or ._txtPerfil.Text = "Liq" Or ._txtPerfil.Text = "Conta" Or ._txtPerfil.Text = "Vig" Or ._txtPerfil.Text = "ContaF" Or ._txtPerfil.Text = "CxP" Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_usr_solicita", Val(._txtIdUsuario.Text))
                End If
                If .cbSolicitante.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_usr_solicita", .ddlSolicitante.SelectedValue)
                End If
                If .cbEmpresa.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedValue)
                End If
                If .cbFechaC.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaIni", .wdpFechaI.Value)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaFin", .wdpFechaF.Date.AddHours(23).AddMinutes(59).AddSeconds(59))
                    'sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaFin", .wdpFechaF.Value)
                End If
                If .cbAutorizador.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@autorizador", .ddlAutorizador.SelectedValue)
                End If
                If .cbNoSolRec.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_ms_recursos", .txtNoSolRec.Text.Trim)
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

                .lblFolio.Text = .gvRegistros.SelectedRow.Cells(1).Text

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaSol As New SqlDataAdapter
                Dim dsSol As New DataSet
                Dim query As String
                query = "select ms_recursos.id_ms_recursos " +
                        "     , ms_recursos.empleado " +
                        "     , isnull(ms_anticipo.no_proveedor, '') as no_proveedor " +
                        "     , ms_recursos.autorizador " +
                        "     , isnull(ms_recursos.lugar_orig, '') as lugar_orig " +
                        "     , isnull(ms_recursos.lugar_dest, '') as lugar_dest " +
                        "     , ms_recursos.destino " +
                        "     , ms_recursos.empresa " +
                        "     , ms_recursos.actividad as just " +
                        "     , convert(varchar, ms_recursos.periodo_ini, 103) as periodo_ini " +
                        "     , convert(varchar, ms_recursos.periodo_fin, 103) as periodo_fin " +
                        "     , ms_recursos.tipo_transporte " +
                        "     , ms_recursos.incluye_anticipo " +
                        "     , ms_anticipo.id_ms_anticipo " +
                        "     , isnull(dias_hospedaje, 0) as dias_hospedaje " +
                        "     , monto_hospedaje " +
                        "     , isnull(dias_alimentos, 0) as dias_alimentos " +
                        "     , monto_alimentos " +
                        "     , isnull(dias_casetas, 0) as dias_casetas " +
                        "     , monto_casetas " +
                        "     , isnull(dias_otros, 0) as dias_otros " +
                        "     , monto_otros " +
                        "     , isnull(otros_especifico, 'XX') as otros_especifico " +
                        "     , case tipo_pago when 'E' then 'Efectivo' when 'T' then 'Transferencia' else '-' end as tipo_pago " +
                        "     , monto_hospedaje + monto_alimentos + monto_casetas + monto_otros as importe " +
                        "     , ms_recursos.incluye_reserva " +
                        "     , ms_recursos.lugares_dispo " +
                        "     , ms_recursos.lugares_reque " +
                        "     , ms_recursos.id_ms_reserva " +
                        "     , ms_reserva.fecha_ini " +
                        "     , ms_reserva.fecha_fin " +
                        "     , ms_reserva.no_eco as no_eco_reserva " +
                        "     , ms_reserva.modelo as modelo_reserva " +
                        "     , ms_recursos.incluye_hist_util " +
                        "     , dt_hist_util.id_dt_hist_util " +
                        "     , dt_hist_util.periodo_ini as periodo_ini_util " +
                        "     , dt_hist_util.periodo_fin as periodo_fin_util " +
                        "     , dt_hist_util.no_eco as no_eco_util " +
                        "     , dt_hist_util.modelo as modelo_util " +
                        "     , isnull(format(dt_hist_util.km_actual, 'N0', 'es-MX'), '') as km_actual " +
                        "     , ms_recursos.incluye_comb " +
                        "     , ms_recursos.id_ms_comb " +
                        "     , ms_comb.no_eco as no_eco_comb " +
                        "     , ms_comb.no_tarjeta_edenred " +
                        "     , isnull(format(ms_comb.litros_comb, 'N0', 'es-MX'), '') as litros_comb " +
                        "     , isnull(format(ms_comb.importe_comb, 'C2', 'es-MX'), '') as importe_comb " +
                        "     , ms_recursos.incluye_avion " +
                        "     , ms_avion.id_ms_avion " +
                        "     , convert(varchar, ms_avion.fecha_nacimiento, 103) as fecha_nacimiento " +
                        "     , ms_avion.fecha_salida as fecha_salida " +
                        "     , ms_avion.fecha_regreso as fecha_regreso " +
                        "     , isnull(ms_avion.justificacion, '') as justificacion " +
                        "from ms_recursos " +
                        "  left join ms_anticipo on ms_recursos.id_ms_anticipo = ms_anticipo.id_ms_anticipo " +
                        "  left join ms_reserva on ms_recursos.id_ms_reserva = ms_reserva.id_ms_reserva " +
                        "  left join dt_hist_util on ms_recursos.id_dt_hist_util = dt_hist_util.id_dt_hist_util " +
                        "  left join ms_comb on ms_recursos.id_ms_comb = ms_comb.id_ms_comb " +
                        "  left join ms_avion on ms_recursos.id_ms_avion = ms_avion.id_ms_avion " +
                        "where id_ms_recursos = @id_ms_recursos "
                sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaSol.SelectCommand.Parameters.AddWithValue("@id_ms_recursos", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaSol.Fill(dsSol)
                ConexionBD.Close()
                .lblSolicitante.Text = dsSol.Tables(0).Rows(0).Item("empleado").ToString()
                .lblNoProveedor.Text = dsSol.Tables(0).Rows(0).Item("no_proveedor").ToString()
                .lblAutorizador.Text = dsSol.Tables(0).Rows(0).Item("autorizador").ToString()
                .lblOrig.Text = dsSol.Tables(0).Rows(0).Item("lugar_orig").ToString()
                .lblDest.Text = dsSol.Tables(0).Rows(0).Item("lugar_dest").ToString()
                .lblDestino.Text = dsSol.Tables(0).Rows(0).Item("destino").ToString()
                .lblEmpresa.Text = dsSol.Tables(0).Rows(0).Item("empresa").ToString()
                .txtJust.Text = dsSol.Tables(0).Rows(0).Item("just").ToString()
                .lblPeriodoIni.Text = dsSol.Tables(0).Rows(0).Item("periodo_ini").ToString()
                .lblPeriodoFin.Text = dsSol.Tables(0).Rows(0).Item("periodo_fin").ToString()
                .lblTipoTansp.Text = dsSol.Tables(0).Rows(0).Item("tipo_transporte").ToString()
                'Anticipo
                If dsSol.Tables(0).Rows(0).Item("incluye_anticipo").ToString() = "S" Then
                    .cblRecursos.Items(0).Selected = True
                    .pnlAnticipo.Visible = True

                    .lblFolioA.Text = dsSol.Tables(0).Rows(0).Item("id_ms_anticipo").ToString()
                    If Val(dsSol.Tables(0).Rows(0).Item("dias_hospedaje").ToString()) = 0 Then
                        .wneDiasH.Text = ""
                        .wceMontoH.Text = ""
                    Else
                        .wneDiasH.Text = Val(dsSol.Tables(0).Rows(0).Item("dias_hospedaje").ToString())
                        .wceMontoH.Text = Val(dsSol.Tables(0).Rows(0).Item("monto_hospedaje").ToString())
                    End If
                    If Val(dsSol.Tables(0).Rows(0).Item("dias_alimentos").ToString()) = 0 Then
                        .wneDiasA.Text = ""
                        .wceMontoA.Text = ""
                    Else
                        .wneDiasA.Text = Val(dsSol.Tables(0).Rows(0).Item("dias_alimentos").ToString())
                        .wceMontoA.Text = Val(dsSol.Tables(0).Rows(0).Item("monto_alimentos").ToString())
                    End If
                    If Val(dsSol.Tables(0).Rows(0).Item("dias_casetas").ToString()) = 0 Then
                        .wneDiasC.Text = ""
                        .wceMontoC.Text = ""
                    Else
                        .wneDiasC.Text = Val(dsSol.Tables(0).Rows(0).Item("dias_casetas").ToString())
                        .wceMontoC.Text = Val(dsSol.Tables(0).Rows(0).Item("monto_casetas").ToString())
                    End If
                    If Val(dsSol.Tables(0).Rows(0).Item("dias_otros").ToString()) = 0 Then
                        .wneDiasO.Text = ""
                        .wceMontoO.Text = ""
                        .lblOtros.Text = ""
                    Else
                        .wneDiasO.Text = Val(dsSol.Tables(0).Rows(0).Item("dias_otros").ToString())
                        .wceMontoO.Text = Val(dsSol.Tables(0).Rows(0).Item("monto_otros").ToString())
                        .lblOtros.Text = dsSol.Tables(0).Rows(0).Item("otros_especifico").ToString()
                    End If
                    .lblTipoPago.Text = dsSol.Tables(0).Rows(0).Item("tipo_pago").ToString()
                    .wceMontoT.Text = Val(dsSol.Tables(0).Rows(0).Item("importe").ToString())
                    .lblMontoTLetra.Text = "(" + montoLetra() + ")"
                Else
                    .cblRecursos.Items(0).Selected = False
                    .pnlAnticipo.Visible = False
                End If
                .upAnticipo.Update()
                'Reserva / Hist. Utilitarios
                If dsSol.Tables(0).Rows(0).Item("incluye_reserva").ToString() = "S" Or dsSol.Tables(0).Rows(0).Item("incluye_hist_util").ToString() = "S" Then
                    .cblRecursos.Items(1).Selected = True
                    .pnlVehiculo.Visible = True

                    .lblLugaresDisp.Text = dsSol.Tables(0).Rows(0).Item("lugares_dispo").ToString()
                    .lblLugaresRequ.Text = dsSol.Tables(0).Rows(0).Item("lugares_reque").ToString()
                    If dsSol.Tables(0).Rows(0).Item("incluye_reserva").ToString() = "S" Then
                        'ms_reserva
                        .lbl_FolioV.Visible = True
                        .lblFolioV.Visible = True
                        .lblFolioV.Text = dsSol.Tables(0).Rows(0).Item("id_ms_reserva").ToString()
                        .lblVehiculo.Text = dsSol.Tables(0).Rows(0).Item("no_eco_reserva").ToString()
                        .lblModelo.Text = dsSol.Tables(0).Rows(0).Item("modelo_reserva").ToString()
                        .lblFechaIni.Text = dsSol.Tables(0).Rows(0).Item("fecha_ini").ToString()
                        .lblFechaFin.Text = dsSol.Tables(0).Rows(0).Item("fecha_fin").ToString()
                        .lblKmsActual.Text = "" 'queda vacío porque no aplica
                        .pnlKmActual.Visible = False
                    Else
                        'dt_hist_util
                        .lbl_FolioV.Visible = False 'No aplica
                        .lblFolioV.Visible = False
                        .lblFolioV.Text = dsSol.Tables(0).Rows(0).Item("id_dt_hist_util").ToString()
                        .lblVehiculo.Text = dsSol.Tables(0).Rows(0).Item("no_eco_util").ToString()
                        .lblModelo.Text = dsSol.Tables(0).Rows(0).Item("modelo_util").ToString()
                        .lblFechaIni.Text = dsSol.Tables(0).Rows(0).Item("periodo_ini_util").ToString()
                        .lblFechaFin.Text = dsSol.Tables(0).Rows(0).Item("periodo_fin_util").ToString()
                        .lblKmsActual.Text = dsSol.Tables(0).Rows(0).Item("km_actual").ToString()
                        .pnlKmActual.Visible = True
                    End If
                Else
                    .cblRecursos.Items(1).Selected = False
                    .pnlVehiculo.Visible = False
                End If
                .upVehiculo.Update()
                .upKmActual.Update()
                'Combustible
                If dsSol.Tables(0).Rows(0).Item("incluye_comb").ToString() = "S" Then
                    .cblRecursos.Items(2).Selected = True
                    .pnlCombustible.Visible = True
                    .lblFolioC.Text = dsSol.Tables(0).Rows(0).Item("id_ms_comb").ToString()
                    .lblVehiculoC.Text = dsSol.Tables(0).Rows(0).Item("no_eco_comb").ToString()
                    .lblTarjEdenred.Text = dsSol.Tables(0).Rows(0).Item("no_tarjeta_edenred").ToString()
                    .lblLitros.Text = dsSol.Tables(0).Rows(0).Item("litros_comb").ToString()
                    .lblImporte.Text = dsSol.Tables(0).Rows(0).Item("importe_comb").ToString()
                Else
                    .cblRecursos.Items(2).Selected = False
                    .pnlCombustible.Visible = False
                End If
                .upCombustible.Update()
                'Avión
                If dsSol.Tables(0).Rows(0).Item("incluye_avion").ToString() = "S" Then
                    .cblRecursos.Items(3).Selected = True
                    .pnlAvion.Visible = True
                    .lblFolioAv.Text = dsSol.Tables(0).Rows(0).Item("id_ms_avion").ToString()
                    .lblFechaNac.Text = dsSol.Tables(0).Rows(0).Item("fecha_nacimiento").ToString()
                    .lblFechaSalida.Text = dsSol.Tables(0).Rows(0).Item("fecha_salida").ToString()
                    .lblFechaRegreso.Text = dsSol.Tables(0).Rows(0).Item("fecha_regreso").ToString()
                    .lblJustAv.Text = dsSol.Tables(0).Rows(0).Item("justificacion").ToString()
                    If .lblJustAv.Text = "" Then
                        .pnlJust.Visible = False
                    Else
                        .pnlJust.Visible = True
                    End If
                Else
                    .cblRecursos.Items(3).Selected = False
                    .pnlAvion.Visible = False
                End If
                .upAvion.Update()

                sdaSol.Dispose()
                dsSol.Dispose()

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

End Class