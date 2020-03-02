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
        #region GET
        // GET: Customer/All
        [Route("All")]
        [HttpGet]
        public async Task<string> GetAsync()
        {
            var allCustomer = await dal.GetAllCustomer();
            return allCustomer;
        }
        
        // GET: Customer/id
        [HttpGet("{id}", Name = "Get")]
        public async Task<string> Get(string id)
        {
            return await dal.GetCustomerByID(id);
        }
        #endregion

        #region POST
        // POST: Customer/add
        [HttpPost]
        [Route("addCustomer")]
        public void Post([FromBody] Customer value)
        {
            _ = dal.AddCustomerAsync(value);   
        }
        #endregion

        #region PUT
        // PUT: Customer/updateCustomer
        [Route("updateCustomer")]
        [HttpPut]
        public Task<string> Put([FromBody] Customer value)
        {
            return dal.UpdateCustomer(value);
        }

        #endregion

        #region DELETE
        // DELETE: Customer/DeleteOnly/id
        [Route("DeleteOnly/{id}")] 
        [HttpDelete("{id}",Name = "idDelete")]
        public Task<string> Delete(string id)
        {
            return dal.RemoveCustomer(id);
        }

        //DELETE : Customer/DeleteMany
        [Route("DeleteMany")]
        [HttpDelete]
        public Task<string> DeleteMany([FromBody] List<Customer> customers)
        {
            return dal.RemoveLotCustomer(customers);
        }
        #endregion

    }
}
