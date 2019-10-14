using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

namespace Transport_Unit_Tests
{
    [TestClass]
    public class ValueTests
    {
        public List<string> vehicleList;

        public List<string> VehicleTypes = new List<string>();
        public List<string> VehicleRoutes = new List<string>();
        public List<string> Longtitudes = new List<string>();
        public List<string> Latitudes = new List<string>();
        public List<string> Angles = new List<string>();
        public List<string> IDs = new List<string>();

        public ValueTests()
        {
            var webRequest = WebRequest.Create(@"https://transport.tallinn.ee/gps.txt");

            var response = webRequest.GetResponse();
            var content = response.GetResponseStream();
            var reader = new StreamReader(content);

            var lines = reader.ReadToEnd();
            vehicleList = new List<String>(lines.Split(new char[] { '\n' }));

            foreach (var item in vehicleList)
            {
                List<string> SplitItem = new List<string>(item.Split(new char[] { ',' }));
                if (item != "")
                {
                    SplitItem.RemoveAt(4);

                    VehicleTypes.Add(SplitItem.ElementAt(0));
                    VehicleRoutes.Add(SplitItem.ElementAt(1));
                    Longtitudes.Add(SplitItem.ElementAt(2));
                    Latitudes.Add(SplitItem.ElementAt(3));
                    Angles.Add(SplitItem.ElementAt(4));
                    IDs.Add(SplitItem.ElementAt(5));
                }
            }
        }


        [TestMethod]
        public void TypeValidation()
        {
            var Trol = new string("1");
            var Bus = new string("2");
            var Tram = new string("3");

            foreach ( var item in VehicleTypes)
            {
                bool Status = false;
                if (item == Trol || item== Bus || item == Tram)
                {
                    Status = true;
                    Assert.IsTrue(Status);
                }
                else
                {
                    Assert.Fail("Unexpected Vehicle Type detected.");
                }
            }
        }

        [TestMethod]
        public void LongtitudeValidation()
        {
            foreach( var item in Longtitudes)
            {
                bool Status = false;
                if( item == "0")
                {
                    Status = true;
                    Assert.IsTrue(Status);
                }
                else
                {
                    var Numbers = item.ToCharArray();
                    if (Numbers[0] == '2' && (Numbers[1] == '4' || Numbers[1] == '5') && Numbers[7] == '0')
                    {
                        Status = true;
                        Assert.IsTrue(Status);
                    }
                    else
                    {
                        Assert.Fail("Longtitude value is invalid.");
                    }
                }
            }
        }

        [TestMethod]
        public void LatitudeValidation()
        {
            foreach (var item in Latitudes)
            {
                bool Status = false;
                if (item == "0")
                {
                    Status = true;
                    Assert.IsTrue(Status);
                }
                else
                {
                    var Numbers = item.ToCharArray();
                    if (Numbers[0] == '5' && (Numbers[1] == '9' || Numbers[1] == '8') && Numbers[7] == '0')
                    {
                        Status = true;
                        Assert.IsTrue(Status);
                    }
                    else
                    {
                        Assert.Fail("Latitude value is invalid.");
                    }
                }
            }
        }

        [TestMethod]
        public void AngleValidation()
        {
            foreach(var item in Angles)
            {
                bool Status = false;
                int value = Int32.Parse(item);
                if( value == 999)
                {
                    Status = true;
                    Assert.IsTrue(Status);
                }
                else
                {
                    if (value <= 360)
                    {
                        Status = true;
                        Assert.IsTrue(Status);
                    }
                    else
                    {
                        Assert.Fail("Angle value is invalid.");
                    }
                }
            }
        }

    }
}
