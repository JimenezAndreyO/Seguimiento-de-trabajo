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
using DAL.Entidades;

namespace PaginasMuni
{
    public partial class CambioDeContraseña : Form
    {
        private UsuarioBLL usuarioBLL = new UsuarioBLL();
        private UsuarioSesion perfil;

        public CambioDeContraseña(UsuarioSesion usuario)
        {
            InitializeComponent();
            perfil = usuario;
        }

        private void CambioDeContraseña_Load(object sender, EventArgs e)
        {

            txtIdPersona.Text = perfil.IdPersona.ToString();
            txtIdPersona.ReadOnly = true;
       
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                
             
                int IdPerfil = int.Parse(txtIdPersona.Text.Trim());
                string NuevaContraseña = txtNuevaContraseña.Text.Trim();
                string ConfirmaContraseña = txtConfirmarContraseña.Text.Trim();
               
               

                if (NuevaContraseña == ConfirmaContraseña)
                {
                    usuarioBLL.ModificarContraseña(IdPerfil, NuevaContraseña);
                    MessageBox.Show("Contraseña cambiada con existo");
                }
                else
                {
                    MessageBox.Show("Contraseña y confirmar contraseña no son iguales");
                }
            }
            catch
            {
                MessageBox.Show("Error a lo hora de cambiar contraseña");
            }
        }
    }
}
