using System.Security.Cryptography;

namespace SGA.Domain.Common
{
    /// <summary>
    /// Hashing de contraseñas con PBKDF2 (estándar, incluido en .NET, sin paquetes externos).
    /// Formato guardado: "iteraciones.saltEnBase64.hashEnBase64".
    /// </summary>
    public static class PasswordHasher
    {
        private const int Iteraciones = 100_000;
        private const int TamanoSalt = 16;
        private const int TamanoHash = 32;

        /// <summary>Genera el hash a guardar en base de datos a partir de una contraseña en texto plano.</summary>
        public static string Hash(string passwordEnTextoPlano)
        {
            var salt = RandomNumberGenerator.GetBytes(TamanoSalt);
            var hash = Rfc2898DeriveBytes.Pbkdf2(passwordEnTextoPlano, salt, Iteraciones, HashAlgorithmName.SHA256, TamanoHash);

            return $"{Iteraciones}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        /// <summary>Verifica una contraseña en texto plano contra el hash guardado en base de datos.</summary>
        public static bool Verificar(string passwordEnTextoPlano, string? hashGuardado)
        {
            if (string.IsNullOrWhiteSpace(passwordEnTextoPlano) || string.IsNullOrWhiteSpace(hashGuardado))
                return false;

            var partes = hashGuardado.Split('.');
            if (partes.Length != 3 || !int.TryParse(partes[0], out var iteraciones))
                return false;

            try
            {
                var salt = Convert.FromBase64String(partes[1]);
                var hashEsperado = Convert.FromBase64String(partes[2]);
                var hashCalculado = Rfc2898DeriveBytes.Pbkdf2(
                    passwordEnTextoPlano, salt, iteraciones, HashAlgorithmName.SHA256, hashEsperado.Length);

                return CryptographicOperations.FixedTimeEquals(hashCalculado, hashEsperado);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        /// True si el valor guardado NO tiene el formato de hash esperado (por ejemplo, contraseñas
        /// antiguas guardadas en texto plano antes de este cambio). Útil para migrar datos existentes.
        /// </summary>
        public static bool PareceTextoPlano(string? valorGuardado)
            => !string.IsNullOrWhiteSpace(valorGuardado) && valorGuardado.Split('.').Length != 3;
    }
}
