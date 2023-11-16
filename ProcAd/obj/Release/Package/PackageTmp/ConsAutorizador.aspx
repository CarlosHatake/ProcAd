﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ConsAutorizador.aspx.vb" Inherits="ProcAd.ConsAutorizador" %>
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
                <ig:WebScriptManager ID="wsmConsCompT" runat="server">
                </ig:WebScriptManager>
                <table style="width: 1350px;">
                    <tr>
                        <td>
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
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: right; width: 80px;">&nbsp;</td>
                            <td style="text-align: left; width: 120px;">
                                <asp:CheckBox ID="cbEmpresa" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Empresa:" />
                            </td>
                            <td style="text-align: left; width: 440px;">
                                <asp:Panel ID="pnlEmpresa" runat="server" Width="330px">
                                    <asp:DropDownList ID="ddlEmpresa" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="200px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td style="text-align: left; width: 120px;">
                                <asp:CheckBox ID="cbCC" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Centro de Costo:" />
                            </td>
                            <td style="width: 340px">
                                <asp:Panel ID="pnlCC" runat="server" Width="330px">
                                    <asp:DropDownList ID="ddlCC" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td style="width: 120px">
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">&nbsp;</td>
                            <td style="text-align: left">
                                <asp:CheckBox ID="cbEmpleado" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Empleado:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlEmpleado" runat="server" Width="330px">
                                    <asp:DropDownList ID="ddlEmpleado" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:CheckBox ID="cbAutorizador" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Autorizador:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlAutorizador" runat="server" Width="330px">
                                    <asp:DropDownList ID="ddlAutorizador" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
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
                                        <td style="height: 40px;">&nbsp;</td>
                                        <td>
                                            <table style="width: 200px;">
                                                <tr>
                                                    <td style="text-align: center">
                                                        <asp:Button ID="btnExportar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Exportar" Width="150px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10px">&nbsp;</td>
                                        <td>
                                            <asp:GridView ID="gvRegistros" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1500px">
                                                <Columns>
                                                    <asp:BoundField DataField="id_usuario" HeaderText="Id Usuario">
                                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Empleado" HeaderText="Empleado">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Usuario" HeaderText="Usuario" Visible="False">
                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Perfil" HeaderText="Perfil">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Empresa" HeaderText="Empresa">
                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Centro de Costo" HeaderText="Centro de Costo">
                                                    <ItemStyle HorizontalAlign="Left" Width="140px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="puesto" HeaderText="Puesto">
                                                    <ItemStyle HorizontalAlign="Left" Width="190px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Autorizador" HeaderText="Autorizador">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Director" HeaderText="Director">
                                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Usuario Autorizador" HeaderText="Usuario Autorizador" Visible="False">
                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Perfil Autorizador" HeaderText="Perfil Autorizador">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Empresa Autorizador" HeaderText="Empresa Autorizador">
                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Puesto Autorizador" HeaderText="Puesto Autorizador">
                                                    <ItemStyle HorizontalAlign="Left" Width="140px" />
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