namespace GestorProductosAT
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Inicializa la configuración básica de la aplicación
            ApplicationConfiguration.Initialize();

            // Comprueba que el archivo de la base de datos exista junto al programa
            string rutaBd = Path.Combine(AppContext.BaseDirectory, "GestorProductosAT.db");
            if (!File.Exists(rutaBd))
            {
                MessageBox.Show("No se encontró la base de datos:\n" + rutaBd +
                                "\n\nCopia el archivo GestorProductosAT.db junto al programa.");
                return;
            }

            // Muestra un mensaje cuando ocurre un error inesperado, en lugar de cerrar el programa
            Application.ThreadException += (s, e) =>
                MessageBox.Show("Ocurrió un error inesperado:\n" + e.Exception.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // Actualiza la base de datos (columnas nuevas y contraseñas protegidas)
            try { BaseDatos.Actualizar(); }
            catch (Exception ex)
            {
                // Avisa del problema y cierra el programa si no se pudo actualizar
                MessageBox.Show("No se pudo actualizar la base de datos:\n" + ex.Message);
                return;
            }

            // Repite el inicio de sesión mientras el usuario cierre sesión
            while (true)
            {
                // Muestra el inicio de sesión; si se cancela, termina el programa
                using (var login = new FrmLogin())
                {
                    if (login.ShowDialog() != DialogResult.OK) return;
                }

                // Abre la ventana principal
                var principal = new Form1();
                Application.Run(principal);

                // Si no se pidió cerrar sesión, se cerró el programa
                if (!principal.CerrarSesionSolicitado) return;
            }
        }
    }
}