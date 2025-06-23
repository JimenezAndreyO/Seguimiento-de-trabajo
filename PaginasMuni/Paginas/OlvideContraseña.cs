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
    public partial class OlvideContraseña : Form
    {
        //private UsuarioBLL usuarioBLL; // Capa de negocios
        //private UsuarioSesion usuarioActual; // Usuario en sesión
        //private UsuarioBLL usuarioBLL; // Capa de negocios





        public OlvideContraseña()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide(); // Oculta la ventana actual
            Form1 form = new Form1();
            form.FormClosed += (s, args) => this.Close();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UsuarioBLL usuarioBLL = new UsuarioBLL();
            string email = textBox1.Text;


            if (usuarioBLL.EnviarTokenRecuperacion(email))
            {
                MessageBox.Show("Se ha enviado un correo con las instrucciones.", "Recuperación de Contraseña");
                this.Close();
            }
            else
            {
                MessageBox.Show("Correo no registrado o error al enviar el correo.", "Error");
            }




        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide(); // Oculta la ventana actual
            CambioDeContraseñaToken form = new CambioDeContraseñaToken();
            form.FormClosed += (s, args) => this.Close();
            form.Show();
        }
    }
}
