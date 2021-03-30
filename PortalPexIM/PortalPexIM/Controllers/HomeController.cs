using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortalPexIM.Model;
using PortalPexIM.Models;
using PortalPexIM.ViewModel;

namespace PortalPexIM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        PexIMContext db = new PexIMContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            FiltroPesquisa filtro = new FiltroPesquisa();
            filtro.Cidades = db.ImoveisClassificados.Select(x => x.Cidade).Distinct().ToList();
            filtro.Bairro = db.ImoveisClassificados.Select(x => x.Bairro).Distinct().ToList();
            filtro.Tipo = db.ImoveisClassificados.Select(x => x.Tipo).Distinct().ToList();

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
        public JsonResult Evolutivo() 
        {
       
            var imoveis = (from i in db.ImoveisClassificados
                           where i.Tipo!=null
                           group i by new { i.Tipo } into g
                           select new
                           {
                               tipo = g.Key.Tipo,
                               valor = g.Count(),    
                           });

            return Json(imoveis);
        }

        [HttpPost]
        public JsonResult Cidades()
        {
          
            var imoveis = (from i in db.ImoveisClassificados
                          where i.Cidade!=null
                           group i by new { i.Cidade } into g
                           select new
                           {
                               tipo = g.Key.Cidade,
                               valor = g.Count()
                           });

            return Json(imoveis);
        }

        [HttpPost]
        public JsonResult Bairros()
        {
 
            var imoveis = (from i in db.ImoveisClassificados
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
