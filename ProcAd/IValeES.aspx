<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="IValeES.aspx.vb" Inherits="ProcAd.IValeES" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vale de Entrada y/o Salida de Unidades</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width: 680px; position: absolute; top: 0px; left: 0px; font-family: Verdana; font-size: 8pt; text-align: center;">
            <tr>
                <td>
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: center; width: 80px">
                                <asp:Image ID="imgLogo" runat="server" ImageUrl="images\logo.jpg" Width="50px" />
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="lbl_Titulo" runat="server" Font-Bold="True" Font-Size="10pt" Text="Vale de Entrada y/o Salida de Unidades"></asp:Label>
                            </td>
                            <td style="width: 90px">
                                <table style="width: 100%; font-size: 6pt; text-align: right;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_Formato1" runat="server" Text="Clave: Fr 14.5 1-F2"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_Formato2" runat="server" Text="Versión: 4.0"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_Formato3" runat="server" Text="Fecha: 21/11/13"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px; width: 25%; height: 18px">
                                <asp:Label ID="lbl_Unidad" runat="server" Font-Bold="False" Text="Unidad"></asp:Label>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px; width: 25%;">
                                <asp:Label ID="lbl_Placas" runat="server" Font-Bold="False" Text="Placas"></asp:Label>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px; width: 25%;">
                                <asp:Label ID="lbl_TarComb" runat="server" Font-Bold="False" Text="Tarjeta de Combustible"></asp:Label>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px">
                                <asp:Label ID="lbl_TarCirc" runat="server" Font-Bold="False" Text="Tarjeta de Circulación"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px; height: 18px">
                                <asp:Label ID="lblUnidad" runat="server" Font-Bold="False" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px">
                                <asp:Label ID="lblPlacas" runat="server" Font-Bold="False" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px; text-align: center;">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width: 20px">&nbsp;</td>
                                        <td>
                                <asp:RadioButtonList ID="rblTarComb" runat="server" RepeatColumns="2" Width="120px">
                                    <asp:ListItem>Sí</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:RadioButtonList>
                                        </td>
                                        <td style="width: 20px">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px; text-align: center;">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width: 20px">&nbsp;</td>
                                        <td>
                                <asp:RadioButtonList ID="rblTarCirc" runat="server" RepeatColumns="2" Width="120px">
                                    <asp:ListItem>Sí</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:RadioButtonList>
                                        </td>
                                        <td style="width: 20px">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td>
                                <asp:Label ID="lbl_CondSalida" runat="server" Font-Bold="True" Text="CONDICIONES DE SALIDA DEL VEHÍCULO"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%;">
                        <tr>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px; width: 25%; height: 18px;">
                                <asp:Label ID="lbl_Fecha" runat="server" Font-Bold="False" Text="Fecha"></asp:Label>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px; width: 25%;">
                                <asp:Label ID="lbl_NoEco" runat="server" Font-Bold="False" Text="Número Económico"></asp:Label>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px; width: 25%;">
                                <asp:Label ID="lbl_Kms" runat="server" Font-Bold="False" Text="Kilómetros"></asp:Label>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px">
                                <asp:Label ID="lbl_Hora" runat="server" Font-Bold="False" Text="Hora"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px; height: 18px">
                                <asp:Label ID="lblFecha" runat="server" Font-Bold="False" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px">
                                <asp:Label ID="lblNoEco" runat="server" Font-Bold="False" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px">&nbsp;</td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px">
                                <asp:Label ID="lbl_ExtLimp" runat="server" Font-Bold="False" Text="Limpio Exterior:"></asp:Label>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width: 20px">&nbsp;</td>
                                        <td>
                                <asp:RadioButtonList ID="rblExtLimp" runat="server" RepeatColumns="2" Width="120px">
                                    <asp:ListItem>Sí</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:RadioButtonList>
                                        </td>
                                        <td style="width: 20px">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px">
                                <asp:Label ID="lbl_IntLimp" runat="server" Font-Bold="False" Text="Limpio Interior:"></asp:Label>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width: 20px">&nbsp;</td>
                                        <td>
                                <asp:RadioButtonList ID="rblIntLimp" runat="server" RepeatColumns="2" Width="120px">
                                    <asp:ListItem>Sí</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:RadioButtonList>
                                        </td>
                                        <td style="width: 20px">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="lbl_CondSalida2" runat="server" Font-Bold="False" Text="Describa las condiciones y accesorios de salida del vehículo."></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 25%">
                                <asp:Image ID="imgVentoRS" runat="server" ImageUrl="images\Vento Right.png" Width="150px" />
                            </td>
                            <td style="width: 25%">
                                <asp:Image ID="imgVentoLS" runat="server" ImageUrl="images\Vento Left.png" Width="150px" />
                            </td>
                            <td style="width: 25%">
                                <asp:Image ID="imgSilveradoRS" runat="server" ImageUrl="images\Silverado Right.png" Width="150px" />
                            </td>
                            <td>
                                <asp:Image ID="imgSilveradoLS" runat="server" ImageUrl="images\Silverado Left.png" Width="150px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Image ID="imgTanqueS" runat="server" ImageUrl="images\Tanque.png" Width="150px" />
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: center">
                                <asp:Label ID="lbl_DañosS" runat="server" Font-Bold="True" Text="MARQUE LAS ÁREAS DAÑADAS DEL VEHÍCULO"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:RadioButtonList ID="rblDaños1S" runat="server" RepeatColumns="6" Width="665px">
                                    <asp:ListItem>a) Cristal Roto</asp:ListItem>
                                    <asp:ListItem>b) Golpe</asp:ListItem>
                                    <asp:ListItem>c) Rayón</asp:ListItem>
                                    <asp:ListItem>d) Abolladura</asp:ListItem>
                                    <asp:ListItem>e) Calavera Rota</asp:ListItem>
                                    <asp:ListItem>f) Espejo Roto</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width: 80px">
                                <asp:RadioButtonList ID="rblDaños2S" runat="server" RepeatColumns="1" Width="80px">
                                    <asp:ListItem>g) Otros</asp:ListItem>
                                </asp:RadioButtonList>
                                        </td>
                                        <td>
                                <asp:Label ID="lbl_OtrosS" runat="server" Font-Bold="False" Text="_________________________________________________"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%;">
                        <tr>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px; width: 33%; height: 18px;">
                                <asp:Label ID="lbl_Conductor1" runat="server" Font-Bold="False" Text="Conductor"></asp:Label>
                                <asp:Label ID="lbl_Conductor2" runat="server" Font-Bold="False" Text="(Nombre y Firma)" Font-Size="6pt"></asp:Label>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px; width: 33%;">
                                <asp:Label ID="lbl_Vigilante1" runat="server" Font-Bold="False" Text="Vigilante"></asp:Label>
                                <asp:Label ID="lbl_Vigilante2" runat="server" Font-Bold="False" Text="(Nombre y Firma)" Font-Size="6pt"></asp:Label>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px; ">
                                <asp:Label ID="lbl_RespUnidad1" runat="server" Font-Bold="False" Text="Responsable de la Unidad"></asp:Label>
                                <asp:Label ID="lbl_RespUnidad2" runat="server" Font-Bold="False" Text="(Nombre y Firma)" Font-Size="6pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px; height: 18px">
                                <asp:Label ID="lblConductor" runat="server" Font-Bold="False" ForeColor="Blue" Width="190px"></asp:Label>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px; height: 50px;">
                                &nbsp;</td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px">
                                <asp:Label ID="lblRespUnidad" runat="server" Font-Bold="False" ForeColor="Blue" Width="190px"></asp:Label>
                            </td>
                        </tr>
                        </table>
                    <table style="width:100%;">
                        <tr>
                            <td>
                                <asp:Label ID="lbl_CondEntrada" runat="server" Font-Bold="True" Text="CONDICIONES DE ENTRADA DEL VEHÍCULO"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%;">
                        <tr>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px; width: 25%; height: 18px;">
                                <asp:Label ID="lbl_Fecha0" runat="server" Font-Bold="False" Text="Fecha Agendada"></asp:Label>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px; width: 25%;">
                                <asp:Label ID="lbl_Hora0" runat="server" Font-Bold="False" Text="Fecha Real"></asp:Label>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px; " colspan="2">
                                <asp:Label ID="lbl_Kms0" runat="server" Font-Bold="False" Text="Kilómetros"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px; height: 18px">
                                <asp:Label ID="lblFechaE" runat="server" Font-Bold="False" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px">
                                &nbsp;</td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px" colspan="2">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px">
                                <asp:Label ID="lbl_ExtLimp0" runat="server" Font-Bold="False" Text="Limpio Exterior:"></asp:Label>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width: 20px">&nbsp;</td>
                                        <td>
                                <asp:RadioButtonList ID="rblExtLimp0" runat="server" RepeatColumns="2" Width="120px">
                                    <asp:ListItem>Sí</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:RadioButtonList>
                                        </td>
                                        <td style="width: 20px">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px; width: 25%;">
                                <asp:Label ID="lbl_IntLimp0" runat="server" Font-Bold="False" Text="Limpio Interior:"></asp:Label>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width: 20px">&nbsp;</td>
                                        <td>
                                <asp:RadioButtonList ID="rblIntLimp0" runat="server" RepeatColumns="2" Width="120px">
                                    <asp:ListItem>Sí</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:RadioButtonList>
                                        </td>
                                        <td style="width: 20px">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="lbl_CondEntrada2" runat="server" Font-Bold="False" Text="Describa las condiciones y accesorios de entrada del vehículo."></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 25%">
                                <asp:Image ID="imgVentoRE" runat="server" ImageUrl="images\Vento Right.png" Width="150px" />
                            </td>
                            <td style="width: 25%">
                                <asp:Image ID="imgVentoLE" runat="server" ImageUrl="images\Vento Left.png" Width="150px" />
                            </td>
                            <td style="width: 25%">
                                <asp:Image ID="imgSilveradoRE" runat="server" ImageUrl="images\Silverado Right.png" Width="150px" />
                            </td>
                            <td>
                                <asp:Image ID="imgSilveradoLE" runat="server" ImageUrl="images\Silverado Left.png" Width="150px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Image ID="imgTanqueE" runat="server" ImageUrl="images\Tanque.png" Width="150px" />
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: center">
                                <asp:Label ID="lbl_DañosE" runat="server" Font-Bold="True" Text="MARQUE LAS ÁREAS DAÑADAS DEL VEHÍCULO"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:RadioButtonList ID="rblDaños1E" runat="server" RepeatColumns="6" Width="665px">
                                    <asp:ListItem>a) Cristal Roto</asp:ListItem>
                                    <asp:ListItem>b) Golpe</asp:ListItem>
                                    <asp:ListItem>c) Rayón</asp:ListItem>
                                    <asp:ListItem>d) Abolladura</asp:ListItem>
                                    <asp:ListItem>e) Calavera Rota</asp:ListItem>
                                    <asp:ListItem>f) Espejo Roto</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width: 80px">
                                <asp:RadioButtonList ID="rblDaños2E" runat="server" RepeatColumns="1" Width="80px">
                                    <asp:ListItem>g) Otros</asp:ListItem>
                                </asp:RadioButtonList>
                                        </td>
                                        <td>
                                <asp:Label ID="lbl_OtrosE" runat="server" Font-Bold="False" Text="_________________________________________________"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%;">
                        <tr>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px; width: 50%; height: 18px;">
                                <asp:Label ID="lbl_Entrega1" runat="server" Font-Bold="False" Text="Entrega"></asp:Label>
                                <asp:Label ID="lbl_Entrega2" runat="server" Font-Bold="False" Text="(Nombre y Firma)" Font-Size="6pt"></asp:Label>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px; ">
                                <asp:Label ID="lbl_Vigilante3" runat="server" Font-Bold="False" Text="Vigilancia"></asp:Label>
                                <asp:Label ID="lbl_Vigilante4" runat="server" Font-Bold="False" Text="(Nombre y Firma)" Font-Size="6pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px; height: 18px">
                                <asp:Label ID="lblEntrega" runat="server" Font-Bold="False" ForeColor="Blue" Width="300px"></asp:Label>
                            </td>
                            <td style="border: thin solid #808080; padding: 0px; margin: 0px; height: 50px;">
                                &nbsp;</td>
                        </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td style="color: #FF0000">
                    <asp:Literal ID="litError" runat="server"></asp:Literal>
                    <asp:TextBox ID="_txtFolio" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="20px" Visible="False"></asp:TextBox>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
