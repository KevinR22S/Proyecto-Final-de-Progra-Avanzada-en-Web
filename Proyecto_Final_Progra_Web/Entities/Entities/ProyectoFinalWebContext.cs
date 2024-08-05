using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entities.Entities;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-D15SUTD\\SQLEXPRESS;Database=Proyecto_Final_Web;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Carta>(entity =>
        {
            entity.HasKey(e => e.CartaId).HasName("PK__cartas__D8704F7BE1CB01CC");

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
            entity.HasKey(e => e.MazoId).HasName("PK__mazos__80DC12C788FBDCDD");

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
                .HasConstraintName("FK__mazos__usuario_i__3E52440B");

            entity.HasMany(d => d.Carta).WithMany(p => p.Mazos)
                .UsingEntity<Dictionary<string, object>>(
                    "CartasEnMazo",
                    r => r.HasOne<Carta>().WithMany()
                        .HasForeignKey("CartaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__cartas_en__carta__4316F928"),
                    l => l.HasOne<Mazo>().WithMany()
                        .HasForeignKey("MazoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__cartas_en__mazo___4222D4EF"),
                    j =>
                    {
                        j.HasKey("MazoId", "CartaId").HasName("PK__cartas_e__2D5B1630D4735FA7");
                        j.ToTable("cartas_en_mazo");
                        j.IndexerProperty<int>("MazoId").HasColumnName("mazo_id");
                        j.IndexerProperty<int>("CartaId").HasColumnName("carta_id");
                    });
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__usuarios__2ED7D2AFA4D7C62B");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.NombreUsuario, "UQ__usuarios__D4D22D74D19F414C").IsUnique();

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
