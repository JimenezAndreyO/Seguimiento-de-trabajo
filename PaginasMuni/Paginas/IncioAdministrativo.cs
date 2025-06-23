using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using DAL.Entidades;
using Negocios;

namespace PaginasMuni
{
    public partial class IncioAdministrativo: Form
    {
        private UsuarioSesion perfil;

        private UsuarioBLL usuarioBLL = new UsuarioBLL(); // Se mantiene igual


        private void IncioAdministrativo_Load(object sender, EventArgs e)
        {
           // CargarPermisos(perfil.Identificacion);
        }

        public IncioAdministrativo(UsuarioSesion perfil)
        {
            InitializeComponent();
            this.perfil = perfil;
         

            
            if (perfil != null)
            {
                lblNombreUsuario.Text = $"Bienvenido, {perfil.Nombre}";
                CargarPermisos();
            }
            else
            {
                MessageBox.Show("No se pudieron cargar los datos del usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button6_Click(object sender, EventArgs e)
        {
            InsertarPersonas form = new InsertarPersonas();
            form.FormClosed += (s, args) => { }; // Eliminar this.Close()
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reporte form = new Reporte(perfil);
            form.FormClosed += (s, args) => { }; // Eliminar this.Close()
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Auditoria form = new Auditoria();
            form.FormClosed += (s, args) => { }; // Eliminar this.Close()
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide(); // Oculta la ventana actual
            Form1 form = new Form1();
            form.FormClosed += (s, args) => this.Close();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RespuestaSolicitud form = new RespuestaSolicitud();
            form.FormClosed += (s, args) => { }; // Eliminar this.Close()
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
          
            SeguimientoDeTrabajo form = new SeguimientoDeTrabajo(perfil);

            form.FormClosed += (s, args) => { };  

            form.Show();  
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ModificaryEliminarPerfil form = new ModificaryEliminarPerfil();
            form.FormClosed += (s, args) => { }; // Eliminar this.Close()
            form.Show();
        }

        private void CargarPermisos()
        {
            if (perfil == null || string.IsNullOrEmpty(perfil.Identificacion))
            {
                MessageBox.Show("No se pudo cargar la información del usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (int.TryParse(perfil.Identificacion, out int identificacionNumerica))
            {
                DataTable permisos = usuarioBLL.Obtenerpermisos(identificacionNumerica);

                if (permisos.Rows.Count > 0)
                {
                    button2.Enabled = Convert.ToBoolean(permisos.Rows[0]["CrearReporte"]);
                    button3.Enabled = Convert.ToBoolean(permisos.Rows[0]["RespuestaSolicitud"]);
                    button4.Enabled = Convert.ToBoolean(permisos.Rows[0]["Auditoria"]);
                    button5.Enabled = Convert.ToBoolean(permisos.Rows[0]["SeguimientodeTrabajo"]);
                    button6.Enabled = Convert.ToBoolean(permisos.Rows[0]["AgregarPersona"]);
                    btnEliminar.Enabled = Convert.ToBoolean(permisos.Rows[0]["EliminarPersona"]);
                     btnCambiarPermisos.Enabled = Convert.ToBoolean(permisos.Rows[0]["PermisosUsuarios"]);
                }
                else
                {
                    MessageBox.Show("No se pudieron cargar los permisos del usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("La identificación no es válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CambioDeContraseña form = new CambioDeContraseña(perfil);
            form.FormClosed += (s, args) => { }; // Eliminar this.Close()
            form.Show();
        }

        private void cambioDeContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambioDeContraseña form = new CambioDeContraseña(perfil);
            form.FormClosed += (s, args) => { }; // Eliminar this.Close()
            form.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CambiarPermisosUsuario form = new CambiarPermisosUsuario();
            form.FormClosed += (s, args) => { }; // Eliminar this.Close()
            form.Show();
        }
    }
}
