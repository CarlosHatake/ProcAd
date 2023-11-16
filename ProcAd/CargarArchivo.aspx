<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="CargarArchivo.aspx.vb" Inherits="ProcAd.CargarArchivo" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000;">
                <table style="width: 1360px;">
                    <tr>
                        <td>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width:1360px;">
                    <tr>
                        <td style="width: 300px; text-align: right;">
                                <asp:Label ID="lbl_Tipo" runat="server" Text="Tipo:"></asp:Label>
                            </td>
                        <td>
                            <table style="width:100%;">
                                <tr>
                                    <td style="width: 200px">
                            <asp:DropDownList ID="ddlTipo" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px">
                                <asp:ListItem Value="G">General</asp:ListItem>
                                <asp:ListItem Value="D">Detalle</asp:ListItem>
                            </asp:DropDownList>
                                    </td>
                                    <td style="width: 100px; text-align: right;">
                                <asp:Label ID="lbl_Version" runat="server" Text="Versión:"></asp:Label>
                                    </td>
                                    <td>
                            <asp:DropDownList ID="ddlVersion" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="60px">
                                <asp:ListItem Value="3.3">3.3</asp:ListItem>
                                <asp:ListItem Value="4.0">4.0</asp:ListItem>
                            </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 300px; text-align: right;">
                                <asp:Label ID="lbl_Archivo" runat="server" Text="Archivo:"></asp:Label>
                            </td>
                        <td>
                            <asp:FileUpload ID="fuArchivo" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="800px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 300px; text-align: right;">&nbsp;</td>
                        <td>
                            <asp:Label ID="lblMessage" runat="server" Font-Names="Verdana" Font-Size="8pt" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="width: 1360px; height: 50px;">
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnAceptar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Cargar Archivo" Width="200px" OnClientClick="this.disabled = true; this.value='Procesando…';" UseSubmitBehavior="false" OnClick ="btnAceptar_Click" />
                        </td>
                        <td style="text-align: center">
                            <asp:Button ID="btnNuevo" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Nuevo Archivo" Width="200px" OnClientClick="this.disabled = true; this.value='Procesando…';" UseSubmitBehavior="false" OnClick ="btnNuevo_Click" />
                        </td>
                    </tr>
                </table>
                <table style="width: 1360px; height: 50px;">
                    <tr>
                        <td style="text-align: center; width: 50%;">
                            <asp:TextBox ID="txtResultado" runat="server" Height="109px" TextMode="MultiLine" Width="280px" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="text-align: left">
                            <table style="width:100%;">
                                <tr>
                                    <td style="width: 190px; text-align: right; vertical-align: top;">
                                <asp:Label ID="lbl_UuidOmitido" runat="server" Text="UUIDs omitidos:"></asp:Label>
                                    </td>
                                    <td>
                            <asp:TextBox ID="txtUuidOmitido" runat="server" Height="109px" TextMode="MultiLine" Width="300px" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gvFacturas" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal">
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
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
