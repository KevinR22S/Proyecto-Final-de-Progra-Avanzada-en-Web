using BackEnd.Model;
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

        private MazoModel Convertir(Mazo mazo)
        {
            MazoModel entity = new MazoModel
            {
                MazoId=mazo.MazoId,
                NombreMazo=mazo.NombreMazo,
                CreadoEn=mazo.CreadoEn

            };
            return entity;
        }

        private Mazo Convertir(MazoModel mazo)
        {
            Mazo entity = new Mazo
            {
                MazoId = mazo.MazoId,
                NombreMazo = mazo.NombreMazo,
                CreadoEn = mazo.CreadoEn

            };
            return entity;
        }


        public bool Add(MazoModel mazo)
        {
            _unidadDeTrabajo.MazoDAL.Add(Convertir(mazo));
            return _unidadDeTrabajo.Complete();
        }


        public MazoModel Get(int id)
        {
            return Convertir(_unidadDeTrabajo.MazoDAL.Get(id));
        }

        public IEnumerable<MazoModel> Get()
        {
            var lista = _unidadDeTrabajo.MazoDAL.GetAll();
            List<MazoModel> mazo = new List<MazoModel>();
            foreach (var item in lista)
            {
                mazo.Add(Convertir(item));
            }
            return mazo;
        }

        public bool Remove(MazoModel mazo)
        {
            _unidadDeTrabajo.MazoDAL.Remove(Convertir(mazo));
            return _unidadDeTrabajo.Complete();
        }


        public bool Edit(MazoModel mazo)
        {
            _unidadDeTrabajo.MazoDAL.Update(Convertir(mazo));
            return _unidadDeTrabajo.Complete();
        }

    }
}
