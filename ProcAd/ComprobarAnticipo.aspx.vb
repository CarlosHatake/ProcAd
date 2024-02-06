Public Class ComprobarAnticipo
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtIdMsInst.Text = Session("idMsInst")
                        ._txtIdSolicitud.Text = Session("idSolicitud")
                        ._txtBan.Text = 0
                        pantallaInicio()

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        'Nombre del Solicitante
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        'Tope 1
                        SCMValores.CommandText = "  SELECT valor FROM cg_parametros WHERE parametro = 'ingFact_tope1'"
                        ConexionBD.Open()
                        ._txtTope1.Text = SCMValores.ExecuteScalar()
                        ConexionBD.Close()
                        'Tope 2
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "SELECT valor FROM cg_parametros WHERE parametro = 'ingFact_tope2'"
                        ConexionBD.Open()
                        ._txtTope2.Text = SCMValores.ExecuteScalar()
                        ConexionBD.Close()

                        'Pedido de compra
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "SELECT COUNT(*) FROM dt_pedidos_compra WHERE id_ms_anticipo_proveedor = @id_ms_anticipo_proveedor"
                        SCMValores.Parameters.AddWithValue("@id_ms_anticipo_proveedor", Val(._txtIdSolicitud.Text))
                        ConexionBD.Open()
                        ._txtPedidoComp.Text = SCMValores.ExecuteScalar()
                        ConexionBD.Close()
                    Else
                        Server.Transfer("Login.aspx")
                    End If
                Catch ex As Exception

                End Try
            End With
        End If
    End Sub
#End Region

#Region "Funciones"
    Public Sub pantallaInicio()
        With Me
            Try

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim query As String
                Dim sdaSolicitud As New SqlDataAdapter
                Dim dsSolicitud As New DataSet

                query = " SELECT id_ms_anticipo_proveedor, ms_anticipo_proveedor.id_empresa, empresa, tipo_anticipo, importe_requerido, emp.rfc as RFCEmpresa, prov.rfc as RFCProveedor, prov.nombre as Proveedor " +
                        " FROM ms_anticipo_proveedor " +
                        " LEFT JOIN ms_instancia inst ON ms_anticipo_proveedor.id_ms_anticipo_proveedor = inst.id_ms_sol " +
                        " LEFT JOIN bd_Empleado.dbo.cg_empresa emp ON emp.id_empresa = ms_anticipo_proveedor.id_empresa " +
                        " LEFT JOIN bd_SiSAC.dbo.cg_proveedor prov ON ms_anticipo_proveedor.id_proveedor = prov.id_proveedor " +
                        " WHERE inst.id_ms_instancia = @id_ms_instancia "
                sdaSolicitud.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_ms_instancia", Val(._txtIdMsInst.Text))
                ConexionBD.Open()
                sdaSolicitud.Fill(dsSolicitud)
                ConexionBD.Close()
                .lblFolio.Text = dsSolicitud.Tables(0).Rows(0).Item("id_ms_anticipo_proveedor").ToString()
                .lblEmpresa.Text = dsSolicitud.Tables(0).Rows(0).Item("empresa").ToString()
                ._txtIdEmpresa.Text = dsSolicitud.Tables(0).Rows(0).Item("id_empresa").ToString()
                ._txtTipoAnt.Text = dsSolicitud.Tables(0).Rows(0).Item("tipo_anticipo").ToString()
                ._txtImporte.Text = dsSolicitud.Tables(0).Rows(0).Item("importe_requerido").ToString()
                ._txtRFCEmpr.Text = dsSolicitud.Tables(0).Rows(0).Item("RFCEmpresa").ToString()
                ._txtRFCProv.Text = dsSolicitud.Tables(0).Rows(0).Item("RFCProveedor").ToString()
                .lblProveedorNAV.Text = dsSolicitud.Tables(0).Rows(0).Item("Proveedor").ToString()
                .lblNumProveedorNAV.Text = dsSolicitud.Tables(0).Rows(0).Item("RFCProveedor").ToString()

                'Vista de inicio

                'Anticipo sin pedido de compra
                If ._txtTipoAnt.Text = "1" And Val(._txtPedidoComp.Text) = 0 Then
                    datosInicio()
                    .pnlSeccion1.Visible = True
                    .pnlgvFacturas.Visible = False
                    .btnGuardarSeccion1.Enabled = False
                    llenarFactura("INGRESO")
                    ._txtEscenario.Text = "1"

                    'Anticipo con pedido de compra -- NO SE USA
                ElseIf ._txtTipoAnt.Text = "1" And Val(._txtPedidoComp.Text) > 0 Then
                    datosInicio()
                    .pnlSeccion1.Visible = True
                    .pnlgvFacturas.Visible = False
                    .btnGuardarSeccion1.Enabled = False
                    ._txtEscenario.Text = "2"
                    llenarFactura("INGRESO")

                    'Pago anticipado sin pedido de compra
                ElseIf ._txtTipoAnt.Text = "2" And Val(._txtPedidoComp.Text) = 0 Then
                    datosInicio()
                    .pnlSeccion1.Visible = True
                    .pnlgvFacturas.Visible = False
                    .btnGuardarSeccion1.Visible = False
                    .btnAutorizacion.Visible = True
                    .pnlAdjuntos.Visible = True
                    .lbl_TipoOperacion.Visible = True
                    .rbTipoOperacion.Visible = True
                    .llenarFactura("INGRESO")
                    ._txtEscenario.Text = "3"


                    'Pago anticipado con agente aduanal
                ElseIf ._txtTipoAnt.Text = "3" Then
                    datosInicio()
                    llenarProv()
                    .pnlSeccion1.Visible = False
                    .pnlgvFacturas.Visible = False
                    .btnGuardarSeccion1.Enabled = False
                    .lbl_TipoOperacion.Visible = True
                    .rbTipoOperacion.Visible = True
                    .lbl_ProveedorGral.Visible = True
                    .txtProveedor.Visible = True
                    .ibtnBuscarProveedor.Visible = True
                    .ddlProveedor.Visible = True
                    .pnlAdjEvidencias.Visible = False
                    .upGvEvidencias.Visible = False
                    ._txtEscenario.Text = "5"


                End If

            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

    Public Sub datosInicio()
        With Me
            Try
                .lbl_Empresa.Visible = True
                .lblEmpresa.Visible = True
                .lbl_ProvedoorNAV.Visible = True
                .lblProveedorNAV.Visible = True
                .lbl_NumProveedorNAV.Visible = True
                .lblNumProveedorNAV.Visible = True
                .lbl_Autorizador.Visible = True
                .ddlAutorizador.Visible = True
                llenarAutorizador()

            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

    Protected Sub rbTipoOperacion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbTipoOperacion.SelectedIndexChanged
        With Me
            Try
                If .rbTipoOperacion.SelectedIndex = 0 Then
                    .lbl_CC.Visible = True
                    .ddlCC.Visible = True
                    llenarCC()
                    .lbl_Div.Visible = False
                    .ddlDiv.Visible = False

                ElseIf .rbTipoOperacion.SelectedIndex = 1 Then
                    .lbl_Div.Visible = True
                    .ddlDiv.Visible = True
                    llenarDiv()

                    .lbl_CC.Visible = False
                    .ddlCC.Visible = False

                End If
            Catch ex As Exception
                .litError.Text = ""
            End Try

        End With
    End Sub

#End Region

#Region "Anticipo sin pedido de compra "
    Protected Sub btnGuardarSeccion1_Click(sender As Object, e As EventArgs) Handles btnGuardarSeccion1.Click
        With Me
            Try
                If ._txtEscenario.Text = "1" Then
                    .pnlgvFacturas.Visible = True
                    .pnlSeccion1.Visible = False
                    .lbl_TipoOperacion.Visible = True
                    .rbTipoOperacion.Visible = True
                    .pnlAdjuntos.Visible = True

                    llenarFacturaAnticipo("Ingreso")
                    llenarFacturaFact("Ingreso")
                    llenarFacturaEgreso("Egreso")

                    .btnAutorizacion.Visible = True
                End If

                If ._txtEscenario.Text = "2" Then
                    .pnlgvFacturas.Visible = True
                    .pnlSeccion1.Visible = False
                    .lbl_TipoOperacion.Visible = True
                    .rbTipoOperacion.Visible = True
                    .pnlAdjuntos.Visible = True

                    llenarFacturaAnticipo("Ingreso")
                    llenarFacturaFact("Ingreso")
                    llenarFacturaEgreso("Egreso")
                    .btnAutorizacion.Visible = True
                End If


            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

    Protected Sub btnAutorizacion_Click(sender As Object, e As EventArgs) Handles btnAutorizacion.Click
        With Me
            Try
                Dim ban As Integer = 0

                If ._txtEscenario.Text = 1 Then
                    If .gvCFDIAnticipoSec2.SelectedIndex = -1 Or .gvCFDIFacturaSec2.SelectedIndex = -1 Or .gvCFDIEgresoEsc1.SelectedIndex = -1 Then
                        .litError.Text = "Elija los CFDI correspondientes para continuar "
                        ban = 1
                    End If
                End If

                If .rbTipoOperacion.SelectedIndex = -1 Then
                    If ban = 0 Then
                        .litError.Text = .litError.Text + "; "
                    Else ban = 1
                        .litError.Text = .litError.Text + "Elija el tipo de operación"
                    End If
                End If

                If .rbTipoOperacion.SelectedIndex = 0 And ddlCC.SelectedIndex = 0 Then
                    If ban = 1 Then
                        .litError.Text = .litError.Text + "; "
                    Else
                        ban = 1
                    End If
                    .litError.Text = .litError.Text + "Elija un centro de costos válido"
                End If

                If .rbTipoOperacion.SelectedIndex = 1 And ddlDiv.SelectedIndex = 0 Then
                    If ban = 1 Then
                        .litError.Text = .litError.Text + "; "
                    Else
                        ban = 1
                    End If
                    .litError.Text = .litError.Text + "Elija una división válida"
                End If

                If ban = 0 Then
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD

                    Dim fecha As DateTime
                    fecha = Date.Now
                    'Insertar Solicitud
                    Dim sdaSolicitud As New SqlDataAdapter
                    Dim dsSolicitud As New DataSet
                    Dim query As String

                    query = "EXEC SP_I_ms_comprobacion_anticipo @id_ms_anticipo_proveedor, @id_usr_solicita, @fecha_solicita, @tipo_escenario, @tipo_gasto, @id_division, @division, @id_centro_costos ,@centro_costos , @id_usr_autoriza , @autorizador, @id_usr_autoriza2, @autorizador2, @id_usr_autorizador3, @autorizador3, @importe_anticipo, @importe_comprobado, @importe_devolucion, @adj_importe_devolucion "
                    sdaSolicitud.SelectCommand = New SqlCommand(query, ConexionBD)
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_ms_anticipo_proveedor", .lblFolio.Text)
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@tipo_escenario", Val(._txtEscenario.Text))

                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_usr_solicita", Val(._txtIdUsuario.Text))
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@fecha_solicita", fecha)

                    If .rbTipoOperacion.SelectedIndex = 0 Then
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@tipo_gasto", "Administrativo")
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_centro_costos", .ddlCC.SelectedItem.Value)
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@centro_costos", .ddlCC.SelectedItem.Text)
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_division", DBNull.Value)
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@division", DBNull.Value)


                    ElseIf .rbTipoOperacion.SelectedIndex = 1 Then
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@tipo_gasto", "Operativo")
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_division", .ddlDiv.SelectedItem.Value)
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@division", .ddlDiv.SelectedItem.Text)
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_centro_costos", DBNull.Value)
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@centro_costos", DBNull.Value)

                    End If
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_usr_autoriza", .ddlAutorizador.SelectedItem.Value)
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@autorizador", .ddlAutorizador.SelectedItem.Text)

                    If ddlAutorizador2.Visible = True Then
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_usr_autoriza2", ddlAutorizador2.SelectedItem.Value)
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@autorizador2", ddlAutorizador2.SelectedItem.Text)
                    Else
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_usr_autoriza2", DBNull.Value)
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@autorizador2", DBNull.Value)
                    End If

                    If ddlAutorizador3.Visible = True Then
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_usr_autorizador3", ddlAutorizador3.SelectedItem.Value)
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@autorizador3", ddlAutorizador3.SelectedItem.Text)
                    Else
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_usr_autorizador3", DBNull.Value)
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@autorizador3", DBNull.Value)
                    End If

                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@importe_anticipo", Val(._txtImporte.Text))
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@importe_comprobado", Val(._txtImporte.Text))
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@importe_devolucion", DBNull.Value)
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@adj_importe_devolucion", DBNull.Value)

                    ConexionBD.Open()
                    sdaSolicitud.Fill(dsSolicitud)
                    ConexionBD.Close()

                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "select max(id_ms_comprobacion_anticipo) from ms_comprobacion_anticipo where id_usr_solicita = @id_usr_solicita and estatus not in ('Z') "
                    SCMValores.Parameters.AddWithValue("@id_usr_solicita", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    .lblFolio.Text = SCMValores.ExecuteScalar
                    ConexionBD.Close()
                    If Val(.lblFolio.Text) > 0 Then
                        ._txtBan.Text = 1
                    End If

                    If ._txtBan.Text = 1 Then

                        'Insertar datos de las facturas en detalle de comprobación

                        If ._txtEscenario.Text = "1" Then

                            'Se insertan los datos de la factura CFDI ANTICIPO
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "EXEC SP_I_dt_comprobacion_anticipo @id_ms_comp_anticipo, @id_dt_factura, @fecha_emision, @uuid, @serie, @folio, @lugar_exp, @forma_pago, @moneda, @subtotal, @importe, @id_usr_carga"
                            SCMValores.Parameters.AddWithValue("@id_ms_comp_anticipo", .lblFolio.Text)
                            SCMValores.Parameters.AddWithValue("@id_dt_factura", (.gvCFDIAnticipoSec2.DataKeys(gvCFDIAnticipoSec2.SelectedIndex).Values("id_dt_factura")))
                            SCMValores.Parameters.AddWithValue("@fecha_emision", CDate(.gvCFDIAnticipoSec2.Rows(.gvCFDIAnticipoSec2.SelectedIndex).Cells(1).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@uuid", (.gvCFDIAnticipoSec2.Rows(.gvCFDIAnticipoSec2.SelectedIndex).Cells(2).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@serie", (.gvCFDIAnticipoSec2.Rows(.gvCFDIAnticipoSec2.SelectedIndex).Cells(3).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@folio", (.gvCFDIAnticipoSec2.Rows(.gvCFDIAnticipoSec2.SelectedIndex).Cells(4).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@lugar_exp", (.gvCFDIAnticipoSec2.Rows(.gvCFDIAnticipoSec2.SelectedIndex).Cells(5).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@forma_pago", (.gvCFDIAnticipoSec2.Rows(.gvCFDIAnticipoSec2.SelectedIndex).Cells(6).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@moneda", (.gvCFDIAnticipoSec2.Rows(.gvCFDIAnticipoSec2.SelectedIndex).Cells(7).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@subtotal", (.gvCFDIAnticipoSec2.Rows(.gvCFDIAnticipoSec2.SelectedIndex).Cells(8).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@importe", (.gvCFDIAnticipoSec2.Rows(.gvCFDIAnticipoSec2.SelectedIndex).Cells(9).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@id_usr_carga", Val(._txtIdUsuario.Text))
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            'Se insertan los datos de la factura CFDI FACTURA
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "EXEC SP_I_dt_comprobacion_anticipo @id_ms_comp_anticipo, @id_dt_factura, @fecha_emision, @uuid, @serie, @folio, @lugar_exp, @forma_pago, @moneda, @subtotal, @importe, @id_usr_carga"
                            SCMValores.Parameters.AddWithValue("@id_ms_comp_anticipo", .lblFolio.Text)
                            SCMValores.Parameters.AddWithValue("@id_dt_factura", (.gvCFDIFacturaSec2.DataKeys(gvCFDIFacturaSec2.SelectedIndex).Values("id_dt_factura")))
                            SCMValores.Parameters.AddWithValue("@fecha_emision", CDate(.gvCFDIFacturaSec2.Rows(.gvCFDIFacturaSec2.SelectedIndex).Cells(1).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@uuid", (.gvCFDIFacturaSec2.Rows(.gvCFDIFacturaSec2.SelectedIndex).Cells(2).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@serie", (.gvCFDIFacturaSec2.Rows(.gvCFDIFacturaSec2.SelectedIndex).Cells(3).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@folio", (.gvCFDIFacturaSec2.Rows(.gvCFDIFacturaSec2.SelectedIndex).Cells(4).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@lugar_exp", (.gvCFDIFacturaSec2.Rows(.gvCFDIFacturaSec2.SelectedIndex).Cells(5).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@forma_pago", (.gvCFDIFacturaSec2.Rows(.gvCFDIFacturaSec2.SelectedIndex).Cells(6).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@moneda", (.gvCFDIFacturaSec2.Rows(.gvCFDIFacturaSec2.SelectedIndex).Cells(7).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@subtotal", (.gvCFDIFacturaSec2.Rows(.gvCFDIFacturaSec2.SelectedIndex).Cells(8).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@importe", (.gvCFDIFacturaSec2.Rows(.gvCFDIFacturaSec2.SelectedIndex).Cells(9).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@id_usr_carga", Val(._txtIdUsuario.Text))
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                            'Se insertan los datos de la factura CFDI FACTURA
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "EXEC SP_I_dt_comprobacion_anticipo @id_ms_comp_anticipo, @id_dt_factura, @fecha_emision, @uuid, @serie, @folio, @lugar_exp, @forma_pago, @moneda, @subtotal, @importe, @id_usr_carga"
                            SCMValores.Parameters.AddWithValue("@id_ms_comp_anticipo", .lblFolio.Text)
                            SCMValores.Parameters.AddWithValue("@id_dt_factura", (.gvCFDIEgresoEsc1.DataKeys(gvCFDIEgresoEsc1.SelectedIndex).Values("id_dt_factura")))
                            SCMValores.Parameters.AddWithValue("@fecha_emision", CDate(.gvCFDIEgresoEsc1.Rows(.gvCFDIEgresoEsc1.SelectedIndex).Cells(1).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@uuid", (.gvCFDIEgresoEsc1.Rows(.gvCFDIEgresoEsc1.SelectedIndex).Cells(2).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@serie", (.gvCFDIEgresoEsc1.Rows(.gvCFDIEgresoEsc1.SelectedIndex).Cells(3).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@folio", (.gvCFDIEgresoEsc1.Rows(.gvCFDIEgresoEsc1.SelectedIndex).Cells(4).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@lugar_exp", (.gvCFDIEgresoEsc1.Rows(.gvCFDIEgresoEsc1.SelectedIndex).Cells(5).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@forma_pago", (.gvCFDIEgresoEsc1.Rows(.gvCFDIEgresoEsc1.SelectedIndex).Cells(6).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@moneda", (.gvCFDIEgresoEsc1.Rows(.gvCFDIEgresoEsc1.SelectedIndex).Cells(7).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@subtotal", (.gvCFDIEgresoEsc1.Rows(.gvCFDIEgresoEsc1.SelectedIndex).Cells(8).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@importe", (.gvCFDIEgresoEsc1.Rows(.gvCFDIEgresoEsc1.SelectedIndex).Cells(9).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@id_usr_carga", Val(._txtIdUsuario.Text))
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                        ElseIf ._txtEscenario.Text = "3" Then

                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "EXEC SP_I_dt_comprobacion_anticipo @id_ms_comp_anticipo, @id_dt_factura, @fecha_emision, @uuid, @serie, @folio, @lugar_exp, @forma_pago, @moneda, @subtotal, @importe, @id_usr_carga"
                            SCMValores.Parameters.AddWithValue("@id_ms_comp_anticipo", .lblFolio.Text)
                            SCMValores.Parameters.AddWithValue("@id_dt_factura", (.gvCFDIAnticipoEsc1.DataKeys(gvCFDIAnticipoEsc1.SelectedIndex).Values("id_dt_factura")))
                            SCMValores.Parameters.AddWithValue("@fecha_emision", CDate(.gvCFDIAnticipoEsc1.Rows(.gvCFDIAnticipoEsc1.SelectedIndex).Cells(1).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@uuid", (.gvCFDIAnticipoEsc1.Rows(.gvCFDIAnticipoEsc1.SelectedIndex).Cells(2).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@serie", (.gvCFDIAnticipoEsc1.Rows(.gvCFDIAnticipoEsc1.SelectedIndex).Cells(3).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@folio", (.gvCFDIAnticipoEsc1.Rows(.gvCFDIAnticipoEsc1.SelectedIndex).Cells(4).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@lugar_exp", (.gvCFDIAnticipoEsc1.Rows(.gvCFDIAnticipoEsc1.SelectedIndex).Cells(5).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@forma_pago", (.gvCFDIAnticipoEsc1.Rows(.gvCFDIAnticipoEsc1.SelectedIndex).Cells(6).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@moneda", (.gvCFDIAnticipoEsc1.Rows(.gvCFDIAnticipoEsc1.SelectedIndex).Cells(7).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@subtotal", (.gvCFDIAnticipoEsc1.Rows(.gvCFDIAnticipoEsc1.SelectedIndex).Cells(8).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@importe", (.gvCFDIAnticipoEsc1.Rows(.gvCFDIAnticipoEsc1.SelectedIndex).Cells(9).Text.ToString))
                            SCMValores.Parameters.AddWithValue("@id_usr_carga", Val(._txtIdUsuario.Text))
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()

                        End If



                        'Insertar el id_ms_anticipo_proveedor a la tabla dt_archivo_adj_anticipo
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update dt_archivo_adj_comp_anticipo set id_ms_comp_anticipo_proveedor = @id_ms_comp_anticipo_proveedor where id_ms_comp_anticipo_proveedor = -1 and id_usuario = @id_usuario"
                        SCMValores.Parameters.AddWithValue("@id_ms_comp_anticipo_proveedor", Val(.lblFolio.Text))
                        SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Insertar Instancia de Solicitud de Anticipo
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_instancia ( id_ms_sol,  tipo,  id_actividad) " +
                                                 "				    values (@id_ms_sol, @tipo, @id_actividad) "
                        SCMValores.Parameters.AddWithValue("@id_ms_sol", Val(.lblFolio.Text))
                        SCMValores.Parameters.AddWithValue("@tipo", "CAP")
                        SCMValores.Parameters.AddWithValue("@id_actividad", 142)
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
                        SCMValores.Parameters.AddWithValue("@id_actividad", 142)
                        SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@fecha", fecha)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Solicitante 
                        'Dim solicitante As String
                        'SCMValores.Parameters.Clear()
                        'SCMValores.CommandText = "SELECT empleado_solicita FROM ms_comprobacion_anticipo WHERE id_ms_comprobacion_anticipo = @id_ms_comprobacion_anticipo "
                        'SCMValores.Parameters.AddWithValue("@id_ms_comprobacion_anticipo", Val(.lblFolio.Text))
                        'ConexionBD.Open()
                        'solicitante = SCMValores.ExecuteScalar
                        'ConexionBD.Close()

                        ''Envío de Correo
                        'Dim Mensaje As New System.Net.Mail.MailMessage()
                        'Dim destinatario As String = ""
                        ''Obtener el Correo del Solicitante
                        'SCMValores.CommandText = "select cgEmpl.correo " +
                        '                                 " from ms_comprobacion_anticipo " +
                        '                                 "  left join cg_usuario on ms_comprobacion_anticipo.id_usr_autoriza = cg_usuario.id_usuario  " +
                        '                                 "  left join bd_empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado  " +
                        '                                 "  where id_ms_comprobacion_anticipo = @id_ms_anticipo_proveedor "
                        'SCMValores.Parameters.AddWithValue("@id_ms_anticipo_proveedor", Val(.lblFolio.Text))
                        'ConexionBD.Open()
                        'destinatario = SCMValores.ExecuteScalar()
                        'ConexionBD.Close()

                        'Mensaje.[To].Add(destinatario)
                        'Mensaje.Bcc.Add("notificaciones.procad@unne.com.mx")
                        'Mensaje.From = New MailAddress("notificaciones.procad@unne.com.mx")
                        'Mensaje.Subject = "ProcAd - Solicitud de Comprobación de Anticipo No. " + .lblFolio.Text + " por Autorizar"
                        'Dim texto As String
                        'texto = "<span style=""font-family:Verdana;font-size: 10pt;"">" +
                        '               "Buen día:
                        '         <br>
                        '         <br> Por medio de la presente le informamos que se generó la solicitud de comprobación de anticipo número <b>" + .lblFolio.Text +
                        '           "</b> por parte de <b>" + solicitante + "</b>
                        '        <br>
                        '        <br>Favor de validar si procede la Solicitud de comprobación Anticipo de proveedor"
                        'Mensaje.Body = texto
                        'Mensaje.IsBodyHtml = True
                        'Mensaje.Priority = MailPriority.Normal

                        'Dim Servidor As New SmtpClient()
                        'Servidor.Host = "10.10.10.30"
                        'Servidor.Port = 587
                        'Servidor.EnableSsl = False
                        'Servidor.UseDefaultCredentials = False
                        'Servidor.Credentials = New System.Net.NetworkCredential("nprocad", "mc8HLB8lPe78")

                        'Try
                        '    Servidor.Send(Mensaje)
                        'Catch ex As System.Net.Mail.SmtpException
                        '    litError.Text = ex.ToString
                        'End Try

                        .pnlInicio.Enabled = False
                        .pnlInformacionGeneral.Enabled = False

                        End If

                    End If
            Catch ex As Exception

            End Try
        End With
    End Sub


    Public Sub llenarFactura(ByVal tipo)
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaFacturas As New SqlDataAdapter
                Dim dsFacturas As New DataSet
                .gvCFDIAnticipoEsc1.DataSource = dsFacturas

                sdaFacturas.SelectCommand = New SqlCommand("SP_C_dt_facturaLlenado_anticipos @rfcProv, @rfcEmpr, @cfdi, @idUsuario, @fecha, @tipo, @importe, @id_dt_factura, @consulta ", ConexionBD)

                sdaFacturas.SelectCommand.Parameters.AddWithValue("@rfcProv", ._txtRFCProv.Text)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@rfcEmpr", ._txtRFCEmpr.Text)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@cfdi", DBNull.Value)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@fecha", Date.Now)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@tipo", tipo)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@importe", Val(._txtImporte.Text))
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@id_dt_factura", DBNull.Value)

                If ._txtTipoAnt.Text = "3" Then
                    sdaFacturas.SelectCommand.Parameters.AddWithValue("@consulta", "MP")
                Else
                    sdaFacturas.SelectCommand.Parameters.AddWithValue("@consulta", DBNull.Value)

                End If
                ConexionBD.Open()
                sdaFacturas.Fill(dsFacturas)
                .gvCFDIAnticipoEsc1.DataBind()
                ConexionBD.Close()
                sdaFacturas.Dispose()
                dsFacturas.Dispose()
                .gvCFDIAnticipoEsc1.SelectedIndex = -1

                If .gvCFDIAnticipoEsc1.Rows.Count() = 0 Then
                    .gvCFDIAnticipoEsc1.Visible = False
                    .litError.Text = "No existe registros para el anticipo"
                    .lblCFDI.Visible = False
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarFacturaAnticipo(ByVal tipo)
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaFacturas As New SqlDataAdapter
                Dim dsFacturas As New DataSet
                .gvCFDIAnticipoSec2.DataSource = dsFacturas

                sdaFacturas.SelectCommand = New SqlCommand("SELECT * FROM VTA_C_dt_Factura_Anticipo AS VTA WHERE id_dt_factura = @id_dt_factura AND tipo = @tipo ", ConexionBD)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@id_dt_factura", .gvCFDIAnticipoEsc1.DataKeys(gvCFDIAnticipoEsc1.SelectedIndex).Values("id_dt_factura"))
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@tipo", tipo)
                ConexionBD.Open()
                sdaFacturas.Fill(dsFacturas)
                .gvCFDIAnticipoSec2.DataBind()
                ConexionBD.Close()
                sdaFacturas.Dispose()
                dsFacturas.Dispose()
                .gvCFDIAnticipoSec2.SelectedIndex = -1

                If .gvCFDIAnticipoSec2.Rows.Count() = 0 Then
                    .gvCFDIAnticipoSec2.Visible = False
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub


    Public Sub llenarFacturaFact(ByVal tipo)
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaFacturas As New SqlDataAdapter
                Dim dsFacturas As New DataSet
                .gvCFDIFacturaSec2.DataSource = dsFacturas

                sdaFacturas.SelectCommand = New SqlCommand("SP_C_dt_facturaLlenado_anticipos @rfcProv, @rfcEmpr, @cfdi, @idUsuario, @fecha, @tipo, @importe, @id_dt_factura, @consulta ", ConexionBD)

                sdaFacturas.SelectCommand.Parameters.AddWithValue("@rfcProv", ._txtRFCProv.Text)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@rfcEmpr", ._txtRFCEmpr.Text)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@cfdi", DBNull.Value)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@fecha", Date.Now)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@tipo", tipo)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@importe", Val(._txtImporte.Text))
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@id_dt_factura", .gvCFDIAnticipoEsc1.DataKeys(gvCFDIAnticipoEsc1.SelectedIndex).Values("id_dt_factura"))
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@consulta", "CFDI FACTURA")


                ConexionBD.Open()
                sdaFacturas.Fill(dsFacturas)
                .gvCFDIFacturaSec2.DataBind()
                ConexionBD.Close()
                sdaFacturas.Dispose()
                dsFacturas.Dispose()
                .gvCFDIFacturaSec2.SelectedIndex = -1

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarFacturaEgreso(ByVal tipo)
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaFacturas As New SqlDataAdapter
                Dim dsFacturas As New DataSet
                .gvCFDIEgresoEsc1.DataSource = dsFacturas

                sdaFacturas.SelectCommand = New SqlCommand("SP_C_dt_facturaLlenado_anticipos @rfcProv, @rfcEmpr, @cfdi, @idUsuario, @fecha, @tipo, @importe, @id_dt_factura, @consulta", ConexionBD)

                sdaFacturas.SelectCommand.Parameters.AddWithValue("@rfcProv", ._txtRFCProv.Text)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@rfcEmpr", ._txtRFCEmpr.Text)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@cfdi", DBNull.Value)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@fecha", Date.Now)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@tipo", tipo)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@importe", Val(._txtImporte.Text))
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@id_dt_factura", DBNull.Value)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@consulta", DBNull.Value)
                ConexionBD.Open()
                sdaFacturas.Fill(dsFacturas)
                .gvCFDIEgresoEsc1.DataBind()
                ConexionBD.Close()
                sdaFacturas.Dispose()
                dsFacturas.Dispose()
                .gvCFDIEgresoEsc1.SelectedIndex = -1

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub



    Protected Sub gvRegistrosA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvCFDIAnticipoEsc1.SelectedIndexChanged
        With Me
            Try
                .btnGuardarSeccion1.Enabled = True
            Catch ex As Exception

            End Try
        End With
    End Sub

    Protected Sub btnAgregarAdj_Click(sender As Object, e As EventArgs) Handles btnAgregarAdj.Click
        With Me
            Try
                .litError.Text = ""
                .lblMessage.Text = ""


                ' '' Ruta Local
                Dim sFileDir As String = "C:/ProcAd - Adjuntos CompAnt/" 'Ruta en que se almacenará el archivo
                ' Ruta en Atenea
                ' Dim sFileDir As String = "D:\ProcAd - Adjuntos IngFact\" 'Ruta en que se almacenará el archivo
                Dim lMaxFileSize As Long = 10485760 'Equivalente a 10 Megabytes

                'Verificar que el archivo ha sido seleccionado y es un archivo válido
                If (Not fuAdjunto.PostedFile Is Nothing) And (fuAdjunto.PostedFile.ContentLength > 0) Then
                    'Determinar el nombre original del archivo
                    Dim sFileName As String = System.IO.Path.GetFileName(fuAdjunto.PostedFile.FileName)
                    Dim idArchivo As Integer 'Index correspondiente al archivo
                    Dim fecha As DateTime = Date.Now
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    Try
                        If fuAdjunto.PostedFile.ContentLength <= lMaxFileSize Then
                            'Registrar el archivo en la base de datos
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "INSERT INTO dt_archivo_adj_comp_anticipo(id_ms_comp_anticipo_proveedor, id_usuario, nombre, fecha ) values(-1, @id_usuario, @nombre, @fecha)"
                            SCMValores.Parameters.AddWithValue("@id_usuario", Val(Me._txtIdUsuario.Text))
                            SCMValores.Parameters.AddWithValue("@nombre", sFileName)
                            SCMValores.Parameters.AddWithValue("@fecha", fecha)
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()
                            'Obtener el Id del archivo
                            SCMValores.CommandText = "select max(id_dt_archivo_adj_comp_anticipo) from dt_archivo_adj_comp_anticipo where (id_ms_comp_anticipo_proveedor = -1) and (fecha = @fecha)"
                            ConexionBD.Open()
                            idArchivo = SCMValores.ExecuteScalar
                            ConexionBD.Close()
                            'Se agrega el Id al nombre del archivo
                            sFileName = idArchivo.ToString + "-" + sFileName
                            'Almacenar el archivo en la ruta especificada
                            fuAdjunto.PostedFile.SaveAs(sFileDir + sFileName)
                            'lblMessage.Visible = True
                            'lblMessage.Text = "El archivo: " + sFileName + " se ha adjuntado exitosamente"

                            actualizarAdjuntosEsc1()


                        Else
                            'Rechazar el archivo
                            lblMessage.Visible = True
                            lblMessage.Text = "El archivo excede el límite de 10 MB"
                        End If
                    Catch exc As Exception    'En caso de error
                        'Eliminar el archivo en la base de datos
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "delete from dt_archivo_adj_comp_anticipo where id_dt_archivo_adj_comp_anticipo = @idArchivo"
                        SCMValores.Parameters.AddWithValue("@idArchivo", idArchivo)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        lblMessage.Visible = True
                        lblMessage.Text = lblMessage.Text + ". Un Error ha ocurrido. Favor de intentarlo nuevamente"
                        DeleteFile(sFileDir + sFileName)
                    End Try
                Else
                    lblMessage.Visible = True
                    lblMessage.Text = "Favor de seleccionar un Archivo"
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub


    Public Sub actualizarAdjuntosEsc1()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaArchivos As New SqlDataAdapter
                Dim dsArchivos As New DataSet
                .gvAdjuntosEscenario1.DataSource = dsArchivos
                'Adjuntos
                sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " +
                                                           "     , 'http://148.223.153.43/ProcAd - Adjuntos CompAnt/' + cast(id_dt_archivo_adj_comp_anticipo as varchar(20)) + '-' + nombre as path " +
                                                           "from dt_archivo_adj_comp_anticipo " +
                                                           "where id_ms_comp_anticipo_proveedor = -1 " +
                                                            "  and id_usuario = @idUsuario ", ConexionBD)
                sdaArchivos.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                sdaArchivos.Fill(dsArchivos)
                .gvAdjuntosEscenario1.DataBind()
                ConexionBD.Close()
                sdaArchivos.Dispose()
                dsArchivos.Dispose()
                .gvAdjuntosEscenario1.SelectedIndex = -1
                .upAdjuntosEscenario1.Update()
                If gvAdjuntosEscenario1.Rows.Count > 0 Then
                    .upAdjuntosEscenario1.Visible = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub



#End Region

#Region "Pago anticipado agente aduanal"

    Public Sub llenarProv()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaProv As New SqlDataAdapter
                Dim dsProv As New DataSet
                Dim query As String
                query = " SELECT DISTINCT(rfc) as rfc_emisor, id_proveedor, rfc + ' / ' + nombre AS proveedor " +
                        " FROM bd_SiSAC.dbo.cg_proveedor " +
                        " WHERE (rfc <> '' AND rfc IS NOT NULL) " +
                        " AND (rfc LIKE '%' + @valor+ '%' OR nombre LIKE '%' + @valor+ '%') "

                sdaProv.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaProv.SelectCommand.Parameters.AddWithValue("@valor", .txtProveedor.Text.Trim)
                .ddlProveedor.DataSource = dsProv
                .ddlProveedor.DataTextField = "proveedor"
                .ddlProveedor.DataValueField = "rfc_emisor"
                ConexionBD.Open()
                sdaProv.Fill(dsProv)
                .ddlProveedor.DataBind()
                ConexionBD.Close()
                sdaProv.Dispose()
                dsProv.Dispose()
                .ddlProveedor.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Function devolucion()
        With Me
            Try
                Dim importeComprobado As Integer = 0, importeDevolucion As Double

                'Se suman los importes de las facturas ingresadas

                For Each row As GridViewRow In gvAgenteAduanal.Rows
                    importeComprobado = importeComprobado + row.Cells(9).Text.ToString()
                Next

                'Se calcula el importe a devolver
                importeDevolucion = Val(._txtImporte) - importeComprobado

                'Se asignan los montos
                ._ImporteComprobado.Text = importeComprobado
                ._ImporteDevolucion.Text = importeDevolucion

                If importeDevolucion > 0 Then
                    devolucion = True
                    .upValeIng.Visible = True
                Else
                    devolucion = False
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString()
                devolucion = False
            End Try
        End With
    End Function

    Protected Sub ibtnProveedorAgenteAduanal_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBuscarProveedor.Click
        With Me
             .upProveedor.Update()
            llenarProv()
            llenargvFacturasProveedor("INGRESO")

        End With
    End Sub

    Public Sub llenargvFacturasProveedor(ByVal tipo)

        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaFacturas As New SqlDataAdapter
                Dim dsFacturas As New DataSet
                .gvFacturasProveedor.DataSource = dsFacturas

                sdaFacturas.SelectCommand = New SqlCommand("SP_C_dt_facturaLlenado_anticipos @rfcProv, @rfcEmpr, @cfdi, @idUsuario, @fecha, @tipo, @importe, @id_dt_factura, @consulta ", ConexionBD)

                sdaFacturas.SelectCommand.Parameters.AddWithValue("@rfcProv", .ddlProveedor.SelectedItem.Value)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@rfcEmpr", ._txtRFCEmpr.Text)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@cfdi", DBNull.Value)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@fecha", Date.Now)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@tipo", tipo)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@importe", DBNull.Value)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@id_dt_factura", DBNull.Value)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@consulta", "ESC5")


                ConexionBD.Open()
                sdaFacturas.Fill(dsFacturas)
                .gvFacturasProveedor.DataBind()
                ConexionBD.Close()
                sdaFacturas.Dispose()
                dsFacturas.Dispose()
                .gvFacturasProveedor.SelectedIndex = -1

                If .gvFacturasProveedor.Rows.Count() = 0 Then
                    .gvFacturasProveedor.Visible = False
                    .litError.Text = "No existe registros para el proveedor seleccionado"
                End If

            Catch ex As Exception
            .litError.Text = ex.ToString()
            End Try
        End With
    End Sub

    Protected Sub gvFacturasProveedor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvFacturasProveedor.SelectedIndexChanged
        With Me
            Try
                .ibtnAltaProveedor.Enabled = True
            Catch ex As Exception

            End Try
        End With
    End Sub

    Protected Sub ibtnAltaProveedor_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAltaProveedor.Click
        With Me
            Try

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD

                Dim fecha As DateTime
                fecha = Date.Now
                'Insertar facturas del proveedor
                Dim sdaSolicitud As New SqlDataAdapter
                Dim dsSolicitud As New DataSet
                Dim query As String

                query = "EXEC SP_I_dt_comprobacion_anticipo @id_ms_comp_anticipo, @id_dt_factura, @fecha_emision, @uuid, @serie, @folio, @lugar_exp, @forma_pago, @moneda, @subtotal, @importe, @id_usr_carga"
                sdaSolicitud.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_ms_comp_anticipo", "-1")
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_dt_factura", (.gvFacturasProveedor.DataKeys(gvFacturasProveedor.SelectedIndex).Values("id_dt_factura")))
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@fecha_emision", CDate(.gvFacturasProveedor.Rows(.gvFacturasProveedor.SelectedIndex).Cells(1).Text.ToString))
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@uuid", (.gvFacturasProveedor.Rows(.gvFacturasProveedor.SelectedIndex).Cells(2).Text.ToString))
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@serie", (.gvFacturasProveedor.Rows(.gvFacturasProveedor.SelectedIndex).Cells(3).Text.ToString))
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@folio", (.gvFacturasProveedor.Rows(.gvFacturasProveedor.SelectedIndex).Cells(4).Text.ToString))
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@lugar_exp", (.gvFacturasProveedor.Rows(.gvFacturasProveedor.SelectedIndex).Cells(5).Text.ToString))
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@forma_pago", (.gvFacturasProveedor.Rows(.gvFacturasProveedor.SelectedIndex).Cells(6).Text.ToString))
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@moneda", (.gvFacturasProveedor.Rows(.gvFacturasProveedor.SelectedIndex).Cells(7).Text.ToString))
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@subtotal", (.gvFacturasProveedor.Rows(.gvFacturasProveedor.SelectedIndex).Cells(8).Text.ToString))
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@importe", (.gvFacturasProveedor.Rows(.gvFacturasProveedor.SelectedIndex).Cells(9).Text.ToString))
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_usr_carga", Val(._txtIdUsuario.Text))

                ConexionBD.Open()
                sdaSolicitud.Fill(dsSolicitud)
                ConexionBD.Close()

                llenargvAgenteAduanal()

                'Verifica si se requiere agregar vale de ingreso
                devolucion()

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnBajaProveedor_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBajaProveedor.Click
        With Me
            Try

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD

                Dim fecha As DateTime
                fecha = Date.Now
                'Insertar facturas del proveedor
                Dim sdaSolicitud As New SqlDataAdapter
                Dim dsSolicitud As New DataSet
                Dim query As String

                query = "DELETE FROM dt_comprobacion_anticipo WHERE id_dt_factura = @id_dt_factura AND id_ms_comp_anticipo = -1 "
                sdaSolicitud.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_dt_factura", (.gvAgenteAduanal.DataKeys(gvAgenteAduanal.SelectedIndex).Values("id_dt_factura")))
                ConexionBD.Open()
                sdaSolicitud.Fill(dsSolicitud)
                ConexionBD.Close()

                llenargvAgenteAduanal()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenargvAgenteAduanal()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaFacturas As New SqlDataAdapter
                Dim dsFacturas As New DataSet
                .gvAgenteAduanal.DataSource = dsFacturas

                sdaFacturas.SelectCommand = New SqlCommand(" SELECT id_dt_factura, fecha_emision, uuid, serie, folio, lugar_exp, forma_pago, moneda, subtotal,importe " +
                                                           " FROM dt_comprobacion_anticipo " +
                                                           " WHERE id_ms_comp_anticipo = -1 AND id_usr_carga = @id_usr_carga ", ConexionBD)
                sdaFacturas.SelectCommand.Parameters.AddWithValue("@id_usr_carga", Val(._txtIdUsuario.Text))

                ConexionBD.Open()
                sdaFacturas.Fill(dsFacturas)
                .gvAgenteAduanal.DataBind()
                ConexionBD.Close()
                sdaFacturas.Dispose()
                dsFacturas.Dispose()
                .gvAgenteAduanal.SelectedIndex = -1

            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With

    End Sub

    Protected Sub gvAgenteAduanal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvAgenteAduanal.SelectedIndexChanged
        With Me
            Try
                .ibtnAlta.Enabled = True
            Catch ex As Exception

            End Try
        End With
    End Sub

    Protected Sub gvEvidencias_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvEvidencias.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                .ibtnBaja.Enabled = True
                .ibtnBaja.ImageUrl = "images\Trash.png"
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnEvidenciaAgenteAduanal_Click(sender As Object, e As EventArgs) Handles btnEvidenciaAgenteAduanal.Click
        With Me
            Try
                .litError.Text = ""
                .lblMessage.Text = ""


                ' '' Ruta Local
                Dim sFileDir As String = "C:/ProcAd - Adjuntos CompAnt/" 'Ruta en que se almacenará el archivo
                ' Ruta en Atenea
                ' Dim sFileDir As String = "D:\ProcAd - Adjuntos IngFact\" 'Ruta en que se almacenará el archivo
                Dim lMaxFileSize As Long = 10485760 'Equivalente a 10 Megabytes

                'Verificar que el archivo ha sido seleccionado y es un archivo válido
                If (Not fuAdjunto.PostedFile Is Nothing) And (fuAdjunto.PostedFile.ContentLength > 0) Then
                    'Determinar el nombre original del archivo
                    Dim sFileName As String = System.IO.Path.GetFileName(fuAdjunto.PostedFile.FileName)
                    Dim idArchivo As Integer 'Index correspondiente al archivo
                    Dim fecha As DateTime = Date.Now
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    Try
                        If fuAdjunto.PostedFile.ContentLength <= lMaxFileSize Then
                            'Registrar el archivo en la base de datos
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "INSERT INTO dt_archivo_adj_comp_anticipo(id_ms_comp_anticipo_proveedor, id_usuario, nombre, fecha ) values(-1, @id_usuario, @nombre, @fecha)"
                            SCMValores.Parameters.AddWithValue("@id_usuario", Val(Me._txtIdUsuario.Text))
                            SCMValores.Parameters.AddWithValue("@nombre", sFileName)
                            SCMValores.Parameters.AddWithValue("@fecha", fecha)
                            ConexionBD.Open()
                            SCMValores.ExecuteNonQuery()
                            ConexionBD.Close()
                            'Obtener el Id del archivo
                            SCMValores.CommandText = "select max(id_dt_archivo_adj_comp_anticipo) from dt_archivo_adj_comp_anticipo where (id_ms_comp_anticipo_proveedor = -1) and (fecha = @fecha)"
                            ConexionBD.Open()
                            idArchivo = SCMValores.ExecuteScalar
                            ConexionBD.Close()
                            'Se agrega el Id al nombre del archivo
                            sFileName = idArchivo.ToString + "-" + sFileName
                            'Almacenar el archivo en la ruta especificada
                            fuAdjunto.PostedFile.SaveAs(sFileDir + sFileName)
                            'lblMessage.Visible = True
                            'lblMessage.Text = "El archivo: " + sFileName + " se ha adjuntado exitosamente"

                            actualizarAdjuntosEsc5()

                        Else
                            'Rechazar el archivo
                            lblMessage.Visible = True
                            lblMessage.Text = "El archivo excede el límite de 10 MB"
                        End If
                    Catch exc As Exception    'En caso de error
                        'Eliminar el archivo en la base de datos
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "delete from dt_archivo_adj_comp_anticipo where id_dt_archivo_adj_comp_anticipo = @idArchivo"
                        SCMValores.Parameters.AddWithValue("@idArchivo", idArchivo)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        lblMessage.Visible = True
                        lblMessage.Text = lblMessage.Text + ". Un Error ha ocurrido. Favor de intentarlo nuevamente"
                        DeleteFile(sFileDir + sFileName)
                    End Try
                Else
                    lblMessage.Visible = True
                    lblMessage.Text = "Favor de seleccionar un Archivo"
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub


    Public Sub actualizarAdjuntosEsc5()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaArchivos As New SqlDataAdapter
                Dim dsArchivos As New DataSet
                .gvEvidencias.DataSource = dsArchivos
                'Adjuntos
                sdaArchivos.SelectCommand = New SqlCommand("select nombre as archivo " +
                                                           "     , 'http://148.223.153.43/ProcAd - Adjuntos CompAnt/' + cast(id_dt_archivo_adj_comp_anticipo as varchar(20)) + '-' + nombre as path " +
                                                           "from dt_archivo_adj_comp_anticipo " +
                                                           "where id_ms_comp_anticipo_proveedor = -1 " +
                                                            "  and id_usuario = @idUsuario ", ConexionBD)
                sdaArchivos.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                sdaArchivos.Fill(dsArchivos)
                .gvEvidencias.DataBind()
                ConexionBD.Close()
                sdaArchivos.Dispose()
                dsArchivos.Dispose()
                .gvEvidencias.SelectedIndex = -1
                .upGvEvidencias.Update()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub
    Public Sub bloquearPantalla()
        With Me
            Try
                .ibtnAlta.Enabled = False
                .ibtnBaja.Enabled = False
                .ibtnBaja.ImageUrl = "images\Trash_i2.png"
                .ibtnAltaProveedor.Enabled = False
                .ibtnBajaProveedor.Enabled = False
                .ibtnBajaProveedor.ImageUrl = "images\Trash_i2.png"
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With
    End Sub


    Protected Sub ibtnAlta_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnAlta.Click
        With Me
            Try
                .pnlAdjEvidencias.Visible = True
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnBaja_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBaja.Click
        With Me
            Try
                ' '' Ruta Local
                Dim sFileDir As String = "C:/ProcAd - Adjuntos CompAnt/" 'Ruta en que se almacenará el archivo
                ' Ruta en Atenea
                ' Dim sFileDir As String = "D:\ProcAd - Adjuntos CompAnt\" 'Ruta en que se almacenará el archivo
                Dim sFileName
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                sdaCatalogo.SelectCommand = New SqlCommand("DELETE FROM dt_archivo_adj_comp_anticipo WHERE id_dt_factura = @id_dt_factura", ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_lugar", (.gvAgenteAduanal.DataKeys(gvAgenteAduanal.SelectedIndex).Values("id_dt_factura")))
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                bloquearPantalla()
                sFileName = gvEvidencias.DataKeys(gvEvidencias.SelectedIndex).Values("id_dt_archivo_adj_comp_anticipo").ToString + "-" + gvEvidencias.DataKeys(gvEvidencias.SelectedIndex).Values("nombre")
                DeleteFile(sFileDir + sFileName)

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnAceptarAgenteAduanal_Click(sender As Object, e As EventArgs) Handles btnAceptarAgenteAduanal.Click
        With Me
            Try
                Dim ban As Integer = 0



                'Se valida si existe un monto a devolver
                If Val(._ImporteDevolucion.Text) > 0 Then
                    If (Not ((Not fuValeIng.PostedFile Is Nothing) And (fuValeIng.PostedFile.ContentLength > 0))) Or .txtValeIng.Text.Trim = "" Then
                        .litError.Text = "Favor de ingresar la información del Vale de Ingreso"
                        ban = 1
                    End If
                End If

                If .rbTipoOperacion.SelectedIndex - 1 Then
                    If ban = 1 Then
                        .litError.Text = .litError.Text + "; "
                    Else
                        ban = 1
                    End If
                    .litError.Text = "Elija el tipo de operación"
                End If

                If .rbTipoOperacion.SelectedIndex = 0 And ddlCC.SelectedIndex = 0 Then
                    If ban = 1 Then
                        .litError.Text = .litError.Text + "; "
                    Else
                        ban = 1
                    End If
                    .litError.Text = .litError.Text + "Elija un centro de costos válido"
                End If

                If .rbTipoOperacion.SelectedIndex = 1 And ddlDiv.SelectedIndex = 0 Then
                    If ban = 1 Then
                        .litError.Text = .litError.Text + "; "
                    Else
                        ban = 1
                    End If
                    .litError.Text = .litError.Text + "Elija una división válida"
                End If

                If ban = 0 Then

                    'Se guarda el vale de Ingreso
                    ' Ruta Local
                    Dim sFileDir As String = "C:/ProcAd - Adjuntos CompAnt/" 'Ruta en que se almacenará el archivo
                    ' Ruta en Atenea
                    ' Dim sFileDir As String = "D:\ProcAd - Adjuntos CompAnt\" 'Ruta en que se almacenará el archivo
                    Dim lMaxFileSize As Long = 10485760 'Equivalente a 10 Megabytes
                    Dim sFileValeIng As String = ""
                    If .upValeIng.Visible = True Then
                        sFileValeIng = System.IO.Path.GetFileName(fuValeIng.PostedFile.FileName)
                    End If

                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD

                    Dim fecha As DateTime
                    fecha = Date.Now
                    'Insertar Solicitud
                    Dim sdaSolicitud As New SqlDataAdapter
                    Dim dsSolicitud As New DataSet
                    Dim query As String

                    query = "EXEC SP_I_ms_comprobacion_anticipo @id_ms_anticipo_proveedor, @id_usr_solicita, @fecha_solicita, @tipo_escenario, @tipo_gasto, @id_division, @division, @id_centro_costos ,@centro_costos , @id_usr_autoriza , @autorizador, @id_usr_autoriza2, @autorizador2, @id_usr_autorizador3, @autorizador3, @importe_anticipo, @importe_comprobado, @importe_devolucion, @adj_importe_devolucion "
                    sdaSolicitud.SelectCommand = New SqlCommand(query, ConexionBD)
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_ms_anticipo_proveedor", .lblFolio.Text)
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@tipo_escenario", Val(._txtEscenario.Text))

                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_usr_solicita", Val(._txtIdUsuario.Text))
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@fecha_solicita", fecha)

                    If .rbTipoOperacion.SelectedIndex = 0 Then
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@tipo_gasto", "Administrativo")
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_centro_costos", .ddlCC.SelectedItem.Value)
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@centro_costos", .ddlCC.SelectedItem.Text)
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_division", DBNull.Value)
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@division", DBNull.Value)


                    ElseIf .rbTipoOperacion.SelectedIndex = 1 Then
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@tipo_gasto", "Operativo")
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_division", .ddlDiv.SelectedItem.Value)
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@division", .ddlDiv.SelectedItem.Text)
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_centro_costos", DBNull.Value)
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@centro_costos", DBNull.Value)

                    End If
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_usr_autoriza", .ddlAutorizador.SelectedItem.Value)
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@autorizador", .ddlAutorizador.SelectedItem.Text)
                    If ddlAutorizador2.Visible = True Then
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_usr_autoriza2", ddlAutorizador2.SelectedItem.Value)
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@autorizador2", ddlAutorizador2.SelectedItem.Text)
                    Else
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_usr_autoriza2", DBNull.Value)
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@autorizador2", DBNull.Value)
                    End If

                    If ddlAutorizador3.Visible = True Then
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_usr_autorizador3", ddlAutorizador3.SelectedItem.Value)
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@autorizador3", ddlAutorizador3.SelectedItem.Text)
                    Else
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@id_usr_autorizador3", DBNull.Value)
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@autorizador3", DBNull.Value)
                    End If

                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@importe_anticipo", Val(._txtImporte.Text))
                    sdaSolicitud.SelectCommand.Parameters.AddWithValue("@importe_comprobado", Val(._ImporteComprobado))

                    If upValeIng.Visible = True Then
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@importe_devolucion", Val(._ImporteDevolucion))
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@adj_importe_devolucion", sFileValeIng)
                    Else
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@importe_devolucion", DBNull.Value)
                        sdaSolicitud.SelectCommand.Parameters.AddWithValue("@adj_importe_devolucion", DBNull.Value)

                    End If
                    ConexionBD.Open()
                    sdaSolicitud.Fill(dsSolicitud)
                    ConexionBD.Close()

                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "select max(id_ms_comprobacion_anticipo) from ms_comprobacion_anticipo where id_usr_solicita = @id_usr_solicita and estatus not in ('Z') "
                    SCMValores.Parameters.AddWithValue("@id_usr_solicita", Val(._txtIdUsuario.Text))
                    ConexionBD.Open()
                    .lblFolio.Text = SCMValores.ExecuteScalar
                    ConexionBD.Close()
                    If Val(.lblFolio.Text) > 0 Then
                        ._txtBan.Text = 1
                    End If

                    If ._txtBan.Text = 1 Then

                        'Actualizar el id de la comprobación
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update dt_comprobacion_anticipo set id_ms_comp_anticipo = @id_ms_comp_anticipo where id_ms_comp_anticipo = -1 and id_usr_carga = @id_usr_carga"
                        SCMValores.Parameters.AddWithValue("@id_ms_comp_anticipo", Val(.lblFolio.Text))
                        SCMValores.Parameters.AddWithValue("@id_usr_carga", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Insertar el id_ms_anticipo_proveedor a la tabla dt_archivo_adj_anticipo
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "update dt_archivo_adj_comp_anticipo set id_ms_comp_anticipo_proveedor = @id_ms_comp_anticipo_proveedor where id_ms_comp_anticipo_proveedor = -1 and id_usuario = @id_usuario"
                        SCMValores.Parameters.AddWithValue("@id_ms_comp_anticipo_proveedor", Val(.lblFolio.Text))
                        SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        'Insertar Instancia de Solicitud de Anticipo
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "insert into ms_instancia ( id_ms_sol,  tipo,  id_actividad) " +
                                                 "				    values (@id_ms_sol, @tipo, @id_actividad) "
                        SCMValores.Parameters.AddWithValue("@id_ms_sol", Val(.lblFolio.Text))
                        SCMValores.Parameters.AddWithValue("@tipo", "CAP")
                        SCMValores.Parameters.AddWithValue("@id_actividad", 142)
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
                        SCMValores.Parameters.AddWithValue("@id_actividad", 142)
                        SCMValores.Parameters.AddWithValue("@id_usr", Val(._txtIdUsuario.Text))
                        SCMValores.Parameters.AddWithValue("@fecha", fecha)
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()

                        .pnlInicio.Enabled = False
                        .pnlInformacionGeneral.Enabled = False

                    End If

                End If
            Catch ex As Exception

            End Try
        End With
    End Sub

#End Region



#Region "Funciones generales"
    Public Sub llenarCC()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCC As New SqlDataAdapter
                Dim dsCC As New DataSet
                sdaCC.SelectCommand = New SqlCommand("select 0 as id_cc " +
                                                     "     , ' ' as nombre " +
                                                     "union " +
                                                     "select id_cc " +
                                                     "     , nombre " +
                                                     "from bd_Empleado.dbo.cg_cc " +
                                                     "where id_empresa = @idEmpresa " +
                                                     "  and status = 'A' " +
                                                     "order by nombre ", ConexionBD)
                sdaCC.SelectCommand.Parameters.AddWithValue("@idEmpresa", Val(_txtIdEmpresa.Text))
                .ddlCC.DataSource = dsCC
                .ddlCC.DataTextField = "nombre"
                .ddlCC.DataValueField = "id_cc"





                ConexionBD.Open()
                sdaCC.Fill(dsCC)
                .ddlCC.DataBind()

                ConexionBD.Close()
                sdaCC.Dispose()
                dsCC.Dispose()
                .ddlCC.SelectedIndex = -1

                .updCC.Update()

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarDiv()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaDiv As New SqlDataAdapter
                Dim dsDiv As New DataSet
                sdaDiv.SelectCommand = New SqlCommand("select 0 as id_div " +
                                                      "     , ' ' as nombre " +
                                                      "union " +
                                                      "select id_div " +
                                                      "     , nombre " +
                                                      "from bd_Empleado.dbo.cg_div " +
                                                      "where id_empresa = @idEmpresa " +
                                                      "  and status = 'A' " +
                                                      "order by nombre ", ConexionBD)
                sdaDiv.SelectCommand.Parameters.AddWithValue("@idEmpresa", Val(_txtIdEmpresa.Text))
                .ddlDiv.DataSource = dsDiv
                .ddlDiv.DataTextField = "nombre"
                .ddlDiv.DataValueField = "id_div"

                ConexionBD.Open()
                sdaDiv.Fill(dsDiv)
                .ddlDiv.DataBind()
                ConexionBD.Close()
                sdaDiv.Dispose()
                dsDiv.Dispose()
                .ddlDiv.SelectedIndex = -1
                .upDiv.Update()

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub



    Public Sub llenarAutorizador()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaAut As New SqlDataAdapter
                Dim dsAut As New DataSet
                sdaAut.SelectCommand = New SqlCommand(" SELECT cgEmpl.id_empleado, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno AS autorizador " +
                                " FROM dt_autorizador " +
                                " LEFT JOIN cg_usuario usr ON dt_autorizador.id_autorizador = usr.id_usuario " +
                                " LEFT JOIN bd_Empleado.dbo.cg_empleado cgEmpl ON usr.id_empleado = cgEmpl.id_empleado " +
                                " WHERE dt_autorizador .id_usuario = @id_usuario " +
                                " AND usr.status = 'A' " +
                                " ORDER BY aut_dir, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno ", ConexionBD)
                sdaAut.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                .ddlAutorizador.DataSource = dsAut
                .ddlAutorizador.DataTextField = "autorizador"
                .ddlAutorizador.DataValueField = "id_empleado"

                ConexionBD.Open()
                sdaAut.Fill(dsAut)
                .ddlAutorizador.DataBind()
                ConexionBD.Close()
                sdaAut.Dispose()
                dsAut.Dispose()
                .ddlAutorizador.SelectedIndex = -1
                .upAutorizador.Update()

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub


    Public Sub llenarAutorizador2()

        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaAut2 As New SqlDataAdapter
                Dim dsAut2 As New DataSet
                sdaAut2.SelectCommand = New SqlCommand("select cgEmpl.id_empleado, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " +
                                                              "from dt_autorizador " +
                                                              "  left join cg_usuario on dt_autorizador.id_autorizador = cg_usuario.id_usuario " +
                                                              "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                              "where dt_autorizador.id_usuario = @idUsuario " +
                                                              "  and cg_usuario.status = 'A' " +
                                                              "  and dt_autorizador.aut_dir = 'S' " +
                                                              "order by cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno ", ConexionBD)
                sdaAut2.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                .ddlAutorizador2.DataSource = dsAut2
                .ddlAutorizador2.DataTextField = "autorizador"
                .ddlAutorizador2.DataValueField = "id_empleado"

                ConexionBD.Open()
                sdaAut2.Fill(dsAut2)
                .ddlAutorizador2.DataBind()
                ConexionBD.Close()
                sdaAut2.Dispose()
                dsAut2.Dispose()
                .ddlAutorizador2.SelectedIndex = -1
                .up_Autorizador2.Update()
            Catch ex As Exception
                .litError.Text = ex.ToString()
            End Try
        End With

    End Sub

    Public Sub llenarAutorizador3()
        With Me
            Try

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaAut3 As New SqlDataAdapter
                Dim dsAut3 As New DataSet

                If .ddlCC.Visible = True Then
                    'Autorizadores del CC
                    sdaAut3.SelectCommand = New SqlCommand("select cgEmpl.id_empleado, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " +
                                                          "from bd_empleado.dbo.cg_empleado cgEmpl " +
                                                          "  left join bd_empleado.dbo.cg_cc cgCC on cgCC.id_empl_dir = cgEmpl.id_empleado " +
                                                          "  left join cg_usuario on cgEmpl.id_empleado = cg_usuario.id_empleado " +
                                                          "where cgEmpl.status = 'A' " +
                                                          "  and cg_usuario.status = 'A' " +
                                                          "  and cgCC.id_cc = @idCC " +
                                                          "  and autorizador = 'S' " +
                                                          "  and cg_usuario.id_usuario not in (@idUsuario) ", ConexionBD)
                    sdaAut3.SelectCommand.Parameters.AddWithValue("@idCC", .ddlCC.SelectedItem.Value)
                Else
                    'Autorizadores de la Div
                    sdaAut3.SelectCommand = New SqlCommand("select cgEmpl.id_empleado, cgEmpl.nombre + ' ' + ap_paterno + ' ' + ap_materno as nombre_empleado " +
                                                          "from bd_empleado.dbo.cg_empleado cgEmpl " +
                                                          "  left join bd_empleado.dbo.cg_div cgDiv on cgDiv.id_empl_dir = cgEmpl.id_empleado " +
                                                          "  left join cg_usuario on cgEmpl.id_empleado = cg_usuario.id_empleado " +
                                                          "where cgEmpl.status = 'A' " +
                                                          "  and cg_usuario.status = 'A' " +
                                                          "  and cgDiv.id_div = @idDiv " +
                                                          "  and autorizador = 'S' " +
                                                          "  and cg_usuario.id_usuario not in (@idUsuario) " +
                                                          "order by nombre_empleado ", ConexionBD)
                    sdaAut3.SelectCommand.Parameters.AddWithValue("@idDiv", .ddlDiv.SelectedItem.Value)
                End If

                .ddlAutorizador3.DataTextField = "nombre_empleado"
                .ddlAutorizador3.DataValueField = "id_empleado"
                ConexionBD.Open()
                sdaAut3.Fill(dsAut3)
                .ddlAutorizador3.DataBind()
                ConexionBD.Close()
                sdaAut3.Dispose()
                dsAut3.Dispose()
                .ddlAutorizador3.SelectedIndex = -1
                upAutorizador3.Update()

            Catch ex As Exception

            End Try

        End With
    End Sub

    Private Sub DeleteFile(ByVal strFileName As String)
        If strFileName.Trim().Length > 0 Then
            Dim fi As New FileInfo(strFileName)
            If (fi.Exists) Then    'Si existe el archivo, eliminarlo
                fi.Delete()
            End If
        End If
    End Sub

    Protected Sub ddlCC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCC.SelectedIndexChanged
        With Me
            Try
                If .ddlCC.SelectedIndex > 0 Then
                    If Val(._txtImporte.Text) > Val(._txtTope1.Text) Then
                        llenarAutorizador2()
                        .lbl_Autorizador2.Visible = True
                        .ddlAutorizador2.Visible = True
                    End If

                    If Val(._txtImporte.Text) > Val(._txtTope2.Text) Then
                        llenarAutorizador2()
                        .lbl_Autorizador2.Visible = True
                        .ddlAutorizador2.Visible = True
                        llenarAutorizador3()
                        .lbl_Autorizador3.Visible = True
                        .ddlAutorizador3.Visible = True
                    End If
                End If
            Catch ex As Exception

            End Try
        End With
    End Sub



    Protected Sub ddlDiv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDiv.SelectedIndexChanged
        With Me
            Try
                If .ddlDiv.SelectedIndex > 0 Then
                    If Val(._txtImporte.Text) > Val(._txtTope1.Text) Then
                        llenarAutorizador2()
                        .lbl_Autorizador2.Visible = True
                        .ddlAutorizador2.Visible = True
                    End If

                    If Val(._txtImporte.Text) > Val(._txtTope2.Text) Then
                        llenarAutorizador2()
                        .lbl_Autorizador2.Visible = True
                        .ddlAutorizador2.Visible = True
                        llenarAutorizador3()
                        .lbl_Autorizador3.Visible = True
                        .ddlAutorizador3.Visible = True
                    End If
                End If
            Catch ex As Exception

            End Try
        End With

    End Sub

    Protected Sub ddlProveedor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProveedor.SelectedIndexChanged
        With Me
            Try
                llenargvFacturasProveedor("INGRESO")
            Catch ex As Exception

            End Try
        End With

    End Sub


#End Region
End Class