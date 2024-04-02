<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="141.aspx.vb" Inherits="ProcAd._141" %>
<%@ Register Assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
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
            <td>
                <asp:Panel ID="pnlInicio" runat="server">
                    <table style="width: 1266px; height: 25px; font-family: Verdana; font-size: 8pt;">
                        <%--<tr>
                            <td>&nbsp;</td>
                            <td style="width: 150px; text-align: right">
                                <asp:Label ID="lbl_Folio" runat="server" Text="Folio:"></asp:Label>
                            </td>
                            <td style="width: 130px">
                                <asp:Label ID="lblFolio" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                            </td>
                        </tr>--%>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlPagoAnticipadoConPedidoCompra" runat="server">
                        <table style="width: 1266px; height: 44px">
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 160px"></td>
                                <td>
                                    <asp:Label ID="lbl_PagoAnticipado" runat="server" Text="Lista de validación de pagos anticipados: " Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:GridView ID="gvListaValidacion" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="900px" DataKeyNames="id_ms_anticipo_proveedor">
                                        <Columns>
                                            <%--<asp:BoundField DataField="id" HeaderText="idCFDI" />--%>
                                            <asp:BoundField DataField="id_ms_anticipo_proveedor" HeaderText="Solicitud">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_solicita" HeaderText="Fecha de anticipado">
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="proveedor" HeaderText="Proveedor">
                                                <ItemStyle HorizontalAlign="Center" Width="100px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="importe_requerido" HeaderText="Importe">
                                                <ItemStyle HorizontalAlign="Center" Width="90px"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="pedido_compra" HeaderText="Pedidos de compra">
                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="empleado_solicita" HeaderText="Usuario">
                                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbxSeleccionar" runat="server" AutoPostBack="true"/>
                                                </ItemTemplate>
                                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
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
                        <table style="width: 1266px; height: 44px">
                            <tr>
                                <td style="text-align: center">
                                    <asp:Button ID="btnComprobar" runat="server" Text="Guardar" Font-Names="Verdana" Font-Size="8pt" Width="160px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>