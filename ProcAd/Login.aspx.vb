Public Class Login
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    .txtUsuario.Text = ""
                    .txtPass.Text = ""
                Catch ex As Exception
                    .litError.Text = ex.ToString
                End Try
            End With
        End If
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""

                If .txtUsuario.Text.Trim = "" Or .txtPass.Text.Trim = "" Then
                    .litError.Text = "Información Insuficiente, favor de verificar"
                Else
                    'Verificar que exista el usuario en la tabla cg_usuario
                    'Creación de Variables para la conexión y consulta de infromación a la base de datos
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd") 'ConfigurationManager.ConnectionStrings("bd_ProcAdCS").ConnectionString
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD

                    Dim conteo As Integer = 0
                    Dim pass As String = ""
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "select count(*) from cg_usuario where nick=@usuario and status = 'A'"
                    SCMValores.Parameters.AddWithValue("@usuario", .txtUsuario.Text)
                    ConexionBD.Open()
                    conteo = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                    .litError.Text = ""
                    If conteo > 0 Then
                        SCMValores.CommandText = "select pass from cg_usuario where nick=@usuario and status = 'A'"
                        ConexionBD.Open()
                        pass = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        Dim passSHA1 As String = accessDB.Decrypt(pass)
                        Dim passPageSHA1 As String = accessDB.EncryptSHA1(.txtPass.Text)

                        If passSHA1 = passPageSHA1 Then
                            SCMValores.CommandText = "select id_usuario from cg_usuario where nick=@usuario and status = 'A'"
                            ConexionBD.Open()
                            Session("id_usuario") = SCMValores.ExecuteScalar()
                            Server.Transfer("Menu.aspx")
                            ConexionBD.Close()
                            .litError.Text = ""
                        Else
                            .txtPass.Text = ""
                            .litError.Text = "<b>Contraseña Inválida, favor de verificar</b>"
                        End If
                    Else
                        .txtUsuario.Text = ""
                        .txtPass.Text = ""
                        .litError.Text = "<b>La información es incorrecta o el usuario no está activo, verifique con el administrador</b>"
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnConUsuario_Click(sender As Object, e As EventArgs) Handles btnConUsuario.Click
        With Me
            Try
                Server.Transfer("ConLogin.aspx")
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

End Class