using DAL.Intefaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class CartaDALImpl : DALGenericoImpl <Carta>, ICartaDAL
    {
        private ProyectoFinalWebContext _context;

        public CartaDALImpl(ProyectoFinalWebContext context): base(context) 
        {
            this._context = context;
        }

    }
}
