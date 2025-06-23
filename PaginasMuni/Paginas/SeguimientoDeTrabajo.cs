using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using DAL;
using DAL.Entidades;
using Negocios;

namespace PaginasMuni
{
    public partial class SeguimientoDeTrabajo : Form
    {
        private UsuarioSesion usuarioActual;
        private UsuarioBLL usuarioBLL;

        public SeguimientoDeTrabajo(UsuarioSesion usuario)
        {
            InitializeComponent();
            usuarioBLL = new UsuarioBLL();
            usuarioActual = usuario; // Ahora sí se recibe como parámetro
            CargarReportes();
        }


        private void CargarReportes()
        {
            try
            {
                // Obtener reportes desde la capa de negocio
                List<DAL.Entidades.Reporte> reportes = usuarioBLL.ObtenerReportesActivos(usuarioActual.IdPersona);

                // Verificar si la lista de reportes es válida
                if (reportes == null || !reportes.Any())
                {
                    MessageBox.Show("No hay reportes disponibles.");
                    return;
                }

                // Desactivar generación automática de columnas para personalizar el DataGridView
                dgvReportes.AutoGenerateColumns = false;

                // Limpiar columnas antes de agregarlas para evitar duplicados
                dgvReportes.Columns.Clear();

                // Agregar columnas manualmente
                dgvReportes.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdReporte", HeaderText = "ID Reporte", DataPropertyName = "IdReporte" });
                dgvReportes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Departamento", HeaderText = "Departamento", DataPropertyName = "Departamento" });
                dgvReportes.Columns.Add(new DataGridViewTextBoxColumn { Name = "TipoIncidencia", HeaderText = "Tipo de Incidencia", DataPropertyName = "TipoIncidencia" });
                dgvReportes.Columns.Add(new DataGridViewTextBoxColumn { Name = "FechaInicio", HeaderText = "Fecha de Inicio", DataPropertyName = "FechaInicio" });
                dgvReportes.Columns.Add(new DataGridViewTextBoxColumn { Name = "FechaFinal", HeaderText = "Fecha Final", DataPropertyName = "FechaFinal" });
                dgvReportes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Observaciones", HeaderText = "Observaciones", DataPropertyName = "Observaciones" });
                dgvReportes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Estado", HeaderText = "Estado", DataPropertyName = "Estado" });

                // Agregar botones si no existen
                DataGridViewButtonColumn btnVerArchivo = new DataGridViewButtonColumn
                {
                    Name = "btnVerArchivo",
                    HeaderText = "Ver Archivo",
                    Text = "Abrir",
                    UseColumnTextForButtonValue = true
                };
                dgvReportes.Columns.Add(btnVerArchivo);

                DataGridViewButtonColumn btnCompletar = new DataGridViewButtonColumn
                {
                    Name = "btnCompletar",
                    HeaderText = "Completar",
                    Text = "Completar",
                    UseColumnTextForButtonValue = true
                };
                dgvReportes.Columns.Add(btnCompletar);

                DataGridViewButtonColumn btnJustificar = new DataGridViewButtonColumn
                {
                    Name = "btnJustificar",
                    HeaderText = "Justificar",
                    Text = "Justificar",
                    UseColumnTextForButtonValue = true
                };
                dgvReportes.Columns.Add(btnJustificar);

                // Asignar la lista de reportes directamente al DataGridView
                dgvReportes.DataSource = reportes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar reportes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void dgvReportes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            string estado = dgvReportes.Rows[e.RowIndex].Cells["Estado"].Value.ToString();
            DateTime fechaFinal = Convert.ToDateTime(dgvReportes.Rows[e.RowIndex].Cells["FechaFinal"].Value);

            // Verifica si se hizo clic en la columna "Archivo"
            if (e.ColumnIndex == dgvReportes.Columns["btnVerArchivo"].Index && e.RowIndex >= 0)
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

            }
            else if (dgvReportes.Columns[e.ColumnIndex].Name == "btnCompletar")
            {
                int idReporte = Convert.ToInt32(dgvReportes.Rows[e.RowIndex].Cells["IdReporte"].Value);
                if (estado == "Pendiente")
                {
                    usuarioBLL.CompletarReporte(idReporte);
                    MessageBox.Show("Reporte completado.");
                    CargarReportes();
                }
                else
                {
                    MessageBox.Show("El reporte ya ha sido completado.");
                }
            }

            // Justificar Reporte si la fecha ya expiró
            else if (dgvReportes.Columns[e.ColumnIndex].Name == "btnJustificar")
            {
                int idReporte = Convert.ToInt32(dgvReportes.Rows[e.RowIndex].Cells["IdReporte"].Value);
                if (fechaFinal < DateTime.Now)
                {
                    // Mostrar un formulario para que el usuario ingrese la justificación
                    using (Justificacion justificacionForm = new Justificacion(idReporte, usuarioActual)) // Aquí pasamos usuarioActual
                    {
                        if (justificacionForm.ShowDialog() == DialogResult.OK)
                        {
                            string justificacion = justificacionForm.GetJustificacion(); // Obtener la justificación ingresada

                            // Llamar al método de la capa BLL para justificar el reporte
                            bool resultado = usuarioBLL.JustificarReporte(idReporte, justificacion);

                            if (resultado)
                            {
                                MessageBox.Show("Reporte justificado exitosamente.");
                            }
                            else
                            {
                                MessageBox.Show("Error al justificar el reporte.");
                            }

                            CargarReportes();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Error en el seguimiento de trabajo.");
            }
        }

        private void SeguimientoDeTrabajo_Load(object sender, EventArgs e)
        {

        }
    }
}
