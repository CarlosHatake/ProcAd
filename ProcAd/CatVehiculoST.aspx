<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="CatVehiculoST.aspx.vb" Inherits="ProcAd.CatVehiculoST" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style6 {
            width: 1350px;
        }
        .auto-style9 {
            width: 1350px;
            height: 31px;
        }
        .auto-style47 {
            width: 122px;
        }
                    
        .auto-style29 {
            width: 963px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1360px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; font-family: Verdana; font-size: 8pt; color: #FF0000" class="auto-style6">
                <ig:WebScriptManager ID="wsmCatEmpleado" runat="server">
                </ig:WebScriptManager>
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
                            <td style="width: 190px">&nbsp;</td>
                            <td style="width: 60px">
                                <asp:ImageButton ID="ibtnModif" runat="server" ImageUrl="images\Edit.png" ToolTip="Modificar" Width="20px" />
                            </td>
                            <td style="width: 60px">
                                &nbsp;</td>
                            <td style="width: 60px">
                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td style="width: 150px">
                                <asp:TextBox ID="txtValor" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="140px"></asp:TextBox>
                            </td>
                            <td style="width: 100px">
                                <asp:DropDownList ID="ddlCampo" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="110px">
                                    <asp:ListItem Value="no_eco">No. Económico</asp:ListItem>
                                    <asp:ListItem Value="placas">Placas</asp:ListItem>
                                    <asp:ListItem Value="cg_empleado.nombre + ' ' + ap_paterno + ' ' + ap_materno">Asignado a</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 30px">
                                <asp:ImageButton ID="ibtnBuscar" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="17px" />
                            </td>
                            <td style="width: 195px">&nbsp;</td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: center; width: 160px;">&nbsp;</td>
                            <td class="auto-style29">
                                <asp:GridView ID="gvVehiculo" runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="20" Width="950px">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <Columns>
                                        <asp:BoundField DataField="id_ms_vehiculo" HeaderText="id_ms_vehiculo">
                                        <ItemStyle Width="30px" />
                                        </asp:BoundField>
                                        <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" SelectText="" ShowSelectButton="True">
                                        <ItemStyle Width="20px" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="no_eco" HeaderText="No. Económico">
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_unidad" HeaderText="Tipo Unidad">
                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="marca" HeaderText="Marca" >
                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="modelo" HeaderText="Modelo">
                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="año" HeaderText="Año">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="placas" HeaderText="Placas">
                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="asignado_a" HeaderText="Asignado a" />
                                        <asp:BoundField DataField="rendimiento" DataFormatString="{0:n2}" HeaderText="Rendimiento">
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tolerancia" DataFormatString="{0:p}" HeaderText="% Tolerancia">
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
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
            </td>
        </tr>
        <tr>
            <td class="auto-style9" style="text-align: center; margin-right: auto; margin-left: auto">
                <asp:Panel ID="pnlDatos" runat="server">
                    <table style="border: medium inset #808080; width: 1320px; margin-right: auto; margin-left: auto;">
                        <tr>
                            <td>
                                <table style="width:100%; height: 90px;">
                                    <tr>
                                        <td class="auto-style47" style="text-align: right; height: 23px;">
                                            <asp:Label ID="lbl_TipoMov" runat="server" Text="Tipo de Movimiento:"></asp:Label>
                                        </td>
                                        <td style="text-align: left; width: 160px;">
                                            <asp:Label ID="lblTipoMov" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td style="text-align: right; width: 90px;">
                                            &nbsp;</td>
                                        <td style="text-align: left; width: 160px;">
                                            &nbsp;</td>
                                        <td style="text-align: right; width: 80px;">
                                            &nbsp;</td>
                                        <td style="text-align: left; width: 150px;">
                                            &nbsp;</td>
                                        <td style="text-align: right; width: 90px;">&nbsp;</td>
                                        <td style="text-align: left; width: 160px;">&nbsp;</td>
                                        <td style="text-align: right; width: 100px;">
                                            <asp:Label ID="lbl_StatusU" runat="server" Text="Estatus Unidad:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblStatusU" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style47" style="text-align: right; height: 23px;">
                                            <asp:Label ID="lbl_NoEconomico" runat="server" Text="No. Económico:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblNoEconomico" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lbl_Marca" runat="server" Text="Marca:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblMarca" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lbl_Modelo" runat="server" Text="Modelo:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblModelo" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lbl_TipoUnidad" runat="server" Text="Tipo Unidad:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblTipoUnidad" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lbl_KmsAct" runat="server" Text="KM Actual:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblKmsAct" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style47" style="text-align: right; height: 23px;">
                                            <asp:Label ID="lbl_Serie" runat="server" Text="Serie:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblSerie" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lbl_Motor" runat="server" Text="Motor:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblMotor" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lbl_Año" runat="server" Text="Año:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblAño" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lbl_Ubicacion" runat="server" Text="Ubicación:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblUbicacion" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lbl_UltRendTab" runat="server" Text="Últ. Rend. Tab.:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblUltRendTab" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; height: 23px;">
                                            <asp:Label ID="lbl_Placas" runat="server" Text="Placas:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblPlacas" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lbl_ManttoUlt" runat="server" Text="Ult. Mantto.:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblManttoUlt" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lbl_ManttoProx" runat="server" Text="Prox. Mantto.:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblManttoProx" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lbl_Rendimiento" runat="server" Text="Rendimiento:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <ig:WebNumericEditor ID="wneRendimiento" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="90px">
                                            </ig:WebNumericEditor>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lbl_Tolerancia" runat="server" Text="Tolerancia:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <ig:WebPercentEditor ID="wpeTolerancia" runat="server" Font-Names="Verdana" Font-Size="8pt" MinValue="0.01" Nullable="False" Width="60px">
                                            </ig:WebPercentEditor>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width:100%; height: 80px;">
                                    <tr>
                                        <td class="auto-style47" style="text-align: right">
                                            <asp:Label ID="lbl_Obs" runat="server" Text="Observaciones:"></asp:Label>
                                        </td>
                                        <td style="text-align: left; width: 630px;">
                                            <asp:TextBox ID="txtObs" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="60px" TextMode="MultiLine" Width="600px" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left; vertical-align: top;">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="text-align: right; width: 115px; height: 23px;">
                                                        <asp:Label ID="lbl_AsignadoA" runat="server" Text="Asignado a:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAsignadoA" runat="server" Font-Bold="True" ForeColor="#003399" Width="330px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; height: 23px;">
                                                        <asp:Label ID="lbl_CC" runat="server" Text="Centro de Costo:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCC" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%; height: 50px;">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" Width="150px" style="height: 26px" Font-Names="Verdana" Font-Size="8pt" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Width="150px" Font-Names="Verdana" Font-Size="8pt" />
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
