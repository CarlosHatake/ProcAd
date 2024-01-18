<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="VboAnticipo.aspx.vb" Inherits="ProcAd.VboAnticipo" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
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
                <asp:TextBox ID="_txtidMsAnticipo" runat="server" Width="15px" Visible="False"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Panel runat="server" ID="pnlInicio">
                    <table style="width: 1366px; height: 25px; font-family: Verdana; font-size: 8pt;">
                        <tr>
                            <td>&nbsp;</td>
                            <td style="width: 150px; text-align: right">
                                <asp:Label ID="lbl_Folio" runat="server" Text="Folio:"></asp:Label>
                            </td>
                            <td style="width: 130px">
                                <asp:Label ID="lblFolio" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1366px; height: 44px;">
                        <tr>
                            <td style="text-align: right; width: 130px">
                                <asp:Label ID="lbl_Empresa" runat="server" Text="Empresa:"></asp:Label>
                            </td>
                            <td style="width: 550px">
                                <asp:Label ID="lblEmpresa" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>

                            <td style="text-align: right; width: 170px">
                                <asp:Label ID="lbl_Proveedor" runat="server" Text="Proveedor (NAV):"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblProveedor" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_NumProveedor" runat="server" Text="Numero del Proveedor (NAV):"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblNumProveedor" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Importe" runat="server" Text="Importe requerido:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblImporte" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align: top">
                                <asp:Label ID="lbl_Pedido" runat="server" Text="Pedido de compra: "></asp:Label>
                            </td>
                            <td>
                                <asp:GridView ID="gvPedidosCompras" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="250px" DataKeyNames="id_dt_pedidos_compra">
                                    <Columns>
                                        <asp:BoundField DataField="id_anticipo" HeaderText="id_pedido_compra" Visible="false">
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="pedido_compra" HeaderText="Pedido de compra">
                                            <ItemStyle HorizontalAlign="left" Width="250px" />
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
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Justificacion" runat="server" Text="Justificación del anticipo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtJustificacion" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="63px" TextMode="MultiLine" Width="535px" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Adjunto" runat="server" Text="Adjuntos:"></asp:Label>
                            </td>
                            <td style="width: 300px; vertical-align: top">
                                <asp:UpdatePanel ID="upAdjuntos" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvAdjuntos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ShowHeader="False" Width="300px">
                                            <AlternatingRowStyle BackColor="#CCCCCC" />
                                            <Columns>
                                                <asp:HyperLinkField DataNavigateUrlFields="path" DataTextField="archivo" HeaderText="Adjuntos">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="20px" />
                                                </asp:HyperLinkField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>

                        </tr>

                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Observaciones" runat="server" Text="Observaciones: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtObservaciones" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="63px" TextMode="MultiLine" Width="535px"></asp:TextBox>
                            </td>
                            <td>
                                 <asp:Label ID="lblFechaPagoAutorizar" runat="server" Text="Fecha de pago para autorizar: "></asp:Label>
                            </td>
                            <td>
                                <ig:WebDatePicker ID="wdteFechaPagoAutorizar" runat="server" Font-Names="Verdana" Font-Size="8pt" Nullable="False">
                                    <AutoPostBackFlags ValueChanged="On" />
                                </ig:WebDatePicker>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1200px; height: 44px;">
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnAutorizar" runat="server" Text="Autorizar" Width="200px" OnClientClick="this.disabled = true; this.value='Procesando…';" UseSubmitBehavior="false" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button ID="btnRechazar" runat="server" Text="Rechazar" Width="200px" OnClientClick="this.disabled = true; this.value='Procesando…';" UseSubmitBehavior="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>

        </tr>
    </table>
</asp:Content>
