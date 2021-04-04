using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImobiliariasCrawler.Main.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "imoveiscapturados",
                columns: table => new
                {
                    codImovelcapturado = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<string>(type: "varchar(200)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1")
                        .Annotation("MySql:Collation", "latin1_swedish_ci"),
                    Cidade = table.Column<string>(type: "varchar(250)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1")
                        .Annotation("MySql:Collation", "latin1_swedish_ci"),
                    Bairro = table.Column<string>(type: "varchar(250)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1")
                        .Annotation("MySql:Collation", "latin1_swedish_ci"),
                    Valor = table.Column<string>(type: "varchar(45)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1")
                        .Annotation("MySql:Collation", "latin1_swedish_ci"),
                    Quartos = table.Column<string>(type: "varchar(45)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1")
                        .Annotation("MySql:Collation", "latin1_swedish_ci"),
                    Suites = table.Column<string>(type: "varchar(45)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1")
                        .Annotation("MySql:Collation", "latin1_swedish_ci"),
                    Garagens = table.Column<string>(type: "varchar(45)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1")
                        .Annotation("MySql:Collation", "latin1_swedish_ci"),
                    Churrasqueiras = table.Column<string>(type: "varchar(45)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1")
                        .Annotation("MySql:Collation", "latin1_swedish_ci"),
                    Satus = table.Column<string>(type: "varchar(45)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1")
                        .Annotation("MySql:Collation", "latin1_swedish_ci"),
                    Url = table.Column<string>(type: "varchar(1000)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1")
                        .Annotation("MySql:Collation", "latin1_swedish_ci"),
                    Descricao = table.Column<string>(type: "varchar(4000)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1")
                        .Annotation("MySql:Collation", "latin1_swedish_ci"),
                    SiglaEstado = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1")
                        .Annotation("MySql:Collation", "latin1_swedish_ci"),
                    TipoImovel = table.Column<int>(type: "int(11)", nullable: true),
                    AreaTotal = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1")
                        .Annotation("MySql:Collation", "latin1_swedish_ci"),
                    AreaPrivativa = table.Column<string>(type: "varchar(45)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1")
                        .Annotation("MySql:Collation", "latin1_swedish_ci"),
                    Imagens = table.Column<string>(type: "varchar(3000)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1")
                        .Annotation("MySql:Collation", "latin1_swedish_ci"),
                    CodImobiliaria = table.Column<int>(type: "int(11)", nullable: true),
                    DataCaptura = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Excluido = table.Column<int>(type: "int(11)", nullable: true),
                    Iptu = table.Column<string>(type: "varchar(45)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1")
                        .Annotation("MySql:Collation", "latin1_swedish_ci"),
                    Condominio = table.Column<string>(type: "varchar(45)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1")
                        .Annotation("MySql:Collation", "latin1_swedish_ci"),
                    Cep = table.Column<string>(type: "varchar(45)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1")
                        .Annotation("MySql:Collation", "latin1_swedish_ci"),
                    Rua = table.Column<string>(type: "varchar(2000)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1")
                        .Annotation("MySql:Collation", "latin1_swedish_ci"),
                    Banheiros = table.Column<string>(type: "varchar(45)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1")
                        .Annotation("MySql:Collation", "latin1_swedish_ci"),
                    Anunciante = table.Column<string>(type: "varchar(400)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1")
                        .Annotation("MySql:Collation", "latin1_swedish_ci"),
                    Finalidade = table.Column<string>(type: "varchar(45)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1")
                        .Annotation("MySql:Collation", "latin1_swedish_ci"),
                    Localidade = table.Column<string>(type: "varchar(1000)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1")
                        .Annotation("MySql:Collation", "latin1_swedish_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.codImovelcapturado);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "imoveiscapturados");
        }
    }
}
