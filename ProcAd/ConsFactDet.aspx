<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ConsFactDet.aspx.vb" Inherits="ProcAd.ConsFactDet" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style5 {
            width: 142px;
        }
        .auto-style28 {
            width: 110px;
        }
        .auto-style46 {
            width: 483px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; font-weight: bold; color: #FF0000;">
                <ig:WebScriptManager ID="wsmConsComp" runat="server">
                </ig:WebScriptManager>
                <asp:TextBox ID="_txtIdSolicito" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtEmpleado" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtPerfil" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <table style="width: 1366px">
                    <tr>
                        <td>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlFiltros" runat="server" Width="1360px">
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: right; width: 80px;">&nbsp;</td>
                            <td style="text-align: center; " class="auto-style5">
                                <asp:Label ID="lbl_Filtros" runat="server" Font-Bold="True" Text="Filtros..."></asp:Label>
                            </td>
                            <td >&nbsp;</td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: right; width: 80px;">&nbsp;</td>
                            <td style="text-align: left; width: 120px;">
                                <asp:CheckBox ID="cbCFDI" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="CFDI:" />
                            </td>
                            <td style="text-align: left; width: 340px;">
                                <asp:Panel ID="pnlCFDI" runat="server" Width="330px">
                                    <asp:TextBox ID="txtCFDI" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px"></asp:TextBox>
                                </asp:Panel>
                            </td>
                            <td style="text-align: left; width: 120px;">
                                <asp:CheckBox ID="cbFolio" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Folio Factura:" />
                            </td>
                            <td style="width: 240px">
                                <asp:Panel ID="pnlFolio" runat="server" Width="150px">
                                    <asp:TextBox ID="txtFolio" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                </asp:Panel>
                            </td>
                            <td style="width: 120px">
                                <asp:CheckBox ID="cbEmpresa" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Empresa:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlEmpresa" runat="server" Width="220px">
                                    <asp:DropDownList ID="ddlEmpresa" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="200px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">&nbsp;</td>
                            <td style="text-align: left">
                                <asp:CheckBox ID="cbFechaC" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Fecha Emisión:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlFechaC" runat="server">
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
                                                            <ig:WebDatePicker ID="wdpFechaI" runat="server" Font-Names="Verdana" Font-Size="8pt" MinValue="2014-08-06" Nullable="False" Width="120px">
                                                            </ig:WebDatePicker>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="auto-style46">&nbsp;</td>
                                            <td style="border: medium inset #808080; margin-right: auto; margin-left: auto;">
                                                <table style="width: 127px;">
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lbl_FechaCF" runat="server" Text="Fecha Final"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <ig:WebDatePicker ID="wdpFechaF" runat="server" Font-Names="Verdana" Font-Size="8pt" MinValue="2014-08-06" Nullable="False" Width="120px">
                                                            </ig:WebDatePicker>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                    <table style="width: 100%; text-align: center; margin-right: auto; margin-left: auto; height: 60px;">
                        <tr>
                            <td class="auto-style51">
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="150px" Font-Names="Verdana" Font-Size="8pt" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlRegistros" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td class="auto-style55">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width: 1px">&nbsp;</td>
                                        <td>
                                            <asp:GridView ID="gvRegistros" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1335px">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                    <ItemStyle Width="15px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="ID" HeaderText="ID">
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_carga" HeaderText="Fecha Carga">
                                                    <ItemStyle Width="85px" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_emision" HeaderText="Fecha Emisión">
                                                    <ItemStyle Width="85px" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="uuid" HeaderText="CFDI" >
                                                    </asp:BoundField>
                                                    <asp:HyperLinkField DataNavigateUrlFields="path" DataTextField="PDF" HeaderText="">
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                    </asp:HyperLinkField>
                                                    <asp:BoundField DataField="folio_factura" HeaderText="Folio Factura" >
                                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="razon_emisor" HeaderText="Razon Emisor">
                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="razon_receptor" HeaderText="Razón Receptor">
                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="forma_pago" HeaderText="Forma Pago">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="importe" HeaderText="Importe" DataFormatString="{0:c}">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="status" HeaderText="Estatus">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
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
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlDetalle" runat="server">
                    <table style="width:1366px;">
                        <tr>
                            <td style="text-align: center; width: 300px; height: 35px;">
                                <asp:Button ID="btnNueBusProd" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Nueva Búsqueda" Width="150px" />
                            </td>
                            <td style="text-align: center; width: 460px; height: 35px;">
                                &nbsp;</td>
                            <td style="text-align: center; width: 210px; height: 35px;">
                                &nbsp;</td>
                            <td style="text-align: right">
                                &nbsp;</td>
                            <td class="auto-style28">
                                &nbsp;</td>
                        </tr>
                    </table>
                    <table style="width:1366px;">
                        <tr>
                            <td style="width: 15px; height: 30px; text-align: right">&nbsp;</td>
                            <td>
                                <asp:Label ID="lbl_FacturaCab" runat="server" Font-Bold="True" Text="Factura"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15px; height: 30px; text-align: right">&nbsp;</td>
                            <td>
                                <asp:GridView ID="gvFacturaC" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1335px">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_carga" HeaderText="Fecha Carga">
                                        <ItemStyle HorizontalAlign="Center" Width="85px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_emision" HeaderText="Fecha Emisión">
                                        <ItemStyle HorizontalAlign="Center" Width="85px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="uuid" HeaderText="CFDI" />
                                        <asp:HyperLinkField DataNavigateUrlFields="path" DataTextField="PDF" HeaderText="">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                        </asp:HyperLinkField>
                                        <asp:BoundField DataField="folio_factura" HeaderText="Folio Factura">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="razon_emisor" HeaderText="Razon Emisor">
                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="razon_receptor" HeaderText="Razón Receptor">
                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="forma_pago" HeaderText="Forma Pago">
                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="importe" DataFormatString="{0:c}" HeaderText="Importe">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Moneda" HeaderText="Moneda">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="status" HeaderText="Estatus">
                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
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
                        <tr>
                            <td style="width: 15px; height: 30px; text-align: right">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 15px; ">&nbsp;</td>
                            <td>
                                <asp:Label ID="lbl_FacturaLin" runat="server" Font-Bold="True" Text="Líneas de la Factura"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15px; height: 30px; text-align: right">&nbsp;</td>
                            <td>
                                <asp:GridView ID="gvFacturaL" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1750px">
                                    <Columns>
                                        <asp:BoundField DataField="no_" HeaderText="No.">
                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="no_comp" HeaderText="No. Comp.">
                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_carga" HeaderText="Fecha Carga">
                                        <ItemStyle HorizontalAlign="Center" Width="85px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cantidad" DataFormatString="{0:N}" HeaderText="Cantidad">
                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                        <asp:BoundField DataField="valor_unitario" DataFormatString="{0:c}" HeaderText="Valor Unitario">
                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="importe" DataFormatString="{0:c}" HeaderText="Importe">
                                        <ItemStyle HorizontalAlign="Right" Width="130px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descuento" DataFormatString="{0:c}" HeaderText="Descuento">
                                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tot_tras_iva" DataFormatString="{0:c}" HeaderText="Tot Tras IVA">
                                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tot_tras_ieps" DataFormatString="{0:c}" HeaderText="Tot Tras IEPS">
                                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tot_ret_iva" DataFormatString="{0:c}" HeaderText="Tot Ret IVA">
                                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tot_ret_ieps" DataFormatString="{0:c}" HeaderText="Tot Ret IEPS">
                                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tot_ret_isr" DataFormatString="{0:c}" HeaderText="Tot Ret ISR">
                                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_tras_1" HeaderText="Tipo Tras 1">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tasa_tras_1" DataFormatString="{0:#0.00}" HeaderText="Tasa Tras 1">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_ret_1" HeaderText="Tipo Ret 1">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tasa_ret_1" DataFormatString="{0:#0.00}" HeaderText="Tasa Ret 1">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_tras_2" HeaderText="Tipo Tras 2">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tasa_tras_2" DataFormatString="{0:#0.00}" HeaderText="Tasa Tras 2">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_ret_2" HeaderText="Tipo Ret 2">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tasa_ret_2" DataFormatString="{0:#0.00}" HeaderText="Tasa Ret 2">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
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
                        <tr>
                            <td style="width: 15px; height: 30px; text-align: right">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 15px; ">&nbsp;</td>
                            <td>
                                <asp:Label ID="lbl_FacturaComp" runat="server" Font-Bold="True" Text="Comprobación"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15px; height: 30px; text-align: right">&nbsp;</td>
                            <td>
                                <asp:GridView ID="gvComp" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1200px">
                                    <Columns>
                                        <asp:BoundField DataField="id_ms_comp" HeaderText="Folio">
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="empleado" HeaderText="Empleado">
                                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="autorizador" HeaderText="Autorizador">
                                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="director" HeaderText="Director">
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="periodo_comp" HeaderText="Periodo Comp">
                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="estatus" HeaderText="Estatus">
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
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
                        <tr>
                            <td style="width: 15px; height: 30px; text-align: right">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 15px; ">&nbsp;</td>
                            <td>
                                <asp:Label ID="lbl_FacturaIng" runat="server" Font-Bold="True" Text="Ingreso de Factura"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15px; height: 30px; text-align: right">&nbsp;</td>
                            <td>
                                <asp:GridView ID="gvFact" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1200px">
                                    <Columns>
                                        <asp:BoundField DataField="no_factura" HeaderText="Folio">
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="centro_costo" HeaderText="Centro de Costo">
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="empleado" HeaderText="Empleado">
                                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="autorizador" HeaderText="Autorizador">
                                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_solicita" HeaderText="Fecha Solicitó">
                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="estatus" HeaderText="Estatus">
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
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