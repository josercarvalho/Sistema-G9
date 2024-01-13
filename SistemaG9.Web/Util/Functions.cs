using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace SistemaG9.Web.Util
{
    /// <summary>
    /// Métodos utilitários
    /// </summary>
    public static class Functions
    {

        public static string GetRandomString()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());
            return result;
        }

        public static int CalculaIdade(DateTime dataNascimento)
        {

            int anos = DateTime.Now.Year - dataNascimento.Year;

            if (DateTime.Now.Month < dataNascimento.Month ||
                (DateTime.Now.Month == dataNascimento.Month && DateTime.Now.Day < dataNascimento.Day))

                anos--;

            return anos;

        }

        public static string GetDataAtualPorExtenso()
        {
            return DateTime.Now.ToString("dddd").ToUpper() + ", " + DateTime.Now.ToString("dd").ToUpper() +
                   " DE " + DateTime.Now.ToString("MMMM").ToUpper() + " DE " + DateTime.Now.ToString("yyyy").ToUpper() +
                   " " +
                   DateTime.Now.ToString("HH:mm").ToUpper();
        }

        public static string GetServerIP()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress address in ipHostInfo.AddressList)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                    return address + "|" + ipHostInfo.HostName;
            }

            return string.Empty;
        }

        public enum TypeString
        {
            Text,
            Numeric,
            CNPJ,
            CPF,
            Date,
            Int,
            CEP,
            Telefone,
            Currency,
            Percent,
            Milhar,
            AnoMes
        }

        public static string FormatString(string value, TypeString type)
        {
            try
            {
                NumberFormatInfo numberFormatInfo = new CultureInfo("pt-BR", false).NumberFormat;

                switch (type)
                {
                    case TypeString.Text:
                        return value;
                    case TypeString.Numeric:
                        return Convert.ToDouble(value).ToString("#,##0.00");
                    case TypeString.CNPJ:
                        value = value.PadLeft(14, '0');
                        return string.Format("{0}.{1}.{2}/{3}-{4}", value.Substring(0, 2), value.Substring(2, 3),
                            value.Substring(5, 3), value.Substring(8, 4), value.Substring(12, 2));
                    case TypeString.CPF:
                        value = value.PadLeft(11, '0');
                        return string.Format("{0}.{1}.{2}-{3}", value.Substring(0, 3), value.Substring(3, 3),
                            value.Substring(6, 3), value.Substring(9, 2));
                    case TypeString.Date:
                        if (Convert.ToDateTime(value) == Convert.ToDateTime("1/1/1900"))
                            return string.Empty;
                        else
                            return Convert.ToDateTime(value).ToString("dd/MM/yyyy");
                    case TypeString.CEP:
                        value = value.PadLeft(8, '0');
                        return string.Format("{0}.{1}-{2}", value.Substring(0, 2), value.Substring(2, 3),
                            value.Substring(5, 3));
                    case TypeString.Telefone:
                        return string.Format("({0}) {1}", value.Substring(0, 2), value.Substring(2, value.Length - 2));
                    case TypeString.Currency:
                        return Convert.ToDouble(value).ToString("C");
                    case TypeString.Percent:
                        numberFormatInfo.PercentDecimalDigits = 0;
                        return Convert.ToDouble(value).ToString("P", numberFormatInfo);
                    case TypeString.Milhar:
                        return Convert.ToDouble(value).ToString("#,##0");
                    case TypeString.AnoMes:
                        return string.Format("{0}/{1}", value.Substring(value.Length - 2), value.Substring(0, 4));
                    default:
                        return value;
                }
            }
            catch
            {
                return value;
            }
        }

        public static string CalcularHora(DateTime horaIni, DateTime horaFim)
        {
            DateTime horaInicial = DateTime.Parse(horaIni.ToString(CultureInfo.CurrentCulture));
            DateTime horaFinal = DateTime.Parse(horaFim.ToString(CultureInfo.CurrentCulture));
            var tempoPercorrido = horaFinal.Subtract(horaInicial);
            return tempoPercorrido.ToString();
        }

        /// <summary>
        /// Remove caracteres não numéricos
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveNaoNumericos(string text)
        {
            var reg = new Regex(@"[^0-9]");
            string ret = reg.Replace(text, string.Empty);
            return ret;
        }

        /// <summary>
        /// Valida se um cpf é válido
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public static bool ValidaCpf(string cpf)
        {
            //Remove formatação do número, ex: "123.456.789-01" vira: "12345678901"
            cpf = RemoveNaoNumericos(cpf);

            if (cpf.Length > 11)
                return false;

            switch (cpf)
            {
                case "00000000000":
                case "11111111111":
                case "22222222222":
                case "33333333333":
                case "44444444444":
                case "55555555555":
                case "66666666666":
                case "77777777777":
                case "88888888888":
                case "99999999999":
                    return false;
            }

            while (cpf.Length != 11)
                cpf = '0' + cpf;

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (cpf[i] != cpf[0])
                    igual = false;

            if (igual || cpf == "12345678909")
                return false;

            var numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(cpf[i].ToString(CultureInfo.InvariantCulture));

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }

        //Método que valida o CNPJ 
        /// <summary>
        /// Valida se um cnpj é válido
        /// </summary>
        /// <param name="vrCNPJ"></param>
        /// <returns></returns>
        public static bool ValidaCNPJ(string vrCNPJ)
        {
            if (vrCNPJ == null) throw new ArgumentNullException("vrCNPJ");

            string cnpj = vrCNPJ.Replace(".", "");
            cnpj = cnpj.Replace("/", "");
            cnpj = cnpj.Replace("-", "");

            const string ftmt = "6543298765432";
            var digitos = new int[14];
            var soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            var resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;
            var cnpjOk = new bool[2];
            cnpjOk[0] = false;
            cnpjOk[1] = false;

            try
            {
                int nrDig;
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(
                     cnpj.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] *
                        int.Parse(ftmt.Substring(
                          nrDig + 1, 1)));
                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] *
                        int.Parse(ftmt.Substring(
                          nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (resultado[nrDig] == 1))
                        cnpjOk[nrDig] = (
                        digitos[12 + nrDig] == 0);

                    else
                        cnpjOk[nrDig] = (
                        digitos[12 + nrDig] == (
                        11 - resultado[nrDig]));

                }

                return (cnpjOk[0] && cnpjOk[1]);

            }
            catch
            {
                return false;
            }

        }

        //Método que valida o Cep
        /// <summary>
        /// Valida se um cep é válido
        /// </summary>
        /// <param name="cep"></param>
        /// <returns></returns>
        public static bool ValidaCep(string cep)
        {
            if (cep.Length == 8)
            {
                cep = cep.Substring(0, 5) + "-" + cep.Substring(5, 3);
                //txt.Text = cep;
            }
            return Regex.IsMatch(cep, ("[0-9]{5}-[0-9]{3}"));
        }

        //Método que valida o Email
        public static bool ValidaEmail(string email)
        {
            return Regex.IsMatch(email, ("(?<user>[^@]+)@(?<host>.+)"));
        }
        
        //public static MvcHtmlString TypeaheadFor<TModel, TValue>(
        //this HtmlHelper<TModel> htmlHelper,
        //Expression<Func<TModel, TValue>> expression,
        //IEnumerable<string> source,
        //int items = 8)
        //{
        //    if (expression == null)
        //        throw new ArgumentNullException("expression");

        //    if (source == null)
        //        throw new ArgumentNullException("source");

        //    var jsonString = new JavaScriptSerializer().Serialize(source);

        //    return htmlHelper.TextBoxFor(
        //        expression,
        //        new
        //        {
        //            autocomplete = "off",
        //            data_provide = "typeahead",
        //            data_items = items,
        //            data_source = jsonString
        //        }
        //    );
        //}
    }
}