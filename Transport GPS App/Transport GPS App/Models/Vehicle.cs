using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Transport_GPS_App.Models
{
    public class Vehicle
    {
        public string Type { get; set; }
        public string Route { get; set; }

        public string Longtitude { get; set; }
        public string Latitude { get; set; }

        public string Angle { get; set; }
        public string VehicleID { get; set; }
    }
}