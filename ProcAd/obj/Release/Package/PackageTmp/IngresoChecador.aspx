<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="IngresoChecador.aspx.vb" Inherits="ProcAd.IngresoChecador" %>
<%@ Register Assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<%@ Register Assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI" TagPrefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .igte_Edit
        {
	        background-color:White;
	        font-size:13px;
	        font-family: Segoe UI, Verdana, Arial, Sans-Serif;
	        border:solid 1px #CCCCCC;
	        outline: 0;
	        color:#333333;
        }

        .auto-style19 {
            width: 330px;
            height: 10px;
        }
        .auto-style43 {
            width: 30px;
            height: 10px;
        }
        .auto-style45 {
            height: 10px;
        }
        .auto-style46 {
            width: 10px;
        }
        .auto-style55 {
            width: 925px;
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
            
        .auto-style67 {
            height: 17px;
        }
            
        .auto-style68 {
            width: 310px;
            height: 10px;
        }
    </style>

    <script type="text/javascript">
        function impLibResp() {
            window.open("impLibResp.aspx", "Get", "width=800, height=950, resizeable=no, scrollbars=yes, toolbar=no, menubar=no")
        }
   </script>	
    <script type="text/javascript">
          function impEncuesta() {
            window.open("impEncuesta.aspx", "Get", "width=800, height=950, resizeable=no, scrollbars=yes, toolbar=no, menubar=no")
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>
                 <table style="width: 1360px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="color: #FF0000; text-align: center">

                <ig:WebScriptManager ID="wsmChrcador" runat="server"></ig:WebScriptManager>
                <table style="width: 1360px;">
                    <tr>
                        <td>
                            <table style="width: 100%; text-align: center; margin-right: auto; margin-left: auto;">
                                <tr>
                                    <td style="width: 1360px;">
                                        <asp:Literal ID="litError" runat="server"></asp:Literal>
                                        <asp:TextBox ID="_txtIdUsuario" runat="server" Visible="False" Width="16px"></asp:TextBox>
                                        <asp:TextBox ID="_txtIdMsInst" runat="server" Visible="False" Width="16px"></asp:TextBox>
                                        <asp:TextBox ID="_txtPerfil" runat="server" Visible="False" Width="16px"></asp:TextBox>
                                        <asp:TextBox ID="_txtNoEmpleado" runat="server" Visible="False" Width="16px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlFiltros" runat="server">
                    <table style="width: 1360px;">
                        <tr>
                            <td class="auto-style43" style="text-align: right"></td>
                            <td style="text-align: left" colspan="2" class="auto-style45">
                                <asp:Label ID="lbl_Categorias" runat="server" Font-Bold="True" Text="Habilitar Filtros"></asp:Label>
                            </td>
                            <td class="auto-style45"></td>
                            <td class="auto-style19"></td>
                            <td class="auto-style45"></td>
                            <td class="auto-style68"></td>
                        </tr>
                        <tr>
                            <td class="auto-style43" style="text-align: right"></td>
                            <td class="auto-style45" style="text-align: left">
                                <asp:CheckBox ID="cbEmpresa" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Empresa:" />
                            </td>
                            <td class="auto-style19">
                                <asp:Panel ID="pnlEmpresa" runat="server" Width="330px">
                                    <asp:DropDownList ID="ddlEmpresa" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="100px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td class="auto-style45">
                                <asp:CheckBox ID="cbDireccion" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Direccion:" Width="100px" />
                            </td>
                            <td class="auto-style19">
                                <asp:Panel ID="pnlDireccion" runat="server" Width="330px">
                                    <asp:DropDownList ID="ddlDireccion" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td class="auto-style45">
                                <asp:CheckBox ID="cbPuesto" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Puesto:" />
                            </td>
                            <td class="auto-style45">
                                <asp:Panel ID="pnlPuesto" runat="server" Width="250px">
                                    <asp:DropDownList ID="ddlPuesto" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style43" style="text-align: right"></td>
                            <td class="auto-style45" style="text-align: left">
                                <asp:CheckBox ID="cbPeriodo" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Periodo:" />
                            </td>
                            <td class="auto-style19">
                                <asp:Panel ID="pnlFecha" runat="server">
                                    <table style="width: 260px;">
                                        <tr>
                                            <td style="border: medium inset #808080; margin-right: auto; margin-left: auto;">
                                                <table style="width: 127px;">
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lbl_FechaAsigI" runat="server" Text="Fecha Inicial"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <ig:WebDatePicker ID="wdpFechaI" runat="server" Font-Names="Verdana" Font-Size="8pt" Nullable="False">
                                                            </ig:WebDatePicker>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="auto-style46">&nbsp;</td>
                                            <td style="border: medium inset #808080; margin-right: auto; margin-left: auto;">
                                                <table style="width: 127px;">
                                                    <tr>
                                                        <td style="text-align: center" class="auto-style67">
                                                            <asp:Label ID="lbl_FechaAsigF" runat="server" Text="Fecha Final"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <ig:WebDatePicker ID="wdpFechaF" runat="server" Font-Names="Verdana" Font-Size="8pt" Nullable="False">
                                                            </ig:WebDatePicker>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td class="auto-style45">
                                <asp:CheckBox ID="cbNombre" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="120px" Text="Nombre:" />
                            </td>
                            <td class="auto-style19">
                                <asp:Panel ID="pnlNombre" runat="server">
                                    <asp:TextBox ID="txtNombre" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px"></asp:TextBox>
                                </asp:Panel>
                            </td>

                            <td>
                                <asp:CheckBox ID="cbDepartamento" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Departamento:" Width="120px" />
                            </td>
                            <td>&nbsp;
                                <asp:Panel ID="pnlDepartamento" runat="server">
                                    <asp:DropDownList ID="ddlDepartamento" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style43" style="text-align: right"></td>
                            <td class="auto-style45" style="text-align: left">
                                <asp:CheckBox ID="cbHorario" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Horario:" />
                            </td>
                            <td class="auto-style45">
                                <asp:Panel ID="pnlHorario" runat="server">
                                    <asp:RadioButton ID="rbtnAm" runat="server" GroupName="horario" Text="a.m." AutoPostBack="True" />
                                    <asp:RadioButton ID="rbtnPm" runat="server" GroupName="horario" Text="p.m" AutoPostBack="True" />
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td> </td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                    
                    </table>
                </asp:Panel>
                <table>
                    <tr>
                        <td width="438px"></td>
                        <td>
                             <asp:Button ID="btnExportar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Exportar" Width="150px" />
                        </td>
                        <td width="150px">
                        </td>
                        <td>
                            <asp:Button ID="btnBuscar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Buscar" Width="150px" />
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
    </table>

    <asp:Panel ID="pnlRegistros" runat="server" Visible="False">
        <table style="width: 100%;">
            <tr>
                <td class="auto-style55">
                    <asp:GridView ID="gvRegistrosT" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" GridLines="Horizontal" Visible="False" Width="1500px">
                        <Columns>
                            <asp:BoundField DataField="no_empleado" HeaderText="No. Empleado" />
                            <asp:BoundField DataField="Ingreso" HeaderText="Fecha Checador">
                                <ItemStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="direccion" HeaderText="Direccion" />
                            <asp:BoundField DataField="empresa" HeaderText="Empresa" />
                            <asp:BoundField DataField="departamento" HeaderText="Departamento" />
                            <asp:BoundField DataField="puesto" HeaderText="puesto" />
                            <asp:BoundField DataField="centro_consto" HeaderText="Centro de Costos" />
                            <asp:BoundField DataField="ubicacion" HeaderText="Ubicacion" />
                            <asp:BoundField DataField="tipoNomina" HeaderText="Tip. Nomina" />
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
    </asp:Panel>
            </td>
        </tr>
    </table>
  
</asp:Content>
