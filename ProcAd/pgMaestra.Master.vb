Public Class pgMaestra
    Inherits System.Web.UI.MasterPage
    Dim accessDB As New clsAccess

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    Session("litError") = ""
                    'Verificar que exista el usuario en la tabla cg_usuario
                    'Creación de Variables para la conexión y consulta de infromación a la base de datos
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD

                    Dim conteo As Integer = 0
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "select count(*) from cg_usuario where id_usuario = @idUsuario and status = 'A'"
                    SCMValores.Parameters.AddWithValue("@idUsuario", Val(Session("id_usuario")))
                    ConexionBD.Open()
                    conteo = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    If conteo > 0 Then
                        SCMValores.CommandText = "select nombre + ' ' + ap_paterno + ' ' + ap_materno from cg_usuario inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado where id_usuario = @idUsuario"
                        ConexionBD.Open()
                        .lblUsuario.Text = SCMValores.ExecuteScalar()
                        ConexionBD.Close()
                        .lblTitulo.Text = Session("TituloAct")
                    Else
                        Session("id_usuario") = 0
                    End If
                Catch ex As Exception
                    Session("litError") = ex.ToString
                End Try
            End With
        End If
    End Sub

    Protected Sub btnMenu_Click(sender As Object, e As EventArgs) Handles btnMenu.Click
        Server.Transfer("Menu.aspx")
    End Sub

End Class