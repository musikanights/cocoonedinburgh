using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CocoonMicrosite
{
    public class Time
    {
        public string Id { get; set; }
        public string TimeSlot { get; set; }

        public Time() {}

        public Time(string id, string time)
        {
            Id = id;
            TimeSlot = time;
        }
    }
}