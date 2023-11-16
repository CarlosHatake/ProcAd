<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="ProcAd.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link rel="shortcut icon" href="images\ProcAd.ico" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>ProcAd</title>
    <style type="text/css">
        .auto-style2 {
            width: 84px;
        }
        .auto-style3 {
            width: 159px;
        }
        .auto-style4 {
            height: 23px;
        }
        #form1 {
            width: 1366px;
        }
    </style>
    <script type="text/javascript">
        window.history.forward();
        function noBack() { window.history.forward(); }
    </script>
</head>
<body onload="noBack();" onpageshow="if (event.persisted) noBack();" onunload="">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblTitulo1" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="12pt" ForeColor="White" style="z-index: 2; left: 587px; top: 233px; position: absolute" Text="Sistema de"></asp:Label>
        <asp:Label ID="lblTitulo2" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="12pt" ForeColor="White" style="z-index: 2; left: 644px; top: 255px; position: absolute" Text="Procesos Administrativos"></asp:Label>
        <table style="width: 1366px;">
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4" style="text-align: center; font-family: Verdana; font-size: 8pt; color: #FF0000;">
                    <asp:Literal ID="litError" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
        </table>
    
    </div>
        <asp:Image ID="Image1" runat="server" ImageUrl="images\LU.jpg" style="z-index: 1; left: 484px; top: 219px; position: absolute" />
        <table style="width: 256px; z-index: 1; left: 564px; top: 334px; position: absolute; height: 71px;">
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lblUsuario" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" Text="Usuario:"></asp:Label>
                </td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtUsuario" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="140px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lblPass" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" Text="Contraseña:"></asp:Label>
                </td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtPass" runat="server" Font-Names="Verdana" Font-Size="8pt" TextMode="Password" Width="140px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnAceptar" runat="server" Font-Names="Verdana" Font-Size="8pt" style="z-index: 1; left: 628px; top: 438px; position: absolute; width: 129px" Text="Aceptar" />
        <table style="width: 256px; z-index: 1; left: 564px; top: 480px; position: absolute; height: 21px;">
            <tr>
                <td style="padding: 0px 0px 4px 0px; margin: 0px; border-width: 0px; text-align: center;">
                    <asp:Button ID="btnConUsuario" runat="server" BackColor="White" BorderStyle="None" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="9pt" Font-Underline="True" Text="Restablecer Contraseña" Width="128px" ForeColor="Blue" Visible="False" />
                </td>
            </tr>
         </table>
    </form>
</body>
</html>
