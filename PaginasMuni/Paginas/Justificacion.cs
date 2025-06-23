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
using DAL;
namespace PaginasMuni
{
    public partial class Justificacion : Form
    {

        private UsuarioSesion usuarioActual; // Usuario en sesión
        private UsuarioBLL usuarioBLL; // Capa de negocios

        public int IdReporte { get; set; }
        public string JustificacionText { get; set; }

        // Constructor
        public Justificacion(int idReporte, UsuarioSesion usuario)
        {

            InitializeComponent();
            usuarioBLL = new UsuarioBLL();
            usuarioActual = usuario ?? throw new ArgumentNullException(nameof(usuario), "El usuario en sesión no puede ser nulo.");
            IdReporte = idReporte;
        }

        // Cargar el formulario
        private void Justificacion_Load(object sender, EventArgs e)
        {
            // Configuración inicial si es necesario
        }

        // Método para obtener la justificación del usuario
        public string GetJustificacion()
        {
            return txtJustificacion.Text; // Asegúrate de que txtJustificacion sea el nombre del TextBox en el formulario
        }

        // Evento del botón "Aceptar" para guardar la justificación
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJustificacion.Text))
            {
                MessageBox.Show("Por favor, ingrese una justificación antes de continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                this.DialogResult = DialogResult.OK; // Indicar que el formulario se cerró correctamente
                this.Close(); // Cerrar el formulario
            }
        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                string justificacion = txtJustificacion.Text.Trim();

                if (string.IsNullOrEmpty(justificacion))
                {
                    MessageBox.Show("Por favor, ingrese una justificación antes de continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Pasamos el ID de la persona en sesión
                bool resultado = usuarioBLL.AgregarJustificacion(usuarioActual.IdPersona, IdReporte, justificacion);

                if (resultado)
                {
                    MessageBox.Show("Justificación agregada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar la justificación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtJustificacion_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
    
}