using DAL.Intefaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class UnidadDeTrabajo : IUnidadDeTrabajo
    {
        public ICartaDAL CartaDAL { get; set; }

        private ProyectoFinalWebContext _context;
        public IMazoDAL MazoDAL { get; set; }


        public UnidadDeTrabajo(ProyectoFinalWebContext context,
            ICartaDAL cartaDAL, IMazoDAL mazoDAL) { 
            
            this._context = context;
            this.CartaDAL = cartaDAL;
            this.MazoDAL = mazoDAL;
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
