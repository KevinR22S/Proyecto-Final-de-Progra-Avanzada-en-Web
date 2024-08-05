using BackEnd.Services.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartaController : ControllerBase
    {
        private readonly ICartaService _cartaService;

        public CartaController(ICartaService cartaService)
        {
            _cartaService = cartaService;
        }

        // GET: api/<CartaController>
        [HttpGet]
        public ActionResult<IEnumerable<Carta>> Get()
        {
            var cartas = _cartaService.Get();
            return Ok(cartas);
        }

        // GET api/<CartaController>/5
        [HttpGet("{id}")]
        public ActionResult<Carta> Get(int id)
        {
            var carta = _cartaService.Get(id);
            if (carta == null)
            {
                return NotFound();
            }
            return Ok(carta);
        }

        // POST api/<CartaController>
        [HttpPost]
        public ActionResult<Carta> Post([FromBody] Carta carta)
        {
            if (ModelState.IsValid)
            {
                _cartaService.Add(carta);
                return CreatedAtAction(nameof(Get), new { id = carta.CartaId }, carta);
            }
            return BadRequest(ModelState);
        }

        // PUT api/<CartaController>/5
        [HttpPut("{id}")]
        public ActionResult<Carta> Put(int id, [FromBody] Carta carta)
        {
            if (id != carta.CartaId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var updated = _cartaService.Update(carta);
                if (!updated)
                {
                    return NotFound();
                }
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        // DELETE api/<CartaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var carta = _cartaService.Get(id);
            if (carta == null)
            {
                return NotFound();
            }
            _cartaService.Remove(carta);
            return NoContent();
        }
    }
}
