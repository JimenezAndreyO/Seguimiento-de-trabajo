using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entidades
{
    public class UsuarioSesion
    {
        public int IdPersona { get; set; }

        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Usuario { get; set; }
        public string Correo { get; set; }

        public string Estado { get; set; }

        // Constructor opcional
        public UsuarioSesion() { }

        public UsuarioSesion(string identificacion , string nombre, string apellido1, string apellido2, string usuario, string correo)
        {
            Identificacion = Identificacion;
            Nombre = nombre;
            Apellido1 = apellido1;
            Apellido2 = apellido2;
            Usuario = usuario;
            Correo = correo;
        }

       
    }

    public class Reporte
    {
        public int IdReporte { get; set; }
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Departamento { get; set; }
        public string TipoIncidencia { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public string Observaciones { get; set; }

        public string NombrePersonaReporte { get; set; }
        public string ApellidoPersonaReporte { get; set; }
        public string Apellido2PersonaReporte { get; set; }


        public byte[] Archivo { get; set; }
        public string Estado { get; set; }
    }

    public class Justificacion
    {
        public int IdJustificacion { get; set; }
        public string Motivo { get; set; }
        public DateTime Fecha { get; set; }

        // Constructor vacío
        public Justificacion() { }

        // ✅ Constructor que acepta un argumento (ejemplo con IdJustificacion)
        public Justificacion(int idJustificacion)
        {
            IdJustificacion = idJustificacion;
        }
    }

    public class ManejodePermisos
    {
        public int IdPermisoUsuario { get; set; }
    }

}