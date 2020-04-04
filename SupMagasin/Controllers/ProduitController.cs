using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupMagasin.Dal;
using SupMagasin.Model;


namespace SupMagasin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProduitController : ControllerBase
    {
        public Dal_Produit dal { get; set; }

        public ProduitController()
        {
            dal = new Dal_Produit();
        }

        #region Get
        // GET: Produit/All
        [Route("All")]
        [HttpGet]
        public async Task<string> GetAsync()
        {

            var allProduit = await dal.GetAllProduit();
            return allProduit;
        }

        // GET: Produit/5
        [HttpGet("{id}", Name = "idProduit")]
        public async Task<string> GetAsync(string id)
        {
            var Produit = await dal.GetProduitByID(id);
            return Produit;
        }
        #endregion

        #region POST
        // POST: Produit/addProduit
        [HttpPost]
        [Route("addProduit")]
        public void Post([FromBody] Produit newProduit)
        {

            _ = dal.AddProduitAsync(newProduit);

        }
        #endregion

        #region PUT
        // PUT: Produit/updateProduit
        [Route("updateProduit")]
        [HttpPut]
        public Task<string> Put([FromBody] Produit value)
        {
            return dal.UpdateProduit(value);
        }
        #endregion

        #region DELETE
        [Route("DeleteProd")]
        [HttpDelete]
        public async Task<string> DeleteMany([FromBody] List<Produit> Produits)
        {
            if(Produits.Count == 1)
            {
                return await dal.RemoveProduit(Produits[0].ID);
            }
            return await dal.RemoveLotProduit(Produits);
        }
        #endregion
    }
}
