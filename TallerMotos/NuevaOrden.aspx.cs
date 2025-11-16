using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TallerMotos
{
    public partial class NuevaOrden : System.Web.UI.Page
    {
        // Asegúrate de que las declaraciones de los controles existan en el designer.cs
        // (o declaralas manualmente si persiste el error)

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarControles();
            }
        }

        private void CargarControles()
        {
            ReparacionNegocio negocio = new ReparacionNegocio();

            try
            {
                // 1. Carga de SERVICIOS (Tu código existente)
                DataTable dtServicios = negocio.ObtenerCatalogoServicios();
                ddlServicioDesc.DataSource = dtServicios;
                ddlServicioDesc.DataTextField = "Descripcion";
                ddlServicioDesc.DataValueField = "Descripcion";
                ddlServicioDesc.DataBind();
                ddlServicioDesc.Items.Insert(0, new ListItem("— Seleccione el Servicio —", ""));


                // -----------------------------------------------------
                // 2. Carga de MECÁNICOS (NUEVA LÓGICA)
                // -----------------------------------------------------
                DataTable dtMecanicos = negocio.ObtenerCatalogoMecanicos();

                ddlMecanico.DataSource = dtMecanicos;


                ddlMecanico.DataTextField = "NombreCompleto";
                ddlMecanico.DataValueField = "Dni";

                ddlMecanico.DataBind();
                ddlMecanico.Items.Insert(0, new ListItem("— Seleccione el Mecánico —", ""));

            }
            catch (Exception ex)
            {
                // Muestra el error de conexión si el SQL falla al obtener los catálogos
                // Asegúrate de que lblMensaje esté accesible en el Page_Load para mostrar errores
                throw new Exception("Fallo al cargar catálogos. Verifique la conexión a SQL Server y los datos de las tablas Mecanico/Servicio. Error: " + ex.Message);
            }
        }

            protected void btnRegistrar_Click(object sender, EventArgs e)
            {
                ReparacionNegocio negocio = new ReparacionNegocio();
                decimal costoServicio;

                if (!decimal.TryParse(txtCostoServicio.Text.Trim(), out costoServicio))
                {
                    lblMensaje.Text = "ERROR: Ingrese un costo válido.";
                    lblMensaje.CssClass = "alert-danger";
                    return;
                }

                try
                {
                    // 1. Recolección de Parámetros
                    string dniMecanico = ddlMecanico.SelectedValue.Trim();

                    // 2. LLAMADA AL SP CON 9 PARÁMETROS (La lógica de inserción de moto está en el SP)
                    negocio.RegistrarNuevaOrden(
                        // --- DATOS DE MOTO/CLIENTE ---
                        txtPatente.Text.Trim(),
                        txtMarca.Text.Trim(),
                        txtModelo.Text.Trim(),
                        txtDniCliente.Text.Trim(), // Nuevo parámetro de Cliente

                        // --- DATOS DE ORDEN/SERVICIO ---
                        dniMecanico, // Mecánico Principal DNI
                        txtDescripcionOrden.Text,
                        ddlServicioDesc.SelectedValue,
                        costoServicio,
                        dniMecanico // Mecánico Detalle DNI
                    );

                    lblMensaje.Text = "✅ Orden y Moto (si era nueva) registradas con éxito.";
                    lblMensaje.CssClass = "alert-success";
                }
                catch (Exception ex)
                {
                    // El SP arrojará el error si el DNI del Cliente no existe.
                    lblMensaje.Text = "❌ ERROR: " + ex.Message;
                    lblMensaje.CssClass = "alert-danger";
                }
            }
    }
}