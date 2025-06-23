using System;
using System.Windows.Forms;

namespace PaginasMuni
{
    static class Program
    {
        [STAThread] // Necesario para aplicaciones Windows Forms
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1()); // Asegúrate de que Form1 sea tu formulario de inicio
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al iniciar la aplicación:\n" + ex.Message,
                                "Error crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
