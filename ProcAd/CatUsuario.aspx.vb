Public Class CatUsuario
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

    'variable para saber si se ha cambiado el texto
    Private cambioContraseña As Boolean
    Private cambioPerfil As Boolean
    Private cambioUsuario As Boolean
    Private cambioTipoC As Boolean

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

    Protected Sub cmdBuscarE_Click(sender As Object, e As EventArgs) Handles cmdBuscarE.Click
        If Me.lblTipoMov.Text = "Alta" Then
            llenarEmpleados(0)
        Else
            llenarEmpleados(Me.gvUsuario.SelectedRow.Cells(0).Text)
        End If
        idGralE()
        empleadoInf()
        'usrUnificado()
        'passUnificado()
        'correoE()
    End Sub

#End Region

#Region "Funciones"

    Public Sub limpiarPantalla()
        With Me
            llenarGrid()
            .pnlGrid.Enabled = True
            .ibtnAlta.Enabled = True
            .ibtnBaja.Enabled = False
            .ibtnBaja.ImageUrl = "images\Trash_i2.png"
            .ibtnModif.Enabled = False
            .ibtnModif.ImageUrl = "images\Edit_i2.png"
            .pnlGrid.Visible = True
            .pnlDatos.Visible = False
        End With
    End Sub

    Public Sub habilitarCampos(ByVal valor As Boolean)
        With Me
            .txtEmpleado.Enabled = valor
            .cmdBuscarE.Enabled = valor
            .ddlEmpleado.Enabled = valor
            .txtUsuario.Enabled = valor
            '.txtContraseña.Enabled = valor
            .ddlPerfil.Enabled = valor
            .txtCorreoE.Enabled = valor
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
            .cbFechaTermino.Enabled = valor
            .cbIngresarNocheHospedaje.Enabled = valor
            .cbOmitirValidacionAnt.Enabled = valor
            .cbDatosComprobacion.Enabled = valor
            .cbMovimientosLibre.Enabled = valor
            .cbAmericanExpress.Enabled = valor
            .gvAutorizadores.Enabled = valor
            .ibtnAltaAut.Enabled = valor
            .ibtnBajaAut.Enabled = valor
            .ibtnModifAut.Enabled = valor
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
                query = "select id_usuario, nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre, nick, " +
                        "       perfil = case perfil " +
                        "                  when 'AltUsr' then 'Admin. Usuarios' " +
                        "                  when 'Usr' then 'Usuario' " +
                        "                  when 'UsrSL' then 'Usuario Solo Lectura' " +
                        "                  when 'Aut' then 'Autorizador' " +
                        "                  when 'DirAdFi' then 'Director de Administración y Finanzas' " +
                        "                  when 'AdmViajes' then 'Administrador de Viajes' " +
                        "                  when 'SegViajes' then 'Seguimiento a Viajes' " +
                        "                  when 'GerConta' then 'Gerente de Contabilidad' " +
                        "                  when 'AdmCat' then 'Admin. Catálogos' " +
                        "                  when 'AdmCatEst' then 'Admin. Catálogos Estadía' " +
                        "                  when 'Conta' then 'Contador' " +
                        "                  when 'ContaF' then 'Contador Funcionarios' " +
                        "                  when 'Vig' then 'Vigilante' " +
                        "                  when 'GerTesor' then 'Gerente de Tesorería' " +
                        "                  when 'ValPresup' then 'Validador de Presupuesto' " +
                        "                  when 'CoPame' then 'Comprobaciones PAME' " +
                        "                  when 'CoDCM' then 'Comprobaciones DICOMEX' " +
                        "                  when 'Comp' then 'Comprobaciones' " +
                        "                  when 'Caja' then 'Caja' " +
                        "                  when 'CxP' then 'Cuentas por Pagar' " +
                        "                  when 'AdmonDCM' then 'Administración Dicomex' " +
                        "                  when 'GerSopTec' then 'Gerente de Soporte Técnico' " +
                        "                  when 'SopTec' then 'Soporte Técnico' " +
                        "                  when 'JefCompras' then 'Jefe de Compras' " +
                        "                  when 'Compras' then 'Compras' " +
                        "                  when 'DesOrg' then 'Desarrollo Organizacional' " +
                        "                  when 'Liq' then 'Liquidador' " +
                        "                  when 'AutAud' then 'Autorizador Auditoría' " +
                        "                  when 'Aud' then 'Auditor' " +
                        "                  else '-' end " +
                        "from cg_usuario " +
                        "  inner join bd_empleado.dbo.cg_empleado Emp on cg_usuario.id_empleado = Emp.id_empleado " +
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
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarEmpleados(ByVal idUsuario)
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .ddlEmpleado.DataSource = dsCatalogo
                'Catálogo de Empleados
                sdaCatalogo.SelectCommand = New SqlCommand("select id_empleado, Emp.nombre + ' ' + ap_paterno + ' ' + ap_materno + ' [' + CC.nombre + ']' as empleado " +
                                                           "from bd_empleado.dbo.cg_empleado Emp " +
                                                           "  inner join bd_empleado.dbo.cg_cc CC on Emp.id_cc = CC.id_cc " +
                                                           "where (ap_paterno + ' ' + ap_materno + ' ' + Emp.nombre like '%' + @nombreEmpl +'%') and Emp.status = 'A' " +
                                                           "  and id_empleado not in (select id_empleado from cg_usuario where cg_usuario.status = 'A' and id_usuario <> @idUsuario) " +
                                                           "order by empleado ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@nombreEmpl", .txtEmpleado.Text)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@idUsuario", idUsuario)
                .ddlEmpleado.DataTextField = "empleado"
                .ddlEmpleado.DataValueField = "id_empleado"
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .ddlEmpleado.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .ddlEmpleado.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarAutorizadores()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .ddlUsuarioAut.DataSource = dsCatalogo
                'Catálogo de Autorizadores
                sdaCatalogo.SelectCommand = New SqlCommand("select id_usuario " +
                                                           "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as autorizador " +
                                                           "from cg_usuario " +
                                                           "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                           "where cg_usuario.status = 'A' " +
                                                           "  and cgEmpl.status = 'A' " +
                                                           "  and cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno like '%' + @nombreEmplAut + '%' " +
                                                           "order by autorizador ", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@nombreEmplAut", .txtUsuarioAutB.Text)
                .ddlUsuarioAut.DataTextField = "autorizador"
                .ddlUsuarioAut.DataValueField = "id_usuario"
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .ddlUsuarioAut.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .ddlUsuarioAut.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub actualizarPass()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()

                'Actualizar Usuario y Contraseña del Empleado en ProcAd
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "UPDATE bd_ProcAd.dbo.cg_usuario SET nick = @nick, pass = @pass WHERE id_empleado = @id_empleado and status = 'A' "
                SCMValores.Parameters.AddWithValue("@id_empleado", .ddlEmpleado.SelectedValue)
                SCMValores.Parameters.AddWithValue("@nick", .txtUsuario.Text.Trim.ToLower)
                SCMValores.Parameters.AddWithValue("@pass", .txtContraseña.Text.Trim)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                'Actualizar Usuario y Contraseña del Empleado en SiLi
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "UPDATE bd_SiLi.dbo.cg_usuario SET nick = @nick, pass = @pass WHERE id_empleado = @id_empleado and status = 'A' "
                SCMValores.Parameters.AddWithValue("@id_empleado", .ddlEmpleado.SelectedValue)
                SCMValores.Parameters.AddWithValue("@nick", .txtUsuario.Text.Trim.ToLower)
                SCMValores.Parameters.AddWithValue("@pass", .txtContraseña.Text.Trim)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                ' * * *  C A J A  * * * 
                'Actualizar Usuario y Contraseña del Empleado en SiCa
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "UPDATE bd_SiCa.dbo.cg_usuario SET nick = @nick, pass = @pass WHERE id_empleado = @id_empleado and status = 'A' "
                SCMValores.Parameters.AddWithValue("@id_empleado", .ddlEmpleado.SelectedValue)
                SCMValores.Parameters.AddWithValue("@nick", .txtUsuario.Text.Trim.ToLower)
                SCMValores.Parameters.AddWithValue("@pass", .txtContraseña.Text.Trim)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                'Actualizar Usuario y Contraseña del Empleado en EfEx
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "UPDATE bd_EfEx.dbo.cg_usuario SET nick = @nick, pass = @pass WHERE id_empleado = @id_empleado and status = 'A' "
                SCMValores.Parameters.AddWithValue("@id_empleado", .ddlEmpleado.SelectedValue)
                SCMValores.Parameters.AddWithValue("@nick", .txtUsuario.Text.Trim.ToLower)
                SCMValores.Parameters.AddWithValue("@pass", .txtContraseña.Text.Trim)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                ' * * *  C O M P R A S  * * * 
                'Actualizar Usuario y Contraseña del Empleado en SiSAC
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "UPDATE bd_SiSAC.dbo.cg_usuario SET nick = @nick, pass = @pass WHERE id_empleado = @id_empleado and status = 'A' "
                SCMValores.Parameters.AddWithValue("@id_empleado", .ddlEmpleado.SelectedValue)
                SCMValores.Parameters.AddWithValue("@nick", .txtUsuario.Text.Trim.ToLower)
                SCMValores.Parameters.AddWithValue("@pass", .txtContraseña.Text.Trim)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                ' * * *  T I C K E T S  * * * 
                'Actualizar Usuario y Contraseña del Empleado en SiTAF
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "UPDATE bd_SiTAF.dbo.cg_usuario SET nick = @nick, pass = @pass WHERE id_empleado = @id_empleado and status = 'A' "
                SCMValores.Parameters.AddWithValue("@id_empleado", .ddlEmpleado.SelectedValue)
                SCMValores.Parameters.AddWithValue("@nick", .txtUsuario.Text.Trim.ToLower)
                SCMValores.Parameters.AddWithValue("@pass", .txtContraseña.Text.Trim)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                'Actualizar Usuario y Contraseña del Empleado en SiTA
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "UPDATE bd_SiTA.dbo.cg_usuario SET nick = @nick, pass = @pass WHERE id_empleado = @id_empleado and status = 'A' "
                SCMValores.Parameters.AddWithValue("@id_empleado", .ddlEmpleado.SelectedValue)
                SCMValores.Parameters.AddWithValue("@nick", .txtUsuario.Text.Trim.ToLower)
                SCMValores.Parameters.AddWithValue("@pass", .txtContraseña.Text.Trim)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                'Actualizar Usuario y Contraseña del Empleado en SiTTe
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "UPDATE bd_SiTTe.dbo.cg_usuario SET nick = @nick, pass = @pass WHERE id_empleado = @id_empleado and status = 'A' "
                SCMValores.Parameters.AddWithValue("@id_empleado", .ddlEmpleado.SelectedValue)
                SCMValores.Parameters.AddWithValue("@nick", .txtUsuario.Text.Trim.ToLower)
                SCMValores.Parameters.AddWithValue("@pass", .txtContraseña.Text.Trim)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                'Actualizar Usuario y Contraseña del Empleado en SiSeg
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "UPDATE bd_SiSeg.dbo.cg_usuario SET nick = @nick, pass = @pass WHERE id_empleado = @id_empleado and status = 'A' "
                SCMValores.Parameters.AddWithValue("@id_empleado", .ddlEmpleado.SelectedValue)
                SCMValores.Parameters.AddWithValue("@nick", .txtUsuario.Text.Trim.ToLower)
                SCMValores.Parameters.AddWithValue("@pass", .txtContraseña.Text.Trim)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                ' * * *  A C T I V O S  F I J O S  * * * 
                'Actualizar Usuario y Contraseña del Empleado en SiRAc
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "UPDATE bd_SiRAc.dbo.cg_usuario SET nick = @nick, pass = @pass WHERE id_empleado = @id_empleado and status = 'A' "
                SCMValores.Parameters.AddWithValue("@id_empleado", .ddlEmpleado.SelectedValue)
                SCMValores.Parameters.AddWithValue("@nick", .txtUsuario.Text.Trim.ToLower)
                SCMValores.Parameters.AddWithValue("@pass", .txtContraseña.Text.Trim)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                'Actualizar Usuario y Contraseña del Empleado en SiCEm
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "UPDATE bd_empleado.dbo.cg_usuario SET nick = @nick, pass = @pass WHERE id_empleado = @id_empleado and status = 'A' "
                SCMValores.Parameters.AddWithValue("@id_empleado", .ddlEmpleado.SelectedValue)
                SCMValores.Parameters.AddWithValue("@nick", .txtUsuario.Text)
                SCMValores.Parameters.AddWithValue("@pass", .txtContraseña.Text.Trim)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                'Actualizar Correo del Empleado en SiCEm
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "UPDATE bd_empleado.dbo.cg_empleado SET correo = @correo, id_gral = @id_gral WHERE id_empleado = @id_empleado and status = 'A' "
                SCMValores.Parameters.AddWithValue("@id_empleado", .ddlEmpleado.SelectedValue)
                SCMValores.Parameters.AddWithValue("@id_gral", ._txtIdGral.Text.Trim)
                SCMValores.Parameters.AddWithValue("@correo", .txtCorreoE.Text.Trim.ToLower)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                Dim idGral As Integer
                idGral = Val(._txtIdGral.Text.Trim)

                'Actualizar Usuario y Contraseña del Empleado en SiTTi
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "UPDATE bd_SiTTi.dbo.cg_usuario SET nick = @nick, pass = @pass WHERE id_empleado in (select id_empleado from bd_SiTTi.dbo.cg_empleado where id_gral = @id_gral) and status = 'A' "
                SCMValores.Parameters.AddWithValue("@id_gral", Val(._txtIdGral.Text.Trim))
                SCMValores.Parameters.AddWithValue("@nick", .txtUsuario.Text.Trim.ToLower)
                SCMValores.Parameters.AddWithValue("@pass", .txtContraseña.Text.Trim)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub enviarCorreo()
        Try
            litError.Text = ""

            Dim Mensaje As New System.Net.Mail.MailMessage()
            Dim destinatario As String = ""
            Dim nick As String = ""
            Dim pass As String = ""
            Dim pin As String = ""

            destinatario = Me.txtCorreoE.Text.Trim
            nick = Me.txtUsuario.Text.Trim
            pass = Me.txtContraseñaOrg.Text.Trim
            pin = Me.txtPIN.Text.Trim

            Mensaje.[To].Add(destinatario)
            Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
            'Mensaje.From = New System.Net.Mail.MailAddress("soporte.unne.procad@gmail.com", "ProcAd", System.Text.Encoding.UTF8)
            Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
            Mensaje.Subject = "Usuario ProcAd "
            Dim texto As String
            texto = "<span style=""font-family:Verdana;font-size: 10pt;"">
                     Buen día: 
                 <br>
                 <br> Por medio de la presente te hacemos llegar tu usuario y contraseña para el uso del ProcAd (Sistema de Procesos Administrativos). 
                      Es muy importante que utilices correctamente esta información considerando los siguientes aspectos: 
                 <br>
                 <ul style= ""list-style-type: square;"">
                 <li> El acceso al ProcAd es a través de la dirección http://148.223.153.43/ProcAd </li>
                 <li> La utilización del usuario y contraseña son de uso <b>PERSONAL</b>, es decir, tú eres el responsable de cualquier movimiento 
                        que se haga en el sistema con ese usuario, por lo tanto no debes prestarlo a nadie. </li>
                 </ul>
                 </span>
                 <table style=""font-family:Calibri;font-size: 11pt; border: 1px solid black; border-collapse: collapse; border-spacing: 5px; width: 310px"">
                 <tr><th style=""border: 1px solid black; border-collapse: collapse; border-spacing: 10px; text-align: center; width:125 px; color: white; background-color: black""> Usuario ProcAd </th>
                     <th style=""border: 1px solid black; border-collapse: collapse; border-spacing: 10px; text-align: center; width:125 px; color: white; background-color: black""> Contraseña </th>
                     <th style=""border: 1px solid black; border-collapse: collapse; border-spacing: 10px; text-align: center; width:45 px; color: white; background-color: black""> PIN </th>
                 </tr>
                 <tr><th style=""border: 1px solid black; border-collapse: collapse; border-spacing: 10px; text-align: center; width:125 px; color: black; background-color: #a9a9a9""> " + nick + " </th>
                     <th style=""border: 1px solid black; border-collapse: collapse; border-spacing: 10px; text-align: center; width:125 px;"">" + pass + "</th>
                     <th style=""border: 1px solid black; border-collapse: collapse; border-spacing: 10px; text-align: center; width:45 px;"">" + pin + "</th>
                 </tr>
                 </table>
                 <span style=""font-family:Verdana;font-size: 10pt;"">                 
                 <br>Saludos.
                 <br></span>
                 <span style=""font-family:Verdana;font-size: 11pt;"">     
                 <br><img src=""data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAFkAAABZCAYAAAEiDLg5AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsQAAA7EAZUrDhsAAAfCSURBVHhe7Z1NbBVVFMe7dOnSJUuXLl26NDHEr0qhlFJKW1pbgdrKo0QpKAoSBCREE2KCJn4kuICFmqgLNNEQjQnGaIgmhoUaDQtZunz2d53/eN7lznTm8ea2D+5Jfpn7ce55p+fduTPvzp3bgc2jOy5XZXF5+b6BRwaH2lVptVob7pgG3139vj0+M9fev3zYpUcmdhU3QMhwJHTLy8v3kH9ieOv16b0L7ZNn33B1HWIbKE8DjmqwsLDwkPsEWaSQBkrTAGXlOeZ/w8ahLTc5roZrMDkze6gqK97cW6svOXeq0pfKb7/7vusv137+pb1rz7wrPHvurVsUwX2tJEYnp87rW9PRfptOfGUJZYIvBL28YuPg0D+kh7aNXVHZ48PbnLIM/qdYsY/U6h8dvvWSvGs3QTIepMO4hkQgv2n7eK7IEPn4lpE8X4XmPUfU44E8p5IbljKxdRqCgXPRno/WTiXjKpt6em4/Rzu+2yN1VvKwZPlcKLPGrY41LsO2vkNU2Wuc5xr2e01jXifDOclwTjKcM3DwyCsdBQz0uhgsHHjeHbkAcEHQxaEKA598+lkbbOHnl7/Ib7jfu/Bh+1Q2mpG2emU0Fwo7fDLmKu8qVkR1bsQyeY3LgyPb/zfki5RDhhHyvmFu7HzDqlO+0LDyj24e/tMa1hXEN8zlLr+DlDKEDM/Pzz9MOmQYQqHI81Ka37fvQVuB4VAjDFvj0vHzHbJ1x8RFVciwVSQtw/yU8+tzUQV/qtK9oMP9XlLrjr4OjXncFI3ehDZFcjoWyelYJKdjkZyORf86bae8uQPX3I5u71899borZ5qTMmH1dcdO/q8bN9pXvvnWQRk/Hzj2itQ9YuGcltj5L6HfAgjzYZTpN4CVscnpN207q2PvxoXuxIV+P0DWrJp04/TU7O7pLDmwuLj4gNoVOa35dnj5tZOlTof8gduONPXM8WdZJyEd2cNB+ztnd2upVqSp73Cax4hqKAZHRq9m1YVOSzcrclLmNBw+djz/jM1jO+tHWvO0Ej0d8cvVqMghcM9PPQk5LWxbkTVz88Ghn1ruQZyUQx+I8AzIGi1zGiZmZo5m1U7KnA45njUrF7/Resd1j1DFesY5zdfdT/RdlJPDTZMcbprkcNMkh5smOdw0yeGmSQ43TXK4aZLDTZMcbpoBlqAow/oWjkxuM2n9zGIrn+gmbetIM3EuHaUpp77uAsmq5A5roc4PP/7U/vX69fa58++49IWLl/I/hFl59IGFy3/fvOnKcVgz99KFL7/6Ok/3igGigiN6TCBHWQzEESWiR5105bSWncph0qy85sgfqkcMvSSddE3Tfw5LSitXJFSGMBFu2/g6fl3Z9CsT6Vmz1cUaFVmVk1CZxLbxdfy60anptXeYBzertbOcOHN2VYetviWrvj2HEd7gKGu3fXo2t7tx05a1cVivlyB6Ramo3cTc7vbw+GRu+8ChF0sd5kGNRTpZdXcOk7fPSezyP4na4bB1Dsoc9kU6WbZ7h0NlWdKJdOTwcy8s5/bpJmvisH2KpGWgklwnc9g6CJyAStfqwzynK6xckbIyv9yK6q3Dx0+fydtZajmMECkK/ce0POALNVBZqI1E9dZhsCegsA7bh4sWd6JkOoVijWZFTmw5+N0BUZ3vMNi2UNaHc0GxyOmlpaX7faNZlRO/zq9HVB5y2J6AUNlhi56H+eXrhWCU1jPJ4aZpbEl5U/RdhPuNvlw20W+kIEcgBTkCKcgRSEGOQApyBFKQI5CCHIEU5AikIEcgBTkCKcgRSEGOQApyBFKQI5CCHIEU5AikIEcgBTkCKcgRyIPMSt5LH33sVu/6+wUmbg8XZALsL22mjCXRLIUm+Fpszpfw0vET+d6K5KXDUmvKtHML67vZtJo0y6z58lg7jg5LsFlazWeQB3TQJc3ya8Cm7PKZ+MHacmyxUwybqKv9eiXvyfzBdvsbAqndvBUQ6REY6RJkG2ACw3p00gouRwJEO7482UGXL4JgURYKMmnso09gaf/b73+4csBmr7fV6TVpTI5ACnIEUpAj4ILsS9EmPj5sXZM16RAWZFo9t+9IiYTWFYcoshPaZkewTZT/xkERdkMjUWlxaTcSO8gIe/3aNiG6CTLwlgQ7RIUCa7njg4zwEgRbPtu2lm6DLOzbHSHuiiBL/B24VrNTNcgwO78YDDBUDXLV+Ig1HZOf3Dpyje33smyH6FWKKnZCQWYxv794X+hNmrsiyAqO3fPQyp49zz5Wx45Fb0zY3fosT42O3XJB7OVwYW2siyDDtp0TH2TFHcI4rVcBuwky8KaS3YxScEE8dvL03RNkYPjgVcWsOhfKqOs2yCL0hhUcPHK0VpAZ4vClDGsjGGSk7Covinqf/34yH5pVdUgoOPTaonF6enZ2b5bskKpBBrtLqWV+/4Fmx2QaZe07xP9GLEWnUehNtDpBFkXjdEjqBBmKLoghehZkem3RhqYSglfY7TOh3r5KLboJMhSdKb7UDTIUXRB9ijpTbZHBOr3HF16jss5Zug0yoBMap610E2QouiBaeh5kQW9kPoHTwu3fGxB+Bo/vmjkV6rk+BEHvs1mq/mdHPoP75pANCNlhc+W5xX2V4C7Dby+4voQ+sw4uhiHjid7hhtlQRaJ3pCBHIAU5Aq1Wa8O/TPDPJAlM/0sAAAAASUVORK5CYII="" >
                 <br><font color='#1F497D'> <b>Departamento de T.I. </b>
                 <br>Carretera Tula-Refinería Km. 3
                 <br>El Llano 1ra Sección, Tula de Allende, Hgo.
                 <br>C.P. 42820
                 <br>Tel. +52 (773) 7329204 </font>
                 <br>http://www.unne.com.mx
                 </span>" +
                 "<span style=""font-family:Tahoma;font-size: 8pt;color:#163C86;"">" +
                 "<br><br>" &
                 "Antes de imprimir, piense en su responsabilidad y compromiso con el </span>" + "<span style=""font-family:Tahoma;font-size: 8pt;color:#9FFF77;"">" + "MEDIO AMBIENTE </span>" &
                 "<span style=""font-family:Tahoma;font-size: 8pt;color:#163C86;"">" +
                 "<br><br>" &
                 "La información contenida en este correo, es de carácter privado y confidencial y solo se dirige exclusivamente a los " +
                 "destinatarios. Se encuentra estrictamente prohibido reproducir, alterar, archivar o comunicar a terceros el presente mensaje " +
                 "y ficheros anexos, todo ello bajo pena de incurrir en responsabilidades legales."

            Mensaje.Body = texto
            Mensaje.IsBodyHtml = True
            Mensaje.Priority = MailPriority.Normal

            Dim Servidor As New SmtpClient()
            ' Versión Anterior
            'Servidor.UseDefaultCredentials = False
            'Servidor.Credentials = New System.Net.NetworkCredential("soporte.unne.procad@gmail.com", "z*W52RG=R$f-4F73")
            'Servidor.Host = "smtp.gmail.com"
            'Servidor.Port = 587
            'Servidor.EnableSsl = True

            Servidor.Host = "10.10.10.30"
            Servidor.Port = 587
            Servidor.EnableSsl = False
            Servidor.UseDefaultCredentials = False
            Servidor.Credentials = New System.Net.NetworkCredential("nprocad", "mc8HLB8lPe78")

            Try
                If destinatario = "" Or nick = "" Or pass = "" Then
                    litError.Text = "Revise que se haya vinculado un correo, usuario o contraseña"
                Else
                    Servidor.Send(Mensaje)
                    Mensaje.Dispose()
                    litError.Text = "Credenciales Enviadas"
                End If

            Catch ex As System.Net.Mail.SmtpException
                litError.Text = ex.ToString
            End Try

        Catch ex As Exception
            litError.Text = "No se pueden enviar las credenciales, intentelo nuevamente"
        End Try

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
                                                           "     , perfil " +
                                                           "     , checador " +
                                                           "     , isnull(Emp.correo, '') as correoE " +
                                                           "     , isnull(pin, '') as pin " +
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
                                                           "     , isnull(Emp.id_gral, 0) as id_gral " +
                                                           "     , hospedaje_libre " +
                                                           "     , anticipo_obl " +
                                                           "     , edit_compro_datos " +
                                                            "    , isnull(movimientos_internos, 'N') as movimientos_internos " +
                                                           "     , isnull(cg_usuario.fecha_termino, 'N') as fecha_termino " +
                                                           "     , isnull(american_express, 'N') as american_express " +
                                                           "     , (select no_anticipos from cg_usuario_ant where id_usuario = @id_usuario and tipo = 'AMEX') as anticipo_AMEX " +
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
                .txtEmpleado.Text = ""
                llenarEmpleados(idRegistro)
                .ddlEmpleado.SelectedValue = Val(dsCatalogo.Tables(0).Rows(0).Item("id_empleado").ToString())
                ._txtIdGral.Text = dsCatalogo.Tables(0).Rows(0).Item("id_gral").ToString()
                usrUnificado()
                passUnificado()
                .ddlPerfil.SelectedValue = dsCatalogo.Tables(0).Rows(0).Item("perfil").ToString()
                .txtCorreoE.Text = dsCatalogo.Tables(0).Rows(0).Item("correoE").ToString()
                .txtPIN.Text = dsCatalogo.Tables(0).Rows(0).Item("pin").ToString()
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
                    cbFechaTermino.Checked = True
                Else
                    cbFechaTermino.Checked = False
                End If
                If dsCatalogo.Tables(0).Rows(0).Item("checador").ToString() = "S" Then
                    .cbChecador.Checked = True
                Else
                    .cbChecador.Checked = False
                End If
                If dsCatalogo.Tables(0).Rows(0).Item("american_express").ToString() = "S" Then
                    cbAmericanExpress.Checked = True
                    If Val(dsCatalogo.Tables(0).Rows(0).Item("anticipo_AMEX").ToString()) = 0 Then
                        txtAnticipoAmex.Text = 1
                    Else
                        txtAnticipoAmex.Text = Val(dsCatalogo.Tables(0).Rows(0).Item("anticipo_AMEX").ToString())
                    End If
                Else
                    cbAmericanExpress.Checked = False
                End If

                limpiarPantallaAut()
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
                .gvAutorizadores.Columns(0).Visible = True
                sdaAutorizadores.SelectCommand = New SqlCommand("select id_dt_autorizador " +
                                                                "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as autorizador " +
                                                                "     , case aut_dir when 'S' then 'Sí' else null end as director " +
                                                                "     , case validador when 'S' then 'Sí' else null end as validador " +
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
                .gvAutorizadores.Columns(0).Visible = False
                .gvAutorizadores.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#Region "Autorizadores"

    Public Sub ocultarBotones()
        With Me
            .pnlAutorizador.Visible = True
            .btnAceptar.Visible = False
            .btnReCredenciales.Visible = False
            .btnCancelar.Visible = False
        End With
    End Sub

    Public Sub habilitarCamposAut(ByVal valor)
        With Me
            .ddlUsuarioAut.Enabled = valor
            .txtUsuarioAutB.Enabled = valor
        End With
    End Sub

    Public Sub localizarAut(ByVal idDtAutorizador)
        With Me
            Try
                'Codigo Nuevo
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCodigo As New SqlDataAdapter
                Dim dsCodigo As New DataSet
                sdaCodigo.SelectCommand = New SqlCommand("select id_autorizador, aut_dir, cg_usuario.status , validador  " +
                                                         "from dt_autorizador " +
                                                         "  left join cg_usuario on dt_autorizador.id_autorizador = cg_usuario.id_usuario " +
                                                         "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                         "where id_dt_autorizador = @idDtAutorizador ", ConexionBD)
                sdaCodigo.SelectCommand.Parameters.AddWithValue("@idDtAutorizador", idDtAutorizador)
                ConexionBD.Open()
                sdaCodigo.Fill(dsCodigo)
                ConexionBD.Close()
                .cbValidador.Checked = False
                .cbDirector.Checked = False
                'Llenar Campos
                If dsCodigo.Tables(0).Rows(0).Item("status").ToString() = "A" Then
                    'Autorizador Activo
                    .txtUsuarioAutB.Text = ""
                    .ddlUsuarioAut.SelectedValue = Val(dsCodigo.Tables(0).Rows(0).Item("id_autorizador").ToString())
                    If dsCodigo.Tables(0).Rows(0).Item("aut_dir").ToString() = "S" Then
                        .cbDirector.Checked = True
                    Else
                        .cbDirector.Checked = False
                    End If
                    If dsCodigo.Tables(0).Rows(0).Item("validador").ToString() = "S" Then
                        .cbValidador.Checked = True
                    Else
                        .cbValidador.Checked = False
                    End If

                Else
                    'Autorizador dado de Baja - Eliminarlo y actualizar tabla
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "delete from dt_autorizador where id_dt_autorizador = @idDtAutorizador "
                    SCMValores.Parameters.AddWithValue("@idDtAutorizador", idDtAutorizador)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()
                    limpiarPantallaAut()
                End If
                sdaCodigo.Dispose()
                dsCodigo.Dispose()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub limpiarPantallaAut()
        With Me
            'Actualizar Lista de Autorizadores
            llenarAutorizadores(.gvUsuario.SelectedRow.Cells(0).Text)
            .pnlDetalle.Enabled = True
            .ibtnAltaAut.Enabled = True
            .ibtnBajaAut.Enabled = False
            .ibtnBajaAut.ImageUrl = "images\Trash_i2.png"
            .ibtnModifAut.Enabled = False
            .ibtnModifAut.ImageUrl = "images\Edit_i2.png"
            .pnlAutorizador.Visible = False
            .btnAceptar.Visible = True
            .btnReCredenciales.Visible = True
            .btnCancelar.Visible = True
        End With
    End Sub

    Public Function validarAut()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMTemp As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMTemp.Connection = ConexionBD
                Dim conteo As Integer = 0
                Dim query As String
                SCMTemp.CommandText = ""
                SCMTemp.Parameters.Clear()
                Select Case ._txtTipoMovA.Text 'Tipo de Ajuste (alta, baja o modificación)
                    Case "A"
                        query = "select count(*) " +
                                "from dt_autorizador " +
                                "where id_usuario = @id_usuario " +
                                "  and id_autorizador = @id_autorizador "
                        SCMTemp.CommandText = query
                    Case Else
                        query = "select count(*) " +
                                "from dt_autorizador " +
                                "where id_usuario = @id_usuario " +
                                "  and id_autorizador = @id_autorizador " +
                                "  and id_dt_autorizador <> @id_dt_autorizador "
                        SCMTemp.CommandText = query
                        SCMTemp.Parameters.AddWithValue("@id_dt_autorizador", Val(.gvAutorizadores.SelectedRow.Cells(0).Text))
                End Select
                SCMTemp.Parameters.AddWithValue("@id_usuario", .gvUsuario.SelectedRow.Cells(0).Text)
                SCMTemp.Parameters.AddWithValue("@id_autorizador", .ddlUsuarioAut.SelectedValue)
                ConexionBD.Open()
                conteo = SCMTemp.ExecuteScalar
                ConexionBD.Close()
                If conteo > 0 Then
                    validarAut = False
                Else
                    validarAut = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
                validarAut = False
            End Try
        End With
    End Function

    Protected Sub ibtnAltaAut_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAltaAut.Click
        With Me
            Try
                ._txtTipoMovA.Text = "A"
                .ddlUsuarioAut.SelectedIndex = -1
                .txtUsuarioAutB.Text = ""
                llenarAutorizadores()
                'Codigo Nuevo
                .cbDirector.Checked = False
                .cbValidador.Checked = False

                habilitarCamposAut(True)
                .pnlDetalle.Enabled = False
                ocultarBotones()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnBajaAut_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBajaAut.Click
        With Me
            Try
                If .gvAutorizadores.SelectedIndex > -1 Then
                    ._txtTipoMovA.Text = "B"
                    .txtUsuarioAutB.Text = ""
                    llenarAutorizadores()
                    localizarAut(.gvAutorizadores.SelectedRow.Cells(0).Text)
                    habilitarCamposAut(False)
                    .pnlDetalle.Enabled = False
                    ocultarBotones()
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnModifAut_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnModifAut.Click
        With Me
            Try
                If .gvAutorizadores.SelectedIndex > -1 Then
                    ._txtTipoMovA.Text = "M"
                    .txtUsuarioAutB.Text = ""
                    llenarAutorizadores()
                    localizarAut(.gvAutorizadores.SelectedRow.Cells(0).Text)
                    habilitarCamposAut(True)
                    .pnlDetalle.Enabled = False
                    ocultarBotones()
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub gvAutorizadores_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvAutorizadores.SelectedIndexChanged
        With Me
            .litError.Text = ""
            .ibtnBajaAut.Enabled = True
            .ibtnBajaAut.ImageUrl = "images\Trash.png"
            .ibtnModifAut.Enabled = True
            .ibtnModifAut.ImageUrl = "images\Edit.png"
        End With
    End Sub

    Protected Sub cmdBuscarUsrAut_Click(sender As Object, e As EventArgs) Handles cmdBuscarUsrAut.Click
        llenarAutorizadores()
    End Sub

    Protected Sub btnAceptarAut_Click(sender As Object, e As EventArgs) Handles btnAceptarAut.Click
        With Me
            Try
                .litError.Text = ""
                Dim ban As Integer = 0
                If .ddlUsuarioAut.Items.Count = 0 Then
                    .litError.Text = "Información Insuficiente, favor de elegir un autorizador"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    Select Case ._txtTipoMovA.Text 'Tipo de Ajuste (Agregar, Eliminar o Modificar)
                        Case "A"
                            If validarAut() Then
                                'Codigo Nuevo


                                SCMValores.CommandText = "insert into dt_autorizador (id_usuario,  id_autorizador,  aut_dir , validador ) values (@id_usuario, @id_autorizador, @aut_dir, @validador )"
                                SCMValores.Parameters.AddWithValue("@id_usuario", .gvUsuario.SelectedRow.Cells(0).Text)
                                SCMValores.Parameters.AddWithValue("@id_autorizador", .ddlUsuarioAut.SelectedValue)
                                If .cbDirector.Checked = True Then
                                    SCMValores.Parameters.AddWithValue("@aut_dir", "S")
                                Else
                                    SCMValores.Parameters.AddWithValue("@aut_dir", "N")
                                End If
                                If .cbValidador.Checked = True Then
                                    SCMValores.Parameters.AddWithValue("@validador", "S")
                                Else
                                    SCMValores.Parameters.AddWithValue("@validador", "N")
                                End If
                            Else
                                .litError.Text = "Valor Inválido, ya se asignó ese Autorizador "
                                ban = 1
                            End If
                        Case "B"
                            SCMValores.CommandText = "delete from dt_autorizador where id_dt_autorizador = @idDtAutorizador "
                            SCMValores.Parameters.AddWithValue("@idDtAutorizador", .gvAutorizadores.SelectedRow.Cells(0).Text)
                        Case Else
                            If validarAut() Then
                                SCMValores.CommandText = "update dt_autorizador SET id_autorizador = @id_autorizador, aut_dir = @aut_dir , validador = @validador  WHERE id_dt_autorizador = @id_dt_autorizador"
                                SCMValores.Parameters.AddWithValue("@id_autorizador", .ddlUsuarioAut.SelectedValue)
                                If .cbDirector.Checked = True Then
                                    SCMValores.Parameters.AddWithValue("@aut_dir", "S")
                                Else
                                    SCMValores.Parameters.AddWithValue("@aut_dir", "N")
                                End If
                                If .cbValidador.Checked = True Then
                                    SCMValores.Parameters.AddWithValue("@validador", "S")
                                Else
                                    SCMValores.Parameters.AddWithValue("@validador", "N")
                                End If
                                SCMValores.Parameters.AddWithValue("@id_dt_autorizador", .gvAutorizadores.SelectedRow.Cells(0).Text)
                            Else
                                .litError.Text = "Valor Inválido, ya se asignó ese Autorizador "
                                ban = 1
                            End If
                    End Select
                    If ban = 0 Then
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()
                        limpiarPantallaAut()
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnCancelarAut_Click(sender As Object, e As EventArgs) Handles btnCancelarAut.Click
        limpiarPantallaAut()
    End Sub

#End Region

    Public Sub idGralE()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select isnull((SELECT Emp.id_gral FROM bd_empleado.dbo.cg_empleado Emp WHERE id_empleado = @id_empleado),0) as id_gral"
                SCMValores.Parameters.AddWithValue("@id_empleado", .ddlEmpleado.SelectedValue)
                ConexionBD.Open()
                ._txtIdGral.Text = SCMValores.ExecuteScalar
                ConexionBD.Close()

                If Val(_txtIdGral.Text) = 0 Then
                    'Determinar si existe el registro en cg_gral
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "select isnull((SELECT Emp.id_gral FROM bd_empleado.dbo.cg_gral Emp WHERE no_empleado in (SELECT no_empleado as registro FROM bd_empleado.dbo.cg_empleado Emp WHERE id_empleado = @id_empleado)),0) as id_gral"
                    SCMValores.Parameters.AddWithValue("@id_empleado", .ddlEmpleado.SelectedValue)
                    ConexionBD.Open()
                    ._txtIdGral.Text = SCMValores.ExecuteScalar
                    ConexionBD.Close()

                    If Val(_txtIdGral.Text) = 0 Then
                        'No existe en la tabla cg_gral, por lo que hay que insertarlo
                        Dim sdaEmpleado As New SqlDataAdapter
                        Dim dsEmpleado As New DataSet
                        sdaEmpleado.SelectCommand = New SqlCommand("select no_empleado " +
                                                                   "     , nombre" +
                                                                   "     , ap_paterno " +
                                                                   "     , ap_materno " +
                                                                   "from bd_empleado.dbo.cg_empleado " +
                                                                   "where id_empleado = @id_empleado ", ConexionBD)
                        sdaEmpleado.SelectCommand.Parameters.AddWithValue("@id_empleado", .ddlEmpleado.SelectedValue)
                        ConexionBD.Open()
                        sdaEmpleado.Fill(dsEmpleado)
                        ConexionBD.Close()
                        'Insertar en cg_general
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "INSERT INTO bd_empleado.dbo.cg_gral(no_empleado, nombre, ap_paterno, ap_materno) values(@no_empleado, @nombre, @ap_paterno, @ap_materno)"
                        SCMValores.Parameters.AddWithValue("@no_empleado", dsEmpleado.Tables(0).Rows(0).Item("no_empleado").ToString())
                        SCMValores.Parameters.AddWithValue("@nombre", dsEmpleado.Tables(0).Rows(0).Item("nombre").ToString())
                        SCMValores.Parameters.AddWithValue("@ap_paterno", dsEmpleado.Tables(0).Rows(0).Item("ap_paterno").ToString())
                        SCMValores.Parameters.AddWithValue("@ap_materno", dsEmpleado.Tables(0).Rows(0).Item("ap_materno").ToString())
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()
                        'Obtener el id_gral
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "SELECT Emp.id_gral as idGral FROM bd_empleado.dbo.cg_gral Emp WHERE no_empleado = @no_empleado"
                        SCMValores.Parameters.AddWithValue("@no_empleado", dsEmpleado.Tables(0).Rows(0).Item("no_empleado").ToString())
                        ConexionBD.Open()
                        ._txtIdGral.Text = SCMValores.ExecuteScalar
                        ConexionBD.Close()

                        sdaEmpleado.Dispose()
                        dsEmpleado.Dispose()
                    End If

                    ' Actualizar la tabla cg_empleado con el id_gral
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "update bd_empleado.dbo.cg_empleado set id_gral = @idGral WHERE id_empleado = @id_empleado and status = 'A' "
                    SCMValores.Parameters.AddWithValue("@idGral", Val(._txtIdGral.Text))
                    SCMValores.Parameters.AddWithValue("@id_empleado", .ddlEmpleado.SelectedValue)
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub empleadoInf()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("SiCEm")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim sdaEmpleado As New SqlDataAdapter
                Dim dsEmpleado As New DataSet
                sdaEmpleado.SelectCommand = New SqlCommand(" SP_C_cg_usuario @id_gral ", ConexionBD)
                sdaEmpleado.SelectCommand.Parameters.AddWithValue("@id_gral", Val(._txtIdGral.Text))
                ConexionBD.Open()
                sdaEmpleado.Fill(dsEmpleado)
                ConexionBD.Close()

                If dsEmpleado.Tables(0).Rows.Count > 0 Then
                    .txtCorreoE.Text = dsEmpleado.Tables(0).Rows(0).Item("correo").ToString()
                    .txtUsuario.Text = dsEmpleado.Tables(0).Rows(0).Item("nick").ToString()
                    .txtContraseña.Text = dsEmpleado.Tables(0).Rows(0).Item("pass").ToString()
                Else
                    .txtCorreoE.Text = ""
                    .txtUsuario.Text = ""
                    .txtContraseña.Text = ""
                End If

                If .txtContraseña.Text = "" Then
                    'No existe contraseña, hay que generarla
                    Me.txtContraseñaOrg.Text = accessDB.RandomString()
                    Dim passSHA1 As String = accessDB.EncryptSHA1(.txtContraseñaOrg.Text)
                    Me.txtContraseña.Text = accessDB.Encrypt(passSHA1)
                End If
                sdaEmpleado.Dispose()
                dsEmpleado.Dispose()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub correoE()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "SELECT isnull(Emp.correo, '') as correoE FROM bd_empleado.dbo.cg_empleado Emp WHERE id_empleado = @id_empleado"
                SCMValores.Parameters.AddWithValue("@id_empleado", .ddlEmpleado.SelectedValue)
                ConexionBD.Open()
                .txtCorreoE.Text = SCMValores.ExecuteScalar
                ConexionBD.Close()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub usrUnificado()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                'Versión Anterior sin SiTA
                'SCMValores.CommandText = "select case " + _
                '                         "         when (select usrProcAd.nick from bd_ProcAd.dbo.cg_usuario usrProcAd where id_empleado = @id_empleado and status = 'A') is null " + _
                '                         "           then (isnull((select usrSiTAF.nick from bd_SiTAF.dbo.cg_usuario usrSiTAF where id_empleado = @id_empleado and status = 'A'),'')) " + _
                '                         "         else " + _
                '                         "           (select usrProcAd.nick from bd_ProcAd.dbo.cg_usuario usrProcAd where id_empleado = @id_empleado and status = 'A') " + _
                '                         "       end as nick "
                'Versión Anterior sin SiTTe
                'SCMValores.CommandText = "select case " + _
                '                         "         when (select usrProcAd.nick from bd_ProcAd.dbo.cg_usuario usrProcAd where id_empleado = @id_empleado and status = 'A') is null " + _
                '                         "           then case " + _
                '                         "                  when (select usrSiTAF.nick from bd_SiTAF.dbo.cg_usuario usrSiTAF where id_empleado = @id_empleado and status = 'A') is null " + _
                '                         "                    then (isnull((select usrSiTA.nick from bd_SiTA.dbo.cg_usuario usrSiTA where id_empleado = @id_empleado and status = 'A'),'')) " + _
                '                         "                  else " + _
                '                         "                    (select usrSiTAF.nick from bd_SiTAF.dbo.cg_usuario usrSiTAF where id_empleado = @id_empleado and status = 'A') " + _
                '                         "                end " + _
                '                         "         else " + _
                '                         "           (select usrProcAd.nick from bd_ProcAd.dbo.cg_usuario usrProcAd where id_empleado = @id_empleado and status = 'A') " + _
                '                         "       end as nick "
                'Versión Anterior sin SiSeg
                'SCMValores.CommandText = "select case " + _
                '                         "         when (select usrProcAd.nick from bd_ProcAd.dbo.cg_usuario usrProcAd where id_empleado = @id_empleado and status = 'A') is null " + _
                '                         "           then case " + _
                '                         "                  when (select usrSiTAF.nick from bd_SiTAF.dbo.cg_usuario usrSiTAF where id_empleado = @id_empleado and status = 'A') is null " + _
                '                         "                    then case " + _
                '                         "                           when (select usrSiTTe.nick from bd_SiTTe.dbo.cg_usuario usrSiTTe where id_empleado = @id_empleado and status = 'A') is null " + _
                '                         "                              then (isnull((select usrSiTA.nick from bd_SiTA.dbo.cg_usuario usrSiTA where id_empleado = @id_empleado and status = 'A'),'')) " + _
                '                         "                         else " + _
                '                         "                           (select usrSiTTe.nick from bd_SiTTe.dbo.cg_usuario usrSiTTe where id_empleado = @id_empleado and status = 'A') " + _
                '                         "                         end " + _
                '                         "                  else " + _
                '                         "                    (select usrSiTAF.nick from bd_SiTAF.dbo.cg_usuario usrSiTAF where id_empleado = @id_empleado and status = 'A') " + _
                '                         "                end " + _
                '                         "         else " + _
                '                         "           (select usrProcAd.nick from bd_ProcAd.dbo.cg_usuario usrProcAd where id_empleado = @id_empleado and status = 'A') " + _
                '                         "       end as nick "
                'Versión Anterior previa a pass encriptado
                'SCMValores.CommandText = "select case " + _
                '                         "         when (select usrProcAd.nick from bd_ProcAd.dbo.cg_usuario usrProcAd where id_empleado = @id_empleado and status = 'A') is null " + _
                '                         "           then case " + _
                '                         "                  when (select usrSiTAF.nick from bd_SiTAF.dbo.cg_usuario usrSiTAF where id_empleado = @id_empleado and status = 'A') is null " + _
                '                         "                    then case " + _
                '                         "                           when (select usrSiSeg.nick from bd_SiSeg.dbo.cg_usuario usrSiSeg where id_empleado = @id_empleado and status = 'A') is null " + _
                '                         "                             then case " + _
                '                         "                                    when (select usrSiTTe.nick from bd_SiTTe.dbo.cg_usuario usrSiTTe where id_empleado = @id_empleado and status = 'A') is null " + _
                '                         "                              then (isnull((select usrSiTA.nick from bd_SiTA.dbo.cg_usuario usrSiTA where id_empleado = @id_empleado and status = 'A'),'')) " + _
                '                         "                                  else " + _
                '                         "                                    (select usrSiTTe.nick from bd_SiTTe.dbo.cg_usuario usrSiTTe where id_empleado = @id_empleado and status = 'A') " + _
                '                         "                                  end " + _
                '                         "                           else " + _
                '                         "                              (select usrSiSeg.nick from bd_SiSeg.dbo.cg_usuario usrSiSeg where id_empleado = @id_empleado and status = 'A') " + _
                '                         "                         end " + _
                '                         "                  else " + _
                '                         "                    (select usrSiTAF.nick from bd_SiTAF.dbo.cg_usuario usrSiTAF where id_empleado = @id_empleado and status = 'A') " + _
                '                         "                end " + _
                '                         "         else " + _
                '                         "           (select usrProcAd.nick from bd_ProcAd.dbo.cg_usuario usrProcAd where id_empleado = @id_empleado and status = 'A') " + _
                '                         "       end as nick "
                'SCMValores.Parameters.AddWithValue("@id_empleado", .ddlEmpleado.SelectedValue)
                'Versión Actual
                SCMValores.CommandText = "select isnull((select top 1 nick " +
                                         "               from (select cg_usuario.nick,  1 as ind from bd_ProcAd.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @idGral and cg_usuario.status = 'A' " +
                                         "                     union " +
                                         "                     select cg_usuario.nick,  2 as ind from bd_SiLi.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @idGral and cg_usuario.status = 'A' " +
                                         "                     union " +
                                         "                     select cg_usuario.nick,  3 as ind from bd_SiCa.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @idGral and cg_usuario.status = 'A' " +
                                         "                     union " +
                                         "                     select cg_usuario.nick,  4 as ind from bd_EfEx.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @idGral and cg_usuario.status = 'A' " +
                                         "                     union " +
                                         "                     select cg_usuario.nick,  5 as ind from bd_SiSAC.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @idGral and cg_usuario.status = 'A' " +
                                         "                     union " +
                                         "                     select cg_usuario.nick,  6 as ind from bd_SiTAF.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @idGral and cg_usuario.status = 'A' " +
                                         "                     union " +
                                         "                     select cg_usuario.nick,  7 as ind from bd_SiTA.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @idGral and cg_usuario.status = 'A' " +
                                         "                     union " +
                                         "                     select cg_usuario.nick,  8 as ind from bd_SiTTe.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @idGral and cg_usuario.status = 'A' " +
                                         "                     union " +
                                         "                     select cg_usuario.nick,  9 as ind from bd_SiSeg.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @idGral and cg_usuario.status = 'A' " +
                                         "                     union " +
                                         "                     select cg_usuario.nick, 10 as ind from bd_SiRAc.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @idGral and cg_usuario.status = 'A' " +
                                         "                     union " +
                                         "                     select cg_usuario.nick, 11 as ind from bd_Empleado.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @idGral and cg_usuario.status = 'A' " +
                                         "                     ) usuarios), '') as nick "

                SCMValores.Parameters.AddWithValue("@idGral", Val(._txtIdGral.Text.Trim))
                ConexionBD.Open()
                .txtUsuario.Text = SCMValores.ExecuteScalar
                ConexionBD.Close()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub passUnificado()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                'Versión Anterior sin SiTA
                'SCMValores.CommandText = "select case " + _
                '                         "         when (select usrProcAd.pass from bd_ProcAd.dbo.cg_usuario usrProcAd where id_empleado = @id_empleado and status = 'A') is null " + _
                '                         "           then (isnull((select usrSiTAF.pass from bd_SiTAF.dbo.cg_usuario usrSiTAF where id_empleado = @id_empleado and status = 'A'),'')) " + _
                '                         "         else " + _
                '                         "           (select usrProcAd.pass from bd_ProcAd.dbo.cg_usuario usrProcAd where id_empleado = @id_empleado and status = 'A') " + _
                '                         "       end as pass "
                'Versión Anterior sin SiTTe
                'SCMValores.CommandText = "select case " + _
                '                         "         when (select usrProcAd.pass from bd_ProcAd.dbo.cg_usuario usrProcAd where id_empleado = @id_empleado and status = 'A') is null " + _
                '                         "           then case " + _
                '                         "                  when (select usrSiTAF.pass from bd_SiTAF.dbo.cg_usuario usrSiTAF where id_empleado = @id_empleado and status = 'A') is null " + _
                '                         "                    then (isnull((select usrSiTA.pass from bd_SiTA.dbo.cg_usuario usrSiTA where id_empleado = @id_empleado and status = 'A'),'')) " + _
                '                         "                  else " + _
                '                         "                    (select usrSiTAF.pass from bd_SiTAF.dbo.cg_usuario usrSiTAF where id_empleado = @id_empleado and status = 'A') " + _
                '                         "                end " + _
                '                         "         else " + _
                '                         "           (select usrProcAd.pass from bd_ProcAd.dbo.cg_usuario usrProcAd where id_empleado = @id_empleado and status = 'A') " + _
                '                         "       end as pass "
                'Versión Anterior sin SiSeg
                'SCMValores.CommandText = "select case " + _
                '         "         when (select usrProcAd.pass from bd_ProcAd.dbo.cg_usuario usrProcAd where id_empleado = @id_empleado and status = 'A') is null " + _
                '         "           then case " + _
                '         "                  when (select usrSiTAF.pass from bd_SiTAF.dbo.cg_usuario usrSiTAF where id_empleado = @id_empleado and status = 'A') is null " + _
                '         "                    then case " + _
                '         "                           when (select usrSiTTe.pass from bd_SiTTe.dbo.cg_usuario usrSiTTe where id_empleado = @id_empleado and status = 'A') is null " + _
                '         "                             then (isnull((select usrSiTA.pass from bd_SiTA.dbo.cg_usuario usrSiTA where id_empleado = @id_empleado and status = 'A'),'')) " + _
                '         "                           else " + _
                '         "                             (select usrSiTTe.pass from bd_SiTTe.dbo.cg_usuario usrSiTTe where id_empleado = @id_empleado and status = 'A') " + _
                '         "                         end " + _
                '         "                  else " + _
                '         "                    (select usrSiTAF.pass from bd_SiTAF.dbo.cg_usuario usrSiTAF where id_empleado = @id_empleado and status = 'A') " + _
                '         "                end " + _
                '         "         else " + _
                '         "           (select usrProcAd.pass from bd_ProcAd.dbo.cg_usuario usrProcAd where id_empleado = @id_empleado and status = 'A') " + _
                '         "       end as pass "
                'Versión Anterior previa a pass encriptado
                'SCMValores.CommandText = "select case " + _
                '                         "         when (select usrProcAd.pass from bd_ProcAd.dbo.cg_usuario usrProcAd where id_empleado = @id_empleado and status = 'A') is null " + _
                '                         "           then case " + _
                '                         "                  when (select usrSiTAF.pass from bd_SiTAF.dbo.cg_usuario usrSiTAF where id_empleado = @id_empleado and status = 'A') is null " + _
                '                         "                    then case " + _
                '                         "                           when (select usrSiSeg.pass from bd_SiSeg.dbo.cg_usuario usrSiSeg where id_empleado = @id_empleado and status = 'A') is null " + _
                '                         "                             then case " + _
                '                         "                                    when (select usrSiTTe.pass from bd_SiTTe.dbo.cg_usuario usrSiTTe where id_empleado = @id_empleado and status = 'A') is null " + _
                '                         "                                      then (isnull((select usrSiTA.pass from bd_SiTA.dbo.cg_usuario usrSiTA where id_empleado = @id_empleado and status = 'A'),'')) " + _
                '                         "                                    else " + _
                '                         "                                      (select usrSiTTe.pass from bd_SiTTe.dbo.cg_usuario usrSiTTe where id_empleado = @id_empleado and status = 'A') " + _
                '                         "                                  end " + _
                '                         "                           else " + _
                '                         "                             (select usrSiSeg.pass from bd_SiSeg.dbo.cg_usuario usrSiSeg where id_empleado = @id_empleado and status = 'A') " + _
                '                         "                         end " + _
                '                         "                  else " + _
                '                         "                    (select usrSiTAF.pass from bd_SiTAF.dbo.cg_usuario usrSiTAF where id_empleado = @id_empleado and status = 'A') " + _
                '                         "                end " + _
                '                         "         else " + _
                '                         "           (select usrProcAd.pass from bd_ProcAd.dbo.cg_usuario usrProcAd where id_empleado = @id_empleado and status = 'A') " + _
                '                         "       end as pass "
                'SCMValores.Parameters.AddWithValue("@id_empleado", .ddlEmpleado.SelectedValue)
                'Versión Actual
                SCMValores.CommandText = "select isnull((select top 1 pass " +
                                         "               from (select cg_usuario.pass,  1 as ind from bd_ProcAd.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @idGral and cg_usuario.status = 'A' " +
                                         "                     union " +
                                         "                     select cg_usuario.pass,  2 as ind from bd_SiLi.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @idGral and cg_usuario.status = 'A' " +
                                         "                     union " +
                                         "                     select cg_usuario.pass,  3 as ind from bd_SiCa.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @idGral and cg_usuario.status = 'A' " +
                                         "                     union " +
                                         "                     select cg_usuario.pass,  4 as ind from bd_EfEx.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @idGral and cg_usuario.status = 'A' " +
                                         "                     union " +
                                         "                     select cg_usuario.pass,  5 as ind from bd_SiSAC.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @idGral and cg_usuario.status = 'A' " +
                                         "                     union " +
                                         "                     select cg_usuario.pass,  6 as ind from bd_SiTAF.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @idGral and cg_usuario.status = 'A' " +
                                         "                     union " +
                                         "                     select cg_usuario.pass,  7 as ind from bd_SiTA.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @idGral and cg_usuario.status = 'A' " +
                                         "                     union " +
                                         "                     select cg_usuario.pass,  8 as ind from bd_SiTTe.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @idGral and cg_usuario.status = 'A' " +
                                         "                     union " +
                                         "                     select cg_usuario.pass,  9 as ind from bd_SiSeg.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @idGral and cg_usuario.status = 'A' " +
                                         "                     union " +
                                         "                     select cg_usuario.pass, 10 as ind from bd_SiRAc.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @idGral and cg_usuario.status = 'A' " +
                                         "                     union " +
                                         "                     select cg_usuario.pass, 11 as ind from bd_Empleado.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @idGral and cg_usuario.status = 'A' " +
                                         "                     ) usuarios), '') as pass "

                SCMValores.Parameters.AddWithValue("@idGral", Val(._txtIdGral.Text.Trim))
                ConexionBD.Open()
                .txtContraseña.Text = SCMValores.ExecuteScalar
                ConexionBD.Close()

                If .txtContraseña.Text = "" Then
                    'No existe contraseña, hay que generarla
                    Me.txtContraseñaOrg.Text = accessDB.RandomString()
                    Dim passSHA1 As String = accessDB.EncryptSHA1(.txtContraseñaOrg.Text)
                    Me.txtContraseña.Text = accessDB.Encrypt(passSHA1)
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Function generarPIN()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMPIN As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMPIN.Connection = ConexionBD
                Dim conteo As Integer = 0
                SCMPIN.CommandText = ""
                SCMPIN.Parameters.Clear()
                SCMPIN.CommandText = "select top 1 FLOOR(RAND()*(9999-1000+1)+1000) as valor " +
                                     "from cg_usuario " +
                                     "where FLOOR(RAND()*(9999-1000+1)+1000) not in (select pin from cg_usuario) "
                ConexionBD.Open()
                generarPIN = SCMPIN.ExecuteScalar
                ConexionBD.Close()
            Catch ex As Exception
                .litError.Text = ex.ToString
                generarPIN = 0
            End Try
        End With
    End Function

    Function validar()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMTemp As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMTemp.Connection = ConexionBD
                Dim conteo As Integer = 0
                SCMTemp.CommandText = ""
                SCMTemp.Parameters.Clear()
                Select Case ._txtTipoMov.Text 'Tipo de Ajuste (alta, baja o modificación)
                    Case "A"
                        SCMTemp.CommandText = "SELECT COUNT(*) FROM cg_usuario WHERE (nick = @nick OR id_empleado = @id_empleado) AND status = 'A'"
                    Case Else
                        SCMTemp.CommandText = "SELECT COUNT(*) FROM cg_usuario WHERE (nick = @nick OR id_empleado = @id_empleado) AND id_usuario <> @id_usuario AND status = 'A'"
                        SCMTemp.Parameters.AddWithValue("@id_usuario", .gvUsuario.SelectedRow.Cells(0).Text)
                End Select
                SCMTemp.Parameters.AddWithValue("@nick", .txtUsuario.Text)
                SCMTemp.Parameters.AddWithValue("@id_empleado", .ddlEmpleado.SelectedValue)
                ConexionBD.Open()
                conteo = SCMTemp.ExecuteScalar
                ConexionBD.Close()
                If conteo > 0 Then
                    validar = False
                Else
                    validar = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
                validar = False
            End Try
        End With
    End Function

    Public Sub accesosUsuario()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("bd_SiRAc")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                Dim msj As String = ""


                sdaCatalogo.SelectCommand = New SqlCommand("SELECT base, nick FROM(SELECT " +
                                                           "(SELECT TOP 1 cg_usuario.nick from bd_ProcAd.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @id_gralProcAd and cg_usuario.status = 'A' ) AS [ProcAd], " +
                                                           "(SELECT TOP 1 cg_usuario.nick from bd_SiLi.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @id_gralSili and cg_usuario.status = 'A') AS [SiLi]," +
                                                           "(SELECT TOP 1 cg_usuario.nick from bd_SiCa.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @id_gralSica and cg_usuario.status = 'A' ) AS [SiCa]," +
                                                           "(SELECT TOP 1 cg_usuario.nick from bd_EfEx.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @id_gralEfex and cg_usuario.status = 'A') AS [EfEx]," +
                                                           "(SELECT TOP 1 cg_usuario.nick from bd_SiSAC.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @id_gralSisac and cg_usuario.status = 'A') AS [SiSAC]," +
                                                           "(SELECT TOP 1 cg_usuario.nick from bd_SiTAF.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @id_gralSitaf and cg_usuario.status = 'A') AS [SiTAF]," +
                                                           "(SELECT TOP 1 cg_usuario.nick from bd_SiTA.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @id_gralSita and cg_usuario.status = 'A') AS [SiTA]," +
                                                           "(SELECT TOP 1 cg_usuario.nick from bd_SiTTe.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @id_gralSitte and cg_usuario.status = 'A') AS [SiTTe]," +
                                                           "(SELECT TOP 1 cg_usuario.nick from bd_SiSeg.dbo.cg_usuario left join bd_Empleado.dbo.cg_empleado on cg_usuario.id_empleado = cg_empleado.id_empleado where id_gral = @id_gralSiseg and cg_usuario.status = 'A') AS [SiSeg]) p " +
                                                           "UNPIVOT " +
                                                           "(nick FOR base IN ([ProcAd],[SiLi],[SiCa],[EfEx],[SiSAC], [SiTAF],[SiTA],[SiTTe],[SiSeg])) AS unpvt ", ConexionBD)

                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_gralProcAd", Val(._txtIdGral.Text.Trim))
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_gralSili", Val(._txtIdGral.Text.Trim))
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_gralSica", Val(._txtIdGral.Text.Trim))
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_gralEfex", Val(._txtIdGral.Text.Trim))
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_gralSisac", Val(._txtIdGral.Text.Trim))
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_gralSitaf", Val(._txtIdGral.Text.Trim))
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_gralSita", Val(._txtIdGral.Text.Trim))
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_gralSitte", Val(._txtIdGral.Text.Trim))
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_gralSiseg", Val(._txtIdGral.Text.Trim))
                'sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_gralEmpleado", Val(._txtIdGral.Text.Trim))



                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()

                For Each Row As DataRow In dsCatalogo.Tables(0).Rows()
                    ' msj = msj + vbCrLf + Row(0) + "  " + " - " + "   "
                    'msj = msj + "<br> " + Row(0) + "</br>"
                    msj = msj + Row(0) + "<br>"
                Next

                txtAccesos.Text = msj


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
                .ibtnBaja.Enabled = True
                .ibtnBaja.ImageUrl = "images\Trash.png"
                .ibtnModif.Enabled = True
                .ibtnModif.ImageUrl = "images\Edit.png"
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Botones Inicio"

    Protected Sub ibtnAlta_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAlta.Click
        With Me
            Try
                ._txtTipoMov.Text = "A"
                .lblTipoMov.Text = "Alta"
                bloqueoPantalla()
                .txtEmpleado.Text = ""
                llenarEmpleados(0)
                idGralE()
                correoE()
                '.txtUsuario.Text = ""
                usrUnificado()
                '.txtContraseña.Text = ""
                passUnificado()
                .ddlPerfil.Visible = True
                .ddlPerfil.SelectedIndex = -1
                .txtPIN.Text = ""
                llenarAutorizadores(0)

                .cbAntXEmpr.Checked = False
                .txtAntXEmpr.Text = ""
                cbAmericanExpress.Checked = False
                txtAnticipoAmex.Text = ""
                .cbAntPend.Checked = False
                .cbTransporte.Checked = False
                .cbLimAutDir.Checked = False
                .txtLimAutDir.Text = ""
                .cbUsrAlim.Checked = False
                .cbUnidadComp.Checked = False
                .cbUsrPagoEfect.Checked = False
                .cbUsrCotUnica.Checked = False
                .cbUsrFactEmiPrev.Checked = False
                .cbUsrFactExtemp.Checked = False
                .cbUsrFactExtempComp.Checked = False
                .cbLider.Checked = False
                .cbOmitirPGV.Checked = False
                .cbAlimentosTab.Checked = False
                .cbTaxiTab.Checked = False
                .cbFechaTermino.Checked = False
                .cbIngresarNocheHospedaje.Checked = False
                .cbOmitirValidacionAnt.Checked = False
                .cbDatosComprobacion.Checked = False

                .ibtnAltaAut.Visible = False
                .ibtnBajaAut.Visible = False
                .ibtnModifAut.Visible = False
                .btnReCredenciales.Visible = False
                .pnlAutorizador.Visible = False
                While .txtPIN.Text = ""
                    .txtPIN.Text = generarPIN()
                End While
                habilitarCampos(True)

                .txtAntXEmpr.Enabled = False
                .txtLimAutDir.Enabled = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnBaja_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBaja.Click
        With Me
            Try
                ._txtTipoMov.Text = "B"
                .lblTipoMov.Text = "Baja"
                localizar(.gvUsuario.SelectedRow.Cells(0).Text)
                .ibtnAltaAut.Visible = True
                .ibtnBajaAut.Visible = True
                .ibtnModifAut.Visible = True
                .btnReCredenciales.Visible = False
                .pnlAutorizador.Visible = False
                habilitarCampos(False)
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnModif_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnModif.Click
        With Me
            Try
                ._txtTipoMov.Text = "M"
                .lblTipoMov.Text = "Modificación"
                localizar(.gvUsuario.SelectedRow.Cells(0).Text)
                .ibtnAltaAut.Visible = True
                .ibtnBajaAut.Visible = True
                .ibtnModifAut.Visible = True
                .btnReCredenciales.Visible = True
                .pnlAutorizador.Visible = False
                habilitarCampos(True)
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ddlEmpleado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEmpleado.SelectedIndexChanged
        idGralE()
        empleadoInf()
        'usrUnificado()
        'passUnificado()
        'correoE()
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

    Protected Sub cbAmericanExpress_CheckedChanged(sender As Object, e As EventArgs) Handles cbAmericanExpress.CheckedChanged
        Try
            If cbAmericanExpress.Checked = True Then
                cbAmericanExpress.Checked = True
                txtAnticipoAmex.Enabled = True
            Else
                cbAmericanExpress.Checked = False
                txtAnticipoAmex.Enabled = False
            End If
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
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
                If .txtUsuario.Text.Trim = "" Or .txtContraseña.Text.Trim = "" Or .txtCorreoE.Text.Trim = "" Then
                    .litError.Text = "Información Insuficiente, favor de verificar"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    Select Case ._txtTipoMov.Text 'Tipo de Ajuste (alta, baja o modificación)
                        Case "A"
                            If validar() Then
                                'SCMValores.CommandText = "INSERT INTO cg_usuario (id_empleado, nick, pass, perfil, pin, ant_pendientes, limite_aut_dir, cotizacion_unica, factura_extemp, factura_emi_prev, pago_efectivo, unidad_comp, transporte, lider, omitir_PGV, alimentos_tab, taxi_tab, factura_extemp_comp, fecha_termino) values (@id_empleado, @nick, @pass, @perfil, @pin, @ant_pendientes, @limite_aut_dir, @cotizacion_unica, @factura_extemp, @factura_emi_prev, @pago_efectivo, @unidad_comp, @transporte, @lider, @omitir_PGV, @alimentos_tab, @taxi_tab , @hospedaje_libre , @factura_extemp_comp , @anticipo_obl ,@edit_compro_datos, @fecha_termino)"

                                SCMValores.CommandText = "INSERT INTO cg_usuario (id_empleado, nick, pass, perfil, pin, ant_pendientes, limite_aut_dir, cotizacion_unica, factura_extemp, factura_emi_prev, pago_efectivo, unidad_comp, transporte, lider, omitir_PGV, alimentos_tab, taxi_tab , hospedaje_libre , factura_extemp_comp , anticipo_obl ,edit_compro_datos, fecha_termino, movimientos_internos, american_express) values(@id_empleado, @nick, @pass, @perfil, @pin, @ant_pendientes, @limite_aut_dir, @cotizacion_unica, @factura_extemp, @factura_emi_prev, @pago_efectivo, @unidad_comp, @transporte, @lider, @omitir_PGV, @alimentos_tab, @taxi_tab , @hospedaje_libre , @factura_extemp_comp , @anticipo_obl ,@edit_compro_datos, @fecha_termino, @movimientos_internos, @american_express)"

                                SCMValores.CommandText = "INSERT INTO cg_usuario (id_empleado, nick, pass, perfil, pin, ant_pendientes, limite_aut_dir, cotizacion_unica, factura_extemp, factura_emi_prev, pago_efectivo, unidad_comp, transporte, lider, omitir_PGV, alimentos_tab, taxi_tab , hospedaje_libre , factura_extemp_comp , anticipo_obl ,edit_compro_datos, fecha_termino, checador, american_express) values(@id_empleado, @nick, @pass, @perfil, @pin, @ant_pendientes, @limite_aut_dir, @cotizacion_unica, @factura_extemp, @factura_emi_prev, @pago_efectivo, @unidad_comp, @transporte, @lider, @omitir_PGV, @alimentos_tab, @taxi_tab , @hospedaje_libre , @factura_extemp_comp , @anticipo_obl ,@edit_compro_datos, @fecha_termino, @checador, @american_express)"

                            Else
                                .litError.Text = "Valor Inválido, ya existe ese Usuario"
                                ban = 1
                            End If
                        Case "B"
                            SCMValores.CommandText = "UPDATE cg_usuario SET status = 'B' WHERE id_usuario = @id_usuario"
                            SCMValores.Parameters.AddWithValue("@id_usuario", .gvUsuario.SelectedRow.Cells(0).Text)
                        Case Else
                            If validar() Then

                                SCMValores.CommandText = "UPDATE cg_usuario SET id_empleado = @id_empleado, perfil = @perfil, pin = @pin, ant_pendientes = @ant_pendientes, limite_aut_dir = @limite_aut_dir, cotizacion_unica = @cotizacion_unica, factura_extemp = @factura_extemp, factura_emi_prev = @factura_emi_prev, pago_efectivo = @pago_efectivo, unidad_comp = @unidad_comp, transporte = @transporte, lider = @lider, omitir_PGV = @omitir_PGV, alimentos_tab = @alimentos_tab, taxi_tab = @taxi_tab , hospedaje_libre=@hospedaje_libre ,factura_extemp_comp = @factura_extemp_comp, anticipo_obl = @anticipo_obl , edit_compro_datos = @edit_compro_datos, fecha_termino = @fecha_termino, movimientos_internos = @movimientos_internos, american_express = @american_express  WHERE id_usuario = @id_usuario"

                                SCMValores.CommandText = "UPDATE cg_usuario SET id_empleado = @id_empleado, perfil = @perfil, pin = @pin, ant_pendientes = @ant_pendientes, limite_aut_dir = @limite_aut_dir, cotizacion_unica = @cotizacion_unica, factura_extemp = @factura_extemp, factura_emi_prev = @factura_emi_prev, pago_efectivo = @pago_efectivo, unidad_comp = @unidad_comp, transporte = @transporte, lider = @lider, omitir_PGV = @omitir_PGV, alimentos_tab = @alimentos_tab, taxi_tab = @taxi_tab , hospedaje_libre=@hospedaje_libre ,factura_extemp_comp = @factura_extemp_comp, anticipo_obl = @anticipo_obl , edit_compro_datos = @edit_compro_datos, fecha_termino = @fecha_termino, checador = @checador, american_express = @american_express WHERE id_usuario = @id_usuario"

                                SCMValores.Parameters.AddWithValue("@id_usuario", .gvUsuario.SelectedRow.Cells(0).Text)
                            Else
                                .litError.Text = "Valor Inválido, ya existe ese Usuario"
                                ban = 1
                            End If
                    End Select
                    If ban = 0 Then
                        SCMValores.Parameters.AddWithValue("@id_empleado", .ddlEmpleado.SelectedValue)
                        SCMValores.Parameters.AddWithValue("@nick", .txtUsuario.Text)
                        SCMValores.Parameters.AddWithValue("@pass", .txtContraseña.Text.Trim)
                        SCMValores.Parameters.AddWithValue("@perfil", .ddlPerfil.SelectedValue)
                        SCMValores.Parameters.AddWithValue("@pin", .txtPIN.Text)
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

                        If cbChecador.Checked = True Then
                            SCMValores.Parameters.AddWithValue("@checador", "S")
                        Else
                            SCMValores.Parameters.AddWithValue("@checador", "N")
                        End If

                        If cbAmericanExpress.Checked = True Then
                            SCMValores.Parameters.AddWithValue("@american_express", "S")
                        Else
                            SCMValores.Parameters.AddWithValue("@american_express", "N")
                        End If


                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        If ._txtTipoMov.Text = "A" Or ._txtTipoMov.Text = "M" Then
                            'Obtener el id_usuario
                            Dim idUsr As Integer
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "select isnull(max(id_usuario), 0) from cg_usuario WHERE id_empleado = @id_empleado and status = 'A' "
                            SCMValores.Parameters.AddWithValue("@id_empleado", .ddlEmpleado.SelectedValue)
                            ConexionBD.Open()
                            idUsr = SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            'Anticipos por Empresa
                            Dim cont As Integer
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "select Count(*) from cg_usuario_ant where id_usuario = @id_usuario and tipo is null "
                            SCMValores.Parameters.AddWithValue("@id_usuario", idUsr)
                            ConexionBD.Open()
                            cont = SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            If .cbAntXEmpr.Checked = True Then
                                If cont > 0 Then
                                    SCMValores.CommandText = "update cg_usuario_ant set no_anticipos = @no_anticipos where id_usuario = @id_usuario "
                                Else
                                    SCMValores.CommandText = "insert into cg_usuario_ant (id_usuario, no_anticipos, tipo) values (@id_usuario, @no_anticipos, null) "
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

                            'Anticipos AMEX'
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "select no_anticipos from cg_usuario_ant where id_usuario = @id_usuario and tipo = 'AMEX' "
                            SCMValores.Parameters.AddWithValue("@id_usuario", idUsr)
                            ConexionBD.Open()
                            cont = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If cbAmericanExpress.Checked = True Then
                                If cont > 0 Then
                                    SCMValores.CommandText = "update cg_usuario_ant set no_anticipos = @no_anticipos where id_usuario = @id_usuario and tipo = 'AMEX'"
                                Else
                                    SCMValores.CommandText = "insert into cg_usuario_ant (id_usuario, no_anticipos, tipo) values (@id_usuario, @no_anticipos, 'AMEX') "
                                End If
                                SCMValores.Parameters.AddWithValue("@no_anticipos", Val(txtAnticipoAmex.Text))
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()
                            Else
                                If cont > 0 Then
                                    SCMValores.CommandText = "delete from cg_usuario_ant where id_usuario = @id_usuario and tipo = 'AMEX' "
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

                            actualizarPass()
                        End If

                        If ._txtTipoMov.Text = "A" Then
                            'Validar si se ha enviado previamente la contraseña
                            Dim contPass As Integer
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "select sum(no_registros) as no_usuarios " +
                                                     "from (select count(*) as no_registros from bd_ProcAd.dbo.cg_usuario WHERE id_empleado in (select id_empleado from bd_Empleado.dbo.cg_empleado where id_gral = @id_gral) and status = 'A' " +
                                                     "      union all " +
                                                     "      select count(*) as no_registros from bd_SiLi.dbo.cg_usuario WHERE id_empleado in (select id_empleado from bd_Empleado.dbo.cg_empleado where id_gral = @id_gral) and status = 'A' " +
                                                     "      union all " +
                                                     "      select count(*) as no_registros from bd_SiCa.dbo.cg_usuario WHERE id_empleado in (select id_empleado from bd_Empleado.dbo.cg_empleado where id_gral = @id_gral) and status = 'A' " +
                                                     "      union all " +
                                                     "      select count(*) as no_registros from bd_EfEx.dbo.cg_usuario WHERE id_empleado in (select id_empleado from bd_Empleado.dbo.cg_empleado where id_gral = @id_gral) and status = 'A' " +
                                                     "      union all " +
                                                     "      select count(*) as no_registros from bd_SiSAC.dbo.cg_usuario WHERE id_empleado in (select id_empleado from bd_Empleado.dbo.cg_empleado where id_gral = @id_gral) and status = 'A' " +
                                                     "      union all " +
                                                     "      select count(*) as no_registros from bd_SiTA.dbo.cg_usuario WHERE id_empleado in (select id_empleado from bd_Empleado.dbo.cg_empleado where id_gral = @id_gral) and status = 'A' " +
                                                     "      union all " +
                                                     "      select count(*) as no_registros from bd_SiTTe.dbo.cg_usuario WHERE id_empleado in (select id_empleado from bd_Empleado.dbo.cg_empleado where id_gral = @id_gral) and status = 'A' " +
                                                     "      union all " +
                                                     "      select count(*) as no_registros from bd_SiSeg.dbo.cg_usuario WHERE id_empleado in (select id_empleado from bd_Empleado.dbo.cg_empleado where id_gral = @id_gral) and status = 'A' " +
                                                     "      union all " +
                                                     "      select count(*) as no_registros from bd_SiRAc.dbo.cg_usuario WHERE id_empleado in (select id_empleado from bd_Empleado.dbo.cg_empleado where id_gral = @id_gral) and status = 'A' " +
                                                     "      union all " +
                                                     "      select count(*) as no_registros from bd_empleado.dbo.cg_usuario WHERE id_empleado in (select id_empleado from bd_Empleado.dbo.cg_empleado where id_gral = @id_gral) and status = 'A' " +
                                                     "      union all " +
                                                     "      select count(*) as no_registros from bd_SiTTi.dbo.cg_usuario WHERE id_empleado in (select id_empleado from bd_SiTTi.dbo.cg_empleado where id_gral = @id_gral) and status = 'A') as usuarios "



                            '"      union all " +
                            '"      select count(*) as no_registros from bd_SiTAF.dbo.cg_usuario WHERE id_empleado in (select id_empleado from bd_Empleado.dbo.cg_empleado where id_gral = @id_gral) and status = 'A' " +



                            SCMValores.Parameters.AddWithValue("@id_gral", Val(._txtIdGral.Text))
                            ConexionBD.Open()
                            contPass = SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            If contPass = 1 Then
                                'Enviar correo en caso de Alta si es el primer usuario
                                enviarCorreo()

                            Else

                            End If
                        End If

                        limpiarPantalla()
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnReCredenciales_Click(sender As Object, e As EventArgs) Handles btnReCredenciales.Click
        'Valida que no haya cambios y envia el correo
        Try
            litError.Text = ""
            If cambioContraseña Or cambioPerfil Or cambioUsuario Or cambioTipoC Then
                litError.Text = "Valores modificados, guarde los cambios primero"
            Else
                'Crear nueva contreña
                Me.txtContraseñaOrg.Text = accessDB.RandomString()
                Dim passSHA1 As String = accessDB.EncryptSHA1(Me.txtContraseñaOrg.Text)
                Me.txtContraseña.Text = accessDB.Encrypt(passSHA1)

                actualizarPass()
                enviarCorreo()
            End If
        Catch ex As Exception
            litError.Text = "No se pueden enviar las credenciales, intentelo nuevamnete"
        End Try
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        limpiarPantalla()
    End Sub

#End Region

#Region "Eventos cambios de valores"
    Protected Sub txtContraseña_TextChanged(sender As Object, e As EventArgs) Handles txtContraseña.TextChanged
        Me.cambioContraseña = True
    End Sub

    Protected Sub ddlPerfil_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPerfil.SelectedIndexChanged
        Me.cambioPerfil = True
    End Sub

    Protected Sub txtUsuario_TextChanged(sender As Object, e As EventArgs) Handles txtUsuario.TextChanged
        Me.cambioUsuario = True
    End Sub
    ''Nuevo Codigo
    Protected Sub cbDirector_CheckedChanged(sender As Object, e As EventArgs) Handles cbDirector.CheckedChanged
        With Me
            Try
                If cbDirector.Checked = True Then
                    cbValidador.Checked = False
                End If

            Catch ex As Exception
                .litError.Text = ex.Message.ToString()
            End Try
        End With
    End Sub

    Protected Sub cbValidador_CheckedChanged(sender As Object, e As EventArgs) Handles cbValidador.CheckedChanged
        With Me
            Try
                If cbValidador.Checked = True Then
                    cbDirector.Checked = False
                End If

            Catch ex As Exception
                .litError.Text = ex.Message.ToString()
            End Try
        End With
    End Sub


#End Region


End Class