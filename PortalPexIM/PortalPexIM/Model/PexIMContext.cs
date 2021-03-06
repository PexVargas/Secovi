﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PortalPexIM.Model
{
    public partial class peximContext : DbContext
    {
        //public peximContext()
        //{
        //}

        public peximContext()
        {
            Database.SetCommandTimeout(150000);
        }

        public peximContext(DbContextOptions<peximContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cidades> Cidades { get; set; }
        public virtual DbSet<Imobiliarias> Imobiliarias { get; set; }
        public virtual DbSet<Imoveiscapturados> Imoveiscapturados { get; set; }
        public virtual DbSet<Imoveiscapturadosbkp> Imoveiscapturadosbkp { get; set; }
        public virtual DbSet<Imoveisclassificados> Imoveisclassificados { get; set; }
        public virtual DbSet<Palavrasbairros> Palavrasbairros { get; set; }
        public virtual DbSet<Palavrascidade> Palavrascidade { get; set; }
        public virtual DbSet<Palavrasrelacionadasbairro> Palavrasrelacionadasbairro { get; set; }
        public virtual DbSet<Palavrasrelacionadascidade> Palavrasrelacionadascidade { get; set; }
        public virtual DbSet<Palavrasrelacionadastipo> Palavrasrelacionadastipo { get; set; }
        public virtual DbSet<Palavrastipo> Palavrastipo { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<VwImoveiscapturados> VwImoveiscapturados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("Data Source=72.167.226.226;Initial Catalog=pexim;User ID=pexboot;Password=pex2021#");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cidades>(entity =>
            {
                entity.HasKey(e => e.CodCidade)
                    .HasName("PRIMARY");

                entity.ToTable("cidades");

                entity.Property(e => e.CodCidade).HasColumnType("int(11)");

                entity.Property(e => e.Excluido).HasColumnType("int(11)");

                entity.Property(e => e.NomeCidade).HasMaxLength(500);

                entity.Property(e => e.SiglaEstado).HasMaxLength(45);
            });

            modelBuilder.Entity<Imobiliarias>(entity =>
            {
                entity.HasKey(e => e.CodImobiliaria)
                    .HasName("PRIMARY");

                entity.ToTable("imobiliarias");

                entity.Property(e => e.CodImobiliaria)
                    .HasColumnName("codImobiliaria")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(6)");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Imoveiscapturados>(entity =>
            {
                entity.HasKey(e => e.CodImovelcapturado)
                    .HasName("PRIMARY");

                entity.ToTable("imoveiscapturados");

                entity.HasIndex(e => new { e.CodImobiliaria, e.TipoImovel })
                    .HasName("idx_imoveiscapturados_CodImobiliaria_TipoImovel");

                entity.HasIndex(e => new { e.CodImobiliaria, e.TipoImovel, e.SiglaEstado })
                    .HasName("idx_imoveiscapturados_CodImobiliaria_TipoImovel_SiglaEstado");

                entity.Property(e => e.CodImovelcapturado)
                    .HasColumnName("codImovelcapturado")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Anunciante).HasMaxLength(400);

                entity.Property(e => e.AreaPrivativa).HasMaxLength(45);

                entity.Property(e => e.AreaTotal).HasMaxLength(45);

                entity.Property(e => e.Bairro).HasMaxLength(250);

                entity.Property(e => e.Banheiros).HasMaxLength(45);

                entity.Property(e => e.Caracteristicas).HasMaxLength(4000);

                entity.Property(e => e.Cep).HasMaxLength(45);

                entity.Property(e => e.Churrasqueiras).HasMaxLength(45);

                entity.Property(e => e.Cidade).HasMaxLength(250);

                entity.Property(e => e.CodImobiliaria).HasColumnType("int(11)");

                entity.Property(e => e.CodImolvelApi)
                    .HasColumnName("CodImolvelAPI")
                    .HasMaxLength(50);

                entity.Property(e => e.CodImovel).HasMaxLength(50);

                entity.Property(e => e.Condominio).HasMaxLength(45);

                entity.Property(e => e.Descricao).HasMaxLength(4000);

                entity.Property(e => e.Excluido).HasColumnType("int(11)");

                entity.Property(e => e.Finalidade).HasMaxLength(45);

                entity.Property(e => e.Garagens).HasMaxLength(45);

                entity.Property(e => e.Imagens).HasColumnName("imagens");

                entity.Property(e => e.Iptu).HasMaxLength(45);

                entity.Property(e => e.Localidade).HasMaxLength(1000);

                entity.Property(e => e.Quartos).HasMaxLength(45);

                entity.Property(e => e.Rua).HasMaxLength(2000);

                entity.Property(e => e.Satus).HasMaxLength(45);

                entity.Property(e => e.SiglaEstado).HasMaxLength(20);

                entity.Property(e => e.Suites).HasMaxLength(45);

                entity.Property(e => e.Tipo).HasMaxLength(200);

                entity.Property(e => e.TipoImovel).HasColumnType("int(11)");

                entity.Property(e => e.Url).HasMaxLength(1000);

                entity.Property(e => e.Valor).HasMaxLength(45);
            });

            modelBuilder.Entity<Imoveiscapturadosbkp>(entity =>
            {
                entity.HasKey(e => e.CodImovelcapturado)
                    .HasName("PRIMARY");

                entity.ToTable("imoveiscapturadosbkp");

                entity.Property(e => e.CodImovelcapturado)
                    .HasColumnName("codImovelcapturado")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Anunciante).HasMaxLength(400);

                entity.Property(e => e.AreaPrivativa).HasMaxLength(45);

                entity.Property(e => e.AreaTotal).HasMaxLength(20);

                entity.Property(e => e.Bairro).HasMaxLength(250);

                entity.Property(e => e.Banheiros).HasMaxLength(45);

                entity.Property(e => e.Cep).HasMaxLength(45);

                entity.Property(e => e.Churrasqueiras).HasMaxLength(45);

                entity.Property(e => e.Cidade).HasMaxLength(250);

                entity.Property(e => e.CodImobiliaria).HasColumnType("int(11)");

                entity.Property(e => e.Condominio).HasMaxLength(45);

                entity.Property(e => e.Descricao).HasMaxLength(4000);

                entity.Property(e => e.Excluido).HasColumnType("int(11)");

                entity.Property(e => e.Finalidade).HasMaxLength(45);

                entity.Property(e => e.Garagens).HasMaxLength(45);

                entity.Property(e => e.Imagens)
                    .HasColumnName("imagens")
                    .HasColumnType("varchar(30000)");

                entity.Property(e => e.Iptu).HasMaxLength(45);

                entity.Property(e => e.Localidade).HasMaxLength(1000);

                entity.Property(e => e.Quartos).HasMaxLength(45);

                entity.Property(e => e.Rua).HasMaxLength(2000);

                entity.Property(e => e.Satus).HasMaxLength(45);

                entity.Property(e => e.SiglaEstado).HasMaxLength(20);

                entity.Property(e => e.Suites).HasMaxLength(45);

                entity.Property(e => e.Tipo).HasMaxLength(200);

                entity.Property(e => e.TipoImovel).HasColumnType("int(11)");

                entity.Property(e => e.Url).HasMaxLength(1000);

                entity.Property(e => e.Valor).HasMaxLength(45);
            });

            modelBuilder.Entity<Imoveisclassificados>(entity =>
            {
                entity.HasKey(e => e.CodImovelclassificado)
                    .HasName("PRIMARY");

                entity.ToTable("imoveisclassificados");

                entity.HasIndex(e => new { e.Tipo, e.SiglaEstado, e.TipoImovel, e.DataClassificacao, e.Excluido })
                    .HasName("idx_imoveisclassificados_home");

                entity.Property(e => e.CodImovelclassificado)
                    .HasColumnName("codImovelclassificado")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Anunciante).HasMaxLength(400);

                entity.Property(e => e.Apto).HasMaxLength(45);

                entity.Property(e => e.AreaPrivativa).HasColumnType("decimal(16,2)");

                entity.Property(e => e.AreaTotal).HasColumnType("decimal(16,2)");

                entity.Property(e => e.Bairro).HasMaxLength(250);

                entity.Property(e => e.BairroCapturado).HasMaxLength(250);

                entity.Property(e => e.Banheiros).HasMaxLength(45);

                entity.Property(e => e.Cep).HasMaxLength(45);

                entity.Property(e => e.Churrasqueiras).HasColumnType("int(11)");

                entity.Property(e => e.Cidade).HasMaxLength(250);

                entity.Property(e => e.CidadeCapturada).HasMaxLength(250);

                entity.Property(e => e.CodImobiliaria).HasColumnType("int(11)");

                entity.Property(e => e.Condominio).HasMaxLength(45);

                entity.Property(e => e.DataClassificacao).HasColumnType("date");

                entity.Property(e => e.Descricao).HasColumnType("varchar(8000)");

                entity.Property(e => e.Excluido).HasColumnType("int(11)");

                entity.Property(e => e.Finalidade).HasMaxLength(45);

                entity.Property(e => e.Garagens).HasColumnType("int(11)");

                entity.Property(e => e.Imagens)
                    .HasColumnName("imagens")
                    .HasColumnType("varchar(30000)");

                entity.Property(e => e.Iptu).HasMaxLength(45);

                entity.Property(e => e.Localidade).HasMaxLength(1000);

                entity.Property(e => e.PalavraExclusao).HasMaxLength(200);

                entity.Property(e => e.Quartos).HasColumnType("int(11)");

                entity.Property(e => e.Rua).HasMaxLength(2000);

                entity.Property(e => e.Satus).HasMaxLength(45);

                entity.Property(e => e.SiglaEstado).HasMaxLength(20);

                entity.Property(e => e.Suites).HasColumnType("int(11)");

                entity.Property(e => e.Tipo).HasMaxLength(200);

                entity.Property(e => e.TipoCapturado).HasMaxLength(250);

                entity.Property(e => e.TipoImovel).HasColumnType("int(11)");

                entity.Property(e => e.Url).HasMaxLength(1000);

                entity.Property(e => e.Valor).HasColumnType("decimal(16,2)");
            });

            modelBuilder.Entity<Palavrasbairros>(entity =>
            {
                entity.HasKey(e => e.CodPalavra)
                    .HasName("PRIMARY");

                entity.ToTable("palavrasbairros");

                entity.Property(e => e.CodPalavra).HasColumnType("int(11)");

                entity.Property(e => e.CodCidade).HasColumnType("int(11)");

                entity.Property(e => e.Excluido).HasColumnType("int(11)");

                entity.Property(e => e.Palavra).HasMaxLength(500);

                entity.Property(e => e.SiglaEstado).HasMaxLength(45);
            });

            modelBuilder.Entity<Palavrascidade>(entity =>
            {
                entity.HasKey(e => e.CodPalavra)
                    .HasName("PRIMARY");

                entity.ToTable("palavrascidade");

                entity.Property(e => e.CodPalavra).HasColumnType("int(11)");

                entity.Property(e => e.CodCidade).HasColumnType("int(11)");

                entity.Property(e => e.Excluido).HasColumnType("int(11)");

                entity.Property(e => e.Palavra).HasMaxLength(500);

                entity.Property(e => e.SiglaEstado).HasMaxLength(45);
            });

            modelBuilder.Entity<Palavrasrelacionadasbairro>(entity =>
            {
                entity.HasKey(e => e.CodPalavrasRelacionadaBairro)
                    .HasName("PRIMARY");

                entity.ToTable("palavrasrelacionadasbairro");

                entity.Property(e => e.CodPalavrasRelacionadaBairro).HasColumnType("int(11)");

                entity.Property(e => e.CodPalavra).HasColumnType("int(11)");

                entity.Property(e => e.Excluido).HasColumnType("int(11)");

                entity.Property(e => e.Palavra).HasMaxLength(500);
            });

            modelBuilder.Entity<Palavrasrelacionadascidade>(entity =>
            {
                entity.HasKey(e => e.CodPalavraRelacionadaCidade)
                    .HasName("PRIMARY");

                entity.ToTable("palavrasrelacionadascidade");

                entity.Property(e => e.CodPalavraRelacionadaCidade).HasColumnType("int(11)");

                entity.Property(e => e.CodPalavra).HasColumnType("int(11)");

                entity.Property(e => e.Excluida).HasColumnType("int(11)");

                entity.Property(e => e.Palavra).HasMaxLength(500);
            });

            modelBuilder.Entity<Palavrasrelacionadastipo>(entity =>
            {
                entity.HasKey(e => e.CodPalavraRelacionadaTipo)
                    .HasName("PRIMARY");

                entity.ToTable("palavrasrelacionadastipo");

                entity.Property(e => e.CodPalavraRelacionadaTipo).HasColumnType("int(11)");

                entity.Property(e => e.CodPalavra).HasColumnType("int(11)");

                entity.Property(e => e.Excluido).HasColumnType("int(11)");

                entity.Property(e => e.Palavra).HasMaxLength(500);
            });

            modelBuilder.Entity<Palavrastipo>(entity =>
            {
                entity.HasKey(e => e.CodPalavraTipo)
                    .HasName("PRIMARY");

                entity.ToTable("palavrastipo");

                entity.Property(e => e.CodPalavraTipo).HasColumnType("int(11)");

                entity.Property(e => e.Excluido).HasColumnType("int(11)");

                entity.Property(e => e.Palavra).HasMaxLength(500);

                entity.Property(e => e.SiglaEstado).HasMaxLength(45);
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.CodUsuario)
                    .HasName("PRIMARY");

                entity.ToTable("usuarios");

                entity.Property(e => e.CodUsuario)
                    .HasColumnName("codUsuario")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(30);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Senha)
                    .HasColumnName("senha")
                    .HasMaxLength(50);

                entity.Property(e => e.SiglaEstado)
                    .HasColumnName("siglaEstado")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VwImoveiscapturados>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_imoveiscapturados");

                entity.Property(e => e.Anunciante).HasMaxLength(400);

                entity.Property(e => e.AreaPrivativa).HasMaxLength(45);

                entity.Property(e => e.AreaTotal).HasMaxLength(45);

                entity.Property(e => e.Bairro).HasMaxLength(250);

                entity.Property(e => e.Banheiros).HasMaxLength(45);

                entity.Property(e => e.Cep).HasMaxLength(45);

                entity.Property(e => e.Churrasqueiras).HasMaxLength(45);

                entity.Property(e => e.Cidade).HasMaxLength(250);

                entity.Property(e => e.CodImobiliaria).HasColumnType("int(11)");

                entity.Property(e => e.CodImovelcapturado)
                    .HasColumnName("codImovelcapturado")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Condominio).HasMaxLength(45);

                entity.Property(e => e.Descricao).HasMaxLength(4000);

                entity.Property(e => e.Excluido).HasColumnType("int(11)");

                entity.Property(e => e.Finalidade).HasMaxLength(45);

                entity.Property(e => e.Garagens).HasMaxLength(45);

                entity.Property(e => e.Imagens).HasColumnName("imagens");

                entity.Property(e => e.Iptu).HasMaxLength(45);

                entity.Property(e => e.Localidade).HasMaxLength(1000);

                entity.Property(e => e.Quartos).HasMaxLength(45);

                entity.Property(e => e.Rua).HasMaxLength(2000);

                entity.Property(e => e.Satus).HasMaxLength(45);

                entity.Property(e => e.SiglaEstado).HasMaxLength(20);

                entity.Property(e => e.Suites).HasMaxLength(45);

                entity.Property(e => e.Tipo).HasMaxLength(200);

                entity.Property(e => e.TipoImovel).HasColumnType("int(11)");

                entity.Property(e => e.TipoProcessado).HasMaxLength(200);

                entity.Property(e => e.Url).HasMaxLength(1000);

                entity.Property(e => e.Valor).HasMaxLength(45);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
