<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ConsComp.aspx.vb" Inherits="ProcAd.ConsComp" %>
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
            height: 20px;
        }
        .auto-style8 {
            height: 20px;
        }
        .auto-style11 {
            width: 180px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; font-weight: bold; color: #FF0000;">
                <ig:WebScriptManager ID="wsmConsComp" runat="server">
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
                            <td style="text-align: right; width: 80px;">&nbsp;</td>
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
                                    <asp:Panel ID="pnlSolicitante" runat="server" Width="330px">
                                        <asp:DropDownList ID="ddlSolicitante" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px">
                                        </asp:DropDownList>
                                    </asp:Panel>
                                </td>
                                <td style="text-align: left; width: 120px;">
                                    <asp:CheckBox ID="cbEmpresa" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Empresa:" />
                                </td>
                                <td>
                                    <asp:Panel ID="pnlEmpresa" runat="server" Width="330px">
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
                            <td style="text-align: left; width: 120px;">
                                <asp:CheckBox ID="cbAutorizador" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Autorizador:" />
                            </td>
                            <td style="width: 340px">
                                <asp:Panel ID="pnlAutorizador" runat="server" Width="330px">
                                    <asp:DropDownList ID="ddlAutorizador" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td style="width: 120px">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">&nbsp;</td>
                            <td style="text-align: left">
                                <asp:CheckBox ID="cbStatus" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Estado:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlStatus" runat="server" Width="330px">
                                    <asp:DropDownList ID="ddlStatus" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="180px">
                                        <asp:ListItem Value="'P'">Pendiente</asp:ListItem>
                                        <asp:ListItem Value="'A'">Autorizado</asp:ListItem>
                                        <asp:ListItem Value="'Z','ZA','ZD','ZC','ZP','ZU'">Rechazado</asp:ListItem>
                                        <asp:ListItem Value="'R'">Comprobación Registrada</asp:ListItem>
                                        <asp:ListItem Value="'P','A','R','Z','ZA','ZD','ZC','ZP','ZU'">Todos</asp:ListItem>
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:CheckBox ID="cbPagoEfec" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Pago Efectivo.:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlPagoEfec" runat="server" Width="330px">
                                    <asp:DropDownList ID="ddlPagoEfec" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="50px">
                                        <asp:ListItem Value="S">Sí</asp:ListItem>
                                        <asp:ListItem Value="N">No</asp:ListItem>
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:CheckBox ID="cbNoComp" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="No. de Comp.:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlNoComp" runat="server" Width="150px">
                                    <asp:TextBox ID="txtNoComp" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
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
                                        <td style="height: 35px">
                                            <asp:Button ID="btnExportar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Exportar" Width="150px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 1px">&nbsp;</td>
                                        <td>
                                            <asp:GridView ID="gvRegistros" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1680px">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                    <ItemStyle Width="15px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="no_comp" HeaderText="No. Comp">
                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Empresa" HeaderText="Empresa">
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="empleado" HeaderText="Solicita">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="autorizador" HeaderText="Autoriza" />
                                                    <asp:BoundField DataField="director" HeaderText="Director">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="periodo_ini" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Desde">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="periodo_fin" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Hasta">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="importe_ant" DataFormatString="{0:c}" HeaderText="Anticipos">
                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="importe_comp" DataFormatString="{0:c}" HeaderText="Comprobado">
                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="importe_tot" DataFormatString="{0:c}" HeaderText="Total">
                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_solicita" HeaderText="Fecha Solicita">
                                                    <ItemStyle HorizontalAlign="Center" Width="85px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_autoriza" HeaderText="Fecha Autoriza">
                                                    <ItemStyle HorizontalAlign="Center" Width="85px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_aut_dir" HeaderText="Fecha Director">
                                                    <ItemStyle HorizontalAlign="Center" Width="85px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_valida" HeaderText="Fecha Valida">
                                                    <ItemStyle HorizontalAlign="Center" Width="85px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Pago Efectivo" HeaderText="Pago Efectivo">
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Fecha Efectivo" HeaderText="Fecha Efectivo">
                                                    <ItemStyle HorizontalAlign="Center" Width="85px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="estatus" HeaderText="Estado">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nombre_actividad" HeaderText="Actividad Actual">
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
                                                    <asp:BoundField DataField="no_comp" HeaderText="No. Comp">
                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Empresa" HeaderText="Empresa">
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="empleado" HeaderText="Solicita">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="autorizador" HeaderText="Autoriza" />
                                                    <asp:BoundField DataField="director" HeaderText="Director">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="periodo_ini" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Desde">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="periodo_fin" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Hasta">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="importe_ant" DataFormatString="{0:c}" HeaderText="Anticipos">
                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="importe_comp" DataFormatString="{0:c}" HeaderText="Comprobado">
                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="importe_tot" DataFormatString="{0:c}" HeaderText="Total">
                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_solicita" HeaderText="Fecha Solicita">
                                                    <ItemStyle HorizontalAlign="Center" Width="85px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_autoriza" HeaderText="Fecha Autoriza">
                                                    <ItemStyle HorizontalAlign="Center" Width="85px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_aut_dir" HeaderText="Fecha Director">
                                                    <ItemStyle HorizontalAlign="Center" Width="85px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_valida" HeaderText="Fecha Valida">
                                                    <ItemStyle HorizontalAlign="Center" Width="85px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Pago Efectivo" HeaderText="Pago Efectivo">
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Fecha Efectivo" HeaderText="Fecha Efectivo">
                                                    <ItemStyle HorizontalAlign="Center" Width="85px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="estatus" HeaderText="Estado">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nombre_actividad" HeaderText="Actividad Actual">
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
                <asp:Panel ID="pnlDetalle" runat="server">
                    <table style="width:1366px;">
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
                    <table style="width: 1366px; height: 66px;">
                        <tr>
                            <td style="text-align: right; width: 150px;">
                                <asp:Label ID="lbl_Empresa" runat="server" Text="Empresa:"></asp:Label>
                            </td>
                            <td style="width: 300px; ">
                                <asp:Label ID="lblEmpresa" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 160px;" class="auto-style10">
                                <asp:Label ID="lbl_Periodo" runat="server" Text="Periodo de Comprobación:"></asp:Label>
                            </td>
                            <td style="width: 300px; ">
                                <asp:Label ID="lblPeriodo" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 120px;">
                                <asp:Label ID="lbl_Empleado" runat="server" Text="Empleado:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <asp:Label ID="lblEmpleado" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_TipoGasto" runat="server" Text="Tipo de Gasto:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTipoGasto" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_TipoAct" runat="server" Text="Tipo de Actividad:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTipoAct" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Autorizador" runat="server" Text="Autorizador:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblAutorizador" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_CC" runat="server" Text="Centro de Costo:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCC" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Div" runat="server" Text="División:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDiv" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Director" runat="server" Text="Director:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDirector" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1366px;">
                        <tr>
                            <td style="width: 150px; height: 30px; text-align: right">
                                <asp:Label ID="lbl_Just" runat="server" Text="Justificación:"></asp:Label>
                            </td>
                            <td style="width: 767px">
                                <asp:TextBox ID="txtJust" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="74px" ReadOnly="True" TextMode="MultiLine" Width="735px"></asp:TextBox>
                            </td>
                            <td class="auto-style10" style="text-align: right;">
                                <asp:Label ID="lbl_Anticipos" runat="server" Text="Anticipos:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <asp:GridView ID="gvAnticipos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="300px">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <Columns>
                                        <asp:BoundField DataField="id_ms_anticipo" HeaderText="No. Anticipo">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="periodo_ini" DataFormatString="{0:d}" HeaderText="Fecha Ini">
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="periodo_fin" DataFormatString="{0:d}" HeaderText="Fecha Fin">
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="importe" DataFormatString="{0:c}" HeaderText="Importe">
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
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
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_ObsAut" runat="server" Text="Observaciones del Autorizador:" Width="130px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtObsAut" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="56px" ReadOnly="True" TextMode="MultiLine" Width="735px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 150px; height: 20px; text-align: right">
                                &nbsp;</td>
                            <td style="width: 767px; vertical-align: top;" rowspan="2">
                                <table style="width: 678px;">
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td style="width: 110px; text-align: center;">
                                                        <asp:Label ID="lbl_TotalA" runat="server" Font-Bold="True" Text="Anticipos"></asp:Label>
                                                    </td>
                                                    <td style="width: 110px; text-align: center;">
                                                        <asp:Label ID="lbl_TotalC" runat="server" Font-Bold="True" Text="Comprobado"></asp:Label>
                                                    </td>
                                                    <td style="width: 110px; text-align: center;">
                                                        <asp:Label ID="lbl_TotalS" runat="server" Font-Bold="True" Text="Saldo"></asp:Label>
                                                    </td>
                                                    <td class="auto-style11" rowspan="2" style="text-align: center;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center;">
                                                        <asp:Label ID="lblTotalA" runat="server" Font-Bold="False" Font-Underline="True" Text="0"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:Label ID="lblTotalC" runat="server" Font-Bold="False" Font-Underline="True" Text="0"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:Label ID="lblTotalS" runat="server" Font-Bold="False" Font-Underline="True" Text="0"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="auto-style10" style="text-align: right;">
                                <asp:Label ID="lbl_Evidencia" runat="server" Text="Evidencia:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <table style="width: 85%;">
                                    <tr>

                                        <td>
                                            <asp:GridView ID="gvEvidencias" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="200px">
                                                <Columns>
                                                    <asp:BoundField DataField="id_dt_archivo_comp" HeaderText="id" Visible="true">
                                                        <ItemStyle HorizontalAlign="Center" Width="5px" />
                                                    </asp:BoundField>
                                                    <asp:HyperLinkField DataNavigateUrlFields="ruta" DataTextField="nombre" HeaderText="Nombre" />
                                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" Visible="false">
                                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
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
                                </table>
                                <asp:HyperLink ID="hlEvidencia" runat="server" Width="270px">[hlEvidencia]</asp:HyperLink>
                                <asp:Label ID="lblEvidenciaN" runat="server" Font-Italic="True" Text="No Existe"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 150px; height: 20px; text-align: right">
                                <ig:WebCurrencyEditor ID="wceTotalA" runat="server" Visible="False" Width="40px">
                                </ig:WebCurrencyEditor>
                                <ig:WebCurrencyEditor ID="wceTotalC" runat="server" Visible="False" Width="40px">
                                </ig:WebCurrencyEditor>
                                <ig:WebCurrencyEditor ID="wceTotalS" runat="server" Visible="False" Width="40px">
                                </ig:WebCurrencyEditor>
                            </td>
                            <td class="auto-style10" style="text-align: right;">
                                <asp:Label ID="lbl_ValeI" runat="server" Text="Vale de Ingreso:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <asp:HyperLink ID="hlValeI" runat="server" Width="270px">[hlValeI]</asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlUnidad" runat="server">
                        <table style="width: 1366px;">
                            <tr>
                                <td style="width: 145px; ">&nbsp;</td>
                                <td style="vertical-align: top">
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvUnidad" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="1100px">
                                                    <Columns>
                                                        <asp:BoundField DataField="id_dt_unidad" HeaderText="id_dt_unidad" />
                                                        <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="no_economico" HeaderText="No. Económico">
                                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="status" HeaderText="Estatus">
                                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="tipo" HeaderText="Tipo">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="no_serie" HeaderText="No. Serie">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="modelo" HeaderText="Modelo">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="placas" HeaderText="Placas">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="div" HeaderText="DIV">
                                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="division" HeaderText="División">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="zn" HeaderText="ZN">
                                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="zona" HeaderText="Zona">
                                                        <ItemStyle HorizontalAlign="Center" />
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
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table style="width:1366px;">
                        <tr>
                            <td style="width: 15px; height: 30px; text-align: right">&nbsp;</td>
                            <td>
                                <asp:GridView ID="gvConceptos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" GridLines="Vertical" Width="1325px">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <Columns>
                                        <asp:BoundField DataField="id_dt_factura" HeaderText="id_dt_factura" />
                                        <asp:BoundField DataField="Factura" HeaderText="Factura" />
                                        <asp:BoundField DataField="fecha_realizo" DataFormatString="{0:d}" HeaderText="Fecha de Realización">
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:HyperLinkField DataNavigateUrlFields="path" DataTextField="Factura" HeaderText="Fact.">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                        </asp:HyperLinkField>
                                        <asp:BoundField DataField="Tabulador" HeaderText="Tab.">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre_concepto" HeaderText="Concepto">
                                        <ItemStyle Width="160px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="no_personas" HeaderText="No. Pers.">
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="no_dias" HeaderText="No. Días">
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="no_factura" HeaderText="No. Factura">
                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="monto_subtotal" DataFormatString="{0:c}" HeaderText="Subtotal">
                                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="monto_iva" DataFormatString="{0:c}" HeaderText="IVA">
                                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="monto_total" DataFormatString="{0:c}" HeaderText="Total">
                                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="rfc" HeaderText="RFC">
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="proveedor" HeaderText="Proveedor">
                                        <ItemStyle Width="160px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="origen_destino" HeaderText="Origen - Destino">
                                        <ItemStyle Width="110px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="vehiculo" HeaderText="Vehículo">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="obs" HeaderText="Observaciones" />
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
                </asp:Panel>
            </td>
        </tr>
        </table>
</asp:Content>