<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="117.aspx.vb" Inherits="ProcAd._117" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .auto-style5 {
            width: 125px;
            height: 17px;
        }
        .auto-style11 {
            height: 23px;
        }
        .auto-style12 {
            width: 100px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000;">
                <ig:WebScriptManager ID="wsm44" runat="server">
                </ig:WebScriptManager>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdUsuarioAut" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtCorreoAut" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtCorreoSolic" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsPresup" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsInst" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:UpdatePanel ID="upLitError" runat="server">
                    <ContentTemplate>
                        <asp:Literal ID="litError" runat="server"></asp:Literal>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="pnlInicio">
                    <table style="width: 1366px; height: 25px;">
                        <tr>
                            <td>&nbsp;</td>
                            <td style="width: 150px; text-align: right">
                                <asp:Label ID="lbl_Folio" runat="server" Text="Folio"></asp:Label>
                            </td>
                            <td style="width: 130px">
                                <asp:Label ID="lblFolio" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </table>


                    <table style="width: 1366px; height: 44px;">
                        <tr>
                            <td style="width: 50px"></td>

                            <td style="width: 100px; text-align: right;">
                                <asp:Label ID="lbl_Solicitante" runat="server" Text="Solicitante:"></asp:Label>
                            </td>
                            <td style="width: 220px">
                                <asp:Label runat="server" ID="lblSolicitante" ForeColor="Blue" Width="220px"></asp:Label>
                            </td>
                            <td style="width: 100px; text-align: right;">
                                <asp:Label runat="server" ID="lbl_Autorizador" Text="Autorizador:"></asp:Label>
                            </td>
                            <td style="width: 220px;">
                                <asp:Label runat="server" ID="lblAutorizador" ForeColor="Blue" Width="220px"></asp:Label>
                            </td>
                            <td style="text-align: right;" class="auto-style12">
                                <asp:Label runat="server" ID="lbl_Empresa" Text="Empresa:"></asp:Label>
                            </td>
                            <td style="width: 125px">
                                <asp:Label runat="server" ID="lblEmpresa" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="width: 120px; text-align: right">
                                <asp:Label runat="server" ID="lbl_CC" Text="Centro de costos:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblCC" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="width: 50px"></td>
                        </tr>


                    </table>


                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:Panel runat="server" ID="pnlUpdateAmpliacion">
                                <table style="border: medium inset #808080; width: 450px; margin-right: auto; margin-left: auto;">
                                    <tr>
                                        <td class="auto-style11">
                                            <table style="width: 350px; margin-right: auto; margin-left: auto;">
                                                <tr>
                                                    <td style="text-align: center;">
                                                        <asp:Label runat="server" ID="lbl_Titulo" Text="Modificar presupuesto" Font-Bold="True"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td style="width: 60px;"></td>
                                                    <td style="text-align: right; width: 100px">
                                                        <asp:Label runat="server" ID="lbl_MontoSol" Text="Monto solicitado:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:Label runat="server" ID="lblMontoSol" ForeColor="Blue"></asp:Label>
                                                                <ig:WebCurrencyEditor ID="wbeMontoActual" runat="server" Font-Names="Verdana" Font-Size="8pt" ReadOnly="true" Visible="false" Width="10px">
                                                                </ig:WebCurrencyEditor>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>

                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td style="width: 60px;"></td>
                                                    <td style="text-align: right; width: 100px">
                                                        <asp:Label runat="server" ID="lbl_MontoVal" Text="Monto validado:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <ig:WebCurrencyEditor ID="wceMontoSolVal" runat="server" Font-Names="Verdana" Font-Size="8pt">
                                                        </ig:WebCurrencyEditor>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td style="width: 100px"></td>
                                                    <td>
                                                        <asp:Button runat="server" ID="btnActPres" Width="80px" Text="Aceptar" />
                                                    </td>
                                                    <td style="width: 60px"></td>
                                                    <td>
                                                        <asp:Button runat="server" ID="btnCancelarActPres" Width="80px" Text="Cancelar" />
                                                    </td>
                                                    <td style="width: 100px"></td>
                                                </tr>
                                            </table>
                                        </td>

                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <table style="width: 100%;">
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 240px"></td>
                                    <td style="align-items: center">
                                        <asp:UpdatePanel ID="upAdjuntos" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="gvDtAmpliacion" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="798px" SelectedRowStyle-BackColor="Black">
                                                    <Columns>
                                                        <asp:BoundField DataField="id_dt_ampliacion_p" HeaderText="id" />
                                                        <asp:BoundField DataField="monto_solicita" HeaderText="monto">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="año" HeaderText="Año">
                                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="mes" HeaderText="Mes">
                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="monto_actual" DataFormatString="{0:c}" HeaderText="Monto Actual">
                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="monto_solicita" DataFormatString="{0:c}" HeaderText="Monto Solicitado">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="monto_nuevo" DataFormatString="{0:c}" HeaderText="Nuevo Monto">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="impacto_pres_monto" DataFormatString="{0:c}" HeaderText="Impacto Monto">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="impacto_pres_porcent" DataFormatString="{0:p}" HeaderText="Impacto %">
                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="monto_solicita_val" DataFormatString="{0:c}" HeaderText="Monto Validado">
                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                        </asp:BoundField>
                                                        <asp:CommandField ButtonType="Image" SelectImageUrl="images\edit.png" ShowSelectButton="True">
                                                            <ItemStyle Width="10px" />
                                                        </asp:CommandField>
                                                        <asp:TemplateField>
                                                     <%--       <HeaderTemplate>
                                                                <asp:Label Text="Seleccionar" runat="server"></asp:Label><asp:CheckBox ID="cbxHeaderS" runat="server"
                                                                    AutoPostBack="true" OnCheckedChanged="cbxHeaderS_CheckedChanged"
                                                                    />
                                                            </HeaderTemplate>--%>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbxSeleccionar" runat="server" AutoPostBack="true" OnCheckedChanged="cbxSeleccionar_CheckedChanged" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="110px" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                    <SortedDescendingHeaderStyle BackColor="#242121" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td style="width: 230px"></td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <table style="width: 1366px;">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50px"></td>
                            <td style="text-align: right; width: 125px">
                                <asp:Label ID="lbl_MotivosCambio" runat="server" Text="Motivo del cambio:" Width="125px"></asp:Label>
                            </td>
                            <td style="width: 500px">
                                <asp:TextBox ID="txtMotivosCambio" runat="server" Height="50px" ReadOnly="True" TextMode="MultiLine" Width="800px"></asp:TextBox>
                            </td>
                            <td style="width: 50px"></td>
                        </tr>
                    </table>
                    <table style="width: 1366px;">
                        <tr>
                            <td style="width: 50px"></td>

                            <td style="text-align: right; width: 125px;">
                                <asp:Label ID="lbl_JustBeneficio" runat="server" Text="Beneficios en caso de autorizar el cambio:" Width="125px"></asp:Label>
                            </td>
                            <td style="width: 800px">
                                <asp:TextBox ID="txtJustBeneficio" runat="server" Height="50px" ReadOnly="true" TextMode="MultiLine" Width="800px" MaxLength="350"></asp:TextBox>
                            </td>
                            <td style="width: 50px"></td>

                        </tr>
                    </table>

                    <table style="width: 1366px;">
                        <tr>
                            <td style="width: 50px"></td>

                            <td style="text-align: right; width: 125px;">
                                <asp:Label ID="lblImplicaciones" runat="server" Text="Implicaciones en caso de no autorizar el cambio:" Width="125px"></asp:Label>
                            </td>
                            <td style="width: 800px">
                                <asp:TextBox ID="txtJustImpacto" runat="server" Height="50px" ReadOnly="true" TextMode="MultiLine" Width="800px" MaxLength="350"></asp:TextBox>
                            </td>
                            <td style="width: 50px"></td>

                        </tr>
                    </table>

                    <table>
                        <tr>
                            <td class="auto-style5" style="text-align: right">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>

                    <table style="width: 1366px; height: 44px">
                        <tr>
                            <td style="width: 50px"></td>
                            <td style="text-align: right; width: 125px">
                                <asp:Label ID="lblAdjuntos" runat="server" Text="Adjuntos:"></asp:Label>
                            </td>
                            <td style="width: 800px">
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
                            <td style="width: 50px"></td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                    </table>

                    <asp:UpdatePanel runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <table style="width: 1366px;">
                                <tr>
                                    <td style="width: 300px"></td>
                                    <td style="width: 200px">
                                        <asp:Button ID="btnAutorizar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Autorizar" UseSubmitBehavior="false" Width="200px" />
                                    </td>
                                    <td style="width: 350px"></td>
                                    <td style="width: 200px">
                                        <asp:Button ID="btnRechazar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Rechazar" UseSubmitBehavior="false" Width="200px" />
                                    </td>
                                    <td style="width: 300px"></td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>


                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
