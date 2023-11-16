<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="43.aspx.vb" Inherits="ProcAd._43" %>
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
                <ig:WebScriptManager ID="wsm43" runat="server">
                </ig:WebScriptManager>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdDtServ" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtRFCEmpr" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsInst" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtCotUnica" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtCotizaciones" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtContrato" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtServNeg" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtCuentaCont" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdValidador" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdValidador2" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtFinanzas" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtValPresup" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdAutorizador1" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdAutorizador2" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdAutorizador3" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtRFCProv" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtTope1" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtTope2" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtTipoMov" runat="server" Width="15px" Visible="False"></asp:TextBox>

                
                <asp:UpdatePanel ID="upLitError" runat="server">
                    <ContentTemplate>
                        <asp:Literal ID="litError" runat="server"></asp:Literal>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="upRFCContrato" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="_txtRFCContrato" runat="server" Visible="False" Width="15px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="upBanConfig" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="_txtBanConfig" runat="server" Visible="False" Width="15px"></asp:TextBox>
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
                            <td class="auto-style9">
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
                        <tr>
                            <td style="text-align: right; " class="auto-style5">
                                <asp:Label ID="lbl_TipoServicio" runat="server" Text="Tipo de Servicio:"></asp:Label>
                            </td>
                            <td class="auto-style11">
                                <asp:UpdatePanel ID="upTipoServicio" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlTipoServicio" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="280px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="text-align: right; " class="auto-style10">
                                </td>
                            <td class="auto-style11">
                                <asp:UpdatePanel ID="upAdmonOper" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:RadioButtonList ID="rblAdmonOper" runat="server" AutoPostBack="True" RepeatColumns="2" Width="240px">
                                            <asp:ListItem Value="Admon">Administrativo</asp:ListItem>
                                            <asp:ListItem Value="Oper">Operativo</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="text-align: right; " class="auto-style10">
                                <asp:Label ID="lbl_Div" runat="server" Text="División:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <asp:UpdatePanel ID="upDiv" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlDiv" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="280px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align: top;">
                                <asp:Label ID="lbl_Servicio" runat="server" Text="Servicio:"></asp:Label>
                            </td>
                            <td style="vertical-align: top">
                                <asp:UpdatePanel ID="upServicio" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlServicio" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="280px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="text-align: right; vertical-align: top;">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="height: 20px">
                                            <asp:UpdatePanel ID="up_Validador" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label ID="lbl_Validador" runat="server" Text="Validador:"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 20px">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align: top;">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="height: 20px">
                                            <asp:UpdatePanel ID="upValidador" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlValidador" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="280px">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 20px">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                            <td style="text-align: right; vertical-align: top;">
                                <asp:UpdatePanel ID="up_Proveedor" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table style="width:100%;">
                                            <tr>
                                                <td style="height: 20px">
                                                    <asp:Label ID="lbl_Proveedor" runat="server" Text="Proveedor:"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 20px">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="vertical-align: top;">
                                <asp:UpdatePanel ID="upProveedor" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlProveedor" runat="server">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtProveedor" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="200px"></asp:TextBox>
                                                        &nbsp;<asp:ImageButton ID="ibtnBuscarProv" runat="server" AutoPostBack="True" Height="16px" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:UpdatePanel ID="upProveedores" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="ddlProveedor" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="280px">
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">&nbsp;</td>
                            <td>
                                <asp:UpdatePanel ID="upCbAltaContrato" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="cbAltaContrato" runat="server" AutoPostBack="True" Text="Alta de Contrato NAV" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="text-align: right">
                                <asp:UpdatePanel ID="upCbContratoNav" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="cbContratoNav" runat="server" AutoPostBack="True" Text="Contrato NAV Reg." />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td colspan="3">
                                <asp:UpdatePanel ID="upFiltroContrato" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlFiltroContrato" runat="server">
                                            <table style="width:570px;">
                                                <tr>
                                                    <td style="width: 100px">
                                                        <asp:TextBox ID="txtContratoNAV" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 20px">
                                                        <asp:UpdatePanel ID="upBuscarContrato" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="ibtnBuscarContrato" runat="server" Height="16px" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td style="text-align: right">
                                                        <asp:UpdatePanel ID="upDdlContrato" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="ddlContrato" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="420px">
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1366px; height: 44px;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Especificaciones" runat="server" Text="Especificaciones:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEspecificaciones" runat="server" Height="52px" TextMode="MultiLine" Width="1072px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Archivo" runat="server" Text="Archivo:"></asp:Label>
                            </td>
                            <td>
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width: 520px">
                                            <asp:FileUpload ID="fuAdjunto" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="500px" />
                                        </td>
                                        <td style="width: 105px">
                                            <asp:DropDownList ID="ddlTipoArchivo" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="90px">
                                                <asp:ListItem Value="A">Adjunto</asp:ListItem>
                                                <asp:ListItem Value="E">Evidencia</asp:ListItem>
                                            </asp:DropDownList>
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
                                        <td style="width: 150px; text-align: right; vertical-align: top;">
                                            <asp:UpdatePanel ID="up_AdjuntosReq" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label ID="lbl_AdjuntoReq" runat="server" Text="Adjuntos Requeridos:"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="vertical-align: top">
                                            <asp:UpdatePanel ID="upAdjuntosReq" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
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
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:UpdatePanel ID="upFactura" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnlFactura" runat="server">
                                <table style="width: 1366px; height: 22px;">
                                    <tr>
                                        <td class="auto-style5" style="text-align: right">
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
                                        <td class="auto-style10" style="text-align: right">
                                            <asp:UpdatePanel ID="up_Autorizador2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label ID="lbl_Autorizador2" runat="server" Text="Segundo Autorizador:"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td class="auto-style9">
                                            <asp:UpdatePanel ID="upAutorizador2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlAutorizador2" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="280px">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td class="auto-style10" style="text-align: right; ">
                                            <asp:UpdatePanel ID="up_Autorizador3" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label ID="lbl_Autorizador3" runat="server" Text="Tercer Autorizador:"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td class="auto-style8">
                                            <asp:UpdatePanel ID="upAutorizador3" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlAutorizador3" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="280px">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 1366px; height: 44px;">
                                    <tr>
                                        <td class="auto-style5" style="text-align: right">
                                            <asp:Label ID="lbl_Factura" runat="server" Text="Facturas:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="upFacturas" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvFacturas" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1072px" AllowSorting="true" OnSorting="gvFacturas_Sorting" DataKeyNames="id_dt_factura,importe">
                                                        <Columns>
                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                            <ItemStyle Width="15px" />
                                                            </asp:CommandField>
                                                            <asp:BoundField DataField="Factura" HeaderText="Fact." Visible ="False" />
                                                            <asp:HyperLinkField DataNavigateUrlFields="path" DataTextField="Factura" HeaderText="Fact.">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                            </asp:HyperLinkField>
                                                            <asp:BoundField DataField="fecha_emision" HeaderText="Fecha Emisión" SortExpression="fecha_emision">
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
                                                            <%--<asp:BoundField DataField="importe" HeaderText="importe" />--%>
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
                                <table style="width:100%;">
                                    <tr>
                                        <td class="auto-style5" style="text-align: right">&nbsp;</td>
                                        <td style="width: 700px">&nbsp; </td>
                                        <td style="width: 100px; text-align: right;">
                                            <asp:UpdatePanel ID="upCbAF" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:CheckBox ID="cbAF" runat="server" AutoPostBack="True" Text="AF:" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="upFiltroAF" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Panel ID="pnlFiltroAF" runat="server">
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td style="width: 110px">
                                                                    <asp:TextBox ID="txtAF" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 20px">
                                                                    <asp:UpdatePanel ID="upBuscarAF" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:ImageButton ID="ibtnBuscarAF" runat="server" Height="16px" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                                <td style="width: 130px; text-align: right">
                                                                    <asp:UpdatePanel ID="upDdlAF" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="ddlAF" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:UpdatePanel ID="upAgregarAF" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:Button ID="btnAgregarAF" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnAgregarAF_Click" OnClientClick="this.disabled = true; this.value='Procesando…';" Text="Agregar AF" UseSubmitBehavior="false" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; vertical-align: top;">
                                            <asp:Label ID="lbl_Evidencia" runat="server" Text="Evidencias:"></asp:Label>
                                        </td>
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
                                        <td>
                                            <asp:UpdatePanel ID="upAF" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvAF" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="280px">
                                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                                        <Columns>
                                                            <asp:BoundField DataField="no_economico" HeaderText="Económico">
                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
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
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>


                     <%-- Panel división de factura --%>
                    
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:Panel runat ="server" ID="pnlCBDividirFact" Visible ="false">
                                <table>
                        <tr>
                            <td class="auto-style5" style="text-align: right; vertical-align: top">
                             <asp:UpdatePanel ID="updtPanelDivision" runat="server" UpdateMode="Conditional">
                               <ContentTemplate>
                                        <asp:Panel runat="server" ID="pnlcbDividir">
                                             <td>
                                <asp:CheckBox ID="cbDividirFact" runat="server" AutoPostBack="True" Text="Dividir factura" width="120px"/>
                                </td>
                                        </asp:Panel>
                                    </ContentTemplate>
                                                             </asp:UpdatePanel>
                            </td>

                            <td>
                                <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upDividirFactura">
                                    <ContentTemplate>
                                        <asp:Panel runat="server" ID="pnlDividirFactura" Visible="false">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table style="border: thin solid #000000; width: 1200px; height: 44px;">
                                                            <tr>
                                                                <td style="text-align: right;" width="90px" class="auto-style12">
                                                                    <asp:Label ID="lbl_TipoMov" runat="server" Text="Tipo de Mov.:"></asp:Label>
                                                                </td>
                                                                <td width="80px" style="text-align:left">
                                                                    <asp:Label ID="lblTipoMov" runat="server" Font-Bold="True"></asp:Label>
                                                                </td>
                                                                <td style="text-align: right;" width="70px" class="auto-style12">
                                                                    <asp:Label ID="lbl_TipoAsig" runat="server" Text="Asig. por:"></asp:Label>
                                                                </td>
                                                                <td colspan="2"; width="100px">
                                                                    <asp:RadioButtonList ID="rblTipoAsig" runat="server" AutoPostBack="True" RepeatColumns="2" Width="180px">
                                                                        <asp:ListItem Value="P">Porcentaje</asp:ListItem>
                                                                        <asp:ListItem Value="I">Importe</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                                <td width="80px"></td>
                                                                <td style="text-align: right;" width="60px"></td>
                                                                <td width="60px"></td>
                                                                <td width="110px"></td>
                                                                <td width="60px"></td>
                                                                <td width="50px"></td>
                                                                <td width="60px"></td>
                                                                <td width="5px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right; width: 90px;">
                                                                    <asp:Label ID="lbl_CuentaC" runat="server" Text="Cuenta Contable:"></asp:Label>
                                                                </td>
                                                                <td style="width: 80px">
                                                                    <asp:TextBox ID="txtCuentaC1" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxLength="4" Width="30px"></asp:TextBox>
                                                                    <asp:Label ID="lbl_CuentaCG" runat="server" Text="-"></asp:Label>
                                                                    <asp:TextBox ID="txtCuentaC2" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxLength="4" Width="30px"></asp:TextBox>
                                                                </td>
                                                                <td style="text-align: right;" width="70px">
                                                                    <asp:Label ID="lbl_Porcent" runat="server" Text="Porcentaje:"></asp:Label>
                                                                </td>
                                                                <td style="width:90px">
                                                                    <ig:WebNumericEditor ID="wnePorcent" runat="server" MaxDecimalPlaces="2" Width="75px">
                                                                    </ig:WebNumericEditor>
                                                                    <asp:Label ID="lblPorcent" runat="server" Visible="False"></asp:Label>
                                                                   
                                                                </td>
                                                                <td style="width: 130px; text-align: left;">
                                                                    <asp:Label ID="lbl_Importe" runat="server" Text="Importe:"></asp:Label>&nbsp;
                                                                    <ig:WebCurrencyEditor ID="wceImporte" runat="server" Width="80px">
                                                                    </ig:WebCurrencyEditor>
                                                                </td>
                                                             
                                                                <td style="text-align: right; width: 80px;">
                                                                    <asp:CheckBox ID="cbCC" runat="server" Text="Centro de Costo" />
                                                                </td>
                                                                <td style="width: 60px">
                                                                    <asp:TextBox ID="txtCC" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="50px"></asp:TextBox>
                                                                </td>
                                                                <td style="text-align: right; width: 65px;">
                                                                    <asp:CheckBox ID="cbDiv" runat="server" Text="División" AutoPostBack ="true" />
                                                                </td>
                                                                <td style="width: 60px">
                                                                 <%-- <asp:DropDownList runat="server" ID ="ddlDivD" Width ="130px" AutoPostBack="true"></asp:DropDownList>--%>
                                                                    <asp:UpdatePanel runat="server" ID="upDivDDl" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList runat="server" ID="ddlDivD" Width="130px" AutoPostBack="true"></asp:DropDownList>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                                <td style="width: 50px; text-align: right;">
                                                                    <asp:CheckBox ID="cbZona" runat="server" Text="Zona" />
                                                                </td>
                                                                <td style="width: 60px">
                                                                    <asp:TextBox ID="txtZona" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="60px"></asp:TextBox>
                                                                </td>
                                                                <td style="text-align: center;width:60px">
                                                                    <asp:Button ID="btnAceptarDiv" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClientClick="this.disabled = true;" Text="Aceptar" UseSubmitBehavior="false" Width="60px" style="height: 21px" />
                                                                </td>
                                                                 <td style="text-align: center;width:5px"></td>

                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <td style="text-align: right; width: 90px;">
                                                                </td>
                                                                <td style="width: 80px">
                                                                    </td>
                                                                <td style="text-align: right;" width="70px">
                                                                </td>
                                                                <td style="width:90px">
                                                                    
                                                                </td>
                                                                <td style="width: 120px; text-align: left;">
                                                                    
                                                                </td>
                                                             
                                                                <td style="text-align: right; width: 60px;">
                                                                </td>
                                                                <td style="width: 60px">
                                                                </td>
                                                                    <asp:UpdatePanel runat="server" ID="upDivD" UpdateMode ="Conditional" >
                                                                        <ContentTemplate>
                                                                            <td style="text-align: left; width: 130px;">
                                                                                <asp:TextBox ID="txtDiv" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="60px"></asp:TextBox>
                                                                            </td>
                                                                        </ContentTemplate>

                                                                    </asp:UpdatePanel>
                                                                
                                                                <td style="width: 60px">
                                                                </td>
                                                                <td style="width: 50px; text-align: right;">
                                                                </td>
                                                                <td style="width: 60px">
                                                                </td>
                                                                <td style="text-align: center;width:5px">
                                                                </td>
                                                                
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                    <table>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:Label ID="lbl_PorcentAsig" runat="server" Text="Porcentaje Asignado:" Width="125px" Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPorcentAsig" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                            </td>

                                        </tr>
                                    </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                   
                    <%-- Fin panel division de factura --%>

                     <%-- Gridview division de factura  --%>

                    <table>
                       <tr>
                           <td></td>
                       </tr>
                        <tr>
                            <td class="auto-style5"> </td>
                            <td>
                                <asp:UpdatePanel runat="server" ID="upGvFacturaDiv">
                                <ContentTemplate >
                                    <asp:Panel runat="server" ID="pnlgvFacturaDividida" Visible="false" >
                                        <table>
                                            <tr>
                                                <td class="auto-style5"></td>
                                                <td>
                                                    <asp:GridView ID="gvFacturaDividida" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="600px" DataKeyNames="id_dt_factura_div, id_dt_factura, id_ms_factura">
                                                        <Columns>
                                                           <%-- <asp:BoundField DataField="id_dt_factura_div" HeaderText="id_dt_factura_div" />
                                                            <asp:BoundField DataField="id_dt_factura" HeaderText="id_dt_factura" />
                                                            <asp:BoundField DataField="id_ms_factura" HeaderText="id_ms_factura" />--%>
                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                <ItemStyle Width="15px" />
                                                            </asp:CommandField>
                                                            <asp:BoundField DataField="cuenta_c" HeaderText="Cuenta Contable">
                                                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Importe" HeaderText="Monto" DataFormatString="{0:c}">
                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                            </asp:BoundField> 
                                                            <asp:BoundField DataField="porcent" HeaderText="Porcentaje">
                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                            </asp:BoundField> 
                                                            <asp:BoundField DataField="centro_costo" HeaderText="Centro de costo" >
                                                                <ItemStyle HorizontalAlign="Center" Width="140px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="division" HeaderText="Division">
                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="zona" HeaderText="Zona">
                                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
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
                                                <td style="vertical-align: top; text-align: center; width: 30px;">
                                                    <asp:ImageButton ID="ibtnAlta" runat="server" ImageUrl="images\Add.png" ToolTip="Agregar" Width="20px" Visible ="false" />
                                                </td>
                                                <td style="vertical-align: top; text-align: center; width: 30px;">
                                                    <asp:ImageButton ID="ibtnModif" runat="server" ImageUrl="images\Edit.png" ToolTip="Modificar" Width="20px" />
                                                </td>
                                                <td style="vertical-align: top; text-align: center; width: 30px;">
                                                    <asp:ImageButton ID="ibtnEliminar" runat="server" ImageUrl="images\Trash.png" ToolTip="Eliminar" Width="20px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>

                    <%-- Fin del grid division factura --%>

                    <table style="width: 1366px; height: 50px;">
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnGuardar" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnGuardar_Click" OnClientClick="this.disabled = true; this.value='Procesando…';" Text="Solicitar Validación" UseSubmitBehavior="false" Width="200px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
