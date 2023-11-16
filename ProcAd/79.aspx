<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="79.aspx.vb" Inherits="ProcAd._79" %>
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


        .auto-style5 {
            width: 194px;
        }


    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1360px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000;">
                <ig:WebScriptManager ID="wsm79" runat="server">
                </ig:WebScriptManager>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtMes" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtAño" runat="server" Width="15px" Visible="False"></asp:TextBox>
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
                                        <td style="width: 150px; text-align: right;">
                                            <asp:Label ID="lbl_MesEval" runat="server" ForeColor="#000066" Text="Mes a evaluar:"></asp:Label>
                                        </td>
                                        <td style="width: 130px;">
                                            <asp:Label ID="lblMesEval" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label ID="lbl_Evaluaciones" runat="server" Font-Bold="True" ForeColor="White">EVALUACIONES</asp:Label>
                                        </td>
                                        <td style="width: 150px; text-align: right;">
                                            &nbsp;</td>
                                        <td style="width: 130px;">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td style="width: 10px">&nbsp;</td>
                            <td>
                                <table style="width: 1320px; height: 35px;">
                                    <tr>
                                        <td style="text-align: center; width: 300px;">
                                            <asp:Button ID="btnExportar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Exportar" Width="150px" />
                                        </td>
                                        <td style="text-align: center">&nbsp;</td>
                                        <td style="text-align: center; width: 300px;">
                                            <asp:Button ID="btnAceptar" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnAceptar_Click" OnClientClick="this.disabled = true; this.value='Procesando…';" Text="Evaluaciones Procesadas" UseSubmitBehavior="false" Width="200px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10px">&nbsp;</td>
                            <td>
                                <asp:GridView ID="gvEvaluaciones" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1320px">
                                    <Columns>
                                        <asp:BoundField DataField="id_ms_evaluacion" HeaderText="id_ms_evaluacion" />
                                        <asp:BoundField DataField="no_empleado" HeaderText="No. Empleado">
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="colaborador" HeaderText="Colaborador">
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="lider" HeaderText="Líder">
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="area" HeaderText="Área">
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="unidad_neg" HeaderText="Unidad Negocio">
                                        <ItemStyle HorizontalAlign="Left" Width="110px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="puesto" HeaderText="Puesto">
                                        <ItemStyle HorizontalAlign="Left" Width="170px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="puesto_lider" HeaderText="Puesto Líder">
                                        <ItemStyle HorizontalAlign="Left" Width="170px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="mes_eval" HeaderText="Mes Eval.">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="porcent_cumpl" DataFormatString="{0:p}" HeaderText="Cumpl.">
                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cobra_bono_asist" HeaderText="Bono Asistencia">
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
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
