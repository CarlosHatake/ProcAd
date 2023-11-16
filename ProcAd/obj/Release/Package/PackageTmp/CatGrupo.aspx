<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="CatGrupo.aspx.vb" Inherits="ProcAd.CatGrupo" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        .auto-style29 {
            width: 963px;
        }
        .auto-style30 {
            width: 170px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000">
                <asp:TextBox ID="_txtIdUsrSecre" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="20px" Visible="False"></asp:TextBox>
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
                                <td>
                                    <ig1:WebScriptManager ID="wsmCatGrupo" runat="server">
                                    </ig1:WebScriptManager>
                                </td>
                                <td style="width: 150px">
                                    <asp:TextBox ID="txtValor" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="140px"></asp:TextBox>
                                </td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="ddlCampo" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="110px">
                                        <asp:ListItem Value="grupo">Grupo</asp:ListItem>
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
                                <td style="text-align: center; width: 320px;">&nbsp;</td>
                                <td class="auto-style29">
                                    <asp:GridView ID="gvGrupo" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="700px">
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>
                                            <asp:BoundField DataField="id_grupo" HeaderText="id_grupo">
                                            <ItemStyle Width="30px" />
                                            </asp:BoundField>
                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" SelectText="" ShowSelectButton="True">
                                            <ItemStyle Width="20px" />
                                            </asp:CommandField>
                                            <asp:BoundField DataField="grupo" HeaderText="Nombre">
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
                                                <td style="text-align: right; width: 123px;">
                                                    <asp:Label ID="lbl_TipoMov" runat="server" Text="Tipo de Movimiento:"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 400px;">
                                                    <asp:Label ID="lblTipoMov" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 200px;">&nbsp;</td>
                                                <td style="text-align: left; ">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lbl_Grupo" runat="server" Text="Nombre:"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:TextBox ID="txtGrupo" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="16px" Width="343px"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lbl_MinPart" runat="server" Text="Mínimo de Participaciones:"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <ig:WebNumericEditor ID="wneMinPart" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxValue="10" MinValue="0" Nullable="False" Width="60px">
                                                    </ig:WebNumericEditor>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="height: 28px;">
                                            <tr>
                                                <td style="text-align: right; width: 123px;">&nbsp;</td>
                                                <td style="text-align: center; width: 410px;">
                                                    <asp:GridView ID="gvIntegrantes" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="400px">
                                                        <Columns>
                                                            <asp:BoundField DataField="id_dt_grupo" HeaderText="id_dt_grupo" />
                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                            <ItemStyle Width="15px" />
                                                            </asp:CommandField>
                                                            <asp:BoundField DataField="integrante" HeaderText="Integrante">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="secretario" HeaderText="Secretario">
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
                                                <td style="vertical-align: top; text-align: center; width: 30px;">
                                                    <asp:ImageButton ID="ibtnAltaInt" runat="server" ImageUrl="images\Add.png" ToolTip="Agregar" Width="20px" />
                                                </td>
                                                <td style="vertical-align: top; text-align: center; width: 30px;">
                                                    <asp:ImageButton ID="ibtnBajaInt" runat="server" ImageUrl="images\Trash.png" style="height: 20px" ToolTip="Eliminar" Width="20px" />
                                                </td>
                                                <td style="vertical-align: top; text-align: center; width: 30px;">
                                                    <asp:ImageButton ID="ibtnModifInt" runat="server" ImageUrl="images\Edit.png" ToolTip="Modificar" Width="20px" />
                                                </td>
                                                <td style="vertical-align: top; text-align: left; width: 200px;">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlAutorizador" runat="server">
                                        <table style="width:100%;">
                                            <tr>
                                                <td style="width: 130px;">&nbsp;</td>
                                                <td>
                                                    <table style="border: thin solid #000000; width: 850px; height: 44px;">
                                                        <tr>
                                                            <td style="text-align: right; width: 115px;">
                                                                <asp:Label ID="lbl_UsuarioInt" runat="server" Text="Integrante:"></asp:Label>
                                                            </td>
                                                            <td class="auto-style30" style="text-align: left; ">
                                                                <asp:TextBox ID="txtUsuarioIntB" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="140px"></asp:TextBox>
                                                            </td>
                                                            <td style="text-align: left; width: 115px;">
                                                                <asp:Button ID="cmdBuscarUsrInt" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Buscar" />
                                                            </td>
                                                            <td rowspan="2" style="width: 120px">
                                                                <asp:CheckBox ID="cbSecretario" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Secretario" />
                                                            </td>
                                                            <td rowspan="2" style="text-align: center">
                                                                <asp:Button ID="btnAceptarInt" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnAceptarInt_Click" OnClientClick="this.disabled = true;" Text="Aceptar" UseSubmitBehavior="false" Width="90px" />
                                                            </td>
                                                            <td rowspan="2" style="text-align: center">
                                                                <asp:Button ID="btnCancelarInt" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnCancelarInt_Click" OnClientClick="this.disabled = true;" Text="Cancelar" UseSubmitBehavior="false" Width="90px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right; width: 115px;">&nbsp;</td>
                                                            <td colspan="2" style="text-align: left;">
                                                                <asp:DropDownList ID="ddlUsuarioInt" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
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
