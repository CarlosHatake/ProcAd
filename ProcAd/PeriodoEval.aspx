<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="PeriodoEval.aspx.vb" Inherits="ProcAd.PeriodoEval" %>
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
            height: 21px;
            width: 150px;
        }


    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1360px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000;">
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlInicio" runat="server">
                    <table style="width: 1360px; height: 66px;">
                        <tr>
                            <td style="text-align: right; " class="auto-style5">
                                &nbsp;</td>
                            <td style="text-align: right; width: 160px; height: 21px;">
                                <asp:Label ID="lbl_PeriodoAct" runat="server" Text="Periodo Actual:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPeriodoAct" runat="server" ForeColor="Blue" Width="300px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5" style="text-align: right; ">&nbsp;</td>
                            <td style="text-align: right; width: 160px; height: 21px;">
                                <asp:Label ID="lbl_PeriodoNuevo" runat="server" Text="Periodo por Abrir:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPeriodoNuevo" runat="server" ForeColor="Blue" Width="300px"></asp:Label>
                                <asp:Label ID="lblPeriodoNuevoD" runat="server" ForeColor="#009933" Visible="False" Width="300px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1360px; height: 50px;">
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnAceptar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Abrir Periodo" Width="200px" OnClientClick="this.disabled = true; this.value='Procesando…';" UseSubmitBehavior="false" OnClick ="btnAceptar_Click" />
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
