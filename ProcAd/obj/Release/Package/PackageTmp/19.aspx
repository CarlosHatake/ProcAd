<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="19.aspx.vb" Inherits="ProcAd._19" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000;">
                <ig:WebScriptManager ID="wsm18" runat="server">
                </ig:WebScriptManager>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsReserv" runat="server" Width="15px" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlSolicitud" runat="server">
                    <table style="width: 100%; height: 25px;">
                        <tr>
                            <td>&nbsp;</td>
                            <td style="width: 150px; text-align: right">
                                <asp:Label ID="lbl_Folio" runat="server" Text="Folio:"></asp:Label>
                            </td>
                            <td style="width: 130px">
                                <asp:Label ID="lblFolio" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; height: 60px;">
                        <tr>
                            <td style="width: 200px; text-align: right">
                                <asp:Label ID="lbl_Solicitante" runat="server" Text="Solicitante:"></asp:Label>
                            </td>
                            <td style="width: 300px">
                                <asp:Label ID="lblSolicitante" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td style="width: 120px; text-align: right">
                                <asp:Label ID="lbl_Autorizador" runat="server" Text="Autorizador:"></asp:Label>
                            </td>
                            <td style="width: 300px">
                                <asp:Label ID="lblAutorizador" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 120px">
                                <asp:Label ID="lbl_Prioridad" runat="server" Text="Prioridad:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPrioridad" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_FechaIni" runat="server" Text="Reservar desde:"></asp:Label>
                            </td>
                            <td>
                                <ig:WebDateTimeEditor ID="wdteFechaIni" runat="server" DisplayModeFormat="G" EditModeFormat="G" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="155px" Enabled="False">
                                </ig:WebDateTimeEditor>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_FechaFin" runat="server" Text="Reservar hasta:"></asp:Label>
                            </td>
                            <td>
                                <ig:WebDateTimeEditor ID="wdteFechaFin" runat="server" DisplayModeFormat="G" EditModeFormat="G" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="155px" Enabled="False">
                                </ig:WebDateTimeEditor>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Vehiculo" runat="server" Text="Vehículo:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblVehiculo" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 200px; height: 30px; text-align: right">
                                <asp:Label ID="lbl_Destino" runat="server" Text="Destino:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDestino" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Just" runat="server" Text="Justificación:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtJust" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="90px" TextMode="MultiLine" Width="1000px" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <table style="width: 100%; height: 50px;">
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnAceptar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Autorizar Reservación" Width="200px" OnClientClick="this.disabled = true;" UseSubmitBehavior="false" OnClick ="btnAceptar_Click" />
                        </td>
                        <td style="text-align: center">
                            <asp:Button ID="btnRechazar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Rechazar Reservación" Width="200px" OnClientClick="this.disabled = true;" UseSubmitBehavior="false" OnClick ="btnRechazar_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
