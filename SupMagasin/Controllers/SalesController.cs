using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupMagasin.Model;
using SupSales.Dal;

namespace SupMagasin.Controllers
{
    [Route("sales")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            return await Dal.GetSaleByID(id);
        }


        //GET: sales/user/{name}
        [HttpGet("user/{iduser}")]
        public async Task<string> GetSaleByUser(string iduser)
        {
            return await Dal.GetSalesByIdUser(iduser);
        }

        //GET: sales/shop/
        [HttpGet("shop/{id}")]
        public async Task<string> GetSalesByShop(string id)
        {
            return await Dal.GetSalesByIdMagasin(id);
        }
        #endregion

        #region POST
        // POST: sales/addsales
        [HttpPost]
        [Route("addsales")]
        public void Addsales([FromBody] Sale sale)
        {
            _ = Dal.AddSalesAsync(sale);
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
