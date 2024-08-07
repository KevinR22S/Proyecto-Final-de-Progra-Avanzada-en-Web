using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Entities;

public partial class Mazo
{
    public int MazoId { get; set; }

    public String? UsuarioId { get; set; }

    public string NombreMazo { get; set; } = null!;

    public DateTime CreadoEn { get; set; }

    public string Estado { get; set; }

    public ApplicationUser? UsuarioModificacion { get; set; }

    public virtual ICollection<Carta> Carta { get; set; } = new List<Carta>();
}
