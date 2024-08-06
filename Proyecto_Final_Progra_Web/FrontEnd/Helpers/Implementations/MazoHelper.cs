using FrontEnd.ApiModels;
using FrontEnd.Helpers.Intefaces;
using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers.Implementations
{
    public class MazoHelper : IMazoHelper
    {
        IServiceRepository ServiceRepository;

        public MazoHelper(IServiceRepository serviceRepository)
        {
            this.ServiceRepository = serviceRepository;
        }

        private MazoViewModel Convertir(Mazo mazo)
        {
            return new MazoViewModel
            {
                MazoId=mazo.MazoId,
                NombreMazo=mazo.NombreMazo,
                UsuarioId=mazo.UsuarioId,
                CreadoEn = mazo.CreadoEn   
            };
        }

        private Mazo Convertir(MazoViewModel mazo)
        {
            return new Mazo
            {
                MazoId = mazo.MazoId,
                NombreMazo = mazo.NombreMazo,
                UsuarioId = mazo.UsuarioId,
                CreadoEn = mazo.CreadoEn
            };
        }

        public MazoViewModel Add(MazoViewModel mazo)
        {
            HttpResponseMessage response = ServiceRepository.PostResponse("api/mazo", Convertir(mazo));
            if (response != null)
            {
                var content = response.Content.ReadAsStringAsync().Result;
            }
            return mazo;
        }

        public List<MazoViewModel> GetMazos()
        {
            HttpResponseMessage response = ServiceRepository.GetResponse("api/mazo");
            List<Mazo> resultado = new List<Mazo>();
            if (response != null)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                resultado = JsonConvert.DeserializeObject<List<Mazo>>(content);
            }
            List<MazoViewModel> mazo = new List<MazoViewModel>();
            foreach (var item in resultado)
            {
                mazo.Add(Convertir(item));
            }
            return mazo;
        }

        public MazoViewModel GetMazo(int id)
        {
            HttpResponseMessage response = ServiceRepository.GetResponse("api/mazo/" + id.ToString());
            Mazo resultado = new Mazo();
            if (response != null)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                resultado = JsonConvert.DeserializeObject<Mazo>(content);
            }
            return Convertir(resultado);
        }

        public MazoViewModel Remove(int id)
        {
            HttpResponseMessage response = ServiceRepository.DeleteResponse("api/mazo/" + id.ToString());
            Mazo resultado = new Mazo();
            if (response != null)
            {
                var content = response.Content.ReadAsStringAsync().Result;
            }
            return Convertir(resultado);
        }

        public MazoViewModel Update(MazoViewModel mazo)
        {
            HttpResponseMessage response = ServiceRepository.PutResponse("api/mazo", Convertir(mazo));
            if (response != null)
            {
                var content = response.Content.ReadAsStringAsync().Result;
            }
            return mazo;
        }
    }
}
