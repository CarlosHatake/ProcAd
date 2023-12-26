<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ComprobarAnticipo.aspx.vb" Inherits="ProcAd.ComprobarAnticipo" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                <ig:WebScriptManager ID="wsm43" runat="server">
</ig:WebScriptManager>
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td>
                <asp:Panel ID="pnlInicio" runat="server">
                    <table style="width: 1266px; height: 25px; font-family: Verdana; font-size: 8pt;">
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

                    <asp:Panel ID="pnlAnticipoSinPedidoCompra" runat="server" Visible="false">
                        <asp:Panel ID="pnlSeccion1SinPedidoCompra" runat="server" Visible="false">
                            <table style="width: 1366px; height:40px">
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblSeccion" runat="server" Text="SECCIÓN 1"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 1366px; height: 44px;">

                                <tr>
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
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lbl_NumProveedor" runat="server" Text="Numero del Proveedor (NAV):"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNumProveedor" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 105px"></td>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCFDI" runat="server" Text="CFDI ANTICIPO:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:GridView ID="gvCFDIAnticipo" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px">
                                                        <Columns>
                                                            <asp:BoundField DataField="id" HeaderText="idCFDI" />
                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
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
                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" Width="200px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlSeccion2SinPedidoCompra" runat="server" Visible="false">
                            <table style="width:1366px; height:44px;">
                                <tr>
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
                                        <asp:DropDownList ID="ddlCentroCosto" runat="server" Width="190px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width:250px">

                                                </td>
                                                <td style="text-align:center">
                                                    <asp:RadioButtonList runat="server" ID="rbTipoOperacion" Width="250px" RepeatColumns="4">
                                                        <asp:ListItem>Administrativo</asp:ListItem>
                                                        <asp:ListItem>Operativo</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                      
                                    </td>
                                    <td style="text-align:right">
                                        <asp:Label ID="lblDivision" runat="server" Text="División:"></asp:Label>                                        
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlDivision" runat="server" Width="190px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td style="text-align:center">
                                        <table style="width:100%">
                                            <tr>
                                                <td style="width:250px"></td>
                                                <td>
                                                     <asp:Label ID="lblSeccion2" runat="server" Text="SECCIÓN 2"></asp:Label>                                        
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <table style="width:100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCFDIAnticipo" runat="server" Text="CFDI ANTICIPO:"></asp:Label>                                        
                                                </td>
                                                <td>
                                                    <asp:GridView ID="gvCFDIAnticipoSeccion2" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px">
                                                        <Columns>
                                                            <asp:BoundField DataField="id" HeaderText="idCFDI" />
                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
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
                                                    <asp:Label ID="lblCFDIFactura" runat="server" Text="CFDI FACTURA:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:GridView ID="gvCFDIFactura" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px">
                                                        <Columns>
                                                            <asp:BoundField DataField="id" HeaderText="idCFDI" />
                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
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
                                                    <asp:Label ID="lblCFDIEgreso" runat="server" Text="CFDI EGRESO:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:GridView ID="gvCFDIEgreso" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px">
                                                        <Columns>
                                                            <asp:BoundField DataField="id" HeaderText="idCFDI" />
                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
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
                                        <table style="width:100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="pnl_Adjunto" runat="server" Text="Archivo Adjunto"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:FileUpload ID="fuAdjunto" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table style="width:1266px">
                                <tr>
                                    <td style="text-align:center">
                                        <asp:Button ID="btnAutorizacion" runat="server" Text="Solicitar Autorización" Width="180px"/>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:Panel>

                    <asp:Panel ID="pnlAnticipoConPedidoCompra" runat="server" visible="false">
                        <asp:Panel ID="pnlSeccion1ConPedidoCompra" runat="server">
                            <table style="width: 1366px; height: 40px">
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Label ID="lbl_Seccion1ConPedidoCompra" runat="server" Text="SECCIÓN 1"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 1366px; height: 44px;">
                                <tr>
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
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lbl_NumProveedorConPedidoCompra" runat="server" Text="Numero del Proveedor (NAV):"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNumProveedorConPedidoCompra" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 105px"></td>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_CFDIAnticipo" runat="server" Text="CFDI ANTICIPO:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:GridView ID="gvCFDIAnticipoConPedidoCompra" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px">
                                                        <Columns>
                                                            <asp:BoundField DataField="id" HeaderText="idCFDI" />
                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
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
                                        <asp:Button ID="btnGuardarConPedidoCompra" runat="server" Text="Guardar" Width="200px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlSeccion2ConPedidoCompra" runat="server">
                            <table style="width: 1366px; height: 44px;">
                                <tr>
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
                                        <asp:DropDownList ID="ddlCentroCostoConPedidoCompra" runat="server" Width="190px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width: 250px"></td>
                                                <td style="text-align: center">
                                                    <asp:RadioButtonList runat="server" ID="rbTipoOperacionConPedidoCompra" Width="250px" RepeatColumns="4">
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
                                        <asp:DropDownList ID="ddlDivisionConPedidoCompra" runat="server" Width="190px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td style="text-align: center">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 250px"></td>
                                                <td>
                                                    <asp:Label ID="lbl_Seccion2" runat="server" Text="SECCIÓN 2"></asp:Label>
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
                                                    <asp:GridView ID="gvAnticipoConPedidoCompra" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px">
                                                        <Columns>
                                                            <asp:BoundField DataField="id" HeaderText="idCFDI" />
                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
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
                                                    <asp:GridView ID="gvCFDIFacturaConPedidoCompra" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px">
                                                        <Columns>
                                                            <asp:BoundField DataField="id" HeaderText="idCFDI" />
                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
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
                                                    <asp:GridView ID="gvCFDIEgresoConPedidoCompra" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px">
                                                        <Columns>
                                                            <asp:BoundField DataField="id" HeaderText="idCFDI" />
                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
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
                                        <asp:Button ID="btnSolicitarAutoriacion" runat="server" Text="Solicitar Autorización" Width="180px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:Panel>

                    <asp:Panel ID="pnlPagoAnticipadoSinPedidosCompra" runat="server" Visible="false">
                        <table style="width: 1366px; height: 44px;">
                            <tr>
                                <td style="text-align:right; width:110px">
                                    <asp:Label ID="lbl_Solicitante" runat="server" Text="Solicitante: "></asp:Label>
                                </td>
                                <td style="width:200px">
                                    <asp:Label ID="lblSolicitante" runat="server"></asp:Label>
                                </td>

                                <td style="text-align:right; width:50px">
                                    <asp:label ID="lbl_EmpresaPagoAnticipado" runat="server" Text="Empresa: "></asp:label>
                                </td>
                                <td style="width:200px">
                                    <asp:Panel ID="lblEmpresaPagoAnticipado" runat="server"></asp:Panel>
                                </td>

                                <td style="text-align:right; width:70px">
                                    <asp:label ID="lbl_CentroCostoPagoAnticipado" runat="server" Text="Centro de Costo:"></asp:label>
                                </td>
                                <td style="width:200px">
                                    <asp:Label ID="lblCentroCostoPagoAnticipado" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td style="text-align:right">
                                    <asp:Label ID="lbl_Autorizador" runat="server" Text="Autorizador: "></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlAutorizador" runat="server" Width="180px"></asp:DropDownList>
                                </td>
                                <td style="text-align:right">
                                    <asp:Label ID="lbl_ProveedorPagoAnticipado" runat="server" Text="Proveedor:"></asp:Label>
                                </td>
                                <td>
                                    <asp:label ID="lblProveedorPagoAnticipado" runat="server"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td style="text-align:right">
                                    <asp:Label ID="lbl_SegundoAutorizador" runat="server" Text="Segundo Autorizador:"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSegundoAutorizador" runat="server"></asp:DropDownList>
                                </td>
                                <td style="text-align:right">
                                    <asp:Label ID="lbl_CentroCostoPagoAnticipado2" runat="server" Text="Centro de Costo"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCentroCostoPagoAnticipado" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:RadioButtonList ID="rbOpcionesPagoAnticipado" runat="server">
                                        <asp:ListItem>Administrativo</asp:ListItem>
                                        <asp:ListItem>Operativo</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                        <table style="width:100%">
                            <tr>
                                <td style="width:110px; text-align:right">
                                    <asp:Label ID="lbl_Factura" runat="server" Text="Factura: "></asp:Label>
                                </td>
                                <td>
                                    <asp:GridView ID="gvFactura" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px">
                                        <Columns>
                                            <asp:BoundField DataField="id" HeaderText="idCFDI" />
                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
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
                        <table style="width:100%">
                            <tr>
                                <td style="width: 110px; text-align: right">
                                    <asp:Label ID="lbl_Evidencia" runat="server" Text="Evidencia: "></asp:Label>
                                </td>
                                <td>
                                    <asp:FileUpload ID="fuEvidenciaPagoAnticipado" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px" />
                                </td>
                                <td>
                                    <asp:Button ID="btnAgregarPagoAnticipado" runat="server" Width="100px"/>
                                </td>
                            </tr>
                        </table>
                        <table style="width:100%">
                            <tr>
                                <td style="text-align:center">
                                    <asp:Button ID="btnGuardarPagoAnticipado" runat="server" Width="100px"/>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                    <asp:Panel ID="pnlPagoAnticipadoConPedidoCompra" runat="server" Visible="false">
                        <table style="width:1266px; height:44px">
                            <tr>
                                <td style="width:50px">

                                </td>
                                <td>
                                    <asp:Label ID="lbl_PagoAnticipado" runat="server" Text="Lista de validación de pagos anticipados: "></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>

                                </td>
                                <td>
                                    <asp:GridView ID="gvPagoAnticipadoConPedidosCompra" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px">
                                        <Columns>
                                            <asp:BoundField DataField="id" HeaderText="idCFDI" />
                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                <ItemStyle Width="15px" />
                                            </asp:CommandField>
                                            <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fechaPagoAnticipado" HeaderText="Fecha de anticipado">
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="proveedor" HeaderText="Proveedor">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="importe" HeaderText="Importe">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="pedidosCompra" HeaderText="Pedidos de compra">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="usuario" HeaderText="Usuario">
                                                <ItemStyle HorizontalAlign="Center" Width="40px" />
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
                        <table style="width:1266px; height:44px">
                            <tr>
                                <td style="text-align:center">
                                    <asp:Button ID="btnGuardarPagoAnticipadoConPedidoCompra" runat="server" Text="Guardar" Width="270px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                    <asp:Panel ID="pnlPagoAnticipadoAgenteAduanal" runat="server">
                        <table style="width: 1366px; height: 40px">
                            <tr>
                                <td style="text-align: right; width: 130px">
                                    <asp:Label ID="lbl_EmpresaPagoAnticipadoAgenteAduanal" runat="server" Text="Empresa:"></asp:Label>
                                </td>
                                <td style="width: 550px">
                                    <asp:DropDownList ID="ddlEmpresaAgenteAduanal" runat="server" Width="170px"></asp:DropDownList>
                                </td>

                                <td style="text-align: right; width: 170px">
                                    <asp:Label ID="lbl_CentroCostoAgenteAduanal" runat="server" Text="Centro de Costo:"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCentroCostoAgenteAduanal" runat="server" Width="170px"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:RadioButtonList ID="rbTipoAgenteAduanal" runat="server">
                                        <asp:ListItem>Administrativo</asp:ListItem>
                                        <asp:ListItem>Operativo</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_DivisionAgenteAduanal" runat="server" Text="División: "></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDivisionAgenteAduanal" runat="server" Width="170px"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label ID="lbl_ProveedorAgenteAduanal" runat="server" Text="Proveedor: "></asp:Label>
                                </td>
                                <td>
                                    <table style="width:100%">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtProveedorAgenteAduanal" runat="server" Width="160px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbAgenteAduanal" runat="server" ImageUrl="images\icn_search.png"/>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:DropDownList ID="ddlProveedorAgenteAduanal" runat="server" Width="180px"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:right">
                                    <asp:Label ID="lbl_EvidenciaAgenteAduanal" runat="server" Text="Evidencia: "></asp:Label>
                                </td>
                                <td>
                                    <asp:FileUpload ID="fuEvidenciaAgenteAduanal" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px" />
                                </td>
                                <td style="text-align:left">
                                    <asp:Button Id="btnEvidenciaAgenteAduanal" runat="server" Text="Agregar"/>
                                </td>
                                <td>
                                    <asp:GridView ID="gvEvidencias" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="200px">
                                        <Columns>
                                            <asp:HyperLinkField DataNavigateUrlFields="ruta" DataTextField="nombre" HeaderText="Nombre" />
                                            <asp:BoundField DataField="nombre" HeaderText="Nombre" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nombre_archivo" HeaderText="nombre_archivo">
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ruta_archivo" HeaderText="ruta_archivo">
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            </asp:BoundField>
                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\Trash15.png" ShowSelectButton="True">
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
                        <table style="width:1366px; height:40px">
                            <tr>
                                <td style="width:70px; text-align:right">
                                    <asp:Label ID="lbl_CFDIAnticipoAgenteAduanal" runat="server" Text="CFDI ANTICIPO:"></asp:Label>
                                </td>
                                <td>
                                    <asp:GridView ID="gvCFDIAgenteAduanal" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px">
                                        <Columns>
                                            <asp:BoundField DataField="id" HeaderText="idCFDI" />
                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
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
                            <tr>
                                <td style="text-align:right">
                                    <asp:Label ID="lbl_CFDIFacturaAgenteAduanal" runat="server" Text="CFDI FACTURA:"></asp:Label>
                                </td>
                                <td>
                                    <asp:GridView ID="gvCFDIFacturaAgenteAduanal" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px">
                                        <Columns>
                                            <asp:BoundField DataField="id" HeaderText="idCFDI" />
                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
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
                            <tr>
                                <td style="text-align:right">
                                    <asp:Label ID="lbl_CFDIEgresoAgenteAduanal" runat="server" Text="CFDI EGRESO:"></asp:Label>
                                </td>
                                <td>
                                    <asp:GridView ID="gvCFDIEgresoAgenteAduanal" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px">
                                        <Columns>
                                            <asp:BoundField DataField="id" HeaderText="idCFDI" />
                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
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
                        <table style="width:1366px">
                            <tr>
                                <td style="text-align:center">
                                    <asp:Button ID="btnAceptarAgenteAduanal" runat="server" Text="Aceptar" Width="170px"/>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
