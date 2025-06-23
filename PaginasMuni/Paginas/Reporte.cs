using System;
using System.IO;
using System.Windows.Forms;
using Negocios;
using DAL.Entidades;

using System.Collections.Generic;


namespace PaginasMuni
{
    public partial class Reporte : Form
    {
        private UsuarioBLL usuarioBLL = new UsuarioBLL();
        private UsuarioSesion perfil;
        private byte[] archivoBytes = null;
        private List<Tuple<string, byte[]>> archivos = new List<Tuple<string, byte[]>>();

        public Reporte(UsuarioSesion usuario)
        {
            InitializeComponent();
            perfil = usuario;
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void Reporte_Load(object sender, EventArgs e)
        {
            if (perfil != null)
            {
                txtNombreCreadorReporte.Text = perfil.Nombre;
                txtApellido1CreadroReporte.Text = perfil.Apellido1;
                txtApellido2CreadorReporte.Text = perfil.Apellido2;

                // Bloquear edición de los campos de creador del reporte
                txtNombreCreadorReporte.ReadOnly = true;
                txtApellido1CreadroReporte.ReadOnly = true;
                txtApellido2CreadorReporte.ReadOnly = true;
            }
            else
            {
                MessageBox.Show("Error: No se ha encontrado la información del usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = true; // Permitir selección múltiple
                openFileDialog.Filter = "Archivos PDF|*.pdf"; // Solo archivos PDF

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                   // archivos.Clear(); // Limpiar la lista antes de agregar nuevos archivos
                    List<string> nombresArchivos = new List<string>(); // Lista para mostrar en el Label

                    foreach (string filePath in openFileDialog.FileNames)
                    {
                        byte[] fileBytes = File.ReadAllBytes(filePath);
                        archivos.Add(new Tuple<string, byte[]>(Path.GetFileName(filePath), fileBytes));
                        nombresArchivos.Add(Path.GetFileName(filePath)); // Agregar nombre del archivo a la lista
                    }

                    // Mostrar todos los archivos en el Label
                    lblArchivoSeleccionado.Text = string.Join(Environment.NewLine, nombresArchivos);
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido1.Text) ||
                string.IsNullOrWhiteSpace(txtapellido2.Text) ||
                string.IsNullOrWhiteSpace(txtDepartamento.Text) ||
                string.IsNullOrWhiteSpace(txtIncidencia.Text) ||
                string.IsNullOrWhiteSpace(txtObservaciones.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (archivos.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione al menos un archivo.");
                return;
            }

            string Nombre = txtNombre.Text.Trim();
            string Apellido1 = txtApellido1.Text.Trim();
            string Apellido2 = txtapellido2.Text.Trim();
            string Departamento = txtDepartamento.Text.Trim();
            string TipoIncidencia = txtIncidencia.Text.Trim();
            DateTime FechaInicio = DateTimeInicio.Value;
            DateTime FechaFinal = DateTimeFinal.Value;
            string Observaciones = txtObservaciones.Text.Trim();
            string Estado = "Pendiente";

            int idReporte; // Variable para almacenar el ID del reporte

            try
            {
                // Guardar el reporte y obtener el ID generado
                bool resultado = usuarioBLL.GuardarReporte(
                    Nombre, Apellido1, Apellido2, Departamento, TipoIncidencia,
                    FechaInicio, FechaFinal, Observaciones, perfil.Nombre,
                    perfil.Apellido1, perfil.Apellido2, Estado, out idReporte
                );

                if (resultado && idReporte > 0)
                {
                    // Guardar cada archivo con el mismo IDReporte
                    foreach (var archivo in archivos)
                    {
                        usuarioBLL.GuardarArchivo(idReporte, archivo.Item1, archivo.Item2);
                    }

                    MessageBox.Show("El reporte y sus archivos han sido guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se encontró una persona activa con esos datos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar el reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        // Método para limpiar los campos
        private void LimpiarCampos()
        {
            txtNombre.Clear(); 
            txtApellido1.Clear();
            txtapellido2.Clear();
            txtObservaciones.Clear();
            archivoBytes = null;
            lblArchivoSeleccionado.Text = "Ningún archivo seleccionado.";
        }


        private void Reporte_Load_1(object sender, EventArgs e)
        {
            if (perfil != null)
            {
                txtNombreCreadorReporte.Text = perfil.Nombre;
                txtApellido1CreadroReporte.Text = perfil.Apellido1;
                txtApellido2CreadorReporte.Text = perfil.Apellido2;

                // Bloquear edición de los campos de creador del reporte
                txtNombreCreadorReporte.ReadOnly = true;
                txtApellido1CreadroReporte.ReadOnly = true;
                txtApellido2CreadorReporte.ReadOnly = true;
            }
            else
            {
                MessageBox.Show("Error: No se ha encontrado la información del usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }


    }
}
