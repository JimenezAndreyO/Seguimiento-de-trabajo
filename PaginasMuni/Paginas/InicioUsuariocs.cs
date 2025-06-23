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
using System.IO;
using DAL; 
using DAL.Entidades;// Importar la entidad

namespace PaginasMuni
{
    public partial class InicioUsuariocs: Form
    {
       

        private UsuarioBLL usuarioBLL = new UsuarioBLL(); // Se mantiene igual


        private UsuarioSesion perfil;
        public InicioUsuariocs(UsuarioSesion perfil)
        {   
            InitializeComponent();
            this.perfil = perfil;  // Guarda el usuario logueado   
            lblNombreUsuario.Text = $"Bienvenido, {perfil.Nombre}";

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



        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void InicioUsuariocs_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            SeguimientoDeTrabajo form = new SeguimientoDeTrabajo(perfil);
            form.FormClosed += (s, args) => { };  
            form.Show();  
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide(); // Oculta la ventana actual
            Form1 form = new Form1();
            form.FormClosed += (s, args) => this.Close();
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
                    btnCrearReporte.Enabled = Convert.ToBoolean(permisos.Rows[0]["CrearReporte"]);
                    btnRespuestaSolicitud.Enabled = Convert.ToBoolean(permisos.Rows[0]["RespuestaSolicitud"]);
                    btnAuditoria.Enabled = Convert.ToBoolean(permisos.Rows[0]["Auditoria"]);
                    btnSeguimientoTrabajo.Enabled = Convert.ToBoolean(permisos.Rows[0]["SeguimientodeTrabajo"]);
                    BtnAgregarPersona.Enabled = Convert.ToBoolean(permisos.Rows[0]["AgregarPersona"]);
                    BtnEliminaryModificarPersona.Enabled = Convert.ToBoolean(permisos.Rows[0]["EliminarPersona"]);
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

        private void btnCrearReporte_Click(object sender, EventArgs e)
        {
            Reporte form = new Reporte(perfil);
            form.FormClosed += (s, args) => { }; // Eliminar this.Close()
            form.Show();
        }

        private void btnRespuestaSolicitud_Click(object sender, EventArgs e)
        {
            RespuestaSolicitud form = new RespuestaSolicitud();
            form.FormClosed += (s, args) => { }; // Eliminar this.Close()
            form.Show();
        }

        private void btnAuditoria_Click(object sender, EventArgs e)
        {
            Auditoria form = new Auditoria();
            form.FormClosed += (s, args) => { }; // Eliminar this.Close()
            form.Show();
        }

        private void BtnAgregarPersona_Click(object sender, EventArgs e)
        {
            InsertarPersonas form = new InsertarPersonas();
            form.FormClosed += (s, args) => { }; // Eliminar this.Close()
            form.Show();
        }

        private void BtnEliminaryModificarPersona_Click(object sender, EventArgs e)
        {
            ModificaryEliminarPerfil form = new ModificaryEliminarPerfil();
            form.FormClosed += (s, args) => { }; // Eliminar this.Close()
            form.Show();
        }

        private void btnCambiarPermisos_Click(object sender, EventArgs e)
        {
            CambiarPermisosUsuario form = new CambiarPermisosUsuario();
            form.FormClosed += (s, args) => { }; // Eliminar this.Close()
            form.Show();
        }
    }
}
