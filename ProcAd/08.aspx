<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="08.aspx.vb" Inherits="ProcAd._08" EnableEventValidation="False" %>

<%@ Register Assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function CheckOne(obj) {
            var grid = obj.parentNode.parentNode.parentNode;
            var inputs = grid.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    if (obj.checked && inputs[i] != obj && inputs[i].checked) {
                        inputs[i].checked = false;
                    }
                }
            }
        }
    </script>

    <style type="text/css">
        .auto-style5 {
            width: 150px;
            height: 21px;
        }

        .auto-style8 {
            height: 21px;
        }

        .auto-style10 {
            width: 120px;
            height: 21px;
        }

        .auto-style11 {
            width: 20px;
            height: 32px;
        }

        .auto-style12 {
            width: 100px;
            height: 32px;
        }

        .auto-style13 {
            width: 45px;
            height: 32px;
        }

        .auto-style14 {
            width: 60px;
            height: 32px;
        }

        .auto-style15 {
            width: 220px;
            height: 32px;
        }

        .auto-style16 {
            width: 40px;
            height: 32px;
        }

        .auto-style17 {
            width: 90px;
            height: 32px;
        }

        .auto-style18 {
            width: 140px;
            height: 32px;
        }

        .auto-style19 {
            width: 230px;
            height: 32px;
        }

        .auto-style20 {
            width: 75px;
            height: 32px;
        }

        .auto-style21 {
            width: 70px;
            height: 32px;
        }

        .auto-style22 {
            height: 32px;
        }

        .auto-style23 {
            margin-left: 40px;
        }

        .auto-style24 {
            width: 286px;
        }

        .auto-style29 {
            width: 156px;
        }

        .auto-style30 {
            width: 156px;
            height: 21px;
        }

        .auto-style31 {
            width: 161px;
            height: 21px;
        }

        .auto-style32 {
            width: 161px;
        }

        .auto-style33 {
            width: 331px;
        }

        .auto-style34 {
            width: 286px;
            height: 21px;
        }

        .auto-style35 {
            width: 331px;
            height: 21px;
        }

        .auto-style36 {
            width: 1366px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000; width: 1366px;">
                <ig:WebScriptManager ID="wsm08" runat="server">
                </ig:WebScriptManager>
                <asp:UpdatePanel ID="upDev" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="_txtDev" runat="server" Visible="False" Width="15px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtOmitValPGV" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtAntPend" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdEmpresaEmpl" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdCCEmpl" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtPuestoTab" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtRFCEmpr" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsInst" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtAutDir" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtCargComb" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtAlimTab" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtTaxiTab" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIsrRet" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtPeriodoLibre" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtEditComprobacion" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdAnticipoSeleccionado" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtVal_Origen_Destino" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtTot_ret_irs" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBotonCancelar" runat="server" Width="15px" Visible="false"></asp:TextBox>
                <asp:TextBox ID="_txtAnticipo" runat="server" Width="15px" Visible="false"></asp:TextBox>
               
                <asp:UpdatePanel ID="upAbreviatura" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="_txtTipoGasto" runat="server" Width="15px" Visible="False"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="upIdCC" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="_txtIdCC" runat="server" Width="45px" Visible="False"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <table style="width: 1366px;">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Literal ID="litError" runat="server"></asp:Literal>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
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
                                <asp:Label ID="lbl_Empresa" runat="server" Text="Empresa:"></asp:Label>
                            </td>
                            <td class="auto-style24">
                                <asp:UpdatePanel ID="upEmpresa" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlEmpresa" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="180px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                            <td class="auto-style31" style="text-align: right;">
                                <asp:Label ID="lbl_TipoGasto" runat="server" Text="Tipo de Gasto:"></asp:Label>
                            </td>
                            <td class="auto-style33">
                                <asp:UpdatePanel ID="upTipoGasto" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlTipoGasto" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="280px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                            <td class="auto-style30" style="text-align: right;">

                                <asp:UpdatePanel ID="up_lblCCDiv" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="lbl_CC" runat="server" Text="Centro de Costo:"></asp:Label>
                                        &nbsp;
                                        <asp:Label ID="lbl_Div" runat="server" Text="División:"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                            <td class="auto-style8">

                                <asp:UpdatePanel ID="upCCDiv" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlCC" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="280px" Visible="false">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlDiv" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="280px" Visible="false">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Empleado" runat="server" Text="Empleado:"></asp:Label>
                            </td>
                            <td class="auto-style24">



                                <asp:Label ID="lblEmpleado" runat="server" ForeColor="Blue" Width="280px"></asp:Label>



                            </td>
                            <td class="auto-style31" style="text-align: right;">
                                <asp:Label ID="lbl_Validador" runat="server" Text="Validador:"></asp:Label>
                            </td>
                            <td class="auto-style33">

                                <asp:UpdatePanel ID="upValidador" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlValidador" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="280px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                            <td class="auto-style30" style="text-align: right;">
                                <asp:Label ID="lbl_Autorizador" runat="server" Text="Autorizador:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <asp:UpdatePanel ID="upAutorizador" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlAutorizador" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="280px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Periodo" runat="server" Text="Periodo de Comprobación:"></asp:Label>
                            </td>
                            <td class="auto-style34">

                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <ig:WebDateTimeEditor ID="wdteFechaIni" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="75px">
                                        </ig:WebDateTimeEditor>
                                        &nbsp;
                                        <asp:Label ID="lblFechInc" runat="server" Text=""></asp:Label>
                                        &nbsp;
                                        <asp:Label ID="lbl_PeriodoA" runat="server" Text="a"></asp:Label>
                                        &nbsp;
                                        <ig:WebDateTimeEditor ID="wdteFechaFin" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="80px">
                                        </ig:WebDateTimeEditor>
                                        &nbsp;
                                        <asp:Label ID="lblFechFin" runat="server" Text=""></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                            <td  style="text-align: right; width:20px">
                                &nbsp;</td>
                            <td class="auto-style35">

                                &nbsp;</td>
                            <td class="auto-style30" style="text-align: right;">
                                <asp:Label ID="lbl_Director" runat="server" Text="Director:"></asp:Label>
                            </td>
                            <td class="auto-style8">

                                <asp:UpdatePanel ID="upDirector" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlDirector" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="280px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">&nbsp;</td>
                            <td class="auto-style24">&nbsp;</td>
                            <td style="text-align: right" class="auto-style32">&nbsp;</td>
                            <td class="auto-style33"></td>
                            <td style="text-align: right" class="auto-style29">&nbsp;</td>
                            <td></td>
                        </tr>
                    </table>
                    <table class="auto-style36">
                        <tr>
                            <td style="width: 130px; height: 30px; text-align: right">
                                <asp:Label ID="lbl_Just" runat="server" Text="Justificación:"></asp:Label>
                            </td>
                            <td style="width: 568px">
                                <asp:TextBox ID="txtJust" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="63px" TextMode="MultiLine" Width="535px"></asp:TextBox>
                            </td>
                            <td class="auto-style10" style="text-align: right;">
                                
                                <asp:Label ID="lbl_Evidencia" runat="server" Text="Evidencia:"></asp:Label>
                            </td>
                            <td class="auto-style8" style="width: 50px;">
                                    <asp:FileUpload ID="fuEvidencia" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px" />                    
                            </td>
                            <td>
                                <asp:Button ID="btnAEvidencia" runat="server" Text="Agregar" Font-Names="Verdana" Font-Size="8pt" />                        
                            </td>
                            <td>
                                <asp:Panel runat="server" ID="pnlGvEvidencias">
                                    <table style="width: 85%;">
                                        <tr>
                                            <td style="width: 90%; text-align: left">&nbsp;</td>
                                            <td>
                                                <asp:GridView ID="gvEvidencias" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="200px">
                                                    <Columns>
                                                        <asp:HyperLinkField DataNavigateUrlFields="ruta" DataTextField="nombre" HeaderText="Nombre" />
                                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" Visible="false">
                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="nombre_archivo" HeaderText="nombre_archivo">
                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ruta_archivo" HeaderText="ruta_archivo">
                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                        </asp:BoundField>
                                                        <asp:CommandField ButtonType="Image" SelectImageUrl="images\Trash15.png" ShowSelectButton="True">
                                                            <ItemStyle Width="15px" />
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
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlAnticiposDecision" runat="server">
                        <table style="width: 1000px">
                            <tr>
                                <td style="width: 250px; text-align:right">
                                   <asp:Label ID="lbl_Mensaje" runat="server" Text="Elije un Anticipo para comprobar"></asp:Label>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbdOpcionAnticipo" runat="server" Width="250px" RepeatColumns="3" AutoPostBack="true">
                                    
                                        <asp:ListItem>Anticipo</asp:ListItem>
                                      
                                        <asp:ListItem>Anticipo AMEX</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                   
                    <table style="width: 1366px;">
                        <tr>
                            <td style="width: 130px; height: 30px; text-align: right">
                                <asp:Label ID="lbl_Anticipos" runat="server" Text="Anticipos:"></asp:Label>
                            </td>
                            <td style="width: 430px">
                                <asp:UpdatePanel ID="upAnticipos" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvAnticipos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="300px">
                                            <Columns>
                                                <asp:BoundField DataField="id_ms_anticipo" HeaderText="id_ms_anticipo" />
                                                <asp:BoundField DataField="fecha" DataFormatString="{0:d}" HeaderText="Fecha">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="importe" DataFormatString="{0:c}" HeaderText="Importe">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="importe" HeaderText="impAnt" />
                                                <asp:BoundField DataField="importeAPGV" HeaderText="impAntPGV" />
                                                <asp:TemplateField HeaderText="Comprobar">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkCtrl" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                </asp:TemplateField>
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

                                        <asp:GridView ID="gvAnticiposAmex" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="300px" >
                                            <Columns>
                                                <asp:BoundField DataField="id_ms_anticipo" HeaderText="id_ms_anticipo" />
                                                <asp:BoundField DataField="fecha" DataFormatString="{0:d}" HeaderText="Fecha">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="importe" DataFormatString="{0:c}" HeaderText="Importe">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="importe" HeaderText="impAnt" />
                                                <asp:BoundField DataField="importeAPGV" HeaderText="impAntPGV" />
                                                <asp:BoundField DataField="tipo" HeaderText="AMEX"/>
                                                <asp:TemplateField HeaderText="Comprobar">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkCtrl" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                </asp:TemplateField>
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="vertical-align: top">
                                <asp:Panel ID="pnlUnidad" runat="server">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 130px; text-align: right">
                                                <asp:Label ID="lbl_Unidad" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Size="8pt" Height="16px" Text="Unidad:" Width="50px"></asp:Label>
                                            </td>
                                            <td style="width: 95px">
                                                <asp:TextBox ID="txtUnidad" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="16px" Width="80px"></asp:TextBox>
                                            </td>
                                            <td style="width: 130px">
                                                <asp:Button ID="cmdBuscarU" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Buscar" Width="80px" />
                                            </td>
                                            <td style="width: 260px">
                                                <asp:DropDownList ID="ddlUnidad" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Button ID="cmdAgregarU" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Agregar" Width="80px" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 30px; text-align: right">&nbsp;</td>
                                            <td>
                                                <asp:GridView ID="gvUnidad" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px">
                                                    <Columns>
                                                        <asp:BoundField DataField="id_dt_unidad" HeaderText="id_dt_unidad" />
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
                                                        <asp:CommandField ButtonType="Image" SelectImageUrl="images\Trash15.png" ShowSelectButton="True">
                                                            <ItemStyle Width="15px" />
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
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                

                    <asp:Panel ID="pnlConcepto1" runat="server">
                        <table style="width: 1880px;">
                            <tr>
                                <td style="border: 1px solid #000000; text-align: center" class="auto-style11"></td>
                                <td style="border: 1px solid #000000; text-align: center" class="auto-style12">
                                    <asp:Label ID="lbl_Fecha" runat="server" Text="Fecha de Realización"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center" class="auto-style13">
                                    <asp:Label ID="lbl_Factura" runat="server" Text="Factura"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center" class="auto-style14">
                                    <asp:Label ID="lbl_Tabulador" runat="server" Text="Tabulador"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center" class="auto-style15">
                                    <asp:Label ID="lbl_Concepto" runat="server" Text="Concepto"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center" class="auto-style16">
                                    <asp:Label ID="lbl_NoPersonas" runat="server" Text="No. Pers." Width="30px"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center" class="auto-style16">
                                    <asp:Label ID="lbl_NoDias" runat="server" Text="No. Días" Width="30px"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center" class="auto-style17">
                                    <asp:Label ID="lbl_Subtotal" runat="server" Text="Subtotal"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center" class="auto-style17">
                                    <asp:Label ID="lbl_IVA" runat="server" Text="IVA"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center" class="auto-style17">
                                    <asp:Label ID="lbl_Total" runat="server" Text="Total"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center" class="auto-style14">
                                    <asp:Label ID="lbl_PorcentAut" runat="server" Text="% Aut"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center" class="auto-style18">
                                    <asp:Label ID="lbl_RFC" runat="server" Text="RFC"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center" class="auto-style19">
                                    <asp:Label ID="lbl_Proveedor" runat="server" Text="Proveedor"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center" class="auto-style18">
                                    <asp:Label ID="lbl_NoFactura" runat="server" Text="No. Factura"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center" class="auto-style20">
                                    <asp:Label ID="lbl_Origen" runat="server" Text="Orig"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center" class="auto-style20">
                                    <asp:Label ID="lbl_Destino" runat="server" Text="Dest"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center" class="auto-style21">
                                    <asp:Label ID="lbl_Vehi" runat="server" Text="Vehiculo"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center" class="auto-style22">
                                    <asp:Label ID="lbl_Obs" runat="server" Text="Obs."></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid #000000; text-align: center">&nbsp;</td>
                                <td style="border: 1px solid #000000; text-align: center">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <ig:WebDatePicker ID="wdpFecha1" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="95px">
                                                </ig:WebDatePicker>
                                            </td>
                                            <td style="width: 10px">
                                                <asp:UpdatePanel ID="upDateV1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblDateV1" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center">
                                    <asp:UpdatePanel ID="upFactura1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbFactura1" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center">
                                    <asp:UpdatePanel ID="upTabulador1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbTabulador1" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center">
                                    <asp:UpdatePanel ID="upConcepto1" runat="server" UpdateMode="Conditional">
                                        <%-- <Triggers>
                                            <asp:AsyncPostBackTrigger controlid="rblTipo1" eventname="SelectedIndexChanged" />
                                        </Triggers>--%>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlConcepto1" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="210px" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center">
                                    <asp:UpdatePanel ID="upNoPers1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoPers1" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center">
                                    <ig:WebNumericEditor ID="wneNoDias1" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="30px" DataMode="Int" MinValue="1" Nullable="False">
                                    </ig:WebNumericEditor>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center">
                                    <asp:UpdatePanel ID="upSubtotal1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceSubtotal1" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center">
                                    <asp:UpdatePanel ID="upIVA1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceIVA1" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center">
                                    <asp:UpdatePanel ID="upTotal1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceTotal1" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center">
                                    <asp:UpdatePanel ID="upPorcentAut1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebPercentEditor ID="wpePorcentAut1" runat="server" ReadOnly="True" Width="55px" Font-Names="Verdana" Font-Size="7pt">
                                            </ig:WebPercentEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center">
                                    <asp:UpdatePanel ID="upRFC1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtRFC1" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="ibtnRFCBus1" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" Style="height: 16px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; text-align: left">
                                    <asp:UpdatePanel ID="upProveedor1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:HyperLink ID="hlProveedor1" runat="server" Width="220px">[hlProveedor1]</asp:HyperLink>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; text-align: left">
                                    <asp:UpdatePanel ID="upNoFactura1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlNoFactura1" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 10px">
                                                        <asp:UpdatePanel ID="upObsE16" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblNoFacturaE1" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center">
                                    <asp:DropDownList ID="ddlOrig1" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtOriDes1" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center">
                                    <asp:DropDownList ID="ddlDest1" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center">
                                    <asp:TextBox ID="txtVehi1" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="60px"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; text-align: left">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtObs1" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="160px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px">
                                                <asp:UpdatePanel ID="upObsE1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblObsE1" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>


                    <asp:Panel ID="pnlAceptar" runat="server">
                        <table style="width: 1366px; height: 40px;">
                            <tr>
                                <td style="text-align: right; width: 50%" class="auto-style23">

                                    <asp:Button ID="btnAceptar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Agregar Concepto" Width="200px" />
                                </td>
                                <td style="text-align: center; width: 50%">
                                    <asp:Button ID="btnCancelar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Limpiar Concepto" Width="200px" />

                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                      <asp:Panel ID="pnlConceptos" runat="server">
                      <table style="width:100%">

                          <tr style="height:25px">
                              <td style="width:250px; text-align:right">
                                  <asp:Label ID="lbl_NoConceptos" runat="server" Text="Num. de conceptos:"></asp:Label>
                              </td>
                              <td style="width:150px; text-align:center">
                                  <asp:Label ID="lblNoConceptos" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="0" Width="50px"></asp:Label>
                              </td>
                              <td style="width:150px; text-align:center">
                                  &nbsp;</td>
                            <td>

                            </td>
                          </tr>
                      </table>
                  </asp:Panel>


                       <asp:Panel ID="pnlGrid" runat="server">
                        <table style="width: 100%;">

                            <tr>
                                <td style="text-align: center; width: 40px;">&nbsp;</td>
                                <td class="auto-style23">
                                    <asp:GridView ID="gvProducto" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="1850px">
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                <ItemStyle Width="15px" />
                                            </asp:CommandField>
                                            <asp:BoundField HeaderText="id_concepto" DataField="id_concepto">
                                                <ItemStyle Width="5px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Fecha de Realización" DataField="Fecha de Realizacion">
                                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Tipo" DataField="Tipo">
                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Concepto" DataField="concepto">
                                                <ItemStyle Width="230px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="No. Pers" DataField="No.Pers">
                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="No. Días" DataField="No.Dias">
                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Subtotal" DataField="Subtotal">
                                                <ItemStyle HorizontalAlign="Right" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="IVA" DataField="IVA">
                                                <ItemStyle Width="70px" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Total" DataField="Total">
                                                <ItemStyle HorizontalAlign="Right" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="% Aut" DataField="%Aut">
                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="RFC" DataField="RFC">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="C.P" DataField="lugar_exp">
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Proveedor" DataField="Proveedor">
                                                <ItemStyle HorizontalAlign="left" Width="200px" />
                                            </asp:BoundField>
                                            <asp:HyperLinkField DataNavigateUrlFields="ruta" DataTextField="No.Factura" HeaderText="No.Factura">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                            </asp:HyperLinkField>
                                            <asp:BoundField HeaderText="Origen" DataField="Origen">
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Destino" DataField="Destino">
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Vehiculo" DataField="Vehiculo">
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Observaciones" DataField="Observaciones">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="txtOriDes" DataField="txtOriDes">
                                                <ItemStyle HorizontalAlign="Center" Width="5px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="id_lugar_orig" DataField="id_lugar_orig">
                                                <ItemStyle HorizontalAlign="Center" Width="5px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="id_lugar_dest" DataField="id_lugar_dest">
                                                <ItemStyle HorizontalAlign="Center" Width="5px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="isr_ret" DataField="isr_ret">
                                                <ItemStyle HorizontalAlign="Center" Width="5px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="ruta" DataField="ruta" Visible="true">
                                                <ItemStyle HorizontalAlign="Center" Width="5px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="No. Factura" DataField="No.Factura" Visible="true">
                                                <ItemStyle HorizontalAlign="Center" Width="5px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="nombre_fact" DataField="nombre_fact">
                                                <ItemStyle HorizontalAlign="Center" Width="5px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="ruta_fact" DataField="ruta_fact">
                                                <ItemStyle HorizontalAlign="Center" Width="5px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="% Aut" DataField="%Auto">
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:BoundField>
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

                    </asp:Panel>

                        <asp:Panel runat="server" ID="pnlEliminar">
                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align: center">
                                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar Concepto" Font-Names="Verdana" Font-Size="8pt" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </asp:Panel>

                    <asp:Panel ID="pnlSuma" runat="server">
                        <table style="width: 1366px; height: 50px;">
                            <tr>
                                <td style="text-align: center">
                                    <table style="width: 678px;">
                                        <tr>
                                            <td style="width: 120px;">
                                                <ig:WebCurrencyEditor ID="wceTotalPGV" runat="server" Width="25px" Visible="False">
                                                </ig:WebCurrencyEditor>
                                                <ig:WebCurrencyEditor ID="wceTotalA" runat="server" Visible="False" Width="25px">
                                                </ig:WebCurrencyEditor>
                                                <ig:WebCurrencyEditor ID="wceTotalAPGV" runat="server" Visible="False" Width="25px">
                                                </ig:WebCurrencyEditor>
                                                <ig:WebCurrencyEditor ID="wceTotalC" runat="server" Visible="False" Width="25px">
                                                </ig:WebCurrencyEditor>
                                                <ig:WebCurrencyEditor ID="wceTotalS" runat="server" Visible="False" Width="25px">
                                                </ig:WebCurrencyEditor>
                                            </td>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 110px; text-align: center;">
                                                            <asp:Label ID="lbl_TotalA" runat="server" Font-Bold="True" Text="Anticipos"></asp:Label>
                                                        </td>
                                                        <td style="width: 110px; text-align: center;">
                                                            <asp:Label ID="lbl_TotalC" runat="server" Font-Bold="True" Text="Comprobado"></asp:Label>
                                                        </td>
                                                        <td style="width: 110px; text-align: center;">
                                                            <asp:Label ID="lbl_TotalS" runat="server" Font-Bold="True" Text="Saldo"></asp:Label>
                                                        </td>
                                                        <td rowspan="2">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="lblTotalA" runat="server" Font-Bold="False" Font-Underline="True" Text="0"></asp:Label>
                                                        </td>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="lblTotalC" runat="server" Font-Bold="False" Font-Underline="True" Text="0"></asp:Label>
                                                        </td>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="lblTotalS" runat="server" Font-Bold="False" Font-Underline="True" Text="0"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="text-align: center; width: 783px;">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="height: 20px">
                                                <asp:UpdatePanel ID="upValeIng" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td style="width: 100px; text-align: right;">
                                                                    <asp:Label ID="lbl_ValeIng" runat="server" Text="Vale Ingreso:"></asp:Label>
                                                                </td>
                                                                <td style="text-align: left; width: 115px;">
                                                                    <asp:TextBox ID="txtValeIng" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td style="text-align: right; width: 110px;">
                                                                    <asp:Label ID="lbl_ValeIngC" runat="server" Text="Comprobante:"></asp:Label>
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:FileUpload ID="fuValeIng" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="320px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                

                    <asp:Panel ID="pnlFinalizar" runat="server">
                        <table style="width: 1366px; height: 40px;">
                            <tr>
                                <td style="text-align: center">
                                    <asp:Button ID="btnGuardar" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnGuardar_Click" OnClientClick="this.disabled = true;" Text="Enviar a Aprobación" UseSubmitBehavior="false" Width="200px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>


                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
