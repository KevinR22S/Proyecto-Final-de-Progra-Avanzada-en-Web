using System;
using System.Collections.Generic;

namespace Entity.Entities;

public partial class Padecimiento
{
    public int IdPadecimiento { get; set; }

    public int IdMascota { get; set; }

    public string Padece { get; set; } = null!;

    public virtual Mascota IdMascotaNavigation { get; set; } = null!;
}
