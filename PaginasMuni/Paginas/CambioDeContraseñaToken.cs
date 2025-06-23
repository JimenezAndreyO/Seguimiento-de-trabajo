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
using DAL;
using DAL.Entidades;

namespace PaginasMuni
{
    public partial class CambioDeContraseñaToken : Form
    {
        public CambioDeContraseñaToken()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            UsuarioBLL usuarioBLL = new UsuarioBLL(); // Crear instancia

            string email = txtCorreo.Text;
            string token = txtToken.Text;
            string nuevaContrasena = txtNuevaContraseña.Text;
            string ConfirmarContraseña = txtConfirmarContraseña.Text; 

            if (nuevaContrasena == ConfirmarContraseña)
            {
                if (usuarioBLL.VerificarYActualizarContrasena(email, token, nuevaContrasena))
                {
                    MessageBox.Show("Contraseña actualizada con éxito.", "Éxito");
                    this.Close(); // Cerrar formulario
                }
                else
                {
                    MessageBox.Show("Token inválido o expirado.", "Error");
                }
            }
            else
            {

                MessageBox.Show("Contraseñas no son iguales");
            }
          
        }

        private void CambioDeContraseñaToken_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide(); // Oculta la ventana actual
            Form1 form = new Form1();
            form.FormClosed += (s, args) => this.Close();
            form.Show();
        }
    }
}
