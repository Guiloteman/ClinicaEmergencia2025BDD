using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace ClinicaEmergencia2025BDD.App
{
    public class ServicioAutenticacion
    {
        public const int SALT_SIZE = 16;       
        public const int HASH_SIZE = 32;       
        public const int ITERATIONS = 4;       
        public const int MEMORY_COST = 65536;  
        public const int DEGREE_OF_PARALLELISM = 2;

        public string CrearHash(string password)
        {
            // 1. Generar Salt (Sal) Aleatorio
            byte[] salt = new byte[SALT_SIZE];
            RandomNumberGenerator.Fill(salt);

            // 2. Configurar el Argon2id
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = DEGREE_OF_PARALLELISM,
                Iterations = ITERATIONS,
                MemorySize = MEMORY_COST
            };

            byte[] hashBytes = argon2.GetBytes(HASH_SIZE);

            return $"{Convert.ToBase64String(salt)}|{Convert.ToBase64String(hashBytes)}";
        }

        public bool VerificarHash(string password, string hashAlmacenado)
        {
            // 1. Separar el Salt y el Hash del formato almacenado "Salt|Hash"
            string[] partes = hashAlmacenado.Split('|');
            if (partes.Length != 2)
            {
                // El formato almacenado es incorrecto
                return false;
            }

            // 2. Convertir de Base64 a bytes
            byte[] salt = Convert.FromBase64String(partes[0]);
            byte[] hashEsperado = Convert.FromBase64String(partes[1]);

            // 3. Configurar Argon2id con la contraseña y el Salt ALMACENADO
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = DEGREE_OF_PARALLELISM,
                Iterations = ITERATIONS,
                MemorySize = MEMORY_COST
            };

            // 4. Recalcular el hash con la contraseña ingresada
            byte[] hashRecalculado = argon2.GetBytes(HASH_SIZE);

            // 5. Comparar los hashes usando una comparación de tiempo constante (seguro contra side-channels)
            return CryptographicOperations.FixedTimeEquals(hashRecalculado, hashEsperado);
        }
    }
}
