<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="CargasSinID.aspx.vb" Inherits="ProcAd.CargasSinID" %>
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
        .auto-style28 {
            width: 110px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; font-weight: bold; color: #FF0000;">
                <ig:WebScriptManager ID="wsmCargasSinID" runat="server">
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
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: right; width: 80px;">&nbsp;</td>
                            <td style="text-align: left; width: 120px;">
                                <asp:CheckBox ID="cbFechaC" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Fecha de Carga:" />
                            </td>
                            <td style="text-align: left; width: 340px;">
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
                            </td>
                            <td style="text-align: right; width: 120px;">
                                <asp:CheckBox ID="cbConductor" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Conductor:" />
                            </td>
                            <td style="width: 290px">
                                <asp:Panel ID="pnlConductor" runat="server" Width="285px">
                                    <asp:TextBox ID="txtConductor" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="270px"></asp:TextBox>
                                </asp:Panel>
                            </td>
                            <td style="width: 120px; text-align: right;">
                                <asp:CheckBox ID="cbUnidad" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Unidad:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlUnidad" runat="server" Width="170px">
                                    <asp:TextBox ID="txtUnidad" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="150px"></asp:TextBox>
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
                                        <td style="width: 20px">&nbsp;</td>
                                        <td>
                                            <asp:GridView ID="gvRegistros" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1310px">
                                                <Columns>
                                                        <asp:BoundField DataField="id_dt_carga_comb" HeaderText="id_dt_carga_comb" />
                                                        <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                        <ItemStyle Width="15px" />
                                                        </asp:CommandField>
                                                        <asp:BoundField DataField="Región" HeaderText="Región">
                                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Centro de Costos" HeaderText="Centro de Costos">
                                                        <ItemStyle HorizontalAlign="Left" Width="170px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Identificador Vehículo" HeaderText="Unidad">
                                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Vehículo" HeaderText="Vehículo">
                                                        <ItemStyle HorizontalAlign="Left" Width="160px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Transacción" HeaderText="Transacción">
                                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="razon_social_afiliado" HeaderText="Estación de Carga">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Fecha Transacción" HeaderText="Fecha Transacción" DataFormatString="{0:d}">
                                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="cantidad_mercancia" HeaderText="Litros">
                                                        <ItemStyle HorizontalAlign="Right" Width="60px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Importe Transacción" HeaderText="Importe" DataFormatString="{0:c}">
                                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ID Conductor Edenred" HeaderText="ID Conductor Edenred">
                                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Conductor" HeaderText="Conductor">
                                                        <ItemStyle Width="180px" />
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
                            <td style="text-align: center; ">
                                &nbsp;</td>
                            <td style="text-align: right; width: 150px;">
                                <asp:Label ID="lbl_Fecha" runat="server" Text="Fecha de Transacción:"></asp:Label>
                            </td>
                            <td class="auto-style28" style="width: 330px">
                                <asp:Label ID="lblFecha" runat="server" Font-Bold="False" ForeColor="#003399"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1360px; height: 50px;">
                        <tr>
                            <td style="text-align: right; width: 150px;">
                                <asp:Label ID="lbl_Unidad" runat="server" Text="Unidad:"></asp:Label>
                            </td>
                            <td style="width: 300px;">
                                <asp:Label ID="lblUnidad" runat="server" ForeColor="#003399" Width="150px"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 110px;">
                                <asp:Label ID="lbl_Region" runat="server" Text="Región:"></asp:Label>
                            </td>
                            <td style="width: 360px;">
                                <asp:Label ID="lblRegion" runat="server" ForeColor="#003399"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 90px;">
                                <asp:Label ID="lbl_Transaccion" runat="server" Text="Transacción:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTransaccion" runat="server" Font-Bold="False" ForeColor="#003399"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Vehiculo" runat="server" Text="Vehículo:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblVehiculo" runat="server" ForeColor="#003399"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_CC" runat="server" Text="Centro de Costo:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCC" runat="server" ForeColor="#003399" Width="280px"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Estacion" runat="server" Text="Estación:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblEstacion" runat="server" ForeColor="#003399"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_IdConductor" runat="server" Text="ID Conductor:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtIdConductor" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="120px"></asp:TextBox>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_ConductorE" runat="server" Text="Conductor:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtConductorE" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="270px"></asp:TextBox>
                            </td>
                            <td style="text-align: right">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <table style="width: 1360px; height: 25px;">
                        <tr>
                            <td style="width: 150px; text-align: right">
                                <asp:Label ID="lbl_Litros" runat="server" Text="Litros:"></asp:Label>
                            </td>
                            <td style="width: 130px">
                                <ig:WebNumericEditor ID="wneLitros" runat="server" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" ReadOnly="True" Width="70px">
                                </ig:WebNumericEditor>
                            </td>
                            <td style="text-align: right; width: 120px;">
                                <asp:Label ID="lbl_Precio" runat="server" Text="Precio:"></asp:Label>
                            </td>
                            <td style="width: 130px;">
                                <ig:WebCurrencyEditor ID="wcePrecio" runat="server" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" ReadOnly="True" Width="80px">
                                </ig:WebCurrencyEditor>
                            </td>
                            <td style="width: 120px; text-align: right">
                                <asp:Label ID="lbl_Subtotal" runat="server" Text="Subtotal:"></asp:Label>
                            </td>
                            <td style="width: 130px;">
                                <ig:WebCurrencyEditor ID="wceSubtotal" runat="server" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" ReadOnly="True" Width="110px">
                                </ig:WebCurrencyEditor>
                            </td>
                            <td style="width: 120px; text-align: right">
                                <asp:Label ID="lbl_IVA" runat="server" Text="IVA:"></asp:Label>
                            </td>
                            <td style="width: 130px;">
                                <ig:WebCurrencyEditor ID="wceIVA" runat="server" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" ReadOnly="True" Width="80px">
                                </ig:WebCurrencyEditor>
                            </td>
                            <td style="width: 120px; text-align: right">
                                <asp:Label ID="lbl_Total" runat="server" Text="Total:"></asp:Label>
                            </td>
                            <td>
                                <ig:WebCurrencyEditor ID="wceTotal" runat="server" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" ReadOnly="True" Width="110px">
                                </ig:WebCurrencyEditor>
                            </td>
                        </tr>
                    </table>
                    <table style="width:1360px; height: 50px;">
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnActualizar" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnActualizar_Click" OnClientClick="this.disabled = true; this.value='Procesando…';" Text="Actualizar Carga" UseSubmitBehavior="false" Width="200px" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button ID="btnCancelar" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnCancelar_Click" OnClientClick="this.disabled = true; this.value='Procesando…';" Text="Cancelar Carga" UseSubmitBehavior="false" Width="200px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        </table>
</asp:Content>