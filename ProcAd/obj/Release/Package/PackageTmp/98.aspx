<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="98.aspx.vb" Inherits="ProcAd._98" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style5 {
            width: 150px;
            height: 21px;
        }
        .auto-style8 {
            height: 21px;
        }
        .auto-style9 {
            width: 300px;
        }
        .auto-style10 {
            width: 140px;
            height: 21px;
        }
    .igte_EditWithButtons
{
	background-color:White;
	font-size:12px;
	font-family: Segoe UI, Verdana, Arial, Sans-Serif;
	border:solid 1px #CCCCCC;
	height: 24px;
	width: 130px;
	outline: 0;
}


.igte_Inner
{
	border-width:0px;
}


.igte_EditInContainer
{
	background-color:Transparent;
	font-size:12px;
	font-family: Segoe UI, Verdana, Arial, Sans-Serif;
	border-width:0px;
	outline: 0;
	color:#333333;
}


.igte_Button
{
	background-color:#4F4F4F;
	border-style:none;
	line-height: normal;
	cursor:pointer;
	color:White;
}


.igte_ButtonSize
{
	width: 22px;
	height: 22px;
}


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
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000;">
                <ig:WebScriptManager ID="wsm98" runat="server">
                </ig:WebScriptManager>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsInst" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdDtServ" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlInicio" runat="server">
                    <table style="width: 1366px; height: 25px;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                &nbsp;</td>
                            <td style="width: 120px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
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
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Solicitante" runat="server" Text="Solicitante:"></asp:Label>
                            </td>
                            <td class="auto-style9">
                                <asp:Label ID="lblSolicitante" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td class="auto-style10" style="text-align: right">
                                <asp:Label ID="lbl_Empresa" runat="server" Text="Empresa:"></asp:Label>
                            </td>
                            <td style="width: 210px">
                                <asp:Label ID="lblEmpresa" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 80px;">
                                <asp:Label ID="lbl_Dimension" runat="server" Text="Dimension:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <asp:Label ID="lblDimension" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; " class="auto-style5">
                                <asp:Label ID="lbl_Servicio" runat="server" Text="Servicio:"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblServicio" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Proveedor" runat="server" Text="Proveedor:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblProveedor" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5" style="text-align: right; ">
                                <asp:Label ID="lbl_Base" runat="server" Text="Base:"></asp:Label>
                            </td>
                            <td class="auto-style9">
                                <asp:Label ID="lblBase" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td class="auto-style10" style="text-align: right; ">
                                <asp:Label ID="lbl_Lugar" runat="server" Text="Lugar de Ejecución:"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblLugar" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td class="auto-style5" style="text-align: right; vertical-align: top;">
                                <asp:Label ID="lbl_Cantidad" runat="server" Text="Cantidad:"></asp:Label>
                            </td>
                            <td style="width: 770px">
                                <asp:GridView ID="gvDtFacturaSN" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="760px">
                                    <Columns>
                                        <asp:BoundField DataField="id_dt_factura_sn" HeaderText="id" />
                                        <asp:BoundField DataField="cantidad" DataFormatString="{0:n}" HeaderText="Cantidad">
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="no_economico" HeaderText="No. Económico">
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo" HeaderText="Tipo">
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="modelo" HeaderText="Modelo">
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="placas" HeaderText="Placas">
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="div" HeaderText="DIV">
                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="division" HeaderText="División">
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="zn" HeaderText="ZN">
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
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
                            <td style="width: 135px; text-align: right; vertical-align: top;">
                                <asp:Label ID="lbl_Soportes" runat="server" Text="Soporte de Ampliación de Presupuesto:" Width="130px"></asp:Label>
                            </td>
                            <td style="vertical-align: top">
                                <asp:GridView ID="gvSoportes" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ShowHeader="False" Width="280px">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <Columns>
                                        <asp:HyperLinkField DataNavigateUrlFields="path" DataTextField="archivo" HeaderText="Archivo">
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
                    <table style="width: 1366px; height: 44px;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Descripcion" runat="server" Text="Descripción:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescripcion" runat="server" Height="52px" TextMode="MultiLine" Width="1100px" Font-Names="Verdana" Font-Size="8pt" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align: top;">
                                <asp:Label ID="lbl_Adjunto" runat="server" Text="Adjuntos:"></asp:Label>
                            </td>
                            <td>
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width: 600px; vertical-align: top">
                                            <asp:UpdatePanel ID="upAdjuntos" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvAdjuntos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ShowHeader="False" Width="350px">
                                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                                        <Columns>
                                                            <asp:HyperLinkField DataNavigateUrlFields="path" DataTextField="archivo" HeaderText="Adjuntos">
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
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="width: 150px; text-align: right; vertical-align: top;">
                                            <asp:Label ID="lbl_AdjuntoReq" runat="server" Text="Adjuntos Requeridos:"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top">
                                            <asp:GridView ID="gvAdjuntosReq" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ShowHeader="False" Width="400px">
                                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                                <Columns>
                                                    <asp:BoundField DataField="adjunto" HeaderText="Adjunto" />
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
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1366px; height: 44px;">
                        <tr>
                            <td style="text-align: right" class="auto-style5">
                                <asp:Label ID="lbl_Validador" runat="server" Text="Validador:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblValidador" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_ComentarioVal" runat="server" Text="Comentarios Validador:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtComentarioVal" runat="server" Height="52px" MaxLength="350" ReadOnly="True" TextMode="MultiLine" Width="1072px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_ComentarioAF" runat="server" Text="Comentarios del Gerente de Administración:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtComentarioAF" runat="server" Height="52px" MaxLength="350" ReadOnly="True" TextMode="MultiLine" Width="1072px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Comentario" runat="server" Text="Comentarios:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtComentario" runat="server" Height="52px" MaxLength="350" TextMode="MultiLine" Width="1072px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <table style="width: 1366px; height: 50px;">
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnAceptar" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnAceptar_Click" OnClientClick="this.disabled = true; this.value='Procesando…';" Text="Autorizar" UseSubmitBehavior="false" Width="200px" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button ID="btnRechazar" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnRechazar_Click" OnClientClick="this.disabled = true; this.value='Procesando…';" Text="Rechazar" UseSubmitBehavior="false" Width="200px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
