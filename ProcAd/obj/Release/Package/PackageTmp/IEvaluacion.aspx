<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="IEvaluacion.aspx.vb" Inherits="ProcAd.IEvaluacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Evaluación</title>
    <style type="text/css">

    

.igte_Edit
{
	background-color:White;
	font-size:13px;
	font-family: Segoe UI, Verdana, Arial, Sans-Serif;
	border:solid 1px #CCCCCC;
	outline: 0;
	color:#333333;
}


    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width: 904px; position: absolute; top: 0px; left: 0px; font-family: Verdana; font-size: 7pt; text-align: center;">
            <tr>
                <td>
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: center; width: 80px">
                                <asp:Image ID="imgLogo" runat="server" ImageUrl="images\logo.jpg" Width="50px" />
                            </td>
                            <td style="text-align: center; margin-left: 40px;">
                                <asp:Label ID="lbl_Titulo" runat="server" Font-Bold="True" Font-Size="10pt" Text="Evaluación Individual" Visible="False"></asp:Label>
                            </td>
                            <td style="width: 90px">
                                <table style="width: 100%; font-size: 6pt; text-align: right;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_Formato1" runat="server" Text="Clave: Fr 14.5 1-F2" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_Formato2" runat="server" Text="Versión: 4.0" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_Formato3" runat="server" Text="Fecha: 21/11/13" Visible="False"></asp:Label>
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
                <asp:Panel ID="pnlInicio" runat="server">
                    <table style="width:100%; background-color: #4F81BD;">
                        <tr>
                            <td>
                                <table style="width: 100%; ">
                                    <tr>
                                        <td style="width: 130px;">&nbsp;</td>
                                        <td style="text-align: center;">
                                            <asp:Label ID="lbl_DatosGral" runat="server" Font-Bold="True" ForeColor="White">DATOS GENERALES</asp:Label>
                                        </td>
                                        <td style="width: 50px; text-align: right;">
                                            <asp:Label ID="lbl_Folio" runat="server" ForeColor="#000066" Text="Folio:"></asp:Label>
                                        </td>
                                        <td style="width: 70px; text-align: left;">
                                            <asp:Label ID="lblFolio" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; height: 44px;">
                        <tr>
                            <td style="text-align: right; width: 120px; height: 21px;">
                                <asp:Label ID="lbl_Colaborador" runat="server" Text="Colaborador evaluado:"></asp:Label>
                            </td>
                            <td style="text-align: left; width: 210px;">
                                <asp:Label ID="lblColaborador" runat="server" ForeColor="Blue" Width="200px"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 90px; height: 21px;">
                                <asp:Label ID="lbl_PuestoEval" runat="server" Text="Puesto evaluado:"></asp:Label>
                            </td>
                            <td style="text-align: left; width: 200px;">
                                <asp:Label ID="lblPuestoEval" runat="server" ForeColor="Blue" Width="190px"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 80px; height: 21px;">
                                <asp:Label ID="lbl_Area" runat="server" Text="Área:"></asp:Label>
                            </td>
                            <td style="text-align: left; ">
                                <asp:Label ID="lblArea" runat="server" ForeColor="Blue" Width="170px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; height: 21px;">
                                <asp:Label ID="lbl_Lider" runat="server" Text="Líder evaluador:"></asp:Label>
                            </td>
                            <td style="text-align: left; ">
                                <asp:Label ID="lblLider" runat="server" ForeColor="Blue" Width="200px"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_PuestoLider" runat="server" Text="Puesto del Líder:"></asp:Label>
                            </td>
                            <td style="text-align: left; ">
                                <asp:Label ID="lblPuestoLider" runat="server" ForeColor="Blue" Width="190px"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_MesEval" runat="server" Text="Mes a evaluar:"></asp:Label>
                            </td>
                            <td style="text-align: left; ">
                                <asp:Label ID="lblMesEval" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlIndicadores" runat="server">
                        <table style="width: 100%; height: 25px;">
                            <tr>
                                <td style="text-align: center; background-color: #4F81BD">
                                    <asp:Label ID="lbl_IndAsig" runat="server" Font-Bold="True" ForeColor="White">INDICADORES ASIGNADOS</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; background-color: #95B3D7">
                                    <asp:Label ID="lbl_IndAsigI" runat="server" Font-Bold="True" ForeColor="#1F497D">De acuerdo al cumplimiento obtenido captura el % de avance a cada objetivo o indicador establecido en tu Descripción de puestos</asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table style="width:100%;">
                            <tr>
                                <td style="width: 20px">&nbsp;</td>
                                <td>
                                    <asp:GridView ID="gvIndicadores" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="850px">
                                        <Columns>
                                            <asp:BoundField DataField="indicador" HeaderText="Indicador">
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="mes_eval" HeaderText="Mes a evaluar">
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="tipo_indicador" HeaderText="Tipo Indicador">
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ponderacion" DataFormatString="{0:p}" HeaderText="Pond.">
                                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="meta" DataFormatString="{0:p}" HeaderText="Meta">
                                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="real" DataFormatString="{0:p}" HeaderText="Real">
                                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cumpl_pond" DataFormatString="{0:p}" HeaderText="Cumplimiento Ponderación">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fuente" HeaderText="Fuente">
                                            <ItemStyle HorizontalAlign="Left" Width="150px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
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
                        <table style="width:100%;">
                            <tr>
                                <td style="width: 20px">&nbsp;</td>
                                <td style="width: 380px; text-align: right;">
                                    <asp:Label ID="lbl_PondTotal" runat="server" Text="Ponderación Indicadores Total"></asp:Label>
                                </td>
                                <td style="width: 65px; text-align: center;">
                                    <asp:Label ID="lblPondTotal" runat="server" Font-Bold="True" Text="0 %"></asp:Label>
                                </td>
                                <td style="width: 141px; text-align: right;">&nbsp;</td>
                                <td style="width: 80px; text-align: center;">
                                    <asp:Label ID="lblPorcentCumpl" runat="server" Font-Bold="True" Text="0 %"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="lbl_PorcentCumpl" runat="server" Text="Porcent. Cumplimiento Indicadores"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td style="text-align: center;">
                                    <asp:Label ID="lblCalifCumpl" runat="server" Font-Bold="True" Text="Sobresaliente"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="lbl_CalifCumpl" runat="server" Text="Calificación Total Indicadores"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table style="width:100%;">
                            <tr>
                                <td style="width: 70px">&nbsp;</td>
                                <td style="text-align: center; background-color: #000000; color: #FFFFFF; font-weight: bold;">
                                    <asp:Label ID="lbl_ComentarioC" runat="server" Text="Comentarios Colaborador" Width="200px"></asp:Label>
                                </td>
                                <td style="width: 140px">&nbsp;</td>
                                <td style="text-align: center; background-color: #000000; color: #FFFFFF; font-weight: bold;">
                                    <asp:Label ID="lbl_ComentarioL" runat="server" Text="Comentarios Líder" Width="200px"></asp:Label>
                                </td>
                                <td style="width: 70px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td style="text-align: center">
                                    <asp:TextBox ID="txtComentarioC" runat="server" Font-Names="Verdana" Font-Size="7pt" Height="73px" MaxLength="340" TextMode="MultiLine" Width="400px" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                                <td style="text-align: center">
                                    <asp:TextBox ID="txtComentarioL" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="73px" MaxLength="340" TextMode="MultiLine" Width="400px" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </asp:Panel>
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
