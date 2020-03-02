﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;
using SupMagasin.Dal;
using SupMagasin.Model;

namespace SupMagasin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BorneController : ControllerBase
    {
        Dal_Borne dal { get; set; }

        public BorneController()
        {
            dal = new Dal_Borne();
        }
        #region GET
        // GET: Borne/All
        [Route("All")]
        [HttpGet]
        public async Task<string> GetAsync()
        {
            var allBorne = await dal.GetAllBorne();
            return allBorne;
        }
        
        // GET: Borne/id
        [HttpGet("{id}", Name = "idBorne")]
        public async Task<string> Get(string id)
        {
            return await dal.GetBorneByID(id);
        }
        #endregion

        #region POST
        // POST: Borne/addBorne
        [HttpPost]
        [Route("addBorne")]
        public void Post([FromBody] Borne value)
        {
            _ = dal.AddBorneAsync(value);   
        }
        #endregion

        #region PUT
        // PUT: Borne/updateBorne
        [Route("updateBorne")]
        [HttpPut]
        public Task<string> Put([FromBody] Borne value)
        {
            return dal.UpdateBorne(value);
        }

        #endregion

        #region DELETE
        // DELETE: Borne/DeleteOnly/id
        [Route("DeleteOnly/{id}")] 
        [HttpDelete("{id}",Name = "idDeleteBorne")]
        public Task<string> Delete(string id)
        {
            return dal.RemoveBorne(id);
        }

        //DELETE : Borne/DeleteMany
        [Route("DeleteMany")]
        [HttpDelete]
        public Task<string> DeleteMany([FromBody] List<Borne> Bornes)
        {
            return dal.RemoveLotBorne(Bornes);
        }
        #endregion


    }
}
