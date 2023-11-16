Public Class CatUsuarioAC
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")

                        limpiarPantalla()
                    Else
                        Server.Transfer("Login.aspx")
                    End If
                Catch ex As Exception
                    .litError.Text = ex.ToString
                End Try
            End With
        End If
    End Sub

    Protected Sub ibtnBuscar_Click(sender As Object, e As EventArgs) Handles ibtnBuscar.Click
        llenarGrid()
    End Sub

#End Region

#Region "Funciones"

    Public Sub limpiarPantalla()
        With Me
            'Configuraciones Globales
            .cbTaxiTabG.Checked = False
            .cbAlimentosTabG.Checked = False
            .cbUsrFactExtempG.Checked = False
            .cbUsrFactEmiPrevG.Checked = False

            'Catálogo de Usuarios
            llenarGrid()
            .pnlGrid.Enabled = True
            .ibtnModif.Enabled = False
            .ibtnModif.ImageUrl = "images\Edit_i2.png"
            .pnlGrid.Visible = True
            .pnlDatos.Visible = False
        End With
    End Sub

    Public Sub habilitarCampos(ByVal valor As Boolean)
        With Me

            'nuevo codigo
            .cbAntXEmpr.Enabled = valor
            .txtAntXEmpr.Enabled = valor
            .cbAntPend.Enabled = valor
            .cbTransporte.Enabled = valor
            .cbLimAutDir.Enabled = valor
            .txtLimAutDir.Enabled = valor
            .cbUsrAlim.Enabled = valor
            .cbUnidadComp.Enabled = valor
            .cbUsrPagoEfect.Enabled = valor
            .cbUsrCotUnica.Enabled = valor
            .cbUsrFactEmiPrev.Enabled = valor
            .cbUsrFactExtemp.Enabled = valor
            .cbUsrFactExtempComp.Enabled = valor
            .cbLider.Enabled = valor
            .cbOmitirPGV.Enabled = valor
            .cbAlimentosTab.Enabled = valor
            .cbTaxiTab.Enabled = valor
            .cbIngresarNocheHospedaje.Enabled = valor
            .cbOmitirValidacionAnt.Enabled = valor
            .cbDatosComprobacion.Enabled = valor
            .cbFechaTermino.Enabled = valor
            .cbMovimientosLibre.Enabled = valor
            .gvAutorizadores.Enabled = valor
        End With
    End Sub

    Public Sub bloqueoPantalla()
        With Me
            .pnlGrid.Visible = False
            .pnlDatos.Visible = True
        End With
    End Sub

    Public Sub llenarGrid()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .gvUsuario.Columns(0).Visible = True
                .gvUsuario.DataSource = dsCatalogo
                'Catálogo de Usuarios
                Dim query As String = ""
                query = "select cg_usuario.id_usuario, nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre, nick, " +
                        "       perfil = case perfil " +
                        "                  when 'AltUsr' then 'Admin. Usuarios' " +
                        "                  when 'Usr' then 'Usuario' " +
                        "                  when 'Aut' then 'Autorizador' " +
                        "                  when 'DirAdFi' then 'Director de Administración y Finanzas' " +
                        "                  when 'GerConta' then 'Gerente de Contabilidad' " +
                        "                  when 'AdmCat' then 'Admin. Catálogos' " +
                        "                  when 'AdmCatEst' then 'Admin. Catálogos Estadía' " +
                        "                  when 'Vig' then 'Vigilante' " +
                        "                  when 'CoPame' then 'Comprobaciones PAME' " +
                        "                  when 'CoDCM' then 'Comprobaciones DICOMEX' " +
                        "                  when 'Comp' then 'Comprobaciones' " +
                        "                  when 'Aud' then 'Auditor' " +
                        "                  when 'Caja' then 'Caja' " +
                        "                  when 'CxP' then 'Cuentas por Pagar' " +
                        "                  when 'Conta' then 'Contador' " +
                        "                  when 'ContaF' then 'Contador Funcionarios' " +
                        "                  when 'GerSopTec' then 'Gerente de Soporte Técnico' " +
                        "                  when 'SopTec' then 'Soporte Técnico' " +
                        "                  when 'GerTesor' then 'Gerente de Tesorería' " +
                        "                  when 'ValPresup' then 'Validador de Presupuesto' " +
                        "                  when 'AdmonDCM' then 'Administración Dicomex' " +
                        "                  when 'Compras' then 'Compras' " +
                        "                  when 'JefCompras' then 'Jefe de Compras' " +
                        "                  when 'AdmViajes' then 'Administrador de Viajes' " +
                        "                  when 'SegViajes' then 'Seguimiento a Viajes' " +
                        "                  when 'DesOrg' then 'Desarrollo Organizacional' " +
                        "                  when 'Liq' then 'Liquidador' " +
                        "                  when 'AutAud' then 'Autorizador Auditoría' " +
                        "                  else '-' end " +
                        "     , case when cg_usuario_alim.id_usuario is null then 'No' else 'Sí' end as alim_mult_pers " +
                        "from cg_usuario " +
                        "  inner join bd_empleado.dbo.cg_empleado Emp on cg_usuario.id_empleado = Emp.id_empleado " +
                        "  left join cg_usuario_alim on cg_usuario.id_usuario = cg_usuario_alim.id_usuario " +
                        "where (" + .ddlCampo.SelectedValue.ToString + " like '%' + @valor +'%') " +
                        "  and cg_usuario.status = 'A' and perfil <> 'Adm' " +
                        "order by nick"
                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@valor", .txtValor.Text.Trim)
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvUsuario.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvUsuario.Columns(0).Visible = False
                .gvUsuario.SelectedIndex = -1

                'Pintar Celda en caso de aplicar
                pintarTabla(.gvUsuario)
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub pintarTabla(ByRef gridView)
        With Me
            Try
                Dim i As Integer
                Dim ban As Integer
                For i = 0 To (gridView.Rows.Count() - 1)
                    ban = 0
                    If gridView.Rows(i).Cells(5).Text <> "No" Then
                        ban = 1
                    End If
                    Select Case ban
                        Case 0
                            gridView.Rows(i).Cells(5).ForeColor = Color.Black
                            gridView.Rows(i).Cells(5).Font.Bold = False
                        Case 1
                            gridView.Rows(i).Cells(5).ForeColor = Color.Red
                            gridView.Rows(i).Cells(5).Font.Bold = True
                            'gridView.Rows(i).Cells(5).BorderColor = Color.Black
                        Case Else
                            gridView.Rows(i).Cells(5).ForeColor = Color.Black
                            gridView.Rows(i).Cells(5).Font.Bold = False
                    End Select
                Next
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub localizar(ByVal idRegistro)
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                sdaCatalogo.SelectCommand = New SqlCommand("select cg_usuario.id_empleado " +
                                                           "     , Emp.nombre + ' ' + Emp.ap_paterno + ' ' + Emp.ap_materno as empleado " +
                                                           "     , perfil = case perfil " +
                                                           "                  when 'AltUsr' then 'Admin. Usuarios' " +
                                                           "                  when 'Usr' then 'Usuario' " +
                                                           "                  when 'Aut' then 'Autorizador' " +
                                                           "                  when 'DirAdFi' then 'Director de Administración y Finanzas' " +
                                                           "                  when 'GerConta' then 'Gerente de Contabilidad' " +
                                                           "                  when 'AdmCat' then 'Admin. Catálogos' " +
                                                           "                  when 'AdmCatEst' then 'Admin. Catálogos Estadía' " +
                                                           "                  when 'Vig' then 'Vigilante' " +
                                                           "                  when 'CoPame' then 'Comprobaciones PAME' " +
                                                           "                  when 'CoDCM' then 'Comprobaciones DICOMEX' " +
                                                           "                  when 'Comp' then 'Comprobaciones' " +
                                                           "                  when 'Aud' then 'Auditor' " +
                                                           "                  when 'Caja' then 'Caja' " +
                                                           "                  when 'CxP' then 'Cuentas por Pagar' " +
                                                           "                  when 'Conta' then 'Contador' " +
                                                           "                  when 'ContaF' then 'Contador Funcionarios' " +
                                                           "                  when 'GerSopTec' then 'Gerente de Soporte Técnico' " +
                                                           "                  when 'SopTec' then 'Soporte Técnico' " +
                                                           "                  when 'GerTesor' then 'Gerente de Tesorería' " +
                                                           "                  when 'ValPresup' then 'Validador de Presupuesto' " +
                                                           "                  when 'AdmonDCM' then 'Administración Dicomex' " +
                                                           "                  when 'Compras' then 'Compras' " +
                                                           "                  when 'JefCompras' then 'Jefe de Compras' " +
                                                           "                  when 'AdmViajes' then 'Administrador de Viajes' " +
                                                           "                  when 'SegViajes' then 'Seguimiento a Viajes' " +
                                                           "                  when 'DesOrg' then 'Desarrollo Organizacional' " +
                                                           "                  when 'Liq' then 'Liquidador' " +
                                                           "                  else '-' end " +
                                                           "     , isnull(nick, '') as nick " +
                                                           "     , isnull(Emp.correo, '') as correoE " +
                                                           "     , isnull(no_anticipos, 0) as no_anticipos " +
                                                           "     , ant_pendientes " +
                                                           "     , isnull(cg_usuario_alim.id_usuario_alim, 0) as id_usr_alim " +
                                                           "     , isnull(limite_aut_dir, -1) as limite_aut_dir " +
                                                           "     , cotizacion_unica " +
                                                           "     , factura_emi_prev " +
                                                           "     , factura_extemp " +
                                                           "     , factura_extemp_comp " +
                                                           "     , pago_efectivo " +
                                                           "     , unidad_comp " +
                                                           "     , cg_usuario.transporte " +
                                                           "     , cg_usuario.lider " +
                                                           "     , cg_usuario.omitir_PGV " +
                                                           "     , cg_usuario.alimentos_tab " +
                                                           "     , cg_usuario.taxi_tab " +
                                                           "     , hospedaje_libre " +
                                                           "     , anticipo_obl " +
                                                           "     , edit_compro_datos " +
                                                           "     , isnull(cg_usuario.fecha_termino, 'N') as fecha_termino " +
                                                           "     , isnull(movimientos_internos, 'N') as movimientos_internos " +
                                                           "from cg_usuario " +
                                                           "  inner join bd_empleado.dbo.cg_empleado Emp on cg_usuario.id_empleado = Emp.id_empleado " +
                                                           "  inner join bd_empleado.dbo.cg_cc CC on Emp.id_cc = CC.id_cc " +
                                                           "  left join cg_usuario_ant on cg_usuario.id_usuario = cg_usuario_ant.id_usuario " +
                                                           "  left join cg_usuario_alim on cg_usuario.id_usuario = cg_usuario_alim.id_usuario " +
                                                           "where cg_usuario.id_usuario = @id_usuario ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_usuario", idRegistro)
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                ConexionBD.Close()
                .lblIdEmpleado.Text = dsCatalogo.Tables(0).Rows(0).Item("id_empleado").ToString()
                .lblEmpleado.Text = dsCatalogo.Tables(0).Rows(0).Item("empleado").ToString()
                .lblUsuario.Text = dsCatalogo.Tables(0).Rows(0).Item("nick").ToString()
                .lblPerfil.Text = dsCatalogo.Tables(0).Rows(0).Item("perfil").ToString()
                .lblCorreoE.Text = dsCatalogo.Tables(0).Rows(0).Item("correoE").ToString()

                'nuevo codigo
                If Val(dsCatalogo.Tables(0).Rows(0).Item("no_anticipos").ToString()) > 0 Then
                    .cbAntXEmpr.Checked = True
                    .txtAntXEmpr.Enabled = True
                    .txtAntXEmpr.Text = dsCatalogo.Tables(0).Rows(0).Item("no_anticipos").ToString()
                Else
                    .cbAntXEmpr.Checked = False
                    .txtAntXEmpr.Enabled = False
                    .txtAntXEmpr.Text = ""
                End If
                If dsCatalogo.Tables(0).Rows(0).Item("ant_pendientes").ToString() = "S" Then
                    .cbAntPend.Checked = True
                Else
                    .cbAntPend.Checked = False
                End If
                If Val(dsCatalogo.Tables(0).Rows(0).Item("id_usr_alim").ToString()) > 0 Then
                    .cbUsrAlim.Checked = True
                Else
                    .cbUsrAlim.Checked = False
                End If
                If Val(dsCatalogo.Tables(0).Rows(0).Item("limite_aut_dir").ToString()) <> -1 Then
                    .cbLimAutDir.Checked = True
                    .txtLimAutDir.Enabled = True
                    .txtLimAutDir.Text = dsCatalogo.Tables(0).Rows(0).Item("limite_aut_dir").ToString()
                Else
                    .cbLimAutDir.Checked = False
                    .txtLimAutDir.Enabled = False
                    .txtLimAutDir.Text = ""
                End If
                If dsCatalogo.Tables(0).Rows(0).Item("cotizacion_unica").ToString() = "S" Then
                    .cbUsrCotUnica.Checked = True
                Else
                    .cbUsrCotUnica.Checked = False
                End If
                If dsCatalogo.Tables(0).Rows(0).Item("factura_emi_prev").ToString() = "S" Then
                    .cbUsrFactEmiPrev.Checked = True
                Else
                    .cbUsrFactEmiPrev.Checked = False
                End If
                If dsCatalogo.Tables(0).Rows(0).Item("factura_extemp").ToString() = "S" Then
                    .cbUsrFactExtemp.Checked = True
                Else
                    .cbUsrFactExtemp.Checked = False
                End If
                If dsCatalogo.Tables(0).Rows(0).Item("factura_extemp_comp").ToString() = "S" Then
                    .cbUsrFactExtempComp.Checked = True
                Else
                    .cbUsrFactExtempComp.Checked = False
                End If
                If dsCatalogo.Tables(0).Rows(0).Item("pago_efectivo").ToString() = "S" Then
                    .cbUsrPagoEfect.Checked = True
                Else
                    .cbUsrPagoEfect.Checked = False
                End If
                If dsCatalogo.Tables(0).Rows(0).Item("unidad_comp").ToString() = "S" Then
                    .cbUnidadComp.Checked = True
                Else
                    .cbUnidadComp.Checked = False
                End If
                If Val(dsCatalogo.Tables(0).Rows(0).Item("transporte").ToString()) = 1 Then
                    .cbTransporte.Checked = True
                Else
                    .cbTransporte.Checked = False
                End If
                If dsCatalogo.Tables(0).Rows(0).Item("lider").ToString() = "S" Then
                    .cbLider.Checked = True
                Else
                    .cbLider.Checked = False
                End If
                If dsCatalogo.Tables(0).Rows(0).Item("omitir_PGV").ToString() = "S" Then
                    .cbOmitirPGV.Checked = True
                Else
                    .cbOmitirPGV.Checked = False
                End If
                If dsCatalogo.Tables(0).Rows(0).Item("alimentos_tab").ToString() = "S" Then
                    .cbAlimentosTab.Checked = True
                Else
                    .cbAlimentosTab.Checked = False
                End If
                If dsCatalogo.Tables(0).Rows(0).Item("taxi_tab").ToString() = "S" Then
                    .cbTaxiTab.Checked = True
                Else
                    .cbTaxiTab.Checked = False
                End If
                If dsCatalogo.Tables(0).Rows(0).Item("hospedaje_libre").ToString = "S" Then
                    .cbIngresarNocheHospedaje.Checked = True
                Else
                    .cbIngresarNocheHospedaje.Checked = False
                End If
                If dsCatalogo.Tables(0).Rows(0).Item("anticipo_obl").ToString = "S" Then
                    .cbOmitirValidacionAnt.Checked = True
                Else
                    .cbOmitirValidacionAnt.Checked = False
                End If
                If dsCatalogo.Tables(0).Rows(0).Item("edit_compro_datos").ToString = "S" Then
                    .cbDatosComprobacion.Checked = True
                Else
                    .cbDatosComprobacion.Checked = False
                End If
                If dsCatalogo.Tables(0).Rows(0).Item("fecha_termino").ToString() = "S" Then
                    .cbFechaTermino.Checked = True
                Else
                    .cbFechaTermino.Checked = False
                End If
                If dsCatalogo.Tables(0).Rows(0).Item("movimientos_internos").ToString() = "S" Then
                    cbMovimientosLibre.Checked = True
                Else
                    cbMovimientosLibre.Checked = False
                End If

                llenarAutorizadores(idRegistro)
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                bloqueoPantalla()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarAutorizadores(ByVal idUsuario)
        With Me
            Try
                'Llenar Grid
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaAutorizadores As New SqlDataAdapter
                Dim dsAutorizadores As New DataSet
                .gvAutorizadores.DataSource = dsAutorizadores
                sdaAutorizadores.SelectCommand = New SqlCommand("select cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as autorizador " +
                                                                "     , case aut_dir when 'S' then 'Sí' else null end as director " +
                                                                "from dt_autorizador " +
                                                                "  left join cg_usuario on dt_autorizador.id_autorizador = cg_usuario.id_usuario " +
                                                                "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                                "where dt_autorizador.id_usuario = @idUsuario " +
                                                                "order by director, autorizador ", ConexionBD)
                sdaAutorizadores.SelectCommand.Parameters.AddWithValue("@idUsuario", idUsuario)
                ConexionBD.Open()
                sdaAutorizadores.Fill(dsAutorizadores)
                .gvAutorizadores.DataBind()
                ConexionBD.Close()
                sdaAutorizadores.Dispose()
                dsAutorizadores.Dispose()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Selección"

    Protected Sub gvUsuario_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvUsuario.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                .ibtnModif.Enabled = True
                .ibtnModif.ImageUrl = "images\Edit.png"
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Botones Inicio"

    Protected Sub ibtnModif_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnModif.Click
        With Me
            Try
                ._txtTipoMov.Text = "M"
                .lblTipoMov.Text = "Modificación"
                localizar(.gvUsuario.SelectedRow.Cells(0).Text)
                habilitarCampos(True)
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Botones Datos"

    Protected Sub cbAntXEmpr_CheckedChanged(sender As Object, e As EventArgs) Handles cbAntXEmpr.CheckedChanged
        With Me
            If .cbAntXEmpr.Checked = True Then
                .cbAntXEmpr.Checked = True
                .txtAntXEmpr.Enabled = True
                .txtAntXEmpr.Text = 1
            Else
                .cbAntXEmpr.Checked = False
                .txtAntXEmpr.Enabled = False
                .txtAntXEmpr.Text = ""
            End If
        End With
    End Sub

    Protected Sub cbLimAutDir_CheckedChanged(sender As Object, e As EventArgs) Handles cbLimAutDir.CheckedChanged
        With Me
            If .cbLimAutDir.Checked = True Then
                .cbLimAutDir.Checked = True
                .txtLimAutDir.Enabled = True
                .txtLimAutDir.Text = 1
            Else
                .cbLimAutDir.Checked = False
                .txtLimAutDir.Enabled = False
                .txtLimAutDir.Text = ""
            End If
        End With
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""
                Dim ban As Integer = 0
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "UPDATE cg_usuario SET  ant_pendientes = @ant_pendientes, limite_aut_dir = @limite_aut_dir, cotizacion_unica = @cotizacion_unica, factura_extemp = @factura_extemp, factura_emi_prev = @factura_emi_prev, pago_efectivo = @pago_efectivo, unidad_comp = @unidad_comp, transporte = @transporte, lider = @lider, omitir_PGV = @omitir_PGV, alimentos_tab = @alimentos_tab, taxi_tab = @taxi_tab , hospedaje_libre=@hospedaje_libre ,factura_extemp_comp = @factura_extemp_comp , anticipo_obl = @anticipo_obl , edit_compro_datos = @edit_compro_datos, fecha_termino = @fecha_termino, movimientos_internos = @movimientos_internos WHERE id_usuario = @id_usuario"
                SCMValores.Parameters.AddWithValue("@id_usuario", .gvUsuario.SelectedRow.Cells(0).Text)
                If ban = 0 Then
                    If .cbAntPend.Checked = True Then
                        SCMValores.Parameters.AddWithValue("@ant_pendientes", "S")
                    Else
                        SCMValores.Parameters.AddWithValue("@ant_pendientes", "N")
                    End If
                    If .cbLimAutDir.Checked = True Then
                        SCMValores.Parameters.AddWithValue("@limite_aut_dir", .txtLimAutDir.Text)
                    Else
                        SCMValores.Parameters.AddWithValue("@limite_aut_dir", DBNull.Value)
                    End If
                    If .cbUsrCotUnica.Checked = True Then
                        SCMValores.Parameters.AddWithValue("@cotizacion_unica", "S")
                    Else
                        SCMValores.Parameters.AddWithValue("@cotizacion_unica", "N")
                    End If
                    If .cbUsrFactEmiPrev.Checked = True Then
                        SCMValores.Parameters.AddWithValue("@factura_emi_prev", "S")
                    Else
                        SCMValores.Parameters.AddWithValue("@factura_emi_prev", "N")
                    End If
                    If .cbUsrFactExtemp.Checked = True Then
                        SCMValores.Parameters.AddWithValue("@factura_extemp", "S")
                    Else
                        SCMValores.Parameters.AddWithValue("@factura_extemp", "N")
                    End If
                    If .cbUsrFactExtempComp.Checked = True Then
                        SCMValores.Parameters.AddWithValue("@factura_extemp_comp", "S")
                    Else
                        SCMValores.Parameters.AddWithValue("@factura_extemp_comp", "N")
                    End If
                    If .cbUsrPagoEfect.Checked = True Then
                        SCMValores.Parameters.AddWithValue("@pago_efectivo", "S")
                    Else
                        SCMValores.Parameters.AddWithValue("@pago_efectivo", "N")
                    End If
                    If .cbUnidadComp.Checked = True Then
                        SCMValores.Parameters.AddWithValue("@unidad_comp", "S")
                    Else
                        SCMValores.Parameters.AddWithValue("@unidad_comp", "N")
                    End If
                    If .cbTransporte.Checked = True Then
                        SCMValores.Parameters.AddWithValue("@transporte", 1)
                    Else
                        SCMValores.Parameters.AddWithValue("@transporte", 0)
                    End If
                    If .cbLider.Checked = True Then
                        SCMValores.Parameters.AddWithValue("@lider", "S")
                    Else
                        SCMValores.Parameters.AddWithValue("@lider", "N")
                    End If
                    If .cbOmitirPGV.Checked = True Then
                        SCMValores.Parameters.AddWithValue("@omitir_PGV", "S")
                    Else
                        SCMValores.Parameters.AddWithValue("@omitir_PGV", "N")
                    End If
                    If .cbAlimentosTab.Checked = True Then
                        SCMValores.Parameters.AddWithValue("@alimentos_tab", "S")
                    Else
                        SCMValores.Parameters.AddWithValue("@alimentos_tab", "N")
                    End If
                    If .cbTaxiTab.Checked = True Then
                        SCMValores.Parameters.AddWithValue("@taxi_tab", "S")
                    Else
                        SCMValores.Parameters.AddWithValue("@taxi_tab", "N")
                    End If
                    If cbIngresarNocheHospedaje.Checked = True Then
                        SCMValores.Parameters.AddWithValue("@hospedaje_libre", "S")
                    Else
                        SCMValores.Parameters.AddWithValue("@hospedaje_libre", "N")
                    End If
                    If cbOmitirValidacionAnt.Checked = True Then
                        SCMValores.Parameters.AddWithValue("@anticipo_obl", "S")
                    Else
                        SCMValores.Parameters.AddWithValue("@anticipo_obl", "N")
                    End If
                    If cbDatosComprobacion.Checked = True Then
                        SCMValores.Parameters.AddWithValue("@edit_compro_datos", "S")
                    Else
                        SCMValores.Parameters.AddWithValue("@edit_compro_datos", "N")
                    End If
                    If cbFechaTermino.Checked = True Then
                        SCMValores.Parameters.AddWithValue("@fecha_termino", "S")
                    Else
                        SCMValores.Parameters.AddWithValue("@fecha_termino", "N")
                    End If
                    If cbMovimientosLibre.Checked = True Then
                        SCMValores.Parameters.AddWithValue("@movimientos_internos", "S")
                    Else
                        SCMValores.Parameters.AddWithValue("@movimientos_internos", "N")
                    End If
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Obtener el id_usuario
                    Dim idUsr As Integer
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "select isnull(max(id_usuario), 0) from cg_usuario WHERE id_empleado = @id_empleado and status = 'A' "
                    SCMValores.Parameters.AddWithValue("@id_empleado", Val(.lblIdEmpleado.Text))
                    ConexionBD.Open()
                    idUsr = SCMValores.ExecuteScalar()
                    ConexionBD.Close()

                    'Anticipos por Empresa
                    Dim cont As Integer
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "select count(*) from cg_usuario_ant where id_usuario = @id_usuario "
                    SCMValores.Parameters.AddWithValue("@id_usuario", idUsr)
                    ConexionBD.Open()
                    cont = SCMValores.ExecuteScalar()
                    ConexionBD.Close()

                    If .cbAntXEmpr.Checked = True Then
                        If cont > 0 Then
                            SCMValores.CommandText = "update cg_usuario_ant set no_anticipos = @no_anticipos where id_usuario = @id_usuario "
                        Else
                            SCMValores.CommandText = "insert into cg_usuario_ant (id_usuario, no_anticipos) values (@id_usuario, @no_anticipos) "
                        End If
                        SCMValores.Parameters.AddWithValue("@no_anticipos", Val(.txtAntXEmpr.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()
                    Else
                        If cont > 0 Then
                            SCMValores.CommandText = "delete from cg_usuario_ant where id_usuario = @id_usuario "
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()
                        End If
                    End If

                    'Solicitar Alimentos para Múltiples
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "select count(*) from cg_usuario_alim where id_usuario = @id_usuario "
                    SCMValores.Parameters.AddWithValue("@id_usuario", idUsr)
                    ConexionBD.Open()
                    cont = SCMValores.ExecuteScalar()
                    ConexionBD.Close()

                    If .cbUsrAlim.Checked = True Then
                        If cont = 0 Then
                            SCMValores.CommandText = "insert into cg_usuario_alim (id_usuario) values (@id_usuario) "
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()
                        End If
                    Else
                        If cont > 0 Then
                            SCMValores.CommandText = "delete from cg_usuario_alim where id_usuario = @id_usuario "
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()
                        End If
                    End If

                    'Identificar si existe registro dt_empleado sin id_usuario
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "select count(*) " +
                                             "from dt_empleado " +
                                             "where no_empleado in (select case len(cg_empleado.no_empleado) when 4 then '000' + cg_empleado.no_empleado when 5 then '00' + cg_empleado.no_empleado else cg_empleado.no_empleado end " +
                                             "                      from cg_usuario " +
                                             "                          left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " +
                                             "                      where cg_usuario.id_usuario = @id_usuario) " +
                                             "  and id_usr_empl is null " +
                                             " and dt_empleado.status = 'A' "
                    SCMValores.Parameters.AddWithValue("@id_usuario", idUsr)
                    ConexionBD.Open()
                    cont = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    If cont > 0 Then
                        Dim idDtEmpleado As Integer
                        SCMValores.CommandText = "select id_dt_empleado " +
                                                 "from dt_empleado " +
                                                 "where no_empleado in (select case len(cg_empleado.no_empleado) when 4 then '000' + cg_empleado.no_empleado when 5 then '00' + cg_empleado.no_empleado else cg_empleado.no_empleado end " +
                                                 "                      from cg_usuario " +
                                                 "                          left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado " +
                                                 "                      where cg_usuario.id_usuario = @id_usuario) " +
                                                 "  and id_usr_empl is null " +
                                                 " and dt_empleado.status = 'A' "
                        ConexionBD.Open()
                        idDtEmpleado = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        'Actualizar registro de la tabla dt_empleado
                        SCMValores.CommandText = "update dt_empleado " +
                                                 "  set id_usr_empl = @id_usuario " +
                                                 "where id_dt_empleado = @id_dt_empleado "
                        SCMValores.Parameters.AddWithValue("@id_dt_empleado", idDtEmpleado)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()
                    End If

                    limpiarPantalla()
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        limpiarPantalla()
    End Sub

#End Region

#Region "Configuraciones Globales"

    Public Sub actGlobal(ByVal campo, ByVal valor)
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD

                'Actualizar usuarios activos
                SCMValores.CommandText = "update cg_usuario " +
                                         "  set " + campo + " = '" + valor + "' " +
                                         "where status = 'A' "
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                .litError.Text = "Se actualizaron todos los usuarios activos, "
                If valor = "S" Then
                    .litError.Text = .litError.Text + "marcando"
                Else
                    .litError.Text = .litError.Text + "desmarcando"
                End If
                .litError.Text = .litError.Text + " la opción '"
                Select Case campo
                    Case "taxi_tab"
                        .litError.Text = .litError.Text + "Taxi Tabulador'"
                    Case "alimentos_tab"
                        .litError.Text = .litError.Text + "Alimentos Tabulador'"
                    Case "factura_emi_prev"
                        .litError.Text = .litError.Text + "Ingresar Facturas Emisión Previa'"
                    Case "factura_extemp"
                        .litError.Text = .litError.Text + "Ingresar Facturas Extemporánea'"
                    Case "factura_extemp_comp"
                        .litError.Text = .litError.Text + "Ingresar Facturas Extemporánea en Comprobaciones'"
                End Select
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnTaxiTabG_Click(sender As Object, e As EventArgs) Handles btnTaxiTabG.Click
        If Me.cbTaxiTabG.Checked = True Then
            actGlobal("taxi_tab", "S")
        Else
            actGlobal("taxi_tab", "N")
        End If
    End Sub

    Protected Sub btnAlimentosTabG_Click(sender As Object, e As EventArgs) Handles btnAlimentosTabG.Click
        If Me.cbAlimentosTabG.Checked = True Then
            actGlobal("alimentos_tab", "S")
        Else
            actGlobal("alimentos_tab", "N")
        End If
    End Sub

    Protected Sub btnUsrFactExtempG_Click(sender As Object, e As EventArgs) Handles btnUsrFactExtempG.Click
        If Me.cbUsrFactExtempG.Checked = True Then
            actGlobal("factura_extemp", "S")
        Else
            actGlobal("factura_extemp", "N")
        End If
    End Sub

    Protected Sub btnUsrFactEmiPrevG_Click(sender As Object, e As EventArgs) Handles btnUsrFactEmiPrevG.Click
        If Me.cbUsrFactEmiPrevG.Checked = True Then
            actGlobal("factura_emi_prev", "S")
        Else
            actGlobal("factura_emi_prev", "N")
        End If
    End Sub

    Protected Sub btnUsrFactExtempCompG_Click(sender As Object, e As EventArgs) Handles btnUsrFactExtempCompG.Click
        If Me.cbUsrFactExtempCompG.Checked = True Then
            actGlobal("factura_extemp_comp", "S")
        Else
            actGlobal("factura_extemp_comp", "N")
        End If
    End Sub

#End Region

End Class