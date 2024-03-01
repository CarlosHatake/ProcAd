<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ConsContratos.aspx.vb" Inherits="ProcAd.ConsultaContratos" %>

<%@ Register Assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000;">
                <ig:WebScriptManager ID="wsm46" runat="server">
                </ig:WebScriptManager>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsInst" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdContrato" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtModContrato" runat="server" Width="15px" Visible="False"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Panel runat="server" ID="pnlInicio">
                    <asp:Panel runat="server" ID="pnlFiltros">
                        <table>
                            <tr>
                                <td style="width: 1366px"></td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td style="height: 50px; width: 500px; text-align: right">
                                    <asp:CheckBox runat="server" ID="cbEmpresa" Text="Empresa:" AutoPostBack ="true"  />
                                </td>
                                <td style="width: 195px; text-align: right">
                                    <asp:Panel runat="server" ID="pnlEmpresa">
                                        <asp:DropDownList runat="server" ID="ddlEmpresa" Width="180px"></asp:DropDownList>
                                    </asp:Panel>
                                </td>
                                <td style="height: 50px; width: 130px; text-align: right">
                                    <asp:CheckBox runat="server" ID="cbNoContrato" Text="No. Contrato:" AutoPostBack ="true" />
                                </td>
                                <td style="width: 195px; text-align: right">
                                    <asp:Panel runat="server" ID="pnlNoContrato">
                                        <%--<asp:DropDownList runat="server" ID="ddlNoContrato" Width="180px"></asp:DropDownList>--%>
                                        <asp:TextBox runat="server" ID="txtNoContrato" Width="170px" ></asp:TextBox>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 50px; width: 500px; text-align: right">
                                    <asp:CheckBox runat="server" ID="cbTipoArrenda" Text="Tipo arrendamiento:" AutoPostBack ="true"/>
                                </td>
                                <td style="width: 195px; text-align: right">
                                    <asp:Panel runat="server" ID="pnlTipoArrenda">
                                        <asp:DropDownList runat="server" ID="ddlTipoArrenda" Width="180px"></asp:DropDownList>
                                    </asp:Panel>
                                </td>
                                <td style="height: 50px; width: 130px; text-align: right">
                                    <asp:CheckBox runat="server" ID="cbArrendadora" Text="Arrendadora:" AutoPostBack ="true" />
                                </td>
                                <td style="width: 195px; text-align: right">
                                    <asp:Panel runat="server" ID="pnlArrendadora">
                                        <asp:DropDownList runat="server" ID="ddlArrendadora" Width="180px"></asp:DropDownList>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                    <table>
                        <tr>
                            <td style="width: 823px; height: 50px; text-align: right">
                                <asp:Button runat="server" ID="btnBuscar" Text="Buscar" Font-Size="8pt" Font-Names="Verdana" Width="120px" OnClientClick="this.disabled = true; this.value='Procesando…';" UseSubmitBehavior="false" Height="22px" />
                            </td>
                        </tr>
                    </table>

                    <asp:Panel runat="server" ID="pnlDatosContrato">
                        <table>
                            <tr>
                                <td style="width: 300px"></td>
                                <td>
                                    <asp:GridView ID="gvContratos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="800px" DataKeyNames="id_ms_contrato">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                <ItemStyle Width="15px" />
                                            </asp:CommandField>
                                            <asp:BoundField DataField="no_contrato" HeaderText="No. Contrato">
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                                <ItemStyle HorizontalAlign="Center" Width="160px" />
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
                                            <asp:BoundField DataField="fecha_fin" HeaderText="Fecha fin" DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:BoundField>
                                          <%--  <asp:HyperLinkField DataNavigateUrlFields="ruta" DataTextField="contrato" HeaderText="Contrato" />
                                            <asp:BoundField DataField="contrato" HeaderText="Contrato" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            </asp:BoundField>--%>
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
                </asp:Panel>
            </td>
        </tr>
    </table>

</asp:Content>
