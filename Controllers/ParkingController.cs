using System;
using System.Collections.Generic;
using Devi.ParkingService.Models;
using Microsoft.AspNetCore.Mvc;

namespace Devi.ParkingService.Controllers
{

    public class ParkingController : Controller
    {
        private static List<Location> locationList = new List<Location>();
        private static List<Area> areaList = new List<Area>();
        private static List<Booking> bookingList = new List<Booking>();

        public ParkingController()
        {
            locationList.Add(new Location{
                Name="Location1",
                Id=1
            });
            areaList.Add(new Area{
                Name="P1",
                Id=1,
                LocationId=1,
                Capacity=1
            });
            areaList.Add(new Area{
                Name="P2",
                Id=2,
                LocationId=1,
                Capacity=1
            });
        }

        [HttpGet]
        public JsonResult GetArea(int idnum)
        {
            Area returnVal = null;

            for (int i=0; i<areaList.Count; i++)
            {
                if (areaList[i].Id == idnum)
                    returnVal = areaList[i];
            }

            return Json(returnVal);

        }

        [HttpPost]
        public JsonResult SaveArea(Area area)
        {
            Area newArea = new Area
            {
                Name=area.Name,
                Id=area.Id,
                LocationId=area.LocationId,
                Capacity=area.Capacity
            };
            areaList.Add(newArea);
            return Json(newArea);

        }

        [HttpPost]
        public JsonResult SaveBooking(Booking booking)
        {
            Booking newBooking = new Booking
            {
                Id=booking.Id,
                CustomerId=booking.CustomerId,
                CarId=booking.CarId,
                LocationId=booking.LocationId,
                AreaId=booking.AreaId,
                start=booking.start,
                end=booking.end
            };
            bookingList.Add(newBooking);
            return Json(newBooking);
        }

        public List<Area> getAvailableArea(DateTime start, DateTime end, Location loc)
        {
            List<Area> result = null;

            for(int i=0; i<areaList.Count; i++)
            {
                if(areaList[i].LocationId == loc.Id)
                {
                    result.Add(areaList[i]);
                }
            }

            return result;
        }

        //bagian booking
        public JsonResult startBooking(DateTime start, DateTime end, Location loc)
        {
            List<Area> getAreas = getAvailableArea(start,end,loc);
            if (getAreas.Count==0)
                return Json(false); //flag

            for (int i=0; i<bookingList.Count; i++)
            {
                
            }
            return Json(true);

        }

        


        
    }
}
