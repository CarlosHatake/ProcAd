<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ModContratos.aspx.vb" Inherits="ProcAd.ModContratos" %>

<%@ Register Assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style6 {
            height: 30px;
        }

        .auto-style7 {
            width: 140px;
            height: 70px;
        }

        .auto-style8 {
            width: 155px;
            height: 70px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; height: 25px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td style="text-align: center; color: #FF0000;">
                <ig:WebScriptManager ID="wsm46" runat="server">
                </ig:WebScriptManager>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtBan" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsInst" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdContrato" runat="server" Width="15px" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style6"></td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="pnlInfoContrato">
                    <table>
                        <tr style="height: 20px">
                            <td style="width: 140px"></td>
                            <td>
                                <asp:Label runat="server" Text="Contrato:"></asp:Label>
                            </td>
                            <td style="width: 270px">
                                <asp:Label runat="server" ID="lblContrato" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" Text="Empresa:"></asp:Label>
                            </td>
                            <td style="width: 250px">
                                <asp:Label runat="server" ID="lblEmpresa" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" Text="Tipo Arrendamiento:"></asp:Label>
                            </td>
                            <td style="width: 250px">
                                <asp:Label runat="server" ID="lblTipoArrendamiento" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td style="width: 140px"></td>
                            <td>
                                <asp:Label runat="server" Text="Arrendadora:"></asp:Label>
                            </td>
                            <td style="width: 270px">
                                <asp:Label runat="server" ID="lblArrendadora" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" Text="RFC Arrendadora:"></asp:Label>
                            </td>
                            <td style="width: 250px">
                                <asp:Label runat="server" ID="lblRFCArrendadora" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" Text="Plazo:"></asp:Label>
                            </td>
                            <td style="width: 250px">
                                <asp:Label runat="server" ID="lblPlazo" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>

                        </tr>
                        <tr style="height: 20px">
                            <td style="width: 140px"></td>

                            <td>
                                <asp:Label runat="server" Text="Fecha inicio:"></asp:Label>
                            </td>
                            <td style="width: 270px">
                                <asp:Label runat="server" ID="lblFecInicio" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" Text="Fecha Fin:"></asp:Label>
                            </td>
                            <td style="width: 250px">
                                <asp:Label runat="server" ID="lblFechaFin" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" Text="Inversión:"></asp:Label>
                            </td>
                            <td style="width: 250px">
                                <asp:Label runat="server" ID="lblInversion" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <%--<table>
                        <tr>
                            <td style="width:140px; height:100px"></td>
                            <td>
                                <asp:Label runat ="server" Text="Mostrar unidades:" Font-Bold ="true"></asp:Label>
                            </td>
                            <td style="width: 300px;">
                                <asp:CheckBoxList ID="cbConsultaUnidades" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbConsultaUnidades_SelectedIndexChanged">
                                    <asp:ListItem Text="Unidades por anexo" Value="1" onclick="MutExChkList(this);" />
                                    <asp:ListItem Text="Unidades por contrato" Value="2" onclick="MutExChkList(this);" />
                                </asp:CheckBoxList>

                            </td>
                        </tr>
                    </table>--%>
                     </asp:Panel>
                    <table>
                         <tr>
                            <td style="width:140px"></td>
                            <td class="auto-style8">
                                <asp:Label runat="server" Text="ANEXOS DEL CONTRATO" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
               
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:Panel runat="server" ID="pnlModificarAnexo" Visible="False">
                            <table>
                                <tr style="height: 30px">
                                    <td style="width: 140px"></td>
                                    <td style="text-align: right">
                                        <asp:Label runat="server" Text="Anexo:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtAnexo"></asp:TextBox>
                                    </td>
                                    <td style="width: 50px"></td>
                                    <td style="text-align: right">
                                        <asp:Label runat="server" Text="Fecha inicio:"></asp:Label>
                                    </td>
                                    <td>
                                        <ig:WebDatePicker ID="wdpFecInicioAnexo" runat="server" Width="160px"></ig:WebDatePicker>
                                    </td>
                                    <td style="width: 50px"></td>
                                    <td style="text-align: right">
                                        <asp:Label runat="server" Text="Fecha fin:"></asp:Label>
                                    </td>
                                    <td>
                                        <ig:WebDatePicker ID="wdpFecFinAnexo" runat="server" Width="160px"></ig:WebDatePicker>
                                    </td>
                                    <td style="width: 50px"></td>
                                    <td>
                                        <asp:Button runat="server" ID="btnGuardarAnexo" Text="Guardar" Font-Names="Verdana" Font-Size="8pt" Width="80px" />
                                    </td>
                                    <td style="width: 25px"></td>

                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <asp:UpdatePanel runat="server" ID="pnlgvAnexos" Visible="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table>
                            <tr style="height: 30px">
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="width: 140px"></td>
                                <td>
                                    <asp:GridView ID="gvAnexos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px" DataKeyNames="id_ms_anexo">
                                        <Columns>
                                            <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="anexo" HeaderText="Anexo">
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="tipo_arrendamiento" HeaderText="Tipo Arrendamiento">
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="arrendadora" HeaderText="Arrendadora">
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_inicio" HeaderText="Fecha incio" DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_fin" HeaderText="Fecha fin" DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                                <ItemStyle Width="15px" />
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
                                </td>
                                <%--<td style="width: 300px;">
                                     <asp:Panel runat="server" ID="pnlMostrarUnidades">
                                        <table>
                                            <td style="width:70px"></td>
                                            <td>
                                                <asp:CheckBoxList ID="cbConsultaUnidades" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbConsultaUnidades_SelectedIndexChanged">
                                                    <asp:ListItem Text="Unidades por anexo" Value="1" onclick="MutExChkList(this);" />
                                                    <asp:ListItem Text="Unidades por contrato" Value="2" onclick="MutExChkList(this);" />
                                                </asp:CheckBoxList>
                                            </td>
                                        </table>
                                    </asp:Panel>
                                </td>--%>
                               
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <%--<asp:UpdatePanel runat="server" ID="upUnidades" UpdateMode="Conditional">--%>
                    <%--<ContentTemplate>--%>
                        <asp:Panel ID="pnlUnidades" runat="server" Visible="false">
                            <table>
                                <tr>
                                    <td style="width: 140px; height: 60px"></td>
                                    <td style="width: 160px">
                                        <asp:Label runat="server" Text="UNIDADES DEL ANEXO" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td style="text-align: right; width: 635px">
                                    <asp:FileUpload ID="UpArchivo" runat="server" Font-Names="Verdana" Font-Size="8pt" />
                                    </td>
                                    <td style="width:10px"></td>
                                    <td>
                                         <asp:Button ID="btnImportarUnidades" runat="server" Text="Importar" Font-Size="8pt" Font-Names="Verdana" />
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td>
                                        <asp:Button ID="btnExportarUnidades" runat="server" Text="Exportar" Font-Size="8pt" Font-Names="Verdana" />
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Panel runat="server" ID="pnlgvUnidades" Visible="False">
                                            <table>
                                                <tr>
                                                    <td style="width: 140px"></td>

                                                    <td>
                                                      <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                               
                                                                <asp:GridView ID="gvUnidades" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="900px" DataKeyNames="id_ms_anexo, id_ms_equipo">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="serie" HeaderText="Serie">
                                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="año" HeaderText="Año">
                                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="mes1" HeaderText="Mes 1">
                                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="mes2" HeaderText="Mes 2">
                                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="mes3" HeaderText="Mes 3">
                                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="mes4" HeaderText="Mes 4">
                                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="mes5" HeaderText="Mes 5">
                                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="mes6" HeaderText="Mes 6">
                                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="mes7" HeaderText="Mes 7">
                                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="mes8" HeaderText="Mes 8">
                                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="mes9" HeaderText="Mes 9">
                                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="mes10" HeaderText="Mes 10">
                                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="mes11" HeaderText="Mes 11">
                                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="mes12" HeaderText="Mes 12">
                                                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                                        </asp:BoundField>
                                                                        <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True" HeaderText="Factura">
                                                                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                                                                        </asp:CommandField>
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

                                                                 <asp:GridView ID="gvUnidadesExportar" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="900" Visible="false">
                                                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
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
                                                <tr>
                                                    <td></td>
                                                    <td style="height: 40px">
                                                        <asp:Label runat="server" Text="Total:" Font-Bold="true"></asp:Label>
                                                        &nbsp;
                                                    <asp:Label runat="server" ID="lblTotal" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                                    </td>

                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                  <%--  </ContentTemplate>
                </asp:UpdatePanel>--%>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlFacturas" runat="server" Visible="false">
                    <table>
                        <tr style="height: 40px">
                            <td style="width: 140px; height: 60px"></td>
                            <td style="width: 130px">
                                <asp:Label runat="server" Text="FACTURAS" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width: 120px; text-align: right">
                                <asp:Label runat="server" Text="Centro de Costos:"></asp:Label>
                            </td>
                            <td style="width: 180px">
                                <asp:DropDownList runat="server" ID="ddlCC" Width="150px"></asp:DropDownList>
                            </td>
                            <td style="width: 60px; text-align: right">
                                <asp:Label runat="server" Text="División:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlDivision" Width="150px"></asp:DropDownList>
                            </td>
                            <td style="width: 90px; text-align: right">
                                <asp:Label runat="server" Text="Proveedor:"></asp:Label>
                            </td>
                            <td style="width: 180px">
                                <asp:Label runat="server" ID="lblProveedor" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width: 30px">
                                <asp:ImageButton ID="ibtnAgregarFactura" runat="server" ImageUrl="images\Add.png" ToolTip="Alta" Width="20px"/>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="width: 140px; text-align: right"></td>
                            <td>
                                <asp:GridView ID="gvFacturasProveedor" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="900px" DataKeyNames="id_dt_factura">
                                    <Columns>
                                        <%--<asp:BoundField DataField="id" HeaderText="idCFDI" />--%>
                                        <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                            <ItemStyle Width="15px" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="fecha_emision" HeaderText="Fecha Emisión">
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="uuid" HeaderText="UUID">
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="serie" HeaderText="Serie">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="folio" HeaderText="Folio">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="lugar_exp" HeaderText="Lugar Exp.">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="forma_pago" HeaderText="Forma Pago">
                                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="moneda" HeaderText="Moneda">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="subtotal" HeaderText="Subtotal">
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="importe" HeaderText="Importe">
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:BoundField>
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
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
