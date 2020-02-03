using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SupMagasin.Model;

namespace SupBorne.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BorneController : ControllerBase
    {
        public List<Borne> Bornes { get; set; } = new List<Borne>();

        public BorneController()
        {
            for (int x = 0; x < 10; x++)
            {
                Bornes.Add(new Borne()
                {
                    ID = x,
                    Position = "Borne_" + x.ToString(),
                    EtatBorne = EtatBorne.EnStock
                });
            }
        }

        // GET: Borne
        [HttpGet]
        public string Get()
        {
            return JsonConvert.SerializeObject(Bornes);
        }

        // GET: Borne/5
        [HttpGet("{id}", Name = "GetBorne")]
        public string Get(int id)
        {
            return JsonConvert.SerializeObject(Bornes[id]);
        }

        // POST: Borne
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: Borne/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: Borne/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
