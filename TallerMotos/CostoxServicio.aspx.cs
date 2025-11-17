using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TallerMotos
{
    public partial class CostoxServicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            ReparacionNegocio negocio = new ReparacionNegocio();
            int idDetalle;

            // Validación de entrada
            if (!int.TryParse(txtDetalleId.Text, out idDetalle))
            {
                lblMensaje.Text = "ERROR: Ingrese un ID de Reparación numérico válido.";
                lblMensaje.CssClass = "alert-danger";
                return;
            }

            string nuevoCosto = txtCostoxServicio.Text;

            if (string.IsNullOrEmpty(nuevoCosto))
            {
                lblMensaje.Text = "ERROR: Debe seleccionar un estado.";
                lblMensaje.CssClass = "alert-danger";
                return;
            }

            try
            {
                // Ejecutar el Stored Procedure SP_ActualizarEstado
                negocio.ActualizarCosto(idDetalle, nuevoCosto);

                lblMensaje.Text = $"✅ Orden {idDetalle} actualizada a '{nuevoCosto}'.";
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