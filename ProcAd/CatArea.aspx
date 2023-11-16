<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="CatArea.aspx.vb" Inherits="ProcAd.CatArea" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        .auto-style29 {
            width: 963px;
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
                                <td>
                                    <ig:WebScriptManager ID="wsmCatArea" runat="server">
                                    </ig:WebScriptManager>
                                </td>
                                <td style="width: 150px">
                                    <asp:TextBox ID="txtValor" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="140px"></asp:TextBox>
                                </td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="ddlCampo" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="110px">
                                        <asp:ListItem Value="cg_empresa.nombre">Empresa</asp:ListItem>
                                        <asp:ListItem Value="direccion">Dirección</asp:ListItem>
                                        <asp:ListItem Value="area">Área</asp:ListItem>
                                        <asp:ListItem Value="cg_empleado.nombre + ' ' + cg_empleado.ap_paterno + ' ' + ap_materno">Autorizador</asp:ListItem>
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
                                <td style="text-align: center; width: 100px;">&nbsp;</td>
                                <td class="auto-style29">
                                    <asp:GridView ID="gvArea" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="1120px">
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>
                                            <asp:BoundField DataField="id_dt_area" HeaderText="id_dt_area">
                                            <ItemStyle Width="30px" />
                                            </asp:BoundField>
                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" SelectText="" ShowSelectButton="True">
                                            <ItemStyle Width="20px" />
                                            </asp:CommandField>
                                            <asp:BoundField DataField="empresa" HeaderText="Empresa" >
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="direccion" HeaderText="Dirección" >
                                            <ItemStyle Width="400px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="area" HeaderText="Área" >
                                            <ItemStyle Width="400px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="autorizador" HeaderText="Autorizador" >
                                            <ItemStyle Width="300px" />
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
                                    <table style="height: 28px; width: 100%;">
                                        <tr>
                                            <td style="text-align: right; width: 205px;">
                                                <asp:Label ID="lbl_Empresa" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Empresa:"></asp:Label>
                                            </td>
                                            <td style="text-align: left; ">
                                                <asp:DropDownList ID="ddlEmpresa" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 205px;">
                                                <asp:Label ID="lbl_Direccion" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Dirección:"></asp:Label>
                                            </td>
                                            <td style="text-align: left; ">
                                                <asp:DropDownList ID="ddlDirecccion" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="350px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 205px;">
                                                <asp:Label ID="lbl_Area" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Área:"></asp:Label>
                                            </td>
                                            <td style="text-align: left; ">
                                                <asp:TextBox ID="txtArea" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxLength="340" Width="450px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 205px;">
                                                <asp:Label ID="lbl_Autorizador" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Autorizador:"></asp:Label>
                                            </td>
                                            <td style="text-align: left; ">
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td style="width: 120px">
                                                            <asp:TextBox ID="txtAutorizador" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxLength="340" Width="110px"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 30px">
                                                            <asp:ImageButton ID="ibtnBuscarAut" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlAutorizador" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="350px" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 205px;">
                                                <asp:Label ID="lbl_AutorizadorUsr" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Usuario ProcAd:"></asp:Label>
                                            </td>
                                            <td style="text-align: left; ">
                                                <asp:Label ID="lblAutorizadorUsr" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"></asp:Label>
                                                <asp:Label ID="lblAutorizadorID" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%; height: 50px;">
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Button ID="btnAceptar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Aceptar" Width="150px" />
                                            </td>
                                            <td style="text-align: center">
                                                <asp:Button ID="btnCancelar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Cancelar" Width="150px" />
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
