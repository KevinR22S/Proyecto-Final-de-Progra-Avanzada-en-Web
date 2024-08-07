using System;
using System.Collections.Generic;

namespace Entity.Entities;

public partial class RazaMascota
{
    public int IdRazaMascota { get; set; }

    public int IdTipoMascota { get; set; }

    public string Raza { get; set; } = null!;

    public virtual TipoMascota IdTipoMascotaNavigation { get; set; } = null!;

    public virtual ICollection<Mascota> Mascota { get; set; } = new List<Mascota>();
}
