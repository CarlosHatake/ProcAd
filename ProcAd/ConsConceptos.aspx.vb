Public Class ConsConceptos
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        cbConcepto.Checked = False
                        cbServicios.Checked = False
                        pnlSolicitante.Visible = False
                        pnlServicios.Visible = False
                        ddlConcepto.SelectedIndex = 0
                        ddlServicios.SelectedIndex = 0
                        pnlExportar.Visible = False
                    Else
                        Server.Transfer("Login.aspx")
                    End If
                Catch ex As Exception
                    litError.Text = ex.Message
                End Try
            End With
        End If
    End Sub

#End Region

#Region "Funciones"
    Public Function validarCheck()
        Dim bandera As Boolean = True
        If (cbConcepto.Checked = True And ddlConcepto.SelectedValue = "'NA'") Or (cbServicios.Checked = True And ddlServicios.SelectedValue = "'NA'") Then
            bandera = False
        Else
            bandera = True
        End If

        Return bandera
    End Function

    Public Sub llenarGrid()
        Try
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim sdaGVProducto As New SqlDataAdapter
            Dim dsGVProducto As New DataSet
            gvRegistrosCon.DataSource = dsGVProducto
            gvServicios.DataSource = dsGVProducto
            Dim query As String = ""
            Dim valor As Integer = 0
            If ddlConcepto.SelectedValue = "'F'" Then
                gvRegistrosCon.Columns(2).Visible = True
                query = "SELECT concepto, iva, cuenta, cve_concepto1, abreviatura FROM cg_concepto_comp " +
                    "  where abreviatura = 'GOI' " +
                    "  order  by id_concepto_comp asc "
                valor = 1
            ElseIf ddlConcepto.SelectedValue = "'SNF'" Then
                gvRegistrosCon.Columns(2).Visible = False
                query = "SELECT nombre_concepto as concepto, cuenta, cve_concepto1, abreviatura FROM cg_concepto " +
                    "  where abreviatura = 'GOI' " +
                    "  order by id_concepto asc  "
                valor = 1
            ElseIf ddlServicios.SelectedValue = "'S'" Then
                query = "select servicio, tipo_servicio, admon_oper,  " +
                        "isnull ((SELECT nombre from bd_Empleado.dbo.cg_empresa where id_empresa = dt.id_empresa), 'TODAS') as empresa " +
                        "from cg_servicio CG " +
                        "inner join dt_servicio_conf DT on CG.id_servicio = DT.id_servicio "
                valor = 2
            ElseIf ddlServicios.SelectedValue = "'AU'" Then
                query = "select servicio, tipo_servicio, admon_oper,  " +
                        "isnull ((SELECT nombre from bd_Empleado.dbo.cg_empresa where id_empresa = dt.id_empresa), 'TODAS') as empresa " +
                        "from cg_servicio CG " +
                        "inner join dt_servicio_conf DT on CG.id_servicio = DT.id_servicio "
                valor = 2
            End If

            sdaGVProducto.SelectCommand = New SqlCommand(query, ConexionBD)
            ConexionBD.Open()
            sdaGVProducto.Fill(dsGVProducto)
            If valor = 1 Then
                gvRegistrosCon.DataBind()
            Else
                gvServicios.DataBind()
            End If
            ConexionBD.Close()
            If gvRegistrosCon.Rows.Count = 0 And gvServicios.Rows.Count = 0 Then
                pnlExportar.Visible = False
                gvRegistrosCon.Visible = False
                gvServicios.Visible = False
                btnBuscar.Enabled = True
            Else
                pnlExportar.Visible = True
                btnExportar.Visible = True
                If cbConcepto.Checked = True Then
                    gvRegistrosCon.Visible = True
                Else
                    gvServicios.Visible = True
                End If
                litError.Text = "Para otra busqueda seleccione otra opción"

            End If
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Public Sub exportar()
        Try
            ''
            litError.Text = ""
            Dim noEmp As Integer
            noEmp = 9
            'EXCEL'
            Dim dtRegistros As DataTable
            Dim workbook As Workbook = New Workbook()
            If cbConcepto.Checked = True Then
                workbook.LoadFromFile(Server.MapPath("ConsultaConceptos\ConsConceptos.xlsx"))
            Else
                workbook.LoadFromFile(Server.MapPath("ConsultaConceptos\Servicios.xlsx"))
            End If

            Dim sheet As Worksheet = workbook.Worksheets(0)
            dtRegistros = sheet.ExportDataTable()

            'CONSULTA PARA LLENAR EL EXCEL'
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim sdaConsulta As New SqlDataAdapter
            Dim dsConsulta As New DataTable
            Dim query As String = ""
            Dim registros As Integer
            Dim valor As Integer = 0

            If ddlConcepto.SelectedValue = "'F'" Then
                query = "SELECT concepto, iva, cuenta, cve_concepto1, abreviatura, cve_concepto2  FROM cg_concepto_comp " +
                   "  where abreviatura = 'GOI' and status = 'A' " +
                   "  order  by id_concepto_comp asc "
                valor = 1
            ElseIf ddlConcepto.SelectedValue = "'SNF'" Then
                query = "SELECT nombre_concepto as concepto, ' ' as iva, cuenta, cve_concepto1, abreviatura, cve_concepto2 FROM cg_concepto " +
                  "  where abreviatura = 'GOI' and status = 'A' " +
                  "  order by id_concepto asc  "
                valor = 1
            ElseIf ddlServicios.SelectedValue = "'S'" Then
                query = "select servicio, tipo_servicio, id_dt_servicio_conf as ID_Configuración, " +
                        "admon_oper, " +
                        "(SELECT nombre from bd_Empleado.dbo.cg_empresa where id_empresa = dt.id_empresa) as empresa, " +
                        "cuenta_cont, " +
                        "(SELECT nombre from bd_Empleado.dbo.cg_empleado BDE inner join cg_usuario CGU on CGU.id_empleado = BDE.id_empleado where CGU.id_usuario = DT.id_usr_valida) as validador, " +
                        "finanzas, valida_presup, " +
                        "(SELECT nombre from bd_Empleado.dbo.cg_empleado BDE inner join cg_usuario CGU on CGU.id_empleado = BDE.id_empleado where CGU.id_usuario = DT.id_usr_valida2) as validador2, " +
                        "(SELECT nombre from bd_Empleado.dbo.cg_empleado BDE inner join cg_usuario CGU on CGU.id_empleado = BDE.id_empleado where CGU.id_usuario = DT.id_usr_autoriza1) as autorizador," +
                        "(SELECT nombre from bd_Empleado.dbo.cg_empleado BDE inner join cg_usuario CGU on CGU.id_empleado = BDE.id_empleado where CGU.id_usuario = DT.id_usr_autoriza2) as autorizador2, " +
                        "(SELECT nombre from bd_Empleado.dbo.cg_empleado BDE inner join cg_usuario CGU on CGU.id_empleado = BDE.id_empleado where CGU.id_usuario = DT.id_usr_autoriza3) as autorizador3 " +
                        "From cg_servicio CG " +
                        "inner Join dt_servicio_conf DT on CG.id_servicio = DT.id_servicio "
                valor = 2
            ElseIf ddlServicios.SelectedValue = " 'AU'" Then
                query = "select  servicio, tipo_servicio, id_dt_servicio_conf as ID_Configuración, " +
                        "admon_oper, " +
                        "isnull((SELECT nombre from bd_Empleado.dbo.cg_empresa where id_empresa = dt.id_empresa), 'TODAS') as empresa, " +
                        " ' ' as cuenta_cont " +
                        "(SELECT nombre from bd_Empleado.dbo.cg_empleado BDE inner join cg_usuario CGU on CGU.id_empleado = BDE.id_empleado where CGU.id_usuario = DT.id_usr_valida) as validador, " +
                        " ' ' as finanzas, ' ' as valida_presup " +
                        "(SELECT nombre from bd_Empleado.dbo.cg_empleado BDE inner join cg_usuario CGU on CGU.id_empleado = BDE.id_empleado where CGU.id_usuario = DT.id_usr_valida2) as validador2, " +
                        "(SELECT nombre from bd_Empleado.dbo.cg_empleado BDE inner join cg_usuario CGU on CGU.id_empleado = BDE.id_empleado where CGU.id_usuario = DT.id_usr_autoriza1) as autorizador, " +
                        "(SELECT nombre from bd_Empleado.dbo.cg_empleado BDE inner join cg_usuario CGU on CGU.id_empleado = BDE.id_empleado where CGU.id_usuario = DT.id_usr_autoriza2) as autorizador2, " +
                        "(SELECT nombre from bd_Empleado.dbo.cg_empleado BDE inner join cg_usuario CGU on CGU.id_empleado = BDE.id_empleado where CGU.id_usuario = DT.id_usr_autoriza3) as autorizador3 " +
                        "From cg_servicio CG " +
                        "inner Join dt_servicio_conf DT on CG.id_servicio = DT.id_servicio "
                valor = 3
            End If
            sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)
            ConexionBD.Open()
            sdaConsulta.Fill(dsConsulta)
            ConexionBD.Close()
            registros = dsConsulta.Rows.Count() - 1

            'Carga de datos'
            Dim casilla As String
            Dim id As Integer = 1
            casilla = "A"
            If valor = 1 Then

                For index As Integer = 0 To registros
                    id = id + 1
                    'Abreviatura'
                    casilla = "A" + CStr(id)
                    sheet.Range(casilla).Text = dsConsulta.Rows(index).Item(4).ToString

                    'concepto'
                    casilla = "B" + CStr(id)
                    sheet.Range(casilla).Text = dsConsulta.Rows(index).Item(0).ToString()

                    'iva'
                    casilla = "C" + CStr(id)
                    If ddlConcepto.SelectedValue = "F" Then
                        sheet.Range(casilla).Text = dsConsulta.Rows(index).Item(1).ToString()
                    Else
                        sheet.Range(casilla).Text = "No aplica"
                    End If

                    'cuenta'
                    casilla = "D" + CStr(id)
                    sheet.Range(casilla).Text = dsConsulta.Rows(index).Item(2).ToString

                    'cve_conceto1'
                    casilla = "E" + CStr(id)
                    sheet.Range(casilla).Text = dsConsulta.Rows(index).Item(3).ToString


                    'cve_concepto2'
                    casilla = "F" + CStr(id)
                    sheet.Range(casilla).Text = dsConsulta.Rows(index).Item(5).ToString

                Next
            Else

                For index As Integer = 0 To registros
                    id = id + 1
                    'Servicio'
                    casilla = "A" + CStr(id)
                    sheet.Range(casilla).Text = dsConsulta.Rows(index).Item(0).ToString

                    'tipo servicio'
                    casilla = "B" + CStr(id)
                    sheet.Range(casilla).Text = dsConsulta.Rows(index).Item(1).ToString()

                    'Id configuracion'
                    casilla = "C" + CStr(id)
                    sheet.Range(casilla).Text = dsConsulta.Rows(index).Item(2).ToString()

                    'admon_oper'
                    casilla = "D" + CStr(id)
                    sheet.Range(casilla).Text = dsConsulta.Rows(index).Item(3).ToString

                    'empresa'
                    casilla = "E" + CStr(id)
                    sheet.Range(casilla).Text = dsConsulta.Rows(index).Item(4).ToString


                    'cuenta_cont -3'
                    casilla = "F" + CStr(id)
                    sheet.Range(casilla).Text = dsConsulta.Rows(index).Item(5).ToString

                    'validador'
                    casilla = "G" + CStr(id)
                    sheet.Range(casilla).Text = dsConsulta.Rows(index).Item(6).ToString

                    'finanzas -3'
                    casilla = "H" + CStr(id)
                    sheet.Range(casilla).Text = dsConsulta.Rows(index).Item(7).ToString


                    'validada presupuesto -3'
                    casilla = "I" + CStr(id)
                    sheet.Range(casilla).Text = dsConsulta.Rows(index).Item(8).ToString

                    'validador 2'
                    casilla = "J" + CStr(id)
                    sheet.Range(casilla).Text = dsConsulta.Rows(index).Item(9).ToString

                    'Autorizador'
                    casilla = "K" + CStr(id)
                    sheet.Range(casilla).Text = dsConsulta.Rows(index).Item(10).ToString

                    'Autorizador 2'
                    casilla = "L" + CStr(id)
                    sheet.Range(casilla).Text = dsConsulta.Rows(index).Item(11).ToString

                    'Autorizador 3'
                    casilla = "M" + CStr(id)
                    sheet.Range(casilla).Text = dsConsulta.Rows(index).Item(12).ToString

                Next

            End If


            ' sheet.Protect("C12$xzB89fN8+")
            If cbConcepto.Checked = True Then
                workbook.SaveToFile(Server.MapPath("ConsultaConceptos\" + _txtIdUsuario.Text.ToString() + "-" + "ConsConceptos.xlsx"))
            Else
                workbook.SaveToFile(Server.MapPath("ConsultaConceptos\" + _txtIdUsuario.Text.ToString() + "-" + "Servicios.xlsx"))
            End If

            System.Diagnostics.Process.Start(workbook.FileName)
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "Botones"
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            If validarCheck() Then
                llenarGrid()
            Else
                litError.Text = "Favor de seleccionar una opción"
            End If
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        Try
            If gvRegistrosCon.Rows.Count <> 0 Or gvServicios.Rows.Count <> 0 Then
                exportar()
            End If
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlConcepto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConcepto.SelectedIndexChanged
        Try

            pnlExportar.Visible = False
            btnBuscar.Enabled = True
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlServicios_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlServicios.SelectedIndexChanged
        Try
            pnlExportar.Visible = False
            btnBuscar.Enabled = True

        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub cbConcepto_CheckedChanged(sender As Object, e As EventArgs) Handles cbConcepto.CheckedChanged
        Try
            If cbConcepto.Checked = True Then
                pnlSolicitante.Visible = True
                pnlServicios.Visible = False
                cbServicios.Checked = False
                btnBuscar.Enabled = True
                gvRegistrosCon.Visible = False
                gvServicios.Visible = False
                btnExportar.Visible = False
            Else
                pnlSolicitante.Visible = False
                btnExportar.Visible = False
            End If
            ddlConcepto.SelectedIndex = -1
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub cbServicios_CheckedChanged(sender As Object, e As EventArgs) Handles cbServicios.CheckedChanged
        Try
            If cbServicios.Checked = True Then
                pnlServicios.Visible = True
                pnlSolicitante.Visible = False
                cbConcepto.Checked = False
                btnBuscar.Enabled = True
                gvRegistrosCon.Visible = False
                gvServicios.Visible = False
                btnExportar.Visible = False
            Else
                pnlServicios.Visible = False
                btnExportar.Visible = False
            End If
            ddlServicios.SelectedIndex = -1
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub


#End Region

End Class