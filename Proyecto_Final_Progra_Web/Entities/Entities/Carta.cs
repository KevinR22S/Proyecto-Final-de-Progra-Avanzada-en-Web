using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Carta
{
    public int CartaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int? PuntosAtaque { get; set; }

    public int? PuntosDefensa { get; set; }

    public DateTime? CreadoEn { get; set; }

    public virtual ICollection<Mazo> Mazos { get; set; } = new List<Mazo>();
}
