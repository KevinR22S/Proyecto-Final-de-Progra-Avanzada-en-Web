using BackEnd.Services.Interfaces;
using DAL.Intefaces;
using Entities.Entities;

namespace BackEnd.Services.Implementations
{
    public class MazoService : IMazoService
    {
        private IUnidadDeTrabajo _unidadDeTrabajo;

        private IMazoDAL mazoDAL;

        public MazoService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            this._unidadDeTrabajo = unidadDeTrabajo;
        }

        public bool Add(Mazo mazo)
        {
            return _unidadDeTrabajo.MazoDAL.Add(mazo);
        }

        public Mazo Get(int id)
        {
            return _unidadDeTrabajo.MazoDAL.Get(id);
        }

        public IEnumerable<Mazo> Get()
        {
            return _unidadDeTrabajo.MazoDAL.GetAll();
        }

        public bool Remove(Mazo mazo)
        {
            return _unidadDeTrabajo.MazoDAL.Remove(mazo);
        }

        public bool Update(Mazo mazo)
        {
            return _unidadDeTrabajo.MazoDAL.Update(mazo);
        }
    }
}
