using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Kids.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FamilyController : ControllerBase
    {
        // GET api/values
        [HttpPost]
        public ActionResult<IEnumerable<string>> Index([FromBody] int family)
        {
            return new string[] { "index", family.ToString() };
        }

        [HttpPost()]
        public ActionResult<IEnumerable<string>> Actions([FromBody] int id)
        {
            return new string[] { "value1", "value2" };
        }

    }
}
