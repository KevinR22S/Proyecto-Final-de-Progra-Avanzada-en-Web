using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Mazo
{
    public int MazoId { get; set; }

    public int? UsuarioId { get; set; }

    public string NombreMazo { get; set; } = null!;

    public DateTime? CreadoEn { get; set; }

    public virtual Usuario? Usuario { get; set; }

    public virtual ICollection<Carta> Carta { get; set; } = new List<Carta>();
}
