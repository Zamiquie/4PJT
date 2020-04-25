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
    public class ProductController : ControllerBase
    {
        public DalProduit dal { get; set; }

        public ProductController()
        {
            dal = new DalProduit();
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

        // GET: Produit/{name}
        [HttpGet("name/{designation}", Name = "designation")]
        public async Task<string> GetSearchByName(string designation)
        {
            var Produit = await dal.GetProduitByName(designation);
            return Produit;
        }

        // GET: Produit/{id}/Commentary
        [HttpGet("{id}/commentary", Name = "GetCommentary")]
        public async Task<string> GetCommentary(string id)
        {
            var commentaries = await dal.GetCommentaryById(id);
            return commentaries;
        }

        // GET: Produit/{id}/supplier
        [HttpGet("{id}/supplier", Name = "GetSupplier")]
        public async Task<string> GetSupplier(string id)
        {
            var supplier = await dal.GetSupplierByIdProduct(id);
            return supplier;
        }

        // GET: Produit/{id}/delivery
        [HttpGet("{id}/delivery", Name = "GetDeliveries")]
        public async Task<string> GetDelivery(string id)
        {
            var deliveries = await dal.GetDeliveryByIdProduct(id);
            return deliveries;
        }

        #endregion

        #region POST
        // POST: Product/addProd
        [HttpPost]
        [Route("addProd")]
        public void Post([FromBody] Produit newProduit)
        {

            _ = dal.AddProduitAsync(newProduit);

        }

        //POST: product/addProdMulti
        [HttpPost]
        [Route("addProdMulti")]
        public void AddMultiProd([FromBody] List<Produit> newProduits)
        {
            _ = dal.AddMultiProduit(newProduits);
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
