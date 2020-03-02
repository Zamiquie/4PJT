using MongoDB.Bson;
using SupMagasin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupMagasin.Dal
{
    public class Dal_Magasin : Dal<Magasin>
    {

        public Dal_Magasin() : base("Magasin"){}


        public async Task<string> AddMagasinAsync(Magasin newMagasin)
        {
            return await AddElement(newMagasin);
        }

        public async Task<string> GetAllMagasin()
        {
            return await QueryAllElement();
        }

        public async Task<string> GetMagasinByID(string id)
        {
            var list = await QueryElementById();
            return list.Where(mag => mag.ID == id).ToJson();
        }
        public async Task<string> UpdateMagasin(Magasin currentMagasin)
        {
            return await UdpateElement(currentMagasin.ID.ToString(), currentMagasin);

        }

        #region Delete
        public async Task<string> RemoveMagasin(string id)
        {
            return await DeleteEntry(id);
        }
        public async Task<string> RemoveLotMagasin(List<Magasin> MagasinsToDelete)
        {
            return await DeleteMultielement(MagasinsToDelete);
        }
        #endregion
    }
}
