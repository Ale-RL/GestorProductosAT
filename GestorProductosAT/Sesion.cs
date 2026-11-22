
namespace GestorProductosAT
{
    // Clase encargada de almacenar los datos del usuario que tiene una sesión activa.
    public static class Sesion
    {
        // Guarda el nombre del usuario que inició sesión.
        public static string Nombre { get; set; } = "";

        // Guarda el rol del usuario para identificar sus permisos dentro del programa.
        public static string Rol { get; set; } = "";

        // Guarda el identificador único del usuario registrado en la base de datos.
        public static long Id { get; set; }
    }
}
