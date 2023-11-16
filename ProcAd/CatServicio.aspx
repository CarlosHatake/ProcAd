<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="CatServicio.aspx.vb" Inherits="ProcAd.CatServicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        .auto-style29 {
            width: 963px;
        }
        .auto-style33 {
            width: 30px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000">
                <asp:TextBox ID="_txtTipoMovC" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtTipoMov" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtIdServicio" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtIdConfig" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
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
                                <td>&nbsp;</td>
                                <td style="width: 150px">
                                    <asp:TextBox ID="txtValor" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="140px"></asp:TextBox>
                                </td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="ddlCampo" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="110px">
                                        <asp:ListItem Value="servicio">Servicio</asp:ListItem>
                                        <asp:ListItem Value="tipo_servicio">Tipo</asp:ListItem>
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
                                <td style="text-align: center; width: 150px;">&nbsp;</td>
                                <td class="auto-style29">
                                    <asp:GridView ID="gvServicio" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="1030px">
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>
                                            <asp:BoundField DataField="id_servicio" HeaderText="id_servicio">
                                            <ItemStyle Width="30px" />
                                            </asp:BoundField>
                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" SelectText="" ShowSelectButton="True">
                                            <ItemStyle Width="20px" />
                                            </asp:CommandField>
                                            <asp:BoundField DataField="Servicio" HeaderText="Servicio">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" >
                                            <ItemStyle HorizontalAlign="Center" Width="160px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="admon_oper" HeaderText="Admon. / Operación">
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                            <ItemStyle HorizontalAlign="Center" Width="140px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="finanzas" HeaderText="Finanzas">
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="valida_presup" HeaderText="Validar Presupuesto">
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
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
                        <table style="border: medium inset #808080; width: 1300px; margin-right: auto; margin-left: auto;">
                            <tr>
                                <td class="auto-style8">
                                    <asp:Panel ID="pnlDetalle" runat="server">
                                        <asp:Panel ID="pnlGeneral" runat="server">
                                            <table style="width: 100%; height: 50px;">
                                                <tr>
                                                    <td style="text-align: right; width: 123px;">
                                                        <asp:Label ID="lbl_TipoMov" runat="server" Text="Tipo de Movimiento:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; width: 370px;">
                                                        <asp:Label ID="lblTipoMov" runat="server" Font-Bold="True"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; width: 100px;">&nbsp;</td>
                                                    <td style="text-align: left; width: 300px;">&nbsp;</td>
                                                    <td style="text-align: left; width: 230px;">&nbsp;</td>
                                                    <td style="text-align: left; ">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right">
                                                        <asp:Label ID="lbl_Servicio" runat="server" Text="Servicio:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:TextBox ID="txtServicio" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="16px" Width="343px"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: right">
                                                        <asp:Label ID="lbl_Tipo" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Tipo:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:DropDownList ID="ddlTipo" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="180px">
                                                            <asp:ListItem>Servicios Específicos</asp:ListItem>
                                                            <asp:ListItem>Servicios Únicos</asp:ListItem>
                                                            <asp:ListItem>Contrato</asp:ListItem>
                                                            <asp:ListItem>Servicios Negociados</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:CheckBox ID="cbReqCotizaciones" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Requiere Cotizaciones" />
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:Button ID="btnAceptarA" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Aceptar" Width="70px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlTablaConfig" runat="server">
                                            <table style="height: 28px;">
                                                <tr>
                                                    <td style="text-align: right; width: 123px; vertical-align: top;">
                                                        <asp:Label ID="lbl_Config" runat="server" Text="Configuraciones:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center; width: 1035px;">
                                                        <asp:GridView ID="gvConfiguracion" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1020px">
                                                            <Columns>
                                                                <asp:BoundField DataField="id_dt_servicio_conf" HeaderText="id" />
                                                                <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                <ItemStyle Width="15px" />
                                                                </asp:CommandField>
                                                                <asp:BoundField DataField="admon_oper" HeaderText="Admon. / Operación">
                                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="cuenta_cont" HeaderText="Cuenta Contable">
                                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="validador" HeaderText="Validador">
                                                                <ItemStyle HorizontalAlign="Center" Width="110px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="finanzas" HeaderText="Finanzas">
                                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="valida_presup" HeaderText="Validar Presupuesto">
                                                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="autorizador1" HeaderText="Autorizador 1">
                                                                <ItemStyle HorizontalAlign="Center" Width="110px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="autorizador2" HeaderText="Autorizador 2">
                                                                <ItemStyle HorizontalAlign="Center" Width="110px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="autorizador3" HeaderText="Autorizador 3">
                                                                <ItemStyle HorizontalAlign="Center" Width="110px" />
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
                                                    <td style="vertical-align: top; text-align: center; width: 30px;">
                                                        <asp:ImageButton ID="ibtnAltaConf" runat="server" ImageUrl="images\Add.png" ToolTip="Agregar" Width="20px" />
                                                    </td>
                                                    <td style="vertical-align: top; text-align: center; width: 30px;">
                                                        <asp:ImageButton ID="ibtnBajaConf" runat="server" ImageUrl="images\Trash.png" style="height: 20px" ToolTip="Eliminar" Width="20px" />
                                                    </td>
                                                    <td class="auto-style33" style="vertical-align: top; text-align: center; ">
                                                        <asp:ImageButton ID="ibtnModifConf" runat="server" ImageUrl="images\Edit.png" ToolTip="Modificar" Width="20px" />
                                                    </td>
                                                    <td style="vertical-align: top; text-align: left; ">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 123px; vertical-align: top;">
                                                        <asp:Label ID="lbl_Adjuntos" runat="server" Text="Adjuntos Requeridos:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; width: 1035px;">
                                                        <asp:TextBox ID="txtAdjunto" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="16px" Width="1015px"></asp:TextBox>
                                                    </td>
                                                    <td style="vertical-align: top; text-align: center; width: 30px;">
                                                        <asp:ImageButton ID="ibtnAltaAdj" runat="server" ImageUrl="images\Add.png" ToolTip="Agregar" Width="20px" />
                                                    </td>
                                                    <td style="vertical-align: top; text-align: center; width: 30px;">
                                                        <asp:ImageButton ID="ibtnBajaAdj" runat="server" ImageUrl="images\Trash.png" style="height: 20px" ToolTip="Eliminar" Width="20px" />
                                                    </td>
                                                    <td class="auto-style33" style="vertical-align: top; text-align: center; ">
                                                        <asp:ImageButton ID="ibtnModifAdj" runat="server" ImageUrl="images\Edit.png" ToolTip="Modificar" Width="20px" />
                                                    </td>
                                                    <td style="vertical-align: top; text-align: left; ">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 123px; vertical-align: top;">&nbsp;</td>
                                                    <td style="text-align: center; width: 1035px;">
                                                        <asp:GridView ID="gvAdjunto" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1020px">
                                                            <Columns>
                                                                <asp:BoundField DataField="id_dt_servicio_adj" HeaderText="id" />
                                                                <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                <ItemStyle Width="15px" />
                                                                </asp:CommandField>
                                                                <asp:BoundField DataField="adjunto" HeaderText="Adjunto">
                                                                <ItemStyle HorizontalAlign="Left" />
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
                                                    <td style="vertical-align: top; text-align: center; width: 30px;">&nbsp;</td>
                                                    <td style="vertical-align: top; text-align: center; width: 30px;">&nbsp;</td>
                                                    <td class="auto-style33" style="vertical-align: top; text-align: center; ">&nbsp;</td>
                                                    <td style="vertical-align: top; text-align: left; ">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlConfig" runat="server">
                                        <table style="width: 100%; height: 25px;">
                                            <tr>
                                                <td style="text-align: right; width: 123px;">
                                                    <asp:Label ID="lbl_AdmonOper" runat="server" Text="Admon. / Operación:"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 310px;">
                                                    <asp:RadioButtonList ID="rblAdmonOper" runat="server" RepeatColumns="3" Width="280px">
                                                        <asp:ListItem Value="Admon">Administrativo</asp:ListItem>
                                                        <asp:ListItem Value="Oper">Operativo</asp:ListItem>
                                                        <asp:ListItem>Ambos</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td style="text-align: right; width: 150px;">
                                                    <asp:Label ID="lbl_Empresa" runat="server" Text="Empresa:"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 190px;">
                                                    <asp:DropDownList ID="ddlEmpresa" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="140px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: right; width: 180px;">
                                                    <asp:Label ID="lbl_CuentaCont" runat="server" Text="Cuenta Contable:"></asp:Label>
                                                </td>
                                                <td style="text-align: left; ">
                                                    <asp:TextBox ID="txtCuentaCont" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="16px" Width="70px">0000-0000</asp:TextBox>
                                                </td>
                                                <td style="text-align: left; ">
                                                    <asp:Button ID="btnAceptarC" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Aceptar" Width="70px" />
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Panel ID="pnlConfigVA" runat="server">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="text-align: right; width: 123px;">
                                                        <asp:Label ID="lbl_Validador1" runat="server" Text="Validador:"></asp:Label>
                                                    </td>
                                                    <td style="width: 300px">
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td style="vertical-align: top">
                                                                    <asp:DropDownList ID="ddlValidador1T" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="160px">
                                                                        <asp:ListItem Value="1">Usuario Específico</asp:ListItem>
                                                                        <asp:ListItem Value="0">Lista Pre-definida</asp:ListItem>
                                                                        <asp:ListItem Value="-1">x Organigrama</asp:ListItem>
                                                                        <asp:ListItem Value="-2">Gerente Organigrama</asp:ListItem>
                                                                        <asp:ListItem Value="-3">Director Organigrama</asp:ListItem>
                                                                        <asp:ListItem Value="-4">Gerente División/CC</asp:ListItem>
                                                                        <asp:ListItem Value="-5">Director División/CC</asp:ListItem>
                                                                        <asp:ListItem Value="-6">Gerente Empresa</asp:ListItem>
                                                                        <asp:ListItem Value="-7">Director Empresa</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: left; width: 125px;">&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="text-align: right; width: 123px;">&nbsp;</td>
                                                    <td style="width: 300px">
                                                        <asp:CheckBox ID="cbFinanzas" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Requiere Autorización de Finanzas" />
                                                    </td>
                                                    <td style="text-align: right; width: 123px;">
                                                        <asp:Label ID="lbl_Validador2" runat="server" Text="Validador 2:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td style="vertical-align: top">
                                                                    <asp:DropDownList ID="ddlValidador2T" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="160px">
                                                                        <asp:ListItem Value="1">Usuario Específico</asp:ListItem>
                                                                        <asp:ListItem Value="0">Lista Pre-definida</asp:ListItem>
                                                                        <asp:ListItem Value="-1">x Organigrama</asp:ListItem>
                                                                        <asp:ListItem Value="-2">Gerente Organigrama</asp:ListItem>
                                                                        <asp:ListItem Value="-3">Director Organigrama</asp:ListItem>
                                                                        <asp:ListItem Value="-4">Gerente División/CC</asp:ListItem>
                                                                        <asp:ListItem Value="-5">Director División/CC</asp:ListItem>
                                                                        <asp:ListItem Value="-6">Gerente Empresa</asp:ListItem>
                                                                        <asp:ListItem Value="-7">Director Empresa</asp:ListItem>
                                                                        <asp:ListItem Value="-98">No Requiere</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: left; width: 125px;">&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td style="vertical-align: top">
                                                                    <asp:TextBox ID="txtUsuarioVal1B" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="140px"></asp:TextBox>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: left; width: 125px;">
                                                                    <asp:Button ID="cmdBuscarUsrVal1" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Buscar" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:CheckBox ID="cbValidaPrep" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Requiere Validación de Presupuesto" />
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td style="vertical-align: top">
                                                                    <asp:TextBox ID="txtUsuarioVal2B" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="140px"></asp:TextBox>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: left; width: 125px;">
                                                                    <asp:Button ID="cmdBuscarUsrVal2" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Buscar" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td style="vertical-align: top">
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td style="vertical-align: top">
                                                                    <asp:DropDownList ID="ddlUsuarioVal1" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: center; width: 30px;">
                                                                    <asp:ImageButton ID="ibtnAltaVal1" runat="server" ImageUrl="images\Add.png" ToolTip="Agregar" Width="20px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: top">
                                                                    <asp:GridView ID="gvValidador1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="260px">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="id_usuario" HeaderText="id" />
                                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                            <ItemStyle Width="15px" />
                                                                            </asp:CommandField>
                                                                            <asp:BoundField DataField="usuario" HeaderText="Usuario">
                                                                            <ItemStyle HorizontalAlign="Left" />
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
                                                                <td style="vertical-align: top; text-align: center; width: 30px;">
                                                                    <asp:ImageButton ID="ibtnBajaVal1" runat="server" ImageUrl="images\Trash.png" style="height: 20px" ToolTip="Eliminar" Width="20px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td style="vertical-align: top">
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td style="vertical-align: top">
                                                                    <asp:DropDownList ID="ddlUsuarioVal2" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: center; width: 30px;">
                                                                    <asp:ImageButton ID="ibtnAltaVal2" runat="server" ImageUrl="images\Add.png" ToolTip="Agregar" Width="20px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: top">
                                                                    <asp:GridView ID="gvValidador2" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="260px">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="id_usuario" HeaderText="id" />
                                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                            <ItemStyle Width="15px" />
                                                                            </asp:CommandField>
                                                                            <asp:BoundField DataField="usuario" HeaderText="Usuario">
                                                                            <ItemStyle HorizontalAlign="Left" />
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
                                                                <td style="vertical-align: top; text-align: center; width: 30px;">
                                                                    <asp:ImageButton ID="ibtnBajaVal2" runat="server" ImageUrl="images\Trash.png" style="height: 20px" ToolTip="Eliminar" Width="20px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="lbl_Autorizador1" runat="server" Text="Autorizador 1:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td style="vertical-align: top">
                                                                    <asp:DropDownList ID="ddlAutorizador1T" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="160px">
                                                                        <asp:ListItem Value="1">Usuario Específico</asp:ListItem>
                                                                        <asp:ListItem Value="0">Lista Pre-definida</asp:ListItem>
                                                                        <asp:ListItem Value="-1">x Organigrama</asp:ListItem>
                                                                        <asp:ListItem Value="-2">Gerente Organigrama</asp:ListItem>
                                                                        <asp:ListItem Value="-3">Director Organigrama</asp:ListItem>
                                                                        <asp:ListItem Value="-4">Gerente División/CC</asp:ListItem>
                                                                        <asp:ListItem Value="-5">Director División/CC</asp:ListItem>
                                                                        <asp:ListItem Value="-6">Gerente Empresa</asp:ListItem>
                                                                        <asp:ListItem Value="-7">Director Empresa</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: left; width: 125px;">&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="lbl_Autorizador2" runat="server" Text="Autorizador 2:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td style="vertical-align: top">
                                                                    <asp:DropDownList ID="ddlAutorizador2T" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="160px">
                                                                        <asp:ListItem Value="1">Usuario Específico</asp:ListItem>
                                                                        <asp:ListItem Value="0">Lista Pre-definida</asp:ListItem>
                                                                        <asp:ListItem Value="-1">x Organigrama</asp:ListItem>
                                                                        <asp:ListItem Value="-2">Gerente Organigrama</asp:ListItem>
                                                                        <asp:ListItem Value="-3">Director Organigrama</asp:ListItem>
                                                                        <asp:ListItem Value="-4">Gerente División/CC</asp:ListItem>
                                                                        <asp:ListItem Value="-5">Director División/CC</asp:ListItem>
                                                                        <asp:ListItem Value="-6">Gerente Empresa</asp:ListItem>
                                                                        <asp:ListItem Value="-7">Director Empresa</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: left; width: 125px;">&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="lbl_Autorizador3" runat="server" Text="Autorizador 3:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td style="vertical-align: top">
                                                                    <asp:DropDownList ID="ddlAutorizador3T" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="160px">
                                                                        <asp:ListItem Value="1">Usuario Específico</asp:ListItem>
                                                                        <asp:ListItem Value="0">Lista Pre-definida</asp:ListItem>
                                                                        <asp:ListItem Value="-1">x Organigrama</asp:ListItem>
                                                                        <asp:ListItem Value="-2">Gerente Organigrama</asp:ListItem>
                                                                        <asp:ListItem Value="-3">Director Organigrama</asp:ListItem>
                                                                        <asp:ListItem Value="-4">Gerente División/CC</asp:ListItem>
                                                                        <asp:ListItem Value="-5">Director División/CC</asp:ListItem>
                                                                        <asp:ListItem Value="-6">Gerente Empresa</asp:ListItem>
                                                                        <asp:ListItem Value="-7">Director Empresa</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: left; width: 125px;">&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td style="vertical-align: top">
                                                                    <asp:TextBox ID="txtUsuarioAut1B" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="140px"></asp:TextBox>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: left; width: 125px;">
                                                                    <asp:Button ID="cmdBuscarUsrAut1" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Buscar" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td style="vertical-align: top">
                                                                    <asp:TextBox ID="txtUsuarioAut2B" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="140px"></asp:TextBox>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: left; width: 125px;">
                                                                    <asp:Button ID="cmdBuscarUsrAut2" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Buscar" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td style="vertical-align: top">
                                                                    <asp:TextBox ID="txtUsuarioAut3B" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="140px"></asp:TextBox>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: left; width: 125px;">
                                                                    <asp:Button ID="cmdBuscarUsrAut3" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Buscar" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td style="vertical-align: top">
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td style="vertical-align: top">
                                                                    <asp:DropDownList ID="ddlUsuarioAut1" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: center; width: 30px;">
                                                                    <asp:ImageButton ID="ibtnAltaAut1" runat="server" ImageUrl="images\Add.png" ToolTip="Agregar" Width="20px" style="height: 20px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: top">
                                                                    <asp:GridView ID="gvAutorizador1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="260px">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="id_usuario" HeaderText="id" />
                                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                            <ItemStyle Width="15px" />
                                                                            </asp:CommandField>
                                                                            <asp:BoundField DataField="usuario" HeaderText="Usuario">
                                                                            <ItemStyle HorizontalAlign="Left" />
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
                                                                <td style="vertical-align: top; text-align: center; width: 30px;">
                                                                    <asp:ImageButton ID="ibtnBajaAut1" runat="server" ImageUrl="images\Trash.png" style="height: 20px" ToolTip="Eliminar" Width="20px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td style="vertical-align: top">
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td style="vertical-align: top">
                                                                    <asp:DropDownList ID="ddlUsuarioAut2" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: center; width: 30px;">
                                                                    <asp:ImageButton ID="ibtnAltaAut2" runat="server" ImageUrl="images\Add.png" ToolTip="Agregar" Width="20px" style="height: 20px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: top">
                                                                    <asp:GridView ID="gvAutorizador2" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="260px">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="id_usuario" HeaderText="id" />
                                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                            <ItemStyle Width="15px" />
                                                                            </asp:CommandField>
                                                                            <asp:BoundField DataField="usuario" HeaderText="Usuario">
                                                                            <ItemStyle HorizontalAlign="Left" />
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
                                                                <td style="vertical-align: top; text-align: center; width: 30px;">
                                                                    <asp:ImageButton ID="ibtnBajaAut2" runat="server" ImageUrl="images\Trash.png" style="height: 20px" ToolTip="Eliminar" Width="20px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td style="vertical-align: top">
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td style="vertical-align: top">
                                                                    <asp:DropDownList ID="ddlUsuarioAut3" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: center; width: 30px;">
                                                                    <asp:ImageButton ID="ibtnAltaAut3" runat="server" ImageUrl="images\Add.png" ToolTip="Agregar" Width="20px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: top">
                                                                    <asp:GridView ID="gvAutorizador3" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="260px">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="id_usuario" HeaderText="id" />
                                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                                            <ItemStyle Width="15px" />
                                                                            </asp:CommandField>
                                                                            <asp:BoundField DataField="usuario" HeaderText="Usuario">
                                                                            <ItemStyle HorizontalAlign="Left" />
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
                                                                <td style="vertical-align: top; text-align: center; width: 30px;">
                                                                    <asp:ImageButton ID="ibtnBajaAut3" runat="server" ImageUrl="images\Trash.png" style="height: 20px" ToolTip="Eliminar" Width="20px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <table style="width: 100%; height: 50px;">
                                            <tr>
                                                <td style="text-align: center; width: 150px;">&nbsp;</td>
                                                <td style="text-align: center">
                                                    <asp:Button ID="btnAceptarConf" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Aceptar" Width="100px" />
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Button ID="btnCancelarConf" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Cancelar" Width="100px" />
                                                </td>
                                                <td style="text-align: center; width: 150px;">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <table style="width: 100%; height: 50px;">
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" Width="150px" Font-Names="Verdana" Font-Size="8pt" />
                                            </td>
                                            <td style="text-align: center">
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
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
