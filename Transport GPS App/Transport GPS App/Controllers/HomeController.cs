using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Transport_GPS_App.Models;

namespace Transport_GPS_App.Controllers
{
    public class HomeController : Controller
    {


        public List<Vehicle> TheList = new List<Vehicle>();
        public HomeController()
        {
            var webRequest = WebRequest.Create(@"https://transport.tallinn.ee/gps.txt");

            var response = webRequest.GetResponse();
            var content = response.GetResponseStream();
            var reader = new StreamReader(content);

            var lines = reader.ReadToEnd();
            List<string> vehicleList = new List<String>(lines.Split(new char[] { '\n' }));

            foreach(var item in vehicleList)
            {
                List<string> SplitItem = new List<string>(item.Split(new char[] { ',' }));
                if( item != "" )
                {
                    SplitItem.RemoveAt(4);

                    var New = new Vehicle
                    {
                        Type = SplitItem.ElementAt(0),
                        Route = SplitItem.ElementAt(1),

                        Longtitude = SplitItem.ElementAt(2),
                        Latitude = SplitItem.ElementAt(3),

                        Angle = SplitItem.ElementAt(4),
                        VehicleID = SplitItem.ElementAt(5)
                    };
                    TheList.Add(New);
                }
            }
        }
        

        public ActionResult Index(string sortBy)
        {
            //var vehicles = TheList;

            ViewBag.SortTypeParameter = string.IsNullOrEmpty(sortBy) ? "Type desc" : "";
            ViewBag.SortRouteParameter = sortBy == "Route" ? "Route desc" : "";

            var vehicles = TheList.AsQueryable();

            return View(vehicles);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}