using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public AccesoDatos() // Constructor
        {
        
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=TallerMecanicoMotos; integrated security=true");
            comando = new SqlCommand();
        }

        public void setearConsulta(string consulta)
        {
            comando.Parameters.Clear();
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void setearParametro(string nombre, object valor)
        {
            // Usa DBNull.Value para manejar valores nulos correctamente en SQL
            comando.Parameters.AddWithValue(nombre, valor ?? DBNull.Value);
        }

        public void setearProcedimiento(string sp)
        {
            // Configurar el comando para ejecutar un SP (ej. SP_RegistrarOrdenCompleta)
            comando.Parameters.Clear();
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = sp;
        }

        public void ejecutarLectura() // Para SELECT (Vistas)
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al ejecutar lectura SQL: " + ex.Message, ex);
            }
        }

        public void ejecutarAccion() // Para INSERT, UPDATE, DELETE, EXEC SP
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (SqlException sqlEx)
            {
                // capturar errores específicos del servidor (ej. el RAISERROR de disponibilidad)
                throw new Exception("Error del servidor SQL (Transacción fallida): " + sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar acción SQL: " + ex.Message, ex);
            }
            finally
            {
                // Cierra la conexión después de la acción
                if (conexion.State == System.Data.ConnectionState.Open)
                    conexion.Close();
            }
        }

        public void cerrarConexion()
        {
            // Cierra el lector y la conexión, independientemente del éxito o fracaso
            if (lector != null && !lector.IsClosed)
                lector.Close();

            if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
                conexion.Close();
        }
    }
}