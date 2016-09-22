using System;
using System.Collections.Generic;
using Devi.ParkingService.Models;
using Microsoft.AspNetCore.Mvc;

namespace Devi.ParkingService.Controllers
{

    public class TestController : Controller
    {
        private static List<Location> locationList = new List<Location>();
        private static List<Area> areaList = new List<Area>();
        private static List<Booking> bookingList = new List<Booking>();

        static TestController()
        {
            locationList.Add(new Location{
                Name="Location1",
                Id=1
            });
            areaList.Add(new Area{
                Name="P1",
                Id=1,
                LocationId=1,
                Capacity=2
            });
            areaList.Add(new Area{
                Name="P2",
                Id=2,
                LocationId=1,
                Capacity=1
            });
            bookingList.Add(new Booking{
                Id=1,
                CustomerId=1,
                CarId=1,
                AreaId=1,
                LocationId=1,
                start=new DateTime(2016,9,23,12,0,39),
                end=new DateTime(2016,9,23,22,0,39)
            });
            bookingList.Add(new Booking{
                Id=2,
                CustomerId=2,
                CarId=2,
                AreaId=1,
                LocationId=1,
                start=new DateTime(2016,9,23,12,0,39),
                end=new DateTime(2016,9,23,22,0,39)
            });
            bookingList.Add(new Booking{
                Id=3,
                CustomerId=1,
                CarId=1,
                AreaId=1,
                LocationId=1,
                start=new DateTime(2016,8,23,12,0,39),
                end=new DateTime(2016,8,23,22,0,39)
            });
        }

        public TestController(){
            
        }

        public List<Area> GetArea(int inLocId, DateTime inStart, DateTime inEnd)
        {
            List<Area> result = new List<Area>();

            //fetch areas that is in the inputted location
            for(int i=0; i<areaList.Count; i++)
            {
                if (inLocId == areaList[i].LocationId)
                {
                    //for each booking that 'collides' with the input, avCount++
                    //area is available if avCount<area.Capacity
                    int avCount=0;

                    //loop bookingList, check each booking if it collides with input
                    for(int j=0; j<bookingList.Count; j++)
                    {
                        //area check
                        //  Query : SELECT * FROM booking WHERE Id=inLocId
                        if (areaList[i].Id == bookingList[j].AreaId)
                        {
                            if(!checkTime(bookingList[j], inStart, inEnd)) avCount++;
                        }
                    }
                    
                    if (avCount<areaList[i].Capacity) result.Add(areaList[i]);

                }
            }
            return result;

        }

        public ActionResult AvailableArea()
        {

            //Get from form index.cshtml
            string var1 = Request.Form["inputId"];
            int inLocId = Int32.Parse(var1);
            int inCarId = Int32.Parse(Request.Form["carId"]);
            int inCustId = Int32.Parse(Request.Form["custId"]);

            DateTime inStart = Convert.ToDateTime(Request.Form["instart"]);
            inStart = inStart.Add(new TimeSpan(Int32.Parse(Request.Form["instarthour"]), Int32.Parse(Request.Form["instartminute"]), 0));

            DateTime inEnd = Convert.ToDateTime(Request.Form["inend"]);
            inEnd = inEnd.Add(new TimeSpan(Int32.Parse(Request.Form["inendhour"]), Int32.Parse(Request.Form["inendminute"]), 0));

            //hardcoded inputs
            //int inLocId = 1;
            //DateTime inStart= new DateTime(2016,9,24,12,30,38);
            //DateTime inEnd = new DateTime(2016,9,24,18,0,0);
            //endinput

            List<Area> result = GetArea(inLocId, inStart, inEnd);

            if (result.Count==0) return Json("not found");

            Booking newBooking = new Booking{
                Id=-1,
                CustomerId=inCustId,
                CarId=inCarId,
                AreaId=-1,
                LocationId=inLocId,
                start=inStart,
                end=inEnd
            };

            //return Json(result);
            return RedirectToAction("ChooseArea", new {Id=-1,
                CustomerId=inCustId,
                CarId=inCarId,
                AreaId=-1,
                LocationId=inLocId,
                start=inStart,
                end=inEnd});
        }

        public ActionResult ChooseArea(Booking nBk)
        {
            List<Area> avArea = GetArea(nBk.LocationId, nBk.start, nBk.end);

            return View(avArea);
        }

        [HttpPost]
        public ActionResult SaveBooking(Booking nBk)
        {
            bookingList.Add(new Booking{
                Id=bookingList.Count,
                CustomerId=nBk.CustomerId,
                CarId=nBk.CarId,
                AreaId=nBk.AreaId,
                LocationId=nBk.LocationId,
                start=nBk.start,
                end=nBk.end,
            });

            return Json("done");
        }


        //fix inputted's time according to a time unit
        public DateTime fixStartTime()
        {
            DateTime input = new DateTime(2016,10,23,12,0,39);

            //putting this here now, ill implement it properly later
            //values are in minutes
            int timeUnit = 30;

            int min;
            if (timeUnit<60)
            {
                min = input.Minute%timeUnit;
                if (min==0) 
                    min=0;
                else 
                    min=min*timeUnit;
            }
            else 
                min=0;

            return new DateTime(input.Year, input.Month, input.Day, input.Hour, min, 0);
        }

        //check between a booking's start and end value with
        //inputed's start and end value
        public bool checkTime(Booking book1, DateTime start, DateTime end)
        {
            int valueA, valueB, valueC, valueD;

            valueA = DateTime.Compare(start, book1.start);
            valueB = DateTime.Compare(start, book1.end);
            valueC = DateTime.Compare(end, book1.start);
            valueD = DateTime.Compare(end, book1.end);

            if (valueA == valueB &&
                valueC == valueD &&
                valueA == valueC)
            return true; //no collision, slot can be used
            else return false; //slot is used
        }

        public JsonResult Test()
        {
            
            //input
            Booking book1 = bookingList[0];
            DateTime end = new DateTime(2016,10,23,20,30,38);
            DateTime start = new DateTime(2016,10,23,12,0,39);
            //endinput

            return Json(checkTime(book1, start, end));

        }

        //view practice etc
        
        public ViewResult Index()
        {
            return View();
        }

        public ActionResult DisplayBookings()
        {
            return View();
        }

    }

}