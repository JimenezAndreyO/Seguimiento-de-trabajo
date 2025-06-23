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
    public partial class ModificaryEliminarPerfil : Form
    {

        UsuarioBLL usuarioBLL = new UsuarioBLL();
        public ModificaryEliminarPerfil()
        {
            InitializeComponent();

         

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string apellido1 = txtApellido1.Text.Trim();
            string apellido2 = txtApellido2.Text.Trim();

            // Obtener datos desde la base de datos
            UsuarioBLL reporteNegocios = new UsuarioBLL();
            DataTable dt = reporteNegocios.ObtenerPersonas(nombre, apellido1, apellido2);

            // Asignar los datos al DataGridView
            dgvPersonas.DataSource = dt;
            dgvPersonas.Columns["IdPersona"].ReadOnly = true;
            dgvPersonas.Columns["Estado"].ReadOnly = true;
            CargarDatos();
        }

        private void CargarDatos()
        {
            string nombre = txtNombre.Text.Trim();
            string apellido1 = txtApellido1.Text.Trim();
            string apellido2 = txtApellido2.Text.Trim();

            UsuarioBLL reporteNegocios = new UsuarioBLL();
            DataTable dt = reporteNegocios.ObtenerPersonas(nombre,apellido1,apellido2); // Debes tener este método en la BLL
            dgvPersonas.DataSource = dt;
            dgvPersonas.Columns["IdPersona"].ReadOnly = true;

            foreach (DataGridViewRow row in dgvPersonas.Rows)
            {
                // Asegúrate de que la celda no sea null
                if (row.Cells["Contraseña"].Value != DBNull.Value && row.Cells["Contraseña"].Value != null)
                {
                    string contraseñaOriginal = row.Cells["Contraseña"].Value.ToString();
                    row.Cells["Contraseña"].Value = new string('*', contraseñaOriginal.Length); // Mostrar asteriscos según el largo de la contraseña
                }
                else
                {
                    row.Cells["Contraseña"].Value = ""; // Si es null, ponemos un valor vacío
                }
            }
        }


        private void dgvPersonas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0) // Asegura que no sea el encabezado
            {
                int idPersona = Convert.ToInt32(dgvPersonas.Rows[e.RowIndex].Cells["IdPersona"].Value);
               

                if (dgvPersonas.Columns[e.ColumnIndex].HeaderText == "Modificar")
                {



                    // Obtener valores de la fila seleccionada
                    string identificacion = dgvPersonas.Rows[e.RowIndex].Cells["Identificacion"].Value.ToString();
                    string nombre = dgvPersonas.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                    string apellido1 = dgvPersonas.Rows[e.RowIndex].Cells["Apellido1"].Value.ToString();
                    string apellido2 = dgvPersonas.Rows[e.RowIndex].Cells["Apellido2"].Value.ToString();
                    string correo = dgvPersonas.Rows[e.RowIndex].Cells["Correo"].Value.ToString();
                    string usuario = dgvPersonas.Rows[e.RowIndex].Cells["Usuario"].Value.ToString();


                    string contraseña = dgvPersonas.Rows[e.RowIndex].Cells["Contraseña"].Value.ToString();

                    // Llamar al método de modificación
                    bool resultado = usuarioBLL.ModificarPersona(idPersona, identificacion, nombre, apellido1, apellido2, correo, usuario, contraseña);

                    if (resultado)
                    {
                        MessageBox.Show("Persona modificada correctamente.");
                        CargarDatos(); // Recargar la tabla
                    }
                    else
                    {
                        MessageBox.Show("Error al modificar la persona.");
                    }
                }
                else if (dgvPersonas.Columns[e.ColumnIndex].HeaderText == "Estado")
                {

                    // Confirmar modificación de estado
                    DialogResult confirmacion = MessageBox.Show(
                        "¿Seguro que deseas cambiar el estado de esta persona?",
                        "Confirmar",
                        MessageBoxButtons.YesNo
                    );

                    if (confirmacion == DialogResult.Yes)
                    {
                        bool resultado = usuarioBLL.ModificarEstado(idPersona);

                        if (resultado)
                        {
                            MessageBox.Show("Estado modificado correctamente.");
                            CargarDatos(); // Recargar la tabla
                        }
                        else
                        {
                            MessageBox.Show("Error al modificar el estado.");
                        }
                    }
                }

            }
        }


        private void ConfigurarDataGridView()
        {
       
            foreach (DataGridViewColumn column in dgvPersonas.Columns)
            {
                if (column.Name == "Contraseña") // Reemplaza "Contraseña" con el nombre de la columna correspondiente
                {
                    column.DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Regular);
                    column.DefaultCellStyle.NullValue = null;
                }
            }

            // Configuramos la celda para mostrar asteriscos en la columna de "Contraseña"
            dgvPersonas.Columns["Contraseña"].DefaultCellStyle.Format = "*****"; // Esto hará que las celdas se muestren con asteriscos
            dgvPersonas.Columns["Contraseña"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void ModificaryEliminarPerfil_Load(object sender, EventArgs e)
        {
            DataGridViewButtonColumn btnModificar = new DataGridViewButtonColumn();

            btnModificar.HeaderText = "Modificar";
            btnModificar.Text = "Modificar";
            btnModificar.UseColumnTextForButtonValue = true;
            dgvPersonas.Columns.Add(btnModificar);



            DataGridViewButtonColumn btnEstado = new DataGridViewButtonColumn();

            btnEstado.HeaderText = "Estado";
            btnEstado.Text = "Estado";
            btnEstado.UseColumnTextForButtonValue = true;
            dgvPersonas.Columns.Add(btnEstado);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
