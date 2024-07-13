using System;
using System.Collections.Generic;

namespace Proyecto_Final_Progra_Web.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string ContrasenaHash { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public DateTime? CreadoEn { get; set; }

    //public DateTime CreadoEn { get; set; } = DateTime.Now; // Valor por defecto de fecha actual


    public virtual ICollection<Mazo> Mazos { get; set; } = new List<Mazo>();
}
