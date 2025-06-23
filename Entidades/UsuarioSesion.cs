using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace Entidades
{
    public class UsuarioSesion
    {
        public static int IdPersona { get; set; }
        public static int Identificacion { get; set; }
        public static string Nombre { get; set; }
        public static string Apellido1 { get; set; }
        public static string Apellido2 { get; set; }
        public static string Correo { get; set; }
        public static string Usuario { get; set; }

        public static void CargarPerfil(Perfil perfil)
        {
            if (perfil != null)
            {
                IdPersona = perfil.IdPersona;
                Identificacion = perfil.Identificacion;
                Nombre = perfil.Nombre;
                Apellido1 = perfil.Apellido1;
                Apellido2 = perfil.Apellido2;
                Correo = perfil.Correo;
                Usuario = perfil.Usuario;
            }
        }
    }
}
