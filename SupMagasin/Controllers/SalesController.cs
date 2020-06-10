using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using SupMagasin.Dal;
using SupMagasin.Model;
using SupMagasin.Utils;
using SupSales.Dal;

namespace SupMagasin.Controllers
{
    [Route("sales")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors(PolicyName = "PolicyFrontEnd")]
    public class SalesController : ControllerBase
    {
         DalSales Dal { get; set;  }

        public SalesController()
        {
            Dal = new DalSales();
        }

        #region GET
        // GET: sales/All
        [Route("All")]
        [HttpGet]
        public string GetAsync()
        {
            var allsales =  Dal.GetAllSale();
            return allsales;
        }

        // GET: sales/id
        [HttpGet("{id}")]
        public async Task<string> Get(string id)
        {
            var currentSale = await Dal.GetSaleByID(id);
            return currentSale.ToJson() ;
        }


        //GET: sales/user/{name}
        [HttpGet("user/{iduser}")]
        public async Task<string> GetSaleByUser(string iduser)
        {
            var result = await Dal.GetSalesByIdUser(iduser);
            result = result.OrderByDescending(fac => fac.SaleDate).ToList();
            return result.ToJson();
            
        }

        //GET : sales/facture/{idSale}
        [HttpGet("/facture/{idSale}")]
        public async Task<ActionResult> GetFacturePdf(string idSale)
        {
            //on recupere la vente
            Sale sale = await Dal.GetSaleByID(idSale);
            //string du fichier
            string pathFile = Directory.GetCurrentDirectory() + "/Asset/Factures/" + sale.IdShop + "/" + "f" + sale.ID + ".pdf";

            try
            {
               //on reupère les bytes du fichier
               byte[] fileToDowload = System.IO.File.ReadAllBytes(pathFile);
                return File(fileToDowload, "application/pdf", "facture_" + idSale + ".pdf", false);

            }
            catch(DirectoryNotFoundException e)
            {
                return BadRequest("file not founds. Error code:"+e.Message);
            }
            catch(Exception e)
            {
                return BadRequest("Erreur interne:"+e.Message);
            }
        }

        //GET: sales/shop/
        [HttpGet("shop/{id}")]
        public async Task<string> GetSalesByShop(string id)
        {
            return await Dal.GetSalesByIdMagasin(id);
        }

        [HttpGet("/sales/ca")]
        public string GetCA(DateTime seachBegin,DateTime searchEnd)
        {
            
            if (seachBegin == null) { seachBegin = new DateTime(2020, 01, 01); };
            if (searchEnd == null) { seachBegin = new DateTime(2099, 01, 01); }
            return Dal.GetTotalSaleBording(seachBegin, searchEnd).ToJson();
        }
        #endregion

        #region POST
        // POST: sales/addsales
        [HttpPost]
        [Route("addsales")]
        public async Task<IActionResult> Addsales([FromBody] Sale sale)
        {
            //enregistrement de la vente
            var saleFinish = await Dal.AddSalesAsync(sale);
            //récupération des datas liès  la vente
            DalCustomer dal = new DalCustomer();
            var customer = await dal.GetCustomerByID(sale.IdCustomer);
            DalShop dalShop = new DalShop();
            var shop = await dalShop.GetShopByID(sale.IdShop);
            //création du document
            await GenerateDocument.GenerateFacture(sale, customer, shop);
            //envois d'un email avec facture
            new Mailling().sendFactureByMail(sale, customer);
            //renvois 
            return Ok(new { message = "ID Sale :"+ saleFinish });

        }

        // POST: sales/addMultisales
        [HttpPost]
        [Route("addMultisales")]
        public void AddMultisales([FromBody] List<Sale> newSales)
        {
            _ = Dal.AddMultiSalesAsync(newSales);
        }

        #endregion

        #region PUT
        // PUT: sales/updatesales
        [Route("updatesales")]
        [HttpPut]
        public Task<string> Put([FromBody] Sale value)
        {
            return Dal.UpdateSales(value);
        }
        #endregion

        #region DELETE
        //DELETE : sales/Delete
        [Route("Delete")]
        [HttpDelete]
        public async Task<string> DeleteMany([FromBody] List<Sale> lotSales)
        {
            if (lotSales.Count == 1)
            {
                return await Dal.RemoveSales(lotSales[0].ID);
            }

            return await Dal.RemoveLotSales(lotSales);
        }
        #endregion
    }
}
