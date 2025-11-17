<%@ Page Title="Menú Principal del Taller" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TallerMotos._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Sistema de Gestión de Taller Mecánico</h2>
    <hr />

    <div class="row">
        <h3>Funcionalidades Clave del Sistema</h3>
        <p>Seleccione una opción para demostrar el funcionamiento</p>
    </div>

    <div class="row" style="margin-top: 20px;">
        <div class="col-md-4">
            <a href="Reportes.aspx" class="btn btn-info btn-lg btn-block" style="padding: 20px;">
                <span class="glyphicon glyphicon-stats"></span>
                <br/> 1. Ver Dashboard / Reportes (VISTAS)
            </a>
        </div>

        <div class="col-md-4">
            <a href="ControlEstado.aspx" class="btn btn-warning btn-lg btn-block" style="padding: 20px;">
                <span class="glyphicon glyphicon-pencil"></span>
                <br/> 2. Actualizar Flujo de Tareas (SP)
            </a>
        </div>

        <div class="col-md-4">
            <a href="NuevaOrden.aspx" class="btn btn-success btn-lg btn-block" style="padding: 20px;">
                <span class="glyphicon glyphicon-plus"></span>
                <br />
                3. Registrar Nueva Orden (SP)
            </a>
        </div>

        <div class="col-md-4">
            <a href="CostoxServicio.aspx" class="btn btn-success btn-lg btn-block" style="padding: 20px;">
                <span class="glyphicon glyphicon-plus"></span>
                <br />
                3. Actualizar costo x servicio (SP)
            </a>
        </div>
    </div>
</asp:Content>