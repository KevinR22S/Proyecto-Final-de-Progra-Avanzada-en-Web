using BackEnd.Model;
using BackEnd.Services.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartaController : ControllerBase
    {
        private ICartaService _cartaService;

        public CartaController(ICartaService cartaService)
        {
            this._cartaService = cartaService;
        }
        // GET: api/<CartaController>
        [HttpGet]
        public IEnumerable<CartaModel> Get()
        {
            return _cartaService.Get();
        }

        // GET api/<CartaController>/5
        [HttpGet("{id}")]
        public CartaModel Get(int id)
        {
            return _cartaService.Get(id);
        }

        // POST api/<CartaController>
        [HttpPost]
        public CartaModel Post([FromBody] CartaModel carta)
        {
            _cartaService.Add(carta);
            return carta;
        }

        // PUT api/<CartaController>/5
        [HttpPut("{id}")]
        public CartaModel Put([FromBody] CartaModel carta)
        {
            _cartaService.Update(carta);
            return carta;
        }

        // DELETE api/<CartaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            CartaModel carta = new CartaModel { CartaId = id };
            _cartaService.Remove(carta);
        }
    }
}
