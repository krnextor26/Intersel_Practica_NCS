<%@ Page Language="C#" MasterPageFile="~/Vista/vMasterModulo.Master" AutoEventWireup="true" CodeBehind="vConsulta.aspx.cs" Inherits="Web_INTERTEL.Vista.vConsulta" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" runat="server">
    <asp:HiddenField ID="HfMobileLine" runat="server" Value ="0" />
    <div id="dvConsultar" runat="server" Visible ="true">            
            <asp:GridView ID="gvInforme" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="MobileLine" CellPadding="3" ForeColor="#333333" PageSize="15"  
                Visible="true" OnPageIndexChanging="gvInforme_PageIndexChanging" OnRowCommand="gvInforme_RowCommand" Width="100%" Height="70%">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="MobileLineId" HeaderText="MobileLineId" SortExpression="MobileLineId" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Visible="false"/>
                                <asp:BoundField DataField="MobileLine" HeaderText="Celular" SortExpression="MobileLine" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>                             
                                <asp:BoundField DataField="Description" HeaderText="Descripción" SortExpression="Description" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="Estatus" HeaderText="Estatus" SortExpression="Estatus" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="Usuario" HeaderText="Usuario Alta" SortExpression="Usuario" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="FechaAlta" HeaderText="Fecha Alta" SortExpression="FechaAlta" HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>                                
                                        <asp:LinkButton runat="server" ID="btnVer" class="btn btn-success btn-sm" CommandName="Ver" CommandArgument='<%# Eval("MobileLine") %>'>Ver Detalles</asp:LinkButton>    
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
    <div id="dvDetalle" runat="server" Visible ="false">            
            <asp:GridView ID="gvDetalles" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="3" ForeColor="#333333" PageSize="15"  
                Visible="true" OnPageIndexChanging="gvDetalles_PageIndexChanging" Width="100%">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="MobileLineId" HeaderText="MobileLineId" SortExpression="MobileLineId" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Visible="false"/>
                                <asp:BoundField DataField="MobileLine" HeaderText="Celular" SortExpression="MobileLine" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>                             
                                <asp:BoundField DataField="Description" HeaderText="Descripción" SortExpression="Description" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="CalledPartyNumber" HeaderText="Número Marcado" SortExpression="CalledPartyNumber" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="CalledPartyDescription" HeaderText="Lugar" SortExpression="CalledPartyDescription" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="FechaAlta" HeaderText="Fecha Hora" SortExpression="FechaAlta" HtmlEncode="false" DataFormatString="{0:MM-dd-yyyy hh:mm tt}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="Duration" HeaderText="Duración" SortExpression="Duration" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="TotalCost" HeaderText="Costo Total" SortExpression="TotalCost" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="Estatus" HeaderText="Estatus" SortExpression="Estatus" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="NombreCompleto" HeaderText="Usuario Alta" SortExpression="NombreCompleto" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
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
        <br />
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" class="btn btn-success btn-sm" OnClick="btnRegresar_Click"/> 
        </div>
        
</asp:Content>
