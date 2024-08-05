using BackEnd.Services.Interfaces;
using DAL.Intefaces;
using Entities.Entities;
using System.Collections.Generic;

namespace BackEnd.Services.Implementations
{
    public class CartaService : ICartaService
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public CartaService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public bool Add(Carta carta)
        {
            var result = _unidadDeTrabajo.CartaDAL.Add(carta);
            if (result)
            {
                _unidadDeTrabajo.Complete();
            }
            return result;
        }

        public Carta Get(int id)
        {
            return _unidadDeTrabajo.CartaDAL.Get(id);
        }

        public IEnumerable<Carta> Get()
        {
            return _unidadDeTrabajo.CartaDAL.GetAll();
        }

        public bool Remove(Carta carta)
        {
            var result = _unidadDeTrabajo.CartaDAL.Remove(carta);
            if (result)
            {
                _unidadDeTrabajo.Complete();
            }
            return result;
        }

        public bool Update(Carta carta)
        {
            var result = _unidadDeTrabajo.CartaDAL.Update(carta);
            if (result)
            {
                _unidadDeTrabajo.Complete();
            }
            return result;
        }
    }
}
