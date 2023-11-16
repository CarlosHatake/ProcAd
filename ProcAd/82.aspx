<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="82.aspx.vb" Inherits="ProcAd._82" %>
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
                <ig:WebScriptManager ID="wsm73" runat="server">
                </ig:WebScriptManager>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:TextBox ID="_txtIdDireccion" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdDtArea" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsEvaluacionA" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsInst" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdUsuarioE" runat="server" Width="15px" Visible="False"></asp:TextBox>
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
                                            <asp:Label ID="lbl_MesEval" runat="server" ForeColor="#000066" Text="Mes a evaluar:"></asp:Label>
                                        </td>
                                        <td style="width: 130px;">
                                            <asp:Label ID="lblMesEval" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1360px; height: 22px;">
                        <tr>
                            <td style="text-align: right; width: 150px; height: 21px;">
                                <asp:Label ID="lbl_Lider" runat="server" Text="Líder evaluador:"></asp:Label>
                            </td>
                            <td style="width: 300px;">
                                <asp:Label ID="lblLider" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 140px; height: 21px;">
                                <asp:Label ID="lbl_PuestoLider" runat="server" Text="Puesto del Líder:"></asp:Label>
                            </td>
                            <td style="width: 320px;">
                                <asp:Label ID="lblPuestoLider" runat="server" ForeColor="Blue" Width="300px"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 100px; height: 21px;">
                                <asp:Label ID="lbl_Area" runat="server" Text="Área:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblArea" runat="server" ForeColor="Blue" Width="300px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td style="width: 150px; text-align: right;">
                                <asp:Label ID="lbl_Evidencias" runat="server" Text="Evidencias:"></asp:Label>
                            </td>
                            <td style="text-align: left; ">
                                <asp:GridView ID="gvEvidencias" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ShowHeader="False" Width="450px">
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
                        <tr>
                            <td>&nbsp;</td>
                            <td style="text-align: left">&nbsp;</td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlIndicadores" runat="server">
                        <table style="width: 1360px; height: 25px;">
                            <tr>
                                <td style="text-align: center; background-color: #4F81BD">
                                    <asp:Label ID="lbl_IndAsig" runat="server" Font-Bold="True" ForeColor="White">EVALUACIONES</asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table style="width:100%;">
                            <tr>
                                <td style="width: 10px">&nbsp;</td>
                                <td>
                                    <asp:GridView ID="gvEvaluaciones" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1325px">
                                        <Columns>
                                            <asp:BoundField DataField="id_ms_evaluacion" HeaderText="id_ms_evaluacion" />
                                            <asp:BoundField DataField="no_empleado" HeaderText="No. Empleado">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="colaborador" HeaderText="Colaborador">
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="centro_costo" HeaderText="Centro de Costo">
                                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="puesto" HeaderText="Puesto">
                                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="base" HeaderText="Base">
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="porcent_cumpl" DataFormatString="{0:p}" HeaderText="Cumplimiento">
                                            <ItemStyle HorizontalAlign="Center" Width="110px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cobra_bono_asist" HeaderText="Bono Asistencia">
                                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cobra_bono_cumpl_UN" HeaderText="Bono Cumpl UN">
                                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="invalida" HeaderText="Inválida">
                                            <ItemStyle HorizontalAlign="Center" Width="60px" />
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
                    </asp:Panel>
                    <table style="width: 1360px; height: 50px;">
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnAutorizar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Autorizar" Width="200px" OnClientClick="this.disabled = true; this.value='Procesando…';" UseSubmitBehavior="false" OnClick ="btnAutorizar_Click" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button ID="btnRechazar" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnRechazar_Click" OnClientClick="this.disabled = true; this.value='Procesando…';" Text="Rechazar" UseSubmitBehavior="false" Width="200px" />
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
