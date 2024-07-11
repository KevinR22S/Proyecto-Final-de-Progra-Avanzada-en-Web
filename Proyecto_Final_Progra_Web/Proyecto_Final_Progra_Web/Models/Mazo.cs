using System;
using System.Collections.Generic;

namespace Proyecto_Final_Progra_Web.Models;

public partial class Mazo
{
    public int MazoId { get; set; }

    public int? UsuarioId { get; set; }

    public string NombreMazo { get; set; } = null!;

    public DateTime? CreadoEn { get; set; }

    public virtual Usuario? Usuario { get; set; }

    public virtual ICollection<Carta> Carta { get; set; } = new List<Carta>();
}
