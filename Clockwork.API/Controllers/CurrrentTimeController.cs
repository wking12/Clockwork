using Clockwork.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Clockwork.API.Controllers
{
    [Route("api/[controller]")]
    public class CurrentTimeController : Controller
    {
        // GET api/currenttime
        [HttpGet]
        public IActionResult Get(string timeZoneId)
        {
            TimeZoneInfo requestedTimeZone = TimeZoneInfo.Local;
            try
            {
                requestedTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            }
            catch (TimeZoneNotFoundException)
            {
                return BadRequest("Invalid timeZoneId.");
            }
            catch (ArgumentNullException)
            {
                return BadRequest("timeZoneId cannot be null.");
            }
            var utcTime = DateTime.UtcNow;
            var serverTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, requestedTimeZone);
            var ip = this.HttpContext.Connection.RemoteIpAddress.ToString();

            var returnVal = new CurrentTimeQuery
            {
                UTCTime = utcTime,
                ClientIp = ip,
                Time = serverTime,
                RequestedTimeZoneId = timeZoneId
            };

            using (var db = new ClockworkContext())
            {
                db.CurrentTimeQueries.Add(returnVal);
                var count = db.SaveChanges();
                Console.WriteLine("{0} records saved to database", count);

                Console.WriteLine();
                foreach (var CurrentTimeQuery in db.CurrentTimeQueries)
                {
                    Console.WriteLine(" - {0}", CurrentTimeQuery.UTCTime);
                }
            }

            return Ok(returnVal);
        }
    }
}
