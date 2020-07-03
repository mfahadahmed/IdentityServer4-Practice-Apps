using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API3.Controllers
{
    [Route("identity")]
    public class IdentityController : ControllerBase
    {
        [HttpGet("get1")]
        [Authorize(Policy = "policy1")]
        public IActionResult Get1()
        {
            return Ok(new Dictionary<string, string> { { "Message", "Get1 of API3 called" } });
        }

        [HttpGet("get2")]
        [Authorize(Policy = "policy2")]
        public IActionResult Get2()
        {
            return Ok(new Dictionary<string, string> { { "Message", "Get2 of API3 called" } });
        }
    }
}
