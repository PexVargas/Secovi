using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BootSecovi.Model
{
    public partial class PexIMContext : DbContext
    {
        public PexIMContext()
        {
        }

        public PexIMContext(DbContextOptions<PexIMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ImoveisCapturados> ImoveisCapturados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("Data Source=database-1.czclmi3p5njz.us-east-2.rds.amazonaws.com;Initial Catalog=PexIM;User ID=admin;Password=Pietro2011#");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImoveisCapturados>(entity =>
            {
                entity.HasKey(e => e.CodImovelCapturado)
                    .HasName("PRIMARY");

                entity.Property(e => e.AreaPrivativa).HasMaxLength(45);

                entity.Property(e => e.AreaTotal).HasMaxLength(20);

                entity.Property(e => e.Bairro).HasMaxLength(250);

                entity.Property(e => e.Banheiros).HasMaxLength(45);

                entity.Property(e => e.Cep).HasMaxLength(45);

                entity.Property(e => e.Churrasqueiras).HasMaxLength(45);

                entity.Property(e => e.Cidade).HasMaxLength(250);

                entity.Property(e => e.Condominio).HasMaxLength(45);

                entity.Property(e => e.Descricao).HasMaxLength(4000);

                entity.Property(e => e.Garagens).HasMaxLength(45);

                entity.Property(e => e.Imagens).HasMaxLength(3000);

                entity.Property(e => e.Iptu).HasMaxLength(45);

                entity.Property(e => e.Quartos).HasMaxLength(45);

                entity.Property(e => e.Rua).HasMaxLength(2000);

                entity.Property(e => e.Satus).HasMaxLength(45);

                entity.Property(e => e.SiglaEstado).HasMaxLength(20);

                entity.Property(e => e.Suites).HasMaxLength(45);

                entity.Property(e => e.Tipo).HasMaxLength(200);

                entity.Property(e => e.Url).HasMaxLength(1000);

                entity.Property(e => e.Valor).HasMaxLength(45);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
