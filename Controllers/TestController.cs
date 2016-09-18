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

        public TestController(){
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
        }


        public JsonResult AvailableArea(int inLocId)
        {
            //inputs
            //Location inLoc = locationList[0];
            DateTime inStart= new DateTime(2016,9,23,12,30,38);
            DateTime inEnd = new DateTime(2016,9,23,18,0,0);
            //endinput

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
            if (result.Count==0)
                return Json("not found");
            else
                return Json(result);

            
        }

        //fix inputted's time according to a time unit
        public DateTime fixTime()
        {
            DateTime input = new DateTime(2016,10,23,12,0,39);

            //putting this here now, ill implement it properly later
            //values are in minutes
            int timeUnit = 30;
            int gracePeriod = 10;

            int min = input.Minute%timeUnit;
            if (min==0) min=0;
            else min=min*timeUnit;
            


            return input;
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