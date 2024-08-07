using EDAL.Interfaces;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDAL.Implementations
{
    public class MascotaDALImpl : DALGenericoImpl<Mascota>,IMascotaDAL
    {
        private VeterinariaDbContext _context;

        public MascotaDALImpl(VeterinariaDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
