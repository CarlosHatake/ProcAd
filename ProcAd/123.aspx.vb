Public Class _123
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        _txtIdUsuario.Text = Session("id_usuario")
                        _txtBan.Text = 0
                        'Creación de Variables para la conexión y consulta de información a la base de datos
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        'Nombre del Solicitante
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "select nombre + ' ' + ap_paterno + ' ' + ap_materno " +
                                                 "from cg_usuario " +
                                                 "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                 "where id_usuario = @idUsuario "
                        SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        lblSolicitante.Text = SCMValores.ExecuteScalar()
                        ConexionBD.Close()
                        'Lista de Empresas
                        Dim sdaEmpresa As New SqlDataAdapter
                        Dim dsEmpresa As New DataSet
                        sdaEmpresa.SelectCommand = New SqlCommand("select id_empresa, nombre " +
                                                                  "from bd_empleado.dbo.cg_empresa Empresa " +
                                                                  "where status = 'A' " +
                                                                  "order by nombre", ConexionBD)
                        .ddlEmpresa.DataSource = dsEmpresa
                        .ddlEmpresa.DataTextField = "nombre"
                        .ddlEmpresa.DataValueField = "id_empresa"
                        ConexionBD.Open()
                        sdaEmpresa.Fill(dsEmpresa)
                        .ddlEmpresa.DataBind()
                        ConexionBD.Close()
                        sdaEmpresa.Dispose()
                        dsEmpresa.Dispose()
                        .ddlEmpresa.SelectedValue = 9
                        'Centros de Costo
                        llenarCC()
                        'División
                        llenarDiv()

                        rblAdmonOper.SelectedIndex = 0
                        'Llenar el autorizador'
                        Dim sdaAut As New SqlDataAdapter
                        Dim dsAut As New DataSet
                        sdaAut.SelectCommand = New SqlCommand("select cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as autorizador " +
                                                                "     , id_autorizador " +
                                                                "     , case aut_dir when 'S' then 'Sí' else null end as director " +
                                                                "from dt_autorizador " +
                                                                "  left join cg_usuario on dt_autorizador.id_autorizador = cg_usuario.id_usuario " +
                                                                "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                                "where dt_autorizador.id_usuario = @idUsuario " +
                                                                "order by director, autorizador ", ConexionBD)
                        sdaAut.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(_txtIdUsuario.Text))
                        ddlAutorizador.DataSource = dsAut
                        ddlAutorizador.DataTextField = "autorizador"
                        ddlAutorizador.DataValueField = "id_autorizador"
                        ConexionBD.Open()
                        sdaAut.Fill(dsAut)
                        ddlAutorizador.DataBind()
                        ConexionBD.Close()
                        sdaAut.Dispose()
                        dsAut.Dispose()

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


    Public Sub llenarCC()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCC As New SqlDataAdapter
                Dim dsCC As New DataSet
                sdaCC.SelectCommand = New SqlCommand("select 0 as id_cc " +
                                                     "     , ' ' as nombre " +
                                                     "union " +
                                                     "select id_cc " +
                                                     "     , nombre " +
                                                     "from bd_Empleado.dbo.cg_cc " +
                                                     "where id_empresa = @idEmpresa " +
                                                     "  and status = 'A' " +
                                                     "order by nombre ", ConexionBD)
                sdaCC.SelectCommand.Parameters.AddWithValue("@idEmpresa", .ddlEmpresa.SelectedValue)
                .ddlCC.DataSource = dsCC
                .ddlCC.DataTextField = "nombre"
                .ddlCC.DataValueField = "id_cc"
                ConexionBD.Open()
                sdaCC.Fill(dsCC)
                .ddlCC.DataBind()
                ConexionBD.Close()
                sdaCC.Dispose()
                dsCC.Dispose()
                .ddlCC.SelectedIndex = -1
                .upCC.Update()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarDiv()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaDiv As New SqlDataAdapter
                Dim dsDiv As New DataSet
                sdaDiv.SelectCommand = New SqlCommand("select 0 as id_div " +
                                                      "     , ' ' as nombre " +
                                                      "union " +
                                                      "select id_div " +
                                                      "     , nombre " +
                                                      "from bd_Empleado.dbo.cg_div " +
                                                      "where id_empresa = @idEmpresa " +
                                                      "  and status = 'A' " +
                                                      "order by nombre ", ConexionBD)
                sdaDiv.SelectCommand.Parameters.AddWithValue("@idEmpresa", ddlEmpresa.SelectedValue)
                .ddlDiv.DataSource = dsDiv
                .ddlDiv.DataTextField = "nombre"
                .ddlDiv.DataValueField = "id_div"
                ConexionBD.Open()
                sdaDiv.Fill(dsDiv)
                .ddlDiv.DataBind()
                ConexionBD.Close()
                sdaDiv.Dispose()
                dsDiv.Dispose()
                .ddlDiv.SelectedIndex = -1
                .upDiv.Update()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub


#End Region
#Region "Botones"
    Protected Sub btnAgregarAdj_Click(sender As Object, e As EventArgs) Handles btnAgregarAdj.Click
        With Me
            Try
                litError.Text = ""
                If System.IO.Path.GetFileName(fuAdjunto.PostedFile.FileName) = "" Then
                    litError.Text = "No hay ningun archivo adjunto"
                Else
                    gvAdjuntosComp.Columns(2).Visible = True
                    gvAdjuntosComp.Columns(3).Visible = True

                    'Agregar evidencia'
                    Dim rutaArchivo As String = "Evidencias MovLib\" 'Ruta en que se almacenará el archivo
                    Dim sFileNameAr As String = System.IO.Path.GetFileName(fuAdjunto.PostedFile.FileName)
                    'Guarda archivo'
                    fuAdjunto.PostedFile.SaveAs(Server.MapPath("Evidencias MovLib\" + _txtIdUsuario.Text.ToString + "-" + sFileNameAr))

                    If gvAdjuntosComp.Rows.Count <> 0 Then
                        Dim tabla As DataTable = New DataTable
                        tabla.Columns.Add("nombre", GetType(String))
                        tabla.Columns.Add("ruta", GetType(String))
                        tabla.Columns.Add("nombre_archivo", GetType(String))
                        tabla.Columns.Add("ruta_archivo", GetType(String))
                        For index As Integer = 0 To gvAdjuntosComp.Rows.Count - 1
                            Dim Row1 As DataRow = tabla.NewRow
                            Row1("ruta") = gvAdjuntosComp.Rows(index).Cells(3).Text
                            Row1("nombre") = gvAdjuntosComp.Rows(index).Cells(2).Text
                            Row1("nombre_archivo") = gvAdjuntosComp.Rows(index).Cells(2).Text
                            Row1("ruta_archivo") = gvAdjuntosComp.Rows(index).Cells(3).Text
                            tabla.Rows.Add(Row1)
                        Next

                        Dim Row As DataRow = tabla.NewRow
                        Row("nombre") = sFileNameAr
                        Row("ruta") = "http://148.223.153.43/ProcAd/Evidencias MovLib/"
                        'Row("ruta") = "http://172.16.18.239/Evidencias MovLib/"
                        Row("nombre_archivo") = sFileNameAr
                        Row("ruta_archivo") = "http://148.223.153.43/ProcAd/Evidencias MovLib/"

                        tabla.Rows.Add(Row)
                        gvAdjuntosComp.DataSource = tabla
                        gvAdjuntosComp.DataBind()

                    Else
                        Dim tabla As DataTable = New DataTable
                        tabla.Columns.Add("nombre", GetType(String))
                        tabla.Columns.Add("ruta", GetType(String))
                        tabla.Columns.Add("nombre_archivo", GetType(String))
                        tabla.Columns.Add("ruta_archivo", GetType(String))

                        Dim Row1 As DataRow = tabla.NewRow
                        Row1("nombre") = sFileNameAr
                        Row1("ruta") = "http://148.223.153.43/ProcAd/Evidencias MovLib/"
                        Row1("nombre_archivo") = sFileNameAr
                        Row1("ruta_archivo") = "http://148.223.153.43/ProcAd/Evidencias MovLib/"

                        tabla.Rows.Add(Row1)
                        gvAdjuntosComp.DataSource = tabla
                        gvAdjuntosComp.DataBind()
                    End If

                    gvAdjuntosComp.Columns(2).Visible = False
                    gvAdjuntosComp.Columns(3).Visible = False
                End If

            Catch ex As Exception
                litError.Text = ex.Message
            End Try
        End With
    End Sub

    Protected Sub ddlCC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCC.SelectedIndexChanged
        If Me.ddlCC.SelectedValue <> 0 Then
            Me.ddlDiv.SelectedIndex = -1
            Me.upDiv.Update()
            ' configServ()
        End If
    End Sub

    Protected Sub ddlDiv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDiv.SelectedIndexChanged
        If Me.ddlDiv.SelectedValue <> 0 Then
            Me.ddlCC.SelectedIndex = -1
            Me.upCC.Update()
            '   configServ()
        End If
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Try
            litError.Text = ""
            If ddlCC.SelectedIndex = 0 And ddlDiv.SelectedIndex = 0 Then
                litError.Text = "Favor de asignar un centro de costo o división"
            ElseIf txtEspecificaciones.Text = "" Then
                litError.Text = "Hace falta poner las especificaciones"
            ElseIf gvAdjuntosComp.Rows.Count() = 0 Then
                litError.Text = "Favor de agregar por lo menos un archivo adjunto"
            Else
                pnlInicio.Enabled = False
                txtEspecificaciones.Enabled = False
                fuAdjunto.Enabled = False
                btnAgregarAdj.Visible = False
                btnAceptar.Enabled = False

                'Proceso normal'
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                Dim idMsMov As Integer = 0
                SCMValores.Connection = ConexionBD
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "insert into ms_movimientos_internos(id_usr_solicita, fecha_solicita, id_usr_autoriza, empresa, centro_costo, division, admon_oper, tipoM, especificaciones, estatus) " +
                                                                    "values (@id_usr_solicita, @fecha_solicita, @id_usr_autoriza, @empresa, @centro_costo, @division, @admon_oper, @tipoM, @especificaciones, 'P'); SELECT id_ms_movimientos_internos FROM ms_movimientos_internos where id_ms_movimientos_internos = SCOPE_IDENTITY(); "
                SCMValores.Parameters.AddWithValue("@id_usr_solicita", Val(Me._txtIdUsuario.Text))
                SCMValores.Parameters.AddWithValue("@fecha_solicita", Date.Now)
                SCMValores.Parameters.AddWithValue("@id_usr_autoriza", ddlAutorizador.SelectedValue)
                SCMValores.Parameters.AddWithValue("@empresa", ddlEmpresa.SelectedItem.Text)
                SCMValores.Parameters.AddWithValue("@centro_costo", ddlCC.SelectedItem.Text)
                SCMValores.Parameters.AddWithValue("@division", ddlDiv.SelectedItem.Text)
                SCMValores.Parameters.AddWithValue("@admon_oper", rblAdmonOper.SelectedValue)
                SCMValores.Parameters.AddWithValue("@tipoM", ddlTipo.SelectedValue)
                SCMValores.Parameters.AddWithValue("@especificaciones", txtEspecificaciones.Text)
                ConexionBD.Open()
                idMsMov = SCMValores.ExecuteScalar()
                ConexionBD.Close()
                'Obtener el id de la tabla maestra '

                gvAdjuntosComp.Columns(2).Visible = True
                gvAdjuntosComp.Columns(3).Visible = True

                'Insertar Evidencias'
                For index As Integer = 0 To gvAdjuntosComp.Rows.Count - 1

                    Dim archivo As String
                    archivo = Dir(Server.MapPath("Evidencias MovLib\" + _txtIdUsuario.Text.ToString() + "-" + gvAdjuntosComp.Rows(index).Cells(2).Text.ToString))
                    If archivo <> "" Then
                        My.Computer.FileSystem.RenameFile(Server.MapPath("Evidencias MovLib\" + _txtIdUsuario.Text.ToString() + "-" + gvAdjuntosComp.Rows(index).Cells(2).Text.ToString), CStr(idMsMov) + "-" + gvAdjuntosComp.Rows(index).Cells(2).Text)

                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "Insert into dt_archivo_movInt (id_ms_movimientos_internos, nombre, fecha, estatus) " +
                                        "                     values (@id_ms_movimientos_internos, @nombre,@fecha, 'A') "
                        SCMValores.Parameters.AddWithValue("@id_ms_movimientos_internos", Val(idMsMov))
                        SCMValores.Parameters.AddWithValue("@nombre", gvAdjuntosComp.Rows(index).Cells(2).Text)
                        SCMValores.Parameters.AddWithValue("@fecha", Date.Now)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                    Else

                    End If

                Next

                'Insertar Instancia de Solicitud de Liberación
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "insert into ms_instancia ( id_ms_sol,  tipo,  id_actividad) " +
                                                         "				    values (@id_ms_sol, @tipo, @id_actividad) "
                SCMValores.Parameters.AddWithValue("@id_ms_sol", Val(idMsMov))
                SCMValores.Parameters.AddWithValue("@tipo", "MI")
                SCMValores.Parameters.AddWithValue("@id_actividad", 124)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()
                'Obtener ID de la Instancia de Solicitud 
                Dim idMsInst As Integer
                SCMValores.CommandText = "select max(id_ms_instancia) from ms_instancia where id_ms_sol = @id_ms_sol and tipo = 'MI' "
                ConexionBD.Open()
                idMsInst = SCMValores.ExecuteScalar
                ConexionBD.Close()
                'Insertar Históricos
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "insert into ms_historico ( id_ms_instancia,  id_actividad,  id_usr,  fecha) " +
                                                         "				    values (@id_ms_instancia, @id_actividad, @id_usr, @fecha) "
                SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(idMsInst))
                SCMValores.Parameters.AddWithValue("@id_actividad", 124)
                SCMValores.Parameters.AddWithValue("@id_usr", Val(_txtIdUsuario.Text))
                SCMValores.Parameters.AddWithValue("@fecha", Date.Now)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()


                gvAdjuntosComp.Columns(2).Visible = False
                gvAdjuntosComp.Columns(3).Visible = False

                lblFolio.Text = idMsMov.ToString

            End If
        Catch ex As Exception
            btnAceptar.Enabled = False
            litError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlEmpresa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEmpresa.SelectedIndexChanged
        With Me
            Try

                .litError.Text = ""
                .ddlDiv.Items.Clear()
                .ddlCC.Items.Clear()
                llenarDiv()
                llenarCC()


            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub
#End Region


End Class