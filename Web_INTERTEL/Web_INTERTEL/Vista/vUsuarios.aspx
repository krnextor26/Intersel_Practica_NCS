<%@ Page Language="C#" MasterPageFile="~/Vista/vMasterModulo.Master" AutoEventWireup="true" CodeBehind="vUsuarios.aspx.cs" Inherits="Web_INTERTEL.Vista.vUsuarios" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script language="javascript" type="text/javascript">
        function validaForm() {
            var mensaje = "";

            if ($('#cphContenido_txtUsuario').val() == '')
            {
                mensaje += "Capture el campo de usuario.\n\r";
            }
            if ($('#cphContenido_txtContra').val() == '') {
                mensaje += "Capture el campo de contraseña.\n\r";
            }
            if ($('#cphContenido_txtConfirma').val() == '') {
                mensaje += "Capture el campo de confirma contraseña.\n\r";
            }
            if ($('#cphContenido_txtNombre').val() == '') {
                mensaje += "Capture el campo de nombre.\n\r";
            }
            if ($('#cphContenido_txtTelefono').val() == '') {
                mensaje += "Capture el campo de teléfono.\n\r";
            }

            if ($('#cphContenido_ddlRol').val() == '0') {
                mensaje += "Seleccione un rol.\n\r";
            }
            if ($('#cphContenido_ddlTipoUsuario').val() == '0') {
                mensaje += "Seleccione un tipo de usuario.\n\r";
            }

            if (mensaje == "") {
                var contra = $('#cphContenido_txtContra').val();
                var confirma = $('#cphContenido_txtConfirma').val();


                if (contra != confirma) {
                    mensaje += "El valor de la contraseña y su confirmación no son iguales. Verifique \n\r";
                }

            }
            if (mensaje != "") {
                swal("Revisa los campos requeridos.", mensaje, "warning");
                return false;
            }
            else { return true; }

        }
    </script>

    <asp:Panel ID="Panel1" runat="server" Visible="true" CssClass="container-fluid">
        <div class="row">
            <div class="col-sm-6" style="text-align:justify">
                <asp:Button ID="btnInsertar" runat="server" Text="Insertar" CssClass="btn btn-success btn-sm" OnClick="btnInsertar_Click"/>
            </div>
            <div class="col-sm-6" style="text-align:justify">
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="btn btn-success btn-sm" OnClick="btnConsultar_Click"/>
            </div>
        </div>
    </asp:Panel>

     <div id="dvInsertar" runat="server" visible="false">            
            <table  width="100%" align="center" >
                <tr style="background-color: #092E86 ;color: white; height:25px; font-weight: bold;"><td colspan="6" class="auto-style2" align="center">Datos</td> </tr>
                <tr>
                    <td><asp:Label  runat="server" Text="Usuario:" ForeColor="CornflowerBlue" Font-Bold="true"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtUsuario" runat="server"  MaxLength="30" CssClass="form-control texto-espacio" ></asp:TextBox>
                    </td>
                    <td><asp:Label  runat="server" Text="Contraseña:" ForeColor="CornflowerBlue" Font-Bold="true"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtContra" runat="server" TextMode="Password"  MaxLength="30" CssClass="form-control texto-espacio" ></asp:TextBox>
                    </td>
                    <td><asp:Label  runat="server" Text="Confirma Contraseña:" ForeColor="CornflowerBlue" Font-Bold="true"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtConfirma" runat="server" TextMode="Password"  MaxLength="30" CssClass="form-control texto-espacio" ></asp:TextBox>
                    </td>
                    
                </tr>
                <tr>
                    <td><asp:Label  runat="server" Text="Nombre Completo:" ForeColor="CornflowerBlue" Font-Bold="true"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNombre" runat="server"  MaxLength="100" Width="100%"  CssClass="form-control texto-espacio" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label  runat="server" Text="Teléfono:" ForeColor="CornflowerBlue" Font-Bold="true"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTelefono" runat="server"  MaxLength="10" Width="100%"  CssClass="form-control texto-espacio" ></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="ftxtNumeroTelefono" runat="server"  TargetControlID="txtTelefono" ValidChars="1234567890" ></asp:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:Label  runat="server" Text="Tipo Usuario:" ForeColor="CornflowerBlue" Font-Bold="true"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-control"></asp:DropDownList> 
                    </td>
                    
                </tr>
                <tr>
                    <td>
                        <asp:Label  runat="server" Text="Rol:" ForeColor="CornflowerBlue" Font-Bold="true"></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control"></asp:DropDownList> 
                    </td>
                    <td>
                         &nbsp;
                    </td>
                    <td>
                         &nbsp;
                    </td>
                    <td>
                         &nbsp;
                    </td>
                    <td>
                         &nbsp;
                    </td>
                    
                </tr>
                <tr>
                    <td colspan="6" class="auto-style2" align="center">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="btn btn-primary" OnClientClick="return validaForm();" OnClick="btnGuardar_Click"/>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="HfIdUsuario" runat="server" Value ="0" />
        <div id="dvUsuarios" runat="server" Visible ="true">            
                <asp:GridView ID="gvUsuario" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="UsuarioID" CellPadding="3" ForeColor="#333333" PageSize="15"  
                    Visible="true" OnPageIndexChanging="gvUsuario_PageIndexChanging" OnRowCommand="gvUsuario_RowCommand" Width="100%">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="UsuarioID" HeaderText="UsuarioID" SortExpression="UsuarioID" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Visible="false"/>
                                    <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>                             
                                    <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo" SortExpression="NombreCompleto" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" SortExpression="Telefono" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="TipoUsuario" HeaderText="Tipo Usuario" SortExpression="TipoUsuario" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="Rol" HeaderText="Rol" SortExpression="Rol" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="Estatus" HeaderText="Estatus" SortExpression="Estatus" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="FechaAlta" HeaderText="Fecha Alta" SortExpression="FechaAlta" HtmlEncode="false" DataFormatString="{0:MM-dd-yyyy hh:mm tt}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>                                
                                            <asp:LinkButton runat="server" ID="btnActualizar" class="btn btn-success btn-sm" CommandName="Actualizar" CommandArgument='<%# Eval("UsuarioID") %>'>Actualizar</asp:LinkButton>    
                                        </ItemTemplate>                           
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>                                
                                            <asp:LinkButton runat="server" ID="btnEliminar" class="btn btn-success btn-sm" CommandName="Eliminar" CommandArgument='<%# Eval("UsuarioID") %>'>Eliminar</asp:LinkButton>    
                                        </ItemTemplate>                           
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                         </asp:GridView>
            </div>
        
</asp:Content>
