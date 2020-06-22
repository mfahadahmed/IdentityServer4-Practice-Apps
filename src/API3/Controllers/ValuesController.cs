using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API3.Controllers
{
    [Route("values")]
    public class ValuesController : ControllerBase
    {
        public IActionResult GetValues()
        {
            return Ok(new Dictionary<string, string> { { "Message", "Values1 endpoint of API3 called with Default Policy" } });
        }
    }
}
