<%@ Page Title="Contador de Servicios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ContadorServicios.aspx.cs" Inherits="TallerMotos.ContadorServicios" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Función Escalar: Contador de Servicios</h2>
    <p>Utiliza la función `FN_ContarServiciosEjecutadosPorMecanico` para obtener la productividad de cada mecánico.</p>
    <hr />

    <div class="row">
        <div class="col-md-6">
            <div class="form-group mb-3">
                <label for="ddlMecanicoFuncion">Seleccionar Mecánico:</label>
                <asp:DropDownList ID="ddlMecanicoFuncion" 
                    runat="server" 
                    CssClass="form-control" 
                    AutoPostBack="true" 
                    OnSelectedIndexChanged="ddlMecanicoFuncion_SelectedIndexChanged" 
                    required />
            </div>
        </div>
        <div class="col-md-6">
            <h4 class="mt-4">Servicios Totales Ejecutados:</h4>
            <asp:Label ID="lblResultado" runat="server" CssClass="h1 text-success" Text="0" />
            <p>Conteo directo de la función de base de datos.</p>
        </div>
    </div>
    <asp:Label ID="lblError" runat="server" EnableViewState="false" CssClass="alert alert-danger mt-3" Visible="false" />
</asp:Content>