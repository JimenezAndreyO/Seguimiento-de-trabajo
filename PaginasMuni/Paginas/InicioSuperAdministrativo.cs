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
using Negocios;
using DAL.Entidades;

namespace PaginasMuni
{
    public partial class InicioSuperAdministrativo : Form
    {

        private UsuarioSesion perfil;
        public InicioSuperAdministrativo(UsuarioSesion perfil)
        {
            InitializeComponent();
            this.perfil = perfil;
            lblNombreUsuario.Text = $"Bienvenido, {perfil.Nombre}";
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

        private void btnSeguimientoTrabajo_Click(object sender, EventArgs e)
        {
            SeguimientoDeTrabajo form = new SeguimientoDeTrabajo(perfil);

            form.FormClosed += (s, args) => { };

            form.Show();
        }

        private void btnAgregarUusuario_Click(object sender, EventArgs e)
        {
            InsertarPersonas form = new InsertarPersonas();
            form.FormClosed += (s, args) => { }; // Eliminar this.Close()
            form.Show();
        }

        private void btnAgregarAdministrativo_Click(object sender, EventArgs e)
        {
            AgregarPersonasAdministrativas form = new AgregarPersonasAdministrativas();
            form.FormClosed += (s, args) => { }; // Eliminar this.Close()
            form.Show();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Hide(); // Oculta la ventana actual
            Form1 form = new Form1();
            form.FormClosed += (s, args) => this.Close();
            form.Show();
        }

        private void InicioSuperAdministrativo_Load(object sender, EventArgs e)
        {

        }

        private void btnModificaryEliminar_Click(object sender, EventArgs e)
        {
            ModificaryEliminarPerfil form = new ModificaryEliminarPerfil();
            form.FormClosed += (s, args) => { }; // Eliminar this.Close()
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CambiarPermisosUsuario form = new CambiarPermisosUsuario();
            form.FormClosed += (s, args) => { }; // Eliminar this.Close()
            form.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CambioDeContraseña form = new CambioDeContraseña(perfil);
            form.FormClosed += (s, args) => { }; // Eliminar this.Close()
            form.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void cambioDeContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambioDeContraseña form = new CambioDeContraseña(perfil);
            form.FormClosed += (s, args) => { }; // Eliminar this.Close()
            form.Show();
        }

        private void cambioDeContraseñaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CambioDeContraseña form = new CambioDeContraseña(perfil);
            form.FormClosed += (s, args) => { }; // Eliminar this.Close()
            form.Show();
        }
    }
}
