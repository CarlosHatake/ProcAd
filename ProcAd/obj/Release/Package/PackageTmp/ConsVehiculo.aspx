<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ConsVehiculo.aspx.vb" Inherits="ProcAd.ConsVehiculo" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style5 {
            width: 142px;
        }
        </style>
    <script type="text/javascript">
        function impFormato() {
            window.open("IEvaluacion.aspx", "Get", "width=910, height=680, resizeable=no, scrollbars=yes, toolbar=no, menubar=no")
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="width: 1366px; text-align: center; font-weight: bold; color: #FF0000;">
                <ig:WebScriptManager ID="wsmConsEval" runat="server">
                </ig:WebScriptManager>
                    <asp:TextBox ID="_txtEmpleado" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtPerfil" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlFiltros" runat="server" Width="1366px">
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: right; width: 180px;">&nbsp;</td>
                            <td style="text-align: center; " class="auto-style5">
                                <asp:Label ID="lbl_Filtros" runat="server" Font-Bold="True" Text="Filtros..."></asp:Label>
                            </td>
                            <td >&nbsp;</td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: right; width: 40px;">&nbsp;</td>
                            <td style="text-align: left; width: 120px;">
                                <asp:CheckBox ID="cbUsoUtil" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Uso Utilitario:" />
                            </td>
                            <td style="text-align: left; width: 310px;">
                                <asp:Panel ID="pnlUsoUtil" runat="server" Width="290px">
                                    <asp:DropDownList ID="ddlUsoUtil" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="200px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td style="text-align: left; width: 110px;">
                                <asp:CheckBox ID="cbAsignadoA" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Asignado a:" />
                            </td>
                            <td style="text-align: left; width: 330px;">
                                <asp:Panel ID="pnlAsignadoA" runat="server" Width="310px">
                                    <asp:DropDownList ID="ddlAsignadoA" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td style="text-align: left; width: 90px;">
                                <asp:CheckBox ID="cbStatus" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Estado:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlStatus" runat="server" Width="290px">
                                    <asp:DropDownList ID="ddlStatus" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                        <asp:ListItem Value="A">Activo</asp:ListItem>
                                        <asp:ListItem Value="S">Siniestrado</asp:ListItem>
                                        <asp:ListItem Value="M">Mantenimiento</asp:ListItem>
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">&nbsp;</td>
                            <td style="text-align: left">
                                <asp:CheckBox ID="cbNoEco" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="No. Económico:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlNoEco" runat="server" Width="310px">
                                    <asp:TextBox ID="txtNoEco" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="140px"></asp:TextBox>
                                </asp:Panel>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:CheckBox ID="cbPlacas" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Placas:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlPlacas" runat="server" Width="290px">
                                    <asp:TextBox ID="txtPlacas" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="120px"></asp:TextBox>
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
                                        <td>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="width: 10px">&nbsp;</td>
                                                    <td>
                                                        <table style="width: 1320px; height: 35px;">
                                                            <tr>
                                                                <td style="text-align: center; width: 300px;">
                                                                    <asp:Button ID="btnExportar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Exportar" Width="150px" />
                                                                </td>
                                                                <td style="text-align: center">&nbsp;</td>
                                                                <td style="text-align: center; width: 300px;">
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 10px">&nbsp;</td>
                                                    <td>
                                                        <asp:GridView ID="gvRegistros" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="3800px">
                                                            <Columns>
                                                                <asp:BoundField DataField="No. Económico" HeaderText="No. Económico">
                                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Marca" HeaderText="Marca">
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Modelo" HeaderText="Modelo">
                                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Tipo Unidad" HeaderText="Tipo Unidad">
                                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Uso Utilitario" HeaderText="Uso Utilitario">
                                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Serie" HeaderText="Serie">
                                                                <ItemStyle HorizontalAlign="Left" Width="130px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Motor" HeaderText="Motor">
                                                                <ItemStyle HorizontalAlign="Left" Width="110px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Año" HeaderText="Año">
                                                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Ubicación" HeaderText="Ubicación">
                                                                <ItemStyle HorizontalAlign="Left" Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Estado" HeaderText="Estado">
                                                                <ItemStyle HorizontalAlign="Left" Width="40px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Placas" HeaderText="Placas">
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Periodo Verificación" HeaderText="Periodo Verificación">
                                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Vig. Verificación" HeaderText="Vig. Verificación" DataFormatString="{0:d}">
                                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Vig. Tarjeta Circulación" HeaderText="Vig. Tarjeta Circulación" DataFormatString="{0:d}">
                                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Tipo IAVE" HeaderText="Tipo IAVE">
                                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IAVE" HeaderText="IAVE">
                                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Fecha Ult. Mantenimiento" HeaderText="Fecha Ult. Mantenimiento" DataFormatString="{0:d}">
                                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Fecha Prox. Mantenimiento" HeaderText="Fecha Prox. Mantenimiento" DataFormatString="{0:d}">
                                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Fecha Prox. Mantenimiento KM" HeaderText="Fecha Prox. Mantenimiento KM" DataFormatString="{0:d}">
                                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Póliza Seguro" HeaderText="Póliza Seguro">
                                                                <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Póliza Inciso" HeaderText="Póliza Inciso">
                                                                <ItemStyle HorizontalAlign="Left" Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Inicio Póliza de Seguro" HeaderText="Inicio Póliza de Seguro" DataFormatString="{0:d}">
                                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Vig. Póliza de Seguro" HeaderText="Vig. Póliza de Seguro" DataFormatString="{0:d}">
                                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Último KM" HeaderText="Último KM">
                                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="KM Actual" HeaderText="KM Actual">
                                                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="No. Tarjeta Edenred" HeaderText="No. Tarjeta Edenred">
                                                                <ItemStyle HorizontalAlign="Center" Width="110px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Arrendado" HeaderText="Arrendado">
                                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Arrendadora" HeaderText="Arrendadora">
                                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="No. de Contrato" HeaderText="No. de Contrato">
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Anexo" HeaderText="Anexo">
                                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Tipo de Arrendamiento" HeaderText="Tipo de Arrendamiento">
                                                                <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Meses de Arrendamiento" HeaderText="Meses de Arrendamiento">
                                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Inicio de Arrendamiento" HeaderText="Inicio de Arrendamiento" DataFormatString="{0:d}">
                                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Fin de Arrendamiento" HeaderText="Fin de Arrendamiento" DataFormatString="{0:d}">
                                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Observaciones" HeaderText="Observaciones">
                                                                <ItemStyle HorizontalAlign="Left" Width="70px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Asignado a" HeaderText="Asignado a">
                                                                <ItemStyle HorizontalAlign="Left" Width="130px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Estatus" HeaderText="Estatus">
                                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
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
                                                    <td style="width: 10px">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
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
