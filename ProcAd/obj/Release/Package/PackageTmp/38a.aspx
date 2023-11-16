<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="38a.aspx.vb" Inherits="ProcAd._38a" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style5 {
            width: 120px;
            height: 21px;
        }
        .auto-style8 {
            height: 21px;
        }
        .auto-style10 {
            width: 140px;
            height: 21px;
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


.igte_Edit
{
	background-color:White;
	font-size:13px;
	font-family: Segoe UI, Verdana, Arial, Sans-Serif;
	border:solid 1px #CCCCCC;
	outline: 0;
	color:#333333;
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


.igte_Edit
{
	background-color:White;
	font-size:13px;
	font-family: Segoe UI, Verdana, Arial, Sans-Serif;
	border:solid 1px #CCCCCC;
	outline: 0;
	color:#333333;
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


.igte_Edit
{
	background-color:White;
	font-size:13px;
	font-family: Segoe UI, Verdana, Arial, Sans-Serif;
	border:solid 1px #CCCCCC;
	outline: 0;
	color:#333333;
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


.igte_Edit
{
	background-color:White;
	font-size:13px;
	font-family: Segoe UI, Verdana, Arial, Sans-Serif;
	border:solid 1px #CCCCCC;
	outline: 0;
	color:#333333;
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


.igte_Edit
{
	background-color:White;
	font-size:13px;
	font-family: Segoe UI, Verdana, Arial, Sans-Serif;
	border:solid 1px #CCCCCC;
	outline: 0;
	color:#333333;
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


.igte_Edit
{
	background-color:White;
	font-size:13px;
	font-family: Segoe UI, Verdana, Arial, Sans-Serif;
	border:solid 1px #CCCCCC;
	outline: 0;
	color:#333333;
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


        .auto-style9 {
            width: 300px;
        }
        

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000;">
                <ig:WebScriptManager ID="wsm01" runat="server">
                </ig:WebScriptManager>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsInst" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsInstA" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsInstC" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtPuestoTab" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtNoEmpleado" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtCorreo" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdCC" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtAñoPGV" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtMesPGV" runat="server" Width="15px" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlInicio" runat="server">
                    <table style="width: 1360px; height: 25px;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td style="width: 250px; text-align: right">
                                <asp:Label ID="lbl_Folio" runat="server" Text="Folio de Solicitud de Recursos:"></asp:Label>
                            </td>
                            <td style="width: 130px">
                                <asp:Label ID="lblFolio" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                            </td>
                        </tr>
                    </table>
                     <table style="width: 1360px; height: 22px;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Solicitante" runat="server" Text="Solicitante:"></asp:Label>
                            </td>
                            <td class="auto-style9">
                                
                                <asp:Label ID="lblSolicitante" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                                
                            </td>
                            <td style="text-align: right; width: 100px; height: 21px;">
                                <asp:Label ID="lbl_Autorizador" runat="server" Text="Autorizador:"></asp:Label>
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="lblAutorizador" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td style="width: 150px; text-align: right;">
                                <asp:Label ID="lbl_Director" runat="server" Text="Director:"></asp:Label>
                            </td>
                             <td>
                                <asp:Label ID="lblDirector" runat="server" ForeColor="Blue" Width="290px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1360px; height: 22px;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_NoProveedor" runat="server" Text="Num. Proveedor:"></asp:Label>
                            </td>
                            <td class="auto-style9">
                                <asp:Label ID="lblNoProveedor" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 100px; height: 21px;">
                                <asp:Label ID="lbl_Empresa" runat="server" Text="Empresa:"></asp:Label>
                            </td>
                             <td style="width: 150px">
                                <asp:Label ID="lblEmpresa"  runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td style="width: 150px; text-align: right;">
                                <asp:Label ID="lbl_CC" runat="server" Text="Centro de Costo:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCC" runat="server" ForeColor="Blue" Width="290px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1360px; height: 22px;">
                        <tr>
                            <td style="text-align: right; width: 120px; height: 21px;">
                                <asp:Label ID="lbl_Orig" runat="server" Text="Origen:"></asp:Label>
                            </td>
                            <td style="width: 220px;">
                                <asp:Label ID="lblOrig" runat="server" ForeColor="Blue" Width="200px"></asp:Label>
                            </td>
                            <td style="width: 60px; height: 21px; text-align: right">
                                <asp:Label ID="lbl_Dest" runat="server" Text="Destino:"></asp:Label>
                            </td>
                            <td style="width: 240px;">
                                <asp:Label ID="lblDest" runat="server" ForeColor="Blue" Width="200px"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 180px; height: 21px;">
                                <asp:CheckBoxList ID="cblMovLocales" runat="server" Enabled="False" RepeatColumns="1">
                                    <asp:ListItem>Movimientos Locales</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <td style="text-align: right; width: 130px; height: 21px;">
                                <asp:Label ID="lbl_Destino0" runat="server" Text="Lugar:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDestino" runat="server" ForeColor="Blue" Width="380px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1360px;">
                        <tr>
                            <td style="width: 120px; text-align: right" rowspan="3">
                                <asp:Label ID="lbl_Just" runat="server" Text="Justificación:"></asp:Label>
                            </td>
                            <td style="width: 747px" rowspan="3">
                                <asp:TextBox ID="txtJust" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="61px" TextMode="MultiLine" Width="735px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="auto-style10" style="text-align: right;">
                                <asp:Label ID="lbl_PeriodoIni" runat="server" Text="Periodo de Comisión:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <table style="width:200px;">
                                    <tr>
                                        <td style="width: 70px">
                                            <asp:Label ID="lblPeriodoIni" runat="server" ForeColor="Blue">00/00/0000</asp:Label>
                                        </td>
                                        <td style="width: 12px; text-align: left;">
                                            <asp:Label ID="lbl_PeriodoA" runat="server" Text="a"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPeriodoFin" runat="server" ForeColor="Blue">00/00/0000</asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style10" style="text-align: right;">
                                <asp:Label ID="lbl_Recursos" runat="server" Text="Recursos Requeridos:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <asp:UpdatePanel ID="upRecursos" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBoxList ID="cblRecursos" runat="server" AutoPostBack="True" RepeatColumns="4" Width="330px" Enabled="False">
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
                            <td class="auto-style10" style="text-align: right;">
                                <asp:Label ID="lbl_TipoTansp" runat="server" Text="Tipo Transporte:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <asp:Label ID="lblTipoTansp" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
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
                                                <td style="width: 100px; text-align: center">
                                                    <asp:Label ID="lbl_Días" runat="server" Text="Días"></asp:Label>
                                                </td>
                                                <td style="text-align: center; width: 140px;">
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
                                                <td>
                                                    &nbsp;</td>
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
                                                <td class="auto-style5" style="text-align: right">
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
                                                <td style="text-align: right" class="auto-style5">
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
                                                <td style="text-align: right" class="auto-style5">
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
                                                            <td class="auto-style5" style="text-align: right">
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
                                                <td class="auto-style5" style="text-align: right">
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
                                                <td style="text-align: right">
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
            </td>
        </tr>
        <tr>
            <td>
                    <table style="width: 1360px; height: 50px;">
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnAutoriza" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Autorizar" Width="200px" OnClientClick="this.disabled = true; this.value='Procesando…';" UseSubmitBehavior="false" OnClick ="btnAutoriza_Click" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button ID="btnRechaza" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Rechazar" Width="200px" OnClientClick="this.disabled = true; this.value='Procesando…';" UseSubmitBehavior="false" OnClick ="btnRechaza_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
        </tr>
    </table>
</asp:Content>