using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocios;

namespace PaginasMuni
{
    public partial class AgregarPersonasAdministrativas: Form
    {


        private UsuarioBLL usuarioBLL = new UsuarioBLL(); // Se mantiene igual
        public AgregarPersonasAdministrativas()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string Identificacion = txtIdentificacion.Text.Trim();
                string Nombre = txtNombre.Text.Trim();
                string Apellido1 = txtApellido1.Text.Trim();
                string Apellido2 = txtApellido2.Text.Trim();
                string Correo = txtCorreo.Text.Trim();
                string Usuario = txtUsuario.Text.Trim();
                string Contraseña = txtContraseña.Text.Trim();

         

                    // Guardar los permisos en la base de datos
                    bool CrearReporte = chCrearReporte.Checked;
                    bool RespuestaSolicitud = ChRespuestaSolicitud.Checked;
                    bool Auditoria = ChAuditoria.Checked;
                    bool SeguimientoTrabajo = ChSeguimientoTrabajo.Checked;
                    bool AgregarPersona = ChAgregarPersonas.Checked;
                    bool EliminarPermiso = ChElimimarModificar.Checked;
                    bool ModificarPermisos = chModificarPermisos.Checked;

                 bool esValido=   usuarioBLL.InsertarPersonasAdministrativas(Identificacion, Nombre, Apellido1, Apellido2, Correo, Usuario, Contraseña,
                                                    CrearReporte, RespuestaSolicitud, Auditoria, SeguimientoTrabajo,
                                                    AgregarPersona, EliminarPermiso, ModificarPermisos);

                if(esValido)
                {
                    MessageBox.Show("Agregado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    // Limpiar los campos después de la inserción
                    txtIdentificacion.Clear();
                    txtNombre.Clear();
                    txtApellido1.Clear();
                    txtApellido2.Clear();
                    txtCorreo.Clear();
                    txtUsuario.Clear();
                    txtContraseña.Clear();
                    chCrearReporte.Checked = false;
                    ChRespuestaSolicitud.Checked = false;
                    ChAuditoria.Checked = false;
                    ChSeguimientoTrabajo.Checked = false;
                    ChAgregarPersonas.Checked = false;
                    ChElimimarModificar.Checked = false;
                    chModificarPermisos.Checked = false;

                }
               
             
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al intentar agregar el usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AgregarPersonasAdministrativas_Load(object sender, EventArgs e)
        {

        }
    }
}
