﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="Anexos.aspx.vb" Inherits="ProcAd.Anexos" %>

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
                <asp:TextBox ID="_txtNombreArchivo" runat="server" Width="15px" Visible="False"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsContrato" runat="server" Width="15px" Visible="False"></asp:TextBox>
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
                            <td style="font-family: Verdana; font-size: 8pt;">
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
                <asp:Panel runat="server" ID="pnlCargarArchivo">
                    <table>
                        <tr>
                            <td style="width: 100px"></td>
                        </tr>
                        <tr>
                            <td style="width: 100px"></td>
                            <td style="font-family: Verdana; font-size: 8pt;">
                                <asp:Label ID="lbl_cargaArchivo" runat="server" Text="Cargar archivo:"></asp:Label>
                            </td>
                            <td>
                                <asp:FileUpload runat="server" ID="fuArchivo" Font-Size="8pt" Font-Names="Verdana" />
                            </td>
                            <td>&nbsp;&nbsp;
                                <asp:Button runat="server" ID="btnValidar" Text="Validar archivo" Font-Size="8pt" Font-Names="Verdana" />
                            </td>
                            <td style="width:500px; text-align:right">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px"></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="width:75%"></td>
                            <td>
                               <asp:HyperLink ID="hlPlantilla" runat="server" Font-Names="Verdana" Font-Size="8pt">Plantilla</asp:HyperLink>
                            </td>
                            <td>
                               <asp:Button runat="server" ID="btnGenerarPlantilla" Text="Generar plantilla" Font-Size="8pt" Font-Names="Verdana"/>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="width: 200px"></td>

                            <td style="font-family: Verdana; font-size: 8pt;">
                                <asp:Label ID="lbl_Registrados" runat="server" Text="Registrados:" Font-Names="Verdana" Font-Size="8pt"></asp:Label>
                            </td>
                            <td style="font-family: Verdana; font-size: 8pt;">
                                <asp:Label runat="server" ID="lblRegistrados" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 200px"></td>

                            <td style="text-align: right">
                                <asp:Label ID="lbl_Omitidos" runat="server" Text="Omitidos:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblOmitidos" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="width: 100px"></td>
                            <td>
                                <asp:GridView ID="gvRegistros" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1200px" >
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True">
                                            <ItemStyle Width="15px" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="contrato" HeaderText="Contrato">
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="anexo" HeaderText="Anexo">
                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="arrendadora" HeaderText="Arrendado">
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="serie" HeaderText="Serie">
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo de arrendamiento" HeaderText="Tipo de arrendamiento">
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="plazo" HeaderText="Periodo">
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="inicio" HeaderText="Fecha inicio" DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="terminacion" HeaderText="Fecha fin" DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                        </asp:BoundField>
                                        <%--
                                        <asp:BoundField DataField="mes_año" HeaderText="Mes y año">
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:BoundField
                                        </asp:BoundField
                                        --%>
                                        <asp:BoundField DataField="inversion s/iva" HeaderText="Monto" DataFormatString="{0:c}">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
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
                            <td style="width: 600px"></td>
                            <td>
                                <asp:Button ID="btnGuardarArchivo" runat="server" Text="Guardar" Font-Size="8pt" Font-Names="Verdana" Width="150px" OnClientClick="this.disabled = true; this.value='Procesando…';" UseSubmitBehavior="false" />
                            </td>
                            <td style="width: 600px"></td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="pnlCargaManual">
                    <table>
                        <tr style="height: 30px">
                            <td style="width: 160px"></td>
                            <td style="text-align: right">
                                <asp:Label runat="server" Text="Empresa:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlEmpresa" Width="150px"></asp:DropDownList>
                            </td>
                            <td style="width: 50px"></td>
                            <td style="text-align: right">
                                <asp:Label runat="server" Text="Contrato:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtContrato" Width="150px"></asp:TextBox>
                            </td>
                            <td style="width: 50px"></td>
                            <td style="text-align: right">
                                <asp:Label runat="server" Text="Anexo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtAnexo" Width="150px"></asp:TextBox>
                            </td>
                            <td style="width: 160px"></td>

                        </tr>
                        <tr style="height: 30px">
                            <td style="width: 160px"></td>
                            <td style="text-align: right">
                                <asp:Label runat="server" Text="Arrendado:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtArrendado" Width="150px"></asp:TextBox>
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
                                <asp:Button ID="btnGuardarManual" runat="server" Text="Guardar" Font-Size="8pt" Font-Names="Verdana" Width="150px" OnClientClick="this.disabled = true; this.value='Procesando…';" UseSubmitBehavior="false" />
                            </td>
                            <td style="width: 600px"></td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>