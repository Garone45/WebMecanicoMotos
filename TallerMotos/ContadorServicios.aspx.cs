using Negocio;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace TallerMotos
{
    public partial class ContadorServicios : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMecanicos();
                // Opcional: Si deseas cargar el resultado inicial para el primer elemento
                // ddlMecanicoFuncion_SelectedIndexChanged(sender, e);
            }
        }

        private void CargarMecanicos()
        {
            ReparacionNegocio negocio = new ReparacionNegocio();
            try
            {
                // Reutilizamos el método existente para cargar los mecánicos (DNI y Nombre)
                DataTable dtMecanicos = negocio.ObtenerCatalogoMecanicos();

                ddlMecanicoFuncion.DataSource = dtMecanicos;
                ddlMecanicoFuncion.DataTextField = "NombreCompleto";
                ddlMecanicoFuncion.DataValueField = "Dni";
                ddlMecanicoFuncion.DataBind();

                ddlMecanicoFuncion.Items.Insert(0, new ListItem("— Seleccione un Mecánico —", ""));
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al cargar mecánicos: " + ex.Message;
                lblError.Visible = true;
            }
        }

        protected void ddlMecanicoFuncion_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblError.Visible = false;
            string dniSeleccionado = ddlMecanicoFuncion.SelectedValue;

            if (string.IsNullOrEmpty(dniSeleccionado))
            {
                lblResultado.Text = "0";
                return;
            }

            ReparacionNegocio negocio = new ReparacionNegocio();
            try
            {
                // LLAMADA CLAVE: Llama a la función SQL a través del método de negocio
                int conteo = negocio.ContarServiciosEjecutados(dniSeleccionado);

                lblResultado.Text = conteo.ToString();
            }
            catch (Exception ex)
            {
                // Muestra cualquier error que venga de la base de datos o del método de negocio
                lblError.Text = "Error al contar servicios: " + ex.Message;
                lblError.Visible = true;
                lblResultado.Text = "ERROR";
            }
        }
    }
}