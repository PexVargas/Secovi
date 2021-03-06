﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
                var ic = RetornaDadosClassificados(tipoImovel, siglaEstado, mes, ano);

                var lstImobiliarias = db.Imobiliarias.ToList();
                sb.AppendLine("Tipo;Cidade;Bairro;Valor;Area Privativa;Area Total;Quartos;Garagens;Suítes;Url;Descrição;Imobiliaria;Estado;Finalidade;Perfil;Anunciante;Endereco;Iptu;Apto;Condominio;Dormitorios");

                foreach (ImoveisDados line in ic)
                { 
                    if(line.Tipo != null)
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
            var result = Encoding.UTF8.GetPreamble().Concat(utf8Bytes).ToArray();

            string NomeArquivo = String.Format("{0}_{1}_{2}", tipoImovel, siglaEstado, (ano.ToString() + mes.ToString()));

            return File(result, "text/csv", string.Format( "{0}.csv", NomeArquivo));

        }

        public List<ImoveisDados> RetornaDadosClassificados(int tipoImovel, string siglaEstado, string mes, int ano)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
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
                                     //Perfil = ic.Perfil,
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

        public static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        public string LinhaCSV(ImoveisDados line, List<Imobiliarias> listImobiliarias)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
            string retorno = string.Empty;
            try
            {
                string descricao = string.Empty;

                retorno += line.Tipo + ";";
                retorno += line.Cidade.Replace(";", "") + ";";
                retorno += line.Bairro.Replace(";","") + ";";

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

                descricao = StripHTML(line.Descricao
                                .Replace(";", ",")
                                .Replace("\r\n", " ")
                                .Replace("\n\r", " ")
                                .Replace("\\r", " ")
                                .Replace("\\n", " ")
                                .Replace("\r", " ")
                                .Replace("\n", " ")
                                .Replace("\"", "")
                                .Replace(Environment.NewLine, " "));

          
                retorno += descricao + ";";

                var imobiliaria = listImobiliarias.Where(x => x.Id == line.CodImobiliaria).FirstOrDefault();

                if (imobiliaria != null)
                   retorno += imobiliaria.Nome + ";";
                else
                    retorno += "NA" + ";";

                retorno += line.SiglaEstado + ";";
                retorno += (line.TipoImovel == 1 ? "Venda" : "Locação") + ";";
               
                if (line.Tipo != null)
                    retorno += DeParaPerfil(line.Tipo) + ";";
                else
                    line.Tipo = "";

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

        public string DeParaPerfil(string tipo)
        {
            string perfil = "OUTROS";

            if (tipo.Contains("CASA")|| 
                tipo.Contains("APARTAMENTO") || 
                tipo.Contains("COBERTURA") ||
                tipo.Contains("COBERTURA") ||
                tipo.Contains("QUITINETE"))
                perfil = "RESIDENCIAL";

            if (tipo.Contains("COMERCIAL") || 
                tipo.Contains("GALPÃO/DEPÓSITO/ARMAZÉM") || 
                tipo.Contains("LOJA") || 
                tipo.Contains("PONTO COMERCIAL/LOJA/BOX") || 
                tipo.Contains("PRÉDIO")||
                tipo.Contains("EDIFÍCIO") ||
                tipo.Contains("PONTO COMERCIAL") ||
                tipo.Contains("BOX") ||
                tipo.Contains("SALA") ||
                tipo.Contains("CONJUNTO")  )
                perfil = "COMERCIAL";


            return perfil;
        }
        public string DeParaPerfilOlD(string tipo)
        {
            string perfil = "OUTROS";
            Dictionary<string, string> deParaPerfil =  new Dictionary<string, string>();

            deParaPerfil.Add("APARTAMENTO", "RESIDENCIAL");
            deParaPerfil.Add("COBERTURA", "RESIDENCIAL");
            deParaPerfil.Add("CASA", "RESIDENCIAL");

            deParaPerfil.Add("CASA 1 DORM.", "RESIDENCIAL");
            deParaPerfil.Add("CASA 2 DORM.", "RESIDENCIAL");
            deParaPerfil.Add("CASA 3 DORM.", "RESIDENCIAL");
            deParaPerfil.Add("CASA 4 DORM.", "RESIDENCIAL");
            deParaPerfil.Add("CASA DE CONDOMÍNIO DE DOIS ANDARES >= 5 DORM.", "RESIDENCIAL");
            deParaPerfil.Add("CASA DE CONDOMÍNIO DE DOIS ANDARES 2 DORM.", "RESIDENCIAL");
            deParaPerfil.Add("CASA DE CONDOMÍNIO DE DOIS ANDARES 3 DORM.", "RESIDENCIAL");
            deParaPerfil.Add("CASA DE CONDOMÍNIO DE DOIS ANDARES 4 DORM.", "RESIDENCIAL");
            deParaPerfil.Add("CASA DE VILA 1 DORM.", "RESIDENCIAL");
            deParaPerfil.Add("CASA DE VILA 2 DORM.", "RESIDENCIAL");
            deParaPerfil.Add("CASA DE VILA DUPLEX 2 DORM.", "RESIDENCIAL");
            deParaPerfil.Add("COBERTURA", "RESIDENCIAL");
            deParaPerfil.Add("COBERTURA >= 5 DORM.", "RESIDENCIAL");
            deParaPerfil.Add("COBERTURA 1 DORM.", "RESIDENCIAL");
            deParaPerfil.Add("COBERTURA 2 DORM.", "RESIDENCIAL");
            deParaPerfil.Add("COBERTURA 3 DORM.", "RESIDENCIAL");
            deParaPerfil.Add("COBERTURA 4 DORM.", "RESIDENCIAL");
            deParaPerfil.Add("COBERTURA TRIPLEX 4 DORM.", "RESIDENCIAL");
            deParaPerfil.Add("QUITINETE", "RESIDENCIAL");

            deParaPerfil.Add("COMERCIAL", "COMERCIAL");
            deParaPerfil.Add("GALPÃO/DEPÓSITO/ARMAZÉM", "COMERCIAL");
            deParaPerfil.Add("LOJA", "COMERCIAL");
            deParaPerfil.Add("PONTO COMERCIAL/LOJA/BOX", "COMERCIAL");
            deParaPerfil.Add("PRÉDIO", "COMERCIAL");
            deParaPerfil.Add("PRÉDIO/EDIFÍCIO INTEIRO", "COMERCIAL");
            deParaPerfil.Add("SALA/CONJUNTO", "COMERCIAL");


            var cPerfil = deParaPerfil.Where(x => x.Key.Contains(tipo.Trim().ToUpper())).FirstOrDefault();
           
            if(cPerfil.Key != null)
                perfil = cPerfil.Value;

            return perfil;
        }
    }
}
