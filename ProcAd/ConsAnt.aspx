<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ConsAnt.aspx.vb" Inherits="ProcAd.ConsAnt" %>
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
        .auto-style9 {
            width: 300px;
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
            <td style="text-align: center; font-weight: bold; color: #FF0000;">
                <ig:WebScriptManager ID="wsmConsAnt" runat="server">
                </ig:WebScriptManager>
                <asp:TextBox ID="_txtIdSolicito" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtEmpleado" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtPerfil" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
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
                    <asp:Panel ID="pnlSolicitó" runat="server">
                        <table style="width:100%;">
                            <tr>
                                <td style="text-align: right; width: 80px;">&nbsp;</td>
                                <td style="text-align: left; width: 120px;">
                                    <asp:CheckBox ID="cbSolicitante" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Solicitante:" />
                                </td>
                                <td style="text-align: left; width: 340px;">
                                    <asp:Panel ID="pnlSolicitante" runat="server" Width="310px">
                                        <asp:DropDownList ID="ddlSolicitante" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px">
                                        </asp:DropDownList>
                                    </asp:Panel>
                                </td>
                                <td style="text-align: left; width: 120px;">
                                    <asp:CheckBox ID="cbEmpresa" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Empresa:" />
                                </td>
                                <td>
                                    <asp:Panel ID="pnlEmpresa" runat="server" Width="310px">
                                        <asp:DropDownList ID="ddlEmpresa" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="200px">
                                        </asp:DropDownList>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: right; width: 80px;">&nbsp;</td>
                            <td style="text-align: left; width: 120px;">
                                <asp:CheckBox ID="cbFechaC" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Fecha Creación:" />
                            </td>
                            <td style="text-align: left; width: 340px;">
                                <asp:Panel ID="pnlFechaC" runat="server" Width="310px">
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
                                <asp:CheckBox ID="cbAutorizador" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Autorizador:" />
                            </td>
                            <td style="width: 340px">
                                <asp:Panel ID="pnlAutorizador" runat="server" Width="310px">
                                    <asp:DropDownList ID="ddlAutorizador" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td style="text-align: left; width: 120px;">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">&nbsp;</td>
                            <td style="text-align: left">
                                <asp:CheckBox ID="cbStatus" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Estado:" />
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
                                        <asp:ListItem Value="T">Transferencia</asp:ListItem>
                                        <asp:ListItem Value="E">Efectivo</asp:ListItem>
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:CheckBox ID="cbNoAnticipo" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="No. de Anticipo:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlNoAnticipo" runat="server" Width="120px">
                                    <asp:TextBox ID="txtNoAnticipo" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
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
                                        <td style="width: 1px">&nbsp;</td>
                                        <td>
                                            <asp:GridView ID="gvRegistros" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1380px">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                    <ItemStyle Width="15px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="no_anticipo" HeaderText="No. Anticipo">
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Empresa" HeaderText="Empresa">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="tipoAnt" HeaderText="Tipo">
                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="empleado" HeaderText="Solicita">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="autorizador" HeaderText="Autoriza" />
                                                    <asp:BoundField DataField="periodo_ini" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Desde">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="periodo_fin" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Hasta">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="importe" DataFormatString="{0:c}" HeaderText="Importe">
                                                    <ItemStyle HorizontalAlign="Right" Width="110px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_solicita" HeaderText="Fecha Solicita">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_autoriza" HeaderText="Fecha Autoriza">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_pago" HeaderText="Fecha Pago">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="estatus" HeaderText="Estado">
                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="comprobado" HeaderText="Comprobación">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AMEX" HeaderText="AMEX">
                                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="codigo_reservacion" HeaderText="Codigo Reservacion">
                                                        <ItemStyle HorizontalAlign="center" Width="50px" />
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
                                                    <asp:BoundField DataField="no_anticipo" HeaderText="No. Anticipo">
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Empresa" HeaderText="Empresa">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="tipoAnt" HeaderText="Tipo">
                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="empleado" HeaderText="Solicita">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="autorizador" HeaderText="Autoriza" />
                                                    <asp:BoundField DataField="periodo_ini" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Desde">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="periodo_fin" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Hasta">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="importe" DataFormatString="{0:c}" HeaderText="Importe">
                                                    <ItemStyle HorizontalAlign="Right" Width="110px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_solicita" HeaderText="Fecha Solicita">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_autoriza" HeaderText="Fecha Autoriza">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_pago" HeaderText="Fecha Pago">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="estatus" HeaderText="Estado">
                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="comprobado" HeaderText="Comprobación">
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
                <asp:Panel ID="pnlDetalle" runat="server">
                    <table style="width:1360px;">
                        <tr>
                            <td style="text-align: center; width: 300px; height: 35px;">
                                <asp:Button ID="btnNueBusProd" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Nueva Búsqueda" Width="150px" />
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
                    <table style="width: 1360px; height: 44px;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Solicitante" runat="server" Text="Solicitante:"></asp:Label>
                            </td>
                            <td class="auto-style9">
                                <asp:Label ID="lblSolicitante" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td class="auto-style10" style="text-align: right">
                                <asp:Label ID="lbl_NoProveedor" runat="server" Text="Num. Proveedor:"></asp:Label>
                            </td>
                            <td class="auto-style9">
                                <asp:Label ID="lblNoProveedor" runat="server" ForeColor="Blue" Width="150px"></asp:Label>
                            </td>
                            <td class="auto-style10" style="text-align: right; ">
                                <asp:Label ID="lbl_Empresa" runat="server" Text="Empresa:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <asp:Label ID="lblEmpresa" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Autorizador" runat="server" Text="Autorizador:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblAutorizador" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Periodo" runat="server" Text="Periodo de Comisión:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPeriodo" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Destino" runat="server" Text="Destino:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDestino" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_CodigoReservacion" runat="server" Text="Codigo Reservación:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCodigoReservacion" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1360px;">
                        <tr>
                            <td style="width: 150px; height: 30px; text-align: right">
                                <asp:Label ID="lbl_Act" runat="server" Text="Actividad:"></asp:Label>
                            </td>
                            <td style="width: 747px">
                                <asp:TextBox ID="txtAct" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="43px" ReadOnly="True" TextMode="MultiLine" Width="735px"></asp:TextBox>
                            </td>
                            <td class="auto-style10" style="text-align: right;">
                                <asp:Label ID="lbl_NoPersonas" runat="server" Text="Cantidad de Personas:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <ig:WebNumericEditor ID="wneNoPersonas" runat="server" Font-Names="Verdana" Font-Size="8pt" Nullable="False" ReadOnly="True" Width="70px">
                                </ig:WebNumericEditor>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1360px; height: 60px;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">&nbsp;</td>
                            <td style="width: 100px; text-align: center">
                                <asp:Label ID="lbl_Días" runat="server" Text="Días"></asp:Label>
                            </td>
                            <td style="text-align: center; width: 180px;">
                                <asp:Label ID="lbl_Monto" runat="server" Text="Monto"></asp:Label>
                            </td>
                            <td rowspan="4" style="width: 458px; text-align: center">
                                <asp:Label ID="lbl_BuenViaje" runat="server" Font-Bold="True" Font-Size="18pt" Text="Buen  Viaje" Visible="False"></asp:Label>
                            </td>
                            <td class="auto-style10" style="text-align: right;">
                                <asp:Label ID="lbl_TipoPago" runat="server" Text="Tipo Pago:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTipoPago" runat="server" ForeColor="Blue" Width="200px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Hospedaje" runat="server" Text="No. noches hospedaje:"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <ig:WebNumericEditor ID="wneDiasH" runat="server" Font-Names="Verdana" Font-Size="8pt" ReadOnly="True" Width="70px">
                                </ig:WebNumericEditor>
                            </td>
                            <td style="text-align: center">
                                <ig:WebCurrencyEditor ID="wceMontoH" runat="server" ReadOnly="True">
                                </ig:WebCurrencyEditor>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Alimentos" runat="server" Text="Alimentos:"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <ig:WebNumericEditor ID="wneDiasA" runat="server" Font-Names="Verdana" Font-Size="8pt" ReadOnly="True" Width="70px">
                                </ig:WebNumericEditor>
                            </td>
                            <td style="text-align: center">
                                <ig:WebCurrencyEditor ID="wceMontoA" runat="server" ReadOnly="True">
                                </ig:WebCurrencyEditor>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Casetas" runat="server" Text="Casetas:"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <ig:WebNumericEditor ID="wneDiasC" runat="server" Font-Names="Verdana" Font-Size="8pt" ReadOnly="True" Width="70px">
                                </ig:WebNumericEditor>
                            </td>
                            <td style="text-align: center">
                                <ig:WebCurrencyEditor ID="wceMontoC" runat="server" ReadOnly="True">
                                </ig:WebCurrencyEditor>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Otros" runat="server" Text="Otros *Especificar:"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <ig:WebNumericEditor ID="wneDiasO" runat="server" Font-Names="Verdana" Font-Size="8pt" ReadOnly="True" Width="70px">
                                </ig:WebNumericEditor>
                            </td>
                            <td style="text-align: center">
                                <ig:WebCurrencyEditor ID="wceMontoO" runat="server" ReadOnly="True">
                                </ig:WebCurrencyEditor>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lblOtros" runat="server" ForeColor="Blue" Width="500px"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_MontoT" runat="server" Text="Monto Solicitado:"></asp:Label>
                            </td>
                            <td style="text-align: center">&nbsp;</td>
                            <td style="text-align: center">
                                <ig:WebCurrencyEditor ID="wceMontoT" runat="server" Nullable="False" ReadOnly="True">
                                </ig:WebCurrencyEditor>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lblMontoTLetra" runat="server" Font-Italic="True"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        </table>
</asp:Content>
