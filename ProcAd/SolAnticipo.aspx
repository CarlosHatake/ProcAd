<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="SolAnticipo.aspx.vb" Inherits="ProcAd.SolAnticipo" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
     
     .auto-style8 {
         height: 21px;
     }
     .auto-style11 {
         width: 300px;
         height: 21px;
     }
 </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <ig:WebScriptManager ID="wsm43" runat="server">
 </ig:WebScriptManager>
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td>
                <asp:Panel ID="pnlInicio" runat="server">
                    <table style="width: 1366px; height: 25px; font-family: Verdana; font-size: 8pt;">
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
                    <table style="width: 1366px; height: 44px;">
                        <tr>
                            <td style="text-align: right; width: 260px">
                                <asp:Label ID="lbl_Empresa" runat="server" Text="Empresa:"></asp:Label>
                            </td>
                            <td style="width: 520px">
                                <asp:UpdatePanel ID="upEmpresa" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlEmpresa" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="180px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>

                            <td style="text-align: right; width: 50px">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="height: 20px">
                                            <asp:Label ID="lbl_Proveedor" runat="server" Text="Proveedor:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 20px">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                            <td class="auto-style8">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TextBox1" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="200px"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" AutoPostBack="True" Height="16px" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="280px">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lblAutorizador" runat="server" Text="Autorizador:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAutorizador" runat="server" Width="180px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; height: 45px">
                                <asp:Label ID="lbl_Opcion" runat="server" Text="Seleccione una opción:"></asp:Label>
                            </td>
                            <td>
                                <table style="width: 100px">
                                    <tr>
                                        <td>
                                            <asp:CheckBoxList runat="server" ID="cblTipos" RepeatColumns="4" Width="550px">
                                                <asp:ListItem>Anticipo</asp:ListItem>
                                                <asp:ListItem>Pago Anticipado</asp:ListItem>
                                                <asp:ListItem>Pago Anticipado Agente Aduanal</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Importe" Text="Importe requerido: " runat="server"></asp:Label>
                            </td>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtImporte" Width="150px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Divisa" Text="Divisa: " runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDivisa" runat="server" Width="180px"></asp:DropDownList>

                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <asp:Label ID="lbl_PedidosCompra" Text="Pedidos de compra: " runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPedidosCompra" runat="server" Width="180px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Justificacion" runat="server" Text="Justificación del anticipo: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtJustificacion" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="63px" TextMode="MultiLine" Width="535px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Adjuntos" runat="server" Text="Archivo Adjunto: "></asp:Label>
                            </td>
                            <td>
                                <asp:FileUpload ID="fuAdjunto" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px" />

                            </td>
                        </tr>
                    </table>
                    <table style="width:1366px; height:44px;">
                        <tr>
                            <td style="text-align:center">
                                  <asp:Button ID="btnAutorizar" runat="server" Text="Enviar a Autorización" Width="200px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
