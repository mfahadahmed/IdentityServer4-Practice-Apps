using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API1.Controllers
{
    [Route("identity")]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        [HttpGet("get1")]
        [Authorize(Policy = "policy1")]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        [HttpGet("get2")]
        [Authorize(Policy = "policy2")]
        public IActionResult Get2()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
