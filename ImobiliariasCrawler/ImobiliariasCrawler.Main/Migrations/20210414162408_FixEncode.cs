using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImobiliariasCrawler.Main.Migrations
{
    public partial class FixEncode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "imoveiscapturados",
                newName: "ImoveisCapturados");

            migrationBuilder.AlterColumn<string>(
                name: "Valor",
                table: "ImoveisCapturados",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_0900_ai_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "ImoveisCapturados",
                type: "varchar(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_0900_ai_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterColumn<int>(
                name: "TipoImovel",
                table: "ImoveisCapturados",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "ImoveisCapturados",
                type: "varchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_0900_ai_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Suites",
                table: "ImoveisCapturados",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_0900_ai_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterColumn<string>(
                name: "SiglaEstado",
                table: "ImoveisCapturados",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_0900_ai_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Satus",
                table: "ImoveisCapturados",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_0900_ai_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Rua",
                table: "ImoveisCapturados",
                type: "varchar(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2000)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_0900_ai_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Quartos",
                table: "ImoveisCapturados",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_0900_ai_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Localidade",
                table: "ImoveisCapturados",
                type: "varchar(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_0900_ai_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Iptu",
                table: "ImoveisCapturados",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_0900_ai_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Imagens",
                table: "ImoveisCapturados",
                type: "varchar(3000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(3000)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_0900_ai_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Garagens",
                table: "ImoveisCapturados",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_0900_ai_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Finalidade",
                table: "ImoveisCapturados",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_0900_ai_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterColumn<int>(
                name: "Excluido",
                table: "ImoveisCapturados",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "ImoveisCapturados",
                type: "varchar(4000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(4000)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_0900_ai_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Condominio",
                table: "ImoveisCapturados",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_0900_ai_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterColumn<int>(
                name: "CodImobiliaria",
                table: "ImoveisCapturados",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "ImoveisCapturados",
                type: "varchar(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_0900_ai_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Churrasqueiras",
                table: "ImoveisCapturados",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_0900_ai_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Cep",
                table: "ImoveisCapturados",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_0900_ai_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Banheiros",
                table: "ImoveisCapturados",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_0900_ai_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Bairro",
                table: "ImoveisCapturados",
                type: "varchar(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_0900_ai_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterColumn<string>(
                name: "AreaTotal",
                table: "ImoveisCapturados",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_0900_ai_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterColumn<string>(
                name: "AreaPrivativa",
                table: "ImoveisCapturados",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_0900_ai_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Anunciante",
                table: "ImoveisCapturados",
                type: "varchar(400)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(400)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_0900_ai_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterColumn<int>(
                name: "codImovelcapturado",
                table: "ImoveisCapturados",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "ImoveisCapturados",
                newName: "imoveiscapturados");

            migrationBuilder.AlterColumn<string>(
                name: "Valor",
                table: "imoveiscapturados",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("MySql:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "imoveiscapturados",
                type: "varchar(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("MySql:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<int>(
                name: "TipoImovel",
                table: "imoveiscapturados",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "imoveiscapturados",
                type: "varchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("MySql:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Suites",
                table: "imoveiscapturados",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("MySql:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "SiglaEstado",
                table: "imoveiscapturados",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("MySql:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Satus",
                table: "imoveiscapturados",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("MySql:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Rua",
                table: "imoveiscapturados",
                type: "varchar(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2000)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("MySql:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Quartos",
                table: "imoveiscapturados",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("MySql:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Localidade",
                table: "imoveiscapturados",
                type: "varchar(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("MySql:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Iptu",
                table: "imoveiscapturados",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("MySql:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Imagens",
                table: "imoveiscapturados",
                type: "varchar(3000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(3000)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("MySql:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Garagens",
                table: "imoveiscapturados",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("MySql:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Finalidade",
                table: "imoveiscapturados",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("MySql:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<int>(
                name: "Excluido",
                table: "imoveiscapturados",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "imoveiscapturados",
                type: "varchar(4000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(4000)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("MySql:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Condominio",
                table: "imoveiscapturados",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("MySql:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<int>(
                name: "CodImobiliaria",
                table: "imoveiscapturados",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "imoveiscapturados",
                type: "varchar(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("MySql:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Churrasqueiras",
                table: "imoveiscapturados",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("MySql:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Cep",
                table: "imoveiscapturados",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("MySql:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Banheiros",
                table: "imoveiscapturados",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("MySql:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Bairro",
                table: "imoveiscapturados",
                type: "varchar(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("MySql:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "AreaTotal",
                table: "imoveiscapturados",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("MySql:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "AreaPrivativa",
                table: "imoveiscapturados",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("MySql:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Anunciante",
                table: "imoveiscapturados",
                type: "varchar(400)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(400)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("MySql:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AlterColumn<int>(
                name: "codImovelcapturado",
                table: "imoveiscapturados",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
        }
    }
}
