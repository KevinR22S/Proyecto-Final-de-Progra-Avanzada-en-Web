using DAL.Intefaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class MazoDALImpl :DALGenericoImpl<Mazo>, IMazoDAL
    {
        private ProyectoFinalWebContext _context;

        public MazoDALImpl(ProyectoFinalWebContext context) : base(context)
        {
            this._context = context;
        }
    }
}
