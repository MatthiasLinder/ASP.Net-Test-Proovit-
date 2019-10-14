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
    public class FormatTests
    {
        public List<string> vehicleList;

        public List<string> VehicleTypes = new List<string>();
        public List<string> VehicleRoutes = new List<string>();
        public List<string> Longtitudes = new List<string>();
        public List<string> Latitudes = new List<string>();
        public List<string> Angles = new List<string>();
        public List<string> IDs = new List<string>();

        private bool headOnly;
        public bool HeadOnly
        {
            get { return headOnly; }
            set { headOnly = value; }
        }

        public FormatTests()
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
        public void InputExists()
        {
            using (WebClient client = new MyClient())
            {
                string link = client.DownloadString("https://transport.tallinn.ee/gps.txt");
                Assert.IsNotNull(link);
            }
        }

        [TestMethod]
        public void VehicleType_FormatTest()
        {
            foreach (var item in VehicleTypes)
            {
                int length = item.Length;
                int formatlength = 1;

                if (length == formatlength)
                {
                    Assert.AreEqual(length, formatlength);
                }
                else
                {
                    Assert.Fail("One or more Vehicle types is not valid.");
                }
            }
        }

        [TestMethod]
        public void VehicleRoutes_FormatTest()
        {
            foreach (var item in VehicleRoutes)
            {
                bool Status = false;
                int length = item.Length;

                int ValidLength1 = 1;
                int ValidLength2 = 2;
                int ValidLength3 = 3;

                

                if (length == ValidLength1 || length == ValidLength2 || length == ValidLength3)
                {
                    Status = true;
                    Assert.IsTrue(Status);
                }
                else
                {
                    Assert.Fail("One or more Vehicle Routes is not valid.");
                }
            }
        }

        [TestMethod]
        public void Longtitudes_FormatTest()
        {
            foreach (var item in Longtitudes)
            {
                int length = item.Length;
                int formatlength = 8;

                bool Status = false;

                if (length == formatlength || item == "0")
                {
                    Status = true;
                    Assert.IsTrue(Status);
                }
                else
                {
                    Assert.Fail("One or more Longtitude values not valid.");
                }
            }
        }

        [TestMethod]
        public void Latitudes_FormatTest()
        {
            foreach (var item in Latitudes)
            {
                int length = item.Length;
                int formatlength = 8;

                bool Status = false;

                if (length == formatlength || item == "0")
                {
                    Status = true;
                    Assert.IsTrue(Status);
                }
                else
                {
                    Assert.Fail("One or more Latitude values not valid.");
                }
            }
        }

        [TestMethod]
        public void Angles_FormatTest()
        {
            foreach (var item in Angles)
            {
                int length = item.Length;
                bool Status = false;

                if (length <= 3)
                {
                    Status = true;
                    Assert.IsTrue(Status);
                }
                else
                {
                    Assert.Fail("One or more Angles is not valid.");
                }
            }
        }

        [TestMethod]
        public void IDs_FormatTest()
        {
            foreach (var item in IDs)
            {
                int length = item.Length;

                bool Status = false;

                if (length <= 4)
                {
                    Status = true;
                    Assert.IsTrue(Status);
                }
                else
                {
                    Assert.Fail("Input Format is not valid.");
                }
            }
        }
    }



    class MyClient : WebClient
    {
        public bool HeadOnly { get; set; }
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest req = base.GetWebRequest(address);
            if (HeadOnly && req.Method == "GET")
            {
                req.Method = "HEAD";
            }
            return req;
        }
    }
}
