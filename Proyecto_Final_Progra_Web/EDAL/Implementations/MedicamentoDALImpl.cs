using EDAL.Interfaces;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDAL.Implementations
{
    public class MedicamentoDALImpl : DALGenericoImpl<Medicamento>,IMedicamentoDAL
    {
        private VeterinariaDbContext _context;

        public MedicamentoDALImpl(VeterinariaDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
