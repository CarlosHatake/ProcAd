<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="66.aspx.vb" Inherits="ProcAd._66" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style5 {
            width: 150px;
            height: 21px;
        }
        .auto-style8 {
            height: 21px;
        }
        .auto-style9 {
            width: 300px;
        }
        .auto-style10 {
            width: 140px;
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1360px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000;">
                <ig:WebScriptManager ID="wsm66" runat="server">
                </ig:WebScriptManager>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsAnticipo" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsInst" runat="server" Width="15px" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlInicio" runat="server">
                    <table style="width: 1360px; height: 25px;">
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
                    <table style="width: 1360px; height: 44px;">
                        <tr>
                            <td style="text-align: right" class="auto-style5">
                                <asp:Label ID="lbl_Solicitante" runat="server" Text="Solicitante:"></asp:Label>
                            </td>
                            <td class="auto-style9">
                                <asp:Label ID="lblSolicitante" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td style="text-align: right" class="auto-style10">
                                <asp:Label ID="lbl_NoProveedor" runat="server" Text="Num. Proveedor:"></asp:Label>
                            </td>
                            <td class="auto-style9">
                                <asp:Label ID="lblNoProveedor" runat="server" ForeColor="Blue" Width="150px"></asp:Label>
                            </td>
                            <td style="text-align: right; " class="auto-style10">
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
                    </table>
                    <table style="width: 1360px;">
                        <tr>
                            <td style="width: 150px; height: 30px; text-align: right">
                                <asp:Label ID="lbl_Act" runat="server" Text="Actividad:"></asp:Label>
                            </td>
                            <td style="width: 747px">
                                <asp:TextBox ID="txtAct" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="43px" TextMode="MultiLine" Width="735px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="auto-style10" style="text-align: right;">
                                <asp:Label ID="lbl_NoPersonas" runat="server" Text="Cantidad de Personas:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <ig:WebNumericEditor ID="wneNoPersonas" runat="server" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="70px" ReadOnly="True">
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
                                <asp:Label ID="lbl_BuenViaje" runat="server" Font-Bold="True" Font-Size="18pt" Text="Buen  Viaje"></asp:Label>
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
                                <asp:Label ID="lbl_Hospedaje" runat="server" Text="Hospedaje:"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <ig:WebNumericEditor ID="wneDiasH" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px" ReadOnly="True">
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
                                <ig:WebNumericEditor ID="wneDiasA" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px" ReadOnly="True">
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
                                <ig:WebNumericEditor ID="wneDiasC" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px" ReadOnly="True">
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
                                <ig:WebNumericEditor ID="wneDiasO" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px" ReadOnly="True">
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
                    <table style="width: 1360px; height: 50px;">
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnAceptar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Anticipo AE Registrado" Width="200px" OnClientClick="this.disabled = true; this.value='Procesando…';" UseSubmitBehavior="false" OnClick ="btnAceptar_Click" />
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
