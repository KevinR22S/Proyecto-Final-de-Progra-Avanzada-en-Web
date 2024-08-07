using System;
using System.Collections.Generic;

namespace Entity.Entities;

public partial class Medicamento
{
    public int IdMedicamento { get; set; }

    public string Nombre { get; set; } = null!;

    public int? CitaIdCita { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual Cita? CitaIdCitaNavigation { get; set; }
}
