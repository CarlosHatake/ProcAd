Public Class Anexos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtIdMsInst.Text = Session("idMsInst")
                        ._txtBan.Text = 0

                    Else
                        Server.Transfer("Login.aspx")
                    End If
                Catch ex As Exception
                    .litError.Text = ex.ToString()

                End Try
            End With
        End If
    End Sub

    Protected Sub cbTipoCarga_SelectedIndexChanged(sender As Object, e As EventArgs)
        With Me
            Try
                If cbTipoCarga.SelectedIndex = 0 Then
                    .pnlCargarArchivo.Visible = True
                    .pnlCargaManual.Visible = False
                ElseIf cbTipoCarga.SelectedIndex = 1 Then
                    .pnlCargaManual.Visible = True
                    .pnlCargarArchivo.Visible = False
                ElseIf cbTipoCarga.SelectedIndex = -1 Then
                    .pnlCargarArchivo.Visible = False
                    .pnlCargaManual.Visible = False
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString()

            End Try
        End With
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardarContrato.Click
        With Me
            Try
                .pnlCargaManual.Enabled = False
                .pnlAgregarAnexo.Visible = True
            Catch ex As Exception

            End Try
        End With
    End Sub

End Class