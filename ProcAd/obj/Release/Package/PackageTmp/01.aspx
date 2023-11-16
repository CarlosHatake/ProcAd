<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="01.aspx.vb" Inherits="ProcAd._01" %>
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
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000;">
                <ig:WebScriptManager ID="wsm01" runat="server">
                </ig:WebScriptManager>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
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
                                <asp:DropDownList ID="ddlEmpresa" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="180px" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Autorizador" runat="server" Text="Autorizador:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAutorizador" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="280px">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Periodo" runat="server" Text="Periodo de Comisión:"></asp:Label>
                            </td>
                            <td>
                                <ig:WebDateTimeEditor ID="wdteFechaIni" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Width="90px" Nullable="False">
                                </ig:WebDateTimeEditor>
                                &nbsp;
                                <asp:Label ID="lbl_PeriodoA" runat="server" Text="a"></asp:Label>
                                &nbsp;<ig:WebDateTimeEditor ID="wdteFechaFin" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Width="90px" Nullable="False">
                                </ig:WebDateTimeEditor>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Destino" runat="server" Text="Destino:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDestino" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="280px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1360px;">
                        <tr>
                            <td style="width: 150px; height: 30px; text-align: right">
                                <asp:Label ID="lbl_Act" runat="server" Text="Actividad:"></asp:Label>
                            </td>
                            <td style="width: 747px">
                                <asp:TextBox ID="txtAct" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="43px" TextMode="MultiLine" Width="735px"></asp:TextBox>
                            </td>
                            <td class="auto-style10" style="text-align: right;">
                                <asp:Label ID="lbl_NoPersonas" runat="server" Text="Cantidad de Personas:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <ig:WebNumericEditor ID="wneNoPersonas" runat="server" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="70px" DataMode="Int" MinValue="1">
                                </ig:WebNumericEditor>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1360px; height: 120px;">
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
                                <asp:RadioButtonList ID="rblTipoPago" runat="server" RepeatColumns="2" Width="250px" Enabled="False">
                                    <asp:ListItem Value="E">Efecitvo</asp:ListItem>
                                    <asp:ListItem Value="T">Transferencia</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Hospedaje" runat="server" Text="Hospedaje:"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <ig:WebNumericEditor ID="wneDiasH" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px" DataMode="Int" MinValue="1">
                                </ig:WebNumericEditor>
                            </td>
                            <td style="text-align: center">
                                <ig:WebCurrencyEditor ID="wceMontoH" runat="server" MinValue="1">
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
                                <ig:WebNumericEditor ID="wneDiasA" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px" DataMode="Int" MinValue="1">
                                </ig:WebNumericEditor>
                            </td>
                            <td style="text-align: center">
                                <ig:WebCurrencyEditor ID="wceMontoA" runat="server" MinValue="1">
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
                                <ig:WebNumericEditor ID="wneDiasC" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px" DataMode="Int" MinValue="1">
                                </ig:WebNumericEditor>
                            </td>
                            <td style="text-align: center">
                                <ig:WebCurrencyEditor ID="wceMontoC" runat="server" MinValue="1">
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
                                <ig:WebNumericEditor ID="wneDiasO" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px" DataMode="Int" MinValue="1">
                                </ig:WebNumericEditor>
                            </td>
                            <td style="text-align: center">
                                <ig:WebCurrencyEditor ID="wceMontoO" runat="server" MinValue="1">
                                </ig:WebCurrencyEditor>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtOtros" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="400px"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_MontoT" runat="server" Text="Monto Solicitado:"></asp:Label>
                            </td>
                            <td style="text-align: center">&nbsp;</td>
                            <td style="text-align: center">
                                <ig:WebCurrencyEditor ID="wceMontoT" runat="server" Nullable="False" ReadOnly="True" MinValue="1">
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
                                <asp:Button ID="btnSumar" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnSumar_Click" OnClientClick="this.disabled = true; this.value='Procesando…';" Text="Sumar" UseSubmitBehavior="false" Width="200px" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button ID="btnEnviar" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnEnviar_Click" OnClientClick="this.disabled = true; this.value='Procesando…';" Text="Enviar a Aprobación" UseSubmitBehavior="false" Width="200px" />
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
