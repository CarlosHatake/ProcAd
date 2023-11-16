Public Class _93
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 1

                    ' Lic Marco 45 
                    ' Luis 1359  
                    ' Ing Ruben 14

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
                        sdaEmpresa.SelectCommand = New SqlCommand("select 0 as id_empresa, '' as nombre " + _
                                                                  "union " + _
                                                                  "select id_empresa, nombre " + _
                                                                  "from bd_empleado.dbo.cg_empresa Empresa " + _
                                                                  "where status = 'A' " + _
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

                        actualizarNegociacion(0)

                        'Eliminar registro de Cantidades no almacenadas previamente
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "DELETE from dt_factura_sn where id_ms_factura = -1 and id_usuario = @id_usuario"
                        SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Eliminar registro de Adjuntos no almacenados previamente
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "DELETE from dt_archivo where id_ms_factura = -1 and id_usuario = @id_usuario"
                        SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        .btnGuardar.Enabled = True
                    Else
                        Server.Transfer("Login.aspx")
                    End If
                Catch ex As Exception
                    .litError.Text = ex.ToString
                End Try
            End With
        End If
    End Sub

    Protected Sub btnBuscarNS_Click(sender As Object, e As EventArgs) Handles btnBuscarNS.Click
        actualizarNegociacion(Val(Me.txtFolioNS.Text))
    End Sub

    Protected Sub btnAgregarAdj_Click(sender As Object, e As EventArgs) Handles btnAgregarAdj.Click
        With Me
            Try
                .litError.Text = ""

                ' '' Ruta Local
                ''Dim sFileDir As String = "C:/ProcAd - Adjuntos IngFact/" 'Ruta en que se almacenará el archivo
                ' Ruta en Atenea
                Dim sFileDir As String = "D:\ProcAd - Adjuntos IngFact\" 'Ruta en que se almacenará el archivo
                Dim lMaxFileSize As Long = 10485760 'Equivalente a 10 Megabytes

                'Verificar que el archivo ha sido seleccionado y es un archivo válido
                If (Not fuAdjunto.PostedFile Is Nothing) And (fuAdjunto.PostedFile.ContentLength > 0) Then
                    'Determinar el nombre original del archivo
                    Dim sFileName As String = System.IO.Path.GetFileName(fuAdjunto.PostedFile.FileName)
                    Dim idArchivo As Integer 'Index correspondiente al archivo
                    Dim fecha As DateTime = Date.Now
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    Try
                        If fuAdjunto.PostedFile.ContentLength <= lMaxFileSize Then
                            'Registrar el archivo en la base de datos
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "INSERT INTO dt_archivo(id_ms_factura, id_actividad, id_usuario, nombre, fecha, tipo) values(-1, 43, @id_usuario, @nombre, @fecha, @tipo)"
                            SCMValores.Parameters.AddWithValue("@id_usuario", Val(Me._txtIdUsuario.Text))
                            SCMValores.Parameters.AddWithValue("@nombre", sFileName)
                            SCMValores.Parameters.AddWithValue("@fecha", fecha)
                            SCMValores.Parameters.AddWithValue("@tipo", .ddlTipoArchivo.SelectedValue)
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()
                            'Obtener el Id del archivo
                            SCMValores.CommandText = "select max(id_dt_archivo) from dt_archivo where (id_ms_factura = -1) and (fecha = @fecha)"
                            ConexionBD.Open()
                            idArchivo = SCMValores.ExecuteScalar
                            ConexionBD.Close()
                            'Se agrega el Id al nombre del archivo
                            sFileName = idArchivo.ToString + "-" + sFileName
                            'Almacenar el archivo en la ruta especificada
                            fuAdjunto.PostedFile.SaveAs(sFileDir + sFileName)
                            'lblMessage.Visible = True
                            'lblMessage.Text = "El archivo: " + sFileName + " se ha adjuntado exitosamente"

                            actualizarAdjuntos()
                        Else
                            'Rechazar el archivo
                            lblMessage.Visible = True
                            lblMessage.Text = "El archivo excede el límite de 10 MB"
                        End If
                    Catch exc As Exception    'En caso de error
                        'Eliminar el archivo en la base de datos
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "delete from dt_archivo where id_archivo = @idArchivo"
                        SCMValores.Parameters.AddWithValue("@idArchivo", idArchivo)
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        lblMessage.Visible = True
                        lblMessage.Text = lblMessage.Text + ". Un Error ha ocurrido. Favor de intentarlo nuevamente"
                        DeleteFile(sFileDir + sFileName)
                    End Try
                Else
                    lblMessage.Visible = True
                    lblMessage.Text = "Favor de seleccionar un Archivo"
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Funciones"

    Public Sub actualizarNegociacion(ByVal folio)
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")

                'Validar que el Folio de Negociación sea válido
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim contNS As Integer = 0
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select count(*) " + _
                                         "from ms_negoc_servicio " + _
                                         "where id_ms_negoc_servicio = @id_ms_negoc_servicio " + _
                                         "  and status = 'NeA' " + _
                                         "  and fecha_ini <= getdate() " + _
                                         "  and fecha_fin >= getdate() "
                SCMValores.Parameters.AddWithValue("@id_ms_negoc_servicio", folio)
                ConexionBD.Open()
                contNS = SCMValores.ExecuteScalar()
                ConexionBD.Close()

                If contNS = 0 Then
                    .litError.Text = "Folio Inválido, favor de verificarlo"

                    'Limpiar Campos
                    .ddlEmpresa.SelectedValue = 0
                    .ddlEmpresa.Enabled = False
                    .rblDimension.SelectedIndex = -1
                    .rblDimension.Enabled = False
                    .lbl_Dimension.Text = ""
                    .ddlDimension.Items.Clear()
                    .lblServicio.Text = ""
                    ._txtIdDtServ.Text = ""
                    .ddlBase.Items.Clear()
                    .txtLugar.Text = ""
                    ._txtCotizaciones.Text = ""
                    ._txtCotUnica.Text = ""
                    ._txtContrato.Text = ""
                    ._txtServNeg.Text = ""
                    ._txtCuentaCont.Text = ""
                    ._txtIdValidador.Text = ""
                    ._txtIdValidador2.Text = ""
                    ._txtFinanzas.Text = ""
                    ._txtValPresup.Text = ""

                    'Validador 1
                    actualizarValAut(-99, "V1", .lbl_Validador1, .ddlValidador1)
                    'Validador 2
                    actualizarValAut(-99, "V2", .lbl_Validador2, .ddlValidador2)

                Else
                    Dim sdaSol As New SqlDataAdapter
                    Dim dsSol As New DataSet
                    Dim query As String
                    query = "select ms_negoc_servicio.empresa " + _
                            "     , isnull(ms_negoc_servicio.division, ms_negoc_servicio.centro_costo) as centro_costo " + _
                            "     , case when ms_negoc_servicio.division is null then 'CC' else 'DIV' end as dimension " + _
                            "     , ms_negoc_servicio.servicio " + _
                            "     , ms_negoc_servicio.id_dt_servicio_conf " + _
                            "     , ms_negoc_servicio.base " + _
                            "     , ms_negoc_servicio.lugar_ejecucion " + _
                            "     , cg_servicio.cotizaciones " + _
                            "     , cg_servicio.cotizacion_unica " + _
                            "     , cg_servicio.contrato " + _
                            "     , cg_servicio.servicio_negociado " + _
                            "     , dt_servicio_conf.cuenta_cont " + _
                            "     , isnull(id_usr_valida, -99) as id_usr_valida1 " + _
                            "     , isnull(id_usr_valida2, -99) as id_usr_valida2 " + _
                            "     , finanzas " + _
                            "     , valida_presup " + _
                            "     , cg_empresa.id_empresa " + _
                            "     , case when ms_negoc_servicio.division is null then (select cg_cc.id_cc from bd_Empleado.dbo.cg_cc where cg_cc.nombre = ms_negoc_servicio.centro_costo and cg_cc.id_empresa = cg_empresa.id_empresa) else (select cg_div.id_div from bd_Empleado.dbo.cg_div where cg_div.nombre = ms_negoc_servicio.division and cg_div.id_empresa = cg_empresa.id_empresa) end as id_division " + _
                            "     , ms_negoc_servicio.admon_oper " + _
                            "     , ms_negoc_servicio.proveedor_selec " + _
                            "     , cg_proveedor.rfc " + _
                            "     , cg_servicio.id_servicio " + _
                            "     , cg_base.id_base " + _
                            "from ms_negoc_servicio " + _
                            "  left join dt_servicio_conf on ms_negoc_servicio.id_dt_servicio_conf = dt_servicio_conf.id_dt_servicio_conf " + _
                            "  left join cg_servicio on dt_servicio_conf.id_servicio = cg_servicio.id_servicio " + _
                            "  left join bd_Empleado.dbo.cg_empresa on ms_negoc_servicio.empresa = cg_empresa.nombre " + _
                            "  left join bd_SiSAC.dbo.cg_proveedor on ms_negoc_servicio.proveedor_selec = cg_proveedor.id_proveedor " + _
                            "  left join cg_base on cg_base.id_empresa = cg_empresa.id_empresa and ms_negoc_servicio.base = cg_base.base " + _
                            "where id_ms_negoc_servicio = @id_ms_negoc_servicio " + _
                            "  and ms_negoc_servicio.status = 'NeA' " + _
                            "  and fecha_ini <= getdate() " + _
                            "  and fecha_fin >= getdate() "
                    sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                    sdaSol.SelectCommand.Parameters.AddWithValue("@id_ms_negoc_servicio", folio)
                    ConexionBD.Open()
                    sdaSol.Fill(dsSol)
                    ConexionBD.Close()
                    .rblDimension.SelectedValue = dsSol.Tables(0).Rows(0).Item("dimension").ToString()
                    .rblDimension.Enabled = True
                    If .rblDimension.SelectedValue = "CC" Then
                        .lbl_Dimension.Text = "Centro de Costo:"
                    Else
                        .lbl_Dimension.Text = "División:"
                    End If
                    .ddlEmpresa.SelectedValue = Val(dsSol.Tables(0).Rows(0).Item("id_empresa").ToString())
                    .ddlEmpresa.Enabled = True
                    'Catálogo de Bases
                    llenarBase()
                    'Dimensiones
                    actualizarDimension()
                    .ddlDimension.SelectedValue = Val(dsSol.Tables(0).Rows(0).Item("id_division").ToString())
                    .lblServicio.Text = dsSol.Tables(0).Rows(0).Item("servicio").ToString()
                    ._txtIdDtServ.Text = dsSol.Tables(0).Rows(0).Item("id_dt_servicio_conf").ToString()
                    .ddlBase.SelectedValue = Val(dsSol.Tables(0).Rows(0).Item("id_base").ToString())
                    .txtLugar.Text = dsSol.Tables(0).Rows(0).Item("lugar_ejecucion").ToString()
                    ._txtCotizaciones.Text = dsSol.Tables(0).Rows(0).Item("cotizaciones").ToString()
                    ._txtCotUnica.Text = dsSol.Tables(0).Rows(0).Item("cotizacion_unica").ToString()
                    ._txtContrato.Text = dsSol.Tables(0).Rows(0).Item("contrato").ToString()
                    ._txtServNeg.Text = dsSol.Tables(0).Rows(0).Item("servicio_negociado").ToString()
                    ._txtCuentaCont.Text = dsSol.Tables(0).Rows(0).Item("cuenta_cont").ToString()
                    ._txtIdValidador.Text = dsSol.Tables(0).Rows(0).Item("id_usr_valida1").ToString()
                    ._txtIdValidador2.Text = dsSol.Tables(0).Rows(0).Item("id_usr_valida2").ToString()
                    ._txtFinanzas.Text = dsSol.Tables(0).Rows(0).Item("finanzas").ToString()
                    ._txtValPresup.Text = dsSol.Tables(0).Rows(0).Item("valida_presup").ToString()
                    ._txtAdmonOper.Text = dsSol.Tables(0).Rows(0).Item("admon_oper").ToString()
                    ._txtIdProveedor.Text = dsSol.Tables(0).Rows(0).Item("proveedor_selec").ToString()
                    ._txtRFC.Text = dsSol.Tables(0).Rows(0).Item("rfc").ToString()
                    ._txtIdServicio.Text = dsSol.Tables(0).Rows(0).Item("id_servicio").ToString()
                    sdaSol.Dispose()
                    dsSol.Dispose()

                    'Adjuntos Requeridos
                    Dim sdaAdjReq As New SqlDataAdapter
                    Dim dsAdjReq As New DataSet
                    .gvAdjuntosReq.DataSource = dsAdjReq
                    sdaAdjReq.SelectCommand = New SqlCommand("select adjunto " + _
                                                             "from dt_servicio_adj " + _
                                                             "where id_servicio = @id_servicio ", ConexionBD)
                    sdaAdjReq.SelectCommand.Parameters.AddWithValue("@id_servicio", Val(._txtIdServicio.Text))
                    ConexionBD.Open()
                    sdaAdjReq.Fill(dsAdjReq)
                    .gvAdjuntosReq.DataBind()
                    ConexionBD.Close()
                    sdaAdjReq.Dispose()
                    dsAdjReq.Dispose()
                    .gvAdjuntosReq.SelectedIndex = -1
                    If .gvAdjuntosReq.Rows.Count = 0 Then
                        .lbl_AdjuntoReq.Visible = False
                        .gvAdjuntosReq.Visible = False
                    Else
                        .lbl_AdjuntoReq.Visible = True
                        .gvAdjuntosReq.Visible = True
                    End If

                    'Validador 1
                    actualizarValAut(Val(._txtIdValidador.Text), "V1", .lbl_Validador1, .ddlValidador1)
                    'Validador 2
                    If Val(._txtIdValidador2.Text) = -98 Then
                        'No Aplica
                        actualizarValAut(-99, "V2", .lbl_Validador2, .ddlValidador2)
                    Else
                        actualizarValAut(Val(._txtIdValidador2.Text), "V2", .lbl_Validador2, .ddlValidador2)
                    End If

                    actualizarDtFactura()
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub actualizarDimension()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaDim As New SqlDataAdapter
                Dim dsDim As New DataSet
                Dim query As String
                If .rblDimension.SelectedValue = "CC" Then
                    query = "select 0 as id_dim " + _
                            "     , ' ' as nombre " + _
                            "union " + _
                            "select id_cc as id_dim " + _
                            "     , nombre " + _
                            "from bd_Empleado.dbo.cg_cc " + _
                            "where id_empresa = @idEmpresa " + _
                            "  and status = 'A' " + _
                            "order by nombre "
                Else
                    query = "select 0 as id_dim " + _
                            "     , ' ' as nombre " + _
                            "union " + _
                            "select id_div as id_dim " + _
                            "     , nombre " + _
                            "from bd_Empleado.dbo.cg_div " + _
                            "where id_empresa = @idEmpresa " + _
                            "  and status = 'A' " + _
                            "order by nombre "
                End If

                sdaDim.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaDim.SelectCommand.Parameters.AddWithValue("@idEmpresa", .ddlEmpresa.SelectedValue)
                .ddlDimension.DataSource = dsDim
                .ddlDimension.DataTextField = "nombre"
                .ddlDimension.DataValueField = "id_dim"
                ConexionBD.Open()
                sdaDim.Fill(dsDim)
                .ddlDimension.DataBind()
                ConexionBD.Close()
                sdaDim.Dispose()
                dsDim.Dispose()
                .ddlDimension.SelectedIndex = -1
                .upDimension.Update()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarBase()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaBase As New SqlDataAdapter
                Dim dsBase As New DataSet
                sdaBase.SelectCommand = New SqlCommand("select id_base " + _
                                                       "     , base " + _
                                                       "from cg_base " + _
                                                       "where status = 'A' " + _
                                                       "  and id_empresa = @id_empresa " + _
                                                       "order by base ", ConexionBD)
                sdaBase.SelectCommand.Parameters.AddWithValue("@id_empresa", .ddlEmpresa.SelectedValue)
                .ddlBase.DataSource = dsBase
                .ddlBase.DataTextField = "base"
                .ddlBase.DataValueField = "id_base"
                ConexionBD.Open()
                sdaBase.Fill(dsBase)
                .ddlBase.DataBind()
                ConexionBD.Close()
                sdaBase.Dispose()
                dsBase.Dispose()
                .upBase.Update()
            Catch ex As Exception

            End Try
        End With
    End Sub

    Public Sub actualizarValAut(ByVal idUsr, ByVal tipo, ByRef etiqueta, ByRef lista)
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                If idUsr = -99 Then
                    'No aplica
                    etiqueta.Visible = False
                    'upEtiqueta.Update()
                    lista.Visible = False
                    lista.Items.Clear()
                    'upLista.Update()
                Else
                    'Sí aplica
                    Dim sdaValAut As New SqlDataAdapter
                    Dim dsValAut As New DataSet
                    etiqueta.Visible = True
                    'upEtiqueta.Update()
                    lista.Visible = True
                    lista.DataSource = dsValAut
                    Select Case idUsr
                        Case -7
                            ' Director de Empresa
                            sdaValAut.SelectCommand = New SqlCommand("select isnull(id_usr_director, 0) as id_usuario " + _
                                                                     "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                     "from bd_Empleado.dbo.cg_empresa " + _
                                                                     "  left join cg_usuario on cg_empresa.id_usr_director = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                                                                     "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                                                                     "where id_empresa = @idEmpresa " + _
                                                                     "  and cg_empresa.status = 'A' ", ConexionBD)
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@idEmpresa", .ddlEmpresa.SelectedValue)
                        Case -6
                            ' Gerente de Empresa
                            sdaValAut.SelectCommand = New SqlCommand("select isnull(id_usr_gerente, 0) as id_usuario " + _
                                                                     "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                     "from bd_Empleado.dbo.cg_empresa " + _
                                                                     "  left join cg_usuario on cg_empresa.id_usr_gerente = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                                                                     "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                                                                     "where id_empresa = @idEmpresa " + _
                                                                     "  and cg_empresa.status = 'A' ", ConexionBD)
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@idEmpresa", .ddlEmpresa.SelectedValue)
                        Case -5
                            ' Director División / CC
                            If .lbl_Dimension.Text = "División:" Then
                                'División
                                sdaValAut.SelectCommand = New SqlCommand("select isnull(id_usr_director, 0) as id_usuario " + _
                                                                         "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                         "from bd_Empleado.dbo.cg_div " + _
                                                                         "  left join cg_usuario on cg_div.id_usr_director = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                                                                         "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                                                                         "where cg_div.id_div = @id_ccDiv " + _
                                                                         "  and cg_div.status = 'A' ", ConexionBD)
                                sdaValAut.SelectCommand.Parameters.AddWithValue("@id_ccDiv", .ddlDimension.SelectedValue)
                            Else
                                'Centro de Costo
                                sdaValAut.SelectCommand = New SqlCommand("select isnull(id_usr_director, 0) as id_usuario " + _
                                                                         "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                         "from bd_Empleado.dbo.cg_cc " + _
                                                                         "  left join cg_usuario on cg_cc.id_usr_director = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                                                                         "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                                                                         "where cg_cc.id_cc = @id_ccDiv " + _
                                                                         "  and cg_cc.status = 'A' ", ConexionBD)
                                sdaValAut.SelectCommand.Parameters.AddWithValue("@id_ccDiv", .ddlDimension.SelectedValue)
                            End If
                        Case -4
                            ' Gerente División / CC
                            If .lbl_Dimension.Text = "División:" Then
                                'División
                                sdaValAut.SelectCommand = New SqlCommand("select isnull(id_usr_gerente, 0) as id_usuario " + _
                                                                         "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                         "from bd_Empleado.dbo.cg_div " + _
                                                                         "  left join cg_usuario on cg_div.id_usr_gerente = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                                                                         "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                                                                         "where cg_div.id_div = @id_ccDiv " + _
                                                                         "  and cg_div.status = 'A' ", ConexionBD)
                                sdaValAut.SelectCommand.Parameters.AddWithValue("@id_ccDiv", .ddlDimension.SelectedValue)
                            Else
                                'Centro de Costo
                                sdaValAut.SelectCommand = New SqlCommand("select isnull(id_usr_gerente, 0) as id_usuario " + _
                                                                         "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                         "from bd_Empleado.dbo.cg_cc " + _
                                                                         "  left join cg_usuario on cg_cc.id_usr_gerente = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                                                                         "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                                                                         "where cg_cc.id_cc = @id_ccDiv " + _
                                                                         "  and cg_cc.status = 'A' ", ConexionBD)
                                sdaValAut.SelectCommand.Parameters.AddWithValue("@id_ccDiv", .ddlDimension.SelectedValue)
                            End If
                        Case -3
                            ' Director Organigrama
                            sdaValAut.SelectCommand = New SqlCommand("select cgUsrA.id_usuario as id_usuario " + _
                                                                     "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                     "from cg_usuario cgUsr " + _
                                                                     "  left join dt_autorizador on cgUsr.id_usuario = dt_autorizador.id_usuario " + _
                                                                     "  left join cg_usuario cgUsrA on dt_autorizador.id_autorizador = cgUsrA.id_usuario and cgUsrA.status = 'A' " + _
                                                                     "  left join bd_Empleado.dbo.cg_empleado on cgUsrA.id_empleado = cg_empleado.id_empleado " + _
                                                                     "where cgUsr.id_usuario = @idUsuario " + _
                                                                     "  and dt_autorizador.aut_dir = 'S' ", ConexionBD)
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        Case -2
                            ' Gerente Organigrama
                            sdaValAut.SelectCommand = New SqlCommand("select cgUsrA.id_usuario as id_usuario " + _
                                                                     "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                     "from cg_usuario cgUsr " + _
                                                                     "  left join dt_autorizador on cgUsr.id_usuario = dt_autorizador.id_usuario " + _
                                                                     "  left join cg_usuario cgUsrA on dt_autorizador.id_autorizador = cgUsrA.id_usuario and cgUsrA.status = 'A' " + _
                                                                     "  left join bd_Empleado.dbo.cg_empleado on cgUsrA.id_empleado = cg_empleado.id_empleado " + _
                                                                     "where cgUsr.id_usuario = @idUsuario " + _
                                                                     "  and dt_autorizador.aut_dir = 'N' ", ConexionBD)
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        Case -1
                            ' x Organigrama
                            sdaValAut.SelectCommand = New SqlCommand("select cgUsrA.id_usuario as id_usuario " + _
                                                                     "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                     "from cg_usuario cgUsr " + _
                                                                     "  left join dt_autorizador on cgUsr.id_usuario = dt_autorizador.id_usuario " + _
                                                                     "  left join cg_usuario cgUsrA on dt_autorizador.id_autorizador = cgUsrA.id_usuario and cgUsrA.status = 'A' " + _
                                                                     "  left join bd_Empleado.dbo.cg_empleado on cgUsrA.id_empleado = cg_empleado.id_empleado " + _
                                                                     "where cgUsr.id_usuario = @idUsuario ", ConexionBD)
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        Case 0
                            ' Lista Pre-definida
                            sdaValAut.SelectCommand = New SqlCommand("select cg_usuario.id_usuario as id_usuario " + _
                                                                     "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                     "from dt_servicio_aut " + _
                                                                     "  left join cg_usuario on dt_servicio_aut.id_usuario = cg_usuario.id_usuario and cg_usuario.status = 'A' " + _
                                                                     "  left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " + _
                                                                     "where id_dt_servicio_conf = @idDtServicio " + _
                                                                     "  and tipo = @tipo ", ConexionBD)
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@idDtServicio", Val(._txtIdDtServ.Text))
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@tipo", tipo)
                        Case Else
                            ' Usuario Específico
                            sdaValAut.SelectCommand = New SqlCommand("select cgUsrA.id_usuario as id_usuario " + _
                                                                     "     , cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + cg_empleado.ap_materno as empleado " + _
                                                                     "from cg_usuario cgUsrA " + _
                                                                     "  left join bd_Empleado.dbo.cg_empleado on cgUsrA.id_empleado = cg_empleado.id_empleado " + _
                                                                     "where cgUsrA.id_usuario = @idValAut " + _
                                                                     "  and cgUsrA.status = 'A' ", ConexionBD)
                            sdaValAut.SelectCommand.Parameters.AddWithValue("@idValAut", idUsr)
                    End Select
                    lista.DataTextField = "empleado"
                    lista.DataValueField = "id_usuario"
                    ConexionBD.Open()
                    sdaValAut.Fill(dsValAut)
                    lista.DataBind()
                    ConexionBD.Close()
                    sdaValAut.Dispose()
                    dsValAut.Dispose()
                    lista.SelectedIndex = -1
                    'upLista.Update()
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub buscarNoEco()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBDNAV As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBDNAV.ConnectionString = accessDB.conBD("NAV")
                Dim sdaNoEco As New SqlDataAdapter
                Dim dsNoEco As New DataSet
                Dim query As String = "select [No_] as id " + _
                                      "     , [Núm_ Economico] + ' - ' + [Empresa que administra] + ' - ' + [Num_ Placas] as no_eco " + _
                                      "from [TRACSA$Standard Units] " + _
                                      "where [Núm_ Economico] like '%' + @noEco + '%' " + _
                                      "  and [Núm_ Economico] <> '' " + _
                                      "  and [Status] <> 2 "
                ' TR
                query = query + "  and ( [Tipo Unidad] in (0, 4) "
                ' TQ
                query = query + "  or [Tipo Unidad] in (1) "
                ' DL
                query = query + "  or [Tipo Unidad] in (2) )"

                sdaNoEco.SelectCommand = New SqlCommand(query, ConexionBDNAV)
                sdaNoEco.SelectCommand.Parameters.AddWithValue("@noEco", "%" + .txtUnidad.Text.ToUpper + "%")
                .ddlUnidad.DataSource = dsNoEco
                .ddlUnidad.DataTextField = "no_eco"
                .ddlUnidad.DataValueField = "id"
                ConexionBDNAV.Open()
                sdaNoEco.Fill(dsNoEco)
                .ddlUnidad.DataBind()
                ConexionBDNAV.Close()
                sdaNoEco.Dispose()
                dsNoEco.Dispose()
                .ddlUnidad.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub actualizarDtFactura()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .gvDtFacturaSN.DataSource = dsCatalogo
                'Habilitar columna para actualización
                .gvDtFacturaSN.Columns(0).Visible = True
                'Catálogo de Unidades agregados
                sdaCatalogo.SelectCommand = New SqlCommand("select id_dt_factura_sn " + _
                                                           "     , cantidad " + _
                                                           "     , empresa " + _
                                                           "     , no_economico " + _
                                                           "     , tipo " + _
                                                           "     , modelo " + _
                                                           "     , placas " + _
                                                           "     , div " + _
                                                           "     , division " + _
                                                           "     , zn " + _
                                                           "from dt_factura_sn " + _
                                                           "where id_ms_factura = -1 " + _
                                                           "  and id_usuario = @idUsuario ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvDtFacturaSN.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvDtFacturaSN.SelectedIndex = -1
                'Inhabilitar columna para vista
                .gvDtFacturaSN.Columns(0).Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub actualizarAdjuntos()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaArchivos As New SqlDataAdapter
                Dim dsArchivos As New DataSet
                .gvAdjuntos.DataSource = dsArchivos
                'Adjuntos
                sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " + _
                                                           "     , 'http://148.223.153.43/ProcAd - Adjuntos IngFact/' + cast(id_dt_archivo as varchar(20)) + '-' + nombre as path " + _
                                                           "from dt_archivo " + _
                                                           "where id_ms_factura = -1 " + _
                                                           "  and tipo = 'A' " + _
                                                           "  and id_usuario = @idUsuario ", ConexionBD)
                sdaArchivos.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                sdaArchivos.Fill(dsArchivos)
                .gvAdjuntos.DataBind()
                ConexionBD.Close()
                sdaArchivos.Dispose()
                dsArchivos.Dispose()
                .gvAdjuntos.SelectedIndex = -1
                .upAdjuntos.Update()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Private Sub DeleteFile(ByVal strFileName As String)
        If strFileName.Trim().Length > 0 Then
            Dim fi As New FileInfo(strFileName)
            If (fi.Exists) Then    'Si existe el archivo, eliminarlo
                fi.Delete()
            End If
        End If
    End Sub

#End Region

#Region "Unidades"

    Protected Sub cmdBuscarU_Click(sender As Object, e As EventArgs) Handles cmdBuscarU.Click
        buscarNoEco()
    End Sub

    Protected Sub cmdAgregarU_Click(sender As Object, e As EventArgs) Handles cmdAgregarU.Click
        With Me
            Try
                If .wneCantidad.Text = "" Then
                    .litError.Text = "Favor de indicar la cantidad"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    Dim contMod As Decimal
                    SCMValores.Parameters.Clear()
                    'SCMValores.CommandText = "select cast(@cantidad as decimal(18,4)) % cast(ms_factura.cantidad as decimal(18,4)) " + _
                    '                         "from ms_factura " + _
                    '                         "where ms_factura.id_ms_factura = @id_ms_factura "
                    SCMValores.CommandText = "select cast(@cantidad as decimal(18,4)) % cast(ms_negoc_servicio.cantidad as decimal(18,4)) " + _
                                             "from ms_negoc_servicio " + _
                                             "where ms_negoc_servicio.id_ms_negoc_servicio = @id_ms_negoc_servicio "
                    SCMValores.Parameters.AddWithValue("@id_ms_negoc_servicio", Val(Me.txtFolioNS.Text))
                    SCMValores.Parameters.AddWithValue("@cantidad", .wneCantidad.Value)
                    ConexionBD.Open()
                    contMod = SCMValores.ExecuteScalar
                    ConexionBD.Close()

                    If contMod <> 0 Then
                        .litError.Text = "Cantidad Inválida, esta debe ser múltiplo de la cantidad autorizada en la negociación"
                    Else
                        If .ddlUnidad.Items.Count <> 0 Then
                            Dim ConexionBDNAV As SqlConnection = New System.Data.SqlClient.SqlConnection
                            ConexionBDNAV.ConnectionString = accessDB.conBD("NAV")
                            'Se incluye Unidad
                            Dim SCMValoresNAV As SqlCommand = New System.Data.SqlClient.SqlCommand
                            Dim empresa As String
                            Dim noEconomico As String
                            SCMValoresNAV.Connection = ConexionBDNAV
                            SCMValoresNAV.CommandText = ""
                            SCMValoresNAV.Parameters.Clear()
                            'Obtener Empresa que administra de la Unidad en NAV
                            SCMValoresNAV.CommandText = "select [Empresa que administra] " + _
                                                        "from [TRACSA$Standard Units] " + _
                                                        "where [No_] = @No_ "
                            SCMValoresNAV.Parameters.AddWithValue("@No_", .ddlUnidad.SelectedValue)
                            ConexionBDNAV.Open()
                            empresa = SCMValoresNAV.ExecuteScalar
                            ConexionBDNAV.Close()
                            'Obtener Número económico de la Unidad en NAV
                            SCMValoresNAV.CommandText = "select [Núm_ Economico] " + _
                                                        "from [TRACSA$Standard Units] " + _
                                                        "where [No_] = @No_ "
                            ConexionBDNAV.Open()
                            noEconomico = SCMValoresNAV.ExecuteScalar
                            ConexionBDNAV.Close()

                            'Obtener datos de la Unidad
                            Dim sdaUnidad As New SqlDataAdapter
                            Dim dsUnidad As New DataSet
                            sdaUnidad.SelectCommand = New SqlCommand("select StandardUnits.[Empresa que administra] as empresa " + _
                                                                     "     , StandardUnits.No_ as codigo " + _
                                                                     "     , StandardUnits.Description as descripcion " + _
                                                                     "     , StandardUnits.[Núm_ Economico] as no_economico " + _
                                                                     "     , case StandardUnits.Status " + _
                                                                     "         when 0 then 'Patio' " + _
                                                                     "         when 1 then 'Activo' " + _
                                                                     "         when 2 then 'Baja' " + _
                                                                     "       end as status " + _
                                                                     "     , case StandardUnits.[Tipo Unidad] " + _
                                                                     "         when 0 then 'Tractocamion' " + _
                                                                     "         when 1 then 'Remolque' " + _
                                                                     "         when 2 then 'Dolly' " + _
                                                                     "         when 3 then 'Maquinaria' " + _
                                                                     "         when 4 then 'Torton' " + _
                                                                     "       end as tipo " + _
                                                                     "     , StandardUnits.[Num_ Serie Chasis] as no_serie " + _
                                                                     "     , StandardUnits.AñoModelo as modelo " + _
                                                                     "     , StandardUnits.Marca as marca " + _
                                                                     "     , StandardUnits.[Num_ Placas] as placas " + _
                                                                     "     , StandardUnits.Division as div " + _
                                                                     "     , DIVValue.Name as division " + _
                                                                     "     , StandardUnits.[Shortcut Dimension 3 Code] as zn " + _
                                                                     "     , ZNValue.Name as zona " + _
                                                                     "from [" + empresa + "$Standard Units] as StandardUnits " + _
                                                                     "  left join [" + empresa + "$Dimension Value] DIVValue on StandardUnits.Division = DIVValue.Code and DIVValue.[Dimension Code] = 'DIV' " + _
                                                                     "  left join [" + empresa + "$Dimension Value] ZNValue on StandardUnits.[Shortcut Dimension 3 Code] = ZNValue.Code and ZNValue.[Dimension Code] = 'ZN' " + _
                                                                     "where StandardUnits.Status <> 2 " + _
                                                                     "  and [Núm_ Economico] = @numEconomico ", ConexionBDNAV)
                            sdaUnidad.SelectCommand.Parameters.AddWithValue("@numEconomico", noEconomico)
                            ConexionBDNAV.Open()
                            sdaUnidad.Fill(dsUnidad)
                            ConexionBDNAV.Close()

                            'Insertar datos de Unidad en dt_factura_sn
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "insert into dt_factura_sn (id_ms_factura,  id_usuario,  cantidad,  empresa,  codigo,  descripcion,  no_economico,  status,  tipo,  no_serie,  modelo,  placas,  div,  division,  zn,  zona) " + _
                                                     "                   values (           -1, @id_usuario, @cantidad, @empresa, @codigo, @descripcion, @no_economico, @status, @tipo, @no_serie, @modelo, @placas, @div, @division, @zn, @zona)"
                            SCMValores.Parameters.AddWithValue("@id_usuario", Val(Me._txtIdUsuario.Text))
                            SCMValores.Parameters.AddWithValue("@cantidad", .wneCantidad.Value)
                            SCMValores.Parameters.AddWithValue("@empresa", dsUnidad.Tables(0).Rows(0).Item("empresa").ToString())
                            SCMValores.Parameters.AddWithValue("@codigo", dsUnidad.Tables(0).Rows(0).Item("codigo").ToString())
                            SCMValores.Parameters.AddWithValue("@descripcion", dsUnidad.Tables(0).Rows(0).Item("descripcion").ToString())
                            SCMValores.Parameters.AddWithValue("@no_economico", dsUnidad.Tables(0).Rows(0).Item("no_economico").ToString())
                            SCMValores.Parameters.AddWithValue("@status", dsUnidad.Tables(0).Rows(0).Item("status").ToString())
                            SCMValores.Parameters.AddWithValue("@tipo", dsUnidad.Tables(0).Rows(0).Item("tipo").ToString())
                            SCMValores.Parameters.AddWithValue("@no_serie", dsUnidad.Tables(0).Rows(0).Item("no_serie").ToString())
                            SCMValores.Parameters.AddWithValue("@modelo", dsUnidad.Tables(0).Rows(0).Item("modelo").ToString())
                            SCMValores.Parameters.AddWithValue("@placas", dsUnidad.Tables(0).Rows(0).Item("placas").ToString())
                            SCMValores.Parameters.AddWithValue("@div", dsUnidad.Tables(0).Rows(0).Item("div").ToString())
                            SCMValores.Parameters.AddWithValue("@division", dsUnidad.Tables(0).Rows(0).Item("division").ToString())
                            SCMValores.Parameters.AddWithValue("@zn", dsUnidad.Tables(0).Rows(0).Item("zn").ToString())
                            SCMValores.Parameters.AddWithValue("@zona", dsUnidad.Tables(0).Rows(0).Item("zona").ToString())
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()
                            sdaUnidad.Dispose()
                            dsUnidad.Dispose()

                            actualizarDtFactura()
                        Else
                            'Sin Unidad
                            'Insertar datos de Unidad en dt_factura_sn
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "insert into dt_factura_sn (id_ms_factura,  id_usuario,  cantidad) " + _
                                                     "                   values (           -1, @id_usuario, @cantidad)"
                            SCMValores.Parameters.AddWithValue("@id_usuario", Val(Me._txtIdUsuario.Text))
                            SCMValores.Parameters.AddWithValue("@cantidad", .wneCantidad.Value)
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            actualizarDtFactura()
                        End If
                    End If
                End If
            Catch exc As Exception
                .litError.Text = exc.ToString
            End Try
        End With
    End Sub

    Protected Sub gvDtFacturaSN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDtFacturaSN.SelectedIndexChanged
        With Me
            Try
                'Eliminar la Unidad
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD

                'Registrar la unidad en la base de datos
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "delete from dt_factura_sn where id_dt_factura_sn = @id_dt_factura_sn"
                SCMValores.Parameters.AddWithValue("@id_dt_factura_sn", .gvDtFacturaSN.SelectedRow.Cells(0).Text)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                actualizarDtFactura()
            Catch exc As Exception
                .litError.Text = exc.ToString
            End Try
        End With
    End Sub

#End Region

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        With Me
            Try
                .litError.Text = ""
                If Val(._txtBan.Text) = 1 Then
                    .litError.Text = "Ya se almacenó la factura previamente, favor de validarlo en el apartado de Consulta de Facturas"
                Else
                    Dim ban As Integer = 0
                    If .txtDescripcion.Text.Trim = "" Then
                        .litError.Text = "Favor de ingresar la descripción correspondiente"
                        ban = 1
                    End If
                    If .gvDtFacturaSN.Rows.Count = 0 Then
                        If ban = 1 Then
                            .litError.Text = .litError.Text + "; "
                        Else
                            ban = 1
                        End If
                        .litError.Text = .litError.Text + "Debe existir al menos un renglón con Cantidad"
                    End If
                    If .ddlEmpresa.SelectedValue = 0 Then
                        If ban = 1 Then
                            .litError.Text = .litError.Text + "; "
                        Else
                            ban = 1
                        End If
                        .litError.Text = .litError.Text + "Favor de seleccionar la Empresa"
                    End If
                    If .ddlDimension.SelectedValue = 0 Then
                        If ban = 1 Then
                            .litError.Text = .litError.Text + "; "
                        Else
                            ban = 1
                        End If
                        .litError.Text = .litError.Text + "Favor de seleccionar el Centro de Costo o División correspondiente"
                    End If

                    If ban = 0 Then
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD

                        While Val(._txtBan.Text) = 0
                            Dim fecha As DateTime
                            fecha = Date.Now
                            'Insertar Solicitud
                            Dim sdaEmpleado As New SqlDataAdapter
                            Dim dsEmpleado As New DataSet
                            sdaEmpleado.SelectCommand = New SqlCommand("select cgEmpl.no_empleado as no_empleadoE " + _
                                                                       "     , cgVal.no_empleado as no_empleadoV " + _
                                                                       "from bd_empleado.dbo.cg_empleado cgEmpl " + _
                                                                       "  left join cg_usuario cgUsrE on cgEmpl.id_empleado = cgUsrE.id_empleado " + _
                                                                       "  left join cg_usuario cgUsrV on cgUsrV.id_usuario = @idVal " + _
                                                                       "  left join bd_Empleado.dbo.cg_empleado cgVal on cgVal.id_empleado = cgUsrV.id_empleado " + _
                                                                       "where cgUsrE.id_usuario = @idEmpl ", ConexionBD)
                            sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idEmpl", Val(._txtIdUsuario.Text))
                            sdaEmpleado.SelectCommand.Parameters.AddWithValue("@idVal", .ddlValidador1.SelectedValue)
                            ConexionBD.Open()
                            sdaEmpleado.Fill(dsEmpleado)
                            ConexionBD.Close()

                            Dim sdaCantPrec As New SqlDataAdapter
                            Dim dsCantPrec As New DataSet
                            sdaCantPrec.SelectCommand = New SqlCommand("select cantidad, precio, id_divisa, tipo_cambio " + _
                                                                       "from ms_negoc_servicio " + _
                                                                       "where id_ms_negoc_servicio = @id_ms_negoc_servicio ", ConexionBD)
                            sdaCantPrec.SelectCommand.Parameters.AddWithValue("@id_ms_negoc_servicio", Val(.txtFolioNS.Text))
                            ConexionBD.Open()
                            sdaCantPrec.Fill(dsCantPrec)
                            ConexionBD.Close()

                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "insert into ms_factura ( id_usr_solicita,  fecha_solicita,  id_usr_autoriza,  id_usr_autoriza2,  id_usr_autoriza3,  empresa,  centro_costo,  division,  no_empleado,  empleado,  no_autorizador,  autorizador,  RFC,  CFDI,  importe_tot,  id_tipo_servicio,  tipo_servicio,  contrato_NAV_alta,  contrato_NAV_reg,  no_contrato_NAV,  no_validador,  validador,  especificaciones,  proveedor_selec,  cotizacion_selec,  lugar_ejecucion,  base,  id_usr_valida,  id_usr_valida2,  cValidadorSop,  id_dt_servicio_conf,  id_ms_negoc_servicio,  servicio_tipo,  servicio,  admon_oper,  cValidador,  cCompras,  cFinanzas,  cPresupuesto,  cuenta_cont,  cantidad,  precio,  id_divisa,  tipo_cambio,  status) " + _
                                                     " 			      values (@id_usr_solicita, @fecha_solicita, @id_usr_autoriza, @id_usr_autoriza2, @id_usr_autoriza3, @empresa, @centro_costo, @division, @no_empleado, @empleado, @no_autorizador, @autorizador, @RFC, @CFDI, @importe_tot, @id_tipo_servicio, @tipo_servicio, @contrato_NAV_alta, @contrato_NAV_reg, @no_contrato_NAV, @no_validador, @validador, @especificaciones, @proveedor_selec, @cotizacion_selec, @lugar_ejecucion, @base, @id_usr_valida, @id_usr_valida2, @cValidadorSop, @id_dt_servicio_conf, @id_ms_negoc_servicio, @servicio_tipo, @servicio, @admon_oper, @cValidador, @cCompras, @cFinanzas, @cPresupuesto, @cuenta_cont, @cantidad, @precio, @id_divisa, @tipo_cambio, @status)"
                            SCMValores.Parameters.AddWithValue("@id_usr_solicita", Val(._txtIdUsuario.Text))
                            SCMValores.Parameters.AddWithValue("@fecha_solicita", fecha)
                            SCMValores.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedItem.Text)
                            If .lbl_Dimension.Text = "División:" Then
                                SCMValores.Parameters.AddWithValue("@centro_costo", "")
                                SCMValores.Parameters.AddWithValue("@division", .ddlDimension.SelectedItem.Text)
                            Else
                                SCMValores.Parameters.AddWithValue("@centro_costo", .ddlDimension.SelectedItem.Text)
                                SCMValores.Parameters.AddWithValue("@division", DBNull.Value)
                            End If
                            SCMValores.Parameters.AddWithValue("@no_empleado", dsEmpleado.Tables(0).Rows(0).Item("no_empleadoE").ToString())
                            SCMValores.Parameters.AddWithValue("@empleado", .lblSolicitante.Text)
                            SCMValores.Parameters.AddWithValue("@id_tipo_servicio", DBNull.Value)
                            SCMValores.Parameters.AddWithValue("@tipo_servicio", DBNull.Value)
                            SCMValores.Parameters.AddWithValue("@id_usr_valida", .ddlValidador1.SelectedValue)
                            SCMValores.Parameters.AddWithValue("@no_validador", dsEmpleado.Tables(0).Rows(0).Item("no_empleadoV").ToString())
                            SCMValores.Parameters.AddWithValue("@validador", .ddlValidador1.SelectedItem.Text)
                            If .ddlValidador2.Visible = True Then
                                SCMValores.Parameters.AddWithValue("@id_usr_valida2", .ddlValidador2.SelectedValue)
                                SCMValores.Parameters.AddWithValue("@cValidadorSop", "S")
                            Else
                                'No Aplica
                                SCMValores.Parameters.AddWithValue("@id_usr_valida2", DBNull.Value)
                                SCMValores.Parameters.AddWithValue("@cValidadorSop", "N")
                            End If
                            SCMValores.Parameters.AddWithValue("@especificaciones", .txtDescripcion.Text.Trim)

                            SCMValores.Parameters.AddWithValue("@id_dt_servicio_conf", ._txtIdDtServ.Text)
                            SCMValores.Parameters.AddWithValue("@servicio_tipo", "Servicios Negociados")
                            SCMValores.Parameters.AddWithValue("@servicio", .lblServicio.Text)
                            SCMValores.Parameters.AddWithValue("@admon_oper", ._txtAdmonOper.Text)
                            SCMValores.Parameters.AddWithValue("@cValidador", "S")
                            SCMValores.Parameters.AddWithValue("@cCompras", ._txtCotizaciones.Text)
                            SCMValores.Parameters.AddWithValue("@cFinanzas", ._txtFinanzas.Text)
                            SCMValores.Parameters.AddWithValue("@cPresupuesto", ._txtValPresup.Text)
                            SCMValores.Parameters.AddWithValue("@cuenta_cont", ._txtCuentaCont.Text)
                            SCMValores.Parameters.AddWithValue("@lugar_ejecucion", .txtLugar.Text.Trim)
                            SCMValores.Parameters.AddWithValue("@base", .ddlBase.SelectedItem.Text)

                            SCMValores.Parameters.AddWithValue("@id_ms_negoc_servicio", Val(.txtFolioNS.Text))
                            SCMValores.Parameters.AddWithValue("@cantidad", dsCantPrec.Tables(0).Rows(0).Item("cantidad").ToString())
                            SCMValores.Parameters.AddWithValue("@precio", dsCantPrec.Tables(0).Rows(0).Item("precio").ToString())
                            SCMValores.Parameters.AddWithValue("@id_divisa", dsCantPrec.Tables(0).Rows(0).Item("id_divisa").ToString())
                            SCMValores.Parameters.AddWithValue("@tipo_cambio", dsCantPrec.Tables(0).Rows(0).Item("tipo_cambio").ToString())

                            'Contrato NAV
                            SCMValores.Parameters.AddWithValue("@contrato_NAV_alta", "N")
                            SCMValores.Parameters.AddWithValue("@contrato_NAV_reg", "N")
                            SCMValores.Parameters.AddWithValue("@no_contrato_NAV", DBNull.Value)

                            SCMValores.Parameters.AddWithValue("@RFC", ._txtRFC.Text)
                            SCMValores.Parameters.AddWithValue("@proveedor_selec", ._txtIdProveedor.Text)
                            SCMValores.Parameters.AddWithValue("@cotizacion_selec", 0)
                            SCMValores.Parameters.AddWithValue("@status", "P")
                            SCMValores.Parameters.AddWithValue("@id_usr_autoriza", DBNull.Value)
                            SCMValores.Parameters.AddWithValue("@no_autorizador", DBNull.Value)
                            SCMValores.Parameters.AddWithValue("@autorizador", DBNull.Value)
                            SCMValores.Parameters.AddWithValue("@id_usr_autoriza2", DBNull.Value)
                            SCMValores.Parameters.AddWithValue("@id_usr_autoriza3", DBNull.Value)
                            SCMValores.Parameters.AddWithValue("@CFDI", DBNull.Value)
                            SCMValores.Parameters.AddWithValue("@importe_tot", DBNull.Value)
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()
                            sdaEmpleado.Dispose()
                            dsEmpleado.Dispose()
                            sdaCantPrec.Dispose()
                            dsCantPrec.Dispose()

                            'Obtener ID de la Solicitud
                            SCMValores.CommandText = "select max(id_ms_factura) from ms_factura where no_empleado = @no_empleado and status not in ('Z') "
                            ConexionBD.Open()
                            .lblFolio.Text = SCMValores.ExecuteScalar
                            ConexionBD.Close()
                            If Val(.lblFolio.Text) > 0 Then
                                ._txtBan.Text = 1
                            End If

                            'Actualizar Cantidades no almacenadas
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "update dt_factura_sn set id_ms_factura = @idMsFactura where id_ms_factura = -1 and id_usuario = @id_usuario"
                            SCMValores.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))
                            SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            'Actualizar registro de Adjuntos / Evidencias no almacenados
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "update dt_archivo set id_ms_factura = @idMsFactura where id_ms_factura = -1 and id_usuario = @id_usuario"
                            SCMValores.Parameters.AddWithValue("@idMsFactura", Val(.lblFolio.Text))
                            SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            If .gvAdjuntosReq.Rows.Count > 0 Then
                                'Insertar Adjuntos Requeridos 
                                SCMValores.CommandText = ""
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "insert into dt_factura_adj (id_ms_factura, adjunto) " + _
                                                         "select @id_ms_factura, adjunto " + _
                                                         "from dt_servicio_adj " + _
                                                         "where id_servicio = @id_servicio "
                                SCMValores.Parameters.AddWithValue("@id_ms_factura", Val(.lblFolio.Text))
                                SCMValores.Parameters.AddWithValue("@id_servicio", Val(._txtIdServicio.Text))
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()
                            End If

                            'Insertar Instancia de Solicitud de Liberación
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "insert into ms_instancia ( id_ms_sol,  tipo,  id_actividad) " + _
                                                     "				    values (@id_ms_sol, @tipo, @id_actividad) "
                            SCMValores.Parameters.AddWithValue("@id_ms_sol", Val(.lblFolio.Text))
                            SCMValores.Parameters.AddWithValue("@tipo", "F")
                            SCMValores.Parameters.AddWithValue("@id_actividad", 94)
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()
                            'Obtener ID de la Instancia de Solicitud 
                            SCMValores.CommandText = "select max(id_ms_instancia) from ms_instancia where id_ms_sol = @id_ms_sol and tipo = 'F' "
                            ConexionBD.Open()
                            ._txtIdMsInst.Text = SCMValores.ExecuteScalar
                            ConexionBD.Close()
                            'Insertar Históricos
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "insert into ms_historico ( id_ms_instancia,  id_actividad,  id_usr,  fecha) " + _
                                                     "				    values (@id_ms_instancia, @id_actividad, @id_usr, @fecha) "
                            SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                            SCMValores.Parameters.AddWithValue("@id_actividad", 94)
                            SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                            SCMValores.Parameters.AddWithValue("@fecha", fecha)
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            'Envío de Correo
                            Dim Mensaje As New System.Net.Mail.MailMessage()
                            Dim destinatario As String = ""
                            SCMValores.CommandText = "select cgEmpl.correo from cg_usuario left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado where cg_usuario.id_usuario = @idAut "
                            SCMValores.Parameters.AddWithValue("@idAut", .ddlValidador1.SelectedValue)
                            'Obtener el Correo del Validador/Autorizador
                            ConexionBD.Open()
                            destinatario = SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            Mensaje.[To].Add(destinatario)
                            Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                            Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                            Dim texto As String
                            Mensaje.Subject = "ProcAd - Solicitud de Servicio Negociado No. " + .lblFolio.Text + " por Validar Aplicabilidad"
                            texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" + _
                                    "Se ingresó la solicitud de servicio negociado número <b>" + .lblFolio.Text + _
                                    "</b> por parte de <b>" + .lblSolicitante.Text + _
                                    "</b><br><br>Favor de Validar si Aplica </span>"
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
                    'End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ddlEmpresa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEmpresa.SelectedIndexChanged
        'Bases
        llenarBase()
        'Actualizar Centro de Costo / División
        actualizarDimension()
    End Sub

    Protected Sub rblDimension_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblDimension.SelectedIndexChanged
        'Actualizar Centro de Costo / División
        actualizarDimension()
    End Sub
End Class