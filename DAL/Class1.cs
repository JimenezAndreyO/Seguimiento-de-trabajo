using System;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class ConexionBD
    {
        private readonly string cadenaConexion;

        public ConexionBD()
        {
            var configuracion = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            cadenaConexion = configuracion.GetConnectionString("MiConexion");
        }

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}
