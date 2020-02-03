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
    public class CategorieController : ControllerBase
    {
        public List<Categorie> Categorieers { get; set; } = new List<Categorie>();

        public CategorieController()
        {
            for (int x = 0; x < 10; x++)
            {
                Categorieers.Add(new Categorie()
                {
                    
                });
            }
        }

        // GET: Categorie
        [HttpGet]
        public string Get()
        {
            return JsonConvert.SerializeObject(Categorieers);
        }

        // GET: Categorie/5
        [HttpGet("{id}", Name = "GetCategorie")]
        public string Get(int id)
        {
            return JsonConvert.SerializeObject(Categorieers[id]);
        }

        // POST: Categorie
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: Categorie/5
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
