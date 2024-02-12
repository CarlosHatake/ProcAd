<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="GenerarReporteDinamico.aspx.vb" Inherits="ProcAd.GenerarReporteDinamico" %>

<%@ Register Assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; height: 25px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000;">
                <ig:WebScriptManager ID="wsm46" runat="server">
                </ig:WebScriptManager>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsInst" runat="server" Width="15px" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 50px"></td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="pnlFiltros"></asp:Panel>
                <table>
                    <tr style="height: 40px">
                        <td style="width: 300px"></td>
                        <td>
                            <asp:Label runat ="server" Text="Contrato:" Font-Bold="true" ></asp:Label>
                        </td>
                        <td style="vertical-align:top">
                            <asp:Panel runat="server" ID="pnlContrato">
                                <asp:CheckBoxList runat="server">
                                    <asp:ListItem Text="No. Contrato" Value="1"/>
                                    <asp:ListItem Text="Empresa" Value="2"/>
                                    <asp:ListItem Text="Arrendadora" Value="3" />
                                    <asp:ListItem Text="Fecha inicio" Value="4" />
                                    <asp:ListItem Text="Fecha fin" Value="5" />
                                    <asp:ListItem Text="Plazo" Value="6" />
                                    <asp:ListItem Text="RFC Arrendadora" Value="7" />
                                </asp:CheckBoxList>
                            </asp:Panel>
                        </td>
                        <td style="width: 70px"></td>
                        <td>
                            <asp:Label runat="server" Text="Anexo" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="vertical-align:top">
                            <asp:Panel runat="server" ID="pnlAnexo">
                                <asp:CheckBoxList runat="server">
                                    <asp:ListItem Text="Empresa" Value="1"/>
                                    <asp:ListItem Text="No. Contrato" Value="2"/>
                                    <asp:ListItem Text="Arrendadora" Value="3" />
                                    <asp:ListItem Text="Anexo" Value="4" />
                                    <asp:ListItem Text="Fecha inicio" Value="5" />
                                    <asp:ListItem Text="Fecha fin" Value="6" />
                                   
                                </asp:CheckBoxList>
                            </asp:Panel>
                        </td>
                        <td style="width: 70px"></td>
                        <td>
                            <asp:Label runat="server" Text="Equipo:" Font-Bold="true"/>
                        </td>
                        <td>
                            <asp:Panel runat="server" ID="pnlEquipo">
                                <asp:CheckBoxList runat="server">
                                    <asp:ListItem Text="Serie" Value="1"/>
                                    <asp:ListItem Text="Provision" Value="2"/>
                                    <asp:ListItem Text="Tipo Arrendamiento" Value="3" />
                                    <asp:ListItem Text="División" Value="4" />
                                    <asp:ListItem Text="Tipo de unidad" Value="5" />
                                    <asp:ListItem Text="Zona" Value="6" />
                                    <asp:ListItem Text="Fecha actual" Value="7" />
                                    <asp:ListItem Text="Fecha fin" Value="8" />
                                    <asp:ListItem Text="Estatus" Value="9" />
                                </asp:CheckBoxList>
                            </asp:Panel>
                        </td>
                        <td style="width: 70px"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr style="height:40px">
                        <td style="width:650px"></td>
                        <td>
                            <asp:Button runat="server" Text="Generar reporte" Font-Names="Verdana" Font-Size="8pt"/>
                        </td>
                        <td style="width:620px"></td>

                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server">
                    <table>
                        <tr style="height:40px">
                            <td></td>
                            <td style="text-align:right">
                                <asp:Button runat="server" Text="Exportar" Font-Names="Verdana" Font-Size="8pt" Width="120px" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width:300px"></td>
                            <td>
                                <asp:GridView ID="gvReporte" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="800px" DataKeyNames="id_ms_anexo">
                                    <Columns>
                                        <asp:BoundField DataField="id_ms_contrato" HeaderText="No. Contrato">
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="anexo" HeaderText="Anexo">
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                            <ItemStyle HorizontalAlign="Center" Width="140px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="arrendadora" HeaderText="Arrendadora">
                                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_arrendamiento" HeaderText="Tipo de Arrendamiento">
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_inicio" HeaderText="Fecha Inicio" DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_fin" HeaderText="Fecha Fin" DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
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
