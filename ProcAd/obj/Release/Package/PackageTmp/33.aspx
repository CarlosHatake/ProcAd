<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="33.aspx.vb" Inherits="ProcAd._33" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style5 {
            width: 150px;
        }
        .auto-style9 {
            width: 300px;
        }
        .auto-style10 {
            width: 140px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1360px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000;">
                <ig:WebScriptManager ID="wsm33" runat="server">
                </ig:WebScriptManager>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdDtCargaComb" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBloqueoST" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtConductor" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtRendimiento" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtRendimientoReal" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtTolerancia" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtDesvReal" runat="server" Width="15px" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlInicio" runat="server">
                    <table style="width: 1360px; height: 75px;">
                        <tr>
                            <td style="text-align: right" class="auto-style5">
                                <asp:Label ID="lbl_Unidad" runat="server" Text="Unidad:"></asp:Label>
                            </td>
                            <td class="auto-style9">
                                <asp:Label ID="lblUnidad" runat="server" ForeColor="Blue" Width="150px"></asp:Label>
                            </td>
                            <td style="text-align: right" class="auto-style10">
                                <asp:Label ID="lbl_Placa" runat="server" Text="Placa:"></asp:Label>
                            </td>
                            <td class="auto-style9">
                                <asp:Label ID="lblPlaca" runat="server" ForeColor="Blue" Width="150px"></asp:Label>
                            </td>
                            <td style="text-align: right; " class="auto-style10">
                                <asp:Label ID="lbl_NoTarjeta" runat="server" Text="No. Tarjeta:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblNoTarjeta" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Marca" runat="server" Text="Marca:"></asp:Label>
                            </td>
                            <td class="auto-style9">
                                <asp:Label ID="lblMarca" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                            <td class="auto-style10" style="text-align: right">
                                <asp:Label ID="lbl_Modelo" runat="server" Text="Modelo:"></asp:Label>
                            </td>
                            <td class="auto-style9">
                                <asp:Label ID="lblModelo" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                            <td class="auto-style10" style="text-align: right; ">
                                <asp:Label ID="lbl_Fecha" runat="server" Text="Fecha de Carga:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFecha" runat="server" Font-Bold="False" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Estacion" runat="server" Text="Estación:"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblEstacion" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_NoTicket" runat="server" Text="No. Ticket:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblNoTicket" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1360px; height: 50px;">
                        <tr>
                            <td style="width: 150px; text-align: right">
                                <asp:Label ID="lbl_Litros" runat="server" Text="Litros:"></asp:Label>
                            </td>
                            <td style="width: 130px">
                                <ig:WebNumericEditor ID="wneLitros" runat="server" Font-Names="Verdana" Font-Size="8pt" ReadOnly="True" Width="70px">
                                </ig:WebNumericEditor>
                            </td>
                            <td style="text-align: right; width: 120px;">
                                <asp:Label ID="lbl_Precio" runat="server" Text="Precio:"></asp:Label>
                            </td>
                            <td style="width: 130px;">
                                <ig:WebCurrencyEditor ID="wcePrecio" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="80px" ReadOnly="True">
                                </ig:WebCurrencyEditor>
                            </td>
                            <td style="width: 120px; text-align: right">
                                <asp:Label ID="lbl_Subtotal" runat="server" Text="Subtotal:"></asp:Label>
                            </td>
                            <td style="width: 130px;">
                                <ig:WebCurrencyEditor ID="wceSubtotal" runat="server" Font-Names="Verdana" Font-Size="8pt" ReadOnly="True" Width="110px">
                                </ig:WebCurrencyEditor>
                            </td>
                            <td style="width: 120px; text-align: right">
                                <asp:Label ID="lbl_IVA" runat="server" Text="IVA:"></asp:Label>
                            </td>
                            <td style="width: 130px;">
                                <ig:WebCurrencyEditor ID="wceIVA" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="80px" ReadOnly="True">
                                </ig:WebCurrencyEditor>
                            </td>
                            <td style="width: 120px; text-align: right">
                                <asp:Label ID="lbl_Total" runat="server" Text="Total:"></asp:Label>
                            </td>
                            <td>
                                <ig:WebCurrencyEditor ID="wceTotal" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="110px" ReadOnly="True">
                                </ig:WebCurrencyEditor>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 150px; text-align: right">
                                <asp:Label ID="lbl_OdometroE" runat="server" Text="Odómetro Edenred:"></asp:Label>
                            </td>
                            <td style="width: 130px">
                                <ig:WebNumericEditor ID="wneOdometroE" runat="server" Font-Names="Verdana" Font-Size="8pt" ReadOnly="True" Width="90px">
                                </ig:WebNumericEditor>
                            </td>
                            <td style="text-align: right; width: 120px;">
                                <asp:Label ID="lbl_OdometroU" runat="server" Text="Odómetro Usuario:"></asp:Label>
                            </td>
                            <td style="width: 130px;">
                                <ig:WebNumericEditor ID="wneOdometroU" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="90px">
                                </ig:WebNumericEditor>
                            </td>
                            <td style="width: 120px; text-align: right">
                                <asp:Label ID="lbl_OdometroF" runat="server" Text="Foto Odómetro:"></asp:Label>
                            </td>
                            <td colspan="5">
                                <asp:FileUpload ID="fuOdometroF" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="520px" />
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1360px; height: 50px;">
                        <tr>
                            <td style="text-align: right; width: 150px;">
                                <asp:Label ID="lbl_Obs" runat="server" Text="Movimientos:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtObs" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="61px" TextMode="MultiLine" Width="888px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1360px; height: 50px;">
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnAceptar" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnAceptar_Click" OnClientClick="this.disabled = true; this.value='Procesando…';" Text="Registrar" UseSubmitBehavior="false" Width="200px" />
                                <asp:Button ID="btnConfirmar" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnConfirmar_Click" OnClientClick="this.disabled = true; this.value='Procesando…';" Text="Confirmar" UseSubmitBehavior="false" Width="200px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
