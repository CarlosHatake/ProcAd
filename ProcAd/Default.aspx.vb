Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        With Me
            Try
                If Not IsPostBack Then
                    .litError.Text = ""
                    Session("id_usuario") = 0
                    Server.Transfer("Menu.aspx")
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

End Class