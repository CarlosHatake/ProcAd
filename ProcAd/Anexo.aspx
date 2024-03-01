<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="Anexo.aspx.vb" Inherits="ProcAd.Anexo" %>
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
                                <asp:Label ID="lbl_RFC" runat="server" Text="RFC:"></asp:Label>
                            </td>
                            <td style="width: 520px">
                                <asp:TextBox ID="txtRFC" runat="server" Width="150px"></asp:TextBox>
                            </td>

                            <td style="text-align: right; width: 50px">
                                <asp:Label ID="lbl_Division" runat="server" Text="División:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="280px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lblTipoArrendamiento" runat="server" Text="Tipo Arrendamiento:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAutorizador" runat="server" Width="180px"></asp:DropDownList>
                            </td>
                            <td style="text-align:left">
                                <asp:Label ID="lbl_Zona" runat="server" Text="Zona: "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlZona" runat="server" Width="280px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; height: 45px">
                                <asp:Label ID="lbl_TipoUnidad" runat="server" Text="Tipo Unidad:"></asp:Label>
                            </td>
                            <td>
                              <asp:DropDownList ID="ddlTipoUnidad" runat="server" Width="170px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_NoPago" Text="No.Pago: " runat="server"></asp:Label>
                            </td>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtImporte" Width="150px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_PagRegistrado" Text="Pago Registrado: " runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPagRegistrado" runat="server" Width="150px"></asp:TextBox>

                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <asp:Label ID="lbl_Vigencia" Text="Vigencia: " runat="server"></asp:Label>
                            </td>
                            <td>
                                <!--  
                                <ig:WebDateTimeEditor ID="wdteFechaPago" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="75px">
                                </ig:WebDateTimeEditor>
                                -->
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
                                  <asp:Button ID="btnGuardar" runat="server" Text="Guardar" Width="200px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>

