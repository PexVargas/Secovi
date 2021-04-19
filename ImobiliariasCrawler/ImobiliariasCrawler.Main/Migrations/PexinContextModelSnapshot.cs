﻿// <auto-generated />
using System;
using ImobiliariasCrawler.Main.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ImobiliariasCrawler.Main.Migrations
{
    [DbContext(typeof(PexinContext))]
    partial class PexinContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ImobiliariasCrawler.Main.Imoveiscapturados", b =>
                {
                    b.Property<int>("CodImovelcapturado")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("codImovelcapturado")
                        .HasColumnType("int");

                    b.Property<string>("Anunciante")
                        .HasColumnType("varchar(400)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<string>("AreaPrivativa")
                        .HasColumnType("varchar(45)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<string>("AreaTotal")
                        .HasColumnType("varchar(45)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<string>("Bairro")
                        .HasColumnType("varchar(250)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<string>("Banheiros")
                        .HasColumnType("varchar(45)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<string>("Cep")
                        .HasColumnType("varchar(45)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<string>("Churrasqueiras")
                        .HasColumnType("varchar(45)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<string>("Cidade")
                        .HasColumnType("varchar(250)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<int?>("CodImobiliaria")
                        .HasColumnType("int");

                    b.Property<string>("Condominio")
                        .HasColumnType("varchar(45)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<DateTime?>("DataCaptura")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(4000)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<int?>("Excluido")
                        .HasColumnType("int");

                    b.Property<string>("Finalidade")
                        .HasColumnType("varchar(45)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<string>("Garagens")
                        .HasColumnType("varchar(45)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<string>("Imagens")
                        .HasColumnType("varchar(3000)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<string>("Iptu")
                        .HasColumnType("varchar(45)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<string>("Localidade")
                        .HasColumnType("varchar(1000)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<string>("Quartos")
                        .HasColumnType("varchar(45)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<string>("Rua")
                        .HasColumnType("varchar(2000)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<string>("Satus")
                        .HasColumnType("varchar(45)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<string>("SiglaEstado")
                        .HasColumnType("varchar(20)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<string>("Suites")
                        .HasColumnType("varchar(45)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<string>("Tipo")
                        .HasColumnType("varchar(200)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<int?>("TipoImovel")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("varchar(1000)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.Property<string>("Valor")
                        .HasColumnType("varchar(45)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_0900_ai_ci");

                    b.HasKey("CodImovelcapturado")
                        .HasName("PRIMARY");

                    b.ToTable("ImoveisCapturados");
                });

            modelBuilder.Entity("ImobiliariasCrawler.Main.Model.UrlsProcessadas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ArgsJson")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("ProcessedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Spider")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("Url")
                        .IsUnique();

                    b.ToTable("UrlsProcessadas");
                });
#pragma warning restore 612, 618
        }
    }
}
