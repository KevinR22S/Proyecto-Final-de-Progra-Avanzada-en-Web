using System;
using System.Collections.Generic;

namespace Entity.Entities;

public partial class VacunasDesparasitacione
{
    public int IdVacunaDesparasitacion { get; set; }

    public int IdMascota { get; set; }

    public string Tipo { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public string? Producto { get; set; }

    public virtual Mascota IdMascotaNavigation { get; set; } = null!;
}
