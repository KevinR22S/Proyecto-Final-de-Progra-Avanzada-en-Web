using System;
using System.Collections.Generic;

namespace Entity.Entities;

public partial class AspNetUser
{
    public string Id { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public bool Estado { get; set; }

    public byte[]? Imagen { get; set; }

    public DateTime? UltimaConexion { get; set; }

    public string? Roles { get; set; }

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    public virtual ICollection<Cita> CitaUsuarioCreacions { get; set; } = new List<Cita>();

    public virtual ICollection<Cita> CitaUsuarioModificacions { get; set; } = new List<Cita>();

    public virtual ICollection<Mascota> MascotaUsuarioCreacions { get; set; } = new List<Mascota>();

    public virtual ICollection<Mascota> MascotaUsuarioModificacions { get; set; } = new List<Mascota>();

    public virtual ICollection<AspNetRole> RolesNavigation { get; set; } = new List<AspNetRole>();
}
