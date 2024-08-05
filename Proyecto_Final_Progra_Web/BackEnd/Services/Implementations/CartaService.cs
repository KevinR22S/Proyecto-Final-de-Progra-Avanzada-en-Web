using BackEnd.Model;
using BackEnd.Services.Interfaces;
using DAL.Intefaces;
using Entities.Entities;

namespace BackEnd.Services.Implementations
{
    public class CartaService : ICartaService
    {
        private IUnidadDeTrabajo _unidadDeTrabajo;

        private ICartaDAL cartaDAL;

        public CartaService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        private CartaModel Convertir(Carta carta)
        {
            CartaModel entity = new CartaModel
            {
                CartaId = carta.CartaId,
                Nombre = carta.Nombre,
                Descripcion = carta.Descripcion,
                PuntosAtaque = carta.PuntosAtaque,
                PuntosDefensa = carta.PuntosDefensa,
                CreadoEn= carta.CreadoEn
               
            };
            return entity;
        }

        private Carta Convertir(CartaModel carta)
        {
            Carta entity = new Carta
            {
                CartaId = carta.CartaId,
                Nombre = carta.Nombre,
                Descripcion = carta.Descripcion,
                PuntosAtaque = carta.PuntosAtaque,
                PuntosDefensa = carta.PuntosDefensa,
                CreadoEn = carta.CreadoEn

            };
            return entity;
        }


        public bool Add(CartaModel carta)
        {
            _unidadDeTrabajo.CartaDAL.Add(Convertir(carta));
            return _unidadDeTrabajo.Complete();
        }


        public CartaModel Get(int id)
        {
            return Convertir(_unidadDeTrabajo.CartaDAL.Get(id));
        }

        public IEnumerable<CartaModel> Get()
        {
            var lista = _unidadDeTrabajo.CartaDAL.GetAll();
            List<CartaModel> carta = new List<CartaModel>();
            foreach (var item in lista)
            {
                carta.Add(Convertir(item));
            }
            return carta;
        }

        public bool Remove(CartaModel carta)
        {
            _unidadDeTrabajo.CartaDAL.Remove(Convertir(carta));
            return _unidadDeTrabajo.Complete();
        }


        public bool Update(CartaModel carta)
        {
            _unidadDeTrabajo.CartaDAL.Update(Convertir(carta));
            return _unidadDeTrabajo.Complete();
        }


    }
}
