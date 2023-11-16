<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ConsNegoc.aspx.vb" Inherits="ProcAd.ConsNegoc" %>
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
        .auto-style10 {
            width: 140px;
            height: 21px;
        }
        .auto-style8 {
            height: 21px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="width: 1366px; text-align: center; font-weight: bold; color: #FF0000;">
                <ig:WebScriptManager ID="wsmConsFact" runat="server">
                </ig:WebScriptManager>
                <asp:TextBox ID="_txtIdSolicito" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
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
                    <asp:Panel ID="pnlSolicitó" runat="server">
                        <table style="width:100%;">
                            <tr>
                                <td style="text-align: right; width: 180px;">&nbsp;</td>
                                <td style="text-align: left; width: 130px;">
                                    <asp:CheckBox ID="cbEmpresa" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Empresa:" />
                                </td>
                                <td style="text-align: left; width: 360px;">
                                    <asp:Panel ID="pnlEmpresa" runat="server" Width="350px">
                                        <asp:DropDownList ID="ddlEmpresa" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="200px">
                                        </asp:DropDownList>
                                    </asp:Panel>
                                </td>
                                <td style="text-align: left; width: 130px;">
                                    <asp:CheckBox ID="cbSolicitante" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Solicitante:" />
                                </td>
                                <td>
                                    <asp:Panel ID="pnlSolicitante" runat="server" Width="350px">
                                        <asp:DropDownList ID="ddlSolicitante" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px">
                                        </asp:DropDownList>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: right; width: 180px;">&nbsp;</td>
                            <td style="text-align: left; width: 130px;">
                                <asp:CheckBox ID="cbFechaC" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Fecha Creación:" />
                            </td>
                            <td style="text-align: left; width: 360px;">
                                <asp:Panel ID="pnlFechaC" runat="server" Width="350px">
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
                            <td style="text-align: left; width: 130px;">
                                <asp:CheckBox ID="cbAutorizador" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Autorizador:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlAutorizador" runat="server" Width="350px">
                                    <asp:DropDownList ID="ddlAutorizador" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">&nbsp;</td>
                            <td style="text-align: left">
                                <asp:CheckBox ID="cbProveedor" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Proveedor:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlProveedor" runat="server" Width="350px">
                                    <asp:TextBox ID="txtProveedor" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="175px"></asp:TextBox>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:CheckBox ID="cbNoNegoc" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Folio:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlNoNegoc" runat="server" Width="350px">
                                    <asp:TextBox ID="txtNoNegoc" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">&nbsp;</td>
                            <td style="text-align: left">
                                <asp:CheckBox ID="cbStatus" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Estado:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlStatus" runat="server" Width="350px">
                                    <asp:DropDownList ID="ddlStatus" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                        <asp:ListItem Value="'P'">Pendiente de Cotización</asp:ListItem>
                                        <asp:ListItem Value="'CotI'">Pendiente Autorización de Cotización</asp:ListItem>
                                        <asp:ListItem Value="'CotA'">Pendiente Autorización de Negociación</asp:ListItem>
                                        <asp:ListItem Value="'NeA'">Autorizado</asp:ListItem>
                                        <asp:ListItem Value="'NeZ'">Rechazado</asp:ListItem>
                                        <asp:ListItem Value="'P', 'CotI', 'CotA', 'NeA', 'NeZ' ">Todos</asp:ListItem>
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
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
                                            <table style="width: 400px;">
                                                <tr>
                                                    <td style="width: 50px; height: 30px;">&nbsp;</td>
                                                    <td>
                                                        <asp:Button ID="btnExportar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Exportar" Width="150px" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:GridView ID="gvRegistros" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="2200px">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                    <ItemStyle Width="15px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="id_ms_negoc_servicio" HeaderText="Folio">
                                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="servicio" HeaderText="Servicio">
                                                    <ItemStyle Width="200px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="empleado_solicita" HeaderText="Solicita">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="empleado_cotiza" HeaderText="Cotiza" />
                                                    <asp:BoundField DataField="empleado_aut_cotiza" HeaderText="Autoriza Cotización" />
                                                    <asp:BoundField DataField="empleado_aut_negoc" HeaderText="Autoriza Negociación" />
                                                    <asp:BoundField DataField="empresa" HeaderText="Empresa" />
                                                    <asp:BoundField DataField="centro_costo" HeaderText="Centro Costo / División" />
                                                    <asp:BoundField DataField="base" HeaderText="Base" />
                                                    <asp:BoundField DataField="fecha_ini" HeaderText="Fecha Inicio" DataFormatString="{0:d}">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_fin" HeaderText="Fecha Fin" DataFormatString="{0:d}">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="proveedor" HeaderText="Proveedor" />
                                                    <asp:BoundField DataField="fecha_solicita" HeaderText="Fecha Solicita">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_cotiza" HeaderText="Fecha Cotización">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_aut_cotiza" HeaderText="Fecha Aut. de Cotización">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_aut_negoc" HeaderText="Fecha Aut. de Negociación">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="estatus" HeaderText="Estado">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
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
                                            <asp:GridView ID="gvRegistrosT" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                                                <Columns>
                                                    <asp:BoundField DataField="id_ms_negoc_servicio" HeaderText="Folio">
                                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="servicio" HeaderText="Servicio">
                                                    <ItemStyle Width="200px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="empleado_solicita" HeaderText="Solicita">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="empleado_cotiza" HeaderText="Cotiza" />
                                                    <asp:BoundField DataField="empleado_aut_cotiza" HeaderText="Autoriza Cotización" />
                                                    <asp:BoundField DataField="empleado_aut_negoc" HeaderText="Autoriza Negociación" />
                                                    <asp:BoundField DataField="empresa" HeaderText="Empresa" />
                                                    <asp:BoundField DataField="centro_costo" HeaderText="Centro Costo / División" />
                                                    <asp:BoundField DataField="base" HeaderText="Base" />
                                                    <asp:BoundField DataField="fecha_ini" HeaderText="Fecha Inicio" DataFormatString="{0:d}">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_fin" HeaderText="Fecha Fin" DataFormatString="{0:d}">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="proveedor" HeaderText="Proveedor" />
                                                    <asp:BoundField DataField="fecha_solicita" HeaderText="Fecha Solicita">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_cotiza" HeaderText="Fecha Cotización">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_aut_cotiza" HeaderText="Fecha Aut. de Cotización">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_aut_negoc" HeaderText="Fecha Aut. de Negociación">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="estatus" HeaderText="Estado">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
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
                <asp:Panel ID="pnlDetalle" runat="server" Width="1366px">
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: center; width: 300px; height: 35px;">
                                <asp:Button ID="btnNuevaBus" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Nueva Búsqueda" Width="150px" />
                            </td>
                            <td style="text-align: center; width: 460px; height: 35px;">
                                &nbsp;</td>
                            <td style="text-align: center; width: 210px; height: 35px;">
                                &nbsp;</td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Folio" runat="server" Text="Folio:"></asp:Label>
                            </td>
                            <td class="auto-style28">
                                <asp:Label ID="lblFolio" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1366px; height: 44px;">
                        <tr>
                            <td class="auto-style5" style="text-align: right; vertical-align: top;">
                                <asp:Label ID="lbl_Solicitante" runat="server" Text="Solicitante:"></asp:Label>
                            </td>
                            <td style="vertical-align: top; width: 390px;">
                                <asp:Label ID="lblSolicitante" runat="server" ForeColor="Blue" Width="380px"></asp:Label>
                            </td>
                            <td style="text-align: right; vertical-align: top;" class="auto-style10">
                                <asp:Label ID="lbl_Empresa" runat="server" Text="Empresa:"></asp:Label>
                            </td>
                            <td style="vertical-align: top; width: 220px;">
                                <asp:Label ID="lblEmpresa" runat="server" ForeColor="Blue" Width="200px"></asp:Label>
                            </td>
                            <td class="auto-style10" style="text-align: right; vertical-align: top;">
                                <asp:Label ID="lbl_CC" runat="server" Text="Centro de Costo:"></asp:Label>
                            </td>
                            <td class="auto-style8" style="vertical-align: top">
                                <asp:Label ID="lblCC" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align: top;" class="auto-style5">
                                <asp:Label ID="lbl_TipoServicio" runat="server" Text="Servicio:"></asp:Label>
                            </td>
                            <td style="vertical-align: top">
                                <asp:Label ID="lblTipoServicio" runat="server" ForeColor="Blue" Width="380px"></asp:Label>
                            </td>
                            <td style="text-align: right; vertical-align: top;">
                                <asp:Label ID="lbl_Base" runat="server" Text="Base:"></asp:Label>
                            </td>
                            <td style="vertical-align: top">
                                <asp:Label ID="lblBase" runat="server" ForeColor="Blue" Width="200px"></asp:Label>
                            </td>
                            <td style="text-align: right; vertical-align: top;">
                                <asp:Label ID="lbl_Vigencia" runat="server" Text="Vigencia del:"></asp:Label>
                            </td>
                            <td style="vertical-align: top">
                                <asp:Label ID="lblVigencia" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align: top;" class="auto-style5">
                                <asp:Label ID="lbl_Lugar" runat="server" Text="Lugar de Ejecución:"></asp:Label>
                            </td>
                            <td colspan="5" style="vertical-align: top">
                                <asp:Label ID="lblLugar" runat="server" ForeColor="Blue" Width="1100px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; ">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Descripcion" runat="server" Text="Descripción:"></asp:Label>
                            </td>
                            <td style="width: 747px">
                                <asp:TextBox ID="txtDescripcion" runat="server" Height="79px" ReadOnly="True" TextMode="MultiLine" Width="723px"></asp:TextBox>
                            </td>
                            <td class="auto-style10" style="text-align: right">
                                <asp:Label ID="lbl_Adjunto" runat="server" Text="Adjuntos:"></asp:Label>
                            </td>
                            <td>
                                <asp:GridView ID="gvAdjuntos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ShowHeader="False" Width="280px">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <Columns>
                                        <asp:HyperLinkField DataNavigateUrlFields="path" DataTextField="archivo" HeaderText="Archivo">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="20px" />
                                        </asp:HyperLinkField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1366px; height: 44px;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Cotizacion" runat="server" Text="Cotizaciones:"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Panel ID="pnlCotizaciones" runat="server">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 20px">&nbsp;</td>
                                            <td style="width: 265px;">
                                                <asp:Label ID="lbl_ProveedorCot" runat="server" Text="Proveedor"></asp:Label>
                                            </td>
                                            <td style="width: 110px">
                                                <asp:Label ID="lbl_Cantidad" runat="server" Text="Cantidad"></asp:Label>
                                            </td>
                                            <td style="width: 110px">
                                                <asp:Label ID="lbl_Importe" runat="server" Text="Importe"></asp:Label>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:Label ID="lbl_Divisa" runat="server" Text="Divisa"></asp:Label>
                                            </td>
                                            <td style="width: 120px">
                                                <asp:Label ID="lbl_FechaIni" runat="server" Text="Fecha Inicio"></asp:Label>
                                            </td>
                                            <td style="width: 120px">
                                                <asp:Label ID="lbl_FechaFin" runat="server" Text="Fecha de Término"></asp:Label>
                                            </td>
                                            <td style="width: 120px">
                                                <asp:Label ID="lbl_CondP" runat="server" Text="Cond. de Pago" Width="110px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_PDF" runat="server" Text="PDF Cotización" Width="120px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Label ID="lbl_No1" runat="server" Text="1"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblProveedorCot1" runat="server" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCantidad1" runat="server" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblImporte1" runat="server" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDivisa1" runat="server" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFechaIni1" runat="server" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFechaFin1" runat="server" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCondP1" runat="server" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="hlPDF1" runat="server" ForeColor="Blue">[hlPDF1]</asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Label ID="lbl_No2" runat="server" Text="2"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblProveedorCot2" runat="server" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCantidad2" runat="server" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblImporte2" runat="server" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDivisa2" runat="server" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFechaIni2" runat="server" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFechaFin2" runat="server" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCondP2" runat="server" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="hlPDF2" runat="server" ForeColor="Blue">[hlPDF2]</asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Label ID="lbl_No3" runat="server" Text="3"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblProveedorCot3" runat="server" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCantidad3" runat="server" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblImporte3" runat="server" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDivisa3" runat="server" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFechaIni3" runat="server" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFechaFin3" runat="server" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCondP3" runat="server" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="hlPDF3" runat="server" ForeColor="Blue">[hlPDF3]</asp:HyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_ProvSel" runat="server" Text="Proveedor Elegido:"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblProvSel" runat="server" Enabled="False" Font-Names="Verdana" Font-Size="8pt" RepeatColumns="3" Width="170px">
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_ComentarioComp" runat="server" Text="Comentarios Compras:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtComentarioComp" runat="server" Height="52px" ReadOnly="True" TextMode="MultiLine" Width="1072px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_ComentarioAut" runat="server" Text="Comentarios Aut.:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtComentario" runat="server" Height="52px" MaxLength="350" TextMode="MultiLine" Width="1072px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        </table>
</asp:Content>
