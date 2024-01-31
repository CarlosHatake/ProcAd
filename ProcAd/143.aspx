<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="143.aspx.vb" Inherits="ProcAd._143" %>
<%@ Register Assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <style type="text/css">
      .auto-style5 {
          width: 150px;
          }
      .auto-style8 {
          height: 21px;
      }
      .auto-style9 {
          width: 300px;
      }
      .auto-style10 {
          width: 140px;
          height: 21px;
      }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000;">
                <ig:webscriptmanager ID="wsm50" runat="server">
                </ig:webscriptmanager>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsInst" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdAutorizador2" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdAutorizador3" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtCFDI_factura" runat="server" Width="15px" Visible="false"></asp:TextBox>
                <asp:TextBox ID="_txtCFDI_egreso" runat="server" Width="15px" Visible="false"></asp:TextBox>
                <asp:TextBox ID="_txtCFDI_anticipo" runat="server" Width="15px" Visible="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlInicio" runat="server">
                    <table style="width: 1366px; height: 25px;">
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
                    <table style="width: 1366px; height: 44px;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Solicitante" runat="server" Text="Solicitante:"></asp:Label>
                            </td>
                            <td class="auto-style9">
                                <asp:Label ID="lblSolicitante" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td class="auto-style10" style="text-align: right">
                                <asp:Label ID="lbl_Empresa" runat="server" Text="Empresa:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblEmpresa" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td class="auto-style10" style="text-align: right; ">
                                <asp:Label ID="lbl_CC" runat="server" Text="Centro de Costo:"></asp:Label>
                            </td>
                            <td class="auto-style8">
                                <asp:Label ID="lblCC" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Proveedor" runat="server" Text="Proveedor:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblProveedor" runat="server" ForeColor="Blue" Width="700px"></asp:Label>
                            </td>
                            <td style="text-align:right">
                                <asp:Label ID="lbl_Division" runat="server" Text="División"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDivision" runat="server" Text="" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Autorizador" runat="server" Text="Autorizador:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblAutorizador" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Autorizador2" runat="server" Text="Segundo Autorizador:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblAutorizador2" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Autorizador3" runat="server" Text="Tercer Autorizador:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblAutorizador3" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                    </table>
                   
                    <table style="width: 1366px; height: 44px;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Factura" runat="server" Text="Factura:"></asp:Label>
                            </td>
                            <td>
                                <asp:GridView ID="gvFacturas" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="900px">
                                    <Columns>
                                        <asp:BoundField DataField="id_dt_comprobacion_anticipo" HeaderText="id_dt_comprobacion_anticipo" />

                                        <asp:BoundField DataField="fecha_emision" HeaderText="Fecha Emisión">
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="uuid" HeaderText="UUID">
                                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="lugar_exp" HeaderText="Lugar Exp.">
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="forma_pago" HeaderText="Forma Pago">
                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="moneda" HeaderText="Moneda">
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="subtotal" DataFormatString="{0:c}" HeaderText="Subtotal">
                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="importe" DataFormatString="{0:c}" HeaderText="Importe">
                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
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
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: right; vertical-align: top; width:150px">
                                <asp:Label ID="lbl_Evidencia" runat="server" Text="Evidencias:"></asp:Label>
                            </td>
                            <td style="width: 700px; vertical-align: top;">
                                <asp:GridView ID="gvEvidencias" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ShowHeader="False" Width="350px">
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
                            <td></td>
                        </tr>
                    </table>
                    <table style="width: 1366px; height: 44px;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Comentario" runat="server" Text="Comentarios:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtComentario" runat="server" Height="52px" TextMode="MultiLine" Width="1072px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1366px; height: 50px;">
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnAceptar" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnAceptar_Click" OnClientClick="this.disabled = true; this.value='Procesando…';" Text="Aceptar" UseSubmitBehavior="false" Width="200px" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button ID="btnRechazar" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnRechazar_Click" OnClientClick="this.disabled = true; this.value='Procesando…';" Text="Rechazar" UseSubmitBehavior="false" Width="200px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
