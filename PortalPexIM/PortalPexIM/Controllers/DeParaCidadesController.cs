using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portal.ViewModels.DeParaCidades;
using PortalPexIM.Model;

namespace PortalPexIM.Controllers
{
    public class DeParaCidadesController : Controller
    {
        peximContext db = new peximContext();
        public IActionResult Index()
        {
            FiltrosListagem filtros = new FiltrosListagem();
            filtros.Cidades = (from c in db.Palavrascidade
                               //where c.Excluido != 1
                               select new Portal.ViewModels.DePara.Cidade()
                               {
                                   CodCidade =  c.CodCidade,
                                   NomeCidade = c.Palavra,
                                   SiglaEstadoCidade = c.SiglaEstado
                               }).OrderBy(x => x.NomeCidade).ToList();

            return View(filtros);
        }

        [HttpPost]
        public JsonResult RetornarDadosTabela(int numRegistros, int pagina, int? CodEstado, int? CodCidade)
        {
            try
            {
                peximContext dbe = new peximContext();
                var siglaEstado = User.FindFirst(ClaimTypes.NameIdentifier).Value;


                IQueryable<Palavra> qPalavras = (from dp in db.Palavrascidade
                                                 where (dp.SiglaEstado == siglaEstado)
                                                  && dp.Excluido != 1
                                                 select new Palavra()
                                                 {
                                                     CodPalavra = dp.CodPalavra,
                                                     NomePalavra = dp.Palavra.ToUpper(),
                                                     SiglaEstado = siglaEstado,
                                                 }).Distinct().OrderBy(x => x.NomePalavra);


                int countPalavras = qPalavras.Count();
                IEnumerable<Palavra> palavras = (from u in qPalavras
                                                 select u);

                PalavrasListagem dados = new PalavrasListagem()
                {
                    Palavras = palavras.ToList(),
                    CountPalavras = countPalavras,
                    Pagina = pagina,
                    NumRegistros = numRegistros,
                    CodEstado = CodEstado,
                    CodCidade = CodCidade
                };

               // Log.LogaNavegacao("Exibiu dados carregados na tabela em De/Para Cidades.", "De para Cidades");
                return Json(dados);
            }
            catch (Exception ex)
            {
                return Json("Ocorreu um erro ao tentar atualizar as informações da tabela. Por favor, tente atualizar a página. Exceção: " + ex.Message);
            }
        }
    }
}
