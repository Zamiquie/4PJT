using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SupMagasin.Dal;
using SupMagasin.Model;

namespace SupMagasin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors(PolicyName = "PolicyFrontEnd")]
    public class ShopController : ControllerBase
    {
        public DalShop dal { get; set; }

        public ShopController()
        {
            dal = new DalShop();
        }

        #region Get
        // GET: Magasin/All
        [Route("All")]
        [HttpGet]
        public async Task<string> GetAsync()
        {
            var allMagasin = dal.GetAllShop();
            return allMagasin;
        }

        // GET: Magasin/5
        [HttpGet("{id}", Name = "idMagasin")]
        public async Task<string> GetAsync(string id)
        {
            var Magasin = await dal.GetShopByID(id);
            return Magasin;
        }

        [HttpGet("{id}/work", Name = "getWorkers")]
        public async Task<string> GetWorker(string id)
        {
            var employes = await dal.GetEmployesByShop(id);
            return employes;
        }

        [HttpGet("{id}/borne", Name = "getBornes")]
        public async Task<string> GetBornes(string id)
        {
            var bornes = await dal.GetBorneByShop(id);
            return bornes;
        }

        [HttpGet("{id}/rayon", Name = "getRayon")]
        public async Task<string> GetRayons(string id)
        {
            var rayons = await dal.GetRayonByShop(id);
            return rayons;
        }

        #endregion

        #region POST
        // POST: shop/addShop
        [HttpPost]
        [Route("addShop")]
        public void Post([FromBody] Shop newMagasin)
        {

            _ = dal.AddMagasinAsync(newMagasin);

        }

        [HttpPost]
        // POST: shop/addShop
        [Route("addMultiShop")]
        public void Post([FromBody] List<Shop> newMagasins)
        {
            _ = dal.AddMultiMagasinAsync(newMagasins);
        }
        #endregion

        #region PUT
        // PUT: Magasin/updateMagasin
        [Route("updateMagasin")]
        [HttpPut]
        public Task<string> Put([FromBody] Shop value)
        {
            return dal.UpdateMagasin(value);
        }
        #endregion

        #region DELETE
     
        [Route("Delete")]
        [HttpDelete]
        public async Task<string> DeleteMany([FromBody] List<Shop> Magasins)
        {
            if (Magasins.Count == 1)
            {
                return await dal.RemoveMagasin(Magasins[0].ID);
            }
        
            return await dal.RemoveLotMagasin(Magasins);
        }
        #endregion
    }
}
