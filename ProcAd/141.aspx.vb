Public Class _141
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        '._txtIdMsInst.Text = Session("idMsInst")
                        ._txtBan.Text = 0
                        listaValidacion()
                    Else
                        Server.Transfer("Login.aspx")
                    End If
                Catch ex As Exception

                End Try
            End With
        End If


    End Sub

    Public Sub listaValidacion()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaFacturas As New SqlDataAdapter
                Dim dsFacturas As New DataSet
                .gvListaValidacion.DataSource = dsFacturas

                sdaFacturas.SelectCommand = New SqlCommand(" SELECT ms_anticipo_proveedor.id_ms_anticipo_proveedor, empresa, fecha_solicita, proveedor, importe_requerido, empleado_solicita, pc.pedido_compra  " +
                                                           " FROM ms_anticipo_proveedor " +
                                                           " LEFT JOIN ms_instancia inst ON inst.id_ms_sol = ms_anticipo_proveedor.id_ms_anticipo_proveedor " +
                                                           " LEFT JOIN dt_pedidos_compra pc ON ms_anticipo_proveedor.id_ms_anticipo_proveedor = pc.id_ms_anticipo_proveedor " +
                                                           " WHERE inst.id_actividad = 141 AND tipo = 'AP' AND NOT EXISTS (SELECT NULL FROM	ms_comprobacion_anticipo comp WHERE ms_anticipo_proveedor.id_ms_anticipo_proveedor = comp.id_ms_anticipo_proveedor) ", ConexionBD)



                ConexionBD.Open()
                sdaFacturas.Fill(dsFacturas)
                .gvListaValidacion.DataBind()
                ConexionBD.Close()
                sdaFacturas.Dispose()
                dsFacturas.Dispose()
                .gvListaValidacion.SelectedIndex = -1
                If .gvListaValidacion.Rows.Count() = 0 Then
                    .litError.Text = "No hay registros de pagos anticipados"
                    .btnComprobar.Visible = False
                End If

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#Region "Pago anticipado con pedido de compra"

    Protected Sub btnComprobar_Click(sender As Object, e As EventArgs) Handles btnComprobar.Click

        With Me
            Try
                litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim fecha As DateTime = Date.Now
                Dim selecComp As Integer = 0, solic As Integer = 0, ban As Integer = 0



                For Each row As GridViewRow In .gvListaValidacion.Rows
                    solic = solic + 1
                    If (TryCast(row.FindControl("cbxSeleccionar"), CheckBox)).Checked Then
                        selecComp = selecComp + 1
                    End If
                Next

                If selecComp = 0 Then
                    .litError.Text = "Seleccione un registro para continuar"
                Else
                    For Each row As GridViewRow In .gvListaValidacion.Rows
                        If (TryCast(row.FindControl("cbxSeleccionar"), CheckBox)).Checked Then
                            Dim valor As Integer = 0
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "DECLARE @valorR int;  EXEC SP_I_ms_comprobacion_anticipo_CxP @id_ms_anticipo_proveedor,  @id_usr_cxp_valida, @fecha_valida_cxp, @importe_comprobado, @valorR OUTPUT; SELECT @valorR"
                            SCMValores.Parameters.AddWithValue("@id_ms_anticipo_proveedor", .gvListaValidacion.DataKeys(row.RowIndex).Value.ToString())
                            SCMValores.Parameters.AddWithValue("@importe_comprobado", CDbl(row.Cells(4).Text))
                            SCMValores.Parameters.AddWithValue("@id_usr_cxp_valida", Val(._txtIdUsuario.Text))
                            SCMValores.Parameters.AddWithValue("@fecha_valida_cxp", fecha)

                            ConexionBD.Open()
                            valor = SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            If valor > 0 Then
                                'Insertar Instancia de Anticipo comprobado
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "insert into ms_instancia ( id_ms_sol,  tipo,  id_actividad) " +
                                                     "				    values (@id_ms_sol, @tipo, @id_actividad) "
                                SCMValores.Parameters.AddWithValue("@id_ms_sol", .gvListaValidacion.DataKeys(row.RowIndex).Value.ToString())
                                SCMValores.Parameters.AddWithValue("@tipo", "CAP")
                                SCMValores.Parameters.AddWithValue("@id_actividad", 150)
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()
                                'Obtener ID de la Instancia de Solicitud 
                                SCMValores.CommandText = "select max(id_ms_instancia) from ms_instancia where id_ms_sol = @id_ms_sol and tipo = 'CAP' "
                                ConexionBD.Open()
                                ._txtIdMsInst.Text = SCMValores.ExecuteScalar
                                ConexionBD.Close()

                                'Insertar Históricos
                                SCMValores.Parameters.Clear()
                                SCMValores.CommandText = "insert into ms_historico ( id_ms_instancia,  id_actividad,  id_usr,  fecha) " +
                                                     "				    values (@id_ms_instancia, @id_actividad, @id_usr, @fecha) "
                                SCMValores.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                                SCMValores.Parameters.AddWithValue("@id_actividad", 150)
                                SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                                SCMValores.Parameters.AddWithValue("@fecha", fecha)
                                ConexionBD.Open()
                                SCMValores.ExecuteNonQuery()
                                ConexionBD.Close()
                                ban = 1
                            End If

                        End If
                    Next
                    If ban = 0 Then
                        Server.Transfer("Menu.aspx")
                    Else
                        .listaValidacion()
                        If .gvListaValidacion.Rows.Count() = 0 Then
                            .litError.Text = "No hay registros de pagos anticipados"
                            .btnComprobar.Visible = False
                        End If
                    End If
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub


#End Region

End Class
