Public Class ConsultaContratos
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        'Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        'SCMValores.Connection = ConexionBD


                        Dim query As String
                        'Llenar lista de empresas 
                        Dim sdaEmpresa As New SqlDataAdapter
                        Dim dsEmpresa As New DataSet
                        query = ""
                        query = " SELECT DISTINCT(empresa) AS empresa " +
                                " FROM ms_contrato_arrenda " +
                                " ORDER BY empresa "
                        sdaEmpresa.SelectCommand = New SqlCommand(query, ConexionBD)

                        .ddlEmpresa.DataSource = dsEmpresa
                        .ddlEmpresa.DataTextField = "empresa"
                        .ddlEmpresa.DataValueField = "empresa"
                        ConexionBD.Open()
                        sdaEmpresa.Fill(dsEmpresa)
                        .ddlEmpresa.DataBind()
                        ConexionBD.Close()
                        sdaEmpresa.Dispose()
                        dsEmpresa.Dispose()
                        .ddlEmpresa.SelectedIndex = -1

                        'Llenar lista de tipos de arrendamiento
                        Dim sdaTipoArrenda As New SqlDataAdapter
                        Dim dsTipoArrenda As New DataSet
                        query = ""
                        query = " SELECT DISTINCT tipo_arrendamiento " +
                                " FROM ms_contrato_arrenda " +
                                " ORDER BY tipo_arrendamiento "
                        sdaTipoArrenda.SelectCommand = New SqlCommand(query, ConexionBD)
                        .ddlTipoArrenda.DataSource = dsTipoArrenda
                        .ddlTipoArrenda.DataTextField = "tipo_arrendamiento"
                        .ddlTipoArrenda.DataValueField = "tipo_arrendamiento"
                        ConexionBD.Open()
                        sdaTipoArrenda.Fill(dsTipoArrenda)
                        .ddlTipoArrenda.DataBind()
                        ConexionBD.Close()
                        sdaTipoArrenda.Dispose()
                        dsTipoArrenda.Dispose()
                        .ddlTipoArrenda.SelectedIndex = -1


                        'Llenar lista de Arrenadora
                        Dim sdaArrendadora As New SqlDataAdapter
                        Dim dsArrendadora As New DataSet
                        query = ""
                        query = " SELECT DISTINCT arrendadora " +
                                " FROM ms_contrato_arrenda " +
                                " ORDER BY arrendadora "
                        sdaArrendadora.SelectCommand = New SqlCommand(query, ConexionBD)

                        .ddlArrendadora.DataSource = dsArrendadora
                        .ddlArrendadora.DataTextField = "arrendadora"
                        .ddlArrendadora.DataValueField = "arrendadora"
                        ConexionBD.Open()
                        sdaArrendadora.Fill(dsArrendadora)
                        .ddlArrendadora.DataBind()
                        ConexionBD.Close()
                        sdaArrendadora.Dispose()
                        dsArrendadora.Dispose()
                        .ddlArrendadora.SelectedIndex = -1

                        'Llenar lista de Contratos
                        'Dim sdaNoContrato As New SqlDataAdapter
                        'Dim dsNoContrato As New DataSet
                        'query = ""
                        'query = " SELECT DISTINCT id_usr_autorizador2, autorizador2 " +
                        '        " FROM ms_comprobacion_anticipo " +
                        '        " ORDER BY autorizador2 "
                        'sdaNoContrato.SelectCommand = New SqlCommand(query, ConexionBD)

                        '.ddlNoContrato.DataSource = dsNoContrato
                        '.ddlNoContrato.DataTextField = "autorizador2"
                        '.ddlNoContrato.DataValueField = "id_usr_autorizador2"
                        'ConexionBD.Open()
                        'sdaNoContrato.Fill(dsNoContrato)
                        '.ddlNoContrato.DataBind()
                        'ConexionBD.Close()
                        'sdaNoContrato.Dispose()
                        'dsNoContrato.Dispose()
                        '.ddlNoContrato.SelectedIndex = -1


                        limpiarPantalla()
                    Else
                        Server.Transfer("Login.aspx")
                    End If

                Catch ex As Exception
                    .litError.Text = ex.ToString()
                End Try
            End With
        End If
    End Sub
#End Region

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

    Public Sub limpiarPantalla()
        With Me
            Try
                .cbEmpresa.Checked = False
                .pnlEmpresa.Visible = False
                .ddlEmpresa.SelectedIndex = -1
                .cbNoContrato.Checked = False
                .pnlNoContrato.Visible = False
                .txtNoContrato.Text = ""
                .cbTipoArrenda.Checked = False
                .pnlTipoArrenda.Visible = False
                .ddlTipoArrenda.SelectedIndex = -1
                .cbArrendadora.Checked = False
                .pnlArrendadora.Visible = False
                .ddlArrendadora.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

    Public Sub llenarContratos()
        With Me
            Try


                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaContrato As New SqlDataAdapter
                Dim dsContrato As New DataSet
                .gvContratos.DataSource = dsContrato


                Dim query As String = ""

                query = " SELECT id_ms_contrato, no_contrato, empresa, tipo_arrendamiento, arrendadora, fecha_inicio, fecha_fin FROM ms_contrato_arrenda " +
                        " WHERE 1 = 1 "

                If .cbEmpresa.Checked = True Then
                    query = query + "  and empresa = @empresa "
                End If
                If .cbNoContrato.Checked = True Then
                    query = query + "  and no_contrato = @no_contrato "
                End If
                If .cbTipoArrenda.Checked = True Then
                    query = query + "  and tipo_arrendamiento  = @tipo_arrendamiento "
                End If
                If .cbArrendadora.Checked = True Then
                    query = query + "  and arrendadora = @arrendadora "
                End If

                query = query + "ORDER BY id_ms_contrato "

                sdaContrato.SelectCommand = New SqlCommand(query, ConexionBD)


                If .cbEmpresa.Checked = True Then
                    sdaContrato.SelectCommand.Parameters.AddWithValue("@empresa", .ddlEmpresa.SelectedItem.Text)
                End If

                If .cbNoContrato.Checked = True Then
                    sdaContrato.SelectCommand.Parameters.AddWithValue("@no_contrato", .cbNoContrato.Text)
                End If
                If .cbTipoArrenda.Checked = True Then
                    sdaContrato.SelectCommand.Parameters.AddWithValue("@tipo_arrendamiento", .ddlTipoArrenda.SelectedItem.Text)
                End If
                If .cbArrendadora.Checked = True Then
                    sdaContrato.SelectCommand.Parameters.AddWithValue("@arrendadora", .ddlArrendadora.SelectedItem.Text)
                End If


                .gvContratos.DataSource = dsContrato
                ConexionBD.Open()
                sdaContrato.Fill(dsContrato)
                ConexionBD.Close()
                .gvContratos.DataBind()
                sdaContrato.Dispose()
                dsContrato.Dispose()
                .gvContratos.SelectedIndex = -1
                If .gvContratos.Rows.Count = 0 Then
                    .pnlDatosContrato.Visible = False
                    .litError.Text = "No existe Registros para esos valores"
                Else
                    .pnlDatosContrato.Visible = True
                End If
            Catch ex As Exception

            End Try

        End With
    End Sub

#End Region

    Protected Sub btnImportarUnidades_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        llenarContratos()
    End Sub
#Region "Filtros"
    Protected Sub cbEmpresa_CheckedChanged(sender As Object, e As EventArgs) Handles cbEmpresa.CheckedChanged
        vista(Me.pnlEmpresa, Me.cbEmpresa.Checked)
    End Sub
    Protected Sub cbNoContrato_CheckedChanged(sender As Object, e As EventArgs) Handles cbNoContrato.CheckedChanged
        vista(Me.pnlNoContrato, Me.cbNoContrato.Checked)
    End Sub
    Protected Sub cbTipoArrenda_CheckedChanged(sender As Object, e As EventArgs) Handles cbTipoArrenda.CheckedChanged
        vista(Me.pnlTipoArrenda, Me.cbTipoArrenda.Checked)
    End Sub
    Protected Sub cbArrendadora_CheckedChanged(sender As Object, e As EventArgs) Handles cbArrendadora.CheckedChanged
        vista(Me.pnlArrendadora, Me.cbArrendadora.Checked)
    End Sub

#End Region



End Class