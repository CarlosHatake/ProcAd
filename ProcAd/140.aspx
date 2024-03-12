<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="140.aspx.vb" Inherits="ProcAd._140" %>
<%@ Register Assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style5 {
            height: 63px;
        }
        .auto-style6 {
            width: 151px;
        }
    </style>
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
                <asp:TextBox ID="_txtFolio" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtTipoAnt" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtEscenario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtPedidoComp" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdEmpresa" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsInst" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdSolicitud" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtImporte" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtTope1" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtTope2" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtRFCProv" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtRFCEmpr" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_ImporteComprobado" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_ImporteDevolucion" runat="server" Width="15px" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; color: #FF0000;">
                <asp:Label ID="lblMessage" runat="server" Font-Names="Verdana" Font-Size="8pt" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlInicio" runat="server">
                    <table style="width: 1350px; height: 25px; font-family: Verdana; font-size: 8pt;">
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





                    <table style="width: 1350px; height: 25px; font-family: Verdana; font-size: 8pt;">
                        <tr>
                            <td>
                                <asp:Panel runat="server" ID="pnlInformacionGeneral">
                                    <table>
                                        <tr>
                                            <td style="width: 80px"></td>
                                            <td style="width: 175px; text-align: right">
                                                <asp:Label runat="server" ID="lbl_Empresa" Text="Empresa:"></asp:Label>
                                            </td>
                                            <td class="auto-style6">
                                                <asp:Label runat="server" ID="lblEmpresa" Font-Bold="true" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td style="width: 35px"></td>
                                            <td style="width: 130px; text-align: right">
                                                <asp:Label ID="lbl_TipoOperacion" runat="server" Text="Tipo de Gasto:" Visible="false"></asp:Label>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:RadioButtonList runat="server" ID="rbTipoOperacion" Width="250px" RepeatColumns="4" AutoPostBack="true" Visible="false">
                                                    <asp:ListItem>Administrativo</asp:ListItem>
                                                    <asp:ListItem>Operativo</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                             <td style="width: 35px"></td>
                                            <td style="text-align: right; width: 100px">
                                                <asp:Label ID="lbl_CC" runat="server" Text="Centro de Costo:" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 300px">
                                                <asp:UpdatePanel runat="server" ID="updCC" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlCC" runat="server" Width="300px" Visible="false" AutoPostBack="true"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="width: 80px"></td>
                                            <td style="width: 175px; text-align: right">
                                                <asp:Label runat="server" ID="lbl_ProvedoorNAV" Text="Proveedor (NAV):"></asp:Label>
                                            </td>
                                            <td class="auto-style6">
                                                <asp:Label runat="server" ID="lblProveedorNAV" Font-Bold="true" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lbl_Autorizador" runat="server" Text="Autorizador:" Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel runat="server" ID="upAutorizador">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlAutorizador" runat="server" Width="250px" Visible="false"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                             <td style="width: 35px"></td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lbl_Div" runat="server" Text="División:" Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel runat="server" ID="upDiv" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlDiv" runat="server" Width="300px" Visible="false" AutoPostBack="true"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 80px"></td>
                                            <td style="width: 175px; text-align: right">
                                                <asp:Label runat="server" ID="lbl_NumProveedorNAV" Text="Número del Proveedor (NAV):"></asp:Label>
                                            </td>
                                            <td class="auto-style6">
                                                <asp:Label runat="server" ID="lblNumProveedorNAV" Font-Bold="true" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lbl_Autorizador2" runat="server" Text="Segundo Autorizador:" Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel runat="server" ID="up_Autorizador2">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlAutorizador2" runat="server" Width="250px" Visible="false"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                             <td style="width: 35px"></td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lbl_ProveedorGral" runat="server" Text="Proveedor:" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 160px">
                                                            <asp:TextBox ID="txtProveedor" runat="server" Width="160px" Visible="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ibtnBuscarProveedor" runat="server" ImageUrl="images\Search.png" Visible="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 80px"></td>
                                            <td>&nbsp;</td>
                                            <td class="auto-style6">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lbl_Autorizador3" runat="server" Text="Tercer Autorizador:" Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel runat="server" ID="upAutorizador3">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlAutorizador3" runat="server" Width="250px" Visible="false"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                             <td style="width: 35px"></td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:UpdatePanel runat="server" ID="upProveedor" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlProveedor" runat="server" Width="300px" Visible="false" AutoPostBack="True"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </td>
                                            <td style="width: 40px; vertical-align: top; text-align: right">
                                                <asp:ImageButton ID="ibtnAltaProveedor" runat="server" ImageUrl="images\Add.png" ToolTip="Alta" Width="20px" Visible="false" />
                                            </td>
                                            <td style="width: 30px; vertical-align: top; text-align: right">
                                                <asp:ImageButton ID="ibtnBajaProveedor" runat="server" ImageUrl="images\Trash.png" ToolTip="Baja" Width="20px" Visible="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>


                    <asp:Panel runat="server" ID="pnlSeccion1">
                        <table style="width: 1366px; height: 44px;">

                            <%--<tr>
                                    <td style="text-align: right; width: 130px">
                                        <asp:Label ID="lbl_Empresa" runat="server" Text="Empresa:"></asp:Label>
                                    </td>
                                    <td style="width: 550px">
                                        <asp:Label ID="lblEmpresa" runat="server"></asp:Label>
                                    </td>

                                    <td style="text-align: right; width: 170px">
                                        <asp:Label ID="lbl_Proveedor" runat="server" Text="Proveedor (NAV):"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblProveedor" runat="server"></asp:Label>
                                    </td>
                                </tr>--%>
                            <%-- <tr>
                                    <td></td>
                                    <td></td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lbl_NumProveedor" runat="server" Text="Numero del Proveedor (NAV):"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNumProveedor" runat="server"></asp:Label>
                                    </td>
                                </tr>--%>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="height: 100px; width: 120px"></td>
                                <td>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblCFDI" runat="server" Text="CFDI ANTICIPO:" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:GridView ID="gvCFDIAnticipoEsc1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px" DataKeyNames="id_dt_factura, importe">
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                            <ItemStyle Width="15px" />
                                                        </asp:CommandField>
                                                        <asp:BoundField DataField="fecha_emision" HeaderText="Fecha Emisión">
                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="uuid" HeaderText="UUID">
                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="serie" HeaderText="Serie">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="folio" HeaderText="Folio">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="lugar_exp" HeaderText="Lugar Exp.">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="forma_pago" HeaderText="Forma Pago">
                                                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="moneda" HeaderText="Moneda">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="subtotal" HeaderText="Subtotal">
                                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="importe" HeaderText="Importe">
                                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
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
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 1200px">
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:Button ID="btnGuardarSeccion1" runat="server" Text="Guardar" Font-Names="Verdana" Font-Size="8pt" Width="160px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>


                    <asp:Panel runat="server" ID="pnlSeccion2">

                        <asp:Panel runat="server" ID="pnlgvFacturas" Visible="false">
                            <table style="width: 1366px; height: 44px;">
                                <tr>
                                    <td style="height: 100px; width: 120px"></td>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="text-align: right; width: 150px">
                                                    <asp:Label ID="lblCFDIAnticipo" runat="server" Text="CFDI ANTICIPO:" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:GridView ID="gvCFDIAnticipoSec2" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px" DataKeyNames="id_dt_factura">
                                                        <Columns>
                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                <ItemStyle Width="15px" />
                                                            </asp:CommandField>
                                                            <asp:BoundField DataField="fecha_emision" HeaderText="Fecha Emisión">
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="uuid" HeaderText="UUID">
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="serie" HeaderText="Serie">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="folio" HeaderText="Folio">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="lugar_exp" HeaderText="Lugar Exp.">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="forma_pago" HeaderText="Forma Pago">
                                                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="moneda" HeaderText="Moneda">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="subtotal" HeaderText="Subtotal">
                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="importe" HeaderText="Importe">
                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
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
                                        </table>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="height: 100px; width: 120px"></td>

                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="text-align: right; width: 150px">
                                                    <asp:Label ID="lblCFDIFactura" runat="server" Text="CFDI FACTURA:" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:GridView ID="gvCFDIFacturaSec2" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px" DataKeyNames="id_dt_factura">
                                                        <Columns>
                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                <ItemStyle Width="15px" />
                                                            </asp:CommandField>
                                                            <asp:BoundField DataField="fecha_emision" HeaderText="Fecha Emisión">
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="uuid" HeaderText="UUID">
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="serie" HeaderText="Serie">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="folio" HeaderText="Folio">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="lugar_exp" HeaderText="Lugar Exp.">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="forma_pago" HeaderText="Forma Pago">
                                                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="moneda" HeaderText="Moneda">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="subtotal" HeaderText="Subtotal">
                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="importe" HeaderText="Importe">
                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
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
                                        </table>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="height: 100px; width: 120px"></td>

                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="text-align: right; width: 150px">
                                                    <asp:Label ID="lblCFDIEgreso" runat="server" Text="CFDI EGRESO:" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:GridView ID="gvCFDIEgresoEsc1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px" DataKeyNames="id_dt_factura">
                                                        <Columns>
                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                <ItemStyle Width="15px" />
                                                            </asp:CommandField>
                                                            <asp:BoundField DataField="fecha_emision" HeaderText="Fecha Emisión">
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="uuid" HeaderText="UUID">
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="serie" HeaderText="Serie">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="folio" HeaderText="Folio">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="lugar_exp" HeaderText="Lugar Exp.">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="forma_pago" HeaderText="Forma Pago">
                                                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="moneda" HeaderText="Moneda">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="subtotal" HeaderText="Subtotal">
                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="importe" HeaderText="Importe">
                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
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
                                        </table>
                                    </td>

                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlAdjuntos" Visible="false">
                            <table>
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 120px"></td>
                                                <td style="width: 150px; text-align: right">
                                                    <asp:Label ID="pnl_Adjunto" runat="server" Text="Archivo Adjunto"></asp:Label>
                                                </td>
                                                <td style="width: 10px"></td>
                                                <td style="width: 330px">
                                                    <asp:FileUpload ID="fuAdjunto" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnAgregarAdj" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnAgregarAdj_Click" OnClientClick="this.disabled = true; this.value='Procesando…';" Text="Agregar Archivo" UseSubmitBehavior="false" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Panel runat="server" ID="pnlAdjuntosGv" Visible="false">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td style="width: 270px; text-align: right">
                                                                    <asp:Label ID="lbl_Adjunto" runat="server" Text="Adjuntos:"></asp:Label>
                                                                </td>
                                                                <td style="width: 10px"></td>
                                                                <td style="width: 600px; vertical-align: top">
                                                                    <asp:UpdatePanel ID="upAdjuntosEscenario1" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:GridView ID="gvAdjuntosEscenario1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ShowHeader="False" Width="300px">
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
                                                                <td class="auto-style5"></td>
                                                                <td class="auto-style5"></td>
                                                                <td class="auto-style5"></td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="width: 1266px">
                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:Button ID="btnAutorizacion" runat="server" Text="Solicitar Autorización" Font-Names="Verdana" Font-Size="8pt" Width="180px" Visible="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>



                    </asp:Panel>



                    <%-- Primer escenario de comprobación --%>
                    <%--<asp:Panel ID="pnlAnticipoSinPedidoCompra" runat="server" Visible="false">
                        <asp:Panel ID="pnlSeccion1AntSinPedidoCompra" runat="server" Visible="false">
                            <table style="width: 1366px; height: 40px">
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblSeccion" runat="server" Text="SECCIÓN 1 - PRIMER ESCENARIO" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            
                        </asp:Panel>
                        <asp:Panel ID="pnlSeccion2AntSinPedidoCompra" runat="server" Visible="false">
                            <table style="width: 1366px; height: 44px;">--%>
                    <%--<tr>
                                    <td style="text-align: right; width: 130px">
                                        <asp:Label ID="lbl_EmpresaSec2" runat="server" Text="Empresa:"></asp:Label>
                                    </td>
                                    <td style="width: 550px">
                                        <asp:Label ID="lblEmpresaSec2" runat="server"></asp:Label>
                                    </td>

                                    <td style="text-align: right; width: 170px">
                                        <asp:Label ID="lbl_CentroCosto" runat="server" Text="Centro de Costo:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel runat="server" ID="upCC1">
                                            <ContentTemplate>
                                                 <asp:DropDownList ID="ddlCentroCostoSinPedidoCompra" runat="server" Width="190px" Visible ="false"> </asp:DropDownList>
                                            </ContentTemplate>
                                           
                                        </asp:UpdatePanel> 
                                    </td>
                                       
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width: 250px; text-align:right">
                                                  <asp:Label ID="lblTipoGastoEsc1" runat="server" Text="Tipo de Gasto:"></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:RadioButtonList runat="server" ID="rbTipoOperacionAntSinPedidoC" Width="250px" RepeatColumns="4" AutoPostBack="true" >
                                                        <asp:ListItem>Administrativo</asp:ListItem>
                                                        <asp:ListItem>Operativo</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>

                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblDivision" runat="server" Text="División:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel runat="server" ID="upDivision1">
                                            <ContentTemplate >
                                                <asp:DropDownList ID="ddlDivisionAntSinPedidoC" runat="server" Width="190px" Visible ="false"></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        
                                    </td>
                                </tr>--%>
                    <%--   <tr>
                                    <td></td>
                                    <td style="text-align: center">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 250px"></td>
                                                <td>
                                                    <asp:Label ID="lblSeccion2" runat="server" Text="SECCIÓN 2 - PRIMER ESCENARIO" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                               
                                
                            </table>
                            
                            
                        </asp:Panel>
                    </asp:Panel>--%>
                    <%-- *-----------------------------------* --%>

                    <%-- Segundo escenario de comprobación --%>
                    <%--  <asp:Panel ID="pnlAnticipoConPedidoCompra" runat="server" Visible="false">
                        <asp:Panel ID="pnlSeccion1AntConPedidoCompra" runat="server">
                            <table style="width: 1366px; height: 40px">
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Label ID="lbl_Seccion1ConPedidoCompra" runat="server" Text="SECCIÓN 1 - SEGUNDO ESCENARIO"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 1366px; height: 44px;">--%>
                    <%--<tr>
                                    <td style="text-align: right; width: 130px">
                                        <asp:Label ID="lbl_EmpresaConPedidoCompra" runat="server" Text="Empresa:"></asp:Label>
                                    </td>
                                    <td style="width: 550px">
                                        <asp:Label ID="lblEmpresaConPedidoCompra" runat="server"></asp:Label>
                                    </td>

                                    <td style="text-align: right; width: 170px">
                                        <asp:Label ID="lbl_ProveedorConPedidoCompra" runat="server" Text="Proveedor (NAV):"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblProveedorConPedidoCompra" runat="server"></asp:Label>
                                    </td>
                                </tr>--%>
                    <%-- <tr>
                                    <td></td>
                                    <td></td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lbl_NumProveedorConPedidoCompra" runat="server" Text="Numero del Proveedor (NAV):"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNumProveedorConPedidoCompra" runat="server"></asp:Label>
                                    </td>
                                </tr>--%>
                    <%--<tr>
                                    <td style="height: 105px"></td>
                                    <td>--%>
                    <%--<table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_CFDIAnticipo" runat="server" Text="CFDI ANTICIPO:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:GridView ID="gvCFDIAnticipoConPedidoCompra" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px" DataKeyNames="id_dt_factura">
                                                        <Columns>--%>
                    <%--<asp:BoundField DataField="id" HeaderText="idCFDI" />--%>
                    <%--                     <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                <ItemStyle Width="15px" />
                                                            </asp:CommandField>
                                                            <asp:BoundField DataField="fechaEmision" HeaderText="Fecha Emisión">
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="UUID" HeaderText="UUID">
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="serie" HeaderText="Serie">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="folio" HeaderText="Folio">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="lugarExp" HeaderText="Lugar Exp.">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="formaPago" HeaderText="Forma Pago">
                                                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="moneda" HeaderText="Moneda">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="subtotal" HeaderText="Subtotal">
                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="importe" HeaderText="Importe">
                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
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
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 1200px; height: 104px;">
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnGuardarConPedidoCompra" runat="server" Text="Guardar" Font-Names="Verdana" Font-Size="8pt" Width="160px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlSeccion2AntConPedidoCompra" runat="server">
                            <table style="width: 1366px; height: 44px;">--%>
                    <%--  <tr>
                                    <td style="text-align: right; width: 130px">
                                        <asp:Label ID="lbl_EmpresaSeccion2ConPedidoCompra" runat="server" Text="Empresa:"></asp:Label>
                                    </td>
                                    <td style="width: 550px">
                                        <asp:Label ID="lblEmpresaSeccion2ConPedidoCompra" runat="server"></asp:Label>
                                    </td>

                                    <td style="text-align: right; width: 170px">
                                        <asp:Label ID="lbl_CentroCostoConPedidoCompra" runat="server" Text="Centro de Costo:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel runat="server" ID="upCC2">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlCentroCostoConPedidoCompra" runat="server" Width="190px">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width: 250px;text-align:right">
                                                    <asp:Label ID="lblTipoGastoEsc2" runat="server" Text="Tipo de Gasto:"></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:RadioButtonList runat="server" ID="rbTipoOperacionConPedidoCompra" Width="250px" RepeatColumns="4" AutoPostBack="true" >
                                                        <asp:ListItem>Administrativo</asp:ListItem>
                                                        <asp:ListItem>Operativo</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>

                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lbl_Division" runat="server" Text="División:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel runat="server" ID="upDivision2">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlDivisionConPedidoCompra" runat="server" Width="190px">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>--%>
                    <%--  <tr>
                                    <td></td>
                                    <td style="text-align: center">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 250px"></td>
                                                <td>
                                                    <asp:Label ID="lbl_Seccion2" runat="server" Text="SECCIÓN 2 - ESCENARIO 2"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_CFDIAnticipoConPedidoCompra" runat="server" Text="CFDI ANTICIPO:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:GridView ID="gvCFDIAnticipoEsc2" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px" DataKeyNames="id_dt_factura">
                                                        <Columns>--%>
                    <%--<asp:BoundField DataField="id" HeaderText="idCFDI" />--%>
                    <%-- <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                <ItemStyle Width="15px" />
                                                            </asp:CommandField>
                                                            <asp:BoundField DataField="fechaEmision" HeaderText="Fecha Emisión">
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="UUID" HeaderText="UUID">
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="serie" HeaderText="Serie">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="folio" HeaderText="Folio">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="lugarExp" HeaderText="Lugar Exp.">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="formaPago" HeaderText="Forma Pago">
                                                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="moneda" HeaderText="Moneda">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="subtotal" HeaderText="Subtotal">
                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="importe" HeaderText="Importe">
                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
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
                                        </table>
                                    </td>

                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_CFDIFactura" runat="server" Text="CFDI FACTURA:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:GridView ID="gvCFDIFacturaEsc2" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px" DataKeyNames="id_dt_factura">
                                                        <Columns>--%>
                    <%--<asp:BoundField DataField="id" HeaderText="idCFDI" />--%>
                    <%--    <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                <ItemStyle Width="15px" />
                                                            </asp:CommandField>
                                                            <asp:BoundField DataField="fechaEmision" HeaderText="Fecha Emisión">
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="UUID" HeaderText="UUID">
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="serie" HeaderText="Serie">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="folio" HeaderText="Folio">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="lugarExp" HeaderText="Lugar Exp.">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="formaPago" HeaderText="Forma Pago">
                                                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="moneda" HeaderText="Moneda">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="subtotal" HeaderText="Subtotal">
                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="importe" HeaderText="Importe">
                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
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
                                        </table>
                                    </td>

                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_CFDIEgreso" runat="server" Text="CFDI EGRESO:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:GridView ID="gvCFDIEgresoEsc2" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px" DataKeyNames="id_dt_factura">
                                                        <Columns>--%>
                    <%--<asp:BoundField DataField="id" HeaderText="idCFDI" />--%>
                    <%--   <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                <ItemStyle Width="15px" />
                                                            </asp:CommandField>
                                                            <asp:BoundField DataField="fechaEmision" HeaderText="Fecha Emisión">
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="UUID" HeaderText="UUID">
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="serie" HeaderText="Serie">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="folio" HeaderText="Folio">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="lugarExp" HeaderText="Lugar Exp.">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="formaPago" HeaderText="Forma Pago">
                                                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="moneda" HeaderText="Moneda">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="subtotal" HeaderText="Subtotal">
                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="importe" HeaderText="Importe">
                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
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
                                        </table>
                                    </td>

                                </tr>
                            </table>
                            <table style="width: 1266px">
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnAutorizacionEsc2" runat="server" Text="Solicitar Autorización" Width="180px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:Panel>--%>
                    <%-- *-----------------------------------* --%>


                    <%-- Tercer escenario de comprobación --%>
                    <%-- <asp:Panel ID="pnlPagoAnticipadoSinPedidosCompra" runat="server" Visible="false">
                        <table style="width: 1366px; height: 44px;">--%>
                    <%-- <tr>
                                <td style="text-align: right; width: 110px">
                                    <asp:Label ID="lbl_Solicitante" runat="server" Text="Solicitante: "></asp:Label>
                                </td>
                                <td style="width: 200px">
                                    <asp:Label ID="lblSolicitante" runat="server"></asp:Label>
                                </td>

                                <td style="width: 150px; text-align:right">
                                    <asp:Label ID="lbl_EmpresaPagoAnticipado" runat="server" Text="Empresa: "></asp:Label>
                                </td>
                                <td style="width: 200px">
                                    <asp:Label ID="lblEmpresaPagoAnt" runat="server"></asp:Label>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lbl_ProveedorPagoAnticipado" runat="server" Text="Proveedor:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblProveedorPagoAnticipado" runat="server"></asp:Label>
                                </td>
                                <%--<td style="text-align: right; width: 70px">
                                    <asp:Label ID="lbl_CentroCostoPagoAnticipado" runat="server" Text="Centro de Costo:"></asp:Label>
                                </td>
                                <td style="width: 200px">
                                    <asp:Label ID="lblCentroCostoPagoAnticipado" runat="server"></asp:Label>
                                </td>--%>
                    <%-- </tr>--%>
                    <%--<tr>
                                <td></td>
                                <td></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lbl_Autorizador" runat="server" Text="Autorizador: "></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlAutorizadorEsc3" runat="server" Width="180px"></asp:DropDownList>
                                </td>
                                
                                <td style="text-align: right">
                                        <asp:Label ID="lblDivision3" runat="server" Text="División:"></asp:Label>
                                    </td>
                                <td>
                                        <asp:UpdatePanel runat="server" ID="upDivision3">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlDivisionPagoAnticipado" runat="server" Width="190px">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                            </tr>--%>
                    <%--<tr>
                                <td></td>
                                <td></td>
                                <td style="text-align: right">
                                    <asp:Label ID="lbl_SegundoAutorizador" runat="server" Text="Segundo Autorizador:"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSegundoAutorizador" runat="server"></asp:DropDownList>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lbl_CentroCostoPagoAnticipado2" runat="server" Text="Centro de Costo"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="upCC3" ></asp:UpdatePanel>
                                    <asp:DropDownList ID="ddlCentroCostoPagoAnticipado" runat="server" Width="190px"></asp:DropDownList>
                                </td>
                            </tr>--%>
                    <%--  <tr>
                                <td></td>
                                <td></td>
                                <td style="text-align:right">
                                    <asp:Label ID="lblTipoGastoEsc3" runat="server" Text="Tipo de Gasto:"></asp:Label>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbTipoOperacionPagoAnticipadoSinPedidoC" runat="server" AutoPostBack="true">
                                        <asp:ListItem>Administrativo</asp:ListItem>
                                        <asp:ListItem>Operativo</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>--%>
                    <%--   </table>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" Text ="SECCION 1 - ESCENARIO 3"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 110px; text-align: right">
                                    <asp:Label ID="lbl_Factura" runat="server" Text="Factura: "></asp:Label>
                                </td>
                                <td>
                                    <asp:GridView ID="gvFacturaEsc3" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px" DataKeyNames="id_dt_factura">
                                        <Columns>--%>
                    <%--<asp:BoundField DataField="id" HeaderText="idCFDI" />--%>
                    <%--     <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                <ItemStyle Width="15px" />
                                            </asp:CommandField>
                                            <asp:BoundField DataField="fechaEmision" HeaderText="Fecha Emisión">
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UUID" HeaderText="UUID">
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="serie" HeaderText="Serie">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="folio" HeaderText="Folio">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="lugarExp" HeaderText="Lugar Exp.">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="formaPago" HeaderText="Forma Pago">
                                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="moneda" HeaderText="Moneda">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="subtotal" HeaderText="Subtotal">
                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="importe" HeaderText="Importe">
                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
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
                        </table>
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 110px; text-align: right">
                                    <asp:Label ID="lbl_Evidencia" runat="server" Text="Evidencia: "></asp:Label>
                                </td>
                                <td style="width:330px">
                                    <asp:FileUpload ID="fuEvidenciaPagoAnticipado" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px" />
                                </td>
                                <td>
                                    <asp:Button ID="btnAgregarAdjEsc3" runat="server" Text="Agregar adjunto" Font-Names="Verdana" Font-Size="8pt" Width="100px" />
                                </td>
                            </tr>
                        </table>

                        <asp:Panel runat="server" ID="pnlAdjuntosEsc3" Visible="false">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 110px; text-align: right">
                                                                        <asp:Label ID="lblAdjuntosEscenario3" runat="server" Text="Adjuntos:"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 600px; vertical-align: top">
                                                                        <asp:UpdatePanel ID="upAdjuntosEscenario3" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <asp:GridView ID="gvAdjuntosEscenario3" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ShowHeader="False" Width="300px">
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
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                    </asp:Panel>
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: center">
                                    <asp:Button ID="btnAutorizacionEsc3" runat="server" Text="Guardar" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>--%>
                    <%-- *-----------------------------------* --%>

                    <%-- Cuarto escenario de comprobación --%>
                    <%--<asp:Panel ID="pnlPagoAnticipadoConPedidoCompra" runat="server" Visible="false">
                        <table style="width: 1266px; height: 44px">
                            <tr>
                                <td style="width: 50px"></td>
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
                                    <asp:GridView ID="gvListaValidacion" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px" DataKeyNames="id_dt_factura">
                                        <Columns>
                                           
                                            <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_solicita" HeaderText="Fecha de anticipado">
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="proveedor" HeaderText="Proveedor">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="importe_requerido" HeaderText="Importe">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="pedido_compra" HeaderText="Pedidos de compra">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="empleado_solicita" HeaderText="Usuario">
                                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbxSeleccionar" runat="server" AutoPostBack="true" OnCheckedChanged="cbxSeleccionar_CheckedChanged"/>
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
                    </asp:Panel>--%>
                    <%-- *-----------------------------------* --%>

                    <%-- Quinto escenario de comprobación --%>
                    <asp:Panel ID="pnlPagoAnticipadoAgenteAduanal" runat="server">
                        <asp:Panel runat="server" ID="pnlFacturasProveedor" Visible="false">
                            <table style="width: 1366px; height: 40px">
                                <tr>
                                    <td style="height: 100px; width: 75px"></td>
                                    <td style="width: 50px; text-align: right">
                                        <asp:Label ID="lblFacturasProveedor" runat="server" Text="Facturas proveedor:" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td style="width: 700px">
                                        <asp:GridView ID="gvFacturasProveedor" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px" DataKeyNames="id_dt_factura" AllowPaging="true" PageSize="10">
                                            <Columns>
                                                <%--<asp:BoundField DataField="id" HeaderText="idCFDI" />--%>
                                                <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                    <ItemStyle Width="15px" />
                                                </asp:CommandField>
                                                <asp:BoundField DataField="fecha_emision" HeaderText="Fecha Emisión">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="uuid" HeaderText="UUID">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="serie" HeaderText="Serie">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="folio" HeaderText="Folio">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="lugar_exp" HeaderText="Lugar Exp.">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="forma_pago" HeaderText="Forma Pago">
                                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="moneda" HeaderText="Moneda">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="subtotal" HeaderText="Subtotal">
                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="importe" HeaderText="Importe">
                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
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
                            </table>
                        </asp:Panel>
                        <table style="width: 1366px; height: 40px">
                            <tr>
                                <td style="height: 100px; width: 120px"></td>
                                <td style="width: 110px; text-align: right">
                                    <asp:Label ID="lbl_CFDIAnticipoAgenteAduanal" runat="server" Text="CFDI ANTICIPO:" Font-Bold="true" Visible="false"></asp:Label>
                                </td>
                                <td style="width: 700px">
                                    <asp:GridView ID="gvAgenteAduanal" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px" DataKeyNames="id_dt_factura, uuid">
                                        <Columns>
                                            <%--<asp:BoundField DataField="id" HeaderText="idCFDI" />--%>
                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                <ItemStyle Width="15px" />
                                            </asp:CommandField>
                                            <asp:BoundField DataField="fecha_emision" HeaderText="Fecha Emisión">
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="uuid" HeaderText="UUID">
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="serie" HeaderText="Serie">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="folio" HeaderText="Folio">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="lugar_exp" HeaderText="Lugar Exp.">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="forma_pago" HeaderText="Forma Pago">
                                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="moneda" HeaderText="Moneda">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="subtotal" HeaderText="Subtotal">
                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="importe" HeaderText="Importe">
                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
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
                                <td style="width: 30px"></td>
                                <td>
                                    <asp:Panel runat="server" ID="pnlBtnAdjEvidenvias" Visible="false">

                                        <table>
                                            <tr>
                                                <td style="width: 80px"></td>
                                                <td style="width: 60px; vertical-align: top">
                                                    <asp:ImageButton ID="ibtnAlta" runat="server" ImageUrl="images\Add.png" ToolTip="Alta" Width="20px" />
                                                </td>
                                                <td style="width: 60px; vertical-align: top">
                                                    <asp:UpdatePanel runat="server" UpdateMode="Always">
                                                        <ContentTemplate>
                                                            <asp:ImageButton ID="ibtnBaja" runat="server" ImageUrl="images\Trash.png" ToolTip="Baja" Width="20px" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>

                                    </asp:Panel>

                                    <asp:Panel runat="server" ID="pnlAdjEvidencias" Visible="false">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="text-align: right; vertical-align: top">
                                                            <asp:Label ID="lbl_EvidenciaAgenteAduanal" runat="server" Text="Evidencia: "></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: top; width: 210px">
                                                            <asp:FileUpload ID="fuEvidenciaAgenteAduanal" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="200px" />
                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: left; vertical-align: top">
                                                            <asp:Button ID="btnEvidenciaAgenteAduanal" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Agregar" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                    </asp:Panel>
                                    <asp:UpdatePanel runat="server" ID="upGvEvidencias" Visible="false" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top">
                                                        <asp:GridView ID="gvEvidencias" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="350px" DataKeyNames="id_dt_archivo_adj_comp_anticipo">
                                                            <Columns>
                                                                <asp:HyperLinkField DataNavigateUrlFields="archivo" DataTextField="path" HeaderText="" Visible="false" />
                                                                <asp:BoundField DataField="archivo" HeaderText="Nombre" Visible="false">
                                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="archivo" HeaderText="Nombre">
                                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="uuid_factura" HeaderText="UUID factura">
                                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                    <ItemStyle Width="15px" />
                                                                </asp:CommandField>
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
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="width: 120px"></td>
            <td style="width: 110px"></td>
            <td style="height: 20px">
                <asp:UpdatePanel ID="upValeIng" runat="server" UpdateMode="Conditional" Visible="false">
                    <ContentTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 100px; text-align: right;">
                                    <asp:Label ID="lbl_ValeIng" runat="server" Text="Vale Ingreso:"></asp:Label>
                                </td>
                                <td style="text-align: left; width: 115px;">
                                    <asp:TextBox ID="txtValeIng" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                </td>
                                <td style="text-align: right; width: 110px;">
                                    <asp:Label ID="lbl_ValeIngC" runat="server" Text="Comprobante:"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:FileUpload ID="fuValeIng" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="320px" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <table style="width: 1366px">
        <tr>
            <td style="text-align: center">
                <asp:Button ID="btnAceptarAgenteAduanal" runat="server" Text="Aceptar" Width="170px" Visible="false" />
            </td>
        </tr>
    </table>
    </asp:Panel>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
