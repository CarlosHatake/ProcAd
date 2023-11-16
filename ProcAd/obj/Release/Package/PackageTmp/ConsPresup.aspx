<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ConsPresup.aspx.vb" Inherits="ProcAd.ConsPresup" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style5 {
            width: 142px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; font-weight: bold; color: #FF0000;">
                <ig:WebScriptManager ID="wsmConsAmpl" runat="server">
                </ig:WebScriptManager>
                <asp:TextBox ID="_txtIdSolicito" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtEmpleado" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtPerfil" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtPnlAut" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtUsrAut" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <table style="width: 1360px">
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
                            <td style="text-align: right; width: 180px;">&nbsp;</td>
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
                                <asp:CheckBox ID="cbAño" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Año:" Checked="True" Enabled="False" />
                            </td>
                            <td style="text-align: left; width: 540px;">
                                <asp:Panel ID="pnlAño" runat="server" Width="310px">
                                    <asp:DropDownList ID="ddlAño" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="90px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td style="text-align: left; width: 120px;">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 80px;">&nbsp;</td>
                            <td style="text-align: left; width: 120px;">
                                <asp:CheckBox ID="cbEmpresa" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Empresa:" />
                            </td>
                            <td style="text-align: left; width: 540px;">
                                <asp:Panel ID="pnlEmpresa" runat="server" Width="310px">
                                    <asp:DropDownList ID="ddlEmpresa" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="200px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td style="text-align: left; width: 120px;">
                                <asp:CheckBox ID="cbCC" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Centro de Costo:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlCC" runat="server" Width="310px">
                                    <asp:DropDownList ID="ddlCC" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px">
                                    </asp:DropDownList>
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
                                        <td style="width: 1px">&nbsp;</td>
                                        <td>
                                            <asp:GridView ID="gvRegistros" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Font-Names="Verdana" Font-Size="8pt" Width="6560px" >
                                                <Columns>
                                                    <asp:BoundField DataField="id_ms_presupuesto" HeaderText="ID">
                                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="centro_costo" HeaderText="Centro Costo">
                                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="año" HeaderText="Año">
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_01_p" DataFormatString="{0:c}" HeaderText="Enero Presupuestado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_01_a" DataFormatString="{0:c}" HeaderText="Enero Ampliado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_01_ep" DataFormatString="{0:c}" HeaderText="Enero En Proceso">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_01_r" DataFormatString="{0:c}" HeaderText="Enero Registrado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_01_disp" DataFormatString="{0:c}" HeaderText="Enero Disponible">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_02_p" DataFormatString="{0:c}" HeaderText="Febrero Presupuestado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_02_a" DataFormatString="{0:c}" HeaderText="Febrero Ampliado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_02_ep" DataFormatString="{0:c}" HeaderText="Febrero En Proceso">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_02_r" DataFormatString="{0:c}" HeaderText="Febrero Registrado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_02_disp" DataFormatString="{0:c}" HeaderText="Febrero Disponible">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_03_p" DataFormatString="{0:c}" HeaderText="Marzo Presupuestado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_03_a" DataFormatString="{0:c}" HeaderText="Marzo Ampliado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_03_ep" DataFormatString="{0:c}" HeaderText="Marzo En Proceso">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_03_r" DataFormatString="{0:c}" HeaderText="Marzo Registrado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_03_disp" DataFormatString="{0:c}" HeaderText="Marzo Disponible">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_04_p" DataFormatString="{0:c}" HeaderText="Abril Presupuestado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_04_a" DataFormatString="{0:c}" HeaderText="Abril Ampliado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_04_ep" DataFormatString="{0:c}" HeaderText="Abril En Proceso">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_04_r" DataFormatString="{0:c}" HeaderText="Abril Registrado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_04_disp" DataFormatString="{0:c}" HeaderText="Abril Disponible">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_05_p" DataFormatString="{0:c}" HeaderText="Mayo Presupuestado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_05_a" DataFormatString="{0:c}" HeaderText="Mayo Ampliado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_05_ep" DataFormatString="{0:c}" HeaderText="Mayo En Proceso">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_05_r" DataFormatString="{0:c}" HeaderText="Mayo Registrado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_05_disp" DataFormatString="{0:c}" HeaderText="Mayo Disponible">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_06_p" DataFormatString="{0:c}" HeaderText="Junio Presupuestado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_06_a" DataFormatString="{0:c}" HeaderText="Junio Ampliado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_06_ep" DataFormatString="{0:c}" HeaderText="Junio En Proceso">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_06_r" DataFormatString="{0:c}" HeaderText="Junio Registrado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_06_disp" DataFormatString="{0:c}" HeaderText="Junio Disponible">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_07_p" DataFormatString="{0:c}" HeaderText="Julio Presupuestado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_07_a" DataFormatString="{0:c}" HeaderText="Julio Ampliado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_07_ep" DataFormatString="{0:c}" HeaderText="Julio En Proceso">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_07_r" DataFormatString="{0:c}" HeaderText="Julio Registrado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_07_disp" DataFormatString="{0:c}" HeaderText="Julio Disponible">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_08_p" DataFormatString="{0:c}" HeaderText="Agosto Presupuestado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_08_a" DataFormatString="{0:c}" HeaderText="Agosto Ampliado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_08_ep" DataFormatString="{0:c}" HeaderText="Agosto En Proceso">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_08_r" DataFormatString="{0:c}" HeaderText="Agosto Registrado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_08_disp" DataFormatString="{0:c}" HeaderText="Agosto Disponible">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_09_p" DataFormatString="{0:c}" HeaderText="Septiembre Presupuestado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_09_a" DataFormatString="{0:c}" HeaderText="Septiembre Ampliado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_09_ep" DataFormatString="{0:c}" HeaderText="Septiembre En Proceso">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_09_r" DataFormatString="{0:c}" HeaderText="Septiembre Registrado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_09_disp" DataFormatString="{0:c}" HeaderText="Septiembre Disponible">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_10_p" DataFormatString="{0:c}" HeaderText="Octubre Presupuestado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_10_a" DataFormatString="{0:c}" HeaderText="Octubre Ampliado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_10_ep" DataFormatString="{0:c}" HeaderText="Octubre En Proceso">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_10_r" DataFormatString="{0:c}" HeaderText="Octubre Registrado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_10_disp" DataFormatString="{0:c}" HeaderText="Octubre Disponible">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_11_p" DataFormatString="{0:c}" HeaderText="Noviembre Presupuestado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_11_a" DataFormatString="{0:c}" HeaderText="Noviembre Ampliado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_11_ep" DataFormatString="{0:c}" HeaderText="Noviembre En Proceso">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_11_r" DataFormatString="{0:c}" HeaderText="Noviembre Registrado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_11_disp" DataFormatString="{0:c}" HeaderText="Noviembre Disponible">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_12_p" DataFormatString="{0:c}" HeaderText="Diciembre Presupuestado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_12_a" DataFormatString="{0:c}" HeaderText="Diciembre Ampliado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_12_ep" DataFormatString="{0:c}" HeaderText="Diciembre En Proceso">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_12_r" DataFormatString="{0:c}" HeaderText="Diciembre Registrado">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mes_12_disp" DataFormatString="{0:c}" HeaderText="Diciembre Disponible">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="acumulado" DataFormatString="{0:c}" HeaderText="Presupuesto acumulado disponible">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
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
