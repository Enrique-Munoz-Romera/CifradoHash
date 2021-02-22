using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CifradoHash.Helpers
{
    public class CypherService
    {
        public static string GetSalt()
        {
            Random rnd = new Random();
            string salt = "";
            for (int i = 1; i <= 50; i++)
            {
                int aleat = rnd.Next(0, 255);
                char letra = Convert.ToChar(aleat);
                salt += letra;
            }
            return salt;
        }

        public static byte[] CypherHashefficent(string contenido, string salt)
        {

            String contenidoSalt = contenido + salt;
            SHA256Managed sha = new SHA256Managed();
            byte[] output;
            output = Encoding.UTF8.GetBytes(contenidoSalt);
            for (int i = 1; i <= 100; i++)
            {
                //realizamos el cifrado n veces
                output = sha.ComputeHash(output);
            }
            sha.Clear();
            return output;
        }
    }
}
