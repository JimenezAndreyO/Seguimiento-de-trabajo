using System;
using System.Data;
using System.Windows.Forms;
using Negocios;
using System.Drawing;

namespace PaginasMuni
{
    public partial class CambiarPermisosUsuario : Form
    {
        private UsuarioBLL usuarioBLL = new UsuarioBLL();

        public CambiarPermisosUsuario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string identificacion = string.IsNullOrWhiteSpace(txtIdentificacion.Text) ? null : txtIdentificacion.Text.Trim();
            string nombre = string.IsNullOrWhiteSpace(txtNombre.Text) ? null : txtNombre.Text.Trim();
            string apellido1 = string.IsNullOrWhiteSpace(txtApellido1.Text) ? null : txtApellido1.Text.Trim();
            string apellido2 = string.IsNullOrWhiteSpace(txtApellido2.Text) ? null : txtApellido2.Text.Trim();

            DataTable dt = usuarioBLL.ObtenerPermisos(identificacion, nombre, apellido1, apellido2);

            if (dt.Rows.Count > 0)
            {
                dgvPersonas.DataSource = dt;
                dgvPersonas.Columns["IdPermisoUsuario"].DisplayIndex = 0;
                dgvPersonas.Columns["Identificacion"].DisplayIndex = 1;
                dgvPersonas.Columns["Nombre"].DisplayIndex = 2;
                dgvPersonas.Columns["Apellido1"].DisplayIndex = 3;
                dgvPersonas.Columns["Apellido2"].DisplayIndex = 4;
            }
            else
            {
                MessageBox.Show("No se encontraron registros.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Agregar columnas de checkboxes si no existen
            AgregarColumnasPermisos();
            LlenarCheckboxes();
        }

        private void CambiarPermisosUsuario_Load(object sender, EventArgs e)
        {
            AgregarColumnasPermisos();
        }

        private void AgregarColumnaCheckBox(string nombre, string textoEncabezado)
        {
            if (dgvPersonas.Columns[nombre] == null)
            {
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn
                {
                    Name = nombre,
                    HeaderText = textoEncabezado,
                    TrueValue = 1,
                    FalseValue = 0,
                    DataPropertyName = nombre
                };
                dgvPersonas.Columns.Add(checkBoxColumn);
            }
        }

        private void AgregarColumnasPermisos()
        {
            AgregarColumnaCheckBox("CrearReporte", "Crear Reporte");
            AgregarColumnaCheckBox("CrearRespuesta", "Crear Respuesta");
            AgregarColumnaCheckBox("Auditoria", "Auditoría");
            AgregarColumnaCheckBox("SeguimientoTrabajo", "Seguimiento de Trabajo");
            AgregarColumnaCheckBox("AgregarPersona", "Agregar Persona");
            AgregarColumnaCheckBox("EliminarPersona", "Eliminar Persona");
            AgregarColumnaCheckBox("PermisosUsuarios", "Permisos de Usuario");
        }

        private void LlenarCheckboxes()
        {
            foreach (DataGridViewRow row in dgvPersonas.Rows)
            {
                row.Cells["CrearReporte"].Value = Convert.ToBoolean(row.Cells["CrearReporte"].Value ?? false);
                row.Cells["CrearRespuesta"].Value = Convert.ToBoolean(row.Cells["CrearRespuesta"].Value ?? false);
                row.Cells["Auditoria"].Value = Convert.ToBoolean(row.Cells["Auditoria"].Value ?? false);
                row.Cells["SeguimientoTrabajo"].Value = Convert.ToBoolean(row.Cells["SeguimientoTrabajo"].Value ?? false);
                row.Cells["AgregarPersona"].Value = Convert.ToBoolean(row.Cells["AgregarPersona"].Value ?? false);
                row.Cells["EliminarPersona"].Value = Convert.ToBoolean(row.Cells["EliminarPersona"].Value ?? false);
                row.Cells["PermisosUsuarios"].Value = Convert.ToBoolean(row.Cells["PermisosUsuarios"].Value ?? false);
            }
        }

        public void dgvPersonas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvPersonas.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
                {
                    string identificacion = dgvPersonas.Rows[e.RowIndex].Cells["Identificacion"].Value?.ToString() ?? "";

                    int crearReporte = Convert.ToInt32(dgvPersonas.Rows[e.RowIndex].Cells["CrearReporte"].EditedFormattedValue ?? 0);
                    int respuestaSolicitud = Convert.ToInt32(dgvPersonas.Rows[e.RowIndex].Cells["CrearRespuesta"].EditedFormattedValue ?? 0);
                    int auditoria = Convert.ToInt32(dgvPersonas.Rows[e.RowIndex].Cells["Auditoria"].EditedFormattedValue ?? 0);
                    int seguimientoTrabajo = Convert.ToInt32(dgvPersonas.Rows[e.RowIndex].Cells["SeguimientoTrabajo"].EditedFormattedValue ?? 0);
                    int agregarPersona = Convert.ToInt32(dgvPersonas.Rows[e.RowIndex].Cells["AgregarPersona"].EditedFormattedValue ?? 0);
                    int eliminarPersona = Convert.ToInt32(dgvPersonas.Rows[e.RowIndex].Cells["EliminarPersona"].EditedFormattedValue ?? 0);
                    int permisosUsuario = Convert.ToInt32(dgvPersonas.Rows[e.RowIndex].Cells["PermisosUsuarios"].EditedFormattedValue ?? 0);

                    // Llamar a la capa de negocios para actualizar permisos
                    usuarioBLL.ActualizarPermisos(identificacion, crearReporte, respuestaSolicitud, auditoria, seguimientoTrabajo, agregarPersona, eliminarPersona, permisosUsuario);

                    MessageBox.Show($"Permisos actualizados para {identificacion}.", "Éxito");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
