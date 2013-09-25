using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CocoonMicrosite.Controllers
{
    public class BookingsController : ApiController
    {
        // GET api/bookings
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/bookings/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/bookings
        public void Post([FromBody]string value)
        {
        }

        // PUT api/bookings/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/bookings/5
        public void Delete(int id)
        {
        }

        #region Private Methods

        

        #endregion
    }
}