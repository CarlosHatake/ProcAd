Public Class ConsCFDI
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"
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
#End Region

#Region "Funciones"

    Public Sub localizar(id As Integer)
        Try
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim sdaConsulta As New SqlDataAdapter
            Dim dsConsulta As New DataSet
            Dim query As String
            query = "Select CFDI, fecha_emision, fecha_timbrado, razon_receptor, rfc_emisor, movimiento, rfc_receptor, uso_cfdi, regimen_receptor, regimen_emisor, version, serie, moneda, domicilio_fiscal_receptor, sistema, folio, pac_certificador, importe, descuento, total_tras_IVA, total_tras_IEPS, total_ret_IVA, total_ret_IEPS, total_ret_ISR from dt_factura_nav_net_procad where id_dt_factura_nav_net_procad = @id_dt_factura_nav_net_procad"
            sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)
            sdaConsulta.SelectCommand.Parameters.AddWithValue("@id_dt_factura_nav_net_procad", id)
            gvConsultaFactura.DataSource = dsConsulta
            ConexionBD.Open()
            sdaConsulta.Fill(dsConsulta)
            ConexionBD.Close()
            gvConsultaFactura.DataBind()
            btnExportar.Visible = False
            gvFacturasCFDI.Visible = False
            pnlConsultaFactura.Visible = True
            gvConsultaFactura.Visible = True
            pnlGvConsultaFactura.Visible = True

            lblCFDI.Text = dsConsulta.Tables(0).Rows(0).Item("CFDI").ToString()
            lblFechaEmision.Text = dsConsulta.Tables(0).Rows(0).Item("fecha_emision").ToString()
            lblFechaTimbrado.Text = dsConsulta.Tables(0).Rows(0).Item("fecha_timbrado").ToString()
            lblRSReceptor.Text = dsConsulta.Tables(0).Rows(0).Item("razon_receptor").ToString()
            lblRSEmisor.Text = dsConsulta.Tables(0).Rows(0).Item("rfc_emisor").ToString()
            lblMovimiento.Text = dsConsulta.Tables(0).Rows(0).Item("movimiento").ToString()
            lblRFCReceptor.Text = dsConsulta.Tables(0).Rows(0).Item("rfc_receptor").ToString()
            lblUsoCFDI.Text = dsConsulta.Tables(0).Rows(0).Item("uso_cfdi").ToString()
            lblRFReceptor.Text = dsConsulta.Tables(0).Rows(0).Item("regimen_receptor").ToString()
            lblRFEmisor.Text = dsConsulta.Tables(0).Rows(0).Item("regimen_emisor").ToString()

            lblVersion.Text = dsConsulta.Tables(0).Rows(0).Item("version").ToString()
            lblSerie.Text = dsConsulta.Tables(0).Rows(0).Item("serie").ToString()
            lblMoneda.Text = dsConsulta.Tables(0).Rows(0).Item("moneda").ToString()
            lblDFReceptor.Text = dsConsulta.Tables(0).Rows(0).Item("domicilio_fiscal_receptor").ToString()
            lblSistema.Text = dsConsulta.Tables(0).Rows(0).Item("sistema").ToString()
            lblFolio.Text = dsConsulta.Tables(0).Rows(0).Item("folio").ToString()
            lnlPACCertificador.Text = dsConsulta.Tables(0).Rows(0).Item("pac_certificador").ToString()

            sdaConsulta.Dispose()
            dsConsulta.Dispose()
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub

    Public Sub vista(ByRef control, ByVal valor)
        With Me
            If valor Then
                control.Visible = True
            Else
                control.Visible = False
            End If
        End With
    End Sub

    Public Sub llenarGrid()
        Try
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim sdaConsulta As New SqlDataAdapter
            Dim dsConsulta As New DataSet
            Dim query As String
            Dim valor As Integer = 1
            Dim bandera As Boolean = True
            query = "select id_dt_factura_nav_net_procad, CFDI, fecha_emision, fecha_timbrado, movimiento, rfc_receptor, razon_receptor, regimen_receptor, uso_cfdi, rfc_emisor, razon_emisor, regimen_emisor, estatus, tipo_comprobante, serie, folio, lugar_expedicion, metodo_pago, forma_pago, tipo_cambio, moneda, descripcion, subtotal, total_tras_IVA, total_tras_IEPS, total_ret_IVA, total_ret_IEPS, total_ret_ISR, descuento, importe, fecha_cancelacion, estado, sistema, actividad " +
                    " from dt_factura_nav_net_procad where 1 = 1 "

            While bandera = True
                If txtCFDI.Text.Trim <> "" And valor = 1 Then
                    query = query + " and CFDI = @CFDI "
                ElseIf txtCFDI.Text.Trim <> "" And valor = 2 Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@CFDI", txtCFDI.Text)
                End If

                If cbFechaCreacion.Checked = True And valor = 1 Then
                    query = query + " and (fecha_emision between @fechaIni and @fechaFin)  "
                ElseIf cbFechaCreacion.Checked = True And valor = 2 Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaIni", wdpFechaI.Date)
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@fechaIni", wdpFechaF.Date)
                End If

                If txtRFCEmisor.Text.Trim <> "" And valor = 1 Then
                    query = query + " and rfc_emisor  like '%@rfc_emisor%' "
                ElseIf txtRFCEmisor.Text.Trim <> "" And valor = 2 Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@rfc_emisor", txtRFCEmisor.Text)
                End If

                If cbVersion.Checked = True And valor = 1 Then
                    query = query + " and version = @version "
                ElseIf cbVersion.Checked = True And valor = 2 Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@version", ddlVersion.SelectedValue.ToString)
                End If

                If cbSistema.Checked = True And valor = 1 Then
                    query = query + " and sistema = @sistema "
                ElseIf cbSistema.Checked = True And valor = 2 Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@sistema", ddlSistema.SelectedValue.ToString)
                End If

                If cbUsoCFDI.Checked = True And valor = 1 Then
                    query = query + " and uso_cfdi = @uso_cfdi "
                ElseIf cbUsoCFDI.Checked = True And valor = 2 Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@version", ddlUsoCFDI.SelectedValue.ToString)
                End If

                If cbRFCReceptor.Checked = True And valor = 1 Then
                    query = query + " and rfc_receptor like '%@rfc_receptor%' "
                ElseIf cbRFCReceptor.Checked = True And valor = 2 Then
                    sdaConsulta.SelectCommand.Parameters.AddWithValue("@version", txtRFCReceptor.Text.ToString)
                End If

                If valor = 1 Then
                    sdaConsulta.SelectCommand = New SqlCommand(query, ConexionBD)
                    valor = 2
                Else
                    bandera = False
                End If
            End While

            gvFacturasCFDI.Visible = True
            gvFacturasCFDI.DataSource = dsConsulta
            ConexionBD.Open()
            sdaConsulta.Fill(dsConsulta)
            ConexionBD.Close()
            gvFacturasCFDI.DataBind()
            sdaConsulta.Dispose()
            dsConsulta.Dispose()
            If gvFacturasCFDI.Rows.Count = 0 Then
                pnlRegistros.Visible = False
                litError.Text = "No existe Registros para esos valores"
            Else
                pnlRegistros.Visible = True
            End If
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
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
                llenarGrid()
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
                gvExportar.Visible = True
                gvExportar.RenderControl(hw)
                gvExportar.Visible = False
                Response.Write(tw.ToString())
                Response.End()
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

    Protected Sub gvFacturasCFDI_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvFacturasCFDI.SelectedIndexChanged
        Try
            localizar(gvFacturasCFDI.DataKeys(gvFacturasCFDI.SelectedIndex).Values("id_dt_factura_nav_net_procad"))
        Catch ex As Exception
            litError.Text = ex.Message
        End Try
    End Sub


#End Region
End Class