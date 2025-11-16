using System;

namespace TallerMotos
{
    public partial class _Default : System.Web.UI.Page
    {
        // Navegación a Reportes (Vistas)
        protected void btnReportes_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reportes.aspx");
        }

        // Navegación a Actualizar Estado (SP)
        protected void btnActualizarEstado_Click(object sender, EventArgs e)
        {
            Response.Redirect("ControlEstado.aspx");
        }

        // Navegación a Nueva Orden (Pendiente)
        protected void btnNuevaOrden_Click(object sender, EventArgs e)
        {
            Response.Redirect("NuevaOrden.aspx");
        }
    }
}