using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace PrEParateApp.Utilities
{
    public class Utils
    {
        // Validación de DNI
        public static bool ValidarDNI(string dni)
        {
            if (string.IsNullOrEmpty(dni)) 
                return false;

            if (dni.Length != 9 || !char.IsLetter(dni[^1]))
                return false;

            string numero = dni.Substring(0, dni.Length - 1);
            if (!int.TryParse(numero, out int n))
                return false;

            char letra = char.ToUpper(dni[^1]);
            string letras = "TRWAGMYFPDXBNJZSQVHLCKE";
            return letra == letras[n % 23];
        }

        // Validación de NIE
        public static bool ValidarNIE(string nie)
        {
            if (string.IsNullOrEmpty(nie)) 
                return false;

            if (nie.Length != 9 || !char.IsLetter(nie[^1]))
                return false;

            char inicial = char.ToUpper(nie[0]);
            if (inicial != 'X' && inicial != 'Y' && inicial != 'Z')
                return false;

            string numero = nie.Substring(1, nie.Length - 2);
            if (!int.TryParse(numero, out int n))
                return false;

            if (inicial == 'Y')
                n += 10000000;
            else if (inicial == 'Z')
                n += 20000000;

            char letra = char.ToUpper(nie[^1]);
            string letras = "TRWAGMYFPDXBNJZSQVHLCKE";
            return letra == letras[n % 23];
        }

        // Validación de Número de Seguridad Social
        public static bool EsNumeroSeguridadSocialValido(string numero)
        {
            if(String.IsNullOrEmpty(numero)) 
                return false;

            if (numero.Length != 12 || !Regex.IsMatch(numero, @"^\d{12}$"))
            {
                return false;
            }
            string codigoProvincia = numero.Substring(0, 2);
            string numeroSecuencial = numero.Substring(2, 8);
            string digitosControl = numero.Substring(10, 2);

            if (!long.TryParse(numero.Substring(0, 10), out long numeroCompleto) || !int.TryParse(digitosControl, out int dc))
            {
                return false;
            }
            int resto = (int)(numeroCompleto % 97);
            return resto == dc;
        }

        // Validación de Número de Tarjeta Sanitaria Valenciana
        public static bool EsNumeroTarjetaSIPValido(string numero)
        {
            return !string.IsNullOrEmpty(numero) && numero.Length == 10 && Regex.IsMatch(numero, @"^\d{10}$");
        }
    }
}
