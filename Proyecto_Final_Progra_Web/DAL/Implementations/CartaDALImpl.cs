using DAL.Intefaces;
using Entities.Entities;

namespace DAL.Implementations
{
    public class CartaDALImpl : DALGenericoImpl<Carta>, ICartaDAL
    {
        private ProyectoFinalWebContext _context;

        public CartaDALImpl(ProyectoFinalWebContext context) : base(context)
        {
            this._context = context;
        }
    }
}
