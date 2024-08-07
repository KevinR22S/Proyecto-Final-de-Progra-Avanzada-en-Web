using EDAL.Interfaces;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDAL.Implementations
{
    public class UnidadDeTrabajo :IUnidadDeTrabajo
    {
        public ICitaDAL CitaDAL {  get; set; }
        public IMascotaDAL MascotaDAL { get; set; }
        public IMedicamentoDAL MedicamentoDAL { get; set; }
        public IPadecimientoDAL PadecimientoDAL { get;set; }
        public IRazaMascotaDAL RazaMascotaDAL { get; set; }
        public ITipoMascotaDAL TipoMascotaDAL { get; set; }
        public IVacunasDesparasitacioneDAL VacunasDAL { get; set; }

        private VeterinariaDbContext _context;

        public UnidadDeTrabajo(VeterinariaDbContext dbContext,
            ICitaDAL citaDAL,
            IMascotaDAL mascotaDAL,
            IMedicamentoDAL medicamentoDAL,
            IPadecimientoDAL padecimientoDAL,
            IRazaMascotaDAL razaMascotaDAL,
            ITipoMascotaDAL tipoMascotaDAL,
            IVacunasDesparasitacioneDAL vacunasDesparasitacioneDAL)
        {
            this._context = dbContext;
            this.CitaDAL = citaDAL;
            this.MascotaDAL = mascotaDAL;
            this.MedicamentoDAL = medicamentoDAL;
            this.PadecimientoDAL = padecimientoDAL;
            this.RazaMascotaDAL = razaMascotaDAL;
            this.TipoMascotaDAL = tipoMascotaDAL;
            this.VacunasDAL = vacunasDesparasitacioneDAL;
        }
        public bool Complete()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

    }
}
