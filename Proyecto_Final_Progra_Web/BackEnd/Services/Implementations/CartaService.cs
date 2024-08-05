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
            return _unidadDeTrabajo.CartaDAL.Add(carta);
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
            return _unidadDeTrabajo.CartaDAL.Remove(carta);
        }

        public bool Update(Carta carta)
        {
            return _unidadDeTrabajo.CartaDAL.Update(carta);
        }
    }
}
