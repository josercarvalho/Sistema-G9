using SistemaG9.Domain.Interfaces.Services;
using SistemaG9.Domain.Models;
using SistemaG9.Web.Util;
using SistemaG9.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace SistemaG9.Web.Controllers
{
    public class CadastroController : Controller
    {
        //private readonly EntitieContext _db = new EntitieContext();
        private readonly IClienteService _cliente;
        private readonly IBancoService _banco;
        private readonly ICidadeService _cidade;
        private readonly IEstadoService _estado;
        private readonly IDadosBancariosService _dadosbancarios;
        private readonly IRedeService _rede;

        public CadastroController(IClienteService servCliente, 
                                  IBancoService servBanco, 
                                  ICidadeService servCidade,
                                  IEstadoService servEstado,
                                  IDadosBancariosService servDadosBanco,
                                  IRedeService servRede)
        {
            _cliente = servCliente;
            _banco = servBanco;
            _cidade = servCidade;
            _estado = servEstado;
            _dadosbancarios = servDadosBanco;
            _rede = servRede;
        }

        // GET: Cadastro/Create
        public ActionResult Create()
        {
            ViewBag.BancoId = new SelectList(_banco.GetAll(), "BancoId", "Nome");
            ViewBag.Estado = _estado.BuscaEstados().Select(x => new { x.EstadoId, x.Nome }).ToList();
            return View();
        }

        // POST: Cadastro/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ClienteId,PerfilUsuarioId,Login,Senha,Nome,DataNascimento,CPF,Email,Endereco,Numero,Complemento,Bairro,CEP,PaisId,CidadeId,EstadoId,Telefone,Operadora,WhatsApp,SKYPE,DataCadastro,Reentradas,Status,BancoId,Titular.TipoConta,Agencia,Conta,Operacao")] CadastroViewModel cadastroViewModel)
        public ActionResult Create(CadastroViewModel cadastroViewModel)
        {
            try
            {
                if (_cliente.IsExistEmail(cadastroViewModel.Email))
                    ModelState.AddModelError("Email", "E-mail cadastrado, tente outro...");

                if (_cliente.IsExistLogin(cadastroViewModel.Login))
                    ModelState.AddModelError("login", "Login já cadastrado, tente outro...");

                var cpf = cadastroViewModel.CPF;
                if (Functions.ValidaCpf(cpf) == false)
                    ModelState.AddModelError("CPF", "CPF Inválido...");

                if (ModelState.IsValid)
                {
                    var cliente = new Clientes();
                    var dadosBancarios = new DadosBancarioViewModel();

                    // Dados do Cliente para salvar
                    //-----------------------------

                    cliente.PerfilUsuarioId = 3;
                    cliente.Login = cadastroViewModel.Login;
                    cliente.Senha = cadastroViewModel.Senha;
                    cliente.Nome =  cadastroViewModel.Nome.ToUpper();
                    cliente.Numero = cadastroViewModel.Numero;
                    cliente.Operadora = cadastroViewModel.Operadora;
                    cliente.DataNascimento = cadastroViewModel.DataNascimento;
                    cliente.CPF = cadastroViewModel.CPF;
                    cliente.Email = cadastroViewModel.Email;
                    cliente.Endereco = cadastroViewModel.Endereco;
                    cliente.Complemento = cadastroViewModel.Complemento;
                    cliente.Bairro = cadastroViewModel.Bairro;
                    cliente.CEP = cadastroViewModel.CEP;
                    cliente.PaisId = 1;
                    cliente.CidadeId = cadastroViewModel.CidadeId;
                    cliente.EstadoId = cadastroViewModel.EstadoId;
                    cliente.Telefone = cadastroViewModel.Telefone;
                    cliente.SKYPE = cadastroViewModel.SKYPE;
                    cliente.WhatsApp = cadastroViewModel.WhatsApp;
                    //cliente.Reentradas = 0;
                    cliente.Status = 0;
                    _cliente.Add(cliente);

                    // Dados Báncários do Cliente
                    //---------------------------

                    dadosBancarios.ClienteId = cliente.ClienteId;
                    dadosBancarios.BancoId = cadastroViewModel.BancoId;
                    dadosBancarios.Titular = cadastroViewModel.Titular.ToUpper();
                    dadosBancarios.Agencia = cadastroViewModel.Agencia;
                    dadosBancarios.Conta = cadastroViewModel.Conta;
                    dadosBancarios.TipoConta = cadastroViewModel.TipoConta;
                    dadosBancarios.Operacao = cadastroViewModel.Operacao;
                    _dadosbancarios.Add(dadosBancarios);

                    _rede.CadastoNaRede(cliente.ClienteId, 1, cliente.Login, cliente.Login);

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            //ViewBag.Estado = _estado.BuscaEstados().ToList();
            ViewBag.Estado = _estado.BuscaEstados().Select(x => new { x.EstadoId, x.Nome }).ToList();
            ViewBag.BancoId = new SelectList(_banco.GetAll(), "BancoId", "Nome");

            return View(cadastroViewModel);
        }

        //public void CadastoNaRede(int id, string login)
        //{
        //    // Busca ultima posição
        //    var redeMatriz = _db.Rede.OrderByDescending(x => x.Posicao)
        //                        .Select(x => new { x.ClienteId, x.Login, x.Recebedor, x.Posicao });
        //    var ultimoCadastrado = redeMatriz.Max(x => x.Posicao);

        //    // Busca cliente patrocinador da ultima posição
        //    var clienteRecebedor = redeMatriz.SingleOrDefault(x => x.Posicao.Equals(ultimoCadastrado));
        //    if (clienteRecebedor != null)
        //    {
        //        var recebedorDaPosicao = clienteRecebedor.Recebedor;
        //        // Busca quantidade do patrocinador na matriz que não pode exceder a 3
        //        var recebedor = redeMatriz.Count(x => x.Recebedor.Equals(recebedorDaPosicao));
        //        // Determina o patrocinador para o próximo cliente da matriz
        //        //var origem = patrocinador.Count() < 3 ? origemDaPosicao : origemDaPosicao + 1
        //        int origem;
        //        if (recebedor < 3)
        //        {
        //            origem = recebedorDaPosicao;
        //        }
        //        else
        //        {
        //            origem = recebedorDaPosicao + 1;
        //        }

        //        var matriz = new Rede
        //        {
        //            MatrizId = 1,
        //            Nivel = 1,
        //            ClienteId = id,
        //            Login = login,
        //            Apelido = login,
        //            Recebedor = origem,
        //            Posicao = ultimoCadastrado + 1,
        //            Status = 0
        //        };

        //        _db.Rede.Add(matriz);

        //        //Salvar UpLine do Cliente
        //        var upline = new UpLine
        //        {
        //            ClienteId = id,
        //            Recebedor = origem,
        //            Nivel = 1,
        //            Valor = 50,
        //            Status = 0
        //        };
        //        _db.UpLines.Add(upline);
        //        _db.SaveChanges();

        //        //Salva Doação do Cliente ficando em aberto para recebimento.
        //        var orDefault = _db.Cliente.SingleOrDefault(x => x.ClienteId.Equals(origem));
        //        DateTime dataLimite = DateTime.Now.AddDays(1);
        //        var doacao = new Doacoes
        //        {
        //            MatrizId = 1,
        //            ClienteId = id,
        //            Doador = login,
        //            Recebedor = orDefault.Login,
        //            ExpiraData = dataLimite,
        //            Tipo = 0,
        //            Valor = 50,
        //            Status = 0
        //        };
        //        _db.Doacao.Add(doacao);
        //        _db.SaveChanges();
        //    }
        //}

        private IEnumerable<Cidades> GetCidades(int estadoId)
        {
            return _cidade.BuscarPorEstado(estadoId).ToList();
        }

        //[AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadCidadeId(string estadoId)
        {
            var cidadeList = GetCidades(Convert.ToInt32(estadoId));

            var cidadeData = cidadeList.Select(m => new SelectListItem
            {
                Text = m.Nome,
                Value = m.CidadeId.ToString(CultureInfo.InvariantCulture),
            });
            return Json(cidadeData, JsonRequestBehavior.AllowGet);
        }

        //public bool IsExistEmail(string email)
        //{
        //    var query = _cliente.RecuperarUsuarioPorEmail(email); // _db.Cliente.FirstOrDefault(p => p.Email.Equals(email)));

        //    return query != null;
        //}

        //public bool IsExistLogin(string login)
        //{
        //    if (login.Contains("sistema")) return true;

        //    var query = _cliente.ListarPorLogin(login); // _db.Cliente.FirstOrDefault(p => p.Login.Equals(login));
        //    return query != null;
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cliente.Dispose();
                _banco.Dispose();
                _cidade.Dispose();
                _dadosbancarios.Dispose();
                _estado.Dispose();
                _rede.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}