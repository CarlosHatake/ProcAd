<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="RegCumplUN.aspx.vb" Inherits="ProcAd.RegCumplUN" %>
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


.igte_Edit
{
	background-color:White;
	font-size:13px;
	font-family: Segoe UI, Verdana, Arial, Sans-Serif;
	border:solid 1px #CCCCCC;
	outline: 0;
	color:#333333;
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


        .auto-style30 {
            width: 123px;
            height: 24px;
        }
        .auto-style31 {
            height: 24px;
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
                                    &nbsp;</td>
                                <td style="width: 60px">
                                    <asp:ImageButton ID="ibtnBaja" runat="server" ImageUrl="images\Trash.png" ToolTip="Baja" Width="20px" />
                                </td>
                                <td style="width: 60px">
                                    <asp:ImageButton ID="ibtnModif" runat="server" ImageUrl="images\Edit.png" ToolTip="Modificar" Width="20px" />
                                </td>
                                <td>&nbsp;</td>
                                <td style="width: 150px; text-align: right;">
                                    <asp:Label ID="lbl_MesEvalB" runat="server" Text="Mes Evaluación:"></asp:Label>
                                </td>
                                <td style="width: 170px; text-align: left;">
                                    <asp:DropDownList ID="ddlMesEvalB" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="125px" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 170px">
                                    <asp:Label ID="lblMesEvalD" runat="server" ForeColor="#009933" Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table style="width:100%;">
                            <tr>
                                <td style="text-align: center; width: 290px;">
                                    <ig:WebScriptManager ID="wsmRegCumplUN" runat="server">
                                    </ig:WebScriptManager>
                                </td>
                                <td class="auto-style29">
                                    <asp:GridView ID="gvCumplUN" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="730px">
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>
                                            <asp:BoundField DataField="id_ms_cumpl_UN" HeaderText="id_ms_cumpl_UN" />
                                            <asp:BoundField DataField="id_unidad_neg" HeaderText="id_unidad_neg" />
                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" SelectText="" ShowSelectButton="True">
                                            <ItemStyle Width="20px" />
                                            </asp:CommandField>
                                            <asp:BoundField DataField="unidad_neg" HeaderText="Unidad de Negocio">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="porcent_cumpl" HeaderText="% Cumplimiento" DataFormatString="{0:p}">
                                            <ItemStyle HorizontalAlign="Center" Width="160px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="porcent_bono" HeaderText="% Pago Bono" DataFormatString="{0:p}">
                                            <ItemStyle HorizontalAlign="Center" Width="160px" />
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
                                        <table style="width: 100%; height: 50px;">
                                            <tr>
                                                <td style="text-align: right; " class="auto-style30">
                                                    <asp:Label ID="lbl_TipoMov" runat="server" Text="Tipo de Movimiento:"></asp:Label>
                                                </td>
                                                <td style="text-align: left; " class="auto-style31">
                                                    <asp:Label ID="lblTipoMov" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lbl_UnidadNeg" runat="server" Text="Unidad de Negocio:"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblUnidadNeg" runat="server" Font-Bold="True"></asp:Label>
                                                    <asp:Label ID="lblUnidadNegID" runat="server" ForeColor="#009933" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="width:100%;">
                                            <tr>
                                                <td style="text-align: right; width: 123px;">
                                                    <asp:Label ID="lbl_MesEval" runat="server" Text="Mes Evaluación:"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 190px;">
                                                    <asp:Label ID="lblMesEval" runat="server" ForeColor="Blue"></asp:Label>
                                                </td>
                                                <td style="text-align: right; width: 123px;">
                                                    <asp:Label ID="lbl_PorcentCumpl" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="% de Cumplimiento:"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 130px;">
                                                    <ig:WebPercentEditor ID="wpePorcentCumpl" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxValue="10" MinValue="-99.99" Nullable="False" Width="70px">
                                                    </ig:WebPercentEditor>
                                                </td>
                                                <td style="text-align: right; width: 123px;">
                                                    &nbsp;</td>
                                                <td style="text-align: left;">
                                                    &nbsp;</td>
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
