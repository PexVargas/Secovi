using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace BootSecovi
{
    class GeradorCSV
    {
        public static void GeradorZapRJ() 
        {
           var dtDataTable =  OpenSQL(@"SELECT 
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
                                Cep
	                            FROM ImoveisCapturados ");


            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames
                    = dtDataTable.Columns.Cast<DataColumn>().
                        Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in dtDataTable.Rows)
            {
                IEnumerable<string> fields
                    = row.ItemArray.Select(
                    field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }

            File.WriteAllText(@"c:\csv\zap.csv", sb.ToString());
        }

        public static DataTable OpenSQL(string pSql)
        {
            var cs = "Server=pexim.czclmi3p5njz.us-east-2.rds.amazonaws.com;Initial Catalog=PexIM; uid=admin;pwd=Pietro2011#";
            DataTable dt = new DataTable();

            //VERIFICA SE O TIPO DE BANCO É SQL SERVER

             var connString = "Server=localhost;Database=test;Uid=usuario;Pwd=senha"; 
    var connection = new MySqlConnection(connString);
          
            //using (SqlConnection cn = new SqlConnection())
            //{

            //    cn.ConnectionString = cs;

            //    SqlDataAdapter da = new SqlDataAdapter();

            //    da.SelectCommand = new SqlCommand();

            //    da.SelectCommand.Connection = cn;
            //    da.SelectCommand.CommandTimeout = 200;
            //    da.SelectCommand.CommandType = CommandType.Text;

            //    da.SelectCommand.CommandText = pSql;


            //    cn.Open();

            //    da.Fill(dt);
            //}
            return dt;
        }
    }
}
