<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="Anexos.aspx.vb" Inherits="ProcAd.Anexos" %>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="CargaContratos.aspx.vb" Inherits="ProcAd.CargaContratos" %>
<%@ Register Assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
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
    <style type="text/css">
        .auto-style5 {
            height: 85px;
        }
    </style>
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
            <td class="auto-style5">
                <asp:Panel ID="pnlInicio" runat="server">
                    <table style="width: 1366px; height: 25px; font-family: Verdana; font-size: 8pt;">
                        <tr>
                            <td>&nbsp;</td>
                            <td style="width: 150px; text-align: right">
                                <asp:Label ID="lbl_Folio" runat="server" Text="Folio:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width: 130px">
                                <asp:Label ID="lblFolio" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="width: 610px"></td>
                            <td>
                                <asp:CheckBoxList ID="cbTipoCarga" runat="server" AutoPostBack="true">
                                    <asp:ListItem Text="Carga de archivo" Value="1" onclick="MutExChkList(this);" />
                                    <asp:ListItem Text="Carga manual" Value="2" onclick="MutExChkList(this);" />
                                </asp:CheckBoxList>
                            </td>
                            <td style="width: 610px"></td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="pnlCargarArchivo" Visible="false">
                    <table>
                        <tr>
                            <td style="width: 100px"></td>
                        </tr>
                        <tr>
                            <td style="width: 100px"></td>
                            <td>
                                <asp:Label runat="server" Text="Cargar archivo:"></asp:Label>
                            </td>
                            <td>
                                <asp:FileUpload runat="server" ID="fuArchivo" Font-Size="8pt" Font-Names="Verdana" />
                            </td>
                            <td>&nbsp;&nbsp;
                                <asp:Button runat="server" ID="btnValidar" Text="Validar archivo" Font-Size="8pt" Font-Names="Verdana" />
                            </td>
                            <td style="width: 500px"></td>
                            <td>
                                <asp:Button runat="server" ID="btnGenerarPlantilla" Text="Generar plantilla" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px"></td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="width: 100px"></td>
                            <td>
                                <asp:GridView ID="gvRegistros" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1200px" AllowPaging="True" PageSize="10">
                                    <Columns>
                                        <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="contrato" HeaderText="Contrato">
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="anexo" HeaderText="Anexo">
                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="arrendado" HeaderText="Arrendado">
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="serie" HeaderText="Serie">
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_arrendamiento" HeaderText="Tipo de arrendamiento">
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="periodo" HeaderText="Periodo">
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_inicio" HeaderText="Fecha inicio" DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_fin" HeaderText="Fecha fin" DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
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
                    <table>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="width: 200px"></td>

                            <td>
                                <asp:Label runat="server" Text="Registrados:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblRegistrados" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 200px"></td>

                            <td style="text-align: right">
                                <asp:Label runat="server" Text="Omitidos:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblOmitidos" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="width: 600px"></td>
                            <td>
                                <asp:Button ID="btnGuardarArchivo" runat="server" Text="Guardar" Font-Size="8pt" Font-Names="Verdana" Width="150px" OnClientClick="this.disabled = true; this.value='Procesando…';" UseSubmitBehavior="false" Height="22px" />
                            </td>
                            <td style="width: 600px"></td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="pnlCargaManual" Visible="false">
                    <table>
                        <tr style="height: 30px">
                            <td style="width: 160px"></td>
                            <td style="text-align: right">
                                <asp:Label runat="server" Text="Empresa:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlEmpresa" Width="160px"></asp:DropDownList>
                            </td>
                            <td style="width: 50px"></td>
                            <td style="text-align: right">
                                <asp:Label runat="server" Text="No.Contrato:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtContrato" Width="150px"></asp:TextBox>
                            </td>
                            <td style="width: 50px"></td>
                            <td style="text-align: right">
                                <asp:Label runat="server" Text="RFC Arrendadora:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblRFCArrendadoraContrato" Font-Bold="true" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="width: 160px"></td>

                        </tr>
                        <tr style="height: 30px">
                            <td style="width: 160px"></td>
                            <td style="text-align: right">
                                <asp:Label runat="server" Text="Arrendadora:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlArrendadora" Width="160px"></asp:DropDownList>
                            </td>
                            <td style="width: 50px"></td>
                            <td style="text-align: right">
                                <asp:Label runat="server" Text="Tipo de arrendamiento:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlTipoArrendamiento" Width="160px"></asp:DropDownList>
                            </td>
                            <td style="width: 50px"></td>
                            <td style="text-align: right">
                                <asp:Label runat="server" Text="Periodo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtPeriodo" Width="150px"></asp:TextBox>
                            </td>
                            <td style="width: 160px"></td>

                        </tr>
                        <tr style="height: 30px">
                            <td style="width: 160px"></td>
                            <td style="text-align: right">
                                <asp:Label runat="server" Text="Fecha inicio:"></asp:Label>
                            </td>
                            <td>
                                <ig:WebDatePicker ID="wdpFechaIni" runat="server" Width="160px"></ig:WebDatePicker>
                            </td>
                            <td style="width: 50px"></td>
                            <td style="text-align: right">
                                <asp:Label runat="server" Text="Fecha fin:"></asp:Label>
                            </td>
                            <td>
                                <ig:WebDatePicker ID="wdpFechaFin" runat="server" Width="160px"></ig:WebDatePicker>
                            </td>
                            <td style="width: 50px"></td>
                            <td style="text-align: right">
                                <asp:Label runat="server" Text="Inversión:"></asp:Label>
                            </td>
                            <td>
                                <ig:WebCurrencyEditor ID="wceInversíon" runat="server" Width="150px">
                                </ig:WebCurrencyEditor>
                            </td>
                            <td style="width: 160px"></td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="width: 600px"></td>
                            <td>
                                <asp:Button ID="btnGuardarContrato" runat="server" Text="Guardar contrato" Font-Size="8pt" Font-Names="Verdana" Width="150px" OnClientClick="this.disabled = true; this.value='Procesando…';" UseSubmitBehavior="false" />
                            </td>
                            <td style="width: 600px"></td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Panel runat="server" ID="pnlInfoContrato">
                                    <table>
                                        <tr>
                                            <td style="width: 160px"></td>
                                            <td>
                                                <asp:Label runat="server" Text="Contrato:"></asp:Label>
                                            </td>
                                            <td style="width: 200px">
                                                <asp:Label runat="server" ID="lblContrato" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" Text="Empresa:"></asp:Label>
                                            </td>
                                            <td style="width: 200px">
                                                <asp:Label runat="server" ID="lblEmpresa" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" Text="Arrendadora:"></asp:Label>
                                            </td>
                                            <td style="width: 200px">
                                                <asp:Label runat="server" ID="lblArrendadora" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" Text="Tipo de Arrendamiento:"></asp:Label>
                                            </td>
                                            <td style="width: 200px">
                                                <asp:Label runat="server" ID="lblTipoArrendamiento" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 160px"></td>
                                            <td>
                                                <asp:Label runat="server" Text="RFC Arrendadora:"></asp:Label>
                                            </td>
                                            <td style="width: 200px">
                                                <asp:Label runat="server" ID="lblRFCArrendadora" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" Text="Plazo:"></asp:Label>
                                            </td>
                                            <td style="width: 200px">
                                                <asp:Label runat="server" ID="lblPlazo" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" Text="Fecha inicio:"></asp:Label>
                                            </td>
                                            <td style="width: 200px">
                                                <asp:Label runat="server" ID="lblFecInicio" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" Text="Fecha Fin:"></asp:Label>
                                            </td>
                                            <td style="width: 200px">
                                                <asp:Label runat="server" ID="lblFechaFin" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel runat="server" ID="pnlAgregarAnexo" Visible="False">
                        <table>
                            <tr style="height: 60px">
                                <td style="width: 160px"></td>
                                <td style="width: 130px">
                                    <asp:Label runat="server" Text="AGREGAR ANEXOS" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr style="height: 30px">
                                <td style="width: 160px"></td>
                                <td style="text-align: right">
                                    <asp:Label runat="server" Text="Anexo:"></asp:Label>
                                </td>
                                <td>
                                    <ig:WebCurrencyEditor ID="WebCurrencyEditor1" runat="server" Width="150px">
                                    </ig:WebCurrencyEditor>
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
                                <td>
                                    <asp:Button runat="server" ID="btnNuevo" Text="Nuevo" Font-Names="Verdana" Font-Size="8pt" Width="80px" />
                                </td>

                            </tr>
                        </table>
                    </asp:Panel>

                    <asp:UpdatePanel runat="server" ID="pnlgvAnexos">
                        <ContentTemplate>
                            <table>
                                <tr style="height: 30px">
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="width: 300px"></td>
                                    <td>
                                        <asp:GridView ID="gvAnexos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px" DataKeyNames="id_ms_anexo">
                                            <Columns>
                                                <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Arrendadora" HeaderText="Arrendadora">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="anexo" HeaderText="Anexo">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="tipo_arrendamiento" HeaderText="Tipo Arrendamiento">
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="fecha_inicio" HeaderText="Fecha incio">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="fecha_fin" HeaderText="Fecha fin">
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
                                    <td style="width: 300px"></td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Panel ID="pnlEquipos" runat="server">
                        <table>
                            <tr>
                                <td style="width: 160px; height: 60px"></td>
                                <td style="width: 130px">
                                    <asp:Label runat="server" Text="EQUIPOS" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="text-align: right; width: 800px">
                                    <asp:Button runat="server" Text="Importar" Font-Size="8pt" Font-Names="Verdana" />
                                    &nbsp; &nbsp;
                                <asp:Button runat="server" Text="Exportar" Font-Size="8pt" Font-Names="Verdana" />
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <asp:Panel runat="server" ID="pnlDetalleAnexos">
                                        <table>
                                            <tr>
                                                <td style="width: 290px"></td>

                                                <td>
                                                    <asp:GridView ID="gvDetalleAnexo" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="800px" DataKeyNames="id_ms_anexo">
                                                        <Columns>

                                                            <asp:BoundField DataField="unidad" HeaderText="No. de Unidad">
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="mes_01" HeaderText="Mes">
                                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="mes_02" HeaderText="Mes">
                                                                <ItemStyle HorizontalAlign="Center" Width="140px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="mes_03" HeaderText="Mes">
                                                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="mes_04" HeaderText="Mes">
                                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="mes_05" HeaderText="Mes">
                                                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="mes_06" HeaderText="Mes">
                                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
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
                    <asp:Panel ID="pnlFacturas" runat="server">
                        <table>
                            <tr style="height: 40px">
                                <td style="width: 160px; height: 60px"></td>
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
                                <td style="width: 200px">
                                    <asp:Label runat="server" ID="lblProveedor" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td style="width: 290px; text-align: right"></td>
                                <td>
                                    <asp:GridView ID="gvFacturasProveedor" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="700px" DataKeyNames="id_dt_factura">
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
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
