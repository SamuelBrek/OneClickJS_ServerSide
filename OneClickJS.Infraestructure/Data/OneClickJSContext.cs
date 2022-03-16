using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OneClickJS.Domain.Entities;

#nullable disable

namespace OneClickJS.Infraestructure.Data
{
    public partial class OneClickJSContext : DbContext
    {
        public OneClickJSContext()
        {
        }

        public OneClickJSContext(DbContextOptions<OneClickJSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Empleo> Empleos { get; set; }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<Postulacione> Postulaciones { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=OneClickJS.mssql.somee.com;Initial Catalog=OneClickJS;Persist Security Info=False;User ID=Javier11x_SQLLogin_1;Password=yqkl1gjdod");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria);

                entity.Property(e => e.NombreCategoria)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Empleo>(entity =>
            {
                entity.HasKey(e => e.IdEmpleo);

                entity.HasIndex(e => e.IdCategoria, "IX_Empleos_IdCategoria");

                entity.HasIndex(e => e.IdEmpresa, "IX_Empleos_IdEmpresa");

                entity.Property(e => e.DescripcionEmpleo)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.FotoEmpleo).IsRequired();

                entity.Property(e => e.MunicipioEmpleo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NombreEmpleo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PrestacionesEmpleo)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.TipoEmpleo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Empleos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK_Empleos_Categorias");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Empleos)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("FK_Empleos_Empresas");
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa);

                entity.Property(e => e.CalleEmpresa)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ColoniaEmpresa)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ContraseniaEmpresa).HasMaxLength(10);

                entity.Property(e => e.CorreoEmpresa).HasMaxLength(100);

                entity.Property(e => e.CruzamientoEmpresa)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DirectorEmpresa)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FotoEmpresa).IsRequired();

                entity.Property(e => e.MunicipioEmpresa)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NivelEmpresa).IsRequired();

                entity.Property(e => e.NombreEmpresa)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NumeroEmpresa)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TelefonoEmpresa)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Postulacione>(entity =>
            {
                entity.HasKey(e => e.IdPostulacion);

                entity.Property(e => e.ArchivoPostulacion).IsRequired();

                entity.HasOne(d => d.IdEmpleoNavigation)
                    .WithMany(p => p.Postulaciones)
                    .HasForeignKey(d => d.IdEmpleo)
                    .HasConstraintName("FK_Postulaciones_Empleos");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Postulaciones)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Postulaciones_Usuarios");

                entity.Property(d => d.EstadoPostulacion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.Property(e => e.ApellidoUsuario)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ContraseñaUsuario)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.Property(e => e.CorreoUsuario)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NivelUsuario).IsRequired();

                entity.Property(e => e.NombreUsuario)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
