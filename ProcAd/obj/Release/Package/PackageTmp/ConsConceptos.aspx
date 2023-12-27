
<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ConsConceptos.aspx.vb" Inherits="ProcAd.ConsConceptos" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style5 {
            width: 142px;
        }
        .auto-style28 {
            width: 110px;
        }
        .auto-style46 {
            width: 483px;
        }
        </style>
       
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; font-weight: bold; color: #FF0000;">
                <ig:WebScriptManager ID="wsmConsSol" runat="server">
                </ig:WebScriptManager>
                <asp:TextBox ID="_txtIdSolicito" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
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
                            <td> &nbsp; </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlSolicitó" runat="server">
                        <table style="width:100%;">
                            <tr>
                                <td style="text-align: right; width: 140px;">&nbsp;</td>
                                <td style="text-align: left; width: 130px;">
                                    <asp:CheckBox ID="cbConcepto" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Tipo Concepto:" />
                                </td>
                                <td style="text-align: left; ">
                                    <asp:Panel ID="pnlSolicitante" runat="server" Width="350px">
                                        <asp:DropDownList ID="ddlConcepto" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                            <asp:ListItem Value="'NA'"> SELECCIONE UNA OPCIÓN </asp:ListItem>
                                            <asp:ListItem Value="'F'">Con Factura</asp:ListItem>
                                            <asp:ListItem Value="'SNF'">Sin Factura</asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:Panel>
                                </td>
                             
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 140px;">&nbsp;</td>
                                <td style="text-align: left; width: 130px;">
                                    <asp:CheckBox ID="cbServicios" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Servicios:" />
                                </td>
                                <td style="text-align: left; ">
                                    <asp:Panel ID="pnlServicios" runat="server" Width="350px">
                                        <asp:DropDownList ID="ddlServicios" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                            <asp:ListItem Value="'NA'"> SELECCIONE UNA OPCIÓN </asp:ListItem>
                                            <asp:ListItem Value="'S'">Servicios</asp:ListItem>
                                            <asp:ListItem Value="'AU'">Autorizadores</asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr style="height:40px">
                                <td>&nbsp;</td>
                                <td>
                                    <asp:Panel ID="pnlExportar" runat="server">
                                        <asp:Button ID="btnExportar" runat="server" Text="Exportar" Width="150px" Font-Names="Verdana" Font-Size="8pt" />
                                    </asp:Panel>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                  

                    <table style="width: 100%; text-align: center; margin-right: auto; margin-left: auto; height: 60px;">
                        <tr>
                            <td class="auto-style51" style=" width:1120px; text-align:center">
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
                                        <td style="width: 180px">&nbsp;</td>
                                        <td>
                                            <asp:GridView ID="gvRegistrosCon" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="750px">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                    <ItemStyle Width="15px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="concepto" HeaderText="Concepto">
                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="iva" HeaderText="IVA">
                                                    <ItemStyle HorizontalAlign="Left" Width="50px"/>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="cuenta" HeaderText="Cuenta">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="cve_concepto1" HeaderText="Clave Concepto">
                                                    <ItemStyle HorizontalAlign="Center" Width="110px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="abreviatura" HeaderText="Abreviatura">
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
               
                  <asp:Panel ID="pnlRegistrosServicios" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td class="auto-style55">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width: 180px">&nbsp;</td>
                                        <td>
                                            <asp:GridView ID="gvServicios" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="750px">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                    <ItemStyle Width="15px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="servicio" HeaderText="Servicio">
                                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="tipo_servicio" HeaderText="Tipo Servicio">
                                                    <ItemStyle HorizontalAlign="Left" Width="50px"/>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="admon_oper" HeaderText="Admon / Oper">
                                                    <ItemStyle HorizontalAlign="Center" Width="110px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="empresa" HeaderText="Empresa">
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

