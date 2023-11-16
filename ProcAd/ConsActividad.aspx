<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ConsActividad.aspx.vb" Inherits="ProcAd.ConsActividad" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style11 {
            width: 590px;
        }
        .auto-style12 {
            width: 1360px;
        }
        .auto-style15 {
            width: 115px;
        }
        .auto-style19 {
            width: 360px;
            height: 10px;
        }
        .auto-style43 {
            width: 200px;
            height: 10px;
        }
        .auto-style45 {
            width: 142px;
            height: 10px;
        }
        .auto-style46 {
            width: 10px;
        }
        .auto-style55 {
            width: 925px;
        }
        .auto-style63 {
            width: 75px;
            height: 25px;
        }
        .auto-style66 {
            width: 281px;
        }
        .auto-style70 {
            width: 1360px;
            height: 10px;
        }
        .auto-style78 {
            width: 20px;
            height: 10px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 1366px; font-family: Verdana; font-size: 8pt;">
        <tr>
            <td class="auto-style70" style="text-align: center; font-family: Verdana; font-size: 9px; color: #FF0000; font-weight: bold; ">
                <ig:WebScriptManager ID="wsmConsAct" runat="server">
                </ig:WebScriptManager>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:TextBox ID="_txtIdMsHist" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtBanSecre" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtIdUsuario" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="_txtIdMsAct" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style12">
                <asp:Panel ID="pnlFiltros" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td class="auto-style43" style="text-align: right"></td>
                            <td class="auto-style45" style="text-align: center">
                                <asp:Label ID="lbl_Categorias" runat="server" Font-Bold="True" Text="Habilitar Filtros" Width="142px"></asp:Label>
                            </td>
                            <td class="auto-style19">&nbsp;</td>
                            <td class="auto-style45">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style43" style="text-align: right"></td>
                            <td class="auto-style45" style="text-align: left">
                                <asp:CheckBox ID="cbFecha" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Fechas de Creación:" />
                            </td>
                            <td class="auto-style19">
                                <asp:Panel ID="pnlFecha" runat="server">
                                    <table style="width:260px;">
                                        <tr>
                                            <td style="border: medium inset #808080; margin-right: auto; margin-left: auto;">
                                                <table style="width: 127px;">
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lbl_FechaAsigI" runat="server" Text="Fecha Inicial"></asp:Label>
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
                                                <table style="width: 127px;">
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lbl_FechaAsigF" runat="server" Text="Fecha Final"></asp:Label>
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
                            <td class="auto-style45">
                                <asp:CheckBox ID="cbTema" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Tema:" Width="142px" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlTema" runat="server" Width="350px">
                                    <asp:TextBox ID="txtTema" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px"></asp:TextBox>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style43" style="text-align: right"></td>
                            <td class="auto-style45" style="text-align: left">
                                <asp:CheckBox ID="cbAsignado" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Asignado a:" />
                            </td>
                            <td class="auto-style19">
                                <asp:Panel ID="pnlAsignado" runat="server">
                                    <asp:DropDownList ID="ddlAsignado" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="250px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td class="auto-style45">
                                <asp:CheckBox ID="cbGrupo" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Grupo:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlGrupo" runat="server">
                                    <asp:DropDownList ID="ddlGrupo" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="350px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style43" style="text-align: right"></td>
                            <td class="auto-style45" style="text-align: left">
                                <asp:CheckBox ID="cbStatus" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Estatus:" />
                            </td>
                            <td class="auto-style19">
                                <asp:Panel ID="pnlStatus" runat="server">
                                    <asp:CheckBoxList ID="cblStatus" runat="server" RepeatColumns="3" Width="350px">
                                        <asp:ListItem Value="P">En Proceso</asp:ListItem>
                                        <asp:ListItem Value="C">Cerrado</asp:ListItem>
                                        <asp:ListItem Value="PU">Pendiente Usr</asp:ListItem>
                                        <asp:ListItem Value="PP">Pendiente Prov.</asp:ListItem>
                                        <asp:ListItem Value="Ca">Cancelado</asp:ListItem>
                                    </asp:CheckBoxList>
                                </asp:Panel>
                            </td>
                            <td class="auto-style45">
                                <asp:CheckBox ID="cbNoActividad" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="No. de Actividad:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlNoActividad" runat="server">
                                    <asp:TextBox ID="txtNoActividad" runat="server" Font-Names="Verdana" Font-Size="8pt"></asp:TextBox>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; text-align: center; margin-right: auto; margin-left: auto; height: 60px;">
                        <tr>
                            <td class="auto-style12">
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="150px" Font-Names="Verdana" Font-Size="8pt" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlTickets" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td class="auto-style55">
                                <table style="width: 31%; height: 49px;">
                                    <tr>
                                        <td class="auto-style66" style="text-align: center">
                                            <asp:Button ID="btnExportar" runat="server" Text="Exportar" Width="150px" Font-Names="Verdana" Font-Size="8pt" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:GridView ID="gvActividades" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1450px" Font-Names="Verdana" Font-Size="8pt">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True" >
                                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="id_ms_actividad" HeaderText="No.">
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="grupo" HeaderText="Grupo" />
                                        <asp:BoundField DataField="tema" HeaderText="Tema" />
                                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                        <asp:BoundField DataField="solicito" HeaderText="Solicitó" />
                                        <asp:BoundField DataField="responsable" HeaderText="Responsable" />
                                        <asp:BoundField DataField="fecha_registro" HeaderText="Fecha Alta">
                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_compromiso" HeaderText="Fecha Comp.">
                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_cierre" HeaderText="Fecha Cierre">
                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="estatus" HeaderText="Estatus">
                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
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
                                <asp:GridView ID="gvActividadesT" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Visible="False" Width="1000px">
                                    <Columns>
                                        <asp:BoundField DataField="id_ms_actividad" HeaderText="No.">
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="grupo" HeaderText="Grupo" />
                                        <asp:BoundField DataField="tema" HeaderText="Tema" />
                                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                        <asp:BoundField DataField="solicito" HeaderText="Solicitó" />
                                        <asp:BoundField DataField="responsable" HeaderText="Responsable" />
                                        <asp:BoundField DataField="fecha_registro" HeaderText="Fecha Alta">
                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_compromiso" HeaderText="Fecha Comp.">
                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_cierre" HeaderText="Fecha Cierre">
                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="estatus" HeaderText="Estatus">
                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
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
                    <table style="width: 1360px;">
                        <tr>
                            <td class="auto-style63"></td>
                            <td style="width: 700px">
                                <asp:Button ID="btnNuevoBus" runat="server" Text="Nueva Búsqueda" Width="150px" Font-Names="Verdana" Font-Size="8pt" />
                            </td>
                            <td style="text-align: right; width: 534px;">
                                <table style="width:534px;">
                                    <tr>
                                        <td style="border: thin outset #808000; text-align: center; width: 138px;">
                                            <table style="width:100%; font-family: Verdana; font-size: 8pt;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbl_NoActividad" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" Text="No. Actividad"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblNoActividad" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#990000" Width="98px"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="border: thin outset #808000; text-align: center; width: 188px;">
                                            <table style="width:100%; font-family: Verdana; font-size: 8pt;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbl_Fecha" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" Text="Fecha Registro"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblFecha" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Size="8pt" ForeColor="#0033CC"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="border: thin groove #808000; text-align: center; width:208px;">
                                            <table style="width:100%; font-family: Verdana; font-size: 8pt;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbl_FechaComp" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" Text="Fecha Compromiso"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblFechaComp" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#0033CC"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="text-align: center">
                                <asp:Panel ID="pnlFechaTer" runat="server">
                                    <table style="width:208px;">
                                        <tr>
                                            <td style="border: thin groove #808000; text-align: center; ">
                                                <table style="width:100%; font-family: Verdana; font-size: 8pt;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_FechaTer" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" Text="Fecha Término"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblFechaTer" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#0033CC"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td class="auto-style78" style="text-align: center"></td>
                        </tr>
                        <tr>
                            <td style="height: 5px">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <table style="width: 1360px;">
                        <tr>
                            <td style="width: 20px">
                                &nbsp;</td>
                            <td style="width: 660px">
                                <asp:Label ID="lbl_InfAct" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" Text="Información de la Actividad"></asp:Label>
                            </td>
                            <td style="width:40px;">&nbsp;</td>
                            <td>
                                <asp:Label ID="lbl_Descripcion" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Size="8pt" Text="Descripción:" Width="90px"></asp:Label>
                            </td>
                            <td style="width:20px;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 20px">&nbsp;</td>
                            <td style="border: thin outset #808000; width: 660px">
                                <table style="text-align: center; width: 650px; height: 80px;">
                                    <tr>
                                        <td style="text-align: right; width:100px;">
                                            <asp:Label ID="lbl_Grupo" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Size="8pt" Text="Grupo:"></asp:Label>
                                        </td>
                                        <td style="text-align: left; ">
                                            <asp:Label ID="lblGrupo" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Size="8pt" ForeColor="#0033CC" style="margin-bottom: 0px"></asp:Label>
                                        </td>
                                        <td style="text-align: right; width: 55px;">
                                            <asp:Label ID="lbl_Estatus" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Size="8pt" Text="Estatus:"></asp:Label>
                                        </td>
                                        <td style="text-align: left; width: 120px;">
                                            <asp:Label ID="lblEstatus" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#0033CC" style="margin-bottom: 0px" Width="115px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Label ID="lbl_Solicito" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Size="8pt" Text="Solicitó:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblSolicito" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Size="8pt" ForeColor="#0033CC"></asp:Label>
                                        </td>
                                        <td style="text-align: left">&nbsp;</td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblIdUsrReg" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Size="8pt" ForeColor="#0033CC" style="margin-bottom: 0px" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Label ID="lbl_Responsable" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Size="8pt" Text="Responsable:"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblResponsable" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Size="8pt" ForeColor="#0033CC"></asp:Label>
                                        </td>
                                        <td style="text-align: left">&nbsp;</td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblIdUsrSecre" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Size="8pt" ForeColor="#0033CC" style="margin-bottom: 0px" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width:40px;">&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtDescripcion" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="87px" MaxLength="990" ReadOnly="True" TextMode="MultiLine" Width="549px"></asp:TextBox>
                            </td>
                            <td style="width:20px;">&nbsp;</td>
                        </tr>
                    </table>
                    <table style="text-align: center; width: 1360px;">
                        <tr>
                            <td style="text-align: right; height: 5px; width: 50px;">
                                &nbsp;</td>
                            <td class="auto-style11" style="text-align: left">
                                <asp:Label ID="lbl_InfAcciones" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" Text="Acciones Realizadas"></asp:Label>
                            </td>
                            <td style="width: 150px; text-align: right; ">
                                <asp:Label ID="lbl_Adjuntos" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Size="8pt" Height="16px" Text="Archivos Adjuntos:" Width="110px"></asp:Label>
                            </td>
                            <td style="text-align: left">
                                <asp:FileUpload ID="FUAdjuntos" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="300px" />
                                &nbsp;
                                <asp:Button ID="cmdAdjuntar" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Adjuntar" Width="112px" />
                                <asp:Label ID="lblMessage" runat="server" Font-Names="Verdana" Font-Size="8pt" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">&nbsp;</td>
                            <td style="text-align: left; vertical-align: top;">
                                <asp:GridView ID="gvHistorico" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="590px">
                                    <Columns>
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha">
                                        <ItemStyle HorizontalAlign="Center" Width="75px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Empleado" HeaderText="Usuario">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="comentario" HeaderText="Comentarios" HtmlEncode="False" />
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
                            <td style="text-align: left">&nbsp;</td>
                            <td style="text-align: left; vertical-align: top;">
                                <asp:GridView ID="gvAdjuntos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" Font-Size="7pt" ForeColor="Black" GridLines="Horizontal" Width="490px">
                                    <Columns>
                                        <asp:BoundField DataField="id_ms_archivoA" HeaderText="id_ms_archivoA" />
                                        <asp:BoundField DataField="id_ms_actividad" HeaderText="id_ms_actividad" />
                                        <asp:HyperLinkField DataNavigateUrlFields="Path" DataTextField="Archivo" HeaderText="Archivo" />
                                        <asp:BoundField DataField="Archivo" HeaderText="Archivo" Visible="False" />
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
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
                    <asp:Panel ID="pnlAccion" runat="server">
                        <table style="text-align: center; height: 250px; width: 1360px;">
                            <tr>
                                <td style="width: 70px; text-align: right; height: 5px;">&nbsp;</td>
                                <td class="auto-style11" style="text-align: left">&nbsp;</td>
                                <td class="auto-style15" style="text-align: left">&nbsp;</td>
                                <td style="text-align: left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 5px;">
                                    <asp:Label ID="lbl_Accion" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Size="8pt" Text="Acción:" Width="50px"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlAccion" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="170px">
                                        <asp:ListItem>Solicitar Inf. al Usuario</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left">&nbsp;</td>
                                <td style="text-align: center">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 5px;">&nbsp;</td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtComentario" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="119px" TextMode="MultiLine" Width="590px"></asp:TextBox>
                                </td>
                                <td style="text-align: left">&nbsp;</td>
                                <td style="text-align: center">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 5px;">&nbsp;</td>
                                <td style="text-align: center; height: 35px;">
                                    <asp:Button ID="btnActualiza" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Actualizar Ticket" Width="150px" />
                                </td>
                                <td style="text-align: left">&nbsp;</td>
                                <td style="text-align: center">&nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </asp:Panel>
            </td>
        </tr>
        </table>
</asp:Content>
