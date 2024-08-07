using EDAL.Interfaces;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDAL.Implementations
{
    public class CitaDALImpl : DALGenericoImpl <Cita>, ICitaDAL
    {
        private VeterinariaDbContext _context;

        public CitaDALImpl(VeterinariaDbContext context): base(context) 
        { 
            this._context = context;
        }

    }
}
