using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entity.Entities;

public partial class VeterinariaDbContext : DbContext
{
    public VeterinariaDbContext()
    {
    }

    public VeterinariaDbContext(DbContextOptions<VeterinariaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Cita> Citas { get; set; }

    public virtual DbSet<Mascota> Mascotas { get; set; }

    public virtual DbSet<Medicamento> Medicamentos { get; set; }

    public virtual DbSet<Padecimiento> Padecimientos { get; set; }

    public virtual DbSet<RazaMascota> RazaMascotas { get; set; }

    public virtual DbSet<TipoMascota> TipoMascotas { get; set; }

    public virtual DbSet<VacunasDesparasitacione> VacunasDesparasitaciones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-D15SUTD\\SQLEXPRESS;Database=VeterinariaDB;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.RolesNavigation).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.IdCita);

            entity.HasIndex(e => e.IdMascota, "IX_Citas_IdMascota");

            entity.HasIndex(e => e.IdMedicamento, "IX_Citas_IdMedicamento");

            entity.HasIndex(e => e.UsuarioCreacionId, "IX_Citas_UsuarioCreacionId");

            entity.HasIndex(e => e.UsuarioModificacionId, "IX_Citas_UsuarioModificacionId");

            entity.HasOne(d => d.IdMascotaNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdMascota)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdMedicamentoNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdMedicamento)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.UsuarioCreacion).WithMany(p => p.CitaUsuarioCreacions).HasForeignKey(d => d.UsuarioCreacionId);

            entity.HasOne(d => d.UsuarioModificacion).WithMany(p => p.CitaUsuarioModificacions).HasForeignKey(d => d.UsuarioModificacionId);
        });

        modelBuilder.Entity<Mascota>(entity =>
        {
            entity.HasKey(e => e.IdMascota);

            entity.HasIndex(e => e.IdRazaMascota, "IX_Mascotas_IdRazaMascota");

            entity.HasIndex(e => e.IdTipoMascota, "IX_Mascotas_IdTipoMascota");

            entity.HasIndex(e => e.UsuarioCreacionId, "IX_Mascotas_UsuarioCreacionId");

            entity.HasIndex(e => e.UsuarioModificacionId, "IX_Mascotas_UsuarioModificacionId");

            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.IdRazaMascotaNavigation).WithMany(p => p.Mascota).HasForeignKey(d => d.IdRazaMascota);

            entity.HasOne(d => d.IdTipoMascotaNavigation).WithMany(p => p.Mascota)
                .HasForeignKey(d => d.IdTipoMascota)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.UsuarioCreacion).WithMany(p => p.MascotaUsuarioCreacions).HasForeignKey(d => d.UsuarioCreacionId);

            entity.HasOne(d => d.UsuarioModificacion).WithMany(p => p.MascotaUsuarioModificacions).HasForeignKey(d => d.UsuarioModificacionId);
        });

        modelBuilder.Entity<Medicamento>(entity =>
        {
            entity.HasKey(e => e.IdMedicamento);

            entity.HasIndex(e => e.CitaIdCita, "IX_Medicamentos_CitaIdCita");

            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.CitaIdCitaNavigation).WithMany(p => p.Medicamentos).HasForeignKey(d => d.CitaIdCita);
        });

        modelBuilder.Entity<Padecimiento>(entity =>
        {
            entity.HasKey(e => e.IdPadecimiento);

            entity.HasIndex(e => e.IdMascota, "IX_Padecimientos_IdMascota");

            entity.Property(e => e.Padece).HasMaxLength(100);

            entity.HasOne(d => d.IdMascotaNavigation).WithMany(p => p.Padecimientos)
                .HasForeignKey(d => d.IdMascota)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<RazaMascota>(entity =>
        {
            entity.HasKey(e => e.IdRazaMascota);

            entity.HasIndex(e => e.IdTipoMascota, "IX_RazaMascotas_IdTipoMascota");

            entity.Property(e => e.Raza).HasMaxLength(100);

            entity.HasOne(d => d.IdTipoMascotaNavigation).WithMany(p => p.RazaMascota)
                .HasForeignKey(d => d.IdTipoMascota)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<TipoMascota>(entity =>
        {
            entity.HasKey(e => e.IdTipoMascota);

            entity.Property(e => e.Tipo).HasMaxLength(100);
        });

        modelBuilder.Entity<VacunasDesparasitacione>(entity =>
        {
            entity.HasKey(e => e.IdVacunaDesparasitacion);

            entity.HasIndex(e => e.IdMascota, "IX_VacunasDesparasitaciones_IdMascota");

            entity.Property(e => e.Producto).HasMaxLength(100);
            entity.Property(e => e.Tipo).HasMaxLength(100);

            entity.HasOne(d => d.IdMascotaNavigation).WithMany(p => p.VacunasDesparasitaciones)
                .HasForeignKey(d => d.IdMascota)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
