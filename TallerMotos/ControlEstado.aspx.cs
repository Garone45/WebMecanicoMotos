using Negocio;
using System;
using System.Web.UI.WebControls;
using System.Data;

namespace TallerMotos
{


public partial class ControlEstado : System.Web.UI.Page
{
    private void CargarEstados()
    {
        
        ddlNuevoEstado.Items.Clear();
        ddlNuevoEstado.Items.Add(new ListItem("— Seleccione un Estado —", ""));
        ddlNuevoEstado.Items.Add(new ListItem("Esperando Presupuesto", "Esperando Presupuesto"));
        ddlNuevoEstado.Items.Add(new ListItem("En Reparación", "En Reparación"));
        ddlNuevoEstado.Items.Add(new ListItem("Control de Calidad", "Control de Calidad"));
        ddlNuevoEstado.Items.Add(new ListItem("Listo para Retirar", "Listo para Retirar"));
        ddlNuevoEstado.Items.Add(new ListItem("Entregado", "Entregado")); // Añadir el estado final
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarEstados();
        }
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        ReparacionNegocio negocio = new ReparacionNegocio();
        int idReparacion;

        // Validación de entrada
        if (!int.TryParse(txtReparacionId.Text, out idReparacion))
        {
            lblMensaje.Text = "ERROR: Ingrese un ID de Reparación numérico válido.";
            lblMensaje.CssClass = "alert-danger";
            return;
        }

        string nuevoEstado = ddlNuevoEstado.SelectedValue;

        if (string.IsNullOrEmpty(nuevoEstado))
        {
            lblMensaje.Text = "ERROR: Debe seleccionar un estado.";
            lblMensaje.CssClass = "alert-danger";
            return;
        }

        try
        {
            // Ejecutar el Stored Procedure SP_ActualizarEstado
            negocio.ActualizarEstado(idReparacion, nuevoEstado);

            lblMensaje.Text = $"✅ Orden {idReparacion} actualizada a '{nuevoEstado}'.";
            lblMensaje.CssClass = "alert-success";

            // Opcional: Refrescar el GridView del Dashboard (si estuviera en esta página)
        }
        catch (Exception ex)
        {
            // Captura el error de SQL Server si el ID no existe
            lblMensaje.Text = "❌ Error al actualizar: " + ex.Message;
            lblMensaje.CssClass = "alert-danger";
        }
    }
}
}