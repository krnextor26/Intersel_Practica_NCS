<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Web_INTERTEL.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <%--<link rel="shortcut icon" type="image/x-icon" href="imagenes/iconoSC.ICO" />--%>
    <link type="text/css" rel="stylesheet" href="Content/BOOTSTRAP/css/bootstrap.css" />
    <link rel="stylesheet" href="Content/Sweetalert/dist/sweetalert.css" />
    <link rel="stylesheet" href="Css/Login.css" />    
    <script src="Content/BOOTSTRAP/js/jquery.js"></script>
    <script src="Content/Sweetalert/dist/sweetalert.min.js"></script>
    <title>Login</title>
</head>

<body>
     <!--FONDO-->
    <img alt="" runat="server" id="imgFondoLogin" src="~/imagenes/fondo.png"/>
    <%--<img alt="" runat="server" id="plecaLogin" src="~/imagenes/logoSC_New.png"/>--%>
    <form id="form1" runat="server">
    <div>
    <span id="icoUser" class="glyphicon glyphicon-user"></span>
    <span id="icoPass" class="glyphicon glyphicon-lock"></span>
    <asp:TextBox ID="User" runat="server" PlaceHolder="Usuario" CssClass="txtControl" ></asp:TextBox>
    <asp:TextBox ID="Pass" TextMode="Password" runat="server" PlaceHolder="Contraseña" CssClass="txtControl"></asp:TextBox>
        <br />
    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
    <asp:Button ID="btnEntrar" runat="server" Text="Entrar" OnClick="btnEntrar_Click" />
    </div>
    <!--PIE DE PÁGINA-->
    <div class="footer">
    <p>V1.0.0</p>
    </div>
    </form>
</body>
</html>
