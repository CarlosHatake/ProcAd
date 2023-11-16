﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="101.aspx.vb" Inherits="ProcAd._101" %>
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
                <ig:WebScriptManager ID="wsm101" runat="server">
                </ig:WebScriptManager>
                                <asp:UpdatePanel ID="upLitError" runat="server">
                                    <ContentTemplate>
                                        <asp:Literal ID="litError" runat="server"></asp:Literal>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsInst" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdDtServ" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtRFCEmpr" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdValidador2" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdAutorizador1" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdAutorizador2" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdAutorizador3" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdEmpresa" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtCC" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtDiv" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdProveedor" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtRFCProv" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtTope1" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtTope2" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:Label ID="lblMessage" runat="server" Font-Names="Verdana" Font-Size="8pt" ForeColor="Red"></asp:Label>
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
                            <td style="width: 285px">
                                <asp:Label ID="lblEmpresa" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 130px;">
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
                        <tr>
                            <td class="auto-style5" style="text-align: right; ">
                                <asp:UpdatePanel ID="up_Autorizador" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="lbl_Autorizador" runat="server" Text="Autorizador:"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="auto-style9">
                                <asp:UpdatePanel ID="upAutorizador" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlAutorizador" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="280px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="auto-style10" style="text-align: right; ">
                                <asp:UpdatePanel ID="up_Autorizador2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="lbl_Autorizador2" runat="server" Text="Segundo Autorizador:"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="upAutorizador2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlAutorizador2" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="280px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="text-align: right">
                                <asp:UpdatePanel ID="up_Autorizador3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="lbl_Autorizador3" runat="server" Text="Tercer Autorizador:"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="upAutorizador3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlAutorizador3" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="280px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td class="auto-style5" style="text-align: right; vertical-align: top;">
                                <asp:Label ID="lbl_Cantidad" runat="server" Text="Cantidad:"></asp:Label>
                            </td>
                            <td>
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
                    <table style="width:100%;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Evidencia" runat="server" Text="Evidencias:"></asp:Label>
                            </td>
                            <td style="width: 700px">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width: 515px">
                                            <asp:FileUpload ID="fuEvidencia" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="500px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnAgregarEvid" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnAgregarEvid_Click" OnClientClick="this.disabled = true; this.value='Procesando…';" Text="Agregar Evidencia" UseSubmitBehavior="false" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 100px; text-align: right;">
                                <asp:UpdatePanel ID="up_ValSoporte" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="lbl_ValSoporte" runat="server" Text="Val. Soporte:"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="upValSoporte" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlValSoporte" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="280px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td style="vertical-align: top">
                                <asp:UpdatePanel ID="upEvidencias" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvEvidencias" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ShowHeader="False" Width="350px">
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <table style="width: 1366px; height: 44px;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Factura" runat="server" Text="Factura:"></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="upFacturas" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvFacturas" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1072px">
                                            <Columns>
                                                <asp:BoundField DataField="id_dt_factura" HeaderText="id_dt_factura" />
                                                <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                <ItemStyle Width="15px" />
                                                </asp:CommandField>
                                                <asp:BoundField DataField="fecha_emision" HeaderText="Fecha Emisión">
                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="uuid" HeaderText="UUID">
                                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="serie" HeaderText="Serie">
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="folio" HeaderText="Folio">
                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="lugar_exp" HeaderText="Lugar Exp.">
                                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="forma_pago" HeaderText="Forma Pago">
                                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="moneda" HeaderText="Moneda">
                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="subtotal" DataFormatString="{0:c}" HeaderText="Subtotal">
                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="importe" DataFormatString="{0:c}" HeaderText="Importe">
                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="importe" HeaderText="importe" />
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1366px; height: 50px;">
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnGuardar" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnGuardar_Click" OnClientClick="this.disabled = true; this.value='Procesando…';" Text="Guardar" UseSubmitBehavior="false" Width="200px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
