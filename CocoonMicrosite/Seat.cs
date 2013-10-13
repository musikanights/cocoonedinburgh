using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CocoonMicrosite
{
    public class Seat
    {
        public string Id { get; set; }
        public string BookedName { get; set; }
        public string BookingReference { get; set; }

        public Seat() {}

        public Seat(string id, string bookedName, string bookingReference)
        {
            Id = id;
            BookedName = bookedName;
            BookingReference = bookingReference;
        }
    }
}