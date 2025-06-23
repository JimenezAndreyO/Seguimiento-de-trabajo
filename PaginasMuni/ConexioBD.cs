using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PaginasMuni
{
   

    class ConexionBD
    {

        private readonly string cadenaConexion;

        public ConexionBD()
        {
            // Cargar la configuración desde appsettings.json
            var configuracion = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Carpeta raíz del proyecto
                .AddJsonFile("appsettings.json") // Cargar JSON
                .Build();

            // Obtener la cadena de conexión del JSON
            cadenaConexion = configuracion.GetConnectionString("MiConexion");
        }
        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }



        public class UsuarioDAL
        {
            private readonly ConexionBD conexion = new ConexionBD();

            public bool ValidarUsuario(string usuario, string contraseña)
            {
                using (SqlConnection conn = conexion.ObtenerConexion())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = @usuario AND Contraseña = @contraseña";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@contraseña", contraseña);

                        int count = (int)cmd.ExecuteScalar();
                        return count > 0; // Si existe el usuario, retorna true
                    }
                }
            }
        }





    }
}
