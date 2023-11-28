Public Class ConsCFDI
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        '.cbFechaCreacion.Checked = True
                        '.cbFechaCreacion.Enabled = False
                        limpiar()
                    Else

                        Server.Transfer("Login.aspx")
                    End If

                Catch ex As Exception

                End Try
            End With
        End If
    End Sub

    Protected Sub cbCFDI_CheckedChanged(sender As Object, e As EventArgs) Handles cbCFDI.CheckedChanged
        vista(Me.pnlCFDI, Me.cbCFDI.Checked)
        If Me.cbCFDI.Checked = True Then
            Me.txtCFDI.Text = ""
        End If
    End Sub

    Protected Sub cbSistema_CheckedChanged(sender As Object, e As EventArgs) Handles cbSistema.CheckedChanged
        vista(Me.pnlSistema, Me.cbSistema.Checked)
    End Sub

    Protected Sub cbFechaCreacion_CheckedChanged(sender As Object, e As EventArgs) Handles cbFechaCreacion.CheckedChanged
        vista(Me.pnlFechaCreacion, Me.cbFechaCreacion.Checked)
    End Sub

    Protected Sub cbUsoCFDI_CheckedChanged(sender As Object, e As EventArgs) Handles cbUsoCFDI.CheckedChanged
        vista(Me.pnlUsoCFDI, Me.cbUsoCFDI.Checked)
    End Sub

    Protected Sub cbRFCEmisor_CheckedChanged(sender As Object, e As EventArgs) Handles cbRFCEmisor.CheckedChanged
        vista(Me.pnlRFCEmisor, Me.cbRFCEmisor.Checked)
        If Me.cbRFCEmisor.Checked = True Then
            Me.txtRFCEmisor.Text = ""
        End If
    End Sub

    Protected Sub cbNoSol_CheckedChanged(sender As Object, e As EventArgs) Handles cbRFCReceptor.CheckedChanged
        vista(Me.pnlRFCReceptor, Me.cbRFCReceptor.Checked)
        If Me.cbRFCReceptor.Checked = True Then
            Me.txtRFCReceptor.Text = ""
        End If
    End Sub
    Protected Sub cbVersion_CheckedChanged(sender As Object, e As EventArgs) Handles cbVersion.CheckedChanged
        vista(Me.pnlVersion, Me.cbVersion.Checked)
    End Sub
    'Protected Sub cbEmpresa_CheckedChanged(sender As Object, e As EventArgs) Handles cbEmpresa.CheckedChanged
    '    vista(Me.pnlEmpresa, Me.cbEmpresa.Checked)
    'End Sub
    'Protected Sub cbRegimenEmisor_CheckedChanged(sender As Object, e As EventArgs) Handles cbRegimenEmisor.CheckedChanged
    '    vista(Me.pnlRegimenEmisor, Me.cbRegimenEmisor.Checked)
    'End Sub
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

    Public Sub limpiar()
        With Me
            .pnlFiltros.Visible = True
            .cbCFDI.Checked = False
            .pnlCFDI.Visible = False
            .cbSistema.Checked = False
            .pnlSistema.Visible = False
            .cbFechaCreacion.Checked = False
            .pnlFechaCreacion.Visible = False
            .wdpFechaI.Date = Date.Now.ToShortDateString
            .wdpFechaF.Date = Date.Now.ToShortDateString
            .cbUsoCFDI.Checked = False
            .pnlUsoCFDI.Visible = False
            .cbRFCEmisor.Checked = False
            .pnlRFCEmisor.Visible = False
            .cbRFCReceptor.Checked = False
            .pnlRFCReceptor.Visible = False
            .cbVersion.Checked = False
            .pnlVersion.Visible = False
            '.cbEmpresa.Checked = False
            '.pnlEmpresa.Visible = False
            '.cbRegimenEmisor.Checked = False
            '.pnlRegimenEmisor.Visible = False
            'Ocultar resto de paneles principales
            .pnlRegistros.Visible = False
            .pnlExportar.Visible = False
            .pnlConsultaFactura.Visible = False
            .pnlGvConsultaFactura.Visible = False



        End With
    End Sub

#End Region

#Region "Buscar"
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        With Me
            Try
                .pnlFiltros.Visible = False
                .btnBuscar.Visible = False
                .pnlConsultaFactura.Visible = True
                .pnlGvConsultaFactura.Visible = True

            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub
#End Region


#Region "Exportar"

    Protected Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        Dim tw As New System.IO.StringWriter
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        With Me
            Try
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment;filename=Evaluaciones.xls")
                Response.ContentType = "application/vnd.ms-excel"
                .gvExportar.Visible = True
                .gvExportar.RenderControl(hw)
                .gvExportar.Visible = False
                Response.Write(tw.ToString())
                Response.End()
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub

#End Region
End Class