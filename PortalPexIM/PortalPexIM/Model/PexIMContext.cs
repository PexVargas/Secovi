using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PortalPexIM.Model
{
    public partial class peximContext : DbContext
    {
        public peximContext()
        {
        }

        public peximContext(DbContextOptions<peximContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Imoveiscapturados> Imoveiscapturados { get; set; }
        public virtual DbSet<Imoveisclassificados> Imoveisclassificados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("Data Source=72.167.226.226,3306;Initial Catalog=pexim;User ID=pexboot;Password=pex2021#");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Imoveiscapturados>(entity =>
            {
                entity.HasKey(e => e.CodImovelcapturado)
                    .HasName("PRIMARY");

                entity.ToTable("imoveiscapturados");

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

                entity.Property(e => e.Descricao).HasMaxLength(4000);

                entity.Property(e => e.Excluido).HasColumnType("int(11)");

                entity.Property(e => e.Finalidade).HasMaxLength(45);

                entity.Property(e => e.Garagens).HasColumnType("int(11)");

                entity.Property(e => e.Imagens)
                    .HasColumnName("imagens")
                    .HasColumnType("varchar(30000)");

                entity.Property(e => e.Iptu).HasMaxLength(45);

                entity.Property(e => e.Localidade).HasMaxLength(1000);

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
