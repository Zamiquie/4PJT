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

        public async Task<string> GetCustomerByID(string id)
        {
            var list = await QueryElementById();
            return list.Where(cu => cu.Id == id).ToJson();
        }
        public async Task<string> UpdateCustomer(Customer currentCustomer)
        {
            return await UdpateElement(currentCustomer.Id.ToString(), currentCustomer);
             
        }
        #region Delete
        public async Task<string> RemoveCustomer(string id)
        {
            return await DeleteEntry(id);
        }
        public async Task<string> RemoveLotCustomer(List<Customer> customersToDelete)
        {
            return await DeleteMultielement(customersToDelete);
        }
        #endregion

    }
}
