using System.Security.Cryptography;
using System.Text;

namespace SistemaVenda.Helpers
{
    public class Criptografia
    {
        // Método para gerar o hash MD5 a partir de uma string
        public static string GetMD5Hash(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Converte a string em bytes e computa o hash
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Constrói a string hexadecimal do hash
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        // Método para verificar se o hash gerado de "input" é igual ao hash passado
        public static bool VerifyMD5Hash(string input, string hash)
        {
            string hashOfInput = GetMD5Hash(input);

            // Faz comparação sem diferenciar maiúsculas/minúsculas
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            // Retorna true se os hashes forem iguais e false caso contrário
            return comparer.Compare(hashOfInput, hash) == 0; 
        }
    }
}
