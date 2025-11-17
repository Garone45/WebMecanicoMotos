<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CostoxServicio.aspx.cs" Inherits="TallerMotos.CostoxServicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <h2>Actualizar costo x servicio</h2>
  <p>Utilice este formulario para actualizar el costo x servicio .</p>
  <hr />

  <div class="form-group">
      <label for="txtDetalleId">ID del detalle de Reparacion:</label>
      <asp:TextBox ID="txtDetalleId" runat="server" CssClass="form-control" />
  </div>

  <div class="form-group">
      <label for="ddlNuevoCostoxServicio">Nuevo costo:</label>
      <asp:TextBox ID="txtCostoxServicio" runat="server" CssClass="form-control" />
  </div>

  <asp:Button ID="btnActualizar" runat="server" Text="Actualizar costo x servico" OnClick="btnActualizar_Click"  CssClass="btn btn-primary" />
  
  <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" CssClass="mt-3" />
</asp:Content>
