using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SupMagasin.Dal;
using SupMagasin.Model;


namespace SupMagasin.Controllers
{
    [Route("[controller]")]
    [ApiController]
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
        [HttpPut("")]
        public Task<string> Put([FromBody] Produit value)
        {
            return dal.UpdateProduit(value);
        }
        #endregion

        #region DELETE
        // DELETE: Produit/DeleteOnly/5
        [Route("DeleteOnlyProduit/{id}")]
        [HttpDelete("{id}")]
        public Task<string> Delete(string id)
        {
            return dal.RemoveProduit(id);
        }

        [Route("DeleteManyProduits")]
        [HttpDelete]
        public async Task<string> DeleteMany([FromBody] List<Produit> Produits)
        {
            return await dal.RemoveLotProduit(Produits);
        }
        #endregion
    }
}
