using DAL.Intefaces;
using Entities.Entities;
using System;

namespace DAL.Implementations
{
    public class UnidadDeTrabajo : IUnidadDeTrabajo, IDisposable
    {
        public ICartaDAL CartaDAL { get; set; }
        private readonly ProyectoFinalWebContext _context;
        public IMazoDAL MazoDAL { get; set; }

        public UnidadDeTrabajo(ProyectoFinalWebContext context, ICartaDAL cartaDAL, IMazoDAL mazoDAL)
        {
            _context = context;
            CartaDAL = cartaDAL;
            MazoDAL = mazoDAL;
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
            _context.Dispose();
        }
    }
}
