using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortalPexIM.Model;
using PortalPexIM.ViewModel;

namespace PortalPexIM.Controllers
{
    public class UploadController : Controller
    {
        PexIMContext db = new PexIMContext();
        public IActionResult Index()
        {
            var result = (from i in db.Estados
                          select new FiltroEstados
                          {
                              CodEstado = i.CodEstado,
                              NomeEstado = i.NomeEstado,
                          }).ToList();

            return View();
        }
    }
}
