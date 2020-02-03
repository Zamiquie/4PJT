using MongoDB.Bson;
using SupMagasin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Dal
{
    public class DalCustomer : Dal<Customer>
    {

        public DalCustomer() : base("Customer")
        {
        }


        public async Task<string> AddCustomerAsync(Customer newCustomer)
        {
            return await AddElement(newCustomer); 
        }

        public async Task<string> GetAllCustomer()
        {
            return await QueryAllElement();
        }

        public async Task<string> GetCustomerByID(int id)
        {
            var list = await QueryElementById();
            return list.Where(cu => cu.Id == id).ToJson();
        }

     
    }
}
