﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    public class MagasinController : ControllerBase
    {
        public Dal_Magasin dal { get; set; }

        public MagasinController()
        {
            dal = new Dal_Magasin();
        }

        #region Get
        // GET: Magasin/All
        [Route("All")]
        [HttpGet]
        public async Task<string> GetAsync()
        {
            var allMagasin = await dal.GetAllMagasin();
            return allMagasin;
        }

        // GET: Magasin/5
        [HttpGet("{id}", Name = "idMagasin")]
        public async Task<string> GetAsync(string id)
        {
            var Magasin = await dal.GetMagasinByID(id);
            return Magasin;
        }
        #endregion

        #region POST
        // POST: Magasin/addMagasin
        [HttpPost]
        [Route("addMagasin")]
        public void Post([FromBody] Magasin newMagasin)
        {

            _ = dal.AddMagasinAsync(newMagasin);

        }
        #endregion

        #region PUT
        // PUT: Magasin/updateMagasin
        [Route("updateMagasin")]
        [HttpPut]
        public Task<string> Put([FromBody] Magasin value)
        {
            return dal.UpdateMagasin(value);
        }
        #endregion

        #region DELETE
     
        [Route("Delete")]
        [HttpDelete]
        public async Task<string> DeleteMany([FromBody] List<Magasin> Magasins)
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
