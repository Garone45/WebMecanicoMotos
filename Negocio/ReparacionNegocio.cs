using System;
using System.Data;
using System.Collections.Generic;

namespace Negocio
{
    public class ReparacionNegocio
    {

        public void RegistrarNuevaOrden(
    string patente,
    string marca,
    string modelo,
    string dniCliente,
    string dniMecanicoPrincipal, // Parámetro 5
    string descripcionOrden,
    string servicioDesc,
    decimal costoServicio,
    string dniMecanicoDetalle    // Parámetro 9
)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_RegistrarOrdenCompleta");

                // Los 9 parámetros deben ser seteados en la Capa de Datos:
                datos.setearParametro("@Patente", patente);
                datos.setearParametro("@Marca", marca);
                datos.setearParametro("@Modelo", modelo);
                datos.setearParametro("@DniCliente", dniCliente);

                datos.setearParametro("@MecanicoPrincipalDni", dniMecanicoPrincipal); // Usando el Parámetro 5

                datos.setearParametro("@DescripcionOrden", descripcionOrden);
                datos.setearParametro("@ServicioDesc", servicioDesc);
                datos.setearParametro("@CostoServicio", costoServicio);

                datos.setearParametro("@MecanicoDetalleDni", dniMecanicoDetalle); // Usando el Parámetro 9

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar la orden: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        /// <summary>
        /// Actualiza el id_Estado de una reparación. Llama al SP_ActualizarEstado.
        /// </summary>
        public void ActualizarEstado(int idReparacion, string nuevoEstado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_ActualizarEstado");
                datos.setearParametro("@idReparacion", idReparacion);
                datos.setearParametro("@NuevoEstado", nuevoEstado);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        // ========================================================================
        // 2. MÉTODOS DE LECTURA (DQL - Llaman a VISTAS y Catálogos)
        // ========================================================================

        /// <summary>
        /// Obtiene el catálogo de servicios (solo Descripción) para llenar un DropDownList.
        /// </summary>
        public DataTable ObtenerCatalogoServicios()
        {
            AccesoDatos datos = new AccesoDatos();
            DataTable tabla = new DataTable();

            try
            {
                // Consulta SQL: SELECT Descripcion FROM Servicio
                datos.setearConsulta("SELECT Descripcion FROM Servicio ORDER BY Descripcion");
                datos.ejecutarLectura();

                if (datos.Lector != null)
                {
                    tabla.Load(datos.Lector);
                }
                return tabla;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el catálogo de servicios: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        /// <summary>
        /// Obtiene el catálogo de mecánicos (DNI y Nombre Completo) para llenar un DropDownList.
        /// </summary>
        public DataTable ObtenerCatalogoMecanicos()
        {
            AccesoDatos datos = new AccesoDatos();
            DataTable tabla = new DataTable();

            try
            {
                // Consulta SQL: Une Mecanico y Usuario para obtener el Nombre Completo y DNI.
                datos.setearConsulta(@"
                    SELECT 
                        U.Dni AS Dni, 
                        U.Nombre + ' ' + U.Apellido AS NombreCompleto
                    FROM Mecanico M
                    JOIN Usuario U ON M.id_Usuario = U.id_Usuario
                    ORDER BY U.Nombre");

                datos.ejecutarLectura();

                if (datos.Lector != null)
                {
                    tabla.Load(datos.Lector);
                }
                return tabla;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el catálogo de mecánicos: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        /// <summary>
        /// Obtiene la Vista de Reparaciones Activas (JOIN complejo para monitoreo).
        /// </summary>
        public DataTable ObtenerReparacionesActivas()
        {
            AccesoDatos datos = new AccesoDatos();
            DataTable tabla = new DataTable();
            try
            {
                datos.setearConsulta("SELECT * FROM V_Reparaciones_Activas");
                datos.ejecutarLectura();
                if (datos.Lector != null) tabla.Load(datos.Lector);
                return tabla;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la vista de reparaciones activas: " + ex.Message);
            }
            finally { datos.cerrarConexion(); }
        }

        /// <summary>
        /// Obtiene la Vista de Productividad (Reporte analítico).
        /// </summary>
        public DataTable ObtenerReporteProductividad()
        {
            AccesoDatos datos = new AccesoDatos();
            DataTable tabla = new DataTable();
            try
            {
                datos.setearConsulta("SELECT * FROM V_Productividad_Mecanicos");
                datos.ejecutarLectura();
                if (datos.Lector != null) tabla.Load(datos.Lector);
                return tabla;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el reporte de productividad: " + ex.Message);
            }
            finally { datos.cerrarConexion(); }
        }

        /// <summary>
        /// Obtiene la Vista de Días en Taller (Métrica de eficiencia).
        /// </summary>
        public DataTable ObtenerReporteDiasTaller()
        {
            AccesoDatos datos = new AccesoDatos();
            DataTable tabla = new DataTable();

            try
            {
                datos.setearConsulta("SELECT * FROM V_DIAS_EN_TALLER_POR_MOTO");
                datos.ejecutarLectura();

                if (datos.Lector != null)
                {
                    tabla.Load(datos.Lector);
                }
                return tabla;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la vista de días en taller: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}