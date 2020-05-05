using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SupMagasin.Controllers
{
    [Route("/")]
    [EnableCors(PolicyName = "PolicyFrontEnd")]
    public class DefaultController : Controller
    {
        // GET: Default
        [HttpGet,HttpOptions]
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}