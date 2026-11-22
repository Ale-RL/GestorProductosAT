
namespace GestorProductosAT
{
    // Clase principal encargada de iniciar y controlar la ejecución del programa.
    internal static class Program
    {
        // Define el método principal desde donde comienza a ejecutarse la aplicación.
        [STAThread]
        static void Main()
        {
            // Inicializa la configuración necesaria para utilizar Windows Forms.
            ApplicationConfiguration.Initialize();

            // Intenta actualizar la estructura y los datos de la base de datos.
            try { BaseDatos.Actualizar(); }

            // Captura cualquier error que ocurra durante la actualización.
            catch (Exception ex)
            {
                // Muestra un mensaje indicando que ocurrió un problema con la base de datos.
                MessageBox.Show("No se pudo actualizar la base de datos:\n" + ex.Message);
                return; // Finaliza la ejecución del programa.
            }

            // Mantiene el ciclo de inicio de sesión y permite volver a entrar después de cerrar sesión.
            while (true)
            {
                // Crea y muestra el formulario de inicio de sesión.
                using (var login = new FrmLogin())
                {
                    // Comprueba si el inicio de sesión no terminó correctamente.
                    if (login.ShowDialog() != DialogResult.OK) return;
                }

                // Crea una instancia del formulario principal del programa.
                var principal = new Form1();

                // Ejecuta el formulario principal y espera hasta que se cierre.
                Application.Run(principal);

                // Comprueba si el usuario solicitó cerrar sesión.
                if (!principal.CerrarSesionSolicitado) return;
            }
        }
    }
}
