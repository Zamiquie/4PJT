using MongoDB.Bson;
using SupMagasin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SupMagasin.Model.CustomerModel;
using SupMagasin.Utils;
using System.Text;
using System.Security.Cryptography;

namespace SupMagasin.Dal
{
    public class DalCustomer : Dal<Customer>
    {

        public DalCustomer() : base("Customer")
        {
        }


        //region d'insertion des données
        #region Insert

        // On element
        public async Task<string> AddCustomerAsync(Customer newCustomer)
        {
            newCustomer.Id = GenerateId(newCustomer);
            newCustomer.Password = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(newCustomer.Password)));
            return await AddElement(newCustomer); 
        }

        //Multi Element
        public async Task<string> AddLotCustomerAsync(List<Customer> newCustomers)
        {
            foreach(Customer cust in newCustomers)
            {
                cust.Password = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(cust.Password)));
            }
            return await AddListOfElement(newCustomers);
        }

        #endregion

        //region d'interrogation des données
        #region Query
        public async Task<string> GetCustomerByID(string id)
        {
            var list = await QueryElementById();
            return list.Where(cu => cu.Id == id).ToJson();
        }
        
        public async Task<string> GetAllCustomer()
        {
            
            return await QueryAllElement();
        }
        public async Task<List<Customer>> GetCustomerByName(string name)
        {
            var result = await Collection.FindAsync(r => r.Name == name);
            return result.ToList();
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            var result = await Collection.FindAsync(cus => cus.Email == email);
            return result.FirstOrDefault();
        }

        public async Task<string> GetBanqsAccountCount(string id)
        {
            var result = await Collection.FindAsync(cus => cus.Id == id);
            return result.First().BanqData.ToJson();
        }

        public async Task<string> GetAllPhonesCustomer(string id)
        {
            var result = await Collection.FindAsync(cus => cus.Id == id);
            return result.First().Phones.ToJson();
        }

        #endregion

        //region des suppression de données
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

        //region de mise a jour 
        #region Update

        public async Task<string> UpdateCustomer(Customer currentCustomer)
        {
            return await UdpateElement(currentCustomer.Id.ToString(), currentCustomer);
        }
        #endregion

        //Region Gestion des Enfants
        public async Task<string> AddBankAccount(string id ,BanqDataModel banqData )
        {
            //on filtre et on met
            var filter = Builders<Customer>.Filter.Eq("_id", id);
            var update = Builders<Customer>.Update.Push<BanqDataModel>(enf => enf.BanqData,banqData);

            try
            {
                await Collection.UpdateOneAsync(filter, update);
                return true.ToJson();
            }
            catch (MongoException e)
            {

                new WriteLog(TypeLog.MangoDb).WriteFile(e.Message);
                return false.ToJson();

            } 
        }
        public async Task<string> AddPhones(string id, PhoneModel phoneModel)
        {
            //on filtre et on met l'update
            var filter = Builders<Customer>.Filter.Eq("_id", id);
            var update = Builders<Customer>.Update.Push<PhoneModel>(enf => enf.Phones, phoneModel );

            try
            {
                await Collection.UpdateManyAsync(filter, update);
                return true.ToJson();
            }
            catch (MongoException e)
            {

                new WriteLog(TypeLog.MangoDb).WriteFile(e.Message);
                return false.ToJson();

            }
        }

        #region Private
        //Generation de Id
        private string GenerateId(Customer customer)
        {
            return customer.Adress.Substring(0,3).Replace(' ','X') + customer.Name[0] + customer.FirstName[0] + customer.Postal_Code.Substring(2,2);  
        }

        #endregion

    }
}
