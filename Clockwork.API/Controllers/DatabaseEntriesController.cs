using Clockwork.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Clockwork.API.Controllers
{
    [Route("api/[controller]")]
    public class DatabaseEntriesController : Controller
    {
        // GET api/databaseentries
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<CurrentTimeQuery> returnVal;
            using (var db = new ClockworkContext())
            {
                returnVal = db.CurrentTimeQueries.ToList();
            }

            return Ok(returnVal);
        }
    }
}
