using EDAL.Interfaces;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDAL.Implementations
{
    public class PadecimientoDALImpl : DALGenericoImpl<Padecimiento>,IPadecimientoDAL
    {
        private VeterinariaDbContext _context;

        public PadecimientoDALImpl(VeterinariaDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
