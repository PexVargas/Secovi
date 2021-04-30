using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortalPexIM.Model;
using PortalPexIM.Models;
using PortalPexIM.ViewModel;

namespace PortalPexIM.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        peximContext db = new peximContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
           
            var siglaEstado = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            FiltroPesquisa filtro = new FiltroPesquisa();
            filtro.Cidades = db.Imoveisclassificados.Where(x=>x.SiglaEstado == siglaEstado).Select(x => x.Cidade).Distinct().ToArray();
            filtro.Bairros = db.Imoveisclassificados.Where(x => x.SiglaEstado == siglaEstado).Select(x => x.Bairro).Distinct().ToArray();
            filtro.Tipos = db.Imoveisclassificados.Where(x => x.SiglaEstado == siglaEstado).Select(x => x.Tipo).Distinct().ToArray();

            return View(filtro);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public JsonResult GetEvolutivo([FromBody] FiltroPesquisa filtro) 
        {
            peximContext dbe = new peximContext();
            var siglaEstado = User.FindFirst(ClaimTypes.NameIdentifier).Value;
          

            var mes = Convert.ToInt32(filtro.DtReferencia.Split("/")[0]);
            var ano = Convert.ToInt32(filtro.DtReferencia.Split("/")[1]);
            var dataBase = new DateTime(ano, mes, 1).AddMonths(-13);

            var imoveis = (from i in dbe.Imoveisclassificados
                           where i.Tipo!=null 
                           && ((filtro.Cidades == null || filtro.Cidades.Length == 0)|| filtro.Cidades.Contains(i.Cidade)) 
                           && ((filtro.Bairros == null || filtro.Bairros.Length == 0)|| filtro.Bairros.Contains(i.Bairro))
                           && ((filtro.Tipos == null || filtro.Tipos.Length == 0) ||filtro.Tipos.Contains(i.Tipo))
                           && i.TipoImovel == filtro.TipoImovel
                           && i.Excluido == 0
                           && i.SiglaEstado == siglaEstado
                           && i.DataClassificacao >= (dataBase)
                           group i by new { i.DataClassificacao.Value.Year, i.DataClassificacao.Value.Month } into g
                           
                           select new
                           {
                               key = FormatarMesAno(g.Key.Year, g.Key.Month),
                               value = g.Count(),    
                           }).ToList();

            return Json(imoveis);
        }

        [HttpPost]
        public JsonResult GetTipos([FromBody] FiltroPesquisa filtro)
        {
            peximContext dbt = new peximContext();
            var siglaEstado = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var mes = Convert.ToInt32(filtro.DtReferencia.Split("/")[0]);
            var ano = Convert.ToInt32(filtro.DtReferencia.Split("/")[1]);
            var dataBase = new DateTime(ano, mes, 1);

            var imoveis = (from i in dbt.Imoveisclassificados
                           where i.Tipo != null
                            && ((filtro.Cidades == null || filtro.Cidades.Length == 0) || filtro.Cidades.Contains(i.Cidade))
                            && ((filtro.Bairros == null || filtro.Bairros.Length == 0) || filtro.Bairros.Contains(i.Bairro))
                            && ((filtro.Tipos == null || filtro.Tipos.Length == 0) || filtro.Tipos.Contains(i.Tipo))
                            && i.TipoImovel == filtro.TipoImovel
                            && i.Excluido == 0
                            && i.SiglaEstado == siglaEstado
                            && i.DataClassificacao == (dataBase)
                           group i by new { i.Tipo } into g

                           select new
                           {
                               key = g.Key.Tipo,
                               value = g.Count(),
                           }).ToList().OrderByDescending(x=>x.value);

            return Json(imoveis);
        }


        [HttpPost]
        public JsonResult GetCidades([FromBody] FiltroPesquisa filtro)
        {
            var siglaEstado = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var mes = Convert.ToInt32(filtro.DtReferencia.Split("/")[0]);
            var ano = Convert.ToInt32(filtro.DtReferencia.Split("/")[1]);
            var dataBase = new DateTime(ano, mes, 1);

            peximContext dbc = new peximContext();
            var imoveis = (from i in dbc.Imoveisclassificados
                           where i.Tipo != null
                            && ((filtro.Cidades == null || filtro.Cidades.Length == 0) || filtro.Cidades.Contains(i.Cidade))
                            && ((filtro.Bairros == null || filtro.Bairros.Length == 0) || filtro.Bairros.Contains(i.Bairro))
                            && ((filtro.Tipos == null || filtro.Tipos.Length == 0) || filtro.Tipos.Contains(i.Tipo))
                            && i.TipoImovel == filtro.TipoImovel
                            && i.SiglaEstado == siglaEstado
                            && i.Excluido == 0
                            && i.DataClassificacao == (dataBase)
                           group i by new { i.Cidade } into g

                           select new
                           {
                               key = g.Key.Cidade,
                               value = g.Count(),
                           }).ToList().OrderByDescending(x=>x.value);

            return Json(imoveis);
        }

        [HttpPost]
        public JsonResult GetBairros([FromBody] FiltroPesquisa filtro)
        {
            var siglaEstado = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var mes = Convert.ToInt32(filtro.DtReferencia.Split("/")[0]);
            var ano = Convert.ToInt32(filtro.DtReferencia.Split("/")[1]);
            var dataBase = new DateTime(ano, mes, 1);

            peximContext dbc = new peximContext();
            var imoveis = (from i in dbc.Imoveisclassificados
                           where i.Tipo != null
                            && ((filtro.Cidades == null || filtro.Cidades.Length == 0) || filtro.Cidades.Contains(i.Cidade))
                            && ((filtro.Bairros == null || filtro.Bairros.Length == 0) || filtro.Bairros.Contains(i.Bairro))
                            && ((filtro.Tipos == null || filtro.Tipos.Length == 0) || filtro.Tipos.Contains(i.Tipo))
                            && i.TipoImovel == filtro.TipoImovel
                            && i.SiglaEstado == siglaEstado
                            && i.Excluido == 0
                            && i.DataClassificacao == (dataBase)
                           group i by new { i.Bairro } into g

                           select new
                           {
                               key = g.Key.Bairro,
                               value = g.Count(),
                           }).ToList().OrderByDescending(x => x.value);

            return Json(imoveis);
        }

        [HttpPost]
        public JsonResult GetGaragens([FromBody] FiltroPesquisa filtro)
        {
            var siglaEstado = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var mes = Convert.ToInt32(filtro.DtReferencia.Split("/")[0]);
            var ano = Convert.ToInt32(filtro.DtReferencia.Split("/")[1]);
            var dataBase = new DateTime(ano, mes, 1);

            peximContext dbc = new peximContext();
            var imoveis = (from i in dbc.Imoveisclassificados
                           where i.Tipo != null
                            && ((filtro.Cidades == null || filtro.Cidades.Length == 0) || filtro.Cidades.Contains(i.Cidade))
                            && ((filtro.Bairros == null || filtro.Bairros.Length == 0) || filtro.Bairros.Contains(i.Bairro))
                            && ((filtro.Tipos == null || filtro.Tipos.Length == 0) || filtro.Tipos.Contains(i.Tipo))
                            && i.TipoImovel == filtro.TipoImovel
                            && i.SiglaEstado == siglaEstado
                            && i.Excluido == 0
                            && i.DataClassificacao == (dataBase)
                           group i by new { i.Garagens } into g
                           select new
                           {
                               key = FormatarGaragem(g.Key.Garagens),
                               value = g.Count(),
                           }).ToList();

            return Json(imoveis);
        }

        static string FormatarGaragem(int? garagens)
        {
            string garagem = "";

            switch (garagens)
            {
                case null:
                case 0:
                    garagem = "NI/NP";
                    break;
                case 1:
                    garagem = "1 GARAGEM";
                    break;
                case 2:
                    garagem = "2 GARAGENS";
                    break;
            }

            if(garagens > 2)
                    garagem = ">2 GARAGENS";
        

            return garagem;
        }

        static string FormatarMesAno(int ano,  int mes) 
        {
            string mesNumero = "";

               switch (mes)
                {
                    case 1:
                        mesNumero = "jan";
                        break;
                    case 2:
                        mesNumero = "fev";
                        break;
                    case 3:
                        mesNumero = "mar";
                        break;
                    case 4:
                        mesNumero = "abr";
                        break;
                    case 5:
                        mesNumero = "mai";
                        break;
                    case 6:
                        mesNumero = "jun";
                        break;
                    case 7:
                        mesNumero = "jul";
                        break;
                    case 8:
                        mesNumero = "ago";
                        break;
                    case 9:
                        mesNumero = "set";
                        break;
                    case 10:
                        mesNumero = "out";
                        break;
                    case 11:
                        mesNumero = "nov";
                        break;
                    case 12:
                        mesNumero = "dez";
                        break;
                }

            return string.Format("{0}/{1}", mesNumero, ano.ToString());
        }

        [HttpPost]
        public JsonResult Cidades()
        {
            var siglaEstado = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var imoveis = (from i in db.Imoveisclassificados
                          where i.Cidade!=null && i.SiglaEstado == siglaEstado
                           group i by new { i.Cidade } into g
                           select new
                           {
                               tipo = g.Key.Cidade,
                               valor = g.Count()
                           });

            return Json(imoveis);
        }

        [HttpPost]
        public JsonResult Bairros([FromBody]  FiltroPesquisa filtro)
        {
            var siglaEstado = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var imoveis = (from i in db.Imoveisclassificados
                           where filtro.Cidades.Contains(i.Cidade) && i.SiglaEstado == siglaEstado
                           group i by new { i.Bairro } into g
                           select new
                           {
                               tipo = g.Key.Bairro,
                               valor = g.Count()
                           }).ToList();

            return Json(imoveis);
        }


    }
}
