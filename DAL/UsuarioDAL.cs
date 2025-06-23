using System;
using System.Data.SqlClient;
using System.Data;
using DAL.Entidades;
using static DAL.Entidades.UsuarioSesion;
using System.Collections.Generic;


namespace DAL
{
    public class UsuarioDAL
    {

        public readonly ConexionBD conexion = new ConexionBD();

        public DataTable ObtenerPermisos(int IdPermisoUsuario)
        {

            using (SqlConnection conn = conexion.ObtenerConexion())

            {
                string query = "Select * from PermisosUsuarios where Identificacion = @Identificacion";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("Identificacion", IdPermisoUsuario);


                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;

            }
        }




        public bool ValidarUsuario(string Usuario, string Contraseña, out string mensaje, out int rol)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                // Nombre del procedimiento almacenado
                string storedProcedure = "VerificarUsuario";

                using (SqlCommand cmd = new SqlCommand(storedProcedure, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros de entrada
                    cmd.Parameters.AddWithValue("@Usuario", Usuario);
                    cmd.Parameters.AddWithValue("@Contraseña", Contraseña);

                    // Parámetros de salida
                    SqlParameter outputRol = new SqlParameter("@Rol", SqlDbType.Int);  // Cambiado a SqlDbType.Int
                    outputRol.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputRol);

                    SqlParameter outputEstado = new SqlParameter("@Estado", SqlDbType.NVarChar, 50);
                    outputEstado.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputEstado);

                    SqlParameter outputMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 255);
                    outputMensaje.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputMensaje);

                    // Ejecutar el procedimiento almacenado
                    cmd.ExecuteNonQuery();

                    // Obtener el mensaje y el rol de salida
                    mensaje = outputMensaje.Value.ToString();

                    // Comprobar si el rol tiene un valor válido (no NULL) o es 0 en caso de error
                    rol = (outputRol.Value != DBNull.Value) ? (int)outputRol.Value : 0;

                    // Si el mensaje es de éxito, se retorna true
                    return mensaje == "Usuario autenticado correctamente.";
                }
            }
        }

        public  bool InsertarPersona(
        string Identificacion, string Nombre, string Apellido1, string Apellido2,
        string Correo, string Usuario, string Contraseña,
        bool CrearReporte, bool RespuestaSolicitud, bool Auditoria,
        bool SeguimientodeTrabajo, bool AgregarPersona, bool EliminarPersona, bool PermisosUsuarios)
        {
            try
            {


                using (SqlConnection conn = conexion.ObtenerConexion())
                {
                    conn.Open();
                    string storedProcedure = "BuscarEInsertarReporte";

                    using (SqlCommand cmd = new SqlCommand(storedProcedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@Identificacion", Identificacion);
                        cmd.Parameters.AddWithValue("@Nombre", Nombre);
                        cmd.Parameters.AddWithValue("@Apellido1", Apellido1);
                        cmd.Parameters.AddWithValue("@Apellido2", Apellido2);
                        cmd.Parameters.AddWithValue("@Correo", Correo);
                        cmd.Parameters.AddWithValue("@Usuario", Usuario);
                        cmd.Parameters.AddWithValue("@Contraseña", Contraseña);

                        // Parámetros de permisos (booleanos)
                        cmd.Parameters.AddWithValue("@CrearReporte", CrearReporte);
                        cmd.Parameters.AddWithValue("@RespuestaSolicitud", RespuestaSolicitud);
                        cmd.Parameters.AddWithValue("@Auditoria", Auditoria);
                        cmd.Parameters.AddWithValue("@SeguimientodeTrabajo", SeguimientodeTrabajo);
                        cmd.Parameters.AddWithValue("@AgregarPersona", AgregarPersona);
                        cmd.Parameters.AddWithValue("@EliminarPersona", EliminarPersona);
                        cmd.Parameters.AddWithValue("@PermisosUsuarios", PermisosUsuarios);

                        // Ejecutar el procedimiento almacenado
                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en DAL: " + ex.Message);
                return false;
            }
        }

        public bool ReportePersonas(string nombre, string apellido1, string apellido2, string departamento,
                                  string tipoIncidencia, DateTime fechaInicio, DateTime fechaFinal,
                                  string observaciones, string nombrePersonaReporte,
                                  string apellidoPersonaReporte, string apellido2PersonaReporte,
                                  string estado, out int idReporte)
        {
            idReporte = -1; // Inicializamos con un valor inválido

            try
            {
                using (SqlConnection conn = conexion.ObtenerConexion())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("BuscarEInsertarReporte", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Nombre", string.IsNullOrEmpty(nombre) ? (object)DBNull.Value : nombre);
                        cmd.Parameters.AddWithValue("@Apellido1", string.IsNullOrEmpty(apellido1) ? (object)DBNull.Value : apellido1);
                        cmd.Parameters.AddWithValue("@Apellido2", string.IsNullOrEmpty(apellido2) ? (object)DBNull.Value : apellido2);
                        cmd.Parameters.AddWithValue("@Departamento", string.IsNullOrEmpty(departamento) ? (object)DBNull.Value : departamento);
                        cmd.Parameters.AddWithValue("@TipoIncidencia", string.IsNullOrEmpty(tipoIncidencia) ? (object)DBNull.Value : tipoIncidencia);
                        cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFinal", fechaFinal);
                        cmd.Parameters.AddWithValue("@Observaciones", string.IsNullOrEmpty(observaciones) ? (object)DBNull.Value : observaciones);
                        cmd.Parameters.AddWithValue("@NombrePersonaReporte", string.IsNullOrEmpty(nombrePersonaReporte) ? (object)DBNull.Value : nombrePersonaReporte);
                        cmd.Parameters.AddWithValue("@ApellidoPersonaReporte", string.IsNullOrEmpty(apellidoPersonaReporte) ? (object)DBNull.Value : apellidoPersonaReporte);
                        cmd.Parameters.AddWithValue("@Apellido2PersonaReporte", string.IsNullOrEmpty(apellido2PersonaReporte) ? (object)DBNull.Value : apellido2PersonaReporte);
                        cmd.Parameters.AddWithValue("@Estado", string.IsNullOrEmpty(estado) ? (object)DBNull.Value : estado);

                        // Parámetro de salida para obtener el ID del reporte insertado
                        SqlParameter outputParam = new SqlParameter("@IdReporte", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        // Ejecutamos el procedimiento almacenado
                        cmd.ExecuteNonQuery();

                        // Obtenemos el valor del parámetro de salida después de ejecutar la consulta
                        if (outputParam.Value != DBNull.Value)
                        {
                            idReporte = Convert.ToInt32(outputParam.Value);
                        }

                        return idReporte > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar reporte: " + ex.Message);
                return false;
            }
        }



        public void InsertarArchivo(int idReporte, string nombreArchivo, byte[] archivo)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("InsertarArchivoReporte", conn))
                {


                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDReporte", idReporte);
                    cmd.Parameters.AddWithValue("@NombreArchivo", nombreArchivo);
                    cmd.Parameters.AddWithValue("@Archivo", archivo);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        public UsuarioSesion ObtenerPerfilPorUsuario(string usuario)
        {
            UsuarioSesion perfil = null;

            try
            {
                using (SqlConnection conn = conexion.ObtenerConexion())
                {
                    conn.Open();

                    string query = "SELECT IdPersona, Identificacion, Nombre, Apellido1, Apellido2, Correo, Usuario, Estado FROM Personas WHERE Usuario = @Usuario";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Usuario", usuario);


                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                perfil = new UsuarioSesion
                                {
                                    IdPersona = reader["IdPersona"] != DBNull.Value ? Convert.ToInt32(reader["IdPersona"]) : 0, // Si IdPersona es int
                                    Identificacion = reader["Identificacion"].ToString(),
                                    Nombre = reader["Nombre"].ToString(),
                                    Apellido1 = reader["Apellido1"].ToString(),
                                    Apellido2 = reader["Apellido2"].ToString(),
                                    Usuario = reader["Usuario"].ToString(),
                                    Correo = reader["Correo"].ToString(),
                                    Estado = reader["Estado"].ToString()
                                };

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el perfil del usuario: " + ex.Message);
            }

            return perfil;
        }

        public DataTable BuscarPersonas(string Nombre, string Apellido1, String Apellido2)
        {

            DataTable dt = new DataTable(); 
            try
            {
                using (SqlConnection conn = conexion.ObtenerConexion())
                {

                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("buscarpersonas", conn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", (object)Nombre ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Apellido1", (object)Apellido1 ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Apellido2", (object)Apellido2 ?? DBNull.Value);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }

                    }
                }

            }
            catch(Exception ex)
            {
                throw new Exception("Error al obtener los reportes: " + ex.Message);
            }

            return dt;
        }


        public DataTable BuscarPersonasPermisos(string Nombre, string Apellido1, String Apellido2)
        {

            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = conexion.ObtenerConexion())
                {

                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("buscarpersonasPermisos", conn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", (object)Nombre ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Apellido1", (object)Apellido1 ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Apellido2", (object)Apellido2 ?? DBNull.Value);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los reportes: " + ex.Message);
            }

            return dt;
        }
        public DataTable BuscarPersonasConReportes(string nombre, string apellido1, string apellido2, DateTime? fechaInicio, DateTime? fechaFinal, string tipoIncidencia)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = conexion.ObtenerConexion())
                {
                    conn.Open(); 

                    using (SqlCommand cmd = new SqlCommand("BuscarPersonasConReportes", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros y manejar valores NULL correctamente
                        cmd.Parameters.AddWithValue("@Nombre", (object)nombre ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Apellido1", (object)apellido1 ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Apellido2", (object)apellido2 ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@FechaInicio", (object)fechaInicio ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@FechaFinal", (object)fechaFinal ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TipoIncidencia", (object)tipoIncidencia ?? DBNull.Value);
                        //cmd.Parameters.AddWithValue("@NombrePersonaReporte", (object)NombrePersonaReporte ?? DBNull.Value);
                        //cmd.Parameters.AddWithValue("@ApellidoPersonaReporte ", (object)ApellidoPersonaReporte ?? DBNull.Value);
                        //cmd.Parameters.AddWithValue("@Apellido2PersonaReporte ", (object)Apellido2PersonaReporte ?? DBNull.Value);

                        // ✅ Usar SqlDataAdapter para ejecutar la consulta y llenar el DataTable
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los reportes: " + ex.Message);
            }

            return dt; // ✅ Devolver el DataTable con los resultados
        }

        public List<byte[]> ObtenerArchivosPorId(int idReporte)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();
                string storedProc = "ObtenerArchivosPorIdReporte"; // Nombre del procedimiento almacenado
                using (SqlCommand cmd = new SqlCommand(storedProc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdReporte", idReporte);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<byte[]> archivos = new List<byte[]>();

                        while (reader.Read())
                        {
                            // Verifica si el archivo no es DBNull
                            if (reader["Archivo"] != DBNull.Value)
                            {
                                archivos.Add((byte[])reader["Archivo"]);
                            }
                        }

                        return archivos;
                    }
                }
            }
        }


        // Manejo de reportes de usuarios activos
        public List<DAL.Entidades.Reporte> ObtenerReportesActivos(int idPersona)
        {
            List<DAL.Entidades.Reporte> reportes = new List<DAL.Entidades.Reporte>();

            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("ObtenerReportesActivos", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPersona", idPersona);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    reportes.Add(new DAL.Entidades.Reporte
                    {
                        IdReporte = reader.GetInt32(reader.GetOrdinal("IdReporte")),
                        IdPersona = idPersona, // Ya se pasa como parámetro
                        Departamento = reader.IsDBNull(reader.GetOrdinal("Departamento")) ? "" : reader["Departamento"].ToString(),
                        TipoIncidencia = reader.IsDBNull(reader.GetOrdinal("TipoIncidencia")) ? "" : reader["TipoIncidencia"].ToString(),
                        FechaInicio = reader.IsDBNull(reader.GetOrdinal("FechaInicio")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("FechaInicio")),
                        FechaFinal = reader.IsDBNull(reader.GetOrdinal("FechaFinal")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("FechaFinal")),
                        Observaciones = reader.IsDBNull(reader.GetOrdinal("Observaciones")) ? "" : reader["Observaciones"].ToString(),
                        NombrePersonaReporte = reader.IsDBNull(reader.GetOrdinal("NombrePersonaReporte")) ? "" : reader["NombrePersonaReporte"].ToString(),
                        ApellidoPersonaReporte = reader.IsDBNull(reader.GetOrdinal("ApellidoPersonaReporte")) ? "" : reader["ApellidoPersonaReporte"].ToString(),
                        Apellido2PersonaReporte = reader.IsDBNull(reader.GetOrdinal("Apellido2PersonaReporte")) ? "" : reader["Apellido2PersonaReporte"].ToString(),
                        Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? "" : reader["Estado"].ToString()
                    });
                }
            }
            return reportes;
        }

        public DataTable ObtenerPermisos(string identificacion, string nombre, string apellido1, string apellido2)
        {
            DataTable permisos = new DataTable();
            try
            {
                using (SqlConnection conn = conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("ObtenerPermisosPorIdentificacion", conn)) 
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@Identificacion", identificacion);
                        cmd.Parameters.AddWithValue("@Identificacion", (object)identificacion ?? DBNull.Value);

                        cmd.Parameters.AddWithValue("@Nombre", (object)nombre ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Apellido1", (object)apellido1 ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Apellido2", (object)apellido2 ?? DBNull.Value);
                      
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(permisos);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los reportes: " + ex.Message);
            }
            return permisos;
        }

        public bool CompletarReporte(int idReporte)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("CompletarReporte", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdReporte", idReporte);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public byte[] ObtenerArchivo(int idReporte)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("ObtenerArchivo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdReporte", idReporte);
                conn.Open();

                object result = cmd.ExecuteScalar();
                return result != DBNull.Value ? (byte[])result : null;
            }
        }
        public bool JustificarReporte(int idReporte, string justificacion)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("JustificarReporte", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdReporte", idReporte);
                cmd.Parameters.AddWithValue("@Justificacion", justificacion);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        //Modificar Personas 


        public bool ModificarPersona(int IdPersona, string Identificacion, string Nombre, string Apellido1, string Apellido2, string Correo, string Usuario, string Contraseña)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("ModificarPersona", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdPersona", IdPersona);
                        cmd.Parameters.AddWithValue("@Identificacion", Identificacion);
                        cmd.Parameters.AddWithValue("@Nombre", Nombre);
                        cmd.Parameters.AddWithValue("@Apellido1", Apellido1);
                        cmd.Parameters.AddWithValue("@Apellido2", Apellido2);
                        cmd.Parameters.AddWithValue("@Correo", Correo);
                        cmd.Parameters.AddWithValue("@Usuario", Usuario);
                        cmd.Parameters.AddWithValue("@Contraseña", Contraseña);

                        // Ejecutar consulta y obtener filas afectadas
                        object result = cmd.ExecuteScalar();
                        int resultado = (result != null) ? Convert.ToInt32(result) : 0;
                        return resultado > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al modificar persona: " + ex.Message);
                }
            }
        }


        public bool InsertarJustificacion(int idPersona, int idReporte, string justificacion)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("InsertarJustificacionReporte", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdPersona", idPersona);
                        cmd.Parameters.AddWithValue("@IdReporte", idReporte);
                        cmd.Parameters.AddWithValue("@Justificacion", justificacion);

                        SqlParameter returnValue = new SqlParameter();
                        returnValue.Direction = ParameterDirection.ReturnValue;
                        cmd.Parameters.Add(returnValue);

                        cmd.ExecuteNonQuery();
                        int rowsAffected = Convert.ToInt32(returnValue.Value);
                        return rowsAffected > 0; // Devuelve true si se insertó correctamentete

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al insertar la justificación: " + ex.Message);
                }
            }

        }

        public bool EliminarPersona(int idPersona)
        {

            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("EliminarPersona", conn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdPersona", idPersona);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }

                catch (Exception)
                {
                    return false;

                }
            }
        }

        public DataTable ObtenerJustificaciones(int? idJustificacionReporte = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("VerYActualizarJustificacionReporte", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdJustificacionReporte", (object)idJustificacionReporte ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NuevoEstado", DBNull.Value); // No actualizar, solo consulta

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }


        public bool ActualizarEstadoJustificacion(int idJustificacionReporte, string nuevoEstado)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("VerYActualizarJustificacionReporte", conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdJustificacionReporte", idJustificacionReporte);
                    cmd.Parameters.AddWithValue("@NuevoEstado", nuevoEstado);

                    // Parámetro de retorno
                    SqlParameter returnValue = new SqlParameter();
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnValue);

                    cmd.ExecuteNonQuery();

                    // Obtener el valor de retorno del procedimiento almacenado
                    int filasAfectadas = Convert.ToInt32(returnValue.Value);
                    return filasAfectadas > 0;
                }
            }
        }

        public bool InsertarPersonasAdministrativo(string Identificacion, string Nombre, string Apellido1, string Apellido2,
        string Correo, string Usuario, string Contraseña,
        bool CrearReporte, bool RespuestaSolicitud, bool Auditoria,
        bool SeguimientodeTrabajo, bool AgregarPersona, bool EliminarPersona, bool PermisosUsuarios)
        {

            {
                try
                {


                    using (SqlConnection conn = conexion.ObtenerConexion())
                    {
                        conn.Open();
                        string storedProcedure = "InsertarPersonaAdministrativa";

                        using (SqlCommand cmd = new SqlCommand(storedProcedure, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Parámetros de entrada
                            cmd.Parameters.AddWithValue("@Identificacion", Identificacion);
                            cmd.Parameters.AddWithValue("@Nombre", Nombre);
                            cmd.Parameters.AddWithValue("@Apellido1", Apellido1);
                            cmd.Parameters.AddWithValue("@Apellido2", Apellido2);
                            cmd.Parameters.AddWithValue("@Correo", Correo);
                            cmd.Parameters.AddWithValue("@Usuario", Usuario);
                            cmd.Parameters.AddWithValue("@Contraseña", Contraseña);

                            // Parámetros de permisos (booleanos)
                            cmd.Parameters.AddWithValue("@CrearReporte", CrearReporte);
                            cmd.Parameters.AddWithValue("@RespuestaSolicitud", RespuestaSolicitud);
                            cmd.Parameters.AddWithValue("@Auditoria", Auditoria);
                            cmd.Parameters.AddWithValue("@SeguimientodeTrabajo", SeguimientodeTrabajo);
                            cmd.Parameters.AddWithValue("@AgregarPersona", AgregarPersona);
                            cmd.Parameters.AddWithValue("@EliminarPersona", EliminarPersona);
                            cmd.Parameters.AddWithValue("@PermisosUsuarios", PermisosUsuarios);

                            // Ejecutar el procedimiento almacenado
                            int rowsAffected = cmd.ExecuteNonQuery();

                            return rowsAffected > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error en DAL: " + ex.Message);
                    return false;
                }
            }
        }
        //Manejo de olvide contraseña 

        public bool ValidarCorreo(string email)
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConexion())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Personas WHERE Correo = @Correo", conn);
                    cmd.Parameters.AddWithValue("@Correo", email);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al validar correo: " + ex.Message);
                return false; // Retorna false en caso de error
            }
        }

        public void GuardarToken(string email, string token)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Personas SET TokenRecuperacion = @Token WHERE Correo = @Correo", conn);
                cmd.Parameters.AddWithValue("@Token", token);
                cmd.Parameters.AddWithValue("@Correo", email);
                cmd.ExecuteNonQuery();
            }

        }

        public bool VerificarToken(string email, string token)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Personas WHERE Correo = @Correo AND TokenRecuperacion = @Token", conn);
                cmd.Parameters.AddWithValue("@Correo", email);
                cmd.Parameters.AddWithValue("@Token", token);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }


        public  void ActualizarContrasena(string email, string ContraseñaNueva)
        {
         
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Personas SET Contraseña = @Contraseña, TokenRecuperacion = NULL WHERE Correo = @Correo", conn);
            

                cmd.Parameters.AddWithValue("@Contraseña", ContraseñaNueva);
                cmd.Parameters.AddWithValue("@Correo", email);
                cmd.ExecuteNonQuery();
            }
        }
        public static void ActualizarReporte(string identificacion, int crearReporte, int respuestaSolicitud, int auditoria, int seguimientoTrabajo, int agregarPersona, int eliminarPersona)
        {

            //using (SqlConnection conn = conexion.ObtenerConexion())
            //{
            //    conn.Open();

            //    EjecutarProcedimiento(conn, identificacion, "CrearReporte", crearReporte);
            //    EjecutarProcedimiento(conn, identificacion, "RespuestaSolicitud", respuestaSolicitud);
            //    EjecutarProcedimiento(conn, identificacion, "Auditoria", auditoria);
            //    EjecutarProcedimiento(conn, identificacion, "SeguimientodeTrabajo", seguimientoTrabajo);
            //    EjecutarProcedimiento(conn, identificacion, "AgregarPersona", agregarPersona);
            //    EjecutarProcedimiento(conn, identificacion, "EliminarPersona", eliminarPersona);
            //}
        }

        public void EjecutarProcedimiento(SqlConnection conn, string identificacion, string permiso, int valor)
        {
            using (SqlCommand cmd = new SqlCommand("ModificarPermisoUsuario", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Identificacion", identificacion);
                cmd.Parameters.AddWithValue("@Permiso", permiso);
                cmd.Parameters.AddWithValue("@Valor", valor);
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarPermisos(string identificacion, int crearReporte, int respuestaSolicitud, int auditoria, int seguimientoTrabajo, int agregarPersona, int eliminarPersona, int permisosUsuario)
        {

            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();
                EjecutarProcedimiento(conn, identificacion, "CrearReporte", crearReporte);
                EjecutarProcedimiento(conn, identificacion, "RespuestaSolicitud", respuestaSolicitud);
                EjecutarProcedimiento(conn, identificacion, "Auditoria", auditoria);
                EjecutarProcedimiento(conn, identificacion, "SeguimientodeTrabajo", seguimientoTrabajo);
                EjecutarProcedimiento(conn, identificacion, "AgregarPersona", agregarPersona);
                EjecutarProcedimiento(conn, identificacion, "EliminarPersona", eliminarPersona);
                EjecutarProcedimiento(conn, identificacion, "PermisosUsuarios", permisosUsuario);

                
            }
        }
        public bool ModificarContraseña(int IdPersona, string Contraseña)
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConexion())
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("ModificarContraseñaPersona", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdPersona", IdPersona);
                        cmd.Parameters.AddWithValue("@Contraseña", Contraseña);

                        // Capturar el número de filas afectadas
                        int rowsAffected = Convert.ToInt32(cmd.ExecuteScalar());

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al modificar la contraseña: {ex.Message}");
                return false;
            }
        }

        public bool ActualizarEstado( int IdPersona)
        {

            try
            {
                using (SqlConnection conn = conexion.ObtenerConexion())
                {
                    conn.Open();

                    string storedProcedure = "CambiarEstadoPersona";

                    using (SqlCommand cmd = new SqlCommand(storedProcedure, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@IdPersona", IdPersona);
                     
                        // Capturar el número de filas afectadas
                       
                        int filasAfectadas = cmd.ExecuteNonQuery(); // Obtener filas afectadas

                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Registrar error en la consola
                Console.WriteLine("Error al insertar persona: " + ex.Message);
                return false; // Retornar false en caso de error
            }
            // Registrar error
        }


        public void guardarpermisos( string Identificacion, bool CrearReporte, bool RespuestaSolicitud, bool Auditoria, bool SeguimientodeTrabajo, bool AgregarPersona, bool EliminarPersona, bool PermisosUsuarios)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                string query = "insert into PermisosUsuarios(Identificacion, CrearReporte, RespuestaSolicitud, Auditoria ,SeguimientodeTrabajo, AgregarPersona, EliminarPersona,PermisosUsuarios)" +
                    "Values (@Identificacion, @CrearReporte, @RespuestaSolicitud, @Auditoria, @SeguimientoTrabajo, @AgregarPersona, @EliminarPersona, @PermisosUsuarios)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("Identificacion", Identificacion);
                cmd.Parameters.AddWithValue("CrearReporte", CrearReporte);
                cmd.Parameters.AddWithValue("RespuestaSolicitud", RespuestaSolicitud);
                cmd.Parameters.AddWithValue("Auditoria", Auditoria);
                cmd.Parameters.AddWithValue("SeguimientoTrabajo", SeguimientodeTrabajo);
                cmd.Parameters.AddWithValue("AgregarPersona", AgregarPersona);
                cmd.Parameters.AddWithValue("EliminarPersona", EliminarPersona);
                cmd.Parameters.AddWithValue("PermisosUsuarios", PermisosUsuarios);

                conn.Open();
                cmd.ExecuteNonQuery();


            }

        }


        public bool InsertarUsuarios(string Identificacion, string Nombre, string Apellido1, string Apellido2,
        string Correo, string Usuario, string Contraseña,
        bool CrearReporte, bool RespuestaSolicitud, bool Auditoria,
        bool SeguimientodeTrabajo, bool AgregarPersona, bool EliminarPersona, bool PermisosUsuarios)
        {

            {
                try
                {


                    using (SqlConnection conn = conexion.ObtenerConexion())
                    {
                        conn.Open();
                        string storedProcedure = "InsertarPersona";

                        using (SqlCommand cmd = new SqlCommand(storedProcedure, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Parámetros de entrada
                            cmd.Parameters.AddWithValue("@Identificacion", Identificacion);
                            cmd.Parameters.AddWithValue("@Nombre", Nombre);
                            cmd.Parameters.AddWithValue("@Apellido1", Apellido1);
                            cmd.Parameters.AddWithValue("@Apellido2", Apellido2);
                            cmd.Parameters.AddWithValue("@Correo", Correo);
                            cmd.Parameters.AddWithValue("@Usuario", Usuario);
                            cmd.Parameters.AddWithValue("@Contraseña", Contraseña);

                            // Parámetros de permisos (booleanos)
                            cmd.Parameters.AddWithValue("@CrearReporte", CrearReporte);
                            cmd.Parameters.AddWithValue("@RespuestaSolicitud", RespuestaSolicitud);
                            cmd.Parameters.AddWithValue("@Auditoria", Auditoria);
                            cmd.Parameters.AddWithValue("@SeguimientodeTrabajo", SeguimientodeTrabajo);
                            cmd.Parameters.AddWithValue("@AgregarPersona", AgregarPersona);
                            cmd.Parameters.AddWithValue("@EliminarPersona", EliminarPersona);
                            cmd.Parameters.AddWithValue("@PermisosUsuarios", PermisosUsuarios);

                            // Ejecutar el procedimiento almacenado
                            int rowsAffected = cmd.ExecuteNonQuery();

                            return rowsAffected > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error en DAL: " + ex.Message);
                    return false;
                }
            }
        }


    }
}