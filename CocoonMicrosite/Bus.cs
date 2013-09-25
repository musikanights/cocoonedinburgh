using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CocoonMicrosite
{
    public class Bus
    {
        public int Id { get; set; }
        public Location Location { get; set; }
        public List<Seat> Seats { get; set; }
        public DateTime Time { get; set; }
    }
}