Public Class IValeES
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio e Impresión"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack() Then
            With Me
                Try
                    'Session("idMsReserv") = 13
                    ._txtFolio.Text = Session("idMsReserv")
                    If Val(._txtFolio.Text) > 0 Then
                        .litError.Text = ""

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        'Obtener información de Vale
                        Dim sdaVale As New SqlDataAdapter
                        Dim dsVale As New DataSet
                        sdaVale.SelectCommand = New SqlCommand("select empleado_nom + ' ' + empleado_appat + ' ' + empleado_apmat as solicito " + _
                                                               "     , autorizador_nom + ' ' + autorizador_appat + ' ' + autorizador_apmat as autorizador " + _
                                                               "	 , marca " + _
                                                               "	 , placas " + _
                                                               "	 , fecha_ini " + _
                                                               "	 , no_eco " + _
                                                               "	 , fecha_fin " + _
                                                               "from ms_reserva " + _
                                                               "where id_ms_reserva = @idMsReserv ", ConexionBD)
                        sdaVale.SelectCommand.Parameters.AddWithValue("@idMsReserv", Val(._txtFolio.Text))
                        ConexionBD.Open()
                        sdaVale.Fill(dsVale)
                        ConexionBD.Close()
                        .lblUnidad.Text = dsVale.Tables(0).Rows(0).Item("marca").ToString()
                        .lblPlacas.Text = dsVale.Tables(0).Rows(0).Item("placas").ToString()
                        .lblFecha.Text = dsVale.Tables(0).Rows(0).Item("fecha_ini").ToString()
                        .lblNoEco.Text = dsVale.Tables(0).Rows(0).Item("no_eco").ToString()
                        .lblConductor.Text = dsVale.Tables(0).Rows(0).Item("solicito").ToString()
                        .lblEntrega.Text = .lblConductor.Text
                        .lblRespUnidad.Text = dsVale.Tables(0).Rows(0).Item("autorizador").ToString()
                        .lblFechaE.Text = dsVale.Tables(0).Rows(0).Item("fecha_fin").ToString()
                        sdaVale.Dispose()
                        dsVale.Dispose()

                        imprimir()
                    Else
                        .litError.Text = "Folio Inválido, favor de Verificar"
                    End If
                Catch ex As Exception
                    .litError.Text = ex.ToString
                End Try
            End With
        End If
    End Sub

    Private Sub imprimir()
        ' * * * Anterior * * *
        'Dim scriptString As String = "<script LANGUAJE='JavaScript'> window.print();</script>"
        'Page.RegisterStartupScript("clientScript", scriptString)
        ' * * * Anterior * * *

        ' Define el Nombre y Tipo del client scripts en la página
        Dim csNombre As [String] = "ImpPagina"
        Dim csTipo As Type = Me.[GetType]()
        'Asigna al ClientScriptManager la referencia del la clase de la Página 
        Dim cs As ClientScriptManager = Page.ClientScript
        'Revisa si el inicio del script ya está registrado 
        If Not cs.IsStartupScriptRegistered(csTipo, csNombre) Then
            Dim cstext1 As New StringBuilder()
            cstext1.Append("<script type=text/javascript> window.print();</script>")
            cs.RegisterStartupScript(csTipo, csNombre, cstext1.ToString())
        End If
    End Sub

#End Region

End Class