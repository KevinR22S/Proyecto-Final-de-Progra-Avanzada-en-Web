using BackEnd.Services.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MazoController : ControllerBase
    {
        private IMazoService _mazoService;
        public MazoController(IMazoService mazoService)
        {
            this._mazoService = mazoService;
        }
        // GET: api/<MazoController>
        [HttpGet]
        public IEnumerable<Mazo> Get()
        {
            return _mazoService.Get();
        }

        // GET api/<MazoController>/5
        [HttpGet("{id}")]
        public Mazo Get(int id)
        {
            return _mazoService.Get(id);
        }

        // POST api/<MazoController>
        [HttpPost]
        public Mazo Post([FromBody] Mazo mazo)
        {
            _mazoService.Add(mazo);
            return mazo;
        }

        // PUT api/<MazoController>/5
        [HttpPut("{id}")]
        public Mazo Put([FromBody] Mazo mazo)
        {
            _mazoService.Update(mazo);
            return mazo;
        }

        // DELETE api/<MazoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Mazo mazo = new Mazo { MazoId = id };
            _mazoService.Remove(mazo);
        }
    }
}
