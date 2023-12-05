<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ConsCFDI.aspx.vb" Inherits="ProcAd.ConsCFDI" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="width: 1366px; text-align: center; font-weight: bold; color: #FF0000;">
                <ig:WebScriptManager ID="wsmConsFact" runat="server">
                </ig:WebScriptManager>
                 <asp:TextBox ID="_txtIdUsuario" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                 <asp:Literal ID="litError" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat ="server" ID="pnlFiltros"> 
                <table>
                    <tr>
                        <td style="width:80px"></td>
                        <td style="width:120px;text-align:center">
                            <asp:Label runat="server" Text="Filtros..." Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:200px"></td>
                        <td style="width:120px;text-align:left">
                            <asp:CheckBox runat="server" ID="cbCFDI" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="CFDI:"/>
                        </td>
                        <td style="text-align: left; width: 340px;">
                            <asp:Panel ID="pnlCFDI" runat="server">
                                <asp:TextBox ID="txtCFDI" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="245px">
                                </asp:TextBox>
                            </asp:Panel>
                        </td>
                        <td style="width:130px"></td>
                        <td style="width:120px;text-align:left">
                            <asp:CheckBox runat="server" ID="cbSistema" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Sistema:"/>
                        </td>
                        <td style="text-align: left; width: 250px;">
                            <asp:Panel ID="pnlSistema" runat="server">
                                <asp:DropDownList ID="ddlSistema" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                </asp:DropDownList>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:200px"></td>
                        <td style="width:120px;text-align:left">
                            <asp:CheckBox runat="server" ID="cbFechaCreacion" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Fecha emisión:"/>
                        </td>
                        <td style="text-align: left; width: 340px;">
                                <asp:Panel ID="pnlFechaCreacion" runat="server">
                                    <table style="width:260px;">
                                        <tr>
                                            <td style="border: medium inset #808080; margin-right: auto; margin-left: auto;">
                                                <table style="width: 127px;">
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lbl_FechaCI" runat="server" Text="Fecha Inicial"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <ig:WebDatePicker ID="wdpFechaI" runat="server" Font-Names="Verdana" Font-Size="8pt" MinValue="2014-08-06" Nullable="False" Width="110px">
                                                            </ig:WebDatePicker>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width:5px"></td>
                                            <td style="border: medium inset #808080; margin-right: auto; margin-left: auto;">
                                                <table style="width: 127px;">
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lbl_FechaCF" runat="server" Text="Fecha Final"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <ig:WebDatePicker ID="wdpFechaF" runat="server" Font-Names="Verdana" Font-Size="8pt" MinValue="2014-08-06" Nullable="False" Width="110px">
                                                            </ig:WebDatePicker>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        <td style="width:130px"></td>
                        <td style="width:120px;text-align:left">
                            <asp:CheckBox runat="server" ID="cbUsoCFDI" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Uso CFDI:"/>
                        </td>
                        <td style="text-align: left; width: 250px;">
                            <asp:Panel ID="pnlUsoCFDI" runat="server">
                                <asp:DropDownList ID="ddlUsoCFDI" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                </asp:DropDownList>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px"></td>
                        <td style="width: 120px; text-align: left">
                            <asp:CheckBox runat="server" ID="cbRFCEmisor" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="RFC Emisor:" />
                        </td>
                        <td style="text-align: left; width: 340px;">
                            <asp:Panel ID="pnlRFCEmisor" runat="server">
                                <asp:TextBox ID="txtRFCEmisor" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="245px">
                                </asp:TextBox>
                            </asp:Panel>
                        </td>
                        <td style="width:130px"></td>
                        <td style="width: 120px; text-align: left">
                            <asp:CheckBox runat="server" ID="cbRFCReceptor" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="RFC Receptor:" />
                        </td>
                        <td style="text-align: left; width: 250px;">
                            <asp:Panel ID="pnlRFCReceptor" runat="server">
                                <asp:TextBox ID="txtRFCReceptor" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="245px">
                                </asp:TextBox>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:200px"></td>
                        <td style="width: 120px; text-align: left">
                            <asp:CheckBox runat="server" ID="cbVersion" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Versión:" />
                        </td>
                        <td style="text-align: left; width: 340px;">
                            <asp:Panel ID="pnlVersion" runat="server">
                                <asp:DropDownList ID="ddlVersion" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                </asp:DropDownList>
                            </asp:Panel>
                        </td>
                        <td style="width:130px"></td>
                        <%--<td style="width: 120px; text-align: left">
                            <asp:CheckBox runat="server" ID="cbEmpresa" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Empresa:" />
                        </td>
                        <td style="text-align: left; width: 250px;">
                            <asp:Panel ID="pnlEmpresa" runat="server">
                                <asp:DropDownList ID="ddlEmpresa" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                </asp:DropDownList>
                            </asp:Panel>
                        </td>--%>
                    </tr>
                   <%-- <tr>
                        <td style="width:200px"></td>
                        <td style="width: 120px; text-align: left">
                            <asp:CheckBox runat="server" ID="cbRegimenEmisor" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Regimen fiscal del emisor:" />
                        </td>
                        <td style="text-align: left; width: 340px;">
                            <asp:Panel ID="pnlRegimenEmisor" runat="server">
                                <asp:DropDownList ID="ddlRegimenEmisor" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                </asp:DropDownList>
                            </asp:Panel>
                        </td>
                    </tr>--%>
                    </table>
                    <table>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width:600px"></td>
                            <td style="width:130px">
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="130px" Font-Names="Verdana" Font-Size="8pt"/>
                            </td>
                            <td style="width:460px"></td>

                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID ="pnlRegistros">
                    <table>
                        <tr>
                            <td style="width: 5px"></td>
                            <td>
                                <asp:Button ID="btnExportar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Exportar" Width="150px" />
                            </td>
                        </tr>
                    </table> 
                    <table>
                        <tr>
                            <td style="width: 5px"></td>
                            <td>
                                <asp:GridView ID="gvFacturasCFDI" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="2500" DataKeyNames="id_dt_factura_nav_net_procad">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                            <ItemStyle Width="15px" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="CFDI" HeaderText="UUID">
                                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                                        </asp:BoundField>
                                        <%-- <asp:BoundField DataField="Factura" HeaderText=" " Visible ="False" />
                                                            <asp:HyperLinkField DataNavigateUrlFields="path" DataTextField="Factura" HeaderText="Fact.">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                            </asp:HyperLinkField>--%>
                                        <asp:BoundField DataField="fecha_emision" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha emisión">
                                         <ItemStyle HorizontalAlign="Center" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_timbrado" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha timbrado">
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="movimiento" HeaderText="Movimiento">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="rfc_receptor" HeaderText="RFC Receptor">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="razon_receptor" HeaderText="Razon social receptor">
                                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="regimen_receptor" HeaderText="Regimen fiscal receptor">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="uso_cfdi" HeaderText="Uso CFDI">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="rfc_emisor" HeaderText="RFC emisor">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="razon_emisor" HeaderText="Razon social emisor">
                                            <ItemStyle HorizontalAlign="Center" Width="130px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="regimen_emisor" HeaderText="Régimen fiscal emisor">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="estatus" HeaderText="Estatus">
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                            </asp:BoundField>
                                         <asp:BoundField DataField="tipo_comprobante" HeaderText="Tipo comprobante" />
                                        <asp:BoundField DataField="serie" HeaderText="Serie">
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="serie" HeaderText="Serie" />
                                        <asp:BoundField DataField="folio" HeaderText="Folio" />
                                        <asp:BoundField DataField="lugar_expedicion" HeaderText="Lugar Expedición" />
                                        <asp:BoundField DataField="metodo_pago" HeaderText="Método de Pago" />
                                        <asp:BoundField DataField="forma_pago" HeaderText="Forma de Pago" />
                                        <asp:BoundField DataField="tipo_cambio" HeaderText="Tipo de cambio" />
                                        <asp:BoundField DataField="moneda" HeaderText="Moneda" />
                                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                        <asp:BoundField DataField="subtotal" DataFormatString="{0:c}" HeaderText="Subtotal">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="total_tras_IVA" DataFormatString="{0:c}" HeaderText="Total trasladado IVA">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="total_tras_IEPS" DataFormatString="{0:c}" HeaderText="Total trasladado IEPS">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="total_ret_IVA" DataFormatString="{0:c}" HeaderText="Total retenido IVA">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="total_ret_IEPS" DataFormatString="{0:c}" HeaderText="Total retenido IEPS">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="total_ret_ISR" DataFormatString="{0:c}" HeaderText="Total retenido ISR">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                       <%-- <asp:BoundField DataField="total_impuestos_tras" DataFormatString="{0:c}" HeaderText="Total impuestos trasladados">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>--%>
                                        <%--<asp:BoundField DataField="total_impuestos_ret" DataFormatString="{0:c}" HeaderText="Total impuestos retenidos">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>--%>
                                        
                                        <asp:BoundField DataField="descuento" DataFormatString="{0:c}" HeaderText="Descuento">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="importe" DataFormatString="{0:c}" HeaderText="Importe">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_cancelacion" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha cancelación">
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="estado" HeaderText="Estado">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="sistema" HeaderText="Sistema">
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="actividad" HeaderText="Actividad">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
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
                        </tr>
                    </table>
                </asp:Panel>

            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID ="pnlExportar">
                    <table>
                        <tr>
                            <td style="width: 5px"></td>
                            <td>
                                <asp:GridView ID="gvExportar" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="2000" Visible="False">
                                    <Columns>
                                        <asp:BoundField DataField="CFDI" HeaderText="CFDI"></asp:BoundField>
                                        <asp:BoundField DataField="fecha_emision" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha emisión">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_timbrado" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha timbrado">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="movimiento" HeaderText="Movimiento">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="version" HeaderText="Versión">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="rfc_receptor" HeaderText="RFC Receptor">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="razon_receptor" HeaderText="Razon social receptor">
                                            <ItemStyle HorizontalAlign="Center" Width="130px" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="regimen_receptor" HeaderText="Regimen fiscal receptor">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="domicilio_fiscal_receptor" HeaderText="Domicilio fiscal receptor">
                                            <ItemStyle HorizontalAlign="Center" Width="130px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="uso_cfdi" HeaderText="Uso CFDI">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="rfc_emisor" HeaderText="RFC emisor">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="razon_emisor" HeaderText="Razón social emisor">
                                            <ItemStyle HorizontalAlign="Center" Width="130px" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="regimen_emisor" HeaderText="Régimen fiscal emisor">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="pac_certificador" HeaderText="PACCertificador">
                                            <ItemStyle HorizontalAlign="Center" Width="130px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="estatus" HeaderText="Estatus" />
                                        <asp:BoundField DataField="tipo_comprobante" HeaderText="Tipo comprobante" />
                                        <asp:BoundField DataField="serie" HeaderText="Serie" />
                                        <asp:BoundField DataField="folio" HeaderText="Folio" />
                                        <asp:BoundField DataField="lugar_expedicion" HeaderText="Lugar Expedición" />
                                        <asp:BoundField DataField="metodo_pago" HeaderText="Metodo de Pago" />
                                        <asp:BoundField DataField="forma_pago" HeaderText="Forma de Pago" />
                                        <asp:BoundField DataField="condicion_pago" HeaderText="Condiciones de pago" />
                                        <asp:BoundField DataField="tipo_cambio" HeaderText="Tipo de cambio" />
                                        <asp:BoundField DataField="moneda" HeaderText="Moneda" />
                                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                        <asp:BoundField DataField="total_tras_IVA" DataFormatString="{0:c}" HeaderText="Total trasladado IVA">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="total_tras_IEPS" DataFormatString="{0:c}" HeaderText="Total trasladado IEPS">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="total_ret_IVA" DataFormatString="{0:c}" HeaderText="Total retenido IVA">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="total_ret_IEPS" DataFormatString="{0:c}" HeaderText="Total retenido IEPS">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="total_ret_ISR" DataFormatString="{0:c}" HeaderText="Total retenido ISR">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="total_impuestos_tras" DataFormatString="{0:c}" HeaderText="Total impuestos trasladados">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="total_impuestos_ret" DataFormatString="{0:c}" HeaderText="Total impuestos retenidos">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="subtotal" DataFormatString="{0:c}" HeaderText="Subtotal">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descuento" DataFormatString="{0:c}" HeaderText="Descuento">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="importe" DataFormatString="{0:c}" HeaderText="Importe">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_cancelacion" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha cancelación">
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="sistema" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Sistema">
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="estado" HeaderText="Estado">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="actividad" HeaderText="Actividad">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
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
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="pnlConsultaFactura">
                    <table>
                        <tr>
                            <td style="width:40px"></td>
                            <td style="text-align:right">
                                <asp:Label runat="server" ID="lbl_CFDI" Text="CFDI:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width:400px">
                                <asp:Label runat="server" ID="lblCFDI" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="width:5px"></td>
                            <td style="text-align:right">
                                <asp:Label runat="server" ID="lbl_Movimiento" Text="Movimiento:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width:300px">
                                <asp:Label runat="server" ID="lblMovimiento" ForeColor="Blue"></asp:Label>
                            </td>
                             <td style="width:5px"></td>
                            <td style="text-align:right">
                                <asp:Label runat="server" ID="lbl_Version" Text="Versión:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width:150px">
                                <asp:Label runat="server" ID="lblVersion" ForeColor="Blue"></asp:Label>
                            </td>
                             <td style="width:5px"></td>
                            <td style="text-align:right">
                                <asp:Label runat="server" ID="lbl_Sistema" Text="Sistema:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width:200px">
                                <asp:Label runat="server" ID="lblSistema" ForeColor="Blue"></asp:Label>
                            </td>
                             <td style="width:10px"></td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>


                        <tr>
                            <td style="width:40px"></td>
                            <td style="text-align:right">
                                <asp:Label runat="server" ID="lbl_FechaEmision" Text="Fecha emisión:" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblFechaEmision" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="width:5px"></td>
                            <td style="text-align:right">
                                <asp:Label runat="server" ID="lbl_RFCReceptor" Text="RFC Receptor:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width:300px">
                                <asp:Label runat="server" ID="lblRFCReceptor" ForeColor="Blue"></asp:Label>
                            </td>
                             <td style="width:5px"></td>
                            <td style="text-align:right">
                                <asp:Label runat="server" ID="lbl_Serie" Text="Serie:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width:150px">
                                <asp:Label runat="server" ID="lblSerie" ForeColor="Blue"></asp:Label>
                            </td>
                             <td style="width:5px"></td>
                            <td style="text-align:right">
                                <asp:Label runat="server" ID="lbl_Folio" Text="Folio:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width:200px">
                                <asp:Label runat="server" ID="lblFolio" ForeColor="Blue"></asp:Label>
                            </td>
                             <td style="width:10px"></td>
                        </tr>

                         <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width:40px"></td>
                            <td style="text-align:right; width:220px">
                                <asp:Label runat="server" ID="lbl_FechaTimbrado" Text="Fecha timbrado:" Font-Bold="true"></asp:Label>
                            </td>
                            <td> 
                                <asp:Label runat="server" ID="lblFechaTimbrado" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="width:5px"></td>
                            <td style="text-align:right">
                                <asp:Label runat="server" ID="lbl_UsoCFDI" Text="Uso CFDI:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width:300px">
                                <asp:Label runat="server" ID="lblUsoCFDI" ForeColor="Blue"></asp:Label>
                            </td>
                             <td style="width:5px"></td>
                            <td style="text-align:right">
                                <asp:Label runat="server" ID="lbl_Moneda" Text="Moneda:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width:150px">
                                <asp:Label runat="server" ID="lblMoneda" ForeColor="Blue"></asp:Label>
                            </td>
                             <td style="width:5px"></td>
                            <td style="text-align:right">
                                <asp:Label runat="server" ID="lbl_PACCertificador" Text="PACCertificador:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width:200px">
                                <asp:Label runat="server" ID="lnlPACCertificador" ForeColor="Blue"></asp:Label>
                            </td>
                             <td style="width:10px"></td>
                        </tr>
                         <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width:40px"></td>
                            <td style="text-align:right; width:220px">
                                <asp:Label runat="server" ID="lbl_RSReceptor" Text="Razón Social Receptor:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width:300px">
                                <asp:Label runat="server" ID="lblRSReceptor" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="width:5px"></td>
                            <td style="text-align:right; width:220px">
                                <asp:Label runat="server" ID="lbl_RFReceptor" Text="Régimen Fiscal Receptor:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width:300px">
                                <asp:Label runat="server" ID="lblRFReceptor" ForeColor="Blue"></asp:Label>
                            </td>
                             <td style="width:10px"></td>
                            <td style="text-align:right; width:250px">
                                <asp:Label runat="server" ID="lbl_DFReceptor" Text="Domicilio Fiscal Receptor:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width:150px">
                                <asp:Label runat="server" ID="lblDFReceptor" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                          <tr>
                            <td style="width:40px"></td>
                            <td style="text-align:right; width:220px">
                                <asp:Label runat="server" ID="lbl_RSEmisor" Text="Razón Social Emisor:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width:300px">
                                <asp:Label runat="server" ID="lblRSEmisor" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="width:5px"></td>
                            <td style="text-align:right; width:220px">
                                <asp:Label runat="server" ID="lbl_RFEmisor" Text="Régimen Fiscal Emisor:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width:300px">
                                <asp:Label runat="server" ID="lblRFEmisor" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Panel runat="server" ID="pnlGvConsultaFactura">
                    <table>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width:120px"></td>
                            <td style="vertical-align:top">
                                <asp:GridView ID="gvConsultaFactura" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1000">
                                    <Columns>
                                         <asp:BoundField DataField="importe" DataFormatString="{0:c}" HeaderText="Importe">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descuento" DataFormatString="{0:c}" HeaderText="Descuento">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="total_tras_IVA" DataFormatString="{0:c}" HeaderText="Total trasladado IVA">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="total_tras_IEPS" DataFormatString="{0:c}" HeaderText="Total trasladado IEPS">
                                            <ItemStyle HorizontalAlign="Right" Width="110px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="total_ret_IVA" DataFormatString="{0:c}" HeaderText="Total retenido IVA">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="total_ret_IEPS" DataFormatString="{0:c}" HeaderText="Total retenido IEPS">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="total_ret_ISR" DataFormatString="{0:c}" HeaderText="Total retenido ISR">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
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

                            <td style="width:185px"></td>
                            <td style="vertical-align:top">
                                <asp:GridView ID="gvRegistroHistorico" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="500">
                                    <Columns>
                                         <asp:BoundField DataField="fecha" HeaderText="Fecha">
                                             <ItemStyle HorizontalAlign="Justify" Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="historico" HeaderText="Histórico">
                                            <ItemStyle HorizontalAlign="Center" />
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
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
