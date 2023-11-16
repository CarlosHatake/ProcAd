<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Encuesta.aspx.vb" Inherits="ProcAd.Encuesta" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link rel="shortcut icon" href="images\ProcAd.ico" />
    <title> Encuesta ProcAd </title>

	<script>
	    function nobackbutton() {
	        window.location.hash = "no-back-button";
	        window.location.hash = "Again-No-back-button" //chrome
	        window.onhashchange = function () { window.location.hash = "no-back-button"; }
	    }
	</script>
    <style type="text/css">

        .auto-style5 {
            width: 120px;
            height: 21px;
        }
        

        .auto-style9 {
            width: 300px;
        }
        

        .auto-style10 {
            width: 140px;
            height: 21px;
        }
    

        .auto-style8 {
            height: 21px;
        }
        </style>
</head>
<body onload="nobackbutton();">
    <form id="form1" runat="server">
    <div>
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt; z-index:2;">
        <tr>
            <td style="text-align: center; font-family: Verdana; font-size: 9px; ">
                <asp:Image ID="imgTitulo" runat="server" ImageAlign="Middle" ImageUrl="images\bannerT2.png" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center; font-family: Verdana; font-size: 8pt; color: #FF0000; font-weight: bold; ">
                <asp:Literal ID="litError" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; font-family: Verdana; font-size: 8pt; ">

                <asp:Panel ID="pnlSolRec" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td style="width: 150px; text-align: right;">
                                <asp:Label ID="lbl_Folio" runat="server" Text="No. Sol. Recursos:"></asp:Label>
                            </td>
                            <td>
                                <table style="width:100%;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblFolio" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                            <asp:Label ID="lblID" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            &nbsp;</td>
                                        <td style="width: 450px">
                                            <ig:WebScriptManager ID="wsmEncuesta" runat="server">
                                            </ig:WebScriptManager>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1360px; height: 22px;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Solicitante" runat="server" Text="Solicitante:"></asp:Label>
                            </td>
                            <td class="auto-style9">
                                <asp:Label ID="lblSolicitante" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 100px; height: 21px;">
                                <asp:Label ID="lbl_NoProveedor" runat="server" Text="Num. Proveedor:"></asp:Label>
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="lblNoProveedor" runat="server" ForeColor="Blue" Width="150px"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 70px; height: 21px;">
                                <asp:Label ID="lbl_Empresa" runat="server" Text="Empresa:"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label ID="lblEmpresa" runat="server" ForeColor="Blue" Width="150px"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 90px; height: 21px;">
                                <asp:Label ID="lbl_Autorizador" runat="server" Text="Autorizador:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblAutorizador" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1360px; height: 22px;">
                        <tr>
                            <td style="text-align: right; width: 120px; height: 21px;">
                                <asp:Label ID="lbl_Orig" runat="server" Text="Origen:"></asp:Label>
                            </td>
                            <td style="width: 220px;">
                                <asp:Label ID="lblOrig" runat="server" ForeColor="Blue" Width="200px"></asp:Label>
                            </td>
                            <td style="width: 60px; height: 21px; text-align: right">
                                <asp:Label ID="lbl_Dest" runat="server" Text="Destino:"></asp:Label>
                            </td>
                            <td style="width: 240px;">
                                <asp:Label ID="lblDest" runat="server" ForeColor="Blue" Width="200px"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 180px; height: 21px;">
                                <asp:CheckBoxList ID="cblMovLocales" runat="server" Enabled="False" RepeatColumns="1">
                                    <asp:ListItem>Movimientos Locales</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <td style="text-align: right; width: 60px; height: 21px;">
                                <asp:Label ID="lbl_Destino0" runat="server" Text="Lugar:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDestino" runat="server" ForeColor="Blue" Width="380px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1360px;">
                        <tr>
                            <td rowspan="3" style="width: 120px; text-align: right">
                                <asp:Label ID="lbl_Just" runat="server" Text="Justificación:"></asp:Label>
                            </td>
                            <td rowspan="3" style="width: 747px">
                                <asp:TextBox ID="txtJust" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="61px" ReadOnly="True" TextMode="MultiLine" Width="735px"></asp:TextBox>
                            </td>
                            <td class="auto-style10" style="text-align: right;">
                                <asp:Label ID="lbl_PeriodoIni" runat="server" Text="Periodo de Comisión:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <table style="width:200px;">
                                    <tr>
                                        <td style="width: 70px">
                                            <asp:Label ID="lblPeriodoIni" runat="server" ForeColor="Blue">00/00/0000</asp:Label>
                                        </td>
                                        <td style="width: 12px; text-align: left;">
                                            <asp:Label ID="lbl_PeriodoA" runat="server" Text="a"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPeriodoFin" runat="server" ForeColor="Blue">00/00/0000</asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style10" style="text-align: right;">
                                <asp:Label ID="lbl_Recursos" runat="server" Text="Recursos Requeridos:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <asp:UpdatePanel ID="upRecursos" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBoxList ID="cblRecursos" runat="server" AutoPostBack="True" Enabled="False" RepeatColumns="4" Width="330px">
                                            <asp:ListItem>Anticipo</asp:ListItem>
                                            <asp:ListItem>Vehículo</asp:ListItem>
                                            <asp:ListItem>Combustible</asp:ListItem>
                                            <asp:ListItem>Avión</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style10" style="text-align: right;">
                                <asp:Label ID="lbl_TipoTansp" runat="server" Text="Tipo Transporte:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <asp:Label ID="lblTipoTansp" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: center; height: 10px;">

                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; height: 30px; border-top-style: dashed; border-top-width: thin; border-top-color: #666666;">
                                <asp:Label ID="lbl_Opinion" runat="server" Font-Bold="True" Font-Size="10pt" Text="Tu opinión es Importante"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlR1" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: left; width: 350px;">&nbsp;</td>
                            <td style="text-align: left;">
                                <asp:Label ID="lbl_P1" runat="server" Text="En general, ¿cómo calificaría el servicio que le brindo la Administradora de Viajes?"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">&nbsp;</td>
                            <td style="text-align: left;">
                                <asp:Label ID="lbl_S1" runat="server" Text="Selección una sola respuesta"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">&nbsp;</td>
                            <td style="text-align: left;">
                                <asp:RadioButtonList ID="rblR1" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="E">Excelente</asp:ListItem>
                                    <asp:ListItem Value="MB">Muy bueno</asp:ListItem>
                                    <asp:ListItem Value="B">Bueno</asp:ListItem>
                                    <asp:ListItem Value="R">Regular</asp:ListItem>
                                    <asp:ListItem Value="M">Mala</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlR1a" runat="server">
                        <table style="width:100%;">
                            <tr>
                                <td style="text-align: left; width: 350px;">&nbsp;</td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lbl_P1a" runat="server" Text="Específicamente, ¿qué hizo que el servicio fuera Excelente?"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left;">&nbsp;</td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtR1a" runat="server" Height="60px" TextMode="MultiLine" Width="700px" Font-Names="Verdana" Font-Size="8pt"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table style="width:100%;">
                            <tr>
                                <td style="text-align: center; height: 40px;">
                                    <asp:Button ID="btnSig1" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Siguiente" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel ID="pnlR2" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: left; width: 280px;">&nbsp;</td>
                            <td style="text-align: left; width: 400px;">
                                <asp:Label ID="lbl_P2" runat="server" Text="¿Cómo calificaría a la Administradora de Viajes?"></asp:Label>
                            </td>
                            <td style="text-align: left; width: 450px;">&nbsp;</td>
                            <td style="text-align: left;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">&nbsp;</td>
                            <td style="text-align: left;">
                                <asp:Label ID="lbl_S2" runat="server" Text="Selecciones una respuesta para cada afirmación"></asp:Label>
                            </td>
                            <td style="text-align: left;">&nbsp;</td>
                            <td style="text-align: left;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">&nbsp;</td>
                            <td style="text-align: left;">&nbsp;</td>
                            <td style="text-align: left;">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="text-align: center; width: 20%">
                                            <asp:Label ID="lbl_2E" runat="server" Text="Excelente"></asp:Label>
                                        </td>
                                        <td style="text-align: center; width: 20%">
                                            <asp:Label ID="lbl_2MB" runat="server" Text="Muy Bueno"></asp:Label>
                                        </td>
                                        <td style="text-align: center; width: 20%">
                                            <asp:Label ID="lbl_2B" runat="server" Text="Bueno"></asp:Label>
                                        </td>
                                        <td style="text-align: center; width: 20%">
                                            <asp:Label ID="lbl_2R" runat="server" Text="Regular"></asp:Label>
                                        </td>
                                        <td style="text-align: center; width: 20%">
                                            <asp:Label ID="lbl_2M" runat="server" Text="Mala"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="text-align: left;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">&nbsp;</td>
                            <td style="text-align: left;">
                                <asp:Label ID="lbl_P2a" runat="server" Text="Mostró actitud de servicio" Width="400px"></asp:Label>
                            </td>
                            <td style="text-align: center;">
                                <asp:RadioButtonList ID="rblR2a" runat="server" RepeatColumns="5" Width="450px" ForeColor="White">
                                    <asp:ListItem Value="E">.</asp:ListItem>
                                    <asp:ListItem Value="MB">.</asp:ListItem>
                                    <asp:ListItem Value="B">.</asp:ListItem>
                                    <asp:ListItem Value="R">.</asp:ListItem>
                                    <asp:ListItem Value="M">.</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="text-align: left;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">&nbsp;</td>
                            <td style="text-align: left;">
                                <asp:Label ID="lbl_P2b" runat="server" Text="Mostró profesionalismo al brindar el servicio requerido"></asp:Label>
                            </td>
                            <td style="text-align: center;">
                                <asp:RadioButtonList ID="rblR2b" runat="server" ForeColor="White" RepeatColumns="5" Width="450px">
                                    <asp:ListItem Value="E">.</asp:ListItem>
                                    <asp:ListItem Value="MB">.</asp:ListItem>
                                    <asp:ListItem Value="B">.</asp:ListItem>
                                    <asp:ListItem Value="R">.</asp:ListItem>
                                    <asp:ListItem Value="M">.</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="text-align: left;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">&nbsp;</td>
                            <td style="text-align: left;">
                                <asp:Label ID="lbl_P2c" runat="server" Text="Dio seguimiento a su solicitud hasta la confirmación de la misma"></asp:Label>
                            </td>
                            <td style="text-align: center;">
                                <asp:RadioButtonList ID="rblR2c" runat="server" ForeColor="White" RepeatColumns="5" Width="450px">
                                    <asp:ListItem Value="E">.</asp:ListItem>
                                    <asp:ListItem Value="MB">.</asp:ListItem>
                                    <asp:ListItem Value="B">.</asp:ListItem>
                                    <asp:ListItem Value="R">.</asp:ListItem>
                                    <asp:ListItem Value="M">.</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="text-align: left;">&nbsp;</td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: center; height: 40px;">
                                <asp:Button ID="btnSig2" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Siguiente" Width="100px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlR3" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: left; width: 460px;">&nbsp;</td>
                            <td style="text-align: left;">
                                <asp:Label ID="lbl_P3" runat="server" Text="¿Cómo considera el tiempo de respuesta de su solicitud?"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">&nbsp;</td>
                            <td style="text-align: left;">
                                <asp:Label ID="lbl_S3" runat="server" Text="Selección una sola respuesta"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">&nbsp;</td>
                            <td style="text-align: left;">
                                <asp:RadioButtonList ID="rblR3" runat="server">
                                    <asp:ListItem Value="E">Excelente</asp:ListItem>
                                    <asp:ListItem Value="MB">Muy bueno</asp:ListItem>
                                    <asp:ListItem Value="B">Bueno</asp:ListItem>
                                    <asp:ListItem Value="R">Regular</asp:ListItem>
                                    <asp:ListItem Value="M">Malo</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: center; height: 40px;">
                                <asp:Button ID="btnSig3" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Siguiente" Width="100px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlR4" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: left; width: 350px;">&nbsp;</td>
                            <td style="text-align: left;">
                                <asp:Label ID="lbl_P4" runat="server" Text="Comentarios para mejorar:"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">&nbsp;</td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtR4" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="60px" TextMode="MultiLine" Width="700px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: center; height: 40px;">
                                <asp:Button ID="btnSig4" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Siguiente" Width="100px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlR5" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: center; height: 50px;">
                                
                                <asp:Label ID="lbl_G1" runat="server" Font-Bold="True" Font-Size="12pt" Text="Gracias por tomarse el tiempo para completar este cuestionario"></asp:Label>
                                
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Label ID="lbl_G2" runat="server" Font-Bold="False" Font-Size="10pt" Text="Tu opinión es muy importante para nosotros y nos ayudará a mejorar el servicio que le ofrecemos a nuestros clientes internos"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; height: 60px;">
                                <asp:Label ID="lbl_G3" runat="server" Font-Bold="False" Font-Size="10pt" Text="Atentamente"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;" class="auto-style2">
                                <asp:Label ID="lbl_G4" runat="server" Font-Bold="False" Font-Size="10pt" Text="Dirección de Administración y Finanzas"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Label ID="lbl_G5" runat="server" Font-Bold="False" Font-Size="10pt" Text="Corporativo UNNE"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
