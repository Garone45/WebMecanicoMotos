<%@ Page Title="Dashboard de Reportes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="TallerMotos.Reportes" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Dashboard Operacional y Reportes (TPI)</h2>
    <hr />

    <ul class="nav nav-tabs" id="reportTabs" role="tablist">
        <li class="nav-item">
            <a class="nav-link active" id="activas-tab" data-bs-toggle="tab" href="#activas" role="tab" aria-selected="true">Órdenes Activas</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" id="productividad-tab" data-bs-toggle="tab" href="#productividad" role="tab" aria-selected="false">Productividad Mecánicos</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" id="dias-taller-tab" data-bs-toggle="tab" href="#dias-taller" role="tab" aria-selected="false">Días de Permanencia</a>
        </li>
    </ul>

    <div class="tab-content" id="reportTabsContent">
        
        <div class="tab-pane fade show active" id="activas" role="tabpanel" aria-labelledby="activas-tab">
            <h3>Trazabilidad de Órdenes en Curso</h3>
            <asp:GridView
                ID="GridViewActivas"
                runat="server"
                AutoGenerateColumns="true"
                CssClass="table table-bordered table-striped mt-3">
            </asp:GridView>
        </div>

        <div class="tab-pane fade" id="productividad" role="tabpanel" aria-labelledby="productividad-tab">
            <h3>Reporte Analítico (V_Productividad_Mecanicos)</h3>
            <asp:GridView
                ID="GridViewProductividad"
                runat="server"
                AutoGenerateColumns="true"
                CssClass="table table-bordered table-striped mt-3">
            </asp:GridView>
        </div>
        
        <div class="tab-pane fade" id="dias-taller" role="tabpanel" aria-labelledby="dias-taller-tab">
            <h3>Métrica de Días en Taller (Vista: V_DIAS_EN_TALLER_POR_MOTO)</h3>
            <p>Reporte de la permanencia de las motos, indicando el tiempo transcurrido desde el ingreso.</p>
            <asp:GridView
                ID="GridViewDiasTaller"
                runat="server"
                AutoGenerateColumns="true"
                CssClass="table table-bordered table-striped mt-3">
            </asp:GridView>
        </div>
        
    </div> 
    </asp:Content>