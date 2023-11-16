<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="68.aspx.vb" Inherits="ProcAd._68" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1360px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000;">
                <ig:WebScriptManager ID="wsm66" runat="server">
                </ig:WebScriptManager>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:TextBox ID="_txtIdDtArea" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsInst" runat="server" Width="15px" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlInicio" runat="server">
                    <table style="width:100%; background-color: #4F81BD;">
                        <tr>
                            <td>
                                <table style="width: 1360px; height: 25px;">
                                    <tr>
                                        <td style="width: 280px;">&nbsp;</td>
                                        <td style="text-align: center;">
                                            <asp:Label ID="lbl_DatosGral" runat="server" Font-Bold="True" ForeColor="White">DATOS GENERALES</asp:Label>
                                        </td>
                                        <td style="width: 150px; text-align: right;">
                                            <asp:Label ID="lbl_Folio" runat="server" ForeColor="#000066" Text="Folio:"></asp:Label>
                                        </td>
                                        <td style="width: 130px;">
                                            <asp:Label ID="lblFolio" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1360px; height: 44px;">
                        <tr>
                            <td style="text-align: right; width: 150px; height: 21px;">
                                <asp:Label ID="lbl_Colaborador" runat="server" Text="Colaborador evaluado:"></asp:Label>
                            </td>
                            <td style="width: 300px;">
                                <asp:DropDownList ID="ddlColaborador" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="270px">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right; width: 140px; height: 21px;">
                                <asp:Label ID="lbl_PuestoEval" runat="server" Text="Puesto evaluado:"></asp:Label>
                            </td>
                            <td style="width: 320px;">
                                <asp:Label ID="lblPuestoEval" runat="server" ForeColor="Blue" Width="300px"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 100px; height: 21px;">
                                <asp:Label ID="lbl_Area" runat="server" Text="Área:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblArea" runat="server" ForeColor="Blue" Width="300px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; height: 21px;">
                                <asp:Label ID="lbl_Lider" runat="server" Text="Líder evaluador:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblLider" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_PuestoLider" runat="server" Text="Puesto del Líder:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPuestoLider" runat="server" ForeColor="Blue" Width="300px"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_MesEval" runat="server" Text="Mes a evaluar:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblMesEval" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td style="width: 70px; text-align: right; vertical-align: top;">
                                <asp:Label ID="lbl_ArchivoE" runat="server" Text="Archivo:"></asp:Label>
                            </td>
                            <td style="text-align: left; width: 600px; vertical-align: top;">
                                <asp:FileUpload ID="fuEvidencias" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="340px" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="cmdAgregarE" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="20px" Text="Agregar" Width="90px" />
                            </td>
                            <td style="text-align: left; width: 70px; vertical-align: top;">
                                <asp:Label ID="lbl_Evidencias" runat="server" Text="Evidencias:"></asp:Label>
                            </td>
                            <td style="text-align: left; ">
                                <asp:GridView ID="gvEvidencia" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ShowHeader="False" Width="450px">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <Columns>
                                        <asp:HyperLinkField DataNavigateUrlFields="path" DataTextField="evidencia" HeaderText="Evidencia">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="20px" />
                                        </asp:HyperLinkField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlIndicadores" runat="server">
                        <table style="width: 1360px; height: 25px;">
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
                                    <asp:GridView ID="gvIndicadores" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1300px">
                                        <Columns>
                                            <asp:BoundField DataField="id_dt_evaluacion" HeaderText="id_dt_evaluacion" />
                                            <asp:BoundField DataField="id_dt_empl_ind" HeaderText="id_dt_empl_ind" />
                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                            <ItemStyle Width="15px" />
                                            </asp:CommandField>
                                            <asp:BoundField DataField="indicador" HeaderText="Indicador">
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="mes_eval" HeaderText="Mes a evaluar">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="tipo_indicador" HeaderText="Tipo Indicador">
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ponderacion" DataFormatString="{0:p}" HeaderText="Pond.">
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="meta" DataFormatString="{0:p}" HeaderText="Meta">
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="real" DataFormatString="{0:p}" HeaderText="Real">
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cumpl_pond" DataFormatString="{0:p}" HeaderText="Cumplimiento Ponderación">
                                            <ItemStyle HorizontalAlign="Center" Width="110px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fuente" HeaderText="Fuente">
                                            <ItemStyle HorizontalAlign="Left" Width="190px" />
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
                                <td style="width: 715px; text-align: right;">
                                    <asp:Label ID="lbl_PondTotal" runat="server" Text="Ponderación Indicadores Total"></asp:Label>
                                </td>
                                <td style="width: 90px; text-align: center;">
                                    <asp:Label ID="lblPondTotal" runat="server" Font-Bold="True" Text="0 %"></asp:Label>
                                </td>
                                <td style="width: 171px; text-align: right;">&nbsp;</td>
                                <td style="width: 110px; text-align: center;">
                                    <asp:Label ID="lblPorcentCumpl" runat="server" Font-Bold="True" Text="0 %"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_PorcentCumpl" runat="server" Text="Porcentaje Cumplimiento Indicadores"></asp:Label>
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
                                <td>
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
                                    <asp:TextBox ID="txtComentarioC" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="73px" MaxLength="340" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                                <td style="text-align: center">
                                    <asp:TextBox ID="txtComentarioL" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="73px" MaxLength="340" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlIndicador" runat="server">
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <table style="width:100%;">
                                        <tr>
                                            <td style="width: 50px">&nbsp;</td>
                                            <td style="text-align: center; background-color: #000000; color: #FFFFFF; font-weight: bold;">
                                                <asp:Label ID="lbl_Indicador" runat="server" Text="Indicador"></asp:Label>
                                            </td>
                                            <td style="width: 100px; text-align: center; background-color: #000000; color: #FFFFFF; font-weight: bold;">
                                                <asp:Label ID="lbl_MesEvalI" runat="server" Text="Mes a evaluar"></asp:Label>
                                            </td>
                                            <td style="width: 90px; text-align: center; background-color: #000000; color: #FFFFFF; font-weight: bold;">
                                                <asp:Label ID="lbl_TipoIndI" runat="server" Text="Tipo de Indicador"></asp:Label>
                                            </td>
                                            <td style="width: 90px; text-align: center; background-color: #000000; color: #FFFFFF; font-weight: bold;">
                                                <asp:Label ID="lbl_PondIndI" runat="server" Text="Ponderación Indicador"></asp:Label>
                                            </td>
                                            <td style="width: 80px; text-align: center; background-color: #000000; color: #FFFFFF; font-weight: bold;">
                                                <asp:Label ID="lbl_MetaI" runat="server" Text="Meta"></asp:Label>
                                            </td>
                                            <td style="width: 80px; text-align: center; background-color: #000000; color: #FFFFFF; font-weight: bold;">
                                                <asp:Label ID="lbl_RealI" runat="server" Text="Real"></asp:Label>
                                            </td>
                                            <td style="width: 220px; text-align: center; background-color: #000000; color: #FFFFFF; font-weight: bold;">
                                                <asp:Label ID="lbl_FuenteI" runat="server" Text="Fuente"></asp:Label>
                                            </td>
                                            <td style="width: 50px">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Label ID="lblIndicador" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:Label ID="lblMesEvalI" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:Label ID="lblTipoIndI" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:Label ID="lblPondIndI" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:Label ID="lblMetaI" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: center">
                                                <ig:WebPercentEditor ID="wpeReal" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxValue="1" MinValue="0.01" Nullable="False" Width="70px">
                                                </ig:WebPercentEditor>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFuenteI" runat="server"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; height: 40px;">
                                    <asp:Button ID="btnAceptarI" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnAceptarI_Click" Text="Aceptar" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </asp:Panel>
                    <table style="width: 1360px; height: 50px;">
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnAceptar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Registrar Evaluación" Width="200px" OnClientClick="this.disabled = true; this.value='Procesando…';" UseSubmitBehavior="false" OnClick ="btnAceptar_Click" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button ID="btnNuevo" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnNuevo_Click" OnClientClick="this.disabled = true; this.value='Procesando…';" Text="Nueva Evaluación" UseSubmitBehavior="false" Width="200px" />
                            </td>
                        </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
