<%@ Page Title="Registrar Nueva Orden" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevaOrden.aspx.cs" Inherits="TallerMotos.NuevaOrden" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Registro de Nueva Orden de Reparación</h2>
    <p>Complete los datos de la orden y del primer servicio para iniciar la transacción.</p>
    <hr />

    <div class="row">
        <div class="col-md-6">
            <h4 class="mb-3">Datos de la Orden y Propietario</h4>

            <div class="form-group mb-3">
                <label for="txtPatente">Patente de la Moto:</label>
                <asp:TextBox ID="txtPatente" runat="server" CssClass="form-control" placeholder="Ej: ABC123BB" required />
            </div>

            <div class="form-group mb-3">
                <label for="txtDniCliente">DNI del Cliente (Dueño):</label>
                <asp:TextBox ID="txtDniCliente" runat="server" CssClass="form-control" placeholder="Ej: 30300304" required />
            </div>

            <div class="form-group mb-3">
                <label for="txtMarca">Marca de la Moto:</label>
                <asp:TextBox ID="txtMarca" runat="server" CssClass="form-control" placeholder="Ej: Honda" required />
            </div>

            <div class="form-group mb-3">
                <label for="txtModelo">Modelo de la Moto:</label>
                <asp:TextBox ID="txtModelo" runat="server" CssClass="form-control" placeholder="Ej: CB 250" required />
            </div>
            
            <div class="form-group mb-3">
                <label for="ddlMecanico">Mecánico Asignado (Responsable y Ejecutor):</label>
                <asp:DropDownList ID="ddlMecanico" runat="server" CssClass="form-control" required />
            </div>

            <div class="form-group mb-3">
                <label for="txtDescripcionOrden">Descripción del Problema:</label>
                <asp:TextBox ID="txtDescripcionOrden" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" />
            </div>
        </div>

        <div class="col-md-6">
            <h4 class="mb-3">Primer Servicio (Detalle de Costo)</h4>

            <div class="form-group mb-3">
                <label for="ddlServicioDesc">Servicio a Realizar (Catálogo):</label>
                <asp:DropDownList ID="ddlServicioDesc" runat="server" CssClass="form-control" required />
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