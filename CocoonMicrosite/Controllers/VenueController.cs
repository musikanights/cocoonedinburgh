using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CocoonMicrosite.Controllers
{
    public class VenueController : ApiController
    {
        private BookingData bookingData;

        // GET api/venue
        public IEnumerable<string> Get()
        {
            bookingData = new BookingData();

            return new string[] { "value1", "value3" };
        }

        // GET api/venue/5
        public Venue Get(string id)
        {
            bookingData = new BookingData();

            Seat seat = new Seat()
            {
                BookedName = "Gareth Barry",
                BookingReference = "cocoon_5"
            };

            bookingData.SaveSeatBooking("1", seat);

            //var times = bookingData.GetTimes();
            Bus buswithSeats = bookingData.GetBusWithSeats(id);

            return bookingData.GetVenue(id);
        }

        // POST api/venue
        public void Post(/*[FromBody]Venue venue*/)
        {
            bookingData = new BookingData();
            
        }

        // PUT api/venue/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/venue/5
        public void Delete(int id)
        {
        }
    }
}
