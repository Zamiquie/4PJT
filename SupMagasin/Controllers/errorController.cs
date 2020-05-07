using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SupMagasin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors(PolicyName = "PolicyFrontEnd")]
    public class ErrorController : ControllerBase
    {
        // GET: api/error
        [HttpGet] 
        public ActionResult Error()
        {
            //var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            return Problem();
        }
        
    }
}
