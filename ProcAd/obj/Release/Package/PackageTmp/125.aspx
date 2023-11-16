<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="125.aspx.vb" Inherits="ProcAd._125" %>
<%@ Register Assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI" TagPrefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style5 {
            width: 147px;
        }
        .auto-style6 {
            width: 100px;
            height: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
     <tr>
         <td style="text-align: center; color: #FF0000;">
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsInst" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtTipoMov" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
             <ig:WebScriptManager ID="WebScriptManager1" runat="server"></ig:WebScriptManager>
         </td>
     </tr>
    <tr>
        <td>
            <asp:Panel ID="pnlInicio" runat="server">
                   <table style="width: 1360px; height: 25px;">
                        <tr>
                            <td>&nbsp;</td>
                            <td style="width: 150px; text-align: right">
                                <asp:Label ID="lbl_Folio" runat="server" Text="Folio:"></asp:Label>
                            </td>
                            <td style="width: 130px">
                                <asp:Label ID="lblFolio" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1360px; height: 44px;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Solicitante" runat="server" Text="Solicitante:"></asp:Label>
                            </td>
                            <td class="auto-style9">
                                <asp:Label ID="lblSolicitante" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                             <td class="auto-style10" style="text-align: right; ">
                                <asp:Label ID="lbl_Empresa" runat="server" Text="Empresa:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <asp:Label ID="lblEmpresa" runat="server" Text="" ForeColor="Blue" ></asp:Label>
                            </td>
                            <td class="auto-style10" style="text-align: right">
                                <asp:Label ID="lbl_CentroCosto" runat="server" Text="Centro de Costo:"></asp:Label>
                            </td>
                            <td class="auto-style9">
                                <asp:Label ID="lblCentroCosto" runat="server" Text="" ForeColor="Blue" ></asp:Label>
                            </td>
                           
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Tipo" runat="server" Text="Tipo:"></asp:Label>
                            </td>
                            <td>
                                 <asp:Label ID="lblTipo" runat="server" Text="" ForeColor="Blue" ></asp:Label>
                            </td>
                            <td style="text-align: right">
                                &nbsp;</td>
                            <td>
                                <asp:RadioButton ID="rbAdministrativo" runat="server" Text="Administrativo" />
                                &nbsp;
                                <asp:RadioButton ID="rbOperativo" runat="server" Text="Operativo" />
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Division" runat="server" Text="División:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDivision" runat="server" Text="" ForeColor="Blue" ></asp:Label>                               
                            </td>
                        </tr>
                        <tr>
                            <td>

                            </td>
                            <td>

                            </td>
                            <td style="text-align:right">
                                <asp:Label ID="lbl_Autorizador" runat="server" Text="Autorizador:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblAutorizador" runat="server" Text="" ForeColor="Blue" ></asp:Label>                                
                            </td>
                            <td>

                            </td>
                            <td></td>
                        </tr>
                    </table>
                    <table style="width: 100%; ">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Especificaciones" runat="server" Text="Especificaciones:"></asp:Label>
                            </td>
                            <td style="width: 747px">
                                <asp:TextBox ID="txtEspecificaciones" runat="server" Height="79px" ReadOnly="True" TextMode="MultiLine" Width="723px"></asp:TextBox>
                            </td>
                            <td class="auto-style10" style="text-align: right">
                                <asp:Label ID="lbl_Adjunto" runat="server" Text="Adjuntos:"></asp:Label>
                            </td>
                            <td>
                                <asp:GridView ID="gvAdjuntos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ShowHeader="False" Width="280px">
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
                <table style="width:100%">
                    <tr>
                        <td style="width:130px; text-align:right">
                            <asp:Label ID="lbl_Porcentaje" runat="server" Text="Porcentaje Asignado:"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblPorcentaje" runat="server" Text="0" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnlGrid" runat="server">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 100px;"></td>
                            <td style="width: 850px">
                                <asp:GridView ID="gvRegistros" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="700px">
                                    <Columns>
                                        <asp:BoundField DataField="id_dt_movimientos_int" HeaderText="id_dt_movimientos_int">
                                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                                        </asp:BoundField>
                                        <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" SelectText="" ShowSelectButton="True">
                                            <ItemStyle Width="20px" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="porcentaje" HeaderText="Porcentaje">
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                           <asp:BoundField DataField="cuenta_contable" HeaderText="Cuenta Contable">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="centro_costo" HeaderText="Centro de Costo">
                                            <ItemStyle HorizontalAlign="Left" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="division" HeaderText="División">
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="zona" HeaderText="Zona">
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
                            <td>
                                <table class="auto-style6">
                                    <tr>
                                        <td style="width: 60px">
                                            <asp:ImageButton ID="ibtnAlta" runat="server" ImageUrl="images\Add.png" ToolTip="Alta" Width="20px" style="height: 20px" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:ImageButton ID="ibtnBaja" runat="server" ImageUrl="images\Trash.png" ToolTip="Baja" Width="20px" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:ImageButton ID="ibtnModif" runat="server" ImageUrl="images\Edit.png" ToolTip="Modificar" Width="20px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlDatos" runat="server" Width="85%">
                    <table style="border: medium inset #808080; width: 950px; margin-right: auto; margin-left: auto;">
                        <tr>
                            <td>
                                <table style="width: 100%; height: 50px;">
                                    <tr>
                                        <td style="text-align: right; width: 110px;">
                                            <asp:Label ID="lbl_CuentaContable" runat="server" Text="Cuenta Contable :"></asp:Label>
                                        </td>
                                        <td style="text-align: left; width:250px" >
                                            <asp:TextBox ID="txtCuentaContable" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="16px" Width="250px"></asp:TextBox>
                                        </td>
                                        <td style="text-align:right; width:100px">
                                            <asp:Label ID="lbl_PorcentajeA" runat="server" Text="Porcentaje :"></asp:Label>
                                        </td>
                                        <td style="text-align:left; width:150px">
                                            <asp:TextBox ID="txtPorcentaje" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="16px" Width="140px"></asp:TextBox>
                                        </td>
                                        <td style="text-align:right; width:100px">
                                           <asp:Label ID="lbl_CC" runat="server" Text="Centro Costo :"></asp:Label>
                                        </td>
                                        <td style="text-align:left; width:150px">
                                            <asp:TextBox ID="txtCentroCosto" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="16px" Width="140px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right">
                                             <asp:Label ID="lbl_Div" runat="server" Text="División :"></asp:Label>
                                        </td>
                                        <td style="text-align:left">
                                            <asp:TextBox ID="txtDiv" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="16px" Width="140px"></asp:TextBox>
                                        </td>
                                        <td style="text-align:right">
                                             <asp:Label ID="lbl_zona" runat="server" Text="Zona :"></asp:Label>
                                        </td>
                                        <td style="text-align:left">
                                            <asp:TextBox ID="txtZona" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="16px" Width="140px"></asp:TextBox>
                                        </td>
                                        <td>

                                        </td>
                                        <td>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>

                                        </td>
                                        <td>
                                            <asp:Button ID="btnAceptarC" runat="server" Text="Aceptar" Font-Names="Verdana" Font-Size="8pt" Width="200px" />
                                        </td>
                                        <td>

                                        </td>
                                        <td>
                                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Font-Names="Verdana" Font-Size="8pt" Width="200px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                 <table style="width: 1366px; height: 44px;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Comentario" runat="server" Text="Comentarios: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtComentario" runat="server" Height="52px" TextMode="MultiLine" Width="1072px" MaxLength="350"></asp:TextBox>
                            </td>
                        </tr>
                        
                    </table>
                <asp:Panel ID="pnlBotones" runat="server">


                    <table style="width: 1366px; height: 44px;">
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" Font-Names="Verdana" Font-Size="8pt" Width="200px" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button ID="btnRechazar" runat="server" Text="Rechazar" Font-Names="Verdana" Font-Size="8pt" Width="200px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:Panel>
        </td>
    </tr>
    </table>
</asp:Content>
