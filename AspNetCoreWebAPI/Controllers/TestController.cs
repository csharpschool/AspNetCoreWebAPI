using AspNetCoreWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebAPI.Controllers
{
    [Route("api/test")]
    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var model = new Message { Id = 1, Text = "Message, from the Get action." };

            return Ok(model);
        }
 
    }
}
