using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SupMagasin.Dal;
using SupMagasin.Model;

namespace SupMagasin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeController : ControllerBase
    {
        public Dal_Employe dal { get; set; }

        public EmployeController()
        {
            dal = new Dal_Employe();
        }

        #region Get
        // GET: Employe/AllEmploye
        [Route("All")]
        [HttpGet]
        public async Task<string> GetAsync()
        {

            var allEmploye = await dal.GetAllEmploye();
            return allEmploye;
        }

        // GET: Employe/5
        [HttpGet("{id}", Name = "idEmploye")]
        public async Task<string> GetAsync(string id)
        {
            var Employe = await dal.GetEmployeByID(id);
            return Employe;
        }
        #endregion

        #region POST
        // POST: Employe/addEmploye
        [HttpPost]
        [Route("addEmploye")]
        public void Post([FromBody] Employe newEmploye)
        {

            _ = dal.AddEmployeAsync(newEmploye);

        }
        #endregion

        #region PUT
        // PUT: Employe/updateEmploye
        [Route("updateEmploye")]
        [HttpPut("")]
        public Task<string> Put([FromBody] Employe value)
        {
            return dal.UpdateEmploye(value);
        }
        #endregion

        #region DELETE
        // DELETE: Employe/DeleteOnly/5
        [Route("DeleteOnlyEmploye/{id}")]
        [HttpDelete("{id}")]
        public Task<string> Delete(string id)
        {
            return dal.RemoveEmploye(id);
        }

        [Route("DeleteManyEmploye")]
        [HttpDelete]
        public async Task<string> DeleteMany([FromBody] List<Employe> employes)
        {
            return await dal.RemoveLotEmploye(employes);
        }
        #endregion
    }
}
