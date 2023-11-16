<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="46.aspx.vb" Inherits="ProcAd._46" %>
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
        width: 265px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000;">
                <ig:WebScriptManager ID="wsm46" runat="server">
                </ig:WebScriptManager>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtRFCEmpr" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsInst" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtRFCProv" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtFinanzas" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtValPresup" runat="server" Width="15px" Visible="False"></asp:TextBox>
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
                                <asp:Label ID="lblEmpresa" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td class="auto-style10" style="text-align: right; ">
                                <asp:Label ID="lbl_CC" runat="server" Text="Centro de Costo:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <asp:Label ID="lblCC" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_TipoServicio" runat="server" Text="Servicio:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTipoServicio" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Validador" runat="server" Text="Validador:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblValidador" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_ContratoNAV" runat="server" Text="Alta de Contrato NAV:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblContratoNAV" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; ">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Especificaciones" runat="server" Text="Especificaciones:"></asp:Label>
                            </td>
                            <td style="width: 747px">
                                <asp:TextBox ID="txtEspecificaciones" runat="server" Height="79px" ReadOnly="True" TextMode="MultiLine" Width="723px"></asp:TextBox>
                            </td>
                            <td class="auto-style10" style="text-align: right">
                                <asp:Label ID="lbl_Adjunto" runat="server" Text="Adjuntos:"></asp:Label>
                            </td>
                            <td>
                                <asp:GridView ID="gvAdjuntos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ShowHeader="False" Width="280px">
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
                                <asp:Label ID="lbl_ComentarioVal" runat="server" Text="Comentarios Validador:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <asp:TextBox ID="txtComentarioVal" runat="server" Height="52px" ReadOnly="True" TextMode="MultiLine" Width="1072px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Cotizacion" runat="server" Text="Cotizaciones:"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width: 20px">&nbsp;</td>
                                        <td class="auto-style11">
                                            <asp:Label ID="lbl_Proveedor0" runat="server" Text="Proveedor"></asp:Label>
                                        </td>
                                        <td style="width: 120px">
                                            <asp:Label ID="lbl_Importe" runat="server" Text="Importe"></asp:Label>
                                        </td>
                                        <td style="width: 70px">
                                            <asp:Label ID="lbl_Divisa" runat="server" Text="Divisa"></asp:Label>
                                        </td>
                                        <td style="width: 140px">
                                            <asp:Label ID="lbl_FechaIni" runat="server" Text="Fecha Inicio"></asp:Label>
                                        </td>
                                        <td style="width: 140px">
                                            <asp:Label ID="lbl_FechaFin" runat="server" Text="Fecha de Término"></asp:Label>
                                        </td>
                                        <td style="width: 160px">
                                            <asp:Label ID="lbl_CondP" runat="server" Text="Cond. de Pago" Width="120px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_PDF" runat="server" Text="PDF Cotización" Width="120px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label ID="lbl_No1" runat="server" Text="1"></asp:Label>
                                        </td>
                                        <td style="text-align: center" class="auto-style11">
                                            <table style="width: 210px">
                                                <tr>
                                                    <td style="text-align: left">
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtProveedor1" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="220px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel ID="upBuscarP1" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:ImageButton ID="ibtnBuscarP1" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:UpdatePanel ID="upProveedor1" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="ddlProveedor1" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <ig:WebCurrencyEditor ID="wceImporte1" runat="server" font-names="Verdana" font-size="8pt" maxdecimalplaces="4" minvalue="0.01" width="100px" >
                                            </ig:WebCurrencyEditor>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDivisa1" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="50px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <ig:WebDatePicker ID="wdpFechaIni1" runat="server">
                                            </ig:WebDatePicker>
                                        </td>
                                        <td>
                                            <ig:WebDatePicker ID="wdpFechaFin1" runat="server">
                                            </ig:WebDatePicker>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCondP1" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="140px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="fuPDF1" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="240px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label ID="lbl_No2" runat="server" Text="2"></asp:Label>
                                        </td>
                                        <td style="text-align: center" class="auto-style11">
                                            <table style="width: 205px">
                                                <tr>
                                                    <td style="text-align: left">
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtProveedor2" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="220px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel ID="upBuscarP2" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:ImageButton ID="ibtnBuscarP2" runat="server" ImageUrl="images\Search.png" style="height: 16px" ToolTip="Buscar" Width="15px" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:UpdatePanel ID="upProveedor2" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="ddlProveedor2" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <ig:WebCurrencyEditor ID="wceImporte2" runat="server" font-names="Verdana" font-size="8pt" maxdecimalplaces="4" minvalue="0.01" width="100px">
                                            </ig:WebCurrencyEditor>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDivisa2" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="50px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <ig:WebDatePicker ID="wdpFechaIni2" runat="server">
                                            </ig:WebDatePicker>
                                        </td>
                                        <td>
                                            <ig:WebDatePicker ID="wdpFechaFin2" runat="server">
                                            </ig:WebDatePicker>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCondP2" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="140px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="fuPDF2" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="240px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label ID="lbl_No3" runat="server" Text="3"></asp:Label>
                                        </td>
                                        <td style="text-align: center" class="auto-style11">
                                            <table style="width: 205px">
                                                <tr>
                                                    <td style="text-align: left">
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtProveedor3" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="220px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel ID="upBuscarP3" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:ImageButton ID="ibtnBuscarP3" runat="server" ImageUrl="images\Search.png" style="height: 16px" ToolTip="Buscar" Width="15px" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:UpdatePanel ID="upProveedor3" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="ddlProveedor3" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <ig:WebCurrencyEditor ID="wceImporte3" runat="server" font-names="Verdana" font-size="8pt" maxdecimalplaces="4" minvalue="0.01" width="100px">
                                            </ig:WebCurrencyEditor>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDivisa3" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="50px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <ig:WebDatePicker ID="wdpFechaIni3" runat="server">
                                            </ig:WebDatePicker>
                                        </td>
                                        <td>
                                            <ig:WebDatePicker ID="wdpFechaFin3" runat="server">
                                            </ig:WebDatePicker>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCondP3" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="140px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="fuPDF3" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="240px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_ProvSel" runat="server" Text="Proveedor Elegido:"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblProvSel" runat="server" Font-Names="Verdana" Font-Size="8pt" RepeatColumns="3" Width="170px">
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Comentario" runat="server" Text="Comentarios:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtComentario" runat="server" Height="52px" TextMode="MultiLine" Width="1072px" MaxLength="350"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1366px; height: 50px;">
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnGuardar" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnGuardar_Click" OnClientClick="this.disabled = true; this.value='Procesando…';" Text="Guardar" UseSubmitBehavior="false" Width="200px" />
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
