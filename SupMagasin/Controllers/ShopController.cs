using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SupMagasin.Model;

namespace SupMagasin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        public List<Magasin> Shopers { get; set; } = new List<Magasin>();

        public ShopController()
        {
            for (int x = 0; x < 10; x++)
            {
                Shopers.Add(new Magasin()
                {
                    ID = x,
                    Name = "Magasin_" + x.ToString(),
                    Adress = "26 Rue du Clos",
                    Postal_Code = "0"+x.ToString()+"500",
                    City = "Grellote"
                });
            }
        }

        // GET: Shop
        [HttpGet]
        public string Get()
        {
            return JsonConvert.SerializeObject(Shopers);
        }

        // GET: Shop/5
        [HttpGet("{id}", Name = "GetShop")]
        public string Get(int id)
        {
            return JsonConvert.SerializeObject(Shopers[id]);
        }

        // POST: Shop
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: Shop/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
