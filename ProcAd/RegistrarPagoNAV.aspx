<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="RegistrarPagoNAV.aspx.vb" Inherits="ProcAd.RegistrarPagoNAV" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <ig:WebScriptManager ID="wsm43" runat="server">
</ig:WebScriptManager>
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td>
                <asp:Panel runat="server" ID="pnlInicio">
                    <table style="width: 1266px; height: 25px; font-family: Verdana; font-size: 8pt;">
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
                            <td style="text-align: right; width: 130px">
                                <asp:Label ID="lbl_Empresa" runat="server" Text="Empresa:"></asp:Label>
                            </td>
                            <td style="width: 550px">
                                <asp:Label ID="lblEmpresa" runat="server"></asp:Label>                                
                            </td>

                            <td style="text-align:right; width:170px">
                                <asp:Label ID="lbl_Proveedor" runat="server" Text="Proveedor (NAV):"></asp:Label>
                            </td>
                            <td >
                                <asp:Label ID="lblProveedor" runat="server"></asp:Label>                                
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td style="text-align:right">
                                <asp:Label ID="lbl_NumProveedor" runat="server" Text="Numero del Proveedor (NAV):"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblNumProveedor" runat="server"></asp:Label>                                
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right">
                                <asp:Label ID="lbl_Importe" runat="server" Text="Importe requerido:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblImporte" runat="server"></asp:Label>                                
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Justificacion" runat="server" Text="Justificación del anticipo:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblJustificacion" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Adjunto" runat="server" Text="Archivo adjunto:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblAdjunto" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Pedido" runat="server" Text="Pedido de compra: "></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPedido" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_FechaPago" runat="server" Text="Fecha de pago: "></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFechaPago" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1200px; height: 104px;">
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnRegistrar" runat="server" Text="Registrar Pago" Width="200px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            
        </tr>
    </table>
</asp:Content>
