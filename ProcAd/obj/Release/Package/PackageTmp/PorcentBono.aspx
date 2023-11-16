<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="PorcentBono.aspx.vb" Inherits="ProcAd.PorcentBono" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        .auto-style29 {
            width: 963px;
        }
            

.igte_Edit
{
	background-color:White;
	font-size:13px;
	font-family: Segoe UI, Verdana, Arial, Sans-Serif;
	border:solid 1px #CCCCCC;
	outline: 0;
	color:#333333;
}


        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000">
                <asp:TextBox ID="_txtTipoMovA" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtTipoMov" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                    <asp:Panel ID="pnlGrid" runat="server">
                        <table style="width: 100%; text-align: center; margin-right: auto; margin-left: auto; height: 60px;">
                            <tr>
                                <td style="width: 180px">&nbsp;</td>
                                <td style="width: 60px">
                                    <asp:ImageButton ID="ibtnAlta" runat="server" ImageUrl="images\Add.png" ToolTip="Alta" Width="20px" />
                                </td>
                                <td style="width: 60px">
                                    <asp:ImageButton ID="ibtnBaja" runat="server" ImageUrl="images\Trash.png" ToolTip="Baja" Width="20px" />
                                </td>
                                <td style="width: 60px">
                                    <asp:ImageButton ID="ibtnModif" runat="server" ImageUrl="images\Edit.png" ToolTip="Modificar" Width="20px" />
                                </td>
                                <td>&nbsp;</td>
                                <td style="width: 150px">
                                    &nbsp;</td>
                                <td style="width: 100px">
                                    &nbsp;</td>
                                <td style="width: 30px">
                                    &nbsp;</td>
                                <td style="width: 170px">&nbsp;</td>
                            </tr>
                        </table>
                        <table style="width:100%;">
                            <tr>
                                <td style="text-align: center; width: 480px;">
                                    <ig:WebScriptManager ID="wsmPorcentBono" runat="server">
                                    </ig:WebScriptManager>
                                </td>
                                <td class="auto-style29">
                                    <asp:GridView ID="gvPorcentBono" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="450px">
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>
                                            <asp:BoundField DataField="id_dt_porcent_bono" HeaderText="id_dt_porcent_bono">
                                            <ItemStyle Width="30px" />
                                            </asp:BoundField>
                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" SelectText="" ShowSelectButton="True">
                                            <ItemStyle Width="20px" />
                                            </asp:CommandField>
                                            <asp:BoundField DataField="porcent_min" HeaderText="% Mínimo" DataFormatString="{0:p}" >
                                            <ItemStyle HorizontalAlign="Center" Width="140px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="porcent_max" HeaderText="% Máximo" DataFormatString="{0:p}">
                                            <ItemStyle HorizontalAlign="Center" Width="140px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="porcent_bono" HeaderText="% Pago Bono" DataFormatString="{0:p}">
                                            <ItemStyle HorizontalAlign="Center" Width="140px" />
                                            </asp:BoundField>
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
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlDatos" runat="server">
                        <table style="border: medium inset #808080; width: 850px; margin-right: auto; margin-left: auto;">
                            <tr>
                                <td class="auto-style8">
                                    <asp:Panel ID="pnlDetalle" runat="server">
                                        <table style="width: 100%; height: 20px;">
                                            <tr>
                                                <td style="text-align: right; width: 123px;">
                                                    <asp:Label ID="lbl_TipoMov" runat="server" Text="Tipo de Movimiento:"></asp:Label>
                                                </td>
                                                <td style="text-align: left; ">
                                                    <asp:Label ID="lblTipoMov" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="width:100%; height: 30px;">
                                            <tr>
                                                <td style="text-align: right; width: 123px;">
                                                    <asp:Label ID="lbl_Min" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="% Mínimo:"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 140px;">
                                                    <ig:WebPercentEditor ID="wpeMin" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxValue="1" MinValue="-99.99" Nullable="False" Width="70px">
                                                    </ig:WebPercentEditor>
                                                </td>
                                                <td style="text-align: right; width: 123px;">
                                                    <asp:Label ID="lbl_Max" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="% Máximo:"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 140px;">
                                                    <ig:WebPercentEditor ID="wpeMax" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxValue="10" MinValue="0.01" Nullable="False" Width="70px">
                                                    </ig:WebPercentEditor>
                                                </td>
                                                <td style="text-align: right; width: 123px;">
                                                    <asp:Label ID="lbl_PorcentBono" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="% Bono:"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <ig:WebPercentEditor ID="wpeBono" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxValue="1" MinValue="0" Nullable="False" Width="70px">
                                                    </ig:WebPercentEditor>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <table style="width: 100%; height: 50px;">
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" Width="150px" Font-Names="Verdana" Font-Size="8pt" />
                                            </td>
                                            <td style="text-align: center">
                                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Width="150px" Font-Names="Verdana" Font-Size="8pt" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
