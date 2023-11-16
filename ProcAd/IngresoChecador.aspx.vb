Public Class IngresoChecador
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        _txtIdUsuario.Text = Session("id_usuario")

                        .cbEmpresa.Checked = False
                        .pnlEmpresa.Visible = False
                        .cbPeriodo.Checked = False
                        .pnlFecha.Visible = False
                        .cbDireccion.Checked = False
                        .cbNombre.Checked = False
                        .pnlDireccion.Visible = False
                        .pnlNombre.Visible = False
                        .cbPuesto.Checked = False
                        .pnlPuesto.Visible = False
                        .cbDepartamento.Checked = False
                        .pnlDepartamento.Visible = False
                        .cbHorario.Checked = False
                        .pnlHorario.Visible = False

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("SiLi")

                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        'Dim noEmpleado As Integer = 0

                        'Obtener número de empleado 
                        SCMValores.Connection = ConexionBD
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = " select no_empleado from bd_Empleado.dbo.cg_empleado empl" +
                                                 " left join bd_ProcAd.dbo.cg_usuario on cg_usuario.id_empleado = empl.id_empleado" +
                                                 " where id_usuario = @id_usuario"
                        SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        ._txtNoEmpleado.Text = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        'Llenar Empresas
                        Dim sdaEmpresa As New SqlDataAdapter
                        Dim dsEmpresa As New DataSet
                        'sdaEmpresa.SelectCommand = New SqlCommand("select distinct(empresa) as Empresa from ms_ingreso_checador  order by Empresa  asc ", ConexionBD)
                        sdaEmpresa.SelectCommand = New SqlCommand("select distinct(empresa) as Empresa from ms_ingreso_checador where no_empleado = @noEmpleado order by Empresa  asc ", ConexionBD)
                        sdaEmpresa.SelectCommand.Parameters.AddWithValue("@noEmpleado", Val(._txtNoEmpleado.Text))

                        .ddlEmpresa.DataSource = dsEmpresa
                        .ddlEmpresa.DataTextField = "Empresa"
                        .ddlEmpresa.DataValueField = "Empresa"
                        ConexionBD.Open()
                        sdaEmpresa.Fill(dsEmpresa)
                        .ddlEmpresa.DataBind()
                        ConexionBD.Close()
                        sdaEmpresa.Dispose()
                        dsEmpresa.Dispose()
                        .ddlEmpresa.SelectedIndex = -1

                        Dim sdaDireccion As New SqlDataAdapter
                        Dim dsDireccion As New DataSet
                        'sdaDireccion.SelectCommand = New SqlCommand("select distinct(direccion) as Direccion from ms_ingreso_checador order by Direccion  asc ", ConexionBD)
                        sdaDireccion.SelectCommand = New SqlCommand("select distinct(direccion) as Direccion from ms_ingreso_checador where no_empleado = @noEmpleado order by Direccion  asc ", ConexionBD)
                        sdaDireccion.SelectCommand.Parameters.AddWithValue("@noEmpleado", Val(._txtNoEmpleado.Text))

                        .ddlDireccion.DataSource = dsDireccion
                        .ddlDireccion.DataTextField = "Direccion"
                        .ddlDireccion.DataValueField = "Direccion"
                        ConexionBD.Open()
                        sdaDireccion.Fill(dsDireccion)
                        .ddlDireccion.DataBind()
                        ConexionBD.Close()
                        sdaDireccion.Dispose()
                        dsDireccion.Dispose()
                        .ddlDireccion.SelectedIndex = -1



                        Dim sdadepart As New SqlDataAdapter
                        Dim dsdepart As New DataSet
                        sdadepart.SelectCommand = New SqlCommand(" select distinct(departamento ) as depart from ms_ingreso_checador where no_empleado = @noEmpleado order by depart  asc ", ConexionBD)
                        sdadepart.SelectCommand.Parameters.AddWithValue("@noEmpleado", Val(._txtNoEmpleado.Text))
                        .ddlDepartamento.DataSource = dsdepart
                        .ddlDepartamento.DataTextField = "depart"
                        .ddlDepartamento.DataValueField = "depart"
                        ConexionBD.Open()
                        sdadepart.Fill(dsdepart)
                        .ddlDepartamento.DataBind()
                        ConexionBD.Close()
                        sdadepart.Dispose()
                        dsdepart.Dispose()
                        .ddlDepartamento.SelectedIndex = -1

                        Dim sdaPuesto As New SqlDataAdapter
                        Dim dsPuesto As New DataSet
                        sdaPuesto.SelectCommand = New SqlCommand("select distinct(puesto) as puesto from ms_ingreso_checador where departamento = @departamento order by puesto  asc", ConexionBD)
                        sdaPuesto.SelectCommand.Parameters.AddWithValue("@departamento", .ddlDepartamento.SelectedValue)

                        .ddlPuesto.DataSource = dsPuesto
                        .ddlPuesto.DataTextField = "puesto"
                        .ddlPuesto.DataValueField = "puesto"
                        ConexionBD.Open()
                        sdaPuesto.Fill(dsPuesto)
                        .ddlPuesto.DataBind()
                        ConexionBD.Close()
                        sdaPuesto.Dispose()
                        dsPuesto.Dispose()
                        .ddlPuesto.SelectedIndex = -1


                    Else
                        Server.Transfer("Login.aspx")
                    End If
                Catch ex As Exception
                    .litError.Text = ex.Message
                End Try
            End With
        End If
    End Sub
#Region "Funciones"

    Public Sub vista(ByRef control, ByVal valor)
        With Me
            If valor Then
                control.Visible = True
            Else
                control.Visible = False
            End If
        End With
    End Sub

    Public Sub pintarTablaTemp(ByRef gridView)
        With Me
            Try
                Dim i As Integer
                Dim ban As Integer
                Dim fecha As DateTime

                Dim result As Integer = 0
                Dim result1 As Integer = 0
                For i = 0 To (gridView.Rows.Count() - 1)
                    ban = 0
                    fecha = CDate(gridView.Rows(i).Cells(1).Text)

                    If fecha.TimeOfDay.ToString() > "08:10:59 AM" And fecha.TimeOfDay.ToString() < "18:00:00 pM" Then
                        ban = 0
                    Else
                        ban = 1
                    End If
                    Select Case ban

                        Case 0
                            gridView.Rows(i).Cells(1).ForeColor = Color.Red
                            gridView.Rows(i).Cells(1).Font.Bold = True
                        Case 1
                            gridView.Rows(i).Cells(1).ForeColor = Color.Black
                            gridView.Rows(i).Cells(1).Font.Bold = False
                    End Select
                Next

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub cbEmpresa_CheckedChanged(sender As Object, e As EventArgs) Handles cbEmpresa.CheckedChanged
        vista(Me.pnlEmpresa, Me.cbEmpresa.Checked)
    End Sub

    Protected Sub cbPeriodo_CheckedChanged(sender As Object, e As EventArgs) Handles cbPeriodo.CheckedChanged
        vista(Me.pnlFecha, Me.cbPeriodo.Checked)
    End Sub

    Protected Sub cbDireccion_CheckedChanged(sender As Object, e As EventArgs) Handles cbDireccion.CheckedChanged
        vista(Me.pnlDireccion, Me.cbDireccion.Checked)
    End Sub

    Protected Sub cbNombre_CheckedChanged(sender As Object, e As EventArgs) Handles cbNombre.CheckedChanged
        vista(Me.pnlNombre, Me.cbNombre.Checked)
    End Sub

    Protected Sub cbPuesto_CheckedChanged(sender As Object, e As EventArgs) Handles cbPuesto.CheckedChanged
        vista(Me.pnlPuesto, Me.cbPuesto.Checked)
    End Sub

    Protected Sub cbDepartamento_CheckedChanged(sender As Object, e As EventArgs) Handles cbDepartamento.CheckedChanged
        vista(Me.pnlDepartamento, Me.cbDepartamento.Checked)
    End Sub

    Protected Sub cbHorario_CheckedChanged(sender As Object, e As EventArgs) Handles cbHorario.CheckedChanged
        vista(Me.pnlHorario, Me.cbHorario.Checked)
    End Sub

    'Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
    '    With Me
    '        Try
    '            .litError.Text = ""
    '            .pnlRegistros.Visible = True
    '            .gvRegistrosT.Visible = True
    '            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
    '            ConexionBD.ConnectionString = accessDB.conBD("SiLi")
    '            Dim query As String
    '            Dim sdaConsulta As New SqlDataAdapter
    '            Dim dsConsulta As New DataSet
    '            query = " select no_empleado ,Ingreso, nombre ,direccion ,empresa ,departamento,puesto ,centro_consto ,ubicacion ,tipoNomina  from ms_ingreso_checador where 1=1 "
    '            If .cbEmpresa.Checked = True Then
    '                query = query + "  and empresa = @empresa "

    '            End If
    '            If .cbDireccion.Checked = True Then
    '                query = query + "  and direccion = @direccion "

    '            End If
    '            If .cbPuesto.Checked = True Then
    '                query = query + "  and puesto = @puesto "

    '            End If
    '            If .cbPeriodo.Checked = True Then
    '                query = query + "  and (Ingreso BETWEEN @f1 AND @f2) "

    '            End If
    '            If .cbNombre.Checked = True Then
    '                query = query + "  and nombre  like '%'+ @nombre + '%'"

    '            End If
    '            If .cbDepartamento.Checked = True Then
    '                query = query + "  and departamento = @departamento "


    '            End If
    '            If .cbHorario.Checked = True Then
    '                If rbtnAm.Checked = True Then
    '                    query = query + "  and DATEPART(HOUR, Ingreso) < 12 "

    '                ElseIf rbtnPm.Checked = True Then

    '                    query = query + "  and DATEPART(HOUR, Ingreso) < 12 "
    '                End If
    '            End If

    '            sdaConsulta.SelectCommand = New SqlCommand(query + " order by nombre, Ingreso ASC ", ConexionBD)


    '            If .cbEmpresa.Checked = True Then
    '                sdaConsulta.SelectCommand.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedValue)
    '            End If
    '            If .cbDireccion.Checked = True Then
    '                sdaConsulta.SelectCommand.Parameters.AddWithValue("@direccion", .ddlDireccion.SelectedValue)
    '            End If
    '            If .cbPuesto.Checked = True Then
    '                sdaConsulta.SelectCommand.Parameters.AddWithValue("@puesto", .ddlPuesto.SelectedValue)
    '            End If
    '            If .cbPeriodo.Checked = True Then
    '                sdaConsulta.SelectCommand.Parameters.AddWithValue("@f1", .wdpFechaI.Date)
    '                sdaConsulta.SelectCommand.Parameters.AddWithValue("@f2", .wdpFechaF.Date)
    '            End If
    '            If .cbNombre.Checked = True Then
    '                sdaConsulta.SelectCommand.Parameters.AddWithValue("@nombre", .txtNombre.Text)
    '            End If
    '            If .cbDepartamento.Checked = True Then
    '                sdaConsulta.SelectCommand.Parameters.AddWithValue("@departamento", .ddlDepartamento.SelectedValue)
    '            End If


    '            .gvRegistrosT.DataSource = dsConsulta
    '            ConexionBD.Open()
    '            sdaConsulta.Fill(dsConsulta)
    '            ConexionBD.Close()
    '            .gvRegistrosT.DataBind()
    '            sdaConsulta.Dispose()
    '            dsConsulta.Dispose()

    '            pintarTablaTemp(.gvRegistrosT)
    '        Catch ex As Exception
    '            .litError.Text = ex.ToString
    '        End Try
    '    End With
    'End Sub


    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        With Me
            Try
                .litError.Text = ""
                .pnlRegistros.Visible = True
                .gvRegistrosT.Visible = True
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("SiLi")
                Dim query As String
                Dim sdaConsulta As New SqlDataAdapter
                Dim dsConsulta As New DataSet

                query = " select no_empleado ,Ingreso, nombre ,direccion ,empresa ,departamento,puesto ,centro_consto ,ubicacion ,tipoNomina from ms_ingreso_checador where departamento = @departamento"

                If .cbEmpresa.Checked = True Then
                    query = query + "  and empresa = @empresa "

                End If

                If .cbDireccion.Checked = True Then
                    query = query + "  and direccion = @direccion "

                End If
                If .cbPuesto.Checked = True Then
                    query = query + "  and puesto = @puesto "

                End If
                If .cbPeriodo.Checked = True Then
                    query = query + "  and (Ingreso BETWEEN @f1 AND @f2) "

                End If
                If .cbNombre.Checked = True Then
                    query = query + "  and nombre  like '%'+ @nombre + '%'"

                End If

                If .cbHorario.Checked = True Then
                    If rbtnAm.Checked = True Then
                        query = query + "  and DATEPART(HOUR, Ingreso) < 12 "

                    ElseIf rbtnPm.Checked = True Then

                        query = query + "  and DATEPART(HOUR, Ingreso) < 12 "
                    End If
                End If

                sdaConsulta.SelectCommand = New SqlCommand(query + " order by nombre, Ingreso ASC ", ConexionBD)


                sdaConsulta.SelectCommand.Parameters.AddWithValue("@departamento", .ddlDepartamento.SelectedValue)

                If cbEmpresa.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedValue)

                End If

                If .cbDireccion.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@direccion", .ddlDireccion.SelectedValue)
                End If
                If .cbPuesto.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@puesto", .ddlPuesto.SelectedValue)
                End If
                If .cbPeriodo.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@f1", .wdpFechaI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@f2", .wdpFechaF.Date)
                End If
                If .cbNombre.Checked = True Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@nombre", .txtNombre.Text)
                End If




                .gvRegistrosT.DataSource = dsConsulta
                ConexionBD.Open()
                sdaConsulta.Fill(dsConsulta)
                ConexionBD.Close()
                .gvRegistrosT.DataBind()
                sdaConsulta.Dispose()
                dsConsulta.Dispose()

                pintarTablaTemp(.gvRegistrosT)

                If gvRegistrosT.Rows.Count() = 0 Then
                    litError.Text = "No existen registros para esos valores, rectifique"
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub



    Protected Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        Dim tw As New System.IO.StringWriter
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        With Me
            Try
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment;filename=Registros.xls")
                Response.ContentType = "application/vnd.ms-excel"
                .gvRegistrosT.RenderControl(hw)
                Response.Write(tw.ToString())
                Response.End()
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub


    Public Sub registrosEspecificos(ByVal noEmpleado)
        With Me
            Try
                Dim ConexionBDNom As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBDNom.ConnectionString = accessDB.conBD("NOM")
                Dim sdaEmpleado As New SqlDataAdapter
                Dim dsEmpleado As New DataSet
                Dim query As String
                query = " "
                query = "  select REPLACE(LTRIM(REPLACE(noEmpleado,'0',' ')),' ','0') as no_empleado  ,  " +
                        "  nombreCompleto as nombre, direccion as Direccion , unidadNegocio as Empresa, " +
                        "  gerencia as departamento,Puesto as puesto ,centroCostos as centro_consto, " +
                        "  ubicacion as ubicacion , tipoNomina " +
                        "  from VTA_NOM_Empleado WHERE  REPLACE(LTRIM(REPLACE(noEmpleado ,'0',' ')),' ','0') = @no_emple  and estatus ='A' "
                sdaEmpleado.SelectCommand = New SqlCommand(query, ConexionBDNom)
                sdaEmpleado.SelectCommand.Parameters.AddWithValue("@no_emple", noEmpleado)
                ConexionBDNom.Open()
                sdaEmpleado.Fill(dsEmpleado)
                ConexionBDNom.Close()

                If dsEmpleado.Tables(0).Rows.Count > 0 Then
                    .ddlEmpresa.SelectedValue = dsEmpleado.Tables(0).Rows(0).Item("Empresa").ToString()
                    .ddlDireccion.SelectedValue = dsEmpleado.Tables(0).Rows(0).Item("Direccion").ToString()
                    .ddlDepartamento.SelectedValue = dsEmpleado.Tables(0).Rows(0).Item("departamento").ToString()

                    'Inhabilitar los ddl
                    .ddlEmpresa.Enabled = False
                    .ddlDireccion.Enabled = False
                    .ddlDepartamento.Enabled = False

                    'Puestos de a cuerdo al departamento del usuario
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("SiLi")
                    Dim sdaPuesto As New SqlDataAdapter
                    Dim dsPuesto As New DataSet
                    sdaPuesto.SelectCommand = New SqlCommand("select distinct(puesto) as puesto from ms_ingreso_checador where departamento = @departamento  order by puesto  asc", ConexionBD)
                    sdaPuesto.SelectCommand.Parameters.AddWithValue("departamento", .ddlDepartamento.SelectedValue)
                    .ddlPuesto.DataSource = dsPuesto
                    .ddlPuesto.DataTextField = "puesto"
                    .ddlPuesto.DataValueField = "puesto"
                    ConexionBD.Open()
                    sdaPuesto.Fill(dsPuesto)
                    .ddlPuesto.DataBind()
                    ConexionBD.Close()
                    sdaPuesto.Dispose()
                    dsPuesto.Dispose()
                    .ddlPuesto.SelectedIndex = -1
                End If

                sdaEmpleado.Dispose()
                dsEmpleado.Dispose()
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

#End Region

End Class