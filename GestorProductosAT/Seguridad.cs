using System.Security.Cryptography;

namespace GestorProductosAT
{
    public static class Seguridad
    {
        // Las contraseñas protegidas empiezan con este texto, así se distinguen de las viejas
        private const string Prefijo = "PBKDF2$";
        private const int Iteraciones = 100000;

        public static bool EsHash(string valor) => valor.StartsWith(Prefijo);

        // Convierte "1234" en un texto largo e ilegible que no se puede revertir
        public static string Hashear(string contrasena)
        {
            byte[] sal = RandomNumberGenerator.GetBytes(16); // texto al azar, distinto por usuario
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                contrasena, sal, Iteraciones, HashAlgorithmName.SHA256, 32);
            return Prefijo + Convert.ToBase64String(sal) + "$" + Convert.ToBase64String(hash);
        }

        // Repite el cálculo con la contraseña escrita y compara con lo guardado
        public static bool Verificar(string contrasena, string guardada)
        {
            if (!EsHash(guardada)) return false;

            var partes = guardada.Split('$'); // [prefijo, sal, hash]
            if (partes.Length != 3) return false;

            byte[] sal = Convert.FromBase64String(partes[1]);
            byte[] esperado = Convert.FromBase64String(partes[2]);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                contrasena, sal, Iteraciones, HashAlgorithmName.SHA256, esperado.Length);

            return CryptographicOperations.FixedTimeEquals(hash, esperado);
        }
    }
}