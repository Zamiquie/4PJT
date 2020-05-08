using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SupMagasin.Utils;

namespace SupMagasin.Controllers
{
    [Route("/")]
    [EnableCors(PolicyName = "PolicyFrontEnd")]
    public class DefaultController : Controller
    {
        public Mailling Mailing {get; set;}
        public DefaultController(IConfiguration configuration ){
             Mailing = new Mailling(configuration);
           
        }
        // GET: Default
        [HttpGet,HttpOptions]
        public ActionResult Index()
        {
            Mailing.SendAdvertissmentConnexionInd(Request);
            return View("Index");
        }
    }
}