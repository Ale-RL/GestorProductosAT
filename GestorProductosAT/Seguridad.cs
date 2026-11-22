
using System.Security.Cryptography; // Importa las herramientas necesarias para trabajar con métodos criptográficos.

namespace GestorProductosAT //La seguridad es muy segura sabia profe
{
    // Clase encargada de proteger las contraseñas y verificar su autenticidad.
    public static class Seguridad
    {
        // Define un prefijo para identificar las contraseñas que ya fueron protegidas.
        private const string Prefijo = "PBKDF2$";

        // Establece la cantidad de iteraciones utilizadas para generar el hash.
        private const int Iteraciones = 100000;

        // Comprueba si una contraseña ya tiene el formato de un hash protegido.
        public static bool EsHash(string valor) => valor.StartsWith(Prefijo);

        // Convierte una contraseña normal en un hash para almacenarla de forma segura.
        public static string Hashear(string contrasena)
        {
            // Genera una secuencia aleatoria de 16 bytes que se utilizará como sal.
            byte[] sal = RandomNumberGenerator.GetBytes(16);

            // Genera el hash de la contraseña utilizando el algoritmo PBKDF2 con SHA256.
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                contrasena, sal, Iteraciones, HashAlgorithmName.SHA256, 32);

            // Convierte la sal y el hash a texto y los une junto con el prefijo.
            return Prefijo + Convert.ToBase64String(sal) + "$" + Convert.ToBase64String(hash);
        }

        // Comprueba si una contraseña escrita coincide con el hash almacenado.
        public static bool Verificar(string contrasena, string guardada)
        {
            // Comprueba que el valor almacenado tenga el formato de un hash protegido.
            if (!EsHash(guardada)) return false;

            // Separa el texto almacenado en tres partes: prefijo, sal y hash.
            var partes = guardada.Split('$');

            // Comprueba que se hayan obtenido las tres partes esperadas.
            if (partes.Length != 3) return false;

            // Recupera la sal almacenada y la convierte nuevamente en bytes.
            byte[] sal = Convert.FromBase64String(partes[1]);

            // Recupera el hash original almacenado en la base de datos.
            byte[] esperado = Convert.FromBase64String(partes[2]);

            // Genera un nuevo hash utilizando la contraseña ingresada y la sal recuperada.
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                contrasena, sal, Iteraciones, HashAlgorithmName.SHA256, esperado.Length);

            // Compara ambos hashes de manera segura y devuelve si son iguales.
            return CryptographicOperations.FixedTimeEquals(hash, esperado);
        }
    }
}
