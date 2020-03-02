using MongoDB.Bson;
using SupMagasin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Dal
{
    public class Dal_Employe : Dal<Employe>
    {

        public Dal_Employe() : base("employes") { }

        public async Task<string> AddEmployeAsync(Employe newEmploye)
        {
            return await AddElement(newEmploye);
        }

        public async Task<string> GetAllEmploye()
        {
            return await QueryAllElement();
        }

        public async Task<string> GetEmployeByID(string id)
        {
            var list = await QueryElementById();
            return list.Where(cu => cu.ID == id).ToJson();
        }
        public async Task<string> UpdateEmploye(Employe currentEmploye)
        {
            return await UdpateElement(currentEmploye.ID.ToString(), currentEmploye);

        }
        #region Delete
        public async Task<string> RemoveEmploye(string ID)
        {
            return await DeleteEntry(ID);
        }
        public async Task<string> RemoveLotEmploye(List<Employe> EmployesToDelete)
        {
            return await DeleteMultielement(EmployesToDelete);
        }
        #endregion



    }
}
