using BackEnd.Model;
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
        public IEnumerable<MazoModel> Get()
        {
            return _mazoService.Get();
        }

        // GET api/<MazoController>/5
        [HttpGet("{id}")]
        public MazoModel Get(int id)
        {
            return _mazoService.Get(id);
        }

        // POST api/<MazoController>
        [HttpPost]
        public MazoModel Post([FromBody] MazoModel mazo)
        {
            _mazoService.Add(mazo);
            return mazo;
        }

        // PUT api/<MazoController>/5
        [HttpPut("{id}")]
        public MazoModel Put(int id, [FromBody] MazoModel supplier)
        {
            _mazoService.Edit(supplier);
            return supplier;
        }

        // DELETE api/<MazoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            MazoModel mazo = new MazoModel { MazoId = id };
            _mazoService.Remove(mazo);
        }
    }
}
