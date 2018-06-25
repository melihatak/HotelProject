using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hotelproject.Models
{
    public class AddReservationVM
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string gender { get; set; }
        public string checkin { get; set; }
        public string checkout { get; set; }
        public int room { get; set; }
    }
}