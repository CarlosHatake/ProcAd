<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pgMaestra.Master" CodeBehind="ConsReunion.aspx.vb" Inherits="ProcAd.ConsReunion" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics45.Web.v21.2, Version=21.2.20212.9, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style12 {
            width: 1360px;
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
        .auto-style5 {
            width: 150px;
            height: 21px;
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
                <asp:TextBox ID="_txtIdUsuario" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="False" Width="10px"></asp:TextBox>
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
                                <asp:CheckBox ID="cbFecha" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Fechas de Reunión:" />
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
                                <asp:CheckBox ID="cbGrupo" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="Grupo:" />
                            </td>
                            <td class="auto-style19">
                                <asp:Panel ID="pnlGrupo" runat="server">
                                    <asp:DropDownList ID="ddlGrupo" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="350px">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                            <td class="auto-style45">
                                <asp:CheckBox ID="cbNoReunion" runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="8pt" Text="No. de Reunión:" />
                            </td>
                            <td>
                                <asp:Panel ID="pnlNoReunion" runat="server">
                                    <asp:TextBox ID="txtNoReunion" runat="server" Font-Names="Verdana" Font-Size="8pt"></asp:TextBox>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style43" style="text-align: right"></td>
                            <td class="auto-style45" style="text-align: left">
                                &nbsp;</td>
                            <td class="auto-style19">
                                &nbsp;</td>
                            <td class="auto-style45">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
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
                                <asp:GridView ID="gvReuniones" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1450px" Font-Names="Verdana" Font-Size="8pt">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" ShowSelectButton="True" >
                                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="id_ms_reunion" HeaderText="No.">
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="grupo" HeaderText="Grupo" />
                                        <asp:BoundField DataField="tema" HeaderText="Tema" />
                                        <asp:BoundField DataField="lider" HeaderText="Líder" />
                                        <asp:BoundField DataField="secretario" HeaderText="Secretario" />
                                        <asp:BoundField DataField="fecha_reunion" HeaderText="Fecha Reunión">
                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_inicio" HeaderText="Fecha Inicio">
                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_cierre" HeaderText="Fecha Término">
                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="calif_gral" HeaderText="Calificación">
                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
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
                                <asp:GridView ID="gvReunionesT" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Visible="False" Width="1000px">
                                    <Columns>
                                        <asp:BoundField DataField="id_ms_reunion" HeaderText="No.">
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="grupo" HeaderText="Grupo" />
                                        <asp:BoundField DataField="tema" HeaderText="Tema" />
                                        <asp:BoundField DataField="lider" HeaderText="Líder" />
                                        <asp:BoundField DataField="secretario" HeaderText="Secretario" />
                                        <asp:BoundField DataField="fecha_reunion" HeaderText="Fecha Reunión">
                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_inicio" HeaderText="Fecha Inicio">
                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_cierre" HeaderText="Fecha Término">
                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="calif_gral" HeaderText="Calificación">
                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
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
                            <td>
                                <asp:Button ID="btnNuevoBus" runat="server" Text="Nueva Búsqueda" Width="150px" Font-Names="Verdana" Font-Size="8pt" />
                            </td>
                            <td style="text-align: right; width: 138px;">
                                <table style="width:138px;">
                                    <tr>
                                        <td style="border: thin outset #808000; text-align: center; width: 138px;">
                                            <table style="width:100%; font-family: Verdana; font-size: 8pt;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbl_NoReunion" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" Text="No. Reunión"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblNoReunion" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#990000" Width="98px"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="auto-style78" style="text-align: center"></td>
                        </tr>
                        <tr>
                            <td style="height: 5px">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <table style="width: 1360px; height: 20px;">
                        <tr>
                            <td class="auto-style5" style="text-align: right">
                                <asp:Label ID="lbl_Tema" runat="server" Text="Tema:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTema" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: right">
                                <asp:Label ID="lbl_Grupo" runat="server" Text="Grupo:"></asp:Label>
                            </td>
                            <td style="width: 350px">
                                <asp:Label ID="lblGrupo" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 50px;">
                                <asp:Label ID="lbl_Fecha0" runat="server" Text="Fecha:"></asp:Label>
                            </td>
                            <td style="width: 250px">
                                <asp:Label ID="lblFecha" runat="server" ForeColor="Blue" Width="230px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 1360px;">
                        <tr>
                            <td style="width: 130px; ">&nbsp;</td>
                            <td style="width: 460px; text-align: center">
                                <asp:Label ID="lbl_Asistencia" runat="server" Font-Bold="True" Text="Asistencia" Width="200px"></asp:Label>
                            </td>
                            <td style="width: 170px">&nbsp;</td>
                            <td style="text-align: center; width: 460px;">
                                <asp:Label ID="lbl_Participacion" runat="server" Font-Bold="True" Text="Participación" Width="200px"></asp:Label>
                            </td>
                            <td style="text-align: center;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td style="text-align: center">
                                <asp:GridView ID="gvAsistencia" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="450px">
                                    <Columns>
                                        <asp:BoundField DataField="id_dt_reunion" HeaderText="id_dt_reunion" />
                                        <asp:BoundField DataField="integrante" HeaderText="Integrante">
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="llegada_hora" HeaderText="Llegada">
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
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
                            <td>&nbsp;</td>
                            <td style="text-align: center;">
                                <asp:GridView ID="gvParticipacion" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="450px">
                                    <Columns>
                                        <asp:BoundField DataField="id_dt_reunion" HeaderText="id_dt_reunion" />
                                        <asp:BoundField DataField="integrante" HeaderText="Integrante">
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="particip_pos" HeaderText="Aportación">
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
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
                            <td style="text-align: center;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td style="text-align: center">&nbsp;</td>
                            <td style="text-align: center">&nbsp;</td>
                            <td style="text-align: center;">&nbsp;</td>
                            <td style="text-align: center;">&nbsp;</td>
                        </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td style="width: 350px;">&nbsp;</td>
                            <td style="width: 660px; text-align: center;">
                                <asp:Label ID="lbl_Actividad" runat="server" Font-Bold="True" Text="Actividades" Width="200px"></asp:Label>
                            </td>
                            <td style="vertical-align: top">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 350px;">&nbsp;</td>
                            <td style="width: 660px">
                                <asp:GridView ID="gvActividad" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="650px">
                                    <Columns>
                                        <asp:BoundField DataField="id_dt_reunion" HeaderText="id_dt_reunion" />
                                        <asp:BoundField DataField="integrante" HeaderText="Integrante">
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="total" HeaderText="Total">
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="et" HeaderText="ET">
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ft" HeaderText="FT">
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Calif" DataFormatString="{0:.##}" HeaderText="Calif.">
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
                            <td style="vertical-align: top">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 350px;">&nbsp;</td>
                            <td style="width: 660px">&nbsp;</td>
                            <td style="vertical-align: top">&nbsp;</td>
                        </tr>
                    </table>
                    <table style="width: 1360px;">
                        <tr>
                            <td style="width: 450px; ">&nbsp;</td>
                            <td style="width: 460px; text-align: center">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width: 280px; text-align: right;">
                                            <asp:Label ID="lbl_Calif" runat="server" Font-Bold="True" Text="Calificación General"></asp:Label>
                                        </td>
                                        <td style="width: 5px">&nbsp;</td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblCalif" runat="server" Font-Bold="True" ForeColor="Blue" Width="100px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="text-align: center;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td style="text-align: center">
                                <asp:GridView ID="gvCalificacion" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="450px">
                                    <Columns>
                                        <asp:BoundField DataField="id_dt_reunion" HeaderText="id_dt_reunion" />
                                        <asp:BoundField DataField="integrante" HeaderText="Integrante">
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="reunion_calif" HeaderText="Calificación">
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
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
                            <td style="text-align: center;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td style="text-align: center">&nbsp;</td>
                            <td style="text-align: center;">&nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        </table>
</asp:Content>
