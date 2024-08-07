using EDAL.Interfaces;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDAL.Implementations
{
    public class TipoMascotaDALImpl : DALGenericoImpl<TipoMascota>,ITipoMascotaDAL
    {
        private VeterinariaDbContext _context;

        public TipoMascotaDALImpl(VeterinariaDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
