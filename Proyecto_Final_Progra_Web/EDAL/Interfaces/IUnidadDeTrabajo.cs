using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDAL.Interfaces
{
    public interface IUnidadDeTrabajo : IDisposable
    {

        ICitaDAL CitaDAL { get; }
        IMascotaDAL MascotaDAL { get; } 
        IMedicamentoDAL MedicamentoDAL { get; }
        IPadecimientoDAL PadecimientoDAL { get; }
        IRazaMascotaDAL RazaMascotaDAL { get; }
        ITipoMascotaDAL TipoMascotaDAL { get; }
        IVacunasDesparasitacioneDAL VacunasDAL { get; }

        bool Complete();
    }
}
