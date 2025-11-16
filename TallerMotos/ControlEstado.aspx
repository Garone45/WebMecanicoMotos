<%@ Page Title="Control de Flujo de Reparación" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ControlEstado.aspx.cs" Inherits="TallerMotos.ControlEstado" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Control de Estado de Órdenes</h2>
    <p>Utilice este formulario para mover una reparación a la siguiente fase del taller.</p>
    <hr />

    <div class="form-group">
        <label for="txtReparacionId">ID de Reparación a Modificar:</label>
        <asp:TextBox ID="txtReparacionId" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label for="ddlNuevoEstado">Nuevo Estado:</label>
        <asp:DropDownList ID="ddlNuevoEstado" runat="server" CssClass="form-control">
            </asp:DropDownList>
    </div>

    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar Estado" OnClick="btnActualizar_Click" CssClass="btn btn-primary" />
    
    <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" CssClass="mt-3" />
</asp:Content>