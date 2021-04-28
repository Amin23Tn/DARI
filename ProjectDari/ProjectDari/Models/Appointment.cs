using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectDari.Models
{
    public class Appointment
    {
        public int id { get; set; }
        public DateTime date_app { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public DateTime meeting_time { get; set; }
        public string image { get; set; }


    }
}