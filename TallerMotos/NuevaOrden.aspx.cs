using Negocio;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace TallerMotos
{
    public partial class NuevaOrden : System.Web.UI.Page
    {
        // Asegúrate de que los nuevos controles estén declarados en designer.cs:
        // ddlDniCliente, ddlPatente, ddlMecanico, ddlServicioDesc, txtCostoServicio, lblMensaje

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarControles();
                // Si hay un valor por defecto seleccionado al inicio, cargamos las motos
                if (!string.IsNullOrEmpty(ddlDniCliente.SelectedValue))
                {
                    CargarMotos(ddlDniCliente.SelectedValue);
                }
            }
        }

        private void CargarControles()
        {
            ReparacionNegocio negocio = new ReparacionNegocio();

            try
            {
                // 1. Carga de SERVICIOS
                DataTable dtServicios = negocio.ObtenerCatalogoServicios();
                ddlServicioDesc.DataSource = dtServicios;
                ddlServicioDesc.DataTextField = "Descripcion";
                ddlServicioDesc.DataValueField = "Descripcion";
                ddlServicioDesc.DataBind();
                ddlServicioDesc.Items.Insert(0, new ListItem("— Seleccione el Servicio —", ""));

                // 2. Carga de MECÁNICOS
                DataTable dtMecanicos = negocio.ObtenerCatalogoMecanicos();
                ddlMecanico.DataSource = dtMecanicos;
                ddlMecanico.DataTextField = "NombreCompleto";
                ddlMecanico.DataValueField = "Dni";
                ddlMecanico.DataBind();
                ddlMecanico.Items.Insert(0, new ListItem("— Seleccione el Mecánico —", ""));

                // 3. Carga de CLIENTES (NUEVA LÓGICA)
                DataTable dtClientes = negocio.ObtenerCatalogoClientes(); // Requiere el nuevo método en Negocio
                ddlDniCliente.DataSource = dtClientes;
                ddlDniCliente.DataTextField = "NombreCompleto";
                ddlDniCliente.DataValueField = "Dni"; // Usamos el DNI como valor
                ddlDniCliente.DataBind();
                ddlDniCliente.Items.Insert(0, new ListItem("— Seleccione el Cliente —", ""));

                // Inicializar el DDL de Patentes
                ddlPatente.Items.Clear();
                ddlPatente.Items.Insert(0, new ListItem("— Primero seleccione un Cliente —", ""));
            }
            catch (Exception ex)
            {
                // Muestra el error de conexión si el SQL falla
                lblMensaje.Text = "❌ ERROR de conexión al cargar catálogos: " + ex.Message;
                lblMensaje.CssClass = "alert-danger";
                // Considera si quieres lanzar el error o solo mostrarlo en la UI
                // throw new Exception("Fallo al cargar catálogos. Error: " + ex.Message);
            }
        }

        // Evento que se dispara cuando cambia la selección en ddlDniCliente
        protected void ddlDniCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dniSeleccionado = ddlDniCliente.SelectedValue;

            if (string.IsNullOrEmpty(dniSeleccionado))
            {
                // Si no se selecciona ningún cliente, limpiar la lista de motos
                ddlPatente.Items.Clear();
                ddlPatente.Items.Insert(0, new ListItem("— Seleccione la Patente —", ""));
                return;
            }

            // Llamar a la función que carga las motos del cliente
            CargarMotos(dniSeleccionado);
        }

        // Método auxiliar para cargar las motos de un DNI específico
        private void CargarMotos(string dni)
        {
            ReparacionNegocio negocio = new ReparacionNegocio();
            ddlPatente.Items.Clear(); // Limpiar antes de cargar

            try
            {
                // Requiere el nuevo método en Negocio
                DataTable dtMotos = negocio.ObtenerMotosPorDniCliente(dni);

                ddlPatente.DataSource = dtMotos;
                ddlPatente.DataTextField = "DescripcionMotoCompleta"; // Muestra Marca y Modelo
                ddlPatente.DataValueField = "Patente"; // Usa la Patente como valor
                ddlPatente.DataBind();

                if (dtMotos.Rows.Count == 0)
                {
                    ddlPatente.Items.Insert(0, new ListItem("— No hay motos registradas para este cliente —", ""));
                }
                else
                {
                    // Inserta una opción por defecto al inicio si hay datos
                    ddlPatente.Items.Insert(0, new ListItem("— Seleccione una moto —", ""));
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "❌ ERROR al cargar las motos: " + ex.Message;
                lblMensaje.CssClass = "alert-danger";
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            ReparacionNegocio negocio = new ReparacionNegocio();
            decimal costoServicio;

            // 1. Validación de Costo
            if (!decimal.TryParse(txtCostoServicio.Text.Trim(), out costoServicio))
            {
                lblMensaje.Text = "ERROR: Ingrese un costo válido (numérico).";
                lblMensaje.CssClass = "alert-danger";
                return;
            }

            // 2. Validación de DropDownLists (Asegurar que se seleccionó algo)
            if (ddlDniCliente.SelectedValue == "" || ddlMecanico.SelectedValue == "" ||
                ddlServicioDesc.SelectedValue == "" || ddlPatente.SelectedValue == "")
            {
                lblMensaje.Text = "ERROR: Debe seleccionar un valor válido para Cliente, Patente, Mecánico y Servicio.";
                lblMensaje.CssClass = "alert-danger";
                return;
            }

            try
            {
                // 3. Recolección de Parámetros (5 valores)
                string patente = ddlPatente.SelectedValue.Trim();
                string dniCliente = ddlDniCliente.SelectedValue.Trim();
                string dniMecanico = ddlMecanico.SelectedValue.Trim();
                string servicioDesc = ddlServicioDesc.SelectedValue.Trim();

                // 4. LLAMADA AL SP CON 5 PARÁMETROS
                negocio.RegistrarNuevaOrden(
                    patente,
                    dniCliente,
                    dniMecanico,
                    servicioDesc,
                    costoServicio
                );

                // Si no hubo excepción, fue exitoso
                lblMensaje.Text = "✅ Orden de reparación registrada con éxito.";
                lblMensaje.CssClass = "alert-success";
            }
            catch (Exception ex)
            {
                // Muestra el error de SQL/SP
                lblMensaje.Text = "❌ ERROR al registrar: " + ex.Message;
                lblMensaje.CssClass = "alert-danger";
            }
        }
    }
}