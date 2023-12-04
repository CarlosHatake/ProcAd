<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ModCargasCombustible.aspx.vb" Inherits="ProcAd.ModCargasCombustible" %>
<%@ Register Assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <ig:WebScriptManager ID="wsmCargasComb" runat="server"></ig:WebScriptManager>
    <table style="width: 1360px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; font-family: Verdana; font-size: 8pt; color: #FF0000" class="auto-style6">
                <asp:TextBox ID="_txtTipoMov" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="auto-style6">
                <asp:Panel ID="pnlInicio" runat="server">
                    <table style="width: 100%; text-align: center; margin-right: auto; margin-left: auto; height: 60px;">
                        <tr>
                            <td style="width: 180px">&nbsp;</td>

                            <td>&nbsp;</td>
                            <td style="width: 150px">&nbsp;</td>
                        </tr>
                    </table>


                    <asp:Panel runat="server" ID="pnlFiltrosBusqueda">
                        <table>
                            <tr>
                                <td style="width: 260px"></td>
                                <td>
                                    <asp:CheckBox ID="cbEmpleado" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="100px" Text="Empleado:" />
                                </td>
                                <td style="width:230px">
                                    <asp:Panel runat="server" ID="pnlEmpleadoB">
                                        <asp:TextBox runat="server" ID="txtEmpleado" Width="170px" Font-Names="Verdana" Font-Size="8pt"></asp:TextBox>
                                        <asp:ImageButton ID="ibtnBuscarEmpleado" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="17px" />
                                    </asp:Panel>
                                </td>
                                <td style="width: 130px"></td>
                                <td>
                                    <asp:CheckBox ID="cbUnidad" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Unidad:" />
                                </td>
                                <td>
                                    <asp:Panel runat="server" ID="pnlUnidadB">
                                        <asp:TextBox runat="server" ID="txtUnidad" Width="170px" Font-Names="Verdana" Font-Size="8pt"></asp:TextBox>
                                        <asp:ImageButton ID="ibtnBuscarUnidad" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="17px" />
                                    </asp:Panel>
                                </td>
                                <td style="width: 250px"></td>

                            </tr>

                            <tr>
                                <td style="width: 260px"></td>
                                <td></td>
                                <td style="width:230px">
                                    <asp:Panel runat="server" ID="pnlEmpleadoD">
                                        <asp:DropDownList ID="ddlEmpleado" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="200px"></asp:DropDownList>
                                    </asp:Panel>
                                </td>
                                <td style="width: 130px"></td>
                                <td></td>
                                <td>
                                    <asp:Panel runat="server" ID="pnlUnidadD">
                                        <asp:DropDownList ID="ddlUnidad" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="200px"></asp:DropDownList>
                                    </asp:Panel>
                                </td>
                                <td style="width: 250px"></td>

                            </tr>
                            <tr>
                                <td style="width:260px"></td>
                            </tr>
                            <tr>
                                <td style="width:100px"></td>
                                <td>
                                    <asp:CheckBox ID="cbPeriodo" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Periodo inicial:" />
                                </td>
                                <td style="width:250px">
                                    <asp:Panel ID="pnlPeriodo" runat="server">
                                        <table style="width: 220px;">
                                            <tr>
                                                <td style="border: medium inset #808080; margin-right: auto; margin-left: auto; width:110px;">
                                                    <table style="width: 110px;">
                                                        <tr>
                                                            <td style="text-align: center">
                                                                <asp:Label ID="lbl_FechaAsigI" runat="server" Text="Fecha Inicial"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: center;">
                                                                <ig:WebDatePicker ID="wdpFechaI" runat="server" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="100px">
                                                                </ig:WebDatePicker>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width:10px"></td>
                                                <td style="border: medium inset #808080; margin-right: auto; margin-left: auto; width:110px">
                                                    <table style="width: 110px;">
                                                        <tr>
                                                            <td style="text-align: center" class="auto-style67">
                                                                <asp:Label ID="lbl_FechaAsigF" runat="server" Text="Fecha Final"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: center">
                                                                <ig:WebDatePicker ID="wdpFechaF" runat="server" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="100px">
                                                                </ig:WebDatePicker>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>

                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td style="width: 130px"></td>
                                <td style="width:100px">
                                    <asp:CheckBox ID="cbFecha" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Fecha carga:" />
                                </td>
                                <td style="width:110px">
                                    <asp:Panel runat="server" ID="pnlFecha">
                                        <table>
                                            <tr>
                                                <td style="border: medium inset #808080; margin-right: auto; margin-left: auto; width:110px">
                                                    <table style="width: 110px;">
                                                        <tr>
                                                            <td style="text-align: center">
                                                                <asp:Label ID="Label1" runat="server" Text="Fecha"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: center">
                                                                <ig:WebDatePicker ID="wdpFecha" runat="server" Font-Names="Verdana" Font-Size="8pt" Nullable="False" Width="110px">
                                                                </ig:WebDatePicker>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td style="width: 250px"></td>

                            </tr>

                        </table>



                        <%-- BOTON BUSCAR --%>
                        <table>
                            <tr>
                                <td>&nbsp; </td>
                            </tr>
                            <tr>
                                <td style="width: 570px"></td>
                                <td>
                                    <asp:Button runat="server" Text="Buscar" Font-Names="Verdana" Font-Size="8pt" Width="130px" ID="btnBuscar" />
                                </td>
                                <td style="width: 590px"></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </table>

                    </asp:Panel>

                    <asp:Panel runat="server" ID="pnlActualizaciones">
                        <table >
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td style="width: 100px"></td>
                                            <td>
                                                <asp:Panel runat="server" ID="pnlAmpliarPeriodo">
                                                    <table style="border: medium inset #808080; width: 500px; margin-right: auto; margin-left: auto;">
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 100px"></td>
                                                                        <td style="width: 300px; align-content: center">
                                                                            <asp:Label runat="server" Text="Ampliar periodo de solicitud de combustible" Font-Bold="true" Font-Size="8pt" Font-Names="Verdana" Width="300px"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 100px"></td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 15px"></td>
                                                                        <td style="width: 115px">
                                                                            <asp:Label runat="server" Text="Número de unidad:"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lbl_Unidad" Font-Bold="true" ForeColor="Blue"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 10px"></td>
                                                                        <td>
                                                                            <asp:Label runat="server" Text="Estatus:"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lbl_Status" Font-Bold="true" ForeColor="Blue"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 15px"></td>
                                                                        <td style="width: 115px">
                                                                            <asp:Label runat="server" Text="Periodo inicio actual:" Width="115px"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lbl_PeriodoInic" Font-Bold="true" ForeColor="Blue"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 20px"></td>

                                                                        <td>
                                                                            <asp:Label runat="server" Text="Periodo fin actual:" Width="100px"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lbl_PeriodoFin" Font-Bold="true" ForeColor="Blue"></asp:Label>
                                                                        </td>

                                                                    </tr>
                                                                    <tr> 
                                                                        <td style="width:15px"></td>
                                                                        <td style="width: 20px">
                                                                            <asp:Label runat="server" Text="Nuevo periodo inicio:"></asp:Label>
                                                                        </td>
                                                                         <td>
                                                                            <ig:WebDatePicker ID="wdpNuevoPeriodoIni" runat="server" Font-Names="Verdana" Font-Size="8pt" Nullable="False">
                                                                            </ig:WebDatePicker>
                                                                        </td>
                                                                        <td style="width: 20px"></td>
                                                                        <td style="width: 20px">
                                                                            <asp:Label runat="server" Text="Nuevo periodo fin:"></asp:Label>
                                                                        </td>
                                                                         <td>
                                                                            <ig:WebDatePicker ID="wdpNuevoPeriodoFin" runat="server" Font-Names="Verdana" Font-Size="8pt" Nullable="False">
                                                                            </ig:WebDatePicker>
                                                                        </td>
                                                                    </tr>

                                                                </table>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 100px"></td>
                                                                        <td style="width: 125px">
                                                                            <asp:Button runat="server" Text="Aceptar" ID="btnAmpliarPeriodo" Width="125px" Font-Names="Verdana" Font-Size="8pt" />
                                                                        </td>
                                                                        <td style="width: 30px"></td>
                                                                        <td style="width: 125px">
                                                                            <asp:Button runat="server" Text="Cancelar" ID="btnCancelarAmpliarPeriodo" Width="125px" Font-Names="Verdana" Font-Size="8pt" />
                                                                        </td>
                                                                        <td style="width: 100px"></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>

                                            </td>
                                            <td style="width: 50px"></td>
                                            <td>
                                                <asp:Panel runat="server" ID="pnlDescomplementar">
                                                    <table style="border: medium inset #808080; width: 550px; margin-right: auto; margin-left: auto;">
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 90px"></td>
                                                                        <td style="width: 340px; align-content: center">
                                                                            <asp:Label runat="server" Text="Complementar hora / Descomplementar carga" Font-Bold="true" Font-Size="8pt" Font-Names="Verdana" Width="320px"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 90px"></td>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 30px"></td>
                                                                        <td style="width: 115px">
                                                                            <asp:Label runat="server" Text="Número de unidad:"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lbl_UnidadDt" Font-Bold="true" ForeColor="Blue"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 10px"></td>
                                                                        <td>
                                                                            <asp:Label runat="server" Text="Estatus:"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lbl_StatusDt" ont-Bold="true" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 30px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 30px"></td>
                                                                        <td style="width: 115px">
                                                                            <asp:Label runat="server" Text="Fecha actual:" Width="115px"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lbl_FechaActual" Font-Bold="true" ForeColor="Blue"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 20px"></td>

                                                                        <td> 
                                                                            <asp:Label runat="server" Text="Nueva hora:"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                             <asp:TextBox type="time" ID="txtFechaN" runat="server" Width="100px" Height="15px" Font-Names="Verdana" Font-Size="8pt"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 30px"></td>
                                                                    </tr>

                                                                </table>

                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 30px"></td>
                                                                        <td style="width: 125px">
                                                                            <asp:Button runat="server" Text="Complementar hora" ID="btnComplementarHora" Width="125px" Font-Names="Verdana" Font-Size="8pt" />
                                                                        </td>
                                                                        <td style="width: 30px"></td>
                                                                        <td style="width: 120px">
                                                                            <asp:Button runat="server" Text="Descomplementar carga" ID="btnDescomplementar" Width="150px" Font-Names="Verdana" Font-Size="8pt" />
                                                                        </td>
                                                                        <td style="width: 30px"></td>
                                                                        <td style="width: 125px">
                                                                            <asp:Button runat="server" Text="Cancelar" ID="btnCancelarDtCarga" Width="125px" Font-Names="Verdana" Font-Size="8pt" />
                                                                        </td>
                                                                        <td style="width: 30px"></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px"></td>
                                            <td style="vertical-align:top" >
                                                <asp:GridView ID="gvMsCargasCombustible" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="500px" DataKeyNames="id_ms_comb">
                                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" SelectText="" ShowSelectButton="True">
                                                            <ItemStyle Width="20px" />
                                                        </asp:CommandField>
                                                        <asp:BoundField DataField="no_eco" HeaderText="Unidad">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="empleado" HeaderText="Empleado">
                                                            <ItemStyle HorizontalAlign="Center" Width=" 200px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="periodo_ini" DataFormatString="{0:d}" HeaderText="Periodo inicio">
                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="periodo_fin" DataFormatString="{0:d}" HeaderText="Periodo fin">
                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="status" HeaderText="Status">
                                                            <ItemStyle HorizontalAlign="Center" />
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
                                            <td style="width: 50px"></td>
                                             <td style="vertical-align:top" >
                                                <asp:GridView ID="gvDtCargaCombustible" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="500px" DataKeyNames="id_dt_carga_comb_toka">
                                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" SelectText="" ShowSelectButton="True">
                                                            <ItemStyle Width="20px" />
                                                        </asp:CommandField>
                                                        <asp:BoundField DataField="identificador_vehiculo" HeaderText="Unidad">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="razon_social_afiliado" HeaderText="Razon social">
                                                            <ItemStyle HorizontalAlign="Center" Width=" 200px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="cantidad_mercancia" HeaderText="Cantidad mercancia">
                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="importe_transaccion" HeaderText="Importe transacción">
                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="km_ant_transaccion" HeaderText="Km Ant transacción">
                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="km_transaccion" HeaderText="Km Transacción">
                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="recorrido" HeaderText="Recorrido">
                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="fecha" HeaderText="Fecha">
                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="status" HeaderText="Status">
                                                            <ItemStyle HorizontalAlign="Center" />
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
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>