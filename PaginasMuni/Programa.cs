using System;
using System.Windows.Forms;

namespace PaginasMuni
{
    static class Program
    {
        [STAThread] 
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1()); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar la aplicación:\n" + ex.Message,
                                "Error crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
