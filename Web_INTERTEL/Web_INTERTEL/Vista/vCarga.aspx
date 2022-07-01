<%@ Page Language="C#" MasterPageFile="~/Vista/vMasterModulo.Master" AutoEventWireup="true" CodeBehind="vCarga.aspx.cs" Inherits="Web_INTERTEL.Vista.vCarga" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" runat="server">
        <div>
        <center>
          <table>
             <tr><td colspan="5"><h3>CARGA DE ARCHIVO</h3></td></tr>
             <tr>
                 <td>
                     <asp:Label ID="lblSeleccion" runat="server" CssClass="form-control" Text="Seleccione el archivo a cargar: "></asp:Label>
                 </td>
                 <td>
                     <asp:FileUpload ID="fupCargaArchivo" runat="server" CssClass="form-control"/>
                 </td>
                <td>
                    <asp:Label ID="lblTipo" runat="server" CssClass="form-control" Text="Seleccione tipo de archivo a cargar: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTipoArchivo" runat="server" CssClass="form-control">
                        <asp:ListItem Value="0">-- Selecciona el un tipo de archivo --</asp:ListItem>
                        <asp:ListItem Value="1">Líneas Telefónicas</asp:ListItem>
                        <asp:ListItem Value="2">Detalle de Líneas Telefónicas</asp:ListItem>
                    </asp:DropDownList> 
                </td>
                 <td>
                    <asp:Button ID="btnCargaArchivo" runat="server" CssClass="btn btn-success btn-sm" Text="Cargar Archivo" onclick="btnCargaArchivo_Click" />
                </td>
             </tr>
             </table>
        </center>


    </div>
</asp:Content>