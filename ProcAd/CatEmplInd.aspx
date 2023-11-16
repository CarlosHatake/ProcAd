<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="CatEmplInd.aspx.vb" Inherits="ProcAd.CatEmplInd" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        .auto-style29 {
            width: 963px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000">
                <asp:TextBox ID="_txtTipoMovI" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtTipoMov" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                    <asp:Panel ID="pnlGrid" runat="server">
                        <table style="width: 100%; text-align: center; margin-right: auto; margin-left: auto; height: 60px;">
                            <tr>
                                <td style="width: 180px">&nbsp;</td>
                                <td style="width: 60px">
                                    <asp:ImageButton ID="ibtnAlta" runat="server" ImageUrl="images\Add.png" ToolTip="Alta" Width="20px" />
                                </td>
                                <td style="width: 60px">
                                    <asp:ImageButton ID="ibtnBaja" runat="server" ImageUrl="images\Trash.png" ToolTip="Baja" Width="20px" />
                                </td>
                                <td style="width: 60px">
                                    <asp:ImageButton ID="ibtnModif" runat="server" ImageUrl="images\Edit.png" ToolTip="Modificar" Width="20px" />
                                </td>
                                <td>
                                    <ig:WebScriptManager ID="wsmCatEmplInd" runat="server">
                                    </ig:WebScriptManager>
                                </td>
                                <td style="width: 150px">
                                    <asp:TextBox ID="txtValor" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="140px"></asp:TextBox>
                                </td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="ddlCampo" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="110px">
                                        <asp:ListItem Value="dt_empleado.nombre">Empleado</asp:ListItem>
                                        <asp:ListItem Value="empresa">Empresa</asp:ListItem>
                                        <asp:ListItem Value="unidad_neg">Unidad de Negocio</asp:ListItem>
                                        <asp:ListItem Value="direccion">Dirección</asp:ListItem>
                                        <asp:ListItem Value="area">Área</asp:ListItem>
                                        <asp:ListItem Value="nombre_aut">Lider</asp:ListItem>
                                        <asp:ListItem Value="dt_empleado.no_empleado">No. Empleado</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 30px">
                                    <asp:ImageButton ID="ibtnBuscar" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="20px" />
                                </td>
                                <td style="width: 170px">&nbsp;</td>
                            </tr>
                        </table>
                        <table style="width:100%;">
                            <tr>
                                <td style="text-align: center; width: 20px;">&nbsp;</td>
                                <td class="auto-style29">
                                    <asp:GridView ID="gvEmpleado" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="1300px">
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>
                                            <asp:BoundField DataField="id_dt_empleado" HeaderText="id_dt_empleado">
                                            <ItemStyle Width="30px" />
                                            </asp:BoundField>
                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" SelectText="" ShowSelectButton="True">
                                            <ItemStyle Width="20px" />
                                            </asp:CommandField>
                                            <asp:BoundField DataField="empleado" HeaderText="Empleado">
                                            <ItemStyle Width="230px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="no_empleado" HeaderText="No. Empleado">
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="unidad_neg" HeaderText="Unidad de Negocio">
                                            <ItemStyle HorizontalAlign="Center" Width="170px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="direccion" HeaderText="Dirección">
                                            <ItemStyle Width="230px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="area" HeaderText="Área">
                                            <ItemStyle Width="220px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="evalua" HeaderText="Evalúa">
                                            <ItemStyle Width="170px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="valida" HeaderText="Valida">
                                            <ItemStyle Width="170px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="no_indicadores" HeaderText="# Indicadores">
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
                    <asp:Panel ID="pnlDatos" runat="server">
                        <table style="border: medium inset #808080; width: 850px; margin-right: auto; margin-left: auto;">
                            <tr>
                                <td class="auto-style8">
                                    <asp:Panel ID="pnlDetalle" runat="server">
                                        <table style="width: 100%; height: 20px;">
                                            <tr>
                                                <td style="width: 123px; text-align: right">
                                                    <asp:Label ID="lbl_TipoMov" runat="server" Text="Tipo de Movimiento:"></asp:Label>
                                                </td>
                                                <td style="text-align: left; ">
                                                    <asp:Label ID="lblTipoMov" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="width: 100%; height: 28px;">
                                            <tr>
                                                <td style="text-align: right; width: 123px;">
                                                    <asp:Label ID="lbl_Empresa" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Empresa:"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 264px;">
                                                    <asp:DropDownList ID="ddlEmpresa" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblFechaIngD" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" Visible="False"></asp:Label>
                                                </td>
                                                <td style="text-align: right; width: 123px;">
                                                    <asp:Label ID="lbl_UnidadN" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Unidad de Negocio:"></asp:Label>
                                                </td>
                                                <td style="text-align: left; ">
                                                    <asp:DropDownList ID="ddlUnidadN" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="220px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lbl_Direccion" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Dirección:"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlDirecccion" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: right; width: 123px;">
                                                    <asp:Label ID="lbl_Area" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Área:"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlArea" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="320px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lbl_Evalua" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Evalúa:"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:TextBox ID="txtEvalua" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxLength="340" Width="190px"></asp:TextBox>
                                                    &nbsp;
                                                    <asp:ImageButton ID="ibtnBuscarEval" runat="server" ImageUrl="images\Search.png" style="height: 15px" ToolTip="Buscar" Width="15px" />
                                                </td>
                                                <td style="text-align: right; width: 123px;">
                                                    <asp:Label ID="lbl_Valida" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Valida:"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:TextBox ID="txtValida" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxLength="340" Width="190px"></asp:TextBox>
                                                    &nbsp;
                                                    <asp:ImageButton ID="ibtnBuscarVal" runat="server" ImageUrl="images\Search.png" style="height: 15px" ToolTip="Buscar" Width="15px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">&nbsp;</td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlEvalua" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: right; width: 123px;">&nbsp;</td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlValida" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="height: 28px; width: 100%;">
                                            <tr>
                                                <td style="text-align: right; width: 123px;">
                                                    <asp:Label ID="lbl_Empleado" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Empleado:"></asp:Label>
                                                </td>
                                                <td style="text-align: left; ">
                                                    <table style="width:100%;">
                                                        <tr>
                                                            <td style="width: 120px">
                                                                <asp:TextBox ID="txtEmpleado" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxLength="340" Width="110px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 30px">
                                                                <asp:ImageButton ID="ibtnBuscarEmpl" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" style="height: 15px" />
                                                            </td>
                                                            <td style="width: 360px">
                                                                <asp:DropDownList ID="ddlEmpleado" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="350px" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 105px; text-align: right">
                                                                <asp:Label ID="lbl_FechaIng" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Fecha Ingreso:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblFechaIng" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; ">
                                                    &nbsp;</td>
                                                <td style="text-align: left; ">
                                                    <table style="width: 90%">
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="cbDirector" runat="server" Text="Director" />
                                                            </td>
                                                            <td style="width: 255px; text-align: left;">
                                                                <asp:CheckBox ID="cb1erND" runat="server" Text="1er Nivel Dirección" />
                                                            </td>
                                                            <td style="width: 40px">
                                                                &nbsp;</td>
                                                            <td style="width: 40px">
                                                                &nbsp;</td>
                                                            <td style="width: 40px">
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; ">
                                                    <asp:Label ID="lbl_EmpleadoUsr" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Usuario ProcAd:"></asp:Label>
                                                </td>
                                                <td style="text-align: left; ">
                                                    <table style="width: 90%">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblEmpleadoUsr" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"></asp:Label>
                                                                <asp:Label ID="lblEmpleadoID" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" Visible="False"></asp:Label>
                                                            </td>
                                                            <td style="width: 120px; text-align: right;">
                                                                <asp:Label ID="lbl_PondT" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Ponderación Total:"></asp:Label>
                                                            </td>
                                                            <td style="width: 140px">
                                                                <asp:Label ID="lblPondT" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td style="width: 40px">
                                                                <asp:ImageButton ID="ibtnAltaInd" runat="server" ImageUrl="images\Add.png" ToolTip="Agregar" Width="20px" />
                                                            </td>
                                                            <td style="width: 40px">
                                                                <asp:ImageButton ID="ibtnBajaInd" runat="server" ImageUrl="images\Trash.png" style="height: 20px" ToolTip="Eliminar" Width="20px" />
                                                            </td>
                                                            <td style="width: 40px">
                                                                <asp:ImageButton ID="ibtnModifInd" runat="server" ImageUrl="images\Edit.png" ToolTip="Modificar" Width="20px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="height: 28px; width: 100%;">
                                            <tr>
                                                <td style="text-align: right; width: 15px;">&nbsp;</td>
                                                <td style="text-align: center; ">
                                                    <asp:GridView ID="gvIndicadores" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="820px">
                                                        <Columns>
                                                            <asp:BoundField DataField="id_dt_empl_ind" HeaderText="id_dt_empl_ind" />
                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                            <ItemStyle Width="15px" />
                                                            </asp:CommandField>
                                                            <asp:BoundField DataField="indicador" HeaderText="Indicador">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="tipo_indicador" HeaderText="Tipo">
                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ponderacion" HeaderText="Pond." DataFormatString="{0:p}">
                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="meta" HeaderText="Meta" DataFormatString="{0:p}">
                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="fuente" HeaderText="Fuente">
                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="formula" HeaderText="Fórmula">
                                                            <ItemStyle HorizontalAlign="Left" Width="150px" />
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
                                    </asp:Panel>
                                    <asp:Panel ID="pnlIndicador" runat="server">
                                        <table style="width:100%;">
                                            <tr>
                                                <td style="width: 130px;">&nbsp;</td>
                                                <td>
                                                    <table style="border: thin solid #000000; width: 850px; height: 44px;">
                                                        <tr>
                                                            <td style="text-align: right; ">
                                                                <asp:Label ID="lbl_Indicador" runat="server" Text="Indicador:"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="txtIndicador" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="650px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right; width: 123px;">
                                                                <asp:Label ID="lbl_TipoInd" runat="server" Text="Tipo Indicador:"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left; width: 284px;">
                                                                <asp:DropDownList ID="ddlTipoInd" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="120px">
                                                                    <asp:ListItem>Ascendente</asp:ListItem>
                                                                    <asp:ListItem>Descendente</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="text-align: right; width: 123px;">
                                                                <asp:Label ID="lbl_Pond" runat="server" Text="Ponderación:"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left; width: 288px;">
                                                                <ig:WebPercentEditor ID="wpePond" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px" MaxValue="1" MinValue="0.01" Nullable="False">
                                                                </ig:WebPercentEditor>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right; ">
                                                                <asp:Label ID="lbl_Meta" runat="server" Text="Meta:"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <ig:WebPercentEditor ID="wpeMeta" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="70px" MaxValue="1" MinValue="0.01" Nullable="False">
                                                                </ig:WebPercentEditor>
                                                            </td>
                                                            <td style="text-align: right; ">
                                                                <asp:Label ID="lbl_Fuente" runat="server" Text="Fuente:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFuente" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="230px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right; ">
                                                                <asp:Label ID="lbl_Formula" runat="server" Text="Fórmula:"></asp:Label>
                                                            </td>
                                                            <td colspan="3" style="text-align: left; ">
                                                                <asp:TextBox ID="txtFormula" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="500px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="text-align: center; ">
                                                                <asp:Button ID="btnAceptarInd" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Aceptar" Width="90px" />
                                                            </td>
                                                            <td colspan="2" style="text-align: center;">
                                                                <asp:Button ID="btnCancelarInd" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Cancelar" Width="90px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <table style="width: 100%; height: 50px;">
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" OnClientClick="this.disabled = true;" Text="Aceptar" UseSubmitBehavior="false" Width="150px" Font-Names="Verdana" Font-Size="8pt" />
                                            </td>
                                            <td style="text-align: center">
                                                <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" OnClientClick="this.disabled = true;" Text="Cancelar" UseSubmitBehavior="false" Width="150px" Font-Names="Verdana" Font-Size="8pt" />
                                            </td>
                                        </tr>
                                    </table>
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
