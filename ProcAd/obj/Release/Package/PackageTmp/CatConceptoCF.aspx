<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="CatConceptoCF.aspx.vb" Inherits="ProcAd.CatConceptoCF" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
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
            width: 100%;
            height: 31px;
            margin-top: 59px;
        }
        .auto-style33 {
            width: 130px;
            height: 26px;
        }
        .auto-style34 {
            width: 55px;
            height: 26px;
        }
        .auto-style35 {
            width: 60px;
            height: 26px;
        }
        .auto-style36 {
            width: 180px;
            height: 26px;
        }
        .auto-style38 {
            width: 245px;
        }
        .auto-style39 {
            width: 100%;
        }
        .auto-style40 {
            width: 180px;
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
                            <td style="width: 180px">
                                <ig:WebScriptManager ID="wsmCatConceptoCF" runat="server">
                                </ig:WebScriptManager>
                            </td>
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
                                    <asp:ListItem Value="concepto">Concepto</asp:ListItem>
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
                                        <asp:BoundField DataField="id_concepto_comp" HeaderText="id_concepto_comp">
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
                                        <asp:BoundField DataField="iva" HeaderText="IVA">
                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
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
                                        <td style="text-align: left; width: 145px;">
                                            <asp:Label ID="lblTipoMov" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td style="text-align: right; width: 150px;">
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
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtAbrev" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="50px"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right">
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
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtCuentaC" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="80px"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lbl_IVA" runat="server" Text="IVA:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <ig:WebNumericEditor ID="wneIVA" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxDecimalPlaces="2" MinDecimalPlaces="2" Width="60px">
                                            </ig:WebNumericEditor>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Label ID="lbl_Empresa" runat="server" Text="Empresa:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlEmpresa" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="130px" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lbl_CC" runat="server" Text="Centro de Costo:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlCC" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width:100%;">
                                    <tr>
                                        <td style="text-align: right; width: 130px;">
                                            <asp:Label ID="lbl_NoConceptos" runat="server" Text="No. de Conceptos:"></asp:Label>
                                        </td>
                                         <td style="width: 55px; text-align: left;">
                                            <asp:DropDownList ID="ddlNoConceptos" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="40px" AutoPostBack="True">
                                                <asp:ListItem>0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                       <td style="text-align: center; " class="auto-style38">
                                            <asp:CheckBox ID="cbCombustible" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Combustible" />
                                        </td>
                                        <td style="text-align: left; width: 350px;">
                                            &nbsp;&nbsp;
                                            <asp:CheckBox ID="cbValAlimentos" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Validación de Alimentos" />
                                        </td>
                                    </tr>
                                </table>
                                <table style="width:100%;">
                                    <tr>
                                        <td style="text-align: right; width: 130px;">
                                            <asp:Label ID="lbl_Cant1" runat="server" Text="Cantidad 1:"></asp:Label>
                                        </td>
                                        <td style="width: 55px; text-align: left;">
                                            <asp:DropDownList ID="ddlCant1" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="40px">
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right; width: 60px;">
                                            <asp:Label ID="lbl_Clave1" runat="server" Text="Clave 1:"></asp:Label>
                                        </td>
                                        <td style="text-align: left; " class="auto-style40">
                                            <asp:DropDownList ID="ddlClave1" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="130px">
                                                <asp:ListItem>Desayuno/Cena</asp:ListItem>
                                                <asp:ListItem>Comida</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                       <td style="text-align: left; width: 350px;">
                                           &nbsp;&nbsp;
                                            <asp:CheckBox ID="cbReqAutDir" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Requiere Aut. Director" />
                                        </td>
                                    </tr>
                                </table>
                                 <table class="auto-style39">
                                    <tr>
                                        <td style="text-align: right; " class="auto-style33">
                                            <asp:Label ID="lbl_Cant2" runat="server" Text="Cantidad 2:"></asp:Label>
                                        </td>
                                        <td style="text-align: left;" class="auto-style34">
                                            <asp:DropDownList ID="ddlCant2" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="40px">
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right; " class="auto-style35">
                                            <asp:Label ID="lbl_Clave2" runat="server" Text="Clave 2:"></asp:Label>
                                        </td>
                                        <td style="text-align: left; " class="auto-style36">
                                            <asp:DropDownList ID="ddlClave2" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="130px">
                                                <asp:ListItem>Desayuno/Cena</asp:ListItem>
                                                <asp:ListItem>Comida</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left; width: 350px;">
                                            &nbsp;&nbsp;
                                            <asp:CheckBox ID="cbVali_Orineg_Destino" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Validación de Origen y Destino." />
                                        </td>
                                    </tr>
                                </table>
                                <table class="auto-style30">
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
