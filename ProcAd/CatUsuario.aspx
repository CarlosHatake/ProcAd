<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="CatUsuario.aspx.vb" Inherits="ProcAd.CatUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        function limpiar(cual, accion) {
            // Action: 0=Deseleccionar todos 1=Seleccionar todos -1=Invertir seleccion
            var f = document.formulario
            for (var i = 0; i < f.elements.length; i++) {
                var obj = f.elements[i]
                var name = obj.name
                if (name == cual) {
                    obj.checked = ((accion == 1) ? true : ((accion == 0) ? false : !obj.checked));
                }
            }
        }
    </script>
    <style type="text/css">

        .auto-style29 {
            width: 963px;
        }
        .auto-style30 {
            width: 170px;
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
                <asp:TextBox ID="_txtIdGral" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="txtAccesos" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
               
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
                                        <asp:ListItem Value="nick">Usuario</asp:ListItem>
                                        <asp:ListItem Value="nombre + ' ' + ap_paterno + ' ' + ap_materno">Nombre</asp:ListItem>
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
                                            <asp:BoundField DataField="nick" HeaderText="Usuario" >
                                            <ItemStyle HorizontalAlign="Center" Width="140px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="perfil" HeaderText="Perfil">
                                            <ItemStyle HorizontalAlign="Center" Width="200px" />
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
                                    <asp:Panel ID="pnlDetalle" runat="server" Width="910px">
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
                                                    <asp:TextBox ID="txtEmpleado" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="16px" Width="343px"></asp:TextBox>
                                                    &nbsp;&nbsp;&nbsp;<asp:Button ID="cmdBuscarE" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Buscar" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">&nbsp;</td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlEmpleado" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="450px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="height: 28px; width: 100%;">
                                            <tr>
                                                <td style="text-align: right; width: 123px;">
                                                    <asp:Label ID="lbl_Usuario" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Usuario:"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 264px;">
                                                    <asp:TextBox ID="txtUsuario" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxLength="15" Width="160px"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right; width: 123px;">
                                                    <asp:Label ID="lbl_Contraseña" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Contraseña:"></asp:Label>
                                                </td>
                                                <td style="text-align: left; ">
                                                    <asp:TextBox ID="txtContraseña" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px" Enabled="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtContraseñaOrg" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="15px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lbl_Perfil" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Perfil:"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlPerfil" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="200px">
                                                        <asp:ListItem Value="Usr">Usuario</asp:ListItem>
                                                        <asp:ListItem Value="Aut">Autorizador</asp:ListItem>
                                                        <asp:ListItem Value="DirAdFi">Director de Administración y Finanzas</asp:ListItem>
                                                        <asp:ListItem Value="AdmViajes">Administrador de Viajes</asp:ListItem>
                                                        <asp:ListItem Value="SegViajes">Seguimiento a Viajes</asp:ListItem>
                                                        <asp:ListItem Value="GerConta">Gerente de Contabilidad</asp:ListItem>
                                                        <asp:ListItem Value="AdmCat">Admin. Catálogos</asp:ListItem>
                                                        <asp:ListItem Value="AdmCatEst">Admin. Catálogos Estadía</asp:ListItem>
                                                        <asp:ListItem Value="Conta">Contador</asp:ListItem>
                                                        <asp:ListItem Value="ContaF">Contador Funcionarios</asp:ListItem>
                                                        <asp:ListItem Value="Vig">Vigilante</asp:ListItem>
                                                        <asp:ListItem Value="GerTesor">Gerente de Tesorería</asp:ListItem>
                                                        <asp:ListItem Value="ValPresup">Validador Presupuesto</asp:ListItem>
                                                        <asp:ListItem Value="AdmonDCM">Administración Dicomex</asp:ListItem>
                                                        <asp:ListItem Value="Comp">Comprobaciones</asp:ListItem>
                                                        <asp:ListItem Value="CoPame">Comprobaciones PAME</asp:ListItem>
                                                        <asp:ListItem Value="CoDCM">Comprobaciones DICOMEX</asp:ListItem>
                                                        <asp:ListItem>Caja</asp:ListItem>
                                                        <asp:ListItem Value="CxP">Cuentas por Pagar</asp:ListItem>
                                                        <asp:ListItem Value="GerSopTec">Gerente de Soporte Técnico</asp:ListItem>
                                                        <asp:ListItem Value="SopTec">Soporte Técnico</asp:ListItem>
                                                        <asp:ListItem Value="JefCompras">Jefe de Compras</asp:ListItem>
                                                        <asp:ListItem Value="Compras">Compras</asp:ListItem>
                                                        <asp:ListItem Value="DesOrg">Desarrollo Organizacional</asp:ListItem>
                                                        <asp:ListItem Value="JefInfo">Jefatura de Información</asp:ListItem>
                                                        <asp:ListItem Value="Liq">Liquidador</asp:ListItem>
                                                        <asp:ListItem Value="AutAud">Autorizador Auditoría</asp:ListItem>
                                                        <asp:ListItem Value="Aud">Auditor</asp:ListItem>
                                                        <asp:ListItem Value="UsrSL">Usuario Solo Lectura</asp:ListItem>
                                                        <asp:ListItem Value="AltUsr">Admin. Usuarios</asp:ListItem>
                                                        <asp:ListItem Value="Adm">Administrador</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: right; ">
                                                    <asp:Label ID="lbl_CorreoE" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Correo Empleado:"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:TextBox ID="txtCorreoE" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxLength="50" Width="220px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                          <table style="width:100%;">
                                            <tr>
                                                <td style="text-align: right; width: 123px;">
                                                    <asp:Label ID="lbl_PIN" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="PIN:"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 145px;">
                                                    <asp:TextBox ID="txtPIN" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxLength="4" Width="60px"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right; width: 173px;">
                                                   
                                                </td>
                                                <td style="text-align: left; width: 140px;">
                                                    
                                                </td>
                                                <td style="text-align: left;">
                                                   
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
                                                      &nbsp;</td>
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
                                                <td style="text-align: left; " class="auto-style94">
                                                    <asp:CheckBox ID="cbChecador" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Ingreso Checador" />
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
                                                <td style="text-align: left;" class="auto-style94">
                                                    <asp:CheckBox ID="cbConsAntProv" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Cons. Anticipos Prov." />
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
                                                      &nbsp;
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True">Anticipo </asp:Label>
                                                </td>
                                                
                                            </tr>
                                             <tr>
                                                 <td class="auto-style89" style="text-align: left; ">
                                                    <asp:CheckBox ID="cbFechaTermino" runat="server" Text="Fecha Termino Libre" />
                                                 </td>
                                                 <td class="auto-style90" style="text-align: left; ">
                                                     
                                                 </td>
                                                 <td class="auto-style83" style="text-align: left; ">&nbsp;</td>
                                                 <td style="text-align: left; " class="auto-style88">
                                                    <asp:CheckBox ID="cbAntXEmpr" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Anticipos x Empresa:" />
                                                    <asp:TextBox ID="txtAntXEmpr" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxLength="4" Width="30px"></asp:TextBox>
                                                </td>   
                                             </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                 <td class="auto-style88" style="text-align: left; ">
                                                     <asp:CheckBox ID="cbIngresarNocheHospedaje" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Ingresar Noches Hospedaje." />
                                                 </td>
                                            </tr>
                                        </table> 
                                           
                                              
                                        <%----%>
                                            
                                      
                                        <table style="height: 28px;">
                                            <tr>
                                                <td style="text-align: right; width: 123px;">&nbsp;</td>
                                                <td style="text-align: center; width: 375px;">
                                                    <asp:GridView ID="gvAutorizadores" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="373px">
                                                        <Columns>
                                                            <asp:BoundField DataField="id_dt_autorizador" HeaderText="id_dt_autorizador" />
                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                            <ItemStyle Width="15px" />
                                                            </asp:CommandField>
                                                            <asp:BoundField DataField="autorizador" HeaderText="Autorizador">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="director" HeaderText="Director">
                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:BoundField>
                                                             <asp:BoundField DataField="validador" HeaderText="Validador">
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
                                                    <asp:ImageButton ID="ibtnAltaAut" runat="server" ImageUrl="images\Add.png" ToolTip="Agregar" Width="20px" />
                                                </td>
                                                <td style="vertical-align: top; text-align: center; width: 30px;">
                                                    <asp:ImageButton ID="ibtnBajaAut" runat="server" ImageUrl="images\Trash.png" style="height: 20px" ToolTip="Eliminar" Width="20px" />
                                                </td>
                                                <td style="vertical-align: top; text-align: center; width: 30px;">
                                                    <asp:ImageButton ID="ibtnModifAut" runat="server" ImageUrl="images\Edit.png" ToolTip="Modificar" Width="20px" />
                                                </td>
                                                <td style="vertical-align: top; text-align: left; width: 250px;">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlAutorizador" runat="server">
                                        <table style="width:100%;">
                                            <tr>
                                                <td style="width: 130px;">&nbsp;</td>
                                                <td>
                                                    <table style="border: thin solid #000000; width: 850px; height: 44px;">
                                                        <tr>
                                                            <td style="text-align: right; width: 115px;">
                                                                <asp:Label ID="lbl_UsuarioAut" runat="server" Text="Autorizador:"></asp:Label>
                                                            </td>
                                                            <td class="auto-style30" style="text-align: left; ">
                                                                <asp:TextBox ID="txtUsuarioAutB" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="140px"></asp:TextBox>
                                                            </td>
                                                            <td style="text-align: left; width: 115px;">
                                                                <asp:Button ID="cmdBuscarUsrAut" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Buscar" />
                                                            </td>
                                                            <td rowspan="2" style="width: 150px">
                                                                 <asp:CheckBox ID="cbDirector" runat="server" AutoPostBack="true"   Font-Names="Verdana" Font-Size="8pt" Text="Director"/>
                                                                 <asp:CheckBox ID="cbValidador" runat="server" AutoPostBack="true" Font-Names="Verdana" Font-Size="8pt" Text="Validador"/>
                                                            </td>
                                                            <td rowspan="2" style="text-align: center">
                                                                <asp:Button ID="btnAceptarAut" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnAceptarAut_Click" OnClientClick="this.disabled = true;" Text="Aceptar" UseSubmitBehavior="false" Width="90px" />
                                                            </td>
                                                            <td rowspan="2" style="text-align: center">
                                                                <asp:Button ID="btnCancelarAut" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnCancelarAut_Click" OnClientClick="this.disabled = true;" Text="Cancelar" UseSubmitBehavior="false" Width="90px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right; width: 115px;">&nbsp;</td>
                                                            <td colspan="2" style="text-align: left;">
                                                                <asp:DropDownList ID="ddlUsuarioAut" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                                                </asp:DropDownList>
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
                                                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" Width="150px" Font-Names="Verdana" Font-Size="8pt" style="height: 21px" />
                                            </td>
                                            <td style="text-align: center" aria-busy="False" aria-expanded="true" class="auto-style35">
                                                <asp:Button ID="btnReCredenciales" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Restablecer contraseña" Width="206px" />
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
