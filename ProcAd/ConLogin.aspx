<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ConLogin.aspx.vb" Inherits="ProcAd.ConLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link rel="shortcut icon" href="images\ProcAd.ico" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Recuperacion de Usuario</title>
  <style type="text/css">
        #form1 {
            width: 1360px;
        }
        .auto-style4 {
            height: 23px;
        }
              
       .auto-style29 {
            width: 963px;
        }
        .auto-style37 {
            width: 330px;
            height: 17px;
        }
        .auto-style42 {
          width: 100%;
      }
        </style>
</head>
<body>
    <form id="form1" runat="server">

    <div>
        <table style="width: 1360px; font-family: Verdana; font-size: 8pt;">
            <tr>
                <td style="text-align: center">
                    <table style="width:100%;">
                        <tr>
                            <td style="width: 200px; text-align: center">&nbsp;</td>
                            <td>
                <asp:Label ID="lblTitulo" runat="server" Text="Recuperacion de Usuario Y Contraseña" Font-Bold="True" Font-Names="Verdana" Font-Size="12pt"></asp:Label>
                            </td>
                            <td style="width: 200px; text-align: center">
                 <asp:Button ID="btnMenu" runat="server" BackColor="White" BorderStyle="None" Font-Bold="True" Font-Italic="False" Font-Names="Verdana" Font-Overline="False" Font-Size="9pt"  Text="Regresar a Login" Width="128px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="auto-style4" style="text-align: center; font-family: Verdana; font-size: 8pt; color: #FF0000;">
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlGrid" runat="server">
                        <table style="text-align: center; " class="auto-style42">
                            <tr>
                                <td style="width: 560px; text-align: right;">
                                    <asp:Label ID="lblCorreo" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" Text="Correo:"></asp:Label>
                                 </td>
                                <td style="text-align: left; width: 245px;" >
                                    <asp:TextBox ID="txtCorreo" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="230px"></asp:TextBox>
                                </td>
                                <td style="text-align: left" >
                                    <asp:ImageButton ID="ibtnCorreo" runat="server" ImageUrl="images\Search.png" ToolTip="Buscar" Width="20px" />
                                 </td>
                            </tr>
                        </table>
                      
                    </asp:Panel>
                   
                </td>
            </tr>
            <tr>
                <td class="auto-style4">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                <table style="width:100%;">
                <tr>
                <td style="text-align: center; width: 450px;">&nbsp;</td>
                <td class="auto-style29">
                    <asp:GridView ID="gvEmp" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="Vertical" Width="510px" Font-Bold="False" Font-Italic="False">
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <Columns >
                            <asp:BoundField DataField="id_usuario" HeaderText="N°">
                             <ItemStyle Width="10px" />
                            </asp:BoundField>
                            <asp:CommandField ButtonType="Image" SelectImageUrl="images\ok.png" SelectText="" ShowSelectButton="True">
                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                            </asp:CommandField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" >
                           <ItemStyle HorizontalAlign="Left" Width="160px" />
                            </asp:BoundField>
                             <asp:BoundField DataField="ap_paterno" HeaderText="Apellido Paterno">
                             <ItemStyle HorizontalAlign="Left" Width="160px" />
                             </asp:BoundField>
                            <asp:BoundField DataField="ap_materno" HeaderText="Apellido Materno" >
                            <ItemStyle HorizontalAlign="Left" Width="160px" />
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
                </td>
            </tr>
        </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel1" runat="server">
                        <table style="text-align: center; width: 100%;">
                            <tr>
                                
                                <td style="width: 505px; height: 40px;">
                                 </td>
                                <td style="text-align: left; width:300px" >
                                    <asp:Label ID="lbl_Nomb" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Size="8pt" Text="Enviar a:" Visible="False"></asp:Label>
                                    <asp:Label ID="lblNomb" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" Text="*" Visible="False"></asp:Label>
                                </td>
                                <td style="text-align: left" >
                                    <asp:ImageButton ID="ibtnEnviar" runat="server" Height="19px" ImageUrl="images\Send_2.png" ToolTip="Enviar" Visible="False" />
                                 </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    
    </div>        
    </form>
</body>
</html>