<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ConsFactC.aspx.vb" Inherits="ProcAd.ConsFactC" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style5 {
            width: 142px;
        }
        .auto-style46 {
            width: 483px;
        }
        .auto-style28 {
            width: 110px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; font-weight: bold; color: #FF0000;">
                <ig:WebScriptManager ID="wsmConsFactI" runat="server">
                </ig:WebScriptManager>
                <asp:TextBox ID="_txtIdSolicito" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtEmpleado" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtPerfil" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlFiltros" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: right; width: 180px;">&nbsp;</td>
                            <td style="text-align: center; " class="auto-style5">
                                <asp:Label ID="lbl_Filtros" runat="server" Font-Bold="True" Text="Filtros..."></asp:Label>
                            </td>
                            <td >&nbsp;</td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlSolicitó" runat="server">
                        <table style="width:100%;">
                            <tr>
                                <td style="text-align: right; width: 180px;">&nbsp;</td>
                                <td style="text-align: left; width: 130px;">
                                    <asp:CheckBox ID="cbEmpresa" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Empresa:" />
                                </td>
                                <td style="text-align: left; width: 360px;">
                                    <asp:Panel ID="pnlEmpresa" runat="server" Width="350px">
                                        <asp:DropDownList ID="ddlEmpresa" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="200px">
                                        </asp:DropDownList>
                                    </asp:Panel>
                                </td>
                                <td style="text-align: left; width: 130px;">
                                    <asp:CheckBox ID="cbSolicitante" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Solicitante:" />
                                </td>
                                <td>
                                    <asp:Panel ID="pnlSolicitante" runat="server" Width="350px">
                                        <asp:DropDownList ID="ddlSolicitante" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px">
                                        </asp:DropDownList>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: right; width: 180px;">&nbsp;</td>
                            <td style="text-align: left; width: 130px;">
                                <asp:CheckBox ID="cbFechaC" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Fecha Creación:" />
                            </td>
                            <td style="text-align: left; width: 360px;">
                                <asp:Panel ID="pnlFechaC" runat="server" Width="350px">
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
                            <td style="text-align: left; width: 130px;">
                                <asp:CheckBox ID="cbAutorizador" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Autorizador:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlAutorizador" runat="server" Width="350px">
                                    <asp:DropDownList ID="ddlAutorizador" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">&nbsp;</td>
                            <td style="text-align: left">
                                <asp:CheckBox ID="cbProveedor" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Proveedor:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlProveedor" runat="server" Width="350px">
                                    <asp:TextBox ID="txtProveedor" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="175px"></asp:TextBox>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:CheckBox ID="cbNoFact" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Folio:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlNoFact" runat="server" Width="350px">
                                    <asp:TextBox ID="txtNoFact" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                </asp:Panel>
                            </td>
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
                                        <td style="width: 10px">&nbsp;</td>
                                        <td>
                                            <asp:GridView ID="gvRegistros" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1325px">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                    <ItemStyle Width="15px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="No. Solicitud" HeaderText="Folio">
                                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Razón Social" HeaderText="Razón Social"></asp:BoundField>
                                                    <asp:BoundField DataField="Descripción" HeaderText="Descripción">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Fecha Emisión" HeaderText="Fecha Emisión">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Fecha Solicitud usuario" HeaderText="Fecha Solicitud usuario">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Fecha Asignación Cuenta" HeaderText="Fecha Asignación Cuenta">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Folio Externo" HeaderText="Folio Externo">
                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CFDI" HeaderText="CFDI">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="subtotal" DataFormatString="{0:c}" HeaderText="Subtotal">
                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Impuestos Locales" DataFormatString="{0:c}" HeaderText="Impuestos Locales">
                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Importe Total" DataFormatString="{0:c}" HeaderText="Importe Total">
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
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlDetalle" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: center; width: 300px; height: 35px;">
                                <asp:Button ID="btnNueBusProd" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Nueva Búsqueda" Width="150px" />
                            </td>
                            <td style="text-align: center; width: 460px; height: 35px;">
                                &nbsp;</td>
                            <td style="text-align: center; width: 210px; height: 35px;">
                                &nbsp;</td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Folio" runat="server" Text="Folio:"></asp:Label>
                            </td>
                            <td class="auto-style28">
                                <asp:Label ID="lblFolio" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1366px; height: 44px;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_RazonS" runat="server" Text="Razón Social:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblRazonS" runat="server" ForeColor="Blue" Width="510px"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 120px; height: 21px;">
                                <asp:Label ID="lbl_FechaAsig" runat="server" Text="Fecha Asig. Cuenta:"></asp:Label>
                            </td>
                            <td style="width: 180px;">
                                <asp:Label ID="lblFechaAsig" runat="server" ForeColor="Blue" Width="170px"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 90px; height: 21px;">
                                <asp:Label ID="lbl_FolioE" runat="server" Text="Folio Externo:"></asp:Label>
                            </td>
                            <td style="width: 300px;">
                                <asp:Label ID="lblFolioE" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Descrip" runat="server" Text="Descripción:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDescrip" runat="server" ForeColor="Blue" Width="510px"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_FechaEmi" runat="server" Text="Fecha Emisión:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFechaEmi" runat="server" ForeColor="Blue" Width="170px"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_CFDI" runat="server" Text="CFDI:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCFDI" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1366px; height: 22px;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_PDF" runat="server" Text="Documento:"></asp:Label>
                            </td>
                            <td>
                                <asp:HyperLink ID="hlPDF" runat="server">PDF</asp:HyperLink>
                            </td>
                            <td style="text-align: right; width: 120px; height: 21px;">
                                <asp:Label ID="lbl_Subtotal" runat="server" Text="Subtotal:"></asp:Label>
                            </td>
                            <td style="width: 180px; ">
                                <asp:Label ID="lblSubtotal" runat="server" ForeColor="Blue" Width="195px"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 120px; height: 21px;">
                                <asp:Label ID="lbl_ImpLoc" runat="server" Text="Impuestos Locales:"></asp:Label>
                            </td>
                            <td style="width: 180px;">
                                <asp:Label ID="lblImpLoc" runat="server" ForeColor="Blue" Width="170px"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 90px; height: 21px;">
                                <asp:Label ID="lbl_Total" runat="server" Text="Total:"></asp:Label>
                            </td>
                            <td style="width: 300px;">
                                <asp:Label ID="lblTotal" runat="server" ForeColor="Blue" Width="170px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td class="auto-style5" style="text-align: right; vertical-align: top;">
                                <asp:Label ID="lbl_Adjunto" runat="server" Text="Adjuntos:"></asp:Label>
                            </td>
                            <td style="width: 350px; vertical-align: top;">
                                <asp:GridView ID="gvAdjuntos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ShowHeader="False" Width="340px">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <Columns>
                                        <asp:HyperLinkField DataNavigateUrlFields="path" DataTextField="archivo" HeaderText="Archivo">
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
                            </td>
                            <td style="width: 100px; vertical-align: top; text-align: right;">
                                <asp:Label ID="lbl_Evidencia" runat="server" Text="Evidencias:"></asp:Label>
                            </td>
                            <td style="width: 350px; vertical-align: top;">
                                <asp:GridView ID="gvEvidencias" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ShowHeader="False" Width="340px">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <Columns>
                                        <asp:HyperLinkField DataNavigateUrlFields="path" DataTextField="archivo" HeaderText="Archivo">
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
                            </td>
                            <td style="width: 98px; text-align: right; vertical-align: top;">
                                <asp:Label ID="lbl_AF" runat="server" Text="AF:"></asp:Label>
                            </td>
                            <td style="vertical-align: top">
                                <asp:GridView ID="gvAF" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="280px">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <Columns>
                                        <asp:BoundField DataField="no_economico" HeaderText="Económico">
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
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
                    <table style="width: 1366px; height: 44px;">
                        <tr>
                            <td style="text-align: right; width: 275px;">&nbsp;</td>
                            <td>
                                <asp:GridView ID="gvFactura" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="800px">
                                    <Columns>
                                        <asp:BoundField DataField="Cuenta Contable" HeaderText="Cuenta Contable">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Centro de Costo" HeaderText="Centro de Costo">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="División" HeaderText="División">
                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Zona" HeaderText="Zona">
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Porcentaje Partida" HeaderText="Porcentaje Partida" DataFormatString="{0:###.##################}">
                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Importe Partida" HeaderText="Subtotal" DataFormatString="{0:c}">
                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Impuestos Locales" DataFormatString="{0:c}" HeaderText="Impuestos Locales ">
                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Divisa" HeaderText="Divisa">
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
                    </table>
                    <table style="width: 100%; text-align: center; margin-right: auto; margin-left: auto; height: 60px;">
                        <tr>
                            <td class="auto-style51">
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        </table>
</asp:Content>
