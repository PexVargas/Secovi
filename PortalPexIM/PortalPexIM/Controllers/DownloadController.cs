using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortalPexIM.Model;
using PortalPexIM.Models;
using PortalPexIM.ViewModel;
using PortalPexIM.ViewModel.Download;

namespace PortalPexIM.Controllers
{
    public class DownloadController : Controller
    {
        peximContext db = new peximContext();
        public IActionResult Index()
        {


            TempData["DiffUrl"] = "Download";
            return View();
        }

        [HttpPost]
        public FileResult GerarCsv(string periodo, int tipoImovel)
        {
            var siglaEstado = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var mes = Convert.ToString(periodo.Split("/")[0]);
            var ano = Convert.ToInt32(periodo.Split("/")[1]);

            StringBuilder sb = new StringBuilder();

            try
            {
                List<ImoveisDados> ic = RetornaDadosClassificados(tipoImovel, siglaEstado, mes, ano);

                var lstImobiliarias = db.Imobiliarias.ToList();
                sb.AppendLine("Tipo;Cidade;Bairro;Valor;Area Privativa;Area Total;Quartos;Garagens;Suítes;Url;Descrição;Imobiliaria;Estado;Finalidade;Perfil;Anunciante;Endereco; Iptu; Apto;Condominio;Dormitorios");

                foreach (ImoveisDados line in ic)
                { 
                   sb.AppendLine(LinhaCSV(line, lstImobiliarias));
                }
            }
            catch (Exception ex)
            {
                
            }

            // Convert a string to utf-8 bytes.
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(sb.ToString());

            // Convert utf-8 bytes to a string.
            string s_unicode2 = Encoding.UTF8.GetString(utf8Bytes);
            string NomeArquivo = String.Format("{0}_{1}_{2}", tipoImovel, siglaEstado, (ano.ToString() + mes.ToString()));

            return File(Encoding.UTF8.GetBytes(s_unicode2), "text/csv", string.Format( "{0}.csv", NomeArquivo));

        }

        public List<ImoveisDados> RetornaDadosClassificados(int tipoImovel, string siglaEstado, string mes, int ano)
        {
            using (var dbx = new peximContext())
            {
               
                List<ImoveisDados> dados;

                var dataIni = new DateTime(ano, Convert.ToInt32(mes), 1);
                var dataFim = dataIni.AddMonths(1);

                
                        dados = (from ic in dbx.Imoveisclassificados
                                 where ic.SiglaEstado == siglaEstado
                                 && (tipoImovel == 0 || ic.TipoImovel == tipoImovel)
                                 && ic.DataClassificacao.Value >= dataIni
                                 && ic.DataClassificacao.Value < dataFim
                                 && ic.TipoImovel != 3
                                 && ic.Excluido == 0
                                 select new ImoveisDados()
                                 {
                                     Tipo = ic.Tipo,
                                     Cidade = ic.Cidade,
                                     Bairro = ic.Bairro,
                                     Valor = ic.Valor,
                                     AreaPrivativa = ic.AreaPrivativa,
                                     AreaTotal = ic.AreaTotal,
                                     Quartos = ic.Quartos,
                                     Suites = ic.Suites,
                                     Url = ic.Url,
                                     Descricao = ic.Descricao,
                                     CodImobiliaria = ic.CodImobiliaria,
                                     SiglaEstado = ic.SiglaEstado,
                                     TipoImovel = ic.TipoImovel,
                                    // Perfil = ic.Perfil,
                                     Anunciante = ic.Anunciante,
                                     Endereco = ic.Localidade,//revisar
                                     Iptu = ic.Iptu,
                                     Apto = ic.Apto,
                                     Garagens = ic.Garagens,
                                     Condominio = ic.Condominio,
                                     //Dormitorios = ic.Dormitorios
                                 }).ToList();

               
                return dados;
            }
        }

        public string LinhaCSV(ImoveisDados line, List<Imobiliarias> listImobiliarias)
        {
            string retorno = string.Empty;
            try
            {
                string descricao = string.Empty;

                retorno += line.Tipo + ";";
                retorno += line.Cidade + ";";
                retorno += line.Bairro + ";";

                retorno += line.Valor + ";";

                retorno += line.AreaPrivativa + ";";
                retorno += line.AreaTotal + ";";

                retorno += (line.Quartos == null ? 0 : line.Quartos) + ";";
                retorno += (line.Garagens == null ? 0 : line.Garagens) + ";";
                retorno += (line.Suites == null ? 0 : line.Suites) + ";";
                //retorno += "\"" + line.Url.Replace(";", ",") + "\"" + ";";
                retorno += line.Url.Replace(";", ",") + ";";

                if (line.Descricao == null)
                    line.Descricao = "";

                descricao = line.Descricao
                                .Replace(";", ",")
                                .Replace("\r\n", " ")
                                .Replace("\n\r", " ")
                                .Replace("\\r", " ")
                                .Replace("\\n", " ")
                                .Replace("\r", " ")
                                .Replace("\n", " ")
                                .Replace("\"", "")
                                .Replace(Environment.NewLine, " ");

          
                retorno += descricao + ";";

                var imobiliaria = listImobiliarias.Where(x => x.Id == line.CodImobiliaria).FirstOrDefault();

                if (imobiliaria != null)
                   retorno += imobiliaria.Nome + ";";
                else
                    retorno += "NA" + ";";

                retorno += line.SiglaEstado + ";";
                retorno += (line.TipoImovel == 1 ? "Venda" : "Locação") + ";";
                retorno += line.Perfil + ";";
                retorno += line.Anunciante + ";";
                retorno += line.Endereco + ";";
                retorno += line.Iptu + ";";
                retorno += line.Apto + ";";
                retorno += line.Condominio + ";";
                retorno += line.Dormitorios + "";
            }
            catch (Exception ex)
            {
                retorno = string.Empty;
            }

            return retorno;
        }
    }
}
