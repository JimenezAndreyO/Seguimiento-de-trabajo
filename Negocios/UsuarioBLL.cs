using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Entidades;
using System.Net.Mail;
using System.Net;

namespace Negocios
{
    public class UsuarioBLL
    {

        private UsuarioDAL usuarioDAL;

        public UsuarioBLL()
        {
            usuarioDAL = new UsuarioDAL();
        }


        public List<Reporte> ObtenerReportesActivos(int idPersona)
        {
            return usuarioDAL.ObtenerReportesActivos(idPersona)
                .Select(r => new Reporte
                {
                    IdReporte = r.IdReporte,
                    IdPersona = r.IdPersona,
                    Departamento = r.Departamento,
                    TipoIncidencia = r.TipoIncidencia,
                    FechaInicio = r.FechaInicio,
                    FechaFinal = r.FechaFinal,
                    Observaciones = r.Observaciones,
                    Archivo = r.Archivo,
                    Estado = r.Estado
                }).ToList();
        }

        public UsuarioSesion ObtenerUsuarioSesion(string usuarioId)
        {
            // Llamada al DAL (capa de datos) para obtener el perfil del usuario
            return usuarioDAL.ObtenerPerfilPorUsuario(usuarioId);
        }



        public bool Login(string Usuario, string Contraseña, out string mensaje, out int rol, out UsuarioSesion perfil)
        {
            bool esValido = usuarioDAL.ValidarUsuario(Usuario, Contraseña, out mensaje, out rol);

            if (esValido)
            {
                perfil = usuarioDAL.ObtenerPerfilPorUsuario(Usuario);
            }
            else
            {
                perfil = null;
            }

            return esValido;
        }

        public bool JustificarReporte(int idReporte, string justificacion)
        {

            UsuarioDAL usuarioDAL = new UsuarioDAL();
            return usuarioDAL.JustificarReporte(idReporte, justificacion);
        }

        public bool Insertar(string Identificacion, string Nombre, string Apellido1, string Apellido2,
        string Correo, string Usuario, string Contraseña,
        bool CrearReporte, bool RespuestaSolicitud, bool Auditoria,
        bool SeguimientodeTrabajo, bool AgregarPersona, bool EliminarPersona, bool PermisosUsuarios)
        {

            return usuarioDAL.InsertarUsuarios(Identificacion, Nombre, Apellido1, Apellido2,
            Correo, Usuario, Contraseña,
            CrearReporte, RespuestaSolicitud, Auditoria,
            SeguimientodeTrabajo, AgregarPersona, EliminarPersona, PermisosUsuarios);
        }


        public bool ModificarEstado(int IdPersona)

        {
            return usuarioDAL.ActualizarEstado(IdPersona);
        }

        public void GuardarPermisos(string Identificacion, bool CrearReporte, bool RespuestaSolicitud, bool Auditoria, bool SeguimientodeTrabajo, bool AgregarPersona, bool EliminarPermoiso, bool PermisosUsuarios)
        {
            usuarioDAL.guardarpermisos(Identificacion, CrearReporte, RespuestaSolicitud, Auditoria, SeguimientodeTrabajo, AgregarPersona, EliminarPermoiso, PermisosUsuarios);
        }

        public DataTable Obtenerpermisos(int Identificaacion)
        {
            return usuarioDAL.ObtenerPermisos(Identificaacion);
        }
        public bool InsertarPersonasAdministrativas(string Identificacion, string Nombre, string Apellido1, string Apellido2,
        string Correo, string Usuario, string Contraseña,
        bool CrearReporte, bool RespuestaSolicitud, bool Auditoria,
        bool SeguimientodeTrabajo, bool AgregarPersona, bool EliminarPersona, bool PermisosUsuarios)

        {

            return usuarioDAL.InsertarPersonasAdministrativo(Identificacion, Nombre, Apellido1, Apellido2,
            Correo, Usuario, Contraseña,
            CrearReporte, RespuestaSolicitud, Auditoria,
            SeguimientodeTrabajo, AgregarPersona, EliminarPersona, PermisosUsuarios);
        }


        public bool GuardarReporte(string nombre, string apellido1, string apellido2,
       string departamento, string tipoIncidencia, DateTime fechaInicio,
       DateTime fechaFinal, string observaciones, string nombreCreador,
       string apellido1Creador, string apellido2Creador, string estado,
       out int idReporte)  // Agregamos el parámetro de salida
        {
            return usuarioDAL.ReportePersonas(nombre, apellido1, apellido2,
                departamento, tipoIncidencia, fechaInicio, fechaFinal,
                observaciones, nombreCreador, apellido1Creador, apellido2Creador, estado,
                out idReporte);  // Pasamos el `out int idReporte` a la capa de datos
        }



        public void GuardarArchivo(int idReporte, string nombreArchivo, byte[] archivo)
        {
            usuarioDAL.InsertarArchivo(idReporte, nombreArchivo, archivo);
        }


        //Manejando el select de los reportes.
        public DataTable ObtenerReportes(string nombre, string apellido1, string apellido2, DateTime? fechaInicio, DateTime? fechaFinal, string tipoIncidencia)
        {
            return usuarioDAL.BuscarPersonasConReportes(nombre, apellido1, apellido2, fechaInicio, fechaFinal, tipoIncidencia);
        }

        public DataTable ObtenerPersonas(string Nombre, string Apellido1, string Apellido2)
        {
            return usuarioDAL.BuscarPersonas( Nombre,  Apellido1,  Apellido2);
        }

        public DataTable ObtenerPermisos(string identificacion,string Nombre, string Apellido1, string Apellido2)
        {
            return usuarioDAL.ObtenerPermisos(identificacion, Nombre,Apellido1,Apellido2);
        }

        public DataTable ObtenerPersonasPermisos(string Nombre, string Apellido1, string Apellido2)
        {
            return usuarioDAL.BuscarPersonasPermisos(Nombre, Apellido1, Apellido2);
        }

        //Manejo de obtener archivos
        public List<byte[]> ObtenerArchivos(int idReporte)
        {
            UsuarioDAL dal = new UsuarioDAL();
            return dal.ObtenerArchivosPorId(idReporte);
        }


        public DataTable ObtenerJustificaciones(int? idJustificacionReporte = null)
        {
            return usuarioDAL.ObtenerJustificaciones(idJustificacionReporte);
        }

        public bool ActualizarEstadoJustificacion(int idJustificacionReporte, string nuevoEstado)
        {
            if (nuevoEstado != "Aprobada" && nuevoEstado != "Rechazada")
            {
                throw new ArgumentException("El estado debe ser 'Aprobada' o 'Rechazada'.");
            }
            return usuarioDAL.ActualizarEstadoJustificacion(idJustificacionReporte, nuevoEstado);
        }
        public bool CompletarReporte(int idReporte)
        {
            return usuarioDAL.CompletarReporte(idReporte);
        }

        public bool AgregarJustificacion(int idPersona, int idReporte, string justificacion)
        {
            if (idPersona <= 0)
                throw new ArgumentException("El ID de la persona no es válido.");

            if (idReporte <= 0)
                throw new ArgumentException("El ID del reporte no es válido.");

            if (string.IsNullOrWhiteSpace(justificacion))
                throw new ArgumentException("La justificación no puede estar vacía.");

            return usuarioDAL.InsertarJustificacion(idPersona,idReporte, justificacion);
        }


        public bool EnviarTokenRecuperacion(string email)
        {
            if (usuarioDAL.ValidarCorreo(email)) // Verifica si el correo existe en la BD
            {
                string token = Guid.NewGuid().ToString(); // Genera un token único
                usuarioDAL.GuardarToken(email, token); // Guarda el token en la BD

                // Enviar correo con el token
                string asunto = "Recuperación de contraseña";
                string mensaje = $"Su código de recuperación es: {token}";

                return EnviarCorreo(email, asunto, mensaje); // Llamar a la función de envío de correo
            }
            return false;
        }

        private static string GenerarToken()
        {
            return Guid.NewGuid().ToString().Substring(0, 8); // Código de 8 caracteres
        }


        public bool EnviarCorreo(string destinatario, string asunto, string mensaje)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("municipalidad261@gmail.com"); // Cambia esto por tu correo
                mail.To.Add(destinatario);
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com"); // Servidor SMTP
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential("municipalidad261@gmail.com", "brtu ufou hsfa cxay"); // Cambia por tu correo y contraseña
                smtp.EnableSsl = true;
                smtp.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar correo: " + ex.Message);
                return false;
            }
        }

        public bool VerificarYActualizarContrasena(string email, string token, string nuevaContrasena)
        {
            if (usuarioDAL.VerificarToken(email, token))
            {
                usuarioDAL.ActualizarContrasena(email, nuevaContrasena);
                return true;
            }
            return false;
        }

        public bool ModificarPersona(int IdPersona, string Identificacion, string Nombre, string Apellido1, string Apellido2, string Correo, string Usuario, string Contraseña)

        {
            return usuarioDAL.ModificarPersona(IdPersona, Identificacion, Nombre, Apellido1, Apellido2, Correo, Usuario, Contraseña);
        }

        public bool ModificarContraseña(int IdPersona, string Contraseña)

        { 
            return usuarioDAL.ModificarContraseña(IdPersona,Contraseña);
        }

        public bool EliminarPersona(int idPersona)
        {
            return usuarioDAL.EliminarPersona(idPersona);
        }


        public void ActualizarPermisos(string identificacion, int crearReporte, int respuestaSolicitud, int auditoria, int seguimientoTrabajo, int agregarPersona, int eliminarPersona, int permisosUsuario)
        {

            usuarioDAL.ActualizarPermisos(identificacion, crearReporte, respuestaSolicitud, auditoria, seguimientoTrabajo, agregarPersona, eliminarPersona, permisosUsuario);
        }

    }
}
