using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Final_Progra_Web.Models;

public partial class ProyectoFinalWebContext : DbContext
{
    public ProyectoFinalWebContext()
    {
    }

    public ProyectoFinalWebContext(DbContextOptions<ProyectoFinalWebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carta> Cartas { get; set; }

    public virtual DbSet<Mazo> Mazos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carta>(entity =>
        {
            entity.HasKey(e => e.CartaId).HasName("PK__cartas__D8704F7BA7987DA0");

            entity.ToTable("cartas");

            entity.Property(e => e.CartaId).HasColumnName("carta_id");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creado_en");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PuntosAtaque).HasColumnName("puntos_ataque");
            entity.Property(e => e.PuntosDefensa).HasColumnName("puntos_defensa");
        });

        modelBuilder.Entity<Mazo>(entity =>
        {
            entity.HasKey(e => e.MazoId).HasName("PK__mazos__80DC12C72EEB1B57");

            entity.ToTable("mazos");

            entity.Property(e => e.MazoId).HasColumnName("mazo_id");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creado_en");
            entity.Property(e => e.NombreMazo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_mazo");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Mazos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__mazos__usuario_i__3F466844");

            entity.HasMany(d => d.Carta).WithMany(p => p.Mazos)
                .UsingEntity<Dictionary<string, object>>(
                    "CartasEnMazo",
                    r => r.HasOne<Carta>().WithMany()
                        .HasForeignKey("CartaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__cartas_en__carta__440B1D61"),
                    l => l.HasOne<Mazo>().WithMany()
                        .HasForeignKey("MazoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__cartas_en__mazo___4316F928"),
                    j =>
                    {
                        j.HasKey("MazoId", "CartaId").HasName("PK__cartas_e__2D5B1630D0CEECD6");
                        j.ToTable("cartas_en_mazo");
                        j.IndexerProperty<int>("MazoId").HasColumnName("mazo_id");
                        j.IndexerProperty<int>("CartaId").HasColumnName("carta_id");
                    });
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__usuarios__2ED7D2AFCD04864E");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.NombreUsuario, "UQ__usuarios__D4D22D741F8CFC81").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            entity.Property(e => e.ContrasenaHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contrasena_hash");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creado_en");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_usuario");
            entity.Property(e => e.Rol)
                .HasMaxLength(10)
                .HasColumnName("rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
