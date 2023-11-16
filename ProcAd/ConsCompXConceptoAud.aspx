<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ConsCompXConceptoAud.aspx.vb" Inherits="ProcAd.ConsCompXConceptoAud" %>
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
                <ig:WebScriptManager ID="wsmConsCompConta" runat="server">
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
                            <td style="text-align: right; width: 170px;">&nbsp;</td>
                            <td style="text-align: center; width: 120px;" class="auto-style5">
                                <asp:Label ID="lbl_Filtros" runat="server" Font-Bold="True" Text="Filtros..."></asp:Label>
                            </td>
                            <td >&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 170px;">&nbsp;</td>
                            <td class="auto-style5" style="text-align: center; width: 120px;">
                                <asp:CheckBox ID="cbFechaV" runat="server" AutoPostBack="True" Checked="True" Enabled="False" Font-Names="Verdana" Font-Size="8pt" Text="Fecha Validó:" />
                            </td>
                            <td>
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
                                        <td style="height: 40px">
                                            <asp:Button ID="btnExportar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Exportar" Width="150px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25px">&nbsp;</td>
                                        <td>
                                            <asp:GridView ID="gvRegistros" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="3600px">
                                                <Columns>
                                                    <asp:BoundField DataField="ID Empleado" HeaderText="Id Empleado">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Empleado" HeaderText="Empleado">
                                                    <ItemStyle Width="200px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Empresa" HeaderText="Empresa">
                                                    <ItemStyle Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CC / División" HeaderText="CC / División">
                                                    <ItemStyle Width="180px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Autorizador" HeaderText="Autorizador">
                                                    <ItemStyle Width="200px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Fecha de Solicitud" HeaderText="Fecha de Solicitud">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Fecha de Autorización" HeaderText="Fecha de Autorización">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Fecha de Validación" HeaderText="Fecha de Validación">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="No. Comp" HeaderText="No. Comp">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Tipo Gasto" HeaderText="Tipo Gasto">
                                                    <ItemStyle Width="150px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Tipo Actividad" HeaderText="Tipo Actividad">
                                                    <ItemStyle Width="150px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Periodo Comp." HeaderText="Periodo Comp.">
                                                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Anticipos" DataFormatString="{0:c}" HeaderText="Anticipos" >
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Comprobado" DataFormatString="{0:c}" HeaderText="Comprobado" >
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Saldo" DataFormatString="{0:c}" HeaderText="Saldo" >
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Tipo" HeaderText="Tipo">
                                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Concepto" HeaderText="Concepto">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Cuenta Contable" HeaderText="Cuenta Contable">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="No. Personas" HeaderText="No. Personas">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="No. Días" HeaderText="No. Días">
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IVA" HeaderText="IVA">
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Subtotal" DataFormatString="{0:c}" HeaderText="Subtotal">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:c}">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Proveedor" HeaderText="Proveedor">
                                                    <ItemStyle Width="150px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Origen - Destino" HeaderText="Origen - Destino">
                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Observaciones" HeaderText="Observaciones">
                                                    <ItemStyle Width="180px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Fecha Realizó" HeaderText="Fecha Realizó">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Día" HeaderText="Día">
                                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Mes" HeaderText="Mes" >
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Año" HeaderText="Año">
                                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Mes Numero" HeaderText="Mes Numero">
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
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        </table>
</asp:Content>