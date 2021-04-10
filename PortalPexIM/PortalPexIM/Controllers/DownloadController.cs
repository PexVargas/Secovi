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
    public class DownloadController : Controller
    {
        peximContext db = new peximContext();
        public IActionResult Index()
        {
            //var result = (from i in db.Estados
            //             select new FiltroEstados
            //             {
            //                 CodEstado = i.CodEstado,
            //                 NomeEstado = i.NomeEstado,
            //             }).ToList();


            return View();
        }
    }
}
