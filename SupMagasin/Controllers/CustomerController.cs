using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;
using SupMagasin.Dal;
using SupMagasin.Model;

namespace SupMagasin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        DalCustomer dal { get; set; }

        public CustomerController()
        {
            dal = new DalCustomer();
        }
        // GET: Customer
        [HttpGet]
        public async Task<string> GetAsync()
        {
            var allCustomer = await dal.GetAllCustomer();
            return allCustomer;
        }
        
        // GET: Customer/id
        [HttpGet("{id}", Name = "Get")]
        public async Task<string> Get(int id)
        {
            return await dal.GetCustomerByID(id);
        }

        // POST: Customer/add
        [HttpPost]
        [Route("add")]
        public void Post([FromBody] Customer value)
        {
            DalCustomer dal = new DalCustomer();
            _ = dal.AddCustomerAsync(value);
            
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
