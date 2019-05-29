using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Restaurantes.Models
{
    public partial class cruceroContext : DbContext
    {
        public cruceroContext()
        {
        }

        public cruceroContext(DbContextOptions<cruceroContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Jornada> Jornada { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Plato> Plato { get; set; }
        public virtual DbSet<Restaurante> Restaurante { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=crucero;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("categoria");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Jornada>(entity =>
            {
                entity.ToTable("jornada");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("menu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

                entity.Property(e => e.Jornada).HasColumnName("jornada");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(100);

                entity.Property(e => e.Restaurante).HasColumnName("restaurante");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnName("tipo")
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('General')");

                entity.HasOne(d => d.JornadaNavigation)
                    .WithMany(p => p.Menu)
                    .HasForeignKey(d => d.Jornada)
                    .HasConstraintName("fk_menu_jornada");

                entity.HasOne(d => d.RestauranteNavigation)
                    .WithMany(p => p.Menu)
                    .HasForeignKey(d => d.Restaurante)
                    .HasConstraintName("fk_menu_restaurante");
            });

            modelBuilder.Entity<Plato>(entity =>
            {
                entity.ToTable("plato");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Categoria).HasColumnName("categoria");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(300);

                entity.Property(e => e.Menu).HasColumnName("menu");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(200);

                entity.HasOne(d => d.CategoriaNavigation)
                    .WithMany(p => p.Plato)
                    .HasForeignKey(d => d.Categoria)
                    .HasConstraintName("fk_plato_categoria");

                entity.HasOne(d => d.MenuNavigation)
                    .WithMany(p => p.Plato)
                    .HasForeignKey(d => d.Menu)
                    .HasConstraintName("fk_plato_menu");
            });

            modelBuilder.Entity<Restaurante>(entity =>
            {
                entity.ToTable("restaurante");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Capacidad).HasColumnName("capacidad");

                entity.Property(e => e.Horario)
                    .HasColumnName("horario")
                    .HasMaxLength(100);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(100);

                entity.Property(e => e.Principal).HasColumnName("principal");
            });
        }
    }
}
