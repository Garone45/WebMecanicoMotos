<%@ Page Title="Registrar Nueva Orden" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevaOrden.aspx.cs" Inherits="TallerMotos.NuevaOrden" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Registro de Nueva Orden de Reparación</h2>
    <p>Seleccione el cliente, su moto y el servicio a realizar.</p>
    <hr />

    <div class="row">
        <div class="col-md-6">
            <h4 class="mb-3">🏍️ Datos de la Moto y Propietario</h4>

            <div class="form-group mb-3">
                <label for="ddlDniCliente">Cliente (DNI):</label>
                <asp:DropDownList ID="ddlDniCliente" runat="server" 
                    CssClass="form-control" 
                    AutoPostBack="true" 
                    OnSelectedIndexChanged="ddlDniCliente_SelectedIndexChanged" 
                    required />
            </div>

            <div class="form-group mb-3">
                <label for="ddlPatente">Patente de la Moto:</label>
                <asp:DropDownList ID="ddlPatente" runat="server" CssClass="form-control" required />
            </div>

            </div>

        <div class="col-md-6">
            <h4 class="mb-3">⚙️ Detalle de la Orden y Servicio</h4>

            <div class="form-group mb-3">
                <label for="ddlMecanico">Mecánico Asignado (Principal y Ejecutor):</label>
                <asp:DropDownList ID="ddlMecanico" runat="server" CssClass="form-control" required />
            </div>

            <div class="form-group mb-3">
                <label for="ddlServicioDesc">Servicio a Realizar (Catálogo):</label>
                <asp:DropDownList ID="ddlServicioDesc" runat="server" CssClass="form-control" required />
            </div>

            <div class="form-group mb-3">
                <label for="txtDescripcion">Descripción (Problema reportado / Notas):</label>
                <asp:TextBox ID="txtDescripcion" runat="server" 
                    CssClass="form-control" 
                    TextMode="MultiLine" 
                    Rows="3" 
                    placeholder="Ej: El cliente reporta un ruido al frenar. Revisar también nivel de aceite." />
            </div>
            <div class="form-group mb-3">
                <label for="txtCostoServicio">Costo Acordado del Servicio:</label>
                <asp:TextBox ID="txtCostoServicio" runat="server" CssClass="form-control" placeholder="Ej: 12000.00" required />
            </div>
            
        </div>
    </div>

    <hr />
    
    <asp:Button ID="btnRegistrar" runat="server" Text="Registrar Orden Completa" OnClick="btnRegistrar_Click" CssClass="btn btn-success btn-lg" />
    
    <div class="mt-3">
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="false" />
    </div>

</asp:Content>