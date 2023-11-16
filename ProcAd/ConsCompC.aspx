<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ConsCompC.aspx.vb" Inherits="ProcAd.ConsCompC" %>
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
        .auto-style47 {
            width: 1544px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; font-weight: bold; color: #FF0000;">
                <ig:WebScriptManager ID="wsmConsCompC" runat="server">
                </ig:WebScriptManager>
                <table style="width: 1350px;">
                    <tr>
                        <td>
                <asp:TextBox ID="_txtIdSolicito" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtEmpleado" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtPerfil" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlFiltros" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: right; width: 80px;">&nbsp;</td>
                            <td style="text-align: center; " class="auto-style5">
                                <asp:Label ID="lbl_Filtros" runat="server" Font-Bold="True" Text="Filtros..."></asp:Label>
                            </td>
                            <td >&nbsp;</td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlSolicitó" runat="server">
                        <table style="width:100%;">
                            <tr>
                                <td style="text-align: right; width: 80px;">&nbsp;</td>
                                <td style="text-align: left; width: 120px;">
                                    <asp:CheckBox ID="cbSolicitante" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Solicitante:" />
                                </td>
                                <td style="text-align: left; width: 340px;">
                                    <asp:Panel ID="pnlSolicitante" runat="server" Width="330px">
                                        <asp:DropDownList ID="ddlSolicitante" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px">
                                        </asp:DropDownList>
                                    </asp:Panel>
                                </td>
                                <td style="text-align: left; width: 120px;">
                                    <asp:CheckBox ID="cbEmpresa" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Empresa:" />
                                </td>
                                <td>
                                    <asp:Panel ID="pnlEmpresa" runat="server" Width="330px">
                                        <asp:DropDownList ID="ddlEmpresa" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="200px">
                                        </asp:DropDownList>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: right; width: 80px;">&nbsp;</td>
                            <td style="text-align: left; width: 120px;">
                                <asp:CheckBox ID="cbFechaC" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Fecha Creación:" />
                            </td>
                            <td style="text-align: left; width: 340px;">
                                <asp:Panel ID="pnlFechaC" runat="server" Width="330px">
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
                            <td style="text-align: left; width: 120px;">
                                <asp:CheckBox ID="cbAutorizador" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Autorizador:" />
                            </td>
                            <td style="width: 340px">
                                <asp:Panel ID="pnlAutorizador" runat="server" Width="330px">
                                    <asp:DropDownList ID="ddlAutorizador" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td style="width: 120px">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">&nbsp;</td>
                            <td style="text-align: left">
                                <asp:CheckBox ID="cbValeA" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Vale Azul" />
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:CheckBox ID="cbPagoEfec" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Pago Efectivo.:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlPagoEfec" runat="server" Width="330px">
                                    <asp:DropDownList ID="ddlPagoEfec" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="50px">
                                        <asp:ListItem Value="S">Sí</asp:ListItem>
                                        <asp:ListItem Value="N">No</asp:ListItem>
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:CheckBox ID="cbNoComp" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="No. de Comp.:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlNoComp" runat="server" Width="330px">
                                    <asp:TextBox ID="txtNoComp" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; text-align: center; margin-right: auto; margin-left: auto; height: 60px;">
                        <tr>
                            <td style="width: 1366px">
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="150px" Font-Names="Verdana" Font-Size="8pt" />
                            </td>
                            <td>&nbsp;</td>
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
                                            <asp:GridView ID="gvRegistros" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="2000px">
                                                <Columns>
                                                    <asp:BoundField DataField="id_ms_comp" HeaderText="No. Comp">
                                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="importe_tot" HeaderText="Saldo Comp" DataFormatString="{0:c}">
                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="pago_efectivo" HeaderText="Pago Efectivo" >
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_efectivo" HeaderText="Fecha Pago Efectivo">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_realizo" HeaderText="Fecha Realizó" DataFormatString="{0:dd/MM/yyyy}">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Factura" HeaderText="Factura">
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Tabulador" HeaderText="Tabulador">
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nombre_concepto" HeaderText="Concepto">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="no_personas" HeaderText="No. Personas">
                                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="no_dias" HeaderText="No. Días">
                                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="monto_subtotal" HeaderText="Subtotal" DataFormatString="{0:c}">
                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="monto_iva" HeaderText="IVA" DataFormatString="{0:c}">
                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="monto_total" DataFormatString="{0:c}" HeaderText="Total">
                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="rfc" HeaderText="RFC">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="proveedor" HeaderText="Proveedor">
                                                    <ItemStyle HorizontalAlign="Left" Width="180px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="no_factura" HeaderText="No. Factura">
                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="origen_destino" HeaderText="Orig-Dest">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="vehiculo" HeaderText="Vehículo">
                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="obs" HeaderText="Observaciones" />
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
            </td>
        </tr>
        </table>
</asp:Content>