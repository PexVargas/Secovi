using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portal.ViewModels.DePara;
using PortalPexIM.Model;
using PortalPexIM.ViewModel.DeParaCidades;

namespace PortalPexIM.Controllers
{
    public class DeParaController : Controller
    {
        peximContext db = new peximContext();
        public ActionResult Index()
        {

            return View();
        }


        /// Método que vai retornar a lista de Cidades (para)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PopulaListaPalavrasRelacionadasCidades(int cod)
        {
            var palavras = db.Palavrasrelacionadascidade.Where(x => x.CodPalavra == cod && x.Excluida != 1)
                 .Select(x => new Palavra()
                 {

                     PalavraRelacionada = x.Palavra.ToUpper()
                 }).ToList();

            //   Log.LogaNavegacao("Preencheu a lista de Palavras Relacionadas Cidades em De/Para.", "De Para");
            return Json(palavras);
        }

        [HttpPost]
        public JsonResult PopulaListaPalavrasRelacionadasBairros(int cod)
        {
            var palavras = db.Palavrasrelacionadasbairro.Where(x => x.CodPalavra == cod && x.Excluido != 1)
                 .Select(x => new Palavra()
                 {
                     PalavraRelacionada = x.Palavra
                 }).OrderBy(x => x.PalavraRelacionada).ToList();

            return Json(palavras);
        }

        [HttpPost]
        public JsonResult PopulaListaPalavrasRelacionadasTipos(int cod)
        {
            var palavras = db.Palavrasrelacionadastipo.Where(x => x.CodPalavra == cod && x.Excluido != 1)
                 .Select(x => new Palavra()
                 {

                     PalavraRelacionada = x.Palavra
                 }).ToList();

            return Json(palavras);
        }

        [HttpPost]
        public JsonResult RetornarPalavrasBairros(int cod)
        {
            var siglaEstado = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var palavras = db.Palavrasbairros.Where(x => x.SiglaEstado == siglaEstado && x.Excluido != 1 && (x.CodCidade == cod))
                 .Select(x => new Palavra()
                 {
                     CodPalavra = x.CodPalavra,
                     NomePalavra = x.Palavra.ToUpper()
                 }).Distinct().OrderBy(x => x.NomePalavra).ToList();


            return Json(palavras);
        }

        [HttpPost]
        public JsonResult RetornarTipos(int codEstado)
        {
            var siglaEstado = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            List<Tipo> lstResult = new List<Tipo>();

            List<Tipo> listaTipos = new List<Tipo>();
            listaTipos = (from im in db.Imoveisclassificados
                          where im.SiglaEstado == siglaEstado
                          && im.Excluido != 1
                          && im.Tipo != null
                          select new Tipo()
                          {
                              NomeTipo = im.Tipo.ToUpper(),
                          }
                          ).Distinct().OrderBy(x => x.NomeTipo).ToList();

            var lstPalavrasTipos = (from im in db.Palavrastipo
                                    where im.SiglaEstado == siglaEstado
                                    && im.Excluido != 1
                                    select new Tipo()
                                    {
                                        NomeTipo = im.Palavra.ToUpper(),

                                    }
             ).Distinct().OrderBy(x => x.NomeTipo).ToList();

            foreach (var item in listaTipos)
            {
                if (lstPalavrasTipos.Where(x => x.NomeTipo.ToUpper().Trim() == item.NomeTipo.ToUpper().Trim()).Count() == 0)
                {
                    lstResult.Add(new Tipo() { NomeTipo = item.NomeTipo });
                }
            }

            // Log.LogaNavegacao("Retornou lista de Tipos em De/Para.", "De Para");
            return Json(lstResult);
        }

        [HttpPost]
        public JsonResult RetornarBairros(int codEstado)
        {
            var siglaEstado = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            List<Bairro> listaBairros = new List<Bairro>();
            List<Bairro> lstResult = new List<Bairro>();

            listaBairros = (from im in db.Imoveisclassificados
                            where im.SiglaEstado == siglaEstado
                            && im.Excluido != 1

                            select new Bairro()
                            {
                                NomeBairro = im.Cidade.ToUpper() + " - " + im.Bairro.ToUpper(),
                                Cidade = im.Cidade.ToUpper().Trim(),
                                BairroStr = im.Bairro.ToUpper().Trim()
                            }
                            ).Distinct().OrderBy(x => x.NomeBairro).ToList();


            var lstPalavrasBairro = (from im in db.Palavrasbairros //seguir aqui
                                     join c in db.Cidades on im.CodCidade equals c.CodCidade
                                     where im.SiglaEstado == siglaEstado
                                     && im.Excluido != 1
                                     select new Bairro()
                                     {
                                         NomeBairro = im.Palavra.ToUpper().Trim(),
                                         Cidade = c.NomeCidade.ToUpper().Trim()
                                     }
                                     ).Distinct().OrderBy(x => x.NomeBairro).ToList();


            foreach (var item in listaBairros)
            {
                if (lstPalavrasBairro.Where(x => x.NomeBairro == item.BairroStr && x.Cidade == item.Cidade).Count() == 0)
                {
                    lstResult.Add(new Bairro() { NomeBairro = item.NomeBairro, Cidade = item.Cidade, BairroStr = item.BairroStr });
                }
            }

            return Json(lstResult.OrderBy(x => x.Cidade).ThenBy(x => x.BairroStr));
        }

        [HttpPost]
        public JsonResult RetornarCidadesBairro(int codEstado)
        {
            var siglaEstado = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            List<Cidade> listaCidades = new List<Cidade>();
            listaCidades = (from im in db.Cidades
                            where im.SiglaEstado == siglaEstado
                            select new Cidade()
                            {
                                NomeCidade = im.NomeCidade.ToUpper().Trim(),
                                CodCidade = im.CodCidade
                            }
                                   ).Distinct().OrderBy(x => x.NomeCidade).ToList();

            //  Log.LogaNavegacao("Retornar lista de Cidades/Bairros em De/Para.", "De Para");
            return Json(listaCidades);
        }

        [HttpPost]
        public JsonResult RetornarCidadesDePara(int codEstado)
        {
            FiltrosLocalizacao filtros = new FiltrosLocalizacao();

            // var siglaestado = db.Estados.Find(codEstado).SiglaEstado;
            var siglaEstado = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            filtros = RetornaCidades(); // RetornaCidades(siglaEstado);

            //  Log.LogaNavegacao("Exibiu a lista de Cidades em De/Para.", "De Para");
            return Json(filtros);
        }


        private FiltrosLocalizacao RetornaCidades()
        {
            var siglaEstado = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            using (var dbx = new peximContext())
            {

                List<Cidade> lstResult = new List<Cidade>();

                var lstCidades = (from im in dbx.Imoveisclassificados
                                  where im.SiglaEstado == siglaEstado
                                  && im.Excluido != 1
                                  select new Cidade
                                  {
                                      NomeCidade = im.Cidade.ToUpper(),
                                  }
                         ).Distinct().OrderBy(x => x.NomeCidade).Where(x => x.NomeCidade != null).ToList();

                var lstPalavrasCidades = (from im in dbx.Palavrasrelacionadascidade
                                          join c in dbx.Palavrascidade on im.CodPalavra equals c.CodPalavra
                                          where c.SiglaEstado == siglaEstado
                                          && im.Excluida != 1
                                          select new Cidade
                                          {
                                              NomeCidade = im.Palavra.ToUpper(),
                                          }
                     ).Distinct().OrderBy(x => x.NomeCidade).ToList();

                foreach (var item in lstCidades)
                {
                    if (lstPalavrasCidades.Where(x => x.NomeCidade.ToUpper().Trim() == item.NomeCidade.ToUpper().Trim()).Count() == 0)
                    {
                        lstResult.Add(new Cidade { NomeCidade = item.NomeCidade });
                    }
                }

                var f = new FiltrosLocalizacao(lstResult);
                return f;
            }
        }

        [HttpPost]
        public JsonResult SalvarTipo([FromBody] DeParaPalavras filtro)
        {
            var palavrasRelacionadas = db.Palavrasrelacionadastipo.Where(x => x.CodPalavra == filtro.CodPalavra).ToList();

            foreach (var item in palavrasRelacionadas)
            {
                item.Excluido = 1;
            }

            foreach (var palavra in filtro.Palavras)
            {
                Palavrasrelacionadastipo p = new Palavrasrelacionadastipo();
                p.CodPalavra = filtro.CodPalavra;
                p.Palavra = palavra;
                p.Excluido = 0;
                db.Palavrasrelacionadastipo.Add(p);
            }

            db.SaveChanges();
            //  Log.LogaNavegacao("Salvou Tipo em De/Para.", "De Para");
            return Json(true);
        }

        [HttpPost]
        public JsonResult SalvarCidade([FromBody] DeParaPalavras filtro)//(int CodPalavra, string[] Palavras)
        {
            var palavrasRelacionadas = db.Palavrasrelacionadascidade.Where(x => x.CodPalavra == filtro.CodPalavra).ToList();

            foreach (var item in palavrasRelacionadas)
            {
                item.Excluida = 1;
            }

            if (filtro.Palavras != null)
            {
                foreach (var palavra in filtro.Palavras)
                {
                    Palavrasrelacionadascidade p = new Palavrasrelacionadascidade();
                    p.CodPalavra = filtro.CodPalavra;
                    p.Palavra = palavra;
                    p.Excluida = 0;
                    db.Palavrasrelacionadascidade.Add(p);
                }
            }

            db.SaveChanges();
            ////Log.LogaNavegacao("Salvou Cidade em De/Para.", "De Para");
            return Json(true);
        }

        [HttpPost]
        public JsonResult SalvarBairro([FromBody] DeParaPalavras filtro)
        {
            var palavrasRelacionadas = db.Palavrasrelacionadasbairro.Where(x => x.CodPalavra == filtro.CodPalavra).ToList();

            foreach (var item in palavrasRelacionadas)
            {
                item.Excluido = 1;
            }

            foreach (var palavra in filtro.Palavras)
            {
                Palavrasrelacionadasbairro p = new Palavrasrelacionadasbairro();
                p.CodPalavra = filtro.CodPalavra;
                p.Palavra = palavra;
                p.Excluido = 0;
                db.Palavrasrelacionadasbairro.Add(p);
            }

            db.SaveChanges();
            // Log.LogaNavegacao("Salvou Bairro em De/Para.", "De Para");
            return Json(true);
        }

        [HttpPost]
        public void AplicarDePara(int DePara)
        {

            var siglaEstado = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            //var estado = db.Estados.Find(CodEstado);
            var tipoDePara = string.Empty;
            //int CodUsuario = (int)Session["CodUsuarioLogado"];
            string email = "";

            //var usuarioEnvio = db.Usuarios.Find(CodUsuario);
            //if (usuarioEnvio.Email != null)
            //    email = usuarioEnvio.Email;

            switch (DePara)
            {
                case 1:
                    tipoDePara = "Cidade";

                    var palavras = db.Palavrascidade.Where(x => x.SiglaEstado == siglaEstado && x.Excluido != 1).ToList();

                    foreach (var palava in palavras)
                    {
                        var palavrasRelacionadas = db.Palavrasrelacionadascidade.Where(x => x.CodPalavra == palava.CodPalavra).ToList();

                        foreach (var item in palavrasRelacionadas)
                        {
                            var imoveis = db.Imoveisclassificados.Where(x => x.Cidade.ToUpper().Trim() == item.Palavra.ToUpper().Trim() 
                            && x.SiglaEstado == siglaEstado 
                            && x.Excluido != 1
                            && (x.DataClassificacao.Value.Year == DateTime.Now.Year && x.DataClassificacao.Value.Month == DateTime.Now.Month)).ToList();

                            foreach (var im in imoveis)
                            {
                                im.Cidade = palava.Palavra;
                            }

                            db.SaveChanges();
                        }
                    }

                    break;
                case 2:
                    tipoDePara = "Bairro";
                    var cidades = db.Cidades.Where(x => x.SiglaEstado == siglaEstado).ToList();

                    var palavrasBairro = db.Palavrasbairros.Where(x => x.SiglaEstado == siglaEstado && x.Excluido != 1).ToList();

                    foreach (var palava in palavrasBairro)
                    {
                        Console.WriteLine(palava.Palavra);
                        var palavrasRelacionadas = db.Palavrasrelacionadasbairro.Where(x => x.CodPalavra == palava.CodPalavra).ToList();

                        foreach (var item in palavrasRelacionadas)
                        {
                            var cidade = cidades.Where(x => x.CodCidade == palava.CodCidade).FirstOrDefault().NomeCidade.ToUpper().Trim();

                            var imoveis = db.Imoveisclassificados.Where(x => x.Bairro.ToUpper().Trim() == item.Palavra.ToUpper().Trim()
                            && x.SiglaEstado == siglaEstado
                            && x.Excluido != 1
                            && x.Cidade.ToUpper().Trim() == cidade
                            && (x.DataClassificacao.Value.Year == DateTime.Now.Year && x.DataClassificacao.Value.Month == DateTime.Now.Month)).ToList();

                            foreach (var im in imoveis)
                            {
                                im.Bairro = palava.Palavra;
                            }

                            db.SaveChanges();
                        }
                    }
                    break;
                default:
                    tipoDePara = "Tipo";

                    var palavrasTipo = db.Palavrastipo.Where(x => x.SiglaEstado == siglaEstado && x.Excluido != 1).ToList();

                    foreach (var palava in palavrasTipo)
                    {
                        var palavrasRelacionadasTipo = db.Palavrasrelacionadastipo.Where(x => x.CodPalavra == palava.CodPalavraTipo).ToList();

                        foreach (var item in palavrasRelacionadasTipo)
                        {
                            var imoveis = db.Imoveisclassificados.Where(x => x.Tipo.ToUpper().Trim() == item.Palavra.ToUpper().Trim() 
                            && x.SiglaEstado == siglaEstado 
                            && x.Excluido != 1 
                            && (x.DataClassificacao.Value.Year == DateTime.Now.Year && x.DataClassificacao.Value.Month == DateTime.Now.Month)).ToList();

                            foreach (var im in imoveis)
                            {
                                im.Tipo = palava.Palavra;
                            }

                            db.SaveChanges();
                        }
                    }
                    break;
            }

            try
            {
                using (var dbx = new peximContext())
                {
                    //IObjectContextAdapter dbcontextadapter = (IObjectContextAdapter)dbx;
                    //dbcontextadapter.ObjectContext.CommandTimeout = 20000;

                    //dbx.spDePara(DePara, CodEstado);
                }
            }
            catch (Exception ex)
            {

                var teste = ex;
            }


            //string html = db.Configuracoes.Where(x => x.Chave == "TemplateDePara").First().Valor;
            //html = html.Replace("{Param:DePara}", tipoDePara)
            //    .Replace("{Param:Estado}", estado.NomeEstado)
            //    .Replace("{Param:Url}", "http://" + Request.Url.Host + "/");

            //  Log.LogaNavegacao("Acessou Applicar Processo em De/Para.", "De Para");

            //Thread t1 = new Thread(delegate()
            //{
            //    try
            //    {
            //        db.spDePara(DePara, CodEstado);

            //        if (email != "" && email != null)
            //            EnviarEmail("Processo De Para executado", html, email, true);
            //    }
            //    catch (Exception)
            //    {
            //        EnviarEmail("ERRO Processo De Para executado", html, "eduardo.vargas@plugar.com.br", true);
            //    }
            //});

            //t1.Priority = ThreadPriority.Lowest;
            //t1.Start();

        }

        public void EnviarEmail(string assunto, string mensagem, string destinatario, bool bodyHtml)
        {
            //DadosEnvioEmail dadosMail = db.DadosEnvioEmail.FirstOrDefault();

            //SmtpClient client = new SmtpClient(dadosMail.smtp);

            //client.Port = dadosMail.porta;
            //client.UseDefaultCredentials = dadosMail.defaultCredential;
            //client.EnableSsl = dadosMail.SSL;

            //if (client.UseDefaultCredentials == false)
            //    client.Credentials = new System.Net.NetworkCredential(dadosMail.mailAuth, dadosMail.mailPass);

            //MailMessage mail = new MailMessage(dadosMail.mailSender, destinatario, assunto, mensagem);
            //mail.IsBodyHtml = bodyHtml;
            //mail.BodyEncoding = System.Text.Encoding.UTF8;

            //client.Send(mail);
        }
    }
}

