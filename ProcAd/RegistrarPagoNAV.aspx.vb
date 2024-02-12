Public Class RegistrarPagoNAV
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtIdMsInst.Text = Session("idMsInst")
                        ._txtBan.Text = 0

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim query As String
                        Dim sdaSolicitud As New SqlDataAdapter
                        Dim dsSolicitud As New DataSet

                        query = " SELECT id_ms_anticipo_proveedor, importe_requerido, empresa, justificacion, fecha_prog_pago_tesoreria, tipo_anticipo, prov.rfc as RFCProveedor, prov.nombre as Proveedor FROM ms_anticipo_proveedor LEFT JOIN ms_instancia ON ms_anticipo_proveedor.id_ms_anticipo_proveedor = ms_instancia.id_ms_sol LEFT JOIN bd_SiSAC.dbo.cg_proveedor prov ON ms_anticipo_proveedor.id_proveedor = prov.id_proveedor  WHERE id_ms_instancia = @id_ms_instancia"
                        sdaSolicitud.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaSolicitud.Fill(dsSolicitud)
                        ConexionBD.Close()
                        .lblFolio.Text = dsSolicitud.Tables(0).Rows(0).Item("id_ms_anticipo_proveedor").ToString()
                        .lblImporte.Text = "$" + dsSolicitud.Tables(0).Rows(0).Item("importe_requerido").ToString()
                        .lblEmpresa.Text = dsSolicitud.Tables(0).Rows(0).Item("empresa").ToString()
                        .txtJustificacion.Text = dsSolicitud.Tables(0).Rows(0).Item("justificacion").ToString()
                        .lblFechaPago.Text = dsSolicitud.Tables(0).Rows(0).Item("fecha_prog_pago_tesoreria").ToShortDateString()
                        ._txtTipo_pago.Text = dsSolicitud.Tables(0).Rows(0).Item("tipo_anticipo").ToString()
                        .lblProveedor.Text = dsSolicitud.Tables(0).Rows(0).Item("Proveedor").ToString()
                        .lblNumProveedor.Text = dsSolicitud.Tables(0).Rows(0).Item("RFCProveedor").ToString()


                        'Consulta de Adjuntos
                        Dim sdaArchivos As New SqlDataAdapter
                        Dim dsArchivos As New DataSet
                        .gvAdjuntos.DataSource = dsArchivos
                        'Adjuntos
                        sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " +
                                                                   "     , 'http://148.223.153.43/ProcAd - Adjuntos AntProv/' + cast(id_dt_archivo_adj_anticipo as varchar(20)) + '-' + nombre as path " +
                                                                   "from dt_archivo_adj_anticipo " +
                                                                   "where id_ms_anticipo_proveedor = @id_ms_anticipo_proveedor ", ConexionBD)
                        sdaArchivos.SelectCommand.Parameters.AddWithValue("@id_ms_anticipo_proveedor", Val(.lblFolio.Text))
                        ConexionBD.Open()
                        sdaArchivos.Fill(dsArchivos)
                        .gvAdjuntos.DataBind()
                        ConexionBD.Close()
                        sdaArchivos.Dispose()
                        dsArchivos.Dispose()
                        .gvAdjuntos.SelectedIndex = -1
                        .upAdjuntos.Update()
                        llenarGvPedidosCompra()
                        If gvPedidosCompras.Rows.Count = 0 Then
                            .lbl_Pedido.Visible = False
                            .gvPedidosCompras.Visible = False
                        End If
                    Else
                        Server.Transfer("Login.aspx")
                    End If

                Catch ex As Exception
                    .litError.Text = ex.ToString()
                End Try

            End With
        End If
    End Sub

    Public Sub llenarGvPedidosCompra()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaPedidosC As New SqlDataAdapter
                Dim dsPedididosC As New DataSet
                .gvPedidosCompras.DataSource = dsPedididosC
                sdaPedidosC.SelectCommand = New SqlCommand(" SELECT id_dt_pedidos_compra, id_pedido_compra, pedido_compra " +
                                                           " FROM dt_pedidos_compra " +
                                                           " WHERE id_ms_solicitud_anticipo = @id_ms_solicitud_anticipo ", ConexionBD)
                sdaPedidosC.SelectCommand.Parameters.AddWithValue("@id_ms_solicitud_anticipo", Val(.lblFolio.Text))
                ConexionBD.Open()
                sdaPedidosC.Fill(dsPedididosC)
                .gvPedidosCompras.DataBind()
                ConexionBD.Close()
                sdaPedidosC.Dispose()
                dsPedididosC.Dispose()
                .gvPedidosCompras.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnAutorizar_Click(sender As Object, e As EventArgs) Handles btnRegistrar.Click
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD

                Dim fecha As DateTime = Date.Now
                Dim valor As Integer = 0

                'Autorizar la solicitud
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = " DECLARE @valorR int; EXEC SP_U_ms_anticipo_proveedor_CxP @id_ms_anticipo_proveedor, @id_usr_registro_pago_cxp, @fecha_registro_pago_cxp, @id_ms_instancia, @id_actividad, @fecha, @id_usr, @valorR OUTPUT; SELECT @valorR"
                SCMValores.Parameters.AddWithValue("@id_ms_anticipo_proveedor", .lblFolio.Text)
                SCMValores.Parameters.AddWithValue("@id_usr_registro_pago_cxp", Val(._txtIdUsuario.Text))
                SCMValores.Parameters.AddWithValue("@fecha_registro_pago_cxp", fecha)
                SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                If ._txtTipo_pago.Text = "1" And .gvPedidosCompras.Rows.Count > 0 Then
                    SCMValores.Parameters.AddWithValue("@id_actividad", 141)
                Else
                    SCMValores.Parameters.AddWithValue("@id_actividad", 140)
                End If
                SCMValores.Parameters.AddWithValue("@fecha", fecha)
                SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                valor = SCMValores.ExecuteScalar()
                ConexionBD.Close()

                If valor = 1 Then
                    .pnlInicio.Enabled = False
                Else
                    Server.Transfer("Menu.aspx")
                End If
                SCMValores.Parameters.Clear()
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With

    End Sub
End Class