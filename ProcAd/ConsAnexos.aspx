<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ConsAnexos.aspx.vb" Inherits="ProcAd.ConsAnexos" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .auto-style6 {
        width: 1018px;
    }
    .auto-style19 {
        width: 673px;
    }
    .auto-style9 {
        width: 1018px;
        height: 31px;
    }
    .auto-style43 {
        width: 140px;
    }
    .auto-style45 {
        width: 142px;
    }
    .auto-style47 {
        width: 263px;
    }
    .auto-style48 {
        width: 140px;
        height: 24px;
    }
    .auto-style49 {
        width: 142px;
        height: 24px;
    }
    .auto-style50 {
        width: 673px;
        height: 24px;
    }
    .auto-style51 {
        width: 237px;
    }
    #btnImprimir {
        width: 182px;
    }
    .auto-style53 {
        width: 215px;
    }
    #btnImprimir0 {
        width: 181px;
    }
    #btnImprimirC {
        width: 184px;
    }
    .auto-style54 {
        width: 100%;
    }
    .auto-style55 {
        width: 184px;
    }
    .auto-style56 {
        width: 130px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
    <tr>
        <td style="text-align: center; font-family: Verdana; font-size: 8pt; color: #FF0000" class="auto-style6">
            <ig:WebScriptManager ID="wsmConsResguardo" runat="server">
            </ig:WebScriptManager>
            <asp:TextBox ID="_txtTipoMov" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
            <asp:TextBox ID="_txtIdUsuario" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
            <asp:Literal ID="litError" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td class="auto-style6">
            <asp:Panel ID="pnlInicio" runat="server" Width="1366px">
                <table style="width:100%;">
                    <tr>
                        <td class="auto-style43" style="text-align: right">
                            &nbsp;</td>
                        <td class="auto-style45" style="text-align: center">
                            <asp:Label ID="lbl_Categorias" runat="server" Font-Bold="True" Text="Habilitar Filtros"></asp:Label>
                        </td>
                        <td class="auto-style19">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style48" style="text-align: right"></td>
                        <td class="auto-style49" style="text-align: left">
                            <asp:CheckBox ID="cbCarga" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Carga" AutoPostBack="True" />
                        </td>
                        <td class="auto-style50">
                            <asp:Panel ID="pnlCarga" runat="server" >
                                 <asp:TextBox ID="txtNoCarga" Text="" runat="server" Font-Names="Verdana" Font-Size="8pt" ></asp:TextBox>
                            </asp:Panel>
                        </td>
                    </tr>

                    <tr>
                        <td class="auto-style48" style="text-align: right">
                            </td>
                        <td class="auto-style49" style="text-align: left">
                            <asp:CheckBox ID="cbContratos" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Contratos" AutoPostBack="True" />
                        </td>
                        <td class="auto-style50">
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style48" style="text-align: right"></td>
                        <td class="auto-style49" style="text-align: left">
                            <asp:CheckBox ID="cbAnexos" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Anexos" AutoPostBack="True" />
                        </td>
                        <td class="auto-style50">
                           
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style43" style="text-align: right">&nbsp;</td>
                        <td class="auto-style45" style="text-align: left">
                            <asp:CheckBox ID="cbUnidades" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Unidades" AutoPostBack="True" />
                        </td>
                        <td class="auto-style19">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style43" style="text-align: right">&nbsp;</td>
                        <td class="auto-style45" style="text-align: left">
                            <asp:CheckBox ID="cbImporteUnidades" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Importes" AutoPostBack="True" />
                        </td>
                        <td class="auto-style19">&nbsp;</td>
                    </tr>

                </table>
                <table style="width: 100%; text-align: center; margin-right: auto; margin-left: auto; height: 60px;">
                    <tr>
                        <td>
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="150px" Font-Names="Verdana" Font-Size="8pt"/>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td class="auto-style9" style="text-align: center; margin-right: auto; margin-left: auto">
            <asp:Panel ID="pnlDatos" runat="server" Width="1366px">
                <table style="width:100%;">
                    <tr>
                        <td class="auto-style56"></td>
                        <td class="auto-style47">
                            <asp:Button ID="btnExportar" runat="server" Text="Exportar" Width="150px" Font-Names="Verdana" Font-Size="8pt"/>
                        </td>
                        <td style="text-align: center" class="auto-style51">
                            <asp:Panel ID="pnlImprimir" runat="server">
                                &nbsp;</asp:Panel>
                        </td>
                        <td style="text-align: center" class="auto-style53">
                            <asp:Panel ID="pnlImprimirC" runat="server" Width="225px">
                                &nbsp;</asp:Panel>
                        </td>
                        <td style="text-align: left">&nbsp;</td>
                    </tr>
                </table>
                <table class="auto-style54">
                    <tr>
                        <td class="auto-style55"></td>
                        <td>
                            <asp:GridView ID="gvContratos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1200px" AllowPaging="True" PageSize="20">
                                <Columns>
                                    <asp:BoundField DataField="id_ms_contrato" HeaderText="id_ms_contrato">
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:BoundField>
                                    <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                        <ItemStyle Width="15px" />
                                    </asp:CommandField>
                                    <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="no_contrato" HeaderText="Contrato">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="arrendadora" HeaderText="Arrendado">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tipo_arrendamiento" HeaderText="Tipo de arrendamiento">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="plazo_meses" HeaderText="Periodo">
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_inicio" HeaderText="Fecha inicio" DataFormatString="{0:dd/MM/yyyy}">
                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_fin" HeaderText="Fecha fin" DataFormatString="{0:dd/MM/yyyy}">
                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="inversion" HeaderText="Monto" DataFormatString="{0:c}">
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
                    <tr>
                        <td style="width: 100px"></td>
                        <td>
                            <asp:GridView ID="gvAnexos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="850px">
                                <Columns>
                                    <asp:BoundField DataField="id_ms_anexo" HeaderText="id_ms_anexo">
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:BoundField>
                                    <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                        <ItemStyle Width="15px" />
                                    </asp:CommandField>
                                    <asp:BoundField DataField="no_contrato" HeaderText="Contrato">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="anexo" HeaderText="Anexo">
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="arrendadora" HeaderText="Arrendado">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tipo_arrendamiento" HeaderText="Tipo de arrendamiento">
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
                        <td style="width: 100px"></td>
                        <td>
                            <asp:GridView ID="gvUnidades" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="850px">
                                <Columns>
                                    <asp:BoundField DataField="id_ms_equipo" HeaderText="id_ms_equipo">
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:BoundField>
                                    <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                        <ItemStyle Width="15px" />
                                    </asp:CommandField>
                                    <asp:BoundField DataField="no_contrato" HeaderText="Contrato">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="anexo" HeaderText="Anexo">
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="serie" HeaderText="Serie">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tipo_arrendamiento" HeaderText="Tipo de arrendamiento">
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
                        <td style="width: 100px"></td>
                        <td>
                            <asp:GridView ID="gvImportes" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="850px">
                                <Columns>
                                    <asp:BoundField DataField="serie" HeaderText="Serie">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="año" HeaderText="Año">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="mes1" HeaderText="Enero">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="mes2" HeaderText="Febrero">
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="mes3" HeaderText="Marzo">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="mes4" HeaderText="Abril">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="mes5" HeaderText="Mayo">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="mes6" HeaderText="Junio">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="mes7" HeaderText="Julio">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="mes8" HeaderText="Agosto">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="mes9" HeaderText="Septiembre">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="mes10" HeaderText="Octubre">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="mes11" HeaderText="Noviembre">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="mes12" HeaderText="Diciembre">
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
                        <td style="width:100px"></td>
                        <td>
                            <asp:GridView ID="gvExportar" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1011px">
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
