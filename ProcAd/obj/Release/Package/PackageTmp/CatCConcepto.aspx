<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="CatCConcepto.aspx.vb" Inherits="ProcAd.CatCConcepto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        .auto-style6 {
            width: 1350px;
        }
        .auto-style9 {
            width: 1350px;
            height: 31px;
        }
        .auto-style8 {
            width: 807px;
        }
        .auto-style14 {
            width: 123px;
        }
        .auto-style16 {
            height: 23px;
        }
        .auto-style10 {
            height: 17px;
        }
                
        .auto-style29 {
            width: 963px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1360px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; font-family: Verdana; font-size: 8pt; color: #FF0000" class="auto-style6">
                <asp:TextBox ID="_txtTipoMov" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="auto-style6">
                <asp:Panel ID="pnlInicio" runat="server">
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
                                <asp:TextBox ID="txtValor" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="140px"></asp:TextBox>
                            </td>
                            <td style="width: 100px">
                                <asp:DropDownList ID="ddlCampo" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="110px">
                                    <asp:ListItem Value="categoria">Categoría</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 30px">
                                <asp:ImageButton ID="ibtnBuscar" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="20px" />
                            </td>
                            <td style="width: 170px">&nbsp;</td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: center; width: 450px;">&nbsp;</td>
                            <td class="auto-style29">
                                <asp:GridView ID="gvCategoria" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="500px">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <Columns>
                                        <asp:BoundField DataField="id_concepto_cat" HeaderText="id_concepto_cat">
                                        <ItemStyle Width="30px" />
                                        </asp:BoundField>
                                        <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" SelectText="" ShowSelectButton="True">
                                        <ItemStyle Width="20px" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="categoria" HeaderText="Categoría">
                                        <ItemStyle HorizontalAlign="Left" />
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
            </td>
        </tr>
        <tr>
            <td class="auto-style9" style="text-align: center; margin-right: auto; margin-left: auto">
                <asp:Panel ID="pnlDatos" runat="server">
                    <table style="border: medium inset #808080; width: 807px; margin-right: auto; margin-left: auto;">
                        <tr>
                            <td class="auto-style8">
                                <table style="width: 100%; height: 50px;">
                                    <tr>
                                        <td class="auto-style14" style="text-align: right">
                                            <asp:Label ID="lbl_TipoMov" runat="server" Text="Tipo de Movimiento:"></asp:Label>
                                        </td>
                                        <td class="auto-style16" style="text-align: left; height: 25px; width: 450px;">
                                            <asp:Label ID="lblTipoMov" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td class="auto-style16" style="text-align: left; height: 25px;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style14" style="text-align: right">
                                            <asp:Label ID="lbl_Categoria" runat="server" Text="Categoría:"></asp:Label>
                                        </td>
                                        <td class="auto-style16" style="text-align: left">
                                            <asp:TextBox ID="txtNombre" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="400px"></asp:TextBox>
                                        </td>
                                        <td class="auto-style16" style="text-align: left">
                                            <asp:CheckBox ID="cbGastosViaje" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Gastos de Viaje" />
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%; height: 50px;">
                                    <tr>
                                        <td class="auto-style10">
                                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" Width="150px" Font-Names="Verdana" Font-Size="8pt" />
                                        </td>
                                        <td class="auto-style10">
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
    </table>
</asp:Content>
