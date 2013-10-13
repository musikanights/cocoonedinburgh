using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CocoonMicrosite
{
    public class Bus
    {
        public string Id { get; set; }
        public Venue Venue { get; set; }
        public List<Seat> Seats { get; set; }
        public Time Time { get; set; }
    }
}