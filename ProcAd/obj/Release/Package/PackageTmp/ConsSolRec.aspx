<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ConsSolRec.aspx.vb" Inherits="ProcAd.ConsSolRec" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style5 {
            width: 142px;
        }
        .auto-style28 {
            width: 110px;
        }
        .auto-style46 {
            width: 483px;
        }
        .auto-style8 {
            height: 21px;
        }
        .auto-style9 {
            width: 300px;
        }
        

        .auto-style11 {
            width: 300px;
            height: 21px;
        }


        .auto-style10 {
            width: 140px;
            height: 21px;
        }
    

        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; font-weight: bold; color: #FF0000;">
                <ig:WebScriptManager ID="wsmConsComp" runat="server">
                </ig:WebScriptManager>
                <asp:TextBox ID="_txtIdSolicito" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtEmpleado" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtPerfil" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <table style="width: 1366px">
                    <tr>
                        <td>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlFiltros" runat="server" Width="1360px">
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: right; width: 80px;">&nbsp;</td>
                            <td style="text-align: center; " class="auto-style5">
                                <asp:Label ID="lbl_Filtros" runat="server" Font-Bold="True" Text="Filtros..."></asp:Label>
                            </td>
                            <td >&nbsp;</td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlSolicitó" runat="server">
                        <table style="width:100%;">
                            <tr>
                                <td style="text-align: right; width: 80px;">&nbsp;</td>
                                <td style="text-align: left; width: 120px;">
                                    <asp:UpdatePanel ID="upCbEmpresa" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbEmpresa" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Empresa:" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="text-align: left; width: 340px;">
                                    <asp:UpdatePanel ID="upEmpresa" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlEmpresa" runat="server" Width="330px">
                                                <asp:DropDownList ID="ddlEmpresa" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="200px">
                                                </asp:DropDownList>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="text-align: left; width: 120px;">
                                    <asp:UpdatePanel ID="upCbSolicitante" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbSolicitante" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Solicitante:" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upSolicitante" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlSolicitante" runat="server" Width="330px">
                                                <asp:DropDownList ID="ddlSolicitante" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px">
                                                </asp:DropDownList>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: right; width: 80px;">&nbsp;</td>
                            <td style="text-align: left; width: 120px;">
                                <asp:UpdatePanel ID="upCbFechaC" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="cbFechaC" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Fecha Creación:" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="text-align: left; width: 340px;">
                                <asp:UpdatePanel ID="upFechaC" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlFechaC" runat="server" Width="330px">
                                            <table style="width:260px;">
                                                <tr>
                                                    <td style="border: medium inset #808080; margin-right: auto; margin-left: auto;">
                                                        <table style="width: 127px;">
                                                            <tr>
                                                                <td style="text-align: center">
                                                                    <asp:Label ID="lbl_FechaCI" runat="server" Text="Fecha Inicial"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: center">
                                                                    <ig:WebDatePicker ID="wdpFechaI" runat="server" Font-Names="Verdana" Font-Size="8pt" MinValue="2014-08-06" Nullable="False" Width="120px">
                                                                    </ig:WebDatePicker>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td class="auto-style46">&nbsp;</td>
                                                    <td style="border: medium inset #808080; margin-right: auto; margin-left: auto;">
                                                        <table style="width: 127px;">
                                                            <tr>
                                                                <td style="text-align: center">
                                                                    <asp:Label ID="lbl_FechaCF" runat="server" Text="Fecha Final"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: center">
                                                                    <ig:WebDatePicker ID="wdpFechaF" runat="server" Font-Names="Verdana" Font-Size="8pt" MinValue="2014-08-06" Nullable="False" Width="120px">
                                                                    </ig:WebDatePicker>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="text-align: left; width: 120px;">
                                <asp:UpdatePanel ID="upCbAutorizador" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="cbAutorizador" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Autorizador:" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="width: 340px">
                                <asp:UpdatePanel ID="upAutorizador" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlAutorizador" runat="server" Width="330px">
                                            <asp:DropDownList ID="ddlAutorizador" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="width: 120px">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">&nbsp;</td>
                            <td style="text-align: left">
                                <asp:UpdatePanel ID="upCbStatus" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="cbStatus" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Estado:" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="upStatus" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlStatus" runat="server" Width="330px">
                                            <asp:DropDownList ID="ddlStatus" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="180px">
                                                <asp:ListItem Value="'P'">Pendiente</asp:ListItem>
                                                <asp:ListItem Value="'A'">Autorizado</asp:ListItem>
                                                <asp:ListItem Value="'Z'">Rechazado</asp:ListItem>
                                                <asp:ListItem Value="'P','A','Z'">Todos</asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="upCbNoSolRec" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="cbNoSolRec" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="No. de Sol. Rec.:" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="upNoSolRec" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlNoSolRec" runat="server" Width="150px">
                                            <asp:TextBox ID="txtNoSolRec" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                    <table style="width: 100%; text-align: center; margin-right: auto; margin-left: auto; height: 60px;">
                        <tr>
                            <td class="auto-style51">
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="150px" Font-Names="Verdana" Font-Size="8pt" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlRegistros" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td class="auto-style55">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width: 1px">&nbsp;</td>
                                        <td>
                                            <asp:GridView ID="gvRegistros" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1400px">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                    <ItemStyle Width="15px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="id_ms_recursos" HeaderText="No. Sol. Recursos">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Empresa" HeaderText="Empresa">
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="empleado" HeaderText="Solicita">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="autorizador" HeaderText="Autoriza" />
                                                    <asp:BoundField DataField="periodo_ini" HeaderText="Desde" DataFormatString="{0:dd/MM/yyyy}">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="periodo_fin" HeaderText="Hasta" DataFormatString="{0:dd/MM/yyyy}">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="no_anticipo" HeaderText="Folio Anticipo">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="no_reserva" HeaderText="Folio Reserva">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="no_combustible" HeaderText="Folio Combustible">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_solicita" HeaderText="Fecha Solicita">
                                                    <ItemStyle HorizontalAlign="Center" Width="85px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_autoriza" HeaderText="Fecha Autoriza">
                                                    <ItemStyle HorizontalAlign="Center" Width="85px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_vobo" HeaderText="Fecha Vo.Bo.">
                                                    <ItemStyle HorizontalAlign="Center" Width="85px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="estatus" HeaderText="Estado">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
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
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlDetalle" runat="server">
                    <table style="width:1366px;">
                        <tr>
                            <td style="text-align: center; width: 300px; height: 35px;">
                                <asp:Button ID="btnNueBusProd" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Nueva Búsqueda" Width="150px" />
                            </td>
                            <td style="text-align: center; width: 460px; height: 35px;">
                                &nbsp;</td>
                            <td style="text-align: center; width: 210px; height: 35px;">
                                &nbsp;</td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Folio" runat="server" Text="Folio de Solicitud de Recursos:"></asp:Label>
                            </td>
                            <td class="auto-style28">
                                <asp:Label ID="lblFolio" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1360px; height: 22px;">
                        <tr>
                            <td style="text-align: right; " class="auto-style5">
                                <asp:Label ID="lbl_Solicitante" runat="server" Text="Solicitante:"></asp:Label>
                            </td>
                            <td class="auto-style11">
                                <asp:Label ID="lblSolicitante" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td class="auto-style10" style="text-align: right; ">
                                <asp:Label ID="lbl_NoProveedor" runat="server" Text="Num. Proveedor:"></asp:Label>
                            </td>
                            <td class="auto-style11">
                                <asp:Label ID="lblNoProveedor" runat="server" ForeColor="Blue" Width="150px"></asp:Label>
                            </td>
                            <td style="text-align: right; " class="auto-style10">
                                <asp:Label ID="lbl_Empresa" runat="server" Text="Empresa:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <asp:Label ID="lblEmpresa" runat="server" ForeColor="Blue" Width="150px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1360px; height: 22px;">
                        <tr>
                            <td style="text-align: right; width: 120px; height: 21px;">
                                <asp:Label ID="lbl_Orig" runat="server" Text="Origen:"></asp:Label>
                            </td>
                            <td style="width: 190px;">
                                <asp:Label ID="lblOrig" runat="server" ForeColor="Blue" Width="170px"></asp:Label>
                            </td>
                            <td style="width: 60px; height: 21px; text-align: right">
                                <asp:Label ID="lbl_Dest" runat="server" Text="Destino:"></asp:Label>
                            </td>
                            <td style="width: 190px;">
                                <asp:Label ID="lblDest" runat="server" ForeColor="Blue" Width="170px"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 60px; height: 21px;">
                                <asp:Label ID="lbl_Destino0" runat="server" Text="Lugar:"></asp:Label>
                            </td>
                            <td style="width: 280px">
                                <asp:Label ID="lblDestino" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 90px; height: 21px;">
                                <asp:Label ID="lbl_Autorizador" runat="server" Text="Autorizador:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <asp:Label ID="lblAutorizador" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1360px;">
                        <tr>
                            <td rowspan="3" style="width: 120px; text-align: right">
                                <asp:Label ID="lbl_Just" runat="server" Text="Justificación:"></asp:Label>
                            </td>
                            <td rowspan="3" style="width: 747px">
                                <asp:TextBox ID="txtJust" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="61px" ReadOnly="True" TextMode="MultiLine" Width="735px"></asp:TextBox>
                            </td>
                            <td style="text-align: right; width: 140px; height: 21px;">
                                <asp:Label ID="lbl_PeriodoIni" runat="server" Text="Periodo de Comisión:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width: 100px">
                                            <asp:Label ID="lblPeriodoIni" runat="server" ForeColor="Blue"></asp:Label>
                                        </td>
                                        <td style="width: 20px; text-align: center;">
                                            <asp:Label ID="lbl_PeriodoA" runat="server" Text="a"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPeriodoFin" runat="server" ForeColor="Blue" Width="130px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <asp:Label ID="lbl_Recursos" runat="server" Text="Recursos Requeridos:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <asp:UpdatePanel ID="upRecursos" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBoxList ID="cblRecursos" runat="server" AutoPostBack="True" Enabled="False" RepeatColumns="4" Width="330px">
                                            <asp:ListItem>Anticipo</asp:ListItem>
                                            <asp:ListItem>Vehículo</asp:ListItem>
                                            <asp:ListItem>Combustible</asp:ListItem>
                                            <asp:ListItem>Avión</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 140px; height: 21px;">
                                <asp:Label ID="lbl_TipoTansp" runat="server" Text="Tipo Transporte:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <asp:Label ID="lblTipoTansp" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td style="width: 675px; vertical-align: top;">
                                <asp:UpdatePanel ID="upAnticipo" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlAnticipo" runat="server">
                                            <table style="width: 670px; ">
                                                <tr>
                                                    <td colspan="4" style="text-align: center; background-color: #666666;">
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td style="width: 165px">&nbsp;</td>
                                                                <td>
                                                                    <asp:Label ID="lbl_AnticipoT" runat="server" Font-Bold="True" Font-Overline="False" Font-Size="9pt" ForeColor="White" Text="Anticipo"></asp:Label>
                                                                </td>
                                                                <td style="width: 60px; text-align: right">
                                                                    <asp:Label ID="lbl_FolioA" runat="server" ForeColor="White" Text="Folio:"></asp:Label>
                                                                </td>
                                                                <td style="width: 100px; text-align: left;">
                                                                    <asp:Label ID="lblFolioA" runat="server" Font-Bold="True" Font-Overline="False" Font-Size="8pt" ForeColor="#FF9B9B"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 140px; height: 21px;">&nbsp;</td>
                                                    <td style="width: 90px; text-align: center">
                                                        <asp:Label ID="lbl_Días" runat="server" Text="Días"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center; width: 130px;">
                                                        <asp:Label ID="lbl_Monto" runat="server" Text="Monto"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center; ">
                                                        <asp:Label ID="lbl_TipoPago" runat="server" Text="Tipo Pago"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right">
                                                        <asp:Label ID="lbl_Hospedaje" runat="server" Text="No. noches hospedaje:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <ig:WebNumericEditor ID="wneDiasH" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Width="70px">
                                                        </ig:WebNumericEditor>
                                                    </td>
                                                    <td style="text-align: center; ">
                                                        <ig:WebCurrencyEditor ID="wceMontoH" runat="server" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Width="110px">
                                                        </ig:WebCurrencyEditor>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="lblTipoPago" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right">
                                                        <asp:Label ID="lbl_Alimentos" runat="server" Text="Alimentos:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <ig:WebNumericEditor ID="wneDiasA" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Width="70px">
                                                        </ig:WebNumericEditor>
                                                    </td>
                                                    <td style="text-align: center; ">
                                                        <ig:WebCurrencyEditor ID="wceMontoA" runat="server" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Width="110px">
                                                        </ig:WebCurrencyEditor>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right">
                                                        <asp:Label ID="lbl_Casetas" runat="server" Text="Casetas:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <ig:WebNumericEditor ID="wneDiasC" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Width="70px">
                                                        </ig:WebNumericEditor>
                                                    </td>
                                                    <td style="text-align: center; ">
                                                        <ig:WebCurrencyEditor ID="wceMontoC" runat="server" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Width="110px">
                                                        </ig:WebCurrencyEditor>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right">
                                                        <asp:Label ID="lbl_Otros" runat="server" Text="Otros *Especificar:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <ig:WebNumericEditor ID="wneDiasO" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Width="70px">
                                                        </ig:WebNumericEditor>
                                                    </td>
                                                    <td style="text-align: center; ">
                                                        <ig:WebCurrencyEditor ID="wceMontoO" runat="server" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Width="110px">
                                                        </ig:WebCurrencyEditor>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lblOtros" runat="server" Font-Bold="True" ForeColor="#003399" Width="240px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right">
                                                        <asp:Label ID="lbl_MontoT" runat="server" Text="Monto Solicitado:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">&nbsp;</td>
                                                    <td style="text-align: center; ">
                                                        <ig:WebCurrencyEditor ID="wceMontoT" runat="server" Font-Names="Verdana" Font-Size="8pt" Nullable="False" ReadOnly="True" Width="110px">
                                                        </ig:WebCurrencyEditor>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lblMontoTLetra" runat="server" Font-Italic="True"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdatePanel ID="upAvion" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlAvion" runat="server">
                                            <table style="width: 670px; ">
                                                <tr>
                                                    <td colspan="4" style="text-align: center; background-color: #666666;">
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td style="width: 165px">&nbsp;</td>
                                                                <td>
                                                                    <asp:Label ID="lbl_AvionT" runat="server" Font-Bold="True" Font-Overline="False" Font-Size="9pt" ForeColor="White" Text="Avión"></asp:Label>
                                                                </td>
                                                                <td style="width: 60px; text-align: right">
                                                                    <asp:Label ID="lbl_FolioAv" runat="server" ForeColor="White" Text="Folio:"></asp:Label>
                                                                </td>
                                                                <td style="width: 100px; text-align: left;">
                                                                    <asp:Label ID="lblFolioAv" runat="server" Font-Bold="True" Font-Overline="False" Font-Size="8pt" ForeColor="#FF9B9B"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style5" style="text-align: right">
                                                        <asp:Label ID="lbl_FechaNac" runat="server" Text="Fecha Nacimiento:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; width: 200px;">
                                                        <asp:Label ID="lblFechaNac" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                                    </td>
                                                    <td style="text-align: right; width: 120px;">&nbsp;</td>
                                                    <td style="text-align: left; ">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right">
                                                        <asp:Label ID="lbl_FechaSalida" runat="server" Text="Fecha Salida:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lblFechaSalida" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                                    </td>
                                                    <td style="text-align: right">
                                                        <asp:Label ID="lbl_FechaRegreso" runat="server" Text="Fecha Regreso:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lblFechaRegreso" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Panel ID="pnlJust" runat="server">
                                                <table style="width: 670px; ">
                                                    <tr>
                                                        <td class="auto-style5" style="text-align: right">
                                                            <asp:Label ID="lbl_JustAv" runat="server" Text="Justificación:"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; ">
                                                            <asp:Label ID="lblJustAv" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="vertical-align: top">
                                <asp:UpdatePanel ID="upVehiculo" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlVehiculo" runat="server">
                                            <table style="width: 670px; ">
                                                <tr>
                                                    <td colspan="4" style="text-align: center; background-color: #666666;">
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td style="width: 165px">&nbsp;</td>
                                                                <td>
                                                                    <asp:Label ID="lbl_VehiculoT" runat="server" Font-Bold="True" Font-Overline="False" Font-Size="9pt" ForeColor="White" Text="Reserva de Vehículo"></asp:Label>
                                                                </td>
                                                                <td style="width: 60px; text-align: right">
                                                                    <asp:Label ID="lbl_FolioV" runat="server" ForeColor="White" Text="Folio:"></asp:Label>
                                                                </td>
                                                                <td style="width: 100px; text-align: left;">
                                                                    <asp:Label ID="lblFolioV" runat="server" Font-Bold="True" Font-Overline="False" Font-Size="8pt" ForeColor="#FF9B9B"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 120px; height: 21px;">
                                                        <asp:Label ID="lbl_Vehiculo" runat="server" Text="Vehículo:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; width: 200px;">
                                                        <asp:Label ID="lblVehiculo" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                                    </td>
                                                    <td style="text-align: right; width: 120px;">
                                                        <asp:Label ID="lbl_Modelo" runat="server" Text="Modelo:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; ">
                                                        <asp:Label ID="lblModelo" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 120px; height: 21px;">
                                                        <asp:Label ID="lbl_FechaIni" runat="server" Text="Reservar desde:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lblFechaIni" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                                    </td>
                                                    <td style="text-align: right">
                                                        <asp:Label ID="lbl_FechaFin" runat="server" Text="Reservar hasta:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lblFechaFin" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 120px; height: 21px;">
                                                        <asp:Label ID="lbl_LugaresDisp" runat="server" Text="Lugares Disponibles:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lblLugaresDisp" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                                    </td>
                                                    <td style="text-align: right">
                                                        <asp:Label ID="lbl_LugaresRequ" runat="server" Text="Lugares Requeridos:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lblLugaresRequ" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:UpdatePanel ID="upKmActual" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Panel ID="pnlKmActual" runat="server">
                                                        <table style="width: 670px; ">
                                                            <tr>
                                                                <td class="auto-style46" style="text-align: right; width: 120px; height: 21px;">
                                                                    <asp:Label ID="lbl_KmsActual" runat="server" Text="Kilometraje Actual:"></asp:Label>
                                                                </td>
                                                                <td style="text-align: left; width: 200px;">
                                                                    <asp:Label ID="lblKmsActual" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                                                </td>
                                                                <td style="text-align: right; width: 120px;">&nbsp;</td>
                                                                <td style="text-align: left">&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdatePanel ID="upCombustible" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlCombustible" runat="server">
                                            <table style="width: 670px; ">
                                                <tr>
                                                    <td colspan="4" style="text-align: center; background-color: #666666;">
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td style="width: 165px">&nbsp;</td>
                                                                <td>
                                                                    <asp:Label ID="lbl_CombustibleT" runat="server" Font-Bold="True" Font-Overline="False" Font-Size="9pt" ForeColor="White" Text="Combustible"></asp:Label>
                                                                </td>
                                                                <td style="width: 60px; text-align: right">
                                                                    <asp:Label ID="lbl_FolioC" runat="server" ForeColor="White" Text="Folio:"></asp:Label>
                                                                </td>
                                                                <td style="width: 100px; text-align: left;">
                                                                    <asp:Label ID="lblFolioC" runat="server" Font-Bold="True" Font-Overline="False" Font-Size="8pt" ForeColor="#FF9B9B"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 120px; height: 21px;">
                                                        <asp:Label ID="lbl_VehiculoC" runat="server" Text="Vehículo:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; width: 200px;">
                                                        <asp:Label ID="lblVehiculoC" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                                    </td>
                                                    <td style="text-align: right; width: 120px;">
                                                        <asp:Label ID="lbl_TarjEdenred" runat="server" Text="Tarjeta Edenred:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; ">
                                                        <asp:Label ID="lblTarjEdenred" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 120px; height: 21px;">
                                                        <asp:Label ID="lbl_Litros" runat="server" Text="Litros:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lblLitros" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                                    </td>
                                                    <td style="text-align: right">
                                                        <asp:Label ID="lbl_Importe" runat="server" Text="Importe:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lblImporte" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
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
            </td>
        </tr>
        </table>
</asp:Content>