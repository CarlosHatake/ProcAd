<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ModContratos.aspx.vb" Inherits="ProcAd.ModContratos" %>

<%@ Register Assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style6 {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; height: 25px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000;">
                <ig:WebScriptManager ID="wsm46" runat="server">
                </ig:WebScriptManager>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsInst" runat="server" Width="15px" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style6"></td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="pnlInfoContrato">
                    <table>
                        <tr>
                            <td style="width: 200px"></td>
                            <td>
                                <asp:Label runat="server" Text="Contrato:"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label runat="server" ID="lblContrato" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" Text="Empresa:"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label runat="server" ID="lblEmpresa" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" Text="Arrendadora:"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label runat="server" ID="lblArrendadora" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" Text="Tipo de Arrendamiento:"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label runat="server" ID="lblTipoArrendamiento" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 200px"></td>
                            <td>
                                <asp:Label runat="server" Text="RFC Arrendadora:"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label runat="server" ID="lblRFCArrendadora" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" Text="Plazo:"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label runat="server" ID="lblPlazo" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" Text="Fecha inicio:"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label runat="server" ID="lblFecInicio" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" Text="Fecha Fin:"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label runat="server" ID="lblFechaFin" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="pnlAnexos">
                    <table>
                        <tr>
                            <td style="height: 30px"></td>
                        </tr>
                        <tr>
                            <td style="width: 200px"></td>
                            <td style="vertical-align: top; width: 100px; text-align: center">
                                <asp:Label runat="server" Text="ANEXOS" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:GridView ID="gvAnexos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="800px" DataKeyNames="id_ms_anexo">
                                    <Columns>

                                        <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                            <ItemStyle Width="15px" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="id_ms_contrato" HeaderText="No. Contrato">
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="anexo" HeaderText="Anexo">
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                            <ItemStyle HorizontalAlign="Center" Width="140px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="arrendadora" HeaderText="Arrendadora">
                                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_arrendamiento" HeaderText="Tipo de Arrendamiento">
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_inicio" HeaderText="Fecha Inicio" DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_fin" HeaderText="Fecha Fin" DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#242121" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="pnlDetalleAnexos">
                    <table>
                        <tr>
                            <td style="height: 80px"></td>
                            <td></td>
                            <td style="text-align: right">
                                <asp:Button runat="server" Text="Importar" Font-Size="8pt" Font-Names="Verdana" />
                                &nbsp; &nbsp;
                                <asp:Button runat="server" Text="Exportar" Font-Size="8pt" Font-Names="Verdana" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 200px"></td>
                            <td style="vertical-align: top; width: 100px; text-align: center">
                                <asp:Label runat="server" Text="DETALLE ANEXOS" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:GridView ID="gvDetalleAnexo" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="800px" DataKeyNames="id_ms_anexo">
                                    <Columns>

                                        <asp:BoundField DataField="unidad" HeaderText="No. de Unidad">
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="mes_01" HeaderText="Mes">
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="mes_02" HeaderText="Mes">
                                            <ItemStyle HorizontalAlign="Center" Width="140px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="mes_03" HeaderText="Mes">
                                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="mes_04" HeaderText="Mes">
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="mes_05" HeaderText="Mes">
                                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="mes_06" HeaderText="Mes">
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        </asp:BoundField>
                                        <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True" HeaderText="Factura">
                                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                                        </asp:CommandField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#242121" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td style="height: 50px">
                                <asp:Label runat="server" Text="Total:" Font-Bold="true"></asp:Label>
                                &nbsp;
                                <asp:Label runat="server" ID="lblTotal" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>

                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="pnlFacturas">
                    <table>
                        <tr>
                            <td style="width: 200px"></td>
                            <td>
                                <table>
                                    <tr>
                                        <td style="width: 120px; text-align: right">
                                            <asp:Label runat="server" Text="Centro de Costos:"></asp:Label>
                                        </td>
                                        <td style="width: 180px">
                                            <asp:DropDownList runat="server" ID="ddlCC" Width="150px"></asp:DropDownList>
                                        </td>
                                        <td style="width: 100px; text-align: right">
                                            <asp:Label runat="server" Text="División:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlDivision" Width="150px"></asp:DropDownList>
                                        </td>
                                        <td style="width:140px; text-align:right">
                                            <asp:Label runat="server" Text="Proveedor:"></asp:Label>
                                        </td>
                                        <td style="width: 200px">
                                            <asp:Label runat="server" ID="lblProveedor" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <table>
                        <tr>
                            <td style="width: 200px; height: 30px"></td>
                            <td style="width: 120px; text-align: right">
                                        </td>
                            <td>
                                <asp:GridView ID="gvFacturasProveedor" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px" DataKeyNames="id_dt_factura">
                                    <Columns>
                                        <%--<asp:BoundField DataField="id" HeaderText="idCFDI" />--%>
                                        <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                            <ItemStyle Width="15px" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="fecha_emision" HeaderText="Fecha Emisión">
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="uuid" HeaderText="UUID">
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="serie" HeaderText="Serie">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="folio" HeaderText="Folio">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="lugar_exp" HeaderText="Lugar Exp.">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="forma_pago" HeaderText="Forma Pago">
                                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="moneda" HeaderText="Moneda">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="subtotal" HeaderText="Subtotal">
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="importe" HeaderText="Importe">
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#242121" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
