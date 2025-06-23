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
using System.IO;      // Para manipulación de archivos
using ClosedXML.Excel;

namespace PaginasMuni
{
    public partial class RespuestaSolicitud: Form
    {
        private UsuarioBLL usuarioBLL = new UsuarioBLL();

        public RespuestaSolicitud()
        {
            InitializeComponent();
            CargarJustificaciones();
            ConfigurarDataGridView();
            dataGridViewJustificaciones.CellClick += dataGridViewJustificaciones_CellContentClick;
        }

        private void CargarJustificaciones()
        {
            dataGridViewJustificaciones.DataSource = usuarioBLL.ObtenerJustificaciones();
        }

        private void ConfigurarDataGridView()
        {
            // Limpiar columnas previas para evitar duplicados
            dataGridViewJustificaciones.Columns.Clear();

            // Cargar los datos nuevamente
            dataGridViewJustificaciones.DataSource = usuarioBLL.ObtenerJustificaciones();

            // Agregar una columna de botón
            DataGridViewButtonColumn btnActualizar = new DataGridViewButtonColumn();
            btnActualizar.HeaderText = "Acción";
            btnActualizar.Text = "Actualizar Estado";
            btnActualizar.UseColumnTextForButtonValue = true;
            btnActualizar.Name = "btnActualizar";


            // Agregar una columna de botón
            //DataGridViewButtonColumn btnExcel = new DataGridViewButtonColumn();
            //btnExcel.HeaderText = "Acción";
            //btnExcel.Text = "Excel";
            //btnExcel.UseColumnTextForButtonValue = true;
            //btnExcel.Name = "btnExcel";

            // Agregar la columna de botones al DataGridView
            dataGridViewJustificaciones.Columns.Add(btnActualizar);
          //  dataGridViewJustificaciones.Columns.Add(btnExcel);
        }

        private void RespuestaSolicitud_Load(object sender, EventArgs e)
        {

        }

        private void dataGridViewJustificaciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si la columna presionada es la del botón
            if (e.ColumnIndex == dataGridViewJustificaciones.Columns["btnActualizar"].Index && e.RowIndex >= 0)
            {
                int idJustificacion = Convert.ToInt32(dataGridViewJustificaciones.Rows[e.RowIndex].Cells["IdJustificacionReporte"].Value);

                // Mostrar un cuadro de selección de estado
                string nuevoEstado = MostrarCuadroSeleccionEstado();

                if (!string.IsNullOrEmpty(nuevoEstado))
                {
                    bool actualizado = usuarioBLL.ActualizarEstadoJustificacion(idJustificacion, nuevoEstado);

                    if (actualizado)
                    {
                        MessageBox.Show("Estado actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ConfigurarDataGridView(); // Refrescar la tabla con los cambios
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar el estado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            //if (e.ColumnIndex == dataGridViewJustificaciones.Columns["btnExcel"].Index && e.RowIndex >= 0)
            //{
            //    ExportarAExcel();
            //}
        }

        private string MostrarCuadroSeleccionEstado()
        {
            using (Form seleccionarEstado = new Form())
            {
                seleccionarEstado.Text = "Seleccionar Estado";
                seleccionarEstado.Size = new Size(300, 150);
                seleccionarEstado.StartPosition = FormStartPosition.CenterParent;

                ComboBox comboBoxEstado = new ComboBox()
                {
                    Location = new Point(50, 20),
                    Width = 180
                };
                comboBoxEstado.Items.Add("Aprobada");
                comboBoxEstado.Items.Add("Rechazada");

                Button btnAceptar = new Button()
                {
                    Text = "Aceptar",
                    Location = new Point(100, 60),
                    DialogResult = DialogResult.OK
                };

                seleccionarEstado.Controls.Add(comboBoxEstado);
                seleccionarEstado.Controls.Add(btnAceptar);

                if (seleccionarEstado.ShowDialog() == DialogResult.OK && comboBoxEstado.SelectedItem != null)
                {
                    return comboBoxEstado.SelectedItem.ToString();
                }
                return null;
            }
        }



        // Método para exportar los datos a un archivo Excel
        private void ExportarAExcel()
        {
            // Crear un nuevo libro de trabajo de Excel
            var wb = new XLWorkbook();

            // Crear una hoja en el archivo Excel
            var ws = wb.Worksheets.Add("Justificaciones");

            // Obtener los datos del DataGridView
            var rowCount = dataGridViewJustificaciones.Rows.Count;
            var columnCount = dataGridViewJustificaciones.Columns.Count;

            // Agregar los encabezados de las columnas
            for (int col = 0; col < columnCount; col++)
            {
                ws.Cell(1, col + 1).Value = dataGridViewJustificaciones.Columns[col].HeaderText;
            }

            // Agregar los datos del DataGridView
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < columnCount; col++)
                {
                    // Verifica si el valor es nulo antes de asignarlo
                    var cellValue = dataGridViewJustificaciones.Rows[row].Cells[col].Value;
                    if (cellValue != null)
                    {
                        ws.Cell(row + 2, col + 1).Value = cellValue.ToString();
                    }
                }
            }

            // Mostrar un cuadro de diálogo para seleccionar la ubicación y el nombre del archivo
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Guardar el archivo Excel en la ubicación seleccionada
                var filePath = saveFileDialog.FileName;
                wb.SaveAs(filePath);

                // Notificar que el archivo fue guardado exitosamente
                MessageBox.Show("Archivo Excel guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExportarAExcel();

        }
    }
    }



