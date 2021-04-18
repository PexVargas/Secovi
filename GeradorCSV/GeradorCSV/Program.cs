using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo ptBR = new CultureInfo("pt-BR");
            //Data Source=72.167.226.226,3306;Initial Catalog=pexim_homolog;User ID=pexboot;Password=pex2021#;

            string connStr = "server=72.167.226.226;user=pexboot;database=pexim_homolog;password=pex2021#";

            GerarArquivoGarcia(connStr);

           //GerarArquivoCreditoReal(connStr);

            //GerarArquivoZap(connStr);


            Console.WriteLine("Pressiona qualquer tecla para encerrar...");
            Console.Read();
        }

        public static void GerarArquivoCreditoReal(string connStr) 
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                //SQL Query to execute

                string sql = @"SELECT 
                             CASE
                                   WHEN TipoImovel='1' THEN 'Venda'
                                   ELSE 'Aluguel'
                                   END AS Tipo_Busca,
                                Finalidade,
	                            Tipo,
                                Cidade,
                                Bairro as Localidade,
                                Bairro,
                                Valor,
                                quartos,
                                AreaTotal as Metragem_Util,
                                Iptu,
                                Condominio,
                                Garagens as Vagas,
                                Rua,
                                Suites,
                                url as Link_do_anuncio,
                                Anunciante,
                                AreaPrivativa,
                                'Credito Real' as Imobiliaria,
                                Cep,
                                Descricao
	                            FROM ImoveisCapturados where  codImobiliaria = 6 and siglaEstado = 'RS';";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandTimeout = 9000;

                MySqlDataReader rdr = cmd.ExecuteReader();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("Tipo_Busca;Finalidade;Tipo;Cidade;Localidade;Bairro;Valor;Quartos;Metragem_Util;Iptu;Condominio;Vagas;Rua;Suites;Link_do_anuncio;Anunciante;AreaPrivativa;Imobiliaria;Cep;Descricao");

                //read the data
                while (rdr.Read())
                {
                    sb.AppendLine(rdr[0].ToString().Trim() + ";" + rdr[1].ToString().Trim() + ";" + rdr[2].ToString().Trim() + ";" + rdr[3].ToString().Trim() + ";" + rdr[4].ToString().Trim() + ";" + rdr[5].ToString().Trim() + ";" + rdr[6].ToString().Trim() + ";" + rdr[7].ToString().Trim() + ";" + rdr[8].ToString().Trim() + ";" + rdr[9].ToString().Trim() + ";" + rdr[10].ToString().Trim() + ";" + rdr[11].ToString().Trim() + ";" + rdr[12].ToString().Trim() + ";" + rdr[13].ToString().Trim() + ";" + rdr[14].ToString().Trim() + ";" + rdr[15].ToString().Trim() + ";" + rdr[16].ToString().Trim() + ";" + rdr[17].ToString().Trim() + ";" + rdr[18].ToString().Trim().Replace("\"", "") + ";" + rdr[19].ToString().Trim().Replace("\"", "").Replace(";", "").Replace("\r\n", "").Replace("\r", "")); 
                }

                rdr.Close();

                File.WriteAllText(@"c:\csv\CreditoReal_RS.csv", sb.ToString());
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }

            conn.Close();
        }
        public static void GerarArquivoGarcia(string connStr)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                //SQL Query to execute

                string sql = @"SELECT 
                             CASE
                                   WHEN TipoImovel='1' THEN 'Venda'
                                   ELSE 'Aluguel'
                                   END AS Tipo_Busca,
                                Finalidade,
	                            Tipo,
                                Cidade,
                                Bairro as Localidade,
                                Bairro,
                                Valor,
                                quartos,
                                AreaTotal as Metragem_Util,
                                Iptu,
                                Condominio,
                                Garagens as Vagas,
                                Rua,
                                Suites,
                                url as Link_do_anuncio,
                                Anunciante,
                                AreaPrivativa,
                                'Garcia' as Imobiliaria,
                                Cep,
                                Descricao
	                            FROM ImoveisCapturados where  codImobiliaria = 7 and siglaEstado = 'RS';";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandTimeout = 9000;

                MySqlDataReader rdr = cmd.ExecuteReader();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("Tipo_Busca;Finalidade;Tipo;Cidade;Localidade;Bairro;Valor;Quartos;Metragem_Util;Iptu;Condominio;Vagas;Rua;Suites;Link_do_anuncio;Anunciante;AreaPrivativa;Imobiliaria;Cep;Descricao");

                //read the data
                while (rdr.Read())
                {
                    sb.AppendLine(rdr[0].ToString().Trim() + ";" + rdr[1].ToString().Trim() + ";" + rdr[2].ToString().Trim() + ";" + rdr[3].ToString().Trim() + ";" + rdr[4].ToString().Trim() + ";" + rdr[5].ToString().Trim() + ";" + rdr[6].ToString().Trim() + ";" + rdr[7].ToString().Trim() + ";" + rdr[8].ToString().Trim() + ";" + rdr[9].ToString().Trim() + ";" + rdr[10].ToString().Trim() + ";" + rdr[11].ToString().Trim() + ";" + rdr[12].ToString().Trim() + ";" + rdr[13].ToString().Trim() + ";" + rdr[14].ToString().Trim() + ";" + rdr[15].ToString().Trim() + ";" + rdr[16].ToString().Trim() + ";" + rdr[17].ToString().Trim() + ";" + rdr[18].ToString().Trim().Replace("\"", "") + ";" + rdr[19].ToString().Trim().Replace("\"", "").Replace(";", "").Replace("\r\n", "").Replace("\r", ""));
                }

                rdr.Close();

                File.WriteAllText(@"c:\csv\Garcia.csv", sb.ToString());
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }

            conn.Close();
        }


            public static void GerarArquivoZap(string connStr)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                //SQL Query to execute

                string sql = @"SELECT 
                             CASE
                                   WHEN TipoImovel='1' THEN 'Venda'
                                   ELSE 'Aluguel'
                                   END AS Tipo_Busca,
                            CASE
		                            WHEN Finalidade='COMMERCIAL' THEN 'Comercial'
                                    ELSE 'Residencial'
                                    END AS Finalidade,
	                            CASE
		                            WHEN Tipo='HOME' THEN 'Casa'
		                            WHEN Tipo='APARTMENT' THEN 'Apartamento'
		                            WHEN Tipo='ALLOTMENT_LAND' THEN 'Terrenos/Lotes Comerciais'
		                            WHEN Tipo='BUILDING' THEN 'Prédio Inteiro'
		                            WHEN Tipo='BUSINESS' THEN 'Loja/Salão/Ponto Comercial'
		                            WHEN Tipo='CLINIC' THEN 'Conjunto Comercial/Sala'
		                            WHEN Tipo='COMMERCIAL_ALLOTMENT_LAND' THEN 'Terrenos/Lotes Comerciais'
		                            WHEN Tipo='COMMERCIAL_BUILDING' THEN 'Prédio Inteiro'
		                            WHEN Tipo='COMMERCIAL_PROPERTY' THEN 'Propriedade Comercial'
		                            WHEN Tipo='CONDOMINIUM' THEN 'Casa de Condomínio'
		                            WHEN Tipo='CONDOMINIUM,\r\n  SINGLE_STOREY_HOUSE' THEN 'Casa de Condomínio'
		                            WHEN Tipo='CONDOMINIUM,\r\n  TWO_STORY_HOUSE' THEN 'Casa de Condomínio'
		                            WHEN Tipo='FARM' THEN 'Fazenda/Sítio/Chácara'
                                    WHEN Tipo='GALLERY' THEN 'Loja/Salão/Ponto Comercial'
		                            WHEN Tipo='FLAT' THEN 'Flat'
		                            WHEN Tipo='HOTEL' THEN 'Hotel/Motel/Pousada'
		                            WHEN Tipo='KITNET' THEN 'Kitnet'
		                            WHEN Tipo='LOFT' THEN 'Loft'
		                            WHEN Tipo='LOFT,\r\n  DUPLEX' THEN 'Loft'
		                            WHEN Tipo='LOFT,\r\n  TRIPLEX' THEN 'Loft'
		                            WHEN Tipo='OFFICE' THEN 'Conjunto Comercial/Sala'
		                            WHEN Tipo='PARKING_SPACE' THEN 'Garagem'
		                            WHEN Tipo='PENTHOUSE' THEN'Cobertura'
                                    WHEN Tipo='PENTHOUSE,\r\n  DUPLEX' THEN 'Cobertura'
		                            WHEN Tipo='PENTHOUSE,\r\n  TRIPLEX' THEN 'Cobertura'
		                            WHEN Tipo='RESIDENTIAL_ALLOTMENT_LAND' THEN 'Terreno/Lote/Condomínio'
		                            WHEN Tipo='RETAIL_CENTER' THEN 'Loja/Salão/Ponto Comercial'
		                            WHEN Tipo='SHED_DEPOSIT_WAREHOUSE' THEN 'Galpão/Depósito/Armazém'
		                            WHEN Tipo='SHOPPING' THEN 'Loja/Salão/Ponto Comercial'
		                            WHEN Tipo='SINGLE_STOREY_HOUSE' THEN 'Casa'
		                            WHEN Tipo='STUDIO' THEN 'Studio'
		                            WHEN Tipo='TWO_STORY_HOUSE' THEN 'Casa'
                                    WHEN Tipo='DUPLEX' THEN 'Apartamento'
                                    WHEN Tipo='TRIPLEX' THEN 'Apartamento'
		                            WHEN Tipo='VILLAGE_HOUSE' THEN 'Casa de Vila'
		                            WHEN Tipo='VILLAGE_HOUSE,\r\n  SINGLE_STOREY_HOUSE' THEN 'Casa de Vila'
		                            WHEN Tipo='VILLAGE_HOUSE,\r\n  TWO_STORY_HOUSE' THEN 'Casa de Vila'
		                            ELSE Tipo
	                            END AS Tipo,
                                Cidade,
                                Bairro as Localidade,
                                Bairro,
                                Valor,
                                quartos,
                                AreaTotal as Metragem_Util,
                                Iptu,
                                Condominio,
                                Garagens as Vagas,
                                Rua,
                                Suites,
                                url as Link_do_anuncio,
                                Anunciante,
                                AreaPrivativa,
                                'Zap' as Imobiliaria,
                                Cep,
                                Descricao
	                            FROM ImoveisCapturados and codImobiliaria = 1 and siglaEstado = 'RJ' ;";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandTimeout = 9000;

                MySqlDataReader rdr = cmd.ExecuteReader();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("Tipo_Busca;Finalidade;Tipo;Cidade;Localidade;Bairro;Valor;Quartos;Metragem_Util;Iptu;Condominio;Vagas;Rua;Suites;Link_do_anuncio;Anunciante;AreaPrivativa;Imobiliaria;Cep;Descricao");

                //read the data
                while (rdr.Read())
                {
                    sb.AppendLine(rdr[0].ToString().Trim() + ";" + rdr[1].ToString().Trim() + ";" + rdr[2].ToString().Trim() + ";" + rdr[3].ToString().Trim() + ";" + rdr[4].ToString().Trim() + ";" + rdr[5].ToString().Trim() + ";" + rdr[6].ToString().Trim() + ";" + rdr[7].ToString().Trim() + ";" + rdr[8].ToString().Trim() + ";" + rdr[9].ToString().Trim() + ";" + rdr[10].ToString().Trim() + ";" + rdr[11].ToString().Trim() + ";" + rdr[12].ToString().Trim() + ";" + rdr[13].ToString().Trim() + ";" + rdr[14].ToString().Trim() + ";" + rdr[15].ToString().Trim() + ";" + rdr[16].ToString().Trim() + ";" + rdr[17].ToString().Trim() + ";" + rdr[18].ToString().Trim().Replace("\"", "") + ";" + rdr[19].ToString().Trim().Replace("\"", "")); ;
                }

                rdr.Close();

                File.WriteAllText(@"c:\csv\Zap_RJ.csv", sb.ToString());
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }

            conn.Close();
        }
    }
}
