using Negocio;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace TallerMotos 
{
    public partial class Reportes : System.Web.UI.Page
    {
      

        private void CargarReportes()
        {
            ReparacionNegocio negocio = new ReparacionNegocio();

            try
            {
                // 1. Cargar la Vista de Operaciones Activas (JOINs complejos)
                GridViewActivas.DataSource = negocio.ObtenerReparacionesActivas();
                GridViewActivas.DataBind();

                // 2. Cargar la Vista de Reporte Analítico (GROUP BY/SUM)
                GridViewProductividad.DataSource = negocio.ObtenerReporteProductividad();
                GridViewProductividad.DataBind();

                // 3. Cargar la Vista de Días en Taller (NUEVA)
                GridViewDiasTaller.DataSource = negocio.ObtenerReporteDiasTaller();
                GridViewDiasTaller.DataBind();

                DataTable dtDetalleServicios = negocio.ObtenerDetalleServiciosPorReparacion(); // Usa el método que agregaste a ReparacionNegocio
                GridViewDetalleServicios.DataSource = dtDetalleServicios;
                GridViewDetalleServicios.DataBind();

                GridViewFacturacion.DataSource = negocio.listarResumenFacturacion();
                GridViewFacturacion.DataBind();
            }
            catch (Exception ex)
            {
         
                Response.Write("<div class='alert alert-danger'>Error al cargar reportes: " + ex.Message + "</div>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar datos solo la primera vez que se carga la página.
                CargarReportes();
            }
        }
    }
}