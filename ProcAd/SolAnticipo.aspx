<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="SolAnticipo.aspx.vb" Inherits="ProcAd.SolAnticipo" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
     
     .auto-style8 {
         height: 21px;
     }
     .auto-style11 {
         width: 300px;
         height: 21px;
     }

 </style>
    <script>
        function selectOnlyThis(id) {
            for (var i = 1; i <= 3; i++) {
                document.getElementById("cbTipo" + i).checked = false;
            }
            document.getElementById(id).checked = true;
        }

        function MutExChkList(chk) {
            var chkList = chk.parentNode.parentNode.parentNode;
            var chks = chkList.getElementsByTagName("input");
            for (var i = 0; i < chks.length; i++) {
                if (chks[i] != chk && chk.checked) {
                    chks[i].checked = false;
                }
            }
        }

    </script>

   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000;">
                <ig:WebScriptManager ID="wsm46" runat="server">
                </ig:WebScriptManager>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsInst" runat="server" Width="15px" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; color: #FF0000;">
                <asp:Label ID="lblMessage" runat="server" Font-Names="Verdana" Font-Size="8pt" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlInicio" runat="server">
                    <table style="width: 1366px; height: 25px; font-family: Verdana; font-size: 8pt;">
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
                            <td style="text-align: right; width: 260px">
                                <asp:Label ID="lbl_Empresa" runat="server" Text="Empresa:"></asp:Label>
                            </td>
                            <td style="width: 520px">
                                <asp:UpdatePanel ID="upEmpresa" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlEmpresa" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="220px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>

                            <td style="text-align: right; width: 50px">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="height: 20px;text-align:right">
                                            <asp:Label ID="lbl_Proveedor" runat="server" Text="Proveedor:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 20px">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                            <td class="auto-style8">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtProveedor" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="200px"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="ibtnBuscarProv" runat="server" AutoPostBack="True" Height="16px" ImageUrl="images\Search.png" ToolTip="Buscar" Width="15px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="upProveedor" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlProveedor" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="280px">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lblAutorizador" runat="server" Text="Autorizador:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAutorizador" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="220px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; height: 45px">
                                <asp:Label ID="lbl_Opcion" runat="server" Text="Seleccione una opción:"></asp:Label>
                            </td>
                            <td>
                                <table style="width: 485px">
                                    <tr>
                                        <td>
                                            <asp:CheckBoxList ID="cbTipoAnticipo" runat="server" OnSelectedIndexChanged="cbTipoAnticipo_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Text="Anticipo" Value="1" onclick="MutExChkList(this);" />
                                                <asp:ListItem Text="Pago Anticipado" Value="2" onclick="MutExChkList(this);" />
                                                <asp:ListItem Text="Pago Anticipado Agente Aduanal" Value="3" onclick="MutExChkList(this);" />
                                            </asp:CheckBoxList>
                                        </td>

                                       <%-- <td>
                                            <asp:CheckBox ID="cbAnticipo" runat="server" Text="Anticipo" OnCheckedChanged="cbAnticipo_CheckedChanged" AutoPostBack="true" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="cbPagoAnticipado" runat="server" Text="Pago Anticipado" OnCheckedChanged="cbPagoAnticipado_CheckedChanged" AutoPostBack="true"  />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="cbPagoAnticipadoAA" runat="server" Text="Pago Anticipado Agente Aduanal" OnCheckedChanged="cbPagoAnticipadoAA_CheckedChanged" AutoPostBack="true" />
                                        </td>--%>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Importe" Text="Importe requerido: " runat="server"></asp:Label>
                            </td>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <ig:WebCurrencyEditor ID="wceImporte" runat="server" Width="120px">
                                            </ig:WebCurrencyEditor>
                                        </td>
                                        <td style="text-align:right">
                                            <asp:Label ID="lbl_Divisa" Text="Divisa: " runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDivisa" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100px"></asp:DropDownList>

                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width:125px;text-align:right">
                                <asp:Label ID="lbl_PedidosCompra" Text="Pedidos de compra: " runat="server" Visible ="false" ></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPedidosCompra" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="200px" Visible ="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPedidosCompra_SelectedIndexChanged">
                                     <asp:ListItem Value="0"></asp:ListItem>
                                     <asp:ListItem Value="1">Pedido 1</asp:ListItem>
                                        <asp:ListItem Value="2">Pedido 2</asp:ListItem>
                                        <asp:ListItem Value="3">pedido 3</asp:ListItem>
                                </asp:DropDownList> &nbsp;&nbsp;
                                <asp:ImageButton ID="ibtnAltaPedidoC" runat="server" ImageUrl="images\Add.png" ToolTip="Alta" Width="20px" Visible="false"/>&nbsp;&nbsp;
                                <asp:ImageButton ID="ibtnBajaPedidoC" runat="server" ImageUrl="images\Trash.png" ToolTip="Baja" Width="20px" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;vertical-align:top">
                                <asp:Label ID="lbl_Justificacion" runat="server" Text="Justificación del anticipo: "></asp:Label>
                            </td>
                            <td style="vertical-align:top">
                                <asp:TextBox ID="txtJustificacion" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="63px" TextMode="MultiLine" Width="535px"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                            
                            <td style="vertical-align: top">
                                <asp:UpdatePanel runat="server" ID="upPedidosCompras" UpdateMode="Conditional" Visible="False">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvPedidosCompras" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="270px" DataKeyNames="id_dt_pedidos_compra">
                                            <Columns>
                                                <asp:BoundField DataField="id_anticipo" HeaderText="id_pedido_compra" Visible="false">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="pedido_compra" HeaderText="Pedido de compra">
                                                    <ItemStyle HorizontalAlign="left" Width="250px" />
                                                </asp:BoundField>
                                                <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                    <ItemStyle Width="20px" />
                                                </asp:CommandField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                            <SortedDescendingHeaderStyle BackColor="#242121" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                        </tr>
                        
                    </table>
                    <asp:Panel runat="server" ID="pnlAdjuntos" Visible ="false">
                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align: right">

                                    <asp:Label ID="lbl_Adjuntos" runat="server" Text="Archivo Adjunto: "></asp:Label>
                                </td>
                                <td>
                                    <asp:FileUpload ID="fuAdjunto" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px" />
                                    &nbsp; &nbsp; &nbsp;
                                <asp:Button ID="btnAgregarAdj" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnAgregarAdj_Click" OnClientClick="this.disabled = true; this.value='Procesando…';" Text="Agregar Archivo" UseSubmitBehavior="false" />
                                </td>

                            </tr>

                            <tr>
                                <td style="width: 260px; text-align: right">
                                    <asp:Label ID="lbl_Adjunto" runat="server" Text="Adjuntos:"></asp:Label>
                                </td>
                                <td style="width: 600px; vertical-align: top">
                                    <asp:UpdatePanel ID="upAdjuntos" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvAdjuntos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ShowHeader="False" Width="300px">
                                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                                <Columns>
                                                    <asp:HyperLinkField DataNavigateUrlFields="path" DataTextField="archivo" HeaderText="Adjuntos">
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
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>

                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table style="width:1366px; height:44px;">
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align:center">
                                  <asp:Button ID="btnGuardar" runat="server" Text="Enviar a Autorización" Width="200px" OnClientClick="this.disabled = true; this.value='Procesando…';" UseSubmitBehavior="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
