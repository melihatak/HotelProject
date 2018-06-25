using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hotelproject.Models
{
    public class ReservationVM
    {
        public string room_type { get; set; }
        public string room { get; set; }
        public string guest_name { get; set; }
        public int hosted_at_id { get; set; }
        public string checkin { get; set; }
        public string checkout { get; set; }
        public string made_by { get; set; }
    }
}