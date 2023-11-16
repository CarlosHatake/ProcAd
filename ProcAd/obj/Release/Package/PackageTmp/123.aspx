<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="123.aspx.vb" Inherits="ProcAd._123" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000;">
                <ig:webscriptmanager ID="wsm01" runat="server">
                </ig:webscriptmanager>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsInst" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdValidador" runat="server" Width="15px" Visible="False"></asp:TextBox>

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
                                <asp:DropDownList ID="ddlEmpresa" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="180px" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td class="auto-style10" style="text-align: right">
                                <asp:Label ID="lbl_CentroCosto" runat="server" Text="Centro de Costo:"></asp:Label>
                            </td>
                            <td class="auto-style9">
                                  <asp:UpdatePanel ID="upCC" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlCC" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="280px" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel></td>
                           
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Tipi" runat="server" Text="Tipo:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTipo" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                     <asp:ListItem Value="CPP">Cuentas Por Pagar</asp:ListItem>
                                     <asp:ListItem Value="CPC">Cuentas Por Cobrar</asp:ListItem>
                                     <asp:ListItem Value="CON">Contabilidad</asp:ListItem>

                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right">
                                &nbsp;</td>
                            <td>
                                <asp:UpdatePanel ID="upAdmonOper" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:RadioButtonList ID="rblAdmonOper" runat="server" AutoPostBack="True" RepeatColumns="2" Width="240px">
                                            <asp:ListItem Value="Admon">Administrativo</asp:ListItem>
                                            <asp:ListItem Value="Oper">Operativo</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Division" runat="server" Text="División:"></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="upDiv" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlDiv" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Width="280px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

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
                                <asp:DropDownList ID="ddlAutorizador" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="220px"></asp:DropDownList>
                            </td>
                            <td>

                            </td>
                            <td></td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                         <td style="width: 150px; height: 30px; text-align: right">
                                <asp:Label ID="lbl_Especificaciones" runat="server" Text="Especificaciones:"></asp:Label>
                            </td>
                            <td style="width: 847px">
                                <asp:TextBox ID="txtEspecificaciones" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="43px" TextMode="MultiLine" Width="835px"></asp:TextBox>
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width:1366px; height: 44px">
                           <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lbl_Archivo" runat="server" Text="Archivo:"></asp:Label>
                            </td>
                            <td>
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width: 520px">
                                            <asp:FileUpload ID="fuAdjunto" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="500px" />
                                        </td>
                                        <td style="width: 105px">
                                            &nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnAgregarAdj" runat="server" Font-Names="Verdana" Font-Size="8pt" OnClick="btnAgregarAdj_Click" OnClientClick="this.disabled = true; this.value='Procesando…';" Text="Agregar Archivo" UseSubmitBehavior="false" />
                                        </td>
                                    </tr>
                                  
                                </table>
                            </td>
                        </tr>
                            <tr>
                            <td style="text-align: right; vertical-align: top;">
                                <asp:Label ID="lblAdjuntosComp" runat="server" Text="Adjuntos:"></asp:Label>
                            </td>
                            <td>
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width: 600px; vertical-align: top">
                                            <asp:UpdatePanel ID="upAdjuntos" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvAdjuntosComp" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" ShowHeader="False" Width="350px">
                                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                                        <Columns>
                                                           <asp:HyperLinkField DataNavigateUrlFields="ruta" DataTextField="nombre" HeaderText="Nombre" />
                                                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" Visible="false">
                                                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="nombre_archivo" HeaderText="nombre_archivo">
                                                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ruta_archivo" HeaderText="ruta_archivo">
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
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="width: 150px; text-align: right; vertical-align: top;">
                                            
                                        </td>
                                        <td style="vertical-align: top">
                                        
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td style="text-align:center">
                <asp:Button ID="btnAceptar" runat="server" Text="Solicitar Validación" Font-Names="Verdana" Font-Size="8pt" />
            </td>
        </tr>
    </table>
</asp:Content>
