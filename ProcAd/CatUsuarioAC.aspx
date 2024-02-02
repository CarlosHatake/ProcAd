<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="CatUsuarioAC.aspx.vb" Inherits="ProcAd.CatUsuarioAC" %>
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
                <asp:TextBox ID="_txtTipoMovA" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtTipoMov" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                    <asp:Panel ID="pnlGrid" runat="server">
                        <table style="width:100%;">
                            <tr>
                                <td>&nbsp;</td>
                                <td colspan="8" style="text-align: center">
                                    <asp:Label ID="lblConfigGlob" runat="server" Font-Bold="True">Configuraciones Globales</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25px">&nbsp;</td>
                                <td style="width: 110px">
                                    <asp:CheckBox ID="cbTaxiTabG" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Taxi Tabulador" />
                                </td>
                                <td style="width: 160px">
                                    <asp:Button ID="btnTaxiTabG" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Aplicar a Todos" Width="110px" />
                                </td>
                                <td style="width: 145px">
                                    <asp:CheckBox ID="cbAlimentosTabG" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Alimentos Tabulador" />
                                </td>
                                <td style="width: 160px">
                                    <asp:Button ID="btnAlimentosTabG" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Aplicar a Todos" Width="110px" />
                                </td>
                                <td style="width: 215px">
                                    <asp:CheckBox ID="cbUsrFactExtempG" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Ingresar Facturas Extemporánea" />
                                </td>
                                <td style="width: 160px">
                                    <asp:Button ID="btnUsrFactExtempG" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Aplicar a Todos" Width="110px" />
                                </td>
                                <td style="width: 215px">
                                    <asp:CheckBox ID="cbUsrFactEmiPrevG" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Ingresar Facturas Emisión Previa" />
                                </td>
                                <td>
                                    <asp:Button ID="btnUsrFactEmiPrevG" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Aplicar a Todos" Width="110px" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:CheckBox ID="cbUsrFactExtempCompG" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Ingresar Facturas Ext. Comp." />
                                </td>
                                <td>
                                    <asp:Button ID="btnUsrFactExtempCompG" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Aplicar a Todos" Width="110px" />
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                        <table style="width: 100%; text-align: center; margin-right: auto; margin-left: auto; height: 60px;">
                            <tr>
                                <td style="width: 180px">&nbsp;</td>
                                <td style="width: 60px">
                                    &nbsp;</td>
                                <td style="width: 60px">
                                    <asp:ImageButton ID="ibtnModif" runat="server" ImageUrl="images\Edit.png" ToolTip="Modificar" Width="20px" />
                                </td>
                                <td style="width: 60px">
                                    &nbsp;</td>
                                <td>&nbsp;</td>
                                <td style="width: 150px">
                                    <asp:TextBox ID="txtValor" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="140px"></asp:TextBox>
                                </td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="ddlCampo" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="110px">
                                        <asp:ListItem Value="nick">Usuario</asp:ListItem>
                                        <asp:ListItem Value="nombre + ' ' + ap_paterno + ' ' + ap_materno">Nombre</asp:ListItem>
                                        <asp:ListItem Value="case when cg_usuario_alim.id_usuario is null then 'No' else 'Sí' end">Alim. Múltiples Personas</asp:ListItem>
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
                                <td style="text-align: center; width: 240px;">&nbsp;</td>
                                <td class="auto-style29">
                                    <asp:GridView ID="gvUsuario" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="830px">
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>
                                            <asp:BoundField DataField="id_usuario" HeaderText="id_usuario">
                                            <ItemStyle Width="30px" />
                                            </asp:BoundField>
                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" SelectText="" ShowSelectButton="True">
                                            <ItemStyle Width="20px" />
                                            </asp:CommandField>
                                            <asp:BoundField DataField="nick" HeaderText="Usuario">
                                            <ItemStyle HorizontalAlign="Center" Width="140px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                            <asp:BoundField DataField="perfil" HeaderText="Perfil">
                                            <ItemStyle HorizontalAlign="Center" Width="170px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="alim_mult_pers" HeaderText="Alim. Múltiples Personas">
                                            <ItemStyle HorizontalAlign="Center" Width="130px" />
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
                                    <asp:Panel ID="pnlDetalle" runat="server" Width="898px">
                                        <table style="width: 100%; height: 50px;">
                                            <tr>
                                                <td style="text-align: right; width: 123px;">
                                                    <asp:Label ID="lbl_TipoMov" runat="server" Text="Tipo de Movimiento:"></asp:Label>
                                                </td>
                                                <td style="text-align: left; ">
                                                    <asp:Label ID="lblTipoMov" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lbl_Empleado" runat="server" Text="Empleado:"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblEmpleado" runat="server" Font-Bold="True"></asp:Label>
                                                    <asp:Label ID="lblIdEmpleado" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="height: 28px; width: 100%;">
                                            <tr>
                                                <td style="text-align: right; width: 123px;">
                                                    <asp:Label ID="lbl_Usuario" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Usuario:"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 264px;">
                                                    <asp:Label ID="lblUsuario" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td style="text-align: right; width: 123px;">
                                                    &nbsp;</td>
                                                <td style="text-align: left; ">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lbl_Perfil" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Perfil:"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblPerfil" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td style="text-align: right; ">
                                                    <asp:Label ID="lbl_CorreoE" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Correo Empleado:"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblCorreoE" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                             <tr>
                                                <td style="text-align: center; " class="auto-style64">
                                                    <asp:Label ID="Label3" runat="server" Font-Bold="True">Comprobacion </asp:Label>
                                                </td>
                                                <td style="text-align: center; " class="auto-style77">
                                                   
                                                </td>
                                                <td style="text-align: center;  " class="auto-style83">
                                                    <asp:Label ID="Label2" runat="server" Font-Bold="True">Factura</asp:Label>
                                                </td>
                                                <td style="text-align: center;  " class="auto-style88">
                                                    
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True">Consulta y Otros</asp:Label>
                                                 </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; " class="auto-style89">
                                                    
                                                    
                                                    <asp:CheckBox ID="cbAntPend" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Comprobar con Anticipos Pendientes" />
                                                    
                                                    
                                                </td>
                                                 <td style="text-align: left; " class="auto-style90">
                                                     <asp:CheckBox ID="cbTaxiTab" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Taxi Tabulador" />
                                                </td>
                                                 <td style="text-align: left; " class="auto-style83">
                                                    <asp:CheckBox ID="cbUsrFactEmiPrev" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Ingresar Facturas Emisión Previa" />
                                                </td>
                                                 <td style="text-align: left; " class="auto-style88">
                                                    <asp:CheckBox ID="cbTransporte" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Cons. Transporte" />
                                                 </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; " class="auto-style66">
                                                    <asp:CheckBox ID="cbLimAutDir" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Límite Monto Aut. Director:" />
                                                   &nbsp;
                                                    <asp:TextBox ID="txtLimAutDir" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxLength="9" Width="40px"></asp:TextBox>
                                                    
                                                </td>
                                                <td style="text-align: left; " class="auto-style79">
                                                  <asp:CheckBox ID="cbAlimentosTab" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Alimentos Tabulador" />
                                                </td>
                                                <td style="text-align: left; " class="auto-style84">
                                                     <asp:CheckBox ID="cbUsrFactExtemp" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Ingresar Facturas Extemporánea" />
                                                </td>
                                                <td style="text-align: left; " class="auto-style49">
                                                    <asp:CheckBox ID="cbUnidadComp" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Unidad en Comp." />
                                                </td>   
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; " class="auto-style89">
                                                    <asp:CheckBox ID="cbUsrPagoEfect" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Pago Comp. en Efectivo" />
                                                   
                                                </td>
                                               <td style="text-align: left; " class="auto-style90">
                                                   <asp:CheckBox ID="cbUsrAlim" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Comprobar Alimentos Múltiple Personas" />
                                                </td>
                                                 <td style="text-align: left; " class="auto-style83">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: left; " class="auto-style88">
                                                    <asp:CheckBox ID="cbMovimientosLibre" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Movimientos Internos" />
                                                </td>   
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; " class="auto-style91">
                                                     <asp:CheckBox ID="cbUsrCotUnica" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Cotización Única" />
                                                </td>
                                                <td style="text-align: left; " class="auto-style92">
                                                    <asp:CheckBox ID="cbLider" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Líder" />
                                                </td>
                                                 <td style="text-align: left; " class="auto-style93">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: center; " class="auto-style94">
                                                   <asp:Label ID="Label1" runat="server" Font-Bold="True">Anticipo </asp:Label>
                                                </td>   
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; " class="auto-style89">
                                                     <asp:CheckBox ID="cbOmitirPGV" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Omitir Prespup. GV" />
                                                </td>
                                                <td style="text-align: left; " class="auto-style90">
                                                    <asp:CheckBox ID="cbOmitirValidacionAnt" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Omitir Anticipo de Comprobacíon" />
                                                </td>
                                                 <td style="text-align: left; " class="auto-style83">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: left; " class="auto-style88">
                                                    <asp:CheckBox ID="cbAntXEmpr" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Anticipos x Empresa:" />
                                                      &nbsp;
                                                    <asp:TextBox ID="txtAntXEmpr" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxLength="4" Width="30px"></asp:TextBox>
                                                </td>   
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; " class="auto-style89">
                                                    <asp:CheckBox ID="cbUsrFactExtempComp" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Periodo Libre Fact. Comprobaciones" />
                                                </td>
                                                <td style="text-align: left; " class="auto-style90">
                                                    <asp:CheckBox ID="cbDatosComprobacion" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Modificacion de datos de Comprobación" />
                                                </td>
                                                 <td style="text-align: left; " class="auto-style83">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: left; " class="auto-style88">
                                                    <asp:CheckBox ID="cbIngresarNocheHospedaje" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Ingresar Noches Hospedaje." />
                                                </td>   
                                            </tr>
                                             <tr>
                                                 <td class="auto-style89" style="text-align: left; ">
                                                     <asp:CheckBox ID="cbFechaTermino" runat="server" Text="Fecha Termino Libre" />
                                                 </td>
                                                 <td class="auto-style90" style="text-align: left; ">
                                                     &nbsp;</td>
                                                 <td class="auto-style83" style="text-align: left; ">&nbsp;</td>
                                                 <td class="auto-style88" style="text-align: left;">
                                                     <asp:CheckBox ID="cbAmericanExpress" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="American Express" />
                                                     <asp:TextBox ID="txtAnticipoAmex" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxLength="4" Width="30px"></asp:TextBox>
                                                 </td>
                                             </tr>
                                        </table> 
                                     <%--   <table style="width:865px;">
                                            <tr>
                                                <td style="text-align: right; width: 123px;">
                                                    &nbsp;</td>
                                                <td style="text-align: left; width: 173px;">
                                                    <asp:CheckBox ID="cbTransporte" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Cons. Transporte" />
                                                </td>
                                                <td style="text-align: right; width: 173px;">
                                                    <asp:CheckBox ID="cbAntXEmpr" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Anticipos x Empresa:" />
                                                </td>
                                                <td style="text-align: left; width: 100px;">
                                                    <asp:TextBox ID="txtAntXEmpr" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxLength="4" Width="60px"></asp:TextBox>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:CheckBox ID="cbAntPend" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Comprobar con Anticipos Pendientes" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; width: 123px;">&nbsp;</td>
                                                <td style="text-align: left; ">
                                                    <asp:CheckBox ID="cbUnidadComp" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Unidad en Comp." />
                                                </td>
                                                <td style="text-align: left; ">
                                                    <asp:CheckBox ID="cbLimAutDir" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Límite Monto Aut. Director:" />
                                                </td>
                                                <td style="text-align: left; ">
                                                    <asp:TextBox ID="txtLimAutDir" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxLength="9" Width="60px"></asp:TextBox>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:CheckBox ID="cbUsrAlim" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Comprobar Alimentos Múltiple Personas" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; width: 123px;">&nbsp;</td>
                                                <td style="text-align: left; ">
                                                    <asp:CheckBox ID="cbUsrPagoEfect" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Pago Comp. en Efectivo" />
                                                </td>
                                                <td style="text-align: left; ">
                                                    <asp:CheckBox ID="cbTaxiTab" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Taxi Tabulador" />
                                                </td>
                                                <td style="text-align: center; ">
                                                    &nbsp;</td>
                                                <td style="text-align: left;">
                                                    <asp:CheckBox ID="cbUsrFactExtemp" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Ingresar Facturas Extemporánea" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; width: 123px;">&nbsp;</td>
                                                <td style="text-align: left; ">
                                                    <asp:CheckBox ID="cbOmitirPGV" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Omitir Prespup. GV" />
                                                </td>
                                                <td style="text-align: left; ">
                                                    <asp:CheckBox ID="cbAlimentosTab" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Alimentos Tabulador" />
                                                </td>
                                                <td style="text-align: left; ">&nbsp;</td>
                                                <td style="text-align: left;">
                                                    <asp:CheckBox ID="cbUsrFactEmiPrev" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Ingresar Facturas Emisión Previa" />
                                                </td>
                                            </tr>
                                        </table>--%>
                                        <table style="height: 28px;">
                                            <tr>
                                                <td style="text-align: right; width: 123px;">&nbsp;</td>
                                                <td style="text-align: center; width: 375px;">
                                                    <asp:GridView ID="gvAutorizadores" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="373px">
                                                        <Columns>
                                                            <asp:BoundField DataField="autorizador" HeaderText="Autorizador">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="director" HeaderText="Director">
                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
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
                                                    &nbsp;</td>
                                                <td style="vertical-align: top; text-align: center; width: 15px;">
                                                    &nbsp;</td>
                                                <td style="vertical-align: top; text-align: center; width: 25px;">
                                                    &nbsp;</td>
                                                <td style="vertical-align: top; text-align: left; width: 275px;">
                                                     &nbsp;
                                                </td>
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
