<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="CatConceptoSF.aspx.vb" Inherits="ProcAd.CatConceptoSF" %>
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
        .auto-style10 {
            height: 17px;
        }
                
        .auto-style29 {
            width: 963px;
        }
        .auto-style30 {
            width: 165px;
        }
        .auto-style31 {
            width: 143px;
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
                                    <asp:ListItem Value="nombre_concepto">Concepto</asp:ListItem>
                                    <asp:ListItem Value="abreviatura">Abreviatura</asp:ListItem>
                                    <asp:ListItem Value="categoria">Categoría</asp:ListItem>
                                    <asp:ListItem Value="cuenta">Cuenta</asp:ListItem>
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
                                <asp:GridView ID="gvConcepto" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="1100px">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <Columns>
                                        <asp:BoundField DataField="id_concepto" HeaderText="id_concepto">
                                        <ItemStyle Width="30px" />
                                        </asp:BoundField>
                                        <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" SelectText="" ShowSelectButton="True">
                                        <ItemStyle Width="20px" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="concepto" HeaderText="Concepto">
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="abreviatura" HeaderText="Abreviatura">
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cuenta" HeaderText="Cuenta Contable">
                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="categoria" HeaderText="Categoría">
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="alimentos" HeaderText="Valida Alimentos">
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="no_conceptos" HeaderText="No. Conceptos">
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="centro_costo" HeaderText="Centro de Costo" />
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
                                        <td 
                                            style="text-align: right; width: 130px;">
                                            <asp:Label ID="lbl_TipoMov" runat="server" Text="Tipo de Movimiento:"></asp:Label>
                                        </td>
                                        <td style="text-align: left; " class="auto-style31">
                                            <asp:Label ID="lblTipoMov" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td style="text-align: right; " class="auto-style30">
                                            <asp:Label ID="lbl_Categoria" runat="server" Text="Categoría:"></asp:Label>
                                        </td>
                                        <td style="text-align: left; ">
                                            <asp:DropDownList ID="ddlCategoria" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Label ID="lbl_Abrev" runat="server" Text="Abreviatura:"></asp:Label>
                                        </td>
                                        <td style="text-align: left" class="auto-style31">
                                            <asp:TextBox ID="txtAbrev" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="50px"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right" class="auto-style30">
                                            <asp:Label ID="lbl_Concepto" runat="server" Text="Concepto:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtConcepto" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="330px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Label ID="lbl_CuentaC" runat="server" Text="Cuenta Cont.:"></asp:Label>
                                        </td>
                                        <td style="text-align: left" class="auto-style31">
                                            <asp:TextBox ID="txtCuentaC" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="80px"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right" class="auto-style30">
                                            <asp:Label ID="lbl_TipoComp" runat="server" Text="Tipo Comprobación:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlTipoComp" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="160px">
                                                <asp:ListItem Value="1">Tabulador y Catalogo</asp:ListItem>
                                                <asp:ListItem Value="2">Otros Conceptos</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Label ID="lbl_Empresa" runat="server" Text="Empresa:"></asp:Label>
                                        </td>
                                        <td style="text-align: left" class="auto-style31">
                                            <asp:DropDownList ID="ddlEmpresa" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="130px" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right" class="auto-style30">
                                            <asp:Label ID="lbl_CC" runat="server" Text="Centro de Costo:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlCC" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Label ID="lbl_NoConceptos" runat="server" Text="No. de Conceptos:"></asp:Label>
                                        </td>
                                        <td style="text-align: left" class="auto-style31">
                                            <asp:DropDownList ID="ddlNoConceptos" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="40px" AutoPostBack="True">
                                                <asp:ListItem>0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left" class="auto-style30">
                                            <asp:CheckBox ID="cbValAlimentos" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Validación de Alimentos" />
                                        </td>
                                        <td style="text-align: center">
                                            <asp:CheckBox ID="cbValOrigen_Destino" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Validación de Origen y Destino" />
                                        </td>
                                    </tr>
                                </table>
                                <table style="width:100%;">
                                    <tr>
                                        <td style="text-align: right; width: 105px;">
                                            <asp:Label ID="lbl_Cant1" runat="server" Text="Cantidad 1:"></asp:Label>
                                        </td>
                                        <td style="text-align: left; width: 60px;">
                                            <asp:DropDownList ID="ddlCant1" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="40px">
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right; width: 60px;">
                                            <asp:Label ID="lbl_Clave1" runat="server" Text="Clave 1:"></asp:Label>
                                        </td>
                                        <td style="text-align: left; width: 180px;">
                                            <asp:DropDownList ID="ddlClave1" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="130px">
                                                <asp:ListItem>Desayuno/Cena</asp:ListItem>
                                                <asp:ListItem>Comida</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right; width: 80px;">
                                            <asp:Label ID="lbl_Cant2" runat="server" Text="Cantidad 2:"></asp:Label>
                                        </td>
                                        <td style="text-align: left; width: 60px;">
                                            <asp:DropDownList ID="ddlCant2" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="40px">
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right; width: 60px;">
                                            <asp:Label ID="lbl_Clave2" runat="server" Text="Clave 2:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlClave2" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="130px">
                                                <asp:ListItem>Desayuno/Cena</asp:ListItem>
                                                <asp:ListItem>Comida</asp:ListItem>
                                            </asp:DropDownList>
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
