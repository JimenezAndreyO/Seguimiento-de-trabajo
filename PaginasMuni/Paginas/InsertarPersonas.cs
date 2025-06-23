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
    public partial class InsertarPersonas: Form
    {

        private UsuarioBLL usuarioBLL = new UsuarioBLL(); // Se mantiene igual
        public InsertarPersonas()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string Identificacion = txtidentificacion.Text.Trim();
                string Nombre = txtnombre.Text.Trim();
                string Apellido1 = txtapellido1.Text.Trim();
                string Apellido2 = txtapellido2.Text.Trim();
                string Correo = txtCorreo.Text.Trim();
                string Usuario = txtusuario.Text.Trim();
                string Contraseña = txtcontraseña.Text.Trim();

                // Obtener valores de permisos desde los CheckBoxes
                bool CrearReporte = chCrearReporte.Checked;
                bool RespuestaSolicitud = ChRespuestaSolicitud.Checked;
                bool Auditoria = ChAuditoria.Checked;
                bool SeguimientoTrabajo = ChSeguimientoTrabajo.Checked;
                bool AgregarPersona = ChAgregarPersonas.Checked;
                bool EliminarPermiso = ChEliminarModificar.Checked;
                bool ModificarPermisos = chAgregarModifcarPermisos.Checked;

                // Insertar usuario y permisos en un solo paso
                bool esValido = usuarioBLL.Insertar(Identificacion, Nombre, Apellido1, Apellido2, Correo, Usuario, Contraseña,
                                                    CrearReporte, RespuestaSolicitud, Auditoria, SeguimientoTrabajo,
                                                    AgregarPersona, EliminarPermiso, ModificarPermisos);

                if (esValido)
                {
                    MessageBox.Show("Usuario agregado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar los campos después de la inserción
                    txtidentificacion.Clear();
                    txtnombre.Clear();
                    txtapellido1.Clear();
                    txtapellido2.Clear();
                    txtCorreo.Clear();
                    txtusuario.Clear();
                    txtcontraseña.Clear();
                    chCrearReporte.Checked = false;
                    ChRespuestaSolicitud.Checked = false;
                    ChAuditoria.Checked = false;
                    ChSeguimientoTrabajo.Checked = false;
                    ChAgregarPersonas.Checked = false;
                    ChEliminarModificar.Checked = false;
                    chAgregarModifcarPermisos.Checked = false;
                }
                else
                {
                    MessageBox.Show("No se pudo agregar el usuario. Verifique los datos e intente nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al intentar agregar el usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
