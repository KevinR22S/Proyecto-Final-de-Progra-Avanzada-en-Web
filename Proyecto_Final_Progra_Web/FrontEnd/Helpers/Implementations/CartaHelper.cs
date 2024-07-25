using FrontEnd.ApiModels;
using FrontEnd.Helpers.Intefaces;
using FrontEnd.Models;
using Newtonsoft.Json;


namespace FrontEnd.Helpers.Implementations
{
    public class CartaHelper : ICartaHelper
    {
        IServiceRepository ServiceRepository;

        public CartaHelper(IServiceRepository serviceRepository)
        {
            this.ServiceRepository = serviceRepository;
        }

        private CartaViewModel Convertir(Carta carta)
        {
            return new CartaViewModel
            {
                CartaId = carta.CartaId,
                Nombre = carta.Nombre,
                Descripcion = carta.Descripcion,
                PuntosAtaque = carta.PuntosAtaque,
                PuntosDefensa = carta.PuntosDefensa,
                CreadoEn = carta.CreadoEn
            };
        }

        private Carta Convertir(CartaViewModel carta)
        {
            return new Carta
            {
                CartaId = carta.CartaId,
                Nombre = carta.Nombre,
                Descripcion = carta.Descripcion,
                PuntosAtaque = carta.PuntosAtaque,
                PuntosDefensa = carta.PuntosDefensa,
                CreadoEn = carta.CreadoEn
            };
        }


        public CartaViewModel Add(CartaViewModel carta)
        {
            HttpResponseMessage response = ServiceRepository.PostResponse("api/carta", Convertir(carta));
            if(response != null) 
            {
                var content= response.Content.ReadAsStringAsync().Result;
            }
            return carta;
        }

        public CartaViewModel GetCarta(int id)
        {
            HttpResponseMessage response = ServiceRepository.GetResponse("api/carta/" + id.ToString());
            Carta resultado= new Carta();
            if (response != null) 
            {
                var content = response.Content.ReadAsStringAsync().Result;
                resultado = JsonConvert.DeserializeObject<Carta>(content);
            }
            return Convertir(resultado);
        }

        public List<CartaViewModel> GetCartas()
        {
            HttpResponseMessage response = ServiceRepository.GetResponse("api/carta");
            List<Carta> resultado= new List<Carta>();
            if (response != null) 
            {
                var content = response.Content.ReadAsStringAsync().Result;
                resultado= JsonConvert.DeserializeObject<List<Carta>>(content);
            }
            List<CartaViewModel> cartas= new List<CartaViewModel>();
            foreach (var item in resultado)
            {
                cartas.Add(Convertir(item));
            }
            return cartas;
        }

        public CartaViewModel Remove(int id)
        {
            HttpResponseMessage response = ServiceRepository.DeleteResponse("api/carta/" + id.ToString());
            Carta resultado = new Carta();
            if (response != null) 
            {
                var content = response.Content.ReadAsStringAsync().Result;
            }
            return Convertir(resultado);
        }

        public CartaViewModel Update(CartaViewModel carta)
        {
            HttpResponseMessage response = ServiceRepository.PutResponse("api/carta", Convertir(carta));
            if (response != null) 
            {
                var content = response.Content.ReadAsStringAsync().Result;
            }
            return carta;
        }
    }
}
