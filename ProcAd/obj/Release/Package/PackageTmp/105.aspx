<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="105.aspx.vb" Inherits="ProcAd._105" %>
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
        .auto-style10 {
            width: 140px;
            height: 21px;
        }
        .auto-style11 {
            width: 300px;
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000;">
                <ig:WebScriptManager ID="wsm105" runat="server">
                </ig:WebScriptManager>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsInst" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtCorreoAut" runat="server" Width="15px" Visible="False"></asp:TextBox>
                 <asp:TextBox ID="_txtMontoSol" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:UpdatePanel ID="upLitError" runat="server">
                    <ContentTemplate>
                        <asp:Literal ID="litError" runat="server"></asp:Literal>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:Label ID="lblMessage" runat="server" Font-Names="Verdana" Font-Size="8pt" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlInicio" runat="server">
                    <table style="width: 1366px; height: 25px;">
                        <tr>
                            <td>&nbsp;</td>
                            <td style="width: 150px; text-align: right">
                                <asp:Label ID="lbl_Folio" runat="server" Text="Folio:"></asp:Label>
                            </td>
                            <td style="width: 130px">
                                <asp:Label ID="lblFolio" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1366px; height: 22px;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Solicitante" runat="server" Text="Solicitante:"></asp:Label>
                            </td>
                            <td class="auto-style11">
                                <asp:Label ID="lblSolicitante" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td class="auto-style10" style="text-align: right">
                                <asp:Label ID="lbl_Empresa" runat="server" Text="Empresa:"></asp:Label>
                            </td>
                            <td class="auto-style11">
                                <asp:UpdatePanel ID="upEmpresa" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlEmpresa" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="180px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="auto-style10" style="text-align: right; ">
                                <asp:Label ID="lbl_CC" runat="server" Text="Centro de Costo:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <asp:UpdatePanel ID="upCC" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlCC" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="280px" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Año" runat="server" Text="Año:"></asp:Label>
                            </td>
                            <td style="width: 130px">
                                <asp:UpdatePanel ID="upAño" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlAño" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="width: 80px; text-align: right;">
                                <asp:Label ID="lbl_Mes" runat="server" Text="Mes:"></asp:Label>
                            </td>
                            <td style="width: 210px">
                                <asp:UpdatePanel ID="upMes" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlMes" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="180px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="width: 120px; text-align: right;">
                                <asp:Label ID="lbl_MontoAct" runat="server" Text="Monto Actual:"></asp:Label>
                            </td>
                            <td style="width: 210px">
                                <asp:UpdatePanel ID="upMontoAct" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <ig:WebCurrencyEditor ID="wceMontoAct" runat="server" Font-Names="Verdana" Font-Size="8pt" ReadOnly="True">
                                        </ig:WebCurrencyEditor>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="width: 120px; text-align: right;">
                                <asp:Label ID="lbl_MontoSol" runat="server" Text="Monto Solicitado:"></asp:Label>
                            </td>
                            <td style="width: 180px">
                                <ig:WebCurrencyEditor ID="wceMontoSol" runat="server" Font-Names="Verdana" Font-Size="8pt">
                                </ig:WebCurrencyEditor>
                            </td>
                            <td>
                                <asp:Button ID="btnAgregar" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnAgregar_Click" Text="Agregar" UseSubmitBehavior="false" Width="80px" />
                            </td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">&nbsp;</td>
                            <td>
                                <asp:GridView ID="gvDtAmpliacion" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="760px">
                                    <Columns>
                                        <asp:BoundField DataField="id_dt_ampliacion_p" HeaderText="id" />
                                        <asp:BoundField DataField="año" HeaderText="Año">
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="mes" HeaderText="Mes">
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="monto_actual" HeaderText="Monto Actual" DataFormatString="{0:c}">
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="monto_solicita" HeaderText="Monto Solicitado" DataFormatString="{0:c}">
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="monto_nuevo" HeaderText="Nuevo Monto" DataFormatString="{0:c}">
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="impacto_pres_monto" HeaderText="Impacto Monto" DataFormatString="{0:c}">
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="impacto_pres_porcent" HeaderText="Impacto %" DataFormatString="{0:p}">
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:CommandField ButtonType="Image" SelectImageUrl="images\Trash15.png" ShowSelectButton="True">
                                        <ItemStyle Width="10px" />
                                        </asp:CommandField>
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
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <table style="width: 1366px; height: 44px;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_JustMotivo" runat="server" Text="Motivo del cambio:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtJustMotivo" runat="server" Height="52px" TextMode="MultiLine" Width="1072px" MaxLength="350"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_JustBeneficio" runat="server" Text="Beneficios en caso de autorizar el cambio:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtJustBeneficio" runat="server" Height="52px" MaxLength="350" TextMode="MultiLine" Width="1072px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_JustImpacto" runat="server" Text="Implicaciones en caso de no autorizar el cambio:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtJustImpacto" runat="server" Height="52px" MaxLength="350" TextMode="MultiLine" Width="1072px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1366px; height: 44px;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Archivo" runat="server" Text="Archivo:"></asp:Label>
                            </td>
                            <td>
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width: 450px">
                                            <asp:FileUpload ID="fuAdjunto" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="430px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnAgregarAdj" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnAgregarAdj_Click" OnClientClick="this.disabled = true; this.value='Procesando…';" Text="Agregar Archivo" UseSubmitBehavior="false" />
                                        </td>
                                    </tr>
                                </table>
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
                                        <td style="width: 150px; text-align: right; vertical-align: top;">&nbsp;</td>
                                        <td style="vertical-align: top">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1366px; height: 50px;">
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnGuardar" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnGuardar_Click" OnClientClick="this.disabled = true; this.value='Procesando…';" Text="Solicitar Autorización" UseSubmitBehavior="false" Width="200px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
