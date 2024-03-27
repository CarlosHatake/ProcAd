<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="AsginarCuentaTemporal.aspx.vb" Inherits="ProcAd.AsginarCuentaTemporal" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .auto-style5 {
        width: 150px;
    }
    .auto-style9 {
        width: 300px;
    }
    .auto-style10 {
        width: 140px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000;">
                <ig:WebScriptManager ID="wsm14" runat="server">
                </ig:WebScriptManager>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsInst" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtTipoMov" runat="server" Width="15px" Visible="False"></asp:TextBox>
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
                            <td class="auto-style9">
                                <asp:Label ID="lblEmpresa" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td class="auto-style10" style="text-align: right; ">
                                <asp:Label ID="lbl_CC" runat="server" Text="Centro de Costo:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCC" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
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
                                <asp:Label ID="lbl_Proveedor" runat="server" Text="Proveedor:"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblProveedor" runat="server" ForeColor="Blue" Width="700px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_RFC" runat="server" Text="RFC Proveedor:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblRFC" runat="server" ForeColor="Blue" Width="280px"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                &nbsp;</td>
                            <td colspan="3">
                                &nbsp;</td>
                        </tr>
                    </table>
                    <table style="width: 1366px; height: 44px;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">&nbsp;</td>
                            <td>
                                <asp:GridView ID="gvFactura" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" Width="1072px">
                                    <Columns>
                                        <asp:BoundField DataField="fecha_emision" HeaderText="Fecha Emisión">
                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="uuid" HeaderText="UUID">
                                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="serie" HeaderText="Serie">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="folio" HeaderText="Folio">
                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="lugar_exp" HeaderText="Lugar Exp.">
                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
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
                                        <asp:HyperLinkField DataNavigateUrlFields="path" DataTextField="pdf">
                                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                                        </asp:HyperLinkField>
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
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td class="auto-style5" style="text-align: right; vertical-align: top;">
                                <asp:Label ID="lbl_Adjunto" runat="server" Text="Adjuntos:"></asp:Label>
                            </td>
                            <td style="width: 350px; vertical-align: top;">
                                <asp:GridView ID="gvAdjuntos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ShowHeader="False" Width="340px">
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
                           
                            <td style="width: 60px; text-align: right; vertical-align: top;">
                                &nbsp;</td>
                            <td style="vertical-align: top">
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlPartida" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td style="width: 60px">&nbsp;</td>
                            <td>
                                <table style="border: thin solid #000000; width: 1250px; height: 44px;">
                                    <tr>
                                        <td style="text-align: right; width: 115px;">
                                            <asp:Label ID="lbl_TipoMov" runat="server" Text="Tipo de Mov.:"></asp:Label>
                                        </td>
                                        <td style="width: 100px">
                                            <asp:Label ID="lblTipoMov" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lbl_TipoAsig" runat="server" Text="Asig. por:"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <asp:RadioButtonList ID="rblTipoAsig" runat="server" AutoPostBack="True" RepeatColumns="2" Width="180px">
                                                <asp:ListItem Value="P">Porcentaje</asp:ListItem>
                                                <asp:ListItem Value="I">Importe</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td style="text-align: right; width: 120px;">
                                            &nbsp;</td>
                                        <td style="width: 60px">
                                            &nbsp;</td>
                                        <td style="text-align: right; width: 90px;">
                                            &nbsp;</td>
                                        <td style="width: 60px">
                                            &nbsp;</td>
                                        <td style="width: 70px; text-align: right;">
                                            &nbsp;</td>
                                        <td style="width: 60px">
                                            &nbsp;</td>
                                        <td style="text-align: center">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 115px;">
                                            <asp:Label ID="lbl_CuentaC" runat="server" Text="Cuenta Contable:"></asp:Label>
                                        </td>
                                        <td style="width: 100px">
                                            <asp:TextBox ID="txtCuentaC1" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxLength="4" Width="30px">9999</asp:TextBox>
                                            <asp:Label ID="lbl_CuentaCG" runat="server" Text="-"></asp:Label>
                                            <asp:TextBox ID="txtCuentaC2" runat="server" Font-Names="Verdana" Font-Size="8pt" MaxLength="4" Width="30px">9999</asp:TextBox>
                                        </td>
                                        <td style="width: 70px; text-align: right;">
                                            <asp:Label ID="lbl_Porcent" runat="server" Text="Porcentaje:"></asp:Label>
                                        </td>
                                        <td style="width: 100px">
                                            <ig:WebNumericEditor ID="wnePorcent" runat="server" MaxDecimalPlaces="2" Width="50px">
                                            </ig:WebNumericEditor>
                                            <asp:Label ID="lblPorcent" runat="server" Visible="False"></asp:Label>
                                        </td>
                                        <td style="width: 70px; text-align: right;">
                                            <asp:Label ID="lbl_Importe" runat="server" Text="Importe:"></asp:Label>
                                        </td>
                                        <td style="width: 110px">
                                            <ig:WebCurrencyEditor ID="wceImporte" runat="server" Width="100px">
                                            </ig:WebCurrencyEditor>
                                        </td>
                                        <td style="text-align: right; width: 120px;">
                                            <asp:CheckBox ID="cbCC" runat="server" Text="Centro de Costo" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:TextBox ID="txtCC" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="50px"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; width: 90px;">
                                            <asp:CheckBox ID="cbDiv" runat="server" Text="División" AutoPostBack ="true" />
                                        </td>
                                        <td style="width: 60px">
                                           <asp:UpdatePanel runat="server" ID="upDivDDl" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList runat="server" ID="ddlDivD" Width="130px" AutoPostBack="true"></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        </td>
                                        <td style="width: 70px; text-align: right;">
                                            <asp:CheckBox ID="cbZona" runat="server" Text="Zona" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:TextBox ID="txtZona" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="40px"></asp:TextBox>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Button ID="btnAceptarP" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnAceptarP_Click" OnClientClick="this.disabled = true;" Text="Aceptar" UseSubmitBehavior="false" Width="90px" />
                                        </td>
                                    </tr>
                               <tr>
                                        <td style="text-align: right; width: 115px;"></td>
                                        <td style="width: 100px"></td>
                                        <td style="width: 70px; text-align: right;"></td>
                                        <td style="width: 100px"></td>
                                        <td style="width: 70px; text-align: right;"></td>
                                        <td style="width: 110px"></td>
                                        <td style="text-align: right; width: 120px;"></td>
                                        <td style="width: 60px"></td>

                                        <td style="text-align: left; width: 130px;"></td>
                                        <td width="60px">
                                        <asp:UpdatePanel runat="server" ID="upDivD" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                        <asp:TextBox ID="txtDiv" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="60px" Enabled ="false"  ></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            </td>
                                        <td style="width: 60px">
                                            
                                        </td>

                                        <td style="width: 70px; text-align: right;"></td>
                                        <td style="width: 60px"></td>
                                        <td style="text-align: center"></td>
                                    </tr>
                                </table>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
                    <table style="width: 1366px; height: 44px;" >
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_PorcentAsig" runat="server" Text="Porcentaje Asignado:" Width="140px"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPorcentAsig" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5" style="text-align: right">&nbsp;</td>
                            <td>
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width: 780px">
                                            <asp:GridView ID="gvPartidas" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="770px" DataKeyNames="id_dt_factura">
                                                <Columns>
                                                    <asp:BoundField DataField="id_dt_factura_div" HeaderText="id_dt_factura_div" />
                                                    
<%--                                                    <asp:BoundField DataField="id_dt_factura" HeaderText="id_dt_factura" />--%>
                                                    <asp:BoundField DataField="id_ms_factura" HeaderText="id_ms_factura" />
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                    <ItemStyle Width="15px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="cuenta_c" HeaderText="Cuenta Contable">
                                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="porcent" HeaderText="Porcentaje">
                                                    <ItemStyle HorizontalAlign="Center" Width="25" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Importe" HeaderText="Importe Partida" DataFormatString="{0:c}">
                                                     <ItemStyle HorizontalAlign="Right" Width="90px"/>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="centro_costo" HeaderText="Centro de Costo">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="division" HeaderText="División">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
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
                                        <td style="vertical-align: top; text-align: center; width: 30px;">
                                            <asp:ImageButton ID="ibtnAlta" runat="server" ImageUrl="images\Add.png" ToolTip="Agregar" Width="20px" />
                                        </td>
                                        <td style="vertical-align: top; text-align: center; width: 30px;">
                                            <asp:ImageButton ID="ibtnBaja" runat="server" ImageUrl="images\Trash.png" ToolTip="Eliminar" Width="20px" />
                                        </td>
                                        <td style="vertical-align: top; text-align: center; width: 30px;">
                                            <asp:ImageButton ID="ibtnModif" runat="server" ImageUrl="images\Edit.png" ToolTip="Modificar" Width="20px" />
                                        </td>
                                        <td style="vertical-align: top; text-align: left">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlComentario" runat="server">
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
                </asp:Panel>
                    <table style="width: 1366px; height: 50px;">
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnAceptar" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnAceptar_Click" OnClientClick="this.disabled = true;" Text="Registrar" UseSubmitBehavior="false" Width="200px" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button ID="btnRechaza" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnRechaza_Click" OnClientClick="this.disabled = true;" Text="Rechazar" UseSubmitBehavior="false" Width="200px" />
                            </td>
                        </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
