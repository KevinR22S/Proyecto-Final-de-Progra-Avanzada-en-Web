using System;
using System.Collections.Generic;

namespace Entity.Entities;

public partial class TipoMascota
{
    public int IdTipoMascota { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual ICollection<Mascota> Mascota { get; set; } = new List<Mascota>();

    public virtual ICollection<RazaMascota> RazaMascota { get; set; } = new List<RazaMascota>();
}
