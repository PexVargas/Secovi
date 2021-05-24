using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portal.ViewModels.DeParaTipos;
using PortalPexIM.Model;

namespace PortalPexIM.Controllers
{
    public class DeParaTiposController : Controller
    {
        peximContext db = new peximContext();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult RetornarDadosTabela(int numRegistros, int pagina, int? CodEstado)
        {
            try
            {
                var siglaEstado = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                IQueryable<Palavra> qPalavras = (from dp in db.Palavrastipo
                                                 where dp.Excluido != 1
                                
                                                 &&  dp.SiglaEstado == siglaEstado
                                                 select new Palavra()
                                                 {
                                                     CodPalavra = dp.CodPalavraTipo,
                                                     NomePalavra = dp.Palavra,
                                                     //Estado = dp.Estados.NomeEstado,
                                                     SiglaEstado = siglaEstado
                                                 }).OrderBy(x => x.NomePalavra);

                int countPalavras = qPalavras.Count();

                IEnumerable<Palavra> palavras = (from u in qPalavras
                                                 select u);

                PalavrasListagem dados = new PalavrasListagem()
                {
                    Palavras = palavras.ToList(),// palavras.Skip((pagina - 1) * numRegistros).Take(numRegistros).ToList(),
                    CountPalavras = countPalavras,
                    Pagina = pagina,
                    NumRegistros = numRegistros,
                    CodEstado = CodEstado
                };
                dados.Palavras = dados.Palavras.OrderBy(x => x.NomePalavra).ToList();

                //Log.LogaNavegacao("Exibiu dados carregados na tabela em De/Para Tipos.", "De para Tipos");
                return Json(dados);
            }
            catch (Exception ex)
            {
                //Log.LogaErro("Ocorreu um erro ao tentar atualizar as informações da tabela em De/Para Tipos.", "De para Tipos", ex);
                return Json("Ocorreu um erro ao tentar atualizar as informações da tabela. Por favor, tente atualizar a página. Exceção: " + ex.Message);
            }
        }
    }
}
