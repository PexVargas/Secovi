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
            TempData["DiffUrl"] = "";
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
            var dataCalculo = new DateTime(ano, mes, 1);
            var lstImoveis = new List<Imovel>();

            //var imoveis = (from i in dbe.Imoveisclassificados
            //               where i.Tipo!=null 
            //               && ((filtro.Cidades == null || filtro.Cidades.Length == 0)|| filtro.Cidades.Contains(i.Cidade)) 
            //               && ((filtro.Bairros == null || filtro.Bairros.Length == 0)|| filtro.Bairros.Contains(i.Bairro))
            //               && ((filtro.Tipos == null || filtro.Tipos.Length == 0) ||filtro.Tipos.Contains(i.Tipo))
            //               && i.TipoImovel == filtro.TipoImovel
            //               && i.Excluido == 0
            //               && i.SiglaEstado == siglaEstado
            //               && i.DataClassificacao >= (dataBase)
            //               group i by new { i.DataClassificacao.Value.Year, i.DataClassificacao.Value.Month } into g

            //               select new
            //               {
            //                   key = FormatarMesAno(g.Key.Year, g.Key.Month),
            //                   value = g.Count(),    
            //               }).ToList();

            if (filtro.Unidade == "quantidade")
            {
                lstImoveis = (from i in dbe.Imoveisclassificados
                              where i.Tipo != null
                               && ((filtro.Cidades == null || filtro.Cidades.Length == 0) || filtro.Cidades.Contains(i.Cidade))
                               && ((filtro.Bairros == null || filtro.Bairros.Length == 0) || filtro.Bairros.Contains(i.Bairro))
                               && ((filtro.Tipos == null || filtro.Tipos.Length == 0) || filtro.Tipos.Contains(i.Tipo))
                               && i.TipoImovel == filtro.TipoImovel
                               && i.Excluido == 0
                               && i.SiglaEstado == siglaEstado
                               && (i.DataClassificacao >= (dataBase) && i.DataClassificacao <= (dataCalculo))
                               
                              select new Imovel
                              {
                                  Data = i.DataClassificacao,
                                  Valor = 1,
                                  ValorTotal = (i.Valor == null ? 0 : i.Valor),
                                  AreaPrivativa = i.AreaPrivativa,
                                  AreaTotal = i.AreaTotal,
                              }).OrderBy(x=>x.Data).ToList();
            }
            else if (filtro.Unidade == "valorMedio")
            {
                lstImoveis = (from i in dbe.Imoveisclassificados
                              where i.Tipo != null
                               && ((filtro.Cidades == null || filtro.Cidades.Length == 0) || filtro.Cidades.Contains(i.Cidade))
                               && ((filtro.Bairros == null || filtro.Bairros.Length == 0) || filtro.Bairros.Contains(i.Bairro))
                               && ((filtro.Tipos == null || filtro.Tipos.Length == 0) || filtro.Tipos.Contains(i.Tipo))
                               && i.TipoImovel == filtro.TipoImovel
                               && i.Excluido == 0
                               && i.SiglaEstado == siglaEstado
                               && (i.DataClassificacao >= (dataBase) && i.DataClassificacao <= (dataCalculo))
                               && i.Valor > 0 && i.Valor < 30000000000000
                              select new Imovel
                              {
                                  Data = i.DataClassificacao,
                                  Valor = i.Valor,
                                  ValorTotal = (i.Valor == null ? 0 : i.Valor),
                                  AreaPrivativa = i.AreaPrivativa,
                                  AreaTotal = i.AreaTotal,
                              }).ToList();
            }
            else if (filtro.Unidade == "valorMedioMetros")
            {
                lstImoveis = (from i in dbe.Imoveisclassificados
                              where i.Tipo != null
                               && ((filtro.Cidades == null || filtro.Cidades.Length == 0) || filtro.Cidades.Contains(i.Cidade))
                               && ((filtro.Bairros == null || filtro.Bairros.Length == 0) || filtro.Bairros.Contains(i.Bairro))
                               && ((filtro.Tipos == null || filtro.Tipos.Length == 0) || filtro.Tipos.Contains(i.Tipo))
                               && i.TipoImovel == filtro.TipoImovel
                               && i.Excluido == 0
                               && i.SiglaEstado == siglaEstado
                               && (i.DataClassificacao >= (dataBase) && i.DataClassificacao <= (dataCalculo))
                               && i.Valor > 0 && i.Valor < 30000000000000
                               && ((filtro.TipoArea == 1 && i.AreaTotal != null && i.AreaTotal > 0 && i.AreaTotal < 1000000000)
                               || (filtro.TipoArea == 2 && i.AreaPrivativa != null && i.AreaPrivativa > 0 && i.AreaPrivativa < 1000000000))
                              select new Imovel
                              {
                                  Data = i.DataClassificacao,
                                  Valor = (filtro.TipoArea == 1 ? (i.Valor / i.AreaTotal) : (i.Valor / i.AreaPrivativa)),
                                  ValorTotal = (i.Valor == null ? 0 : i.Valor),
                                  AreaPrivativa = i.AreaPrivativa,
                                  AreaTotal = i.AreaTotal,
                              }).ToList();
            }

            var evolutivo = BuscarPainel(lstImoveis, filtro.Unidade, filtro.TipoArea);
            var imoveis = (from i in evolutivo
                           select new
                           {
                               key = i.Chave,
                               value = i.Valor,
                               metragemMedia = i.Metragem,
                               quantidadeOfertas = i.Quantidade
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
            var lstImoveis = new List<Imovel>();

            if (filtro.Unidade == "quantidade") 
            {
                lstImoveis = (from i in dbt.Imoveisclassificados
                              where i.Tipo != null
                               && ((filtro.Cidades == null || filtro.Cidades.Length == 0) || filtro.Cidades.Contains(i.Cidade))
                               && ((filtro.Bairros == null || filtro.Bairros.Length == 0) || filtro.Bairros.Contains(i.Bairro))
                               && ((filtro.Tipos == null || filtro.Tipos.Length == 0) || filtro.Tipos.Contains(i.Tipo))
                               && i.TipoImovel == filtro.TipoImovel
                               && i.Excluido == 0
                               && i.SiglaEstado == siglaEstado
                               && i.DataClassificacao == (dataBase)
                              select new Imovel
                              {
                                  Tipo = i.Tipo,
                                  Valor = 1,
                                  ValorTotal = (i.Valor == null ? 0 : i.Valor),
                                  AreaPrivativa = i.AreaPrivativa,
                                  AreaTotal = i.AreaTotal,
                              }).OrderBy(x => x.Valor).ToList();
            }
            else if (filtro.Unidade == "valorMedio") 
            {
                lstImoveis = (from i in dbt.Imoveisclassificados
                              where i.Tipo != null
                               && ((filtro.Cidades == null || filtro.Cidades.Length == 0) || filtro.Cidades.Contains(i.Cidade))
                               && ((filtro.Bairros == null || filtro.Bairros.Length == 0) || filtro.Bairros.Contains(i.Bairro))
                               && ((filtro.Tipos == null || filtro.Tipos.Length == 0) || filtro.Tipos.Contains(i.Tipo))
                               && i.TipoImovel == filtro.TipoImovel
                               && i.Excluido == 0
                               && i.SiglaEstado == siglaEstado
                               && i.DataClassificacao == (dataBase)
                               && i.Valor > 0 && i.Valor < 30000000000000
                              select new Imovel
                              {
                                  Tipo = i.Tipo,
                                  Valor = i.Valor,
                                  ValorTotal = (i.Valor == null ? 0 : i.Valor),
                                  AreaPrivativa = i.AreaPrivativa,
                                  AreaTotal = i.AreaTotal,
                              }).OrderBy(x => x.Valor).ToList();
            }
            else if (filtro.Unidade == "valorMedioMetros")
            {
                lstImoveis = (from i in dbt.Imoveisclassificados
                              where i.Tipo != null
                               && ((filtro.Cidades == null || filtro.Cidades.Length == 0) || filtro.Cidades.Contains(i.Cidade))
                               && ((filtro.Bairros == null || filtro.Bairros.Length == 0) || filtro.Bairros.Contains(i.Bairro))
                               && ((filtro.Tipos == null || filtro.Tipos.Length == 0) || filtro.Tipos.Contains(i.Tipo))
                               && i.TipoImovel == filtro.TipoImovel
                               && i.Excluido == 0
                               && i.SiglaEstado == siglaEstado
                               && i.DataClassificacao == (dataBase)
                                && i.Valor > 0 && i.Valor < 30000000000000
                               && ((filtro.TipoArea == 1 && i.AreaTotal != null && i.AreaTotal > 0 && i.AreaTotal < 1000000000)
                               || (filtro.TipoArea == 2 && i.AreaPrivativa != null && i.AreaPrivativa > 0 && i.AreaPrivativa < 1000000000))
                              select new Imovel
                              {
                                  Tipo = i.Tipo,
                                  Valor = (filtro.TipoArea == 1 ? (i.Valor / i.AreaTotal) : (i.Valor / i.AreaPrivativa)),
                                  ValorTotal = (i.Valor == null ? 0 : i.Valor),
                                  AreaPrivativa = i.AreaPrivativa,
                                  AreaTotal = i.AreaTotal,
                              }).OrderBy(x => x.Valor).ToList();
            }




            var Tipos = BuscarTipos(lstImoveis,filtro.Unidade, filtro.TipoArea );

            var imoveis = (from i in Tipos
                           select new
                           {
                               key = i.Chave,
                               value = i.Valor,
                           }).ToList().OrderByDescending(x => x.value);

            return Json(imoveis);
        }

        [HttpPost]
        public JsonResult GetCidades([FromBody] FiltroPesquisa filtro)
        {
            peximContext dbt = new peximContext();
            var siglaEstado = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var mes = Convert.ToInt32(filtro.DtReferencia.Split("/")[0]);
            var ano = Convert.ToInt32(filtro.DtReferencia.Split("/")[1]);
            var dataBase = new DateTime(ano, mes, 1);
            var lstImoveis = new List<Imovel>();

            if (filtro.Unidade == "quantidade")
            {
                lstImoveis = (from i in dbt.Imoveisclassificados
                              where i.Tipo != null
                               && ((filtro.Cidades == null || filtro.Cidades.Length == 0) || filtro.Cidades.Contains(i.Cidade))
                               && ((filtro.Bairros == null || filtro.Bairros.Length == 0) || filtro.Bairros.Contains(i.Bairro))
                               && ((filtro.Tipos == null || filtro.Tipos.Length == 0) || filtro.Tipos.Contains(i.Tipo))
                               && i.TipoImovel == filtro.TipoImovel
                               && i.Excluido == 0
                               && i.SiglaEstado == siglaEstado
                               && i.DataClassificacao == (dataBase)
                              select new Imovel
                              {
                                  Cidade = i.Cidade,
                                  Valor = 1,
                                  ValorTotal = (i.Valor == null ? 0 : i.Valor),
                                  AreaPrivativa = i.AreaPrivativa,
                                  AreaTotal = i.AreaTotal,
                              }).OrderBy(x => x.Valor).ToList();
            }
            else if (filtro.Unidade == "valorMedio")
            {
                lstImoveis = (from i in dbt.Imoveisclassificados
                              where i.Tipo != null
                               && ((filtro.Cidades == null || filtro.Cidades.Length == 0) || filtro.Cidades.Contains(i.Cidade))
                               && ((filtro.Bairros == null || filtro.Bairros.Length == 0) || filtro.Bairros.Contains(i.Bairro))
                               && ((filtro.Tipos == null || filtro.Tipos.Length == 0) || filtro.Tipos.Contains(i.Tipo))
                               && i.TipoImovel == filtro.TipoImovel
                               && i.Excluido == 0
                               && i.SiglaEstado == siglaEstado
                               && i.DataClassificacao == (dataBase)
                               && i.Valor > 0 && i.Valor < 30000000000000
                              select new Imovel
                              {
                                  Cidade = i.Cidade,
                                  Valor = i.Valor,
                                  ValorTotal = (i.Valor == null ? 0 : i.Valor),
                                  AreaPrivativa = i.AreaPrivativa,
                                  AreaTotal = i.AreaTotal,
                              }).OrderBy(x => x.Valor).ToList();
            }
            else if (filtro.Unidade == "valorMedioMetros")
            {
                lstImoveis = (from i in dbt.Imoveisclassificados
                              where i.Tipo != null
                               && ((filtro.Cidades == null || filtro.Cidades.Length == 0) || filtro.Cidades.Contains(i.Cidade))
                               && ((filtro.Bairros == null || filtro.Bairros.Length == 0) || filtro.Bairros.Contains(i.Bairro))
                               && ((filtro.Tipos == null || filtro.Tipos.Length == 0) || filtro.Tipos.Contains(i.Tipo))
                               && i.TipoImovel == filtro.TipoImovel
                               && i.Excluido == 0
                               && i.SiglaEstado == siglaEstado
                               && i.DataClassificacao == (dataBase)
                                && i.Valor > 0 && i.Valor < 30000000000000
                               && ((filtro.TipoArea == 1 && i.AreaTotal != null && i.AreaTotal > 0 && i.AreaTotal < 1000000000)
                               || (filtro.TipoArea == 2 && i.AreaPrivativa != null && i.AreaPrivativa > 0 && i.AreaPrivativa < 1000000000))
                              select new Imovel
                              {
                                  Cidade = i.Cidade,
                                  Valor = (filtro.TipoArea == 1 ? (i.Valor / i.AreaTotal) : (i.Valor / i.AreaPrivativa)),
                                  ValorTotal = (i.Valor == null ? 0 : i.Valor),
                                  AreaPrivativa = i.AreaPrivativa,
                                  AreaTotal = i.AreaTotal,
                              }).OrderBy(x => x.Valor).ToList();
            }




            var Cidades = BuscarCidades(lstImoveis, filtro.Unidade, filtro.TipoArea);

            var imoveis = (from i in Cidades
                           select new
                           {
                               key = i.Chave,
                               value = i.Valor,
                           }).ToList().OrderByDescending(x => x.value);

            return Json(imoveis);
        }


        public JsonResult GetBairros([FromBody] FiltroPesquisa filtro)
        {
            peximContext dbt = new peximContext();
            var siglaEstado = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var mes = Convert.ToInt32(filtro.DtReferencia.Split("/")[0]);
            var ano = Convert.ToInt32(filtro.DtReferencia.Split("/")[1]);
            var dataBase = new DateTime(ano, mes, 1);
            var lstImoveis = new List<Imovel>();

            if (filtro.Unidade == "quantidade")
            {
                lstImoveis = (from i in dbt.Imoveisclassificados
                              where i.Tipo != null
                               && ((filtro.Cidades == null || filtro.Cidades.Length == 0) || filtro.Cidades.Contains(i.Cidade))
                               && ((filtro.Bairros == null || filtro.Bairros.Length == 0) || filtro.Bairros.Contains(i.Bairro))
                               && ((filtro.Tipos == null || filtro.Tipos.Length == 0) || filtro.Tipos.Contains(i.Tipo))
                               && i.TipoImovel == filtro.TipoImovel
                               && i.Excluido == 0
                               && i.SiglaEstado == siglaEstado
                               && i.DataClassificacao == (dataBase)
                              select new Imovel
                              {
                                  Bairro = i.Bairro,
                                  Valor = 1,
                                  ValorTotal = (i.Valor == null ? 0 : i.Valor),
                                  AreaPrivativa = i.AreaPrivativa,
                                  AreaTotal = i.AreaTotal,
                              }).OrderBy(x => x.Valor).ToList();
            }
            else if (filtro.Unidade == "valorMedio")
            {
                lstImoveis = (from i in dbt.Imoveisclassificados
                              where i.Tipo != null
                               && ((filtro.Cidades == null || filtro.Cidades.Length == 0) || filtro.Cidades.Contains(i.Cidade))
                               && ((filtro.Bairros == null || filtro.Bairros.Length == 0) || filtro.Bairros.Contains(i.Bairro))
                               && ((filtro.Tipos == null || filtro.Tipos.Length == 0) || filtro.Tipos.Contains(i.Tipo))
                               && i.TipoImovel == filtro.TipoImovel
                               && i.Excluido == 0
                               && i.SiglaEstado == siglaEstado
                               && i.DataClassificacao == (dataBase)
                               && i.Valor > 0 && i.Valor < 30000000000000
                              select new Imovel
                              {
                                  Bairro = i.Bairro,
                                  Valor = i.Valor,
                                  ValorTotal = (i.Valor == null ? 0 : i.Valor),
                                  AreaPrivativa = i.AreaPrivativa,
                                  AreaTotal = i.AreaTotal,
                              }).OrderBy(x => x.Valor).ToList();
            }
            else if (filtro.Unidade == "valorMedioMetros")
            {
                lstImoveis = (from i in dbt.Imoveisclassificados
                              where i.Tipo != null
                               && ((filtro.Cidades == null || filtro.Cidades.Length == 0) || filtro.Cidades.Contains(i.Cidade))
                               && ((filtro.Bairros == null || filtro.Bairros.Length == 0) || filtro.Bairros.Contains(i.Bairro))
                               && ((filtro.Tipos == null || filtro.Tipos.Length == 0) || filtro.Tipos.Contains(i.Tipo))
                               && i.TipoImovel == filtro.TipoImovel
                               && i.Excluido == 0
                               && i.SiglaEstado == siglaEstado
                               && i.DataClassificacao == (dataBase)
                                && i.Valor > 0 && i.Valor < 30000000000000
                               && ((filtro.TipoArea == 1 && i.AreaTotal != null && i.AreaTotal > 0 && i.AreaTotal < 1000000000)
                               || (filtro.TipoArea == 2 && i.AreaPrivativa != null && i.AreaPrivativa > 0 && i.AreaPrivativa < 1000000000))
                              select new Imovel
                              {
                                  Bairro = i.Bairro,
                                  Valor = (filtro.TipoArea == 1 ? (i.Valor / i.AreaTotal) : (i.Valor / i.AreaPrivativa)),
                                  ValorTotal = (i.Valor == null ? 0 : i.Valor),
                                  AreaPrivativa = i.AreaPrivativa,
                                  AreaTotal = i.AreaTotal,
                              }).OrderBy(x => x.Valor).ToList();
            }




            var Bairros = BuscarBairros(lstImoveis, filtro.Unidade, filtro.TipoArea);

            var imoveis = (from i in Bairros
                           select new
                           {
                               key = i.Chave,
                               value = i.Valor,
                           }).ToList().OrderByDescending(x => x.value);

            return Json(imoveis);
        }

        public JsonResult GetGaragens([FromBody] FiltroPesquisa filtro)
        {
            peximContext dbt = new peximContext();
            var siglaEstado = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var mes = Convert.ToInt32(filtro.DtReferencia.Split("/")[0]);
            var ano = Convert.ToInt32(filtro.DtReferencia.Split("/")[1]);
            var dataBase = new DateTime(ano, mes, 1);
            var lstImoveis = new List<Imovel>();

            if (filtro.Unidade == "quantidade")
            {
                lstImoveis = (from i in dbt.Imoveisclassificados
                              where i.Tipo != null
                               && ((filtro.Cidades == null || filtro.Cidades.Length == 0) || filtro.Cidades.Contains(i.Cidade))
                               && ((filtro.Bairros == null || filtro.Bairros.Length == 0) || filtro.Bairros.Contains(i.Bairro))
                               && ((filtro.Tipos == null || filtro.Tipos.Length == 0) || filtro.Tipos.Contains(i.Tipo))
                               && i.TipoImovel == filtro.TipoImovel
                               && i.Excluido == 0
                               && i.SiglaEstado == siglaEstado
                               && i.DataClassificacao == (dataBase)
                              select new Imovel
                              {
                                  Garagem = i.Garagens,
                                  Valor = 1,
                                  ValorTotal = (i.Valor == null ? 0 : i.Valor),
                                  AreaPrivativa = i.AreaPrivativa,
                                  AreaTotal = i.AreaTotal,
                              }).OrderBy(x => x.Valor).ToList();
            }
            else if (filtro.Unidade == "valorMedio")
            {
                lstImoveis = (from i in dbt.Imoveisclassificados
                              where i.Tipo != null
                               && ((filtro.Cidades == null || filtro.Cidades.Length == 0) || filtro.Cidades.Contains(i.Cidade))
                               && ((filtro.Bairros == null || filtro.Bairros.Length == 0) || filtro.Bairros.Contains(i.Bairro))
                               && ((filtro.Tipos == null || filtro.Tipos.Length == 0) || filtro.Tipos.Contains(i.Tipo))
                               && i.TipoImovel == filtro.TipoImovel
                               && i.Excluido == 0
                               && i.SiglaEstado == siglaEstado
                               && i.DataClassificacao == (dataBase)
                               && i.Valor > 0 && i.Valor < 30000000000000
                              select new Imovel
                              {
                                  Garagem = i.Garagens,
                                  Valor = i.Valor,
                                  ValorTotal = (i.Valor == null ? 0 : i.Valor),
                                  AreaPrivativa = i.AreaPrivativa,
                                  AreaTotal = i.AreaTotal,
                              }).OrderBy(x => x.Valor).ToList();
            }
            else if (filtro.Unidade == "valorMedioMetros")
            {
                lstImoveis = (from i in dbt.Imoveisclassificados
                              where i.Tipo != null
                               && ((filtro.Cidades == null || filtro.Cidades.Length == 0) || filtro.Cidades.Contains(i.Cidade))
                               && ((filtro.Bairros == null || filtro.Bairros.Length == 0) || filtro.Bairros.Contains(i.Bairro))
                               && ((filtro.Tipos == null || filtro.Tipos.Length == 0) || filtro.Tipos.Contains(i.Tipo))
                               && i.TipoImovel == filtro.TipoImovel
                               && i.Excluido == 0
                               && i.SiglaEstado == siglaEstado
                               && i.DataClassificacao == (dataBase)
                                && i.Valor > 0 && i.Valor < 30000000000000
                               && ((filtro.TipoArea == 1 && i.AreaTotal != null && i.AreaTotal > 0 && i.AreaTotal < 1000000000)
                               || (filtro.TipoArea == 2 && i.AreaPrivativa != null && i.AreaPrivativa > 0 && i.AreaPrivativa < 1000000000))
                              select new Imovel
                              {
                                  Garagem = i.Garagens,
                                  Valor = (filtro.TipoArea == 1 ? (i.Valor / i.AreaTotal) : (i.Valor / i.AreaPrivativa)),
                                  ValorTotal = (i.Valor == null ? 0 : i.Valor),
                                  AreaPrivativa = i.AreaPrivativa,
                                  AreaTotal = i.AreaTotal,
                              }).OrderBy(x => x.Valor).ToList();
            }




            var garagens = BuscarGaragens(lstImoveis, filtro.Unidade, filtro.TipoArea);

            var imoveis = (from i in garagens
                           select new
                           {
                               key = FormatarGaragem(Convert.ToInt32(i.Chave)),
                               value = i.Valor,
                           }).ToList().OrderByDescending(x => x.value);

            return Json(imoveis);
        }

        public List<BaseDados> BuscarPainel(List<Imovel> imoveis,  string unidade, int? tipoArea)
        {
            if (unidade != "valorMedioMetros")
                tipoArea = 2;

            List<BaseDados> tiposResult = new List<BaseDados>();

            var dadosAgrupados = (from i in imoveis
                                  select new
                                  {
                                      Chave = i.Data,
                                      Valor = i.Valor,
                                      Area = (tipoArea == 1 ? i.AreaTotal : i.AreaPrivativa),
                                  }).GroupBy(x => x.Chave).ToList();

            foreach (var dados in dadosAgrupados)
            {
                if (dados.Key != null)
                {
                    BaseDados baseDados = new BaseDados();

                    var valores = (from i in imoveis
                                   where i.Data == dados.Key
                                
                                   select new ImovelOutlier()
                                   {
                                       valor = Convert.ToDouble(i.Valor),
                                       Area = (tipoArea == 1 ? i.AreaTotal : i.AreaPrivativa),
                                   }).ToList();


                    var dadosGrupo = dados.FirstOrDefault();
                    baseDados.Chave = FormatarMesAno(dadosGrupo.Chave.Value.Year, dadosGrupo.Chave.Value.Month);//dadosGrupo.Chave;

                    if (unidade == "quantidade")
                    {
                        var valor = valores.Count;
                        baseDados.Valor = valor;
                        baseDados.Quantidade = valor;
                        baseDados.Metragem = valores.Where(x => x.Area > 0).Average(x => x.Area);
                    }
                    else
                    {
                        var grupo = AplicarOutLier(valores);
                        baseDados.Valor = Convert.ToDecimal(grupo.Valor);
                        baseDados.Quantidade = grupo.Quantidade;
                        baseDados.Minimo = grupo.Minimo;
                        baseDados.Maximo = grupo.Maximo;
                        baseDados.Metragem = grupo.Metragem;
                    }

                    tiposResult.Add(baseDados);
                }
            }

            //if (unidade == "quantidade")
            //    return tiposResult.OrderByDescending(x => x.Quantidade).Where(x => x.Quantidade > 0).ToList();
            //else
            //    return tiposResult.OrderByDescending(x => x.Valor).Where(x => x.Quantidade > 0).ToList();

            if (unidade == "quantidade")
                return tiposResult.Where(x => x.Quantidade > 0).ToList();
            else
                return tiposResult.Where(x => x.Quantidade > 0).ToList();
        }

        public List<BaseDados> BuscarTipos(List<Imovel> imoveis, string unidade, int? tipoArea)
        {
            if (unidade != "valorMedioMetros")
                tipoArea = 2;

            List<BaseDados> tiposResult = new List<BaseDados>();

            var dadosAgrupados = (from i in imoveis
                                  select new
                                  {
                                      Chave = i.Tipo,
                                      Valor = i.Valor,
                                      Area = (tipoArea == 1 ? i.AreaTotal : i.AreaPrivativa),
                                  }).GroupBy(x => x.Chave).ToList();

            foreach (var dados in dadosAgrupados)
            {
                if (dados.Key != null)
                {
                    BaseDados baseDados = new BaseDados();

                    var valores = (from i in imoveis
                                   where i.Tipo == dados.Key

                                   select new ImovelOutlier()
                                   {
                                       valor = Convert.ToDouble(i.Valor),
                                       Area = (tipoArea == 1 ? i.AreaTotal : i.AreaPrivativa),
                                   }).ToList();


                    var dadosGrupo = dados.FirstOrDefault();
                    baseDados.Chave = dadosGrupo.Chave;

                    if (unidade == "quantidade")
                    {
                        var valor = valores.Count;
                        baseDados.Valor = valor;
                        baseDados.Quantidade = valor;
                        baseDados.Metragem = valores.Where(x => x.Area > 0).Average(x => x.Area);
                    }
                    else
                    {
                        var grupo = AplicarOutLier(valores);
                        baseDados.Valor = Convert.ToDecimal(grupo.Valor);
                        baseDados.Quantidade = grupo.Quantidade;
                        baseDados.Minimo = grupo.Minimo;
                        baseDados.Maximo = grupo.Maximo;
                        baseDados.Metragem = grupo.Metragem;
                    }

                    tiposResult.Add(baseDados);
                }
            }

            if (unidade == "quantidade")
                return tiposResult.OrderByDescending(x => x.Quantidade).Where(x => x.Quantidade > 0).ToList();
            else
                return tiposResult.OrderByDescending(x => x.Valor).Where(x => x.Quantidade > 0).ToList();
        }
        public List<BaseDados> BuscarCidades(List<Imovel> imoveis, string unidade, int? tipoArea)
        {
            if (unidade != "valorMedioMetros")
                tipoArea = 2;

            List<BaseDados> tiposResult = new List<BaseDados>();

            var dadosAgrupados = (from i in imoveis
                                  select new
                                  {
                                      Chave = i.Cidade,
                                      Valor = i.Valor,
                                      Area = (tipoArea == 1 ? i.AreaTotal : i.AreaPrivativa),
                                  }).GroupBy(x => x.Chave).ToList();

            foreach (var dados in dadosAgrupados)
            {
                if (dados.Key != null)
                {
                    BaseDados baseDados = new BaseDados();

                    var valores = (from i in imoveis
                                   where i.Cidade == dados.Key

                                   select new ImovelOutlier()
                                   {
                                       valor = Convert.ToDouble(i.Valor),
                                       Area = (tipoArea == 1 ? i.AreaTotal : i.AreaPrivativa),
                                   }).ToList();


                    var dadosGrupo = dados.FirstOrDefault();
                    baseDados.Chave = dadosGrupo.Chave;

                    if (unidade == "quantidade")
                    {
                        var valor = valores.Count;
                        baseDados.Valor = valor;
                        baseDados.Quantidade = valor;
                        baseDados.Metragem = valores.Where(x => x.Area > 0).Average(x => x.Area);
                    }
                    else
                    {
                        var grupo = AplicarOutLier(valores);
                        baseDados.Valor = Convert.ToDecimal(grupo.Valor);
                        baseDados.Quantidade = grupo.Quantidade;
                        baseDados.Minimo = grupo.Minimo;
                        baseDados.Maximo = grupo.Maximo;
                        baseDados.Metragem = grupo.Metragem;
                    }

                    tiposResult.Add(baseDados);
                }
            }

            if (unidade == "quantidade")
                return tiposResult.OrderByDescending(x => x.Quantidade).Where(x => x.Quantidade > 0).ToList();
            else
                return tiposResult.OrderByDescending(x => x.Valor).Where(x => x.Quantidade > 0).ToList();
        }

        public List<BaseDados> BuscarBairros(List<Imovel> imoveis, string unidade, int? tipoArea)
        {
            if (unidade != "valorMedioMetros")
                tipoArea = 2;

            List<BaseDados> tiposResult = new List<BaseDados>();

            var dadosAgrupados = (from i in imoveis
                                  select new
                                  {
                                      Chave = i.Bairro,
                                      Valor = i.Valor,
                                      Area = (tipoArea == 1 ? i.AreaTotal : i.AreaPrivativa),
                                  }).GroupBy(x => x.Chave).ToList();

            foreach (var dados in dadosAgrupados)
            {
                if (dados.Key != null)
                {
                    BaseDados baseDados = new BaseDados();

                    var valores = (from i in imoveis
                                   where i.Bairro == dados.Key

                                   select new ImovelOutlier()
                                   {
                                       valor = Convert.ToDouble(i.Valor),
                                       Area = (tipoArea == 1 ? i.AreaTotal : i.AreaPrivativa),
                                   }).ToList();


                    var dadosGrupo = dados.FirstOrDefault();
                    baseDados.Chave = dadosGrupo.Chave;

                    if (unidade == "quantidade")
                    {
                        var valor = valores.Count;
                        baseDados.Valor = valor;
                        baseDados.Quantidade = valor;
                        baseDados.Metragem = valores.Where(x => x.Area > 0).Average(x => x.Area);
                    }
                    else
                    {
                        var grupo = AplicarOutLier(valores);
                        baseDados.Valor = Convert.ToDecimal(grupo.Valor);
                        baseDados.Quantidade = grupo.Quantidade;
                        baseDados.Minimo = grupo.Minimo;
                        baseDados.Maximo = grupo.Maximo;
                        baseDados.Metragem = grupo.Metragem;
                    }

                    tiposResult.Add(baseDados);
                }
            }

            if (unidade == "quantidade")
                return tiposResult.OrderByDescending(x => x.Quantidade).Where(x => x.Quantidade > 0).ToList();
            else
                return tiposResult.OrderByDescending(x => x.Valor).Where(x => x.Quantidade > 0).ToList();
        }
        public List<BaseDados> BuscarGaragens(List<Imovel> imoveis, string unidade, int? tipoArea)
        {
            if (unidade != "valorMedioMetros")
                tipoArea = 2;

            List<BaseDados> tiposResult = new List<BaseDados>();

            var dadosAgrupados = (from i in imoveis
                                  select new
                                  {
                                      Chave = i.Garagem,
                                      Valor = i.Valor,
                                      Area = (tipoArea == 1 ? i.AreaTotal : i.AreaPrivativa),
                                  }).GroupBy(x => x.Chave).ToList();

            foreach (var dados in dadosAgrupados)
            {
                if (dados.Key != null)
                {
                    BaseDados baseDados = new BaseDados();

                    var valores = (from i in imoveis
                                   where i.Garagem == dados.Key

                                   select new ImovelOutlier()
                                   {
                                       valor = Convert.ToDouble(i.Valor),
                                       Area = (tipoArea == 1 ? i.AreaTotal : i.AreaPrivativa),
                                   }).ToList();


                    var dadosGrupo = dados.FirstOrDefault();
                    baseDados.Chave = dadosGrupo.Chave.ToString();

                    if (unidade == "quantidade")
                    {
                        var valor = valores.Count;
                        baseDados.Valor = valor;
                        baseDados.Quantidade = valor;
                        baseDados.Metragem = valores.Where(x => x.Area > 0).Average(x => x.Area);
                    }
                    else
                    {
                        var grupo = AplicarOutLier(valores);
                        baseDados.Valor = Convert.ToDecimal(grupo.Valor);
                        baseDados.Quantidade = grupo.Quantidade;
                        baseDados.Minimo = grupo.Minimo;
                        baseDados.Maximo = grupo.Maximo;
                        baseDados.Metragem = grupo.Metragem;
                    }

                    tiposResult.Add(baseDados);
                }
            }

            if (unidade == "quantidade")
                return tiposResult.OrderByDescending(x => x.Quantidade).Where(x => x.Quantidade > 0).ToList();
            else
                return tiposResult.OrderByDescending(x => x.Valor).Where(x => x.Quantidade > 0).ToList();
        }


        public BaseDados AplicarOutLier(List<ImovelOutlier> imoveis)
        {
            var outlierGeral = false;

            BaseDados baseDados = new BaseDados();

            if (imoveis.Count > 9)
            {
                imoveis = imoveis.OrderBy(x => x.valor).ToList();
                var meioListagem = Convert.ToInt32((imoveis.Count / 2));

                for (int i = meioListagem; i >= 0; i--)
                {
                    if (imoveis[i].valor > 0)
                    {
                        double imovelSuperior = imoveis[i + 1].valor;//E4
                        var imovelInferior = imoveis[i].valor;//E3 aqui é o ultimo

                        if (imovelSuperior <= (imovelInferior + (imovelInferior * 0.50))) //verdadeiro
                            imoveis[i].outlier = outlierGeral;
                        else
                        {
                            outlierGeral = true;
                            imoveis[i].outlier = outlierGeral;
                        }
                    }
                    else
                        imoveis[i].outlier = true;
                }

                outlierGeral = false;

                ////varre do  meio  para o final
                for (int i = meioListagem + 1; i < imoveis.Count; i++)
                {
                    if (imoveis[i].valor > 0)
                    {
                        var imovelSuperior = imoveis[i].valor;//E4
                        var imovelAnterior = imoveis[i - 1].valor;//E3

                        imovelAnterior += (imovelAnterior * 0.50);

                        if (imovelAnterior >= imovelSuperior)
                            imoveis[i].outlier = outlierGeral;
                        else
                        {
                            outlierGeral = true;
                            imoveis[i].outlier = outlierGeral;
                        }
                    }
                    else
                        imoveis[i].outlier = true;
                }

                var contador = 0;
                double valorTotalPrimeiroCorte = 0;

                for (var i = 0; i < imoveis.Count; i++)
                {
                    if (imoveis[i].outlier == false)
                    {
                        valorTotalPrimeiroCorte += imoveis[i].valor;
                        contador++;
                    }
                }

                var MediaPrimerioCorte = (valorTotalPrimeiroCorte / contador);
                MediaPrimerioCorte = Math.Round(MediaPrimerioCorte, 4);
                var ValorCorteInicial = (MediaPrimerioCorte - (MediaPrimerioCorte * 0.40));
                var ValorCorteFinal = (MediaPrimerioCorte + (MediaPrimerioCorte * 0.40));

                //somente os que não são outliers
                for (int i = 0; i < imoveis.Count; i++)
                {
                    if (imoveis[i].outlier == false)
                    {
                        if (imoveis[i].valor < ValorCorteInicial)
                            imoveis[i].outlier = true;
                        else if (imoveis[i].valor > ValorCorteFinal)
                            imoveis[i].outlier = true;
                    }
                }

                var contadorSegundoCorte = 0;
                double valorTotalSegundoCorte = 0;

                for (var i = 0; i < imoveis.Count; i++)
                {
                    if (imoveis[i].outlier == false)
                    {
                        valorTotalSegundoCorte += imoveis[i].valor;
                        contadorSegundoCorte++;
                    }
                }

                var MediaValor = 0;

                if (valorTotalSegundoCorte == 0 && contadorSegundoCorte == 0) { }
                else
                {
                    baseDados.lstImovelOutlier = imoveis.Where(x => x.outlier == false).ToList();
                    baseDados.Quantidade = contadorSegundoCorte;
                    valorTotalSegundoCorte = Math.Round(valorTotalSegundoCorte, 4);

                    decimal valor = Convert.ToDecimal(valorTotalSegundoCorte / contadorSegundoCorte);

                    baseDados.Valor = Math.Round(valor, 2, MidpointRounding.AwayFromZero);

                    baseDados.Minimo = imoveis.Where(x => x.outlier == false).Min(x => x.valor);
                    baseDados.Maximo = imoveis.Where(x => x.outlier == false).Max(x => x.valor);
                    baseDados.Metragem = Convert.ToDecimal(imoveis.Where(x => x.outlier == false && x.Area > 0).Average(x => x.Area));
                    return baseDados; 
                }
       
            }
            else
            {
                if (imoveis.Count > 0)
                {
                    baseDados.Valor = Convert.ToDecimal(imoveis.Average(x => x.valor));
                    baseDados.Quantidade = imoveis.Count();

                    baseDados.Minimo = imoveis.Min(x => x.valor);
                    baseDados.Maximo = imoveis.Max(x => x.valor);
                    baseDados.Metragem = Convert.ToDecimal(imoveis.Where(x => x.Area > 0).Average(x => x.Area));
                }
                return baseDados;
            }

            return baseDados;//verificar melhor retorno
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
