<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ConsEval.aspx.vb" Inherits="ProcAd.ConsEval" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style5 {
            width: 142px;
        }
        .auto-style46 {
            width: 483px;
        }
        </style>
    <script type="text/javascript">
        function impFormato() {
            window.open("IEvaluacion.aspx", "Get", "width=910, height=680, resizeable=no, scrollbars=yes, toolbar=no, menubar=no")
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="width: 1366px; text-align: center; font-weight: bold; color: #FF0000;">
                <ig:WebScriptManager ID="wsmConsEval" runat="server">
                </ig:WebScriptManager>
                    <asp:TextBox ID="_txtEmpleado" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                    <asp:TextBox ID="_txtNoEmpleado" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtPerfil" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlFiltros" runat="server" Width="1366px">
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: right; width: 180px;">&nbsp;</td>
                            <td style="text-align: center; " class="auto-style5">
                                <asp:Label ID="lbl_Filtros" runat="server" Font-Bold="True" Text="Filtros..."></asp:Label>
                            </td>
                            <td >&nbsp;</td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlSolicitó" runat="server">
                        <table style="width:100%;">
                            <tr>
                                <td style="text-align: right; width: 40px;">&nbsp;</td>
                                <td style="text-align: left; width: 120px;">
                                    <asp:CheckBox ID="cbEmpresa" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Empresa:" />
                                </td>
                                <td style="text-align: left; width: 310px;">
                                    <asp:Panel ID="pnlEmpresa" runat="server" Width="290px">
                                        <asp:DropDownList ID="ddlEmpresa" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="200px">
                                        </asp:DropDownList>
                                    </asp:Panel>
                                </td>
                                <td style="text-align: left; width: 120px;">
                                    <asp:CheckBox ID="cbDireccion" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Dirección:" />
                                </td>
                                <td style="text-align: left; width: 300px;">
                                    <asp:Panel ID="pnlDireccion" runat="server" Width="290px">
                                        <asp:DropDownList ID="ddlDireccion" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="290px">
                                        </asp:DropDownList>
                                    </asp:Panel>
                                </td>
                                <td style="text-align: left; width: 120px;">
                                    <asp:CheckBox ID="cbEmpleado" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Empleado:" />
                                </td>
                                <td>
                                    <asp:Panel ID="pnlEmpleado" runat="server" Width="310px">
                                        <asp:DropDownList ID="ddlEmpleado" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="290px">
                                        </asp:DropDownList>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: right; width: 40px;">&nbsp;</td>
                            <td style="text-align: left; width: 120px;">
                                <asp:CheckBox ID="cbFechaR" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Fecha Registro:" />
                            </td>
                            <td style="text-align: left; width: 310px;">
                                <asp:Panel ID="pnlFechaR" runat="server" Width="290px">
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
                            </td>
                            <td style="text-align: left; width: 120px;">
                                <asp:CheckBox ID="cbMesEval" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Mes Evaluado:" />
                            </td>
                            <td style="text-align: left; width: 300px;">
                                <asp:Panel ID="pnlMesEval" runat="server" Width="290px">
                                    <asp:DropDownList ID="ddlMesEval" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="200px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td style="text-align: left; width: 120px;">
                                <asp:CheckBox ID="cbEvaluador" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Evaluador:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlEvaluador" runat="server" Width="310px">
                                    <asp:DropDownList ID="ddlEvaluador" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">&nbsp;</td>
                            <td style="text-align: left">
                                <asp:CheckBox ID="cbStatus" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Estado:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlStatus" runat="server" Width="290px">
                                    <asp:DropDownList ID="ddlStatus" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="140px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:CheckBox ID="cbNoEval" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Folio:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlNoEval" runat="server" Width="310px">
                                    <asp:TextBox ID="txtNoEval" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:TextBox>
                                </asp:Panel>
                            </td>
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
                                        <td>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="width: 10px">&nbsp;</td>
                                                    <td>
                                                        <table style="width: 1320px; height: 35px;">
                                                            <tr>
                                                                <td style="text-align: center; width: 300px;">
                                                                    <asp:Button ID="btnExportar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Exportar" Width="150px" />
                                                                </td>
                                                                <td style="text-align: center">&nbsp;</td>
                                                                <td style="text-align: center; width: 300px;">
                                                                    <asp:Panel ID="pnlImp" runat="server">
                                                                        <input id="btnImpEval" style="font-family: Verdana; font-size: 8pt; " type="button" value="Imprimir Evaluación" onclick="impFormato();" />
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 10px">&nbsp;</td>
                                                    <td>
                                                        <asp:GridView ID="gvRegistros" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1800px">
                                                            <Columns>
                                                                <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                <ItemStyle Width="15px" />
                                                                </asp:CommandField>
                                                                <asp:BoundField DataField="id_ms_evaluacion" HeaderText="Folio">
                                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="no_empleado" HeaderText="No. Empleado">
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="colaborador" HeaderText="Colaborador">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="lider" HeaderText="Líder">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                                                                <asp:BoundField DataField="area" HeaderText="Área">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="unidad_neg" HeaderText="Unidad Negocio">
                                                                <ItemStyle HorizontalAlign="Left" Width="110px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="puesto" HeaderText="Puesto">
                                                                <ItemStyle HorizontalAlign="Left" Width="170px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="puesto_lider" HeaderText="Puesto Líder">
                                                                <ItemStyle HorizontalAlign="Left" Width="170px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="mes_eval" HeaderText="Mes Eval.">
                                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="porcent_cumpl" DataFormatString="{0:p}" HeaderText="Cumpl.">
                                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="cobra_bono_asist" HeaderText="Bono Asistencia">
                                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="cobra_bono_cumpl_UN" HeaderText="Bono Cumpl UN">
                                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="porcent_bono_cumpl_UN" DataFormatString="{0:p}" HeaderText="% Cumpl UN">
                                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="estatus" HeaderText="Estatus">
                                                                <ItemStyle HorizontalAlign="Center" Width="150px" />
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
                                                        <asp:GridView ID="gvRegistrosT" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Visible="False">
                                                            <Columns>
                                                                <asp:BoundField DataField="id_ms_evaluacion" HeaderText="Folio">
                                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="no_empleado" HeaderText="No. Empleado">
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="colaborador" HeaderText="Colaborador">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="lider" HeaderText="Líder">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                                                                <asp:BoundField DataField="area" HeaderText="Área">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="unidad_neg" HeaderText="Unidad Negocio">
                                                                <ItemStyle HorizontalAlign="Left" Width="110px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="puesto" HeaderText="Puesto">
                                                                <ItemStyle HorizontalAlign="Left" Width="170px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="puesto_lider" HeaderText="Puesto Líder">
                                                                <ItemStyle HorizontalAlign="Left" Width="170px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="mes_eval" HeaderText="Mes Eval.">
                                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="porcent_cumpl" DataFormatString="{0:p}" HeaderText="Cumpl.">
                                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="cobra_bono_asist" HeaderText="Bono Asistencia">
                                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="cobra_bono_cumpl_UN" HeaderText="Bono Cumpl UN">
                                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="porcent_bono_cumpl_UN" DataFormatString="{0:p}" HeaderText="% Cumpl UN">
                                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="estatus" HeaderText="Estatus">
                                                                <ItemStyle HorizontalAlign="Center" Width="150px" />
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
                                                <tr>
                                                    <td style="width: 10px">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        </table>
</asp:Content>
