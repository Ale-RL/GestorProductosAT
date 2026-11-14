namespace GestorProductosAT
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            try { BaseDatos.Actualizar(); }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo actualizar la base de datos:\n" + ex.Message);
                return;
            }

            while (true) 
            {

                using (var login = new FrmLogin())
                {
                    if (login.ShowDialog() != DialogResult.OK) return;
                }

                var principal = new Form1();
                Application.Run(principal);

                //Si no se pidio cerrar sesion, se cerró el programa
                if (!principal.CerrarSesionSolicitado) return;
            }
        }
    }
}