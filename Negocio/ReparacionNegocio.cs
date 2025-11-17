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

        public void ActualizarCosto(int idDetalle, string nuevoCosto)
        {
            
            AccesoDatos datos = new AccesoDatos();
            try
            {
                decimal costoDecimal;
                if (!decimal.TryParse(nuevoCosto, out costoDecimal))
                {
                    throw new Exception("El nuevo costo no es un número válido.");
                }
                datos.setearProcedimiento("SP_ActualizarCostoxServicio");
                datos.setearParametro("@idDetalle", idDetalle);
                datos.setearParametro("@NuevoCosto", nuevoCosto);
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

        public DataTable listarResumenFacturacion()
        {
            AccesoDatos datos = new AccesoDatos();
            DataTable tabla = new DataTable();

            try
            {
                // Consulta SQL a la vista ya existente
                datos.setearConsulta("SELECT * FROM V_Resumen_Facturacion ORDER BY id_Reparacion DESC");
                datos.ejecutarLectura();

                if (datos.Lector != null)
                {
                    tabla.Load(datos.Lector);
                }
                return tabla;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el resumen de facturacion por reparación: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public int ContarServiciosEjecutados(string dniMecanico)
        {
            AccesoDatos datos = new AccesoDatos();
            DataTable tabla = new DataTable();

            try
            {
                // 1. La consulta llama a la función escalar y devuelve el resultado como una tabla de 1x1.
                datos.setearConsulta("SELECT dbo.FN_ContarServiciosEjecutadosPorMecanico(@DniMecanico)");
                datos.setearParametro("@DniMecanico", dniMecanico);

                datos.ejecutarLectura(); // Ejecuta la consulta (esto llenará el Lector/DataReader)

                // 2. Cargar el resultado del Lector en un DataTable para procesarlo.
                if (datos.Lector != null)
                {
                    tabla.Load(datos.Lector);
                }

                // 3. Procesar el resultado: si hay una fila, extraemos el primer valor.
                if (tabla.Rows.Count > 0)
                {
                    // El valor de la función escalar está en la celda [0][0]
                    // Usamos Convert.ToInt32 para asegurar que el valor sea un entero.
                    return Convert.ToInt32(tabla.Rows[0][0]);
                }
                return 0;
            }
            catch (Exception ex)
            {
                // Es crucial cerrar la conexión en el finally, pero lanzamos el error para CATCH.
                throw new Exception("Error al llamar a la función de conteo: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}