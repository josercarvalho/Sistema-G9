using SistemaG9.Domain.Interfaces.Services;
using SistemaG9.Domain.Models;
using SistemaG9.Web.Util;
using SistemaG9.Web.ViewModels;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SistemaG9.Web.Areas.Escritorio.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IClienteService _servicoDeUsuario;
        private readonly IDadosBancariosService _dadosBancarios;
        private readonly IRedeService _sericoDeRede;

        public AccountController(IClienteService servicoDeUsuario, IDadosBancariosService servDadosBancarios, IRedeService servRede)
        {
            _servicoDeUsuario = servicoDeUsuario;
            _dadosBancarios = servDadosBancarios;
            _sericoDeRede = servRede;
        }

        // GET: Account
        public ActionResult Login()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            Session.Abandon();
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var usuario = _servicoDeUsuario.LogaCliente(viewModel.Nome, viewModel.Senha);
            if (usuario == null)
            {
                ModelState.AddModelError("", "Login ou Senha incorretos.");
                return View(viewModel);
            }

            //Irá setar um cookie encriptado com o Login do usuário autenticado
            FormsAuthentication.SetAuthCookie(usuario.Nome, false);

            //Sessão para uso de menus e redirecionamento do Sistema
            SessionManager.UsuarioLogado = usuario;

            //Sessão para uso de rotinas realcioandas as doações e reentradas
            var rede = _sericoDeRede.BuscarPorApelido(SessionManager.UsuarioLogado.Login);
            SessionManager.UsuarioNaRede = rede;

            //

            if (usuario.Nome == "NOME") return RedirectToAction("ConfirmaNome", "Account");

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            if (HttpContext.Session != null) HttpContext.Session.RemoveAll();
            Session.Abandon();
            Session.RemoveAll();

            ModelState.AddModelError("", "Usuário desconectado com sucesso.");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult RecuperarSenha()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RecuperarSenha(ForgotPasswordViewModel recuperaSenha)
        {
            if (_servicoDeUsuario.IsExistEmail(recuperaSenha.Email))
            {
                string email = recuperaSenha.Email.ToString(CultureInfo.InvariantCulture);
                Clientes cliente = _servicoDeUsuario.RecuperarUsuarioPorEmail(email);
                var login = cliente.Login;
                var htmlMensagem = new StringBuilder();
                htmlMensagem.AppendLine("<p>Sr(a) " + cliente.Nome + ", </p>");
                htmlMensagem.AppendLine(" </b>");
                htmlMensagem.AppendLine(" </b>");
                htmlMensagem.AppendLine("<p>Avisamos que sua senha houve solicitação de mudança.</p>");
                htmlMensagem.AppendLine(" </b>");
                htmlMensagem.AppendLine(" </b>");
                htmlMensagem.AppendLine("<p>Segue abaixo sua nova senha gerada de acesso a seu Escritório Virtual do Sistema G9. </p>");
                htmlMensagem.AppendLine(" </b>");
                htmlMensagem.AppendLine(" </b>");
                htmlMensagem.AppendLine("<p>Login: " + cliente.Login + " </p>");
                htmlMensagem.AppendLine("<p>Senha: @Senha2017 </p>");
                htmlMensagem.AppendLine(" </b>");
                htmlMensagem.AppendLine("<p> http://www.sistemag9.com.br/Escritorio/Account/Login </p>");
                htmlMensagem.AppendLine(" </b>");
                htmlMensagem.AppendLine("<p>Para alterar sua senha, após conectar, clique em MEUS DADOS. </p>");
                htmlMensagem.AppendLine(" </b>");
                htmlMensagem.AppendLine(" </b>");
                htmlMensagem.AppendLine("<p>Obs.: Não responder este e-mail. </p>");

                //var envia = new Email();
                //envia.EnviarEmail("Nova Senha Gerada - Sistema G9", htmlMensagem.ToString(), true, "", new[] { email }, "suporte@sistemag9.com.br");

                var body = "<p>Email De: {0} ({1})</p><p><em>MENSAGEM:</em></p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(email));  // replace with valid value 
                message.From = new MailAddress("suporte@sistemag9.com.br");  // replace with valid value
                message.Subject = "Nova Senha Gerada - Sistema G9";
                message.Body = string.Format(body, "Sistema G9", "suporte@sistemag9.com.br", htmlMensagem.ToString());
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "suporte@sistemag9.com.br",  // replace with valid value
                        Password = "J0t!nh@69"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "mail.top3host.com.br";
                    smtp.Port = 26;
                    smtp.EnableSsl = false;
                    await smtp.SendMailAsync(message);
                }

                cliente.Senha = "@Senha2017";
                _servicoDeUsuario.Update(cliente);

                TempData["SuccessMail"] = "E-mail enviado com sucesso. Verifique seu e-mail, caso não esteja em sua Caixa de Entrada, veja no Lixo Eletrônico, ou Caixa de Span.";

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "E-mail não encontrado ou não cadastrado. Tente novamente!");

            return View(recuperaSenha);
        }

        public ActionResult AlteraSenha()
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlteraSenha(ReseteLoginViewModel resetaSenha)
        {
            var senhaAtual = SessionManager.UsuarioLogado.Senha;
            if (!resetaSenha.SenhaAtual.Equals(senhaAtual))
            {
                ModelState.AddModelError("SenhaAtual", "Senha atual inválida, tente novamente...");
            }

            if (ModelState.IsValid)
            {
                var id = SessionManager.UsuarioLogado.ClienteId;
                Clientes cliente = _servicoDeUsuario.GetById(id);
                cliente.Senha = resetaSenha.Senha;
                _servicoDeUsuario.Update(cliente);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult ConfirmaNome()
        {
            return View();

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ConfirmaNome(NomeTitularViewModel nomeTitular)
        //{
        //    var id = SessionManager.UsuarioLogado.ClienteId;
        //    Clientes cliente = _servicoDeUsuario.GetById(id);
        //    cliente.Nome = nomeTitular.Nome.ToUpper();
        //    _servicoDeUsuario.Update(cliente);

        //    DadosBancarios dadosBancarios = _dadosBancarios.ListarPorCliente(id);
        //    dadosBancarios.Titular = nomeTitular.Titular.ToUpper();
        //    _dadosBancarios.Update(dadosBancarios);

        //    SessionManager.UsuarioLogado.Nome = nomeTitular.Nome.ToUpper();

        //    return RedirectToAction("Index", "Home");
        //}
    }
}