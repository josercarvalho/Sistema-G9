using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace SistemaG9.Web.Util
{
    public class Email
    {
        /// <summary>
        /// Envia um e-mail
        /// </summary>
        /// <param name="titulo">Título do e-mail</param>
        /// <param name="mensagem">Mensagem</param>
        /// <param name="emailHtml">Informa se o corpo do e-mail é um HTML. True=HMTL | False=Texto</param>
        /// <param name="caminhoAnexo">Arquivo em anexo</param>
        /// <param name="emailTo">E-mail(s) do(s) destinatário(s)</param>
        /// <param name="emailFrom">E-mail(s) do(s) remetente(s)</param>
        public async void EnviarEmail(string titulo, string mensagem, bool emailHtml, string caminhoAnexo, string[] emailTo, string emailFrom)
        {
            try
            {
                // Cria os objetos para envio de e-mail
                MailMessage email = new MailMessage();
                MailAddress fromAddress = new MailAddress(emailFrom);

                // Atribui à propriedade From o valor do Remetente
                email.From = fromAddress;

                // Destinatário
                email.To.Add(emailTo[0]);

                // Caso haja mais de um destinatário, envia o e-mail como cópia
                if (emailTo.Length > 1)
                {
                    // Guarda uma cópia do e-mail enviado
                    //Email.CC.Add(fromAddress);

                    for (int index = 1; index < emailTo.Length; index++)
                    //foreach (string email in emailTo)
                    {
                        // Adiciona destinatário com Cópia
                        if (!String.IsNullOrEmpty(emailTo[index]))
                        {
                            email.CC.Add(emailTo[index]);
                        }
                    }
                }

                // Verifica se há um arquivo a ser anexado
                if (!string.IsNullOrEmpty(caminhoAnexo))
                {
                    // Define o arquivo anexo
                    Attachment attachment = new Attachment(caminhoAnexo, MediaTypeNames.Application.Zip);
                    email.Attachments.Add(attachment);
                }

                //Atribui ao método Subject o assunto da mensagem
                email.Subject = titulo;
                //Define o formato da mensagem que pode ser Texto ou Html
                email.IsBodyHtml = emailHtml;
                //Atribui ao método Body a texto da mensagem
                email.Body = mensagem;
                email.Priority = MailPriority.High;

                //// Porta de conexão
                //var SmtpMail = new SmtpClient("mail.top3host.com.br", Convert.ToInt32(25));
                //SmtpMail.EnableSsl = true;
                //SmtpMail.Credentials = new NetworkCredential("suporte@sistemag9.com.br", "J0t!nh@69");
                //SmtpMail.Port = 25;
                //SmtpMail.Host = "mail.top3host.com.br";
                var smtpMail = new SmtpClient
                {
                    EnableSsl = false,
                    //COLOCA AQUI SEU EMAIL E SENHA DO GMAIL
                    Credentials = new NetworkCredential("suporte@sistemag9.com.br", "J0t!nh@69"),
                    Port = 25,
                    Host = "mail.top3host.com.br"
                };
                smtpMail.Send(email);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void EnviarEmailPreparado(string email)
        {
            string htmlMensagem = RecuperaTabelaConteudo();

            EnviarEmail("Console Aplication", "teste", true, "", new string[] { email }, "josercarvalho@gmail.com");
        }

        private string RecuperaTabelaConteudo()
        {
            StringBuilder htmlMensagem = new StringBuilder();
            htmlMensagem.AppendLine(@"
                            <table>
                            <th>Nome</th>
                            <th>Telefone</th>
                            <th>Endereço</th>");

            SqlConnection conexao = new SqlConnection("Data Source=(local);Initial Catalog=BeloCorpo;User ID=sa;Password=adm9612");

            conexao.Open();

            SqlCommand cmd = new SqlCommand("Select * from tblCliente", conexao);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string nome = reader["NOME"].ToString();
                string telefone = reader["TELEFONE"].ToString();
                string endereco = reader["ENDERECO"].ToString();


                htmlMensagem.AppendFormat(@"<tr>
                                <td>{0}</td>
                                <td>{1}</td>
                                <td>{2}</td>
                            </tr>", nome, telefone, endereco);
            }

            htmlMensagem.AppendLine("</table>");

            return htmlMensagem.ToString();
        }
    }
}