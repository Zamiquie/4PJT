using MongoDB.Bson;
using SupMagasin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Dal
{
    public class Dal_Borne : Dal<Borne>
    {

        public Dal_Borne() : base("Bornes") { }

        public async Task<string> AddBorneAsync(Borne newBorne)
        {
            return await AddElement(newBorne);
        }

        public async Task<string> GetAllBorne()
        {
            return await QueryAllElement();
        }

        public async Task<string> GetBorneByID(string id)
        {
            var list = await QueryElementById();
            return list.Where(cu => cu.ID == id).ToJson();
        }
        public async Task<string> UpdateBorne(Borne currentBorne)
        {
            return await UdpateElement(currentBorne.ID.ToString(), currentBorne);

        }
        #region Delete
        public async Task<string> RemoveBorne(string ID)
        {
            return await DeleteEntry(ID);
        }
        public async Task<string> RemoveLotBorne(List<Borne> BornesToDelete)
        {
            return await DeleteMultielement(BornesToDelete);
        }
        #endregion



    }
}
