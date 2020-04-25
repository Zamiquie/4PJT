using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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


        //GET: Customer/id/BanqDatas
        [HttpGet("{id}/BanqDatas")]
        public async Task<string> GetDataAccount(string id)
        {
            return await dal.GetBanqsAccountCount(id);
        }

        //GET: Customer/id/Phones
        [HttpGet("{id}/Phones")]
        public async Task<string> GetPhones(string id)
        {
            return await dal.GetAllPhonesCustomer(id);
        }
        #endregion

        #region POST
        // POST: Customer/addMultiCustomer
        [HttpPost]
        [Route("addCustomer")]
        public void AddCustomer([FromBody] Customer customer)
        {
            _ = dal.AddCustomerAsync(customer);   
        }

        // POST: customer/addMultiCustomer
        [HttpPost]
        [Route("addMultiCustomer")]
        public void AddMultiCustomer([FromBody] List<Customer> newCustomers)
        {
            _ = dal.AddLotCustomerAsync(newCustomers);
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
        //DELETE : Customer/Delete
        [Route("Delete")]
        [HttpDelete]
        public async Task<string> DeleteMany([FromBody] List<Customer> customers)
        {
            if (customers.Count == 1)
            {
                return await dal.RemoveCustomer(customers[0].Id);
            }
            
            return await dal.RemoveLotCustomer(customers);
        }
        #endregion

    }
}
