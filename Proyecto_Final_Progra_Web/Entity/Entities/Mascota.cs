using System;
using System.Collections.Generic;

namespace Entity.Entities;

public partial class Mascota
{
    public int IdMascota { get; set; }

    public string? UsuarioCreacionId { get; set; }

    public string? UsuarioModificacionId { get; set; }

    public int IdTipoMascota { get; set; }

    public int? IdRazaMascota { get; set; }

    public string Nombre { get; set; } = null!;

    public string Genero { get; set; } = null!;

    public string? Edad { get; set; }

    public string Peso { get; set; } = null!;

    public byte[]? Imagen { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool Eliminado { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual RazaMascota? IdRazaMascotaNavigation { get; set; }

    public virtual TipoMascota IdTipoMascotaNavigation { get; set; } = null!;

    public virtual ICollection<Padecimiento> Padecimientos { get; set; } = new List<Padecimiento>();

    public virtual AspNetUser? UsuarioCreacion { get; set; }

    public virtual AspNetUser? UsuarioModificacion { get; set; }

    public virtual ICollection<VacunasDesparasitacione> VacunasDesparasitaciones { get; set; } = new List<VacunasDesparasitacione>();
}
