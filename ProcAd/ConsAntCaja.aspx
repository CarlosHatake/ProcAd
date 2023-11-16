<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ConsAntCaja.aspx.vb" Inherits="ProcAd.ConsAntCaja" %>
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
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; font-weight: bold; color: #FF0000;">
                <ig:WebScriptManager ID="wsmConsAntCaja" runat="server">
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
                    <table style="width:1700px;">
                        <tr>
                            <td style="text-align: right; width: 80px;">&nbsp;</td>
                            <td style="text-align: left; " class="auto-style5">
                                <asp:Label ID="lbl_FiltrosA" runat="server" Font-Bold="True" Text="Filtros Anticipos..."></asp:Label>
                            </td>
                            <td style="width: 695px" >&nbsp;</td>
                            <td>
                                <asp:Label ID="lbl_FiltrosC" runat="server" Font-Bold="True" Text="Filtros Comprobaciones..."></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlSolicitó" runat="server">
                        <table style="width:1700 px;">
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
                                <td style="width: 250px">
                                    <asp:Panel ID="pnlEmpresa" runat="server" Width="210px">
                                        <asp:DropDownList ID="ddlEmpresa" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="200px">
                                        </asp:DropDownList>
                                    </asp:Panel>
                                </td>
                                <td style="width: 120px">
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
                            </tr>
                        </table>
                    </asp:Panel>
                    <table style="width:1700px;">
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
                                <asp:CheckBox ID="cbNoAnticipo" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="No. de Anticipo:" />
                            </td>
                            <td style="width: 250px">
                                <asp:Panel ID="pnlNoAnticipo" runat="server" Width="120px">
                                    <asp:TextBox ID="txtNoAnticipo" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                </asp:Panel>
                            </td>
                            <td style="width: 120px">
                                <asp:CheckBox ID="cbFechaCC" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Fecha Creación:" />
                            </td>
                            <td style="width: 340px">
                                <asp:Panel ID="pnlFechaCC" runat="server" Width="330px">
                                    <table style="width:260px;">
                                        <tr>
                                            <td style="border: medium inset #808080; margin-right: auto; margin-left: auto;">
                                                <table style="width: 127px;">
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lbl_FechaCCI" runat="server" Text="Fecha Inicial"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <ig:WebDatePicker ID="wdpFechaCI" runat="server" Font-Names="Verdana" Font-Size="8pt" MinValue="2014-08-06" Nullable="False" Width="120px">
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
                                                            <asp:Label ID="lbl_FechaCCF" runat="server" Text="Fecha Final"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <ig:WebDatePicker ID="wdpFechaCF" runat="server" Font-Names="Verdana" Font-Size="8pt" MinValue="2014-08-06" Nullable="False" Width="120px">
                                                            </ig:WebDatePicker>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td style="width: 120px">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">&nbsp;</td>
                            <td style="text-align: left">
                                <asp:CheckBox ID="cbStatus" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Estado Anticipo:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlStatus" runat="server" Width="310px">
                                    <asp:DropDownList ID="ddlStatus" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="140px">
                                        <asp:ListItem Value="'P'">Pendiente</asp:ListItem>
                                        <asp:ListItem Value="'A'">Autorizado</asp:ListItem>
                                        <asp:ListItem Value="'Z','ZC'">Rechazado</asp:ListItem>
                                        <asp:ListItem Value="'TR','TRCP','TRCA','TRCR','EE','EECP','EECA','EECR'">Finalizado</asp:ListItem>
                                        <asp:ListItem Value="'P','A','TR','TRCP','TRCA','TRCR','EE','EECP','EECA','EECR','Z','ZC'">Todos</asp:ListItem>
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:CheckBox ID="cbTipoAnt" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Tipo Anticipo:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlTipoAnt" runat="server" Width="130px">
                                    <asp:DropDownList ID="ddlTipoAnt" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                        <asp:ListItem Value="E">Efectivo</asp:ListItem>
                                        <asp:ListItem Value="T">Transferencia</asp:ListItem>
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:CheckBox ID="cbStatusC" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Estado Comp.:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlStatusC" runat="server" Width="310px">
                                    <asp:DropDownList ID="ddlStatusC" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="180px">
                                        <asp:ListItem Value="'P'">Pendiente</asp:ListItem>
                                        <asp:ListItem Value="'A'">Autorizado</asp:ListItem>
                                        <asp:ListItem Value="'Z','ZC','ZP'">Rechazado</asp:ListItem>
                                        <asp:ListItem Value="'R'">Comprobación Registrada</asp:ListItem>
                                        <asp:ListItem Value="'P','A','R','Z','ZC','ZP'">Todos</asp:ListItem>
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:CheckBox ID="cbNoComp" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="No. de Comp.:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlNoComp" runat="server" Width="150px">
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
                                        <td style="height: 40px">
                                            <asp:Button ID="btnExportar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Exportar" Width="150px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10px">&nbsp;</td>
                                        <td>
                                            <asp:GridView ID="gvRegistros" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1680px">
                                                <Columns>
                                                    <asp:BoundField DataField="No. Anticipo" HeaderText="No. Anticipo">
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Solicitante" HeaderText="Solicitante">
                                                    <ItemStyle Width="220px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Tipo Anticipo" HeaderText="Tipo Anticipo">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Importe del Anticipo" DataFormatString="{0:c}" HeaderText="Importe del Anticipo">
                                                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Fecha de Entrega del Efectivo" HeaderText="Fecha de Entrega del Efectivo">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Estatus Anticipo" HeaderText="Estatus Anticipo">
                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="No. Comprobación" HeaderText="No. Comprobación">
                                                    <ItemStyle HorizontalAlign="Center" Width="105px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Fecha de Validación de la comprobación" HeaderText="Fecha de Validación Comp">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Estatus Comprobación" HeaderText="Estatus Comprobación">
                                                    <ItemStyle HorizontalAlign="Center" Width="95px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Total Anticipo" DataFormatString="{0:c}" HeaderText="Total Anticipo">
                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Total Comprobado" DataFormatString="{0:c}" HeaderText="Total Comprobado">
                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Saldo Comprobación" DataFormatString="{0:c}" HeaderText="Saldo Comprobación">
                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:HyperLinkField DataNavigateUrlFields="evidencia_ruta" DataTextField="Evidencia" HeaderText="Evidencia">
                                                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                                                    </asp:HyperLinkField>
                                                    <asp:HyperLinkField DataNavigateUrlFields="vale_ruta" DataTextField="Vale Ingreso" HeaderText="Vale Ingreso">
                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                    </asp:HyperLinkField>
                                                    <asp:BoundField DataField="Pago Efectivo" HeaderText="Pago Efectivo">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Fecha Efectivo" HeaderText="Fecha Efectivo">
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
            </td>
        </tr>
        </table>
</asp:Content>