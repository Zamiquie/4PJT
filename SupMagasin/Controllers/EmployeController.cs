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
    public class EmployeController : ControllerBase
    {
        public List<Employe> Employes { get; set; } = new List<Employe>();

        public EmployeController()
        {
            for (int x = 0; x < 10; x++)
            {
                Employes.Add(new Employe()
                {
                    ID = x,
                    Nom = "Esclave" + x.ToString(),
                    Prenom = "Slave" + x.ToString()
                });
            }

        }

        // GET: Employe
        [HttpGet]
        public string Get()
        {

            return JsonConvert.SerializeObject(Employes); 
            
        }

        // GET: Employe/5
        [HttpGet("{id}", Name = "id")]
        public string Get(int id)
        {
           return JsonConvert.SerializeObject(Employes[id]);
        }

        // POST: api/Employe
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Employe/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: Employe/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            try
            {
                Employes.RemoveAt(id);
                return true;
            }
            catch (ArgumentOutOfRangeException e)
            {

                return false;
            }
   
        }
    }
}
