<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ConAntProvedor.aspx.vb" Inherits="ProcAd.ConAntProvedor" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style58 {
            width: 127px;
        }
        .auto-style59 {
            width: 1518px;
        }
        </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:1360px; font-family: Verdana; font-size: 8pt;">
        <tr>
             <td style="text-align: center; font-weight: bold; color: #FF0000;" class="auto-style59">
                <ig:WebScriptManager ID="wsmConsAntProv" runat="server">
                </ig:WebScriptManager>
                <asp:TextBox ID="_txtIdSolicito" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtEmpleado" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtPerfil" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <table style="width: 1360px">
                    <tr>
                        <td class="auto-style48">
                            <asp:Literal ID="litError" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="auto-style59"> 
                <asp:Panel ID="pnlFiltros" runat="server" Width="1360px">
                 <table style="width :100%">
                     <tr>

                          <td style="text-align: right; width :100px">&nbsp;</td>
                          <td style="text-align: left; width:270px"> 
                                  <asp:CheckBox ID="cbSolicitante" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Solicitante:" />
                          </td>
                          <td style="text-align: right; width:310px;">
                              <asp:Panel ID="pnlSolicitante" runat="server" Width="310px">
                                  <asp:DropDownList ID="ddlSolicitante" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="310px">
                                  </asp:DropDownList>
                              </asp:Panel>
                          </td>
                         <td style="text-align: left; width:310px">
                              <asp:CheckBox ID="cbEmpresa" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Empresa:" />
                          </td>
                         <td style="text-align: right; width:270px;">
                              <asp:Panel ID="pnlEmpresa" runat="server" Width="310px">
                                  <asp:DropDownList ID="ddlEmpresa" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="310px">
                                  </asp:DropDownList>
                              </asp:Panel>
                          </td>
                          <td style="text-align: right; width :100px">&nbsp;</td>
                     </tr>
                     <tr>
                          <td style="text-align: right; width :100px">&nbsp;</td>
                          <td style="text-align: left; width: 270px;">
                                <asp:CheckBox ID="cbFechaC" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt"  Text="Fecha Creación:" />
                           </td> 
                          <td style="text-align: left; width: 310px;">
                                <asp:Panel ID="pnlFechaC" runat="server" Width="310px">
                                    <table style="width:260px;">
                                        <tr>
                                            <td style="border: medium inset #808080; margin-right: auto; margin-left: auto;">
                                                <table style="width: 127px;">
                                                    <tr>
                                                        <td style="text-align: center" class="auto-style48">
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
                                                <table class="auto-style58">
                                                    <tr>
                                                        <td style="text-align: center" class="auto-style48">
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
                         <td style="text-align: left; width:270px">
                              <asp:CheckBox ID="cbStatus" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Estado de Comprobación:" />
                          </td>
                          <td style="text-align: left; width:310px;">
                              <asp:Panel ID="pnlStatus" runat="server" Width="310px">
                                    <asp:DropDownList ID="ddlStatus" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="310px">
                                        <asp:ListItem Value="'P'">Pendiente de autorización por autorizador</asp:ListItem>
                                        <asp:ListItem Value="'A'">Autorizado por el autorizador</asp:ListItem>
                                        <asp:ListItem Value="'ZA'">Rechazado por el autorizador</asp:ListItem>
                                        <asp:ListItem Value="'A'">Autorizado por el director</asp:ListItem>
                                        <asp:ListItem Value="'ZD'">Rechazado por el director</asp:ListItem>
                                        <asp:ListItem Value="'A'">Autorizado por el C</asp:ListItem>
                                        <asp:ListItem Value="'ZC'">Rechazado por el C</asp:ListItem>
                                        <asp:ListItem Value="'AF'">Validado en codificacion contable</asp:ListItem>
                                        <asp:ListItem Value="'RN'">Comprobado</asp:ListItem>
                                        <asp:ListItem Value="'Z'">Rechazado por cuentas por pagar</asp:ListItem>
                                       
                                    </asp:DropDownList>
                                </asp:Panel>
                          </td>
                          <td style="text-align: right; width :100px">&nbsp;</td>
                     </tr>

                       <tr>
                           <td style="text-align: right; width :100px">&nbsp;</td>
                            <td style="text-align: left; width: 270px;">
                                <asp:CheckBox ID="cbAutorizador" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="130px" Text="Autorizador:" />
                            </td>
                          <td style="width: 310px">
                                <asp:Panel ID="pnlAutorizador" runat="server" Width="310px">
                                    <asp:DropDownList ID="ddlAutorizador" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="310px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                          <td  style="text-align: left; width :270px">
                                <asp:CheckBox ID="cbTipoAnt" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Tipo Anticipo:" />
                            </td>
                          <td style="text-align: left; width:310px;">
                                <asp:Panel ID="pnlTipoAnt" runat="server" Width="310px">
                                    <asp:DropDownList ID="ddlTipoAnt" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="310px">
                                        <asp:ListItem Value="1">Anticipo</asp:ListItem>
                                        <asp:ListItem Value="2">Pago Anticipado </asp:ListItem>
                                        <asp:ListItem Value="3">pago anticipado agente aduanal</asp:ListItem>
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                        <td style="text-align: right; width :100px">&nbsp;</td>
                      </tr>
                       <tr>
                           <td style="text-align: right; width :100px">&nbsp;</td>
                            <td style="text-align: left; width: 270px;">
                                <asp:CheckBox ID="cbNoAnticipo" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="130px" Text="No Anticipo" />
                            </td>
                          <td style="width: 310px">
                               <asp:Panel ID="pnlNoAnticipo" runat="server" Width="120px">
                                    <asp:TextBox ID="txtNoAnticipo" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px" ></asp:TextBox>
                                </asp:Panel>
                            </td>
                          <td  style="text-align: left; width :270px">&nbsp;</td>
                          <td style="text-align: left; width:310px;">&nbsp;</td>
                        <td style="text-align: right; width :100px">&nbsp;</td>
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

                <asp:Panel ID="pnlRegistros" runat="server" Width="1360px">
                    <table style="width:100%;">
                        <tr>
                            <td style="width: 1px">&nbsp;</td>
                            <td>
                                <table style="width: 200px;">
                                    <tr>
                                        <td style="text-align: center" class="auto-style48">
                                            <asp:Button ID="btnExportar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Exportar" Width="150px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 1px">&nbsp;</td>
                            <td>
                                <asp:GridView ID="gvRegistros" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1559px">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                            <ItemStyle Width="15px" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="id_ms_anticipo_proveedor" HeaderText="No. Anticipo">
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="empleado_solicita" HeaderText="Solicita">
                                            <ItemStyle HorizontalAlign="Left" Width="180px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="autorizador" HeaderText="Autorizador">
                                            <ItemStyle HorizontalAlign="Left" Width="160px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="importe_requerido" DataFormatString="{0:c}" HeaderText="Importe">
                                            <ItemStyle HorizontalAlign="Right" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_solicita" HeaderText="Fecha de Solicitud">
                                            <ItemStyle HorizontalAlign="Center" Width="170px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_reg_autorizador" HeaderText="Fecha de Autorizacion">
                                            <ItemStyle HorizontalAlign="Center" Width="170px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_prog_pago_tesoreria" HeaderText="Fecha programación de pago">
                                            <ItemStyle HorizontalAlign="Center" Width="170px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="estado_anticipo" HeaderText="Estado Anticipo">
                                            <ItemStyle HorizontalAlign="Center" Width="220px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="estatus" HeaderText="Estado de Comprobación">
                                            <ItemStyle HorizontalAlign="Center" Width="250px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_anticipo" HeaderText="Tipo de Anticipo">
                                            <ItemStyle HorizontalAlign="Center" Width="220px" />
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
                                <asp:GridView ID="gvRegistrosT" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1560px">
                                                <Columns>
                                                    <asp:BoundField DataField="id_ms_anticipo_proveedor" HeaderText="No. Anticipo">
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px"/>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="empleado_solicita" HeaderText="Solicita">
                                                    <ItemStyle HorizontalAlign="Center" Width="180px"/>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="autorizador" HeaderText="Autorizador">
                                                    <ItemStyle HorizontalAlign="Left" Width="160px"/>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="importe_requerido" HeaderText="Importe">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px"/>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_solicita" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha de Solicitud">
                                                    <ItemStyle HorizontalAlign="Center" Width="170px"/>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_reg_autorizador" HeaderText="Fecha Autorizacion">
                                                    <ItemStyle HorizontalAlign="Center" Width="170px"/>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecha_prog_pago_tesoreria" HeaderText="Fecha Pago">
                                                    <ItemStyle HorizontalAlign="Center" Width="170px"/>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="estado_anticipo" HeaderText="Estado Anticipo">
                                                        <ItemStyle HorizontalAlign="Center" Width="220px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="estatus" HeaderText="Estado">
                                                    <ItemStyle HorizontalAlign="Center" Width="250px"/>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="tipo_anticipo" HeaderText="Tipo de Anticipo">
                                                    <ItemStyle HorizontalAlign="Center" Width="220px"/>
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

                <asp:Panel ID="pnlDetalle" runat="server">
                     <table style="width:1360px;">
                        <tr>
                            <td style="text-align: center; width: 300px; height: 35px;">
                                <asp:Button ID="btnNueBusProd" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Nueva Búsqueda" Width="150px" />
                            </td>
                            <td style="text-align: center; width: 460px; height: 35px;">
                                &nbsp;</td>
                            <td style="text-align: center; width: 210px; height: 35px;">
                                &nbsp;</td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Folio" runat="server" Text="Folio:"></asp:Label>
                            </td>
                            <td class="auto-style28">
                                <asp:Label ID="lblFolio" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1360px; height: 44px;">
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Solicitante" runat="server" Text="Solicitante:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSolicitante" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_NoProveedor" runat="server" Text="Num. Proveedor:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblNoProveedor" runat="server" ForeColor="Blue" Width="150px"></asp:Label>
                            </td>
                            <td style="text-align: right; ">
                                <asp:Label ID="lbl_Empresa" runat="server" Text="Empresa:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblEmpresa" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Autorizador" runat="server" Text="Autorizador:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblAutorizador" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_FechaS" runat="server" Text="Fecha de Solicitud:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFechaS" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Proveedor" runat="server" Text="Proveedor:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblProveedor" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1360px;">
                        <tr>
                            <td style="width: 150px; height: 30px; text-align: right">
                                <asp:Label ID="lbl_Act" runat="server" Text="Justificacion:"></asp:Label>
                            </td>
                            <td style="width: 747px">
                                <asp:TextBox ID="txtAct" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="43px" ReadOnly="True" TextMode="MultiLine" Width="735px"></asp:TextBox>
                            </td>
                            <td  style= "width: 130px; text-align: right;">
                                <asp:label  ID="lbl_Adjunto" runat="server" Text="Adjuntos:" >  </asp:label>
                            </td>
                            
                               <td style= "width: 330px; "text-align: right;">
                                <asp:GridView ID="gvAdjuntos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ShowHeader="False" Width="320px">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <Columns>
                                        <asp:HyperLinkField DataNavigateUrlFields="path" DataTextField="archivo" HeaderText="Archivo">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="20px" />
                                        </asp:HyperLinkField>
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
    </table>
 </asp:Content>
