<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="61.aspx.vb" Inherits="ProcAd._61" %>
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
            width: 120px;
            height: 21px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000; width: 1366px;">
                <ig:WebScriptManager ID="wsm61" runat="server">
                </ig:WebScriptManager>
                <asp:UpdatePanel ID="upDev" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="_txtDev" runat="server" Visible="False" Width="15px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="upAntPend" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="_txtAntPend" runat="server" Width="55px" Visible="False"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="upIdEmpresaEmpl" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="_txtIdEmpresaEmpl" runat="server" Width="55px" Visible="False"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="upIdCCEmpl" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="_txtIdCCEmpl" runat="server" Width="55px" Visible="False"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="upPuestoTab" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="_txtPuestoTab" runat="server" Width="55px" Visible="False"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdAutorizador" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtRFCEmpr" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsInst" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtCargComb" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtVal_Origen_Destino1" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtVal_Origen_Destino2" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtVal_Origen_Destino3" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtVal_Origen_Destino4" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtVal_Origen_Destino5" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtVal_Origen_Destino6" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtVal_Origen_Destino7" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtVal_Origen_Destino8" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtVal_Origen_Destino9" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtVal_Origen_Destino10" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtVal_Origen_Destino11" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtVal_Origen_Destino12" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtVal_Origen_Destino13" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtVal_Origen_Destino14" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtVal_Origen_Destino15" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtVal_Origen_Destino16" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtVal_Origen_Destino17" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtVal_Origen_Destino18" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtVal_Origen_Destino19" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtVal_Origen_Destino20" runat="server" Width="15px" Visible="False"></asp:TextBox>
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
                <asp:Literal ID="litError" runat="server"></asp:Literal>
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
                            <td class="auto-style9">
                                <asp:UpdatePanel ID="upEmpresa" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlEmpresa" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="180px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="auto-style10" style="text-align: right; width: 160px;">
                                <asp:Label ID="lbl_Periodo" runat="server" Text="Periodo de Comprobación:"></asp:Label>
                            </td>
                            <td class="auto-style9">
                                <ig:WebDateTimeEditor ID="wdteFechaIni" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="90px">
                                </ig:WebDateTimeEditor>
                                &nbsp;
                                <asp:Label ID="lbl_PeriodoA" runat="server" Text="a"></asp:Label>
                                &nbsp;<ig:WebDateTimeEditor ID="wdteFechaFin" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="90px">
                                </ig:WebDateTimeEditor>
                            </td>
                            <td class="auto-style10" style="text-align: right; ">
                                <asp:Label ID="lbl_Empleado" runat="server" Text="Empleado:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <asp:UpdatePanel ID="upEmpleadoBus" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtEmpleadoBus" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="150px"></asp:TextBox>
                                        &nbsp;<asp:ImageButton ID="ibtnEmpleadoBus" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_TipoGasto" runat="server" Text="Tipo de Gasto:"></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="upTipoGasto" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlTipoGasto" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="280px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Autorizador" runat="server" Text="Autorizador:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblAutorizador" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                &nbsp;</td>
                            <td>
                                <asp:UpdatePanel ID="upEmpleado" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlEmpleado" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="280px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_TipoAct" runat="server" Text="Tipo de Actividad:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTipoAct" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="280px">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_CC" runat="server" Text="Centro de Costo:"></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="upCC" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlCC" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="280px" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblCC" runat="server" Font-Italic="True" Font-Names="Tahoma" Font-Size="8pt" style="Z-INDEX: 0"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Div" runat="server" Text="División:"></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="upDiv" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlDiv" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="280px">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblDiv" runat="server" Font-Italic="True" Font-Names="Tahoma" Font-Size="8pt" style="Z-INDEX: 0"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1366px;">
                        <tr>
                            <td style="width: 150px; height: 30px; text-align: right">
                                <asp:Label ID="lbl_Just" runat="server" Text="Justificación:"></asp:Label>
                            </td>
                            <td style="width: 768px">
                                <asp:TextBox ID="txtJust" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="43px" TextMode="MultiLine" Width="735px"></asp:TextBox>
                            </td>
                            <td class="auto-style10" style="text-align: right;">
                                <asp:Label ID="lbl_NoConceptos" runat="server" Text="Num. de conceptos:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="ddlNoConceptos" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="50px" AutoPostBack="True">
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem Value="6"></asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>9</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                    <asp:ListItem>13</asp:ListItem>
                                    <asp:ListItem>14</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>16</asp:ListItem>
                                    <asp:ListItem>17</asp:ListItem>
                                    <asp:ListItem>18</asp:ListItem>
                                    <asp:ListItem>19</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1366px;">
                        <tr>
                            <td style="width: 150px; height: 30px; text-align: right">
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
                                <asp:UpdatePanel ID="upUnidad" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlUnidad" runat="server">
                                            <table style="width:100%;">
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
                                            <table style="width:100%;">
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlConcepto1" runat="server">
                        <table style="width:1880px;">
                            <tr>
                                <td style="border: 1px solid #000000; width: 20px; text-align: center">&nbsp;</td>
                                <td style="border: 1px solid #000000; width: 100px; text-align: center">
                                    <asp:Label ID="lbl_Fecha" runat="server" Text="Fecha de Realización"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 45px; text-align: center">
                                    <asp:Label ID="lbl_Factura" runat="server" Text="Factura"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:Label ID="lbl_Tabulador" runat="server" Text="Tabulador"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 220px; text-align: center">
                                    <asp:Label ID="lbl_Concepto" runat="server" Text="Concepto"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:Label ID="lbl_NoPersonas" runat="server" Text="No. Pers." Width="30px"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:Label ID="lbl_NoDias" runat="server" Text="No. Días" Width="30px"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:Label ID="lbl_Subtotal" runat="server" Text="Subtotal"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:Label ID="lbl_IVA" runat="server" Text="IVA"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:Label ID="lbl_Total" runat="server" Text="Total"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:Label ID="lbl_PorcentAut" runat="server" Text="% Aut"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:Label ID="lbl_RFC" runat="server" Text="RFC"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 230px; text-align: center">
                                    <asp:Label ID="lbl_Proveedor" runat="server" Text="Proveedor"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:Label ID="lbl_NoFactura" runat="server" Text="No. Factura"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:Label ID="lbl_Origen" runat="server" Text="Orig"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:Label ID="lbl_Destino" runat="server" Text="Dest"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 70px; text-align: center">
                                    <asp:Label ID="lbl_Vehi" runat="server" Text="Vehiculo"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center">
                                    <asp:Label ID="lbl_Obs" runat="server" Text="Obs."></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid #000000; text-align: center">
                                    <asp:Label ID="lbl_No1" runat="server" Text="1"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; text-align: center">
                                    <ig:WebDatePicker ID="wdpFecha1" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="95px">
                                    </ig:WebDatePicker>
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
                                    <asp:UpdatePanel ID="upConcepto1" runat="server" updatemode="Conditional">
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
                                    <asp:UpdatePanel ID="upNoDias1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoDias1" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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
                                            &nbsp;<asp:ImageButton ID="ibtnRFCBus1" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
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
                                    <asp:UpdatePanel ID="upNoFactura1" runat="server" updatemode="Conditional">
                                        <ContentTemplate>
                                            <table style="width:100%;">
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
                                    <table style="width:100%;">
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
                    <asp:Panel ID="pnlConcepto2" runat="server">
                        <table style="width:1880px;">
                            <tr>
                                <td style="border: 1px solid #000000; width: 20px; text-align: center">
                                    <asp:Label ID="lbl_No2" runat="server" Text="2"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 100px; text-align: center">
                                    <ig:WebDatePicker ID="wdpFecha2" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="95px">
                                    </ig:WebDatePicker>
                                </td>
                                <td style="border: 1px solid #000000; width: 45px; text-align: center">
                                    <asp:UpdatePanel ID="upFactura2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbFactura2" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upTabulador2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbTabulador2" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 220px; text-align: center">
                                    <asp:UpdatePanel ID="upConcepto2" runat="server" updatemode="Conditional">
                                        <%--<Triggers>
                                            <asp:AsyncPostBackTrigger controlid="rblTipo2" eventname="SelectedIndexChanged" />
                                        </Triggers>--%>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlConcepto2" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="210px" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoPers2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoPers2" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoDias2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoDias2" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upSubtotal2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceSubtotal2" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upIVA2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceIVA2" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upTotal2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceTotal2" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upPorcentAut2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebPercentEditor ID="wpePorcentAut2" runat="server" Font-Names="Verdana" Font-Size="7pt" ReadOnly="True" Width="55px">
                                            </ig:WebPercentEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upRFC2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtRFC2" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="ibtnRFCBus2" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 230px; text-align: left">
                                    <asp:UpdatePanel ID="upProveedor2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:HyperLink ID="hlProveedor2" runat="server" Width="220px">[hlProveedor2]</asp:HyperLink>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upNoFactura2" runat="server" updatemode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger controlid="ibtnRFCBus2" eventname="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlNoFactura2" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 10px">
                                                        <asp:UpdatePanel ID="upObsE17" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblNoFacturaE2" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlOrig2" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtOriDes2" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="10px" Visible="False"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlDest2" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                </td>
                                <td style="border: 1px solid #000000; width: 70px; text-align: center">
                                    <asp:TextBox ID="txtVehi2" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="60px"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; text-align: left">
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtObs2" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="160px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px">
                                                <asp:UpdatePanel ID="upObsE2" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblObsE2" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlConcepto3" runat="server">
                        <table style="width:1880px;">
                            <tr>
                                <td style="border: 1px solid #000000; width: 20px; text-align: center">
                                    <asp:Label ID="lbl_No3" runat="server" Text="3"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 100px; text-align: center">
                                    <ig:WebDatePicker ID="wdpFecha3" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="95px">
                                    </ig:WebDatePicker>
                                </td>
                                <td style="border: 1px solid #000000; width: 45px; text-align: center">
                                    <asp:UpdatePanel ID="upFactura3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbFactura3" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upTabulador3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbTabulador3" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 220px; text-align: center">
                                    <asp:UpdatePanel ID="upConcepto3" runat="server" updatemode="Conditional">
                                        <%--<Triggers>
                                            <asp:AsyncPostBackTrigger controlid="rblTipo2" eventname="SelectedIndexChanged" />
                                        </Triggers>--%>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlConcepto3" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="210px" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoPers3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoPers3" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoDias3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoDias3" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upSubtotal3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceSubtotal3" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upIVA3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceIVA3" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upTotal3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceTotal3" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upPorcentAut3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebPercentEditor ID="wpePorcentAut3" runat="server" Font-Names="Verdana" Font-Size="7pt" ReadOnly="True" Width="55px">
                                            </ig:WebPercentEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upRFC3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtRFC3" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="ibtnRFCBus3" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 230px; text-align: left">
                                    <asp:UpdatePanel ID="upProveedor3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:HyperLink ID="hlProveedor3" runat="server" Width="220px">[hlProveedor3]</asp:HyperLink>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upNoFactura3" runat="server" updatemode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger controlid="ibtnRFCBus2" eventname="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlNoFactura3" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 10px">
                                                        <asp:UpdatePanel ID="upObsE18" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblNoFacturaE3" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlOrig3" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtOriDes3" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="10px" Visible="False"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlDest3" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                </td>
                                <td style="border: 1px solid #000000; width: 70px; text-align: center">
                                    <asp:TextBox ID="txtVehi3" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="60px"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; text-align: left">
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtObs3" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="160px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px">
                                                <asp:UpdatePanel ID="upObsE3" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblObsE3" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlConcepto4" runat="server">
                        <table style="width:1880px;">
                            <tr>
                                <td style="border: 1px solid #000000; width: 20px; text-align: center">
                                    <asp:Label ID="lbl_No4" runat="server" Text="4"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 100px; text-align: center">
                                    <ig:WebDatePicker ID="wdpFecha4" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="95px">
                                    </ig:WebDatePicker>
                                </td>
                                <td style="border: 1px solid #000000; width: 45px; text-align: center">
                                    <asp:UpdatePanel ID="upFactura4" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbFactura4" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upTabulador4" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbTabulador4" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 220px; text-align: center">
                                    <asp:UpdatePanel ID="upConcepto4" runat="server" updatemode="Conditional">
                                        <%--<Triggers>
                                            <asp:AsyncPostBackTrigger controlid="rblTipo2" eventname="SelectedIndexChanged" />
                                        </Triggers>--%>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlConcepto4" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="210px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoPers4" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoPers4" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoDias4" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoDias4" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upSubtotal4" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceSubtotal4" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upIVA4" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceIVA4" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upTotal4" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceTotal4" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upPorcentAut4" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebPercentEditor ID="wpePorcentAut4" runat="server" Font-Names="Verdana" Font-Size="7pt" ReadOnly="True" Width="55px">
                                            </ig:WebPercentEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upRFC4" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtRFC4" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="ibtnRFCBus4" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 230px; text-align: left">
                                    <asp:UpdatePanel ID="upProveedor4" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:HyperLink ID="hlProveedor4" runat="server" Width="220px">[hlProveedor4]</asp:HyperLink>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upNoFactura4" runat="server" updatemode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger controlid="ibtnRFCBus2" eventname="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlNoFactura4" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 10px">
                                                        <asp:UpdatePanel ID="upObsE19" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblNoFacturaE4" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlOrig4" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtOriDes4" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="10px" Visible="False"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlDest4" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                </td>
                                <td style="border: 1px solid #000000; width: 70px; text-align: center">
                                    <asp:TextBox ID="txtVehi4" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="60px"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; text-align: left">
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtObs4" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="160px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px">
                                                <asp:UpdatePanel ID="upObsE4" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblObsE4" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlConcepto5" runat="server">
                        <table style="width:1880px;">
                            <tr>
                                <td style="border: 1px solid #000000; width: 20px; text-align: center">
                                    <asp:Label ID="lbl_No5" runat="server" Text="5"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 100px; text-align: center">
                                    <ig:WebDatePicker ID="wdpFecha5" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="95px">
                                    </ig:WebDatePicker>
                                </td>
                                <td style="border: 1px solid #000000; width: 45px; text-align: center">
                                    <asp:UpdatePanel ID="upFactura5" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbFactura5" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upTabulador5" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbTabulador5" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 220px; text-align: center">
                                    <asp:UpdatePanel ID="upConcepto5" runat="server" updatemode="Conditional">
                                        <%--<Triggers>
                                            <asp:AsyncPostBackTrigger controlid="rblTipo2" eventname="SelectedIndexChanged" />
                                        </Triggers>--%>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlConcepto5" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="210px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoPers5" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoPers5" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoDias5" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoDias5" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upSubtotal5" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceSubtotal5" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upIVA5" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceIVA5" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upTotal5" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceTotal5" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upPorcentAut5" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebPercentEditor ID="wpePorcentAut5" runat="server" Font-Names="Verdana" Font-Size="7pt" ReadOnly="True" Width="55px">
                                            </ig:WebPercentEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upRFC5" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtRFC5" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="ibtnRFCBus5" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 230px; text-align: left">
                                    <asp:UpdatePanel ID="upProveedor5" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:HyperLink ID="hlProveedor5" runat="server" Width="220px">[hlProveedor5]</asp:HyperLink>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upNoFactura5" runat="server" updatemode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger controlid="ibtnRFCBus2" eventname="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlNoFactura5" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 10px">
                                                        <asp:UpdatePanel ID="upObsE20" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblNoFacturaE5" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlOrig5" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtOriDes5" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="10px" Visible="False"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlDest5" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                </td>
                                <td style="border: 1px solid #000000; width: 70px; text-align: center">
                                    <asp:TextBox ID="txtVehi5" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="60px"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; text-align: left">
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtObs5" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="160px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px">
                                                <asp:UpdatePanel ID="upObsE5" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblObsE5" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlConcepto6" runat="server">
                        <table style="width:1880px;">
                            <tr>
                                <td style="border: 1px solid #000000; width: 20px; text-align: center">
                                    <asp:Label ID="lbl_No6" runat="server" Text="6"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 100px; text-align: center">
                                    <ig:WebDatePicker ID="wdpFecha6" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="95px">
                                    </ig:WebDatePicker>
                                </td>
                                <td style="border: 1px solid #000000; width: 45px; text-align: center">
                                    <asp:UpdatePanel ID="upFactura6" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbFactura6" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upTabulador6" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbTabulador6" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 220px; text-align: center">
                                    <asp:UpdatePanel ID="upConcepto6" runat="server" updatemode="Conditional">
                                        <%--<Triggers>
                                            <asp:AsyncPostBackTrigger controlid="rblTipo2" eventname="SelectedIndexChanged" />
                                        </Triggers>--%>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlConcepto6" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="210px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoPers6" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoPers6" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoDias6" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoDias6" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upSubtotal6" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceSubtotal6" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upIVA6" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceIVA6" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upTotal6" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceTotal6" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upPorcentAut6" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebPercentEditor ID="wpePorcentAut6" runat="server" Font-Names="Verdana" Font-Size="7pt" ReadOnly="True" Width="55px">
                                            </ig:WebPercentEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upRFC6" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtRFC6" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="ibtnRFCBus6" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 230px; text-align: left">
                                    <asp:UpdatePanel ID="upProveedor6" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:HyperLink ID="hlProveedor6" runat="server" Width="220px">[hlProveedor6]</asp:HyperLink>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upNoFactura6" runat="server" updatemode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger controlid="ibtnRFCBus2" eventname="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlNoFactura6" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 10px">
                                                        <asp:UpdatePanel ID="upObsE21" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblNoFacturaE6" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlOrig6" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtOriDes6" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="10px" Visible="False"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlDest6" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                </td>
                                <td style="border: 1px solid #000000; width: 70px; text-align: center">
                                    <asp:TextBox ID="txtVehi6" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="60px"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; text-align: left">
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtObs6" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="160px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px">
                                                <asp:UpdatePanel ID="upObsE6" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblObsE6" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlConcepto7" runat="server">
                        <table style="width:1880px;">
                            <tr>
                                <td style="border: 1px solid #000000; width: 20px; text-align: center">
                                    <asp:Label ID="lbl_No7" runat="server" Text="7"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 100px; text-align: center">
                                    <ig:WebDatePicker ID="wdpFecha7" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="95px">
                                    </ig:WebDatePicker>
                                </td>
                                <td style="border: 1px solid #000000; width: 45px; text-align: center">
                                    <asp:UpdatePanel ID="upFactura7" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbFactura7" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upTabulador7" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbTabulador7" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 220px; text-align: center">
                                    <asp:UpdatePanel ID="upConcepto7" runat="server" updatemode="Conditional">
                                        <%--<Triggers>
                                            <asp:AsyncPostBackTrigger controlid="rblTipo2" eventname="SelectedIndexChanged" />
                                        </Triggers>--%>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlConcepto7" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="210px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoPers7" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoPers7" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoDias7" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoDias7" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upSubtotal7" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceSubtotal7" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upIVA7" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceIVA7" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upTotal7" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceTotal7" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upPorcentAut7" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebPercentEditor ID="wpePorcentAut7" runat="server" Font-Names="Verdana" Font-Size="7pt" ReadOnly="True" Width="55px">
                                            </ig:WebPercentEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upRFC7" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtRFC7" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="ibtnRFCBus7" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 230px; text-align: left">
                                    <asp:UpdatePanel ID="upProveedor7" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:HyperLink ID="hlProveedor7" runat="server" Width="220px">[hlProveedor3]</asp:HyperLink>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upNoFactura7" runat="server" updatemode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger controlid="ibtnRFCBus2" eventname="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlNoFactura7" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 10px">
                                                        <asp:UpdatePanel ID="upObsE22" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblNoFacturaE7" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlOrig7" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtOriDes7" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="10px" Visible="False"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlDest7" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                </td>
                                <td style="border: 1px solid #000000; width: 70px; text-align: center">
                                    <asp:TextBox ID="txtVehi7" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="60px"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; text-align: left">
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtObs7" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="160px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px">
                                                <asp:UpdatePanel ID="upObsE7" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblObsE7" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlConcepto8" runat="server">
                        <table style="width:1880px;">
                            <tr>
                                <td style="border: 1px solid #000000; width: 20px; text-align: center">
                                    <asp:Label ID="lbl_No8" runat="server" Text="8"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 100px; text-align: center">
                                    <ig:WebDatePicker ID="wdpFecha8" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="95px">
                                    </ig:WebDatePicker>
                                </td>
                                <td style="border: 1px solid #000000; width: 45px; text-align: center">
                                    <asp:UpdatePanel ID="upFactura8" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbFactura8" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upTabulador8" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbTabulador8" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 220px; text-align: center">
                                    <asp:UpdatePanel ID="upConcepto8" runat="server" updatemode="Conditional">
                                        <%--<Triggers>
                                            <asp:AsyncPostBackTrigger controlid="rblTipo2" eventname="SelectedIndexChanged" />
                                        </Triggers>--%>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlConcepto8" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="210px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoPers8" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoPers8" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoDias8" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoDias8" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upSubtotal8" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceSubtotal8" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upIVA8" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceIVA8" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upTotal8" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceTotal8" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upPorcentAut8" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebPercentEditor ID="wpePorcentAut8" runat="server" Font-Names="Verdana" Font-Size="7pt" ReadOnly="True" Width="55px">
                                            </ig:WebPercentEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upRFC8" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtRFC8" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="ibtnRFCBus8" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 230px; text-align: left">
                                    <asp:UpdatePanel ID="upProveedor8" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:HyperLink ID="hlProveedor8" runat="server" Width="220px">[hlProveedor3]</asp:HyperLink>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upNoFactura8" runat="server" updatemode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger controlid="ibtnRFCBus2" eventname="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlNoFactura8" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 10px">
                                                        <asp:UpdatePanel ID="upObsE23" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblNoFacturaE8" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlOrig8" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtOriDes8" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="10px" Visible="False"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlDest8" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                </td>
                                <td style="border: 1px solid #000000; width: 70px; text-align: center">
                                    <asp:TextBox ID="txtVehi8" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="60px"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; text-align: left">
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtObs8" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="160px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px">
                                                <asp:UpdatePanel ID="upObsE8" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblObsE8" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlConcepto9" runat="server">
                        <table style="width:1880px;">
                            <tr>
                                <td style="border: 1px solid #000000; width: 20px; text-align: center">
                                    <asp:Label ID="lbl_No9" runat="server" Text="9"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 100px; text-align: center">
                                    <ig:WebDatePicker ID="wdpFecha9" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="95px">
                                    </ig:WebDatePicker>
                                </td>
                                <td style="border: 1px solid #000000; width: 45px; text-align: center">
                                    <asp:UpdatePanel ID="upFactura9" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbFactura9" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upTabulador9" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbTabulador9" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 220px; text-align: center">
                                    <asp:UpdatePanel ID="upConcepto9" runat="server" updatemode="Conditional">
                                        <%--<Triggers>
                                            <asp:AsyncPostBackTrigger controlid="rblTipo2" eventname="SelectedIndexChanged" />
                                        </Triggers>--%>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlConcepto9" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="210px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoPers9" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoPers9" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoDias9" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoDias9" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upSubtotal9" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceSubtotal9" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upIVA9" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceIVA9" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upTotal9" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceTotal9" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upPorcentAut9" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebPercentEditor ID="wpePorcentAut9" runat="server" Font-Names="Verdana" Font-Size="7pt" ReadOnly="True" Width="55px">
                                            </ig:WebPercentEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upRFC9" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtRFC9" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="ibtnRFCBus9" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 230px; text-align: left">
                                    <asp:UpdatePanel ID="upProveedor9" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:HyperLink ID="hlProveedor9" runat="server" Width="220px">[hlProveedor3]</asp:HyperLink>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upNoFactura9" runat="server" updatemode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger controlid="ibtnRFCBus2" eventname="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlNoFactura9" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 10px">
                                                        <asp:UpdatePanel ID="upObsE24" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblNoFacturaE9" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlOrig9" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtOriDes9" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="10px" Visible="False"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlDest9" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                </td>
                                <td style="border: 1px solid #000000; width: 70px; text-align: center">
                                    <asp:TextBox ID="txtVehi9" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="60px"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; text-align: left">
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtObs9" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="160px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px">
                                                <asp:UpdatePanel ID="upObsE9" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblObsE9" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlConcepto10" runat="server">
                        <table style="width:1880px;">
                            <tr>
                                <td style="border: 1px solid #000000; width: 20px; text-align: center">
                                    <asp:Label ID="lbl_No10" runat="server" Text="10"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 100px; text-align: center">
                                    <ig:WebDatePicker ID="wdpFecha10" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="95px">
                                    </ig:WebDatePicker>
                                </td>
                                <td style="border: 1px solid #000000; width: 45px; text-align: center">
                                    <asp:UpdatePanel ID="upFactura10" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbFactura10" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upTabulador10" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbTabulador10" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 220px; text-align: center">
                                    <asp:UpdatePanel ID="upConcepto10" runat="server" updatemode="Conditional">
                                        <%--<Triggers>
                                            <asp:AsyncPostBackTrigger controlid="rblTipo2" eventname="SelectedIndexChanged" />
                                        </Triggers>--%>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlConcepto10" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="210px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoPers10" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoPers10" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoDias10" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoDias10" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upSubtotal10" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceSubtotal10" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upIVA10" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceIVA10" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upTotal10" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceTotal10" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upPorcentAut10" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebPercentEditor ID="wpePorcentAut10" runat="server" Font-Names="Verdana" Font-Size="7pt" ReadOnly="True" Width="55px">
                                            </ig:WebPercentEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upRFC10" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtRFC10" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="ibtnRFCBus10" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 230px; text-align: left">
                                    <asp:UpdatePanel ID="upProveedor10" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:HyperLink ID="hlProveedor10" runat="server" Width="220px">[hlProveedor3]</asp:HyperLink>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upNoFactura10" runat="server" updatemode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger controlid="ibtnRFCBus2" eventname="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlNoFactura10" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 10px">
                                                        <asp:UpdatePanel ID="upObsE25" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblNoFacturaE10" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlOrig10" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtOriDes10" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="10px" Visible="False"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlDest10" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                </td>
                                <td style="border: 1px solid #000000; width: 70px; text-align: center">
                                    <asp:TextBox ID="txtVehi10" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="60px"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; text-align: left">
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtObs10" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="160px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px">
                                                <asp:UpdatePanel ID="upObsE10" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblObsE10" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlConcepto11" runat="server">
                        <table style="width:1880px;">
                            <tr>
                                <td style="border: 1px solid #000000; width: 20px; text-align: center">
                                    <asp:Label ID="lbl_No11" runat="server" Text="11"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 100px; text-align: center">
                                    <ig:WebDatePicker ID="wdpFecha11" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="95px">
                                    </ig:WebDatePicker>
                                </td>
                                <td style="border: 1px solid #000000; width: 45px; text-align: center">
                                    <asp:UpdatePanel ID="upFactura11" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbFactura11" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upTabulador11" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbTabulador11" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 220px; text-align: center">
                                    <asp:UpdatePanel ID="upConcepto11" runat="server" updatemode="Conditional">
                                        <%--<Triggers>
                                            <asp:AsyncPostBackTrigger controlid="rblTipo2" eventname="SelectedIndexChanged" />
                                        </Triggers>--%>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlConcepto11" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="210px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoPers11" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoPers11" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoDias11" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoDias11" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upSubtotal11" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceSubtotal11" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upIVA11" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceIVA11" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upTotal11" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceTotal11" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upPorcentAut11" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebPercentEditor ID="wpePorcentAut11" runat="server" Font-Names="Verdana" Font-Size="7pt" ReadOnly="True" Width="55px">
                                            </ig:WebPercentEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upRFC11" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtRFC11" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="ibtnRFCBus11" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 230px; text-align: left">
                                    <asp:UpdatePanel ID="upProveedor11" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:HyperLink ID="hlProveedor11" runat="server" Width="220px">[hlProveedor3]</asp:HyperLink>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upNoFactura11" runat="server" updatemode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger controlid="ibtnRFCBus2" eventname="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlNoFactura11" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 10px">
                                                        <asp:UpdatePanel ID="upObsE26" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblNoFacturaE11" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlOrig11" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtOriDes11" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="10px" Visible="False"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlDest11" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                </td>
                                <td style="border: 1px solid #000000; width: 70px; text-align: center">
                                    <asp:TextBox ID="txtVehi11" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="60px"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; text-align: left">
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtObs11" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="160px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px">
                                                <asp:UpdatePanel ID="upObsE11" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblObsE11" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlConcepto12" runat="server">
                        <table style="width:1880px;">
                            <tr>
                                <td style="border: 1px solid #000000; width: 20px; text-align: center">
                                    <asp:Label ID="lbl_No12" runat="server" Text="12"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 100px; text-align: center">
                                    <ig:WebDatePicker ID="wdpFecha12" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="95px">
                                    </ig:WebDatePicker>
                                </td>
                                <td style="border: 1px solid #000000; width: 45px; text-align: center">
                                    <asp:UpdatePanel ID="upFactura12" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbFactura12" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upTabulador12" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbTabulador12" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 220px; text-align: center">
                                    <asp:UpdatePanel ID="upConcepto12" runat="server" updatemode="Conditional">
                                        <%--<Triggers>
                                            <asp:AsyncPostBackTrigger controlid="rblTipo2" eventname="SelectedIndexChanged" />
                                        </Triggers>--%>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlConcepto12" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="210px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoPers12" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoPers12" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoDias12" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoDias12" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upSubtotal12" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceSubtotal12" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upIVA12" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceIVA12" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upTotal12" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceTotal12" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upPorcentAut12" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebPercentEditor ID="wpePorcentAut12" runat="server" Font-Names="Verdana" Font-Size="7pt" ReadOnly="True" Width="55px">
                                            </ig:WebPercentEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upRFC12" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtRFC12" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="ibtnRFCBus12" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 230px; text-align: left">
                                    <asp:UpdatePanel ID="upProveedor12" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:HyperLink ID="hlProveedor12" runat="server" Width="220px">[hlProveedor3]</asp:HyperLink>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upNoFactura12" runat="server" updatemode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger controlid="ibtnRFCBus2" eventname="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlNoFactura12" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 10px">
                                                        <asp:UpdatePanel ID="upObsE27" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblNoFacturaE12" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlOrig12" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtOriDes12" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="10px" Visible="False"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlDest12" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                </td>
                                <td style="border: 1px solid #000000; width: 70px; text-align: center">
                                    <asp:TextBox ID="txtVehi12" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="60px"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; text-align: left">
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtObs12" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="160px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px">
                                                <asp:UpdatePanel ID="upObsE12" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblObsE12" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlConcepto13" runat="server">
                        <table style="width:1880px;">
                            <tr>
                                <td style="border: 1px solid #000000; width: 20px; text-align: center">
                                    <asp:Label ID="lbl_No13" runat="server" Text="13"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 100px; text-align: center">
                                    <ig:WebDatePicker ID="wdpFecha13" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="95px">
                                    </ig:WebDatePicker>
                                </td>
                                <td style="border: 1px solid #000000; width: 45px; text-align: center">
                                    <asp:UpdatePanel ID="upFactura13" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbFactura13" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upTabulador13" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbTabulador13" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 220px; text-align: center">
                                    <asp:UpdatePanel ID="upConcepto13" runat="server" updatemode="Conditional">
                                        <%--<Triggers>
                                            <asp:AsyncPostBackTrigger controlid="rblTipo2" eventname="SelectedIndexChanged" />
                                        </Triggers>--%>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlConcepto13" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="210px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoPers13" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoPers13" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoDias13" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoDias13" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upSubtotal13" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceSubtotal13" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upIVA13" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceIVA13" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upTotal13" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceTotal13" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upPorcentAut13" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebPercentEditor ID="wpePorcentAut13" runat="server" Font-Names="Verdana" Font-Size="7pt" ReadOnly="True" Width="55px">
                                            </ig:WebPercentEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upRFC13" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtRFC13" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="ibtnRFCBus13" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 230px; text-align: left">
                                    <asp:UpdatePanel ID="upProveedor13" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:HyperLink ID="hlProveedor13" runat="server" Width="220px">[hlProveedor3]</asp:HyperLink>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upNoFactura13" runat="server" updatemode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger controlid="ibtnRFCBus2" eventname="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlNoFactura13" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 10px">
                                                        <asp:UpdatePanel ID="upObsE28" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblNoFacturaE13" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlOrig13" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtOriDes13" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="10px" Visible="False"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlDest13" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                </td>
                                <td style="border: 1px solid #000000; width: 70px; text-align: center">
                                    <asp:TextBox ID="txtVehi13" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="60px"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; text-align: left">
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtObs13" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="160px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px">
                                                <asp:UpdatePanel ID="upObsE13" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblObsE13" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlConcepto14" runat="server">
                        <table style="width:1880px;">
                            <tr>
                                <td style="border: 1px solid #000000; width: 20px; text-align: center">
                                    <asp:Label ID="lbl_No14" runat="server" Text="14"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 100px; text-align: center">
                                    <ig:WebDatePicker ID="wdpFecha14" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="95px">
                                    </ig:WebDatePicker>
                                </td>
                                <td style="border: 1px solid #000000; width: 45px; text-align: center">
                                    <asp:UpdatePanel ID="upFactura14" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbFactura14" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upTabulador14" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbTabulador14" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 220px; text-align: center">
                                    <asp:UpdatePanel ID="upConcepto14" runat="server" updatemode="Conditional">
                                        <%--<Triggers>
                                            <asp:AsyncPostBackTrigger controlid="rblTipo2" eventname="SelectedIndexChanged" />
                                        </Triggers>--%>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlConcepto14" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="210px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoPers14" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoPers14" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoDias14" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoDias14" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upSubtotal14" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceSubtotal14" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upIVA14" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceIVA14" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upTotal14" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceTotal14" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upPorcentAut14" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebPercentEditor ID="wpePorcentAut14" runat="server" Font-Names="Verdana" Font-Size="7pt" ReadOnly="True" Width="55px">
                                            </ig:WebPercentEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upRFC14" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtRFC14" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="ibtnRFCBus14" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 230px; text-align: left">
                                    <asp:UpdatePanel ID="upProveedor14" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:HyperLink ID="hlProveedor14" runat="server" Width="220px">[hlProveedor3]</asp:HyperLink>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upNoFactura14" runat="server" updatemode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger controlid="ibtnRFCBus2" eventname="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlNoFactura14" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 10px">
                                                        <asp:UpdatePanel ID="upObsE29" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblNoFacturaE14" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlOrig14" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtOriDes14" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="10px" Visible="False"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlDest14" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                </td>
                                <td style="border: 1px solid #000000; width: 70px; text-align: center">
                                    <asp:TextBox ID="txtVehi14" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="60px"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; text-align: left">
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtObs14" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="160px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px">
                                                <asp:UpdatePanel ID="upObsE14" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblObsE14" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlConcepto15" runat="server">
                        <table style="width:1880px;">
                            <tr>
                                <td style="border: 1px solid #000000; width: 20px; text-align: center">
                                    <asp:Label ID="lbl_No15" runat="server" Text="15"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 100px; text-align: center">
                                    <ig:WebDatePicker ID="wdpFecha15" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="95px">
                                    </ig:WebDatePicker>
                                </td>
                                <td style="border: 1px solid #000000; width: 45px; text-align: center">
                                    <asp:UpdatePanel ID="upFactura15" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbFactura15" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upTabulador15" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbTabulador15" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 220px; text-align: center">
                                    <asp:UpdatePanel ID="upConcepto15" runat="server" updatemode="Conditional">
                                        <%--<Triggers>
                                            <asp:AsyncPostBackTrigger controlid="rblTipo2" eventname="SelectedIndexChanged" />
                                        </Triggers>--%>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlConcepto15" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="210px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoPers15" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoPers15" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoDias15" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoDias15" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upSubtotal15" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceSubtotal15" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upIVA15" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceIVA15" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upTotal15" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceTotal15" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upPorcentAut15" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebPercentEditor ID="wpePorcentAut15" runat="server" Font-Names="Verdana" Font-Size="7pt" ReadOnly="True" Width="55px">
                                            </ig:WebPercentEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upRFC15" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtRFC15" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="ibtnRFCBus15" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 230px; text-align: left">
                                    <asp:UpdatePanel ID="upProveedor15" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:HyperLink ID="hlProveedor15" runat="server" Width="220px">[hlProveedor3]</asp:HyperLink>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upNoFactura15" runat="server" updatemode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger controlid="ibtnRFCBus2" eventname="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlNoFactura15" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 10px">
                                                        <asp:UpdatePanel ID="upObsE30" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblNoFacturaE15" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlOrig15" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtOriDes15" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="10px" Visible="False"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlDest15" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                </td>
                                <td style="border: 1px solid #000000; width: 70px; text-align: center">
                                    <asp:TextBox ID="txtVehi15" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="60px"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; text-align: left">
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtObs15" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="160px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px">
                                                <asp:UpdatePanel ID="upObsE15" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblObsE15" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlConcepto16" runat="server">
                        <table style="width:1880px;">
                            <tr>
                                <td style="border: 1px solid #000000; width: 20px; text-align: center">
                                    <asp:Label ID="lbl_No16" runat="server" Text="16"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 100px; text-align: center">
                                    <ig:WebDatePicker ID="wdpFecha16" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="95px">
                                    </ig:WebDatePicker>
                                </td>
                                <td style="border: 1px solid #000000; width: 45px; text-align: center">
                                    <asp:UpdatePanel ID="upFactura16" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbFactura16" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upTabulador16" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbTabulador16" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 220px; text-align: center">
                                    <asp:UpdatePanel ID="upConcepto16" runat="server" updatemode="Conditional">
                                        <%--<Triggers>
                                            <asp:AsyncPostBackTrigger controlid="rblTipo2" eventname="SelectedIndexChanged" />
                                        </Triggers>--%>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlConcepto16" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="210px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoPers16" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoPers16" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoDias16" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoDias16" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upSubtotal16" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceSubtotal16" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upIVA16" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceIVA16" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upTotal16" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceTotal16" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upPorcentAut16" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebPercentEditor ID="wpePorcentAut16" runat="server" Font-Names="Verdana" Font-Size="7pt" ReadOnly="True" Width="55px">
                                            </ig:WebPercentEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upRFC16" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtRFC16" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="ibtnRFCBus16" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 230px; text-align: left">
                                    <asp:UpdatePanel ID="upProveedor16" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:HyperLink ID="hlProveedor16" runat="server" Width="220px">[hlProveedor3]</asp:HyperLink>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upNoFactura16" runat="server" updatemode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger controlid="ibtnRFCBus2" eventname="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlNoFactura16" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 10px">
                                                        <asp:UpdatePanel ID="upObsE31" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblNoFacturaE16" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlOrig16" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtOriDes16" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="10px" Visible="False"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlDest16" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                </td>
                                <td style="border: 1px solid #000000; width: 70px; text-align: center">
                                    <asp:TextBox ID="txtVehi16" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="60px"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; text-align: left">
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtObs16" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="160px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px">
                                                <asp:UpdatePanel ID="upObsE32" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblObsE16" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlConcepto17" runat="server">
                        <table style="width:1880px;">
                            <tr>
                                <td style="border: 1px solid #000000; width: 20px; text-align: center">
                                    <asp:Label ID="lbl_No17" runat="server" Text="17"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 100px; text-align: center">
                                    <ig:WebDatePicker ID="wdpFecha17" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="95px">
                                    </ig:WebDatePicker>
                                </td>
                                <td style="border: 1px solid #000000; width: 45px; text-align: center">
                                    <asp:UpdatePanel ID="upFactura17" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbFactura17" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upTabulador17" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbTabulador17" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 220px; text-align: center">
                                    <asp:UpdatePanel ID="upConcepto17" runat="server" updatemode="Conditional">
                                        <%--<Triggers>
                                            <asp:AsyncPostBackTrigger controlid="rblTipo2" eventname="SelectedIndexChanged" />
                                        </Triggers>--%>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlConcepto17" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="210px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoPers17" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoPers17" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoDias17" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoDias17" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upSubtotal17" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceSubtotal17" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upIVA17" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceIVA17" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upTotal17" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceTotal17" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upPorcentAut17" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebPercentEditor ID="wpePorcentAut17" runat="server" Font-Names="Verdana" Font-Size="7pt" ReadOnly="True" Width="55px">
                                            </ig:WebPercentEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upRFC17" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtRFC17" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="ibtnRFCBus17" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 230px; text-align: left">
                                    <asp:UpdatePanel ID="upProveedor17" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:HyperLink ID="hlProveedor17" runat="server" Width="220px">[hlProveedor3]</asp:HyperLink>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upNoFactura17" runat="server" updatemode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger controlid="ibtnRFCBus2" eventname="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlNoFactura17" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 10px">
                                                        <asp:UpdatePanel ID="upObsE33" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblNoFacturaE17" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlOrig17" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtOriDes17" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="10px" Visible="False"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlDest17" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                </td>
                                <td style="border: 1px solid #000000; width: 70px; text-align: center">
                                    <asp:TextBox ID="txtVehi17" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="60px"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; text-align: left">
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtObs17" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="160px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px">
                                                <asp:UpdatePanel ID="upObsE34" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblObsE17" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlConcepto18" runat="server">
                        <table style="width:1880px;">
                            <tr>
                                <td style="border: 1px solid #000000; width: 20px; text-align: center">
                                    <asp:Label ID="lbl_No18" runat="server" Text="18"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 100px; text-align: center">
                                    <ig:WebDatePicker ID="wdpFecha18" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="95px">
                                    </ig:WebDatePicker>
                                </td>
                                <td style="border: 1px solid #000000; width: 45px; text-align: center">
                                    <asp:UpdatePanel ID="upFactura18" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbFactura18" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upTabulador18" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbTabulador18" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 220px; text-align: center">
                                    <asp:UpdatePanel ID="upConcepto18" runat="server" updatemode="Conditional">
                                        <%--<Triggers>
                                            <asp:AsyncPostBackTrigger controlid="rblTipo2" eventname="SelectedIndexChanged" />
                                        </Triggers>--%>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlConcepto18" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="210px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoPers18" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoPers18" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoDias18" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoDias18" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upSubtotal18" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceSubtotal18" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upIVA18" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceIVA18" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upTotal18" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceTotal18" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upPorcentAut18" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebPercentEditor ID="wpePorcentAut18" runat="server" Font-Names="Verdana" Font-Size="7pt" ReadOnly="True" Width="55px">
                                            </ig:WebPercentEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upRFC18" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtRFC18" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="ibtnRFCBus18" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 230px; text-align: left">
                                    <asp:UpdatePanel ID="upProveedor18" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:HyperLink ID="hlProveedor18" runat="server" Width="220px">[hlProveedor3]</asp:HyperLink>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upNoFactura18" runat="server" updatemode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger controlid="ibtnRFCBus2" eventname="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlNoFactura18" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 10px">
                                                        <asp:UpdatePanel ID="upObsE35" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblNoFacturaE18" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlOrig18" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtOriDes18" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="10px" Visible="False"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlDest18" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                </td>
                                <td style="border: 1px solid #000000; width: 70px; text-align: center">
                                    <asp:TextBox ID="txtVehi18" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="60px"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; text-align: left">
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtObs18" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="160px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px">
                                                <asp:UpdatePanel ID="upObsE36" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblObsE18" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlConcepto19" runat="server">
                        <table style="width:1880px;">
                            <tr>
                                <td style="border: 1px solid #000000; width: 20px; text-align: center">
                                    <asp:Label ID="lbl_No19" runat="server" Text="19"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 100px; text-align: center">
                                    <ig:WebDatePicker ID="wdpFecha19" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="95px">
                                    </ig:WebDatePicker>
                                </td>
                                <td style="border: 1px solid #000000; width: 45px; text-align: center">
                                    <asp:UpdatePanel ID="upFactura19" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbFactura19" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upTabulador19" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbTabulador19" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 220px; text-align: center">
                                    <asp:UpdatePanel ID="upConcepto19" runat="server" updatemode="Conditional">
                                        <%--<Triggers>
                                            <asp:AsyncPostBackTrigger controlid="rblTipo2" eventname="SelectedIndexChanged" />
                                        </Triggers>--%>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlConcepto19" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="210px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoPers19" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoPers19" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoDias19" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoDias19" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upSubtotal19" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceSubtotal19" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upIVA19" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceIVA19" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upTotal19" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceTotal19" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upPorcentAut19" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebPercentEditor ID="wpePorcentAut19" runat="server" Font-Names="Verdana" Font-Size="7pt" ReadOnly="True" Width="55px">
                                            </ig:WebPercentEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upRFC19" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtRFC19" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="ibtnRFCBus19" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 230px; text-align: left">
                                    <asp:UpdatePanel ID="upProveedor19" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:HyperLink ID="hlProveedor19" runat="server" Width="220px">[hlProveedor3]</asp:HyperLink>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upNoFactura19" runat="server" updatemode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger controlid="ibtnRFCBus2" eventname="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlNoFactura19" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 10px">
                                                        <asp:UpdatePanel ID="upObsE37" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblNoFacturaE19" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlOrig19" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtOriDes19" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="10px" Visible="False"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlDest19" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                </td>
                                <td style="border: 1px solid #000000; width: 70px; text-align: center">
                                    <asp:TextBox ID="txtVehi19" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="60px"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; text-align: left">
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtObs19" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="160px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px">
                                                <asp:UpdatePanel ID="upObsE38" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblObsE19" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlConcepto20" runat="server">
                        <table style="width:1880px;">
                            <tr>
                                <td style="border: 1px solid #000000; width: 20px; text-align: center">
                                    <asp:Label ID="lbl_No20" runat="server" Text="20"></asp:Label>
                                </td>
                                <td style="border: 1px solid #000000; width: 100px; text-align: center">
                                    <ig:WebDatePicker ID="wdpFecha20" runat="server" DisplayModeFormat="d" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="95px">
                                    </ig:WebDatePicker>
                                </td>
                                <td style="border: 1px solid #000000; width: 45px; text-align: center">
                                    <asp:UpdatePanel ID="upFactura20" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbFactura20" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upTabulador20" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="cbTabulador20" runat="server" AutoPostBack="True" Text=" " />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 220px; text-align: center">
                                    <asp:UpdatePanel ID="upConcepto20" runat="server" updatemode="Conditional">
                                        <%--<Triggers>
                                            <asp:AsyncPostBackTrigger controlid="rblTipo2" eventname="SelectedIndexChanged" />
                                        </Triggers>--%>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlConcepto20" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="210px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoPers20" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoPers20" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 40px; text-align: center">
                                    <asp:UpdatePanel ID="upNoDias20" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebNumericEditor ID="wneNoDias20" runat="server" DataMode="Int" Font-Names="Verdana" Font-Size="8pt" MinValue="1" Nullable="False" Width="30px">
                                            </ig:WebNumericEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upSubtotal20" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceSubtotal20" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upIVA20" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceIVA20" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 90px; text-align: center">
                                    <asp:UpdatePanel ID="upTotal20" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebCurrencyEditor ID="wceTotal20" runat="server" Width="80px">
                                            </ig:WebCurrencyEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 60px; text-align: center">
                                    <asp:UpdatePanel ID="upPorcentAut20" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ig:WebPercentEditor ID="wpePorcentAut20" runat="server" Font-Names="Verdana" Font-Size="7pt" ReadOnly="True" Width="55px">
                                            </ig:WebPercentEditor>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upRFC20" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtRFC20" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="ibtnRFCBus20" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 230px; text-align: left">
                                    <asp:UpdatePanel ID="upProveedor20" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:HyperLink ID="hlProveedor20" runat="server" Width="220px">[hlProveedor3]</asp:HyperLink>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 140px; text-align: center">
                                    <asp:UpdatePanel ID="upNoFactura20" runat="server" updatemode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger controlid="ibtnRFCBus2" eventname="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlNoFactura20" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 10px">
                                                        <asp:UpdatePanel ID="upObsE39" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblNoFacturaE20" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlOrig20" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtOriDes20" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="10px" Visible="False"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; width: 75px; text-align: center">
                                    <asp:DropDownList ID="ddlDest20" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px">
                                    </asp:DropDownList>
                                </td>
                                <td style="border: 1px solid #000000; width: 70px; text-align: center">
                                    <asp:TextBox ID="txtVehi20" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="60px"></asp:TextBox>
                                </td>
                                <td style="border: 1px solid #000000; text-align: left">
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtObs20" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="160px"></asp:TextBox>
                                            </td>
                                            <td style="width: 10px">
                                                <asp:UpdatePanel ID="upObsE40" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblObsE20" runat="server" Font-Bold="True" ForeColor="Red" Text="*"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table style="width: 1366px; height: 50px;">
                        <tr>
                            <td style="text-align: center">
                                <table style="width: 678px;">
                                    <tr>
                                        <td style="width: 130px;">
                                            <ig:WebCurrencyEditor ID="wceTotalA" runat="server" Width="40px" Visible="False">
                                            </ig:WebCurrencyEditor>
                                            <ig:WebCurrencyEditor ID="wceTotalC" runat="server" Visible="False" Width="40px">
                                            </ig:WebCurrencyEditor>
                                            <ig:WebCurrencyEditor ID="wceTotalS" runat="server" Visible="False" Width="40px">
                                            </ig:WebCurrencyEditor>
                                        </td>
                                        <td>
                                            <table style="width:100%;">
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
                                                    <td rowspan="2">
                                                        <asp:Button ID="btnSumar" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnSumar_Click" OnClientClick="this.disabled = true;" Text="Sumar" UseSubmitBehavior="false" Width="90px" />
                                                    </td>
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
                                <table style="width:100%;">
                                    <tr>
                                        <td style="height: 20px; text-align: left;">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="width: 100px; text-align: right;">
                                                        <asp:Label ID="lbl_Evidencia" runat="server" Text="Evidencia:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:FileUpload ID="fuEvidencia" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="320px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 20px">
                                            <asp:UpdatePanel ID="upValeIng" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <table style="width:100%;">
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
                    <table style="width: 1366px; height: 40px;">
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnGuardar" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnGuardar_Click" OnClientClick="this.disabled = true;" Text="Enviar a Aprobación" UseSubmitBehavior="false" Width="200px" />
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
