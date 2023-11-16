<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ConsSolRecDet.aspx.vb" Inherits="ProcAd.ConsSolRecDet" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style5 {
            width: 142px;
        }
        .auto-style46 {
            width: 483px;
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
                                    <asp:UpdatePanel ID="upCbEmpresa" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbEmpresa" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Empresa:" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="text-align: left; width: 340px;">
                                    <asp:UpdatePanel ID="upEmpresa" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlEmpresa" runat="server" Width="330px">
                                                <asp:DropDownList ID="ddlEmpresa" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="200px">
                                                </asp:DropDownList>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="text-align: left; width: 120px;">
                                    <asp:UpdatePanel ID="upCbSolicitante" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbSolicitante" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Solicitante:" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upSolicitante" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlSolicitante" runat="server" Width="330px">
                                                <asp:DropDownList ID="ddlSolicitante" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px">
                                                </asp:DropDownList>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: right; width: 80px;">&nbsp;</td>
                            <td style="text-align: left; width: 120px;">
                                <asp:UpdatePanel ID="upCbFechaC" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="cbFechaC" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Fecha Creación:" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="text-align: left; width: 340px;">
                                <asp:UpdatePanel ID="upFechaC" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="text-align: left; width: 120px;">
                                <asp:UpdatePanel ID="upCbAutorizador" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="cbAutorizador" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Autorizador:" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="width: 340px">
                                <asp:UpdatePanel ID="upAutorizador" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlAutorizador" runat="server" Width="330px">
                                            <asp:DropDownList ID="ddlAutorizador" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="width: 210px">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">&nbsp;</td>
                            <td style="text-align: left">
                                <asp:UpdatePanel ID="upCbStatus" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="cbStatus" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Estado:" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="upStatus" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlStatus" runat="server" Width="330px">
                                            <asp:DropDownList ID="ddlStatus" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="180px">
                                                <asp:ListItem Value="'P'">Pendiente</asp:ListItem>
                                                <asp:ListItem Value="'A'">Autorizado</asp:ListItem>
                                                <asp:ListItem Value="'Z'">Rechazado</asp:ListItem>
                                                <asp:ListItem Value="'P','A','Z'">Todos</asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="upCbNoSolRec" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="cbNoSolRec" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="No. de Sol. Rec.:" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="upNoSolRec" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlNoSolRec" runat="server" Width="150px">
                                            <asp:TextBox ID="txtNoSolRec" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="text-align: right">
                                <asp:UpdatePanel ID="upCbNoSolRec0" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="cbHospVehAvion" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Hospedaje, Vehículo o Avión" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                &nbsp;</td>
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
                                        <td style="width: 1px">&nbsp;</td>
                                        <td>
                                            <asp:GridView ID="gvRegistros" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="2580px">
                                                <Columns>
                                                    <asp:BoundField DataField="Folio" HeaderText="No. Sol. Recursos">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Empresa" HeaderText="Empresa">
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Solicitó" HeaderText="Solicita">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Autorizó" HeaderText="Autoriza" />
                                                    <asp:BoundField DataField="Fecha de Solicitud" HeaderText="Fecha Solicita">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Fecha de Autorización" HeaderText="Fecha Autoriza">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Origen" HeaderText="Origen"></asp:BoundField>
                                                    <asp:BoundField DataField="Destino" HeaderText="Destino"></asp:BoundField>
                                                    <asp:BoundField DataField="Lugar" HeaderText="Lugar"></asp:BoundField>
                                                    <asp:BoundField DataField="Desde" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Desde">
                                                    <ItemStyle HorizontalAlign="Center" Width="85px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Hasta" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Hasta">
                                                    <ItemStyle HorizontalAlign="Center" Width="85px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Actividad" HeaderText="Actividad"></asp:BoundField>
                                                    <asp:BoundField DataField="No. Anticipo" HeaderText="Folio Anticipo">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Noches Hotel" HeaderText="Noches Hotel">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Monto Hospedaje" DataFormatString="{0:c}" HeaderText="Monto Hospedaje">
                                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="No. Reserva Vehículo" HeaderText="Folio Reserva">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="No. Económico" HeaderText="No. Económico">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Tipo Vehículo" HeaderText="Tipo Vehículo">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Lugares Disponibles" HeaderText="Lugares Disponibles">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Lugares Requeridos" HeaderText="Lugares Requeridos">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="No. Comb" HeaderText="Folio Combustible">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Reserva de Avión" HeaderText="Reserva de Avión">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Fuera de Tiempo" HeaderText="Fuera de Tiempo">
                                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Justificación" HeaderText="Justificación" />
                                                    <asp:BoundField DataField="Tabulador" HeaderText="Tabulador">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Estatus" HeaderText="Estatus">
                                                    <ItemStyle HorizontalAlign="Center" Width="110px" />
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