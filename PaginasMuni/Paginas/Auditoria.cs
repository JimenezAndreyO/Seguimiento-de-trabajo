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
using System.Diagnostics;
using System.IO;

namespace PaginasMuni

{
    public partial class Auditoria : Form
    {
        public Auditoria()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string apellido1 = txtApellido1.Text.Trim();
            string apellido2 = txtApellido2.Text.Trim();
            string tipoIncidencia = txtIncidencia.Text.Trim();

            DateTime? fechaInicio = null;
            DateTime? fechaFinal = null;

            dgvReportes.AutoGenerateColumns = false;

            // Validar fechas solo si el checkbox está marcado
            if (datatimeFechaInicio.Checked)
            {
                fechaInicio = datatimeFechaInicio.Value;
            }

            if (DatetimeFechaFinal.Checked)
            {
                fechaFinal = DatetimeFechaFinal.Value;
            }

            // Obtener datos desde la base de datos
            UsuarioBLL reporteNegocios = new UsuarioBLL();
            DataTable dt = reporteNegocios.ObtenerReportes(nombre, apellido1, apellido2, fechaInicio, fechaFinal, tipoIncidencia);

            // Eliminar duplicados
            DataView dv = dt.DefaultView;
            dv.Sort = "IdReporte"; // Asegúrate de ordenar por la columna que distingue a cada reporte
            DataTable dtSinDuplicados = dv.ToTable(true, "IdReporte", "Nombre", "Apellido1", "Apellido2", "Departamento", "TipoIncidencia", "FechaInicio", "FechaFinal", "Observaciones", "Estado");

            // Asignar los datos al DataGridView
            dgvReportes.DataSource = dtSinDuplicados;
        }



        private void dgvReportes_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica si se hizo clic en la columna "Archivo"
            if (e.ColumnIndex == dgvReportes.Columns["Archivo"].Index && e.RowIndex >= 0)
            {
                int idReporte = Convert.ToInt32(dgvReportes.Rows[e.RowIndex].Cells["IdReporte"].Value);

                UsuarioBLL bll = new UsuarioBLL();
                List<byte[]> archivosBytes = bll.ObtenerArchivos(idReporte);  // Cambié el método para obtener varios archivos

                if (archivosBytes != null && archivosBytes.Count > 0)
                {
                    foreach (var archivoBytes in archivosBytes)
                    {
                        if (archivoBytes != null && archivoBytes.Length > 0)
                        {
                            // Guarda el archivo como PDF en una ubicación temporal
                            string tempPath = Path.Combine(Path.GetTempPath(), "ReporteDescargado_" + Guid.NewGuid() + ".pdf");
                            File.WriteAllBytes(tempPath, archivoBytes);

                            try
                            {
                                Process.Start(new ProcessStartInfo
                                {
                                    FileName = tempPath,
                                    UseShellExecute = true
                                });
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error al abrir el archivo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No hay archivos disponibles para este reporte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnRetornar_Click(object sender, EventArgs e)
        {
            //this.Hide(); // Oculta la ventana actual
            //IncioAdministrativo form = new IncioAdministrativo();
            //form.FormClosed += (s, args) => this.Close();
            //form.Show();
        }

        private void Auditoria_Load(object sender, EventArgs e)
        {
            dgvReportes.Columns.Clear(); // Elimina cualquier columna previa

            // Agregar columnas manualmente
            dgvReportes.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdReporte", HeaderText = "ID Reporte", DataPropertyName = "IdReporte" });
            dgvReportes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nombre", HeaderText = "Nombre", DataPropertyName = "Nombre" });
            dgvReportes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Apellido1", HeaderText = "Apellido 1", DataPropertyName = "Apellido1" });
            dgvReportes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Apellido2", HeaderText = "Apellido 2", DataPropertyName = "Apellido2" });
            dgvReportes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Departamento", HeaderText = "Departamento", DataPropertyName = "Departamento" });
            dgvReportes.Columns.Add(new DataGridViewTextBoxColumn { Name = "TipoIncidencia", HeaderText = "Tipo de Incidencia", DataPropertyName = "TipoIncidencia" });
            dgvReportes.Columns.Add(new DataGridViewTextBoxColumn { Name = "FechaInicio", HeaderText = "Fecha de Inicio", DataPropertyName = "FechaInicio" });
            dgvReportes.Columns.Add(new DataGridViewTextBoxColumn { Name = "FechaFinal", HeaderText = "Fecha Final", DataPropertyName = "FechaFinal" });
            dgvReportes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Observaciones", HeaderText = "Observaciones", DataPropertyName = "Observaciones" });
            //dgvReportes.Columns.Add(new DataGridViewTextBoxColumn { Name = "NombrePersonaReporte", HeaderText = "NombrePersonaReporte", DataPropertyName = "NombrePersonaReporte" });
            //dgvReportes.Columns.Add(new DataGridViewTextBoxColumn { Name = "ApellidoPersonaReporte ", HeaderText = "ApellidoPersonaReporte ", DataPropertyName = "ApellidoPersonaReporte " });
            //dgvReportes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Apellido2PersonaReporte ", HeaderText = "Apellido2PersonaReporte ", DataPropertyName = "Apellido2PersonaReporte " });

            dgvReportes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Estado", HeaderText = "Estado", DataPropertyName = "Estado" });


            dgvReportes.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Archivo",
                HeaderText = "Archivo",
                Text = "Abrir",
                UseColumnTextForButtonValue = true
            });
        }
    }
}
