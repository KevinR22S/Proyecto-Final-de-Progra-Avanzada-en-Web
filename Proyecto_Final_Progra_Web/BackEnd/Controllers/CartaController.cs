using BackEnd.Services.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartaController : Controller
    {
        private ICartaService cartaService;

        public CartaController(ICartaService cartaService)
        {
            this.cartaService = cartaService;
        }


        // GET: CartaController
        [HttpGet]
        public IEnumerable<Carta> Get()
        {
            return cartaService.Get();
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public Carta Get(int id)
        {
            return cartaService.Get(id);
        }

        [HttpPost]
        public Carta Post([FromBody] Carta carta)
        {
            cartaService.Add(carta);
            return carta;

        }

        [HttpPut]
        public Carta Put([FromBody] Carta carta)
        {
            cartaService.Update(carta);
            return carta;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Carta carta = new Carta { CartaId = id };
            cartaService.Remove(carta);

        }
    }
}
