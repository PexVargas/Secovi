using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalPexIM.Model;
using PortalPexIM.ViewModel;

namespace PortalPexIM.Controllers
{
    public class UploadController : Controller
    {

        public IActionResult Index()
        {
            //var result = (from i in db.Estados
            //              select new FiltroEstados
            //              {
            //                  CodEstado = i.CodEstado,
            //                  NomeEstado = i.NomeEstado,
            //              }).ToList();
            TempData["DiffUrl"] = "Upload";
            return View();
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        public ActionResult Importar(IFormFile arquivo, string periodo)
        {
            var mes = Convert.ToInt32( periodo.Split("/")[0]);
            var ano = Convert.ToInt32( periodo.Split("/")[1]);
            var siglaEstado = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var db1 = new peximContext();

            // using (var db1  = new peximContext())
            {
                var lstImobiliarias = db1.Imobiliarias.ToList();

                using (var reader = new StreamReader(arquivo.OpenReadStream()))
                {
                    bool header = true;
                    int linha = 0;

                    while (reader.Peek() >= 0)
                    {
                        var linhaArray = reader.ReadLine().Split(';');

                        if (!header)
                        {
                            try
                            {
                                Imoveisclassificados im = new Imoveisclassificados();
                                im.Tipo = linhaArray[0];
                                im.Cidade = linhaArray[1];
                                im.Bairro = linhaArray[2];

                                if (linhaArray[3] != "")
                                    im.Valor = Convert.ToDecimal(linhaArray[3].Replace(".", ""));

                                if (linhaArray[4] != "")
                                    im.AreaPrivativa = Convert.ToDecimal(linhaArray[4].Replace(".", ""));

                                if (linhaArray[5] != "")
                                    im.AreaTotal = Convert.ToDecimal(linhaArray[5].Replace(".", ""));

                                if (linhaArray[6] != "")
                                    im.Quartos = Convert.ToInt32(linhaArray[6]);

                                if (linhaArray[7] != "")
                                    im.Garagens = Convert.ToInt32(linhaArray[7]);

                                if (linhaArray[8] != "")
                                    im.Suites = Convert.ToInt32(linhaArray[8]);

                                im.Url = linhaArray[9];
                                im.Descricao = linhaArray[10];

                                var Imobiliaria = lstImobiliarias.Where(x => x.Nome == linhaArray[11]).FirstOrDefault();
                                if(Imobiliaria != null)
                                    im.CodImobiliaria = Imobiliaria.CodImobiliaria;

                                im.SiglaEstado = siglaEstado;
                                im.Finalidade = linhaArray[13];
                                im.TipoImovel = linhaArray[13].Trim().ToLower() == "venda" ? 1 : 2;
                                //im.Perfil = linhaArray[14];
                                im.Anunciante = linhaArray[15];
                                im.Localidade = linhaArray[16];
                                im.Iptu = linhaArray[17];
                                im.Apto = linhaArray[18];
                                im.Condominio = linhaArray[19];
                                // im.Dormitorios = linhaArray[20];
                                im.DataClassificacao = new DateTime(ano, mes, 1);
                                im.Excluido = 0;
                                db1.Imoveisclassificados.Add(im);
                               // db1.SaveChanges();
                            }
                            catch (Exception ex)
                            {

                               
                            }

                            
                        }

                        linha++;
                        header = false;

                    }
                }
            }

            db1.SaveChanges();
            return View("Index");
        }

    }
}
