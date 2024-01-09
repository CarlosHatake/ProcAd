<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Menu.aspx.vb" Inherits="ProcAd.Menu" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link rel="shortcut icon" href="images\ProcAd.ico" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Menú</title>
    <style type="text/css">
        .auto-style1 {
            width: 260px;
        }
        .auto-style4 {
            width: 694px;
        }
        .auto-style12 {
            width: 23px;
            text-align:center;
        }
        .auto-style13 {
            width: 50px;
        }
        .auto-style14 {
            height: 20px;
        }
        .stbutton{
            text-align:left;
        }
    </style>
    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
    </script>
    <link href="css\bootstrap.css" rel="stylesheet" />
</head>
    <body onload="noBack();" onpageshow="if (event.persisted) noBack();" onunload="">
        <form id="form1" runat="server">
            <div>
                  <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
                      <tr>
                          <td style="margin-right: auto; margin-left: auto; text-align: center;">
                              <asp:Image ID="imgTitulo" runat="server" ImageAlign="Middle" ImageUrl="images\banner v2.png" />
                          </td>
                      </tr>
                      <tr>
                          <td style="font-family: Verdana; font-size: 8pt; color: #FF0000; text-align: center;">
                              <asp:TextBox ID="_txtTransporte" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                              <asp:TextBox ID="_txtNoEmpleado" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                              <asp:TextBox ID="_txtPerfil" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                              <asp:TextBox ID="_txtIdAct" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                              <asp:TextBox ID="_txtIdUsuario" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                              <asp:Literal ID="litError" runat="server"></asp:Literal>
                          </td>
                      </tr>
                      <tr>
                           <td>
                               <table style="width:98%;">
                                   <tr>
                                       <td class="auto-style13">&nbsp;</td>
                                       <td class="auto-style1" style="vertical-align: top">
                                           <div id="accordion">
                                               <div>
                                                   <div id="headEvaluacion">
                                                       <div class="mb-0">
                                                           <a class="btn btn-link" data-toggle="collapse" data-target="#collEvaluacion" aria-expanded="true" aria-controls="collEvaluacion">
                                                               <asp:Panel ID="pnlEvaluacionTit" runat="server">
                                                                   <table id="Table69" style="height: 22px; width: 224px; text-align: right">
                                                                       <tr>
                                                                           <td style="text-align: center; width: 20px;">
                                                                               <asp:Image ID="imgEval11" runat="server" ImageUrl="images\icn_categories.png" Width="17px" />
                                                                           </td>
                                                                           <td style="text-align: left">
                                                                               <asp:Label ID="lblProcAdEval" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" Style="z-index: 0" ForeColor="Black">Evaluaciones</asp:Label>
                                                                           </td>
                                                                       </tr>
                                                                   </table>
                                                               </asp:Panel>
                                                           </a>
                                                       </div>
                                                   </div>
                                                   <div id="collEvaluacion" class="collapse" aria-labelledby="headEvaluacion" data-parent="#accordion">
                                                       <div class="card-body">
                                                           <asp:Panel ID="pnlEvaluacion" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgEval1" runat="server" ImageUrl="images\icn_new.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnRegistrar" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Registrar Evaluación" Width="128px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlEvaluacionAut" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgEval2" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnValidarEval" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Validar Evaluación" Width="118px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlEvaluacionCorr" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgEval3" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCorregirEval" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Corregir Evaluación" Width="122px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlAutorizarEvalA" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgEval5" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnAutorizarEvalA" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Autorizar Evaluaciones del Área" Width="190px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlCorregirEvalA" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgEval6" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCorregirEvalA" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Corregir Evaluaciones del Área" Width="185px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlValidarEvalA" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgEval4" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnValidarEvalA" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Validar Evaluaciones del Área" Width="178px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlProcesarEval1Q" runat="server">
                                                               <table style="height: 20px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgEval9" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnProcesarEval1Q" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Procesar Eval. 1ra Quincena" Width="168px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlConcentrarEval" runat="server">
                                                               <table style="height: 20px; width: 235px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgEval7" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConcentrarEval" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Concentrar Evaluaciones" Width="151px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlProcesarEval2Q" runat="server">
                                                               <table style="height: 20px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgEval8" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnProcesarEval2Q" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Procesar Eval. 2da Quincena" Width="170px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlJefInfo" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgCa9" runat="server" ImageUrl="images\icn_new.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnRegCumplUN" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Registrar % Cumplimiento UN" Width="178px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlCatEvaluacion" runat="server">
                                                               <table style="height: 100px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgCa3" runat="server" ImageUrl="images\icn_new.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCatUnidadN" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Catálogo de Unidades de Negocio" Width="202px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgCa4" runat="server" ImageUrl="images\icn_new.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCatDireccion" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Catálogo de Direcciones" Width="149px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgCa5" runat="server" ImageUrl="images\icn_new.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCatArea" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Catálogo de Áreas" Width="118px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgCa6" runat="server" ImageUrl="images\icn_new.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnPorcentBono" runat="server" BackColor="White" BorderStyle="None" CssClass="stbutton" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Porcentaje Bono" Width="112px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgCa7" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnPeriodoEval" runat="server" BackColor="White" BorderStyle="None" CssClass="stbutton" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Periodo de Evaluaciones" Width="152px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgCa8" runat="server" ImageUrl="images\icn_new.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCatEmplInd" runat="server" BackColor="White" BorderStyle="None" CssClass="stbutton" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Catálogo de Empleado-Indicador" Width="195px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlConsEvaluacion" runat="server" Width="250px">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgEval10" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsEvaluacion" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consultar Evaluaciones" Width="140px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                       </div>
                                                   </div>
                                               </div>
                                               <div>
                                                   <div id="headReunion">
                                                       <div class="mb-0">
                                                           <a class="btn btn-link" data-toggle="collapse" data-target="#collReunion" aria-expanded="true" aria-controls="collReunion">
                                                               <asp:Panel ID="pnlReunionTit" runat="server">
                                                                   <table id="Table70" style="height: 22px; width: 224px; text-align: right">
                                                                       <tr>
                                                                           <td style="text-align: center; width: 20px;">
                                                                               <asp:Image ID="imgReun8" runat="server" ImageUrl="images\icn_categories.png" Width="17px" />
                                                                           </td>
                                                                           <td style="text-align: left">
                                                                               <asp:Label ID="lblProcAdReun" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" Style="z-index: 0" ForeColor="Black">Reuniones</asp:Label>
                                                                           </td>
                                                                       </tr>
                                                                   </table>
                                                               </asp:Panel>
                                                           </a>
                                                       </div>
                                                   </div>
                                                   <div id="collReunion" class="collapse" aria-labelledby="headReunion" data-parent="#accordion">
                                                       <div class="card-body">
                                                           <asp:Panel ID="pnlCatGrupo" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgReun1" runat="server" ImageUrl="images\icn_new.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCatGrupo" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Catálogo de Grupos" Width="128px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlAltaReunion" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgReun2" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnAltaReunion" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Registrar Reunión" Width="117px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlSegReunion" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgReun3" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnSegReunion" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Seguimiento a Reunión" Width="147px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlEvalReunion" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgReun5" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnEvalReunion" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Evaluar Participantes" Width="130px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlConsReunion" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgReun6" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsReunion" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta de Reuniones" Width="143px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlAltaActividad" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgReun7" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnAltaActividad" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Alta de Actividad" Width="115px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlConsActividad" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgReun4" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsActividad" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta de Actividades" Width="147px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                       </div>
                                                   </div>
                                               </div>
                                               <div>
                                                   <div id="headChecador">
                                                       <div class="mb-0">
                                                           <a class="btn btn-link" data-toggle="collapse" data-target="#collChecador" aria-expanded="true" aria-controls="collChecador">
                                                               <asp:Panel ID="pnlChecador1" runat="server">
                                                                   <table id="tbl58" style="height: 22px; width: 224px; text-align: right">
                                                                       <tr>
                                                                           <td style="text-align: center; width: 20px;">
                                                                               <asp:Image ID="Image6" runat="server" ImageUrl="images\icn_categories.png" Width="17px" />
                                                                           </td>
                                                                           <td style="text-align: left">
                                                                               <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" Style="z-index: 0" ForeColor="Black">Checador</asp:Label>
                                                                           </td>
                                                                       </tr>
                                                                   </table>
                                                               </asp:Panel>
                                                           </a>
                                                       </div>
                                                   </div>
                                                   <div id="collChecador" class="collapse" aria-labelledby="headChecador" data-parent="#accordion">
                                                       <div class="card-body">
                                                           <asp:Panel ID="pnlChecador" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="Image7" runat="server" ImageUrl="images\icn_new.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnChecador" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Ingresos Checador" Width="130px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                       </div>
                                                   </div>
                                               </div>
                                               <div>
                                                   <div id="headCatalogoUser">
                                                       <div class="mb-0">
                                                           <a class="btn btn-link" data-toggle="collapse" data-target="#collCatalogoUser" aria-expanded="true" aria-controls="collCatalogoUser">
                                                               <asp:Panel ID="Panel1" runat="server">
                                                                   <table id="tbl57" style="height: 22px; width: 224px; text-align: right">
                                                                       <tr>
                                                                           <td style="text-align: center; width: 20px;">
                                                                               <asp:Image ID="Image2" runat="server" ImageUrl="images\icn_categories.png" Width="17px" />
                                                                           </td>
                                                                           <td style="text-align: left">
                                                                               <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" Style="z-index: 0" ForeColor="Black">Catálogo de Usuarios</asp:Label>
                                                                           </td>
                                                                       </tr>
                                                                   </table>
                                                               </asp:Panel>
                                                           </a>
                                                       </div>
                                                   </div>
                                                   <div id="collCatalogoUser" class="collapse" aria-labelledby="headCatalogoUser" data-parent="#accordion">
                                                       <div class="card-body">
                                                           <asp:Panel ID="pnlCatAltaUsrAC" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgCa12" runat="server" ImageUrl="images\icn_new.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCatUsuarioAC" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Catálogo de Usuarios" Width="132px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>

                                                           <asp:Panel ID="pnlModCargasCombustible" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="Image5" runat="server" ImageUrl="images\icn_new.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnModCargasC" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Cargas de combustible" Width="132px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                       </div>
                                                   </div>
                                               </div>
                                               <div>
                                                   <div id="headCatalogo">
                                                       <div class="mb-0">
                                                           <a class="btn btn-link" data-toggle="collapse" data-target="#collCatalogo" aria-expanded="true" aria-controls="collCatalogo">
                                                               <asp:Panel ID="pnlCaTitulo" runat="server">
                                                                   <table id="Table57" style="height: 22px; width: 224px; text-align: right">
                                                                       <tr>
                                                                           <td style="text-align: center; width: 20px;">
                                                                               <asp:Image ID="imgCa16" runat="server" ImageUrl="images\icn_categories.png" Width="17px" />
                                                                           </td>
                                                                           <td style="text-align: left">
                                                                               <asp:Label ID="lblProcAdCat" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" Style="z-index: 0" ForeColor="Black">Catálogos</asp:Label>
                                                                           </td>
                                                                       </tr>
                                                                   </table>
                                                               </asp:Panel>
                                                           </a>
                                                       </div>
                                                   </div>
                                                   <div id="collCatalogo" class="collapse" aria-labelledby="headCatalogo" data-parent="#accordion">
                                                       <div class="card-body">
                                                           <asp:Panel ID="pnlCatAltaUsr" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgCa1" runat="server" ImageUrl="images\icn_new.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCatUsuario" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Catálogo de Usuarios" Width="132px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlCatPermisos" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="Image3" runat="server" ImageUrl="images\icn_new.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCatPermisos" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Catálogo de Perfiles" Width="125px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlCatConcepto" runat="server">
                                                               <table style="height: 20px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgCa13" runat="server" ImageUrl="images\icn_new.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCatCategoria" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Catálogo de Categorías" Width="145px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgCa14" runat="server" ImageUrl="images\icn_new.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCatConceptoSF" runat="server" BackColor="White" BorderStyle="None" CssClass="stbutton" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Catálogo de Conceptos Sin Fact." Width="199px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgCa15" runat="server" ImageUrl="images\icn_new.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCatConceptoCF" runat="server" BackColor="White" BorderStyle="None" CssClass="stbutton" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Catálogo de Conceptos Con Fact." Width="202px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlCatServicio" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgCa11" runat="server" ImageUrl="images\icn_new.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCatServicio" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Catálogo de Servicios" Width="136px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlCatOrigenDest" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgCa2" runat="server" ImageUrl="images\icn_new.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCatLugar" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Catálogo de Lugares" Width="132px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlCatConsAut" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgCa10" runat="server" ImageUrl="images\icn_new.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCatConsAut" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta de Autorizadores" Width="162px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlCatVoBo" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgCa17" runat="server" ImageUrl="images\icn_new.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCatTipoHospedaje" runat="server" BackColor="White" BorderStyle="None" CssClass="stbutton" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" ForeColor="Black" Text="Catálogo de Tipos de Hospedaje" Width="188px" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlCatAuditoria" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgCa18" runat="server" ImageUrl="images\icn_new.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCatListaNegra" runat="server" BackColor="White" BorderStyle="None" CssClass="stbutton" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" ForeColor="Black" Text="Catálogo Lista Negra" Width="130px" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           
                                                           <asp:Panel ID="pnlConsSer" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imaCa19" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsServ" runat="server" BackColor="White" BorderStyle="None" CssClass="stbutton" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" ForeColor="Black" Text="Consulta Conceptos y Servicios" Width="180px" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                       </div>
                                                   </div>
                                               </div>
                                               <div>
                                                   <div id="headFacturaSAT">
                                                       <div class="mb-0">
                                                           <a class="btn btn-link" data-toggle="collapse" data-target="#collFacturaSAT" aria-expanded="true" aria-controls="collFacturaSAT">
                                                               <asp:Panel ID="pnlFSATTitulo" runat="server">
                                                                   <table id="Table61" style="height: 22px; width: 224px; text-align: right">
                                                                       <tr>
                                                                           <td style="text-align: center; width: 20px;">
                                                                               <asp:Image ID="imgFSAT4" runat="server" ImageUrl="images\icn_categories.png" Width="17px" />
                                                                           </td>
                                                                           <td style="text-align: left">
                                                                               <asp:Label ID="lblProcAdFSAT" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" Style="z-index: 0" ForeColor="Black">Facturas SAT</asp:Label>
                                                                           </td>
                                                                       </tr>
                                                                   </table>
                                                               </asp:Panel>
                                                           </a>
                                                       </div>
                                                   </div>
                                                   <div id="collFacturaSAT" class="collapse" aria-labelledby="headFacturaSAT" data-parent="#accordion">
                                                       <div class="card-body">
                                                           <asp:Panel ID="pnlFSATCarga" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgFSAT1" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCargaFact" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Carga de Facturas" Width="115px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlFSATConsultar" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgFSAT2" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsFactDet" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta de Facturas" Width="130px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlFSATLiq" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgFSAT3" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsFactLiq" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consultar Facturas" Width="117px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                       </div>
                                                   </div>
                                               </div>
                                               <%--cambio dayra--%>
                                             <%--  <div id="headFacturaCFDI">
                                                       <div class="mb-0">
                                                           <a class="btn btn-link" data-toggle="collapse" data-target="#collFacturasCFDI" aria-expanded="true" aria-controls="collFacturasCFDI">
                                                               <asp:Panel ID="pnlFCFDITitulo" runat="server">
                                                                   <table id="tblCFDI" style="height: 22px; width: 224px; text-align: right">
                                                                       <tr>
                                                                           <td style="text-align: center; width: 20px;">
                                                                               <asp:Image ID="imgFCFDI" runat="server" ImageUrl="images\icn_categories.png" Width="17px" />
                                                                           </td>
                                                                           <td style="text-align: left">
                                                                               <asp:Label ID="lblProcAdFCFDI" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" Style="z-index: 0" ForeColor="Black">Facturas NAV-NET-PROCAD</asp:Label>
                                                                           </td>
                                                                       </tr>
                                                                   </table>
                                                               </asp:Panel>
                                                           </a>
                                                       </div>
                                                   </div>
                                                   <div id="collFacturasCFDI" class="collapse" aria-labelledby="headFacturaSAT" data-parent="#accordion">
                                                       <div class="card-body">
                                                           <asp:Panel ID="pnlFCFDIConsulta" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgFCFDI1" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsCFDI" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta de Facturas" Width="115px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                       </div>
                                                   </div>--%>

                                               <div>
                                                   <div id="headNegServ">
                                                       <div class="mb-0">
                                                           <a class="btn btn-link" data-toggle="collapse" data-target="#collNegServ" aria-expanded="true" aria-controls="collNegServ">
                                                               <asp:Panel ID="pnlNSTitulo" runat="server">
                                                                   <table id="Table71" style="height: 22px; width: 242px; text-align: right">
                                                                       <tr>
                                                                           <td style="text-align: center; width: 20px;">
                                                                               <asp:Image ID="imgNS6" runat="server" ImageUrl="images\icn_categories.png" Width="17px" />
                                                                           </td>
                                                                           <td style="text-align: left">
                                                                               <asp:Label ID="lblProcAdNS" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" Style="z-index: 0" ForeColor="Black">Negociación de Servicio</asp:Label>
                                                                               <asp:Label ID="lblNS" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="Red" Text="Label"></asp:Label>
                                                                           </td>
                                                                       </tr>
                                                                   </table>
                                                               </asp:Panel>
                                                           </a>
                                                       </div>
                                                   </div>
                                                   <div id="collNegServ" class="collapse" aria-labelledby="headNegServ" data-parent="#accordion">
                                                       <div class="card-body">
                                                           <asp:Panel ID="pnlNSSolicitar" runat="server">
                                                               <table style="height: 20px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgNS1" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnSolicitarNS" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Solicitar Negociación de Servicio" Width="190px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlNSIngresarCot" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgNS2" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnIngresarCotNS" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Ingresar Cotizaciones" Width="137px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlNSAutorizarCot" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgNS3" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnAutorizarCotNS" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Autorizar Cotización" Width="130px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlNSAutorizarNeg" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgNS4" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnAutorizarNegNS" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Autorizar Negociación" Width="127px" ForeColor="Black" />
                                                                           <asp:Label ID="lblAutorizarNegNS" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="Red" Text="Label"></asp:Label>
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlNSConsultaNeg" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgNS5" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsultaNeg" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta de Negociaciones" Width="165px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                       </div>
                                                   </div>
                                               </div>
                                               <div>
                                                   <div id="headServNeg">
                                                       <div class="mb-0">
                                                           <a class="btn btn-link" data-toggle="collapse" data-target="#collServNeg" aria-expanded="true" aria-controls="collServNeg">
                                                               <asp:Panel ID="pnlSNTitulo" runat="server">
                                                                   <table id="Table72" style="height: 22px; width: 242px; text-align: right">
                                                                       <tr>
                                                                           <td style="text-align: center; width: 20px;">
                                                                               <asp:Image ID="imgSN9" runat="server" ImageUrl="images\icn_categories.png" Width="17px" />
                                                                           </td>
                                                                           <td style="text-align: left">
                                                                               <asp:Label ID="lblProcAdSN" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" Style="z-index: 0" ForeColor="Black">Servicios Negociados</asp:Label>
                                                                               <asp:Label ID="lblSN" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="Red" Text="Label"></asp:Label>
                                                                           </td>
                                                                       </tr>
                                                                   </table>
                                                               </asp:Panel>
                                                           </a>
                                                       </div>
                                                   </div>
                                                   <div id="collServNeg" class="collapse" aria-labelledby="headServNeg" data-parent="#accordion">
                                                       <div class="card-body">
                                                           <asp:Panel ID="pnlSNSolicitar" runat="server">
                                                               <table style="height: 20px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgSN1" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnSolicitarSN" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Solicitar Servicio Negociado" Width="170px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlSNValidar1" runat="server">
                                                               <table style="height: 20px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgSN2" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnValidar1SN" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Validar Aplicabilidad" Width="115px" ForeColor="Black" />
                                                                           <asp:Label ID="lblValidar1SN" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="Red" Text="Label"></asp:Label>
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlSNAutorizar" runat="server">
                                                               <table style="height: 20px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgSN3" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnAutorizarSN" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Autorizar Servicio Negociado" Width="165px" ForeColor="Black" />
                                                                           <asp:Label ID="lblAutorizarSN" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="Red" Text="Label"></asp:Label>
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlSNValPresup" runat="server">
                                                               <table style="height: 20px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgSN4" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnValPresupSN" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Validar Presupuesto para SN" Width="178px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlSNSolAmplPre" runat="server">
                                                               <table style="height: 20px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgSN5" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnSolAmplPreSN" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Solicitar Ampl. de Presup. para SN" Width="205px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlSNIngresarF" runat="server">
                                                               <table style="height: 20px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgSN7" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnIngresarFSN" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Ingresar Factura de SN" Width="145px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlSNValidar2" runat="server">
                                                               <table style="height: 20px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgSN6" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnValidar2SN" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Validar Soportes" Width="100px" ForeColor="Black" />
                                                                           <asp:Label ID="lblValidar2SN" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="Red" Text="Label"></asp:Label>
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlSNCorregirF" runat="server">
                                                               <table style="height: 20px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgSN8" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCorregirFSN" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Corregir Factura de SN" Width="145px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                       </div>
                                                   </div>
                                               </div>

                                               <%--Validar de que proceso es--%>

                                       <%--        <div>
                                                   <div id="headFact">
                                                       <div class="mb-0">
                                                           <a class="btn btn-link" data-toggle="collapse" data-target="#collFact" aria-expanded="true" aria-controls="collFact">
                                                               <asp:Panel ID="pnlFTitulo" runat="server">
                                                                   <table id="Table62" style="height: 22px; width: 234px; text-align: right">
                                                                       <tr>
                                                                           <td style="text-align: center; width: 20px;">
                                                                               <asp:Image ID="imgFT" runat="server" ImageUrl="images\icn_categories.png" Width="17px" />
                                                                           </td>
                                                                           <td style="text-align: left">
                                                                               <asp:Label ID="lblProcAdF" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" Style="z-index: 0" ForeColor="Black">Facturas de Gasto, Seg. y Asesoría</asp:Label>
                                                                           </td>
                                                                       </tr>
                                                                   </table>
                                                               </asp:Panel>
                                                           </a>
                                                       </div>
                                                   </div>
                                                   <div id="collFact" class="collapse" aria-labelledby="headFact" data-parent="#accordion">
                                                       <div class="card-body">
                                                           <asp:Panel ID="pnlFIngresar" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgF1" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnSolFact" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Ingresar Factura" Width="105px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlFCorregir" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgF2" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCorrFact" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Corregir Factura" Width="105px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlFAutorizar" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgF3" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnAutFact" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Autorizar Factura" Width="110px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                       </div>
                                                   </div>
                                               </div>
                                               <div>--%>
                                                   <div id="headIngFact">
                                                       <div class="mb-0">
                                                           <a class="btn btn-link" data-toggle="collapse" data-target="#collIngFact" aria-expanded="true" aria-controls="collIngFact">
                                                               <asp:Panel ID="pnlIFTitulo" runat="server">
                                                                   <table id="Table66" style="height: 22px; width: 258px; text-align: right">
                                                                       <tr>
                                                                           <td style="text-align: center; width: 20px;">
                                                                               <asp:Image ID="imgIF15" runat="server" ImageUrl="images\icn_categories.png" Width="17px" />
                                                                           </td>
                                                                           <td style="text-align: left">
                                                                               <asp:Label ID="lblProcAdIF" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" Style="z-index: 0" ForeColor="Black">Solicitud de Gasto, Serv. o Asesoría</asp:Label>
                                                                               <asp:Label ID="lblServGastoAseso" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="Red" Text="99"></asp:Label>
                                                                           </td>
                                                                       </tr>
                                                                   </table>
                                                               </asp:Panel>
                                                           </a>
                                                       </div>
                                                   </div>
                                                   <div id="collIngFact" class="collapse" aria-labelledby="headIngFact" data-parent="#accordion">
                                                       <div class="card-body">
                                                           <asp:Panel ID="pnlIFSolicitar" runat="server">
                                                               <table style="height: 20px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgIF1" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnSolicitarGSA" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Solicitar Gasto, Servicio o Asesoría" Width="205px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlIFValidar" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgIF4" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnValidarSol" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Validar Solicitud" Width="95px" ForeColor="Black" />
                                                                           <asp:Label ID="lblValidarSol" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="Red" Text="Label"></asp:Label>
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlIFIngresarCot" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgIF5" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnIngresarCot" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Ingresar Cotizaciones" Width="137px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlIFCorregirSol" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgIF13" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCorregirSol" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Corregir Solicitud" Width="114px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlIFAutorizarSol" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgIF6" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnAutorizarSol" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Autorizar Solicitud" Width="105px" ForeColor="Black" />
                                                                           <asp:Label ID="lblAutorizarSol" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="Red" Text="Label"></asp:Label>
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlIFValidarPresup" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgIF12" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnValidarPresup" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Validar Presupuesto" Width="117px" ForeColor="Black" />
                                                                           <asp:Label ID="lblValidarPresup" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="Red" Text="Label"></asp:Label>
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlIFSolAmplPresup" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgIF14" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnSolAmplPresup" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Solicitar Ampl. de Presupuesto" Width="189px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlIFCompContrato" runat="server">
                                                               <table style="height: 20px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgIF8" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCompContrato" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Complementar Datos de Contrato" Width="204px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlIFAutContrato" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgIF9" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnAutContrato" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Autorizar Sol. de Contrato" Width="152px" ForeColor="Black" />
                                                                           <asp:Label ID="lblAutContrato" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="Red" Text="Label"></asp:Label>
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlIFAsigCContrato" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgIF10" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnAsigCContrato" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Asignar Cuentas para Contrato" Width="188px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlIFRegContrato" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgIF11" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnRegContrato" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Registrar Contrato en NAV" Width="166px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlIFIngresar" runat="server">
                                                               <table style="height: 40px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgIF2" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnIngresarFact" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Ingresar Factura" Width="108px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgIF3" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCorrFactura" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Corregir Factura" Width="105px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlIFAutorizarFact" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgIF7" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnAutorizarFact" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Autorizar Factura" Width="99px" ForeColor="Black" />
                                                                           <asp:Label ID="lblAutorizarFact" runat="server" Font-Names="Verdana" Font-Size="7pt" Text="Label" Font-Bold="True" ForeColor="Red"></asp:Label>
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlFAsignar" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgF4" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnAsigCuenta" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Asignar Cuenta" Width="100px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlFRegistrarNAV" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgF5" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsFactI" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Facturas Ingresadas" Width="127px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlFConsultar" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgF6" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsFact" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Style="height: 18px" Text="Consulta de Facturas" Width="132px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlFConsultarCot" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgF7" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsCotFact" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Style="height: 18px" Text="Consulta de Cot. de Facturas" Width="174px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlCConsFactExp" runat="server">
                                                               <table style="height: 20px; width: 255px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgC16" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsFactExp" runat="server" BackColor="White" BorderStyle="None" CssClass="stbutton" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" ForeColor="Black" Text="Consulta de Facturas Expromat" Width="185px" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                       </div>
                                                   </div>
                                               </div>
                                               <div>
                                                   <div id="headPresupGV">
                                                       <div class="mb-0">
                                                           <a class="btn btn-link" data-toggle="collapse" data-target="#collPresupGV" aria-expanded="true" aria-controls="collPresupGV">
                                                               <asp:Panel ID="pnlPGVTitulo" runat="server">
                                                                   <table id="Table73" style="height: 22px; width: 244px; text-align: right">
                                                                       <tr>
                                                                           <td style="text-align: center; width: 20px;">
                                                                               <asp:Image ID="imgPGVT" runat="server" ImageUrl="images\icn_categories.png" Width="17px" />
                                                                           </td>
                                                                           <td style="text-align: left">
                                                                               <asp:Label ID="lblProcAdPGV" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" Style="z-index: 0" ForeColor="Black">Presupuesto de Gastos de Viaje</asp:Label>
                                                                               <asp:Label ID="lblPGV" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="Red" Text="99"></asp:Label>
                                                                           </td>
                                                                       </tr>
                                                                   </table>
                                                               </asp:Panel>
                                                           </a>
                                                       </div>
                                                   </div>
                                                   <div id="collPresupGV" class="collapse" aria-labelledby="headPresupGV" data-parent="#accordion">
                                                       <div class="card-body">
                                                           <asp:Panel ID="pnlPGVCargarPresup" runat="server">
                                                               <table style="height: 20px; width: 244px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgPGV1" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCargaPresupGV" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Carga de Presup. Gastos de Viaje" Width="202px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgPGV5" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsPresupGV" runat="server" BackColor="White" BorderStyle="None" CssClass="stbutton" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta Presup. Gastos de Viaje" Width="202px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlPGVConsulta" runat="server">
                                                               <table style="height: 20px; width: 244px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgPGV6" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsultaPGV" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta Presup. Gastos de Viaje" Width="196px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlPGVSolicitarAmpl" runat="server">
                                                               <table style="height: 20px; width: 244px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgPGV2" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnSolicitarAmplPGV" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Solicitar Ampl. Presup. Gastos V." Width="196px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlPGVValidarAmpl" runat="server">
                                                               <table style="height: 20px; width: 249px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="Image1" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnValAmplPGV" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Validar Ampl. Presup. Gastos V." Width="180px" ForeColor="Black" />
                                                                           <asp:Label ID="lblValAmplPGV" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="Red" Text="99"></asp:Label>
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlPGVAutorizarAmpl" runat="server">
                                                               <table style="height: 20px; width: 249px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgPGV3" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnAutorizarAmplPGV" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Autorizar Ampl. Presup. Gastos V." Width="192px" ForeColor="Black" />
                                                                           <asp:Label ID="lblAutorizarAmplPGV" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="Red" Text="99"></asp:Label>
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlPGVConsultarAmpl" runat="server">
                                                               <table style="height: 20px; width: 244px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgPGV4" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsultaAmplPGV" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta Ampl. Presup. Gastos V." Width="198px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                       </div>
                                                   </div>
                                               </div>
                                               <div>
                                                   <div id="headSolRec">
                                                       <div class="mb-0">
                                                           <a class="btn btn-link" data-toggle="collapse" data-target="#collSolRec" aria-expanded="true" aria-controls="collSolRec">
                                                               <asp:Panel ID="pnlSRTitulo" runat="server">
                                                                   <table id="Table58" style="height: 22px; width: 224px; text-align: right">
                                                                       <tr>
                                                                           <td style="text-align: center; width: 20px;">
                                                                               <asp:Image ID="imgSRT" runat="server" ImageUrl="images\icn_categories.png" Width="17px" />
                                                                           </td>
                                                                           <td style="text-align: left">
                                                                               <asp:Label ID="lblProcAdSR" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" Style="z-index: 0" ForeColor="Black">Solicitud de Recursos</asp:Label>
                                                                               <asp:Label ID="lblSolRec" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="Red" Text="Label"></asp:Label>
                                                                           </td>
                                                                       </tr>
                                                                   </table>
                                                               </asp:Panel>
                                                           </a>
                                                       </div>
                                                   </div>
                                                   <div id="collSolRec" class="collapse" aria-labelledby="headSolRec" data-parent="#accordion">
                                                       <div class="card-body">
                                                           <asp:Panel ID="pnlSRSolicitar" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgSR1" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnSolRec" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Style="height: 18px" Text="Solicitar Recursos" Width="115px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlSRAutorizar" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgSR2" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnAutSolRec" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Autorizar Sol. Recursos" Width="132px" ForeColor="Black" />
                                                                           <asp:Label ID="lblAutSolRec" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="Red" Text="Label"></asp:Label>
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlSRAutDir" runat="server">
                                                               <table style="height: 20px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="Image14" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnSRAutDir" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Autorizar Sol. Recursos Exedente" Width="190px" ForeColor="Black" />
                                                                           <asp:Label ID="lblSRAutDir" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="Red" Text="Label"></asp:Label>
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlSRVoBo" runat="server">
                                                               <table style="height: 20px; width: 254px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgSR5" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnVoBoSolRec" runat="server" BackColor="White" BorderStyle="None" CssClass="stbutton" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" ForeColor="Black" Text="Vo.Bo. Sol. Recursos" Width="122px" />
                                                                           <asp:Label ID="lblVoBoSolRec" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="Red" Text="Label"></asp:Label>
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgSR6" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsReservCH" runat="server" BackColor="White" BorderStyle="None" CssClass="stbutton" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" ForeColor="Black" Text="Consulta de Hosp. Casa de Huéspedes" Width="230px" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlSRConsultar" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgSR3" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsSR" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta de Sol. Recursos" Width="161px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlSRConsultarAV" runat="server">
                                                               <table style="height: 20px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgSR4" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsSRAV" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta de Sol. Recursos Detalle" Width="198px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                       </div>
                                                   </div>
                                               </div>
                                               <div>
                                                   <div id="headAnt">
                                                       <div class="mb-0">
                                                           <a class="btn btn-link" data-toggle="collapse" data-target="#collAnt" aria-expanded="true" aria-controls="collAnt">
                                                               <asp:Panel ID="pnlATitulo" runat="server">
                                                                   <table id="Table59" style="height: 22px; width: 224px; text-align: right">
                                                                       <tr>
                                                                           <td style="text-align: center; width: 20px;">
                                                                               <asp:Image ID="imgAT" runat="server" ImageUrl="images\icn_categories.png" Width="17px" />
                                                                           </td>
                                                                           <td style="text-align: left">
                                                                               <asp:Label ID="lblProcAdA" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" Style="z-index: 0" ForeColor="Black">Anticipos</asp:Label>
                                                                           </td>
                                                                       </tr>
                                                                   </table>
                                                               </asp:Panel>
                                                           </a>
                                                       </div>
                                                   </div>
                                                   <div id="collAnt" class="collapse" aria-labelledby="headAnt" data-parent="#accordion">
                                                       <div class="card-body">
                                                           <asp:Panel ID="pnlAGenerarAAE" runat="server">
                                                               <table style="height: 20px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgA5" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnGenAAE" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Generar Anticipo American Express" Width="210px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlAGenerarTransfer" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgA1" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnGenTransf" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Generar Transferencia" Width="142px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlAEntregarEfect" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgA2" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnEntEfectA" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Entregar Efectivo" Width="114px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlARegistrarAAE" runat="server">
                                                               <table style="height: 20px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgA6" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnRegAAE" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Registrar Anticipo American Express" Width="210px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlAConsultar" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgA3" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsAnt" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta de Anticipos" Width="135px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlAConsCuadreA" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgA4" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsAntCaja" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta Cuadre de Anticipos" Width="176px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlAConsAud" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgA7" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsAntAud" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta de Anticipos Auditoria" Width="187px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                       </div>
                                                   </div>
                                               </div>
                                               <div>
                                                   <div id="headReservVeh">
                                                       <div class="mb-0">
                                                           <a class="btn btn-link" data-toggle="collapse" data-target="#collReservVeh" aria-expanded="true" aria-controls="collReservVeh">
                                                               <asp:Panel ID="pnlRVTitulo" runat="server">
                                                                   <table id="Table63" style="height: 22px; width: 224px; text-align: right">
                                                                       <tr>
                                                                           <td style="text-align: center; width: 20px;">
                                                                               <asp:Image ID="imgRVT" runat="server" ImageUrl="images\icn_categories.png" Width="17px" />
                                                                           </td>
                                                                           <td style="text-align: left">
                                                                               <asp:Label ID="lblProcAdRV" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" Style="z-index: 0" ForeColor="Black">Reservación de Vehículos</asp:Label>
                                                                           </td>
                                                                       </tr>
                                                                   </table>
                                                               </asp:Panel>
                                                           </a>
                                                       </div>
                                                   </div>
                                                   <div id="collReservVeh" class="collapse" aria-labelledby="headReservVeh" data-parent="#accordion">
                                                       <div class="card-body">
                                                           <asp:Panel ID="pnlRAdministar" runat="server">
                                                               <table style="height: 100px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgRV1" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnEntVeh" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Entregar Vehículo al Usuario" Width="170px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgRV2" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnRecibVeh" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Recibir Vehículo del Usuario" Width="167px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgRV3" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCambVeh" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Cambiar Vehículo" Width="110px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgRV4" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCancSol" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Cancelar Reservación" Width="131px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgRV5" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCambFecha" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Cambiar de Fecha de Regreso" Width="180px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlRVConsultar" runat="server">
                                                               <table style="height: 40px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgRV6" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsDisp" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta de Disponibilidad" Width="156px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgRV7" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsSol" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta de Solicitudes" Width="145px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlRVConsultaV" runat="server">
                                                               <table style="height: 27px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgRV8" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsVeh" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta de Vehículos" Width="139px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                       </div>
                                                   </div>
                                               </div>
                                               <div>
                                                   <div id="headGasolina">
                                                       <div class="mb-0">
                                                           <a class="btn btn-link" data-toggle="collapse" data-target="#collGasolina" aria-expanded="true" aria-controls="collGasolina">
                                                               <asp:Panel ID="pnlGTitulo" runat="server">
                                                                   <table id="Table64" style="height: 22px; width: 224px; text-align: right">
                                                                       <tr>
                                                                           <td style="text-align: center; width: 20px;">
                                                                               <asp:Image ID="imgGT" runat="server" ImageUrl="images\icn_categories.png" Width="17px" />
                                                                           </td>
                                                                           <td style="text-align: left">
                                                                               <asp:Label ID="lblProcAdG" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" Style="z-index: 0" ForeColor="Black">Gasolina</asp:Label>
                                                                           </td>
                                                                       </tr>
                                                                   </table>
                                                               </asp:Panel>
                                                           </a>
                                                       </div>
                                                   </div>
                                                   <div id="collGasolina" class="collapse" aria-labelledby="headGasolina" data-parent="#accordion">
                                                       <div class="card-body">
                                                           <asp:Panel ID="pnlGCargar" runat="server">
                                                               <table style="height: 20px; width: 244px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgG1" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCargaEdenred" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Carga de Registros Edenred" Width="170px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgG8" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCargaToka" runat="server" BackColor="White" BorderStyle="None" CssClass="stbutton" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Carga de Registros Toka" Width="150px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlGDispersar" runat="server">
                                                               <table style="height: 60px; width: 244px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgG2" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnDispersarG" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Dispersar Gasolina" Width="120px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgG7" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCargaSinID" runat="server" BackColor="White" BorderStyle="None" CssClass="stbutton" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Cargas Sin ID Conductor" Width="152px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgG3" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsUsrBloq" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Usuarios Bloqueados" Width="130px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlGComprobar" runat="server">
                                                               <table style="height: 55px; width: 244px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgG4" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCompComb" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Comprobar Combustible" Width="145px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgG5" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCompCombTOKA" runat="server" BackColor="White" BorderStyle="None" CssClass="stbutton" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Comprobar Combustible TOKA" Width="178px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgG9" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCompCombTar" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Comp. Combustible con Tarjeta B." Width="200px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlGConsultar" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgG6" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsComb" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta de Sol. Combustible" Width="176px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                       </div>
                                                   </div>
                                               </div>
                                               <div>
                                                   <div id="headComprob">
                                                       <div class="mb-0">
                                                           <a class="btn btn-link" data-toggle="collapse" data-target="#collComprob" aria-expanded="true" aria-controls="collComprob">
                                                               <asp:Panel ID="pnlCTitulo" runat="server">
                                                                   <table id="Table60" style="height: 22px; width: 224px; text-align: right">
                                                                       <tr>
                                                                           <td style="text-align: center; width: 20px;">
                                                                               <asp:Image ID="imgCT" runat="server" ImageUrl="images\icn_categories.png" Width="17px" />
                                                                           </td>
                                                                           <td style="text-align: left">
                                                                               <asp:Label ID="lblProcAdAdC" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" Style="z-index: 0" ForeColor="Black">Comprobaciones</asp:Label>
                                                                               <asp:Label ID="lblComp" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="Red" Text="Label"></asp:Label>
                                                                           </td>
                                                                       </tr>
                                                                   </table>
                                                               </asp:Panel>
                                                           </a>
                                                       </div>
                                                   </div>
                                                   <div id="collComprob" class="collapse" aria-labelledby="headComprob" data-parent="#accordion">
                                                       <div class="card-body">
                                                           <asp:Panel ID="pnlCGenerarCompExt" runat="server">
                                                               <table style="height: 20px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgC12" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnGenCompExt" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Generar Comprobación Extemp." Width="192px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlCGenerarComp" runat="server">
                                                               <table style="height: 40px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgC1" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnGenComp" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Generar Comprobación" Width="145px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgC2" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCorrComp" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Corregir Comprobación" Width="142px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlCAutCompExt" runat="server">
                                                               <table style="height: 20px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgC13" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnAutCompExt" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Autorizar Comprobación Extemp." Width="197px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlPreAtorizacion" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="Image15" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnPreAutorizacion" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Pre - Autorizar Comprobaciones" Width="160px" ForeColor="Black" />
                                                                           <asp:Label ID="lblPreAutorizacion" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="Red" Text="Label"></asp:Label>
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlCAutorizar" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgC3" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnAutComp" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Autorizar Comprobaciones" Width="150px" ForeColor="Black" />
                                                                           <asp:Label ID="lblAutComp" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="Red" Text="Label"></asp:Label>
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlCValidar" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgC4" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnValComp" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Validar Comprobación" Width="137px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlCEntregarEfect" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgC5" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnEntEfectC" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Entregar Efectivo" Width="115px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlCConsultar" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgC6" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsComp" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta de Comprobaciones" Width="175px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlCConsCompConta" runat="server">
                                                               <table style="height: 20px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgC7" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsCompConta" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta de Comprobaciones Cont." Width="208px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlCConsCompT" runat="server">
                                                               <table style="height: 20px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgC14" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsCompT" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta de Comprobaciones Detalle" Width="210px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlCConsCompSV" runat="server">
                                                               <table style="height: 20px; width: 240px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgC11" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsCompSV" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta de Comprobaciones SV" Width="191px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlCConsCompExp" runat="server">
                                                               <table style="height: 20px; width: 255px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgC15" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsCompExp" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta de Comprobaciones Expromat" Width="230px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlCConsultarComp" runat="server">
                                                               <table style="height: 60px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgC8" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnPlantComp" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Plantilla Comprobaciones" Width="152px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgC9" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsCompResum" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta Comp. Resumen" Width="154px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgC10" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsCompXConc" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta Comp. x Concepto" Width="168px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                       </div>
                                                   </div>
                                               </div>
                                               <div>
                                                   <div id="headingOne">
                                                       <div class="mb-0">
                                                           <a class="btn btn-link" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                                               <asp:Panel ID="pnlVBRTitulo" runat="server">
                                                                   <table id="Table65" style="height: 22px; width: 245px; text-align: right">
                                                                       <tr>
                                                                           <td style="text-align: center; width: 20px;">
                                                                               <asp:Image ID="imgVBRT" runat="server" ImageUrl="images\icn_categories.png" Width="17px" />
                                                                           </td>
                                                                           <td style="text-align: left">
                                                                               <asp:Label ID="lblProcAdVBR" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" Style="z-index: 0" ForeColor="Black">Vehículos / Bloqueos x Rendimiento</asp:Label>
                                                                           </td>
                                                                       </tr>
                                                                   </table>
                                                               </asp:Panel>
                                                           </a>
                                                       </div>
                                                   </div>
                                                   <div id="collapseOne" class="collapse" aria-labelledby="headingOne" data-parent="#accordion">
                                                       <div class="card-body">
                                                           <asp:Panel ID="pnlVBRCatVehiculo" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgVBR1" runat="server" ImageUrl="images\icn_new.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCatVehiculo" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" EnableTheming="True" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Catálogo de Vehículos" Width="136px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlVBRBloqueo" runat="server">
                                                               <table style="height: 20px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgVBR2" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsUsrBloqR" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Bloqueos por Rendimiento" Width="158px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlVBRConsultar" runat="server">
                                                               <table style="height: 40px; width: 224px; font-family: Verdana; font-size: 8px;">
                                                                   <tr>
                                                                       <td style="border-width: 0px; padding: 0px; margin: 0px;" class="auto-style12">
                                                                           <asp:Image ID="imgVBR3" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnBitacoraCargas" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Bitácora de Cargas" Width="118px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgVBR4" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsBloqueos" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta de Bloqueos" Width="135px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlConsAud" runat="server">
                                                               <table id="Table43" aria-orientation="horizontal" style="height: 22px; width: 224px; text-align: right">
                                                                   <tr>
                                                                       <td style="text-align: left">
                                                                           <asp:Label ID="lblProcAdConsAud" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" Style="z-index: 0">Consultas de Auditoría</asp:Label>
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                               <table style="font-family: Verdana; font-size: 8px; width: 224px; height: 60px;">
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgConsA1" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsAntCaAud" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta de Anticipos" Width="133px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgConsA2" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsAntCajaAud" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta Cuadre de Anticipos" Width="176px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgConsA3" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsCompXConAud" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta Comp. x Concepto" Width="168px" ForeColor="Black" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                       </div>
                                                   </div>
                                               </div>
                                               <div>
                                                   <div id="headingInt">
                                                       <div class="mb-0">
                                                           <a class="btn btn-link" data-toggle="collapse" data-target="#collapseInt" aria-expanded="true" aria-controls="collapseInt">
                                                               <asp:Panel ID="pnlMovInt" runat="server">
                                                                   <table id="Table67" style="height: 22px; width: 245px; text-align: right">
                                                                       <tr>
                                                                           <td style="text-align: center; width: 20px;">
                                                                               <asp:Image ID="Image16" runat="server" ImageUrl="images\icn_categories.png" Width="17px" />
                                                                           </td>
                                                                           <td style="text-align: left">
                                                                               <asp:Label ID="lblMovInt" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" Style="z-index: 0" ForeColor="Black">Movimientos Internos</asp:Label>
                                                                               <asp:Label ID="lblAutMovInt" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="Red" Text="99"></asp:Label>

                                                                           </td>
                                                                       </tr>
                                                                   </table>
                                                               </asp:Panel>
                                                           </a>
                                                       </div>
                                                   </div>
                                                   <div id="collapseInt" class="collapse" aria-labelledby="headingInt" data-parent="#accordion">
                                                       <div class="card-body">
                                                           <asp:Panel ID="pnlAutMovInt" runat="server">
                                                               <table style="font-family: Verdana; font-size: 8px; width: 224px; height: 20px;">
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgMov1" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 1px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnAutMov" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Autorizar Movimientos Internos" Width="173px" ForeColor="Black" Style="height: 18px" />
                                                                           <asp:Label ID="lblAutMov" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="Red" Text="99"></asp:Label>

                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlSolMovInt" runat="server">
                                                               <table style="font-family: Verdana; font-size: 8px; width: 224px; height: 20px;">
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgMov3" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 1px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnSolMovInt" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Solicitar Movimientos Internos " Width="173px" ForeColor="Black" Style="height: 18px" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlCodificacionCont" runat="server">
                                                               <table style="font-family: Verdana; font-size: 8px; width: 224px; height: 20px;">
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="imgMov2" runat="server" ImageUrl="images\icn_edit.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 1px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnCodCont" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Codificación Contable " Width="123px" ForeColor="Black" Style="height: 18px" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                           <asp:Panel ID="pnlConsFactIng" runat="server">
                                                               <table style="font-family: Verdana; font-size: 8px;" class="auto-style16">
                                                                   <tr>
                                                                       <td class="auto-style12" style="border-width: 0px; padding: 0px; margin: 0px;">
                                                                           <asp:Image ID="Image17" runat="server" ImageUrl="images\icn_search.png" Width="17px" />
                                                                       </td>
                                                                       <td class="auto-style14" style="padding: 0px 0px 1px 0px; margin: 0px; border-width: 0px; text-align: left;">
                                                                           <asp:Button ID="btnConsFactIng" runat="server" CssClass="stbutton" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Consulta de Facturas Ingresadas" Width="183px" ForeColor="Black" Style="height: 18px" />
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </asp:Panel>
                                                       </div>
                                                   </div>
                                               </div>
                                           </div>
                                       </td>


                                       <td style="text-align: center; vertical-align: top;">
                                           <asp:Image ID="imgMenu" runat="server" Width="695px" />
                                           <table style="width: 100%;">
                                               <tr>
                                                   <td style="width: 35px">
                                                       <asp:Image ID="imgTrans" runat="server" ImageUrl="images\trans.gif" Width="30px" />
                                                   </td>
                                                   <td>
                                                       <asp:GridView ID="gvRegistrosReun" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="900px">
                                                           <Columns>
                                                               <asp:BoundField DataField="id_ms_reunion" HeaderText="id_ms_reunion" />
                                                               <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                   <ItemStyle Width="15px" />
                                                               </asp:CommandField>
                                                               <asp:BoundField DataField="id_ms_reunion" HeaderText="Folio">
                                                                   <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="grupo" HeaderText="Grupo">
                                                                   <ItemStyle HorizontalAlign="Left" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="tema" HeaderText="Tema">
                                                                   <ItemStyle HorizontalAlign="Left" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="fecha_reunion" HeaderText="Fecha Reunión">
                                                                   <ItemStyle HorizontalAlign="Center" Width="180px" />
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
                                                       <asp:GridView ID="gvRegistrosEval" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="900px">
                                                           <Columns>
                                                               <asp:BoundField DataField="id_ms_instancia" HeaderText="id_ms_instancia" />
                                                               <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                   <ItemStyle Width="15px" />
                                                               </asp:CommandField>
                                                               <asp:BoundField DataField="id_ms_evaluacion" HeaderText="Folio">
                                                                   <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="direccion" HeaderText="Direccion">
                                                                   <ItemStyle HorizontalAlign="Left" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="area" HeaderText="Área">
                                                                   <ItemStyle HorizontalAlign="Left" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="empleado" HeaderText="Colaborador">
                                                                   <ItemStyle HorizontalAlign="Left" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="puesto" HeaderText="Puesto">
                                                                   <ItemStyle HorizontalAlign="Left" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="porcent_cumpl" HeaderText="Porcent. Cumpl." DataFormatString="{0:p}">
                                                                   <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="fecha_registro" HeaderText="Fecha Registro">
                                                                   <ItemStyle HorizontalAlign="Center" Width="100px" />
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
                                                       <asp:GridView ID="gvRegistrosSR" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="900px">
                                                           <Columns>
                                                               <asp:BoundField DataField="id_ms_instancia" HeaderText="id_ms_instancia" />
                                                               <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                   <ItemStyle Width="15px" />
                                                               </asp:CommandField>
                                                               <asp:BoundField DataField="id_ms_recursos" HeaderText="No. Sol. Recursos">
                                                                   <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="empresa" HeaderText="Empresa" />
                                                               <asp:BoundField DataField="solicito" HeaderText="Solicitó">
                                                                   <ItemStyle HorizontalAlign="Left" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="periodo_ini" HeaderText="Desde" DataFormatString="{0:d}">
                                                                   <ItemStyle HorizontalAlign="Center" Width="75px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="periodo_fin" HeaderText="Hasta" DataFormatString="{0:d}">
                                                                   <ItemStyle HorizontalAlign="Center" Width="75px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="destino" HeaderText="Destino">
                                                                   <ItemStyle HorizontalAlign="Left" Width="190px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="id_ms_anticipo" HeaderText="No. Anticipo">
                                                                   <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="id_ms_reserva" HeaderText="No. Reserva">
                                                                   <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="id_ms_comb" HeaderText="No. Comb.">
                                                                   <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="id_ms_avion" HeaderText="Sol. Avión">
                                                                   <ItemStyle HorizontalAlign="Center" Width="60px" />
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
                                                       <asp:Panel ID="pnlFiltroA" runat="server" Width="900px">
                                                           <table style="width: 100%; height: 35px;">
                                                               <tr>
                                                                   <td style="text-align: right">
                                                                       <asp:Label ID="lbl_EmpresaA" runat="server" Text="Empresa:"></asp:Label>
                                                                   </td>
                                                                   <td style="width: 190px; text-align: left">
                                                                       <asp:DropDownList ID="ddlEmpresaA" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="180px">
                                                                       </asp:DropDownList>
                                                                   </td>
                                                               </tr>
                                                           </table>
                                                       </asp:Panel>
                                                       <asp:GridView ID="gvRegistrosA" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="900px">
                                                           <Columns>
                                                               <asp:BoundField DataField="id_ms_instancia" HeaderText="id_ms_instancia" />
                                                               <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                   <ItemStyle Width="15px" />
                                                               </asp:CommandField>
                                                               <asp:BoundField DataField="id_ms_anticipo" HeaderText="No. Anticipo">
                                                                   <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="empresa" HeaderText="Empresa" />
                                                               <asp:BoundField DataField="solicito" HeaderText="Solicitó">
                                                                   <ItemStyle HorizontalAlign="Left" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="periodo_ini" HeaderText="Desde" DataFormatString="{0:d}">
                                                                   <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="periodo_fin" HeaderText="Hasta" DataFormatString="{0:d}">
                                                                   <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="destino" HeaderText="Destino">
                                                                   <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="importe" HeaderText="Importe" DataFormatString="{0:c}">
                                                                   <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="fecha_autoriza" HeaderText="Fecha Autorización">
                                                                   <ItemStyle HorizontalAlign="Center" Width="100px" />
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
                                                       <asp:GridView ID="gvRegistrosC" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="900px">
                                                           <Columns>
                                                               <asp:BoundField DataField="id_ms_instancia" HeaderText="id_ms_instancia" />
                                                               <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                   <ItemStyle Width="15px" />
                                                               </asp:CommandField>
                                                               <asp:BoundField DataField="id_ms_comp" HeaderText="No. Comp.">
                                                                   <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="empresa" HeaderText="Empresa" />
                                                               <asp:BoundField DataField="empleado" HeaderText="Comprobó">
                                                                   <ItemStyle HorizontalAlign="Left" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="periodo_ini" HeaderText="Desde" DataFormatString="{0:d}">
                                                                   <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="periodo_fin" HeaderText="Hasta" DataFormatString="{0:d}">
                                                                   <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="importe_ant" HeaderText="Anticipos" DataFormatString="{0:c}">
                                                                   <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="importe_comp" HeaderText="Comprobado" DataFormatString="{0:c}">
                                                                   <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="importe_tot" DataFormatString="{0:c}" HeaderText="Saldo">
                                                                   <ItemStyle HorizontalAlign="Right" Width="90px" />
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
                                                       <asp:GridView ID="gvRegistrosNS" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="900px">
                                                           <Columns>
                                                               <asp:BoundField DataField="id_ms_instancia" HeaderText="id_ms_instancia" />
                                                               <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                   <ItemStyle Width="15px" />
                                                               </asp:CommandField>
                                                               <asp:BoundField DataField="id_ms_negoc_servicio" HeaderText="No. Solicitud">
                                                                   <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                                                   <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="empleado_solicita" HeaderText="Solicitó">
                                                                   <ItemStyle HorizontalAlign="Left" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="cc_div" HeaderText="Centro Costo / División">
                                                                   <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="servicio" HeaderText="Servicio">
                                                                   <ItemStyle HorizontalAlign="Left" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="proveedor" HeaderText="Proveedor">
                                                                   <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="fecha" HeaderText="Fecha Solicitud">
                                                                   <ItemStyle HorizontalAlign="Center" Width="100px" />
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
                                                       <asp:Panel ID="pnlFiltroF" runat="server" Width="900px">
                                                           <table style="width: 100%; height: 35px;">
                                                               <tr>
                                                                   <td style="text-align: right">
                                                                       <asp:Label ID="lbl_Empresa" runat="server" Text="Empresa:"></asp:Label>
                                                                   </td>
                                                                   <td style="width: 190px; text-align: left">
                                                                       <asp:DropDownList ID="ddlEmpresaF" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="180px">
                                                                       </asp:DropDownList>
                                                                   </td>
                                                               </tr>
                                                           </table>
                                                       </asp:Panel>
                                                       <asp:GridView ID="gvRegistrosF" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="900px">
                                                           <Columns>
                                                               <asp:BoundField DataField="id_ms_instancia" HeaderText="id_ms_instancia" />
                                                               <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                   <ItemStyle Width="15px" />
                                                               </asp:CommandField>
                                                               <asp:BoundField DataField="id_ms_factura" HeaderText="No. Solicitud">
                                                                   <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                                                   <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="solicito" HeaderText="Solicitó">
                                                                   <ItemStyle HorizontalAlign="Left" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="centro_costo" HeaderText="Centro Costo / División">
                                                                   <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="razon_emisor" HeaderText="Proveedor">
                                                                   <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="fecha_emision" HeaderText="Fecha Emisión">
                                                                   <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="importe" HeaderText="Importe" DataFormatString="{0:c}">
                                                                   <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="tipo_servicio" HeaderText="Tipos Servicio">
                                                                   <ItemStyle HorizontalAlign="Left" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="fecha_solicita" HeaderText="Fecha Solicitud">
                                                                   <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="alta_contrato" HeaderText="Alta Contrato">
                                                                   <ItemStyle HorizontalAlign="Center" Width="60px" />
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
                                                       <asp:GridView ID="gvRegistrosSAP" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="900px">
                                                           <Columns>
                                                               <asp:BoundField DataField="id_ms_instancia" HeaderText="id_ms_instancia" />
                                                               <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                   <ItemStyle Width="15px" />
                                                               </asp:CommandField>
                                                               <asp:BoundField DataField="id_ms_ampliacion_p" HeaderText="No. Solicitud">
                                                                   <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                                                   <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="solicita" HeaderText="Solicitó">
                                                                   <ItemStyle HorizontalAlign="Left" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="centro_costo" HeaderText="Centro Costo / División">
                                                                   <ItemStyle HorizontalAlign="Left" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="monto_solicita" HeaderText="Importe Solicitado" DataFormatString="{0:c}">
                                                                   <ItemStyle HorizontalAlign="Right" Width="130px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="fecha_solicita" HeaderText="Fecha Solicitud">
                                                                   <ItemStyle HorizontalAlign="Center" Width="100px" />
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
                                                       <asp:GridView ID="gvRegistrosV" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="900px">
                                                           <Columns>
                                                               <asp:BoundField DataField="id_ms_reserva" HeaderText="id_ms_reserva" />
                                                               <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                   <ItemStyle Width="15px" />
                                                               </asp:CommandField>
                                                               <asp:BoundField DataField="id_ms_reserva" HeaderText="No. Reserva">
                                                                   <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="solicito" HeaderText="Solicitó">
                                                                   <ItemStyle HorizontalAlign="Left" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="fecha_ini" HeaderText="Desde">
                                                                   <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="fecha_fin" HeaderText="Hasta">
                                                                   <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="prioridad" HeaderText="Prioridad">
                                                                   <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="no_eco" HeaderText="No. Económico">
                                                                   <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="placas" HeaderText="Placas">
                                                                   <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="destino" HeaderText="Destino">
                                                                   <ItemStyle HorizontalAlign="Left" Width="150px" />
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
                                                       <asp:GridView ID="gvRegistrosG" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="900px">
                                                           <Columns>
                                                               <asp:BoundField DataField="id_dt_carga_comb" HeaderText="id_dt_carga_comb" />
                                                               <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                   <ItemStyle Width="15px" />
                                                               </asp:CommandField>
                                                               <asp:BoundField DataField="fecha" HeaderText="Fecha">
                                                                   <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="unidad" HeaderText="Unidad">
                                                                   <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="placa" HeaderText="Placas">
                                                                   <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="num_tarjeta" HeaderText="Tarjeta">
                                                                   <ItemStyle HorizontalAlign="Center" Width="130px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="no_comprobante" HeaderText="No. Ticket">
                                                                   <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="razon_social_afiliado" HeaderText="Estación de Carga"></asp:BoundField>
                                                               <asp:BoundField DataField="cantidad_mercancia" HeaderText="Litros">
                                                                   <ItemStyle HorizontalAlign="Right" Width="60px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="importe_transaccion" HeaderText="Importe" DataFormatString="{0:c}">
                                                                   <ItemStyle HorizontalAlign="Right" Width="80px" />
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
                                                       <asp:GridView ID="gvRegistrosDG" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="900px">
                                                           <Columns>
                                                               <asp:BoundField DataField="id_ms_instancia" HeaderText="id_ms_instancia" />
                                                               <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                   <ItemStyle Width="15px" />
                                                               </asp:CommandField>
                                                               <asp:BoundField DataField="id_ms_comb" HeaderText="Sol. Combustible">
                                                                   <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="solicito" HeaderText="Solicitó">
                                                                   <ItemStyle HorizontalAlign="Left" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="desde" HeaderText="Desde" DataFormatString="{0:d}">
                                                                   <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="hasta" HeaderText="Hasta" DataFormatString="{0:d}">
                                                                   <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="no_eco" HeaderText="No. Económico">
                                                                   <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="litros_comb" HeaderText="Litros">
                                                                   <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="importe_comb" HeaderText="Importe">
                                                                   <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="fecha_autoriza" HeaderText="Fecha Autoriza">
                                                                   <ItemStyle HorizontalAlign="Center" Width="100px" />
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
                                                       <asp:GridView ID="gvMovInt" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="900px">
                                                           <Columns>
                                                               <asp:BoundField DataField="id_ms_instancia" HeaderText="id_ms_instancia" />
                                                               <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                   <ItemStyle Width="15px" />
                                                               </asp:CommandField>
                                                               <asp:BoundField DataField="id_ms_movimientos_internos" HeaderText="No. Solicitud">
                                                                   <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                                                   <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="solicita" HeaderText="Solicitó">
                                                                   <ItemStyle HorizontalAlign="Left" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="centro_costo" HeaderText="Centro Costo / División">
                                                                   <ItemStyle HorizontalAlign="Left" />
                                                               </asp:BoundField>
                                                               <asp:BoundField DataField="fecha_solicita" HeaderText="Fecha Solicitud">
                                                                   <ItemStyle HorizontalAlign="Center" Width="100px" />
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
                           </td>
                      </tr>
                  </table>
            </div>
            <asp:Button ID="btnSalir" runat="server" BackColor="White" BorderColor="White" BorderStyle="None" Font-Bold="True" Font-Names="Verdana" Font-Size="9pt" Style="z-index: 1; left: 1185px; top: 90px; position: absolute; width: 130px;" Text="Cerrar Sesión" />
            <asp:Label ID="lblUsuario" runat="server" Text="Admon" Font-Bold="False" Font-Names="Verdana" Font-Size="9pt" Style="z-index: 1; left: 78px; top: 100px; position: absolute"></asp:Label>
            <asp:Image ID="imgUsuario" runat="server" ImageUrl="images\icn_user.png" Style="z-index: 1; left: 56px; top: 98px; position: absolute" />
            <asp:HyperLink ID="hlUNNE" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="14pt" ForeColor="Black" NavigateUrl="http://unne.com.mx" Style="z-index: 1; left: 1200px; top: 24px; position: absolute">Ir a UNNE</asp:HyperLink>
            <table style="width: 671px; position: absolute; z-index: 2; left: 351px; top: 90px; height: 20px;">
                <tr>
                    <td class="auto-style4" style="text-align: center;">
                        <asp:Label ID="lblTitulo" runat="server" Text="Menú Principal" Font-Bold="True" Font-Names="Verdana" Font-Size="12pt"></asp:Label>
                    </td>
                </tr>
            </table>
        </form>
        
    <script src ="js\jquery-3.3.1.js"></script>
    <script src ="js\bootstrap.js"></script>
    </body>
</html>


<script>
    jQuery(document).ready(function ($) {
        $(".card-body").each(function (index) {
            if ($(this).html().trim() == "") {
                $(this).parent().parent().remove();
            }
        })
    });
</script>
