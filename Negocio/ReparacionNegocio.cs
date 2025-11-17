using System;
using System.Data;
using System.Collections.Generic;

namespace Negocio
{
    public class ReparacionNegocio
    {

        public void RegistrarNuevaOrden(
        
        string patente,
        string dniMecanico,
        string servicioDesc,
        decimal costoServicio,
        string descripcion
        )

        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_RegistrarOrdenCompleta");

                // Seteo de los 5 parámetros
                datos.setearParametro("@Patente", patente);
                datos.setearParametro("@MecanicoPrincipalDni", dniMecanico);
                datos.setearParametro("@ServicioDesc", servicioDesc);
                datos.setearParametro("@CostoServicio", costoServicio);
                datos.setearParametro("@DescripcionOrden", descripcion);

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
        public DataTable ObtenerCatalogoClientes()
        {
            AccesoDatos datos = new AccesoDatos();
            DataTable tabla = new DataTable();

            try
            {
                // id_TipoUsuario = 2 es el Cliente
                datos.setearConsulta(@"
            SELECT 
                U.Dni AS Dni, 
                U.Nombre + ' ' + U.Apellido AS NombreCompleto
            FROM Usuario U
            WHERE U.id_TipoUsuario = 2 
            ORDER BY U.Apellido, U.Nombre");

                datos.ejecutarLectura();

                if (datos.Lector != null)
                {
                    tabla.Load(datos.Lector);
                }
                return tabla;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el catálogo de clientes: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        /// <summary>
        /// Obtiene las motos (Patente y Descripción) asociadas a un DNI de Cliente.
        /// </summary>
        public DataTable ObtenerMotosPorDniCliente(string dni)
        {
            AccesoDatos datos = new AccesoDatos();
            DataTable tabla = new DataTable();

            try
            {
                // La Patente y DNI deben recortarse para mayor robustez
                datos.setearConsulta(@"
                SELECT 
                    M.id_Moto,
                    LTRIM(RTRIM(M.Patente)) AS Patente, 
                    M.Marca + ' ' + M.Modelo AS DescripcionMotoCompleta
                FROM Motos M
                JOIN Usuario U ON M.id_Usuario = U.id_Usuario
                WHERE LTRIM(RTRIM(U.Dni)) = @DniCliente
                ORDER BY M.Patente;");

                datos.setearParametro("@DniCliente", dni);
                datos.ejecutarLectura();

                if (datos.Lector != null)
                {
                    tabla.Load(datos.Lector);
                }
                return tabla;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las motos del cliente: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public DataTable ObtenerDetalleServiciosPorReparacion()
        {
            AccesoDatos datos = new AccesoDatos();
            DataTable tabla = new DataTable();

            try
            {
                // Consulta SQL a la vista ya existente
                datos.setearConsulta("SELECT * FROM V_Detalle_Servicios_Por_Reparacion ORDER BY id_Reparacion, Costo_del_Servicio DESC");
                datos.ejecutarLectura();

                if (datos.Lector != null)
                {
                    tabla.Load(datos.Lector);
                }
                return tabla;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el detalle de servicios por reparación: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}