using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APIStartController : ControllerBase
    {

        public APIStartController()
        {
            Start();
        }


        public IActionResult Start()
        {
            return Ok("API has been started!");
        }    

    }
}
